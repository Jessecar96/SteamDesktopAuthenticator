using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Steam_Desktop_Authenticator
{
    public class Manifest
    {
        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("first_run")]
        public bool FirstRun { get; set; } = true;

        [JsonProperty("minimise_to_system_tray")]
        public string MinimiseToSystemTray { get; set; } = "close";

        [JsonProperty("show_confirmation_list_btn")]
        public bool ShowConfirmationListButton { get; set; } = false;

        [JsonProperty("auto_check_for_updated")]
        public bool AutoCheckForUpdates { get; set; } = true;

        [JsonProperty("entries")]
        public List<ManifestEntry> Entries { get; set; }

        [JsonProperty("periodic_checking")]
        public bool PeriodicChecking { get; set; } = false;

        [JsonProperty("periodic_checking_interval")]
        public int PeriodicCheckingInterval { get; set; } = 5;

        [JsonProperty("periodic_checking_checkall")]
        public bool CheckAllAccounts { get; set; } = false;

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
            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
            string saFile = maDir + "manifest.json";

            // If there's no config dir, create it
            if (!Directory.Exists(maDir))
            {
                _manifest = _generateNewManifest();
                return _manifest;
            }

            // If there's no manifest, create it
            if (!File.Exists(saFile))
            {
                _manifest = _generateNewManifest(true);
                return _manifest;
            }

            try
            {
                string manifestContents = File.ReadAllText(saFile);
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


        // Verify Accounts
        /// vvvvvvvvvvvvvvvvvvvvvvvvvvvv
        public string VeryfyIfAccountsAreOk()
        {
            string Return = "";

            int TotalAcc = 0;
            int TotalEncryptedAcc = 0;
            int TotalNotEncryptedAcc = 0;

            string EncryptedAcc_List = "";
            string NotEncryptedAcc_List = "";

            string AccountsProblemDetected = "";

            // Accounts Inventory
            //-------------------------
            #region Accounts Inventory
            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
                if (Directory.Exists(maDir))
                {
                    DirectoryInfo dir = new DirectoryInfo(maDir);
                    var files = dir.GetFiles();

                    foreach (var file in files)
                    {
                        if (file.Extension == ".saFile")
                        {
                            TotalAcc++;
                            string content = File.ReadAllText(file.FullName);
                            string FileName_ = Path.GetFileName(file.FullName);

                            // search in string
                            if (content.Contains("<next_string_of_data>"))
                            {
                                TotalEncryptedAcc++;
                                if (EncryptedAcc_List == "") { EncryptedAcc_List = FileName_; } else { EncryptedAcc_List += " " + FileName_; }
                            }
                            else {
                                TotalNotEncryptedAcc++;
                                if (NotEncryptedAcc_List == "") { NotEncryptedAcc_List = FileName_; } else { NotEncryptedAcc_List += " " + FileName_; }
                            }
                        }
                    }
                }

                // Problem ?
                if (Encrypted)
                {
                    if (TotalNotEncryptedAcc == 0) { } else { AccountsProblemDetected = "1"; }
                }
                else {
                    if (TotalEncryptedAcc == 0) { } else { AccountsProblemDetected = "1"; }
                }

            #endregion // Accounts Inventory END



            // Fix Problem
            //-------------------------
            #region Fix Problem
                if (AccountsProblemDetected == "1")
                {
                    // Msg Title
                    string VerifyAccTitle = "Problem detected in your accounts:";

                    // Starting to write Msg
                    string StatusMsg = "Problem detected in your accounts!\n\nStatus:\n";
                    if (Encrypted) { StatusMsg += "Manifest encrypted=true\n\n"; }
                    else { StatusMsg += "Manifest encrypted=false\n\n"; }


                    if (TotalEncryptedAcc == 0 && Encrypted == true)
                    {
                        StatusMsg += "Repair: I will set manifest encrypted=false";
                        // set manifest
                        Encrypted = false;
                        Save();
                    }
                    else if (TotalNotEncryptedAcc == 0 && Encrypted == false)
                    {
                        StatusMsg += "Repair: I will set manifest encrypted=true";
                        // set manifest
                        Encrypted = true;
                        Save();
                    }
                    else
                    {
                        // Fix Bigger Problem
                        //-------------------------
                        #region Fix Bigger Problem
                            // This will move the encrypted accounts to anoter folder in the app "accounts to import again"

                            // move to
                            string ToImportAgainFolder_path = Manifest.GetExecutableDir() + @"\accounts to import again\";

                            StatusMsg += "Repair:\n- I will set manifest encrypted=false\n";
                            StatusMsg += "- I will move your encrypted files to folder:\n";
                            StatusMsg += ToImportAgainFolder_path + "\n\n";
                            StatusMsg += "You need to import these moved accounts again!\n";

                            // set manifest
                            Encrypted = false;
                            Save();

                                // Move Encrypted files
                                string[] Files = EncryptedAcc_List.Split(' ');// split by character


                                foreach (string File_Name_ in Files)
                                {
                                    string ExtractFileNameSteamID64 = File_Name_.Replace(".saFile", "");

                                    if(File_Name_ == "" || File_Name_ == null || ExtractFileNameSteamID64 == "" || ExtractFileNameSteamID64 == null) { }else{
                        
                                        // move
                                        string ReturnFromMovingFile = MoveAccountToRemovedFromManifest(null, ExtractFileNameSteamID64, "accToImpAga");

                                        if (ReturnFromMovingFile == null)
                                        {
                                            StatusMsg += "Failed to move: " + File_Name_ + "\n";
                                            Return = "error";
                                        }else {
                                             StatusMsg += File_Name_ + "\n";
                                        }
                                        if (Return == "error") { StatusMsg += "\n\nQuit"; }

                                        // unlink
                                         UnlinkAccFromManifestStringSteamID64(ExtractFileNameSteamID64);

                                    }
                                }
                                if (Return == "") { Return = ToImportAgainFolder_path; }
                               

                        #endregion // Fix Bigger Problem END
                    }

                    MessageBox.Show(StatusMsg, VerifyAccTitle);
                }
                else
                {
                    // there was no problem
                        Return = "ok";
                }
            #endregion // Fix Problem END

            return Return;
        }
        /// vvvvvvvvvvvvvvvvvvvvvvvvvvvv Verify Accounts END





        private static Manifest _generateNewManifest(bool scanDir = false)
        {
            // No directory means no manifest file anyways.
            Manifest newManifest = new Manifest();
            newManifest.Encrypted = false;
            newManifest.PeriodicCheckingInterval = 5;
            newManifest.PeriodicChecking = false;
            newManifest.Entries = new List<ManifestEntry>();
            newManifest.FirstRun = true;

            // Take a pre-manifest version and generate a manifest for it.
            if (scanDir)
            {
                string maDir = Manifest.GetExecutableDir() + "/saFiles/";
                if (Directory.Exists(maDir))
                {
                    DirectoryInfo dir = new DirectoryInfo(maDir);
                    var files = dir.GetFiles();

                    foreach (var file in files)
                    {
                        if (file.Extension != ".saFile") continue;

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
            string maDir = Manifest.GetExecutableDir() + "/saFiles/";

            List<SteamAuth.SteamGuardAccount> accounts = new List<SteamAuth.SteamGuardAccount>();
            foreach (var entry in this.Entries)
            {
                string fileText = File.ReadAllText(maDir + entry.Filename);
                if (this.Encrypted)
                {
                    string[] fileTextPart = Regex.Split(fileText, "<next_string_of_data>");
                    string Salt = fileTextPart[0];
                    string IV = fileTextPart[1];
                    fileText = fileTextPart[2];

                    string decryptedText = FileEncryptor.DecryptData(passKey, Salt, IV, fileText);
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

            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
            for (int i = 0; i < this.Entries.Count; i++)
            {
                ManifestEntry entry = this.Entries[i];
                string filename = maDir + entry.Filename;
                if (!File.Exists(filename)) continue;

                string fileContents = File.ReadAllText(filename);
                if (this.Encrypted)
                {
                    string[] fileTextPart = Regex.Split(fileContents, "<next_string_of_data>");
                    string Salt = fileTextPart[0];
                    string IV = fileTextPart[1];
                    fileContents = fileTextPart[2];

                    fileContents = FileEncryptor.DecryptData(oldKey, Salt, IV, fileContents);
                }

                string newSalt = null;
                string newIV = null;
                string toWriteFileContents = fileContents;

                if (toEncrypt)
                {
                    newSalt = FileEncryptor.GetRandomSalt();
                    newIV = FileEncryptor.GetInitializationVector();
                    toWriteFileContents = newSalt + "<next_string_of_data>" + newIV + "<next_string_of_data>" +  FileEncryptor.EncryptData(newKey, newSalt, newIV, fileContents);
                }

                File.WriteAllText(filename, toWriteFileContents);
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


        public string MoveAccountToRemovedFromManifest(SteamGuardAccount account = null, string SteamID64 = null, string Folder = null) {
            string Return = null;

            string EntryFileName = null;
            string FolderName = "accounts removed from manifest";

            // use SteamGuardAccount account
            if (SteamID64 == null)
            {
                ManifestEntry entry = (from e in this.Entries where e.SteamID == account.Session.SteamID select e).FirstOrDefault();

                // If something never existed, did you do what they asked?
                if (entry == null) { }
                else {
                    EntryFileName = entry.Filename;
                }
            }
            // use SteamID64
            else {
                EntryFileName = SteamID64 + ".saFile";
            }

            if (Folder == "accToImpAga") { FolderName = "accounts to import again"; }




            if (EntryFileName == null) {
                MessageBox.Show("Can't move file to: '" + FolderName + "'\nError, account filename is empty\n\nOperation canceled!", "Remove from Manifest Error");
            } else {
                string maDir = Manifest.GetExecutableDir() + "/saFiles/";
                string filenameWithPath = maDir + EntryFileName;

                string MoveToFolder = Manifest.GetExecutableDir() + @"\" + FolderName + @"\";
                string MoveToFilenameWithPath = MoveToFolder + EntryFileName;


                // set check if dir exists:
                string DirExists_Status = "";

                if (Directory.Exists(MoveToFolder))
                {
                    DirExists_Status = "1";
                }
                else {
                    try
                    {
                        Directory.CreateDirectory(MoveToFolder);
                        // verify
                        if (Directory.Exists(MoveToFolder))
                        {
                            DirExists_Status = "1";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can't create folder: '" + FolderName + "'\n\nOperation canceled!", "Remove from Manifest Error");
                    }
                }

                // move the file
                string Move_Status = "1";
                if (DirExists_Status == "1")
                {
                    // if file already exists rename it
                    if (File.Exists(@MoveToFilenameWithPath)) {
                        // create new name
                        string ExtractFileName = Path.GetFileNameWithoutExtension(@MoveToFilenameWithPath);
                        string ExtractFolderPath = Path.GetDirectoryName(@MoveToFilenameWithPath);
                        string timeStamp1 = DateTime.Now.ToString();
                        string timeStamp2 = timeStamp1.Replace("/", "-");
                        string timeStamp3 = timeStamp2.Replace(":", ".");
                        string NewFileName = ExtractFolderPath + @"\" + ExtractFileName + " old " + timeStamp3 + ".saFile";

                        try
                        {
                            System.IO.File.Move(MoveToFilenameWithPath, NewFileName);
                        }
                        catch
                        {

                        }
                    }

                    try
                    {
                        if (File.Exists(@MoveToFilenameWithPath))
                        {
                            Move_Status = null;
                            MessageBox.Show("Can't move file to: '" + FolderName + "'\nThere is another file in that folder with the same name.\n\nOperation canceled!", "Remove from Manifest Error");
                        }
                        else
                        {
                            File.Move(@filenameWithPath, @MoveToFilenameWithPath); // Try to move
                        }   
                    }
                    catch (IOException)
                    {
                        Move_Status = null;
                        MessageBox.Show("Can't move file to: '" + FolderName + "'\n\nOperation canceled!", "Remove from Manifest Error");
                    }
                }

                if (Move_Status == "1")
                {
                    Return = MoveToFolder;
                }

            }

            return Return;
        }

        public bool RemoveAccount(SteamGuardAccount account, bool deletesaFile = true)
        {
            ManifestEntry entry = (from e in this.Entries where e.SteamID == account.Session.SteamID select e).FirstOrDefault();
            if (entry == null) return true; // If something never existed, did you do what they asked?

            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
            string filenameWithPath = maDir + entry.Filename;

                this.Entries.Remove(entry);

                if (this.Entries.Count == 0)
                {
                    this.Encrypted = false;
                }

                if (this.Save() && deletesaFile)
                {
                    try
                    {
                        File.Delete(filenameWithPath);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            
            return false;
        }

        public bool UnlinkAccFromManifestStringSteamID64(string account)
        {
            ManifestEntry entry = (from e in this.Entries where e.SteamID.ToString() == account select e).FirstOrDefault();
            if (entry == null) return true; // If something never existed, did you do what they asked?

            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
            string filename = maDir + entry.Filename;
            this.Entries.Remove(entry);
            Save();
            return false;
        }


        public string ExportAccountAsMaFile(SteamGuardAccount account, string oldKey)
        {
            string Return = "empty";

            ManifestEntry entry = (from e in this.Entries where e.SteamID == account.Session.SteamID select e).FirstOrDefault();
            if (entry == null)
            {
                Return = "AccpuntNotFounInManifest";
            }
            else {
                string maDir = Manifest.GetExecutableDir() + "/saFiles/";
                string filename_withExt = entry.Filename;
                string filename = filename_withExt.Replace(".saFile", "");
                string fileFullPath = maDir + filename_withExt;

                if (filename_withExt == "" || filename_withExt == null || filename == "" || filename == null)
                {
                    Return = "ErrorFileName";
                }
                else {
                    if (File.Exists(fileFullPath))
                    {
                        string contentToWrite = File.ReadAllText(fileFullPath);

                        if (Encrypted)
                        {
                            string[] fileTextPart = Regex.Split(contentToWrite, "<next_string_of_data>");
                            string Salt = fileTextPart[0];
                            string IV = fileTextPart[1];
                            string fileContents = fileTextPart[2];

                            contentToWrite = FileEncryptor.DecryptData(oldKey, Salt, IV, fileContents);
                        }


                        if (contentToWrite == null || contentToWrite == "" || contentToWrite == " ")
                        {
                            Return = "ErrorDecryption";
                        }
                        else {
                            // Export to Desktop
                            string DesktopFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            DesktopFilePath += "\\" + filename + ".maFile";

                            // Write
                            File.WriteAllText(DesktopFilePath, contentToWrite);

                            // verify content
                            if (File.Exists(DesktopFilePath))
                            {
                                // read file 
                                string contentWritten = File.ReadAllText(DesktopFilePath);
                                if (contentWritten == contentToWrite)
                                {
                                    Return = "ok";
                                }
                                else {
                                    Return = "FailedToWriteData";
                                    try { File.Delete(DesktopFilePath); }
                                    catch (IOException) { }
                                }

                            }
                            else {
                                Return = "FailedToVerifyDataWritten";
                            }
                        }

                    }
                    else {
                        Return = "FileNotFound";
                    }
                }
            }
            return Return;
        }
        /* Return:
        empty = An error occurred
        ErrorFileName = Failed to get the name from manifest.json
        FileNotFound = Error Account file not found
        ErrorDecryption = Invalid passkey
        FailedToVerifyDataWritten
        FailedToWriteData
        ok
        */

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

            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
            string filename = account.Session.SteamID.ToString() + ".saFile";

            ManifestEntry newEntry = new ManifestEntry()
            {
                SteamID = account.Session.SteamID,
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
            string maDir = Manifest.GetExecutableDir() + "/saFiles/";
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
            string maDir = Manifest.GetExecutableDir() + "/saFiles/";

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
            /* to delete
            [JsonProperty("encryption_iv")]
            public string IV { get; set; }

            [JsonProperty("encryption_salt")]
            public string Salt { get; set; }*/

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("steamid")]
            public ulong SteamID { get; set; }
        }
    }
}
