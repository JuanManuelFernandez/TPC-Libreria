<%@ Page Title="Recuperar clave" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="Libreria.Recuperar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS de Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .breadcrumb {
            background-color: transparent;
            padding: 0;
        }

        .section-title {
            font-weight: bold;
        }

        .info-box {
            margin-bottom: 20px;
        }

        .btn-shop {
            width: 100%;
            font-weight: bold;
        }

        .form-control {
            border-radius: 10px;
        }

        .section-header {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        /* Sección del código de verificación */
        .verification-container {
            background-color: white;
            border-radius: 10px;
            padding: 30px;
            text-align: center;
            width: 350px;
            margin: 30px auto;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        .verification-logo {
            font-size: 50px;
            color: #007bff;
            margin-bottom: 20px;
        }

        .verification-header {
            font-size: 22px;
            font-weight: 600;
            color: #333;
            margin-bottom: 15px;
        }

        .verification-email {
            font-weight: 600;
            color: #007bff;
        }

        .verification-code-input {
            display: flex;
            justify-content: space-between;
            margin: 0 30px 20px 30px;
        }

            .verification-code-input input {
                width: 50px;
                height: 50px;
                text-align: center;
                font-size: 24px;
                border-radius: 5px;
                border: 1px solid #ccc;
            }

        .verification-time {
            font-size: 14px;
            color: #888;
            margin-bottom: 20px;
        }

        .resend-link {
            font-size: 14px;
            color: #007bff;
            text-decoration: none;
        }

            .resend-link:hover {
                text-decoration: underline;
            }

        .re-send {
            font-size: 12px;
            color: #888;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item"><a href="Cuenta.aspx">Mi Cuenta</a></li>
                <li class="breadcrumb-item active" aria-current="page">Cambiar Clave</li>
            </ol>
        </nav>

        <div class="row">
            <!-- Cambio de clave -->
            <div class="col-md-8">
                <h1 class="section-header">Cambiar contraseña</h1>
                <div class="info-box">
                    <h4 class="section-title">Vamos a enviarte un email para que puedas cambiar tu contraseña.</h4>

                    <!-- Mail -->
                    <asp:Panel ID="MailPanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblMail" runat="server" AssociatedControlID="txtMail" Text="Mail de cuenta" CssClass="form-label" />
                        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblMensaje" runat="server" Visible="false" />
                    </asp:Panel>

                    <asp:Button ID="btnEnviarMail" runat="server" CssClass="btn btn-primary" Text="Enviar mail" OnClick="BtnEnviarMail_Click" />
                </div>

            </div>

        </div>

        <% 
            if (MailEnviado)
            {
        %>
        <!-- Sección de Verificación de código -->
        <div class="verification-container">
            <!-- Icono del sobre -->
            <div class="verification-logo">📧</div>

            <!-- Título -->
            <h2 class="verification-header">Te enviamos un código a:</h2>

            <!-- Correo electrónico -->
            <p class="verification-email"><%= Session["MailRecuperacion"] %></p>

            <p>Ingresa el Token que recibiste vía mail a continuación</p>

            <!-- Campo de ingreso del código -->
            <div class="mb-3">
                <asp:TextBox ID="txtCodigoVerificacion" runat="server"
                    CssClass="form-control text-center"
                    MaxLength="6"
                    placeholder="000000"
                    Style="font-size: 24px; letter-spacing: 10px; font-weight: bold;"
                    onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
            </div>

            <asp:Button ID="btnEnviarToken" runat="server" CssClass="btn btn-primary" Text="Verificar código" OnClick="BtnEnviarToken_Click" />

            <!-- Tiempo de expiración con temporizador -->
            <p class="verification-time" id="timerDisplay">El código expira en <span id="countdown">05:00</span></p>

            <!-- Botón de reenvío (oculto hasta expirado el timer inicialmente) -->
            <div id="resendContainer" style="display: none;">
                <asp:LinkButton ID="btnReenviarCodigo" runat="server" CssClass="resend-link" OnClick="BtnReenviarCodigo_Click">
            <i class="bi bi-arrow-clockwise"></i> Enviar código nuevamente
                </asp:LinkButton>
            </div>
        </div>

        <script>
            // Función para mover al siguiente campo
            function moveToNext(current, nextFieldID) {
                if (current.value.length >= current.maxLength) {
                    if (nextFieldID) {
                        document.getElementById(nextFieldID).focus();
                    }
                }
            }

            // Temporizador de cuenta regresiva
            (function () {
                // 5 minutos = 300 segundos
                let timeRemaining = 5 * 60;

                const countdownElement = document.getElementById('countdown');
                const timerDisplay = document.getElementById('timerDisplay');
                const resendContainer = document.getElementById('resendContainer');

                function updateTimer() {
                    if (timeRemaining <= 0) {
                        // Tiempo expirado
                        timerDisplay.innerHTML = '<span style="color: #dc3545; font-weight: bold;">El código ha expirado</span>';
                        resendContainer.style.display = 'block';
                        clearInterval(timerInterval);
                        return;
                    }

                    // Calcular minutos y segundos
                    const minutes = Math.floor(timeRemaining / 60);
                    const seconds = timeRemaining % 60;

                    // Formatear con ceros a la izquierda
                    const formattedTime = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
                    countdownElement.textContent = formattedTime;

                    // Cambiar color cuando quede poco tiempo
                    if (timeRemaining <= 60) { // Último minuto
                        countdownElement.style.color = '#dc3545'; // Rojo
                        countdownElement.style.fontWeight = 'bold';
                    } else if (timeRemaining <= 120) { // Últimos 2 minutos
                        countdownElement.style.color = '#ffc107'; // Amarillo
                    }

                    timeRemaining--;
                }

                // Actualizar cada segundo
                updateTimer(); // Llamada inicial
                const timerInterval = setInterval(updateTimer, 1000);
            })();
        </script>
        <%  
            }
        %>
</asp:Content>
