<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PanelUsuario.aspx.cs" Inherits="PanelUsuario" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <title>Perfil de Usuario</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        html, body {
            height: 100%;
            width: 100%;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(to bottom, #a3d4ff, #b3e0ff);
            overflow-x: hidden;
        }

        #form1 {
            height: 100vh;
            width: 100vw;
        }

        .container {
            display: flex;
            height: 100%;
            width: 100%;
        }

        .curriculums {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 15px;
            padding: 20px;
            overflow-y: auto;
        }

        .curriculum-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background-color: #e8e8e8;
            padding: 15px 20px;
            border-radius: 30px;
            width: 100%;
            height: 80px;
            min-height: 80px;
        }

        .curriculum-title {
            font-weight: bold;
            flex-grow: 1;
            padding-left: 10px;
        }

        .delete-btn {
            font-weight: bold;
            color: red;
            font-size: 20px;
            margin: 0 15px;
            cursor: pointer;
            transition: color 0.3s;
        }

        .delete-btn:hover {
            color: #ff3333;
        }

        .review-btn {
            background-color: white;
            padding: 10px 20px;
            border-radius: 25px;
            border: none;
            cursor: pointer;
            font-weight: bold;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s;
        }

        .review-btn:hover {
            transform: translateY(-2px);
        }

        .add-curriculum {
            display: flex;
            align-items: center;
            justify-content: center;
            border: 4px solid white;
            border-radius: 30px;
            height: 80px;
            min-height: 80px;
            cursor: pointer;
            color: orange;
            font-size: 52px;
            font-weight: bold;
            transition: all 0.3s;
        }

        .add-curriculum:hover {
            background-color: rgba(255, 255, 255, 0.1);
            transform: scale(1.02);
        }

        /* SECCIÓN DE PERFIL MEJORADA */
        .profile-section {
            width: 450px;
            min-width: 400px;
            background: linear-gradient(135deg, #ffffff, #f8f9ff);
            margin: 20px;
            border-radius: 25px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            display: flex;
            flex-direction: column;
        }

        .profile-header {
            background: linear-gradient(135deg, #667eea, #764ba2);
            color: white;
            padding: 30px;
            text-align: center;
            position: relative;
        }

        .profile-header::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
        }

        .profile-avatar {
            width: 80px;
            height: 80px;
            background: linear-gradient(135deg, #ff9a9e, #fecfef);
            border-radius: 50%;
            margin: 0 auto 15px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 32px;
            font-weight: bold;
            color: white;
            text-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
            position: relative;
            z-index: 1;
        }

        .profile-name {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 5px;
            position: relative;
            z-index: 1;
        }

        .profile-username {
            font-size: 14px;
            opacity: 0.9;
            position: relative;
            z-index: 1;
        }

        .profile-content {
            padding: 30px;
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 25px;
        }

        .profile-tabs {
            display: flex;
            background-color: #f0f4f8;
            border-radius: 12px;
            padding: 4px;
            margin-bottom: 20px;
        }

        .tab-button {
            flex: 1;
            padding: 10px 16px;
            border: none;
            background: none;
            border-radius: 8px;
            cursor: pointer;
            font-weight: 500;
            transition: all 0.3s;
            color: #64748b;
        }

        .tab-button.active {
            background: white;
            color: #667eea;
            box-shadow: 0 2px 8px rgba(102, 126, 234, 0.15);
        }

        .tab-content {
            display: none;
        }

        .tab-content.active {
            display: block;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-label {
            display: block;
            margin-bottom: 6px;
            font-weight: 500;
            color: #374151;
            font-size: 14px;
        }

        .form-input {
            width: 100%;
            padding: 12px 16px;
            border: 2px solid #e5e7eb;
            border-radius: 10px;
            font-size: 14px;
            transition: all 0.3s;
            background-color: #ffffff;
        }

        .form-input:focus {
            outline: none;
            border-color: #667eea;
            box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
        }

        .form-input:disabled {
            background-color: #f9fafb;
            color: #6b7280;
            cursor: not-allowed;
        }

        .input-group {
            position: relative;
        }

        .input-icon {
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            color: #9ca3af;
            cursor: pointer;
            transition: color 0.3s;
        }

        .input-icon:hover {
            color: #667eea;
        }

        .btn {
            padding: 12px 24px;
            border: none;
            border-radius: 10px;
            font-weight: 500;
            cursor: pointer;
            transition: all 0.3s;
            font-size: 14px;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }

        .btn-primary {
            background: linear-gradient(135deg, #667eea, #764ba2);
            color: white;
            box-shadow: 0 4px 15px rgba(102, 126, 234, 0.3);
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(102, 126, 234, 0.4);
        }

        .btn-secondary {
            background-color: #f3f4f6;
            color: #374151;
            border: 1px solid #d1d5db;
        }

        .btn-secondary:hover {
            background-color: #e5e7eb;
        }

        .btn-danger {
            background: linear-gradient(135deg, #ef4444, #dc2626);
            color: white;
        }

        .btn-danger:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(239, 68, 68, 0.3);
        }

        .action-buttons {
            display: flex;
            gap: 12px;
            margin-top: 20px;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
            margin-bottom: 20px;
        }

        .stat-card {
            background: linear-gradient(135deg, #f0f9ff, #e0f2fe);
            padding: 20px;
            border-radius: 12px;
            text-align: center;
            border: 1px solid #e0f2fe;
        }

        .stat-number {
            font-size: 24px;
            font-weight: bold;
            color: #0284c7;
            margin-bottom: 5px;
        }

        .stat-label {
            font-size: 12px;
            color: #64748b;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .security-info {
            background: linear-gradient(135deg, #f0fdf4, #dcfce7);
            padding: 20px;
            border-radius: 12px;
            border: 1px solid #bbf7d0;
            margin-bottom: 20px;
        }

        .security-info h4 {
            color: #166534;
            margin-bottom: 10px;
            font-size: 16px;
        }

        .security-info p {
            color: #166534;
            font-size: 14px;
            line-height: 1.5;
        }

        .edit-mode .form-input:not(:disabled) {
            border-color: #667eea;
            background-color: #f8faff;
        }

        .success-message {
            background: linear-gradient(135deg, #dcfce7, #bbf7d0);
            color: #166534;
            padding: 12px 16px;
            border-radius: 8px;
            margin-bottom: 20px;
            font-size: 14px;
            display: none;
        }

        .success-message.show {
            display: block;
            animation: slideIn 0.3s ease-out;
        }

        @keyframes slideIn {
            from {
                opacity: 0;
                transform: translateY(-10px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Lista de currículums -->
            <div class="curriculums">
                <div class="curriculum-item">
                    <span class="curriculum-title">Curriculum de medicina - Español</span>
                    <asp:Button runat="server" CssClass="delete-btn" Text="✖" />
                    <asp:Button runat="server" CssClass="review-btn" Text="Ver reseñas"  />
                </div>

                <!-- Botón Agregar -->
                <asp:Button ID="btnAgregarCurriculum" runat="server" Text="+" CssClass="add-curriculum" />
            </div>

            <!-- Perfil de usuario -->
            <div class="profile-section">
                <div class="profile-header">
                    <div class="profile-avatar" id="avatar">JG</div>
                    <div class="profile-name" id="nombreCompleto">Juan Gomez</div>
                    <div class="profile-username" id="usernameDisplay">@UsuarioPrueba123</div>
                </div>

                <div class="profile-content">
                    <div class="success-message" id="successMessage">
                        ✓ Cambios guardados exitosamente
                    </div>

                    <div class="profile-tabs">
                        <asp:Button ID="btnTabPerfil" runat="server" Text="Perfil" CssClass="tab-button"  />
                        <asp:Button ID="btnTabSeguridad" runat="server" Text="Seguridad" CssClass="tab-button" />
                    </div>

                    <!-- Tab de Perfil -->
                    <div id="profile-tab" class="tab-content">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-input" Enabled="false" />
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-input" Enabled="false" />
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-input" Enabled="false" />
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-input" Enabled="false" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input" TextMode="Email" Enabled="false" />

                        <div class="action-buttons">
                            <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn btn-primary" Text="💾 Guardar Cambios" />
                            <asp:Button ID="btnCancelarEdicion" runat="server" CssClass="btn btn-secondary" Text="✖️ Cancelar"  />
                        </div>
                    </div>

                    <!-- Tab de Seguridad -->
                    <div id="security-tab" class="tab-content" style="display:none;">
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-input" TextMode="Password" Placeholder="Ingresa tu contraseña actual" />
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-input" TextMode="Password" Placeholder="Ingresa tu nueva contraseña" />
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-input" TextMode="Password" Placeholder="Confirma tu nueva contraseña" />

                        <div class="action-buttons">
                            <asp:Button ID="btnCambiarPassword" runat="server" CssClass="btn btn-danger" Text="🔑 Cambiar Contraseña" OnClientClick="return validarPassword();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function validarPassword() {
            const nueva = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            const confirmar = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
            const actual = document.getElementById('<%= txtCurrentPassword.ClientID %>').value;

            if (!nueva || !confirmar || !actual) {
                alert("Por favor, completa todos los campos.");
                return false;
            }

            if (nueva.length < 6) {
                alert("La contraseña debe tener al menos 6 caracteres.");
                return false;
            }

            if (nueva !== confirmar) {
                alert("Las contraseñas no coinciden.");
                return false;
            }

            return true;
        }
    </script>
</body>
</html>