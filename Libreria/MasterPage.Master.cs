using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string UserName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }

            // Marcar el filtro activo en cada carga de página
            MarcarFiltroActivo();

            if (Session["usuario"] != null)
            {
                var dataCli = new AccesoClientes();
                Usuario user = (Usuario)Session["usuario"];
                switch (user.TipoUsuario)
                {
                    // Admin
                    default:
                        UserName = "Admin";
                        btnDeseados.Visible = false;
                        btnCarrito.Visible = false;
                        btnContacto.Visible = false;
                        break;
                    // Cliente
                    case TipoUsuario.Cliente:
                        UserName = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Nombre;
                        break;
                }
                
            }
        }

        private void CargarCategorias()
        {
            try
            {
                AccesoGeneros negocioGeneros = new AccesoGeneros();
                List<Genero> categorias = negocioGeneros.Listar();

                rptCategorias.DataSource = categorias;
                rptCategorias.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar categorias: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        private void MarcarFiltroActivo()
        {
            // Obtener parámetros de la URL
            string generoId = Request.QueryString["genero"];
            string paginaActual = System.IO.Path.GetFileName(Request.Path);

            // Remover clase active de todos los botones primero
            btnFiltroTodos.CssClass = "category-filter-btn";

            // Si estamos en Default.aspx o no hay filtro, marcar "TODOS"
            if (paginaActual == "Default.aspx" || (string.IsNullOrEmpty(generoId) && Request.QueryString["q"] == null))
            {
                btnFiltroTodos.CssClass = "category-filter-btn active";
            }
            // Si hay un filtro de género, marcar ese botón
            else if (!string.IsNullOrEmpty(generoId))
            {
                int idGeneroActivo = int.Parse(generoId);

                // Iterar por los items del repeater para encontrar el botón correcto
                foreach (RepeaterItem item in rptCategorias.Items)
                {
                    LinkButton btnCategoria = (LinkButton)item.FindControl("btnCategoria");
                    if (btnCategoria != null)
                    {
                        if (btnCategoria.CommandArgument == idGeneroActivo.ToString())
                        {
                            btnCategoria.CssClass = "category-filter-btn active";
                        }
                        else
                        {
                            btnCategoria.CssClass = "category-filter-btn";
                        }
                    }
                }
            }
        }

        protected void BtnFiltroCategoria_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            int idGenero = int.Parse(btn.CommandArgument);

            // Si es "TODOS" (idGenero = 0)
            if (idGenero == 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Redirigir a página de búsqueda con filtro de categoría
                Response.Redirect($"Busqueda.aspx?genero={idGenero}");
            }
        }

        protected void BtnBuscar_click(object sender, EventArgs e)
        {
            string termino = txtBusqueda.Text.Trim();
            if (!string.IsNullOrEmpty(termino))
            {
                Response.Redirect($"Busqueda.aspx?q={Server.UrlEncode(termino)}");
            }
        }

        protected void BtnInicio_click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BtnCuenta_click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            Response.Redirect("Cuenta.aspx");
        }

        protected void BtnDeseados_click(object sender, EventArgs e)
        {
            Response.Redirect("Deseados.aspx");
        }

        protected void BtnCarrito_click(object sender, EventArgs e)
        {
            Response.Redirect("Carrito.aspx");
        }

        protected void BtnContacto_click(object sender, EventArgs e)
        {
            Response.Redirect("Contacto.aspx");
        }

        protected void BtnLogout_click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
        protected void BtnMisCompras_click(object sender, EventArgs e)
        {
            Response.Redirect("MisCompras.aspx");
        }
    }
}