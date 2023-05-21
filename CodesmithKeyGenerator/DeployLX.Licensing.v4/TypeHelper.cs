using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DeployLX.Licensing.v4
{
	public sealed class TypeHelper
	{
		private TypeHelper()
		{
		}

		public static Type FindType(string typeName, bool throwOnError)
		{
			return FindType(typeName, throwOnError, null);
		}

		public static Type FindType(string typeName, bool throwOnError, string defaultNamespace)
		{
			Type type = null;
			try
			{
				type = Type.GetType(typeName, throwOnError: false, ignoreCase: true);
			}
			catch (Exception)
			{
			}
			if (type == null)
			{
				if (defaultNamespace != null && defaultNamespace[defaultNamespace.Length - 1] != '.')
				{
					defaultNamespace += '.';
				}
				int num = typeName.IndexOf(',');
				if (num <= -1)
				{
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					foreach (Assembly assembly in assemblies)
					{
						type = assembly.GetType(typeName, throwOnError: false, ignoreCase: true);
						if (type != null)
						{
							break;
						}
						if (defaultNamespace != null)
						{
							type = assembly.GetType(defaultNamespace + typeName, throwOnError: false, ignoreCase: true);
						}
						if (type != null)
						{
							break;
						}
					}
				}
				else
				{
					string text = typeName.Substring(0, num);
					string text2 = typeName.Substring(num + 1).Trim();
					Assembly assembly2 = null;
					try
					{
						assembly2 = Assembly.Load(text2);
					}
					catch
					{
					}
					if (assembly2 == null)
					{
						try
						{
							assembly2 = Assembly.Load(text2);
						}
						catch
						{
						}
					}
					if (assembly2 == null)
					{
						try
						{
							text2 = Regex.Replace(text2, ",\\s*Version\\s?=\\s?[0-9\\.]*\\s*", ",");
							assembly2 = Assembly.Load(text2);
						}
						catch
						{
						}
					}
					if (assembly2 == null && text2.IndexOf(',') < 0)
					{
						Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
						foreach (Assembly assembly3 in assemblies)
						{
							if (string.Compare(assembly3.GetName().Name, text2, ignoreCase: true) == 0)
							{
								assembly2 = assembly3;
								break;
							}
						}
					}
					if (assembly2 != null)
					{
						type = assembly2.GetType(text, throwOnError, ignoreCase: true);
						if (type == null && defaultNamespace != null)
						{
							type = assembly2.GetType(defaultNamespace + text, throwOnError, ignoreCase: true);
						}
					}
				}
			}
			if (type == null && throwOnError)
			{
				type = Type.GetType(typeName, throwOnError: true, ignoreCase: true);
			}
			return type;
		}

		public static string GetNonVersionedAssemblyName(Assembly asm)
		{
			return GetNonVersionedAssemblyName(asm.FullName);
		}

		public static string GetNonVersionedAssemblyName(string assemblyName)
		{
			int num = assemblyName.IndexOf("Version=");
			if (num == -1)
			{
				return assemblyName;
			}
			num = assemblyName.LastIndexOf(',', num);
			if (num == -1)
			{
				return null;
			}
			int num2 = assemblyName.IndexOf(',', num + 1);
			if (num2 == -1)
			{
				return assemblyName.Substring(0, num);
			}
			return assemblyName.Substring(0, num) + assemblyName.Substring(num2);
		}

		public static string GetNonVersionedAssemblyQualifiedName(Type type)
		{
			return type.FullName + ", " + GetNonVersionedAssemblyName(type.Assembly);
		}
	}
}
