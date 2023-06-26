using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace Steam_Desktop_Authenticator
{
    public partial class MainForm : Form
    {
        private SteamGuardAccount currentAccount = null;
        private SteamGuardAccount[] allAccounts;
        private List<string> updatedSessions = new List<string>();
        private Manifest manifest;
        private static SemaphoreSlim confirmationsSemaphore = new SemaphoreSlim(1, 1);

        private long steamTime = 0;
        private long currentSteamChunk = 0;
        private string passKey = null;
        private bool startSilent = false;

        // Forms
        private TradePopupForm popupFrm = new TradePopupForm();

        public MainForm()
        {
            InitializeComponent();
        }

        public void SetEncryptionKey(string key)
        {
            passKey = key;
        }

        public void StartSilent(bool silent)
        {
            startSilent = silent;
        }

        // Form event handlers

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.manifest = Manifest.GetManifest();

            // Make sure we don't show that welcome dialog again
            this.manifest.FirstRun = false;
            this.manifest.Save();

            // Tick first time manually to sync time
            timerSteamGuard_Tick(new object(), EventArgs.Empty);

            if (manifest.Encrypted)
            {
                if (passKey == null)
                {
                    passKey = manifest.PromptForPassKey();
                    if (passKey == null)
                    {
                        Application.Exit();
                    }
                }

                btnManageEncryption.Text = "Настройки шифрования";
            }
            else
            {
                btnManageEncryption.Text = "Зашифровать";
            }

            btnManageEncryption.Enabled = manifest.Entries.Count > 0;

            loadSettings();
            loadAccountsList();

            if (startSilent)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            trayIcon.Icon = this.Icon;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        // UI Button handlers

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.loadAccountsList();
        }

        private async void btnTradeConfirmations_Click(object sender, EventArgs e)
        {
            if (currentAccount == null) return;

            string oText = btnTradeConfirmations.Text;
            btnTradeConfirmations.Text = "Загрузка...";
            //await RefreshAccountSession(currentAccount);
            btnTradeConfirmations.Text = oText;

            ConfirmationForm confirms = new ConfirmationForm(currentAccount);
            confirms.Show();
        }

        private void btnManageEncryption_Click(object sender, EventArgs e)
        {
            if (manifest.Encrypted)
            {
                InputForm currentPassKeyForm = new InputForm("Введите текущий passkey", true);
                currentPassKeyForm.ShowDialog();

                if (currentPassKeyForm.Canceled)
                {
                    return;
                }

                string curPassKey = currentPassKeyForm.txtBox.Text;

                InputForm changePassKeyForm = new InputForm("Введите новый passkey или оставьте поле пустым, чтобы удалить шифрование.");
                changePassKeyForm.ShowDialog();

                if (changePassKeyForm.Canceled && !string.IsNullOrEmpty(changePassKeyForm.txtBox.Text))
                {
                    return;
                }

                InputForm changePassKeyForm2 = new InputForm("Подтвердите новый passkey или оставьте пустым, чтобы удалить шифрование.");
                changePassKeyForm2.ShowDialog();

                if (changePassKeyForm2.Canceled && !string.IsNullOrEmpty(changePassKeyForm.txtBox.Text))
                {
                    return;
                }

                string newPassKey = changePassKeyForm.txtBox.Text;
                string confirmPassKey = changePassKeyForm2.txtBox.Text;

                if (newPassKey != confirmPassKey)
                {
                    MessageBox.Show("Passkeys не совпадают.");
                    return;
                }

                if (newPassKey.Length == 0)
                {
                    newPassKey = null;
                }

                string action = newPassKey == null ? "remove" : "change";
                if (!manifest.ChangeEncryptionKey(curPassKey, newPassKey))
                {
                    MessageBox.Show("Не в состоянии " + action + " passkey.");
                }
                else
                {
                    MessageBox.Show("Passkey успешно " + action + "d.");
                    this.loadAccountsList();
                }
            }
            else
            {
                passKey = manifest.PromptSetupPassKey();
                this.loadAccountsList();
            }
        }


        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyLoginToken();
        }


        // Tool strip menu handlers

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuRemoveAccountFromManifest_Click(object sender, EventArgs e)
        {
            if (manifest.Encrypted)
            {
                MessageBox.Show("Невозможно удалить учетные записи из файла манифеста, пока он зашифрован.", "Удалить из манифеста", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult res = MessageBox.Show("Это приведет к удалению выбранной учетной записи из файла манифеста.\nИспользуется для перемещения файла на другой компьютер.\nЭто не удалит ваш maFile.", "Удалить из манифеста", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                {
                    manifest.RemoveAccount(currentAccount, false);
                    MessageBox.Show("Аккаунт удален из манифеста.\nТеперь вы можете переместить файл на другой компьютер и импортировать его с помощью меню Файл.", "Удалить из манифеста");
                    loadAccountsList();
                }
            }
        }

        private void menuLoginAgain_Click(object sender, EventArgs e)
        {
            this.PromptRefreshLogin(currentAccount);
        }

        private void menuImportMaFile_Click(object sender, EventArgs e)
        {
            ImportAccountForm currentImport_maFile_Form = new ImportAccountForm();
            currentImport_maFile_Form.ShowDialog();
            loadAccountsList();
        }

        private void menuImportAndroid_Click(object sender, EventArgs e)
        {
            new PhoneExtractForm().ShowDialog();
            loadAccountsList();
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

            DialogResult res = MessageBox.Show("Вы хотите полностью удалить Steam Guard?\nYes-полностью снимите защиту Steam Guard.\nNo-переключиться обратно на проверку подлинности электронной почты.", "Снять Защиту Steam", MessageBoxButtons.YesNoCancel);
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
                InputForm confirmationDialog = new InputForm(String.Format("Удаление Steam Guard из {0}. Введите этот код подтверждения: {1}", currentAccount.AccountName, confCode));
                confirmationDialog.ShowDialog();

                if (confirmationDialog.Canceled)
                {
                    return;
                }

                string enteredCode = confirmationDialog.txtBox.Text.ToUpper();
                if (enteredCode != confCode)
                {
                    MessageBox.Show("Коды подтверждения не совпадают. SteamGuard не снят.");
                    return;
                }

                bool success = currentAccount.DeactivateAuthenticator(scheme);
                if (success)
                {
                    MessageBox.Show(String.Format("Steam Guard {0}. MaFile будет удален после нажатия кнопки ОК. Если вам нужно сделать резервную копию, сейчас самое время.", (scheme == 2 ? "removed completely" : "switched to emails")));
                    this.manifest.RemoveAccount(currentAccount);
                    this.loadAccountsList();
                }
                else
                {
                    MessageBox.Show("Не удалось деактивировать Steam Guard.");
                }
            }
            else
            {
                MessageBox.Show("Steam Guard не был удален. Никаких мер принято не было.");
            }
        }

        private async void menuRefreshSession_Click(object sender, EventArgs e)
        {
            /* bool status = await RefreshAccountSession(currentAccount);
             if (status == true)
             {
                 MessageBox.Show("Ваша сессия была обновлена.", "Обновление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 manifest.SaveAccount(currentAccount, manifest.Encrypted, passKey);
             }
             else
             {
                 MessageBox.Show("Ошибка обновления сессии. Используйте функцию \"Перезайти (Login again)\".", "Обновление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);

             }*/
            MessageBox.Show("Данная функция отключена. Используйте функцию \"Перезайти (Login again)\".", "Обновление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void listAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < allAccounts.Length; i++)
            {
                // Check if index is out of bounds first
                if (i < 0 || listAccounts.SelectedIndex < 0)
                    continue;

                SteamGuardAccount account = allAccounts[i];
                if (account.AccountName == (string)listAccounts.Items[listAccounts.SelectedIndex])
                {
                    trayAccountList.Text = account.AccountName;
                    currentAccount = account;
                    loadAccountInfo();
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
            lblStatus.Text = "Выравнивание времени сo Steam...";
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
            if (currentAccount == null || (popupFrm != null && popupFrm.Visible)) return;
            if (!confirmationsSemaphore.Wait(0))
            {
                return; //Only one thread may access this critical section at once. Mutex is a bad choice here because it'll cause a pileup of threads.
            }

            try
            {
                Dictionary<SteamGuardAccount, List<Confirmation>> confs = new Dictionary<SteamGuardAccount, List<Confirmation>>();
                //List<Confirmation> confs = new List<Confirmation>();
                Dictionary<SteamGuardAccount, List<Confirmation>> autoAcceptConfirmations = new Dictionary<SteamGuardAccount, List<Confirmation>>();

                SteamGuardAccount[] accs =
                    manifest.CheckAllAccounts ? allAccounts : new SteamGuardAccount[] { currentAccount };

                lblStatus.Text = "Проверка подтверждений...";

                foreach (var acc in accs)
                {
                    try
                    {
                        Confirmation[] tmp = acc.FetchConfirmations();
                        foreach (var conf in tmp)
                        {
                            if ((conf.ConfType == Confirmation.ConfirmationType.MarketSellTransaction && manifest.AutoConfirmMarketTransactions) ||
                                (conf.ConfType == Confirmation.ConfirmationType.Trade && manifest.AutoConfirmTrades))
                            {
                                if (!autoAcceptConfirmations.ContainsKey(acc))
                                    autoAcceptConfirmations[acc] = new List<Confirmation>();
                                autoAcceptConfirmations[acc].Add(conf);
                            }
                            else
                            {
                                if (!confs.ContainsKey(acc))
                                    confs[acc] = new List<Confirmation>();
                                confs[acc].Add(conf);
                            }
                            //confs.Add(conf);
                        }
                    }
                    catch (SteamGuardAccount.WGTokenInvalidException)
                    {
                        lblStatus.Text = "Обновление сеанса";
                        //await currentAccount.RefreshSessionAsync(); //Don't save it to the HDD, of course. We'd need their encryption passkey again.
                        lblStatus.Text = "";
                    }
                    catch (SteamGuardAccount.WGTokenExpiredException)
                    {
                        //Prompt to relogin
                        PromptRefreshLogin(acc);
                        break; //Don't bombard a user with login refresh requests if they have multiple accounts. Give them a few seconds to disable the autocheck option if they want.
                    }
                    catch (WebException)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }

                lblStatus.Text = "";

                if (confs.Count > 0)
                {
                    foreach (var acc in confs.Keys)
                    {
                        try
                        {
                            popupFrm.Account = acc;
                            popupFrm.Confirmations = confs[acc].ToArray();
                            popupFrm.Popup();
                        }
                        catch (Exception ex) { popupFrm = new TradePopupForm(); }
                    }
                }
                if (autoAcceptConfirmations.Count > 0)
                {
                    foreach (var acc in autoAcceptConfirmations.Keys)
                    {
                        try
                        {
                            var confirmations = autoAcceptConfirmations[acc].ToArray();
                            acc.AcceptMultipleConfirmations(confirmations);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (SteamGuardAccount.WGTokenInvalidException)
            {
                lblStatus.Text = "";
            }

            confirmationsSemaphore.Release();
        }

        // Other methods

        private void CopyLoginToken()
        {
            string text = txtLoginToken.Text;
            if (String.IsNullOrEmpty(text))
                return;
            Clipboard.SetText(text);
        }

        /// <summary>
        /// Refresh this account's session data using their OAuth Token
        /// </summary>
        /// <param name="account">The account to refresh</param>
        /// <param name="attemptRefreshLogin">Whether or not to prompt the user to re-login if their OAuth token is expired.</param>
        /// <returns></returns>
        private async Task<bool> RefreshAccountSession(SteamGuardAccount account, bool attemptRefreshLogin = true)
        {
            if (account == null) return false;

            try
            {
                bool refreshed = account.RefreshSession();
                return refreshed; //No exception thrown means that we either successfully refreshed the session or there was a different issue preventing us from doing so.
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                if (!attemptRefreshLogin) return false;

                PromptRefreshLogin(account);

                return await RefreshAccountSession(account, false);
                //return true;
            }
        }

        /// <summary>
        /// Display a login form to the user to refresh their OAuth Token
        /// </summary>
        /// <param name="account">The account to refresh</param>
        private void PromptRefreshLogin(SteamGuardAccount account)
        {
            var loginForm = new LoginForm(LoginForm.LoginType.Refresh, account);
            loginForm.ShowDialog();
        }

        /// <summary>
        /// Load UI with the current account info, this is run every second
        /// </summary>
        private void loadAccountInfo()
        {
            if (currentAccount != null && steamTime != 0)
            {
                //popupFrm.Account = currentAccount;
                txtLoginToken.Text = currentAccount.GenerateSteamGuardCodeForTime(steamTime);
                groupAccount.Text = "Аккаунт: " + currentAccount.AccountName;
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
            menuDeactivateAuthenticator.Enabled = btnTradeConfirmations.Enabled = allAccounts.Length > 0;
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
            timerTradesPopup.Enabled = manifest.PeriodicChecking;
            timerTradesPopup.Interval = manifest.PeriodicCheckingInterval * 1000;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                CopyLoginToken();
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
