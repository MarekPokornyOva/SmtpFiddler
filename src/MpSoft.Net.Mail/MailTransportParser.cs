#region using
using MpSoft.Collections;
using MpSoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Mail;
using MpSoft.Collections.Helpers;
#endregion using

namespace MpSoft.Net.Mail
{
	public static class SmtpMailMessageParser
	{
		public static MailMessage Parse(SmtpMailMessageRaw message)
		{
			return ParseMimePart(message.Body);
		}

		static MailMessage ParseMimePart(byte[] bytes)
		{
			Encoding baseEncoding = Encoding.UTF8;

			MailMessage result = new MailMessage();
			string contentType = null;

			int bodyPos = ArrayExt.FindArrIndex(bytes, new byte[] { 13, 10, 13, 10 }) + 4;
			byte[] headBytes = bytes;
			int removePos = ArrayExt.FindArrIndex(headBytes, Encoding.UTF8.GetBytes("multipart/mixed;\r\n"), 0, bodyPos);
			bool newBrNeeded = removePos != -1;
			if (newBrNeeded)
			{
				headBytes = new byte[bodyPos];
				Array.Copy(bytes, headBytes, bodyPos);
				removePos += 16;
				Array.Copy(headBytes, removePos + 2, headBytes, removePos, bodyPos - removePos - 2);
				Array.Resize(ref headBytes, bodyPos - 2);
			}

			BytesReader br = new BytesReader(headBytes);

			string line;
			List<string> unknownHeaders = new List<string>();
			while ((line = br.ReadLine(baseEncoding) ?? "").Length != 0)
			{
				if (line.StartsWith("Sender: "))
					result.Sender = ParseMailAddress(line.Substring(8));
				if (line.StartsWith("From: "))
					result.From = ParseMailAddress(line.Substring(6));
				else if (line.StartsWith("To: "))
					AddAddresses(result.To, line.Substring(4));
				else if (line.StartsWith("Cc: "))
					AddAddresses(result.CC, line.Substring(4));
				else if (line.StartsWith("Reply-To: "))
					AddAddresses(result.ReplyToList, line.Substring(10));
				else if (line.StartsWith("Subject: "))
					SetSubject(result, line.Substring(9));
				else if (line.StartsWith("Content-Type: "))
					contentType = line.Substring(14);
				else
					unknownHeaders.Add(line);
			}

			if (newBrNeeded)
			{
				br = new BytesReader(bytes);
				br.Position = bodyPos;
			}
			string[] contentTypeParts = contentType == null ? new string[0] : contentType.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
			contentType = contentTypeParts.Length > 0 ? contentTypeParts[0] : "";

			try
			{
				switch (contentType)
				{
					case "text/plain":
						ParsePlainText(br, result, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders, null);
						break;
					case "text/html":
						ParsePlainText(br, result, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders, null);
						result.IsBodyHtml = true;
						break;
					case "multipart/mixed":
						ParseMultipart(br, result, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders);
						break;
				}
			}
			catch (Exception ex)
			{
				result.Body = "Body parser error: "+ex.GetBaseException().Message;
			}

			return result;
		}

		static void ParsePlainText(BytesReader br, MailMessage message, string encoding, List<string> unknownHeaders, string partsDelimiter)
		{
			Encoding enc = Encoding.GetEncoding(encoding.Substring(8));
			string result = partsDelimiter == null ? br.ReadToEnd(enc) : br.ReadToBreak(enc, enc.GetBytes("\r\n--" + partsDelimiter));
			string temp;
			if ((TryFindHeader(unknownHeaders, "Content-Transfer-Encoding", out temp)) && (temp == "base64"))
				result = enc.GetString(Convert.FromBase64String(result));
			message.Body = result;
			message.BodyEncoding = enc;
		}

		static void ParseMultipart(BytesReader br, MailMessage message, string partsDelimiter, List<string> unknownHeaders)
		{
			partsDelimiter = partsDelimiter.Substring(9);
			Encoding enc = Encoding.UTF8;
			br.Position += 2;
			br.ReadLine(enc);

			string contentType;
			List<string> unknownHeaders2;
			while (br.Position + 2 != br.Length)
			{
				ReadHeaders(br, out contentType, out unknownHeaders2);
				string[] contentTypeParts = contentType == null ? new string[0] : contentType.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
				contentType = contentTypeParts.Length > 0 ? contentTypeParts[0] : "";
				switch (contentType)
				{
					case "text/plain":
						ParsePlainText(br, message, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders2, partsDelimiter);
						break;
					case "text/html":
						ParsePlainText(br, message, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders2, partsDelimiter);
						message.IsBodyHtml = true;
						break;
					case "application/octet-stream":
						ParseAttachment(br, message, contentTypeParts.Length > 1 ? contentTypeParts[1] : "", unknownHeaders2, partsDelimiter);
						break;
					case "multipart/alternative;":
						ParseAlternative(br, message, unknownHeaders2, partsDelimiter);
						break;
				}
				br.Position += 2;
			}
		}

		static void ParseAttachment(BytesReader br, MailMessage message, string attName, List<string> unknownHeaders, string partsDelimiter)
		{
			Encoding enc = Encoding.UTF8;
			byte[] result = br.ReadToBreak(enc.GetBytes("\r\n--" + partsDelimiter));
			string temp;
			if ((TryFindHeader(unknownHeaders, "Content-Transfer-Encoding", out temp)) && (temp == "base64"))
				result = Convert.FromBase64String(enc.GetString(result));
			message.Attachments.Add(new Attachment(new MemoryStream(result, 0, result.Length, false, true), attName.Substring(5)));
		}

		static void ParseAlternative(BytesReader br, MailMessage message, List<string> unknownHeaders, string partsDelimiter)
		{
			//Encoding enc = Encoding.GetEncoding(encoding.Substring(8));
			Encoding enc = Encoding.UTF8;
			string result = partsDelimiter == null ? br.ReadToEnd(enc) : br.ReadToBreak(enc, enc.GetBytes("\r\n--" + partsDelimiter));
			string temp;
			if ((TryFindHeader(unknownHeaders, "Content-Transfer-Encoding", out temp)) && (temp == "base64"))
				result = enc.GetString(Convert.FromBase64String(result));
			message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(result,enc,""));
		}

		static bool TryFindHeader(List<string> headers, string toFind, out string value)
		{
			int findLen = toFind.Length;
			string temp = headers.Find(x => (string.Compare(x, 0, toFind, 0, findLen) == 0) && (string.Compare(x, findLen, ": ", 0, 2) == 0));
			if (temp == null)
			{
				value = null;
				return false;
			}
			else
			{
				value = temp.Substring(findLen + 2);
				return true;
			}
		}

		static void SetSubject(MailMessage message, string subject)
		{
			if (subject.StartsWith("=?utf-8?B?") && (subject.EndsWith("?=")))
			{
				message.SubjectEncoding = Encoding.UTF8;
				subject = Encoding.UTF8.GetString(Convert.FromBase64String(subject.Substring(10, subject.Length - 12)));
			}
			message.Subject = subject;
		}

		static void ReadHeaders(BytesReader br, out string contentType, out List<string> unknownHeaders)
		{
			contentType = null;
			unknownHeaders = new List<string>();

			Encoding baseEncoding = Encoding.UTF8;
			string line;
			while ((line = br.ReadLine(baseEncoding) ?? "").Length != 0)
				if (line.StartsWith("Content-Type: "))
					contentType = line.Substring(14);
				else
					unknownHeaders.Add(line);
		}

		static MailAddress ParseMailAddress(string value)
		{
			return new MailAddress(value);
		}

		static void AddAddresses(MailAddressCollection collection, string list)
		{
			foreach (string item in list.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
				collection.Add(ParseMailAddress(item));
		}
	}
}
