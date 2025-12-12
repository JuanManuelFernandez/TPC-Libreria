<%@ Page Title="Agregar Genero" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarGenero.aspx.cs" Inherits="Libreria.AgregarGenero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-4">

        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item"><a href="AgregarLibro.aspx">Agregar Libro</a></li>
                <li class="breadcrumb-item active" aria-current="page">Agregar Genero</li>
            </ol>
        </nav>

        <h1 class="text-center">Agregar genero
        </h1>

        <div class="text-center mt-3">
            <div class="col-md-2 mx-auto d-flex justify-content-center">
                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control form-control-lg" PlaceHolder="Nombre"></asp:TextBox>
            </div>
        </div>

        <div class="text-center mt-3">
            <asp:Button ID="Btn_Agregar" runat="server" CssClass="btn btn-success" Text="Agregar" OnCommand="Btn_AgregarGenero" />
        </div>

        <div class="text-center mt-2">
            <asp:Label ID="LblMensaje" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>

        <div class="text-center mt-2">
            <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-primary" Text="Volver" OnCommand="Btn_Volver" />
        </div>

    </div>

</asp:Content>
