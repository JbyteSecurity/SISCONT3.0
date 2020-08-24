using System.Data;
using System.Data.SqlClient;


namespace Datos
{
    public class DaoPdt
    {
        Conexion conexion = new Conexion();

        SqlCommand sqlCommand = new SqlCommand();

        public DataTable PDT(string ruc, int anio)
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();

            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_pdt";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ruc", ruc);
            sqlCommand.Parameters.AddWithValue("@anio", anio);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();
            return dataTable;
        }
    }
}
