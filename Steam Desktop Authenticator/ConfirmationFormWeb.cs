using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using SteamAuth;
using Newtonsoft.Json;
using System.Net.Http;
using System.Drawing.Drawing2D;

namespace Steam_Desktop_Authenticator
{
    public partial class ConfirmationFormWeb : Form
    {
        private readonly HttpClient httpClient;
        private string steamCookies;
        private SteamGuardAccount steamAccount;

        public ConfirmationFormWeb(SteamGuardAccount steamAccount)
        {
            InitializeComponent();
            this.steamAccount = steamAccount;
            this.Text = String.Format("Trade Confirmations - {0}", steamAccount.AccountName);

            CefSettings settings = new CefSettings();
            settings.PersistSessionCookies = false;
            settings.Locale = "en-US";
            settings.UserAgent = "Mozilla/5.0 (Linux; U; Android 4.1.1; en-us; Google Nexus 4 - 4.1.1 - API 16 - 768x1280 Build/JRO03S) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
            steamCookies = String.Format("mobileClientVersion=0 (2.1.3); mobileClient=android; steamid={0}; steamLoginSecure={1}; Steam_Language=english; dob=;", steamAccount.Session.SteamID.ToString(), steamAccount.Session.SteamLoginSecure);

            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = settings.PersistSessionCookies;

            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("Cookie", steamCookies);
            httpClient.DefaultRequestHeaders.Add("User-Agent", settings.UserAgent);
            
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
                            Panel panel = new Panel() { Dock = DockStyle.Top, Height = 120 };
                            panel.Paint += (s, e) =>
                            {
                                using (LinearGradientBrush brush = new LinearGradientBrush(panel.ClientRectangle,
                                                                                           Color.Black,
                                                                                           Color.DarkCyan,
                                                                                           90F))
                                {
                                    e.Graphics.FillRectangle(brush, panel.ClientRectangle);
                                }
                            };

                            PictureBox pictureBox = new PictureBox() { Width = 60, Height = 60, Location = new Point(20, 20), SizeMode = PictureBoxSizeMode.Zoom };
                            pictureBox.Load(confirmation.Icon);
                            panel.Controls.Add(pictureBox);

                            Label nameLabel = new Label() { 
                                Text = $"{confirmation.Headline}\n{confirmation.Creator.ToString()}",
                                AutoSize = true,
                                ForeColor = Color.Snow,
                                Location = new Point(90, 20),
                                BackColor = Color.Transparent
                            };
                            panel.Controls.Add(nameLabel);

                            ConfirmationButton acceptButton = new ConfirmationButton() { 
                                Text = confirmation.Accept,
                                Location = new Point(90, 50),
                                FlatStyle = FlatStyle.Flat, 
                                FlatAppearance = { BorderSize = 0 },
                                BackColor = Color.Black,
                                ForeColor = Color.Snow,
                                Confirmation = confirmation 
                            };
                            acceptButton.Click += btnAccept_Click;
                            panel.Controls.Add(acceptButton);

                            ConfirmationButton cancelButton = new ConfirmationButton() { 
                                Text = confirmation.Cancel,
                                Location = new Point(180, 50),
                                FlatStyle = FlatStyle.Flat,
                                FlatAppearance = { BorderSize = 0 },
                                BackColor = Color.Black,
                                ForeColor = Color.Snow,
                                Confirmation = confirmation 
                            };
                            cancelButton.Click += btnCancel_Click;
                            panel.Controls.Add(cancelButton);

                            Label summaryLabel = new Label() { 
                                Text = String.Join("\n", confirmation.Summary),
                                AutoSize = true,
                                ForeColor = Color.Snow, Location = new Point(90, 80),
                                BackColor = Color.Transparent
                            };
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
