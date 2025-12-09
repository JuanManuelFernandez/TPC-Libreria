using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    var dataCli = new AccesoClientes();
                    Usuario user = (Usuario)Session["usuario"];

                    switch (user.TipoUsuario)
                    {
                        case TipoUsuario.Admin:
                            TxtNombre.Text = "Admin";
                            TxtApellido.Text = "";
                            TxtMail.Text = user.Mail;
                            TxtTelefono.Text = "N/A";
                            btnEliminarCuenta.Visible = false;
                            break;

                        case TipoUsuario.Cliente:
                            var cli = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);

                            TxtNombre.Text = cli.Nombre;
                            TxtApellido.Text = cli.Apellido;
                            TxtMail.Text = user.Mail;
                            TxtTelefono.Text = cli.Telefono;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }


        // Muestra mensaje de la confirmacion de eliminacion
        protected void BtnEliminarCuenta_Click(object sender, EventArgs e)
        {
            // Oculto datos personales y muestro confirmacion
            datosPersonales.Visible = false;
            carrito.Visible = false;
            confirmacion.Visible = true;
        }

        protected void BtnSi_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                var accesoUsuario = new AccesoUsuario();
                accesoUsuario.EliminarUsuarioId(user.IdUsuario);

                // Limpiar la sesión y vuelvo a Default
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        protected void BtnNo_Click(object sender, EventArgs e)
        {
            // Muestro datos personales y oculto confirmacion
            datosPersonales.Visible = true;
            carrito.Visible = true;
            confirmacion.Visible = false;
        }
    }
}
