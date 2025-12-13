<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Libreria.MisCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <h1 class="text-center mb-4">
            <asp:Label ID="lblTitulo" runat="server"/>
        </h1>

        <div class="text-center">
             <asp:Label ID="LblMensaje" runat="server" Text="Label" Visible = "false"></asp:Label>
        </div>
       

        <asp:Repeater ID="rptCompras" runat="server">
            <ItemTemplate>
                <div class="card mb-3 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title fw-bold">
                            Compra Nº <%# Eval("IDCompra") %>
                        </h5>
                        <p class="card-text">
                            Fecha: <%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %><br />
                            Dirección de facturación: <%# Eval("DFacturacion") %><br />
                            Localidad: <%# Eval("Localidad") %><br />
                            Código Postal: <%# Eval("Codigo") %><br />
                            Total: $<%# Eval("Total") %>
                        </p>
                        <asp:Button ID="btnDetalle" runat="server" Text="Ver estado" CssClass="btn btn-primary" CommandName="VerDetalle" 
                            CommandArgument='<%# Eval("IDCompra") %>' OnCommand="Btn_VerDetalle"/>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rptComprasAdmin" runat="server" Visible="false">
            <ItemTemplate>
                <div class="card mb-3 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title fw-bold">
                            Compra Nº <%# Eval("IDCompra") %>
                        </h5>
                        <p class="card-text">
                            Fecha: <%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %><br />
                            Cliente: <%# Eval("Nombre") %> <%# Eval("Apellido") %><br />
                            Mail: <%# Eval("Mail") %><br />
                            IDCliente: <%# Eval("IDCliente") %><br />
                            Dirección de facturación: <%# Eval("DFacturacion") %><br />
                            Localidad: <%# Eval("Localidad") %><br />
                            Código Postal: <%# Eval("Codigo") %><br />
                            Total: $<%# Eval("Total") %>
                        </p>
                         <asp:Button ID="btnDetalle" runat="server" Text="Ver estado" CssClass="btn btn-primary" CommandName="VerDetalle" 
                            CommandArgument='<%# Eval("IDCompra") %>' OnCommand="Btn_VerDetalle"/>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
