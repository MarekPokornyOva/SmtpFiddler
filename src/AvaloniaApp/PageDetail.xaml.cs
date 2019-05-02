using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MpSoft.SmtpFiddler.Core;
using System.Net.Mail;

namespace AvaloniaApp
{
	public class PageDetail : UserControl, IMailView
	{
		TextBlock labelFrom, labelTo, labelCc, labelSubject, labelSize;

		public PageDetail()
		{
			this.InitializeComponent();

			labelFrom = this.FindControl<TextBlock>(nameof(labelFrom));
			labelTo = this.FindControl<TextBlock>(nameof(labelTo));
			labelCc = this.FindControl<TextBlock>(nameof(labelCc));
			labelSubject = this.FindControl<TextBlock>(nameof(labelSubject));
			labelSize = this.FindControl<TextBlock>(nameof(labelSize));
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public void RenderMail(MailMessageContainer mailContainer)
		{
			MailMessage mess = mailContainer.Mess;
			labelFrom.Text = FormatMethods.FormatMailAddress(mess.From);
			labelTo.Text = FormatMethods.FormatMailAddresses(mess.To);
			labelCc.Text = FormatMethods.FormatMailAddresses(mess.CC);
			labelSubject.Text = mess.Subject;
			labelSize.Text = FormatMethods.FormatSize(mailContainer.Raw.Body.Length);
		}
	}
}
