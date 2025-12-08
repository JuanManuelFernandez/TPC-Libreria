<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Libreria.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS de Bootstrap -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    <style>
        /* Altura fija para las imágenes de los libros */
        .book-image-container {
            height: 300px;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #f8f9fa;
            overflow: hidden;
        }

            .book-image-container img {
                width: 100%;
                height: 100%;
                object-fit: contain;
                padding: 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Imagen de presentación--%>
    <div class="container-fluid">
        <img src="assets/Header2.png" class="img-fluid w-100" style="height: 900px; object-fit: cover;" />
    </div>
    <!-- Sección dinámica de libros desde la BD -->
    <div class="container my-5">
        <h2 class="text-center mb-4">Catálogo</h2>
        <asp:Repeater ID="rptLibros" runat="server" OnItemDataBound="Ocultar_Botones">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-lg-3 col-md-4 col-sm-6 mb-4" id='libro<%# Eval("IDLibro") %>'>
                    <div class="card h-100 shadow-sm">
                        <!-- Contenedor de imagen con altura fija -->
                        <div class="book-image-container">
                            <img src='assets/portadas/<%# Eval("IDLibro") %>.jpg'
                                alt='<%# Eval("Titulo") %>'
                                onerror="this.src='assets/portadas/default.png';" />
                        </div>
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                            <p class="card-text"><%# Eval("Stock") + " disponibles" %></p>

                            <div class="mt-auto">
                                <asp:LinkButton ID="btnAgregarCarrito" runat="server"
                                    CssClass="btn btn-success w-100"
                                    CommandArgument='<%# Eval("IDLibro") %>'
                                    OnCommand="Btn_AgregarCarrito"
                                    Text='<i class="bi bi-cart-plus me-2"></i>AGREGAR'
                                    Visible='<%# Convert.ToInt32(Eval("Stock")) > 0 %>' />
                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnAgregarLista" runat="server"
                                    CssClass="btn btn-primary w-100"
                                    CommandArgument='<%# Eval("IDLibro") %>'
                                    OnCommand="Btn_AgregarLista"
                                    Text='LISTA DE DESEADOS' />
                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnEliminarLibro" runat="server"
                                    CssClass="btn btn-danger w-100"
                                    CommandArgument='<%# Eval("IDLibro")%>'
                                    OnCommand="Btn_EliminarLibro"
                                    Text="Eliminar"
                                    Visible='<%# Convert.ToBoolean(Eval("Disponible")) %>' />

                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnDarDeAlta" runat="server"
                                    CssClass="btn btn-success w-100"
                                    CommandArgument='<%# Eval("IDLibro")%>'
                                    OnCommand="Btn_DarDeAltaLibro"
                                    Text="Mostrar" />
                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnModificar" runat="server"
                                    CssClass="btn btn-primary w-100"
                                    CommandArgument='<%# Eval("IDLibro")%>'
                                    OnCommand="Btn_Modificar"
                                    Text="Editar" />
                            </div>
                            <asp:Label ID="lblError" runat="server"
                                Text="Debes iniciar sesión."
                                Visible="false"
                                ForeColor="Red"
                                CssClass="text-center w-100 mt-2 small" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <div class="text-center">
            <asp:Button ID="btnAgregarLibro" runat="server" Text="Agregar libro" CssClass="btn btn-primary" Visible="false" OnCommand="Btn_AgregarLibro" />
        </div>

        <!-- Mensaje si no hay libros -->
        <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
            <p>No hay libros disponibles en este momento, vuelve más tarde.</p>
        </asp:Panel>
    </div>
</asp:Content>
