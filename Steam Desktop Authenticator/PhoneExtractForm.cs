using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public partial class PhoneExtractForm : Form
    {
        public PhoneExtractForm()
        {
            InitializeComponent();
        }

        PhoneBridge bridge;
        ManualResetEventSlim mreWait = new ManualResetEventSlim(false);
        string SelectedSteamID = "*";

        public SteamAuth.SteamGuardAccount Result;

        private void PhoneExtractForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Bridge_PhoneBridgeError(string msg)
        {
            Log(msg);
            if (msg != "Device not detected")
                ResetAll();
        }

        private void Bridge_DeviceWaited(object sender, EventArgs e)
        {
            Log("Starting");
        }

        private void WaitForDevice()
        {
            Log("Waiting for device...");
            bridge.WaitForDeviceAsync();
        }

        private void Log(string l)
        {
            if (lblLog.InvokeRequired)
            {
                lblLog.Invoke((MethodInvoker)delegate { Log(l); });
            }
            else
            {
                lblLog.Items.Add(l);
                lblLog.TopIndex = lblLog.Items.Count - 1;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            InputForm input = new InputForm("Enter IP of the device");
            input.ShowDialog();
            if (!input.Canceled)
            {
                bridge.ConnectWiFi(input.txtBox.Text);
            }
        }

        private SteamAuth.SteamGuardAccount acc;
        private void Extract()
        {
            acc = bridge.ExtractSteamGuardAccount(SelectedSteamID, SelectedSteamID != "*");

            if (acc != null)
            {
                Result = acc;
                Log("Account extracted succesfully!");
                this.Close();
            }
        }

        private void CheckDevice()
        {
            string state = bridge.GetState();
            if (state == "device")
            {
                tCheckDevice.Stop();
                Log("Starting");
                Extract();
            }
            else if (state == "noadb")
            {
                Log("ADB not found");
                tCheckDevice.Stop();
            }
            else
            {
                Log("Device not connected");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckDevice();
        }

        private void tCheckDevice_Tick(object sender, EventArgs e)
        {
            CheckDevice();
        }

        private void ResetAll()
        {
            bridge.Close();
            Init();
            tCheckDevice.Start();
            lblLog.Items.Clear();
        }

        private void Init()
        {
            bridge = new PhoneBridge();
            bridge.DeviceWaited += Bridge_DeviceWaited;
            bridge.PhoneBridgeError += Bridge_PhoneBridgeError;
            bridge.OutputLog += Bridge_OutputLog;
            bridge.MoreThanOneAccount += Bridge_MoreThanOneAccount;
        }

        private void Bridge_MoreThanOneAccount(List<string> accounts)
        {
            Log("More than one account found");
            tCheckDevice.Stop();

            ListInputForm frm = new ListInputForm(accounts);
            frm.ShowDialog();
            this.SelectedSteamID = accounts[frm.SelectedIndex];
            CheckDevice();
        }

        private void Bridge_OutputLog(string msg)
        {
            Log(msg);
        }

        private void PhoneExtractForm_Shown(object sender, EventArgs e)
        {
            CheckDevice();
        }
    }
}
