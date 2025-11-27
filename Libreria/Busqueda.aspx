<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="Libreria.Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .resultados-header {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="resultados-header">
            <h2>Resultados de búsqueda</h2>
            <p class="text-muted mb-0">Mostrando los resultados para "<asp:Literal ID="litTermino" runat="server" />"</p>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false" />

        <asp:Repeater ID="rptLibros" runat="server">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-6 col-lg-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='assets/portadas/<%# Eval("IdLibro") %>.jpg'
                            class="card-img-top"
                            alt='<%# Eval("Titulo") %>'
                            style="width: 100%; height: auto; max-height: 400px; object-fit: contain; display: block; margin: 0 auto;"
                            onerror="this.src='assets/portadas/default.png';" />
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text text-muted"><%# Eval("NombreAutor") %></p>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                            <div class="mt-auto">
                                <asp:LinkButton ID="btnAgregarCarrito" runat="server"
                                    CssClass="btn btn-success w-100"
                                    CommandArgument='<%# Eval("IdLibro") %>'
                                    OnCommand="Btn_AgregarCarrito"
                                    Text='<i class="bi bi-cart-plus me-2"></i>AGREGAR' />
                            </div>
                            <div class="mt-2">
                                <asp:LinkButton ID="btnAgregarLista" runat="server"
                                    CssClass="btn btn-primary w-100"
                                    CommandArgument='<%# Eval("IdLibro") %>'
                                    OnCommand="Btn_AgregarLista"
                                    Text='LISTA DE DESEADOS' />
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

        <!-- Mensaje si no hay libros -->
        <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
            <p>Todavía no has agregado libros a tu lista de deseados.</p>
        </asp:Panel>

    </div>
</asp:Content>
