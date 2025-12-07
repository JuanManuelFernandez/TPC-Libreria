using Dominio;
using Negocio;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Default : System.Web.UI.Page
    {
        private AccesoDatos datos = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
                CargarLibros();

                if (usuario != null && usuario.TipoUsuario == TipoUsuario.Admin)
                {
                    btnAgregarLibro.Visible = true;
                }
            }
        }

        private void CargarLibros()
        {
            var connSettings = ConfigurationManager.ConnectionStrings["TPCLibreriaUTN"];
            string connString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();
            Usuario usuario = (Usuario)Session["usuario"];

            string query;

            if(usuario != null && usuario.TipoUsuario == TipoUsuario.Admin)
            {
                query = "SELECT * FROM Libros";
            }
            else
            {
                query = "SELECT * FROM Libros WHERE Disponible = 1 ORDER BY IDLibro";
            }

            using (SqlConnection conn = new SqlConnection(connString))
            
            using (SqlCommand cmd = new SqlCommand(query, conn)) 
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtLibros);

                    rptLibros.DataSource = dtLibros;
                    rptLibros.DataBind();

                    if (dtLibros.Rows.Count == 0)
                    {
                        rptLibros.Visible = false;
                        pnlNoLibros.Visible = true;
                    }
                    else
                    {
                        rptLibros.Visible = true;
                        pnlNoLibros.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void MostrarErrorSinLogin()
        {
            foreach (RepeaterItem item in rptLibros.Items)
            {
                Label lblError = (Label)item.FindControl("lblError");
                if (lblError != null)
                {
                    lblError.Visible = true;
                }
            }
        }
        private void AgregarAlCarrito(int idCliente, int idLibro)
        {
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Carrito (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.SetearParametro("@IDLibro", idLibro);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }

        }
        protected void Btn_AgregarCarrito(object sender, CommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario == null)
            {
                MostrarErrorSinLogin();
            }
            if(usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
            {
                var dataCli = new AccesoClientes();
                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente; //usuario.IdUsuario ;

                int idLibro = Convert.ToInt32(e.CommandArgument);

                try
                {
                   AgregarAlCarrito(idCliente, idLibro);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void AgregarLista(int idCliente, int idLibro)
        {
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Deseados (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.SetearParametro("@IDLibro", idLibro);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_AgregarLista(object sender, CommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (Session["usuario"] == null)
            {
                MostrarErrorSinLogin();
            }
            if (usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
            {
                var dataCli = new AccesoClientes();
                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente; //usuario.IdUsuario ;

                int idLibro = Convert.ToInt32(e.CommandArgument);

                try
                {
                    AgregarLista(idCliente, idLibro);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void Btn_EliminarLibro(object sender, CommandEventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            int idLibro = Convert.ToInt32(e.CommandArgument);
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Libros SET Disponible = 0 WHERE IDLibro = @IDLibro");
                datos.SetearParametro("@IDLibro", idLibro);
                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_DarDeAltaLibro (object sender, CommandEventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            int idLibro = Convert.ToInt32 (e.CommandArgument);
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Libros SET Disponible = 1 WHERE IDLibro = @IDLibro");
                datos.SetearParametro("@IDLibro", idLibro);
                datos.EjecutarNonQuery();
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
        protected void Btn_Modificar(object sender, CommandEventArgs e)
        {
            int idLibro = Convert.ToInt32(e.CommandArgument);
            //Pasamos el id del libro para modificarlo
            Response.Redirect("EditarLibro.aspx?id=" + idLibro);
        }
        protected void Btn_AgregarLibro(object sender, CommandEventArgs e)
        {
            Response.Redirect("AgregarLibro.aspx");
        }
        protected void Ocultar_Botones(object sender, RepeaterItemEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(usuario != null && usuario.TipoUsuario == TipoUsuario.Admin)
                {
                    LinkButton btnAgregarCarrito = (LinkButton)e.Item.FindControl("btnAgregarCarrito");
                    LinkButton btnAgregarLista = (LinkButton)e.Item.FindControl("btnAgregarLista");

                    btnAgregarCarrito.Visible = false;
                    btnAgregarLista.Visible = false;
                }
                if (usuario == null || usuario.TipoUsuario == TipoUsuario.Cliente)
                {
                    LinkButton btnEliminarLibro = (LinkButton)e.Item.FindControl("btnEliminarLibro");
                    LinkButton btnDarDeAlta = (LinkButton)e.Item.FindControl("btnDarDeAlta");
                    LinkButton btnModificar = (LinkButton)e.Item.FindControl("btnModificar");

                    btnEliminarLibro.Visible = false;
                    btnDarDeAlta.Visible = false;
                    btnModificar.Visible = false;
                    
                }
                
            }
        }
    }
}
