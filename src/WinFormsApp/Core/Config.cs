#region using
using Microsoft.Win32;
using MpSoft.Net.Mail;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
	public static class Config
	{
		readonly static string appName = @"MpSoft\"+Path.GetFileNameWithoutExtension(Application.ExecutablePath);
		static Config()
		{
			RegistryKey rootRegistry = null;
			string listenAddress = null;
			int? listenPort = null;
			string forwardHost = null;
			int? forwardPort = null;
			try
			{
				rootRegistry=Registry.CurrentUser.OpenSubKey(@"Software\"+appName);
				if (rootRegistry!=null)
				{
					listenAddress=rootRegistry.GetValue("ListenAddress") as string;
					listenPort=GetInt(rootRegistry.GetValue("ListenPort"));
					forwardHost=rootRegistry.GetValue("ForwardHost") as string;
					forwardPort=GetInt(rootRegistry.GetValue("ForwardPort"));
					FiddleMode=(FiddleMode)GetInt(rootRegistry.GetValue("FiddleMode"));
				}
			}
			catch
			{ }
			finally
			{
				if (rootRegistry!=null)
					rootRegistry.Dispose();
			}
			_smtpListenConfig=new SmtpListenConfig() { Endpoint=new IPEndPoint(listenAddress==null ? IPAddress.Any : IPAddress.Parse(listenAddress),listenPort??SmtpDefault.Port) };
			_smtpForwardConfig=new SmtpForwardConfig() { Host=forwardHost??IPAddress.Loopback.ToString(),Port=forwardPort??SmtpDefault.Port };
			_rootUserPluginsPath=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),appName);
		}

		public static void Save()
		{
			using (RegistryKey rootRegistry = Registry.CurrentUser.OpenSubKey(@"Software\"+appName,true)??Registry.CurrentUser.CreateSubKey(@"Software\"+appName))
			{
				rootRegistry.SetValue("ListenAddress",_smtpListenConfig.Endpoint.Address.ToString(),RegistryValueKind.String);
				rootRegistry.SetValue("ListenPort",_smtpListenConfig.Endpoint.Port,RegistryValueKind.DWord);
				rootRegistry.SetValue("ForwardHost",_smtpForwardConfig.Host,RegistryValueKind.String);
				rootRegistry.SetValue("ForwardPort",_smtpForwardConfig.Port,RegistryValueKind.DWord);
				rootRegistry.SetValue("FiddleMode",(int)FiddleMode,RegistryValueKind.DWord);
			}
		}

		static int? GetInt(object value)
		{
			return value==null ? (int?)null : (int)value;
		}

		readonly static string _rootUserPluginsPath;
		public static string GetRootUserPluginsPath()
		{
			return _rootUserPluginsPath;
		}

		static SmtpListenConfig _smtpListenConfig;
		public static SmtpListenConfig GetSmtpListenConfig()
		{
			return _smtpListenConfig;
		}

		static SmtpForwardConfig _smtpForwardConfig;
		public static SmtpForwardConfig GetSmtpForwardConfig()
		{
			return _smtpForwardConfig;
		}

		public static FiddleMode FiddleMode { get; set; }
	}

	public class SmtpListenConfig
	{
		public IPEndPoint Endpoint { get; set; }
	}

	public class SmtpForwardConfig
	{
		public string Host { get; set; }
		public int Port { get; set; }
	}
}
