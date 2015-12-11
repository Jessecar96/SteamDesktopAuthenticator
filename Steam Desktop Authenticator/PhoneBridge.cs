using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamAuth
{
    public class PhoneBridge
    {
        public bool OutputToConsole = true;

        private Process console;
        private ManualResetEvent mreOutput = new ManualResetEvent(false);

        public delegate void BridgeError(string msg);
        public event BridgeError PhoneBridgeError;
        private void OnPhoneBridgeError(string msg)
        {
            if (PhoneBridgeError != null)
                PhoneBridgeError(msg);
        }

        private string Error = "";

        private void InitConsole()
        {
            if (console != null) return;

            console = new Process();

            console.StartInfo.UseShellExecute = false;
            console.StartInfo.RedirectStandardOutput = true;
            console.StartInfo.RedirectStandardInput = true;
            console.StartInfo.CreateNoWindow = true;
            console.StartInfo.FileName = "CMD.exe";
            console.StartInfo.Arguments = "/K";
            console.Start();
            console.BeginOutputReadLine();

            console.OutputDataReceived += (sender, e) =>
            {
                if (e.Data.Contains(">@") || !OutputToConsole || e.Data == "") return;
                if (OutputToConsole)
                    Console.WriteLine(e.Data);
            };
        }

        public SteamGuardAccount ExtractSteamGuardAccount()
        {
            InitConsole(); // Init the console

            // Check required states
            Error = ErrorsFound();
            if (Error != "")
            {
                OnPhoneBridgeError(Error);
                return null;
            }

            SteamGuardAccount acc;

            if (IsRooted()){
                acc = JsonConvert.DeserializeObject<SteamGuardAccount>(PullJson());
            } else {
                acc = JsonConvert.DeserializeObject<SteamGuardAccount>(PullJsonNoRoot());
            }

            return acc;
        }

        private string ErrorsFound()
        {
            if (!CheckAdb()) return "ADB not found";
            if (!DeviceUp()) return "Device not detected";
            if (!SteamAppInstalled()) return "Steam Community app not installed";
            return "";
        }

        private string PullJson()
        {
            string steamid = null;
            string json = null;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data.Contains("No such file or directory"))
                {
                    mre.Set();
                    return;
                }
                if (e.Data.Contains("Steamguard-"))
                {
                    steamid = e.Data.Split('-')[1];
                    mre.Set();
                }
                if (e.Data.StartsWith("{"))
                {
                    json = e.Data;
                    mre.Set();
                }
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb shell \"su -c ls /data/data/com.valvesoftware.android.steam.community/files\"");
            mre.Wait();

            mre.Reset();
            ExecuteCommand("adb shell su -c \"cat /data/data/com.valvesoftware.android.steam.community/files/Steamguard-" + steamid + "\"");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return json;
        }
        private string PullJsonNoRoot()
        {
            string json = "Error";
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data == "Done")
                    mre.Set();
                if (e.Data.StartsWith("{"))
                    json = e.Data;
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb backup --noapk com.valvesoftware.android.steam.community & echo Done");
            mre.Wait();

            mre.Reset();
            ExecuteCommand("adb push backup.ab /sdcard/steamauth/backup.ab & echo Done");
            mre.Wait();

            mre.Reset();
            ExecuteCommand("adb shell \" cd /sdcard/steamauth ; ( printf " + @" '\x1f\x8b\x08\x00\x00\x00\x00\x00'" + " ; tail -c +25 backup.ab ) |  tar xfvz - \" & echo Done");
            mre.Wait();

            mre.Reset();
            ExecuteCommand("adb shell \"cat /sdcard/steamauth/apps/*/f/Steamguard-*\" & echo: & echo Done");
            mre.Wait();

            mre.Reset();
            ExecuteCommand("adb shell \"rm -dR /sdcard/steamauth\" & echo Done");
            mre.Wait();

            System.IO.File.Delete("backup.ab");

            console.OutputDataReceived -= f1;

            return json;
        }
        private bool CheckAdb()
        {
            bool exists = true;
            Process p = new Process();

            p.StartInfo.FileName = "adb.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;

            try
            {
                p.Start();
            }
            catch (Exception)
            {
                exists = false;
            }

            return exists;
        }
        private bool DeviceUp()
        {
            bool up = false;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@")) return;
                if (e.Data.Contains("device"))
                    up = true;
                mre.Set();
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb get-state");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return up;
        }
        private bool SteamAppInstalled()
        {
            bool ins = false;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data == "Yes")
                    ins = true;
                mre.Set();
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb shell \"cd /data/data/com.valvesoftware.android.steam.community && echo Yes\"");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return ins;
        }
        private bool IsRooted()
        {
            bool root = false;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data == "Yes")
                    root = true;
                mre.Set();
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb shell su -c echo Yes");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return root;
        }

        private void ExecuteCommand(string cmd)
        {
            console.StandardInput.WriteLine("@" + cmd);
            console.StandardInput.Flush();
        }
    }
}
