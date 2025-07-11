﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdmin_RubrosIdiomas.aspx.cs" Inherits="MenuAdmin_RubrosIdiomas" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>PA - Gestión de Rubros e Idiomas</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <style>
        html, body {
            margin: 0;
            padding: 0;
            height: 100vh;
            font-family: sans-serif;
            overflow-x: hidden;
        }

        .contenedor {
            padding: 0;
            max-width: 100%;
            margin: 0;
            box-sizing: border-box;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .navbar {
            background-color: #2c3e50;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 20px;
            height: 60px;
            color: white;
            box-shadow: 0 2px 8px rgba(0,0,0,0.2);
            width: 100%;
            box-sizing: border-box;
        }

        .navbar .logo {
            font-size: 20px;
            font-weight: bold;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .navbar .menu {
            display: flex;
            gap: 20px;
        }

        .menu-button {
            background: none;
            border: none;
            color: white;
            font-size: 15px;
            cursor: pointer;
            display: flex;
            align-items: center;
            gap: 6px;
            padding: 10px;
            transition: background 0.3s ease;
            text-decoration: none;
        }

        .menu-button:hover {
            background-color: #34495e;
            border-radius: 4px;
        }

        .contenido-principal {
            flex: 1;
            padding: 20px;
            background-color: #f4f4f4;
            display: flex;
            gap: 30px;
            min-height: calc(100vh - 60px);
            box-sizing: border-box;
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
            max-height: calc(100vh - 120px);
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

        @media (max-width: 768px) {
            .navbar {
                flex-direction: column;
                align-items: flex-start;
                height: auto;
                padding: 10px 20px;
            }

            .navbar .menu {
                flex-direction: column;
                width: 100%;
            }

            .menu-button {
                width: 100%;
                justify-content: flex-start;
            }

            .contenido-principal {
                flex-direction: column;
                min-height: calc(100vh - 140px);
            }

            .seccion {
                max-height: none;
            }
        }
    </style>
</head>
<body>
<form runat="server">
    <div class="contenedor">
        <div class="navbar">
            <div class="logo">
                <i class="fa-solid fa-cogs"></i>
                Panel de Administracion
            </div>
            <div class="menu">
                <asp:LinkButton ID="btnInicio" runat="server" OnClick="btnInicio_Click" CssClass="menu-button">
                    <i class="fa fa-home"></i> Inicio
                </asp:LinkButton>
                <asp:LinkButton ID="btnUsuarios" runat="server" OnClick="btnUsuarios_Click" CssClass="menu-button">
                    <i class="fa fa-users"></i> Usuarios
                </asp:LinkButton>
                <asp:LinkButton ID="btnRubrosIdiomas" runat="server" OnClick="btnRubrosIdiomas_Click" CssClass="menu-button">
                    <i class="fa fa-language"></i> Rubros e Idiomas
                </asp:LinkButton>
                <asp:LinkButton ID="btnVolverALanding" runat="server" OnClick="btnVolverALanding_Click" CssClass="menu-button">
                    <i class="fa fa-arrow-left"></i> Volver
                </asp:LinkButton>
                <asp:LinkButton ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" CssClass="menu-button">
                    <i class="fa fa-sign-out-alt"></i> Cerrar Sesión
                </asp:LinkButton>
            </div>
        </div>

        <div class="contenido-principal">
            <div class="seccion">
                <h2>Gestión de Rubros</h2>
                <div class="cuadro-grilla">
                    <asp:HiddenField ID="hfIdRubro" runat="server" />
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
                    <asp:Button ID="btnAgregarRubro" runat="server" Text="Agregar" CssClass="boton boton-bien" OnClick="btnAgregarRubro_Click" />
                    <asp:Button ID="btnModificarRubro" runat="server" Text="Modificar" CssClass="boton boton-principal" OnClick="btnModificarRubro_Click" />
                    <asp:Button ID="btnEliminarRubro" runat="server" Text="Eliminar" CssClass="boton boton-peligro" OnClick="btnEliminarRubro_Click" />
                </div>
            </div>

            <div class="seccion">
                <h2>Gestión de Idiomas</h2>
                <div class="cuadro-grilla">
                    <asp:HiddenField ID="hfIdIdioma" runat="server" />
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
                    <asp:Button ID="btnAgregarIdioma" runat="server" Text="Agregar" CssClass="boton boton-bien" OnClick="btnAgregarIdioma_Click" />
                    <asp:Button ID="btnModificarIdioma" runat="server" Text="Modificar" CssClass="boton boton-principal" OnClick="btnModificarIdioma_Click" />
                    <asp:Button ID="btnEliminarIdioma" runat="server" Text="Eliminar" CssClass="boton boton-peligro" OnClick="btnEliminarIdioma_Click" />
                </div>
            </div>
        </div>
    </div>
</form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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
                const id = celdas[1]?.innerText?.trim() || '';

                if (targetInputId === 'txtDescripcionRubro') {
                    document.getElementById('<%= txtDescripcionRubro.ClientID %>').value = texto;
                document.getElementById('<%= hfIdRubro.ClientID %>').value = id;
            } else if (targetInputId === 'txtDescripcionIdioma') {
                document.getElementById('<%= txtDescripcionIdioma.ClientID %>').value = texto;
                document.getElementById('<%= hfIdIdioma.ClientID %>').value = id;
            }
        });
    });
    });

</script>
</body>
</html>
