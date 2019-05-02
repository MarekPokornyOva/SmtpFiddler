#region using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
#endregion using

namespace MpSoft.Net.Mail
{
    public class SmtpForward:ISmtpTarget
    {
        public SmtpForward(int port):this(new IPEndPoint(IPAddress.Loopback,port))
        {}

        TcpClient _client;

        public SmtpForward(string host,int port)
        {
            _client=new TcpClient();
            _client.Connect(host,port);
        }

        public SmtpForward(IPEndPoint endpoint)
        {
            _client=new TcpClient();
            _client.Connect(endpoint);
        }

        public void Dispose()
        {
            _client.Client.Dispose();
        }

        public int InitRead(byte[] buffer,int size)
        {
            return _client.Client.Receive(buffer,0,size,SocketFlags.None);
        }

        public int Send(byte[] buffer,int size,MailReadPhase phase)
        {
            try
            {
                _client.Client.Send(buffer,0,size,SocketFlags.None);
                if (phase!=MailReadPhase.Data)
                    return _client.Client.Receive(buffer,0,buffer.Length,SocketFlags.None);
            }
            catch
            {}

            return _client.Client.Connected?0:-1;
        }
    }
}
