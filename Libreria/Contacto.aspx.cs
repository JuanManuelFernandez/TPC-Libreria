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
            //var emailService = new EmailService();

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                lblError.Text = "La descripcion no puede estar vacia.";
                lblError.Visible = true;
                return;
            }

            //emailService.ArmarCorreo(txtEmail.Text, "Consulta #" + idConsulta, txtDescripcion.Text);
        }
    }
}