using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.UI;
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
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
                CargarLibros();
            }
        }

        private void CargarLibros()
        {
            var connSettings = ConfigurationManager.ConnectionStrings["TPCLibreriaUTN"];

            string connString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Libros ORDER BY IDLibro", conn)) 
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
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }

        }
        protected void Btn_AgregarCarrito(object sender, CommandEventArgs e)
        {
            if (Session["usuario"] == null)
            {
                MostrarErrorSinLogin();
            }
            if(Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                int idCliente = usuario.IdUsuario;

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
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_AgregarLista (object sender, CommandEventArgs e)
        {
            if (Session["usuario"] == null)
            {
                MostrarErrorSinLogin();
            }
            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                int idCliente = usuario.IdUsuario;

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
        }
    }
}
