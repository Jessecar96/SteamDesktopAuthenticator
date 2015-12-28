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



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Manifest manifest = Manifest.GetManifest();
            string RunGUI = manifest.UseGUI;



            if (manifest.FirstRun)
            {
                // Install VC++ Redist and wait
                DialogResult res = MessageBox.Show("Install Visual C++ Redistributable 2013\nvcredist_x86.exe", "SDA Setup", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    new InstallRedistribForm().ShowDialog();
                }

                // Already has accounts, just run
                if (manifest.Entries.Count > 0)
                {
                    if (RunGUI == "2")
                    {
                        Application.Run(new MainFormGuiCompact());
                    }
                    else {
                        // use default GUI
                        Application.Run(new MainForm());
                    }
                }
                else
                {
                    // No accounts, run welcome form
                    Application.Run(new WelcomeForm());
                }
            }
            else
            {
                if (RunGUI == "2")
                {
                    Application.Run(new MainFormGuiCompact());
                }
                else {
                    Application.Run(new MainForm());
                }
            }



        }
    }
}
