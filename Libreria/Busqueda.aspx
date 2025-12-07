<%@ Page Title="Buscar Libros" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="Libreria.Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .resultados-header {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 30px;
        }

        .filter-sidebar {
            background-color: #fff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            position: sticky;
            top: 20px;
        }

        .filter-section {
            margin-bottom: 25px;
            padding-bottom: 20px;
            border-bottom: 1px solid #e9ecef;
        }

            .filter-section:last-child {
                border-bottom: none;
            }

        .filter-title {
            font-size: 16px;
            font-weight: 700;
            color: #333;
            margin-bottom: 15px;
            text-transform: uppercase;
        }

        .filter-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 8px 0;
            cursor: pointer;
            transition: all 0.2s;
        }

            .filter-item:hover {
                color: #0d6efd;
                padding-left: 5px;
            }

            .filter-item input[type="checkbox"] {
                margin-right: 10px;
                cursor: pointer;
            }

        .filter-label {
            flex-grow: 1;
            cursor: pointer;
        }

        .filter-count {
            color: #6c757d;
            font-size: 14px;
            margin-left: 10px;
        }

        .btn-clear-filters {
            width: 100%;
            margin-top: 15px;
        }

        .active-filters {
            background-color: #e7f3ff;
            padding: 10px 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }

        .filter-tag {
            display: inline-block;
            background-color: #0d6efd;
            color: white;
            padding: 5px 12px;
            border-radius: 15px;
            margin: 5px 5px 5px 0;
            font-size: 13px;
        }

            .filter-tag .remove-filter {
                margin-left: 8px;
                cursor: pointer;
                font-weight: bold;
            }

                .filter-tag .remove-filter:hover {
                    color: #ffeb3b;
                }

        .results-count {
            font-size: 14px;
            color: #6c757d;
            margin-bottom: 15px;
        }

        /* Altura fija para las imágenes de los libros */
        .book-image-container {
            height: 300px;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #f8f9fa;
            overflow: hidden;
        }

        .book-image-container img {
            width: 100%;
            height: 100%;
            object-fit: contain;
            padding: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="resultados-header">
            <h2>Resultados de búsqueda</h2>
            <p class="text-muted mb-0">Mostrando los resultados para "<asp:Literal ID="litTermino" runat="server" />"</p>
        </div>

        <div class="row">
            <!-- Sidebar de Filtros -->
            <div class="col-lg-3 col-md-4">
                <div class="filter-sidebar">
                    <h4 class="mb-4">Filtros</h4>

                    <!-- Filtros Activos -->
                    <asp:Panel ID="pnlFiltrosActivos" runat="server" CssClass="active-filters" Visible="false">
                        <strong>Filtros aplicados:</strong>
                        <div id="activeTags">
                            <asp:Literal ID="litFiltrosActivos" runat="server" />
                        </div>
                        <asp:Button ID="btnLimpiarFiltros" runat="server"
                            CssClass="btn btn-sm btn-outline-danger btn-clear-filters"
                            Text="Limpiar todos los filtros"
                            OnClick="BtnLimpiarFiltros_Click" />
                    </asp:Panel>

                    <!-- Filtro por Categoría -->
                    <div class="filter-section">
                        <h5 class="filter-title">Categoría</h5>
                        <asp:Repeater ID="rptFiltroGeneros" runat="server">
                            <ItemTemplate>
                                <div class="filter-item">
                                    <asp:CheckBox ID="chkGenero" runat="server"
                                        CssClass="filter-checkbox"
                                        AutoPostBack="true"
                                        OnCheckedChanged="FiltroChanged"
                                        Text='<%# Eval("Nombre") %>' />
                                    <span class="filter-count">(<%# Eval("Cantidad") %>)</span>
                                    <asp:HiddenField ID="hdnGeneroId" runat="server" Value='<%# Eval("IdGenero") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Filtro por Editorial -->
                    <div class="filter-section">
                        <h5 class="filter-title">Editorial</h5>
                        <asp:Repeater ID="rptFiltroEditoriales" runat="server">
                            <ItemTemplate>
                                <div class="filter-item">
                                    <asp:CheckBox ID="chkEditorial" runat="server"
                                        CssClass="filter-checkbox"
                                        AutoPostBack="true"
                                        OnCheckedChanged="FiltroChanged"
                                        Text='<%# Eval("Nombre") %>' />
                                    <span class="filter-count">(<%# Eval("Cantidad") %>)</span>
                                    <asp:HiddenField ID="hdnEditorialId" runat="server" Value='<%# Eval("IdEditorial") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Filtro por Autor -->
                    <div class="filter-section">
                        <h5 class="filter-title">Autor</h5>
                        <asp:Repeater ID="rptFiltroAutores" runat="server">
                            <ItemTemplate>
                                <div class="filter-item">
                                    <asp:CheckBox ID="chkAutor" runat="server"
                                        CssClass="filter-checkbox"
                                        AutoPostBack="true"
                                        OnCheckedChanged="FiltroChanged"
                                        Text='<%# Eval("NombreCompleto") %>' />
                                    <span class="filter-count">(<%# Eval("Cantidad") %>)</span>
                                    <asp:HiddenField ID="hdnAutorId" runat="server" Value='<%# Eval("IdAutor") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>

            <!-- Resultados -->
            <div class="col-lg-9 col-md-8">
                <div class="results-count">
                    <asp:Literal ID="litCantidadResultados" runat="server" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false" />

                <asp:Repeater ID="rptLibros" runat="server">
                    <HeaderTemplate>
                        <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-4 col-sm-6 mb-4">
                            <div class="card h-100 shadow-sm">
                                <!-- Contenedor de imagen con altura fija -->
                                <div class="book-image-container">
                                    <img src='assets/portadas/<%# Eval("IdLibro") %>.jpg'
                                        alt='<%# Eval("Titulo") %>'
                                        onerror="this.src='assets/portadas/default.png';" />
                                </div>
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                    <p class="card-text text-muted"><%# Eval("NombreAutor") %></p>
                                    <p class="card-text"><%# Eval("Descripcion") %></p>
                                    <p class="card-text fw-bold text-success mb-3"><%# Eval("Precio", "{0:C}") %></p>
                                    <div class="mt-auto">
                                        <asp:LinkButton ID="btnAgregarCarrito" runat="server"
                                            CssClass="btn btn-success w-100"
                                            CommandArgument='<%# Eval("IdLibro") %>'
                                            OnCommand="Btn_AgregarCarrito"
                                            Text='<i class="bi bi-cart-plus me-2"></i>AGREGAR' />
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnAgregarLista" runat="server"
                                            CssClass="btn btn-primary w-100"
                                            CommandArgument='<%# Eval("IdLibro") %>'
                                            OnCommand="Btn_AgregarLista"
                                            Text='LISTA DE DESEADOS' />
                                    </div>
                                    <asp:Label ID="lblError" runat="server"
                                        Text="Debes iniciar sesión."
                                        Visible="false"
                                        ForeColor="Red"
                                        CssClass="text-center w-100 mt-2 small" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Mensaje si no hay libros -->
                <asp:Panel ID="pnlNoLibros" runat="server" CssClass="text-center" Visible="false">
                    <div class="alert alert-info">
                        <h5>No se encontraron resultados</h5>
                        <p>Intenta ajustar los filtros o realizar una nueva búsqueda.</p>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <!-- Script para remover filtros individuales -->
    <script type="text/javascript">
        function removerFiltro(tipo, id) {
            // Hacer postback al servidor con el filtro a remover
            __doPostBack('removerFiltro_' + tipo + '_' + id, '');
        }
    </script>
</asp:Content>