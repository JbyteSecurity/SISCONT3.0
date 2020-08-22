using Datos;
using System.Data;

namespace Negocios
{
    public class PlanContable
    {
        private DaoPlanContable daoPlanContable = new DaoPlanContable();

        public string GetAcount(string codigo) { return daoPlanContable.ShowAcount(codigo); }

        public DataTable ShowById(int id) { return daoPlanContable.ShowById(id); }

        public DataTable ShowAcountFilter(bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar) { return daoPlanContable.ShowAcountFilter(naturaleza, pago, destino, vnaturaleza, vcobrar); }

        public DataTable All() { return daoPlanContable.All(); }

        public DataTable ShowNaturaleza() { return daoPlanContable.ShowNaturaleza(); }
        public DataTable ShowPago() { return daoPlanContable.ShowPago(); }
        public DataTable ShowDestino() { return daoPlanContable.ShowDestino(); }
        public DataTable ShowNaturalezaV() { return daoPlanContable.ShowNaturalezaV(); }
        public DataTable ShowCobrar() { return daoPlanContable.ShowCobrar(); }

        public bool Insert(int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar, int codigo_padre)
        { return daoPlanContable.Insert(codigo, cuenta, uso, naturaleza, pago, destino, vnaturaleza, vcobrar, codigo_padre); }

        public bool Update(int id, int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, bool vnaturaleza, bool vcobrar, int codigo_padre)
        { return daoPlanContable.Update(id, codigo, cuenta, uso, naturaleza, pago, destino, vnaturaleza, vcobrar, codigo_padre); }

        public bool Destroy(int id) { return daoPlanContable.Destroy(id); }
    }
}
