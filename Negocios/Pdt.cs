using System.Data;
using Datos;

namespace Negocios
{
    public class Pdt
    {
        DaoPdt daoPdt = new DaoPdt();

        public DataTable PDT(string ruc, int anio) { return daoPdt.PDT(ruc, anio); }
    }
}
