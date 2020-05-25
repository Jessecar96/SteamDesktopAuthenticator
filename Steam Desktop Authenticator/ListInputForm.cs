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
    public partial class ListInputForm : Form
    {
        // TODO: What is this form used for? What is being inputted?
        bool darkModeEnable = new SettingsForm().darkModeEnabled;
        public ListInputForm(List<string> options)
        {
            Items = options;
            InitializeComponent();
        }

        public int SelectedIndex;
        List<string> Items;

        private void ListInputForm_Load(object sender, EventArgs e)
        {
            foreach (var item in Items)
            {
                lbItems.Items.Add(item);
            }

            if (darkModeEnable)
            {
                this.BackColor = Color.FromArgb(30, 32, 36);

                lbItems.BackColor = Color.FromArgb(30, 32, 36);
                lbItems.ForeColor = Color.FromArgb(210, 210, 210);
            }

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (lbItems.SelectedIndex != -1)
            {
                SelectedIndex = lbItems.SelectedIndex;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
