using Dominio;
using Negocio;
using System;

namespace Libreria
{
    public partial class Pago : System.Web.UI.Page
    {
        private AccesoDatos datos = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];

                if (usuario != null) //No valido TipoUsuario!!
                {
                    try
                    {
                        var dataCli = new AccesoClientes();

                        //Autofill
                        TxtMail.Text = usuario.Mail;
                        TxtNombre.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).Nombre;
                        TxtApellido.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).Apellido;
                        TxtTelefono.Text = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).Telefono;

                        //Calculo total a pagar
                        var cliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario);
                        var dataCompras = new AccesoCompras();

                        if (cliente != null)
                        {
                            int idCliente = cliente.IdCliente;
                            decimal total = dataCompras.CalcularTotalCompra(idCliente);
                            LblTotal.Text = "Monto a pagar: " + total.ToString("C"); // C para que se muestre como moneda.
                        }
                    }
                    catch (Exception ex)
                    {
                        LblTotal.Text = "Error al calcular el total: " + ex;
                    }
                }
            }
        }

        protected void Btn_Comprar(object sender, EventArgs e)
        {
            int mes, year;
            if(int.TryParse(TxtMes.Text, out mes) && int.TryParse(TxtYear.Text, out year))
            {
                var fechaActual = DateTime.Now;

                if(year < fechaActual.Year || (year == fechaActual.Year && mes < fechaActual.Month))
                {
                    LblCaducidad.Text = "La fecha de caducidad no puede ser menor a la fecha actual.";
                    LblCaducidad.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            Usuario usuario = (Usuario)Session["usuario"];
            var dataCli = new AccesoClientes();
            Cliente cliente = dataCli.BuscarPorIdUsuario(usuario.IdUsuario);
            int idCliente = cliente.IdCliente;

            var dataCompras = new AccesoCompras();
            var dataStocks = new AccesoStocks();
            var dataCarritos = new AccesoCarritos();

            decimal TotalCompra = dataCompras.CalcularTotalCompra(idCliente);

            Compra nuevaCompra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdCliente = idCliente,
                Mail = usuario.Mail,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DFacturacion = TxtDireccion.Text,
                Localidad = TxtLocalidad.Text,
                Codigo = TxtCodigoPostal.Text,
                Telefono = TxtTelefono.Text,
                Total = TotalCompra
            };

            try
            {
                dataCompras.Agregar(nuevaCompra);
                dataStocks.Actualizar(idCliente); // Quizas tenga mas sentido actualizar el stock en base a la misma compra
                dataCarritos.Vaciar(idCliente);
            }
            catch (Exception er)
            {
                throw er;
            }

            TxtNumeroTarjeta.Text = "";
            TxtYear.Text = "";
            TxtMes.Text = "";
            TxtCodigoSeguridad.Text = "";
            TxtMail.Text = "";
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