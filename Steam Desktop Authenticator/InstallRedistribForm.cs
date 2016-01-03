using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace Steam_Desktop_Authenticator
{
    public partial class InstallRedistribForm : Form
    {
        private bool inlineInstall = false;

        public InstallRedistribForm(bool inlineInstall = false)
        {
            InitializeComponent();
            progressBar1.Minimum = 0;

            string path = Manifest.GetExecutableDir() + "/vcredist_x86.exe";

            // Not downloading the file again if it already exists
            if (File.Exists(path))
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        var md5String = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                        // Hardcoding the md5 since file doesn't change
                        if (md5String == "0fc525b6b7b96a87523daa7a0013c69d")
                        {
                            progressBar1.Value = progressBar1.Maximum;
                            Install();
                        }
                    }
                }
            }
            else
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri("https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x86.exe"), path);
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Install();
        }

        private void Install()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Manifest.GetExecutableDir() + "/vcredist_x86.exe";
                startInfo.Verb = "runas";
                startInfo.Arguments = "/q";
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

                Process installProcess = new Process();
                installProcess.StartInfo = startInfo;
                installProcess.EnableRaisingEvents = true;
                installProcess.Exited += InstallProcess_Exited;
                installProcess.Start();
            }
            catch (Exception)
            {
                this.Close();
            }
        }
        private void InstallProcess_Exited(object sender, EventArgs e)
        {
            if (inlineInstall)
            {
                MessageBox.Show("Install complete! You may need to restart Steam Desktop Authenticator to view trade confirmations.", "Visual C++ Redistributable 2013", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive;
            progressBar1.Value = (int)e.BytesReceived;
        }
    }
}
