using Microsoft.Win32;
using System;
using System.Globalization;
using System.Text;

namespace DeployLXKeyGenerator
{
	public class Utils
	{
		internal const string DefaultRadix = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";

		private static readonly int[] _crctable;

		static Utils()
		{
			_crctable = new int[256]
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
		}

		public static RegistryKey OpenRegistryPath(string path, bool writable, bool createIfDoesntExist)
		{
			string[] array = path.Split('\\');
			if (array.Length == 0)
			{
				goto IL_00c5;
			}
			RegistryKey registryKey;
			if (array.Length == 1)
			{
				registryKey = Registry.LocalMachine;
			}
			else
			{
				switch (array[0].ToUpper(CultureInfo.InvariantCulture))
				{
				case "HKLM":
				case "HKEY_LOCAL_MACHINE":
					break;
				case "HKCU":
				case "HKEY_CURRENT_USER":
					goto IL_00b4;
				case "HKCR":
				case "HKEY_CLASSES_ROOT":
					goto IL_00bc;
				default:
					goto IL_00c5;
				}
				registryKey = Registry.LocalMachine;
			}
			goto IL_00cd;
			IL_00b4:
			registryKey = Registry.CurrentUser;
			goto IL_00cd;
			IL_00c5:
			return null;
			IL_00cd:
			RegistryKey registryKey2 = registryKey;
			RegistryKey registryKey3 = registryKey;
			RegistryKey registryKey4 = null;
			RegistryKey registryKey5 = null;
			try
			{
				int num = array.Length - 1;
				while ((array[num].Length == 0 || array[num][0] == '@') && num > 0)
				{
					num--;
				}
				if (num <= 0)
				{
					return null;
				}
				for (int i = 1; i <= num; i++)
				{
					if (i == num)
					{
						registryKey5 = registryKey2.OpenSubKey(array[i], writable);
						if (registryKey5 == null)
						{
							if (!createIfDoesntExist)
							{
								return null;
							}
							registryKey2.Close();
							registryKey2 = registryKey3.OpenSubKey(array[i - 1], writable: true);
							if (registryKey2 == null)
							{
								return null;
							}
							registryKey5 = registryKey2.CreateSubKey(array[i]);
							registryKey2.Close();
						}
					}
					else
					{
						registryKey4 = registryKey2.OpenSubKey(array[i], writable: false);
						if (registryKey4 == null)
						{
							if (!createIfDoesntExist)
							{
								return null;
							}
							registryKey2.Close();
							registryKey2 = registryKey3.OpenSubKey(array[i - 1], writable: true);
							if (registryKey2 == null)
							{
								return null;
							}
							registryKey4 = registryKey2.CreateSubKey(array[i]);
						}
						if (registryKey3 != registryKey)
						{
							registryKey3.Close();
						}
						registryKey3 = registryKey2;
						registryKey2 = registryKey4;
					}
				}
			}
			finally
			{
				if (registryKey2 != registryKey && registryKey2 != null && registryKey2 != registryKey5)
				{
					registryKey2.Close();
				}
				if (registryKey4 != registryKey && registryKey4 != null && registryKey4 != registryKey5)
				{
					registryKey4.Close();
				}
				if (registryKey3 != registryKey && registryKey3 != null && registryKey3 != registryKey5)
				{
					registryKey3.Close();
				}
			}
			return registryKey5;
			IL_00bc:
			registryKey = Registry.ClassesRoot;
			goto IL_00cd;
		}

		public static int GetShortHashCode(string s)
		{
			return Crc16(Encoding.UTF8.GetBytes(s ?? ""));
		}

		public static int Crc16(byte[] data)
		{
			return Crc16(data, 0, data.Length);
		}

		public static int Crc16(byte[] data, int offset, int length)
		{
			int num = 0;
			for (int i = offset; i < offset + length; i++)
			{
				num = ((num >> 8) ^ _crctable[(num & 0xFF) ^ data[i]]);
			}
			return num & 0xFFFF;
		}

		public static string ByteToString(byte[] data, string characterSet, int radix)
		{
			return ByteToString(data, characterSet, radix, 0, data.Length, padToLongest: false);
		}

		public static string ByteToString(byte[] data, string characterSet, int radix, int offset, int count, bool padToLongest)
		{
			if (data == null)
			{
				return null;
			}
			if (characterSet == null)
			{
				characterSet = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";
			}
			StringBuilder stringBuilder = new StringBuilder(data.Length);
			byte[] array = new byte[count];
			Buffer.BlockCopy(data, offset, array, 0, count);
			if (radix < 0)
			{
				radix = characterSet.Length;
			}
			int num = 0;
			int num3;
			do
			{
				int num2 = 0;
				num3 = 0;
				for (int i = num; i < count; i++)
				{
					num2 <<= 8;
					num2 |= array[i];
					array[i] = (byte)(num2 / radix);
					num2 %= radix;
					num3 |= array[i];
				}
				if (array[num] == 0)
				{
					num++;
				}
				stringBuilder.Append(characterSet[num2]);
			}
			while (num3 > 0);
			if (padToLongest)
			{
				int num4 = CalculateLongestStringForByte(array.Length, characterSet, radix);
				while (stringBuilder.Length < num4)
				{
					stringBuilder.Append(characterSet[0]);
				}
			}
			char[] array2 = stringBuilder.ToString().ToCharArray();
			Array.Reverse(array2);
			return new string(array2);
		}

		public static byte[] StringToByte(string input, string charset)
		{
			return StringToByte(input, charset, -1);
		}

		public static byte[] StringToByte(string input, string charset, int int_1)
		{
			if (input == null)
			{
				return null;
			}
			if (charset == null)
			{
				charset = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^*()_+-=[]{}|;:,.?/`~";
			}
			if (int_1 < 0)
			{
				int_1 = charset.Length;
			}
			int[] array = new int[256];
			for (int i = 0; i < int_1; i++)
			{
				array[charset[i]] = i;
			}
			byte[] array2 = new byte[input.Length];
			for (int j = 0; j < input.Length; j++)
			{
				int num = array[input[j]];
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
			double num5 = Math.Floor((double)input.Length * Math.Log10(int_1)) / Math.Log10(2.0);
			int k;
			for (k = (int)Math.Round(num5 / 8.0); array2.Length - k > 0 && array2[array2.Length - k - 1] != 0; k++)
			{
			}
			byte[] array3 = new byte[k];
			Buffer.BlockCopy(array2, array2.Length - k, array3, 0, array3.Length);
			return array3;
		}

		public static int CalculateLongestStringForByte(int dataLength, string characterSet, int radix)
		{
			return CalculateLongestStringForBit(dataLength * 8, characterSet, radix);
		}

		public static int CalculateLongestStringForBit(int msbPos, string characterSet, int radix)
		{
			if (radix < 0)
			{
				radix = characterSet.Length;
			}
			return (int)Math.Ceiling((double)msbPos * Math.Log10(2.0) / Math.Log10(radix));
		}

		public static DateTime ModExpireDate(bool b)
		{
			DateTime date = DateTime.UtcNow.Date;
			if (!b)
			{
				return date.AddTicks(863999990000L);
			}
			return date;
		}
	}
}
