using System.Runtime.Versioning;


#if WINDOWS
using System.Management;
#endif

namespace StrikeLink.Services.WebService
{
	[SupportedOSPlatform("windows")]
	internal class ShadowCopyService
	{		
#if WINDOWS
		private static string GetDriveLetter(string filePath)
		{
			string volume = Path.GetPathRoot(filePath) ?? throw new InvalidOperationException("Could not determine volume root.");
			return volume;
		}
		internal static void CopyFilesViaShadowCopy(IEnumerable<string> filePaths, string saveLocation)
		{
			Dictionary<string, List<string>> filesByVolume = filePaths.GroupBy(GetDriveLetter).ToDictionary(g => g.Key, g => g.ToList());

			EnsureServicesRunning();

			foreach ((string volume, List<string> volumeFiles) in filesByVolume)
			{
				string? shadowId = null;

				try
				{
					shadowId = CreateShadowCopy(volume);
					Log($"Shadow copy created for volume {volume}: {shadowId}");

					foreach (string filePath in volumeFiles)
					{
						try
						{
							string shadowFilePath = GetShadowPath(shadowId, filePath);
							Log($"Shadow device path: {shadowFilePath}");

							string destination = Path.Combine(saveLocation, Path.GetFileName(filePath));
							File.Copy(shadowFilePath, destination, overwrite: true);

							Log($"Copied: {filePath} → {destination}");
						}
						catch (Exception ex)
						{
							Log($"Failed to copy '{filePath}': {ex.Message}");
						}
					}
				}
				catch (Exception ex)
				{
					Log($"Failed to create shadow copy for volume {volume}: {ex.Message}");
				}
				finally
				{
					if (shadowId is not null)
						DeleteShadowCopy(shadowId);
				}
			}
		}
		private static string GetShadowPath(string shadowId, string filePath)
		{
			using ManagementObjectSearcher searcher = new($"SELECT * FROM Win32_ShadowCopy WHERE ID = '{shadowId}'");
			ManagementObjectCollection shadows = searcher.Get();

			ManagementObject? shadow = null;
			foreach (ManagementObject obj in shadows) { shadow = obj; }

			if (shadow is null) {return "";}

			string deviceName = shadow["DeviceObject"]?.ToString() ?? string.Empty;
			string relativePath = filePath[Path.GetPathRoot(filePath)!.Length..];
			string shadowFilePath = Path.Combine(deviceName, relativePath);

			return shadowFilePath;
		}

		private static string CreateShadowCopy(string drive)
		{
			using ManagementClass shadowCopyClass = new(@"\\.\root\cimv2:Win32_ShadowCopy");

			ManagementBaseObject inParams = shadowCopyClass.GetMethodParameters("Create");
			inParams["Volume"] = drive;
			inParams["Context"] = "ClientAccessible";

			ManagementBaseObject result = shadowCopyClass.InvokeMethod("Create", inParams, null);

			uint returnValue = (uint)result["ReturnValue"];
			string shadowId = result["ShadowID"]?.ToString() ?? "Unknown";

			Log($"Return Value: {returnValue}");  // 0 = success
			Log($"Shadow ID: {shadowId}");
			return shadowId;
		}

		private static void DeleteShadowCopy(string shadowId)
		{
			using ManagementObjectSearcher searcher = new($"SELECT * FROM Win32_ShadowCopy WHERE ID = '{shadowId}'");
			ManagementObjectCollection shadows = searcher.Get();

			foreach (ManagementObject shadow in shadows)
			{
				shadow.Delete();
				Log($"Deleted Shadow ID: {shadowId}");
			}
		}

		private static readonly string[] RequiredServices = [
			"VSS",
			"SWPRV",
			"CryptSvc",
			"EventLog",
		];

		private static void EnsureServicesRunning()
		{
			foreach (string serviceName in RequiredServices)
			{
				using ManagementObject service = new($"Win32_Service.Name='{serviceName}'");
				service.Get();

				string startMode = (string)service["StartMode"];
				string state = (string)service["State"];

				Log($"Service: {serviceName} | State: {state} | StartMode: {startMode}");

				if (startMode.Equals("Disabled", StringComparison.OrdinalIgnoreCase))
				{
					using ManagementBaseObject changeParams = service.GetMethodParameters("ChangeStartMode");
					changeParams["StartMode"] = "Manual";
					service.InvokeMethod("ChangeStartMode", changeParams, null);
				}

				if (state.Equals("Running", StringComparison.OrdinalIgnoreCase)) continue;
				
				using ManagementBaseObject startResult = service.InvokeMethod("StartService", null, null);
				uint startCode = (uint)startResult["ReturnValue"];

				if (startCode != 0)
					throw new InvalidOperationException($"Failed to start {serviceName}. Code: {startCode}");
			}
		}
#endif
	}
}
