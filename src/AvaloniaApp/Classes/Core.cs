#region using
using MpSoft.Net.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
	interface IMailView
	{
		void RenderMail(MailMessageContainer mailContainer);
	}

	public class MailMessageContainer
	{
		public SmtpMailMessageRaw Raw;
		public CommunicationItem[] Communication;
		public MailMessage Mess;
		public Dictionary<object, object> Tag;
		public Tuple<string, string>[] MessAltViews;
	}

	public static class FormatMethods
	{
		public static string FormatBytes(byte[] bytes)
			=> new StringBuilder(Encoding.ASCII.GetString(bytes)).Replace((char)0, ' ').ToString();

		public static string FormatHex(byte[] bytes)
		{
			StringBuilder sb = new StringBuilder();
			int a = 0;
			foreach (byte b in bytes)
			{
				sb.Append(b.ToString("x2")).Append(" ");
				if (a++ == 80)
				{
					sb.AppendLine();
					a = 0;
				}
			}
			return sb.ToString();
		}

		public static string FormatMailAddress(MailAddress address)
			=> address.ToString();

		public static string FormatMailAddresses(MailAddressCollection addresses)
			=> string.Join(", ", addresses);

		public static string FormatSize(int size)
			=> size.ToString("N0") + " B [" + (size / 1024).ToString("N0") + " KB]";

		public static string FormatCommunication(CommunicationItem[] communication)
		{
			if (communication == null)
				return "No records";
			StringBuilder sb = new StringBuilder();
			foreach (CommunicationItem item in communication)
			{
				sb.Append(item.Dir.ToString()).Append("-").Append(item.Text);
				if (!item.Text.EndsWith("\r\n"))
					sb.AppendLine();
			}
			return sb.ToString();
		}

		/*
		static string DumpObject(object o)
		{
			 StringBuilder sb=new StringBuilder();
			 System.Reflection.MemberInfo[] members=o.GetType().GetMembers(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
			 foreach (System.Reflection.MemberInfo item in members)
			 {
				  if (item.MemberType==System.Reflection.MemberTypes.Field)
				  {
						object val=((System.Reflection.FieldInfo)item).GetValue(o);
						sb.AppendLine(item.Name+"="+(val==null?"null":val.ToString()));
				  }
				  else if (item.MemberType==System.Reflection.MemberTypes.Property)
				  {
						System.Reflection.PropertyInfo pi=((System.Reflection.PropertyInfo)item);
						if ((pi.CanRead)&&(pi.GetIndexParameters().Length==0))
						{
							 object val=pi.GetValue(o,new object[0]);
							 sb.AppendLine(item.Name + "=" + (val == null ? "null" : val.ToString()));
						}
				  }
			 }
			 return sb.ToString();
		}
		*/
	}
}
