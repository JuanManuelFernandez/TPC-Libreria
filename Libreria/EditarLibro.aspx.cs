using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Libreria
{
    public partial class EditarLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idLibro = Convert.ToInt32(Request.QueryString["id"]);
                AccesoDatos datos = new AccesoDatos();
                datos.Conectar();
                datos.Consultar("SELECT * FROM Libros WHERE IDLibro = @IDLibro");
                datos.SetearParametro("@IDLibro", idLibro);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    TxtIDAutor.Text = datos.Lector["IDAutor"].ToString();
                    TxtIDGenero.Text = datos.Lector["IDGenero"].ToString();
                    TxtIDEditorial.Text = datos.Lector["IDEditorial"].ToString();
                    TxtTitulo.Text = datos.Lector["Titulo"].ToString();
                    TxtDescrip.Text = datos.Lector["Descripcion"].ToString();
                    TxtFecha.Text = TxtFecha.Text = Convert.ToDateTime(datos.Lector["FechaPublicacion"]).ToString("yyyy-MM-dd");
                    TxtPrecio.Text = TxtPrecio.Text = Convert.ToDecimal(datos.Lector["Precio"]).ToString("00", CultureInfo.GetCultureInfo("es-AR"));
                    TxtPaginas.Text = datos.Lector["Paginas"].ToString();
                    TxtStock.Text = datos.Lector["Stock"].ToString();
                }

                datos.Cerrar();
            }
        }
        protected void Btn_GuardarCambios(object sender, EventArgs e)
        {
            int idLibro = Convert.ToInt32(Request.QueryString["id"]);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar(@"UPDATE Libros SET IDAutor = @IDAutor, IDGenero = @IDGenero, IDEditorial = @IDEditorial,
                                    Titulo = @Titulo, Descripcion = @Descripcion, FechaPublicacion = @FechaPublicacion, Precio = @Precio, Paginas = @Paginas, Stock = @Stock
                                    WHERE IDLibro = @IDLibro");
                datos.SetearParametro("@IDAutor", TxtIDAutor.Text);
                datos.SetearParametro("@IDGenero", TxtIDGenero.Text);
                datos.SetearParametro("@IDEditorial", TxtIDEditorial.Text);
                datos.SetearParametro("@Titulo", TxtTitulo.Text);
                datos.SetearParametro("@Descripcion", TxtDescrip.Text);
                datos.SetearParametro("@FechaPublicacion", TxtFecha.Text);
                //Para que el precio no salga con ceros de más, puntos, etc
                datos.SetearParametro("@Precio", decimal.Parse(TxtPrecio.Text, CultureInfo.InvariantCulture));
                datos.SetearParametro("@Paginas", TxtPaginas.Text);
                datos.SetearParametro("@Stock", TxtStock.Text);
                datos.SetearParametro("@IDLibro", idLibro);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.Redirect("LibroModificado.aspx");
                datos.Cerrar();
            }
        }
    }
}