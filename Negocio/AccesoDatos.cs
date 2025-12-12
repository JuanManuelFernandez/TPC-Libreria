using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector = null;

        public SqlDataReader Lector => lector;
        public void Conectar()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TPCLibreriaUTN; integrated security=true"); //\\SQLEXPRESS
            comando = new SqlCommand();
        }
        public void Consultar(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Connection = conexion;
        }
        public void Leer()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        public void EjecutarNonQuery()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception er)
            {

                throw er;
            }
        }
        public object EjectuarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                conexion.Close();
            }
        }
        public void Cerrar()
        {
            lector?.Close();
            conexion.Close();
        }
        public void SetearParametro(string columna, object dato)
        {
            comando.Parameters.AddWithValue(columna, dato);
        }
        public object EjecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public object EjecutarScalarLibros()
        {
            comando.Connection = conexion;
            return comando.ExecuteScalar();
        }
        public void EjecutarNonQueryLibro()
        {
            comando.Connection = conexion;
            comando.ExecuteNonQuery();
        }
        public DataTable EjecutarReader()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
