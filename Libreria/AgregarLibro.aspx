<%@ Page Title="Agregar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarLibro.aspx.cs" Inherits="Libreria.AgregarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center">
        <h1>Agregar libro</h1>
    </div>

    <%--Agregar libro--%>

    <%--IDAutor--%>
    <div class="text-center mt-1">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtIDAutor" runat="server" placeholder="ID de autor" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Genero--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtIDGenero" runat="server" placeholder="ID de genero" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--IDEditorial--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtIDEditorial" runat="server" placeholder="ID de editorial" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Titulo--%>
    <div class="text-center mt-3">
        <div class="col-md-3 mx-auto">
            <asp:TextBox ID="TxtTitulo" runat="server" placeholder="Titulo del libro" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Descripción--%>
    <div class="text-center mt-3">
        <div class="col-md-3 mx-auto">
            <asp:TextBox ID="TxtDescrip" runat="server" placeholder="Descripción" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Fecha--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtFecha" runat="server" placeholder="Año-Mes-Dia" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Precio--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtPrecio" runat="server" placeholder="Precio" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Paginas--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtPaginas" runat="server" placeholder="Páginas" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Stock--%>
    <div class="text-center mt-3">
        <div class="col-md-1 mx-auto">
            <asp:TextBox ID="TxtStock" runat="server" placeholder="Disponibles" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <%--Portada--%>
    <div class="text-center mt-3">
        <asp:FileUpload ID="FilePortada" runat="server" />
    </div>

    <%--Boton agregar--%>
    <div class="text-center mt-3">
        <asp:Button ID="Btn_Cargar" runat="server" Text="Agregar libro" CssClass="btn btn-primary" OnCommand="Btn_Agregar" />
    </div>

</asp:Content>
