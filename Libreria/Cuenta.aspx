<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="Libreria.Cuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex align-items-center justify-content-center">
        <div class="text-center mb-4"">
            <asp:Label ID="LblMaIL" runat="server" Text="Correo electronico" ForeColor="Blue" Visible="true"></asp:Label>
            
            <div class="form-group mb-3">
                <asp:TextBox ID="MailTxt" runat="server" Visible="true" CssClass="form-control form-control-lg"></asp:TextBox>
            </div>

            <asp:Label ID="LblClave" runat="server" Text="Contraseña" ForeColor="Blue" CssClass="form-label" Visible="true"></asp:Label>
            <div class="form-group mb-3">
                <asp:TextBox ID="ClaveTxt" runat="server" Visible="true" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>

                <div class="row justify-content-center mt-3">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" Visible="true" CssClass="btn btn-primary btn-lg mx-3" OnClick ="btnIngresar_Click" />

                    <asp:Label ID="lblError" runat="server" Text="Email o Clave incorrecto..." Visible="false" ForeColor="Red"></asp:Label>

                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar sesión" CssClass="btn btn-danger btn-lg mx-3" Visible="false" OnClick ="btnCerrar_Click" />
                </div>
            </div>
            <div>
                <asp:HyperLink ID="LinkRecuperar" runat="server" NavigateUrl="~/Recuperar.aspx" CssClass="text-primary" Text="Olvide mi contraseña"></asp:HyperLink>
            </div>
            <div>
                <asp:HyperLink ID="LinkRegistro" runat="server" NavigateUrl="~/Registro.aspx" CssClass="text-primary" Text="¿No tienes cuenta? regístrate"></asp:HyperLink>
            </div>
         </div>
    </div>
</asp:Content>