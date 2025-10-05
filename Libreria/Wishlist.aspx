<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="Libreria.Wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Tu lista de deseados</h1>
    <asp:Label ID="LblAviso" runat="server" visible="false" forecolor="red" Text="Debes iniciar sesión para visualizar tu lista de deseados"></asp:Label>
</asp:Content>
