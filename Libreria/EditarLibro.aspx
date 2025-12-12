<%@ Page Title="Editar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditarLibro.aspx.cs" Inherits="Libreria.EditarLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center">
        <h1>Editar libro</h1>
    </div>

    <div class="container mt-4">
        <div class="row">
            <div class="col-md-6">
                <%-- Autor --%>
                <div class="mb-3">
                    <asp:Label ID="LblAutor" runat="server" Text="Autor"></asp:Label>
                    <asp:DropDownList ID="DdlAutor" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                </div>

                <%-- Género --%>
                <div class="mb-3">
                    <asp:Label ID="LblGenero" runat="server" Text="Género"></asp:Label>
                    <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                </div>

                <%-- Editorial --%>
                <div class="mb-3">
                    <asp:Label ID="LblEditorial" runat="server" Text="Editorial"></asp:Label>
                    <asp:DropDownList ID="DdlEditorial" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                </div>

                <%-- Título --%>
                <div class="mb-3">
                    <asp:Label ID="LblTitulo" runat="server" Text="Título"></asp:Label>
                    <asp:TextBox ID="TxtTitulo" runat="server" CssClass="form-control form-control-lg" placeholder="Título del libro"></asp:TextBox>
                </div>

                <%-- Descripción --%>
                <div class="mb-3">
                    <asp:Label ID="LblDescrip" runat="server" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="TxtDescrip" runat="server" CssClass="form-control form-control-lg" placeholder="Descripción"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-6">
                <%-- Fecha --%>
                <div class="mb-3">
                    <asp:Label ID="LblFecha" runat="server" Text="Fecha de publicación"></asp:Label>
                    <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control form-control-lg" placeholder="Año-Mes-Día"></asp:TextBox>
                </div>

                <%-- Precio --%>
                <div class="mb-3">
                    <asp:Label ID="LblPrecio" runat="server" Text="Precio"></asp:Label>
                    <asp:TextBox ID="TxtPrecio" runat="server" CssClass="form-control form-control-lg" placeholder="Precio"></asp:TextBox>
                </div>

                <%-- Páginas --%>
                <div class="mb-3">
                    <asp:Label ID="LblPaginas" runat="server" Text="Número de páginas"></asp:Label>
                    <asp:TextBox ID="TxtPaginas" runat="server" CssClass="form-control form-control-lg" placeholder="Páginas"></asp:TextBox>
                </div>

                <%-- Stock --%>
                <div class="mb-3">
                    <asp:Label ID="LblStock" runat="server" Text="Stock disponible"></asp:Label>
                    <asp:TextBox ID="TxtStock" runat="server" CssClass="form-control form-control-lg" placeholder="Disponibles"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="text-center mt-4">
            <asp:Button ID="Btn_Guardar" runat="server" Text="Guardar cambios" CssClass="btn btn-primary" OnCommand="Btn_GuardarCambios"/>
        </div>
    </div>

</asp:Content>