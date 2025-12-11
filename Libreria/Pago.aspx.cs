using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libreria
{
    public partial class Pago : System.Web.UI.Page
    {
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

        private string GenerarEmail(string nombreCliente, List<LibroPorCarrito> items)
        {
            var sb = new System.Text.StringBuilder();
            var dataLibros = new AccesoLibros();

            sb.Append(@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto;'>
                <h2 style='color:#111;'>¡Gracias por tu compra!</h2>");

            sb.Append($"<p>Hola {nombreCliente},</p>");
            sb.Append("<p>Tu compra se registró correctamente. Aquí tenés el detalle de lo adquirido:</p>");
            sb.Append("<hr>");

            sb.Append("<table style='width:100%; border-collapse: collapse;'>");

            decimal total = 0;

            foreach (var item in items)
            {
                Libro libro = dataLibros.BuscarPorIdLibro(item.IdLibro);
                decimal subtotal = item.Cantidad * item.PrecioUnitario;
                total += subtotal;

                sb.Append($@"
                <tr>
                    <td style='padding: 8px 0;'>
                        <strong>{libro.Titulo}</strong> × {item.Cantidad}
                    </td>
                    <td style='text-align:right; padding: 8px 0;'>
                        ${item.PrecioUnitario:N2} cada uno
                    </td>
                </tr>");
            }

            sb.Append($@"
    <tr>
        <td style='padding: 8px 0; font-weight: bold;'>Total</td>
        <td style='text-align:right; padding: 8px 0; font-weight: bold;'>${total:N2}</td>
    </tr>");

            sb.Append("</table>");
            sb.Append("<hr>");

            sb.Append("<p>Saludos,<br><strong>Librería Online</strong></p>");
            sb.Append("</div>");

            return sb.ToString();
        }


        protected void Btn_Comprar(object sender, EventArgs e)
        {
            // Verificacion de fecha de caducidad de tarjeta
            if (int.TryParse(TxtMes.Text, out int mes) && int.TryParse(TxtYear.Text, out int year))
            {
                var fechaActual = DateTime.Now;

                if (year < fechaActual.Year || (year == fechaActual.Year && mes < fechaActual.Month))
                {
                    LblCaducidad.Text = "La fecha de caducidad no puede ser menor a la fecha actual.";
                    LblCaducidad.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            Usuario usuario = (Usuario)Session["usuario"];
            var dataCli = new AccesoClientes();
            var cliente = dataCli.BuscarPorIdUsuario(usuario.IdUsuario);
            int idCliente = cliente.IdCliente;

            var emailService = new EmailService();
            var dataCompras = new AccesoCompras();
            var dataCarritos = new AccesoCarritos();
            var dataLibrosCarrito = new AccesoLibrosPorCarrito();

            // Obtener carrito
            var carrito = dataCarritos.BuscarPorIdCliente(idCliente);

            // Obtener items del carrito
            List<LibroPorCarrito> items = dataLibrosCarrito.ListarPorIdCarrito(carrito.IdCarrito);

            // Calcular total
            decimal TotalCompra = items.Sum(x => x.PrecioUnitario * x.Cantidad);

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

                // Genero y mandocorreo
                string nombreCompleto = cliente.Nombre + " " + cliente.Apellido;
                string contenido = GenerarEmail(nombreCompleto, items);
                emailService.ArmarMail(TxtMail.Text, "Realizaste una compra", contenido);
                emailService.EnviarEmail();

                dataCompras.ActualizarStock(idCliente);

                // Vacio carrito
                dataCarritos.Vaciar(carrito.IdCarrito);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Limpiar campos
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