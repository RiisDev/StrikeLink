namespace StrikeLink.GSI
{
	// Empty interface.
#pragma warning disable CA1040
	internal interface IGsiPayload {}
#pragma warning restore CA1040

	internal interface IGsiParser
	{
		bool CanParse(JsonElement root);
		IGsiPayload Parse(JsonElement root);
	}

}