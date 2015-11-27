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
                File.WriteAllText(filename, contents);
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

    }
}
