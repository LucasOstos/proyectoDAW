<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PanelUsuario.aspx.cs" Inherits="PanelUsuario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Panel Usuario</title>
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
            min-height:80px;
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
        }

        .review-btn {
            background-color: white;
            padding: 10px 20px;
            border-radius: 25px;
            border: none;
            cursor: pointer;
            font-weight: bold;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .add-curriculum {
            display: flex;
            align-items: center;
            justify-content: center;
            border: 4px solid white;
            border-radius: 30px;
            height: 80px;
            min-height:80px;
            cursor: pointer;
            color: orange;
            font-size: 32px;
            font-weight: bold;
        }

        .profile-section {
            width: 400px;
            min-width: 300px;
            background-color: #ececec;
            padding: 30px;
            display: flex;
            flex-direction: column;
            margin: 20px;
            border-radius:40px;
        }

        .profile-section h2 {
            margin-top: 0;
        }

        .profile-section p {
            margin: 10px 0;
            font-size: 16px;
        }

        .profile-buttons {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin-top: 30px;
        }

        .profile-buttons button {
            flex: 1;
            min-width: 120px;
            padding: 10px 20px;
            background-color: #a3ffff;
            border: none;
            border-radius: 8px;
            font-weight: bold;
            cursor: pointer;
        }

        .scrollbar-hidden::-webkit-scrollbar {
            display: none;
        }
        .profile-btn {
    min-width: 120px;
    padding: 10px 20px;
    background-color: #a3ffff;
    border: none;
    border-radius: 8px;
    font-weight: bold;
    cursor: pointer;
    margin: 5px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Lista de currículums -->
            <div class="curriculums scrollbar-hidden">
                <div class="curriculum-item">
                    <span class="curriculum-title">Curriculum de medicina - Español</span>
                    <span class="delete-btn" onclick="eliminarCurriculum(this)">✖</span>
                    <button class="review-btn" onclick="verResenas()">Ver reseñas</button>
                </div>
                <div class="curriculum-item">
                    <span class="curriculum-title">Curriculum de medicina - Inglés</span>
                    <span class="delete-btn" onclick="eliminarCurriculum(this)">✖</span>
                    <button class="review-btn" onclick="verResenas()">Ver reseñas</button>
                </div>
                <div class="curriculum-item">
                    <span class="curriculum-title">Curriculum para Globant</span>
                    <span class="delete-btn" onclick="eliminarCurriculum(this)">✖</span>
                    <button class="review-btn" onclick="verResenas()">Ver reseñas</button>
                </div>

                <!-- Botón Agregar -->
                <div class="add-curriculum" onclick="subirCurriculum()">+</div>
            </div>

            <!-- Perfil de usuario a la derecha -->
            <div class="profile-section">
                <div>
                    <h2>Información del perfil</h2>
                    <p><strong>Nombre de usuario:</strong> UsuarioPrueba123</p>
                    <p><strong>Nombre:</strong> Juan</p>
                    <p><strong>Apellido:</strong> Gomez</p>
                    <p><strong>DNI:</strong> 44554455</p>
                    <p><strong>Email:</strong> prueba@gmail.com</p>
                </div>

                <div class="profile-buttons">
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="profile-btn" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="profile-btn" OnClick="btnCancelar_Click" />
    <asp:Button ID="btnCambiarPass" runat="server" Text="Cambiar Contraseña" CssClass="profile-btn" OnClick="btnCambiarPass_Click" />
</div>
                <div style="text-align: center; margin-top: auto;">
    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="profile-btn" OnClick="btnCerrarSesion_Click" />
</div>
            </div>
        </div>
    </form>

    <script>
        function eliminarCurriculum(element) {
            if (confirm("¿Estás seguro que deseas eliminar este currículum?")) {
                const item = element.closest('.curriculum-item');
                item.remove();
            }
        }

        function verResenas() {
            window.location.href = 'Resenas.aspx';
        }

        function subirCurriculum() {
            window.location.href = 'SubirCurriculum.aspx';
        }
    </script>
</body>
</html>