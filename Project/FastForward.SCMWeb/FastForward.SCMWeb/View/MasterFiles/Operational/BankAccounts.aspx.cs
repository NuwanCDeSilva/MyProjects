using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class BankAccounts : BasePage
    {
        string _userid = string.Empty;
        List<Int32> successlist = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime date = DateTime.Now;
                    dtpFromDate.Text = date.ToString("dd/MMM/yyyy");

                    gvaccdetails.DataSource = new int[] { };
                    gvaccdetails.DataBind();

                    ViewState["BANK_ACCOUNTS"] = null;
                    DataTable dtbankaccounts = new DataTable();
                    dtbankaccounts.Columns.AddRange(new DataColumn[7] { new DataColumn("msba_cd"), new DataColumn("msba_desc"), new DataColumn("msba_acc_cd"), new DataColumn("msba_acc_desc"), new DataColumn("msba_acc_dt"), new DataColumn("msba_brn_cd"), new DataColumn("msba_stus") });
                    ViewState["BANK_ACCOUNTS"] = dtbankaccounts;
                    this.BindGrid();
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
                dtpFromDate.Text = date.ToString("dd/MMM/yyyy");

                gvaccdetails.DataSource = new int[] { };
                gvaccdetails.DataBind();

                ViewState["BANK_ACCOUNTS"] = null;
                DataTable dtbankaccounts = new DataTable();
                dtbankaccounts.Columns.AddRange(new DataColumn[7] { new DataColumn("msba_cd"), new DataColumn("msba_desc"), new DataColumn("msba_acc_cd"), new DataColumn("msba_acc_desc"), new DataColumn("msba_acc_dt"), new DataColumn("msba_brn_cd"), new DataColumn("msba_stus") });
                ViewState["BANK_ACCOUNTS"] = dtbankaccounts;
                this.BindGrid();

                txtbankcode.Text = string.Empty;
                lblbankname.Text = string.Empty;
                txtbranch.Text = string.Empty;
                txtaccno.Text = string.Empty;
                txtdesc.Text = string.Empty;
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

        protected void BindGrid()
        {
            try
            {
                gvaccdetails.DataSource = (DataTable)ViewState["BANK_ACCOUNTS"];
                gvaccdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtbankcode.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void FilterData()
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable intresult = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                ViewState["SEARCH"] = intresult;

                if (lblvalue.Text == "1")
                {
                    
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
                    //Modified by kelum : load bank aacount by bank code : moved to another method : 2016-May-14
                    GetBankDetails("btnchangebankcode");
                    /*
                    txtbankcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblbankname.Text = grdResult.SelectedRow.Cells[2].Text;

                    MasterBankAccount AccountData = new MasterBankAccount();
                    AccountData.Msba_com = Session["UserCompanyCode"].ToString();
                    AccountData.Msba_acc_cd = txtbankcode.Text;

                    DataTable dtaccdata = CHNLSVC.General.LoadBankAccountData(AccountData);

                    DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];

                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataBind();

                    dt.Merge(dtaccdata, true, MissingSchemaAction.Ignore);

                    DataTable uniqueColscurrency = RemoveDuplicateRows(dt, "msba_acc_cd");

                    gvaccdetails.DataSource = dt;
                    gvaccdetails.DataBind();
                    ViewState["BANK_ACCOUNTS"] = dt;

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    */

                    lblvalue.Text = "";
                    SIPopup.Hide();
                    return; 
                }
                else if (lblvalue.Text == "82")
                {
                    GetBankDetails("btnchangebranchcode");
                    txtbranch.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
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

        protected void txtbankcode_TextChanged(object sender, EventArgs e)
        {  
           //Modified by kelum : 2016-May-14
           // FilterData();
            
            LoadBank();
            GetBankDetails("textchangebankcode");
        }
        
        protected void lbtnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string txtbankcodeval = txtbankcode.Text.ToUpper().ToString();

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

                DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];

                string stus = "Active";

                if (ddlstus.SelectedValue == "0")
                {
                    stus = "Inactive";
                }

                               
                //Kelum : restrict duplicate acc number from grid : 2013-May-14
                checkDuplicateAccNo();

                if (dt != null)
                {

                    foreach (GridViewRow dtRow in gvaccdetails.Rows)
                    {                      
                        Label lblbankcode = (Label)dtRow.FindControl("lblacc");
                        string grdacccode = lblbankcode.Text;

                        if (grdacccode == txtaccno.Text.Trim()) 
                        {

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This Account Number already exist')", true);
                            return;
                        }

                    }

                }
 
                //

                dt.Rows.Add(txtbankcode.Text.Trim(), lblbankname.Text.Trim(), txtaccno.Text.Trim(), txtdesc.Text.Trim(), dtpFromDate.Text, txtbranch.Text.Trim(), stus);

                ViewState["BANK_ACCOUNTS"] = dt;
                gvaccdetails.DataSource = dt;
                gvaccdetails.DataBind();

                txtbankcode.Text = txtbankcodeval;
                lblbankname.Text = lblbankname.Text.Trim();
                txtbranch.Text = string.Empty;
                txtaccno.Text = string.Empty;
                txtdesc.Text = string.Empty;
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

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvaccdetails.Rows.Count == 0)
                {
                    string msg = "Please enter/load bank account details";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    return;
                }

                if (txtconfirmplaceord.Value == "Yes")
                {
                    foreach (GridViewRow hiderowbtn in this.gvaccdetails.Rows)
                    {
                        Label lblbankcode = (Label)hiderowbtn.FindControl("lblbankcode");
                        Label lblbankname = (Label)hiderowbtn.FindControl("lblbankname");
                        Label lblacc = (Label)hiderowbtn.FindControl("lblacc");
                        Label lblaccdesc = (Label)hiderowbtn.FindControl("lblaccdesc");
                        Label lblopendate = (Label)hiderowbtn.FindControl("lblopendate");
                        Label lblbranchcode = (Label)hiderowbtn.FindControl("lblbranchcode");
                        Label lblstus = (Label)hiderowbtn.FindControl("lblstus");

                        bool stusval = true;

                        if (lblstus.Text == "Inactive")
                        {
                            stusval = false;
                        }

                        _userid = (string)Session["UserID"];

                        MasterBankAccount Account = new MasterBankAccount();
                        Account.Msba_com = Session["UserCompanyCode"].ToString();
                        Account.Msba_cd = lblbankcode.Text.ToUpper().Trim();
                        Account.Msba_desc = lblbankname.Text.Trim();
                        Account.Msba_acc_cd = lblacc.Text.ToUpper().Trim();
                        Account.Msba_acc_desc = lblaccdesc.Text.Trim();
                        Account.Msba_acc_dt = Convert.ToDateTime(lblopendate.Text);
                        Account.Msba_stus = stusval;
                        Account.Msba_cre_by = _userid;
                        Account.Msba_cre_dt = DateTime.Now.Date;
                        Account.Msba_mod_by = _userid;
                        Account.Msba_mod_dt = DateTime.Now.Date;
                        Account.Msba_brn_cd = lblbranchcode.Text.ToUpper().Trim();

                        Int32 results = CHNLSVC.General.UpdateBankAccounts(Account);
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

                DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];

                gvaccdetails.DataSource = dt;
                gvaccdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string desc = (row.FindControl("txtaccdesc") as TextBox).Text;
                    string accountno = (row.FindControl("lblacc") as Label).Text;
                    string stus = (row.FindControl("ddlstatus") as DropDownList).SelectedValue;

                    gvaccdetails.EditIndex = -1;

                    DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];

                    string statusvalue = "Active";

                    if (stus == "0")
                    {
                        statusvalue = "Inactive";
                    }
                    dt.Rows[row.RowIndex]["msba_acc_desc"] = desc;
                    dt.Rows[row.RowIndex]["msba_acc_cd"] = accountno;
                    dt.Rows[row.RowIndex]["msba_stus"] = statusvalue;

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
                DataTable result = CHNLSVC.CommonSearch.SearchBankBranchData(SearchParams,null,null);
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

        //Modified by kelum : load bank details to grid : 2016-May-14
        private void GetBankDetails(string method)
        {
            //txtbankcode.Text = "";
            //lblbankname.Text = "";
            try
            {
                string txtbankcodeval = txtbankcode.Text.ToUpper().ToString();

                string txtbranchcodeyval = txtbranch.Text.ToUpper().ToString();


                if (method == "textchangebankcode" & grdResult.Rows.Count > 0)
                {
                    GridViewRow bank = grdResult.Rows[0];

                    txtbankcode.Text = bank.Cells[1].Text;
                    lblbankname.Text = bank.Cells[2].Text;

                    txtbankcodeval = txtbankcode.Text;

                }
                else if (method == "btnchangebankcode")
                {
                    txtbankcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblbankname.Text = grdResult.SelectedRow.Cells[2].Text;

                    txtbankcodeval = txtbankcode.Text;
                }

                else if (method == "textchangebranchcode" & grdResult.Rows.Count > 0)
                {
                    GridViewRow branch = grdResult.Rows[0];

                    txtbranch.Text = branch.Cells[1].Text;

                }

                else if (method == "btnchangebranchcode")
                {
                    txtbranch.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtbranchcodeyval = grdResult.SelectedRow.Cells[1].Text;
                }

                MasterBankAccount AccountData = new MasterBankAccount();
                AccountData.Msba_com = Session["UserCompanyCode"].ToString();
                AccountData.Msba_acc_cd = txtbankcode.Text;

                DataTable dtaccdata = CHNLSVC.General.LoadBankAccountData(AccountData);



                DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];
                dt.Merge(dtaccdata, true, MissingSchemaAction.Ignore);

                DataTable uniqueColscurrency = RemoveDuplicateRows(dt, "msba_acc_cd");

                DataView filterbybank = new DataView(dt);

                filterbybank.RowFilter = "msba_cd ='" + txtbankcodeval + "'";

                if (filterbybank.Count > 0)
                {
                    dt = filterbybank.ToTable();              

                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataSource = dt;
                    gvaccdetails.DataBind();
                    ViewState["BANK_ACCOUNTS"] = dt;
                }

                else
                {
                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataBind();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found for this bank code')", true);
                    txtbankcode.Focus();
                    return;
                }


                //check branch code added
                if (!string.IsNullOrEmpty(txtbranchcodeyval))
                {
                    DataView dv = new DataView(dt);

                    dv.RowFilter = "msba_brn_cd ='" + txtbranchcodeyval + "'";

                    if (dv.Count > 0)
                    {
                        dt = dv.ToTable();

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
                //else
                //{
                //    gvaccdetails.DataSource = null;
                //    gvaccdetails.DataSource = dt;
                //    gvaccdetails.DataBind();
                //    ViewState["BANK_ACCOUNTS"] = dt;

                //} 
                
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
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

        //Modified by kelum : load bank : moved to another method : 2016-May-14
        private void LoadBank() 
        {
            //txtbankcode.Text = "";
            //lblbankname.Text = "";
            try
            {

                ViewState["SEARCH"] = null;

                string txtbankcodey = "";
                txtbankcodey = txtbankcode.Text.ToUpper().ToString();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable intresult = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                ViewState["SEARCH"] = intresult;

                if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);

                    dv.RowFilter = "CODE ='" + txtbankcodey + "'";

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

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found for this bank code')", true);
                        txtbankcode.Focus();
                        return;
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
        
        protected void txtbranch_TextChanged(object sender, EventArgs e)
        {
            //Modified by kelum : 2016-May-14
            // FilterData();
            LoadBranch();
            GetBankDetails("textchangebranchcode");            
          
           
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

        protected void txtaccno_TextChanged(object sender, EventArgs e)
        {
            checkDuplicateAccNo();       

        }

        protected void checkDuplicateAccNo() 
        {
            try
            {
                string txtbankcodeval = txtbankcode.Text.ToUpper().ToString();

                string txtbranchcodeyval = txtbranch.Text.ToUpper().ToString();

                string txtacccodeyval = txtaccno.Text.ToUpper().ToString();

                MasterBankAccount AccountData = new MasterBankAccount();
                AccountData.Msba_com = Session["UserCompanyCode"].ToString();
                AccountData.Msba_acc_cd = txtbankcode.Text;

                DataTable dtaccdata = CHNLSVC.General.LoadBankAccountData(AccountData);

                DataTable dt = (DataTable)ViewState["BANK_ACCOUNTS"];
                dt.Merge(dtaccdata, true, MissingSchemaAction.Ignore);

                DataTable uniqueColscurrency = RemoveDuplicateRows(dt, "msba_acc_cd");

                DataView filterbybank = new DataView(dt);

                filterbybank.RowFilter = "msba_cd ='" + txtbankcodeval + "'";

                if (filterbybank.Count > 0)
                {
                    dt = filterbybank.ToTable();
                }

                //Kelum : restrict duplicate acc number from grid : 2013-May-14

                if (dt != null)
                {

                    foreach (DataRow dtRow in dt.Rows)
                    {
                        string bankrow = dtRow["msba_cd"].ToString();
                        string branchrow = dtRow["msba_brn_cd"].ToString();
                        string accrow = dtRow["msba_acc_cd"].ToString();

                        if (bankrow == txtbankcodeval & branchrow == txtbranchcodeyval & accrow == txtacccodeyval & !(string.IsNullOrEmpty(txtbranchcodeyval)))
                        {
                            
                            txtaccno.Text = "";
                            txtdesc.Text = "";

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This Account Number already exist')", true);
                            return;



                        }

                        if (bankrow == txtbankcodeval & accrow == txtacccodeyval & !(string.IsNullOrEmpty(txtbranchcodeyval)))
                        {
                            txtaccno.Text = "";

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This Account Number belongs to another branch')", true);
                            return;

                            
                        }
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
        
        //
    }
}