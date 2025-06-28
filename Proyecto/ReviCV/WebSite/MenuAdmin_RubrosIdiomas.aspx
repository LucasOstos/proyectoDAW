<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdmin_RubrosIdiomas.aspx.cs" Inherits="MenuAdmin_RubrosIdiomas" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Gestión de Rubros e Idiomas</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
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

        .boton-enlace {
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

        .boton-enlace:hover {
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

        .cuadro-grilla {
            flex-grow: 1;
            overflow-y: auto;
            border: 1px solid #ccc;
            background: white;
            padding: 10px;
        }

        .grilla-estilo {
            width: 100%;
            border-collapse: collapse;
        }

        .grilla-estilo th {
            background-color: #2c3e50;
            color: white;
            padding: 10px;
        }

        .grilla-estilo td {
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

        .textbox-contenido {
            width: 97%;
            padding: 7px;
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

        .boton-icono {
            background: none;
            border: none;
            cursor: pointer;
            color: #2980b9;
            font-size: 16px;
        }

        .boton-icono:hover {
            color: #1c598a;
        }

        h2, h3 {
            margin-top: 0;
        }
    </style>
</head>
<body>
<form runat="server">
    <div class="contenedor">
        <div class="barra-lateral">
            <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="boton-enlace">Inicio</asp:LinkButton>
            <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="boton-enlace">Usuarios</asp:LinkButton>
            <asp:LinkButton ID="btnRubrosIdiomas" runat="server" CssClass="boton-enlace" OnClick="btnRubrosIdiomas_Click">Rubros e Idiomas</asp:LinkButton>
            <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="boton-enlace">Volver a búsqueda de CVs</asp:LinkButton>
            <asp:LinkButton ID="btnBitacora" runat="server" OnClick="btnBitacora_Click" CssClass="boton-enlace">Bitácora</asp:LinkButton>
            <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="boton-enlace">Cerrar Sesión</asp:LinkButton>
        </div>

        <div class="contenido-principal">
            <div class="seccion">
                <h2>Gestión de Rubros</h2>
                <div class="cuadro-grilla">
                    <asp:GridView ID="gvRubros" runat="server" AutoGenerateColumns="False" CssClass="grilla-estilo" Width="100%">
                        <RowStyle CssClass="fila-normal" />
                        <SelectedRowStyle CssClass="fila-seleccionada" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <button type="button" class="boton-icono select-row" data-target="txtDescripcionRubro">
                                        <i class="fa fa-arrow-right"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id_Rubro" HeaderText="ID" />
                            <asp:BoundField DataField="Rubro" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>

                <h3>Alta / Modificación</h3>
                <asp:TextBox ID="txtDescripcionRubro" runat="server" CssClass="textbox-contenido" placeholder="Rubro" />
                <div class="grupo-botones">
                    <asp:Button ID="btnAgregarRubro" runat="server" Text="Agregar" CssClass="boton boton-bien" />
                    <asp:Button ID="btnModificarRubro" runat="server" Text="Modificar" CssClass="boton boton-principal" />
                    <asp:Button ID="btnEliminarRubro" runat="server" Text="Eliminar" CssClass="boton boton-peligro" />
                </div>
            </div>

            <div class="seccion">
                <h2>Gestión de Idiomas</h2>
                <div class="cuadro-grilla">
                    <asp:GridView ID="gvIdiomas" runat="server" AutoGenerateColumns="False" CssClass="grilla-estilo" Width="100%">
                        <RowStyle CssClass="fila-normal" />
                        <SelectedRowStyle CssClass="fila-seleccionada" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <button type="button" class="boton-icono select-row" data-target="txtDescripcionIdioma">
                                        <i class="fa fa-arrow-right"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id_Idioma" HeaderText="ID" />
                            <asp:BoundField DataField="Idioma" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>

                <h3>Alta / Modificación</h3>
                <asp:TextBox ID="txtDescripcionIdioma" runat="server" CssClass="textbox-contenido" placeholder="Idioma" />
                <div class="grupo-botones">
                    <asp:Button ID="btnAgregarIdioma" runat="server" Text="Agregar" CssClass="boton boton-bien" />
                    <asp:Button ID="btnModificarIdioma" runat="server" Text="Modificar" CssClass="boton boton-principal" />
                    <asp:Button ID="btnEliminarIdioma" runat="server" Text="Eliminar" CssClass="boton boton-peligro" />
                </div>
            </div>
        </div>
    </div>
</form>

<script>
    document.addEventListener('DOMContentLoaded', () => {
        const botones = document.querySelectorAll('.select-row');
        botones.forEach(btn => {
            btn.addEventListener('click', function () {
                const fila = btn.closest('tr');
                const celdas = fila.querySelectorAll('td');
                document.querySelectorAll('.grilla-estilo tr').forEach(f => f.classList.remove('fila-seleccionada'));
                fila.classList.add('fila-seleccionada');
                const targetInputId = btn.getAttribute('data-target');
                const texto = celdas[2]?.innerText?.trim() || '';
                if (targetInputId === 'txtDescripcionRubro') {
                    document.getElementById('<%= txtDescripcionRubro.ClientID %>').value = texto;
                } else if (targetInputId === 'txtDescripcionIdioma') {
                    document.getElementById('<%= txtDescripcionIdioma.ClientID %>').value = texto;
                }
            });
        });
    });
</script>
</body>
</html>
