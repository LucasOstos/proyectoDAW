<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="LandingPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        body, html {
            background: radial-gradient(circle at center, #ffffff 0%, #e1f5fe 8%, #b3e5fc 20%, #b3e5fc 100%);
            height: 100%;
            margin: 0;
            transform:scale(1.35);
            overflow: hidden; /* Esconde las barras */
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

        /* CONTENEDOR CENTRAL */
        .selector {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 8px;
            z-index: 20;
        }

        /* CONTENEDOR BOTONES (boton principal + burger) */
.button-row {
    position: relative;
    width: 100%;
    max-width: 360px;
    display: flex;
    justify-content: center;
}


        /* BOTÓN PRINCIPAL */
        .glass-button {
            max-width: 275px;
            width: 100%;
            text-align: center;
            transform: translate(0, 0);
            padding: 15px 28px;
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(4px);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 16px;
            color: #00bcd4;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 18px;
            font-weight: 600;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
        }

            .glass-button:hover {
                background: rgba(176, 255, 243, 0.2);
                border-color: rgba(255, 255, 255, 0.15);
                transform: translateY(-2px);
                box-shadow: 0 12px 40px rgba(0, 0, 0, 0.15);
                color: #0097a7;
            }

            .glass-button:active {
                transform: translateY(0);
                box-shadow: 0 6px 24px rgba(0, 0, 0, 0.1);
            }

           /* Textbox container: predeterminado oculto */
.textbox-container {
    display: flex;
    flex-direction: column;
    gap: 7px;
    width: 100%;
    max-width: 233px;
    background: rgba(255, 255, 255, 0.25);
    border-radius: 20px;
    backdrop-filter: blur(4px);
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
    overflow: hidden;

    /* Inicialmente oculto */
    max-height: 0;
    opacity: 0;
    padding: 0 15px;
    transition:
        max-height 0.4s ease-in-out,
        opacity 0.6s ease-in-out,
        padding 0.4s ease-in-out;
}

/* Visible */
.textbox-container.show {
    max-height: 300px;
    opacity: 1;
    padding: 15px 15px;
}


/* Inputs dentro del textbox */
.textbox-container input[type="text"] {
    height: 20px;
    font-size: 10px;
    border: none;
    border-radius: 12px;
    padding: 5px 10px;
    background: rgba(255, 255, 255, 0.6);
    color: #555;
    font-weight: 500;
}

/* Burger visual simple + cambio de color al tocar */
.burger-button {
    width: 24px;
    height: 18px;
    position: absolute;
    right: -32px;
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
    height: 4px;
    background-color: #00bcd4;
    border-radius: 2px;
    transition: background-color 0.3s ease;
}

/* Cambia color al hacer clic */
.burger-button.clicked span {
    background-color: #007c91;
}

.textbox-container input[type="text"]:focus {

}



           
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Landing Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="principal">
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>
            <div class="rectangle"></div>

            <div class="selector">
                <div class="button-row">
                    <button type="button" class="glass-button">¡Evalúa un nuevo talento!</button>
                    <button type="button" class="burger-button" id="burgerBtn" aria-label="Menú burger">
                        <span></span>
                        <span></span>
                        <span></span>
                    </button>
                </div>

                <div class="textbox-container" id="textboxContainer">
                    <input type="text" placeholder="¿Qué rubro queres analizar?" />
                    <input type="text" placeholder="¿En qué idioma?" />
                    <input type="text" placeholder="¿Que otra pregunta metemos acá?" />
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
