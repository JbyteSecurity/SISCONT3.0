using Datos;
using System.Data;

namespace Negocios
{
    public class Proveedor
    {
        private DaoProveedor daoProveedor = new DaoProveedor();
        public DataTable Show(string ruc)
        {
            return daoProveedor.Show(ruc);
        }

        public DataTable All()
        {
            return daoProveedor.All();
        }

        public bool Insert(string ruc, string razonSocial, string nombre, string correo, string direccion, string telefono, string web)
        {

            return daoProveedor.Insert(ruc, razonSocial, nombre, correo, direccion, telefono, web);
        }

        public bool Update(int id, string ruc, string razonSocial, string nombre, string correo, string direccion, string telefono, string web)
        {
            return daoProveedor.Update(id, ruc, razonSocial, nombre, correo, direccion, telefono, web);
        }

        public bool Destroy(int id)
        {
            return daoProveedor.Destroy(id);
        }
    }
}
