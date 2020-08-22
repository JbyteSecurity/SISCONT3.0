using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Datos
{

    public class DaoProveedor
    {
        private Conexion conexion = new Conexion();
        SqlCommand sqlCommand = new SqlCommand();

        public DataTable Show(string ruc)
        {
            try
            {
                SqlDataReader sqlDataReaderProvider;
                DataTable dataTableProvider = new DataTable();

                sqlCommand.Connection = conexion.OpenConnection();
                sqlCommand.CommandText = "sp_show_proveedor";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ruc", ruc);

                sqlCommand.ExecuteNonQuery();
                sqlDataReaderProvider = sqlCommand.ExecuteReader();
                dataTableProvider.Load(sqlDataReaderProvider);
                sqlCommand.Parameters.Clear();

                conexion.CloseConnection();

                if (dataTableProvider.Rows.Count > 0)
                    return dataTableProvider;
                else return null;
            } catch(Exception Ex) {
                Console.WriteLine(Ex);
                return null;
            }
        }


        public DataTable All()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTableSuppliers = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_all_proveedor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTableSuppliers.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTableSuppliers;

        }

        public bool Insert(string ruc, string razonSocial, string nombre, string correo, string direccion, string telefono, string web)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_insert_proveedor";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ruc", ruc);
            sqlCommand.Parameters.AddWithValue("@razon_social", razonSocial);
            sqlCommand.Parameters.AddWithValue("@nombre", nombre);
            sqlCommand.Parameters.AddWithValue("@mail", correo);
            sqlCommand.Parameters.AddWithValue("@direccion", direccion);
            sqlCommand.Parameters.AddWithValue("@telefono", telefono);
            sqlCommand.Parameters.AddWithValue("@web", web);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            return false;

        }

        public bool Update(int id, string ruc, string razonSocial, string nombre, string correo, string direccion, string telefono, string web)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_update_proveedor";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@ruc", ruc);
            sqlCommand.Parameters.AddWithValue("@razon_social", razonSocial);
            sqlCommand.Parameters.AddWithValue("@nombre", nombre);
            sqlCommand.Parameters.AddWithValue("@mail", correo);
            sqlCommand.Parameters.AddWithValue("@direccion", direccion);
            sqlCommand.Parameters.AddWithValue("@telefono", telefono);
            sqlCommand.Parameters.AddWithValue("@web", web);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            return false;
        }

        public bool Destroy(int id)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_destroy_proveedor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            return false;
        }
    }
}
