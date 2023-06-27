using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using SteamAuth;
using Newtonsoft.Json;
using System.Net.Http;

namespace Steam_Desktop_Authenticator
{
    public partial class ConfirmationFormWeb : Form
    {
        private readonly HttpClient httpClient;
        private string steamCookies;
        private SteamGuardAccount steamAccount;
        private string tradeID;

        public ConfirmationFormWeb(SteamGuardAccount steamAccount)
        {
            InitializeComponent();
            this.steamAccount = steamAccount;
            this.Text = String.Format("Trade Confirmations - {0}", steamAccount.AccountName);

            CefSettings settings = new CefSettings();
            settings.PersistSessionCookies = false;
            settings.Locale = "en-US";
            settings.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 6P Build/XXXXX; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/47.0.2526.68 Mobile Safari/537.36";
            steamCookies = String.Format("mobileClientVersion=0 (2.1.3); mobileClient=android; steamid={0}; steamLoginSecure={1}; Steam_Language=english; dob=;", steamAccount.Session.SteamID.ToString(), steamAccount.Session.SteamLoginSecure);

            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = false;

            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("Cookie", steamCookies);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 6P Build/XXXXX; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/47.0.2526.68 Mobile Safari/537.36");
            
        }
        private async Task LoadData()
        {
            this.splitContainer1.Panel2.Controls.Clear();

            try
            {
                var httpResponse = await httpClient.GetStringAsync(steamAccount.GenerateConfirmationURL());

                var response = JsonConvert.DeserializeObject<ConfirmationsResponse>(httpResponse);

                if (response.Success)
                {
                    if(response.Confirmations != null && response.Confirmations.Length > 0)
                    {
                        foreach (var confirmation in response.Confirmations)
                        {
                            Panel panel = new Panel() { Dock = DockStyle.Top, Height = 100, BackColor = Color.LightSlateGray };

                            PictureBox pictureBox = new PictureBox() { Width = 60, Height = 60, Location = new Point(20, 20), SizeMode = PictureBoxSizeMode.Zoom };
                            pictureBox.Load(confirmation.Icon);
                            panel.Controls.Add(pictureBox);

                            Label nameLabel = new Label() { Text = confirmation.Headline, AutoSize = true, ForeColor = Color.Black, Location = new Point(90, 20) };
                            panel.Controls.Add(nameLabel);

                            ConfirmationButton acceptButton = new ConfirmationButton() { Text = confirmation.Accept, Location = new Point(90, 50), Confirmation = confirmation };
                            acceptButton.Click += btnAccept_Click;
                            panel.Controls.Add(acceptButton);

                            ConfirmationButton cancelButton = new ConfirmationButton() { Text = confirmation.Cancel, Location = new Point(180, 50), Confirmation = confirmation };
                            cancelButton.Click += btnCancel_Click;
                            panel.Controls.Add(cancelButton);

                            Label summaryLabel = new Label() { Text = String.Join(", ", confirmation.Summary), AutoSize = true, ForeColor = Color.Black, Location = new Point(90, 80) };
                            panel.Controls.Add(summaryLabel);

                            this.splitContainer1.Panel2.Controls.Add(panel);
                        }
                    }
                    else
                    {
                        Label errorLabel = new Label() { Text = "Nothing to confirm/cancel", AutoSize = true, ForeColor = Color.Black, Location = new Point(150, 20) };
                        this.splitContainer1.Panel2.Controls.Add(errorLabel);
                    }
                }
                else
                {
                    Label errorLabel = new Label() { Text = "Your steam credentials probably expired, try to reauthenticate.", AutoSize = true, ForeColor = Color.Red, Location = new Point(20, 20) };
                    this.splitContainer1.Panel2.Controls.Add(errorLabel);
                }
            }catch(Exception ex)
            {
                Label errorLabel = new Label() { Text = "Something went wrong: " + ex.Message, AutoSize = true, ForeColor = Color.Red, Location = new Point(20, 20) };
                this.splitContainer1.Panel2.Controls.Add(errorLabel);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            var button = (ConfirmationButton)sender;
            var confirmation = button.Confirmation;
            bool result = steamAccount.AcceptConfirmation(confirmation);

            await this.LoadData();
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var button = (ConfirmationButton)sender;
            var confirmation = button.Confirmation;
            bool result = steamAccount.DenyConfirmation(confirmation);

            await this.LoadData();
        }


        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await this.LoadData();
        }
    }
}
