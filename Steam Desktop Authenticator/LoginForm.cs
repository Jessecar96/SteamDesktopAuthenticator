using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using System.Diagnostics;

namespace Steam_Desktop_Authenticator
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {

        public UserLogin mUserLogin;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            mUserLogin = new UserLogin(username, password);
            LoginResult response = LoginResult.BadCredentials;
            while ((response = mUserLogin.DoLogin()) != LoginResult.LoginOkay)
            {
                switch (response)
                {
                    case LoginResult.NeedEmail:
                        //mUserLogin.EmailCode = code;
                        break;

                    case LoginResult.NeedCaptcha:
                        //APIEndpoints.COMMUNITY_BASE + "/public/captcha.php?gid=" + mUserLogin.CaptchaGID;
                        //mUserLogin.CaptchaText = captchaText;
                        break;

                    case LoginResult.Need2FA:
                        //mUserLogin.TwoFactorCode = code;
                        break;
                }
            }
        }
    }
}
