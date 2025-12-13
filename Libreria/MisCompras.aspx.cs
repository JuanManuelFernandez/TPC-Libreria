using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if(usuario == null)
            {
                Response.Redirect("Login.aspx");
            }
            if(usuario.TipoUsuario == TipoUsuario.Admin)
            {
                lblTitulo.Text = "Compras de usuarios";
            }
            else
            {
                lblTitulo.Text = "Mis compras";
            }
            if (!IsPostBack)
            {
                AccesoClientes cliente = new AccesoClientes();
                int idCliente = cliente.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;

                CargarCompras(idCliente);
            }
        }
        private void CargarCompras(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = (Usuario)Session["usuario"];
            try
            {
                datos.Conectar();
                if(usuario != null && usuario.TipoUsuario == TipoUsuario.Admin)
                {
                    datos.Consultar("SELECT IDCompra, FechaCompra, IDCliente, Mail, Nombre, Apellido, DFacturacion, Localidad, Codigo, Total FROM Compras");
                }
                if(usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
                {
                    datos.Consultar("SELECT IDCompra, FechaCompra, DFacturacion, Localidad, Codigo, Total FROM Compras WHERE IDCliente = @IDCliente");
                    datos.SetearParametro("@IDCliente", idCliente);
                }
                datos.Leer();

                if(!datos.Lector.HasRows)
                {
                    if(usuario.TipoUsuario == TipoUsuario.Admin)
                    {
                        LblMensaje.Text = "Aún no hay compras registradas.";
                        rptComprasAdmin.Visible = false;
                    }
                    else
                    {
                        LblMensaje.Text = "Aún no tiene compras realizadas.";
                        rptCompras.Visible = false;
                    }
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if(usuario.TipoUsuario == TipoUsuario.Admin)
                    {
                        rptComprasAdmin.DataSource = datos.Lector;
                        rptComprasAdmin.DataBind();
                        rptComprasAdmin.Visible = true;
                    }
                    else
                    {
                        rptCompras.DataSource = datos.Lector;
                        rptCompras.DataBind();
                        rptCompras.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_VerDetalle(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                string idCompra = e.CommandArgument.ToString();
                Response.Redirect("DetalleCompra.aspx?IDCompra=" + idCompra);
            }
        }
    }
}