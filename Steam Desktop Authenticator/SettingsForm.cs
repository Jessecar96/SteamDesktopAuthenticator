using Sparrow;
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

            chkPeriodicChecking.Checked = manifest.PeriodicChecking;
            numPeriodicInterval.Value = manifest.PeriodicCheckingInterval;
            chkCheckAll.Checked = manifest.CheckAllAccounts;
            cbLocale.Items.AddRange(LangFile.AvailableLanguages(System.IO.Directory.GetCurrentDirectory() + @"\sdalocales"));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            manifest.PeriodicChecking = chkPeriodicChecking.Checked;
            manifest.PeriodicCheckingInterval = (int)numPeriodicInterval.Value;
            manifest.CheckAllAccounts = chkCheckAll.Checked;
            manifest.LanguageString = cbLocale.SelectedItem.ToString();
            manifest.Save();

            LangFile lang = new LangFile();
            lang.Load(manifest.LanguageString);
            Locale.SelectedLocale = lang;

            this.Close();
        }
    }
}
