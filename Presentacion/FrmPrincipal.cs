using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmPrincipal : Form
    {
        TipoCambio tipoCambio = new TipoCambio();

        Proveedor proveedor = new Proveedor();

        public string username, nombreUsuario;
        public int idUsuario;
        public DataTable empresa = new DataTable();
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            getDolarTypeChange();
            NetworkState();
            UsuarioToolStripMenuItem.Text = nombreUsuario;
        }

        private void menuItemProveedores_Click(object sender, EventArgs e)
        {
            FrmProveedor frmProveedor = FrmProveedor.GetForm();
            frmProveedor.MdiParent = this;
            frmProveedor.Show();
            frmProveedor.BringToFront();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProgramaLibrosElectronicos frmProgramaLibrosElectronicos = FrmProgramaLibrosElectronicos.GetForm();
            frmProgramaLibrosElectronicos.MdiParent = this;
            frmProgramaLibrosElectronicos.Dock = DockStyle.Fill;
            frmProgramaLibrosElectronicos.empresa = empresa;
            frmProgramaLibrosElectronicos.username = username;
            frmProgramaLibrosElectronicos.idUsuario = idUsuario;
            frmProgramaLibrosElectronicos.Show();
            frmProgramaLibrosElectronicos.BringToFront();
        }
        private void tipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoCambio frmTipoCambio = FrmTipoCambio.GetForm();
            frmTipoCambio.MdiParent = this;
            frmTipoCambio.Show();
            frmTipoCambio.BringToFront();
        }

        private void detracciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetraccion frmDetraccion = FrmDetraccion.GetForm();
            frmDetraccion.MdiParent = this;
            frmDetraccion.Show();
            frmDetraccion.BringToFront();
        }

        private void getDolarTypeChange()
        {
            DataTable dataTableFecha = new DataTable();
            dataTableFecha = tipoCambio.Show(DateTime.UtcNow.ToString("dd/MM/yyyy"));

            if (dataTableFecha.Rows.Count <= 0)
            {
                HttpClient cliente = new HttpClient();
                cliente.BaseAddress = new Uri("http://www.sunat.gob.pe/");
                HttpResponseMessage rpta = cliente.GetAsync("cl-at-ittipcam/tcS01Alias").Result;
                if (rpta != null && rpta.IsSuccessStatusCode)
                {
                    string contenido = "";
                    using (MemoryStream ms = (MemoryStream)
                    rpta.Content.ReadAsStreamAsync().Result)
                    {
                        byte[] buffer = ms.ToArray();
                        contenido = Encoding.UTF8.GetString(buffer);
                        contenido = contenido.ToLower();
                    }
                    if (contenido.Length > 0)
                    {
                        File.WriteAllText("Sunat.txt", contenido);
                        int posInicioT1 = contenido.IndexOf("<table");
                        int posFinT1 = contenido.IndexOf("</table");
                        if (posInicioT1 > -1 && posFinT1 > -1)
                        {
                            int posInicioT2 = contenido.IndexOf("<table", posInicioT1 + 1);
                            int posFinT2 = contenido.IndexOf("</table", posFinT1 + 1);
                            string tabla = contenido.Substring(posInicioT2, posFinT2 - posInicioT2 + 8);
                            File.WriteAllText("Tabla.txt", tabla);
                            posInicioT1 = 0;
                            tabla = tabla.Replace("</strong>", "");
                            List<string> valores = new List<string>();
                            for (int i = 1; i < 4; i++)
                            {
                                posInicioT1 = tabla.LastIndexOf("</td");
                                if (posInicioT1 > -1)
                                {
                                    tabla = tabla.Substring(0, posInicioT1).Trim();
                                    posFinT1 = tabla.LastIndexOf(">");
                                    if (posFinT1 > -1)
                                    {
                                        valores.Add(tabla.Substring(posFinT1 + 1,
                                        tabla.Length - posFinT1 - 1).Trim());
                                    }
                                }
                            }
                            if (valores.Count > 0)
                            {
                                double venta = Convert.ToDouble(valores[0]);
                                double compra = Convert.ToDouble(valores[1]);
                                //txtFecha.Text = valores[2];

                                if (tipoCambio.Insert(DateTime.UtcNow.ToString("dd/MM/yyyy"), compra, venta))
                                    MessageBox.Show("Tipo de Cambio actualizado con fecha: " + DateTime.UtcNow.ToString("dd/MM/yyyy"), "Tipo de Cambio .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void mantenimientoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmUsuario frmUsuario = FrmUsuario.GetForm();
            frmUsuario.MdiParent = this;
            frmUsuario.Show();
            frmUsuario.BringToFront();
        }

        private void NetworkState()
        {
            string Estado = "";
            System.Uri Url = new System.Uri("http://198.12.230.10");
            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objetoResp;
            try
            {
                objetoResp = WebRequest.GetResponse();
                Estado = "Conectado al Servidor";
                objetoResp.Close();
            }
            catch (Exception e)
            {
                Estado = "Verifique la conexión al servidor";
            }
            WebRequest = null;
            tssConexion.Text = Estado;
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRol frmRol = FrmRol.GetForm();
            frmRol.MdiParent = this;
            frmRol.Show();
            frmRol.BringToFront();
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void menuItemPlanContable_Click(object sender, EventArgs e)
        {
            FrmPlanContable frmPlanContable = FrmPlanContable.GetForm();
            frmPlanContable.MdiParent = this;
            frmPlanContable.Dock = DockStyle.Fill;
            frmPlanContable.Show();
            frmPlanContable.BringToFront();
        }

        private void UsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
