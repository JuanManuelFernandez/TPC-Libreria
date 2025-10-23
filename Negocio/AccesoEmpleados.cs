using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoEmpleados
    {
        AccesoDatos datos;
        List<Empleado> empleados;
        public List<Empleado> Listar()
        {
            datos = new AccesoDatos();
            empleados = new List<Empleado>();

            datos.Conectar();
            datos.Consultar("SELECT IDEmpleado, IDUsuario, Nombre, Apellido FROM Empleados");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Empleado
                    {
                        IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0,
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty
                    };
                    empleados.Add(aux);
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
            return empleados;
        }
        public Empleado BuscarPorIdUsuario(int id)
        {
            datos = new AccesoDatos();
            empleados = new List<Empleado>();
            var aux = new Empleado();

            datos.Conectar();
            datos.Consultar("SELECT IDEmpleado, IDUsuario, Nombre, Apellido FROM Empleados WHERE IDUsuario = " + id);
            datos.Leer();
            datos.Lector.Read();
            aux.IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0;
            aux.IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0;
            aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
            aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;

            return aux;
        }
        public void AgregarEmpleado(Empleado nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Empleados (IDUsuario, Nombre, Apellido) VALUES (@IDUsuario, @Nombre, @Apellido)");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
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
        public void ModificarEmpleado(Empleado nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Empleados SET Nombre = @Nombre, Apellido = @Apellido WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@IDUsuario", nuevo.IdUsuario);
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
        public bool VerificarDni(string dni)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT 1 FROM Empleados E INNER JOIN Usuarios U ON E.IDUsuario = U.IDUsuario WHERE Eliminado = 0");
                datos.Leer();
                if (datos.Lector.Read())
                {
                    return true;
                }
                else
                {
                    return false;
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
        }
        public int ObtenerIdEmpleadoLogueado(int idUsuario)
        {
            datos = new AccesoDatos();
            var idEmpleado = 0;

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDEmpleado FROM Empleados WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", idUsuario);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    idEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0;
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
            return idEmpleado;
        }
    }
}