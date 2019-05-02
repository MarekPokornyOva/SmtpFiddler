#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using MpSoft.SmtpFiddler.Core;
#endregion using

namespace MpSoft.SmtpFiddler.Plugins.Core
{
    public partial class PageAttachments:UserControl,IMailView
    {
        public PageAttachments()
        {
            InitializeComponent();
            this.Dock=DockStyle.Fill;
        }

        public void RenderMail(MailMessageContainer mailContainer)
        {
            if (mailContainer==null)
            {
                comboBoxSelect.Enabled=false;
                comboBoxSelect.Items.Clear();
                return;
            }

            comboBoxSelect.Items.Clear();
            AttachmentCollection atts=mailContainer.Mess.Attachments;
            comboBoxSelect.Enabled=atts.Count!=0;
            if (comboBoxSelect.Enabled)
            {
                foreach (Attachment item in atts)
                    comboBoxSelect.Items.Add(new ComboBoxAttItem() { Name=GetName(item.Name),Bytes=GetAttachmentBytes(item) });
                comboBoxSelect.SelectedIndex=0;
            }
            buttonSaveAs.Enabled=comboBoxSelect.Enabled;
        }

        string GetName(string value)
        {
            return value.Length==0?"noname":value;
        }

        private void comboBoxAttSelect_SelectedIndexChanged(object sender,EventArgs e)
        {
            int selIndex=comboBoxSelect.SelectedIndex;
            if (selIndex==-1)
            {
                ChangeDetail(null);
                return;
            }
            ComboBoxAttItem comboBoxAttItem=comboBoxSelect.Items[selIndex] as ComboBoxAttItem;
            ChangeDetail(comboBoxAttItem==null?null:comboBoxAttItem.Bytes);
        }

        void ChangeDetail(byte[] bytes)
        {
            if (pictureBox.Image!=null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image=null;
            }
            if (bytes==null)
            {
                textBoxPlain.Text="";
                webBrowser.DocumentText="";
                pictureBox.Image=null;
                textBoxHex.Text="";
                return;
            }
            string content=FormatMethods.FormatBytes(bytes);
            textBoxPlain.Text=content;
            webBrowser.DocumentText=content;
            using (MemoryStream ms=new MemoryStream(bytes))
                try
                {
                    pictureBox.Image=Image.FromStream(ms);
                    PictureSetMode();
                }
                catch
                {}
            textBoxHex.Text=FormatMethods.FormatHex(bytes);
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
            {
                return Name+" ("+FormatMethods.FormatSize(Bytes.Length)+")";
            }
        }

        private void pictureBox_Resize(object sender,EventArgs e)
        {
            PictureSetMode();
        }

        void PictureSetMode()
        {
            if (pictureBox.Image==null)
                return;

            PictureBoxSizeMode newMode=(pictureBox.Width>pictureBox.Image.Width)&&(pictureBox.Height>pictureBox.Image.Height)
                ?PictureBoxSizeMode.Normal
                :PictureBoxSizeMode.Zoom;
            if (pictureBox.SizeMode!=newMode)
                pictureBox.SizeMode=newMode;
        }

        private void buttonSaveAs_Click(object sender,EventArgs e)
        {
            int selIndex=comboBoxSelect.SelectedIndex;
            if (selIndex==-1)
                return;
            ComboBoxAttItem comboBoxAttItem=comboBoxSelect.Items[selIndex] as ComboBoxAttItem;
            if (comboBoxAttItem==null)
                return;

            saveFileDialog.FileName=comboBoxAttItem.Name;
            if (saveFileDialog.ShowDialog()==DialogResult.OK)
                try
                {
                    using (Stream file=saveFileDialog.OpenFile())
                    {
                        file.Position=0;
                        byte[] bytes=comboBoxAttItem.Bytes;
                        file.Write(bytes,0,bytes.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on save:\r\n"+ex.GetBaseException().Message);
                }
        }
    }
}
