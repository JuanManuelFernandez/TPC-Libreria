using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoLibrosPorCarrito
    {
        private AccesoDatos datos = null;
        public List<LibroPorCarrito> ListarPorIdCarrito(int idCarrito)
        {
            var lista = new List<LibroPorCarrito>();
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDLibrosPorCarrito, IDCarrito, IDLibro, Cantidad, PrecioUnitario FROM LibrosPorCarrito " +
                                "WHERE IDCarrito = @IDCarrito");
                datos.SetearParametro("@IDCarrito", idCarrito);
                datos.Leer();

                while (datos.Lector.Read())
                {
                    lista.Add(new LibroPorCarrito
                    {
                        IdLibrosPorCarrito = (int)datos.Lector["IDLibrosPorCarrito"],
                        IdCarrito = (int)datos.Lector["IDCarrito"],
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    });
                }
            }
            finally
            {
                datos.Cerrar();
            }

            return lista;
        }
        public void Agregar(LibroPorCarrito nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO LibrosPorCarrito (IDCarrito, IDLibro, Cantidad, PrecioUnitario) " +
                                "VALUES (@IDCarrito, @IDLibro, @Cantidad, @PrecioUnitario)");
                datos.SetearParametro("@IDCarrito", nuevo.IdCarrito);
                datos.SetearParametro("@IDLibro", nuevo.IdLibro);
                datos.SetearParametro("@Cantidad", nuevo.Cantidad);
                datos.SetearParametro("@PrecioUnitario", nuevo.PrecioUnitario);

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
        public void EliminarLibro(int idCarrito, int idLibro)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                datos.SetearParametro("@IDCarrito", idCarrito);
                datos.SetearParametro("@IDLibro", idLibro);
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
