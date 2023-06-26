using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public class Manifest
    {
        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("first_run")]
        public bool FirstRun { get; set; } = true;

        [JsonProperty("entries")]
        public List<ManifestEntry> Entries { get; set; }

        [JsonProperty("periodic_checking")]
        public bool PeriodicChecking { get; set; } = false;

        [JsonProperty("periodic_checking_interval")]
        public int PeriodicCheckingInterval { get; set; } = 5;

        [JsonProperty("periodic_checking_checkall")]
        public bool CheckAllAccounts { get; set; } = false;

        [JsonProperty("auto_confirm_market_transactions")]
        public bool AutoConfirmMarketTransactions { get; set; } = false;

        [JsonProperty("auto_confirm_trades")]
        public bool AutoConfirmTrades { get; set; } = false;

        private static Manifest _manifest { get; set; }

        public static string GetExecutableDir()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public static Manifest GetManifest(bool forceLoad = false)
        {
            // Check if already staticly loaded
            if (_manifest != null && !forceLoad)
            {
                return _manifest;
            }

            // Find config dir and manifest file
            string maDir = Manifest.GetExecutableDir() + "/maFiles/";
            string maFile = maDir + "manifest.json";

            // If there's no config dir, create it
            if (!Directory.Exists(maDir))
            {
                _manifest = _generateNewManifest();
                return _manifest;
            }

            // If there's no manifest, create it
            if (!File.Exists(maFile))
            {
                _manifest = _generateNewManifest(true);
                return _manifest;
            }

            try
            {
                string manifestContents = File.ReadAllText(maFile);
                _manifest = JsonConvert.DeserializeObject<Manifest>(manifestContents);

                if (_manifest.Encrypted && _manifest.Entries.Count == 0)
                {
                    _manifest.Encrypted = false;
                    _manifest.Save();
                }

                _manifest.RecomputeExistingEntries();

                return _manifest;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Manifest _generateNewManifest(bool scanDir = false)
        {
            // No directory means no manifest file anyways.
            Manifest newManifest = new Manifest();
            newManifest.Encrypted = false;
            newManifest.PeriodicCheckingInterval = 5;
            newManifest.PeriodicChecking = false;
            newManifest.AutoConfirmMarketTransactions = false;
            newManifest.AutoConfirmTrades = false;
            newManifest.Entries = new List<ManifestEntry>();
            newManifest.FirstRun = true;

            // Take a pre-manifest version and generate a manifest for it.
            if (scanDir)
            {
                string maDir = Manifest.GetExecutableDir() + "/maFiles/";
                if (Directory.Exists(maDir))
                {
                    DirectoryInfo dir = new DirectoryInfo(maDir);
                    var files = dir.GetFiles();

                    foreach (var file in files)
                    {
                        if (file.Extension != ".maFile") continue;

                        string contents = File.ReadAllText(file.FullName);
                        try
                        {
                            SteamGuardAccount account = JsonConvert.DeserializeObject<SteamGuardAccount>(contents);
                            ManifestEntry newEntry = new ManifestEntry()
                            {
                                Filename = file.Name,
                                SteamID = account.Session.SteamID
                            };
                            newManifest.Entries.Add(newEntry);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (newManifest.Entries.Count > 0)
                    {
                        newManifest.Save();
                        newManifest.PromptSetupPassKey("This version of SDA has encryption. Please enter a passkey below, or hit cancel to remain unencrypted");
                    }
                }
            }

            if (newManifest.Save())
            {
                return newManifest;
            }

            return null;
        }

        public class IncorrectPassKeyException : Exception { }
        public class ManifestNotEncryptedException : Exception { }

        public string PromptForPassKey()
        {
            if (!this.Encrypted)
            {
                throw new ManifestNotEncryptedException();
            }

            bool passKeyValid = false;
            string passKey = null;
            while (!passKeyValid)
            {
                InputForm passKeyForm = new InputForm("Please enter your encryption passkey.", true);
                passKeyForm.ShowDialog();
                if (!passKeyForm.Canceled)
                {
                    passKey = passKeyForm.txtBox.Text;
                    passKeyValid = this.VerifyPasskey(passKey);
                    if (!passKeyValid)
                    {
                        MessageBox.Show("That passkey is invalid.");
                    }
                }
                else
                {
                    return null;
                }
            }
            return passKey;
        }

        public string PromptSetupPassKey(string initialPrompt = "Enter passkey, or hit cancel to remain unencrypted.")
        {
            InputForm newPassKeyForm = new InputForm(initialPrompt);
            newPassKeyForm.ShowDialog();
            if (newPassKeyForm.Canceled || newPassKeyForm.txtBox.Text.Length == 0)
            {
                MessageBox.Show("WARNING: You chose to not encrypt your files. Doing so imposes a security risk for yourself. If an attacker were to gain access to your computer, they could completely lock you out of your account and steal all your items.");
                return null;
            }

            InputForm newPassKeyForm2 = new InputForm("Confirm new passkey.");
            newPassKeyForm2.ShowDialog();
            if (newPassKeyForm2.Canceled)
            {
                MessageBox.Show("WARNING: You chose to not encrypt your files. Doing so imposes a security risk for yourself. If an attacker were to gain access to your computer, they could completely lock you out of your account and steal all your items.");
                return null;
            }

            string newPassKey = newPassKeyForm.txtBox.Text;
            string confirmPassKey = newPassKeyForm2.txtBox.Text;

            if (newPassKey != confirmPassKey)
            {
                MessageBox.Show("Passkeys do not match.");
                return null;
            }

            if (!this.ChangeEncryptionKey(null, newPassKey))
            {
                MessageBox.Show("Unable to set passkey.");
                return null;
            }
            else
            {
                MessageBox.Show("Passkey successfully set.");
            }

            return newPassKey;
        }

        public SteamAuth.SteamGuardAccount[] GetAllAccounts(string passKey = null, int limit = -1)
        {
            if (passKey == null && this.Encrypted) return new SteamGuardAccount[0];
            string maDir = Manifest.GetExecutableDir() + "/maFiles/";

            List<SteamAuth.SteamGuardAccount> accounts = new List<SteamAuth.SteamGuardAccount>();
            foreach (var entry in this.Entries)
            {
                string fileText = File.ReadAllText(maDir + entry.Filename);
                if (this.Encrypted)
                {
                    string decryptedText = FileEncryptor.DecryptData(passKey, entry.Salt, entry.IV, fileText);
                    if (decryptedText == null) return new SteamGuardAccount[0];
                    fileText = decryptedText;
                }

                var account = JsonConvert.DeserializeObject<SteamAuth.SteamGuardAccount>(fileText);
                if (account == null) continue;
                accounts.Add(account);

                if (limit != -1 && limit >= accounts.Count)
                    break;
            }

            return accounts.ToArray();
        }

        public bool ChangeEncryptionKey(string oldKey, string newKey)
        {
            if (this.Encrypted)
            {
                if (!this.VerifyPasskey(oldKey))
                {
                    return false;
                }
            }
            bool toEncrypt = newKey != null;

            string maDir = Manifest.GetExecutableDir() + "/maFiles/";
            for (int i = 0; i < this.Entries.Count; i++)
            {
                ManifestEntry entry = this.Entries[i];
                string filename = maDir + entry.Filename;
                if (!File.Exists(filename)) continue;

                string fileContents = File.ReadAllText(filename);
                if (this.Encrypted)
                {
                    fileContents = FileEncryptor.DecryptData(oldKey, entry.Salt, entry.IV, fileContents);
                }

                string newSalt = null;
                string newIV = null;
                string toWriteFileContents = fileContents;

                if (toEncrypt)
                {
                    newSalt = FileEncryptor.GetRandomSalt();
                    newIV = FileEncryptor.GetInitializationVector();
                    toWriteFileContents = FileEncryptor.EncryptData(newKey, newSalt, newIV, fileContents);
                }

                File.WriteAllText(filename, toWriteFileContents);
                entry.IV = newIV;
                entry.Salt = newSalt;
            }

            this.Encrypted = toEncrypt;

            this.Save();
            return true;
        }

        public bool VerifyPasskey(string passkey)
        {
            if (!this.Encrypted || this.Entries.Count == 0) return true;

            var accounts = this.GetAllAccounts(passkey, 1);
            return accounts != null && accounts.Length == 1;
        }

        public bool RemoveAccount(SteamGuardAccount account, bool deleteMaFile = true)
        {
            ManifestEntry entry = (from e in this.Entries where e.SteamID == account.Session.SteamID select e).FirstOrDefault();
            if (entry == null) return true; // If something never existed, did you do what they asked?

            string maDir = Manifest.GetExecutableDir() + "/maFiles/";
            string filename = maDir + entry.Filename;
            this.Entries.Remove(entry);

            if (this.Entries.Count == 0)
            {
                this.Encrypted = false;
            }

            if (this.Save() && deleteMaFile)
            {
                try
                {
                    File.Delete(filename);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public bool SaveAccount(SteamGuardAccount account, bool encrypt, string passKey = null)
        {
            if (encrypt && String.IsNullOrEmpty(passKey)) return false;
            if (!encrypt && this.Encrypted) return false;

            string salt = null;
            string iV = null;
            string jsonAccount = JsonConvert.SerializeObject(account);

            if (encrypt)
            {
                salt = FileEncryptor.GetRandomSalt();
                iV = FileEncryptor.GetInitializationVector();
                string encrypted = FileEncryptor.EncryptData(passKey, salt, iV, jsonAccount);
                if (encrypted == null) return false;
                jsonAccount = encrypted;
            }

            string maDir = Manifest.GetExecutableDir() + "/maFiles/";
            string filename = account.Session.SteamID.ToString() + ".maFile";

            ManifestEntry newEntry = new ManifestEntry()
            {
                SteamID = account.Session.SteamID,
                IV = iV,
                Salt = salt,
                Filename = filename
            };

            bool foundExistingEntry = false;
            for (int i = 0; i < this.Entries.Count; i++)
            {
                if (this.Entries[i].SteamID == account.Session.SteamID)
                {
                    this.Entries[i] = newEntry;
                    foundExistingEntry = true;
                    break;
                }
            }

            if (!foundExistingEntry)
            {
                this.Entries.Add(newEntry);
            }

            bool wasEncrypted = this.Encrypted;
            this.Encrypted = encrypt || this.Encrypted;

            if (!this.Save())
            {
                this.Encrypted = wasEncrypted;
                return false;
            }

            try
            {
                File.WriteAllText(maDir + filename, jsonAccount);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Save()
        {
            string maDir = Manifest.GetExecutableDir() + "/maFiles/";
            string filename = maDir + "manifest.json";
            if (!Directory.Exists(maDir))
            {
                try
                {
                    Directory.CreateDirectory(maDir);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            try
            {
                string contents = JsonConvert.SerializeObject(this);
                File.WriteAllText(filename, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void RecomputeExistingEntries()
        {
            List<ManifestEntry> newEntries = new List<ManifestEntry>();
            string maDir = Manifest.GetExecutableDir() + "/maFiles/";

            foreach (var entry in this.Entries)
            {
                string filename = maDir + entry.Filename;
                if (File.Exists(filename))
                {
                    newEntries.Add(entry);
                }
            }

            this.Entries = newEntries;

            if (this.Entries.Count == 0)
            {
                this.Encrypted = false;
            }
        }

        public void MoveEntry(int from, int to)
        {
            if (from < 0 || to < 0 || from > Entries.Count || to > Entries.Count - 1) return;
            ManifestEntry sel = Entries[from];
            Entries.RemoveAt(from);
            Entries.Insert(to, sel);
            Save();
        }

        public class ManifestEntry
        {
            [JsonProperty("encryption_iv")]
            public string IV { get; set; }

            [JsonProperty("encryption_salt")]
            public string Salt { get; set; }

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("steamid")]
            public ulong SteamID { get; set; }
        }
    }
}
