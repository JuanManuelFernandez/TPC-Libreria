<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="Libreria.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aquí puedes agregar otros elementos dentro de la cabecera si es necesario -->
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
        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Mi Cuenta</li>
            </ol>
        </nav>

        <div class="row">
            <!-- Datos personales -->
            <div class="col-md-8">
                <h1>Mi cuenta</h1>
                <div class="info-box">
                    <h4 class="section-title">Datos Personales</h4>
                    <p><strong>Nombre:</strong> <%= UserName %></p>
                    <p><strong>Mail:</strong> <%= UserMail %></p>
                    <p><strong>Teléfono:</strong> <%= UserPhone %></p>
                    <a href="Datos.aspx" class="btn btn-link">Editar</a>
                </div>
            </div>

            <!-- Carrito -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body text-center">
                        <img src="assets/cart.png" alt="Carrito" width="50">
                        <p class="mt-2">¡Hacé tu primera compra!</p>
                        <a href="Default.aspx" class="btn btn-primary btn-shop">IR A LA TIENDA</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts JS de Bootstrap -->
<%--    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>--%>
</asp:Content>