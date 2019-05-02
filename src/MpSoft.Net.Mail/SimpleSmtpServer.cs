#region using
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net.Mail;
using MpSoft.Collections.Helpers;
using System.Linq;
#endregion using

namespace MpSoft.Net.Mail
{
	public class SimpleSmtpServer : IDisposable
	{
		TcpListener _listener;
		Thread _listenThread;

		public SimpleSmtpServer() : this(SmtpDefault.Port)
		{ }

		public SimpleSmtpServer(int port) : this(IPAddress.Any, port)
		{ }

		public SimpleSmtpServer(IPAddress localaddr, int port) : this(new IPEndPoint(localaddr, port))
		{ }

		public SimpleSmtpServer(IPEndPoint localEP)
		{
			ListeningEndpoint = localEP;
		}

		public void Start()
		{
			InternalInitialize();
			_listenThread.Start();
		}

		public void Dispose()
		{
			InternalStop();
		}

		void InternalInitialize()
		{
			_listener = new TcpListener(ListeningEndpoint);
			_listenThread = new Thread(new ThreadStart(MainListenVoid)) { IsBackground = true };
		}

		void InternalStop()
		{
			_listener.Stop();
		}

		public delegate void StartExceptionHandler(StartExceptionEventArgs e);
		public delegate void MessageReceiveHandler(MessageReceiveEventArgs e);
		public event StartExceptionHandler StartException;
		public event MessageReceiveHandler MessageReceive;
		public event MessageReceiveHandler HeadersReceive;
		internal void RaiseMessageReceive(MessageReceiveEventArgs args)
		{
			RaiseEvent(MessageReceive, args);
		}

		internal void RaiseHeadersReceive(MessageReceiveEventArgs args)
		{
			RaiseEvent(HeadersReceive, args);
		}

		internal void RaiseStartException(StartExceptionEventArgs args)
		{
			RaiseEvent(StartException, args);
		}

		void RaiseEvent(Delegate del, object args)
		{
			/*if (del!=null)
				 foreach (Delegate item in del.GetInvocationList())
				 {
					  //c.GetType().BaseType...BaseType.AssemblyQualifiedName="System.Windows.Forms.Control, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
					  System.Windows.Forms.Control c=item.Target as System.Windows.Forms.Control;
					  if (c == null)
							item.DynamicInvoke(args);
					  else
					  {
							//public IAsyncResult BeginInvoke(Delegate method, params object[] args)
							IAsyncResult res=c.BeginInvoke(item,args);
							c.EndInvoke(res);
					  }
				 }*/
			if (del != null)
				del.DynamicInvoke(args);
		}

		IPEndPoint _listeningEndpoint;
		public IPEndPoint ListeningEndpoint
		{
			get
			{
				return _listeningEndpoint;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (!value.Equals(_listeningEndpoint))
				{
					if (_listeningEndpoint == null)
						_listeningEndpoint = new IPEndPoint(new IPAddress(value.Address.GetAddressBytes()), value.Port);
					else
					{
						_listeningEndpoint.Address = new IPAddress(value.Address.GetAddressBytes());
						_listeningEndpoint.Port = value.Port;
					}
					if (_listener != null)
					{
						InternalStop();
						InternalInitialize();
						Start();
					}
				}
			}
		}

		readonly object _targetConnectionInfoLock = new object();
		ConnectionInfo _targetConnectionInfo;
		public ConnectionInfo TargetConnectionInfo
		{
			get
			{
				lock (_targetConnectionInfoLock)
					return _targetConnectionInfo;
			}
			set
			{
				lock (_targetConnectionInfoLock)
					_targetConnectionInfo = value;
			}
		}

		void MainListenVoid()
		{
			try
			{
				_listener.Start();
			}
			catch (Exception ex)
			{
				RaiseStartException(new StartExceptionEventArgs() { Exception = ex });
				return;
			}
			TcpClient client;

			while (true)
				try
				{
					client = _listener.AcceptTcpClient();
					new Thread(new ParameterizedThreadStart(ProcessRequest)) { IsBackground = true }
						 .Start(new object[] { client, this });
				}
				catch (System.Net.Sockets.SocketException ex)
				{
					if (ex.SocketErrorCode == SocketError.Interrupted)
						break;
					else
						throw;
				}
		}

		void ProcessRequest(object parms)
		{
			new SmtpProcessor().ProcessRequest(parms);
		}
	}

	class SmtpProcessor
	{
		SimpleSmtpServer serverInstance;
		bool proxyMode;
		List<string> headers = new List<string>();
		MailReadPhase phase;
		List<byte> body = new List<byte>();
		ISmtpTarget target = null;
		int targetHash;
		Socket _socket;
		List<CommunicationItem> communication = new List<CommunicationItem>();
		byte[] heloCommand = null;
		bool isHeloCommand = true;

		internal SmtpProcessor()
		{ }

		internal void ProcessRequest(object parms)
		{
			object[] objs = (object[])parms;
			TcpClient client = (TcpClient)objs[0];
			serverInstance = (SimpleSmtpServer)objs[1];
			MessageReceiveEventArgs messageEventArgs = null;

			try
			{
				_socket = client.Client;
				SetTarget(true);
				int buffLen = 4096;
				byte[] buffer = new byte[buffLen];

				int readLen = target.InitRead(buffer, buffLen);
				WriteResponse(buffer, readLen);

				while (client.Client.Connected)
				{
					try
					{
						readLen = _socket.Receive(buffer, 0, buffLen, SocketFlags.None);
					}
					catch
					{
						continue;
					}

					if ((readLen == 0) || (IsCommand(buffer, readLen, "QUIT")))
					{
						client.Client.Dispose();
						continue;
					}
					else if (IsCommand(buffer, readLen, "MAIL FROM:"))
					{
						ResetPhase();
						ReleaseObjects();
					}

					if (phase == MailReadPhase.Headers)
					{
						if (IsCommand(buffer, readLen, "DATA\r\n"))
							phase = MailReadPhase.DataCommand;
						else
						{
							int pos = ArrayExt.FindArrIndex(buffer, new byte[] { 13, 10 }, 0, readLen);
							if (pos != -1)
							{
								if (isHeloCommand)
								{
									pos += 2;
									heloCommand = new byte[pos];
									Array.Copy(buffer, 0, heloCommand, 0, pos);
									isHeloCommand = false;
								}
								else
									headers.Add(_baseEncoding.GetString(buffer, 0, pos));
							}
						}
					}
					else
					{
						phase = MailReadPhase.Data;
						if (readLen == buffLen)
							body.AddRange(buffer);
						else
							body.AddRange(buffer.Take(readLen));
					}

					communication.Add(new CommunicationItem(CommunicationItem.Direction.ClientToServer, phase == MailReadPhase.Data ? "Binary data (" + readLen.ToString() + " B)" : _baseEncoding.GetString(buffer, 0, readLen)));

					if (phase == MailReadPhase.Headers)
						readLen = target.Send(buffer, readLen, phase);
					else if (phase == MailReadPhase.DataCommand)
					{
						readLen = target.Send(buffer, readLen, phase);
						messageEventArgs = new MessageReceiveEventArgs() { ClientEndPoint = client.Client.RemoteEndPoint, Message = new SmtpMailMessageRaw() { Headers = headers.ToArray(), Body = null }, Mode = proxyMode ? FiddleMode.Proxy : FiddleMode.Dummy };
						messageEventArgs.Communication = communication.ToArray();
						SetMessageEventArgs(messageEventArgs, buffer, readLen);
						serverInstance.RaiseHeadersReceive(messageEventArgs);
						readLen = SetResponseBuffer(messageEventArgs, buffer);
					}
					else
					{
						int bodyLen = body.Count;
						if (
										(bodyLen > 4) &&
										(body[bodyLen - 5] == 13) &&
										(body[bodyLen - 4] == 10) &&
										(body[bodyLen - 3] == 46) &&
										(body[bodyLen - 2] == 13) &&
										(body[bodyLen - 1] == 10)
								  )
						{
							body.RemoveRange(bodyLen - 7, 7);

							messageEventArgs.Message.Body = body.ToArray();
							phase = MailReadPhase.DataEnd;
							readLen = target.Send(buffer, readLen, phase);
							communication.Add(new CommunicationItem(CommunicationItem.Direction.ServerToClient, "???" + _baseEncoding.GetString(buffer, 0, readLen)));
							messageEventArgs.Communication = communication.ToArray();
							ReleaseObjects();
							SetMessageEventArgs(messageEventArgs, buffer, readLen);
							serverInstance.RaiseMessageReceive(messageEventArgs);
							readLen = SetResponseBuffer(messageEventArgs, buffer);
						}
						else
							readLen = target.Send(buffer, readLen, phase);
					}

					communication.Add(new CommunicationItem(CommunicationItem.Direction.ServerToClient, phase == MailReadPhase.Data ? "No reply" : _baseEncoding.GetString(buffer, 0, readLen)));

					switch (readLen)
					{
						case -1:
							client.Client.Dispose();
							break;
						case 0:
							break;
						default:
							WriteResponse(buffer, readLen);
							break;
					}
				}
			}
			finally
			{
				if (_socket != null)
					_socket.Dispose();
				if (target != null)
					target.Dispose();
				client.Close();
			}
		}

		void ResetPhase()
		{
			phase = MailReadPhase.Headers;
			SetTarget(false);
		}

		void ReleaseObjects()
		{
			headers.Clear();
			body.Clear();
			communication.Clear();
		}

		void WriteResponse(byte[] buffer, int readLen)
		{
			_socket.Send(buffer, 0, readLen, SocketFlags.None);
		}

		internal static Encoding _baseEncoding = Encoding.ASCII;
		static bool IsCommand(byte[] buffer, int bufferLen, string command)
		{
			byte[] commBytes = _baseEncoding.GetBytes(command);
			int commLen = command.Length;
			return (bufferLen >= commLen) && (ArrayExt.BinaryEquals(commBytes, 0, buffer, 0, commLen));
		}

		static bool SetMessageEventArgs(MessageReceiveEventArgs messageEventArgs, byte[] buffer, int readLen)
		{
			messageEventArgs.ReturnStatusCode = SmtpStatusCode.LocalErrorInProcessing;
			messageEventArgs.ReturnStatusText = "";

			string text = _baseEncoding.GetString(buffer, 0, readLen);
			int len = text.Length;
			if (len < 5)
				return false;
			if (!int.TryParse(text.Substring(0, 3), out int tempInt))
				return false;
			messageEventArgs.ReturnStatusCode = (SmtpStatusCode)tempInt;
			if (len > 6)
				messageEventArgs.ReturnStatusText = text.Substring(4, len - 6);
			return true;
		}

		static int SetResponseBuffer(MessageReceiveEventArgs messageEventArgs, byte[] buffer)
		{
			string temp = ((int)messageEventArgs.ReturnStatusCode).ToString();
			int len = _baseEncoding.GetBytes(temp, 0, temp.Length, buffer, 0);
			buffer[len] = 32;
			len++;
			if (!string.IsNullOrEmpty(messageEventArgs.ReturnStatusText))
			{
				temp = messageEventArgs.ReturnStatusText;
				len += _baseEncoding.GetBytes(temp, 0, temp.Length, buffer, len);
			}
			buffer[len] = 13;
			len++;
			buffer[len] = 10;
			return len + 1;
		}

		void SetTarget(bool first)
		{
			ConnectionInfo ci = serverInstance.TargetConnectionInfo;
			int ciHash = GetConnectionInfoHash(ci);

			if ((targetHash != ciHash) || (first))
			{
				if (!first)
					target.Dispose();
				proxyMode = ci != null;
				target = proxyMode ? (ISmtpTarget)new SmtpForward(ci.Host, ci.Port) : new SmtpDummy();
				if (!first)
				{
					int buffLen = 1024;
					byte[] buff = new byte[buffLen];
					target.InitRead(buff, buffLen);
					Array.Copy(heloCommand, 0, buff, 0, heloCommand.Length);
					target.Send(buff, heloCommand.Length, MailReadPhase.Headers);
				}
				targetHash = ciHash;
			}
		}

		static int GetConnectionInfoHash(ConnectionInfo info)
		{
			if (info == null)
				return 0;

			unchecked
			{
				string tempS = info.Host;
				IPEndPoint tempIPEP = info.TargetEndPoint;
				int hash = tempS == null ? 0 : tempS.GetHashCode();
				hash = 31 * hash + info.Port.GetHashCode();
				hash = 31 * hash + (tempIPEP == null ? 0 : tempIPEP.GetHashCode());
				return hash;
			}
		}
	}

	public class MessageReceiveEventArgs
	{
		public EndPoint ClientEndPoint { get; internal set; }
		public SmtpMailMessageRaw Message { get; internal set; }
		public CommunicationItem[] Communication { get; internal set; }
		public FiddleMode Mode { get; internal set; }
		public SmtpStatusCode ReturnStatusCode { get; set; }
		public string ReturnStatusText { get; set; }

		public void SetResponseStatus(SmtpStatusCode code)
		{
			this.ReturnStatusCode = code;
			StringBuilder sb = new StringBuilder(code.ToString());
			for (int a = sb.Length - 1; a > 0; a--)
				if (char.IsUpper(sb[a]))
				{
					sb.Insert(a, ' ');
					sb[a + 1] = char.ToLower(sb[a + 1]);
				}
			this.ReturnStatusText = sb.ToString();
		}

		public void SetResponseStatus(SmtpStatusCode code, string text)
		{
			this.ReturnStatusCode = code;
			this.ReturnStatusText = text;
		}

		public void SetStatusOk()
		{
			SetResponseStatus(SmtpStatusCode.Ok, "OK");
		}

		public void SetStatusError()
		{
			SetResponseStatus(SmtpStatusCode.LocalErrorInProcessing);
		}

		public void SetStatusMailboxUnavailable()
		{
			SetResponseStatus(SmtpStatusCode.MailboxUnavailable);
		}

		public void SetStatusInsufficientSystemStorage()
		{
			SetResponseStatus(SmtpStatusCode.InsufficientStorage);
		}
	}

	public class StartExceptionEventArgs
	{
		public Exception Exception { get; internal set; }
	}

	public enum MailReadPhase
	{
		Headers, DataCommand, Data, DataEnd
	}

	public enum FiddleMode
	{
		Dummy, Proxy
	}

	public class CommunicationItem
	{
		internal CommunicationItem(Direction direction, string text)
		{
			Dir = direction;
			Text = text;
		}

		public Direction Dir { get; set; }
		public string Text { get; set; }

		public enum Direction
		{
			ClientToServer, ServerToClient
		}
	}

	public class SmtpMailMessageRaw
	{
		public string[] Headers { get; set; }
		public byte[] Body { get; set; }
	}

	public static class SmtpDefault
	{
		public const int Port = 25;
		public const int PortSecure = 465;
	}

	public class ConnectionInfo
	{
		public IPEndPoint TargetEndPoint { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
	}
}
