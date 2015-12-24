using System;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class SettingsForm : Form
    {
        Manifest manifest;

        public SettingsForm()
        {
            InitializeComponent();

            // Get latest manifest
            manifest = Manifest.GetManifest(true);

            // set GUI System Tray
            string MinimiseToSystemTray_Val = manifest.MinimiseToSystemTray;
            if (MinimiseToSystemTray_Val == "close") { radioButton1_SystemTray.Checked = true; }
            if (MinimiseToSystemTray_Val == "minimise") { radioButton2_SystemTray.Checked = true; }
            if (MinimiseToSystemTray_Val == "none") { radioButton3_SystemTray.Checked = true; }
            if (MinimiseToSystemTray_Val == "hide") { radioButton4_SystemTray.Checked = true; }

            // set GUI Confirmation Popup
            chkPeriodicChecking.Checked = manifest.PeriodicChecking;
            numPeriodicInterval.Value = manifest.PeriodicCheckingInterval;

            // set check all
            chkCheckAll.Checked = manifest.CheckAllAccounts;

            // set GUI Confirmation List btn
            checkBoxConfirmationListBtn.Checked = manifest.ShowConfirmationListButton;

            // set GUI Confirmation List btn
            checkBoxAutoCheckForUpdates.Checked = manifest.AutoCheckForUpdates;
        }


        private void SettingsOk_Click(object sender, EventArgs e)
        {
            // System Tray
            string SystemTrayValue = "close"; // default value
            if (radioButton1_SystemTray.Checked) { SystemTrayValue = "close"; }
            if (radioButton2_SystemTray.Checked) { SystemTrayValue = "minimise"; }
            if (radioButton3_SystemTray.Checked) { SystemTrayValue = "none"; }
            if (radioButton4_SystemTray.Checked) { SystemTrayValue = "hide"; }
            manifest.MinimiseToSystemTray = SystemTrayValue;

            // Confirmation Popup
            manifest.PeriodicChecking = chkPeriodicChecking.Checked;
            manifest.PeriodicCheckingInterval = (int)numPeriodicInterval.Value;

            // check all
            manifest.CheckAllAccounts = chkCheckAll.Checked;

            // Confirmation List btn
            manifest.ShowConfirmationListButton = checkBoxConfirmationListBtn.Checked;

            // Updates
            manifest.AutoCheckForUpdates = checkBoxAutoCheckForUpdates.Checked;

            //save
            manifest.Save();
            //close
            this.Close();
        }

        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
