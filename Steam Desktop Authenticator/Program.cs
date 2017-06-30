using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
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

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    static public class WinApi
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        public static int RegisterWindowMessage(string format, params object[] args)
        {
            string message = String.Format(format, args);
            return RegisterWindowMessage(message);
        }

        public const int HWND_BROADCAST = 0xffff;
        public const int SW_SHOWNORMAL = 1;

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
    }

    static public class SingleInstance
    {
        public static readonly int WM_SHOWFIRSTINSTANCE = WinApi.RegisterWindowMessage("WM_SHOWFIRSTINSTANCE|{0}", ProgramInfo.AssemblyGuid);
        static Mutex mutex;
        static public bool Start()
        {
            bool onlyInstance = false;
            string mutexName = String.Format("Local\\{0}", ProgramInfo.AssemblyGuid);

            // if you want your app to be limited to a single instance
            // across ALL SESSIONS (multiple users & terminal services), then use the following line instead:
            // string mutexName = String.Format("Global\\{0}", ProgramInfo.AssemblyGuid);

            mutex = new Mutex(true, mutexName, out onlyInstance);
            return onlyInstance;
        }
        static public void ShowFirstInstance()
        {
            WinApi.PostMessage(
                (IntPtr)WinApi.HWND_BROADCAST,
                WM_SHOWFIRSTINSTANCE,
                IntPtr.Zero,
                IntPtr.Zero);
        }
        static public void Stop()
        {
            mutex.ReleaseMutex();
        }
    }

    static public class ProgramInfo
    {
        static public string AssemblyGuid
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // run the program only once
            if (!SingleInstance.Start())
            {
                SingleInstance.ShowFirstInstance();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



        // Parse command line arguments
        var options = new Options();
            Parser.Default.ParseArguments(args, options);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Manifest man = Manifest.GetManifest();
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
