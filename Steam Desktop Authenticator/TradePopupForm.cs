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
        private Manifest manifest;
        private SteamGuardAccount acc;
        private List<Confirmation> confirms = new List<Confirmation>();
        private bool deny2, accept2;

        public int TotalConfirmations = 0;
        public int CurrentConfirmationNo = -1;
        public int DisplayFirstSetOfConfirmations = 0;

        //Settings-declare
        public string Settings_PopupNewConfirmationBorder;

        public TradePopupForm()
        {
            InitializeComponent();
            lblStatus.Text = "";

            // read settings
            // Get latest manifest
            manifest = Manifest.GetManifest(true);
            Settings_PopupNewConfirmationBorder = manifest.PopupNewConfirmationBorder;

            if (Settings_PopupNewConfirmationBorder == "1" || Settings_PopupNewConfirmationBorder == "2") { } else { Settings_PopupNewConfirmationBorder = "1"; }


            if (Settings_PopupNewConfirmationBorder == "1")
            {
                this.Text = string.Empty;
                this.ControlBox = false;
            }
            else if (Settings_PopupNewConfirmationBorder == "2")
            {
                this.Text = string.Empty;
                this.ControlBox = false;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }
        }


        // hide resize mouse
        protected override void WndProc(ref Message message)
        {
            const int WM_NCHITTEST = 0x0084;

            if (message.Msg == WM_NCHITTEST)
                return;

            base.WndProc(ref message);
        }

        public SteamGuardAccount Account
        {
            get { return acc; }
            set
            {
                acc = value;
                lblAccount.Text = acc.AccountName;
            }
        }

        public Confirmation[] Confirmation
        {
            get { return confirms.ToArray(); }
            set { confirms = new List<Confirmation>(value); }
        }

        private void TradePopupForm_Load(object sender, EventArgs e)
        {
            this.Location = (Point)Size.Subtract(Screen.GetWorkingArea(this).Size, this.Size);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!accept2)
            {
                // Allow user to confirm first
                lblStatus.Text = "Press Accept again to confirm";
                btnAccept.BackColor = Color.FromArgb(128, 255, 128);
                accept2 = true;
            }
            else
            {
                lblStatus.Text = "Accepting...";
                acc.AcceptConfirmation(confirms[CurrentConfirmationNo]);
                confirms.RemoveAt(CurrentConfirmationNo);
                CurrentConfirmationNo = -1; // show again first confirmation
                Reset();
            }
        }

        private void btnDeny_Click(object sender, EventArgs e)
        {
            if (!deny2)
            {
                lblStatus.Text = "Press Deny again to confirm";
                btnDeny.BackColor = Color.FromArgb(255, 255, 128);
                deny2 = true;
            }
            else
            {
                lblStatus.Text = "Denying...";
                acc.DenyConfirmation(confirms[CurrentConfirmationNo]);
                confirms.RemoveAt(CurrentConfirmationNo);
                CurrentConfirmationNo = -1; // show again first confirmation
                Reset();
            }
        }

        private void Reset(string MoveDirection = "front")
        {
            TotalConfirmations = confirms.Count;

            // this will trigger the GUI to show next confirmation
            if (MoveDirection == "front")
            {
                if (TotalConfirmations > 1 && (TotalConfirmations - 1) > CurrentConfirmationNo)
                {
                    CurrentConfirmationNo++;
                }
                else {
                    CurrentConfirmationNo = 0; // first confirmation
                }
            }
            else if (MoveDirection == "back")
            {
                if (TotalConfirmations > 1 && CurrentConfirmationNo > 1)
                {
                    CurrentConfirmationNo--;
                }
                else {
                    CurrentConfirmationNo = 0; // first confirmation
                }
            }
            else if (MoveDirection == "reset_to_first_confirmation") {
                CurrentConfirmationNo = 0; // first confirmation
            }

            deny2 = false;
            accept2 = false;
            btnAccept.BackColor = Color.FromArgb(192, 255, 192);
            btnDeny.BackColor = Color.FromArgb(255, 255, 192);

            btnAccept.Text = "Accept";
            btnDeny.Text = "Deny";
            lblAccount.Text = "";
            lblStatus.Text = "";

            if (TotalConfirmations == 0)
            {
                this.Hide();
            }
            else
            {

                string Trade_with_user = confirms[CurrentConfirmationNo].ConfirmationDescription.Replace("Trade with ", "");
                lblDesc.Text = Trade_with_user;;
            }
            if (TotalConfirmations == 1)
            {
                BtnPopupConfBack.Visible = false;
                BtnPopupConfNext.Visible = false;
            }
            else if (TotalConfirmations > 1)
            {
                if (CurrentConfirmationNo == 0) { BtnPopupConfBack.Visible = false; } else { BtnPopupConfBack.Visible = true; }
                if (CurrentConfirmationNo == (TotalConfirmations - 1)) { BtnPopupConfNext.Visible = false; } else { BtnPopupConfNext.Visible = true; }
            }


            labelConfirmationNo.Text = "no. " + (1 + CurrentConfirmationNo).ToString();
            label_Info.Text = "Total " + TotalConfirmations.ToString() + " confirmations waiting...";
        }


        private void TradePopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // show next confirmation
                Reset();
                DisplayFirstSetOfConfirmations = 0; // reset
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                this.Close();
            }

        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayFirstSetOfConfirmations = 0; // reset
            this.Hide();
        }


        private void PopupConfNext_Click(object sender, EventArgs e)
        {
            // show next confirmation
            Reset();
        }

        private void PopupConfBack_Click(object sender, EventArgs e)
        {
            // show next confirmation
            Reset("back");
        }

        public void Popup()
        {
            Reset("reset_to_first_confirmation");
            this.Show();
        }
    }
}
