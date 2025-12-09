using Dominio;
using Negocio;
using System;
using System.Text.RegularExpressions;
using System.Web.Services.Description;

namespace Libreria
{
    public partial class Registro : System.Web.UI.Page
    {
        public bool CodigoEnviado
        {
            get => Session["CodigoEnviado"] != null && (bool)Session["CodigoEnviado"];
            set => Session["CodigoEnviado"] = value;
        }

        public string TokenGenerado
        {
            get => Session["TokenRegistro"] != null ? Session["TokenRegistro"].ToString() : string.Empty;
            set => Session["TokenRegistro"] = value;
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
            Session["CodigoEnviado"] = false;
            Session["TokenRegistro"] = null;
            Session["TokenExpiracion"] = null;
            Session["RegistroNombre"] = null;
            Session["RegistroApellido"] = null;
            Session["RegistroDNI"] = null;
            Session["RegistroEmail"] = null;
            Session["RegistroTelefono"] = null;
            Session["RegistroClave"] = null;
        }

        private bool ValidarFortalezaClave(string clave)
        {
            // Al menos 8 caracteres
            if (clave.Length < 8)
                return false;

            // Al menos una mayúscula
            if (!Regex.IsMatch(clave, @"[A-Z]"))
                return false;

            // Al menos una minúscula
            if (!Regex.IsMatch(clave, @"[a-z]"))
                return false;

            // Al menos un número
            if (!Regex.IsMatch(clave, @"[0-9]"))
                return false;

            return true;
        }

        private string GenerarToken()
        {
            Random random = new Random();
            int token = random.Next(100000, 999999);
            return token.ToString();
        }

        private string GenerarEmailHTML(string nombreCliente, string token)
        {
            return $@"
                <html><body style='font-family: Arial, sans-serif; background:#f4f6f8; margin:0; padding:20px;'>
                  <div style='max-width:600px; margin:auto; background:#fff; padding:30px; border-radius:8px;'>
                    <h2 style='color:#333;'>Verificación de registro</h2>

                    <p style='font-size:16px; color:#555;'>¡Hola {nombreCliente}! Gracias por registrarte en Librería UTN.</p>
                    <p style='font-size:16px; color:#555;'>Usa el código de abajo para verificar tu cuenta.</p>

                    <p style='text-align:center;'>
                      <span style='background:#0d6efd; color:#fff; padding:12px 24px; border-radius:6px; font-weight:bold; font-size:24px; display:inline-block;'>{token}</span>
                    </p>

                    <p style='font-size:13px; color:#777;'>Si no solicitaste este registro, puedes ignorar este email.</p>
                    <p style='font-size:13px; color:#777;'>Este código expira en 5 minutos.</p>
                    <p style='font-size:12px; color:#999; margin-top:30px;'>&copy; {DateTime.UtcNow.Year} Librería UTN. Todos los derechos reservados.</p>
                  </div>
                </body></html>";
        }

        private bool EnviarCodigoVerificacion(string email, string nombre, bool esReenvio = false)
        {
            try
            {
                EmailService emailService = new EmailService();

                // Generar nuevo token
                string nuevoToken = GenerarToken();

                // Actualizar sesión
                TokenGenerado = nuevoToken;
                Session["TokenExpiracion"] = DateTime.Now.AddMinutes(5);

                // Generar contenido del mail
                string contenidoHTML = GenerarEmailHTML(nombre, nuevoToken);
                string asunto = esReenvio ? "Nuevo código de verificación" : "Código de verificación - Registro";

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
            LblError.Visible = true;
            LblError.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            LblError.Text = mensaje;
        }

        private void MostrarMensajeVerificacion(string mensaje, bool esExito)
        {
            lblMensajeVerificacion.Visible = true;
            lblMensajeVerificacion.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensajeVerificacion.Text = mensaje;
        }

        protected void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {
            // Validar todos los campos
            if (string.IsNullOrWhiteSpace(NombreTxt.Text) ||
                string.IsNullOrWhiteSpace(ApellidoTxt.Text) ||
                string.IsNullOrWhiteSpace(DNITxt.Text) ||
                string.IsNullOrWhiteSpace(MailTxt.Text) ||
                string.IsNullOrWhiteSpace(TelefonoTxt.Text) ||
                string.IsNullOrWhiteSpace(ClaveTxt.Text))
            {
                MostrarMensaje("Por favor, completa todos los campos.", false);
                return;
            }

            // Validar fortaleza de contraseña
            if (!ValidarFortalezaClave(ClaveTxt.Text))
            {
                MostrarMensaje("La contraseña no cumple con los requisitos de seguridad.", false);
                return;
            }

            // Verificar que el email no esté registrado
            AccesoUsuario datosUsr = new AccesoUsuario();
            if (datosUsr.MailExiste(MailTxt.Text))
            {
                MostrarMensaje("Este email ya está registrado.", false);
                return;
            }

            // Verificar que el DNI no esté registrado
            AccesoClientes datoCli = new AccesoClientes();
            if (datoCli.Listar().Exists(c => c.Dni == DNITxt.Text))
            {
                MostrarMensaje("Este DNI ya está registrado.", false);
                return;
            }

            // Guardar datos en sesión
            Session["RegistroNombre"] = NombreTxt.Text;
            Session["RegistroApellido"] = ApellidoTxt.Text;
            Session["RegistroDNI"] = DNITxt.Text;
            Session["RegistroEmail"] = MailTxt.Text;
            Session["RegistroTelefono"] = TelefonoTxt.Text;
            Session["RegistroClave"] = ClaveTxt.Text;

            // Enviar código
            bool codigoEnviado = EnviarCodigoVerificacion(MailTxt.Text, NombreTxt.Text);

            if (codigoEnviado)
            {
                CodigoEnviado = true;
                pnlRegistro.Visible = false;
            }
            else
            {
                MostrarMensaje("Error al enviar el código. Intenta nuevamente.", false);
            }
        }

        protected void BtnVerificarCodigo_Click(object sender, EventArgs e)
        {
            // Obtener el código ingresado por el usuario
            string codigoIngresado = txtCodigoVerificacion.Text.Trim();

            // Validar expiración
            DateTime? expiracion = Session["TokenExpiracion"] as DateTime?;

            if (expiracion.HasValue && DateTime.Now > expiracion.Value)
            {
                MostrarMensajeVerificacion("El código ha expirado. Solicita uno nuevo.", false);
                return;
            }

            // Validar que el código tenga 6 dígitos
            if (string.IsNullOrEmpty(codigoIngresado) || codigoIngresado.Length != 6)
            {
                MostrarMensajeVerificacion("Por favor, ingresa un código de 6 dígitos.", false);
                return;
            }

            // Validar que solo contenga números
            if (!Regex.IsMatch(codigoIngresado, @"^\d{6}$"))
            {
                MostrarMensajeVerificacion("El código debe contener solo números.", false);
                return;
            }

            // Comparar con el token generado
            if (codigoIngresado != TokenGenerado)
            {
                MostrarMensajeVerificacion("El código ingresado es incorrecto. Intenta nuevamente.", false);
                return;
            }

            try
            {
                // Código correcto - Crear usuario
                AccesoUsuario datosUsr = new AccesoUsuario();
                Usuario nuevoUsuario = new Usuario
                {
                    Mail = Session["RegistroEmail"].ToString(),
                    Clave = Session["RegistroClave"].ToString(),
                    TipoUsuario = TipoUsuario.Cliente
                };

                datosUsr.Agregar(nuevoUsuario);

                // Obtener el ID del usuario recién creado
                Usuario usuarioCreado = datosUsr.Listar().Find(u => u.Mail == nuevoUsuario.Mail);

                // Crear cliente
                AccesoClientes datoCli = new AccesoClientes();
                Cliente nuevoCliente = new Cliente
                {
                    Usuario = usuarioCreado,
                    Nombre = Session["RegistroNombre"].ToString(),
                    Apellido = Session["RegistroApellido"].ToString(),
                    Dni = Session["RegistroDNI"].ToString(),
                    Telefono = Session["RegistroTelefono"].ToString()
                };

                datoCli.Agregar(nuevoCliente);

                // Iniciar sesión automáticamente
                Session["usuario"] = usuarioCreado;

                // Limpiar sesiones de registro
                LimpiarSesion();

                // Mostrar mensaje de éxito
                pnlMensaje.Visible = true;
                pnlMensaje.CssClass = "alert alert-success alert-custom";
                lblMensaje.Text = "¡Cuentta creada exitosamente! Serás redirigido al la pagina principal.";

                // Redirigir después de 3 segundos
                Response.AddHeader("REFRESH", "3;URL=Default.aspx");
            }
            catch (Exception ex)
            {
                MostrarMensajeVerificacion("Error al crear la cuenta: " + ex.Message, false);
            }
        }

        protected void BtnReenviarCodigo_Click(object sender, EventArgs e)
        {
            string email = Session["RegistroEmail"]?.ToString();
            string nombre = Session["RegistroNombre"]?.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(nombre))
            {
                MostrarMensajeVerificacion("Sesión expirada. Por favor, inicia el registro nuevamente.", false);
                CodigoEnviado = false;
                pnlRegistro.Visible = true;
                return;
            }

            bool codigoEnviado = EnviarCodigoVerificacion(email, nombre, esReenvio: true);

            if (codigoEnviado)
            {
                // Recargar la página para reiniciar el temporizador
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                MostrarMensajeVerificacion("Error al reenviar el código. Intenta nuevamente.", false);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarSesion();
            Response.Redirect("Default.aspx");
        }
    }
}