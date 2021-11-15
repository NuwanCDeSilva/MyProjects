using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class ExchangeRateDefinition : BasePage
    {
        public List<MasterCompany> ListCom;
        public List<SystemUserCompany> userCompanyList;
        public List<MasterExchangeRate> masterExchangeRate;
        public List<MasterExchangeRate> exchangeRates;
        public MasterExchangeRate exchangeRate;
        string queryString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["queryString"] = queryString;
                    if (Request.QueryString.Count < 1)
                    {
                        Response.Redirect("~/View/ADMIN/Home.aspx"); return;
                    }
                    queryString = Request.QueryString[0];
                    Session["queryString"] = queryString;
                    if (string.IsNullOrEmpty(queryString))
                    {
                        Response.Redirect("~/View/ADMIN/Home.aspx"); return;
                    }

                    masterExchangeRate = new List<MasterExchangeRate>();
                    Session["masterExchangeRate"] = masterExchangeRate;
                    if ((string)Session["queryString"] == "Sales")
                    {
                        txtBankSellRate.Enabled = true;
                        txtBankBuyRate.Enabled = true;
                        txtCustmRate.Enabled = false;
                    }
                    if ((string)Session["queryString"] == "Custom")
                    {
                        txtBankSellRate.Enabled = false;
                        txtBankBuyRate.Enabled = false;
                        txtCustmRate.Enabled = true;
                    }
                    
                    ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
                    if (ListCom != null)
                    {
                        var company = ListCom.Where(c => c.Mc_cd == Session["UserCompanyCode"].ToString()).FirstOrDefault();
                        if (company != null)
                        {
                            txtFromCurr.Text ="USD";
                            txtToCurr.Text = company.Mc_cur_cd;
                        }
                    }
                    hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                    txtFromDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                    txtToDate.Text = DateTime.Today.AddMonths(1).Date.ToString("dd/MMM/yyyy");

                    userCompanyList = GetUserCompany(Session["UserID"].ToString());
                    //............................................................................................................
                    bool b16045 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16045);
                    bool b16046 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16046);
                    bool b16047 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16047);

                    dgvCompany.DataSource = new int[] { };
                    if (b16047)
                    {
                        dgvCompany.DataSource = userCompanyList;
                        hdfShowCom.Value = "1";
                    }
                    else
                    {
                        userCompanyList = userCompanyList.Where(c => c.SEC_COM_CD == Session["UserCompanyCode"].ToString()).ToList();
                        dgvCompany.DataSource = userCompanyList;
                        hdfShowCom.Value = "0";
                    }
                    dgvCompany.DataBind();
                    PanelPrfit.Enabled = b16045 ? true : false;
                    txtProfitCenter.Text =b16045? Session["UserDefProf"].ToString():"";
                    txtProfitCenter.ToolTip = CHNLSVC.General.Get_ProfitCenter_desc(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper());
                    hdfShowLoc.Value = "0" ;
                    hdfShowDelBtn.Value = "0";
                    
                    
                    foreach (GridViewRow rw in dgvCompany.Rows)
                    {
                        Label lblCode = (Label)rw.FindControl("lblCode");
                        CheckBox chkCompanyCode = (CheckBox)rw.FindControl("chkCompanyCode");
                        if (lblCode.Text == Session["UserCompanyCode"].ToString())
                        {
                            chkCompanyCode.Checked = true;
                            break;
                        }
                    }
                    CheckBox chkboxSelectAllCom = (CheckBox)dgvCompany.HeaderRow.FindControl("chkboxSelectAllCom");
                    chkboxSelectAllCom.Visible = dgvCompany.Rows.Count > 1 ? true : false;
                    txtProfitCenter.Enabled = false;
                    //if (b16045 || b16046 || b16047)
                    //{
                    //    txtProfitCenter.Enabled = true;
                    //    hdfShowLoc.Value = "1";
                    //}
                    if (b16045)
                    {
                        txtProfitCenter.Enabled = true;
                        hdfShowLoc.Value = "1";
                    }
                    dgvRate.DataSource = new int[] { };
                    dgvRate.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/View/ADMIN/Home.aspx");
                }
            }
        }
        private List<SystemUserCompany> GetUserCompany(string userName)
        {
            userCompanyList = new List<SystemUserCompany>();
            if (!string.IsNullOrEmpty(userName))
            {
                userCompanyList = CHNLSVC.Security.GetUserCompany(userName);
                if (userCompanyList != null)
                {
                    dgvCompany.DataSource = userCompanyList.OrderBy(c => c.SEC_COM_CD).ToList();
                }
            }
            return userCompanyList;
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                
                masterExchangeRate = new List<MasterExchangeRate>();
                Session["masterExchangeRate"] = masterExchangeRate;
                bool b16045 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16045);
                hdfShowLoc.Value = "1";
                hdfShowDelBtn.Value = "0";
                bool selectCom = false;
                foreach (GridViewRow row in dgvCompany.Rows)
                {
                    CheckBox chkCompanyCode = (CheckBox)row.FindControl("chkCompanyCode");
                    if (chkCompanyCode.Checked)
                    {
                        selectCom = true; break;
                    }
                }
                if (!selectCom)
                {
                    DispMsg("Please select a company !", "W"); return;
                }
                DataTable gridData = new DataTable();
                foreach (GridViewRow rw in dgvCompany.Rows)
                {
                    CheckBox chkCompanyCode = (CheckBox)rw.FindControl("chkCompanyCode");
                    if (chkCompanyCode.Checked)
                    {
                        Label lblCode = (Label)rw.FindControl("lblCode");
                        //DataTable dt = CHNLSVC.CommonSearch.GetValid_ExchangeRates(lblCode.Text, txtFromCurr.Text.Trim().ToUpper(), txtToCurr.Text.Trim().ToUpper(), Convert.ToDateTime(txtFromDate.Text));
                        DateTime _dtTmp = DateTime.MinValue;
                        MasterExchangeRate exRate = new MasterExchangeRate() { 
                            Mer_act = true, 
                            Mer_com = string.IsNullOrEmpty(lblCode.Text)?null:lblCode.Text, 
                            Mer_cur=string.IsNullOrEmpty(txtFromCurr.Text)?null: txtFromCurr.Text.Trim().ToUpper(),
                            Mer_to_cur=string.IsNullOrEmpty(txtToCurr.Text)?null:txtToCurr.Text.Trim().ToUpper(),
                            Mer_vad_from=string.IsNullOrEmpty(txtFromDate.Text)?DateTime.MinValue:Convert.ToDateTime(txtFromDate.Text),
                            isDateRange = string.IsNullOrEmpty(txtFromDate.Text) ? 0 : DateTime.TryParse(txtFromDate.Text, out _dtTmp) ? 1 : 0
                        }; 
                        DataTable dt = CHNLSVC.CommonSearch.SearchExchangeRateHistry(exRate);
                        if (dt.Rows.Count > 0)
                        {
                            gridData.Merge(dt);
                        }
                    }
                }
                if (gridData.Rows.Count > 0)
                {
                    // Session["masterExchangeRate"] = masterExchangeRate;
                    dgvRate.DataSource = gridData;
                }
                else
                {
                    //masterExchangeRate = new List<MasterExchangeRate>();
                    // Session["masterExchangeRate"] = masterExchangeRate;
                    dgvRate.DataSource = new int[] { };
                    DispMsg("No data available.  !!!", "N");
                }
                dgvRate.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        private void DispMsg(string msgText, string msgType)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        protected void lbtnDelRate_Click(object sender, EventArgs e)
        {
            if (hdfDelete.Value == "Yes")
            {
                bool b16045 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16045);
                hdfShowLoc.Value = b16045 ? "0" : "1";
                hdfShowDelBtn.Value = "1";
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblId = (Label)row.FindControl("lblId");
                Label lblCom = (Label)row.FindControl("lblCom");
                Label lblBaseCurr = (Label)row.FindControl("lblBaseCurr");
                Label lblToCurr = (Label)row.FindControl("lblToCurr");
                Label lblFromDate = (Label)row.FindControl("lblFromDate");
                Label lblToDate = (Label)row.FindControl("lblToDate");
                Label lblSellingRate = (Label)row.FindControl("lblSellingRate");
                Label lblBuyingRate = (Label)row.FindControl("lblBuyingRate");
                Label lblCustomRate = (Label)row.FindControl("lblCustomRate");
                Label lblProfCenter = (Label)row.FindControl("lblProfCenter");
                masterExchangeRate = (List<MasterExchangeRate>)Session["masterExchangeRate"];
                if (masterExchangeRate.Count > 0)
                {
                    if ((string)Session["queryString"] == "Sales")
                    {
                        var v = masterExchangeRate.Where(c => c.Mer_com == lblCom.Text && c.Mer_pc == lblProfCenter.Text && c.Mer_cur == lblBaseCurr.Text && c.Mer_to_cur == lblToCurr.Text &&
                          c.Mer_vad_from == Convert.ToDateTime(lblFromDate.Text) && c.Mer_vad_to == Convert.ToDateTime(lblToDate.Text)
                          && c.Mer_bnkbuy_rt == ConvDecZero(lblBuyingRate.Text)
                          && c.Mer_bnksel_rt == ConvDecZero(lblSellingRate.Text)
                          ).ToList().FirstOrDefault();
                        if (v != null)
                        {
                            masterExchangeRate.Remove(v);
                        }
                    }
                    if ((string)Session["queryString"] == "Custom")
                    {
                        var v = masterExchangeRate.Where(c => c.Mer_com == lblCom.Text && c.Mer_pc == lblProfCenter.Text && c.Mer_cur == lblBaseCurr.Text && c.Mer_to_cur == lblToCurr.Text &&
                         c.Mer_vad_from == Convert.ToDateTime(lblFromDate.Text) && c.Mer_vad_to == Convert.ToDateTime(lblToDate.Text)
                         && c.Mer_cussel_rt == ConvDecZero(lblCustomRate.Text)
                         ).ToList().FirstOrDefault();
                        if (v != null)
                        {
                            masterExchangeRate.Remove(v);
                        }
                    }
                }
                Session["masterExchangeRate"] = masterExchangeRate;
                dgvRate.DataSource = masterExchangeRate;
                dgvRate.DataBind();
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = null;
                Session["AllProfitCenters"] = null;
                Session["FromCurrency"] = null;
                Session["ToCurrency"] = null;
                if (lblSearchType.Text == "AllProfitCenters")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _dt = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["AllProfitCenters"] = _dt;
                }
                else if (lblSearchType.Text == "FromCurrency")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    _dt = CHNLSVC.CommonSearch.GetCurrencyData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["FromCurrency"] = _dt;
                }
                else if (lblSearchType.Text == "ToCurrency")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    _dt = CHNLSVC.CommonSearch.GetCurrencyData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ToCurrency"] = _dt;
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _dt;
                    }
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "AllProfitCenters")
                {
                    _result = (DataTable)Session["AllProfitCenters"];
                }
                else if (lblSearchType.Text == "FromCurrency")
                {
                    _result = (DataTable)Session["FromCurrency"];
                }
                else if (lblSearchType.Text == "ToCurrency")
                {
                    _result = (DataTable)Session["ToCurrency"];
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
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "AllProfitCenters")
                {
                    txtProfitCenter.Text = code;
                    txtProfitCenter_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "FromCurrency")
                {
                    txtFromCurr.Text = code;
                    txtFromCurr_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "ToCurrency")
                {
                    txtToCurr.Text = code;
                    txtToCurr_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
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
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void lbtnSeProfiCenter_Click(object sender, EventArgs e)
        {
            try
            {
                bool b16045 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16045);
                bool b16046 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16046);
                bool b16047 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16047);
                if (!b16045)
                {
                    if (!b16046)
                    {
                        if (!b16047)
                        {
                            if (!b16045)
                            {
                                DispMsg("Sorry, You have no permission !     (Permission code :16045)", "W"); return;
                            }
                            else if (!b16046)
                            {
                                DispMsg("Sorry, You have no permission !     (Permission code :16046)", "W"); return;
                            }
                            else if (!b16047)
                            {
                                DispMsg("Sorry, You have no permission !     (Permission code :16047)", "W"); return;
                            }
                        }
                    }
                }

                lblSearchType.Text = "AllProfitCenters";
                Session["AllProfitCenters"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["AllProfitCenters"] = _result;
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
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtProfitCenter_TextChanged(object sender, EventArgs e)
        {
            txtProfitCenter.ToolTip = "";
            if (!string.IsNullOrEmpty(txtProfitCenter.Text) &&!CHNLSVC.General.CheckProfitCenter(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper()))
            {
                txtProfitCenter.Text = ""; txtProfitCenter.Focus();
                DispMsg("Please select a valid profit center !!!", "W"); return;
            }
            txtProfitCenter.ToolTip = CHNLSVC.General.Get_ProfitCenter_desc(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper());
        }

        protected void lbtnSeFromCurr_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "FromCurrency";
                Session["FromCurrency"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["FromCurrency"] = _result;
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
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeToCurr_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ToCurrency";
                Session["ToCurrency"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["ToCurrency"] = _result;
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
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtFromCurr_TextChanged(object sender, EventArgs e)
        {
            txtFromCurr.ToolTip = "";
            if (string.IsNullOrEmpty(txtFromCurr.Text))
            {
                return; 
            }
            if (!CHNLSVC.General.CheckCurrency(txtFromCurr.Text.Trim().ToUpper()))
            {
                txtFromCurr.Text = ""; txtFromCurr.Focus();
                DispMsg("Please select a valid from currency !!!", "W"); return;
            }
            txtFromCurr.ToolTip = CHNLSVC.General.Get_Currency_desc(txtFromCurr.Text.Trim().ToUpper());
        }

        protected void txtToCurr_TextChanged(object sender, EventArgs e)
        {
            txtToCurr.ToolTip = "";
            if (string.IsNullOrEmpty(txtToCurr.Text))
            {
                return;
            }
            if (!CHNLSVC.General.CheckCurrency(txtToCurr.Text.Trim().ToUpper()))
            {
                txtToCurr.Text = ""; txtToCurr.Focus();
                DispMsg("Please select a valid to currency !!!", "W"); return;
            }
            txtToCurr.ToolTip = CHNLSVC.General.Get_Currency_desc(txtToCurr.Text.Trim().ToUpper());
        }

        protected void lbtnAddRate_Click(object sender, EventArgs e)
        {
            bool b16045 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16045);
            hdfShowLoc.Value = b16045 ? "1" : "0";
            hdfShowDelBtn.Value = "1";
            #region validation
            queryString = (string)Session["queryString"];
            if (string.IsNullOrEmpty(queryString))
            {
                Response.Redirect("~/View/ADMIN/Home.aspx"); return;
            }
            bool selectCom = false;
            foreach (GridViewRow row in dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = (CheckBox)row.FindControl("chkCompanyCode");
                if (chkCompanyCode.Checked)
                {
                    selectCom = true; break;
                }
            }
            if (!selectCom)
            {
                DispMsg("Please select a company !", "W"); return;
            }
            //if (!b16045)
            //{
            //    if (string.IsNullOrEmpty(txtProfitCenter.Text))
            //    {
            //        DispMsg("Please select a profit center !", "W"); return;
            //    }
            //}
            if (string.IsNullOrEmpty(txtFromCurr.Text))
            {
                DispMsg("Please select a from currency !", "W"); return;
            }
            if (string.IsNullOrEmpty(txtToCurr.Text))
            {
                DispMsg("Please select a to currency !", "W"); return;
            }
            if (string.IsNullOrEmpty(txtFromDate.Text))
            {
                DispMsg("Please enter a from date !", "W"); return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtFromDate.Text);
                }
                catch (Exception)
                {
                    DispMsg("Please enter a valid from date !", "W"); return;
                }
            }
            if (string.IsNullOrEmpty(txtToDate.Text))
            {
                DispMsg("Please enter a to date !", "W"); return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtToDate.Text);
                }
                catch (Exception)
                {
                    DispMsg("Please enter a valid to date !", "W"); return;
                }
            }
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                DispMsg("Please select a valid date range !", "W"); return;
            }
            if (queryString == "Sales" && string.IsNullOrEmpty(txtBankSellRate.Text))
            {
                DispMsg("Please enter bank selling rate !", "W"); return;
            }
            if (queryString == "Sales" && string.IsNullOrEmpty(txtBankBuyRate.Text))
            {
                DispMsg("Please enter bank buying rate !", "W"); return;
            }
            if (queryString == "Custom" && string.IsNullOrEmpty(txtCustmRate.Text))
            {
                DispMsg("Please enter custom rate !", "W"); return;
            }
            if (queryString == "Custom" && string.IsNullOrEmpty(txtCustmRate.Text))
            {
                DispMsg("Please enter custom rate !", "W"); return;
            }
            if (!b16045)
            {
                if (!string.IsNullOrEmpty(txtProfitCenter.Text))
                {
                    if (!CHNLSVC.General.CheckProfitCenter(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper()))
                    {
                        DispMsg("Please select a valid profit center !", "W"); return;
                    }
                }
            }
            if (!CHNLSVC.General.CheckCurrency(txtFromCurr.Text.Trim().ToUpper()))
            {
                DispMsg("Please select a valid from currency !", "W"); return;
            }
            if (!CHNLSVC.General.CheckCurrency(txtToCurr.Text.Trim().ToUpper()))
            {
                DispMsg("Please select a valid to currency !", "W"); return;
            }
            if (queryString == "Sales")
            {
                try
                {
                    if (Convert.ToDecimal(txtBankSellRate.Text) <= 0)
                    {
                        txtBankSellRate.Text = "";
                        DispMsg("Please enter a valid bank selling rate !", "W"); return;
                    }
                }
                catch (Exception)
                {
                    txtBankSellRate.Text = "";
                    DispMsg("Please enter a valid bank selling rate !", "W"); return;
                }
                try
                {
                    if (Convert.ToDecimal(txtBankBuyRate.Text) <= 0)
                    {
                        txtBankBuyRate.Text = "";
                        DispMsg("Please enter a valid bank buying rate !", "W"); return;
                    }
                }
                catch (Exception)
                {
                    txtBankBuyRate.Text = "";
                    DispMsg("Please enter a valid bank buying rate !", "W"); return;
                }
            }
            if (queryString == "Custom")
            {
                try
                {
                    if (Convert.ToDecimal(txtCustmRate.Text) <= 0)
                    {
                        txtCustmRate.Text = "";
                        DispMsg("Please enter a valid custom rate !", "W"); return;
                    }
                }
                catch (Exception)
                {
                    txtCustmRate.Text = "";
                    DispMsg("Please enter a valid custom rate !", "W"); return;
                }
            }
            if (queryString == "Sales")
            {
                if (Convert.ToDecimal(txtBankSellRate.Text) == 1 && Convert.ToDecimal(txtBankBuyRate.Text) == 1)
                { }
            }
            if (txtFromCurr.Text==txtToCurr.Text)
            {
                    DispMsg("Both from currency and to currency cannot be same !", "W"); return;
            }
            #endregion
            masterExchangeRate = (List<MasterExchangeRate>)Session["masterExchangeRate"];
            foreach (GridViewRow rw in dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = (CheckBox)rw.FindControl("chkCompanyCode");
                if (chkCompanyCode.Checked)
                {
                    Label lblCode = (Label)rw.FindControl("lblCode");
                    Label lblComDesc = (Label)rw.FindControl("lblComDesc");

                    #region AddRates
                    exchangeRate = new MasterExchangeRate()
                    {
                        Mer_com = lblCode.Text,
                        Mer_com_desc = lblComDesc.Text,
                        Mer_cur = txtFromCurr.Text,
                        Mer_vad_from = Convert.ToDateTime(txtFromDate.Text),
                        Mer_vad_to = Convert.ToDateTime(txtToDate.Text),
                        Mer_bnksel_rt = queryString == "Sales" ? ConvDecZero(txtBankSellRate.Text) : 0,
                        Mer_bnkbuy_rt = queryString == "Sales" ? ConvDecZero(txtBankBuyRate.Text) : 0,
                        Mer_cussel_rt = queryString == "Custom" ? ConvDecZero(txtCustmRate.Text) : 0,
                        Mer_buyvad_from = Convert.ToDateTime(txtFromDate.Text),
                        Mer_buyvad_to = Convert.ToDateTime(txtToDate.Text),
                        Mer_act = true,
                        Mer_cre_by = Session["UserID"].ToString(),
                        Mer_cre_dt = DateTime.Now,
                        Mer_mod_by = Session["UserID"].ToString(),
                        Mer_mod_dt = DateTime.Now,
                        Mer_session_id = Session["SessionID"].ToString(),
                        Mer_to_cur = txtToCurr.Text,
                       // Mer_pc = !string.IsNullOrEmpty(Session["SessionID"].ToString()) ? txtProfitCenter.Text : queryString == "Custom" ? "WHARF" : "",
                        Mer_pc = (queryString == "Custom") ? "WHARF" : b16045?txtProfitCenter.Text :"",
                        /*Temp object data */
                        Mer_to_cur_desc=txtFromCurr.ToolTip,
                        Mer_from_cur_desc=txtToCurr.ToolTip
                    };
                    if (queryString == "Sales")
                    {
                        var v = masterExchangeRate.Where(c => c.Mer_com == exchangeRate.Mer_com && c.Mer_pc == exchangeRate.Mer_pc &&
                          c.Mer_cur == exchangeRate.Mer_cur && c.Mer_to_cur == exchangeRate.Mer_to_cur &&
                           c.Mer_vad_from == exchangeRate.Mer_vad_from && c.Mer_vad_to == exchangeRate.Mer_vad_to
                           && c.Mer_bnksel_rt == exchangeRate.Mer_bnksel_rt
                           && c.Mer_bnkbuy_rt == exchangeRate.Mer_bnkbuy_rt 
                           ).ToList().FirstOrDefault();
                        if (v != null)
                        {
                            DispMsg("The exchange rates already added", "W"); return;
                        }
                    }
                    if (queryString == "Custom")
                    {
                        var v = masterExchangeRate.Where(c => c.Mer_com == exchangeRate.Mer_com && c.Mer_pc == exchangeRate.Mer_pc &&
                          c.Mer_cur == exchangeRate.Mer_cur && c.Mer_to_cur == exchangeRate.Mer_to_cur &&
                           c.Mer_vad_from == exchangeRate.Mer_vad_from && c.Mer_vad_to == exchangeRate.Mer_vad_to
                           && c.Mer_cussel_rt == exchangeRate.Mer_cussel_rt
                           ).ToList().FirstOrDefault();
                        if (v != null)
                        {
                            DispMsg("The exchange rates already added", "W"); return;
                        }
                    }
                    masterExchangeRate.Add(exchangeRate);
                    #endregion
                    if (queryString == "Sales")
                    {
                        #region USD
                        if (txtFromCurr.Text != "USD" && chkUs.Checked == true)
                        {
                            DataTable DT = CHNLSVC.Financial.GetFromCurr_to_ToCurr("USD", "LKR", DateTime.Now.Date, Session["UserCompanyCode"].ToString());
                            if (DT == null)
                            {
                                DispMsg("Please save USD to LKR first", "W"); return;
                            }
                            if (DT.Rows.Count < 1)
                            {
                                DispMsg("Please save USD to LKR first", "W"); return;
                            }
                            //---------------------------------------------------------
                            DT = CHNLSVC.Financial.GetFromCurr_to_ToCurr("USD", "LKR", DateTime.Now.Date, Session["UserCompanyCode"].ToString());
                            if (DT == null)
                            {
                                string msg = "Please save " + txtFromCurr.Text + "to LKR first !";
                                DispMsg(msg, "W"); return;
                            }
                            if (DT.Rows.Count < 1)
                            {
                                string msg = "Please save " + txtFromCurr.Text + "to LKR first !";
                                DispMsg(msg, "W"); return;
                            }
                            Decimal usd_to_lkr_RATE1 = Convert.ToDecimal(DT.Rows[0]["MER_BNKSEL_RT"].ToString());//120
                            Decimal other_to_lkr_RATE1 = Convert.ToDecimal(txtBankSellRate.Text); //30
                            Decimal usd_to_other_RATE1 = usd_to_lkr_RATE1 / other_to_lkr_RATE1;

                            //--------------------------------------
                            Decimal usd_to_lkr_RATE2 = Convert.ToDecimal(DT.Rows[0]["MER_BNKBUY_RT"].ToString());//120
                            Decimal other_to_lkr_RATE2 = Convert.ToDecimal(txtBankSellRate.Text); //30
                            Decimal usd_to_other_RATE2 = usd_to_lkr_RATE2 / other_to_lkr_RATE2;
                            //-----------------------------------------
                            //--------------------------------------
                            Decimal usd_to_lkr_RATE3 = Convert.ToDecimal(DT.Rows[0]["MER_BNKBUY_RT"].ToString());//120
                            Decimal other_to_lkr_RATE3 = Convert.ToDecimal(txtBankSellRate.Text); //30
                            Decimal usd_to_other_RATE3 = usd_to_lkr_RATE3 / other_to_lkr_RATE3;
                            //-----------------------------------------
                            //USD/ERO= 120/30
                            //USD   

                            //USD :LKR OK  
                            // 1  : 120
                            //USD : EORO
                            //ERO : LKR
                            // 1  : 30
                            exchangeRate = new MasterExchangeRate();
                            // exchangeRate.Mer_id = _lineNo;
                            exchangeRate.Mer_com = lblCode.Text;
                            exchangeRate.Mer_cur = "USD";//ddlFromCur.SelectedValue.ToString();
                            exchangeRate.Mer_vad_from = Convert.ToDateTime(txtFromDate.Text);
                            exchangeRate.Mer_vad_to = Convert.ToDateTime(txtToDate.Text);
                            exchangeRate.Mer_bnksel_rt = Math.Round(usd_to_other_RATE1, 6);//Convert.ToDecimal(txtBankSelling.Text);
                            exchangeRate.Mer_bnkbuy_rt = Math.Round(usd_to_other_RATE2, 6);//Convert.ToDecimal(txtBankBuy.Text);
                            exchangeRate.Mer_cussel_rt = Math.Round(usd_to_other_RATE3, 6);//Convert.ToDecimal(txtCustom.Text);
                            exchangeRate.Mer_buyvad_from = Convert.ToDateTime(txtFromDate.Text);
                            exchangeRate.Mer_buyvad_to = Convert.ToDateTime(txtToDate.Text);
                            exchangeRate.Mer_act = true;
                            exchangeRate.Mer_cre_by = Session["UserID"].ToString();
                            exchangeRate.Mer_cre_dt = DateTime.Now;
                            exchangeRate.Mer_mod_by = Session["UserID"].ToString();
                            exchangeRate.Mer_mod_dt = DateTime.Now;
                            //exchangeRate.Mer_session_id =Session["SessionID"].ToString();
                            exchangeRate.Mer_to_cur = txtToCurr.Text;
                            exchangeRate.Mer_pc = (queryString == "Custom") ? "WHARF" : b16045 ? txtProfitCenter.Text : "";
                             /*Temp object data */
                            exchangeRate.Mer_to_cur_desc=txtFromCurr.ToolTip;
                            exchangeRate.Mer_from_cur_desc = txtToCurr.ToolTip;
                            masterExchangeRate.Add(exchangeRate);

                            //-------------------------------------------------
                            usd_to_other_RATE1 = other_to_lkr_RATE1 / usd_to_lkr_RATE1;
                            usd_to_other_RATE2 = other_to_lkr_RATE2 / usd_to_lkr_RATE2;
                            usd_to_other_RATE3 = other_to_lkr_RATE3 / usd_to_lkr_RATE3;

                            exchangeRate = new MasterExchangeRate();
                            // exchangeRate.Mer_id = _lineNo;
                            exchangeRate.Mer_com = lblCode.Text;
                            exchangeRate.Mer_cur = txtFromCurr.Text;
                            exchangeRate.Mer_vad_from = Convert.ToDateTime(txtFromDate.Text);
                            exchangeRate.Mer_vad_to = Convert.ToDateTime(txtToDate.Text);
                            exchangeRate.Mer_bnksel_rt = Math.Round(usd_to_other_RATE1, 6);//Convert.ToDecimal(txtBankSelling.Text);
                            exchangeRate.Mer_bnkbuy_rt = Math.Round(usd_to_other_RATE2, 6);//Convert.ToDecimal(txtBankBuy.Text);
                            exchangeRate.Mer_cussel_rt = Math.Round(usd_to_other_RATE3, 6);//Convert.ToDecimal(txtCustom.Text);
                            exchangeRate.Mer_buyvad_from = Convert.ToDateTime(txtFromDate.Text);
                            exchangeRate.Mer_buyvad_to = Convert.ToDateTime(txtToDate.Text);
                            exchangeRate.Mer_act = true;
                            exchangeRate.Mer_cre_by = Session["UserID"].ToString();
                            exchangeRate.Mer_cre_dt = DateTime.Now;
                            exchangeRate.Mer_mod_by = Session["UserID"].ToString();
                            exchangeRate.Mer_mod_dt = DateTime.Now;
                            //_tempItem.Mer_session_id =Session["SessionID"].ToString();
                            exchangeRate.Mer_to_cur = "USD";// ddlFromCur.SelectedValue.ToString();
                            exchangeRate.Mer_pc = (queryString == "Custom") ? "WHARF" : b16045 ? txtProfitCenter.Text : "";
                            /*Temp object data */
                            exchangeRate.Mer_to_cur_desc = txtFromCurr.ToolTip;
                            exchangeRate.Mer_from_cur_desc = txtToCurr.ToolTip;
                            masterExchangeRate.Add(exchangeRate);
                        }
                        #endregion
                    }
                }
            }

            Session["masterExchangeRate"] = masterExchangeRate;
           // txtProfitCenter.Text = "";
            txtProfitCenter.ToolTip = CHNLSVC.General.Get_ProfitCenter_desc(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper());
            ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
            if (ListCom != null)
            {
                var company = ListCom.Where(c => c.Mc_cd == Session["UserCompanyCode"].ToString()).FirstOrDefault();
                if (company != null)
                {
                    txtFromCurr.Text = "USD";
                    txtToCurr.Text = company.Mc_cur_cd;
                }
            }
            hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
           // txtFromDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
           // txtToDate.Text = DateTime.Today.AddMonths(1).Date.ToString("dd/MMM/yyyy");
            dgvRate.DataSource = new int[] { };
            if (masterExchangeRate.Count > 0)
            {
                dgvRate.DataSource = masterExchangeRate;
            }
            dgvRate.DataBind();
            txtBankSellRate.Text = "";
            txtBankBuyRate.Text = "";
            txtCustmRate.Text = "";
        }
        private decimal ConvDecZero(string val)
        {
            decimal d = 0;
            try
            {
                decimal.TryParse(val, out d); return d;
            }
            catch (Exception)
            {
                return d;
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "ConfirmUsdZero()", true);
            try
            {
                if (hdfSaveData.Value == "1")
                {
                    masterExchangeRate = (List<MasterExchangeRate>)Session["masterExchangeRate"];
                    if (masterExchangeRate == null)
                    {
                        DispMsg("Please add details to save !", "W"); return;
                    }
                    if (masterExchangeRate.Count < 1)
                    {
                        DispMsg("Please add details to save !", "W"); return;
                    }
                    queryString = (string)Session["queryString"];
                    if (string.IsNullOrEmpty(queryString))
                    {
                        Response.Redirect("~/View/ADMIN/Home.aspx"); return;
                    }
                    int _effect = CHNLSVC.Sales.Save_Exchange_Rate(masterExchangeRate, queryString);
                    if (_effect == 1)
                    {
                        DispMsg("Exchange rate details saved successfully ", "S");
                        foreach (GridViewRow row in dgvCompany.Rows)
                        {
                            CheckBox chkCompanyCode = (CheckBox)row.FindControl("chkCompanyCode");
                            chkCompanyCode.Checked = false;
                        }
                        masterExchangeRate = new List<MasterExchangeRate>();
                        Session["masterExchangeRate"] = masterExchangeRate;
                        dgvRate.DataSource = new int[] { };
                        dgvRate.DataBind();
                    }
                    else
                    {
                        DispMsg("Error Occurred while processing !!!", "E");
                    }
                  
                }
                // _rowEffect = (Int32)CHNLSVC.Sales.SaveExchangeRate(exchangeRates);
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        [WebMethod]
        public int textmessage()
        {
            return (Int32)CHNLSVC.Sales.Save_Exchange_Rate(masterExchangeRate, "");
        }


        [WebMethod]
        public static void SendMail()
        {
            List<MasterExchangeRate> ex = new List<MasterExchangeRate>();
            ex = (List<MasterExchangeRate>)HttpContext.Current.Session["masterExchangeRate"];
            #region mail
            //_ExchangeRate = (List<MasterExchangeRate>)Session["exchangeRates"];
            //foreach (MasterExchangeRate exr in exchangeRatess)
            //{
            //    if (exr.Mer_cur == "USD" && exr.Mer_to_cur == "LKR")
            //    {
            //        if (exr.Mer_bnkbuy_rt == 1)
            //        {
            //            txtCustmRate.Text = "Test";
            //                //string _mail = "Exchange Rate between USD and LKR is set as 1.00 for ," + Environment.NewLine;
            //                //_mail += "Profit center - " + txtProfitCenter.Text.Trim() + "" + Environment.NewLine;
            //                //_mail += "User - " + BaseCls.GlbUserName + " (" + Session["UserID"].ToString() + ")";
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("ab_dfree@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("ab_dfs6d@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("chamald@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("chathuranga@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("darshana@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("sachith@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("asanka@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //                //CHNLSVC.CommonSearch.Send_SMTPMail("kelum@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
            //        }
            //    }

            // }
            #endregion
            Base _basepage = new Base();
            // return (Int32)_basepage.CHNLSVC.Sales.Save_Exchange_Rate(masterExchangeRate, "");
        }

        protected void dgvRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}