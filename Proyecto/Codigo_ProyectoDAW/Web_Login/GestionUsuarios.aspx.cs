using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

public partial class GestionUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BotonAceptar_Click(object sender, EventArgs e)
    {
        GestorUsuario gUsuarios = new GestorUsuario();
        int dni;

        if(int.TryParse(tbDNI.Text, out dni))
        {
            gUsuarios.InsertarUsuario(new Usuario(dni, tbNombre.Text, tbApellido.Text, tbUsername.Text, tbPassword.Text, tbMail.Text, tbRol.Text));
            Confirmacion.Text = "El usuario se ha insertado correctamente";
        }
    }
}