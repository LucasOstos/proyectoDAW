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
            background: url('data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"><circle cx="20" cy="20" r="2" fill="rgba(255,255,255,0.1)"/><circle cx="80" cy="30" r="1.5" fill="rgba(255,255,255,0.1)"/><circle cx="40" cy="70" r="1" fill="rgba(255,255,255,0.1)"/><circle cx="90" cy="80" r="2.5" fill="rgba(255,255,255,0.1)"/></svg>');
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
    <form id="form1">
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

            <!-- Perfil de usuario mejorado -->
            <div class="profile-section">
                <div class="profile-header">
                    <div class="profile-avatar">JG</div>
                    <div class="profile-name">Juan Gomez</div>
                    <div class="profile-username">@UsuarioPrueba123</div>
                </div>

                <div class="profile-content">
                    <div class="success-message" id="successMessage">
                        ✓ Cambios guardados exitosamente
                    </div>

                    <div class="profile-tabs">
                        <button type="button" class="tab-button active" onclick="switchTab('profile')">Perfil</button>
                        <button type="button" class="tab-button" onclick="switchTab('security')">Seguridad</button>
                        <button type="button" class="tab-button" onclick="switchTab('stats')">Estadísticas</button>
                    </div>

                    <!-- Tab de Perfil -->
                    <div id="profile-tab" class="tab-content active">
                        <form id="profileForm">
                            <div class="form-group">
                                <label class="form-label">Nombre de usuario</label>
                                <div class="input-group">
                                    <input type="text" class="form-input" id="username" value="UsuarioPrueba123" disabled>
                                    <span class="input-icon" onclick="toggleEdit('username')">✏️</span>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="form-label">Nombre</label>
                                <div class="input-group">
                                    <input type="text" class="form-input" id="firstName" value="Juan" disabled>
                                    <span class="input-icon" onclick="toggleEdit('firstName')">✏️</span>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="form-label">Apellido</label>
                                <div class="input-group">
                                    <input type="text" class="form-input" id="lastName" value="Gomez" disabled>
                                    <span class="input-icon" onclick="toggleEdit('lastName')">✏️</span>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="form-label">DNI</label>
                                <div class="input-group">
                                    <input type="text" class="form-input" id="dni" value="44554455" disabled>
                                    <span class="input-icon" onclick="toggleEdit('dni')">✏️</span>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="form-label">Email</label>
                                <div class="input-group">
                                    <input type="email" class="form-input" id="email" value="prueba@gmail.com" disabled>
                                    <span class="input-icon" onclick="toggleEdit('email')">✏️</span>
                                </div>
                            </div>

                            <div class="action-buttons">
                                <button type="button" class="btn btn-primary" onclick="saveProfile()">
                                    💾 Guardar Cambios
                                </button>
                                <button type="button" class="btn btn-secondary" onclick="cancelEdit()">
                                    ✖️ Cancelar
                                </button>
                            </div>
                        </form>
                    </div>

                    <!-- Tab de Seguridad -->
                    <div id="security-tab" class="tab-content">
                        <div class="security-info">
                            <h4>🔐 Seguridad de la cuenta</h4>
                            <p>Mantén tu cuenta segura cambiando tu contraseña regularmente y usando una contraseña fuerte.</p>
                        </div>

                        <form id="passwordForm">
                            <div class="form-group">
                                <label class="form-label">Contraseña actual</label>
                                <input type="password" class="form-input" id="currentPassword" placeholder="Ingresa tu contraseña actual">
                            </div>

                            <div class="form-group">
                                <label class="form-label">Nueva contraseña</label>
                                <input type="password" class="form-input" id="newPassword" placeholder="Ingresa tu nueva contraseña">
                            </div>

                            <div class="form-group">
                                <label class="form-label">Confirmar nueva contraseña</label>
                                <input type="password" class="form-input" id="confirmPassword" placeholder="Confirma tu nueva contraseña">
                            </div>

                            <div class="action-buttons">
                                <button type="button" class="btn btn-danger" onclick="changePassword()">
                                    🔑 Cambiar Contraseña
                                </button>
                            </div>
                        </form>
                    </div>

                    <!-- Tab de Estadísticas -->
                    <div id="stats-tab" class="tab-content">
                        <div class="stats-grid">
                            <div class="stat-card">
                                <div class="stat-number">12</div>
                                <div class="stat-label">Currículums</div>
                            </div>
                            <div class="stat-card">
                                <div class="stat-number">847</div>
                                <div class="stat-label">Visualizaciones</div>
                            </div>
                            <div class="stat-card">
                                <div class="stat-number">23</div>
                                <div class="stat-label">Reseñas</div>
                            </div>
                            <div class="stat-card">
                                <div class="stat-number">4.8</div>
                                <div class="stat-label">Calificación</div>
                            </div>
                        </div>

                        <div class="security-info">
                            <h4>📊 Resumen de actividad</h4>
                            <p>Tus currículums han sido visualizados 847 veces este mes, con un promedio de 4.8 estrellas en las reseñas recibidas.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        let editingFields = new Set();

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

        function switchTab(tabName) {
            // Remover clase active de todos los tabs
            document.querySelectorAll('.tab-button').forEach(btn => btn.classList.remove('active'));
            document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));

            // Activar el tab seleccionado
            event.target.classList.add('active');
            document.getElementById(tabName + '-tab').classList.add('active');
        }

        function toggleEdit(fieldId) {
            const input = document.getElementById(fieldId);
            const section = document.querySelector('.profile-section');

            if (input.disabled) {
                input.disabled = false;
                input.focus();
                editingFields.add(fieldId);
                section.classList.add('edit-mode');
            }
        }

        function cancelEdit() {
            // Restaurar valores originales y deshabilitar campos
            document.getElementById('username').value = 'UsuarioPrueba123';
            document.getElementById('firstName').value = 'Juan';
            document.getElementById('lastName').value = 'Gomez';
            document.getElementById('dni').value = '44554455';
            document.getElementById('email').value = 'prueba@gmail.com';

            // Deshabilitar todos los campos
            document.querySelectorAll('#profileForm .form-input').forEach(input => {
                input.disabled = true;
            });

            editingFields.clear();
            document.querySelector('.profile-section').classList.remove('edit-mode');
        }

        function saveProfile() {
            // Validar campos editados
            if (editingFields.size === 0) {
                alert('No hay cambios para guardar');
                return;
            }

            // Simular guardado
            setTimeout(() => {
                // Deshabilitar campos editados
                editingFields.forEach(fieldId => {
                    document.getElementById(fieldId).disabled = true;
                });

                // Actualizar avatar y nombre si se cambió
                updateProfileDisplay();

                // Mostrar mensaje de éxito
                const successMsg = document.getElementById('successMessage');
                successMsg.classList.add('show');
                setTimeout(() => successMsg.classList.remove('show'), 3000);

                editingFields.clear();
                document.querySelector('.profile-section').classList.remove('edit-mode');
            }, 500);
        }

        function updateProfileDisplay() {
            const firstName = document.getElementById('firstName').value;
            const lastName = document.getElementById('lastName').value;
            const username = document.getElementById('username').value;

            // Actualizar avatar
            const avatar = document.querySelector('.profile-avatar');
            avatar.textContent = (firstName[0] + lastName[0]).toUpperCase();

            // Actualizar nombre
            document.querySelector('.profile-name').textContent = firstName + ' ' + lastName;
            document.querySelector('.profile-username').textContent = '@' + username;
        }

        function changePassword() {
            const currentPassword = document.getElementById('currentPassword').value;
            const newPassword = document.getElementById('newPassword').value;
            const confirmPassword = document.getElementById('confirmPassword').value;

            if (!currentPassword || !newPassword || !confirmPassword) {
                alert('Por favor, completa todos los campos');
                return;
            }

            if (newPassword !== confirmPassword) {
                alert('Las contraseñas no coinciden');
                return;
            }

            if (newPassword.length < 6) {
                alert('La contraseña debe tener al menos 6 caracteres');
                return;
            }

            // Simular cambio de contraseña
            setTimeout(() => {
                alert('Contraseña cambiada exitosamente');
                document.getElementById('passwordForm').reset();
            }, 500);
        }

        // Permitir edición con Enter
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Enter' && e.target.classList.contains('form-input') && !e.target.disabled) {
                saveProfile();
            }
        });
    </script>
</body>
</html>