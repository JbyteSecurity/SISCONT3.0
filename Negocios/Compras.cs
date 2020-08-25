using Datos;
using System.Data;

namespace Negocios
{
    public class Compras
    {

        private DaoCompras daoCompras = new DaoCompras();

        //public DataTable AllCurrentMonth() { return daoCompras.AllCurrentMonth(); }
        public DataSet AllCurrentMonth() { return daoCompras.AllCurrentMonth(); }

        //public DataTable AllByMonthFilter(int anio, int mes) { return daoCompras.AllByMonthFilter(anio, mes); }
        public DataSet AllByMonthFilter(int anio, int mes) { return daoCompras.AllByMonthFilter(anio, mes); }

        public DataTable GetForTXT(int anio, int mes, string ruc, int usuario) { return daoCompras.GetForTXT(anio, mes, ruc, usuario); }

        public bool Insert(
            string nReg, string fechaEmision, string fechaPago, string cTipo, string cSeire, string cnDocumento,
            string pTipo, string pNumero, string pRazonSocial, string cuenta, string descripcion, double baseImponible,
            double igv, double noGravada, double descuento, double importeTotal, double dolares, double tipoCambio, double percepcion, string destino,
            string descripcionDestino, string cuentaDestino, string codigo, string constanciaNumero, string constanciaFechapago,
            double constanciaMonto, string constanciaReferencia, string bancarizacionFecha, string bancarizacionBco, int bancarizacionOperacion, string referenciaFecha,
            string referenciaTipo, string referenciaSerie, string referenciaNumero, int usuario, double comprasConversionDolares, string observacion, string rucEmpresa
            )
        {
            return daoCompras.Insert(
                nReg, fechaEmision, fechaPago, cTipo, cSeire, cnDocumento,
                pTipo, pNumero, pRazonSocial, cuenta, descripcion, baseImponible,
                igv, noGravada, descuento, importeTotal, dolares, tipoCambio, percepcion, destino,
                descripcionDestino, cuentaDestino, codigo, constanciaNumero, constanciaFechapago,
                constanciaMonto, constanciaReferencia, bancarizacionFecha, bancarizacionBco, bancarizacionOperacion, referenciaFecha,
                referenciaTipo, referenciaSerie, referenciaNumero, usuario, comprasConversionDolares, observacion, rucEmpresa
                );
        }

        public bool Update(
            int id, string nReg, string fechaEmision, string fechaPago, string cTipo, string cSeire, string cnDocumento,
            string pTipo, string pNumero, string pRazonSocial, string cuenta, string descripcion, double baseImponible,
            double igv, double noGravada, double descuento, double importeTotal, double dolares, double tipoCambio, double percepcion, string destino,
            string descripcionDestino, string cuentaDestino, string codigo, string constanciaNumero, string constanciaFechapago,
            double constanciaMonto, string constanciaReferencia, string bancarizacionFecha, string bancarizacionBco, int bancarizacionOperacion, string referenciaFecha,
            string referenciaTipo, string referenciaSerie, string referenciaNumero, double comprasConversionDolares, string observacion
            )
        {
            return daoCompras.Update(
                id, nReg, fechaEmision, fechaPago, cTipo, cSeire, cnDocumento,
                pTipo, pNumero, pRazonSocial, cuenta, descripcion, baseImponible,
                igv, noGravada, descuento, importeTotal, dolares, tipoCambio, percepcion, destino,
                descripcionDestino, cuentaDestino, codigo, constanciaNumero, constanciaFechapago,
                constanciaMonto, constanciaReferencia, bancarizacionFecha, bancarizacionBco, bancarizacionOperacion, referenciaFecha,
                referenciaTipo, referenciaSerie, referenciaNumero, comprasConversionDolares, observacion
                );
        }

        public bool Destroy(int id)
        {
            return daoCompras.Destroy(id);
        }
    }
}
