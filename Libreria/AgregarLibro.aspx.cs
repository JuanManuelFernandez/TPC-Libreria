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
            int idLibro = 0;

            if (FilePortada.HasFile)
            {
                string extension = Path.GetExtension(FilePortada.FileName).ToLower();

                if (extension == ".jpg")
                {
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
                        datos.SetearParametro("@Paginas", TxtPaginas.Text);
                        datos.SetearParametro("@Stock", TxtStock.Text);

                        datos.EjecutarNonQuery();

                        datos.Consultar("SELECT MAX(IDLibro) FROM Libros");
                        idLibro = Convert.ToInt32(datos.EjecutarScalarLibros());

                        string destino = "~/assets/portadas";
                        string ruta = Server.MapPath(destino);

                        string nombrePortada = idLibro + extension;
                        string rutaCompleta = Path.Combine(ruta, nombrePortada);

                        FilePortada.SaveAs(rutaCompleta);

                        datos.Consultar("INSERT INTO Portadas (IDLibro, Imagen) VALUES (@IDLibro, @Imagen)");
                        datos.SetearParametro("@IDLibro", idLibro);
                        datos.SetearParametro("@Imagen", nombrePortada);

                        datos.EjecutarNonQueryLibro();

                        LimpiarCampos();
                        Response.Redirect("LibroAgregado.aspx");
                    }
                    catch (Exception ex)
                    {
                        LblError.Text = "Ocurrió un error al agregar el libro";
                        LblError.Visible = true;
                        LblError.ForeColor = System.Drawing.Color.Red;
                        throw ex;
                    }
                    finally
                    {
                        datos.Cerrar();
                    }
                }
                else
                {
                    LblError.Text = "Solo se permiten imágenes en formato .jpg";
                    LblError.Visible = true;
                    LblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LblError.Text = "Debe seleccionar una portada en formato .jpg";
                LblError.Visible = true;
                LblError.ForeColor= System.Drawing.Color.Red;
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
        protected void Btn_AgregarAutor(object sender, EventArgs e)
        {
            Response.Redirect("AgregarAutor.aspx");
        }
        protected void Btn_AgregarGenero(object sender, EventArgs e)
        {
            Response.Redirect("AgregarGenero.aspx");
        }
        protected void Btn_AgregarEditorial(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditorial.aspx");
        }
    }
}