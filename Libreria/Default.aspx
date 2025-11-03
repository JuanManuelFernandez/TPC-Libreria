<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Libreria.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Imagen de presentación--%>
    <div class="container-fluid">
        <img src="assets/Header.png" class="img-fluid w-100" style="height: 900px; object-fit: cover;" />
    </div>

    <!-- Sección dinámica de libros desde la BD -->
    <div class="container my-5">
        <h2 class="text-center mb-4">Catálogo</h2>

        <asp:Repeater ID="rptLibros" runat="server">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-6 col-lg-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='assets/libros/<%# Eval("IDLibro") %>.jpg'
                            class="card-img-top"
                            alt='<%# Eval("Titulo") %>'
                            style="width: 100%; height: auto; max-height: 400px; object-fit: contain; display: block; margin: 0 auto;"
                            onerror="src='assets/libros/default.png';" />
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                            <div class="mt-auto">
                                <asp:LinkButton ID="btnAgregarCarrito" runat="server" CssClass="btn btn-success w-100" CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_AgregarCarrito" Text='<i class="bi bi-cart-plus me-2"></i>AGREGAR' />
                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnAgregarLista" runat="server" CssClass="btn btn-primary w-100" CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_AgregarLista" Text='LISTA DE DESEADOS' />
                            </div>
                            <asp:Label ID="lblError" runat="server" Text="Debes iniciar sesión." Visible="false" ForeColor="Red" CssClass="text-center w-100 mt-2 small" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <!-- Mensaje si no hay libros -->
        <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
            <p>No hay libros disponibles en este momento, vuelve más tarde.</p>
        </asp:Panel>
    </div>

</asp:Content>