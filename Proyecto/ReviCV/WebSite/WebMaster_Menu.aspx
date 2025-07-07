<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebMaster_Menu.aspx.cs" Inherits="WebMaster_Menu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Panel de WebMaster</title>
    <style>
        body, html {
            margin: 0;
            padding: 0;
            font-family: sans-serif;
            background-color: #ecf0f1;
        }

        .nav {
            width: 100%;
            background: #2c3e50;
        }

        .container {
            display: flex;
            justify-content: space-around;
            align-items: center;
            padding: 0.6em 1em;
            gap: 10px;
        }

        .btn-container {
            position: relative;
            flex: 1;
            text-align: center;
        }

        .btn {
            width: 100%;
            padding: 0.6em 0;
            font-size: 0.95em;
            border: none;
            background: transparent;
            color: #fff;
            cursor: pointer;
            position: relative;
            z-index: 1;
        }

        svg {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            pointer-events: none;
            z-index: 0;
        }

        .rect {
            stroke: #fff;
            fill: transparent;
            stroke-width: 2.5;
            stroke-dasharray: 200;
            stroke-dashoffset: 200;
            transition: stroke-dashoffset 0.4s ease;
        }

        .btn-container:hover .rect {
            stroke-dashoffset: 0;
        }

        .contenido {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 60px);
            text-align: center;
        }

        .centrado h2 {
            font-size: 30px;
            color: #2c3e50;
            margin-bottom: 15px;
        }

        .centrado p {
            font-size: 18px;
            color: #555;
        }

        .contenido h2 {
            color: #2c3e50;
            font-size: 28px;
            margin-bottom: 10px;
        }

        .contenido p {
            color: #555;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="nav">
            <div class="container">
                <div class="btn-container">
                    <asp:Button ID="btnHome" CssClass="btn" runat="server" Text="Inicio" OnClick="btnHome_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnContact" CssClass="btn" runat="server" Text="Backup/Restore" OnClick="btnContact_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnFAQ" CssClass="btn" runat="server" Text="Digitos Verificadores" OnClick="btnFAQ_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>

                <div class="btn-container">
                    <asp:Button ID="btnVerPefil" CssClass="btn" runat="server" Text="Ver Perfil" OnClick="btnPerfil_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Bitacora" OnClick="Button1_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="Button2" CssClass="btn" runat="server" Text="Cerrar Sesion" OnClick="Button2_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="centrado">
                <h2>Bienvenido al panel de Web Master</h2>
                <p>Seleccioná una opción del menú para comenzar</p>
            </div>
        </div>
    </form>
</body>
</html>
