using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Desktop_Authenticator
{
    public class MobileAuthenticatorFileHandler
    {
        public static bool SaveMaFile(SteamGuardAccount account)
        {
            string filename = "./maFiles/" + account.Session.SteamID + ".maFile";
            if (!Directory.Exists("./maFiles"))
            {
                try
                {
                    Directory.CreateDirectory("./maFiles");
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            try
            {
                string contents = JsonConvert.SerializeObject(account, Formatting.Indented);
                // Encrypt the JSON
                byte[] encrypted = EncryptionHandler.EncryptString(contents);
                File.WriteAllBytes(filename, encrypted);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static void DeleteMaFile(SteamGuardAccount account)
        {
            string filename = "./maFiles/" + account.Session.SteamID + ".maFile";
            File.Delete(filename);
        }

        public static SteamGuardAccount[] GetAllAccounts()
        {
            if (!Directory.Exists("./maFiles")) return new SteamGuardAccount[0];

            DirectoryInfo info = new DirectoryInfo("./maFiles");
            var files = info.GetFiles("*.maFile");

            List<SteamGuardAccount> accounts = new List<SteamGuardAccount>();
            foreach (var file in files)
            {
                try
                {
                    string text = EncryptionHandler.DecryptString(File.ReadAllBytes(file.FullName));
                    SteamGuardAccount account = JsonConvert.DeserializeObject<SteamGuardAccount>(text);
                    accounts.Add(account);
                }
                catch (Exception e)
                {

                }
            }

            return accounts.ToArray();
        }
    }
}
