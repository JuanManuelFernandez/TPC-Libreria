<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="Libreria.Pago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center">
        <div class="w-50">
            <h2 class="text-center">Agregar método de pago</h2>

            <div class="d-flex align-items-center gap-5">

                <!-- Número de tarjeta -->
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblNumeroTarjeta" runat="server" Text="Número de la tarjeta"></asp:Label>
                    <asp:TextBox ID="TxtNumeroTarjeta" runat="server" CssClass="form-control form-control-lg"
                        placeholder="0000-0000-0000-0000"
                        Style="width: 230px !important;"
                        required="required"
                        pattern="^\d{16}$" />
                </div>

                <!-- Fecha y CVV -->
                <div>
                    <asp:Label ID="LblCaducidad" runat="server" Text="Fecha de caducidad y código de seguridad"></asp:Label>
                    <div class="d-flex align-items-center gap-4">

                        <!-- Mes -->
                        <asp:TextBox ID="TxtMes" runat="server" CssClass="form-control form-control-lg"
                            Style="width: 65px !important;"
                            required="required"
                            pattern="^(0[1-9]|1[0-2])$"
                            placeholder="MM" />

                        <!-- Año -->
                        <asp:TextBox ID="TxtYear" runat="server" CssClass="form-control form-control-lg"
                            Style="width: 80px !important;"
                            required="required"
                            pattern="^20[2-9][0-9]$"
                            placeholder="AAAA" />

                        <!-- Código de seguridad -->
                        <asp:TextBox ID="TxtCodigoSeguridad" runat="server" CssClass="form-control form-control-lg"
                            Style="width: 80px !important;"
                            required="required"
                            pattern="^\d{3,4}$"
                            placeholder="CVV" />

                    </div>
                </div>
            </div>

            <!-- Información de facturación -->
            <div class="text-center mt-4">
                <h3>Información de facturación</h3>
            </div>

            <!-- Email -->
            <div class="text-center mt-3">
                <asp:Label ID="LblMail" runat="server" Text="Mail" Style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtMail" runat="server" CssClass="form-control form-control-lg"
                    Style="width: 540px !important;"
                    TextMode="Email"
                    required="required"
                    pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
            </div>

            <!-- Nombre y apellido -->
            <div class="d-flex align-items-center gap-3 mt-3">

                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control form-control-lg"
                        required="required"
                        pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
                </div>

                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblApellido" runat="server" Text="Apellido"></asp:Label>
                    <asp:TextBox ID="TxtApellido" runat="server" CssClass="form-control form-control-lg"
                        required="required"
                        pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
                </div>
            </div>

            <!-- Dirección -->
            <div class="text-center mt-3">
                <asp:Label ID="LblDireccion" runat="server" Text="Dirección de facturación" Style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control form-control-lg"
                    Style="width: 540px !important;"
                    required="required"
                    pattern="^[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ\s.,#-]{5,100}$" />
            </div>

            <!-- Localidad y CP -->
            <div class="d-flex align-items-center gap-3 mt-3">
                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblLocalidad" runat="server" Text="Localidad"></asp:Label>
                    <asp:TextBox ID="TxtLocalidad" runat="server" CssClass="form-control form-control-lg"
                        required="required"
                        pattern="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$" />
                </div>

                <div class="d-flex flex-column align-items-center">
                    <asp:Label ID="LblCodigoPostal" runat="server" Text="Código postal"></asp:Label>
                    <asp:TextBox ID="TxtCodigoPostal" runat="server" CssClass="form-control form-control-lg"
                        required="required"
                        pattern="^\d{4,5}$" />
                </div>
            </div>

            <!-- Teléfono -->
            <div class="text-center mt-3">
                <asp:Label ID="LblTelefono" runat="server" Text="Teléfono" Style="margin-left: -100px;"></asp:Label>
                <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control form-control-lg"
                    placeholder="11-1111-1111"
                    Style="margin-left: 190px; width: 155px !important"
                    required="required"
                    pattern="^\d{10,11}$" />
            </div>

            <!-- Total -->
            <div class="text-center mt-2">
                <asp:Label ID="LblTotal" runat="server" Text="Label" CssClass="text-success" Style="margin-left: -100px;"></asp:Label>
            </div>

            <div class="text-center">
                <asp:Button ID="btnPagar" runat="server" CssClass="btn btn-primary btn-lg" Text="Finalizar compra" OnCommand="Btn_Comprar" Style="margin-left: -100px;"/>
            </div>
        </div>
    </div>
</asp:Content>
