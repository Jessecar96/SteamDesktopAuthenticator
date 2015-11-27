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
    public partial class InputForm : MetroFramework.Forms.MetroForm
    {
        public bool Canceled = false;

        public InputForm(string label)
        {
            InitializeComponent();
            this.labelText.Text = label;
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Canceled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Canceled = true;
            this.Close();
        }
    }
}
