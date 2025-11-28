using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoCompras
    {
        private List<Compra> compras = null;
        private AccesoDatos datos = null;

        public List<Compra> Listar()
        {
            compras = new List<Compra>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDCompra, IDCliente, IDLibro, FROM Compras");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Compra
                    {
                        IdCompra = datos.Lector["IDCompra"] != DBNull.Value ? (int)datos.Lector["IDCompra"] : 0,
                        FechaCompra = datos.Lector["FechaCompra"] != DBNull.Value ? (DateTime)datos.Lector["FechaCompra"] : DateTime.MaxValue,
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    };

                    compras.Add(aux);
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
            return compras;
        }
        public void AgregarCompra(Compra nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoCompras();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Compras (IDCompra, IDCliente, IDLibro) VALUES (@IDCompra, @IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCompra", aux.Listar()[aux.Listar().Count - 1].IdCompra);

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
        public Compra BuscarPorIdCompra(int id)
        {
            datos = new AccesoDatos();
            Compra aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCompra, IDCliente, IDLibro FROM Compras WHERE IDCompra = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Compra
                {
                    IdCompra = datos.Lector["IDCompra"] != DBNull.Value ? (int)datos.Lector["IDCompra"] : 0,
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
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
