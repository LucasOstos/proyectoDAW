<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="LandingPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        body, html {
            height: 100%;
            margin: 0;
            overflow: hidden;
            background: radial-gradient(circle at center, #ffffff 0%, #e1f5fe 8%, #b3e5fc 20%, #b3e5fc 100%);
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .rectangle {
            position: absolute;
            top: 50%;
            left: 50%;
            background-color: rgba(21, 101, 192, 0.07);
            border: 1px solid rgba(21, 101, 192, 0.5);
            transform: translate(-50%, -50%);
            opacity: 1;
            animation: growSize 12s linear infinite;
            filter: blur(1px);
        }

        @keyframes growSize {
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

        .rectangle:nth-child(1) {
            animation-delay: -9.6s;
        }

        .rectangle:nth-child(2) {
            animation-delay: -7.2s;
        }

        .rectangle:nth-child(3) {
            animation-delay: -4.8s;
        }

        .rectangle:nth-child(4) {
            animation-delay: -2.4s;
        }

        .rectangle:nth-child(5) {
            animation-delay: 0s;
        }

        .selector {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 14px;
            z-index: 20;
        }

        .button-row {
            position: relative;
            width: 100%;
            max-width: 607px;
            display: flex;
            justify-content: center;
        }

        .glass-button {
            max-width: 464px;
            width: 100%;
            text-align: center;
            padding: 39px 33px;
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(4px);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 27px;
            color: #00bcd4;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 30px;
            font-weight: 600;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 14px 54px rgba(0, 0, 0, 0.1);
        }

            .glass-button:hover {
                background: rgba(176, 255, 243, 0.2);
                border-color: rgba(255, 255, 255, 0.15);
                transform: translateY(-2px);
                box-shadow: 0 20px 68px rgba(0, 0, 0, 0.15);
                color: #0097a7;
            }

            .glass-button:active {
                transform: translateY(0);
                box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            }

        .textbox-container {
            display: flex;
            flex-direction: column;
            gap: 12px;
            width: 100%;
            max-width: 367px;
            background: rgba(255, 255, 255, 0.25);
            border-radius: 34px;
            backdrop-filter: blur(4px);
            box-shadow: 0 7px 34px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            max-height: 0;
            opacity: 0;
            padding: 0 25px;
            transition: max-height 0.4s ease-in-out, opacity 0.6s ease-in-out, padding 0.4s ease-in-out;
        }

            .textbox-container.show {
                max-height: 300px;
                opacity: 1;
                padding: 25px 25px;
            }

            .textbox-container .textbox-input {
                height: 54px;
                font-size: 17px;
                border: none;
                border-radius: 20px;
                padding: 8px 17px;
                background: rgba(255, 255, 255, 0.6);
                color: #555;
                font-weight: 500;
                appearance: none;
                -webkit-appearance: none;
                -moz-appearance: none;
                cursor: pointer;
                outline: none;
                box-sizing: border-box;
                width: 100%;
            }

        .textbox-input option {
            font-size: 24px;
        }

        .burger-button {
            width: 41px;
            height: 30px;
            position: absolute;
            right: -56px;
            top: 50%;
            transform: translateY(-50%);
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            cursor: pointer;
            background: transparent;
            border: none;
            padding: 0;
        }

            .burger-button span {
                display: block;
                height: 7px;
                background-color: #00bcd4;
                border-radius: 2px;
                transition: background-color 0.3s ease;
            }

            .burger-button.clicked span {
                background-color: #007c91;
            }

.user-icon {
    position: fixed;
    top: 15px;
    right: 15px;
    z-index: 1000;
}

.user-icon-img {
    width: 68px;
    height: 68px;
    border-radius: 50%;
    cursor: pointer;
    box-shadow: 0 0 14px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s ease;
}

.user-icon-img:hover {
    transform: scale(1.1);
}

    </style>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Landing Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Ícono de usuario, SIEMPRE en la esquina superior derecha -->
        <div class="user-icon">
            <asp:ImageButton ID="imgUserIcon" runat="server" ImageUrl="Imagenes/userIcon.png" OnClick="imgUserIcon_Click" CssClass="user-icon-img" />
        </div>


        <!-- Contenedor principal -->
        <div class="principal">
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>

            <div class="selector">
                <div class="button-row">
                    <asp:Button ID="EvaluarCVBoton" type="button" runat="server" CssClass="glass-button" Text="¡Evalúa un nuevo talento!" OnClick="EvaluarCVBoton_Click" />
                    <button type="button" class="burger-button" id="burgerBtn" aria-label="Menú burger">
                        <span></span>
                        <span></span>
                        <span></span>
                    </button>
                </div>

                <div class="textbox-container" id="textboxContainer">
                    <asp:DropDownList ID="ddlRubro" runat="server" CssClass="textbox-input" AppendDataBoundItems="true">
                        <asp:ListItem Text="¿Qué rubro queres analizar?" Value="" Enabled="false" Selected="True" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlIdioma" runat="server" CssClass="textbox-input" AppendDataBoundItems="true">
                        <asp:ListItem Text="¿En qué idioma?" Value="" Enabled="false" Selected="True" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlOtraPregunta" runat="server" CssClass="textbox-input" AppendDataBoundItems="true">
                        <asp:ListItem Text="¿Qué otra pregunta metemos acá?" Value="" Enabled="false" Selected="True" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </form>

    <script>
        const burgerBtn = document.getElementById('burgerBtn');
        const textboxContainer = document.getElementById('textboxContainer');

        burgerBtn.addEventListener('click', () => {
            textboxContainer.classList.toggle('show');
            burgerBtn.classList.toggle('clicked');
        });
    </script>
</body>
</html>
