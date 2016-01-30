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
            string Settings_MinimiseToSystemTray = manifest.MinimiseToSystemTray;
            if (Settings_MinimiseToSystemTray == "close") { radioButton1_SystemTray.Checked = true; }
            else if (Settings_MinimiseToSystemTray == "minimise") { radioButton2_SystemTray.Checked = true; }
            else if (Settings_MinimiseToSystemTray == "none") { radioButton3_SystemTray.Checked = true; }
            else if (Settings_MinimiseToSystemTray == "hide") { radioButton4_SystemTray.Checked = true; }
            else { radioButton1_SystemTray.Checked = true; }

            // set GUI Confirmation Popup
            chkPopupNewConfPeriodicChecking.Checked = manifest.PopupNewConfPeriodicChecking;
            numPeriodicInterval.Value = manifest.PopupNewConfPerCheckingInterval;

            string Settings_PopupNewConfirmationBorder = manifest.PopupNewConfirmationBorder;
            if (Settings_PopupNewConfirmationBorder == "1") { radioBtnBorderConfPopup1.Checked = true; }
            else if (Settings_PopupNewConfirmationBorder == "2") { radioBtnBorderConfPopup2.Checked = true; }
            else { radioBtnBorderConfPopup2.Checked = true; }

            // set check all
            chkCheckAll.Checked = manifest.PopupNewConfPerCheckAllAccounts;

            // set GUI Confirmation List btn
            checkBoxConfirmationListBtn.Checked = manifest.ShowConfirmationListButton;

            // Set GUI
            string Settings_UseGUI = manifest.UseGUI;
            if (Settings_UseGUI == "1") { radioBtnGui1.Checked = true; }
            else if (Settings_UseGUI == "2") { radioBtnGui2.Checked = true; }
            else { radioBtnGui1.Checked = true; }


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
            manifest.PopupNewConfPeriodicChecking = chkPopupNewConfPeriodicChecking.Checked;
            manifest.PopupNewConfPerCheckingInterval = (int)numPeriodicInterval.Value;

            string PopupNewConfirmationBorderValue = "1"; // default value
            if (radioBtnBorderConfPopup1.Checked) { PopupNewConfirmationBorderValue = "1"; }
            if (radioBtnBorderConfPopup2.Checked) { PopupNewConfirmationBorderValue = "2"; }
            manifest.PopupNewConfirmationBorder = PopupNewConfirmationBorderValue;

            // check all
            manifest.PopupNewConfPerCheckAllAccounts = chkCheckAll.Checked;

            // Confirmation List btn
            manifest.ShowConfirmationListButton = checkBoxConfirmationListBtn.Checked;

            // GUI
            if (radioBtnGui1.Checked) { manifest.UseGUI = "1"; }
            if (radioBtnGui2.Checked) { manifest.UseGUI = "2"; }

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
