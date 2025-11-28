<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="Libreria.Pago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center">
        <div class="w-50">
            <h2 class="text-center">Agregar metodo de pago</h2>

            <div class="d-flex align-items-center gap-5">
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblNumeroTarjeta" runat="server" Text="Número de la tarjeta"></asp:Label>
                    <asp:TextBox ID="TxtNumeroTarjeta" runat="server" CssClass="form-control form-control-lg" style="width: 230px !important;"></asp:TextBox>
                </div>

                <div>
                    <asp:Label ID="LblCaducidad" runat="server" Text="Fecha de caducidad y codigo de seguridad"></asp:Label>
                    <div class="d-flex align-items-center gap-4">
                        <asp:TextBox ID="TxtMes" runat="server" CssClass="form-control form-control-lg" style="width: 65px !important;"></asp:TextBox>
                        <h2>/</h2>
                        <asp:TextBox ID="TxtYear" runat="server" CssClass="form-control form-control-lg" style="width: 80px !important;"></asp:TextBox>
                        <asp:TextBox ID="TxtCodigoSeguridad" runat="server" CssClass="form-control form-control-lg" style="width: 80px !important;"></asp:TextBox>
                    </div>
                </div>
            </div>
            

            <div class="text-center mt-4">
                <h3>Información de facturación</h3>
            </div>

            <div class="text-center mt-3">
                <asp:Label ID="LblCorreo" runat="server" Text="Correo electronico" style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control form-control-lg" style="width: 540px !important;"></asp:TextBox>
            </div>

            <div class="d-flex align-items-center gap-3">
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
                <div>
                    <div class="d-flex flex-column align-items-center">
                        <asp:Label ID="LblApellido" runat="server" Text="Apellido"></asp:Label>
                        <asp:TextBox ID="TxtApellido" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="text-center mt-3">
                <asp:Label ID="LblDireccion" runat="server" Text="Dirección de facturación" style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control form-control-lg" style="width: 540px !important;"></asp:TextBox>
            </div>

            <div class="d-flex align-items-center gap-3">
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblLocalidad" runat="server" Text="Localidad"></asp:Label>
                    <asp:TextBox ID="TxtLocalidad" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblCodigoPostal" runat="server" Text="Código postal"></asp:Label>
                    <asp:TextBox ID="TxtCodigoPostal" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>

            <div class="text-center mt-3">
                <asp:Label ID="LblTelefono" runat="server" Text="Teléfono" style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control form-control-lg" style="margin-left: 190px; width: 155px !important"></asp:TextBox>
            </div>

            <div class="mt-2 text-center">
                <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-primary" OnCommand="Btn_Comprar" Visible="true" Text='Finalizar compra' style="margin-left: -100px;" />
            </div>
        </div>
    </div>
</asp:Content>
