namespace Presentacion.reportes
{
    partial class FormReportePDT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewerPDT = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSPdt = new Presentacion.DSPdt();
            this.sppdtBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_pdtTableAdapter = new Presentacion.DSPdtTableAdapters.sp_pdtTableAdapter();
            this.sp_pdtBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sppdtBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dSPdt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sppdtBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_pdtBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sppdtBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewerPDT
            // 
            this.reportViewerPDT.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "PDTInforme";
            reportDataSource1.Value = this.sppdtBindingSource1;
            this.reportViewerPDT.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerPDT.LocalReport.ReportEmbeddedResource = "Presentacion.reportes.ReportPDT.rdlc";
            this.reportViewerPDT.Location = new System.Drawing.Point(0, 0);
            this.reportViewerPDT.Name = "reportViewerPDT";
            this.reportViewerPDT.ServerReport.BearerToken = null;
            this.reportViewerPDT.Size = new System.Drawing.Size(1086, 603);
            this.reportViewerPDT.TabIndex = 0;
            // 
            // dSPdt
            // 
            this.dSPdt.DataSetName = "DSPdt";
            this.dSPdt.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sppdtBindingSource
            // 
            this.sppdtBindingSource.DataMember = "sp_pdt";
            this.sppdtBindingSource.DataSource = this.dSPdt;
            // 
            // sp_pdtTableAdapter
            // 
            this.sp_pdtTableAdapter.ClearBeforeFill = true;
            // 
            // sp_pdtBindingSource
            // 
            this.sp_pdtBindingSource.DataMember = "sp_pdt";
            this.sp_pdtBindingSource.DataSource = this.dSPdt;
            // 
            // sppdtBindingSource1
            // 
            this.sppdtBindingSource1.DataMember = "sp_pdt";
            this.sppdtBindingSource1.DataSource = this.dSPdt;
            // 
            // FormReportePDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 603);
            this.Controls.Add(this.reportViewerPDT);
            this.Name = "FormReportePDT";
            this.Text = "FormReportePDT";
            this.Load += new System.EventHandler(this.FormReportePDT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dSPdt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sppdtBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_pdtBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sppdtBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerPDT;
        private System.Windows.Forms.BindingSource sp_pdtBindingSource;
        private DSPdt dSPdt;
        private System.Windows.Forms.BindingSource sppdtBindingSource;
        private DSPdtTableAdapters.sp_pdtTableAdapter sp_pdtTableAdapter;
        private System.Windows.Forms.BindingSource sppdtBindingSource1;
    }
}