﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using CommandLine;
using CommandLine.Text;

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

        [Option('d', "directory", Required = false,
            HelpText = "Custom maFiles directory", DefaultValue = null)]
        public string MaFilesDirectory { get; set; }

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

            Manifest.MaDir = options.MaFilesDirectory;
            Manifest man;

            try
            {
                man = Manifest.GetManifest();
            }
            catch (ManifestParseException)
            {
                // Manifest file was corrupted, generate a new one.
                try
                {
                    MessageBox.Show("Your settings were unexpectedly corrupted and were reset to defaults.", "Steam Desktop Authenticator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    man = Manifest.GenerateNewManifest(true);
                }
                catch (MaFileEncryptedException)
                {
                    // An maFile was encrypted, we're fucked.
                    MessageBox.Show("Sorry, but SDA was unable to recover your accounts since you used encryption.\nYou'll need to recover your Steam accounts by removing the authenticator.\nClick OK to view instructions.", "Steam Desktop Authenticator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Process.Start(@"https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Help!-I'm-locked-out-of-my-account");
                    return;
                }
            }

            if (man.FirstRun)
            {
                // Install VC++ Redist and wait
                new InstallRedistribForm().ShowDialog();

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
