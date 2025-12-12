<%@ Page Title="Agregar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarLibro.aspx.cs" Inherits="Libreria.AgregarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center">
        <h1>Agregar libro</h1>
    </div>

    <div class="container mt-4">
        <div class="row">
            <!-- Columna izquierda -->
            <div class="col-md-6">
                <%-- Autor --%>
                <div class="mb-3 d-flex">
                    <asp:DropDownList ID="DdlAutor" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarAutor"/>
                </div>

                <%-- Género --%>
                <div class="mb-3 d-flex">
                    <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                    <asp:Button ID="Button2" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarGenero"/>
                </div>

                <%-- Editorial --%>
                <div class="mb-3 d-flex">
                    <asp:DropDownList ID="DdlEditorial" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarEditorial"/>
                </div>

                <%-- Título --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtTitulo" runat="server" placeholder="Título del libro" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <%-- Descripción --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtDescrip" runat="server" placeholder="Descripción" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>

            <!-- Columna derecha -->
            <div class="col-md-6">
                <%-- Fecha --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtFecha" runat="server" placeholder="Año-Mes-Día" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <%-- Precio --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtPrecio" runat="server" placeholder="Precio" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <%-- Páginas --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtPaginas" runat="server" placeholder="Páginas" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <%-- Stock --%>
                <div class="mb-3">
                    <asp:TextBox ID="TxtStock" runat="server" placeholder="Disponibles" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <%-- Portada --%>
                <div class="mb-3">
                    <asp:FileUpload ID="FilePortada" runat="server" CssClass="form-control form-control-lg"/>
                </div>

                <%-- Error label --%>
                <div class="mb-3">
                    <asp:Label ID="LblError" runat="server" Text="Label" Visible="false" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Botón agregar libro -->
        <div class="row mt-4">
            <div class="col-md-6 mx-auto">
                <asp:Button ID="Btn_Cargar" runat="server" Text="Agregar libro" CssClass="btn btn-primary btn-lg w-100" OnCommand="Btn_Agregar"/>
            </div>
        </div>
    </div>

</asp:Content>
