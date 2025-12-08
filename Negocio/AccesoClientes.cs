using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoClientes
    {
        private List<Cliente> clientes = null;
        private AccesoDatos datos = null;

        public List<Cliente> Listar()
        {
            clientes = new List<Cliente>();
            datos = new AccesoDatos();

            // Unimos Clientes y Usuarios mediante IDUsuario
            datos.Conectar();
            datos.Consultar(
                "SELECT C.IDCliente,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Mail,U.Clave,U.Eliminado FROM Clientes C " +
                "INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Cliente
                    {
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        Usuario = new Usuario
                        {
                            IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                            TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                            Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                            Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty,
                            Eliminado = datos.Lector["Eliminado"] == DBNull.Value || (bool)datos.Lector["Eliminado"]
                        },
                        Dni = datos.Lector["DNI"] != DBNull.Value ? (string)datos.Lector["DNI"] : string.Empty,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty
                    };

                    clientes.Add(aux);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
            return clientes;
        }
        public void AgregarCliente(Cliente nuevo)
        {
            datos = new AccesoDatos();
            var auxiliar = new AccesoUsuario();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Telefono)");
                datos.SetearParametro("@IDUsuario", auxiliar.Listar()[(auxiliar.Listar().Count) - 1].IdUsuario);
                datos.SetearParametro("@DNI", nuevo.Dni);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Telefono", nuevo.Telefono);

                datos.EjecutarNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public Cliente BuscarPorIdUsuario(int id)
        {
            datos = new AccesoDatos();
            Cliente aux;
            try
            {
                datos.Conectar();
                datos.Consultar(
                    "SELECT C.IDCliente,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Mail,U.Clave FROM Clientes C " +
                    "INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario " +
                    "WHERE U.IDUsuario = " + id);
                datos.Leer();
                datos.Lector.Read();
                aux = new Cliente
                {
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    Usuario = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                        Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                        Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty
                    },
                    Dni = datos.Lector["DNI"] != DBNull.Value ? (string)datos.Lector["DNI"] : string.Empty,
                    Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                    Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty
                };
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
            return aux;
        }
        public bool BuscarPorMail(string mail)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT Mail, Eliminado FROM Usuarios WHERE Mail =" + mail);
                datos.Leer();
                datos.Lector.Read();

                return datos.Lector["Mail"] != DBNull.Value;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public Cliente BuscarPorDni(string dni)
        {
            datos = new AccesoDatos();
            Cliente aux = null;

            try
            {
                datos.Conectar();
                datos.Consultar(
                    "SELECT C.IDCliente, C.IDUsuario, C.DNI, C.Nombre, C.Apellido, C.Telefono, U.TipoUsuario, U.Mail, U.Clave FROM Clientes C " +
                    "INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario " +
                    "WHERE C.DNI = @Dni");
                datos.SetearParametro("@Dni", dni);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    aux = new Cliente
                    {
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        Usuario = new Usuario
                        {
                            IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                            TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                            Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                            Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty
                        },
                        Dni = datos.Lector["DNI"] != DBNull.Value ? (string)datos.Lector["DNI"] : string.Empty,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty
                    };
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
            return aux;
        }
        public void Modificar(Cliente mod)
        {
            datos = new AccesoDatos();
            var dataUsuarios = new AccesoUsuario();
            try
            {
                datos.Conectar();
                dataUsuarios.Modificar(mod.Usuario);
                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("UPDATE Clientes SET DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@DNI", mod.Dni);
                datos.SetearParametro("@Nombre", mod.Nombre);
                datos.SetearParametro("@Apellido", mod.Apellido);
                datos.SetearParametro("@Telefono", mod.Telefono);
                datos.SetearParametro("@IDUsuario", mod.Usuario.IdUsuario);
                datos.EjecutarNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        // Validaciones
        public bool ValidarDni(string dni)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar(
                    "SELECT 1 FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario " +
                    "WHERE Eliminado = 0 AND DNI = '" + dni + "'");
                datos.Leer();
                return datos.Lector.Read();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public bool ValidarTelefono(string telefono)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM Clientes WHERE Telefono = '" + telefono + "'");
                datos.Leer();
                return datos.Lector.Read();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public bool ValidarReactivar(Cliente aux)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar(
                    "SELECT 1 FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE U.Eliminado = 1 AND " +
                    "C.DNI = '" + aux.Dni + "' AND " +
                    "U.Clave = '" + aux.Usuario.Clave + "' AND " +
                    "U.Mail = '" + aux.Usuario.Mail + "'");
                datos.Leer();
                return datos.Lector.Read();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
    }
}