<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3><asp:Label ID="Label1" runat="server" Text="Nombre de Usuario"></asp:Label></h3>
<p>        
    <asp:TextBox ID="tbNombreUsuario" runat="server"></asp:TextBox>
    
</p>
<h3><asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label></h3>
<p>
    <asp:TextBox ID="tbContraseña" runat="server"></asp:TextBox>
    
</p>
<p>
    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />     
    <asp:Button ID="btnLogout" runat="server" Text="Cerrar Sesión" OnClick="btnLogout_Click"/>        

</p>
            <p>
                <asp:Label ID="labelErrores" runat="server" Visible="true"></asp:Label>
            </p>
        </div>
    </form>
    
</body>
</html>
