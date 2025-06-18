using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Celular { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public Usuario(int pDNI, string pNombre, string pApellido, int pCelular, string pUsuario, string pContraseña, string pEmail)
        {
            DNI = pDNI;
            Nombre = pNombre;
            Apellido = pApellido;
            Celular = pCelular;
            NombreUsuario = pUsuario;
            Contraseña = pContraseña;
            Email = pEmail;
        }
    }
}
