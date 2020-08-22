using System;
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

        public DataTable ShowById(int id)
        {
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();

            if (dataTable.Rows.Count > 0)
                return dataTable;
            else
                return null;
        }

        public DataTable ShowAcountFilter(bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar)
        {
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;

            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_filter";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@naturaleza", naturaleza);
            sqlCommand.Parameters.AddWithValue("@pago", pago);
            sqlCommand.Parameters.AddWithValue("@destino", destino);
            sqlCommand.Parameters.AddWithValue("@vnaturaleza", vnaturaleza);
            sqlCommand.Parameters.AddWithValue("@vcobrar", vcobrar);

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

        public DataTable ShowNaturaleza()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_naturaleza";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTable;
        }

        public DataTable ShowPago()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_pago";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTable;
        }

        public DataTable ShowDestino()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_destino";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTable;
        }

        public DataTable ShowNaturalezaV()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_vnaturaleza";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTable;
        }

        public DataTable ShowCobrar()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_show_plan_vcobrar";
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

        public bool Insert(int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar, int codigo_padre)
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
            sqlCommand.Parameters.AddWithValue("@vnaturaleza", vnaturaleza);
            sqlCommand.Parameters.AddWithValue("@vcobrar", vcobrar);
            sqlCommand.Parameters.AddWithValue("@codigo_padre", codigo_padre);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else
                return false;
        }

        public bool Update(int id, int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar, int codigo_padre)
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
            sqlCommand.Parameters.AddWithValue("@vnaturaleza", vnaturaleza);
            sqlCommand.Parameters.AddWithValue("@vcobrar", vcobrar);
            sqlCommand.Parameters.AddWithValue("@codigo_padre", codigo_padre);

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
            try
            {
                sqlCommand.Connection = conexion.OpenConnection();
                sqlCommand.CommandText = "sp_delete_plan";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            } catch(Exception Ex)
            {
                return false;
            }
        }
    }
}
