using System;

namespace DeployLX.Licensing.v4
{
	[Flags]
	public enum OSEditions
	{
		DomainController = 0x10,
		Home = 0x100000,
		MediaCenter = 0x8,
		NotSet = 0x0,
		Professional = 0x200000,
		Server = 0x2,
		SixtyFourBit = 0x10000,
		TabletPC = 0x4,
		ThirtyTwoBit = 0x20000,
		Workstation = 0x1
	}
}
