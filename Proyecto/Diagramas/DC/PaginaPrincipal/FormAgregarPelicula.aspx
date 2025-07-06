<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormAgregarPelicula.aspx.cs" Inherits="FormAgregarPelicula" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="EstilosFormulario.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="barraVertical">
            <ul>
                <li><a href="Principal.aspx"> Inicio</a></li>
                <li><a href="FormPeliculas.aspx"> Ver peliculas</a></li>
            </ul>
        </div>
        <div class="entrada">
            <label>Nombre de la pelicula</label>
            <asp:TextBox ID="tbNombre" runat="server" CssClass="entrada-filtro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="tbNombre" ErrorMessage="No puede dejar el nombre vacío" ValidationGroup="grupoAlta" CssClass="error"/>
        </div>

        <div class="entrada">
            <label>Categoría</label>
            <asp:TextBox ID="tbCategoria" runat="server" CssClass="entrada-filtro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="tbCategoria" ErrorMessage="No puede dejar la categoría vacía" ValidationGroup="grupoAlta" CssClass="error"/>
        </div>

        <div class="entrada">
            <label>Año de lanzamiento</label>
            <asp:TextBox ID="tbAnio" runat="server" CssClass="entrada-filtro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAnio" runat="server" ControlToValidate="tbAnio" ErrorMessage="No puede dejar el año vacío" ValidationGroup="grupoAlta" CssClass="error"/>
        </div>

        <div class="entrada">
            <label>Director</label>
            <asp:TextBox ID="tbDirector" runat="server" CssClass="entrada-filtro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDirector" runat="server" ControlToValidate="tbDirector" ErrorMessage="No puede dejar el director vacío" ValidationGroup="grupoAlta" CssClass="error"/>
        </div>

        <div class="botones">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="boton" OnClick="btnAgregar_Click" ValidationGroup="grupoAlta"/>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="btnCancelar_Click"/>
        </div>
    </form>
</body>
</html>
