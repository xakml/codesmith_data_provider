using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace DeployLX.Licensing.v4
{
	public sealed class OSRecord : IChange
	{
		private bool bool_0;

		private ChangeEventHandler changeEventHandler_0;

		private OSEditions oseditions_0;

		private OSProduct osproduct_0;

		private static OSRecord osrecord_0;

		private string string_0;

		private Version version_0;

		public OSEditions Edition
		{
			get
			{
				return oseditions_0;
			}
			set
			{
				method_1();
				if (oseditions_0 != value)
				{
					oseditions_0 = value;
					method_0("Edition");
				}
			}
		}

		public OSProduct Product
		{
			get
			{
				return osproduct_0;
			}
			set
			{
				method_1();
				if (osproduct_0 != value)
				{
					osproduct_0 = value;
					method_0("Product");
				}
			}
		}

		public string ServicePack
		{
			get
			{
				return string_0;
			}
			set
			{
				method_1();
				if (string_0 != value)
				{
					string_0 = value;
					method_0("ServicePack");
				}
			}
		}

		public static OSRecord ThisMachine
		{
			get
			{
				if (osrecord_0 == null && osrecord_0 == null)
				{
					osrecord_0 = smethod_0();
				}
				return osrecord_0;
			}
		}

		public Version Version
		{
			get
			{
				return version_0;
			}
			set
			{
				method_1();
				if (version_0 != value)
				{
					version_0 = value;
					method_0("Version");
				}
			}
		}

		public event ChangeEventHandler Changed
		{
			add
			{
				ChangeEventHandler changeEventHandler = changeEventHandler_0;
				ChangeEventHandler changeEventHandler2;
				do
				{
					changeEventHandler2 = changeEventHandler;
					ChangeEventHandler value2 = (ChangeEventHandler)Delegate.Combine(changeEventHandler2, value);
					changeEventHandler = Interlocked.CompareExchange(ref changeEventHandler_0, value2, changeEventHandler2);
				}
				while (changeEventHandler != changeEventHandler2);
			}
			remove
			{
				ChangeEventHandler changeEventHandler = changeEventHandler_0;
				ChangeEventHandler changeEventHandler2;
				do
				{
					changeEventHandler2 = changeEventHandler;
					ChangeEventHandler value2 = (ChangeEventHandler)Delegate.Remove(changeEventHandler2, value);
					changeEventHandler = Interlocked.CompareExchange(ref changeEventHandler_0, value2, changeEventHandler2);
				}
				while (changeEventHandler != changeEventHandler2);
			}
		}

		void IChange.MakeReadOnly()
		{
			bool_0 = true;
		}

		public bool IsMatch(OSRecord record)
		{
			if (osproduct_0 != 0 && record.osproduct_0 != 0 && osproduct_0 != record.osproduct_0)
			{
				return false;
			}
			if (oseditions_0 != 0 && record.oseditions_0 != 0)
			{
				IEnumerator enumerator = Enum.GetValues(typeof(OSEditions)).GetEnumerator();
				while (enumerator.MoveNext())
				{
					int num = (int)enumerator.Current;
					OSEditions oSEditions = (OSEditions)((int)record.oseditions_0 & num);
					OSEditions oSEditions2 = (OSEditions)((int)oseditions_0 & num);
					if (oSEditions != 0 && oSEditions2 == OSEditions.NotSet)
					{
						return false;
					}
				}
			}
			if (version_0 != null && record.version_0 != null)
			{
				if (version_0.Major != record.version_0.Major)
				{
					return false;
				}
				if (record.version_0.Minor > -1 && version_0.Minor != record.version_0.Minor)
				{
					return false;
				}
				if (record.version_0.Build > -1 && version_0.Build != record.version_0.Build)
				{
					return false;
				}
			}
			return record.string_0 == null || (string_0 != null && string.Compare(string_0, record.string_0, ignoreCase: true) == 0);
		}

		private void method_0(string string_1)
		{
			if (changeEventHandler_0 != null)
			{
				changeEventHandler_0(this, new ChangeEventArgs(string_1, this));
			}
		}

		private void method_1()
		{
			if (bool_0)
			{
				throw new Exception("E_ReadOnlyObject");
			}
		}

		public bool ReadFromXml(XmlReader reader)
		{
			osproduct_0 = OSProduct.NotSet;
			oseditions_0 = OSEditions.NotSet;
			version_0 = null;
			string attribute = reader.GetAttribute("product");
			if (attribute != null)
			{
				osproduct_0 = (OSProduct)Toolbox.FastParseInt32(attribute);
			}
			attribute = reader.GetAttribute("edition");
			if (attribute != null)
			{
				oseditions_0 = (OSEditions)Toolbox.FastParseInt32(attribute);
			}
			attribute = reader.GetAttribute("version");
			if (attribute != null)
			{
				version_0 = new Version(attribute);
			}
			string_0 = reader.GetAttribute("servicePack");
			reader.Read();
			return true;
		}

		private static OSRecord smethod_0()
		{
			OSRecord oSRecord = new OSRecord();
			oSRecord.version_0 = Environment.OSVersion.Version;
			OSRecord oSRecord2 = oSRecord;
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Win32S:
				oSRecord2.osproduct_0 = OSProduct.Windows32s;
				break;
			case PlatformID.Win32Windows:
				oSRecord2.osproduct_0 = OSProduct.Windows9x;
				break;
			case PlatformID.Win32NT:
				if (oSRecord2.version_0.Major > 4)
				{
					byte[] array = new byte[284];
					array[0] = 28;
					array[1] = 1;
					GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					try
					{
						if (!Class21.GetVersionEx(gCHandle.AddrOfPinnedObject()))
						{
							array[0] = 20;
							array[1] = 1;
							Class21.GetVersionEx(gCHandle.AddrOfPinnedObject());
						}
					}
					finally
					{
						gCHandle.Free();
					}
					int major = BitConverter.ToInt32(array, 4);
					int minor = BitConverter.ToInt32(array, 8);
					int build = BitConverter.ToInt32(array, 12);
					oSRecord2.version_0 = new Version(major, minor, build);
					switch (array[282])
					{
					case 1:
						oSRecord2.oseditions_0 |= OSEditions.Workstation;
						break;
					case 2:
						oSRecord2.oseditions_0 |= OSEditions.DomainController;
						break;
					case 3:
						oSRecord2.oseditions_0 |= OSEditions.Server;
						break;
					}
					if ((BitConverter.ToInt16(array, 280) & 0x200) != 0)
					{
						oSRecord2.oseditions_0 |= OSEditions.Home;
					}
					else if ((oSRecord2.oseditions_0 & (OSEditions.DomainController | OSEditions.Server)) == OSEditions.NotSet)
					{
						oSRecord2.oseditions_0 |= OSEditions.Professional;
					}
					int count = 256;
					for (int i = 20; i < 276; i += 2)
					{
						if (array[i] == 0)
						{
							count = i - 20;
							break;
						}
					}
					oSRecord2.string_0 = Encoding.Unicode.GetString(array, 20, count);
					if (Class21.GetSystemMetrics(87) != 0)
					{
						oSRecord2.oseditions_0 |= OSEditions.MediaCenter;
					}
					if (Class21.GetSystemMetrics(86) != 0)
					{
						oSRecord2.oseditions_0 |= OSEditions.TabletPC;
					}
				}
				switch (oSRecord2.version_0.Major)
				{
				case 3:
				case 4:
					oSRecord2.osproduct_0 = OSProduct.WindowsNT;
					break;
				case 5:
					switch (oSRecord2.Version.Minor)
					{
					case 0:
						oSRecord2.osproduct_0 = OSProduct.Windows2000;
						break;
					case 1:
						oSRecord2.osproduct_0 = OSProduct.WindowsXP;
						break;
					case 2:
						if ((oSRecord2.oseditions_0 & OSEditions.Workstation) != 0)
						{
							oSRecord2.osproduct_0 = OSProduct.WindowsXP;
						}
						else
						{
							oSRecord2.osproduct_0 = OSProduct.Windows2003;
						}
						break;
					}
					break;
				case 6:
					if (oSRecord2.Version.Minor != 1)
					{
						if ((oSRecord2.oseditions_0 & OSEditions.Workstation) != 0)
						{
							oSRecord2.osproduct_0 = OSProduct.WindowsVista;
						}
						else
						{
							oSRecord2.osproduct_0 = OSProduct.Windows2008;
						}
					}
					else
					{
						oSRecord2.osproduct_0 = OSProduct.Windows7;
					}
					break;
				}
				break;
			case PlatformID.WinCE:
				oSRecord2.osproduct_0 = OSProduct.WindowsCE;
				break;
			}
			if (IntPtr.Size == 8)
			{
				oSRecord2.oseditions_0 |= OSEditions.SixtyFourBit;
				return oSRecord2;
			}
			if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432") != null)
			{
				oSRecord2.oseditions_0 |= OSEditions.SixtyFourBit;
				return oSRecord2;
			}
			oSRecord2.oseditions_0 |= OSEditions.ThirtyTwoBit;
			return oSRecord2;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			OSProduct oSProduct = osproduct_0;
			if (oSProduct <= OSProduct.WindowsNT)
			{
				if (oSProduct <= OSProduct.WindowsCE)
				{
					if (oSProduct != 0)
					{
						if (oSProduct != OSProduct.WindowsCE)
						{
							goto IL_0162;
						}
						stringBuilder.Append("Windows CE");
					}
					else
					{
						stringBuilder.Append("Unknown OS");
					}
				}
				else
				{
					switch (oSProduct)
					{
					case OSProduct.Windows9x:
						break;
					case OSProduct.WindowsNT:
						goto IL_0092;
					case OSProduct.Windows32s:
						goto IL_00a3;
					default:
						goto IL_0162;
					}
					stringBuilder.Append("Windows 9x");
				}
			}
			else
			{
				switch (oSProduct)
				{
				case OSProduct.Windows2000:
					break;
				case OSProduct.WindowsXP:
					goto IL_010d;
				case OSProduct.Windows2003:
					goto IL_011b;
				case OSProduct.WindowsVista:
					goto IL_0129;
				case OSProduct.Windows2008:
					goto IL_0137;
				case OSProduct.Windows7:
					goto IL_0145;
				case OSProduct.Unknown:
					goto IL_0153;
				default:
					goto IL_0162;
				}
				stringBuilder.Append("Windows 2000");
			}
			goto IL_016e;
			IL_0137:
			stringBuilder.Append("Windows 2008");
			goto IL_016e;
			IL_011b:
			stringBuilder.Append("Windows 2003");
			goto IL_016e;
			IL_016e:
			if (osproduct_0 < OSProduct.WindowsVista)
			{
				if ((oseditions_0 & OSEditions.Home) != 0)
				{
					stringBuilder.Append(" Home");
				}
				if ((oseditions_0 & OSEditions.Professional) != 0)
				{
					stringBuilder.Append(" Professional");
				}
				if ((oseditions_0 & (OSEditions.DomainController | OSEditions.Server)) != 0)
				{
					stringBuilder.Append(" Server");
				}
				if ((oseditions_0 & OSEditions.TabletPC) != 0)
				{
					stringBuilder.Append(" Tablet PC");
				}
				if ((oseditions_0 & OSEditions.MediaCenter) != 0)
				{
					stringBuilder.Append(" Media Center");
				}
			}
			if ((oseditions_0 & (OSEditions.DomainController | OSEditions.Server)) != 0)
			{
				stringBuilder.Append(" Server");
			}
			if ((oseditions_0 & OSEditions.ThirtyTwoBit) != 0)
			{
				stringBuilder.Append(" 32-bit");
			}
			if ((oseditions_0 & OSEditions.SixtyFourBit) != 0)
			{
				stringBuilder.Append(" 64-bit");
			}
			if (string_0 != null && string_0.Length > 0)
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(string_0);
			}
			if (version_0 != null && version_0.Major > 0)
			{
				stringBuilder.Append(" [");
				stringBuilder.Append(version_0.Major);
				if (version_0.Minor > -1)
				{
					stringBuilder.Append('.');
					stringBuilder.Append(version_0.Minor);
					if (version_0.Build > -1)
					{
						stringBuilder.Append(" Build ");
						stringBuilder.Append(version_0.Build);
					}
				}
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
			IL_0153:
			stringBuilder.Append("UNIX");
			goto IL_016e;
			IL_0162:
			stringBuilder.Append("ERROR BAD OS");
			goto IL_016e;
			IL_010d:
			stringBuilder.Append("Windows XP");
			goto IL_016e;
			IL_0092:
			stringBuilder.Append("Windows NT");
			goto IL_016e;
			IL_0145:
			stringBuilder.Append("Windows 7");
			goto IL_016e;
			IL_00a3:
			stringBuilder.Append("Windows 3.x");
			goto IL_016e;
			IL_0129:
			stringBuilder.Append("Windows Vista");
			goto IL_016e;
		}

		public bool WriteToXml(XmlWriter writer)
		{
			writer.WriteStartElement("OSRecord");
			if (osproduct_0 != 0)
			{
				int num = (int)osproduct_0;
				writer.WriteAttributeString("product", num.ToString());
			}
			if (oseditions_0 != 0)
			{
				int num = (int)oseditions_0;
				writer.WriteAttributeString("edition", num.ToString());
			}
			if (version_0 != null && version_0.Major > 0)
			{
				writer.WriteAttributeString("version", version_0.ToString());
			}
			if (string_0 != null && string_0.Length > 0)
			{
				writer.WriteAttributeString("servicePack", string_0);
			}
			writer.WriteEndElement();
			return true;
		}
	}
}
