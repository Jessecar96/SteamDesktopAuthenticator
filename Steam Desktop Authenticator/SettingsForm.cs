using System;
using System.Drawing;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class SettingsForm : Form
    {
        Manifest manifest;
        bool fullyLoaded = false;
        public bool darkModeEnabled => chkEnableDarkMode.Checked;

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
            chkEnableDarkMode.Checked = manifest.EnableDarkMode;

            SetControlsEnabledState(chkPeriodicChecking.Checked);

            fullyLoaded = true;
        }

        private void SetControlsEnabledState(bool enabled)
        {
            numPeriodicInterval.Enabled = chkCheckAll.Enabled = chkConfirmMarket.Enabled = chkConfirmTrades.Enabled = enabled;
        }

        private void ShowWarning(CheckBox affectedBox)
        {
            if (!fullyLoaded) return;

            var result = MessageBox.Show("Warning: enabling this will severely reduce the security of your items! Use of this option is at your own risk. Would you like to continue?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                affectedBox.Checked = false;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (darkModeEnabled)
            {
                this.BackColor = Color.FromArgb(30, 32, 36);

                chkPeriodicChecking.ForeColor = Color.FromArgb(210, 210, 210);

                label1.ForeColor = Color.FromArgb(210, 210, 210);
                numPeriodicInterval.BackColor = Color.FromArgb(30, 32, 36);
                numPeriodicInterval.ForeColor = Color.FromArgb(210, 210, 210);

                chkCheckAll.ForeColor = Color.FromArgb(210, 210, 210);

                chkConfirmMarket.ForeColor = Color.FromArgb(210, 210, 210);

                chkConfirmTrades.ForeColor = Color.FromArgb(210, 210, 210);

                chkEnableDarkMode.ForeColor = Color.FromArgb(210, 210, 210);

            }
            
            
        }



        
        private void btnSave_Click(object sender, EventArgs e)
        {
            manifest.PeriodicChecking = chkPeriodicChecking.Checked;
            manifest.PeriodicCheckingInterval = (int)numPeriodicInterval.Value;
            manifest.CheckAllAccounts = chkCheckAll.Checked;
            manifest.AutoConfirmMarketTransactions = chkConfirmMarket.Checked;
            manifest.AutoConfirmTrades = chkConfirmTrades.Checked;
            manifest.EnableDarkMode = chkEnableDarkMode.Checked;
            
            manifest.Save();
            this.Close();
        }

        private void chkPeriodicChecking_CheckedChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(chkPeriodicChecking.Checked);
        }



        private void chkConfirmMarket_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirmMarket.Checked)
                ShowWarning(chkConfirmMarket);
        }

        private void chkConfirmTrades_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirmTrades.Checked)
                ShowWarning(chkConfirmTrades);
        }

        private void chkEnabledDarkMode_CheckedChanged(object sender, EventArgs e)
        {

        }




    }
}
