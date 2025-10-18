<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Libreria.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="d-flex align-items-center justify-content-center">
    <div class="text-center mb-4"">
        <%--Nombre--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="NombreTxt">Nombre</label>
        <div>
            <asp:TextBox ID="NombreTxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
        <%--Apellido--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="ApellidoTxt">Apellido</label>
        <div>
            <asp:TextBox ID="ApellidoTxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
        <%--DNI--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="DNITxt">DNI</label>
        <div>
            <asp:TextBox ID="DNITxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
        <%--Mail--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="MailTxt">Email</label>
        <div class="form-group mb-3">
            <asp:TextBox ID="MailTxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
        <%--Telefono--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="TelefonoTxt">Telefono</label>
        <div>
            <asp:TextBox ID="TelefonoTxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
        </div>
        <%--Contraseña--%>
        <label class="form-label text-primary fs-5 fw-bold mb-3" for="ClaveTxt">Contraseña</label>
        <div class="form-group mb-3">
            <asp:TextBox ID="ClaveTxt" runat="server" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>
        </div>

        <asp:Button ID="BtnRegistro" runat="server" CssClass="btn btn-primary btn-lg" Text="Registrarse" OnClick="BtnRegistro_Click" />
     </div>
</div>
</asp:Content>
