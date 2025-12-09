<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="Libreria.Cuenta" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Barra de navegacion -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Mi Cuenta</li>
            </ol>
        </nav>

        <div class="row">
            <!-- Datos personales -->
            <asp:Panel ID="datosPersonales" runat="server" CssClass="col-md-8">
                <h1>Mi cuenta</h1>

                <div class="info-box">
                    <h4 class="section-title">Datos Personales</h4>

                    <asp:Label ID="LblNombre" runat="server" Text="Nombre:" CssClass="fw-bold"></asp:Label>
                    <asp:Label ID="TxtNombre" runat="server" CssClass="d-block mb-2"></asp:Label>

                    <asp:Label ID="LblApellido" runat="server" Text="Apellido:" CssClass="fw-bold"></asp:Label>
                    <asp:Label ID="TxtApellido" runat="server" CssClass="d-block mb-2"></asp:Label>

                    <asp:Label ID="LblMail" runat="server" Text="Mail:" CssClass="fw-bold"></asp:Label>
                    <asp:Label ID="TxtMail" runat="server" CssClass="d-block mb-2"></asp:Label>

                    <asp:Label ID="LblTelefono" runat="server" Text="Teléfono:" CssClass="fw-bold"></asp:Label>
                    <asp:Label ID="TxtTelefono" runat="server" CssClass="d-block mb-3"></asp:Label>

                    <asp:HyperLink ID="linkEditar" runat="server" NavigateUrl="Datos.aspx" CssClass="btn btn-link">
                        Editar
                    </asp:HyperLink>

                    <asp:Button ID="btnEliminarCuenta"
                        runat="server"
                        CssClass="btn btn-link"
                        Text="Eliminar cuenta"
                        OnClick="BtnEliminarCuenta_Click"
                        Style="color: red; background-color: transparent; border: none;" />

                </div>
            </asp:Panel>



            <!-- Mensaje de confirmacion -->
            <asp:Panel ID="confirmacion" runat="server" CssClass="col-md-8" Visible="false">
                <h1>¿Estás seguro de que quieres eliminar tu cuenta?</h1>
                <asp:Button ID="btnSi" runat="server" Text="Sí" CssClass="btn btn-success" OnClick="BtnSi_Click" />
                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger" OnClick="BtnNo_Click" />
            </asp:Panel>

            <!-- Carrito -->
            <asp:Panel ID="carrito" runat="server" CssClass="col-md-4">
                <div class="card">
                    <div class="card-body text-center">
                        <img src="assets/cart.png" alt="Carrito" width="50">
                        <p class="mt-2">¡Hacé tu primera compra!</p>
                        <a href="Default.aspx" class="btn btn-primary btn-shop">IR A LA TIENDA</a>
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>
