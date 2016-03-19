using System;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class SettingsForm : Form
    {
        Manifest manifest;
        bool fullyLoaded = false;
        bool Read_AutoConfirmTrades_IsStartedSecurely = false;
        bool Read_AutoConfirmMarket_IsStartedSecurely = false;
        
        public SettingsForm()
        {
            InitializeComponent();

            // Get latest manifest
            manifest = Manifest.GetManifest(true);

            chkPeriodicChecking.Checked = manifest.PeriodicChecking;
            numPeriodicInterval.Value = manifest.PeriodicCheckingInterval;
            chkCheckAll.Checked = manifest.CheckAllAccounts;







            // set Auto Confirm Trades
            if (manifest.Encrypted)
            {
                /*secure*/
                Read_AutoConfirmTrades_IsStartedSecurely = Manifest.AutoConfirm_IsStartedSecurely("read", false, "trades");
            }
            else {
                /*unsecure*/
                Read_AutoConfirmTrades_IsStartedSecurely = true;
            }
            if (Read_AutoConfirmTrades_IsStartedSecurely == true)
            {
                chkConfirmTrades.Checked = manifest.AutoConfirmTrades;
            }
            else {
                chkConfirmTrades.Checked = false;
            }

            // set Auto Confirm Market
            if (manifest.Encrypted) {
                /*secure*/
                Read_AutoConfirmMarket_IsStartedSecurely = Manifest.AutoConfirm_IsStartedSecurely("read", false, "market");
            } else {
                /*unsecure*/
                Read_AutoConfirmMarket_IsStartedSecurely = true;
            }
            if (Read_AutoConfirmMarket_IsStartedSecurely == true)
            {
                chkConfirmMarket.Checked = manifest.AutoConfirmMarketTransactions;
            }
            else {
                chkConfirmMarket.Checked = false;
            }
            

            SetControlsEnabledState(chkPeriodicChecking.Checked);

            fullyLoaded = true;
        }

        private void SetControlsEnabledState(bool enabled)
        {
            numPeriodicInterval.Enabled = chkCheckAll.Enabled = chkConfirmMarket.Enabled = chkConfirmTrades.Enabled = enabled;
        }

        private bool ShowWarning()
        {
            if (!fullyLoaded) { return false; };

            var result = MessageBox.Show("Warning: enabling this will severely reduce the security of your items! Use of this option is at your own risk. Would you like to continue?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else {
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            manifest.PeriodicChecking = chkPeriodicChecking.Checked;
            manifest.PeriodicCheckingInterval = (int)numPeriodicInterval.Value;
            manifest.CheckAllAccounts = chkCheckAll.Checked;

            // Auto Confirm
            if (chkConfirmTrades.Checked == true)
            {
                Manifest.AutoConfirm_IsStartedSecurely("set", true, "trades");
                manifest.AutoConfirmTrades = chkConfirmTrades.Checked;
            }
            else
            {
                manifest.AutoConfirmTrades = chkConfirmTrades.Checked;
            }

            if (chkConfirmMarket.Checked == true)
            {
                Manifest.AutoConfirm_IsStartedSecurely("set", true, "market");
                manifest.AutoConfirmMarketTransactions = chkConfirmMarket.Checked;
            }
            else
            {
                manifest.AutoConfirmMarketTransactions = chkConfirmMarket.Checked;
            }

            manifest.Save();
            this.Close();
        }

        private void chkPeriodicChecking_CheckedChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(chkPeriodicChecking.Checked);
        }

        private void chkConfirmMarket_CheckedChanged(object sender, EventArgs e)
        {
            if (fullyLoaded)
            {
                if (chkConfirmMarket.Checked)
                {
                    bool EnableAutoConfirm_Market_Warning = ShowWarning();

                    if (EnableAutoConfirm_Market_Warning == true)
                    {
                        bool EnableAutoConfirm_Market_Securely_atStartup = false;
                        if (manifest.Encrypted)
                        {
                            // secure
                            EnableAutoConfirm_Market_Securely_atStartup = Manifest.PromptForSecureActvationAutoConfirm("settings_change");
                        }
                        else {
                            // unsecure
                            EnableAutoConfirm_Market_Securely_atStartup = true;
                        }

                        if (EnableAutoConfirm_Market_Securely_atStartup == true)
                        {
                            Manifest.AutoConfirm_IsStartedSecurely("set", true, "market");
                            manifest.AutoConfirmMarketTransactions = chkConfirmMarket.Checked;
                        }
                        else
                        {
                            manifest.AutoConfirmMarketTransactions = false;
                            chkConfirmMarket.Checked = false;
                        }
                    }
                    else {
                        chkConfirmMarket.Checked = false;
                    }
                }
            }
        }

        private void chkConfirmTrades_CheckedChanged(object sender, EventArgs e)
        {
            if (fullyLoaded)
            {
                if (chkConfirmTrades.Checked)
                {
                    bool EnableAutoConfirm_Trades_Warning = ShowWarning();

                    if (EnableAutoConfirm_Trades_Warning == true)
                    {
                        bool EnableAutoConfirm_Trades_Securely_atStartup = false;
                        if (manifest.Encrypted)
                        {
                            // secure
                            EnableAutoConfirm_Trades_Securely_atStartup = Manifest.PromptForSecureActvationAutoConfirm("settings_change");
                        }
                        else {
                            // unsecure
                            EnableAutoConfirm_Trades_Securely_atStartup = true;
                        }

                        if (EnableAutoConfirm_Trades_Securely_atStartup == true)
                        {
                            Manifest.AutoConfirm_IsStartedSecurely("set", true, "trades");
                            manifest.AutoConfirmTrades = chkConfirmTrades.Checked;
                        }
                        else
                        {
                            manifest.AutoConfirmTrades = false;
                            chkConfirmTrades.Checked = false;
                        }
                    }
                    else {
                        chkConfirmTrades.Checked = false;
                    }
                }
            }
        }
        
        
    }
}
