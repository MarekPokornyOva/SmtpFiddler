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
	public class FormConfig : Window
	{
#pragma warning disable 0649, 0169
		private readonly TextBox textBoxListenAddress, textBoxListenPort, textBoxForwardHost, textBoxForwardPort;
		private readonly RadioButton radioButtonModeDummy, radioButtonModeProxy;
		private readonly Button buttonOk, buttonCancel;
		private readonly TextBlock groupBoxListen, groupBoxForward, labelListenAddress, labelListenPort, labelForwardPort;
#pragma warning restore 0649, 0169

		public FormConfig()
		{
			this.InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.InitControlFields();

			SmtpListenConfig slc = Config.GetSmtpListenConfig();
			textBoxListenAddress.Text = slc.Endpoint.Address.ToString();
			textBoxListenPort.Text = slc.Endpoint.Port.ToString();

			SmtpForwardConfig sfc = Config.GetSmtpForwardConfig();
			textBoxForwardHost.Text = sfc.Host;
			textBoxForwardPort.Text = sfc.Port.ToString();

			bool proxyMode = Config.FiddleMode == FiddleMode.Proxy;
			radioButtonModeDummy.IsChecked = !proxyMode;
			radioButtonModeProxy.IsChecked = proxyMode;

			buttonOk.Click += buttonOk_Click;
			buttonCancel.Click += buttonCancel_Click;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (!SetConfigValues())
				return;
			//this.DialogResult = DialogResult.OK;
			Config.Save();
			this.Close(true);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			//this.DialogResult = DialogResult.Cancel;
			this.Close(false);
		}

		bool SetConfigValues()
		{
			List<string> errors = new List<string>();

			SmtpListenConfig slc = Config.GetSmtpListenConfig();
			if (IPAddress.TryParse(textBoxListenAddress.Text, out IPAddress tempAddr))
				slc.Endpoint.Address = tempAddr;
			else
				errors.Add(groupBoxListen.Text + " - " + labelListenAddress.Text + " - invalid value");
			if (TryParsePortNumber(textBoxListenPort.Text, out int tempInt))
				slc.Endpoint.Port = tempInt;
			else
				errors.Add(groupBoxListen.Text + " - " + labelListenPort.Text + " - invalid value");

			SmtpForwardConfig sfc = Config.GetSmtpForwardConfig();
			sfc.Host = textBoxForwardHost.Text;
			if (TryParsePortNumber(textBoxForwardPort.Text, out tempInt))
				sfc.Port = tempInt;
			else
				errors.Add(groupBoxForward.Text + " - " + labelForwardPort.Text + " - invalid value");

			Config.FiddleMode = radioButtonModeDummy.IsChecked == true ? FiddleMode.Dummy : FiddleMode.Proxy;

			bool result = errors.Count == 0;
			/*if (!result)
				MessageBox.Show(string.Join(Environment.NewLine, errors), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);*/
			return result;
		}

		bool TryParsePortNumber(string value, out int result)
			=> (int.TryParse(value, out result)) && (result > -1) && (result < 65536);
	}
}
