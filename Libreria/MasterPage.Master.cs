using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string UserName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
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