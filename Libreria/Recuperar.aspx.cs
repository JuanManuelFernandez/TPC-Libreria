using Dominio;
using Negocio;
using System;

using System.Security.Cryptography;

namespace Libreria
{
    public partial class Recuperar : System.Web.UI.Page
    {
        public bool MailEnviado { get; set; }
        public string Mail { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SendResetLinkButton_Click(object sender, EventArgs e) { }
        protected void BtnEnviarMail_Click(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();
            var datosCli = new AccesoClientes();

            AccesoUsuario datosUsr = new AccesoUsuario();
            EmailService emailService = new EmailService();

            bool MailExiste = datosUsr.MailExiste(txtMail.Text);

            if (MailExiste)
            {
                cli.Nombre = datosUsr.BuscarClientePorMail(txtMail.Text).Nombre;

                Random random = new Random();
                int token = random.Next(1, 100000); // Token de 6 digitos
                string formattedToken = token.ToString("D6"); // Se asegura de obtener los ceros a la izquierda

                string Descripcion = $@"
                    <html><body style='font-family: Arial, sans-serif; background:#f4f6f8; margin:0; padding:20px;'>
                      <div style='max-width:600px; margin:auto; background:#fff; padding:30px; border-radius:8px;'>
                        <h2 style='color:#333;'Cambio de contraseña</h2>

                        <p style='font-size:16px; color:#555;'>Hola {cli.Nombre}. Recibimos una solicitud para restablecer tu contraseña</p>
                        <p style='font-size:16px; color:#555;'>Usa el codigo de abajo para continuar con el proceso.</p>

                        <p style='text-align:center;'>
                          <p style='background:#0d6efd; color:#fff; padding:12px 24px; border-radius:6px; text-decoration:none; font-weight:bold;'>{formattedToken}</p>
                        </p>

                        <p style='font-size:13px; color:#777;'>Si vos no hiciste este pedido, puedes ignorar este email.</p>
                        <p style='font-size:12px; color:#999; margin-top:30px;'>&copy; {DateTime.UtcNow.Year} Libreria UTN. Todos los derechos reservados.</p>
                      </div>
                    </body></html>";

                emailService.ArmarCorreo(txtMail.Text, "Cambio de contraseña", Descripcion);

                bool MailEnviado = false;
                try
                {
                    emailService.EnviarEmail();
                    MailEnviado = true;
                    Session.Add("MailEnviado", MailEnviado);
                }
                catch (Exception ex)
                {
                    throw ex;
                    //Session.Add("error", ex);
                    //Response.Redirect("error.aspx");
                }

                lblMensaje.Visible = true;
                if (MailEnviado)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Email enviado con exito. Revisa tu casilla de correo.";
                }
                else
                {
                    // Este aviso puede ser una vulnerabilidad, ya que da cuenta d eque mails validos existen
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No encontramos ninguna cuenta registrada con este email. Intentalo nuevamente chequeando que esté bien escrito.";
                }

            }
        }
        protected void BtnEnviarToken_Click(object sender, EventArgs e)
        {
            //if (formattedToken) { }
        }
    }
}