using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Busqueda : System.Web.UI.Page
    {
        private AccesoDatos datos = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string termino = Request.QueryString["q"];
                if (!string.IsNullOrEmpty(termino))
                {
                    litTermino.Text = Server.HtmlEncode(termino);
                    CargarResultados(termino);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        private void CargarLibros(int idCliente)
        {
            var connSettings = ConfigurationManager.ConnectionStrings["TPCLibreriaUTN"];
            string connString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();

            List<int> IdsLibros = new List<int>();

            string queryIds = "SELECT IDLibro FROM Deseados WHERE IDCliente = @IDCliente";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmdIds = new SqlCommand(queryIds, conn))
            {
                cmdIds.Parameters.AddWithValue("@IDCliente", idCliente);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmdIds.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IdsLibros.Add(reader.GetInt32(0));
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (IdsLibros.Count == 0)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                return;
            }

            string[] parametros = IdsLibros.Select((id, index) => "@id" + index).ToArray();
            string queryLibros = $"SELECT * FROM Libros WHERE IDLibro IN ({string.Join(",", parametros)}) ORDER BY IDLibro";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmdLibros = new SqlCommand(queryLibros, conn))
            {
                for (int i = 0; i < IdsLibros.Count; i++)
                {
                    cmdLibros.Parameters.AddWithValue(parametros[i], IdsLibros[i]);
                }

                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdLibros);
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
        private void CargarResultados(string termino)
        {
            try
            {
                AccesoLibros negocioLibros = new AccesoLibros();
                AccesoAutores negocioAutores = new AccesoAutores();

                List<Libro> resultados = negocioLibros.BuscarLibros(termino);

                if (resultados.Count > 0)
                {
                    var librosMostrar = new List<object>();

                    foreach (var libro in resultados)
                    {
                        librosMostrar.Add(new
                        {
                            IdLibro = libro.IdLibro,
                            Titulo = libro.Titulo,
                            Descripcion = libro.Descripcion,
                            NombreAutor = negocioAutores.ObtenerNombreCompleto(libro.IdAutor),
                            Precio = libro.Precio
                        });
                    }

                    rptLibros.DataSource = librosMostrar;
                    rptLibros.DataBind();

                    rptLibros.Visible = true;
                    lblMensaje.Visible = false;
                }
                else
                {
                    rptLibros.Visible = false;
                    lblMensaje.Text = "No se encontraron resultados para tu búsqueda. Intenta con otros términos.";
                    lblMensaje.CssClass = "alert alert-warning";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                rptLibros.Visible = false;
                lblMensaje.Text = "Error al realizar la búsqueda: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
        protected void BtnComprar_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.Button)sender;
            string idLibro = btn.CommandArgument;

            // Redirigir a la página de detalle del libro
            Response.Redirect($"DetalleLibro.aspx?id={idLibro}");
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
        private void BorrarDeDeseados(int idCliente, int idLibro)
        {
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();
            dynamic usuario = Session["usuario"];

            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM Deseados WHERE IDCliente = @idCliente AND IDLibro = @idLibro");
                datos.SetearParametro("@idCliente", idCliente);
                datos.SetearParametro("@idLibro", idLibro);

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
            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();

                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                int idLibro = Convert.ToInt32(e.CommandArgument);

                try
                {
                    AgregarAlCarrito(idCliente, idLibro);
                    BorrarDeDeseados(idCliente, idLibro);
                    CargarLibros(idCliente);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
    }
}