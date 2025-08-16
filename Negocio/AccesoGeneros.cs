using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoGeneros
    {
        private List<Genero> generos = null;
        private AccesoDatos datos = null;

        public List<Genero> Listar()
        {
            generos = new List<Genero>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDGenero, Nombre FROM Generos");
            datos.Leer();
            
            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Genero
                    {
                        IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    };

                    generos.Add(aux);
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
            return generos;
        }
        public void AgregarGenero(Genero nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoGeneros();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Generos (IDGenero, Nombre) VALUES (@IDGenero, @Nombre)");
                datos.SetearParametro("@IDGenero", aux.Listar()[aux.Listar().Count - 1].IdGenero);

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
        public Genero BuscarPorIdGenero(int id)
        {
            datos = new AccesoDatos();
            Genero aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDGenero, Nombre FROM Generos WHERE IDGenero = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Genero
                {
                    IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                    Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
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
