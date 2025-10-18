using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void BtnRegistro_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                CargarUsuario();
                CargarCliente();
                Response.Redirect("Default.aspx");
            }
        }
        public void CargarCliente()
        {
            var dataCliente = new AccesoClientes();
            var nuevoCliente = new Cliente();
            {
                nuevoCliente.Dni = DNITxt.Text;
                nuevoCliente.Nombre = NombreTxt.Text;
                nuevoCliente.Apellido = ApellidoTxt.Text;
                nuevoCliente.Telefono = TelefonoTxt.Text;

                DNITxt.Text = " ";
                NombreTxt.Text = " ";
                ApellidoTxt.Text = " ";
                TelefonoTxt.Text = " ";
            }
            dataCliente.AgregarCliente(nuevoCliente);
        }
        public void CargarUsuario()
        {
            var dataUser = new AccesoUsuario();
            var nuevoUsuario = new Usuario();
            {
                nuevoUsuario.Mail = MailTxt.Text;
                nuevoUsuario.Clave = ClaveTxt.Text;

                MailTxt.Text = " ";
                ClaveTxt.Text = " ";
            }
            dataUser.AgregarUsuario(nuevoUsuario);
        }
        
    }
}