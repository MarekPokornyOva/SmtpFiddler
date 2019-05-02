#region using
using MpSoft.SmtpFiddler.Plugins.Core;
using MpSoft.Net.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Linq;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
	public partial class FormMain : Form
	{
		#region controls event handlers
		public FormMain()
		{
			InitializeComponent();
		}

		SimpleSmtpServer _smtpServer;
		private void Form1_Load(object sender, EventArgs e)
		{
			CreateCoreTabs();
			LoadPlugins();

			IPEndPoint listenEndpoint = Config.GetSmtpListenConfig().Endpoint;
			SetFormTitle(listenEndpoint);

			_smtpServer = new SimpleSmtpServer(listenEndpoint);
			_smtpServer.MessageReceive += SimpleSmtpServer_MessageReceive;
			_smtpServer.StartException += new SimpleSmtpServer.StartExceptionHandler(args => { MessageBox.Show(args.Exception.GetBaseException().Message); });
			SetServerConfig();
			_smtpServer.Start();

			buttonTest.Visible=false;
			comboBoxViewSelect.SelectedIndex = 0;
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			_smtpServer.Dispose();
			ClosePlugins();
		}

		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MINIMIZE = 0xF020;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_SYSCOMMAND:
					int command = m.WParam.ToInt32() & 0xfff0;
					if ((command == SC_MINIMIZE)/*&&(this.minimizeToTray)*/)
					{
						MinimizeToTray();
						return;
					}
					break;
			}
			base.WndProc(ref m);
		}

		private void listViewMessages_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection sels = listViewMessages.SelectedItems;
			ChangeDetail(sels.Count == 0 ? null : GetItemMessage(sels[0]), true);
		}

		private void listViewMessages_DoubleClick(object sender, EventArgs e)
		{
			tabControlMain.SelectedIndex = 1;
		}

		private void comboBoxViewSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection sels = listViewMessages.SelectedItems;
			ChangeDetail(sels.Count == 0 ? null : GetItemMessage(sels[0]), false);
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			this.ShowInTaskbar = true;
			this.WindowState = FormWindowState.Normal;
			this.Show();
			this.BringToFront();
		}

		private void ToolStripMenuItemResponseChoose_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem item in ToolStripMenuItemResponseChoose.DropDownItems)
				item.Checked = false;
			((ToolStripMenuItem)sender).Checked = true;
		}

		private void toolStripButtonForwardMail_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewMessages.SelectedItems)
				ForwardMail(GetItemMessage(item));
		}

		private void toolStripButtonSaveMessages_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() != DialogResult.OK)
				return;
			string filename = saveFileDialog.FileName;

			try
			{
				if (listViewMessages.SelectedItems.Count == 1)
				{
					ImportExport.SaveTo(GetItemMessage(listViewMessages.SelectedItems[0]).Raw, filename);
					return;
				}

				string[] parts = new string[2];
				parts[1] = Path.GetExtension(filename);
				parts[0] = filename.Substring(0, filename.Length - parts[1].Length) + "-";
				int a = 0;
				foreach (ListViewItem item in listViewMessages.SelectedItems)
					ImportExport.SaveTo(GetItemMessage(item).Raw, parts[0] + ((++a).ToString("0000")) + parts[1]);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error on save:\r\n" + ex.GetBaseException().Message);
			}
		}

		private void toolStripButtonLoadMessages_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() != DialogResult.OK)
				return;
			try
			{
				foreach (string item in openFileDialog.FileNames)
					AddMessage(ImportExport.LoadFrom(item), null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error on load:\r\n" + ex.GetBaseException().Message);
			}
		}

		FormConfig _formConfig;
		private void toolStripButtonConfiguration_Click(object sender, EventArgs e)
		{
			if (_formConfig == null)
				_formConfig = new FormConfig();
			if (_formConfig.ShowDialog(this) == DialogResult.OK)
				SetServerConfig();
		}
		#endregion controls event handlers

		#region private methods
		void CreateCoreTabs()
		{
			tabPageDetail.Controls.Add(new PageDetail());
			Func<MailMessageContainer, string> translator = x => x == null
				? ""
				: comboBoxViewSelect.SelectedIndex == 0
					? x.Mess.Body
					: x.MessAltViews[comboBoxViewSelect.SelectedIndex - 1].Item2;
			tabPagePlain.Controls.Add(new PageText(translator));
			tabPageHtml.Controls.Add(new PageHtml(translator));
			tabPageAttachments.Controls.Add(new PageAttachments());
			tabPageHex.Controls.Add(new PageText(x => x == null ? "" : FormatMethods.FormatHex(x.Raw.Body)));
			tabPageRaw.Controls.Add(new PageText(x => x == null ? "" :
					  new StringBuilder(string.Join(Environment.NewLine, x.Raw.Headers))
					  .AppendLine().AppendLine().Append(FormatMethods.FormatBytes(x.Raw.Body)).ToString()
				 ));
			tabPageCommunication.Controls.Add(new PageText(x => x == null ? "" : FormatMethods.FormatCommunication(x.Communication)));
		}

		string GetAltViewContent(AlternateView alternateView)
		{
			string result = new StreamReader(alternateView.ContentStream, Encoding.GetEncoding(alternateView.ContentType.CharSet))
				.ReadToEnd();
			alternateView.ContentStream.Position = 0;
			return result;
		}

		int a = 0;
		void AddMessage(SmtpMailMessageRaw message, CommunicationItem[] communication)
		{
			MailMessage mess = SmtpMailMessageParser2.Parse(message);
			ListViewItem lvi = new ListViewItem((++a).ToString());
			lvi.SubItems.Add(DateTime.Now.ToString("HH:mm:ss (dd.MM.)"));
			lvi.SubItems.Add(mess.Subject);
			lvi.Tag = new MailMessageContainer() { Raw = message, Mess = mess, Communication = communication, MessAltViews = mess.AlternateViews.Select(x=>new Tuple<string, string>(x.ContentType.MediaType, GetAltViewContent(x))).ToArray() };
			listViewMessages.Items.Add(lvi);
		}

		MailMessageContainer GetItemMessage(ListViewItem item)
		{
			return item.Tag as MailMessageContainer;
		}

		void ChangeDetail(MailMessageContainer detail, bool refreshViews)
		{
			if (detail == null)
			{
				labelFrom.Text = "";
				labelTo.Text = "";
				labelSubject.Text = "";
				return;
			}

			labelFrom.Text = FormatMethods.FormatMailAddress(detail.Mess.From);
			labelTo.Text = FormatMethods.FormatMailAddresses(detail.Mess.To);
			labelSubject.Text = detail.Mess.Subject;

			if (refreshViews)
			{
				comboBoxViewSelect.SuspendLayout();
				for (int a = comboBoxViewSelect.Items.Count - 1; a > 0; a--)
					comboBoxViewSelect.Items.RemoveAt(a);
				if (detail != null)
				{
					foreach (AlternateView altView in detail.Mess.AlternateViews)
						comboBoxViewSelect.Items.Add(altView.ContentType.MediaType);
					comboBoxViewSelect.SelectedIndex = 0;
				}
				comboBoxViewSelect.ResumeLayout();
			}

			foreach (TabPage tp in tabControlMain.TabPages)
				foreach (Control c in tp.Controls)
				{
					IMailView mv = c as IMailView;
					if (mv != null)
						mv.RenderMail(detail);
				}
		}

		void MinimizeToTray()
		{
			this.Hide();
			this.ShowInTaskbar = false;
		}

		void LoadPlugins()
		{
			PluginsLoader.Load();
		}

		void ClosePlugins()
		{
			PluginsLoader.Close();
		}

		SmtpClient _smtpClient;
		void ForwardMail(MailMessageContainer message)
		{
			SmtpForwardConfig conf = Config.GetSmtpForwardConfig();
			if (_smtpClient == null)
				_smtpClient = new SmtpClient();
			_smtpClient.Host = conf.Host;
			_smtpClient.Port = conf.Port;
			try
			{
				_smtpClient.SendAsync(message.Mess, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		void SetServerConfig()
		{
			IPEndPoint listenEndpoint = Config.GetSmtpListenConfig().Endpoint;
			SetFormTitle(listenEndpoint);
			_smtpServer.ListeningEndpoint = listenEndpoint;

			SmtpForwardConfig sfc = Config.GetSmtpForwardConfig();
			_smtpServer.TargetConnectionInfo = Config.FiddleMode == FiddleMode.Dummy ? null : new ConnectionInfo() { Host = sfc.Host, Port = sfc.Port };
		}

		const string _listeningOnTitlePrefix = " - Listening on ";
		void SetFormTitle(IPEndPoint listenEndpoint)
		{
			string val = listenEndpoint.ToString();
			int pos = this.Text.IndexOf(_listeningOnTitlePrefix);
			this.Text = pos == -1 ? this.Text + _listeningOnTitlePrefix + val : this.Text.Substring(0, pos + _listeningOnTitlePrefix.Length) + val;
		}
		#endregion private methods

		#region MessageReceive event handler
		ReplyStatusChoose _replyStatusChoose;
		void SimpleSmtpServer_MessageReceive(MessageReceiveEventArgs e)
		{
			int replyStatus = 0;
			if (ToolStripMenuItemResponseOk.Checked)
				replyStatus = 1;
			else if (ToolStripMenuItemResponseAsk.Checked)
			{
				if (_replyStatusChoose == null)
					_replyStatusChoose = new ReplyStatusChoose();
				_replyStatusChoose.ShowDialog(this);
				replyStatus = _replyStatusChoose.ChoosenOption;
			}
			else if (ToolStripMenuItemResponseMailboxUnavailable.Checked)
				replyStatus = 2;
			else if (ToolStripMenuItemResponseInsufficientSystemStorage.Checked)
				replyStatus = 3;
			else if (ToolStripMenuItemResponseError.Checked)
				replyStatus = 4;

			switch (replyStatus)
			{
				case 2:
					e.SetStatusMailboxUnavailable();
					break;
				case 3:
					e.SetStatusInsufficientSystemStorage();
					break;
				case 4:
					e.SetStatusError();
					break;
				default:
					AddMessage(e.Message, e.Communication);
					notifyIcon.Text = listViewMessages.Items.Count.ToString() + " messages";
					notifyIcon.ShowBalloonTip(1, "New message", notifyIcon.Text, ToolTipIcon.Info);
					break;
			}
		}
		#endregion MessageReceive event handler

		#region test button
		private void buttonTest_Click(object sender, EventArgs e)
		{
			//MpSoft.SmtpFiddler.Test.Tester.SendMail01(_smtpServer.ListeningEndpoint.Port);
			MpSoft.SmtpFiddler.Test.Tester.SendMail02(_smtpServer.ListeningEndpoint.Port);
		}
		#endregion test button
	}
}
