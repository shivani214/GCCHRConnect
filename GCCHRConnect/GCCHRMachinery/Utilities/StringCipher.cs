using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Security;
using Microsoft.Win32;

namespace Utilities
{
    /// <summary>
    /// This class provides encryption and decryption capabilities.
    /// Encryption steps:-
    /// 1. Any sensitive information should not be held in memory in the form of <see cref="string"/>. Hence it is recommended to convert it to <see cref="SecureString"/>immediately after it is received from input. <see cref="ToInsecureString(SecureString)"/> does this for you.
    /// 2. Actual encryption is then performed by <see cref="Encrypt(SecureString, string)"/>.
    /// 
    /// Decryption steps:-
    /// 1. Follow reverse steps of Encryption. The encrypted value must first be decrypted by <see cref="Decrypt(string, string)"/>
    /// 2. The decrypted <see cref="SecureString"/> must be hopped around and its conversion to <see cref="string"/> by <see cref="ToInsecureString(SecureString)"/>must be delayed as much as possible
    /// </summary>
    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int KEY_SIZE = 32;
        private const int HASH_SIZE = 64;
        // This constant determines the number of iterations for the password bytes generation function.
        private const int DERIVATIONITERATIONS = 64000;
        /// <summary>
        /// Encrypts the provided string
        /// </summary>
        /// <param name="securedString">The string to encrypt</param>
        /// <param name="passPhrase">The password to use for encryption and decryption</param>
        /// <returns>Encrypted string</returns>
        public static string Encrypt(string plainText)
        {
            //string plainText = ToInsecureString(securedString);
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            //var saltStringBytes = Generate256BitsOfRandomEntropy();
            byte[] saltStringBytes = Convert.FromBase64String(GenerateRandomEntropy(KEY_SIZE));
            byte[] ivStringBytes = Convert.FromBase64String(GenerateRandomEntropy(KEY_SIZE));
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            string passPhrase = GetPassphrase();
            var keyBytes = KeyStretch_PBKDF2(passPhrase, saltStringBytes,KEY_SIZE);

            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                            var cipherTextBytes = saltStringBytes;
                            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                            memoryStream.Close();
                            cryptoStream.Close();
                            return Convert.ToBase64String(cipherTextBytes);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the provided encrypted string
        /// </summary>
        /// <param name="cipherText">The string to decrypt</param>
        /// <param name="passPhrase">The password provided during encryption</param>
        /// <returns>A secure string of the decrypted value</returns>
        public static string Decrypt(string cipherText)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEY_SIZE / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEY_SIZE / 8).Take(KEY_SIZE / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((KEY_SIZE / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((KEY_SIZE / 8) * 2)).ToArray();

            string passPhrase = GetPassphrase();
            var keyBytes = KeyStretch_PBKDF2(passPhrase, saltStringBytes,KEY_SIZE);

            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                {
                    using (var memoryStream = new MemoryStream(cipherTextBytes))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var plainTextBytes = new byte[cipherTextBytes.Length];
                            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memoryStream.Close();
                            cryptoStream.Close();
                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts a <see cref="string"/> to <see cref="SecureString"/>
        /// </summary>
        /// <param name="input">The plain string</param>
        /// <returns>Secure string</returns>
        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        /// <summary>
        /// Converts a <see cref="SecureString" to <see cref="string"/>/>
        /// </summary>
        /// <param name="input">The secure string</param>
        /// <returns>Plain string</returns>
        public static string FromInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        #region My security features for sha-512
        public static string GenerateHash(string plainPassword, string salt)
        {
            #region Check password or salt
            if (string.IsNullOrWhiteSpace(plainPassword))
            {
                throw new ArgumentNullException("plainPassword");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException("salt");
            }

            byte[] blankSalt = new byte[64];
            byte[] saltBytes = Convert.FromBase64String(salt);

            if (saltBytes == blankSalt)
            {
                blankSalt = null;
                saltBytes = null;
                throw new ArgumentException("salt", "Null value found! The salt should be generated using RandomGenerator() before hashing");
            }
            blankSalt = null;

            if (saltBytes.Length != 64)
            {
                saltBytes = null;
                throw new ArgumentException("Incorrect size! The salt should be generated using RandomGenerator() before hashing", "salt");
            }
            #endregion

            SHA512Managed sha512 = new SHA512Managed();
            //string saltedPassword = plainPassword + salt;
            //byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            //var hash = sha512.ComputeHash(saltedPasswordBytes);

            byte[] saltedPasswordBytesKeyStretched = KeyStretch_PBKDF2(plainPassword, saltBytes,HASH_SIZE);
            var hashKeyStretched = sha512.ComputeHash(saltedPasswordBytesKeyStretched);
            //return encrypted hash
            return Convert.ToBase64String(hashKeyStretched);
        }

        public static string GenerateRandomEntropy(int byteSize)
        {
            if (byteSize != KEY_SIZE && byteSize != HASH_SIZE)
            {
                throw new ArgumentException("Only 32 or 64 valid", "byteSize");
            }
            var randomBytes = new byte[byteSize]; // 32 Bytes will give us 256 bits, 64 Bytes will give us 512 bits.
            using (var csprng = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                csprng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private static byte[] KeyStretch_PBKDF2(string password, byte[] saltBytes, int byteSize)
        {
            //byte[] saltBytes = Convert.FromBase64String(saltBytes);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, DERIVATIONITERATIONS))
            {
                return pbkdf2.GetBytes(byteSize);
            }
        }


        #region Getting Passphrase from Registry or Environment variable for Encryption/Decryption
        /// <summary>
        /// Gets the passphrase for <see cref="Encrypt(string)"/> and <see cref="Decrypt(string, string)"/> from Windows Registry, and if not found, Environment Vaiable
        /// </summary>
        /// <returns>Passphrase from registry or environment variable</returns>
        /// <exception cref="ApplicationException">Thrown if Passphrase is not found in either Windows Registry or Environment variable.</exception>
        private static string GetPassphrase()
        {
            string passPhrase;
            try
            {
                passPhrase = GetPassphraseFromRegistry();
            }
            catch (ApplicationException passPhraseAbsentInRegistry)
            {
                try
                {
                    passPhrase = GetPassphraseFromEnvironmentVariable();
                }
                catch (ApplicationException passPhraseAbsentInEnvironmentVariable)
                {
                    throw new ApplicationException(string.Format("{0} {1}", passPhraseAbsentInRegistry.Message, passPhraseAbsentInEnvironmentVariable.Message));
                }
                //throw;
            }

            return passPhrase;
        }
        /// <summary>
        /// Gets passphrase <seealso cref="GetPassphrase"/>
        /// </summary>
        /// <returns>The passphrase from Windows Registry</returns>
        /// <exception cref="ApplicationException">When any of the folders in the tree structure or the EncryptionPassphrase key is missing in Windows Registry</exception>
        private static string GetPassphraseFromRegistry()
        {
            #region TreeStructure
            string root = "HKEY_LOCAL_MACHINE";
            string software = "SOFTWARE";
            string gcchrConnect = "GCCHRConnect";
            string security = "Security";
            string passphraseValue = "EncryptionPassphrase";
            #endregion
            string errorMessage = "Registry path not found: ";
            System.Text.StringBuilder path = new System.Text.StringBuilder();

            try
            {
                RegistryKey rootKey;
                if (Environment.Is64BitOperatingSystem)
                    rootKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                else
                    rootKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                path.Append(root);
                if (rootKey == null)
                {
                    throw new ApplicationException(string.Format("{0}{1}.", errorMessage, path.ToString()));
                }
                //Assert.IsNotNull(rootKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey softwareKey = rootKey.OpenSubKey(software);
                path.Append(@"\").Append(software);
                if (softwareKey == null)
                {
                    throw new ApplicationException(string.Format("{0}{1}.", errorMessage, path.ToString()));
                }
                //Assert.IsNotNull(softwareKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey gcchrConnectKey = softwareKey.OpenSubKey(gcchrConnect);
                path.Append(@"\").Append(gcchrConnect);
                if (gcchrConnectKey == null)
                {
                    throw new ApplicationException(string.Format("{0}{1}.", errorMessage, path.ToString()));
                }
                //Assert.IsNotNull(gcchrConnectKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey securityKey = gcchrConnectKey.OpenSubKey(security);
                path.Append(@"\").Append(security);
                if (securityKey == null)
                {
                    throw new ApplicationException(string.Format("{0}{1}.", errorMessage, path.ToString()));
                }
                //Assert.IsNotNull(securityKey, "{0}{1}", errorMessage, path.ToString());

                string passPhrase = (string)securityKey.GetValue(passphraseValue);
                if (passPhrase == null)
                {
                    throw new ApplicationException(string.Format("{0}{1}.", errorMessage, path.ToString()));
                }
                //Assert.IsNotNull(passPhrase, "Key not found:{0}", passphraseValue);
                return passPhrase;
            }
            finally
            {
                path = null;
            }
        }

        /// <summary>
        /// Gets passphrase <seealso cref="GetPassphrase"/>
        /// </summary>
        /// <returns>The passphrase from environment variable</returns>
        /// <exception cref="ApplicationException">Thrown if Environment variable with the name EncryptionPassphrase does not exist</exception>
        public static string GetPassphraseFromEnvironmentVariable()
        {
            string passPhrase = Environment.GetEnvironmentVariable("EncryptionPassphrase", EnvironmentVariableTarget.Machine);
            if (passPhrase == null)
            {
                throw new ApplicationException("Environment variable for passphrase does not exist.");
            }
            //Assert.IsNotNull(passPhrase);
            return passPhrase;
        }
        #endregion
        #endregion
    }
}
