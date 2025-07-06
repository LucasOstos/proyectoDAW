<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackUp_ReStore.aspx.cs" Inherits="BackUp_ReStore" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>BackUp / Restore</title>

    <!-- Font Awesome para íconos -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <link href="Estilos_css\BR.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method ="post">
        <div class="container">
            <h1><i class="fa-solid fa-shield-halved"></i> Gestión de BackUp y Restore</h1>

            <!-- Sección BackUp -->
            <div class="card">
                <h2><i class="fa-solid fa-database"></i> Crear BackUp</h2>
                <asp:Button ID="Button1" runat="server" Text="Realizar BackUp" CssClass="btn primary" OnClick="Button1_Click" />
                 <asp:Label ID="LblConfirmacionBackup" runat="server" Text="" ForeColor="Green" CssClass="confirm-label" />           
            </div>

            <!-- Sección Restore -->
            <div class="card">
                <h2><i class="fa-solid fa-upload"></i> Restaurar Backup</h2>
                <asp:Label ID="Titulo0" runat="server" AssociatedControlID="TextBox2"
                    Text="Archivo: "></asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="TextBox2" runat="server" placeholder=""></asp:TextBox>
                    <asp:Button ID="BtnBuscarRestore" runat="server" CssClass="icon-button" OnClick="BtnBuscarRestore_Click" ToolTip="Buscar archivo" Text="..." />
                    <input type="file" id="fileUpload2" name="fileUpload" style="display: none;" />
                </div>
                <asp:Button ID="Button2" runat="server" Text="Realizar Restore" CssClass="btn primary" OnClick="Button2_Click" />
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
