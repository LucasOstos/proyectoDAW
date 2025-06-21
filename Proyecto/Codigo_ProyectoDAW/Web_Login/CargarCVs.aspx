<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargarCVs.aspx.cs" Inherits="CargarCVs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cargar CVs</title>
    <style>
        #dropZone {
            width: 100%;
            max-width: 400px;
            height: 200px;
            border: 2px dashed #007bff;
            border-radius: 10px;
            text-align: center;
            line-height: 180px;
            color: #007bff;
            margin: 20px auto;
            font-family: Arial, sans-serif;
            transition: background-color 0.3s;
        }

        #dropZone.dragover {
            background-color: #e3f2fd;
        }

        #fileList {
            text-align: center;
            font-family: Arial, sans-serif;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="DropDownList1" runat="server">

        </asp:DropDownList><div id="dropZone">Arrastra aquí archivos PDF o imágenes</div>
        <div id="fileList"></div>

        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" Style="display:none;" /><asp:Button ID="Button1" runat="server" Text="Subir Archivo" />
    </form>

    <script>
        const dropZone = document.getElementById("dropZone");
        const fileList = document.getElementById("fileList");

        dropZone.addEventListener("dragover", function (e) {
            e.preventDefault();
            e.stopPropagation();
            dropZone.classList.add("dragover");
        });

        dropZone.addEventListener("dragleave", function (e) {
            e.preventDefault();
            e.stopPropagation();
            dropZone.classList.remove("dragover");
        });

        dropZone.addEventListener("drop", function (e) {
            e.preventDefault();
            e.stopPropagation();
            dropZone.classList.remove("dragover");

            const files = e.dataTransfer.files;
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

        // Si querés, también podés asignar los archivos al FileUpload (no completamente funcional sin backend)
            // document.getElementById('<%= FileUpload1.ClientID %>').files = files;
        });
    </script>
</body>
</html>
