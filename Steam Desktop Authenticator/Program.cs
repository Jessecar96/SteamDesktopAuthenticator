﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Steam_Desktop_Authenticator
{
    static class Program
    {
        public static bool winStartup;

        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            try
            {
                Process curr = Process.GetCurrentProcess();
                Process[] procs = Process.GetProcessesByName(curr.ProcessName);
                foreach (Process p in procs)
                {
                    if ((p.Id != curr.Id) &&
                        (p.MainModule.FileName == curr.MainModule.FileName))
                        return p;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // run the program only once
            if (PriorProcess() != null)
            {
                MessageBox.Show("Another instance of the app is already running.");
                return;
            }

            bool startup = args.Length > 0 && args[0] == "-startup";
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Manifest man = Manifest.GetManifest();

            winStartup = startup && man.StartupMinimized;

            if (man.FirstRun)
            {
                // Install VC++ Redist and wait
                new InstallRedistribForm().ShowDialog();

                if (man.Entries.Count > 0)
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
