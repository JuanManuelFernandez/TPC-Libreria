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
    public partial class Pago : System.Web.UI.Page
    {
        private AccesoDatos datos = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtNumeroTarjeta.Attributes.Add("placeholder", "0000-0000-0000-0000");
            TxtTelefono.Attributes.Add("placeholder", "11-1111-1111");
        }
        private void AgregarCompra(int idCliente, decimal TotalCompra)
        {
            datos = new AccesoDatos();
            //Usuario
            var auxiliar = new AccesoUsuario();
            var usuario = new Usuario();
            //Cliente
            var dataCli = new AccesoClientes();
            var cliente = dataCli.Listar().Find(x => x.IdCliente == idCliente);

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Compras (FechaCompra, IDCliente, Correo, Nombre, Apellido, DFacturacion, Localidad, Codigo, Telefono, Total) " +
                                "VALUES (@FechaCompra, @IDCliente, @Correo, @Nombre, @Apellido, @DFacturacion, @Localidad, @Codigo, @Telefono, @Total)");
                datos.SetearParametro("@FechaCompra", DateTime.Now);
                datos.SetearParametro("@IDCliente", idCliente);
                datos.SetearParametro("@Correo", usuario.Mail);
                datos.SetearParametro("@Nombre", cliente.Nombre);
                datos.SetearParametro("@Apellido", cliente.Apellido);
                datos.SetearParametro("@DFacturacion", TxtDireccion.Text);
                datos.SetearParametro("@Localidad", TxtLocalidad.Text);
                datos.SetearParametro("@Codigo", TxtCodigoPostal.Text);
                datos.SetearParametro("@Telefono", TxtTelefono.Text);
                datos.SetearParametro("@Total", TotalCompra);

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
        private decimal CalcularTotalCompra(int idCliente)
        {
            decimal total = 0;
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT SUM(L.Precio * C.Cantidad) FROM Carrito C INNER JOIN Libros L ON C.IDLibro = L.IDLibro WHERE C.IDCliente = @IDCliente");
                datos.SetearParametro("@IDCliente", idCliente);
                var resultado = datos.EjectuarScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    total = Convert.ToDecimal(resultado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
            return total;
        }
        protected void Btn_Comprar(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            var dataCli = new AccesoClientes();
            int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente; //usuario.IdUsuario ;

            decimal TotalCompra = CalcularTotalCompra(idCliente);
            try
            {
                AgregarCompra(idCliente, TotalCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TxtNumeroTarjeta.Text = "";
            TxtYear.Text = "";
            TxtMes.Text = "";
            TxtCodigoSeguridad.Text = "";
            TxtCorreo.Text = "";
            TxtNombre.Text = "";
            TxtApellido.Text = "";
            TxtDireccion.Text = "";
            TxtTelefono.Text = "";
            TxtLocalidad.Text = "";
            TxtCodigoPostal.Text = "";

            Response.Redirect("PagoCompletado.aspx");
        }
    }
}