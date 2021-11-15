using FF.AbansTours.UserControls;
using FF.BusinessObjects;
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
    public partial class VehicleAllocation : BasePage
    {
        private BasePage _basePage;
        private List<InvoiceItem> oMainInvoiceItems = null;
        private InvoiceHeader oHeader = new InvoiceHeader();
        private List<RecieptItem> _recieptItem = new List<RecieptItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserSubChannl"]))
                    )
                {
                   
                    if (!IsPostBack)
                    {
                        txtExpectedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                        txtReturnDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

                        LoadInvoiceType();
                        loadVehicleTypes();
                        txtFromTown.Text = "COLOMBO";

                        btnPrint.Enabled = false;

                        string id = Request.QueryString["htenus"];
                        if (!string.IsNullOrEmpty(id))
                        {
                            txtCustomer.Text = Session["newCustomer"].ToString();
                            Session["newCustomer"] = null;
                        }
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "TNSPT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "0,1,2,3,4,5,6,7,8,9,10" + seperator + "TNSPT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "TRANS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Drivers:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + ddlVehicleType.SelectedValue.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #region Main Btns
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int result = 0;
            bool isinvoicvesave = false;

            if (ValidateSave())
            {
                #region Enquiry

                GEN_CUST_ENQ oItem = new GEN_CUST_ENQ();
                oItem.GCE_SEQ = (lblEnquirySeq.Text == "") ? 0 : Convert.ToInt32(lblEnquirySeq.Text);
                oItem.GCE_ENQ_ID = txtEnquiryID.Text;
                oItem.GCE_REF = txtReferenceNum.Text;
                oItem.GCE_ENQ_TP = "TNSPT";
                oItem.GCE_COM = Session["UserCompanyCode"].ToString();
                oItem.GCE_PC = Session["UserDefProf"].ToString();
                oItem.GCE_DT = DateTime.Now.Date;
                oItem.GCE_CUS_CD = txtCustomer.Text;

                MasterBusinessEntity custProf = GetbyCustCD(txtCustomer.Text.Trim().ToUpper());
                if (custProf == null || string.IsNullOrEmpty(custProf.Mbe_cd))
                {
                    DisplayMessages("please enter a valid Customer code");
                    return;
                }
                oItem.GCE_NAME = custProf.MBE_FNAME;
                oItem.GCE_ADD1 = custProf.Mbe_add1;
                oItem.GCE_ADD2 = custProf.Mbe_add2;
                oItem.GCE_MOB = custProf.Mbe_mob;
                oItem.GCE_EMAIL = custProf.Mbe_email;
                oItem.GCE_NIC = custProf.Mbe_nic;

                DateTime date = DateTime.Now;
                TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
                oItem.GCE_EXPECT_DT = Convert.ToDateTime(txtExpectedDate.Text).Add(time);

                oItem.GCE_SER_LVL = string.Empty;
                oItem.GCE_ENQ = "Generated in Trip request form";
                oItem.GCE_ENQ_COM = txtFacilityCom.Text;
                oItem.GCE_ENQ_PC = txtFacilitySBL.Text;
                oItem.GCE_ENQ_PC_DESC = string.Empty;
                oItem.GCE_STUS = 9;
                oItem.GCE_CRE_BY = Session["UserID"].ToString();
                oItem.GCE_CRE_DT = DateTime.Now;
                oItem.GCE_MOD_BY = Session["UserID"].ToString();
                oItem.GCE_MOD_DT = DateTime.Now;

                oItem.GCE_FRM_TN = txtFromTown.Text;
                oItem.GCE_TO_TN = txtToTown.Text;
                oItem.GCE_FRM_ADD = txtAddress1.Text;
                oItem.GCE_TO_ADD = txtAddress2.Text;
                oItem.GCE_NO_PASS = Convert.ToInt32(txtNoOfPassengers.Text);
                oItem.GCE_VEH_TP = ddlVehicleType.SelectedValue.ToString();

                oItem.GCE_DRIVER = txtDriver.Text;
                oItem.GCE_FLEET = txtVehicle.Text;

                date = DateTime.Now;
                time = new TimeSpan(0, tmReturn.Hour, tmReturn.Minute, 0);
                oItem.GCE_RET_DT = Convert.ToDateTime(txtReturnDate.Text).Add(time);

                if (oItem.GCE_EXPECT_DT > oItem.GCE_RET_DT)
                {
                    DisplayMessages("Please select valid date range");
                    return;
                }

                MasterProfitCenter oPc = CHNLSVC.General.GetPCByPCCode(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                oItem.GCE_ENQ_PC_DESC = (oPc != null) ? oPc.Mpc_desc : string.Empty;

                MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                _ReqInsAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                _ReqInsAuto.Aut_cate_tp = "PC";
                _ReqInsAuto.Aut_direction = null;
                _ReqInsAuto.Aut_modify_dt = null;
                _ReqInsAuto.Aut_moduleid = "AT";
                _ReqInsAuto.Aut_number = 0;
                _ReqInsAuto.Aut_start_char = "AT";
                _ReqInsAuto.Aut_year = DateTime.Today.Year;

                if (ddlPayType.SelectedValue == "CS")
                {
                    isinvoicvesave = true;
                    oItem.GCE_STUS = 5;
                }

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

                MasterBusinessEntity oCust = GetbyCustCD(txtCustomer.Text.Trim().ToUpper());
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
                oHeader.Sah_inv_tp = ddlPayType.SelectedValue.ToString();
                oHeader.Sah_is_acc_upload = false;
                oHeader.Sah_man_ref = txtReferenceNum.Text;
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

                _recieptItem = (List<RecieptItem>)Session["_recieptItem"];
                if (_recieptItem == null)
                {
                    _recieptItem = new List<RecieptItem>();
                }

                RecieptItem _item = new RecieptItem();

                _item.Sard_deposit_bank_cd = null;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = "CS";
                _item.Sard_settle_amt = oMainInvoiceItems.Sum(x => x.Sad_tot_amt);

                _recieptItem.Add(_item);



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

                string err;

                result = _basePage.CHNLSVC.Tours.SaveTripRequestWithInvoice(oHeader, oMainInvoiceItems, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, oItem, _ReqInsAuto, out err, isinvoicvesave);
                /*Lakshan 2016/01/25 */
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

                #endregion
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        #endregion

        #region Searchs
        protected void btnEnquiryID_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchEnquiryWithStage(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtEnquiryID.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtEnquiryID.Focus();
            //DisplayMessages("Error Occurred while processing !!!"); return;

        }

        protected void btnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCustomer.ClientID;
            ucc.UCModalPopupExtender.Show();
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

        protected void btnFromTown_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtFromTown.ClientID;
            //ucc.UCModalPopupExtender.Show();
            try
            {
                lblSearchType.Text = "Town_new_from";
                Session["Town_new_from"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
                DataTable _result = CHNLSVC.CommonSearch.GetTown_new(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    // _result.Columns["Serial #"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["Town_new_from"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
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
                DisplayMessages("Error Occurred while processing !!!"); return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnToTown_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown_new(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtToTown.ClientID;
            //ucc.UCModalPopupExtender.Show();
            try
            {
                lblSearchType.Text = "Town_new";
                Session["Town_new"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
                DataTable _result = CHNLSVC.CommonSearch.GetTown_new(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    // _result.Columns["Serial #"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["Town_new"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                { dgvResult.DataSource = null; }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            { DisplayMessages("Error Occurred while processing !!!"); return; }
            finally
            { CHNLSVC.CloseChannel(); }
        }

        protected void btnChargeCode_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_TransferCode(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtChargeCode.ClientID;
            //ucc.UCModalPopupExtender.Show();

            try
            {
                lblSearchType.Text = "TransferCodes";
                Session["TransferCodes"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_TransferCode(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    // _result.Columns["Serial #"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["TransferCodes"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
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
                DisplayMessages("Error Occurred while processing !!!"); return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnVehicle_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchVehicle(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtVehicle.ClientID;
            //ucc.UCModalPopupExtender.Show();
            try
            {
                lblSearchType.Text = "Vehicles";
                Session["Vehicles"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
                DataTable _result = CHNLSVC.CommonSearch.SearchVehicle(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    // _result.Columns["Serial #"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["Vehicles"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
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
                DisplayMessages("Error Occurred while processing !!!"); return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnDriver_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Drivers);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchDrivers(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtDriver.ClientID;
            //ucc.UCModalPopupExtender.Show();
            try
            {
                lblSearchType.Text = "Drivers";
                Session["Drivers"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Drivers);
                DataTable _result = CHNLSVC.CommonSearch.SearchDrivers(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    // _result.Columns["Serial #"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["Drivers"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
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
                DisplayMessages("Error Occurred while processing !!!"); return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        #endregion

        #region Methods

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

        private void clearAll()
        {
            lblEnquirySeq.Text = "0";
            if (ddlPayType.Items.Count > 0)
            {
                ddlPayType.SelectedIndex = 0;
            }
            txtEnquiryID.Text = "";
            txtCustomer.Text = "";
            txtFacilityCom.Text = "";
            txtFacilitySBL.Text = "";
            txtFacility.Text = "Transport";
            txtReferenceNum.Text = "";
            txtFromTown.Text = "";
            txtToTown.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            if (ddlVehicleType.Items.Count > 0)
            {
                ddlVehicleType.SelectedIndex = 0;
            }
            txtExpectedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtReturnDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtChargeCode.Text = "";
            txtUnitRate.Text = "";
            txtDriverName.Text = "";

            txtFromTown.Text = "COLOMBO";
            dgvChargeItems.DataSource = null;
            dgvChargeItems.DataBind();
            txtNoOfPassengers.Text = "";
            txtVehicle.Text = "";
            txtDriver.Text = "";
            txtDriverName.Text = "";

            btnPrint.Enabled = false;
            Session["oMainInvoiceItems"] = null;

            lblEnquirySeq.Text = "";
        }

        private bool LoadInvoiceType()
        {
            List<PriceDefinitionRef> _PriceDefinitionRef = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
            string DefaultInvoiceType = string.Empty;
            if (_PriceDefinitionRef != null && _PriceDefinitionRef.Count > 0)
            {
                var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                if (_defaultValue != null)
                    if (_defaultValue.Count > 0)
                    {
                        DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                    }
            }
            else
            {
                DisplayMessages("Please setup price definition.");
            }

            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    ddlPayType.DataSource = _types;
                    ddlPayType.DataBind();

                    //ddlInvoiceType.SelectedIndex = ddlInvoiceType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        ddlPayType.Text = DefaultInvoiceType;
                    }
                }
                else
                    ddlPayType.DataSource = null;
            else
                ddlPayType.DataSource = null;

            return _isAvailable;
        }

        private void loadVehicleTypes()
        {
            List<ComboBoxObject> oVehicleTypes = CHNLSVC.Tours.GET_ALL_VEHICLE_FOR_COMBO();
            ddlVehicleType.DataSource = oVehicleTypes;
            ddlVehicleType.DataTextField = "Text";
            ddlVehicleType.DataValueField = "Value";
            ddlVehicleType.DataBind();
        }


        private bool ValidateSave()
        {
            bool status = true;
            if (String.IsNullOrEmpty(ddlPayType.Text))
            {
                DisplayMessages("Please select a pay type");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                DisplayMessages("Please select a customer");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFacilityCom.Text))
            {
                DisplayMessages("Please select a Facility Company");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFacilitySBL.Text))
            {
                DisplayMessages("Please select a Facility SBL");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtReferenceNum.Text))
            {
                DisplayMessages("Please enter a reference number");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFromTown.Text))
            {
                DisplayMessages("Please select a from town");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtToTown.Text))
            {
                DisplayMessages("Please select a to town");
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
            if (string.IsNullOrEmpty(txtNoOfPassengers.Text))
            {
                DisplayMessages("Please enter no of passengers");
                status = false;
                return status;
            }
            if (!isdecimal(txtNoOfPassengers.Text))
            {

                DisplayMessages("Please enter valid no of passengers");
                status = false;
                return status;
            }
            if (String.IsNullOrEmpty(ddlVehicleType.Text))
            {
                DisplayMessages("Please select a vehicle type");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtVehicle.Text))
            {
                DisplayMessages("Please select a vehicle");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDriver.Text))
            {
                DisplayMessages("Please select a driver");
                status = false;
                return status;
            }

            DateTime dateExpected = DateTime.Now;
            TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
            dateExpected = Convert.ToDateTime(txtExpectedDate.Text).Add(time);

            DateTime datReturne = DateTime.Now;
            time = new TimeSpan(0, tmReturn.Hour, tmReturn.Minute, 0);
            datReturne = Convert.ToDateTime(txtReturnDate.Text).Add(time);

            if (dateExpected > datReturne)
            {
                DisplayMessages("Please enter valid date and time range");
                status = false;
                return status;
            }
            if (dateExpected < DateTime.Now)
            {
                DisplayMessages("Please enter valid expected date and time");
                status = false;
                return status;
            }
            if (datReturne < DateTime.Now)
            {
                DisplayMessages("Please enter valid return date and time");
                status = false;
                return status;
            }
            if (dgvChargeItems.Rows.Count == 0)
            {
                DisplayMessages("Please add chargers");
                status = false;
                return status;
            }
            return status;
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
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

        private void modifyGRD()
        {
            for (int i = 0; i < dgvChargeItems.Rows.Count; i++)
            {
                GridViewRow row = dgvChargeItems.Rows[i];
                Label lblSad_qty = (Label)row.FindControl("lblSad_qty");
                Label lblSad_unit_rt = (Label)row.FindControl("lblSad_unit_rt");
                Label lblSad_tot_amt = (Label)row.FindControl("lblSad_tot_amt");

                lblSad_qty.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_qty.Text));
                lblSad_unit_rt.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_unit_rt.Text));
                lblSad_tot_amt.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_tot_amt.Text));
            }
        }

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        #endregion

        #region Details Loads

        protected void btnEnquiryIDLoad_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnquiryID.Text))
            {
                DisplayMessages("Please select a enquiry ID");
                return;
            }
            GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
            if (oItem != null)
            {
                lblEnquirySeq.Text = oItem.GCE_SEQ.ToString();
                txtCustomer.Text = oItem.GCE_CUS_CD;
                txtFacilityCom.Text = oItem.GCE_COM;
                txtFacilitySBL.Text = oItem.GCE_PC;
                txtReferenceNum.Text = oItem.GCE_REF;
                txtFromTown.Text = oItem.GCE_FRM_TN;
                txtToTown.Text = oItem.GCE_TO_TN;
                txtAddress1.Text = oItem.GCE_ADD1;
                txtAddress2.Text = oItem.GCE_ADD2;
                txtNoOfPassengers.Text = oItem.GCE_NO_PASS.ToString();
                if (!string.IsNullOrEmpty(oItem.GCE_VEH_TP))
                {
                    ddlVehicleType.SelectedValue = oItem.GCE_VEH_TP;
                }
                if (oItem.GCE_EXPECT_DT == DateTime.MinValue)
                {
                    txtExpectedDate.Text = DateTime.Now.ToLocalTime().ToString("dd/MMM/yyyy");
                }
                else
                {
                    txtExpectedDate.Text = oItem.GCE_EXPECT_DT.ToLocalTime().ToString("dd/MMM/yyyy");

                }
                if (oItem.GCE_RET_DT == DateTime.MinValue)
                {

                    txtReturnDate.Text = DateTime.Now.ToLocalTime().ToString("dd/MMM/yyyy");
                }
                else
                {
                    txtReturnDate.Text = oItem.GCE_RET_DT.ToLocalTime().ToString("dd/MMM/yyyy");
                }

                if (oItem.GCE_STUS > 4)
                    btnPrint.Enabled = true;

            }
        }

        protected void btnCustomerLoad_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnFacilityComLoad_Click(object sender, ImageClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFacilityCom.Text))
            {
                List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(txtFacilityCom.Text.Trim(), "TNSPT");
                if (oMST_FACBY.Count > 0)
                {
                    txtFacilityCom.Text = oMST_FACBY[0].MFB_FACCOM;
                    txtFacilitySBL.Text = oMST_FACBY[0].MFB_FACPC;
                }
            }
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

        #endregion

        #region TextChanges

        protected void txtEnquiryID_TextChanged(object sender, EventArgs e)
        {
            btnEnquiryIDLoad_Click(null, null);
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            btnCustomerLoad_Click(null, null);
        }

        protected void txtFacilityCom_TextChanged(object sender, EventArgs e)
        {
            btnFacilityComLoad_Click(null, null);
        }

        protected void txtChargeCode_TextChanged(object sender, EventArgs e)
        {
            btnChargeCodeLoad_Click(null, null);
        }

        #endregion

        #region Sub Events

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            Session["RedirectPage"] = "~/Sales/VehicleAllocation.aspx";
            Response.Redirect("~/DataEnty/CustomerCreation.aspx");
        }

        protected void txtDriver_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDriver.Text))
            {
                MST_EMPLOYEE_TBS oMST_EMPLOYEE_TBS = CHNLSVC.Tours.GetEmployeeByComPC(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDriver.Text.Trim());
                if (oMST_EMPLOYEE_TBS != null && oMST_EMPLOYEE_TBS.MEMP_EPF != null)
                {
                    txtDriverName.Text = oMST_EMPLOYEE_TBS.MEMP_FIRST_NAME + " " + oMST_EMPLOYEE_TBS.MEMP_LAST_NAME;
                }
                else
                {
                    DisplayMessages("please select a correct EPF number");
                }
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

        protected void txtNoOfPassengers_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoOfPassengers.Text) && !isdecimal(txtNoOfPassengers.Text))
            {
                DisplayMessages("please enter a valid number");
                txtNoOfPassengers.Text = "";
            }
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

        protected void dgvChargeItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void dgvChargeItems_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string enquiryid = txtEnquiryID.Text;
                Session["EnquiryID"] = enquiryid;
                mpReceiptPrint.Show();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void Close_Click(object sender, EventArgs e)
        {
            mpReceiptPrint.Hide();
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                string desc = dgvResult.SelectedRow.Cells[2].Text;

                if (lblSearchType.Text == "Drivers")
                {
                    txtDriver.Text = code;
                    txtDriver_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "TransferCodes")
                {
                    txtChargeCode.Text = code;
                    txtChargeCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Vehicles")
                {
                    txtVehicle.Text = code;
                    //   txtVehicle_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Town_new_from")
                {
                    txtFromTown.Text = code;
                    //   txtVehicle_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Town_new")
                {
                    txtToTown.Text = code;
                    //   txtVehicle_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DisplayMessages("Error Occurred while processing !!!"); return;
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
                if (lblSearchType.Text == "Drivers")
                {
                    _result = (DataTable)Session["Drivers"];
                }
                else if (lblSearchType.Text == "TransferCodes")
                {
                    _result = (DataTable)Session["TransferCodes"];
                }
                else if (lblSearchType.Text == "Vehicles")
                {
                    _result = (DataTable)Session["Vehicles"];
                }
                else if (lblSearchType.Text == "Town_new_from")
                {
                    _result = (DataTable)Session["Town_new_from"];
                }
                else if (lblSearchType.Text == "Town_new")
                {
                    _result = (DataTable)Session["Town_new"];
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
                DisplayMessages("Error Occurred while processing !!!"); return;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = null;
                Session["Drivers"] = null;
                if (lblSearchType.Text == "Drivers")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Drivers);
                    _dt = CHNLSVC.CommonSearch.SearchDrivers(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Drivers"] = _dt;
                }
                else if (lblSearchType.Text == "TransferCodes")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                    _dt = CHNLSVC.CommonSearch.SEARCH_TransferCode(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Drivers"] = _dt;
                }
                else if (lblSearchType.Text == "Vehicles")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
                    _dt = CHNLSVC.CommonSearch.SearchVehicle(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Vehicles"] = _dt;
                }
                else if (lblSearchType.Text == "Town_new_from")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
                    _dt = CHNLSVC.CommonSearch.GetTown_new(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Town_new_from"] = _dt;
                }
                else if (lblSearchType.Text == "Town_new")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
                    _dt = CHNLSVC.CommonSearch.GetTown_new(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Town_new"] = _dt;
                }
                dgvResult.DataSource = null;
                if (dgvResult.PageIndex > 0)
                {
                    dgvResult.SetPageIndex(0);
                }
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
                DisplayMessages("Error Occurred while processing !!!"); return;
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

        protected void lbtnSeEnquery_Click(object sender, ImageClickEventArgs e)
        {
            TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
            DateTime dt=Convert.ToDateTime(txtExpectedDate.Text).Add(time);
            uc_VehicleEnquiry.ExpectedDate = Convert.ToDateTime(txtExpectedDate.Text).Add(time);
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
    }
}