namespace Dominio
{
    public class LibroPorCarrito
    {
        public int IdLibrosPorCarrito { get; set; }
        public int IdCarrito { get; set; }
        public int IdLibro { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
