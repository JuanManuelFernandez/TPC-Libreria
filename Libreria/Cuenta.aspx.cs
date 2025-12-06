using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Cuenta : System.Web.UI.Page
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                var dataCli = new AccesoClientes();
                Usuario user = (Usuario)Session["usuario"];

                switch (user.TipoUsuario)
                {
                    default:
                        UserName = "Admin";
                        UserMail = user.Mail;
                        UserPhone = "N/A";
                        break;

                    case TipoUsuario.Cliente:
                        UserName = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Nombre;
                        UserSurname = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Apellido;
                        UserMail = user.Mail;
                        UserPhone = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Telefono;
                        break;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        // Muestra mensaje de la confirmacion de eliminacion
        protected void BtnEliminarCuenta_Click(object sender, EventArgs e)
        {
            // Oculto datos personales y muestro confirmacion
            datosPersonales.Visible = false;
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
            confirmacion.Visible = false;
        }
    }
}
