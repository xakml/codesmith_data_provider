using DeployLXLicensing;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DeployLXKeyGenerator
{
	public class LicenseKeyGen
	{
		private class SerialNumberAlgo : ILicenseCodeSupport
		{
			private LicenseKeyGen licenseKey_0;

			public string CharacterSet => null;

			public SerialNumberAlgo(LicenseKeyGen key)
			{
				licenseKey_0 = key;
			}

			public byte[] MakeCode(byte[] data)
			{
				if (ReadLicenseKey.keySN == null || ReadLicenseKey.keySN.Length == 0)
				{
					return data;
				}
				byte[] array = new byte[2];
				RandomNumberGenerator.Create().GetBytes(array);
				byte[] array2 = new byte[data.Length + 3];
				Array.Copy(data, 0, array2, 1, data.Length);
				int num = 0;
				for (int i = 0; i < 10; i++)
				{
					if (num == array.Length)
					{
						num = 0;
					}
					for (int j = 0; j < data.Length; j++)
					{
						array2[1 + j] = (byte)(array2[1 + j] ^ array[num++]);
						if (num == array.Length)
						{
							num = 0;
						}
					}
					if (data.Length % array.Length == 0 && i != 9)
					{
						num++;
					}
				}
				array2[0] = array[1];
				array2[array2.Length - 2] = (byte)(num - 1);
				array2[array2.Length - 1] = array[0];
				for (int k = 0; k < array2.Length; k++)
				{
					array2[k] = (byte)(array2[k] ^ ReadLicenseKey.keySN[k % (ReadLicenseKey.keySN.Length - 1)]);
				}
				return array2;
			}

			public byte[] ParseCode(byte[] code)
			{
				return code;
			}
		}

		private class ActivationCodeAlgo : ILicenseCodeSupport
		{
			private LicenseKeyGen licenseKey_0;

			public string CharacterSet => null;

			public ActivationCodeAlgo(LicenseKeyGen key)
			{
				licenseKey_0 = key;
			}

			public byte[] MakeCode(byte[] data)
			{
				if (ReadLicenseKey.keyA == null || ReadLicenseKey.keyA.Length == 0)
				{
					return data;
				}
				using (Rijndael rijndael = new RijndaelManaged())
				{
					rijndael.Key = ReadLicenseKey.keyA;
					rijndael.IV = new byte[16];
					byte[] array = rijndael.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
					rijndael.IV = new byte[16];
					rijndael.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
					return array;
				}
			}

			public byte[] ParseCode(byte[] code)
			{
				return code;
			}
		}

		private class SimpleAlgo : ILicenseCodeSupport
		{
			private LicenseKeyGen licenseKey_0;

			public string CharacterSet => "0123456789";

			public SimpleAlgo(LicenseKeyGen key)
			{
				licenseKey_0 = key;
			}

			public byte[] MakeCode(byte[] data)
			{
				return data;
			}

			public byte[] ParseCode(byte[] code)
			{
				return code;
			}
		}

		private class BasicAlgo : ILicenseCodeSupport
		{
			private LicenseKeyGen licenseKey_0;

			public string CharacterSet => null;

			public BasicAlgo(LicenseKeyGen key)
			{
				licenseKey_0 = key;
			}

			public byte[] MakeCode(byte[] data)
			{
				return data;
			}

			public byte[] ParseCode(byte[] code)
			{
				return MakeCode(code);
			}
		}

		private class AdvancedAlgo : ILicenseCodeSupport
		{
			private LicenseKeyGen licenseKey_0;

			public string CharacterSet => null;

			public AdvancedAlgo(LicenseKeyGen key)
			{
				licenseKey_0 = key;
			}

			public byte[] MakeCode(byte[] data)
			{
				if (ReadLicenseKey.keyAd == null || ReadLicenseKey.keyAd.Length == 0)
				{
					return data;
				}
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Rijndael rijndael = Rijndael.Create())
					{
						byte[] keyAd = ReadLicenseKey.keyAd;
						rijndael.Key = ReadLicenseKey.keyAd;
						rijndael.IV = new byte[16];
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cryptoStream.Write(data, 0, data.Length);
							cryptoStream.Flush();
							cryptoStream.FlushFinalBlock();
							memoryStream.Flush();
							rijndael.Clear();
							cryptoStream.Clear();
						}
						result = memoryStream.ToArray();
					}
				}
				return result;
			}

			public byte[] ParseCode(byte[] code)
			{
				return code;
			}
		}

		private static Hashtable hashtable_0;

		public LicenseKeyGen()
		{
			hashtable_0 = new Hashtable();
		}

		public string MakeSerialNumber(string prefix, int seed, SerialNumberFlags flags, int extendLimitOrdinal1, int extendLimitValue1, int extendLimitOrdinal2, int extendLimitValue2, int[] groupSizes, string characterSet, CodeAlgorithm algorithm)
		{
			if (extendLimitOrdinal2 != -1 && extendLimitOrdinal1 == -1)
			{
				throw new Exception("E_FirstIndexMustBeUsedBeforeSecond");
			}
			if (extendLimitOrdinal1 > 31)
			{
				throw new Exception("extendLimitOrdinal1 out of range!");
			}
			if (extendLimitOrdinal2 > 31)
			{
				throw new Exception("extendLimitOrdinal2 out of range!");
			}
			if (extendLimitValue1 > 134217727)
			{
				throw new Exception("extendLimitValue1 out of range!");
			}
			if (extendLimitValue2 > 134217727)
			{
				throw new Exception("extendLimitValue2 out of range!");
			}
			if (algorithm == CodeAlgorithm.NotSet)
			{
				algorithm = CodeAlgorithm.SerialNumber;
			}
			if (seed > 16777215)
			{
				throw new Exception("E_MaxSeed out of range!");
			}
			if (extendLimitOrdinal1 != -1 && extendLimitOrdinal2 == -1)
			{
				extendLimitOrdinal2 = 0;
				extendLimitValue2 = 0;
			}
			byte[] array = new byte[1 + ((extendLimitOrdinal1 != -1) ? 4 : 0) + ((extendLimitOrdinal2 != -1) ? 4 : 0)];
			array[0] = (byte)flags;
			if (extendLimitOrdinal1 != -1)
			{
				Array.Copy(BitConverter.GetBytes(extendLimitValue1), 0, array, 1, 4);
				array[4] = (byte)(array[4] | (byte)(extendLimitOrdinal1 << 3));
			}
			if (extendLimitOrdinal2 != -1)
			{
				Array.Copy(BitConverter.GetBytes(extendLimitValue2), 0, array, 5, 4);
				array[8] = (byte)(array[8] | (byte)(extendLimitOrdinal2 << 3));
			}
			return MakeSerialNumber(prefix, seed, array, groupSizes, characterSet, algorithm);
		}

		public string MakeSerialNumber(string prefix, int seed, byte[] data, int[] groupSizes, string characterSet, CodeAlgorithm algorithm)
		{
			if (algorithm == CodeAlgorithm.NotSet)
			{
				algorithm = CodeAlgorithm.SerialNumber;
			}
			if (seed > 16777215)
			{
				throw new Exception("E_MaxSeed out of range!");
			}
			byte[] array = (data == null) ? new byte[4] : new byte[4 + data.Length];
			Array.Copy(BitConverter.GetBytes(seed), array, 4);
			if (data != null)
			{
				Array.Copy(data, 0, array, 4, data.Length);
			}
			array[3] = (byte)Utils.Crc16(array, 0, 3);
			return GenerateInternal(prefix, null, array, groupSizes, characterSet, algorithm);
		}

		public string MakeActivationCode(string prefix, string serialNumber, string hash, int refid, DateTime expires, string characterSet, CodeAlgorithm algorithm)
		{
			if (algorithm == CodeAlgorithm.NotSet)
			{
				algorithm = CodeAlgorithm.ActivationCode;
			}
			if (hash == null)
			{
				throw new Exception("hash is null");
			}
			if (!MachineProfile.IsHashValid(hash))
			{
				throw new ArgumentException("Invalid hash.", "hash", null);
			}
			if (expires == DateTime.MinValue)
			{
				expires = Utils.ModExpireDate(b: false).AddDays(7.0);
			}
			int value = (expires.Year - 2000) | (expires.Month << 10) | (expires.Day << 14);
			byte[] array = new byte[10];
			hash = hash.ToUpper();
			Buffer.BlockCopy(BitConverter.GetBytes(Utils.Crc16(Encoding.UTF8.GetBytes(hash))), 0, array, 0, 2);
			array[2] = (byte)(array[0] ^ (255 - refid));
			array[3] = (byte)(array[1] ^ (255 - refid));
			array[4] = (byte)refid;
			Array.Copy(BitConverter.GetBytes(value), 0, array, 5, 4);
			array[array.Length - 1] = (byte)Utils.Crc16(array, 0, array.Length - 2);
			return GenerateInternal(prefix, serialNumber, array, null, characterSet, algorithm);
		}

		private static void Xor(byte[] A, byte[] B)
		{
			int num = 0;
			for (int i = 0; i < A.Length; i++)
			{
				A[i] = (byte)(A[i] ^ B[num++]);
				if (num == B.Length)
				{
					num = 0;
				}
			}
		}

		private static int smethod_14(int int_0, int int_1, out int int_2, out int int_3)
		{
			int_2 = int_0 / int_1;
			for (int_3 = int_0 % int_1; int_3 > int_2; int_3 = int_0 % int_1)
			{
				int_1--;
				int_2 = int_0 / int_1;
			}
			return int_1;
		}

		private string GenerateInternal(string prefix, string SerialNumber, byte[] Data, int[] GroupSizes, string CharacterSet, CodeAlgorithm codeAlgorithm)
		{
			ILicenseCodeSupport customCodeSupport = GetCustomCodeSupport(codeAlgorithm);
			if (customCodeSupport == null)
			{
				throw new Exception("E_UnknownAlgorithm");
			}
			if (CharacterSet == null)
			{
				CharacterSet = customCodeSupport.CharacterSet;
			}
			if (CharacterSet == null)
			{
				CharacterSet = "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5";
			}
			if (CharacterSet.IndexOf('-') > -1)
			{
				throw new ArgumentException("E_CharacterSetContainsDash");
			}
			if (Data.Length > 127)
			{
				throw new ArgumentException("E_MaximumDataExceeded");
			}
			if (SerialNumber != null)
			{
				SerialNumber = SerialNumber.ToUpper();
			}
			byte[] array = new byte[1 + Data.Length];
			Array.Copy(Data, array, Data.Length);
			byte b = (byte)Utils.Crc16(array, 0, array.Length - 1);
			array[array.Length - 1] = b;
			array = customCodeSupport.MakeCode(array);
			if (prefix != null)
			{
				prefix.Trim();
				if (prefix.Length == 0)
				{
					prefix = null;
				}
			}
			if (SerialNumber != null)
			{
				SerialNumber.Trim();
				if (SerialNumber.Length == 0)
				{
					SerialNumber = null;
				}
			}
			if (prefix != null)
			{
				Xor(array, Encoding.UTF8.GetBytes(prefix));
			}
			if (SerialNumber != null)
			{
				Xor(array, Encoding.UTF8.GetBytes(SerialNumber));
			}
			byte b2 = (byte)((byte)Data.Length | 0x80);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(array[i] ^ b2);
			}
			byte[] array2 = new byte[array.Length + 1];
			Array.Copy(array, 0, array2, 1, array.Length);
			array2[0] = b2;
			array = array2;
			char[] array3 = (CharacterSet[(int)codeAlgorithm] + Utils.ByteToString(array, CharacterSet, -1, 0, array.Length, padToLongest: true) + CharacterSet[Utils.Crc16(array) % (CharacterSet.Length - 1)]).ToCharArray();
			Array.Reverse(array3, 0, (int)char.ToLower(array3[array3.Length - 1]) % (array3.Length - 2));
			string text = new string(array3);
			if (prefix != null && prefix.IndexOf('-') == -1)
			{
				text = prefix + text;
			}
			if (GroupSizes == null)
			{
				int int_ = 8;
				if (text.Length <= 18)
				{
					int_ = 4;
				}
				else if (text.Length <= 35)
				{
					int_ = 5;
				}
				else if (text.Length <= 48)
				{
					int_ = 6;
				}
				int_ = smethod_14(text.Length, int_, out int int_2, out int int_3);
				GroupSizes = new int[int_2];
				int_2 = text.Length;
				int num = prefix?.LastIndexOf('-') ?? (-1);
				if (num != -1)
				{
					int num2 = prefix.Length - num - 1;
					if (num2 > 0 && num2 < int_)
					{
						int_ = smethod_14(text.Length + num2, int_, out int_2, out int_3);
						int_2 = text.Length + num2;
						GroupSizes[0] = int_ - num2;
						int_2 -= num;
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				else
				{
					num++;
				}
				for (; num < GroupSizes.Length; num++)
				{
					GroupSizes[num] = int_ + ((int_3 > 0) ? 1 : 0);
					int_2 -= int_ + ((int_3 > 0) ? 1 : 0);
					int_3--;
					if (int_3 < 0)
					{
						int_3 = 0;
					}
				}
			}
			if (GroupSizes.Length <= 0)
			{
				if (prefix != null && prefix.IndexOf('-') == -1)
				{
					return text;
				}
				return string.Format("{0}{2}", prefix, text);
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			int num3 = 0;
			for (int j = 0; j < GroupSizes.Length; j++)
			{
				num3 += GroupSizes[j];
				if (num3 >= stringBuilder.Length)
				{
					break;
				}
				stringBuilder.Insert(num3, '-');
				num3++;
			}
			if (prefix != null && prefix.IndexOf('-') != -1)
			{
				stringBuilder.Insert(0, prefix);
			}
			return stringBuilder.ToString();
		}

		public ILicenseCodeSupport GetCustomCodeSupport(CodeAlgorithm algorithm)
		{
			lock (typeof(LicenseKeyGen))
			{
				ILicenseCodeSupport licenseCodeSupport = hashtable_0[algorithm] as ILicenseCodeSupport;
				if (licenseCodeSupport == null)
				{
					switch (algorithm)
					{
					case CodeAlgorithm.SerialNumber:
						return new SerialNumberAlgo(this);
					case CodeAlgorithm.ActivationCode:
						return new ActivationCodeAlgo(this);
					case CodeAlgorithm.Simple:
						return new SimpleAlgo(this);
					case CodeAlgorithm.Basic:
						return new BasicAlgo(this);
					case CodeAlgorithm.Advanced:
						return new AdvancedAlgo(this);
					}
				}
				return licenseCodeSupport;
			}
		}
	}
}
