using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DaoLeasing
    {
        private Conexion conexion = new Conexion();
        SqlCommand sqlCommand = new SqlCommand();

        public DataTable AllFilter(int mes, int usuario, string ruc)
        {
            try
            {
                SqlDataReader sqlDataReaderProvider;
                DataTable dataTableProvider = new DataTable();

                sqlCommand.Connection = conexion.OpenConnection();
                sqlCommand.CommandText = "sp_all_leasing_filter";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@mes", mes);
                sqlCommand.Parameters.AddWithValue("@usuario", usuario);
                sqlCommand.Parameters.AddWithValue("@ruc", ruc);

                sqlCommand.ExecuteNonQuery();
                sqlDataReaderProvider = sqlCommand.ExecuteReader();
                dataTableProvider.Load(sqlDataReaderProvider);
                sqlCommand.Parameters.Clear();

                conexion.CloseConnection();

                if (dataTableProvider.Rows.Count > 0)
                    return dataTableProvider;
                else return null;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                return null;
            }
        }

        public bool Insert(
            int mes, int numero_cuota, string fecha_vencimiento, string fecha_formalizacion, double dolares_capital, double dolares_interes,
            double dolares_comision_seguros, double dolares_valor_cuota, double dolares_igv, double dolares_total_cuota, double dolares_saldo,
            double fecha_emision_tipo_cambio, double soles_saldo, double soles_capital, double soles_interes, double soles_comision_seguros,
            double soles_valor_cuota, double soles_igv, double soles_total_cuota, double fecha_pago_soles_capital, double fecha_pago_soles_interes,
            double fecha_pago_soles_comision_seguros, double fecha_pago_soles_igv, double fecha_pago_soles_total_cuota, double fecha_pago_soles_saldo,
            double diferencia_cambio_capital, double diferencia_cambio_interes, double diferencia_cambio_igv, double diferencia_cambio_total,
            double fecha_pago, double leasing, double entidad, DateTime fecha_registro, DateTime fecha_modificacion, int usuario, string ruc)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_insert_leasing";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@mes", mes);
            sqlCommand.Parameters.AddWithValue("@numero_cuota", numero_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_vencimiento", fecha_vencimiento);
            sqlCommand.Parameters.AddWithValue("@fecha_formalizacion", fecha_formalizacion);
            sqlCommand.Parameters.AddWithValue("@dolares_capital", dolares_capital);
            sqlCommand.Parameters.AddWithValue("@dolares_interes", dolares_interes);
            sqlCommand.Parameters.AddWithValue("@dolares_comision_seguros", dolares_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@dolares_valor_cuota", dolares_valor_cuota);
            sqlCommand.Parameters.AddWithValue("@dolares_igv", dolares_igv);
            sqlCommand.Parameters.AddWithValue("@dolares_total_cuota", dolares_total_cuota);
            sqlCommand.Parameters.AddWithValue("@dolares_saldo", dolares_saldo);
            sqlCommand.Parameters.AddWithValue("@fecha_emision_tipo_cambio", fecha_emision_tipo_cambio);
            sqlCommand.Parameters.AddWithValue("@soles_saldo", soles_saldo);
            sqlCommand.Parameters.AddWithValue("@soles_capital", soles_capital);
            sqlCommand.Parameters.AddWithValue("@soles_interes", soles_interes);
            sqlCommand.Parameters.AddWithValue("@soles_comision_seguros", soles_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@soles_valor_cuota", soles_valor_cuota);
            sqlCommand.Parameters.AddWithValue("@soles_igv", soles_igv);
            sqlCommand.Parameters.AddWithValue("@soles_total_cuota", soles_total_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_capital", fecha_pago_soles_capital);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_interes", fecha_pago_soles_interes);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_comision_seguros", fecha_pago_soles_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_igv", fecha_pago_soles_igv);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_total_cuota", fecha_pago_soles_total_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_saldo", fecha_pago_soles_saldo);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_capital", diferencia_cambio_capital);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_interes", diferencia_cambio_interes);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_igv", diferencia_cambio_igv);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_total", diferencia_cambio_total);
            sqlCommand.Parameters.AddWithValue("@fecha_pago", fecha_pago);
            sqlCommand.Parameters.AddWithValue("@leasing", leasing);
            sqlCommand.Parameters.AddWithValue("@entidad", entidad);
            sqlCommand.Parameters.AddWithValue("@fecha_registro", fecha_registro);
            sqlCommand.Parameters.AddWithValue("@fecha_modificacion", fecha_modificacion);
            sqlCommand.Parameters.AddWithValue("@usuario", usuario);
            sqlCommand.Parameters.AddWithValue("@ruc", ruc);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            return false;
        }

        public bool Update(
            int id, int mes, int numero_cuota, string fecha_vencimiento, string fecha_formalizacion, double dolares_capital, double dolares_interes,
            double dolares_comision_seguros, double dolares_valor_cuota, double dolares_igv, double dolares_total_cuota, double dolares_saldo,
            double fecha_emision_tipo_cambio, double soles_saldo, double soles_capital, double soles_interes, double soles_comision_seguros,
            double soles_valor_cuota, double soles_igv, double soles_total_cuota, double fecha_pago_soles_capital, double fecha_pago_soles_interes,
            double fecha_pago_soles_comision_seguros, double fecha_pago_soles_igv, double fecha_pago_soles_total_cuota, double fecha_pago_soles_saldo,
            double diferencia_cambio_capital, double diferencia_cambio_interes, double diferencia_cambio_igv, double diferencia_cambio_total,
            double fecha_pago, double leasing, double entidad, DateTime fecha_registro, DateTime fecha_modificacion, int usuario, string ruc)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_update_leasing";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@mes", mes);
            sqlCommand.Parameters.AddWithValue("@numero_cuota", numero_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_vencimiento", fecha_vencimiento);
            sqlCommand.Parameters.AddWithValue("@fecha_formalizacion", fecha_formalizacion);
            sqlCommand.Parameters.AddWithValue("@dolares_capital", dolares_capital);
            sqlCommand.Parameters.AddWithValue("@dolares_interes", dolares_interes);
            sqlCommand.Parameters.AddWithValue("@dolares_comision_seguros", dolares_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@dolares_valor_cuota", dolares_valor_cuota);
            sqlCommand.Parameters.AddWithValue("@dolares_igv", dolares_igv);
            sqlCommand.Parameters.AddWithValue("@dolares_total_cuota", dolares_total_cuota);
            sqlCommand.Parameters.AddWithValue("@dolares_saldo", dolares_saldo);
            sqlCommand.Parameters.AddWithValue("@fecha_emision_tipo_cambio", fecha_emision_tipo_cambio);
            sqlCommand.Parameters.AddWithValue("@soles_saldo", soles_saldo);
            sqlCommand.Parameters.AddWithValue("@soles_capital", soles_capital);
            sqlCommand.Parameters.AddWithValue("@soles_interes", soles_interes);
            sqlCommand.Parameters.AddWithValue("@soles_comision_seguros", soles_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@soles_valor_cuota", soles_valor_cuota);
            sqlCommand.Parameters.AddWithValue("@soles_igv", soles_igv);
            sqlCommand.Parameters.AddWithValue("@soles_total_cuota", soles_total_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_capital", fecha_pago_soles_capital);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_interes", fecha_pago_soles_interes);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_comision_seguros", fecha_pago_soles_comision_seguros);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_igv", fecha_pago_soles_igv);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_total_cuota", fecha_pago_soles_total_cuota);
            sqlCommand.Parameters.AddWithValue("@fecha_pago_soles_saldo", fecha_pago_soles_saldo);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_capital", diferencia_cambio_capital);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_interes", diferencia_cambio_interes);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_igv", diferencia_cambio_igv);
            sqlCommand.Parameters.AddWithValue("@diferencia_cambio_total", diferencia_cambio_total);
            sqlCommand.Parameters.AddWithValue("@fecha_pago", fecha_pago);
            sqlCommand.Parameters.AddWithValue("@leasing", leasing);
            sqlCommand.Parameters.AddWithValue("@entidad", entidad);
            sqlCommand.Parameters.AddWithValue("@fecha_registro", fecha_registro);
            sqlCommand.Parameters.AddWithValue("@fecha_modificacion", fecha_modificacion);
            sqlCommand.Parameters.AddWithValue("@usuario", usuario);
            sqlCommand.Parameters.AddWithValue("@ruc", ruc);

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
            sqlCommand.CommandText = "sp_delete_leasing";
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
