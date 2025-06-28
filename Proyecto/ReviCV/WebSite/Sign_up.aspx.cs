using BE;
using BLL;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sign_up : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ErrorContraseñasLB.Visible = false;
        Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TbPassw1.Text == TbPassw2.Text)
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            Encriptador encriptador = new Encriptador();
            Usuario usuario = new Usuario(int.Parse(TbDNI.Text), TbNombre.Text, TbApellido.Text, TbUserName.Text, encriptador.EncriptarIrreversible(TbPassw1.Text), TbMail.Text);
            gestorUsuario.InsertarUsuario(usuario);
            ErrorContraseñasLB.Visible = true;
            ErrorContraseñasLB.ForeColor = System.Drawing.Color.Green;
            ErrorContraseñasLB.Text = "Usuario registrado correctamente";
            VaciarTB();
        }
        else
        {
            ErrorContraseñasLB.Visible = true;
            ErrorContraseñasLB.ForeColor = System.Drawing.Color.Red;
            ErrorContraseñasLB.Text = "Las contraseñas no coinciden";
        }

    }
    public void VaciarTB()
    {
        TbDNI.Text = string.Empty;
        TbNombre.Text = string.Empty;
        TbApellido.Text = string.Empty;
        TbUserName.Text = string.Empty;
        TbPassw1.Text = string.Empty;
        TbMail.Text = string.Empty;
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}