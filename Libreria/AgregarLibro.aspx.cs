using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class AgregarLibro : System.Web.UI.Page
    {
        private AccesoDatos datos = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Btn_Agregar(object sender, EventArgs e)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Libros (IDAutor, IDGenero, IDEditorial, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock) " +
                                "VALUES (@IDAutor, @IDGenero, @IDEditorial, @Titulo, @Descripcion, @FechaPublicacion, @Precio, @Paginas, @Stock)");
                datos.SetearParametro("@IDAutor", TxtIDAutor.Text);
                datos.SetearParametro("@IDGenero", TxtIDGenero.Text);
                datos.SetearParametro("@IDEditorial", TxtIDEditorial.Text);
                datos.SetearParametro("@Titulo", TxtTitulo.Text);
                datos.SetearParametro("@Descripcion", TxtDescrip.Text);
                datos.SetearParametro("@FechaPublicacion", TxtFecha.Text);
                datos.SetearParametro("@Precio", TxtPrecio.Text);
                datos.SetearParametro("Paginas", TxtPaginas.Text);
                datos.SetearParametro("@Stock", TxtStock.Text);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (FilePortada.HasFile)
            {
                string extension = Path.GetExtension(FilePortada.FileName).ToLower();

                string destino = "~/assets/portadas";
                string ruta = Server.MapPath(destino);

                string nombreBase = Path.GetFileNameWithoutExtension(FilePortada.FileName);
                string nombreArchivo = nombreBase + extension;
                string rutaCompleta = Path.Combine(ruta, nombreArchivo);

                try
                {
                    FilePortada.SaveAs(rutaCompleta);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    //Cambiar esta parte por una idLibro autoincremental o sacarla de un campo txt
                    int idLibro = ;

                    string nombrePortada = idLibro + extension;

                    datos.Conectar();
                    datos.Consultar("INSERT INTO Portadas (IDLibro, Imagen) VALUES (@IDLibro, @Imagen)");
                    datos.SetearParametro("@IDLibro", idLibro);
                    datos.SetearParametro("@Imagen", nombrePortada);

                    datos.EjecutarNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    LimpiarCampos();
                    Response.Redirect("LibroAgregado.aspx");
                    datos.Cerrar();
                }
            }
        }
        protected void LimpiarCampos()
        {
            TxtIDAutor.Text = "";
            TxtIDGenero.Text = "";
            TxtIDEditorial.Text = "";
            TxtTitulo.Text = "";
            TxtDescrip.Text = "";
            TxtFecha.Text = "";
            TxtPrecio.Text = "";
            TxtPaginas.Text = "";
            TxtStock.Text = "";
        }
    }
}