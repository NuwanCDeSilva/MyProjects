using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles
{
    public partial class SupplierProfile : BasePage
    {
        string _userid = string.Empty;
        Boolean _IsCat = false;
        private Boolean _isExsit = false;
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private Boolean _isGroup = false;
        DataTable uniqueColscurrency = new DataTable();
        DataTable uniqueColsPort = new DataTable();
        DataTable uniqueColsItems = new DataTable();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtsupcode.Focus();
                    FillEmptyGrids();
                    PopulateDropDowns();

                    txttaxcode.Enabled = false;
                    txttaxrate.Enabled = false;
                    txttaxdivrate.Enabled = false;
                    lbtncoderate.Visible = false;
                    txttaxratecd.Enabled = false;

                    ViewState["CurrencyTable"] = null;
                    DataTable dtcurrency = new DataTable();
                    dtcurrency.Columns.AddRange(new DataColumn[2] { new DataColumn("Code"), new DataColumn("Description") });
                    ViewState["CurrencyTable"] = dtcurrency;
                    this.BindCurrencyGrid();

                    ViewState["PortsTable"] = null;
                    DataTable dtports = new DataTable();
                    dtports.Columns.AddRange(new DataColumn[3] { new DataColumn("mspr_frm_port"), new DataColumn("mp_name"), new DataColumn("mspr_lead_time") });
                    ViewState["PortsTable"] = dtports;
                    this.BindPortGrid();

                    ViewState["ItemsTable"] = null;
                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[2] { new DataColumn("mbii_itm_cd"), new DataColumn("mi_shortdesc") });
                    ViewState["ItemsTable"] = dtitems;
                    this.BindItemGrid();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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

        protected void InsertToCurrencyGrid()
        {
            try
            {
                DivsHide();
                if (string.IsNullOrEmpty(txtcurrency.Text))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select currency !!!";
                    lbtnfindcurr.Focus();
                    return;
                }

                DataTable dt = (DataTable)ViewState["CurrencyTable"];
                dt.Rows.Add(txtcurrency.Text.Trim(), txtcurrencyname.Text.Trim());
                ViewState["CurrencyTable"] = dt;

                uniqueColscurrency = RemoveDuplicateRows(dt, "Code");

                grdsupcurrency.DataSource = uniqueColscurrency;
                grdsupcurrency.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void BindCurrencyGrid()
        {
            try
            {
                grdsupcurrency.DataSource = (DataTable)ViewState["CurrencyTable"];
                grdsupcurrency.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void BindPortGrid()
        {
            try
            {
                grdports.DataSource = (DataTable)ViewState["PortsTable"];
                grdports.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void BindItemGrid()
        {
            try
            {
                grdallitems.DataSource = (DataTable)ViewState["ItemsTable"];
                grdallitems.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void FillEmptyGrids()
        {
            try
            {
                grdsupcurrency.DataSource = new int[] { };
                grdsupcurrency.DataBind();

                grdports.DataSource = new int[] { };
                grdports.DataBind();

                grdselecteditems.DataSource = new int[] { };
                grdselecteditems.DataBind();

                grdallitems.DataSource = new int[] { };
                grdallitems.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndivokclose_Click(object sender, EventArgs e)
        {
            try
            {
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndicalertclose_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndivinfoclose_Click(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Clear();
                    DivsHide();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        private void Clear()
        {
            try
            {
                txtsupcode.Text = string.Empty;
                txtsupname.Text = string.Empty;
                chkactive.Checked = false;
                txttin.Text = string.Empty;
                txttaxrg.Text = string.Empty;
                txtadd1.Text = string.Empty;
                txtadd2.Text = string.Empty;
                txtcontactperson.Text = string.Empty;
                txttel.Text = string.Empty;
                txtfax.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtweb.Text = string.Empty;
                txtcountry.Text = string.Empty;
                txtcountry2.Text = string.Empty;
                txtdelcurr.Text = string.Empty;
                txtdelcurr2.Text = string.Empty;
                txtcreditperiod.Text = string.Empty;
                txtglacc.Text = string.Empty;
                ddlsuptype.SelectedIndex = 0;
                txttaxcat.Text = string.Empty;
                chksptax.Checked = false;
                txttaxcode.Text = string.Empty;
                txttaxrate.Text = string.Empty;
                txttaxdivrate.Text = string.Empty;
                txtcurrency.Text = string.Empty;
                txtportcode.Text = string.Empty;
                txtportname.Text = string.Empty;
                txtleadtime.Text = string.Empty;
                txtmaincat.Text = string.Empty;
                txtsubcat.Text = string.Empty;
                txtitmrange.Text = string.Empty;
                txtbrand.Text = string.Empty;
                txtitemcode.Text = string.Empty;
                txtmodelno.Text = string.Empty;
                txtsupcode.Text = string.Empty;
                _isExsit = false;
                txttaxratecd.Text = string.Empty;

                ViewState["CurrencyTable"] = null;
                DataTable dtcurrency = new DataTable();
                dtcurrency.Columns.AddRange(new DataColumn[2] { new DataColumn("Code"), new DataColumn("Description") });
                ViewState["CurrencyTable"] = dtcurrency;
                this.BindCurrencyGrid();

                ViewState["PortsTable"] = null;
                DataTable dtports = new DataTable();
                dtports.Columns.AddRange(new DataColumn[3] { new DataColumn("mspr_frm_port"), new DataColumn("mp_name"), new DataColumn("mspr_lead_time") });
                ViewState["PortsTable"] = dtports;
                this.BindPortGrid();

                ViewState["ItemsTable"] = null;
                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[2] { new DataColumn("mbii_itm_cd"), new DataColumn("mi_shortdesc") });
                ViewState["ItemsTable"] = dtitems;
                this.BindItemGrid();

                SessionClear();

                chksptax.Checked = false;
                txttaxcode.Enabled = false;
                txttaxratecd.Enabled = false;
                txttaxrate.Enabled = false;
                txttaxdivrate.Enabled = false;
                lbtncoderate.Visible = false;

            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void PopulateDropDowns()
        {
            try
            {
                ddlsuptype.Items.Insert(0, new ListItem("Select", ""));
                ddlsuptype.Items.Insert(1, new ListItem("Foreign", "F"));
                ddlsuptype.Items.Insert(2, new ListItem("Local", "L"));
                ddlsuptype.Items.Insert(3, new ListItem("Imports", "I"));
                ddlsuptype.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void chksptax_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksptax.Checked == true)
                {
                    txttaxcode.Enabled = true;
                    txttaxrate.Enabled = true;
                    txttaxdivrate.Enabled = true;
                    txttaxratecd.Enabled = true;
                    lbtncoderate.Visible = true;
                }
                else
                {
                    txttaxcode.Enabled = false;
                    txttaxrate.Enabled = false;
                    txttaxdivrate.Enabled = false;
                    txttaxratecd.Enabled = false;
                    lbtncoderate.Visible = false;

                    //txttaxcode.Text = string.Empty;
                    //txttaxrate.Text = string.Empty;
                    //txttaxdivrate.Text = string.Empty;
                    //txttaxratecd.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void Collect_GroupCust()
        {
            try
            {
                DateTime? dob = null;
                _userid = (string)Session["UserID"];

                _custGroup = new GroupBussinessEntity();
                _custGroup.Mbg_cd = txtsupcode.Text.Trim();
                _custGroup.Mbg_name = txtsupname.Text.Trim();
                _custGroup.Mbg_tit = string.Empty;
                _custGroup.Mbg_ini = string.Empty;
                _custGroup.Mbg_fname = string.Empty;
                _custGroup.Mbg_sname = string.Empty;
                _custGroup.Mbg_nationality = "SL";
                _custGroup.Mbg_add1 = txtadd1.Text.Trim();
                _custGroup.Mbg_add2 = txtadd2.Text.Trim();
                _custGroup.Mbg_town_cd = string.Empty;
                _custGroup.Mbg_distric_cd = string.Empty;
                _custGroup.Mbg_province_cd = string.Empty;
                _custGroup.Mbg_country_cd = txtcountry.Text.Trim();
                _custGroup.Mbg_tel = txttel.Text.Trim();
                _custGroup.Mbg_fax = txtfax.Text.Trim();
                _custGroup.Mbg_postal_cd = string.Empty;
                _custGroup.Mbg_mob = string.Empty;
                _custGroup.Mbg_nic = string.Empty;
                _custGroup.Mbg_pp_no = string.Empty;
                _custGroup.Mbg_dl_no = string.Empty;
                _custGroup.Mbg_br_no = string.Empty;
                _custGroup.Mbg_email = txtemail.Text.Trim();
                _custGroup.Mbg_contact = txtcontactperson.Text.Trim();
                _custGroup.Mbg_act = true;
                _custGroup.Mbg_is_suspend = false;
                _custGroup.Mbg_sex = string.Empty;
                _custGroup.Mbg_dob = Convert.ToDateTime(dob);
                _custGroup.Mbg_cre_by = _userid;
                _custGroup.Mbg_cre_dt = CHNLSVC.Security.GetServerDateTime();
                _custGroup.Mbg_mod_by = _userid;
                _custGroup.Mbg_mod_dt = CHNLSVC.Security.GetServerDateTime();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void Collect_Cust()
        {
            try
            {
                Boolean _isSMS = false;
                Boolean _isSVAT = false;
                Boolean _isVAT = false;
                Boolean _TaxEx = false;
                Boolean _isEmail = false;
                _custProfile = new MasterBusinessEntity();
                _custProfile.Mbe_act = true;
                _custProfile.Mbe_add1 = txtadd1.Text.Trim();
                _custProfile.Mbe_add2 = txtadd2.Text.Trim();
                _userid = (string)Session["UserID"];
                DateTime? dob = null;

               _isSMS = false;

               if (string.IsNullOrEmpty(txtsupcode.Text.Trim()))
               {
                   _isExsit = false;
               }
               else
               {
                   _isExsit = true;
               }

               _custProfile.Mbe_acc_cd = txtglacc.Text.Trim();
                _custProfile.Mbe_agre_send_sms = _isSMS;
                _custProfile.Mbe_br_no = string.Empty;
                _custProfile.Mbe_cate = txttaxcat.Text.Trim();
                if (_isExsit == false && _isGroup == false)
                {
                    _custProfile.Mbe_cd = null;
                }
                else
                {
                    _custProfile.Mbe_cd = txtsupcode.Text.Trim();
                }
                _custProfile.Mbe_com = Session["UserCompanyCode"].ToString();
                _custProfile.Mbe_contact = txtcontactperson.Text.Trim();
                _custProfile.Mbe_country_cd = txtcountry.Text.Trim();
                _custProfile.Mbe_cr_add1 = txtadd1.Text.Trim();
                _custProfile.Mbe_cr_add2 = txtadd2.Text.Trim();
                _custProfile.Mbe_cr_country_cd = txtcountry.Text.Trim();
                _custProfile.Mbe_cr_distric_cd = string.Empty;
                _custProfile.Mbe_cr_email = txtemail.Text.Trim();
                _custProfile.Mbe_cr_fax = txtfax.Text.Trim();
                _custProfile.Mbe_cr_postal_cd = string.Empty;
                _custProfile.Mbe_cr_province_cd = string.Empty;
                _custProfile.Mbe_cr_tel = txttel.Text.Trim();
                _custProfile.Mbe_cr_town_cd = string.Empty;
                _custProfile.Mbe_cre_by = _userid;
                _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
                _custProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
                _custProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
                _custProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
                _custProfile.Mbe_distric_cd = string.Empty;
                _custProfile.Mbe_dl_no = string.Empty;
                _custProfile.Mbe_dob = Convert.ToDateTime(dob).Date;
                _custProfile.Mbe_email = txtemail.Text.Trim();
                _custProfile.Mbe_fax = txtfax.Text.Trim();
                _custProfile.Mbe_ho_stus = "GOOD";
                _custProfile.Mbe_income_grup = string.Empty;
                _custProfile.Mbe_intr_com = false;
                _custProfile.Mbe_is_suspend = false;

                if (chksptax.Checked == true)
                {
                    _isSVAT = true;
                }
                else
                {
                    _isSVAT = false;
                }

                _custProfile.Mbe_is_svat = _isSVAT;

                _isVAT = false;
                
                _custProfile.Mbe_is_tax = _isVAT;
                _custProfile.Mbe_mob = string.Empty;
                _custProfile.Mbe_name = txtsupname.Text.Trim();
                _custProfile.Mbe_nic = string.Empty;
                _custProfile.Mbe_oth_id_no = txttin.Text.Trim();
                _custProfile.Mbe_oth_id_tp = "TIN";
                _custProfile.Mbe_pc_stus = "GOOD";
                _custProfile.Mbe_postal_cd = string.Empty;
                _custProfile.Mbe_pp_no = string.Empty;
                _custProfile.Mbe_province_cd = string.Empty;
                _custProfile.Mbe_sex = string.Empty;
                _custProfile.Mbe_sub_tp = ddlsuptype.SelectedValue;
                _custProfile.Mbe_svat_no = string.Empty;

              
                _TaxEx = false;
                
                _custProfile.Mbe_tax_ex = _TaxEx;
                _custProfile.Mbe_tax_no = string.Empty;
                _custProfile.Mbe_tel = txttel.Text.Trim();
                _custProfile.Mbe_town_cd = string.Empty;
                _custProfile.Mbe_tp = "S";
                _custProfile.Mbe_wr_add1 = string.Empty;
                _custProfile.Mbe_wr_add2 = string.Empty;
                _custProfile.Mbe_wr_com_name = string.Empty;
                _custProfile.Mbe_wr_country_cd = txtcountry.Text.Trim();
                _custProfile.Mbe_wr_dept = string.Empty;
                _custProfile.Mbe_wr_designation = string.Empty;
                _custProfile.Mbe_wr_distric_cd = string.Empty;
                _custProfile.Mbe_wr_email = string.Empty;
                _custProfile.Mbe_wr_fax = string.Empty;
                _custProfile.Mbe_wr_proffesion = string.Empty;
                _custProfile.Mbe_wr_province_cd = string.Empty;
                _custProfile.Mbe_wr_tel = string.Empty;
                _custProfile.Mbe_wr_town_cd = string.Empty;
                _custProfile.MBE_FNAME = string.Empty;
                _custProfile.MBE_SNAME = string.Empty;
                _custProfile.MBE_INI = string.Empty;
                _custProfile.MBE_TIT = string.Empty;

                _isEmail = false;
               
                _custProfile.Mbe_agre_send_email = _isEmail;
                _custProfile.Mbe_cust_lang = string.Empty;
                _custProfile.Mbe_nationality = "SL";
                _custProfile.MBE_WEB = txtweb.Text.Trim();

                Int32 creditperiod = 0;
                if (string.IsNullOrEmpty(txtcreditperiod.Text.Trim()))
                {
                    creditperiod = 0;
                }
                else
                {
                    creditperiod= Convert.ToInt32(txtcreditperiod.Text.Trim());
                }
                _custProfile.MBE_CR_PERIOD = Convert.ToInt32(creditperiod);
                _custProfile.Mbe_cur_cd = txtdelcurr.Text;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SaveSupplier()
        {
            try
            {
                Int32 _effect = 0;
                string _cusCode = "";
                Collect_Cust();
                Collect_GroupCust();

                if (_isExsit == false)
                {
                    _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList,null, out _cusCode, null, _isExsit, _isGroup, _custGroup);
                }
                else
                {
                    _cusCode = txtsupcode.Text.Trim();
                    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList,null, null, _custGroup);
                }

                if (_effect == 1)
                {
                    if (_isExsit == false)
                    {
                        divok.Visible = true;
                        lblok.Text = "New supplier created. Supplier Code : " + _cusCode;
                        txtsupcode.Text = _cusCode;
                    }
                    else
                    {
                        divok.Visible = true;
                        lblok.Text = "Supplier updated. Supplier Code : " + _cusCode;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_cusCode))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Terminated !!! " + _cusCode;
                        return;
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Erroe occured !!! " + _cusCode;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void DivsHide()
        {
            try
            {
                divalert.Visible = false;
                divok.Visible = false;
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnApproval_Click(object sender, EventArgs e)
        {
            if (txtsavesupplier.Value == "Yes")
            {
                try
                {
                    DivsHide();

                    SaveSupplier();

                    if (chksptax.Checked == true)
                    {
                        SaveSupplierTaxCodes();
                    }
                    SaveSupplierCurrency();
                    SaveSupplierPorts();
                    SaveSupplierItems();

                    Clear();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {
                        paramsText.Append(txtmaincat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Ports:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tax:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxCodes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + seperator);
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
                if (lblvalue.Text == "407")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "407";
                    ViewState["SEARCH"] = result;
                    UserPopupSupplier.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "401")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                    DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "401";
                    ViewState["SEARCH"] = result;
                    UserPopupSupplier.Show();
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
                    UserPopupSupplier.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "332")
                {
                    txtmaincat.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "333")
                {
                    txtsubcat.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "406")
                {
                    txtmodelno.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "407")
                {
                    txtitemcode.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "144")
                {
                    txtbrand.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "341")
                {
                    txtcountry.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtcountry2.Text = grdResult.SelectedRow.Cells[2].Text;
                }

                if (lblvalue.Text == "14")
                {
                    txtcurrency.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtcurrencyname.Text = grdResult.SelectedRow.Cells[2].Text;
                }

                if (lblvalue.Text == "335")
                {
                    txtitmrange.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "415")
                {
                    txtportcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtportname.Text = grdResult.SelectedRow.Cells[2].Text;
                }

                if (lblvalue.Text == "333a")
                {
                    txtitmrange.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "416")
                {
                    txttaxcat.Text = grdResult.SelectedRow.Cells[2].Text;
                }

                if (lblvalue.Text == "417")
                {
                    txttaxcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txttaxratecd.Text = grdResult.SelectedRow.Cells[2].Text;
                    txttaxrate.Text = grdResult.SelectedRow.Cells[3].Text;
                    txttaxdivrate.Text = grdResult.SelectedRow.Cells[4].Text;
                }

                if (lblvalue.Text == "14a")
                {
                    txtdelcurr.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtdelcurr2.Text = grdResult.SelectedRow.Cells[2].Text;
                }

                if (lblvalue.Text == "401")
                {
                    txtsupcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    this.txtsupcode_TextChanged(null, null);
                    LoadSupplierData();
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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

        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

            //List<GroupBussinessEntity> results = new List<GroupBussinessEntity>();
            //DataTable dt = new DataTable();
            //DivsHide();

            //ViewState["SEARCH"] = null;
            //txtSearchbyword.Text = string.Empty;

            //GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtsupcode.Text.Trim().ToUpper());
            //results.Add(_grupProf);
            //dt = ConvertToDataTable(results);
            //grdResult.DataSource = dt;
            //grdResult.DataBind();
            //lblvalue.Text = "401";
            //BindUCtrlDDLData(dt);
            //ViewState["SEARCH"] = dt;
            //UserPopupSupplier.Show();
            //txtSearchbyword.Focus();


        }



        protected void lbtnsupsearch_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadSupplierData()
        {
            try
            {
                LoadSupplierCurrencies();
                LoadSupplierPorts();
                LoadSupplierItems();
                LoadSupplierTaxCodes();
                LoadCountryText();
                LoadCurrencyText();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnmaincat_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "332";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnsubcat_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "333";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnmodel_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportModel);
                DataTable result = CHNLSVC.CommonSearch.SearchModel(SearchParams);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "406";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnitemcode_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnbrand_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "144";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtncountry_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
                DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "341";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnfindcurr_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnitmrange_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "333a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnaddcurr_Click(object sender, EventArgs e)
        {
            try
            {
                InsertToCurrencyGrid();
                txtcurrency.Text = string.Empty;
                txtcurrencyname.Text = string.Empty;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdsupcurrency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string currency = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[4].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete currency " + currency + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdsupcurrency_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["CurrencyTable"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["CurrencyTable"] = dt;
                BindCurrencyGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private List<string> get_selected_CurrencyItems()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grdsupcurrency.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        private List<string> get_selected_PortList()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grdports.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        private List<string> get_selected_ItemList()
        {
            List<string> list = new List<string>();

            foreach (GridViewRow r in grdallitems.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkRow");
                if (chk != null && chk.Checked)
                {
                    list.Add(r.Cells[1].Text.ToString());
                }
            }
            return list;
        }

        protected void lbtndelallcurr_Click(object sender, EventArgs e)
        {
            DivsHide();
            if (txtdelallcurr.Value == "Yes")
            {
                try
                {
                    if (grdsupcurrency.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Currency list is empty !!!";
                        return;
                    }

                    List<string> selectedlist = get_selected_CurrencyItems();

                    if ((selectedlist == null) || (selectedlist.Count == 0))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select currency/currencies to delete !!!";
                        return;
                    }

                    DataTable dtTemp = new DataTable();
                    foreach (GridViewRow dgvr in grdsupcurrency.Rows)
                    {
                        Int32 gridline = dgvr.RowIndex;
                        CheckBox chk = (CheckBox)dgvr.FindControl("chkRow");
                        if (chk != null & chk.Checked)
                        {
                            dtTemp = ViewState["CurrencyTable"] as DataTable;
                            dtTemp.Rows[gridline].Delete();
                            ViewState["CurrencyTable"] = dtTemp;
                        }
                    }
                    dtTemp.AcceptChanges();
                    BindCurrencyGrid();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        private bool ValidateSupAddEntry()
        {
            string defaultcurrency = (string)Session["DEFAULT_CURRENCY"];
            if (string.IsNullOrEmpty(defaultcurrency))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select default currency !!!";
                grdsupcurrency.Focus();
                return false;
            }
            return true;
        }

        private void DeactivateSupCurrency()
        {
            try
            {
                Int32 _effect = 0;
                _userid = (string)Session["UserID"];
                SupplierCurrency _supcurrency = new SupplierCurrency();
                string suppliercode = string.Empty;

                suppliercode = txtsupcode.Text.Trim();

                bool isvalid = ValidateSupAddEntry();
                if (isvalid == false)
                {
                    return;
                }

                _supcurrency.MSCU_COM = Session["UserCompanyCode"].ToString();
                _supcurrency.MSCU_SUP_CD = suppliercode;
                _supcurrency.MSCU_ACT = 0;
                _supcurrency.MSCU_MOD_BY = _userid;
                _supcurrency.MSCU_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                _effect = CHNLSVC.General.DeactivateSupplierCurrency(_supcurrency);

            }
            catch (Exception ex)
            {
                divok.Visible = false;
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void SaveSupplierCurrency()
        {
            if (grdsupcurrency.Rows.Count > 0)
            {
                try
                {
                    DeactivateSupCurrency();

                    Int32 _effect = 0;
                    _userid = (string)Session["UserID"];
                    string defaultcurrency = (string)Session["DEFAULT_CURRENCY"];
                    SupplierCurrency _supcurrency = new SupplierCurrency();
                    string suppliercode = string.Empty;
                    Int32 isdefault = 0;

                    suppliercode = txtsupcode.Text.Trim();

                    bool isvalid = ValidateSupAddEntry();
                    if (isvalid == false)
                    {
                        return;
                    }

                    foreach (GridViewRow ddr in grdsupcurrency.Rows)
                    {
                        if (defaultcurrency == ddr.Cells[1].Text)
                        {
                            isdefault = 1;
                        }
                        else
                        {
                            isdefault = 0;
                        }

                        _supcurrency.MSCU_COM = Session["UserCompanyCode"].ToString();
                        _supcurrency.MSCU_SUP_CD = suppliercode;
                        _supcurrency.MSCU_CUR_CD = ddr.Cells[1].Text;
                        _supcurrency.MSCU_ACT = 1;
                        _supcurrency.MSCU_DEF = isdefault;
                        _supcurrency.MSCU_CRE_BY = _userid;
                        _supcurrency.MSCU_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                        _supcurrency.MSCU_MOD_BY = _userid;
                        _supcurrency.MSCU_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                        _effect = CHNLSVC.General.UpdateSupplierCurrency(_supcurrency);
                    }
                }
                catch (Exception ex)
                {
                    divok.Visible = false;
                    divalert.Visible = true;
                    lblalert.Text = ex.Message + " " + "Error saving supplier currency";
                }
            }
        }

        private void LoadSupplierCurrencies()
        {
            try
            {
                DataTable dtcurrencies = CHNLSVC.CommonSearch.LoadSupplierCurrencies(Session["UserCompanyCode"].ToString(), txtsupcode.Text.Trim());
                if (dtcurrencies.Rows.Count > 0)
                {
                    grdsupcurrency.DataSource = null;
                    grdsupcurrency.DataBind();

                    grdsupcurrency.DataSource = dtcurrencies;
                    grdsupcurrency.DataBind();
                }

                string defaultcurrency = string.Empty;
                string isdefault = string.Empty;
                Int32 SelectedRow = 0;

                foreach (DataRow dr in dtcurrencies.Rows)
                {
                    isdefault = dr[2].ToString();
                    if (isdefault == "1")
                    {
                        defaultcurrency = dr[0].ToString();
                        SelectedRow = dtcurrencies.Rows.IndexOf(dr);
                    }
                }

                Session["DEFAULT_CURRENCY"] = defaultcurrency;
                grdsupcurrency.SelectedIndex = SelectedRow;

                ViewState["CurrencyTable"] = null;
                this.BindCurrencyGrid();

                ViewState["CurrencyTable"] = dtcurrencies;
                this.BindCurrencyGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void SessionClear()
        {
            try
            {
                Session["DEFAULT_CURRENCY"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void grdsupcurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["DEFAULT_CURRENCY"] = grdsupcurrency.SelectedRow.Cells[1].Text;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnportcode_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                DataTable result = CHNLSVC.CommonSearch.LoadAllPorts(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "415";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private bool ValidatePortAdd()
        {
            if (string.IsNullOrEmpty(txtportcode.Text))
            {
                divalert.Visible = true;
                lblalert.Text = "Please select port !!!";
                lbtnportcode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtleadtime.Text))
            {
                divalert.Visible = true;
                lblalert.Text = "Please enter a lead time !!!";
                txtleadtime.Focus();
                return false;
            }
            return true;
        }
        protected void InsertToPortsGrid()
        {
            try
            {
                DivsHide();

                DataTable dt = (DataTable)ViewState["PortsTable"];
                dt.Rows.Add(txtportcode.Text.Trim(), txtportname.Text.Trim(), txtleadtime.Text.Trim());
                ViewState["PortsTable"] = dt;

                uniqueColsPort = RemoveDuplicateRows(dt, "mspr_frm_port");

                grdports.DataSource = uniqueColsPort;
                grdports.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtnaddports_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalidaddport = ValidatePortAdd();
                if (isvalidaddport == false)
                {
                    return;
                }

                InsertToPortsGrid();
                txtportcode.Text = string.Empty;
                txtportname.Text = string.Empty;
                txtleadtime.Text = string.Empty;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string port = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[5].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete port " + port + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdports_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["PortsTable"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["PortsTable"] = dt;
                BindPortGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndelports_Click(object sender, EventArgs e)
        {
            DivsHide();
            if (txtdelport.Value == "Yes")
            {
                try
                {
                    if (grdports.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Port list is empty !!!";
                        return;
                    }

                    List<string> selectedlist = get_selected_PortList();

                    if ((selectedlist == null) || (selectedlist.Count == 0))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select port/ports to delete !!!";
                        return;
                    }

                    DataTable dtTemp = new DataTable();
                    foreach (GridViewRow dgvr in grdports.Rows)
                    {
                        Int32 gridline = dgvr.RowIndex;
                        CheckBox chk = (CheckBox)dgvr.FindControl("chkRow");
                        //string CType = (dgvr.FindControl("mspr_frm_port") as Label).Text;

                        if (chk != null & chk.Checked)
                        {
                            dtTemp = ViewState["PortsTable"] as DataTable;
                            dtTemp.Rows[gridline].Delete();
                            ViewState["PortsTable"] = dtTemp;
                        }
                    }
                    dtTemp.AcceptChanges();
                    BindPortGrid();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }
        }

        protected void txtcurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                if (string.IsNullOrEmpty(txtcurrency.Text.Trim()))
                {
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Please Enter currency Code !!!";
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    txtcurrency.Focus();
                    return;
                }

                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(txtcurrency.Text.Trim(), null, null);

                DataView dv = new DataView(result);
                string filterword = "Code";
                dv.RowFilter = "" + filterword + " like '%" + txtcurrency.Text.Trim() + "%'";
                if (dv.Count > 0)
                {
                    result = dv.ToTable();
                }
                else
                {
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Invalid Currency code !!!";
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    txtcurrency.Focus();
                    return;
                }

                if (result.Rows.Count == 0)
                {
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Invalid Currency code !!!";
                    txtcurrency.Text = string.Empty;
                    txtcurrencyname.Text = string.Empty;
                    txtcurrency.Focus();
                    return;
                }

                txtcurrency.Text = result.Rows[0]["Code"].ToString();
                txtcurrencyname.Text = result.Rows[0]["Description"].ToString();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void txtportcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                if (string.IsNullOrEmpty(txtportcode.Text.Trim()))
                {
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Please Enter port Code !!!";
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    txtportcode.Focus();
                    return;
                }

                DataTable result = CHNLSVC.CommonSearch.LoadAllPorts(txtportcode.Text.Trim(), null, null);

                DataView dv = new DataView(result);
                string filterword = "Code";
                dv.RowFilter = "" + filterword + " like '%" + txtportcode.Text.Trim() + "%'";
                if (dv.Count > 0)
                {
                    result = dv.ToTable();
                }
                else
                {
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Invalid port code !!!";
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    txtportcode.Focus();
                    return;
                }

                if (result.Rows.Count == 0)
                {
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    divalert.Visible = true;
                    lblalert.Text = "Invalid port code !!!";
                    txtportcode.Text = string.Empty;
                    txtportname.Text = string.Empty;
                    txtportcode.Focus();
                    return;
                }

                txtportcode.Text = result.Rows[0]["Code"].ToString();
                txtportname.Text = result.Rows[0]["Name"].ToString();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdports_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdports.EditIndex = e.NewEditIndex;
                this.BindPortGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            try
            {
                grdports.EditIndex = -1;
                this.BindPortGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

                string leadtime = (row.Cells[3].Controls[0] as TextBox).Text;

                if (string.IsNullOrEmpty(leadtime))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter lead time !!!";
                    return;
                }

                if (!IsNumeric(leadtime, NumberStyles.Float))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for lead time !!!";
                    return;
                }

                DataTable dt = ViewState["PortsTable"] as DataTable;

                dt.Rows[row.RowIndex]["mspr_lead_time"] = leadtime;

                ViewState["PortsTable"] = dt;
                grdports.EditIndex = -1;
                this.BindPortGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void DeactivateSupPorts()
        {
            try
            {
                Int32 _effect = 0;
                _userid = (string)Session["UserID"];
                SupplierPort _sup_port = new SupplierPort();
                string suppliercode = string.Empty;

                suppliercode = txtsupcode.Text.Trim();

                _sup_port.MSPR_COM = Session["UserCompanyCode"].ToString();
                _sup_port.MSPR_CD = suppliercode;
                _sup_port.MSPR_ACT = 0;
                _sup_port.MSPR_MOD_BY = _userid;
                _sup_port.MSPR_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                _effect = CHNLSVC.General.DeactivateSupplierPorts(_sup_port);
            }
            catch (Exception ex)
            {
                divok.Visible = false;
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SaveSupplierPorts()
        {
            if (grdports.Rows.Count > 0)
            {
                try
                {
                    DeactivateSupPorts();

                    Int32 _effect = 0;
                    _userid = (string)Session["UserID"];
                    SupplierPort _supplier_port = new SupplierPort();
                    string suppliercode = string.Empty;

                    suppliercode = txtsupcode.Text.Trim();
                    

                    foreach (GridViewRow ddr in grdports.Rows)
                    {
                        _supplier_port.MSPR_COM = Session["UserCompanyCode"].ToString();
                        _supplier_port.MSPR_CD = suppliercode;
                        _supplier_port.MSPR_TP = "S";
                        _supplier_port.MSPR_FRM_PORT = ddr.Cells[1].Text;
                        _supplier_port.MSPR_TO_PORT = "CMB";
                        _supplier_port.MSPR_LEAD_TIME = Convert.ToInt32(ddr.Cells[3].Text);
                        _supplier_port.MSPR_ACT = 1;
                        _supplier_port.MSPR_CRE_BY = _userid;
                        _supplier_port.MSPR_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                        _supplier_port.MSPR_MOD_BY = _userid;
                        _supplier_port.MSPR_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                        _effect = CHNLSVC.General.UpdateSupplierPorts(_supplier_port);
                    }
                }
                catch (Exception ex)
                {
                    divok.Visible = false;
                    divalert.Visible = true;
                    lblalert.Text = ex.Message + " " + "Error saving supplier ports";
                }
            }
        }

        private void LoadSupplierPorts()
        {
            try
            {
                DataTable dtports = CHNLSVC.CommonSearch.LoadSupplierPorts(Session["UserCompanyCode"].ToString(), txtsupcode.Text.Trim());
                if (dtports.Rows.Count > 0)
                {
                    grdports.DataSource = null;
                    grdports.DataBind();

                    grdports.DataSource = dtports;
                    grdports.DataBind();
                }

                ViewState["PortsTable"] = null;
                this.BindPortGrid();

                ViewState["PortsTable"] = dtports;
                this.BindPortGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DivsHide();
            if (txtdelitem.Value == "Yes")
            {
                try
                {
                    if (grdallitems.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Item list is empty !!!";
                        return;
                    }

                    List<string> selectedlist = get_selected_ItemList();

                    if ((selectedlist == null) || (selectedlist.Count == 0))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select item/items to delete !!!";
                        return;
                    }

                    DataTable dtTemp = new DataTable();
                    foreach (GridViewRow dgvr in grdallitems.Rows)
                    {
                        Int32 gridline = dgvr.RowIndex;
                        CheckBox chk = (CheckBox)dgvr.FindControl("chkRow");
                        if (chk != null & chk.Checked)
                        {
                            dtTemp = ViewState["ItemsTable"] as DataTable;
                            dtTemp.Rows[gridline].Delete();
                            ViewState["ItemsTable"] = dtTemp;
                        }
                    }
                    dtTemp.AcceptChanges();
                    BindItemGrid();
                }
                catch (Exception ex)
                {
                    divalert.Visible = true;
                    lblalert.Text = ex.Message;
                }
            }

        }

        protected void lbtnadditem_Click(object sender, EventArgs e)
        {
            try
            {
                InsertToItemsGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void InsertToItemsGrid()
        {
            try
            {
                DivsHide();

                if (grdselecteditems.Rows.Count == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Search item list is empty !!!";
                    grdselecteditems.Focus();
                    return;
                }

                DataTable dt = (DataTable)ViewState["ItemsTable"];

                foreach (GridViewRow ddritem in grdselecteditems.Rows)
                {
                    CheckBox chk = (CheckBox)ddritem.FindControl("chkRow");
                    if (chk != null && chk.Checked)
                    {
                        dt.Rows.Add(ddritem.Cells[1].Text.ToString(), ddritem.Cells[2].Text.ToString());
                        ViewState["ItemsTable"] = dt;
                        uniqueColsItems = RemoveDuplicateRows(dt, "mbii_itm_cd");
                        grdallitems.DataSource = uniqueColsItems;
                        grdallitems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnfindall_Click(object sender, EventArgs e)
        {
            try
            {
                grdselecteditems.DataSource = null;
                grdselecteditems.DataBind();

                DataTable dt = CHNLSVC.Sales.GetInsuCriteria(Session["UserCompanyCode"].ToString(), "ITEM", txtitemcode.Text.Trim(), txtbrand.Text.Trim(), txtmodelno.Text.Trim(), txtmaincat.Text.Trim(), txtsubcat.Text.Trim(), null, null, null);
                grdselecteditems.DataSource = dt;
                grdselecteditems.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdallitems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void DeactivateSupItems()
        {
            try
            {
                Int32 _effect = 0;
                _userid = (string)Session["UserID"];
                BusEntityItem _sup_Itm = new BusEntityItem();
                string suppliercode = string.Empty;

                suppliercode = txtsupcode.Text.Trim();

                _sup_Itm.MBII_COM = Session["UserCompanyCode"].ToString();
                _sup_Itm.MBII_CD = suppliercode;
                _sup_Itm.MBII_ACT = 0;
                _sup_Itm.MBII_MOD_BY = _userid;
                _sup_Itm.MBII_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                _effect = CHNLSVC.General.DeactivateSupplierItems(_sup_Itm);
            }
            catch (Exception ex)
            {
                divok.Visible = false;
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SaveSupplierItems()
        {
            if (grdallitems.Rows.Count > 0)
            {
                try
                {
                    DeactivateSupItems();

                    Int32 _effect = 0;
                    _userid = (string)Session["UserID"];
                    BusEntityItem _sup_Itm = new BusEntityItem();
                    string suppliercode = string.Empty;
                    string defaultcurrency = (string)Session["DEFAULT_CURRENCY"];

                    
                    suppliercode = txtsupcode.Text.Trim();
                    

                    foreach (GridViewRow ddr in grdallitems.Rows)
                    {
                        _sup_Itm.MBII_COM = Session["UserCompanyCode"].ToString();
                        _sup_Itm.MBII_CD = suppliercode;
                        _sup_Itm.MBII_TP = "S";
                        _sup_Itm.MBII_ITM_CD = ddr.Cells[1].Text;
                        _sup_Itm.MBII_ACT = 1;
                        _sup_Itm.MBII_CRE_BY = _userid;
                        _sup_Itm.MBII_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                        _sup_Itm.MBII_MOD_BY = _userid;
                        _sup_Itm.MBII_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                        _effect = CHNLSVC.General.UpdateSupplierItems(_sup_Itm);
                    }
                }
                catch (Exception ex)
                {
                    divok.Visible = false;
                    divalert.Visible = true;
                    lblalert.Text = ex.Message + " " + "Error saving supplier items";
                }
            }
        }


        protected void grdallitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["ItemsTable"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["ItemsTable"] = dt;
                BindItemGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void grdallitems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdallitems.EditIndex = e.NewEditIndex;
                this.BindItemGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void OnUpdateItems(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

                string supitmcode = (row.Cells[3].Controls[0] as TextBox).Text;
                string price = (row.Cells[4].Controls[0] as TextBox).Text;

                if (string.IsNullOrEmpty(supitmcode))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter sup item code !!!";
                    return;
                }

                if (string.IsNullOrEmpty(price))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter price !!!";
                    return;
                }

                if (!IsNumeric(price, NumberStyles.Float))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid number for price !!!";
                    return;
                }

                DataTable dt = ViewState["ItemsTable"] as DataTable;

                dt.Rows[row.RowIndex]["msi_sup_itm_code"] = supitmcode;
                dt.Rows[row.RowIndex]["msi_price_quoted"] = price;

                ViewState["ItemsTable"] = dt;
                grdallitems.EditIndex = -1;
                this.BindItemGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void OnCancelItems(object sender, EventArgs e)
        {
            try
            {
                grdallitems.EditIndex = -1;
                this.BindItemGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadSupplierItems()
        {
            try
            {
                DataTable dtitems = CHNLSVC.CommonSearch.LoadSupplierItems(Session["UserCompanyCode"].ToString(), txtsupcode.Text.Trim());
                if (dtitems.Rows.Count > 0)
                {
                    grdallitems.DataSource = null;
                    grdallitems.DataBind();

                    grdallitems.DataSource = dtitems;
                    grdallitems.DataBind();
                }

                ViewState["ItemsTable"] = null;
                this.BindItemGrid();

                ViewState["ItemsTable"] = dtitems;
                this.BindItemGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtntaxcat_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tax);
                DataTable result = CHNLSVC.CommonSearch.LoadAllTaxCat(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "416";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SaveSupplierTaxCodes()
        {
            try
            {
                Int32 _effect = 0;
                _userid = (string)Session["UserID"];
                BusEntityTax _tax = new BusEntityTax();
                string suppliercode = string.Empty;

              
                suppliercode = txtsupcode.Text.Trim();
                

                _tax.MBIT_COM = Session["UserCompanyCode"].ToString();
                _tax.MBIT_CD = suppliercode;
                _tax.MBIT_TP = "S";
                _tax.MBIT_TAX_CD = txttaxcode.Text.Trim();
                _tax.MBIT_TAX_RT_CD = txttaxratecd.Text.Trim();
                _tax.MBIT_TAX_RT = Convert.ToDecimal(txttaxrate.Text.Trim());
                _tax.MBIT_ACT = 1;
                _tax.MBIT_EFFCT_DT = DateTime.Now;
                _tax.MBIT_DIV_RT = Convert.ToDecimal(txttaxdivrate.Text.Trim());
                _tax.MBIT_CRE_BY = _userid;
                _tax.MBIT_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                _tax.MBIT_MOD_BY = _userid;
                _tax.MBIT_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                _effect = CHNLSVC.General.UpdateSupplierTax(_tax);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }


        private void LoadSupplierTaxCodes()
        {
            try
            {
                DataTable dttaxcodes = CHNLSVC.CommonSearch.LoadSupplierTaxCodes(Session["UserCompanyCode"].ToString(), txtsupcode.Text.Trim());
                if (dttaxcodes.Rows.Count > 0)
                {
                    foreach (DataRow item in dttaxcodes.Rows)
                    {
                        txttaxcode.Text = item[0].ToString();
                        txttaxratecd.Text = item[1].ToString();
                        txttaxrate.Text = item[2].ToString();
                        txttaxdivrate.Text = item[3].ToString();
                        chksptax.Checked = true;
                        txttaxcode.Enabled = true;
                        txttaxratecd.Enabled = true;
                        txttaxrate.Enabled = true;
                        txttaxdivrate.Enabled = true;
                        lbtncoderate.Visible = true;
                    }
                }
                else
                {
                    chksptax.Checked = false;
                    txttaxcode.Enabled = false;
                    txttaxratecd.Enabled = false;
                    txttaxrate.Enabled = false;
                    txttaxdivrate.Enabled = false;
                    lbtncoderate.Visible = false;

                    txttaxcode.Text = string.Empty;
                    txttaxratecd.Text = string.Empty;
                    txttaxrate.Text = string.Empty;
                    txttaxdivrate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtncoderate_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
                DataTable result = CHNLSVC.CommonSearch.LoadAllTaxCodes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "417";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private DataTable CheckSupGroup()
        {
            DataTable dtsupgroup = new DataTable();
            try
            {
                dtsupgroup = CHNLSVC.CommonSearch.GetSupplierProfileByGrup(txtsupcode.Text.Trim(),"","","","","");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            return dtsupgroup;
        }

        private DataTable CheckSup()
        {
            DataTable dtsup = new DataTable();
            try
            {
                dtsup = CHNLSVC.Inventory.GetSupplier(Session["UserCompanyCode"].ToString(), txtsupcode.Text.Trim());
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            return dtsup;
        }

        protected void txtsupcode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt_group = new DataTable();
            DataTable dt_sup = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(txtsupcode.Text.Trim()))
                {
                    return;
                }

                dt_group = CheckSupGroup();
                dt_sup = CheckSup();

                if ((dt_group.Rows.Count > 0) && (dt_sup.Rows.Count > 0))
                {
                    foreach (DataRow item in dt_sup.Rows)
                    {
                        txtsupname.Text = item[5].ToString();

                        string isactive = item[18].ToString();
                        if (isactive == "1")
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }

                        txttin.Text = item[33].ToString();
                        txttaxrg.Text = item[21].ToString();
                        txtadd1.Text = item[6].ToString();
                        txtadd2.Text = item[7].ToString();
                        txtcontactperson.Text = item[17].ToString();
                        txttel.Text = item[12].ToString();
                        txtfax.Text = item[13].ToString();
                        txtemail.Text = item[16].ToString();
                        txtweb.Text = item[76].ToString();
                        txtcountry.Text = item[8].ToString();
                        txtdelcurr.Text = item[75].ToString();
                        txtcreditperiod.Text = item[77].ToString();
                        txtglacc.Text = item[4].ToString();

                        string suppliertype = item[3].ToString();
                        ddlsuptype.SelectedValue = suppliertype;

                        txttaxcat.Text = item[31].ToString();

                        string issptax = item[37].ToString();
                        if (issptax == "1")
                        {
                            chksptax.Checked = true;
                        }
                        else
                        {
                            chksptax.Checked = false;
                        }
                    }
                    
                    LoadSupplierData();
                    return;
                }
                else if ((dt_group.Rows.Count > 0) && (dt_sup.Rows.Count == 0))
                {
                    foreach (DataRow item in dt_group.Rows)
                    {
                        txtsupname.Text = item[1].ToString();

                        string isactive = item[23].ToString();
                        if (isactive == "1")
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }

                        //txttin.Text = item[33].ToString();
                        //txttaxrg.Text = item[21].ToString();
                        txtadd1.Text = item[7].ToString();
                        txtadd2.Text = item[8].ToString();
                        txtcontactperson.Text = item[22].ToString();
                        txttel.Text = item[13].ToString();
                        txtfax.Text = item[14].ToString();
                        txtemail.Text = item[21].ToString();
                        //txtweb.Text = item[76].ToString();
                        txtcountry.Text = item[12].ToString();
                        //txtdelcurr.Text = item[75].ToString();
                        //txtcreditperiod.Text = item[77].ToString();
                        //txtglacc.Text = item[4].ToString();

                        //string suppliertype = item[3].ToString();
                        //ddlsuptype.SelectedValue = suppliertype;

                        //txttaxcat.Text = item[31].ToString();

                        //string issptax = item[37].ToString();
                        //if (issptax == "1")
                        //{
                        //    chksptax.Checked = true;
                        //}
                        //else
                        //{
                        //    chksptax.Checked = false;
                        //}
                    }

                    LoadSupplierData();
                    return;
                }
                else if ((dt_group.Rows.Count == 0) && (dt_sup.Rows.Count > 0))
                {
                    foreach (DataRow item in dt_sup.Rows)
                    {
                        txtsupname.Text = item[5].ToString();

                        string isactive = item[18].ToString();
                        if (isactive == "1")
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }

                        txttin.Text = item[33].ToString();
                        txttaxrg.Text = item[21].ToString();
                        txtadd1.Text = item[6].ToString();
                        txtadd2.Text = item[7].ToString();
                        txtcontactperson.Text = item[17].ToString();
                        txttel.Text = item[12].ToString();
                        txtfax.Text = item[13].ToString();
                        txtemail.Text = item[16].ToString();
                        txtweb.Text = item[76].ToString();
                        txtcountry.Text = item[8].ToString();
                        txtdelcurr.Text = item[75].ToString();
                        txtcreditperiod.Text = item[77].ToString();
                        txtglacc.Text = item[4].ToString();

                        string suppliertype = item[3].ToString();
                        ddlsuptype.SelectedValue = suppliertype;

                        txttaxcat.Text = item[31].ToString();

                        string issptax = item[37].ToString();
                        if (issptax == "1")
                        {
                            chksptax.Checked = true;
                        }
                        else
                        {
                            chksptax.Checked = false;
                        }
                    }
                    LoadSupplierData();
                    return;
                }
                else if ((dt_group.Rows.Count == 0) && (dt_sup.Rows.Count == 0))
                {
                    Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadCurrencyText()
        {
            try
            {
                DataTable dtcurr = CHNLSVC.CommonSearch.LoadCurrencyText(txtdelcurr.Text.Trim());

                foreach (DataRow item in dtcurr.Rows)
                {
                    txtdelcurr2.Text = item[0].ToString();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadCountryText()
        {
            try
            {
                DataTable dtcurr = CHNLSVC.CommonSearch.LoadCountryText(txtcountry.Text.Trim());

                foreach (DataRow item in dtcurr.Rows)
                {
                    txtcountry2.Text = item[0].ToString();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtndelcurr_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopupSupplier.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

    }
}