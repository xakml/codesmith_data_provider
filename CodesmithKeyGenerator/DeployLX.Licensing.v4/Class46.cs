using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace DeployLX.Licensing.v4
{
	internal class Class46 : IDisposable
	{
		[CompilerGenerated]
		private sealed class Class47
		{
			public Exception exception_0;

			public MethodInvoker methodInvoker_0;

			public void method_0()
			{
				try
				{
					methodInvoker_0();
				}
				catch (Exception ex)
				{
					Exception ex2 = exception_0 = ex;
				}
			}
		}

		public IntPtr intptr_0 = IntPtr.Zero;

		public Class46()
		{
			if (Class21.bool_1)
			{
				method_0();
			}
		}

		public void Dispose()
		{
			if (Class21.bool_1)
			{
				method_1();
			}
		}

		private void method_0()
		{
			Class21.Wow64DisableWow64FsRedirection(ref intptr_0);
		}

		private void method_1()
		{
			Class21.Wow64RevertWow64FsRedirection(intptr_0);
		}

		public static void smethod_0(MethodInvoker methodInvoker_0)
		{
			Class47 @class = new Class47();
			@class.methodInvoker_0 = methodInvoker_0;
			@class.exception_0 = null;
			Class47 class2 = @class;
			new Thread(class2.method_0).Start();
			if (class2.exception_0 != null)
			{
				throw class2.exception_0;
			}
		}
	}
}
