using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoStocks
    {
        private List<Stock> stockes = null;
        private AccesoDatos datos = null;

        public List<Stock> Listar()
        {
            stockes = new List<Stock>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDStock, IdLibro, Cantidad FROM Stocks");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Stock
                    {
                        IdStock = datos.Lector["IDStock"] != DBNull.Value ? (int)datos.Lector["IDStock"] : 0,
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                        Cantidad = datos.Lector["Cantidad"] != DBNull.Value ? (int)datos.Lector["Cantidad"] : 0
                    };

                    stockes.Add(aux);
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
            return stockes;
        }
        public void AgregarStock(Stock nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoStocks();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Stocks (IDStock, IdLibro, Cantidad) VALUES (@IDStock, @IdLibro, @Cantidad)");
                datos.SetearParametro("@IDStock", aux.Listar()[aux.Listar().Count - 1].IdStock);

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
        public Stock BuscarPorIdStock(int id)
        {
            datos = new AccesoDatos();
            Stock aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDStock, IdLibro, Cantidad FROM Stocks WHERE IDStock = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Stock
                {
                    IdStock = datos.Lector["IDStock"] != DBNull.Value ? (int)datos.Lector["IDStock"] : 0,
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    Cantidad = datos.Lector["Cantidad"] != DBNull.Value ? (int)datos.Lector["Cantidad"] : 0
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
