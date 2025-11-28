using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoDeseados
    {
        private List<Deseado> deseados = null;
        private AccesoDatos datos = null;

        public List<Deseado> Listar()
        {
            deseados = new List<Deseado>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDDeseado, IDCliente, IDLibro FROM Deseados");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Deseado
                    {
                        IdDeseado = datos.Lector["IDDeseado"] != DBNull.Value ? (int)datos.Lector["IDDeseado"] : 0,
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0
                    };

                    deseados.Add(aux);
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
            return deseados;
        }
        public void AgregarDeseado(Deseado nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoDeseados();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Deseados (IDDeseado, IDCliente, IDLibro) VALUES (@IDDeseado, @IDCliente, @IDLibro)");
                datos.SetearParametro("@IDDeseado", aux.Listar()[aux.Listar().Count - 1].IdDeseado);

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
        public Deseado BuscarPorIdDeseado(int id)
        {
            datos = new AccesoDatos();
            Deseado aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDDeseado, IDCliente, IDLibro FROM Deseados WHERE IDDeseado = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Deseado
                {
                    IdDeseado = datos.Lector["IDDeseado"] != DBNull.Value ? (int)datos.Lector["IDDeseado"] : 0,
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
