using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using CefSharp;
using CefSharp.WinForms;
using SteamAuth;

namespace Steam_Desktop_Authenticator
{
    public partial class ConfirmationFormWeb : Form
    {
        private readonly ChromiumWebBrowser browser;
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
            steamCookies = String.Format("mobileClientVersion=0 (2.1.3); mobileClient=android; steamid={0}; steamLogin={1}; steamLoginSecure={2}; Steam_Language=english; dob=;", steamAccount.Session.SteamID.ToString(), steamAccount.Session.SteamLogin, steamAccount.Session.SteamLoginSecure);

            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }

            browser = new ChromiumWebBrowser(steamAccount.GenerateConfirmationURL())
            {
                Dock = DockStyle.Fill,
            };
            this.splitContainer1.Panel2.Controls.Add(browser);

            BrowserRequestHandler handler = new BrowserRequestHandler();
            handler.Cookies = steamCookies;
            browser.RequestHandler = handler;
            browser.AddressChanged += Browser_AddressChanged;
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // This looks really ugly, but it's easier than implementing steam's steammobile:// protocol using CefSharp
            // We override the page's GetValueFromLocalURL() to pass in the keys for sending ajax requests
            Debug.WriteLine("IsLoading: " + e.IsLoading);
            if (e.IsLoading == false)
            {
                // Generate url for details
                string urlParams = steamAccount.GenerateConfirmationQueryParams("details" + tradeID);

                var script = string.Format(@"window.GetValueFromLocalURL = 
                function(url, timeout, success, error, fatal) {{            
                    console.log(url);
                    if(url.indexOf('steammobile://steamguard?op=conftag&arg1=allow') !== -1) {{
                        // send confirmation (allow)
                        success('{0}');
                    }} else if(url.indexOf('steammobile://steamguard?op=conftag&arg1=cancel') !== -1) {{
                        // send confirmation (cancel)
                        success('{1}');
                    }} else if(url.indexOf('steammobile://steamguard?op=conftag&arg1=details') !== -1) {{
                        // get details
                        success('{2}');
                    }}
                }}", steamAccount.GenerateConfirmationQueryParams("allow"), steamAccount.GenerateConfirmationQueryParams("cancel"), urlParams);
                try
                {
                    browser.ExecuteScriptAsync(script);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Failed to execute script");
                }
            }
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            string[] urlparts = browser.Address.Split('#');
            if (urlparts.Length > 1)
            {
                tradeID = urlparts[1].Replace("conf_", "");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            browser.Load(steamAccount.GenerateConfirmationURL());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            switch (keyData)
            {
                case Keys.F5:
                    browser.Load(steamAccount.GenerateConfirmationURL());
                    bHandled = true;
                    break;
                case Keys.F1:
                    browser.ShowDevTools();
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
    }
}
