namespace DeployLXKeyGenerator
{
	public interface ILicenseCodeSupport
	{
		string CharacterSet
		{
			get;
		}

		byte[] MakeCode(byte[] data);

		byte[] ParseCode(byte[] code);
	}
}
