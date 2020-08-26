using System.Data;
using Datos;

namespace Negocios
{
    public class Pdt
    {
        DaoPdt daoPdt = new DaoPdt();

        public DataTable PDT(string ruc, int anio, int usuario) { return daoPdt.PDT(ruc, anio, usuario); }

		public DataTable Show(string ruc, int anio, int usuario) { return daoPdt.Show(ruc, anio, usuario); }

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
			return daoPdt.Insert(anio, mes, ruc, ingresoExportacion, ingresoGravadas, ingresoExonerada, ingresoInafecta, ingresoIGV, ingresoImporteTotal, egresoBaseImponible,
				egresoIGV, egresoNoGravada, egresoImporteTotal, ficalIgvImpouestoResultante, ficalIgvCreditoDebito, ficalIgvSaldoFavorPagar, exportadorSFMB, percepcionesIgvDelMes,
				percepcionesIgvMesAnterior, percepcionesIgvAplicada, percepcionesIgvComposicionProcedente, percepcionesIgvPorAplicar, retencionesIgvDelMes, retencionesIgvMesAnterior,
				retencionesIgvAplicada, retencionesIgvComposicionProcedente, retencionesIgvPorAplicar, igvPagoAPagar, igvPagoPagado, impuestoAlaRentaOtrosIngreso,
				impuestoAlaRentaBaseImponible, impuestoAlaRentaCoeficiente, impuestoAlaRentaImpuestoResultante, impuestoAlaRentaPagado, impuestoAlaRentaCompensacionSFA,
				impuestoAlaRentaCompensacionSFMB, impuestoAlaRentaCompensacionITAN, impuestoAlaRentaCompensacionPercepcion, impuestoAlaRentaImputacion, impuestoAlaRentaPorPagar, usuario);
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
			return daoPdt.Update(id, ingresoExportacion, ingresoGravadas, ingresoExonerada, ingresoInafecta, ingresoIGV, ingresoImporteTotal, egresoBaseImponible,
				egresoIGV, egresoNoGravada, egresoImporteTotal, ficalIgvImpouestoResultante, ficalIgvCreditoDebito, ficalIgvSaldoFavorPagar, exportadorSFMB, percepcionesIgvDelMes,
				percepcionesIgvMesAnterior, percepcionesIgvAplicada, percepcionesIgvComposicionProcedente, percepcionesIgvPorAplicar, retencionesIgvDelMes, retencionesIgvMesAnterior,
				retencionesIgvAplicada, retencionesIgvComposicionProcedente, retencionesIgvPorAplicar, igvPagoAPagar, igvPagoPagado, impuestoAlaRentaOtrosIngreso,
				impuestoAlaRentaBaseImponible, impuestoAlaRentaCoeficiente, impuestoAlaRentaImpuestoResultante, impuestoAlaRentaPagado, impuestoAlaRentaCompensacionSFA,
				impuestoAlaRentaCompensacionSFMB, impuestoAlaRentaCompensacionITAN, impuestoAlaRentaCompensacionPercepcion, impuestoAlaRentaImputacion, impuestoAlaRentaPorPagar);
        }

	}
}
