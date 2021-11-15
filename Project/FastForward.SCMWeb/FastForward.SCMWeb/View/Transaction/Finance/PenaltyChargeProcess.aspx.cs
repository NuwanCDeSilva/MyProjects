using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class PenaltyChargeProcess : BasePage
    {
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _para = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtpc.Text = "";
                txtinvtype.Text = "";
                lbldocuments.Text = "";
                txtfdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txttdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtasatdate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                grdrealinv.DataSource = null;
                grdrealinv.DataBind();
                grdinv.DataSource = null;
                grdinv.DataBind();
            }
            else
            {

            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Prefix:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.srtp:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Prefix1:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        protected void txtpc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtinvtype_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix1);
                _serData = CHNLSVC.CommonSearch.GetInv_Typforupdate_SearchData(_para, null, null, txtinvtype.Text);
                if (_serData == null)
               {
                   DispMsg("Invalid Type","N");
               }
                else
                {
                    if (_serData.Rows.Count>0)
                    {

                    }
                    else
                    {
                        DispMsg("Invalid Type", "N");
                    }
                }


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnpc_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProfitCenters);
                _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, null, null);
                LoadSearchPopup("ProfitCenters", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtninvtype_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix1);
                _serData = CHNLSVC.CommonSearch.GetInv_Typforupdate_SearchData(_para, null, null, txtpc.Text.ToString());
                LoadSearchPopup("Prefix1", "CODE", "ASC");


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData);
                    }
                }
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }

        }

        protected void txtSerByKey_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "")
                {

                }
                else if (_serType == "AllProfitCenters")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Prefix")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
                    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "srtp")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.srtp);
                    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Prefix1")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.srtp);
                    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "ProfitCenters")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
             
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "ProfitCenters")
                {
                    txtpc.Visible = true;
                    txtpc.Text = code;
                  
                }
                else if (_serType == "Prefix1")
                {
                    txtinvtype.Text = code;
                }
                
            }

            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    if (txtpc.Text == "")
                    {
                        DispMsg("Please Select PC", "N");
                        return;
                    }
                    string pc = txtpc.Text;
                    string invtype = txtinvtype.Text;
                    string docs = "";
                    lbldocuments.Text = "";
                    int effect = CHNLSVC.Financial.PanaltyChargeProcess(Session["UserCompanyCode"].ToString(), pc, invtype, Session["UserID"].ToString(), Session["SessionID"].ToString(),Convert.ToDateTime(txtasatdate.Text.ToString()), out docs);
                    if (effect > 0)
                    {
                        // DispMsg(docs,"S");
                        lbldocuments.Text = docs;
                    }
                    else
                    {
                        DispMsg(docs, "E");
                    }
                }
            }catch(Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtfdate_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtfdate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid " + "Start" + " Date " + "');", true);
                return;
            }
        }

        protected void txttdate_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txttdate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid " + "End" + " Date " + "');", true);
                return;
            }
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromdate = Convert.ToDateTime(txtfdate.Text.ToString());
                DateTime todate = Convert.ToDateTime(txttdate.Text.ToString());

                DataTable dt = CHNLSVC.Financial.GetPenaltyInvoices(fromdate,todate);
                grdinv.DataSource = dt;
                grdinv.DataBind();

                // group by ref no
                var query = from r in dt.AsEnumerable()
                            group r by r.Field<string>("sah_ref_doc") into groupedTable
                            select new
                            {
                                inv_no = groupedTable.Key
                            };
                DataTable invtable = ConvertToDataTable(query);

                grdrealinv.DataSource = invtable;
                grdrealinv.DataBind();

            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }
        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    int effect = 0;
                    foreach (GridViewRow row in grdinv.Rows)
                    {
                        CheckBox chkselect = (CheckBox)row.FindControl("chkselectinv");
                        if (chkselect.Checked)
                        {
                            //CANCEL PROCESS
                            Label debtno = (Label)row.FindControl("lbinvno");
                            effect = CHNLSVC.Financial.CancelPenalty(debtno.Text.ToString());
                        }
                    }
                    if (effect > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + "Successfully Cancelled" + "');", true);
                        grdrealinv.DataSource = null;
                        grdrealinv.DataBind();
                        grdinv.DataSource = null;
                        grdinv.DataBind();
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Didn't Save" + "');", true);
                        return;
                    }
                }
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            _print();
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in grdrealinv.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkallinv");
                    if (chkall.Checked)
                    {
                        chkselect.Checked = true;

                    }
                    else
                    {
                        chkselect.Checked = false;
                    }
                }
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + ex.Message + "');", true);
                return;
            }
        }

        public void _print()
        {
            try
            {
                string grntxt = "";
                foreach (GridViewRow row in grdrealinv.Rows)
                {
                    CheckBox chkallinv = (CheckBox)row.FindControl("chkallinv");
                    if (chkallinv != null && chkallinv.Checked)
                    {
                        if (grntxt != "")
                        {
                            grntxt = grntxt + ",";
                        }
                        Label lblrealinvno = (Label)row.FindControl("lblrealinvno");
                        string com_cd = lblrealinvno.Text;
                        grntxt = grntxt + com_cd;

                    }
                }


                Session["GlbReportType"] = "SCM1_DO";
                Session["GlbReportName"] = "Outward_Docs_DO.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsInventory obj = new clsInventory();
                obj.printpanaltychargedetails(Session["GlbReportName"].ToString(), grntxt, Session["UserID"].ToString());
                PrintPDF(targetFileName, obj._statementofacc);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Penalty Charge Process Print", "PenaltyChargeProcess", ex.Message, Session["UserID"].ToString());
            }
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtasatdate_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtasatdate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$") == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Please Enter Valid " + "" + " Date " + "');", true);
                return;
            }
        }
    
    
    }
}