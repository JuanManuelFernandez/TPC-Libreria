using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoAutores
    {
        private List<Autor> autores = null;
        private AccesoDatos datos = null;

        public List<Autor> Listar()
        {
            autores = new List<Autor>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDAutor, Nombre, Apellido FROM Autores");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Autor
                    {
                        IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty
                    };

                    autores.Add(aux);
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
            return autores;
        }
        public void AgregarAutor(Autor nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoAutores();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Autors (IDAutor, Nombre, Apellido) VALUES (@IDAutor, @Nombre, @Apellido)");
                datos.SetearParametro("@IDAutor", aux.Listar()[aux.Listar().Count - 1].IdAutor);

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
        public Autor BuscarPorIdAutor(int id)
        {
            datos = new AccesoDatos();
            Autor aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDAutor, Nombre, Apellido FROM Autores WHERE IDAutor = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Autor
                {
                    IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                    Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty
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
        public string ObtenerNombreCompleto(int idAutor)
        {
            try
            {
                Autor autor = BuscarPorIdAutor(idAutor);
                return $"{autor.Nombre} {autor.Apellido}".Trim();
            }
            catch (Exception)
            {
                return "Autor desconocido";
            }
        }
    }
}