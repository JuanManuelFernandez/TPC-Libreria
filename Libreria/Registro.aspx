<%@ Page Title="Registro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Libreria.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .password-requirements {
            font-size: 0.875rem;
            color: #6c757d;
            margin-top: 0.5rem;
            text-align: left;
        }

            .password-requirements ul {
                padding-left: 1.5rem;
                margin-bottom: 0;
            }

            .password-requirements li {
                margin-bottom: 0.25rem;
            }

        .requirement-met {
            color: #28a745;
        }

        .requirement-unmet {
            color: #dc3545;
        }

        /* Sección del código de verificación */
        .verification-container {
            background-color: white;
            border-radius: 10px;
            padding: 30px;
            text-align: center;
            max-width: 400px;
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

        .verification-time {
            font-size: 14px;
            color: #888;
            margin-bottom: 20px;
        }

        .resend-link {
            font-size: 14px;
            color: #007bff;
            text-decoration: none;
            cursor: pointer;
        }

            .resend-link:hover {
                text-decoration: underline;
            }

        .alert-custom {
            border-radius: 10px;
            padding: 15px;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">

        <!-- Breadcrumbs -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Registro</li>
            </ol>
        </nav>

        <!-- Formulario centrado -->
        <div class="d-flex align-items-center justify-content-center">
            <div class="text-center mb-4">

                <!-- Panel de Registro -->
                <asp:Panel ID="pnlRegistro" runat="server" Visible="true">
                    <h2 class="mb-4">Crear cuenta</h2>

                    <!-- Nombre -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="NombreTxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="Nombre"
                            required="required"
                            pattern="^[A-Za-zÁ-Úá-ú\s]{2,50}$" />
                    </div>

                    <!-- Apellido -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="ApellidoTxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="Apellido"
                            required="required"
                            pattern="^[A-Za-zÁ-Úá-ú\s]{2,50}$" />
                    </div>

                    <!-- DNI -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="DNITxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="DNI"
                            required="required"
                            pattern="^\d{8}$" />
                    </div>

                    <!-- Email -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="MailTxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="Email"
                            TextMode="Email"
                            required="required"
                            pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                    </div>

                    <!-- Teléfono -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="TelefonoTxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="Teléfono"
                            required="required"
                            pattern="^\d{10,11}$" />
                    </div>

                    <!-- Contraseña -->
                    <div class="mb-3 position-relative">
                        <asp:TextBox ID="ClaveTxt" runat="server" CssClass="form-control form-control-lg"
                            placeholder="Contraseña"
                            TextMode="Password"
                            required="required" />

                        <div class="password-requirements">
                            <small>La contraseña debe tener:</small>
                            <ul>
                                <li id="req-length" class="requirement-unmet">Al menos 8 caracteres</li>
                                <li id="req-upper" class="requirement-unmet">Al menos una letra mayúscula</li>
                                <li id="req-number" class="requirement-unmet">Al menos un número</li>
                            </ul>
                        </div>
                    </div>

                    <div>
                        <asp:Label ID="LblError" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                    </div>

                    <asp:Button ID="BtnEnviarCodigo" runat="server" CssClass="btn btn-primary btn-lg w-100"
                        Text="Continuar" OnClick="BtnEnviarCodigo_Click" />
                </asp:Panel>

            </div>
        </div>

    </div>

    <!-- Panel de Verificación -->
    <% if (CodigoEnviado) { %>

    <div class="verification-container">
        <div class="verification-logo">📧</div>

        <h2 class="verification-header">Te enviamos un código a:</h2>

        <p class="verification-email"><%= Session["RegistroEmail"] %></p>

        <p>Ingresa el código que recibiste vía mail a continuación</p>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-success alert-custom">
            <asp:Label ID="lblMensaje" runat="server" />
        </asp:Panel>

        <div class="mb-3">
            <asp:TextBox ID="txtCodigoVerificacion" runat="server"
                CssClass="form-control text-center"
                MaxLength="6"
                placeholder="000000"
                Style="font-size: 24px; letter-spacing: 10px; font-weight: bold;"
                onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
        </div>

        <asp:Label ID="lblMensajeVerificacion" runat="server" Visible="false" CssClass="mb-3 d-block" />

        <asp:Button ID="btnVerificarCodigo" runat="server" CssClass="btn btn-success w-100 mb-3"
            Text="Verificar y crear cuenta" OnClick="BtnVerificarCodigo_Click" />

        <p class="verification-time" id="timerDisplay">El código expira en <span id="countdown">05:00</span></p>

        <div id="resendContainer" style="display: none;">
            <asp:LinkButton ID="btnReenviarCodigo" runat="server" CssClass="resend-link" OnClick="BtnReenviarCodigo_Click">
                <i class="bi bi-arrow-clockwise"></i> Enviar código nuevamente
            </asp:LinkButton>
        </div>

        <div class="mt-3">
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary btn-sm w-100"
                Text="Cancelar registro" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </div>

    <script>
        // Temporizador de cuenta regresiva
        (function () {
            // 5 minutos = 300 segundos
            let timeRemaining = 5 * 60;

            const countdownElement = document.getElementById('countdown');
            const timerDisplay = document.getElementById('timerDisplay');
            const resendContainer = document.getElementById('resendContainer');

            if (!countdownElement) return; // Si no existe el elemento, salir

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

    <script>
        // Validación en tiempo real de requisitos de contraseña
        document.addEventListener('DOMContentLoaded', function () {
            const passwordInput = document.getElementById('<%= ClaveTxt.ClientID %>');

            if (passwordInput) {
                passwordInput.addEventListener('input', function () {
                    const password = this.value;

                    // Verificar longitud
                    const reqLength = document.getElementById('req-length');
                    if (password.length >= 8) {
                        reqLength.classList.remove('requirement-unmet');
                        reqLength.classList.add('requirement-met');
                    } else {
                        reqLength.classList.remove('requirement-met');
                        reqLength.classList.add('requirement-unmet');
                    }

                    // Verificar mayúscula
                    const reqUpper = document.getElementById('req-upper');
                    if (/[A-Z]/.test(password)) {
                        reqUpper.classList.remove('requirement-unmet');
                        reqUpper.classList.add('requirement-met');
                    } else {
                        reqUpper.classList.remove('requirement-met');
                        reqUpper.classList.add('requirement-unmet');
                    }

                    // Verificar número
                    const reqNumber = document.getElementById('req-number');
                    if (/[0-9]/.test(password)) {
                        reqNumber.classList.remove('requirement-unmet');
                        reqNumber.classList.add('requirement-met');
                    } else {
                        reqNumber.classList.remove('requirement-met');
                        reqNumber.classList.add('requirement-unmet');
                    }
                });
            }
        });
    </script>
</asp:Content>
