namespace Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public Usuario Usuario { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
    }
}
