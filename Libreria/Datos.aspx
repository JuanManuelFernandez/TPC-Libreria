<%@ Page Title="Mis Datos" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Datos.aspx.cs" Inherits="Libreria.Datos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Agregar CSS de Bootstrap -->
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item"><a href="Cuenta.aspx">Mi Cuenta</a></li>
                <li class="breadcrumb-item active" aria-current="page">Datos</li>
            </ol>
        </nav>

        <div class="row">
            <!-- Edición de datos personales -->
            <div class="col-md-8">
                <h1 class="section-header">Mis datos</h1>
                <div class="info-box">
                    <h4 class="section-title">Editar Datos Personales</h4>

                    <asp:Panel ID="DatosPersonalesPanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />

                        <asp:Label ID="lblApellido" runat="server" AssociatedControlID="txtNombre" Text="Apellido" CssClass="form-label" />
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                    </asp:Panel>

                    <asp:Panel ID="MailPanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblMail" runat="server" AssociatedControlID="txtMail" Text="Mail" CssClass="form-label" />
                        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblErrorMail" runat="server" Visible="false" ForeColor="Red" />
                    </asp:Panel>

                    <asp:Panel ID="TelefonoPanel" runat="server" CssClass="mb-3">
                        <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" Text="Teléfono" CssClass="form-label" />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </asp:Panel>

                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar cambios" OnClick="BtnGuardar_Click" />
                </div>

                <div class="mt-4">
                    <asp:HyperLink ID="lnkCambiarClave" runat="server" NavigateUrl="CambiarClave.aspx" CssClass="btn btn-link">¿Querés cambiar tu contraseña?</asp:HyperLink>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
