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
                var accesoUsuario = new AccesoUsuario();
                var accesoCliente = new AccesoClientes();

                //Mail
                var mailExistente = accesoUsuario.Listar().Find(x => x.Mail == MailTxt.Text);
                //DNI
                var dniExistente = accesoCliente.Listar().Find(x => x.Dni == DNITxt.Text);
                //Telefono
                var telefonoExistente = accesoCliente.Listar().Find(x => x.Telefono == TelefonoTxt.Text);

                if(mailExistente != null)
                {
                    LblError.Text = "El mail ingresado ya esta registrado.";
                    LblError.Visible = true;
                    return;
                }
                if(dniExistente != null)
                {
                    LblError.Text = "El DNI ingresado ya esta registado.";
                    LblError.Visible = true;
                    return;
                }
                if(telefonoExistente != null)
                {
                    LblError.Text = "El numero de telefono ingresado ya esta registado.";
                    LblError.Visible = true;
                    return;
                }

                CargarUsuario();
                CargarCliente();

                var accesousuario = new AccesoUsuario();
                try
                {
                    var usuario = accesousuario.Listar().Find(x =>
                    x.Mail == MailTxt.Text &&
                    x.Clave == ClaveTxt.Text) ?? new Usuario();

                    if (accesousuario.Loguear(usuario))
                    {
                        Session.Add("usuario", usuario);
                        Response.Redirect("Default.aspx");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                DNITxt.Text = " ";
                NombreTxt.Text = " ";
                ApellidoTxt.Text = " ";
                TelefonoTxt.Text = " ";
                MailTxt.Text = " ";
                ClaveTxt.Text = " ";
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
            }
            dataUser.AgregarUsuario(nuevoUsuario);
        }
        
    }
}