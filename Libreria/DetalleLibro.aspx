<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleLibro.aspx.cs" Inherits="Libreria.DetalleLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row">
            <!-- Columna izquierda: portada -->
            <div class="col-md-4 text-center">
                <asp:Image ID="imgLibro" runat="server" CssClass="img-fluid rounded shadow" Style="max-width:300px;" />
            </div>

            <!-- Columna derecha: título y descripción -->
            <div class="col-md-8">
                <h2 class="fw-bold"><asp:Label ID="lblTitulo" runat="server" /></h2>
                <p class="text-muted"><asp:Label ID="lblDescripcion" runat="server" /></p>
                <p><asp:Label ID="lblAutor" runat="server" CssClass="fw-semibold" /></p>
                <p><asp:Label ID="lblGenero" runat="server" CssClass="fw-semibold" /></p>

                <div class="row mt-3">
                    <div class="col-md-3">
                        <asp:Button ID="btnAgregarCarrito" runat="server" CssClass="btn btn-success w-70" Text="Agregar al carrito" OnCommand="Btn_AgregarCarrito"/>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnListaDeseados" runat="server" CssClass="btn btn-primary w-70" Text="Lista de deseados" OnCommand="Btn_AgregarLista"/>
                    </div>
                    <asp:Label ID="LblAgregado" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>

                <div class="mt-4">
                    <asp:Label ID="LblOpinion" runat="server" Text="Escribe tu reseña:"></asp:Label>
                    <asp:TextBox ID="txtOpinion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />

                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary mt-2" Text="Enviar reseña" OnCommand="Btn_Guardar_Opinion" />
                </div>

                <div class="mt-3">
                    <asp:Label ID="LblPuntaje" runat="server" Text="Puntaje:"></asp:Label>
                    <asp:DropDownList ID="ddlPuntaje" runat="server" CssClass="form-select" Style="max-width:120px;">
                        <asp:ListItem Text="★" Value="1"></asp:ListItem>
                        <asp:ListItem Text="★★" Value="2"></asp:ListItem>
                        <asp:ListItem Text="★★★" Value="3"></asp:ListItem>
                        <asp:ListItem Text="★★★★" Value="4"></asp:ListItem>
                        <asp:ListItem Text="★★★★★" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="LblMensaje" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
        <h4 class="mt-4">¿Que opinan los demas?</h4>
            <asp:Repeater ID="rptOpiniones" runat="server" OnItemCommand="rptOpiniones_ItemCommand" OnItemDataBound="Ocultar_BtnEliminar">
                <ItemTemplate>
                    <div class="card mb-2">
                        <div class="card-body">
                            <asp:LinkButton ID="btnEliminar" runat="server" 
                                CommandName="Eliminar" 
                                CommandArgument='<%# Eval("IDReseña") %>'
                                CssClass="btn btn-sm btn-outline-danger float-end">
                                <img src="assets/trash.png" alt="Eliminar" style="width:20px; height:20px;" />
                        </asp:LinkButton>

                            <p class="card-text"><%# Eval("Texto") %></p>
                            <small class="text-muted">
                                <%# new string('★', Convert.ToInt32(Eval("Puntaje"))) %> 
                                - <%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %>
                            </small>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </div>



</asp:Content>
