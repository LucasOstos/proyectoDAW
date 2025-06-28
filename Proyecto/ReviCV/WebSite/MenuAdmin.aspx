<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdmin.aspx.cs" Inherits="MenuAdmin" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Panel de Administración</title>
    <style>
        html, body {
            height: 100%;
            margin: 0;
            font-family: sans-serif;
        }

        .container {
            display: flex;
            height: 100vh;
        }

        .sidebar {
            width: 250px;
            background-color: #2c3e50;
            padding-top: 20px;
        }

        .sidebar .link-button {
            display: block;
            padding: 15px 20px;
            color: white;
            text-decoration: none;
            background: none;
            border: none;
            width: 100%;
            text-align: left;
            font: inherit;
            cursor: pointer;
        }

        .sidebar .link-button:hover {
            background-color: #34495e;
        }

        .main-content {
            flex-grow: 1;
            padding: 30px;
            background-color: #f4f4f4;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="sidebar">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="link-button">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="link-button">Usuarios</asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="link-button">Volver a búsqueda de CVs</asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="link-button">Cerrar Sesión</asp:LinkButton>
            </div>
            <div class="main-content">
                <h2>Bienvenido al panel de administración</h2>
                <p>Seleccioná una opción del menú para comenzar.</p>
            </div>
        </div>
    </form>
</body>
</html>
