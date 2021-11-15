using FF.BusinessObjects;
using FF.BusinessObjects.General;
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
    public partial class SupplierCreation : BasePage
    {
        string emptyText { get { return (string)ViewState["emptyText"]; } set { ViewState["emptyText"] = value; } }
        private MasterBusinessEntity _supProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private Boolean _isExsit = false;
        private Boolean _isGroup = false;
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        //  private List<BusEntityItem> _busItem = new List<BusEntityItem>();
        private List<SupplierPort> _supPorts = new List<SupplierPort>();
        private List<BusEntityItem> _busItem { get { return (List<BusEntityItem>)Session["_busItem"]; } set { Session["_busItem"] = value; } }
        private DataTable selItem { get { return (DataTable)Session["selItem"]; } set { Session["selItem"] = value; } }
        List<MasterBusinessEntity> _mstBusEntity = new List<MasterBusinessEntity>();
        private MasterBusinessEntity _selectBusEnt
        {
            get
            {
                if (Session["_selectBusEnt"] != null)
                {
                    return (MasterBusinessEntity)Session["_selectBusEnt"];
                }
                else
                {
                    return new MasterBusinessEntity();
                }
            }
            set { Session["_selectBusEnt"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.GetPostBackEventReference(txtBrand);
            try
            {
                if (!IsPostBack)
                {
                    BindCombo();
                    dgvSelItms.DataSource = new int[] { };
                    dgvSelItms.DataBind();
                    dgvSupItms.DataSource = new int[] { };
                    dgvSupItms.DataBind();
                    dgvPort.DataSource = new int[] { };
                    dgvPort.DataBind();

                    _supPorts = new List<SupplierPort>();
                    selItem = new DataTable();
                    Session["_supPorts"] = _supPorts;
                    Session["addnewItem"] = "False";
                    txtCountry.Text = "LK";
                    txtCurrency.Text = "LKR";
                    txtPorTime.MaxLength = 6;
                    if (ddlCodeType.SelectedIndex > 1)
                    {
                        ddlCodeType.SelectedIndex = 1;
                    }
                    emptyText = "All data selected...";
                    _selectBusEnt = new MasterBusinessEntity();
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

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusCode = "";
                Int32 _effect = 0;
                SupplierWiseNBT _supplierNBT = new SupplierWiseNBT();
                if (hdfSave.Value == "No")
                {
                    return;
                }
                #region validation

                #region special character validation
                if (!validateinputString(txtCode.Text))
                {
                    DisplayMessage("Invalid charactor found in supplier code.");
                    txtCode.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtName.Text))
                {
                    DisplayMessage("Invalid charactor found in supplier name.");
                    txtName.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtAddress1.Text))
                {
                    DisplayMessage("Invalid charactor found in address 1.");
                    txtAddress1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtAddress2.Text))
                {
                    DisplayMessage("Invalid charactor found in address 2.");
                    txtAddress2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtContactPerson.Text))
                {
                    DisplayMessage("Invalid charactor found in contact person.");
                    txtContactPerson.Focus();
                    return;
                }
                if (!validateinputString(txtPhone.Text))
                {
                    DisplayMessage("Invalid charactor found in phone no.");
                    txtPhone.Focus();
                    return;
                }
                if (!validateinputString(txtFax.Text))
                {
                    DisplayMessage("Invalid charactor found in fax no.");
                    txtFax.Focus();
                    return;
                }
                if (!validateinputString(txtWebSite.Text))
                {
                    DisplayMessage("Invalid charactor found in web site name.");
                    txtWebSite.Focus();
                    return;
                }
                if (!validateinputString(txtAccCode.Text))
                {
                    DisplayMessage("Invalid charactor found in GL Acc code.");
                    txtAccCode.Focus();
                    return;
                }
                if (!validateinputString(txtTinNo.Text))
                {
                    DisplayMessage("Invalid charactor found in tin no.");
                    txtTinNo.Focus();
                    return;
                }
                if (!validateinputString(txtTaxReg.Text))
                {
                    DisplayMessage("Invalid charactor found in tin no.");
                    txtTaxReg.Focus();
                    return;
                }

                if (!validateinputString(txtNBTRate.Text))
                {
                    DisplayMessage("Invalid charactor found in NBT rate.");
                    txtNBTRate.Focus();
                    return;
                }
                if (!validateinputString(txtDivAmt.Text))
                {
                    DisplayMessage("Invalid charactor found in dividend amount.");
                    txtDivAmt.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtSupRem.Text))
                {
                    DisplayMessage("Invalid charactor found in supplier remark.");
                    txtSupRem.Focus();
                    return;
                }
                #endregion


                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    txtCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a code')", true);
                    return;
                }
                if (ddlCodeType.SelectedIndex == 0)
                {
                    ddlCodeType.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a code type')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtName.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter name')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    txtAddress1.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter address')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    txtAddress1.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter address')", true);
                    return;
                }

                if (!string.IsNullOrEmpty(txtPhone.Text))
                {
                    Boolean _isValid = IsValidMobileOrLandNo(txtPhone.Text.Trim());
                    if (_isValid == false)
                    {
                        txtPhone.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid phone #')", true);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtFax.Text))
                {
                    Boolean _isValid = IsValidMobileOrLandNo(txtFax.Text.Trim());
                    if (_isValid == false)
                    {
                        txtFax.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid fax #')", true);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    if (!IsValidEmail(txtEmail.Text))
                    {
                        txtEmail.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid email ')", true);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtCountry.Text))
                {
                    txtCountry.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a country')", true);
                    return;
                }
                if (!string.IsNullOrEmpty(txtTaxCat.Text))
                {
                    if (chkTaxSupplier.Checked == false)
                    {
                        txtTaxCat.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('None tax supplier cannot setup as tax claimble supplier')", true);
                        return;
                    }
                }


                if (!string.IsNullOrEmpty(txtCountry.Text))
                {
                    txtCountry_TextChanged(null, null);
                }
                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    txtCurrency.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a currency')", true);
                    return;
                }
                if (!string.IsNullOrEmpty(txtCurrency.Text))
                {
                    List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(txtCurrency.Text.ToUpper());
                    if (_Currency == null)
                    {
                        txtCurrency.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid currency code ')", true);
                        return;
                    }
                }
                if (ddlType.SelectedIndex == 0)
                {
                    ddlType.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a type')", true);
                    return;
                }
                if (!string.IsNullOrEmpty(txtTaxCat.Text))
                {
                    txtTaxCat_TextChanged(null, null);
                }


                if (chkTaxSupplier.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtTaxReg.Text))
                    {
                        txtTaxReg.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a TAX reg. number')", true);
                        return;
                    }
                }
                #endregion
                Collect_Supp();
                Collect_GroupCust();
                MasterBusinessEntity custProf = GetbyCustCD(txtCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit = true;
                }
                else
                {
                    _isExsit = false;
                }
                if (_busItem == null)
                {
                    _busItem = new List<BusEntityItem>();
                }
                if (chkAct.Checked)
                {
                    if (!string.IsNullOrEmpty(txtCode.Text) && !string.IsNullOrEmpty(txtNBTType.Text) && !string.IsNullOrEmpty(txtNBTRate.Text) && !string.IsNullOrEmpty(txtDivAmt.Text))
                    {
                        string _taxRate_cd = string.Empty;
                        if (Session["desc"] == null)
                        {
                            Session["desc"] = string.Empty;
                        }
                        else
                        {
                            _taxRate_cd = Session["desc"].ToString();
                        }
                        _supplierNBT.MBIT_COM = Session["UserCompanyCode"].ToString();
                        _supplierNBT.MBIT_CD = txtCode.Text;
                        _supplierNBT.MBIT_TAX_CD = string.IsNullOrEmpty(txtNBTType.Text) ? "" : txtNBTType.Text;
                        _supplierNBT.MBIT_TAX_RT_CD = _taxRate_cd;
                        _supplierNBT.MBIT_TAX_RT = Convert.ToDecimal(string.IsNullOrEmpty(txtNBTRate.Text) ? "0" : txtNBTRate.Text);
                        _supplierNBT.MBIT_EFFCT_DT = DateTime.Now;
                        _supplierNBT.MBIT_DIV_RT = Convert.ToDecimal(string.IsNullOrEmpty(txtDivAmt.Text) ? "0" : txtDivAmt.Text);
                        _supplierNBT.MBIT_CRE_BY = Session["UserID"].ToString();
                        _supplierNBT.MBIT_CRE_DT = DateTime.Now;
                        _supplierNBT.MBIT_ACT = true;
                        _supplierNBT.MBIT_TP = "S";
                        _supplierNBT.MBIT_MOD_BY = Session["UserID"].ToString();
                        _supplierNBT.MBIT_MOD_DT = DateTime.Now;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + "Please Select Correct NBT Details" + "');", true);
                        return;
                    }
                }
                else
                {
                    _supplierNBT = null;
                }
                if (_isExsit == false)
                {
                    if (Session["addnewItem"].ToString()=="False")
                    {
                        _busItem = null;
                    }
                    if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllItem.Checked && !chkAllBrand.Checked && !chkAllModel.Checked)
                    {
                        _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_supProfile, _account, _busInfoList, _busItem, out _cusCode, null, _isExsit, _isGroup, _custGroup, true, null, "false", null, null, _supplierNBT);
                    }
                    else
                    {
                        MasterItem item = new MasterItem();
                        item.Mi_purcom_cd = Session["UserCompanyCode"].ToString();
                        item.Mi_anal1 = "ITEM";
                        item.Mi_cd = txtItemCode.Text.Trim().ToUpper();
                        item.Mi_brand = txtBrand.Text.Trim().ToUpper();
                        item.Mi_model = txtModel.Text.Trim().ToUpper();
                        item.Mi_cate_1 = txtMainCat.Text.Trim().ToUpper();
                        item.Mi_cate_1 = txtMainCat.Text.Trim().ToUpper();
                        item.Mi_cre_by = Session["UserID"].ToString();
                        item.Mi_mod_by = Session["UserID"].ToString();
                        item.Mi_mod_dt = DateTime.Now;
                        item.Mi_mod_dt = DateTime.Now;

                        //CHNLSVC.Sales.GetInsuCriteria(Session["UserCompanyCode"].ToString() , "ITEM", txtItemCode.Text.Trim().ToUpper(), txtBrand.Text.ToUpper().Trim(), 
                        //    txtModel.Text.Trim().ToUpper(),
                        //                    txtMainCat.Text.ToUpper().Trim(), txtSubCat.Text.ToUpper().Trim(), null, null, null);
                        _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroupNew(_supProfile, _account,
                            _busInfoList, _busItem, out _cusCode, null, _isExsit, _isGroup, _custGroup, item, _supplierNBT);
                    }
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(txtCode.Text))
                    //    {
                    //        _effect = CHNLSVC.Sales.SupplierNBTDetails(_supplierNBT);
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + "Error Occurred while processing !!!" + "');", true);
                    //        return;
                    //    }
                    //}
                    //catch(Exception ex)
                    //{
                    //    string msg = ex.Message;
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + msg + "');", true);
                    //    Clear(); txtCode.Text = "";
                    //    return;
                    //}
                }
                else
                {
                    if (Session["addnewItem"].ToString() == "False")
                    {
                        _busItem = null;
                    }
                    if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllItem.Checked && !chkAllBrand.Checked && !chkAllModel.Checked)
                    {
                        _cusCode = txtCode.Text.ToUpper().Trim();
                        _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_supProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0,
                            _busInfoList, null, _busItem, _custGroup, null, _supplierNBT);
                    }
                    else
                    {
                        MasterItem item = new MasterItem();
                        item.Mi_purcom_cd = Session["UserCompanyCode"].ToString();
                        item.Mi_anal1 = "ITEM";
                        item.Mi_cd = txtItemCode.Text.Trim().ToUpper();
                        item.Mi_brand = txtBrand.Text.Trim().ToUpper();
                        item.Mi_model = txtModel.Text.Trim().ToUpper();
                        item.Mi_cate_1 = txtMainCat.Text.Trim().ToUpper();
                        item.Mi_cate_1 = txtMainCat.Text.Trim().ToUpper();
                        item.Mi_cre_by = Session["UserID"].ToString();
                        item.Mi_mod_by = Session["UserID"].ToString();
                        item.Mi_mod_dt = DateTime.Now;
                        item.Mi_mod_dt = DateTime.Now;
                        _cusCode = txtCode.Text.ToUpper().Trim();
                        _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroupNew(_supProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0,
                            _busInfoList, null, _busItem, _custGroup, item, _supplierNBT);
                    }
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(txtCode.Text))
                    //    {
                    //        _effect = CHNLSVC.Sales.SupplierNBTDetails(_supplierNBT);
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + "Error Occurred while processing !!!" + "');", true);
                    //        return;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    string msg = ex.Message;
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + msg + "');", true);
                    //    Clear(); txtCode.Text = "";
                    //    return;
                    //}
                }

                if (_effect > 0)
                {
                    _supPorts = (List<SupplierPort>)Session["_supPorts"];
                    int r = CHNLSVC.General.UpdateSupplierPort(_supPorts);
                    if (_isExsit == false)
                    {
                        string msg = "New supplier created. Supplier Code : " + _cusCode;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickySuccessToast('" + msg + "');", true);
                        Clear();
                        txtCode.Text = "";
                    }
                    else
                    {
                        string msg = "Exsiting supplier updated. ";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickySuccessToast('" + msg + "');", true);
                        Clear(); txtCode.Text = "";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_cusCode))
                    {
                        string msg = _cusCode + " Terminated ";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + msg + "');", true);
                        return;
                    }
                    else
                    {
                        string msg = "Creation Fail. ";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyErrorToast('" + msg + "');", true);
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

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = null;
                Session["SupplierCommon"] = null;
                Session["Country"] = null;
                Session["Currency"] = null;
                Session["masterTax"] = null;
                Session["TaxCodes"] = null;
                Session["CAT_Main"] = null;
                Session["CAT_Sub1"] = null;
                Session["Item"] = null;
                Session["ItemBrand"] = null;
                Session["Model"] = null;
                Session["Ports"] = null;
                Session["BLHeader"] = null;
                if (lblSearchType.Text == "SupplierCommon")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
                    _dt = CHNLSVC.CommonSearch.GetSupplierCommon(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    _dt.Columns.Remove("NIC");
                    _dt.Columns.Remove("Mobile");
                    _dt.Columns.Remove("BR No");
                    _dt.Columns.Remove("VAT Reg");
                    Session["SupplierCommon"] = _dt;
                }
                else if (lblSearchType.Text == "Country")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _dt = CHNLSVC.CommonSearch.GetCountrySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Country"] = _dt;
                }
                else if (lblSearchType.Text == "Currency")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    _dt = CHNLSVC.CommonSearch.GetCurrencyData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Currency"] = _dt;
                }
                else if (lblSearchType.Text == "masterTax")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                    _dt = CHNLSVC.CommonSearch.GetTaxClaimCode(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["masterTax"] = _dt;
                }
                else if (lblSearchType.Text == "TaxCodes")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
                    _dt = CHNLSVC.CommonSearch.Search_Tax_CODES(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["TaxCodes"] = _dt;
                }
                else if (lblSearchType.Text == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Main"] = _dt;
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    _dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub1"] = _dt;
                }
                else if (lblSearchType.Text == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _dt = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item"] = _dt;
                }
                else if (lblSearchType.Text == "ItemBrand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    _dt = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ItemBrand"] = _dt;
                }

                else if (lblSearchType.Text == "Model")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _dt = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Model"] = _dt;
                }
                else if (lblSearchType.Text == "Ports")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                    _dt = CHNLSVC.CommonSearch.SearchGetPort(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToUpper().Trim());
                    Session["Ports"] = _dt;
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    _dt = CHNLSVC.CommonSearch.SearchGetPort(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BLHeader"] = _dt;
                }
                else if (lblSearchType.Text == "NBTCode")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
                    _dt = CHNLSVC.CommonSearch.Search_Tax_CODES(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["NBTCode"] = _dt;
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

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "SupplierCommon")
                {
                    _result = (DataTable)Session["SupplierCommon"];
                }
                else if (lblSearchType.Text == "Country")
                {
                    _result = (DataTable)Session["Country"];
                }
                else if (lblSearchType.Text == "Currency")
                {
                    _result = (DataTable)Session["Currency"];
                }
                else if (lblSearchType.Text == "masterTax")
                {
                    _result = (DataTable)Session["masterTax"];
                }
                else if (lblSearchType.Text == "TaxCodes")
                {
                    _result = (DataTable)Session["TaxCodes"];
                }
                else if (lblSearchType.Text == "CAT_Main")
                {
                    _result = (DataTable)Session["CAT_Main"];
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                else if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                else if (lblSearchType.Text == "ItemBrand")
                {
                    _result = (DataTable)Session["ItemBrand"];
                }
                else if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                else if (lblSearchType.Text == "Ports")
                {
                    _result = (DataTable)Session["Ports"];
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    _result = (DataTable)Session["BLHeader"];
                }
                else if (lblSearchType.Text == "NBTCode")
                {
                    _result = (DataTable)Session["NBTCode"];
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
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string rate = string.Empty;
                if (string.IsNullOrEmpty(txtCode.Text) && lblSearchType.Text != "SupplierCommon")
                {
                    txtCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a supplier code frist !')", true);
                    return;
                }
                string code = dgvResult.SelectedRow.Cells[1].Text;
                string desc = dgvResult.SelectedRow.Cells[2].Text;
                if (lblSearchType.Text == "NBTCode")
                {
                    rate = dgvResult.SelectedRow.Cells[3].Text;
                    Session["desc"] = desc;
                }

                if (lblSearchType.Text == "SupplierCommon")
                {
                    txtCode.Text = code;
                    txtCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Country")
                {
                    txtCountry.Text = code;
                    txtCountry_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Currency")
                {
                    txtCurrency.Text = code;
                    txtCurrency_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "masterTax")
                {
                    txtTaxCat.Text = code;
                    txtTaxCat_TextChanged(null, null);
                }
                //else if (lblSearchType.Text == "TaxCodes")
                //{
                //    txtRateCode.Text = code;
                //    txtRateCode_TextChanged(null, null);
                //}
                else if (lblSearchType.Text == "CAT_Main")
                {
                    txtMainCat.Text = code;
                    txtMainCat_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    txtSubCat.Text = code;
                    txtSubCat_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Item")
                {
                    txtItemCode.Text = code;
                    txtItemCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "ItemBrand")
                {
                    txtBrand.Text = code;
                    txtBrand_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Model")
                {
                    txtModel.Text = code;
                    txtModel_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "ItemBrand")
                {
                    txtBrand.Text = code;
                    txtBrand_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Ports")
                {
                    txtPortFromCode.Text = code;
                    txtPortFromCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    txtPortToCode.Text = code;
                    txtPortToCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "NBTCode")
                {
                    txtNBTType.Text = code;
                    txtNBTRate.Text = rate;
                    txtNBTType_TextChanged(null, null);
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtMainCat.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtMainCat.Text.ToUpper() + seperator + txtSubCat.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterTax:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxCodes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        //paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        //break;
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Ports:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
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
        protected void lbtnSeCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "SupplierCommon";
                Session["SupplierCommon"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommon(para, null, null);
                string s = "";
                _result.Columns.Remove("NIC");
                _result.Columns.Remove("Mobile");
                _result.Columns.Remove("BR No");
                _result.Columns.Remove("VAT Reg");
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["SupplierCommon"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Country";
                Session["Country"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Country"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Currency";
                Session["Currency"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Currency"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                Session["CAT_Main"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["CAT_Main"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeSubCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMainCat.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a main category !')", true);
                    return;
                }

                lblSearchType.Text = "CAT_Sub1";
                Session["CAT_Sub1"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["CAT_Sub1"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ItemBrand";
                Session["ItemBrand"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["ItemBrand"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeModel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                Session["Model"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Model"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnSeTaxCat_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "masterTax";
                Session["masterTax"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetTaxClaimCode(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["masterTax"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnSePortFr_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Ports";
                Session["Ports"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["Ports"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSePortTo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BLHeader";
                Session["BLHeader"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["BLHeader"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnAddSellItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainCat.Text) && string.IsNullOrEmpty(txtSubCat.Text) && string.IsNullOrEmpty(txtItemCode.Text)
                && string.IsNullOrEmpty(txtBrand.Text) && string.IsNullOrEmpty(txtModel.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the criteria')", true);
                return;
            }
            Session["addnewItem"] = "True";
            selItem = CHNLSVC.Sales.GetInsuCriteria(Session["UserCompanyCode"].ToString(), "ITEM", txtItemCode.Text.Trim().ToUpper(), txtBrand.Text.ToUpper().Trim(), txtModel.Text.Trim().ToUpper(),
                txtMainCat.Text.ToUpper().Trim(), txtSubCat.Text.ToUpper().Trim(), null, null, null);

            dgvSelItms.DataSource = new int[] { };
            dgvSelItms.DataBind();
            if (selItem.Rows.Count > 0 && selItem.Rows.Count < 500)
            {
                dgvSelItms.DataSource = selItem;
                dgvSelItms.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No data found(or big data load) for the selected criteria')", true);
                return;
            }
        }

        protected void lbtnAddSupItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                _busItem = new List<BusEntityItem>();
                //Session["_busItem"] = _busItem;
                txtCode.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a supplier code ')", true);
                return;
            }
            dgvSupItms.DataSource = new int[] { };
            dgvSupItms.DataBind();

            //CheckBox chkboxSelectAll = ((CheckBox)dgvSelItms.HeaderRow.FindControl("chkboxSelectAll"));
            bool b = false;
            if (dgvSelItms.Rows.Count > 0)
            {
                foreach (GridViewRow row in dgvSelItms.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        b = true; break;
                    }
                }
            }

            if (!b)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a item code')", true);
                return;
            }
            //if (chkboxSelectAll.Checked)
            //{
            //    #region Cheack
            //    foreach (DataRow row in selItem.Rows)
            //    {
            //        BusEntityItem _tmp = new BusEntityItem();
            //        _tmp.MBII_ACT = 1;
            //        _tmp.MBII_CD = txtCode.Text.ToUpper();
            //        _tmp.MBII_COM = Session["UserCompanyCode"].ToString();
            //        _tmp.MBII_CRE_BY = Session["UserID"].ToString();
            //        _tmp.MBII_ITM_CD = row["CODE"].ToString();
            //        _tmp.MBII_TP = "S";
            //        _tmp.MI_SHORTDESC = row["DESCRIPT"].ToString();
            //        _tmp.MBII_MOD_BY = Session["UserID"].ToString();
            //        if (_busItem != null)
            //        {
            //            if (_busItem.Count > 0)
            //            {
            //                var item = _busItem.Where(c => c.MBII_ITM_CD == _tmp.MBII_ITM_CD).FirstOrDefault();
            //                if (item != null)
            //                {
            //                    var rem = _busItem.Where(c => c.MBII_ITM_CD == item.MBII_ITM_CD && c.MBII_ACT == 0).SingleOrDefault();
            //                    if (rem != null)
            //                    {
            //                        _busItem.Remove(rem);
            //                        _busItem.Add(_tmp);
            //                    }
            //                    else
            //                    {
            //                        _busItem.Add(_tmp);
            //                    }
            //                }
            //                else
            //                {
            //                    _busItem.Add(_tmp);
            //                }
            //            }
            //            else
            //            {
            //                _busItem.Add(_tmp);
            //            }
            //        }
            //        else
            //        {
            //            if (_busItem == null)
            //            {
            //                _busItem = new List<BusEntityItem>();
            //            }
            //            _busItem.Add(_tmp);
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{
            //    #region NotCheack
            if (dgvSelItms.Rows.Count > 0)
            {
                foreach (GridViewRow row in dgvSelItms.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        BusEntityItem _tmp = new BusEntityItem();
                        _tmp.MBII_ACT = 1;
                        _tmp.MBII_CD = txtCode.Text.ToUpper();
                        _tmp.MBII_COM = Session["UserCompanyCode"].ToString();
                        _tmp.MBII_CRE_BY = Session["UserID"].ToString();
                        Label lblItemCode = (Label)row.FindControl("lblItemCode");
                        _tmp.MBII_ITM_CD = lblItemCode.Text;
                        _tmp.MBII_TP = "S";
                        Label lblDescription = (Label)row.FindControl("lblDescription");
                        _tmp.MI_SHORTDESC = lblDescription.Text;
                        _tmp.MBII_MOD_BY = Session["UserID"].ToString();
                        //dilshan------
                        _tmp.MBII_WARR_PERI = Convert.ToInt32(ddlSupPrd.SelectedValue.ToString());
                        _tmp.MBII_WARR_RMK = txtSupRem.Text;
                        _tmp.MBII_CRE_DT = DateTime.Now;
                        _tmp.MBII_MOD_DT = DateTime.Now;
                        //---------------------
                        if (_busItem != null)
                        {
                            if (_busItem.Count > 0)
                            {
                                var item = _busItem.Where(c => c.MBII_ITM_CD == _tmp.MBII_ITM_CD).FirstOrDefault();
                                if (item != null)
                                {
                                    //var rem = _busItem.Where(c => c.MBII_ITM_CD == item.MBII_ITM_CD && c.MBII_ACT == 0).SingleOrDefault();
                                    var rem = _busItem.Where(c => c.MBII_ITM_CD == item.MBII_ITM_CD).SingleOrDefault();
                                    if (rem != null)
                                    {
                                        _busItem.Remove(rem);
                                        _busItem.Add(_tmp);
                                    }
                                    else
                                    {
                                        _busItem.Add(_tmp);
                                    }
                                }
                                else
                                {
                                    _busItem.Add(_tmp);
                                }
                            }
                            else
                            {
                                _busItem.Add(_tmp);
                            }
                        }
                        else
                        {
                            _busItem = new List<BusEntityItem>();
                            _busItem.Add(_tmp);
                        }
                    }
                }

            }
            //    } 
            //    #endregion
            //}
            dgvSupItms.DataSource = new int[] { };
            if (_busItem == null)
            {
                _busItem = new List<BusEntityItem>();
            }
            if (_busItem.Count > 0)
            {
                var v = _busItem.Where(c => c.MBII_ACT == 1).ToList();
                dgvSupItms.DataSource = v;

            }
            dgvSupItms.DataBind();
        }
        //protected void chkSpecialTax_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtRate.Text = "";
        //    txtRate.ToolTip = "";
        //    txtRateCode.Text = "";
        //    txtDivRate.Text = "";
        //    if (chkSpecialTax.Checked)
        //    {
        //        panelSpecialTax.Enabled = true;
        //    }
        //    else
        //    {
        //        panelSpecialTax.Enabled = false;
        //    }
        //}

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtCode.Text))
            {
                DisplayMessage("Invalid charactor found in supplier code.");
                txtCode.Focus();
                return;
            }
            try
            {
                Clear();
                if (!string.IsNullOrEmpty(txtCode.Text))
                {
                    MasterBusinessEntity custProf = GetbyCustCD(txtCode.Text.ToUpper().Trim());

                    SupplierWiseNBT _suppNBT = CHNLSVC.Sales.GetSupplierNBT(Session["UserCompanyCode"].ToString(), txtCode.Text.Trim());
                    if (_suppNBT != null)
                    {
                        chkAct.Checked = true;
                        txtNBTType.Text = _suppNBT.MBIT_TAX_CD;
                        txtNBTRate.Text = _suppNBT.MBIT_TAX_RT.ToString();
                        txtDivAmt.Text = _suppNBT.MBIT_DIV_RT.ToString();
                        AutoRefinementCheck.Checked = custProf.Mbe_allow_refini;
                    }
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit = true;

                        LoadCustProf(custProf);
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Supplier is inactivated.')", true);
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        if (_isExsit == true)
                        {
                            string cusCD = txtCode.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtCode.Text = cusCD;
                        }
                        else
                        {
                            _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileNew(
                                new MasterBusinessEntity() { Mbe_cd = txtCode.Text.ToUpper().Trim(), Mbe_tp = "S" });
                            var v = _mstBusEntity.Where(c => c.Mbe_com == Session["UserCompanyCode"].ToString() && c.Mbe_cd == txtCode.Text.ToUpper().Trim()).FirstOrDefault();
                            if (v != null)
                            {
                                _mstBusEntity.Remove(v);
                            }
                            if (_mstBusEntity.Count > 0)
                            {
                                string _msg = "";
                                foreach (var item in _mstBusEntity)
                                {
                                    if (string.IsNullOrEmpty(_msg))
                                    {
                                        _msg = item.Mbe_com;
                                    }
                                    else
                                    {
                                        _msg = _msg + " / " + item.Mbe_com;
                                    }
                                }
                                _selectBusEnt = _mstBusEntity[0];
                                lblMsg1.Text = "Supplier has been allocated for " + _msg + " company. Do you want to add for current company?";
                                lblMsg2.Text = "";
                                popupSupplier.Show();
                                // return; 
                            }
                            txtCountry.Text = "LK";
                            txtCurrency.Text = "LKR";
                        }
                        //Check the group level
                        _isExsit = false;

                    }
                    txtName.Focus();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtCountry.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtCountry.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCountry.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCountry.ToolTip = toolTip;
                        txtCurrency.Focus();
                    }
                    else
                    {
                        txtCountry.ToolTip = "";
                        txtCountry.Text = "";
                        txtCountry.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid country')", true);
                        return;
                    }
                }
                else
                {
                    txtCountry.ToolTip = "";
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

        protected void txtCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtCurrency.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtCurrency.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    if (!string.IsNullOrEmpty(txtCurrency.Text))
                    {
                        List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(txtCurrency.Text.ToUpper());
                        if (_Currency != null)
                        {
                            b2 = true;
                            toolTip = _Currency[0].Mcr_desc;
                        }
                    }
                    txtCurrency.ToolTip = toolTip;
                    if (!b2)
                    {
                        txtCurrency.ToolTip = "";
                        txtCurrency.Text = "";
                        txtCurrency.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid dealing currency !!!')", true);
                        return;
                    }
                    else
                    {
                        txtCreditPeriod.Focus();
                    }
                }
                else
                {
                    txtCurrency.ToolTip = "";
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

        //protected void txtRateCode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtRateCode.Text != "")
        //        {
        //            bool b2 = false;
        //            string toolTip = "";
        //            decimal rate = 0;
        //            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
        //            DataTable _result = CHNLSVC.CommonSearch.Search_Tax_CODES(para, null, null);
        //            foreach (DataRow row in _result.Rows)
        //            {
        //                if (!string.IsNullOrEmpty(row["Code"].ToString()))
        //                {
        //                    if (txtRateCode.Text == row["Code"].ToString())
        //                    {
        //                        b2 = true;
        //                        toolTip = row["Rate Code"].ToString();
        //                        rate =Convert.ToDecimal(row["Rate"].ToString());
        //                        break;
        //                    }
        //                }
        //            }
        //            if (b2)
        //            {
        //                txtRateCode.ToolTip = toolTip;
        //                txtRate.Text = rate.ToString("N");
        //            }
        //            else
        //            {
        //                txtRateCode.ToolTip = "";
        //                txtRateCode.Text = "";
        //                txtRateCode.Focus();
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid tax code !!!')", true);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            txtRateCode.ToolTip = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseChannel();
        //    }
        //}

        protected void txtTaxCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtTaxCat.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtTaxCat.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                    DataTable _result = CHNLSVC.CommonSearch.GetTaxClaimCode(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtTaxCat.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtTaxCat.ToolTip = toolTip;
                    }
                    else
                    {
                        txtTaxCat.ToolTip = "";
                        txtTaxCat.Text = "";
                        txtTaxCat.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid tax category !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtTaxCat.ToolTip = "";
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

        protected void txtMainCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chkAllMainCat.Checked = false;
                chkAllMainCat_CheckedChanged(null, null);
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtMainCat.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtMainCat.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtMainCat.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtMainCat.ToolTip = toolTip;
                        chkAllMainCat_CheckedChanged(null, null);
                    }
                    else
                    {
                        txtMainCat.ToolTip = "";
                        txtMainCat.Text = "";
                        txtMainCat.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid main category !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtMainCat.ToolTip = "";
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

        protected void txtSubCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chkAllSub.Checked = false;
                chkAllSub_CheckedChanged(null, null);
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtSubCat.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtSubCat.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtSubCat.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtSubCat.ToolTip = toolTip;
                    }
                    else
                    {
                        txtSubCat.ToolTip = "";
                        txtSubCat.Text = "";
                        txtSubCat.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid sub category !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtSubCat.ToolTip = "";
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

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chkAllItem.Checked = false;
                chkAllItem_CheckedChanged(null, null);
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtItemCode.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtItemCode.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "Item", "%" + txtItemCode.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Item"].ToString()))
                        {
                            if (txtItemCode.Text.ToUpper() == row["Item"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtItemCode.ToolTip = toolTip;
                    }
                    else
                    {
                        txtItemCode.ToolTip = "";
                        txtItemCode.Text = "";
                        txtItemCode.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Item code !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtItemCode.ToolTip = "";
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

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chkAllBrand.Checked = false;
                chkAllBrand_CheckedChanged(null, null);
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtBrand.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtBrand.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, "CODE", txtBrand.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtBrand.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtBrand.ToolTip = toolTip;
                    }
                    else
                    {
                        txtBrand.ToolTip = "";
                        txtBrand.Text = "";
                        txtBrand.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid brand !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtBrand.ToolTip = "";
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

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chkAllModel.Checked = false;
                chkAllModel_CheckedChanged(null, null);
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtModel.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtModel.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtModel.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtModel.ToolTip = toolTip;
                    }
                    else
                    {
                        txtModel.ToolTip = "";
                        txtModel.Text = "";
                        txtModel.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid model !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtModel.ToolTip = "";
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
        protected void lbtnDelSupItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    txtCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a supplier code ')", true);
                    return;
                }
                if (hdfDelete.Value == "Yes")
                {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    // _busItem = (List<BusEntityItem>)Session["_busItem"];
                    if (_busItem.Count > 0)
                    {
                        string itemCode = "";
                        if (row != null)
                        {
                            itemCode = (row.FindControl("lblSupItem") as Label).Text;
                        }
                        bool isSupplier = false;
                        MasterBusinessEntity custProf = GetbyCustCD(txtCode.Text.Trim().ToUpper());
                        if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                        {
                            isSupplier = true;
                        }
                        if (isSupplier)
                        {
                            for (int i = 0; i < _busItem.Count; i++)
                            {
                                if (_busItem[i].MBII_ITM_CD == itemCode)
                                {
                                    _busItem[i].MBII_ACT = 0;
                                }
                            }
                        }
                        else
                        {
                            var del = _busItem.Where(c => c.MBII_ITM_CD == itemCode).FirstOrDefault();
                            _busItem.Remove(del);
                        }
                        dgvSupItms.DataSource = new int[] { };
                        var v = _busItem.Where(c => c.MBII_ACT == 1).ToList();
                        if (v != null)
                        {
                            if (v.Count > 1)
                            {
                                Session["addnewItem"] = "True";
                                dgvSupItms.DataSource = v;
                            }
                        }
                        dgvSupItms.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V,X]{1}$";

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidMobileOrLandNo(string mobile)
        {
            //string pattern = @"^(\+[0-9])";

            //System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            //int len=mobile.Length;
            //if (match.Success)
            //    return true;
            //else
            //    return false;
            return true;
        }
        private void Collect_Supp()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;

            if (chkTaxSupplier.Checked == true)
            {
                _isVAT = true;
            }
            else
            {
                _isVAT = false;
            }

            _supProfile = new MasterBusinessEntity();
            _supProfile.Mbe_acc_cd = txtAccCode.Text;
            _supProfile.Mbe_act = Convert.ToBoolean(chkActive.Checked);
            _supProfile.Mbe_add1 = txtAddress1.Text.Trim();
            _supProfile.Mbe_add2 = txtAddress2.Text.Trim();

            _supProfile.Mbe_agre_send_sms = _isSMS;
            _supProfile.Mbe_br_no = "";
            _supProfile.Mbe_cate = txtTaxCat.Text.ToUpper();
            if (_isExsit == false && _isGroup == false)
            {
                _supProfile.Mbe_cd = txtCode.Text.Trim();
            }
            else
            {
                _supProfile.Mbe_cd = txtCode.Text.Trim();
            }
            _supProfile.Mbe_com = Session["UserCompanyCode"].ToString();
            _supProfile.Mbe_contact = txtContactPerson.Text.ToUpper();
            _supProfile.Mbe_country_cd = txtCountry.Text.ToUpper().Trim();
            _supProfile.Mbe_cr_add1 = txtAddress1.Text.Trim();
            _supProfile.Mbe_cr_add2 = txtAddress2.Text.Trim();
            _supProfile.Mbe_cr_country_cd = txtCountry.Text.ToUpper().Trim();
            _supProfile.Mbe_cr_distric_cd = "";
            _supProfile.Mbe_cr_email = null;
            _supProfile.Mbe_cr_fax = null;
            _supProfile.Mbe_cr_postal_cd = "";
            _supProfile.Mbe_cr_province_cd = "";
            _supProfile.Mbe_cr_tel = txtPhone.Text.Trim();
            _supProfile.Mbe_cr_town_cd = "";
            _supProfile.Mbe_cre_by = Session["UserID"].ToString();
            _supProfile.Mbe_cre_dt = DateTime.Now;
            _supProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
            _supProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
            _supProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
            _supProfile.Mbe_distric_cd = "";
            _supProfile.Mbe_dl_no = "";
            _supProfile.Mbe_dob = DateTime.Now.Date;
            _supProfile.Mbe_email = txtEmail.Text.Trim();
            _supProfile.Mbe_fax = txtFax.Text;
            _supProfile.Mbe_ho_stus = "GOOD";
            _supProfile.Mbe_income_grup = null;
            _supProfile.Mbe_intr_com = false;
            _supProfile.Mbe_is_suspend = false;

            _supProfile.Mbe_is_tax = _isVAT;
            _supProfile.Mbe_mob = "";
            _supProfile.Mbe_name = txtName.Text.Trim();
            _supProfile.Mbe_nic = "";
            _supProfile.Mbe_oth_id_no = txtTinNo.Text;
            _supProfile.Mbe_oth_id_tp = "TIN";
            _supProfile.Mbe_pc_stus = "GOOD";
            _supProfile.Mbe_postal_cd = "";
            _supProfile.Mbe_pp_no = "";
            _supProfile.Mbe_province_cd = "";
            _supProfile.Mbe_sex = "";
            _supProfile.Mbe_sub_tp = ddlType.SelectedValue;
            _supProfile.Mbe_svat_no = "";


            _supProfile.Mbe_tax_ex = _TaxEx;
            _supProfile.Mbe_tax_no = txtTaxReg.Text.Trim();
            _supProfile.Mbe_tel = txtPhone.Text.Trim();
            _supProfile.Mbe_town_cd = "";
            _supProfile.Mbe_tp = ddlCodeType.SelectedValue;
            _supProfile.Mbe_wr_add1 = "";
            _supProfile.Mbe_wr_add2 = "";
            _supProfile.Mbe_wr_com_name = "";
            _supProfile.Mbe_wr_country_cd = null;
            _supProfile.Mbe_wr_dept = "";
            _supProfile.Mbe_wr_designation = "";
            _supProfile.Mbe_wr_distric_cd = null;
            _supProfile.Mbe_wr_email = "";
            _supProfile.Mbe_wr_fax = "";
            _supProfile.Mbe_wr_proffesion = null;
            _supProfile.Mbe_wr_province_cd = null;
            _supProfile.Mbe_wr_tel = "";
            _supProfile.Mbe_wr_town_cd = null;
            _supProfile.MBE_FNAME = txtName.Text.Trim();
            _supProfile.MBE_SNAME = "";
            _supProfile.MBE_INI = "";
            _supProfile.MBE_TIT = "";
            _supProfile.MBE_WEB = txtWebSite.Text;
            _supProfile.Mbe_acc_cd = txtAccCode.Text;
            _supProfile.Mbe_cur_cd = txtCurrency.Text.ToUpper();
            _supProfile.MBE_CR_PERIOD = txtCreditPeriod.Text == "" ? 0 : Convert.ToInt32(txtCreditPeriod.Text);
            _supProfile.Mbe_fax = txtFax.Text;

            // Nadeeka 15-12-2014
            _supProfile.Mbe_agre_send_email = _isEmail;
            // _supProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();

            //Lakshan 31/12/2015
            _supProfile.Mbe_mod_by = Session["UserID"].ToString();
            _supProfile.Mbe_mod_dt = DateTime.Now;
            _supProfile.Mbe_mod_session = Session["SessionID"].ToString();
            _supProfile.Mbe_cre_session = Session["SessionID"].ToString();

            //Dulaj 2018-Mar-03
            _supProfile.Mbe_allow_refini = AutoRefinementCheck.Checked;
        }
        private void Collect_GroupCust()
        {
            _custGroup = new GroupBussinessEntity();
            _custGroup.Mbg_cd = txtCode.Text.Trim();
            _custGroup.Mbg_name = txtName.Text.Trim();
            //_custGroup.Mbg_tit = cmbTitle.Text;
            //_custGroup.Mbg_ini = txtInit.Text.Trim();
            _custGroup.Mbg_fname = txtName.Text.Trim();
            //_custGroup.Mbg_sname = txtSName.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtAddress1.Text.Trim();
            _custGroup.Mbg_add2 = txtAddress2.Text.Trim();
            //_custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            //_custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            //_custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            _custGroup.Mbg_country_cd = txtCountry.Text.ToUpper().Trim();
            _custGroup.Mbg_tel = txtPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            // _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
            // _custGroup.Mbg_mob = txtMob.Text.Trim();
            //_custGroup.Mbg_nic = txtNIC.Text.Trim();
            //_custGroup.Mbg_pp_no = txtPP.Text.Trim();
            //_custGroup.Mbg_dl_no = txtDL.Text.Trim();
            //_custGroup.Mbg_br_no = txtBR.Text.Trim();
            _custGroup.Mbg_email = txtEmail.Text.Trim();
            _custGroup.Mbg_contact = "";
            _custGroup.Mbg_act = true;
            _custGroup.Mbg_is_suspend = false;
            // _custGroup.Mbg_sex = cmbSex.Text;
            _custGroup.Mbg_dob = DateTime.Now.Date;
            _custGroup.Mbg_cre_by = Session["UserID"].ToString();
            _custGroup.Mbg_mod_by = Session["UserID"].ToString();
        }
        #region LoadCustProfile
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            MasterBusinessEntity customer = CHNLSVC.Sales.GetCustomerProfileNew(new MasterBusinessEntity() { Mbe_com = Session["UserCompanyCode"].ToString(), Mbe_cd = custCD, Mbe_tp = "S" }).FirstOrDefault();
            if (customer != null)
            {
                if (customer.Mbe_com == Session["UserCompanyCode"].ToString())
                {
                    return customer;
                }
                else
                {
                    return new MasterBusinessEntity();
                }
            }
            else
            {
                return new MasterBusinessEntity();

            }
        }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppno, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, Session["UserCompanyCode"].ToString());
        }
        #endregion LoadCustProfile

        public void LoadCustProf(MasterBusinessEntity cust)
        {

            ddlCodeType.SelectedIndex = ddlCodeType.Items.IndexOf(ddlCodeType.Items.FindByValue(cust.Mbe_tp));
            txtName.Text = cust.Mbe_name;
            txtContactPerson.Text = cust.Mbe_contact;
            txtAddress1.Text = cust.Mbe_add1;
            txtAddress2.Text = cust.Mbe_add2;
            txtCountry.Text = cust.Mbe_country_cd;
            txtPhone.Text = cust.Mbe_tel == "N/A" ? "" : cust.Mbe_tel;
            txtEmail.Text = cust.Mbe_email == "n/a" ? "" : cust.Mbe_email;
            chkTaxSupplier.Checked = cust.Mbe_is_tax;
            txtTaxReg.Text = cust.Mbe_tax_no;
            txtCurrency.Text = cust.Mbe_cur_cd;
            txtAccCode.Text = cust.Mbe_acc_cd;
            txtWebSite.Text = cust.MBE_WEB;
            txtCreditPeriod.Text = cust.MBE_CR_PERIOD.ToString();
            txtTinNo.Text = cust.Mbe_oth_id_no;
            ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(cust.Mbe_sub_tp));
            txtFax.Text = cust.Mbe_fax == "N/A" ? "" : cust.Mbe_fax;
            txtCurrency.Text = cust.Mbe_cur_cd;
            txtCountry.Text = cust.Mbe_country_cd;
            txtTaxCat.Text = cust.Mbe_cate;
            chkActive.Checked = cust.Mbe_act == true ? true : false;
            AutoRefinementCheck.Checked = cust.Mbe_allow_refini==true?true:false;
            _busItem = new List<BusEntityItem>();
            _busItem = CHNLSVC.General.GetBuninessEntityItemBySupplier(Session["UserCompanyCode"].ToString(), txtCode.Text);
            dgvSupItms.DataSource = new int[] { };
            if (_busItem != null)
            {
                for (int i = 0; i < _busItem.Count; i++)
                {
                    _busItem[i].MI_SHORTDESC = _busItem[i].MBII_CUSTNAME;
                }
                //Session["_busItem"] = _busItem;
                if (_busItem.Count > 0)
                {
                    dgvSupItms.DataSource = _busItem;
                }
            }
            dgvSupItms.DataBind();

            SupplierPort supObject = new SupplierPort()
            {
                MSPR_COM = Session["UserCompanyCode"].ToString(),
                MSPR_CD = cust.Mbe_cd,
                MSPR_TP = cust.Mbe_tp,
                MSPR_ACT = 1
            };
            _supPorts = CHNLSVC.General.GetSupplierPorts(supObject);
            Session["_supPorts"] = new List<SupplierPort>();
            if (_supPorts != null)
            {
                Session["_supPorts"] = _supPorts;
            }

            dgvPort.DataSource = new int[] { };
            if (_supPorts != null)
            {

                if (_supPorts.Count > 0)
                {
                    var data = _supPorts.Where(c => c.MSPR_ACT == 1).ToList();
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add(new DataColumn("FromPortCd", typeof(string)));
                    dt.Columns.Add(new DataColumn("FromPortDesc", typeof(string)));
                    dt.Columns.Add(new DataColumn("ToPortCd", typeof(string)));
                    dt.Columns.Add(new DataColumn("ToPortDesc", typeof(string)));
                    dt.Columns.Add(new DataColumn("CmbTime", typeof(Int32)));

                    foreach (var port in data)
                    {
                        dr = dt.NewRow();
                        dr["FromPortCd"] = port.MSPR_FRM_PORT;
                        DataTable dtFrom = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_FRM_PORT);
                        string frDesc = "";
                        if (dtFrom.Rows.Count > 0)
                        {
                            frDesc = dtFrom.Rows[0][1].ToString();
                        }
                        dr["FromPortDesc"] = frDesc;
                        dr["ToPortCd"] = port.MSPR_TO_PORT;
                        DataTable dtTo = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_TO_PORT);
                        string toDesc = "";
                        if (dtTo.Rows.Count > 0)
                        {
                            toDesc = dtTo.Rows[0][1].ToString();
                        }
                        dr["ToPortDesc"] = toDesc;
                        dr["CmbTime"] = port.MSPR_LEAD_TIME;
                        dt.Rows.Add(dr);
                    }
                    dgvPort.DataSource = dt;
                }
            }
            dgvPort.DataBind();
        }

        private void Clear()
        {
            _selectBusEnt = new MasterBusinessEntity();
            //   txtCode.Text = "";
            ddlCodeType.SelectedIndex = 0;
            chkTaxSupplier.Checked = false;
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtContactPerson.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtWebSite.Text = "";
            txtCountry.Text = "";
            txtCurrency.Text = "";
            txtCreditPeriod.Text = "";
            txtAccCode.Text = "";
            ddlType.SelectedIndex = 0;
            txtTaxCat.Text = "";
            txtTinNo.Text = "";
            txtTaxReg.Text = "";
            chkActive.Checked = true;
            txtMainCat.Text = "";
            txtSubCat.Text = "";
            txtItemCode.Text = "";
            txtBrand.Text = "";
            txtModel.Text = "";
            dgvSelItms.DataSource = new int[] { };
            dgvSelItms.DataBind();
            dgvSupItms.DataSource = new int[] { };
            dgvSupItms.DataBind();
            dgvPort.DataSource = new int[] { };
            dgvPort.DataBind();
            _busItem = new List<BusEntityItem>();
            // Session["_busItem"] = _busItem;
            _supPorts = new List<SupplierPort>();
            Session["_supPorts"] = _supPorts;
            Session["SupplierCommon"] = null;
            Session["Country"] = null;
            Session["Currency"] = null;
            Session["masterTax"] = null;
            Session["TaxCodes"] = null;
            Session["CAT_Main"] = null;
            Session["CAT_Sub1"] = null;
            Session["Item"] = null;
            Session["ItemBrand"] = null;
            Session["Model"] = null;
            txtNBTType.Text = string.Empty;
            txtNBTRate.Text = string.Empty;
            txtDivAmt.Text = string.Empty;
            chkAct.Checked = false;
            ddlSupPrd.SelectedIndex = -1;
            txtSupRem.Text = "";
        }

        protected void dgvSupItms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //dgvSupItms.DeleteRow(dgvSupItms.SelectedIndex);
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtCode.Text))
            //{
            //    txtEmail.Text = "";
            //    txtCode.Focus();
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
            //    return;
            //}
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    txtEmail.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid email address !!!')", true);
                    return;
                }
            }
            else
            {
                txtWebSite.Focus();
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
                //CheckBox chkTempSelect = (CheckBox)Row.FindControl("chkSelect");
                //bool select = chkTempSelect.Checked;
                //bool allSelected = false;
                //if (select)
                //{
                //    allSelected = true;
                //    foreach (GridViewRow hiderowbtn in this.dgvSelItms.Rows)
                //    {
                //        CheckBox chkSelect = (CheckBox)hiderowbtn.FindControl("chkSelect");
                //        if (chkSelect.Checked == false)
                //        {
                //            allSelected = false;
                //        }
                //    }
                //}
                //else
                //{
                //    allSelected = false;
                //}
                ////CheckBox chk = (CheckBox)dgvSelItms.HeaderRow.FindControl("chkboxSelectAll");
                ////chk.Checked = allSelected;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }

        }

        protected void txtCreditPeriod_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtCode.Text))
            //{
            //    txtCode.Focus();
            //    txtCreditPeriod.Text = "";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
            //    return;
            //} 
            if (Convert.ToDecimal(txtCreditPeriod.Text) < 0)
            {
                txtCreditPeriod.Focus();
                txtCreditPeriod.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid credit days !')", true);
                return;
            }
            else
            {
                txtAccCode.Focus();
            }
        }

        protected void txtPortFromCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtPortFromCode.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}
                if (txtPortFromCode.Text != "")
                {
                    if (txtPortToCode.Text == txtPortFromCode.Text)
                    {
                        txtPortFromCode.Text = "";
                        txtPortFromCode.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Both from port and to port cannot be same !')", true);
                        return;
                    }
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                    DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, "CODE", txtPortFromCode.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtPortFromCode.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtPortFromCode.ToolTip = toolTip;
                    }
                    else
                    {
                        txtPortFromCode.ToolTip = "";
                        txtPortFromCode.Text = "";
                        txtPortFromCode.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid from port code !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtPortFromCode.ToolTip = "";
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

        protected void txtPortToCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtCode.Text))
                //{
                //    txtPortToCode.Text = "";
                //    txtCode.Focus();
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the supplier code !')", true);
                //    return;
                //}

                if (txtPortToCode.Text != "")
                {
                    if (txtPortToCode.Text == txtPortFromCode.Text)
                    {
                        txtPortToCode.Text = "";
                        txtPortToCode.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Both from port and to port cannot be same !')", true);
                        return;
                    }
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtPortToCode.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtPortToCode.ToolTip = toolTip;
                    }
                    else
                    {
                        txtPortToCode.ToolTip = "";
                        txtPortToCode.Text = "";
                        txtPortToCode.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid to port code !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtPortToCode.ToolTip = "";
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

        protected void lbtnAddPort_Click(object sender, EventArgs e)
        {
            try
            {
                #region validatePort
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    _supPorts = new List<SupplierPort>();
                    Session["_supPorts"] = _supPorts;
                    txtCode.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a supplier code ')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtPortFromCode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a port from code !!!')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtPortToCode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a port to code !!!')", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtPorTime.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter lead time cmb!!!')", true);
                    return;
                }
                bool b1 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtPortToCode.Text.ToUpper() == row["Code"].ToString())
                        {
                            b1 = true;
                            break;
                        }
                    }
                }
                if (!b1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select a valid from port !!!')", true);
                    return;
                }
                bool b2 = false;
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtPortToCode.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            break;
                        }
                    }
                }
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid to port !!!')", true);
                    return;
                }
                if (txtPorTime.Text.Length > 6)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid time cmb !')", true);
                    txtPorTime.Text = "";
                    txtPorTime.Focus();
                    return;
                }
                int time = 0;
                try
                {
                    Int32.TryParse(txtPorTime.Text, out time);
                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Lead time cmb is invalid !')", true);
                    return;
                }

                #endregion
                //Add section
                SupplierPort supPort = new SupplierPort();
                supPort.MSPR_COM = Session["UserCompanyCode"].ToString();
                supPort.MSPR_CD = txtCode.Text;
                supPort.MSPR_TP = ddlCodeType.SelectedValue;
                supPort.MSPR_FRM_PORT = txtPortFromCode.Text.ToUpper();
                supPort.MSPR_TO_PORT = txtPortToCode.Text.ToUpper();
                supPort.MSPR_LEAD_TIME = Convert.ToInt32(txtPorTime.Text);
                supPort.MSPR_ACT = 1;
                supPort.MSPR_CRE_BY = Session["UserID"].ToString();
                supPort.MSPR_CRE_DT = DateTime.Now;
                supPort.MSPR_MOD_BY = Session["UserID"].ToString();
                supPort.MSPR_MOD_DT = DateTime.Now;
                _supPorts = (List<SupplierPort>)Session["_supPorts"];
                var v = new SupplierPort();
                v = _supPorts.Where(c =>
             c.MSPR_COM == supPort.MSPR_COM &&
             c.MSPR_CD == supPort.MSPR_CD &&
             c.MSPR_TP == supPort.MSPR_TP &&
             c.MSPR_FRM_PORT == supPort.MSPR_FRM_PORT &&
             c.MSPR_TO_PORT == supPort.MSPR_TO_PORT).SingleOrDefault();
                if (v != null)
                {
                    _supPorts.Where(c =>
                   c.MSPR_COM == supPort.MSPR_COM &&
                   c.MSPR_CD == supPort.MSPR_CD &&
                   c.MSPR_TP == supPort.MSPR_TP &&
                   c.MSPR_FRM_PORT == supPort.MSPR_FRM_PORT &&
                   c.MSPR_TO_PORT == supPort.MSPR_TO_PORT).SingleOrDefault().MSPR_ACT = supPort.MSPR_ACT;
                    _supPorts.Where(c =>
                  c.MSPR_COM == supPort.MSPR_COM &&
                  c.MSPR_CD == supPort.MSPR_CD &&
                  c.MSPR_TP == supPort.MSPR_TP &&
                  c.MSPR_FRM_PORT == supPort.MSPR_FRM_PORT &&
                  c.MSPR_TO_PORT == supPort.MSPR_TO_PORT).SingleOrDefault().MSPR_LEAD_TIME = supPort.MSPR_LEAD_TIME;
                }

                else
                {
                    _supPorts.Add(supPort);
                }
                Session["_supPorts"] = _supPorts;
                dgvPort.DataSource = new int[] { };
                if (_supPorts.Count > 0)
                {
                    var data = _supPorts.Where(c => c.MSPR_ACT == 1).ToList();
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add(new DataColumn("FromPortCd", typeof(string)));
                    dt.Columns.Add(new DataColumn("FromPortDesc", typeof(string)));
                    dt.Columns.Add(new DataColumn("ToPortCd", typeof(string)));
                    dt.Columns.Add(new DataColumn("ToPortDesc", typeof(string)));
                    dt.Columns.Add(new DataColumn("CmbTime", typeof(Int32)));

                    foreach (var port in data)
                    {
                        dr = dt.NewRow();
                        dr["FromPortCd"] = port.MSPR_FRM_PORT;
                        DataTable dtFrom = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_FRM_PORT);
                        string frDesc = "";
                        if (dtFrom.Rows.Count > 0)
                        {
                            frDesc = dtFrom.Rows[0][1].ToString();
                        }
                        dr["FromPortDesc"] = frDesc;
                        dr["ToPortCd"] = port.MSPR_TO_PORT;
                        DataTable dtTo = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_TO_PORT);
                        string toDesc = "";
                        if (dtTo.Rows.Count > 0)
                        {
                            toDesc = dtTo.Rows[0][1].ToString();
                        }
                        dr["ToPortDesc"] = toDesc;
                        dr["CmbTime"] = port.MSPR_LEAD_TIME;
                        dt.Rows.Add(dr);
                    }
                    dgvPort.DataSource = dt;
                }
                dgvPort.DataBind();
                txtPortFromCode.Text = "";
                txtPortToCode.Text = "";
                txtPorTime.Text = "";
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
        protected void lbtnDelPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdfDelete.Value == "Yes")
                {
                    _supPorts = (List<SupplierPort>)Session["_supPorts"];
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        Label lblFrCode = (Label)row.FindControl("lblFrCode");
                        Label lblToCode = (Label)row.FindControl("lblToCode");

                        var v = _supPorts.Where(c =>
                           c.MSPR_COM == Session["UserCompanyCode"].ToString() &&
                           c.MSPR_CD == txtCode.Text &&
                           c.MSPR_TP == ddlCodeType.SelectedValue &&
                           c.MSPR_FRM_PORT == lblFrCode.Text &&
                           c.MSPR_TO_PORT == lblToCode.Text).SingleOrDefault();
                        if (v != null)
                        {
                            _supPorts.Where(c =>
                           c.MSPR_COM == Session["UserCompanyCode"].ToString() &&
                           c.MSPR_CD == txtCode.Text &&
                           c.MSPR_TP == ddlCodeType.SelectedValue &&
                           c.MSPR_FRM_PORT == lblFrCode.Text &&
                           c.MSPR_TO_PORT == lblToCode.Text).SingleOrDefault().MSPR_ACT = 0;
                        }
                    }
                    Session["_supPorts"] = _supPorts;
                    dgvPort.DataSource = new int[] { };
                    if (_supPorts.Count > 0)
                    {
                        var data = _supPorts.Where(c => c.MSPR_ACT == 1).ToList();
                        DataTable dt = new DataTable();
                        DataRow dr;
                        dt.Columns.Add(new DataColumn("FromPortCd", typeof(string)));
                        dt.Columns.Add(new DataColumn("FromPortDesc", typeof(string)));
                        dt.Columns.Add(new DataColumn("ToPortCd", typeof(string)));
                        dt.Columns.Add(new DataColumn("ToPortDesc", typeof(string)));
                        dt.Columns.Add(new DataColumn("CmbTime", typeof(Int32)));

                        foreach (var port in data)
                        {
                            dr = dt.NewRow();
                            dr["FromPortCd"] = port.MSPR_FRM_PORT;
                            DataTable dtFrom = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_FRM_PORT);
                            string frDesc = "";
                            if (dtFrom.Rows.Count > 0)
                            {
                                frDesc = dtFrom.Rows[0][1].ToString();
                            }
                            dr["FromPortDesc"] = frDesc;
                            dr["ToPortCd"] = port.MSPR_TO_PORT;
                            DataTable dtTo = CHNLSVC.General.GET_PORT_DATA_BY_CD(port.MSPR_TO_PORT);
                            string toDesc = "";
                            if (dtTo.Rows.Count > 0)
                            {
                                toDesc = dtTo.Rows[0][1].ToString();
                            }
                            dr["ToPortDesc"] = toDesc;
                            dr["CmbTime"] = port.MSPR_LEAD_TIME;
                            dt.Rows.Add(dr);
                        }
                        dgvPort.DataSource = dt;
                    }
                    dgvPort.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //var ch = (CheckBox)sender;
            //    foreach (GridViewRow row  in dgvSelItms.Rows)
            //    {
            //        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            //        chkSelect.Checked = ch.Checked;
            //    }
        }

        protected void chkAllMainCat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllMainCat.Checked)
            {
                //txtMainCat.Text = "";
                txtSubCat.Text = "";
                txtItemCode.Text = "";
                txtBrand.Text = "";
                txtModel.Text = "";

                txtMainCat.Enabled = true;
                txtSubCat.Enabled = false;
                txtItemCode.Enabled = false;
                txtBrand.Enabled = false;
                txtModel.Enabled = false;

                emptyText = "All data Select...";
                //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
            }
            else
            {
                if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllItem.Checked && !chkAllBrand.Checked && !chkAllModel.Checked)
                {
                    txtMainCat.Enabled = true;
                    txtSubCat.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand.Enabled = true;
                    txtModel.Enabled = true;

                    emptyText = "No data found...";
                    //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                    dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
                }
            }
        }

        protected void chkAllSub_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSub.Checked)
            {
                //txtMainCat.Text = "";
                //txtSubCat.Text = "";
                txtItemCode.Text = "";
                txtBrand.Text = "";
                txtModel.Text = "";

                txtMainCat.Enabled = true;
                txtSubCat.Enabled = true;
                txtItemCode.Enabled = false;
                txtBrand.Enabled = false;
                txtModel.Enabled = false;
                emptyText = "All data Select...";
                //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
            }
            else
            {
                if (!chkAllMainCat.Checked && !chkAllItem.Checked && !chkAllBrand.Checked && !chkAllModel.Checked)
                {
                    txtMainCat.Enabled = true;
                    txtSubCat.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand.Enabled = true;
                    txtModel.Enabled = true;

                    emptyText = "No data found...";
                    //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                    dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
                }
            }
        }

        protected void chkAllItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllItem.Checked)
            {
                //txtMainCat.Text = "";
                //txtSubCat.Text = "";
                // txtItemCode.Text = "";
                txtBrand.Text = "";
                txtModel.Text = "";

                txtMainCat.Enabled = true;
                txtSubCat.Enabled = true;
                txtItemCode.Enabled = true;
                txtBrand.Enabled = false;
                txtModel.Enabled = false;
                emptyText = "All data Select...";
                //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
            }
            else
            {
                if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllBrand.Checked && !chkAllModel.Checked)
                {
                    txtMainCat.Enabled = true;
                    txtSubCat.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand.Enabled = true;
                    txtModel.Enabled = true;

                    emptyText = "No data found...";
                    //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                    dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
                }
            }
        }

        protected void chkAllBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrand.Checked)
            {
                //txtMainCat.Text = "";
                //txtSubCat.Text = "";
                // txtItemCode.Text = "";
                // txtBrand.Text = "";
                txtModel.Text = "";

                txtMainCat.Enabled = true;
                txtSubCat.Enabled = true;
                txtItemCode.Enabled = true;
                txtBrand.Enabled = true;
                txtModel.Enabled = false;
                emptyText = "All data Select...";
                //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
            }
            else
            {
                if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllItem.Checked && !chkAllModel.Checked)
                {
                    txtMainCat.Enabled = true;
                    txtSubCat.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand.Enabled = true;
                    txtModel.Enabled = true;

                    emptyText = "No data found...";
                    //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                    dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
                }
            }
        }

        protected void chkAllModel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllModel.Checked)
            {
                //txtMainCat.Text = "";
                //txtSubCat.Text = "";
                // txtItemCode.Text = "";
                // txtBrand.Text = "";
                //txtModel.Text = "";

                txtMainCat.Enabled = true;
                txtSubCat.Enabled = true;
                txtItemCode.Enabled = true;
                txtBrand.Enabled = true;
                txtModel.Enabled = true;
                emptyText = "All data Select...";
                //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
            }
            else
            {
                if (!chkAllMainCat.Checked && !chkAllSub.Checked && !chkAllItem.Checked && !chkAllBrand.Checked)
                {
                    txtMainCat.Enabled = true;
                    txtSubCat.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand.Enabled = true;
                    txtModel.Enabled = true;

                    emptyText = "No data found...";
                    //dgvSupItms.DataSource = new int[] { }; dgvSupItms.EmptyDataText = emptyText; dgvSupItms.DataBind();
                    dgvSelItms.DataSource = new int[] { }; dgvSelItms.EmptyDataText = emptyText; dgvSelItms.DataBind();
                }
            }
        }

        protected void lbtnNewSupNo_Click(object sender, EventArgs e)
        {
            popupSupplier.Hide();
            Response.Redirect(Request.RawUrl);
        }

        protected void lbtnNewSupOk_Click(object sender, EventArgs e)
        {
            try
            {
                popupSupplier.Hide();
                LoadCustProf(_selectBusEnt);
                _selectBusEnt = new MasterBusinessEntity();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void txtNBTType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNBTType.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    //DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
                    DataTable _result = CHNLSVC.CommonSearch.Search_Tax_CODES(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtNBTType.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["RATE CODE"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtNBTType.ToolTip = toolTip;
                    }
                    else
                    {
                        txtNBTType.ToolTip = "";
                        txtNBTType.Text = "";
                        txtNBTType.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid NBT Code !!!')", true);
                        return;
                    }
                }
                else
                {
                    txtModel.ToolTip = "";
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

        protected void lbtnNBT_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "NBTCode";
                Session["NBTCode"] = null;
                //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                //DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);

                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
                DataTable _result = CHNLSVC.CommonSearch.Search_Tax_CODES(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                Session["NBTCode"] = _result;

                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                    Session["NBTCode"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        void BindCombo()
        {
            DataTable dtwar = CHNLSVC.General.GetWarrantyPeriod();
            if (dtwar != null && dtwar.Rows.Count > 0)
            {
                ddlSupPrd.DataSource = dtwar;
                ddlSupPrd.DataTextField = "wp_des";
                ddlSupPrd.DataValueField = "wp_period";
                ddlSupPrd.DataBind();
            }
        }
        public bool validateinputString(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!regexItem.IsMatch(input))
            {
                return false;
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!(regexItem.IsMatch(input) || input.Contains(" ")))
            {
                return false;
            }
            return true;
        }
        private void DisplayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtName.Text))
            {
                DisplayMessage("Invalid charactor found in supplier name.");
                txtName.Focus();
                return;
            }
        }

        protected void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtAddress1.Text))
            {
                DisplayMessage("Invalid charactor found in address 1.");
                txtAddress1.Focus();
                return;
            }
        }

        protected void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtAddress2.Text))
            {
                DisplayMessage("Invalid charactor found in address 2.");
                txtAddress2.Focus();
                return;
            }
        }

        protected void txtContactPerson_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtContactPerson.Text))
            {
                DisplayMessage("Invalid charactor found in contact person.");
                txtContactPerson.Focus();
                return;
            }
        }

        protected void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPhone.Text))
            {
                DisplayMessage("Invalid charactor found in phone no.");
                txtPhone.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtPhone.Text.Trim());
                if (_isValid == false)
                {
                    txtPhone.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid phone # !!!')", true);
                    return;
                }
            }
            else
            {
                txtFax.Focus();
            }
        }

        protected void txtFax_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtFax.Text))
            {
                DisplayMessage("Invalid charactor found in fax no.");
                txtFax.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtFax.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtFax.Text.Trim());
                if (_isValid == false)
                {
                    txtFax.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid fax # !!!')", true);
                    return;
                }
            }
            else
            {
                txtFax.Focus();
            }
        }

        protected void txtWebSite_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtWebSite.Text))
            {
                DisplayMessage("Invalid charactor found in web site name.");
                txtWebSite.Focus();
                return;
            }
        }

        protected void txtAccCode_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtAccCode.Text))
            {
                DisplayMessage("Invalid charactor found in GL Acc code.");
                txtAccCode.Focus();
                return;
            }
        }

        protected void txtTinNo_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtTinNo.Text))
            {
                DisplayMessage("Invalid charactor found in tin no.");
                txtTinNo.Focus();
                return;
            }
        }

        protected void txtTaxReg_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtTaxReg.Text))
            {
                DisplayMessage("Invalid charactor found in tax reg no.");
                txtTaxReg.Focus();
                return;
            }
        }

        protected void txtNBTRate_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtNBTRate.Text))
            {
                DisplayMessage("Invalid charactor found in NBT rate.");
                txtNBTRate.Focus();
                return;
            }
        }

        protected void txtDivAmt_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtDivAmt.Text))
            {
                DisplayMessage("Invalid charactor found in dividend amount.");
                txtDivAmt.Focus();
                return;
            }
        }

        protected void txtSupRem_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtSupRem.Text))
            {
                DisplayMessage("Invalid charactor found in supplier remark.");
                txtSupRem.Focus();
                return;
            }
        }
    }
}