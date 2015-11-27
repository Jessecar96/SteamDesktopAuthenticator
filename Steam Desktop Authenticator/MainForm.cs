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
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private SteamGuardAccount mCurrentAccount = null;
        private SteamGuardAccount[] allAccounts;

        private long steamTime = 0;
        private int steamTimeLeft = 30;

        public MainForm()
        {
            InitializeComponent();
            loadAccountsList();

            pbTimeout.Maximum = 30;
            pbTimeout.Minimum = 0;
            pbTimeout.Value = 30;
        }

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.ShowDialog();
        }

        private void listAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            // Triggered when list item is clicked
            for (int i = 0; i < allAccounts.Length; i++)
            {
                SteamGuardAccount account = allAccounts[i];
                if (account.AccountName == (string) listAccounts.Items[listAccounts.SelectedIndex])
                {
                    mCurrentAccount = account;
                    loadAccountInfo();
                }
            }
        }

        private void loadAccountsList()
        {
            listAccounts.Items.Clear();

            allAccounts = MobileAuthenticatorFileHandler.GetAllAccounts();
            for (int i = 0; i < allAccounts.Length; i++)
            {
                SteamGuardAccount account = allAccounts[i];
                listAccounts.Items.Add(account.AccountName);
            }
        }

        private void loadAccountInfo()
        {
            if (mCurrentAccount != null)
            {
                txtLoginToken.Text = mCurrentAccount.GenerateSteamGuardCodeForTime(steamTime);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(steamTime == 0 || steamTimeLeft <= 0)
            {
                steamTime = TimeAligner.GetSteamTime();
                steamTimeLeft = 30;
                loadAccountInfo();
            }
            steamTimeLeft--;
            pbTimeout.Value = steamTimeLeft;
        }
    }
}
