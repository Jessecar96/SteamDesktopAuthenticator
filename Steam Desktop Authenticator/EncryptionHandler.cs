using System;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace Steam_Desktop_Authenticator
{
    public class EncryptionHandler
    {
        private static void PadToMultipleOf(ref byte[] src, int pad)
        {
            int len = (src.Length + pad - 1) / pad * pad;
            Array.Resize(ref src, len);
        }

		public static void EncryptData(ref byte[] buffer, MemoryProtectionScope scope)
		{
			// Check our buffer for data
			if (buffer.Length <= 0)
				throw new ArgumentException("Buffer");
			if (buffer == null)
				throw new ArgumentNullException("Buffer");

			PadToMultipleOf(ref buffer, 16);

			// Encrypt the buffer. This function doesn't return the data
			// as it is stored in the same byte array as the original
			ProtectedMemory.Protect(buffer, scope);
		}

        public static void DecryptData(byte[] buffer, MemoryProtectionScope scope)
        {
            // Check our buffer for data
            if (buffer.Length <= 0)
                throw new ArgumentException("Buffer");
            if (buffer == null)
                throw new ArgumentNullException("Buffer");

            // Decrypt the buffer. This function doesn't return the data
            // as it is stored in the same byte array as the original
            ProtectedMemory.Unprotect(buffer, scope);
        }

        public static byte[] EncryptString(string input)
        {
            byte[] toEncrypt = Encoding.Unicode.GetBytes(input);
            EncryptData(ref toEncrypt, MemoryProtectionScope.SameLogon);
            return toEncrypt;
        }

        public static string DecryptString(byte[] input)
        {
            DecryptData(input, MemoryProtectionScope.SameLogon);
            return Encoding.Unicode.GetString(input);
        }
    }
}
