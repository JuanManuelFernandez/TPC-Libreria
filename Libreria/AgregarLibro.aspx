<%@ Page Title="Agregar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarLibro.aspx.cs" Inherits="Libreria.AgregarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-4">

        <!-- Barra de navegaci�n -->
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Agregar libro</li>
            </ol>
        </nav>

        <h1 class="text-center">Agregar libro</h1>

        <div class="container mt-4">
            <div class="row">

                <!-- Columna izquierda -->
                <div class="col-md-6">

                    <!-- Autor -->
                    <div class="mb-3 d-flex">
                        <asp:DropDownList ID="DdlAutor" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="valAutor" runat="server"
                            ControlToValidate="DdlAutor"
                            InitialValue=""
                            ErrorMessage="Seleccione un autor"
                            CssClass="text-danger ms-2" />
                        <asp:Button ID="Button1" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarAutor" />
                    </div>

                    <!-- Género -->
                    <div class="mb-3 d-flex">
                        <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="valGenero" runat="server"
                            ControlToValidate="DdlGenero"
                            InitialValue=""
                            ErrorMessage="Seleccione un género"
                            CssClass="text-danger ms-2" />
                        <asp:Button ID="Button2" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarGenero" />
                    </div>

                    <!-- Editorial -->
                    <div class="mb-3 d-flex">
                        <asp:DropDownList ID="DdlEditorial" runat="server" CssClass="form-select form-select-lg me-2"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="valEditorial" runat="server"
                            ControlToValidate="DdlEditorial"
                            InitialValue=""
                            ErrorMessage="Seleccione una editorial"
                            CssClass="text-danger ms-2" />
                        <asp:Button ID="Button3" runat="server" Text="Agregar" CssClass="btn btn-success btn-lg w-25" OnCommand="Btn_AgregarEditorial" />
                    </div>

                    <!-- Título -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtTitulo" runat="server" placeholder="Título del libro" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valTituloReq" runat="server"
                            ControlToValidate="TxtTitulo"
                            ErrorMessage="Ingrese un título"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valTituloLen" runat="server"
                            ControlToValidate="TxtTitulo"
                            ValidationExpression="^.{1,50}$"
                            ErrorMessage="El título debe tener hasta 50 caracteres"
                            CssClass="text-danger d-block" />
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtDescrip" runat="server" placeholder="Descripción" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valDescripReq" runat="server"
                            ControlToValidate="TxtDescrip"
                            ErrorMessage="Ingrese una descripción"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valDescripLen" runat="server"
                            ControlToValidate="TxtDescrip"
                            ValidationExpression="^.{1,500}$"
                            ErrorMessage="La descripción debe tener hasta 500 caracteres"
                            CssClass="text-danger d-block" />
                    </div>

                </div>

                <!-- Columna derecha -->
                <div class="col-md-6">

                    <!-- Fecha -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtFecha" runat="server" placeholder="Año-Mes-Día" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valFechaReq" runat="server"
                            ControlToValidate="TxtFecha"
                            ErrorMessage="Ingrese una fecha"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valFechaFmt" runat="server"
                            ControlToValidate="TxtFecha"
                            ValidationExpression="^\d{4}-\d{2}-\d{2}$"
                            ErrorMessage="Formato válido: YYYY-MM-DD"
                            CssClass="text-danger d-block" />
                    </div>

                    <!-- Precio -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtPrecio" runat="server" placeholder="Precio" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valPrecioReq" runat="server"
                            ControlToValidate="TxtPrecio"
                            ErrorMessage="Ingrese un precio"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valPrecioNum" runat="server"
                            ControlToValidate="TxtPrecio"
                            ValidationExpression="^\d+(\.\d{1,2})?$"
                            ErrorMessage="Ingrese un precio válido"
                            CssClass="text-danger d-block" />
                    </div>

                    <!-- Páginas -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtPaginas" runat="server" placeholder="Páginas" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valPagReq" runat="server"
                            ControlToValidate="TxtPaginas"
                            ErrorMessage="Ingrese el número de páginas"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valPagNum" runat="server"
                            ControlToValidate="TxtPaginas"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Ingrese un número válido"
                            CssClass="text-danger d-block" />
                    </div>

                    <!-- Stock -->
                    <div class="mb-3">
                        <asp:TextBox ID="TxtStock" runat="server" placeholder="Disponibles" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valStockReq" runat="server"
                            ControlToValidate="TxtStock"
                            ErrorMessage="Ingrese el stock"
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="valStockNum" runat="server"
                            ControlToValidate="TxtStock"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Solo números enteros"
                            CssClass="text-danger d-block" />
                    </div>

                    <!-- Portada -->
                    <div class="mb-3">
                        <asp:FileUpload ID="FilePortada" runat="server" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="valPortada" runat="server"
                            ControlToValidate="FilePortada"
                            ErrorMessage="Debe seleccionar una imagen"
                            CssClass="text-danger d-block" />
                    </div>

                </div>
            </div>

            <%-- Error --%>
            <div class="mb-3">
                <asp:Label ID="LblError" runat="server" Text="Label" Visible="false" CssClass="text-danger"></asp:Label>
            </div>

            <!-- Botón agregar libro -->
            <div class="row mt-4">
                <div class="col-md-6 mx-auto">
                    <asp:Button ID="Btn_Cargar" runat="server" Text="Agregar libro" CssClass="btn btn-primary btn-lg w-100" OnCommand="Btn_Agregar" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
