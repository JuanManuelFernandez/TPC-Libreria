<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Libreria.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Altura fija para las imágenes de los libros */
        .cart-image-container {
            height: 300px;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #f8f9fa;
            overflow: hidden;
        }

            .cart-image-container img {
                width: 100%;
                height: 100%;
                object-fit: contain;
                padding: 10px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <!-- Barra de navegacion -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Mi Carrito</li>
            </ol>
        </nav>

        <h2 class="text-center mb-4">Tu carrito de compras</h2>

        <div class="row">

            <asp:Repeater ID="rptLibros" runat="server">

                <ItemTemplate>
                    <div class="col-md-6 col-lg-3 mb-4">
                        <div class="card h-100 shadow-sm">
                            <!-- Contenedor de imagen con altura fija -->
                            <div class="cart-image-container">
                                <img src='assets/portadas/<%# Eval("IDLibro") %>.jpg'
                                    class="card-img-top"
                                    alt='<%# Eval("Titulo") %>'
                                    onerror="src='assets/portadas/default.png';" />
                            </div>
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                            </div>

                            <div class="row mb-2 m-2">
                                <!-- Botón de resta -->
                                <div class="col-4 text-start">
                                    <asp:LinkButton ID="btnRestar" runat="server"
                                        CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_Eliminar">
                                    <img src="assets/resta.png" alt="-" style="height:20px;" />
                                    </asp:LinkButton>
                                </div>

                                <!-- Contador -->
                                <div class="col-4 text-center">
                                    <span class="badge bg-primary">x<%# Eval("Cantidad") %></span>
                                </div>

                                <!-- Botón de suma -->
                                <div class="col-4 text-end">
                                    <asp:LinkButton ID="btnSumar" runat="server"
                                        CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_Sumar">
                                    <img src="assets/suma.png" alt="+" style="height:20px;" />
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- Botón eliminar -->
                            <div class="mt-auto">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger w-100"
                                    CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_Eliminar"
                                    Text='<i class="bi bi-cart-plus me-2"></i>ELIMINAR' />
                            </div>

                        </div>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>

        <div class="text-center">
            <asp:Label ID="LblMensaje" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>

        <div class="mt-2 text-center">
            <asp:LinkButton ID="btnIrApagar" runat="server" CssClass="btn btn-primary" OnCommand="Btn_IrApagar" Visible="true" Text='Ir a pagar' />
        </div>

        <!-- Mensaje si no hay libros -->
        <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
            <p>Todavía no has agregado libros a tu carrito.</p>
        </asp:Panel>

    </div>
</asp:Content>
