using Negocio;
using System;

namespace Libreria
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            var accesousuario = new AccesoUsuario();
            try
            {
                var usuario = accesousuario.Listar().Find(x =>
                x.Mail == MailTxt.Text && x.Clave == ClaveTxt.Text);
                
                if(accesousuario.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}