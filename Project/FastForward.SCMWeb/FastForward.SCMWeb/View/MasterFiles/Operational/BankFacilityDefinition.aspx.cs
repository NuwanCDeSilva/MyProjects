using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class BankFacilityDefinition : BasePage
    {
        string _userid = string.Empty;
        List<Int32> successlist = new List<int>();
        Decimal Balance = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime date1 = DateTime.Now;
                    txtvalidfrom.Text = date1.ToString("dd/MMM/yyyy");

                    DateTime date2 = DateTime.Now;
                    txtvalidto.Text = date2.ToString("dd/MMM/yyyy");

                    DateTime date3 = DateTime.Now;
                    txtfacilitydate.Text = date3.ToString("dd/MMM/yyyy");

                    gvaccdetails.DataSource = new int[] { };
                    gvaccdetails.DataBind();

                    ViewState["ACCOUNT_FACILITY"] = null;
                    DataTable dtbankaccounts = new DataTable();
                    dtbankaccounts.Columns.AddRange(new DataColumn[16] { new DataColumn("msbf_cd"), new DataColumn("msbf_branch_cd"), new DataColumn("msbf_acc_cd"), new DataColumn("msbf_curr"), new DataColumn("msbf_valid_frm"), new DataColumn("msbf_valid_to"), new DataColumn("msbf_fac_tp"), new DataColumn("msbf_fac_dt"), new DataColumn("msbf_fac_lmt", typeof(decimal)), new DataColumn("msbf_fac_ult", typeof(decimal)), new DataColumn("balance", typeof(decimal)), new DataColumn("msbf_fac_rt"), new DataColumn("msbf_our_ref"), new DataColumn("msbf_bank_ref"), new DataColumn("msbf_comm_rt"), new DataColumn("msbf_act") });
                    ViewState["ACCOUNT_FACILITY"] = dtbankaccounts;
                    this.BindGrid();

                    PopolateFacilityType();
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

        private void PopolateFacilityType()
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
                DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
                ddlfacility.DataSource = result;
                ddlfacility.DataTextField = "CODE";
                ddlfacility.DataValueField = "CODE";
                ddlfacility.DataBind();

                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
                DataTable result2 = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams2, "Code", ddlfacility.Text);

                if (result2 != null)
                {
                    ddlfacility.ToolTip = result2.Rows[0][1].ToString();
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

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtbankcode.Text.Trim() + seperator + txtbranch.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtbankcode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FacilityAmout:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlfacility.Text);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void BindGrid()
        {
            try
            {
                gvaccdetails.DataSource = (DataTable)ViewState["ACCOUNT_FACILITY"];
                gvaccdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
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

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "14")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "14";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                if (lblvalue.Text == "22")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                    DataTable result = CHNLSVC.General.SearchBankAccounts(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "22";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                    return;
                }

                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "21")
                {
                    GetBankDetails("btnchangebankcode");
                }
                else if (lblvalue.Text == "82")
                {
                    
                    GetBankDetails("btnchangebranchcode");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "22")
                {                    
                    GetBankDetails("btnchangeacccode");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "14")
                {
                    txtcurr.Text = grdResult.SelectedRow.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
                    lbtnbank.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtbranch.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the branch !!!')", true);
                    LinkButton3.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtaccno.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter account number !!!')", true);
                    txtaccno.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtcurr.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter currency !!!')", true);
                    txtcurr.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtfacilityamy.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter facility amount !!!')", true);
                    txtfacilityamy.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtutilamt.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter utilized amount !!!')", true);
                    txtutilamt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtbankrt.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Bank RT !!!')", true);
                    txtbankrt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtourref.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Other Reference !!!')", true);
                    txtbankrt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtbankref.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Bank Reference  !!!')", true);
                    txtbankrt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtbal.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Balance !!!')", true);
                    txtbankrt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtcomrate.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Commission Rate !!!')", true);
                    txtbankrt.Focus();
                    return;
                }            
              

                DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

                string stus = "Active";

                if (ddlstus.SelectedValue == "0")
                {
                    stus = "Inactive";
                }

                Decimal comrate = 0;

                if (!string.IsNullOrEmpty(txtcomrate.Text))
                {
                    comrate = Convert.ToDecimal(txtcomrate.Text.Trim());
                }

                //Kelum : restrict duplicate bank code,branch code,facility code,acc number from grid : 2013-May-16

                if (dt != null)
                {
                    foreach (GridViewRow dtRow in gvaccdetails.Rows)
                    {
                        Label lblbankcode = (Label)dtRow.FindControl("lblbankcode");
                        Label lblbranchcode = (Label)dtRow.FindControl("lblbranch");
                        Label lblacccode = (Label)dtRow.FindControl("lblacc");

                        string grdbankcode = lblbankcode.Text;
                        string grdbranchcode = lblbranchcode.Text;
                        string grdacccode = lblacccode.Text;

                        if (grdbankcode == txtbankcode.Text.Trim() & grdbranchcode == txtbranch.Text.Trim() & grdacccode == txtaccno.Text.Trim())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This Account Number already exist')", true);
                            return;
                        }
                    }

                }

                dt.Rows.Add(txtbankcode.Text.Trim(), txtbranch.Text.Trim(), txtaccno.Text.Trim(), txtcurr.Text.Trim(), txtvalidfrom.Text.Trim(), txtvalidto.Text.Trim(), ddlfacility.SelectedValue, txtfacilitydate.Text.Trim(), txtfacilityamy.Text.Trim(), txtutilamt.Text.Trim(), txtbal.Text.Trim(), txtbankrt.Text.Trim(), txtourref.Text.Trim(), txtbankref.Text.Trim(), comrate, stus);

                ViewState["ACCOUNT_FACILITY"] = dt;
                gvaccdetails.DataSource = dt;
                gvaccdetails.DataBind();

                txtbankcode.Text = string.Empty;
                txtbranch.Text = string.Empty;
                txtaccno.Text = string.Empty;
                txtcurr.Text = string.Empty;
                ddlfacility.SelectedValue = "DA";
                txtfacilityamy.Text = string.Empty;
                txtutilamt.Text = string.Empty;
                txtbal.Text = string.Empty;
                txtbankrt.Text = string.Empty;
                txtourref.Text = string.Empty;
                txtbankref.Text = string.Empty;
                txtcomrate.Text = string.Empty;
                ddlstus.SelectedValue = "1";

                FormatNumbers();

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

        private void FormatNumbers()
        {
            foreach (GridViewRow gvrow in gvaccdetails.Rows)
            {
                Label lblfacilityamt = (Label)gvrow.FindControl("lblfacilityamt");
                Label lblutilamt = (Label)gvrow.FindControl("lblutilamt");
                Label lblbalance = (Label)gvrow.FindControl("lblbalance");
                Label lblbankrt = (Label)gvrow.FindControl("lblbankrt");
                Label lblcomrate = (Label)gvrow.FindControl("lblcomrate");

                lblfacilityamt.Text = DoFormat(Convert.ToDecimal(lblfacilityamt.Text.Trim()));

                lblutilamt.Text = DoFormat(Convert.ToDecimal(lblutilamt.Text.Trim()));

                lblbalance.Text = DoFormat(Convert.ToDecimal(lblbalance.Text.Trim()));

                lblbankrt.Text = DoFormat(Convert.ToDecimal(lblbankrt.Text.Trim()));

                lblcomrate.Text = DoFormat(Convert.ToDecimal(lblcomrate.Text.Trim()));
            }
        }

        private void ddlSubpaymentTernDataPopulate(string PCAD, string PCD)
        {
        }

        protected void ddlfacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSubpaymentTernDataPopulate("IPM", ddlfacility.Text);
                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
                DataTable result2 = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams2, "Code", ddlfacility.Text);

                if (result2 != null)
                {
                    ddlfacility.ToolTip = result2.Rows[0][1].ToString();
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

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvaccdetails.Rows.Count == 0)
                {
                    string msg = "Please enter/load bank account facility details";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    return;
                }

                if (txtconfirmplaceord.Value == "Yes")
                {
                    foreach (GridViewRow hiderowbtn in this.gvaccdetails.Rows)
                    {
                        Label lblbankcode = (Label)hiderowbtn.FindControl("lblbankcode");
                        Label lblbranch = (Label)hiderowbtn.FindControl("lblbranch");
                        Label lblacc = (Label)hiderowbtn.FindControl("lblacc");
                        Label lblcurr = (Label)hiderowbtn.FindControl("lblcurr");
                        Label lblvalidfrom = (Label)hiderowbtn.FindControl("lblvalidfrom");
                        Label lblvalidto = (Label)hiderowbtn.FindControl("lblvalidto");
                        Label lblfacility = (Label)hiderowbtn.FindControl("lblfacility");
                        Label lblfacilitydate = (Label)hiderowbtn.FindControl("lblfacilitydate");
                        Label lblfacilityamt = (Label)hiderowbtn.FindControl("lblfacilityamt");
                        Label lblutilamt = (Label)hiderowbtn.FindControl("lblutilamt");
                        Label lblbankrt = (Label)hiderowbtn.FindControl("lblbankrt");
                        Label lblourref = (Label)hiderowbtn.FindControl("lblourref");
                        Label lblbankref = (Label)hiderowbtn.FindControl("lblbankref");
                        Label lblcomrate = (Label)hiderowbtn.FindControl("lblcomrate");
                        Label lblstus = (Label)hiderowbtn.FindControl("lblstus");

                        Int32 stusval = 1;

                        if (lblstus.Text == "Inactive")
                        {
                            stusval = 0;
                        }

                        _userid = (string)Session["UserID"];

                        BankAccountFacility Facility = new BankAccountFacility();

                        Facility.MSBF_COM = Session["UserCompanyCode"].ToString();
                        Facility.MSBF_CD = lblbankcode.Text.ToUpper().Trim();
                        Facility.MSBF_ACC_CD = lblacc.Text.ToUpper().Trim();
                        Facility.MSBF_FAC_DT = Convert.ToDateTime(lblfacilitydate.Text.Trim());
                        Facility.MSBF_FAC_TP = lblfacility.Text.Trim();
                        Facility.MSBF_FAC_LMT = Convert.ToDecimal(lblfacilityamt.Text.Trim());
                        Facility.MSBF_FAC_ULT = Convert.ToDecimal(lblutilamt.Text.Trim());
                        Facility.MSBF_VALID_FRM = Convert.ToDateTime(lblvalidfrom.Text.Trim());
                        Facility.MSBF_VALID_TO = Convert.ToDateTime(lblvalidto.Text.Trim());
                        Facility.MSBF_FAC_RT = Convert.ToDecimal(lblbankrt.Text.Trim());
                        Facility.MSBF_ACT = stusval;
                        Facility.MSBF_CRE_BY = _userid;
                        Facility.MSBF_CRE_DT = Convert.ToDateTime(DateTime.Now.Date);
                        Facility.MSBF_MOD_BY = _userid;
                        Facility.MSBF_MOD_DT = Convert.ToDateTime(DateTime.Now.Date);
                        Facility.MSBF_ISDEFAULT = 1;
                        Facility.MSBF_CNTY_CD = "LK";
                        Facility.MSBF_COMM_RT = Convert.ToDecimal(lblcomrate.Text.Trim());
                        Facility.MSBF_FIN_COMM_RT = Convert.ToDecimal(0);
                        Facility.MSBF_BRANCH_CD = lblbranch.Text.ToUpper().Trim();
                        Facility.MSBF_OUR_REF = lblourref.Text.Trim();
                        Facility.MSBF_BANK_REF = lblbankref.Text.Trim();
                        Facility.MSBF_CURR = lblcurr.Text.Trim();

                        Int32 results = CHNLSVC.General.UpdateAccountFacility(Facility);
                        successlist.Add(results);
                    }

                    if (!successlist.Contains(-1))
                    {
                        string Msg = "Successfully saved !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        private void Clear()
        {
            try
            {
                DateTime date = DateTime.Now;
                txtvalidfrom.Text = date.ToString("dd/MMM/yyyy");

                DateTime date1 = DateTime.Now;
                txtvalidto.Text = date1.ToString("dd/MMM/yyyy");

                DateTime date3 = DateTime.Now;
                txtfacilitydate.Text = date3.ToString("dd/MMM/yyyy");

                gvaccdetails.DataSource = new int[] { };
                gvaccdetails.DataBind();

                ViewState["ACCOUNT_FACILITY"] = null;
                DataTable dtbankaccounts = new DataTable();
                dtbankaccounts.Columns.AddRange(new DataColumn[16] { new DataColumn("msbf_cd"), new DataColumn("msbf_branch_cd"), new DataColumn("msbf_acc_cd"), new DataColumn("msbf_curr"), new DataColumn("msbf_valid_frm"), new DataColumn("msbf_valid_to"), new DataColumn("msbf_fac_tp"), new DataColumn("msbf_fac_dt"), new DataColumn("msbf_fac_lmt", typeof(decimal)), new DataColumn("msbf_fac_ult", typeof(decimal)), new DataColumn("balance", typeof(decimal)), new DataColumn("msbf_fac_rt"), new DataColumn("msbf_our_ref"), new DataColumn("msbf_bank_ref"), new DataColumn("msbf_comm_rt"), new DataColumn("msbf_act") });
                ViewState["ACCOUNT_FACILITY"] = dtbankaccounts;
                this.BindGrid();

                txtbankcode.Text = string.Empty;
                txtbranch.Text = string.Empty;
                txtaccno.Text = string.Empty;
                txtcurr.Text = string.Empty;
                ddlfacility.SelectedValue = "DA";
                txtfacilityamy.Text = string.Empty;
                txtutilamt.Text = string.Empty;
                txtbal.Text = string.Empty;
                txtbankrt.Text = string.Empty;
                txtourref.Text = string.Empty;
                txtbankref.Text = string.Empty;
                txtcomrate.Text = string.Empty;
                ddlstus.SelectedValue = "1";
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    Clear(); 
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

        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                gvaccdetails.EditIndex = grdr.RowIndex;

                DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

                gvaccdetails.DataSource = dt;
                gvaccdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //modified by kelum : active inactive , valid from to valid to changed : 2016-May-19
        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string txtfacilityamt = (row.FindControl("txtfacilityamt") as TextBox).Text;
                    string txtutilamt = (row.FindControl("txtutilamt") as TextBox).Text;
                    string txtbankrt = (row.FindControl("txtbankrt") as TextBox).Text;
                    string txtvalidfrom = (row.FindControl("txtvalidfrom") as TextBox).Text;
                    string txtvalidto = (row.FindControl("txtvalidto") as TextBox).Text;
                    string txtourref = (row.FindControl("txtourref") as TextBox).Text;
                    string txtbankref = (row.FindControl("txtbankref") as TextBox).Text;
                    string txtcomrate = (row.FindControl("txtcomrate") as TextBox).Text;
                    string stus = (row.FindControl("ddlstatus") as DropDownList).SelectedValue;

                    gvaccdetails.EditIndex = -1;

                    DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

                    //modified by kelum 

                    //string statusvalue = "Active";

                    //if (stus == "Inactive")
                    //{
                    //    statusvalue = "Inactive";
                    //}

                    dt.Rows[row.RowIndex]["msbf_fac_lmt"] = txtfacilityamt;
                    dt.Rows[row.RowIndex]["msbf_fac_ult"] = txtutilamt;
                    dt.Rows[row.RowIndex]["msbf_fac_rt"] = txtbankrt;
                    dt.Rows[row.RowIndex]["msbf_valid_frm"] = txtvalidfrom;
                    dt.Rows[row.RowIndex]["msbf_valid_to"] = txtvalidto;
                    dt.Rows[row.RowIndex]["msbf_our_ref"] = txtourref;
                    dt.Rows[row.RowIndex]["msbf_bank_ref"] = txtbankref;
                    dt.Rows[row.RowIndex]["msbf_comm_rt"] = txtcomrate;
                    dt.Rows[row.RowIndex]["msbf_act"] = stus;

                    gvaccdetails.DataSource = dt;
                    gvaccdetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                gvaccdetails.EditIndex = -1;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void txtfacilityamy_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        protected void txtutilamt_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void CalculateBalance()
        {
            try
            {
                Decimal facility = 0;
                Decimal utility = 0;

                if (!string.IsNullOrEmpty(txtfacilityamy.Text))
                {
                    facility = Convert.ToDecimal(txtfacilityamy.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtutilamt.Text))
                {
                    utility = Convert.ToDecimal(txtutilamt.Text.Trim());
                }

                Balance = (facility) - (utility);
                txtbal.Text = Balance.ToString();
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

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        protected void lbtnbank_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
                    lbtnbank.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable result = CHNLSVC.CommonSearch.SearchBankBranchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "82";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
                    lbtnbank.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtbranch.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the branch !!!')", true);
                    LinkButton3.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = CHNLSVC.General.SearchBankAccounts(SearchParams, null,null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            try
            {

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtbankcode_TextChanged(object sender, EventArgs e)
        {
            LoadBank();
            GetBankDetails("textchangebankcode");
        }

        protected void txtbranch_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
            //    lbtnbank.Focus();
            //    txtbranch.Text = string.Empty;
            //    return;
            //}

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
            //DataTable result = CHNLSVC.CommonSearch.SearchBankBranchData(SearchParams,"CODE",txtbranch.Text.Trim());

            //DataView dv = new DataView(result);

            //if (dv.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid branch code !!!')", true);
            //    txtbranch.Text = string.Empty;
            //    txtbranch.Focus();
            //    return;
            //}
            //else
            //{
            //    txtbranch.Text = txtbranch.Text.ToUpper().Trim();
            //}
            LoadBranch();
            GetBankDetails("textchangebranchcode");
        }

        protected void txtaccno_TextChanged(object sender, EventArgs e)
        {
            LoadAccount();
            GetBankDetails("textchangeacccode");
        }

        protected void txtcurr_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
            DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, "Code", txtcurr.Text.Trim());

            DataView dv = new DataView(result);

            if (dv.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid currency !!!')", true);
                txtcurr.Text = string.Empty;
                txtcurr.Focus();
                return;
            }
            else
            {
                txtcurr.Text = txtcurr.Text.ToUpper().Trim();
            }
        }

        //Modified by kelum : load bank account facility to grid : 2016-May-16
        private void GetBankDetails(string method) 
        {
            string txtbankcodeval = txtbankcode.Text.ToUpper().ToString();

            string txtbranchcodeyval = txtbranch.Text.ToUpper().ToString();

            string txtacccodeyval = txtaccno.Text.ToUpper().ToString();

            if (method == "btnchangebankcode")
            {
                txtbankcode.Text = grdResult.SelectedRow.Cells[1].Text;
                txtbankcodeval = txtbankcode.Text;
            }

            else if (method == "btnchangebranchcode")
            {
                txtbranch.Text = grdResult.SelectedRow.Cells[1].Text;
                txtbranchcodeyval = txtbranch.Text;
            }

            else if (method == "btnchangeacccode")
            {
                txtaccno.Text = grdResult.SelectedRow.Cells[1].Text;
                txtacccodeyval = txtaccno.Text;
            }

            else if (method == "textchangebankcode" & grdResult.Rows.Count > 0)
            {
                GridViewRow bank = grdResult.Rows[0];

                txtbankcode.Text = bank.Cells[1].Text;              

                txtbankcodeval = txtbankcode.Text;

            }

            else if (method == "textchangebranchcode" & grdResult.Rows.Count > 0)
            {
                GridViewRow branch = grdResult.Rows[0];

                txtbranch.Text = branch.Cells[1].Text;

                txtbranchcodeyval = txtbranch.Text;

            }


            else if (method == "textchangeacccode" & grdResult.Rows.Count > 0)
            {
                GridViewRow account = grdResult.Rows[0];

                txtaccno.Text = account.Cells[1].Text;

                txtacccodeyval = txtaccno.Text;

            }

            BankAccountFacility FacilityData = new BankAccountFacility();
            FacilityData.MSBF_COM = Session["UserCompanyCode"].ToString();
            FacilityData.MSBF_CD = txtbankcode.Text;

            DataTable dtaccdata = CHNLSVC.General.LoadBankAccountFacilityData(FacilityData);
            
            DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

            gvaccdetails.DataSource = null;
            gvaccdetails.DataBind();

           

           dt.Merge(dtaccdata, true, MissingSchemaAction.Ignore);

            //Modified by kelum : filter bank facility data : 2016-May-17

            DataView filterbybank = new DataView(dt);

            filterbybank.RowFilter = "msbf_cd ='" + txtbankcodeval + "'";

            if (filterbybank.Count > 0)
            {
                dt = filterbybank.ToTable();

                var qryLatestInterview = from rows in dt.AsEnumerable()
                                         group rows by new
                                         {
                                             PositionID = rows["msbf_acc_cd"],
                                             CandidateID = rows["msbf_fac_tp"],
                                         } into grp
                                         select grp.OrderBy(x => x["msbf_fac_dt"]).First();

                if (dt.Rows.Count > 0)
                {
                    dt = qryLatestInterview.CopyToDataTable();

                }

                ////sort by facility date
                //DataView view = dt.DefaultView;
                //view.Sort = "msbf_fac_dt";
                //dt = view.ToTable();
                ////

                gvaccdetails.DataSource = null;
                gvaccdetails.DataSource = dt;
    
                gvaccdetails.DataBind();
                
                ViewState["ACCOUNT_FACILITY"] = dt;
                
               
            }

            else
            {
                gvaccdetails.DataSource = null;
                gvaccdetails.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found for this bank code')", true);
                txtbankcode.Focus();
                lblvalue.Text = "";
                SIPopup.Hide();
                return;
            }


            //check branch code added
            if (!string.IsNullOrEmpty(txtbranchcodeyval))
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "msbf_branch_cd ='" + txtbranchcodeyval + "'";

                if (dv.Count > 0)
                {
                    dt = dv.ToTable();

                    //sort by facility date
                    //DataView view = dt.DefaultView;
                    //view.Sort = "msbf_fac_dt";
                    //dt = view.ToTable();
                    //

                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataSource = dt;
                    gvaccdetails.DataBind();
                    ViewState["BANK_ACCOUNTS"] = dt;
                }

                else
                {
                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataBind();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found for this branch code')", true);
                    txtbranch.Focus();
                    return;
                }

            }

            //check acc code added
            if (!string.IsNullOrEmpty(txtacccodeyval))
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "msbf_acc_cd ='" + txtacccodeyval + "'";

                if (dv.Count > 0)
                {
                    dt = dv.ToTable();

                    //sort by facility date
                    //DataView view = dt.DefaultView;
                    //view.Sort = "msbf_fac_dt";
                    //dt = view.ToTable();
                    //

                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataSource = dt;
                    gvaccdetails.DataBind();
                    ViewState["BANK_ACCOUNTS"] = dt;
                }

                else
                {
                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataBind();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found for this account code')", true);
                    txtaccno.Focus();
                    return;
                }

            }

           //
           //  FormatNumbers();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);


        }

        private void LoadBank()
        {
            try
            {
                
                ViewState["SEARCH"] = null;

                string txtbankcodey = "";
                txtbankcodey = txtbankcode.Text.ToUpper().ToString();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = CHNLSVC.CommonSearch.SearchBank(SearchParams);

                DataView dv = new DataView(result);

                dv.RowFilter = "" + "CODE" + " like '%" + txtbankcode.Text.ToUpper().Trim() + "%'";

                if (dv.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid bank code !!!')", true);
                    txtbankcode.Text = string.Empty;
                    txtbankcode.Focus();
                    grdResult.DataSource = null;
                    grdResult.DataBind();

                    return;
                }
                if (dv.Count > 0)
                {
                    result = dv.ToTable();

                    grdResult.DataSource = result;
                    grdResult.DataBind();
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

        private void LoadBranch()
        {
            try
            {

                ViewState["SEARCH"] = null;

                string txtbranchCode = "";
                txtbranchCode = txtbranch.Text.ToUpper().ToString();


                if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
                    lbtnbank.Focus();
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable branchresult = CHNLSVC.CommonSearch.SearchBankBranchData(SearchParams, null, null);
                ViewState["SEARCH"] = branchresult;

                if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);

                    dv.RowFilter = "Code ='" + txtbranchCode + "'";

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();

                        grdResult.DataSource = result;
                        grdResult.DataBind();
                    }
                    else
                    {
                        grdResult.DataSource = null;
                        grdResult.DataBind();

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

        private void LoadAccount()
        {
            try
            {
                ViewState["SEARCH"] = null;
              
                string txtacccodeyval = "";
                txtacccodeyval = txtaccno.Text.ToUpper().ToString();

                if (string.IsNullOrEmpty(txtbankcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bank !!!')", true);
                    lbtnbank.Focus();
                    txtaccno.Text = string.Empty;
                    return;
                }

                if (string.IsNullOrEmpty(txtbranch.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the branch !!!')", true);
                    LinkButton3.Focus();
                    txtaccno.Text = string.Empty;
                    return;
                }


                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable accountsresult = CHNLSVC.General.SearchBankAccounts(SearchParams, "Account", txtaccno.Text.Trim());

                //DataView dv = new DataView(result);

                //if (dv.Count == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid account # !!!')", true);
                //    txtaccno.Text = string.Empty;
                //    txtaccno.Focus();
                //    return;
                //}
                //else
                //{
                //    txtaccno.Text = txtaccno.Text.ToUpper().Trim();
                //}

                ViewState["SEARCH"] = accountsresult;

                if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);

                    dv.RowFilter = "Account ='" + txtacccodeyval + "'";

                    if (dv.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid account # !!!')", true);
                        txtaccno.Text = string.Empty;
                        txtaccno.Focus();
                        return;
                    }

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();

                        grdResult.DataSource = result;
                        grdResult.DataBind();
                    }
                    else
                    {
                        grdResult.DataSource = null;
                        grdResult.DataBind();

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
        
    }
}