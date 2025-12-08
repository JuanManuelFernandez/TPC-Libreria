using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Recuperar : System.Web.UI.Page
    {
        public bool MailEnviado
        {
            get => Session["MailEnviado"] != null && (bool)Session["MailEnviado"];
            set => Session["MailEnviado"] = value;
        }

        public string TokenGenerado
        {
            get => Session["TokenRecuperacion"] != null ? Session["TokenRecuperacion"].ToString() : string.Empty;
            set => Session["TokenRecuperacion"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarSesion();
            }
        }

        private void LimpiarSesion()
        {
            Session["MailEnviado"] = false;
            Session["TokenRecuperacion"] = null;
            Session["MailRecuperacion"] = null;
            Session["TokenExpiracion"] = null;
        }

        private string GenerarToken()
        {
            Random random = new Random();
            int token = random.Next(100000, 999999);
            return token.ToString();
        }

        private string GenerarEmailHTML(string nombreCliente, string token, bool esReenvio = false)
        {
            string tituloAccion = esReenvio ? "Has solicitado un nuevo código de verificación" : "Recibimos una solicitud para restablecer tu contraseña";

            return $@"
                <html><body style='font-family: Arial, sans-serif; background:#f4f6f8; margin:0; padding:20px;'>
                  <div style='max-width:600px; margin:auto; background:#fff; padding:30px; border-radius:8px;'>
                    <h2 style='color:#333;'>Cambio de contraseña</h2>

                    <p style='font-size:16px; color:#555;'>Hola {nombreCliente}. {tituloAccion}</p>
                    <p style='font-size:16px; color:#555;'>Usa el código de abajo para continuar con el proceso.</p>

                    <p style='text-align:center;'>
                      <span style='background:#0d6efd; color:#fff; padding:12px 24px; border-radius:6px; font-weight:bold; font-size:24px; display:inline-block;'>{token}</span>
                    </p>

                    <p style='font-size:13px; color:#777;'>Si vos no hiciste este pedido, puedes ignorar este email.</p>
                    <p style='font-size:13px; color:#777;'>Este código expira en 5 minutos.</p>
                    <p style='font-size:12px; color:#999; margin-top:30px;'>&copy; {DateTime.UtcNow.Year} Libreria UTN. Todos los derechos reservados.</p>
                  </div>
                </body></html>";
        }

        private bool EnviarCodigoRecuperacion(string email, bool esReenvio = false)
        {
            try
            {
                AccesoUsuario datosUsr = new AccesoUsuario();
                EmailService emailService = new EmailService();

                // Obtener información del cliente
                Cliente cliente = datosUsr.BuscarClientePorMail(email);

                if (cliente == null)
                {
                    return false;
                }

                // Generar nuevo token
                string nuevoToken = GenerarToken();

                // Actualizar sesión
                TokenGenerado = nuevoToken;
                Session["MailRecuperacion"] = email;
                Session["TokenExpiracion"] = DateTime.Now.AddMinutes(5);

                // Generar contenido del mail
                string contenidoHTML = GenerarEmailHTML(cliente.Nombre, nuevoToken, esReenvio);
                string asunto = esReenvio ? "Nuevo código de verificación" : "Cambio de contraseña";

                // Enviar mail
                emailService.ArmarMail(email, asunto, contenidoHTML);
                emailService.EnviarEmail();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Visible = true;
            lblMensaje.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.Text = mensaje;
        }

        protected void BtnEnviarMail_Click(object sender, EventArgs e)
        {
            AccesoUsuario datosUsr = new AccesoUsuario();
            bool mailExiste = datosUsr.MailExiste(txtMail.Text);

            if (!mailExiste)
            {
                MostrarMensaje("No encontramos ninguna cuenta registrada con este mail. Inténtalo nuevamente chequeando que esté bien escrito.", false);
                return;
            }

            bool mailEnviado = EnviarCodigoRecuperacion(txtMail.Text);

            if (mailEnviado)
            {
                MailEnviado = true;
                MostrarMensaje("Mail enviado con éxito. Revisa tu casilla de mail.", true);

                // Ocultar el panel de mail
                MailPanel.Visible = false;
                btnEnviarMail.Visible = false;
            }
            else
            {
                MostrarMensaje("Error al enviar el mail. Intenta nuevamente.", false);
            }
        }

        protected void BtnEnviarToken_Click(object sender, EventArgs e)
        {
            // Obtener el código ingresado por el usuario
            string codigoIngresado = txtCodigoVerificacion.Text.Trim();

            // Validar expiración
            DateTime? expiracion = Session["TokenExpiracion"] as DateTime?;

            if (expiracion.HasValue && DateTime.Now > expiracion.Value)
            {
                MostrarMensaje("El código ha expirado. Solicita uno nuevo.", false);
                return;
            }

            // Validar que el código tenga 6 dígitos
            if (string.IsNullOrEmpty(codigoIngresado) || codigoIngresado.Length != 6)
            {
                MostrarMensaje("Por favor, ingresa un código de 6 dígitos.", false);
                return;
            }

            // Validar que solo contenga números
            if (!System.Text.RegularExpressions.Regex.IsMatch(codigoIngresado, @"^\d{6}$"))
            {
                MostrarMensaje("El código debe contener solo números.", false);
                return;
            }

            // Comparar con el token generado
            if (codigoIngresado == TokenGenerado)
            {
                // Token válido, redirigir a página de cambio de contraseña
                Response.Redirect("CambiarClave.aspx");
            }
            else
            {
                MostrarMensaje("El código ingresado es incorrecto. Intenta nuevamente.", false);
            }
        }

        protected void BtnReenviarCodigo_Click(object sender, EventArgs e)
        {
            string mailRecuperacion = Session["MailRecuperacion"]?.ToString();

            if (string.IsNullOrEmpty(mailRecuperacion))
            {
                MostrarMensaje("Sesión expirada. Por favor, ingresa tu mail nuevamente.", false);

                // Resetear el formulario
                MailEnviado = false;
                MailPanel.Visible = true;
                btnEnviarMail.Visible = true;
                return;
            }

            bool mailEnviado = EnviarCodigoRecuperacion(mailRecuperacion, esReenvio: true);

            if (mailEnviado)
            {
                // Recargar la página para reiniciar el temporizador
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                MostrarMensaje("Error al reenviar el código. Intenta nuevamente.", false);
            }
        }
    }
}