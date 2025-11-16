using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page, IObserver
{
    public void Actualizar()
    {
        RecorrerControles(this);
    }
    void RecorrerControles(Control controlPadre)
    {
        TraductorDAL traductor = new TraductorDAL();
        foreach (Control c in controlPadre.Controls)
        {
            if (c is Label lbl && lbl.Attributes["data-key"] != null)
            {
                string clave = lbl.Attributes["data-key"];
                lbl.Text = traductor.Traducir(clave);
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = traductor.Traducir(clave);
            }
            else if (c is HtmlGenericControl html)
            {
                if (html.Attributes["data-key"] != null)
                {
                    string clave = html.Attributes["data-key"];
                    html.InnerText = traductor.Traducir(clave);
                }
                else if (html.TagName.Equals("h2", StringComparison.OrdinalIgnoreCase))
                {
                    html.InnerText = html.InnerText.ToUpper();
                }
            }
            if (c.HasControls())
            {
                RecorrerControles(c);
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Usuario"] != null)
            {
                labelErrores.ForeColor = System.Drawing.Color.Orange;
                labelErrores.Text = "Ya hay una sesión iniciada";
                return;
            }

            GestorUsuario gestorUsuario = new GestorUsuario();
            Usuario u = gestorUsuario.ObtenerUsuario(tbNombreUsuario.Text);

            if (u == null)
            {
                labelErrores.ForeColor = System.Drawing.Color.Red;
                labelErrores.Text = "No existe el usuario";
                return;
            }

            Validador validador = new Validador();
            Encriptador encriptador = new Encriptador();
            string pass = encriptador.EncriptarIrreversible(tbContraseña.Text);

            if (!validador.Verificar(u.NombreUsuario, pass))
            {
                labelErrores.ForeColor = System.Drawing.Color.Red;
                labelErrores.Text = "Credenciales incorrectas";
                return;
            }

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            string bdErrores = gestorIntegridad.VerificarIntegridadTodasLasTablas();

            Application["EstadoBD"] = string.IsNullOrEmpty(bdErrores);
            Application["ErroresBD"] = "";

            GestorPermisos gestorPermisos = new GestorPermisos();
            var permisoRol = gestorPermisos.ObtenerPermisoCompuesto(u.Rol);

            if (permisoRol == null)
            {
                labelErrores.ForeColor = System.Drawing.Color.Red;
                labelErrores.Text = "Su usuario no tiene rol. Comuníquese con un administrador.";
                return;
            }

            Session["Usuario"] = u;
            Session["Rol"] = permisoRol;

            if (Application["EstadoBD"].Equals(true))
            {
                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora("Login", u.NombreUsuario);
                Response.Redirect("LandingPage.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Application["ErroresBD"] = bdErrores;

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora("Error en Base de Datos: " + bdErrores, u.NombreUsuario);

                if (GestorPermisos.TienePermiso(Session["Rol"] as PermisoCompuesto, PermisosStatic.pAccesoIntegridad))
                {
                    Response.Redirect("Verificador.aspx");
                }
                else
                {
                    Response.Redirect("AvisoErrorBD.aspx");
                }

                Context.ApplicationInstance.CompleteRequest();
            }
        }
        catch
        {
            labelErrores.ForeColor = System.Drawing.Color.Red;
            labelErrores.Text = "Tiempo de espera agotado.";
        }
    }


    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Sign_Up.aspx");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //TraductorDAL traductor = new TraductorDAL();
        //traductor.Suscribe(this);
        //traductor.Notify();
        //traductor.CargarTraduccionesDesdeBD("Español");
        if (Session["Rol"] != null) Response.Redirect("LandingPage.aspx");
        //Actualizar();
    }
}