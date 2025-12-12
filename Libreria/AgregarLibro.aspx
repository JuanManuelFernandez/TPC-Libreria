<%@ Page Title="Agregar Libro" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarLibro.aspx.cs" Inherits="Libreria.AgregarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-4">

        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Inicio</a></li>
                <li class="breadcrumb-item active" aria-current="page">Agregar libro</li>
            </ol>
        </nav>

        <h1 class="text-center">Agregar libro</h1>

        <div class="container mt-4">

            <div class="row mb-3">
                <div class="row mb-3">
                    <div class="col-md-6 d-flex">
                        <asp:DropDownList ID="DdlAutor" runat="server" CssClass="form-select form-select-sm me-2"></asp:DropDownList>
                        <asp:Button ID="Button1" runat="server" Text="Agregar" CssClass="btn btn-success btn-sm" CausesValidation="false" OnCommand="Btn_AgregarAutor" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtFecha" runat="server" placeholder="Año-Mes-Día" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqFecha" runat="server"
                            ControlToValidate="TxtFecha"
                            ErrorMessage="La fecha es obligatoria."
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="RegFecha" runat="server"
                            ControlToValidate="TxtFecha"
                            ValidationExpression="^\d{4}-\d{2}-\d{2}$"
                            ErrorMessage="Debe ingresar una fecha válida (YYYY-MM-DD)."
                            CssClass="text-danger d-block" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 d-flex">
                        <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select form-select-sm me-2"></asp:DropDownList>
                        <asp:Button ID="Button2" runat="server" Text="Agregar" CssClass="btn btn-success btn-sm" CausesValidation="false" OnCommand="Btn_AgregarGenero" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtPrecio" runat="server" placeholder="Precio" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPrecio" runat="server"
                            ControlToValidate="TxtPrecio"
                            ErrorMessage="El precio es obligatorio."
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="RegPrecio" runat="server"
                            ControlToValidate="TxtPrecio"
                            ValidationExpression="^\d+(\.\d{1,2})?$"
                            ErrorMessage="Debe ingresar un precio válido (solo números, opcional decimales)."
                            CssClass="text-danger d-block" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 d-flex">
                        <asp:DropDownList ID="DdlEditorial" runat="server" CssClass="form-select form-select-sm me-2"></asp:DropDownList>
                        <asp:Button ID="Button3" runat="server" Text="Agregar" CssClass="btn btn-success btn-sm" CausesValidation="false" OnCommand="Btn_AgregarEditorial" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtPaginas" runat="server" placeholder="Páginas" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPaginas" runat="server"
                            ControlToValidate="TxtPaginas"
                            ErrorMessage="El número de páginas es obligatorio."
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="RegPaginas" runat="server"
                            ControlToValidate="TxtPaginas"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Debe ingresar solo números enteros."
                            CssClass="text-danger d-block" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtTitulo" runat="server" placeholder="Título del libro" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTitulo" runat="server"
                            ControlToValidate="TxtTitulo"
                            ErrorMessage="El título es obligatorio."
                            CssClass="text-danger d-block" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtStock" runat="server" placeholder="Disponibles" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqStock" runat="server"
                            ControlToValidate="TxtStock"
                            ErrorMessage="El stock es obligatorio."
                            CssClass="text-danger d-block" />
                        <asp:RegularExpressionValidator ID="RegStock" runat="server"
                            ControlToValidate="TxtStock"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Debe ingresar solo números enteros."
                            CssClass="text-danger d-block" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtDescrip" runat="server" placeholder="Descripción" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqDescrip" runat="server"
                            ControlToValidate="TxtDescrip"
                            ErrorMessage="La descripción es obligatoria."
                            CssClass="text-danger d-block" />
                    </div>
                    <div class="col-md-6">
                        <asp:FileUpload ID="FilePortada" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="ReqPortada" runat="server"
                            ControlToValidate="FilePortada"
                            ErrorMessage="Debe seleccionar una imagen."
                            CssClass="text-danger d-block" />
                    </div>
                </div>

            <div class="mb-3">
                <asp:Label ID="LblError" runat="server" Text="Label" Visible="false" CssClass="text-danger"></asp:Label>
            </div>

            <div class="row mt-4">
                <div class="col-md-6 mx-auto">
                    <asp:Button ID="Btn_Cargar" runat="server" Text="Agregar libro" CssClass="btn btn-primary w-100" OnCommand="Btn_Agregar" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>