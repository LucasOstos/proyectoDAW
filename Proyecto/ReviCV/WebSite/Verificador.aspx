<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Verificador.aspx.cs" Inherits="Verificador" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recalcular Dígitos Verificadores</title>
    <link href="Estilos_css/Verificador.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">
            <h2>Recalcular Digitos Verificadores</h2>

            <asp:Button ID="btnRecalcular" runat="server" Text="Verificar digitos" CssClass="boton-verificar" OnClick="btnRecalcular_Click"/>
            <asp:Label ID="Label1" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>
