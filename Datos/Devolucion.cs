using System;

namespace Dominio
{
    public class Devolucion
    {
        public int IdDevolucion { get; set; }
        public int IdCliente { get; set; }
        public int IdCompra { get; set; }
        public int IdLibro { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}