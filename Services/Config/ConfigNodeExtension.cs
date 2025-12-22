namespace StrikeLink.Services.Config
{
	public static class ConfigNodeExtension
	{
		extension(ConfigNode node)
		{
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

			public bool TryGetPath(string path, out ConfigNode result)
			{
				try { result = node.GetPath(path); return true; }
				catch { result = default; return false; }
			}
		}
	}
}
