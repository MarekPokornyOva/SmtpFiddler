#region using
using MpSoft.SmtpFiddler.Core;
using System;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Plugins.Core
{
    public partial class PageText:UserControl,IMailView
    {
        public PageText()
        {
            InitializeComponent();
            this.Dock=DockStyle.Fill;
        }

        public PageText(Func<MailMessageContainer,string> translator):this()
        {
            SetTranslator(translator);
        }

        Func<MailMessageContainer,string> _translator;
        public void SetTranslator(Func<MailMessageContainer,string> translator)
        {
            _translator=translator;
        }

        public void RenderMail(MailMessageContainer mailContainer)
        {
            if (_translator!=null)
                textBoxView.Text=_translator(mailContainer);
        }
    }
}
