using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MpSoft.Net.Mail;
using MpSoft.SmtpFiddler.Core;
using System;
using System.Collections.Generic;
using System.Net;

namespace AvaloniaApp
{
	public class ReplyStatusChoose: Window
	{
#pragma warning disable 0649, 0169
		private readonly RadioButton rbOk, rbMailboxUnavailable, rbInsufficientSystemStorage, rbError;
#pragma warning restore 0649, 0169

		public ReplyStatusChoose(int defaultValue)
		{
			this.InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.InitControlFields();

			rbOk.Click+=rbClick;
			rbMailboxUnavailable.Click+=rbClick;
			rbInsufficientSystemStorage.Click+=rbClick;
			rbError.Click+=rbClick;

			(defaultValue==1 ? rbOk :
			defaultValue==2 ? rbMailboxUnavailable :
			defaultValue==3 ? rbInsufficientSystemStorage :
			rbError).IsChecked=true;
		}

		[Obsolete("Don't use this constructor. It's just for compatibility with Avalonia framework.")]
		public ReplyStatusChoose()
		{
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void rbClick(object sender, EventArgs e)
		{
			this.Close(int.Parse(((RadioButton)sender).Tag as string));
		}

	}
}
