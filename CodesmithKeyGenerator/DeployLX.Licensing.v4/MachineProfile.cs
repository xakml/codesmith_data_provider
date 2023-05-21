using Microsoft.Win32;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace DeployLX.Licensing.v4
{
	public sealed class MachineProfile
	{
		public struct Struct32
		{
			public string string_0;

			public string string_1;

			public string[] string_2;

			public string string_3;

			public string string_4;

			public string string_5;
		}

		private const int int_0 = 3;

		private const int int_1 = 4;

		private const int int_2 = 4;

		private const int int_3 = 3;

		private const int int_4 = 4;

		private static ArrayList arrayList_0;

		private static bool bool_0;

		private static readonly byte[] byte_0 = new byte[39]
		{
			28,
			0,
			0,
			0,
			83,
			67,
			83,
			73,
			68,
			73,
			83,
			75,
			16,
			39,
			0,
			0,
			1,
			5,
			27,
			0,
			0,
			0,
			0,
			0,
			17,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			236
		};

		private int int_5;

		private static int int_6;

		private static readonly int[] int_7 = new int[6]
		{
			416,
			268,
			8,
			400,
			404,
			648
		};

		private static readonly int[] int_8 = new int[6]
		{
			420,
			272,
			12,
			404,
			408,
			704
		};

		private static MachineProfile machineProfile_0;

		private static MachineProfile machineProfile_1 = GetDefaultProfile();

		private MachineProfileEntryCollection machineProfileEntryCollection_0;

		private string string_0;

		public MachineProfileEntryCollection HardwareList => machineProfileEntryCollection_0;

		public string Hash
		{
			get
			{
				if (string_0 == null)
				{
					string_0 = GetComparableHash(firstOnly: false, null);
				}
				return string_0;
			}
		}

		public static bool IsLaptop
		{
			get
			{
				if (int_6 == 0)
				{
					int_6 = ((smethod_0() && smethod_1()) ? 1 : (-1));
				}
				return int_6 == 1;
			}
		}

		public static MachineType MachineType
		{
			get
			{
				if (!IsLaptop)
				{
					return MachineType.Desktop;
				}
				return MachineType.Laptop;
			}
		}

		public static MachineProfile Profile
		{
			get
			{
				if (machineProfile_0 == null && machineProfile_0 == null)
				{
					machineProfile_0 = GetDefaultProfile();
				}
				return machineProfile_0;
			}
		}

		public static bool Use64BitCompatibleCpuid
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
				machineProfile_0 = null;
			}
		}

		public int Version
		{
			get
			{
				return int_5;
			}
			set
			{
				int_5 = value;
				string_0 = null;
			}
		}

		public MachineProfile()
		{
			int_5 = 3;
			machineProfileEntryCollection_0 = new MachineProfileEntryCollection();
			machineProfileEntryCollection_0.Changed += machineProfileEntryCollection_0_Changed;
		}

		internal MachineProfile(MachineProfileEntryCollection hardware)
		{
			int_5 = 3;
			machineProfileEntryCollection_0 = new MachineProfileEntryCollection();
			machineProfileEntryCollection_0 = hardware;
		}

		public static bool CheckIsDefault(MachineProfileEntryCollection profile)
		{
			if (profile.Count != machineProfile_1.machineProfileEntryCollection_0.Count)
			{
				return false;
			}
			foreach (MachineProfileEntry item in (IEnumerable)machineProfile_1.machineProfileEntryCollection_0)
			{
				bool flag = false;
				foreach (MachineProfileEntry item2 in (IEnumerable)profile)
				{
					if (item2.Type == item.Type)
					{
						if (item2.Weight != item.Weight)
						{
							return false;
						}
						if (item2.PartialMatchWeight != item.PartialMatchWeight)
						{
							return false;
						}
						if (item2.Interface1_0 != item.Interface1_0)
						{
							return false;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		public static int CompareHash(string hash, MachineProfile profile, bool fileMoved)
		{
			return CompareHash(hash, profile.GetComparableHash(firstOnly: false, null), fileMoved, profile.machineProfileEntryCollection_0);
		}

		public static int CompareHash(string hash, string comparedHash, bool fileMoved, MachineProfileEntryCollection hardwareList)
		{
			MachineProfileEntryType[] diffs;
			return CompareHash(hash, comparedHash, fileMoved, hardwareList, out diffs);
		}

		public static int CompareHash(string hash, string comparedHash, bool fileMoved, MachineProfileEntryCollection hardwareList, out MachineProfileEntryType[] diffs)
		{
			diffs = null;
			if (!IsHashValid(hash) || !IsHashValid(comparedHash))
			{
				return 10001;
			}
			byte[] byte_;
			ArrayList arrayList = smethod_4(hash, out byte_);
			ArrayList arrayList2 = smethod_4(comparedHash, out byte_);
			ArrayList arrayList3 = new ArrayList();
			int num = (((int[])arrayList[0])[0] >> 2) & 7;
			int num2 = (((int[])arrayList2[0])[0] >> 2) & 7;
			if (num > 4 || num < 3 || num2 > 4 || num2 < 3)
			{
				return 10002;
			}
			bool flag = (((int[])arrayList[0])[0] & 1) != 0;
			int num3 = 0;
			for (int i = 0; i < hardwareList.Count; i++)
			{
				int[] array = (arrayList.Count <= i + 1) ? new int[0] : (arrayList[i + 1] as int[]);
				int[] array2 = (arrayList2.Count <= i + 1) ? new int[0] : (arrayList2[i + 1] as int[]);
				bool flag2 = array.Length == 0 && array2.Length == 0;
				for (int j = 0; j < array.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						if (array[j] == array2[k])
						{
							flag2 = true;
							array2[k] = -1;
							array[j] = -1;
						}
					}
				}
				if (flag2)
				{
					bool flag3 = false;
					for (int l = 0; l < array.Length; l++)
					{
						if (array[l] != -1)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3 && !flag)
					{
						for (int m = 0; m < array2.Length; m++)
						{
							if (array2[m] != -1)
							{
								flag3 = true;
								break;
							}
						}
					}
					if (flag3)
					{
						num3 = ((!fileMoved) ? (num3 + hardwareList[i].PartialMatchWeight) : (num3 + Math.Max(hardwareList[i].FileMovedWeight, hardwareList[i].PartialMatchWeight)));
						arrayList3.Add(hardwareList[i].Type | MachineProfileEntryType.PartialFlag);
					}
				}
				else
				{
					num3 += (fileMoved ? hardwareList[i].FileMovedWeight : hardwareList[i].Weight);
					arrayList3.Add(hardwareList[i].Type);
				}
			}
			if (arrayList3.Count > 0)
			{
				diffs = (arrayList3.ToArray(typeof(MachineProfileEntryType)) as MachineProfileEntryType[]);
			}
			return Math.Min(num3, 10);
		}

		public static byte[] GetAdditionalData(string hash)
		{
			byte[] array = Class30.smethod_5(smethod_3(hash).ToString(), "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5");
			Array.Reverse(array);
			byte[] array2 = null;
			int num = array[array.Length - 4] >> 4;
			if (num > 0)
			{
				array2 = new byte[num];
				Buffer.BlockCopy(array, array.Length - (4 + num), array2, 0, num);
			}
			return array2;
		}

		public string GetComparableHash(bool firstOnly, byte[] additional)
		{
			if (machineProfileEntryCollection_0.Count == 0)
			{
				return null;
			}
			if (additional != null)
			{
				if (additional.Length > 8)
				{
					throw new Exception("E_AdditionalProfileDataTooLong");
				}
				if (additional.Length == 0)
				{
					additional = null;
				}
			}
			ArrayList arrayList_ = new ArrayList();
			using (MemoryStream memoryStream_ = new MemoryStream())
			{
				foreach (MachineProfileEntry item in (IEnumerable)machineProfileEntryCollection_0)
				{
					string text = null;
					string[] array = null;
					switch (item.Type & MachineProfileEntryType.TypeMask)
					{
					case MachineProfileEntryType.Cpu:
						text = smethod_13();
						break;
					case MachineProfileEntryType.Memory:
						text = smethod_12();
						break;
					case MachineProfileEntryType.MACAddress:
						array = smethod_14();
						break;
					case MachineProfileEntryType.SystemDrive:
						text = smethod_5();
						break;
					case MachineProfileEntryType.CDRom:
						array = smethod_16("{4D36E965-E325-11CE-BFC1-08002BE10318}", false, null);
						break;
					case MachineProfileEntryType.VideoCard:
						array = smethod_16("{4D36E968-E325-11CE-BFC1-08002BE10318}", false, "03");
						break;
					case MachineProfileEntryType.Scsi:
						array = smethod_16("{4D36E97B-E325-11CE-BFC1-08002BE10318}", false, "01", "00", "01", "04");
						break;
					case MachineProfileEntryType.Ide:
						array = smethod_16("{4D36E96A-E325-11CE-BFC1-08002BE10318}", false, "01", "01");
						break;
					case MachineProfileEntryType.Motherboard:
						array = smethod_19(bool_1: false);
						break;
					case MachineProfileEntryType.Custom1:
					case MachineProfileEntryType.Custom2:
					case MachineProfileEntryType.Custom3:
					case MachineProfileEntryType.Custom4:
						if (item.Interface1_0 == null)
						{
							text = "";
						}
						else
						{
							array = item.Interface1_0.imethod_0(item.Type);
						}
						break;
					default:
						text = "";
						break;
					}
					if (array == null)
					{
						array = new string[1]
						{
							text
						};
					}
					method_1(item, firstOnly, array, arrayList_, memoryStream_);
				}
				return method_0(firstOnly, additional, arrayList_, memoryStream_);
			}
		}

		public string GetComparableHashFromDiagnostic(bool firstOnly, string diagnosticFragment)
		{
			if (machineProfileEntryCollection_0.Count == 0)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(diagnosticFragment);
			ArrayList arrayList_ = new ArrayList();
			using (MemoryStream memoryStream_ = new MemoryStream())
			{
				foreach (MachineProfileEntry item in (IEnumerable)machineProfileEntryCollection_0)
				{
					string[] array = null;
					XmlNode xmlNode = xmlDocument.SelectSingleNode($"//Component[@type='{item.Type}']");
					if (xmlNode == null)
					{
						array = new string[1]
						{
							""
						};
					}
					else
					{
						array = new string[xmlNode.ChildNodes.Count];
						int num = 0;
						foreach (XmlNode childNode in xmlNode.ChildNodes)
						{
							if (childNode.Attributes["raw"] != null)
							{
								byte[] array2 = Convert.FromBase64String(childNode.Attributes["raw"].Value);
								char[] array3 = new char[array2.Length];
								for (int i = 0; i < array2.Length; i++)
								{
									array3[i] = (char)array2[i];
								}
								array[num] = new string(array3);
							}
							else
							{
								array[num] = childNode.Attributes["id"].Value;
							}
							num++;
						}
					}
					method_1(item, firstOnly, array, arrayList_, memoryStream_);
				}
				return method_0(firstOnly, null, arrayList_, memoryStream_);
			}
		}

		public static MachineProfile GetDefaultProfile()
		{
			MachineProfile machineProfile = new MachineProfile();
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.MACAddress, 3, 1, 3));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.Cpu, 1, 1, 2));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.SystemDrive, 3, 2, 3));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.Memory, 1, 1, 1));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.CDRom, 1, 1, 1));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.VideoCard, 1, 1, 1));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.Ide, 1, 1, 1));
			machineProfile.machineProfileEntryCollection_0.Add(new MachineProfileEntry(MachineProfileEntryType.Scsi, 1, 1, 1));
			return machineProfile;
		}

		public string GetDiagnosticHash()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter w = new StringWriter(stringBuilder))
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(w))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.Indentation = 1;
					xmlTextWriter.IndentChar = '\t';
					xmlTextWriter.WriteStartDocument();
					xmlTextWriter.WriteStartElement("MachineProfile");
					xmlTextWriter.WriteAttributeString("isLaptop", IsLaptop ? "true" : "false");
					xmlTextWriter.WriteElementString("Hash", GetComparableHash(firstOnly: false, null));
					xmlTextWriter.WriteStartElement("Os");
					OSRecord thisMachine = OSRecord.ThisMachine;
					xmlTextWriter.WriteAttributeString("product", thisMachine.Product.ToString());
					xmlTextWriter.WriteAttributeString("edition", thisMachine.Edition.ToString());
					if (thisMachine.Version != null)
					{
						xmlTextWriter.WriteAttributeString("version", thisMachine.Version.ToString());
					}
					xmlTextWriter.WriteAttributeString("servicePack", thisMachine.ServicePack);
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteStartElement("Laptop");
					xmlTextWriter.WriteAttributeString("isLaptop", IsLaptop ? "yes" : "no");
					bool systemPowerStatus = Class21.GetSystemPowerStatus(new byte[24]);
					xmlTextWriter.WriteAttributeString("battery", smethod_0() ? "yes" : "no");
					xmlTextWriter.WriteAttributeString("lid", smethod_1() ? "yes" : "no");
					xmlTextWriter.WriteAttributeString("gotPower", systemPowerStatus ? "yes" : "no");
					xmlTextWriter.WriteEndElement();
					foreach (MachineProfileEntry item in (IEnumerable)machineProfileEntryCollection_0)
					{
						xmlTextWriter.WriteStartElement("Component");
						xmlTextWriter.WriteAttributeString("displayName", item.DisplayName);
						xmlTextWriter.WriteAttributeString("type", item.Type.ToString());
						string text = null;
						string[] array = null;
						switch (item.Type & MachineProfileEntryType.TypeMask)
						{
						case MachineProfileEntryType.Cpu:
							text = smethod_13();
							break;
						case MachineProfileEntryType.Memory:
							text = smethod_12();
							break;
						case MachineProfileEntryType.MACAddress:
							array = smethod_14();
							break;
						case MachineProfileEntryType.SystemDrive:
							text = smethod_5();
							break;
						case MachineProfileEntryType.CDRom:
							array = smethod_16("{4D36E965-E325-11CE-BFC1-08002BE10318}", true, null);
							break;
						case MachineProfileEntryType.VideoCard:
							array = smethod_16("{4D36E968-E325-11CE-BFC1-08002BE10318}", true, "03");
							break;
						case MachineProfileEntryType.Scsi:
							array = smethod_16("{4D36E97B-E325-11CE-BFC1-08002BE10318}", true, "01", "00", "01", "04");
							break;
						case MachineProfileEntryType.Ide:
							array = smethod_16("{4D36E96A-E325-11CE-BFC1-08002BE10318}", true, "01", "01");
							break;
						case MachineProfileEntryType.Motherboard:
							array = smethod_19(bool_1: true);
							break;
						case MachineProfileEntryType.Custom1:
						case MachineProfileEntryType.Custom2:
						case MachineProfileEntryType.Custom3:
						case MachineProfileEntryType.Custom4:
							if (item.Interface1_0 == null)
							{
								text = "";
							}
							else
							{
								array = item.Interface1_0.imethod_0(item.Type);
							}
							break;
						default:
							text = "";
							break;
						}
						if (text == null)
						{
							if (array != null)
							{
								string[] array2 = array;
								foreach (string string_ in array2)
								{
									smethod_2(xmlTextWriter, string_);
								}
							}
						}
						else
						{
							smethod_2(xmlTextWriter, text);
						}
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
				}
			}
			return stringBuilder.ToString();
		}

		public static MachineProfileEntryType[] GetDifferences(string hash, MachineProfile profile)
		{
			return GetDifferences(hash, profile.GetComparableHash(firstOnly: false, null), profile.machineProfileEntryCollection_0);
		}

		public static MachineProfileEntryType[] GetDifferences(string hash, string comparedHash, MachineProfileEntryCollection hardwareList)
		{
			CompareHash(hash, comparedHash, fileMoved: false, hardwareList, out MachineProfileEntryType[] diffs);
			return diffs;
		}

		public static int GetHashVersion(string hash)
		{
			byte[] byte_;
			return (((int[])smethod_4(hash, out byte_)[0])[0] >> 2) & 7;
		}

		public static bool IsHashValid(string hash)
		{
			if (hash.Length < 5)
			{
				return false;
			}
			try
			{
				byte[] array = Class30.smethod_5(smethod_3(hash).ToString(), "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5");
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

		public static bool IsLaptopHash(string hash)
		{
			if (hash.Length < 5)
			{
				return false;
			}
			return (Class30.smethod_5(smethod_3(hash).ToString(), "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5")[1] & 2) != 0;
		}

		private void machineProfileEntryCollection_0_Changed(object sender, CollectionEventArgs e)
		{
			string_0 = null;
			if (machineProfileEntryCollection_0.Count > 16)
			{
				throw new Exception("E_TooMuchHardware");
			}
		}

		private string method_0(bool bool_1, byte[] byte_1, ArrayList arrayList_1, MemoryStream memoryStream_0)
		{
			int num = 0;
			if (byte_1 != null)
			{
				num += byte_1.Length * 8;
			}
			if (!bool_1)
			{
				num += (int)Math.Ceiling((double)arrayList_1.Count / 2.0) * 8;
			}
			for (int num2 = Class30.smethod_7((int)((memoryStream_0.Length + 3) * 8) + 4 + num, "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5", -1) % 5; num2 != 0; num2 = Class30.smethod_7((int)((memoryStream_0.Length + 3) * 8) + 4 + num, "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5", -1) % 5)
			{
				memoryStream_0.WriteByte(133);
			}
			if (byte_1 != null)
			{
				memoryStream_0.Write(byte_1, 0, byte_1.Length);
			}
			if (!bool_1)
			{
				for (int i = 0; i < arrayList_1.Count; i++)
				{
					byte b = (byte)((int)arrayList_1[i++] << 4);
					if (i < arrayList_1.Count)
					{
						b = (byte)(b | (byte)(int)arrayList_1[i]);
					}
					memoryStream_0.WriteByte(b);
				}
			}
			int num4 = machineProfileEntryCollection_0.Count - 1;
			if (byte_1 != null)
			{
				num4 |= byte_1.Length << 4;
			}
			memoryStream_0.WriteByte((byte)num4);
			memoryStream_0.Flush();
			byte[] array = memoryStream_0.ToArray();
			int num5 = 5381;
			byte[] array2 = array;
			foreach (byte b2 in array2)
			{
				num5 = (((num5 << 5) + num5) ^ b2);
			}
			memoryStream_0.WriteByte((byte)num5);
			byte value = (byte)(0x30 | (IsLaptop ? 2 : 0) | (bool_1 ? 1 : 0));
			memoryStream_0.WriteByte(value);
			value = 8;
			memoryStream_0.WriteByte(8);
			array = memoryStream_0.ToArray();
			Array.Reverse(array);
			StringBuilder stringBuilder = new StringBuilder(Class30.smethod_1(array, "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5", -1));
			char[] array3 = stringBuilder.ToString().ToCharArray();
			int num6 = 3 | ((int)array3[array3.Length - 1] % (array3.Length - 2));
			if (num6 >= array3.Length)
			{
				num6 = array3.Length - 1;
			}
			Array.Reverse(array3, 0, num6);
			num5 = ((((int)array3[0] % (array3.Length - 1)) & 7) | 1);
			Array.Reverse(array3, num5, array3.Length - num5);
			stringBuilder.Length = 0;
			stringBuilder.Append(array3);
			int k = stringBuilder.Length % 5;
			if (k == 0)
			{
				k = 5;
			}
			for (; k < stringBuilder.Length; k += 6)
			{
				stringBuilder.Insert(k, '-');
			}
			return stringBuilder.ToString();
		}

		private void method_1(MachineProfileEntry machineProfileEntry_0, bool bool_1, string[] string_1, ArrayList arrayList_1, MemoryStream memoryStream_0)
		{
			if (string_1.Length > 15)
			{
				throw new InvalidOperationException(Class37.smethod_0("E_TooManyHardwareVariations", machineProfileEntry_0.DisplayName));
			}
			if (!bool_1)
			{
				arrayList_1.Add(string_1.Length);
			}
			foreach (string string_2 in string_1)
			{
				memoryStream_0.WriteByte((byte)Class30.smethod_11(string_2));
				if (bool_1)
				{
					break;
				}
			}
		}

		private static bool smethod_0()
		{
			byte[] array = new byte[24];
			return Class21.GetSystemPowerStatus(array) && array[1] < 128;
		}

		private static bool smethod_1()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E97D-E325-11CE-BFC1-08002BE10318}", writable: false))
			{
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					foreach (string name in subKeyNames)
					{
						try
						{
							using (RegistryKey registryKey2 = registryKey.OpenSubKey(name, writable: false))
							{
								if (registryKey2 != null && registryKey2.GetValue("MatchingDeviceId") as string== "*pnp0c0d")
								{
									return true;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			return false;
		}

		private static string smethod_10(byte[] byte_1, int int_9)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = int_9; i < byte_1.Length && byte_1[i] != 0; i++)
			{
				num++;
			}
			for (int j = 0; j < num; j += 4)
			{
				for (int num2 = 1; num2 >= 0; num2--)
				{
					int num3 = 0;
					for (int k = 0; k < 2; k++)
					{
						num3 *= 16;
						byte b = byte_1[int_9 + j + num2 * 2 + k];
						if (b >= 48 && b <= 57)
						{
							num3 += b - 48;
						}
						else if (b >= 97 && b <= 102)
						{
							num3 += b - 81;
						}
						else if (b >= 65 && b <= 70)
						{
							num3 += b - 49;
						}
					}
					if (num3 > 0)
					{
						stringBuilder.Append((char)num3);
					}
				}
			}
			return stringBuilder.ToString();
		}

		private static string smethod_11(byte[] byte_1, int int_9)
		{
			int num = int_9;
			byte b;
			do
			{
				b = byte_1[num];
				num++;
			}
			while (b != 0);
			return Encoding.ASCII.GetString(byte_1, int_9, num - int_9 - 1);
		}

		private static string smethod_12()
		{
			string result = "";
			ulong num = 0uL;
			try
			{
				byte[] array = new byte[128];
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(4, 0))
				{
					array[0] = 64;
					Class21.GlobalMemoryStatusEx(array);
					return BitConverter.ToUInt64(array, 8).ToString(CultureInfo.InvariantCulture);
				}
				array[0] = 32;
				Class21.GlobalMemoryStatus(array);
				num = BitConverter.ToUInt32(array, 8);
			}
			catch (Exception)
			{
				result = "Exception";
			}
			return result;
		}

		private static string smethod_13()
		{
			byte[] array = new byte[48];
			Class28.smethod_1(array);
			if (Use64BitCompatibleCpuid)
			{
				for (int i = 40; i < array.Length; i++)
				{
					array[i] = 0;
				}
			}
			char[] array2 = new char[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = (char)array[j];
			}
			return new string(array2);
		}

		private static string[] smethod_14()
		{
			string[] result = null;
			try
			{
				byte[] array = null;
				uint uint_ = 0u;
				int[] array2 = Class21.bool_0 ? int_8 : int_7;
				int num = 0;
				Class21.GetAdaptersInfo(null, ref uint_);
				array = new byte[uint_];
				Class21.GetAdaptersInfo(array, ref uint_);
				if (array != null && array.Length != 0)
				{
					ArrayList arrayList = new ArrayList();
					while (true)
					{
						if (BitConverter.ToInt32(array, array2[0]) == 6)
						{
							string text = smethod_15(array, array2[1], 128);
							if (text.IndexOf("Windows Mobile") <= -1 && text.IndexOf("Bluetooth") <= -1 && text.IndexOf("VPN") <= -1 && (array[array2[4]] & 2) != 2)
							{
								string strB = smethod_15(array, array2[2], 256);
								using (RegistryKey registryKey = Toolbox.GetRegistryKey("HKLM\\SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E972-E325-11CE-BFC1-08002bE10318}", writeAccess: false))
								{
									if (registryKey != null)
									{
										bool flag = false;
										string[] subKeyNames = registryKey.GetSubKeyNames();
										foreach (string name in subKeyNames)
										{
											using (RegistryKey registryKey2 = registryKey.OpenSubKey(name, writable: false))
											{
												string text2 = registryKey2.GetValue("NetCfgInstanceId") as string;
												if (text2 != null && string.Compare(text2, strB, ignoreCase: true) == 0)
												{
													string text3 = registryKey2.GetValue("NetworkAddress") as string;
													if (text3 != null && text3.Length > 0)
													{
														flag = true;
														num++;
													}
													break;
												}
											}
										}
										if (flag)
										{
											goto IL_0061;
										}
									}
								}
								StringBuilder stringBuilder = new StringBuilder();
								int num2 = BitConverter.ToInt32(array, array2[3]) + array2[4];
								for (int j = array2[4]; j < num2; j++)
								{
									byte b = array[j];
									stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}:", b);
								}
								arrayList.Add(stringBuilder.ToString());
							}
						}
						goto IL_0061;
						IL_0061:
						if (Class21.bool_0)
						{
							long num3 = BitConverter.ToInt64(array, 0);
							if (num3 == 0)
							{
								break;
							}
							Marshal.Copy(new IntPtr(num3), array, 0, array2[5]);
						}
						else
						{
							int num4 = BitConverter.ToInt32(array, 0);
							if (num4 == 0)
							{
								break;
							}
							Marshal.Copy(new IntPtr(num4), array, 0, array2[5]);
						}
					}
					if (num > 0)
					{
						if (arrayList.Count > 0)
						{
							while (num > 0)
							{
								arrayList.Add("MANUALSAFE");
								num--;
							}
						}
						else
						{
							while (num > 0)
							{
								arrayList.Add(new Random().Next(1048575).ToString("X05"));
								num--;
							}
						}
					}
					if (arrayList.Count == 0)
					{
						return null;
					}
					return arrayList.ToArray(typeof(string)) as string[];
				}
				result = null;
			}
			catch
			{
			}
			return result;
		}

		private static string smethod_15(byte[] byte_1, int int_9, int int_10)
		{
			for (int i = 0; i < int_10; i++)
			{
				if (byte_1[i + int_9] == 0)
				{
					return Encoding.ASCII.GetString(byte_1, int_9, i);
				}
			}
			return Encoding.ASCII.GetString(byte_1, int_9, int_10);
		}

		private static string[] smethod_16(string string_1, bool bool_1, string string_2, params string[] string_3)
		{
			smethod_18();
			ArrayList arrayList = new ArrayList();
			foreach (Struct32 item in arrayList_0)
			{
				Struct32 struct32_ = item;
				if (!(struct32_.string_1 != string_1))
				{
					if (string_2 != null)
					{
						string[] string_4 = struct32_.string_2;
						foreach (string text in string_4)
						{
							int num = text.IndexOf("&CC_");
							if (num >= 0 && string.Compare(string_2, 0, text, num + 4, 2) == 0)
							{
								if (string_3 != null && string_3.Length > 0)
								{
									bool flag = false;
									foreach (string strA in string_3)
									{
										if (string.Compare(strA, 0, text, num + 6, 2) == 0)
										{
											smethod_17(bool_1, arrayList, struct32_);
											flag = true;
											break;
										}
									}
									if (flag)
									{
										break;
									}
								}
								else
								{
									smethod_17(bool_1, arrayList, struct32_);
								}
							}
						}
					}
					else
					{
						smethod_17(bool_1, arrayList, struct32_);
					}
				}
			}
			return arrayList.ToArray(typeof(string)) as string[];
		}

		private static void smethod_17(bool bool_1, ArrayList arrayList_1, Struct32 struct32_0)
		{
			string text = (!bool_1) ? struct32_0.string_5 : $"{struct32_0.string_5}\0[{struct32_0.string_3}]";
			if (!arrayList_1.Contains(text))
			{
				arrayList_1.Add(text);
			}
		}

		private static void smethod_18()
		{
			if (arrayList_0 == null)
			{
				lock (typeof(MachineProfile))
				{
					arrayList_0 = new ArrayList();
					ArrayList arrayList = new ArrayList();
					arrayList.Add("{4D36E97B-E325-11CE-BFC1-08002BE10318}");
					arrayList.Add("{4D36E965-E325-11CE-BFC1-08002BE10318}");
					arrayList.Add("{4D36E96A-E325-11CE-BFC1-08002BE10318}");
					arrayList.Add("{4D36E968-E325-11CE-BFC1-08002BE10318}");
					arrayList.Add("{4D36E97D-E325-11CE-BFC1-08002BE10318}");
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum", writable: false))
					{
						if (registryKey != null)
						{
							string[] array = new string[3]
							{
								"ACPI",
								"PCI",
								"IDE"
							};
							foreach (string text in array)
							{
								using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, writable: false))
								{
									if (registryKey2 != null)
									{
										string[] subKeyNames = registryKey2.GetSubKeyNames();
										foreach (string name in subKeyNames)
										{
											using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name, writable: false))
											{
												if (registryKey3 != null)
												{
													string[] subKeyNames2 = registryKey3.GetSubKeyNames();
													foreach (string name2 in subKeyNames2)
													{
														using (RegistryKey registryKey4 = registryKey3.OpenSubKey(name2, writable: false))
														{
															if (registryKey4 != null)
															{
																string text2 = (registryKey4.GetValue("ClassGUID") as string)?.ToUpper();
																if (text2 != null && arrayList.Contains(text2))
																{
																	Struct32 @struct = default(Struct32);
																	@struct.string_0 = registryKey4.Name;
																	@struct.string_1 = text2;
																	@struct.string_3 = (registryKey4.GetValue("DeviceDesc") as string);
																	@struct.string_4 = text;
																	@struct.string_2 = (registryKey4.GetValue("HardwareID") as string[]);
																	Struct32 struct2 = @struct;
																	if (struct2.string_2 != null)
																	{
																		int num = struct2.string_0.IndexOf(struct2.string_2[0]);
																		if (num != -1)
																		{
																			struct2.string_5 = struct2.string_0.Substring(num);
																			if (Class21.CM_Locate_DevNode(out IntPtr _, struct2.string_5, 0) == 0)
																			{
																				arrayList_0.Add(struct2);
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private static string[] smethod_19(bool bool_1)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = null;
			array = smethod_16("{4D36E97D-E325-11CE-BFC1-08002BE10318}", bool_1, "05");
			if (array != null && array.Length > 0)
			{
				arrayList.AddRange(array);
			}
			array = smethod_16("{4D36E97D-E325-11CE-BFC1-08002BE10318}", bool_1, "06");
			if (array != null && array.Length > 0)
			{
				arrayList.AddRange(array);
			}
			array = smethod_16("{4D36E97D-E325-11CE-BFC1-08002BE10318}", bool_1, "08");
			if (array != null && array.Length > 0)
			{
				arrayList.AddRange(array);
			}
			array = smethod_16("{4D36E97D-E325-11CE-BFC1-08002BE10318}", bool_1, "0C", "03", "00");
			if (array != null && array.Length > 0)
			{
				arrayList.AddRange(array);
			}
			if (bool_1)
			{
				return arrayList.ToArray(typeof(string)) as string[];
			}
			long num = 0L;
			foreach (string item in arrayList)
			{
				num += Class30.smethod_10(item);
			}
			return new string[1]
			{
				num.ToString()
			};
		}

		private static void smethod_2(XmlTextWriter xmlTextWriter_0, string string_1)
		{
			if (string_1 == null)
			{
				return;
			}
			int num = string_1.IndexOf('\0');
			if (num > -1 && string_1.Length > num && string_1[num + 1] == '[')
			{
				xmlTextWriter_0.WriteStartElement("Instance");
				xmlTextWriter_0.WriteAttributeString("id", string_1.Substring(0, num));
				xmlTextWriter_0.WriteAttributeString("details", string_1.Substring(num + 1));
				xmlTextWriter_0.WriteEndElement();
			}
			else if (num > -1)
			{
				xmlTextWriter_0.WriteStartElement("Instance");
				xmlTextWriter_0.WriteAttributeString("id", string_1.Replace('\0', ' '));
				byte[] array = new byte[string_1.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)string_1[i];
				}
				xmlTextWriter_0.WriteAttributeString("raw", Convert.ToBase64String(array));
				xmlTextWriter_0.WriteEndElement();
			}
			else
			{
				xmlTextWriter_0.WriteStartElement("Instance");
				xmlTextWriter_0.WriteAttributeString("id", string_1);
				xmlTextWriter_0.WriteEndElement();
			}
		}

		private static StringBuilder smethod_3(string string_1)
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

		private static ArrayList smethod_4(string string_1, out byte[] byte_1)
		{
			byte_1 = null;
			ArrayList arrayList = new ArrayList();
			byte[] array = Class30.smethod_5(smethod_3(string_1).ToString(), "U9VWT2FG3Q7RS0AC1DEYMNX6P8HJ4KL5");
			byte b = array[1];
			Array.Reverse(array);
			bool flag = (b & 1) != 0;
			arrayList.Add(new int[1]
			{
				b
			});
			int num = (array[array.Length - 4] & 0xF) + 1;
			int i = 0;
			if (flag)
			{
				for (; i < num; i++)
				{
					arrayList.Add(new int[1]
					{
						array[i]
					});
				}
			}
			else
			{
				int[] array2 = new int[num];
				int num2 = 0;
				for (int j = array.Length - (4 + (int)Math.Ceiling((double)num / 2.0)); j < array.Length - 4; j++)
				{
					byte b2 = array[j];
					array2[num2++] = b2 >> 4;
					if (num2 < array2.Length)
					{
						array2[num2++] = (b2 & 0xF);
					}
				}
				for (int k = 0; k < num; k++)
				{
					int[] array3 = new int[array2[k]];
					for (num2 = 0; num2 < array2[k]; num2++)
					{
						array3[num2] = array[i++];
					}
					arrayList.Add(array3);
				}
			}
			int num6 = array[array.Length - 4] >> 4;
			if (num6 > 0)
			{
				byte_1 = new byte[num6];
				Buffer.BlockCopy(array, array.Length - (4 + num6), byte_1, 0, num6);
			}
			return arrayList;
		}

		private static string smethod_5()
		{
			return smethod_6(Environment.GetFolderPath(Environment.SpecialFolder.System));
		}

		private static string smethod_6(string string_1)
		{
			string text = null;
			string string_2;
			string string_3 = smethod_9(string_1, out string_2);
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				text = smethod_8(string_3);
				if (text == null)
				{
					text = smethod_7(string_2);
				}
			}
			if (text != null && text.Length != 0 && !text.StartsWith("VEND:"))
			{
				return text;
			}
			if (!Class21.GetVolumeInformation(string_2 + '\\', null, 0, out int num, out int num2, out num2, null, 0))
			{
				num = new Random().Next(int.MaxValue);
			}
			long long_ = 0L;
			long long_2 = 0L;
			if (OSRecord.ThisMachine.Version.Major >= 5)
			{
				IntPtr intptr_ = Class21.CreateFile($"\\\\.\\{string_2}", 0u, 3u, IntPtr.Zero, 3u, 0u, IntPtr.Zero);
				byte[] array = new byte[256];
				try
				{
					int num3 = 0;
					if (Class21.DeviceIoControl(intptr_, 458824, IntPtr.Zero, 0, array, array.Length, ref num3, IntPtr.Zero))
					{
						long_ = BitConverter.ToInt64(array, 16) - 4096;
					}
				}
				finally
				{
					Class21.CloseHandle(intptr_);
				}
			}
			if (long_ == 0 && !Class21.GetDiskFreeSpaceEx(string_2 + '\\', out long_2, out long_, out long_2))
			{
				long_ = new Random().Next(int.MaxValue);
			}
			return string.Format("{3}{0:X4}-{1:X4}:{2}", num >> 16, num & 0xFFFF, long_, text);
		}

		private static string smethod_7(string string_1)
		{
			IntPtr intptr_ = Class21.CreateFile($"\\\\.\\{string_1}", 0u, 3u, IntPtr.Zero, 3u, 0u, IntPtr.Zero);
			if (intptr_.ToInt32() == -1)
			{
				return null;
			}
			string result;
			try
			{
				byte[] value = new byte[12];
				byte[] array = new byte[10000];
				int num = 0;
				GCHandle gCHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
				try
				{
					if (!Class21.DeviceIoControl(intptr_, 2954240, gCHandle.AddrOfPinnedObject(), 12, array, array.Length, ref num, IntPtr.Zero))
					{
						return null;
					}
				}
				finally
				{
					gCHandle.Free();
				}
				int num2 = BitConverter.ToInt32(array, 24);
				if (num2 <= num && num2 >= 0)
				{
					string text = smethod_10(array, num2);
					if (text != null && text.Length != 0)
					{
						return text.Trim();
					}
					num2 = BitConverter.ToInt32(array, 12);
					text = null;
					if (num2 != 0)
					{
						text = smethod_11(array, num2).Trim();
					}
					num2 = BitConverter.ToInt32(array, 16);
					if (num2 != 0)
					{
						text = text + '-' + smethod_11(array, num2).Trim();
					}
					num2 = BitConverter.ToInt32(array, 20);
					if (num2 != 0)
					{
						text = text + '.' + smethod_11(array, num2).Trim();
					}
					return "VEND:" + text;
				}
				result = null;
			}
			finally
			{
				if (intptr_.ToInt32() != -1)
				{
					Class21.CloseHandle(intptr_);
				}
			}
			return result;
		}

		private static string smethod_8(string string_1)
		{
			IntPtr intptr_ = Class21.CreateFile($"\\\\.\\Scsi{string_1}:", 3221225472u, 3u, IntPtr.Zero, 3u, 0u, IntPtr.Zero);
			if (intptr_.ToInt32() == -1)
			{
				return null;
			}
			string @string;
			try
			{
				int num = 0;
				byte[] array;
				while (true)
				{
					if (num >= 2)
					{
						return null;
					}
					array = new byte[557];
					Buffer.BlockCopy(byte_0, 0, array, 0, byte_0.Length);
					GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					int num2 = 0;
					if (Class21.DeviceIoControl(intptr_, 315400, gCHandle.AddrOfPinnedObject(), 60, array, array.Length, ref num2, IntPtr.Zero))
					{
						break;
					}
					gCHandle.Free();
					num++;
				}
				for (int i = 64; i < 80; i += 2)
				{
					Array.Reverse(array, i, 2);
				}
				@string = Encoding.ASCII.GetString(array, 64, 16);
			}
			finally
			{
				if (intptr_.ToInt32() != -1)
				{
					Class21.CloseHandle(intptr_);
				}
			}
			return @string;
		}

		private static string smethod_9(string string_1, out string string_2)
		{
			string_2 = null;
			if (string_1 != null && string_1.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder(255);
				string pathRoot = Path.GetPathRoot(string_1);
				string_2 = ((pathRoot == string_1) ? pathRoot : pathRoot.Substring(0, pathRoot.Length - 1));
				Class21.QueryDosDevice(string_2, stringBuilder, 255);
				if (stringBuilder.Length > 0)
				{
					pathRoot = Path.GetDirectoryName(stringBuilder.ToString());
					if (pathRoot == "\\Device")
					{
						pathRoot = stringBuilder.ToString();
					}
					int num = pathRoot.Length;
					while (num > 0 && char.IsDigit(pathRoot[num - 1]))
					{
						num--;
					}
					if (num > 0)
					{
						return pathRoot.Substring(num);
					}
				}
			}
			return null;
		}
	}
}
