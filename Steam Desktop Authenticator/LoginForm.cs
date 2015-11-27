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
                        InputForm emailForm = new InputForm("Enter the code sent to your email:");
                        emailForm.ShowDialog();
                        if (emailForm.Canceled)
                        {
                            this.Close();
                            return;
                        }

                        mUserLogin.EmailCode = emailForm.txtBox.Text;
                        break;

                    case LoginResult.NeedCaptcha:
                        System.Diagnostics.Process.Start(String.Format("{0}/public/captcha.php?gid={1}", APIEndpoints.COMMUNITY_BASE, mUserLogin.CaptchaGID));

                        InputForm captchaForm = new InputForm("Enter the captcha that opened in your browser:");
                        captchaForm.ShowDialog();
                        if (captchaForm.Canceled)
                        {
                            this.Close();
                            return;
                        }

                        mUserLogin.CaptchaText = captchaForm.txtBox.Text;
                        break;

                    case LoginResult.Need2FA:
                        InputForm authForm = new InputForm("Enter the code from your authenticator:");
                        authForm.ShowDialog();
                        if (authForm.Canceled)
                        {
                            this.Close();
                            return;
                        }

                        mUserLogin.TwoFactorCode = authForm.txtBox.Text;
                        break;
                }
            }

            // start auth setup process (with another form)


        }
    }
}
