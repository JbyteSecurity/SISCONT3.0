using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmPlanContable : Form
    {
        bool edit = false;

        PlanContable planContable = new PlanContable();
        DataTable dataTable = new DataTable();
        public FrmPlanContable()
        {
            InitializeComponent();
        }

        private void FrmPlanContable_Load(object sender, EventArgs e)
        {
            All();
        }

        private void Clear()
        {
            txtNodoPadre.Text = null;
            txtNodoHijo.Text = null;
            txtNodoTexto.Text = null;
            edit = false;
        }

        private void All()
        {
            
            dataTable = planContable.All();
            AddNodes(1, null);
        }

        

        private void AddNodes(int id, TreeNode parentNode)
        {
            TreeNode childremNone;

            foreach (DataRow dr in dataTable.Select("[CodigoPadre]=" + id))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["Cuenta"].ToString();
                t.Name = dr["Codigo"].ToString();
                t.Tag = dataTable.Rows.IndexOf(dr);

                if (parentNode == null)
                {
                    tvPlanContable.Nodes.Add(t);
                    childremNone = t;
                } else
                {
                    parentNode.Nodes.Add(t);
                    childremNone = t;
                }

                AddNodes(Convert.ToInt32(dr["Codigo"].ToString()), childremNone);
            }

        }

        private void Save() { }
        private void Edit() { }
        private void Destroy() { }

        private void tvPlanContable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtNodoPadre.Text = e.Node.Text;
            txtNodoHijo.Text = e.Node.Name;
        }
    }
}
