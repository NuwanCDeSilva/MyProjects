using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class TechnicianAllocation : BasePage //System.Web.UI.Page
    {
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _para = "";
        private List<TechAllocation> _TechAllocation = new List<TechAllocation>();
        DataTable resultnull = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdJobDeatils.DataSource = resultnull;
                grdJobDeatils.DataBind();
                grdTechnician.DataSource = _TechAllocation;
                grdTechnician.DataBind();
                txtFromDate.Text = DateTime.Now.ToShortDateString();
                txtToDate.Text = DateTime.Now.ToShortDateString();
                txtTDateFrom.Text = DateTime.Now.ToShortDateString();
                txtTDateTo.Text = DateTime.Now.ToShortDateString();
            }
        }

        
        protected void DummyDataBind()
        {
            DataTable dummy = new DataTable();
            System.Data.DataColumn code = new System.Data.DataColumn("CODE", typeof(System.String));
            System.Data.DataColumn desc = new System.Data.DataColumn("DESCRIPTION", typeof(System.String));
            dummy.Columns.Add(code);
            dummy.Columns.Add(desc);
            BindUCtrlDDLData(dummy);
        }

        protected void lbtnModel_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.GetAllModels(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "Model";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "Item";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnEngine_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EngineNo);
                _serData = CHNLSVC.CommonSearch.GetAllEngineNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "EngineNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnJob_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                txtFDate.Text = DateTime.Now.ToShortDateString();
                txtTDate.Text = DateTime.Now.ToShortDateString(); 
                //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                //_serData = CHNLSVC.CommonSearch.GetServiceJobs(_para, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, null, null, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);//ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text

                grdResultD.DataSource = _serData;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_serData);
                UserDPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
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

        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EngineNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtSearchbyword.Text) ? string.Empty : txtSearchbyword.Text) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ChassiNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JobNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + (chkNotAllocated.Checked ? "1" : "0") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.aodNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Technician:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                //    {
                //        String isCustExpect = "0";

                //        isCustExpect = (chkCusExpected.Checked) ? "1" : "0";

                //        if (chkNotAllocated.Checked)
                //        {
                //            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "1.1,2" + seperator + isCustExpect + seperator);
                //            break;
                //        }
                //        else
                //        {
                //            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "1.1,3,4,4.1,5,5.1" + seperator + isCustExpect + seperator);
                //            break;
                //        }
                //    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Des = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Technician")
            {
                Des = grdResult.SelectedRow.Cells[2].Text;
            }
            
            if (lblvalue.Text == "Model")
            {
                txtModel.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "Item")
            {
                txtItem.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "EngineNo")
            {
                txtEngine.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "ChassiNo")
            {
                txtchassi.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "JobNo")
            {
                txtJob.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "AodNo")
            {
                txtAod.Text = ID;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "Technician")
            {
                txtTechCode.Text = ID; //+ " - " + Des;
                txtTechName.Text = Des;
                lblvalue.Text = "";
                txtSearchbyword.Text = string.Empty;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "EngineNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EngineNo);
                _serData = CHNLSVC.CommonSearch.GetAllEngineNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "EngineNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "JobNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "ChassiNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChassiNo);
                _serData = CHNLSVC.CommonSearch.GetAllChassiNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "ChassiNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Item")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Item";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "AodNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.aodNo);
                _serData = CHNLSVC.CommonSearch.GetAllAodNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "AodNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Technician")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Technician);
                _serData = CHNLSVC.CommonSearch.GetAllTechnician(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Technician";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Model")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.GetAllModels(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Model";
                txtSearchbyword.Focus();
            }
        }

        protected void lbtnchassi_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChassiNo);
                _serData = CHNLSVC.CommonSearch.GetAllChassiNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "ChassiNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }

        //Searching Grid Data
        protected void lbtnSearchJob_Click(object sender, EventArgs e)
        {
            parameterValidate();
            if (txtFromDate.Text != "" || txtToDate.Text != "")
            {
                bool vFDate = IsDateTime(txtFromDate.Text);
                bool vTDate = IsDateTime(txtToDate.Text);
                if (!vFDate && !vTDate)
                {
                    DispMsg("Please select a valid date", "E");
                    return;
                }
            }
            if((txtModel.Text != "") && (txtItem.Text != "") && (txtJob.Text != "") && (txtchassi.Text != "") && (txtEngine.Text != "") && (txtAod.Text != ""))
            {
                txtFromDate.Text = null;
                txtToDate.Text = null;
            }

            DataTable _result = new DataTable();
            _result = CHNLSVC.Inventory.GetTechAllocationDetails(Session["UserCompanyCode"].ToString(), chkNotAllocated.Checked ? "1" : "0", txtModel.Text, txtItem.Text, txtJob.Text, txtchassi.Text, txtEngine.Text, (txtFromDate.Text == "" ? "" : txtFromDate.Text), (txtToDate.Text == "" ? null : txtToDate.Text), txtAod.Text, Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString());
            ViewState["SEARCH"] = _result;
            grdJobDeatils.DataSource = _result;
            grdJobDeatils.DataBind();
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "EngineNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EngineNo);
                _serData = CHNLSVC.CommonSearch.GetAllEngineNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "EngineNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "JobNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "ChassiNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllChassiNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "ChassiNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Item")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Item";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Model")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.GetAllModels(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Model";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "AodNo")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChassiNo);
                _serData = CHNLSVC.CommonSearch.GetAllAodNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "AodNo";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "Technician")
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Technician);
                _serData = CHNLSVC.CommonSearch.GetAllTechnician(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "Technician";
                txtSearchbyword.Focus();
            }
        }

        protected void grdJobDeatils_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdJobDeatils_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdJobDeatils.Rows)
            {
                Label act = (Label)row.FindControl("col_Allocated");
                decimal actVal = Convert.ToDecimal(act.Text.Trim());
                (row.FindControl("col_Allocated") as Label).Text = actVal > 2 ? "Yes" : "No";
            }
        }

        protected void chkHeaderAllApp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeaderAllApp = grdJobDeatils.HeaderRow.FindControl("chkHeaderAllApp") as CheckBox;
            foreach (GridViewRow item in grdJobDeatils.Rows)
            {
                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                if (chkHeaderAllApp.Checked)
                {
                    ChkselectItm.Checked = true;
                }
                else
                {
                    ChkselectItm.Checked = false;
                }
            }
        }

        protected void ChkselectItm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable _result = new DataTable();
            foreach (GridViewRow item in grdJobDeatils.Rows)
            {
                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                Label job_No = item.FindControl("col_JobNo") as Label;
                CheckBox chkHeaderAllApp = grdJobDeatils.HeaderRow.FindControl("chkHeaderAllApp") as CheckBox;
                if (ChkselectItm.Checked)
                {
                    _result = CHNLSVC.Inventory.get_technicianAllocated_Details(Session["UserCompanyCode"].ToString() , Session["UserDefLoca"].ToString(), job_No.Text);
                }
            }
            if (_result.Rows.Count > 0)
            {
                txtTechCode.Text = _result.AsEnumerable().FirstOrDefault().Field<string>("sth_emp_cd");
                txtTechName.Text = _result.AsEnumerable().FirstOrDefault().Field<string>("esep_first_name");
            }
        }

        protected void lbtnAod_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.aodNo);
                _serData = CHNLSVC.CommonSearch.GetAllAodNos(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "AodNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void grdTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnTechCode_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Technician);
                _serData = CHNLSVC.CommonSearch.GetAllTechnician(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _serData;
                grdResult.DataBind();
                BindUCtrlDDLData(_serData);
                UserPopoup.Show();
                lblvalue.Text = "Technician";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnAllocate_Click(object sender, EventArgs e)
        {
            parameterValidate();
            int totalCount = 0;
            totalCount = grdJobDeatils.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("ChkselectItm")).Checked);
            if (string.IsNullOrEmpty(txtTechCode.Text))
            {
                ErrorBinMsg("Please Select Technician...!");
                return;
            }
            if (string.IsNullOrEmpty(txtTDateFrom.Text))
            {
                ErrorBinMsg("Please Select From Date...!");
                return;
            }
            if (string.IsNullOrEmpty(txtTDateTo.Text))
            {
                ErrorBinMsg("Please Select To Date...!");
                return;
            }
            if (grdJobDeatils.Rows.Count <= 0)
            {
                ErrorBinMsg("Please Select Jobs To Allocate...!");
                return;
            }
            bool vFDate = IsDateTime(txtTDateFrom.Text);
            bool vTDate = IsDateTime(txtTDateTo.Text);
            if (!vFDate || !vTDate)
            {
                ErrorBinMsg("Please select the valid Date");
                return;
            }
            if (Convert.ToDateTime(txtTDateFrom.Text).Date > Convert.ToDateTime(txtTDateTo.Text).Date)
            {
                ErrorBinMsg("To Date Must Be Greter Than From Date...!");
                return;
            }

            _TechAllocation = ViewState["StoreTA"] as List<TechAllocation>;
            if (ViewState["StoreTA"] == null || ViewState["StoreTA"].ToString() == "")
            {
                _TechAllocation = new List<TechAllocation>();
            }

            foreach (GridViewRow row in grdJobDeatils.Rows)
            {
                if (_TechAllocation.Count > 0 && ViewState["VSItem"] != null)
                {
                    var rmvItem = _TechAllocation.Find(r => r.JobNo == ViewState["VSItem"].ToString());
                    if (rmvItem != null)
                    {
                        _TechAllocation.Remove(rmvItem);
                    }
                    ViewState["VSItem"] = null;
                }

                CheckBox cheHeader = row.FindControl("ChkselectItm") as CheckBox;
                if (cheHeader.Checked)
                {
                    TechAllocation _TecAll = new TechAllocation();
                    _TecAll.JobNo = (row.FindControl("col_JobNo") as Label).Text;
                    _TecAll.EngineNo = (row.FindControl("col_EngineNo") as Label).Text;
                    _TecAll.ChassisNo = (row.FindControl("col_ChassisNo") as Label).Text;
                    _TecAll.LineNo = Convert.ToInt16((row.FindControl("col_LineNo") as Label).Text);
                    _TecAll.EmpName = txtTechName.Text;
                    _TecAll.FromDate = string.IsNullOrEmpty(txtTDateFrom.Text) ? DateTime.Now : Convert.ToDateTime(txtTDateFrom.Text);
                    _TecAll.ToDate = string.IsNullOrEmpty(txtTDateTo.Text) ? DateTime.Now : Convert.ToDateTime(txtTDateTo.Text);
                    _TecAll.TechCode = txtTechCode.Text;
                    _TecAll.TownNo = (row.FindControl("col_Town") as Label).Text;
                    var res = _TechAllocation.Where(c => c.JobNo == (row.FindControl("col_JobNo") as Label).Text && c.EmpName == txtTechName.Text && grdTechnician.Rows.Count >= 1).FirstOrDefault();
                    if (res != null)
                    {
                        ErrorBinMsg("Can not add same job again...!!!");
                    }
                    else
                        _TechAllocation.Add(_TecAll);
                }
            }
            if (totalCount == 0)
            {
                ErrorBinMsg("Please select at least one item from the job list...!");
                return;
            }
            grdTechnician.DataSource = _TechAllocation;
            grdTechnician.DataBind();
            ViewState["StoreTA"] = _TechAllocation;
        }
        private void ErrorBinMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
        }

        private void SuccessBinMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
        }

        protected void lbtnTechAlloSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                try
                {
                    if (grdTechnician.Rows.Count > 0)
                    {
                        SaveUpdateTechAllocation();
                    }
                    else
                        ErrorBinMsg("Please Add Job For Technician Before Saving");
                }
                catch (Exception ex)
                {
                    ErrorBinMsg(ex.Message);
                }

            }
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
        }

        protected Int16 UpdateAutoNo()
        {
            MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();
            _masterAutoNumber.Aut_moduleid = "TECH";
            _masterAutoNumber.Aut_direction = 1;
            _masterAutoNumber.Aut_start_char = "TECH";
            _masterAutoNumber.Aut_cate_tp = "LOC";
            _masterAutoNumber.Aut_cate_cd = Session["UserDefLoca"].ToString();
            _masterAutoNumber.Aut_modify_dt = DateTime.Now;
            _masterAutoNumber.Aut_year = DateTime.Now.Year;
            return CHNLSVC.Inventory.UpdateTechAlloSeqNo(_masterAutoNumber);
        }

        protected DataTable TechAlloSeqNo(string modid, string locid, string catid)
        {
            return CHNLSVC.Inventory.TechAlloSeqNo(modid, locid, catid);
        }

        protected string UpdateAutoNo(string _module, Int16? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            try
            {
                return CHNLSVC.Financial.GetAndUpdateAutoNo(_module, _direction, _startChar, _catType, _catCode, _modifyDate, _year);
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
                return "";
            }
        }

        protected void SaveUpdateTechAllocation()
        {
            parameterValidate();
            int row_aff = 0;
            string doc = string.Empty;
            string errMsg = string.Empty;
            foreach (GridViewRow row in grdTechnician.Rows)
            {
                //string autoGeNo = UpdateAutoNo("TECH", 0, "TECH", "LOC", Session["UserDefProf"].ToString(), null, DateTime.Now.Year);
                //DataTable rootAutoNo = TechAlloSeqNo("TECH", Session["UserDefLoca"].ToString(), "LOC");
                TechAllocation _TecAll = new TechAllocation();
                _TecAll.JobNo = (row.FindControl("col_TJobNo") as Label).Text;
                _TecAll.FromDate = Convert.ToDateTime((row.FindControl("col_TFDate") as Label).Text);
                _TecAll.ToDate = Convert.ToDateTime((row.FindControl("col_TToDate") as Label).Text);
                _TecAll.Status = (row.FindControl("ChkselectTechItm") as CheckBox).Checked ? 1 : 0;
                _TecAll.Company = Session["UserCompanyCode"].ToString();
                _TecAll.Location = Session["UserDefLoca"].ToString();
                _TecAll.LineNo = Convert.ToInt16((row.FindControl("col_TLineNo") as Label).Text);
                _TecAll.sessionCreateBy = Session["UserID"].ToString();
                _TecAll.sessionCreateDate = DateTime.Now;
                _TecAll.sessionModBy = Session["UserID"].ToString();
                _TecAll.sessionModDate = DateTime.Now;
                _TecAll.SessionId = Session["SessionID"].ToString();
                _TecAll.TechCode = txtTechCode.Text;// ((row.FindControl("col_TTechCode") as Label).Text).Substring(0, ((row.FindControl("col_TTechCode") as Label).Text).IndexOf("-")).Trim();
                _TecAll.STH_TP = "J";
                _TecAll.TownNo = (row.FindControl("col_Town") as Label).Text;
                //_TecAll.AlcoNo = autoGeNo;//rootAutoNo.Rows[0]["AUT_NUMBER"].ToString();
                //Int16 val = UpdateAutoNo();
                _TechAllocation.Add(_TecAll);
            }
            try
            {
                row_aff = (Int32)CHNLSVC.Inventory.saveUpdate_TechnicianAllocation(_TechAllocation, GenerateMasterAutoNumber(), out doc, out errMsg);
            }
            catch (Exception ex)
            {
                if (errMsg != string.Empty) ErrorBinMsg(errMsg);
                else ErrorBinMsg(ex.Message);
            }
            if (row_aff >= 1)
            {
                SuccessBinMsg("Successfully Saved The Technician Allocation Details...! - "+doc);
                TechAlloClean();
            }
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString(); 
            masterAuto.Aut_direction = 0;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "TECH";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "TECH";
            masterAuto.Aut_year = DateTime.Now.Year;
            return masterAuto;
        }

        protected void lbtnTechAlloClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
                TechAlloClean();
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void TechAlloClean()
        {
            grdJobDeatils.DataSource = null;
            grdJobDeatils.DataBind();
            grdTechnician.DataSource = null;
            grdTechnician.DataBind();
            txtModel.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtEngine.Text = string.Empty;
            txtchassi.Text = string.Empty;
            txtJob.Text = string.Empty;
            txtAod.Text = string.Empty;
            //txtFromDate.Text = string.Empty;
            //txtToDate.Text = string.Empty;
            txtTDateFrom.Text = string.Empty;
            txtTDateTo.Text = string.Empty;
            chkNotAllocated.Checked = true;
            ViewState["StoreTA"] = null;
            txtTechCode.Text = string.Empty;
            txtTechName.Text = string.Empty;
            //txtFromDate.Text = DateTime.Now.ToShortDateString();
            //txtToDate.Text = DateTime.Now.ToShortDateString();
        }

        protected void parameterValidate()
        {
            _serData = new DataTable();
            if (txtEngine.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EngineNo);
                _serData = CHNLSVC.CommonSearch.GetAllEngineNos(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtEngine.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid engine no");
                    return;
                }

                //foreach (string item in _serData.Rows)
                //{
                //    txtEngine.Text = (from DataRow dr in _serData.Rows select dr["CODE"]).FirstOrDefault().ToString();
                //}
            }
            if (txtJob.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, null, null, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);
                bool contains = _serData.AsEnumerable().Any(row => txtJob.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid job no");
                    return;
                }
            }
            if (txtchassi.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChassiNo);
                _serData = CHNLSVC.CommonSearch.GetAllChassiNos(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtchassi.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid chassis no");
                    return;
                }
            }
            if (txtItem.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtItem.Text == row.Field<String>("ITEM"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid Item");
                    return;
                }
            }
            if (txtAod.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.aodNo);
                _serData = CHNLSVC.CommonSearch.GetAllAodNos(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtAod.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid AOD no");
                    return;
                }
            }
            if (txtTechCode.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Technician);
                _serData = CHNLSVC.CommonSearch.GetAllTechnician(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtTechCode.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid technisian number");
                    return;
                }
            }
            if (txtModel.Text != "")
            {
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.GetAllModels(_para, null, null);
                bool contains = _serData.AsEnumerable().Any(row => txtModel.Text == row.Field<String>("CODE"));
                if (contains == false)
                {
                    ErrorBinMsg("Please select a valid model no");
                    return;
                }
            }
        }

        protected void lbtnDetaltecost_Click(object sender, EventArgs e)
        {
            try
            {
                List<TechAllocation> crdNote = new List<TechAllocation>();
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    crdNote = (List<TechAllocation>)ViewState["StoreTA"];
                    if (crdNote.Count > 0)
                    {
                        GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                        Int32 rowIndex = dr.RowIndex;
                        Label lblItem = grdTechnician.Rows[rowIndex].FindControl("col_TJobNo") as Label;
                        ViewState["VSItem"] = lblItem.Text;
                        var rmvItm = crdNote.Find(r => r.JobNo == lblItem.Text);//Single
                        crdNote.Remove(rmvItm);
                        grdTechnician.DataSource = crdNote;
                        grdTechnician.DataBind();
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();

                //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                //_serData = CHNLSVC.CommonSearch.GetServiceJobs(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, null, null);

                grdResultD.DataSource = _serData;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_serData);
                UserDPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
            }
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();

                //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                //_serData = CHNLSVC.CommonSearch.GetServiceJobs(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);

                grdResultD.DataSource = _serData;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_serData);
                UserDPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = grdResultD.SelectedRow.Cells[1].Text;
            txtJob.Text = ID;
            lblvalue.Text = "";
            txtSearchbyword.Text = string.Empty;
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            _serData = new DataTable();
            //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //_serData = CHNLSVC.CommonSearch.GetServiceJobs(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
            _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, txtFDate.Text == "" ? null : txtFDate.Text, txtTDate.Text == "" ? null : txtTDate.Text);

            grdResultD.DataSource = _serData;
            grdResultD.DataBind();
            UserDPopoup.Show();
            lblvalue.Text = "JobNo";
            txtSearchbyword.Focus();
        }

        protected void grdJobDeatils_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdJobDeatils.PageIndex = e.NewPageIndex;
            _serData = new DataTable();
            _serData = (DataTable)ViewState["SEARCH"];
            grdJobDeatils.DataSource = _serData;
            grdJobDeatils.DataBind();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = string.Empty;
                DummyDataBind();
                _serData = new DataTable();

                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                _serData = CHNLSVC.CommonSearch.GetAllJobNos(_para, ddlSearchbykeyD.SelectedItem.Text, null, txtFDate.Text, txtTDate.Text);

                grdResultD.DataSource = _serData;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_serData);
                UserDPopoup.Show();
                lblvalue.Text = "JobNo";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
            }
        }
    }
}