<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraPage.aspx.cs" Inherits="BitacoraPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bitácora</title>
    <link href="Estilos_css/BitacoraCSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="nav">
            <div class="container">
                <div class="btn-container">
                    <asp:Button ID="btnHome" CssClass="btn" runat="server" Text="Inicio" OnClick="btnHome_Click" data-key="Inicio"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnContact" CssClass="btn" runat="server" Text="Backup/Restore" OnClick="btnContact_Click" data-key="BackupRestore"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnFAQ" CssClass="btn" runat="server" Text="Digitos Verificadores" OnClick="btnFAQ_Click" data-key="DigitosVerificadores"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>

                <div class="btn-container">
                    <asp:Button ID="btnVerPefil" CssClass="btn" runat="server" Text="Ver Perfil" OnClick="btnPerfil_Click"  data-key="VerPerfil"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Bitacora" OnClick="Button1_Click" data-key="Bitacora"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="Button2" CssClass="btn" runat="server" Text="Cerrar Sesion" OnClick="Button2_Click" data-key="CerrarSesion"/>
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
            </div>
        </div>
        <div class="contenedor">
            <h1 runat="server" data-key="BitacoraSistema">Bitácora del sistema</h1>
        </div>
        <h2 runat="server" data-key="Filtros">Filtros</h2>
        <div class="filtros-bitacora">
            <div class="filtro">
                <label runat="server" data-key="Desde">Desde: </label>
                <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="input-filtro" />
            </div>
            <div class="filtro">
                <label runat="server" data-key="Hasta">Hasta:</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="input-filtro" />
            </div>
            <div class="filtro">
                <label runat="server" data-key="Usuario">Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="input-filtro" />
            </div>
            <div class="filtro">
                <label runat="server" data-key="Operacion">Operación:</label>
                <asp:TextBox ID="txtOperacion" runat="server" CssClass="input-filtro" />
            </div>
            <div class="filtro">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="boton-filtrar" data-key="btnFiltrar"/>
            </div>
            <div class="filtro">
                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="boton-filtrar" OnClick="btnLimpiarFiltros_Click" data-key="btnLimpiar"/>
            </div>
        </div>
        <div>
            <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" CssClass="tabla-bitacora">
                <Columns>

    <asp:TemplateField>
        <HeaderTemplate>
            <span runat="server" data-key="ID">ID</span>
        </HeaderTemplate>
        <ItemTemplate>
            <%# Eval("ID") %>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField>
        <HeaderTemplate>
            <span runat="server" data-key="Fecha">Fecha</span>
        </HeaderTemplate>
        <ItemTemplate>
            <%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField>
        <HeaderTemplate>
            <span runat="server" data-key="Operacion">Operación</span>
        </HeaderTemplate>
        <ItemTemplate>
            <%# Eval("Operacion") %>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField>
        <HeaderTemplate>
            <span runat="server" data-key="Usuario">Usuario</span>
        </HeaderTemplate>
        <ItemTemplate>
            <%# Eval("Usuario") %>
        </ItemTemplate>
    </asp:TemplateField>

</Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
