using System;

namespace Dominio
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public int IdAutor { get; set; }
        public int IdGenero { get; set; }
        public int IdEditorial { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public float Precio { get; set; }
        public int Paginas { get; set; }
        public int Stock { get; set; }
        public string NombreAutor { get; set; }
    }
}