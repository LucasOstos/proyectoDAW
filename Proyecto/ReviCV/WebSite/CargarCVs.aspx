<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargarCVs.aspx.cs" Inherits="CargarCVs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cargar CVs</title>
    <style>
        body {
            transform: scale(1.1);
            margin-top: 80px;
            font-family: Arial, sans-serif;
        }

        .contenedor {
            max-width: 800px;
            margin: auto;
            text-align: center;
        }

        #dropZone {
            width: 100%;
            max-width: 580px;
            height: 200px;
            border: 2px dashed #007bff;
            border-radius: 10px;
            text-align: center;
            line-height: 180px;
            color: #007bff;
            margin: 20px auto;
            transition: background-color 0.3s;
            cursor: pointer;
        }

        #dropZone.dragover {
            background-color: #e3f2fd;
        }

        #fileList {
            margin-top: 20px;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
        <div class="contenedor">
            <asp:DropDownList ID="DropDownList1" runat="server" />

             <div style="margin-bottom: 10px; text-align: left;">
        <label for="ddlRubro" style="font-weight: bold;">Seleccioná un rubro:</label><br />
        <asp:DropDownList ID="ddlRubro" runat="server" Width="250px" />
    </div>

    <!-- DDL Idioma -->
    <div style="margin-bottom: 20px; text-align: left;">
        <label for="ddlIdioma" style="font-weight: bold;">Seleccioná un idioma:</label><br />
        <asp:DropDownList ID="ddlIdioma" runat="server" Width="250px" />
    </div>

            <!-- Zona drag & drop -->
            <div id="dropZone">Arrastrá aquí archivos PDF o imágenes, o hacé clic para seleccionar</div>

            <!-- Input file HTML (oculto visualmente) -->
            <input type="file" id="fileUpload" name="fileUpload" style="display: none;" />

            <!-- Lista de archivos -->
            <div id="fileList"></div>

            <!-- Botón de envío -->
            <asp:Button ID="Button1" runat="server" Text="Subir Archivo" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Confirmacion" runat="server" Text=""></asp:Label>
        </div>

        <!-- SECTOR VISOR Y CONTROLES -->
        <div class="contenedor" style="margin-top: 40px; display: flex; justify-content: center; gap: 30px; align-items: flex-start;">
            <!-- Visor del CV -->
            <div id="visorCV" runat="server" style="
                width: 580px;
                height: 600px;
                border: 2px solid #007bff;
                border-radius: 10px;
                overflow: hidden;
                background-color: #f9f9f9;
                display: flex;
                align-items: center;
                justify-content: center;
            ">
                <asp:Literal ID="LiteralVisorCV" runat="server" />
            </div>

            <!-- Controles -->
            <div style="display: flex; flex-direction: column; align-items: flex-start; gap: 10px;">
                <asp:TextBox ID="TextBoxNumeroCV" runat="server" placeholder="Ingrese número de CV" Width="200px" />
                <asp:Button ID="ButtonMostrarCV" runat="server" Text="Mostrar CV por número" OnClick="ButtonMostrarCV_Click" />
            </div>
        </div>
    </form>

    <script>
        const dropZone = document.getElementById("dropZone");
        const fileInput = document.getElementById("fileUpload");
        const fileList = document.getElementById("fileList");

        // Abrir selector al hacer clic
        dropZone.addEventListener("click", () => {
            fileInput.click();
        });

        // Drag & Drop
        dropZone.addEventListener("dragover", function (e) {
            e.preventDefault();
            dropZone.classList.add("dragover");
        });

        dropZone.addEventListener("dragleave", function (e) {
            e.preventDefault();
            dropZone.classList.remove("dragover");
        });

        dropZone.addEventListener("drop", function (e) {
            e.preventDefault();
            dropZone.classList.remove("dragover");
            const files = e.dataTransfer.files;
            updateFileList(files);
        });

        fileInput.addEventListener("change", function () {
            updateFileList(fileInput.files);
        });

        function updateFileList(files) {
            fileList.innerHTML = "";
            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                if (file.type === "application/pdf" || file.type.startsWith("image/")) {
                    const item = document.createElement("div");
                    item.textContent = file.name;
                    fileList.appendChild(item);
                } else {
                    alert("Solo se permiten archivos PDF o imágenes.");
                }
            }
        }
    </script>
</body>
</html>
