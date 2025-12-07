using System;

namespace Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdCliente { get; set; }
        public int IdLibro { get; set; }
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DFacturacion { get; set; }
        public string Localidad { get; set; }
        public string Codigo { get; set; }
        public string Telefono { get; set; }
        public decimal Total { get; set; }
    }
}