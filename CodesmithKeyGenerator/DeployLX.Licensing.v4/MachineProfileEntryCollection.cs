using System;
using System.Collections;

namespace DeployLX.Licensing.v4
{
	[Serializable]
	public sealed class MachineProfileEntryCollection : WithEventsCollection
	{
		public MachineProfileEntry this[int index]
		{
			get
			{
				return base.List[index] as MachineProfileEntry;
			}
			set
			{
				base.List[index] = value;
			}
		}

		public int Add(MachineProfileEntry item)
		{
			return base.List.Add(item);
		}

		public void AddRange(MachineProfileEntry[] items)
		{
			if (items != null)
			{
				foreach (MachineProfileEntry item in items)
				{
					Add(item);
				}
			}
		}

		public void AddRange(MachineProfileEntryCollection items)
		{
			if (items != null)
			{
				foreach (MachineProfileEntry item in (IEnumerable)items)
				{
					Add(item);
				}
			}
		}

		public bool Contains(MachineProfileEntry item)
		{
			return base.List.Contains(item);
		}

		public void CopyTo(MachineProfileEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		public int IndexOf(MachineProfileEntry item)
		{
			return base.List.IndexOf(item);
		}

		public void Insert(int index, MachineProfileEntry item)
		{
			base.List.Insert(index, item);
		}

		protected override void MakeReadOnly(bool isReadOnly)
		{
			base.MakeReadOnly(isReadOnly);
		}

		public void Remove(MachineProfileEntry item)
		{
			base.List.Remove(item);
		}
	}
}
