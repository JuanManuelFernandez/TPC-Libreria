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
    public partial class Carrito : System.Web.UI.Page
    {
        private AccesoDatos datos = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();
                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                CargarLibros(idCliente);
            }
        }

        private void CargarLibros(int idCliente)
        {
            var connSettings = ConfigurationManager.ConnectionStrings["TPCLibreriaUTN"];
            string connString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();
            List<int> IdsLibros = new List<int>();

            int idCarrito = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("SELECT IDCarrito FROM Carritos WHERE IDCliente = @IDCliente", conn))
            {
                cmd.Parameters.AddWithValue("@IDCliente", idCliente);

                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                    idCarrito = (int)result;
                conn.Close();
            }

            if (idCarrito == 0)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                btnIrApagar.Visible = false;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("SELECT IDLibro FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito", conn))
            {
                cmd.Parameters.AddWithValue("@IDCarrito", idCarrito);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        IdsLibros.Add(reader.GetInt32(0));
                }
                conn.Close();
            }

            if (IdsLibros.Count == 0)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                btnIrApagar.Visible = false;
                return;
            }

            string[] parametros = IdsLibros.Select((id, index) => "@id" + index).ToArray();
            string queryLibros = $"SELECT * FROM Libros WHERE IDLibro IN ({string.Join(",", parametros)}) ORDER BY IDLibro";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmdLibros = new SqlCommand(queryLibros, conn))
            {
                for (int i = 0; i < IdsLibros.Count; i++)
                    cmdLibros.Parameters.AddWithValue(parametros[i], IdsLibros[i]);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmdLibros);
                adapter.Fill(dtLibros);

                rptLibros.DataSource = dtLibros;
                rptLibros.DataBind();
                conn.Close();

                rptLibros.Visible = dtLibros.Rows.Count > 0;
            }
        }

        protected void Btn_Eliminar(object sender, CommandEventArgs e)
        {
            datos = new AccesoDatos();
            dynamic usuario = Session["usuario"];
            var dataCli = new AccesoClientes();
            int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;

            int idLibro = Convert.ToInt32(e.CommandArgument);
            int idCarrito = 0;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCarrito FROM Carritos WHERE IDCliente = @IDCliente");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.Leer();
                if (datos.Lector.Read())
                    idCarrito = (int)datos.Lector["IDCarrito"];
                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("DELETE FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                datos.SetearParametro("@IDCarrito", idCarrito);
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

            CargarLibros(idCliente);
        }

        protected void Btn_IrApagar(object sender, CommandEventArgs e)
        {
            Response.Redirect("Pago.aspx");
        }
    }
}
