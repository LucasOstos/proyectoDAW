<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdmin_RubrosIdiomas.aspx.cs" Inherits="MenuAdmin_RubrosIdiomas" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Gestión de Rubros e Idiomas</title>
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
            gap: 30px;
        }

        .section {
            flex: 1;
            background: white;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 6px;
            display: flex;
            flex-direction: column;
            gap: 20px;
            overflow-y: auto;
            max-height: 90vh;
        }

        h2, h3 {
            margin-top: 0;
        }

        .grid-section {
            flex-grow: 1;
            overflow-y: auto;
            border: 1px solid #ccc;
            background: white;
            padding: 10px;
        }

        .gridview-style td, .gridview-style th {
            padding: 6px;
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
        
        .btn-group {
            display: flex;
            justify-content: flex-start;
            gap: 10px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="sidebar">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="link-button">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="link-button">Usuarios</asp:LinkButton>
                <asp:LinkButton ID="btnRubrosIdiomas" runat="server" OnClick="btnRubrosIdiomas_Click" CssClass="link-button">Rubros e Idiomas</asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="link-button">Volver a búsqueda de CVs</asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="link-button">Cerrar Sesión</asp:LinkButton>
            </div>

            <div class="main-content">
                <!-- Sección Rubros -->
                <div class="section" id="sectRubros">
                    <h2>Gestión de Rubros</h2>

                    <div class="grid-section">
                        <asp:GridView ID="gvRubros" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="gridview-style" >
                            <Columns>
                                <asp:BoundField DataField="IdRubro" HeaderText="ID" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <h3>Alta / Modificación</h3>
                    <asp:TextBox ID="txtIdRubro" runat="server" CssClass="form-control" placeholder="ID (solo modificar)" ReadOnly="true" />
                    <asp:TextBox ID="txtDescripcionRubro" runat="server" CssClass="form-control" placeholder="Descripción" />

                    <div class="btn-group">
                        <asp:Button ID="btnAgregarRubro" runat="server" Text="Agregar" CssClass="btn btn-success" />
                        <asp:Button ID="btnModificarRubro" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                        <asp:Button ID="btnEliminarRubro" runat="server" Text="Eliminar" CssClass="btn btn-danger" />
                    </div>
                </div>

                <!-- Sección Idiomas -->
                <div class="section" id="sectIdiomas">
                    <h2>Gestión de Idiomas</h2>

                    <div class="grid-section">
                        <asp:GridView ID="gvIdiomas" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="gridview-style" >
                            <Columns>
                                <asp:BoundField DataField="IdIdioma" HeaderText="ID" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <h3>Alta / Modificación</h3>
                    <asp:TextBox ID="txtIdIdioma" runat="server" CssClass="form-control" placeholder="ID (solo modificar)" ReadOnly="true" />
                    <asp:TextBox ID="txtDescripcionIdioma" runat="server" CssClass="form-control" placeholder="Descripción" />

                    <div class="btn-group">
                        <asp:Button ID="btnAgregarIdioma" runat="server" Text="Agregar" CssClass="btn btn-success" />
                        <asp:Button ID="btnModificarIdioma" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                        <asp:Button ID="btnEliminarIdioma" runat="server" Text="Eliminar" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
