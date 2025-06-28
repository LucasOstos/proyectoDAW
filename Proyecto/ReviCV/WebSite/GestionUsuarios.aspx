<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionUsuarios.aspx.cs" Inherits="GestionUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LabelDNI" runat="server" Text="DNI: "></asp:Label>  <asp:TextBox ID="tbDNI" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelNombre" runat="server" Text="Nombre: "></asp:Label>  <asp:TextBox ID="tbNombre" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelApellido" runat="server" Text="Apellido: "></asp:Label>  <asp:TextBox ID="tbApellido" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelUsername" runat="server" Text="Username: "></asp:Label>  <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelPassword" runat="server" Text="Contraseña: "></asp:Label>  <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelMail" runat="server" Text="Mail "></asp:Label>  <asp:TextBox ID="tbMail" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelRol" runat="server" Text="Rol "></asp:Label>  <asp:TextBox ID="tbRol" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="BotonAceptar" runat="server" Text="Crear Usuario" OnClick="BotonAceptar_Click" />
            <asp:Label ID="Confirmacion" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
