using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.Sales
{
    public partial class TransportEnquiryManagement : BasePage
    {
        List<RecieptItemTBS> recieptItemList;
        private List<RecieptItemTBS> _recieptItemTbs;
        private List<RecieptItem> _recieptItems;
        private BasePage _basePage;
        private decimal _paidAmount;
        private List<InvoiceItem> oMainInvoiceItems = null;
        private InvoiceHeader oHeader = new InvoiceHeader();
        private List<RecieptItem> _recieptItem = new List<RecieptItem>();

        protected List<GEN_CUST_ENQ> oGEN_CUST_ENQs { get { return (List<GEN_CUST_ENQ>)Session["oGEN_CUST_ENQs"]; } set { Session["oGEN_CUST_ENQs"] = value; } }

        public List<RecieptItemTBS> RecieptItemList
        {
            get { return recieptItemList; }
            set { recieptItemList = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                       !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                       !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                       !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {
                        dgvReq.DataSource=new int[] { };
                        dgvHistry.DataSource = new int[] { };
                        dgvChargeItems.DataSource = new int[] { };

                        dgvReq.DataBind();
                        dgvHistry.DataBind();
                        dgvChargeItems.DataBind();
                        
                        loadVehicleTypes();
                        loadPaymenttypes();
                       LoadEnqSubTp();
                        loadData();
                        oMainInvoiceItems = new List<InvoiceItem>();
                        Session["ShowUcVehicleInquiry"] = "N";
                        Session["oMainInvoiceItems"] = oMainInvoiceItems;
                        txtReturnDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                        txtRequestDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        string s = (string)Session["ShowUcVehicleInquiry"];
                        if (s == "Y")
                        {
                            //  string s = Session["ShowUcDocument"].ToString();
                            PopupEnquery.Show();
                        }
                        else
                        {
                            Session["ShowUcVehicleInquiry"] = "N";
                            //PopupEnquery.Hide();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadData()
        {
            string Status = "0,1,2,3,4,5,6,7,8";

            List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Status, Session["UserID"].ToString(), 15001);
            if (oItems.Count > 0)
            {
                dgvReq.DataSource = oItems;
                Session["Data"] = oItems;
                dgvReq.DataBind();
               // modifyGrid();
                ViewState["oItems"] = oItems;
            }
            else
            {
                dgvReq.DataSource = new int[] { }; ;
                Session["Data"] = null;
                dgvReq.DataBind();
                // modifyGrid();
                ViewState["oItems"] = null;
            }
        }
        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            //loadToSesstion();
            Response.Redirect("~/DataEnty/CustomerCreation.aspx");
        }
        protected void btnHistory_Click(object sender, EventArgs e)
        {
            List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_CUST(Session["UserCompanyCode"].ToString(), txtCustomerCode.Text);
            if (oItems.Count > 0)
            {
                dgvHistry.DataSource = oItems;
                dgvHistry.DataBind();
                mpBatchConfirmationOne.Show();

                for (int i = 0; i < dgvHistry.Rows.Count; i++)
                {
                    GridViewRow row = dgvHistry.Rows[i];
                    Label lblExpectDate = (Label)row.FindControl("lblExpectDate");
                    lblExpectDate.Text = Convert.ToDateTime(lblExpectDate.Text).ToString("dd/MMM/yyyy");
                    Label lbltDate = (Label)row.FindControl("lbltDate");
                    lbltDate.Text = Convert.ToDateTime(lbltDate.Text).ToString("dd/MMM/yyyy");
                }
            }
            else
            {
                DisplayMessages("No records exists");
            }
        }
        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCustomerCode_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCustomerCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.ToursFacCompany:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlFacility.SelectedValue.ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + ddlPayMode.SelectedItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + ddlVehicleType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Drivers:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "TRANS" + seperator);
                        break;
                    }
               
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpBatchConfirmationOne.Hide();
        }
        protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            txtMobile.Text = "";
            txtNIC.Text = "";
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                //btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyCustCD(txtCustomerCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    //btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    //btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit.Value == "1")
                    {
                        string cusCD = txtCustomerCode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtCustomerCode.Text = cusCD;
                    }
                }
            }
        }
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }
        public void LoadCustProf(MasterBusinessEntity cust)
        {
            txtNIC.Text = cust.Mbe_nic;
            txtCustomerCode.Text = cust.Mbe_cd;
            txtMobile.Text = cust.Mbe_mob;
            txtName.Text = cust.Mbe_name;
            //txtEmail.Text = cust.Mbe_email;
            txtAddress1.Text = cust.Mbe_add1;
            txtAddress2.Text = cust.Mbe_add2;
            txtCustomerCode.Focus();
        }
        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessages("Invalid phone number.");
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
            }
        }
        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                //btnCreate.Enabled = true;
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessages("Invalid NIC.");
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    //check multiple Add By Chamal 24/04/2014
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(Session["UserCompanyCode"].ToString(), txtNIC.Text.Trim(), "", "", "", "", 1);
                    if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    {
                        string _custNIC = "Duplicate customers found!\n";
                        //foreach (var _nicCust in _custList)
                        //{
                        //    _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        //}
                        DisplayMessages(_custNIC);
                        txtNIC.Text = "";
                        txtNIC.Focus();
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit.Value = "1";
                        //btnCreate.Text = "Update";
                        LoadCustProf(custProf);
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                        //btnCreate.Enabled = false;
                        LoadCustProf(custProf);
                    }
                    else//added on 01/10/2012
                    {
                        if (_isExsit.Value == "1")
                        {
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtNIC.Text = nic;
                        }

                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup.Value = "1";
                            _isExsit.Value = "0";
                            //btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup.Value = "0";
                        }

                        _isExsit.Value = "0";
                        //btnCreate.Text = "Create";
                    }
                }
            }
        }
        public GroupBussinessEntity GetbyNICGrup(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }
        public void LoadCustProfByGrup(GroupBussinessEntity _cust)
        {
            txtNIC.Text = _cust.Mbg_nic;
            txtCustomerCode.Text = _cust.Mbg_cd;
            txtMobile.Text = _cust.Mbg_mob;
            txtName.Text = _cust.Mbg_name;
            txtAddress1.Text = _cust.Mbg_add1;
            txtAddress2.Text = _cust.Mbg_add2;
        }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, Session["UserCompanyCode"].ToString());
        }
        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;

            //if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            //{
            //    MultiView1.ActiveViewIndex = 1;
            //}
            //else if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            //{
            //    MultiView1.ActiveViewIndex = 2;
            //}
            //else if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
            //{
            //    MultiView1.ActiveViewIndex = 3;
            //}
        }
        protected void btnPayAdd_Click(object sender, EventArgs e)
        {
           // AddPayment();
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int result = 0;
            bool isinvoicvesave = false;
            if (!validateSave())
            {
                return; 
            }
            #region saveEnq
            MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = null;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "AT";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "AT";
            _ReqInsAuto.Aut_year = DateTime.Today.Year;

            MasterAutoNumber oMainReq = new MasterAutoNumber();
            oMainReq.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            oMainReq.Aut_cate_tp = "PC";
            oMainReq.Aut_direction = null;
            oMainReq.Aut_modify_dt = null;
            oMainReq.Aut_moduleid = "ATMN";
            oMainReq.Aut_number = 0;
            oMainReq.Aut_start_char = "ATMN";
            oMainReq.Aut_year = DateTime.Today.Year;

            string err;
            GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
            oItem.GCE_SEQ = Convert.ToInt32(lblSeqNum.Text);
            oItem.GCE_ENQ_ID = txtEnquiryID.Text;
            oItem.GCE_REF = txtReference.Text;
            oItem.GCE_ENQ_TP = "TNSPT";
            oItem.GCE_COM = (string)Session["UserCompanyCode"];
            oItem.GCE_PC = (string)Session["UserDefProf"];
            oItem.GCE_DT = DateTime.Now.Date;
            oItem.GCE_CUS_CD = txtCustomerCode.Text;
            oItem.GCE_NAME = txtName.Text;
            oItem.GCE_ADD1 = txtAddress1.Text;
            oItem.GCE_ADD2 = txtAddress2.Text;
            oItem.GCE_MOB = txtMobile.Text;
            MasterBusinessEntity custProf = GetbyCustCD(txtCustomerCode.Text.Trim().ToUpper());
            oItem.GCE_EMAIL = custProf.Mbe_email;
            oItem.GCE_NIC = txtNIC.Text;
            TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
            DateTime date = Convert.ToDateTime(txtRequestDate.Text).Add(time);
            oItem.GCE_EXPECT_DT = date; 
            //oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtRequestDate.Text);
            oItem.GCE_SER_LVL = string.Empty;
            oItem.GCE_ENQ = txtRemarks.Text;
            oItem.GCE_ENQ_COM = Session["UserCompanyCode"].ToString();
            oItem.GCE_ENQ_PC = Session["UserDefProf"].ToString();
            MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            oItem.GCE_ENQ_PC_DESC = (oPc != null) ? oPc.Mpc_desc : string.Empty;
            oItem.GCE_STUS = 1;
            oItem.GCE_CRE_BY = Session["UserID"].ToString();
            oItem.GCE_CRE_DT = DateTime.Now;
            oItem.GCE_MOD_BY = Session["UserID"].ToString();
            oItem.GCE_MOD_DT = DateTime.Now;

            List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(Session["UserCompanyCode"].ToString(), "Transport");
            if (oMST_FACBY != null && oMST_FACBY.Count > 0 && oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC).Count > 0)
            {
                oItem.Gce_bill_cuscd = oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC)[0].MFB_BILL_CD;
                //oItem.Gce_bill_cusname = oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC)[0].MFB_;
            }
            else
            {
                oItem.Gce_bill_cuscd = oItem.GCE_CUS_CD;
                oItem.Gce_bill_cusname = oItem.Gce_bill_cusname;
            }

            oItem.GCE_FRM_TN = txtPTown.Text;
            oItem.GCE_TO_TN = tXtDTown.Text;
            oItem.GCE_FRM_ADD = txtPAddress.Text;
            oItem.GCE_TO_ADD = txtDAddress.Text;
            if (!string.IsNullOrEmpty(txtNoOfPassengers.Text))
            {
                oItem.GCE_NO_PASS = Convert.ToInt32(txtNoOfPassengers.Text);
            }
            oItem.GCE_VEH_TP = ddlVehicleType.SelectedValue.ToString();
            time = new TimeSpan(0, tmReturn.Hour, tmReturn.Minute, 0);
            date = Convert.ToDateTime(txtReturnDate.Text).Add(time);
            oItem.GCE_RET_DT = date;
            //oItem.GCE_RET_DT = Convert.ToDateTime(txtReturnDate.Text);
            oItem.GCE_REF = txtReference.Text;
            oItem.GCE_FLEET = txtVehicle.Text;
            oItem.GCE_DRIVER = txtDriver.Text;

            oItem.GCE_CONT_PER = txtContactPerson.Text;
            oItem.GCE_CONT_MOB= txtContactMobile.Text;
            oItem.GCE_REQ_NO_VEH =string.IsNullOrEmpty(txtNoOfreqVehicle.Text)?0:Convert.ToInt32(txtNoOfreqVehicle.Text);
            oItem.GCE_ENQ_SUB_TP = ddlTripTp.SelectedValue;
          //  oGEN_CUST_ENQs.Add(oItem);
          //  result = CHNLSVC.Tours.SaveEnquiryRequestList(oGEN_CUST_ENQs, _ReqInsAuto, oMainReq, out err);
            #endregion
            #region save
            //MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

            _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = null;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "AT";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "AT";
            _ReqInsAuto.Aut_year = DateTime.Today.Year;
                isinvoicvesave = true;
                oItem.GCE_STUS = 5;

            #region Invoice

            if (Session["oHeader"] != null)
            {
                oHeader = (InvoiceHeader)Session["oHeader"];
            }
            else
            {
                oHeader = new InvoiceHeader();
            }

            oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];

            MasterBusinessEntity oCust = GetbyCustCD(txtCustomerCode.Text.Trim().ToUpper());
            oHeader.Sah_com = Session["UserCompanyCode"].ToString();
            oHeader.Sah_cre_by = Session["UserID"].ToString();
            oHeader.Sah_cre_when = DateTime.Now;
            oHeader.Sah_currency = "LKR";
            oHeader.Sah_cus_add1 = oCust.Mbe_add1;
            oHeader.Sah_cus_add2 = oCust.Mbe_add2;
            oHeader.Sah_cus_cd = oCust.Mbe_cd;
            oHeader.Sah_cus_name = oCust.MBE_FNAME;
            oHeader.Sah_d_cust_add1 = oCust.Mbe_add1;
            oHeader.Sah_d_cust_add2 = oCust.Mbe_add2;
            oHeader.Sah_d_cust_cd = oCust.Mbe_cd;
            oHeader.Sah_d_cust_name = oCust.MBE_FNAME;
            oHeader.Sah_direct = true;
            oHeader.Sah_dt = DateTime.Now.Date;
            oHeader.Sah_epf_rt = 0;
            oHeader.Sah_esd_rt = 0;
            oHeader.Sah_ex_rt = 1;
            oHeader.Sah_inv_no = "na";
            oHeader.Sah_inv_sub_tp = "SA";
            oHeader.Sah_inv_tp = ddlPayMode.SelectedValue.ToString();
            oHeader.Sah_is_acc_upload = false;
            oHeader.Sah_man_ref = txtReference.Text;
            oHeader.Sah_manual = false;
            oHeader.Sah_mod_by = Session["UserID"].ToString();
            oHeader.Sah_mod_when = DateTime.Now;
            oHeader.Sah_pc = Session["UserDefProf"].ToString();
            oHeader.Sah_pdi_req = 0;
            oHeader.Sah_ref_doc = txtEnquiryID.Text;
            oHeader.Sah_sales_chn_cd = "";
            oHeader.Sah_sales_chn_man = "";
            oHeader.Sah_sales_ex_cd = Session["UserID"].ToString();
            oHeader.Sah_sales_region_cd = "";
            oHeader.Sah_sales_region_man = "";
            oHeader.Sah_sales_sbu_cd = "";
            oHeader.Sah_sales_sbu_man = "";
            oHeader.Sah_sales_str_cd = "";
            oHeader.Sah_sales_zone_cd = "";
            oHeader.Sah_sales_zone_man = "";
            oHeader.Sah_seq_no = 1;
            oHeader.Sah_session_id = Session["SessionID"].ToString();
            // oHeader.Sah_structure_seq = txtQuotation.Text.Trim();
            oHeader.Sah_stus = "A";
            //  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) oHeader.Sah_stus = "D";
            oHeader.Sah_town_cd = "";
            oHeader.Sah_tp = "INV";
            oHeader.Sah_wht_rt = 0;
            oHeader.Sah_direct = true;
            oHeader.Sah_tax_inv = oCust.Mbe_is_tax;
            //oHeader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
            //oHeader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            oHeader.Sah_del_loc = string.Empty;
            //oHeader.Sah_grn_com = _customerCompany;
            //oHeader.Sah_grn_loc = _customerLocation;
            //oHeader.Sah_is_grn = _isCustomerHasCompany;
            //oHeader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            oHeader.Sah_is_svat = oCust.Mbe_is_svat;
            oHeader.Sah_tax_exempted = oCust.Mbe_tax_ex;
            oHeader.Sah_anal_2 = "SCV";
            oHeader.Sah_anal_3 = "";
            //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
            oHeader.Sah_man_cd = Session["UserDefProf"].ToString();
            oHeader.Sah_is_dayend = 0;
            oHeader.Sah_remarks = "Invoice generated by trip request";

            _recieptItems = (List<RecieptItem>)Session["_recieptItems"];
            if (_recieptItems == null)
            {
                _recieptItems = new List<RecieptItem>();
            }

            RecieptItem _item = new RecieptItem();

            _item.Sard_deposit_bank_cd = null;
            _item.Sard_deposit_branch = null;
            _item.Sard_pay_tp = "CS";
            _item.Sard_settle_amt = oMainInvoiceItems.Sum(x => x.Sad_tot_amt);

            _recieptItems.Add(_item);



            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = -1; //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
            _ReceiptHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
            _ReceiptHeader.Sar_receipt_type = "VHREG";
            // _ReceiptHeader.Sar_receipt_no = txtRecNo.Text.Trim();
            MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
            _RecDiv = CHNLSVC.Sales.GetDefRecDivision(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            if (_RecDiv.Msrd_cd != null)
            {
                _ReceiptHeader.Sar_prefix = _RecDiv.Msrd_cd;
            }
            else
            {
                _ReceiptHeader.Sar_prefix = "";
            }
            //_ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            // _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
            _ReceiptHeader.Sar_receipt_date = DateTime.Now.Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = oCust.Mbe_cd;
            _ReceiptHeader.Sar_debtor_name = oCust.MBE_FNAME;
            _ReceiptHeader.Sar_debtor_add_1 = oCust.Mbe_add1;
            _ReceiptHeader.Sar_debtor_add_2 = oCust.Mbe_add2;
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = oCust.Mbe_mob;
            _ReceiptHeader.Sar_nic_no = oCust.Mbe_nic;
            //if (ddlInvoiceType.Text != "CRED")
            {
                _ReceiptHeader.Sar_tot_settle_amt = _recieptItem.Sum(x => x.Sard_settle_amt);
            }
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            // _ReceiptHeader.Sar_remarks = txtNote.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = "";
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;
            _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
            _ReceiptHeader.Sar_anal_1 = oCust.Mbe_cr_distric_cd;
            _ReceiptHeader.Sar_anal_2 = oCust.Mbe_cr_province_cd;

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _invoiceAuto.Aut_cate_tp = "TINVO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CS";
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = "TINVO";
            _invoiceAuto.Aut_year = DateTime.Now.Date.Year;

            MasterAutoNumber _receiptAuto = null;
            if (_recieptItem != null)
                if (_recieptItem.Count > 0)
                {
                    _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                    _receiptAuto.Aut_cate_tp = "PRO";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "DIR";
                    _receiptAuto.Aut_year = DateTime.Now.Date.Year;
                }

            string _invoiceNo;
            string _receiptNo;
            string _deliveryOrder;
            string _error;
            string _buybackadj;

            List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();
            InventoryHeader _inventoryHeader = new InventoryHeader();
            List<ReptPickSerials> _pickSerial = new List<ReptPickSerials>();
            List<ReptPickSerialsSub> _pickSubSerial = new List<ReptPickSerialsSub>();

            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            List<InvoiceVoucher> _voucher = new List<InvoiceVoucher>();
            InventoryHeader _buybackheader = new InventoryHeader();
            MasterAutoNumber _buybackauto = new MasterAutoNumber();
            List<ReptPickSerials> _buybacklist = new List<ReptPickSerials>();

            _basePage = new BasePage();

            #endregion

            //string err;

            result = _basePage.CHNLSVC.Tours.SaveTripRequestWithInvoice(oHeader, oMainInvoiceItems, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, oItem, _ReqInsAuto, out err, isinvoicvesave);
            if (result == 1)
            {
                if (!string.IsNullOrEmpty(txtDriver.Text))
                {
                    MST_EMPLOYEE_TBS driver = CHNLSVC.Tours.Get_mst_employee(txtDriver.Text);
                    OutSMS _out = new OutSMS();
                    _out.Msg = "Your vehicle details as follows.  " +
                     "\nVehicle # : " + txtVehicle.Text +
                     "\nDriver Name : " + txtDriverName.Text +
                     "\nDriver contact # : " + txtDriverName.Text;
                    _out.Msgstatus = 0;
                    _out.Msgtype = "S";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = "";
                    _out.Receiverphno = driver.MEMP_MOBI_NO;
                    // _out.Receiverphno = "+94712115036";
                    _out.Senderphno = "+94712115036";
                    _out.Refdocno = "0";
                    _out.Sender = "Message Agent";
                    _out.Createtime = DateTime.Now;

                    int smsResult = CHNLSVC.General.SaveSMSOut(_out);
                }
                if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                {
                   // MasterBusinessEntity custProf = GetbyCustCD(txtCustomer.Text.Trim().ToUpper());
                    OutSMS _out = new OutSMS();
                    _out.Msg = "Your vehicle details as follows.  " +
                     "\nVehicle # : " + txtVehicle.Text +
                     "\nDriver Name : " + txtDriverName.Text +
                     "\nDriver contact # : " + txtDriverName.Text;
                    _out.Msgstatus = 0;
                    _out.Msgtype = "S";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = "";
                    _out.Receiverphno = custProf.Mbe_mob;
                    // _out.Receiverphno = "+94712115036";
                    _out.Senderphno = "+94712115036";
                    _out.Refdocno = "0";
                    _out.Sender = "Message Agent";
                    _out.Createtime = DateTime.Now;
                    int smsResult = CHNLSVC.General.SaveSMSOut(_out);
                }
            }
            if (result > 0)
            {
                string msg;
                if (isinvoicvesave)
                {
                    msg = "Successfully saved. Enquiry No :" + err + "  Invoice No :" + _invoiceNo + "  Receipt No :" + _receiptNo;
                }
                else
                {
                    if (!string.IsNullOrEmpty(err))
                    {
                        msg = "Successfully saved. Enquiry Number :" + err;
                    }
                    else
                    {
                        msg = "Successfully Updated.";
                    }
                }

                DisplayMessages(msg);
                clearAll();
            }
            else
            {
                DisplayMessages("Error Occurred." + err);
            }
            clearAll();
            #endregion
        }
        private bool validateSave()
        {
            bool status = true;
            #region enquiry
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                DisplayMessages("Please enter reference num");
                status = false;
                return status;
            }
            else if (Convert.ToDateTime(txtReturnDate.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please select a valid date");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                DisplayMessages("Please select a customer code");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                DisplayMessages("Please enter enquiry");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtPTown.Text))
            {
                DisplayMessages("Please enter picked up town");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(tXtDTown.Text))
            {
                DisplayMessages("Please enter drop town");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                DisplayMessages("Please enter address 1");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtAddress2.Text))
            {
                DisplayMessages("Please enter address 2");
                status = false;
                return status;
            }
            else if (string.IsNullOrEmpty(txtNoOfPassengers.Text))
            {
                DisplayMessages("Please enter number of passengers");
                status = false;
                return status;
            }
            else if (!isdecimal(txtNoOfPassengers.Text))
            {
                DisplayMessages("Please enter valid number of passengers");
                status = false;
                return status;
            }
            else if (Convert.ToDecimal(txtNoOfPassengers.Text) < 0)
            {
                DisplayMessages("Please enter valid number of passengers");
                status = false;
                return status;
            }

            else if (string.IsNullOrEmpty(txtNoOfreqVehicle.Text))
            {
                DisplayMessages("Please enter number of request vehicles");
                status = false;
                return status;
            }
            else if (!isInteger(txtNoOfreqVehicle.Text))
            {
                DisplayMessages("Please enter valid number of request vehicles");
                status = false;
                return status;
            }
            else if (Convert.ToInt32(txtNoOfreqVehicle.Text) < 1)
            {
                DisplayMessages("Please enter valid number of request vehicles");
                status = false;
                return status;
            }

            else if (string.IsNullOrEmpty(txtReturnDate.Text))
            {
                DisplayMessages("Please select a return date");
                status = false;
                return status;
            }
            else if (Convert.ToDateTime(txtReturnDate.Text) < DateTime.Now.Date)
            {
                DisplayMessages("Please select a valid return date");
                status = false;
                return status;
            }
            #endregion
            #region Allocation
            MasterBusinessEntity custProf = GetbyCustCD(txtCustomerCode.Text.Trim().ToUpper());
            if (custProf == null || string.IsNullOrEmpty(custProf.Mbe_cd))
            {
                DisplayMessages("please enter a valid Customer code");
                status = false;
                return status;
            }
            if (Convert.ToDateTime(txtRequestDate.Text) > Convert.ToDateTime(txtReturnDate.Text))
            {
                DisplayMessages("Please select valid date range");
                status = false;
                return status;
            }
            #endregion
            return status;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEnquiryID.Text))
            {
                GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
                if (oItem != null)
                {
                    if (oItem.GCE_STUS == 0)
                    {
                        DisplayMessages("Enquiry is already canceled.");
                        return;
                    }
                    else if (oItem.GCE_STUS >= 2)
                    {
                        DisplayMessages("Enquiry can not cancel in this stage.");
                        return;
                    }
                    else if (oItem.GCE_STUS == 1)
                    {
                        String err = string.Empty;
                        Int32 result = CHNLSVC.Tours.UpdateEnquiryStageWithlog(0, Session["UserID"].ToString(), txtEnquiryID.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), out err);
                        if (result > 0)
                        {
                            DisplayMessages("Enquiry canceled.");
                            clearAll();
                            return;
                        }
                        else
                        {
                            DisplayMessages("Process Terminated. " + err);
                            return;
                        }
                    }
                }
            }
            else
            {
                DisplayMessages("Please select enquiry");
                return;
            }
        }

        private void loadVehicleTypes()
        {
            List<ComboBoxObject> oVehicleTypes = CHNLSVC.Tours.GET_ALL_VEHICLE_FOR_COMBO();
            ddlVehicleType.DataSource = oVehicleTypes;
            ddlVehicleType.DataTextField = "Text";
            ddlVehicleType.DataValueField = "Value";
            ddlVehicleType.DataBind();
        }
        private void LoadEnqSubTp()
        {
            List<MST_ENQSUBTP> domains = CHNLSVC.Tours.GET_ENQRY_SUB_TP(new MST_ENQSUBTP()
            { 
                MEST_COM=(string)Session["UserCompanyCode"],
                MEST_TPCD="TNSPT"
            });
            ddlTripTp.DataSource = domains;
            ddlTripTp.DataTextField = "mest_desc";
            ddlTripTp.DataValueField = "mest_stpcd";
            ddlTripTp.DataBind();
        }
        protected void ImgAmount_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString()))
                {
                    DisplayMessages("Please select the valid payment type.");
                    return;
                }
                if (String.IsNullOrEmpty(txtAmount.Text))
                {
                    DisplayMessages("Please select the valid pay amount.");
                    return;
                }
                if (String.IsNullOrEmpty(txtAmount.Text))
                {
                    DisplayMessages("Amount is empty");
                    return;
                }
                
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    if (String.IsNullOrEmpty(txtChequeNo.Text))
                    {
                        DisplayMessages("ChequeNo is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCheque.Text))
                    {
                        DisplayMessages("Bank is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCheque.Text))
                    {
                        DisplayMessages("Branch is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtChequeDate.Text))
                    {
                        DisplayMessages("Cheque Date is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCheque.Text))
                    {
                        DisplayMessages("Deposit Bank is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCheque.Text))
                    {
                        DisplayMessages("Deposit Branch is required.");
                        return;
                    }
                }
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    if (String.IsNullOrEmpty(txtCardNo.Text))
                    {
                        DisplayMessages("Card No is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCard.Text))
                    {
                        DisplayMessages("Bank is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCard.Text))
                    {
                        DisplayMessages("Branch is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCard.Text))
                    {
                        DisplayMessages("Deposit Bank is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCard.Text))
                    {
                        DisplayMessages("Branch Card is required.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtPeriod.Text))
                    {
                        DisplayMessages("Branch is required.");
                        return;
                    }
                }

                AddPayment();
                recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
                LoadRecieptGrid();

                txtChequeNo.Text = string.Empty;
                txtBranchCheque.Text = String.Empty;
                txtBankCheque.Text = String.Empty;
                txtChequeDate.Text = String.Empty;
                txtDepositBranchCheque.Text = string.Empty;
                txtDepositBankCheque.Text = string.Empty;

                lblPromotion.Text = string.Empty;
                lblbank.Text = string.Empty;
                txtDepositBranchCard.Text = string.Empty;
                txtDepositBankCard.Text = string.Empty;
                ddlCardType.Items.Clear();
                ddlCardType.DataBind();

                //ValidateTrue();

                //txtTotalAmount.Enabled = false;
                //txtCustCode.Enabled = false;
                //ddlPaymentType.Enabled = false;
                //btnSearch.Enabled = false;

            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 2);
            }
        }

        public void LoadRecieptGrid()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("SIRD_PAY_TP");
            dt.Columns.Add("SIRD_INV_NO");
            dt.Columns.Add("sird_chq_bank_cd");
            dt.Columns.Add("sird_chq_branch");
            dt.Columns.Add("sird_cc_tp");
            dt.Columns.Add("sird_anal_3");
            dt.Columns.Add("sird_settle_amt", typeof(decimal));
            dt.Columns.Add("Sird_ref_no");
            dt.Columns.Add("sird_anal_1");
            dt.Columns.Add("sird_anal_4");
            dt.Columns.Add("Sird_cc_period");

            recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];

            if (recieptItemList != null)
            {
                foreach (RecieptItemTBS ri in RecieptItemList)
                {
                    DataRow dr = dt.NewRow();
                    if (ri.Sird_pay_tp == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {

                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_chq_bank_cd;
                        dr[3] = ri.Sird_chq_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                    }
                    else if (ri.Sird_pay_tp == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_credit_card_bank;
                        dr[3] = ri.Sird_chq_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                        dr[10] = ri.Sird_cc_period;
                    }
                    else
                    {
                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_deposit_bank_cd;
                        dr[3] = ri.Sird_deposit_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                    }
                    dt.Rows.Add(dr);
                }
            }

            grdPaymentDetails.AutoGenerateColumns = false;
            grdPaymentDetails.DataSource = dt;
            grdPaymentDetails.DataBind();

        }
        private void AddPayment()
        {
            string invoiceType = "CS";

            if (Session["_recieptItem"] == null)
            {
                _recieptItemTbs = new List<RecieptItemTBS>();
            }
            else
            {
                _recieptItemTbs = (List<RecieptItemTBS>)Session["_recieptItem"];
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { DisplayMessages("Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtAmount.Text)) { DisplayMessages("Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtAmount.Text) <= 0) { DisplayMessages("Please select the valid pay amount"); return; }

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), GlbUserDefProf, "CS", DateTime.Now.Date);
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtAmount.Text) - BCV) * BCR / (BCR + 100);
                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                        BankOrOther_Charges = BankOrOtherCharge_;
                        txtAmount.Text = FormatToCurrency(Convert.ToString(_selectAmt - BankOrOther_Charges));
                    }
                }
            }

            if (Convert.ToDecimal(txtAmount.Text) < 0)
            {
                DisplayMessages("Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                if (string.IsNullOrEmpty(txtBankCard.Text))
                {
                    DisplayMessages("Please select the valid bank");
                    txtBankCard.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCardNo.Text))
                {
                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        DisplayMessages("Please select the card no");
                    ////else
                    ////{
                    ////    DisplayMessages("Please select the cheque no");
                    ////    txtCardNo.Focus();
                    ////    return;
                    ////}
                }
                if (string.IsNullOrEmpty(ddlCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the card type');", true);
                    ddlCardType.Focus();
                    return;
                }
            }
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
            {
                if (string.IsNullOrEmpty(txtBankCheque.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the valid bank');", true);
                    txtBankCheque.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtChequeNo.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque no');", true);
                    ddlCardType.Focus();
                    return;
                }

            }

            ////if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            ////{
            ////if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text))
            ////{
            ////    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the document no");
            ////    txtPayAdvReceiptNo.Focus();
            ////    return;
            ////}
            ////}

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPeriod.Text))
                {
                    DisplayMessages("Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPeriod.Text) <= 0)
                        {
                            DisplayMessages("Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPeriod.Text);

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                DisplayMessages("Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAmount.Text) <= 0)
                    {
                        DisplayMessages("Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }

            _payAmount = Convert.ToDecimal(txtAmount.Text);

            if (_recieptItem.Count <= 0)
            {
                RecieptItemTBS _item = new RecieptItemTBS();


                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    _cardno = txtCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    ////_cardno = txtPayAdvReceiptNo.Text;
                    ////chkPayCrPromotion.Checked = false;
                    ////_period = 0;
                    ////txtPayCrCardType.Text = string.Empty;
                    ////txtPayCrBranch.Text = string.Empty;
                    ////txtPayCrBank.Text = string.Empty;
                }

                _item.Sird_cc_is_promo = chkPromotion.Checked ? true : false;
                _item.Sird_cc_period = _period;

                _item.Sird_deposit_bank_cd = txtDepositBank.Text;
                _item.Sird_deposit_branch = txtDepositBranch.Text;

                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    _item.Sird_credit_card_bank = txtBankCard.Text;
                    _item.Sird_chq_branch = txtBranchCard.Text;
                    _item.Sird_ref_no = txtCardNo.Text;
                    _item.Sird_cc_tp = ddlCardType.SelectedItem.Text;

                    if (!string.IsNullOrEmpty(txtExpireCard.Text))
                    {
                        _item.Sird_cc_expiry_dt = Convert.ToDateTime(txtExpireCard.Text).Date;
                    }
                }
                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    _item.Sird_chq_bank_cd = txtBankCheque.Text;
                    _item.Sird_chq_branch = txtBranchCheque.Text;
                    _item.Sird_ref_no = txtChequeNo.Text;
                }
                if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                    if (_bankAccounts == null)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Bank not found for code');", true);
                        return;
                    }

                    if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        if (string.IsNullOrEmpty(txtBranchCheque.Text))
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque branch');", true);
                            txtBranchCheque.Focus();
                            return;
                        }

                        if (txtChequeNo.Text.Length != 6)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter correct cheque number. [Cheque number should be 6 numbers.]');", true);
                            txtChequeNo.Focus();
                            return;
                        }

                        _item.Sird_chq_dt = Convert.ToDateTime(txtChequeDate.Text);
                        _item.Sird_anal_5 = Convert.ToDateTime(txtChequeDate.Text);
                    }

                    _item.Sird_chq_bank_cd = txtBankCheque.Text;
                    _item.Sird_chq_branch = txtBranchCheque.Text;


                    ////_item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                    ////_item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                    _item.Sird_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                    _item.Sird_deposit_branch = txtDepositBranchCheque.Text;
                    _item.Sird_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNo.Text;
                    ////_item.Sard_anal_5 = dateTimePickerExpire.Value.Date;

                    ////bank = textBoxChqBank.Text;
                    ////branch = textBoxChqBranch.Text;
                    ////depBank = textBoxChqDepBank.Text; ;
                    ////depBranch = textBoxChqDepBranch.Text;
                    ////chqNo = textBoxChequeNo.Text;
                    ////chqExpire = dateTimePickerExpire.Value.Date;
                    ////NEED CHEQUE DATE

                    ////_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                    ////SARD_CHQ_DT NOT IN BO

                }

                ////_item.Sard_credit_card_bank = txtBankCard.Text;
                ////_item.Sard_chq_branch = txtBranchCard.Text;
                ////_item.Sard_ref_no = txtCardNo.Text;    

              //  _item.Sird_inv_no = txtInvoice.Text;

                _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _item.Sird_rmk = txtRemark.Text;
               // _paidAmount += _payAmount;

                _recieptItemTbs.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in _recieptItemTbs
                                 where _dup.Sird_pay_tp == ddlPayMode.SelectedValue.ToString()
                                 select _dup;
                if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    //var _dup_crcd = from _dup in _duplicate
                    //                where _dup.Sard_cc_tp == ddlCardType.Text && _dup.Sard_ref_no == txtCardNo.Text
                    //                select _dup;
                    //if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    //var _dup_chq = from _dup in _duplicate
                    //               where _dup.Sard_chq_bank_cd == txtBankCard.Text && _dup.Sard_ref_no == txtCardNo.Text
                    //               select _dup;
                    //if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    //var _dup_adv = from _dup in _duplicate
                    //               where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                    //               select _dup;
                    //if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();

                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    //var _dup_adv = from _dup in _duplicate
                    //               where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                    //               select _dup;
                    //if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItemTBS _item = new RecieptItemTBS();


                    if (string.IsNullOrEmpty(txtPeriod.Text.Trim()))
                        txtPeriod.Text = "0";

                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        //chkPayCrPromotion.Checked = false;
                        //_period = 0;
                        //txtPayCrCardType.Text = string.Empty;
                        //txtPayCrBranch.Text = string.Empty;
                        //txtPayCrBank.Text = string.Empty;
                    }
                    if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                        if (_bankAccounts == null)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Bank not found for code');", true);
                            return;
                        }

                        if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtBranchCheque.Text))
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque branch');", true);
                                txtBranchCheque.Focus();
                                return;
                            }

                            if (txtChequeNo.Text.Length != 6)
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter correct cheque number. [Cheque number should be 6 numbers.]');", true);
                                txtChequeNo.Focus();
                                return;
                            }


                            _item.Sird_chq_bank_cd = txtBankCheque.Text;
                            _item.Sird_chq_branch = txtBranchCheque.Text;


                            _item.Sird_chq_dt = Convert.ToDateTime(txtChequeDate.Text);
                            //_item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                            //_item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                            _item.Sird_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                            _item.Sird_deposit_branch = txtDepositBranchCheque.Text;
                            _item.Sird_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNo.Text;
                            _item.Sird_anal_5 = Convert.ToDateTime(txtChequeDate.Text);

                            //bank = textBoxChqBank.Text;
                            //branch = textBoxChqBranch.Text;
                            //depBank = textBoxChqDepBank.Text; ;
                            //depBranch = textBoxChqDepBranch.Text;
                            //chqNo = textBoxChequeNo.Text;
                            //chqExpire = dateTimePickerExpire.Value.Date;
                            //NEED CHEQUE DATE

                            //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                            //SARD_CHQ_DT NOT IN BO
                        }

                    }
                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        _item.Sird_credit_card_bank = txtBankCard.Text;
                        _item.Sird_chq_branch = txtBranchCard.Text;
                        _item.Sird_ref_no = txtCardNo.Text;
                        _item.Sird_cc_tp = ddlCardType.SelectedItem.Text;

                        if (!string.IsNullOrEmpty(txtExpireCard.Text))
                        { _item.Sird_cc_expiry_dt = Convert.ToDateTime(txtExpireCard.Text).Date; }

                    }
                    _item.Sird_cc_is_promo = chkPromotion.Checked ? true : false;
                    _item.Sird_cc_period = Convert.ToInt32(txtPeriod.Text);
                    _item.Sird_cc_period = _period;

                    //_item.Sird_inv_no = txtInvoice.Text;

                    ////_item.Sard_chq_bank_cd = txtBankCheque.Text;
                    ////_item.Sard_chq_branch = txtBranchCheque.Text;
                    ////_item.Sard_credit_card_bank = null;
                    //_item.Sird_deposit_bank_cd = txtDepositBank.Text;
                    //_item.Sird_deposit_branch = txtDepositBranch.Text;
                    //_item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                    //_item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                    //_item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    //_paidAmount += _payAmount;
                    _recieptItemTbs.Add(_item);
                }
                else
                {
                    DisplayMessages("You can not add duplicate payments");
                    return;
                }
            }

            Session["_recieptItem"] = _recieptItemTbs;
            grdPaymentDetails.DataSource = _recieptItemTbs;
            grdPaymentDetails.DataBind();

            //txtPaidAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtPaidAmount.Text) + Convert.ToDecimal(_paidAmount)));
            ////txtBalanceAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            ////txtAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //txtBalanceAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(_paidAmount)));
            //txtAmount.Text = txtBalanceAmount.Text;

            txtRemark.Text = "";
            txtCardNo.Text = "";
            txtBankCard.Text = "";
            txtBranchCard.Text = "";
            //ddlCardType.SelectedIndex = 0;
            txtExpireCard.Text = "";
            chkPromotion.Checked = false;
            //txtPayAdvReceiptNo.Text = "";
            //txtPayAdvRefAmount.Text = "";
            txtPeriod.Text = "";
            // txtPayCrPeriod.Enabled = false;

            txtDepositBank.Text = string.Empty;
            txtDepositBranch.Text = string.Empty;
        }
        protected void grdPaymentDetails_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    if (e.CommandName == "DeleteAmount")
            //    {
            //        recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
            //        RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

            //        Session["recieptItem"] = recieptItemList;

            //        _paidAmount = 0;
            //        foreach (RecieptItemTBS _list in RecieptItemList)
            //        {
            //            _paidAmount += _list.Sird_settle_amt;
            //        }

            //        GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
            //        txtAmount.Text = FormatToCurrency((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
            //        txtBalanceAmount.Text = FormatToCurrency((Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());

            //        if (RecieptItemList.Count > 0)
            //        {
            //            LoadRecieptGrid();
            //        }
            //        else
            //        {
            //            LoadRecieptGrid();
            //        }

            //        base.CHNLSVC.CloseAllChannels();

            //        txtAmount.Text = (txtBalanceAmount.Text);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message, 2);
            //}
        }

        protected void ImagebtnDepositBank_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetBankAccounts(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtDepositBank.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBank.Focus();

               // ValidateTrue();
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 2);
            }
        }
        protected void txtBankCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
             
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 2);
            }
        }
        protected void imgbtntxtBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = base.CHNLSVC.CommonSearch.GetBusinessCompany(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBankCard.Focus();

                //ValidateTrue();
            }
            catch (Exception ex)
            {
               // DisplayMessage(ex.Message, 2);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void imgbtnbankcard_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(txtBankCard.Text))
            //    {
            //        DisplayMessage("Bank is required.", 2);
            //        txtBankCard.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        if (!CheckBank(txtBankCard.Text, lblbank))
            //        {
            //            txtBankCard.Text = string.Empty;
            //            txtBankCard.Focus();
            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) > 0)
            //            {
            //                LoadCardType(txtBankCard.Text);
            //            }
            //            else
            //            {
            //                DisplayMessage("Amount is required.", 2);
            //                return;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message, 2);
            //}
            //finally
            //{
            //    base.CHNLSVC.CloseAllChannels();
            //}
        }
        protected void imgbtnDepositBankCard_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    BasePage basepage = new BasePage();
            //    Page pp = (Page)this.Page;
            //    uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //    ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
            //    DataTable _result = base.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
            //    ucc.BindUCtrlDDLData(_result);
            //    ucc.BindUCtrlGridData(_result);
            //    ucc.ReturnResultControl = txtDepositBankCard.ClientID;
            //    ucc.UCModalPopupExtender.Show();
            //    bool enable = false;
            //    ucc.DateEnable(enable);
            //    txtDepositBankCard.Focus();

            //    ValidateTrue();
            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message, 2);
            //}
            //finally
            //{
            //    base.CHNLSVC.CloseAllChannels();
            //}
        }
        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //LoadMIDno();
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 2);
            }
        }

        protected void imgbtnBankCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetBusinessCompany(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtBankCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBankCheque.Focus();

                //ValidateTrue();
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message, 2);
            }
        }
        protected void imgbtnDepositBankCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtDepositBankCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBankCheque.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }
        protected void ddlPayMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mltPaymentDetails.ActiveViewIndex = 0;

                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                {
                    mltPaymentDetails.ActiveViewIndex = 1;
                }
                else if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
                {
                    mltPaymentDetails.ActiveViewIndex = 2;
                }
                else if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
                {
                    mltPaymentDetails.ActiveViewIndex = 3;
                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }
        protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteAmount")
                {
                    recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];
                    RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

                    Session["_recieptItem"] = recieptItemList;

                    _paidAmount = 0;
                    foreach (RecieptItemTBS _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sird_settle_amt;
                    }

                    GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
                   // //txtPaidAmount.Text = (Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble((grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)]).Cells[19])).ToString();
                   // txtPaidAmount.Text = FormatToCurrency((Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
                  //  txtBalanceAmount.Text = FormatToCurrency((Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());

                   // //lblPaidAmo.Text = Convert.ToString(_paidAmount);
                   // //lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                  //  //textBoxAmount.Text = lblbalanceAmo.Text;

                    if (RecieptItemList.Count > 0)
                    {
                        LoadRecieptGrid();
                    }
                    else
                    {
                        LoadRecieptGrid();
                    }

                    //ItemAdded(sender, e);
                    //calculateBankChg = false;

                    base.CHNLSVC.CloseAllChannels();

                //    txtAmount.Text = (txtBalanceAmount.Text);

                    //if (grdPaymentDetails.Rows.Count > 0)
                    //   // btnCustomerPaymentAdd.Enabled = false;
                    //else
                    //   // btnCustomerPaymentAdd.Enabled = true;

                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }
        protected void imgbtnDepositBranchCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 1;
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = base.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = textBoxCCDepBranch;
                //_CommonSearch.ShowDialog();
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBranchCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBranchCard.Focus();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + ex.Message + " ');", true);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void imgbtnBranchCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtBranchCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBranchCheque.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        private void loadPaymenttypes()
        {
            ddlPayMode.Items.Clear();
            BasePage basePage = new BasePage();
            List<PaymentType> _paymentTypeRef = basePage.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(),
                Session["UserDefProf"].ToString(), "CS", DateTime.Now);
            List<string> payTypes = new List<string>();
            //payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            payTypes = payTypes.Distinct().ToList();
            ddlPayMode.DataSource = payTypes;
            ddlPayMode.DataBind();
            ddlPayMode.SelectedIndex = 0;
            if (payTypes != null)
            {
                if (payTypes.Count>0)
                {
                    if (payTypes[0].ToString().ToUpper() == "CASH")
                    {
                        mltPaymentDetails.ActiveViewIndex = 0;
                    }
                    else if (payTypes[0].ToString().ToUpper() == "CRCD")
                    {
                        mltPaymentDetails.ActiveViewIndex = 1;
                    }
                    else if (payTypes[0].ToString().ToUpper() == "CHEQUE")
                    {
                        mltPaymentDetails.ActiveViewIndex = 2;
                    }
                }
               
            }
        }

        protected void btnpiUpTown_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtPTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnDrTown_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = tXtDTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnVehicle_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchVehicle(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtVehicle.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void txtDriver_TextChanged(object sender, EventArgs e)
        {
            txtDriverName.Text = "";
            txtDriverContact.Text = "";
            if (!string.IsNullOrEmpty(txtDriver.Text))
            {
                MST_EMPLOYEE_TBS oMST_EMPLOYEE_TBS = CHNLSVC.Tours.GetEmployeeByComPC(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDriver.Text.Trim());
                if (oMST_EMPLOYEE_TBS != null && oMST_EMPLOYEE_TBS.MEMP_EPF != null)
                {
                    txtDriverName.Text = oMST_EMPLOYEE_TBS.MEMP_FIRST_NAME + " " + oMST_EMPLOYEE_TBS.MEMP_LAST_NAME;
                    txtDriverContact.Text = oMST_EMPLOYEE_TBS.MEMP_MOBI_NO;
                }
                else
                {
                    DisplayMessages("please select a correct EPF number");
                }
            }
        }
         protected void btnDriver_Click(object sender, ImageClickEventArgs e)
        {
            txtDriver_TextChanged(null, null);
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Drivers);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchDrivers(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtDriver.ClientID;
            ucc.UCModalPopupExtender.Show();
         }

         protected void btnChargeCode_Click(object sender, ImageClickEventArgs e)
         {
             BasePage basepage = new BasePage();
             Page pp = (Page)this.Page;
             uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
             ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
             DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_TransferCode(ucc.SearchParams, null, null);
             ucc.BindUCtrlDDLData(dataSource);
             ucc.BindUCtrlGridData(dataSource);
             ucc.ReturnResultControl = txtChargeCode.ClientID;
             ucc.UCModalPopupExtender.Show();
         }

         protected void btnChargeCodeLoad_Click(object sender, ImageClickEventArgs e)
         {
             SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), "TRANS", txtChargeCode.Text);
             if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
             {
                 txtUnitRate.Text = oSR_AIR_CHARGE.STC_RT.ToString();
             }
             else
             {
                 DisplayMessages("Please enter valid charge code");
                 txtChargeCode.Text = "";
                 txtChargeCode.Focus();
                 return;
             }
         }

         protected void btnAdd_Click(object sender, EventArgs e)
         {
             if (String.IsNullOrEmpty(txtChargeCode.Text))
             {
                 DisplayMessages("Please select a charge code");
                 return;
             }
             if (string.IsNullOrEmpty(txtUnitRate.Text))
             {
                 DisplayMessages("Please enter a unit amount");
                 return;
             }
             if (!isdecimal(txtUnitRate.Text))
             {
                 DisplayMessages("Please enter valid unit amount");
                 return;
             }
             if (string.IsNullOrEmpty(txtNoOfPassengers.Text))
             {
                 DisplayMessages("please enter number of passengers");
                 return;
             }

             if (Session["oMainInvoiceItems"] != null)
             {
                 oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
             }
             else
             {
                 oMainInvoiceItems = new List<InvoiceItem>();
             }

             if (oMainInvoiceItems.FindAll(x => x.Sad_itm_cd == txtChargeCode.Text).Count > 0)
             {
                 DisplayMessages("Selected charge code is already added.");
                 return;
             }

             SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), "TRANS", txtChargeCode.Text);
             InvoiceItem oItem = new InvoiceItem();
             oItem.Sad_itm_cd = txtChargeCode.Text;
             oItem.Sad_itm_stus = "GOD";
             oItem.Sad_qty = Convert.ToDecimal(txtNoOfPassengers.Text);
             oItem.Sad_unit_rt = Convert.ToDecimal(txtUnitRate.Text);
             oItem.Sad_tot_amt = oItem.Sad_unit_rt * oItem.Sad_qty;
             oItem.SII_CURR = oSR_AIR_CHARGE.STC_CURR;
             oItem.Sad_alt_itm_desc = oSR_AIR_CHARGE.STC_DESC;
             oMainInvoiceItems.Add(oItem);
             Session["oMainInvoiceItems"] = oMainInvoiceItems;
             bindTOGrid();
         }
         private bool isdecimal(string txt)
         {
             decimal asdasd;
             return decimal.TryParse(txt, out asdasd);
         }
         private bool isInteger(string txt)
         {
             Int32 asdasd;
             return Int32.TryParse(txt, out asdasd);
         }
         private void bindTOGrid()
         {
             if (Session["oMainInvoiceItems"] != null)
             {
                 oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                 Session["oMainInvoiceItems"] = oMainInvoiceItems;
                 dgvChargeItems.DataSource = oMainInvoiceItems;
                 dgvChargeItems.DataBind();
                 //modifyGRD();
             }
         }
         protected void txtChargeCode_TextChanged(object sender, EventArgs e)
         {
             btnChargeCodeLoad_Click(null, null);
         }

         protected void dgvChargeItems_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             try
             {
                 if (e.CommandName == "Delete")
                 {
                     GridViewRow row = dgvChargeItems.Rows[Convert.ToInt32(e.CommandArgument)];
                     Label lblQCD_CAT = (Label)row.FindControl("lblSad_itm_cd");

                     oMainInvoiceItems = (List<InvoiceItem>)Session["oMainInvoiceItems"];
                     oMainInvoiceItems.RemoveAt(Convert.ToInt32(e.CommandArgument));
                     Session["oMainInvoiceItems"] = oMainInvoiceItems;
                     bindTOGrid();
                 }
             }
             catch (Exception ex)
             {
             }

         }

         protected void dgvReq_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             try
             {
                 if (e.CommandName == "Select")
                 {
                     GridViewRow row = dgvReq.Rows[Convert.ToInt32(e.CommandArgument)];
                     Label lblEnquiry = (Label)row.FindControl("lblEnquiryID");
                     txtEnquiryID.Text = lblEnquiry.Text;
                     txtEnquiryID_TextChanged(null,null);
                     //mpEnquiry.Show();
                 }
             }
             catch (Exception ex)
             {
             }
         }

         protected void dgvReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             try
             {
                 dgvReq.PageIndex = e.NewPageIndex;
                 dgvReq.DataSource = null;
                 dgvReq.DataSource = (List<GEN_CUST_ENQ>)Session["Data"];
                 dgvReq.DataBind();
                 modifyGrid();
             }
             catch (Exception ex)
             {

             }
         }
         private void modifyGrid()
         {
             //if (dgvReq.Rows.Count > 0)
             //{
             //    for (int i = 0; i < dgvReq.Rows.Count; i++)
             //    {
             //        GridViewRow row = dgvReq.Rows[i];
             //        Label IsLateToNextStage = (Label)row.FindControl("IsLateToNextStage");
             //        if (IsLateToNextStage.Text.ToUpper() == "TRUE")
             //        {
             //            row.BackColor = System.Drawing.Color.Lavender;
             //        }
             //        Label lblDate = (Label)row.FindControl("lblDate");
             //        Label lblExpectedDate = (Label)row.FindControl("lblExpectedDate");
             //        lblDate.Text = Convert.ToDateTime(lblDate.Text).Date.ToString("dd/MMM/yyyy");
             //        lblExpectedDate.Text = Convert.ToDateTime(lblExpectedDate.Text).Date.ToString("dd/MMM/yyyy");
             //    }
             //}
         }
         protected void dgvReq_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             try
             {
                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {
                     e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvReq, "Select$" + e.Row.RowIndex);
                     e.Row.Attributes["style"] = "cursor:pointer";
                 }
             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
             }
         }

         protected void dgvReq_SelectedIndexChanged(object sender, EventArgs e)
         {
             //string EnquiryID = (dgvHistry.SelectedRow.FindControl("gce_mainreqid") as Label).Text;
             //List<GEN_CUST_ENQ> _enqList = ViewState["oItems"] as List<GEN_CUST_ENQ>;

             //if (_enqList.Count > 0)
             //{
             //    if (EnquiryID != "")
             //    {
             //        var Filter = _enqList.Where(x => x.Gce_mainreqid == EnquiryID).ToList();
             //        grdstatus.DataSource = Filter;
             //        grdstatus.DataBind();
             //    }
             //    else
             //    {
             //        grdstatus.DataSource = new int[] { };
             //        grdstatus.DataBind();

             //    }

             //}
         }

         protected void txtEnquiryID_TextChanged(object sender, EventArgs e)
         {
             ClearData();
             if (string.IsNullOrEmpty(txtEnquiryID.Text))
             {
                 DisplayMessages("Please select a enquery id. "); txtEnquiryID.Focus(); return;
             }
             GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
             if (oItem.GCE_ENQ_ID == null)
             {
                 DisplayMessages("The enquery id is invalid ! "); txtEnquiryID.Focus(); txtEnquiryID.Text = "";
                 return;
             }
             LoadData();
         }

         private void ClearData()
         {
             ddlTripTp.SelectedIndex = 0;
             txtCustomerCode.Text = "";
             txtCustomerCode_TextChanged(null,null);
             txtNoOfPassengers.Text = "";
             ddlServiceType.SelectedIndex = 0;
             ddlVehicleType.SelectedIndex = 0;
             txtNoOfreqVehicle.Text = "";
             txtPTown.Text = "";
             txtPAddress.Text = "";
             tXtDTown.Text = "";
             txtDAddress.Text = "";
             txtContactPerson.Text = "";
             txtContactMobile.Text = "";
             txtRemarks.Text = "";
             txtReturnDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
             txtRequestDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
             //MKB.TimePicker.TimeSelector.AmPmSpec am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
             //string hour = DateTime.Now.ToString("HH");
             //string mint = DateTime.Now.ToString("mm");
             //if (hour == "00" && mint == "00")
             //{
             //    tmExpect.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, am_pm);
             //}
             //else
             //{
             //    if (DateTime.Now.ToString("tt") == "AM")
             //    {
             //        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
             //    }
             //    else
             //    {
             //        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
             //    }
             //    tmExpect.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, am_pm);
             //}
             //tmExpect.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
             //tmReturn.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
             ddlServiceType.SelectedIndex = 0;
             txtVehicle.Text = "";
             txtDriver.Text = "";
             txtDriver_TextChanged(null,null);
             txtChargeCode.Text = "";
             txtUnitRate.Text = "";
             txtReference.Text = "";
             dgvChargeItems.DataSource = new int[] { }; dgvChargeItems.DataBind();
             ddlPayMode.SelectedIndex = 0;
             txtAmount.Text = "";
             txtDepositBank.Text = "";
             txtBranchCard.Text = "";
             txtRemark.Text = "";
              txtNoOfreqVehicle.Text ="";
              txtContactMobile.Text = "";
              txtContactPerson.Text = "";
         }
         protected void btnEnquiryID_Click(object sender, ImageClickEventArgs e)
         {
             ClearData();
             BasePage basepage = new BasePage();
             Page pp = (Page)this.Page;
             uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
             ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
             DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ENQUIRY(ucc.SearchParams, null, null);
             ucc.BindUCtrlDDLData(dataSource);
             ucc.BindUCtrlGridData(dataSource);
             ucc.ReturnResultControl = txtEnquiryID.ClientID;
             ucc.UCModalPopupExtender.Show();
             txtEnquiryID.Focus();
             //txtEnquiryID_TextChanged(null,null);
         }

         protected void btnRecall_Click(object sender, EventArgs e)
         {
             if (string.IsNullOrEmpty(txtEnquiryID.Text))
             {
                 DisplayMessages("Please select a enquery id. "); txtEnquiryID.Focus(); return;
             }
             GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
             if (oItem == null)
             {
                 DisplayMessages("The enquery id is invalid ! "); txtEnquiryID.Focus(); txtEnquiryID.Text = ""; return;
             }
             LoadData();
         }
         private void LoadData()
         {
             GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
             if (oItem != null)
             {
                 lblSeqNum.Text = oItem.GCE_SEQ.ToString();
                 txtReference.Text = oItem.GCE_REF;
                 ddlTripTp.SelectedIndex = ddlTripTp.Items.IndexOf(ddlTripTp.Items.FindByValue(oItem.GCE_ENQ_SUB_TP));
                 //ddlFacility.SelectedValue = oItem.GCE_ENQ_TP;
                 txtCustomerCode.Text = oItem.GCE_CUS_CD;
                 txtCustomerCode_TextChanged(null,null);
                 //txtName.Text = oItem.GCE_NAME;
                 //txtAddress1.Text = oItem.GCE_ADD1;
                 //txtAddress2.Text = oItem.GCE_ADD2;
                 //txtMobile.Text = oItem.GCE_MOB;
                 //txtEmail.Text = oItem.GCE_EMAIL;
                 //txtNIC.Text = oItem.GCE_NIC;
                 txtRequestDate.Text = oItem.GCE_EXPECT_DT.ToString("dd/MMM/yyyy");
                 DateTime date=oItem.GCE_EXPECT_DT;
                 //MKB.TimePicker.TimeSelector.AmPmSpec am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                 //string hour = date.ToString("HH");
                 //string mint = date.ToString("mm");
                 //if (hour == "00" && mint == "00")
                 //{
                 //    tmExpect.SetTime(date.Hour, date.Minute, am_pm);
                 //}
                 //else
                 //{
                 //    if (date.ToString("tt") == "AM")
                 //    {
                 //        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                 //    }
                 //    else
                 //    {
                 //        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                 //    }
                 //    tmExpect.SetTime(date.Hour, date.Minute, am_pm);
                 //}
                 txtRemarks.Text = oItem.GCE_ENQ;
                 //txtFacilityCom.Text = oItem.GCE_ENQ_COM;
                 //txtFacilityPC.Text = oItem.GCE_ENQ_PC;

                 if (oItem.GCE_ENQ_TP == "TNSPT")   
                 {
                     //divOther.Visible = true;
                 }
                 else
                 {
                     //divOther.Visible = false;
                 }

                 txtPTown.Text = oItem.GCE_FRM_TN;
                 tXtDTown.Text = oItem.GCE_TO_TN;
                 txtPAddress.Text = oItem.GCE_FRM_ADD;
                 txtDAddress.Text = oItem.GCE_TO_ADD;
                 txtNoOfPassengers.Text = oItem.GCE_NO_PASS.ToString();
                 if (!String.IsNullOrEmpty(oItem.GCE_VEH_TP))
                 {
                     ddlVehicleType.SelectedIndex =ddlVehicleType.Items.IndexOf(ddlVehicleType.Items.FindByValue(oItem.GCE_VEH_TP));
                 }
                 txtReturnDate.Text = oItem.GCE_RET_DT.ToString("dd/MMM/yyyy");
                 date = oItem.GCE_RET_DT;
                 //am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                 //if (date.ToString("tt") == "AM")
                 //{
                 //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                 //}
                 //else
                 //{
                 //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                 //}
                 //tmReturn.SetTime(date.Hour, date.Minute, am_pm);
                 if (oItem.GCE_STUS == 0)
                 {
                     //lblStatusText.Visible = true;
                     //lblStatus.Visible = true;
                     //lblStatusText.Text = "Cancelled";
                     btnCreate.Enabled = false;

                     btnCancel.Enabled = false;
                 }
                 txtDriver.Text = oItem.GCE_DRIVER;
                 txtDriver_TextChanged(null,null);
                 txtVehicle.Text = oItem.GCE_FLEET;
                 txtNoOfreqVehicle.Text = oItem.GCE_REQ_NO_VEH.ToString();
                 txtContactMobile.Text = oItem.GCE_CONT_MOB.ToString();
                 txtContactPerson.Text = oItem.GCE_CONT_PER.ToString();
              //   ddlTripTp.SelectedIndex = ddlTripTp.Items.IndexOf(ddlTripTp.Items.FindByValue(oItem.GCE_ENQ_TP));
             }
         }

         protected void btnFleetShed_Click(object sender, EventArgs e)
         {
             TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
             DateTime dt = Convert.ToDateTime(txtRequestDate.Text).Add(time);
             uc_VehicleEnquiry.ExpectedDate = Convert.ToDateTime(txtRequestDate.Text).Add(time);
             uc_VehicleEnquiry.TmExpect = tmExpect;
             uc_VehicleEnquiry.Vehicle = txtVehicle.Text;
             uc_VehicleEnquiry.DisplayParentData();
             VehicleTypeBind();
             uc_VehicleEnquiry.VehicleType.SelectedIndex = ddlVehicleType.SelectedIndex;
             PopupEnquery.Show();
             Session["ShowUcVehicleInquiry"] = "Y";
         }
         private void VehicleTypeBind()
         {
             List<ComboBoxObject> oVehicleTypes = CHNLSVC.Tours.GET_ALL_VEHICLE_FOR_COMBO();
             uc_VehicleEnquiry.VehicleType.DataSource = oVehicleTypes;
             uc_VehicleEnquiry.VehicleType.DataTextField = "Text";
             uc_VehicleEnquiry.VehicleType.DataValueField = "Value";
             uc_VehicleEnquiry.VehicleType.DataBind();
         }
         protected void LinkButton1_Click(object sender, EventArgs e)
         {
             Session["ShowUcVehicleInquiry"] = "N";
             PopupEnquery.Hide();
         }
         private void clearAll()
         {
             ClearData();
             txtEnquiryID.Text = "";
             lblSeqNum.Text = "0";
         }
    }
}