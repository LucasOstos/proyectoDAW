using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class Encriptador
    {
        public string EncriptarIrreversible(string contraseña) //SHA256
        {
            using (SHA256 encrypt = SHA256.Create())
            {
                byte[] bytes = encrypt.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder SB = new StringBuilder();
                for (int x = 0; x < bytes.Length; x++)
                {
                    SB.Append(bytes[x].ToString("x2"));
                }
                return SB.ToString();
            }
        }
    }
}
