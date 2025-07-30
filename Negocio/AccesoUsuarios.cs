using Dominio;
using System;
using System.Collections.Generic;

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
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Email, Clave, Eliminado FROM Usuarios");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Email, Clave, Eliminado FROM Usuarios WHERE TipoUsuario !=" + (int)TipoUsuario.Admin);
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Email, Clave, Eliminado FROM Usuarios WHERE TipoUsuario = " + 2);
                datos.Leer();
                while (datos.Lector.Read())
                {
                    var aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
                datos.Consultar("INSERT INTO Usuarios (TipoUsuario, Email, Clave) VALUES (@TipoUsuario, @Email, @Clave)");
                datos.SetearParametro("@TipoUsuario", nuevo.TipoUsuario);
                datos.SetearParametro("@Email", nuevo.Email);
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
                datos.Consultar("SELECT IdUsuario, TipoUsuario, Email, Clave FROM Usuarios WHERE Email = @Email AND Clave = @Clave");
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Clave", usuario.Clave);

                datos.Leer();
                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)datos.Lector["TipoUsuario"] == 1 ? TipoUsuario.Admin : (int)datos.Lector["TipoUsuario"] == 2 ? TipoUsuario.Empleado : TipoUsuario.Cliente;
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
        public Usuario BuscarUsuarioPorId(int id)
        {
            datos = new AccesoDatos();
            Usuario aux;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Email, Clave, Eliminado FROM Usuarios WHERE IDUsuario =" + id);
                datos.Leer();
                datos.Lector.Read();
                aux = new Usuario
                {
                    IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                    TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                    Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
        public void ActivarUsuarioConEmail(string email)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Usuarios SET Eliminado = 0 WHERE Email = @email");
                datos.SetearParametro("@email", email);
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
        public void ModificarUsuario(Usuario mod)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Usuarios SET TipoUsuario = @TipoUsuario, Email = @Email, Clave = @Clave, Eliminado = @Eliminado WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@TipoUsuario", mod.TipoUsuario);
                datos.SetearParametro("@Email", mod.Email);
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
        public bool VerificarEmail(string email)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM USUARIOS WHERE Email = '" + email + "'");
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
