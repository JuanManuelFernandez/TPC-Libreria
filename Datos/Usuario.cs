namespace Dominio
{
    public enum TipoUsuario
    {
        Admin = 1,
        Cliente = 2
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Mail { get; set; }
        public string Clave { get; set; }
        public bool Reporte { get; set; }
        public bool Eliminado { get; set; }

        public Usuario(int tipo, string email, string clave)
        {
            TipoUsuario = tipo == 1 ? TipoUsuario.Admin : TipoUsuario.Cliente;
            Mail = email;
            Clave = clave;
        }
        public Usuario()
        {
            TipoUsuario = TipoUsuario.Cliente;
            Mail = "user@mail.com";
            Clave = "password";
        }
    }
}