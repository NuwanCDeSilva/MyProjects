using FastForward.SCMWeb.Services;
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

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucCustomer : System.Web.UI.UserControl
    {
        //public MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        //public GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        //public CustomerAccountRef _account = new CustomerAccountRef();
        //public List<BusEntityItem> busItemList = new List<BusEntityItem>();
        //public List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        public MasterBusinessEntity _custProfile { get { return (MasterBusinessEntity)ViewState["_custProfile"]; } set { ViewState["_custProfile"] = value; } }
        public GroupBussinessEntity _custGroup { get { return (GroupBussinessEntity)ViewState["_custGroup"]; } set { ViewState["_custGroup"] = value; } }
        public CustomerAccountRef _account { get { return (CustomerAccountRef)ViewState["_account"]; } set { ViewState["_account"] = value; } }
        public List<BusEntityItem> busItemList { get { return (List<BusEntityItem>)ViewState["busItemList"]; } set { ViewState["busItemList"] = value; } }
        public List<MasterBusinessEntityInfo> _busInfoList { get { return (List<MasterBusinessEntityInfo>)ViewState["_busInfoList"]; } set { ViewState["_busInfoList"] = value; } }
        public List<MasterInvoiceType> SalesType { get { return (List<MasterInvoiceType>)ViewState["SalesType"]; } set { ViewState["SalesType"] = value; } }
        public List<MasterBusinessEntitySalesType> _salesTypes { get { return (List<MasterBusinessEntitySalesType>)ViewState["_salesTypes"]; } set { ViewState["_salesTypes"] = value; } }
        public List<MasterBusinessOfficeEntry> _MstBusOffEntry { get { return (List<MasterBusinessOfficeEntry>)ViewState["_MstBusOffEntry"]; } set { ViewState["_MstBusOffEntry"] = value; } }

        private Boolean _isExsit = false;
        private Boolean _isGroup = false;
        private Boolean _isUpdate = false;
        private Int32 _eff = 0;
        private string _err = string.Empty;
        Base _basePage;
        public Boolean _isFromOther = false;
        public TextBox obj_TragetTextBox;
        private DataTable _tbl = null;
        
        public TextBox TxtCusCode
        {
          get { return txtCusCode; }
          set { txtCusCode = value; }
        }
        public TextBox TxtNIC
        {
            get { return txtNIC; }
            set { txtNIC = value; }
        }
        public string TxtCusCodenew
        {
            get { return txtCusCode.Text; }
            set { txtCusCode.Text = value; }
        }

        public string TxtFullName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string TxtAddress1
        {
            get { return txtPerAdd1.Text; }
            set { txtPerAdd1.Text = value; }
        }
        public string TxtAddress2
        {
            get { return txtPerAdd2.Text; }
            set { txtPerAdd2.Text = value; }
        }
        public string TxtMobile
        {
            get { return txtMob.Text; }
            set { txtMob.Text = value; }
        }
       
        public string Code
        {
          get { return TxtCusCode.Text; }
          set { TxtCusCode.Text = value; }
        }
        Boolean _isExtendedPage { get { return (Boolean)Session["_isExtentedPage"]; } set { ViewState["_isExtentedPage"] = value; } }
       
        //public bool cuspagestatus;
                
        public void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                loadCutTypes();
                ClearMsg();
                clearPage();
            }
            else
            {
                if (Session["_isFromOther"] == "true")
                {
                    ddlCtype.SelectedItem.Text = "INDIVIDUAL";
                    ddlSex.SelectedItem.Text = "MALE";
                    ddlTitle.SelectedItem.Text = "MR.";
                    txtDOB.Text = System.DateTime.Now.ToShortDateString();
                    if (Session["confmbox"] == "true")
                    {
                        userconfmbox.Show();
                        Session["confmbox"] = "";
                    }
                    else if (Session["code"] == "true")
                    {
                        UserPopoup.Show();
                        Session["code"] = "";
                    }
                
                }
                else if (Session["code"] == "true") 
                {
                    UserPopoup.Show();
                    Session["code"] = "";
                }
                else if (Session["Town"] == "true")
                {
                    UserPopoup.Show();
                    Session["Town"] = "";
                }
                
                EnableTruelinkButton();
                txtDOB.Text = Request[txtDOB.UniqueID];
            }
        }
        private void ClearMsg()
        {
            //WarnningCustomer.Visible = false;
           // SuccessCustomer.Visible = false;
        }
        private void ErrorMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
           // lblWarnningCustomer.Text = _Msg;
           // WarnningCustomer.Visible = true;
        }
        private void SuccessMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
           // lblSuccessCustomer.Text = _Msg;
           // SuccessCustomer.Visible = true;
        }
        public void clearPage()
        {
            
            Session["c"] = "true";
            txtName.Enabled = true;
           // ddlCtype.SelectedValue = "0";
            txtNIC.Text = string.Empty;
            txtPP.Text = string.Empty;
            txtDL.Text = string.Empty;
            txtCusCode.Text = string.Empty;
            txtBR.Text = string.Empty;
            txtMob.Text = string.Empty;
            //ddlPrefLang.SelectedValue = "0";
            txtDOB.Text = System.DateTime.Now.ToShortDateString();
            LoadLanguage();
            chkSMS.Checked = false;
            chkMail.Checked = false;
           // ddlSex.SelectedValue = "0";
            Sextype();
            txtInit.Text = string.Empty;
            txtFname.Text = string.Empty;
            txtSName.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPerAdd1.Text = string.Empty;
            txtPerAdd2.Text = string.Empty;
            txtPerTown.Text = string.Empty;
            txtPerDistrict.Text = string.Empty;
            txtPerPostal.Text = string.Empty;
            txtPerProvince.Text = string.Empty;
            txtPerCountry.Text = string.Empty;
            txtPerPhone.Text = string.Empty;
            txtPerEmail.Text = string.Empty;
            txtPreAdd1.Text = string.Empty;
            txtPreAdd2.Text = string.Empty;
            txtPreTown.Text = string.Empty;
            txtPreDistrict.Text = string.Empty;
            txtPrePostal.Text = string.Empty;
            txtPreProvince.Text = string.Empty;
            txtPreCountry.Text = string.Empty;
            txtPrePhone.Text = string.Empty;
            txtWorkName.Text = string.Empty;
            txtWorkAdd1.Text = string.Empty;
            txtWorkAdd2.Text = string.Empty;
            txtWorkDept.Text = string.Empty;
            txtWorkPhone.Text = string.Empty;
            txtWorkDesig.Text = string.Empty;
            txtWorkFax.Text = string.Empty;
            txtWorkEmail.Text = string.Empty;
            chkVAT.Checked = false;
            chkVatEx.Checked = false;
            txtVatreg.Text = string.Empty;
            chkSVAT.Checked = false;
            txtSVATReg.Text = string.Empty;
            Session["_isExsit"] = _isExsit;
            Session["_isGroup"] = _isGroup;
            Session["_isFromOther"] = _isFromOther;
            Session["_isUpdate"] = _isUpdate;

            txtFname.Enabled = true;
            txtCusCode.Enabled = true;
            txtSName.Enabled = true;
            txtInit.Enabled = true;
            ddlSex.Enabled = true;
            chkVatEx.Checked = false;
            chkVatEx.Enabled = false;
            chkSVAT.Checked = false;
            chkSVAT.Enabled = false;
            txtSVATReg.Text = "";
            txtVatreg.Text = "";
            txtSVATReg.Enabled = false;
            txtVatreg.Enabled = false;
            chkVatEx.Enabled = true;
            MasterBusinessEntity _custProfile = new MasterBusinessEntity();
            GroupBussinessEntity _custGroup = new GroupBussinessEntity();
            CustomerAccountRef _account = new CustomerAccountRef();
            List<BusEntityItem> busItemList = new List<BusEntityItem>();
            List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        }

        private void Sextype()
        {
            ddlSex.Items.Clear();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("MALE", "MALE"));
            items.Add(new ListItem("FEMALE", "FEMALE"));
            ddlSex.Items.AddRange(items.ToArray());
                

        }
        private void EnableFalselinkButton()
        {
            lbtnSave.Enabled = false;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";
        }
        private void EnableTruelinkButton()
        {
            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "UpdateConfirm();";
            lbtnSave.CssClass = "buttonUndocolor";
        }
        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator );
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        private void LoadLanguage()
        {
            _basePage = new Base();
            _tbl = _basePage.CHNLSVC.General.get_Language();
            ddlPrefLang.DataSource = _tbl;
            ddlPrefLang.DataTextField = "mla_desc";
            ddlPrefLang.DataValueField = "mla_cd";
            ddlPrefLang.DataBind();
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
        public void LoadCustProf(MasterBusinessEntity cust, Boolean _isExtendedPage)
        {
            //string typed_nic = txtNicNo.Text.Trim();
            //string typed_ppno = txtPassportNo.Text.Trim().ToUpper();
            //string typed_dl = txtDLno.Text.Trim().ToUpper();
            //string typed_br = txtBrNo.Text.Trim().ToUpper();
            //------------------------------------------
            // ddlCustSupType.SelectedValue = cust.Mbe_sub_tp;
            ddlCtype.SelectedItem.Text = cust.Mbe_cate;
            txtNIC.Text = cust.Mbe_nic;
            txtPP.Text = cust.Mbe_pp_no;
            txtBR.Text = cust.Mbe_br_no;
            txtCusCode.Text = cust.Mbe_cd;
            txtDL.Text = cust.Mbe_dl_no;
            txtMob.Text = cust.Mbe_mob;
            chkSMS.Checked = cust.Mbe_agre_send_sms;
            
            //Added By Dulaj 2018/Jul/11 
        
            //
            //optPCwise.Checked = cust.Mbe_qno_gen_seq;
            //------------------------------------------
           // Sextype();
            string sextype = cust.Mbe_sex;
            if (sextype != "")
            {
                ddlSex.Text = cust.Mbe_sex;
            }
            else
            {

            }
            ddlSex.Enabled = false;
            txtName.Text = cust.Mbe_name;
           // Sextype();
            String nic_ = txtNIC.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(nic_))
            {
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    ddlTitle.SelectedItem.Text = "MS.";
                }
                else
                {
                    ddlTitle.SelectedItem.Text = "MR.";
                }
            }

            if (cust.Mbe_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                txtDOB.Text = Convert.ToDateTime(cust.Mbe_dob).Date.ToShortDateString();// Convert.ToString(cust.Mbe_dob.Date); ToString()
            }
            else
            {

            }
            //-------------------------------------------

            txtPerAdd1.Text = cust.Mbe_add1;
            txtPerAdd2.Text = cust.Mbe_add2;
            txtPerTown.Text = cust.Mbe_town_cd;
            txtPerPostal.Text = cust.Mbe_postal_cd;
            txtPerCountry.Text = cust.Mbe_country_cd;
            txtPerDistrict.Text = cust.Mbe_distric_cd;
            txtPerProvince.Text = cust.Mbe_province_cd;
            txtPerPhone.Text = cust.Mbe_tel;
            txtPerEmail.Text = cust.Mbe_email;

            txtPreAdd1.Text = cust.Mbe_cr_add1;
            txtPreAdd2.Text = cust.Mbe_cr_add2;
            txtPreTown.Text = cust.Mbe_cr_town_cd;
            txtPrePostal.Text = cust.Mbe_cr_postal_cd;
            txtPreCountry.Text = cust.Mbe_cr_country_cd;
            txtPreDistrict.Text = cust.Mbe_cr_distric_cd;
            txtPreProvince.Text = cust.Mbe_cr_province_cd;
            txtPrePhone.Text = cust.Mbe_cr_tel;

            txtWorkAdd1.Text = cust.Mbe_wr_add1;
            txtWorkAdd2.Text = cust.Mbe_wr_add2;
            txtWorkName.Text = cust.Mbe_wr_com_name;
            txtWorkDept.Text = cust.Mbe_wr_dept;
            txtWorkDesig.Text = cust.Mbe_wr_designation;
            txtWorkEmail.Text = cust.Mbe_wr_email;
            txtWorkFax.Text = cust.Mbe_wr_fax;
            txtWorkPhone.Text = cust.Mbe_wr_tel;

            chkVAT.Checked = cust.Mbe_is_tax;
            chkVatEx.Checked = cust.Mbe_tax_ex;
            chkSVAT.Checked = cust.Mbe_is_svat;
            txtVatreg.Text = cust.Mbe_tax_no;
            txtSVATReg.Text = cust.Mbe_svat_no;
            txtInit.Text = cust.MBE_INI;
            txtFname.Text = cust.MBE_FNAME;
            txtSName.Text = cust.MBE_SNAME;
            // Nadeeka 15-12-2014
            chkMail.Checked = cust.Mbe_agre_send_email;


            if (string.IsNullOrEmpty(cust.Mbe_cust_lang))
            {
                ddlPrefLang.SelectedValue = "E";

            }
            else
            {
                ddlPrefLang.SelectedValue = cust.Mbe_cust_lang;
            }

            if (string.IsNullOrEmpty(txtInit.Text))
            {
                txtInit.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                txtFname.Enabled = true;
            }
            else
            {
                txtFname.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtSName.Text))
            {
                txtSName.Enabled = true;
            }
            else
            {
                txtSName.Enabled = false;
            }
            //if (string.IsNullOrEmpty(txtName.Text))
            //{
            //    txtName.Enabled = true;
            //}
            //else
            //{
            //    txtName.Enabled = false;
            //}
            //------------------------------------------
             
            if (_isExtendedPage == true)
            {
                ((TextBox)this.Parent.FindControl("txtTINNo")).Text = cust.Mbe_oth_id_no;
                ((TextBox)this.Parent.FindControl("txtProceCode")).Text = cust.Mbe_proc_cd;
                ((TextBox)this.Parent.FindControl("txtWareHseCde")).Text = cust.Mbe_wh_cd;
                ((TextBox)this.Parent.FindControl("txtAccCde")).Text = cust.Mbe_acc_cd;
                ((TextBox)this.Parent.FindControl("txtMinDwnPymnt")).Text = cust.Mbe_min_dp_per.ToString();
                ((TextBox)this.Parent.FindControl("txtexecutive")).Text = cust.mbe_ref_no;
                ((TextBox)this.Parent.FindControl("txtddlPrVal1")).Text = cust.Mbe_proc_val1;
                ((TextBox)this.Parent.FindControl("txtddlPrVal2")).Text = cust.Mbe_proc_val2;
                ((TextBox)this.Parent.FindControl("txtaccountNumber")).Text = cust.Mbe_acc_no;
               
                
                if ( !string.IsNullOrEmpty( cust.Mbe_pc_stus))
                ((DropDownList)this.Parent.FindControl("ddlShowroomStus")).Text = cust.Mbe_pc_stus.ToString();
                if (!string.IsNullOrEmpty(cust.Mbe_ho_stus))
                ((DropDownList)this.Parent.FindControl("ddlHeadOffStus")).Text = cust.Mbe_ho_stus.ToString();
                
                if (cust.Mbe_qno_gen_seq==0)
                {
                    ((RadioButton)this.Parent.FindControl("optPCwise")).Checked = true;
                }
                else
                {
                    ((RadioButton)this.Parent.FindControl("optCompwise")).Checked = true;
                }
                if (cust.Mbe_act == true)
                {
                    ((CheckBox)this.Parent.FindControl("radactive")).Checked = true;
                }
                if (cust.Mbe_foc == 1)
                {
                    ((CheckBox)this.Parent.FindControl("chkfoc")).Checked = true;
                }
                if (cust.Mbe_is_suspend == true)
                {
                    ((CheckBox)this.Parent.FindControl("radsuspent")).Checked = true;
                }
                if (cust.Mbe_ins_man==1)
               {
                     ((CheckBox)this.Parent.FindControl("chkInsuMan")).Checked = true;

               }
                else
                {
                    ((CheckBox)this.Parent.FindControl("chkInsuMan")).Checked = false;
                }
                if (cust.Mbe_alw_dcn == 1)
                {
                    ((CheckBox)this.Parent.FindControl("chkDCN")).Checked = true;

                }
                else
                {
                    ((CheckBox)this.Parent.FindControl("chkDCN")).Checked = false;
                }

                if (_isExtendedPage == true)
                {
                    GetCustomerDetailsExtended(_isExtendedPage);
                    return;
                }
            }

            txtCusCode.Enabled = false;
        }
        public void LoadCustProfByGrup(GroupBussinessEntity _cust)
        {

            txtNIC.Text = _cust.Mbg_nic;
            txtPP.Text = _cust.Mbg_pp_no;
            txtBR.Text = _cust.Mbg_br_no;
            txtCusCode.Text = _cust.Mbg_cd;
            txtDL.Text = _cust.Mbg_dl_no;
            txtMob.Text = _cust.Mbg_mob;
            ddlSex.SelectedItem.Text = _cust.Mbg_sex;
            txtName.Text = _cust.Mbg_name;
            ddlTitle.SelectedItem.Text = _cust.Mbg_tit;
            txtFname.Text = _cust.Mbg_fname;
            txtSName.Text = _cust.Mbg_sname;
            txtInit.Text = _cust.Mbg_ini;

            if (_cust.Mbg_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                txtDOB.Text = Convert.ToDateTime(_cust.Mbg_dob).Date.ToShortDateString();// Convert.ToString(cust.Mbe_dob.Date); ToString()
            }
            else
            {

            }
            txtPerAdd1.Text = _cust.Mbg_add1;
            txtPerAdd2.Text = _cust.Mbg_add2;
            txtPerTown.Text = _cust.Mbg_town_cd;
            txtPerPostal.Text = _cust.Mbg_postal_cd;
            txtPerCountry.Text = _cust.Mbg_country_cd;
            txtPerDistrict.Text = _cust.Mbg_distric_cd;
            txtPerProvince.Text = _cust.Mbg_province_cd;
            txtPerPhone.Text = _cust.Mbg_tel;
            txtPerEmail.Text = _cust.Mbg_email;

            txtPreAdd1.Text = _cust.Mbg_add1;
            txtPreAdd2.Text = _cust.Mbg_add2;
            txtPreTown.Text = _cust.Mbg_town_cd;
            txtPrePostal.Text = _cust.Mbg_postal_cd;
            txtPreCountry.Text = _cust.Mbg_country_cd;
            txtPreDistrict.Text = _cust.Mbg_distric_cd;
            txtPreProvince.Text = _cust.Mbg_province_cd;
            txtPrePhone.Text = _cust.Mbg_tel;

            txtWorkAdd1.Text = "";
            txtWorkAdd2.Text = "";
            txtWorkName.Text = "";
            txtWorkDept.Text = "";
            txtWorkDesig.Text = "";
            txtWorkEmail.Text = "";
            txtWorkFax.Text = "";
            txtWorkPhone.Text = "";

            chkVAT.Checked = false;
            chkVatEx.Checked = false;
            chkSVAT.Checked = false;
            txtVatreg.Text = "";
            txtSVATReg.Text = "";




            if (string.IsNullOrEmpty(txtInit.Text))
            {
                txtInit.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                txtFname.Enabled = true;
            }
            else
            {
                txtFname.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtSName.Text))
            {
                txtSName.Enabled = true;
            }
            else
            {
                txtSName.Enabled = false;
            }
            //if (string.IsNullOrEmpty(txtName.Text))
            //{
            //    txtName.Enabled = true;
            //}
            //else
            //{
            //    txtName.Enabled = false;
            //}
            //------------------------------------------

            txtCusCode.Enabled = false;


        }
        #region LoadCustProfile
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyDL(string dl)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppno, null, Session["UserCompanyCode"].ToString());
        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, Session["UserCompanyCode"].ToString());
        }
        #endregion LoadCustProfile
        #region LoadCustByGroup
        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }
        public GroupBussinessEntity GetbyNICGrup(string nic)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }
        public GroupBussinessEntity GetbyDLGrup(string dl)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, dl, null, null, null);
        }
        public GroupBussinessEntity GetbyPPnoGrup(string ppno)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, ppno, null, null);
        }
        public GroupBussinessEntity GetbyBrNoGrup(string brNo)
        {
            _basePage = new Base();
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, brNo, null);
        }
        public GroupBussinessEntity GetbyMobGrup(string mobNo)
        {
            _basePage = new Base();
            return _basePage.CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobNo);
        }
        #endregion LoadCustByGroup
        public void Collect_GroupCust()
        {
            _custGroup = new GroupBussinessEntity();
            _custGroup.Mbg_cd = txtCusCode.Text.Trim();
            _custGroup.Mbg_name = txtName.Text.Trim();
            _custGroup.Mbg_tit = ddlTitle.SelectedItem.Text;
            _custGroup.Mbg_ini = txtInit.Text.Trim();
            _custGroup.Mbg_fname = txtFname.Text.Trim();
            _custGroup.Mbg_sname = txtSName.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtPreAdd1.Text.Trim();
            _custGroup.Mbg_add2 = txtPreAdd2.Text.Trim();
            _custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            _custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            _custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            _custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
            _custGroup.Mbg_tel = txtPerPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
            _custGroup.Mbg_mob = txtMob.Text.Trim();
            _custGroup.Mbg_nic = txtNIC.Text.Trim();
            _custGroup.Mbg_pp_no = txtPP.Text.Trim();
            _custGroup.Mbg_dl_no = txtDL.Text.Trim();
            _custGroup.Mbg_br_no = txtBR.Text.Trim();
            _custGroup.Mbg_email = txtPerEmail.Text.Trim();
            _custGroup.Mbg_contact = "";
            _custGroup.Mbg_act = true;
            _custGroup.Mbg_is_suspend = false;
            _custGroup.Mbg_sex = ddlSex.SelectedItem.Text;
            _custGroup.Mbg_dob = Convert.ToDateTime(txtDOB.Text).Date;
            _custGroup.Mbg_cre_by = Session["UserID"].ToString();
            _custGroup.Mbg_mod_by = Session["UserID"].ToString();

        }
        public void Collect_Cust()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;
            _custProfile = new MasterBusinessEntity();
           

            if (txtCusCode.Text.ToString() != "")
            {
                _custProfile.Mbe_acc_cd = txtCusCode.Text.ToString();
                _isExsit = true;
            }
            else
            {
                _custProfile.Mbe_acc_cd = null;
            }

            _custProfile.Mbe_act = true;
            _custProfile.Mbe_add1 = txtPerAdd1.Text.Trim();
            _custProfile.Mbe_add2 = txtPreAdd2.Text.Trim();
            _custProfile.Mbe_town_cd = txtPerTown.Text.Trim();

            if (chkSMS.Checked == true)
            {
                _isSMS = true;
            }
            else
            {
                _isSMS = false;
            }
            _custProfile.Mbe_agre_send_sms = _isSMS;
            _custProfile.Mbe_br_no = txtBR.Text.Trim();
            _custProfile.Mbe_cate = ddlCtype.SelectedItem.Text;
            _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
            _isGroup = Convert.ToBoolean(Session["_isGroup"].ToString());
            if (_isExsit == false && _isGroup == false)
            {
                _custProfile.Mbe_cd = null;
            }
            else
            {
                _custProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            _custProfile.Mbe_com = Session["UserCompanyCode"].ToString();
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = txtPerCountry.Text.Trim();
            _custProfile.Mbe_cr_add1 = txtPreAdd1.Text.Trim();
            _custProfile.Mbe_cr_add2 = txtPreAdd2.Text.Trim();
            _custProfile.Mbe_cr_country_cd = txtPreCountry.Text.Trim();
            _custProfile.Mbe_cr_distric_cd = txtPreDistrict.Text;
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cr_postal_cd = txtPrePostal.Text.Trim();
            _custProfile.Mbe_cr_province_cd = txtPreProvince.Text.Trim();
            _custProfile.Mbe_cr_tel = txtPrePhone.Text.Trim();
            _custProfile.Mbe_cr_town_cd = txtPreTown.Text.Trim();
            _custProfile.Mbe_cre_by = Session["UserID"].ToString();
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
            _custProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
            _custProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = txtDL.Text.Trim();
            _custProfile.Mbe_dob = Convert.ToDateTime(txtDOB.Text).Date;//Convert.ToDateTime(txtDOB.Text).Date;
            _custProfile.Mbe_email = txtPerEmail.Text.Trim();
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;
            _custProfile.Mbe_acc_no = LoadAccountNumber(_custProfile.Mbe_cd);

            if (chkSVAT.Checked == true)
            {
                _isSVAT = true;
            }
            else
            {
                _isSVAT = false;
            }

            _custProfile.Mbe_is_svat = _isSVAT;

            if (chkVAT.Checked == true)
            {
                _isVAT = true;
            }
            else
            {
                _isVAT = false;
            }
            _custProfile.Mbe_is_tax = _isVAT;
            _custProfile.Mbe_mob = txtMob.Text.Trim();
            _custProfile.Mbe_name = txtName.Text.Trim();
            _custProfile.Mbe_nic = txtNIC.Text.Trim();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_postal_cd = txtPerPostal.Text.Trim();
            _custProfile.Mbe_pp_no = txtPP.Text.Trim();
            _custProfile.Mbe_province_cd = txtPerProvince.Text.Trim();
            _custProfile.Mbe_sex = ddlSex.SelectedItem.Text;
            _custProfile.Mbe_sub_tp = null;
            _custProfile.Mbe_svat_no = txtSVATReg.Text.Trim();

            if (chkVatEx.Checked == true)
            {
                _TaxEx = true;
            }
            else
            {
                _TaxEx = false;
            }
            _custProfile.Mbe_tax_ex = _TaxEx;
            _custProfile.Mbe_tax_no = txtVatreg.Text.Trim();
            _custProfile.Mbe_tel = txtPerPhone.Text.Trim();
            _custProfile.Mbe_town_cd = txtPerTown.Text.Trim();
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_wr_add1 = txtWorkAdd1.Text.Trim();
            _custProfile.Mbe_wr_add2 = txtWorkAdd2.Text.Trim();
            _custProfile.Mbe_wr_com_name = txtWorkName.Text.Trim();
            _custProfile.Mbe_wr_country_cd = null;
            _custProfile.Mbe_wr_dept = txtWorkDept.Text.Trim();
            _custProfile.Mbe_wr_designation = txtWorkDesig.Text.Trim();
            _custProfile.Mbe_wr_distric_cd = null;
            _custProfile.Mbe_wr_email = txtWorkEmail.Text.Trim();
            _custProfile.Mbe_wr_fax = txtWorkFax.Text.Trim();
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            _custProfile.Mbe_wr_tel = txtWorkPhone.Text.Trim();
            _custProfile.Mbe_wr_town_cd = null;
            _custProfile.MBE_FNAME = txtFname.Text.Trim();
            _custProfile.MBE_SNAME = txtSName.Text.Trim();
            _custProfile.MBE_INI = txtInit.Text.Trim();
            _custProfile.MBE_TIT = ddlTitle.SelectedItem.Text.Trim();
            if (chkMail.Checked == true)
            {
                _isEmail = true;
            }
            else
            {
                _isEmail = false;
            }
            // Nadeeka 15-12-2014
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.Mbe_cust_lang = ddlPrefLang.SelectedValue.ToString();
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
        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        private void dobGeneration()
        {// NADEEKA 18-12-2014
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = (nicarray[2]).ToString();
            //---------DOB generation----------------------
            string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
            Int32 DPBnum = Convert.ToInt32(threechar);
            if (DPBnum > 500)
            {
                DPBnum = DPBnum - 500;
            }

            // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;

            Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
            monthDict.Add("JAN", 31);
            monthDict.Add("FEF", 29);
            monthDict.Add("MAR", 31);
            monthDict.Add("APR", 30);
            monthDict.Add("MAY", 31);
            monthDict.Add("JUN", 30);
            monthDict.Add("JUL", 31);
            monthDict.Add("AUG", 31);
            monthDict.Add("SEP", 30);
            monthDict.Add("OCT", 31);
            monthDict.Add("NOV", 30);
            monthDict.Add("DEC", 31);

            string bornMonth = string.Empty;
            Int32 bornDate = 0;

            Int32 leftval = DPBnum;
            foreach (var itm in monthDict)
            {
                bornDate = leftval;

                if (leftval <= itm.Value)
                {
                    bornMonth = itm.Key;

                    break;
                }
                leftval = leftval - itm.Value;
            }

            //-------------------------------
            // string bornMonth1 = bornMonth;
            // Int32 bornDate1 = bornDate;

            Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
            monthDict2.Add("JAN", 1);
            monthDict2.Add("FEF", 2);
            monthDict2.Add("MAR", 3);
            monthDict2.Add("APR", 4);
            monthDict2.Add("MAY", 5);
            monthDict2.Add("JUN", 6);
            monthDict2.Add("JUL", 7);
            monthDict2.Add("AUG", 8);
            monthDict2.Add("SEP", 9);
            monthDict2.Add("OCT", 10);
            monthDict2.Add("NOV", 11);
            monthDict2.Add("DEC", 12);
            Int32 dobMon = 0;
            foreach (var itm in monthDict2)
            {
                if (itm.Key == bornMonth)
                {
                    dobMon = itm.Value;
                }
            }
            Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
            try
            {
                DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                txtDOB.Text = dob.Date.ToShortDateString();

            }
            catch (Exception ex)
            {
            }
        }

        public bool IsValidNIC(string nic)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = @"\d{9}[V|v|x|X]";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;
            //make sure an email address was provided
            if (string.IsNullOrEmpty(nic))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(nic);
            }
            //return the value to the calling method
            return valid;
        }
        public void GetCustomerDetailsByCode(Boolean _isExtendedPage)
        {            
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {               
                MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    custProf.Mbe_acc_no = LoadAccountNumber(custProf.Mbe_cd);
                    _isExsit = true;
                    Session["_isExsit"] = "true";
                    Session["_isUpdate"] = "true";
                    //btnCreate.Text = "Update";
                    LoadCustProf(custProf, _isExtendedPage);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    ErrorMsg("Customer is inactivated.Please contact accounts dept.");                    
                   // btnCreate.Enabled = false;
                    EnableFalselinkButton();
                    LoadCustProf(custProf, _isExtendedPage);
                }
                else
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == true)
                    {
                        string cusCD = txtCusCode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null, _isExtendedPage);
                        txtCusCode.Text = cusCD;
                    }
                    //Check the group level
                    GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCusCode.Text.Trim().ToUpper());
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
                   // btnCreate.Text = "Create";
                    if (custProf.Mbe_cd == null)
                    {
                        ErrorMsg("Invalid customer code.");
                       // MessageBox.Show("Invalid customer code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Text = "";
                    }
                }
            }

            if (_isExtendedPage == true)
            {
                GetCustomerDetailsExtended(_isExtendedPage);
            }

        }

        private void GetCustomerDetailsExtended(Boolean _isExtendedPage)
        {
            if (_isExtendedPage == true)
            {
                //Sales Types
                //List<MasterBusinessEntitySalesType> _salesTypes = ViewState["_salesTypes"] as List<MasterBusinessEntitySalesType>;            
                SalesType = ViewState["SalesType"] as List<MasterInvoiceType>;
                _basePage = new Base();
                DataTable _dtSalesTypes = _basePage.CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCusCode.Text);

                   //Added By Dulaj 2018/Dec/03
                DataTable _salesTypeWithDate = new DataTable();
                _salesTypeWithDate.Columns.Add("Srtp_cd",typeof(string));
                _salesTypeWithDate.Columns.Add("Srtp_desc",typeof(string));
                _salesTypeWithDate.Columns.Add("Srtp_main_tp",typeof(string));
                _salesTypeWithDate.Columns.Add("MBSA_VALID_FRM_DT",typeof(DateTime));
                _salesTypeWithDate.Columns.Add("MBSA_VALID_TO_DT", typeof(DateTime));

                if (_dtSalesTypes != null)
                {
                    DataTable _cusSalesTypes = new DataTable();

                    foreach (DataRow sp in _dtSalesTypes.Rows)
                    {
                        string _typecode = sp.Field<string>("MBSA_SA_TP").ToString();
                        DateTime Srtp_valid_from_dt = DateTime.MinValue;
                        if (sp["MBSA_VALID_FRM_DT"].ToString() != "")
                        {
                             Srtp_valid_from_dt = Convert.ToDateTime(sp.Field<DateTime>("MBSA_VALID_FRM_DT").ToString());
                        }
                        DateTime Srtp_valid_to_dt = DateTime.MinValue;
                        if (sp["MBSA_VALID_TO_DT"].ToString() != "")
                        {
                            Srtp_valid_to_dt = Convert.ToDateTime(sp.Field<DateTime>("MBSA_VALID_TO_DT").ToString());
                        }                       
                        DataTable _avdtSalesTypes = _basePage.CHNLSVC.General.GetSalesTypes("", "SRTP_CD", _typecode);


                        if (_avdtSalesTypes != null)
                        {
                            //_cusSalesTypes.Merge(_avdtSalesTypes);
                            foreach(DataRow dr in _avdtSalesTypes.Rows)
                            {
                                _salesTypeWithDate.Rows.Add(_typecode, dr.Field<string>("SRTP_DESC").ToString(), dr.Field<string>("SRTP_MAIN_TP").ToString(), Srtp_valid_from_dt, Srtp_valid_to_dt);
                            }
                        }
                    }




                    ((GridView)this.Parent.FindControl("grdSalesTypes")).DataSource = _salesTypeWithDate;
                    ((GridView)this.Parent.FindControl("grdSalesTypes")).DataBind();

                    //List<MasterInvoiceType> list = _cusSalesTypes.ToList<MasterInvoiceType>;

                    // foreach (DataRow dr in _cusSalesTypes.Rows)
                    //   {
                    //     SalesType.Add(dr);
                    //}
                    // SalesType = _cusSalesTypes.Rows.Cast<MasterInvoiceType>().ToList();
                    // ViewState["SalesType"] = SalesType;

                    SalesType = BaseCls.ToGenericList<MasterInvoiceType>(_salesTypeWithDate, MasterInvoiceType.ConverterCustomerWeb);
                    ViewState["SalesType"] = SalesType;
                }



                //Load Customer Segmentation
                List<MasterBusinessEntityInfo> _segmentation = _basePage.CHNLSVC.Sales.GetCustomerSegmentation(txtCusCode.Text);
                if (_segmentation !=null)
                {
                    _segmentation = _segmentation.Where(z => z.Mbei_available == 1).ToList();
                    if (_segmentation != null && _segmentation.Count > 0)
                    {
                        foreach (GridViewRow gr in ((GridView)this.Parent.FindControl("grdCustomerSegmentation")).Rows)
                        {
                            Label sd = (Label)gr.FindControl("rbt_tp");
                            string item = sd.Text;
                            int avai = (from _res in _segmentation
                                        where _res.Mbei_tp == item && _res.Mbei_com == Session["UserCompanyCode"].ToString()
                                        select _res.Mbei_available).SingleOrDefault<int>();
                            if (avai == 1)
                            {

                                DataTable dtVal = new DataTable();
                                dtVal = _basePage.CHNLSVC.Sales.GetBusinessEntityAllValues("C", item);
                                DropDownList ddlAgeVals = (DropDownList)gr.FindControl("ddlAgeVals");
                                CheckBox ch = (CheckBox)gr.FindControl("_Mandatory");
                                string selectedval = (from _res in _segmentation
                                                      where _res.Mbei_tp == item && _res.Mbei_com == Session["UserCompanyCode"].ToString()
                                                      select _res.Mbei_val).SingleOrDefault<string>();



                                ddlAgeVals.SelectedIndex = ddlAgeVals.Items.IndexOf(ddlAgeVals.Items.FindByValue(selectedval));
                                ch.Checked = true;
                            }


                        }







                    }
                }
            

                //Account
                CustomerAccountRef _cust = _basePage.CHNLSVC.Sales.GetCustomerAccount(Session["UserCompanyCode"].ToString(), txtCusCode.Text);
                if (_cust != null)
                {
                    //txtCredLimit
                    //chkDCN
                    //chkInsuMan
                    //txtMinDwnPymnt
                    //txtCredLimit.Text = _cust.Saca_crdt_lmt.ToString();

                    ((TextBox)this.Parent.FindControl("txtCredLimit")).Text = _cust.Saca_crdt_lmt.ToString();

                }

                //Load Office of Entry                
                _MstBusOffEntry = ViewState["_MstBusOffEntry"] as List<MasterBusinessOfficeEntry>;
                 Int32 der=0;
                _MstBusOffEntry = _basePage.CHNLSVC.General.getCustomerOfficeofEntry(Session["UserCompanyCode"].ToString(), txtCusCode.Text, "C");
                if(_MstBusOffEntry !=null)    der = _MstBusOffEntry.Max(z => z._mbbo_direct);
             
                if (_MstBusOffEntry != null)
                {
                    _MstBusOffEntry.Where(w => w._mbbo_direct == 1).ToList().ForEach(s => s._mbbo_direct = 1);
                    _MstBusOffEntry.Where(w => w._mbbo_direct == 0).ToList().ForEach(s => s._mbbo_direct = 0);
                    _MstBusOffEntry.Where(w => w._mbbo_act == 1).ToList().ForEach(s => s._mbbo_act = 1);
                    _MstBusOffEntry.Where(w => w._mbbo_act == 0).ToList().ForEach(s => s._mbbo_act = 0);
                }
               
               
              
              
                ((GridView)this.Parent.FindControl("grdcustomerOfficeEntry")).DataSource = _MstBusOffEntry;
                ((GridView)this.Parent.FindControl("grdcustomerOfficeEntry")).DataBind();

                ViewState["_MstBusOffEntry"] = _MstBusOffEntry;

            }
        }
        
        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "code")
            {     
                    Session["c"] = "true";
                    txtCusCode.Text = ID;
                    GetCustomerDetailsByCode(_isExtendedPage);
                    lblvalue.Text = "";
                    UserPopoup.Hide();
                    return;                

            }
            if (lblvalue.Text == "Town")
            {
                string Dis = grdResult.SelectedRow.Cells[2].Text;
                string Pro = grdResult.SelectedRow.Cells[3].Text;
                string pcode = grdResult.SelectedRow.Cells[4].Text;
                Session["c"] = "true";
                txtPerTown.Text = ID;
                txtPreTown.Text = ID;
                txtPerDistrict.Text = Dis;
                txtPerPostal.Text = pcode;
                txtPerProvince.Text = Pro;
                txtPreDistrict.Text = Dis;
                txtPrePostal.Text = pcode;
                txtPreProvince.Text = Pro;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Session["c"] = "true";
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "code")
            {

                _basePage = new Base();
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, null, null);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Town")
            {
                _basePage = new Base();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetTown(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "code")
            {
                Session["c"] = "true";
                _basePage = new Base();
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(),  txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Town")
            {
                Session["c"] = "true";
                _basePage = new Base();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetTown(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "code")
            {
                Session["c"] = "true";
                _basePage = new Base();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(),  txtSearchbyword.Text.ToString());
                
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["code"] = "true";
                return;
            }
            if (lblvalue.Text == "Town")
            {
                Session["c"] = "true";
                _basePage = new Base();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetTown(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["Town"] = "true";
                return;
            }
        }
        #endregion
        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                _basePage = new Base();
               // btnCreate.Enabled = true;
                EnableTruelinkButton();
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    ErrorMsg("Invalid NIC.");
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    string nicnum = "";
                    //check multiple Add By Chamal 24/04/2014
                    List<MasterBusinessEntity> _custList = _basePage.CHNLSVC.Sales.GetCustomerByKeys(Session["UserCompanyCode"].ToString(), txtNIC.Text.Trim(), "", "", "", "", 1);
                    if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    {
                        foreach (var nics in _custList)
                        {
                            nicnum = nicnum + nics.Mbe_cd.ToString() + "   ,";
                        }
                        string _custNIC = "Duplicate customers found ";
                        foreach (var _nicCust in _custList)
                        {
                            _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        }
                        // _custNIC = _custNIC + "\nPlease contact accounts department";
                        string _msg = "Duplicate customers found Custormer Code : " + nicnum;
                        ErrorMsg(_msg);
                       // MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNIC.Text = "";
                        txtNIC.Focus();
                      //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _custNIC + "');", true);
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        Session["_isUpdate"] = "true";
                        Session["_isExsit"] = "true";
                        _isExsit = true;
                       // btnCreate.Text = "Update";
                        dobGeneration();
                        LoadCustProf(custProf, _isExtendedPage);
                        //txtDL.ReadOnly = true;
                        //txtPP.ReadOnly = true;
                        //txtNIC.ReadOnly = true;
                        //txtBR.ReadOnly = true;
                        //txtMob.ReadOnly = true;
                        Int32 _attemt = 0;

                        //load extended page details
                        if (_isExtendedPage == true)
                        {
                             GetCustomerDetailsExtended(_isExtendedPage);
                        }
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        ErrorMsg("Customer is inactivated.Please contact accounts dept.");
                        EnableFalselinkButton();
                        //btnCreate.Enabled = false;
                        LoadCustProf(custProf, _isExtendedPage);
                    }
                    else//added on 01/10/2012
                    {
                        _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                        if (_isExsit == true && _custList !=null)
                        {
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null, _isExtendedPage);
                            txtNIC.Text = nic;
                        }

                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                            Session["_isGroup"] = "true";
                            //btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                            Session["_isGroup"] = "false";
                        }
                        Session["_isExsit"] = "false";
                        _isExsit = false;
                        //btnCreate.Text = "Create";
                        String nic_ = txtNIC.Text.Trim().ToUpper();
                        char[] nicarray = nic_.ToCharArray();
                        string thirdNum = (nicarray[2]).ToString();
                        if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                        {
                            ddlSex.SelectedItem.Text = "FEMALE";
                            ddlTitle.SelectedItem.Text = "MS.";
                        }
                        else
                        {
                            ddlSex.SelectedItem.Text = "MALE";
                            ddlTitle.SelectedItem.Text = "MR.";
                        }


                        //---------DOB generation----------------------
                        string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                        Int32 DPBnum = Convert.ToInt32(threechar);
                        if (DPBnum > 500)
                        {
                            DPBnum = DPBnum - 500;
                        }



                        // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;


                        Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                        monthDict.Add("JAN", 31);
                        monthDict.Add("FEF", 29);
                        monthDict.Add("MAR", 31);
                        monthDict.Add("APR", 30);
                        monthDict.Add("MAY", 31);
                        monthDict.Add("JUN", 30);
                        monthDict.Add("JUL", 31);
                        monthDict.Add("AUG", 31);
                        monthDict.Add("SEP", 30);
                        monthDict.Add("OCT", 31);
                        monthDict.Add("NOV", 30);
                        monthDict.Add("DEC", 31);

                        string bornMonth = string.Empty;
                        Int32 bornDate = 0;

                        Int32 leftval = DPBnum;
                        foreach (var itm in monthDict)
                        {
                            bornDate = leftval;

                            if (leftval <= itm.Value)
                            {
                                bornMonth = itm.Key;

                                break;
                            }
                            leftval = leftval - itm.Value;
                        }

                        //-------------------------------
                        // string bornMonth1 = bornMonth;
                        // Int32 bornDate1 = bornDate;

                        Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                        monthDict2.Add("JAN", 1);
                        monthDict2.Add("FEF", 2);
                        monthDict2.Add("MAR", 3);
                        monthDict2.Add("APR", 4);
                        monthDict2.Add("MAY", 5);
                        monthDict2.Add("JUN", 6);
                        monthDict2.Add("JUL", 7);
                        monthDict2.Add("AUG", 8);
                        monthDict2.Add("SEP", 9);
                        monthDict2.Add("OCT", 10);
                        monthDict2.Add("NOV", 11);
                        monthDict2.Add("DEC", 12);
                        Int32 dobMon = 0;
                        foreach (var itm in monthDict2)
                        {
                            if (itm.Key == bornMonth)
                            {
                                dobMon = itm.Value;
                            }
                        }
                        Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                        try
                        {
                            DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                            txtDOB.Text = dob.Date.ToShortDateString();
                            //dob.ToString("dd/MM/yyyy");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        protected void txtPP_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPP.Text))
            {
                ErrorMsg("Invalid charactor found in passport no.");
                txtPP.Focus();
                return;
            }
            string _current_cust = string.Empty;
            if (!string.IsNullOrEmpty(txtPP.Text))
            {
                _basePage = new Base();
                EnableTruelinkButton();
               // btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _current_cust = custProf.Mbe_cd;
                    Session["_isUpdate"] = true;
                    Int32 _attemt = 0;
                    ////txtDL.ReadOnly = true;
                    ////txtPP.ReadOnly = true;
                    ////txtNIC.ReadOnly = true;
                    ////txtBR.ReadOnly = true;
                    ////txtMob.ReadOnly = true;
                    #region PP
                    if (_current_cust != txtCusCode.Text)
                    {
                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true)
                        {
                            lblMssg.Text = "Customer already exists for this  mobile number";
                            lblMssg1.Text = "Do you want to recall the existing  customer details ?";
                            userconfmbox.Show();
                            Session["confmbox"] = "true";
                        }
                    }
                    #endregion






                   // Session["_isExsit"] = "true";
                   // _isExsit = true;
                   //// btnCreate.Text = "Update";
                   // LoadCustProf(custProf);

                    

                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    ErrorMsg("Customer is inactivated.Please contact accounts dept.");
                   // MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   // btnCreate.Enabled = false;
                    EnableFalselinkButton();
                    LoadCustProf(custProf, _isExtendedPage);
                }
                else
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == true)
                    {
                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true)
                        {
                           // btnCreate.Text = "Update";
                        }
                        else
                        {
                            string PP = txtPP.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null, _isExtendedPage);
                            txtPP.Text = PP;
                            //Check the group level
                            GroupBussinessEntity _grupProf = GetbyPPnoGrup(txtPP.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                Session["_isGroup"] = "true";
                                _isExsit = false;
                                Session["_isExsit"] = "false";
                                // btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                Session["_isGroup"] = "false";
                                _isGroup = false;
                            }
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                            //btnCreate.Text = "Create";
                        }                       
                    }
                   
                }
            }
        }
        public void lbtnCusCode_Click(object sender, EventArgs e)
        {

            _basePage = new Base();
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, null, null);
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            DataTable result = _basePage.CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "code";
            UserPopoup.Show();
        }

        protected void lbtnTown_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.GetTown(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Town";
            UserPopoup.Show();
        }

        protected void txtDL_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtDL.Text))
            {
                ErrorMsg("Invalid charactor found in DL no.");
                txtDL.Focus();
                return;
            }
            string _current_cust = string.Empty;
            // Int32 _cnt = 0;
            if (!string.IsNullOrEmpty(txtDL.Text))
            {
                // btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _current_cust = custProf.Mbe_cd;
                    Session["_isUpdate"] = true;
                    //txtDL.ReadOnly = true;
                    //txtPP.ReadOnly = true;
                    //txtNIC.ReadOnly = true;
                    //txtBR.ReadOnly = true;
                    //txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {
                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true)
                        {
                            lblMssg.Text = "Customer already exists for this  DL number";
                            lblMssg1.Text = "Do you want to recall the existing  customer details ?";
                            userconfmbox.Show();
                            Session["confmbox"] = "true";
                        }


                    }
                    #endregion

                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    ErrorMsg("Customer is inactivated.Please contact accounts dept.");
                    //MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // btnCreate.Enabled = false;
                    LoadCustProf(custProf, _isExtendedPage);


                }
                else
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == true)
                    {

                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true)
                        {
                            _isExsit = true;
                            Session["_isExsit"] = "true";
                            //btnCreate.Text = "Update";
                        }
                        else
                        {
                            string DL = txtDL.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null, _isExtendedPage);
                            txtDL.Text = DL;
                            //Check the group level
                            GroupBussinessEntity _grupProf = GetbyDLGrup(txtDL.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                Session["_isExsit"] = "false";
                                Session["_isGroup"] = "true";
                                //btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                Session["_isGroup"] = "false";
                                _isGroup = false;
                            }
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                            //btnCreate.Text = "Create";
                        }
                    }

                }
            }
        }

        public void txtCusCode_TextChanged(object sender, EventArgs e)
        {
            GetCustomerDetailsByCode(_isExtendedPage);
        }

        protected void txtBR_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtBR.Text))
            {
                ErrorMsg("Invalid charactor found in BR no.");
                txtBR.Focus();
                return;
            }
            string _current_cust = string.Empty;
            if (!string.IsNullOrEmpty(txtBR.Text))
            {
                _basePage = new Base();
                EnableTruelinkButton();
               // btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _current_cust = custProf.Mbe_cd;
                    Session["_isUpdate"] = true;
                    txtDL.ReadOnly = true;
                    //txtPP.ReadOnly = true;
                    //txtNIC.ReadOnly = true;
                    //txtBR.ReadOnly = true;
                    //txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {

                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true)
                        {
                            lblMssg.Text = "Customer already exists for this  BR number";
                            lblMssg1.Text = "Do you want to recall the existing  customer details ?";
                            userconfmbox.Show();
                            Session["confmbox"] = "true";
                        }


                    }
                    #endregion
                    // _isExsit = true;
                   // Session["_isExsit"] = "true";
                   //// btnCreate.Text = "Update";
                   // LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    ErrorMsg("Customer is inactivated.Please contact accounts dept.");
                   // MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   // btnCreate.Enabled = false;
                    LoadCustProf(custProf, _isExtendedPage);
                }
                else
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == false)
                    {
                        string BR = txtBR.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        //LoadCustProf(cust_null, _isExtendedPage);
                        txtBR.Text = BR;
                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyBrNoGrup(txtBR.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                            Session["_isGroup"] = "true";
                           // btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                            Session["_isGroup"] = "false";
                        }
                        _isExsit = false;
                        Session["_isExsit"] = "false";
                       // btnCreate.Text = "Create";
                    }

                }
            }
        }

        public void MobileNoVerification()
        {
            int distance;
            ////if (int.TryParse(txtMob.Text, out distance))
            ////{
            ////    return;
            ////}
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                _basePage = new Base();
                EnableTruelinkButton();
                // btnCreate.Enabled = true;
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
               // Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                string _msg = "";
                Boolean _isValid = _basePage.CHNLSVC.General.IsvalidMobileNo(txtMob.Text.Trim(),out _msg);

                if (_isValid == false)
                {
                    ErrorMsg("Invalid mobile number.");
                    // MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMob.Text = "";
                    txtMob.Focus();
                    return;
                }
                string cuscode = "";
                string _current_cust = string.Empty;
                Int32 _cnt = 0;
                //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                List<MasterBusinessEntity> _fk = _basePage.CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                if (_fk.Count > 0)
                {
                    foreach (var fklist in _fk)
                    {
                        cuscode = cuscode + fklist.Mbe_cd.ToString() + "  ,";
                    }
                }
               
                if (_fk != null && _fk.Count > 0)
                {
                    _cnt = _fk.Count;
                    _current_cust = _fk[0].Mbe_cd;
                    //  LoadCustProf(_masterBusinessCompany, _isExtendedPage);
                    if (_fk.Count > 1 && Session["_isinvoicepage"] as string != "true")
                    {
                        ErrorMsg("There are/is " + _fk.Count + " customer(s) available for the selected mobile #." + "Custormer Code: " + cuscode);
                        //MessageBox.Show("There are " + _fk.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMob.Text = "";
                        txtMob.Focus();
                        return;
                    }
                    if (_fk.Count > 1 && Session["_isinvoicepage"] as string == "true" && txtCusCode.Text.ToString() == "")
                    {
                        ErrorMsg("There are/is " + _fk.Count + "  customer(s) available for the selected mobile #." + "Custormer Code: " + cuscode);
                        //MessageBox.Show("There are " + _fk.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMob.Text = "";
                        txtMob.Focus();
                        return;
                    }
                    if (_fk.Count == 1 && Session["_isinvoicepage"] as string != "true")
                    {
                        _masterBusinessCompany = _basePage.CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                        LoadCustProf(_masterBusinessCompany, _isExtendedPage);
                        Session["_isExsit"] = "true";
                        return;
                    }
                    if (_fk.Count == 1 && Session["_isinvoicepage"] as string == "true" && txtCusCode.Text.ToString() == "")
                    {
                        _masterBusinessCompany = _basePage.CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                        LoadCustProf(_masterBusinessCompany, _isExtendedPage);
                        Session["_isExsit"] = "true";
                        return;
                    }

                    _isExsit = true;
                    Session["_isExsit"] = "true";
                    Session["_isGroup"] = "false";
                }
                Int32 _attemt = 0;
                if (_current_cust != txtCusCode.Text || _cnt > 1)
                {
                    //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                    _masterBusinessCompany = _basePage.CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        //  Session["Load"] = "";
                        Session["_isUpdate"] = true;
                        //txtDL.ReadOnly = true;
                        //txtPP.ReadOnly = true;
                        //txtNIC.ReadOnly = true;
                        //txtBR.ReadOnly = true;
                        //txtMob.ReadOnly = true;
                        #region Mobileno
                        _isUpdate = Convert.ToBoolean(Session["_isUpdate"].ToString());
                        if (_isUpdate == true && txtCusCode.Text.ToString() != "")
                        {
                            lblMssg.Text = "Customer already exists for this  mobile number";
                            lblMssg1.Text = "Do you want to recall the existing  customer details ?";
                            userconfmbox.Show();
                            Session["confmbox"] = "true";
                        }
                        #endregion

                        //Session["_isExsit"] = "true";
                        //_isExsit = true;
                        //btnCreate.Text = "Update";
                        //LoadCustProf(_masterBusinessCompany, _isExtendedPage);
                    }
                    else if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == false)
                    {
                        ErrorMsg("Customer is inactivated.Please contact accounts dept.");
                        // MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // btnCreate.Enabled = false;
                        EnableFalselinkButton();
                        LoadCustProf(_masterBusinessCompany, _isExtendedPage);
                    }
                    else
                    {
                        _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                        if (_isExsit == true)
                        {
                            string Mob = txtMob.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            //   if (txtCusCode.Text.ToString() == "") LoadCustProf(cust_null, _isExtendedPage);
                            txtMob.Text = Mob;
                            //  Session["Load"] = "Loaded";

                        }
                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyMobGrup(txtMob.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            Session["_isExsit"] = "false";
                            Session["_isGroup"] = "true";
                            // btnCreate.Text = "Create";
                            //   Session["Load"] = "Loaded";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                            Session["_isGroup"] = "false";
                        }
                        _isExsit = false;
                        Session["_isExsit"] = "false";
                        //btnCreate.Text = "Create";
                    }
                }
            }
        }
        protected void txtMob_TextChanged(object sender, EventArgs e)
        {
           MobileNoVerification();
        }

        protected void txtPerAdd1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtPerAdd1.Text))
            {
                ErrorMsg("Invalid charactor found in permenent address line 1.");
                txtPerAdd1.Focus();
                return;
            }
            txtPreAdd1.Text = txtPerAdd1.Text.Trim();
        }

        protected void txtPerAdd2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtPerAdd2.Text))
            {
                ErrorMsg("Invalid charactor found in permenent address line 2.");
                txtPerAdd2.Focus();
                return;
            }
            txtPreAdd2.Text = txtPerAdd2.Text.Trim();
        }

        protected void txtPerTown_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPerTown.Text))
            {
                ErrorMsg("Invalid charactor found in town code.");
                txtPerTown.Focus();
                return;
            }
            txtPerDistrict.Text = "";
            txtPerProvince.Text = "";
            txtPerPostal.Text = "";
            txtPerCountry.Text = "";

            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dt = new DataTable();
                _basePage = new Base();
                dt = _basePage.CHNLSVC.General.Get_DetBy_town(txtPerTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtPerDistrict.Text = district;
                        txtPerProvince.Text = province;
                        txtPerPostal.Text = postalCD;
                        txtPerCountry.Text = countryCD;

                        txtPreTown.Text = txtPerTown.Text; // Added by Nadeeka 29-05-2015 Requested By Dilanda

                    }
                    else
                    {
                        ErrorMsg("Invalid town.");
                        //MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerTown.Text = "";
                        txtPerTown.Focus();
                    }
                }
                else
                {
                    ErrorMsg("Invalid town.");
                    //MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerTown.Text = "";
                    txtPerTown.Focus();
                }
            }
        }

        protected void txtPerEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerEmail.Text))
            {
                Boolean _isValid = IsValidEmail(txtPerEmail.Text.Trim());

                if (_isValid == false)
                {
                    ErrorMsg("Invalid Permenent email address.");
                    //MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerEmail.Text = "";
                    txtPerEmail.Focus();
                    return;
                }
            }
        }

        protected void txtPrePhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPrePhone.Text))
            {
                ErrorMsg("Invalid charactor found in phone.");
                txtPrePhone.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtPerPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());

                if (_isvalid == false)
                {
                    ErrorMsg("Invalid Present phone number.");
                    //MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerPhone.Text = "";
                    txtPerPhone.Focus();
                    return;
                }
            }
        }

        protected void txtWorkPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtWorkPhone.Text.Trim());

                if (_isvalid == false)
                {
                    ErrorMsg("Invalid Work phone number.");

                    txtWorkPhone.Text = "";
                    txtWorkPhone.Focus();
                    return;
                }
            }
        }

        protected void txtPerPhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPerPhone.Text))
            {
                ErrorMsg("Invalid charactor found in phone no.");
                txtPerPhone.Focus();
                return;
            }
            txtPrePhone.Text = txtPerPhone.Text;
            if (!string.IsNullOrEmpty(txtPerPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());

                if (_isvalid == false)
                {
                    ErrorMsg("Invalid Permenent phone number.");
                    
                    txtPerPhone.Text = "";
                    txtPrePhone.Text = "";
                    txtPerPhone.Focus();
                    return;
                }
            }
        }

      

        protected void txtWorkFax_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkFax.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtWorkFax.Text.Trim());

                if (_isvalid == false)
                {
                    ErrorMsg("Invalid work Fax number.");
                   
                    txtWorkFax.Text = "";
                    txtWorkFax.Focus();
                    return;
                }
            }
        }

        protected void txtWorkEmail_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtWorkEmail.Text))
            //{
            //    Boolean _isValid = IsValidEmail(txtWorkEmail.Text.Trim());

            //    if (_isValid == false)
            //    {
            //        ErrorMsg("Invalid work email address.");
                    
            //        txtWorkEmail.Text = "";
            //        txtWorkEmail.Focus();
            //        return;
            //    }
            //}
            if (!string.IsNullOrEmpty(txtWorkEmail.Text))
            {
                Boolean _isValid = IsValidEmail(txtWorkEmail.Text.Trim());
                if (_isValid == false)
                {
                    ErrorMsg("Invalid Permenent email address.");
                    txtWorkEmail.Text = "";
                    txtWorkEmail.Focus();
                    return;
                }
            }
        }

        protected void chkVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVAT.Checked == true)
            {
                txtVatreg.Enabled = true;
                chkVatEx.Enabled = true;
                chkSVAT.Enabled = true;
                txtVatreg.Focus();
            }
            else
            {
                chkVatEx.Checked = false;
                chkVatEx.Enabled = false;
                chkSVAT.Checked = false;
                chkSVAT.Enabled = false;

                txtSVATReg.Enabled = false;
                txtVatreg.Enabled = false;
                chkVatEx.Enabled = true;
            }
        }

        protected void txtPerCountry_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPerCountry.Text))
            {
                ErrorMsg("Invalid charactor found in country code.");
                txtPerCountry.Focus();
                return;
            }
            txtPreCountry.Text = txtPerCountry.Text;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _basePage = new Base();
                string _cusCode = "";
                Int32 _effect = 0;
                if (Session["confmbox"] as string== "true")
               {
                   return;
               }
                if (ValidateSave())
                {                 
                
                Collect_Cust();
                Collect_GroupCust();

                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }
                _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                if (_isExsit == false)
                {

                    _effect = _basePage.CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList, busItemList, out _cusCode, null, _isExsit, _isGroup, _custGroup);
               
                }
                else
                {
                    _cusCode = txtCusCode.Text.Trim();
                    _effect = _basePage.CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList,null,busItemList, _custGroup);
                }
                if (_effect == 1)
                {
                    _isExsit = Convert.ToBoolean(Session["_isExsit"].ToString());
                    if (_isExsit == false)
                    {
                        SuccessMsg("New customer created. Customer Code : " + _cusCode);
                        //MessageBox.Show("New customer created. Customer Code : " + _cusCode, "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        SuccessMsg("Exsiting customer updated.");
                        //MessageBox.Show("Exsiting customer updated.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_cusCode))
                    {
                        ErrorMsg(_cusCode);
                       // MessageBox.Show(_cusCode, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        ErrorMsg("Creation Fail.");
                       // MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                _isFromOther = Convert.ToBoolean(Session["_isFromOther"].ToString());

                if (_isFromOther == true)
                {
                    //HP.AccountCreation _acc = new HP.AccountCreation();
                    //obj_TragetTextBox.Text = _cusCode;
                    Session["_cusCode"] = _cusCode;
                    Session["c"] = "false";
                    //this.Close();
                    UserPopoup.Hide();
                }
                clearPage();
            }
               // clearPage();
            }
            catch (Exception err)
            {

                _basePage.CHNLSVC.CloseChannel();
                ErrorMsg(err.Message);
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidateSave()
        {
            List<MasterBusinessEntity> list = new List<MasterBusinessEntity>();
            _basePage = new Base();

            if ((((this.txtNIC.Text == "") && (this.txtBR.Text == "")) && ((this.txtPP.Text == "") && (this.txtDL.Text == ""))) && (this.txtMob.Text == ""))
            {
                this.ErrorMsg("One of required information not enterd.[NIC/BR/PP/DL/MOB]");
                this.txtNIC.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.txtWorkEmail.Text) && !IsValidEmail(this.txtWorkEmail.Text.Trim()))
            {
                this.ErrorMsg("Invalid work email address.");
                this.txtWorkEmail.Text = "";
                this.txtWorkEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.ErrorMsg("Please enter name of customer");
                this.txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPerAdd1.Text))
            {
                this.ErrorMsg("Please enter customer present address.");
                this.txtPerAdd1.Focus();
                return false;
            }
            if (this.chkVAT.Checked && string.IsNullOrEmpty(this.txtVatreg.Text))
            {
                this.ErrorMsg("Please enter VAT reg. number.");
                this.txtVatreg.Focus();
                return false;
            }
            if (this.chkSVAT.Checked && string.IsNullOrEmpty(this.txtSVATReg.Text))
            {
                this.ErrorMsg("Please enter SVAT reg. number.");
                this.txtSVATReg.Focus();
                return false;
            }
            if (this.chkMail.Checked && string.IsNullOrEmpty(this.txtPerEmail.Text))
            {
                this.ErrorMsg("Please enter Email.");
                this.txtPerEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPerTown.Text))
            {
                this.ErrorMsg("Please enter town");
                this.txtPerTown.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(this.txtMob.Text))
            //{
            //    this.ErrorMsg("Please enter mobile #");
            //    this.txtMob.Focus();
            //    return false;
            //}
            if (this.chkSVAT.Checked)
            {
                MasterCompany compByCode = new MasterCompany();
                compByCode = this._basePage.CHNLSVC.General.GetCompByCode(base.Session["UserCompanyCode"].ToString());
                if (compByCode.Mc_cd == null)
                {
                    this.ErrorMsg("Cannot find company details.Please re-try.");
                    return false;
                }
                if (string.IsNullOrEmpty(compByCode.Mc_tax2))
                {
                    this.ErrorMsg("Cannot create SVAT customer under your current login company.");
                    return false;
                }
            }
            list = this._basePage.CHNLSVC.Sales.GetCustomerByKeys(Session["UserCompanyCode"].ToString(), this.txtNIC.Text.Trim(), "", "", "", "", 1);
            if (((list != null) && (list.Count >= 1)) && (this.txtNIC.Text.ToUpper() != "N/A"))
            {
            }
            list = null;
            list = this._basePage.CHNLSVC.Sales.GetCustomerByKeys(Session["UserCompanyCode"].ToString(), "", this.txtMob.Text.Trim(), "", "", "", 1);
            if (((list != null) && (list.Count >= 1)) && (this.txtMob.Text.ToUpper() != "N/A"))
            {
            }
            return true;
        }



        protected void lbtnclose_Click(object sender, EventArgs e)
        {
            ClearMsg();
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            clearPage();
        }

        protected void chkSVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSVAT.Checked == true)
            {
                txtSVATReg.Enabled = true;
                txtSVATReg.Focus();
            }
            else
            {
                txtSVATReg.Enabled = false;
            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            string _mobi = txtMob.Text;
            Int32 cutstVeri = _basePage.CHNLSVC.General.GetCutomerValidationCode(_mobi);
            Int32 _attemt = 0;
            Int32 _enteredCode;
        err:
            _enteredCode = Convert.ToInt32(txtVcode.Text);

            if (cutstVeri == _enteredCode)
            {
                _eff = _basePage.CHNLSVC.General.UpdateCutomerMobile(txtPP.Text.Trim().ToUpper(), "PP", out _err);
                if (_eff == -1)
                {
                    ErrorMsg(_err);
                    // MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //btnCreate_Click(null, null);
                lbtnSave_Click(null, null);
            }
            else
            {
                _attemt = Convert.ToInt32(Session["_attemt"].ToString());
                ErrorMsg("Invalid varification code.");
                // MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_attemt == 5)
                {

                    return;
                }
                else
                {

                    _attemt = _attemt + 1;
                    Session["_attemt"] = _attemt;
                    userconfmbox.Show();
                    goto err;
                }
            }




           
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {

            _isExsit = true;
            Session["_isExsit"] = _isExsit;

            // btnCreate.Text = "Update";
           // MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
            MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
            if (custProf.Mbe_cd == null)
            {
                return;
            }
            LoadCustProf(custProf, _isExtendedPage);

            //load extended page details
            if (_isExtendedPage == true)
            {
                GetCustomerDetailsExtended(_isExtendedPage);
                return;
            }

            userconfmbox.Hide();
        }

        private void loadCutTypes()
        {
            List<ComboBoxObject> oItems = new List<ComboBoxObject>();
            ComboBoxObject o1 = new ComboBoxObject();
            o1.Text = "INDIVIDUAL";
            o1.Value = "0";
            oItems.Add(o1);

            ComboBoxObject o2 = new ComboBoxObject();
            o2.Text = "GROUP";
            o2.Value = "0";
            oItems.Add(o2);


            ComboBoxObject o3 = new ComboBoxObject();
            o3.Text = "LEASE";
            o3.Value = "3";
            oItems.Add(o3);

            ddlCtype.DataSource = oItems;
            ddlCtype.DataTextField = "Text";
            ddlCtype.DataValueField = "Value";
            ddlCtype.DataBind();
        }

        public void VisbleButttons()
        {
            lbtnSave.Visible = false;
            lbtnClear.Visible = false;
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

        protected void txtInit_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtInit.Text))
            {
                ErrorMsg("Invalid charactor found in initials.");
                txtInit.Focus();
                return;
            }
        }

        protected void txtFname_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtFname.Text))
            {
                ErrorMsg("Invalid charactor found in first name.");
                txtFname.Focus();
                return;
            }
        }

        protected void txtSName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtSName.Text))
            {
                ErrorMsg("Invalid charactor found in surname.");
                txtSName.Focus();
                return;
            }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtName.Text))
            {
                ErrorMsg("Invalid charactor found in full name.");
                txtName.Focus();
                return;
            }
        }

        protected void txtPerPostal_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPerPostal.Text))
            {
                ErrorMsg("Invalid charactor found in postal code.");
                txtPerPostal.Focus();
                return;
            }
        }

        protected void txtPerProvince_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPerProvince.Text))
            {
                ErrorMsg("Invalid charactor found in province code.");
                txtPerProvince.Focus();
                return;
            }
        }

        protected void txtPreAdd1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtPreAdd1.Text))
            {
                ErrorMsg("Invalid charactor found in present address line 1.");
                txtPreAdd1.Focus();
                return;
            }
        }

        protected void txtPreAdd2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtPreAdd2.Text))
            {
                ErrorMsg("Invalid charactor found in present address line 2.");
                txtPreAdd2.Focus();
                return;
            }
        }

        protected void txtPreDistrict_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPreDistrict.Text))
            {
                ErrorMsg("Invalid charactor found in district code.");
                txtPreDistrict.Focus();
                return;
            }
        }

        protected void txtPrePostal_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPrePostal.Text))
            {
                ErrorMsg("Invalid charactor found in postal code.");
                txtPrePostal.Focus();
                return;
            }
        }

        protected void txtPreProvince_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPreProvince.Text))
            {
                ErrorMsg("Invalid charactor found in province code.");
                txtPreProvince.Focus();
                return;
            }
        }

        protected void txtPreCountry_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPreCountry.Text))
            {
                ErrorMsg("Invalid charactor found in country code.");
                txtPreCountry.Focus();
                return;
            }
        }

        protected void txtWorkName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtWorkName.Text))
            {
                ErrorMsg("Invalid charactor found in working place name.");
                txtWorkName.Focus();
                return;
            }
        }

        protected void txtWorkAdd1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtWorkAdd1.Text))
            {
                ErrorMsg("Invalid charactor found in working place address line 1.");
                txtWorkAdd1.Focus();
                return;
            }
        }

        protected void txtWorkAdd2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtWorkAdd2.Text))
            {
                ErrorMsg("Invalid charactor found in working place address line 2.");
                txtWorkAdd2.Focus();
                return;
            }
        }

        protected void txtWorkDept_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtWorkDept.Text))
            {
                ErrorMsg("Invalid charactor found in department.");
                txtWorkDept.Focus();
                return;
            }
        }

        protected void txtWorkDesig_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtWorkDesig.Text))
            {
                ErrorMsg("Invalid charactor found in designation.");
                txtWorkDesig.Focus();
                return;
            }
        }

        protected void txtVatreg_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtVatreg.Text))
            {
                ErrorMsg("Invalid charactor found in vat reg no.");
                txtVatreg.Focus();
                return;
            }
        }

        protected void txtSVATReg_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtSVATReg.Text))
            {
                ErrorMsg("Invalid charactor found in svat reg no.");
                txtSVATReg.Focus();
                return;
            }
        }

        protected void txtPerDistrict_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtPerDistrict.Text))
            {
                ErrorMsg("Invalid charactor found in distric code.");
                txtPerDistrict.Focus();
                return;
            }
        }
        private string LoadAccountNumber(string customerCode)
        {
            string accountNo = _basePage.CHNLSVC.Sales.GetAccountNo(customerCode, Session["UserCompanyCode"].ToString());
            return accountNo;
        }
    }
}