using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class AgentDefinition : BasePage
    {
        private Boolean _isExsit = false;
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        private List<BusEntityItem> busItemList = new List<BusEntityItem>();
        private Boolean _isGroup = false;
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private Boolean _isUpdate = false;
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtagcode.Focus();
                    chkactive.Checked = true; 
                }

                if (Session["_isExsit"] == null)
                {
                    Session["_isExsit"] = false; 
                }

                if (Session["_isGroup"] == null)
                {
                    Session["_isGroup"] = false; 
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

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (txtconfirmplaceord.Value == "Yes")
            {
                try
                {
                    string _cusCode = "";
                    Int32 _effect = 0;

                    if (string.IsNullOrEmpty(txtagcode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a agent code !!!')", true);
                        txtagcode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtname.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a name !!!')", true);
                        txtname.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtadd1.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a address !!!')", true);
                        txtadd1.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtbeno.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter BR # !!!')", true);
                        txtbeno.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtcountry.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter/select country code # !!!')", true);
                        txtcountry.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtmob.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter mobile !!!')", true);
                        txtmob.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtemail.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter email !!!')", true);
                        txtemail.Focus();
                        return;
                    }



                    //if (string.IsNullOrEmpty(txttel.Text.Trim()))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter telephone number !!!')", true);
                    //    txttel.Focus();
                    //    return;
                    //}

                    //if (string.IsNullOrEmpty(txtfax.Text.Trim()))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter fax !!!')", true);
                    //    txtfax.Focus();
                    //    return;
                    //}
                    
                    //if (string.IsNullOrEmpty(txtrefno.Text.Trim()))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter ref # !!!')", true);
                    //    txtrefno.Focus();
                    //    return;
                    //}

                    Boolean _isValid = IsValidEmail(txtemail.Text.Trim());

                    if (_isValid == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid email address !!!')", true);
                        txtemail.Text = "";
                        txtemail.Focus();
                        return;
                    }

                    Collect_Cust();

                    Collect_GroupCust();

                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());

                    if (_isExsit == false)
                    {
                        _custProfile.Mbe_cd = txtagcode.Text.Trim();
                        _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList, busItemList, out _cusCode, null, _isExsit, _isGroup, _custGroup);
                    }
                    else
                    {
                        _cusCode = txtagcode.Text.Trim();
                        _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null, busItemList, _custGroup);
                    }

                    if (_effect > 0)
                    {
                        _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                        if (_isExsit == false)
                        {
                            string msg = "New agent created. Agent Code : " + _cusCode;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "')", true);
                        }
                        else
                        {
                            string message = "Existing agent " + txtagcode.Text.Trim() + " updated successfully !!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + message + "')", true);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_cusCode))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            return;
                        }
                    }

                    Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        private void Collect_Cust()
        {
            _userid = (string)Session["UserID"];

            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;
            _custProfile = new MasterBusinessEntity();
            _custProfile.Mbe_acc_cd = txtrefno.Text.Trim().ToUpper();

            if (chkactive.Checked == true)
            {
                _custProfile.Mbe_act = true;
            }
            else
            {
                _custProfile.Mbe_act = false;
            }

            _custProfile.Mbe_add1 = txtadd1.Text.Trim().ToUpper();
            _custProfile.Mbe_add2 = txtadd2.Text.Trim().ToUpper();
            _custProfile.Mbe_agre_send_sms = _isSMS;
            _custProfile.Mbe_br_no = txtbeno.Text.Trim().ToUpper();
            _custProfile.Mbe_cate = "INDIVIDUAL";
            _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
            _isGroup = Convert.ToBoolean(Session["_isGroup"].ToString());
            if (_isExsit == false && _isGroup == false)
            {
                _custProfile.Mbe_cd = null;
            }
            else
            {
                _custProfile.Mbe_cd = txtagcode.Text.Trim();
            }
            _custProfile.Mbe_com = Session["UserCompanyCode"].ToString();
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = txtcountry.Text.Trim();
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cre_by = Session["UserID"].ToString();
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Now);
            _custProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
            _custProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
            _custProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
            _custProfile.Mbe_email = txtemail.Text.Trim().ToUpper();
            _custProfile.Mbe_fax = txtfax.Text.Trim();
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;

            _custProfile.Mbe_is_svat = _isSVAT;

            _custProfile.Mbe_is_tax = _isVAT;
            _custProfile.Mbe_mob = txtmob.Text.Trim();
            _custProfile.Mbe_name = txtname.Text.Trim().ToUpper();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_sub_tp = null;

            _custProfile.Mbe_tax_ex = _TaxEx;
            _custProfile.Mbe_tel = txttel.Text.Trim();
            _custProfile.Mbe_tp = "A";
            _custProfile.Mbe_wr_country_cd = null;
            _custProfile.Mbe_wr_distric_cd = null;
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            _custProfile.Mbe_wr_town_cd = null;
          
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.Mbe_mod_by = _userid;
            _custProfile.Mbe_mod_dt = Convert.ToDateTime(DateTime.Now);
        }

        private void Collect_GroupCust()
        {
            _userid = (string)Session["UserID"];
            _custGroup = new GroupBussinessEntity();
            _custGroup.Mbg_cd = txtagcode.Text.Trim();
            _custGroup.Mbg_name = txtname.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtadd1.Text.Trim();
            _custGroup.Mbg_add2 = txtadd2.Text.Trim();
            _custGroup.Mbg_country_cd = txtcountry.Text.Trim();
            _custGroup.Mbg_tel = txttel.Text.Trim();
            _custGroup.Mbg_fax = txtfax.Text.Trim();
            _custGroup.Mbg_mob = txtmob.Text.Trim();
            _custGroup.Mbg_br_no = txtbeno.Text.Trim();
            _custGroup.Mbg_email = txtemail.Text.Trim();
            _custGroup.Mbg_contact = "";

            if (chkactive.Checked == true)
            {
                _custGroup.Mbg_act = true;
            }
            else
            {
                _custGroup.Mbg_act = false;
            }

            _custGroup.Mbg_is_suspend = false;
            _custGroup.Mbg_cre_by = Session["UserID"].ToString();
            _custGroup.Mbg_cre_dt = Convert.ToDateTime(DateTime.Now);
            _custGroup.Mbg_mod_by = Session["UserID"].ToString();
            _custGroup.Mbg_mod_by = _userid;
            _custGroup.Mbg_mod_dt = Convert.ToDateTime(DateTime.Now);
        }

        private void Clear()
        {
            try
            {
                txtagcode.Text = string.Empty;
                txtname.Text = string.Empty;
                txtadd1.Text = string.Empty;
                txtadd2.Text = string.Empty;
                txtrefno.Text = string.Empty;
                txtcountry.Text = string.Empty;
                txtbeno.Text = string.Empty;
                txtmob.Text = string.Empty;
                txtfax.Text = string.Empty;
                txttel.Text = string.Empty;
                txtemail.Text = string.Empty;
                chkactive.Checked = true;
                txtagcode.Enabled = true;
                txtname.Enabled = true;
                Session["_isExsit"] = null;
                Session["_isUpdate"] = null;
                Session["_isGroup"] = null;
                Session["confmbox"] = null;
                lblbountry.Text = string.Empty;
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "all" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
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
                if (lblvalue.Text == "27")
                {
                    txtcountry.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblbountry.Text = grdResult.SelectedRow.Cells[2].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                else if (lblvalue.Text == "406")
                {
                    txtagcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    GetCustomerDetailsByCode();
                    LoadCountryName();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "27")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "27";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "406")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable result = CHNLSVC.General.SearchAgent(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "406";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
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

        protected void lbtncountry_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "27";
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

        protected void txtagcode_TextChanged(object sender, EventArgs e)
        {
            GetCustomerDetailsByCode();
            LoadCountryName();
        }

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyAllCustCD(string custCD)
        {
            return CHNLSVC.General.GetAllCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            txtbeno.Text = cust.Mbe_br_no;
            txtagcode.Text = cust.Mbe_cd;
            txtmob.Text = cust.Mbe_mob;
            string sextype = cust.Mbe_sex;
            
            txtname.Text = cust.Mbe_name;

            txtadd1.Text = cust.Mbe_add1;
            txtadd2.Text = cust.Mbe_add2;
          
            txtcountry.Text = cust.Mbe_country_cd;
           
            txttel.Text = cust.Mbe_tel;
            txtemail.Text = cust.Mbe_email;

            if (string.IsNullOrEmpty(txtname.Text))
            {
                txtname.Enabled = true;
            }
            else
            {
               // txtname.Enabled = false;
            }

            txtagcode.Enabled = false;

            bool isact = cust.Mbe_act;

            if (isact == true)
            {
                chkactive.Checked = true;
            }
            else
            {
                chkactive.Checked = false;
            }

            txtfax.Text = cust.Mbe_fax;
            txtrefno.Text = cust.Mbe_acc_cd;

            LoadCountryName();
        }

        private void EnableFalselinkButton()
        {
            lbtnsave.Enabled = false;
            lbtnsave.OnClientClick = "return Enable();";
            lbtnsave.CssClass = "buttoncolor";
        }

        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }


        public void LoadCustProfByGrup(GroupBussinessEntity _cust)
        {
            txtbeno.Text = _cust.Mbg_br_no;
            txtagcode.Text = _cust.Mbg_cd;
            txtmob.Text = _cust.Mbg_mob;
            txtname.Text = _cust.Mbg_name;
            txtadd1.Text = _cust.Mbg_add1;
            txtadd2.Text = _cust.Mbg_add2;
          
            txtcountry.Text = _cust.Mbg_country_cd;
            
            txttel.Text = _cust.Mbg_tel;
            txtemail.Text = _cust.Mbg_email;

           
            if (string.IsNullOrEmpty(txtname.Text))
            {
                txtname.Enabled = true;
            }
            else
            {
               // txtname.Enabled = false;
            }

            txtagcode.Enabled = false;

            bool act = _cust.Mbg_act;

            if (act == true)
            {
                chkactive.Checked = true;
            }
            else
            {
                chkactive.Checked = false;
            }
        }

        private void GetCustomerDetailsByCode()
        {
            if (!string.IsNullOrEmpty(txtagcode.Text))
            {
                MasterBusinessEntity custProf = GetbyAllCustCD(txtagcode.Text.Trim().ToUpper());
                custProf = CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), txtagcode.Text.Trim().ToUpper(), "A").FirstOrDefault();
                if (custProf!=null)
                {
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit = true;
                        Session["_isExsit"] = "true";
                        Session["_isUpdate"] = "true";
                        LoadCustProf(custProf);
                    }
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        _isExsit = true;
                        Session["_isExsit"] = "true";
                        Session["_isUpdate"] = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Agent is inactivated !!!')", true);
                        EnableFalselinkButton();
                        LoadCustProf(custProf);
                    }
                }
                else
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == true)
                    {
                        string cusCD = txtagcode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtagcode.Text = cusCD;
                    }

                    GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtagcode.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        LoadCustProfByGrup(_grupProf);
                        Session["_isGroup"] = "true";
                        _isGroup = true;
                    }
                    else
                    {
                        Session["_isGroup"] = "false";
                        _isGroup = false;
                    }
                    Session["_isExsit"] = "false";
                    _isExsit = false;
                    //if (custProf.Mbe_cd == null)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid agent code !!!')", true);
                    //    txtagcode.Text = "";
                    //    Clear();
                    //}
                }
            }
        }

        private void EnableTruelinkButton()
        {
            lbtnsave.Enabled = true;
            lbtnsave.OnClientClick = "UpdateConfirm();";
            lbtnsave.CssClass = "buttonUndocolor";
        }

        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, Session["UserCompanyCode"].ToString());
        }

        //Kelum : get agent by BR# : 2016-May-13
        //send active status as 2 to get both active and inactive agents
        public MasterBusinessEntity GetAgentbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetBusEntityProfile(null, null, null, null, brNo, Session["UserCompanyCode"].ToString(), null,"A", 2);
        }

        //Kelum : get agent by MOB# : 2016-May-13
        //send active status as 2 to get both active and inactive agents
        public MasterBusinessEntity GetAgentbyMobNo(string MobNo)
        {
            return CHNLSVC.Sales.GetBusEntityProfile(null, null, null, null, null, Session["UserCompanyCode"].ToString(), MobNo, "A", 2);
        }

        //Kelum : get agent by BR# : 2016-May-18        
        public MasterBusinessEntity GetAgentbyCode(string brNo)
        {
            return CHNLSVC.Sales.GetBusEntityProfile(null, null, null, null, brNo, Session["UserCompanyCode"].ToString(), null, "A", 2);
        }
        public GroupBussinessEntity GetbyBrNoGrup(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, brNo, null);
        }
        protected void txtbeno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string _current_cust = string.Empty;
                if (!string.IsNullOrEmpty(txtbeno.Text))
                {

                    txtagcode.Enabled = false;
                    EnableTruelinkButton();
                    //Modified by Kelum : get agent profile : 2016-May-13
                    //MasterBusinessEntity custProf = GetbyBrNo(txtbeno.Text.Trim().ToUpper());
                    MasterBusinessEntity custProf = GetAgentbyBrNo(txtbeno.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _current_cust = custProf.Mbe_cd;
                        Session["_isUpdate"] = true;


                        #region DL
                        if (_current_cust != txtagcode.Text)
                        {

                            _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                            if (_isUpdate == true)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Agent already exists for this BR number !!!')", true);
                                Session["confmbox"] = "true";
                                Session["_isExsit"] = "true";
                                txtagcode.Text = custProf.Mbe_cd;
                                txtname.Text = custProf.Mbe_name;
                                txtadd1.Text = custProf.Mbe_add1;
                                txtadd2.Text = custProf.Mbe_add2;
                                txtrefno.Text = custProf.Mbe_acc_cd;
                                txtcountry.Text = custProf.Mbe_country_cd;
                                txtmob.Text = custProf.Mbe_mob;
                                txttel.Text = custProf.Mbe_tel;
                                txtfax.Text = custProf.Mbe_fax;
                                txtemail.Text = custProf.Mbe_email;

                                bool act = custProf.Mbe_act;

                                if (act == true)
                                {
                                    chkactive.Checked = true;
                                }
                                else
                                {
                                    chkactive.Checked = false;
                                }
                                LoadCountryName();
                            }


                        }
                        #endregion
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Agent is inactivated.Please contact accounts dept !!!')", true);
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                        if (_isExsit == false)
                        {
                            string BR = txtbeno.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            if (_isExsit == true)
                            {
                                LoadCustProf(cust_null);
                            }

                            txtbeno.Text = BR;
                            GroupBussinessEntity _grupProf = GetbyBrNoGrup(txtbeno.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                Session["_isExsit"] = "false";
                                Session["_isGroup"] = "true";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                                Session["_isGroup"] = "false";
                            }
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                        }

                    }

                    //if (custProf.Mbe_cd == null)                
                    //{
                    //    ClearDetails("brno");
                    //}

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

        public GroupBussinessEntity GetbyMobGrup(string mobNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobNo);
        }
        protected void txtmob_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtmob.Text))
                {

                    txtagcode.Enabled = false;
                    EnableTruelinkButton();
                    MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                    Boolean _isValid = IsValidMobileOrLandNo(txtmob.Text.Trim());

                    if (_isValid == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid mobile number !!!')", true);
                        txtmob.Text = "";
                        txtmob.Focus();
                        return;
                    }

                    /*
                 
                      Modified by Kelum : get agent profile : 2016-May-13 : Comments from previous version
                 
                     */


                    //string _current_cust = string.Empty;
                    //Int32 _cnt = 0;                
                    //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtmob.Text, "A");

                    //if (_fk != null && _fk.Count > 0)
                    //{
                    //    _cnt = _fk.Count;
                    //    _current_cust = _fk[0].Mbe_cd;
                    //}

                    //if (_current_cust != txtagcode.Text || _cnt > 1)
                    //{
                    _masterBusinessCompany = GetAgentbyMobNo(txtmob.Text.Trim().ToUpper());

                    if (_masterBusinessCompany.Mbe_cd != txtagcode.Text || _masterBusinessCompany != null)
                    {
                        //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtmob.Text, "A");

                        if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                        {
                            Session["_isUpdate"] = true;
                            #region Mobileno
                            _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                            if (_isUpdate == true)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Agent already exists for this mobile number !!!')", true);
                                Session["confmbox"] = "true";
                                Session["_isExsit"] = "true";
                                txtagcode.Text = _masterBusinessCompany.Mbe_cd;
                                txtname.Text = _masterBusinessCompany.Mbe_name;
                                txtadd1.Text = _masterBusinessCompany.Mbe_add1;
                                txtadd2.Text = _masterBusinessCompany.Mbe_add2;
                                txtrefno.Text = _masterBusinessCompany.Mbe_acc_cd;
                                txtcountry.Text = _masterBusinessCompany.Mbe_country_cd;
                                txtbeno.Text = _masterBusinessCompany.Mbe_br_no;
                                txttel.Text = _masterBusinessCompany.Mbe_tel;
                                txtfax.Text = _masterBusinessCompany.Mbe_fax;
                                txtemail.Text = _masterBusinessCompany.Mbe_email;

                                bool act = _masterBusinessCompany.Mbe_act;

                                if (act == true)
                                {
                                    chkactive.Checked = true;
                                }
                                else
                                {
                                    chkactive.Checked = false;
                                }
                                LoadCountryName();
                            }
                            #endregion
                        }
                        else if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == false)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Agent is inactivated.Please contact accounts dept !!!')", true);
                            EnableFalselinkButton();
                            LoadCustProf(_masterBusinessCompany);
                        }
                        else
                        {
                            _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                            if (_isExsit == true)
                            {
                                string Mob = txtmob.Text.Trim().ToUpper();
                                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                                //LoadCustProf(cust_null);
                                txtmob.Text = Mob;
                            }
                            GroupBussinessEntity _grupProf = GetbyMobGrup(txtmob.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                Session["_isExsit"] = "false";
                                Session["_isGroup"] = "true";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                                Session["_isGroup"] = "false";
                            }
                            //_isExsit = false;
                            //Session["_isExsit"] = "false";
                        }
                    }
                    //if (_masterBusinessCompany.Mbe_cd == null)
                    //{
                    //    ClearDetails("mobno");
                    //}
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

        private void LoadCountryName()
        {
            try
            {           
            if (!string.IsNullOrEmpty(txtcountry.Text))
            {
                DataTable dtcountry = CHNLSVC.MsgPortal.GetCountryDetails(txtcountry.Text.Trim().ToUpper());
                if (dtcountry.Rows.Count > 0)
                {
                    foreach (DataRow item in dtcountry.Rows)
                    {
                        lblbountry.Text = item["MCU_DESC"].ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid country code !!!')", true);
                    txtcountry.Text = "";
                    lblbountry.Text = "";
                    txtcountry.Focus();
                    return;
                }

            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter country code !!!')", true);
                txtcountry.Text = "";
                txtcountry.Focus();
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

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable result = CHNLSVC.General.SearchAgent(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "406";
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

        private void ClearDetails(string code)
        {           
            txtname.Text = "";
            txtadd1.Text = "";
            txtadd2.Text = "";
            txtrefno.Text = "";
            txtcountry.Text = "";            
            txttel.Text = "";
            txtfax.Text = "";
            txtemail.Text = "";

            if (code=="agentcode") 
            {                
                txtmob.Text = "";
                txtbeno.Text = "";
            }

            if (code == "mobno")
            {
                txtagcode.Text = "";
                txtbeno.Text = "";
            }

            if (code=="brno")
            {
                txtagcode.Text = "";                
                txtmob.Text = "";
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No such record found for this entry !!!')", true);
        
        }

        protected void txttel_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txttel.Text))
            {

                Boolean _isValid = IsValidMobileOrLandNo(txttel.Text.Trim());

                if (_isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid telephone number !!!')", true);
                    txttel.Text = "";
                    txttel.Focus();
                    return;
                }
            }
        }

        protected void txtfax_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtfax.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtfax.Text.Trim());

                if (_isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid fax number !!!')", true);
                    txtfax.Text = "";
                    txtfax.Focus();
                    return;
                }
            }
        }

        protected void txtcountry_TextChanged(object sender, EventArgs e) 
        {
            LoadCountryName();
            /*
            try
            {
            if (!string.IsNullOrEmpty(txtcountry.Text))
            {
                //string countrycode = txtcountry.Text.Trim().ToUpper();
                //string countrydescription = "";

                //DataTable result = CHNLSVC.Sales.GetCountryDetails(countrycode);
                //if (result.Rows.Count > 0)
                //{
                //    countrycode = (String)result.Rows[0][0];
                //    countrydescription = (String)result.Rows[0][1];

                //    txtcountry.Text = countrycode;
                //    lblbountry.Text = countrydescription;
                //}

                //else 
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid country code !!!')", true);
                //    txtcountry.Text = "";
                //    lblbountry.Text = "";
                //    txtcountry.Focus();
                //    return;
                //}
                
            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter country code !!!')", true);
                txtcountry.Text = "";                
                txtcountry.Focus();
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
             */
        }
    }
}