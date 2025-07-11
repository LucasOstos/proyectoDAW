﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio de Sesión - ReviCV</title>
    <link href="Estilos_css\Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>LogIn</h2>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="UserName"></asp:Label>
                <asp:TextBox ID="tbNombreUsuario" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="tbContraseña" runat="server" TextMode="Password" CssClass="aspNetTextBox"></asp:TextBox>
            </div>

            <div class="form-buttons">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                <asp:Button ID="btnSignUp" runat="server" Text="Sign up" OnClick="btnSignUp_Click" />
            </div>

            <asp:Label ID="labelErrores" runat="server" CssClass="error-label" Visible="true"></asp:Label>
        </div>
    </form>
</body>
</html>












