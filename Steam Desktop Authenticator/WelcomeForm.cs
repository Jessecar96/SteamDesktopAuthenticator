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

namespace Steam_Desktop_Authenticator
{
    public partial class WelcomeForm : Form
    {
        private Manifest man;

        public WelcomeForm()
        {
            InitializeComponent();
            man = Manifest.GetManifest();
        }

        private void btnJustStart_Click(object sender, EventArgs e)
        {
            // Mark as not first run anymore
            man.FirstRun = false;
            man.Save();

            showMainForm();
        }

        private void btnImportConfig_Click(object sender, EventArgs e)
        {
            // Let the user select the config dir
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select the folder of your old Steam Desktop Authenticator install";
            DialogResult userClickedOK = folderBrowser.ShowDialog();

            if (userClickedOK == DialogResult.OK)
            {
                string path = folderBrowser.SelectedPath;
                string pathToCopy = null;
                string StarusOk = null;

                if (Directory.Exists(path + "/saFiles"))
                {
                    // User selected the root install dir
                    pathToCopy = path + "/saFiles";
                    StarusOk = "1";
                }
                else
                {
                    // Could not find either.
                    MessageBox.Show("This folder does not contain saFiles folder.\nPlease select the location where you had Steam Desktop Authenticator installed.\n\nIf you are using old .maFile's you will need to import them using the File Impor menu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                if (StarusOk == "1") {
                    // Copy the contents of the config dir to the new config dir
                    string currentPath = Manifest.GetExecutableDir();

                    // Create config dir if we don't have it
                    if (!Directory.Exists(currentPath + "/saFiles"))
                    {
                        Directory.CreateDirectory(currentPath + "/saFiles");
                    }

                    // Copy all files from the old dir to the new one
                    foreach (string newPath in Directory.GetFiles(pathToCopy, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(pathToCopy, currentPath + "/saFiles"), true);
                    }

                    // Set first run in manifest
                    man = Manifest.GetManifest(true);
                    man.FirstRun = false;
                    man.Save();

                    // All done!
                    MessageBox.Show("All accounts and settings have been imported! Press OK to continue.", "Import accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                showMainForm();
            }
            
        }

        private void showMainForm()
        {
            this.Hide();
            new MainForm().Show();
        }

        private void btnAndroidImport_Click(object sender, EventArgs e)
        {
            int oldEntries = man.Entries.Count;

            new PhoneExtractForm().ShowDialog();

            if (man.Entries.Count > oldEntries)
            {
                // Mark as not first run anymore
                man.FirstRun = false;
                man.Save();
                showMainForm();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
