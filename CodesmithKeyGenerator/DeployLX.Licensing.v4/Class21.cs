using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace DeployLX.Licensing.v4
{
	[SecuritySafeCritical]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class Class21
	{
		public class Class22 : IDisposable
		{
			private GCHandle gchandle_0;

			private GCHandle gchandle_1;

			private Struct38 struct38_0 = default(Struct38);

			public Class22(string value)
			{
				struct38_0.ushort_0 = (ushort)(value.Length * 2);
				struct38_0.ushort_1 = struct38_0.ushort_0;
				gchandle_0 = GCHandle.Alloc(value, GCHandleType.Pinned);
				struct38_0.intptr_0 = gchandle_0.AddrOfPinnedObject();
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
				method_1();
			}

			~Class22()
			{
				method_1();
			}

			public IntPtr method_0()
			{
				if (!gchandle_1.IsAllocated)
				{
					gchandle_1 = GCHandle.Alloc(struct38_0, GCHandleType.Pinned);
				}
				return gchandle_1.AddrOfPinnedObject();
			}

			public void method_1()
			{
				if (gchandle_1.IsAllocated)
				{
					gchandle_1.Free();
				}
				if (gchandle_0.IsAllocated)
				{
					gchandle_0.Free();
				}
			}
		}

		public class Class23 : IDisposable
		{
			private Class22 class22_0;

			private GCHandle gchandle_0;

			private IntPtr intptr_0;

			private Struct39 struct39_0 = default(Struct39);

			public void Dispose()
			{
				GC.SuppressFinalize(this);
				method_2();
			}

			~Class23()
			{
				method_2();
			}

			public void method_0(string string_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2)
			{
				method_2();
				struct39_0.uint_0 = (uint)Marshal.SizeOf(struct39_0);
				struct39_0.intptr_0 = intptr_1;
				struct39_0.uint_1 = uint_0;
				class22_0 = new Class22(string_0);
				struct39_0.intptr_1 = class22_0.method_0();
				if (intptr_2 == new IntPtr(-1))
				{
					ConvertStringSecurityDescriptorToSecurityDescriptor($"O:{smethod_1()}G:S-1-3-1D:(A;;GA;;;S-1-1-0)", 1, out intptr_0, out ulong _);
					intptr_2 = intptr_0;
				}
				struct39_0.intptr_2 = intptr_2;
				struct39_0.intptr_3 = IntPtr.Zero;
			}

			public IntPtr method_1()
			{
				gchandle_0 = GCHandle.Alloc(struct39_0, GCHandleType.Pinned);
				return gchandle_0.AddrOfPinnedObject();
			}

			public void method_2()
			{
				if (gchandle_0.IsAllocated)
				{
					gchandle_0.Free();
				}
				if (class22_0 != null)
				{
					class22_0.Dispose();
					class22_0 = null;
				}
				if (intptr_0 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intptr_0);
					intptr_0 = IntPtr.Zero;
				}
			}
		}

		public struct Struct36
		{
			public int int_0;

			public int int_1;

			public int int_2;

			public Struct37 struct37_0;
		}

		public struct Struct37
		{
			public IntPtr intptr_0;

			public int int_0;

			public int int_1;

			public int int_2;

			public IntPtr intptr_1;
		}

		public struct Struct38
		{
			public ushort ushort_0;

			public ushort ushort_1;

			public IntPtr intptr_0;
		}

		public struct Struct39
		{
			public uint uint_0;

			public IntPtr intptr_0;

			public IntPtr intptr_1;

			public uint uint_1;

			public IntPtr intptr_2;

			public IntPtr intptr_3;
		}

		public static bool bool_0 = IntPtr.Size == 8;

		public static bool bool_1 = smethod_0();

		private Class21()
		{
		}

		[DllImport("kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr intptr_0);

		[DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int CM_Locate_DevNode(out IntPtr intptr_0, string string_0, int int_0);

		[DllImport("advapi32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ConvertSidToStringSid(IntPtr intptr_0, ref IntPtr intptr_1);

		[DllImport("advapi32", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor([In] [MarshalAs(UnmanagedType.LPTStr)] string string_0, int int_0, out IntPtr intptr_0, out ulong ulong_0);

		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ConvertStringSidToSid([MarshalAs(UnmanagedType.LPTStr)] string string_0, ref IntPtr intptr_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateFile(string string_0, uint uint_0, uint uint_1, IntPtr intptr_0, uint uint_2, uint uint_3, IntPtr intptr_1);

		[DllImport("gdi32")]
		public static extern IntPtr CreatePatternBrush(IntPtr intptr_0);

		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteAce(IntPtr intptr_0, int int_0);

		[DllImport("gdi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject(IntPtr intptr_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeviceIoControl(IntPtr intptr_0, int int_0, IntPtr intptr_1, int int_1, [Out] byte[] byte_0, int int_2, ref int int_3, IntPtr intptr_2);

		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DuplicateToken(IntPtr intptr_0, int int_0, ref IntPtr intptr_1);

		[DllImport("user32")]
		public static extern int FillRect(IntPtr intptr_0, byte[] byte_0, IntPtr intptr_1);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int FreeLibrary(IntPtr intptr_0);

		[DllImport("iphlpapi", CharSet = CharSet.Unicode)]
		public static extern int GetAdaptersInfo([Out] byte[] byte_0, ref uint uint_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetDiskFreeSpaceEx([In] string string_0, out long long_0, out long long_1, out long long_2);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetDriveType(string string_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string string_0);

		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetNamedSecurityInfo(string string_0, int int_0, int int_1, [Out] IntPtr intptr_0, [Out] IntPtr intptr_1, out IntPtr intptr_2, [Out] IntPtr intptr_3, out IntPtr intptr_4);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr intptr_0, string string_0);

		[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetSystemMetrics(int int_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetSystemPowerStatus(byte[] byte_0);

		[DllImport("gdi32", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetTextExtentPoint32(IntPtr intptr_0, string string_0, int int_0, [MarshalAs(UnmanagedType.LPArray, SizeConst = 8, SizeParamIndex = 0)] byte[] byte_0);

		[DllImport("advapi32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetTokenInformation(IntPtr intptr_0, int int_0, IntPtr intptr_1, int int_1, ref int int_2);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetVersionEx(IntPtr intptr_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetVolumeInformation([In] string string_0, [Out] StringBuilder stringBuilder_0, int int_0, out int int_1, out int int_2, out int int_3, [Out] StringBuilder stringBuilder_1, int int_4);

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern void GlobalMemoryStatus(byte[] byte_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GlobalMemoryStatusEx(byte[] byte_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibraryExW([MarshalAs(UnmanagedType.LPWStr)] string string_0, IntPtr intptr_0, uint uint_0);

		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool LogonUser([MarshalAs(UnmanagedType.LPTStr)] string string_0, [MarshalAs(UnmanagedType.LPTStr)] string string_1, [MarshalAs(UnmanagedType.LPTStr)] string string_2, int int_0, int int_1, ref IntPtr intptr_0);

		[DllImport("user32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool MessageBeep(int int_0);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtCreateKey([In] [Out] ref IntPtr intptr_0, uint uint_0, IntPtr intptr_1, int int_0, IntPtr intptr_2, int int_1, [In] [Out] ref int int_2);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtDeleteKey(IntPtr intptr_0);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtDeleteValueKey(IntPtr intptr_0, IntPtr intptr_1);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtEnumerateValueKey(IntPtr intptr_0, int int_0, int int_1, [Out] byte[] byte_0, int int_2, out int int_3);

		[DllImport("ntdll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint NtFsControlFile(IntPtr intptr_0, IntPtr intptr_1, IntPtr intptr_2, IntPtr intptr_3, [Out] byte[] byte_0, uint uint_0, [In] [Out] byte[] byte_1, uint uint_1, [Out] ulong[] ulong_0, uint uint_2);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtOpenKey(out IntPtr intptr_0, uint uint_0, IntPtr intptr_1);

		[DllImport("ntdll")]
		public static extern int NtQuerySystemInformation(int int_0, byte[] byte_0, int int_1, out int int_2);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtQueryValueKey(IntPtr intptr_0, IntPtr intptr_1, int int_0, [Out] byte[] byte_0, int int_1, out int int_2);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtSetSecurityObject(IntPtr intptr_0, int int_0, IntPtr intptr_1);

		[DllImport("ntdll", CharSet = CharSet.Unicode)]
		public static extern int NtSetValueKey(IntPtr intptr_0, IntPtr intptr_1, int int_0, int int_1, byte[] byte_0, int int_2);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int QueryDosDevice(string string_0, StringBuilder stringBuilder_0, int int_0);

		[DllImport("gdi32")]
		public static extern IntPtr SelectObject(IntPtr intptr_0, IntPtr intptr_1);

		[DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);

		[DllImport("gdi32")]
		public static extern int SetBkMode(IntPtr intptr_0, int int_0);

		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int SetEntriesInAcl(int int_0, [In] [Out] ref Struct36 struct36_0, IntPtr intptr_0, out IntPtr intptr_1);

		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int SetNamedSecurityInfo(string string_0, int int_0, uint uint_0, IntPtr intptr_0, IntPtr intptr_1, IntPtr intptr_2, IntPtr intptr_3);

		[DllImport("gdi32")]
		public static extern int SetTextColor(IntPtr intptr_0, int int_0);

		[DllImport("shell32", CharSet = CharSet.Unicode)]
		public static extern int SHGetFolderPath(IntPtr intptr_0, int int_0, IntPtr intptr_1, int int_1, StringBuilder stringBuilder_0);

		private static bool smethod_0()
		{
			return Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432") != null;
		}

		public static string smethod_1()
		{
			byte[] array = new byte[256];
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				int int_ = 0;
				if (!GetTokenInformation(current.Token, 1, gCHandle.AddrOfPinnedObject(), array.Length, ref int_))
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				IntPtr intptr_ = IntPtr.Zero;
				try
				{
					ConvertSidToStringSid(Marshal.ReadIntPtr(gCHandle.AddrOfPinnedObject()), ref intptr_);
					return Marshal.PtrToStringAnsi(intptr_);
				}
				finally
				{
					if (intptr_ != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intptr_);
					}
				}
			}
			finally
			{
				gCHandle.Free();
			}
		}

		[DllImport("gdi32", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool TextOut(IntPtr intptr_0, int int_0, int int_1, string string_0, int int_2);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_0, out int int_1);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr intptr_0);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool Wow64RevertWow64FsRedirection(IntPtr intptr_0);
	}
}
