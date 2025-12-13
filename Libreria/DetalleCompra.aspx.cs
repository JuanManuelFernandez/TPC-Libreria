using Dominio;
using Negocio;
using System;
using System.Web.UI;

namespace Libreria
{
    public partial class DetalleCompra : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            if(usuario.TipoUsuario == TipoUsuario.Cliente)
            {
                lblCliente.Visible = false;
                btnSiguienteEtapa.Visible = false;
            }
            if (!IsPostBack)
            {
                int idCompra = Convert.ToInt32(Request.QueryString["IDCompra"]);
                CargarDetalle(idCompra);
            }
        }

        private void CargarDetalle(int idCompra)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT C.IDCompra, C.Estado, CL.Nombre, CL.Apellido FROM Compras C INNER JOIN Clientes CL ON C.IDCliente = CL.IDCliente WHERE IDCompra = @IDCompra");
                datos.SetearParametro("@IDCompra", idCompra);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    lblIDCompra.Text = datos.Lector["IDCompra"].ToString();
                    lblCliente.Text = "Cliente: " + datos.Lector["Nombre"].ToString() + " " + datos.Lector["Apellido"].ToString();

                    string estado = datos.Lector["Estado"].ToString();
                    switch (estado)
                    {
                        case "Pedido en preparación":
                            litEstado.Text = "<img src='assets/reloj.png' style='width:35px;height:35px;'/> Pedido en preparación";
                            break;
                        case "Enviado":
                            litEstado.Text = "<img src='assets/camion.png' style='width:35px;height:35px;'/> Enviado";
                            break;
                        case "Entregado":
                            litEstado.Text = "<img src='assets/caja.png' style='width:35px;height:35px;'/> Entregado";
                            btnSiguienteEtapa.Visible = false;
                            break;
                    }
                }
            }
            finally
            {
                datos.Cerrar();
            }
        }
        protected void BtnSiguienteEtapa_Click(object sender, EventArgs e)
        {
            int idCompra = Convert.ToInt32(Request.QueryString["IDCompra"]);
            string estadoActual = litEstado.Text;
            string nuevoEstado = "";

            if (estadoActual.Contains("Pedido")) nuevoEstado = "Enviado";
            else if (estadoActual.Contains("Enviado")) nuevoEstado = "Entregado";
            else nuevoEstado = "Entregado";

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Compras SET Estado = @Estado WHERE IDCompra = @IDCompra");
                datos.SetearParametro("@Estado", nuevoEstado);
                datos.SetearParametro("@IDCompra", idCompra);
                datos.EjecutarNonQuery();

                CargarDetalle(idCompra);
            }
            finally
            {
                datos.Cerrar();
            }
        }
    }
}