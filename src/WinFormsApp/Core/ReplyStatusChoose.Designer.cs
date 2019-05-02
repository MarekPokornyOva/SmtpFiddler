namespace MpSoft.SmtpFiddler.Core
{
    partial class ReplyStatusChoose
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
            this.radioButtonOk = new System.Windows.Forms.RadioButton();
            this.radioButtonMailboxUnavailable = new System.Windows.Forms.RadioButton();
            this.radioButtonInsufficientSystemStorage = new System.Windows.Forms.RadioButton();
            this.radioButtonError = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonOk
            // 
            this.radioButtonOk.AutoSize = true;
            this.radioButtonOk.Checked = true;
            this.radioButtonOk.Location = new System.Drawing.Point(12, 40);
            this.radioButtonOk.Name = "radioButtonOk";
            this.radioButtonOk.Size = new System.Drawing.Size(40, 17);
            this.radioButtonOk.TabIndex = 0;
            this.radioButtonOk.TabStop = true;
            this.radioButtonOk.Text = "OK";
            this.radioButtonOk.UseVisualStyleBackColor = true;
            this.radioButtonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // radioButtonMailboxUnavailable
            // 
            this.radioButtonMailboxUnavailable.AutoSize = true;
            this.radioButtonMailboxUnavailable.Location = new System.Drawing.Point(12, 64);
            this.radioButtonMailboxUnavailable.Name = "radioButtonMailboxUnavailable";
            this.radioButtonMailboxUnavailable.Size = new System.Drawing.Size(118, 17);
            this.radioButtonMailboxUnavailable.TabIndex = 1;
            this.radioButtonMailboxUnavailable.Text = "Mailbox unavailable";
            this.radioButtonMailboxUnavailable.UseVisualStyleBackColor = true;
            this.radioButtonMailboxUnavailable.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // radioButtonInsufficientSystemStorage
            // 
            this.radioButtonInsufficientSystemStorage.AutoSize = true;
            this.radioButtonInsufficientSystemStorage.Location = new System.Drawing.Point(12, 88);
            this.radioButtonInsufficientSystemStorage.Name = "radioButtonInsufficientSystemStorage";
            this.radioButtonInsufficientSystemStorage.Size = new System.Drawing.Size(149, 17);
            this.radioButtonInsufficientSystemStorage.TabIndex = 2;
            this.radioButtonInsufficientSystemStorage.Text = "Insufficient system storage";
            this.radioButtonInsufficientSystemStorage.UseVisualStyleBackColor = true;
            this.radioButtonInsufficientSystemStorage.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // radioButtonError
            // 
            this.radioButtonError.AutoSize = true;
            this.radioButtonError.Location = new System.Drawing.Point(12, 112);
            this.radioButtonError.Name = "radioButtonError";
            this.radioButtonError.Size = new System.Drawing.Size(47, 17);
            this.radioButtonError.TabIndex = 3;
            this.radioButtonError.TabStop = true;
            this.radioButtonError.Text = "Error";
            this.radioButtonError.UseVisualStyleBackColor = true;
            this.radioButtonError.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message arrived!";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(180, 137);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // ReplyStatusChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 168);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonError);
            this.Controls.Add(this.radioButtonInsufficientSystemStorage);
            this.Controls.Add(this.radioButtonMailboxUnavailable);
            this.Controls.Add(this.radioButtonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplyStatusChoose";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose reply status";
            this.Load += new System.EventHandler(this.ReplyStatusChoose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonOk;
        private System.Windows.Forms.RadioButton radioButtonMailboxUnavailable;
        private System.Windows.Forms.RadioButton radioButtonInsufficientSystemStorage;
        private System.Windows.Forms.RadioButton radioButtonError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
    }
}