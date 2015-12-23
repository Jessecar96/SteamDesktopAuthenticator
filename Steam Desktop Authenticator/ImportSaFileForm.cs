using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Steam_Desktop_Authenticator
{
    public partial class ImportSaFileForm : Form
    {
        private SteamGuardAccount mCurrentAccount = null;
        private Manifest manifest;

        public ImportSaFileForm()
        {
            InitializeComponent();

            this.manifest = Manifest.GetManifest();

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // check if data already added is encripted
            #region check if data already added is encripted
            string ContiuneImport = "0";

            string ManifestFile = "saFiles/manifest.json";
            if (File.Exists(ManifestFile))
            {
                string AppManifest_maFileContents = File.ReadAllText(ManifestFile);
                AppManifest_maFile_Import_saFile AppManifest_maFileData = JsonConvert.DeserializeObject<AppManifest_maFile_Import_saFile>(AppManifest_maFileContents);
                bool AppManifest_maFileData_encrypted = AppManifest_maFileData.Encrypted;
                if (AppManifest_maFileData_encrypted == true)
                {
                    MessageBox.Show("You can't import an .saFile because the existing account in the app is encrypted.\nDecrypt it and try again.");
                    this.Close();
                }
                else if (AppManifest_maFileData_encrypted == false)
                {
                    ContiuneImport = "1";
                }
                else
                {
                    MessageBox.Show("invalid value for variable 'encrypted' inside manifest.json");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("An Error occurred, Restart the program!");
            }
            #endregion

            // Continue
            #region Continue
            if (ContiuneImport == "1")
            {
                this.Close();

                // read EncriptionKey from imput box
                string ImportUsingEncriptionKey = txtBox.Text;

                // Open file browser > to select the file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Set filter options and filter index.
                openFileDialog1.Filter = "saFiles (.saFile)|*.saFile|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = false;

                // Call the ShowDialog method to show the dialog box.
                DialogResult userClickedOK = openFileDialog1.ShowDialog();

                // Process input if the user clicked OK.
                if (userClickedOK == DialogResult.OK)
                {
                    // Open the selected file to read.
                    System.IO.Stream fileStream = openFileDialog1.OpenFile();
                    string fileContents = null;

                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        fileContents = reader.ReadToEnd();
                    }
                    fileStream.Close();

                    //Declare
                    string Run_Import_saFile = null;
                    string Run_ImportEncrypted_saFile = null;

                    // check if file is encrypted
                    string fileImportedIsEncrypted = null;
                    if (fileContents.Contains("<next_string_of_data>")) { fileImportedIsEncrypted = "1"; } // search in string


                    // user encryption key added for decryption
                    if (ImportUsingEncriptionKey == "")
                    {
                        // Import saFile - trigger
                        //-------------------------------------------
                        if (fileImportedIsEncrypted == "1")
                        {
                            MessageBox.Show("Failed to import! This .saFile is encrypted!");
                        }
                        else {
                            Run_Import_saFile = "1";
                        }
                    }
                    else
                    {
                        // Import Encripted saFile - trigger
                        //-------------------------------------------
                        if (fileImportedIsEncrypted == null)
                        {
                            //This .saFile is not encrypted
                            Run_Import_saFile = "1";
                        }
                        else {
                            Run_ImportEncrypted_saFile = "1";
                        }
                    }

                    // Import saFile
                    //-------------------------------------------
                    #region Import saFile
                    if (Run_Import_saFile == "1")
                    {
                        SteamGuardAccount saFile = JsonConvert.DeserializeObject<SteamGuardAccount>(fileContents);
                        if (saFile.Session.SteamID != 0)
                        {
                            manifest.SaveAccount(saFile, false);
                            MessageBox.Show("Account Imported!");
                        }
                        else
                        {
                            throw new Exception("Invalid SteamID");
                        }
                    }
                    #endregion //Import saFile END


                    // Import Encripted saFile
                    //-------------------------------------------
                    #region Import Encripted saFile
                    if (Run_ImportEncrypted_saFile == "1")
                    {
                        // extract folder path
                        string fullPath = openFileDialog1.FileName;
                        string fileName = openFileDialog1.SafeFileName;
                        string path = fullPath.Replace(fileName, "");

                        // extract fileName
                        string ImportFileName = fullPath.Replace(path, "");

                        string ImportManifest_maFileFile = path + "manifest.json";

                        // extract data
                        string[] fileTextPart = Regex.Split(fileContents, "<next_string_of_data>");
                        string Salt = fileTextPart[0];
                        string IV = fileTextPart[1];
                        fileContents = fileTextPart[2];


                        if (Salt == null || IV == null || fileContents == null)
                        {
                            MessageBox.Show("This file is not a valid SteamAuth saFile.\nImport Failed.");
                        }
                        else
                        {
                            // DECRIPT & Import
                            //--------------------
                            string decryptedText = FileEncryptor.DecryptData(ImportUsingEncriptionKey, Salt, IV, fileContents);


                            if (decryptedText == null)
                            {
                                MessageBox.Show("Decryption Failed.\nImport Failed.");
                            }
                            else
                            {
                                string fileText = decryptedText;

                                SteamGuardAccount saFile = JsonConvert.DeserializeObject<SteamGuardAccount>(fileText);
                                if (saFile.Session.SteamID != 0)
                                {
                                    manifest.SaveAccount(saFile, false);
                                    MessageBox.Show("Account Imported!\nYour Account in now Decrypted!");
                                    //MainForm.loadAccountsList();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid SteamID.\nImport Failed.");
                                }
                            }

                        }
                    }
                    #endregion //Import Encripted saFile END




                }
            }
            #endregion // Continue End
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportSaFileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }


    public class AppManifest_maFile_Import_saFile
    {
        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }
    }


    public class ImportManifest_maFile_Import_saFile
    {
        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("entries")]
        public List<ImportManifestEntry_maFile_Import_saFile> Entries { get; set; }
    }

    public class ImportManifestEntry_maFile_Import_saFile
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("steamid")]
        public ulong SteamID { get; set; }
    }
}
