<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarAutor.aspx.cs" Inherits="Libreria.AgregarAutor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">
        AGREGAR AUTOR
    </h1>

    <div class="text-center">
        <div class="col-md-2 mx-auto d-flex justify-content-center">
            <asp:TextBox ID="TxtNombre" runat="server" PlaceHolder="Nombre" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>


    <div class="text-center mt-3">
        <div class="col-md-2 mx-auto d-flex justify-content-center">
            <asp:TextBox ID="TxtApellido" runat="server" PlaceHolder="Apellido" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
    </div>

    <div class="text-center mt-3">
        <asp:Button ID="BtnAgregar" runat="server" CssClass="btn btn-success" Text="Agregar" OnCommand="Btn_AgregarAutor"/>
    </div>

    <div class="text-center mt-2">
        <asp:Label ID="LblMensaje" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>

    <div class="text-center mt-2">
        <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-primary" Text="Volver" OnCommand="Btn_Volver"/>
    </div>
</asp:Content>
