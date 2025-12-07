<%@ Page Title="Libro Modificado" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LibroModificado.aspx.cs" Inherits="Libreria.LibroModificado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>¡Libro editado con exito!</h1>
    </div>}

    <div class="text-center">
        <img src="/assets/Check.png" class="img-fluid mb-3" style="max-width: 300px;" />
    </div>

    <div class="text-center">
        <asp:Button ID="Btn_VolverInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-primary" OnCommand="Btn_Inicio"/>
    </div>

</asp:Content>
