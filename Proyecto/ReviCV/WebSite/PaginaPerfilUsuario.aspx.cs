using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaginaPerfilUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == "") Response.Redirect("LandingPage.aspx");
        if (!IsPostBack)
        {
            SettearHiddenFields();
        }
    }


    private void SettearHiddenFields()
    {
        string nombreUsuario = Session["username"] as string;

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            Usuario usuario = gestorUsuario.ObtenerUsuario(nombreUsuario);

            if (usuario != null)
            {
                username.Text = usuario.NombreUsuario;
                firstName.Text = usuario.Nombre;
                lastName.Text = usuario.Apellido;
                email.Text = usuario.Email;

                hfOriginalUsername.Value = usuario.NombreUsuario;
                hfOriginalFirstName.Value = usuario.Nombre;
                hfOriginalLastName.Value = usuario.Apellido;
                hfOriginalEmail.Value = usuario.Email;
                hfOriginalDNI.Value = usuario.DNI.ToString();
                hfOriginalRol.Value = usuario.Rol;

                lblNombrePerfil.Text = usuario.Nombre + " " + usuario.Apellido;
                lblUsuarioPerfil.Text = usuario.NombreUsuario;
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (username.Text == "" || firstName.Text == "" || lastName.Text == "" || email.Text == "")
        {
            string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'Necesitas completar todos los campos.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'PaginaPerfilUsuario.aspx';
                });
            } else {
                window.location.href = 'PaginaPerfilUsuario.aspx';
            }
        });";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
        }
        else
        {


            GestorUsuario gestorUsuario = new GestorUsuario();
            Usuario usuario = new Usuario
            {
                NombreUsuario = username.Text,
                Nombre = firstName.Text,
                Password = "",
                Apellido = lastName.Text,
                Email = email.Text,
                Rol = hfOriginalRol.Value,
                DNI = int.Parse(hfOriginalDNI.Value)
            };
            gestorUsuario.ModificarUsuario(usuario);
            Session["username"] = username.Text;
            SettearHiddenFields();

            string script = @"
        document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '¡Actualizaste tus datos!',
                    text: 'Cambios realizados con éxito.',
                    icon: 'success',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'PaginaPerfilUsuario.aspx';
                });
            } else {
                window.location.href = 'PaginaPerfilUsuario.aspx';
            }
        });";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        username.Text = hfOriginalUsername.Value;
        firstName.Text = hfOriginalFirstName.Value;
        lastName.Text = hfOriginalLastName.Value;
        email.Text = hfOriginalEmail.Value;
    }


    protected void btnCambiarPassword_Click(object sender, EventArgs e)
    {
        if (newPassword.Text == confirmPassword.Text)
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            gestorUsuario.CambiarPassword(int.Parse(hfOriginalDNI.Value), newPassword.Text);

            string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '¡Actualizaste tus contraseña!',
                    text: 'Contraseña cambiada con éxito.',
                    icon: 'success',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'PaginaPerfilUsuario.aspx';
                });
            } else {
                window.location.href = 'PaginaPerfilUsuario.aspx';
            }
        });";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
        }
        else
        {
            string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'Las contraseñas no coinciden.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'PaginaPerfilUsuario.aspx';
                });
            } else {
                window.location.href = 'PaginaPerfilUsuario.aspx';
            }
        });";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
        }
    }

    protected void btnVolverPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }
}