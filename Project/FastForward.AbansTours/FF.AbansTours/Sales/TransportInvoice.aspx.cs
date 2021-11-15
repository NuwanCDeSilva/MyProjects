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
    public partial class TransportInvoice : BasePage
    {
        private BasePage _basePage;
        private List<TR_LOGSHEET_DET> oMainItems = new List<TR_LOGSHEET_DET>();
        private List<RecieptItemTBS> recieptItem;
        private decimal _paidAmount;
        List<RecieptItemTBS> recieptItemList;
        List<PaymentType> _paymentTypeRef;
        private InvoiceHeader oHeader;

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
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserSubChannl"]))
                    )
                {
                    lbtColse_Click(null, null);
                    if (!IsPostBack)
                    {
                        ClearAll();
                        LoadInvoiceType();
                        ValidateTrue();
                        LoadPayMode();
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "~/login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCustCode.Text))
            {
                DisplayMessage("Please select a customer.", 2);
                return;
            }
            if (string.IsNullOrEmpty(ddlPaymentType.Text))
            {
                DisplayMessage("Please select a payment type", 2);
                return;
            }
            if (dgvItems.Rows.Count == 0)
            {
                DisplayMessage("Please add payment records", 2);
                return;
            }
            if (Session["oHeader"] != null)
            {
                oHeader = (InvoiceHeader)Session["oHeader"];
            }
            else
            {
                oHeader = new InvoiceHeader();
            }

            if (ddlPaymentType.Text == "CS")
            {
                if (Session["recieptItem"] == null)
                {
                    DisplayMessage("please add payments", 2);
                    return;
                }
            }

            if (Convert.ToDateTime(txtProcessDate.Text).Date < DateTime.Now.Date)
            {
                DisplayMessage("Please select a valid date", 2);
                return;
            }

            MasterBusinessEntity oCust = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCode.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
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
            oHeader.Sah_dt = Convert.ToDateTime(txtProcessDate.Text);
            oHeader.Sah_epf_rt = 0;
            oHeader.Sah_esd_rt = 0;
            oHeader.Sah_ex_rt = 1;
            oHeader.Sah_inv_no = "na";
            oHeader.Sah_inv_sub_tp = "SA";
            oHeader.Sah_inv_tp = ddlPaymentType.SelectedItem.ToString();
            oHeader.Sah_is_acc_upload = false;
            oHeader.Sah_man_ref = string.Empty;// lblManrefNumber.Text;
            oHeader.Sah_manual = false;
            oHeader.Sah_mod_by = Session["UserID"].ToString();
            oHeader.Sah_mod_when = DateTime.Now;
            oHeader.Sah_pc = Session["UserDefProf"].ToString();
            oHeader.Sah_pdi_req = 0;
            oHeader.Sah_ref_doc = string.Empty;//txtEnquiryID.Text;
            oHeader.Sah_sales_chn_cd = "";
            oHeader.Sah_sales_chn_man = "";
            oHeader.Sah_sales_ex_cd = Session["UserID"].ToString();// ddlExecutive.SelectedValue.ToString();
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
            oHeader.Sah_tax_inv = oCust.Mbe_is_tax;// chkTaxPayable.Checked ? true : false;
            //oHeader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
            //oHeader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            oHeader.Sah_del_loc = string.Empty;
            //oHeader.Sah_grn_com = _customerCompany;
            //oHeader.Sah_grn_loc = _customerLocation;
            //oHeader.Sah_is_grn = _isCustomerHasCompany;
            //oHeader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            oHeader.Sah_is_svat = oCust.Mbe_is_svat;// lblSVATStatus.Text == "Available" ? true : false;
            oHeader.Sah_tax_exempted = oCust.Mbe_tax_ex;// lblExemptStatus.Text == "Available" ? true : false;
            oHeader.Sah_anal_2 = "TOUR";
            oHeader.Sah_anal_3 = string.Empty;// ddlPackageType.Text;
            //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
            oHeader.Sah_man_cd = Session["UserDefProf"].ToString();
            oHeader.Sah_is_dayend = 0;
            oHeader.Sah_remarks = string.Empty;// txtMainRemark.Text;

            recieptItem = (List<RecieptItemTBS>)Session["recieptItem"];

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
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtProcessDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = txtCustCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = oCust.MBE_FNAME;
            _ReceiptHeader.Sar_debtor_add_1 = oCust.Mbe_add1;
            _ReceiptHeader.Sar_debtor_add_2 = oCust.Mbe_add2;
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = oCust.Mbe_mob;
            _ReceiptHeader.Sar_nic_no = oCust.Mbe_nic;
            if (ddlPaymentType.Text != "CRED")
            {
                _ReceiptHeader.Sar_tot_settle_amt = recieptItem.Sum(x => x.Sird_settle_amt);
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

            List<InvoiceItem> oMainItemsList = new List<InvoiceItem>();

            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                Label lblTLD_CHR_CD = (Label)dgvItems.Rows[i].FindControl("lblTLD_CHR_CD");
                Label lblTLD_CHR_DESC = (Label)dgvItems.Rows[i].FindControl("lblTLD_CHR_DESC");
                Label lblTLD_QTY = (Label)dgvItems.Rows[i].FindControl("lblTLD_QTY");
                Label lblTLD_U_RT = (Label)dgvItems.Rows[i].FindControl("lblTLD_U_RT");
                Label lblTLD_TOT = (Label)dgvItems.Rows[i].FindControl("lblTLD_TOT");
                Label lblTLD_SEQ = (Label)dgvItems.Rows[i].FindControl("lblTLD_SEQ");
                Label lblTLH_DT = (Label)dgvItems.Rows[i].FindControl("lblTLH_DT");

                InvoiceItem oItem = new InvoiceItem();
                oItem.Sad_itm_cd = lblTLD_CHR_CD.Text;
                oItem.Sad_itm_stus = "GOD";
                oItem.Sad_alt_itm_desc = lblTLD_CHR_DESC.Text;
                oItem.Sad_qty = Convert.ToDecimal(lblTLD_QTY.Text);
                oItem.Sad_unit_rt = Convert.ToDecimal(lblTLD_U_RT.Text);
                oItem.Sad_tot_amt = Convert.ToDecimal(lblTLD_TOT.Text);
                oItem.Sad_promo_cd = lblTLD_SEQ.Text;
                oItem.Sad_print_stus = true;
                oItem.Sad_warr_remarks = lblTLH_DT.Text;
                //oItem.SII_CURR = item.QCD_CURR;
                //oItem.SII_EX_RT = item.QCD_EX_RATE;
                oMainItemsList.Add(oItem);
            }

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _invoiceAuto.Aut_cate_tp = "TINVO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CS";
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = "TINVO";
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtProcessDate.Text).Year;

            MasterAutoNumber _receiptAuto = null;
            if (recieptItem != null)
                if (recieptItem.Count > 0)
                {
                    _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                    _receiptAuto.Aut_cate_tp = "PRO";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "DIR";
                    _receiptAuto.Aut_year = Convert.ToDateTime(txtProcessDate.Text).Year;
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

            List<RecieptItem> NewRecieptItems = ConvertRecipt(recieptItem);

            int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, null, _ReceiptHeader, NewRecieptItems, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, true);
            if (result > 0)
            {
                DisplayMessage("Successfully saved. Invoice : " + _invoiceNo, 1);
                ClearAll();
                Session["invoiceNum"] = _invoiceNo;
                Response.Redirect("~/InvoicePrint3.aspx");
            }
            else
            {
                DisplayMessage("Error Occurred" + _error, 2);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
            {
                string err;
                Int32 result = CHNLSVC.Tours.UPDATE_INVOICE_STATUS("C", Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtPaymentNo.Text, out err);

                if (result > 0)
                {
                    ClearAll();
                    DisplayMessage("Invoice canceled.", 1);
                    return;
                }
                else
                {
                    DisplayMessage("Error: " + err, 2);
                    return;
                }
            }
            else
            {
                DisplayMessage("please select a invoice.", 1);
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

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
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "MSCELNS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "0,1,2,3,4,5,6,7,8,9,10" + seperator + "TNSPT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Drivers:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LogSheetHEader:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiveSeach:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ClearAll()
        {
            txtProcessDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtCustCode.Text = "";
            txtPaymentNo.Text = "";
            txtPaymentFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            if (ddlPaymentType.Items.Count > 0)
            {
                ddlPaymentType.SelectedIndex = 0;
            }
            txtPaymentToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtBalanceAmount.Text = "";
            txtRemark.Text = "";
            dgvItems.DataSource = null;
            dgvItems.DataBind();
            oMainItems = new List<TR_LOGSHEET_DET>();
            recieptItem = new List<RecieptItemTBS>();
            recieptItemList = new List<RecieptItemTBS>();
            _paymentTypeRef = new List<PaymentType>();
            _paidAmount = 0;
            Session["oMainItems"] = null;
            txtTotalAmount.Text = "";
            txtBalanceAmount.Text = "";
            grdPaymentDetails.DataSource = null;
            grdPaymentDetails.DataBind();

            txtAmount.Text = "";
            Session["recieptItem"] = null;
            grdPaymentDetails.DataSource = null;
            grdPaymentDetails.DataBind();
            txtDepositBank.Text = "";
            txtDepositBankCard.Text = "";
            txtDepositBankCheque.Text = "";
            txtDepositBranch.Text = "";
            txtDepositBranchCard.Text = "";
            txtDepositBranchCheque.Text = "";
            txtCustCode.Enabled = true;
            btnSearch.Enabled = true;
            ddlPaymentType.Enabled = true;
            DivAlert.Visible = false;
            btnCreate.Enabled = true;
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
                DisplayMessage("Please setup price definition.", 1);
            }

            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    ddlPaymentType.DataSource = _types;
                    ddlPaymentType.DataBind();

                    //ddlInvoiceType.SelectedIndex = ddlInvoiceType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        ddlPaymentType.Text = DefaultInvoiceType;
                    }
                }
                else
                    ddlPaymentType.DataSource = null;
            else
                ddlPaymentType.DataSource = null;

            return _isAvailable;
        }

        private void DisplayMessage(String message, Int16 options)
        {
            if (options == 1)
            {
                DivAlert.Attributes.Add("class", "alert alert-success");
            }
            else if (options == 2)
            {
                DivAlert.Attributes.Add("class", "alert alert-danger");
            }
            else if (options == 3)
            {
                DivAlert.Attributes.Add("class", "alert alert-info");
            }
            DivAlert.Visible = true;
            lblAsk.Text = message;
        }

        private void bindTOGrid()
        {
            if (Session["oMainItems"] != null)
            {
                oMainItems = (List<TR_LOGSHEET_DET>)Session["oMainItems"];
                Session["oMainItems"] = oMainItems;
                dgvItems.DataSource = oMainItems;
                dgvItems.DataBind();
                modifyGRD();

                txtTotalAmount.Text = oMainItems.Sum(x => x.TLD_TOT).ToString("N2");
                txtAmount.Text = txtTotalAmount.Text;
            }
        }

        private void modifyGRD()
        {
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                GridViewRow row = dgvItems.Rows[i];
                Label lblTLD_QTY = (Label)row.FindControl("lblTLD_QTY");
                Label lblTLD_U_RT = (Label)row.FindControl("lblTLD_U_RT");
                Label lblTLD_U_AMT = (Label)row.FindControl("lblTLD_U_AMT");
                Label lblTLD_TAX = (Label)row.FindControl("lblTLD_TAX");
                Label lblTLD_DIS_RT = (Label)row.FindControl("lblTLD_DIS_RT");
                Label lblTLD_DIS_AMT = (Label)row.FindControl("lblTLD_DIS_AMT");
                Label lblTLD_TOT = (Label)row.FindControl("lblTLD_TOT");

                Label lblTLD_IS_CUS = (Label)row.FindControl("lblTLD_IS_CUS");
                Label lblTLD_IS_DRI = (Label)row.FindControl("lblTLD_IS_DRI");

                Image imgCusFalse = (Image)row.FindControl("imgCusFalse");
                Image imgCusTrue = (Image)row.FindControl("imgCusTrue");

                Image imgDriverFalse = (Image)row.FindControl("imgDriverFalse");
                Image imgDriverTrue = (Image)row.FindControl("imgDriverTrue");

                Label lblTLH_DT = (Label)row.FindControl("lblTLH_DT");

                lblTLD_QTY.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_QTY.Text));
                lblTLD_U_RT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_U_RT.Text));
                lblTLD_U_AMT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_U_AMT.Text));

                lblTLD_TAX.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_TAX.Text));
                lblTLD_DIS_RT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_DIS_RT.Text));
                lblTLD_DIS_AMT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_DIS_AMT.Text));
                lblTLD_TOT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_TOT.Text));

                lblTLH_DT.Text = Convert.ToDateTime(lblTLH_DT.Text).ToString("dd/MMM/yyyy");

                if (lblTLD_IS_CUS.Text == "1")
                {
                    imgCusTrue.Visible = true;
                }
                else
                {
                    imgCusFalse.Visible = true;
                }

                if (lblTLD_IS_DRI.Text == "1")
                {
                    imgDriverTrue.Visible = true;
                }
                else
                {
                    imgDriverFalse.Visible = true;
                }
            }
        }

        protected void btnCustCode_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCustCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DivAlert.Visible = false;
            if (String.IsNullOrEmpty(txtCustCode.Text))
            {
                DisplayMessage("Please select a customer", 1);
                return;
            }

            oMainItems = CHNLSVC.Tours.GetLogDetailsCustInvoice(txtCustCode.Text.Trim(), Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtPaymentFromDate.Text), Convert.ToDateTime(txtPaymentToDate.Text), 0);
            if (oMainItems.Count > 0)
            {
                Session["oMainItems"] = oMainItems;
                bindTOGrid();
            }
            else
            {
                DisplayMessage("No records.", 3);
                ClearAll();
            }
        }

        //Payments
        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void ImgAmount_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString()))
                {
                    DisplayMessage("Please select the valid payment type.", 2);
                    return;
                }
                if (String.IsNullOrEmpty(txtAmount.Text))
                {
                    DisplayMessage("Please select the valid pay amount.", 2);
                    return;
                }
                if (String.IsNullOrEmpty(txtTotalAmount.Text))
                {
                    DisplayMessage("Total Amount is empty", 2);
                    return;
                }
                //if (String.IsNullOrEmpty(txtBalanceAmount.Text))
                //{
                //    DisplayMessage("Please select the valid pay amount.", 2);
                //    return;
                //}
                //if (Convert.ToDecimal(txtBalanceAmount.Text) <= 0)
                //{
                //    DisplayMessage("Please select the valid pay amount.", 2);
                //    return;
                //}
                //if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CASH.ToString())
                //{
                //    if (String.IsNullOrEmpty(txtDepositBank.Text))
                //    {
                //        DisplayMessage("Bank is required.", 2);
                //        return;
                //    }
                //    if (String.IsNullOrEmpty(txtDepositBranch.Text))
                //    {
                //        DisplayMessage("Branch is required.", 2);
                //        return;
                //    }
                //}
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    if (String.IsNullOrEmpty(txtChequeNo.Text))
                    {
                        DisplayMessage("ChequeNo is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCheque.Text))
                    {
                        DisplayMessage("Bank is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCheque.Text))
                    {
                        DisplayMessage("Branch is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtChequeDate.Text))
                    {
                        DisplayMessage("Cheque Date is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCheque.Text))
                    {
                        DisplayMessage("Deposit Bank is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCheque.Text))
                    {
                        DisplayMessage("Deposit Branch is required.", 2);
                        return;
                    }
                }
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    if (String.IsNullOrEmpty(txtCardNo.Text))
                    {
                        DisplayMessage("Card No is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCard.Text))
                    {
                        DisplayMessage("Bank is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCard.Text))
                    {
                        DisplayMessage("Branch is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCard.Text))
                    {
                        DisplayMessage("Deposit Bank is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCard.Text))
                    {
                        DisplayMessage("Branch Card is required.", 2);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtPeriod.Text))
                    {
                        DisplayMessage("Branch is required.", 2);
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

                ValidateTrue();

                txtTotalAmount.Enabled = false;
                txtCustCode.Enabled = false;
                ddlPaymentType.Enabled = false;
                btnSearch.Enabled = false;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteAmount")
                {
                    recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
                    RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

                    Session["recieptItem"] = recieptItemList;

                    _paidAmount = 0;
                    foreach (RecieptItemTBS _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sird_settle_amt;
                    }

                    GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
                    //txtPaidAmount.Text = (Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble((grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)]).Cells[19])).ToString();
                    txtAmount.Text = FormatToCurrency((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
                    txtBalanceAmount.Text = FormatToCurrency((Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());

                    //lblPaidAmo.Text = Convert.ToString(_paidAmount);
                    //lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    //textBoxAmount.Text = lblbalanceAmo.Text;

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

                    txtAmount.Text = (txtBalanceAmount.Text);

                    //if (grdPaymentDetails.Rows.Count > 0)
                    //    btnCustomerPaymentAdd.Enabled = false;
                    //else
                    //    btnCustomerPaymentAdd.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void grdPaymentDetails_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteAmount")
                {
                    recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
                    RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

                    Session["recieptItem"] = recieptItemList;

                    _paidAmount = 0;
                    foreach (RecieptItemTBS _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sird_settle_amt;
                    }

                    GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
                    //txtPaidAmount.Text = (Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble((grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)]).Cells[19])).ToString();
                    txtAmount.Text = FormatToCurrency((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
                    txtBalanceAmount.Text = FormatToCurrency((Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());

                    //lblPaidAmo.Text = Convert.ToString(_paidAmount);
                    //lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    //textBoxAmount.Text = lblbalanceAmo.Text;

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

                    txtAmount.Text = (txtBalanceAmount.Text);

                    //if (grdPaymentDetails.Rows.Count > 0)
                    //    btnCustomerPaymentAdd.Enabled = false;
                    //else
                    //    btnCustomerPaymentAdd.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        protected void txtBankCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable _dt = base.CHNLSVC.Sales.GetBankCC(comboBoxCCBank.SelectedValue.ToString());
                //if (_dt.Rows.Count > 0)
                //{
                //    comboBoxCardType.DataSource = _dt;
                //    comboBoxCardType.DisplayMember = "mbct_cc_tp";
                //    comboBoxCardType.ValueMember = "mbct_cc_tp";
                //}
                //else
                //{
                //    comboBoxCardType.DataSource = null;
                //}
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void imgbtnbankcard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBankCard.Text))
                {
                    DisplayMessage("Bank is required.", 2);
                    txtBankCard.Focus();
                    return;
                }
                else
                {
                    if (!CheckBank(txtBankCard.Text, lblbank))
                    {
                        txtBankCard.Text = string.Empty;
                        txtBankCard.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) > 0)
                        {
                            //LoadBankChg();
                            LoadCardType(txtBankCard.Text);
                            //PROMOTION
                            //LoadPromotions();
                            // comboBoxPayModes_SelectionChangeCommitted(null, null);
                        }
                        else
                        {
                            DisplayMessage("Amount is required.", 2);
                            return;
                        }
                    }
                }
                //LoadBankChg();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void imgbtnDepositBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = base.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBankCard.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void imgbtnDepositBranchCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = base.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBranchCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBranchCard.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //LoadMIDno();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
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

                ValidateTrue();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
            }
        }

        private void ValidateTrue()
        {
            DivAlert.Visible = false;
        }

        private void AddPayment()
        {
            string invoiceType = ddlPaymentType.SelectedItem.Value;

            if (Session["recieptItem"] == null)
            {
                recieptItem = new List<RecieptItemTBS>();
            }
            else
            {
                recieptItem = (List<RecieptItemTBS>)Session["recieptItem"];
            }

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
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

            if (String.IsNullOrEmpty(txtBalanceAmount.Text))
            {
                txtBalanceAmount.Text = (Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtAmount.Text)).ToString();

            }

            //if (Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtAmount.Text) < 0)
            //{
            //    DisplayMessage("Please select the valid pay amount", 2);
            //    return;
            //}

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPeriod.Text))
                {
                    DisplayMessage("Please select the valid period", 2);
                    return;
                }
                if (Convert.ToInt32(txtPeriod.Text) <= 0)
                {
                    DisplayMessage("Please select the valid period", 2);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtPeriod.Text))
                _period = 0;
            else
                _period = Convert.ToInt32(txtPeriod.Text);

            _payAmount = Convert.ToDecimal(txtAmount.Text);

            if (recieptItem.Count <= 0)
            {
                RecieptItemTBS _item = new RecieptItemTBS();

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    _cardno = txtCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    //_cardno = txtPayAdvReceiptNo.Text;
                    //chkPayCrPromotion.Checked = false;
                    //_period = 0;
                    //txtPayCrCardType.Text = string.Empty;
                    //txtPayCrBranch.Text = string.Empty;
                    //txtPayCrBank.Text = string.Empty;
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
                        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Bank not found for code');", true);
                        //return;
                        DisplayMessage("Bank not found for code", 2);
                        return;
                    }

                    if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        if (string.IsNullOrEmpty(txtBranchCheque.Text))
                        {
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque branch');", true);
                            //txtBranchCheque.Focus();
                            //return;
                            DisplayMessage("Please enter cheque branch", 2);
                            return;
                        }

                        if (txtChequeNo.Text.Length != 6)
                        {
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter correct cheque number. [Cheque number should be 6 numbers.]');", true);
                            //txtChequeNo.Focus();
                            //return;
                            DisplayMessage("Please enter correct cheque number. [Cheque number should be 6 numbers.].", 2);
                            return;
                        }

                        _item.Sird_chq_dt = Convert.ToDateTime(txtChequeDate.Text);
                        _item.Sird_anal_5 = Convert.ToDateTime(txtChequeDate.Text);
                    }

                    _item.Sird_chq_bank_cd = txtBankCheque.Text;
                    _item.Sird_chq_branch = txtBranchCheque.Text;


                    //_item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                    //_item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                    _item.Sird_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                    _item.Sird_deposit_branch = txtDepositBranchCheque.Text;
                    _item.Sird_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNo.Text;
                    //_item.Sard_anal_5 = dateTimePickerExpire.Value.Date;

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

                //_item.Sard_credit_card_bank = txtBankCard.Text;
                //_item.Sard_chq_branch = txtBranchCard.Text;
                //_item.Sard_ref_no = txtCardNo.Text;    

                //_item.Sird_inv_no = txtInvoice.Text;
                _item.Sird_inv_no = "";

                _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _item.Sird_rmk = txtRemark.Text;
                _paidAmount += _payAmount;

                recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in recieptItem
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
                    _item.Sird_inv_no = "";

                    //_item.Sard_chq_bank_cd = txtBankCheque.Text;
                    //_item.Sard_chq_branch = txtBranchCheque.Text;
                    //_item.Sard_credit_card_bank = null;
                    _item.Sird_deposit_bank_cd = txtDepositBank.Text;
                    _item.Sird_deposit_branch = txtDepositBranch.Text;
                    _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount += _payAmount;
                    recieptItem.Add(_item);
                }
                else
                {
                    DisplayMessage("You can not add duplicate", 2);
                    return;
                    //DisplayMessages("You can not add duplicate payments");
                    //return;
                }
            }

            Session["recieptItem"] = recieptItem;
            grdPaymentDetails.DataSource = recieptItem;
            grdPaymentDetails.DataBind();

            //txtPaidAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtPaidAmount.Text) + Convert.ToDecimal(_paidAmount)));
            //txtBalanceAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //txtAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //txtBalanceAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(_paidAmount)));
            txtBalanceAmount.Text = (Convert.ToDecimal(txtTotalAmount.Text) - recieptItem.Sum(x => x.Sird_settle_amt)).ToString();
            txtAmount.Text = txtBalanceAmount.Text;

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

            recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];

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

        private bool CheckBank(string bank, Label lbl)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());

                if (_bankAccounts.Mbi_cd != null)
                {
                    if (_paymentTypeRef == null)
                    {
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef == null)
                    {
                        DisplayMessage("invalid pay mode.", 2);
                        return false;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
                        _paymentTypeRef = _paymentTypeRef1;
                    }

                    var _promo = (from _prom in _paymentTypeRef
                                  where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
                                  select _prom).ToList();

                    foreach (PaymentType _type in _promo)
                    {
                        if (_type.Stp_pd != null && _type.Stp_pd > 0 && _type.Stp_bank == txtBankCard.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                        {
                            pnlPermotion.Visible = true;
                            chkPromotion.Checked = false;
                        }
                    }
                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the valid bank.');", true);
                    return false;
                }
            }
            return false;

        }

        protected void LoadCardType(string bank)
        {
            MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
            if (_bankAccounts != null)
            {
                DataTable _dt = base.CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
                if (_dt.Rows.Count > 0)
                {
                    ddlCardType.DataSource = _dt;
                    ddlCardType.DataTextField = "mbct_cc_tp";
                    ddlCardType.DataValueField = "mbct_cc_tp";
                    ddlCardType.DataBind();
                }
                else
                {
                    ddlCardType.DataSource = null;
                }

                var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

                if (dr.Count() > 0)
                    ddlCardType.SelectedValue = "VISA";
            }
        }

        private void LoadPayMode()
        {
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, base.GlbChannel, GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
            ddlPayMode.DataSource = _paymentTypeRef;
            ddlPayMode.DataTextField = "Stp_pay_tp";
            ddlPayMode.DataValueField = "Stp_pay_tp";
            ddlPayMode.DataBind();
        }

        private List<RecieptItem> ConvertRecipt(List<RecieptItemTBS> recieptItemTBS)
        {
            List<RecieptItem> outPut = new List<RecieptItem>();

            if (recieptItemTBS != null)
            {
                foreach (RecieptItemTBS item in recieptItemTBS)
                {
                    RecieptItem NewItem = new RecieptItem();
                    NewItem.Sard_seq_no = item.Sird_seq_no;
                    NewItem.Sard_line_no = item.Sird_line_no;
                    NewItem.Sard_receipt_no = item.Sird_receipt_no;
                    NewItem.Sard_inv_no = item.Sird_inv_no;
                    NewItem.Sard_pay_tp = item.Sird_pay_tp;
                    NewItem.Sard_ref_no = item.Sird_ref_no;
                    NewItem.Sard_chq_bank_cd = item.Sird_chq_bank_cd;
                    NewItem.Sard_chq_branch = item.Sird_chq_branch;
                    NewItem.Sard_deposit_bank_cd = item.Sird_deposit_bank_cd;
                    NewItem.Sard_deposit_branch = item.Sird_deposit_branch;
                    NewItem.Sard_credit_card_bank = item.Sird_credit_card_bank;
                    NewItem.Sard_cc_tp = item.Sird_cc_tp;
                    NewItem.Sard_cc_expiry_dt = item.Sird_cc_expiry_dt;
                    NewItem.Sard_cc_is_promo = item.Sird_cc_is_promo;
                    NewItem.Sard_cc_period = item.Sird_cc_period;
                    NewItem.Sard_gv_issue_loc = item.Sird_gv_issue_loc;
                    NewItem.Sard_gv_issue_dt = item.Sird_gv_issue_dt;
                    NewItem.Sard_settle_amt = item.Sird_settle_amt;
                    NewItem.Sard_sim_ser = item.Sird_sim_ser;
                    NewItem.Sard_anal_1 = item.Sird_anal_1;
                    NewItem.Sard_anal_2 = item.Sird_anal_2;
                    NewItem.Sard_anal_3 = item.Sird_anal_3;
                    NewItem.Sard_anal_4 = item.Sird_anal_4;
                    NewItem.Sard_anal_5 = item.Sird_anal_5;
                    NewItem.Sard_chq_dt = item.Sird_chq_dt;
                    NewItem.Sard_cc_batch = item.Sird_cc_batch;
                    NewItem.Sard_rmk = item.Sird_rmk;
                    outPut.Add(NewItem);
                }
            }

            return outPut;
        }

        private List<RecieptItemTBS> ConvertReciptTBS(List<RecieptItem> recieptItemTs)
        {
            List<RecieptItemTBS> outPut = new List<RecieptItemTBS>();
            foreach (RecieptItem NewItem in recieptItemTs)
            {
                RecieptItemTBS item = new RecieptItemTBS();
                item.Sird_seq_no = NewItem.Sard_seq_no;
                item.Sird_line_no = NewItem.Sard_line_no;
                item.Sird_receipt_no = NewItem.Sard_receipt_no;
                item.Sird_inv_no = NewItem.Sard_inv_no;
                item.Sird_pay_tp = NewItem.Sard_pay_tp;
                item.Sird_ref_no = NewItem.Sard_ref_no;
                item.Sird_chq_bank_cd = NewItem.Sard_chq_bank_cd;
                item.Sird_chq_branch = NewItem.Sard_chq_branch;
                item.Sird_deposit_bank_cd = NewItem.Sard_deposit_bank_cd;
                item.Sird_deposit_branch = NewItem.Sard_deposit_branch;
                item.Sird_credit_card_bank = NewItem.Sard_credit_card_bank;
                item.Sird_cc_tp = NewItem.Sard_cc_tp;
                item.Sird_cc_expiry_dt = NewItem.Sard_cc_expiry_dt;
                item.Sird_cc_is_promo = NewItem.Sard_cc_is_promo;
                item.Sird_cc_period = NewItem.Sard_cc_period;
                item.Sird_gv_issue_loc = NewItem.Sard_gv_issue_loc;
                item.Sird_gv_issue_dt = NewItem.Sard_gv_issue_dt;
                item.Sird_settle_amt = NewItem.Sard_settle_amt;
                item.Sird_sim_ser = NewItem.Sard_sim_ser;
                item.Sird_anal_1 = NewItem.Sard_anal_1;
                item.Sird_anal_2 = NewItem.Sard_anal_2;
                item.Sird_anal_3 = NewItem.Sard_anal_3;
                item.Sird_anal_4 = NewItem.Sard_anal_4;
                item.Sird_anal_5 = NewItem.Sard_anal_5;
                item.Sird_chq_dt = NewItem.Sard_chq_dt;
                item.Sird_cc_batch = NewItem.Sard_cc_batch;
                item.Sird_rmk = NewItem.Sard_rmk;
                outPut.Add(item);
            }
            return outPut;
        }

        private List<TR_LOGSHEET_DET> ConvertInvoiceItmToLog(List<InvoiceItem> InputItems)
        {
            List<TR_LOGSHEET_DET> outPutItems = new List<TR_LOGSHEET_DET>();
            foreach (InvoiceItem item in InputItems)
            {
                TR_LOGSHEET_DET oNewItem = new TR_LOGSHEET_DET();
                oNewItem.TLD_CHR_CD = item.Sad_itm_cd;
                oNewItem.TLD_CHR_DESC = item.Sad_alt_itm_desc;
                oNewItem.TLD_QTY = item.Sad_qty;
                oNewItem.TLD_U_RT = item.Sad_unit_rt;
                oNewItem.TLD_TOT = item.Sad_tot_amt;
                oNewItem.TLD_SEQ = (item.Sad_promo_cd == "") ? 0 : Convert.ToInt32(item.Sad_promo_cd);
                if (!string.IsNullOrEmpty(item.Sad_warr_remarks))
                {
                    oNewItem.TLH_DT = Convert.ToDateTime(item.Sad_warr_remarks);
                }
                outPutItems.Add(oNewItem);
            }
            return outPutItems;
        }

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAlert.Visible = false;
        }

        protected void btnPaymentNo_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiveSeach);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Invoice(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtPaymentNo.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtPaymentNo.Focus();
        }

        protected void ImgPaymentNoserch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
            {
                RecieptHeader oRecieptHeader = null;
                string err;

                List<InvoiceItem> oMainItemsList = new List<InvoiceItem>();
                RecieptHeader _ReceiptHeader = new RecieptHeader();

                List<RecieptItem> recieptItem = new List<RecieptItem>();


                int asd = CHNLSVC.Tours.GetInvoiceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtPaymentNo.Text.Trim(), out oHeader, out oMainItemsList, out oRecieptHeader, out recieptItem, out   err);

                if (oHeader.Sah_stus == "C")
                {
                    DisplayMessage("Invoice is canceled", 2);
                    return;
                }
                if (oHeader.Sah_anal_2 != "TOUR")
                {
                    DisplayMessage("Selected invoice is not a transport invoice", 2);
                    return;
                }

                recieptItemList = ConvertReciptTBS(recieptItem);
                oMainItems = ConvertInvoiceItmToLog(oMainItemsList);

                if (asd < 0)
                {
                    DisplayMessage(err, 2);
                    return;
                }

                Session["oMainItems"] = oMainItems;
                bindTOGrid();

                Session["recieptItem"] = recieptItemList;
                grdPaymentDetails.DataSource = recieptItemList; ;
                grdPaymentDetails.DataBind();
                btnCreate.Enabled = false;
                btnCancel.Visible = true;
            }
        }

        protected void txtPaymentNo_TextChanged(object sender, EventArgs e)
        {
            ImgPaymentNoserch_Click(null, null);
        }

        protected void txtCustCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}