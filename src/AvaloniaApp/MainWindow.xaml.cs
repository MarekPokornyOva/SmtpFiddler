using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MpSoft.Net.Mail;
using MpSoft.SmtpFiddler.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace AvaloniaApp
{
	public class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}

		TabControl tabControlMain;
		TabItem tabPageDetail, tabPagePlain, /*tabPageHtml,*/ tabPageAttachments, tabPageHex, tabPageRaw, tabPageCommunication;
		ComboBox comboBoxViewSelect;
		StackPanel responsePanel;
		Button btnTest;

		ObservableCollection<string> comboBoxViewSelectList = new ObservableCollection<string>() { "<MainView>" };
		ListBox listViewMessages;
		ObservableCollection<ListBoxItem> listViewMessagesList=new ObservableCollection<ListBoxItem>();
		bool comboBoxViewSelect_SelectedIndexChanged_Active = true;

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);

			this.Closed += MainWindow_Closed;

			tabControlMain = this.FindControl<TabControl>(nameof(tabControlMain));
			tabPageDetail = this.FindControl<TabItem>(nameof(tabPageDetail));
			tabPagePlain = this.FindControl<TabItem>(nameof(tabPagePlain));
			//tabPageHtml = this.FindControl<TabItem>(nameof(tabPageHtml));
			tabPageAttachments = this.FindControl<TabItem>(nameof(tabPageAttachments));
			tabPageHex = this.FindControl<TabItem>(nameof(tabPageHex));
			tabPageRaw = this.FindControl<TabItem>(nameof(tabPageRaw));
			tabPageCommunication = this.FindControl<TabItem>(nameof(tabPageCommunication));
			comboBoxViewSelect = tabPageDetail.FindControl<ComboBox>(nameof(comboBoxViewSelect));
			listViewMessages = tabPageDetail.FindControl<ListBox>(nameof(listViewMessages));
			btnTest=tabPageDetail.FindControl<Button>("btnTest");
			btnTest.Click += buttonTest_Click;
			this.FindControl<Button>("toolStripButtonResponseStatus").Click+=toolStripButtonResponseStatus_Click;
			this.FindControl<Button>("toolStripButtonConfiguration").Click += toolStripButtonConfiguration_Click;
			this.FindControl<Button>("toolStripButtonLoadMessages").Click += toolStripButtonLoadMessages_Click;
			this.FindControl<Button>("toolStripButtonSaveMessages").Click += toolStripButtonSaveMessages_Click;
			this.FindControl<Button>("toolStripButtonForwardMail").Click+=toolStripButtonForwardMail_Click;
			responsePanel=this.FindControl<StackPanel>(nameof(responsePanel));
			foreach (Control c in responsePanel.Children)
				if (c is Button btn)
					btn.Click+=ToolStripMenuItemResponseChoose_Click;

			comboBoxViewSelect.DataContext = comboBoxViewSelectList;
			listViewMessages.DataContext = listViewMessagesList;
			listViewMessages.SelectionChanged += listViewMessages_SelectedIndexChanged;
			listViewMessages.DoubleTapped += listViewMessages_DoubleClick;
			comboBoxViewSelect.SelectionChanged += comboBoxViewSelect_SelectedIndexChanged;

			Form1_Load();
		}

		SimpleSmtpServer _smtpServer;
		private void Form1_Load()
		{
			CreateCoreTabs();
			LoadPlugins();

			IPEndPoint listenEndpoint = Config.GetSmtpListenConfig().Endpoint;
			SetFormTitle(listenEndpoint);

			_smtpServer = new SimpleSmtpServer(listenEndpoint);
			_smtpServer.MessageReceive += SimpleSmtpServer_MessageReceive;
			_smtpServer.StartException += new SimpleSmtpServer.StartExceptionHandler(args => {
				Avalonia.Threading.Dispatcher.UIThread.Post(() =>
						MessageBox.Show(args.Exception.GetBaseException().Message)
						);
				//throw new ApplicationException("StartException", args.Exception);
			});
			SetServerConfig();
			_smtpServer.Start();

			btnTest.IsVisible=false;
			comboBoxViewSelect.SelectedIndex = 0;
		}

		private void MainWindow_Closed(object sender, EventArgs e)
		{
			ClosePlugins();
			_smtpServer.Dispose();
		}

		#region controls event handlers
		//private const int WM_SYSCOMMAND = 0x0112;
		//private const int SC_MINIMIZE = 0xF020;

		/*[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_SYSCOMMAND:
					int command = m.WParam.ToInt32() & 0xfff0;
					if ((command == SC_MINIMIZE)*//*&&(this.minimizeToTray)*//*)
					{
						MinimizeToTray();
						return;
					}
					break;
			}
			base.WndProc(ref m);
		}*/

		private void listViewMessages_SelectedIndexChanged(object sender, EventArgs e)
			=> ChangeDetail(GetItemMessage(listViewMessages.SelectedItem as ListBoxItem), true);

		private void listViewMessages_DoubleClick(object sender, EventArgs e)
		{
			tabControlMain.SelectedIndex = 1;
		}

		private void comboBoxViewSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxViewSelect_SelectedIndexChanged_Active)
				ChangeDetail(GetItemMessage(listViewMessages.SelectedItem as ListBoxItem), false);
		}

		/*private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			this.ShowInTaskbar = true;
			this.WindowState = FormWindowState.Normal;
			this.Show();
			this.BringToFront();
		}*/

		int _responseStatus=1;
		private void ToolStripMenuItemResponseChoose_Click(object sender, EventArgs e)
		{
			_responseStatus=int.Parse(((Button)sender).Tag as string);
		}

		IEnumerable<ListBoxItem> GetSelectedMessages()
			=> listViewMessages.SelectedItems.OfType<ListBoxItem>();

		private void toolStripButtonForwardMail_Click(object sender, EventArgs e)
		{
			foreach (ListBoxItem item in listViewMessages.SelectedItems)
				ForwardMail(GetItemMessage(item));
		}

		SaveFileDialog saveFileDialog = new SaveFileDialog();
		private void toolStripButtonSaveMessages_Click(object sender,EventArgs e)
		{
			saveFileDialog.ShowAsync(this).ContinueWith(ts =>
			{
				string filename = ts.Result;
				if (filename!=null)
					Avalonia.Threading.Dispatcher.UIThread.Post(() =>
					{
						try
						{
							if (listViewMessages.SelectedItems.Count==1)
							{
								ImportExport.SaveTo(GetItemMessage((ListBoxItem)listViewMessages.SelectedItems[0]).Raw,filename);
								return;
							}

							string[] parts = new string[2];
							parts[1]=Path.GetExtension(filename);
							parts[0]=filename.Substring(0,filename.Length-parts[1].Length)+"-";
							int a = 0;
							foreach (ListBoxItem item in listViewMessages.SelectedItems)
								ImportExport.SaveTo(GetItemMessage(item).Raw,parts[0]+((++a).ToString("0000"))+parts[1]);
						}
						catch (Exception ex)
						{
							MessageBox.Show("Error on save:\r\n" + ex.GetBaseException().Message);
						}
					});
			});
		}

		OpenFileDialog openFileDialog = new OpenFileDialog();
		private void toolStripButtonLoadMessages_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowAsync(this).ContinueWith(ts =>
			{
				Avalonia.Threading.Dispatcher.UIThread.Post(() =>
				{
					try
					{
						foreach (string item in ts.Result)
							AddMessage(ImportExport.LoadFrom(item),null);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error on load:\r\n"+ex.GetBaseException().Message);
					}
				});
			});
		}

		private void toolStripButtonResponseStatus_Click(object sender,EventArgs e)
		{
			responsePanel.IsVisible=!responsePanel.IsVisible;
		}

		private void toolStripButtonConfiguration_Click(object sender, EventArgs e)
		{
			new FormConfig().ShowDialog<bool?>(this).ContinueWith(ts => { if (ts.Result==true) Avalonia.Threading.Dispatcher.UIThread.Post(() => SetServerConfig()); });
		}
		#endregion controls event handlers

		#region private methods
		void CreateCoreTabs()
		{
			tabPageDetail.Content = new PageDetail();
			string translator(MailMessageContainer x) => x == null
				? ""
				: comboBoxViewSelect.SelectedIndex == 0
					? x.Mess.Body
					: x.MessAltViews[comboBoxViewSelect.SelectedIndex - 1].Item2;
			tabPagePlain.Content = new PageText(translator);
			//tabPageHtml.Content = new PageHtml(translator);
			tabPageAttachments.Content = new PageAttachments();
			tabPageHex.Content = new PageText(x => x == null ? "" : FormatMethods.FormatHex(x.Raw.Body));
			tabPageRaw.Content = new PageText(x => x == null ? "" :
					  new StringBuilder(string.Join(Environment.NewLine, x.Raw.Headers))
					  .AppendLine().AppendLine().Append(FormatMethods.FormatBytes(x.Raw.Body)).ToString()
				 );
			tabPageCommunication.Content = new PageText(x => x == null ? "" : FormatMethods.FormatCommunication(x.Communication));
		}
		
		string GetAltViewContent(AlternateView alternateView)
		{
			string result = new StreamReader(alternateView.ContentStream, Encoding.GetEncoding(alternateView.ContentType.CharSet))
				.ReadToEnd();
			alternateView.ContentStream.Position = 0;
			return result;
		}

		void AddMessage(SmtpMailMessageRaw message, CommunicationItem[] communication)
		{
			MailMessage mess = SmtpMailMessageParser2.Parse(message);
			ListBoxItem lvi = new ListBoxItem
			{
				Content = DateTime.Now.ToString("HH:mm:ss (dd.MM.)") + mess.Subject,
				Tag = new MailMessageContainer() { Raw = message, Mess = mess, Communication = communication, MessAltViews = mess.AlternateViews.Select(x => new Tuple<string, string>(x.ContentType.MediaType, GetAltViewContent(x))).ToArray() }
			};
			listViewMessagesList.Add(lvi);
		}

		MailMessageContainer GetItemMessage(ListBoxItem item)
			=> (MailMessageContainer)item.Tag;

		void ChangeDetail(MailMessageContainer detail, bool refreshViews)
		{
			if (refreshViews)
			{
				//comboBoxViewSelect.SuspendLayout();
				int oldIndex = comboBoxViewSelect.SelectedIndex;
				comboBoxViewSelect_SelectedIndexChanged_Active = false;
				for (int a = comboBoxViewSelectList.Count - 1; a > 0; a--)
					comboBoxViewSelectList.RemoveAt(a);
				if (detail != null)
				{
					foreach (AlternateView altView in detail.Mess.AlternateViews)
						comboBoxViewSelectList.Add(altView.ContentType.MediaType);
				}
				if (oldIndex > comboBoxViewSelectList.Count - 1)
					oldIndex = comboBoxViewSelectList.Count - 1;
				comboBoxViewSelect_SelectedIndexChanged_Active = true;
				comboBoxViewSelect.SelectedIndex = oldIndex;
				//comboBoxViewSelect.ResumeLayout();
			}

			foreach (TabItem tp in tabControlMain.Items)
				if (tp.Content is IMailView mv)
					mv.RenderMail(detail);
		}

		/*void MinimizeToTray()
		{
			this.Hide();
			this.ShowInTaskbar = false;
		}*/

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
			int pos = this.Title.IndexOf(_listeningOnTitlePrefix);
			this.Title = pos == -1 ? this.Title + _listeningOnTitlePrefix + val : this.Title.Substring(0, pos + _listeningOnTitlePrefix.Length) + val;
		}
		#endregion private methods

		#region MessageReceive event handler
		void SimpleSmtpServer_MessageReceive(MessageReceiveEventArgs e)
		{
			int replyStatus;
			if (_responseStatus==0)
			{
				replyStatus=0;
				ManualResetEventSlim mre=new ManualResetEventSlim(false);
				Avalonia.Threading.Dispatcher.UIThread.Post(() => new ReplyStatusChoose(1).ShowDialog<int>(this).ContinueWith(ts => { replyStatus=ts.Result; mre.Set(); }));
				mre.Wait();
			}
			else
				replyStatus=_responseStatus;

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
					Avalonia.Threading.Dispatcher.UIThread.Post(() => AddMessage(e.Message,e.Communication));
					//notifyIcon.Text = listViewMessages.Items.Count.ToString() + " messages";
					//notifyIcon.ShowBalloonTip(1, "New message", notifyIcon.Text, ToolTipIcon.Info);
					break;
			}
		}
		#endregion MessageReceive event handler

		#region test button
		private void buttonTest_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			//MpSoft.SmtpFiddler.Test.Tester.SendMail01(_smtpServer.ListeningEndpoint.Port);
			MpSoft.SmtpFiddler.Test.Tester.SendMail02(_smtpServer.ListeningEndpoint.Port);
		}
		#endregion test button
	}
}
