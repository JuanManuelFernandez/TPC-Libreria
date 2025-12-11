using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class AgregarAutor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private int CantidadAutores(string nombre, string apellido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT COUNT(*) FROM Autores WHERE Nombre = @Nombre AND Apellido = @Apellido");
                datos.SetearParametro("@Nombre", nombre);
                datos.SetearParametro("@Apellido", apellido);

                int cantidad = Convert.ToInt32(datos.EjecutarScalar());
                return cantidad;
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al validar el autor: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
                return -1;
            }
            finally
            {
                datos.Cerrar();
            }
        }

        private void Agregar_Autor(string nombre, string apellido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Autores (Nombre, Apellido) VALUES (@Nombre, @Apellido)");
                datos.SetearParametro("@Nombre", nombre);
                datos.SetearParametro("@Apellido", apellido);

                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al agregar el autor a la base de datos: " + ex.Message;
                LblMensaje.Visible = true;
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_AgregarAutor(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();
            string apellido = TxtApellido.Text.Trim();

            //Trim = para eliminar espacios que se ponen de manera automática

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido))
            {
                int cantidad = CantidadAutores(nombre, apellido);

                if (cantidad > 0)
                {
                    LblMensaje.Text = "El autor ya existe en la base de datos.";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (cantidad == 0)
                {
                    Agregar_Autor(nombre, apellido);
                    LblMensaje.Text = "¡Autor agregado con éxito!";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Green;
                    TxtNombre.Text = "";
                    TxtApellido.Text = "";
                }
                else
                {
                    LblMensaje.Text = "Error al agregar el autor.";
                    LblMensaje.Visible = true;
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LblMensaje.Text = "Los campos deben estar completos.";
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