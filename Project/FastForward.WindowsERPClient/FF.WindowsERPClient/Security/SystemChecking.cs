using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Diagnostics;

namespace FF.WindowsERPClient.Security
{
    public partial class SystemChecking : Base
    {
        public SystemChecking()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                _view.GlbReportName = "InvoiceHalfPrints.rpt";
                _view.GlbReportDoc = "AAZEM-CS00016";
                _view.Show();
                _view = null;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                _view.GlbReportName = "InvoicePrintTax.rpt";
                _view.GlbReportDoc = "AAZPG-CR00142";
                _view.Show();
                _view = null;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExpotExcel_Click(object sender, EventArgs e)
        {
            try
            {
                 string _Err = string.Empty;  
                Cursor.Current = Cursors.WaitCursor;
                string _filePath = CHNLSVC.Inventory.SampleExportExcel2007(BaseCls.GlbUserComCode, BaseCls.GlbUserID,"", out _Err);
                if (!string.IsNullOrEmpty(_Err))
                {
                    MessageBox.Show(_Err.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutoTransfer_Click(object sender, EventArgs e)
        {
            int _ref = 0;

            //_ref = CHNLSVC.Inventory.CompanyTransferAutoProcessPriceCheck("SGL", "1800019", Convert.ToDateTime("27-Mar-2014").Date);

            //_ref = CHNLSVC.Inventory.CompanyTransferAutoProcess("ABL", "1800019", Convert.ToDateTime("31-Mar-2014").Date);

            _ref = 0;
        }
    }
}
