namespace MpSoft.SmtpFiddler.Plugins.Core
{
    partial class PageAttachments
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxSelect = new System.Windows.Forms.ComboBox();
            this.tabControlAtt = new System.Windows.Forms.TabControl();
            this.tabPagePlain = new System.Windows.Forms.TabPage();
            this.textBoxPlain = new System.Windows.Forms.TextBox();
            this.tabPageHtml = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.textBoxHex = new System.Windows.Forms.TextBox();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControlAtt.SuspendLayout();
            this.tabPagePlain.SuspendLayout();
            this.tabPageHtml.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPageHex.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelect.Enabled = false;
            this.comboBoxSelect.FormattingEnabled = true;
            this.comboBoxSelect.Location = new System.Drawing.Point(0, 0);
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(519, 21);
            this.comboBoxSelect.TabIndex = 3;
            this.comboBoxSelect.SelectedIndexChanged += new System.EventHandler(this.comboBoxAttSelect_SelectedIndexChanged);
            // 
            // tabControlAtt
            // 
            this.tabControlAtt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAtt.Controls.Add(this.tabPagePlain);
            this.tabControlAtt.Controls.Add(this.tabPageHtml);
            this.tabControlAtt.Controls.Add(this.tabPageImage);
            this.tabControlAtt.Controls.Add(this.tabPageHex);
            this.tabControlAtt.Location = new System.Drawing.Point(0, 27);
            this.tabControlAtt.Name = "tabControlAtt";
            this.tabControlAtt.SelectedIndex = 0;
            this.tabControlAtt.Size = new System.Drawing.Size(519, 361);
            this.tabControlAtt.TabIndex = 2;
            // 
            // tabPagePlain
            // 
            this.tabPagePlain.Controls.Add(this.textBoxPlain);
            this.tabPagePlain.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlain.Name = "tabPagePlain";
            this.tabPagePlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlain.Size = new System.Drawing.Size(511, 335);
            this.tabPagePlain.TabIndex = 0;
            this.tabPagePlain.Text = "Plain";
            this.tabPagePlain.UseVisualStyleBackColor = true;
            // 
            // textBoxPlain
            // 
            this.textBoxPlain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPlain.Location = new System.Drawing.Point(3, 3);
            this.textBoxPlain.Multiline = true;
            this.textBoxPlain.Name = "textBoxPlain";
            this.textBoxPlain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPlain.Size = new System.Drawing.Size(505, 329);
            this.textBoxPlain.TabIndex = 1;
            this.textBoxPlain.WordWrap = false;
            // 
            // tabPageHtml
            // 
            this.tabPageHtml.Controls.Add(this.webBrowser);
            this.tabPageHtml.Location = new System.Drawing.Point(4, 22);
            this.tabPageHtml.Name = "tabPageHtml";
            this.tabPageHtml.Size = new System.Drawing.Size(511, 335);
            this.tabPageHtml.TabIndex = 2;
            this.tabPageHtml.Text = "Html";
            this.tabPageHtml.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(511, 335);
            this.webBrowser.TabIndex = 0;
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.pictureBox);
            this.tabPageImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Size = new System.Drawing.Size(511, 335);
            this.tabPageImage.TabIndex = 3;
            this.tabPageImage.Text = "Image";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(511, 335);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.textBoxHex);
            this.tabPageHex.Location = new System.Drawing.Point(4, 22);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHex.Size = new System.Drawing.Size(511, 335);
            this.tabPageHex.TabIndex = 1;
            this.tabPageHex.Text = "Hex";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // textBoxHex
            // 
            this.textBoxHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHex.Location = new System.Drawing.Point(3, 3);
            this.textBoxHex.Multiline = true;
            this.textBoxHex.Name = "textBoxHex";
            this.textBoxHex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxHex.Size = new System.Drawing.Size(505, 329);
            this.textBoxHex.TabIndex = 2;
            this.textBoxHex.WordWrap = false;
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAs.Enabled = false;
            this.buttonSaveAs.Location = new System.Drawing.Point(444, 23);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAs.TabIndex = 4;
            this.buttonSaveAs.Text = "Save as...";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.AddExtension = false;
            // 
            // PageAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.comboBoxSelect);
            this.Controls.Add(this.tabControlAtt);
            this.Name = "PageAttachments";
            this.Size = new System.Drawing.Size(519, 388);
            this.tabControlAtt.ResumeLayout(false);
            this.tabPagePlain.ResumeLayout(false);
            this.tabPagePlain.PerformLayout();
            this.tabPageHtml.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPageHex.ResumeLayout(false);
            this.tabPageHex.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSelect;
        private System.Windows.Forms.TabControl tabControlAtt;
        private System.Windows.Forms.TabPage tabPagePlain;
        private System.Windows.Forms.TextBox textBoxPlain;
        private System.Windows.Forms.TabPage tabPageHex;
        private System.Windows.Forms.TextBox textBoxHex;
        private System.Windows.Forms.TabPage tabPageHtml;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;


    }
}
