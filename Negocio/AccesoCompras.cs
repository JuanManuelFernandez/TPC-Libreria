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
            datos.Consultar("SELECT IDCompra, FechaCompra, IDCliente, IDLibro, Mail, Nombre, Apellido, DFacturacion, Localidad, Codigo, Telefono, Total FROM Compras");
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
                        Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        DFacturacion = datos.Lector["DFacturacion"] != DBNull.Value ? (string)datos.Lector["DFacturacion"] : string.Empty,
                        Localidad = datos.Lector["Localidad"] != DBNull.Value ? (string)datos.Lector["Localidad"] : string.Empty,
                        Codigo = datos.Lector["Codigo"] != DBNull.Value ? (string)datos.Lector["Codigo"] : string.Empty,
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty,
                        Total = datos.Lector["Total"] != DBNull.Value ? (decimal)datos.Lector["Total"] : 0
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

        public void Agregar(Compra nuevo)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Compras (FechaCompra, IDCliente, Mail, Nombre, Apellido, DFacturacion, Localidad, Codigo, Telefono, Total)" +
                                "VALUES (@FechaCompra, @IDCliente, @Mail, @Nombre, @Apellido, @DFacturacion, @Localidad, @Codigo, @Telefono, @Total)");
                datos.SetearParametro("@FechaCompra", nuevo.FechaCompra);
                datos.SetearParametro("@IDCliente", nuevo.IdCliente);
                datos.SetearParametro("@Mail", nuevo.Mail);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@DFacturacion", nuevo.DFacturacion);
                datos.SetearParametro("@Localidad", nuevo.Localidad);
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Telefono", nuevo.Telefono);
                datos.SetearParametro("@Total", nuevo.Total);

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

        public decimal ObtenerTotal(int idCompra)
        {
            datos = new AccesoDatos();
            decimal total = 0;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT SUM(Subtotal) FROM LibrosPorCompra WHERE IDCompra = @IDCompra");
                datos.SetearParametro("@IDCompra", idCompra);

                object resultado = datos.EjectuarScalar();

                if (resultado != null && resultado != DBNull.Value)
                {
                    total = (decimal)resultado;
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

            return total;
        }

        public Compra BuscarPorIdCompra(int id)
        {
            datos = new AccesoDatos();
            Compra aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCompra, FechaCompra, IDCliente, IDLibro, Mail, Nombre, Apellido, DFacturacion, Localidad, Codigo, Telefono, Total FROM Compras WHERE IDCompra = @IDCompra");
                datos.SetearParametro("@IDCompra", id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Compra
                {
                    IdCompra = datos.Lector["IDCompra"] != DBNull.Value ? (int)datos.Lector["IDCompra"] : 0,
                    FechaCompra = datos.Lector["FechaCompra"] != DBNull.Value ? (DateTime)datos.Lector["FechaCompra"] : DateTime.MaxValue,
                    IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    Mail = datos.Lector["Mail"] != DBNull.Value ? (string)datos.Lector["Mail"] : string.Empty,
                    Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                    DFacturacion = datos.Lector["DFacturacion"] != DBNull.Value ? (string)datos.Lector["DFacturacion"] : string.Empty,
                    Localidad = datos.Lector["Localidad"] != DBNull.Value ? (string)datos.Lector["Localidad"] : string.Empty,
                    Codigo = datos.Lector["Codigo"] != DBNull.Value ? (string)datos.Lector["Codigo"] : string.Empty,
                    Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty,
                    Total = datos.Lector["Total"] != DBNull.Value ? (decimal)datos.Lector["Total"] : 0
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
