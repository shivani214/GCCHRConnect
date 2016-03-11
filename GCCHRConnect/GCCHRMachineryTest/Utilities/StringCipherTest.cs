using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;

namespace Utilities
{
    [TestClass]
    public class StringCipherTest
    {
        /// <summary>
        /// Checks if the Environment variable, which holds the passphrase for encryption/decryption, exists with the specified name
        /// </summary>
        [TestMethod]
        public void PassphraseEnvironmentVariableExists()
        {
            string passPhrase = Environment.GetEnvironmentVariable("EncryptionPassphrase", EnvironmentVariableTarget.Machine);
            Assert.IsNotNull(passPhrase);
        }

        /// <summary>
        /// Checks if the registry tree, which holds the passphrase for encryption/decryption, exists with the specified name
        /// </summary>
        [TestMethod]
        public void PassphraseRegistryExists()
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
                Assert.IsNotNull(rootKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey softwareKey = rootKey.OpenSubKey(software);
                path.Append(@"\").Append(software);
                Assert.IsNotNull(softwareKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey gcchrConnectKey = softwareKey.OpenSubKey(gcchrConnect);
                path.Append(@"\").Append(gcchrConnect);
                Assert.IsNotNull(gcchrConnectKey, "{0}{1}", errorMessage, path.ToString());

                RegistryKey securityKey = gcchrConnectKey.OpenSubKey(security);
                path.Append(@"\").Append(security);
                Assert.IsNotNull(securityKey, "{0}{1}", errorMessage, path.ToString());

                string passPhrase = (string)securityKey.GetValue(passphraseValue);
                Assert.IsNotNull(passPhrase, "Key not found:{0}", passphraseValue);
            }
            finally
            {
                path = null;
            }
        }

        [TestMethod]
        public void CanEncryptAndDecryptSampleStringCorrectly()
        {
            // Arrange
            var plainText = "This is my sample plain text message";
            var passPhrase = "supersecretpassword";

            // Act
            var cipherText = StringCipher.Encrypt(plainText, passPhrase);
            var decryptedText = StringCipher.Decrypt(cipherText, passPhrase);

            // Assert
            Assert.AreEqual(plainText, decryptedText);

        }

        [TestMethod]
        public void GenerateHash(string passPhrase)
        {
            string passwordToHash = "AnyUserPasswordOrCreditCard";
            string salt = StringCipher.RandomStringGenerator();
            //string salt = "1234";
            string passwordHash = StringCipher.GenerateHash(passwordToHash, salt);
            string encryptedHash = StringCipher.Encrypt(passwordHash, passPhrase);

            string decryptedHash = StringCipher.Decrypt(encryptedHash, passPhrase);
            string passwordMatchHash = StringCipher.GenerateHash(passwordToHash, salt);
            Assert.AreEqual(decryptedHash, passwordMatchHash);
        }

        [TestMethod]
        public void HashUsingString()
        {
            GenerateHash("CIPHERdecryptionKEY");
        }
        


        [TestMethod]
        public void EncryptingTheSamePlainTextWithTheSamePasswordMultipleTimesProducesDifferentCipherText()
        {
            // Arrange
            var plainText = "This is my sample plain text message";
            var passPhrase = "supersecretpassword";

            // Act
            var cipherText1 = StringCipher.Encrypt(plainText, passPhrase);
            var cipherText2 = StringCipher.Encrypt(plainText, passPhrase);

            // Assert
            Assert.AreNotEqual(cipherText1, cipherText2);
        }

        [TestMethod]
        public void EncryptingTheSamePlainTextWithTheSamePasswordMultipleTimesProducesDifferentCipherTextButBothCanBeDecryptedCorrectly()
        {
            // Arrange
            var plainText = "This is my sample plain text message";
            var passPhrase = "supersecretpassword";

            // Act
            var cipherText1 = StringCipher.Encrypt(plainText, passPhrase);
            var cipherText2 = StringCipher.Encrypt(plainText, passPhrase);
            var decryptedText1 = StringCipher.Decrypt(cipherText1, passPhrase);
            var decryptedText2 = StringCipher.Decrypt(cipherText2, passPhrase);

            // Assert
            Assert.AreEqual(decryptedText1, plainText);
            Assert.AreEqual(decryptedText1, decryptedText2);

        }
    }
}
