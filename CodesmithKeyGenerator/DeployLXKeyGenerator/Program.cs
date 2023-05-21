using System;
using System.Windows.Forms;

namespace DeployLXKeyGenerator
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			ReadLicenseKey.keySN = null;
			ReadLicenseKey.keyA = null;
			ReadLicenseKey.keyAd = null;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new MainForm());
		}
	}
}
