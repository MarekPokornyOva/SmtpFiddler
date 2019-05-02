namespace MpSoft.SmtpFiddler.Core
{
    partial class FormConfig
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxListen = new System.Windows.Forms.GroupBox();
            this.textBoxListenPort = new System.Windows.Forms.TextBox();
            this.labelListenPort = new System.Windows.Forms.Label();
            this.textBoxListenAddress = new System.Windows.Forms.TextBox();
            this.labelListenAddress = new System.Windows.Forms.Label();
            this.groupBoxForward = new System.Windows.Forms.GroupBox();
            this.textBoxForwardPort = new System.Windows.Forms.TextBox();
            this.labelForwardPort = new System.Windows.Forms.Label();
            this.textBoxForwardHost = new System.Windows.Forms.TextBox();
            this.labelForwardHost = new System.Windows.Forms.Label();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButtonModeDummy = new System.Windows.Forms.RadioButton();
            this.radioButtonModeProxy = new System.Windows.Forms.RadioButton();
            this.groupBoxListen.SuspendLayout();
            this.groupBoxForward.SuspendLayout();
            this.groupBoxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(99, 242);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(180, 242);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxListen
            // 
            this.groupBoxListen.Controls.Add(this.textBoxListenPort);
            this.groupBoxListen.Controls.Add(this.labelListenPort);
            this.groupBoxListen.Controls.Add(this.textBoxListenAddress);
            this.groupBoxListen.Controls.Add(this.labelListenAddress);
            this.groupBoxListen.Location = new System.Drawing.Point(12, 12);
            this.groupBoxListen.Name = "groupBoxListen";
            this.groupBoxListen.Size = new System.Drawing.Size(243, 70);
            this.groupBoxListen.TabIndex = 9;
            this.groupBoxListen.TabStop = false;
            this.groupBoxListen.Text = "Listen endpoint";
            // 
            // textBoxListenPort
            // 
            this.textBoxListenPort.Location = new System.Drawing.Point(58, 43);
            this.textBoxListenPort.Name = "textBoxListenPort";
            this.textBoxListenPort.Size = new System.Drawing.Size(179, 20);
            this.textBoxListenPort.TabIndex = 3;
            // 
            // labelListenPort
            // 
            this.labelListenPort.AutoSize = true;
            this.labelListenPort.Location = new System.Drawing.Point(7, 46);
            this.labelListenPort.Name = "labelListenPort";
            this.labelListenPort.Size = new System.Drawing.Size(26, 13);
            this.labelListenPort.TabIndex = 2;
            this.labelListenPort.Text = "Port";
            // 
            // textBoxListenAddress
            // 
            this.textBoxListenAddress.Location = new System.Drawing.Point(58, 17);
            this.textBoxListenAddress.Name = "textBoxListenAddress";
            this.textBoxListenAddress.Size = new System.Drawing.Size(179, 20);
            this.textBoxListenAddress.TabIndex = 1;
            // 
            // labelListenAddress
            // 
            this.labelListenAddress.AutoSize = true;
            this.labelListenAddress.Location = new System.Drawing.Point(7, 20);
            this.labelListenAddress.Name = "labelListenAddress";
            this.labelListenAddress.Size = new System.Drawing.Size(45, 13);
            this.labelListenAddress.TabIndex = 0;
            this.labelListenAddress.Text = "Address";
            // 
            // groupBoxForward
            // 
            this.groupBoxForward.Controls.Add(this.textBoxForwardPort);
            this.groupBoxForward.Controls.Add(this.labelForwardPort);
            this.groupBoxForward.Controls.Add(this.textBoxForwardHost);
            this.groupBoxForward.Controls.Add(this.labelForwardHost);
            this.groupBoxForward.Location = new System.Drawing.Point(12, 88);
            this.groupBoxForward.Name = "groupBoxForward";
            this.groupBoxForward.Size = new System.Drawing.Size(243, 70);
            this.groupBoxForward.TabIndex = 10;
            this.groupBoxForward.TabStop = false;
            this.groupBoxForward.Text = "Forward";
            // 
            // textBoxForwardPort
            // 
            this.textBoxForwardPort.Location = new System.Drawing.Point(58, 43);
            this.textBoxForwardPort.Name = "textBoxForwardPort";
            this.textBoxForwardPort.Size = new System.Drawing.Size(179, 20);
            this.textBoxForwardPort.TabIndex = 3;
            // 
            // labelForwardPort
            // 
            this.labelForwardPort.AutoSize = true;
            this.labelForwardPort.Location = new System.Drawing.Point(7, 46);
            this.labelForwardPort.Name = "labelForwardPort";
            this.labelForwardPort.Size = new System.Drawing.Size(26, 13);
            this.labelForwardPort.TabIndex = 2;
            this.labelForwardPort.Text = "Port";
            // 
            // textBoxForwardHost
            // 
            this.textBoxForwardHost.Location = new System.Drawing.Point(58, 17);
            this.textBoxForwardHost.Name = "textBoxForwardHost";
            this.textBoxForwardHost.Size = new System.Drawing.Size(179, 20);
            this.textBoxForwardHost.TabIndex = 1;
            // 
            // labelForwardHost
            // 
            this.labelForwardHost.AutoSize = true;
            this.labelForwardHost.Location = new System.Drawing.Point(7, 20);
            this.labelForwardHost.Name = "labelForwardHost";
            this.labelForwardHost.Size = new System.Drawing.Size(29, 13);
            this.labelForwardHost.TabIndex = 0;
            this.labelForwardHost.Text = "Host";
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonModeProxy);
            this.groupBoxMode.Controls.Add(this.radioButtonModeDummy);
            this.groupBoxMode.Location = new System.Drawing.Point(12, 164);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(243, 70);
            this.groupBoxMode.TabIndex = 11;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Mode";
            // 
            // radioButtonModeDummy
            // 
            this.radioButtonModeDummy.AutoSize = true;
            this.radioButtonModeDummy.Location = new System.Drawing.Point(58, 19);
            this.radioButtonModeDummy.Name = "radioButtonModeDummy";
            this.radioButtonModeDummy.Size = new System.Drawing.Size(60, 17);
            this.radioButtonModeDummy.TabIndex = 0;
            this.radioButtonModeDummy.TabStop = true;
            this.radioButtonModeDummy.Text = "Dummy";
            this.radioButtonModeDummy.UseVisualStyleBackColor = true;
            // 
            // radioButtonModeProxy
            // 
            this.radioButtonModeProxy.AutoSize = true;
            this.radioButtonModeProxy.Location = new System.Drawing.Point(58, 43);
            this.radioButtonModeProxy.Name = "radioButtonModeProxy";
            this.radioButtonModeProxy.Size = new System.Drawing.Size(51, 17);
            this.radioButtonModeProxy.TabIndex = 1;
            this.radioButtonModeProxy.TabStop = true;
            this.radioButtonModeProxy.Text = "Proxy";
            this.radioButtonModeProxy.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(267, 273);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.groupBoxForward);
            this.Controls.Add(this.groupBoxListen);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBoxListen.ResumeLayout(false);
            this.groupBoxListen.PerformLayout();
            this.groupBoxForward.ResumeLayout(false);
            this.groupBoxForward.PerformLayout();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxListen;
        private System.Windows.Forms.TextBox textBoxListenPort;
        private System.Windows.Forms.Label labelListenPort;
        private System.Windows.Forms.TextBox textBoxListenAddress;
        private System.Windows.Forms.Label labelListenAddress;
        private System.Windows.Forms.GroupBox groupBoxForward;
        private System.Windows.Forms.TextBox textBoxForwardPort;
        private System.Windows.Forms.Label labelForwardPort;
        private System.Windows.Forms.TextBox textBoxForwardHost;
        private System.Windows.Forms.Label labelForwardHost;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonModeProxy;
        private System.Windows.Forms.RadioButton radioButtonModeDummy;
    }
}