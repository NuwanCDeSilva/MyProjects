using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Globalization;
using System.Text;
using System.Data;
using System.Transactions;
using System.IO;

namespace FF.WebERPClient.HP_Module
{
    public partial class HpRevertRelease : BasePage
    {

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["HpRevertReleaseViewState"];
            LosFormatter m_formatter = new LosFormatter();
            try
            {
                viewStateBag = m_formatter.Deserialize(m_viewState);
            }
            catch
            {
                throw new HttpException("The View State is invalid.");
            }
            return viewStateBag;
        }
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            LosFormatter m_formatter = new LosFormatter();
            m_formatter.Serialize(ms, viewState);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string viewStateString = sr.ReadToEnd();
            Session["HpRevertReleaseViewState"] = viewStateString;
            ms.Close();
            return;
        }


        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)ViewState["Receipt_List"]; }
            set { ViewState["Receipt_List"] = value; }
        }
        public List<ReptPickSerials> SelectedItemList
        {
            get { return (List<ReptPickSerials>)ViewState["SelectedItemList"]; }
            set { ViewState["SelectedItemList"] = value; }
        }
        public List<ReptPickSerials> OriginalRevertedItemList
        {
            get { return (List<ReptPickSerials>)ViewState["OriginalRevertedItemList"]; }
            set { ViewState["OriginalRevertedItemList"] = value; }
        }

        public List<RecieptHeader> Final_recHeaderList
        {
            get { return (List<RecieptHeader>)Session["Final_recHeaderList"]; }
            set { Session["Final_recHeaderList"] = value; }
        }
        public List<RecieptItem> ReceiptItem
        {
            get { return (List<RecieptItem>)Session["ReceiptItem"]; }
            set { Session["ReceiptItem"] = value; }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);
                ProfitCenterValidate();
                Receipt_List = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
                _paidAmount = 0;
                BalanceAmount = 0;
                PaidAmount = 0;
                txtPayCrBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));

                BindReceiptItem(string.Empty);
                BindPaymentType(ddlPayMode);
                loadPrifixes("");
                bind_gvReceipts(null);
                BindAccountItem(string.Empty);

                txtReceiptNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnReceiptEnter.ClientID + "')");
                txtReciptAmount.Attributes.Add("onkeypress", "return fun1(event,'" + ImgBtnAddReceipt.ClientID + "')");
                txtDate.Text = DateTime.Now.Date.ToShortDateString();
                _globalReceiptCounter = 0;

                BindRevertItem(null);
                BindRevertSerial(null);

                txtProfitCenter.Attributes.Add("onkeyup", "return clickButton(event,'" + ImgBtnProfitCenter.ClientID + "')");
                txtProfitCenter.Attributes.Add("onblur", "CheckProfitCenter('" + txtProfitCenter.ClientID + "')");

                txtRDiscount.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnRCalculate, ""));
                txtRPartRelease.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnRCalculate, ""));

                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                txtAccountNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAccount, ""));

                BindRequestsToDropDown(string.Empty, ddlRequestNo);
                radDiscount.Checked = true;
                //Ad-hoc user pemission for checking purpose
                GlbReqIsApprovalNeed = true;
                GlbReqUserPermissionLevel = 3;
                GlbReqIsFinalApprovalUser = true;
                GlbReqIsRequestGenerateUser = true;
            }
        }

        private void ProfitCenterValidate()
        {
            string v = Request.QueryString["pc"];
            if (v != null) { MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, v); if (_pc != null) { txtProfitCenter.Text = v; return; } }
            txtProfitCenter.Text = GlbUserDefProf;
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtProfitCenter.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }
        #endregion

        #region   DataBind Area
        protected void BindReceiptItem(string _invoice)
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(_invoice);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(_invoice);

            }
            gvPayment.DataBind();
        }
        protected void BindPaymentType(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPRV", DateTime.Now.Date);
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            _ddl.DataSource = payTypes;
            _ddl.DataBind();

        }
        private void loadPrifixes(string _dtype)
        {
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, txtProfitCenter.Text.Trim());
            List<string> prifixes = new List<string>();
            prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, _dtype, 1);
            ddlPrefix.DataSource = prifixes;
            ddlPrefix.DataBind();
        }
        #endregion

        #region  Receipt Issue

        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(ViewState["PaidAmount"]); }
            set { ViewState["PaidAmount"] = Math.Round(value, 2); }
        }
        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(ViewState["BalanceAmount"]); }
            set { ViewState["BalanceAmount"] = Math.Round(value, 2); }
        }

        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
                    Decimal amt = Convert.ToDecimal(gvr.Cells[18].Text.Trim());
                    PaidAmount = PaidAmount + amt;
                }
            }
            lblPayPaid.Text = PaidAmount.ToString();

        }
        private void set_BalanceAmount()
        {
            BalanceAmount = 0;
            Decimal receiptAmt = 0;
            if (gvReceipts.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvReceipts.Rows)
                {
                    Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                    receiptAmt = receiptAmt + amt;
                }
                BalanceAmount = receiptAmt - PaidAmount;
            }
            lblPayBalance.Text = BalanceAmount.ToString();
        }


        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            if (Receiptlist != null)
                if (Receiptlist.Count > 0)
                {
                    gvReceipts.DataSource = Receiptlist;
                }
                else
                {
                    gvReceipts.DataSource = new List<RecieptHeader>();
                }
            else
            {
                gvReceipts.DataSource = new List<RecieptHeader>();
            }
            gvReceipts.DataBind();
        }

        private int _globalReceiptCounter
        {
            get { return Convert.ToInt32(ViewState["_globalReceiptCounter"]); }
            set { ViewState["_globalReceiptCounter"] = value; }
        }


        protected void ImgBtnAddReceipt_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select the profit center");
                return;
            }

            this.MasterMsgInfoUCtrl.Clear();
            string location = GlbUserDefProf;
            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());

            if (Rh != null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt already used!");
                return;
            }

            try
            {
                Decimal receiptamount = Convert.ToDecimal(txtReciptAmount.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter a valid Receipt amount!");
                return;
            }

            if (Convert.ToDecimal(txtReciptAmount.Text) > 10000)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed Rs.10,000!");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {

                string prefix = gvr.Cells[1].Text.Trim();
                Int32 recNo = Convert.ToInt32(gvr.Cells[2].Text.Trim());
                if (prefix == ddlPrefix.SelectedValue && recNo == Convert.ToInt32(txtReceiptNo.Text.Trim()))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt number already used!");
                    return;
                }

            }
            try
            {
                Convert.ToDateTime(txtDate.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter receipt date!");
                return;
            }

            if ((txtReciptAmount.Text) == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) > 10000)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed Rs.10,000!");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }


            RecieptHeader _recHeader = new RecieptHeader();

            #region Receipt Header Value Assign
            _recHeader.Sar_acc_no = lblAccountNo.Text;
            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            _recHeader.Sar_is_mgr_iss = false;
            if (GlbUserDefProf != txtProfitCenter.Text.Trim())
            {
                _recHeader.Sar_is_oth_shop = true;
            }
            else
            {
                _recHeader.Sar_is_oth_shop = false;
            }
            _recHeader.Sar_is_used = false;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            _recHeader.Sar_oth_sr = "";
            _recHeader.Sar_prefix = ddlPrefix.SelectedValue;
            _recHeader.Sar_profit_center_cd = GlbUserDefProf;
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text.Trim()).Date;
            _recHeader.Sar_manual_ref_no = txtReceiptNo.Text;
            _recHeader.Sar_receipt_type = "HPRM";
            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;
            _recHeader.Sar_wht_rate = 0;
            _recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            _recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;
            //Added Prabhath
            _globalReceiptCounter++;
            _recHeader.Sar_anal_7 = _globalReceiptCounter;
            #endregion

            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            if (X == false)
            {
                _globalReceiptCounter--;
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence not correct!");
                return;
            }
            else
            {
                Receipt_List.Add(_recHeader);
                bind_gvReceipts(Receipt_List);

            }

            set_PaidAmount();
            set_BalanceAmount();

            txtReciptAmount.Text = "";
            txtReceiptNo.Text = "";
        }
        protected void gvReceipts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<RecieptHeader> _temp = new List<RecieptHeader>();
            _temp = Receipt_List;

            int row_id = e.RowIndex;
            string prefix = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_prefix"]);
            // string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_receipt_no"]);
            string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_manual_ref_no"]);

            _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
            Receipt_List = _temp;
            _globalReceiptCounter--;
            bind_gvReceipts(Receipt_List);

            Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(GlbUserName, prefix, Convert.ToInt32(receiptNo));

        }

        protected void btnReceiptEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select the profit center");
                return;
            }

            string location = txtProfitCenter.Text.Trim();
            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
            if (Rh != null)
            {
                if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL" || Rh.Sar_act == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
                    return;
                }
                if (Rh.Sar_receipt_type != "HPRM")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
                    return;
                }
                if (Rh.Sar_receipt_date < Convert.ToDateTime(txtDate.Text.Trim()))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot edit/cancel back dated receipts!");
                    return;
                }
                string Msg1 = "<script>alert('Receipt already used- you can edit or cancel Receipt!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                //---------------
                string AccNo = Rh.Sar_acc_no;
                string ReceiptNo = Rh.Sar_receipt_no;
                //----------------------------------------------
                HpAccount Acc = new HpAccount();
                Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtDate.Text).Date, txtProfitCenter.Text.Trim());


                lblAccountNo.Text = AccNo;
                txtReciptAmount.Text = Rh.Sar_tot_settle_amt.ToString();
                txtAccountNo.Text = Acc.Hpa_seq.ToString();
                //---------------------------------------------------------------
                //add on 20-7-2012

                txtReciptAmount.Focus();
            }
            else
            {
                //====added on 26-7-2012
                string receipt_type;

                receipt_type = "HPRM";

                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence not correct!");
                    return;
                }
                //=======================
                txtReciptAmount.Focus();
            }
        }

        protected void gvReceipts_OnRowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int _seq = Convert.ToInt32(e.Row.Cells[0].Text);
                    ImageButton _btn = (ImageButton)e.Row.FindControl("ImgBtnDelRecipt");

                    if (_seq < _globalReceiptCounter)
                        _btn.Visible = false;
                    else
                        _btn.Visible = true;

                }
        }

        #endregion

        #region Payment
        protected decimal _paidAmount
        {
            get { return (decimal)ViewState["_paidAmount"]; }
            set { ViewState["_paidAmount"] = value; }
        }
        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
            set { ViewState["_recieptItem"] = value; }
        }

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                divCredit.Visible = false; divAdvReceipt.Visible = true;
            }
            else
            {
                divCredit.Visible = false; divAdvReceipt.Visible = false;
            }
            txtPayAmount.Text = (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Focus();
        }
        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }

            if (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (string.IsNullOrEmpty(txtPayCrBank.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card no/checq no"); txtPayCrCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == "CRCD") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            {
                if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the document no"); txtPayAdvReceiptNo.Focus(); return; }
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPayCrPeriod.Text);


            if (string.IsNullOrEmpty(txtPayAmount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
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
                if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS") _cardno = txtPayAdvReceiptNo.Text;

                _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
                _item.Sard_cc_period = _period;
                _item.Sard_cc_tp = txtPayCrCardType.Text;
                _item.Sard_chq_bank_cd = txtPayCrBank.Text;
                _item.Sard_chq_branch = txtPayCrBranch.Text;
                _item.Sard_credit_card_bank = txtPayCrBank.Text;
                _item.Sard_ref_no = _cardno;
                _item.Sard_deposit_bank_cd = null;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
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

                if (ddlPayMode.SelectedValue == "CRCD")
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == txtPayCrCardType.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == "CHEQUE")
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == txtPayCrBank.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "ADVAN")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "LORE")
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "GVO" || ddlPayMode.SelectedValue == "GVS")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                    _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
                    _item.Sard_cc_period = Convert.ToInt32(txtPayCrPeriod.Text);
                    _item.Sard_cc_period = _period;
                    _item.Sard_cc_tp = txtPayCrCardType.Text;
                    _item.Sard_chq_bank_cd = txtPayCrBank.Text;
                    _item.Sard_chq_branch = txtPayCrBranch.Text;
                    _item.Sard_credit_card_bank = null;
                    _item.Sard_deposit_bank_cd = null;
                    _item.Sard_deposit_branch = null;
                    _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                    _paidAmount += _payAmount;
                    _recieptItem.Add(_item);
                }
                else
                {
                    //duplicates
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not add duplicate payments");
                    return;

                }



            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblPayBalance.Text = (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Text = (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);

            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayAdvReceiptNo.Text = "";
            txtPayAdvRefAmount.Text = "";
            txtPayCrPeriod.Text = "";
            txtPayCrPeriod.Enabled = false;

        }
        protected void AddPayment(object sender, EventArgs e)
        {
            AddPayment();
        }
        protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {

            if (_recieptItem == null || _recieptItem.Count == 0) return;

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

            lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblPayBalance.Text = (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Text = (Convert.ToDecimal(lblTotalReceivable.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        }
        private void CheckBank()
        {
            MasterBankAccount _bankAccounts = new MasterBankAccount();
            if (!string.IsNullOrEmpty(txtPayCrBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, txtPayCrBank.Text, "");

                if (_bankAccounts.Msba_cd != null)
                {
                    txtPayCrBank.Text = _bankAccounts.Msba_cd;

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                    txtPayCrBank.Text = "";
                    txtPayCrBank.Focus();
                    return;
                }
            }

            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPRV", DateTime.Now.Date);
            var _promo = (from _prom in _paymentTypeRef
                          where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
                          select _prom).ToList();

            foreach (PaymentType _type in _promo)
            {
                if (_type.Stp_pro == "1" && _type.Stp_bank == txtPayCrBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                {
                    chkPayCrPromotion.Checked = true;
                    txtPayCrPeriod.Text = _type.Stp_pd.ToString();
                }

            }

        }

        protected void CheckBank(object sender, EventArgs e)
        {
            CheckBank();
        }
        #endregion

        private void BindAccountItem(string _account)
        {

            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;

            if (_invoice != null)
                if (_invoice.Count > 0)
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvATradeItem.DataSource = _table;
            }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                    #region New Method
                    var _sales = from _lst in _invoice
                                 where _lst.Sah_direct == true
                                 select _lst;

                    foreach (InvoiceHeader _hdr in _sales)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }
                    #endregion
                    gvATradeItem.DataSource = _itemList;
                }

            gvATradeItem.DataBind();
        }
        static int _count = 0;
        protected void AccountItem_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnIsForward = (HiddenField)e.Row.FindControl("hdnIsForwardSale");
                    HiddenField _hdnInvoiceNo = (HiddenField)e.Row.FindControl("hdnInvoiceNo");
                    HiddenField _hdnDONo = (HiddenField)e.Row.FindControl("hdnDONo");
                    HiddenField _hdnLineNo = (HiddenField)e.Row.FindControl("hdnlineNo");

                    if (Convert.ToBoolean(_hdnIsForward.Value.ToString()) == true)
                    {
                        e.Row.Style.Add("color", "#990000"); //e.Row.Style.Add("background-color", "#9DC2D5");
                        e.Row.Style.Add("font-weight", "bold");
                    }

                    e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                    e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(
                                gvATradeItem,
                                String.Concat("Select$", e.Row.RowIndex),
                                true);

                    _count += 1;

                }

        }


        private void BindRevertItem(object _list)
        {
            if (_list != null)
                gvRevetedItem.DataSource = _list;
            else
                gvRevetedItem.DataSource = new List<ReptPickSerials>();
            gvRevetedItem.DataBind();

        }
        private void BindRevertSerial(List<ReptPickSerials> _list)
        {
            if (_list != null)
                if (_list.Count > 0)
                    gvRevertedSerial.DataSource = _list;
                else
                    gvRevertedSerial.DataSource = new List<ReptPickSerials>();
            else
                gvRevertedSerial.DataSource = new List<ReptPickSerials>();
            gvRevertedSerial.DataBind();
        }

        #region Revert Detail Load
        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }

        protected string RevertNo
        {
            get { return Convert.ToString(Session["RevertNo"]); }
            set { Session["RevertNo"] = value; }
        }
        protected string RevertInventoryDocument
        {
            get { return Convert.ToString(Session["RevertInventoryDocument"]); }
            set { Session["RevertInventoryDocument"] = value; }
        }

        private HpRevertHeader GetRevertHeaderfromAccountnoForRelease(string _account, Int16 _status) { return CHNLSVC.Inventory.GetRevertHeaderfromAccountnoForRelease(GlbUserComCode, txtProfitCenter.Text.Trim(), _account, _status); }

        private void LoadRevertItemNSerial(string _account, Int16 _status)
        {
            HpRevertHeader _hdr = GetRevertHeaderfromAccountnoForRelease(_account, _status);
            if (_hdr != null)
            {

                string _revertno = _hdr.Hrt_ref;
                RevertNo = _revertno;
                List<ReptPickSerials> _list = CHNLSVC.Inventory.GetAdjustmentDetailFromRevertNo(GlbUserComCode, _account, _revertno);
                OriginalRevertedItemList = _list;
                RevertInventoryDocument = _list[0].Tus_doc_no;
                BindRevertItemNSerial(_list);
                return;
            }
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no revert details available");
        }

        private void BindRevertItemNSerial(List<ReptPickSerials> _list)
        {
            string _avaLoc = string.Empty;
            if (_list != null)
                if (_list.Count > 0)
                {

                    _avaLoc = _list[0].Tus_loc;
                    _list.Where(y => y.Tus_new_status == "1" && y.Tus_loc == _avaLoc && Convert.ToDecimal(y.Tus_serial_id) < y.Tus_qty).ToList().ForEach(x => x.Tus_serial_id = Convert.ToString(Convert.ToDecimal(x.Tus_serial_id) + 1));

                    var _item = (from _lst in _list
                                 group _lst by new { _lst.Tus_itm_cd, _lst.Tus_itm_desc, _lst.Tus_itm_model, _lst.Tus_itm_stus, _lst.Tus_serial_id, _lst.Tus_ser_line, _lst.Tus_doc_no } into itm
                                 select new { Tus_itm_cd = itm.Key.Tus_itm_cd, Tus_itm_desc = itm.Key.Tus_itm_desc, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_serial_id = itm.Key.Tus_serial_id, Tus_ser_line = itm.Key.Tus_ser_line, Tus_doc_no = itm.Key.Tus_doc_no, Tus_qty = itm.Sum(p => p.Tus_qty) }).ToList();
                    BindRevertItem(_item);
                }

            if (_avaLoc == GlbUserDefLoca)
            {
                var _avaLst = (from _l in _list
                               where Convert.ToInt32(_l.Tus_new_status) != 1
                               select _l).ToList();
                if (_avaLst != null)
                    if (_avaLst.Count > 0)
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Some Reverted Items are not available in your location.");

                BindRevertSerial(_list.Where(x => x.Tus_new_status == "1").ToList());
                SelectedItemList = _list.Where(x => x.Tus_new_status == "1").ToList();

            }
            else
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item available in different location, " + _avaLoc + ". You have to pick different campatible serial or transfer the product.");
            return;

        }

        private void LoadAccountDetail(string _account, DateTime _date)
        {
            lblAccountNo.Text = _account;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, _account);
            //lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountsList)
            {
                if (_account == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, _date.Date, GlbUserDefProf);
            BindAccountItem(account.Hpa_acc_no);
            LoadRevertItemNSerial(account.Hpa_acc_no, 0);
            btnSave.Enabled = true;
            BindRequestsToDropDown(account.Hpa_acc_no, ddlRequestNo);
            CalculateBalanceSheet();
        }

        protected void btn_validateACC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNo.Text)) return;

            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid profit center");
                txtProfitCenter.Text = string.Empty;
                return;
            }
            string location = txtProfitCenter.Text.Trim();
            string acc_seq = txtAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "R");
            AccountsList = accList;//save in veiw state
            if (accList == null || accList.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                txtAccountNo.Text = null;
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                foreach (HpAccount ac in accList)
                {

                    LoadAccountDetail(ac.Hpa_acc_no, DateTime.Now.Date);
                }
            }
            else if (accList.Count > 1)
            {
                //show a pop up to select the account number
                grvMpdalPopUp.DataSource = accList;
                grvMpdalPopUp.DataBind();
                ModalPopupAccItem.Show();
            }

        }
        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            LoadAccountDetail(accountNo, DateTime.Now.Date);
        }

        protected void gvRevertedSerial_OnBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnIsPicked = (HiddenField)e.Row.FindControl("hdnIsPicked");
                    ImageButton _btn = (ImageButton)e.Row.FindControl("imgBtnRDelSerial");
                    if (string.IsNullOrEmpty(_hdnIsPicked.Value)) _btn.Visible = false; else if (_hdnIsPicked.Value == "0") _btn.Visible = false; else _btn.Visible = true;
                }
        }

        protected void gvRevertedItem_OnBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //ImageButton _btn = (ImageButton)e.Row.FindControl("imgBtnRPick");

                    //string _cellText = e.Row.Cells[4].Text;
                    //if (string.IsNullOrEmpty(_cellText))
                    //    _cellText = "0";
                    //else if (_cellText == "&nbsp;")
                    //    _cellText = "0";

                    //decimal _avaQty = Convert.ToDecimal(_cellText);
                    //if (_avaQty == 0)
                    //    _btn.Visible = true;
                    //else
                    //    _btn.Visible = false;

                    e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                gvRevetedItem,
                                String.Concat("Select$", e.Row.RowIndex),
                                true);

                    _count += 1;
                }
        }
        #endregion

        #region  Serial Scan
        protected void btnPopupOk_Click(object sender, EventArgs e)
        {

            Int32 generated_seq = -1;
            string itemCode = lblPopupItemCode.Text.Trim();


            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblRevertedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {

                divPopupImg.Visible = true;
                lblpopupMsg.Text = "Can't exceed Invoice Qty!";
                ModalPopupScanItem.Show();
                return;
            }



            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            {
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {

                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_usrseq_no = generated_seq;

                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_base_doc_no = RevertNo;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_new_status = "1";
                        _reptPickSerial_.Tus_new_remarks = "1";

                        SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Value));

                        SelectedItemList.Add(_reptPickSerial_);

                        rowCount++;

                    }

                }
            }

            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();


                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {

                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);



                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;

                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);

                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = RevertNo;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        _reptPickSerial_nonSer.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_nonSer.Tus_new_status = "1";
                        _reptPickSerial_nonSer.Tus_new_remarks = "1";

                        SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Value));

                        SelectedItemList.Add(_reptPickSerial_nonSer);

                        rowCount++;

                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                    }

                }

            }

            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {

                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);



                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;


                        Decimal pending_amt_ = Convert.ToDecimal(lblRevertedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());

                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {

                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        }


                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = RevertNo;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        _reptPickSerial_nonSer.Tus_new_status = "1";
                        _reptPickSerial_nonSer.Tus_new_remarks = "1";

                        SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Value));

                        SelectedItemList.Add(_reptPickSerial_nonSer);

                        rowCount++;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);


                    }

                }

            }


            BindRevertItemNSerial(SelectedItemList);
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }

        public List<ReptPickSerials> serial_list
        {
            get { return (List<ReptPickSerials>)ViewState["ReptPickSerials"]; }
            set { ViewState["ReptPickSerials"] = value; }
        }

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();

            if (ddlPopupSerial.SelectedValue == "Serial 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupScanItem.Show();


            }
            else if (ddlPopupSerial.SelectedValue == "Serial 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupScanItem.Show();
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select serial type from drop down!");

            }

        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblRevertedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Reverted Qty! Can add only " + pending_amt + " itmes more.");
                return;
            }

            ModalPopupScanItem.Show();
        }

        protected void GridViewDo_itm_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow gvr = gvRevetedItem.SelectedRow;

            string ScannedDoQty = gvr.Cells[4].Text == "&nbsp;" ? "0" : string.IsNullOrEmpty(gvr.Cells[4].Text) ? "0" : gvr.Cells[4].Text;


            lblScanQty.Text = ScannedDoQty.ToString();

            lblRevertedQty.Text = gvRevetedItem.SelectedRow.Cells[3].Text.ToString();


            lblPopupItemCode.Text = gvRevetedItem.SelectedRow.Cells[1].Text.ToString();
            string longDiscript = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text).Mi_longdesc;

            hdnInvoiceLineNo.Value = gvRevetedItem.SelectedRow.Cells[0].Text.ToString();

            divPopupImg.Visible = false;
            lblpopupMsg.Text = string.Empty;

            if (lblPopupItemCode.Text != "&nbsp;" && Convert.ToDecimal(lblRevertedQty.Text) - Convert.ToDecimal(ScannedDoQty) > 0)
            {
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                lblPopupBinCode.Visible = false;
                txtPopupQty.Visible = true;

                MasterItem msitem = new MasterItem();
                msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);

                List<MasterCompanyItemStatus> _list = CHNLSVC.Inventory.GetAllCompanyStatuslist(GlbUserComCode).Where(x => x.Mic_isrvt == true).ToList();


                if (msitem.Mi_is_ser1 == 0)
                {

                    serial_list = new List<ReptPickSerials>();


                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != RevertInventoryDocument).ToList();


                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupScanItem.Show();
                }
                else if (msitem.Mi_is_ser1 == 1) //serial
                {
                    serial_list = new List<ReptPickSerials>();


                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != RevertInventoryDocument).ToList();

                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupScanItem.Show();
                }
                else if (msitem.Mi_is_ser1 == -1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Non serial, decimal allow");
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != RevertInventoryDocument).ToList();


                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupScanItem.Show();
                }


            }

        }

        protected void SelectedItem_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
            if (SelectedItemList == null) return;
            if (SelectedItemList.Count <= 0) return;

            int row_id = e.RowIndex;
            Int32 _serialID = (Int32)gvRevertedSerial.DataKeys[row_id][1];
            string _item = (string)gvRevertedSerial.DataKeys[row_id][0];

            MasterItem _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 != -1)
            {
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, _serialID, 1);
            }
            else
            {
                SelectedItemList.RemoveAll(x => x.Tus_itm_cd == _item);
            }

            var _notAvailable = (from _l in OriginalRevertedItemList
                                 where string.IsNullOrEmpty(_l.Tus_new_status) || _l.Tus_new_status == "0"
                                 select _l).ToList();
            SelectedItemList.AddRange(_notAvailable);


            BindRevertItemNSerial(SelectedItemList);

        }
        #endregion

        #region Request Generate
        private void BindRequestsToDropDown(string _account, DropDownList _ddl)
        {
            if (GlbReqIsApprovalNeed)
            {
                //case
                //1.get user approval level
                //2.if user request generate user, allow to check approval request check box and load approved requests
                //3.else load the request which lower than the approval level in the table which is not approved

                int _isApproval = 0;

                if (GlbReqIsRequestGenerateUser)
                    //no need to load pendings, but if check box select, load apporoved requests
                    if (chkApproved.Checked) _isApproval = 1;
                    else _isApproval = 0;
                else _isApproval = 0;

                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _ddl.Items.Clear();
                        //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        _ddl.DataSource = query;
                        _ddl.DataBind();

                        RequestApprovalHeader _l = _lst[0];
                        if (_l.Grad_req_param == "RVTRELEASE_RELEASE")
                        {
                            radPartRelease.Checked = true;
                            txtRPartRelease.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));
                        }
                        else if (_l.Grad_req_param == "RVTRELEASE_DISCOUNT")
                        {
                            radDiscount.Checked = true;
                            txtRDiscount.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));
                        }
                        CalculateBalanceSheet();

                    }
                }

            }

        }
        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
            CalculateRequest();
            CalculateBalanceSheet();
        }
        protected void btnSendEcdReq_Click(object sender, EventArgs e)
        {

            if (radDiscount.Checked && string.IsNullOrEmpty(txtRDiscount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the discount");
                return;
            }
            if (radPartRelease.Checked && string.IsNullOrEmpty(txtRPartRelease.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the part release");
                return;
            }

            if (GlbReqIsApprovalNeed == true)
            {

                //send custom request.
                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = GlbUserName;
                ra_hdr.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString();
                ra_hdr.Grah_com = GlbUserComCode;
                ra_hdr.Grah_cre_by = GlbUserName;
                ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdr.Grah_loc = GlbUserDefProf;// GlbUserDefLoca;

                ra_hdr.Grah_mod_by = GlbUserName;
                ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);


                if (GlbUserDefProf != txtProfitCenter.Text.Trim())
                {
                    ra_hdr.Grah_oth_loc = "1";
                }
                else
                { ra_hdr.Grah_oth_loc = "0"; }

                if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()))
                {
                    ra_hdr.Grah_ref = string.Empty;
                }
                else
                {
                    ra_hdr.Grah_ref = ddlRequestNo.SelectedValue;
                }
                // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "HP Revert Release";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = radDiscount.Checked ? "RVTRELEASE_DISCOUNT" : "RVTRELEASE_RELEASE";
                ra_det.Grad_val1 = radDiscount.Checked ? Convert.ToDecimal(txtRDiscount.Text.Trim()) : radPartRelease.Checked ? Convert.ToDecimal(txtRPartRelease.Text.Trim()) : 0;
                ra_det.Grad_is_rt1 = true;
                ra_det.Grad_anal1 = lblAccountNo.Text;
                ra_det.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = GlbUserName;
                ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT006.ToString();
                ra_hdrLog.Grah_com = GlbUserComCode;
                ra_hdrLog.Grah_cre_by = GlbUserName;
                ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdrLog.Grah_loc = GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = GlbUserName;
                ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);
                if (GlbUserDefProf != txtProfitCenter.Text.Trim())
                {
                    ra_hdrLog.Grah_oth_loc = "1";
                }
                else
                { ra_hdrLog.Grah_oth_loc = "0"; }

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = radDiscount.Checked ? "RVTRELEASE_DISCOUNT" : "RVTRELEASE_RELEASE";
                ra_detLog.Grad_val1 = radDiscount.Checked ? Convert.ToDecimal(txtRDiscount.Text.Trim()) : radPartRelease.Checked ? Convert.ToDecimal(txtRPartRelease.Text.Trim()) : 0;
                ra_detLog.Grad_is_rt1 = true;
                ra_detLog.Grad_anal1 = lblAccountNo.Text;
                ra_detLog.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                #endregion

                string referenceNo = string.Empty;
                Int32 eff = -1;
                try
                {
                    eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);

                }
                catch (Exception ex)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, ex.Message);
                    return;
                }
                finally
                {
                    if (eff > 0)
                    {
                        string Msg = "alert('Request Successfully Saved! Request No : " + referenceNo + "');";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "RevertReleaseRequest1", Msg, true);

                        Msg = "window.location = 'HpRevertRelease.aspx';";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "RevertReleaseRequest2", Msg, true);

                    }
                    else
                    {
                        string Msg = "<script>alert('Request Fail!' );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                    BindRequestsToDropDown(lblAccountNo.Text, ddlRequestNo);
                }
            }
        }
        private void CalculateRequest()
        {
            if (radDiscount.Checked)
                if (!string.IsNullOrEmpty(txtRDiscount.Text))
                {
                    if (!IsNumeric(txtRDiscount.Text.Trim(), NumberStyles.Currency))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select valid discount");
                        txtRDiscount.Text = "";
                        lblReqDiscountAmount.Text = "";
                        lblReqReleaseAmount.Text = "";
                        txtRDiscount.Focus();
                        return;
                    }

                    decimal _dis = Convert.ToDecimal(txtRDiscount.Text.Trim());
                    if (_dis > 100)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Rate can not be grater than 100%");
                        txtRDiscount.Text = "";
                        lblReqDiscountAmount.Text = "";
                        lblReqReleaseAmount.Text = "";
                        txtRDiscount.Focus();
                        return;
                    }
                    lblReqReleaseAmount.Text = "";
                    lblReqDiscountAmount.Text = FormatToCurrency(Convert.ToString(uc_HpAccountSummary1.Uc_AccBalance - ((uc_HpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtRDiscount.Text)) / 100)));
                }
            if (radPartRelease.Checked)
                if (!string.IsNullOrEmpty(txtRPartRelease.Text))
                {
                    if (!IsNumeric(txtRPartRelease.Text.Trim(), NumberStyles.Currency))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select valid discount");
                        txtRPartRelease.Text = "";
                        lblReqDiscountAmount.Text = "";
                        lblReqReleaseAmount.Text = "";
                        txtRPartRelease.Focus();
                        return;
                    }

                    decimal _dis = Convert.ToDecimal(txtRPartRelease.Text.Trim());
                    if (_dis > 100)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Rate can not be grater than 100%");
                        txtRPartRelease.Text = "";
                        lblReqDiscountAmount.Text = "";
                        lblReqReleaseAmount.Text = "";
                        txtRPartRelease.Focus();
                        return;
                    }
                    lblReqDiscountAmount.Text = "";
                    lblReqReleaseAmount.Text = FormatToCurrency(Convert.ToString(uc_HpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtRPartRelease.Text) / 100));
                }
        }
        private void CalculateBalanceSheet()
        {

            if (uc_HpAccountSummary1.Uc_AccBalance != 0)
            {
                lblSumAccBalance.Text = Convert.ToString(uc_HpAccountSummary1.Uc_AccBalance);
                if (!string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()) && chkApproved.Checked)
                {
                    decimal _value = 0;
                    CalculateRequest();
                    if (radDiscount.Checked)
                        _value = Convert.ToDecimal(lblReqDiscountAmount.Text);
                    else if (radPartRelease.Checked)
                        _value = Convert.ToDecimal(lblReqReleaseAmount.Text);


                    if (radDiscount.Checked) lblSumDiscountAmt.Text = FormatToCurrency(lblReqDiscountAmount.Text);
                    if (radPartRelease.Checked) lblSumReleaseAmt.Text = FormatToCurrency(lblReqReleaseAmount.Text);
                    lblTotalReceivable.Text = FormatToCurrency(Convert.ToString(_value));

                }
                else
                {
                    lblTotalReceivable.Text = FormatToCurrency(lblSumAccBalance.Text);
                }

                lblSumReceipt.Text = FormatToCurrency(lblTotalReceivable.Text);
                lblSumPay.Text = FormatToCurrency(lblTotalReceivable.Text);
            }
        }
        protected void CalculateRequest(object sender, EventArgs e)
        {
            CalculateRequest();
            CalculateBalanceSheet();
        }

        #endregion

        private void collection_Headers_Gen(List<RecieptHeader> _receiptHeader)
        {
            foreach (RecieptHeader _h in _receiptHeader)
            {
                foreach (RecieptHeader _i in Receipt_List)
                {
                    if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                    {
                        if (_h.Sar_tot_settle_amt <= (Convert.ToDecimal(lblSumPay.Text.Trim())) && _h.Sar_tot_settle_amt != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = _h.Sar_tot_settle_amt;
                            lblSumPay.Text = (Convert.ToDecimal(lblSumPay.Text.Trim()) - _h.Sar_tot_settle_amt).ToString();
                            _h.Sar_tot_settle_amt = 0;
                            Final_recHeaderList.Add(_i);

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(lblSumPay.Text.Trim())) != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = Convert.ToDecimal(lblSumPay.Text.Trim());
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - Convert.ToDecimal(lblSumPay.Text.Trim());
                            lblSumPay.Text = (Convert.ToDecimal(0)).ToString();
                            Final_recHeaderList.Add(_i);
                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(lblSumPay.Text.Trim())) == 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = _i.Sar_tot_settle_amt + _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = 0;
                            //Final_recHeaderList.Add(_i);
                        }
                    }
                }
            }
        }

        public List<HpTransaction> Transaction_List
        {
            get { return (List<HpTransaction>)ViewState["Transaction_List"]; }
            set { ViewState["Transaction_List"] = value; }
        }
        private void fill_Transactions(RecieptHeader r_hdr)
        {
            //(to write to hpt_txn)
            HpTransaction tr = new HpTransaction();
            tr.Hpt_acc_no = lblAccountNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
            tr.Hpt_cre_by = GlbUserName;
            tr.Hpt_cre_dt = Convert.ToDateTime(txtDate.Text);
            tr.Hpt_dbt = 0;
            tr.Hpt_com = GlbUserComCode;
            tr.Hpt_pc = GlbUserDefProf;
            if (r_hdr.Sar_is_oth_shop == true)
            {
                tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + GlbUserDefProf; ;
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+GlbUserDefProf;   //"prefix-receiptNo-pc"

            }
            else
            {
                tr.Hpt_desc = ("Payment receive").ToUpper();

            }
            if (r_hdr.Sar_is_mgr_iss)
            {
                //"prefix-receiptNo-issues"
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();

            }
            else
            { //"prefix-receiptNo"
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
            }
            tr.Hpt_pc = GlbUserDefProf;

            tr.Hpt_ref_no = "";
            tr.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
            tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();


            Transaction_List.Add(tr);

        }

        protected void Process(object sender, EventArgs e)
        {
            #region Priliminary Checking before save
            if (gvRevetedItem.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the reverted items");
                return;
            }
            if (gvRevertedSerial.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the reverted serial");
                return;
            }
            if (gvReceipts.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select receipt detail");
                return;
            }
            if (gvPayment.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the payment detail");
                return;
            }
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the profit center");
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the date");
                txtDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the account no");
                txtAccountNo.Focus();
                return;
            }
            #endregion

            string _bin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
            OriginalRevertedItemList.ForEach(x => x.Tus_loc = GlbUserDefLoca);
            OriginalRevertedItemList.ForEach(x => x.Tus_bin = _bin);

            #region Fill Revert Header

            HpRevertHeader _rvhdr = new HpRevertHeader();
            _rvhdr.Hrt_acc_no = lblAccountNo.Text;
            _rvhdr.Hrt_bal = 0;
            _rvhdr.Hrt_bal_cap = 0;
            _rvhdr.Hrt_bal_intr = 0;
            _rvhdr.Hrt_com = GlbUserComCode;
            _rvhdr.Hrt_cre_by = GlbUserName;
            _rvhdr.Hrt_cre_dt = DateTime.Now.Date;
            _rvhdr.Hrt_is_rls = false;
            _rvhdr.Hrt_mod_by = GlbUserName;
            _rvhdr.Hrt_mod_dt = DateTime.Now.Date;
            _rvhdr.Hrt_pc = txtProfitCenter.Text;
            _rvhdr.Hrt_ref = "0";
            _rvhdr.Hrt_rvt_dt = Convert.ToDateTime(txtDate.Text);
            _rvhdr.Hrt_seq = 0;
            _rvhdr.Hrt_rvt_comment = string.Empty;


            //ADDED BY SACHITH 2012/11/05
            //ADD HRT_RVT_BY,HRT_RLS_DT

            string _rvtBy = GlbUserName;
            _rvhdr.Hrt_rvt_by = _rvtBy;
            _rvhdr.Hrt_rls_dt = new DateTime(9999, 12, 31);

            //END


            #endregion

            #region Fill Inventory Header
            InventoryHeader inHeader = new InventoryHeader();
            inHeader.Ith_acc_no = lblAccountNo.Text;
            inHeader.Ith_anal_10 = true;
            inHeader.Ith_anal_11 = true;
            inHeader.Ith_anal_12 = true;
            inHeader.Ith_anal_2 = lblAccountNo.Text;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_bus_entity = string.Empty;
            inHeader.Ith_cate_tp = "NOR";
            inHeader.Ith_channel = string.Empty;
            inHeader.Ith_com = GlbUserComCode;
            inHeader.Ith_com_docno = string.Empty;
            inHeader.Ith_cre_by = string.Empty;
            inHeader.Ith_cre_when = DateTime.MinValue;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";
            inHeader.Ith_direct = true;
            inHeader.Ith_doc_date = DateTime.Today;
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = DateTime.Today.Year;
            inHeader.Ith_entry_no = string.Empty;
            inHeader.Ith_entry_tp = string.Empty;
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = "1";
            inHeader.Ith_isprinted = true;
            inHeader.Ith_is_manual = true;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = GlbUserDefLoca;
            inHeader.Ith_manual_ref = string.Empty;
            inHeader.Ith_mod_by = GlbUserName;
            inHeader.Ith_mod_when = DateTime.MinValue;
            inHeader.Ith_noofcopies = 2;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_remarks = string.Empty;
            inHeader.Ith_sbu = string.Empty;
            inHeader.Ith_seq_no = 6;
            inHeader.Ith_session_id = GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "RV";
            inHeader.Ith_vehi_no = string.Empty;
            #endregion

            #region Fill Inentory AutoNumber
            MasterAutoNumber invAuto = new MasterAutoNumber();
            invAuto.Aut_cate_cd = GlbUserDefLoca;
            invAuto.Aut_cate_tp = "LOC";
            invAuto.Aut_direction = null;
            invAuto.Aut_modify_dt = null;
            invAuto.Aut_moduleid = "ADJ";
            invAuto.Aut_number = 0;
            invAuto.Aut_start_char = "ADJ";
            invAuto.Aut_year = null;
            #endregion

            #region Fill Revert AutoNumber
            MasterAutoNumber rvAuto = new MasterAutoNumber();
            rvAuto.Aut_cate_cd = GlbUserDefLoca;
            rvAuto.Aut_cate_tp = "PC";
            rvAuto.Aut_direction = 1;
            rvAuto.Aut_modify_dt = null;
            rvAuto.Aut_moduleid = "RV";
            rvAuto.Aut_number = 0;
            rvAuto.Aut_start_char = "RV";
            rvAuto.Aut_year = null;
            string _rvdoc = string.Empty;
            string _adjdoc = string.Empty;
            #endregion

            #region Preparing Receipt Entry For the Invoice (OUT)

            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
            _receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HP";
            _receiptAuto.Aut_number = 0;
            _receiptAuto.Aut_year = null;

            List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            receiptHeaderList = Receipt_List;

            List<RecieptItem> receipItemList = new List<RecieptItem>();
            receipItemList = _recieptItem;
            List<RecieptItem> save_receipItemList = new List<RecieptItem>();
            List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
            Int32 tempHdrSeq = 0;
            foreach (RecieptHeader _h in receiptHeaderList)
            {
                _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                fill_Transactions(_h);
                tempHdrSeq--;
                Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                foreach (RecieptItem _i in receipItemList)
                {
                    if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                    {
                        // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                        //  save_receipItemList.Add(_i);
                        // finish_receipItemList.Add(_i);
                        RecieptItem ri = new RecieptItem();
                        //ri = _i;
                        ri.Sard_settle_amt = _i.Sard_settle_amt;
                        ri.Sard_pay_tp = _i.Sard_pay_tp;
                        ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                        ri.Sard_seq_no = _h.Sar_seq_no;
                        //-------------------------------    //have to copy all properties.
                        ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                        ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                        ri.Sard_cc_period = _i.Sard_cc_period;
                        ri.Sard_cc_tp = _i.Sard_cc_tp;
                        ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                        ri.Sard_chq_branch = _i.Sard_chq_branch;
                        ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                        ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                        ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                        //--------------------------------
                        ri.Sard_ref_no = _i.Sard_ref_no;

                        //********
                        ri.Sard_anal_3 = _i.Sard_anal_3;
                        //--------------------------------
                        save_receipItemList.Add(ri);

                        _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                        _i.Sard_settle_amt = 0;

                    }
                    else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                    {
                        RecieptItem ri = new RecieptItem();
                        ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                        ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                        ri.Sard_pay_tp = _i.Sard_pay_tp;
                        ri.Sard_seq_no = _h.Sar_seq_no;
                        //-------------------------------    //have to copy all properties.
                        ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                        ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                        ri.Sard_cc_period = _i.Sard_cc_period;
                        ri.Sard_cc_tp = _i.Sard_cc_tp;
                        ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                        ri.Sard_chq_branch = _i.Sard_chq_branch;
                        ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                        ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                        ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                        //--------------------------------
                        ri.Sard_ref_no = _i.Sard_ref_no;
                        //--------------------------------
                        save_receipItemList.Add(ri);
                        _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                        _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                    }
                }
                _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


            }

            #endregion

            #region

            string _outInvDocument = string.Empty;
            string _outSaleDocument = string.Empty;

            try
            {
                btnSave.Enabled = false;
                btnClear.Enabled = false; 

                CHNLSVC.Sales.SaveRevertRelease(txtProfitCenter.Text.Trim(), inHeader, invAuto, OriginalRevertedItemList, null, Final_recHeaderList, _receiptAuto, ReceiptItem, out _outInvDocument, out _outSaleDocument,false,0,0,0,0,null,null);

                btnSave.Enabled = true;
                btnClear.Enabled = true;
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                btnClear.Enabled = true;
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Document not generated due to internal error. Please retry!");
                return;
            }
            finally
            {
                btnSave.Enabled = true;
                btnClear.Enabled = true;

                GlbDocNosList = _rvdoc;
                GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";

                GlbMainPage = "~/Inventory_Module/InterCompanyOutWardEntry.aspx";

                string Msg = "<script> alert('Successfully Saved! Document No : " + _outSaleDocument + " and inventory document : " + _outInvDocument + " \n'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts123", Msg, false);
                Msg = "<script> window.open('../Reports_Module/Inv_Rep/Print.aspx','_blank');  </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts23", Msg, false);

                Msg = "<script> window.location = 'HpRevert.aspx';  </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts2311", Msg, false);
            }

            #endregion
        }
    }
}