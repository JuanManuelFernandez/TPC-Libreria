<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Libreria.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex align-items-center justify-content-center">
        <div class="text-center mb-4"">

            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnIngresar">
                <div class="form-group mb-3">
                    <asp:Label ID="LblMaIL" runat="server" Text="Correo electronico" ForeColor="Blue" Visible="true"></asp:Label>
                    <asp:TextBox ID="MailTxt" runat="server" Visible="true" pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>

                <div class="form-group mb-3">
                    <asp:Label ID="LblClave" runat="server" Text="Contraseña" ForeColor="Blue" CssClass="form-label" Visible="true"></asp:Label>
                    <asp:TextBox ID="ClaveTxt" runat="server" Visible="true" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>
                </div>

                <div class="row justify-content-center mt-3">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" Visible="true" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnIngresar_Click" />
                    <asp:Label ID="lblError" runat="server" Text="Mail o Clave incorrecto..." Visible="false" ForeColor="Red"></asp:Label>
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-danger btn-lg mx-3" Visible="false" OnClick="btnCerrarSesion_Click" />
                </div>

            </asp:Panel>

            <div>
                <asp:HyperLink ID="lnkRecuperar" runat="server" NavigateUrl="~/Recuperar.aspx" CssClass="text-primary" Text="Olvide mi contraseña"></asp:HyperLink>
                <br>
                <asp:HyperLink ID="lnkRegistro" runat="server" NavigateUrl="~/Registro.aspx" CssClass="text-primary" Text="¿No tienes cuenta? Regístrate aqui"></asp:HyperLink>
            </div>

         </div>
    </div>
</asp:Content>