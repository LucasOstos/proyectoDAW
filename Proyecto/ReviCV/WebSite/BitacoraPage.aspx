<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraPage.aspx.cs" Inherits="BitacoraPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Estilos_css\BitacoraCSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">
            <h1>Bitácora </h1>
        </div>
        <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" CssClass="table">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Operacion" HeaderText="Operación" />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
