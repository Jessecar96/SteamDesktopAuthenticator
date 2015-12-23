using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Steam_Desktop_Authenticator
{
    /// <summary>
    /// This class provides the controls that will encrypt and decrypt the *.maFile files
    /// 
    /// Passwords entered will be passed into 100k rounds of PBKDF2 (RFC2898) with a cryptographically random salt.
    /// The generated key will then be passed into AES-256 (RijndalManaged) which will encrypt the data
    /// in cypher block chaining (CBC) mode, and then write both the PBKDF2 salt and encrypted data onto the disk.
    /// </summary>
    public static class FileEncryptor
    {
        private const int PBKDF2_ITERATIONS = 50000; //Set to 50k to make program not unbearably slow. May increase in future.
        private const int SALT_LENGTH = 8;
        private const int KEY_SIZE_BYTES = 32;
        private const int IV_LENGTH = 16;

        /// <summary>
        /// Returns an 8-byte cryptographically random salt in base64 encoding
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSalt()
        {
            byte[] salt = new byte[SALT_LENGTH];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Returns a 16-byte cryptographically random initialization vector (IV) in base64 encoding
        /// </summary>
        /// <returns></returns>
        public static string GetInitializationVector()
        {
            byte[] IV = new byte[IV_LENGTH];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(IV);
            }
            return Convert.ToBase64String(IV);
        }


        /// <summary>
        /// Generates an encryption key derived using a password, a random salt, and specified number of rounds of PBKDF2
        /// 
        /// TODO: pass in password via SecureString?
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static byte[] GetEncryptionKey(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is empty");
            }
            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Salt is empty");
            }
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), PBKDF2_ITERATIONS))
            {
                return pbkdf2.GetBytes(KEY_SIZE_BYTES);
            }
        }

        /// <summary>
        /// Tries to decrypt and return data given an encrypted base64 encoded string. Must use the same
        /// password, salt, IV, and ciphertext that was used during the original encryption of the data.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="IV">Initialization Vector</param>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static string DecryptData(string password, string passwordSalt, string IV, string encryptedData)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is empty");
            }
            if (string.IsNullOrEmpty(passwordSalt))
            {
                throw new ArgumentException("Salt is empty");
            }
            if (string.IsNullOrEmpty(IV))
            {
                throw new ArgumentException("Initialization Vector is empty");
            }
            if (string.IsNullOrEmpty(encryptedData))
            {
                throw new ArgumentException("Encrypted data is empty");
            }

            byte[] cipherText = Convert.FromBase64String(encryptedData);
            byte[] key = GetEncryptionKey(password, passwordSalt);
            string plaintext = null;

            using (RijndaelManaged aes256 = new RijndaelManaged())
            {
                aes256.IV = Convert.FromBase64String(IV);
                aes256.Key = key;
                aes256.Padding = PaddingMode.PKCS7;
                aes256.Mode = CipherMode.CBC;

                //create decryptor to perform the stream transform
                ICryptoTransform decryptor = aes256.CreateDecryptor(aes256.Key, aes256.IV);

                //wrap in a try since a bad password yields a bad key, which would throw an exception on decrypt
                try
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch (CryptographicException)
                {
                    plaintext = null;
                }
            }
            return plaintext;
        }

        /// <summary>
        /// Encrypts a string given a password, salt, and initialization vector, then returns result in base64 encoded string.
        /// 
        /// To retrieve this data, you must decrypt with the same password, salt, IV, and cyphertext that was used during encryption
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="IV"></param>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string EncryptData(string password, string passwordSalt, string IV, string plaintext)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is empty");
            }
            if (string.IsNullOrEmpty(passwordSalt))
            {
                throw new ArgumentException("Salt is empty");
            }
            if (string.IsNullOrEmpty(IV))
            {
                throw new ArgumentException("Initialization Vector is empty");
            }
            if (string.IsNullOrEmpty(plaintext))
            {
                throw new ArgumentException("Plaintext data is empty");
            }
            byte[] key = GetEncryptionKey(password, passwordSalt);
            byte[] ciphertext;

            using (RijndaelManaged aes256 = new RijndaelManaged())
            {
                aes256.Key = key;
                aes256.IV = Convert.FromBase64String(IV);
                aes256.Padding = PaddingMode.PKCS7;
                aes256.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes256.CreateEncryptor(aes256.Key, aes256.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncypt = new StreamWriter(csEncrypt))
                        {
                            swEncypt.Write(plaintext);
                        }
                        ciphertext = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(ciphertext);
        }
    }
}
