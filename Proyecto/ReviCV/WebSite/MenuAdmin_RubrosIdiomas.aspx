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

        .contenedor {
            display: flex;
            height: 100vh;
        }

        .barra-lateral {
            width: 250px;
            background-color: #2c3e50;
            padding-top: 20px;
        }

        .barra-lateral .boton-enlace {
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

        .barra-lateral .boton-enlace:hover {
            background-color: #34495e;
        }

        .contenido-principal {
            flex: 1;
            padding: 30px;
            background-color: #f4f4f4;
            display: flex;
            gap: 30px;
        }

        .seccion {
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

        .cuadro-grilla {
            flex-grow: 1;
            overflow-y: auto;
            border: 1px solid #ccc;
            background: white;
            padding: 10px;
        }

        .grilla-estilo td, .grilla-estilo th {
            padding: 6px;
        }

        .formulario-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .boton {
            padding: 8px 12px;
            border: none;
            border-radius: 4px;
            color: white;
            cursor: pointer;
            margin-right: 10px;
        }

        .boton-bien { background-color: #27ae60; }
        .boton-principal { background-color: #2980b9; }
        .boton-peligro { background-color: #c0392b; }
        .boton-secundario { background-color: #7f8c8d; }
        
        .grupo-botones {
            display: flex;
            justify-content: flex-start;
            gap: 10px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="contenedor">
            <div class="barra-lateral">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="boton-enlace">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="boton-enlace">Usuarios</asp:LinkButton>
                <asp:LinkButton ID="btnRubrosIdiomas" runat="server" OnClick="btnRubrosIdiomas_Click" CssClass="boton-enlace">Rubros e Idiomas</asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="boton-enlace">Volver a búsqueda de CVs</asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="boton-enlace">Cerrar Sesión</asp:LinkButton>
            </div>

            <div class="contenido-principal">
                <!-- Sección Rubros -->
                <div class="seccion" id="sectRubros">
                    <h2>Gestión de Rubros</h2>

                    <div class="cuadro-grilla">
                        <asp:GridView ID="gvRubros" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="grilla-estilo" >
                            <Columns>
                                <asp:BoundField DataField="IdRubro" HeaderText="ID" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <h3>Alta / Modificación</h3>
                    <asp:TextBox ID="txtIdRubro" runat="server" CssClass="formulario-control" placeholder="ID (solo modificar)" ReadOnly="true" />
                    <asp:TextBox ID="txtDescripcionRubro" runat="server" CssClass="formulario-control" placeholder="Descripción" />

                    <div class="grupo-botones">
                        <asp:Button ID="btnAgregarRubro" runat="server" Text="Agregar" CssClass="boton boton-bien" />
                        <asp:Button ID="btnModificarRubro" runat="server" Text="Modificar" CssClass="boton boton-principal" />
                        <asp:Button ID="btnEliminarRubro" runat="server" Text="Eliminar" CssClass="boton boton-peligro" />
                    </div>
                </div>

                <!-- Sección Idiomas -->
                <div class="seccion" id="sectIdiomas">
                    <h2>Gestión de Idiomas</h2>

                    <div class="cuadro-grilla">
                        <asp:GridView ID="gvIdiomas" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="grilla-estilo" >
                            <Columns>
                                <asp:BoundField DataField="IdIdioma" HeaderText="ID" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <h3>Alta / Modificación</h3>
                    <asp:TextBox ID="txtIdIdioma" runat="server" CssClass="formulario-control" placeholder="ID (solo modificar)" ReadOnly="true" />
                    <asp:TextBox ID="txtDescripcionIdioma" runat="server" CssClass="formulario-control" placeholder="Descripción" />

                    <div class="grupo-botones">
                        <asp:Button ID="btnAgregarIdioma" runat="server" Text="Agregar" CssClass="boton boton-bien" />
                        <asp:Button ID="btnModificarIdioma" runat="server" Text="Modificar" CssClass="boton boton-principal" />
                        <asp:Button ID="btnEliminarIdioma" runat="server" Text="Eliminar" CssClass="boton boton-peligro" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
