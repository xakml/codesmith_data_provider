using System;

namespace DeployLXLicensing
{
	[Flags]
	public enum SerialNumberFlags
	{
		Flag1 = 0x1,
		Flag2 = 0x2,
		Flag3 = 0x4,
		Flag4 = 0x8,
		Flag5 = 0x10,
		Flag6 = 0x20,
		Flag7 = 0x40,
		Flag8 = 0x80,
		None = 0x0
	}
}
