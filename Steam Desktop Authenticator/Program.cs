using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Squirrel;

namespace Steam_Desktop_Authenticator
{
    static class Program
    {
        static bool ShowTheWelcomeWizard;

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

+        // Activate Old Process Window
+        [DllImportAttribute ("user32.dll")]
+        public static extern IntPtr FindWindow (string lpClassName, string lpWindowName);
+        	
+        [DllImportAttribute ("user32.dll")]
+        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
+        	
+        [DllImportAttribute ("user32.dll")]
+        public static extern bool SetForegroundWindow(IntPtr hWnd);
+        	
+        public static void ShowToFront(string windowName)
+        {
+        	IntPtr firstInstance = FindWindow(null, windowName);
+        	ShowWindow(firstInstance, 1);
+        	SetForegroundWindow(firstInstance);
+        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                using (var mgr = new UpdateManager("https://s3.amazonaws.com/steamdesktopauthenticator/releases"))
                {
                    // Note, in most of these scenarios, the app exits after this method
                    // completes!
                    SquirrelAwareApp.HandleEvents(
                      onInitialInstall: v => mgr.CreateShortcutForThisExe(),
                      onAppUpdate: v => mgr.CreateShortcutForThisExe(),
                      onAppUninstall: v => mgr.RemoveShortcutForThisExe(),
                      onFirstRun: () => ShowTheWelcomeWizard = true);
                }
            }
            catch
            {
                // Not using a squirrel app
            }

            // run the program only once
            if (PriorProcess() != null)
            {
                //Another instance of the app is already running.
                ShowToFront("Steam Desktop Authenticator");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Manifest man = Manifest.GetManifest();
            if(man.FirstRun)
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
