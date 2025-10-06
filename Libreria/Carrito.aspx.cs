using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                LblAviso.Visible = true;
            }
            else
            {
                LblAviso.Visible = false;
                dynamic usuario = Session["usuario"];
                int idCliente = usuario.IdUsuario;
                CargarLibros(idCliente);
            }
        }
        private void CargarLibros(int idCliente)
        {
            var connSettings = ConfigurationManager.ConnectionStrings["TPCLibreriaUTN"];
            string connString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();

            List<int> IdsLibros = new List<int>();

            string queryIds = "SELECT IDLibro FROM Carrito WHERE IDCliente = @IDCliente";

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
        protected void Btn_Eliminar(object sender, CommandEventArgs e)
        {
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();
            dynamic usuario = Session["usuario"];
            int idCliente = usuario.IdUsuario;

            int idLibro = Convert.ToInt32(e.CommandArgument);

            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM Carrito WHERE IDCliente = @idCliente AND IDLibro = @idLibro");
                datos.SetearParametro("@idCliente", idCliente);
                datos.SetearParametro("@idLibro", idLibro);

                datos.EjecutarNonQuery();

                CargarLibros(idCliente);
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