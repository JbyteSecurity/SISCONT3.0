﻿using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DaoVentas
    {
        private Conexion conexion = new Conexion();

        SqlCommand sqlCommand = new SqlCommand();

        public DataTable AllByMonth()
        {
            SqlDataReader sqlDataReader;
            DataTable dataTableDetraccion = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_all_month_ventas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTableDetraccion.Load(sqlDataReader);
            conexion.CloseConnection();
            return dataTableDetraccion;
        }

        public DataTable AllByMonthFilter(int anio, int mes)
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();

            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_all_month_ventas_filtro";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Anio", anio);
            sqlCommand.Parameters.AddWithValue("@Mes", mes);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();
            return dataTable;
        }

        public DataTable GetForTXT(int anio, int mes, string ruc)
        {
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_generate_txt_ventas";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Anio", anio);
            sqlCommand.Parameters.AddWithValue("@Mes", mes);
            sqlCommand.Parameters.AddWithValue("@ruc", ruc);

            sqlCommand.ExecuteNonQuery();
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlCommand.Parameters.Clear();

            conexion.CloseConnection();
            return dataTable;
        }

        public bool Insert(
            string numeroRegistro, string fechaEmision, string fechaPago, string cdpTipo, string cdpSerie,
            string cdpNumeroDocumento, string proveedorTipo, string proveedorNumero, string proveedorNombreRazonSocial,
            string cuenta, string descripcion, double valorExportacion, double baseImponible, double importeTotalExonerada,
            double importeTotalInafecta, double igv, double importeTotal, double tipoCambio, double dolares, double igvRetencion,
            string cuentaDestino, string cuentaDestinoDescripcion, string referenciaFecha, string referenciaTipo, string referenciaSerie, string referenciaNumeroDocumento,
            string codigo, string constanciaNumero, string constanciaFechaPago, double detraccionSoles, string referencia, string observacion, string usuario, string rucEmpresa
            )
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_insert_ventas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Clear();

            sqlCommand.Parameters.AddWithValue("@NReg", numeroRegistro);
            sqlCommand.Parameters.AddWithValue("@FechaEmision", fechaEmision);
            sqlCommand.Parameters.AddWithValue("@FechaPago", fechaPago);
            sqlCommand.Parameters.AddWithValue("@CTipo", cdpTipo);
            sqlCommand.Parameters.AddWithValue("@CSerie", cdpSerie);
            sqlCommand.Parameters.AddWithValue("@CNDocumento", cdpNumeroDocumento);
            sqlCommand.Parameters.AddWithValue("@PTipo", proveedorTipo);
            sqlCommand.Parameters.AddWithValue("@PNumero", proveedorNumero);
            sqlCommand.Parameters.AddWithValue("@PNombreRazonSocial", proveedorNombreRazonSocial);
            sqlCommand.Parameters.AddWithValue("@Cuenta", cuenta);
            sqlCommand.Parameters.AddWithValue("@Descripcion", descripcion);
            sqlCommand.Parameters.AddWithValue("@ValorExportacion", valorExportacion);
            sqlCommand.Parameters.AddWithValue("@BaseImponible", baseImponible);
            sqlCommand.Parameters.AddWithValue("@ImporteTotalExonerada", importeTotalExonerada);
            sqlCommand.Parameters.AddWithValue("@ImporteTotalInafecta", importeTotalInafecta);
            sqlCommand.Parameters.AddWithValue("@IGV", igv);
            sqlCommand.Parameters.AddWithValue("@ImporteTotal", importeTotal);
            sqlCommand.Parameters.AddWithValue("@TC", tipoCambio);
            sqlCommand.Parameters.AddWithValue("@Dolares", dolares);
            sqlCommand.Parameters.AddWithValue("@IgvRetencion", igvRetencion);
            sqlCommand.Parameters.AddWithValue("@CuentaDestino", cuentaDestino);
            sqlCommand.Parameters.AddWithValue("@CuentaDestinoDescripcion", cuentaDestinoDescripcion);
            sqlCommand.Parameters.AddWithValue("@ReferenciaFecha", referenciaFecha);
            sqlCommand.Parameters.AddWithValue("@ReferenciaTipo", referenciaTipo);
            sqlCommand.Parameters.AddWithValue("@ReferenciaSerie", referenciaSerie);
            sqlCommand.Parameters.AddWithValue("@ReferenciaNumeroDocumento", referenciaNumeroDocumento);
            sqlCommand.Parameters.AddWithValue("@Codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@ConstanciaNumero", constanciaNumero);
            sqlCommand.Parameters.AddWithValue("@ConstanciaFechaPago", constanciaFechaPago);
            sqlCommand.Parameters.AddWithValue("@DetraccionSoles", detraccionSoles);
            sqlCommand.Parameters.AddWithValue("@Referencia", referencia);
            sqlCommand.Parameters.AddWithValue("@Observacion", observacion);
            sqlCommand.Parameters.AddWithValue("@Usuario", usuario);
            sqlCommand.Parameters.AddWithValue("@rucEmpresa", rucEmpresa);

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            return false;
        }

        public bool Update(
            int id, string numeroRegistro, string fechaEmision, string fechaPago, string cdpTipo, string cdpSerie,
            string cdpNumeroDocumento, string proveedorTipo, string proveedorNumero, string proveedorNombreRazonSocial,
            string cuenta, string descripcion, double valorExportacion, double baseImponible, double importeTotalExonerada,
            double importeTotalInafecta, double igv, double importeTotal, double tipoCambio, double dolares, double igvRetencion,
            string cuentaDestino, string cuentaDestinoDescripcion, string referenciaFecha, string referenciaTipo, string referenciaSerie, string referenciaNumeroDocumento,
            string codigo, string constanciaNumero, string constanciaFechaPago, double detraccionSoles, string referencia, string observacion, string usuario
            )
        {
            sqlCommand.Connection = conexion.OpenConnection();
            sqlCommand.CommandText = "sp_update_ventas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Clear();

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@NReg", numeroRegistro);
            sqlCommand.Parameters.AddWithValue("@FechaEmision", fechaEmision);
            sqlCommand.Parameters.AddWithValue("@FechaPago", fechaPago);
            sqlCommand.Parameters.AddWithValue("@CTipo", cdpTipo);
            sqlCommand.Parameters.AddWithValue("@CSerie", cdpSerie);
            sqlCommand.Parameters.AddWithValue("@CNDocumento", cdpNumeroDocumento);
            sqlCommand.Parameters.AddWithValue("@PTipo", proveedorTipo);
            sqlCommand.Parameters.AddWithValue("@PNumero", proveedorNumero);
            sqlCommand.Parameters.AddWithValue("@PNombreRazonSocial", proveedorNombreRazonSocial);
            sqlCommand.Parameters.AddWithValue("@Cuenta", cuenta);
            sqlCommand.Parameters.AddWithValue("@Descripcion", descripcion);
            sqlCommand.Parameters.AddWithValue("@ValorExportacion", valorExportacion);
            sqlCommand.Parameters.AddWithValue("@BaseImponible", baseImponible);
            sqlCommand.Parameters.AddWithValue("@ImporteTotalExonerada", importeTotalExonerada);
            sqlCommand.Parameters.AddWithValue("@ImporteTotalInafecta", importeTotalInafecta);
            sqlCommand.Parameters.AddWithValue("@IGV", igv);
            sqlCommand.Parameters.AddWithValue("@ImporteTotal", importeTotal);
            sqlCommand.Parameters.AddWithValue("@TC", tipoCambio);
            sqlCommand.Parameters.AddWithValue("@Dolares", dolares);
            sqlCommand.Parameters.AddWithValue("@IgvRetencion", igvRetencion);
            sqlCommand.Parameters.AddWithValue("@CuentaDestino", cuentaDestino);
            sqlCommand.Parameters.AddWithValue("@CuentaDestinoDescripcion", cuentaDestinoDescripcion);
            sqlCommand.Parameters.AddWithValue("@ReferenciaFecha", referenciaFecha);
            sqlCommand.Parameters.AddWithValue("@ReferenciaTipo", referenciaTipo);
            sqlCommand.Parameters.AddWithValue("@ReferenciaSerie", referenciaSerie);
            sqlCommand.Parameters.AddWithValue("@ReferenciaNumeroDocumento", referenciaNumeroDocumento);
            sqlCommand.Parameters.AddWithValue("@Codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@ConstanciaNumero", constanciaNumero);
            sqlCommand.Parameters.AddWithValue("@ConstanciaFechaPago", constanciaFechaPago);
            sqlCommand.Parameters.AddWithValue("@DetraccionSoles", detraccionSoles);
            sqlCommand.Parameters.AddWithValue("@Referencia", referencia);
            sqlCommand.Parameters.AddWithValue("@Observacion", observacion);
            sqlCommand.Parameters.AddWithValue("@Usuario", usuario);

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
            sqlCommand.CommandText = "sp_delete_ventas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", id);
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlCommand.Parameters.Clear();
                conexion.CloseConnection();
                return true;
            }
            else return false;
        }
    }
}
