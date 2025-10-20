using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"]!=null)
            {
                LblMaIL.Visible = false;
                LblClave.Visible = false;
                MailTxt.Visible = false;
                ClaveTxt.Visible = false;
                btnIngresar.Visible = false;
                btnCerrar.Visible = true;
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            var accesousuario = new AccesoUsuario();
            try
            {
                var usuario = accesousuario.Listar().Find(x =>
                x.Mail == MailTxt.Text && x.Clave == ClaveTxt.Text) != null ?
                accesousuario.Listar().Find(x =>
                        x.Mail == MailTxt.Text &&
                        x.Clave == ClaveTxt.Text) : new Usuario();
                ;
                
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
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}