using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;

namespace Steam_Desktop_Authenticator
{
    public partial class MainForm : Form
    {
        //Settings-declare
        public string Settings_MinimiseToSystemTray;
        public bool Settings_ShowConfirmationListButton;
        public bool Settings_AutoCheckForUpdates;

        private SteamGuardAccount currentAccount = null;
        private SteamGuardAccount[] allAccounts;
        private List<string> updatedSessions = new List<string>();
        private Manifest manifest;

        private bool checkAllAccounts;

        private long steamTime = 0;
        private long currentSteamChunk = 0;
        private string passKey = null;

        // Forms
        private TradePopupForm popupFrm = new TradePopupForm();


        public MainForm()
        {
            InitializeComponent();
        }


        // Form event handlers

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.labelVersion.Text = String.Format("v{0}", Application.ProductVersion);
            this.manifest = Manifest.GetManifest();

            // Make sure we don't show that welcome dialog again
            this.manifest.FirstRun = false;
            this.manifest.Save();

            // Tick first time manually to sync time
            timerSteamGuard_Tick(new object(), EventArgs.Empty);

            if (manifest.Encrypted)
            {
                passKey = manifest.PromptForPassKey();
                if(passKey == null)
                {
                    Application.Exit();
                }

            }
            else
            {
            }

            btnManageEncryption.Enabled = manifest.Entries.Count > 0;

            loadSettings();
            loadAccountsList();

            if (Settings_AutoCheckForUpdates == true) { checkForUpdates(); } 
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            trayIcon.Icon = this.Icon;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (Settings_MinimiseToSystemTray == "minimise") {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Settings_MinimiseToSystemTray == "close")
                {
                    this.Hide();
                    e.Cancel = true;
                }
            }
            else
            {
                Application.Exit();
            } 
        }


        // UI Button handlers

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.ShowDialog();
            this.loadAccountsList();
        }

        private void btnTradeConfirmations_Click(object sender, EventArgs e)
        {
            if (currentAccount == null) return;

            try
            {
                ConfirmationFormWeb confirms = new ConfirmationFormWeb(currentAccount);
                confirms.Show();
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("You are missing a dependency required to view your trade confirmations.\nWould you like to install it now?", "Trade confirmations failed to open", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    new InstallRedistribForm(true).ShowDialog();
                }
            }
        }

        private void btnTradeConfirmationsList_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("No account selected");
                return;
            }
            else
            {
                ConfirmationForm confirmations = new ConfirmationForm(currentAccount);
                confirmations.ShowDialog();
            }
        }


        private void btnManageEncryption_Click(object sender, EventArgs e)
        {
            if (manifest.Encrypted)
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
                if (!manifest.ChangeEncryptionKey(curPassKey, newPassKey))
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
                passKey = manifest.PromptSetupPassKey();
                this.loadAccountsList();
            }
        }

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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtLoginToken.Text == null || txtLoginToken.Text == "") { }
            else {
                Clipboard.SetText(txtLoginToken.Text);
            }
        }


        // Tool strip menu handlers

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuRemoveAccountFromManifest_Click(object sender, EventArgs e)
        {
            string Message = "This will remove the selected account from the manifest file.\n\n";
            Message += "This will NOT delete your.saFile,\nyour file will be moved to:  'accounts removed from manifest'.\n\n";
            if (manifest.Encrypted)
            {
                Message += "Your .saFile will remain encrypted, if you want to remove it and be decrypted remove the encryption first.\n\n";
                Message += ".saFile's that are encrypted can be easily imported back, using your encryption passkey.\n\n";
            }
            Message += "Use this to move a saFile to another computer.";

            DialogResult res = MessageBox.Show(Message, "Remove from manifest", MessageBoxButtons.OKCancel);

            if (res == DialogResult.OK)
            {
                string MoveFileReturn = manifest.MoveAccountToRemovedFromManifest(currentAccount);

                if (MoveFileReturn == null)
                {
                }
                else {
                    // Remove from manifest
                    manifest.RemoveAccount(currentAccount, false);

                    DialogResult ReturnDialog = MessageBox.Show("Account removed from manifest.\nYou can now move the .saFile to another computer and import it using the File menu.\n\nDo you want to open the folder: 'accounts removed from manifest' ???", "Remove from Manifest", MessageBoxButtons.YesNo);
                    if (ReturnDialog == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", @MoveFileReturn);
                    }
                    loadAccountsList();
                }
            }
        }


        // remove account from manifest & export it to .maFile
        private void RemoveAndExportMaFile_Click(object sender, EventArgs e)
        {
            string Title = "Remove from manifest and export .maFile";
            string Message = "";
            Message = "This will remove the selected account from the manifest file.\n\n";
            Message += "This will NOT delete your.saFile,\nyour file will be moved to:  'accounts removed from manifest'.\n\n";
            if (manifest.Encrypted)
            {
                Message += "Your .saFile will remain encrypted, if you want to remove it and be decrypted remove the encryption first.\n\n";
                Message += ".saFile's that are encrypted can be easily imported back, using your encryption passkey.\n\n";

                Message += "This will export a decrypted .maFile.\n\n";
            }
            Message += "Use this to move a saFile to another computer.";

            DialogResult res = MessageBox.Show(Message, Title, MessageBoxButtons.OKCancel);

            if (res == DialogResult.OK)
            {
                string PassKey = null;
                string CancelAction = null;
                if (manifest.Encrypted)
                {
                    // Get user passkey

                    InputForm confirmationDialog = new InputForm("Enter your encryption passkey:");
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.Canceled)
                    {
                        CancelAction = "1";
                        MessageBox.Show("This operation has been cancelled!", Title);
                    }
                    else {
                        PassKey = confirmationDialog.txtBox.Text;
                    }
                }

                // if is not canceled
                if (CancelAction == null)
                {
                    // decrypt the data 
                    string Export_maFile = manifest.ExportAccountAsMaFile(currentAccount, PassKey);


                    if (Export_maFile == "ok")
                    {

                        string MoveFileReturn = manifest.MoveAccountToRemovedFromManifest(currentAccount);

                        if (MoveFileReturn == null)
                        {
                        }
                        else {
                            // Remove from manifest
                            manifest.RemoveAccount(currentAccount, false);

                            // Exported Ok
                            DialogResult ReturnDialog = MessageBox.Show("Done!\nAccount removed from manifest.\n.maFile exported ok to Desktop!\n\nYou can now move the .saFile / .maFile to another computer and import it using the File menu.\n\nDo you want to open the folder: 'accounts removed from manifest' ???", "Remove from Manifest", MessageBoxButtons.YesNo);
                            if (ReturnDialog == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start("explorer.exe", @MoveFileReturn);
                            }
                            loadAccountsList();
                        }
                    }
                    else {
                        // Error msg
                        string ErrorMessage = "This operation has been cancelled!\n\n";
                        if (Export_maFile == "empty") { ErrorMessage += "An unknown error occurred."; }
                        else if (Export_maFile == "ErrorFileName") { ErrorMessage += "Error: Failed to get the file name from manifest.json"; }
                        else if (Export_maFile == "FileNotFound") { ErrorMessage += "Error: Account file not found."; }
                        else if (Export_maFile == "ErrorDecryption") { ErrorMessage += "Error: Invalid passkey."; }
                        else if (Export_maFile == "FailedToVerifyDataWritten") { ErrorMessage += "Error: Failed to verify exported data."; }
                        else if (Export_maFile == "FailedToWriteData") { ErrorMessage += "Error: Failed to export."; }
                        ErrorMessage += "\n\nThis operation has been cancelled!";
                        MessageBox.Show(Export_maFile);
                    }
                }

            }
        }

        private void menuLoginAgain_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.androidAccount = currentAccount;
            mLoginForm.refreshLogin = true;
            mLoginForm.ShowDialog();
        }

        private void menuImportSaFile_Click(object sender, EventArgs e)
        {
            ImportSaFileForm currentImport_saFile_Form = new ImportSaFileForm();
            currentImport_saFile_Form.ShowDialog();
            loadAccountsList();
        }

        private void menuImportMaFile_Click(object sender, EventArgs e)
        {
            ImportMaFileForm currentImport_maFile_Form = new ImportMaFileForm();
            currentImport_maFile_Form.ShowDialog();
            loadAccountsList();
        }

        private void menuImportAndroid_Click(object sender, EventArgs e)
        {
            new PhoneExtractForm().ShowDialog();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
            manifest = Manifest.GetManifest(true);
            loadSettings();
        }

        private void menuDeactivateAuthenticator_Click(object sender, EventArgs e)
        {
            if (currentAccount == null) return;

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
                string confCode = currentAccount.GenerateSteamGuardCode();
                InputForm confirmationDialog = new InputForm(String.Format("Remvoing Steam Guard from {0}. Enter this confirmation code: {1}", currentAccount.AccountName, confCode));
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

                bool success = currentAccount.DeactivateAuthenticator(scheme);
                if (success)
                {
                    MessageBox.Show(String.Format("Steam Guard {0}. saFile will be deleted after hitting okay. If you need to make a backup, now's the time.", (scheme == 2 ? "removed completely" : "switched to emails")));
                    this.manifest.RemoveAccount(currentAccount);
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

        private async void menuRefreshSession_Click(object sender, EventArgs e)
        {
            bool status = await currentAccount.RefreshSessionAsync();
            if(status == true)
            {
                MessageBox.Show("Your session has been refreshed.", "Session refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                manifest.SaveAccount(currentAccount, manifest.Encrypted, passKey);
            } 
            else
            {
                MessageBox.Show("Failed to refresh your session.\nTry again soon.", "Session refresh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Tray menu handlers

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            trayRestore_Click(sender, EventArgs.Empty);
        }

        private void trayRestore_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void trayQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trayTradeConfirmations_Click(object sender, EventArgs e)
        {
            btnTradeConfirmations_Click(sender, e);
        }

        private void trayCopySteamGuard_Click(object sender, EventArgs e)
        {
            if (txtLoginToken.Text != "")
            {
                Clipboard.SetText(txtLoginToken.Text);
            }
        }

        private void trayAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listAccounts.SelectedIndex = trayAccountList.SelectedIndex;
        }


        // Misc UI handlers

        private async void listAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < allAccounts.Length; i++)
            {
                SteamGuardAccount account = allAccounts[i];
                if (account.AccountName == (string)listAccounts.Items[listAccounts.SelectedIndex])
                {
                    trayAccountList.Text = account.AccountName;
                    currentAccount = account;
                    loadAccountInfo();
                    await UpdateCurrentSession();
                    break;
                }
            }
        }

        private void txtAccSearch_TextChanged(object sender, EventArgs e)
        {
            List<string> names = new List<string>(getAllNames());
            names = names.FindAll(new Predicate<string>(IsFilter));

            listAccounts.Items.Clear();
            listAccounts.Items.AddRange(names.ToArray());

            trayAccountList.Items.Clear();
            trayAccountList.Items.AddRange(names.ToArray());
        }


        // Timers

        private async void timerSteamGuard_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "Aligning time with Steam...";
            steamTime = await TimeAligner.GetSteamTimeAsync();
            lblStatus.Text = "";

            currentSteamChunk = steamTime / 30L;
            int secondsUntilChange = (int)(steamTime - (currentSteamChunk * 30L));

            loadAccountInfo();
            if (currentAccount != null)
            {
                pbTimeout.Value = 30 - secondsUntilChange;
            }
        }

        private async void timerTradesPopup_Tick(object sender, EventArgs e)
        {
            if (currentAccount == null || popupFrm.Visible) return;

            List < Confirmation > confs = new List<Confirmation>();
            SteamGuardAccount[] accs =
            checkAllAccounts ? allAccounts : new SteamGuardAccount[] { currentAccount };

            try
            {
                lblStatus.Text = "Checking confirmations...";

                foreach (var item in accs)
                {
                    Confirmation[] tmp = await currentAccount.FetchConfirmationsAsync();
                    confs.AddRange(tmp);
                }

                lblStatus.Text = "";

                if (confs.Count == 0) return;

                popupFrm.Confirmation = confs.ToArray();
                popupFrm.Popup();
            }
            catch (SteamGuardAccount.WGTokenInvalidException)
            {
                lblStatus.Text = "";
            }
        }


        // Other methods

        /// <summary>
        /// Load UI with the current account info, this is run every second
        /// </summary>
        private void loadAccountInfo()
        {
            if (currentAccount != null && steamTime != 0)
            {
                popupFrm.Account = currentAccount;
                txtLoginToken.Text = currentAccount.GenerateSteamGuardCodeForTime(steamTime);
                groupAccount.Text = "Account: " + currentAccount.AccountName;
                accountToolStripMenuItem.Text = "Account: " + currentAccount.AccountName;
            }
        }

        /// <summary>
        /// Decrypts files and populates list UI with accounts
        /// </summary>
        private void loadAccountsList()
        {
            currentAccount = null;

            listAccounts.Items.Clear();
            listAccounts.SelectedIndex = -1;

            trayAccountList.Items.Clear();
            trayAccountList.SelectedIndex = -1;


            if (manifest.Encrypted)
            {
                btnManageEncryption.Text = "Manage Encryption";

                importAccountToolStripMenuItem.Enabled = false;
                importAccountToolStripMenuItem.Text = "Import Account - Disable the Encryption First";
            }
            else
            {
                btnManageEncryption.Text = "Setup Encryption";

                importAccountToolStripMenuItem.Enabled = true;
                importAccountToolStripMenuItem.Text = "Import Account";
            }

            btnManageEncryption.Enabled = manifest.Entries.Count > 0;



            allAccounts = manifest.GetAllAccounts(passKey);

            if (allAccounts.Length > 0)
            {
                for (int i = 0; i < allAccounts.Length; i++)
                {
                    SteamGuardAccount account = allAccounts[i];
                    listAccounts.Items.Add(account.AccountName);
                    trayAccountList.Items.Add(account.AccountName);
                }

                listAccounts.SelectedIndex = 0;
                trayAccountList.SelectedIndex = 0;
            }
            menuDeactivateAuthenticator.Enabled = btnTradeConfirmations.Enabled = btnTradeConfirmationsList.Enabled = menuLoginAgain.Enabled = menuRefreshSession.Enabled = menuRemoveAccountFromManifest.Enabled = RemoveAndExportMaFile.Enabled = menuDeactivateAuthenticator.Enabled = allAccounts.Length > 0;
            
        }

        /// <summary>
        /// Reload the session of the current account
        /// </summary>
        /// <returns></returns>
        private async Task UpdateCurrentSession()
        {
            await UpdateSession(currentAccount);
        }
        
        private async Task UpdateSession(SteamGuardAccount account)
        {
            if (account == null) return;
            if (updatedSessions.Contains(account.AccountName)) return;

            lblStatus.Text = "Refreshing session...";
            btnTradeConfirmations.Enabled = false;
            btnTradeConfirmationsList.Enabled = false;

            await currentAccount.RefreshSessionAsync();
            updatedSessions.Add(account.AccountName);

            lblStatus.Text = "";
            btnTradeConfirmations.Enabled = true;
            btnTradeConfirmationsList.Enabled = true;
        }

        private void listAccounts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    int to = listAccounts.SelectedIndex - (e.KeyCode == Keys.Up ? 1 : -1);
                    manifest.MoveEntry(listAccounts.SelectedIndex, to);
                    loadAccountsList();
                }
                return;
            }

            if (!IsKeyAChar(e.KeyCode) && !IsKeyADigit(e.KeyCode))
            {
                return;
            }

            txtAccSearch.Focus();
            txtAccSearch.Text = e.KeyCode.ToString();
            txtAccSearch.SelectionStart = 1;
        }

        private static bool IsKeyAChar(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        private static bool IsKeyADigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        private bool IsFilter(string f)
        {
            if (txtAccSearch.Text.StartsWith("~"))
            {
                try
                {
                    return Regex.IsMatch(f, txtAccSearch.Text);
                }
                catch (Exception)
                {
                    return true;
                }

            }
            else
            {
                return f.Contains(txtAccSearch.Text);
            }
        }

        private string[] getAllNames()
        {
            string[] itemArray = new string[allAccounts.Length];
            for (int i = 0; i < itemArray.Length; i++)
            {
                itemArray[i] = allAccounts[i].AccountName;
            }
            return itemArray;
        }

        private void loadSettings()
        {
            Settings_MinimiseToSystemTray = manifest.MinimiseToSystemTray;
            timerTradesPopup.Enabled = manifest.PeriodicChecking;
            timerTradesPopup.Interval = manifest.PeriodicCheckingInterval * 1000;
            Settings_ShowConfirmationListButton = manifest.ShowConfirmationListButton;
            Settings_AutoCheckForUpdates = manifest.AutoCheckForUpdates;

            // apply settings
            //-------------------------

            // Quit btn under x
            if (Settings_MinimiseToSystemTray == "close")
            {
                quitUnderXToolStripMenuItem.Visible = true;
            }
            else
            {
                quitUnderXToolStripMenuItem.Visible = false;
            }
            if (Settings_MinimiseToSystemTray == "hide")
            {
                trayIcon.Visible = false;
            }
            else
            {
                trayIcon.Visible = true;
            }

            // Show/Hide Confirmations List btn - update GUI
            if (Settings_ShowConfirmationListButton == true)
            {
                //groupAccount
                this.groupAccount.Controls.Add(this.btnTradeConfirmations);
                this.groupAccount.Controls.Add(this.btnTradeConfirmationsList);
                this.groupAccount.Location = new System.Drawing.Point(12, 150);
                this.groupAccount.Size = new System.Drawing.Size(327, 104);

                //btnTradeConfirmations
                btnTradeConfirmations.Location = new System.Drawing.Point(6, 15);
                btnTradeConfirmations.Size = new System.Drawing.Size(315, 36);
                btnTradeConfirmations.Text = "View Trade Confirmations in Browser";

                // btnTradeConfirmationsList
                btnTradeConfirmationsList.Visible = true;
                this.btnTradeConfirmationsList.Location = new System.Drawing.Point(6, 60);
                this.btnTradeConfirmationsList.Size = new System.Drawing.Size(313, 36);

                // listAccounts
                this.listAccounts.Location = new System.Drawing.Point(12, 260);
                this.listAccounts.Size = new System.Drawing.Size(327, 173);

                /* Backup code

                //groupAccount
                this.groupAccount.Location = new System.Drawing.Point(12, 150);
                this.groupAccount.Size = new System.Drawing.Size(327, 104);

                //btnTradeConfirmations
                btnTradeConfirmations.Location = new System.Drawing.Point(6, 15);
                btnTradeConfirmations.Size = new System.Drawing.Size(315, 36);
                btnTradeConfirmations.Text = "View Trade Confirmations in Browser";

                // btnTradeConfirmationsList
                btnTradeConfirmationsList.Visible = true;
                this.btnTradeConfirmationsList.Location = new System.Drawing.Point(6, 60);
                this.btnTradeConfirmationsList.Size = new System.Drawing.Size(313, 36);

                // listAccounts
                this.listAccounts.Location = new System.Drawing.Point(12, 260);
                this.listAccounts.Size = new System.Drawing.Size(327, 173);
                */
            }
            else
            {
                //groupAccount
                this.groupAccount.Location = new System.Drawing.Point(12, 154);
                this.groupAccount.Size = new System.Drawing.Size(327, 61);

                //btnTradeConfirmations
                this.btnTradeConfirmations.Location = new System.Drawing.Point(6, 18);
                this.btnTradeConfirmations.Size = new System.Drawing.Size(313, 36);
                btnTradeConfirmations.Text = "View Trade Confirmations";

                // btnTradeConfirmationsList
                btnTradeConfirmationsList.Visible = false;
                this.btnTradeConfirmationsList.Location = new System.Drawing.Point(6, 18);
                this.btnTradeConfirmationsList.Size = new System.Drawing.Size(313, 36);

                // listAccounts
                this.listAccounts.Location = new System.Drawing.Point(12, 221);
                this.listAccounts.Size = new System.Drawing.Size(327, 212);

                /* backup code
                //groupAccount
                this.groupAccount.Location = new System.Drawing.Point(12, 155);
                this.groupAccount.Size = new System.Drawing.Size(327, 61);

                //btnTradeConfirmations
                this.btnTradeConfirmations.Location = new System.Drawing.Point(6, 18);
                this.btnTradeConfirmations.Size = new System.Drawing.Size(313, 36);
                btnTradeConfirmations.Text = "View Trade Confirmations";

                // btnTradeConfirmationsList
                btnTradeConfirmationsList.Visible = false;
                this.btnTradeConfirmationsList.Location = new System.Drawing.Point(6, 18);
                this.btnTradeConfirmationsList.Size = new System.Drawing.Size(313, 36);

                // listAccounts
                this.listAccounts.Location = new System.Drawing.Point(12, 221);
                this.listAccounts.Size = new System.Drawing.Size(327, 212);
                */
            }

        }


        // Logic for version checking
        private Version newVersion = null;
        private Version currentVersion = null;
        private WebClient updateClient = null;
        private string updateUrl = null;
        private bool startupUpdateCheck = true;

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
    }
}
