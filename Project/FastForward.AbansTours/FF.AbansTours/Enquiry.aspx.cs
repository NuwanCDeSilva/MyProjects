using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;

namespace FF.AbansTours
{
    public partial class Enquiry : BasePage
    {
        private BasePage _basePage;
        protected List<GEN_CUST_ENQ> oGEN_CUST_ENQs { get { return (List<GEN_CUST_ENQ>)Session["oGEN_CUST_ENQs"]; } set { Session["oGEN_CUST_ENQs"] = value; } }

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
                case CommonUIDefiniton.SearchUserControlType.ToursFacCompany:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlFacility.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
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
                        clearAll();
                        loadEnquiryTypes();
                        loadVehicleTypes();
                        LoadEnqSubTp();
                        string id = Request.QueryString["htenus"];
                        if (!string.IsNullOrEmpty(id))
                        {
                            LoadFromSesstions();
                            if (ddlFacility.SelectedValue == "TNSPT")
                            {
                                divOther.Visible = true;
                            }
                            else
                            {
                                divOther.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            loadToSesstion();
            Response.Redirect("~/DataEnty/CustomerCreation.aspx");
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

        protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
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

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessages("Invalid work phone number.");
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

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, Session["UserCompanyCode"].ToString());
        }

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            txtNIC.Text = cust.Mbe_nic;
            txtCustomerCode.Text = cust.Mbe_cd;
            txtMobile.Text = cust.Mbe_mob;
            txtName.Text = cust.Mbe_name;
            txtEmail.Text = cust.Mbe_email;
            txtAddress1.Text = cust.Mbe_add1;
            txtAddress2.Text = cust.Mbe_add2;
            txtCustomerCode.Focus();
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

        private void loadEnquiryTypes()
        {
            List<MST_ENQTP> oEnqType = CHNLSVC.Tours.GET_ENQUIRY_TYPE(Session["UserCompanyCode"].ToString());
            if (oEnqType.Count > 0)
            {
                ddlFacility.DataSource = oEnqType;
                ddlFacility.DataTextField = "MET_DESC";
                ddlFacility.DataValueField = "MET_CD";
                ddlFacility.DataBind();
            }
        }

        private void LoadFacilityByCompany()
        {
            List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(Session["UserCompanyCode"].ToString(), ddlFacility.SelectedValue.ToString());
            if (oMST_FACBY.Count > 0)
            {
                txtFacilityCom.Text = oMST_FACBY[0].MFB_FACCOM;
                txtFacilityPC.Text = oMST_FACBY[0].MFB_FACPC;
            }

            if (ddlFacility.SelectedValue == "TNSPT")
            {
                divOther.Visible = true;
            }
            else
            {
                divOther.Visible = false;
            }
        }

        private void clearAll()
        {
            txtEnquiryID.Text = "";
            txtFacilityCom.Text = "";
            txtReference.Text = "";
            txtExpectedDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtCustomerCode.Text = "";
            txtMobile.Text = "";
            txtNIC.Text = "";
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtEmail.Text = "";
            txtEnquiry.Text = "";
            ddlFacility.SelectedIndex = 0;
            ddlFacility.Focus();
            _isExsit.Value = "0";
            _isGroup.Value = "0";
            lblSeqNum.Text = "0";
            txtFacilityPC.Text = "";

            txtFromTown.Text = "";
            txtToTown.Text = "";
            txtAddFrom.Text = "";
            txtAddTo.Text = "";
            txtNonOfPassengers.Text = "";
            ddlVehicleType.SelectedIndex = 0;
            txtRetuenDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            lblStatusText.Visible = false;
            lblStatus.Visible = false;

            btnCancel.Enabled = true;
            oGEN_CUST_ENQs = new List<GEN_CUST_ENQ>();
            bindData();
           
        }

        private void loadToSesstion()
        {
            GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
            oItem.GCE_SEQ = Convert.ToInt32(lblSeqNum.Text);
            oItem.GCE_ENQ_ID = txtEnquiryID.Text;
            oItem.GCE_REF = txtReference.Text;
            oItem.GCE_ENQ_TP = ddlFacility.SelectedValue.ToString();
            oItem.GCE_COM = txtFacilityCom.Text;
            oItem.GCE_PC = txtFacilityPC.Text;
            oItem.GCE_DT = DateTime.Now.Date;
            oItem.GCE_CUS_CD = txtCustomerCode.Text;
            oItem.GCE_NAME = txtName.Text;
            oItem.GCE_ADD1 = txtAddress1.Text;
            oItem.GCE_ADD2 = txtAddress2.Text;
            oItem.GCE_MOB = txtMobile.Text;
            oItem.GCE_EMAIL = txtEmail.Text;
            oItem.GCE_NIC = txtNIC.Text;
            oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtExpectedDate.Text);
            oItem.GCE_SER_LVL = string.Empty;
            oItem.GCE_ENQ = txtEnquiry.Text;
            oItem.GCE_ENQ_COM = Session["UserCompanyCode"].ToString();
            oItem.GCE_ENQ_PC = Session["UserDefProf"].ToString();
            MasterProfitCenter oPc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            oItem.GCE_ENQ_PC_DESC = oPc.Mpc_desc;
            oItem.GCE_STUS = 1;
            oItem.GCE_CRE_BY = Session["UserID"].ToString();
            oItem.GCE_CRE_DT = DateTime.Now;
            oItem.GCE_MOD_BY = Session["UserID"].ToString();
            oItem.GCE_MOD_DT = DateTime.Now;

            oItem.GCE_FRM_TN = txtFromTown.Text;
            oItem.GCE_TO_TN = txtToTown.Text;
            oItem.GCE_FRM_ADD = txtAddFrom.Text;
            oItem.GCE_TO_ADD = txtAddTo.Text;
            if (!string.IsNullOrEmpty(txtNonOfPassengers.Text))
            {
                oItem.GCE_NO_PASS = Convert.ToInt32(txtNonOfPassengers.Text);
            }
            oItem.GCE_VEH_TP = ddlVehicleType.SelectedValue.ToString();
            oItem.GCE_RET_DT = Convert.ToDateTime(txtRetuenDate.Text);

            Session["GEN_CUST_ENQ"] = oItem;
            Session["RedirectPage"] = "~/Enquiry.aspx";



        }

        private void LoadFromSesstions()
        {
            if (Session["GEN_CUST_ENQ"] != null)
            {
                GEN_CUST_ENQ oItem = (GEN_CUST_ENQ)Session["GEN_CUST_ENQ"];
                lblSeqNum.Text = oItem.GCE_SEQ.ToString();
                txtReference.Text = oItem.GCE_REF;
                ddlFacility.SelectedValue = oItem.GCE_ENQ_TP;
                txtCustomerCode.Text = oItem.GCE_CUS_CD;
                txtCustomerCode_TextChanged(null, null);
                txtExpectedDate.Text = oItem.GCE_EXPECT_DT.ToString("dd/MMM/yyyy");
                txtEnquiry.Text = oItem.GCE_ENQ;
                txtFacilityCom.Text = oItem.GCE_ENQ_COM;
                txtFacilityPC.Text = oItem.GCE_ENQ_PC;
                txtEnquiryID.Text = oItem.GCE_ENQ_ID;

                txtFromTown.Text = oItem.GCE_FRM_TN;
                txtToTown.Text = oItem.GCE_TO_TN;
                txtAddFrom.Text = oItem.GCE_FRM_ADD;
                txtAddTo.Text = oItem.GCE_TO_ADD;
                txtNonOfPassengers.Text = oItem.GCE_NO_PASS.ToString();
                ddlVehicleType.SelectedValue = oItem.GCE_VEH_TP;
                txtRetuenDate.Text = oItem.GCE_RET_DT.ToString("dd/MMM/yyyy");
            }
        }

        private bool validateSave()
        {
            bool status = true;
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                DisplayMessages("Please enter reference num");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFacilityCom.Text))
            {
                DisplayMessages("Please select facility company");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFacilityPC.Text))
            {
                DisplayMessages("Please select facility SBL");
                status = false;
                return status;
            }
            if (Convert.ToDateTime(txtExpectedDate.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please select a valid date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                DisplayMessages("Please select a customer code");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtEnquiry.Text))
            {
                DisplayMessages("Please enter enquiry");
                status = false;
                return status;
            }

            if (ddlFacility.SelectedValue == "TNSPT")
            {
                if (string.IsNullOrEmpty(txtFromTown.Text))
                {
                    DisplayMessages("Please enter from town");
                    status = false;
                    return status;
                }
                if (string.IsNullOrEmpty(txtToTown.Text))
                {
                    DisplayMessages("Please enter to town");
                    status = false;
                    return status;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    DisplayMessages("Please enter address 1");
                    status = false;
                    return status;
                }
                if (string.IsNullOrEmpty(txtAddress2.Text))
                {
                    DisplayMessages("Please enter address 2");
                    status = false;
                    return status;
                }
                if (string.IsNullOrEmpty(txtNonOfPassengers.Text))
                {
                    DisplayMessages("Please enter number of packages");
                    status = false;
                    return status;
                }
                if (!isdecimal(txtNonOfPassengers.Text))
                {
                    DisplayMessages("Please enter valid number of packages");
                    status = false;
                    return status;
                }
                if (Convert.ToDecimal(txtNonOfPassengers.Text) < 0)
                {
                    DisplayMessages("Please enter valid number of packages");
                    status = false;
                    return status;
                }
                if (string.IsNullOrEmpty(txtRetuenDate.Text))
                {
                    DisplayMessages("Please select a return date");
                    status = false;
                    return status;
                }
                if (Convert.ToDateTime(txtRetuenDate.Text) < DateTime.Now.Date)
                {
                    DisplayMessages("Please select a valid return date");
                    status = false;
                    return status;
                }
            }
            return status;
        }

        private void loadVehicleTypes()
        {
            List<ComboBoxObject> oVehicleTypes = CHNLSVC.Tours.GET_ALL_VEHICLE_FOR_COMBO();
            ddlVehicleType.DataSource = oVehicleTypes;
            ddlVehicleType.DataTextField = "Text";
            ddlVehicleType.DataValueField = "Value";
            ddlVehicleType.DataBind();
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
        }

        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnqSubTp();
            LoadFacilityByCompany();
        }

        protected void ddlFacility_TextChanged(object sender, EventArgs e)
        {
            LoadFacilityByCompany();
        }

        protected void btnFacilityCom_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ToursFacCompany);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_FAC_COM(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFacilityCom.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
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
            int result = CHNLSVC.Tours.SaveEnquiryRequestList(oGEN_CUST_ENQs, _ReqInsAuto, oMainReq, out err);
            if (result > 0)
            {
                string msg;
                if (!string.IsNullOrEmpty(err))
                {
                    msg = "Successfully saved. Inquiry Number :" + err;
                }
                else
                {
                    msg = "Successfully Updated.";
                }
                DisplayMessages(msg);
                clearAll();
            }
            else
            {
                DisplayMessages("Error Occurred." + err);
            }

            //if (!validateSave())
            //{
            //    return;
            //}

            //GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
            //oItem.GCE_SEQ = Convert.ToInt32(lblSeqNum.Text);
            //oItem.GCE_ENQ_ID = txtEnquiryID.Text;
            //oItem.GCE_REF = txtReference.Text;
            //oItem.GCE_ENQ_TP = ddlFacility.SelectedValue.ToString();
            //oItem.GCE_COM = txtFacilityCom.Text;
            //oItem.GCE_PC = txtFacilityPC.Text;
            //oItem.GCE_DT = DateTime.Now.Date;
            //oItem.GCE_CUS_CD = txtCustomerCode.Text;
            //oItem.GCE_NAME = txtName.Text;
            //oItem.GCE_ADD1 = txtAddress1.Text;
            //oItem.GCE_ADD2 = txtAddress2.Text;
            //oItem.GCE_MOB = txtMobile.Text;
            //oItem.GCE_EMAIL = txtEmail.Text;
            //oItem.GCE_NIC = txtNIC.Text;
            //oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtExpectedDate.Text);
            //oItem.GCE_SER_LVL = string.Empty;
            //oItem.GCE_ENQ = txtEnquiry.Text;
            //oItem.GCE_ENQ_COM = Session["UserCompanyCode"].ToString();
            //oItem.GCE_ENQ_PC = Session["UserDefProf"].ToString();
            //oItem.GCE_ENQ_PC_DESC = string.Empty;
            //oItem.GCE_STUS = 1;
            //oItem.GCE_CRE_BY = Session["UserID"].ToString();
            //oItem.GCE_CRE_DT = DateTime.Now;
            //oItem.GCE_MOD_BY = Session["UserID"].ToString();
            //oItem.GCE_MOD_DT = DateTime.Now;

            //oItem.GCE_FRM_TN = txtFromTown.Text;
            //oItem.GCE_TO_TN = txtToTown.Text;
            //oItem.GCE_FRM_ADD = txtAddFrom.Text;
            //oItem.GCE_TO_ADD = txtAddTo.Text;
            //if (!string.IsNullOrEmpty(txtNonOfPassengers.Text))
            //{
            //    oItem.GCE_NO_PASS = Convert.ToInt32(txtNonOfPassengers.Text);
            //}
            //oItem.GCE_VEH_TP = ddlVehicleType.SelectedValue.ToString();
            //oItem.GCE_RET_DT = Convert.ToDateTime(txtRetuenDate.Text);

            //MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            //oItem.GCE_ENQ_PC_DESC = (oPc != null) ? oPc.Mpc_desc : string.Empty;

            //MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
            //_ReqInsAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            //_ReqInsAuto.Aut_cate_tp = "PC";
            //_ReqInsAuto.Aut_direction = null;
            //_ReqInsAuto.Aut_modify_dt = null;
            //_ReqInsAuto.Aut_moduleid = "AT";
            //_ReqInsAuto.Aut_number = 0;
            //_ReqInsAuto.Aut_start_char = "AT";
            //_ReqInsAuto.Aut_year = DateTime.Today.Year;

            //string err;
            //int result = CHNLSVC.Tours.Save_GEN_CUST_ENQ(oItem, _ReqInsAuto, out err);
            //if (result > 0)
            //{
            //    string msg;
            //    if (!string.IsNullOrEmpty(err))
            //    {
            //        msg = "Successfully saved. Enquiry Number :" + err;
            //    }
            //    else
            //    {
            //        msg = "Successfully Updated.";
            //    }
            //    DisplayMessages(msg);
            //    clearAll();
            //}
            //else
            //{
            //    DisplayMessages("Error Occurred." + err);
            //}
        }

        protected void txtFacilityCom_TextChanged(object sender, EventArgs e)
        {
            LoadFacilityByCompany();
        }

        protected void btnEnquiryID_Click(object sender, ImageClickEventArgs e)
        {
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
        }

        protected void txtEnquiryID_TextChanged(object sender, EventArgs e)
        {
            GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
            if (oItem != null)
            {
                lblSeqNum.Text = oItem.GCE_SEQ.ToString();
                txtReference.Text = oItem.GCE_REF;
                ddlFacility.SelectedValue = oItem.GCE_ENQ_TP;
                txtCustomerCode.Text = oItem.GCE_CUS_CD;
                txtName.Text = oItem.GCE_NAME;
                txtAddress1.Text = oItem.GCE_ADD1;
                txtAddress2.Text = oItem.GCE_ADD2;
                txtMobile.Text = oItem.GCE_MOB;
                txtEmail.Text = oItem.GCE_EMAIL;
                txtNIC.Text = oItem.GCE_NIC;
                txtExpectedDate.Text = oItem.GCE_EXPECT_DT.ToString("dd/MMM/yyyy");
                txtEnquiry.Text = oItem.GCE_ENQ;
                txtFacilityCom.Text = oItem.GCE_ENQ_COM;
                txtFacilityPC.Text = oItem.GCE_ENQ_PC;

                if (oItem.GCE_ENQ_TP == "TNSPT")
                {
                    divOther.Visible = true;
                }
                else
                {
                    divOther.Visible = false;
                }

                txtFromTown.Text = oItem.GCE_FRM_TN;
                txtToTown.Text = oItem.GCE_TO_TN;
                txtAddFrom.Text = oItem.GCE_FRM_ADD;
                txtAddTo.Text = oItem.GCE_TO_ADD;
                txtNonOfPassengers.Text = oItem.GCE_NO_PASS.ToString();
                if (!String.IsNullOrEmpty(oItem.GCE_VEH_TP))
                {
                    ddlVehicleType.SelectedValue = oItem.GCE_VEH_TP;
                }
                txtRetuenDate.Text = oItem.GCE_RET_DT.ToString("dd/MMM/yyyy");
                ddlTripTp.SelectedIndex = ddlTripTp.Items.IndexOf(ddlTripTp.Items.FindByValue(oItem.GCE_ENQ_SUB_TP));
                if (oItem.GCE_STUS == 0)
                {
                    lblStatusText.Visible = true;
                    lblStatus.Visible = true;
                    lblStatusText.Text = "Cancelled";
                    btnCreate.Enabled = false;

                    btnCancel.Enabled = false;
                }
            }
        }
        private void LoadEnqSubTp()
        {
            while (ddlTripTp.Items.Count>1)
            {
                ddlTripTp.Items.RemoveAt(1);  
            }
            if (ddlFacility.SelectedIndex>0)
            {
                List<MST_ENQSUBTP> domains = CHNLSVC.Tours.GET_ENQRY_SUB_TP(new MST_ENQSUBTP()
                {
                    MEST_COM = (string)Session["UserCompanyCode"],
                    MEST_TPCD = ddlFacility.SelectedValue
                });
                ddlTripTp.DataSource = domains;
                ddlTripTp.DataTextField = "mest_desc";
                ddlTripTp.DataValueField = "mest_stpcd";
            }
            ddlTripTp.DataBind();
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpBatchConfirmationOne.Hide();
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

        protected void btnToTown_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtToTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnFromTown_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFromTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void txtNonOfPassengers_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNonOfPassengers.Text) && !isdecimal(txtNonOfPassengers.Text))
            {
                DisplayMessages("Please enter a valid number of passenger");
                txtNonOfPassengers.Focus();
            }
        }

        protected void btnAddRecords_Click(object sender, EventArgs e)
        {
            if (!validateSave())
            {
                return;
            }

            GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
            oItem.GCE_SEQ = Convert.ToInt32(lblSeqNum.Text);
            oItem.GCE_ENQ_ID = txtEnquiryID.Text;
            oItem.GCE_REF = txtReference.Text;
            oItem.GCE_ENQ_TP = ddlFacility.SelectedValue.ToString();
            oItem.GCE_COM = txtFacilityCom.Text;
            oItem.GCE_PC = txtFacilityPC.Text;
            oItem.GCE_DT = DateTime.Now.Date;
            oItem.GCE_CUS_CD = txtCustomerCode.Text;
            oItem.GCE_NAME = txtName.Text;
            oItem.GCE_ADD1 = txtAddress1.Text;
            oItem.GCE_ADD2 = txtAddress2.Text;
            oItem.GCE_MOB = txtMobile.Text;
            oItem.GCE_EMAIL = txtEmail.Text;
            oItem.GCE_NIC = txtNIC.Text;
            oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtExpectedDate.Text);
            oItem.GCE_SER_LVL = string.Empty;
            oItem.GCE_ENQ = txtEnquiry.Text;
            oItem.GCE_ENQ_COM = Session["UserCompanyCode"].ToString();
            oItem.GCE_ENQ_PC = Session["UserDefProf"].ToString();
            oItem.GCE_ENQ_PC_DESC = string.Empty;
            oItem.GCE_STUS = 1;
            oItem.GCE_CRE_BY = Session["UserID"].ToString();
            oItem.GCE_CRE_DT = DateTime.Now;
            oItem.GCE_MOD_BY = Session["UserID"].ToString();
            oItem.GCE_MOD_DT = DateTime.Now;

            List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(Session["UserCompanyCode"].ToString(), ddlFacility.SelectedValue.ToString());
            if (oMST_FACBY != null && oMST_FACBY.Count > 0 && oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC).Count > 0)
            {
                oItem.Gce_bill_cuscd = oMST_FACBY.FindAll(x => x.MFB_FACPC == oItem.GCE_PC)[0].MFB_BILL_CD;
            }
            else
            {
                oItem.Gce_bill_cuscd = oItem.GCE_CUS_CD;
            }

            oItem.GCE_FRM_TN = txtFromTown.Text;
            oItem.GCE_TO_TN = txtToTown.Text;
            oItem.GCE_FRM_ADD = txtAddFrom.Text;
            oItem.GCE_TO_ADD = txtAddTo.Text;
            if (!string.IsNullOrEmpty(txtNonOfPassengers.Text))
            {
                oItem.GCE_NO_PASS = Convert.ToInt32(txtNonOfPassengers.Text);
            }
            oItem.GCE_VEH_TP = ddlVehicleType.SelectedValue.ToString();
            oItem.GCE_RET_DT = Convert.ToDateTime(txtRetuenDate.Text);
            oItem.GCE_ENQ_SUB_TP = ddlTripTp.SelectedValue;
            MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            oItem.GCE_ENQ_PC_DESC = (oPc != null) ? oPc.Mpc_desc : string.Empty;
            oGEN_CUST_ENQs.Add(oItem);
            bindData();
        }

        protected void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblmet_desc = dr.FindControl("lblmet_desc") as Label;
            Label lblgce_com = dr.FindControl("lblgce_com") as Label;
            Label lblgce_pc = dr.FindControl("lblgce_pc") as Label;
            Label lblGCE_ENQ_TP = dr.FindControl("lblGCE_ENQ_TP") as Label;


            oGEN_CUST_ENQs.RemoveAll(x => x.GCE_ENQ_TP == lblGCE_ENQ_TP.Text.Trim() && x.GCE_COM == lblgce_com.Text.Trim() && x.GCE_PC == lblgce_pc.Text.Trim());
            bindData();
        }

        private void clearAferAdd()
        {

        }

        private void bindData()
        {
            if (oGEN_CUST_ENQs.Count > 0)
            {
                pnlCustomerDetails.Enabled = false;
                dgvAddedDetails.DataSource = oGEN_CUST_ENQs;
                dgvAddedDetails.DataBind();
            }
            else
            {
                dgvAddedDetails.DataSource = new int[] { };
                dgvAddedDetails.DataBind();
                pnlCustomerDetails.Enabled = true;
            }
        }
    }
}