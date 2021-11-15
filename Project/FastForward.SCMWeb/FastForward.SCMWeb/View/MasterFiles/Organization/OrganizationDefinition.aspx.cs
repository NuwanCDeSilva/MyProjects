using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Organization
{
    public partial class OrganizationDefinition : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = null;
                Session["BankAll"] = null;
                Session["BranchBankId"] = null;
                Session["BankBranch"] = null;
                Session["ImportAgent"] = null;
                Session["BrImportAgent"] = null;
                if (lblSearchType.Text == "BankAll")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
                    _result = CHNLSVC.CommonSearch.GetBusinessCompanyDataByCountry(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BankAll"] = _result;
                }
                if (lblSearchType.Text == "BranchBankId")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
                    _result = CHNLSVC.CommonSearch.GetBusinessCompanyDataByCountry(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BranchBankId"] = _result;
                }
                if (lblSearchType.Text == "BankBranch")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    _result = CHNLSVC.CommonSearch.SearchBankBranchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BankBranch"] = _result;
                }
                if (lblSearchType.Text == "ImportAgent")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ImportAgent"] = _result;
                }
                if (lblSearchType.Text == "BrImportAgent")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BrImportAgent"] = _result;
                }
               
                dgvResultItem.DataSource = null; ;
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
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

        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "BankAll")
                {
                    _result = (DataTable)Session["BankAll"];
                }
                if (lblSearchType.Text == "BranchBankId")
                {
                    _result = (DataTable)Session["BranchBankId"];
                }
                if (lblSearchType.Text == "BankBranch")
                {
                    _result = (DataTable)Session["BankBranch"];
                }
                if (lblSearchType.Text == "ImportAgent")
                {
                    _result = (DataTable)Session["ImportAgent"];
                }
                if (lblSearchType.Text == "BrImportAgent")
                {
                    _result = (DataTable)Session["BrImportAgent"];
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                    "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "BankAll")
                {
                    txtBankId.Text = code;
                    if (!string.IsNullOrEmpty(txtBankId.Text))
                    {
                        load_bank_det();
                    }
                }
                if (lblSearchType.Text == "BranchBankId")
                {
                   txtBrBankId.Text = code;
                   txtBranchCode.Text = "";
                   txtBranchName.Text = "";
                   chkBranchActive.Checked = false;
                    load_branch_bank_det();
                }
                if (lblSearchType.Text == "BankBranch")
                {
                   txtBranchCode.Text = code;
                   if (!string.IsNullOrEmpty(txtBranchCode.Text))
                   {
                       load_branch_det();
                   }
                }
                if (lblSearchType.Text == "ImportAgent")
                {
                    txtCountryCode.Text = code;
                    txtCountryCode_TextChanged(null,null);
                }
                if (lblSearchType.Text == "BrImportAgent")
                {
                    txtBrCountry.Text = code;
                    txtBrCountry_TextChanged(null, null);
                }
                
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void load_branch_bank_det()
        {
            lblBankName.Text = "";
            lblBankId.Text = "";
            lblBranchBank.Text = "";
            DataTable BankName = CHNLSVC.Sales.get_Bank_Name(txtBrBankId.Text);
            foreach (DataRow row2 in BankName.Rows)
            {
                lblBankName.Text = row2["mbi_desc"].ToString();
                lblBankId.Text = row2["mbi_cd"].ToString();
                lblBranchBank.Text = row2["mbi_desc"].ToString();
            }
        }
        protected void lbtnSeBankId_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BankAll";
                hdfBankType.Value = "Bank";
                Session["BankAll"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyDataByCountry(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BankAll"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                hdfBankType.Value = "0";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                hdfBankType.Value = "Bank";
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

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }
        
        protected void lbtnSeBrBankId_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BranchBankId";
                hdfBankType.Value = "Branch";
                Session["BranchBankId"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyDataByCountry(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BranchBankId"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                hdfBankType.Value = "0";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                hdfBankType.Value = "Branch";
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeBrCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBrBankId.Text == "")
                {
                    DisplayMessage("Select branch bank id ");
                    return;
                }
                lblSearchType.Text = "BankBranch";
                Session["BankBranch"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BankBranch"] = _result;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnBankSave_Click(object sender, EventArgs e)
          {
              try
              {
                  SaveBankData();
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

        protected void lbtnBankClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                txtBankId.Text = "";
                txtBankCode.Text = "";
                txtBankName.Text = "";
                txtSunCode.Text = "";
                lblBank.Text = "";
                chkBankActive.Checked = false;
                hdfClearData.Value = "0";
                hdfBankType.Value = "0";
                txtCountryCode.Text = "";
                lblCountryCode.Text="";
            }
        }

        protected void lbtnBranchSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveBranch();
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

        private void SaveBankData()
        {
            string _CompCode = "";
            if (txtCountryCode.Text == "")
            {
                string msg = "Please enter the country code";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBankId.Text == "")
            {
                string msg = "Please enter the bank id ";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBankCode.Text == "")
            {
                string msg = "Please enter the bank code";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBankName.Text == "")
            {
                string msg = "Please enter the bank name";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtSunCode.Text == "")
            {
                string msg = "Please enter the SUN code";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBankId.Text == "")
            {
                string msg = "Please enter the bank id ";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtBankCode.Text))
            {
                if (IsNumeric(txtBankCode.Text) == false)
                { 
                    txtBankCode.Focus();
                    string msg = "The Bank code is invalid !";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
                    return;
                }
            }
            if (txtSunCode.Text.Length > 5)
            {
                //  string msg = txtSunCode.Text + " is invalid !";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('The sun code length should be less than 5 !!!')", true);
                txtBankCode.Focus();
                return;
            }

            bool b2 = false;
            string bankCode = "";
            hdfBankType.Value = "Bank";
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyDataByCountry(para, null, null);
            foreach (DataRow row in _result.Rows)
            {
                if (!string.IsNullOrEmpty(row["ID"].ToString()))
                {
                    if (txtBankId.Text == row["ID"].ToString())
                    {
                        b2 = true;
                        bankCode = row["Code"].ToString();
                        break;
                    }
                }
            }
            
            if (b2 && txtBankCode.Text!=bankCode)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('The bank id is already exists !!!')", true);
                return;
            }
          
            MasterOutsideParty _outsideParty = new MasterOutsideParty();

            _outsideParty.Mbi_cd = txtBankCode.Text;
            _outsideParty.Mbi_country_cd =txtCountryCode.Text;
            _outsideParty.Mbi_desc = txtBankName.Text;
            _outsideParty.Mbi_tp = "BANK";
            _outsideParty.Mbi_id = txtBankId.Text;
            _outsideParty.Mbi_act = chkBankActive.Checked;
            _outsideParty.Mbi_sun_bank = txtSunCode.Text;
            _outsideParty.Mbi_add1 = "";
            _outsideParty.Mbi_add2 = "";
            _outsideParty.Mbi_tel = "";
            _outsideParty.Mbi_fax = "";
            _outsideParty.Mbi_email = "";
            _outsideParty.Mbi_web = "";
            _outsideParty.Mbi_town_cd = "";
            _outsideParty.Mbi_tax1 = "";
            _outsideParty.Mbi_tax2 = "";
            _outsideParty.Mbi_tax3 = "";
            _outsideParty.Mbi_cre_by = Session["UserID"].ToString();
            _outsideParty.Mbi_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_mod_by = Session["UserID"].ToString();
            _outsideParty.Mbi_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_session_id = Session["SessionID"].ToString();

            DataTable BankNames = CHNLSVC.Sales.get_Bank_Name(txtBankId.Text);
            string dbMode = "";
            if (BankNames.Rows.Count > 0)
            {
                dbMode = "Update";
            }
            else
            {
                dbMode = "Save";
            }

            int row_aff = CHNLSVC.General.SaveOutsideParty(_outsideParty, null, out _CompCode);
            if (row_aff != -99 && row_aff >= 0)
            {
                hdfClearData.Value = "1";
                lbtnBankClear_Click(null, null);
                if (dbMode == "Save")
                {
                    string msg = "Successfully saved !";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                }
                if(dbMode=="Update")
                {
                    string msg = "Successfully updated !";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }


        private void SaveBranch()
        {
            string _errr = "";
            if (txtBrCountry.Text == "")
            {
                string msg = "Please enter the country code";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBranchCode.Text == "")
            {
                string msg = "Please enter the branch code !";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (txtBranchName.Text == "")
            {
                string msg = "Please enter the branch name !";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
            {
                if (IsNumeric(txtBranchCode.Text) == false)
                {
                    txtBranchCode.Focus();
                    string msg = "The branch code is invalid !";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
                    
                    return;
                }
            }
            //if (txtBranchCode.Text.Length > 5)
            //{
            //    //  string msg = txtSunCode.Text + " is invalid !";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('The sun code length should be less than 5 !!!')", true);
            //    txtBankCode.Focus();
            //    return;
            //}

            MasterBankBranch _bankBranch = new MasterBankBranch();
            _bankBranch.Mbb_country_cd =txtBrCountry.Text;
            _bankBranch.Mbb_bus_cd = lblBankId.Text;
            _bankBranch.Mbb_cd = txtBranchCode.Text;
            _bankBranch.Mbb_desc = txtBranchName.Text;
            _bankBranch.Mbb_active = Convert.ToBoolean(chkBranchActive.Checked);
            _bankBranch.Mbb_cre_by = Session["UserID"].ToString();
            _bankBranch.Mbb_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _bankBranch.Mbb_mod_by = Session["UserID"].ToString();
            _bankBranch.Mbb_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _bankBranch.Mbb_session_id = Session["SessionID"].ToString();
            DataTable BankName = CHNLSVC.Sales.get_Branch_Name(lblBankId.Text, txtBranchCode.Text);
            string dbMode = "";
            if (BankName.Rows.Count > 0)
            {
                dbMode = "Update";
            }
            else
            {
                dbMode = "Save";
            }
            int row_aff = CHNLSVC.General.SaveBankBranch(_bankBranch, out _errr);
            if (row_aff != -99 && row_aff >= 0)
            {
                hdfClearData.Value = "1";
                lbtnBranchClear_Click(null, null);
                    if (dbMode == "Save")
                    {
                        string msg = "Successfully saved ! ";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                    }
                    if(dbMode=="Update")
                    {
                        string msg = "Successfully updated !";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                    }
                   // lbtnBranchClear_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnBranchClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                txtBrBankId.Text = "";
                txtBranchCode.Text = "";
                lblBankId.Text = "";
                txtBranchName.Text = "";
                lblBranchBank.Text = "";
                chkBranchActive.Checked = false;
                hdfClearData.Value = "0";
                txtBrCountry.Text = "";
                lblBrCountry.Text = "";
                hdfBankType.Value = "0";
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.BankALL:
                    {
                        if (hdfBankType.Value=="Bank")
                            paramsText.Append(txtCountryCode.Text+seperator+"BANK"+seperator);
                        if (hdfBankType.Value == "Branch")
                            paramsText.Append(txtBrCountry.Text + seperator + "BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(lblBankId.Text.Trim() + seperator );
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void load_bank_det()
        {
            txtBankName.Text = "";
            txtSunCode.Text = "";
            txtBankCode.Text = "";
            chkBankActive.Checked = false;
            //if (!string.IsNullOrEmpty(txtBankId.Text))
            //{
            //    if (IsNumeric(txtBankId.Text) == false)
            //    {
            //        txtBankCode.Focus();
            //        string msg = "Bank id is invalid !";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
            //        return;
            //    }
            //}
            DataTable BankNames = CHNLSVC.Sales.get_Bank_Name(txtBankId.Text);
            if (BankNames.Rows.Count > 0)
            {
                foreach (DataRow row2 in BankNames.Rows)
                {
                    lblBank.Text = row2["mbi_desc"].ToString();
                    txtBankName.Text = row2["mbi_desc"].ToString();
                    txtSunCode.Text = row2["mbi_sun_bank"].ToString();
                    txtBankCode.Text = row2["mbi_cd"].ToString();
                    chkBankActive.Checked = Convert.ToBoolean(row2["mbi_act"]);
                }
            }
        }
        private void load_branch_det()
        {
            try
            {
                txtBranchName.Text = "";
                chkBranchActive.Checked = false;

                DataTable BankName = CHNLSVC.Sales.get_Branch_Name(lblBankId.Text, txtBranchCode.Text);
                foreach (DataRow row2 in BankName.Rows)
                {
                    txtBranchName.Text = row2["mbb_desc"].ToString();
                    bool b = false;
                    try
                    {
                        if (!string.IsNullOrEmpty(row2["mbb_active"].ToString()))
                        {
                            b = Convert.ToBoolean(row2["mbb_active"]);
                        }
                    }
                    catch (Exception)
                    {
                        b = false;
                    }
                    chkBranchActive.Checked = b;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void DisplayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }
        protected void lBtnDilogResultYes_Click(object sender, EventArgs e)
        {
            hdfDilogResult.Value = "1";
            if (hdfSelectedButton.Value == "lbtnBankSave")
            {
                lbtnBankSave_Click(null, null);
            }
            if (hdfSelectedButton.Value == "lbtnBranchSave")
            {
                lbtnBranchSave_Click(null, null);
            }
        }

        protected void lBtnDilogResultNo_Click(object sender, EventArgs e)
        {
            hdfDilogResult.Value = "2";
            hdfSelectedButton.Value = "";
        }

        protected void txtBankId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBankId.Text))
                load_bank_det();
        }

        protected void txtBranchCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
            {
                if (IsNumeric(txtBranchCode.Text) == false)
                {
                    txtBranchCode.Focus();
                    string msg = txtBranchCode.Text + " is invalid !";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
                load_branch_det();
        }

        protected void lbtnSeCountryCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ImportAgent";
                Session["ImportAgent"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnBrSeCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BrImportAgent";
                Session["BrImportAgent"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BrImportAgent"] = _result;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void txtCountryCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCountryCode.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCountryCode.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                           // return;
                        }
                    }
                }
                txtCountryCode.ToolTip = b2 ? toolTip : "";
                lblCountryCode.Text = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid country !!!')", true);
                    txtCountryCode.Text = "";
                    txtCountryCode.Focus();
                    return;
                }
            }
            else
            {
                txtCountryCode.ToolTip = "";
            }
        }

        protected void txtBrCountry_TextChanged(object sender, EventArgs e)
        {
            if (txtBrCountry.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtBrCountry.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                           // return;
                        }
                    }
                }
                txtBrCountry.ToolTip = b2 ? toolTip : "";
                lblBrCountry.Text = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid country !!!')", true);
                    txtBrCountry.Text = "";
                    txtBrCountry.Focus();
                    return;
                }
            }
            else
            {
                txtBrCountry.ToolTip = "";
            }
        }
    }
}