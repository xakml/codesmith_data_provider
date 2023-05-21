using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DeployLXKeyGenerator
{
	public class ReadLicenseKey
	{
		private static Version version_0;

		private byte[] byte_0;

		private static byte[] byte_1;

		private byte[] byte_2;

		private byte[] byte_3;

		private byte[] byte_4;

		private byte[] byte_5;

		private byte[] byte_6;

		private byte[] byte_7;

		public static Version CurrentVersion;

		public static byte[] keySN;

		public static byte[] keyA;

		public static byte[] keyAd;

		public ReadLicenseKey()
		{
			byte_1 = new byte[3]
			{
				88,
				76,
				75
			};
			version_0 = new Version(3, 0);
			CurrentVersion = version_0;
		}

		private static void BufferBlockCopy(byte[] byte_8, byte[] byte_9, ref int int_4)
		{
			Buffer.BlockCopy(byte_8, int_4, byte_9, 0, byte_9.Length);
			int_4 += byte_9.Length;
		}

		private static byte smethod_1(byte[] byte_8, ref int int_4)
		{
			byte b = 0;
			int num = 0;
			while (num < 8)
			{
				if (int_4 % 4 == 3)
				{
					int_4++;
				}
				b = (byte)(b | (byte)((byte)(byte_8[int_4] & 1) << num));
				num++;
				int_4++;
			}
			return b;
		}

		private static int smethod_2(byte[] byte_8, byte[] byte_9, int int_4)
		{
			for (int i = 0; i < byte_8.Length; i++)
			{
				byte_8[i] = smethod_1(byte_9, ref int_4);
			}
			return int_4;
		}

		private static bool smethod_3(RSACryptoServiceProvider rsacryptoServiceProvider_0, byte[] byte_8, byte[] byte_9)
		{
			using (SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider())
			{
				byte[] rgbHash = sHA1CryptoServiceProvider.ComputeHash(byte_8);
				return rsacryptoServiceProvider_0.VerifyHash(rgbHash, (Environment.OSVersion.Version.Major >= 5) ? "SHA1" : "1.3.14.3.2.26", byte_9);
			}
		}

		private RSACryptoServiceProvider method_23(byte[] byte_8, bool bool_8)
		{
			try
			{
				if (bool_8)
				{
					using (SymmetricAlgorithm symmetricAlgorithm = Toolbox.GetAes(forceFips: false))
					{
						symmetricAlgorithm.IV = new byte[16];
						symmetricAlgorithm.Key = byte_6;
						byte_8 = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(byte_8, 0, byte_8.Length);
					}
				}
				return Class30.smethod_21(byte_8, this) as RSACryptoServiceProvider;
			}
			finally
			{
				if (bool_8)
				{
					Array.Clear(byte_8, 0, byte_8.Length);
				}
			}
		}

		public void Process(string Data)
		{
			byte[] array = Class30.smethod_9(Class30.smethod_5(Data, "7$,lsj*0mkyL._JV3b{Y;^HO~nSK8W@?[w`9F%RP(!qiC52DA&/4v:p)ZcU-6T|Med§GN=g'hoE}+]zQBft¶ax#rI1X"));
			int i;
			for (i = 0; i < byte_1.Length; i++)
			{
				if (array[i] != byte_1[i])
				{
					return;
				}
			}
			int num = BitConverter.ToInt32(array, i);
			i += 4;
			Version v = new Version(num >> 16, num & 0xFFFF);
			if (v == CurrentVersion)
			{
				byte[] array2 = new byte[128];
				BufferBlockCopy(array, array2, ref i);
				byte[] array4;
				using (SymmetricAlgorithm symmetricAlgorithm = Toolbox.GetAes(forceFips: false))
				{
					byte[] array3 = new byte[16];
					Buffer.BlockCopy(array2, 0, array3, 0, array3.Length);
					symmetricAlgorithm.IV = array3;
					array3 = new byte[32];
					Buffer.BlockCopy(array2, symmetricAlgorithm.IV.Length, array3, 0, array3.Length);
					symmetricAlgorithm.Key = array3;
					array3 = new byte[32];
					BufferBlockCopy(array, array3, ref i);
					array4 = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(array3, 0, array3.Length);
				}
				byte[] array5 = new byte[32];
				Buffer.BlockCopy(Images.byte_12, 128, array5, 0, array5.Length);
				byte[] array6 = new byte[BitConverter.ToInt32(array, i)];
				i += 4;
				BufferBlockCopy(array, array6, ref i);
				byte[] array7 = null;
				using (MemoryStream stream = new MemoryStream(Images.byte_0))
				{
					using (Bitmap bitmap = Image.FromStream(stream) as Bitmap)
					{
						BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
						byte[] array8 = new byte[bitmapData.Stride * bitmapData.Height];
						Marshal.Copy(bitmapData.Scan0, array8, 0, array8.Length);
						byte[] array9 = new byte[2];
						int int_ = smethod_2(array9, array8, 0);
						array7 = new byte[BitConverter.ToInt16(array9, 0)];
						smethod_2(array7, array8, int_);
						bitmap.UnlockBits(bitmapData);
					}
				}
				RSACryptoServiceProvider rsacryptoServiceProvider_ = method_23(array7, bool_8: false);
				try
				{
					if (!smethod_3(rsacryptoServiceProvider_, array6, array2))
					{
						Array.Clear(array6, 0, array6.Length);
						Array.Clear(array7, 0, array7.Length);
						throw new Exception("E_InvalidPublicKey");
					}
				}
				finally
				{
					try
					{
					}
					catch
					{
					}
				}
				using (SymmetricAlgorithm symmetricAlgorithm2 = Toolbox.GetAes(forceFips: false))
				{
					symmetricAlgorithm2.IV = array4;
					symmetricAlgorithm2.Key = array5;
					Array.Clear(array4, 0, array4.Length);
					Array.Clear(array5, 0, array5.Length);
					array6 = symmetricAlgorithm2.CreateDecryptor().TransformFinalBlock(array6, 0, array6.Length);
				}
				byte[] array10 = new byte[BitConverter.ToInt16(array6, 0)];
				i = array10.Length + 2;
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				try
				{
					byte[] array11 = new byte[8];
					BufferBlockCopy(array6, array11, ref i);
					dESCryptoServiceProvider.Key = array11;
					array11 = new byte[8];
					BufferBlockCopy(array6, array11, ref i);
					dESCryptoServiceProvider.IV = array11;
					array10 = dESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array6, 2, array10.Length);
					Array.Clear(array, 0, array.Length);
					Array.Clear(array6, 0, array6.Length);
					Array.Reverse(array10, 0, array10.Length);
					i = 32;
					byte_0 = new byte[16];
					BufferBlockCopy(array10, byte_0, ref i);
					byte_5 = new byte[32];
					BufferBlockCopy(array10, byte_5, ref i);
					byte_4 = new byte[32];
					BufferBlockCopy(array10, byte_4, ref i);
					byte_3 = new byte[32];
					BufferBlockCopy(array10, byte_3, ref i);
					i++;
					int num3 = array10[i++] << 3;
					byte_2 = new byte[num3];
					BufferBlockCopy(array10, byte_2, ref i);
					Array.Clear(array10, 0, array10.Length);
					RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
					byte_6 = new byte[32];
					rNGCryptoServiceProvider.GetBytes(byte_6);
					using (SymmetricAlgorithm symmetricAlgorithm3 = Toolbox.GetAes(forceFips: false))
					{
						symmetricAlgorithm3.IV = new byte[16];
						symmetricAlgorithm3.Key = byte_6;
						byte[] array12 = keyAd = byte_5;
						byte_5 = symmetricAlgorithm3.CreateEncryptor().TransformFinalBlock(byte_5, 0, byte_5.Length);
						array12 = (keySN = byte_4);
						byte_4 = symmetricAlgorithm3.CreateEncryptor().TransformFinalBlock(byte_4, 0, byte_4.Length);
						array12 = (keyA = byte_3);
						byte_3 = symmetricAlgorithm3.CreateEncryptor().TransformFinalBlock(byte_3, 0, byte_3.Length);
						array12 = byte_2;
						byte_2 = symmetricAlgorithm3.CreateEncryptor().TransformFinalBlock(byte_2, 0, byte_2.Length);
						byte_7 = symmetricAlgorithm3.CreateEncryptor().TransformFinalBlock(array7, 0, array7.Length);
					}
				}
				finally
				{
					dESCryptoServiceProvider.Clear();
				}
			}
		}
	}
}
