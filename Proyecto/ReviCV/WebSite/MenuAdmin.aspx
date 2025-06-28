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

        .contenedor {
            display: flex;
            height: 100vh;
        }

        .barra-lateral {
            width: 250px;
            background-color: #2c3e50;
            padding-top: 20px;
        }

        .barra-lateral .boton-enlace {
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

        .barra-lateral .boton-enlace:hover {
            background-color: #34495e;
        }

        .contenido-principal {
            flex-grow: 1;
            padding: 30px;
            background-color: #f4f4f4;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="contenedor">
            <div class="barra-lateral">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="boton-enlace">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="boton-enlace">Usuarios</asp:LinkButton>
                <asp:LinkButton ID="btnRubrosIdiomas" runat="server" CssClass ="boton-enlace" OnClick="btnRubrosIdiomas_Click">Rubros e Idiomas</asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="boton-enlace">Volver a búsqueda de CVs</asp:LinkButton>
                <asp:LinkButton ID="btnBitacora" runat="server" CssClass="boton-enlace" OnClick="btnBitacora_Click">Bitácora</asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="boton-enlace">Cerrar Sesión</asp:LinkButton>
            </div>
            <div class="contenido-principal">
                <h2>Bienvenido al panel de administración</h2>
                <p>Seleccioná una opción del menú para comenzar.</p>
            </div>
        </div>
    </form>
</body>
</html>
