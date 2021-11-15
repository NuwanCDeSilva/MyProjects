using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using System.Web.UI.HtmlControls;

namespace FF.AbansTours
{
    public partial class Invoice : BasePage
    {
        private BasePage _basePage;
        private List<InvoiceItem> oMainItemsList = null;
        private InvoiceHeader oHeader = null;
        private List<RecieptItem> _recieptItem;
        private decimal _paidAmount;
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;

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

                        txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                        LoadCurrancyCodes();

                        LoadInvoiceType();
                        loadPaymenttypes();
                        loadCostCate();
                        LoadExecutive();

                        loadPriceDefinition();
                        LoadPriceDefaultValue();

                        CheckDiscount();

                        loadPackageTypes();

                        string id = Request.QueryString["htenus"];
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (Session["EnquiryID"] != null)
                            {
                                txtEnquiryID.Text = Session["EnquiryID"].ToString();
                                txtEnquiryID_TextChanged(null, null);
                            }
                        }

                        Session["_recieptItem"] = null;
                        Session["oHeader"] = null;
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
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + ddlPayMode.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiveSeach:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtCusCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ChargeCodeTours:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnsearchEnquiry_Click(object sender, ImageClickEventArgs e)
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
            if (!string.IsNullOrEmpty(txtEnquiryID.Text))
            {
                loadEnquiryDetails(txtEnquiryID.Text);
                loadCostSheet();
            }
        }

        private void loadEnquiryDetails(string enquiryNum)
        {
            GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), enquiryNum);
            if (oItem != null || oItem.GCE_ENQ != null)
            {
                txtCusCode.Text = oItem.GCE_CUS_CD;
                txtaddress.Text = oItem.GCE_ADD1;
                txtMobile.Text = oItem.GCE_MOB;
                lblManrefNumber.Text = oItem.GCE_REF;

                txtCusCode_TextChanged(null, null);

                if (oItem.GCE_STUS == (int)EnquiryStages.Invoiced)
                {
                    DisplayMessages("This enquiry is already invoiced.");
                    btnCreate.Enabled = false;
                    btnCancel.Visible = true;
                }
            }
        }

        private void LoadCurrancyCodes()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                ddlItmCurncy.DataSource = _cur;
                ddlItmCurncy.DataTextField = "Mcr_cd";
                ddlItmCurncy.DataValueField = "Mcr_cd";
                ddlItmCurncy.DataBind();
                ddlItmCurncy.SelectedValue = "USD";
            }
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
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

        private void bindTOGrid()
        {
            if (Session["oMainItemsList"] != null)
            {
                oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                Session["oMainItemsList"] = oMainItemsList;
                dgvInvoiceItems.DataSource = oMainItemsList;
                dgvInvoiceItems.DataBind();
                modifyGRD();
            }
        }

        private void modifyGRD()
        {
            for (int i = 0; i < dgvInvoiceItems.Rows.Count; i++)
            {
                GridViewRow row = dgvInvoiceItems.Rows[i];
                Label lblSad_qty = (Label)row.FindControl("lblSad_qty");
                Label lblSad_unit_rt = (Label)row.FindControl("lblSad_unit_rt");
                Label lblSad_tot_amt = (Label)row.FindControl("lblSad_tot_amt");

                lblSad_qty.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_qty.Text));
                lblSad_unit_rt.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_unit_rt.Text));
                lblSad_tot_amt.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblSad_tot_amt.Text));
            }

            CalculateTotal();
        }

        private void loadCostSheet()
        {
            string err;
            QUO_COST_HDR oCostHeader = null;
            List<QUO_COST_DET> oCostMainItems = null;

            Int32 result = CHNLSVC.Tours.getCostSheetDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text, "1", out oCostHeader, out oCostMainItems, out err);

            if (oCostMainItems.Count > 0)
            {
                Session["oCostMainItems"] = oCostMainItems;
                Session["oCostHeader"] = oCostHeader;

                txtPax.Text = oCostHeader.QCH_TOT_PAX.ToString();

                //if (oCostHeader.QCH_MARKUP != null && oCostHeader.QCH_MARKUP > 0)
                //{
                //}

                oMainItemsList = new List<InvoiceItem>();

                foreach (QUO_COST_DET item in oCostMainItems)
                {
                    InvoiceItem oItem = new InvoiceItem();
                    oItem.Sad_itm_cd = item.QCD_SUB_CATE;
                    oItem.Sad_itm_stus = "GOD";
                    oItem.Sad_alt_itm_desc = item.QCD_DESC;
                    oItem.Sad_qty = oCostHeader.QCH_TOT_PAX;

                    if (oCostHeader.QCH_MARKUP != null && oCostHeader.QCH_MARKUP > 0)
                    {
                        oItem.Sad_unit_rt = item.QCD_UNIT_COST + ((item.QCD_UNIT_COST * oCostHeader.QCH_MARKUP) / 100);
                        oItem.Sad_tot_amt = item.QCD_TOT_LOCAL + ((item.QCD_TOT_LOCAL * oCostHeader.QCH_MARKUP) / 100);
                    }
                    else
                    {
                        oItem.Sad_unit_rt = item.QCD_UNIT_COST;
                        oItem.Sad_tot_amt = item.QCD_TOT_LOCAL;
                    }
                    oItem.Sad_print_stus = true;
                    oItem.SII_CURR = item.QCD_CURR;
                    oItem.SII_EX_RT = item.QCD_EX_RATE;

                    oMainItemsList.Add(oItem);
                }
                Session["oMainItemsList"] = oMainItemsList;
                bindTOGrid();
            }
        }

        private void CalculateTotal()
        {
            if (Session["oMainItemsList"] != null)
            {
                oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];

                txtInvoiceTotal.Text = oMainItemsList.Sum(x => x.Sad_tot_amt).ToString("N2");
                txtPayAmount.Text = txtInvoiceTotal.Text;
                //lblPayBalance.Text = txtInvoiceTotal.Text;
            }
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            if (_entity.Mbe_is_tax == true)
            {
                chkTaxPayable.Checked = true;
            }
            else
            {
                chkTaxPayable.Checked = false;
            }
            lblSVATStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }

        private void RedirectToBackPage()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["RedirectPage"])))
            {
                Session["isComingBack"] = "1";
                Response.Redirect(Session["RedirectPage"].ToString() + "?htenus=Invoice");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void loadPaymenttypes()
        {
            ddlPayMode.Items.Clear();
            _basePage = new BasePage();
            List<PaymentType> _paymentTypeRef = _basePage.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), Session["UserDefProf"].ToString(), ddlInvoiceType.SelectedItem.ToString(), DateTime.Now);
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            ddlPayMode.DataSource = payTypes;
            ddlPayMode.DataBind();
        }

        protected void LoadCardType(string bank)
        {
            _basePage = new BasePage();
            MasterOutsideParty _bankAccounts = _basePage.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
            if (_bankAccounts != null)
            {
                DataTable _dt = _basePage.CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
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
                    ddlCardType.DataBind();
                }

                var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

                if (dr.Count() > 0)
                    ddlCardType.SelectedValue = "VISA";
            }
        }

        private void AddPayment()
        {
            if (string.IsNullOrEmpty(txtInvoiceTotal.Text))
            {
                DisplayMessages("Please add total invoice amount");
                return;
            }
            if (!isdecimal(txtInvoiceTotal.Text))
            {
                DisplayMessages("Please valid amount");
                return;
            }
            if (String.IsNullOrEmpty(txtDepositBank.Text))
            {
                DisplayMessages("Please enter deposit bank");
                return;
            }
            if (String.IsNullOrEmpty(txtDepositBranch.Text))
            {
                DisplayMessages("Please enter deposit branch");
                return;
            }



            string invoiceType = ddlInvoiceType.SelectedItem.ToString();

            if (Session["_recieptItem"] == null)
            {
                _recieptItem = new List<RecieptItem>();
            }
            else
            {
                _recieptItem = (List<RecieptItem>)Session["_recieptItem"];
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { DisplayMessages("Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { DisplayMessages("Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { DisplayMessages("Please select the valid pay amount"); return; }

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(txtPayAmount.Text);

                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), GlbUserDefProf, invoiceType, DateTime.Now.Date);
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100);
                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                        BankOrOther_Charges = BankOrOtherCharge_;
                        txtPayAmount.Text = FormatToCurrency(Convert.ToString(_selectAmt - BankOrOther_Charges));
                    }
                }
            }

            if (Convert.ToDecimal(txtInvoiceTotal.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            {
                DisplayMessages("Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
            {
                if (string.IsNullOrEmpty(txtCRCDBank.Text))
                {
                    DisplayMessages("Please select the valid bank");
                    txtCRCDBank.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCardNum.Text))
                {
                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        DisplayMessages("Please select the card no");
                    else
                    {
                        DisplayMessages("Please select the cheque no");
                        txtCardNum.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(ddlCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    DisplayMessages("Please select the card type");
                    ddlCardType.Focus();
                    return;
                }
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                //if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text))
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the document no");
                //    txtPayAdvReceiptNo.Focus();
                //    return;
                //}
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtCRCDPeriod.Text))
                {
                    DisplayMessages("Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtCRCDPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtCRCDPeriod.Text) <= 0)
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

            if (string.IsNullOrEmpty(txtCRCDPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtCRCDPeriod.Text);

            if (string.IsNullOrEmpty(txtPayAmount.Text))
            {
                DisplayMessages("Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
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

            _payAmount = Convert.ToDecimal(txtPayAmount.Text);

            if (_recieptItem.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(txtExpireDate.Text))
                {
                    _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireDate.Text).Date;
                }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    _cardno = txtCardNum.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    //_cardno = txtPayAdvReceiptNo.Text;
                    //chkPayCrPromotion.Checked = false;
                    //_period = 0;
                    //txtPayCrCardType.Text = string.Empty;
                    //txtPayCrBranch.Text = string.Empty;
                    //txtPayCrBank.Text = string.Empty;
                }

                _item.Sard_cc_is_promo = chkPromotion.Checked ? true : false;
                _item.Sard_cc_period = _period;
                _item.Sard_cc_tp = txtCardNum.Text;

                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    _item.Sard_chq_bank_cd = txtCRCDBank.Text;
                    _item.Sard_chq_branch = txtCRCDBranch.Text;
                }
                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    _item.Sard_chq_bank_cd = txtChequeBank.Text;
                    _item.Sard_chq_branch = txtChequeBranch.Text;
                }

                _item.Sard_credit_card_bank = txtCRCDBank.Text;
                _item.Sard_ref_no = _cardno;
                _item.Sard_deposit_bank_cd = null;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _paidAmount += _payAmount;

                _recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in _recieptItem
                                 where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
                                 select _dup;
                if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == ddlCardType.Text && _dup.Sard_ref_no == txtCardNum.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == txtCRCDBank.Text && _dup.Sard_ref_no == txtCardNum.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
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
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(txtExpireDate.Text))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireDate.Text).Date; }

                    if (string.IsNullOrEmpty(txtCRCDPeriod.Text.Trim()))
                        txtCRCDPeriod.Text = "0";

                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        //chkPayCrPromotion.Checked = false;
                        //_period = 0;
                        //txtPayCrCardType.Text = string.Empty;
                        //txtPayCrBranch.Text = string.Empty;
                        //txtPayCrBank.Text = string.Empty;
                    }

                    _item.Sard_cc_is_promo = chkPromotion.Checked ? true : false;
                    _item.Sard_cc_period = Convert.ToInt32(txtCRCDPeriod.Text);
                    _item.Sard_cc_period = _period;
                    _item.Sard_cc_tp = ddlCardType.Text;
                    _item.Sard_chq_bank_cd = txtCRCDBank.Text;
                    _item.Sard_chq_branch = txtCRCDBranch.Text;
                    _item.Sard_credit_card_bank = null;
                    _item.Sard_deposit_bank_cd = null;
                    _item.Sard_deposit_branch = null;
                    _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount += _payAmount;
                    _recieptItem.Add(_item);
                }
                else
                {
                    DisplayMessages("You can not add duplicate payments");
                    return;
                }
            }
            Session["_recieptItem"] = _recieptItem;
            gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceTotal.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceTotal.Text) - Convert.ToDecimal(_paidAmount))));

            txtPayRemarks.Text = "";
            txtCardNum.Text = "";
            txtCRCDBank.Text = "";
            txtCRCDBranch.Text = "";
            //ddlCardType.SelectedIndex = 0;
            txtExpireDate.Text = "";
            chkPromotion.Checked = false;
            //txtPayAdvReceiptNo.Text = "";
            //txtPayAdvRefAmount.Text = "";
            txtCRCDPeriod.Text = "";
            // txtPayCrPeriod.Enabled = false;
        }

        private void clearEntryLine()
        {
            txtChargeCode.Text = "";
            txtRemark.Text = "";
            txtPax.Text = "";
            txtUnitRate.Text = "";
            txtTotal.Text = "";
        }

        private void clearPaymentAll()
        {
            txtPayAmount.Text = "";
            txtPayRemarks.Text = "";
            txtDepositBank.Text = "";
            txtDepositBranch.Text = "";
            lblPayPaid.Text = "0.00";
            lblPayBalance.Text = "0.00";

            gvPayment.DataSource = null;
            gvPayment.DataBind();
            txtCardNum.Text = "";
            txtCRCDBank.Text = "";
            txtCRCDBranch.Text = "";
            txtExpireDate.Text = Convert.ToDateTime("31-Dec-" + DateTime.Now.Year.ToString()).ToString("dd/MMM/yyyy");
            chkPromotion.Checked = false;
            txtCRCDPeriod.Text = "";
            txtCRCDPeriod.Enabled = false;

            txtChequeNum.Text = "";
            txtChequeBank.Text = "";
            txtChequeBranch.Text = "";
            txtChequeDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
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
                    ddlInvoiceType.DataSource = _types;
                    ddlInvoiceType.DataBind();

                    //ddlInvoiceType.SelectedIndex = ddlInvoiceType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        ddlInvoiceType.Text = DefaultInvoiceType;
                    }
                }
                else
                    ddlInvoiceType.DataSource = null;
            else
                ddlInvoiceType.DataSource = null;

            return _isAvailable;
        }

        private void ClearAll()
        {
            txtEnquiryID.Text = "";
            txtReffNum.Text = "";
            txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtCusCode.Text = "";
            txtaddress.Text = "";
            txtMobile.Text = "";
            chkTaxPayable.Checked = false;
            lblExemptStatus.Text = "None";
            lblSVATStatus.Text = "None";
            lblManrefNumber.Text = "";

            clearEntryLine();

            dgvInvoiceItems.DataSource = null;
            dgvInvoiceItems.DataBind();
            txtInvoiceTotal.Text = "";
            txtMainRemark.Text = "";
            txtDisAmount.Text = "";
            txtDisPercentage.Text = "";

            clearPaymentAll();

            Session["EnquiryID"] = null;
            Session["oMainItemsList"] = null;
            Session["oCostMainItems"] = null;
            Session["oCostHeader"] = null;
            Session["_recieptItem"] = null;
            Session["Customer"] = null;
            Session["oHeader"] = null;

            //btnCreate.Enabled = true;

            txtEnquiryID.Focus();
            btnCancel.Visible = false;
            btnCreate.Enabled = true;
        }

        private void LoadAdvancedReciept()
        {
            DataTable _dt = CHNLSVC.Sales.GetReceipt(txtADCANReffNumber.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                txtADVANReffAmount.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
            }
            else
            {
                DisplayMessages("Invalid Advanced Receipt No");
                return;
            }
        }

        private void loadCostCate()
        {
            List<MST_COST_CATE> oCate = CHNLSVC.Tours.GET_COST_CATE(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            ddlCostType.DataSource = oCate;
            ddlCostType.DataTextField = "MCC_DESC";
            ddlCostType.DataValueField = "MCC_CD";
            ddlCostType.DataBind();
        }

        private void getChargeCOdeDetails()
        {
            hdfChargeDesc.Value = "";

            if (string.IsNullOrEmpty(cmbPriceBook.SelectedValue))
            {
                DisplayMessages("Please select price book");
                return;
            }

            if (string.IsNullOrEmpty(cmbPriceLevel.SelectedValue))
            {
                DisplayMessages("Please select price level");
                return;
            }

            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                SR_AIR_CHARGE oSR_AIR_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtChargeCode.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.SAC_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_AIR_CHARGE.SAC_CUR;
                   // ddlItmCurncy_SelectedIndexChanged(null, null);
                    //txtUnitRate.Text = oSR_AIR_CHARGE.SAC_RT.ToString();
                    hdfChargeDesc.Value = oSR_AIR_CHARGE.SAC_ADD_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtChargeCode.Text = "";
                    txtChargeCode.Focus();
                    return;
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
            {
                SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtChargeCode.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_AIR_CHARGE.STC_CURR;
                   // ddlItmCurncy_SelectedIndexChanged(null, null);
                    //txtUSD.Text = oSR_AIR_CHARGE.STC_RT.ToString();
                    hdfChargeDesc.Value = oSR_AIR_CHARGE.STC_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtChargeCode.Text = "";
                    txtChargeCode.Focus();
                    return;
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtChargeCode.Text);
                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_SER_MISS.SSM_CUR;
                    //ddlItmCurncy_SelectedIndexChanged(null, null);
                    //txtUSD.Text = oSR_SER_MISS.SSM_RT.ToString();
                    hdfChargeDesc.Value = oSR_SER_MISS.SSM_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtChargeCode.Text = "";
                    txtChargeCode.Focus();
                    return;
                }
            }

            loadPrices();

            if (string.IsNullOrEmpty(txtUnitRate.Text))
            {
                return;
            }

            if (!string.IsNullOrEmpty(txtPax.Text))
            {
                txtTotal.Text = (Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(txtPax.Text)).ToString();

                //txtTotalLKR.Text = (Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(lblItemExRate.Text)).ToString();

                decimal discountAmount = 0;
                decimal discountPercentage = 0;

                if (!string.IsNullOrEmpty(txtDisPercentage.Text))
                {
                    discountPercentage = Convert.ToDecimal(txtDisPercentage.Text);
                    discountAmount = (Convert.ToDecimal(txtDisPercentage.Text) * Convert.ToDecimal(txtTotal.Text)) / 100;
                    //  txtDisAmount.Text = discountAmount.ToString("N2");
                }
                if (!string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    discountAmount = Convert.ToDecimal(txtDisAmount.Text);

                    discountPercentage = ((discountAmount * 100) / Convert.ToDecimal(txtTotal.Text));
                }

                MasterProfitCenter _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                if (_MasterProfitCenter.Mpc_def_dis_rate > 0)
                {
                    if (discountPercentage > _MasterProfitCenter.Mpc_def_dis_rate)
                    {
                        DisplayMessages("Maximum discount rate is " + _MasterProfitCenter.Mpc_def_dis_rate.ToString("N2"));
                        return;
                    }

                    //txtDisAmount.Text = discountAmount.ToString("N2");
                    //txtDisPercentage.Text = discountPercentage.ToString("N2");

                    txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) - discountAmount).ToString("N2");
                }
                btnAddtoGrid.Focus();
            }
        }

        private void LoadExecutive()
        {
            ddlExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            if (_tblExecutive != null)
            {
                //var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                //if (_lst != null && _lst.Count > 0)
                //{
                //    ddlExecutive.DataSource = _lst.CopyToDataTable();
                //}
                ddlExecutive.DataSource = _tblExecutive;

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

                ddlExecutive.DataTextField = "esep_first_name";
                ddlExecutive.DataValueField = "esep_epf";
                ddlExecutive.DataBind();

                ddlExecutive.SelectedValue = _pc.Mpc_man;
            }
        }

        private void loadPriceDefinition()
        {
            _PriceDefinitionRef = CHNLSVC.Tours.GetToursPriceDefByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
            if (_PriceDefinitionRef == null || _PriceDefinitionRef.Count == 0)
            {
                DisplayMessages("please setup price definitions.");
            }
            Session["_PriceDefinitionRef"] = _PriceDefinitionRef;
        }

        private void LoadPriceDefaultValue()
        {
            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"];
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                    if (_defaultValue != null)
                        if (_defaultValue.Count > 0)
                        {
                            //DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                            DefaultBook = _defaultValue[0].Sadd_pb;
                            DefaultLevel = _defaultValue[0].Sadd_p_lvl;
                            //DefaultStatus = _defaultValue[0].Sadd_def_stus;
                            //DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                            LoadInvoiceType();
                            LoadPriceBook(ddlInvoiceType.Text);
                            LoadPriceLevel(ddlInvoiceType.Text, cmbPriceBook.Text.Trim());
                            //LoadLevelStatus(ddlInvoiceType.Text, cmbPriceBook.Text.Trim(), cmbPriceLevel.Text.Trim());
                            // CheckPriceLevelStatusForDoAllow(cmbPriceLevel.Text.Trim(), cmbPriceBook.Text.Trim());
                        }
                }
            //cmbTitle.SelectedIndex = 0;
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"];
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == ddlInvoiceType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbPriceBook.DataSource = _books;
                    //cmbPriceBook.SelectedIndex = cmbPriceBook.Items.Count - 1;

                    //cmbPriceBook.DataTextField = "Sadd_pb";
                    //cmbPriceBook.DataValueField = "Sadd_pb";

                    cmbPriceBook.DataBind();
                    if (!string.IsNullOrEmpty(DefaultBook))
                    {
                        cmbPriceBook.Text = DefaultBook;
                    }
                }
                else
                    cmbPriceBook.DataSource = null;
            else
                cmbPriceBook.DataSource = null;

            return _isAvailable;
        }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"];
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == ddlInvoiceType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbPriceLevel.DataSource = _levels;
                    cmbPriceLevel.DataBind();
                    cmbPriceLevel.SelectedIndex = cmbPriceLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbPriceBook.Text)) cmbPriceLevel.Text = DefaultLevel;

                    // _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _book.Trim(), cmbPriceLevel.Text.Trim());
                    //LoadPriceLevelMessage();
                }
                else
                    cmbPriceLevel.DataSource = null;
            else cmbPriceLevel.DataSource = null;

            return _isAvailable;
        }

        private void loadPrices()
        {
            if (string.IsNullOrEmpty(ddlInvoiceType.SelectedValue))
            {
                //   DisplayMessages("Please select invoice Type");
                return;
            }

            if (string.IsNullOrEmpty(cmbPriceBook.SelectedValue))
            {
                //  DisplayMessages("Please select price book");
                return;
            }

            if (string.IsNullOrEmpty(cmbPriceLevel.SelectedValue))
            {
                //  DisplayMessages("Please select price level");
                return;
            }
            if (string.IsNullOrEmpty(txtChargeCode.Text))
            {
                //  DisplayMessages("Please select a charge code");
                return;
            }

            if (string.IsNullOrEmpty(txtPax.Text))
            {
                //  DisplayMessages("Please enter PAX");
                return;
            }

            List<PriceDetailRef> oPrices = CHNLSVC.Tours.GetToursPrice(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlInvoiceType.SelectedValue.ToString(), cmbPriceBook.SelectedValue.ToString(), cmbPriceLevel.SelectedValue.ToString(), txtCusCode.Text, txtChargeCode.Text, Convert.ToDecimal(txtPax.Text), Convert.ToDateTime(txtdate.Text));
            if (oPrices == null || oPrices.Count == 0)
            {
              //  DisplayMessages("There is no price for the selected item");
                txtUnitRate.Focus();
                return;
            }
            else
            {
                txtUnitRate.Text = oPrices[0].Sapd_itm_price.ToString("N2");
            }
        }

        private void CheckDiscount()
        {
            //MasterProfitCenter _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            //if (_MasterProfitCenter.Mpc_def_dis_rate > 0)
            //{
            //    txtDisAmount.Enabled = true;
            //    txtDisPercentage.Enabled = true;
            //}
            //else
            //{
            //    txtDisAmount.Enabled = false;
            //    txtDisPercentage.Enabled = false;
            //}
        }

        protected bool CheckNewDiscountRate()
        {
            bool _isedit = false;
            MasterProfitCenter _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            if (_MasterProfitCenter.Mpc_def_dis_rate > 0)
            {
                _isedit = true;
            }
            if (string.IsNullOrEmpty(txtChargeCode.Text)) return false;
            if (isdecimal(txtPax.Text) == false)
            {
                DisplayMessages("Please select the valid qty");
                return false;
            }
            if (Convert.ToDecimal(txtPax.Text.Trim()) == 0)
            {
                return false;
            }

            CashGeneralEntiryDiscountDef GeneralDiscount = new CashGeneralEntiryDiscountDef();

            if (!string.IsNullOrEmpty(txtDisPercentage.Text) && _isedit == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisPercentage.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(base.GlbUserComCode, base.GlbUserDefProf, Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbPriceBook.Text.Trim(), cmbPriceLevel.Text.Trim(), txtCusCode.Text.Trim(), txtChargeCode.Text.Trim(), false, false);

                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        //{
                        //    DisplayMessages("Voucher already used!");
                        //    txtDisPercentage.Text = FormatToCurrency("0");
                        //    txtDisPercentage.Enabled = false;
                        //    txtDisAmount.Enabled = false;
                        //    return false;
                        //}

                        if (_IsPromoVou == true)
                        {
                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmount.Text) != vals)
                                {
                                    DisplayMessages("Voucher discount amount should be " + vals + ". Not allowed discount rate " + _disRate + "%");
                                    txtDisPercentage.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    return false;
                                }
                            }
                            else
                            {
                                if (rates != _disRate)
                                {
                                    CalculateItem();
                                    DisplayMessages("Voucher discount rate should be " + rates + "% !. Not allowed discount rate " + _disRate + "% discounted price is " + txtTotal.Text);
                                    txtDisPercentage.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (rates < _disRate)
                            {
                                CalculateItem();
                                DisplayMessages("Exceeds maximum discount allowed " + rates + "% !. " + _disRate + "% discounted price is " + txtTotal.Text);
                                txtDisPercentage.Text = FormatToCurrency("0");
                                CalculateItem();
                                return false;
                            }
                            else
                            {
                                txtDisPercentage.Enabled = true;
                                txtDisAmount.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        DisplayMessages("You are not allow for apply discount");
                        txtDisPercentage.Text = FormatToCurrency("0");
                        return false;
                    }
                }
                else
                {
                    //txtDisPercentage.Enabled = false;
                    //txtDisAmount.Enabled = false;
                }
            }
            else if (_isedit == true)
            {
                txtDisPercentage.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisPercentage.Text))
            {
                txtDisPercentage.Text = FormatToCurrency("0");
            }
            decimal val = Convert.ToDecimal(txtDisPercentage.Text);
            txtDisPercentage.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            btnAddtoGrid.Focus();
            return true;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtPax.Text))
            {
                txtTotal.Text = (Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(txtPax.Text)).ToString();

                //txtTotalLKR.Text = (Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(lblItemExRate.Text)).ToString();

                decimal discountAmount = 0;
                decimal discountPercentage = 0;

                if (!string.IsNullOrEmpty(txtDisPercentage.Text))
                {
                    discountPercentage = Convert.ToDecimal(txtDisPercentage.Text);
                    discountAmount = (Convert.ToDecimal(txtDisPercentage.Text) * Convert.ToDecimal(txtTotal.Text)) / 100;
                    //  txtDisAmount.Text = discountAmount.ToString("N2");
                }
                if (!string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    discountAmount = Convert.ToDecimal(txtDisAmount.Text);

                    discountPercentage = ((discountAmount * 100) / Convert.ToDecimal(txtTotal.Text));
                }

                MasterProfitCenter _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                if (_MasterProfitCenter.Mpc_def_dis_rate > 0)
                {
                    if (discountPercentage > _MasterProfitCenter.Mpc_def_dis_rate)
                    {
                        DisplayMessages("Maximum discount rate is " + _MasterProfitCenter.Mpc_def_dis_rate.ToString("N2"));
                        return;
                    }

                    //txtDisAmount.Text = discountAmount.ToString("N2");
                    //txtDisPercentage.Text = discountPercentage.ToString("N2");

                }
                txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) - discountAmount).ToString("N2");
            }
        }

        private void loadPackageTypes()
        {
            List<ComboBoxObject> oItems = CHNLSVC.Tours.GET_TOUR_PACKAGE_TYPES();
            ddlPackageType.DataSource = oItems;
            ddlPackageType.DataMember = "Value";
            ddlPackageType.DataTextField = "Text";
            ddlPackageType.DataBind();
        }

        private decimal calcutaleTotalWithCurancy(List<InvoiceItem> oItemsList)
        {
            decimal totalLkr = 0;

            return totalLkr;
        }

        protected void txtPax_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPax.Text))
            {
                if (isdecimal(txtPax.Text))
                {
                    getChargeCOdeDetails();
                }
                else
                {
                    DisplayMessages("Please enter valid PAX");
                    txtPax.Text = "";
                    txtPax.Focus();
                }
            }
        }

        protected void txtUnitRate_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUnitRate.Text))
            {
                if (isdecimal(txtUnitRate.Text))
                {
                    decimal total = Convert.ToDecimal(txtUnitRate.Text) * Convert.ToDecimal(lblItemExRate.Text) * Convert.ToDecimal(txtPax.Text);
                    txtTotal.Text = total.ToString();
                    txtTotal.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", total);
                    txtUnitRate.Focus();
                }
                else
                {
                    DisplayMessages("Please enter valid Unite Rate");
                    txtUnitRate.Text = "";
                    txtUnitRate.Focus();
                }
            }
        }

        protected void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTotal.Text))
            {
                if (isdecimal(txtTotal.Text))
                {
                    //btnAddtoGrid.Focus();
                }
                else
                {
                    DisplayMessages("Please enter valid total");
                    txtTotal.Text = "";
                    txtTotal.Focus();
                }
            }
        }

        protected void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRemark.Text))
            {
                //btnAddtoGrid.Focus();
            }
            else
            {
                DisplayMessages("Please enter remark");
                txtRemark.Text = "";
                txtRemark.Focus();
            }
        }

        protected void btnAddtoGrid_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToDecimal(lblItemExRate.Text) == 0)
            {
                DisplayMessages("Please update the exchange rate");
                txtChargeCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtChargeCode.Text))
            {
                DisplayMessages("Please enter a charge code");
                txtChargeCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                DisplayMessages("Please enter a remark");
                txtRemark.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPax.Text))
            {
                DisplayMessages("Please enter a PAX");
                txtPax.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUnitRate.Text))
            {
                DisplayMessages("Please enter unite rate");
                txtUnitRate.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtTotal.Text))
            {
                DisplayMessages("Please enter total amount");
                txtTotal.Focus();
                return;
            }

            if (!isdecimal(lblItemExRate.Text))
            {
                DisplayMessages("please update exchange details");
                return;
            }

            if (Session["oMainItemsList"] != null)
            {
                oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
            }
            else
            {
                oMainItemsList = new List<InvoiceItem>();
            }

            InvoiceItem oItem = new InvoiceItem();
            oItem.Sad_itm_cd = txtChargeCode.Text;
            oItem.Sad_alt_itm_desc = txtRemark.Text;
            oItem.Sad_itm_stus = "GOD";
            oItem.Sad_qty = Convert.ToDecimal(txtPax.Text);
            oItem.Sad_unit_rt = Convert.ToDecimal(txtUnitRate.Text);
            oItem.Sad_tot_amt = oItem.Sad_unit_rt * Convert.ToDecimal(lblItemExRate.Text) * oItem.Sad_qty;
            oItem.Sad_print_stus = true;
            oItem.SII_CURR = ddlItmCurncy.SelectedValue.ToString();
            oItem.SII_EX_RT = Convert.ToDecimal(lblItemExRate.Text);
            oMainItemsList.Add(oItem);
            Session["oMainItemsList"] = oMainItemsList;
            bindTOGrid();

            clearEntryLine();
        }

        protected void txtCusCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCusCode.Text != "")
            {
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCusCode.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                Session["Customer"] = _masterBusinessCompany;
                LoadTaxDetail(_masterBusinessCompany);

                txtaddress.Text = _masterBusinessCompany.Mbe_add1;
                txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            }
        }

        protected void dgvInvoiceItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "delete")
                {
                    GridViewRow row = dgvInvoiceItems.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblQCD_CAT = (Label)row.FindControl("lblQCD_CAT");

                    oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];
                    oMainItemsList.RemoveAt(Convert.ToInt32(e.CommandArgument));
                    Session["oMainItemsList"] = oMainItemsList;
                    bindTOGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void dgvInvoiceItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void dgvInvoiceItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            RedirectToBackPage();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReffNum.Text))
            {
                DisplayMessages("Already Invoiced.");
                return;

            }
            if (dgvInvoiceItems.Rows.Count <= 0)
            {
                DisplayMessages("Please add Items");
                return;
            }
            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                DisplayMessages("Please select a customer");
                return;
            }
            if (string.IsNullOrEmpty(txtInvoiceTotal.Text))
            {
                DisplayMessages("please add amount");
                return;
            }
            if (Convert.ToDecimal(txtInvoiceTotal.Text) == 0)
            {
                DisplayMessages("You cant save zero values");
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

            if (ddlInvoiceType.Text == "CS")
            {
                if (Session["_recieptItem"] == null)
                {
                    DisplayMessages("please add payments");
                    return;
                }
            }
            if (Convert.ToDateTime(txtdate.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please select a valid date");
                return;
            }

            if (ddlExecutive.SelectedValue == null)
            {
                DisplayMessages("Please select sales executive.");
                return;
            }

            MasterBusinessEntity oCust = (MasterBusinessEntity)Session["Customer"];
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
            oHeader.Sah_dt = Convert.ToDateTime(txtdate.Text);
            oHeader.Sah_epf_rt = 0;
            oHeader.Sah_esd_rt = 0;
            oHeader.Sah_ex_rt = 1;
            oHeader.Sah_inv_no = "na";
            oHeader.Sah_inv_sub_tp = "SA";
            oHeader.Sah_inv_tp = ddlInvoiceType.SelectedItem.ToString();
            oHeader.Sah_is_acc_upload = false;
            oHeader.Sah_man_ref = lblManrefNumber.Text;
            oHeader.Sah_manual = false;
            oHeader.Sah_mod_by = Session["UserID"].ToString();
            oHeader.Sah_mod_when = DateTime.Now;
            oHeader.Sah_pc = Session["UserDefProf"].ToString();
            oHeader.Sah_pdi_req = 0;
            oHeader.Sah_ref_doc = txtEnquiryID.Text;
            oHeader.Sah_sales_chn_cd = "";
            oHeader.Sah_sales_chn_man = "";
            oHeader.Sah_sales_ex_cd = ddlExecutive.SelectedValue.ToString();
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
            oHeader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
            //oHeader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
            //oHeader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            oHeader.Sah_del_loc = string.Empty;
            //oHeader.Sah_grn_com = _customerCompany;
            //oHeader.Sah_grn_loc = _customerLocation;
            //oHeader.Sah_is_grn = _isCustomerHasCompany;
            //oHeader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            oHeader.Sah_is_svat = lblSVATStatus.Text == "Available" ? true : false;
            oHeader.Sah_tax_exempted = lblExemptStatus.Text == "Available" ? true : false;
            oHeader.Sah_anal_2 = "SCV";
            oHeader.Sah_anal_3 = ddlPackageType.Text;
            //oHeader.Sah_anal_6 = txtLoyalty.Text.Trim();
            oHeader.Sah_man_cd = Session["UserDefProf"].ToString();
            oHeader.Sah_is_dayend = 0;
            oHeader.Sah_remarks = txtMainRemark.Text;

            _recieptItem = (List<RecieptItem>)Session["_recieptItem"];

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
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtdate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = oCust.MBE_FNAME;
            _ReceiptHeader.Sar_debtor_add_1 = oCust.Mbe_add1;
            _ReceiptHeader.Sar_debtor_add_2 = oCust.Mbe_add2;
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = txtMobile.Text.Trim();
            _ReceiptHeader.Sar_nic_no = oCust.Mbe_nic;
            if (ddlInvoiceType.Text != "CRED")
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

            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _invoiceAuto.Aut_cate_tp = "TINVO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CS";
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = "TINVO";
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtdate.Text).Year;

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
                    _receiptAuto.Aut_year = Convert.ToDateTime(txtdate.Text).Year;
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

            int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, null, _ReceiptHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, null, null, null, null, out _buybackadj, false);

            // int result = _basePage.CHNLSVC.Tours.SaveToursrInvoice(oHeader, oMainItemsList, _invoiceSerial, _ReceiptHeader, _recieptItem, _inventoryHeader, _pickSerial, _pickSubSerial, _invoiceAuto, _receiptAuto, _inventoryAuto, false, out _invoiceNo, out _receiptNo, out _deliveryOrder, oCust, false, false, out _error, _voucher, _buybackheader, _buybackauto, _buybacklist, out _buybackadj);
            if (result > 0)
            {
                DisplayMessages("Successfully saved. Invoice : " + _invoiceNo);
                ClearAll();
                Session["invoiceNum"] = _invoiceNo;
                Response.Redirect("InvoicePrint3.aspx");
                //mpInvoicePrint.Show();
            }
            else
            {
                DisplayMessages("Error Occurred" + _error);
            }
        }

        protected void btnPayAdd_Click(object sender, EventArgs e)
        {
            AddPayment();
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

            if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                MultiView1.ActiveViewIndex = 2;
            }
            else if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
            {
                MultiView1.ActiveViewIndex = 3;
            }
        }

        protected void btnDepositBank_Click(object sender, ImageClickEventArgs e)
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
            txtDepositBank.Focus();
        }

        protected void btnDepositBranch_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtDepositBranch.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtDepositBranch.Focus();
        }

        protected void btnCRCDBank_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCRCDBank.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtCRCDBank.Focus();

            LoadCardType(txtCRCDBank.Text);
        }

        protected void btnCRCDBranch_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCRCDBranch.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtCRCDBranch.Focus();
        }

        protected void txtCRCDBank_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCRCDBank.Text))
            {
                LoadCardType(txtCRCDBank.Text);
            }
        }

        protected void chkPromotion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPromotion.Checked)
            {
                txtCRCDPeriod.Enabled = true;
            }
            else
            {
                txtCRCDPeriod.Enabled = true;
                txtCRCDPeriod.Text = "";
            }
        }

        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string invoiceType = ddlInvoiceType.SelectedItem.ToString();

            if (Session["_recieptItem"] == null)
            {
                return;
            }
            else
            {
                _recieptItem = (List<RecieptItem>)Session["_recieptItem"];
            }

            int row_id = e.RowIndex;
            string _payType = (string)gvPayment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            _temp = _recieptItem;

            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;
            _paidAmount = 0;
            foreach (RecieptItem _list in _temp)
            {
                _paidAmount += _list.Sard_settle_amt;
            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceTotal.Text) - Convert.ToDecimal(_paidAmount))));

            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), Session["UserDefProf"].ToString(), ddlInvoiceType.SelectedItem.ToString(), DateTime.Now);

            Decimal BankOrOtherCharge = 0;
            Decimal BankOrOther_Charges = 0;

            foreach (PaymentType pt in _paymentTypeRef)
            {
                if (_payType == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = Convert.ToDecimal(lblPayBalance.Text.Trim()) * BCR / 100;

                    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    BankOrOtherCharge = BankOrOtherCharge + BCV;

                    BankOrOther_Charges = BankOrOtherCharge;
                }
            }
            if (BankOrOther_Charges > 0)
                txtPayAmount.Text = FormatToCurrency((Convert.ToDecimal(txtInvoiceTotal.Text) - _paidAmount + Math.Round(BankOrOther_Charges)).ToString());
            else
                txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceTotal.Text) - Convert.ToDecimal(_paidAmount))));
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('InvoicePrint2.aspx');", true);
            ClearAll();
        }

        protected void btnSearchInvoice_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiveSeach);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Invoice(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtReffNum.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtReffNum.Focus();
        }

        protected void txtReffNum_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReffNum.Text))
            {
                RecieptHeader oRecieptHeader = null;
                string err;

                int asd = CHNLSVC.Tours.GetInvoiceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtReffNum.Text.Trim(), out oHeader, out oMainItemsList, out oRecieptHeader, out _recieptItem, out   err);

                if (asd < 0)
                {
                    DisplayMessages(err);
                    return;
                }

                txtdate.Text = oHeader.Sah_dt.ToString("dd/MMMM/yyyy");
                txtEnquiryID.Text = oHeader.Sah_ref_doc;
                txtReffNum.Text = oHeader.Sah_inv_no;
                Session["oMainItemsList"] = oMainItemsList;
                bindTOGrid();

                Session["_recieptItem"] = _recieptItem;
                gvPayment.DataSource = _recieptItem; ;
                gvPayment.DataBind();
                btnCreate.Enabled = false;
                btnCancel.Visible = true;
            }
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
            ucc.ReturnResultControl = txtCusCode.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtCusCode_TextChanged(null, null);
        }

        protected void btnChqBank_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtChequeBank.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtChequeBank.Focus();
        }

        protected void btnADVANReffNumber_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtADCANReffNumber.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void txtADCANReffNumber_TextChanged(object sender, EventArgs e)
        {
            LoadAdvancedReciept();
        }

        protected void btnChargeType_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
            //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ChargeCode(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtChargeCode.ClientID;
            //ucc.UCModalPopupExtender.Show();

            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ChargeCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtChargeCode.ClientID;
                ucc.UCModalPopupExtender.Show();
            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
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
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Miscellaneous(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtChargeCode.ClientID;
                ucc.UCModalPopupExtender.Show();
            }
        }

        protected void txtChargeCode_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtChargeCode.Text))
            {
                getChargeCOdeDetails();
                txtRemark.Focus();
                ddlItmCurncy_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlItmCurncy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItmCurncy.SelectedValue != "")
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), ddlItmCurncy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, Session["UserDefProf"].ToString());
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                    lblItemExRate.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);
                    ddlItmCurncy.Focus();
                }
                else
                {
                    DisplayMessages("please update exchange rates for selected currency");
                    ddlItmCurncy.Focus();
                }
            }
        }

        protected void txtChargeCode_TextChanged1(object sender, EventArgs e)
        {
        }

        protected void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            oMainItemsList = (List<InvoiceItem>)Session["oMainItemsList"];

            for (int i = 0; i < dgvInvoiceItems.Rows.Count; i++)
            {
                CheckBox chkPrint = (CheckBox)dgvInvoiceItems.Rows[i].FindControl("chkPrint");
                if (chkPrint.Checked)
                {
                    oMainItemsList[i].Sad_print_stus = true;
                }
                else
                {
                    oMainItemsList[i].Sad_print_stus = false;
                }
            }

            Session["oMainItemsList"] = oMainItemsList;
        }

        protected void chkPrintAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkPrintAll = (CheckBox)dgvInvoiceItems.HeaderRow.FindControl("chkPrintAll");
            if (chkPrintAll.Checked == true)
            {
                foreach (GridViewRow gvRow in dgvInvoiceItems.Rows)
                {
                    CheckBox chkPrint = (CheckBox)gvRow.FindControl("chkPrint");
                    chkPrint.Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow gvRow in dgvInvoiceItems.Rows)
                {
                    CheckBox chkPrint = (CheckBox)gvRow.FindControl("chkPrint");
                    chkPrint.Checked = false;
                }
            }
        }

        protected void btnProcess_Click(object sender, ImageClickEventArgs e)
        {
            getChargeCOdeDetails();
        }

        protected void txtDisPercentage_TextChanged(object sender, EventArgs e)
        {
            getChargeCOdeDetails();
            CheckNewDiscountRate();
        }

        protected void txtDisAmount_TextChanged(object sender, EventArgs e)
        {
            getChargeCOdeDetails();
            CheckNewDiscountRate();
        }

        protected void btnDiscount_Click(object sender, EventArgs e)
        {
            mpDiscount.Show();
            dgvDiscounts.DataSource = new List<CashGeneralEntiryDiscountDef>();

            ddlCategoryMP.Text = "Customer";

            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                DisplayMessages("Please select the customer.");
                mpDiscount.Hide();
                return;
            }

            if (txtCusCode.Text == "CASH")
            {
                DisplayMessages("Please select the valid customer. Customer should be registered.");
                mpDiscount.Hide();
                return;
            }

            if (oMainItemsList != null)
                if (oMainItemsList.Count > 0)
                {
                    ddlCategoryMP.Enabled = true;
                }
                else
                {
                    ddlCategoryMP.Text = "Customer";
                    ddlCategoryMP.Enabled = false;
                }
            else
            {
                ddlCategoryMP.Text = "Customer";
                ddlCategoryMP.Enabled = false;
            }

            if (oMainItemsList != null)
                if (oMainItemsList.Count > 0)
                {
                    List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                    foreach (InvoiceItem _i in oMainItemsList)
                    {
                        CashGeneralEntiryDiscountDef _one = new CashGeneralEntiryDiscountDef();

                        var _dup = from _l in _CashGeneralEntiryDiscount
                                   where _l.Sgdd_itm == _i.Sad_itm_cd && _l.Sgdd_pb == _i.Sad_pbook && _l.Sgdd_pb_lvl == _i.Sad_pb_lvl
                                   select _l;

                        if (_dup == null || _dup.Count() <= 0)
                        {
                            _one.Sgdd_itm = _i.Sad_itm_cd;
                            _one.Sgdd_pb = _i.Sad_pbook;
                            _one.Sgdd_pb_lvl = _i.Sad_pb_lvl;

                            _CashGeneralEntiryDiscount.Add(_one);
                        }
                    }
                    dgvDiscounts.DataSource = _CashGeneralEntiryDiscount;
                }
        }

        protected void btnClose_Click(object sender, ImageClickEventArgs e)
        {
            mpDiscount.Hide();
        }

        protected void btnDisRequst_Click(object sender, EventArgs e)
        {
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(base.GlbUserComCode, base.GlbUserDefProf, CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                DisplayMessages("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            if (_infor.Count <= 0)
            {
                DisplayMessages("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == Session["UserID"].ToString()).ToList();
            if (_available == null || _available.Count <= 0)
            {
                DisplayMessages("Your user id does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }

            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlCategoryMP.Text == "Customer")
            {
                for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                {
                    GridViewRow row = dgvDiscounts.Rows[i];
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    chkSelect.Checked = false;
                }

                if (string.IsNullOrEmpty(txtDisRateMP.Text))
                {
                    DisplayMessages("Please select the discount rate");
                    return;
                }

                if (isdecimal(txtDisRateMP.Text) == false)
                {
                    DisplayMessages("Please select the valid discount rate");
                    return;
                }

                if (Convert.ToDecimal(txtDisRateMP.Text.Trim()) > 100)
                {
                    DisplayMessages("Discount rate can not exceed the 100%");
                    return;
                }

                if (Convert.ToDecimal(txtDisRateMP.Text.Trim()) == 0)
                {
                    DisplayMessages("Discount rate should exceed the 0%");
                    return;
                }
            }

            if (ddlCategoryMP.Text == "Item")
            {
                if (dgvDiscounts.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                    {
                        GridViewRow row = dgvDiscounts.Rows[i];
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        DisplayMessages("Please select the item which you need to request");
                        return;
                    }
                }

                txtDisRateMP.Text = "";
            }

            string _customer = txtCusCode.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (dgvDiscounts.Rows.Count > 0 && ddlCategoryMP.Text == "Item")
            {
                for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                {
                    GridViewRow row = dgvDiscounts.Rows[i];
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    Label lblItmeCodeMP = (Label)row.FindControl("lblItmeCodeMP");
                    Label lblDisAmountMP = (Label)row.FindControl("lblDisAmountMP");
                    Label lblPBMP = (Label)row.FindControl("lblPBMP");
                    Label lblPLMP = (Label)row.FindControl("lblPLMP");
                    Label lblItemTypeMP = (Label)row.FindControl("lblItemTypeMP");
                    DropDownList ddlItemTypeMP = (DropDownList)row.FindControl("ddlItemTypeMP");

                    if (chkSelect.Checked)
                    {
                        string _item = lblItmeCodeMP.Text;
                        string _type = lblItemTypeMP.Text;
                        string _amt = lblDisAmountMP.Text;
                        string _pricebook = lblPBMP.Text;
                        string _pricelvl = lblPLMP.Text;

                        if (string.IsNullOrEmpty(_amt))
                        {
                            DisplayMessages("Please select the amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }

                        if (!isdecimal(_amt))
                        {
                            DisplayMessages("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt) <= 0)
                        {
                            DisplayMessages("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt) > 100 && _type.Contains("Rate"))
                        {
                            DisplayMessages("Rate can not be exceed the 100% in " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = base.GlbUserComCode;
                        _discount.Sgdd_cre_by = Session["UserID"].ToString();
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;
                        if (ddlItemTypeMP.SelectedItem.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(_amt);
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(_amt);
                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = Session["UserID"].ToString();
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = base.GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = ddlInvoiceType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = base.GlbUserComCode;
                _discount.Sgdd_cre_by = Session["UserID"].ToString();
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisRateMP.Text);
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = Session["UserID"].ToString();
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbPriceBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbPriceLevel.Text.Trim();
                _discount.Sgdd_pc = base.GlbUserDefProf;
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = ddlInvoiceType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), base.GlbUserComCode, base.GlbUserDefProf, _customerReq, Session["UserID"].ToString(), _list, txtCusCode.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully Saved! Document No : " + _customerReq + ".";
                        txtDisRateMP.Text = "";
                        pnlDiscount.Visible = false;
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }
                    DisplayMessages(Msg);
                }
                catch (Exception ex)
                {
                    DisplayMessages(ex.Message);
                }
            }
        }

        protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPaymenttypes();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReffNum.Text))
            {
                Session["invoiceNum"] = txtReffNum.Text.Trim();
                //mpInvoicePrint.Show();
                // irm1.Attributes.Add("src", "InvoicePrint3.aspx");
                Response.Redirect("InvoicePrint3.aspx");
            }
            else
            {
                DisplayMessages("Please select a invoice.");
            }
        }

        protected void Close_Click(object sender, EventArgs e)
        {
            mpInvoicePrint.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReffNum.Text))
            {

                RecieptHeader oRecieptHeader = null;
                string err;

                int asd = CHNLSVC.Tours.GetInvoiceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtReffNum.Text.Trim(), out oHeader, out oMainItemsList, out oRecieptHeader, out _recieptItem, out   err);

                if (oHeader.Sah_dt.Date != DateTime.Now.Date)
                {
                    DisplayMessages("Cannot Cancel this invoice. Invoice date: " + oHeader.Sah_dt.ToString("dd/MMM/yyyy"));
                    return;
                }

                Int32 result = CHNLSVC.Tours.UPDATE_INVOICE_STATUS("C", Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtReffNum.Text, out err);
                if (result > 0)
                {
                    if (!string.IsNullOrEmpty(txtEnquiryID.Text))
                    {
                        result = CHNLSVC.Tours.UpdateEnquiryStageWithlog(8, Session["UserID"].ToString(), txtEnquiryID.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), out err);
                    }
                }

                if (result > 0)
                {
                    DisplayMessages("Invoice canceled.");
                    ClearAll();
                    return;
                }
                else
                {
                    DisplayMessages("Error: " + err);
                    return;
                }
            }
        }

        protected void btnDisReqNew_Click(object sender, EventArgs e)
        {
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(base.GlbUserComCode, base.GlbUserDefProf, CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                DisplayMessages("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            if (_infor.Count <= 0)
            {
                DisplayMessages("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == Session["UserID"].ToString()).ToList();
            if (_available == null || _available.Count <= 0)
            {
                DisplayMessages("Your user id does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }

            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlCategoryMP.Text == "Customer")
            {
                for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                {
                    GridViewRow row = dgvDiscounts.Rows[i];
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    chkSelect.Checked = false;
                }

                if (string.IsNullOrEmpty(txtDisRateMP.Text))
                {
                    DisplayMessages("Please select the discount rate");
                    return;
                }

                if (isdecimal(txtDisRateMP.Text) == false)
                {
                    DisplayMessages("Please select the valid discount rate");
                    return;
                }

                if (Convert.ToDecimal(txtDisRateMP.Text.Trim()) > 100)
                {
                    DisplayMessages("Discount rate can not exceed the 100%");
                    return;
                }

                if (Convert.ToDecimal(txtDisRateMP.Text.Trim()) == 0)
                {
                    DisplayMessages("Discount rate should exceed the 0%");
                    return;
                }
            }

            if (ddlCategoryMP.Text == "Item")
            {
                if (dgvDiscounts.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                    {
                        GridViewRow row = dgvDiscounts.Rows[i];
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        DisplayMessages("Please select the item which you need to request");
                        return;
                    }
                }

                txtDisRateMP.Text = "";
            }

            string _customer = txtCusCode.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (dgvDiscounts.Rows.Count > 0 && ddlCategoryMP.Text == "Item")
            {
                for (int i = 0; i < dgvDiscounts.Rows.Count; i++)
                {
                    GridViewRow row = dgvDiscounts.Rows[i];
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    Label lblItmeCodeMP = (Label)row.FindControl("lblItmeCodeMP");
                    Label lblDisAmountMP = (Label)row.FindControl("lblDisAmountMP");
                    Label lblPBMP = (Label)row.FindControl("lblPBMP");
                    Label lblPLMP = (Label)row.FindControl("lblPLMP");
                    Label lblItemTypeMP = (Label)row.FindControl("lblItemTypeMP");
                    DropDownList ddlItemTypeMP = (DropDownList)row.FindControl("ddlItemTypeMP");

                    if (chkSelect.Checked)
                    {
                        string _item = lblItmeCodeMP.Text;
                        string _type = lblItemTypeMP.Text;
                        string _amt = lblDisAmountMP.Text;
                        string _pricebook = lblPBMP.Text;
                        string _pricelvl = lblPLMP.Text;

                        if (string.IsNullOrEmpty(_amt))
                        {
                            DisplayMessages("Please select the amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }

                        if (!isdecimal(_amt))
                        {
                            DisplayMessages("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt) <= 0)
                        {
                            DisplayMessages("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt) > 100 && _type.Contains("Rate"))
                        {
                            DisplayMessages("Rate can not be exceed the 100% in " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = base.GlbUserComCode;
                        _discount.Sgdd_cre_by = Session["UserID"].ToString();
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;
                        if (ddlItemTypeMP.SelectedItem.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(_amt);
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(_amt);
                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = Session["UserID"].ToString();
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = base.GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = ddlInvoiceType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = base.GlbUserComCode;
                _discount.Sgdd_cre_by = Session["UserID"].ToString();
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisRateMP.Text);
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = Session["UserID"].ToString();
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbPriceBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbPriceLevel.Text.Trim();
                _discount.Sgdd_pc = base.GlbUserDefProf;
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = ddlInvoiceType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), base.GlbUserComCode, base.GlbUserDefProf, _customerReq, Session["UserID"].ToString(), _list, txtCusCode.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully Saved! Document No : " + _customerReq + ".";
                        txtDisRateMP.Text = "";
                        pnlDiscount.Visible = false;
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }
                    DisplayMessages(Msg);
                }
                catch (Exception ex)
                {
                    DisplayMessages(ex.Message);
                }
            }
        }
    }
}