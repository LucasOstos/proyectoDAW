<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraPage.aspx.cs" Inherits="BitacoraPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Estilos_css/BitacoraCSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
                <div class="nav">
            <div class="container">
                <div class="btn-container">
                    <asp:Button ID="btnHome" CssClass="btn" runat="server" Text="Inicio" OnClick="btnHome_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnContact" CssClass="btn" runat="server" Text="Backup/Restore" OnClick="btnContact_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnFAQ" CssClass="btn" runat="server" Text="Digitos Verificadores" OnClick="btnFAQ_Click" />
                    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
                        <rect class="rect" x="0" y="0" width="100" height="40" />
                    </svg>
                </div>
                 <div class="btn-container">
     <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Bitacora" OnClick="Button1_Click" />
     <svg viewBox="0 0 100 40" preserveAspectRatio="none">
         <rect class="rect" x="0" y="0" width="100" height="40" />
     </svg>
 </div>
     <div class="btn-container">
    <asp:Button ID="Button2" CssClass="btn" runat="server" Text="Cerrar Sesion" OnClick="Button2_Click" />
    <svg viewBox="0 0 100 40" preserveAspectRatio="none">
        <rect class="rect" x="0" y="0" width="100" height="40" />
    </svg>
</div>
            </div>
        </div>
        <div class="contenedor">
            <h1>Bitácora del sistema</h1>
        </div>
            <h2>Filtros</h2>
        <div class="filtros-bitacora">
            <div class="filtro">
                <label>Desde: </label>
                <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Hasta:</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <label>Operación:</label>
                <asp:TextBox ID="txtOperacion" runat="server" CssClass="input-filtro"/>
            </div>
            <div class="filtro">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="boton-filtrar"/>
                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="boton-filtrar" OnClick="btnLimpiarFiltros_Click"/>
            </div>
            </div>            
        <div>
            <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" CssClass="tabla-bitacora">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID"/>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}"/>
                    <asp:BoundField DataField="Operacion" HeaderText="Operación"/>
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario"/>
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
