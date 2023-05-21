using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DeployLXKeyGenerator
{
	public class MetadataReader
	{
		public enum Types
		{
			Module = 0,
			TypeRef = 1,
			TypeDef = 2,
			FieldPtr = 3,
			Field = 4,
			MethodPtr = 5,
			Method = 6,
			ParamPtr = 7,
			Param = 8,
			InterfaceImpl = 9,
			MemberRef = 10,
			Constant = 11,
			CustomAttribute = 12,
			FieldMarshal = 13,
			Permission = 14,
			ClassLayout = 0xF,
			FieldLayout = 0x10,
			StandAloneSig = 17,
			EventMap = 18,
			EventPtr = 19,
			Event = 20,
			PropertyMap = 21,
			PropertyPtr = 22,
			Property = 23,
			MethodSemantics = 24,
			MethodImpl = 25,
			ModuleRef = 26,
			TypeSpec = 27,
			ImplMap = 28,
			FieldRVA = 29,
			ENCLog = 30,
			ENCMap = 0x1F,
			Assembly = 0x20,
			AssemblyProcessor = 33,
			AssemblyOS = 34,
			AssemblyRef = 35,
			AssemblyRefProcessor = 36,
			AssemblyRefOS = 37,
			File = 38,
			ExportedType = 39,
			ManifestResource = 40,
			NestedClass = 41,
			GenericParam = 42,
			MethodSpec = 43,
			GenericParamConstraint = 44,
			TypeDefOrRef = 0x40,
			HasConstant = 65,
			CustomAttributeType = 66,
			HasSemantic = 67,
			ResolutionScope = 68,
			HasFieldMarshal = 69,
			HasDeclSecurity = 70,
			MemberRefParent = 71,
			MethodDefOrRef = 72,
			MemberForwarded = 73,
			Implementation = 74,
			HasCustomAttribute = 75,
			TypeOrMethodDef = 76,
			UInt16 = 97,
			UInt32 = 99,
			String = 101,
			Blob = 102,
			Guid = 103,
			UserString = 112
		}

		public struct IMAGE_DOS_HEADER
		{
			public short e_magic;

			public short e_cblp;

			public short e_cp;

			public short e_crlc;

			public short e_cparhdr;

			public short e_minalloc;

			public short e_maxalloc;

			public short e_ss;

			public short e_sp;

			public short e_csum;

			public short e_ip;

			public short e_cs;

			public short e_lfarlc;

			public short e_ovno;

			private unsafe fixed short e_res1[4];

			public short e_oeminfo;

			private unsafe fixed short e_res2[10];

			public int e_lfanew;
		}

		public struct IMAGE_NT_HEADERS
		{
			public int Signature;

			public IMAGE_FILE_HEADER ifh;

			public IMAGE_OPTIONAL_HEADER ioh;
		}

		public struct IMAGE_DATA_DIRECTORY
		{
			public int RVA;

			public int Size;
		}

		public struct IMAGE_FILE_HEADER
		{
			public ushort Machine;

			public short NumberOfSections;

			public int TimeDateStamp;

			public int PointerToSymbolTable;

			public int NumberOfSymbols;

			public short SizeOfOptionalHeader;

			public ushort Characteristics;
		}

		public struct IMAGE_OPTIONAL_HEADER
		{
			public short Magic;

			public byte MajorLinkerVersion;

			public byte MinorLinkerVersion;

			public int SizeOfCode;

			public int SizeOfInitializedData;

			public int SizeOfUninitializedData;

			public int AddressOfEntryPoint;

			public int BaseOfCode;

			public int BaseOfData;

			public int ImageBase;

			public int SectionAlignment;

			public int FileAlignment;

			public short MajorOperatingSystemVersion;

			public short MinorOperatingSystemVersion;

			public short MajorImageVersion;

			public short MinorImageVersion;

			public short MajorSubsystemVersion;

			public short MinorSubsystemVersion;

			public int Win32VersionValue;

			public int SizeOfImage;

			public int SizeOfHeaders;

			public int CheckSum;

			public ushort Subsystem;

			public short DllCharacteristics;

			public int SizeOfStackReserve;

			public int SizeOfStackCommit;

			public int SizeOfHeapReserve;

			public int SizeOfHeapCommit;

			public int LoaderFlags;

			public int NumberOfRvaAndSizes;

			public IMAGE_DATA_DIRECTORY ExportDirectory;

			public IMAGE_DATA_DIRECTORY ImportDirectory;

			public IMAGE_DATA_DIRECTORY ResourceDirectory;

			public IMAGE_DATA_DIRECTORY ExceptionDirectory;

			public IMAGE_DATA_DIRECTORY SecurityDirectory;

			public IMAGE_DATA_DIRECTORY RelocationDirectory;

			public IMAGE_DATA_DIRECTORY DebugDirectory;

			public IMAGE_DATA_DIRECTORY ArchitectureDirectory;

			public IMAGE_DATA_DIRECTORY Reserved;

			public IMAGE_DATA_DIRECTORY TLSDirectory;

			public IMAGE_DATA_DIRECTORY ConfigurationDirectory;

			public IMAGE_DATA_DIRECTORY BoundImportDirectory;

			public IMAGE_DATA_DIRECTORY ImportAddressTableDirectory;

			public IMAGE_DATA_DIRECTORY DelayImportDirectory;

			public IMAGE_DATA_DIRECTORY MetaDataDirectory;
		}

		public struct image_section_header
		{
			public unsafe fixed byte name[8];

			public int virtual_size;

			public int virtual_address;

			public int size_of_raw_data;

			public int pointer_to_raw_data;

			public int pointer_to_relocations;

			public int pointer_to_linenumbers;

			public short number_of_relocations;

			public short number_of_linenumbers;

			public int characteristics;
		}

		public struct NETDirectory
		{
			public int cb;

			public short MajorRuntimeVersion;

			public short MinorRuntimeVersion;

			public int MetaDataRVA;

			public int MetaDataSize;

			public int Flags;

			public int EntryPointToken;

			public int ResourceRVA;

			public int ResourceSize;

			public int StrongNameSignatureRVA;

			public int StrongNameSignatureSize;

			public int CodeManagerTableRVA;

			public int CodeManagerTableSize;

			public int VTableFixupsRVA;

			public int VTableFixupsSize;

			public int ExportAddressTableJumpsRVA;

			public int ExportAddressTableJumpsSize;

			public int ManagedNativeHeaderRVA;

			public int ManagedNativeHeaderSize;
		}

		public struct MetaDataHeader
		{
			public int Signature;

			public short MajorVersion;

			public short MinorVersion;

			public int Reserved;

			public int VersionLenght;

			public byte[] VersionString;

			public short Flags;

			public short NumberOfStreams;
		}

		public struct TableHeader
		{
			public int Reserved_1;

			public byte MajorVersion;

			public byte MinorVersion;

			public byte HeapOffsetSizes;

			public byte Reserved_2;

			public long MaskValid;

			public long MaskSorted;
		}

		public struct MetaDataStream
		{
			public int Offset;

			public int Size;

			public string Name;
		}

		public struct TableInfo
		{
			public string Name;

			public string[] names;

			public Types type;

			public Types[] ctypes;
		}

		public struct RefTableInfo
		{
			public Types type;

			public Types[] reftypes;

			public int[] refindex;
		}

		public struct TableSize
		{
			public int TotalSize;

			public int[] Sizes;
		}

		public struct Table
		{
			public long[][] members;
		}

		[Flags]
		public enum ILMethodHeader : byte
		{
			CorILMethod_FatFormat = 0x3,
			CorILMethod_TinyFormat = 0x2,
			CorILMethod_MoreSects = 0x8,
			CorILMethod_InitLocals = 0x10
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct COR_ILMETHOD_SECT_EH_CLAUSE_FAT
		{
			public uint Flags;

			public uint TryOffset;

			public uint TryLength;

			public uint HandlerOffset;

			public uint HandlerLength;

			public uint TokenOrOffset;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct COR_ILMETHOD_SECT_EH_CLAUSE_SMALL
		{
			public ushort Flags;

			public ushort TryOffset;

			public byte TryLength;

			public ushort HandlerOffset;

			public byte HandlerLength;

			public uint TokenOrOffset;
		}

		public const int CorILMethod_Sect_FatFormat = 64;

		public const int SECT_EH_HEADER_SIZE = 4;

		public IMAGE_DOS_HEADER idh;

		public IMAGE_NT_HEADERS inh;

		public image_section_header[] sections;

		public NETDirectory netdir;

		public byte[] StrongName;

		public MetaDataHeader mh;

		public MetaDataStream[] streams;

		public MetaDataStream MetadataRoot;

		public byte[] TablesBytes;

		public byte[] Strings;

		public byte[] US;

		public byte[] GUID;

		public byte[] Blob;

		public byte[] rsrcsection;

		public long TablesOffset;

		public TableHeader tableheader;

		public int[] TableLengths;

		public TableInfo[] tablesinfo;

		public RefTableInfo[] reftables;

		public TableSize[] tablesize;

		public int[] codedTokenBits;

		public Table[] tables;

		public long BlobOffset;

		public long StringOffset;

		public long tablestart;

		private int position = 0;

		public void InitTablesInfo()
		{
			tablesinfo = new TableInfo[45];
			tablesinfo[0].Name = "Module";
			tablesinfo[0].names = new string[5]
			{
				"Generation",
				"Name",
				"Mvid",
				"EncId",
				"EncBaseId"
			};
			tablesinfo[0].type = Types.Module;
			tablesinfo[0].ctypes = new Types[5]
			{
				Types.UInt16,
				Types.String,
				Types.Guid,
				Types.Guid,
				Types.Guid
			};
			tablesinfo[1].Name = "TypeRef";
			tablesinfo[1].names = new string[3]
			{
				"ResolutionScope",
				"Name",
				"Namespace"
			};
			tablesinfo[1].type = Types.TypeRef;
			tablesinfo[1].ctypes = new Types[3]
			{
				Types.ResolutionScope,
				Types.String,
				Types.String
			};
			tablesinfo[2].Name = "TypeDef";
			tablesinfo[2].names = new string[6]
			{
				"Flags",
				"Name",
				"Namespace",
				"Extends",
				"FieldList",
				"MethodList"
			};
			tablesinfo[2].type = Types.TypeDef;
			tablesinfo[2].ctypes = new Types[6]
			{
				Types.UInt32,
				Types.String,
				Types.String,
				Types.TypeDefOrRef,
				Types.Field,
				Types.Method
			};
			tablesinfo[3].Name = "FieldPtr";
			tablesinfo[3].names = new string[1]
			{
				"Field"
			};
			tablesinfo[3].type = Types.FieldPtr;
			tablesinfo[3].ctypes = new Types[1]
			{
				Types.Field
			};
			tablesinfo[4].Name = "Field";
			tablesinfo[4].names = new string[3]
			{
				"Flags",
				"Name",
				"Signature"
			};
			tablesinfo[4].type = Types.Field;
			tablesinfo[4].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.String,
				Types.Blob
			};
			tablesinfo[5].Name = "MethodPtr";
			tablesinfo[5].names = new string[1]
			{
				"Method"
			};
			tablesinfo[5].type = Types.MethodPtr;
			tablesinfo[5].ctypes = new Types[1]
			{
				Types.Method
			};
			tablesinfo[6].Name = "Method";
			tablesinfo[6].names = new string[6]
			{
				"RVA",
				"ImplFlags",
				"Flags",
				"Name",
				"Signature",
				"ParamList"
			};
			tablesinfo[6].type = Types.Method;
			tablesinfo[6].ctypes = new Types[6]
			{
				Types.UInt32,
				Types.UInt16,
				Types.UInt16,
				Types.String,
				Types.Blob,
				Types.Param
			};
			tablesinfo[7].Name = "ParamPtr";
			tablesinfo[7].names = new string[1]
			{
				"Param"
			};
			tablesinfo[7].type = Types.ParamPtr;
			tablesinfo[7].ctypes = new Types[1]
			{
				Types.Param
			};
			tablesinfo[8].Name = "Param";
			tablesinfo[8].names = new string[3]
			{
				"Flags",
				"Sequence",
				"Name"
			};
			tablesinfo[8].type = Types.Param;
			tablesinfo[8].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.UInt16,
				Types.String
			};
			tablesinfo[9].Name = "InterfaceImpl";
			tablesinfo[9].names = new string[2]
			{
				"Class",
				"Interface"
			};
			tablesinfo[9].type = Types.InterfaceImpl;
			tablesinfo[9].ctypes = new Types[2]
			{
				Types.TypeDef,
				Types.TypeDefOrRef
			};
			tablesinfo[10].Name = "MemberRef";
			tablesinfo[10].names = new string[3]
			{
				"Class",
				"Name",
				"Signature"
			};
			tablesinfo[10].type = Types.MemberRef;
			tablesinfo[10].ctypes = new Types[3]
			{
				Types.MemberRefParent,
				Types.String,
				Types.Blob
			};
			tablesinfo[11].Name = "Constant";
			tablesinfo[11].names = new string[3]
			{
				"Type",
				"Parent",
				"Value"
			};
			tablesinfo[11].type = Types.Constant;
			tablesinfo[11].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.HasConstant,
				Types.Blob
			};
			tablesinfo[12].Name = "CustomAttribute";
			tablesinfo[12].names = new string[3]
			{
				"Type",
				"Parent",
				"Value"
			};
			tablesinfo[12].type = Types.CustomAttribute;
			tablesinfo[12].ctypes = new Types[3]
			{
				Types.HasCustomAttribute,
				Types.CustomAttributeType,
				Types.Blob
			};
			tablesinfo[13].Name = "FieldMarshal";
			tablesinfo[13].names = new string[2]
			{
				"Parent",
				"Native"
			};
			tablesinfo[13].type = Types.FieldMarshal;
			tablesinfo[13].ctypes = new Types[2]
			{
				Types.HasFieldMarshal,
				Types.Blob
			};
			tablesinfo[14].Name = "Permission";
			tablesinfo[14].names = new string[3]
			{
				"Action",
				"Parent",
				"PermissionSet"
			};
			tablesinfo[14].type = Types.Permission;
			tablesinfo[14].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.HasDeclSecurity,
				Types.Blob
			};
			tablesinfo[15].Name = "ClassLayout";
			tablesinfo[15].names = new string[3]
			{
				"PackingSize",
				"ClassSize",
				"Parent"
			};
			tablesinfo[15].type = Types.ClassLayout;
			tablesinfo[15].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.UInt32,
				Types.TypeDef
			};
			tablesinfo[16].Name = "FieldLayout";
			tablesinfo[16].names = new string[2]
			{
				"Offset",
				"Field"
			};
			tablesinfo[16].type = Types.FieldLayout;
			tablesinfo[16].ctypes = new Types[2]
			{
				Types.UInt32,
				Types.Field
			};
			tablesinfo[17].Name = "StandAloneSig";
			tablesinfo[17].names = new string[1]
			{
				"Signature"
			};
			tablesinfo[17].type = Types.StandAloneSig;
			tablesinfo[17].ctypes = new Types[1]
			{
				Types.Blob
			};
			tablesinfo[18].Name = "EventMap";
			tablesinfo[18].names = new string[2]
			{
				"Parent",
				"EventList"
			};
			tablesinfo[18].type = Types.EventMap;
			tablesinfo[18].ctypes = new Types[2]
			{
				Types.TypeDef,
				Types.Event
			};
			tablesinfo[19].Name = "EventPtr";
			tablesinfo[19].names = new string[1]
			{
				"Event"
			};
			tablesinfo[19].type = Types.EventPtr;
			tablesinfo[19].ctypes = new Types[1]
			{
				Types.Event
			};
			tablesinfo[20].Name = "Event";
			tablesinfo[20].names = new string[3]
			{
				"EventFlags",
				"Name",
				"EventType"
			};
			tablesinfo[20].type = Types.Event;
			tablesinfo[20].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.String,
				Types.TypeDefOrRef
			};
			tablesinfo[21].Name = "PropertyMap";
			tablesinfo[21].names = new string[2]
			{
				"Parent",
				"PropertyList"
			};
			tablesinfo[21].type = Types.PropertyMap;
			tablesinfo[21].ctypes = new Types[2]
			{
				Types.TypeDef,
				Types.Property
			};
			tablesinfo[22].Name = "PropertyPtr";
			tablesinfo[22].names = new string[1]
			{
				"Property"
			};
			tablesinfo[22].type = Types.PropertyPtr;
			tablesinfo[22].ctypes = new Types[1]
			{
				Types.Property
			};
			tablesinfo[23].Name = "Property";
			tablesinfo[23].names = new string[3]
			{
				"PropFlags",
				"Name",
				"Type"
			};
			tablesinfo[23].type = Types.Property;
			tablesinfo[23].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.String,
				Types.Blob
			};
			tablesinfo[24].Name = "MethodSemantics";
			tablesinfo[24].names = new string[3]
			{
				"Semantic",
				"Method",
				"Association"
			};
			tablesinfo[24].type = Types.MethodSemantics;
			tablesinfo[24].ctypes = new Types[3]
			{
				Types.UInt16,
				Types.Method,
				Types.HasSemantic
			};
			tablesinfo[25].Name = "MethodImpl";
			tablesinfo[25].names = new string[3]
			{
				"Class",
				"MethodBody",
				"MethodDeclaration"
			};
			tablesinfo[25].type = Types.MethodImpl;
			tablesinfo[25].ctypes = new Types[3]
			{
				Types.TypeDef,
				Types.MethodDefOrRef,
				Types.MethodDefOrRef
			};
			tablesinfo[26].Name = "ModuleRef";
			tablesinfo[26].names = new string[1]
			{
				"Name"
			};
			tablesinfo[26].type = Types.ModuleRef;
			tablesinfo[26].ctypes = new Types[1]
			{
				Types.String
			};
			tablesinfo[27].Name = "TypeSpec";
			tablesinfo[27].names = new string[1]
			{
				"Signature"
			};
			tablesinfo[27].type = Types.TypeSpec;
			tablesinfo[27].ctypes = new Types[1]
			{
				Types.Blob
			};
			tablesinfo[28].Name = "ImplMap";
			tablesinfo[28].names = new string[4]
			{
				"MappingFlags",
				"MemberForwarded",
				"ImportName",
				"ImportScope"
			};
			tablesinfo[28].type = Types.ImplMap;
			tablesinfo[28].ctypes = new Types[4]
			{
				Types.UInt16,
				Types.MemberForwarded,
				Types.String,
				Types.ModuleRef
			};
			tablesinfo[29].Name = "FieldRVA";
			tablesinfo[29].names = new string[2]
			{
				"RVA",
				"Field"
			};
			tablesinfo[29].type = Types.FieldRVA;
			tablesinfo[29].ctypes = new Types[2]
			{
				Types.UInt32,
				Types.Field
			};
			tablesinfo[30].Name = "ENCLog";
			tablesinfo[30].names = new string[2]
			{
				"Token",
				"FuncCode"
			};
			tablesinfo[30].type = Types.ENCLog;
			tablesinfo[30].ctypes = new Types[2]
			{
				Types.UInt32,
				Types.UInt32
			};
			tablesinfo[31].Name = "ENCMap";
			tablesinfo[31].names = new string[1]
			{
				"Token"
			};
			tablesinfo[31].type = Types.ENCMap;
			tablesinfo[31].ctypes = new Types[1]
			{
				Types.UInt32
			};
			tablesinfo[32].Name = "Assembly";
			tablesinfo[32].names = new string[9]
			{
				"HashAlgId",
				"MajorVersion",
				"MinorVersion",
				"BuildNumber",
				"RevisionNumber",
				"Flags",
				"PublicKey",
				"Name",
				"Locale"
			};
			tablesinfo[32].type = Types.Assembly;
			tablesinfo[32].ctypes = new Types[9]
			{
				Types.UInt32,
				Types.UInt16,
				Types.UInt16,
				Types.UInt16,
				Types.UInt16,
				Types.UInt32,
				Types.Blob,
				Types.String,
				Types.String
			};
			tablesinfo[33].Name = "AssemblyProcessor";
			tablesinfo[33].names = new string[1]
			{
				"Processor"
			};
			tablesinfo[33].type = Types.AssemblyProcessor;
			tablesinfo[33].ctypes = new Types[1]
			{
				Types.UInt32
			};
			tablesinfo[34].Name = "AssemblyOS";
			tablesinfo[34].names = new string[3]
			{
				"OSPlatformId",
				"OSMajorVersion",
				"OSMinorVersion"
			};
			tablesinfo[34].type = Types.AssemblyOS;
			tablesinfo[34].ctypes = new Types[3]
			{
				Types.UInt32,
				Types.UInt32,
				Types.UInt32
			};
			tablesinfo[35].Name = "AssemblyRef";
			tablesinfo[35].names = new string[9]
			{
				"MajorVersion",
				"MinorVersion",
				"BuildNumber",
				"RevisionNumber",
				"Flags",
				"PublicKeyOrToken",
				"Name",
				"Locale",
				"HashValue"
			};
			tablesinfo[35].type = Types.AssemblyRef;
			tablesinfo[35].ctypes = new Types[9]
			{
				Types.UInt16,
				Types.UInt16,
				Types.UInt16,
				Types.UInt16,
				Types.UInt32,
				Types.Blob,
				Types.String,
				Types.String,
				Types.Blob
			};
			tablesinfo[36].Name = "AssemblyRefProcessor";
			tablesinfo[36].names = new string[2]
			{
				"Processor",
				"AssemblyRef"
			};
			tablesinfo[36].type = Types.AssemblyRefProcessor;
			tablesinfo[36].ctypes = new Types[2]
			{
				Types.UInt32,
				Types.AssemblyRef
			};
			tablesinfo[37].Name = "AssemblyRefOS";
			tablesinfo[37].names = new string[4]
			{
				"OSPlatformId",
				"OSMajorVersion",
				"OSMinorVersion",
				"AssemblyRef"
			};
			tablesinfo[37].type = Types.AssemblyRefOS;
			tablesinfo[37].ctypes = new Types[4]
			{
				Types.UInt32,
				Types.UInt32,
				Types.UInt32,
				Types.AssemblyRef
			};
			tablesinfo[38].Name = "File";
			tablesinfo[38].names = new string[3]
			{
				"Flags",
				"Name",
				"HashValue"
			};
			tablesinfo[38].type = Types.File;
			tablesinfo[38].ctypes = new Types[3]
			{
				Types.UInt32,
				Types.String,
				Types.Blob
			};
			tablesinfo[39].Name = "ExportedType";
			tablesinfo[39].names = new string[5]
			{
				"Flags",
				"TypeDefId",
				"TypeName",
				"TypeNamespace",
				"TypeImplementation"
			};
			tablesinfo[39].type = Types.ExportedType;
			tablesinfo[39].ctypes = new Types[5]
			{
				Types.UInt32,
				Types.UInt32,
				Types.String,
				Types.String,
				Types.Implementation
			};
			tablesinfo[40].Name = "ManifestResource";
			tablesinfo[40].names = new string[4]
			{
				"Offset",
				"Flags",
				"Name",
				"Implementation"
			};
			tablesinfo[40].type = Types.ManifestResource;
			tablesinfo[40].ctypes = new Types[4]
			{
				Types.UInt32,
				Types.UInt32,
				Types.String,
				Types.Implementation
			};
			tablesinfo[41].Name = "NestedClass";
			tablesinfo[41].names = new string[2]
			{
				"NestedClass",
				"EnclosingClass"
			};
			tablesinfo[41].type = Types.NestedClass;
			tablesinfo[41].ctypes = new Types[2]
			{
				Types.TypeDef,
				Types.TypeDef
			};
			tablesinfo[42].Name = "GenericParam";
			tablesinfo[42].names = new string[4]
			{
				"Number",
				"Flags",
				"Owner",
				"Name"
			};
			tablesinfo[42].type = Types.GenericParam;
			tablesinfo[42].ctypes = new Types[4]
			{
				Types.UInt16,
				Types.UInt16,
				Types.TypeOrMethodDef,
				Types.String
			};
			tablesinfo[43].Name = "MethodSpec";
			tablesinfo[43].names = new string[2]
			{
				"Method",
				"Instantiation"
			};
			tablesinfo[43].type = Types.MethodSpec;
			tablesinfo[43].ctypes = new Types[2]
			{
				Types.MethodDefOrRef,
				Types.Blob
			};
			tablesinfo[44].Name = "GenericParamConstraint";
			tablesinfo[44].names = new string[2]
			{
				"Owner",
				"Constraint"
			};
			tablesinfo[44].type = Types.GenericParamConstraint;
			tablesinfo[44].ctypes = new Types[2]
			{
				Types.GenericParam,
				Types.TypeDefOrRef
			};
			codedTokenBits = new int[33]
			{
				0,
				1,
				1,
				2,
				2,
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5
			};
			reftables = new RefTableInfo[13];
			reftables[0].type = Types.TypeDefOrRef;
			reftables[0].reftypes = new Types[3]
			{
				Types.TypeDef,
				Types.TypeRef,
				Types.TypeSpec
			};
			reftables[0].refindex = new int[3]
			{
				1,
				2,
				27
			};
			reftables[1].type = Types.HasConstant;
			reftables[1].reftypes = new Types[3]
			{
				Types.Field,
				Types.Param,
				Types.Property
			};
			reftables[1].refindex = new int[3]
			{
				4,
				8,
				23
			};
			reftables[2].type = Types.CustomAttributeType;
			reftables[2].reftypes = new Types[5]
			{
				Types.TypeRef,
				Types.TypeDef,
				Types.Method,
				Types.MemberRef,
				Types.UserString
			};
			reftables[2].refindex = new int[5]
			{
				1,
				2,
				6,
				10,
				1
			};
			reftables[3].type = Types.HasSemantic;
			reftables[3].reftypes = new Types[2]
			{
				Types.Event,
				Types.Property
			};
			reftables[3].refindex = new int[2]
			{
				20,
				23
			};
			reftables[4].type = Types.ResolutionScope;
			reftables[4].reftypes = new Types[4]
			{
				Types.Module,
				Types.ModuleRef,
				Types.AssemblyRef,
				Types.TypeRef
			};
			reftables[4].refindex = new int[4]
			{
				0,
				26,
				35,
				1
			};
			reftables[5].type = Types.HasFieldMarshal;
			reftables[5].reftypes = new Types[2]
			{
				Types.Field,
				Types.Param
			};
			reftables[5].refindex = new int[2]
			{
				4,
				8
			};
			reftables[6].type = Types.HasDeclSecurity;
			reftables[6].reftypes = new Types[3]
			{
				Types.TypeDef,
				Types.Method,
				Types.Assembly
			};
			reftables[6].refindex = new int[3]
			{
				2,
				6,
				32
			};
			reftables[7].type = Types.MemberRefParent;
			reftables[7].reftypes = new Types[5]
			{
				Types.TypeDef,
				Types.TypeRef,
				Types.ModuleRef,
				Types.Method,
				Types.TypeSpec
			};
			reftables[7].refindex = new int[5]
			{
				2,
				1,
				26,
				6,
				27
			};
			reftables[8].type = Types.MethodDefOrRef;
			reftables[8].reftypes = new Types[2]
			{
				Types.Method,
				Types.MemberRef
			};
			reftables[8].refindex = new int[2]
			{
				6,
				10
			};
			reftables[9].type = Types.MemberForwarded;
			reftables[9].reftypes = new Types[2]
			{
				Types.Field,
				Types.Method
			};
			reftables[9].refindex = new int[2]
			{
				4,
				6
			};
			reftables[10].type = Types.Implementation;
			reftables[10].reftypes = new Types[3]
			{
				Types.File,
				Types.AssemblyRef,
				Types.ExportedType
			};
			reftables[10].refindex = new int[3]
			{
				38,
				35,
				39
			};
			reftables[11].type = Types.HasCustomAttribute;
			reftables[11].reftypes = new Types[19]
			{
				Types.Method,
				Types.Field,
				Types.TypeRef,
				Types.TypeDef,
				Types.Param,
				Types.InterfaceImpl,
				Types.MemberRef,
				Types.Module,
				Types.Permission,
				Types.Property,
				Types.Event,
				Types.StandAloneSig,
				Types.ModuleRef,
				Types.TypeSpec,
				Types.Assembly,
				Types.AssemblyRef,
				Types.File,
				Types.ExportedType,
				Types.ManifestResource
			};
			reftables[11].refindex = new int[19]
			{
				6,
				4,
				1,
				2,
				8,
				9,
				10,
				0,
				14,
				23,
				20,
				17,
				26,
				27,
				32,
				35,
				38,
				39,
				40
			};
			reftables[12].type = Types.TypeOrMethodDef;
			reftables[12].reftypes = new Types[2]
			{
				Types.TypeDef,
				Types.Method
			};
			reftables[12].refindex = new int[2]
			{
				2,
				6
			};
		}

		public int Rva2Offset(int rva)
		{
			for (int i = 0; i < sections.Length; i++)
			{
				if (sections[i].virtual_address <= rva && sections[i].virtual_address + sections[i].size_of_raw_data > rva)
				{
					return sections[i].pointer_to_raw_data + (rva - sections[i].virtual_address);
				}
			}
			return 0;
		}

		public int Rva2Section(int rva)
		{
			for (int i = 0; i < sections.Length; i++)
			{
				if (sections[i].virtual_address <= rva && sections[i].virtual_address + sections[i].size_of_raw_data > rva)
				{
					return i;
				}
			}
			return -1;
		}

		public int Offset2Rva(int uOffset)
		{
			for (int i = 0; i < sections.Length; i++)
			{
				if (sections[i].pointer_to_raw_data <= uOffset && sections[i].pointer_to_raw_data + sections[i].size_of_raw_data > uOffset)
				{
					return sections[i].virtual_address + (uOffset - sections[i].pointer_to_raw_data);
				}
			}
			return 0;
		}

		public int GetTypeSize(Types trans)
		{
			if (trans == Types.UInt16)
			{
				return 2;
			}
			if (trans == Types.UInt32)
			{
				return 4;
			}
			if (trans == Types.String)
			{
				return GetStringIndexSize();
			}
			if (trans == Types.Guid)
			{
				return GetGuidIndexSize();
			}
			if (trans == Types.Blob)
			{
				return GetBlobIndexSize();
			}
			if (trans < Types.TypeDefOrRef)
			{
				if (TableLengths[(int)trans] > 65535)
				{
					return 4;
				}
				return 2;
			}
			if (trans < Types.UInt16)
			{
				int num = (int)(trans - 64);
				int num2 = codedTokenBits[reftables[num].refindex.Length];
				int num3 = 65535;
				num3 >>= num2;
				for (int i = 0; i < reftables[num].refindex.Length; i++)
				{
					if (TableLengths[reftables[num].refindex[i]] > num3)
					{
						return 4;
					}
				}
				return 2;
			}
			return 0;
		}

		public int GetStringIndexSize()
		{
			return ((tableheader.HeapOffsetSizes & 1) != 0) ? 4 : 2;
		}

		public int GetGuidIndexSize()
		{
			return ((tableheader.HeapOffsetSizes & 2) != 0) ? 4 : 2;
		}

		public int GetBlobIndexSize()
		{
			return ((tableheader.HeapOffsetSizes & 4) != 0) ? 4 : 2;
		}

		public byte[] IntToByte(int ivalue)
		{
			return BitConverter.GetBytes(ivalue);
		}

		public byte[] UIntToByte(uint ivalue)
		{
			return BitConverter.GetBytes(ivalue);
		}

		public unsafe bool Intialize(BinaryReader reader)
		{
			reader.BaseStream.Position = 0L;
			try
			{
				byte[] array = reader.ReadBytes(sizeof(IMAGE_DOS_HEADER));
				IntPtr ptr;
				try
				{
					byte[] array2 = array;
					fixed (byte* value = array2)
					{
						ptr = (IntPtr)(void*)value;
					}
				}
				finally
				{
				}
				idh = (IMAGE_DOS_HEADER)Marshal.PtrToStructure(ptr, typeof(IMAGE_DOS_HEADER));
				if (idh.e_magic != 23117)
				{
					return false;
				}
				reader.BaseStream.Position = idh.e_lfanew;
				array = reader.ReadBytes(sizeof(IMAGE_NT_HEADERS));
				try
				{
					byte[] array2 = array;
					fixed (byte* value = array2)
					{
						ptr = (IntPtr)(void*)value;
					}
				}
				finally
				{
				}
				inh = (IMAGE_NT_HEADERS)Marshal.PtrToStructure(ptr, typeof(IMAGE_NT_HEADERS));
				if (inh.Signature != 17744)
				{
					return false;
				}
				reader.BaseStream.Position = idh.e_lfanew + 4 + sizeof(IMAGE_FILE_HEADER) + inh.ifh.SizeOfOptionalHeader;
				sections = new image_section_header[inh.ifh.NumberOfSections];
				array = reader.ReadBytes(sizeof(image_section_header) * inh.ifh.NumberOfSections);
				try
				{
					byte[] array2 = array;
					fixed (byte* value = array2)
					{
						ptr = (IntPtr)(void*)value;
					}
				}
				finally
				{
				}
				for (int i = 0; i < sections.Length; i++)
				{
					sections[i] = (image_section_header)Marshal.PtrToStructure(ptr, typeof(image_section_header));
					ptr = (IntPtr)(ptr.ToInt32() + Marshal.SizeOf(typeof(image_section_header)));
				}
				long num;
				if (inh.ioh.ResourceDirectory.RVA != 0)
				{
					num = Rva2Offset(inh.ioh.ResourceDirectory.RVA);
					if (num != 0)
					{
						int num2 = 0;
						int num3 = -1;
						if ((num3 = Rva2Section(inh.ioh.ResourceDirectory.RVA)) != -1)
						{
							num2 = sections[num3].virtual_size;
							if (num2 == 0)
							{
								num2 = sections[num3].size_of_raw_data;
							}
						}
						reader.BaseStream.Position = num;
						rsrcsection = reader.ReadBytes(num2);
					}
				}
				if (inh.ioh.MetaDataDirectory.RVA == 0)
				{
					return false;
				}
				num = Rva2Offset(inh.ioh.MetaDataDirectory.RVA);
				if (num == 0)
				{
					return false;
				}
				reader.BaseStream.Position = num;
				array = reader.ReadBytes(sizeof(NETDirectory));
				try
				{
					byte[] array2 = array;
					fixed (byte* value = array2)
					{
						ptr = (IntPtr)(void*)value;
					}
				}
				finally
				{
				}
				netdir = (NETDirectory)Marshal.PtrToStructure(ptr, typeof(NETDirectory));
				if (netdir.StrongNameSignatureRVA != 0)
				{
					num = Rva2Offset(netdir.StrongNameSignatureRVA);
					if (num != 0)
					{
						reader.BaseStream.Position = num;
						StrongName = reader.ReadBytes(netdir.StrongNameSignatureSize);
					}
				}
				reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA);
				mh = default(MetaDataHeader);
				mh.Signature = reader.ReadInt32();
				mh.MajorVersion = reader.ReadInt16();
				mh.MinorVersion = reader.ReadInt16();
				mh.Reserved = reader.ReadInt32();
				mh.VersionLenght = reader.ReadInt32();
				mh.VersionString = reader.ReadBytes(mh.VersionLenght);
				mh.Flags = reader.ReadInt16();
				mh.NumberOfStreams = reader.ReadInt16();
				streams = new MetaDataStream[mh.NumberOfStreams];
				for (int i = 0; i < mh.NumberOfStreams; i++)
				{
					streams[i].Offset = reader.ReadInt32();
					streams[i].Size = reader.ReadInt32();
					char[] array3 = new char[32];
					int num4 = 0;
					byte b = 0;
					while ((b = reader.ReadByte()) != 0)
					{
						array3[num4++] = (char)b;
					}
					num4++;
					int count = (num4 % 4 != 0) ? (4 - num4 % 4) : 0;
					reader.ReadBytes(count);
					ref MetaDataStream reference = ref streams[i];
					string text = new string(array3);
					char[] trimChars = new char[1];
					reference.Name = text.Trim(trimChars);
					if (streams[i].Name == "#~" || streams[i].Name == "#-")
					{
						MetadataRoot.Name = streams[i].Name;
						MetadataRoot.Offset = streams[i].Offset;
						MetadataRoot.Size = streams[i].Size;
						long num6 = reader.BaseStream.Position;
						reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + streams[i].Offset;
						TablesBytes = reader.ReadBytes(streams[i].Size);
						reader.BaseStream.Position = num6;
					}
					if (streams[i].Name == "#Strings")
					{
						long num6 = reader.BaseStream.Position;
						reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + streams[i].Offset;
						StringOffset = reader.BaseStream.Position;
						Strings = reader.ReadBytes(streams[i].Size);
						reader.BaseStream.Position = num6;
					}
					if (streams[i].Name == "#US")
					{
						long num6 = reader.BaseStream.Position;
						reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + streams[i].Offset;
						US = reader.ReadBytes(streams[i].Size);
						reader.BaseStream.Position = num6;
					}
					if (streams[i].Name == "#Blob")
					{
						long num6 = reader.BaseStream.Position;
						reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + streams[i].Offset;
						BlobOffset = reader.BaseStream.Position;
						Blob = reader.ReadBytes(streams[i].Size);
						reader.BaseStream.Position = num6;
					}
					if (streams[i].Name == "#GUID")
					{
						long num6 = reader.BaseStream.Position;
						reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + streams[i].Offset;
						GUID = reader.ReadBytes(streams[i].Size);
						reader.BaseStream.Position = num6;
					}
				}
				reader.BaseStream.Position = Rva2Offset(netdir.MetaDataRVA) + MetadataRoot.Offset;
				tablestart = reader.BaseStream.Position;
				array = reader.ReadBytes(sizeof(TableHeader));
				try
				{
					byte[] array2 = array;
					fixed (byte* value = array2)
					{
						ptr = (IntPtr)(void*)value;
					}
				}
				finally
				{
				}
				tableheader = (TableHeader)Marshal.PtrToStructure(ptr, typeof(TableHeader));
				TableLengths = new int[64];
				for (int i = 0; i < 64; i++)
				{
					int num7 = (((tableheader.MaskValid >> i) & 1) != 0) ? reader.ReadInt32() : 0;
					TableLengths[i] = num7;
				}
				TablesOffset = reader.BaseStream.Position;
				InitTablesInfo();
				tablesize = new TableSize[45];
				tables = new Table[45];
				for (int i = 0; i < tablesize.Length; i++)
				{
					tablesize[i].Sizes = new int[tablesinfo[i].ctypes.Length];
					tablesize[i].TotalSize = 0;
					for (int j = 0; j < tablesinfo[i].ctypes.Length; j++)
					{
						tablesize[i].Sizes[j] = GetTypeSize(tablesinfo[i].ctypes[j]);
						tablesize[i].TotalSize = tablesize[i].TotalSize + tablesize[i].Sizes[j];
					}
				}
				for (int i = 0; i < tablesize.Length; i++)
				{
					if (TableLengths[i] > 0)
					{
						tables[i].members = new long[TableLengths[i]][];
						for (int j = 0; j < TableLengths[i]; j++)
						{
							tables[i].members[j] = new long[tablesinfo[i].ctypes.Length];
							for (int k = 0; k < tablesinfo[i].ctypes.Length; k++)
							{
								if (tablesize[i].Sizes[k] == 2)
								{
									tables[i].members[j][k] = (reader.ReadInt16() & 0xFFFF);
								}
								if (tablesize[i].Sizes[k] == 4)
								{
									tables[i].members[j][k] = (reader.ReadInt32() & uint.MaxValue);
								}
							}
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public string ReadUTF8String(int offset)
		{
			position = offset + 4;
			if (Blob[position] == byte.MaxValue)
			{
				position++;
				return null;
			}
			int num = (int)ReadCompressedUInt32();
			if (num == 0)
			{
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(Blob, position, (Blob[position + num - 1] == 0) ? (num - 1) : num);
			position += num;
			return @string;
		}

		public uint ReadCompressedUInt32()
		{
			byte b = ReadByte();
			if ((b & 0x80) == 0)
			{
				return b;
			}
			if ((b & 0x40) == 0)
			{
				return (uint)(((b & -129) << 8) | ReadByte());
			}
			return (uint)(((b & -193) << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte());
		}

		public byte ReadByte()
		{
			byte result = Blob[position];
			position++;
			return result;
		}

		public string SpecialReadName(int offset)
		{
			if (offset + 4 > Blob.Length)
			{
				return "";
			}
			int num = Blob[offset + 3];
			int num2 = 4;
			string text = "";
			for (int i = 0; i < num; i++)
			{
				char c = (char)Blob[offset + num2 + i];
				if (c != 0)
				{
					text += c;
				}
			}
			return text;
		}

		public string ReadName(int offset)
		{
			char c = 'a';
			string text = "";
			int num = offset;
			while (c != 0 && num < Strings.Length)
			{
				c = (char)Strings[num];
				if (c != 0)
				{
					text += c;
				}
				num++;
			}
			return text;
		}

		public byte[] ReadBlob(int offset)
		{
			if (offset > Blob.Length)
			{
				return new byte[0];
			}
			try
			{
				byte[] array = new byte[Blob[offset] + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Blob[offset + i];
				}
				return array;
			}
			catch
			{
				return new byte[0];
			}
		}
	}
}
