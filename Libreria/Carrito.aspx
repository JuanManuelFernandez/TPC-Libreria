<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Libreria.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Tu carrito de compras</h1>
    <asp:Label ID="LblAviso" runat="server" visible="false" forecolor="red" Text="Debes iniciar sesión para visualizar tu carrito"></asp:Label>
</asp:Content>
