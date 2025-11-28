<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PagoCompletado.aspx.cs" Inherits="Libreria.PagoCompletado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>¡Gracias por tu compra!</h1>
    </div>
    
    <div class="text-center">
        <img src="/assets/Check.png" class="img-fluid mb-3" style="max-width: 300px;" />
    </div>
    
    <div class="text-center">
        <h5>Recibiras un correo con toda la información acerca de tu compra.</h5>
    </div>

    <div class="mt-2 text-center">
        <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-primary" OnCommand="Btn_Inicio" Visible="true" Text='Volver al inicio' />
    </div>
</asp:Content>
