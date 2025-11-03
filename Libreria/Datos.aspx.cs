using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Datos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var dataCli = new AccesoClientes();
            Usuario user = (Usuario)Session["usuario"];

            switch (user.TipoUsuario)
            {
                // Admin
                default:
                    txtNombre.Text = "Admin";
                    txtNombre.Enabled = false;

                    txtMail.Text = user.Mail;

                    txtTelefono.Text = "N/A";
                    txtTelefono.Enabled = false;
                    break;
                // Cliente
                case TipoUsuario.Cliente:
                    txtNombre.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Nombre;
                    txtMail.Text = user.Mail;
                    txtTelefono.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Telefono;
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente nuevoCli = new Cliente();
            var datosCli = new AccesoClientes();

            Usuario nuevoUsr = new Usuario();
            var datosUsr = new AccesoUsuario();
            Usuario user = (Usuario)Session["usuario"];

            //if (datosUsr.Listar().Find(x => x.Mail == txtMail.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            //{
            //    lblErrorMail.Visible = true;
            //    lblErrorMail.Text = "Este Mail no esta disponible";
            //}

            nuevoCli.Nombre = txtNombre.Text;
            //cli.Apellido = txtApellido.Text;
            nuevoCli.Telefono = txtTelefono.Text;

            nuevoUsr.TipoUsuario = user.TipoUsuario;
            nuevoUsr.Mail = txtMail.Text;

            datosCli.ModificarCliente(nuevoCli);
            datosUsr.ModificarUsuario(nuevoUsr);
        }
    }
}