using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class AgregarGenero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private int ExisteGenero(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT COUNT(*) FROM Generos WHERE Nombre = @Nombre");
                datos.SetearParametro("@Nombre", nombre);

                int cantidad = Convert.ToInt32(datos.EjecutarScalar());
                return cantidad;
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al validar el genero: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
                return -1;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        private void Agregar_Genero(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Generos (Nombre) VALUES (@Nombre)");
                datos.SetearParametro("@Nombre", nombre);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al agregar el genero a la base de datos: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_AgregarGenero(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();

            //Trim = para eliminar espacios que se ponen de manera automática

            if (!string.IsNullOrEmpty(nombre))
            {
                int cantidad = ExisteGenero(nombre);

                if (cantidad > 0)
                {
                    LblMensaje.Text = "El genero ya existe en la base de datos.";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (cantidad == 0)
                {
                    Agregar_Genero(nombre);
                    LblMensaje.Text = "¡Genero agregado con éxito!";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Green;
                    TxtNombre.Text = "";
                }
                else
                {
                    LblMensaje.Text = "Error al agregar el genero.";
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