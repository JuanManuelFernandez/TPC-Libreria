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
            datos.Consultar("SELECT IDLibro, IDAutor, IDGenero, IDEditorial, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock FROM Libros");
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
                        Titulo = datos.Lector["Titulo"] != DBNull.Value ? (string)datos.Lector["Titulo"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                        FechaPublicacion = datos.Lector["FechaPublicacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaPublicacion"] : DateTime.MaxValue,
                        Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["Precio"]) : 0,
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
                datos.Consultar("INSERT INTO Libros (IDLibro, IDAutor, IDGenero, IDEditorial, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock)" +
                                "VALUES (@IDLibro, @IDAutor, @IDGenero, @IDEditorial, @Titulo, @Descripcion, @FechaPublicacion, @Precio, @Paginas, @Stock)");
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
                datos.Consultar("SELECT IDLibro, IDAutor, IDGenero, IDEditorial, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock FROM Libros WHERE IDLibro = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Libro
                {
                    IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                    IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                    IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                    IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
                    Titulo = datos.Lector["Titulo"] != DBNull.Value ? (string)datos.Lector["Titulo"] : string.Empty,
                    Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                    FechaPublicacion = datos.Lector["FechaPublicacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaPublicacion"] : DateTime.MaxValue,
                    Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["Precio"]) : 0,
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
        public List<Libro> BuscarLibros(string termino)
        {
            libros = new List<Libro>();
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar(@"SELECT DISTINCT L.IDLibro, L.IDAutor, L.IDGenero, L.IDEditorial, 
                         L.Titulo, L.Descripcion, L.FechaPublicacion, L.Precio, L.Paginas, L.Stock
                         FROM Libros L
                         LEFT JOIN Autores A ON L.IDAutor = A.IDAutor
                         LEFT JOIN Editoriales E ON L.IDEditorial = E.IDEditorial
                         LEFT JOIN Generos G ON L.IDGenero = G.IDGenero
                         WHERE L.Titulo LIKE @termino 
                         OR (A.Nombre + ' ' + A.Apellido) LIKE @termino
                         OR A.Apellido LIKE @termino
                         OR E.Nombre LIKE @termino
                         OR G.Nombre LIKE @termino");

                datos.SetearParametro("@termino", "%" + termino + "%");
                datos.Leer();

                while (datos.Lector.Read())
                {
                    var aux = new Libro
                    {
                        IdLibro = datos.Lector["IDLibro"] != DBNull.Value ? (int)datos.Lector["IDLibro"] : 0,
                        IdAutor = datos.Lector["IDAutor"] != DBNull.Value ? (int)datos.Lector["IDAutor"] : 0,
                        IdGenero = datos.Lector["IDGenero"] != DBNull.Value ? (int)datos.Lector["IDGenero"] : 0,
                        IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
                        Titulo = datos.Lector["Titulo"] != DBNull.Value ? (string)datos.Lector["Titulo"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                        FechaPublicacion = datos.Lector["FechaPublicacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaPublicacion"] : DateTime.MaxValue,
                        Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["Precio"]) : 0,
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
    }
}
