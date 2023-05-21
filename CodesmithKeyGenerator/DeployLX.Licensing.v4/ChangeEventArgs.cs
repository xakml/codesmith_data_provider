using System;
using System.Collections;
using System.ComponentModel;

namespace DeployLX.Licensing.v4
{
	public class ChangeEventArgs : EventArgs
	{
		private CollectionChangeAction collectionChangeAction_0;

		private object object_0;

		private object object_1;

		private Stack stack_0;

		private string string_0;

		public Stack BubbleStack => stack_0;

		public CollectionChangeAction CollectionAction
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

		public object CollectionItem
		{
			get
			{
				return object_1;
			}
			set
			{
				object_1 = value;
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

		public string Name
		{
			get
			{
				return string_0;
			}
			set
			{
				string_0 = value;
			}
		}

		public ChangeEventArgs(string name, object element)
		{
			stack_0 = new Stack();
			string_0 = name;
			object_0 = element;
		}

		public ChangeEventArgs(string name, object element, object item, CollectionChangeAction action)
		{
			stack_0 = new Stack();
			string_0 = name;
			object_0 = element;
			object_1 = item;
			collectionChangeAction_0 = action;
		}
	}
}
