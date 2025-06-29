<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvaluarCV.aspx.cs" Inherits="EvaluarCV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        body {
            margin: 0;
            font-family: Segoe UI, sans-serif;
            background: linear-gradient(to bottom right, #cceeff, #a0dcff);
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: hidden; /* Esconde las barras */
            font-size: 1.1rem; 
        }

        .contenedor {
            display: flex;
            gap: 66px;
            padding: 44px; 
            align-items: center;
            max-width: 1430px; 
        }

        .imagen-cv {
            background-color: white;
            border: 1px solid #ccc;
            width: 500.5px; 
            height: 715px; 
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden;
            box-shadow: 0 4.4px 13.2px rgba(0,0,0,0.1); 
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
            max-width: 660px; 
            color: #005f9e;
            display: flex;
            flex-direction: column;
            justify-content: center;
            height: 100%;
        }

            .seccion-resena h2 {
                font-size: 37.4px; 
                margin-bottom: 33px; 
                text-align: center;
            }

        .categoria {
            display: flex;
            align-items: center;
            margin-bottom: 5.5px;
        }

            .categoria span {
                font-weight: bold;
                width: 154px; 
                font-size: 22px; 
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
            font-size: 39.6px; 
            color: #666;
            margin: 0px 0px 5.5px 0px;
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
            margin-top: 44px; 
        }

            .seccion-comentario p {
                text-align: center;
                margin-bottom: 22px; 
            }

            .seccion-comentario textarea {
                width: 100%;
                height: 132px; 
                border: none;
                border-radius: 22px; 
                padding: 19.8px;
                font-size: 18.7px; 
                resize: none;
                font-family: inherit;
                box-shadow: 0 2.2px 8.8px rgba(0,0,0,0.1); 
                box-sizing: border-box;
            }

        .boton-enviar {
            margin-top: 27.5px; 
            background-color: #5ec9ff;
            color: white;
            border: none;
            border-radius: 22px; 
            padding: 19.8px 38.5px; 
            font-size: 22px; 
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

        .auto-style1 {
            height: 68px;
            border-radius: 50%;
            cursor: pointer;
            box-shadow: 0 0 14px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s ease;
        }
    </style>

</head>
<body>
     <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="user-icon">
            <asp:ImageButton ID="imgUserIcon" runat="server" ImageUrl="Imagenes/userIcon.png" OnClick="imgUserIcon_Click" CssClass="auto-style1" />
        </div>

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
                            <input value="5" name="contenido" id="contenido5" type="radio" />
                            <label for="contenido5"></label>
                            <input value="4" name="contenido" id="contenido4" type="radio" />
                            <label for="contenido4"></label>
                            <input value="3" name="contenido" id="contenido3" type="radio" />
                            <label for="contenido3"></label>
                            <input value="2" name="contenido" id="contenido2" type="radio" />
                            <label for="contenido2"></label>
                            <input value="1" name="contenido" id="contenido1" type="radio" />
                            <label for="contenido1"></label>
                        </div>
                    </div>

                    <div class="categoria">
                        <span>Diseño</span>
                        <div class="calificacion">
                            <input value="5" name="diseno" id="diseno5" type="radio" />
                            <label for="diseno5"></label>
                            <input value="4" name="diseno" id="diseno4" type="radio" />
                            <label for="diseno4"></label>
                            <input value="3" name="diseno" id="diseno3" type="radio" />
                            <label for="diseno3"></label>
                            <input value="2" name="diseno" id="diseno2" type="radio" />
                            <label for="diseno2"></label>
                            <input value="1" name="diseno" id="diseno1" type="radio" />
                            <label for="diseno1"></label>
                        </div>
                    </div>

                    <div class="categoria">
                        <span>Claridad</span>
                        <div class="calificacion">
                            <input value="5" name="claridad" id="claridad5" type="radio" />
                            <label for="claridad5"></label>
                            <input value="4" name="claridad" id="claridad4" type="radio" />
                            <label for="claridad4"></label>
                            <input value="3" name="claridad" id="claridad3" type="radio" />
                            <label for="claridad3"></label>
                            <input value="2" name="claridad" id="claridad2" type="radio" />
                            <label for="claridad2"></label>
                            <input value="1" name="claridad" id="claridad1" type="radio" />
                            <label for="claridad1"></label>
                        </div>
                    </div>

                    <div class="categoria">
                        <span>Relevancia</span>
                        <div class="calificacion">
                            <input value="5" name="relevancia" id="relevancia5" type="radio" />
                            <label for="relevancia5"></label>
                            <input value="4" name="relevancia" id="relevancia4" type="radio" />
                            <label for="relevancia4"></label>
                            <input value="3" name="relevancia" id="relevancia3" type="radio" />
                            <label for="relevancia3"></label>
                            <input value="2" name="relevancia" id="relevancia2" type="radio" />
                            <label for="relevancia2"></label>
                            <input value="1" name="relevancia" id="relevancia1" type="radio" />
                            <label for="relevancia1"></label>
                        </div>
                    </div>
                </div>

                <div class="seccion-comentario">
                    <p id="pComentario" runat="server"></p>

                    <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" CssClass="comentarios-textarea"/>

                    <br />
                    <asp:Button ID="enviar" class="boton-enviar" runat="server" Text="¡Enviá tu reseña!" OnClick="enviar_Click" />
                </div>
            </div>
        </div>
    </form>

<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</body>
</html>
