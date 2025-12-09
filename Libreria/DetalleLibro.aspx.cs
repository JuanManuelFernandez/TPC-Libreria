using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class DetalleLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {
                int idLibro;
                if (int.TryParse(Request.QueryString["id"], out idLibro))
                {
                    CargarLibro(idLibro);
                    CargarResenas(idLibro);
                }
                if(usuario != null && usuario.TipoUsuario == TipoUsuario.Admin)
                {
                    btnAgregarCarrito.Visible = false;
                    btnListaDeseados.Visible = false;
                    txtOpinion.Visible = false;
                    btnGuardar.Visible = false;
                    ddlPuntaje.Visible = false;
                    LblOpinion.Visible = false;
                    LblPuntaje.Visible = false;
                }
            }
        }
        private void CargarLibro(int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT L.Titulo, L.Descripcion, A.Nombre + ' ' + A.Apellido AS Autor, G.Nombre AS Genero FROM Libros L INNER JOIN Autores A ON L.IDAutor = A.IDAutor INNER JOIN Generos G ON L.IDGenero = G.IDGenero WHERE L.IDLibro = @IDLibro");
                datos.SetearParametro("@IDLibro", idLibro);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    imgLibro.ImageUrl = "assets/portadas/" + idLibro + ".jpg";
                    lblTitulo.Text = datos.Lector["Titulo"].ToString();
                    lblDescripcion.Text = datos.Lector["Descripcion"].ToString();
                    lblAutor.Text = "Autora: " + datos.Lector["Autor"].ToString();
                    lblGenero.Text = "Género: " + datos.Lector["Genero"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblTitulo.Text = "Error al cargar el libro";
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void Btn_Guardar_Opinion(object sender, EventArgs e)
        {
            int idLibro;
            if (int.TryParse(Request.QueryString["id"], out idLibro))
            {
                AccesoDatos datos = new AccesoDatos();
                Usuario usuario = (Usuario)Session["usuario"];

                if (!string.IsNullOrEmpty(txtOpinion.Text))
                {
                    int puntaje = int.Parse(ddlPuntaje.SelectedValue);

                    try
                    {
                        datos.Conectar();
                        if (usuario != null)
                        {
                            datos.Consultar("INSERT INTO Opiniones (IDLibro, Texto, Puntaje, Fecha) VALUES (@IDLibro, @Texto, @Puntaje, GETDATE())");
                            datos.SetearParametro("@IDLibro", idLibro);
                            datos.SetearParametro("@Texto", txtOpinion.Text);
                            datos.SetearParametro("@Puntaje", puntaje);
                            datos.EjecutarNonQuery();

                            LblMensaje.Visible = true;
                            LblMensaje.Text = "¡Reseña publicada con éxito!";
                            LblMensaje.ForeColor = System.Drawing.Color.Green;
                            txtOpinion.Text = "";
                            ddlPuntaje.SelectedIndex = 0;
                        }
                        else
                        {
                            LblMensaje.Visible = true;
                            LblMensaje.Text = "Debes iniciar sesión para publicar una reseña.";
                            LblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        LblMensaje.Text = "No se pudo publicar la reseña.";
                        throw ex;
                    }
                    finally
                    {
                        CargarResenas(idLibro);
                        datos.Cerrar();
                    }
                }
            }
        }
        private void AgregarAlCarrito(int idCliente, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Carrito (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
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
        }
        protected void Btn_AgregarCarrito(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            int idLibro;
            if (int.TryParse(Request.QueryString["id"], out idLibro))
            {
                if (usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
                {
                    var dataCli = new AccesoClientes();
                    int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente; //usuario.IdUsuario ;

                    try
                    {
                        AgregarAlCarrito(idCliente, idLibro);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    LblAgregado.Visible = true;
                    LblAgregado.Text = "¡Libro agregado al carrito con exito!";
                    LblAgregado.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void AgregarLista(int idCliente, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Deseados (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
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
        }
        protected void Btn_AgregarLista(object sender, CommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            int idLibro;
            if (int.TryParse(Request.QueryString["id"], out idLibro))
            {
                if (usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
                {
                    var dataCli = new AccesoClientes();
                    int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente; //usuario.IdUsuario ;

                    try
                    {
                        AgregarLista(idCliente, idLibro);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    LblAgregado.Visible = true;
                    LblAgregado.Text = "¡Libro agregado a la lista de deseados con exito!";
                    LblAgregado.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        private void CargarResenas(int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDReseña, Texto, Puntaje, Fecha FROM Opiniones WHERE IDLibro = @IDLibro ORDER BY Fecha DESC");
                datos.SetearParametro("@IDLibro", idLibro);
                datos.Leer();

                rptOpiniones.DataSource = datos.Lector;
                rptOpiniones.DataBind();
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void rptOpiniones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idOpinion = int.Parse(e.CommandArgument.ToString());
                EliminarOpinion(idOpinion);
                LblMensaje.Text = "Reseña eliminada correctamente.";

                // refrescar listado
                int idLibro;
                if (int.TryParse(Request.QueryString["id"], out idLibro))
                {
                    CargarResenas(idLibro);
                }
            }
        }
        private void EliminarOpinion(int idOpinion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM Opiniones WHERE IDReseña = @IDReseña");
                datos.SetearParametro("IDReseña", idOpinion);

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
        }
        protected void Ocultar_BtnEliminar(object sender, RepeaterItemEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(usuario == null || usuario.TipoUsuario == TipoUsuario.Cliente)
                {
                    LinkButton btnEliminar = (LinkButton)e.Item.FindControl("btnEliminar");

                    btnEliminar.Visible = false;
                }
            }
        }
    }
}