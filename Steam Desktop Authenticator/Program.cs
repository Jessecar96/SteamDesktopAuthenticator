using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using CommandLine;
using CommandLine.Text;
using System.Net;

namespace Steam_Desktop_Authenticator
{

    class Options
    {
        [Option('k', "encryption-key", Required = false,
          HelpText = "Encryption key for manifest")]
        public string EncryptionKey { get; set; }

        [Option('s', "silent", Required = false,
          HelpText = "Start minimized")]
        public bool Silent { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    static class Program
    {
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // run the program only once
            if (PriorProcess() != null)
            {
                MessageBox.Show("Another instance of the app is already running.");
                return;
            }

            // Parse command line arguments
            var options = new Options();
            Parser.Default.ParseArguments(args, options);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Manifest man = Manifest.GetManifest();
            if (man.FirstRun)
            {
                // Install VC++ Redist and wait
                //new InstallRedistribForm().ShowDialog();

                if (man.Entries.Count > 0)
                {
                    // Already has accounts, just run
                    MainForm mf = new MainForm();
                    mf.SetEncryptionKey(options.EncryptionKey);
                    mf.StartSilent(options.Silent);
                    Application.Run(mf);
                }
                else
                {
                    // No accounts, run welcome form
                    Application.Run(new WelcomeForm());
                }
            }
            else
            {
                MainForm mf = new MainForm();
                mf.SetEncryptionKey(options.EncryptionKey);
                mf.StartSilent(options.Silent);
                Application.Run(mf);
            }
        }
    }
}
