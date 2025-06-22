<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackUp_ReStore.aspx.cs" Inherits="BackUp_ReStore" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>BackUp / Restore</title>

    <!-- Font Awesome para íconos -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <link href="BR.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1><i class="fa-solid fa-shield-halved"></i> Gestión de BackUp y Restore</h1>

            <!-- Sección BackUp -->
            <div class="card">
                <h2><i class="fa-solid fa-database"></i> Crear BackUp</h2>
                <asp:Label ID="Label1" runat="server" AssociatedControlID="TextBox1"
                    Text="Ruta donde guardar el archivo:"></asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Ej: C:\Backups\backup.bak"></asp:TextBox>
                    <asp:Button ID="BtnBuscarBackup" runat="server" CssClass="icon-button" OnClick="BtnBuscarBackup_Click" ToolTip="Buscar archivo" Text="..." />
                </div>
                <asp:Button ID="Button1" runat="server" Text="Realizar BackUp" CssClass="btn primary" OnClick="Button1_Click" />
            </div>

            <!-- Sección Restore -->
            <div class="card">
                <h2><i class="fa-solid fa-upload"></i> Restaurar Backup</h2>
                <asp:Label ID="Titulo0" runat="server" AssociatedControlID="TextBox2"
                    Text="Ruta del archivo .bak a restaurar:"></asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Ej: C:\Backups\backup.bak"></asp:TextBox>
                    <asp:Button ID="BtnBuscarRestore" runat="server" CssClass="icon-button" OnClick="BtnBuscarRestore_Click" ToolTip="Buscar archivo" Text="..." />
                </div>
                <asp:Button ID="Button2" runat="server" Text="Realizar Restore" CssClass="btn primary" OnClick="Button2_Click" />
            </div>
        </div>
    </form>
</body>
</html>
