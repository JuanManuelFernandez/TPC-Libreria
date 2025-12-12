using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Deseados : System.Web.UI.Page
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
            var dataDeseados = new AccesoDeseados();
            var dataLibros = new AccesoLibros();

            var listaDeseados = dataDeseados.Listar()
                .Where(x => x.IdCliente == idCliente)
                .ToList();

            if (listaDeseados.Count == 0)
            {
                rptLibros.Visible = false;
                pnlNoLibros.Visible = true;
                return;
            }

            var libros = listaDeseados
                .Select(d => dataLibros.BuscarPorIdLibro(d.IdLibro))
                .ToList();

            rptLibros.DataSource = libros;
            rptLibros.DataBind();

            rptLibros.Visible = true;
            pnlNoLibros.Visible = false;
        }

        private void AgregarAlCarrito(int idCliente, int idLibro)
        {
            var datosCarritos = new AccesoCarritos();
            var datosLPC = new AccesoLibrosPorCarrito();
            var datosLibros = new AccesoLibros();

            Dominio.Carrito carrito = datosCarritos.BuscarPorIdCliente(idCliente);

            if (carrito == null)
            {
                int nuevoId = datosCarritos.CrearCarrito(idCliente);
                carrito = datosCarritos.BuscarPorIdCliente(idCliente);
            }

            Libro libro = datosLibros.BuscarPorIdLibro(idLibro);

            var nuevoItem = new Dominio.LibroPorCarrito
            {
                IdCarrito = carrito.IdCarrito,
                IdLibro = idLibro,
                Cantidad = 1,
                PrecioUnitario = (decimal)libro.Precio
            };

            try
            {
                datosLPC.Agregar(nuevoItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BorrarDeDeseados(int idCliente, int idLibro)
        {
            var dataDeseados = new AccesoDeseados();
            var lista = dataDeseados.Listar()
                .Where(x => x.IdCliente == idCliente && x.IdLibro == idLibro)
                .ToList();

            foreach (var d in lista)
                dataDeseados.Eliminar(d.IdDeseado);
        }

        protected void Btn_AgregarCarrito(object sender, CommandEventArgs e)
        {
            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();

                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                int idLibro = Convert.ToInt32(e.CommandArgument);

                AgregarAlCarrito(idCliente, idLibro);
                BorrarDeDeseados(idCliente, idLibro);
                CargarLibros(idCliente);
            }
        }

        protected void Btn_Eliminar(object sender, CommandEventArgs e)
        {
            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();

                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                int idLibro = Convert.ToInt32(e.CommandArgument);

                BorrarDeDeseados(idCliente, idLibro);
                CargarLibros(idCliente);
            }
        }
    }
}
