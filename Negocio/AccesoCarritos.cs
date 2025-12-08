using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoCarritos
    {
        private List<Carrito> carritos = null;
        private AccesoDatos datos = null;

        public List<Carrito> Listar()
        {
            carritos = new List<Carrito>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDCarrito, IDCliente, IDLibro FROM Carritos");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Carrito
                    {
                        IdCarrito = datos.Lector["IDCarrito"] != DBNull.Value ? (int)datos.Lector["IDCarrito"] : 0,
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0
                    };

                    carritos.Add(aux);
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
            return carritos;
        }
        public void Agregar(Carrito nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoCarritos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Carritos (IDCarrito, IDCliente, IDLibro) VALUES (@IDCarrito, @IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCarrito", aux.Listar()[aux.Listar().Count - 1].IdCarrito);

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
        public void Vaciar(int idCliente)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM Carrito WHERE IDCliente = @IDCliente");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public Carrito BuscarPorIdCarrito(int id)
        {
            datos = new AccesoDatos();
            Carrito aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCarrito, IDCliente, IDLibro FROM Carritos WHERE IDCarrito = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Carrito
                {
                    IdCarrito = datos.Lector["IDCarrito"] != DBNull.Value ? (int)datos.Lector["IDCarrito"] : 0,
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0
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
