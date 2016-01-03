using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class ImportWinAuthForm : Form
    {
        SteamAuth.SteamGuardAccount steamAccount;
        public ImportWinAuthForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // File Dialog options
            openFileDialog1.Filter = "WinAuth Export (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                var contents = File.ReadAllText(path);

                // WinAuth export is similar to http query paramters
                var collection = System.Web.HttpUtility.ParseQueryString(contents);

                // The data assigned to data is actually valid json
                var jsondata = collection.GetValues("data")[0];
                steamAccount = JsonConvert.DeserializeObject<SteamAuth.SteamGuardAccount>(jsondata);
                
                // WinAuth stores DeviceID outside of the data json
                steamAccount.DeviceID = collection.GetValues("deviceid")[0];
                LoginAccount();
            }
        }
        
        // Same as PhoneExtractForm's Login method since both have the same data at this stage.
        private void LoginAccount()
        {
            MessageBox.Show("Account extracted succesfully! Please login to it.");
            LoginForm login = new LoginForm();
            login.androidAccount = steamAccount;
            login.loginFromAndroid = true;
            login.SetUsername(steamAccount.AccountName);
            login.ShowDialog();
            this.Close();
        }
    }
}
