using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DeployLX.Licensing.v4
{
	internal sealed class Class30
	{
		private class Class5 : Class4
		{
			private bool bool_1;

			private string string_0;

			public Class5(string url, bool newWindow)
			{
				string_0 = url;
				bool_1 = newWindow;
			}

			protected override void vmethod_0()
			{
				try
				{
					string text = string_0.ToLower(CultureInfo.InvariantCulture);
					if (bool_1 && !text.StartsWith(Uri.UriSchemeMailto))
					{
						using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("HTTP\\shell\\open\\command", writable: false))
						{
							if (registryKey != null)
							{
								string text2 = registryKey.GetValue(null) as string;
								if (text2 != null)
								{
									text2 = ((text2.IndexOf("%1") <= -1) ? (text2 + " " + string_0) : text2.Replace("%1", string_0));
									string arguments = null;
									if (text2.StartsWith("\""))
									{
										int num = text2.IndexOf('"', 1);
										if (num > -1)
										{
											arguments = text2.Substring(num + 1).Trim();
											text2 = text2.Substring(1, num - 1);
										}
									}
									else
									{
										int num2 = text2.IndexOf(' ');
										if (num2 > -1)
										{
											arguments = text2.Substring(num2 + 1);
											text2 = text2.Substring(0, num2);
										}
									}
									Process.Start(text2, arguments);
									return;
								}
							}
						}
					}
					Process.Start(string_0);
				}
				catch (Exception)
				{
					Process.Start(string_0);
				}
			}
		}

		private class Class6 : Class4
		{
			private object object_0;

			private string string_0;

			public Class6(string format, object data)
			{
				string_0 = format;
				object_0 = data;
			}

			protected override void vmethod_0()
			{
				IDataObject data = new DataObject(string_0, object_0);
				Clipboard.SetDataObject(data, copy: true);
			}
		}

		private class Class7 : Class4
		{
			public object object_0;

			private string string_0;

			public Class7(string format)
			{
				string_0 = format;
			}

			protected override void vmethod_0()
			{
				IDataObject dataObject = Clipboard.GetDataObject();
				if (dataObject != null)
				{
					object_0 = dataObject.GetData(string_0, autoConvert: true);
				}
			}
		}

		private const string string_0 = "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5";

		private const string string_1 = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";

		private const string string_2 = "7$,lsj*0mkyL._JV3b{Y;^HO~nSK8W@?[w`9F%RP(!qiC52DA&/4v:p)ZcU-6T|Med§GN=g'hoE}+]zQBft¶ax#rI1X";

		private const string string_3 = "0123456789ABCDEF";

		private static readonly byte[] byte_0;

		private static readonly int[] int_0;

		static Class30()
		{
			int_0 = new int[256]
			{
				0,
				49345,
				49537,
				320,
				49921,
				960,
				640,
				49729,
				50689,
				1728,
				1920,
				51009,
				1280,
				50625,
				50305,
				1088,
				52225,
				3264,
				3456,
				52545,
				3840,
				53185,
				52865,
				3648,
				2560,
				51905,
				52097,
				2880,
				51457,
				2496,
				2176,
				51265,
				55297,
				6336,
				6528,
				55617,
				6912,
				56257,
				55937,
				6720,
				7680,
				57025,
				57217,
				8000,
				56577,
				7616,
				7296,
				56385,
				5120,
				54465,
				54657,
				5440,
				55041,
				6080,
				5760,
				54849,
				53761,
				4800,
				4992,
				54081,
				4352,
				53697,
				53377,
				4160,
				61441,
				12480,
				12672,
				61761,
				13056,
				62401,
				62081,
				12864,
				13824,
				63169,
				63361,
				14144,
				62721,
				13760,
				13440,
				62529,
				15360,
				64705,
				64897,
				15680,
				65281,
				16320,
				16000,
				65089,
				64001,
				15040,
				15232,
				64321,
				14592,
				63937,
				63617,
				14400,
				10240,
				59585,
				59777,
				10560,
				60161,
				11200,
				10880,
				59969,
				60929,
				11968,
				12160,
				61249,
				11520,
				60865,
				60545,
				11328,
				58369,
				9408,
				9600,
				58689,
				9984,
				59329,
				59009,
				9792,
				8704,
				58049,
				58241,
				9024,
				57601,
				8640,
				8320,
				57409,
				40961,
				24768,
				24960,
				41281,
				25344,
				41921,
				41601,
				25152,
				26112,
				42689,
				42881,
				26432,
				42241,
				26048,
				25728,
				42049,
				27648,
				44225,
				44417,
				27968,
				44801,
				28608,
				28288,
				44609,
				43521,
				27328,
				27520,
				43841,
				26880,
				43457,
				43137,
				26688,
				30720,
				47297,
				47489,
				31040,
				47873,
				31680,
				31360,
				47681,
				48641,
				32448,
				32640,
				48961,
				32000,
				48577,
				48257,
				31808,
				46081,
				29888,
				30080,
				46401,
				30464,
				47041,
				46721,
				30272,
				29184,
				45761,
				45953,
				29504,
				45313,
				29120,
				28800,
				45121,
				20480,
				37057,
				37249,
				20800,
				37633,
				21440,
				21120,
				37441,
				38401,
				22208,
				22400,
				38721,
				21760,
				38337,
				38017,
				21568,
				39937,
				23744,
				23936,
				40257,
				24320,
				40897,
				40577,
				24128,
				23040,
				39617,
				39809,
				23360,
				39169,
				22976,
				22656,
				38977,
				34817,
				18624,
				18816,
				35137,
				19200,
				35777,
				35457,
				19008,
				19968,
				36545,
				36737,
				20288,
				36097,
				19904,
				19584,
				35905,
				17408,
				33985,
				34177,
				17728,
				34561,
				18368,
				18048,
				34369,
				33281,
				17088,
				17280,
				33601,
				16640,
				33217,
				32897,
				16448
			};
			byte_0 = new byte[3]
			{
				1,
				0,
				1
			};
		}

		private Class30()
		{
		}

		public static string smethod_0(byte[] byte_1, string string_4, int int_1, int int_2, int int_3, bool bool_0)
		{
			if (byte_1 == null)
			{
				return null;
			}
			if (string_4 == null)
			{
				string_4 = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";
			}
			StringBuilder stringBuilder = new StringBuilder(byte_1.Length);
			byte[] array = new byte[int_3];
			Buffer.BlockCopy(byte_1, int_2, array, 0, int_3);
			if (int_1 < 0)
			{
				int_1 = string_4.Length;
			}
			int num = 0;
			int num2 = 0;
			do
			{
				bool flag = true;
				int num3 = 0;
				num = 0;
				for (int i = num2; i < int_3; i++)
				{
					num3 <<= 8;
					num3 |= array[i];
					array[i] = (byte)(num3 / int_1);
					num3 %= int_1;
					num |= array[i];
				}
				if (array[num2] == 0)
				{
					num2++;
				}
				stringBuilder.Append(string_4[num3]);
			}
			while (num > 0);
			if (bool_0)
			{
				int num4 = smethod_6(array.Length, string_4, int_1);
				while (stringBuilder.Length < num4)
				{
					stringBuilder.Append(string_4[0]);
				}
			}
			char[] array2 = stringBuilder.ToString().ToCharArray();
			Array.Reverse(array2);
			return new string(array2);
		}

		public static string smethod_1(byte[] byte_1, string string_4, int int_1)
		{
			if (byte_1 == null)
			{
				return null;
			}
			return smethod_0(byte_1, string_4, int_1, 0, byte_1.Length, bool_0: false);
		}

		public static int smethod_10(string string_4)
		{
			uint num = 5381u;
			int length = string_4.Length;
			for (int i = 0; i < length; i++)
			{
				num = (((num << 5) + num) ^ string_4[i]);
			}
			return (int)num;
		}

		public static int smethod_11(string string_4)
		{
			return smethod_13(Encoding.UTF8.GetBytes((string_4 == null) ? "" : string_4));
		}

		public static int smethod_12(byte[] byte_1, int int_1, int int_2)
		{
			if (int_1 > byte_1.Length)
			{
				throw new ArgumentOutOfRangeException("offset", int_1, null);
			}
			if (int_1 + int_2 > byte_1.Length)
			{
				throw new ArgumentOutOfRangeException("length", int_2, null);
			}
			int num = 0;
			for (int i = int_1; i < int_1 + int_2; i++)
			{
				num = ((num >> 8) ^ int_0[(num & 0xFF) ^ byte_1[i]]);
			}
			return num & 0xFFFF;
		}

		public static int smethod_13(byte[] byte_1)
		{
			return smethod_12(byte_1, 0, byte_1.Length);
		}

		public static bool smethod_16(string string_4, bool bool_0)
		{
			if (string_4 == null || string_4.Length == 0)
			{
				return true;
			}
			if (Regex.IsMatch(string_4, "^(((((((http://)|(https://)|(ftp://)|(asmres://)|(licres://))((([a-zA-Z0-9\\*\\?][\\-_a-zA-Z0-9\\*\\?]{0,25}\\.)*[a-zA-Z0-9\\*\\?][\\-_a-zA-Z0-9\\*\\?]{0,25}(\\.[a-zA-Z0-9\\*\\?]{2,5})?)|((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))(\\:[0-9]{1,5})?(~?/([\\.\\-_a-zA-Z0-9~%={} ]*))*((/?[\\.\\-_a-zA-Z0-9~%\\?&#={}]* ))?(\\?.*)?))|(file://((([A-Za-z]:\\\\)|(\\\\\\\\[A-Za-z0-9]{0,16})\\\\)?(([A-Za-z0-9\\.\\-_\\[\\]\\(\\)\\$ {}/]*)[\\\\/])*([A-Za-z0-9\\.\\-_\\[\\]\\(\\)\\$ {}/]*)))))\\|)*((((((http://)|(https://)|(ftp://)|(asmres://)|(licres://))((([a-zA-Z0-9\\*\\?][\\-_a-zA-Z0-9\\*\\?]{0,25}\\.)*[a-zA-Z0-9\\*\\?][\\-_a-zA-Z0-9\\*\\?]{0,25}(\\.[a-zA-Z0-9\\*\\?]{2,5})?)|((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))(\\:[0-9]{1,5})?(~?/([\\.\\-_a-zA-Z0-9~%={} ]*))*((/?[\\.\\-_a-zA-Z0-9~%\\?&#={}]* ))?(\\?.*)?))|(file://((([A-Za-z]:\\\\)|(\\\\\\\\[A-Za-z0-9]{0,16})\\\\)?(([A-Za-z0-9\\.\\-_\\[\\]\\(\\)\\$ {}/]*)[\\\\/])*([A-Za-z0-9\\.\\-_\\[\\]\\(\\)\\$ {}/]*)))))$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant))
			{
				return true;
			}
			if (bool_0)
			{
				throw new Exception("E_InvalidUrl");
			}
			return false;
		}

		public static void smethod_17(string string_4, bool bool_0)
		{
			new Class5(string_4, bool_0).method_0();
		}

		public static void smethod_18(string string_4, object object_0)
		{
			new Class6(string_4, object_0).method_0();
		}

		public static object smethod_19(string string_4)
		{
			Class7 @class = new Class7(string_4);
			@class.method_1();
			return @class.object_0;
		}

		public static string smethod_2(byte[] byte_1, string string_4)
		{
			return smethod_1(byte_1, string_4, -1);
		}

		public static string smethod_20(string string_4)
		{
			StringBuilder stringBuilder = new StringBuilder(string_4.Length);
			foreach (char c in string_4)
			{
				char c2 = c;
				if (c >= 'a' && c <= 'z')
				{
					c2 = (char)(c2 - 97);
					c2 = (char)(c2 + 13);
					c2 = (char)((int)c2 % 26);
					c2 = (char)(c2 + 97);
				}
				else if (c >= 'A' && c <= 'Z')
				{
					c2 = (char)(c2 - 65);
					c2 = (char)(c2 + 13);
					c2 = (char)((int)c2 % 26);
					c2 = (char)(c2 + 65);
				}
				stringBuilder.Append(c2);
			}
			return stringBuilder.ToString();
		}

		public static object smethod_21(byte[] byte_1, object secureLicenseContext_0)
		{
			RSAParameters rSAParameters = default(RSAParameters);
			rSAParameters.Modulus = byte_1;
			rSAParameters.Exponent = byte_0;
			RSAParameters rsaparameters_ = rSAParameters;
			CspParameters cspParameters = new CspParameters(1, "Microsoft Enhanced Cryptographic Provider v1.0", null);
			try
			{
				return smethod_22(ref rsaparameters_, cspParameters, bool_0: true, secureLicenseContext_0);
			}
			catch
			{
				cspParameters.ProviderName = null;
				object obj = smethod_22(ref rsaparameters_, cspParameters, bool_0: false, secureLicenseContext_0);
				if (obj == null)
				{
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
					cspParameters.ProviderName = "Microsoft Enhanced Cryptographic Provider v1.0";
					obj = smethod_22(ref rsaparameters_, cspParameters, bool_0: false, secureLicenseContext_0);
					if (obj != null)
					{
						return obj;
					}
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
					cspParameters.ProviderName = null;
					obj = smethod_22(ref rsaparameters_, cspParameters, bool_0: false, secureLicenseContext_0);
					if (obj != null)
					{
						return obj;
					}
					cspParameters.Flags = CspProviderFlags.NoFlags;
					cspParameters.KeyContainerName = "DLX_v3";
					cspParameters.ProviderName = null;
					obj = smethod_22(ref rsaparameters_, cspParameters, bool_0: false, secureLicenseContext_0);
					if (obj != null)
					{
						return obj;
					}
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
					cspParameters.KeyContainerName = "DLX_v3";
					obj = smethod_22(ref rsaparameters_, cspParameters, bool_0: false, secureLicenseContext_0);
					if (obj == null)
					{
						throw new Exception("E_RSADenied");
					}
				}
				return obj;
			}
		}

		private static object smethod_22(ref RSAParameters rsaparameters_0, CspParameters cspParameters_0, bool bool_0, object secureLicenseContext_0)
		{
			try
			{
				RSACryptoServiceProvider rSACryptoServiceProvider = rSACryptoServiceProvider = new RSACryptoServiceProvider(384, cspParameters_0);
				try
				{
					rSACryptoServiceProvider.PersistKeyInCsp = false;
				}
				catch
				{
				}
				rSACryptoServiceProvider.ImportParameters(rsaparameters_0);
				return rSACryptoServiceProvider;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				if (bool_0)
				{
					throw;
				}
			}
			return null;
		}

		public static string smethod_3(byte[] byte_1)
		{
			return smethod_1(byte_1, null, -1);
		}

		public static byte[] smethod_4(string string_4, string string_5, int int_1)
		{
			if (string_4 == null)
			{
				return null;
			}
			if (string_5 == null)
			{
				string_5 = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";
			}
			if (int_1 < 0)
			{
				int_1 = string_5.Length;
			}
			int[] array = new int[256];
			for (int i = 0; i < int_1; i++)
			{
				array[string_5[i]] = i;
			}
			byte[] array2 = new byte[string_4.Length];
			for (int j = 0; j < string_4.Length; j++)
			{
				int num = array[string_4[j]];
				int num2 = 0;
				for (int num3 = array2.Length - 1; num3 >= 0; num3--)
				{
					num2 += array2[num3] * int_1;
					array2[num3] = (byte)num2;
					num2 >>= 8;
				}
				num2 = num;
				for (int num4 = array2.Length - 1; num4 >= 0; num4--)
				{
					num2 += array2[num4];
					array2[num4] = (byte)num2;
					num2 >>= 8;
				}
			}
			double num5 = Math.Floor((double)string_4.Length * Math.Log10(int_1)) / Math.Log10(2.0);
			int k;
			for (k = (int)Math.Round(num5 / 8.0); array2.Length - k > 0 && array2[array2.Length - k - 1] != 0; k++)
			{
			}
			byte[] array3 = new byte[k];
			Buffer.BlockCopy(array2, array2.Length - k, array3, 0, array3.Length);
			return array3;
		}

		public static byte[] smethod_5(string string_4, string string_5)
		{
			return smethod_4(string_4, string_5, -1);
		}

		public static int smethod_6(int int_1, string string_4, int int_2)
		{
			return smethod_7(int_1 * 8, string_4, int_2);
		}

		public static int smethod_7(int int_1, string string_4, int int_2)
		{
			if (int_2 < 0)
			{
				int_2 = string_4.Length;
			}
			return (int)Math.Ceiling((double)int_1 * Math.Log10(2.0) / Math.Log10(int_2));
		}

		internal static byte[] smethod_8(byte[] byte_1, int int_1, int int_2)
		{
			byte[] array = new byte[byte_1.Length];
			int num = (int_2 != int.MaxValue) ? (int_1 + int_2) : int_2;
			if (num < 0)
			{
				num = int.MaxValue;
			}
			if (int_1 > 0)
			{
				Buffer.BlockCopy(byte_1, 0, array, 0, int_1);
			}
			int i;
			for (i = int_1; i < num && i + 4 <= array.Length; i += 4)
			{
				array[i] = (byte)((byte_1[i + 1] >> 4) | (byte_1[i + 3] << 4));
				array[i + 1] = (byte)((byte_1[i + 2] >> 4) | (byte_1[i] << 4));
				array[i + 2] = (byte)((byte_1[i + 3] >> 4) | (byte_1[i + 1] << 4));
				array[i + 3] = (byte)((byte_1[i] >> 4) | (byte_1[i + 2] << 4));
			}
			if (i < byte_1.Length)
			{
				Buffer.BlockCopy(byte_1, i, array, i, byte_1.Length - i);
			}
			return array;
		}

		internal static byte[] smethod_9(byte[] byte_1)
		{
			return smethod_8(byte_1, 0, byte_1.Length);
		}
	}
}
