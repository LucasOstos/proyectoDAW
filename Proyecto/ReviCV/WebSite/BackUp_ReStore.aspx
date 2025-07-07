<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackUp_ReStore.aspx.cs" Inherits="BackUp_ReStore" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>BackUp / Restore</title>

    <!-- Font Awesome para íconos -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <style>
        /* ----------- Estilos Generales ----------- */
        body, html {
            margin: 0;
            padding: 0;
            font-family: sans-serif;
            background-color: #ecf0f1;
        }


        .contenedor {
            max-width: 300px;
            margin: 50px auto;
            padding: 20px;
        }

        /* ----------- Encabezado Principal ----------- */
        h1 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 40px;
            font-size: 26px;
        }

        /* ----------- Tarjetas ----------- */
        .card {
            background-color: #ffffff;
            padding: 30px;
            border-radius: 14px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            margin-bottom: 35px;
        }

            .card h2 {
                margin-top: 0;
                font-size: 20px;
                color: #333;
                margin-bottom: 20px;
            }

        /* ----------- Labels y Inputs ----------- */
        asp\\:Label, label {
            display: block;
            font-weight: 600;
            margin-bottom: 8px;
            color: #555;
        }

        asp\\:TextBox, input[type="text"] {
            width: 100%;
            padding: 12px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 8px;
            box-sizing: border-box;
        }

            asp\\:TextBox::placeholder {
                color: #aaa;
            }

        /* ----------- Botones de acción ----------- */
        .bttn {
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
            margin-top: 15px;
        }

            .bttn.primary {
                background-color: #3498db;
                color: white;
            }

                .bttn.primary:hover {
                    background-color: #2980b9;
                    box-shadow: 0 0 10px #2980b980;
                }

        /* ----------- Grupo de input + botón ícono ----------- */
        .input-group {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-bottom: 15px;
        }

        /* ----------- Botón con ícono de carpeta ----------- */
        .icon-button {
            background-color: #ecf0f1;
            border: 1px solid #bdc3c7;
            padding: 10px 12px;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.2s ease;
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            font-size: 16px;
            color: #2c3e50;
            height: 42px;
            min-width: 42px;
            text-align: center;
        }

            .icon-button:hover {
                background-color: #d0d3d4;
            }

        /* Icono de carpeta (fa-folder-open) para botones específicos */
        #BtnBuscarBackup::before,
        #BtnBuscarRestore::before {
            content: "\f07c";
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
        }

        /* ----------- Label de Confirmación ----------- */
        .confirm-label {
            display: block;
            margin-top: 10px;
            font-weight: 600;
            color: #27ae60;
            font-size: 14px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
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
                    <asp:Button ID="Button3" CssClass="btn" runat="server" Text="Bitacora" OnClick="Button3_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="Button4" CssClass="btn" runat="server" Text="Cerrar Sesion" OnClick="Button4_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
            </div>
        </div>
        <div class="contenedor">
            <h1><i class="fa-solid fa-shield-halved"></i>Gestión de BackUp y Restore</h1>

            <!-- Sección BackUp -->
            <div class="card">
                <h2><i class="fa-solid fa-database"></i>Crear BackUp</h2>
                <asp:Button ID="Button1" runat="server" Text="Realizar BackUp" CssClass="bttn primary" OnClick="Button1_Click" />
                <asp:Label ID="LblConfirmacionBackup" runat="server" Text="" ForeColor="Green" CssClass="confirm-label" />
            </div>

            <!-- Sección Restore -->
            <div class="card">
                <h2><i class="fa-solid fa-upload"></i>Restaurar Backup</h2>
                <asp:Label ID="Titulo0" runat="server" AssociatedControlID="TextBox2"
                    Text="Archivo: "></asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="TextBox2" runat="server" placeholder=""></asp:TextBox>
                    <asp:Button ID="BtnBuscarRestore" runat="server" CssClass="icon-button" OnClick="BtnBuscarRestore_Click" ToolTip="Buscar archivo" Text="..." />
                    <input type="file" id="fileUpload2" name="fileUpload" style="display: none;" />
                </div>
                <asp:Button ID="Button2" runat="server" Text="Realizar Restore" CssClass="bttn primary" OnClick="Button2_Click" />
                <asp:Label ID="LblConfirmacionRestore" runat="server" Text="" ForeColor="Green" CssClass="confirm-label" />
            </div>

        </div>
    </form>
    <script>
        window.onload = function () {
            const BtnBuscarRestore = document.getElementById("BtnBuscarRestore");
            const fileInput2 = document.getElementById("fileUpload2");
            const textBoxID = '<%= TextBox2.ClientID %>';

            if (BtnBuscarRestore && fileInput2 && textBoxID) {
                BtnBuscarRestore.addEventListener("click", function (e) {
                    e.preventDefault();
                    fileInput2.click();
                });

                fileInput2.addEventListener("change", function () {
                    const fileName = fileInput2.files[0]?.name || "";
                    document.getElementById(textBoxID).value = fileName;
                });
            }
        };
    </script>
</body>
</html>
