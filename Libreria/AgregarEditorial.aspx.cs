using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class AgregarEditorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private int ExisteEditorial(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT COUNT(*) FROM Editoriales WHERE Nombre = @Nombre");
                datos.SetearParametro("@Nombre", nombre);

                int cantidad = Convert.ToInt32(datos.EjecutarScalar());
                return cantidad;
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al validar la editorial: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
                return -1;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        private void Agregar_Editorial(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Editoriales (Nombre) VALUES (@Nombre)");
                datos.SetearParametro("@Nombre", nombre);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al agregar la editorial a la base de datos: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_AgregarEditorial(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();

            //Trim = para eliminar espacios que se ponen de manera automática

            if (!string.IsNullOrEmpty(nombre))
            {
                int cantidad = ExisteEditorial(nombre);

                if (cantidad > 0)
                {
                    LblMensaje.Text = "La editorial ya existe en la base de datos.";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (cantidad == 0)
                {
                    Agregar_Editorial(nombre);
                    LblMensaje.Text = "¡Editorial agregada con éxito!";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Green;
                    TxtNombre.Text = "";
                }
                else
                {
                    LblMensaje.Text = "Error al agregar la editorial.";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LblMensaje.Text = "El campo debe estar completo.";
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Btn_Volver(object sender, EventArgs e)
        {
            Response.Redirect("AgregarLibro.aspx");
        }
    }
}