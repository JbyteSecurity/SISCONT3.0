using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DaoComprobantePago
    {
        private Conexion conexion = new Conexion();

        SqlCommand sqlCommand = new SqlCommand();
        public DataTable AllCdpTypes()
        {
            SqlDataReader leer;
            DataTable dataTableCDPType = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_all_tipo_comprobante";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            leer = sqlCommand.ExecuteReader();
            dataTableCDPType.Load(leer);
            conexion.CloseConnection();
            return dataTableCDPType;

        }
    }
}