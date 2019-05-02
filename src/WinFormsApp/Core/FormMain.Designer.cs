namespace MpSoft.SmtpFiddler.Core
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageDetail = new System.Windows.Forms.TabPage();
			this.tabPagePlain = new System.Windows.Forms.TabPage();
			this.tabPageHtml = new System.Windows.Forms.TabPage();
			this.tabPageAttachments = new System.Windows.Forms.TabPage();
			this.tabPageHex = new System.Windows.Forms.TabPage();
			this.tabPageRaw = new System.Windows.Forms.TabPage();
			this.tabPageCommunication = new System.Windows.Forms.TabPage();
			this.panelDetailQuick = new System.Windows.Forms.Panel();
			this.labelSubject = new System.Windows.Forms.Label();
			this.labelTo = new System.Windows.Forms.Label();
			this.labelFrom = new System.Windows.Forms.Label();
			this.labelLabSubject = new System.Windows.Forms.Label();
			this.labelLabTo = new System.Windows.Forms.Label();
			this.labelLabFrom = new System.Windows.Forms.Label();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.buttonTest = new System.Windows.Forms.Button();
			this.listViewMessages = new System.Windows.Forms.ListView();
			this.columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panelRight = new System.Windows.Forms.Panel();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.panelTop = new System.Windows.Forms.Panel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ToolStripMenuItemResponseChoose = new System.Windows.Forms.ToolStripDropDownButton();
			this.ToolStripMenuItemResponseOk = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemResponseAsk = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemResponseMailboxUnavailable = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemResponseInsufficientSystemStorage = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemResponseError = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButtonForwardMail = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSaveMessages = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonLoadMessages = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonConfiguration = new System.Windows.Forms.ToolStripButton();
			this.panelMain = new System.Windows.Forms.Panel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.comboBoxViewSelect = new System.Windows.Forms.ComboBox();
			this.tabControlMain.SuspendLayout();
			this.panelDetailQuick.SuspendLayout();
			this.panelLeft.SuspendLayout();
			this.panelRight.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlMain
			// 
			this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlMain.Controls.Add(this.tabPageDetail);
			this.tabControlMain.Controls.Add(this.tabPagePlain);
			this.tabControlMain.Controls.Add(this.tabPageHtml);
			this.tabControlMain.Controls.Add(this.tabPageAttachments);
			this.tabControlMain.Controls.Add(this.tabPageHex);
			this.tabControlMain.Controls.Add(this.tabPageRaw);
			this.tabControlMain.Controls.Add(this.tabPageCommunication);
			this.tabControlMain.Location = new System.Drawing.Point(3, 84);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(536, 399);
			this.tabControlMain.TabIndex = 1;
			// 
			// tabPageDetail
			// 
			this.tabPageDetail.Location = new System.Drawing.Point(4, 22);
			this.tabPageDetail.Name = "tabPageDetail";
			this.tabPageDetail.Size = new System.Drawing.Size(528, 395);
			this.tabPageDetail.TabIndex = 5;
			this.tabPageDetail.Text = "Detail";
			this.tabPageDetail.UseVisualStyleBackColor = true;
			// 
			// tabPagePlain
			// 
			this.tabPagePlain.Location = new System.Drawing.Point(4, 22);
			this.tabPagePlain.Name = "tabPagePlain";
			this.tabPagePlain.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePlain.Size = new System.Drawing.Size(528, 373);
			this.tabPagePlain.TabIndex = 0;
			this.tabPagePlain.Text = "Plain";
			this.tabPagePlain.UseVisualStyleBackColor = true;
			// 
			// tabPageHtml
			// 
			this.tabPageHtml.Location = new System.Drawing.Point(4, 22);
			this.tabPageHtml.Name = "tabPageHtml";
			this.tabPageHtml.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHtml.Size = new System.Drawing.Size(528, 395);
			this.tabPageHtml.TabIndex = 1;
			this.tabPageHtml.Text = "Html";
			this.tabPageHtml.UseVisualStyleBackColor = true;
			// 
			// tabPageAttachments
			// 
			this.tabPageAttachments.Location = new System.Drawing.Point(4, 22);
			this.tabPageAttachments.Name = "tabPageAttachments";
			this.tabPageAttachments.Size = new System.Drawing.Size(528, 395);
			this.tabPageAttachments.TabIndex = 2;
			this.tabPageAttachments.Text = "Attachments";
			this.tabPageAttachments.UseVisualStyleBackColor = true;
			// 
			// tabPageHex
			// 
			this.tabPageHex.Location = new System.Drawing.Point(4, 22);
			this.tabPageHex.Name = "tabPageHex";
			this.tabPageHex.Size = new System.Drawing.Size(528, 395);
			this.tabPageHex.TabIndex = 3;
			this.tabPageHex.Text = "Hex";
			this.tabPageHex.UseVisualStyleBackColor = true;
			// 
			// tabPageRaw
			// 
			this.tabPageRaw.Location = new System.Drawing.Point(4, 22);
			this.tabPageRaw.Name = "tabPageRaw";
			this.tabPageRaw.Size = new System.Drawing.Size(528, 395);
			this.tabPageRaw.TabIndex = 4;
			this.tabPageRaw.Text = "Raw";
			this.tabPageRaw.UseVisualStyleBackColor = true;
			// 
			// tabPageCommunication
			// 
			this.tabPageCommunication.Location = new System.Drawing.Point(4, 22);
			this.tabPageCommunication.Name = "tabPageCommunication";
			this.tabPageCommunication.Size = new System.Drawing.Size(528, 395);
			this.tabPageCommunication.TabIndex = 6;
			this.tabPageCommunication.Text = "Protocol communication";
			this.tabPageCommunication.UseVisualStyleBackColor = true;
			// 
			// panelDetailQuick
			// 
			this.panelDetailQuick.Controls.Add(this.labelSubject);
			this.panelDetailQuick.Controls.Add(this.labelTo);
			this.panelDetailQuick.Controls.Add(this.labelFrom);
			this.panelDetailQuick.Controls.Add(this.labelLabSubject);
			this.panelDetailQuick.Controls.Add(this.labelLabTo);
			this.panelDetailQuick.Controls.Add(this.labelLabFrom);
			this.panelDetailQuick.Location = new System.Drawing.Point(3, 3);
			this.panelDetailQuick.Name = "panelDetailQuick";
			this.panelDetailQuick.Size = new System.Drawing.Size(536, 53);
			this.panelDetailQuick.TabIndex = 2;
			// 
			// labelSubject
			// 
			this.labelSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSubject.Location = new System.Drawing.Point(48, 37);
			this.labelSubject.Name = "labelSubject";
			this.labelSubject.Size = new System.Drawing.Size(484, 13);
			this.labelSubject.TabIndex = 5;
			// 
			// labelTo
			// 
			this.labelTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTo.Location = new System.Drawing.Point(48, 20);
			this.labelTo.Name = "labelTo";
			this.labelTo.Size = new System.Drawing.Size(484, 13);
			this.labelTo.TabIndex = 4;
			// 
			// labelFrom
			// 
			this.labelFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelFrom.Location = new System.Drawing.Point(48, 3);
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.Size = new System.Drawing.Size(484, 13);
			this.labelFrom.TabIndex = 3;
			// 
			// labelLabSubject
			// 
			this.labelLabSubject.AutoSize = true;
			this.labelLabSubject.Location = new System.Drawing.Point(3, 37);
			this.labelLabSubject.Name = "labelLabSubject";
			this.labelLabSubject.Size = new System.Drawing.Size(46, 13);
			this.labelLabSubject.TabIndex = 2;
			this.labelLabSubject.Text = "Subject:";
			// 
			// labelLabTo
			// 
			this.labelLabTo.AutoSize = true;
			this.labelLabTo.Location = new System.Drawing.Point(3, 20);
			this.labelLabTo.Name = "labelLabTo";
			this.labelLabTo.Size = new System.Drawing.Size(23, 13);
			this.labelLabTo.TabIndex = 1;
			this.labelLabTo.Text = "To:";
			// 
			// labelLabFrom
			// 
			this.labelLabFrom.AutoSize = true;
			this.labelLabFrom.Location = new System.Drawing.Point(3, 3);
			this.labelLabFrom.Name = "labelLabFrom";
			this.labelLabFrom.Size = new System.Drawing.Size(33, 13);
			this.labelLabFrom.TabIndex = 0;
			this.labelLabFrom.Text = "From:";
			// 
			// panelLeft
			// 
			this.panelLeft.Controls.Add(this.buttonTest);
			this.panelLeft.Controls.Add(this.listViewMessages);
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(200, 483);
			this.panelLeft.TabIndex = 4;
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(12, 407);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(75, 23);
			this.buttonTest.TabIndex = 5;
			this.buttonTest.Text = "Test";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// listViewMessages
			// 
			this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnId,
            this.columnTime,
            this.columnSubject});
			this.listViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMessages.FullRowSelect = true;
			this.listViewMessages.HideSelection = false;
			this.listViewMessages.LabelWrap = false;
			this.listViewMessages.Location = new System.Drawing.Point(0, 0);
			this.listViewMessages.Name = "listViewMessages";
			this.listViewMessages.Size = new System.Drawing.Size(200, 483);
			this.listViewMessages.TabIndex = 4;
			this.listViewMessages.UseCompatibleStateImageBehavior = false;
			this.listViewMessages.View = System.Windows.Forms.View.Details;
			this.listViewMessages.SelectedIndexChanged += new System.EventHandler(this.listViewMessages_SelectedIndexChanged);
			this.listViewMessages.DoubleClick += new System.EventHandler(this.listViewMessages_DoubleClick);
			// 
			// columnId
			// 
			this.columnId.Text = "ID";
			this.columnId.Width = 32;
			// 
			// columnTime
			// 
			this.columnTime.Text = "Time";
			this.columnTime.Width = 93;
			// 
			// columnSubject
			// 
			this.columnSubject.Text = "Subject";
			this.columnSubject.Width = 200;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(200, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 483);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// panelRight
			// 
			this.panelRight.Controls.Add(this.comboBoxViewSelect);
			this.panelRight.Controls.Add(this.panelDetailQuick);
			this.panelRight.Controls.Add(this.tabControlMain);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelRight.Location = new System.Drawing.Point(203, 0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(542, 483);
			this.panelRight.TabIndex = 6;
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "0 messages";
			this.notifyIcon.Visible = true;
			this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.toolStripContainer1);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(745, 25);
			this.panelTop.TabIndex = 0;
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(745, 0);
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(745, 25);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemResponseChoose,
            this.toolStripButtonForwardMail,
            this.toolStripButtonSaveMessages,
            this.toolStripButtonLoadMessages,
            this.toolStripButtonConfiguration});
			this.toolStrip1.Location = new System.Drawing.Point(7, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(131, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// ToolStripMenuItemResponseChoose
			// 
			this.ToolStripMenuItemResponseChoose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripMenuItemResponseChoose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemResponseOk,
            this.ToolStripMenuItemResponseAsk,
            this.ToolStripMenuItemResponseMailboxUnavailable,
            this.ToolStripMenuItemResponseInsufficientSystemStorage,
            this.ToolStripMenuItemResponseError});
			this.ToolStripMenuItemResponseChoose.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemResponseChoose.Image")));
			this.ToolStripMenuItemResponseChoose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripMenuItemResponseChoose.Name = "ToolStripMenuItemResponseChoose";
			this.ToolStripMenuItemResponseChoose.Size = new System.Drawing.Size(29, 22);
			this.ToolStripMenuItemResponseChoose.Text = "Server reply status";
			// 
			// ToolStripMenuItemResponseOk
			// 
			this.ToolStripMenuItemResponseOk.Checked = true;
			this.ToolStripMenuItemResponseOk.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ToolStripMenuItemResponseOk.Name = "ToolStripMenuItemResponseOk";
			this.ToolStripMenuItemResponseOk.Size = new System.Drawing.Size(205, 22);
			this.ToolStripMenuItemResponseOk.Text = "OK";
			this.ToolStripMenuItemResponseOk.Click += new System.EventHandler(this.ToolStripMenuItemResponseChoose_Click);
			// 
			// ToolStripMenuItemResponseAsk
			// 
			this.ToolStripMenuItemResponseAsk.Name = "ToolStripMenuItemResponseAsk";
			this.ToolStripMenuItemResponseAsk.Size = new System.Drawing.Size(205, 22);
			this.ToolStripMenuItemResponseAsk.Text = "Ask";
			this.ToolStripMenuItemResponseAsk.Click += new System.EventHandler(this.ToolStripMenuItemResponseChoose_Click);
			// 
			// ToolStripMenuItemResponseMailboxUnavailable
			// 
			this.ToolStripMenuItemResponseMailboxUnavailable.Name = "ToolStripMenuItemResponseMailboxUnavailable";
			this.ToolStripMenuItemResponseMailboxUnavailable.Size = new System.Drawing.Size(205, 22);
			this.ToolStripMenuItemResponseMailboxUnavailable.Text = "Mailbox unavailable";
			this.ToolStripMenuItemResponseMailboxUnavailable.Click += new System.EventHandler(this.ToolStripMenuItemResponseChoose_Click);
			// 
			// ToolStripMenuItemResponseInsufficientSystemStorage
			// 
			this.ToolStripMenuItemResponseInsufficientSystemStorage.Name = "ToolStripMenuItemResponseInsufficientSystemStorage";
			this.ToolStripMenuItemResponseInsufficientSystemStorage.Size = new System.Drawing.Size(205, 22);
			this.ToolStripMenuItemResponseInsufficientSystemStorage.Text = "Insufficient system storage";
			this.ToolStripMenuItemResponseInsufficientSystemStorage.Click += new System.EventHandler(this.ToolStripMenuItemResponseChoose_Click);
			// 
			// ToolStripMenuItemResponseError
			// 
			this.ToolStripMenuItemResponseError.Name = "ToolStripMenuItemResponseError";
			this.ToolStripMenuItemResponseError.Size = new System.Drawing.Size(205, 22);
			this.ToolStripMenuItemResponseError.Text = "Error";
			this.ToolStripMenuItemResponseError.Click += new System.EventHandler(this.ToolStripMenuItemResponseChoose_Click);
			// 
			// toolStripButtonForwardMail
			// 
			this.toolStripButtonForwardMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonForwardMail.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonForwardMail.Image")));
			this.toolStripButtonForwardMail.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonForwardMail.Name = "toolStripButtonForwardMail";
			this.toolStripButtonForwardMail.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonForwardMail.Text = "Forward messages";
			this.toolStripButtonForwardMail.Click += new System.EventHandler(this.toolStripButtonForwardMail_Click);
			// 
			// toolStripButtonSaveMessages
			// 
			this.toolStripButtonSaveMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSaveMessages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveMessages.Image")));
			this.toolStripButtonSaveMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSaveMessages.Name = "toolStripButtonSaveMessages";
			this.toolStripButtonSaveMessages.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSaveMessages.Text = "Save messages";
			this.toolStripButtonSaveMessages.Click += new System.EventHandler(this.toolStripButtonSaveMessages_Click);
			// 
			// toolStripButtonLoadMessages
			// 
			this.toolStripButtonLoadMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonLoadMessages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadMessages.Image")));
			this.toolStripButtonLoadMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonLoadMessages.Name = "toolStripButtonLoadMessages";
			this.toolStripButtonLoadMessages.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonLoadMessages.Text = "Load messages";
			this.toolStripButtonLoadMessages.Click += new System.EventHandler(this.toolStripButtonLoadMessages_Click);
			// 
			// toolStripButtonConfiguration
			// 
			this.toolStripButtonConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConfiguration.Image")));
			this.toolStripButtonConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonConfiguration.Name = "toolStripButtonConfiguration";
			this.toolStripButtonConfiguration.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonConfiguration.Text = "Setup";
			this.toolStripButtonConfiguration.Click += new System.EventHandler(this.toolStripButtonConfiguration_Click);
			// 
			// panelMain
			// 
			this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMain.Controls.Add(this.panelRight);
			this.panelMain.Controls.Add(this.splitter1);
			this.panelMain.Controls.Add(this.panelLeft);
			this.panelMain.Location = new System.Drawing.Point(0, 25);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(745, 483);
			this.panelMain.TabIndex = 1;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "eml";
			// 
			// openFileDialog
			// 
			this.openFileDialog.AddExtension = false;
			this.openFileDialog.DefaultExt = "eml";
			this.openFileDialog.Multiselect = true;
			// 
			// comboBoxViewSelect
			// 
			this.comboBoxViewSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxViewSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxViewSelect.FormattingEnabled = true;
			this.comboBoxViewSelect.Items.AddRange(new object[] {
            "<Main view>"});
			this.comboBoxViewSelect.Location = new System.Drawing.Point(4, 57);
			this.comboBoxViewSelect.Name = "comboBoxViewSelect";
			this.comboBoxViewSelect.Size = new System.Drawing.Size(535, 21);
			this.comboBoxViewSelect.TabIndex = 3;
			this.comboBoxViewSelect.SelectedIndexChanged += new System.EventHandler(this.comboBoxViewSelect_SelectedIndexChanged);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(745, 508);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.panelMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.Text = "Smtp Fiddler - Mail in the middle";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControlMain.ResumeLayout(false);
			this.panelDetailQuick.ResumeLayout(false);
			this.panelDetailQuick.PerformLayout();
			this.panelLeft.ResumeLayout(false);
			this.panelRight.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagePlain;
        private System.Windows.Forms.TabPage tabPageHtml;
        private System.Windows.Forms.TabPage tabPageDetail;
        private System.Windows.Forms.TabPage tabPageAttachments;
        private System.Windows.Forms.TabPage tabPageHex;
        private System.Windows.Forms.TabPage tabPageRaw;
        private System.Windows.Forms.Panel panelDetailQuick;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelLabSubject;
        private System.Windows.Forms.Label labelLabTo;
        private System.Windows.Forms.Label labelLabFrom;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.ListView listViewMessages;
        private System.Windows.Forms.ColumnHeader columnId;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ColumnHeader columnSubject;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripMenuItemResponseChoose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemResponseOk;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemResponseAsk;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemResponseMailboxUnavailable;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemResponseInsufficientSystemStorage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemResponseError;
        private System.Windows.Forms.ToolStripButton toolStripButtonForwardMail;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveMessages;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadMessages;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButtonConfiguration;
        private System.Windows.Forms.TabPage tabPageCommunication;
		private System.Windows.Forms.ComboBox comboBoxViewSelect;
	}
}

