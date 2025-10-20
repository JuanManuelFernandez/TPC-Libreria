<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Libreria.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="d-flex align-items-center justify-content-center">
    <div class="text-center mb-4"">
<%-- Nombre --%>
<div class="mb-3 position-relative">
    <asp:TextBox ID="NombreTxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="Nombre"
        required="required"
        pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
    <div class="invalid-tooltip">El nombre es obligatorio (solo letras, 2-50 caracteres).</div>
</div>

<%-- Apellido --%>
<div class="mb-3 position-relative">
    <asp:TextBox ID="ApellidoTxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="Apellido"
        required="required"
        pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
    <div class="invalid-tooltip">El apellido es obligatorio (solo letras, 2-50 caracteres).</div>
</div>

<%-- DNI --%>
<div class="mb-3 position-relative">
    <asp:TextBox ID="DNITxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="DNI (sin puntos)"
        TextMode="Number"
        required="required"
        pattern="^\d{8}$" />
    <div class="invalid-tooltip">Por favor, ingrese un DNI válido de 8 dígitos.</div>
</div>

<%-- Email --%>
<div class="mb-3 position-relative">
    <asp:TextBox ID="MailTxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="Email"
        TextMode="Email"
        required="required"
        pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
    <div class="invalid-tooltip">Por favor, ingrese un correo electrónico válido.</div>
</div>

<%-- Teléfono --%>
<div class="mb-3 position-relative">
    <asp:TextBox ID="TelefonoTxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="Teléfono"
        TextMode="Number"
        required="required"
        pattern="^(?:(?:00)?549?)?0?(?:11|\d)(?:(?=\d{0,2}15)\d{2})?\d{8}$" />
    <div class="invalid-tooltip">Por favor, ingrese un número de teléfono válido.</div>
</div>

<%-- Contraseña --%>
<label class="form-label text-primary fs-5 fw-bold mb-3" for="ClaveTxt">Contraseña</label>
<div class="mb-3 position-relative">
    <asp:TextBox ID="ClaveTxt" runat="server" CssClass="form-control form-control-lg"
        placeholder="Contraseña"
        TextMode="Password"
        required="required"
        pattern="^(?=.*[A-Za-z])(?=.*\d).{8,}$" />
    <div class="invalid-tooltip">La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra y un número.</div>
</div>

        <asp:Button ID="BtnRegistro" runat="server" CssClass="btn btn-primary btn-lg" Text="Registrarse" OnClick="BtnRegistro_Click" />
     </div>
</div>

</asp:Content>