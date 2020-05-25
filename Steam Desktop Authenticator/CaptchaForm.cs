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
        bool darkModeEnabled = new SettingsForm().darkModeEnabled;
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

        private void CaptchaForm_Load(object sender, EventArgs e)
        {
            if (darkModeEnabled)
            {
                this.BackColor = Color.FromArgb(30, 32, 36);

                labelText.ForeColor = Color.FromArgb(210, 210, 210);

                txtBox.BackColor = Color.FromArgb(30, 32, 36);
                txtBox.ForeColor = Color.FromArgb(210, 210, 210);
                // i can't actually see this in the app so I hope this doesn't break the picture
                pictureBoxCaptcha.BackColor = Color.FromArgb(30, 32, 36);
            }
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
    }
}
