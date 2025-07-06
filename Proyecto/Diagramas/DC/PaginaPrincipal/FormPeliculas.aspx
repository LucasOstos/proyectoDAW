<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPeliculas.aspx.cs" Inherits="FormPeliculas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="EstilosVistaPeliculas.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <h1> Peliculas registradas </h1>
    <form id="form1" runat="server">
        <div class="barraVertical">
            <ul>
                <li><a href="Principal.aspx"> Inicio</a></li>
                <li><a href="FormAgregarPelicula.aspx"> Agregar Pelicula</a></li>
            </ul>
        </div>
        <div class="grilla">
            <asp:GridView ID="gvPeliculas" runat="server"></asp:GridView>
        </div>

        <div class="borrar">
            <asp:DropDownList ID="ddlPeliculas" runat="server"></asp:DropDownList>
            <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="boton" OnClick="btnBorrar_Click"/>
        </div>
    </form>
</body>
</html>
