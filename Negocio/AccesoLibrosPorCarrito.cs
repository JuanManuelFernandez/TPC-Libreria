using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoLibrosPorCarrito
    {
        private AccesoDatos datos = null;

        public List<LibroPorCarrito> ListarPorCarrito(int idCarrito)
        {
            var lista = new List<LibroPorCarrito>();
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDLibrosPorCarrito, IDCarrito, IDLibro, Cantidad, PrecioUnitario FROM LibrosPorCarrito WHERE IDCarrito = @IDCarrito");
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
    }

}
