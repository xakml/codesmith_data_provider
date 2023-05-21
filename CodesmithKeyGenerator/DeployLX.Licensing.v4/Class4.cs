using System;
using System.Threading;
using System.Windows.Forms;

namespace DeployLX.Licensing.v4
{
	internal abstract class Class4
	{
		private bool bool_0;

		private ManualResetEvent manualResetEvent_0 = new ManualResetEvent(initialState: false);

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public void method_0()
		{
			Thread thread = new Thread(method_2);
			thread.SetApartmentState(ApartmentState.STA);
			thread.IsBackground = true;
			thread.Name = ToString();
			thread.Start();
		}

		public void method_1()
		{
			method_0();
			manualResetEvent_0.WaitOne();
		}

		private void method_2()
		{
			try
			{
				vmethod_0();
			}
			catch (Exception ex)
			{
				if (bool_0)
				{
					throw;
				}
				try
				{
					Thread.Sleep(1000);
					vmethod_0();
				}
				catch
				{
					MessageBox.Show(ex.ToString());
				}
			}
			finally
			{
				manualResetEvent_0.Set();
			}
		}

		protected abstract void vmethod_0();
	}
}
