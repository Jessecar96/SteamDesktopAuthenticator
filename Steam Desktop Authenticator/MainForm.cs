using System;
using System.IO;
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
using Squirrel;

namespace Steam_Desktop_Authenticator
{
    public partial class MainForm : Form
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

            // Make sure we don't show that dialog again.
            this.mManifest.FirstRun = false;
            this.mManifest.Save();

            pbTimeout.Maximum = 30;
            pbTimeout.Minimum = 0;
            pbTimeout.Value = 30;

            checkForUpdates();
        }

        async void Squirrel_UpdateApp()
        {
            using (var mgr = new UpdateManager("https://path/to/my/update/folder"))
            {
                await mgr.UpdateApp();
            }
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

            try
            {
                ConfirmationFormWeb confirms = new ConfirmationFormWeb(mCurrentAccount);
                confirms.Show();
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("You are missing a dependency required to view your trade confirmations.\nWould you like to install it now?", "Trade confirmations failed to open", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    new InstallRedistribForm().ShowDialog();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (mCurrentAccount == null) return;

            DialogResult res = MessageBox.Show("Would you like to remove Steam Guard completely?\nYes - Remove Steam Guard completely.\nNo - Switch back to Email authentication.", "Remove Steam Guard", MessageBoxButtons.YesNoCancel);
            int scheme = 0;
            if (res == DialogResult.Yes)
            {
                scheme = 2;
            }
            else if (res == DialogResult.No)
            {
                scheme = 1;
            }
            else if (res == DialogResult.Cancel)
            {
                scheme = 0;
            }

            if (scheme != 0)
            {
                string confCode = mCurrentAccount.GenerateSteamGuardCode();
                InputForm confirmationDialog = new InputForm(String.Format("Remvoing Steam Guard from {0}. Enter this confirmation code: {1}", mCurrentAccount.AccountName, confCode));
                confirmationDialog.ShowDialog();

                if (confirmationDialog.Canceled)
                {
                    return;
                }

                string enteredCode = confirmationDialog.txtBox.Text.ToUpper();
                if (enteredCode != confCode)
                {
                    MessageBox.Show("Confirmation codes do not match. Steam Guard not removed.");
                    return;
                }

                bool success = mCurrentAccount.DeactivateAuthenticator(scheme);
                if (success)
                {
                    MessageBox.Show(String.Format("Steam Guard {0}. maFile will be deleted after hitting okay. If you need to make a backup, now's the time.", (scheme == 2 ? "removed completely" : "switched to emails")));
                    this.mManifest.RemoveAccount(mCurrentAccount);
                    this.loadAccountsList();
                }
                else
                {
                    MessageBox.Show("Steam Guard failed to deactivate.");
                }
            }
            else
            {
                MessageBox.Show("Steam Guard was not removed. No action was taken.");
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

        private void menuImportMaFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "maFiles (.maFile)|*.maFile|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                // Open the selected file to read.
                System.IO.Stream fileStream = openFileDialog1.OpenFile();
                string fileContents = null;

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    fileContents = reader.ReadToEnd();
                }
                fileStream.Close();

                try
                {
                    SteamGuardAccount maFile = JsonConvert.DeserializeObject<SteamGuardAccount>(fileContents);
                    if (maFile.Session.SteamID != 0)
                    {
                        mManifest.SaveAccount(maFile, false);
                        MessageBox.Show("Account Imported!");
                        loadAccountsList();
                    }
                    else
                    {
                        throw new Exception("Invalid SteamID");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to parse JSON file. Import Failed.");
                }
            }
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void removeAccountFromManifestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mManifest.Encrypted)
            {
                MessageBox.Show("You cannot remove accounts from the manifest file while it is encrypted.", "Remove from manifest", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult res = MessageBox.Show("This will remove the selected account from the manifest file.\nUse this to move a maFile to another computer.\nThis will NOT delete your maFile.", "Remove from manifest", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                {
                    mManifest.RemoveAccount(mCurrentAccount, false);
                    MessageBox.Show("Account removed from manifest.\nYou can now move its maFile to another computer and import it using the File menu.", "Remove from manifest");
                    loadAccountsList();
                }
            }
        }

        private void loginAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.acc = mCurrentAccount;
            mLoginForm.refreshLogin = true;
            mLoginForm.ShowDialog();
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
                DialogResult updateDialog = MessageBox.Show(String.Format("A new version is available! Would you like to download it now?\nYou will update from version {0} to {1}", Application.ProductVersion, newVersion.ToString()), "New Version", MessageBoxButtons.YesNo);
                if (updateDialog == DialogResult.Yes)
                {
                    Process.Start(updateUrl);
                }
            }
            else
            {
                if (!startupUpdateCheck)
                {
                    MessageBox.Show(String.Format("You are using the latest version: {0}", Application.ProductVersion));
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
