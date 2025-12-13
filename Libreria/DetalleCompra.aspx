<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleCompra.aspx.cs" Inherits="Libreria.DetalleCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Estado de compra</h1>

    <div class="container my-5">
        <div class="card shadow-sm mt-4">
            <div class="card-body">
                <h5 class="card-title">
                    Compra Nº <asp:Label ID="lblIDCompra" runat="server" />
                </h5>
                <p class="card-text">
                    <asp:Label ID="lblCliente" runat="server" />
                </p>
                <p class="card-text">
                    Estado actual: <asp:Literal ID="litEstado" runat="server" />
                </p>

                <asp:Button ID="btnSiguienteEtapa" runat="server" 
                    Text="Pasar a siguiente etapa" 
                    CssClass="btn btn-primary"
                    OnClick="BtnSiguienteEtapa_Click" />
            </div>
        </div>
    </div>
</asp:Content>
