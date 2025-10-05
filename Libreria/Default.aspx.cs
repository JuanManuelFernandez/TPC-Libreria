using System;
using System.Data;  // <-- AGREGADO: Para DataTable y DataReader
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;  // <-- AGREGADO: Para List<int>
using System.Globalization;
using System.Threading;

namespace Libreria
{
    public partial class Default : System.Web.UI.Page
    {
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

            string connectionString = connSettings.ConnectionString;
            DataTable dtLibros = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
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
        protected void Btn_AgregarCarrito(object sender, CommandEventArgs e)
        {

        }
    }
}
