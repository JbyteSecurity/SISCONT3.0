using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Leasing
    {
        DaoLeasing daoLeasing = new DaoLeasing();

        public DataTable AllFilter(int mes, int usuario, string ruc) { return daoLeasing.AllFilter(mes, usuario, ruc); }

        public bool Insert(
            int mes, int numero_cuota, string fecha_vencimiento, string fecha_formalizacion, double dolares_capital, double dolares_interes,
            double dolares_comision_seguros, double dolares_valor_cuota, double dolares_igv, double dolares_total_cuota, double dolares_saldo,
            double fecha_emision_tipo_cambio, double soles_saldo, double soles_capital, double soles_interes, double soles_comision_seguros,
            double soles_valor_cuota, double soles_igv, double soles_total_cuota, double fecha_pago_soles_capital, double fecha_pago_soles_interes,
            double fecha_pago_soles_comision_seguros, double fecha_pago_soles_igv, double fecha_pago_soles_total_cuota, double fecha_pago_soles_saldo,
            double diferencia_cambio_capital, double diferencia_cambio_interes, double diferencia_cambio_igv, double diferencia_cambio_total,
            double fecha_pago, double leasing, double entidad, DateTime fecha_registro, DateTime fecha_modificacion, int usuario, string ruc)
        {
            return daoLeasing.Insert(mes, numero_cuota, fecha_vencimiento, fecha_formalizacion, dolares_capital, dolares_interes,
            dolares_comision_seguros, dolares_valor_cuota, dolares_igv, dolares_total_cuota, dolares_saldo,
            fecha_emision_tipo_cambio, soles_saldo, soles_capital, soles_interes, soles_comision_seguros,
            soles_valor_cuota, soles_igv, soles_total_cuota, fecha_pago_soles_capital, fecha_pago_soles_interes,
            fecha_pago_soles_comision_seguros, fecha_pago_soles_igv, fecha_pago_soles_total_cuota, fecha_pago_soles_saldo,
            diferencia_cambio_capital, diferencia_cambio_interes, diferencia_cambio_igv, diferencia_cambio_total,
            fecha_pago, leasing, entidad, fecha_registro, fecha_modificacion, usuario, ruc);
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
            return daoLeasing.Update(id, mes, numero_cuota, fecha_vencimiento, fecha_formalizacion, dolares_capital, dolares_interes,
            dolares_comision_seguros, dolares_valor_cuota, dolares_igv, dolares_total_cuota, dolares_saldo,
            fecha_emision_tipo_cambio, soles_saldo, soles_capital, soles_interes, soles_comision_seguros,
            soles_valor_cuota, soles_igv, soles_total_cuota, fecha_pago_soles_capital, fecha_pago_soles_interes,
            fecha_pago_soles_comision_seguros, fecha_pago_soles_igv, fecha_pago_soles_total_cuota, fecha_pago_soles_saldo,
            diferencia_cambio_capital, diferencia_cambio_interes, diferencia_cambio_igv, diferencia_cambio_total,
            fecha_pago, leasing, entidad, fecha_registro, fecha_modificacion, usuario, ruc);
        }

        public bool Destroy(int id) { return daoLeasing.Destroy(id); }
    }
}
