#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
    public partial class ReplyStatusChoose:Form
    {
        public ReplyStatusChoose()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender,EventArgs e)
        {
            this.Close();
        }

        public int ChoosenOption
        {
            get
            {
                if (radioButtonOk.Checked)
                    return 1;
                else if (radioButtonMailboxUnavailable.Checked)
                    return 2;
                else if (radioButtonInsufficientSystemStorage.Checked)
                    return 3;
                else if (radioButtonError.Checked)
                    return 4;
                else
                    return 0;
            }
        }

        private void ReplyStatusChoose_Load(object sender,EventArgs e)
        {
            ActiveControl=buttonOk;
        }
    }
}
