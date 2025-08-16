namespace Dominio
{
    public class Portada
    {
        public int IdPortada { get; set; }
        public int IdLibro { get; set; }
        public string Imagen { get; set; }
        // Actualmente es una URL, cambiar a recursos locales luego
    }
}