using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DeployLX.Licensing.v4
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Toolbox
	{
		private sealed class Class39 : IDisposable
		{
			private ThreadPriority threadPriority_0 = Thread.CurrentThread.Priority;

			public Class39(ThreadPriority priority)
			{
				Thread.CurrentThread.Priority = priority;
			}

			public void Dispose()
			{
				Thread.CurrentThread.Priority = threadPriority_0;
			}
		}

		private delegate Bitmap Delegate13(Control ctrl);

		private static readonly ControlStyles controlStyles_0;

		[ThreadStatic]
		private static int int_0;

		private static int int_1;

		private static int int_2;

		private static int int_3;

		private static int int_4;

		private static int int_5;

		private static MethodInfo methodInfo_0;

		private static MethodInfo methodInfo_1;

		[CompilerGenerated]
		private static ThreadStart threadStart_0;

		private static Type type_0;

		public static bool IisIsAvailable => int_2 == 1;

		public static bool IsFipsEnabled
		{
			get
			{
				if (int_4 == 0)
				{
					try
					{
						new RijndaelManaged();
						int_4 = -1;
					}
					catch (InvalidOperationException)
					{
						int_4 = 1;
					}
				}
				return int_4 == 1;
			}
		}

		public static bool IsServiceRequest
		{
			get
			{
				if (int_1 == -1)
				{
					int_1 = ((!Environment.UserInteractive) ? 1 : 0);
				}
				return int_1 == 1;
			}
		}

		public static bool IsTerminalServices
		{
			get
			{
				if (int_3 == -1 && int_3 == -1)
				{
					if ((Environment.OSVersion.Platform & PlatformID.WinCE) != 0)
					{
						int_3 = ((Class21.GetSystemMetrics(4096) != 0) ? 1 : 0);
					}
					else
					{
						int_3 = 0;
					}
				}
				return int_3 == 1;
			}
		}

		public static bool ShouldUseWebLogic
		{
			get
			{
				if (int_0 == 0)
				{
					try
					{
						int_0 = 1;
					}
					catch
					{
					}
				}
				return int_0 == 1;
			}
		}

		static Toolbox()
		{
			controlStyles_0 = ((Environment.Version.Major >= 2) ? (ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer) : ControlStyles.DoubleBuffer);
			int_1 = -1;
			int_2 = -1;
			int_3 = -1;
			try
			{
			}
			catch
			{
				int_2 = 0;
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

		public static bool CanGetControlAsBitmap()
		{
			if (int_5 == 0)
			{
				try
				{
					Control obj = new Control();
					methodInfo_1 = typeof(Control).GetMethod("GetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
					methodInfo_0 = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
					if (methodInfo_1 != null && methodInfo_0 != null)
					{
						methodInfo_0.Invoke(obj, new object[2]
						{
							ControlStyles.DoubleBuffer,
							true
						});
					}
					int_5 = 1;
				}
				catch
				{
					int_5 = -1;
				}
			}
			return int_5 == 1;
		}

		public static bool CheckInternetConnection(Uri suggestedUrl)
		{
			if (suggestedUrl == null)
			{
				suggestedUrl = new Uri("http://www.google.com");
			}
			try
			{
				TcpClient tcpClient = new TcpClient();
				tcpClient.Connect(suggestedUrl.Host, suggestedUrl.Port);
				tcpClient.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static int FastParseInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return FastParseInt32(value, 0, value.Length);
		}

		public static int FastParseInt32(string value, int start, int length)
		{
			if (value == null)
			{
				return 0;
			}
			int num = 0;
			int i = start;
			bool flag = false;
			if (value[0] == '-')
			{
				i++;
				flag = true;
			}
			for (int num2 = start + length; i < num2; i++)
			{
				num = num * 10 + (value[i] - 48);
			}
			if (!flag)
			{
				return num;
			}
			return -num;
		}

		public static DateTime FastParseSortableDate(string value)
		{
			if (value.Length != 19)
			{
				throw new ArgumentException("Invalid date", "value");
			}
			return new DateTime(FastParseInt32(value, 0, 4), FastParseInt32(value, 5, 2), FastParseInt32(value, 8, 2), FastParseInt32(value, 11, 2), FastParseInt32(value, 14, 2), FastParseInt32(value, 17, 2), DateTimeKind.Utc);
		}

		public static string FormatSortableDate(DateTime date)
		{
			return $"{date.Year:0000}-{date.Month:00}-{date.Day:00}T{date.Hour:00}:{date.Minute:00}:{date.Second:00}";
		}

		public static string FormatTime(long seconds, long max, bool includeUnit)
		{
			string text = null;
			if (max > 86400)
			{
				text = "M_Days";
				seconds = (int)Math.Ceiling((float)seconds / 86400f);
			}
			else if (max > 3600)
			{
				text = "M_Hours";
				seconds = (int)Math.Ceiling((float)seconds / 3600f);
			}
			else if (max > 60)
			{
				text = "M_Minutes";
				seconds = (int)Math.Ceiling((float)seconds / 60f);
			}
			else
			{
				text = "M_Seconds";
			}
			if (!includeUnit)
			{
				return seconds.ToString();
			}
			if (seconds == 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return $"{seconds} {Class37.smethod_0(text)}";
		}

		public static SymmetricAlgorithm GetAes(bool forceFips)
		{
			return Activator.CreateInstance(type_0) as SymmetricAlgorithm;
		}

		public static Bitmap GetControlAsBitmap(Control ctrl)
		{
			if (ctrl == null)
			{
				return null;
			}
			Form form = ctrl.FindForm();
			if (ctrl.IsDisposed || (form != null && form.IsDisposed))
			{
				return null;
			}
			if (ctrl.InvokeRequired)
			{
				return ctrl.Invoke(new Delegate13(GetControlAsBitmap), ctrl) as Bitmap;
			}
			Bitmap result;
			using (ThreadBoost(ThreadPriority.Highest))
			{
				if (!ctrl.IsHandleCreated)
				{
					ctrl.CreateControl();
				}
				Bitmap bitmap = new Bitmap(ctrl.Width, ctrl.Height, PixelFormat.Format32bppRgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					IntPtr hdc = graphics.GetHdc();
					ArrayList arrayList = new ArrayList();
					try
					{
						smethod_4(ctrl, arrayList);
						Class21.SendMessage(ctrl.Handle, 791u, hdc, (IntPtr)23);
						result = bitmap;
					}
					finally
					{
						graphics.ReleaseHdc(hdc);
						foreach (Control item in arrayList)
						{
							methodInfo_0.Invoke(item, new object[2]
							{
								controlStyles_0,
								true
							});
						}
					}
				}
			}
			return result;
		}

		[ComVisible(false)]
		public static string GetFullPath(string path)
		{
			if (AppDomain.CurrentDomain.SetupInformation.ConfigurationFile == null)
			{
				return null;
			}
			return Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile), path);
		}

		public static Image GetImage(Stream stream)
		{
			try
			{
				Image image = null;
				if (stream != null)
				{
					image = Image.FromStream(stream);
					if (image != null)
					{
						try
						{
							return new Bitmap(image, image.Size);
						}
						finally
						{
							image.Dispose();
						}
					}
				}
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static RegistryKey GetRegistryKey(string key, bool writeAccess)
		{
			int num = key.StartsWith("registry:") ? 9 : 0;
			if ((num == 0 && key.Length < 5) || (num == 9 && key.Length < 14))
			{
				throw new ArgumentException("E_InvalidRegistryKey");
			}
			RegistryKey registryKey = null;
			bool flag = false;
			if (string.Compare(key, num, "HKLM", 0, 4, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				registryKey = Registry.LocalMachine;
			}
			else if (string.Compare(key, num, "HKCU", 0, 4, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				registryKey = Registry.CurrentUser;
			}
			else if (string.Compare(key, num, "HKCR", 0, 4, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				registryKey = Registry.ClassesRoot;
			}
			else if (string.Compare(key, num, "HK*U", 0, 4, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				registryKey = Registry.CurrentUser;
				flag = true;
			}
			else if (string.Compare(key, num, "HK*M", 0, 4, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				registryKey = Registry.LocalMachine;
				flag = true;
			}
			if (registryKey == null)
			{
				throw new ArgumentException(Class37.smethod_0("E_InvalidRegistryKey", key), "code");
			}
			string name = key.Substring(num + 5);
			RegistryKey registryKey2 = registryKey.OpenSubKey(name, writeAccess);
			if (!flag || registryKey2 != null)
			{
				return registryKey2;
			}
			if (registryKey == Registry.CurrentUser)
			{
				return Registry.LocalMachine.OpenSubKey(name, writeAccess);
			}
			return Registry.CurrentUser.OpenSubKey(name, writeAccess);
		}

		public static void GrantEveryoneAccess(string path)
		{
			if (path != null && path.Length != 0)
			{
				bool flag = path.StartsWith("reg::");
				try
				{
					Thread thread = new Thread(smethod_5);
					thread.Start();
					thread.Join();
				}
				catch
				{
				}
				using (new Class46())
				{
					if (flag)
					{
						path = (path.StartsWith("reg::HKEY_CURRENT_USER") ? path.Substring(10) : (path.StartsWith("reg::HKCU") ? ("CURRENT_USER" + path.Substring(9)) : (path.StartsWith("reg::HKEY_LOCAL_MACHINE") ? ("MACHINE" + path.Substring(24)) : (path.StartsWith("reg::HKLM") ? ("MACHINE" + path.Substring(9)) : (path.StartsWith("reg::HKEY_CLASSES_ROOT") ? path.Substring(9) : (path.StartsWith("reg::HKCR") ? ("CLASSES_ROOT" + path.Substring(9)) : (path.StartsWith("reg::HKEY_USERS") ? path.Substring(10) : ((!path.StartsWith("reg::HKU")) ? path.Substring(5) : ("USERS" + path.Substring(8))))))))));
						goto IL_01fe;
					}
					if (File.Exists(path) || Directory.Exists(path))
					{
						goto IL_01fe;
					}
					goto end_IL_0074;
					IL_01fe:
					IntPtr intptr_ = IntPtr.Zero;
					IntPtr intptr_2 = IntPtr.Zero;
					IntPtr intptr_3 = IntPtr.Zero;
					if (path.IndexOf('\0') > -1)
					{
						path = path.Replace("\0", "");
					}
					try
					{
						int namedSecurityInfo = Class21.GetNamedSecurityInfo(path, (!flag) ? 1 : 4, 4, IntPtr.Zero, IntPtr.Zero, out intptr_, IntPtr.Zero, out intptr_2);
						if (namedSecurityInfo != 0)
						{
							throw new Win32Exception(namedSecurityInfo);
						}
						Class21.Struct36 @struct = default(Class21.Struct36);
						@struct.int_0 = 2097151;
						@struct.int_1 = 1;
						@struct.int_2 = 0;
						Class21.Struct36 struct36_ = @struct;
						if (!Class21.ConvertStringSidToSid("S-1-1-0", ref struct36_.struct37_0.intptr_1))
						{
							Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
						}
						struct36_.struct37_0.intptr_0 = IntPtr.Zero;
						struct36_.struct37_0.int_1 = 0;
						struct36_.struct37_0.int_2 = 5;
						while (Class21.DeleteAce(intptr_, 0))
						{
						}
						namedSecurityInfo = Class21.SetEntriesInAcl(1, ref struct36_, intptr_, out intptr_3);
						if (namedSecurityInfo != 0)
						{
							throw new Win32Exception(namedSecurityInfo);
						}
						namedSecurityInfo = Class21.SetNamedSecurityInfo(path, (!flag) ? 1 : 4, 536870916u, IntPtr.Zero, IntPtr.Zero, intptr_3, IntPtr.Zero);
						if (namedSecurityInfo != 0)
						{
							throw new Win32Exception(namedSecurityInfo);
						}
					}
					finally
					{
						if (intptr_2 != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intptr_2);
						}
						if (intptr_3 != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intptr_3);
						}
					}
					end_IL_0074:;
				}
			}
		}

		public static bool IsResourceAddress(string address)
		{
			if (address == null || address.Length == 0)
			{
				return false;
			}
			if (string.Compare(address, 0, "asmres://", 0, 9, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && string.Compare(address, 0, "licres://", 0, 9, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && string.Compare(address, 0, "html://", 0, 7, ignoreCase: true, CultureInfo.InvariantCulture) != 0)
			{
				return false;
			}
			return true;
		}

		public static bool IsUriResource(string resource)
		{
			if (resource == null || resource.Length == 0)
			{
				return false;
			}
			if (string.Compare(resource, 0, Uri.UriSchemeHttp, 0, Uri.UriSchemeHttp.Length, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && string.Compare(resource, 0, Uri.UriSchemeHttps, 0, Uri.UriSchemeHttps.Length, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && string.Compare(resource, 0, Uri.UriSchemeFtp, 0, Uri.UriSchemeFtp.Length, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && string.Compare(resource, 0, Uri.UriSchemeFile, 0, Uri.UriSchemeFtp.Length, ignoreCase: true, CultureInfo.InvariantCulture) != 0 && !IsResourceAddress(resource))
			{
				return false;
			}
			return true;
		}

		public static string MakeEnumList(object value, string separator)
		{
			if (!(value is Enum))
			{
				throw new InvalidCastException("Must be enum.");
			}
			if (separator == null)
			{
				separator = ", ";
			}
			int num = 1;
			int num2 = (int)value;
			Type type = value.GetType();
			if (num2 == 0)
			{
				return Enum.GetName(type, value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (num > 0)
			{
				int num3 = num & num2;
				if (num3 != 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(separator);
					}
					stringBuilder.Append(Enum.GetName(type, num3));
				}
				num <<= 1;
			}
			return stringBuilder.ToString();
		}

		public static string MakeSystemInfoString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			try
			{
				stringBuilder.Append(Class37.smethod_0("M_Timestamp"));
				stringBuilder.Append(": ");
				stringBuilder.Append(FormatSortableDate(DateTime.UtcNow));
				stringBuilder.Append(" / ");
				stringBuilder.Append(FormatSortableDate(DateTime.Now));
				stringBuilder.Append(Environment.NewLine);
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				stringBuilder.Append(Class37.smethod_0("M_EntryAssembly"));
				stringBuilder.Append(": ");
				try
				{
					stringBuilder.Append((entryAssembly == null) ? "N/A" : entryAssembly.FullName);
				}
				catch (Exception ex)
				{
					stringBuilder.Append(ex.Message);
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_BaseDirectory"));
				stringBuilder.Append(": ");
				stringBuilder.Append(AppDomain.CurrentDomain.BaseDirectory);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_Platform"));
				stringBuilder.Append(": ");
				stringBuilder.Append(OSRecord.ThisMachine.ToString());
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_OSVersion"));
				stringBuilder.Append(": ");
				stringBuilder.Append(Environment.OSVersion.Version);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_CLRVersion"));
				stringBuilder.Append(": ");
				stringBuilder.Append(Environment.Version);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_ProcessBitness"));
				stringBuilder.Append(": ");
				stringBuilder.Append(Class21.bool_0 ? "64-bit" : "32-bit");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_Culture"));
				stringBuilder.Append(": ");
				stringBuilder.Append(CultureInfo.CurrentCulture);
				stringBuilder.Append(" / ");
				stringBuilder.Append(CultureInfo.CurrentUICulture);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_Encoding"));
				stringBuilder.Append(": ");
				stringBuilder.Append(Encoding.Default.EncodingName);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_IISAvailable"));
				stringBuilder.Append(": ");
				stringBuilder.Append(IisIsAvailable);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Class37.smethod_0("M_MachineCode"));
				stringBuilder.Append(": ");
				stringBuilder.Append(MachineProfile.Profile.Hash);
			}
			catch (Exception ex2)
			{
				stringBuilder.Append(ex2.Message);
			}
			return stringBuilder.ToString();
		}

		public static WebProxy MakeWebProxy(string address, string username, string password, string domain)
		{
			if (address == null || address.Length == 0)
			{
				throw new ArgumentNullException("address");
			}
			CredentialCache credentialCache = new CredentialCache();
			NetworkCredential cred = new NetworkCredential(username, password, domain);
			if (string.Compare(address, 0, "http://", 0, 7, ignoreCase: true) != 0 && string.Compare(address, 0, "https://", 0, 7, ignoreCase: true) != 0 && address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			Uri uriPrefix = new Uri(address);
			credentialCache.Add(uriPrefix, "Basic", cred);
			credentialCache.Add(uriPrefix, "NTLM", cred);
			credentialCache.Add(uriPrefix, "Digest", cred);
			credentialCache.Add(uriPrefix, "Kerberos", cred);
			return new WebProxy(address, BypassOnLocal: true, new string[0], credentialCache);
		}

		public static CultureInfo SelectCulture(CultureInfo culture)
		{
			if (Thread.CurrentThread.CurrentUICulture == culture)
			{
				return culture;
			}
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (culture.IsNeutralCulture)
			{
				CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
				foreach (CultureInfo cultureInfo in cultures)
				{
					if (cultureInfo.Name.StartsWith(culture.Name))
					{
						culture = cultureInfo;
						break;
					}
				}
				if (culture.IsNeutralCulture)
				{
					return currentUICulture;
				}
			}
			Thread.CurrentThread.CurrentUICulture = culture;
			return currentUICulture;
		}

		private static void smethod_4(Control control_0, ArrayList arrayList_0)
		{
			if (!arrayList_0.Contains(control_0))
			{
				if ((bool)methodInfo_1.Invoke(control_0, new object[1]
				{
					controlStyles_0
				}))
				{
					arrayList_0.Add(control_0);
					methodInfo_0.Invoke(control_0, new object[2]
					{
						controlStyles_0,
						false
					});
				}
				if (control_0.HasChildren)
				{
					foreach (Control control in control_0.Controls)
					{
						smethod_4(control, arrayList_0);
					}
				}
			}
		}

		[CompilerGenerated]
		private static void smethod_5()
		{
			try
			{
				Class21.LoadLibraryExW("NTMARTA.DLL", IntPtr.Zero, 0u);
				Marshal.GetLastWin32Error();
			}
			catch
			{
			}
		}

		public static void SplitUserName(string winUsername, out string username, out string domain)
		{
			username = null;
			domain = null;
			int num = winUsername.IndexOf('\\');
			if (num > -1)
			{
				domain = winUsername.Substring(0, num);
				username = winUsername.Substring(num + 1);
				return;
			}
			num = winUsername.IndexOf('@');
			if (num > -1)
			{
				username = winUsername.Substring(0, num);
				domain = winUsername.Substring(num + 1);
			}
			else
			{
				username = winUsername;
			}
		}

		public static IDisposable ThreadBoost(ThreadPriority priority)
		{
			return new Class39(priority);
		}

		public static string XmlDecode(string value)
		{
			if (value != null && value.Length != 0)
			{
			}
			return value;
		}

		public static string XmlEncode(string value)
		{
			if (value != null && value.Length != 0)
			{
			}
			return value;
		}
	}
}
