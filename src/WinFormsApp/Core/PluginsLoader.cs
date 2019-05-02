#region using
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
	internal static class PluginsLoader
	{
		static Type _pluginType = typeof(IPlugin);
		static List<IPlugin> _plugins = new List<IPlugin>();

		internal static void Load()
		{
			LoadFromPath(Path.GetDirectoryName(Application.ExecutablePath));
			LoadFromPath(Config.GetRootUserPluginsPath());
		}

		internal static void Close()
		{
			foreach (IPlugin item in _plugins)
				item.Close();
		}

		static void LoadFromPath(string path)
		{
			if (!Directory.Exists(path))
				return;

			Assembly asm;
			ConstructorInfo ci;
			object[] emptyObjects = new object[0];
			foreach (string dllPath in Directory.EnumerateFiles(path,"*.dll",SearchOption.AllDirectories))
			{
				try
				{
					asm=Assembly.ReflectionOnlyLoadFrom(dllPath);
				}
				catch
				{
					continue;
				}

				bool found = false;
				try
				{
					foreach (Type type in asm.GetTypes())
						if (CanAcceptType(type))
						{
							found=true;
							break;
						}
				}
				catch
				{ }
				if (!found)
					continue;

				asm=Assembly.LoadFrom(dllPath);
				foreach (Type type in asm.GetTypes())
					if (CanAcceptType(type,out ci))
					{
						IPlugin plugin = (IPlugin)ci.Invoke(emptyObjects);
						plugin.Initialize();
						_plugins.Add(plugin);
					}
			}
		}

		static bool CanAcceptType(Type type)
		{
			ConstructorInfo ci;
			return CanAcceptType(type,out ci);
		}

		static bool CanAcceptType(Type type,out ConstructorInfo ci)
		{
			if ((!type.IsPublic)||(!type.IsAssignableFrom(type)))
			{
				ci=null;
				return false;
			}
			ci=type.GetConstructor(Type.EmptyTypes);
			return ci!=null;
		}
	}

	public interface IPlugin
	{
		void Initialize();
		void Close();
	}
}
