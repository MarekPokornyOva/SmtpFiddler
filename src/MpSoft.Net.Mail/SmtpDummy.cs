#region using
using MpSoft.Collections.Helpers;
using MpSoft.Net.Mail;
using System.Text;
#endregion using

namespace MpSoft.Net.Mail
{
    class SmtpDummy:ISmtpTarget
    {
        public void Dispose()
        {}

        public int InitRead(byte[] buffer,int size)
        {
            return SetBuffer(buffer,"220 Welcome in SmtpFiddler\r\n");
        }

        public int Send(byte[] buffer,int size,MailReadPhase phase)
        {
            switch (phase)
            {
                case MailReadPhase.Headers:
                    return SetBuffer(buffer,"250 Mail address accepted\r\n");
                case MailReadPhase.DataEnd:
                    return SetBuffer(buffer,"250 Mail read OK\r\n");
                case MailReadPhase.DataCommand:
                    return SetBuffer(buffer,"354 Start input, end data with <CRLF>.<CRLF>\r\n");
            }
            return 0;
        }

        static int SetBuffer(byte[] buffer,string command)
        {
            return SmtpProcessor._baseEncoding.GetBytes(command,0,command.Length,buffer,0);
        }
    }
}
