﻿using Datos;
using System.Data;

namespace Negocios
{
    public class Proveedor
    {
        private DaoProveedor daoProveedor = new DaoProveedor();
        public string GetSupplierName(string ruc)
        {
            string razonSocial;
            razonSocial = daoProveedor.Show(ruc);
            return razonSocial;
        }

        public DataTable All()
        {
            return daoProveedor.All();
        }

        public bool Insert(string ruc, string razonSocial)
        {

            return daoProveedor.Insert(ruc, razonSocial);
        }

        public bool Update(int id, string ruc, string razonSocial)
        {
            return daoProveedor.Update(id, ruc, razonSocial);
        }

        public bool Destroy(int id)
        {
            return daoProveedor.Destroy(id);
        }
    }
}
