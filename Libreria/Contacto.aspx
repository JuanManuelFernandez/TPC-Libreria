<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Libreria.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">

        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item"><a href="AgregarLibro.aspx">Agregar Libro</a></li>
                <li class="breadcrumb-item active" aria-current="page">Contacto</li>
            </ol>
        </nav>

        <!-- Centrado del formulario -->
        <div class="row justify-content-center">
            <div class="col-md-6 text-center">

                <%-- Mail --%>
                <label class="form-label fs-5 fw-bold mb-3" for="MailTxt">Ingrese su Email</label>
                <asp:TextBox ID="txtMail" runat="server"
                    Placeholder="tumail@gmail.com"
                    TextMode="Email"
                    CssClass="form-control mb-3"
                    required="required"
                    MaxLength="100" />

                <%-- Tema --%>
                <label class="form-label fs-5 fw-bold mb-3">¿Cuál es el motivo de su consulta?</label>
                <asp:TextBox ID="txtTema" runat="server"
                    Placeholder="Ej: No puedo loguearme, no llega mi pedido..."
                    CssClass="form-control mb-3"
                    MaxLength="100" />

                <%-- Descripción --%>
                <label class="form-label fs-5 fw-bold mb-3">Explique detalladamente su consulta</label>
                <asp:TextBox ID="txtDescripcion" runat="server"
                    TextMode="MultiLine"
                    CssClass="form-control mb-3"
                    Style="resize: none; height: 135px;"
                    MaxLength="500" />

                <asp:Button ID="btnEnviar" runat="server"
                    Text="Enviar"
                    CssClass="btn btn-primary btn-lg mb-3"
                    OnClick="BtnEnviar_Click" />

                <%-- Errores --%>
                <asp:Label ID="lblErrorTema" runat="server" ForeColor="Red" Visible="false" />
                <asp:Label ID="lblErrorDescripcion" runat="server" ForeColor="Red" Visible="false" />

            </div>
        </div>

    </div>

</asp:Content>
