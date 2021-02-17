using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MpSoft.SmtpFiddler.Core;
using System;

namespace AvaloniaApp
{
	public class PageText : UserControl, IMailView
	{
		TextBox textBoxView;

		public PageText()
		{
			this.InitializeComponent();

			textBoxView = this.FindControl<TextBox>(nameof(textBoxView));
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public PageText(Func<MailMessageContainer, string> translator) : this()
		{
			SetTranslator(translator);
		}

		Func<MailMessageContainer, string> _translator;
		public void SetTranslator(Func<MailMessageContainer, string> translator)
		{
			_translator = translator;
		}

		public void RenderMail(MailMessageContainer mailContainer)
		{
			if (_translator != null)
				textBoxView.Text = _translator(mailContainer);
		}
	}
}
