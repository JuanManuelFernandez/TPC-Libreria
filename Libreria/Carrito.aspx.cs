using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();
                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                CargarLibros(idCliente);
            }
        }

        private void CargarLibros(int idCliente)
        {
            var dataCarritos = new AccesoCarritos();
            var dataLibrosCarrito = new AccesoLibrosPorCarrito();
            var dataLibros = new AccesoLibros();

            Dominio.Carrito carrito = dataCarritos.BuscarPorIdCliente(idCliente);

            if (carrito == null)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                btnIrApagar.Visible = false;
                return;
            }

            List<LibroPorCarrito> items = dataLibrosCarrito.ListarPorIdCarrito(carrito.IdCarrito);

            if (items.Count == 0)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                btnIrApagar.Visible = false;
                return;
            }

            var lista = new List<object>();

            foreach (var item in items)
            {
                var libro = dataLibros.BuscarPorIdLibro(item.IdLibro);

                lista.Add(new
                {
                    libro.IdLibro,
                    libro.Titulo,
                    libro.Descripcion,
                    libro.Precio,
                    item.Cantidad
                });
            }

            rptLibros.DataSource = lista;
            rptLibros.DataBind();
            rptLibros.Visible = true;
            pnlNoLibros.Visible = false;
            btnIrApagar.Visible = true;
        }

        protected void Btn_Eliminar(object sender, CommandEventArgs e)
        {
            dynamic usuario = Session["usuario"];
            var dataCli = new AccesoClientes();
            var dataCarritos = new AccesoCarritos();
            var dataLibrosCarrito = new AccesoLibrosPorCarrito();

            int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
            int idLibro = Convert.ToInt32(e.CommandArgument);

            Dominio.Carrito carrito = dataCarritos.BuscarPorIdCliente(idCliente);

            if (carrito != null)
                dataLibrosCarrito.EliminarLibro(carrito.IdCarrito, idLibro);

            CargarLibros(idCliente);
        }
        public bool ExisteElLibro(int idCarrito, int IdLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT COUNT(*) FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                datos.SetearParametro("IDCarrito", idCarrito);
                datos.SetearParametro("IDLibro", IdLibro);

                int count = (int)datos.EjecutarScalar();
                return count > 0;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public void Agregar(LibroPorCarrito nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                if (ExisteElLibro(nuevo.IdCarrito, nuevo.IdLibro))
                {
                    datos.Consultar("UPDATE LibrosPorCarrito SET Cantidad = Cantidad + 1 WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                    datos.SetearParametro("@IDCarrito", nuevo.IdCarrito);
                    datos.SetearParametro("@IDLibro", nuevo.IdLibro);
                }
                else
                {
                    datos.Consultar("INSERT INTO LibrosPorCarrito (IDCarrito, IDLibro, Cantidad, PrecioUnitario) " +
                                    "VALUES (@IDCarrito, @IDLibro, @Cantidad, @PrecioUnitario)");
                    datos.SetearParametro("@IDCarrito", nuevo.IdCarrito);
                    datos.SetearParametro("@IDLibro", nuevo.IdLibro);
                    datos.SetearParametro("@Cantidad", nuevo.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", nuevo.PrecioUnitario);
                }

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

        protected void Btn_Sumar(object sender, CommandEventArgs e)
        {

            dynamic usuario = Session["usuario"];
            var dataCli = new AccesoClientes();
            var datosCarritos = new AccesoCarritos();
            var dataLibrosCarrito = new AccesoLibrosPorCarrito();
            var datosLibros = new AccesoLibros();

            int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
            int idLibro = Convert.ToInt32(e.CommandArgument);

            Dominio.Carrito carrito = datosCarritos.BuscarPorIdCliente(idCliente);
            var libro = datosLibros.BuscarPorIdLibro(idLibro);


            var nuevoItem = new LibroPorCarrito
            {
                IdCarrito = carrito.IdCarrito,
                IdLibro = idLibro,
                Cantidad = 1,
                PrecioUnitario = (decimal)libro.Precio
            };
            dataLibrosCarrito.Agregar(nuevoItem);

            CargarLibros(idCliente);
        }

        protected void Btn_IrApagar(object sender, CommandEventArgs e)
        {
            Response.Redirect("Pago.aspx");
        }
    }
}
