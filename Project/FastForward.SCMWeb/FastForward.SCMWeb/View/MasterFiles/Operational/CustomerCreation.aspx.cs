using AjaxControlToolkit;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.CustService;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Services;
using System;
using System.Collections;
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
    public partial class CustomerCreation : Base
    {
        public class BusinessEntityTYPE
        {
            //this object stores all types
            private string typeCD_;
            public string TypeCD_
            {
                get { return typeCD_; }
                set { typeCD_ = value; }
            }

            private string typeDesctipt;

            public string TypeDesctipt
            {
                get { return typeDesctipt; }
                set { typeDesctipt = value; }
            }

            private string isMandatory;

            public string IsMandatory
            {
                get { return isMandatory; }
                set { isMandatory = value; }
            }
        }

        //MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        public List<MasterBusinessEntitySalesType> _salesTypes { get { return (List<MasterBusinessEntitySalesType>)ViewState["_salesTypes"]; } set { ViewState["_salesTypes"] = value; } }
        //List<BusEntityTypes> _cusSegmentation = new List<BusEntityTypes>();        
        protected List<MasterInvoiceType> SalesType { get { return (List<MasterInvoiceType>)ViewState["SalesType"]; } set { ViewState["SalesType"] = value; } }
        // List<MasterInvoiceType> SalesType = new List<MasterInvoiceType>();
        protected List<MasterBusinessOfficeEntry> _MstBusOffEntry { get { return (List<MasterBusinessOfficeEntry>)ViewState["_MstBusOffEntry"]; } set { ViewState["_MstBusOffEntry"] = value; } }


        //private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        //private CustomerAccountRef _account = new CustomerAccountRef();
        //private List<BusEntityItem> busItemList = new List<BusEntityItem>();
        //protected List<MasterBusinessEntityInfo> _busInfoList { get { return (List<MasterBusinessEntityInfo>)ViewState["_busInfoList"]; } set { ViewState["_busInfoList"] = value; } }

        public MasterBusinessEntity _custProfile { get { return (MasterBusinessEntity)ViewState["_custProfile"]; } set { ViewState["_custProfile"] = value; } }
        public GroupBussinessEntity _custGroup { get { return (GroupBussinessEntity)ViewState["_custGroup"]; } set { ViewState["_custGroup"] = value; } }
        public CustomerAccountRef _account { get { return (CustomerAccountRef)ViewState["_account"]; } set { ViewState["_account"] = value; } }
        public List<BusEntityItem> busItemList { get { return (List<BusEntityItem>)ViewState["busItemList"]; } set { ViewState["busItemList"] = value; } }
        public List<MasterBusinessEntityInfo> _busInfoList { get { return (List<MasterBusinessEntityInfo>)ViewState["_busInfoList"]; } set { ViewState["_busInfoList"] = value; } }

        private Boolean _isExsit = false;
        private Boolean _isGroup = false;
        private Boolean _isUpdate = false;
        private Int32 _eff = 0;
        private string _err = string.Empty;
        Base _basePage;
        public Boolean _isFromOther = false;
        public TextBox obj_TragetTextBox;
        private DataTable _tbl = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["_isExtentedPage"] = true;
            Session["confmbox"] = "";
            Session["SUNLOOKUP"] = null;
          
            if (!IsPostBack)
            {
                grdSalesTypes.DataSource = new int[] { };
                grdSalesTypes.DataBind();

                grdCustomerSegmentation.DataSource = new int[] { };
                grdCustomerSegmentation.DataBind();

                grdcustomerOfficeEntry.DataSource = new int[] { };
                grdcustomerOfficeEntry.DataBind();

                LoadDefaultValues();
                Session["mst_busentity_add_cus"] = null;
            }
        }

        private void LoadDefaultValues()
        {
            //Sales types default values    

            //Session["viewbutton"] = false;
            ucCustomer.VisbleButttons();
            //ucCustomer._isExtendedPage = true;

            DataTable _dtSalesTypes = CHNLSVC.General.GetSalesTypes("", null, null);

            if (_dtSalesTypes != null)
            {
                DataView dv = _dtSalesTypes.DefaultView;
                dv.Sort = "srtp_desc";
                DataTable sortedDT = dv.ToTable();
                var _def1 = _dtSalesTypes.AsEnumerable().Select(x => x.Field<string>("srtp_cd")).ToList();
                //  string _def01 = Convert.ToString(_def1[0]);

                ddlSalesTypes.DataSource = sortedDT;
                ddlSalesTypes.DataTextField = "srtp_cd";// "srtp_desc";
                ddlSalesTypes.DataValueField = "srtp_cd";
                ddlSalesTypes.DataBind();
                ddlSalesTypes.SelectedItem.Text = "Select";
            }

            //Customer segmentation drop down default

            ddlShowroomStus.Items.Clear();
            List<ListItem> shwroomsstatus = new List<ListItem>();
            shwroomsstatus.Add(new ListItem("GOOD", "GOOD"));
            shwroomsstatus.Add(new ListItem("FAIR", "FAIR"));
            shwroomsstatus.Add(new ListItem("BLACK_LIST", "BLACK_LIST"));
            ddlShowroomStus.Items.AddRange(shwroomsstatus.ToArray());
            Session["SUN"] = "";
            ddlHeadOffStus.Items.Clear();
            List<ListItem> headofficestatus = new List<ListItem>();
            headofficestatus.Add(new ListItem("GOOD", "GOOD"));
            headofficestatus.Add(new ListItem("FAIR", "FAIR"));
            headofficestatus.Add(new ListItem("BLACK_LIST", "BLACK_LIST"));
            ddlHeadOffStus.Items.AddRange(headofficestatus.ToArray());

            //load sales types grid

            //Get Customers Sale tpye list 
            /*                     
           DataTable SalesType = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), "");
           if (_table != null && _table.Rows.Count > 0)
           {
               var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
               if (_isAvailable == null || _isAvailable.Count <= 0)
               {                   
                   DisplayMessage("Customer is not allow for enter transaction under selected invoice type",2);                   
                   return;
               }

               foreach (var srt in _table.Rows)
               {
                   var sf = _table.(x => x.Field<string>("mbsa_sa_tp")).ToList();                     
               }
           }              
            
           */

            //Get Sales Type and Description by Customer Code

            /*
            
            DataTable salestypedatasource = CHNLSVC.General.GetSalesTypes("", null, null);
            ComboBoxDraw(datasource, cmbSalesTp, "srtp_cd", "srtp_desc");
            */

            //load customer segmentation grid : prt 1

            DataTable dt = new DataTable();
            dt = CHNLSVC.Sales.GetBusinessEntityTypes("C");
            List<BusinessEntityTYPE> bindtypeList = new List<BusinessEntityTYPE>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    // Get the value of the wanted column and cast it to string 
                    string TP = Convert.ToString(r["RBT_TP"]);
                    string DESC = Convert.ToString(r["RBT_DESC"]); //rbv_val
                    Boolean isMandetory = Convert.ToBoolean(r["RBT_MAD"]);
                    BusinessEntityTYPE bizTP = new BusinessEntityTYPE();
                    if (isMandetory)
                        bizTP.IsMandatory = "True";
                    else
                        bizTP.IsMandatory = "False";
                    bizTP.TypeCD_ = TP;
                    bizTP.TypeDesctipt = DESC;
                    bindtypeList.Add(bizTP);

                }
            }

            grdCustomerSegmentation.DataSource = null;
            grdCustomerSegmentation.DataSource = new List<BusinessEntityTYPE>();
            grdCustomerSegmentation.DataSource = bindtypeList;
            grdCustomerSegmentation.DataBind();

            //getting Values of Customer segmentation by Type : prt 2
            //      List<MasterBusinessEntityInfo> _segmentation = _basePage.CHNLSVC.Sales.GetCustomerSegmentation(ucCustomer.);
            foreach (DataRow row in dt.Rows)
            {
                string typeName = row["RBT_TP"].ToString();

                DataTable dtVal = new DataTable();
                dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("C", typeName);

                if ((dtVal != null) && (dtVal.Rows.Count > 0))
                {
                    foreach (GridViewRow rows in grdCustomerSegmentation.Rows)
                    {
                        Label sd = (Label)rows.FindControl("rbt_tp");
                        CheckBox ch = (CheckBox)rows.FindControl("_Mandatory");
                        if (sd.Text == typeName)
                        {

                            DropDownList ddlAgeVals = (DropDownList)rows.FindControl("ddlAgeVals");
                            ch.Checked = false;
                            ddlAgeVals.DataSource = dtVal;
                            ddlAgeVals.DataTextField = "RBV_VAL";
                            ddlAgeVals.DataValueField = "RBV_VAL";
                            ddlAgeVals.DataBind();
                            ddlAgeVals.Items.Insert(0, "--Select--");

                        }

                    }
                }

            }


            //Office of Entry Default values

            string toCountry = "";
            MasterCompany COM = CHNLSVC.General.GetCompByCode(Convert.ToString(Session["UserCompanyCode"]));
            if (COM != null)
            {
                toCountry = COM.Mc_anal19;
            }

            DataTable _dtOffEntry = CHNLSVC.Financial.Get_OFFICE_ENTRY(toCountry, null, null);
            DataView dv1 = _dtOffEntry.DefaultView;
            dv1.Sort = "RCUOE_CD";
            DataTable sortedDT1 = dv1.ToTable();
            if (_dtOffEntry != null && _dtOffEntry.Rows.Count > 0)
            {
                //var _def1 = _dtOffEntry.AsEnumerable().Where(x => x.Field<Int16>("RCUOE_DEF") == 1).Select(x => x.Field<string>("RCUOE_CD")).ToList();
               // var _def1 = _dtOffEntry.AsEnumerable().Select(x => x.Field<string>("RCUOE_CD")).ToList();
                //string _def01 = Convert.ToString(_def1[0]);

                ddlOfficeofEntry.DataSource = sortedDT1;
                //  ddlOfficeofEntry.DataTextField = "RCUOE_DESC";
                ddlOfficeofEntry.DataTextField = "RCUOE_CD";
                ddlOfficeofEntry.DataValueField = "RCUOE_CD";
                ddlOfficeofEntry.DataBind();
               // ddlOfficeofEntry.SelectedItem.Text = "Select";
            }

            ddlDirection.Items.Clear();
            List<ListItem> officeentrystatus = new List<ListItem>();
            officeentrystatus.Add(new ListItem("Office In", "Office In"));
            officeentrystatus.Add(new ListItem("Office Out", "Office Out"));
            ddlDirection.Items.AddRange(officeentrystatus.ToArray());

            SalesType = new List<MasterInvoiceType>();


            SalesType = ViewState["SalesType"] as List<MasterInvoiceType>;
        }

        //Load Customer details once click search button
        protected void LoadCustProfExtnd()
        {

            // ucCustomer.LoadCustProf(ucCustomer._custProfile, ucCustomer._isExtendedPage);

            lbtnAddNewCus.Visible = false;
        }


        /*
        Save and Update Employee Details 
        */
        protected void lbtnAddNewCus_Click(object sender, EventArgs e)
        {
            #region Special character validation
            if (!validateinputString(txtTINNo.Text))
            {
                DisplayMessage("Invalid charactor found in tin no.", 2);
                txtTINNo.Focus();
                return;
            }
            if (!validateinputString(txtddlPrVal1.Text))
            {
                DisplayMessage("Invalid charactor found in print value no 1.", 2);
                txtddlPrVal1.Focus();
                return;
            }
            if (!validateinputString(txtddlPrVal2.Text))
            {
                DisplayMessage("Invalid charactor found in print value no 2.", 2);
                txtddlPrVal2.Focus();
                return;
            }
            if (!validateinputString(txtProceCode.Text))
            {
                DisplayMessage("Invalid charactor found in procedure code.", 2);
                txtProceCode.Focus();
                return;
            }
            if (!validateinputString(txtWareHseCde.Text))
            {
                DisplayMessage("Invalid charactor found in warehouse code.", 2);
                txtWareHseCde.Focus();
                return;
            }
            if (!validateinputString(txtAccCde.Text))
            {
                DisplayMessage("Invalid charactor found in account code.", 2);
                txtAccCde.Focus();
                return;
            }
            if (!validateinputString(txtexecutive.Text))
            {
                DisplayMessage("Invalid charactor found in executive code.", 2);
                txtexecutive.Focus();
                return;
            }
            if (!validateinputString(txtOfficeName.Text))
            {
                DisplayMessage("Invalid charactor found in office name.", 2);
                txtOfficeName.Focus();
                return;
            }
            #endregion
            try
            {
                _basePage = new Base();
                string _cusCode = "";
                Int32 _effect = 0;
                //  ucCustomer.MobileNoVerification();
                int i = 0;
                foreach (GridViewRow gvr in this.grdCustomerSegmentation.Rows)
                {
                    CheckBox check = (CheckBox)gvr.FindControl("_Mandatory");
                    DropDownList sd = (DropDownList)gvr.FindControl("ddlAgeVals");
                    if (check.Checked)
                    {
                        string x = sd.SelectedValue;
                        if (sd.SelectedValue == "--Select--")
                        {
                            DisplayMessage("Please Select Value In Customer segmentation ", 2);
                            return;
                        }

                        i++;
                    }
                }
                if (i == 0)
                {
                    //DisplayMessage("Please Check Customer segmentation ", 2);
                    //return;
                }
                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }

                if (txtSaveconformmessageValue.Value == "Yes")
                {

                    if (ucCustomer.ValidateSave())
                    {
                        ucCustomer.Collect_Cust();
                        ucCustomer.Collect_GroupCust();

                        #region Special character validation From user controler
                        if (!validateinputString(ucCustomer._custProfile.Mbe_br_no))
                        {
                            DisplayMessage("Invalid charactor found in br no.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_pp_no))
                        {
                            DisplayMessage("Invalid charactor found in passport no.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_dl_no))
                        {
                            DisplayMessage("Invalid charactor found in dl no.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.MBE_INI))
                        {
                            DisplayMessage("Invalid charactor found in initials.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.MBE_FNAME))
                        {
                            DisplayMessage("Invalid charactor found in first name.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.MBE_SNAME))
                        {
                            DisplayMessage("Invalid charactor found in surname.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_name))
                        {
                            DisplayMessage("Invalid charactor found in name in full.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_add1))
                        {
                            DisplayMessage("Invalid charactor found in permanent address line 1.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_add2))
                        {
                            DisplayMessage("Invalid charactor found in permanent address line 2.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_town_cd))
                        {
                            DisplayMessage("Invalid charactor found in town code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_distric_cd))
                        {
                            DisplayMessage("Invalid charactor found in district code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_postal_cd))
                        {
                            DisplayMessage("Invalid charactor found in postal code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_province_cd))
                        {
                            DisplayMessage("Invalid charactor found in province code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_country_cd))
                        {
                            DisplayMessage("Invalid charactor found in country code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_tel))
                        {
                            DisplayMessage("Invalid charactor found in phone no.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_cr_add1))
                        {
                            DisplayMessage("Invalid charactor found in present address line 1.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_cr_add2))
                        {
                            DisplayMessage("Invalid charactor found in present address line 2.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_town_cd))
                        {
                            DisplayMessage("Invalid charactor found in present town code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_distric_cd))
                        {
                            DisplayMessage("Invalid charactor found in present district code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_postal_cd))
                        {
                            DisplayMessage("Invalid charactor found in present postal code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_province_cd))
                        {
                            DisplayMessage("Invalid charactor found in present province code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_country_cd))
                        {
                            DisplayMessage("Invalid charactor found in present country code.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_cr_tel))
                        {
                            DisplayMessage("Invalid charactor found in present phone no.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_com_name))
                        {
                            DisplayMessage("Invalid charactor found in working com name.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_add1))
                        {
                            DisplayMessage("Invalid charactor found in working com address line 1.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_add2))
                        {
                            DisplayMessage("Invalid charactor found in working com address line 1.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_dept))
                        {
                            DisplayMessage("Invalid charactor found in working dipartment name.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_wr_tel))
                        {
                            DisplayMessage("Invalid charactor found in working com phone.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_dept))
                        {
                            DisplayMessage("Invalid charactor found in working dipartment name.", 2);
                            return;
                        }
                        if (!validateinputStringWithSpace(ucCustomer._custProfile.Mbe_wr_designation))
                        {
                            DisplayMessage("Invalid charactor found in working com designation.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_wr_fax))
                        {
                            DisplayMessage("Invalid charactor found in working com fax no.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_tax_no))
                        {
                            DisplayMessage("Invalid charactor found in tax no.", 2);
                            return;
                        }
                        if (!validateinputString(ucCustomer._custProfile.Mbe_svat_no))
                        {
                            DisplayMessage("Invalid charactor found in svat no.", 2);
                            return;
                        }
                        #endregion

                        //Get Account Values
                        if (_account == null)
                        {
                            _account = new CustomerAccountRef();
                        }

                        _account.Saca_com_cd = Session["UserCompanyCode"].ToString();
                        try
                        {
                            if (txtCredLimit.Text.Trim() != string.Empty)
                            {
                                _account.Saca_crdt_lmt = Convert.ToDecimal(txtCredLimit.Text.Trim());
                                ucCustomer._custProfile.Mbe_sub_tp = "D";
                            }
                            else
                            {
                                _account.Saca_crdt_lmt = 0;
                            }
                        }
                        catch (Exception)
                        {
                            DisplayMessage("Invalid credit limit", 2);
                            return;
                        }

                        _account.Saca_cre_by = Session["UserID"].ToString();
                        _account.Saca_cre_when = DateTime.Now;
                        _account.Saca_cust_cd = ucCustomer._custProfile.Mbe_cd;
                        _account.Saca_mod_by = Session["UserID"].ToString();
                        _account.Saca_mod_when = DateTime.Now;
                        _account.Saca_ord_bal = 0;
                        _account.Saca_session_id = Session["SessionID"].ToString();

                        //Get Sales Types Values
                        if (_salesTypes == null)
                        {
                            _salesTypes = new List<MasterBusinessEntitySalesType>();
                        }
                        // List<MasterBusinessEntitySalesType> _salesTypes = new List<MasterBusinessEntitySalesType>();

                        foreach (MasterInvoiceType sal in SalesType)
                        {
                            MasterBusinessEntitySalesType _type = new MasterBusinessEntitySalesType();
                            _type.Mbsa_com = Session["UserCompanyCode"].ToString();
                            _type.Mbsa_cd = ucCustomer.Code;
                            _type.Mbsa_tp = "C";
                            _type.Mbsa_sa_tp = sal.Srtp_cd;
                            _type.Mbsa_act = true;
                            _type.Mbsa_cre_by = Session["UserID"].ToString();
                            _type.Mbsa_cre_dt = DateTime.Now;
                            _type.Mbsa_mod_by = Session["UserID"].ToString();
                            _type.Mbsa_mod_dt = DateTime.Now;
                            _type.Mbsa_valid_frm_dt = sal.Srtp_valid_from_dt; //Addd By Dulaj 2018/Dec/02
                            _type.Mbsa_valid_to_dt = sal.Srtp_valid_to_dt;
                            _salesTypes.Add(_type);
                        }
                        ViewState["_salesTypes"] = _salesTypes;

                        if (_busInfoList == null)
                        {
                            _busInfoList = new List<MasterBusinessEntityInfo>();
                        }
                        _busInfoList = new List<MasterBusinessEntityInfo>();
                        //Get Customer Segmentation Values
                        foreach (GridViewRow gvr in this.grdCustomerSegmentation.Rows)
                        {
                            MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();


                            CheckBox chk = (CheckBox)gvr.FindControl("_Mandatory");
                            if (chk.Checked)
                            {

                                bisInfo.Mbei_cd = ucCustomer.Code;
                                bisInfo.Mbei_com = Session["UserCompanyCode"].ToString();
                                bisInfo.Mbei_available = 1;
                                Label TypeName = (Label)gvr.FindControl("rbt_tp");
                                DropDownList TypeValue = (DropDownList)gvr.FindControl("ddlAgeVals");
                                string value = TypeValue.SelectedValue.ToString();
                                string type = TypeName.Text;
                                // string value = TypeValue.Text;
                                bisInfo.Mbei_tp = type;
                                if (value != null)
                                    bisInfo.Mbei_val = value;
                                else
                                    bisInfo.Mbei_val = string.Empty;
                                if (!string.IsNullOrEmpty(bisInfo.Mbei_val))
                                {
                                    _busInfoList.Add(bisInfo);
                                }
                            }
                            else
                            {
                                bisInfo.Mbei_cd = ucCustomer.Code;
                                bisInfo.Mbei_com = Session["UserCompanyCode"].ToString();
                                bisInfo.Mbei_available = 0;
                                Label TypeName = (Label)gvr.FindControl("rbt_tp");
                                DropDownList TypeValue = (DropDownList)gvr.FindControl("ddlAgeVals");
                                string value = TypeValue.SelectedValue.ToString();
                                string type = TypeName.Text;
                                // string value = TypeValue.Text;
                                bisInfo.Mbei_tp = type;
                                if (value != null)
                                    bisInfo.Mbei_val = value;
                                else
                                    bisInfo.Mbei_val = string.Empty;
                                if (!string.IsNullOrEmpty(bisInfo.Mbei_val))
                                {
                                    _busInfoList.Add(bisInfo);
                                }
                            }

                        }

                        ViewState["_busInfoList"] = _busInfoList;

                        //Get Additional Details
                        ucCustomer._custProfile.Mbe_oth_id_no = txtTINNo.Text;
                        ucCustomer._custProfile.Mbe_proc_cd = txtProceCode.Text;
                        ucCustomer._custProfile.Mbe_proc_val1 = txtddlPrVal1.Text;
                        ucCustomer._custProfile.Mbe_proc_val2 = txtddlPrVal2.Text;
                        ucCustomer._custProfile.Mbe_proc_cd = txtProceCode.Text;
                        ucCustomer._custProfile.Mbe_wh_cd = txtWareHseCde.Text;
                        ucCustomer._custProfile.Mbe_acc_cd = txtAccCde.Text;
                        ucCustomer._custProfile.Mbe_ho_stus = ddlHeadOffStus.SelectedValue;
                        ucCustomer._custProfile.Mbe_pc_stus = ddlShowroomStus.SelectedValue;
                        ucCustomer._custProfile.mbe_ref_no = txtexecutive.Text.ToString();
                        ucCustomer._custProfile.Mbe_acc_no = txtaccountNumber.Text.ToString();
                        if (radactive.Checked)
                        {
                            ucCustomer._custProfile.Mbe_act = true;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_act = false;
                        }
                        if (radsuspent.Checked)
                        {
                            ucCustomer._custProfile.Mbe_is_suspend = true;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_is_suspend = false;
                        }

                        if (chkfoc.Checked)
                        {
                            ucCustomer._custProfile.Mbe_foc = 1;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_foc = 0;
                        }

                        //if (chkfoc.Checked)
                        //{
                        //    ucCustomer._custProfile.Mbe_is_suspend = true;
                        //}

                        if (chkInsuMan.Checked)
                        {
                            ucCustomer._custProfile.Mbe_ins_man = 1;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_ins_man = 0;
                        }
                        if (txtMinDwnPymnt.Text == "" || Convert.ToDecimal(txtMinDwnPymnt.Text) > 100)
                        {
                            ucCustomer._custProfile.Mbe_min_dp_per = 0;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_min_dp_per = Convert.ToDecimal(txtMinDwnPymnt.Text);
                        }



                        if (chkDCN.Checked)
                        {
                            ucCustomer._custProfile.Mbe_alw_dcn = 1;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_alw_dcn = 0;
                        }



                        // mbe_qno_gen_seq

                        bool isChecked = optCompwise.Checked;
                        if (isChecked)
                        {
                            ucCustomer._custProfile.Mbe_qno_gen_seq = 1;
                        }
                        else
                        {
                            ucCustomer._custProfile.Mbe_qno_gen_seq = 0;
                        }

                        if (txtCredLimit.Text == "") txtCredLimit.Text = "0";




                        if (busItemList == null)
                        {
                            busItemList = new List<BusEntityItem>();
                        }

                        if (ucCustomer._MstBusOffEntry != null)
                        {
                            _MstBusOffEntry = ucCustomer._MstBusOffEntry;
                        }

                        //Get Office of Entry Values
                        if (_MstBusOffEntry == null)
                        {
                            _MstBusOffEntry = new List<MasterBusinessOfficeEntry>();
                        }

                        _MstBusOffEntry = ViewState["_MstBusOffEntry"] as List<MasterBusinessOfficeEntry>;

                        if (_MstBusOffEntry != null)
                        {
                            foreach (GridViewRow lgrv in grdcustomerOfficeEntry.Rows)
                            {
                                string _officecode = (lgrv.FindControl("lbl_ofoenCode") as Label).Text;
                                CheckBox officestatus = (CheckBox)lgrv.FindControl("chk_ofoenStatus");

                                var _chngviewstate_office = _MstBusOffEntry.Single(x => x._mbbo_off_cd == _officecode);
                                if (_chngviewstate_office != null)
                                {
                                    _chngviewstate_office._mbbo_act = (officestatus.Checked == true) ? 1 : 0;

                                }
                            }
                        }

                        CustomsProcedureCodes _cusProcCd = new CustomsProcedureCodes();
                        //CompanyMaster _mstCom=new 

                        _cusProcCd.Mph_cnty = "LK";
                        _cusProcCd.Mph_com = Session["UserCompanyCode"].ToString();
                        _cusProcCd.Mph_cogn_tp = "C";
                        _cusProcCd.Mph_cogn_cd = ucCustomer.TxtCusCode.Text.ToString();
                        _cusProcCd.Mph_doc_tp = "BOI";
                        _cusProcCd.Mph_proc_cd = txtProceCode.Text;
                        _cusProcCd.Mph_proc_desc = "Document & Service Charge"; //AS for the chamal said
                        _cusProcCd.Mph_decl_1 = "I";
                        _cusProcCd.Mph_decl_2 = "M";
                        _cusProcCd.Mph_decl_3 = "7";
                        _cusProcCd.Mph_print_1 = txtddlPrVal1.Text.ToString();
                        _cusProcCd.Mph_print_2 = txtddlPrVal2.Text.ToString();
                        _cusProcCd.Mph_act = 1;
                        _cusProcCd.Mph_cre_by = Session["UserID"].ToString();
                        _cusProcCd.Mph_cre_dt = DateTime.Now;
                        _cusProcCd.Mph_cre_session = Session["SessionID"].ToString();
                        _cusProcCd.Mph_mod_by = Session["UserID"].ToString();
                        _cusProcCd.Mph_mod_dt = DateTime.Now;
                        _cusProcCd.Mph_mod_session = Session["SessionID"].ToString();
                        _cusProcCd.Mph_ignore_duty = 0;
                        _cusProcCd.Mph_def = 0;

                        _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());

                        if (string.IsNullOrEmpty(txtProceCode.Text))
                        {
                            _cusProcCd = null;
                        }
                        MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(ucCustomer.TxtCusCode.Text.Trim().ToString(), null, null, null, null, Session["UserCompanyCode"].ToString());
                        if (custProf.Mbe_cd !=null)
                        {
                            _isExsit = true;
                        }
                        if (_isExsit == false)
                        {
                            if (ucCustomer.TxtCusCode.Text.ToString() != "" && Session["SUN"].ToString()=="true")
                            {
                                ucCustomer._custProfile.Mbe_cd = ucCustomer.TxtCusCode.Text.Trim().ToString();
                                ucCustomer._custProfile.Mbe_add1 = ucCustomer.TxtAddress1.Trim().ToString() ;
                                ucCustomer._custProfile.Mbe_add2 = ucCustomer.TxtAddress2.Trim().ToString();
                                ucCustomer._custProfile.Mbe_tp = "SUN";
                            }
                            _effect = _basePage.CHNLSVC.Sales.SaveBusinessEntityDetailWithGroupNew2(ucCustomer._custProfile, _account, _busInfoList, busItemList, out _cusCode, _salesTypes, _isExsit, _isGroup, ucCustomer._custGroup, false, _MstBusOffEntry, null, null, _cusProcCd);
                            Session["SUN"] = "";
                        }

                        else
                        {
                            _cusCode = ucCustomer.Code.Trim();
                            ucCustomer._custProfile.Mbe_cd = ucCustomer.TxtCusCode.Text.Trim().ToString();
                            ucCustomer._custProfile.Mbe_nic = ucCustomer.TxtNIC.Text.Trim().ToString();
                            ucCustomer._custProfile.Mbe_add2 = ucCustomer.TxtAddress2.Trim().ToString();
                            
                            //Added By Dulaj 2018/Jun/28  acount number
                            ucCustomer._custProfile.Mbe_acc_no = txtaccountNumber.Text;
                            _effect = _basePage.CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroupNeww(ucCustomer._custProfile, _account, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, Convert.ToDecimal(txtCredLimit.Text), _busInfoList, _salesTypes, busItemList, ucCustomer._custGroup, _MstBusOffEntry, _cusProcCd);

                        }

                        if (_effect == 1)
                        {
                            _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                            if (_isExsit == false)
                            {
                                string msg = "New customer created. Customer Code : " + _cusCode;
                                DisplayMessage(msg, 3);
                                ucCustomer.clearPage();
                                clearnewCusPage();
                                return;
                            }
                            else
                            {
                                string msg = "Exsiting customer " + _cusCode + " updated.";
                                DisplayMessage(msg, 3);
                                ucCustomer.clearPage();
                                clearnewCusPage();
                                return;
                            }
                        }

                        else
                        {
                            if (!string.IsNullOrEmpty(_cusCode))
                            {
                                string msg = "Customer " + _cusCode + " updated.";
                                DisplayMessage(msg, 3);
                                ucCustomer.clearPage();
                                clearnewCusPage();
                                return;
                            }

                            else
                            {
                                DisplayMessage("Creation Failed", 2);
                                return;
                            }
                        }

                    }
                }
            }

            catch (Exception err)
            {
                _basePage.CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 2);
            }

            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void clearnewCusPage()
        {
            txtaccountNumber.Text = string.Empty;
            txtddlPrVal1.Text = "";
            txtddlPrVal2.Text = "";
            txtCredLimit.Text = string.Empty;
            txtMinDwnPymnt.Text = string.Empty;
            txtTINNo.Text = string.Empty;
            txtexecutive.Text = string.Empty;
            txtProceCode.Text = string.Empty;
            txtWareHseCde.Text = string.Empty;
            txtAccCde.Text = string.Empty;
            //  grdCustomerSegmentation = null;
            //  grdSalesTypes = null;
            chkDCN.Checked = false;
            chkInsuMan.Checked = false;
            grdSalesTypes.DataSource = new int[] { };
            grdSalesTypes.DataBind();
            ddlShowroomStus.Items.Clear();
            List<ListItem> shwroomsstatus = new List<ListItem>();
            shwroomsstatus.Add(new ListItem("GOOD", "GOOD"));
            shwroomsstatus.Add(new ListItem("FAIR", "FAIR"));
            shwroomsstatus.Add(new ListItem("BLACK_LIST", "BLACK_LIST"));
            ddlShowroomStus.Items.AddRange(shwroomsstatus.ToArray());

            ddlHeadOffStus.Items.Clear();
            List<ListItem> headofficestatus = new List<ListItem>();
            headofficestatus.Add(new ListItem("GOOD", "GOOD"));
            headofficestatus.Add(new ListItem("FAIR", "FAIR"));
            headofficestatus.Add(new ListItem("BLACK_LIST", "BLACK_LIST"));
            ddlHeadOffStus.Items.AddRange(headofficestatus.ToArray());

            optPCwise.Checked = true;
            optPCwise.Checked = false;

            DataTable dt = new DataTable();
            dt = CHNLSVC.Sales.GetBusinessEntityTypes("C");
            foreach (DataRow row in dt.Rows)
            {
                string typeName = row["RBT_TP"].ToString();

                DataTable dtVal = new DataTable();
                dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("C", typeName);

                if ((dtVal != null) && (dtVal.Rows.Count > 0))
                {
                    foreach (GridViewRow rows in grdCustomerSegmentation.Rows)
                    {
                        Label sd = (Label)rows.FindControl("rbt_tp");
                        CheckBox ch = (CheckBox)rows.FindControl("_Mandatory");
                        if (sd.Text == typeName)
                        {

                            DropDownList ddlAgeVals = (DropDownList)rows.FindControl("ddlAgeVals");
                            ch.Checked = false;
                            ddlAgeVals.DataSource = dtVal;
                            ddlAgeVals.DataTextField = "RBV_VAL";
                            ddlAgeVals.DataValueField = "RBV_VAL";
                            ddlAgeVals.DataBind();
                            ddlAgeVals.Items.Insert(0, "--Select--");

                        }

                    }
                }
            }


        }
        //Clear Details

        protected void lbtnClearCus_Click(object sender, EventArgs e)
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
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        /*
         Sales Types 
        */
        protected void lbtnAddSalesType_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucCustomer.SalesType != null)
                {
                    SalesType = ucCustomer.SalesType;
                }

                if (SalesType == null)
                {
                    SalesType = new List<MasterInvoiceType>();
                }

                string code = (ddlSalesTypes.SelectedValue != null) ? ddlSalesTypes.SelectedValue.ToString() : "";
                DataTable _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", code);
                if (_dt.Rows.Count > 0)
                {
                    MasterInvoiceType _duplicate = SalesType.Find(x => x.Srtp_cd == _dt.Rows[0]["Srtp_cd"].ToString());

                    if (_duplicate == null)
                    {
                        MasterInvoiceType _invType = new MasterInvoiceType();
                        _invType.Srtp_cd = _dt.Rows[0]["Srtp_cd"].ToString();
                        _invType.Srtp_desc = _dt.Rows[0]["SRTP_DESC"].ToString();
                        if (!(string.IsNullOrEmpty(txtFrom.Text)))
                        {
                            _invType.Srtp_valid_from_dt = Convert.ToDateTime(txtFrom.Text);
                        }
                        if (!(string.IsNullOrEmpty(txtTo.Text)))
                        {
                            _invType.Srtp_valid_to_dt = Convert.ToDateTime(txtTo.Text);
                        }
                        SalesType.Add(_invType);

                        grdSalesTypes.DataSource = null;
                        grdSalesTypes.DataSource = new List<MasterInvoiceType>();
                        grdSalesTypes.DataSource = SalesType;
                        grdSalesTypes.DataBind();

                        ViewState["SalesType"] = SalesType;
                    }
                    else
                    {
                        DisplayMessage("This sales type is already exist.  ", 2);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n", 2);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void chkSalesTypes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSalesTypes.Checked)
                {
                    DataTable _dt = CHNLSVC.General.GetSalesTypes("", null, null);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        MasterInvoiceType _invoType = new MasterInvoiceType();
                        _invoType.Srtp_cd = dr["SRTP_CD"].ToString();
                        _invoType.Srtp_desc = dr["SRTP_DESC"].ToString();
                        SalesType.Add(_invoType);
                    }

                    // grdSalesTypes.DataSource = null;
                    grdSalesTypes.DataSource = new int[] { };
                    grdSalesTypes.DataBind();
                    grdSalesTypes.DataSource = new List<MasterInvoiceType>();
                    grdSalesTypes.DataSource = SalesType;
                    grdSalesTypes.DataBind();
                    ucCustomer.SalesType = SalesType;
                    ViewState["SalesType"] = SalesType;
                }
                else
                {
                    SalesType = new List<MasterInvoiceType>();
                    grdSalesTypes.DataSource = null;
                    grdSalesTypes.DataSource = new List<MasterInvoiceType>();
                    grdSalesTypes.DataSource = SalesType;
                    grdSalesTypes.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n", 2);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //Remove Sale Types one by one
        protected void lbtnsaletype_Remove_Click(object sender, EventArgs e)
        {


            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }

            if (ucCustomer.SalesType != null)
            {
                SalesType = ucCustomer.SalesType;
            }


            if (grdSalesTypes.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                string _saltypeCode = (row.FindControl("srtp_cd") as Label).Text;

                if (string.IsNullOrEmpty(_saltypeCode))
                {
                    return;
                }
                Int32 effect = CHNLSVC.Sales.Delete_inv_type(Session["UserCompanyCode"].ToString(), ucCustomer.Code, _saltypeCode);
                var _removefilter = SalesType.Single(c => c.Srtp_cd == _saltypeCode);
                SalesType.Remove(_removefilter);

                grdSalesTypes.DataSource = null;
                grdSalesTypes.DataSource = new List<MasterInvoiceType>();
                grdSalesTypes.DataSource = SalesType;
                grdSalesTypes.DataBind();

                ViewState["SalesType"] = SalesType;
            }
        }

        /*
        Add Office of Entry
        */
        protected void lbtnAddOfficeofEntry_Click(object sender, EventArgs e)
        {


            try
            {

                if (ucCustomer._MstBusOffEntry != null)
                {
                    _MstBusOffEntry = ucCustomer._MstBusOffEntry;
                }

                if (string.IsNullOrEmpty(txtOfficeName.Text))
                {
                    DisplayMessage("Select Office", 2);
                    return;
                }

                if (string.IsNullOrEmpty(ucCustomer.Code))
                {
                    DisplayMessage("Please enter customer code", 2);
                    return;
                }

                if (_MstBusOffEntry != null)
                {
                    MasterBusinessOfficeEntry result = _MstBusOffEntry.Find(x => x._mbbo_off_cd == ddlOfficeofEntry.SelectedValue.ToString());
                    if (result != null)
                    {
                        DisplayMessage("This Office already exist", 2);
                        return;
                    }
                }
                else
                {
                    _MstBusOffEntry = new List<MasterBusinessOfficeEntry>();
                }


                MasterBusinessOfficeEntry _cOffEnty = new MasterBusinessOfficeEntry();
                _cOffEnty._mbbo_com = Session["UserCompanyCode"].ToString();
                _cOffEnty._mbbo_cd = ucCustomer.Code;
                _cOffEnty._mbbo_tp = "C";
                _cOffEnty._mbbo_direct = (ddlDirection.SelectedValue.ToString() == "Office In") ? 1 : 0;
                _cOffEnty._mbbo_act = (chkStatus.Checked == true) ? 1 : 0;
                _cOffEnty._mbbo_off_cd = ddlOfficeofEntry.SelectedValue.ToString();
                _cOffEnty._mbbo_cre_by = Session["UserID"].ToString();
                _cOffEnty._mbbo_mod_by = Session["UserID"].ToString();
                _MstBusOffEntry.Add(_cOffEnty);

                grdcustomerOfficeEntry.DataSource = null;
                grdcustomerOfficeEntry.DataSource = new List<MasterBusinessOfficeEntry>();
                grdcustomerOfficeEntry.DataSource = _MstBusOffEntry;

                grdcustomerOfficeEntry.DataBind();

                ViewState["_MstBusOffEntry"] = _MstBusOffEntry;

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n", 2);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        /*
         Load values to Sales types,customer segmentations , Account, Additional Details and office of entry
        */


        /*
        Error Messages Display 
        */

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        protected void _Mandatory_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            //foreach (GridViewRow gvr in this.grdCustomerSegmentation.Rows)
            //{

            // CheckBox check = (CheckBox)gvr.FindControl("_Mandatory");
            // if (check.Checked)
            //    {
            //        i++;
            //        if (i > 1)
            //        {
            //            DisplayMessage("Please Check Only One Option", 2);
            //            check.Checked = false;
            //        }

            //    }

            //}
        }

        protected void lbtnsunaccountup_Click(object sender, EventArgs e)
        {
            try
            {
                upSunaccount.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtnPriceClose_Click(object sender, EventArgs e)
        {
            //  txtPriceEdit.Text = "";
            upSunaccount.Hide();
        }

        protected void lbtnloaddata_Click(object sender, EventArgs e)
        {
            try
            {
                string sunaccno = txtsunaccno.Text.ToString();
                string com = Session["UserCompanyCode"].ToString();
                List<SunAccountBusEntity> cuslist = new List<SunAccountBusEntity>();

                cuslist = CHNLSVC.CustService.GetSunAccountDetails(sunaccno.Trim(), com.Trim());
                if (cuslist.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Cannot found account" + "');", true);
                    ucCustomer.TxtCusCodenew = "";
                    ucCustomer.TxtFullName = "";

                }
                else
                {


                    MasterBusinessEntity bus = new MasterBusinessEntity();
                    bus.Mbe_acc_cd = cuslist.First().accnt_code.ToString().Trim();
                    bus.Mbe_name = cuslist.First().accnt_name.ToString().Trim();
                    bus.Mbe_mob = cuslist.First().CONTACT.ToString().Trim();
                    bus.Mbe_add1 = cuslist.First().ADDRESS_1.ToString().Trim();
                    bus.Mbe_add2 = cuslist.First().ADDRESS_2.ToString().Trim() + " " + cuslist.First().ADDRESS_3.ToString().Trim();
                    bus.mbe_ref_no = cuslist.First().lookup.ToString().Trim();
                    bus.Mbe_com = Session["UserCompanyCode"].ToString();
                    bus.Mbe_tp = "C";


                    //  savesunaccount(bus);

                    MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(cuslist.First().accnt_code.ToString().Trim().ToString(), null, null, null, null, Session["UserCompanyCode"].ToString());

                    if (custProf.Mbe_cd !=null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Custormer Already created');", true);
                        ucCustomer.TxtCusCodenew = cuslist.First().accnt_code.ToString();
                        ucCustomer.txtCusCode_TextChanged(null,null);
                        //ucCustomer.TxtFullName = cuslist.First().accnt_name.ToString();
                        ucCustomer.TxtAddress1 = cuslist.First().ADDRESS_1.ToString();
                        ucCustomer.TxtAddress2 = cuslist.First().ADDRESS_2.ToString();
                        //ucCustomer.TxtMobile = cuslist.First().CONTACT.ToString();
                        txtexecutive.Text = cuslist.First().lookup.ToString().Trim();
                    }
                    else
                    {
                        ucCustomer.TxtCusCodenew = cuslist.First().accnt_code.ToString().Trim();
                        ucCustomer.TxtFullName = cuslist.First().accnt_name.ToString().Trim();
                        ucCustomer.TxtAddress1 = cuslist.First().ADDRESS_1.ToString().Trim();
                        ucCustomer.TxtAddress2 = cuslist.First().ADDRESS_2.ToString().Trim();
                        ucCustomer.TxtMobile = cuslist.First().CONTACT.ToString().Trim();
                        Session["SUN"] = "true";
                        ucCustomer.TxtCusCode.ReadOnly = true;
                    }

                  

                }
                upSunaccount.Hide();

            }
            catch (Exception ex)
            {

            }
        }
        private void savesunaccount(MasterBusinessEntity bus)
        {
            int efffect = CHNLSVC.Sales.SavesunAccount(bus);
            if (efffect < 1) ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Can't Save" + "');", true);
        }
        protected void txtsunaccno_TextChanged(object sender, EventArgs e)
        {

            try
            {
                //string sunaccno = txtsunaccno.Text.ToString();
                //string com = Session["UserCompanyCode"].ToString();
                //List<SunAccountBusEntity> cuslist = new List<SunAccountBusEntity>();

                //cuslist = CHNLSVC.CustService.GetSunAccountDetails(sunaccno.Trim(), com.Trim());
                //if (cuslist.Count == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + "Cannot found account" + "');", true);
                //    ucCustomer.TxtCusCodenew = "";
                //    ucCustomer.TxtFullName = "";
                //}
                //else
                //{
                //    ucCustomer.TxtCusCodenew = cuslist.First().accnt_code.ToString();
                //    ucCustomer.TxtFullName = cuslist.First().accnt_name.ToString();
                //    ucCustomer.TxtAddress1 = cuslist.First().ADDRESS_1.ToString();
                //    ucCustomer.TxtAddress2 = cuslist.First().ADDRESS_2.ToString();
                //    ucCustomer.TxtMobile = cuslist.First().CONTACT.ToString();
                //    Session["SUNLOOKUP"] = cuslist.First().lookup.ToString();
                //    ucCustomer.TxtCusCode.ReadOnly = true;
                //    Session["SUN"] = "true";

                //}
                //upSunaccount.Hide();

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnProceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProcedureCodes);
                DataTable result = CHNLSVC.CommonSearch.SearchGetProcedureCode(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ProcedureCode";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ProcedureCodes:
                    {
                        paramsText.Append("LK" + seperator + Convert.ToString(Session["UserCompanyCode"]) + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
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
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        public void FilterData()
        {
            if (lblvalue.Text == "ProcedureCode")
            {
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProcedureCodes);
                DataTable result = CHNLSVC.CommonSearch.SearchGetProcedureCode(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            if (lblvalue.Text == "Customer")
            {
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
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
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = grdResult.SelectedRow.Cells[1].Text;
                string desc = grdResult.SelectedRow.Cells[2].Text;
                Session["IsSearch"] = null;
                mpUserPopup.Hide();


                 if (lblvalue.Text == "ProcedureCode")
                {
                    txtProceCode.Text = code;
                }
                 if (lblvalue.Text == "Customer")
                 {
                     txtmulticus.Text = code;
                     multiplecuspopup.Show();
                 }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnmultycusclose_Click(object sender, EventArgs e)
        {
            mpUserPopup.Hide();
        }

        protected void lbtnmulticus_Click(object sender, EventArgs e)
        {
            if (ucCustomer.TxtCusCode.Text.ToString()=="")
            {
                DisplayMessage("Please Select Custormer Code", 2);
                return;
            }
            List<mst_busentity_add_cus> _addcus = CHNLSVC.Sales.GetAddtinalCusCodes(ucCustomer.TxtCusCode.Text.ToString());
            Session["mst_busentity_add_cus"] = _addcus;
            grdmultycus.DataSource = _addcus;
            grdmultycus.DataBind();
            multiplecuspopup.Show();
        }

       

        protected void txtmulticus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "CODE", txtmulticus.Text.ToString());
                if (result != null && result.Rows.Count>0)
                {
                    multiplecuspopup.Show();
                }
                else
                {
                    DisplayMessage("Invalid Custormer Code", 2);
                    txtmulticus.Text = "";
                    multiplecuspopup.Show();
                    return;
                }

            }catch(Exception ex)
            {
                 DisplayMessage(ex.Message, 1);
                return;
            }
        }

        protected void lbtnaddmulticus_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_busentity_add_cus> _addcus = Session["mst_busentity_add_cus"] as List<mst_busentity_add_cus>;

                if (_addcus != null && _addcus.Count>0)
                {
                    var count = _addcus.Where(a => a.mbac_add_cd == txtmulticus.Text.ToString()).Count();
                    if (count>0)
                    {
                        DisplayMessage("Already Added this code!", 2);
                        multiplecuspopup.Show();
                        return;
                    }

                    mst_busentity_add_cus ob = new mst_busentity_add_cus();
                    ob.mbac_act = 1;
                    ob.mbac_add_cd = txtmulticus.Text;
                    ob.mbac_com = Session["UserCompanyCode"].ToString();
                    ob.mbac_cre_by = Session["UserID"].ToString();
                    ob.mbac_cre_dt = DateTime.Now;
                    ob.mbac_master_cd = ucCustomer.TxtCusCode.Text.ToString();

                    _addcus.Add(ob);
                    Session["mst_busentity_add_cus"] = _addcus;
                    grdmultycus.DataSource = _addcus;
                    grdmultycus.DataBind();
                    txtmulticus.Text = "";
                    multiplecuspopup.Show();
                }
                else
                {
                    _addcus = new List<mst_busentity_add_cus>();
                    mst_busentity_add_cus ob = new mst_busentity_add_cus();
                    ob.mbac_act = 1;
                    ob.mbac_add_cd = txtmulticus.Text;
                    ob.mbac_com = Session["UserCompanyCode"].ToString();
                    ob.mbac_cre_by = Session["UserID"].ToString();
                    ob.mbac_cre_dt = DateTime.Now;
                    ob.mbac_master_cd = ucCustomer.TxtCusCode.Text.ToString();

                    _addcus.Add(ob);
                    Session["mst_busentity_add_cus"] = _addcus;
                    grdmultycus.DataSource = _addcus;
                    grdmultycus.DataBind();
                    txtmulticus.Text = "";
                    multiplecuspopup.Show();
                }
              
               

            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                return;
            }
        }

        protected void lbtndelmulcus_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label addcuscd = (Label)row.FindControl("lblmbac_add_cd");
                string addcuscode = addcuscd.Text.ToString();
                List<mst_busentity_add_cus> _addcus = Session["mst_busentity_add_cus"] as List<mst_busentity_add_cus>;
                _addcus.RemoveAll(a => a.mbac_add_cd == addcuscode);
                Session["mst_busentity_add_cus"] = _addcus;
                grdmultycus.DataSource = _addcus;
                grdmultycus.DataBind();
                multiplecuspopup.Show();
            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                return;
            }
        }

        protected void lbtnsubcussave_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_busentity_add_cus> _addcus = Session["mst_busentity_add_cus"] as List<mst_busentity_add_cus>;
                string err = "";
                int effect = CHNLSVC.Sales.SaveAddtionalCustomer(_addcus, ref  err);
                if (effect>0)
                {
                    DisplayMessage("Successfully Saved!!",3);
                    return;

                }
                else
                {
                    DisplayMessage(err, 1);
                    return;
                }
            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                return;
            }
        }

        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "Customer";
                ViewState["SEARCH"] = result;
                mpUserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                return;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpUserPopup.Hide();
        }

        protected void txtTINNo_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtTINNo.Text))
            {
                DisplayMessage("Invalid charactor found in tin no.", 2);
                txtTINNo.Focus();
                return;
            }
        }
        public bool validateinputString(string input)
        {
            Match match = Regex.Match(input, @"([~!@#$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            Match match = Regex.Match(input, @"([~!@#$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }

        protected void txtddlPrVal1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtddlPrVal1.Text))
            {
                DisplayMessage("Invalid charactor found in print value 1.", 2);
                txtddlPrVal1.Focus();
                return;
            }
        }

        protected void txtddlPrVal2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtddlPrVal2.Text))
            {
                DisplayMessage("Invalid charactor found in print value 2.", 2);
                txtddlPrVal2.Focus();
                return;
            }
        }

        protected void txtWareHseCde_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtWareHseCde.Text))
            {
                DisplayMessage("Invalid charactor found in warehouse code.", 2);
                txtWareHseCde.Focus();
                return;
            }
        }

        protected void txtAccCde_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtAccCde.Text))
            {
                DisplayMessage("Invalid charactor found in account code.", 2);
                txtAccCde.Focus();
                return;
            }
        }

        protected void txtexecutive_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtexecutive.Text))
            {
                DisplayMessage("Invalid charactor found in executive code.", 2);
                txtexecutive.Focus();
                return;
            }
        }

        protected void txtOfficeName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtOfficeName.Text))
            {
                DisplayMessage("Invalid charactor found in office name.", 2);
                txtOfficeName.Focus();
                return;
            }
        }

        protected void txtProceCode_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtProceCode.Text))
            {
                DisplayMessage("Invalid charactor found in procedure code.", 2);
                txtProceCode.Focus();
                return;
            }
        }
    }
}