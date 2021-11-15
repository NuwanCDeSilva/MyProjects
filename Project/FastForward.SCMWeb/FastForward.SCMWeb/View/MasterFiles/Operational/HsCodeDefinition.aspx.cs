using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class HsCodeDefinition : BasePage
    {
        string _userid = string.Empty;        
        private DataTable _dtSearch
        {
            get { if (Session["_dtSearch"] != null) { return (DataTable)Session["_dtSearch"]; } else { return new DataTable(); } }
            set { Session["_dtSearch"] = value; }
        }
        private DataTable _dtSearchAll
        {
            get { if (Session["_dtSearchAll"] != null) { return (DataTable)Session["_dtSearchAll"]; } else { return new DataTable(); } }
            set { Session["_dtSearchAll"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    MasterCompany COM = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    txtCompnay.Text = Session["UserCompanyCode"].ToString();
                    txtToCountry.Text = "";
                    lblToCountry.Text = "";
                    txtFromCountry.Text = "DEF";
                    txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                    txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
                    List<MasterCompany> ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
                    if (ListCom != null)
                    {
                        var company = ListCom.Where(c => c.Mc_cd == Session["UserCompanyCode"].ToString()).FirstOrDefault();
                        if (company!=null)
                        {
                            lblUCur.Text = company.Mc_cur_cd;
                            lblWCurr.Text = company.Mc_cur_cd;
                        }
                    }
                    LoadGridData();
                    if (COM != null)
                    {
                        txtToCountry.Text = COM.Mc_anal19;
                        DataTable _dt = CHNLSVC.CommonSearch.LoadCountryText(COM.Mc_anal19);
                        foreach (DataRow row in _dt.Rows)
                        {
                            lblToCountry.Text = row["mcu_desc"].ToString();
                        }
                        BindEntryType(COM.Mc_anal19);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void BindEntryType(string country)
        {
            ddlEntryType.DataSource = null;
            ddlEntryType.DataBind();
            DataTable dtEntryType = CHNLSVC.CommonSearch.GetHsCodeEntryType(Session["UserCompanyCode"].ToString());
            if (dtEntryType != null)
            {
                ddlEntryType.DataSource = dtEntryType;
                ddlEntryType.DataValueField = "rcut_desc";
                ddlEntryType.DataTextField = "RCUT_TP";
                ddlEntryType.DataBind();
            }
            ddlEntryType.SelectedIndex = ddlEntryType.Items.IndexOf(ddlEntryType.Items.FindByText("ALL"));
            ddlEntryType_SelectedIndexChanged(null,null);
        }


        private void BindGridData()
        {
            try
            {
                dgvAddDuty.DataSource = null;
                dgvAddDuty.DataBind();
                dgvAddDuty.DataSource = LoadDutyData();
                dgvAddDuty.DataBind();

                dgvDutyClaimable.DataSource = null;
                dgvDutyClaimable.DataBind();
                dgvDutyClaimable.DataSource = LoadClaimData();
                dgvDutyClaimable.DataBind();


                dgvCountryBU.DataSource = null;
                dgvCountryBU.DataBind();
                dgvCountryBU.DataSource = LoadDutyBreakUp();
                dgvCountryBU.DataBind();

                dgvClaimBU.DataSource = null;
                dgvClaimBU.DataBind();
                dgvClaimBU.DataSource = LoadClaimBreakUp();
                dgvClaimBU.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void LoadGridData()
        {
            try
            {
                dgvAddDuty.DataSource = null;
                DataTable dtDuty = new DataTable();
                dtDuty = CHNLSVC.CommonSearch.GetHsCodeDutyTypes(Session["UserCompanyCode"].ToString());
                if (dtDuty.Rows.Count > 0)
                {
                    dgvAddDuty.DataSource = dtDuty;
                }
                dgvAddDuty.DataBind();

                dgvAddDuty.DataSource = null;
                DataTable dtClaim = new DataTable();
                dtClaim = CHNLSVC.CommonSearch.GetHsClaimDutyTypes(Session["UserCompanyCode"].ToString());
                if (dtClaim.Rows.Count > 0)
                {
                    dgvDutyClaimable.DataSource = dtClaim;
                }
                dgvDutyClaimable.DataBind();
                dgvCountryBU.DataBind();
                dgvClaimBU.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private DataTable LoadDutyData()
        {
            DataTable dt = new DataTable();
            HsCode hsCode = new HsCode()
            {
                Mhc_frm_cnty = txtFromCountry.Text,
                Mhc_to_cnty = txtToCountry.Text,
                Mhc_hs_cd = txtHsCode.Text.Trim(),
                Mhc_tp=ddlEntryType.SelectedValue
                //,                Mhc_cost_cat = "CUSTM"
                //,
                //Mhc_cost_tp = "DUTY"
            };
            dt = CHNLSVC.CommonSearch.SearchHsDefinitionDuty(hsCode);
           
            return dt;
        }
        private DataTable LoadClaimData()
        {
            DataTable dt = new DataTable();
            HsCodeClaim hsClaimCode = new HsCodeClaim()
            {
                Mhcl_frm_cnty = txtFromCountry.Text,
                Mhcl_to_cnty = txtToCountry.Text,
                Mhcl_hs_cd = txtHsCode.Text.Trim(),
                Mhcl_com=txtCompnay.Text.ToUpper(),
                Mhcl_cost_cat = "CUSTM",
                Mhcl_cost_tp = "DUTY"
            };
           
            DataTable dtt = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_frm_cnty"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_to_cnty"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_hs_cd"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_tp"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_com"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_cost_cat"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mhcl_cost_tp"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "CODE"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.Decimal"); col.ColumnName = "ClaimPrecentage"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "mp"; dtt.Columns.Add(col);
            col = new DataColumn(); col.DataType = System.Type.GetType("System.String"); col.ColumnName = "Description"; dtt.Columns.Add(col);
            dt = CHNLSVC.CommonSearch.SearchHsDefinitionClaim(hsClaimCode);
            DataTable dtClaim = CHNLSVC.CommonSearch.GetHsClaimDutyTypes(Session["UserCompanyCode"].ToString());
            foreach (DataRow rw in dtClaim.Rows)
            {
                DataRow row = dtt.NewRow();
                row["CODE"] = rw["CODE"].ToString();
                row["Description"] = rw["Description"].ToString();
                row["ClaimPrecentage"] = rw["ClaimPrecentage"].ToString();
                row["mhcl_com"] = rw["MCAE_COM"].ToString();
                //row["mhcl_to_cnty"] = rw["mhcl_to_cnty"].ToString();
                //row["mhcl_hs_cd"] = rw["mhcl_hs_cd"].ToString();
                row["mhcl_cost_cat"] = rw["mcae_ele_cat"].ToString();
                row["mhcl_cost_tp"] = rw["mcae_ele_tp"].ToString();
                row["mp"] = rw["mp"].ToString();
                string code = rw["CODE"].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CODE"].ToString() == code)
                    {
                        row["mhcl_frm_cnty"] = dr["mhcl_frm_cnty"].ToString();
                        row["mhcl_to_cnty"] = dr["mhcl_to_cnty"].ToString();
                        row["mhcl_hs_cd"] = dr["mhcl_hs_cd"].ToString();
                        row["mhcl_tp"] = dr["mhcl_tp"].ToString();
                        row["mhcl_com"] = dr["mhcl_com"].ToString();
                        row["mhcl_cost_cat"] = dr["mhcl_cost_cat"].ToString();
                        row["mhcl_cost_tp"] = dr["mhcl_cost_tp"].ToString();
                        row["ClaimPrecentage"] = dr["ClaimPrecentage"].ToString();
                        row["mp"] = dr["mp"].ToString();
                        break;
                    } 
                }
                dtt.Rows.Add(row);
            }
            return dtt;
        }

        private DataTable LoadDutyBreakUp()
        {
            DataTable dt = CHNLSVC.CommonSearch.GetHsDutyBU(txtToCountry.Text, txtHsCode.Text.Trim());
            return dt;
        }
        private DataTable LoadClaimBreakUp()
        {
            DataTable dt = CHNLSVC.CommonSearch.GetHsClaimBU(txtToCountry.Text, txtHsCode.Text.Trim());
            return dt;
        }

        private void LoadHsData()
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
            DataTable _result = CHNLSVC.CommonSearch.GetHsCodeData(txtHsCode.Text.Trim());
            int c = 0;
            foreach (DataRow row in _result.Rows)
            {
                if (c == 0)
                {
                    txtHsDes.Text = row["MHC_HS_DESC"].ToString();
                    ddlEntryType.SelectedIndex = ddlEntryType.Items.IndexOf(ddlEntryType.Items.FindByText(row["MHC_TP"].ToString()));
                   // ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(row["MHC_ACT"].ToString()));
                    txtFromCountry.Text = row["mhc_frm_cnty"].ToString();
                    txtFromCountry_TextChanged(null, null); ddlEntryType_SelectedIndexChanged(null, null);
                } 
                c++;
            }
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                //case CommonUIDefiniton.SearchUserControlType.HsCla:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HsCode:
                    {
                        string toCountry = "";
                        MasterCompany COM = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                        if (COM != null)
                        {
                            toCountry = COM.Mc_anal19;
                        }
                      //  paramsText.Append(toCountry + seperator + txtFromCountry + seperator + txtHsCode.Text + seperator);
                        paramsText.Append(toCountry);
                        break;
                    }

                   default:
                    break;
            }
            return paramsText.ToString();
        }
        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "ImportAgent")
                {
                    _result = (DataTable)Session["ImportAgent"];
                }
                if (lblSearchType.Text == "HsCode")
                {
                    _result = (DataTable)Session["HsCode"];
                }
                if (lblSearchType.Text == "Company")
                {
                    _result = (DataTable)Session["Company"];
                } 
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                }
                else
                {
                    dgvResult.DataSource = null;
                } 
                dgvResult.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                PopupSearch.Show();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                //     "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResult.SelectedRow.Cells[1].Text;
                string desc = dgvResult.SelectedRow.Cells[2].Text;

                if (lblSearchType.Text == "ImportAgent")
                {
                   txtFromCountry.Text = code;
                   lblFromCountry.Text = desc;
                   txtFromCountry_TextChanged(null,null);
                }
                else if (lblSearchType.Text == "Country")
                {
                    txtCompnay.Text = code;
                    
                }
                else if (lblSearchType.Text == "HsCode")
                {
                    txtHsCode.Text = code;
                    txtHsCode.ToolTip = desc;
                    txtHsCode_TextChanged(null, null);
                } 
                if (lblSearchType.Text == "Company")
                {
                    txtCompnay.Text = code;
                    txtCompnay_TextChanged(null, null);
                } 
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = null;
                Session["ImportAgent"] = null;
                Session["HsCode"] = null;
                Session["Country"] = null;
                Session["Company"] = null;
                
                if (lblSearchType.Text == "ImportAgent")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    _dt = CHNLSVC.CommonSearch.GetAllCountryData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ImportAgent"] = _dt;
                }
                if (lblSearchType.Text == "Country")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _dt = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Country"] = _dt;
                }
                if (lblSearchType.Text == "HsCode")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                    _dt = CHNLSVC.CommonSearch.SearchGetHsCode(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["HsCode"] = _dt;
                }
                if (lblSearchType.Text == "Company")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _dt = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Company"] = _dt;
                }
                dgvResult.DataSource = null;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_dt.Rows.Count > 0)
                {
                    dgvResult.DataSource = _dt;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeHsCode_Click(object sender, EventArgs e)
        {
            lblSearchType.Text = "HsCode";
            Session["HsCode"] = null;
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
            DataTable _result = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
            if (_result.Rows.Count > 0)
            {
                dgvResult.DataSource = _result;
                Session["HsCode"] = _result;
                BindUCtrlDDLData(_result);
            }
            else
            {
                dgvResult.DataSource = null;
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void lbtnSeFromCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ImportAgent";
                Session["ImportAgent"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetAllCountryData(para, null, null);
                dgvResult.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    //foreach (DataRow rw in _result.Rows)
                    //{
                    //    if (rw["Code"].ToString() == txtToCountry.Text)
                    //    {
                    //        rw.Delete();
                    //    }
                    //}
                    dgvResult.DataSource = _result;
                    Session["ImportAgent"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Company";
                Session["Company"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                dgvResult.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Company"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void txtHsCode_TextChanged(object sender, EventArgs e)
        {
            Clear();
            if (txtHsCode.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtHsCode.Text.Trim() == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                txtHsCode.ToolTip = b2 ? toolTip : "";
                if (b2)
                {
                    LoadHsData();
                    BindGridData();
                }
                else
                { //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid hs code !!!')", true);
                    //txtHsCode.Text = "";
                    //txtHsCode.Focus();
                    //return;
                  
                }
            }
            else
            {
                txtHsCode.ToolTip = "";
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
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if (hdfConfirmSave.Value == "Yes")
                {
                    if (string.IsNullOrEmpty(txtHsCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the hs code !!!')", true);
                        txtHsCode.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtHsDes.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the hs description !!!')", true);
                        txtHsDes.Focus();
                        return;
                    }
                    if (ddlEntryType.SelectedIndex<1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the entry type !!!')", true);
                        txtHsDes.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtFromCountry.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the from country !!!')", true);
                        txtHsDes.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtToCountry.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the to country !!!')", true);
                        txtHsDes.Focus();
                        return;
                    }
                    //if (ddlStatus.SelectedIndex < 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select status !!!')", true);
                    //    txtHsDes.Focus();
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtCompnay.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the company !!!')", true);
                        txtCompnay.Focus();
                        return;
                    }
                    int r = 0;
                    string msg="";
                    foreach (GridViewRow row in dgvAddDuty.Rows)
                    {
                        r++;
                        TextBox rate = (TextBox)row.FindControl("txtAdRate");
                        TextBox unitPrice = (TextBox)row.FindControl("txtAdUnitPrice");
                        TextBox weightPrice = (TextBox)row.FindControl("txtAdWeightPrice");
                        if (ConvertDesZero(rate.Text) > 100){
                            msg = "Maximum amount allowed is 100";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            return;
                        }
                        if (!(ConvertDesZero(unitPrice.Text) < 1000000))
                        {
                            msg = "Maximum unit price allowed is 999,999,00";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            return;
                        }
                        if (!(ConvertDesZero(weightPrice.Text) < 1000000))
                        {
                            msg = "Maximum weight price allowed is 999,999,00";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            return;
                        }
                    }
                    List<HsCode> hsCodeList=new List<HsCode>();
                   
                    foreach (GridViewRow row in dgvAddDuty.Rows)
                    {
                        HsCode hsCode = new HsCode();
                        
                        _userid = (string)Session["UserID"];
                        Label lblAdType = (Label)row.FindControl("lblAdType");
                        TextBox rate = (TextBox)row.FindControl("txtAdRate");
                        TextBox unitPrice = (TextBox)row.FindControl("txtAdUnitPrice");
                        TextBox weightPrice = (TextBox)row.FindControl("txtAdWeightPrice");
                        CheckBox ddlMP = (CheckBox)row.FindControl("ddlAdMp");
                        CheckBox chkDutyActive = (CheckBox)row.FindControl("chkDutyActive");
                        TextBox cce = (TextBox)row.FindControl("txtcc");
                        TextBox cceclm = (TextBox)row.FindControl("txtccclm");

                        hsCode.Mhc_frm_cnty = txtFromCountry.Text;
                        hsCode.Mhc_to_cnty = txtToCountry.Text;
                        hsCode.Mhc_hs_cd = txtHsCode.Text.Trim();
                        hsCode.Mhc_hs_desc = txtHsDes.Text;
                        hsCode.Mhc_tp = ddlEntryType.SelectedItem.Text;
                        hsCode.Mhc_cost_cat = "CUSTM";
                        hsCode.Mhc_cost_tp = "DUTY";
                        hsCode.Mhc_cost_ele = lblAdType.Text;
                        hsCode.Mhc_cost_ele_amt =string.IsNullOrEmpty(rate.Text)?0: Convert.ToDecimal(rate.Text);
                        hsCode.Mhc_custom__price = string.IsNullOrEmpty(unitPrice.Text) ? 0 : Convert.ToDecimal(unitPrice.Text);
                        hsCode.Mhc_weight_price = string.IsNullOrEmpty(weightPrice.Text) ? 0 : Convert.ToDecimal(weightPrice.Text);
                        hsCode.Mhc_mp =Convert.ToInt32(ddlMP.Checked);
                        //
                        hsCode.Mhc_act = Convert.ToInt32(chkDutyActive.Checked);
                        hsCode.Mhc_cre_by = _userid;
                        hsCode.Mhc_cre_dt = CHNLSVC.Security.GetServerDateTime();
                        //
                        hsCode.Mhc_cre_session =Session["SessionID"].ToString();
                        hsCode.Mhc_mod_by = _userid;
                        hsCode.Mhc_mod_dt = CHNLSVC.Security.GetServerDateTime();
                        hsCode.Mhc_mod_session = Session["SessionID"].ToString();
                        hsCode.Mhc_cce = string.IsNullOrEmpty(cce.Text) ? 0 : Convert.ToDecimal(cce.Text);
                        hsCode.Mhc_cce_clm = string.IsNullOrEmpty(cceclm.Text) ? 0 : Convert.ToDecimal(cceclm.Text);
                        hsCodeList.Add(hsCode);
                    }
                    
                    List<HsCodeClaim> hsClaimList=new List<HsCodeClaim>();
                    foreach (GridViewRow row in dgvDutyClaimable.Rows)
                    {
                        HsCodeClaim hsClaim = new HsCodeClaim();
                       _userid = (string)Session["UserID"];
                        Label lblDuType = (Label)row.FindControl("lblDuType");
                        TextBox txtDuClaim = (TextBox)row.FindControl("txtDuClaim");
                        CheckBox ddlMP = (CheckBox)row.FindControl("ddlDuMp");

                        hsClaim.Mhcl_frm_cnty = "DEF";
                        hsClaim.Mhcl_to_cnty=txtToCountry.Text;
                        hsClaim.Mhcl_hs_cd = txtHsCode.Text.Trim();
                        hsClaim.Mhcl_tp=ddlEntryType.SelectedValue;
                        hsClaim.Mhcl_com=txtCompnay.Text.ToUpper();
                        hsClaim.Mhcl_cost_cat="CUSTM";
                        hsClaim.Mhcl_cost_tp="DUTY";
                        hsClaim.Mhcl_cost_ele = lblDuType.Text;
                        hsClaim.Mhcl_claim_rt = string.IsNullOrEmpty(txtDuClaim.Text) ? 0 : Convert.ToDecimal(txtDuClaim.Text);
                        hsClaim.Mhcl_act=Convert.ToInt32(ddlMP.Checked);
                        hsClaim.Mhcl_cre_by=_userid;
                        hsClaim.Mhcl_cre_dt=CHNLSVC.Security.GetServerDateTime();
                        hsClaim.Mhcl_cre_session = Session["SessionID"].ToString();
                        hsClaim.Mhcl_mod_by=_userid;
                        hsClaim.Mhcl_mod_dt=CHNLSVC.Security.GetServerDateTime();
                        hsClaim.Mhcl_mod_session = Session["SessionID"].ToString();
                        hsClaimList.Add(hsClaim);
                    }
                    #region Coment
                    //foreach (GridViewRow row in dgvDutyClaimable.Rows)
                    //{
                    //    HsCodeClaim hsClaim = new HsCodeClaim();
                    //    _userid = (string)Session["UserID"];
                    //    Label lblDuType = (Label)row.FindControl("lblDuType");
                    //    TextBox txtDuClaim = (TextBox)row.FindControl("txtDuClaim");
                    //    DropDownList ddlMP = (DropDownList)row.FindControl("ddlDuMp");
                    //    hsClaim.Mhcl_frm_cnty = txtFromCountry.Text;
                    //    hsClaim.Mhcl_to_cnty = txtToCountry.Text;
                    //    hsClaim.Mhcl_hs_cd = txtHsCode.Text;
                    //    hsClaim.Mhcl_tp = ddlEntryType.SelectedValue;
                    //    hsClaim.Mhcl_com = txtCompnay.Text;
                    //    hsClaim.Mhcl_cost_cat = "CUSTM";
                    //    hsClaim.Mhcl_cost_tp = "DUTY";
                    //    hsClaim.Mhcl_cost_ele = lblDuType.Text;
                    //    hsClaim.Mhcl_claim_rt = string.IsNullOrEmpty(txtDuClaim.Text) ? 0 : Convert.ToDecimal(txtDuClaim.Text);
                    //    hsClaim.Mhcl_act = Convert.ToInt32(ddlMP.SelectedValue);
                    //    hsClaim.Mhcl_cre_by = _userid;
                    //    hsClaim.Mhcl_cre_dt = CHNLSVC.Security.GetServerDateTime();
                    //    hsClaim.Mhcl_cre_session = Session["SessionID"].ToString();
                    //    hsClaim.Mhcl_mod_by = _userid;
                    //    hsClaim.Mhcl_mod_dt = CHNLSVC.Security.GetServerDateTime();
                    //    hsClaim.Mhcl_mod_session = Session["SessionID"].ToString();
                    //    hsClaimList.Add(hsClaim);
                    //}
                    #endregion

                    #region Restrict Hs Claimable Data
                    bool _hsFromCntry = false;
                    bool _hsToCntry = false;
                    bool _hsCd = false;
                    bool _hsTp = false;
                    bool _hsCom = false;
                    foreach (var item in hsClaimList)
                    {
                        if (string.IsNullOrEmpty(item.Mhcl_frm_cnty))
                        {
                            _hsFromCntry = true;
                            break;
                        }
                        if (string.IsNullOrEmpty(item.Mhcl_to_cnty))
                        {
                            _hsToCntry = true;
                            break;
                        }
                        if (string.IsNullOrEmpty(item.Mhcl_hs_cd))
                        {
                            _hsCd = true;
                            break;
                        }
                        if (string.IsNullOrEmpty(item.Mhcl_tp))
                        {
                            _hsTp = true;
                            break;
                        }
                        if (string.IsNullOrEmpty(item.Mhcl_com))
                        {
                            _hsCom = true;
                            break;
                        }
                    }
                    if (_hsFromCntry)
                    {
                        DispMsg("Plese check the data. HS code from country is unavailable !"); return;
                    }
                    if (_hsToCntry)
                    {
                        DispMsg("Plese cheak the data. Hs code to country is unavailable !"); return;
                    }
                    if (_hsCd)
                    {
                        DispMsg("Plese cheak the data. Hs code is unavailable !"); return;
                    }
                    if (_hsTp)
                    {
                        DispMsg("Plese cheak the data. Hs code type unavailable !"); return;
                    }
                    if (_hsCom)
                    {
                        DispMsg("Plese cheak the data. Hs code company is unavailable !"); return;
                    }
                    #endregion
                    Int32 _effectHS = 0;
                    Int32 _effectHSClaim = 0;

                    _effectHS = CHNLSVC.General.SaveHsCode(hsCodeList);
                    _effectHSClaim = CHNLSVC.General.SaveHsCodeClaim(hsClaimList);
                    if (_effectHS != -1 && _effectHSClaim!=-1)
                    {
                        txtHsCode.Text = "";
                        Clear();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('HS details saved successfully !!!')", true);
                    }
                    else if (_effectHS == -1 && _effectHSClaim != -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                    else if (_effectHS != -1 && _effectHSClaim == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void txtFromCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFromCountry.Text == txtToCountry.Text)
                {
                    txtFromCountry.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Both from country and to country cannot be same !!!')", true);
                    return;
                }
                if (txtFromCountry.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllCountryData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtFromCountry.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                //return;
                            }
                        }
                    }
                    txtFromCountry.ToolTip = b2 ? toolTip : "";
                    if (b2)        
                    {
                        lblFromCountry.Text = toolTip;
                        ////////
                        bool b3 = false;
                        string p = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                        DataTable r = CHNLSVC.CommonSearch.SearchGetHsCode(p, null, null);
                        foreach (DataRow row in r.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtHsCode.Text.Trim() == row["Code"].ToString())
                                {
                                    b3 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b3)
                        {
                            dgvAddDuty.DataSource = null;
                            dgvAddDuty.DataBind();

                            DataTable dtDuty = LoadDutyData();
                            if (dtDuty.Rows.Count > 0)
                            {
                                dgvAddDuty.DataSource = dtDuty;
                            }
                            else
                            {
                                DataTable dtDutyEmpty = new DataTable();
                                dtDutyEmpty = CHNLSVC.CommonSearch.GetHsCodeDutyTypes(Session["UserCompanyCode"].ToString());
                                dgvAddDuty.DataSource = dtDutyEmpty;
                            }
                            dgvAddDuty.DataBind();

                            dgvDutyClaimable.DataSource = null;
                            dgvDutyClaimable.DataBind();
                            DataTable dtClaim = LoadClaimData();
                            if (dtClaim.Rows.Count > 0)
                            {
                                dgvDutyClaimable.DataSource = dtClaim;
                            }
                            else
                            {
                                DataTable dtClaimEmpty = new DataTable();
                                dtClaimEmpty = CHNLSVC.CommonSearch.GetHsClaimDutyTypes(Session["UserCompanyCode"].ToString());
                                dgvDutyClaimable.DataSource = dtClaimEmpty;
                            }
                            dgvDutyClaimable.DataBind();
                        }
                        //////////
                       
                    }
                    else
                    {
                       txtFromCountry.Text = "";
                        txtFromCountry.Focus();
                        LoadGridData();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid from country !!!')", true);
                        //DisplayMessageJS("test");
                        return;
                    }
                }
                else
                {
                    txtFromCountry.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {  
                txtHsCode.Text = "";
                Clear();
                Response.Redirect(Request.RawUrl);
            }
        }

        private void Clear()
        {
            txtHsDes.Text = "";
            txtFromCountry.Text = "DEF";
            lblFromCountry.Text = "";
            ddlEntryType.SelectedIndex = ddlEntryType.Items.IndexOf(ddlEntryType.Items.FindByText("ALL"));
            //ddlStatus.SelectedIndex = 0;
            LoadGridData();
        }

        protected void dgvAddDuty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ddlAdMp = (e.Row.FindControl("ddlAdMp") as CheckBox);
                string status = (e.Row.FindControl("lblAdMp") as Label).Text;
                if (status == "1")
                    ddlAdMp.Checked = true;
                else
                {
                    ddlAdMp.Checked = false;
                }
                CheckBox chkDutyActive = (CheckBox)e.Row.FindControl("chkDutyActive");
                Label lblDutyActive = (Label)e.Row.FindControl("lblDutyActive");
                if (lblDutyActive.Text == "1")
                    chkDutyActive.Checked = true;
                else
                {
                    chkDutyActive.Checked = false;
                }
            }
        }

        protected void dgvDutyClaimable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ddlDuMp = (e.Row.FindControl("ddlDuMp") as CheckBox);
                string status = (e.Row.FindControl("lblDuMp") as Label).Text;
                if(status=="1")
                    ddlDuMp.Checked = true;
                else
                {
                    ddlDuMp.Checked = false;
                }
            }
        }

        protected void txtCompnay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCompnay.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCompnay.Text.Trim().ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                //return;
                            }
                        }
                    }
                    txtCompnay.ToolTip = b2 ? toolTip : "";
                    if (b2)
                    {
                       // lblFromCountry.Text = toolTip;
                        dgvDutyClaimable.DataSource = null;
                        dgvDutyClaimable.DataBind();
                        DataTable dtClaim = new DataTable();
                        if (!string.IsNullOrEmpty(txtHsCode.Text) && !string.IsNullOrEmpty(txtFromCountry.Text) && !string.IsNullOrEmpty(txtCompnay.Text.ToUpper()))
                        { dtClaim = LoadClaimData(); }
                        if (dtClaim.Rows.Count > 0)
                        {
                            dgvDutyClaimable.DataSource = dtClaim;
                        }
                        else
                        {
                            DataTable dtClaimEmpty = new DataTable();
                            dtClaimEmpty = CHNLSVC.CommonSearch.GetHsClaimDutyTypes(Session["UserCompanyCode"].ToString());
                            dgvDutyClaimable.DataSource = dtClaimEmpty;
                        }
                        dgvDutyClaimable.DataBind();
                        List<MasterCompany> ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
                        if (ListCom != null)
                        {
                            var company = ListCom.Where(c => c.Mc_cd == Session["UserCompanyCode"].ToString()).FirstOrDefault();
                            if (company != null)
                            {
                                lblUCur.Text = company.Mc_cur_cd;
                                lblWCurr.Text = company.Mc_cur_cd;
                            }
                        }
                    }
                    else
                    {
                        txtCompnay.Text = "";
                        txtCompnay.Focus();
                        LoadGridData();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid company !!!')", true);
                        //DisplayMessageJS("test");
                        return;
                    }
                }
                else
                {
                    txtCompnay.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                //CHNLSVC.CloseChannel();
            }
        }

        protected void ddlEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblEntryTy.Text = "";
                if (ddlEntryType.SelectedIndex>0)
                {
                    lblEntryTy.Text = ddlEntryType.SelectedValue;
                    if (string.IsNullOrEmpty(txtHsCode.Text))
                    {
                        LoadGridData();
                    }
                    else
                    {
                        BindGridData();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnEditClaim_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                string comCode = (row.FindControl("lblComCd") as Label).Text;
                if (!string.IsNullOrEmpty(comCode))
                {
                    txtCompnay.Text = comCode;
                    txtCompnay_TextChanged(null,null);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnEditDuty_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                string frCode = (row.FindControl("lblCoFromCountry") as Label).Text;
                if (!string.IsNullOrEmpty(frCode))
                {
                    txtFromCountry.Text = frCode;
                    txtFromCountry_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        private decimal ConvertDesZero(string val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private Int32 ConvertIntZero(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        protected void lbtnPopUpSearch_Click(object sender, EventArgs e)
        {
            lblErr.Visible = false;
            txtHSCodepopup.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEntrymodal.Text = string.Empty;
            GridViewSearch.DataSource = null;
            GridViewSearch.DataBind();
           // txtItemRange.Text = "";
            //lbtnClearItemData_Click(null, null);
            popUpSearchModel.Show();
            Session["SearchPopUp"] = "Show";
        }
        protected void lbtPopDocClose_Click(object sender, EventArgs e)
        {
            popUpSearchModel.Hide();
            Session["SearchPopUp"] = "Hide";
            
        }
        protected void entry_search(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "cusdecEntry";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                _dtSearch = CHNLSVC.CommonSearch.SearchCusdecHeaderHS(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                popUpSearchModel.Show();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void hs_searchModal(object sender, EventArgs e)
        {
        lblSearchType.Text = "HsCodemodal";

//            Session["HsCode"] = null;
            _dtSearch = new DataTable();
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
             _dtSearch = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
            if (_dtSearch.Rows.Count > 0)
            {
                dgvSearch.DataSource = _dtSearch;
               // Session["HsCode"] = _dtSearch;
                BindUCtrlDDLData(_dtSearch);
            }
            else
            {
                dgvSearch.DataSource = null;
            }
            dgvSearch.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            ItemPopup.Show();
            popUpSearchModel.Show();
            if (dgvSearch.PageIndex > 0)
            { dgvSearch.SetPageIndex(0); }
    }
        protected void dgvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvSearch.SelectedRow.Cells[1].Text;
                popUpSearchModel.Show();
                if (lblSearchType.Text == "cusdecEntry")
                {
                    txtEntrymodal.Text = code;
                }
                if (lblSearchType.Text == "HsCodemodal")
                {
                    txtHSCodepopup.Text = code;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void dgvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSearch.PageIndex = e.NewPageIndex;
            dgvSearch.DataSource = new int[] { };
            if (_dtSearch != null)
            {
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                }
            }
            dgvSearch.DataBind();
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
            ItemPopup.Show();
        }

        public void BindUCtrlDdlSeByKey(DataTable _dataSource)
        {
            this.ddlSeByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSeByKey.Items.Add(col.ColumnName);
            }

            this.ddlSeByKey.SelectedIndex = 0;
        }
        protected void lbtnSearchPopUp_Click(object sender, EventArgs e)
        {
            _dtSearch = new DataTable();
            string para = "";
            if (lblSearchType.Text == "cusdecEntry")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                _dtSearch = CHNLSVC.CommonSearch.SearchCusdecHeaderHS(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "HsCodemodal")
            {
                 para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                 _dtSearch = CHNLSVC.CommonSearch.SearchGetHsCode(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);                
            }
            else
            {
                _dtSearch = new DataTable();
            }
            dgvSearch.DataSource = new int[] { };
            if (_dtSearch.Rows.Count > 0)
            {
                dgvSearch.DataSource = _dtSearch;
                BindUCtrlDdlSeByKey(_dtSearch);
            }
            dgvSearch.DataBind();
            txtSearch.Text = "";
            txtSearch.Focus();
            ItemPopup.Show();
            if (dgvSearch.PageIndex > 0)
            { dgvSearch.SetPageIndex(0); }
        }
        protected void btnSearchAllClick(object sender, EventArgs e)
        {
            
            DateTime fromDate = Convert.ToDateTime(txtFrom.Text);
            DateTime toDate = Convert.ToDateTime(txtTo.Text);
            string description = "%"+txtDescription.Text+"%";
            if (fromDate > toDate)
            {
                DispMsg("Date Range is Invalid!!!", "W");
                //return;
            }
            else
            {
                _dtSearchAll = CHNLSVC.CommonSearch.GerHsSearchDetails(Session["UserCompanyCode"].ToString(), txtEntrymodal.Text, "%" + txtHSCodepopup.Text + "%", fromDate, toDate, description);
                GridViewSearch.DataSource = _dtSearchAll;
                GridViewSearch.DataBind();
                if (_dtSearchAll.Rows.Count < 1)
                {

                    DispMsg("No Records Found!!!", "W");

                }
                if (GridViewSearch.PageIndex > 0)
                { GridViewSearch.SetPageIndex(0); }
            }
            txtHSCodepopup.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEntrymodal.Text = string.Empty;
            popUpSearchModel.Show();
        }
        protected void dgvSearchAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSearch.PageIndex = e.NewPageIndex;
            GridViewSearch.DataSource = new int[] { };
            if (_dtSearchAll != null)
            {
                if (_dtSearchAll.Rows.Count > 0)
                {
                    GridViewSearch.DataSource = _dtSearchAll;
                }
                else
                {
                    DispMsg("No Records Found!!!", "W");
                }
            }
            GridViewSearch.DataBind();
            popUpSearchModel.Show();
        }
        protected void txtentry_TextChanged(object sender,EventArgs e)
        {            
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
            DataTable dtSearch = CHNLSVC.CommonSearch.SearchCusdecHeaderHS(para, "DOC NO", "%" + txtEntrymodal.Text);
            if (dtSearch.Rows.Count!=1)
            {
                DispMsg("Please Enter a Valid Entry Number", "W");
                popUpSearchModel.Show();
                txtEntrymodal.Text=string.Empty;           
            }
            popUpSearchModel.Show();            
        }
        protected void txths_TextChanged(object sender, EventArgs e)
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
            DataTable _dtSearch = CHNLSVC.CommonSearch.SearchGetHsCode(para, cmbSearchbykey.SelectedValue, "%" + txtHSCodepopup.Text);
            if (_dtSearch.Rows.Count != 1)
            {
                DispMsg("Please Enter a Valid HS Code", "W");
                popUpSearchModel.Show();
                txtHSCodepopup.Text = string.Empty;
            }
            popUpSearchModel.Show();
        }
        protected void lbtPopitmeClose_Click(object sender, EventArgs e)
        {
            ItemPopup.Hide();
            popUpSearchModel.Show();
            Session["SearchPopUp"] = "Show";            
        }
        
    }
}