#region using
using MpSoft.Collections.Helpers;
using MpSoft.Net.Mail;
using System;
using System.IO;
using System.Text;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
	public static class ImportExport
	{
		public static void SaveTo(SmtpMailMessageRaw message,string path)
		{
			using (Stream stream = File.Create(path))
				Eml.Save(message,stream);
		}

		public static SmtpMailMessageRaw LoadFrom(string path)
		{
			using (Stream stream = File.OpenRead(path))
				return Eml.Load(stream);
		}
	}

	public static class Eml
	{
		public static void Save(SmtpMailMessageRaw message,Stream stream)
		{
			/*
			if (_smtpClient == null)
				 _smtpClient = new SmtpClient();
			MailMessage mess = message.Mess;
			_smtpClient.PickupDirectoryLocation = @"c:\b";
			_smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			_smtpClient.Send(mess);
			*/

			string emlHeader;
			StreamWriter sw = new StreamWriter(stream);
			foreach (string item in message.Headers)
				if (TryTransportHeader2Eml(item,out emlHeader))
					sw.WriteLine(emlHeader);
			sw.Flush();
			byte[] bytes = message.Body;
			stream.Write(bytes,0,bytes.Length);
		}

		static bool TryTransportHeader2Eml(string header,out string result)
		{
			string[] parts;
			if (TryParseHeader(header,":<",1,out parts))
			{
				switch (parts[0])
				{
					case "MAIL FROM":
						result="X-Sender: "+parts[1];
						return true;
					case "RCPT TO":
						result="X-Receiver: "+parts[1];
						return true;
				}
			}
			result=null;
			return false;
		}

		static bool TryParseHeader(string header,string delimiter,int suffixLen,out string[] parts)
		{
			parts=null;
			int pos = header.IndexOf(delimiter);
			if (pos==-1)
				return false;
			parts=new string[] { header.Substring(0,pos),header.Substring(pos+2,header.Length-pos-2-suffixLen) };
			return true;
		}

		public static SmtpMailMessageRaw Load(Stream stream)
		{
			byte[] bytes;
			using (MemoryStream ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				bytes=ms.ToArray();
			}

			Encoding baseEncoding = Encoding.UTF8;
			int pos = ArrayExt.FindArrIndex(bytes,baseEncoding.GetBytes("MIME-Version: "));
			if (pos==-1)
				throw new InvalidDataException("MIME-Version header not found");
			string[] headers = Array.ConvertAll(baseEncoding.GetString(bytes,0,pos-3).Split(new string[] { "\r\n" },StringSplitOptions.None),
				 x => {
					 if (TryParseHeader(x,": ",0,out string[] parts))
						 switch (parts[0])
						 {
							 case "X-Sender":
								 return "MAIL FROM:<"+parts[1]+">";
							 case "X-Receiver":
								 return "RCPT TO:<"+parts[1]+">";
						 };
					 return x;
				 });
			int newLen = bytes.Length-pos;
			Array.Copy(bytes,pos,bytes,0,newLen);
			Array.Resize(ref bytes,newLen);
			return new SmtpMailMessageRaw() { Headers=headers,Body=bytes };
		}
	}
}
