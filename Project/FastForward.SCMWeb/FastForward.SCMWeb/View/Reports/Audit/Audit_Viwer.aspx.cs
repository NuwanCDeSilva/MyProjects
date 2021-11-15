using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;


namespace FastForward.SCMWeb.View.Reports.Audit
{
    public partial class Audit_Viwer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GlbReportName"] == "rptAllSecurityUsers.rpt")
            {
                AllSecurityUserReport();
            }
        }

        private void AllSecurityUserReport()
        {
            DataTable dtData = CHNLSVC.General.GetSecurityUsers(BaseCls.GlbReportCompCode, BaseCls.GlbReportUser, BaseCls.GlbReportDepartment, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate);
            dtData.TableName = "SecUsers";
            if (dtData.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();

                DataTable dtparas = new DataTable();
                dtparas.Columns.Add("User", typeof(string));
                dtparas.Columns.Add("FUser", typeof(string));
                dtparas.Columns.Add("FFromDate", typeof(string));
                dtparas.Columns.Add("FTodate", typeof(string));
                dtparas.Columns.Add("FDeparment", typeof(string));
                dtparas.Columns.Add("FCom", typeof(string));

                String FCom = (BaseCls.GlbReportCompCode == "") ? "All" : BaseCls.GlbReportCompCode;
                String User = (Session["UserID"].ToString() == "") ? "All" : Session["UserID"].ToString();
                String FUser = (BaseCls.GlbReportUser == "") ? "All" : BaseCls.GlbReportUser;
                String FDeparment = (BaseCls.GlbReportDepartment == "") ? "All" : BaseCls.GlbReportDepartment;

                DataRow drTemp = dtparas.NewRow();
                drTemp["User"] = User;
                drTemp["FUser"] = FUser;
                drTemp["FCom"] = FCom;
                drTemp["FFromDate"] = BaseCls.GlbReportFromDate.ToString("dd/MMM/yyyy");
                drTemp["FTodate"] = BaseCls.GlbReportToDate.ToString("dd/MMM/yyyy");
                drTemp["FDeparment"] = FDeparment;
                dtparas.Rows.Add(drTemp);
                ds2.Tables.Add(dtparas);

                ds.Tables.Add(dtData);
                CrystalReportSource1.Report.FileName = "rptAllSecurityUsers.rpt";
                CrystalReportSource1.ReportDocument.Database.Tables[0].SetDataSource(ds);
                CrystalReportSource1.ReportDocument.Database.Tables[1].SetDataSource(dtparas);
                CrystalReportSource1.ReportDocument.Refresh();

                string date = DateTime.Now.ToString("ddMMyyyy");
                string time = DateTime.Now.ToString("hhmmss");
                string ID = Convert.ToString(Session["UserID"]);
                string FileName = time + ID;
                CrystalReportSource1.ReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "RoleMenuPrivilegesReport");
            }
            else
            {
                //Response.Redirect(Session["PriURL"].ToString());
            }
        }
    }
}