namespace MpSoft.SmtpFiddler.Plugins.Core
{
    partial class PageDetail
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
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelLabSubject = new System.Windows.Forms.Label();
            this.labelLabTo = new System.Windows.Forms.Label();
            this.labelLabFrom = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.labelLabSize = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelSubject
            // 
            this.labelSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSubject.Location = new System.Drawing.Point(46, 36);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(582, 13);
            this.labelSubject.TabIndex = 11;
            // 
            // labelTo
            // 
            this.labelTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTo.Location = new System.Drawing.Point(46, 19);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(582, 13);
            this.labelTo.TabIndex = 10;
            // 
            // labelFrom
            // 
            this.labelFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFrom.Location = new System.Drawing.Point(46, 2);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(582, 13);
            this.labelFrom.TabIndex = 9;
            // 
            // labelLabSubject
            // 
            this.labelLabSubject.AutoSize = true;
            this.labelLabSubject.Location = new System.Drawing.Point(1, 36);
            this.labelLabSubject.Name = "labelLabSubject";
            this.labelLabSubject.Size = new System.Drawing.Size(46, 13);
            this.labelLabSubject.TabIndex = 8;
            this.labelLabSubject.Text = "Subject:";
            // 
            // labelLabTo
            // 
            this.labelLabTo.AutoSize = true;
            this.labelLabTo.Location = new System.Drawing.Point(1, 19);
            this.labelLabTo.Name = "labelLabTo";
            this.labelLabTo.Size = new System.Drawing.Size(23, 13);
            this.labelLabTo.TabIndex = 7;
            this.labelLabTo.Text = "To:";
            // 
            // labelLabFrom
            // 
            this.labelLabFrom.AutoSize = true;
            this.labelLabFrom.Location = new System.Drawing.Point(1, 2);
            this.labelLabFrom.Name = "labelLabFrom";
            this.labelLabFrom.Size = new System.Drawing.Size(33, 13);
            this.labelLabFrom.TabIndex = 6;
            this.labelLabFrom.Text = "From:";
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.BackColor = System.Drawing.SystemColors.Control;
            this.labelNote.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labelNote.Location = new System.Drawing.Point(1, 69);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(106, 13);
            this.labelNote.TabIndex = 12;
            this.labelNote.Text = "Needs to be finalized";
            // 
            // labelLabSize
            // 
            this.labelLabSize.AutoSize = true;
            this.labelLabSize.Location = new System.Drawing.Point(1, 53);
            this.labelLabSize.Name = "labelLabSize";
            this.labelLabSize.Size = new System.Drawing.Size(30, 13);
            this.labelLabSize.TabIndex = 13;
            this.labelLabSize.Text = "Size:";
            // 
            // labelSize
            // 
            this.labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSize.Location = new System.Drawing.Point(46, 53);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(582, 13);
            this.labelSize.TabIndex = 14;
            // 
            // PageDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelLabSize);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.labelLabSubject);
            this.Controls.Add(this.labelLabTo);
            this.Controls.Add(this.labelLabFrom);
            this.Name = "PageDetail";
            this.Size = new System.Drawing.Size(632, 464);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelLabSubject;
        private System.Windows.Forms.Label labelLabTo;
        private System.Windows.Forms.Label labelLabFrom;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Label labelLabSize;
        private System.Windows.Forms.Label labelSize;


    }
}
