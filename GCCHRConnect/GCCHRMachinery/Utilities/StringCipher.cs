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
    /// This class provides Hashing + Encryption/Decryption capabilities.
    /// </summary>
    /// <see href="http://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp"/>
    /// <see href="http://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string"/>
    /// <see href="https://crackstation.net/hashing-security.htm"/>
    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int KEY_SIZE = 32;
        private const int HASH_SIZE = 64;
        // This constant determines the number of iterations for the password bytes generation function.
        private const int DERIVATIONITERATIONS = 64000;

        /// <summary>
        /// Encrypts the provided string by using a passphrase from Windows Registry or Environment variable
        /// </summary>
        /// <param name="plainText">The string to encrypt</param>
        /// <returns>Encrypted string</returns>
        /// <seealso cref="getPassphrase"/>
        public static string Encrypt(string plainText)
        {
            //string plainText = ToInsecureString(securedString);
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            //var saltStringBytes = Generate256BitsOfRandomEntropy();
            byte[] saltStringBytes = Convert.FromBase64String(GenerateRandomEntropy(KEY_SIZE));
            byte[] ivStringBytes = Convert.FromBase64String(GenerateRandomEntropy(KEY_SIZE));
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            string passPhrase = getPassphrase();
            var keyBytes = keyStretch_PBKDF2(passPhrase, saltStringBytes,KEY_SIZE);

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
        /// Decrypts the encrypted string provided in <paramref name="cipherText"/> by using a passphrase from Windows Registry or Environment variable
        /// </summary>
        /// <param name="cipherText">The string to decrypt</param>
        /// <returns>A string of the decrypted value</returns>
        public static string Decrypt(string cipherText)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEY_SIZE).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEY_SIZE).Take(KEY_SIZE).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((KEY_SIZE) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((KEY_SIZE / 8) * 2)).ToArray();

            string passPhrase = getPassphrase();
            var keyBytes = keyStretch_PBKDF2(passPhrase, saltStringBytes,KEY_SIZE);

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

        /// <summary>
        /// Hashes the <paramref name="plainText"/> followed by encrypting it with <see cref="Encrypt(string)"/>
        /// </summary>
        /// <param name="plainText">The text to be hashed</param>
        /// <param name="salt">A 64 bit string obtained from <see cref="GenerateRandomEntropy(int)"/></param>
        /// <returns>The encrypted hash for the provided <paramref name="plainText"/></returns>
        public static string GenerateHash(string plainText, string salt)
        {
            #region Verify that the passphrase for encryption exists in either Windows Registry or Environment variable
            try
            {
                getPassphrase();
            }
            catch (ApplicationException)
            {
                throw;
            } 
            #endregion

            #region Check password or salt
            if (string.IsNullOrWhiteSpace(plainText))
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
                throw new ArgumentException("salt", "Null value found! The salt should be generated using GenerateRandomEntropy(64) before hashing");
            }
            blankSalt = null;

            if (saltBytes.Length != 64)
            {
                saltBytes = null;
                throw new ArgumentException("Incorrect size! The salt should be generated using GenerateRandomEntropy(64) before hashing", "salt");
            }
            #endregion

            SHA512Managed sha512 = new SHA512Managed();
            //string saltedPassword = plainPassword + salt;
            //byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            //var hash = sha512.ComputeHash(saltedPasswordBytes);

            byte[] saltedPasswordBytesKeyStretched = keyStretch_PBKDF2(plainText, saltBytes,HASH_SIZE);
            byte[] hashKeyStretched = sha512.ComputeHash(saltedPasswordBytesKeyStretched);
            string hash = Convert.ToBase64String(hashKeyStretched);
            string encryptedHash = Encrypt(hash);
            return encryptedHash;
        }

        /// <summary>
        /// Generates a random string using the .Net implemented RNGCryptoServiceProvider of the CSPRNG algorithm of Cryptography
        /// </summary>
        /// <param name="byteSize">32 for <see cref="Encrypt(string)"/> or 64 for <see cref="GenerateHash(string, string)"/></param>
        /// <returns>The random string of 32 or 64 bit</returns>
        /// <exception cref="ArgumentException">Acceptable values for <paramref name="byteSize"/> are 32 or 64</exception>
        public static string GenerateRandomEntropy(int byteSize)
        {
            verifyByteSize32Or64(byteSize);
            var randomBytes = new byte[byteSize]; // 32 Bytes will give us 256 bits, 64 Bytes will give us 512 bits.
            using (var csprng = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                csprng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        /// <summary>
        /// Checks if the provided number is either 32 or 64
        /// </summary>
        /// <param name="byteSize">Acceptable values: 32 for <see cref="Encrypt(string)"/> and 64 for <see cref="GenerateHash(string, string)"/></param>
        /// <exception cref="ArgumentException">If values not 32 or 64</exception>
        private static void verifyByteSize32Or64(int byteSize)
        {
            if (byteSize != KEY_SIZE && byteSize != HASH_SIZE)
            {
                throw new ArgumentException("Only 32 or 64 valid", "byteSize");
            }
        }

        /// <summary>
        /// Implements the additional security measure of Key stretching
        /// </summary>
        /// <param name="password">The text to be secured</param>
        /// <param name="saltBytes">The generated salt to be added to <paramref name="password"/></param>
        /// <param name="byteSize">32 for <see cref="Encrypt(string)"/> or 64 for <see cref="GenerateHash(string, string)"/></param>
        /// <returns>Key stretched bytes</returns>
        private static byte[] keyStretch_PBKDF2(string password, byte[] saltBytes, int byteSize)
        {
            //byte[] saltBytes = Convert.FromBase64String(saltBytes);
            verifyByteSize32Or64(byteSize);
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
        private static string getPassphrase()
        {
            string passPhrase;
            try
            {
                passPhrase = getPassphraseFromRegistry();
            }
            catch (ApplicationException passPhraseAbsentInRegistry)
            {
                try
                {
                    passPhrase = getPassphraseFromEnvironmentVariable();
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
        /// Gets passphrase from Windows Registry<seealso cref="getPassphrase"/>
        /// </summary>
        /// <returns>The passphrase from Windows Registry</returns>
        /// <exception cref="ApplicationException">When any of the folders in the tree structure or the EncryptionPassphrase key is missing in Windows Registry</exception>
        private static string getPassphraseFromRegistry()
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
        /// Gets passphrase<seealso cref="getPassphrase"/>
        /// </summary>
        /// <returns>The passphrase from environment variable</returns>
        /// <exception cref="ApplicationException">Thrown if Environment variable with the name EncryptionPassphrase does not exist or if it holds blank value</exception>
        private static string getPassphraseFromEnvironmentVariable()
        {
            string passPhrase = Environment.GetEnvironmentVariable("EncryptionPassphrase", EnvironmentVariableTarget.Machine);
            if (passPhrase == null || string.IsNullOrWhiteSpace(passPhrase))
            {
                throw new ApplicationException("Environment variable for passphrase does not exist.");
            }
            //Assert.IsNotNull(passPhrase);
            return passPhrase;
        }
        #endregion
    }
}
