using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Datos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dataCli = new AccesoClientes();
                Usuario user = (Usuario)Session["usuario"];

                switch (user.TipoUsuario)
                {
                    case TipoUsuario.Admin:
                        txtNombre.Text = "Admin";
                        txtNombre.Enabled = false;

                        txtApellido.Text = "N/A";
                        txtApellido.Enabled = false;

                        txtMail.Text = user.Mail;

                        txtTelefono.Text = "N/A";
                        txtTelefono.Enabled = false;
                        break;

                    case TipoUsuario.Cliente:
                        txtNombre.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Nombre;
                        txtApellido.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Apellido;
                        txtMail.Text = user.Mail;
                        txtTelefono.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario).Telefono;
                        break;
                }
            }
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var datosCli = new AccesoClientes();
            var datosUsr = new AccesoUsuario();
            Usuario user = (Usuario)Session["usuario"];

            // Validar que el mail no esté en uso por otro usuario
            var usuarioExistente = datosUsr.Listar().Find(
                x => x.Mail == txtMail.Text && x.IdUsuario != user.IdUsuario);

            if (usuarioExistente != null)
            {
                lblErrorMail.Visible = true;
                lblErrorMail.Text = "Este Mail no está disponible";
                return; // Detener la ejecución si el mail ya existe
            }

            // Modificar Usuario - usar el usuario de la sesión y lo actualizo
            user.Mail = txtMail.Text;
            datosUsr.Modificar(user);

            // Modificar Cliente - solo si es un cliente
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                Cliente cliente = datosCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
                if (cliente != null)
                {
                    cliente.Nombre = txtNombre.Text;
                    cliente.Apellido = txtApellido.Text;
                    cliente.Telefono = txtTelefono.Text;
                    datosCli.Modificar(cliente);
                }
            }

            // Actualizo la sesión
            Session["usuario"] = user;

            // Recargo la pagina
            Response.Redirect("Cuenta.aspx");
        }
    }
}