<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraPage.aspx.cs" Inherits="BitacoraPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Estilos_css/BitacoraCSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">
            <h1>Bitácora del sistema</h1>
        </div>
            <h2>Filtros</h2>
        <div class="filtros-bitacora">
            <div class="filtro">
                <label>Desde: </label>
                <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Hasta:</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Operación:</label>
                <asp:TextBox ID="txtOperacion" runat="server" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="boton-filtrar"/>
            </div>
            </div>            
        <div>
            <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" CssClass="tabla-bitacora">
                <Columns>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}"/>
                    <asp:BoundField DataField="Operacion" HeaderText="Operación"/>
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario"/>
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
