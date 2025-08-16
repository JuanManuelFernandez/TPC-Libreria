using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    internal class AccesoEditoriales
    {
        private List<Editorial> editoriales = null;
        private AccesoDatos datos = null;

        public List<Editorial> Listar()
        {
            editoriales = new List<Editorial>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT IDEditorial, Nombre FROM Editoriales");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Editorial
                    {
                        IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                    };

                    editoriales.Add(aux);
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
            return editoriales;
        }
        public void AgregarEditorial(Editorial nuevo)
        {
            datos = new AccesoDatos();
            var aux = new AccesoEditoriales();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Editoriales (IDEditorial, Nombre) VALUES (@IDEditorial, @Nombre)");
                datos.SetearParametro("@IDEditorial", aux.Listar()[aux.Listar().Count - 1].IdEditorial);

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
        public Editorial BuscarPorIdEditorial(int id)
        {
            datos = new AccesoDatos();
            Editorial aux;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDEditorial, Nombre FROM Editoriales WHERE IDEditorial = " + id);
                datos.Leer();
                datos.Lector.Read();

                aux = new Editorial
                {
                    IdEditorial = datos.Lector["IDEditorial"] != DBNull.Value ? (int)datos.Lector["IDEditorial"] : 0,
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
