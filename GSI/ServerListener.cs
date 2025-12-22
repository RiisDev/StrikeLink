using StrikeLink.Extensions;
using StrikeLink.GSI.ObjectStates;
using StrikeLink.GSI.Parsing;
using System.Net;
using System.Net.Sockets;

// ReSharper disable UseAwaitUsing

namespace StrikeLink.GSI
{
	public class ServerListener : IDisposable, IAsyncDisposable
	{
		#region ServerLogic
		// Private Vars
		private TcpListener _listener = null!;
		private readonly GsiDispatcher _dispatcher = new(
			[
				new PlayerStateParser(),
				new MapStateParser()
			]
		);
		private readonly CancellationTokenSource _cts = new();
		private readonly SynchronizationContext? _syncContext = SynchronizationContext.Current;

		// Public readable values
		public event Action<Uri>? OnReady;
		public event Action<string>? OnPostReceived;
		public bool Ready { get; set { if (field == value) return; field = value; OnReady?.Invoke(new Uri($"http://{Address}:{Port}")); } }
		public IPAddress Address { get; private set; } = null!;
		public int Port { get; private set; }

		public async Task StartAsync(IPAddress? address = null, int? port = null, string? steamPath = null)
		{
			if (Process.GetProcessesByName("cs2").Length > 0)
				throw new InvalidOperationException("Counter Strike must not be running during initialization.");

			Address = address ?? IPAddress.Loopback;
			Port = port ?? GetFreePort();

			(IPAddress newAddress, int newPort) = await GsiManager.GenerateGsiFile(Address, Port, steamPath).ConfigureAwait(false);

			Address = newAddress;
			Port = newPort;

			try
			{
				_listener = new TcpListener(Address, Port);
				_listener.Start();
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
				{
					throw new InvalidOperationException("Port is already in-use, please use another port.");
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Unknown error occured during socket creation", ex);
			}

			Log($"Listening on: http://{Address}:{Port}/");

			_ = Task.Run(async () =>
			{
				while (!_cts.IsCancellationRequested)
				{
					try
					{
						TcpClient client = await _listener.AcceptTcpClientAsync(_cts.Token).ConfigureAwait(false);
						_ = Task.Run(async () =>
						{
							try { await HandleClientAsync(client).ConfigureAwait(false); }
							catch (Exception ex) { Log(ex.ToString()); }
						});
					}
					catch (OperationCanceledException) { }
					catch (ObjectDisposedException) { }
				}
			});

			Ready = true;
		}

		private async Task HandleClientAsync(TcpClient tcpClient)
		{
			Log($"New Client: {tcpClient.Client.RemoteEndPoint}");
			NetworkStream networkStream = tcpClient.GetStream();
			Log("Stream grabbed.");
			using StreamWriter streamWriter = new(networkStream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), leaveOpen: true);
			streamWriter.AutoFlush = true;
			Log("Streams hooked.");

			(int contentLength, byte[] prefetchedBody, int prefetchedLength) = await VerifyContentLength(networkStream).ConfigureAwait(false);

			Log($"Content length: {contentLength}");
			if (contentLength <= 1) { tcpClient.Close(); Log("Invalid content length."); return; }
			
			string requestBody = await ReadBodyAsync(
				networkStream,
				contentLength,
				prefetchedBody,
				prefetchedLength,
				_cts.Token
			).ConfigureAwait(false);


			OnPostReceived?.Invoke(requestBody);

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(requestBody);
				Log($"Valid request body: {requestBody}");
				foreach (IGsiPayload payload in _dispatcher.Dispatch(jsonDocument)) RaisePayload(payload);
			}
			catch (JsonException)
			{
				Log($"Invalid json body: {requestBody}");
				tcpClient.Close();
				return;
			}

			await WriteResponseAsync(streamWriter, 200, "OK").ConfigureAwait(false);
			tcpClient.Close();
		}

		private static async Task<string> ReadBodyAsync(NetworkStream networkStream, int contentLength, byte[] prefetchedBody, int prefetchedLength, CancellationToken cancellationToken)
		{
			Log($"Grabbing body bytes: {contentLength}");

			byte[] buffer = new byte[contentLength];

			if (prefetchedLength > 0)
			{
				Buffer.BlockCopy(
					prefetchedBody,
					0,
					buffer,
					0,
					prefetchedLength
				);
			}

			int totalRead = prefetchedLength;

			while (totalRead < contentLength)
			{
				int bytesRead = await networkStream.ReadAsync(
					buffer.AsMemory(totalRead, contentLength - totalRead),
					cancellationToken
				).ConfigureAwait(false);

				if (bytesRead == 0)
					throw new IOException("Client closed connection while reading body.");

				totalRead += bytesRead;
			}

			Log("Body bytes gathered");

			return Encoding.UTF8.GetString(buffer);
		}

		private static async Task<(int ContentLength, byte[] PrefetchedBody, int PrefetchedLength)> VerifyContentLength(NetworkStream networkStream)
		{
			(string headers, byte[] prefetchedBody, int prefetchedLength) = await ReadHeadersAsync(networkStream).ConfigureAwait(false);

			if (headers.IsNullOrEmpty())
				return (-1, [], 0);

			ReadOnlySpan<char> headerSpan = headers.AsSpan();
			int methodIndex = headerSpan.IndexOf(' ');
			if (methodIndex <= 0)
				return (-1, [], 0);

			ReadOnlySpan<char> methodSpan = headerSpan[..methodIndex];

			Log("Checking HTTP Method");
			if (!methodSpan.Equals("POST", StringComparison.OrdinalIgnoreCase))
				return (-1, [], 0);

			int contentLength = GetContentLength(headers);
			return contentLength <= 0 ? (-1, [], 0) : (contentLength, prefetchedBody, prefetchedLength);
		}

		private static async Task<(string Headers, byte[] PrefetchedBody, int PrefetchedLength)> ReadHeadersAsync(NetworkStream networkStream)
		{
			Log("Reading headers...");

			byte[] buffer = new byte[8192];
			int totalRead = 0;

			while (true)
			{
				int bytesRead = await networkStream.ReadAsync(
					buffer.AsMemory(totalRead),
					CancellationToken.None
				).ConfigureAwait(false);

				if (bytesRead == 0)
					throw new IOException("Client closed connection while reading headers.");

				totalRead += bytesRead;

				int headerEndIndex = FindHeaderTerminator(buffer.AsSpan(0, totalRead));
				if (headerEndIndex >= 0)
				{
					string headers = Encoding.ASCII.GetString(buffer, 0, headerEndIndex);

					int prefetchedBodyLength = totalRead - headerEndIndex;
					byte[] prefetchedBody = new byte[prefetchedBodyLength];

					if (prefetchedBodyLength > 0)
					{
						Buffer.BlockCopy(
							buffer,
							headerEndIndex,
							prefetchedBody,
							0,
							prefetchedBodyLength
						);
					}

					return (headers, prefetchedBody, prefetchedBodyLength);
				}

				if (totalRead == buffer.Length)
					throw new InvalidOperationException("HTTP headers too large.");
			}
		}


		private static int GetContentLength(string input)
		{
			ReadOnlySpan<char> headers = input.AsSpan();
			Log(input);
			while (!headers.IsEmpty)
			{
				int lineEndIndex = headers.IndexOf("\r\n");
				ReadOnlySpan<char> line;

				if (lineEndIndex == -1)
				{
					line = headers;
					headers = ReadOnlySpan<char>.Empty;
				}
				else
				{
					line = headers[..lineEndIndex];
					headers = headers[(lineEndIndex + 2)..];
				}

				int colonIndex = line.IndexOf(':');
				if (colonIndex <= 0) continue;

				ReadOnlySpan<char> headerName = line[..colonIndex].Trim();
				ReadOnlySpan<char> headerValue = line[(colonIndex + 1)..].Trim();

				if (!headerName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)) continue;
				if (int.TryParse(headerValue, out int contentLength)) return contentLength;

				return -1;
			}

			return -1;
		}
		
		private static int FindHeaderTerminator(ReadOnlySpan<byte> buffer)
		{
			for (int i = 0; i <= buffer.Length - 4; i++)
			{
				if (buffer[i] == (byte)'\r' &&
				    buffer[i + 1] == (byte)'\n' &&
				    buffer[i + 2] == (byte)'\r' &&
				    buffer[i + 3] == (byte)'\n')
				{
					return i + 4;
				}
			}

			return -1;
		}

		private static async Task WriteResponseAsync(StreamWriter streamWriter, int statusCode, string message, string excessMessage = "OK")
		{
			byte[] body = Encoding.UTF8.GetBytes(excessMessage);
			string headers =
				$"HTTP/1.1 {statusCode} {message}\r\n" +
				$"Content-Type: text/plain; charset=utf-8\r\n" +
				$"Content-Length: {body.Length}\r\n" +
				$"Connection: close\r\n\r\n";

			await streamWriter.WriteAsync(headers).ConfigureAwait(false);
			await streamWriter.BaseStream.WriteAsync(body).ConfigureAwait(false);
		}

		private static int GetFreePort()
		{
			TcpListener listener = new(IPAddress.Loopback, 0);
			listener.Start();
			int port = ((IPEndPoint)listener.LocalEndpoint).Port;
			listener.Stop();
			return port;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_cts.Cancel();
			_listener.Dispose();
			_cts.Dispose();
		}

		public async ValueTask DisposeAsync()
		{
			GC.SuppressFinalize(this);
			await _cts.CancelAsync().ConfigureAwait(false);
			_listener.Dispose();
			_cts.Dispose();
		}

		#endregion

		#region Event Logic

		private readonly Dictionary<Type, IGsiPayload> _payloadCache = [];

		public event Action<PlayerState>? PlayerStateReceived;
		public event Action<MapState>? MapStateReceived;

		private void RaisePayload(IGsiPayload payload)
		{
			if (_syncContext is null) Invoke();
			else _syncContext.Post(_ => Invoke(), null);

			return;

			void Invoke()
			{
				Type type = payload.GetType();
				Log($"Payload detected: {type.FullName}");

				bool exists = _payloadCache.TryGetValue(type, out IGsiPayload? oldPayload);

				switch (payload)
				{
					case PlayerState playerState:
						if (oldPayload is PlayerState oldPlayerState && playerState == oldPlayerState) return;
						PlayerStateReceived?.Invoke(playerState);
						break;
					case MapState mapState:
						if (oldPayload is MapState oldMapState && mapState == oldMapState) return;
						MapStateReceived?.Invoke(mapState);
						break;
				}

				if (exists) _payloadCache[type] = payload;
				else _payloadCache.TryAdd(type, payload);
			}
		}


		#endregion
	}

}
