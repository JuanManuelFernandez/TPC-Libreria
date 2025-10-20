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
                    // admin
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
        protected void btnInicio_click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void btnCuenta_click (object sender, EventArgs e)
        {
            Response.Redirect("Cuenta.aspx");
        }
        protected void btnDeseados_click(object sender, EventArgs e)
        {
            Response.Redirect("Wishlist.aspx");
        }
        protected void btnCarrito_click(object sender, EventArgs e)
        {
            Response.Redirect("Carrito.aspx");
        }
        protected void btnContacto_click(object sender, EventArgs e) 
        {
            Response.Redirect("Contacto.aspx");
        }

        protected void btnLogout_click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}