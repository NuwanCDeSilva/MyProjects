using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;

namespace FastForward.SCMWeb.View.Enquiries.Imports
{
    public partial class ContainerTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvContainerDetails.DataSource = null;
                dgvContainerSummary.DataSource = null;
                dgvBLDetails.DataSource = null;
                dgvItemList.DataSource = null;
                dgvContainerDetails.DataBind();
                dgvContainerSummary.DataBind();
                dgvBLDetails.DataBind();
                dgvItemList.DataBind();
                txtFromDate.Text = (DateTime.Now.Date.AddDays(-2)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                lbtnMainSerch_Click(null, null);
                //lblUMesur.Text = "";
                //lblUsd.Text = "";
            }
        }

        protected void lBtnContainer_Click(object sender, EventArgs e)
        {
            if (txtContainer.Text != "")
            {
                txtBlNo.Text = "";
                string p_container = txtContainer.Text == "" ? null : txtContainer.Text;
                string p_bl = txtBlNo.Text == "" ? null : txtBlNo.Text;
                string p_from = txtFromDate.Text == "" ? null : txtFromDate.Text;
                string p_to = txtToDate.Text == "" ? null : txtToDate.Text;
                string p_date_range = p_from == null && p_to == null ? null : "Range";
                string p_agent = txtAgent.Text == "" ? null : txtAgent.Text;
                string p_container_tp = txtContainerType.Text == "" ? null : txtContainerType.Text;
                string p_compmany = txtCompany.Text == "" ? null : txtCompany.Text;
                LoadGridData(p_container, null, null, null, null, null, null, null);
            }
            else
            {
                displayMessage("Please enter container number ");
                txtContainer.Focus();
                return;
            }
        }
        protected void lBtnBL_Click(object sender, EventArgs e)
        {
            if (txtBlNo.Text != "")
            {
                txtContainer.Text = "";
                string p_container = txtContainer.Text == "" ? null : txtContainer.Text;
                string p_bl = txtBlNo.Text == "" ? null : txtBlNo.Text;
                string p_from = txtFromDate.Text == "" ? null : txtFromDate.Text;
                string p_to = txtToDate.Text == "" ? null : txtToDate.Text;
                string p_date_range = p_from == null && p_to == null ? null : "Range";
                string p_agent = txtAgent.Text == "" ? null : txtAgent.Text;
                string p_container_tp = txtContainerType.Text == "" ? null : txtContainerType.Text;
                string p_compmany = txtCompany.Text == "" ? null : txtCompany.Text;
                LoadGridData(null, p_bl, null, null, null, null, null, null);
            }
            else
            {
                displayMessage("Please enter b/l number ");
                txtBlNo.Focus();
                return;
            }
        }
        protected void lbtnMainSerch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvContainerDetails.DataSource = null;
                dgvContainerSummary.DataSource = null;
                dgvBLDetails.DataSource = null;
                dgvItemList.DataSource = null;

                dgvContainerDetails.DataBind();
                dgvContainerSummary.DataBind();
                dgvBLDetails.DataBind();
                dgvItemList.DataBind();

                string p_container = txtContainer.Text == "" ? null : txtContainer.Text;
                string p_bl = txtBlNo.Text == "" ? null : txtBlNo.Text;
                string p_from = txtFromDate.Text == "" ? null : txtFromDate.Text;
                string p_to = txtToDate.Text == "" ? null : txtToDate.Text;
                string p_date_range = p_from == null && p_to == null ? null : "Range";

                if (txtContainer.Text != "")
                {
                    bool b2 = false;string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerNo(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtContainer.Text == row["Code"].ToString())
                            {
                                b2 = true;toolTip = row["Doc No"].ToString();
                            }
                        }
                    }
                    txtContainer.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtContainer.Text = "";txtContainer.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid container number !!!')", true);
                        return;
                    }
                }
                if (txtBlNo.Text != "")
                {
                    bool b2 = false; string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable _result = CHNLSVC.CommonSearch.GetBlNumber(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtBlNo.Text == row["Code"].ToString())
                            {
                                b2 = true; toolTip = row["Doc No"].ToString();
                            }
                        }
                    }
                    txtBlNo.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtBlNo.Text = ""; txtBlNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid bl number !!!')", true);
                        return;
                    }
                }
                if (txtAgent.Text != "")
                {
                    bool b2 = false; string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerAgent(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtAgent.Text == row["Code"].ToString())
                            {
                                b2 = true;toolTip = row["Description"].ToString();
                            }
                        }
                    }
                    txtAgent.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtAgent.Text = "";txtAgent.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid agent !!!')", true);
                        return;
                    }
                }

                if (txtContainerType.Text != "")
                {
                    bool b2 = false;string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ContainerType);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerType(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtContainerType.Text == row["Code"].ToString())
                            {
                                b2 = true;toolTip = row["Description"].ToString();
                            }
                        }
                    }
                    txtContainerType.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtContainerType.Text = "";txtContainerType.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid container type !!!')", true);
                        return;
                    }
                }
                if (txtCompany.Text != "")
                {
                    bool b2 = false;string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCompany.Text == row["Code"].ToString())
                            {
                                b2 = true;toolTip = row["Description"].ToString();
                            }
                        }
                    }
                    txtCompany.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtCompany.Text = "";txtCompany.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid company !!!')", true);
                        return;
                    }
                }
                string p_agent = txtAgent.Text == "" ? null : txtAgent.Text;
                string p_container_tp = txtContainerType.Text == "" ? null : txtContainerType.Text;
                string p_compmany = txtCompany.Text == "" ? null : txtCompany.Text;
                if (p_date_range == "Range")
                {
                    DateTime dateFrom = Convert.ToDateTime(txtFromDate.Text);
                    DateTime dateTo = Convert.ToDateTime(txtToDate.Text);
                    if (dateFrom <= dateTo)
                    {

                    }
                    else
                    {
                        displayMessage("Please select a valid date range ");
                        return;
                    }
                }
                LoadGridData(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void LoadGridData(string p_container, string p_bl, string p_date_range, string p_from, string p_to, string p_agent, string p_container_tp, string p_compmany)
        {
            try
            {
                DataTable dtConDetails = new DataTable();
                dtConDetails = CHNLSVC.Inventory.GetContainerDetails(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany);
                dgvContainerDetails.DataSource = dtConDetails;
                dgvContainerDetails.DataBind();

                DataTable dtConSummary = new DataTable();
                dtConSummary = CHNLSVC.Inventory.GetContainerSummary(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany);

                if (dtConSummary.Rows.Count > 0)
                {
                    decimal count = 0;
                    foreach (DataRow r in dtConSummary.Rows)
                    {
                        count = count + Convert.ToDecimal(r["TypeCount"].ToString());
                    }
                    DataRow row = dtConSummary.NewRow();
                    row["ibc_tp"] = "Sub total";
                    row["TypeCount"] = count;
                    dtConSummary.Rows.Add(row);
                }
                dgvContainerSummary.DataSource = dtConSummary;
                dgvContainerSummary.DataBind();

                DataTable dtBlData = new DataTable();
                string option = "BL";
                if (radeta.Checked)
                {
                    option = "ETA";
                }
                dtBlData = CHNLSVC.Inventory.GetContainerBlDetails(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany, option);
                //IB_TOT_PKG
                if (dtBlData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtBlData.Rows)
                    {
                        decimal tot = 0;
                        try
                        {
                            tot=Convert.ToDecimal(row["IB_TOT_PKG"].ToString());
                        }
                        catch
                        {
                            tot = 0;
                        }
                        row["IB_TOT_PKG"] = tot.ToString("N");
                    }
                }

                dgvBLDetails.DataSource = dtBlData;
                dgvBLDetails.DataBind();
                if (dtBlData.Rows.Count > 0)
                {
                    string mesure = "", usd = "";
                    foreach (DataRow dr in dtBlData.Rows)
                    {
                        usd = dr["ib_cur_cd"].ToString();
                        mesure = dr["ib_anal_2"].ToString(); 
                    }
                    //lblUMesur.Text =string.IsNullOrEmpty(mesure)?"": "(" + mesure + ")";
                    //lblUsd.Text = string.IsNullOrEmpty(usd) ? "" : "(" + usd + ")";
                }
                if (dgvContainerDetails.Rows.Count == 0 && dgvContainerSummary.Rows.Count == 0
                    && dgvBLDetails.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no data found !!!')", true);
                    return;
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
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ContainerType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Container:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "Company")
                {
                    _result = (DataTable)Session["Company"];
                }
                if (lblSearchType.Text == "ContainerType")
                {
                    _result = (DataTable)Session["ContainerType"];
                }
                if (lblSearchType.Text == "ImportAgent")
                {
                    _result = (DataTable)Session["ImportAgent"];
                }
                if (lblSearchType.Text == "Container")
                {
                    _result = (DataTable)Session["Container"];
                }
                if (lblSearchType.Text == "BLHeader")
                {
                    _result = (DataTable)Session["BLHeader"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
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

        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                    "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "Company")
                {
                    txtCompany.Text = code;
                    chkAllCompany.Checked = false;
                }
                else if (lblSearchType.Text == "ContainerType")
                {
                    txtContainerType.Text = code;
                    chkAllContType.Checked = false;
                }
                else if (lblSearchType.Text == "ImportAgent")
                {
                    txtAgent.Text = code;
                    chkAllAgent.Checked = false;
                }
                else if (lblSearchType.Text == "Container")
                {
                    txtContainer.Text = code;
                    chkAllContainer.Checked = false;
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    txtBlNo.Text = code;
                    chkAllBlNo.Checked = false;
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
                DataTable dt = null;
                Session["CAT_Main"] = null;
                Session["ContainerType"] = null;
                Session["ImportAgent"] = null;
                Session["CAT_Sub3"] = null;
                Session["BLHeader"] = null;
                if (lblSearchType.Text == "Company")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    dt = CHNLSVC.CommonSearch.GetCompanySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Company"] = dt;
                }
                else if (lblSearchType.Text == "ContainerType")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ContainerType);
                    dt = CHNLSVC.CommonSearch.GetContainerType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ContainerType"] = dt;
                }
                else if (lblSearchType.Text == "ImportAgent")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ContainerType);
                    dt = CHNLSVC.CommonSearch.GetContainerAgent(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ImportAgent"] = dt;
                }
                else if (lblSearchType.Text == "Container")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
                    dt = CHNLSVC.CommonSearch.GetContainerNo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Container"] = dt;
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    dt = CHNLSVC.CommonSearch.GetBlNumber(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BLHeader"] = dt;
                }
                dgvResultItem.DataSource = null;
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (dt.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = dt;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
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

        protected void chkAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCompany.Checked)
            {
                txtCompany.Text = "";
            }
        }

        protected void chkAllContType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllContType.Checked)
            {
                txtContainerType.Text = "";
            }
        }

        protected void chkAllAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllAgent.Checked)
            {
                txtAgent.Text = "";
            }
        }
        protected void lbtnCompnay_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Company";
                Session["Company"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Company"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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


        protected void lbtnContainerType_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ContainerType";
                Session["ContainerType"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ContainerType);
                DataTable _result = CHNLSVC.CommonSearch.GetContainerType(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ContainerType"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnAgent_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ImportAgent";
                Session["ImportAgent"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetContainerAgent(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ImportAgent"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void dgvContainerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvContainerSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvBLDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvItemList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        protected void txtContainer_TextChanged(object sender, EventArgs e)
        {
            lBtnContainer_Click(null, null);
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
                // clear();
            }
        }

       

        protected void lbtnSeContainer_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Container";
                Session["Container"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
                DataTable _result = CHNLSVC.CommonSearch.GetContainerNo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Container"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void lbtnSeBiNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BLHeader";
                Session["BLHeader"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.GetBlNumber(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BLHeader"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
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

        protected void txtContainer_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                if (txtContainer.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerNo(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtContainer.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Doc No"].ToString();
                               // return;
                            }
                        }
                    }
                    txtContainer.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        //txtContainer.Text = "";
                        txtContainer.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid container number !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtContainer.ToolTip = "";
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

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBlNo.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable _result = CHNLSVC.CommonSearch.GetBlNumber(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtBlNo.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Doc No"].ToString();
                                // return;
                            }
                        }
                    }
                    txtBlNo.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    { 
                        txtBlNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid bl number !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtBlNo.ToolTip = "";
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

        protected void txtAgent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAgent.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerAgent(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtAgent.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Doc No"].ToString();
                                // return;
                            }
                        }
                    }
                    txtAgent.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    { 
                        txtAgent.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid agent !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtAgent.ToolTip = "";
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

        protected void txtContainerType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContainerType.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ContainerType);
                    DataTable _result = CHNLSVC.CommonSearch.GetContainerType(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtContainerType.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                // return;
                            }
                        }
                    }
                    txtContainerType.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    { 
                        txtContainerType.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid container type !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtContainerType.ToolTip = "";
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

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
             try
            {
                if (txtCompany.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCompany.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                // return;
                            }
                        }
                    }
                    txtCompany.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        txtCompany.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid company !!!')", true);
                       
                        return;
                    }
                }
                else
                {
                    txtCompany.ToolTip = "";
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

        protected void chkAllContainer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllContainer.Checked)
            {
                txtContainer.Text = "";
            }
        }

        protected void chkAllBlNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBlNo.Checked)
            {
                txtBlNo.Text = "";
            }
        }

        protected void dgvContainerSummary_PreRender(object sender, EventArgs e)
        {
            if (dgvContainerSummary.Rows.Count > 0)
            {
                GridViewRow getRow = dgvContainerSummary.Rows[dgvContainerSummary.Rows.Count - 1];
                getRow.Attributes.Add("class", "highlighted");
            }
        }

        protected void dgvBLDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                //    "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (!string.IsNullOrEmpty(code))
                {
                    string p_container = txtContainer.Text == "" ? null : txtContainer.Text;
                    string p_bl = txtBlNo.Text == "" ? null : txtBlNo.Text;
                    string p_from = txtFromDate.Text == "" ? null : txtFromDate.Text;
                    string p_to = txtToDate.Text == "" ? null : txtToDate.Text;
                    string p_date_range = p_from == null && p_to == null ? null : "Range";
                    string p_agent = txtAgent.Text == "" ? null : txtAgent.Text;
                    string p_container_tp = txtContainerType.Text == "" ? null : txtContainerType.Text;
                    string p_compmany = txtCompany.Text == "" ? null : txtCompany.Text;

                    DataTable dtBlList = new DataTable();
                    dtBlList = CHNLSVC.Inventory.GetContainerBlList(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany);
                    dgvItemList.DataSource = dtBlList;
                    dgvItemList.DataBind();
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

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent.Parent.Parent);
            Label lblCode =(Label)Row.FindControl("lblCode");
            if (!string.IsNullOrEmpty(lblCode.Text))
            {
                string p_container = txtContainer.Text == "" ? null : txtContainer.Text;
                string p_bl = lblCode.Text;
                string p_from = txtFromDate.Text == "" ? null : txtFromDate.Text;
                string p_to = txtToDate.Text == "" ? null : txtToDate.Text;
                string p_date_range = p_from == null && p_to == null ? null : "Range";
                string p_agent = txtAgent.Text == "" ? null : txtAgent.Text;
                string p_container_tp = txtContainerType.Text == "" ? null : txtContainerType.Text;
                string p_compmany = txtCompany.Text == "" ? null : txtCompany.Text;
                DataTable dtBlList = new DataTable();
                dtBlList = CHNLSVC.Inventory.GetContainerBlList(p_container, p_bl, p_date_range, p_from, p_to, p_agent, p_container_tp, p_compmany);
                dgvItemList.DataSource = null;
                if (dtBlList.Rows.Count > 0)
                {
                    dgvItemList.DataSource = dtBlList;
                }
                dgvItemList.DataBind();
            }
        }

        protected void dgvBLDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSupCd = e.Row.FindControl("lblSupCd") as Label;
                Label lblSupplier = e.Row.FindControl("lblSupplier") as Label;
                lblSupCd.ToolTip=lblSupplier.Text;

                Label lblConCd = e.Row.FindControl("lblConCd") as Label;
                Label lblConsigner = e.Row.FindControl("lblConsigner") as Label;
                lblConCd.ToolTip = lblConsigner.Text;

                Label lblDecCd = e.Row.FindControl("lblDecCd") as Label;
                Label lblDeclarant = e.Row.FindControl("lblDeclarant") as Label;
                lblDecCd.ToolTip = lblDeclarant.Text;

                Label lblAgeCd = e.Row.FindControl("lblAgeCd") as Label;
                Label lblAgent = e.Row.FindControl("lblAgent") as Label;
                lblAgeCd.ToolTip = lblAgent.Text;
            }
        }
    }
}