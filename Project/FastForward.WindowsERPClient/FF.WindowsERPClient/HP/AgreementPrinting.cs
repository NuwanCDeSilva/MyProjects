using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class AgreementPrinting : Base
    {
        public AgreementPrinting()
        {
            InitializeComponent();

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                //add by Chamal 17-Jun-2014
                rdoDiriya.Text = ConvertTo_ProperCase(_masterComp.Mc_anal3.ToString());
            }
        }


        string accNo;

        public string AccountNo
        {
            get { return accNo; }
            set { accNo = value; }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                gvAccounts.AutoGenerateColumns = false;
                DataTable _dt = CHNLSVC.Sales.GetHPAccountFromDate(dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                gvAccounts.DataSource = _dt;

                foreach (DataGridViewRow grv in gvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                    cell.Value = "False";
                }
                gvAccounts.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                gvAccounts.EndEdit();
                for (int i = 0; i < gvAccounts.Rows.Count; i++)
                {

                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)gvAccounts.Rows[i].Cells[0];
                    if (cell.Value.ToString().ToUpper() == "TRUE")
                    {
                        AccountNo = gvAccounts.Rows[i].Cells[1].Value.ToString();
                    }

                }


                //.................
                //BaseCls.GlbReportDoc = AccountNo;
                //BaseCls.GlbReportName = "HPAgreemnt_Print.rpt";

                //Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                //_view.GlbReportName = "HP AGREEMENT PRINT";
                //_view.Show();
                //_view = null;
                if (rdoAgree.Checked)
                {
                    BaseCls.GlbReportDoc = AccountNo;
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = "HPAgreemnt_Print.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "HPAgreemnt_Print_SGL.rpt";
                    }

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    _view.GlbReportName = "HP AGREEMENT PRINT";
                    _view.Show();
                    _view = null;

                    //BaseCls.GlbReportDoc = "QUO-012-17";
                    //BaseCls.GlbReportName = "Quotation_RepPrint.rpt";

                    //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //_view.GlbReportName = "QUOTATION";
                    //_view.Show();
                    //_view = null;
                }
                else if (rdoDiriya.Checked)
                {
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    string _docNo = AccountNo;
                    BaseCls.GlbReportName = "HpInsuranceAgreement.rpt";
                    BaseCls.GlbReportDoc = _docNo;

                    _view.Show();
                    _view = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gvAccounts.DataSource = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in gvAccounts.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "True";
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in gvAccounts.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "False";
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            gvAccounts.DataSource = null;
        }

        private void gvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gvAccounts.EndEdit();
            for (int i = 0; i < gvAccounts.Rows.Count; i++)
            {

                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)gvAccounts.Rows[i].Cells[0];
                if (cell.Value == null) {
                    btnView_Click(null, null);
                }
                else if(cell.Value.ToString().ToUpper() == "TRUE")
                {
                    if (i != e.RowIndex)
                        cell.Value = "False";
                }
                

            }
        }


    }
}
