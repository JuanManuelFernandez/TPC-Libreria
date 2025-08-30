<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="Libreria.Cuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex align-items-center justify-content-center">
        <div class="text-center mb-4"">
            <%--<asp:Label ID="LblMaIL" runat="server" Text="Correo electronico" ForeColor="Blue"></asp:Label>--%>
            <label class="form-label text-primary fs-5 fw-bold mb-3" for="MailTxt">Email</label>
            <div class="form-group mb-3">
                <asp:TextBox ID="MailTxt" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
            </div>

            <%--<asp:Label ID="LblClave" runat="server" Text="Contraseña" ForeColor="Blue" CssClass="form-label"></asp:Label>--%>
            <label class="form-label text-primary fs-5 fw-bold mb-3" for="ClaveTxt">Contraseña</label>
            <div class="form-group mb-3">
                <asp:TextBox ID="ClaveTxt" runat="server" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>
                <asp:HyperLink ID="LinkRecuperar" runat="server" NavigateUrl="~/Recuperar.aspx" CssClass="text-primary" Text="Olvide mi contraseña"></asp:HyperLink>
            </div>

            <asp:HyperLink ID="LinkRegistro" runat="server" NavigateUrl="~/Registro.aspx" CssClass="text-primary" Text="¿No tienes cuenta? regístrate"></asp:HyperLink>
         </div>
    </div>
</asp:Content>