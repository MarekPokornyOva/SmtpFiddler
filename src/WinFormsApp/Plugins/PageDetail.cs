#region using
using MpSoft.SmtpFiddler.Core;
using System.Net.Mail;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Plugins.Core
{
    public partial class PageDetail:UserControl,IMailView
    {
        public PageDetail()
        {
            InitializeComponent();
            this.Dock=DockStyle.Fill;
        }

        public void RenderMail(MailMessageContainer mailContainer)
        {
            MailMessage mess=mailContainer.Mess;
            labelFrom.Text=FormatMethods.FormatMailAddress(mess.From);
            labelTo.Text=FormatMethods.FormatMailAddresses(mess.To);
            labelSubject.Text=mess.Subject;
            labelSize.Text=FormatMethods.FormatSize(mailContainer.Raw.Body.Length);
        }
    }
}
