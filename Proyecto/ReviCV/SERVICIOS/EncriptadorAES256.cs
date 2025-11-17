using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES.Tecnico
{
    public static class EncriptadorAES256
    {
        // Clave de 32 bytes → AES-256
        private static readonly byte[] Key = Convert.FromBase64String("WnV2R2t5M1pHU2ZrR3NYTG1HV2Vrc3hURlZ0R1BUZ2g=");

        // IV de 16 bytes
        private static readonly byte[] IV = Convert.FromBase64String("MTIzNDU2Nzg5MDEyMzQ1Ng==");

        public static byte[] Encrypt(byte[] data)
        {
            if (data == null || data.Length == 0)
                return data;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.Close();
                    return ms.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                return encryptedData;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedData, 0, encryptedData.Length);
                    cs.Close();
                    return ms.ToArray();
                }
            }
        }
    }
}
