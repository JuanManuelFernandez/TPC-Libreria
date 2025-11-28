using System;

namespace Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdCliente { get; set; }
        public int IdLibro { get; set; }
    }
}