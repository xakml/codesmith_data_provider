using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DeployLX.Licensing.v4
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class Class28
	{
		private Class28()
		{
		}

		[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_0, [In] [Out] byte[] byte_1, IntPtr intptr_1);

		private static void smethod_0(byte[] byte_0)
		{
			if (!Class21.VirtualProtect(byte_0, new IntPtr(byte_0.Length), 64, out int _))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		[SecuritySafeCritical]
		public static bool smethod_1(byte[] byte_0)
		{
			byte[] byte_ = Class21.bool_0 ? Class27.byte_0 : Class25.byte_0;
			smethod_0(byte_);
			return CallWindowProcW(byte_, IntPtr.Zero, 0, byte_0, new IntPtr(byte_0.Length)) != IntPtr.Zero;
		}
	}
}
