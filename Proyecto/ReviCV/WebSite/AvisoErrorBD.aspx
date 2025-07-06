<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AvisoErrorBD.aspx.cs" Inherits="AvisoErrorBD" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Servicio no disponible</title>
    <style>
        body, html {
            height: 100%;
            margin: 0;
            overflow: hidden;
            background: radial-gradient(circle at center, #ffffff 0%, #e1f5fe 8%, #b3e5fc 20%, #b3e5fc 100%);
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .rectangulo {
            position: absolute;
            top: 50%;
            left: 50%;
            background-color: rgba(21, 101, 192, 0.07);
            border: 1px solid rgba(21, 101, 192, 0.5);
            transform: translate(-50%, -50%);
            opacity: 1;
            animation: crecerTamanio 12s linear infinite;
            filter: blur(1px);
        }

        @keyframes crecerTamanio {
            0% {
                width: 0;
                height: 0;
                opacity: 1;
            }

            100% {
                width: 845px;
                height: 1207px;
                opacity: 0;
            }
        }

.rectangulos .rectangulo:nth-child(1) { animation-delay: -9.6s; }
.rectangulos .rectangulo:nth-child(2) { animation-delay: -7.2s; }
.rectangulos .rectangulo:nth-child(3) { animation-delay: -4.8s; }
.rectangulos .rectangulo:nth-child(4) { animation-delay: -2.4s; }
.rectangulos .rectangulo:nth-child(5) { animation-delay: 0s; }



        .mensaje {
            position: relative;
            z-index: 10;
            max-width: 464px;
            width: 100%;
            padding: 39px 33px;
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(4px);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 27px;
            text-align: center;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
            box-shadow: 0 14px 54px rgba(0, 0, 0, 0.1);
        }

            .mensaje h1 {
                margin-bottom: 30px;
                color: #01579b;
                font-size: 28px;
                margin-bottom: 30px;
            }

        .boton-cerrar-sesion {
            padding: 14px 28px;
            font-size: 18px;
            font-weight: bold;
            color: #ffffff;
            background-color: #00bcd4;
            border: none;
            border-radius: 15px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

            .boton-cerrar-sesion:hover {
                background-color: #0097a7;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="rectangulos">
    <div class="rectangulo"></div>
    <div class="rectangulo"></div>
    <div class="rectangulo"></div>
    <div class="rectangulo"></div>
    <div class="rectangulo"></div>
</div>


        <div class="mensaje">
            <h1>La página web no está disponible actualmente.<br />
                Intente de nuevo más tarde.</h1>
            <asp:Button ID="btnCerrarSesion" runat="server" CssClass="boton-cerrar-sesion" Text="Cerrar sesión" OnClick="btnCerrarSesion_Click" />
        </div>
    </form>
</body>
</html>
