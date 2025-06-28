<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdmin_Usuarios.aspx.cs" Inherits="MenuAdmin_Usuarios" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <title>Gestión de Usuarios</title>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: sans-serif;
        }

        .container {
            display: flex;
            height: 100vh;
        }

        .sidebar {
            width: 250px;
            background-color: #2c3e50;
            padding-top: 20px;
        }
        .btn-icon {
              background: none;
              border: none;
              cursor: pointer;
              color: #2980b9;
              font-size: 16px;

        }
        .btn-icon:hover {
            color: #1c598a;

        }
        .link-button {
            display: block;
            padding: 15px 20px;
            color: white;
            background: none;
            border: none;
            width: 100%;
            text-align: left;
            font: inherit;
            cursor: pointer;
            text-decoration: none;
        }

        .link-button:hover {
            background-color: #34495e;
        }

        .main-content {
    flex: 1;
    padding: 30px;
    background-color: #f4f4f4;
    display: flex;
    flex-direction: column;
    gap: 30px;
    min-width: 0;

        }

        .grid-section {
    height: 100%;
    min-height: 300px;
    overflow-y: auto;
    border: 1px solid #ccc;
    background: white;
    padding: 20px;
    border-radius: 6px;
    box-sizing: border-box;

        }

       .gridview-style {
    width: 100%;
    border-collapse: collapse;
}

.gridview-style th {
    background-color: #2c3e50;
    color: white;
    text-align: left;
    padding: 10px;
}

.gridview-style td {
    padding: 8px;
    border-bottom: 1px solid #ddd;
}
.fila-normal {
    background-color: white;
    transition: background-color 0.3s ease;
}

.fila-seleccionada {
    background-color: #d6eaff !important; 
    font-weight: bold;

}


        .bottom-sections {
            display: flex;
            gap: 20px;
        }

        .abm-section, .filter-section {
            flex: 1;
            background: white;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn {
            padding: 8px 12px;
            border: none;
            border-radius: 4px;
            color: white;
            cursor: pointer;
            margin-right: 10px;
        }

        .btn-success { background-color: #27ae60; }
        .btn-primary { background-color: #2980b9; }
        .btn-danger { background-color: #c0392b; }
        .btn-secondary { background-color: #7f8c8d; }

        h2, h3 {
            margin-top: 0;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="sidebar">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="link-button">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="link-button">Usuarios</asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="link-button">Volver a búsqueda de CVs</asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="link-button">Cerrar Sesión</asp:LinkButton>
            </div>

            <div class="main-content">
                <h2>Listado de Usuarios</h2>
                <div class="grid-section">
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" 
    CssClass="gridview-style" Width="100%" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged">
    <RowStyle CssClass="fila-normal" /> <SelectedRowStyle CssClass="fila-seleccionada" /> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="Select" CssClass="btn-icon" ToolTip="Seleccionar">
                     <i class="fa fa-arrow-right"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="DNI" HeaderText="DNI" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
        <asp:BoundField DataField="NombreUsuario" HeaderText="Username" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Rol" HeaderText="Rol" />
    </Columns>
</asp:GridView>
                </div>

                <div class="bottom-sections">
                    <div class="abm-section">
                        <h3>Creación de usuarios</h3>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="DNI" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" />
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" />
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" />
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Seleccionar rol" Value="" />
                            <asp:ListItem Text="Administrador" Value="Admin" />
                            <asp:ListItem Text="Reclutador" Value="Recruiter" />
                            <asp:ListItem Text="Usuario" Value="Usuario" />
                        </asp:DropDownList>
                        <div>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                        </div>
                    </div>

                    <div class="filter-section">
                        <h3>Filtrar Usuarios</h3>
                        <asp:TextBox ID="txtFiltroDni" runat="server" CssClass="form-control" placeholder="DNI" />
                        <asp:TextBox ID="txtFiltroUsername" runat="server" CssClass="form-control" placeholder="Username" />
                        <asp:TextBox ID="txtFiltroEmail" runat="server" CssClass="form-control" placeholder="Email" />
                        <asp:DropDownList ID="ddlFiltroRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Todos los roles" Value="" />
                            <asp:ListItem Text="Admin" Value="Admin" />
                            <asp:ListItem Text="Recruiter" Value="Recruiter" />
                            <asp:ListItem Text="Usuario" Value="Usuario" />
                        </asp:DropDownList>
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-secondary" OnClick="btnFiltrar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
