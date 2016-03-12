using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Steam_Desktop_Authenticator
{
    static class Program
    {

        // Activate Old Process Window - Part 1
        [DllImportAttribute("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImportAttribute("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Manifest manifest_app = Manifest.GetManifest();

            if (manifest_app.AppCanBeStartedMultipleTimes == false)
            {
                // Activate Old Process Window - Part 2
                // If another instance is already running, activate it and exit - Part 2
                try
                {
                    Process currentProc = Process.GetCurrentProcess();
                    foreach (Process proc in Process.GetProcessesByName(currentProc.ProcessName))
                    {
                        if (proc.Id != currentProc.Id)
                        {
                            IntPtr firstInstance = FindWindow(null, "Steam Desktop Authenticator");
                            ShowWindow(firstInstance, 1);
                            SetForegroundWindow(firstInstance);

                            return;   // Exit application
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            if(manifest_app.FirstRun)
            {
                // Install VC++ Redist and wait
                new InstallRedistribForm().ShowDialog();

                if (manifest_app.Entries.Count > 0)
                {
                    // Already has accounts, just run
                    Application.Run(new MainForm());
                }
                else
                {
                    // No accounts, run welcome form
                    Application.Run(new WelcomeForm());
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
