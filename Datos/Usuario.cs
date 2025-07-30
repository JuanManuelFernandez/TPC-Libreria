namespace Dominio
{
    public enum TipoUsuario
    {
        Admin = 1,
        Empleado = 2,
        Cliente = 3,
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool Reporte { get; set; }
        public bool Eliminado { get; set; }

        public Usuario(int tipo, string email, string clave)
        {
            TipoUsuario = tipo == 1 ? TipoUsuario.Admin : tipo == 2 ? TipoUsuario.Empleado : TipoUsuario.Cliente;
            Email = email;
            Clave = clave;
        }
        public Usuario()
        {
            TipoUsuario = TipoUsuario.Cliente;
            Email = "user@mail.com";
            Clave = "password";
        }
    }
}
