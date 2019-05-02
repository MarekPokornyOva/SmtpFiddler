using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using MpSoft.SmtpFiddler.Core;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mail;

namespace AvaloniaApp
{
	public class PageAttachments : UserControl, IMailView
	{
		ComboBox comboBoxSelect;
		ObservableCollection<ComboBoxAttItem> comboBoxSelectList = new ObservableCollection<ComboBoxAttItem>();
		Button buttonSaveAs;
		TabControl tabControlAtt;
		TabItem tabPagePlain /*,tabPageHtml*/, tabPageImage, tabPageHex;
		Image pictureBox;
		TextBox textBoxPlain, textBoxHex;

		public PageAttachments()
		{
			this.InitializeComponent();

			comboBoxSelect = this.FindControl<ComboBox>(nameof(comboBoxSelect));
			buttonSaveAs = this.FindControl<Button>(nameof(buttonSaveAs));
			tabControlAtt = this.FindControl<TabControl>(nameof(tabControlAtt));
			tabPagePlain = this.FindControl<TabItem>(nameof(tabPagePlain));
			//buttonSaveAs = this.FindControl<TabItem>(nameof(buttonSaveAs));
			tabPageImage = this.FindControl<TabItem>(nameof(tabPageImage));
			tabPageHex = this.FindControl<TabItem>(nameof(tabPageHex));
			pictureBox = this.FindControl<Image>(nameof(pictureBox));
			textBoxPlain = this.FindControl<TextBox>(nameof(textBoxPlain));
			textBoxHex = this.FindControl<TextBox>(nameof(textBoxHex));

			comboBoxSelect.DataContext = comboBoxSelectList;

			comboBoxSelect.SelectionChanged += comboBoxAttSelect_SelectedIndexChanged;
			//pictureBox.LayoutUpdated += pictureBox_Resize;
			buttonSaveAs.Click += buttonSaveAs_Click;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public void RenderMail(MailMessageContainer mailContainer)
		{
			if (mailContainer == null)
			{
				comboBoxSelect.IsEnabled = false;
				comboBoxSelectList.Clear();
				return;
			}

			comboBoxSelectList.Clear();
			AttachmentCollection atts = mailContainer.Mess.Attachments;
			comboBoxSelect.IsEnabled = atts.Count != 0;
			if (comboBoxSelect.IsEnabled)
			{
				foreach (Attachment item in atts)
					comboBoxSelectList.Add(new ComboBoxAttItem() { Name = GetName(item.Name), Bytes = GetAttachmentBytes(item) });
				comboBoxSelect.SelectedIndex = 0;
			}
			buttonSaveAs.IsEnabled = comboBoxSelect.IsEnabled;
		}

		string GetName(string value)
			=> string.IsNullOrEmpty(value) ? "noname" : value;

		private void comboBoxAttSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selIndex = comboBoxSelect.SelectedIndex;
			ChangeDetail(selIndex == -1 ? null : (comboBoxSelectList[selIndex] as ComboBoxAttItem)?.Bytes);
		}

		void ChangeDetail(byte[] bytes)
		{
			if (pictureBox.Source != null)
			{
				pictureBox.Source.Dispose();
				pictureBox.Source = null;
			}
			if (bytes == null)
			{
				textBoxPlain.Text = "";
				//webBrowser.DocumentText = "";
				//pictureBox.Source = null;
				textBoxHex.Text = "";
				return;
			}
			string content = FormatMethods.FormatBytes(bytes);
			textBoxPlain.Text = content;
			//webBrowser.DocumentText = content;
			using (MemoryStream ms = new MemoryStream(bytes))
				try
				{
					pictureBox.Source = new Bitmap(ms);
					PictureSetMode();
				}
				catch
				{ }
			textBoxHex.Text = FormatMethods.FormatHex(bytes);
		}

		static byte[] GetAttachmentBytes(Attachment item)
		{
			return ((MemoryStream)item.ContentStream).GetBuffer();
		}

		class ComboBoxAttItem
		{
			internal string Name;
			internal byte[] Bytes;

			public override string ToString()
				=> $"{Name} ({FormatMethods.FormatSize(Bytes.Length)})";
		}

		private void pictureBox_Resize(object sender, EventArgs e)
		{
			PictureSetMode();
		}

		void PictureSetMode()
		{
			if (pictureBox.Source == null)
				return;


			//pictureBox.Measure(new Size(childSizeX, childSizeY));
			//Size desired = pictureBox.DesiredSize;


			Stretch newMode = (pictureBox.Width > pictureBox.Source.PixelSize.Width) && (pictureBox.Height > pictureBox.Source.PixelSize.Height)
				 ? Stretch.None
				 : Stretch.Uniform;
			if (pictureBox.Stretch != newMode)
				pictureBox.Stretch = newMode;
		}

		SaveFileDialog saveFileDialog = new SaveFileDialog();
		private void buttonSaveAs_Click(object sender, EventArgs e)
		{
			int selIndex = comboBoxSelect.SelectedIndex;
			if (selIndex == -1)
				return;
			ComboBoxAttItem comboBoxAttItem = comboBoxSelectList[selIndex] as ComboBoxAttItem;
			if (comboBoxAttItem == null)
				return;

			saveFileDialog.InitialFileName = comboBoxAttItem.Name;
			saveFileDialog.ShowAsync((Window)this.VisualRoot).ContinueWith(ts =>
			{
				string path = ts.Result;
				if (path!=null)
				{
					//try
					//{
					using (Stream file = File.Create(path))
					{
						file.Position = 0;
						byte[] bytes = comboBoxAttItem.Bytes;
						file.Write(bytes, 0, bytes.Length);
					}
					//}
					//catch (Exception ex)
					//{
					//	MessageBox.Show("Error on save:\r\n" + ex.GetBaseException().Message);
					//}*/
				}
			});
		}
	}
}
