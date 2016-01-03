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
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                var contents = File.ReadAllText(path);
                var collection = System.Web.HttpUtility.ParseQueryString(contents);
                var jsondata = collection.GetValues("data")[0];
                var account = JsonConvert.DeserializeObject<SteamAuth.SteamGuardAccount>(jsondata);
                account.DeviceID = collection.GetValues("deviceid")[0];
            }
        }
    }
}
