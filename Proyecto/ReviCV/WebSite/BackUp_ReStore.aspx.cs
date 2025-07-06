using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SERVICIOS;
public partial class BackUp_ReStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"].ToString() != "Webmaster") Response.Redirect("LandingPage.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // Ruta donde guardás temporalmente el archivo en el servidor
        string backupFolder = Server.MapPath("~/TempBackups");

        // Asegurarte que exista el directorio
        if (!Directory.Exists(backupFolder))
            Directory.CreateDirectory(backupFolder);

        // Llamás a tu función y obtenés la ruta real del .bak
        string rutaGenerada = BackupRestore.DalBURS.RealizarBackup(backupFolder);
        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora("Backup de la base de datos creado", $"{Session["username"].ToString()}");
        // Forzás la descarga al navegador
        DescargarArchivo(rutaGenerada);
    }
    private void DescargarArchivo(string rutaCompleta)
    {
        string nombreArchivo = Path.GetFileName(rutaCompleta);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", $"attachment; filename={nombreArchivo}");
        Response.TransmitFile(rutaCompleta);
        LblConfirmacionBackup.Text = "Backup Realizado con exito";
        Response.End();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        HttpFileCollection archivos = Request.Files;

        if (archivos.Count > 0 && archivos[0] != null && archivos[0].ContentLength > 0)
        {
            HttpPostedFile archivo_BackUp = archivos[0];
            string nombreArchivo = Path.GetFileName(archivo_BackUp.FileName);
            string rutaCarpeta = Server.MapPath("~/TempRestore/");
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }
            string rutaDestino = Path.Combine(rutaCarpeta, nombreArchivo);
            archivo_BackUp.SaveAs(rutaDestino);
            BackupRestore.DalBURS.RealizarRestore(rutaDestino);
            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.GuardarLogBitacora("Restauración de la base de datos", $"{Session["username"].ToString()}");
            LblConfirmacionRestore.Text = "Restore Realizado con exito";
        }
    }

    protected void BtnBuscarBackup_Click(object sender, EventArgs e)
    {

    }

    protected void BtnBuscarRestore_Click(object sender, EventArgs e)
    {

    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebMaster_Menu.aspx");
    }

    protected void btnContact_Click(object sender, EventArgs e)
    {

    }

    protected void btnFAQ_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificador.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraPage.aspx");
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }
}