using System;
using System.Threading;
using System.Xml;

namespace DeployLX.Licensing.v4
{
	[Serializable]
	public sealed class MachineProfileEntry : ICloneable, IChange
	{
		private string _displayName;

		private Interface1 _entryMaker;

		private int _fileMovedWeight;

		private bool _isReadOnly;

		private int _partialMatchWeight;

		private MachineProfileEntryType _type;

		private int _weight;

		private ChangeEventHandler changed;

		public string DisplayName
		{
			get
			{
				if (_displayName == null)
				{
					return _type.ToString();
				}
				return _displayName;
			}
			set
			{
				method_1();
				if (_displayName != value)
				{
					_displayName = value;
					method_0(new ChangeEventArgs("DisplayName", this));
				}
			}
		}

		public int FileMovedWeight
		{
			get
			{
				if (_fileMovedWeight != -1)
				{
					return _fileMovedWeight;
				}
				return _weight;
			}
			set
			{
				_fileMovedWeight = value;
			}
		}

		internal Interface1 Interface1_0
		{
			get
			{
				return _entryMaker;
			}
			set
			{
				method_1();
				if (_entryMaker != null)
				{
					throw new Exception("E_ReadOnlyObject");
				}
				if (_entryMaker != value)
				{
					_entryMaker = value;
					method_0(new ChangeEventArgs("EntryMaker", this));
				}
			}
		}

		public int PartialMatchWeight
		{
			get
			{
				if (_partialMatchWeight != -1)
				{
					return _partialMatchWeight;
				}
				return _weight;
			}
			set
			{
				method_1();
				if (_partialMatchWeight != value)
				{
					_partialMatchWeight = value;
					method_0(new ChangeEventArgs("PartialMatchWeight", this));
				}
			}
		}

		public MachineProfileEntryType Type => _type;

		public int Weight
		{
			get
			{
				return _weight;
			}
			set
			{
				method_1();
				if (_weight != value)
				{
					_weight = value;
					method_0(new ChangeEventArgs("Weight", this));
				}
			}
		}

		public event ChangeEventHandler Changed
		{
			add
			{
				ChangeEventHandler changeEventHandler = changed;
				ChangeEventHandler changeEventHandler2;
				do
				{
					changeEventHandler2 = changeEventHandler;
					ChangeEventHandler value2 = (ChangeEventHandler)Delegate.Combine(changeEventHandler2, value);
					changeEventHandler = Interlocked.CompareExchange(ref changed, value2, changeEventHandler2);
				}
				while (changeEventHandler != changeEventHandler2);
			}
			remove
			{
				ChangeEventHandler changeEventHandler = changed;
				ChangeEventHandler changeEventHandler2;
				do
				{
					changeEventHandler2 = changeEventHandler;
					ChangeEventHandler value2 = (ChangeEventHandler)Delegate.Remove(changeEventHandler2, value);
					changeEventHandler = Interlocked.CompareExchange(ref changed, value2, changeEventHandler2);
				}
				while (changeEventHandler != changeEventHandler2);
			}
		}

		public MachineProfileEntry()
		{
			_weight = 2;
			_partialMatchWeight = -1;
			_fileMovedWeight = -1;
		}

		public MachineProfileEntry(MachineProfileEntryType type, int weight, int partialWeight, int fileMovedWeight)
		{
			_weight = 2;
			_partialMatchWeight = -1;
			_fileMovedWeight = -1;
			_type = type;
			_weight = weight;
			_partialMatchWeight = partialWeight;
			_fileMovedWeight = fileMovedWeight;
		}

		public void MakeReadOnly()
		{
			_isReadOnly = true;
		}

		private void method_0(ChangeEventArgs changeEventArgs_0)
		{
		}

		private void method_1()
		{
			if (_isReadOnly)
			{
				throw new Exception("E_ReadOnlyObject");
			}
		}

		public bool ReadFromXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("type");
			if (attribute == null)
			{
				return false;
			}
			_type = (MachineProfileEntryType)Toolbox.FastParseInt32(attribute);
			attribute = reader.GetAttribute("weight");
			if (attribute != null)
			{
				_weight = Toolbox.FastParseInt32(attribute);
			}
			else
			{
				_weight = 1;
			}
			attribute = reader.GetAttribute("partialWeight");
			if (attribute != null)
			{
				_partialMatchWeight = Toolbox.FastParseInt32(attribute);
			}
			else
			{
				_partialMatchWeight = -1;
			}
			attribute = reader.GetAttribute("movedWeight");
			if (attribute != null)
			{
				_fileMovedWeight = Toolbox.FastParseInt32(attribute);
			}
			else
			{
				_fileMovedWeight = -1;
			}
			_displayName = reader.GetAttribute("displayName");
			attribute = reader.GetAttribute("entryMaker");
			if (attribute != null)
			{
				Type type = TypeHelper.FindType(attribute, throwOnError: true);
				_entryMaker = (Activator.CreateInstance(type) as Interface1);
			}
			reader.Read();
			return true;
		}

		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public void WriteToXml(XmlWriter writer)
		{
			writer.WriteStartElement("Entry");
			int type = (int)_type;
			writer.WriteAttributeString("type", type.ToString());
			writer.WriteAttributeString("weight", _weight.ToString());
			if (_partialMatchWeight != -1 && _partialMatchWeight != _weight)
			{
				writer.WriteAttributeString("partialWeight", _partialMatchWeight.ToString());
			}
			if (_fileMovedWeight != -1 && _fileMovedWeight != _weight)
			{
				writer.WriteAttributeString("movedWeight", _fileMovedWeight.ToString());
			}
			if (_displayName != null)
			{
				writer.WriteAttributeString("displayName", _displayName);
			}
			if (_entryMaker != null)
			{
				writer.WriteAttributeString("entryMaker", _entryMaker.GetType().AssemblyQualifiedName);
			}
			writer.WriteEndElement();
		}
	}
}
