using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Common
{
    public class EncryptUtilities
    {
        #region RSA
        public static string EncryptRSA(string partnerId, string transactionId, string data, string privateKeyXML)
        {
            string[] dal = new string[] { partnerId, transactionId, data };
            string result = SignRSASHA1(string.Join("", dal), privateKeyXML);
            return result;
        }

        public static string SignRSASHA1(string data, string privateKeyXML)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXML);

            var dataBytes = Encoding.UTF8.GetBytes(data);
            var signedData = rsa.SignData(dataBytes, CryptoConfig.MapNameToOID("SHA1"));
            return Convert.ToBase64String(signedData);
        }
        #endregion

        #region AES256
        public static string EncryptString(string text, string password, string saltStr)
        {
            try
            {
                byte[] baPwd = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.        
                byte[] saltBytes = Encoding.UTF8.GetBytes(saltStr);

                byte[] baText = Encoding.UTF8.GetBytes(text);

                var baEncrypted = AES_Encrypt(baText, baPwdHash, saltBytes);

                string result = Convert.ToBase64String(baEncrypted);
                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] encryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        #endregion
    }
}
