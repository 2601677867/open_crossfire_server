using System;
using System.Security.Cryptography;
using System.Text;
using Commons.Log;

namespace Security.SafePassword
{
    public class CSafePassword
    {
        private static readonly string key = "9jUElQ7lhDG4BZ6XfCOSEVKfOO9JI2Os";

        public string SHA256Encrypt(string strData)
        {
            var bytValue = Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                var retVal = sha256.ComputeHash(bytValue);
                var sb = new StringBuilder();
                for (var i = 0; i < retVal.Length; i++) sb.Append(retVal[i].ToString("x2"));
                return sb.ToString();
            }
            catch (Exception ex)
            {
                CPublicLogger.GetLogger().error(ex.Message);
                CPublicLogger.GetLogger().trace(ex.StackTrace);
                return null;
            }
        }

        public static string Encrypt(string toEncrypt)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            var cTransform = rDel.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt)) return "";
            
            try
            {
                var keyArray = Encoding.UTF8.GetBytes(key);
                var toEncryptArray = Convert.FromBase64String(toDecrypt);

                var rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                var cTransform = rDel.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                CPublicLogger.GetLogger().error(ex.Message);
                CPublicLogger.GetLogger().trace(ex.StackTrace);
                return null;
            }
        }
    }
}