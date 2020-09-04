using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ECommerce.Web.Helpers
{
    public class AccCoder
    {
        private static string key = "z81fvm";
        public static string Encode(string encodeMe)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(encodeMe);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Dispose();
                    }
                    encodeMe = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encodeMe;
        }
        public static string Decode(string decodeMe)
        {
            byte[] cipherBytes = Convert.FromBase64String(decodeMe);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Dispose();
                    }
                    decodeMe = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return decodeMe;
        }
        public static string GetUniqueDigits()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();

            //var bytes = new byte[4];
            //var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(bytes);
            //uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            //return String.Format("{0:D6}", random);
        }
    }
}