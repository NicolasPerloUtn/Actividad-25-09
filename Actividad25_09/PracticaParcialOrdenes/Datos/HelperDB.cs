using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Datos
{
    public class HelperDB
    {
        public SqlConnection conexion;
        public static HelperDB instance;
        public HelperDB() 
        { 
            conexion = new SqlConnection(@"");
        }

        public SqlConnection ObtenerConexion()
        {
            return this.conexion;
        }

        public static HelperDB ObtenerInstancia()
        {
            if (instance == null)
            {
                instance = new HelperDB();
            }
            return instance;
        }


        public int ProximaOrden(string parametro, string nombreSP)
        {
            int aux = 0;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            SqlParameter param = new SqlParameter(parametro, SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            comando.Parameters.Add(param);
            comando.ExecuteNonQuery();
            conexion.Close();
            aux = (int)param.Value;
            return aux;
        }


        public DataTable Consultar(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Close();
            return dt;
        }


        // Metodos de consultar

        internal DataTable Consultar(string nombreSP, List<Parametro> lParam)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            comando.Parameters.Clear();

            foreach (Parametro param in lParam)
            {
                comando.Parameters.AddWithValue(param.Nombre, param.Valor);
            }

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Close();
            return dt;
        }

        public int EjecutarSQL(string strSql, List<Parametro> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                conexion.Open();
                t = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (Parametro param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) { t.Rollback(); }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();

            }

            return afectadas;
        }


    }
}
