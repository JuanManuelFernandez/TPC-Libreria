using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

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

            if (Session["usuario"] != null)
            {
                var dataCli = new AccesoClientes();
                Usuario user = (Usuario)Session["usuario"];
                switch (user.TipoUsuario)
                {
                    // Admin
                    default:
                        UserName = "Admin";
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
                // Manejar error si es necesario
            }
        }

        protected void BtnFiltroCategoria_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.LinkButton)sender;
            int idGenero = int.Parse(btn.CommandArgument);

            // Remover clase active de todos los botones
            btnFiltroTodos.CssClass = "category-filter-btn";

            // Si es "TODOS" (idGenero = 0)
            if (idGenero == 0)
            {
                btnFiltroTodos.CssClass = "category-filter-btn active";
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
    }
}