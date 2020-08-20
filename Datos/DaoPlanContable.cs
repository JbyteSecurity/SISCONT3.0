using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DaoPlanContable
    {
        private Conexion conexion = new Conexion();
        SqlCommand sqlCommand = new SqlCommand();

        public string ShowAcount(string codigo)
        {
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_name_cuenta";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@codigo", codigo);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0]["Cuenta"].ToString();
            else
                return null;
        }

        public DataTable ShowAcountFilter(string clasificacion)
        {
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;

            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_filter";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@clasificacion", clasificacion);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();

            return dataTable;
        }

        public DataTable All()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_all_plan_contable";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTable;
        }

        //public DataTable Show(string usuario)
        //{
        //    SqlDataReader sqlDataReader;
        //    DataTable dataTable = new DataTable();

        //    sqlCommand.Connection = conexion.OpenConnection();
        //    sqlCommand.CommandText = "sp_show_user";
        //    sqlCommand.CommandType = CommandType.StoredProcedure;

        //    sqlCommand.Parameters.AddWithValue("@Usuario", usuario);

        //    sqlCommand.ExecuteNonQuery();
        //    sqlDataReader = sqlCommand.ExecuteReader();
        //    dataTable.Load(sqlDataReader);
        //    sqlCommand.Parameters.Clear();

        //    conexion.CloseConnection();
        //    return dataTable;
        //}

        public bool Insert(int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, int codigo_padre)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_insert_plan";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@cuenta", cuenta);
            sqlCommand.Parameters.AddWithValue("@uso", uso);
            sqlCommand.Parameters.AddWithValue("@naturaleza", naturaleza);
            sqlCommand.Parameters.AddWithValue("@pago", pago);
            sqlCommand.Parameters.AddWithValue("@destino", destino);
            sqlCommand.Parameters.AddWithValue("@codigo_pago", codigo_padre);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else
                return false;
        }

        public bool Update(int id, int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, int codigo_padre)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_update_plan";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@cuenta", cuenta);
            sqlCommand.Parameters.AddWithValue("@uso", uso);
            sqlCommand.Parameters.AddWithValue("@naturaleza", naturaleza);
            sqlCommand.Parameters.AddWithValue("@pago", pago);
            sqlCommand.Parameters.AddWithValue("@destino", destino);
            sqlCommand.Parameters.AddWithValue("@codigo_pago", codigo_padre);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else
                return false;
        }

        public bool Destroy(int id)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_delete_plan";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else
                return false;
        }

    }
}
