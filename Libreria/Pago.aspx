<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="Libreria.Pago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center">
        <div class="w-50">
            <h1 class="text-center">Pagina de pago</h1>

            <div class="d-flex flex-column align-items-center">
                <div>
                    <asp:Label ID="LblNumeroTarjeta" runat="server" Text="Número de la tarjeta"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TxtNumeroTarjeta" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>

            <div class="text-center">
                <h3>INFORMACIÓN DE FACTURACIÓN</h3>
            </div>
   
            <div class="d-flex flex-column align-items-center">
                <div>
                    <asp:Label ID="LblNombre" runat="server" Text="Nombre"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>

            <div class="d-flex flex-column align-items-center">
                <div>
                    <asp:Label ID="LblApellido" runat="server" Text="Apellido"></asp:Label>
                </div>
                <div class="mb-3 position-relative">
                    <asp:TextBox ID="TxtApellido" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>

            <div class="d-flex flex-column align-items-center">
                <div>
                    <asp:Label ID="LblDireccion" runat="server" Text="Dirección de facturación"></asp:Label>
                </div>
                <div class="mb-3 position-relative">
                    <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

