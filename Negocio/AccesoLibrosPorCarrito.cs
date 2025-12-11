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
        public bool ExisteElLibro(int idCarrito,int IdLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT COUNT(*) FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                datos.SetearParametro("IDCarrito", idCarrito);
                datos.SetearParametro("IDLibro", IdLibro);

                int count = (int)datos.EjecutarScalar();
                return count > 0;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public void Agregar(LibroPorCarrito nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                if(ExisteElLibro(nuevo.IdCarrito,nuevo.IdLibro))
                {
                    datos.Consultar("UPDATE LibrosPorCarrito SET Cantidad = Cantidad + 1 WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                    datos.SetearParametro("@IDCarrito", nuevo.IdCarrito);
                    datos.SetearParametro("@IDLibro", nuevo.IdLibro);
                }
                else
                {
                    datos.Consultar("INSERT INTO LibrosPorCarrito (IDCarrito, IDLibro, Cantidad, PrecioUnitario) " +
                                    "VALUES (@IDCarrito, @IDLibro, @Cantidad, @PrecioUnitario)");
                    datos.SetearParametro("@IDCarrito", nuevo.IdCarrito);
                    datos.SetearParametro("@IDLibro", nuevo.IdLibro);
                    datos.SetearParametro("@Cantidad", nuevo.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", nuevo.PrecioUnitario);
                }
                
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

                datos.Consultar("SELECT Cantidad FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                datos.SetearParametro("IDCarrito", idCarrito);
                datos.SetearParametro("IDLibro", idLibro);

                datos.Leer();

                if(datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector["Cantidad"];
                    datos.Cerrar();
                    datos.Conectar();

                    if (cantidad > 1)
                    {
                        datos.Consultar("UPDATE LibrosPorCarrito SET Cantidad = Cantidad - 1 WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                        datos.SetearParametro("@IDCarrito", idCarrito);
                        datos.SetearParametro("@IDLibro", idLibro);
                        datos.EjecutarNonQuery();
                    }
                    else
                    {
                        datos.Consultar("DELETE FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito AND IDLibro = @IDLibro");
                        datos.SetearParametro("@IDCarrito", idCarrito);
                        datos.SetearParametro("@IDLibro", idLibro);
                        datos.EjecutarNonQuery();
                    }
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
        }

    }
}
