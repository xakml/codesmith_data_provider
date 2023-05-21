using DeployLXKeyGenerator;
using System;
using System.Text;

namespace DeployLXLicensing
{
	public sealed class MachineProfile
	{
		public static bool IsHashValid(string hash)
		{
			if (hash == null || hash.Length < 5)
			{
				return false;
			}
			try
			{
				byte[] array = Utils.StringToByte(ParseHash(hash).ToString(), "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5");
				byte b = array[1];
				if (((b >> 2) & 7) >= 3 && ((b >> 2) & 7) <= 4)
				{
					Array.Reverse(array);
					int num = 5381;
					for (int i = 0; i < array.Length - 3; i++)
					{
						num = (((num << 5) + num) ^ array[i]);
					}
					if (array[array.Length - 3] != (byte)num)
					{
						return false;
					}
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		private static StringBuilder ParseHash(string string_1)
		{
			StringBuilder stringBuilder = new StringBuilder(string_1);
			if (string_1.Length >= 5)
			{
				stringBuilder.Replace("-", "");
				char[] array = stringBuilder.ToString().ToCharArray();
				int num = (((int)array[0] % (array.Length - 1)) & 7) | 1;
				Array.Reverse(array, num, array.Length - num);
				num = (3 | ((int)array[array.Length - 1] % (array.Length - 2)));
				if (num >= array.Length)
				{
					num = array.Length - 1;
				}
				Array.Reverse(array, 0, num);
				stringBuilder.Length = 0;
				stringBuilder.Append(array);
			}
			return stringBuilder;
		}
	}
}
