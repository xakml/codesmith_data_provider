using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DeployLXKeyGenerator
{
	public static class Toolbox
	{
		private static readonly ControlStyles controlStyles_0;

		private static Type type_0;

		static Toolbox()
		{
			controlStyles_0 = ((Environment.Version.Major >= 2) ? (ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer) : ControlStyles.DoubleBuffer);
			try
			{
			}
			catch
			{
			}
			try
			{
				type_0 = Assembly.Load("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetType("System.Security.Cryptography.AesCryptoServiceProvider");
			}
			catch (FileNotFoundException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			if (type_0 == null)
			{
				type_0 = typeof(RijndaelManaged);
			}
		}

		public static SymmetricAlgorithm GetAes(bool forceFips)
		{
			return Activator.CreateInstance(type_0) as SymmetricAlgorithm;
		}
	}
}
