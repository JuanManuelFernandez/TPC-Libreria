using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoSucursales
    {
        private List<Sucursal> sucursales = null;
        private AccesoDatos datos = null;

        public List<Sucursal> Listar()
        {
            sucursales = new List<Sucursal>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDSucursal, Nombre, Direccion FROM Sucursales");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Sucursal
                    {
                        IdSucursal = datos.Lector["IDSucursal"] != DBNull.Value ? (int)datos.Lector["IDSucursal"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : string.Empty
                    };

                    sucursales.Add(aux);
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
            return sucursales;
        }
        public void AgregarSucursal(Sucursal nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoSucursales();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Sucursales (IDSucursal, Nombre, Direccion) VALUES (@IDSucursal, @Nombre, @Direccion)");
                datos.SetearParametro("@IDSucursal", aux.Listar()[aux.Listar().Count - 1].IdSucursal);

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
        public Sucursal BuscarPorIdSucursal(int id)
        {
            datos = new AccesoDatos();
            Sucursal aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDSucursal, Nombre, Direccion FROM Sucursales WHERE IDSucursal = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Sucursal
                {
                    IdSucursal = datos.Lector["IDSucursal"] != DBNull.Value ? (int)datos.Lector["IDSucursal"] : 0,
                    Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : string.Empty
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
