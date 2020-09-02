using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Negocios;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmLeasing : Form
    {
        #region Variables Globales
        DateTime FechaFormalizacion;
        double TipoCambioG, SaldoG, SaldoSolesG, SaldoSolesFechaPagoG;

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
            FechaFormalizacion = DateTimePickerFechaFormalizacion.Value.Date;
            TBTipoCambio.Text = GetChangeType(Convert.ToString(FechaFormalizacion)).ToString();
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
                double LeasingSolesCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value) : 0;
                double LeasingSolesInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value) : 0;
                double LeasingSolesComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value) : 0;
                double LeasingSolesIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value) : 0;
                //double LeasingSolesTotalCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesTotalCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesTotalCuota"].Value) : 0;
                double LeasingFechaPagoTipoCambio = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTipoCambio"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTipoCambio"].Value) : 0;
                string LeasingFechaPago = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPago"].Value != null /*|| DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPago"].Value != DBNull.Value*/ ? DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPago"].Value.ToString() : Now();
                double LeasingFechaPagoCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoCapital"].Value) : 0;
                double LeasingFechaPagoInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoInteres"].Value) : 0;
                double LeasingFechaPagoComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoComisionSeguros"].Value) : 0;
                double LeasingFechaPagoIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoIgv"].Value) : 0;
                //double LeasingFechaPagoTotalCuota = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTotalCuota"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTotalCuota"].Value) : 0;
                //double LeasingFechaPagoSaldo = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value) : 0;
                double LeasingDirefenciaCambioCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioCapital"].Value) : 0;
                double LeasingDirefenciaCambioInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioInteres"].Value) : 0;
                double LeasingDirefenciaCambioIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioIgv"].Value) : 0;
                double LeasingDirefenciaCambioTotal = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioTotal"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDirefenciaCambioTotal"].Value) : 0;
                //int LeasingFechaPagoMes = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoMes"].Value != DBNull.Value ? Convert.ToInt32(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoMes"].Value) : 0;
                //string Leasing = DGVLeasingDolares.Rows[e.RowIndex].Cells["Leasing"].Value != DBNull.Value ? DGVLeasingDolares.Rows[e.RowIndex].Cells["Leasing"].Value.ToString() : "";
                //string LeasingEntidad = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingEntidad"].Value != DBNull.Value ? DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingEntidad"].Value.ToString() : "";

                double ValorCuota = 0, Igv = 0, TotalCuota = 0, SaldoDolares,
                    SaldoS, CapitalS, InteresS, ComisionSegurosS, IgvS, TotalCoutaS;

                double SaldoSolesFechaPago = 0, SolesFPTipoCambio = 0;

                SolesFPTipoCambio = Convert.ToDouble(GetChangeType(LeasingFechaPago));

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
                        
                        if (e.RowIndex > 0)
                        {
                            SaldoDolares = Math.Round(Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex - 1].Cells["LeasingDolaresSaldo"].Value) - TotalCuota, 2);
                        }
                        else
                        {
                            SaldoDolares = Math.Round(SaldoG - TotalCuota, 2);
                        }

                        SaldoS = SaldoDolares * TipoCambioG;
                        CapitalS = LeasingDolaresCapital * TipoCambioG;
                        InteresS = LeasingDolaresInteres * TipoCambioG;
                        ComisionSegurosS = LeasingDolaresComisionSeguros * TipoCambioG;
                        IgvS = Igv * TipoCambioG;
                        TotalCoutaS = TotalCuota * TipoCambioG;

                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresValorCuota"].Value = ValorCuota.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresIgv"].Value = Igv.ToString("F");
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresTotalCuota"].Value = TotalCuota.ToString("F");

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
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresSaldo"].Value = SaldoDolares.ToString("F");

                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value = SaldoSolesFechaPago.ToString("F");

                        if (LeasingFechaPagoTipoCambio > 0)
                        {
                            CalcSolesFPCapital(e.RowIndex, LeasingDolaresCapital, SolesFPTipoCambio);
                            CalcSolesFPInteres(e.RowIndex, LeasingDolaresInteres, SolesFPTipoCambio);
                            CalcSolesFPComisionSeguros(e.RowIndex, LeasingDolaresComisionSeguros, SolesFPTipoCambio);
                            CalcSolesFPIgv(e.RowIndex, Igv, SolesFPTipoCambio);
                            CalcSolesFPTotalCuota(e.RowIndex, TotalCuota, SolesFPTipoCambio);

                            CalcSaldoSolesFechaPago();

                            CalcDiferenciaCambioCapital(e.RowIndex, CapitalS, LeasingDirefenciaCambioCapital);
                            CalcDiferenciaCambioInteres(e.RowIndex, InteresS, ComisionSegurosS, LeasingFechaPagoInteres, LeasingFechaPagoComisionSeguros);
                            CalcDiferenciaCambioIgv(e.RowIndex, IgvS, LeasingFechaPagoIgv);
                            CalcDiferenciaCambioTotal(e.RowIndex, LeasingDirefenciaCambioCapital, LeasingDirefenciaCambioInteres, LeasingDirefenciaCambioIgv);
                        }
                        break;

                    case "LeasingFechaPago":
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTipoCambio"].Value = SolesFPTipoCambio;

                        CalcSolesFPCapital(e.RowIndex, LeasingDolaresCapital, SolesFPTipoCambio);
                        CalcSolesFPInteres(e.RowIndex, LeasingDolaresInteres, SolesFPTipoCambio);
                        CalcSolesFPComisionSeguros(e.RowIndex, LeasingDolaresComisionSeguros, SolesFPTipoCambio);
                        CalcSolesFPIgv(e.RowIndex, LeasingDolaresIgv, SolesFPTipoCambio);
                        CalcSolesFPTotalCuota(e.RowIndex, LeasingDolaresTotalCuota, SolesFPTipoCambio);

                        CalcSaldoSolesFechaPago();

                        LeasingSolesCapital = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesCapital"].Value) : 0;
                        LeasingSolesInteres = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesInteres"].Value) : 0;
                        LeasingSolesComisionSeguros = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesComisionSeguros"].Value) : 0;
                        LeasingSolesIgv = DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value != DBNull.Value ? Convert.ToDouble(DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesIgv"].Value) : 0;

                        CalcDiferenciaCambioCapital(e.RowIndex, LeasingSolesCapital, LeasingDirefenciaCambioCapital);
                        CalcDiferenciaCambioInteres(e.RowIndex, LeasingSolesInteres, LeasingSolesComisionSeguros, LeasingFechaPagoInteres, LeasingFechaPagoComisionSeguros);
                        CalcDiferenciaCambioIgv(e.RowIndex, LeasingSolesIgv, LeasingFechaPagoIgv);
                        CalcDiferenciaCambioTotal(e.RowIndex, LeasingDirefenciaCambioCapital, LeasingDirefenciaCambioInteres, LeasingDirefenciaCambioIgv);

                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoCapital"].Value = SolesFPCapital.ToString("F");
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoInteres"].Value = SolesFPInteres.ToString("F");
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoComisionSeguros"].Value = SolesFPComisionSeguros.ToString("F");
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoIgv"].Value = SolesFPIgv.ToString("F");
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoTotalCuota"].Value = SolesFPTotalCuota.ToString("F");
                        //DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingFechaPagoSaldo"].Value
                        break;
                }
            }
            catch (Exception Ex) { MessageBox.Show("Err: " + Ex); }
        }

        #region Calculos de Montos de Fecha de Pago
        private void CalcSolesFPCapital(int e, double LeasingDolaresCapital, double SolesFPTipoCambio)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingFechaPagoCapital"].Value = (LeasingDolaresCapital * SolesFPTipoCambio).ToString("F");
        }

        private void CalcSolesFPInteres(int e, double LeasingDolaresComisionSeguros, double SolesFPTipoCambio)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingFechaPagoInteres"].Value = (LeasingDolaresComisionSeguros * SolesFPTipoCambio).ToString("F");
        }

        private void CalcSolesFPComisionSeguros(int e, double LeasingDolaresComisionSeguros, double SolesFPTipoCambio)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingFechaPagoComisionSeguros"].Value = (LeasingDolaresComisionSeguros * SolesFPTipoCambio).ToString("F");
        }

        private void CalcSolesFPIgv(int e, double Igv, double SolesFPTipoCambio)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingFechaPagoIgv"].Value = (Igv * SolesFPTipoCambio).ToString("F");
        }

        private void CalcSolesFPTotalCuota(int e, double TotalCuota, double SolesFPTipoCambio)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingFechaPagoTotalCuota"].Value = (TotalCuota * SolesFPTipoCambio).ToString("F");
        }

        //Diferencia de Cambio
        private void CalcDiferenciaCambioCapital(int e, double SolesCapital, double SolesFechaPagoCapital)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingDirefenciaCambioCapital"].Value = (SolesCapital - SolesFechaPagoCapital).ToString("F");
        }
        private void CalcDiferenciaCambioInteres(int e, double SolesInteres, double SolesComisionSeguro, double SolesFechaPagoInteres, double SolesFechaPagoComisionSeguro)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingDirefenciaCambioInteres"].Value = ((SolesInteres + SolesComisionSeguro) - SolesFechaPagoInteres - SolesFechaPagoComisionSeguro).ToString("F");
        }

        private void CalcDiferenciaCambioIgv(int e, double SolesIgv, double SolesFechaPagoIgv)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingDirefenciaCambioIgv"].Value = (SolesIgv - SolesFechaPagoIgv).ToString("F");
        }

        private void CalcDiferenciaCambioTotal(int e, double DiferenciaCambioCapital, double DiferenciaCambioInteres, double DiferenciaCambioIgv)
        {
            DGVLeasingDolares.Rows[e].Cells["LeasingDirefenciaCambioTotal"].Value = (DiferenciaCambioCapital + DiferenciaCambioInteres + DiferenciaCambioIgv).ToString("F");
        }
        #endregion

        private void TBSaldo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                TipoCambioG = TBTipoCambio.Text != "" ? Convert.ToDouble(TBTipoCambio.Text) : 0;
                SaldoG = TBSaldo.Text != "" ? Convert.ToDouble(TBSaldo.Text) : 0;

                TBSaldoSoles.Text = (SaldoG * TipoCambioG).ToString("F");
                TBSaldoSolesFechaPago.Text = ((SaldoG * TipoCambioG) - CalcSumDirefenciaCambioTotal()).ToString(); // falta Diferencia Cambio Sumatoria
                CalcSaldoDolares();
                CalcSaldoSolesFechaPago();
            }
            catch (Exception) { }
        }

        private void CalcSaldoDolares()
        {
            double DolaresSaldo = 0;
            for (int i = 0; i < DGVLeasingDolares.Rows.Count - 1; i++)
            {
                if (i < 1)
                    DolaresSaldo = Math.Round(SaldoG - Convert.ToDouble(DGVLeasingDolares.Rows[0].Cells["LeasingDolaresTotalCuota"].Value), 2);
                else
                    DolaresSaldo = Math.Round(Convert.ToDouble(DGVLeasingDolares.Rows[i - 1].Cells["LeasingDolaresSaldo"].Value) - Convert.ToDouble(DGVLeasingDolares.Rows[i].Cells["LeasingDolaresTotalCuota"].Value), 2);
                DGVLeasingDolares.Rows[i].Cells["LeasingDolaresSaldo"].Value = DolaresSaldo.ToString("F");
                DGVLeasingDolares.Rows[i].Cells["LeasingSolesSaldo"].Value = (DolaresSaldo * TipoCambioG).ToString("F");
            }
        }

        private void CalcSaldoSolesFechaPago()
        {
            SaldoSolesFechaPagoG = TBSaldoSolesFechaPago.Text != "" ? Convert.ToDouble(TBSaldoSolesFechaPago.Text) : 0;
            double SaldoSolesFechaPago = 0;
            for (int i = 0; i < DGVLeasingDolares.Rows.Count - 1; i++)
            {
                if (i < 1)
                    SaldoSolesFechaPago = SaldoSolesFechaPagoG - Convert.ToDouble(DGVLeasingDolares.Rows[0].Cells["LeasingFechaPagoTotalCuota"].Value);
                else
                    SaldoSolesFechaPago = Convert.ToDouble(DGVLeasingDolares.Rows[i - 1].Cells["LeasingFechaPagoSaldo"].Value) - Convert.ToDouble(DGVLeasingDolares.Rows[i].Cells["LeasingFechaPagoTotalCuota"].Value);
                DGVLeasingDolares.Rows[i].Cells["LeasingFechaPagoSaldo"].Value = SaldoSolesFechaPago.ToString("F");
            }
        }

        private double CalcSumDirefenciaCambioTotal()
        {
            double DiferenciaCambioSumatoriaTotal = 0;
            for (int i = 0; i < DGVLeasingDolares.Rows.Count; i++)
            {
                DiferenciaCambioSumatoriaTotal += Convert.ToDouble(DGVLeasingDolares.Rows[i].Cells["LeasingDirefenciaCambioTotal"].Value);
            }
            return DiferenciaCambioSumatoriaTotal;
        }

        private void DGVLeasingDolares_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                switch (DGVLeasingDolares.Columns[e.ColumnIndex].Name)
                {
                    case "LeasingDolaresSaldo":
                    case "LeasingSolesSaldo":
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingDolaresSaldo"].Style.ForeColor = Color.Red;
                        DGVLeasingDolares.Rows[e.RowIndex].Cells["LeasingSolesSaldo"].Style.ForeColor = Color.Red;
                        break;
                }
            }
            catch (Exception) { }
        }

        private void DateTimePickerFechaFormalizacion_ValueChanged(object sender, EventArgs e)
        {
            FechaFormalizacion = DateTimePickerFechaFormalizacion.Value.Date;
            TBTipoCambio.Text = GetChangeType(Convert.ToString(FechaFormalizacion)).ToString();
            CalcSaldoDolares();
            CalcSaldoSolesFechaPago();
        }

        private void TBSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void DGVLeasingDolares_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #region Helpers
        private double GetChangeType(string Fecha)
        {
            if (Fecha != "" | Fecha != null)
            {
                DataTable dataTable = new DataTable();
                dataTable = tipoCambio.Show(Fecha);
                if (dataTable.Rows.Count > 0)
                    return Convert.ToDouble(dataTable.Rows[0]["Venta"].ToString());
                else
                    MessageBox.Show("No existe un Tipo de Cambio para la fecha ingresada", "SISCONT .::. Leasing");
            }
            return 0;
        }

        private string Now()
        {
            return DateTime.UtcNow.ToString("dd/MM/yyyy");
        }
        #endregion
    }
}
