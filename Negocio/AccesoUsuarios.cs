using Dominio;
using System;
using System.Collections.Generic;
using System.Net;

namespace Negocio
{
    public class AccesoUsuario
    {
        private List<Usuario> usuarios = null;
        private AccesoDatos datos = null;

        public List<Usuario> Listar()
        {
            usuarios = new List<Usuario>();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Mail, Clave, Eliminado FROM Usuarios");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                        Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty,
                        Eliminado = datos.Lector["Eliminado"] != DBNull.Value && (bool)datos.Lector["Eliminado"],
                    };

                    usuarios.Add(aux);
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
            return usuarios;
        }
        public List<Usuario> ListarUsuarios()
        {
            usuarios = new List<Usuario>();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Mail, Clave, Eliminado FROM Usuarios WHERE TipoUsuario !=" + (int)TipoUsuario.Admin);
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                        Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty,
                        Eliminado = datos.Lector["Eliminado"] != DBNull.Value && (bool)datos.Lector["Eliminado"],
                    };

                    usuarios.Add(aux);
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
            return usuarios;
        }

        public List<Usuario> ListarEmpleados()
        {
            usuarios = new List<Usuario>();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Mail, Clave, Eliminado FROM Usuarios WHERE TipoUsuario = " + 2);
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                        Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty,
                        Eliminado = datos.Lector["Eliminado"] != DBNull.Value && (bool)datos.Lector["Eliminado"],
                    };

                    usuarios.Add(aux);
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
            return usuarios;
        }
        public void AgregarUsuario(Usuario nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Usuarios (TipoUsuario, Mail, Clave) VALUES (@TipoUsuario, @Mail, @Clave)");
                datos.SetearParametro("@TipoUsuario", nuevo.TipoUsuario);
                datos.SetearParametro("@Mail", nuevo.Mail);
                datos.SetearParametro("@Clave", nuevo.Clave);
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
        public bool Loguear(Usuario usuario)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IdUsuario, TipoUsuario, Mail, Clave FROM Usuarios WHERE Mail = @Mail AND Clave = @Clave AND Eliminado = 0");
                datos.SetearParametro("@Mail", usuario.Mail);
                datos.SetearParametro("@Clave", usuario.Clave);

                datos.Leer();
                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)datos.Lector["TipoUsuario"] == 1 ? TipoUsuario.Cliente : TipoUsuario.Admin;
                    return true;
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
            return false;
        }

        public Usuario BuscarPorId(int id)
        {
            datos = new AccesoDatos();
            Usuario aux;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Mail, Clave, Eliminado FROM Usuarios WHERE IDUsuario =" + id);
                datos.Leer();
                datos.Lector.Read();
                aux = new Usuario
                {
                    IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                    TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                    Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                    Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty,
                    Eliminado = datos.Lector["Eliminado"] != DBNull.Value && (bool)datos.Lector["Eliminado"],
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
        public Cliente BuscarClientePorMail(string mail)
        {
            datos = new AccesoDatos();
            Cliente aux = null;

            try
            {
                datos.Conectar();
                datos.Consultar(
                    "SELECT U.IDUsuario, U.TipoUsuario, U.Mail, U.Clave, U.Eliminado, " +
                    "C.IDCliente, C.DNI, C.Nombre, C.Apellido, C.Telefono " +
                    "FROM Usuarios U " +
                    "INNER JOIN Clientes C ON U.IDUsuario = C.IDUsuario " +
                    "WHERE U.Mail =  '" + mail + "'");
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
        public void Modificar(Usuario mod)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Usuarios SET TipoUsuario = @TipoUsuario, Mail = @Mail, Clave = @Clave, Eliminado = @Eliminado WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@TipoUsuario", mod.TipoUsuario);
                datos.SetearParametro("@Mail", mod.Mail);
                datos.SetearParametro("@Clave", mod.Clave);
                datos.SetearParametro("@Eliminado", mod.Eliminado);
                datos.SetearParametro("@IDUsuario", mod.IdUsuario);
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
        public bool MailExiste(string mail)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM USUARIOS WHERE Mail = '" + mail + "'");
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
        public void EliminarUsuarioId(int id)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Usuarios SET Eliminado = 1 WHERE IDUsuario =" + id);
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
        public void ActivarUsuario(int id)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Usuarios SET Eliminado = 0 WHERE IDUsuario =" + id);
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
    }
}