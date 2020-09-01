using Negocios;
using System;
using System.Data;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmLeasing : Form
    {
        #region Variables Globales
        DateTime FechaFormalizacion;
        double TipoCambioG, SaldoG, SaldoSolesG;

        TipoCambio tipoCambio = new TipoCambio();
        #endregion
        private FrmLeasing()
        {
            InitializeComponent();
        }

        private static FrmLeasing Instancia = null;

        public static FrmLeasing GetForm()
        {
            if (Instancia == null)
            {
                Instancia = new FrmLeasing();
                Instancia.FormClosed += new FormClosedEventHandler(Reset);//SOLO PARA FORMULARIOS
            }
            return Instancia;
        }

        private static void Reset(object sender, FormClosedEventArgs e)//SOLO PARA FORMULARIOS
        {
            Instancia = null;
        }

        private void FrmLeasing_Load(object sender, EventArgs e)
        {
            GetChangeType();
        }

        private void DataGridViewLeasingDolares_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int LeasingNumeroCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingNumeroCuota"].Value.GetType() != typeof(DBNull) ? Convert.ToInt32(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingNumeroCuota"].Value) : 0;
                //string LeasingFechaVencimiento = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaVencimiento"].Value.GetType() != typeof(DBNull) ? DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaVencimiento"].Value.ToString() : "";
                double LeasingDolaresCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresCapital"].Value) : 0;
                double LeasingDolaresInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresInteres"].Value) : 0;
                double LeasingDolaresComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresComisionSeguros"].Value) : 0;
                double LeasingDolaresValorCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresValorCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresValorCuota"].Value) : 0;
                double LeasingDolaresIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresIgv"].Value) : 0;
                double LeasingDolaresTotalCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresTotalCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresTotalCuota"].Value) : 0;
                double LeasingDolaresSaldo = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresSaldo"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresSaldo"].Value) : 0;
                
                //double LeasingFechaEmisionTipoCambio = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaEmisionTipoCambio"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaEmisionTipoCambio"].Value) : 0;
                //double LeasingSolesSaldo = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesSaldo"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesSaldo"].Value) : 0;
                //double LeasingSolesCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value) : 0;
                //double LeasingSolesInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value) : 0;
                //double LeasingSolesComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value) : 0;
                //double LeasingSolesIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value) : 0;
                //double LeasingSolesTotalCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesTotalCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesTotalCuota"].Value) : 0;
                //double LeasingFechaPagoTipoCambio = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTipoCambio"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTipoCambio"].Value) : 0;
                //string LeasingFechaPago = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPago"].Value != DBNull.Value ? DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPago"].Value.ToString() : "";
                //double LeasingFechaPagoCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoCapital"].Value) : 0;
                //double LeasingFechaPagoInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoInteres"].Value) : 0;
                //double LeasingFechaPagoComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoComisionSeguros"].Value) : 0;
                //double LeasingFechaPagoIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoIgv"].Value) : 0;
                //double LeasingFechaPagoTotalCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTotalCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTotalCuota"].Value) : 0;
                //double LeasingFechaPagoSaldo = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value) : 0;
                //double LeasingDirefenciaCambioCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioCapital"].Value) : 0;
                //double LeasingDirefenciaCambioInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioInteres"].Value) : 0;
                //double LeasingDirefenciaCambioIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioIgv"].Value) : 0;
                //double LeasingDirefenciaCambioTotal = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioTotal"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioTotal"].Value) : 0;
                //int LeasingFechaPagoMes = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoMes"].Value != DBNull.Value ? Convert.ToInt32(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoMes"].Value) : 0;
                //string Leasing = DGVLeasingDolares.Rows[e.RowIndex].Cells["Leasing"].Value != DBNull.Value ? DGVLeasingDolares.Rows[e.RowIndex].Cells["Leasing"].Value.ToString() : "";
                //string LeasingEntidad = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingEntidad"].Value != DBNull.Value ? DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingEntidad"].Value.ToString() : "";

                double ValorCuota = 0, Igv = 0, TotalCuota = 0, SaldoDolares,
                    SaldoS, CapitalS, InteresS, ComisionSegurosS, IgvS, TotalCoutaS;

                TipoCambioG = TBTipoCambio.Text != "" ? Convert.ToDouble(TBTipoCambio.Text) : 0;
                SaldoG = TBSaldo.Text != "" ? Convert.ToDouble(TBSaldo.Text) : 0;
                SaldoSolesG = TBSaldoSoles.Text != "" ? Convert.ToDouble(TBSaldoSoles.Text) : 0;

            
                switch (DGVLeasingDolares.Columns[e.ColumnIndex].Name)
                {
                    case "LeasingDolaresCapital":
                    case "LeasingDolaresInteres":
                    case "LeasingDolaresComisionSeguros":

                        ValorCuota = Math.Round((LeasingDolaresCapital + LeasingDolaresInteres + LeasingDolaresComisionSeguros), 2);
                        Igv = Math.Round((ValorCuota * 0.18), 2);
                        TotalCuota = Math.Round(ValorCuota + Igv, 2);

                        for (int i = 0; i < DGVLeasingDolares.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                SaldoDolares = Math.Round(LeasingDolaresTotalCuota - LeasingDolaresSaldo, 2);
                            } else
                            {
                                SaldoDolares = Math.Round(TotalCuota - SaldoG, 2);
                            }
                            DGVLeasingDolares.Rows[i].Cells["LeasingDolaresSaldo"].Value = SaldoDolares;
                        }

                        SaldoS = Math.Round(LeasingDolaresSaldo * TipoCambioG, 2);
                        CapitalS = Math.Round(LeasingDolaresCapital * TipoCambioG, 2);
                        InteresS = Math.Round(LeasingDolaresInteres * TipoCambioG, 2);
                        ComisionSegurosS = Math.Round(LeasingDolaresComisionSeguros * TipoCambioG, 2);
                        IgvS = Math.Round(LeasingDolaresIgv * TipoCambioG, 2);
                        TotalCoutaS = Math.Round(LeasingDolaresTotalCuota * TipoCambioG, 2);

                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresValorCuota"].Value = ValorCuota;
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresIgv"].Value = Igv;
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresTotalCuota"].Value = TotalCuota;
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaEmisionTipoCambio"].Value = TipoCambioG;

                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesSaldo"].Value = SaldoS.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value = CapitalS.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value = InteresS.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value = ComisionSegurosS.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value = IgvS.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesTotalCuota"].Value = TotalCoutaS.ToString("F");

                        //----------------------------
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresCapital"].Value = LeasingDolaresCapital.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresInteres"].Value = LeasingDolaresInteres.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresComisionSeguros"].Value = LeasingDolaresComisionSeguros.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresValorCuota"].Value = LeasingDolaresValorCuota.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresIgv"].Value = LeasingDolaresIgv.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresTotalCuota"].Value = LeasingDolaresTotalCuota.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresSaldo"].Value = LeasingDolaresSaldo.ToString("F");
                        break;

                    case "LeasingFechaPago":
                        break;
                }
            }
            catch (Exception Ex) { MessageBox.Show("Err: " + Ex); }
        }

        private void TBSaldo_KeyUp(object sender, KeyEventArgs e)
        {
            TipoCambioG = TBTipoCambio.Text != "" ? Convert.ToDouble(TBTipoCambio.Text) : 0;
            SaldoG = TBSaldo.Text != "" ? Convert.ToDouble(TBSaldo.Text) : 0;

            TBSaldoSoles.Text = (SaldoG * TipoCambioG).ToString();
        }

        private void DateTimePickerFechaFormalizacion_ValueChanged(object sender, EventArgs e)
        {
            GetChangeType();
        }

        #region Helpers
        private void GetChangeType()
        {
            FechaFormalizacion = DateTimePickerFechaFormalizacion.Value.Date;
            DataTable dataTable = new DataTable();
            dataTable = tipoCambio.Show(Convert.ToString(FechaFormalizacion));
            if (dataTable.Rows.Count > 0)
            {
                TBTipoCambio.Text = dataTable.Rows[0]["Venta"].ToString();
            }
            else MessageBox.Show("No existe un Tipo de Cambio para la fecha ingresada", "SISCONT .::. Leasing");
        }
        #endregion
    }
}
