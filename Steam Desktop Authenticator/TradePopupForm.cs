using SteamAuth;
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
    public partial class TradePopupForm : Form
    {
        public TradePopupForm(SteamGuardAccount account)
        {
            acc = account;
            InitializeComponent();
        }

        private SteamGuardAccount acc;
        public SteamGuardAccount Account
        {
            get { return acc; }
            set { acc = value; }
        }

        private List<Confirmation> confirms = new List<Confirmation>();
        public Confirmation[] Confirmation
        {
            get { return confirms.ToArray(); }
            set { confirms = new List<Confirmation>(value); }
        }

        private void TradePopupForm_Load(object sender, EventArgs e)
        {
            
        }

        private bool deny2, accept2;

        private void button3_Click(object sender, EventArgs e)
        {
            if (!accept2)
            {
                btnAccept.BackColor = Color.FromArgb(128, 255, 128);
                accept2 = true;
            }
            else
            {
                acc.AcceptConfirmation(confirms[0]);
                confirms.RemoveAt(0);
                Reset();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!accept2)
            {
                btnDeny.BackColor = Color.FromArgb(255, 255, 128);
                deny2 = true;
            }
            else
            {
                acc.DenyConfirmation(confirms[0]);
                confirms.RemoveAt(0);
                Reset();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            deny2 = false;
            accept2 = false;
            btnAccept.BackColor = Color.FromArgb(192, 255, 192);
            btnDeny.BackColor = Color.FromArgb(255, 255, 192);

            if (confirms.Count == 0)
            {
                this.Hide();
            } else {
                lblDesc.Text = confirms[0].ConfirmationDescription;
            }
        }

        public void Popup()
        {
            Reset();
            this.Show();
        }
    }
}
