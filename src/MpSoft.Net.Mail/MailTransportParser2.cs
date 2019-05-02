#region using
using MpSoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Mail;
#endregion using

namespace MpSoft.Net.Mail
{
	public static class SmtpMailMessageParser2
	{
		public static MailMessage Parse(SmtpMailMessageRaw message)
		{
			return ParseMimePart(message.Body);
		}

		static MailMessage ParseMimePart(byte[] bytes)
		{
			MailMessage result = new MailMessage();
			BytesReader reader = new BytesReader(bytes);
			ParsePart(reader, result, null, false);

			return result;
		}

		static void ParsePart(BytesReader reader, MailMessage message, string parentBoundary, bool inAlternative)
		{
			List<KeyValuePair<string, string>> headers = ParseHeaders(reader);
			bool isRoot = headers.FindIndex(x => x.Key == "MIME-Version") != -1;
			if (isRoot)
				ProcessMimeHeader(headers, message);

			string contentType, additionalValue;
			GetContentType(headers, out contentType, out additionalValue);
			string boundary = GetBoundary(headers) ?? parentBoundary;

			try
			{
				switch (contentType)
				{
					case "text/plain":
						ParsePlainText(reader, message, boundary, headers, additionalValue, inAlternative, false);
						break;
					case "text/html":
						ParsePlainText(reader, message, boundary, headers, additionalValue, inAlternative, true);
						break;
					case "multipart/mixed":
						ParseMultipart(reader, message, boundary, headers);
						break;
					case "application/octet-stream":
						ParseAttachment(reader, message, boundary, headers, additionalValue);
						break;
					case "multipart/alternative":
						ParseAlternative(reader, message, boundary, headers);
						break;
				}
			}
			catch (Exception ex)
			{
				if (isRoot)
					message.Body = "Body parser error: " + ex.GetBaseException().Message;
				else
					throw;
			}
		}

		static List<KeyValuePair<string, string>> ParseHeaders(BytesReader reader)
		{
			List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
			Encoding baseEncoding = Encoding.UTF8;
			string line;
			while ((line = reader.ReadLine(baseEncoding) ?? "").Length != 0)
			{
				int a = line.IndexOf(": ");
				result.Add(a == -1 ? new KeyValuePair<string, string>(line, null) : new KeyValuePair<string, string>(line.Substring(0, a), line.Substring(a + 2)));
			}
			if (GetBoundary(result)!=null)
				reader.ReadLine(baseEncoding);
			return result;
		}

		#region handle mime headers
		static void ProcessMimeHeader(List<KeyValuePair<string, string>> headers, MailMessage mailMessage)
		{
			foreach (KeyValuePair<string, string> item in headers)
				switch (item.Key)
				{
					case "Sender":
						mailMessage.Sender = ParseMailAddress(item.Value);
						break;
					case "From":
						mailMessage.From = ParseMailAddress(item.Value);
						break;
					case "To":
						AddAddresses(mailMessage.To, item.Value);
						break;
					case "Cc":
						AddAddresses(mailMessage.CC, item.Value);
						break;
					case "Reply-To":
						AddAddresses(mailMessage.ReplyToList, item.Value);
						break;
					case "Subject":
						SetSubject(mailMessage, item.Value);
						break;
				}
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

		static void SetSubject(MailMessage message, string subject)
		{
			if (subject.StartsWith("=?utf-8?B?") && (subject.EndsWith("?=")))
			{
				message.SubjectEncoding = Encoding.UTF8;
				subject = Encoding.UTF8.GetString(Convert.FromBase64String(subject.Substring(10, subject.Length - 12)));
			}
			message.Subject = subject;
		}
		#endregion handle mime headers

		static void GetContentType(List<KeyValuePair<string, string>> headers, out string contentType, out string charset)
		{
			if (!TryFindHeader(headers, "Content-Type", out contentType))
			{
				charset = null;
				return;
			}
			contentType = contentType.TrimEnd(';');
			int a = contentType.IndexOf("; ");
			if (a == -1)
				charset = null;
			else
			{
				charset = contentType.Substring(contentType.IndexOf("=", a) + 1);
				contentType = contentType.Remove(a);
			}
		}

		static string GetBoundary(List<KeyValuePair<string, string>> headers)
		{
			KeyValuePair<string, string> item = headers.Find(x => x.Key.StartsWith(" boundary="));
			return default(KeyValuePair<string, string>).Equals(item) ? null : item.Key.Substring(10);
		}

		#region content type parsers
		static void ParsePlainText(BytesReader reader, MailMessage message, string boundary, List<KeyValuePair<string, string>> headers, string charset, bool alternative, bool isHtml)
		{
			Encoding enc = Encoding.GetEncoding(charset);
			string result = boundary == null ? reader.ReadToEnd(enc) : reader.ReadToBreak(enc, enc.GetBytes("\r\n--" + boundary));
			string temp;
			if ((TryFindHeader(headers, "Content-Transfer-Encoding", out temp)) && (temp == "base64"))
				result = enc.GetString(Convert.FromBase64String(result));

			if (alternative)
				message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(result, enc, isHtml ? "text/html" : "text/plain"));
			else
			{
				message.Body = result;
				message.BodyEncoding = enc;
				message.IsBodyHtml = isHtml;
			}
		}

		static void ParseMultipart(BytesReader reader, MailMessage message, string boundary, List<KeyValuePair<string, string>> headers)
		{
			while (true)
			{
				ParsePart(reader, message, boundary, false);
				if (IsMultipartEnd(reader))
					return;
			}
		}

		static bool IsMultipartEnd(BytesReader reader)
		{
			byte[] next = new byte[2];
			reader.ReadBytes(next, 2);
			bool result = next[0] == '-' && next[1] == '-';
			if (result)
				reader.Position += 55;
			return result;
		}

		static void ParseAttachment(BytesReader reader, MailMessage message, string boundary, List<KeyValuePair<string, string>> headers, string attName)
		{
			Encoding enc = Encoding.UTF8;
			byte[] result = reader.ReadToBreak(enc.GetBytes("\r\n--" + boundary));
			string temp;
			if ((TryFindHeader(headers, "Content-Transfer-Encoding", out temp)) && (temp == "base64"))
				result = Convert.FromBase64String(enc.GetString(result));
			message.Attachments.Add(new Attachment(new MemoryStream(result, 0, result.Length, false, true), attName));
		}

		static void ParseAlternative(BytesReader reader, MailMessage message, string boundary, List<KeyValuePair<string, string>> headers)
		{
			boundary = GetBoundary(headers) ?? boundary;

			bool isAlt = false;
			while (true)
			{
				ParsePart(reader, message, boundary, isAlt);
				if (IsMultipartEnd(reader))
					return;
				isAlt = true;
			}
		}

		static bool TryFindHeader(List<KeyValuePair<string, string>> headers, string toFind, out string value)
		{
			KeyValuePair<string, string> item = headers.Find(x => x.Key == toFind);
			bool found = !default(KeyValuePair<string, string>).Equals(item);
			value = found ? item.Value : null;
			return found;
		}
		#endregion content type parsers
	}
}
