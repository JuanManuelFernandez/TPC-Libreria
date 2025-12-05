<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Libreria.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <div class="text-center">
        <asp:Label ID="LblAviso" runat="server" Visible="false" ForeColor="red" Text="Debes iniciar sesión para visualizar tu carrito"></asp:Label>
    </div>--%>

    <div class="container my-5">
        <h2 class="text-center mb-4">Tu carrito de compras</h2>

        <asp:Repeater ID="rptLibros" runat="server">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-6 col-lg-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='assets/portadas/<%# Eval("IDLibro") %>.jpg'
                            class="card-img-top"
                            alt='<%# Eval("Titulo") %>'
                            style="width: 100%; height: auto; max-height: 400px; object-fit: contain; display: block; margin: 0 auto;"
                            onerror="src='assets/portadas/default.png';" />
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                        </div>
                        <div class="mt-auto">
                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger w-100" CommandArgument='<%# Eval("IDLibro") %>' OnCommand="Btn_Eliminar" Text='<i class="bi bi-cart-plus me-2"></i>ELIMINAR' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <div class="mt-2 text-center">
            <asp:LinkButton ID="btnIrApagar" runat="server" CssClass="btn btn-primary" OnCommand="Btn_IrApagar" Visible="true" Text='Ir a pagar' />
        </div>
        <!-- Mensaje si no hay libros -->
        <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
            <p>Todavía no has agregado libros a tu carrito.</p>
        </asp:Panel>
    </div>
</asp:Content>
