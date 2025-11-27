using Dominio;
using Negocio;
using System;
using System.Text.RegularExpressions;

namespace Libreria
{
    public partial class CambiarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar que el usuario haya llegado desde el proceso de recuperación
                if (Session["MailRecuperacion"] == null || Session["TokenRecuperacion"] == null)
                {
                    // Si no hay sesión de recuperación, redirigir
                    Response.Redirect("Recuperar.aspx");
                }
            }
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

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = esExito ? "alert alert-success alert-custom" : "alert alert-danger alert-custom";
            lblMensaje.Text = mensaje;
        }

        protected void BtnCambiarClave_Click(object sender, EventArgs e)
        {
            string nuevaClave = txtNuevaClave.Text.Trim();
            string confirmarClave = txtConfirmarClave.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nuevaClave) || string.IsNullOrEmpty(confirmarClave))
            {
                MostrarMensaje("Por favor, completa todos los campos.", false);
                return;
            }

            // Validar que las contraseñas coincidan
            if (nuevaClave != confirmarClave)
            {
                lblErrorConfirmacion.Visible = true;
                lblErrorConfirmacion.Text = "Las contraseñas no coinciden.";
                return;
            }

            // Validar fortaleza de la contraseña
            if (!ValidarFortalezaClave(nuevaClave))
            {
                MostrarMensaje("La contraseña no cumple con los requisitos de seguridad.", false);
                return;
            }

            try
            {
                string mailRecuperacion = Session["MailRecuperacion"]?.ToString();

                if (string.IsNullOrEmpty(mailRecuperacion))
                {
                    MostrarMensaje("Sesión expirada. Por favor, inicia el proceso de recuperación nuevamente.", false);
                    return;
                }

                AccesoUsuario datosUsr = new AccesoUsuario();
                Usuario usuario = datosUsr.Listar().Find(u => u.Mail == mailRecuperacion);

                if (usuario != null)
                {
                    // Actualizar la contraseña
                    usuario.Clave = nuevaClave;
                    datosUsr.Modificar(usuario);

                    // Limpiar sesión de recuperación
                    Session.Remove("MailRecuperacion");
                    Session.Remove("TokenRecuperacion");
                    Session.Remove("TokenExpiracion");
                    Session.Remove("MailEnviado");

                    // Mostrar mensaje de éxito
                    MostrarMensaje("¡Contraseña cambiada exitosamente! Serás redirigido al inicio de sesión.", true);

                    // Redirigir después de 3 segundos
                    Response.AddHeader("REFRESH", "3;URL=Login.aspx");
                }
                else
                {
                    MostrarMensaje("No se encontró el usuario. Por favor, intenta nuevamente.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cambiar la contraseña: " + ex.Message, false);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar sesión y redirigir
            Session.Remove("MailRecuperacion");
            Session.Remove("TokenRecuperacion");
            Session.Remove("TokenExpiracion");
            Session.Remove("MailEnviado");

            Response.Redirect("Default.aspx");
        }
    }
}