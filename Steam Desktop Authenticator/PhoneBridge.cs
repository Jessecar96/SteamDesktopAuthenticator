using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Steam_Desktop_Authenticator
{
    public class PhoneBridge
    {
        public bool OutputToConsole = true;
        public bool OutputToLog = true;

        private Process console;
        private ManualResetEvent mreOutput = new ManualResetEvent(false);

        public delegate void BridgeOutput(string msg);
        public event BridgeOutput PhoneBridgeError;
        public event BridgeOutput OutputLog;
        private void OnPhoneBridgeError(string msg)
        {
            if (PhoneBridgeError != null)
                PhoneBridgeError(msg);
        }
        private void OnOutputLog(string msg)
        {
            if (OutputLog != null)
                OutputLog(msg);
            if (OutputToLog)
                AppendToLog(msg);
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
                if (OutputToLog)
                    AppendToLog(e.Data);
            };
        }

        private void AppendToLog(string line)
        {
            StreamWriter s = File.AppendText("adblog.txt");
            s.WriteLine(line);
            s.Flush();
            s.Close();
        }

        public SteamGuardAccount ExtractSteamGuardAccount(string id = "*", bool skipChecks = false)
        {
            if (!skipChecks) AppendToLog("");
            InitConsole(); // Init the console

            if (!skipChecks)
            {
                OnOutputLog("Checking requirements...");
                // Check required states
                Error = ErrorsFound();
                if (Error != "")
                {
                    OnPhoneBridgeError(Error);
                    return null;
                }
            }

            bool root = IsRooted();

            //root = false; // DEBUG ///////////////////////

            SteamGuardAccount acc;
            string json;

            if (root)
            {
                OnOutputLog("Using root method");
                json = PullJson(id);
            }
            else
            {
                OnOutputLog("Steam has blocked the non-root method of copying data from their app.");
                OnOutputLog("Your phone must now be rooted to use this.");
                json = null;
                //json = PullJsonNoRoot(id);
            }

            if (json == null)
                return null;
            acc = JsonConvert.DeserializeObject<SteamGuardAccount>(json, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            acc.DeviceID = GetDeviceID(root);

            if (acc.DeviceID == null)
            {
                try
                {
                    OnPhoneBridgeError("failed to read the UUID (device id)");
                }
                catch (Exception) { }
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

        private string GetDeviceID(bool root)
        {
            OnOutputLog("Extracting Device ID");
            string id = null;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;

                Regex rx = new Regex(@"<string[^>]*\s+name=" + "\"uuidKey\"" + @"[^>]*>[\s\t\r\n]*(android:.+)[\s\t\r\n]*<\/string>", RegexOptions.IgnoreCase);
                Match m = rx.Match(e.Data);
                if (m.Success) { id = m.Groups[1].Value; }

                //this need more test,but also is not necessary
                //if (e.Data.TrimEnd(' ', '\t', '\n', '\r').EndsWith("Done"))
                mre.Set();
            };

            console.OutputDataReceived += f1;

            if (root)
                ExecuteCommand($"adb shell su -c 'sed -n 3p /data/data/$STEAMAPP/shared_prefs/steam.uuid.xml' & echo Done");
            else
                ExecuteCommand("adb shell \"cat /sdcard/steamauth/apps/$STEAMAPP/sp/steam.uuid.xml\" & echo Done");
            mre.Wait();

            console.OutputDataReceived -= f1;

            CleanBackup();

            return id;
        }

        private string PullJson(string id = "*")
        {
            string steamid = id;
            string json = null;
            int count = 0;
            List<string> accs = new List<string>();
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
                    accs.Add(steamid);
                    count++;
                }

                if (e.Data == "Done")
                    mre.Set();

                if (e.Data.StartsWith("{"))
                {
                    json = e.Data;
                    mre.Set();
                }
            };

            console.OutputDataReceived += f1;

            if (steamid == "*")
            {
                OnOutputLog("Extracting (1/2)");
                ExecuteCommand("adb shell \"su -c 'ls /data/data/com.valvesoftware.android.steam.community/files'\" & echo Done");
                mre.Wait();
                if (count > 1)
                {
                    OnMoreThanOneAccount(accs);
                    return null;
                }
            }

            mre.Reset();
            OnOutputLog("Extracting " + steamid + " (2/2)");
            ExecuteCommand("adb shell \"su -c 'cat /data/data/com.valvesoftware.android.steam.community/files/Steamguard-" + steamid + "'\"");
            mre.Wait();

            console.OutputDataReceived -= f1;

            if (json == null)
            {
                OnOutputLog("An error occured while extracting files");
            }

            return json;
        }

        private string PullJsonNoRoot(string id = "*")
        {
            string json = "Error";
            string sid = id;
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

            if (!Directory.Exists("steamguard"))
            {
                DoBackup();
            }

            mre.Reset();
            OnOutputLog("Extracting (4/5)");
            ExecuteCommand("adb pull /sdcard/steamauth/apps/$STEAMAPP/f steamguard/ & echo Done");
            mre.Wait();

            mre.Reset();
            OnOutputLog("Extracting (5/5)");
            ExecuteCommand("adb shell \"rm -dR /sdcard/steamauth\" & echo Done");
            mre.Wait();

            string[] files = Directory.EnumerateFiles("steamguard").ToArray<string>();
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Split('-')[1];
            }

            if (files.Length > 1 && sid == "*")
            {
                OnMoreThanOneAccount(new List<string>(files));
                return null;
            }
            else
            {
                json = File.ReadAllText("steamguard/Steamguard-" + sid);
            }

            console.OutputDataReceived -= f1;

            return json;
        }

        private void CleanBackup()
        {
            File.Delete("backup.ab");
            if (Directory.Exists("steamguard"))
                Directory.Delete("steamguard", true);
        }

        private void DoBackup()
        {
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data == "Done")
                    mre.Set();
            };

            console.OutputDataReceived += f1;

            OnOutputLog("Extracting (1/5)");
            ExecuteCommand("adb backup --noapk com.valvesoftware.android.steam.community & echo Done");
            OnOutputLog("Now unlock your phone and confirm operation");
            mre.Wait();

            mre.Reset();
            OnOutputLog("Extracting (2/5)");
            ExecuteCommand("adb push backup.ab /sdcard/steamauth/backup.ab & echo Done");
            mre.Wait();

            mre.Reset();
            OnOutputLog("Extracting (3/5)");
            ExecuteCommand("adb shell \" cd /sdcard/steamauth ; ( printf " + @" '\x1f\x8b\x08\x00\x00\x00\x00\x00'" + " ; tail -c +25 backup.ab ) |  tar xfvz - \" & echo Done");
            mre.Wait();

            console.OutputDataReceived -= f1;
        }

        private bool CheckAdb()
        {
            bool exists = true;
            Process p = new Process();

            OnOutputLog("Checking for ADB");

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
            OnOutputLog("Checking for device");
            bool up = false;

            up = GetState() == "device";

            return up;
        }

        private bool SteamAppInstalled()
        {
            OnOutputLog("Checking for Steam app");
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

            ExecuteCommand("adb shell \"su -c 'cd /data/data/com.valvesoftware.android.steam.community && echo Yes'\"");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return ins;
        }

        private bool IsRooted()
        {
            OnOutputLog("Checking root");
            bool root = false;
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                if (e.Data.Contains("Yes"))
                    root = true;
                mre.Set();
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb shell su -c 'echo Yes'");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return root;
        }


        public delegate void AccountsDelegate(List<string> accounts);
        public event AccountsDelegate MoreThanOneAccount;
        private void OnMoreThanOneAccount(List<string> accounts)
        {
            if (MoreThanOneAccount != null)
                MoreThanOneAccount(accounts);
        }


        public event EventHandler DeviceWaited;
        protected void OnDeviceWaited()
        {
            if (DeviceWaited != null)
            {
                DeviceWaited(this, EventArgs.Empty);
            }
        }

        public void WaitForDeviceAsync()
        {
            InitConsole();
            console.OutputDataReceived += WaitForDeviceCallback;

            ExecuteCommand("adb wait-for-device & echo Done");
        }

        private void WaitForDeviceCallback(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.Contains(">@") || e.Data == "") return;
            if (e.Data == "Done")
                OnDeviceWaited();
        }

        public string GetState()
        {
            InitConsole();
            if (!CheckAdb()) return "noadb";

            string state = "Error";
            ManualResetEventSlim mre = new ManualResetEventSlim();
            DataReceivedEventHandler f1 = (sender, e) =>
            {
                if (e.Data.Contains(">@") || e.Data == "") return;
                state = e.Data;
                mre.Set();
            };

            console.OutputDataReceived += f1;

            ExecuteCommand("adb get-state");
            mre.Wait();

            console.OutputDataReceived -= f1;

            return state;
        }

        public void ConnectWiFi(string ip)
        {
            InitConsole();
            ExecuteCommand("adb connect " + ip);
        }

        private void ExecuteCommand(string cmd)
        {
            console.StandardInput.WriteLine("@" + cmd.Replace("$STEAMAPP", "com.valvesoftware.android.steam.community"));
            console.StandardInput.Flush();
        }

        public void Close()
        {
            console.Close();
            console.WaitForExit();
        }
    }
}
