<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Libreria.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">

        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Login</li>
            </ol>
        </nav>

        <!-- Formulario centrado -->
        <div class="d-flex align-items-center justify-content-center">
            <div class="text-center mb-4">

                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnIngresar">

                    <div class="form-group mb-3">
                        <asp:Label ID="LblMail" runat="server" Text="Mail" ForeColor="Blue"></asp:Label>
                        <asp:TextBox ID="MailTxt" runat="server"
                            CssClass="form-control form-control-lg"
                            pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            MaxLength="100"></asp:TextBox>
                    </div>

                    <div class="form-group mb-3">
                        <asp:Label ID="LblClave" runat="server" Text="Contraseña" ForeColor="Blue" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="ClaveTxt" runat="server"
                            CssClass="form-control form-control-lg"
                            TextMode="Password"
                            MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="row justify-content-center mt-3">
                        <asp:Button ID="btnIngresar" runat="server"
                            Text="Ingresar"
                            CssClass="btn btn-primary btn-lg mx-3"
                            OnClick="BtnIngresar_Click" />

                        <asp:Label ID="lblError" runat="server"
                            Text="Mail o Clave incorrecto..."
                            Visible="false"
                            ForeColor="Red"></asp:Label>

                        <asp:Button ID="btnCerrarSesion" runat="server"
                            Text="Cerrar sesión"
                            CssClass="btn btn-danger btn-lg mx-3"
                            Visible="false"
                            OnClick="BtnCerrarSesion_Click" />
                    </div>

                </asp:Panel>

                <div class="mt-3">
                    <asp:HyperLink ID="lnkRecuperar" runat="server"
                        NavigateUrl="~/Recuperar.aspx"
                        CssClass="text-primary"
                        Text="Olvidé mi contraseña"></asp:HyperLink>
                    <br />
                    <asp:HyperLink ID="lnkRegistro" runat="server"
                        NavigateUrl="~/Registro.aspx"
                        CssClass="text-primary"
                        Text="¿No tienes cuenta? Regístrate aquí"></asp:HyperLink>
                </div>

            </div>
        </div>

    </div>

</asp:Content>
