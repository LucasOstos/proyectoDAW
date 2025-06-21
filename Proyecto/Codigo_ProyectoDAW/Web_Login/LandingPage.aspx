<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="LandingPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        /*

        */
        body, html {
            background: radial-gradient(circle at center, #ffffff 0%, #e1f5fe 8%, #b3e5fc 20%, #b3e5fc 100%);
            height: 100%;
        }

        .rectangle {
            position: absolute;
            top: 50%;
            left: 50%;
            background-color: rgba(21, 101, 192, 0.07);
            border: 1px solid rgba(21, 101, 192, 0.5);
            transform: translate(-50%, -50%);
            opacity: 1;
             /* Aplica la animación "growSize" durante 12 segundos, repitiéndola infinitamente */
            animation: growSize 12s linear infinite;
            filter: blur(1px);
        }

        
        /* Define la animación: el rectángulo crece y se desvanece */
        @keyframes growSize {
            0% {
                width: 0;
                height: 0;
                opacity: 1;
            }
            100% {
                width: 500px;
                height: 715px;
                opacity: 0;
            }
        }

        /* 
        :nth-child(n) selecciona el hijo número "n" dentro de su contenedor padre.
        En este caso, usamos diferentes delays (retrasos) en la animación 
        para que los elementos no se animen todos al mismo tiempo, 
        sino uno tras otro en forma escalonada.
        */

        .rectangle:nth-child(1) { animation-delay: -9.6s; }
        .rectangle:nth-child(2) { animation-delay: -7.2s; }
        .rectangle:nth-child(3) { animation-delay: -4.8s; }
        .rectangle:nth-child(4) { animation-delay: -2.4s; }
        .rectangle:nth-child(5) { animation-delay: 0s; }

        .glass-button {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            padding: 18px 36px;
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(2px);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 16px;
            color: #00bcd4;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 18px;
            font-weight: 600;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
            cursor: pointer;
            transition: all 0.3s ease;
            z-index: 10;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
        }

        .glass-button:hover {
            background: rgba(176, 255, 243, 0.2);
            border-color: rgba(255, 255, 255, 0.15);
            transform: translate(-50%, -50%) translateY(-2px);
            box-shadow: 0 12px 40px rgba(0, 0, 0, 0.15);
            color: #0097a7;
        }

        .glass-button:active {
            transform: translate(-50%, -50%);
            box-shadow: 0 6px 24px rgba(0, 0, 0, 0.1);
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>

            <button class="glass-button">¡Evalúa un nuevo talento!</button>
        </div>
    </form>
</body>
</html>
