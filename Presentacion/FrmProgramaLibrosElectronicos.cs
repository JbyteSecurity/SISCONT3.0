using Negocios;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmProgramaLibrosElectronicos : Form
    {
        private PlanContable planContable = new PlanContable();
        private Proveedor proveedor = new Proveedor();
        private ComprobantePago comprobantePago = new ComprobantePago();
        private TipoCambio tipoCambio = new TipoCambio();

        private readonly Compras compras = new Compras();
        private Ventas ventas = new Ventas();
        private Detraccion detraccion = new Detraccion();

        string TXTRoute = null;

        BindingSource bindingSourceCompras = new BindingSource();
        DataTable dataTableCompras = new DataTable();

        public string username;
        public int idUsuario;
        public DataTable empresa = new DataTable();

        Sunat sunat = new Sunat();

        Ruc RucS = new Ruc();

        Pdt pdt = new Pdt();
        private FrmProgramaLibrosElectronicos()
        {
            InitializeComponent();
        }

        private static FrmProgramaLibrosElectronicos Instancia = null;

        public static FrmProgramaLibrosElectronicos GetForm()
        {
            if (Instancia == null)
            {
                Instancia = new FrmProgramaLibrosElectronicos();
                Instancia.FormClosed += new FormClosedEventHandler(Reset);//SOLO PARA FORMULARIOS
            }
            return Instancia;
        }

        private static void Reset(object sender, FormClosedEventArgs e)//SOLO PARA FORMULARIOS
        {
            Instancia = null;
        }

        private void FrmProgramaLibrosElectronicos_Load(object sender, EventArgs e)
        {
            txtNombreAnio.Text = DateTime.UtcNow.ToString("yyyy");
            txtNombreMes.Text = DateTime.UtcNow.ToString("MM");
            txtNombreRuc.Text = empresa.Rows[0]["ruc"].ToString();

            for (int i = 2; i < DgvPDT.Rows.Count; i++)
            {
                DgvPDT.Columns[i].DefaultCellStyle.Format = "0.00##";
            }

            // TODO: esta línea de código carga datos en la tabla 'dSDetracciones.sp_all_combo_detracciones' Puede moverla o quitarla según sea necesario.
            this.TADetraccionesTableAdapter.Fill(this.dSDetracciones.sp_all_combo_detracciones);

            FillPDT();

            FillDataGridViewCompras();
            FillDataGridViewVentas();

            FillComboTipoComprobante();
            FillComboCuentas();

            //FillComboCodigo();

            removeColumnsCompras();
            removeColumnsVentas();
            RemoveColumnPDT();

            //this.dgvRegistroCompras.DataSource = this.BSComprasBindingSource;


            GetRoute();

            dgvRegistroCompras.RowHeadersVisible = false;
            dgvRegistroCompras.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular);
            dgvRegistroCompras.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular);

            dgvRegistroVentas.RowHeadersVisible = false;
            dgvRegistroVentas.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular);
            dgvRegistroVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular);
            lblPeriodoActual.Text = "Periodo Actual: " + DateTime.UtcNow.ToString("MMMM") + " " + DateTime.UtcNow.ToString("yyyy");
            lblRazonSocial.Text = empresa.Rows[0]["razon_social"].ToString();
            //lblPeriodoActual.Text = username;
            dgvRegistroCompras.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRegistroVentas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            CalcPDT();
            ReadOnlyPDT();
        }

        private void cellContentClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            //mostrarProveedor();

        }

        private void tabIndexChangedEvent(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveCompras();
            SaveVentas();
            FillPDT(true);
        }

        private void GetRoute()
        {
            txtRutaTXT.Text = "D:\\SISCONT\\" + txtNombreRuc.Text + "\\" + txtNombreAnio.Text + "\\" + txtNombreMes.Text.PadLeft(2, '0') + "\\";
        }

        private void FillDataGridViewCompras(bool filter = false)
        {
            if (filter)
            {
                int filtroMes = Convert.ToInt32(txtNombreMes.Text);
                int filtroAnio = Convert.ToInt32(txtNombreAnio.Text);

                this.TAComprasTableAdapter.FillByYearAndMonth(this.dSCompras.tblRegistroCompras, filtroMes, filtroAnio, txtNombreRuc.Text, idUsuario);
                if (Convert.ToString(filtroMes) != DateTime.UtcNow.ToString("MM"))
                dgvRegistroCompras.ReadOnly = true;
            }
            else
            {
                this.TAComprasTableAdapter.FillCurrentMonth(this.dSCompras.tblRegistroCompras, txtNombreRuc.Text, idUsuario);
                ReadOnlyCells();
            }
        }
        private void FillDataGridViewVentas(bool filter = false)
        {
            DataTable dataTable = new DataTable();

            if (filter)
            {
                int filtroMes = Convert.ToInt32(txtNombreMes.Text);
                int filtroAnio = Convert.ToInt32(txtNombreAnio.Text);
                this.TAVentasTableAdapter.FillByYearAndMonth(this.dSVentas.tblRegistroVentas, filtroMes, filtroAnio, txtNombreRuc.Text, idUsuario);
                dgvRegistroVentas.ReadOnly = true;
            }
            else
            {
                this.TAVentasTableAdapter.FillCurrentMonth(this.dSVentas.tblRegistroVentas, txtNombreRuc.Text, idUsuario);
                ReadOnlyCells();
            }
        }

        private void ReadOnlyCells()
        {
            dgvRegistroCompras.ReadOnly = false;
            comprasID.ReadOnly = true;
            comprasProveedorRazonSocial.ReadOnly = true;
            comprasIgv.ReadOnly = true;
            comprasTipoCambio.ReadOnly = true;
            comprasConstanciaMonto.ReadOnly = true;

            dgvRegistroVentas.ReadOnly = false;
            ventasID.ReadOnly = true;
            ventasProveedorRazonSocial.ReadOnly = true;
            ventasIgv.ReadOnly = true;
            ventasTipoCambio.ReadOnly = true;
            ventasReferencia.ReadOnly = true;
        }

        //GUARDAR Compras 
        private void SaveCompras()
        {
            try
            {
                foreach (DataGridViewRow row in dgvRegistroCompras.Rows)
                {
                    if (row.Cells["comprasFechaEmision"].Value != null)
                    {
                        int id = row.Cells["comprasID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["comprasID"].Value) : 0;
                        //int comprasMes = row.Cells["comprasMes"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["comprasMes"].Value) : 0;

                        string comprasNumeroRegistro = row.Cells["comprasNumeroRegistro"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasNumeroRegistro"].Value) : "";
                        string comprasFechaEmision = row.Cells["comprasFechaEmision"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasFechaEmision"].Value) : "";
                        string comprasFechaPago = row.Cells["comprasFechaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasFechaPago"].Value) : "1900-01-01";
                        string comprasCdpTipo = row.Cells["comprasCdpTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCdpTipo"].Value) : "";
                        string comprasCdpSerie = row.Cells["comprasCdpSerie"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCdpSerie"].Value).ToUpper() : "";
                        string comprasCdpNumeroDocumento = row.Cells["comprasCdpNumeroDocumento"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCdpNumeroDocumento"].Value) : "";
                        string comprasProveedorTipo = row.Cells["comprasProveedorTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasProveedorTipo"].Value) : "";
                        string comprasProveedorNumeroDocumento = row.Cells["comprasProveedorNumeroDocumento"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasProveedorNumeroDocumento"].Value) : "";
                        //string comprasProveedorTipoDocumento = row.Cells["comprasProveedorTipoDocumento"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasProveedorTipoDocumento"].Value) : "";

                        string comprasProveedorRazonSocial = row.Cells["comprasProveedorRazonSocial"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasProveedorRazonSocial"].Value) : "";
                        string comprasCuenta = row.Cells["comprasCuenta"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCuenta"].Value) : "";
                        string comprasDescripcion = row.Cells["comprasDescripcion"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasDescripcion"].Value) : "";
                        double comprasBaseImponible = row.Cells["comprasBaseImponible"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasBaseImponible"].Value) : 0;
                        double comprasIgv = row.Cells["comprasIgv"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasIgv"].Value) : 0;
                        double comprasNoGravada = row.Cells["comprasNoGravada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasNoGravada"].Value) : 0;
                        double comprasDescuento = row.Cells["comprasDescuento"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasDescuento"].Value) : 0;
                        double comprasImporteTotal = row.Cells["comprasImporteTotal"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasImporteTotal"].Value) : 0;
                        double comprasDolares = row.Cells["comprasDolares"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasDolares"].Value) : 0;
                        double comprasTipoCambio = row.Cells["comprasTipoCambio"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasTipoCambio"].Value) : 0;

                        double comprasPercepcion = row.Cells["comprasPercepcion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasPercepcion"].Value) : 0;
                        string comprasDestino = row.Cells["comprasCuentaDestino"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCuentaDestino"].Value) : "";
                        string comprasDescripcionDestino = row.Cells["comprasDescripcionDestino"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasDescripcionDestino"].Value) : "";
                        string comprasCuentaDestino = row.Cells["comprasCuentaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCuentaPago"].Value) : "";
                        //string comprasPago = row.Cells["comprasPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasPago"].Value) : "";
                        string comprasCodigo = row.Cells["comprasCodigo"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasCodigo"].Value) : "";
                        string comprasConstanciaNumero = row.Cells["comprasConstanciaNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasConstanciaNumero"].Value) : "";
                        string comprasConstanciaFechaPago = row.Cells["comprasConstanciaFechaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasConstanciaFechaPago"].Value) : "1900-01-01";
                        double comprasConstanciaMonto = row.Cells["comprasConstanciaMonto"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasConstanciaMonto"].Value) : 0;
                        string comprasConstanciaReferencia = row.Cells["comprasConstanciaReferencia"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasConstanciaReferencia"].Value) : "";

                        string BancarizacionFecha = row.Cells["BancarizacionFecha"].Value != DBNull.Value ? Convert.ToString(row.Cells["BancarizacionFecha"].Value) : "1900-01-01";
                        string BancarizacionBco = row.Cells["BancarizacionBco"].Value != DBNull.Value ? Convert.ToString(row.Cells["BancarizacionBco"].Value) : "";
                        int BancarizacionOperacion = row.Cells["BancarizacionOperacion"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["BancarizacionOperacion"].Value) : 0;
                        double comprasConversionDolares = row.Cells["comprasConversionDolares"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["comprasConversionDolares"].Value) : 0;

                        string ReferenciaFecha = row.Cells["comprasConstanciaFechaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasReferenciaFecha"].Value) : "";
                        string ReferenciaTipo = row.Cells["comprasReferenciaTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasReferenciaTipo"].Value) : "";
                        string ReferenciaSerie = row.Cells["comprasReferenciaSerie"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasReferenciaSerie"].Value) : "";
                        string ReferenciaNumero = row.Cells["comprasReferenciaNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasReferenciaNumero"].Value) : "";
                        string comprasObservacion = row.Cells["comprasObservacion"].Value != DBNull.Value ? Convert.ToString(row.Cells["comprasObservacion"].Value) : "";

                        string rucEmpresa = txtNombreRuc.Text;

                        if (id < 0)
                        {
                            compras.Insert(
                                comprasNumeroRegistro, comprasFechaEmision, comprasFechaPago, comprasCdpTipo,
                                comprasCdpSerie, comprasCdpNumeroDocumento, comprasProveedorTipo, comprasProveedorNumeroDocumento,
                                comprasProveedorRazonSocial, comprasCuenta, comprasDescripcion,
                                comprasBaseImponible, comprasIgv, comprasNoGravada, comprasDescuento, comprasImporteTotal, comprasDolares,
                                comprasTipoCambio, comprasPercepcion, comprasDestino, comprasDescripcionDestino, comprasCuentaDestino,
                                comprasCodigo, comprasConstanciaNumero, comprasConstanciaFechaPago, comprasConstanciaMonto, comprasConstanciaReferencia,
                                BancarizacionFecha, BancarizacionBco, BancarizacionOperacion, ReferenciaFecha, ReferenciaTipo, ReferenciaSerie, ReferenciaNumero,
                                idUsuario, comprasConversionDolares, comprasObservacion, rucEmpresa
                            );
                        }
                        else
                        {
                            compras.Update(
                                id, comprasNumeroRegistro, comprasFechaEmision, comprasFechaPago, comprasCdpTipo,
                                comprasCdpSerie, comprasCdpNumeroDocumento, comprasProveedorTipo, comprasProveedorNumeroDocumento,
                                comprasProveedorRazonSocial, comprasCuenta, comprasDescripcion, comprasBaseImponible, comprasIgv,
                                comprasNoGravada, comprasDescuento, comprasImporteTotal, comprasDolares, comprasTipoCambio, comprasPercepcion,
                                comprasDestino, comprasDescripcionDestino, comprasCuentaDestino, comprasCodigo, comprasConstanciaNumero,
                                comprasConstanciaFechaPago, comprasConstanciaMonto, comprasConstanciaReferencia, BancarizacionFecha, BancarizacionBco,
                                BancarizacionOperacion, ReferenciaFecha, ReferenciaTipo, ReferenciaSerie, ReferenciaNumero,
                                comprasConversionDolares, comprasObservacion
                                );
                        }
                    }
                }
                MessageBox.Show("Se han guardado los cambios correctamente", "Compras .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillDataGridViewCompras();
            }
            catch (Exception) {}
        }

        //GUARDAR Ventas 
        private void SaveVentas()
        {
            try
            {
                foreach (DataGridViewRow row in dgvRegistroVentas.Rows)
                {
                    if (row.Cells["VentasFechaEmision"].Value != DBNull.Value)
                    {
                        int ventasId = Convert.ToInt32(row.Cells["ventasId"].Value);
                        string ventasNumeroRegistro = row.Cells["ventasNumeroRegistro"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasNumeroRegistro"].Value) : "";
                        string ventasFechaEmision = row.Cells["ventasFechaEmision"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasFechaEmision"].Value) : "";
                        string ventasFechaPago = row.Cells["ventasFechaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasFechaPago"].Value) : "";
                        string ventasCdpTipo = row.Cells["ventasCdpTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCdpTipo"].Value) : "";
                        string ventasCdpSerie = row.Cells["ventasCdpSerie"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCdpSerie"].Value).ToUpper() : "";
                        string ventasCdpNumero = row.Cells["ventasCdpNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCdpNumero"].Value) : "";
                        string ventasProveedorTipo = row.Cells["ventasProveedorTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasProveedorTipo"].Value) : "";
                        string ventasProveedorNumero = row.Cells["ventasProveedorNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasProveedorNumero"].Value).ToUpper() : "";
                        string ventasProveedorRazonSocial = row.Cells["ventasProveedorRazonSocial"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasProveedorRazonSocial"].Value) : "";

                        string ventasCuenta = row.Cells["ventasCuenta"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCuenta"].Value) : "";
                        string ventasDescripcion = row.Cells["ventasDescripcion"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasDescripcion"].Value) : "";
                        double ventasValorExportacion = row.Cells["ventasValorExportacion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasValorExportacion"].Value) : 0;
                        double ventasBaseImponible = row.Cells["ventasBaseImponible"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasBaseImponible"].Value) : 0;
                        double ventasImporteTotalExonerada = row.Cells["ventasImporteTotalExonerada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasImporteTotalExonerada"].Value) : 0;
                        double ventasImporteTotalInafecta = row.Cells["ventasImporteTotalInafecta"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasImporteTotalInafecta"].Value) : 0;
                        double ventasIgv = row.Cells["ventasIgv"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasIgv"].Value) : 0;
                        double ventasImporteTotal = row.Cells["ventasImporteTotal"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasImporteTotal"].Value) : 0;
                        double ventasTipoCambio = row.Cells["ventasTipoCambio"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasTipoCambio"].Value) : 0;
                        double ventasDolares = row.Cells["ventasDolares"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasDolares"].Value) : 0;
                        //double ventasDolaresConversion = row.Cells["ventasDolaresConversion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasDolaresConversion"].Value) : 0;

                        double ventasIgvRetencion = row.Cells["ventasIgvRetencion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasIgvRetencion"].Value) : 0;
                        string ventasCuentaDestino = row.Cells["ventasCuentaDestino"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCuentaDestino"].Value) : "";
                        string ventasCuentaDestinoDescripcion = row.Cells["ventasCuentaDestinoDescripcion"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCuentaDestinoDescripcion"].Value) : "";
                        string ventasReferenciaFecha = row.Cells["ventasReferenciaFecha"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasReferenciaFecha"].Value) : "";
                        string ventasReferenciaTipo = row.Cells["ventasReferenciaTipo"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasReferenciaTipo"].Value) : "";
                        string ventasReferenciaSerie = row.Cells["ventasReferenciaSerie"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasReferenciaSerie"].Value) : "";
                        string ventasReferenciaNumero = row.Cells["ventasReferenciaNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasReferenciaNumero"].Value) : "";
                        string ventasCodigo = row.Cells["ventasCodigo"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasCodigo"].Value) : "";
                        string ventasConstanciaNumero = row.Cells["ventasConstanciaNumero"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasConstanciaNumero"].Value) : "";
                        string ventasConstanciaFechaPago = row.Cells["ventasConstanciaFechaPago"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasConstanciaFechaPago"].Value) : "";

                        double ventasDetraccionSoles = row.Cells["ventasDetraccionSoles"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["ventasDetraccionSoles"].Value) : 0;
                        string ventasReferencia = row.Cells["ventasReferencia"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasReferencia"].Value) : "";
                        string ventasObservacion = row.Cells["ventasObservacion"].Value != DBNull.Value ? Convert.ToString(row.Cells["ventasObservacion"].Value) : "";
                        string rucEmpresa = txtNombreRuc.Text;

                        if (ventasId < 0)
                        {
                            ventas.Insert(
                                ventasNumeroRegistro, ventasFechaEmision, ventasFechaPago, ventasCdpTipo, ventasCdpSerie, ventasCdpNumero, ventasProveedorTipo,
                                ventasProveedorNumero, ventasProveedorRazonSocial, ventasCuenta, ventasDescripcion, ventasValorExportacion, ventasBaseImponible,
                                ventasImporteTotalExonerada, ventasImporteTotalInafecta, ventasIgv, ventasImporteTotal, ventasTipoCambio, ventasDolares,
                                ventasIgvRetencion, ventasCuentaDestino, ventasCuentaDestinoDescripcion, ventasReferenciaFecha, ventasReferenciaTipo, ventasReferenciaSerie,
                                ventasReferenciaNumero, ventasCodigo, ventasConstanciaNumero, ventasConstanciaFechaPago, ventasDetraccionSoles, ventasReferencia,
                                ventasObservacion, idUsuario, rucEmpresa
                                );
                        }
                        else
                        {
                            ventas.Update(
                                ventasId, ventasNumeroRegistro, ventasFechaEmision, ventasFechaPago, ventasCdpTipo, ventasCdpSerie, ventasCdpNumero, ventasProveedorTipo,
                                ventasProveedorNumero, ventasProveedorRazonSocial, ventasCuenta, ventasDescripcion, ventasValorExportacion, ventasBaseImponible,
                                ventasImporteTotalExonerada, ventasImporteTotalInafecta, ventasIgv, ventasImporteTotal, ventasTipoCambio, ventasDolares,
                                ventasIgvRetencion, ventasCuentaDestino, ventasCuentaDestinoDescripcion, ventasReferenciaFecha, ventasReferenciaTipo, ventasReferenciaSerie,
                                ventasReferenciaNumero, ventasCodigo, ventasConstanciaNumero, ventasConstanciaFechaPago, ventasDetraccionSoles, ventasReferencia,
                                ventasObservacion
                                );
                        }

                    }
                }
                MessageBox.Show("Se han guardado los cambios", "Ventas .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillDataGridViewVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo insertar los datos por: " + ex);
            }
        }

        private void FillComboTipoComprobante()
        {
            //Compras
            comprasCdpTipo.DisplayMember = "Detail";
            comprasCdpTipo.ValueMember = "numero";
            comprasCdpTipo.DataSource = comprobantePago.GetAllCpdTypes();

            //Ventas
            ventasCdpTipo.DisplayMember = "Detail";
            ventasCdpTipo.ValueMember = "numero";
            ventasCdpTipo.DataSource = comprobantePago.GetAllCpdTypes();
        }

        private void FillComboCuentas()
        {
            //Compras
            comprasCuenta.DisplayMember = "Detail";
            comprasCuenta.ValueMember = "Codigo";
            comprasCuenta.DataSource = planContable.ShowNaturaleza();

            comprasCuentaPago.DisplayMember = "Detail";
            comprasCuentaPago.ValueMember = "Codigo";
            comprasCuentaPago.DataSource = planContable.ShowPago();

            comprasCuentaDestino.DisplayMember = "Detail";
            comprasCuentaDestino.ValueMember = "Codigo";
            comprasCuentaDestino.DataSource = planContable.ShowDestino();

            //Ventas
            ventasCuenta.DisplayMember = "Detail";
            ventasCuenta.ValueMember = "Codigo";
            ventasCuenta.DataSource = planContable.ShowNaturalezaV();

            ventasCuentaDestino.DisplayMember = "Detail";
            ventasCuentaDestino.ValueMember = "Codigo";
            ventasCuentaDestino.DataSource = planContable.ShowCobrar();
        }

        private void FillComboCodigo()
        {
            comprasCodigo.DisplayMember = "Combo";
            comprasCodigo.ValueMember = "codigo";
            comprasCodigo.DataSource = detraccion.GetForCombo();

            ventasCodigo.DisplayMember = "Combo";
            ventasCodigo.ValueMember = "codigo";
            ventasCodigo.DataSource = detraccion.GetForCombo();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            //
        }

        public static Boolean isDate(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dgvRegistroCompras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvRegistroCompras.EditingControlShowing += dgvRegistroCompras_EditingControlShowing;
            switch (e.ColumnIndex)
            {
                case 2:
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value.ToString() as String))
                            {
                                string fecha = null, compra = null, venta = null;
                                DataTable dataTableTipoCambio = new DataTable();

                                if (isDate(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value.ToString()))
                                {
                                    fecha = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value.ToString();

                                    if (DateTime.Parse(fecha) <= DateTime.Parse(DateTime.UtcNow.ToString("dd/MM/yyyy")))
                                    {
                                        dataTableTipoCambio = tipoCambio.Show(fecha);
                                        if (dataTableTipoCambio.Rows.Count > 0)
                                        {
                                            compra = dataTableTipoCambio.Rows[0]["Compra"].ToString();
                                            venta = dataTableTipoCambio.Rows[0]["Venta"].ToString();

                                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasTipoCambio"].Value = venta;
                                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaPago"].Value = fecha;
                                        }
                                        else
                                            MessageBox.Show("No se encontro un tipo de cambio para la fecha: " + fecha, "Tipo de Cambio .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se aceptan Fechas Futuras");
                                        dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaPago"].Value = "";
                                        dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value = "";
                                        dgvRegistroCompras.CurrentCell = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"];
                                    }
                                }
                                else
                                    MessageBox.Show("(" + dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasFechaEmision"].Value.ToString() + ") No es una fecha valida \nIngrese una fecha válida con los siguientes formatos: \ndd/mm/yyyy o yyyy-mm-dd");
                            }
                        }
                    }
                    catch (Exception) { }
                    break;

                case 4:
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value != DBNull.Value)
                        {
                            if (String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value.ToString() as String))
                            {
                                string comprasCDPTipo = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value.ToString();
                                if (comprasCDPTipo.Equals("07"))
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value) * (-1);
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 8: //obtener proveedor
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorNumeroDocumento"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorNumeroDocumento"].Value.ToString() as string))
                            {
                                string ruc;
                                DataTable razonSocial = new DataTable();
                                ruc = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorNumeroDocumento"].Value.ToString();
                                razonSocial = proveedor.Show(ruc);

                                if (razonSocial == null)
                                {
                                    //ArrayList arraySunat = sunat.buscarRuc(ruc);
                                    ArrayList arraySunat = RucS.GetData(ruc);

                                    string razonSocialSunat = arraySunat.ToArray()[0].ToString();
                                    string direccionSunat = arraySunat.ToArray()[1].ToString();

                                    if (razonSocialSunat == null)
                                    {
                                        arraySunat = sunat.buscarRuc(ruc);

                                        razonSocialSunat = arraySunat.ToArray()[0].ToString();
                                        direccionSunat = arraySunat.ToArray()[1].ToString();
                                    }

                                    if (razonSocialSunat != null)
                                    {
                                        dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorRazonSocial"].Value = razonSocialSunat;
                                        proveedor.Insert(ruc, razonSocialSunat, null, null, direccionSunat, null, null);
                                    } else MessageBox.Show("No se encontro al proveedor con ruc: " + ruc, "Compras .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                else
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorRazonSocial"].Value = razonSocial.Rows[0]["razon_social"].ToString();
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 12:
                case 14:
                case 15:
                    //Calculos de No BaseInmponible
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasBaseImponible"].Value != DBNull.Value)
                        {
                            double baseImponible = 0, descuento = 0, igv = 0, noGravada = 0, negativo = 1, importe_total;
                            string comprasCDPTipo = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value.ToString();

                            if (comprasCDPTipo.Equals("07")) negativo = (-1);

                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasBaseImponible"].Value.ToString() as String))
                                baseImponible = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasBaseImponible"].Value.ToString());

                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDescuento"].Value.ToString() as String))
                                descuento = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDescuento"].Value.ToString());

                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Value.ToString() as String))
                                noGravada = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Value.ToString());

                            if (noGravada < 0)
                            {
                                dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Style.ForeColor = Color.Red;
                                dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Style.Font = new Font(dgvRegistroCompras.DefaultCellStyle.Font, FontStyle.Bold);
                            }
                            else
                            {
                                dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Style.ForeColor = Color.Black;
                                dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Style.Font = new Font(dgvRegistroCompras.DefaultCellStyle.Font, FontStyle.Regular);
                            }

                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasNoGravada"].Value = noGravada.ToString("F");
                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDescuento"].Value = descuento.ToString("F");

                            igv = Math.Round(((baseImponible + descuento) * 0.18) * negativo, 2, MidpointRounding.AwayFromZero);


                            importe_total = Math.Round(((baseImponible + igv) * negativo) + (noGravada) - descuento, 2, MidpointRounding.AwayFromZero);


                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasIgv"].Value = igv.ToString("F");
                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasImporteTotal"].Value = importe_total.ToString("F");
                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasBaseImponible"].Value = Math.Round((baseImponible * negativo), 2, MidpointRounding.AwayFromZero).ToString("F");
                            //importe_total = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasImporteTotal"].Value.ToString());

                            if (importe_total >= 3500)
                            {
                                if (String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Value as String) || String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Value as String) || String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Value as String))
                                {
                                    //MessageBox.Show("Ingrese Bancarización");
                                    //dgvRegistroCompras.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Teal;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Style.BackColor = Color.Red;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Style.BackColor = Color.Red;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    //dgvRegistroCompras.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Style.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Style.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Style.BackColor = Color.White;
                                }

                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 17: // Dolares
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDolares"].Value != DBNull.Value && Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDolares"].Value) != 0)
                        {

                            double tipoCambi = 0, dolares = 0, descuento = 0, igv = 0, noGravada = 0, negativo = 1;
                            string comprasCDPTipo = dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCdpTipo"].Value.ToString();
                            if (comprasCDPTipo.Equals("07")) negativo = (-1);

                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDolares"].Value.ToString() as String))
                                dolares = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDolares"].Value.ToString());

                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasTipoCambio"].Value.ToString() as String))
                                tipoCambi = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasTipoCambio"].Value.ToString());

                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasConversionDolares"].Value = Math.Round((dolares * tipoCambi), 2);

                            //Calcular en soles
                            double convertDolares = Math.Round((dolares * tipoCambi), 2);
                            descuento = 0;
                            noGravada = 0;
                            igv = 0;

                            double doBaseImponible = convertDolares / 1.18;
                            igv = doBaseImponible * 0.18;

                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasDolares"].Value = dolares.ToString("F");
                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasBaseImponible"].Value = Math.Round(doBaseImponible * negativo, 2).ToString("F");

                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasIgv"].Value = Math.Round(igv * negativo, 2).ToString("F");

                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasImporteTotal"].Value = Math.Round((doBaseImponible + descuento + igv + noGravada) * negativo, 2).ToString("F");

                            if (dolares >= 1000)
                            {
                                if (String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Value as String) || String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Value as String) || String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Value as String))
                                {
                                    //MessageBox.Show("Ingrese Bancarización");
                                    //dgvRegistroCompras.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Teal;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Style.BackColor = Color.Red;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Style.BackColor = Color.Red;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    //dgvRegistroCompras.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionFecha"].Style.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionBco"].Style.BackColor = Color.White;
                                    dgvRegistroCompras.Rows[e.RowIndex].Cells["BancarizacionOperacion"].Style.BackColor = Color.White;
                                }

                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 24: // Codigo
                    try
                    {
                        if (dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCodigo"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCodigo"].Value.ToString() as String))
                            {
                                double comprasImporteTotal = 0;
                                int compraCodigo = 0;
                                string codComp = null;

                                codComp = string.Join(" ", dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasCodigo"].Value.ToString().Split(' ').Skip(0).Take(1).ToArray());
                                compraCodigo = Convert.ToInt32(codComp);

                                if (compraCodigo != 0)
                                {

                                    if (String.IsNullOrEmpty(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasImporteTotal"].Value.ToString() as String))
                                        MessageBox.Show("Ingrese un Importe Total");
                                    else comprasImporteTotal = Convert.ToDouble(dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasImporteTotal"].Value.ToString());



                                    DataTable dataTableDetraccion = new DataTable();
                                    dataTableDetraccion = detraccion.Show(compraCodigo);
                                    if (dataTableDetraccion.Rows.Count > 0)
                                    {
                                        double detraccionProcentaje = Convert.ToDouble(dataTableDetraccion.Rows[0]["porcentaje"].ToString());
                                        double detraccionMonto = Convert.ToDouble(dataTableDetraccion.Rows[0]["monto"].ToString());
                                        if (comprasImporteTotal > detraccionMonto)
                                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasConstanciaReferencia"].Value = Math.Round(comprasImporteTotal * detraccionProcentaje, 2);
                                        else
                                            dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasConstanciaReferencia"].Value = "";
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Ingrese un número entero (1 - 5)");
                                        dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasConstanciaReferencia"].Value = "";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
            }
        }

        private void dgvRegistroVentas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2:
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value.ToString() as String))
                            {

                                string fecha = null, compra = null, venta = null;
                                DataTable dataTableTipoCambio = new DataTable();

                                if (isDate(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value.ToString()))
                                {
                                    fecha = dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value.ToString().Substring(0, 10);

                                    if (DateTime.Parse(fecha) <= DateTime.Parse(DateTime.UtcNow.ToString("dd/MM/yyyy")))
                                    {

                                        dataTableTipoCambio = tipoCambio.Show(fecha);
                                        if (dataTableTipoCambio.Rows.Count > 0)
                                        {
                                            compra = dataTableTipoCambio.Rows[0]["Compra"].ToString();
                                            venta = dataTableTipoCambio.Rows[0]["Venta"].ToString();

                                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaPago"].Value = fecha;
                                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasTipoCambio"].Value = venta;
                                        }
                                        else MessageBox.Show("No se encontro un tipo de cambio para la fecha: " + fecha, "Tipo de Cambio .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se aceptan Fechas Futuras");
                                        dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaPago"].Value = "";
                                        dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value = "";
                                        dgvRegistroVentas.CurrentCell = dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"];
                                    }
                                }
                                else MessageBox.Show("(" + dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasFechaEmision"].Value.ToString() + ") No es una fecha valida \nIngrese una fecha válida con los siguientes formatos: \ndd/mm/yyyy o yyyy-mm-dd");
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 8:
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() as String))
                            {
                                string ruc;
                                DataTable razonSocial = new DataTable();
                                ruc = dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                                razonSocial = proveedor.Show(ruc);

                                if (razonSocial == null)
                                {
                                    ArrayList arraySunat = RucS.GetData(ruc);

                                    string razonSocialSunat = arraySunat.ToArray()[0].ToString();
                                    string direccionSunat = arraySunat.ToArray()[1].ToString();

                                    if (razonSocialSunat == null)
                                    {
                                        arraySunat = sunat.buscarRuc(ruc);

                                        razonSocialSunat = arraySunat.ToArray()[0].ToString();
                                        direccionSunat = arraySunat.ToArray()[1].ToString();
                                    }

                                    if (razonSocialSunat != null)
                                    {
                                        dgvRegistroCompras.Rows[e.RowIndex].Cells["comprasProveedorRazonSocial"].Value = razonSocialSunat;
                                        proveedor.Insert(ruc, razonSocialSunat, null, null, direccionSunat, null, null);
                                    }
                                    else MessageBox.Show("No se encontro al proveedor con ruc: " + ruc, "Compras .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = razonSocial.Rows[0]["razon_social"].ToString();
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 17: // BaseImponible
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value != DBNull.Value)
                        {
                            double biImporteTotal = 0, negativo = 1;
                            string ventasTipoCDP = dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasCdpTipo"].Value.ToString();
                            if (ventasTipoCDP.Equals("07")) negativo = (-1);

                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString()))
                                MessageBox.Show("Ingrese un Importe Total");
                            else
                                biImporteTotal = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString());

                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value = Math.Round((biImporteTotal / 1.18) * negativo, 2).ToString("F");


                            double igvImporteTotal = 0, igvBaseImponible = 0;
                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString()))
                                MessageBox.Show("Ingrese un Importe Total");
                            else
                                igvImporteTotal = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString());

                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value.ToString()))
                                MessageBox.Show("Ingrese una Base Imponible");
                            else
                                igvBaseImponible = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value.ToString());

                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasIgv"].Value = Math.Round((igvImporteTotal - igvBaseImponible) * negativo, 2).ToString("F");
                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value = (igvImporteTotal * negativo).ToString("F");
                        }
                    }
                    catch (Exception) { }
                    break;
                case 10:
                case 21:
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() as String))
                            {
                                string codigo;
                                string cuenta;
                                codigo = dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                                cuenta = planContable.GetAcount(codigo);
                                if (cuenta == null)
                                    MessageBox.Show("No se encontro una cuenta con código: " + codigo, "Compras .::. Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    dgvRegistroVentas.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = cuenta;
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
                case 19: // Dolares
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasDolares"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasDolares"].Value.ToString() as String))
                            {
                                double dolares, tipoCambio = 0, impTotal, igv, baseImponible, negativo = 1;
                                string ventasTipoCDP = dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasCdpTipo"].Value.ToString();
                                if (ventasTipoCDP.Equals("07")) negativo = (-1);

                                dolares = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasDolares"].Value.ToString());

                                if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasTipoCambio"].Value != DBNull.Value && !String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasTipoCambio"].Value.ToString() as String))
                                    tipoCambio = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasTipoCambio"].Value.ToString());

                                impTotal = Math.Round(tipoCambio * dolares, 2);
                                baseImponible = Math.Round((impTotal / 1.18) * negativo, 2);
                                igv = Math.Round((impTotal - baseImponible) * negativo, 2);

                                dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value = impTotal.ToString("F");
                                dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value = baseImponible.ToString("F");
                                dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasIGV"].Value = igv.ToString("F");
                                dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasDolares"].Value = dolares.ToString("F");
                            }
                        }

                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value != DBNull.Value)
                        {
                            double biImporteTotal = 0, negativo = 1;
                            string ventasTipoCDP = dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasCdpTipo"].Value.ToString();
                            if (ventasTipoCDP.Equals("07")) negativo = (-1);

                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString()))
                                MessageBox.Show("Ingrese un Importe Total");
                            else
                                biImporteTotal = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString());

                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value = Math.Round((biImporteTotal / 1.18) * negativo, 2).ToString("F");


                            double igvImporteTotal = 0, igvBaseImponible = 0;
                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString()))
                                MessageBox.Show("Ingrese un Importe Total");
                            else
                                igvImporteTotal = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString());

                            if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value.ToString()))
                                MessageBox.Show("Ingrese una Base Imponible");
                            else
                                igvBaseImponible = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasBaseImponible"].Value.ToString());

                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasIgv"].Value = Math.Round((igvImporteTotal - igvBaseImponible) * negativo, 2).ToString("F");
                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value = (igvImporteTotal * negativo).ToString("F");
                        }
                    }
                    catch (Exception) { }
                    break;
                case 27:
                    try
                    {
                        if (dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value != DBNull.Value)
                        {
                            if (!String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasCodigo"].Value.ToString() as String))
                            {
                                double ventasImporteTotal = 0;
                                int ventasCodigo = 0;
                                string ventCod;

                                ventCod = string.Join(" ", dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasCodigo"].Value.ToString().Split(' ').Skip(0).Take(1).ToArray());

                                ventasCodigo = Convert.ToInt32(ventCod);

                                if (ventasCodigo != 0)
                                {

                                    if (String.IsNullOrEmpty(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString() as String))
                                        MessageBox.Show("Ingrese un Importe Total");
                                    else
                                        ventasImporteTotal = Convert.ToDouble(dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasImporteTotal"].Value.ToString());


                                    DataTable dataTableDetraccion = new DataTable();
                                    dataTableDetraccion = detraccion.Show(ventasCodigo);
                                    if (dataTableDetraccion.Rows.Count > 0)
                                    {
                                        double detraccionProcentaje = Convert.ToDouble(dataTableDetraccion.Rows[0]["porcentaje"].ToString());
                                        double detraccionMonto = Convert.ToDouble(dataTableDetraccion.Rows[0]["monto"].ToString());

                                        if (ventasImporteTotal > detraccionMonto)
                                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasReferencia"].Value = Math.Round(ventasImporteTotal * detraccionProcentaje, 2);
                                        else
                                            dgvRegistroVentas.Rows[e.RowIndex].Cells["ventasReferencia"].Value = "";
                                    }
                                    else
                                        MessageBox.Show("Ingrese un número entero (1 - 5)");
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
            }
        }

        private void Destroy()
        {
            int id = Convert.ToInt32(dgvRegistroCompras.CurrentRow.Cells["comprasID"].Value);
            DialogResult DialogResultDestroy = MessageBox.Show("¿Realmente quieres eliminar está compra?", "Compras .::. Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResultDestroy == DialogResult.Yes)
            {
                if (compras.Destroy(id))
                {
                    FillDataGridViewCompras();
                }
                else
                    MessageBox.Show("Compra NO Eliminada", "Compras .::. Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvCompras_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["comprasID"].Value = GenerateID();
            e.Row.DefaultCellStyle.Font = new Font(dgvRegistroCompras.Font, FontStyle.Italic);

            e.Row.Cells["comprasFechaEmision"].Value = DateTime.UtcNow.ToString("dd/MM/yyyy");

            e.Row.Cells["comprasProveedorTipo"].Value = "06";
        }
        private void dgvRegistroVentas_DefaultValueNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["ventasId"].Value = GenerateID();
            e.Row.DefaultCellStyle.Font = new Font(dgvRegistroVentas.Font, FontStyle.Italic);

            e.Row.Cells["ventasFechaEmision"].Value = DateTime.UtcNow.ToString("dd/MM/yyyy");
            e.Row.Cells["ventasProveedorTipo"].Value = "06";
        }


        private int GenerateID()
        {
            Random random = new Random();
            int numero = random.Next(-1000, -1);
            return numero;
        }

        private void DestroyVentas()
        {
            int id = Convert.ToInt32(dgvRegistroVentas.CurrentRow.Cells["ventasId"].Value);
            DialogResult dialogResultVentas = MessageBox.Show("¿Realmente quieres eliminar la venta?", "Ventas .::. Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResultVentas == DialogResult.Yes)
            {
                if (ventas.Destroy(id))
                {
                    FillDataGridViewVentas();
                }
                else
                    MessageBox.Show("Venta no eliminada", "Ventas .::. Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGenerarTXT_Click(object sender, EventArgs e)
        {
            string pleNombreRuc = txtNombreRuc.Text;
            string pleNombreAnio = txtNombreAnio.Text;
            string pleNombreMes = txtNombreMes.Text;

            if (pleNombreRuc != "" && pleNombreAnio != "" && pleNombreMes != "")
            {
                GenerateComprasTXT();
                GenerateCompras82TXT();
                GenerateVentasTXT();
                //MessageBox.Show("Archivos txt Crerados correctamente");
            }
            else MessageBox.Show("Los compos RUC, Año y Mes son Obligatorios", "PLE .::. Generación de TXTs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateComprasTXT()
        {
            DataTable dataTable = new DataTable();
            dataTable = compras.GetForTXT(Convert.ToInt32(txtNombreAnio.Text), Convert.ToInt32(txtNombreMes.Text), txtNombreRuc.Text, idUsuario);
            if (dataTable.Rows.Count > 0)
            {

                string filename;

                filename = nameTXT() + "00080100001111.txt";

                if (File.Exists(filename))
                {
                    DialogResult dialogResult = MessageBox.Show("Ya existe un erchivo COMPRAS 8.1 con el mismo nombre \n¿Quieres Reemplazarlo?", "Archivos txt .::. Info", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CreateComprasTXT(filename, dataTable);
                    }
                }
                else
                    CreateComprasTXT(filename, dataTable);
            }
            else
                MessageBox.Show("No se encontrarón datos para generar el TXT");
        }

        private void GenerateVentasTXT()
        {
            DataTable dataTable = new DataTable();
            dataTable = ventas.GetForTXT(Convert.ToInt32(txtNombreAnio.Text), Convert.ToInt32(txtNombreMes.Text), txtNombreRuc.Text, idUsuario);
            if (dataTable.Rows.Count > 0)
            {
                string filename;

                filename = nameTXT() + "00140100001111.txt";

                if (File.Exists(filename))
                {
                    DialogResult dialogResult = MessageBox.Show("Ya existe un erchivo VENTAS con el mismo nombre  \n¿Quieres Reemplazarlo?", "Archivos txt .::. Info", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CreateVentasTXT(filename, dataTable);
                    }
                }
                else
                    CreateVentasTXT(filename, dataTable);
            }
            else
                MessageBox.Show("No se encontrarón datos para generar el TXT");
        }

        private void GenerateCompras82TXT()
        {
            string filename;

            filename = nameTXT() + "00080200001011.txt";

            if (File.Exists(filename))
            {
                DialogResult dialogResult = MessageBox.Show("Ya existe un erchivo COMPRAS 8.2 con el mismo nombre \n¿Quieres Reemplazarlo?", "Archivos txt .::. Info", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CreateCompras82TXT(filename);
                }
            }
            else
                CreateCompras82TXT(filename);
        }

        private string nameTXT()
        {
            try
            {
                string filename = null;
                string fileRoute = txtRutaTXT.Text;
                string pleLibroCompras = "LE";
                string pleRuc = txtNombreRuc.Text;
                string pleAnio = txtNombreAnio.Text;
                string pleMes = txtNombreMes.Text.PadLeft(2, '0');

                filename = fileRoute + pleLibroCompras + pleRuc + pleAnio + pleMes;

                if (String.IsNullOrEmpty(txtRutaTXT.Text)) MessageBox.Show("Por favor seleccione una carpeta");
                else
                {
                    if (!Directory.Exists(fileRoute)) Directory.CreateDirectory(fileRoute);
                }
                return filename;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("No se pudo acceder a la carpeta o ruta: " + Ex);
                return null;
            }
        }

        private void CreateComprasTXT(string filename, DataTable dataTable)
        {
            try
            {
                StreamWriter fichero;
                fichero = File.CreateText(filename);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string txtPlePeriodo = dataTable.Rows[i]["Periodo"].ToString(); //Campo 01
                    string txtPleRegimen1 = dataTable.Rows[i]["Regimen1"].ToString(); //Campo 02
                    string txtPleRegimen2 = dataTable.Rows[i]["Regimen2"].ToString().PadLeft(3, '0'); //Campo 03
                    string txtPleFechaEmision = dataTable.Rows[i]["FechaEmision"].ToString(); //Campo 04
                    string txtPleFechaPago = dataTable.Rows[i]["FechaPago"].ToString(); //Campo 05
                    string txtPleComprobanteTipo = dataTable.Rows[i]["ComprobanteTipo"].ToString(); //Campo 06
                    string txtPleComprobanteSerie = dataTable.Rows[i]["ComprobanteSerie"].ToString(); //Campo 07
                    string txtPleAnioEmisionAduana = dataTable.Rows[i]["AnioEmisionAduana"].ToString(); //Campo 08
                    string txtPleComprobanteNumero = dataTable.Rows[i]["ComprobanteNumero"].ToString(); //Campo 09
                    string txtPleCampo10 = dataTable.Rows[i]["Campo10"].ToString(); //Campo 10
                    int txtPleProveedorTipo = Convert.ToInt32(dataTable.Rows[i]["ProveedorTipo"].ToString()); //Campo 11
                    string txtPleProveedorNumero = dataTable.Rows[i]["ProveedorNumero"].ToString(); //Campo 12
                    string txtPleProveedorNombre = dataTable.Rows[i]["ProveedorNombre"].ToString(); //Campo 13
                    string txtPleBaseImponible = dataTable.Rows[i]["BaseImponible"].ToString(); //Campo 14
                    string txtPleIGV = dataTable.Rows[i]["IGV"].ToString(); //Campo 15
                    string txtPleBaseImponible2 = dataTable.Rows[i]["BaseImponible2"].ToString(); //Campo 16
                    string txtPleIGV2 = dataTable.Rows[i]["IGV2"].ToString(); //Campo 17
                    string txtPleBaseImponible3 = dataTable.Rows[i]["BaseImponible3"].ToString(); //Campo 18
                    string txtPleIGV3 = dataTable.Rows[i]["IGV3"].ToString(); //Campo 19
                    string txtPleNoGravada = dataTable.Rows[i]["NoGravada"].ToString(); //Campo 20
                    string txtPleImpuestoImponible = dataTable.Rows[i]["ImpuestoImponible"].ToString(); //Campo 21
                    string txtPleOtrosMontos = dataTable.Rows[i]["OtrosMontos"].ToString(); //Campo 22
                    string txtPleImporteTotal = dataTable.Rows[i]["ImporteTotal"].ToString(); //Campo 23
                    string txtPleCodigoMoneda = dataTable.Rows[i]["CodigoMoneda"].ToString(); //Campo 24
                    string txtPleTipoCambio = dataTable.Rows[i]["TipoCambio"].ToString(); //Campo 25
                    string txtPleConstanciaFechaPago = dataTable.Rows[i]["ConstanciaFechaPago"].ToString(); //Campo 26
                    string txtPleComprobanteTipoModifica = dataTable.Rows[i]["ComprobanteTipoModifica"].ToString(); //Campo 27
                    string txtPleComprobanteSerieModifica = dataTable.Rows[i]["ComprobanteSerieModifica"].ToString(); //Campo 28
                    string txtPleCodigoDependenciaAduanera = dataTable.Rows[i]["CodigoDependenciaAduanera"].ToString(); //Campo 29
                    string txtPleComprobanteNumeroModifica = dataTable.Rows[i]["ComprobanteNumeroModifica"].ToString(); //Campo 30
                    string txtPleFechaConstanciaDetraccion = dataTable.Rows[i]["FechaConstanciaDetraccion"].ToString(); //Campo 31
                    string txtPleConstanciaNumero = dataTable.Rows[i]["ConstanciaNumero"].ToString(); //Campo 32
                    string txtPleMancaDetraccion = dataTable.Rows[i]["MancaDetraccion"].ToString(); //Campo 33
                    string txtPleCalsificacionBienes = dataTable.Rows[i]["CalsificacionBienes"].ToString(); //Campo 34
                    string txtPleIdentificacionContrato = dataTable.Rows[i]["IdentificacionContrato"].ToString(); //Campo 35
                    string txtPleErrorTipo1 = dataTable.Rows[i]["ErrorTipo1"].ToString(); //Campo 36
                    string txtPleErrorTipo2 = dataTable.Rows[i]["ErrorTipo2"].ToString(); //Campo 37
                    string txtPleErrorTipo3 = dataTable.Rows[i]["ErrorTipo3"].ToString(); //Campo 38
                    string txtPleErrorTipo4 = dataTable.Rows[i]["ErrorTipo4"].ToString(); //Campo 39
                    string txtPleIdentificadorComprobante = dataTable.Rows[i]["IdentificadorComprobante"].ToString(); //Campo 40
                    string txtPleEstadoIdentifica = dataTable.Rows[i]["EstadoIdentifica"].ToString(); //Campo 41
                    string txtPleCampoLibre = dataTable.Rows[i]["CampoLibre"].ToString(); //Campo 42

                    //if (txtPleCampoLibre != "") txtPleCampoLibre = "|" + txtPleCampoLibre;

                    fichero.WriteLine(
                        txtPlePeriodo + "|" +
                        txtPleRegimen1 + "|" +
                        "M" + txtPleRegimen2 + "|" +
                        txtPleFechaEmision + "|" +
                        txtPleFechaPago + "|" +
                        txtPleComprobanteTipo + "|" +
                        txtPleComprobanteSerie + "|" +
                        txtPleAnioEmisionAduana + "|" +
                        txtPleComprobanteNumero + "|" +
                        txtPleCampo10 + "|" +
                        txtPleProveedorTipo + "|" +
                        txtPleProveedorNumero + "|" +
                        txtPleProveedorNombre + "|" +
                        txtPleBaseImponible + "|" +
                        txtPleIGV + "|" +
                        txtPleBaseImponible2 + "|" +
                        txtPleIGV2 + "|" +
                        txtPleBaseImponible3 + "|" +
                        txtPleIGV3 + "|" +
                        txtPleNoGravada + "|" +
                        txtPleImpuestoImponible + "|" +
                        txtPleOtrosMontos + "|" +
                        txtPleImporteTotal + "|" +
                        txtPleCodigoMoneda + "|" +
                        txtPleTipoCambio + "|" +
                        txtPleConstanciaFechaPago + "|" +
                        txtPleComprobanteTipoModifica + "|" +
                        txtPleComprobanteSerieModifica + "|" +
                        txtPleCodigoDependenciaAduanera + "|" +
                        txtPleComprobanteNumeroModifica + "|" +
                        txtPleFechaConstanciaDetraccion + "|" +
                        txtPleConstanciaNumero + "|" +
                        txtPleMancaDetraccion + "|" +
                        txtPleCalsificacionBienes + "|" +
                        txtPleIdentificacionContrato + "|" +
                        txtPleErrorTipo1 + "|" +
                        txtPleErrorTipo2 + "|" +
                        txtPleErrorTipo3 + "|" +
                        txtPleErrorTipo4 + "|" +
                        txtPleIdentificadorComprobante + "|" +
                        txtPleEstadoIdentifica + "|" +
                        txtPleCampoLibre + "|"
                        );
                }
                fichero.Close();
                MessageBox.Show("Archivo txt (Compras 8.1) generado");
            }
            catch (Exception) { }
        }

        private void CreateVentasTXT(string filename, DataTable dataTable)
        {
            try
            {
                StreamWriter fichero;
                fichero = File.CreateText(filename);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string txtPlePeriodo = dataTable.Rows[i]["Periodo"].ToString(); //Campo 01
                    string txtPleRegimen1 = dataTable.Rows[i]["Regimen1"].ToString(); //Campo 02
                    string txtPleRegimen2 = dataTable.Rows[i]["Regimen2"].ToString().PadLeft(3, '0'); //Campo 03
                    string txtPleFechaEmision = dataTable.Rows[i]["FechaEmision"].ToString(); //Campo 04
                    string txtPleFechaPago = dataTable.Rows[i]["FechaPago"].ToString(); //Campo 05
                    string txtPleComprobanteTipo = dataTable.Rows[i]["ComprobanteTipo"].ToString(); //Campo 06
                    string txtPleComprobanteSerie = dataTable.Rows[i]["ComprobanteSerie"].ToString(); //Campo 07
                    string ComprobanteNumero = dataTable.Rows[i]["ComprobanteNumero"].ToString(); //Campo 08
                    string txtPleRegistroTicket = dataTable.Rows[i]["RegistroTicket"].ToString(); //Campo 09
                    string txtPleProveedorTipo = dataTable.Rows[i]["ProveedorTipo"].ToString(); //Campo 10
                    string txtPleProveedorNumero = dataTable.Rows[i]["ProveedorNumero"].ToString(); //Campo 11
                    string txtPleProveedorNombre = dataTable.Rows[i]["ProveedorNombre"].ToString(); //Campo 12
                    string txtPleValorExportacion = dataTable.Rows[i]["ValorExportacion"].ToString(); //Campo 13
                    string txtPleBaseImponible = dataTable.Rows[i]["BaseImponible"].ToString(); //Campo 14
                    string DocumentoBaseImponible = dataTable.Rows[i]["DocumentoBaseImponible"].ToString(); //Campo 15
                    string txtPleIGV = dataTable.Rows[i]["IGV"].ToString(); //Campo 16
                    string txtPleDescuentoIGV = dataTable.Rows[i]["DescuentoIGV"].ToString(); //Campo 17
                    string txtPleImporteTotalExonerada = dataTable.Rows[i]["ImporteTotalExonerada"].ToString(); //Campo 18
                    string txtPleImporteTotalInafecta = dataTable.Rows[i]["ImporteTotalInafecta"].ToString(); //Campo 19
                    string txtPleISC = dataTable.Rows[i]["ISC"].ToString(); //Campo 20
                    string txtPleBaseImponibleConIGVArroz = dataTable.Rows[i]["BaseImponibleConIGVArroz"].ToString(); //Campo 21
                    string txtPleIVArrozPilado = dataTable.Rows[i]["IVArrozPilado"].ToString(); //Campo 22
                    string txtPleOtrosMontos = dataTable.Rows[i]["OtrosMontos"].ToString(); //Campo 23
                    string txtPleImporteTotal = dataTable.Rows[i]["ImporteTotal"].ToString(); //Campo 24
                    string txtPleCodigoMoneda = dataTable.Rows[i]["CodigoMoneda"].ToString(); //Campo 25
                    string txtPleTipoCambio = dataTable.Rows[i]["TipoCambio"].ToString(); //Campo 26
                    if (txtPleCodigoMoneda == "") txtPleTipoCambio = "";
                    string txtPleReferenciaFecha = dataTable.Rows[i]["ReferenciaFecha"].ToString(); //Campo 27
                    string ComprobanteTipoModifica = dataTable.Rows[i]["ComprobanteTipoModifica"].ToString(); //Campo 28
                    string ComprobanteSerieModifica = dataTable.Rows[i]["ComprobanteSerieModifica"].ToString(); //Campo 29
                    string txtPleComprobanteNumeroModifica = dataTable.Rows[i]["ComprobanteNumeroModifica"].ToString(); //Campo 30
                    string txtPleIdentificacionContrato = dataTable.Rows[i]["IdentificacionContrato"].ToString(); //Campo 31
                    string txtPleErrorTipo1 = dataTable.Rows[i]["ErrorTipo1"].ToString(); //Campo 32
                    string txtPleIdentificadorComprobante = dataTable.Rows[i]["IdentificadorComprobante"].ToString(); //Campo 33
                    string txtPleEstadoIdentifica = dataTable.Rows[i]["EstadoIdentifica"].ToString(); //Campo 34
                    string txtPleCampoLibre = dataTable.Rows[i]["CampoLibre"].ToString(); //Campo 35

                    //if (txtPleCampoLibre != "") txtPleCampoLibre = "|" + txtPleCampoLibre;

                    fichero.WriteLine(
                        txtPlePeriodo + "|" +
                        txtPleRegimen1 + "|" +
                        "M" + txtPleRegimen2 + "|" +
                        txtPleFechaEmision + "|" +
                        txtPleFechaPago + "|" +
                        txtPleComprobanteTipo + "|" +
                        txtPleComprobanteSerie + "|" +
                        ComprobanteNumero + "|" +
                        txtPleRegistroTicket + "|" +
                        txtPleProveedorTipo + "|" +
                        txtPleProveedorNumero + "|" +
                        txtPleProveedorNombre + "|" +
                        txtPleValorExportacion + "|" +
                        txtPleBaseImponible + "|" +
                        DocumentoBaseImponible + "|" +
                        txtPleIGV + "|" +
                        txtPleDescuentoIGV + "|" +
                        txtPleImporteTotalExonerada + "|" +
                        txtPleImporteTotalInafecta + "|" +
                        txtPleISC + "|" +
                        txtPleBaseImponibleConIGVArroz + "|" +
                        txtPleIVArrozPilado + "|" +
                        txtPleOtrosMontos + "|" +
                        txtPleImporteTotal + "|" +
                        txtPleCodigoMoneda + "|" +
                        txtPleTipoCambio + "|" +
                        txtPleReferenciaFecha + "|" +
                        ComprobanteTipoModifica + "|" +
                        ComprobanteSerieModifica + "|" +
                        txtPleComprobanteNumeroModifica + "|" +
                        txtPleIdentificacionContrato + "|" +
                        txtPleErrorTipo1 + "|" +
                        txtPleIdentificadorComprobante + "|" +
                        txtPleEstadoIdentifica + "|" +
                        txtPleCampoLibre
                        );
                }
                fichero.Close();
                MessageBox.Show("Archivo txt (Ventas 14.1) generado");
            }
            catch (Exception) { }
        }

        private void CreateCompras82TXT(string filename)
        {
            try
            {
                StreamWriter fichero;
                fichero = File.CreateText(filename);
                //fichero.WriteLine();
                fichero.Close();
            }
            catch (Exception) { }
        }

        private void btnCargarCarpeta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                TXTRoute = fbd.SelectedPath + "\\" + txtNombreRuc.Text + "\\" + txtNombreAnio.Text + "\\" + txtNombreMes.Text.PadLeft(2, '0') + "\\";
                txtRutaTXT.Text = TXTRoute;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNombreMes.Text != "" && txtNombreAnio.Text != "")
            {
                GetRoute();
                FillDataGridViewCompras(true);
                FillDataGridViewVentas(true);
                FillPDT();
            }
            else
                MessageBox.Show("Ingrese un Año y un Mes para realizar el filtrado");
        }

        private void sgvRegistroCompras_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSourceCompras.Sort = this.dgvRegistroCompras.SortString;
            this.BSComprasBindingSource.Sort = this.dgvRegistroCompras.SortString;
            //this.spallcurrentmonthcomprasBindingSource.Sort = this.dgvRegistroCompras.FilterString;

            //dgvRegistroCompras.Update();
        }

        private void sgvRegistroCompras_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSourceCompras.Filter = this.dgvRegistroCompras.SortString;
            this.BSComprasBindingSource.Filter = this.dgvRegistroCompras.FilterString;
            //this.spallcurrentmonthcomprasBindingSource.Filter = this.dgvRegistroCompras.FilterString;
            //dgvRegistroCompras.Update();
        }

        private void removeColumnsCompras()
        {
            dgvRegistroCompras.Columns.Remove("dataGridViewTextBoxColumn1");
            dgvRegistroCompras.Columns.Remove("dataGridViewTextBoxColumn2");
            dgvRegistroCompras.Columns.Remove("dataGridViewTextBoxColumn3");
            dgvRegistroCompras.Columns.Remove("dataGridViewTextBoxColumn4");
            dgvRegistroCompras.Columns.Remove("FechaPagoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CTipoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CSerieDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CNDocumentoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PTipoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PNumeroDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PDocumentoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PNombreRazonSocialDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CuentaDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("DescripcionDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("BaseImponibleDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("IGVDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("NoGravadaDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("DescuentosDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ImporteTotalDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("DolaresDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("TipoCambioDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PercepcionDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("DestinoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("DescripcionDestinoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("PgoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CuentaDestinoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("CodigoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ConstanciaNumeroDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ConstanciaFechaPagoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ConstanciaMontoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ConstanciaReferenciaDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("BancarizacionFechaDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("BancarizacionBcoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("BancarizacionOperacionDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ReferenciaFechaDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ReferenciaTipoDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ReferenciaSerieDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ReferenciaNumeroDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("UsuarioDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("FechaRegistroDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("FechaModificacionDataGridViewTextBoxColumn");
            dgvRegistroCompras.Columns.Remove("ConversionDolarDataGridViewTextBoxColumn");
        }

        private void removeColumnsVentas()
        {
            dgvRegistroVentas.Columns.Remove("idLibroVentasDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("dataGridViewTextBoxColumn5");
            dgvRegistroVentas.Columns.Remove("dataGridViewTextBoxColumn6");
            dgvRegistroVentas.Columns.Remove("dataGridViewTextBoxColumn7");
            dgvRegistroVentas.Columns.Remove("fechaPagoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("cTipoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("cSerieDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("cNDocumentoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("pTipoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("pNumeroDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("pNombreRazonSocialDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("cuentaDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("descripcionDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("valorExportacionDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("baseImponibleDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("importeTotalExoneradaDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("importeTotalInafectaDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("iGVDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("importeTotalDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("tCDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("dolaresDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("igvRetencionDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("cuentaDestinoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("pagoDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("referenciaFechaDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("referenciaTipoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("referenciaSerieDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("referenciaNumeroDocumentoDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("codigoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("constanciaNumeroDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("constanciaFechaPagoDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("detraccionSolesDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("referenciaDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("observacionDataGridViewTextBoxColumn");
            dgvRegistroVentas.Columns.Remove("usuarioDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("fechaRegistroDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("fechaModificacionDataGridViewTextBoxColumn1");
            dgvRegistroVentas.Columns.Remove("cuentaDestinoDescripcionDataGridViewTextBoxColumn");
        }

        private void RemoveColumnPDT()
        {

            DgvPDT.Columns.Remove("ingresoExportaciónDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoGravadasDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoExoneradaDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoInafectaDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoIGVDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoImporteTotalDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("egresoMesDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("egresoBaseImponibleDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("egresoIGVDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("egresoNoGravadaDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("egresoImporteTotalDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ficalIgvImpouestoResultanteDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ficalIgvCreditoDebitoDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ficalIgvSaldoFavorPagarDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("exportadorSFMBDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("percepcionesIgvDelMesDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("percepcionesIgvMesAnteriorDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("percepcionesIgvAplicadaDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("percepcionesIgvComposicionProcedenteDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("percepcionesIgvPorAplicarDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("retencionesIgvDelMesDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("retencionesIgvMesAnteriorDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("retencionesIgvAplicadaDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("retencionesIgvComposicionProcedenteDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("retencionesIgvPorAplicarDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("igvPagoAPagarDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("igvPagoPagadoDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaOtrosIngresoDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaBaseImponibleDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaCoeficienteDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaImpuestoResultanteDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaPagadoDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaCompensacionSFADataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaCompensacionSFMBDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaCompensacionITANDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaCompensacionPercepcionDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaImputacionDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("impuestoAlaRentaPorPagarDataGridViewTextBoxColumn");
            DgvPDT.Columns.Remove("ingresoExportacionDataGridViewTextBoxColumn");
            //DgvPDT.Columns.Remove("dataGridViewTextBoxColumn8");
        }
        private void fillCurrentMonthToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.TAComprasTableAdapter.FillCurrentMonth(this.dSCompras.tblRegistroCompras, txtNombreRuc.Text, idUsuario);
            }
            catch (Exception) { }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FillDataGridViewCompras();
            FillDataGridViewVentas();

            txtNombreAnio.Text = DateTime.UtcNow.ToString("yyyy");
            txtNombreMes.Text = DateTime.UtcNow.ToString("MM");

            GetRoute();
            FillPDT(true);
        }

        private void dgvRegistroCompras_KeyDown(object sender, KeyEventArgs e)
        {
            ZoomCompras(e);
            ZoomVentas(e);
        }

        private void ZoomCompras(KeyEventArgs e)
        {
            float fontSize = dgvRegistroCompras.DefaultCellStyle.Font.Size;
            float fontHeaderSize = dgvRegistroCompras.ColumnHeadersDefaultCellStyle.Font.Size;

            if (e.KeyCode == Keys.Add)//Control con '+'
            {
                dgvRegistroCompras.DefaultCellStyle.Font = new Font("Tahoma", fontSize + 1, FontStyle.Regular);
                dgvRegistroCompras.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", fontHeaderSize + 1, FontStyle.Bold);
                foreach (DataGridViewColumn column in dgvRegistroCompras.Columns)
                {
                    column.Width += 3;
                }

                foreach (DataGridViewRow row in dgvRegistroCompras.Rows)
                {
                    row.Height += 1;
                }
            }
            else if (e.KeyCode == Keys.Subtract)//Control con '-'
            {
                dgvRegistroCompras.DefaultCellStyle.Font = new Font("Tahoma", fontSize - 1, FontStyle.Regular);
                dgvRegistroCompras.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", fontHeaderSize - 1, FontStyle.Bold);
                foreach (DataGridViewColumn column in dgvRegistroCompras.Columns)
                {
                    column.Width -= 3;
                }

                foreach (DataGridViewRow row in dgvRegistroCompras.Rows)
                {
                    row.Height -= 1;
                }
            }
        }

        private void ZoomVentas(KeyEventArgs e)
        {
            float fontSize = dgvRegistroVentas.DefaultCellStyle.Font.Size;
            float fontHeaderSize = dgvRegistroVentas.ColumnHeadersDefaultCellStyle.Font.Size;

            if (e.KeyCode == Keys.Add)//Control con '+'
            {
                dgvRegistroVentas.DefaultCellStyle.Font = new Font("Tahoma", fontSize + 1, FontStyle.Regular);
                dgvRegistroVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", fontHeaderSize + 1, FontStyle.Bold);
                foreach (DataGridViewColumn column in dgvRegistroVentas.Columns)
                {
                    column.Width += 3;
                }

                foreach (DataGridViewRow row in dgvRegistroVentas.Rows)
                {
                    row.Height += 1;
                }
            }
            else if (e.KeyCode == Keys.Subtract)//Control con '-'
            {
                dgvRegistroVentas.DefaultCellStyle.Font = new Font("Tahoma", fontSize - 1, FontStyle.Regular);
                dgvRegistroVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", fontHeaderSize - 1, FontStyle.Bold);
                foreach (DataGridViewColumn column in dgvRegistroVentas.Columns)
                {
                    column.Width -= 3;
                }

                foreach (DataGridViewRow row in dgvRegistroVentas.Rows)
                {
                    row.Height -= 1;
                }
            }
        }

        private void dgvRegistroCompras_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvRegistroCompras.CurrentCell.ColumnIndex;

            if (dgvRegistroCompras.Columns[columnIndex].Name == "comprasCdpSerie" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasConstanciaNumero" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "BancarizacionBco" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasReferenciaSerie" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasObservacion")
            {
                DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

                dText.KeyPress -= new KeyPressEventHandler(KepPressCompras);
                dText.KeyPress += new KeyPressEventHandler(KepPressCompras);
            }
        }

        void KepPressCompras(object sender, KeyPressEventArgs e)
        {
            int columnIndex = dgvRegistroCompras.CurrentCell.ColumnIndex;

            if (
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasCdpSerie" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasConstanciaNumero" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "BancarizacionBco" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasReferenciaSerie" ||
                dgvRegistroCompras.Columns[columnIndex].Name == "comprasObservacion"
                )
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void dgvRegistroVentas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvRegistroVentas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvRegistroVentas.CurrentCell.ColumnIndex;

            if (dgvRegistroVentas.Columns[columnIndex].Name == "ventasCdpSerie" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasConstanciaNumero" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasReferenciaSerie" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasObservacion"
                )
            {
                DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

                dText.KeyPress -= new KeyPressEventHandler(KepPressVentas);
                dText.KeyPress += new KeyPressEventHandler(KepPressVentas);
            }
        }

        void KepPressVentas(object sender, KeyPressEventArgs e)
        {
            int columnIndex = dgvRegistroVentas.CurrentCell.ColumnIndex;

            if (
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasCdpSerie" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasConstanciaNumero" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasReferenciaSerie" ||
                dgvRegistroVentas.Columns[columnIndex].Name == "ventasObservacion"
                )
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void dgvRegistroCompras_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #region PDT
        //private void SumPDT()
        //{
        //    //int sum = 0;
        //    //for (int i = 0; i < DgvPDT.Rows.Count - 3; ++i)
        //    //{
        //    //    sum += Convert.ToInt32(DgvPDT.Rows[i].Cells[2].Value);
        //    //}
        //    //this.TApdt.Fill(dSPdt.sp_pdt, txtNombreRuc.Text, Convert.ToInt32(txtNombreAnio.Text), idUsuario);
        //    DataTable table = dSPdt.Tables["sp_pdt"];

        //    object sumObject;
        //    sumObject = table.Compute("Sum(ingresoGravadas)", string.Empty);

        //    //DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells[2].Value = sumObject.ToString();
        //    lblSumGravadas.Text = sumObject.ToString();
        //}


        private void FillPDT(bool refresh = false)
        {

            if (pdt.Show(txtNombreRuc.Text, Convert.ToInt32(txtNombreAnio.Text), idUsuario).Rows.Count > 0)
            {
                this.TApdt.FillByRucAnioMesUsuario(dSPdt.sp_pdt, txtNombreRuc.Text, Convert.ToInt32(txtNombreAnio.Text), idUsuario);
                if (refresh) this.TApdt.Fill(dSPdt.sp_pdt, txtNombreRuc.Text, Convert.ToInt32(txtNombreAnio.Text), idUsuario);
            }
            else this.TApdt.Fill(dSPdt.sp_pdt, txtNombreRuc.Text, Convert.ToInt32(txtNombreAnio.Text), idUsuario);
        }

        private void SavePDT()
        {
            string ruc = txtNombreRuc.Text;
            int anio = Convert.ToInt32(txtNombreAnio.Text);

            foreach (DataGridViewRow row in DgvPDT.Rows)
            {
                if (row.Cells["PdMes"].Value.GetType() != typeof(DBNull) | Convert.ToInt32(row.Cells["PdMes"].Value) > 0)
                {
                    int PdtID = row.Cells["PdtID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["PdtID"].Value) : 0;
                    int PdtMes = row.Cells["PdMes"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["PdMes"].Value) : 0;
                    double PdtIngresoExportacion = row.Cells["PdtIngresoExportacion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoExportacion"].Value) : 0;
                    double PdtIngresoGravadas = row.Cells["PdtIngresoGravadas"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoGravadas"].Value) : 0;
                    double PdtIngresoExonerada = row.Cells["PdtIngresoExonerada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoExonerada"].Value) : 0;
                    double PdtIngresoInafecta = row.Cells["PdtIngresoInafecta"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoInafecta"].Value) : 0;
                    double PdtIngresoIGV = row.Cells["PdtIngresoIGV"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoIGV"].Value) : 0;
                    double PdtIngresoImporteTotal = row.Cells["PdtIngresoImporteTotal"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIngresoImporteTotal"].Value) : 0;
                    double PdtEgresoBaseImponible = row.Cells["PdtEgresoBaseImponible"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtEgresoBaseImponible"].Value) : 0;
                    double PdtEgresoIGV = row.Cells["PdtEgresoIGV"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtEgresoIGV"].Value) : 0;
                    double PdtEgresoNoGravada = row.Cells["PdtEgresoNoGravada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtEgresoNoGravada"].Value) : 0;
                    double PdtEgresoImporteTotal = row.Cells["PdtEgresoImporteTotal"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtEgresoImporteTotal"].Value) : 0;
                    double PdtFicalIgvImpouestoResultante = row.Cells["PdtFicalIgvImpouestoResultante"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtFicalIgvImpouestoResultante"].Value) : 0;
                    double PdtFicalIgvCreditoDebito = row.Cells["PdtFicalIgvCreditoDebito"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtFicalIgvCreditoDebito"].Value) : 0;
                    double PdtFicalIgvSaldoFavorPagar = row.Cells["PdtFicalIgvSaldoFavorPagar"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtFicalIgvSaldoFavorPagar"].Value) : 0;
                    double PdtExportadorSFMB = row.Cells["PdtExportadorSFMB"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtExportadorSFMB"].Value) : 0;
                    double PdtPercepcionesIgvDelMes = row.Cells["PdtPercepcionesIgvDelMes"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtPercepcionesIgvDelMes"].Value) : 0;
                    double PdtPercepcionesIgvMesAnterior = row.Cells["PdtPercepcionesIgvMesAnterior"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtPercepcionesIgvMesAnterior"].Value) : 0;
                    double PdtPercepcionesIgvAplicada = row.Cells["PdtPercepcionesIgvAplicada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtPercepcionesIgvAplicada"].Value) : 0;
                    double PdtPercepcionesIgvComposicionProcedente = row.Cells["PdtPercepcionesIgvComposicionProcedente"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtPercepcionesIgvComposicionProcedente"].Value) : 0;
                    double PdtPercepcionesIgvPorAplicar = row.Cells["PdtPercepcionesIgvPorAplicar"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtPercepcionesIgvPorAplicar"].Value) : 0;
                    double PdtRetencionesIgvDelMes = row.Cells["PdtRetencionesIgvDelMes"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtRetencionesIgvDelMes"].Value) : 0;
                    double PdtRetencionesIgvMesAnterior = row.Cells["PdtRetencionesIgvDelMesAnterior"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtRetencionesIgvDelMesAnterior"].Value) : 0;
                    double PdtRetencionesIgvAplicada = row.Cells["PdtRetencionesIgvAplicada"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtRetencionesIgvAplicada"].Value) : 0;
                    double PdtRetencionesIgvComposicionProcedente = row.Cells["PdtRetencionesIgvComposicionProcedente"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtRetencionesIgvComposicionProcedente"].Value) : 0;
                    double PdtRetencionesIgvPorAplicar = row.Cells["PdtRetencionesIgvPorAplicar"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtRetencionesIgvPorAplicar"].Value) : 0;
                    double PdtIgvPagoAPagar = row.Cells["PdtIgvPagoAPagar"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIgvPagoAPagar"].Value) : 0;
                    double PdtIgvPagoPagado = row.Cells["PdtIgvPagoPagado"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtIgvPagoPagado"].Value) : 0;
                    double PdtImpuestoAlaRentaOtrosIngreso = row.Cells["PdtImpuestoAlaRentaOtrosIngreso"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaOtrosIngreso"].Value) : 0;
                    double PdtImpuestoAlaRentaBaseImponible = row.Cells["PdtImpuestoAlaRentaBaseImponible"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaBaseImponible"].Value) : 0;
                    double PdtImpuestoAlaRentaCoeficiente = row.Cells["PdtImpuestoAlaRentaCoeficiente"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaCoeficiente"].Value) : 0;
                    double PdtImpuestoAlaRentaImpuestoResultante = row.Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value) : 0;
                    double PdtImpuestoAlaRentaPagado = row.Cells["PdtImpuestoAlaRentaPagado"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaPagado"].Value) : 0;
                    double PdtImpuestoAlaRentaCompensacionSFA = row.Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value) : 0;
                    double PdtImpuestoAlaRentaCompensacionSFMB = row.Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value) : 0;
                    double PdtImpuestoAlaRentaCompensacionITAN = row.Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value) : 0;
                    double PdtImpuestoAlaRentaCompensacionPercepcion = row.Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value) : 0;
                    double PdtImpuestoAlaRentaImputacion = row.Cells["PdtImpuestoAlaRentaImputacion"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaImputacion"].Value) : 0;
                    double PdtImpuestoAlaRentaPorPagar = row.Cells["PdtImpuestoAlaRentaPorPagar"].Value != DBNull.Value ? Convert.ToDouble(row.Cells["PdtImpuestoAlaRentaPorPagar"].Value) : 0;

                    if (PdtID < 1)
                    {
                        pdt.Insert(anio, PdtMes, ruc, PdtIngresoExportacion, PdtIngresoGravadas, PdtIngresoExonerada, PdtIngresoInafecta, PdtIngresoIGV, PdtIngresoImporteTotal, PdtEgresoBaseImponible,
                            PdtEgresoIGV, PdtEgresoNoGravada, PdtEgresoImporteTotal, PdtFicalIgvImpouestoResultante, PdtFicalIgvCreditoDebito, PdtFicalIgvSaldoFavorPagar, PdtExportadorSFMB,
                            PdtPercepcionesIgvDelMes, PdtPercepcionesIgvMesAnterior, PdtPercepcionesIgvAplicada, PdtPercepcionesIgvComposicionProcedente, PdtPercepcionesIgvPorAplicar,
                            PdtRetencionesIgvDelMes, PdtRetencionesIgvMesAnterior, PdtRetencionesIgvAplicada, PdtRetencionesIgvComposicionProcedente, PdtRetencionesIgvPorAplicar,
                            PdtIgvPagoAPagar, PdtIgvPagoPagado, PdtImpuestoAlaRentaOtrosIngreso, PdtImpuestoAlaRentaBaseImponible, PdtImpuestoAlaRentaCoeficiente, PdtImpuestoAlaRentaImpuestoResultante,
                            PdtImpuestoAlaRentaPagado, PdtImpuestoAlaRentaCompensacionSFA, PdtImpuestoAlaRentaCompensacionSFMB, PdtImpuestoAlaRentaCompensacionITAN, PdtImpuestoAlaRentaCompensacionPercepcion,
                            PdtImpuestoAlaRentaImputacion, PdtImpuestoAlaRentaPorPagar, idUsuario);
                    }
                    else
                    {
                        pdt.Update(PdtID, PdtIngresoExportacion, PdtIngresoGravadas, PdtIngresoExonerada, PdtIngresoInafecta, PdtIngresoIGV, PdtIngresoImporteTotal, PdtEgresoBaseImponible,
                            PdtEgresoIGV, PdtEgresoNoGravada, PdtEgresoImporteTotal, PdtFicalIgvImpouestoResultante, PdtFicalIgvCreditoDebito, PdtFicalIgvSaldoFavorPagar, PdtExportadorSFMB,
                            PdtPercepcionesIgvDelMes, PdtPercepcionesIgvMesAnterior, PdtPercepcionesIgvAplicada, PdtPercepcionesIgvComposicionProcedente, PdtPercepcionesIgvPorAplicar,
                            PdtRetencionesIgvDelMes, PdtRetencionesIgvMesAnterior, PdtRetencionesIgvAplicada, PdtRetencionesIgvComposicionProcedente, PdtRetencionesIgvPorAplicar,
                            PdtIgvPagoAPagar, PdtIgvPagoPagado, PdtImpuestoAlaRentaOtrosIngreso, PdtImpuestoAlaRentaBaseImponible, PdtImpuestoAlaRentaCoeficiente, PdtImpuestoAlaRentaImpuestoResultante,
                            PdtImpuestoAlaRentaPagado, PdtImpuestoAlaRentaCompensacionSFA, PdtImpuestoAlaRentaCompensacionSFMB, PdtImpuestoAlaRentaCompensacionITAN, PdtImpuestoAlaRentaCompensacionPercepcion,
                            PdtImpuestoAlaRentaImputacion, PdtImpuestoAlaRentaPorPagar);
                    }
                }
            }
            MessageBox.Show("PDT guardado correctamente", "SISCONT .::. PDT");
            FillPDT();
        }

        private void DgvPDT_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["PdtID"].Value = GenerateID();
        }

        private void DgvPDT_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void CalcPDT()
        {
            try
            {
                double ImpuesteResultante = 0, CreditoDebito = 0, SaldoFavor = 0;
                double PercepcionDelMes = 0, PercepcionDelMesAntorior = 0, PercepcionAPlicadas = 0, PercepcionComposicionPrecedente = 0, PercepcionAPlicadaResult = 0, PercepcionPorAplicarResult = 0;
                double RetencionDelMes = 0, RetencionDelMesAntorior = 0, RetencionAplicadas = 0, RetencionComposicionPrecedente = 0, RetencionPorAplicar = 0, RetencionAplicadaResult = 0, ExportadorSFMB = 0; ;
                double IgvPagoAPagar = 0;
                double ImpuestoAlaRentaImpuestoResultante = 0, ImpuestoAlaRentaPagado = 0, ImpuestoAlaRentaCompensacionSFA = 0, ImpuestoAlaRentaCompensacionITAN = 0, ImpuestoAlaRentaCompensacionPercepcion = 0, ImpuestoAlaRentaPorPagar = 0;
                for (int i = 0; i < DgvPDT.Rows.Count - 1; i++)
                {
                    //CREDITO/DEBITO FISCAL IGV		

                    if (DgvPDT.Rows[i].Cells["PdtFicalIgvImpouestoResultante"].Value.GetType() != typeof(System.DBNull))
                        ImpuesteResultante = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtFicalIgvImpouestoResultante"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtFicalIgvCreditoDebito"].Value.GetType() != typeof(System.DBNull))
                    {
                        CreditoDebito = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtFicalIgvCreditoDebito"].Value.ToString());
                        SaldoFavor = (double)Math.Round(ImpuesteResultante + (CreditoDebito), 2);

                    }
                    else SaldoFavor = (double)ImpuesteResultante;

                    DgvPDT.Rows[i].Cells["PdtFicalIgvSaldoFavorPagar"].Value = SaldoFavor;
                    DgvPDT.Rows[0].Cells["PdtFicalIgvCreditoDebito"].Style.BackColor = Color.AntiqueWhite;

                    //PERCEPCIONES IGV
                    if (DgvPDT.Rows[i].Cells["PdtPercepcionesIgvDelMes"].Value.GetType() != typeof(System.DBNull))
                        PercepcionDelMes = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtPercepcionesIgvDelMes"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtPercepcionesIgvMesAnterior"].Value.GetType() != typeof(System.DBNull))
                        PercepcionDelMesAntorior = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtPercepcionesIgvMesAnterior"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtPercepcionesIgvAplicada"].Value.GetType() != typeof(System.DBNull))
                        PercepcionAPlicadas = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtPercepcionesIgvAplicada"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtPercepcionesIgvComposicionProcedente"].Value.GetType() != typeof(System.DBNull))
                        PercepcionComposicionPrecedente = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtPercepcionesIgvComposicionProcedente"].Value.ToString());

                    if (SaldoFavor > 0)
                        PercepcionPorAplicarResult = SaldoFavor + PercepcionDelMesAntorior;

                    if (PercepcionPorAplicarResult > 0)
                    {
                        PercepcionPorAplicarResult = 0;
                    } else
                    {
                        if (SaldoFavor > 0)
                            PercepcionPorAplicarResult = SaldoFavor + PercepcionDelMes + PercepcionDelMesAntorior + PercepcionComposicionPrecedente;
                        else
                            PercepcionPorAplicarResult = PercepcionDelMes + PercepcionDelMesAntorior + PercepcionComposicionPrecedente;
                    }

                    PercepcionPorAplicarResult = Math.Round(PercepcionPorAplicarResult, 2);
                    DgvPDT.Rows[i].Cells["PdtPercepcionesIgvPorAplicar"].Value = PercepcionPorAplicarResult;


                    PercepcionAPlicadaResult = Math.Round(((PercepcionDelMes + PercepcionDelMesAntorior) - PercepcionPorAplicarResult) * (-1), 2);
                    DgvPDT.Rows[i].Cells["PdtPercepcionesIgvAplicada"].Value = PercepcionAPlicadaResult;

                    //RETENCIONES IGV
                    if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMes"].Value.GetType() != typeof(System.DBNull))
                        RetencionDelMes = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMes"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMesAnterior"].Value.GetType() != typeof(System.DBNull))
                        RetencionDelMesAntorior = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMesAnterior"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvAplicada"].Value.GetType() != typeof(System.DBNull))
                        RetencionAplicadas = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvAplicada"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvComposicionProcedente"].Value.GetType() != typeof(System.DBNull))
                        RetencionComposicionPrecedente = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvComposicionProcedente"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtExportadorSFMB"].Value.GetType() != typeof(System.DBNull))
                        ExportadorSFMB = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtExportadorSFMB"].Value.ToString());

                    RetencionPorAplicar = (SaldoFavor) + (ExportadorSFMB) + (PercepcionDelMes) + (PercepcionDelMesAntorior);

                    if (RetencionPorAplicar > 0)
                    {
                        if (-(RetencionDelMes + RetencionDelMesAntorior) > RetencionPorAplicar)
                        {
                            RetencionPorAplicar = (RetencionPorAplicar) + (RetencionDelMes) + (RetencionDelMesAntorior) + (RetencionComposicionPrecedente);
                        }
                        else RetencionPorAplicar = 0;
                    }
                    else RetencionPorAplicar = (RetencionDelMes) + (RetencionDelMesAntorior) + (RetencionComposicionPrecedente);

                    DgvPDT.Rows[i].Cells["PdtRetencionesIgvPorAplicar"].Value = Math.Round(RetencionPorAplicar, 2);

                    RetencionAplicadaResult = Math.Round(((RetencionDelMes + RetencionDelMesAntorior) - RetencionPorAplicar) * (-1), 2);
                    DgvPDT.Rows[i].Cells["PdtRetencionesIgvAplicada"].Value = RetencionAplicadaResult;

                    //IGV / PAGO
                    IgvPagoAPagar = (SaldoFavor) + (ExportadorSFMB) + (PercepcionDelMes) + (PercepcionDelMesAntorior) + (RetencionDelMes) + (RetencionDelMesAntorior);
                    if (IgvPagoAPagar < 1)
                        IgvPagoAPagar = 0;

                    DgvPDT.Rows[i].Cells["PdtIgvPagoAPagar"].Value = Math.Round(IgvPagoAPagar, 2);

                    //IMPUESTO A LA RENTA / PAGO
                    if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.GetType() != typeof(System.DBNull))
                        ImpuestoAlaRentaImpuestoResultante = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPagado"].Value.GetType() != typeof(System.DBNull))
                        ImpuestoAlaRentaPagado = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPagado"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.GetType() != typeof(System.DBNull))
                        ImpuestoAlaRentaCompensacionSFA = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.ToString());

                    // if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.GetType() != typeof(System.DBNull))
                    //     ImpuestoAlaRentaCompensacionSFMB = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.GetType() != typeof(System.DBNull))
                        ImpuestoAlaRentaCompensacionITAN = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.ToString());

                    if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.GetType() != typeof(System.DBNull))
                        ImpuestoAlaRentaCompensacionPercepcion = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.ToString());

                    // if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImputacion"].Value.GetType() != typeof(System.DBNull))
                    //     ImpuestoAlaRentaImputacion = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImputacion"].Value.ToString());

                    //if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPorPagar"].Value.GetType() != typeof(System.DBNull))
                    //    ImpuestoAlaRentaPorPagar = Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPorPagar"].Value.ToString());

                    ImpuestoAlaRentaPorPagar = (ImpuestoAlaRentaImpuestoResultante) + (ImpuestoAlaRentaPagado) + (ImpuestoAlaRentaCompensacionSFA) + (ImpuestoAlaRentaCompensacionITAN) + (ImpuestoAlaRentaCompensacionPercepcion);

                    DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPorPagar"].Value = Math.Round(ImpuestoAlaRentaPorPagar, 2);
                }
                CalcSumPDT();
            } catch(Exception) {}
        }

        private void DgvPDT_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.DgvPDT.Columns[e.ColumnIndex].Name)
            {
                case "PdtFicalIgvCreditoDebito":
                case "PdtPercepcionesIgvMesAnterior":
                case "PdtPercepcionesIgvComposicionProcedente":

                case "PdtRetencionesIgvDelMesAnterior":
                case "PdtRetencionesIgvComposicionProcedente":
                //case "PdtRetencionesIgvDelMes":
                //case "PdtRetencionesIgvAplicada":
                //case "PdtRetencionesIgvPorAplicar":
                    try
                    {
                        for (int i = 0; i < DgvPDT.Rows.Count - 1; i++)
                        {
                            CalcPDT();
                            if (i > 0)
                            {
                                //CREDITO/DEBITO FISCAL IGV
                                if (Convert.ToDouble(DgvPDT.Rows[i - 1].Cells["PdtFicalIgvSaldoFavorPagar"].Value.ToString()) > 0)
                                {
                                    DgvPDT.Rows[i].Cells["PdtFicalIgvCreditoDebito"].Value = 0;
                                } else
                                {
                                    DgvPDT.Rows[i].Cells["PdtFicalIgvCreditoDebito"].Value = Convert.ToDouble(DgvPDT.Rows[i - 1].Cells["PdtFicalIgvSaldoFavorPagar"].Value.ToString());
                                }
                                //PERCEPCIONES IGV
                                DgvPDT.Rows[i].Cells["PdtPercepcionesIgvMesAnterior"].Value = DgvPDT.Rows[i - 1].Cells["PdtPercepcionesIgvPorAplicar"].Value.ToString();

                                //RETENCIONES IGV
                                DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMesAnterior"].Value = DgvPDT.Rows[i - 1].Cells["PdtRetencionesIgvPorAplicar"].Value.ToString();
                            }
                        }
                    }
                    catch (Exception) {}
                    break;
                case "PdtImpuestoAlaRentaPagado":
                case "PdtImpuestoAlaRentaCompensacionSFA":
                case "PdtImpuestoAlaRentaCompensacionSFMB":
                case "PdtImpuestoAlaRentaCompensacionITAN":
                case "PdtImpuestoAlaRentaCompensacionPercepcion":
                case "PdtImpuestoAlaRentaImputacion":
                case "PdtImpuestoAlaRentaPorPagar":
                    try
                    {
                        double ImpuestoAlaRentaImpuestoResultante = 0, ImpuestoAlaRentaPagado = 0, ImpuestoAlaRentaCompensacionSFA = 0, ImpuestoAlaRentaCompensacionITAN = 0, ImpuestoAlaRentaCompensacionPercepcion = 0, ImpuestoAlaRentaPorPagar = 0;
                      
                        //IMPUESTO A LA RENTA / PAGO
                        if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.GetType() != typeof(System.DBNull))
                            ImpuestoAlaRentaImpuestoResultante = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.ToString());

                        if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaPagado"].Value.GetType() != typeof(System.DBNull))
                            ImpuestoAlaRentaPagado = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaPagado"].Value.ToString());

                        if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.GetType() != typeof(System.DBNull))
                            ImpuestoAlaRentaCompensacionSFA = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.ToString());

                        // if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.GetType() != typeof(System.DBNull))
                        //     ImpuestoAlaRentaCompensacionSFMB = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.ToString());

                        if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.GetType() != typeof(System.DBNull))
                            ImpuestoAlaRentaCompensacionITAN = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.ToString());

                        if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.GetType() != typeof(System.DBNull))
                            ImpuestoAlaRentaCompensacionPercepcion = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.ToString());

                        // if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaImputacion"].Value.GetType() != typeof(System.DBNull))
                        //     ImpuestoAlaRentaImputacion = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaImputacion"].Value.ToString());

                        //if (DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaPorPagar"].Value.GetType() != typeof(System.DBNull))
                        //    ImpuestoAlaRentaPorPagar = Convert.ToDouble(DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaPorPagar"].Value.ToString());

                        ImpuestoAlaRentaPorPagar = (ImpuestoAlaRentaImpuestoResultante) + (ImpuestoAlaRentaPagado) + (ImpuestoAlaRentaCompensacionSFA) + (ImpuestoAlaRentaCompensacionITAN) + (ImpuestoAlaRentaCompensacionPercepcion);

                        DgvPDT.Rows[e.RowIndex].Cells["PdtImpuestoAlaRentaPorPagar"].Value = Math.Round(ImpuestoAlaRentaPorPagar, 2).ToString("F");
                    }
                    catch (Exception) {}
                    break;
            }
            CalcSumPDT();
        }

        private void ButtonGuardarPDT_Click(object sender, EventArgs e)
        {
            SavePDT();
        }

        private void ButtonReportPDT_Click(object sender, EventArgs e)
        {
            reportes.FormReportePDT FrmReportePDT = new reportes.FormReportePDT
            {
                MdiParent = FrmPrincipal.ActiveForm,
                usuario = idUsuario,
                ruc = txtNombreRuc.Text,
                rz = lblRazonSocial.Text,
                anio = Convert.ToInt32(txtNombreAnio.Text)
            };
            FrmReportePDT.Show();
        }

        private void DgvPDT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value.GetType() != typeof(System.DBNull))
                {
                    if (Convert.ToDouble(e.Value) < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception) {}
        }

        private void ReadOnlyPDT()
        {
            //DgvPDT.ReadOnly = true;
            //DgvPDT.Rows[0].Cells["PdtFicalIgvCreditoDebito"].ReadOnly = false;
            //DgvPDT.Rows[0].Cells["PdtPercepcionesIgvMesAnterior"].ReadOnly = false;
            //DgvPDT.Rows[0].Cells["PdtRetencionesIgvDelMesAnterior"].ReadOnly = false;

            //DgvPDT.Columns["PdtExportadorSFMB"].ReadOnly = false;
            //DgvPDT.Columns["PdtPercepcionesIgvComposicionProcedente"].ReadOnly = false;
            //DgvPDT.Columns["PdtRetencionesIgvComposicionProcedente"].ReadOnly = false;
            //DgvPDT.Columns["PdtIgvPagoPagado"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaPagado"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaCompensacionSFA"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaCompensacionITAN"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaCompensacionPercepcion"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaImputacion"].ReadOnly = false;
            //DgvPDT.Columns["PdtImpuestoAlaRentaPorPagar"].ReadOnly = false;
        }

        private void CalcSumPDT()
        {
            double IngresoExportación = 0, IngresoGravadas = 0, IngresoExonerada = 0, IngresoInafecta = 0, IngresoIGV = 0, IngresoImporteTotal = 0, EgresoBaseImponible = 0,
                EgresoIGV = 0, EgresoNoGravada = 0, EgresoImporteTotal = 0, PercepcionesIgvAplicada = 0, RetencionesIgvDelMes = 0, RetencionesIgvAplicada = 0,
                IgvPagoAPagar = 0, IgvPagoPagado = 0, ImpuestoAlaRentaOtrosIngreso = 0, ImpuestoAlaRentaBaseImponible = 0, ImpuestoAlaRentaImpuestoResultante = 0,
                ImpuestoAlaRentaPagado = 0, ImpuestoAlaRentaCompensacionSFA = 0, ImpuestoAlaRentaCompensacionSFMB = 0,
                ImpuestoAlaRentaCompensacionITAN = 0, ImpuestoAlaRentaCompensacionPercepcion = 0, ImpuestoAlaRentaImputacion = 0, ImpuestoAlaRentaPorPagar = 0;
            for (int i = 0; i < DgvPDT.Rows.Count - 1; i++)
            {
                if (DgvPDT.Rows[i].Cells["PdtIngresoExportación"].Value.GetType() != typeof(System.DBNull))
                    IngresoExportación += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoExportación"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIngresoGravadas"].Value.GetType() != typeof(System.DBNull))
                    IngresoGravadas += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoGravadas"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIngresoExonerada"].Value.GetType() != typeof(System.DBNull))
                    IngresoExonerada += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoExonerada"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIngresoInafecta"].Value.GetType() != typeof(System.DBNull))
                    IngresoInafecta += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoInafecta"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIngresoIGV"].Value.GetType() != typeof(System.DBNull))
                    IngresoIGV += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoIGV"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIngresoImporteTotal"].Value.GetType() != typeof(System.DBNull))
                    IngresoImporteTotal += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIngresoImporteTotal"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtEgresoBaseImponible"].Value.GetType() != typeof(System.DBNull))
                    EgresoBaseImponible += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtEgresoBaseImponible"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtEgresoIGV"].Value.GetType() != typeof(System.DBNull))
                    EgresoIGV += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtEgresoIGV"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtEgresoNoGravada"].Value.GetType() != typeof(System.DBNull))
                    EgresoNoGravada += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtEgresoNoGravada"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtEgresoImporteTotal"].Value.GetType() != typeof(System.DBNull))
                    EgresoImporteTotal += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtEgresoImporteTotal"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtPercepcionesIgvAplicada"].Value.GetType() != typeof(System.DBNull))
                    PercepcionesIgvAplicada += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtPercepcionesIgvAplicada"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMes"].Value.GetType() != typeof(System.DBNull))
                    RetencionesIgvDelMes += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvDelMes"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtRetencionesIgvAplicada"].Value.GetType() != typeof(System.DBNull))
                    RetencionesIgvAplicada += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtRetencionesIgvAplicada"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIgvPagoAPagar"].Value.GetType() != typeof(System.DBNull))
                    IgvPagoAPagar += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIgvPagoAPagar"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtIgvPagoPagado"].Value.GetType() != typeof(System.DBNull))
                    IgvPagoPagado += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtIgvPagoPagado"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaOtrosIngreso"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaOtrosIngreso += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaOtrosIngreso"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaBaseImponible"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaBaseImponible += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaBaseImponible"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaImpuestoResultante += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPagado"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaPagado += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPagado"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaCompensacionSFA += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaCompensacionSFMB += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaCompensacionITAN += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaCompensacionPercepcion += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImputacion"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaImputacion += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaImputacion"].Value.ToString());

                if (DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPorPagar"].Value.GetType() != typeof(System.DBNull))
                    ImpuestoAlaRentaPorPagar += Convert.ToDouble(DgvPDT.Rows[i].Cells["PdtImpuestoAlaRentaPorPagar"].Value.ToString());
            }
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoExportación"].Value = IngresoExportación;
            

            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoGravadas"].Value = IngresoGravadas;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoExonerada"].Value = IngresoExonerada;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoInafecta"].Value = IngresoInafecta;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoIGV"].Value = IngresoIGV;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIngresoImporteTotal"].Value = IngresoImporteTotal;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtEgresoBaseImponible"].Value = EgresoBaseImponible;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtEgresoIGV"].Value = EgresoIGV;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtEgresoNoGravada"].Value = EgresoNoGravada;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtEgresoImporteTotal"].Value = EgresoImporteTotal;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtPercepcionesIgvAplicada"].Value = PercepcionesIgvAplicada;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtRetencionesIgvDelMes"].Value = RetencionesIgvDelMes;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtRetencionesIgvAplicada"].Value = RetencionesIgvAplicada;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIgvPagoAPagar"].Value = IgvPagoAPagar;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtIgvPagoPagado"].Value = IgvPagoPagado;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaOtrosIngreso"].Value = ImpuestoAlaRentaOtrosIngreso;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaBaseImponible"].Value = ImpuestoAlaRentaBaseImponible;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaImpuestoResultante"].Value = ImpuestoAlaRentaImpuestoResultante;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaPagado"].Value = ImpuestoAlaRentaPagado;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaCompensacionSFA"].Value = ImpuestoAlaRentaCompensacionSFA;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaCompensacionSFMB"].Value = ImpuestoAlaRentaCompensacionSFMB;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaCompensacionITAN"].Value = ImpuestoAlaRentaCompensacionITAN;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaCompensacionPercepcion"].Value = ImpuestoAlaRentaCompensacionPercepcion;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaImputacion"].Value = ImpuestoAlaRentaImputacion;
            DgvPDT.Rows[DgvPDT.Rows.Count - 1].Cells["PdtImpuestoAlaRentaPorPagar"].Value = ImpuestoAlaRentaPorPagar;

        }
        #endregion
    }
}
