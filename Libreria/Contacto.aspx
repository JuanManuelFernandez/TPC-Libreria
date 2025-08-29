<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Libreria.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-3">
        <asp:Label ID="lblMail" runat="server" Text="Ingresa tu email:" CssClass="form-label fw-bold"></asp:Label>
        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control w-100" Enabled="false" required="required" AutoPostBack="true" TextMode="Email" MaxLength="100"></asp:TextBox>
    </div>

    <div class="mb-3">
        <asp:Label ID="lblDescripcion" runat="server" Text="Ingrese su consulta a continuacion:" CssClass="form-label fw-bold text-center"> </asp:Label>
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control w-50 mx-auto mb-2" Style="resize: none; height: 135px" MaxLength="500"></asp:TextBox>
    </div>

    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnEnviar_Click" />
        </section>
    </div>

    <div class="text-center mt-1">
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red" />
    </div>

</asp:Content>
