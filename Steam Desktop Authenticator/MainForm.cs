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
using System.Net;
using Newtonsoft.Json;

namespace Steam_Desktop_Authenticator
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private SteamGuardAccount mCurrentAccount = null;
        private SteamGuardAccount[] allAccounts;
        private Manifest mManifest;

        private long steamTime = 0;
        private long currentSteamChunk = 0;

        public MainForm()
        {
            InitializeComponent();

            this.labelVersion.Text = String.Format("v{0}", Application.ProductVersion);
            this.mManifest = Manifest.GetManifest();

            pbTimeout.Maximum = 30;
            pbTimeout.Minimum = 0;
            pbTimeout.Value = 30;

            checkForUpdates();
        }

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.ShowDialog();
            this.loadAccountsList();
        }

        private void listAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            // Triggered when list item is clicked
            for (int i = 0; i < allAccounts.Length; i++)
            {
                SteamGuardAccount account = allAccounts[i];
                if (account.AccountName == (string)listAccounts.Items[listAccounts.SelectedIndex])
                {
                    mCurrentAccount = account;
                    loadAccountInfo();
                }
            }
        }

        private void loadAccountsList()
        {
            mCurrentAccount = null;
            listAccounts.Items.Clear();
            listAccounts.SelectedIndex = -1;

            bool success;
            string passKey = mManifest.PromptForPassKey(out success);
            if (!success)
            {
                this.Close();
                return;
            }

            if (mManifest.Encrypted)
            {
                btnManageEncryption.Text = "Manage Encryption";
            }
            else
            {
                btnManageEncryption.Text = "Setup Encryption";
            }

            btnManageEncryption.Enabled = mManifest.Entries.Count > 0;

            allAccounts = mManifest.GetAllAccounts(passKey);

            if (allAccounts.Length > 0)
            {
                for (int i = 0; i < allAccounts.Length; i++)
                {
                    SteamGuardAccount account = allAccounts[i];
                    listAccounts.Items.Add(account.AccountName);
                }

                listAccounts.SelectedIndex = 0;
            }
            btnDelete.Enabled = btnTradeConfirmations.Enabled = allAccounts.Length > 0;
        }

        private void loadAccountInfo()
        {
            if (mCurrentAccount != null && steamTime != 0)
            {
                txtLoginToken.Text = mCurrentAccount.GenerateSteamGuardCodeForTime(steamTime);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            steamTime = TimeAligner.GetSteamTime();
            currentSteamChunk = steamTime / 30L;

            int secondsUntilChange = (int)(steamTime - (currentSteamChunk * 30L));

            loadAccountInfo();
            if (mCurrentAccount != null)
            {
                pbTimeout.Value = 30 - secondsUntilChange;
            }
        }

        private void btnTradeConfirmations_Click(object sender, EventArgs e)
        {
            if (mCurrentAccount == null) return;

            // Get new cookies every time (sadly)
            mCurrentAccount.RefreshSession();

            ConfirmationFormWeb confirms = new ConfirmationFormWeb(mCurrentAccount);
            confirms.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (mCurrentAccount == null) return;
            string confCode = mCurrentAccount.GenerateSteamGuardCode();
            InputForm confirmationDialog = new InputForm("Removing the authenticator from " + mCurrentAccount.AccountName + ". Enter confirmation code " + confCode);
            confirmationDialog.ShowDialog();

            if (confirmationDialog.Canceled)
            {
                return;
            }

            string enteredCode = confirmationDialog.txtBox.Text.ToUpper();
            if (enteredCode != confCode)
            {
                MessageBox.Show("Confirmation codes do not match. Authenticator has not been unlinked.");
                return;
            }

            bool success = mCurrentAccount.DeactivateAuthenticator();
            if (success)
            {
                MessageBox.Show("Authenticator unlinked. maFile will be deleted after hitting okay. If you need to make a backup, now's the time.");
                this.mManifest.RemoveAccount(mCurrentAccount);
                this.loadAccountsList();
            }
            else
            {
                MessageBox.Show("Authenticator unable to be removed.");
            }
        }

        private void btnManageEncryption_Click(object sender, EventArgs e)
        {
            if (mManifest.Encrypted)
            {
                InputForm currentPassKeyForm = new InputForm("Enter current passkey", true);
                currentPassKeyForm.ShowDialog();

                if (currentPassKeyForm.Canceled)
                {
                    return;
                }

                string curPassKey = currentPassKeyForm.txtBox.Text;

                InputForm changePassKeyForm = new InputForm("Enter new passkey, or leave blank to remove encryption.");
                changePassKeyForm.ShowDialog();

                if (changePassKeyForm.Canceled && !string.IsNullOrEmpty(changePassKeyForm.txtBox.Text))
                {
                    return;
                }

                InputForm changePassKeyForm2 = new InputForm("Confirm new passkey, or leave blank to remove encryption.");
                changePassKeyForm2.ShowDialog();

                if (changePassKeyForm2.Canceled && !string.IsNullOrEmpty(changePassKeyForm.txtBox.Text))
                {
                    return;
                }

                string newPassKey = changePassKeyForm.txtBox.Text;
                string confirmPassKey = changePassKeyForm2.txtBox.Text;

                if (newPassKey != confirmPassKey)
                {
                    MessageBox.Show("Passkeys do not match.");
                    return;
                }

                if (newPassKey.Length == 0)
                {
                    newPassKey = null;
                }

                string action = newPassKey == null ? "remove" : "change";
                if (!mManifest.ChangeEncryptionKey(curPassKey, newPassKey))
                {
                    MessageBox.Show("Unable to " + action + " passkey.");
                }
                else
                {
                    MessageBox.Show("Passkey successfully " + action + "d.");
                    this.loadAccountsList();
                }
            }
            else
            {
                mManifest.PromptSetupPassKey();
                this.loadAccountsList();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            loadAccountsList();
        }

        // Logic for version checking
        private Version newVersion = null;
        private Version currentVersion = null;
        private WebClient updateClient = null;
        private string updateUrl = null;
        private bool startupUpdateCheck = true;

        private void labelUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (newVersion == null || currentVersion == null)
            {
                checkForUpdates();
            }
            else
            {
                compareVersions();
            }
        }

        private void checkForUpdates()
        {
            if (updateClient != null) return;
            updateClient = new WebClient();
            updateClient.DownloadStringCompleted += UpdateClient_DownloadStringCompleted;
            updateClient.Headers.Add("Content-Type", "application/json");
            updateClient.Headers.Add("User-Agent", "Steam Desktop Authenticator");
            updateClient.DownloadStringAsync(new Uri("https://api.github.com/repos/Jessecar96/SteamDesktopAuthenticator/releases/latest"));
        }

        private void compareVersions()
        {
            if (newVersion > currentVersion)
            {
                labelUpdate.Text = "Download new version"; // Show the user a new version is available if they press no
                DialogResult updateDialog = MessageBox.Show("A new version is available! Would you like to download it now?", "New Version", MessageBoxButtons.YesNo);
                if (updateDialog == DialogResult.Yes)
                {
                    Process.Start(updateUrl);
                }
            }
            else
            {
                if (!startupUpdateCheck)
                {
                    MessageBox.Show("You are using the latest version.");
                }
            }

            newVersion = null; // Check the api again next time they check for updates
            updateClient = null; // Set to null to indicate it's done checking
            startupUpdateCheck = false; // Set when it's done checking on startup
        }

        private void UpdateClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                dynamic resultObject = JsonConvert.DeserializeObject(e.Result);
                newVersion = new Version(resultObject.tag_name.Value);
                currentVersion = new Version(Application.ProductVersion);
                updateUrl = resultObject.assets.First.browser_download_url.Value;
                compareVersions();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to check for updates.");
            }
        }
    }
}
