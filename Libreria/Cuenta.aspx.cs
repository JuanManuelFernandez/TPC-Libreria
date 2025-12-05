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
                    // Admin
                    default:
                        UserName = "Admin";
                        UserMail = user.Mail;
                        UserPhone = "N/A";
                        break;
                    // Cliente
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
    }
}