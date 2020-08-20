using Datos;
using System.Data;

namespace Negocios
{
    public class PlanContable
    {
        private DaoPlanContable daoPlanContable = new DaoPlanContable();

        public string GetAcount(string codigo) { return daoPlanContable.ShowAcount(codigo); }

        public DataTable ShowAcountFilter(string clasificacion) { return daoPlanContable.ShowAcountFilter(clasificacion); }

        public DataTable All() { return daoPlanContable.All(); }
    }
}
