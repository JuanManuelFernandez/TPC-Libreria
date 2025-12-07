<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Libreria.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex align-items-center justify-content-center">
        <div class="text-center mb-4"">

        <%--[ Mail ]--%>
        <label class="form-label fs-5 fw-bold mb-3" for="MailTxt">Ingrese su Email</label>
        <asp:Label ID="lblMail" runat="server" Text="" CssClass="form-label fw-bold"></asp:Label>
        <asp:TextBox ID="txtMail" runat="server" Placeholder="pepito@gmail.com" TextMode="Email" CssClass="form-control w-100" required="required" AutoPostBack="true" MaxLength="100"></asp:TextBox>

        <%--[ Tema ]--%>
        <label class="form-label fs-5 fw-bold mb-3" for="ConsultaTxt">¿Cual es el motivo de su consulta?</label>
        <asp:TextBox ID="txtTema" runat="server" Placeholder="Ej: No puedo logeuarme, no llega mi pedido..." TextMode="SingleLine" CssClass="form-control w-100" required="required" MaxLength="100"></asp:TextBox>

        <%--[ Descripcion ]--%>
        <label class="form-label fs-5 fw-bold mb-3" for="ConsultaTxt">Explique detalladamente su consulta</label>
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control w-100 mx-auto mb-2" Style="resize: none; height: 135px" MaxLength="500"></asp:TextBox>

        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnEnviar_Click" />

        <%--[ Error Label ]--%>
        <asp:Label ID="lblErrorTema" runat="server" Text="" Visible="false" ForeColor="Red" />
        <asp:Label ID="lblErrorDescripcion" runat="server" Text="" Visible="false" ForeColor="Red" />

        </div>
    </div>

</asp:Content>
