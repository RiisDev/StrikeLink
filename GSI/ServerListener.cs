using StrikeLink.Extensions;
using StrikeLink.GSI.ObjectStates;
using StrikeLink.GSI.Parsing;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

// ReSharper disable UseAwaitUsing

namespace StrikeLink.GSI
{
	public class ServerListener : IDisposable, IAsyncDisposable
	{
		#region ServerLogic
		// Private Vars
		private readonly TcpListener _listener;
		private readonly GsiDispatcher _dispatcher;
		private readonly CancellationTokenSource _cts = new();
		private readonly SynchronizationContext? _syncContext;

		// Public readable values
		public event Action? OnReady;
		public event Action<string>? OnPostReceived;
		public bool Ready { get; set { if (field == value) return; field = value; OnReady?.Invoke(); } }
		public IPAddress Address { get; }
		public int Port { get; }

		public ServerListener(IPAddress? address = null, int? port = null)
		{
			Port = port ?? GetFreePort();
			Address = address ?? IPAddress.Loopback;
			_syncContext = SynchronizationContext.Current;
			_listener = new TcpListener(Address, Port);
			_dispatcher = new GsiDispatcher(
				[
					new PlayerStateParser(),
					new MapStateParser()
				]
			);
		}

		public async Task StartAsync(string? steamPath = null)
		{
			if (Process.GetProcessesByName("cs2").Length > 0)
				throw new InvalidOperationException("Counter Strike must not be running during initialization.");

			await GsiManager.GenerateGsiFile(Address, Port, steamPath).ConfigureAwait(false);

			_listener.Start();

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

			using StreamReader streamReader = new(networkStream, Encoding.UTF8, leaveOpen: true);
			using StreamWriter streamWriter = new(networkStream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), leaveOpen: true);
			streamWriter.AutoFlush = true;

			int contentLength = await VerifyContentLength(streamReader).ConfigureAwait(false);

			if (contentLength <= 1) { tcpClient.Close(); return; }

			char[] bodyBuffer = new char[contentLength];
			int readTotal = 0;

			while (readTotal < contentLength)
			{
				int read = await streamReader.ReadAsync(bodyBuffer, readTotal, contentLength - readTotal).ConfigureAwait(false);
				if (read == 0) break;
				readTotal += read;
			}

			string requestBody = new(bodyBuffer);
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

		[SuppressMessage("ReSharper", "InvertIf")]
		private static async Task<int> VerifyContentLength(StreamReader streamReader)
		{
			string httpHeaders = await ReadHeadersAsync(streamReader).ConfigureAwait(false);
			if (httpHeaders.IsNullOrEmpty()) { Log("Request data empty"); return -1; }

			ReadOnlySpan<char> headerSpan = httpHeaders.AsSpan();
			int methodIndex = headerSpan.IndexOf(' ');
			if (headerSpan.Length == 0 || methodIndex <= 0) { Log($"Invalid span: {headerSpan.Length} - {methodIndex}"); return -1; }
			ReadOnlySpan<char> methodSpan = httpHeaders.AsSpan()[..methodIndex];

			if (!methodSpan.Equals("POST", StringComparison.OrdinalIgnoreCase)) { Log($"Invalid method on caller: {methodSpan}"); return -1; }

			int contentLength = GetContentLength(httpHeaders);

			if (contentLength <= 0) { Log("ContentLength is empty"); return -1; }

			return contentLength;
		}

		private static int GetContentLength(string input)
		{
			ReadOnlySpan<char> headers = input.AsSpan();

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

		private static async Task<string> ReadHeadersAsync(StreamReader streamReader)
		{
			StringBuilder headersBuilder = new();

			while (true)
			{
				string? line = await streamReader.ReadLineAsync().ConfigureAwait(false);
				if (line is null || line.Length == 0) break;
				headersBuilder.AppendLine(line);
			}

			return headersBuilder.ToString();
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
