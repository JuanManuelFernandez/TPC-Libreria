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
                    // Autores
                    datos.Conectar();
                    datos.Consultar("SELECT IDAutor, (Nombre + ' ' + Apellido) AS NombreCompleto FROM Autores");
                    DdlAutor.DataSource = datos.EjecutarReader();
                    DdlAutor.DataTextField = "NombreCompleto";
                    DdlAutor.DataValueField = "IDAutor";
                    DdlAutor.DataBind();

                    // Géneros
                    datos.Consultar("SELECT IDGenero, Nombre FROM Generos");
                    DdlGenero.DataSource = datos.EjecutarReader();
                    DdlGenero.DataTextField = "Nombre";
                    DdlGenero.DataValueField = "IDGenero";
                    DdlGenero.DataBind();

                    // Editoriales
                    datos.Consultar("SELECT IDEditorial, Nombre FROM Editoriales");
                    DdlEditorial.DataSource = datos.EjecutarReader();
                    DdlEditorial.DataTextField = "Nombre";
                    DdlEditorial.DataValueField = "IDEditorial";
                    DdlEditorial.DataBind();

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
                datos.SetearParametro("@IDAutor", DdlAutor.SelectedValue);
                datos.SetearParametro("@IDGenero", DdlGenero.SelectedValue);
                datos.SetearParametro("@IDEditorial", DdlEditorial.SelectedValue);
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