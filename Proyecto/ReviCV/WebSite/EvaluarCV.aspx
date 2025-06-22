<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvaluarCV.aspx.cs" Inherits="EvaluarCV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style>
        body {
            margin: 0;
            font-family: Segoe UI, sans-serif;
            background: linear-gradient(to bottom right, #cceeff, #a0dcff);
            transform: scale(1.08);
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: hidden; /* Esconde las barras */
        }

        .contenedor {
            display: flex;
            gap: 60px;
            padding: 40px;
            align-items: center;
            max-width: 1300px;
        }

        .imagen-cv {
            background-color: white;
            border: 1px solid #ccc;
            width: 455px;
            height: 650px;
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        .imagen-cv img {
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }

        /* Estilos para el contenido del Literal */
        .imagen-cv iframe {
            width: 100%;
            height: 100%;
            border: none;
        }

        .imagen-cv embed {
            width: 100%;
            height: 100%;
        }

        .imagen-cv object {
            width: 100%;
            height: 100%;
        }

        .seccion-resena {
            flex: 1;
            max-width: 600px;
            color: #005f9e;
            display: flex;
            flex-direction: column;
            justify-content: center;
            height: 100%;
        }

        .seccion-resena h2 {
            font-size: 34px;
            margin-bottom: 30px;
            text-align: center;
        }

        .categoria {
            display: flex;
            align-items: center;
            margin-bottom: 5px;
        }

        .categoria span {
            font-weight: bold;
            width: 140px;
            font-size: 20px;
        }

       /* ========================================
           RATING STARS COMPONENT
           Fuente: https://uiverse.io/andrew-demchenk0/clever-elephant-35
           ======================================== */
        .calificacion:not(:checked) > input {
            position: absolute;
            appearance: none;
        }

        .calificacion:not(:checked) > label {
            float: right;
            cursor: pointer;
            font-size: 36px;
            color: #666;
            margin: 0px 0px 5px 0px;
        }

        .calificacion:not(:checked) > label:before {
            content: '★';
        }

        .calificacion > input:checked + label:hover,
        .calificacion > input:checked + label:hover ~ label,
        .calificacion > input:checked ~ label:hover,
        .calificacion > input:checked ~ label:hover ~ label,
        .calificacion > label:hover ~ input:checked ~ label {
            color: #e58e09;
        }

        .calificacion:not(:checked) > label:hover,
        .calificacion:not(:checked) > label:hover ~ label {
            color: #ff9e0b;
        }

        .calificacion > input:checked ~ label {
            color: #ffa723;
        }
        /* ======================================== */

        .seccion-comentario {
            margin-top: 40px;
        }

        .seccion-comentario p {
            text-align: center;
            margin-bottom: 20px;
        }

        .seccion-comentario textarea {
            width: 100%;
            height: 120px;
            border: none;
            border-radius: 20px;
            padding: 18px;
            font-size: 17px;
            resize: none;
            font-family: inherit;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            box-sizing: border-box;
        }

        .boton-enviar {
            margin-top: 25px;
            background-color: #5ec9ff;
            color: white;
            border: none;
            border-radius: 20px;
            padding: 18px 35px;
            font-size: 20px;
            cursor: pointer;
            transition: background-color 0.3s;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }

        .boton-enviar:hover {
            background-color: #44b6ef;
        }

        .calificaciones {
            display: flex;
            flex-direction: column;
            align-items: center;
        }
    </style>

</head>
<body>
    <div class="contenedor">
        <div class="imagen-cv">
            <asp:Literal ID="VisorCV" runat="server" />
        </div>

        <div class="seccion-resena">
            <h2>¿Qué opinas de este CV?</h2>

            <div class="calificaciones">
                <div class="categoria">
                    <span>Contenido</span>
                    <div class="calificacion">
                        <input value="5" name="contenido" id="contenido5" type="radio"/>
                        <label for="contenido5"></label>
                        <input value="4" name="contenido" id="contenido4" type="radio"/>
                        <label for="contenido4"></label>
                        <input value="3" name="contenido" id="contenido3" type="radio"/>
                        <label for="contenido3"></label>
                        <input value="2" name="contenido" id="contenido2" type="radio"/>
                        <label for="contenido2"></label>
                        <input value="1" name="contenido" id="contenido1" type="radio"/>
                        <label for="contenido1"></label>
                    </div>
                </div>

                <div class="categoria">
                    <span>Diseño</span>
                    <div class="calificacion">
                        <input value="5" name="diseno" id="diseno5" type="radio"/>
                        <label for="diseno5"></label>
                        <input value="4" name="diseno" id="diseno4" type="radio"/>
                        <label for="diseno4"></label>
                        <input value="3" name="diseno" id="diseno3" type="radio"/>
                        <label for="diseno3"></label>
                        <input value="2" name="diseno" id="diseno2" type="radio"/>
                        <label for="diseno2"></label>
                        <input value="1" name="diseno" id="diseno1" type="radio"/>
                        <label for="diseno1"></label>
                    </div>
                </div>

                <div class="categoria">
                    <span>Claridad</span>
                    <div class="calificacion">
                        <input value="5" name="claridad" id="claridad5" type="radio"/>
                        <label for="claridad5"></label>
                        <input value="4" name="claridad" id="claridad4" type="radio"/>
                        <label for="claridad4"></label>
                        <input value="3" name="claridad" id="claridad3" type="radio"/>
                        <label for="claridad3"></label>
                        <input value="2" name="claridad" id="claridad2" type="radio"/>
                        <label for="claridad2"></label>
                        <input value="1" name="claridad" id="claridad1" type="radio"/>
                        <label for="claridad1"></label>
                    </div>
                </div>

                <div class="categoria">
                    <span>Relevancia</span>
                    <div class="calificacion">
                        <input value="5" name="relevancia" id="relevancia5" type="radio"/>
                        <label for="relevancia5"></label>
                        <input value="4" name="relevancia" id="relevancia4" type="radio"/>
                        <label for="relevancia4"></label>
                        <input value="3" name="relevancia" id="relevancia3" type="radio"/>
                        <label for="relevancia3"></label>
                        <input value="2" name="relevancia" id="relevancia2" type="radio"/>
                        <label for="relevancia2"></label>
                        <input value="1" name="relevancia" id="relevancia1" type="radio"/>
                        <label for="relevancia1"></label>
                    </div>
                </div>
            </div>

            <div class="seccion-comentario">
                <p>Agrega un comentario adicional para ayudar a <strong>DANIELA</strong>!</p>
                <textarea placeholder="¿Qué le recomendarías a DANIELA?"></textarea>
                <br />
                <button class="boton-enviar">¡Enviá tu reseña!</button>
            </div>
        </div>
    </div>
</body>
</html>