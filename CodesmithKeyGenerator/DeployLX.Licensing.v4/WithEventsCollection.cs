using System;
using System.Collections;
using System.ComponentModel;

namespace DeployLX.Licensing.v4
{
	[Serializable]
	public abstract class WithEventsCollection : IChange, IList, ICollection, IEnumerable
	{
		[NonSerialized]
		private CollectionEventHandler _added;

		[NonSerialized]
		private CollectionEventHandler _adding;

		[NonSerialized]
		private CollectionEventHandler _changed;

		[NonSerialized]
		private CollectionEventHandler _changing;

		[NonSerialized]
		private ChangeEventHandler _ichanged;

		private bool _isReadOnly;

		private ArrayList _list;

		[NonSerialized]
		private CollectionEventHandler _removed;

		[NonSerialized]
		private CollectionEventHandler _removing;

		private int _suspendEvents;

		public virtual int Count => IList_0.Count;

		public bool EventsSuspended => _suspendEvents > 0;

		private IList IList_0 => _list;

		public bool IsReadOnly => _isReadOnly;

		protected IList List => this;

		public virtual object SyncRoot => IList_0.SyncRoot;

		bool ICollection.IsSynchronized
		{
			get
			{
				return IList_0.IsSynchronized;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return IList_0.IsFixedSize;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return IList_0[index];
			}
			set
			{
				object obj = IList_0[index];
				CollectionEventArgs collectionEventArgs_ = null;
				if (obj != value)
				{
					collectionEventArgs_ = new CollectionEventArgs(CollectionChangeAction.Remove, obj);
					OnRemoving(collectionEventArgs_);
				}
				CollectionEventArgs collectionEventArgs = new CollectionEventArgs(CollectionChangeAction.Add, value);
				OnAdding(collectionEventArgs);
				IList_0[index] = collectionEventArgs.Element;
				if (obj != value)
				{
					OnRemoved(collectionEventArgs_);
				}
				OnAdded(collectionEventArgs);
			}
		}

		public event CollectionEventHandler Added
		{
			add
			{
				lock (this)
				{
					_added = (CollectionEventHandler)Delegate.Combine(_added, value);
				}
			}
			remove
			{
				lock (this)
				{
					_added = (CollectionEventHandler)Delegate.Remove(_added, value);
				}
			}
		}

		public event CollectionEventHandler Adding
		{
			add
			{
				lock (this)
				{
					_adding = (CollectionEventHandler)Delegate.Combine(_adding, value);
				}
			}
			remove
			{
				lock (this)
				{
					_adding = (CollectionEventHandler)Delegate.Remove(_adding, value);
				}
			}
		}

		public event CollectionEventHandler Changed
		{
			add
			{
				lock (this)
				{
					_changed = (CollectionEventHandler)Delegate.Combine(_changed, value);
				}
			}
			remove
			{
				lock (this)
				{
					_changed = (CollectionEventHandler)Delegate.Remove(_changed, value);
				}
			}
		}

		public event CollectionEventHandler Changing
		{
			add
			{
				lock (this)
				{
					_changing = (CollectionEventHandler)Delegate.Combine(_changing, value);
				}
			}
			remove
			{
				lock (this)
				{
					_changing = (CollectionEventHandler)Delegate.Remove(_changing, value);
				}
			}
		}

		event ChangeEventHandler IChange.Changed
		{
			add
			{
				lock (this)
				{
					_ichanged = (ChangeEventHandler)Delegate.Combine(_ichanged, value);
				}
			}
			remove
			{
				lock (this)
				{
					_ichanged = (ChangeEventHandler)Delegate.Remove(_ichanged, value);
				}
			}
		}

		public event CollectionEventHandler Removed
		{
			add
			{
				lock (this)
				{
					_removed = (CollectionEventHandler)Delegate.Combine(_removed, value);
				}
			}
			remove
			{
				lock (this)
				{
					_removed = (CollectionEventHandler)Delegate.Remove(_removed, value);
				}
			}
		}

		public event CollectionEventHandler Removing
		{
			add
			{
				lock (this)
				{
					_removing = (CollectionEventHandler)Delegate.Combine(_removing, value);
				}
			}
			remove
			{
				lock (this)
				{
					_removing = (CollectionEventHandler)Delegate.Remove(_removing, value);
				}
			}
		}

		protected WithEventsCollection()
		{
			_list = new ArrayList();
		}

		protected WithEventsCollection(WithEventsCollection source)
		{
			_list = source._list;
			source.Added += method_5;
			source.Removed += method_4;
			source.Removing += method_3;
			source.Adding += method_2;
			source.Changing += method_1;
			source.Changed += method_0;
		}

		public virtual void Clear()
		{
			if (Count == 0)
			{
				return;
			}
			if (_removed == null && _removing == null && _changed == null && _changing == null)
			{
				IList_0.Clear();
				return;
			}
			ArrayList arrayList = new ArrayList(IList_0);
			IList_0.Clear();
			if (_removing != null || _changing != null)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					CollectionEventArgs collectionEventArgs_ = new CollectionEventArgs(CollectionChangeAction.Remove, arrayList[i]);
					OnRemoving(collectionEventArgs_);
				}
			}
			if (_removed != null || _changed != null)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					CollectionEventArgs collectionEventArgs_2 = new CollectionEventArgs(CollectionChangeAction.Remove, arrayList[j]);
					OnRemoved(collectionEventArgs_2);
				}
			}
		}

		public void MakeReadOnly()
		{
			MakeReadOnly(isReadOnly: true);
		}

		protected virtual void MakeReadOnly(bool isReadOnly)
		{
			_isReadOnly = isReadOnly;
			if (isReadOnly)
			{
				foreach (object item in _list)
				{
					(item as IChange)?.MakeReadOnly();
				}
			}
		}

		private void method_0(object sender, CollectionEventArgs e)
		{
			OnChanged(e);
		}

		private void method_1(object sender, CollectionEventArgs e)
		{
			OnChanging(e);
		}

		private void method_2(object sender, CollectionEventArgs e)
		{
			OnAdding(e);
		}

		private void method_3(object sender, CollectionEventArgs e)
		{
			OnRemoving(e);
		}

		private void method_4(object sender, CollectionEventArgs e)
		{
			OnRemoved(e);
		}

		private void method_5(object sender, CollectionEventArgs e)
		{
			OnAdded(e);
		}

		protected virtual void OnAdded(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0)
			{
				if (_added != null)
				{
					_added(this, collectionEventArgs_0);
				}
				OnChanged(collectionEventArgs_0);
			}
		}

		protected virtual void OnAdding(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0)
			{
				if (_adding != null)
				{
					_adding(this, collectionEventArgs_0);
				}
				OnChanging(collectionEventArgs_0);
			}
		}

		protected virtual void OnChanged(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0)
			{
				if (_changed != null)
				{
					_changed(this, collectionEventArgs_0);
				}
				OnIChanged(collectionEventArgs_0);
			}
		}

		protected virtual void OnChanging(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0 && _changing != null)
			{
				_changing(this, collectionEventArgs_0);
			}
		}

		protected virtual void OnIChanged(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0 && _ichanged != null)
			{
				_ichanged(this, new ChangeEventArgs(null, this, collectionEventArgs_0.Element, collectionEventArgs_0.Action));
			}
		}

		protected virtual void OnRemoved(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0)
			{
				if (_removed != null)
				{
					_removed(this, collectionEventArgs_0);
				}
				OnChanged(collectionEventArgs_0);
			}
		}

		protected virtual void OnRemoving(CollectionEventArgs collectionEventArgs_0)
		{
			if (collectionEventArgs_0.Element != null && _suspendEvents <= 0)
			{
				if (_removing != null)
				{
					_removing(this, collectionEventArgs_0);
				}
				OnChanging(collectionEventArgs_0);
			}
		}

		public virtual void RemoveAt(int index)
		{
			object obj = null;
			CollectionEventArgs collectionEventArgs = null;
			if (_removing != null || _removed != null || _changed != null || _changing != null)
			{
				obj = IList_0[index];
				collectionEventArgs = new CollectionEventArgs(CollectionChangeAction.Remove, obj);
			}
			if (collectionEventArgs != null)
			{
				OnRemoving(collectionEventArgs);
			}
			IList_0.RemoveAt(index);
			if (collectionEventArgs != null)
			{
				OnRemoved(collectionEventArgs);
			}
		}

		public void ResumeEvents()
		{
			_suspendEvents--;
			if (_suspendEvents < 0)
			{
				_suspendEvents = 0;
			}
		}

		public void SuspendEvents()
		{
			_suspendEvents++;
		}

		void ICollection.CopyTo(Array array, int index)
		{
			IList_0.CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return IList_0.GetEnumerator();
		}

		int IList.Add(object value)
		{
			if (value == null)
			{
				return -1;
			}
			int num = -1;
			CollectionEventArgs collectionEventArgs = null;
			if (_added != null || _adding != null || _changed != null || _changing != null)
			{
				collectionEventArgs = new CollectionEventArgs(CollectionChangeAction.Add, value);
			}
			if (collectionEventArgs != null)
			{
				OnAdding(collectionEventArgs);
				value = collectionEventArgs.Element;
			}
			num = IList_0.Add(value);
			if (collectionEventArgs != null)
			{
				OnAdded(collectionEventArgs);
			}
			return num;
		}

		bool IList.Contains(object value)
		{
			return IList_0.Contains(value);
		}

		int IList.IndexOf(object value)
		{
			return IList_0.IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			if (value != null)
			{
				CollectionEventArgs collectionEventArgs = null;
				if (_adding != null || _added != null || _changed != null || _changing != null)
				{
					collectionEventArgs = new CollectionEventArgs(CollectionChangeAction.Add, value);
				}
				if (collectionEventArgs != null)
				{
					OnAdding(collectionEventArgs);
				}
				IList_0.Insert(index, value);
				if (collectionEventArgs != null)
				{
					OnAdded(collectionEventArgs);
				}
			}
		}

		void IList.Remove(object value)
		{
			if (value != null)
			{
				CollectionEventArgs collectionEventArgs = null;
				if (_removing != null || _removed != null || _changed != null || _changing != null)
				{
					collectionEventArgs = new CollectionEventArgs(CollectionChangeAction.Remove, value);
				}
				if (collectionEventArgs != null)
				{
					OnRemoving(collectionEventArgs);
					value = collectionEventArgs.Element;
				}
				IList_0.Remove(value);
				if (collectionEventArgs != null)
				{
					OnRemoved(collectionEventArgs);
				}
			}
		}
	}
}
