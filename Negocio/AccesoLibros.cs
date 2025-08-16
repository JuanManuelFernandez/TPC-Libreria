using Dominio;
using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Negocio
{
    internal class AccesoLibros
    {
        private List<Libro> libros = null;
        private AccesoDatos datos = null;

        public List<Libro> Listar()
        {
            libros = new List<Libro>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDLibro, IDAutor, IDGenero, IDEditorial, IDSucursal, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock FROM Libros");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Libro
                    {
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                        IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                        IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                        IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
                        IdSucursal = datos.Lector["IDSucursal"] != DBNull.Value ? (int)datos.Lector["IDSucursal"] : 0,
                        Titulo = datos.Lector["Titulo"] != DBNull.Value ? (string)datos.Lector["Titulo"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                        FechaPublicacion = datos.Lector["FechaPublicacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaPublicacion"] : DateTime.MaxValue,
                        Precio = datos.Lector["Precio"] != DBNull.Value ? (float)datos.Lector["Precio"] : 0,
                        Paginas = datos.Lector["Paginas"] != DBNull.Value ? (int)datos.Lector["Paginas"] : 0,
                        Stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0
                    };

                    libros.Add(aux);
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
            return libros;
        }
        public void AgregarLibro(Libro nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoLibros();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Libros (IDLibro, IDAutor, IDGenero, IDEditorial, IDSucursal, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock)" +
                                "VALUES (@IDLibro, @IDAutor, @IDGenero, @IDEditorial, @IDSucursal, @Titulo, @Descripcion, @FechaPublicacion, @Precio, @Paginas, @Stock)");
                datos.SetearParametro("@IDLibro", aux.Listar()[aux.Listar().Count - 1].IdLibro);

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
        public Libro BuscarPorIdLibro(int id)
        {
            datos = new AccesoDatos();
            Libro aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDLibro, IDAutor, IDGenero, IDEditorial, IDSucursal, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock FROM Libros WHERE IDLibro = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Libro
                {
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                    IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                    IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
                    IdSucursal = datos.Lector["IDSucursal"] != DBNull.Value ? (int)datos.Lector["IDSucursal"] : 0,
                    Titulo = datos.Lector["Titulo"] != DBNull.Value ? (string)datos.Lector["Titulo"] : string.Empty,
                    Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                    FechaPublicacion = datos.Lector["FechaPublicacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaPublicacion"] : DateTime.MaxValue,
                    Precio = datos.Lector["Precio"] != DBNull.Value ? (float)datos.Lector["Precio"] : 0,
                    Paginas = datos.Lector["Paginas"] != DBNull.Value ? (int)datos.Lector["Paginas"] : 0,
                    Stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0
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
