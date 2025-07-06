<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Verificador.aspx.cs" Inherits="Verificador" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recalcular Dígitos Verificadores</title>
    <style>
        body, html {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', sans-serif;
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
            border-style: none;
            border-color: inherit;
            border-width: medium;
            width: 100%;
            padding: 0.6em 0;
            font-size: 0.95em;
            background: transparent;
            color: #fff;
            cursor: pointer;
            position: relative;
            z-index: 1;
            top: 0px;
            left: 1px;
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

        .contenedor {
           position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 90%;
    max-width: 700px;
    background: #fff;
    padding: 30px;
    border-radius: 12px;
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.08);
    text-align: center;
        }

        h2 {
            color: #333;
            margin-bottom: 25px;
        }

        .boton-verificar {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 12px 20px;
            font-size: 16px;
            border-radius: 6px;
            cursor: pointer;
        }

        .boton-verificar:hover {
            background-color: #0056b3;
        }

        .resultado {
            margin-top: 25px;
            text-align: left;
            font-size: 14px;
        }

        .resultado-error {
            color: #dc3545;
        }

        .resultado-warning {
            color: #ffc107;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Menú de navegación -->
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
                    <asp:Button ID="btnAbout" CssClass="btn" runat="server" Text="Digito Verificador" OnClick="btnAbout_Click" />
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

        <!-- Contenido principal -->
        <div class="contenedor">
            <h2>Recalcular Dígitos Verificadores</h2>
            <asp:Button ID="btnRecalcular" runat="server" Text="Verificar dígitos" CssClass="boton-verificar" OnClick="btnRecalcular_Click" />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>

    </form>
</body>
</html>
