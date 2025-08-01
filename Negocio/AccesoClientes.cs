﻿using Dominio;
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
            datos.Consultar("SELECT C.IDCliente,IDCategoria,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Email,U.Clave,U.Eliminado FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario");
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
                            Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
                // Insertar en Usuarios
                datos.Conectar();
                datos.Consultar("INSERT INTO Usuarios (TipoUsuario, Email, Clave, Eliminado) VALUES (@TipoUsuario, @Email, @Clave, @Eliminado)");
                datos.SetearParametro("@TipoUsuario", nuevo.Usuario.TipoUsuario);
                datos.SetearParametro("@Clave", nuevo.Usuario.Clave);
                datos.SetearParametro("@Email", nuevo.Usuario.Email);
                datos.SetearParametro("@Eliminado", 0);
                datos.EjecutarNonQuery();
                datos.Cerrar();

                // Insertar en Clientes usando el IDUsuario recién generado
                datos.Conectar();
                datos.Consultar("INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono, IDCategoria) VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Telefono, @IDCategoria)");
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
        public Cliente BuscarClientePorIdUsuario(int id)
        {
            datos = new AccesoDatos();
            Cliente aux;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT C.IDCliente,IDCategoria,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Email,U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE U.IDUsuario = " + id);
                datos.Leer();
                datos.Lector.Read();
                aux = new Cliente
                {
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    Usuario = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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
        public bool BuscarClientePorEmail(string email)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT Email, Eliminado FROM Usuarios WHERE Email =" + email);
                datos.Leer();
                datos.Lector.Read();

                return datos.Lector["Email"] != DBNull.Value;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        //REVISAR ESTA FUNCION
        public Cliente BuscarClientePorDni(string dni)
        {
            datos = new AccesoDatos();
            Cliente aux = null;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT C.IDCliente, IDCategoria, C.IDUsuario, C.DNI, C.Nombre, C.Apellido, C.Telefono, U.TipoUsuario, U.Email, U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE C.DNI = @Dni");
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
                            Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
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

        public void ModificarCliente(Cliente mod)
        {
            datos = new AccesoDatos();
            var dataUsuarios = new AccesoUsuario();
            try
            {
                datos.Conectar();
                dataUsuarios.ModificarUsuario(mod.Usuario);
                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("UPDATE Clientes SET IDCategoria = @IDCategoria, DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE IDUsuario = @IDUsuario");
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
        public bool VerificarDni(string dni)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE Eliminado = 0 AND DNI = '" + dni + "'");
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
        public bool VerificaReactivar(Cliente aux)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE U.Eliminado = 1 AND C.DNI = '" + aux.Dni + "' AND U.Clave = '" + aux.Usuario.Clave + "' AND U.Email = '" + aux.Usuario.Email + "'");
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
        public bool VerificarTelefono(string telefono)
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
    }
}