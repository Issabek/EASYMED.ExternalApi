using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EASYMED.ExternalApi.Services
{

    public class Crypt
    {
        #region шифруем строку
        TripleDESCryptoServiceProvider symm;

        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        //private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("IAMBESTOFTHEBEST007");
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("2JT85K9LEWo92Aw3"); //NYX
                                                                                                      // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        /// <summary>
        /// Шифрует
        /// </summary>
        public static string Encrypt(string plainText, string passPhrase, int authId = 0)
        {
            int userId = authId;
            string userIdPad = userId.ToString().PadLeft(12, '0');
            byte[] saltInfo = Encoding.ASCII.GetBytes("uIy71Oh" + userIdPad);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltInfo))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                string resval = Convert.ToBase64String(cipherTextBytes);
                                return ToHexString(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string ToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2 + 2);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string Decrypt(string cipherText, string passPhrase, int authId = 0)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
            {
                return "";
            }
            int userId = authId;
            string userIdPad = userId.ToString().PadLeft(12, '0');
            byte[] saltInfo = Encoding.ASCII.GetBytes("uIy71Oh" + userIdPad);
            string resval = "";
            try
            {
                //cipherText = HttpUtility.UrlDecode(cipherText);
                byte[] cipherTextBytes = StringToByteArray(cipherText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltInfo))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    resval = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                    return resval;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return DecryptOld(cipherText, passPhrase);
                throw new Exception("Ошибка расшифровки параметра");
            }
            return resval;
        }

        public static string DecryptOld(string cipherText, string passPhrase)
        {
            string resval = "";
            try
            {
                //cipherText = HttpUtility.UrlDecode(cipherText);
                byte[] cipherTextBytes = StringToByteArray(cipherText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes("IAMBESTOFTHEBEST007")))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    resval = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                    return resval;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка расшифровки параметра");
            }
            return resval;
        }

        #endregion
    }


}
