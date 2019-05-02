using System;

namespace MpSoft.Net.Mail
{
    interface ISmtpTarget:IDisposable
    {
        int InitRead(byte[] buffer,int size);
        int Send(byte[] buffer,int size,MailReadPhase phase);
    }
}
