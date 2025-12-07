<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="Libreria.CambiarClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS de Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" rel="stylesheet">
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

        .form-control {
            border-radius: 10px;
        }

        .section-header {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .password-requirements {
            font-size: 0.875rem;
            color: #6c757d;
            margin-top: 0.5rem;
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

        .alert-custom {
            border-radius: 10px;
            padding: 15px;
            margin-bottom: 20px;
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
                <li class="breadcrumb-item active" aria-current="page">Cambiar Contraseña</li>
            </ol>
        </nav>

        <div class="row justify-content-center">
            <!-- Cambio de contraseña -->
            <div class="col-md-8">
                <h1 class="section-header">Cambiar contraseña</h1>

                <!-- Mensaje de éxito/error -->
                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert-custom">
                    <asp:Label ID="lblMensaje" runat="server" />
                </asp:Panel>

                <div class="info-box">
                    <h4 class="section-title">Nueva contraseña</h4>
                    <p class="text-muted">Por favor, ingresa tu nueva contraseña</p>

                    <!-- Nueva contraseña -->
                    <asp:Panel ID="NuevaClavePanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblNuevaClave" runat="server" AssociatedControlID="txtNuevaClave" Text="Nueva contraseña" CssClass="form-label" />

                        <div class="password-toggle">
                            <asp:TextBox ID="txtNuevaClave" runat="server" TextMode="Password" CssClass="form-control"
                                placeholder="Ingresa tu nueva contraseña" />
                        </div>

                        <div class="password-requirements">
                            <small>La contraseña debe tener:</small>
                            <ul>
                                <li id="req-length" class="requirement-unmet">Al menos 8 caracteres</li>
                                <li id="req-upper" class="requirement-unmet">Al menos una letra mayúscula</li>
                                <li id="req-lower" class="requirement-unmet">Al menos una letra minúscula</li>
                                <li id="req-number" class="requirement-unmet">Al menos un número</li>
                            </ul>
                        </div>
                    </asp:Panel>

                    <!-- Confirmar contraseña -->
                    <asp:Panel ID="ConfirmarClavePanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblConfirmarClave" runat="server" AssociatedControlID="txtConfirmarClave" Text="Confirmar contraseña" CssClass="form-label" />

                        <div class="password-toggle">
                            <asp:TextBox ID="txtConfirmarClave" runat="server" TextMode="Password" CssClass="form-control"
                                placeholder="Confirma tu nueva contraseña" />
                        </div>

                        <asp:Label ID="lblErrorConfirmacion" runat="server" Visible="false" ForeColor="Red" CssClass="small mt-1 d-block" />
                    </asp:Panel>

                    <div class="d-flex gap-2">
                        <asp:Button ID="btnCambiarClave" runat="server" CssClass="btn btn-primary" Text="Cambiar contraseña" OnClick="BtnCambiarClave_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="BtnCancelar_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Validación en tiempo real de requisitos de contraseña
        document.addEventListener('DOMContentLoaded', function () {
            const passwordInput = document.getElementById('<%= txtNuevaClave.ClientID %>');

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

                    // Verificar minúscula
                    const reqLower = document.getElementById('req-lower');
                    if (/[a-z]/.test(password)) {
                        reqLower.classList.remove('requirement-unmet');
                        reqLower.classList.add('requirement-met');
                    } else {
                        reqLower.classList.remove('requirement-met');
                        reqLower.classList.add('requirement-unmet');
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
