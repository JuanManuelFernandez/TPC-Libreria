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
        public void Agregar(Deseado nuevo)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Deseados (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", nuevo.IdCliente);
                datos.SetearParametro("@IDLibro", nuevo.IdLibro);

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
        public void Eliminar(int idDeseado)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("DELETE FROM Deseados WHERE IDDeseado = @ID");
                datos.SetearParametro("@ID", idDeseado);
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
        public Deseado BuscarPorIdDeseado(int idDeseado)
        {
            datos = new AccesoDatos();
            Deseado aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDDeseado, IDCliente, IDLibro FROM Deseados WHERE IDDeseado = " + idDeseado);
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
