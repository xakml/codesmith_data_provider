using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DeployLXKeyGenerator
{
	internal sealed class Class30
	{
		private static byte[] byte_0;

		private static int[] int_0;

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

		public static byte[] smethod_5(string string_4, string string_5)
		{
			return smethod_4(string_4, string_5, -1);
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

		private static object smethod_22(ref RSAParameters rsaparameters_0, CspParameters cspParameters_0, bool bool_0, ReadLicenseKey secureLicenseContext_0)
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
			}
			return null;
		}

		public static object smethod_21(byte[] byte_1, ReadLicenseKey secureLicenseContext_0)
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

		internal static byte[] smethod_9(byte[] byte_1)
		{
			return smethod_8(byte_1, 0, byte_1.Length);
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
	}
}
