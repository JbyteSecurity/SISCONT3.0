using Datos;
using System.Data;

namespace Negocios
{
    public class PlanContable
    {
        private DaoPlanContable daoPlanContable = new DaoPlanContable();

        public string GetAcount(string codigo) { return daoPlanContable.ShowAcount(codigo); }

        public DataTable ShowAcountFilter(bool naturaleza, bool pago, bool destino) { return daoPlanContable.ShowAcountFilter(naturaleza, pago, destino); }

        public DataTable All() { return daoPlanContable.All(); }

        public bool Insert (int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, int codigo_padre)
        { return daoPlanContable.Insert(codigo, cuenta, uso, naturaleza, pago, destino, codigo_padre); }

        public bool Update(int id, int codigo, string cuenta, bool uso, bool naturaleza, bool pago, bool destino, int codigo_padre)
        { return daoPlanContable.Update(id, codigo, cuenta, uso, naturaleza, pago, destino, codigo_padre); }

        public bool Destroy(int id) { return daoPlanContable.Destroy(id); }
    }
}
