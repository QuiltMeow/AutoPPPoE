using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoPPPoE
{
    public static class SimpleAES
    {
        public static string AESEncryptBase64(string source, string cryptoKey)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
            aes.Key = key;
            aes.IV = iv;

            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string AESDecryptBase64(string encryptData, string cryptoKey)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
            aes.Key = key;
            aes.IV = iv;

            byte[] dataByteArray = Convert.FromBase64String(encryptData);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}