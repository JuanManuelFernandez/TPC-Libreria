using System;
using Dominio;

namespace Libreria
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["usuario"];
            var emailService = new EmailService();

            if (string.IsNullOrEmpty(txtTema.Text))
            {
                lblErrorTema.Text = "El tema no puede estar vacio.";
                lblErrorTema.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                lblErrorDescripcion.Text = "La descripcion no puede estar vacia.";
                lblErrorDescripcion.Visible = true;
                return;
            }

            emailService.ArmarCorreo(txtMail.Text, "Consulta: " + txtTema.Text, txtDescripcion.Text);
            try
            {
                emailService.EnviarEmail();

                bool mailExitoso = true; // 
                Session.Add("mailExitoso", mailExitoso);
            }
            catch (Exception ex)
            {
                //Session.Add("error", ex);
                //Response.Redirect("error.aspx");
            }
            Response.Redirect("Default.aspx");
        }
    }
}