using Datos;
using System.Data;

namespace Negocios
{
    public class ComprobantePago
    {
        private DaoComprobantePago daoComprobantePago = new DaoComprobantePago();
        public DataTable GetAllCpdTypes()
        {
            return daoComprobantePago.AllCdpTypes();
        }
    }
}
