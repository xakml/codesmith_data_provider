using DeployLXLicensing;

namespace DeployLXKeyGenerator
{
	internal interface IMachineProfileEntryMaker
	{
		string[] GetHardwareHash(MachineProfileEntryType type);
	}
}
