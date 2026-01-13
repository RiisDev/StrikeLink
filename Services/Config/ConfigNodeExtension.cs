namespace StrikeLink.Services.Config
{
	/// <summary>
	/// Provides extension methods for navigating <see cref="ConfigNode"/> hierarchies.
	/// </summary>
	public static class ConfigNodeExtension
	{
		/// <summary>
		/// Adds further navigation methods to <see cref="ConfigNode"/>.
		/// </summary>
		extension(ConfigNode node)
		{
			/// <summary>
			/// Gets a nested configuration node using a dot-separated path.
			/// </summary>
			/// <param name="path">
			/// A dot-separated property path (for example, <c>"Software.Valve.Steam"</c>).
			/// </param>
			/// <returns>
			/// The configuration node located at the specified path.
			/// </returns>
			/// <exception cref="ArgumentException">
			/// Thrown when <paramref name="path"/> is <c>null</c>, empty, or whitespace.
			/// </exception>
			/// <exception cref="InvalidOperationException">
			/// Thrown when an intermediate node is not an object.
			/// </exception>
			/// <exception cref="KeyNotFoundException">
			/// Thrown when a path segment does not exist.
			/// </exception>
			public ConfigNode GetPath(string path)
			{
				if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path cannot be null or empty.", nameof(path));

				ReadOnlySpan<char> span = path.AsSpan();

				while (true)
				{
					int dotIndex = span.IndexOf('.');

					ReadOnlySpan<char> segment = dotIndex < 0 ? span : span[..dotIndex];

					node = node.GetProperty(segment.ToString());

					if (dotIndex < 0) return node;

					span = span[(dotIndex + 1)..];
				}
			}

			/// <summary>
			/// Attempts to get a nested configuration node using a dot-separated path.
			/// </summary>
			/// <param name="path">
			/// A dot-separated property path.
			/// </param>
			/// <param name="result">
			/// When this method returns, contains the configuration node if found;
			/// otherwise, <c>default</c>.
			/// </param>
			/// <returns>
			/// <c>true</c> if the node was found; otherwise, <c>false</c>.
			/// </returns>
			public bool TryGetPath(string path, out ConfigNode result)
			{
				try { result = node.GetPath(path); return true; }
				catch { result = default; return false; }
			}
		}
	}
}
