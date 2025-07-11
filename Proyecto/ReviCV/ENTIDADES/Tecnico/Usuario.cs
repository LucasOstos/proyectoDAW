﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public Usuario(int pDNI, string pNombre, string pApellido, string pUsuario, string pPassword, string pEmail, string rol)
        {
            DNI = pDNI;
            Nombre = pNombre;
            Apellido = pApellido;
            NombreUsuario = pUsuario;
            Password = pPassword;
            Email = pEmail;
            Rol = rol;
        }
        public Usuario(int pDNI, string pNombre, string pApellido, string pUsuario, string pPassword, string pEmail)
        {
            DNI = pDNI;
            Nombre = pNombre;
            Apellido = pApellido;
            NombreUsuario = pUsuario;
            Password = pPassword;
            Email = pEmail;
            Rol = "Usuario";
        }
        public Usuario()
        {

        }

        public string[] ToArray()
        {
            return new string[]
            {
                DNI.ToString(),
                Nombre,
                Apellido,
                NombreUsuario,
                Password,
                Email,
                Rol
            };
        }
    }
}
