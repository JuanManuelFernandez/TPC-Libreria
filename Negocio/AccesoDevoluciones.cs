using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoDevoluciones
    {
        private List<Devolucion> devoluciones = null;
        private AccesoDatos datos = null;

        public List<Devolucion> Listar()
        {
            devoluciones = new List<Devolucion>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDDevolucion, IDCliente, IDLibro, IDSucursal FROM Devoluciones");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Devolucion
                    {
                        IdDevolucion = datos.Lector["IDDevolucion"] != DBNull.Value ? (int)datos.Lector["IDDevolucion"] : 0,
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        IdCompra = datos.Lector["IDCompra"] != DBNull.Value ? (int)datos.Lector["IDCompra"] : 0,
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                        Descripcion = (string)datos.Lector["Descripcion"],
                        FechaDevolucion = datos.Lector["FechaDevolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaDevolucion"] : DateTime.MaxValue
                    };

                    devoluciones.Add(aux);
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
            return devoluciones;
        }
        public void AgregarDevolucion(Devolucion nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoDevoluciones();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Devoluciones (IDDevolucion, IDCliente, IDCompra, IDLibro, Descripcion, FechaDevolucion)" +
                                "VALUES (@IDDevolucion, @IDCliente, @IDCompra, @IDLibro, @Descripcion, @FechaDevolucion)");
                datos.SetearParametro("@IDDevolucion", aux.Listar()[aux.Listar().Count - 1].IdDevolucion);

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
        public Devolucion BuscarPorIdDevolucion(int id)
        {
            datos = new AccesoDatos();
            Devolucion aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDDevolucion, IDCliente, IDCompra, IDLibro, Descripcion, FechaDevolucion FROM Devoluciones WHERE IDDevolucion = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Devolucion
                {
                    IdDevolucion = datos.Lector["IDDevolucion"] != DBNull.Value ? (int)datos.Lector["IDDevolucion"] : 0,
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    IdCompra = datos.Lector["IDCompra"] != DBNull.Value ? (int)datos.Lector["IDCompra"] : 0,
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    Descripcion = (string)datos.Lector["Descripcion"],
                    FechaDevolucion = datos.Lector["FechaDevolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaDevolucion"] : DateTime.MaxValue
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
    }
}
