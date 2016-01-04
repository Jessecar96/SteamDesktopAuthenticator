using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        SteamAuth.SteamGuardAccount[] steamAccounts;
        SteamAuth.SteamGuardAccount selectedAccount;
        public ImportWinAuthForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // File Dialog options
            openFileDialog1.Filter = "WinAuth Protected Export (.zip)|*.zip|WinAuth Export (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                txtPassword.Enabled = Path.GetExtension(path) == ".zip";
                txtPath.Text = path;
            }
        }
        
        // Similar to PhoneExtractForm's LoginAccount method since both have the same data at this stage.
        private void LoginAccount()
        {
            LoginForm login = new LoginForm();
            login.androidAccount = selectedAccount;
            login.loginFromAndroid = true;
            login.SetUsername(selectedAccount.AccountName);
            login.ShowDialog();
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var path = txtPath.Text;
            List<SteamAuth.SteamGuardAccount> accountList = new List<SteamAuth.SteamGuardAccount>();
            string[] contents;
            var ext = Path.GetExtension(path);
            if (ext == ".txt")
            {
                contents = File.ReadAllLines(path);
            }
            else if(ext == ".zip")
            {
                ZipFile zf = null;
                List<string> contentsList = new List<string>();
                try
                {
                    FileStream fs = File.OpenRead(path);
                    zf = new ZipFile(fs);
                    if (!String.IsNullOrEmpty(txtPassword.Text))
                    {
                        zf.Password = txtPassword.Text;     // AES encrypted entries are handled automatically
                    }
                    else
                    {
                        return;
                    }
                    foreach (ZipEntry zipEntry in zf)
                    {
                        if (!zipEntry.IsFile)
                        {
                            continue;           // Ignore directories
                        }
                        String entryFileName = zipEntry.Name;
                        // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                        // Optionally match entrynames against a selection list here to skip as desired.
                        // The unpacked length is available in the zipEntry.Size property.

                        // Look for the winauth text file.
                        if(!entryFileName.Contains("winauth"))
                        {
                            continue;
                        }

                        Stream zipStream;
                        try
                        {
                           
                            zipStream = zf.GetInputStream(zipEntry);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Incorrect Password");
                            return;
                        }

                        // Read the contents from the zip stream
                        // The "using" will close the stream even if an exception occurs.
                        using (StreamReader reader = new StreamReader(zipStream))
                        {
                            while (!reader.EndOfStream)
                            {
                                contentsList.Add(reader.ReadLine());
                            }
                        }
                    }
                }
                finally
                {
                    if (zf != null)
                    {
                        zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                        zf.Close(); // Ensure we release resources
                    }
                }
                contents = contentsList.ToArray();
            }
            else
            {
                return;
            }

            foreach (var line in contents)
            {
                // Checking if it's a steam export
                if (line.Contains("steam"))
                {
                    // WinAuth export is similar to http query paramters
                    var collection = ParseQueryString(line);

                    // The data assigned to data is actually valid json
                    var jsondata = collection.GetValues("data")[0];
                    var account = JsonConvert.DeserializeObject<SteamAuth.SteamGuardAccount>(jsondata);

                    // WinAuth stores DeviceID outside of the data json
                    account.DeviceID = collection.GetValues("deviceid")[0];

                    accountList.Add(account);
                    listAccounts.Items.Add(account.AccountName);
                }
            }
            steamAccounts = accountList.ToArray();
        }

        // Function copied from WinAuth code - WinAuth/WinAuthHelper.cs.
        /// <summary>
        /// Our own version of HttpUtility.ParseQueryString so we can remove the reference to System.Web
        /// which is not available in client profile.
        /// </summary>
        /// <param name="qs">string query string</param>
        /// <returns>collection of name value pairs</returns>
        public static NameValueCollection ParseQueryString(string qs)
        {
            NameValueCollection pairs = new NameValueCollection();

            // ignore blanks and remove initial "?"
            if (string.IsNullOrEmpty(qs) == true)
            {
                return pairs;
            }
            if (qs.StartsWith("?") == true)
            {
                qs = qs.Substring(1);
            }

            // get each a=b&... key-value pair
            foreach (string p in qs.Split('&'))
            {
                string[] keypair = p.Split('=');
                string key = keypair[0];
                string v = (keypair.Length >= 2 ? keypair[1] : null);
                if (string.IsNullOrEmpty(v) == false)
                {
                    // decode (without using System.Web)
                    string newv;
                    while ((newv = Uri.UnescapeDataString(v)) != v)
                    {
                        v = newv;
                    }
                }
                pairs.Add(key, v);
            }

            return pairs;
        }

        private void listAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var accountName = (string)listAccounts.SelectedItem;
            for (int i = 0; i < steamAccounts.Length; i++)
            {
                var account = steamAccounts[i];
                if (account.AccountName == (string)listAccounts.Items[listAccounts.SelectedIndex])
                {
                    selectedAccount = account;
                    break;
                }
            }
        }
    }
}
