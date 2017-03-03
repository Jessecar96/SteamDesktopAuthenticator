using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class SettingsForm : Form
    {
        Manifest manifest;
        bool fullyLoaded = false;

        bool startupEnabled;
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private string StartupLine
        {
            get
            {
                return "\"" + Application.ExecutablePath + "\" -startup";
            }
        }

        public SettingsForm()
        {
            InitializeComponent();

            // Get latest manifest
            manifest = Manifest.GetManifest(true);

            chkPeriodicChecking.Checked = manifest.PeriodicChecking;
            numPeriodicInterval.Value = manifest.PeriodicCheckingInterval;
            chkCheckAll.Checked = manifest.CheckAllAccounts;
            chkConfirmMarket.Checked = manifest.AutoConfirmMarketTransactions;
            chkConfirmTrades.Checked = manifest.AutoConfirmTrades;

            SetControlsEnabledState(chkPeriodicChecking.Checked);

            fullyLoaded = true;

            startupEnabled = rkApp.GetValue("SDA")?.ToString() == StartupLine;
            chkStartup.Checked = startupEnabled;
            chkStartMin.Enabled = startupEnabled;
            chkStartMin.Checked = manifest.StartupMinimized;
        }

        private void SetControlsEnabledState(bool enabled)
        {
            numPeriodicInterval.Enabled = chkCheckAll.Enabled = chkConfirmMarket.Enabled = chkConfirmTrades.Enabled = enabled;
        }

        private void ShowWarning(CheckBox affectedBox)
        {
            if (!fullyLoaded) return;

            var result = MessageBox.Show("Warning: enabling this will severely reduce the security of your items! Use of this option is at your own risk. Would you like to continue?", "Warning!", MessageBoxButtons.YesNo);
            if(result == DialogResult.No)
            {
                affectedBox.Checked = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            manifest.PeriodicChecking = chkPeriodicChecking.Checked;
            manifest.PeriodicCheckingInterval = (int)numPeriodicInterval.Value;
            manifest.CheckAllAccounts = chkCheckAll.Checked;
            manifest.AutoConfirmMarketTransactions = chkConfirmMarket.Checked;
            manifest.AutoConfirmTrades = chkConfirmTrades.Checked;
            manifest.StartupMinimized = chkStartMin.Checked;
            manifest.Save();

            if (chkStartup.Checked && !startupEnabled)
            {
                rkApp.SetValue("SDA", StartupLine);
            }
            else if (!chkStartup.Checked && startupEnabled)
            {
                rkApp.DeleteValue("SDA", false);
            }

            this.Close();
        }

        private void chkPeriodicChecking_CheckedChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(chkPeriodicChecking.Checked);
        }

        private void chkConfirmMarket_CheckedChanged(object sender, EventArgs e)
        {
            if(chkConfirmMarket.Checked)
                ShowWarning(chkConfirmMarket);
        }

        private void chkConfirmTrades_CheckedChanged(object sender, EventArgs e)
        {
            if(chkConfirmTrades.Checked)
                ShowWarning(chkConfirmTrades);
        }

        private void chkStartup_CheckedChanged(object sender, EventArgs e)
        {
            chkStartMin.Enabled = chkStartup.Checked;
        }
    }
}
