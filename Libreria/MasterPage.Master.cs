using System;

namespace Libreria
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                btnLogOut.Visible = true;
            }
        }
        protected void btnInicio_click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void btnAccount_click (object sender, EventArgs e)
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
        protected void btnCuenta_click(object sender, EventArgs e) 
        {
            Response.Redirect("Contacto.aspx");
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}