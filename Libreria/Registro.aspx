<%@ Page Title="Registro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Libreria.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex align-items-center justify-content-center">
    <div class="text-center mb-4"">
        <%--Nombre--%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="NombreTxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="Nombre"
                required="required"
                pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
            <%--<div class="invalid-tooltip">El nombre es obligatorio (solo letras, 2-50 caracteres).</div>--%>
        </div>
        
        <%-- Apellido --%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="ApellidoTxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="Apellido"
                required="required"
                pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
            <%--<div class="invalid-tooltip">El apellido es obligatorio (solo letras, 2-50 caracteres).</div>--%>
        </div>
        
        <%-- DNI --%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="DNITxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="DNI"
                required="required"
                pattern="^\d{8}$" />
            <%--<div class="invalid-tooltip">Por favor, ingrese un DNI válido de 8 dígitos.</div>--%>
        </div>

        <%-- Email --%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="MailTxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="Email"
                TextMode="Email"
                required="required"
                pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
            <%--<div class="invalid-tooltip">Por favor, ingrese un mail válido.</div>--%>
        </div>

        <%-- Teléfono --%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="TelefonoTxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="Teléfono"
                required="required"
                pattern="^\d{10,11}$" />
            <%--<div class="invalid-tooltip">Por favor, ingrese un número de teléfono válido.</div>--%>
        </div>
        
        <%-- Contraseña --%>
        <div class="mb-3 position-relative">
            <asp:TextBox ID="ClaveTxt" runat="server" CssClass="form-control form-control-lg"
                placeholder="Contraseña"
                TextMode="Password"
                required="required"
                pattern="^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$" />
            <%--<div class="invalid-tooltip">La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra y un número.</div>--%>
        </div>
        <div>
            <asp:Label ID="LblError" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
        </div>

        <%--(?=.*[A-Za-z]): Al menos una letra (mayúscula o minúscula).--%>
        <%--(?=.*\d): Al menos un dígito (0-9).--%>
        <%--(?=.*[^A-Za-z0-9]): Al menos un carácter especial (cualquier símbolo que no sea letra ni dígito, como !@#$%^&*).--%>
        <%--.{8,}: Longitud mínima de 8 caracteres (puede ser más).--%>
        <%--^ y $: Anclan el inicio y fin de la cadena para asegurar que coincida completamente--%>

        <asp:Button ID="BtnRegistro" runat="server" CssClass="btn btn-primary btn-lg" Text="Registrarse" OnClick="BtnRegistro_Click" />
     </div>
</div>
</asp:Content>
