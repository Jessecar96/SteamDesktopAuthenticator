using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class CaptchaForm : Form
    {
        public bool Canceled = false;
        public string CaptchaGID = "";
        public string CaptchaURL = "";
        public string CaptchaCode
        {
            get
            {
                return this.txtBox.Text;
            }
        }

        public CaptchaForm(string GID)
        {
            this.CaptchaGID = GID;
            this.CaptchaURL = "https://steamcommunity.com/public/captcha.php?gid=" + GID;
            InitializeComponent();
            this.pictureBoxCaptcha.Load(CaptchaURL);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Canceled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Canceled = true;
            this.Close();
        }

        private void CaptchaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
