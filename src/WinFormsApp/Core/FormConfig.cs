#region using
using MpSoft.Net.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
#endregion using

namespace MpSoft.SmtpFiddler.Core
{
    public partial class FormConfig:Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender,EventArgs e)
        {
            ActiveControl=buttonOk;

            SmtpListenConfig slc=Config.GetSmtpListenConfig();
            textBoxListenAddress.Text=slc.Endpoint.Address.ToString();
            textBoxListenPort.Text=slc.Endpoint.Port.ToString();

            SmtpForwardConfig sfc=Config.GetSmtpForwardConfig();
            textBoxForwardHost.Text=sfc.Host;
            textBoxForwardPort.Text=sfc.Port.ToString();

            bool proxyMode=Config.FiddleMode==FiddleMode.Proxy;
            radioButtonModeDummy.Checked=!proxyMode;
            radioButtonModeProxy.Checked=proxyMode;
        }

        private void buttonOk_Click(object sender,EventArgs e)
        {
            if (!SetConfigValues())
                return;
            this.DialogResult=DialogResult.OK;
            Config.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            this.Close();
        }

        bool SetConfigValues()
        {
            List<string> errors=new List<string>();

            SmtpListenConfig slc=Config.GetSmtpListenConfig();
            IPAddress tempAddr;
            if (IPAddress.TryParse(textBoxListenAddress.Text,out tempAddr))
                slc.Endpoint.Address=tempAddr;
            else
                errors.Add(groupBoxListen.Text+" - "+labelListenAddress.Text+" - invalid value");
            int tempInt;
            if (TryParsePortNumber(textBoxListenPort.Text,out tempInt))
                slc.Endpoint.Port=tempInt;
            else
                errors.Add(groupBoxListen.Text+" - "+labelListenPort.Text+" - invalid value");

            SmtpForwardConfig sfc=Config.GetSmtpForwardConfig();
            sfc.Host=textBoxForwardHost.Text;
            if (TryParsePortNumber(textBoxForwardPort.Text,out tempInt))
                sfc.Port=tempInt;
            else
                errors.Add(groupBoxForward.Text+" - "+labelForwardPort.Text+" - invalid value");

            Config.FiddleMode=radioButtonModeDummy.Checked?FiddleMode.Dummy:FiddleMode.Proxy;

            bool result=errors.Count==0;
            if (!result)
                MessageBox.Show(string.Join(Environment.NewLine,errors),"Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            return result;
        }

        bool TryParsePortNumber(string value,out int result)
        {
            return (int.TryParse(value,out result))&&(result>-1)&&(result<65536);
        }
    }
}
