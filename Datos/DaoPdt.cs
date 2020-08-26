using System.Data;
using System.Data.SqlClient;


namespace Datos
{
    public class DaoPdt
    {
        Conexion conexion = new Conexion();

        SqlCommand sqlCommand = new SqlCommand();

        public DataTable PDT(string ruc, int anio, int usuario)
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();

            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_pdt";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ruc", ruc);
            sqlCommand.Parameters.AddWithValue("@anio", anio);
            sqlCommand.Parameters.AddWithValue("@usuario", usuario);

			sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();
            return dataTable;
        }

		public DataTable Show(string ruc, int anio, int usuario)
		{
			SqlDataReader sqlDataReader;
			DataTable dataTable = new DataTable();

			sqlCommand.Connection = conexion.OpenConnection();
			sqlCommand.CommandText = "sp_show_ruc_pdt";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			sqlCommand.Parameters.AddWithValue("@ruc", ruc);
			sqlCommand.Parameters.AddWithValue("@anio", anio);
			sqlCommand.Parameters.AddWithValue("@usuario", usuario);

			sqlCommand.ExecuteNonQuery();
			sqlDataReader = sqlCommand.ExecuteReader();
			dataTable.Load(sqlDataReader);
			sqlCommand.Parameters.Clear();

			conexion.CloseConnection();
			return dataTable;
		}

		public bool Insert(int anio, int mes, string ruc, double ingresoExportacion, double ingresoGravadas, double ingresoExonerada, double ingresoInafecta,
			double ingresoIGV, double ingresoImporteTotal, double egresoBaseImponible, double egresoIGV, double egresoNoGravada, double egresoImporteTotal,
			double ficalIgvImpouestoResultante, double ficalIgvCreditoDebito, double ficalIgvSaldoFavorPagar, double exportadorSFMB, double percepcionesIgvDelMes,
			double percepcionesIgvMesAnterior, double percepcionesIgvAplicada, double percepcionesIgvComposicionProcedente, double percepcionesIgvPorAplicar,
			double retencionesIgvDelMes, double retencionesIgvMesAnterior, double retencionesIgvAplicada, double retencionesIgvComposicionProcedente,
			double retencionesIgvPorAplicar, double igvPagoAPagar, double igvPagoPagado, double impuestoAlaRentaOtrosIngreso, double impuestoAlaRentaBaseImponible,
			double impuestoAlaRentaCoeficiente, double impuestoAlaRentaImpuestoResultante, double impuestoAlaRentaPagado, double impuestoAlaRentaCompensacionSFA,
			double impuestoAlaRentaCompensacionSFMB, double impuestoAlaRentaCompensacionITAN, double impuestoAlaRentaCompensacionPercepcion, double impuestoAlaRentaImputacion,
			double impuestoAlaRentaPorPagar, int usuario)
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_insert_pdt";
            sqlCommand.CommandType = CommandType.StoredProcedure;

			sqlCommand.Parameters.AddWithValue("@anio", anio);
			sqlCommand.Parameters.AddWithValue("@mes", mes);
			sqlCommand.Parameters.AddWithValue("@ruc", ruc);
			sqlCommand.Parameters.AddWithValue("@ingresoExportacion", ingresoExportacion);
			sqlCommand.Parameters.AddWithValue("@ingresoGravadas", ingresoGravadas);
			sqlCommand.Parameters.AddWithValue("@ingresoExonerada", ingresoExonerada);
			sqlCommand.Parameters.AddWithValue("@ingresoInafecta", ingresoInafecta);
			sqlCommand.Parameters.AddWithValue("@ingresoIGV", ingresoIGV);
			sqlCommand.Parameters.AddWithValue("@ingresoImporteTotal", ingresoImporteTotal);
			sqlCommand.Parameters.AddWithValue("@egresoBaseImponible", egresoBaseImponible);
			sqlCommand.Parameters.AddWithValue("@egresoIGV", egresoIGV);
			sqlCommand.Parameters.AddWithValue("@egresoNoGravada", egresoNoGravada);
			sqlCommand.Parameters.AddWithValue("@egresoImporteTotal", egresoImporteTotal);
			sqlCommand.Parameters.AddWithValue("@ficalIgvImpouestoResultante", ficalIgvImpouestoResultante);
			sqlCommand.Parameters.AddWithValue("@ficalIgvCreditoDebito", ficalIgvCreditoDebito);
			sqlCommand.Parameters.AddWithValue("@ficalIgvSaldoFavorPagar", ficalIgvSaldoFavorPagar);
			sqlCommand.Parameters.AddWithValue("@exportadorSFMB", exportadorSFMB);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvDelMes", percepcionesIgvDelMes);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvMesAnterior", percepcionesIgvMesAnterior);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvAplicada", percepcionesIgvAplicada);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvComposicionProcedente", percepcionesIgvComposicionProcedente);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvPorAplicar", percepcionesIgvPorAplicar);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvDelMes", retencionesIgvDelMes);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvMesAnterior", retencionesIgvMesAnterior);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvAplicada", retencionesIgvAplicada);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvComposicionProcedente", retencionesIgvComposicionProcedente);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvPorAplicar", retencionesIgvPorAplicar);
			sqlCommand.Parameters.AddWithValue("@igvPagoAPagar", igvPagoAPagar);
			sqlCommand.Parameters.AddWithValue("@igvPagoPagado", igvPagoPagado);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaOtrosIngreso", impuestoAlaRentaOtrosIngreso);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaBaseImponible", impuestoAlaRentaBaseImponible);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCoeficiente", impuestoAlaRentaCoeficiente);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaImpuestoResultante", impuestoAlaRentaImpuestoResultante);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaPagado", impuestoAlaRentaPagado);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionSFA", impuestoAlaRentaCompensacionSFA);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionSFMB", impuestoAlaRentaCompensacionSFMB);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionITAN", impuestoAlaRentaCompensacionITAN);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionPercepcion", impuestoAlaRentaCompensacionPercepcion);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaImputacion", impuestoAlaRentaImputacion);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaPorPagar", impuestoAlaRentaPorPagar);
			sqlCommand.Parameters.AddWithValue("@usuario", usuario);

			if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else
                return false;
        }

		public bool Update(int id, double ingresoExportacion, double ingresoGravadas, double ingresoExonerada, double ingresoInafecta,
			double ingresoIGV, double ingresoImporteTotal, double egresoBaseImponible, double egresoIGV, double egresoNoGravada, double egresoImporteTotal,
			double ficalIgvImpouestoResultante, double ficalIgvCreditoDebito, double ficalIgvSaldoFavorPagar, double exportadorSFMB, double percepcionesIgvDelMes,
			double percepcionesIgvMesAnterior, double percepcionesIgvAplicada, double percepcionesIgvComposicionProcedente, double percepcionesIgvPorAplicar,
			double retencionesIgvDelMes, double retencionesIgvMesAnterior, double retencionesIgvAplicada, double retencionesIgvComposicionProcedente,
			double retencionesIgvPorAplicar, double igvPagoAPagar, double igvPagoPagado, double impuestoAlaRentaOtrosIngreso, double impuestoAlaRentaBaseImponible,
			double impuestoAlaRentaCoeficiente, double impuestoAlaRentaImpuestoResultante, double impuestoAlaRentaPagado, double impuestoAlaRentaCompensacionSFA,
			double impuestoAlaRentaCompensacionSFMB, double impuestoAlaRentaCompensacionITAN, double impuestoAlaRentaCompensacionPercepcion, double impuestoAlaRentaImputacion,
			double impuestoAlaRentaPorPagar)
		{
			sqlCommand.Connection = conexion.OpenConnection();
			sqlCommand.CommandText = "sp_update_pdt";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			sqlCommand.Parameters.AddWithValue("@id", id);
			sqlCommand.Parameters.AddWithValue("@ingresoExportacion", ingresoExportacion);
			sqlCommand.Parameters.AddWithValue("@ingresoGravadas", ingresoGravadas);
			sqlCommand.Parameters.AddWithValue("@ingresoExonerada", ingresoExonerada);
			sqlCommand.Parameters.AddWithValue("@ingresoInafecta", ingresoInafecta);
			sqlCommand.Parameters.AddWithValue("@ingresoIGV", ingresoIGV);
			sqlCommand.Parameters.AddWithValue("@ingresoImporteTotal", ingresoImporteTotal);
			sqlCommand.Parameters.AddWithValue("@egresoBaseImponible", egresoBaseImponible);
			sqlCommand.Parameters.AddWithValue("@egresoIGV", egresoIGV);
			sqlCommand.Parameters.AddWithValue("@egresoNoGravada", egresoNoGravada);

			sqlCommand.Parameters.AddWithValue("@egresoImporteTotal", egresoImporteTotal);
			sqlCommand.Parameters.AddWithValue("@ficalIgvImpouestoResultante", ficalIgvImpouestoResultante);
			sqlCommand.Parameters.AddWithValue("@ficalIgvCreditoDebito", ficalIgvCreditoDebito);
			sqlCommand.Parameters.AddWithValue("@ficalIgvSaldoFavorPagar", ficalIgvSaldoFavorPagar);
			sqlCommand.Parameters.AddWithValue("@exportadorSFMB", exportadorSFMB);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvDelMes", percepcionesIgvDelMes);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvMesAnterior", percepcionesIgvMesAnterior);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvAplicada", percepcionesIgvAplicada);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvComposicionProcedente", percepcionesIgvComposicionProcedente);
			sqlCommand.Parameters.AddWithValue("@percepcionesIgvPorAplicar", percepcionesIgvPorAplicar);

			sqlCommand.Parameters.AddWithValue("@retencionesIgvDelMes", retencionesIgvDelMes);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvMesAnterior", retencionesIgvMesAnterior);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvAplicada", retencionesIgvAplicada);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvComposicionProcedente", retencionesIgvComposicionProcedente);
			sqlCommand.Parameters.AddWithValue("@retencionesIgvPorAplicar", retencionesIgvPorAplicar);
			sqlCommand.Parameters.AddWithValue("@igvPagoAPagar", igvPagoAPagar);
			sqlCommand.Parameters.AddWithValue("@igvPagoPagado", igvPagoPagado);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaOtrosIngreso", impuestoAlaRentaOtrosIngreso);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaBaseImponible", impuestoAlaRentaBaseImponible);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCoeficiente", impuestoAlaRentaCoeficiente);

			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaImpuestoResultante", impuestoAlaRentaImpuestoResultante);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaPagado", impuestoAlaRentaPagado);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionSFA", impuestoAlaRentaCompensacionSFA);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionSFMB", impuestoAlaRentaCompensacionSFMB);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionITAN", impuestoAlaRentaCompensacionITAN);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaCompensacionPercepcion", impuestoAlaRentaCompensacionPercepcion);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaImputacion", impuestoAlaRentaImputacion);
			sqlCommand.Parameters.AddWithValue("@impuestoAlaRentaPorPagar", impuestoAlaRentaPorPagar);

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
