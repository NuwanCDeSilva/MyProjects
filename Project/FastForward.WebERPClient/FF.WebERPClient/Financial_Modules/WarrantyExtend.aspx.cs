using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class WarrantyExtend : BasePage
    {

        protected MasterBankAccount _bankAccounts
        {
            get { return (MasterBankAccount)Session["_bankAccounts"]; }
            set { Session["_bankAccounts"] = value; }
        }

        protected Int32 _SeqID
        {
            get { return (Int32)Session["_SeqID"]; }
            set { Session["_SeqID"] = value; }
        }

        protected List<PriceBookLevelRef> _PriceBook
        {
            get { return (List<PriceBookLevelRef>)Session["_PriceBook"]; }
            set { Session["_PriceBook"] = value; }
        }

        protected List<PriceDetailRef> _price
        {
            get { return (List<PriceDetailRef>)Session["_price"]; }
            set { Session["_price"] = value; }
        }

        protected List<RecieptItem> _RecieptDetails
        {
            get { return (List<RecieptItem>)Session["_RecieptDetails"]; }
            set { Session["_RecieptDetails"] = value; }
        }

        protected RecieptHeader _ReceiptHeader
        {
            get { return (RecieptHeader)Session["_ReceiptHeader"]; }
            set { Session["_ReceiptHeader"] = value; }
        }

        protected decimal GrndTotal
        {
            get { return (decimal)Session["GrndTotal"]; }
            set { Session["GrndTotal"] = value; }
        }

        protected decimal _maxAllowDays
        {
            get { return (decimal)Session["_maxAllowDays"]; }
            set { Session["_maxAllowDays"] = value; }
        }

        protected Int32 _lineNo
        {
            get { return (Int32)Session["_lineNo"]; }
            set { Session["_lineNo"] = value; }
        }

        protected List<ReceiptWaraExtend> _recWaraExtend
        {
            get { return (List<ReceiptWaraExtend>)Session["_recWaraExtend"]; }
            set { Session["_recWaraExtend"] = value; }
        }

        protected HpSystemParameters _SystemPara
        {
            get { return (HpSystemParameters)Session["_SystemPara"]; }
            set { Session["_SystemPara"] = value; }
        }

        protected decimal _comAmt
        {
            get { return (decimal)Session["_comAmt"]; }
            set { Session["_comAmt"] = value; }
        }

        protected string _invTP
        {
            get { return (string)Session["_invTP"]; }
            set { Session["_invTP"] = value; }
        }

        protected Boolean _isPartPay
        {
            get { return (Boolean)Session["_isPartPay"]; }
            set { Session["_isPartPay"] = value; }
        }

        protected decimal _totComm
        {
            get { return (decimal)Session["_totComm"]; }
            set { Session["_totComm"] = value; }
        }

        protected decimal _examount
        {
            get { return (decimal)Session["_examount"]; }
            set { Session["_examount"] = value; }
        }

        protected Boolean _IsRecall
        {
            get { return (Boolean)Session["_IsRecall"]; }
            set { Session["_IsRecall"] = value; }
        }

        protected Boolean _RecStatus
        {
            get { return (Boolean)Session["_RecStatus"]; }
            set { Session["_RecStatus"] = value; }
        }

        protected Boolean _sunUpload
        {
            get { return (Boolean)Session["_sunUpload"]; }
            set { Session["_sunUpload"] = value; }
        }
            
            

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtInvSerial.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvEngine, ""));
                txtInv.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInv, ""));
                txtInvItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvItem, ""));
                ddlBook.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPriceBook, ""));
                ddlLevel.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPriceLevel, ""));
                txtExNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnRecNo, ""));
                ddlPayMode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPayType, ""));
                txtManual.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnManual, ""));
                txtBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));
                txtDBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAcc, ""));


                _IsRecall = false;
                _RecStatus = false;
                _sunUpload = false;
                _SeqID = 0;
                GrndTotal = 0;
                _lineNo = 0;
                _maxAllowDays = 0;
                _comAmt = 0;
                _invTP = "";
                _isPartPay = false;
                _totComm = 0;
                _examount = 0;
                _PriceBook = new List<PriceBookLevelRef>();
                _price = new List<PriceDetailRef>();
                _RecieptDetails = new List<RecieptItem>();
                _recWaraExtend = new List<ReceiptWaraExtend>();
                _ReceiptHeader = new RecieptHeader();
                _SystemPara = new HpSystemParameters();
                this.Clear_Data();
            }
        }


        private void Clear_Data()
        {
            txtInv.ReadOnly = false;
            txtInv.Text = "";
            txtExNo.Text = "";
            txtInvItem.Text = "";
            txtInvSerial.Text = "";
            lblOthSerial.Text = "";
            lblInvDate.Text = "";
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblDo.Text = "";
            lblWaraNo.Text = "";
            lblWarPeriod.Text = "";
            lblCusAdd1.Text = "";
            lblCusAdd2.Text = "";
            lblNewPeriod.Text = "";
            txtPayAmt.Text = "";
            txtRemarks.Text = "";
            txtTotal.Text = "";
            _isPartPay = false;
            _totComm = 0;
            btnSave.Enabled = true;
            BankDetailsDisable();
            _lineNo = 0;
            _PriceBook = new List<PriceBookLevelRef>();
            _price = new List<PriceDetailRef>();
            _RecieptDetails = new List<RecieptItem>();
            _recWaraExtend = new List<ReceiptWaraExtend>();
            _ReceiptHeader = new RecieptHeader();
            
            _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", GlbUserDefProf, "WAREXDTS", Convert.ToDateTime(DateTime.Now).Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _maxAllowDays = _SystemPara.Hsy_val;
            }

            DataTable _Itemtable = new DataTable();
            gvRecDetails.DataSource = _Itemtable;
            gvRecDetails.DataBind();


            gvWaraDetails.DataSource = _Itemtable;
            gvWaraDetails.DataBind();

            
            ddlBook.DataSource = null;
            ddlBook.DataBind();

            ddlLevel.DataSource = null;
            ddlLevel.DataBind();

            BindPaymentType(ddlPayMode);

            txtInv.Focus();
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInv.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllSalesInvoice:
                    {


                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                    {

                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtInv.Text.Trim() + seperator + txtInvItem.Text.Trim() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "WAREX" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void CalculateGrandTotal(decimal _amt, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndTotal = GrndTotal + Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            else//--
            {
                GrndTotal = GrndTotal - Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }

        }



        protected void BindAddItem()
        {
            gvRecDetails.DataSource = _RecieptDetails;
            gvRecDetails.DataBind();

        }

        protected void BindAddExtendDet()
        {
            gvWaraDetails.DataSource = _recWaraExtend;
            gvWaraDetails.DataBind();
        }

        protected void Img_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBankAccounts(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvItem_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInv.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select related invoice #.");
                txtInvItem.Text = "";
                txtInv.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceItem(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvSearch_Click(object sender, ImageClickEventArgs e)
        {


            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllSalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInv.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvEngine_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select related invoice item.");
                txtInvItem.Focus();
                return;
            }


            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DeliverdSerials);
            DataTable dataSource = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvSerial.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void CheckValidInvEngine(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvSerial.Text))
            {

                InventorySerialN _delList = new InventorySerialN();
                _delList = CHNLSVC.Inventory.GetDeliveredSerialForItem(GlbUserComCode, txtInv.Text.Trim(), txtInvItem.Text.Trim(), txtInvSerial.Text.Trim());

                if (_delList != null)
                {
                    if (_delList.Ins_doc_no != null)
                    {
                        txtInvSerial.Text = _delList.Ins_ser_1;
                        lblOthSerial.Text = _delList.Ins_ser_2;
                        _SeqID = _delList.Ins_ser_id;
                        lblDo.Text = _delList.Ins_doc_no;
                        lblWarPeriod.Text = _delList.Ins_warr_period.ToString();
                        lblWaraNo.Text = _delList.Ins_warr_no;

                        ddlBook.DataSource = null;
                        ddlBook.DataBind();

                        List<PriceBookLevelRef> _def3 = CHNLSVC.Sales.getWarrExBook(GlbUserComCode, string.Empty, string.Empty);
                        _PriceBook = new List<PriceBookLevelRef>();
                        if (_def3 != null)
                        {
                            _PriceBook.AddRange(_def3);
                        }


                        var _final = (from _lst in _PriceBook
                                      select _lst.Sapl_pb).ToList().Distinct();

                        if (_final != null)
                        {
                            ddlBook.DataSource = _final;
                            ddlBook.DataBind();
                        }
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid serial #.");
                        txtInvSerial.Text = "";
                        lblOthSerial.Text = "";
                        _SeqID = 0;
                        lblDo.Text = "";
                        lblWarPeriod.Text = "";
                        lblWaraNo.Text = "";
                            
                        ddlBook.DataSource = null;
                        ddlBook.DataBind();
                        txtInvSerial.Focus();
                        return;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid serial #.");
                    txtInvSerial.Text = "";
                    lblOthSerial.Text = "";
                    _SeqID = 0;
                    lblDo.Text = "";
                    lblWarPeriod.Text = "";
                    lblWaraNo.Text = "";

                    ddlBook.DataSource = null;
                    ddlBook.DataBind();
                    txtInvSerial.Focus();
                    return;
                }
            }
        }

        protected void GetInvDet(object sender, EventArgs e)
        {

            Int32 _daysDiff = 0;
            btnSave.Enabled =true;
            if (!string.IsNullOrEmpty(txtInv.Text))
            {
                 List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(GlbUserComCode, GlbUserDefProf, string.Empty, txtInv.Text.Trim(), "C", lblInvDate.Text, lblInvDate.Text);

                if (_invHdr.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice.");
                    txtInv.Text = "";
                    return;
                }

           


                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice.");
                        txtInv.Text = "";
                        lblCusName.Text = "";
                        lblCusCode.Text = "";
                        lblCusAdd1.Text = "";
                        lblCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        _invTP = "";
                        txtInv.Focus();
                        return;
                    }
                    else
                    {
                        lblInvDate.Text = Convert.ToDateTime(_tempInv.Sah_dt).ToShortDateString();
                        lblCusCode.Text = _tempInv.Sah_cus_cd;
                        lblCusName.Text = _tempInv.Sah_cus_name;
                        lblCusAdd1.Text = _tempInv.Sah_cus_add1;
                        lblCusAdd2.Text = _tempInv.Sah_cus_add2;
                        _invTP = _tempInv.Sah_inv_tp;
                    }
                }

               
                    TimeSpan a = Convert.ToDateTime(DateTime.Now.Date) - Convert.ToDateTime(lblInvDate.Text.Trim());
                    _daysDiff = a.Days;

                    if (_daysDiff > _maxAllowDays)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Date Expire.");
                        btnSave.Enabled = false;
                        return;
                    }
                
            }
        }

        protected void CheckValidInvItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvItem.Text))
            {
                InvoiceItem _invItem = new InvoiceItem();
                _invItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInv.Text.Trim(), txtInvItem.Text.Trim());

                if (_invItem == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice item.");
                    txtInvItem.Text = "";
                    txtInvItem.Focus();
                    return;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        protected void ddlBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PriceBookLevelRef> _def4 = CHNLSVC.Sales.getWarrExBook(GlbUserComCode, ddlBook.SelectedValue, string.Empty);
            _PriceBook = new List<PriceBookLevelRef>();
            if (_def4 != null)
            {
                _PriceBook.AddRange(_def4);
            }


            var _final = (from _lst in _PriceBook
                          select _lst.Sapl_pb_lvl_cd).ToList().Distinct();

            if (_final != null)
            {
                ddlLevel.DataSource = _final;
                ddlLevel.DataBind();
            }
        }

        protected void LoadPriceLvl(object sender, EventArgs e)
        {
            List<PriceBookLevelRef> _def4 = CHNLSVC.Sales.getWarrExBook(GlbUserComCode, ddlBook.SelectedValue, string.Empty);
            _PriceBook = new List<PriceBookLevelRef>();

            ddlLevel.DataSource = null;
            ddlLevel.DataBind();

            if (_def4 != null)
            {
                _PriceBook.AddRange(_def4);
            }

            var _final = (from _lst in _PriceBook
                          select _lst.Sapl_pb_lvl_cd).ToList().Distinct();

            if (_final != null)
            {
                ddlLevel.DataSource = _final;
                ddlLevel.DataBind();
            }
        }

        protected void LoadAmount(object sender, EventArgs e)
        {
            Int32 i = 0;
            List<PriceBookLevelRef> _def5 = CHNLSVC.Sales.getWarrExBook(GlbUserComCode, ddlBook.SelectedValue, ddlLevel.SelectedValue);
            _PriceBook = new List<PriceBookLevelRef>();
            if (_def5 != null)
            {
                _PriceBook.AddRange(_def5);
            }

            foreach (PriceBookLevelRef j in _PriceBook)
            {
                lblNewPeriod.Text = j.Sapl_warr_period.ToString();
            }

            _price = new List<PriceDetailRef>();
            _price = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, string.Empty, ddlBook.SelectedValue, ddlLevel.SelectedValue, string.Empty, txtInvItem.Text.Trim(), 1, Convert.ToDateTime(DateTime.Now));

            foreach (PriceDetailRef h in _price)
            {
                lblAmt.Text = h.Sapd_itm_price.ToString("0.00");
                txtPayAmt.Text = h.Sapd_itm_price.ToString("0.00");
                _examount = h.Sapd_itm_price;
                goto L1;
            }
        L1: i = i + 1;
        }

        protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 i = 0;
            List<PriceBookLevelRef> _def5 = CHNLSVC.Sales.getWarrExBook(GlbUserComCode, ddlBook.SelectedValue, ddlLevel.SelectedValue);
            _PriceBook = new List<PriceBookLevelRef>();
            if (_def5 != null)
            {
                _PriceBook.AddRange(_def5);
            }

            foreach (PriceBookLevelRef j in _PriceBook)
            {
                lblNewPeriod.Text = j.Sapl_warr_period.ToString();
            }

            _price = new List<PriceDetailRef>();
            _price = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, string.Empty, ddlBook.SelectedValue, ddlLevel.SelectedValue, string.Empty, txtInvItem.Text.Trim(), 1, Convert.ToDateTime(DateTime.Now));

            foreach (PriceDetailRef h in _price)
            {
                lblAmt.Text = h.Sapd_itm_price.ToString("0.00");
                goto L1;
            }
        L1: i = i + 1;
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "CS", Convert.ToDateTime(DateTime.Now).Date);
            if (_paymentTypeRef != null)
            {
                _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Stp_pay_tp);
                _ddl.DataTextField = "Stp_pay_tp";
                _ddl.DataValueField = "Stp_pay_tp";
                _ddl.DataBind();
            }

        }

        protected void CheckPayType(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                BankDetailsEnable();
                if (_type.Sapt_cd != "CRCD")
                {
                    ddlRefType.Enabled = false;
                }
            }
            else
            {
                BankDetailsDisable();
            }
            txtPayAmt.Focus();
        }

        private void BankDetailsDisable()
        {
            //  ddlRefType.Text = "";
            txtRef.Text = "";
            txtBank.Text = "";
            txtBranch.Text = "";

            ddlRefType.Enabled = false;
            txtRef.Enabled = false;
            txtBank.Enabled = false;
            txtBranch.Enabled = false;
            ImgBankSearch.Enabled = false;
        }

        private void BankDetailsEnable()
        {
            // ddlRefType.Text = "";
            txtRef.Text = "";
            txtBank.Text = "";
            txtBranch.Text = "";


            ddlRefType.Enabled = true;
            txtRef.Enabled = true;
            txtBank.Enabled = true;
            txtBranch.Enabled = true;
            ImgBankSearch.Enabled = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                if (string.IsNullOrEmpty(txtRef.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment reference.");
                    txtRef.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment bank.");
                    txtBank.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBranch.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment branch.");
                    txtBranch.Focus();
                    return;
                }

                if (ddlPayMode.Text == "CRCD")
                {
                    if (string.IsNullOrEmpty(ddlRefType.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter card type.");
                        ddlRefType.Focus();
                        return;
                    }
                }

            }

            if (string.IsNullOrEmpty(lblCusCode.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is missing.");
                lblCusCode.Focus();
                return;
            }


            if (Convert.ToDecimal(txtPayAmt.Text) > Convert.ToDecimal(lblAmt.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Settle amount is exceed.");
                txtPayAmt.Text = "";
                txtPayAmt.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtInvItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item.");
                txtInvItem.Focus();
                return;
            }


            if (Convert.ToDecimal(txtPayAmt.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Settle amount cannot be zero.");
                txtPayAmt.Text = "";
                txtPayAmt.Focus();
                return;
            }

            foreach (ReceiptWaraExtend temp in _recWaraExtend)
            {
                if (temp.Srw_itm == txtInvItem.Text.Trim() && temp.Srw_ser == txtInvSerial.Text.Trim())
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Already added above serial.");
                    txtInvSerial.Focus();
                    return;
                }
            }

            //foreach (ReceiptWaraExtend _item in _recWaraExtend)
            //{
            //    if (_item.Srw_itm == txtInvItem.Text.Trim() && _item.Srw_ser == txtInvSerial.Text.Trim())
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Already added above serial.");
            //        txtInvSerial.Focus();
            //        return;
            //    }
            //}


            _lineNo += 1;

            _comAmt = CHNLSVC.Sales.Process_WaraEx_Commission(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(DateTime.Now).Date, txtInvItem.Text.Trim(), ddlBook.SelectedValue, ddlLevel.SelectedValue, _invTP, _lineNo, Convert.ToDecimal(txtPayAmt.Text), ddlPayMode.SelectedValue);
            _RecieptDetails.Add(AssignDataToObject());
            BindAddItem();

            lblAmt.Text = (Convert.ToDecimal(lblAmt.Text) - Convert.ToDecimal(txtPayAmt.Text)).ToString("0.00");
            

            if (Convert.ToDecimal(lblAmt.Text) == 0)
            {
                _totComm = _totComm + _comAmt;
                _isPartPay = false;
                _recWaraExtend.Add(AssignWaraDataToObject());
                BindAddExtendDet();
                _totComm = 0;
            }
            else
            {
                _totComm = _totComm + _comAmt;
                _isPartPay = true;
            }

            CalculateGrandTotal(Convert.ToDecimal(txtPayAmt.Text), true);

            txtPayAmt.Text = lblAmt.Text;
            txtInv.ReadOnly = true;

            if (Convert.ToDecimal(txtPayAmt.Text) > 0)
            {
                
                ddlRefType.Text = "";
                txtRef.Text = "";
                txtBank.Text = "";
                lblBankName.Text = "";
                txtBranch.Text = "";
                txtDBank.Text = "";
                txtDBranch.Text = "";
                lblDBankDesc.Text = "";

                ddlBook.Enabled = false;
                ddlLevel.Enabled = false;
                txtInvItem.Enabled = false;
                txtInvSerial.Enabled = false;
                txtPayAmt.Focus();
            }
            else
            {
                ddlBook.Enabled = true;
                ddlLevel.Enabled = true;
                txtInvItem.Enabled = true;
                txtInvSerial.Enabled = true;
                lblAmt.Text = "";
                BindPaymentType(ddlPayMode);
                txtPayAmt.Text = "";
                ddlRefType.Text = "";
                txtRef.Text = "";
                txtBank.Text = "";
                lblBankName.Text = "";
                txtBranch.Text = "";
                txtDBank.Text = "";
                txtDBranch.Text = "";
                lblDBankDesc.Text = "";
                txtInvItem.Text = "";
                lblOthSerial.Text = "";
                lblDo.Text = "";
                lblWaraNo.Text = "";
                lblWarPeriod.Text = "";
                lblNewPeriod.Text = "";
                txtInvSerial.Text = "";
                ddlBook.DataSource = null;
                ddlBook.DataBind();

                ddlLevel.DataSource = null;
                ddlLevel.DataBind();
                txtInv.Focus();
            }
        }


        private ReceiptWaraExtend AssignWaraDataToObject()
        {
            ReceiptWaraExtend _tempWara = new ReceiptWaraExtend();

            _tempWara.Srw_seq_no = 1;
            _tempWara.Srw_line = _lineNo;
            _tempWara.Srw_rec_no = "1";
            _tempWara.Srw_inv_no = txtInv.Text.Trim();
            _tempWara.Srw_do_no = lblDo.Text.Trim();
            _tempWara.Srw_date = Convert.ToDateTime(DateTime.Now).Date;
            _tempWara.Srw_itm = txtInvItem.Text.Trim();
            _tempWara.Srw_ser = txtInvSerial.Text.Trim();
            _tempWara.Srw_oth_ser = lblOthSerial.Text.Trim();
            _tempWara.Srw_warra = lblWaraNo.Text.Trim();
            _tempWara.Srw_ex_period = Convert.ToInt32(lblWarPeriod.Text);
            _tempWara.Srw_new_period = Convert.ToInt32(lblNewPeriod.Text);
            _tempWara.Srw_pb = ddlBook.SelectedValue;
            _tempWara.Srw_lvl = ddlLevel.SelectedValue;
            _tempWara.Srw_amt = _examount;
            _tempWara.Srw_cre_by = GlbUserName;
            _tempWara.Srw_cre_when = Convert.ToDateTime(DateTime.Now).Date;
            _tempWara.Srw_ser_id = _SeqID;
            _tempWara.Srw_comm_amt = _totComm;

            return _tempWara;
        }

        private RecieptItem AssignDataToObject()
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            string _Amt = "";
            RecieptItem _tempItem = new RecieptItem();

            _tempItem.Sard_seq_no = 1;
            _tempItem.Sard_line_no = _lineNo;
            _tempItem.Sard_receipt_no = "1";
            _tempItem.Sard_inv_no = txtInv.Text;
            _tempItem.Sard_pay_tp = ddlPayMode.Text;
            _tempItem.Sard_ref_no = txtRef.Text;
            _tempItem.Sard_chq_bank_cd = txtBank.Text;
            _tempItem.Sard_chq_branch = txtBranch.Text;
            _tempItem.Sard_deposit_bank_cd = txtDBank.Text;
            _tempItem.Sard_deposit_branch = txtDBranch.Text;
            _tempItem.Sard_credit_card_bank = txtBank.Text;
            _tempItem.Sard_cc_tp = ddlRefType.Text;
            _tempItem.Sard_cc_expiry_dt = Convert.ToDateTime(DateTime.Now).Date;
            _tempItem.Sard_cc_is_promo = false;
            _tempItem.Sard_cc_period = 0;
            _tempItem.Sard_gv_issue_loc = "";
            _tempItem.Sard_gv_issue_dt = Convert.ToDateTime(DateTime.Now).Date;

            _Amt = Convert.ToDecimal(txtPayAmt.Text).ToString("0.00", CultureInfo.InvariantCulture);
            _tempItem.Sard_settle_amt = Convert.ToDecimal(_Amt);
            //Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture)
            _tempItem.Sard_sim_ser = "";
            _tempItem.Sard_anal_1 = "";
            _tempItem.Sard_anal_2 = "";
            _tempItem.Sard_anal_3 = _comAmt;
            _tempItem.Sard_anal_4 = 0;
            _tempItem.Sard_anal_5 = Convert.ToDateTime(DateTime.Now).Date;

            return _tempItem;


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(lblCusCode.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is missing.");
                txtInv.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtInv.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invoice is missing.");
                txtInv.Focus();
                return;
            }

            if (gvWaraDetails.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Extended details are not found.");
                txtInv.Focus();
                return;
            }

            SaveWarrantyExtend();

        }

        protected void SaveWarrantyExtend()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            decimal _commission = 0;

            foreach (RecieptItem _com in _RecieptDetails)
            {
                _commission = _commission + _com.Sard_anal_3;
            }

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "EXREC", 1, GlbUserComCode);
            _ReceiptHeader.Sar_com_cd = GlbUserComCode;
            _ReceiptHeader.Sar_receipt_type = "WAREX";
            _ReceiptHeader.Sar_receipt_no = txtExNo.Text.Trim();
            _ReceiptHeader.Sar_prefix = "WAREX";
            _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(DateTime.Now).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = GlbUserDefProf;
            _ReceiptHeader.Sar_debtor_cd = lblCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = lblCusName.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_1 = lblCusAdd1.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_2 = lblCusAdd2.Text.Trim(); ;
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = "";
            _ReceiptHeader.Sar_nic_no = "";
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtTotal.Text);
            _ReceiptHeader.Sar_comm_amt = _commission;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = "";
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;
            _ReceiptHeader.Sar_create_by = GlbUserName;
            _ReceiptHeader.Sar_mod_by = GlbUserName;
            _ReceiptHeader.Sar_session_id = GlbUserSessionID;
            _ReceiptHeader.Sar_anal_1 = "";
            _ReceiptHeader.Sar_anal_2 = "";
            _ReceiptHeader.Sar_anal_3 = "";
            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_8 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;

            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            foreach (RecieptItem line in _RecieptDetails)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _ReceiptDetailsSave.Add(line);
            }


            List<ReceiptWaraExtend> _ReceiptWaraExtendSave = new List<ReceiptWaraExtend>();
            foreach (ReceiptWaraExtend Ext in _recWaraExtend)
            {
                Ext.Srw_seq_no = _ReceiptHeader.Sar_seq_no;
                _ReceiptWaraExtendSave.Add(Ext);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserDefProf;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "WAREX";
            masterAuto.Aut_year = null;

            string QTNum;

            row_aff = (Int32)CHNLSVC.Sales.SaveWarrExReceipt(_ReceiptHeader, _ReceiptDetailsSave, _ReceiptWaraExtendSave, masterAuto, out QTNum);

            if (chkIsManual.Checked == true)
            {
                CHNLSVC.Inventory.UpdateManualDocNo(GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManual.Text));
            }

            if (row_aff == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.Receipt No: " + QTNum);
                if (string.IsNullOrEmpty(QTNum)) return;

                string Msg = "";
                GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
                clearReportParameters();
                GlbDocNosList = QTNum.Trim();



                GlbReportName = "ReceiptPrint";
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";

                Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }

        }

        protected void imgExNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceiptsByType(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtExNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void GetSaveReceipt(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExNo.Text))
            {
                LoadSaveReceipt();
            }

        }

        private void LoadSaveReceipt()
        {

            _IsRecall = false;
            _RecStatus = false;
            _sunUpload = false;

            RecieptHeader _ReceiptHeader = null;
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(GlbUserComCode, GlbUserDefProf, txtExNo.Text.Trim());
            if (_ReceiptHeader != null)
            {
                txtExNo.Text = _ReceiptHeader.Sar_receipt_no;
                txtManual.Text = _ReceiptHeader.Sar_manual_ref_no;
                txtRemarks.Text = _ReceiptHeader.Sar_remarks;
                txtTotal.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                lblCusAdd1.Text = _ReceiptHeader.Sar_debtor_add_1;
                lblCusAdd2.Text = _ReceiptHeader.Sar_debtor_add_2;
                lblCusName.Text = _ReceiptHeader.Sar_debtor_name;
                _RecStatus = _ReceiptHeader.Sar_act;
                _sunUpload = _ReceiptHeader.Sar_uploaded_to_finance;
            }

            BindSaveReceiptDetails(_ReceiptHeader.Sar_receipt_no);
            BindSaveExtendWarra(_ReceiptHeader.Sar_receipt_no);
            _IsRecall = true;
            btnSave.Enabled = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtExNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select receipt #.");
                txtExNo.Focus();
                return;
            }

            if (_RecStatus == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select receipt is already cancelled.");
                txtExNo.Focus();
                return;
            }

            if (_sunUpload == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot cancel.Already uploaded to accounts.");
                return;
            }



            UpdateRecStatus(false);
            _RecieptDetails = new List<RecieptItem>();
            _recWaraExtend = new List<ReceiptWaraExtend>();
            GrndTotal = 0;
            _IsRecall = false;
            _RecStatus = false;
            _lineNo = 0;
            _sunUpload = false;
            txtInv.ReadOnly = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = true;
        }

        private void UpdateRecStatus(Boolean _RecUpdateStatus)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            RecieptHeader _UpdateReceipt = new RecieptHeader();
            _UpdateReceipt.Sar_receipt_no = txtExNo.Text.Trim();
            _UpdateReceipt.Sar_act = _RecUpdateStatus;
            _UpdateReceipt.Sar_com_cd = GlbUserComCode;
            _UpdateReceipt.Sar_profit_center_cd = GlbUserDefProf;
            _UpdateReceipt.Sar_mod_by = GlbUserName;

            List<ReceiptWaraExtend> _tempWaraList = new List<ReceiptWaraExtend>();
            _tempWaraList = _recWaraExtend;

            row_aff = (Int32)CHNLSVC.Sales.CancelWaraRec(_UpdateReceipt, _tempWaraList);

            if (row_aff == 1)
            {

                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully cancelled. ");
                }
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }
        }

        private void BindSaveReceiptDetails(string _RecNo)
        {
            RecieptItem _paramRecDetails = new RecieptItem();

            _paramRecDetails.Sard_receipt_no = _RecNo;

            List<RecieptItem> _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
            _RecieptDetails = new List<RecieptItem>();
            _RecieptDetails = _list;
            gvRecDetails.DataSource = _RecieptDetails;
            gvRecDetails.DataBind();

        }

        private void BindSaveExtendWarra(string _RecNo)
        {
            List<ReceiptWaraExtend> _list = CHNLSVC.Sales.GetWarrantyExtendReceipt(_RecNo);
            _recWaraExtend = new List<ReceiptWaraExtend>();
            _recWaraExtend = _list;
            gvWaraDetails.DataSource = _recWaraExtend;
            gvWaraDetails.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExNo.Text)) return;

            string Msg = "";
            GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
            clearReportParameters();
            GlbDocNosList = txtExNo.Text.Trim();



            GlbReportName = "ReceiptPrint";
            GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrint.rpt";
            GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";

            Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        }

        protected void chkIsManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsManual.Checked == true)
            {
                txtManual.Text = "";
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, GlbUserDefLoca, "MDOC_AVREC");
                if (_NextNo != 0)
                {
                    txtManual.Text = _NextNo.ToString();
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find any available advance receipts.");
                    txtManual.Text = "";
                    chkIsManual.Checked = false;
                    txtManual.Focus();
                    return;

                }
            }

            else
            {
                txtManual.Text = string.Empty;

            }
        }

        //protected void OnRemoveFromRecDetails(object sender, GridViewDeleteEventArgs e)
        //{
        //    int row_id = e.RowIndex;

        //    if (_RecieptDetails != null)
        //        if (_RecieptDetails.Count > 0)
        //        {

        //            decimal _uprice = (decimal)gvRecDetails.DataKeys[row_id][0];

                    
        //            CalculateGrandTotal(_uprice, false);
        //            List<RecieptItem> _tempList = _RecieptDetails;
        //            _lineNo = _lineNo - 1;
        //            _tempList.RemoveAt(row_id);
        //            _RecieptDetails = _tempList;
        //            BindAddItem();
        //        }

        //}

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                BankDetailsEnable();
                if (_type.Sapt_cd != "CRCD")
                {
                    ddlRefType.Enabled = false;
                }
            }
            else
            {
                BankDetailsDisable();
            }
            txtPayAmt.Focus();
        }

        protected void CheckValidManualRef(object sender, EventArgs e)
        {
            if (chkIsManual.Checked == true)
            {
                if (txtManual.Text != "")
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManual.Text));
                    if (_IsValid == false)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                        txtManual.Text = "";
                        txtManual.Focus();
                    }
                }
                else
                {
                    if (chkIsManual.Checked == true)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                        txtManual.Focus();
                    }
                }
            }
        }

        protected void GetBankDetails(object sender, EventArgs e)
        {
            MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
            _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtBank.Text.Trim(), "BANK");

            if (_OutPartyDetails.Mbi_cd != null)
            {
                txtBank.Text = _OutPartyDetails.Mbi_cd;
              //  lblBankName.Text = _OutPartyDetails.Mbi_desc;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                txtBank.Text = "";
                lblBankName.Text = "";
                txtBank.Focus();
                return;
            }

            txtBranch.Focus();
        }

        protected void GetAccDetails(object sender, EventArgs e)
        {
            _bankAccounts = new MasterBankAccount();
            if (!string.IsNullOrEmpty(txtDBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, "", txtDBank.Text);

                if (_bankAccounts.Msba_acc_cd != null)
                {
                    txtDBank.Text = _bankAccounts.Msba_acc_cd;
                   // lblDBankDesc.Text = _bankAccounts.Msba_acc_desc;
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid account.");
                    txtDBank.Text = "";
                    lblDBankDesc.Text = "";
                    txtDBank.Focus();
                    return;
                }
            }
            txtDBranch.Focus();
        }

    }
}