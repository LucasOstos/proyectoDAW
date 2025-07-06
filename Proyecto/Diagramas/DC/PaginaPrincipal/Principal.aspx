<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="EstilosMain.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Pelicula" CssClass="clase-boton" OnClick="btnAgregar_Click"/>
            <asp:Button ID="btnListar" runat="server" Text="Ver Peliculas" CssClass="clase-boton" OnClick="btnListar_Click"/>
        </nav>
        <div class="imagen">
            <img src="https://wallpapercave.com/wp/wp4551747.jpg" />
        </div>
    </form>
</body>
</html>
