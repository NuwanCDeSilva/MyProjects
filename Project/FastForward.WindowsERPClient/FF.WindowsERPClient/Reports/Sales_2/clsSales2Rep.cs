using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Reports.Sales_2
{
    class clsSales2Rep
    {
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        Base bsObj;
        public clsSales2Rep()
        {
            bsObj = new Base();
        }

        public void BrandRep01()
        {// Sanjeewa 03-03-2014
            DataTable param = new DataTable();

            string _err;
            string _filePath;

            DataTable BrandRep = bsObj.CHNLSVC.Financial.GetBrandRep01Details(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbUserID,BaseCls.GlbReportType, out _err, out _filePath);

            if (BrandRep.Rows.Count == 0)
            {
                MessageBox.Show("No Records Found.", "Exporting to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(_err))
                {
                    MessageBox.Show(_err.ToString(), "System Error in Exporting to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                }
            }
        }

    }
}
