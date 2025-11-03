using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                LblMaIL.Visible = false;
                LblClave.Visible = false;
                MailTxt.Visible = false;
                ClaveTxt.Visible = false;
                btnIngresar.Visible = false;
                btnCerrarSesion.Visible = true;
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            var accesousuario = new AccesoUsuario();
            try
            {
                var usuario = accesousuario.Listar().Find(x =>
                x.Mail == MailTxt.Text &&
                x.Clave == ClaveTxt.Text) ?? new Usuario();

                if (accesousuario.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Cuenta.aspx");
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
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}