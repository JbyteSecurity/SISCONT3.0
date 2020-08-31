using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;

namespace Presentacion.reportes
{
    public partial class FormReportePDT : Form
    {
        public string ruc, rz;
        public int anio, usuario;

        public FormReportePDT()
        {
            InitializeComponent();
        }

        private void FormReportePDT_Load(object sender, EventArgs e)
        {
            this.sp_pdtTableAdapter.Fill(this.dSPdt.sp_pdt, ruc, anio, usuario);

            //Array que contendrá los parámetros
            ReportParameter[] parameters = new ReportParameter[3];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("ruc", ruc);
            parameters[1] = new ReportParameter("rz", rz);
            parameters[2] = new ReportParameter("anio", anio.ToString());

            this.reportViewerPDT.LocalReport.SetParameters(parameters);
            this.reportViewerPDT.RefreshReport();

        }
    }
}
