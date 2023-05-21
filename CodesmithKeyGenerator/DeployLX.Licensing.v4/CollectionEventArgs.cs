using System;
using System.ComponentModel;

namespace DeployLX.Licensing.v4
{
	public class CollectionEventArgs : EventArgs
	{
		private CollectionChangeAction collectionChangeAction_0;

		private object object_0;

		public CollectionChangeAction Action
		{
			get
			{
				return collectionChangeAction_0;
			}
			set
			{
				collectionChangeAction_0 = value;
			}
		}

		public object Element
		{
			get
			{
				return object_0;
			}
			set
			{
				object_0 = value;
			}
		}

		public CollectionEventArgs()
		{
		}

		public CollectionEventArgs(CollectionChangeAction action, object element)
		{
			object_0 = element;
			collectionChangeAction_0 = action;
		}
	}
}
