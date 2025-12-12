<%@ Page Title="Editar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditarLibro.aspx.cs" Inherits="Libreria.EditarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-4">

        <!-- Barra de navegación -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Editar libro</li>
            </ol>
        </nav>

        <h1 class="text-center">Editar libro
        </h1>

        <div class="container mt-4">
            <div class="row">
                <div class="col-md-6">

                    <!-- Autor -->
                    <div class="mb-3">
                        <asp:Label ID="LblAutor" runat="server" Text="Autor"></asp:Label>
                        <asp:DropDownList ID="DdlAutor" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                    </div>

                    <!-- Género -->
                    <div class="mb-3">
                        <asp:Label ID="LblGenero" runat="server" Text="Género"></asp:Label>
                        <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                    </div>

                    <!-- Editorial -->
                    <div class="mb-3">
                        <asp:Label ID="LblEditorial" runat="server" Text="Editorial"></asp:Label>
                        <asp:DropDownList ID="DdlEditorial" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                    </div>

                    <!-- Título -->
                    <div class="mb-3">
                        <asp:Label ID="LblTitulo" runat="server" Text="Título"></asp:Label>
                        <asp:TextBox ID="TxtTitulo" runat="server" CssClass="form-control form-control-lg" placeholder="Título del libro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTitulo" runat="server"
                            ControlToValidate="TxtTitulo"
                            CssClass="text-danger"
                            ErrorMessage="El título es obligatorio." />
                        <asp:RegularExpressionValidator ID="RegTitulo" runat="server"
                            ControlToValidate="TxtTitulo"
                            ValidationExpression="^.{2,50}$"
                            CssClass="text-danger"
                            ErrorMessage="El título debe tener entre 2 y 50 caracteres." />
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <asp:Label ID="LblDescrip" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="TxtDescrip" runat="server" CssClass="form-control form-control-lg" placeholder="Descripción"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqDescrip" runat="server"
                            ControlToValidate="TxtDescrip"
                            CssClass="text-danger"
                            ErrorMessage="La descripción es obligatoria." />
                        <asp:RegularExpressionValidator ID="RegDescrip" runat="server"
                            ControlToValidate="TxtDescrip"
                            ValidationExpression="^.{5,500}$"
                            CssClass="text-danger"
                            ErrorMessage="La descripción debe tener entre 5 y 500 caracteres." />
                    </div>

                </div>

                <!-- Columna derecha -->
                <div class="col-md-6">

                    <!-- Fecha -->
                    <div class="mb-3">
                        <asp:Label ID="LblFecha" runat="server" Text="Fecha de publicación"></asp:Label>
                        <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control form-control-lg" placeholder="Año-Mes-Día"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqFecha" runat="server"
                            ControlToValidate="TxtFecha"
                            CssClass="text-danger"
                            ErrorMessage="La fecha es obligatoria." />
                        <asp:RegularExpressionValidator ID="RegFecha" runat="server"
                            ControlToValidate="TxtFecha"
                            ValidationExpression="^\d{4}-\d{2}-\d{2}$"
                            CssClass="text-danger"
                            ErrorMessage="Debe ingresar una fecha válida (YYYY-MM-DD)." />
                    </div>

                    <!-- Precio -->
                    <div class="mb-3">
                        <asp:Label ID="LblPrecio" runat="server" Text="Precio"></asp:Label>
                        <asp:TextBox ID="TxtPrecio" runat="server" CssClass="form-control form-control-lg" placeholder="Precio"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPrecio" runat="server"
                            ControlToValidate="TxtPrecio"
                            CssClass="text-danger"
                            ErrorMessage="El precio es obligatorio." />
                        <asp:RegularExpressionValidator ID="RegPrecio" runat="server"
                            ControlToValidate="TxtPrecio"
                            ValidationExpression="^\d+([.,]\d{1,2})?$"
                            CssClass="text-danger"
                            ErrorMessage="Debe ingresar un precio válido." />
                    </div>

                    <!-- Páginas -->
                    <div class="mb-3">
                        <asp:Label ID="LblPaginas" runat="server" Text="Número de páginas"></asp:Label>
                        <asp:TextBox ID="TxtPaginas" runat="server" CssClass="form-control form-control-lg" placeholder="Páginas"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPaginas" runat="server"
                            ControlToValidate="TxtPaginas"
                            CssClass="text-danger"
                            ErrorMessage="El número de páginas es obligatorio." />
                        <asp:RegularExpressionValidator ID="RegPaginas" runat="server"
                            ControlToValidate="TxtPaginas"
                            ValidationExpression="^\d{1,5}$"
                            CssClass="text-danger"
                            ErrorMessage="Debe ingresar un número válido de páginas." />
                    </div>

                    <!-- Stock -->
                    <div class="mb-3">
                        <asp:Label ID="LblStock" runat="server" Text="Stock disponible"></asp:Label>
                        <asp:TextBox ID="TxtStock" runat="server" CssClass="form-control form-control-lg" placeholder="Disponibles"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqStock" runat="server"
                            ControlToValidate="TxtStock"
                            CssClass="text-danger"
                            ErrorMessage="El stock es obligatorio." />
                        <asp:RegularExpressionValidator ID="RegStock" runat="server"
                            ControlToValidate="TxtStock"
                            ValidationExpression="^\d{1,5}$"
                            CssClass="text-danger"
                            ErrorMessage="Debe ingresar un número válido de stock." />
                    </div>

                </div>
            </div>

            <%-- Error --%>
            <div class="mb-3">
                <asp:Label ID="LblError" runat="server" Text="Label" Visible="false" CssClass="text-danger"></asp:Label>
            </div>

            <%-- Boton editar libro --%>
            <div class="text-center mt-4">
                <asp:Button ID="Btn_Guardar" runat="server" Text="Guardar cambios" CssClass="btn btn-primary"
                    OnCommand="Btn_GuardarCambios" />
            </div>
        </div>

    </div>

</asp:Content>
