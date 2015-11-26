using System;
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
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSteamLogin_Click(object sender, EventArgs e)
        {
            LoginForm mLoginForm = new LoginForm();
            mLoginForm.ShowDialog();
        }
    }
}
