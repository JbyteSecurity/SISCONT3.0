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
        bool newParent = false;

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
            txtCuentaPadre.Text = null;
            txtCodigoPadre.Text = null;
            txtCodigoHijo.Text = null;
            txtCuentaHijo.Text = null;
            chbUso.Checked = true;
            chbCuentaNaturaleza.Checked = false;
            chbCuentaPorPagar.Checked = false;
            chbCuentaDestino.Checked = false;
            edit = false;
            newParent = false;
        }

        private void All()
        {
            dataTable = planContable.All();
            tvPlanContable.Nodes.Clear();
            AddNodes(1, null);
        }

        

        private void AddNodes(int id, TreeNode parentNode)
        {
            try
            {
                TreeNode childremNone;

                foreach (DataRow dr in dataTable.Select("[CodigoPadre]=" + id))
                {
                    TreeNode t = new TreeNode();
                    t.Text = dr["Cuenta"].ToString();
                    t.Name = dr["Codigo"].ToString();
                    t.Tag = dr["id"].ToString();
                    
                    //t.Tag = dataTable.Rows.IndexOf(dr);

                    if (parentNode == null)
                    {
                        tvPlanContable.Nodes.Add(t);
                        childremNone = t;
                    }
                    else
                    {
                        parentNode.Nodes.Add(t);
                        childremNone = t;
                    }

                    AddNodes(Convert.ToInt32(dr["Codigo"].ToString()), childremNone);
                }
            } catch (Exception Ex) { }

        }

        private void Save() { }
        private void Edit()
        {
            edit = true;
            txtCodigoHijo.Text = txtCodigoPadre.Text;
            txtCuentaHijo.Text = string.Join(" ", txtCuentaPadre.Text.Split(' ').Skip(2).Take(txtCuentaPadre.Text.Length).ToArray());
            txtCodigoPadre.Text = Convert.ToString(Convert.ToInt32(txtCodigoPadre.Text.Length) <= 2 ? "1" : txtCodigoPadre.Text.Substring(0, txtCodigoPadre.Text.Length - 1));
        }
        private void Destroy() {
            int id = Convert.ToInt32(txtCuentaId.Text);
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro que quieres eliuminar esta cuenta?", "SISCONT .::. Plan Contable", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                if (planContable.Destroy(id))
                {
                    Clear();
                    All();
                }
            }
        }

        private void tvPlanContable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtCuentaPadre.Text = e.Node.Text;
            txtCodigoPadre.Text = e.Node.Name;
            txtCuentaId.Text = e.Node.Tag.ToString();
            chbUso.Checked = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoPadre, codigoHijo, id;
                bool uso = false, naturaleza = false, pago = false, destino = false;
                string cuenta;

                int codigoHijoFinal = Convert.ToInt32(txtCodigoPadre.Text + txtCodigoHijo.Text);

                codigoPadre = Convert.ToInt32(txtCodigoPadre.Text);
                codigoHijo = Convert.ToInt32(txtCodigoHijo.Text);
                id = Convert.ToInt32(txtCuentaId.Text);

                if (chbUso.Checked) uso = true;
                if (chbCuentaNaturaleza.Checked) naturaleza = true;
                if (chbCuentaPorPagar.Checked) pago = true;
                if (chbCuentaDestino.Checked) destino = true;

                cuenta = txtCuentaHijo.Text;

                if (codigoHijo.Equals("") | cuenta.Equals(""))
                {
                    MessageBox.Show("El código y/o la cuenta son datos obligatorios.");
                    txtCodigoHijo.Focus();
                }
                else
                {
                    if (codigoPadre == 0)
                    {
                        MessageBox.Show("No está permitido agregar cuentas hijas a esta cuenta con codigo 0");
                    }
                    else
                    {
                        if (newParent | codigoPadre == 1) codigoHijoFinal = codigoHijo;
                        if (edit)
                        {
                            if (planContable.Update(id, codigoHijoFinal, cuenta, uso, naturaleza, pago, destino, codigoPadre))
                            {
                                All();
                                Clear();
                                MessageBox.Show("Cuenta actualizada", "SICONT .::. Plan Contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            if (planContable.Insert(codigoHijoFinal, cuenta, uso, naturaleza, pago, destino, codigoPadre))
                            {
                                All();
                                Clear();
                                MessageBox.Show("Cuenta agregada", "SICONT .::. Plan Contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            } catch (Exception Ex) { }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Destroy();
        }

        private void btnNuevaCuentaPadre_Click(object sender, EventArgs e)
        {
            Clear();
            newParent = true;
            txtCuentaPadre.Text = "RAIZ";
            txtCodigoPadre.Text = "1";
        }
    }
}
