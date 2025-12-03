<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LibroAgregado.aspx.cs" Inherits="Libreria.LibroAgregado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>¡Libro agregado con exito!</h1>
    </div>

    <div class="text-center">
        <img src="/assets/Check.png" class="img-fluid mb-3" style="max-width: 300px;" />
    </div>

    <div class="text-center">
        <asp:Button ID="Btn_VolverInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-primary" OnCommand="Btn_Inicio"/>
    </div>

    <div class="text-center mt-3">
        <asp:Button ID="Btn_AgregarOtro" runat="server" Text="Agregar otro libro" CssClass="btn btn-success" OnCommand="Btn_Volver"/>
    </div>
</asp:Content>
