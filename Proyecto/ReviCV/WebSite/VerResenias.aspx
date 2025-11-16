<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerResenias.aspx.cs" Inherits="VerResenias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ReviCV - Opiniones de tu CV</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Segoe UI, sans-serif;
            background: linear-gradient(to bottom right, #cceeff, #a0dcff);
            height: 100vh;
            overflow: hidden;
            font-size: 1rem;
        }

        .contenedor-principal {
            display: flex;
            gap: 50px;
            height: 100vh;
            padding: 40px 50px;
            align-items: center;
        }

        /* CURRICULUM - Columna izquierda (verde) */
        .columna-cv {
            flex: 0 0 42%;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 75vh;
            margin-left: 65px;
        }

        .imagen-cv {
            height: 800px; /* altura fija para todos los CVs */
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden;
            background-color: white;
            border: 1px solid #ccc;
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        }

            .imagen-cv img,
            .imagen-cv iframe {
                height: 100%; /* ocupa todo el alto del contenedor */
                width: auto; /* ancho proporcional */
                object-fit: contain; /* mantiene proporción */
            }

            .imagen-cv embed,
            .imagen-cv object {
                width: 100%;
                height: 100%;
                pointer-events: none;
            }

        /* SECTOR DONDE EL SCROLLBAR FUNCIONA - Columna derecha (morado) */
        .columna-opiniones-wrapper {
            flex: 1;
            display: flex;
            flex-direction: column;
            height: 85vh;
            margin-right: 85px;
        }

        /* Contenedor scrolleable de opiniones (naranja) */
        .columna-opiniones {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 20px;
            overflow-y: scroll;
            overflow-x: hidden;
            padding-right: 15px;
        }

            /* Estilos del scrollbar */
            .columna-opiniones::-webkit-scrollbar {
                width: 12px;
            }

            .columna-opiniones::-webkit-scrollbar-track {
                background: rgba(255, 255, 255, 0.5);
                border-radius: 10px;
            }

            .columna-opiniones::-webkit-scrollbar-thumb {
                background: rgba(94, 184, 229, 0.8);
                border-radius: 10px;
                border: 2px solid rgba(255, 255, 255, 0.5);
            }

                .columna-opiniones::-webkit-scrollbar-thumb:hover {
                    background: rgba(94, 184, 229, 1);
                }

        .columna-opiniones {
            scrollbar-width: auto;
            scrollbar-color: rgba(94, 184, 229, 0.8) rgba(255, 255, 255, 0.5);
        }

        .tarjeta-opinion {
            background-color: white;
            border-radius: 12px;
            padding: 18px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            display: flex;
            gap: 15px;
            position: relative;
            transition: transform 0.2s, box-shadow 0.2s;
            flex-shrink: 0;
            margin-bottom: 5px;
        }

            .tarjeta-opinion:hover {
                transform: translateY(-2px);
                box-shadow: 0 4px 16px rgba(0,0,0,0.15);
            }

        .contenido-opinion {
            flex: 1;
            margin-left: 10px;
            margin-top: 5px;
        }

        .header-opinion {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 12px;
        }

        .foto-usuario {
            width: 45px;
            height: 45px;
            border-radius: 50%;
            object-fit: cover;
            border: 2px solid #5eb8e5;
        }

        .nombre-usuario {
            font-size: 32px;
            font-weight: 600;
            color: #333;
        }

        .calificaciones-grid {
            display: grid;
            grid-template-columns: auto 1fr;
            gap: 6px 12px;
            margin-bottom: 12px;
        }

        .categoria-label {
            color: #5eb8e5;
            font-weight: 600;
            font-size: 29px;
        }

        .estrellas-display {
            display: flex;
            gap: 1px;
        }

        .estrella {
            color: #ffa723;
            font-size: 29px;
        }

            .estrella.vacia {
                color: #ddd;
            }

        .comentario-texto {
            color: #666;
            line-height: 1.5;
            font-size: 18px;
            margin-top: 8px;
            background-color: #f8f9fa;
            border-radius: 8px;
        }

        .botones-reaccion {
            position: absolute;
            top: 16px;
            right: 16px;
            display: flex;
            gap: 8px;
        }

        .btn-reaccion {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            border: 2px solid;
            background-color: white;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 20px;
            transition: all 0.2s;
        }

        .btn-like {
            border-color: #4caf50;
            color: #4caf50;
        }

            .btn-like:hover {
                background-color: #4caf50;
                color: white;
                transform: scale(1.1);
            }

        .btn-dislike {
            border-color: #f44336;
            color: #f44336;
        }

            .btn-dislike:hover {
                background-color: #f44336;
                color: white;
                transform: scale(1.1);
            }

        .user-icon {
            position: fixed;
            top: 15px;
            right: 15px;
            z-index: 1000;
        }

        .user-icon-img {
            width: 60px;
            height: 60px;
            border-radius: 50%;
            cursor: pointer;
            box-shadow: 0 0 14px rgba(0, 0, 0, 0.2);
            transition: transform 0.2s ease;
            background-color: white;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 28px; /* tamaño del ícono */
            color: #333; /* color del ícono */
        }


            .user-icon-img:hover {
                transform: scale(1.1);
            }

        .sin-opiniones {
            text-align: center;
            padding: 60px 20px;
            color: #005f9e;
            font-size: 18px;
            background-color: white;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .btn-like.selected,
        .btn-dislike.selected {
            color: white !important;
            transform: scale(1.1);
        }


        .btn-like.selected {
            background-color: #4caf50 !important;
        }

        .btn-dislike.selected {
            background-color: #f44336 !important;
        }



        @media (max-width: 1200px) {
            .contenedor-principal {
                flex-direction: column;
            }

            .columna-cv {
                flex: none;
                width: 100%;
                height: 50vh;
            }

            .imagen-cv {
                height: 100%;
            }

            .columna-opiniones-wrapper {
                height: 40vh;
                margin-right: 0;
            }

            .botones-reaccion {
                position: relative;
                top: auto;
                right: auto;
                justify-content: center;
                margin-top: 16px;
            }


            .comentario-texto {
                color: #666;
                line-height: 1.5;
                font-size: 16px;
                margin-top: 8px;
                padding: 10px;
                background-color: #f8f9fa;
                border-radius: 8px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="user-icon">
            <asp:LinkButton ID="btnVolver" runat="server" OnClick="imgUserIcon_Click" CssClass="user-icon-img">
        <i class="fa-solid fa-arrow-left"></i>
    </asp:LinkButton>
        </div>


        <div class="contenedor-principal">
            <!-- CURRICULUM - Columna izquierda -->
            <div class="columna-cv">
                <div class="imagen-cv">
                    <asp:Literal ID="VisorCV" runat="server" />
                </div>
            </div>

            <!-- SECTOR DONDE EL SCROLLBAR FUNCIONA - Columna derecha -->
            <div class="columna-opiniones-wrapper">
                <!-- Contenedor scrolleable de opiniones -->
                <div class="columna-opiniones">
                    <asp:Repeater ID="rptOpiniones" runat="server">
                        <ItemTemplate>
                            <div class="tarjeta-opinion">
                                <div class="contenido-opinion">
                                    <div class="header-opinion">
                                        <img src='<%# Eval("FotoUsuario") %>' class="foto-usuario" alt="Foto de usuario" />
                                        <span class="nombre-usuario"><%# Eval("NombreUsuario") %></span>
                                    </div>

                                    <div class="calificaciones-grid">
                                        <span class="categoria-label">Contenido</span>
                                        <div class="estrellas-display">
                                            <%# GenerarEstrellas((int)Eval("Contenido")) %>
                                        </div>

                                        <span class="categoria-label">Diseño</span>
                                        <div class="estrellas-display">
                                            <%# GenerarEstrellas((int)Eval("Diseno")) %>
                                        </div>

                                        <span class="categoria-label">Claridad</span>
                                        <div class="estrellas-display">
                                            <%# GenerarEstrellas((int)Eval("Claridad")) %>
                                        </div>

                                        <span class="categoria-label">Relevancia</span>
                                        <div class="estrellas-display">
                                            <%# GenerarEstrellas((int)Eval("Relevancia")) %>
                                        </div>
                                    </div>

                                    <div class="comentario-texto">
                                        <%# Eval("Comentario") %>
                                    </div>
                                </div>

                                <div class="botones-reaccion">
                                    <button type="button"
                                        class="btn-reaccion btn-like"
                                        data-id="<%# Eval("IdOpinion") %>">
                                        👍</button>

                                    <button type="button"
                                        class="btn-reaccion btn-dislike"
                                        data-id="<%# Eval("IdOpinion") %>">
                                        👎</button>

                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Panel ID="pnlSinOpiniones" runat="server" CssClass="sin-opiniones" Visible="false">
                        <h3>Aún no hay opiniones para este CV</h3>
                        <p>¡Espera a que otros usuarios te encuentren y te califiquen!</p>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".tarjeta-opinion").forEach(card => {

                const btnLike = card.querySelector(".btn-like");
                const btnDislike = card.querySelector(".btn-dislike");

                // Like
                btnLike.addEventListener("click", function (e) {
                    e.preventDefault(); // evita el postback visual
                    btnLike.classList.toggle("selected");
                    if (btnLike.classList.contains("selected")) {
                        btnDislike.classList.remove("selected");
                    }
                });

                // Dislike
                btnDislike.addEventListener("click", function (e) {
                    e.preventDefault();
                    btnDislike.classList.toggle("selected");
                    if (btnDislike.classList.contains("selected")) {
                        btnLike.classList.remove("selected");
                    }
                });

            });
        });
</script>
</body>
</html>
