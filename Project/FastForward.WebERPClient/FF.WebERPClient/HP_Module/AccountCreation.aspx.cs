using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.WebERPClient.UserControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;
using System.Transactions;
using System.IO;
using System.Text.RegularExpressions;



namespace FF.WebERPClient.HP_Module
{
    public partial class AccountCreation : BasePage
    {
        protected static MasterBusinessEntity _masterBusinessCompany = null;
        protected static BlackListCustomers _blackListCustomers = null;

        InvoiceHeader _invheader = new InvoiceHeader();
        MasterAutoNumber _invNo = new MasterAutoNumber();
        HpAccount _HPAcc = new HpAccount();
        MasterAutoNumber _AccNo = new MasterAutoNumber();
        HPAccountLog _HPAccLog = new HPAccountLog();
        MasterAutoNumber _MainTxnAuto = new MasterAutoNumber();

        //public MasterAutoNumber _MainTxnAuto
        //{
        //    get { return (MasterAutoNumber)Session["_MainTxnAuto"]; }
        //    set { Session["_MainTxnAuto"] = value; }
        //}

        //protected override void Render(HtmlTextWriter output)
        //{
        //   StringWriter stringWriter = new StringWriter();

        //    HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);
        //    base.Render(textWriter);

        //    //textWriter.Close();

        //  //  string strOutput = stringWriter.GetStringBuilder().ToString();

        //    string strOutput = Regex.Replace(stringWriter.GetStringBuilder().ToString(), "<input[^>]*id=\"__VIEWSTATE\"[^>]*>", "", RegexOptions.Singleline);

        //   // output.Write(strOutput);
        //}

        protected string WarrantyRemarks
        {
            get { return (string)Session["WarrantyRemarks"]; }
            set { Session["WarrantyRemarks"] = value; }
        }
        //Store Warranty Period
        protected Int32 WarrantyPeriod
        {
            get { return (Int32)Session["WarrantyPeriod"]; }
            set { Session["WarrantyPeriod"] = value; }
        }


        protected List<MasterItemComponent> _masterItemComponent
        {
            get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; }
            set { Session["_masterItemComponent"] = value; }
        }

        public List<HpCustomer> _HpAccCust
        {
            get { return (List<HpCustomer>)Session["_HpAccCust"]; }
            set { Session["_HpAccCust"] = value; }
        }

        public List<HpTransaction> _MainTrans
        {
            get { return (List<HpTransaction>)Session["_MainTrans"]; }
            set { Session["_MainTrans"] = value; }
        }

        public List<MasterBusinessEntity> _HpCustomer
        {
            get { return (List<MasterBusinessEntity>)Session["_HpCustomer"]; }
            set { Session["_HpCustomer"] = value; }
        }

        public List<MasterBusinessEntity> _HpGuranter
        {
            get { return (List<MasterBusinessEntity>)Session["_HpGuranter"]; }
            set { Session["_HpGuranter"] = value; }
        }

        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(Session["PaidAmount"]); }
            set { Session["PaidAmount"] = Math.Round(value, 2); }
        }

        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(Session["BalanceAmount"]); }
            set { Session["BalanceAmount"] = Math.Round(value, 2); }
        }

        public Decimal AmtToPayForFinishPayment
        {
            get { return Convert.ToDecimal(Session["AmtToPayForFinishPayment"]); }
            set { Session["AmtToPayForFinishPayment"] = Math.Round(value, 2); }
        }

        public List<PaymentType> PaymentTypes
        {
            get { return (List<PaymentType>)Session["PaymentTypes"]; }
            set { Session["PaymentTypes"] = value; }
        }

        public Decimal BankOrOther_Charges
        {
            get { return Convert.ToDecimal(Session["BankOrOther_Charges"]); }
            set { Session["BankOrOther_Charges"] = Math.Round(value, 2); }
        }

        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)Session["Receipt_List"]; }
            set { Session["Receipt_List"] = value; }
        }

        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)Session["RecieptItemList"]; }
            set { Session["RecieptItemList"] = value; }
        }

        protected MasterBusinessEntity _businessEntity
        {
            get { return (MasterBusinessEntity)Session["_businessEntity"]; }
            set { Session["_businessEntity"] = value; }
        }

        protected List<MasterBusinessCompany> _selectBusinessentity
        {
            get { return (List<MasterBusinessCompany>)Session["_selectBusinessentity"]; }
            set { Session["_selectBusinessentity"] = value; }
        }

        protected MasterItem _masterItemDetails
        {
            get { return (MasterItem)Session["_masterItemDetails"]; }
            set { Session["_masterItemDetails"] = value; }
        }

        protected MasterCompanyItem _mastercompanyItem
        {
            get { return (MasterCompanyItem)Session["_mastercompanyItem"]; }
            set { Session["_mastercompanyItem"] = value; }
        }

        protected HpSystemParameters _SystemPara
        {
            get { return (HpSystemParameters)Session["_SystemPara"]; }
            set { Session["_SystemPara"] = value; }
        }

        protected HpSchemeDetails _SchemeDetails
        {
            get { return (HpSchemeDetails)Session["_SchemeDetails"]; }
            set { Session["_SchemeDetails"] = value; }
        }

        protected HpSchemeType _SchemeType
        {
            get { return (HpSchemeType)Session["_SchemeType"]; }
            set { Session["_SchemeType"] = value; }
        }

        protected List<MasterItem> _selectItemList
        {
            get { return (List<MasterItem>)Session["_selectItemList"]; }
            set { Session["_selectItemList"] = value; }
        }

        protected List<HpSheduleDetails> _sheduleDetails
        {
            get { return (List<HpSheduleDetails>)Session["_sheduleDetails"]; }
            set { Session["_sheduleDetails"] = value; }
        }

        protected List<PriceDetailRef> _priceDetails
        {
            get { return (List<PriceDetailRef>)Session["_priceDetails"]; }
            set { Session["_priceDetails"] = value; }
        }

        protected List<PriceDetailRef> _promoPriceDetails
        {
            get { return (List<PriceDetailRef>)Session["_promoPriceDetails"]; }
            set { Session["_promoPriceDetails"] = value; }
        }

        protected PriceBookRef _priceBooks
        {
            get { return (PriceBookRef)Session["_priceBooks"]; }
            set { Session["_priceBooks"] = value; }
        }

        protected List<TempCommonPrice> _tempPriceBook
        {
            get { return (List<TempCommonPrice>)Session["_tempPriceBook"]; }
            set { Session["_tempPriceBook"] = value; }
        }

        protected List<TempCommonPrice> _tempItemWithPrices
        {
            get { return (List<TempCommonPrice>)Session["_tempItemWithPrices"]; }
            set { Session["_tempItemWithPrices"] = value; }
        }

        protected List<InvoiceItem> _AccountItems
        {
            get { return (List<InvoiceItem>)Session["_AccountItems"]; }
            set { Session["_AccountItems"] = value; }
        }

        protected List<HpSchemeDefinition> _SchemeDefinition
        {
            get { return (List<HpSchemeDefinition>)Session["_SchemeDefinition"]; }
            set { Session["_SchemeDefinition"] = value; }
        }

        protected List<HpSchemeDefinition> _SchemeDefinitionComm
        {
            get { return (List<HpSchemeDefinition>)Session["_SchemeDefinitionComm"]; }
            set { Session["_SchemeDefinitionComm"] = value; }
        }

        protected List<HpOtherCharges> _SchemeOtherCharges
        {
            get { return (List<HpOtherCharges>)Session["_SchemeOtherCharges"]; }
            set { Session["_SchemeOtherCharges"] = value; }
        }

        protected List<HpServiceCharges> _ServiceCharges
        {
            get { return (List<HpServiceCharges>)Session["_ServiceCharges"]; }
            set { Session["_ServiceCharges"] = value; }
        }

        protected List<HpAdditionalServiceCharges> _AdditionalServiceCharges
        {
            get { return (List<HpAdditionalServiceCharges>)Session["_AdditionalServiceCharges"]; }
            set { Session["_AdditionalServiceCharges"] = value; }
        }

        protected HpSchemeSheduleDefinition _SchemeSheduleDef
        {
            get { return (HpSchemeSheduleDefinition)Session["_SchemeSheduleDef"]; }
            set { Session["_SchemeSheduleDef"] = value; }
        }

        protected List<PriceCombinedItemRef> _combineItems
        {
            get { return (List<PriceCombinedItemRef>)Session["_combineItems"]; }
            set { Session["_combineItems"] = value; }
        }

        protected decimal _discount
        {
            get { return (decimal)Session["_discount"]; }
            set { Session["_discount"] = value; }
        }

        protected decimal _commission
        {
            get { return (decimal)Session["_commission"]; }
            set { Session["_commission"] = value; }
        }

        protected decimal _NetAmt
        {
            get { return (decimal)Session["_NetAmt"]; }
            set { Session["_NetAmt"] = value; }
        }

        protected decimal _TotVat
        {
            get { return (decimal)Session["_TotVat"]; }
            set { Session["_TotVat"] = value; }
        }

        protected decimal _UVAT
        {
            get { return (decimal)Session["_UVAT"]; }
            set { Session["_UVAT"] = value; }
        }

        protected decimal _IVAT
        {
            get { return (decimal)Session["_IVAT"]; }
            set { Session["_IVAT"] = value; }
        }

        protected decimal _varVATAmt
        {
            get { return (decimal)Session["_varVATAmt"]; }
            set { Session["_varVATAmt"] = value; }
        }

        protected decimal _varCashPrice
        {
            get { return (decimal)Session["_varCashPrice"]; }
            set { Session["_varCashPrice"] = value; }
        }

        protected decimal _DisCashPrice
        {
            get { return (decimal)Session["_DisCashPrice"]; }
            set { Session["_DisCashPrice"] = value; }
        }

        protected decimal _varInstallComRate
        {
            get { return (decimal)Session["_varInstallComRate"]; }
            set { Session["_varInstallComRate"] = value; }
        }

        protected decimal _vDPay
        {
            get { return (decimal)Session["_vDPay"]; }
            set { Session["_vDPay"] = value; }
        }

        protected decimal _varInitialVAT
        {
            get { return (decimal)Session["_varInitialVAT"]; }
            set { Session["_varInitialVAT"] = value; }
        }

        protected decimal _varInsVAT
        {
            get { return (decimal)Session["_varInsVAT"]; }
            set { Session["_varInsVAT"] = value; }
        }

        protected decimal _MinDPay
        {
            get { return (decimal)Session["_MinDPay"]; }
            set { Session["_MinDPay"] = value; }
        }

        protected decimal _maxAllowQty
        {
            get { return (decimal)Session["_maxAllowQty"]; }
            set { Session["_maxAllowQty"] = value; }
        }

        protected decimal _varAmountFinance
        {
            get { return (decimal)Session["_varAmountFinance"]; }
            set { Session["_varAmountFinance"] = value; }
        }

        protected decimal _varServiceCharge
        {
            get { return (decimal)Session["_varServiceCharge"]; }
            set { Session["_varServiceCharge"] = value; }
        }

        protected decimal _varInitServiceCharge
        {
            get { return (decimal)Session["_varInitServiceCharge"]; }
            set { Session["_varInitServiceCharge"] = value; }
        }

        protected decimal _varIntRate
        {
            get { return (decimal)Session["_varIntRate"]; }
            set { Session["_varIntRate"] = value; }
        }

        protected decimal _varInterestAmt
        {
            get { return (decimal)Session["_varInterestAmt"]; }
            set { Session["_varInterestAmt"] = value; }
        }

        protected decimal _varHireValue
        {
            get { return (decimal)Session["_varHireValue"]; }
            set { Session["_varHireValue"] = value; }
        }

        protected decimal _varCommAmt
        {
            get { return (decimal)Session["_varCommAmt"]; }
            set { Session["_varCommAmt"] = value; }
        }

        protected decimal _varAddRental
        {
            get { return (decimal)Session["_varAddRental"]; }
            set { Session["_varAddRental"] = value; }
        }

        protected decimal _varStampduty
        {
            get { return (decimal)Session["_varStampduty"]; }
            set { Session["_varStampduty"] = value; }
        }

        protected decimal _varInitialStampduty
        {
            get { return (decimal)Session["_varInitialStampduty"]; }
            set { Session["_varInitialStampduty"] = value; }
        }

        protected decimal _varServiceChargesAdd
        {
            get { return (decimal)Session["_varServiceChargesAdd"]; }
            set { Session["_varServiceChargesAdd"] = value; }
        }

        protected decimal _varInsAmount
        {
            get { return (decimal)Session["_varInsAmount"]; }
            set { Session["_varInsAmount"] = value; }
        }

        protected decimal _varFInsAmount
        {
            get { return (decimal)Session["_varFInsAmount"]; }
            set { Session["_varFInsAmount"] = value; }
        }

        protected decimal _varInsVATRate
        {
            get { return (decimal)Session["_varInsVATRate"]; }
            set { Session["_varInsVATRate"] = value; }
        }

        protected decimal _varInsCommRate
        {
            get { return (decimal)Session["_varInsCommRate"]; }
            set { Session["_varInsCommRate"] = value; }
        }

        protected decimal _varTotCash
        {
            get { return (decimal)Session["_varTotCash"]; }
            set { Session["_varTotCash"] = value; }
        }

        protected decimal _varTotalInstallmentAmt
        {
            get { return (decimal)Session["_varTotalInstallmentAmt"]; }
            set { Session["_varTotalInstallmentAmt"] = value; }
        }

        protected decimal _varRental
        {
            get { return (decimal)Session["_varRental"]; }
            set { Session["_varRental"] = value; }
        }

        protected decimal _varOtherCharges
        {
            get { return (decimal)Session["_varOtherCharges"]; }
            set { Session["_varOtherCharges"] = value; }
        }

        protected string _SchTP
        {
            get { return (string)Session["_SchTP"]; }
            set { Session["_SchTP"] = value; }
        }

        protected decimal _ExTotAmt
        {
            get { return (decimal)Session["_ExTotAmt"]; }
            set { Session["_ExTotAmt"] = value; }
        }

        protected Boolean _isBlackList
        {
            get { return (Boolean)Session["_isBlackList"]; }
            set { Session["_isBlackList"] = value; }
        }

        protected Boolean _isExsistCus
        {
            get { return (Boolean)Session["_isExsistCus"]; }
            set { Session["_isExsistCus"] = value; }
        }

        protected string _cusMsg
        {
            get { return (string)Session["_cusMsg"]; }
            set { Session["_cusMsg"] = value; }
        }

        protected static string _cusCode = "";



        protected void Page_Load(object sender, EventArgs e)
        {

            cusCreP1.UserControlButtonClicked += new
       EventHandler(txtHiddenCustCD_TextChanged);

            cusCreP1.EnableMainButtons(false);

            if (!IsPostBack)
            {
                //text box upper case
                txtIdentiry.Attributes.Add("onkeypress", "uppercase();");
                txtItem.Attributes.Add("onkeypress", "uppercase();");
                //txtNIC.Attributes.Add("onkeypress", "uppercase();");
                //txtcusName.Attributes.Add("onkeypress", "uppercase();");

                //F2 function
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + btnItemSelect.ClientID + "')");

                //enter key function
                txtIdentiry.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCheck.ClientID + "').focus();return false;}} else {return true}; ");
                //txtQty.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnAddItem.ClientID + "').focus();return false;}} else {return true}; ");
                //txtNIC.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtPass.ClientID + "').focus();return false;}} else {return true}; ");
                //txtPass.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtcusName.ClientID + "').focus();return false;}} else {return true}; ");
                //txtcusName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAdd1.ClientID + "').focus();return false;}} else {return true}; ");
                //txtAdd1.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAdd2.ClientID + "').focus();return false;}} else {return true}; ");

                //lost focus validation
                txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
                txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));
                txtInsCom.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInsuCom, ""));
                txtInsPol.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPolicy, ""));

                this.Clear_Data();
                BindPaymentType(ddlPayMode);
                //CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserIP, string.Empty);
                _selectItemList = new List<MasterItem>();
                _priceDetails = new List<PriceDetailRef>();
                _promoPriceDetails = new List<PriceDetailRef>();
                _priceBooks = new PriceBookRef();
                _masterItemDetails = new MasterItem();
                _tempPriceBook = new List<TempCommonPrice>();
                _tempItemWithPrices = new List<TempCommonPrice>();
                _AccountItems = new List<InvoiceItem>();
                _SchemeDefinition = new List<HpSchemeDefinition>();
                _SchemeDefinitionComm = new List<HpSchemeDefinition>();
                _SchemeDetails = new HpSchemeDetails();
                _ServiceCharges = new List<HpServiceCharges>();
                _SchemeOtherCharges = new List<HpOtherCharges>();
                _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
                _SchemeSheduleDef = new HpSchemeSheduleDefinition();
                _sheduleDetails = new List<HpSheduleDetails>();
                _businessEntity = new MasterBusinessEntity();
                _SystemPara = new HpSystemParameters();
                _selectBusinessentity = new List<MasterBusinessCompany>();
                _mastercompanyItem = new MasterCompanyItem();
                _invheader = new InvoiceHeader();
                _invNo = new MasterAutoNumber();
                _HPAcc = new HpAccount();
                _AccNo = new MasterAutoNumber();
                _HPAccLog = new HPAccountLog();
                Receipt_List = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
                PaymentTypes = new List<PaymentType>();
                _HpCustomer = new List<MasterBusinessEntity>();
                _HpGuranter = new List<MasterBusinessEntity>();
                _SchemeType = new HpSchemeType();
                _MainTrans = new List<HpTransaction>();
                _MainTxnAuto = new MasterAutoNumber();
                _HpAccCust = new List<HpCustomer>();
                _masterItemComponent = new List<MasterItemComponent>();
                _combineItems = new List<PriceCombinedItemRef>();
                AmtToPayForFinishPayment = 0;
                BankOrOther_Charges = 0;
                PaidAmount = 0;
                IsEditMode = false;
                _maxAllowQty = 0;
                _isBlackList = false;
                _isExsistCus = false;
                WarrantyRemarks = "";
                WarrantyPeriod = 0;
                _cusMsg = "";
                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPSA", DateTime.Now.Date);
                _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", GlbUserDefProf, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

                if (_SystemPara.Hsy_cd != null)
                {
                    _maxAllowQty = _SystemPara.Hsy_val;
                }
                txtItem.Focus();
                ////get allow invoice qty
                //// _foundItem = false;
                //_SystemPara = new HpSystemParameters();
                //_SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", GlbUserDefProf, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

                //if (_SystemPara.Hsy_cd != null)
                //{
                //    _maxAllowQty = _SystemPara.Hsy_val;
                //}
                //else
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Maxsimum qty parameter not define.");
                //    _maxAllowQty = 0;
                //    return;
                //}
                //_discount = 0;
                //_commission = 0;
                //_TotVat = 0;
                //_NetAmt = 0;
                //_varCashPrice = 0;
                //_varVATAmt = 0;
                //_UVAT = 0;
                //_IVAT = 0;
                //_DisCashPrice = 0;
                //_varInstallComRate = 0;
                //_MinDPay = 0;
                //_varInsVAT = 0;
                //_varInitialVAT = 0;
                //_vDPay = 0;
                //_varAmountFinance = 0;
                //_varServiceCharge = 0;
                //_varInitServiceCharge = 0;
                //_varInterestAmt = 0;
                //_varIntRate = 0;
                //_varHireValue = 0;
                //_varCommAmt = 0;
                //_varAddRental = 0;
                //_varStampduty = 0;
            }

        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        protected void imgSearchAcc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAllHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtReAcc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void LoadItem(object sender, EventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        private void Clear_Data()
        {
            cusCreP1.EnableMainButtons(false);
            txtIdentiry.Text = "";
            //lblCusinfo.Text = "";
            txtItem.Text = "";
            txtDesc.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtQty.Text = "";
            rbCus.Checked = true;
            //Trial cal dispaly area
            lblCreateDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            ddlScheme.DataSource = null;
            ddlScheme.DataBind();
            ddlScheme.Enabled = true;
            lblTerm.Text = "0.00";
            lblCashPrice.Text = "0.00";
            lblVat.Text = "0.00";
            lblAmtFin.Text = "0.00";
            lblService.Text = "0.00";
            lblIntAmt.Text = "0.00";
            lblTotHire.Text = "0.00";
            lblComAmt.Text = "0.00";
            lblDPay.Text = "0.00";
            lblComRate.Text = "0.00";
            lblDisRate.Text = "0.00";
            lblDisAmt.Text = "0.00";
            lblTotCha.Text = "0.00";
            lblInsu.Text = "0.00";
            lblAddRent.Text = "0.00";
            lblstampduty.Text = "0.00";
            lblTotCash.Text = "0.00";
            lblTotAmt.Text = "0.00";
            txtCusPay.Text = "0.00";
            lblInsFee.Text = "0.00";
            lblRegFee.Text = "0.00";
            lblTobePay.Text = "0.00";
            lblPayPaid.Text = "0.00";
            PaidAmount = 0;
            BalanceAmount = 0;


            rdoBtnSystem.Checked = true;
            loadPrifixes();
            //txtNIC.Text = "";
            //txtcusName.Text = "";
            //txtPass.Text = "";
            //txtAdd1.Text = "";

            pnlScheme.Visible = false;
            CollapsiblePanelExtender1.Collapsed = false;
            this.CollapsiblePanelExtender1.ClientState = "false";

            txtProAdd1.Text = "";
            txtProAdd2.Text = "";
            txtProAdd3.Text = "";
            txtPreAdd1.Text = "";
            txtPreAdd2.Text = "";
            txtPreAdd3.Text = "";

            MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
            _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
            if (_OutPartyDetails.Mbi_cd != null)
            {
                txtInsCom.Text = _OutPartyDetails.Mbi_cd;
            }

            InsuarancePolicy _insuPolicy = new InsuarancePolicy();
            _insuPolicy = CHNLSVC.Sales.GetInusPolicy(null);
            if (_insuPolicy.Svip_polc_cd != null)
            {
                txtInsPol.Text = _insuPolicy.Svip_polc_cd;
            }

            lblTot.Text = "0.00";
            lblTotVat.Text = "0.00";

            _discount = 0;
            _commission = 0;
            _TotVat = 0;
            _NetAmt = 0;
            _varCashPrice = 0;
            _varVATAmt = 0;
            _UVAT = 0;
            _IVAT = 0;
            _DisCashPrice = 0;
            _varInstallComRate = 0;
            _MinDPay = 0;
            _varInsVAT = 0;
            _varInitialVAT = 0;
            _vDPay = 0;
            _varAmountFinance = 0;
            _varServiceCharge = 0;
            _varInitServiceCharge = 0;
            _varInterestAmt = 0;
            _varIntRate = 0;
            _varHireValue = 0;
            _varCommAmt = 0;
            _varAddRental = 0;
            _varStampduty = 0;
            _varInitialStampduty = 0;
            _varServiceChargesAdd = 0;
            _varInsAmount = 0;
            _varFInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            _varTotCash = 0;
            _varTotalInstallmentAmt = 0;
            _varRental = 0;
            _varOtherCharges = 0;
            _ExTotAmt = 0;
            btnCreateAcc.Enabled = true;


            PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPSA", DateTime.Now.Date);

            DataTable _Itemtable = new DataTable();
            gvReceipts.DataSource = _Itemtable;
            gvReceipts.DataBind();

            gvHPItem.DataSource = _Itemtable;
            gvHPItem.DataBind();

            gvNormalPrice.DataSource = _Itemtable;
            gvNormalPrice.DataBind();

            gvPromoPrice.DataSource = _Itemtable;
            gvPromoPrice.DataBind();

            gvBooks.DataSource = _Itemtable;
            gvBooks.DataBind();

            //gvItemWithPrice.DataSource = _Itemtable;
            //gvItemWithPrice.DataBind();

            gvAllItems.DataSource = _Itemtable;
            gvAllItems.DataBind();

            gvSchedule.DataSource = _Itemtable;
            gvSchedule.DataBind();

            gvOther.DataSource = _Itemtable;
            gvOther.DataBind();

            gvCus.DataSource = _Itemtable;
            gvCus.DataBind();

            gvGur.DataSource = _Itemtable;
            gvGur.DataBind();

            gvPayment.DataSource = _Itemtable;
            gvPayment.DataBind();

            grvPopUpCombines.DataSource = _Itemtable;
            grvPopUpCombines.DataBind();

            gvfreeItem.DataSource = _Itemtable;
            gvfreeItem.DataBind();

            //get allow invoice qty
            // _foundItem = false;
            _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", GlbUserDefProf, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _maxAllowQty = _SystemPara.Hsy_val;
            }
            //else
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Maxsimum qty parameter not define.");
            //    _maxAllowQty = 0;
            //    return;
            //}

            CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);   
 
            if (GlbUserComCode == "AAL")
            {
                chkDel.Checked = true;
                chkDel.Enabled = false;
            }
            else
            {
                chkDel.Enabled = true;
                chkDel.Checked = false;
            }

            txtItem.Focus();

        }

        protected void txtHiddenCustCD_TextChanged(object sender, EventArgs e)
        {
            //SET VALUES IN THE PAGE
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(cusCreP1.CustCode);
            // ddlHO_status.SelectedValue = custProf.Mbe_ho_stus;
            // ddlSH_status.SelectedValue = custProf.Mbe_pc_stus;

            cusCreP2.SetExtraValues(custProf);
            // cusCreP2.EnableAddressPanel(true);



        }


        protected void btnCreateAcc_Click(object sender, EventArgs e)
        {
            try
            {
                string _documentNo = "";
                string _AccountNo = "";
                string _InvoiceNo = "";
                string _msg = string.Empty;
                string _type = "";
                string _value = "";
                Int32 _NoOfGur = 0;
                Boolean _foundGur = false;
                Int32 I = 0;
                decimal _defAccVal = 0;
                decimal _defNoAcc = 0;
                decimal _accVal = 0;
                decimal _NoAcc = 0;
                _MainTrans = new List<HpTransaction>();

                if (gvAllItems.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No items are selected.");
                    return;
                }

                if (gvCus.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is missing.");
                    return;
                }

                if (string.IsNullOrEmpty(txtPreCode.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer Present address is missing.");
                    return;
                }

                if (gvGur.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Gurantor is missing.");
                    return;
                }

                if (BalanceAmount != 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total payment amount must match the total receipt amount!");
                    return;
                }

                if (Convert.ToDecimal(lblPayBalance.Text) != 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total payment amount must match the total receipt amount!");
                    return;
                }

                if (GlbUserDefProf == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Session expire. Please re-log and continue.");
                    return;
                }

                decimal _totRecAmt = 0;
                decimal _totAmtShouldCollect = 0;

                foreach (RecieptHeader _list in Receipt_List)
                {
                    _totRecAmt = _totRecAmt + _list.Sar_tot_settle_amt;
                }

                _totAmtShouldCollect = Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(lblAddRent.Text);

                if (_totAmtShouldCollect > _totRecAmt)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total receipt amount not match with total cash and rental amount.");
                    return;
                }

                //check required no of gurantors are exsist.
                List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_hir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _hir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        List<HPGurantorParam> _gur = CHNLSVC.Sales.getGurParam(ddlScheme.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);

                        if (_gur != null)
                        {
                            foreach (HPGurantorParam _two in _gur)
                            {
                                _foundGur = false;
                                if (_two.Hpg_chk_on == "UP" && (_two.Hpg_from_val <= _DisCashPrice && _two.Hpg_to_val >= _DisCashPrice))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                                else if (_two.Hpg_chk_on == "AF" && (_two.Hpg_from_val <= _varAmountFinance && _two.Hpg_to_val >= _varAmountFinance))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                                else if (_two.Hpg_chk_on == "HP" && (_two.Hpg_from_val <= _varHireValue && _two.Hpg_to_val >= _varHireValue))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                            }
                        }

                    }
                }
            L1: I = I + 1;

                //comment becuse very troble.... 14-11-2012
                //if (_foundGur == false)
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Gurantor parameter not set.");
                //    return;
                //}

                //if (gvGur.Rows.Count < _NoOfGur)
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Required gurantors :" + _NoOfGur + "not found.");
                //    return;
                //}

                //check whether account creation is blocked
                List<HpAccRestriction> _rest = CHNLSVC.Sales.getAccRest(GlbUserDefProf, Convert.ToDateTime(lblCreateDate.Text).Date, 1);

                if (_rest != null)
                {
                    foreach (HpAccRestriction _three in _rest)
                    {
                        _defNoAcc = _three.Hrs_no_ac;
                        _defAccVal = _three.Hrs_tot_val;

                        List<HpAccount> _Acc = CHNLSVC.Sales.getAccDetRest(GlbUserComCode, GlbUserDefProf, _three.Hrs_from_dt, _three.Hrs_to_dt);

                        if (_Acc != null)
                        {
                            foreach (HpAccount _four in _Acc)
                            {
                                _NoAcc = _NoAcc + 1;
                                _accVal = _accVal + _four.Hpa_cash_val;
                            }
                        }
                        if (_defNoAcc < (_NoAcc + 1) || _defAccVal < (_accVal + _DisCashPrice))
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Account creation restricted by credit department [Month].");
                            return;
                        }

                        goto L2;
                    }
                }
            L2: I = I + 1;

                _defAccVal = 0;
                _defNoAcc = 0;
                _NoAcc = 0;
                _accVal = 0;
                //check whether account creation is blocked
                List<HpAccRestriction> _restAnl = CHNLSVC.Sales.getAccRest(GlbUserDefProf, Convert.ToDateTime(lblCreateDate.Text).Date, 2);

                if (_restAnl != null)
                {
                    foreach (HpAccRestriction _Five in _restAnl)
                    {
                        _defNoAcc = _Five.Hrs_no_ac;
                        _defAccVal = _Five.Hrs_tot_val;

                        List<HpAccount> _AccAnl = CHNLSVC.Sales.getAccDetRest(GlbUserComCode, GlbUserDefProf, _Five.Hrs_from_dt, _Five.Hrs_to_dt);

                        if (_AccAnl != null)
                        {
                            foreach (HpAccount _six in _AccAnl)
                            {
                                _NoAcc = _NoAcc + 1;
                                _accVal = _accVal + _six.Hpa_cash_val;
                            }
                        }
                        if (_defNoAcc < (_NoAcc + 1) || _defAccVal < (_accVal + _DisCashPrice))
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Account creation restricted by credit department [Annual].");
                            return;
                        }

                        goto L3;
                    }
                }
            L3: I = I + 1;



                CollectInvoiceHeader();
                CollectAccount();
                CollectAccountLog();
                CollectTxn("OPBAL", 0, "");
                CollectTxn("DIRIYA", 0, "");

                //collect and process receipt details

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
                    //fill_Transactions(_h);
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

                    if (_h.Sar_receipt_type == "HPDPM")
                    {
                        CollectTxn("HPDPM", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPDPS")
                    {
                        CollectTxn("HPDPS", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPARM")
                    {
                        CollectTxn("HPARM", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPARS")
                    {
                        CollectTxn("HPARS", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }

                }
                gvPayment.DataSource = save_receipItemList;
                gvPayment.DataBind();

                //saveAll_HP_Collect_Recipts

                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "HP";
                _receiptAuto.Aut_number = 0;
                //Fill the Aut_start_char at the saving place (in BLL)
                //if (_h.Sar_receipt_type=="HPRS")
                //{ _receiptAuto.Aut_start_char = "HPRS"; }
                //else if (_h.Sar_receipt_type == "HPRM")
                //{ _receiptAuto.Aut_start_char = "HPRM"; }
                //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _receiptAuto.Aut_year = null;
                #endregion

                #region Txn autonumber value assign
                _MainTxnAuto = new MasterAutoNumber();
                _MainTxnAuto.Aut_cate_cd = GlbUserDefProf;
                _MainTxnAuto.Aut_cate_tp = "PC";
                _MainTxnAuto.Aut_direction = 1;
                _MainTxnAuto.Aut_modify_dt = null;
                _MainTxnAuto.Aut_moduleid = "HP";
                _MainTxnAuto.Aut_number = 0;
                _MainTxnAuto.Aut_start_char = "HPT";
                _MainTxnAuto.Aut_year = null;
                #endregion


                #region Insuarance value assing
                MasterAutoNumber _insuNo = new MasterAutoNumber();
                _insuNo.Aut_cate_cd = GlbUserDefProf;
                _insuNo.Aut_cate_tp = "PC";
                _insuNo.Aut_direction = 1;
                _insuNo.Aut_modify_dt = null;
                _insuNo.Aut_moduleid = "RECEIPT";
                _insuNo.Aut_number = 0;
                _insuNo.Aut_start_char = "INSU";
                _insuNo.Aut_year = null;
                #endregion


                HpInsurance _tempInsu = new HpInsurance();
                _tempInsu.Hit_is_rvs = false;
                _tempInsu.Hti_acc_num = null;
                _tempInsu.Hti_com = GlbUserComCode;
                _tempInsu.Hti_comm_rt = _varInsCommRate;
                decimal _vatAmt = _varFInsAmount / 112 * _varInsVATRate;
                _tempInsu.Hti_comm_val = (_varFInsAmount - _vatAmt) / 100 * _varInsCommRate;
                _tempInsu.Hti_cre_by = GlbUserName;
                _tempInsu.Hti_cre_dt = Convert.ToDateTime(lblCreateDate.Text);
                _tempInsu.Hti_dt = Convert.ToDateTime(lblCreateDate.Text);
                _tempInsu.Hti_epf = 0;
                _tempInsu.Hti_esd = 0;
                _tempInsu.Hti_ins_val = _varFInsAmount;
                _tempInsu.Hti_mnl_num = null;
                _tempInsu.Hti_pc = GlbUserDefProf;
                _tempInsu.Hti_ref = null;
                _tempInsu.Hti_seq = 1;
                _tempInsu.Hti_vat_rt = _varInsVATRate;
                _tempInsu.Hti_vat_val = _vatAmt;
                _tempInsu.Hti_wht = 0;


                //add hpt customers
                HpCustomer _tmpCust = new HpCustomer();

                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtPreCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 3;
                _tmpCust.Htc_adr_01 = txtPreAdd1.Text;
                _tmpCust.Htc_adr_02 = txtPreAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtPreCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 4;
                _tmpCust.Htc_adr_01 = txtProAdd1.Text;
                _tmpCust.Htc_adr_02 = txtProAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);


                if (_AccountItems == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Item Details are missing. Please re-try.");
                    return;
                }

                if (_AccountItems.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Item Details are missing. Please re-try.");
                    return;
                }
                btnCreateAcc.Enabled = false;

                int effect = CHNLSVC.Sales.CreateHPAccount(_HPAcc, _AccNo, _invheader, _AccountItems, _invNo, _HPAccLog, _sheduleDetails, Receipt_List, save_receipItemList, _receiptAuto, _MainTrans, _MainTxnAuto, _HpAccCust, _tempInsu, _insuNo, GlbUserDefLoca, out _documentNo, out _AccountNo, out _InvoiceNo);



                if (effect == 1)
                {

                    string Msg = "";
                    //string Msg = "<script>alert('Account create Successfully!');window.location = 'AccountCreation.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Account No: " + _documentNo);
                    if (string.IsNullOrEmpty(_InvoiceNo)) return;
                   
                    Msg = "";
                    GlbMainPage = "~/HP_Module/AccountCreation.aspx";
                    //GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
                    clearReportParameters();

                    GlbDocNosList = _InvoiceNo.Trim();
                    //GlbDocNosList = "SGBAN-HS00016";

                    GlbReportSerialList = null;
                    GlbReportWarrantyList = null;
                    if (GlbUserComCode == "SGL")
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrintPre.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrintPre.rpt";
                    }
                    else
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoiceHalfPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                    }


                    //   GlbReportPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                    //   GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                    GlbOthReportPath = "~\\Reports_Module\\Sales_Rep\\InsuPrint.rpt";
                    GlbOthReportMapPath = "~/Reports_Module/Sales_Rep/InsuPrint.rpt";




                    GlbOthDocNosList = _AccountNo;
                    //GlbOthDocNosList = "SGBAN-000016";
                    GlbOthReportName = "Sahana";
                    GlbReportName = "Invoice";

                    if (chkDel.Checked == false) { CallDObyInvoice = _InvoiceNo; CallDobyInvoiceManual = _AccountNo; } else { CallDObyInvoice = null; CallDobyInvoiceManual = null; }
                    if (chkDel.Checked == false && _varFInsAmount > 0)
                    {
                        Msg = "<script>window.open('../../Inventory_Module/DeliveryOrder.aspx','_self');window.open('../../Reports_Module/Sales_Rep/InvoiceEntryPrint.aspx','_blank');window.open('../../Reports_Module/Sales_Rep/InsuaranceReceiptPrint.aspx','_blank');</script>";
                    }
                    else if (chkDel.Checked == false && _varFInsAmount <= 0)
                    {
                        Msg = "<script>window.open('../../Inventory_Module/DeliveryOrder.aspx','_self');window.open('../../Reports_Module/Sales_Rep/InvoiceEntryPrint.aspx','_blank');</script>";
                    }
                    else if (chkDel.Checked == true && _varFInsAmount <= 0)
                    {
                        Msg = "<script>window.open('../../Reports_Module/Sales_Rep/InvoiceEntryPrint.aspx','_blank')</script>";
                        //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
                    }
                    else if (chkDel.Checked == true && _varFInsAmount > 0)
                    {
                        Msg = "<script>window.open('../../Reports_Module/Sales_Rep/InvoiceEntryPrint.aspx','_blank');window.open('../../Reports_Module/Sales_Rep/InsuaranceReceiptPrint.aspx','_blank');</script>";
                    }
                    else
                    {

                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    //another report
                    //GlbReportPath = "~/Reports_Module/Sales_Rep/InsuPrint.rpt";
                    //GlbReportMapPath = "~/Reports_Module/Sales_Rep/InsuPrint.rpt";

                    //GlbDocNosList = txtReAcc.Text.Trim();
                    //GlbReportName = "Sahana";

                    //GlbMainPage = "~/HP_Module/AccountCreation.aspx";
                    //Msg = "window.open('../Test/PdfPrint.aspx',  '_blank');";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InsuPrint", Msg, true);

                    //GlbMainPage = "~/HP_Module/AccountCreation.aspx";
                    //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
                    //GlbReportPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";
                    //GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";

                    //GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
                    //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");

                    //Response.Redirect("~/Inventory_Module/DeliveryOrder.aspx");
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
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }


        }

        protected void CollectAccountLog()
        {
            _HPAccLog = new HPAccountLog();

            _HPAccLog.Hal_seq_no = 1;
            _HPAccLog.Hal_acc_no = "na";
            _HPAccLog.Hal_com = GlbUserComCode;
            _HPAccLog.Hal_pc = GlbUserDefProf;
            _HPAccLog.Hal_seq = 1;
            _HPAccLog.Hal_sa_sub_tp = "SA";
            _HPAccLog.Hal_log_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAccLog.Hal_rev_stus = false;
            _HPAccLog.Hpa_acc_cre_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAccLog.Hal_grup_cd = ddlGroup.SelectedValue;
            _HPAccLog.Hal_invc_no = "na";
            _HPAccLog.Hal_sch_tp = _SchTP;
            _HPAccLog.Hal_sch_cd = ddlScheme.Text.Trim();
            _HPAccLog.Hal_term = Convert.ToInt16(lblTerm.Text);
            _HPAccLog.Hal_intr_rt = _varIntRate;
            _HPAccLog.Hal_dp_comm = Convert.ToDecimal(lblComRate.Text);
            _HPAccLog.Hal_inst_comm = _varInstallComRate;
            _HPAccLog.Hal_cash_val = _NetAmt;
            _HPAccLog.Hal_tot_vat = _TotVat;
            _HPAccLog.Hal_net_val = _varCashPrice;
            _HPAccLog.Hal_dp_val = Convert.ToDecimal(lblDPay.Text);
            _HPAccLog.Hal_af_val = _varAmountFinance;
            _HPAccLog.Hal_tot_intr = _varInterestAmt;
            _HPAccLog.Hal_ser_chg = _varServiceCharge;
            _HPAccLog.Hal_hp_val = _varHireValue;
            _HPAccLog.Hal_tc_val = _varTotCash;
            _HPAccLog.Hal_init_ins = _varFInsAmount;
            _HPAccLog.Hal_init_vat = _varInitialVAT;
            _HPAccLog.Hal_init_stm = _varInitialStampduty;
            _HPAccLog.Hal_init_ser_chg = _varInitServiceCharge;
            _HPAccLog.Hal_inst_ins = _varInsAmount - _varFInsAmount;
            _HPAccLog.Hal_inst_vat = (_UVAT + _IVAT) - _varInitialVAT;
            _HPAccLog.Hal_inst_stm = _varStampduty - _varInitialStampduty;
            _HPAccLog.Hal_inst_ser_chg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;
            _HPAccLog.Hal_buy_val = 0;
            _HPAccLog.Hal_oth_chg = _varOtherCharges;
            _HPAccLog.Hal_stus = "A";
            _HPAccLog.Hal_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_ecd_stus = false;
            _HPAccLog.Hal_ecd_tp = null;
            _HPAccLog.Hal_mgr_cd = txtEx.Text.Trim();
            _HPAccLog.Hal_is_rsch = false;
            _HPAccLog.Hal_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_bank = "001";
            _HPAccLog.Hal_flag = "001";
            _HPAccLog.Hal_val_01 = 0;
            _HPAccLog.Hal_val_02 = 0;
            _HPAccLog.Hal_val_03 = 0;
            _HPAccLog.Hal_val_04 = 0;
            _HPAccLog.Hal_val_05 = 0;
            _HPAccLog.Hal_cre_by = GlbUserName;
            _HPAccLog.Hal_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;

        }

        protected void CollectAccount()
        {
            _HPAcc = new HpAccount();

            _HPAcc.Hpa_seq_no = 1;
            _HPAcc.Hpa_acc_no = "na";
            _HPAcc.Hpa_com = GlbUserComCode;
            _HPAcc.Hpa_pc = GlbUserDefProf;
            _HPAcc.Hpa_seq = 1;
            _HPAcc.Hpa_acc_cre_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAcc.Hpa_grup_cd = ddlGroup.SelectedValue;
            _HPAcc.Hpa_invc_no = "na";
            _HPAcc.Hpa_sch_tp = _SchTP;
            _HPAcc.Hpa_sch_cd = ddlScheme.Text.Trim();
            _HPAcc.Hpa_term = Convert.ToInt16(lblTerm.Text);
            _HPAcc.Hpa_intr_rt = _varIntRate;
            _HPAcc.Hpa_dp_comm = Convert.ToDecimal(lblComRate.Text);
            _HPAcc.Hpa_inst_comm = _varInstallComRate;
            _HPAcc.Hpa_cash_val = _NetAmt;
            _HPAcc.Hpa_tot_vat = _TotVat;
            _HPAcc.Hpa_net_val = _varCashPrice;
            _HPAcc.Hpa_dp_val = Convert.ToDecimal(lblDPay.Text);
            _HPAcc.Hpa_af_val = _varAmountFinance;
            _HPAcc.Hpa_tot_intr = _varInterestAmt;
            _HPAcc.Hpa_ser_chg = _varServiceCharge;
            _HPAcc.Hpa_hp_val = _varHireValue;
            _HPAcc.Hpa_tc_val = _varTotCash;
            _HPAcc.Hpa_init_ins = _varFInsAmount;
            _HPAcc.Hpa_init_vat = _varInitialVAT;
            _HPAcc.Hpa_init_stm = _varInitialStampduty;
            _HPAcc.Hpa_init_ser_chg = _varInitServiceCharge;
            _HPAcc.Hpa_inst_ins = _varInsAmount - _varFInsAmount;
            _HPAcc.Hpa_inst_vat = (_UVAT + _IVAT) - _varInitialVAT;
            _HPAcc.Hpa_inst_stm = _varStampduty - _varInitialStampduty;
            _HPAcc.Hpa_inst_ser_chg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;
            _HPAcc.Hpa_buy_val = 0;
            _HPAcc.Hpa_oth_chg = _varOtherCharges;
            _HPAcc.Hpa_stus = "A";
            _HPAcc.Hpa_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_ecd_stus = false;
            _HPAcc.Hpa_ecd_tp = null;
            _HPAcc.Hpa_mgr_cd = txtEx.Text.Trim();
            _HPAcc.Hpa_is_rsch = false;
            _HPAcc.Hpa_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_bank = "001";
            _HPAcc.Hpa_flag = "001";
            _HPAcc.Hpa_prt_ack = false;
            _HPAcc.Hpa_val_01 = 0;
            _HPAcc.Hpa_val_02 = 0;
            _HPAcc.Hpa_val_03 = 0;
            _HPAcc.Hpa_val_04 = 0;
            _HPAcc.Hpa_val_05 = 0;
            _HPAcc.Hpa_cre_by = GlbUserName;
            _HPAcc.Hpa_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;

            _AccNo = new MasterAutoNumber();
            _AccNo.Aut_cate_cd = GlbUserDefProf;
            _AccNo.Aut_cate_tp = "PC";
            _AccNo.Aut_direction = 1;
            _AccNo.Aut_modify_dt = null;
            _AccNo.Aut_moduleid = "HS";
            _AccNo.Aut_number = 0;
            _AccNo.Aut_start_char = "ACC";
            _AccNo.Aut_year = null;

        }

        protected void CollectInvoiceHeader()
        {
            _invheader = new InvoiceHeader();

            _invheader.Sah_com = GlbUserComCode;
            _invheader.Sah_cre_by = GlbUserName;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = "LKR";
            _invheader.Sah_cus_add1 = txtPreAdd1.Text.Trim();
            _invheader.Sah_cus_add2 = txtPreAdd2.Text.Trim();
            _invheader.Sah_cus_cd = txtPreCode.Text.Trim();
            _invheader.Sah_cus_name = txtPreName.Text.Trim();
            _invheader.Sah_d_cust_add1 = txtPreAdd1.Text.Trim();
            _invheader.Sah_d_cust_add2 = txtPreAdd2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtPreCode.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = "HS";
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = "";
            _invheader.Sah_man_ref = "";
            _invheader.Sah_manual = false;
            _invheader.Sah_mod_by = GlbUserName;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = "na";
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = txtEx.Text.Trim();
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = 1;
            _invheader.Sah_session_id = GlbUserSessionID;
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "A";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_tax_inv = false;
            _invheader.Sah_anal_2 = "na";  // account no
            _invheader.Sah_acc_no = "na"; //account no newly added colomns

            _invNo = new MasterAutoNumber();
            _invNo.Aut_cate_cd = GlbUserDefProf;
            _invNo.Aut_cate_tp = "PC";
            _invNo.Aut_direction = 1;
            _invNo.Aut_modify_dt = null;
            _invNo.Aut_moduleid = "HS";
            _invNo.Aut_number = 0;
            _invNo.Aut_start_char = "HS";
            _invNo.Aut_year = null;

        }

        protected void btnClearCus_Click(object sender, EventArgs e)
        {
           
            List<HpCustomer> _hpcust = _HpAccCust;

            _hpcust.RemoveAll(x => x.Htc_cust_tp == "C");

            _HpCustomer = new List<MasterBusinessEntity>();
            gvCus.DataSource = _HpCustomer;
            gvCus.DataBind();

            _HpAccCust = _hpcust;

            txtProAdd1.Text = "";
            txtProAdd2.Text = "";

            txtPreCode.Text = "";
            txtPreName.Text = "";
            txtPreAdd1.Text = "";
            txtPreAdd2.Text = "";
        }

        protected void btnClearGur_Click(object sender, EventArgs e)
        {
            List<HpCustomer> _hpgur = _HpAccCust;
            
            _hpgur.RemoveAll(x => x.Htc_cust_tp == "G");
            _HpAccCust = _hpgur;

            _HpGuranter = new List<MasterBusinessEntity>();
            gvGur.DataSource = _HpGuranter;
            gvGur.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

            _cusCode = ((TextBox)cusCreP1.FindControl("txtCustCode")).Text;

            if (rbCus.Checked == false && rbGur.Checked == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select customer or gurantor option.");
                rbCus.Focus();
                return;
            }

            if (rbCus.Checked == true)
            {
                if (gvCus.Rows.Count > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is already created.");
                    return;
                }
            }

            if (rbGur.Checked == true)
            {
                foreach (MasterBusinessEntity _gur in _HpGuranter)
                {
                    if (_gur.Mbe_cd == _cusCode)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected gurantor is already exsist.");
                        return;
                    }
                }

                foreach (MasterBusinessEntity _cus in _HpCustomer)
                {
                    if (_cus.Mbe_cd == _cusCode)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected gurantor is already exsist as customer.");
                        return;
                    }
                }
            }



            //string _cusCode = "";
            //List<MasterBusinessEntity> _HpCustomer = new List<MasterBusinessEntity>();
            //List<MasterBusinessEntity> _HpGuranter = new List<MasterBusinessEntity>();


            string nicNo = ((TextBox)cusCreP1.FindControl("txtNicNo")).Text;
            string name = ((TextBox)cusCreP1.FindControl("txtFirstName")).Text;
            string DOB = ((TextBox)cusCreP1.FindControl("txtDateOfBirth")).Text;
            _cusCode = ((TextBox)cusCreP1.FindControl("txtCustCode")).Text;

            if (!string.IsNullOrEmpty(_cusCode))
            {
                checkCustomer(_cusCode);

                if (_isBlackList == true)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This customer is back listed." + "-" + _cusCode);
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _cusMsg);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(nicNo))
            {
                checkCustomer(nicNo);
                if (_isBlackList == true)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _cusMsg);
                    return;
                }
            }


            if (nicNo == "" || name == "" || DOB == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields");
                return;
            }
            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = cusCreP1.GetMainCustInfor();
            //----------------------------------------------------------
            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = cusCreP2.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();


            //----------------------------------------------------------
            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            //----------------------------------------------------------
            CustomerAccountRef _account = new CustomerAccountRef();
            //_account.Saca_acc_bal 
            _account.Saca_com_cd = GlbUserComCode;
            _account.Saca_cre_by = GlbUserName;
            _account.Saca_cre_when = DateTime.Now.Date;
            // _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
            _account.Saca_mod_by = GlbUserName;
            _account.Saca_mod_when = DateTime.Now.Date;
            _account.Saca_ord_bal = 0;
            _account.Saca_session_id = GlbUserSessionID;



            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();


            if (string.IsNullOrEmpty(_cusCode))
            {
                CustomerCreationUC CUST = new CustomerCreationUC();
                string custCD = CUST.SaveCustomer(finalCust, _account, bisInfoList);
                _cusCode = custCD;
            }

            // GetCustomerData(null, null);

            //clear controls
            ResetFields(cusCreP1.Controls);
            ResetFields(cusCreP2.Controls);
            cusCreP1 = new uc_CustomerCreation();
            cusCreP2 = new uc_CustCreationExternalDet();

            //if (rbCus.Checked == false && rbGur.Checked == false)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select customer or gurantor option.");
            //    rbCus.Focus();
            //    return;
            //}

            //if (rbCus.Checked == true)
            //{
            //    if (gvCus.Rows.Count > 0)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer already created.");
            //        rbCus.Focus();
            //        return;
            //    }
            //}

            //CollectBusinessEntity();

            //int effect = CHNLSVC.Sales.CreateCustomer(GlbUserDefProf, _businessEntity, out _cusCode);

            HpCustomer _tmpCust = new HpCustomer();
            if (rbCus.Checked == true)
            {
                finalCust.Mbe_cd = _cusCode;
                _HpCustomer.Add(finalCust);
                gvCus.DataSource = _HpCustomer;
                gvCus.DataBind();


                //foreach (MasterBusinessEntity _tempCust in _HpCustomer)
                //{
                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = finalCust.Mbe_cd;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 1;
                _tmpCust.Htc_adr_01 = finalCust.Mbe_add1;
                _tmpCust.Htc_adr_02 = finalCust.Mbe_add2;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);
                //}

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = custPart2.Mbe_cd;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 2;
                _tmpCust.Htc_adr_01 = custPart2.Mbe_add1;
                _tmpCust.Htc_adr_02 = custPart2.Mbe_add2;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);


                txtProAdd1.Text = finalCust.Mbe_add1;
                txtProAdd2.Text = finalCust.Mbe_add2;

                txtPreCode.Text = _cusCode;
                txtPreName.Text = finalCust.Mbe_name;
                txtPreAdd1.Text = finalCust.Mbe_add1;
                txtPreAdd2.Text = finalCust.Mbe_add2;
            }
            else
            {
                finalCust.Mbe_cd = _cusCode;
                _HpGuranter.Add(finalCust);
                gvGur.DataSource = _HpGuranter;
                gvGur.DataBind();
                //foreach (MasterBusinessEntity _tempGur in _HpGuranter)
                //{
                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = finalCust.Mbe_cd;
                _tmpCust.Htc_cust_tp = "G";
                _tmpCust.Htc_adr_tp = 1;
                _tmpCust.Htc_adr_01 = finalCust.Mbe_add1;
                _tmpCust.Htc_adr_02 = finalCust.Mbe_add2;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);
                // }

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = custPart2.Mbe_cd;
                _tmpCust.Htc_cust_tp = "G";
                _tmpCust.Htc_adr_tp = 2;
                _tmpCust.Htc_adr_01 = custPart2.Mbe_add1;
                _tmpCust.Htc_adr_02 = custPart2.Mbe_add2;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = GlbUserName;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);
            }

            // txtNIC.Text = "";
            // txtPass.Text = "";
            // txtcusName.Text = "";
            // txtAdd1.Text = "";

        }

        protected MasterBusinessEntity FinalMasterBusinessEntity(MasterBusinessEntity custPart1, MasterBusinessEntity custPart2)
        {
            MasterBusinessEntity customer = new MasterBusinessEntity();
            customer = custPart2;
            customer.Mbe_com = custPart1.Mbe_com;
            customer.Mbe_act = custPart1.Mbe_act;
            customer.Mbe_name = custPart1.Mbe_name;
            customer.Mbe_nic = custPart1.Mbe_nic;
            customer.Mbe_sub_tp = custPart1.Mbe_sub_tp;
            customer.Mbe_mob = custPart1.Mbe_mob;
            customer.Mbe_tp = custPart1.Mbe_tp;
            customer.Mbe_pp_no = custPart1.Mbe_pp_no;
            customer.Mbe_sex = custPart1.Mbe_sex;
            customer.Mbe_cate = custPart1.Mbe_cate;
            customer.Mbe_cre_dt = custPart1.Mbe_cre_dt;
            customer.Mbe_dl_no = custPart1.Mbe_dl_no;

            customer.Mbe_agre_send_sms = custPart1.Mbe_agre_send_sms;
            customer.Mbe_br_no = custPart1.Mbe_br_no;
            //customer.Mbe_ho_stus = ddlHO_status.SelectedValue;
            //customer.Mbe_pc_stus = ddlSH_status.SelectedValue;

            //--------------------------------------

            return customer;
        }

        //reset fields in container(page/uc)
        public static void ResetFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {

                    case "TextBox": var txtSource = (TextBox)contl; txtSource.Text = ""; break;
                    //case "ListBox": var lstSource = (ListBox)contl; lstSource.SelectedIndex = -1; lstSource.Enabled = true; break;
                    //case "ComboBox": var cmbSource = (ComboBox)contl; cmbSource.SelectedIndex = -1; cmbSource.Enabled = true; break;
                    //case "DataGridView": var dgvSource = (DataGridView)contl; dgvSource.Rows.Clear(); break;
                    //case "CheckBox": var chkSource = (CheckBox)contl; chkSource.Checked = false; chkSource.Enabled = true; break;
                } ResetFields(contl.Controls);
            }
        }

        //private void CollectBusinessEntity()
        //{
        //    _businessEntity = new  MasterBusinessEntity();
        //    _businessEntity.Mbe_act = true;
        //    _businessEntity.Mbe_add1 = "";
        //    _businessEntity.Mbe_add2 = "";
        //    _businessEntity.Mbe_cd = "c1";
        //    _businessEntity.Mbe_com = GlbUserComCode;
        //    _businessEntity.Mbe_contact = "";
        //    _businessEntity.Mbe_email = "";
        //    _businessEntity.Mbe_fax = "";
        //    _businessEntity.Mbe_is_tax = false;
        //    _businessEntity.Mbe_mob = "";
        //    _businessEntity.Mbe_name = "";
        //    _businessEntity.Mbe_nic = "";
        //    _businessEntity.Mbe_tax_no = "";
        //    _businessEntity.Mbe_tel = "";
        //    _businessEntity.Mbe_tp = "C";
        //}



        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdentiry.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter identification.");
                txtIdentiry.Focus();
                return;
            }

            checkCustomer(txtIdentiry.Text.Trim());

            if (_isBlackList == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _cusMsg);
                return;
            }
            else if (_isExsistCus == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _cusMsg);
                return;
            }
            else if (_isExsistCus == false && _isBlackList == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _cusMsg);
                return;
            }
        }

        private void checkCustomer(string _identification)
        {
            _masterBusinessCompany = new MasterBusinessEntity();
            _cusCode = "";
            //lblCusinfo.Text = "";
            _isBlackList = false;
            _isExsistCus = false;
            _cusMsg = "";

            if (!string.IsNullOrEmpty(_identification))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, _identification.Trim(), string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_cd != null)
                {

                    _cusCode = _masterBusinessCompany.Mbe_cd;
                    _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                    if (_blackListCustomers.Hbl_cust_cd != null)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Black listed customer." + _blackListCustomers.Hbl_cust_cd + " - " + _blackListCustomers.Hbl_rmk);
                        _cusMsg = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                        _isBlackList = true;
                        _isExsistCus = false;
                        return;
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exsisting customer.");
                        _cusMsg = "Exsisting customer.";
                        _isBlackList = false;
                        _isExsistCus = true;
                        return;
                    }
                }
                else
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, _identification.Trim(), string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        _cusCode = _masterBusinessCompany.Mbe_cd;
                        _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                        if (_blackListCustomers.Hbl_cust_cd != null)
                        {
                            _cusMsg = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Black listed customer." + _blackListCustomers.Hbl_cust_cd + " - " + _blackListCustomers.Hbl_rmk);
                            _isBlackList = true;
                            _isExsistCus = false;
                            return;
                        }
                        else
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exsisting customer.");
                            _cusMsg = "Exsisting customer.";
                            _isBlackList = false;
                            _isExsistCus = true;
                            return;
                        }
                    }

                    else
                    {
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, string.Empty, _identification.Trim(), "C");

                        if (_masterBusinessCompany.Mbe_cd != null)
                        {
                            _cusCode = _masterBusinessCompany.Mbe_cd;
                            _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                            if (_blackListCustomers.Hbl_cust_cd != null)
                            {
                                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Black listed customer." + _blackListCustomers.Hbl_cust_cd + " - " + _blackListCustomers.Hbl_rmk);
                                _cusMsg = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                _isBlackList = true;
                                _isExsistCus = false;
                                return;
                            }
                            else
                            {
                                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exsisting customer.");
                                _cusMsg = "Exsisting customer.";
                                _isBlackList = false;
                                _isExsistCus = true;
                                return;
                            }
                        }
                        else
                        {
                            _cusMsg = "Cannot find exsisting customer details for given identification.";
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find exsisting customer details for given identification.");
                            _isBlackList = false;
                            _isExsistCus = false;
                            return;
                        }
                    }

                }
            }

        }

        protected void CheckValidQty(object sender, EventArgs e)
        {
            int num;
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                bool isNumeric = int.TryParse(txtQty.Text, out num);
                if (isNumeric)
                {

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Qty should be numeric.");
                    txtQty.Text = "";
                    txtQty.Focus();
                    return;
                }
                btnAddItem.Focus();
            }
        }

        protected void CheckValidItem(object sender, EventArgs e)
        {
            _masterItemDetails = new MasterItem();

            if (_maxAllowQty <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Maxsimum qty parameter not define.");
                return;
            }

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                GetItemDetails(txtItem.Text.Trim());
                //_masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, txtItem.Text, 1);

                //if (_masterItemDetails.Mi_cd != null && _masterItemDetails.Mi_hp_allow == true)
                //{
                //    txtDesc.Text = _masterItemDetails.Mi_longdesc;
                //    txtModel.Text = _masterItemDetails.Mi_model;
                //}
                //else
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Not allow to company or invalid item or not allow to HP.");
                //    txtItem.Text = "";
                //    txtItem.Focus();
                //    return;
                //}
            }


        }

        protected void GetItemDetails(string _itm)
        {
            _masterItemDetails = new MasterItem();

            if (!string.IsNullOrEmpty(_itm))
            {
                _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, _itm, 1);

                if (_masterItemDetails.Mi_cd != null)
                {
                    if (_masterItemDetails.Mi_hp_allow == true)
                    {
                        txtDesc.Text = _masterItemDetails.Mi_longdesc;
                        txtModel.Text = _masterItemDetails.Mi_model;
                        txtBrand.Text = _masterItemDetails.Mi_brand;
                        txtQty.Text = "1";
                        btnAddItem.Focus();
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item is not allow to HP.");
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item is invalid.");
                    txtItem.Text = "";
                    txtItem.Focus();
                    return;
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserSessionID, string.Empty);
            CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);   
            Response.Redirect("~/Default.aspx", false);

            //string Msg = "";
            //GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
            //clearReportParameters();
            //GlbDocNosList = "AAZKI-000001";
            //    GlbReportName = "HP Agreement";
            //    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HP_Agreement.rpt";
            //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/HP_Agreement.rpt";

            //    Msg = "<script>window.open('../../Reports_Module/Sales_Rep/HPAgreementPrint.aspx','_blank');</script>";

            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            //string Msg = "";
            //GlbDocNosList = "SGBAN-HS00001";
            //GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
            //if (GlbUserComCode == "SGL")
            //{
            //    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrintPre.rpt";
            //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrintPre.rpt";
            //}
            //else
            //{
            //    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrint.rpt";
            //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";
            //}
            ////Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
            //Msg = "<script>window.open('../../Reports_Module/Sales_Rep/InvoiceEntryPrint.aspx','_blank')</script>";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        }



        protected void btnClear_Click(object sender, EventArgs e)
        {



            this.Clear_Data();
            _selectItemList = new List<MasterItem>();
            _priceDetails = new List<PriceDetailRef>();
            _promoPriceDetails = new List<PriceDetailRef>();
            _priceBooks = new PriceBookRef();
            _masterItemDetails = new MasterItem();
            _tempPriceBook = new List<TempCommonPrice>();
            _tempItemWithPrices = new List<TempCommonPrice>();
            _AccountItems = new List<InvoiceItem>();
            _SchemeDefinition = new List<HpSchemeDefinition>();
            _SchemeDefinitionComm = new List<HpSchemeDefinition>();
            _SchemeDetails = new HpSchemeDetails();
            _ServiceCharges = new List<HpServiceCharges>();
            _SchemeOtherCharges = new List<HpOtherCharges>();
            _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
            _SchemeSheduleDef = new HpSchemeSheduleDefinition();
            _sheduleDetails = new List<HpSheduleDetails>();
            _businessEntity = new MasterBusinessEntity();
            _SystemPara = new HpSystemParameters();
            _selectBusinessentity = new List<MasterBusinessCompany>();
            _mastercompanyItem = new MasterCompanyItem();
            _invheader = new InvoiceHeader();
            _invNo = new MasterAutoNumber();
            _HPAcc = new HpAccount();
            _AccNo = new MasterAutoNumber();
            _HPAccLog = new HPAccountLog();
            Receipt_List = new List<RecieptHeader>();
            _recieptItem = new List<RecieptItem>();
            PaymentTypes = new List<PaymentType>();
            _HpCustomer = new List<MasterBusinessEntity>();
            _HpGuranter = new List<MasterBusinessEntity>();
            _SchemeType = new HpSchemeType();
            _MainTrans = new List<HpTransaction>();
            _MainTxnAuto = new MasterAutoNumber();
            _HpAccCust = new List<HpCustomer>();
            _masterItemComponent = new List<MasterItemComponent>();
            _combineItems = new List<PriceCombinedItemRef>();

            CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserSessionID, string.Empty);
            BindPaymentType(ddlPayMode);
            _cusCode = "";
        }

        private MasterItem AssignDataToObject()
        {
            MasterItem _tempItem = new MasterItem();
            _tempItem.Mi_cd = txtItem.Text.Trim();
            _tempItem.Mi_longdesc = txtDesc.Text.Trim();
            _tempItem.Mi_model = txtModel.Text.Trim();
            _tempItem.Mi_brand = txtBrand.Text.Trim();
            _tempItem.Mi_dim_length = Convert.ToDecimal(txtQty.Text);
            return _tempItem;
        }

        protected void BindAddItem()
        {
            gvHPItem.DataSource = _selectItemList;
            gvHPItem.DataBind();
        }



        protected void btnCal_Click(object sender, EventArgs e)
        {
            decimal _Bal = 0;
            decimal _tmpTotPay = 0;
            decimal _tmpTotalPay = 0;

            if (string.IsNullOrEmpty(txtCusPay.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter customer payment amount.");
                txtCusPay.Focus();
                return;
            }
            if (Convert.ToDecimal(lblAddRent.Text) > 0)
            {
                if (Convert.ToDecimal(txtCusPay.Text) > _ExTotAmt)
                {
                    _ExTotAmt = Convert.ToDecimal(txtCusPay.Text);
                    _tmpTotalPay = Convert.ToDecimal(lblTotAmt.Text);
                    while (Convert.ToDecimal(lblTotAmt.Text) < Convert.ToDecimal(txtCusPay.Text))
                    {
                        _tmpTotPay = Convert.ToDecimal(lblTotAmt.Text);
                        _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                        lblDPay.Text = Convert.ToDecimal((lblDPay.Text) + _Bal).ToString("0.00");
                    }
                    lblDPay.Text = (Convert.ToDecimal(lblDPay.Text) - (Convert.ToDecimal(lblAddRent.Text) - Convert.ToDecimal(lblAddRent.Text))).ToString("0.00");
                    lblTotAmt.Text = (Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(lblstampduty.Text) + Convert.ToDecimal(lblAddRent.Text) + Convert.ToDecimal(lblInsu.Text)).ToString("0.00");


                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer payment must be higher than the existing Amount.");
                    txtCusPay.Focus();
                    return;
                }
            }
            else
            {

                if (Convert.ToDecimal(lblTotAmt.Text) > Convert.ToDecimal(txtCusPay.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer payment must be higher than the existing initial payment.");
                    txtCusPay.Focus();
                    return;
                }

                while (Convert.ToDecimal(lblTotAmt.Text) < Convert.ToDecimal(txtCusPay.Text))
                {
                    _tmpTotPay = Convert.ToDecimal(lblTotAmt.Text);
                    _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                    // lblDPay.Text = (Convert.ToDecimal(lblDPay.Text) + _Bal).ToString("0.00");
                    GetDiscountAndCommission();
                    GetServiceCharges();
                    CalHireAmount();
                    CalCommissionAmount();
                    GetOtherCharges();
                    GetInsuarance();
                    CalTotalCash();
                    CalInstallmentBaseAmt();
                    TotalCash();
                    GetInsAndReg();
                    Show_Shedule();
                    lblPayBalance.Text = lblTotAmt.Text;
                }
            }

            //GetDiscountAndCommission();
            //GetServiceCharges();
            //CalHireAmount();
            //CalCommissionAmount();
            //GetOtherCharges();
            //GetInsuarance();
            //CalTotalCash();
            //CalInstallmentBaseAmt();
            //TotalCash();

            //Show_Shedule();

        }

        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            string _pBook = "";
            string _pLevel = "";
            double _itmVAT = 0;
            decimal _unitPrice = 0;
            Int32 _pType = 1000;
            Boolean _foundItem = false;
            decimal _qty = 0;
            Boolean _combinebyInv = false;
            Boolean _getPrice = false;
            Boolean _isVAT = false;


            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter item.");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter qty.");
                txtQty.Focus();
                return;
            }

            var currrange = (from cur in _selectItemList
                             where cur.Mi_cd == txtItem.Text.Trim()
                             select cur).ToList();

            if (currrange.Count > 0)// ||currrange !=null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item already exsist.");
                return;
            }

            //check allow qty
            // Check valid Item qtys
            //List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            //if (_hir != null)
            //{
            //    if (_hir.Count > 0)
            //    {
            //        foreach (MasterSalesPriorityHierarchy _one in _hir)
            //        {
            //            string _type = _one.Mpi_cd;
            //            string _value = _one.Mpi_val;
            //            _foundItem = false;
            //            _SystemPara = new HpSystemParameters();
            //            _SystemPara = CHNLSVC.Sales.GetSystemParameter(_type, _value, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

            //            if (_SystemPara.Hsy_cd != null)
            //            {
            //                _foundItem = true;
            if (gvHPItem.Rows.Count > 0)
            {
                for (int x = 0; x < gvHPItem.Rows.Count; x++)
                {

                    _mastercompanyItem = new MasterCompanyItem();
                    _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(GlbUserComCode, txtItem.Text.Trim(), 1);

                    if (_mastercompanyItem.Mci_hpqty_chk == true)
                    {
                        _qty = _qty + Convert.ToDecimal(gvHPItem.Rows[x].Cells[4].Text);
                    }
                }
                _qty = _qty + Convert.ToDecimal(txtQty.Text);
            }
            else
            {
                _mastercompanyItem = new MasterCompanyItem();
                _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(GlbUserComCode, txtItem.Text.Trim(), 1);

                if (_mastercompanyItem.Mci_hpqty_chk == true)
                {
                    _qty = _qty + Convert.ToDecimal(txtQty.Text);
                }
            }

            if (_maxAllowQty < _qty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Maximum Qty exceed.");
                return;
            }
            //                    goto L10;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Hirarchy not defined.");
            //            return;
            //        }
            //    }

            //    else
            //    {

            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Hirarchy not defined.");
            //        return;
            //    }
            //L10:
            //    //check if record found
            //    if (_foundItem == false)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Qty parameter not found.");
            //        return;
            //    }

            _combinebyInv = false;
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text);
            if (_masterItemComponent != null)
            {
                _combinebyInv = true;
            }

            List<TempCommonPrice> _commonprice = new List<TempCommonPrice>();
            List<PriceDefinitionRef> _paramPBForPC = new List<PriceDefinitionRef>();
            TempCommonPrice _tempPrice = new TempCommonPrice();

            _priceBooks = new PriceBookRef();

            _paramPBForPC = CHNLSVC.Sales.GetPriceBooksForPC(GlbUserComCode, GlbUserDefProf, "HS");

            if (_paramPBForPC == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find related price books.");
                txtItem.Focus();
                return;
            }

            if (_combinebyInv == false)
            {

                CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserSessionID, txtItem.Text.Trim());

                //CHECK PROMOTIONS
                foreach (PriceDefinitionRef promo in _paramPBForPC)
                {
                    List<PriceDetailRef> _promoPrice = new List<PriceDetailRef>();
                    _promoPrice = CHNLSVC.Sales.GetCombinePrice(GlbUserComCode, GlbUserDefProf, string.Empty, promo.Sadd_pb, promo.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), DateTime.Today);

                    if (_promoPrice.Count > 0)
                    {

                        grvPopUpCombines.DataSource = _promoPrice;
                        grvPopUpCombines.DataBind();
                        Panel_popUp.Visible = true;
                        ModalPopupExtItem.Show();
                        return;
                    }
                }


                foreach (PriceDefinitionRef book in _paramPBForPC)
                {
                    List<PriceDetailRef> _paramPrice = new List<PriceDetailRef>();
                    _paramPrice = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, string.Empty, book.Sadd_pb, book.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), DateTime.Today);

                    if (_paramPrice.Count > 0)
                    {

                        foreach (PriceDetailRef price in _paramPrice)
                        {
                            if (price.Sapd_price_stus == "A")
                            {

                                if (price.Sapd_price_type == 0)
                                {

                                    if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd || _pType != price.Sapd_price_type)
                                    {


                                        PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                        _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);

                                        if (_lvlDef.Sapl_vat_calc == true)
                                        {
                                            _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                            _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                        }
                                        else
                                        {
                                            _itmVAT = 0;
                                        }
                                        _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                        price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                        _priceDetails.Add(price);

                                    }
                                }
                                else
                                {
                                    //PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                    //_lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);

                                    //if (_lvlDef.Sapl_vat_calc == true)
                                    //{
                                    //    _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                    //    _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                    //}
                                    //else
                                    //{
                                    //    _itmVAT = 0;
                                    //}
                                    ////_itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                    ////_itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                    //_unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                    //price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                    //_promoPriceDetails.Add(price);

                                }
                                _tempPrice.Tcp_pb_cd = price.Sapd_pb_tp_cd;
                                _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, price.Sapd_pb_tp_cd);
                                _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                _tempPrice.Tcp_pb_lvl = price.Sapd_pbk_lvl_cd;
                                _tempPrice.Tcp_itm = txtItem.Text.Trim();
                                _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                                _tempPrice.Tcp_ip = GlbUserSessionID;
                                _tempPrice.Tcp_usr = GlbUserName;
                                _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                                _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                                _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
                                CHNLSVC.Sales.SaveTempPrice(_tempPrice);


                            }

                            _pBook = price.Sapd_pb_tp_cd;
                            _pLevel = price.Sapd_pbk_lvl_cd;
                            _pType = price.Sapd_price_type;
                        }
                    }


                    gvNormalPrice.DataSource = _priceDetails;
                    gvNormalPrice.DataBind();

                    gvPromoPrice.DataSource = _promoPriceDetails;
                    gvPromoPrice.DataBind();


                }
                _selectItemList.Add(AssignDataToObject());
                BindAddItem();

            }
            else
            {

                foreach (MasterItemComponent itemCom in _masterItemComponent)
                {

                    txtItem.Text = itemCom.ComponentItem.Mi_cd;
                    GetItemDetails(txtItem.Text.Trim());
                    txtQty.Text = itemCom.Micp_qty.ToString();
                    _getPrice = false;

                    CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserSessionID, txtItem.Text.Trim());

                    foreach (PriceDefinitionRef book in _paramPBForPC)
                    {
                        List<PriceDetailRef> _paramPrice = new List<PriceDetailRef>();
                        _paramPrice = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, string.Empty, book.Sadd_pb, book.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), DateTime.Today);
                        _pBook = book.Sadd_pb;
                        _pLevel = book.Sadd_p_lvl;
                        _getPrice = false;
                        _pType = 1001;
                        if (_paramPrice.Count > 0)
                        {
                            _getPrice = true;
                            foreach (PriceDetailRef price in _paramPrice)
                            {
                                if (price.Sapd_price_stus == "A")
                                {
                                    //if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd)
                                    if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd || _pType != price.Sapd_price_type)
                                    {
                                        if (price.Sapd_price_type == 0)
                                        {
                                            // _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                            // _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                            PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                            _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);

                                            if (_lvlDef.Sapl_vat_calc == true)
                                            {
                                                _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                                _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                            }
                                            else
                                            {
                                                _itmVAT = 0;
                                            }
                                            _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                            price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                            _priceDetails.Add(price);

                                        }
                                        else
                                        {
                                            //_itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                            //_itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                            PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                            _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);

                                            if (_lvlDef.Sapl_vat_calc == true)
                                            {
                                                _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                                _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                            }
                                            else
                                            {
                                                _itmVAT = 0;
                                            }
                                            _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                            price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                            _promoPriceDetails.Add(price);

                                        }
                                        _tempPrice.Tcp_pb_cd = price.Sapd_pb_tp_cd;
                                        _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, price.Sapd_pb_tp_cd);
                                        _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                        _tempPrice.Tcp_pb_lvl = price.Sapd_pbk_lvl_cd;
                                        _tempPrice.Tcp_itm = txtItem.Text.Trim();
                                        _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                                        _tempPrice.Tcp_ip = GlbUserSessionID;
                                        _tempPrice.Tcp_usr = GlbUserName;
                                        _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                                        _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                                        _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
                                        CHNLSVC.Sales.SaveTempPrice(_tempPrice);

                                    }
                                }

                                _pBook = price.Sapd_pb_tp_cd;
                                _pLevel = price.Sapd_pbk_lvl_cd;
                                _pType = price.Sapd_price_type;
                            }
                        }
                        else if (itemCom.Micp_itm_tp == "C")
                        {
                            _tempPrice.Tcp_pb_cd = _pBook;
                            _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, _pBook);
                            _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                            _tempPrice.Tcp_pb_lvl = _pLevel;
                            _tempPrice.Tcp_itm = txtItem.Text.Trim();
                            _tempPrice.Tcp_price = 0;
                            _tempPrice.Tcp_ip = GlbUserSessionID;
                            _tempPrice.Tcp_usr = GlbUserName;
                            _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                            _tempPrice.tcp_total = 0;
                            _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
                            CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                        }


                        gvNormalPrice.DataSource = _priceDetails;
                        gvNormalPrice.DataBind();

                        gvPromoPrice.DataSource = _promoPriceDetails;
                        gvPromoPrice.DataBind();


                    }

                    //if (_getPrice == false)
                    //{
                    //    _tempPrice.Tcp_pb_cd = _pBook;
                    //    _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, _pBook);
                    //    _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                    //    _tempPrice.Tcp_pb_lvl = _pLevel;
                    //    _tempPrice.Tcp_itm = txtItem.Text.Trim();
                    //    _tempPrice.Tcp_price = 0;
                    //    _tempPrice.Tcp_ip = GlbUserSessionID;
                    //    _tempPrice.Tcp_usr = GlbUserName;
                    //    _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                    //    _tempPrice.tcp_total = 0;
                    //    _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
                    //    CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                    //}

                    _selectItemList.Add(AssignDataToObject());
                    BindAddItem();
                }
            }

            if (gvNormalPrice.Rows.Count > 0 || gvPromoPrice.Rows.Count > 0)
            {
                //_selectItemList.Add(AssignDataToObject());
                //BindAddItem();

                if (_combinebyInv == false)
                {
                    _commonprice = CHNLSVC.Sales.GetCommonPriceBook(GlbUserName, GlbUserSessionID, gvHPItem.Rows.Count);
                    _tempPriceBook = _commonprice;
                    gvBooks.DataSource = _tempPriceBook;
                    gvBooks.DataBind();
                }
                else
                {
                    _commonprice = CHNLSVC.Sales.GetCommonPriceBook(GlbUserName, GlbUserSessionID, gvHPItem.Rows.Count);
                    _tempPriceBook = _commonprice;
                    gvBooks.DataSource = _tempPriceBook;
                    gvBooks.DataBind();
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find valid price.");
                txtItem.Focus();
                return;
            }

            txtItem.Text = "";
            txtDesc.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtQty.Text = "";
            _pBook = "";
            _pLevel = "";
            _pType = 0;
            txtItem.Focus();
        }

        protected void OnRemoveFromHPItem(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_selectItemList != null)
                if (_selectItemList.Count > 0)
                {

                    List<MasterItem> _tempList = _selectItemList;
                    _tempList.RemoveAt(row_id);
                    _selectItemList = _tempList;
                    gvHPItem.DataSource = _selectItemList;
                    gvHPItem.DataBind();

                }

        }


        //protected void gvItemWithPrice_Rowcommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //_AccountItems
        //    string _item = "";
        //    decimal _qty = 0;
        //    decimal _uprice = 0;
        //    double _taxRate = 0;
        //    decimal _amount = 0;
        //    string _pb = "";
        //    string _lvl = "";
        //    decimal _totAmt = 0;
        //    int _row_id = 0;


        //    switch (e.CommandName.ToUpper())
        //    {
        //        case "GET":
        //            {
        //                InvoiceItem _tempHPItems = new InvoiceItem();

        //                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        //                CheckBox check = (CheckBox)row.FindControl("chkGet");
        //                if (check.Checked == true)
        //                {
        //                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This item already selected.");
        //                    return;
        //                }
        //                check.Checked = true;

        //                _row_id = row.RowIndex;
        //                _item = row.Cells[1].Text.ToString();
        //                _qty = Convert.ToDecimal(row.Cells[3].Text);
        //                _uprice = Math.Round(Convert.ToDecimal(row.Cells[4].Text), 0);
        //                _amount = Math.Round(_qty * _uprice, 0);
        //                _pb = (string)gvItemWithPrice.DataKeys[_row_id][0];
        //                _lvl = (string)gvItemWithPrice.DataKeys[_row_id][1];
        //                _taxRate = Convert.ToDouble(TaxCalculation(GlbUserComCode, _item, "GOD", _amount, 0));
        //                _taxRate = Math.Round(_taxRate + 0.49);
        //                _totAmt = Math.Round(_amount + Convert.ToDecimal(_taxRate), 0);
        //                _NetAmt = Math.Round(_NetAmt + _totAmt, 0);
        //                _TotVat = Math.Round(_TotVat + Convert.ToDecimal(_taxRate), 0);

        //                _tempHPItems.Sad_alt_itm_cd = _item;
        //                _tempHPItems.Sad_alt_itm_desc = "";
        //                _tempHPItems.Sad_comm_amt = 0;
        //                _tempHPItems.Sad_disc_amt = 0;
        //                _tempHPItems.Sad_disc_rt = 0;
        //                _tempHPItems.Sad_do_qty = _qty;
        //                _tempHPItems.Sad_fws_ignore_qty = 0;
        //                _tempHPItems.Sad_inv_no = "";
        //                _tempHPItems.Sad_is_promo = false;
        //                _tempHPItems.Sad_itm_cd = _item;
        //                _tempHPItems.Sad_itm_line = _row_id + 1;
        //                _tempHPItems.Sad_itm_seq = 0;
        //                _tempHPItems.Sad_itm_stus = "GOD";
        //                _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(_taxRate);
        //                _tempHPItems.Sad_itm_tp = "";
        //                _tempHPItems.Sad_job_line = 0;
        //                _tempHPItems.Sad_job_no = "";
        //                _tempHPItems.Sad_merge_itm = "";
        //                _tempHPItems.Sad_outlet_dept = "";
        //                _tempHPItems.Sad_pb_lvl = _lvl;
        //                _tempHPItems.Sad_pb_price = _uprice;
        //                _tempHPItems.Sad_pbook = _pb;
        //                _tempHPItems.Sad_print_stus = true;
        //                _tempHPItems.Sad_promo_cd = "";
        //                _tempHPItems.Sad_qty = _qty;
        //                _tempHPItems.Sad_res_line_no = 0;
        //                _tempHPItems.Sad_res_no = "";
        //                _tempHPItems.Sad_seq = 0;
        //                _tempHPItems.Sad_seq_no = 0;
        //                _tempHPItems.Sad_srn_qty = 0;
        //                _tempHPItems.Sad_tot_amt = _totAmt;
        //                _tempHPItems.Sad_unit_amt = _amount;
        //                _tempHPItems.Sad_unit_rt = _uprice;
        //                _tempHPItems.Sad_uom = "";
        //                _tempHPItems.Sad_warr_based = false;
        //                _tempHPItems.Sad_warr_period = 0;
        //                _tempHPItems.Sad_warr_remarks = "";
        //                _tempHPItems.Sad_isapp = false;
        //                _tempHPItems.Sad_iscovernote = false;

        //                _AccountItems.Add(_tempHPItems);
        //                gvAllItems.DataSource = _AccountItems;
        //                gvAllItems.DataBind();

        //                lblTotVat.Text = _TotVat.ToString("0.00");
        //                lblTot.Text = _NetAmt.ToString("0.00");
        //                break;
        //            }
        //    }
        //}

        private decimal TaxCalculation(string _com, string _item, string _status, decimal _UnitPrice, decimal _TaxVal)
        {

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            var _Tax = from _itm in _taxs
                       select _itm;
            foreach (MasterItemTax _one in _Tax)
            {
                _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
            }


            return _TaxVal;
        }


        protected void grvPopUpCombines_Rowcommand(object sender, GridViewCommandEventArgs e)
        {

            string _mainItem = "";
            Int32 _pbSeq = 0;
            Int32 _itmSeq = 0;

            switch (e.CommandName.ToUpper())
            {
                case "SELECT":
                    {
                        for (int i = 0; i < grvPopUpCombines.Rows.Count; i++)
                        {
                            CheckBox chk = (CheckBox)grvPopUpCombines.Rows[i].FindControl("chkPromoSelect");

                            if (chk.Checked == true)
                            {
                                chk.Checked = false;
                            }
                        }


                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        CheckBox check = (CheckBox)row.FindControl("chkPromoSelect");
                        check.Checked = true;

                        _mainItem = row.Cells[3].Text.ToString();
                        _pbSeq = Convert.ToInt32(row.Cells[6].Text);
                        _itmSeq = Convert.ToInt32(row.Cells[7].Text);

                        _combineItems = new List<PriceCombinedItemRef>();
                        _combineItems = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbSeq, _itmSeq, _mainItem, string.Empty);
                        gvfreeItem.DataSource = _combineItems;
                        gvfreeItem.DataBind();

                        Panel_popUp.Visible = true;
                        ModalPopupExtItem.Show();
                        break;
                    }
            }


        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            string _pBook = "";
            string _pLevel = "";
            double _itmVAT = 0;
            decimal _unitPrice = 0;
            Int32 _pType = 1000;
            decimal _qty = 0;
            Boolean _combinebyInv = false;
            Boolean _isVAT = false;



            List<TempCommonPrice> _commonprice = new List<TempCommonPrice>();
            List<PriceDefinitionRef> _paramPBForPC = new List<PriceDefinitionRef>();
            TempCommonPrice _tempPrice = new TempCommonPrice();

            _priceBooks = new PriceBookRef();

            _paramPBForPC = CHNLSVC.Sales.GetPriceBooksForPC(GlbUserComCode, GlbUserDefProf, "HS");

            if (_paramPBForPC == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find related price books.");
                txtItem.Focus();
                return;
            }


            CHNLSVC.Sales.DeleteTempPrice(GlbUserName, GlbUserSessionID, txtItem.Text.Trim());


            foreach (PriceDefinitionRef book in _paramPBForPC)
            {
                List<PriceDetailRef> _paramPrice = new List<PriceDetailRef>();
                _paramPrice = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, string.Empty, book.Sadd_pb, book.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), DateTime.Today);

                if (_paramPrice.Count > 0)
                {

                    foreach (PriceDetailRef price in _paramPrice)
                    {
                        if (price.Sapd_price_stus == "A")
                        {

                            if (price.Sapd_price_type == 0)
                            {

                                if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd || _pType != price.Sapd_price_type)
                                {


                                    PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                    _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);

                                    if (_lvlDef.Sapl_vat_calc == true)
                                    {
                                        _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
                                        _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                    }
                                    else
                                    {
                                        _itmVAT = 0;
                                    }
                                    _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                    price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                    _priceDetails.Add(price);

                                }

                                _tempPrice.Tcp_pb_cd = price.Sapd_pb_tp_cd;
                                _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, price.Sapd_pb_tp_cd);
                                _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                _tempPrice.Tcp_pb_lvl = price.Sapd_pbk_lvl_cd;
                                _tempPrice.Tcp_itm = txtItem.Text.Trim();
                                _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                                _tempPrice.Tcp_ip = GlbUserSessionID;
                                _tempPrice.Tcp_usr = GlbUserName;
                                _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                                _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                                _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
                                CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                            }



                        }

                        _pBook = price.Sapd_pb_tp_cd;
                        _pLevel = price.Sapd_pbk_lvl_cd;
                        _pType = price.Sapd_price_type;
                    }
                }

            }
            gvNormalPrice.DataSource = _priceDetails;
            gvNormalPrice.DataBind();

            gvPromoPrice.DataSource = _promoPriceDetails;
            gvPromoPrice.DataBind();


            if (gvNormalPrice.Rows.Count > 0 || gvPromoPrice.Rows.Count > 0)
            {
                //_selectItemList.Add(AssignDataToObject());
                //BindAddItem();

                if (_combinebyInv == false)
                {
                    _commonprice = CHNLSVC.Sales.GetCommonPriceBook(GlbUserName, GlbUserSessionID, gvHPItem.Rows.Count);
                    _tempPriceBook = _commonprice;
                    gvBooks.DataSource = _tempPriceBook;
                    gvBooks.DataBind();
                }
                else
                {
                    _commonprice = CHNLSVC.Sales.GetCommonPriceBook(GlbUserName, GlbUserSessionID, gvHPItem.Rows.Count);
                    _tempPriceBook = _commonprice;
                    gvBooks.DataSource = _tempPriceBook;
                    gvBooks.DataBind();
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find valid price.");
                txtItem.Focus();
                return;
            }

            _selectItemList.Add(AssignDataToObject());
            BindAddItem();

            txtItem.Text = "";
            txtDesc.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtQty.Text = "";
            _pBook = "";
            _pLevel = "";
            _pType = 0;
            txtItem.Focus();


        }

        protected void btnPopupConfirm_Click(object sender, EventArgs e)
        {
            string _pb = "";
            string _lvl = "";
            string _mainItem = "";
            decimal _unitPrice = 0;
            double _itmVAT = 0;

            List<TempCommonPrice> _commonprice = new List<TempCommonPrice>();
            _promoPriceDetails = new List<PriceDetailRef>();

            if (_combineItems == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select request combine.");
                Panel_popUp.Visible = true;
                ModalPopupExtItem.Show();
                return;
            }

            for (int i = 0; i < grvPopUpCombines.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvPopUpCombines.Rows[i].FindControl("chkPromoSelect");

                if (chk.Checked == true)
                {
                    _mainItem = grvPopUpCombines.Rows[i].Cells[3].Text;
                    _pb = grvPopUpCombines.Rows[i].Cells[1].Text;
                    _lvl = grvPopUpCombines.Rows[i].Cells[2].Text;
                    _unitPrice = Convert.ToDecimal(grvPopUpCombines.DataKeys[i][3]);
                    //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                    //_invheader.Sah_com = gvHInv.DataKeys[i][0].ToString();
                    //_invheader.Sah_cre_by = GlbUserName;
                    //_invheader.Sah_cre_when = DateTime.Now;
                    //_invheader.Sah_currency = gvHInv.Rows[i].Cells[17].Text;
                    //_invheader.Sah_cus_add1 = gvHInv.DataKeys[i][6].ToString();
                    goto L1;
                }
            }

        L1:
            TempCommonPrice _tempPrice = new TempCommonPrice();

            _selectItemList.Add(AssignDataToObject());
            BindAddItem();

            _tempPrice.Tcp_pb_cd = _pb;
            _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, _pb);
            _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
            _tempPrice.Tcp_pb_lvl = _lvl;
            _tempPrice.Tcp_itm = _mainItem;
            _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
            _tempPrice.Tcp_ip = GlbUserSessionID;
            _tempPrice.Tcp_usr = GlbUserName;
            _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
            _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
            _tempPrice.tcp_itm_desc = txtDesc.Text.Trim();
            CHNLSVC.Sales.SaveTempPrice(_tempPrice);

            foreach (PriceCombinedItemRef _tempcombine in _combineItems)
            {
                _tempPrice.Tcp_pb_cd = _pb;
                _priceBooks = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, _pb);
                _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                _tempPrice.Tcp_pb_lvl = _lvl;
                _tempPrice.Tcp_itm = _tempcombine.Sapc_itm_cd;
                _tempPrice.Tcp_price = Math.Round(_tempcombine.Sapc_price, 0);
                _tempPrice.Tcp_ip = GlbUserSessionID;
                _tempPrice.Tcp_usr = GlbUserName;
                _tempPrice.tcp_qty = Convert.ToDecimal(_tempcombine.Sapc_qty);
                _tempPrice.tcp_total = Math.Round(_tempcombine.Sapc_price * Convert.ToDecimal(_tempcombine.Sapc_qty), 0);
                _tempPrice.tcp_itm_desc = "";
                CHNLSVC.Sales.SaveTempPrice(_tempPrice);
            }


            PriceDetailRef _tempPromo = new PriceDetailRef();
            PriceBookLevelRef _lvlDef = new PriceBookLevelRef();

            _tempPromo.Sapd_itm_cd = _mainItem;
            _tempPromo.Sapd_pb_tp_cd = _pb;
            _tempPromo.Sapd_pbk_lvl_cd = _lvl;

            _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, _pb, _lvl);

            if (_lvlDef.Sapl_vat_calc == true)
            {
                _itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", _unitPrice, 0));
                _itmVAT = Math.Round(_itmVAT + 0.49, 0);
            }
            else
            {
                _itmVAT = 0;
            }
            //_itmVAT = Convert.ToDouble(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), "GOD", price.Sapd_itm_price, 0));
            //_itmVAT = Math.Round(_itmVAT + 0.49, 0);
            _unitPrice = Math.Round(_unitPrice, 0);
            _tempPromo.Sapd_itm_price = Math.Round(_unitPrice + Convert.ToDecimal(_itmVAT), 0);
            _promoPriceDetails.Add(_tempPromo);

            gvPromoPrice.DataSource = _promoPriceDetails;
            gvPromoPrice.DataBind();

            txtItem.Text = "";
            txtDesc.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtQty.Text = "";
            txtItem.Focus();

            _tempPriceBook = new List<TempCommonPrice>();
            _commonprice = CHNLSVC.Sales.GetCommonPriceBook(GlbUserName, GlbUserSessionID, gvHPItem.Rows.Count);
            _tempPriceBook = _commonprice;
            gvBooks.DataSource = _tempPriceBook;
            gvBooks.DataBind();

        }

        protected void gvBooks_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _tempPB = "";
            string _tempLVL = "";
            decimal _unitPrice = 0;
            string _itmCD = "";
            decimal _itmTax = 0;

            string _item = "";
            decimal _qty = 0;
            decimal _uprice = 0;
            decimal _amount = 0;
            string _pb = "";
            string _lvl = "";
            double _taxRate = 0;
            decimal _totAmt = 0;
            //decimal _NetAmt = 0;
            //decimal _TotVat = 0;
            Int32 _line = 0;
            string _itmStus = "";



            switch (e.CommandName.ToUpper())
            {
                case "SELECT":
                    {
                        if (gvAllItems.Rows.Count > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You have already select items can't change price book.");
                            return;
                        }

                        for (int i = 0; i < gvBooks.Rows.Count; i++)
                        {
                            CheckBox chk = (CheckBox)gvBooks.Rows[i].FindControl("chkSelect");

                            if (chk.Checked == true)
                            {
                                chk.Checked = false;
                            }
                        }


                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        CheckBox check = (CheckBox)row.FindControl("chkSelect");
                        check.Checked = true;

                        _tempPB = row.Cells[1].Text.ToString();
                        _tempLVL = row.Cells[3].Text.ToString();
                        List<TempCommonPrice> _paramItemsWithPrice = new List<TempCommonPrice>();
                        _paramItemsWithPrice = CHNLSVC.Sales.GetItemsWithPrice(GlbUserName, GlbUserSessionID, _tempPB, _tempLVL);


                        foreach (TempCommonPrice _itmPrice in _paramItemsWithPrice)
                        {
                            InvoiceItem _tempHPItems = new InvoiceItem();
                            _line = _line + 1;
                            //adding newly for get items @ once
                            //_row_id = rowItem.RowIndex;
                            _item = _itmPrice.Tcp_itm;
                            _qty = _itmPrice.tcp_qty;
                            _uprice = _itmPrice.Tcp_price;
                            _amount = Math.Round(_qty * _uprice, 0);
                            _pb = _itmPrice.Tcp_pb_cd;
                            _lvl = _itmPrice.Tcp_pb_lvl;
                            PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                            _lvlDef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, _pb, _lvl);

                            if (GlbUserComCode == "SGL")
                            {
                                _itmStus = "GDLP";
                            }
                            else
                            {
                                _itmStus = "GOD";
                            }


                            if (_lvlDef.Sapl_vat_calc == true)
                            {
                                _taxRate = Convert.ToDouble(TaxCalculation(GlbUserComCode, _item, _itmStus, _amount, 0));
                                _taxRate = Math.Round(_taxRate + 0.49, 0);
                            }
                            else
                            {
                                _taxRate = 0;
                            }

                            // _taxRate = Convert.ToDouble(TaxCalculation(GlbUserComCode, _item, "GOD", _amount, 0));
                            // _taxRate = Math.Round(_taxRate + 0.49);
                            _totAmt = Math.Round(_amount + Convert.ToDecimal(_taxRate), 0);
                            _NetAmt = Math.Round(_NetAmt + _totAmt, 0);
                            _TotVat = Math.Round(_TotVat + Convert.ToDecimal(_taxRate), 0);

                            _tempHPItems.Sad_alt_itm_cd = _item;
                            _tempHPItems.Sad_alt_itm_desc = "";
                            _tempHPItems.Sad_comm_amt = 0;
                            _tempHPItems.Sad_disc_amt = 0;
                            _tempHPItems.Sad_disc_rt = 0;
                            _tempHPItems.Sad_do_qty = 0;
                            _tempHPItems.Sad_fws_ignore_qty = 0;
                            _tempHPItems.Sad_inv_no = "";
                            _tempHPItems.Sad_is_promo = false;
                            _tempHPItems.Sad_itm_cd = _item;
                            _tempHPItems.Sad_itm_line = _line;
                            _tempHPItems.Sad_itm_seq = 0;
                            _tempHPItems.Sad_itm_stus = _itmStus;
                            _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(_taxRate);
                            _tempHPItems.Sad_itm_tp = "";
                            _tempHPItems.Sad_job_line = 0;
                            _tempHPItems.Sad_job_no = "";
                            _tempHPItems.Sad_merge_itm = "";
                            _tempHPItems.Sad_outlet_dept = "";
                            _tempHPItems.Sad_pb_lvl = _lvl;
                            _tempHPItems.Sad_pb_price = _uprice;
                            _tempHPItems.Sad_pbook = _pb;
                            _tempHPItems.Sad_print_stus = true;
                            _tempHPItems.Sad_promo_cd = "";
                            _tempHPItems.Sad_qty = _qty;
                            _tempHPItems.Sad_res_line_no = 0;
                            _tempHPItems.Sad_res_no = "";
                            _tempHPItems.Sad_seq = 0;
                            _tempHPItems.Sad_seq_no = 0;
                            _tempHPItems.Sad_srn_qty = 0;
                            _tempHPItems.Sad_tot_amt = _totAmt;
                            _tempHPItems.Sad_unit_amt = _amount;
                            _tempHPItems.Sad_unit_rt = _uprice;
                            _tempHPItems.Sad_uom = "";
                            _tempHPItems.Sad_warr_based = false;

                            #region Get/Check Warranty Period and Remarks
                            //Get Warranty Details --------------------------
                            List<PriceBookLevelRef> _wlvl = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _pb, _lvl);
                            if (_wlvl != null)
                                if (_wlvl.Count > 0)
                                {
                                    var _lst = (from _l in _wlvl where _l.Sapl_itm_stuts == _itmStus select _l).ToList();
                                    if (_lst != null)
                                        if (_lst.Count > 0)
                                        {
                                            if (_lst[0].Sapl_set_warr == true)
                                            {
                                                WarrantyPeriod = _lst[0].Sapl_warr_period;
                                            }
                                            else
                                            {
                                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item, _itmStus);
                                                if (_period != null)
                                                {
                                                    WarrantyPeriod = _period.Mwp_val;
                                                    WarrantyRemarks = _period.Mwp_rmk;
                                                }
                                                else
                                                {
                                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Warranty period not setup");
                                                    return;
                                                }
                                            }
                                        }
                                }
                            //Get Warranty Details --------------------------
                            #endregion

                            _masterItemDetails = new MasterItem();
                            _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, _item, 1);

                            if (_masterItemDetails.Mi_cd != null)
                            {
                                if (_masterItemDetails.Mi_need_reg == true)
                                {
                                    _tempHPItems.Sad_isapp = false;
                                }
                                else if (_masterItemDetails.Mi_need_reg == false)
                                {
                                    _tempHPItems.Sad_isapp = true;
                                }

                                if (_masterItemDetails.Mi_need_insu == true)
                                {
                                    _tempHPItems.Sad_iscovernote = false;
                                }
                                else if (_masterItemDetails.Mi_need_insu == false)
                                {
                                    _tempHPItems.Sad_iscovernote = true;
                                }
                            }



                            _tempHPItems.Sad_warr_period = WarrantyPeriod;
                            _tempHPItems.Sad_warr_remarks = WarrantyRemarks;
                            _AccountItems.Add(_tempHPItems);


                        }


                        lblTotVat.Text = _TotVat.ToString("0.00");
                        lblTot.Text = _NetAmt.ToString("0.00");


                        gvAllItems.DataSource = _AccountItems;
                        gvAllItems.DataBind();
                        //foreach (TempCommonPrice _one in _paramItemsWithPrice)
                        //{
                        //    _unitPrice = _one.Tcp_price;
                        //    _itmCD = _one.Tcp_itm;
                        //    _itmTax = Math.Round(TaxCalculation(GlbUserComCode, _itmCD, "GOD", _unitPrice, 0), 0);
                        //    _one.Tcp_price = _unitPrice + _itmTax;
                        //    _tempItemWithPrices.Add(_one);

                        //}

                        //_tempItemWithPrices = _paramItemsWithPrice;
                        //gvItemWithPrice.DataSource = _tempItemWithPrices;
                        //gvItemWithPrice.DataBind();

                        break;
                    }

            }
        }



        protected void LoadScheme()
        {
            string _item = "";
            string _brand = "";
            string _mainCat = "";
            string _subCat = "";
            string _pb = "";
            string _lvl = "";


            //List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());


            //if (_hir.Count > 0)
            //{
            //    foreach (MasterSalesPriorityHierarchy _one in _hir)
            //    {
            //string _type = _one.Mpi_cd;
            //string _value = _one.Mpi_val;
            string _type = "PC";
            string _value = GlbUserDefProf;

            for (int x = 0; x < gvAllItems.Rows.Count; x++)
            {
                _masterItemDetails = new MasterItem();
                _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, gvAllItems.Rows[x].Cells[3].Text, 1);

                _item = _masterItemDetails.Mi_cd;
                _brand = _masterItemDetails.Mi_brand;
                _mainCat = _masterItemDetails.Mi_cate_1;
                _subCat = _masterItemDetails.Mi_cate_2;
                _pb = gvAllItems.DataKeys[x][0].ToString();
                _lvl = gvAllItems.DataKeys[x][1].ToString();

                //get details from item
                List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null, null);
                if (_def != null)
                {
                    _SchemeDefinition.AddRange(_def);
                }

                //get details according to main category
                List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null, null);
                if (_def1 != null)
                {
                    _SchemeDefinition.AddRange(_def1);
                }

                //get details according to sub category
                List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat, null);
                if (_def2 != null)
                {
                    _SchemeDefinition.AddRange(_def2);
                }

                //get details according to price book and level
                List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, null);
                if (_def3 != null)
                {
                    _SchemeDefinition.AddRange(_def3);
                }

            }

            //}
            var _record = (from _lst in _SchemeDefinition
                           where _lst.Hpc_is_alw == false
                           select _lst).ToList().Distinct();

            foreach (HpSchemeDefinition j in _record)
            {
                _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd);
            }

            var _final = (from _lst in _SchemeDefinition
                          select _lst.Hpc_sch_cd).ToList().Distinct();

            if (_final != null)
            {
                ddlScheme.DataSource = _final;
                ddlScheme.DataBind();
            }
            //}
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            //decimal _qty = 0;
            // Boolean _foundItem = false;
            Boolean _foundValue = false;

            if (gvAllItems.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Items not found to continue.");
                return;
            }

            //check system parameters
            //    // Item qtys
            //    List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            //    if (_hir != null)
            //    {
            //        if (_hir.Count > 0)
            //        {
            //            foreach (MasterSalesPriorityHierarchy _one in _hir)
            //            {
            //                string _type = _one.Mpi_cd;
            //                string _value = _one.Mpi_val;
            //                _foundItem = false;
            //                _SystemPara = new HpSystemParameters();
            //                _SystemPara = CHNLSVC.Sales.GetSystemParameter(_type, _value, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

            //                if (_SystemPara.Hsy_cd != null)
            //                {
            //                    for (int x = 0; x < gvAllItems.Rows.Count; x++)
            //                    {
            //                        _foundItem = true;
            //                        _mastercompanyItem = new MasterCompanyItem();
            //                        _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(GlbUserComCode, gvAllItems.Rows[x].Cells[3].Text, 1);

            //                        if (_mastercompanyItem.Mci_hpqty_chk == true)
            //                        {
            //                            _qty = _qty + Convert.ToDecimal(gvAllItems.Rows[x].Cells[7].Text);
            //                        }
            //                    }

            //                    if (_SystemPara.Hsy_val < _qty)
            //                    {
            //                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Qty exceed.");
            //                        return;
            //                    }
            //                    goto L10;
            //                }
            //            }
            //        }
            //    }

            //    else
            //    {

            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Hirarchy not defined.");
            //        return;
            //    }
            //L10:
            //    //check if record found
            //    if (_foundItem == false)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Qty parameter not found.");
            //        return;
            //    }

            // Item value
            //List<MasterSalesPriorityHierarchy> _hir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            //if (_hir1 != null)
            //{
            //    if (_hir1.Count > 0)
            //    {
            //        foreach (MasterSalesPriorityHierarchy _one in _hir1)
            //        {

            if (GlbUserDefProf == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Session expire. Please re-log and continue.");
                return;
            }

            _foundValue = false;
            string _type = "PC";
            string _value = GlbUserDefProf;

            _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter(_type, _value, "ACMINAMT", Convert.ToDateTime(lblCreateDate.Text).Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _foundValue = true;
                if (_SystemPara.Hsy_val > _NetAmt)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Minimum value not reach.");
                    return;
                }
                // goto L11;
            }
            //        }
            //    }
            //}
            //    L11:
            if (_foundValue == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot proceed.Minimum HP value parameter not set.");
                return;
            }

            LoadScheme();

            pnlScheme.Visible = true;

            CPEHPItem.Collapsed = true;
            CPEHPItem.ClientState = "true";

            CollapsiblePanelExtender1.Collapsed = false;
            this.CollapsiblePanelExtender1.ClientState = "false";

        }

        protected void imgPro_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlScheme.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select scheme.");
                return;
            }

            if (GlbUserDefProf == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Session expire. Please re-log and continue.");
                return;
            }

            txtCusPay.Text = "0";

            GetDiscountAndCommission();
            GetServiceCharges();
            CalHireAmount();
            CalCommissionAmount();
            GetOtherCharges();
            GetInsuarance();
            CalTotalCash();
            CalInstallmentBaseAmt();
            TotalCash();
            GetInsAndReg();

            Show_Shedule();

            lblPayBalance.Text = lblTotAmt.Text;

        }

        protected void GetInsAndReg()
        {
            Int32 _HpTerm = 0;
            decimal _insAmt = 0;
            decimal _regAmt = 0;
            List<InvoiceItem> _item = new List<InvoiceItem>();
            _item = _AccountItems;

            MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
            foreach (InvoiceItem _tempInv in _item)
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(GlbUserComCode, _tempInv.Sad_itm_cd);

                if (_itemList.Mi_need_insu == true)
                {
                    _HpTerm = Convert.ToInt32(lblTerm.Text);
                    if ((_HpTerm + 3) <= 3)
                    {
                        _HpTerm = 3;
                    }
                    else if ((_HpTerm + 3) <= 6)
                    {
                        _HpTerm = 6;
                    }
                    else if ((_HpTerm + 3) <= 9)
                    {
                        _HpTerm = 9;
                    }
                    else
                    {
                        _HpTerm = 12;
                    }
                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(GlbUserComCode, GlbUserDefProf, "HS", txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, _HpTerm);
                    _insAmt = _insAmt + (_vehIns.Value * _tempInv.Sad_qty);
                }

                if (_itemList.Mi_need_reg == true)
                {
                    _vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(GlbUserComCode, GlbUserDefProf, "HS", _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date);
                    _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
                }
            }
            lblInsFee.Text = _insAmt.ToString("0.00");
            lblRegFee.Text = _regAmt.ToString("0.00");
            lblTobePay.Text = (Convert.ToDecimal(lblTotAmt.Text) + Convert.ToDecimal(lblInsFee.Text) + Convert.ToDecimal(lblRegFee.Text)).ToString("0.00");
        }

        protected void Show_Shedule()
        {
            decimal _insRatio = 0;
            decimal _vatRatio = 0;
            decimal _stampRatio = 0;
            decimal _serviceRatio = 0;
            decimal _intRatio = 0;
            DateTime _tmpDate;
            Int32 i = 0;

            decimal _rental = 0;
            decimal _insuarance = 0;
            decimal _vatAmt = 0;
            decimal _stampDuty = 0;
            decimal _serviceCharge = 0;
            decimal _intamt = 0;
            Int32 _pRental = 0;
            Int32 _balTerm = 0;
            decimal _TotRental = 0;
            _sheduleDetails = new List<HpSheduleDetails>();
            _SchemeDetails = new HpSchemeDetails();

            Int32 _dinsuTerm = 0;
            Int32 _insuTerm = 0;
            string _type = "";
            string _value = "";
            decimal _diriyaInsu = 0;
            string _Htype = "";
            string _Hvalue = "";
            Int32 _colTerm = 0;
            Int32 _MainInsTerm = 0;
            decimal _insuAmt = 0;
            Int32 _SubInsTerm = 0;
            decimal _vehInsuarance = 0;

            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, ddlScheme.Text.Trim());

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_insu_term != null)
                        {
                            _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                        }
                    }
                }
            }

            if (_varTotalInstallmentAmt > 0)
            {
                _diriyaInsu = _varInsAmount - _varFInsAmount;
                _insRatio = (_varInsAmount - _varFInsAmount) / _varTotalInstallmentAmt;
                _vatRatio = (_UVAT + _IVAT - _varInitialVAT) / _varTotalInstallmentAmt;
                _stampRatio = (_varStampduty - _varInitialStampduty) / _varTotalInstallmentAmt;
                _serviceRatio = (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) / _varTotalInstallmentAmt;
                _intRatio = _varInterestAmt / _varTotalInstallmentAmt;
            }

            _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date;

            List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir1.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                {
                    _Htype = _one.Mpi_cd;
                    _Hvalue = _one.Mpi_val;

                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, ddlScheme.Text.Trim());

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_veh_insu_term != null)
                        {

                            _insuTerm = _SchemeDetails.Hsd_veh_insu_term;

                            if (_SchemeDetails.Hsd_veh_insu_col_term != null)
                            {
                                _colTerm = _SchemeDetails.Hsd_veh_insu_col_term;
                            }
                            else
                            {
                                _colTerm = _insuTerm;
                            }


                            _MainInsTerm = _insuTerm / 12;

                            if (_MainInsTerm > 0)
                            {

                                foreach (InvoiceItem _tempInv in _AccountItems)
                                {
                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(GlbUserComCode, GlbUserDefProf, "HS", txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, 12);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                    }
                                }
                            }

                            _SubInsTerm = _insuTerm % 12;

                            if (_SubInsTerm > 0)
                            {
                                if ((_SubInsTerm) <= 3)
                                {
                                    _SubInsTerm = 3;
                                }
                                else if ((_SubInsTerm) <= 6)
                                {
                                    _SubInsTerm = 6;
                                }
                                else if ((_SubInsTerm) <= 9)
                                {
                                    _SubInsTerm = 9;
                                }
                                else
                                {
                                    _SubInsTerm = 12;
                                }

                                foreach (InvoiceItem _tempInv in _AccountItems)
                                {
                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(GlbUserComCode, GlbUserDefProf, "HS", txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, _SubInsTerm);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
                                    }
                                }
                                //else
                                //{
                                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance definition not found for term." + _SubInsTerm);
                                //    return;
                                //}

                            }
                            goto L6;
                        }
                        else
                        {

                            goto L6;
                        }
                    }
                }
            L6: i = 2;
            }





            for (int x = 1; x <= Convert.ToInt16(lblTerm.Text); x++)
            {
                _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(ddlScheme.Text, Convert.ToInt16(x));
                if (_SchemeSheduleDef.Hss_sch_cd != null)
                {
                    if (_SchemeSheduleDef.Hss_is_rt == true)
                    {
                        _rental = (_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt) / 100;
                    }
                    else
                    {
                        _rental = _SchemeSheduleDef.Hss_rnt;
                    }

                    if (x <= _dinsuTerm)
                    {
                        _insuarance = _diriyaInsu / _dinsuTerm;
                    }
                    else
                    {
                        _insuarance = 0;
                    }

                    if (x <= _colTerm)
                    {
                        _vehInsuarance = _insuAmt / _colTerm;
                    }
                    else
                    {
                        _vehInsuarance = 0;
                    }


                    _vatAmt = _rental * _vatRatio;
                    _stampDuty = _rental * _stampRatio;
                    _serviceCharge = _rental * _serviceRatio;
                    _intamt = _rental * _intRatio;
                    _TotRental = _TotRental + _rental;
                    _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                    _pRental = _SchemeSheduleDef.Hss_rnt_no;

                    HpSheduleDetails _tempShedule = new HpSheduleDetails();
                    _tempShedule.Hts_seq = 0;
                    _tempShedule.Hts_acc_no = "";
                    _tempShedule.Hts_rnt_no = _pRental;
                    _tempShedule.Hts_due_dt = _tmpDate;
                    _tempShedule.Hts_rnt_val = _rental;
                    _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                    _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                    _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                    _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                    _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                    _tempShedule.Hts_cre_by = GlbUserName;
                    _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                    _tempShedule.Hts_mod_by = GlbUserName;
                    _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                    _tempShedule.Hts_upload = false;
                    _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                    _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                    _tempShedule.Hts_ins_vat = _varInsVATRate;
                    _tempShedule.Hts_ins_comm = _varInsCommRate;
                    _sheduleDetails.Add(_tempShedule);
                }
                else
                {
                    _rental = _varTotalInstallmentAmt - _TotRental;
                    _balTerm = Convert.ToInt32(lblTerm.Text) - _pRental;
                    _rental = _rental / _balTerm;

                    //_insuarance = _rental * _insRatio;
                    if (x <= _dinsuTerm)
                    {
                        _insuarance = _diriyaInsu / _dinsuTerm;
                    }
                    else
                    {
                        _insuarance = 0;
                    }

                    if (x <= _colTerm)
                    {
                        _vehInsuarance = _insuAmt / _colTerm;
                    }
                    else
                    {
                        _vehInsuarance = 0;
                    }

                    _vatAmt = _rental * _vatRatio;
                    _stampDuty = _rental * _stampRatio;
                    _serviceCharge = _rental * _serviceRatio;
                    _intamt = _rental * _intRatio;

                    _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));

                    HpSheduleDetails _tempShedule = new HpSheduleDetails();
                    _tempShedule.Hts_seq = 0;
                    _tempShedule.Hts_acc_no = "";
                    _tempShedule.Hts_rnt_no = Convert.ToInt16(x);
                    _tempShedule.Hts_due_dt = _tmpDate;
                    _tempShedule.Hts_rnt_val = decimal.Parse(_rental.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                    _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00"));
                    _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                    _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                    _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                    _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                    _tempShedule.Hts_cre_by = GlbUserName;
                    _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                    _tempShedule.Hts_mod_by = GlbUserName;
                    _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                    _tempShedule.Hts_upload = false;
                    _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                    _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                    _tempShedule.Hts_ins_vat = _varInsVATRate;
                    _tempShedule.Hts_ins_comm = _varInsCommRate;
                    _sheduleDetails.Add(_tempShedule);
                }
            }

            gvSchedule.DataSource = _sheduleDetails;
            gvSchedule.DataBind();
        }

        protected void TotalCash()
        {
            lblTotAmt.Text = Convert.ToDecimal(_varInitialVAT + _vDPay + _varInitServiceCharge + _varFInsAmount + _varAddRental + _varInitialStampduty + _varOtherCharges).ToString("0.00");
        }

        protected void CalInstallmentBaseAmt()
        {
            //Calculate amount base to installment
            //vTotalInsValue = Format(varAmountFinance + varInterest + ((varServiceCharges + varServiceChargesAdd - varInitServiceCharges) + (vInsAmt - vFInsAmt)), "0.00")
            _varRental = 0;
            //_varTotalInstallmentAmt = Math.Round(_varAmountFinance + _varInterestAmt + (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) + (_varInsAmount - _varFInsAmount), 0);
            _varTotalInstallmentAmt = Math.Round(_varAmountFinance + _varInterestAmt + (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge), 0);

            _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(ddlScheme.Text, 1);

            if (_SchemeSheduleDef.Hss_sch_cd != null)
            {
                if (_SchemeSheduleDef.Hss_is_rt == true)
                {
                    _varRental = Math.Round(_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt / 100, 0);
                }
                else
                {
                    _varRental = _SchemeSheduleDef.Hss_rnt;
                }
            }
            else
            {
                _varRental = Math.Round(_varTotalInstallmentAmt / Convert.ToInt16(lblTerm.Text), 0);
            }

            CalculateAdditionalRental(_varRental);
        }

        protected void CalculateAdditionalRental(decimal _vRental)
        {
            decimal _tempVarRental = 0;

            _tempVarRental = _vRental * _SchemeDetails.Hsd_noof_addrnt;

            //additional rental calculation
            if (_SchemeDetails.Hsd_add_calwithvat == true)
            {
                if (_SchemeDetails.Hsd_add_is_rt == true)
                {
                    _varAddRental = Math.Round(_DisCashPrice * _SchemeDetails.Hsd_add_rnt / 100, 0);
                }
                else
                {
                    _varAddRental = _SchemeDetails.Hsd_add_rnt;
                }
            }
            else
            {
                if (_SchemeDetails.Hsd_add_is_rt == true)
                {
                    _varAddRental = Math.Round((_DisCashPrice - _UVAT) * _SchemeDetails.Hsd_add_rnt / 100, 0);
                }
                else
                {
                    _varAddRental = _SchemeDetails.Hsd_add_rnt;
                }
            }
            _varAddRental = _varAddRental + _tempVarRental;
            lblAddRent.Text = _varAddRental.ToString("0.00");
        }

        protected void CalTotalCash()
        {
            //Calculate total cash amount
            //varTotalCash = Format(Val(Format(vDPay, "#####0.00")) + varInitServiceCharges + varInitialVAT, "#,###,##0.00")
            _varTotCash = Math.Round(_vDPay + _varInitServiceCharge + _varInitialVAT, 0);
            lblTotCash.Text = _varTotCash.ToString("0.00");
        }

        protected void CalHireAmount()
        {
            //Calculate Hire Value
            _varHireValue = Math.Round(_DisCashPrice + _varInterestAmt + _varServiceCharge, 0);
            lblTotHire.Text = _varHireValue.ToString("0.00");
        }

        protected void CalCommissionAmount()
        {
            //Commission Amount Calulation
            _varCommAmt = Math.Round(_vDPay * _commission / 100, 0);
            lblComAmt.Text = _varCommAmt.ToString("0.00");
        }

        protected void GetServiceCharges()
        {
            string _type = "";
            string _value = "";
            int I = 0;


            //get service chargers
            List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            if (_hir2.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _two in _hir2)
                {
                    _type = _two.Mpi_cd;
                    _value = _two.Mpi_val;

                    List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceCharges(_type, _value, ddlScheme.Text);

                    if (_ser != null)
                    {
                        foreach (HpServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Hps_chk_on == true)
                            {
                                //If Val(rsTemp!sch_Value_From) <= Val(txt_AmtFinance.Text) And Val(rsTemp!sch_Value_To) >= Val(txt_AmtFinance.Text) Then
                                if (_ser1.Hps_from_val <= _varAmountFinance && _ser1.Hps_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Hps_cal_on == true)
                                    {
                                        //varServiceCharges = Format((txt_AmtFinance.Text * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                        _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                    else
                                    {
                                        //varServiceCharges = Format((DisCashPrice * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                        _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                }
                            }
                            else
                            {
                                if (_ser1.Hps_from_val <= _DisCashPrice && _ser1.Hps_to_val >= _DisCashPrice)
                                {
                                    if (_ser1.Hps_cal_on == true)
                                    {
                                        _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                    else
                                    {
                                        _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                }
                            }

                        }
                    }
                }
            L3: I = 1;
                GetAdditionalServiceCharges();
                InitServiceCharge();
            }
        }

        protected void InitServiceCharge()
        {
            if (_SchemeDetails.Hsd_init_serchg == true)
            {
                _varInitServiceCharge = _varServiceCharge;
                _varInitServiceCharge = _varInitServiceCharge + _varServiceChargesAdd;
            }
            else
            {
                _varInitServiceCharge = 0;
            }
            lblService.Text = _varInitServiceCharge.ToString("0.00");
        }


        protected void GetAdditionalServiceCharges()
        {
            string _type = "";
            string _value = "";
            int x = 0;

            _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(ddlScheme.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);

                    if (_ser != null)
                    {
                        foreach (HpAdditionalServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Has_chk_on == true)
                            {
                                if (_ser1.Has_from_val <= _varAmountFinance && _ser1.Has_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Has_cal_on == true)
                                    {
                                        _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                    else
                                    {
                                        _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                }
                            }
                            else
                            {
                                if (_ser1.Has_from_val <= _DisCashPrice && _ser1.Has_to_val >= _DisCashPrice)
                                {
                                    if (_ser1.Has_cal_on == true)
                                    {
                                        _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                    else
                                    {
                                        _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                }
                            }
                        }
                    }
                }
            L6: x = 1;

            }
        }

        protected void GetInsuarance()
        {
            Boolean tempIns = false;
            string _type = "";
            string _value = "";
            decimal _vVal = 0;
            int I = 0;
            _varFInsAmount = 0;
            _varInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            lblInsu.Text = "0.00";
            Boolean _getIns = false;

            if (_SchemeDetails.Hsd_has_insu == true)
            {
                for (int x = 0; x < gvAllItems.Rows.Count; x++)
                {
                    _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, gvAllItems.Rows[x].Cells[3].Text, 1);

                    if (_masterItemDetails.Mi_insu_allow == true)
                    {
                        tempIns = true;
                    }
                }

                if (tempIns == true)
                {
                    List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                    if (_hir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _hir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(ddlScheme.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);
                            if (_ser != null)
                            {
                                foreach (HpInsuranceDefinition _ser1 in _ser)
                                {
                                    _getIns = false;
                                    if (_ser1.Hpi_chk_on == "UP")
                                    {
                                        if (_ser1.Hpi_from_val <= _DisCashPrice && _ser1.Hpi_to_val >= _DisCashPrice)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "AF")
                                    {
                                        if (_ser1.Hpi_from_val <= _varAmountFinance && _ser1.Hpi_to_val >= _varAmountFinance)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "HP")
                                    {
                                        if (_ser1.Hpi_from_val <= _varHireValue && _ser1.Hpi_to_val >= _varHireValue)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;

                                        }
                                    }

                                L7: I = 1;
                                    if (_getIns == true)
                                    {
                                        if (_SchemeDetails.Hsd_init_insu == true)
                                        {
                                            //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = _ser1.Hpi_ins_val;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }

                                        }
                                        else
                                        {
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }
                                        }

                                        _varInsVATRate = _ser1.Hpi_vat_rt;
                                        if (_ser1.Hpi_comm_isrt == true)
                                        {
                                            _varInsCommRate = _ser1.Hpi_comm;
                                        }
                                        lblInsu.Text = _varFInsAmount.ToString("0.00");
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        protected void GetOtherCharges()
        {
            string _item = "";
            string _brand = "";
            string _mainCat = "";
            string _subCat = "";
            string _pb = "";
            string _lvl = "";
            int I = 0;
            decimal TotOther = 0;
            decimal _division = 0;
            int _slabs = 0;
            decimal _grossHV = 0;
            string _type = "";
            string _value = "";
            Int16 _Modslabs = 0;
            _SchemeOtherCharges = new List<HpOtherCharges>();
            _varStampduty = 0;
            _varInitialStampduty = 0;
            _varOtherCharges = 0;
            lblstampduty.Text = "0.00";

            //_SchemeOtherCharges

            for (int x = 0; x < gvAllItems.Rows.Count; x++)
            {
                _masterItemDetails = new MasterItem();
                _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, gvAllItems.Rows[x].Cells[3].Text, 1);

                _item = _masterItemDetails.Mi_cd;
                _brand = _masterItemDetails.Mi_brand;
                _mainCat = _masterItemDetails.Mi_cate_1;
                _subCat = _masterItemDetails.Mi_cate_2;
                _pb = gvAllItems.DataKeys[x][0].ToString();
                _lvl = gvAllItems.DataKeys[x][1].ToString();

                //get details from item
                List<HpOtherCharges> _def = CHNLSVC.Sales.GetOtherCharges(ddlScheme.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null);
                if (_def != null)
                {
                    _SchemeOtherCharges.AddRange(_def);
                    goto L5;
                }

                //get details from main catetory
                List<HpOtherCharges> _def1 = CHNLSVC.Sales.GetOtherCharges(ddlScheme.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null);
                if (_def1 != null)
                {
                    _SchemeOtherCharges.AddRange(_def1);
                    goto L5;
                }

                //get details from sub catetory
                List<HpOtherCharges> _def2 = CHNLSVC.Sales.GetOtherCharges(ddlScheme.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat);
                if (_def2 != null)
                {
                    _SchemeOtherCharges.AddRange(_def2);
                    goto L5;
                }

            L5: I = 1;
            }

            var _record = (from _lst in _SchemeOtherCharges
                           where _lst.Hoc_tp != "STM"
                           select _lst).ToList();

            //if (_record.Count >0)
            //{
            gvOther.DataSource = _record;
            gvOther.DataBind();
            //}

            for (int y = 0; y < gvOther.Rows.Count; y++)
            {
                TotOther = Math.Round(TotOther + Convert.ToDecimal(gvOther.Rows[y].Cells[16].Text), 0);
            }

            lblOtherTotal.Text = TotOther.ToString("0.00");
            lblTotCha.Text = TotOther.ToString("0.00");
            _varOtherCharges = TotOther;

            //stamp duty________
            var _stamp = (from _lst in _SchemeOtherCharges
                          where _lst.Hoc_tp == "STM"
                          select _lst).ToList();

            if (_stamp.Count > 0)
            {
                _grossHV = Convert.ToDecimal(lblTotHire.Text);
                _division = _grossHV / 1000;
                _slabs = Convert.ToInt16(Math.Floor(_division));

                _Modslabs = Convert.ToInt16(_grossHV % 1000);
                if (_Modslabs > 0)
                {
                    _Modslabs = 1;
                }
                else
                {
                    _Modslabs = 0;
                }
                _slabs = Convert.ToInt16(_slabs + _Modslabs);
                _varStampduty = _slabs * 10;
                _varStampduty = Math.Round(_varStampduty, 0);

                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, ddlScheme.Text);

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            if (_SchemeDetails.Hsd_init_sduty == true)
                            {
                                _varInitialStampduty = _varStampduty;
                                lblstampduty.Text = _varInitialStampduty.ToString("0.00");
                                goto L6;
                            }
                            else
                            {
                                _varInitialStampduty = 0;
                                lblstampduty.Text = "0.00";
                                goto L6;
                            }

                        }
                    }
                L6: I = 2;
                }

            }

        }


        protected void GetDiscountAndCommission()
        {
            string _item = "";
            string _brand = "";
            string _mainCat = "";
            string _subCat = "";
            string _pb = "";
            string _lvl = "";
            int i = 0;
            string _type = "";
            string _value = "";
            decimal _vdp = 0;
            decimal _disAmt = 0;
            decimal _sch = 0;
            decimal _FP = 0;
            decimal _inte = 0;
            decimal _AF = 0;
            decimal _rnt = 0;
            decimal _tc = 0;
            decimal _tmpTotPay = 0;
            decimal _Bal = 0;
            _DisCashPrice = 0;
            _varInstallComRate = 0;
            _SchTP = "";
            _SchemeDefinitionComm = new List<HpSchemeDefinition>();
            _SchemeDetails = new HpSchemeDetails();
            _SchemeType = new HpSchemeType();
            _ServiceCharges = new List<HpServiceCharges>();

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {

                for (int x = 0; x < gvAllItems.Rows.Count; x++)
                {

                    _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(GlbUserComCode, gvAllItems.Rows[x].Cells[3].Text, 1);

                    _item = _masterItemDetails.Mi_cd;
                    _brand = _masterItemDetails.Mi_brand;
                    _mainCat = _masterItemDetails.Mi_cate_1;
                    _subCat = _masterItemDetails.Mi_cate_2;
                    _pb = gvAllItems.DataKeys[x][0].ToString();
                    _lvl = gvAllItems.DataKeys[x][1].ToString();

                    foreach (MasterSalesPriorityHierarchy _one in _hir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        //get details from item
                        List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null, ddlScheme.Text);
                        if (_def != null)
                        {
                            _SchemeDefinitionComm.AddRange(_def);
                            goto L1;
                        }

                        //get details according to main category
                        List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null, ddlScheme.Text);
                        if (_def1 != null)
                        {
                            _SchemeDefinitionComm.AddRange(_def1);
                            goto L1;
                        }

                        //get details according to sub category
                        List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat, ddlScheme.Text);
                        if (_def2 != null)
                        {
                            _SchemeDefinitionComm.AddRange(_def2);
                            goto L1;
                        }

                        //get details according to price book and level
                        List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllScheme(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, ddlScheme.Text);
                        if (_def3 != null)
                        {
                            _SchemeDefinitionComm.AddRange(_def3);
                            goto L1;
                        }
                    }
                L1: i = 1;

                }
                _commission = (from _lst in _SchemeDefinitionComm
                               select _lst.Hpc_dp_comm).ToList().Min();

                _discount = (from _lst in _SchemeDefinitionComm
                             select _lst.Hpc_disc).ToList().Min();

                _varInstallComRate = (from _lst in _SchemeDefinitionComm
                                      select _lst.Hpc_inst_comm).ToList().Min();


                lblComRate.Text = _commission.ToString("0.00");
                lblDisRate.Text = _discount.ToString("0.00");
                //lblCashPrice.Text = (Convert.ToDecimal(lblTot.Text) - (Convert.ToDecimal(lblTot.Text) * (_discount) / 100)).ToString("0.00");
                _DisCashPrice = Math.Round(_NetAmt - (_NetAmt * _discount / 100), 0);
                lblCashPrice.Text = _DisCashPrice.ToString("0.00");
                _disAmt = Math.Round(Convert.ToDecimal(_NetAmt) * _discount / 100);
                lblDisAmt.Text = _disAmt.ToString("0.00");

                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, ddlScheme.Text);

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            //get scheme type__________
                            _SchemeType = CHNLSVC.Sales.getSchemeType(_SchemeDetails.Hsd_sch_tp);
                            _SchTP = _SchemeDetails.Hsd_sch_tp;
                            if (_SchemeType.Hst_sch_cat == "S001" || _SchemeType.Hst_sch_cat == "S002")
                            {
                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _UVAT;
                                    _IVAT = 0;
                                }
                                else
                                {
                                    _UVAT = 0;
                                    _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _IVAT;
                                }

                                _varCashPrice = Math.Round(_DisCashPrice - _varVATAmt, 0);
                                lblVat.Text = _UVAT.ToString("0.00");

                                if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }
                                else
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }

                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _varInitialVAT = 0;
                                    _vDPay = Math.Round(_vdp - _UVAT, 0);
                                    _varInitialVAT = _UVAT;
                                }
                                else
                                {
                                    _varInitialVAT = 0;
                                    _varInsVAT = _IVAT;
                                    _varInsVAT = _UVAT;
                                    _vDPay = _vdp;
                                }
                                if (Convert.ToDecimal(txtCusPay.Text) > 0)
                                {
                                    //_vDPay = Convert.ToDecimal(txtCusPay.Text);
                                    _tmpTotPay = Convert.ToDecimal(lblTotAmt.Text);
                                    _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                                    _vDPay = (Convert.ToDecimal(lblDPay.Text) + _Bal);
                                }
                                lblDPay.Text = _vDPay.ToString("0.00");
                                lblVat.Text = _UVAT.ToString("0.00");
                                _MinDPay = _vDPay;
                                _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                lblAmtFin.Text = _varAmountFinance.ToString("0.00");
                                _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);
                                lblIntAmt.Text = _varInterestAmt.ToString("0.00");
                                lblTerm.Text = _SchemeDetails.Hsd_term.ToString();

                                goto L2;
                            }
                            else if (_SchemeType.Hst_sch_cat == "S003" || _SchemeType.Hst_sch_cat == "S004")
                            {

                                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                if (_Saleshir.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchy _one1 in _Saleshir)
                                    {
                                        _type = _one1.Mpi_cd;
                                        _value = _one1.Mpi_val;

                                        _ServiceCharges = CHNLSVC.Sales.getServiceCharges(_type, _value, ddlScheme.Text);

                                        if (_ServiceCharges != null)
                                        {
                                            foreach (HpServiceCharges _ser in _ServiceCharges)
                                            {
                                                if (_ser.Hps_sch_cd != null)
                                                {
                                                    // 1.
                                                    if (_SchemeType.Hst_sch_cat == "S004")
                                                    {
                                                        // 1.1 - Interest free/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = _chr.Hps_chg;
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                            _inte = 0;
                                                        }
                                                        // 1.2 - Interest free/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        // 1.3 - Interest free/Rate/check on Unit Price/calculate on Unit Price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                        }

                                                        // 1.4 - Interest free/Rate/Check on Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _AF / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.5 - Interest free/Rate/Check on Amount Finance/calculate on Unit Price
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _NetAmt / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }

                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.6 - Interest free/Rate/Check on Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_chr.Hps_rt * _AF) / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                    }
                                                    // 2
                                                    else if (_SchemeType.Hst_sch_cat == "S003")
                                                    {
                                                        //2.1 - Interest paid/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100; //rssr!scm_Int_Rate / 100
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                // if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.2 - Interest paid/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.3 - Interest paid/Rate/Check On Unit Price/calculate on unit price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.4 - Interest paid/Rate/Check On Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.5 - Interest paid/Rate/Check On Amount Finance/calculate on unit price
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.6 - Interest paid/Rate/Check On Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    _vDPay = _FP;

                                                    if (Convert.ToDecimal(txtCusPay.Text) > 0)
                                                    {
                                                        //_vDPay = Convert.ToDecimal(txtCusPay.Text);
                                                        _tmpTotPay = Convert.ToDecimal(lblTotAmt.Text);
                                                        _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                                                        _vDPay = (Convert.ToDecimal(lblDPay.Text) + _Bal);
                                                    }

                                                    if (_vDPay < 0)
                                                    {
                                                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Can't create an account.");
                                                        return;
                                                    }

                                                    lblDPay.Text = _vDPay.ToString("0.00");
                                                    lblVat.Text = _UVAT.ToString("0.00");
                                                    _MinDPay = _vDPay;
                                                    _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                                    lblAmtFin.Text = _varAmountFinance.ToString("0.00");
                                                    _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                                    _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);
                                                    lblIntAmt.Text = _varInterestAmt.ToString("0.00");
                                                    lblTerm.Text = _SchemeDetails.Hsd_term.ToString();

                                                    goto L2;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            L2: i = 1;

            }

        }


        protected void rdoBtnSystem_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();

        }

        protected void rdoBtnManual_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();

        }

        private void loadPrifixes()
        {
            //MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, ddl_Location.SelectedValue.Trim());
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, GlbUserDefProf);
            string docTp = "";
            if (rdoBtnManual.Checked)
            { docTp = "HPRM"; }
            else { docTp = "HPRS"; }
            List<string> prifixes = new List<string>();
            // List<string> prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
            prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
            ddlPrefix.DataSource = prifixes;
            ddlPrefix.DataBind();
        }
        protected void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecieptHeader> _temp = new List<RecieptHeader>();
                _temp = Receipt_List;

                int row_id = gvReceipts.Rows.Count - 1;//the last index?
                string prefix = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_prefix"]);
                string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_manual_ref_no"]);
                decimal receiptAmt = Convert.ToDecimal(gvReceipts.DataKeys[row_id]["Sar_tot_settle_amt"]);

                _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
                Receipt_List = _temp;

                bind_gvReceipts(Receipt_List);

                set_PaidAmount();
                set_BalanceAmount();

                Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(GlbUserName, prefix, Convert.ToInt32(receiptNo));
                lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) + Convert.ToDecimal(receiptAmt)).ToString("0.00");

            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            gvReceipts.DataSource = Receiptlist;
            gvReceipts.DataBind();
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

            bind_gvReceipts(Receipt_List);

        }

        public Boolean IsEditMode
        {
            get { return Convert.ToBoolean(Session["IsEditMode"]); }
            set
            {
                Session["IsEditMode"] = value;
                if (value == true)
                {
                    //btnEdit.Enabled = true;
                    //btnCancelRecipt.Enabled = true;
                }
                else
                {
                    //btnEdit.Enabled = false;
                    //btnCancelRecipt.Enabled = false;
                }

            }
        }

        protected void ImgBtnAddReceipt_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();//.SetMessage(CommonUIDefiniton);
            // string location = ddl_Location.SelectedValue;
            //txtAccountNo.Enabled = false;
            string location = GlbUserDefProf;
            string receipt_type = "";
            RecieptHeader Rh = null;

            if (Convert.ToDecimal(lblCashBal.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Amount reach.");
                return;
            }

            if ((Convert.ToDecimal(txtReciptAmount.Text)) > (Convert.ToDecimal(lblCashBal.Text)))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Balance amount exceed.");
                txtReciptAmount.Focus();
                return;
            }


            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
            //------------------------function to edit receipt----------------------
            //if (gvReceipts.Rows.Count == 0)
            //{

            //    if (Rh != null)
            //    {
            //    ////    if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL")
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
            //    ////        string Msg = "<script>alert('This is a cancelled receipt!' );</script>";
            //    ////        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            //    ////        return;
            //    ////    }


            //    ////    //string Msg1 = "<script>alert('Receipt already used- you can edit or cancel Receipt!' );</script>";
            //    ////    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
            //    ////    if (Rh.Sar_receipt_type != "HPRM")
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
            //    ////        return;
            //    ////    }
            //    ////    if (Rh.Sar_receipt_type == "HPRS")//not need
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "System receipts cannot be edited!");
            //    ////        return;
            //    ////    }
            //    ////    if (Rh.Sar_receipt_date < Convert.ToDateTime(lblCreateDate.Text.Trim()))
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot edit/cancel back dated receipts!");
            //    ////        return;
            //    ////    }
            //    ////    //txtAccountNo.Enabled = true;
            //    ////    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can edit or cancel Receipt.");
            //    ////    //----------------------------------------------------------
            //    ////    if ((txtReciptAmount.Text) == "")
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
            //    ////        return;
            //    ////    }
            //    ////    DataTable hierchy_tbl = new DataTable();
            //    ////    Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
            //    ////    hierchy_tbl = SUMMARY.getHP_Hierachy(GlbUserDefProf);//call sp_get_hp_hierachy
            //    ////    Decimal reciptMaxAllowAmount = -99;
            //    ////    if (hierchy_tbl.Rows.Count > 0)
            //    ////    {
            //    ////        foreach (DataRow da in hierchy_tbl.Rows)
            //    ////        {
            //    ////            string party_tp = Convert.ToString(da["MPI_CD"]);
            //    ////            string party_cd = Convert.ToString(da["MPI_VAL"]);
            //    ////            reciptMaxAllowAmount = CHNLSVC.Sales.Get_MaxHpReceiptAmount(Rh.Sar_receipt_type, party_tp, party_cd);
            //    ////            if (reciptMaxAllowAmount >= 0)
            //    ////            {
            //    ////                break;
            //    ////            }

            //    ////        }
            //    ////    }
            //    ////    if (Convert.ToDecimal(txtReciptAmount.Text) > reciptMaxAllowAmount && reciptMaxAllowAmount >= 0)
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed " + reciptMaxAllowAmount);
            //    ////        return;
            //    ////    }
            //    ////    if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            //    ////    {
            //    ////        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT AMOUNT!");
            //    ////        return;
            //    ////    }
            //    ////    string AccNo = Rh.Sar_acc_no;
            //    ////    string ReceiptNo = Rh.Sar_receipt_no;
            //    ////    // //-----------------******************--------------------------
            //    ////    //HpAccount Acc = new HpAccount();
            //    ////    //Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
            //    ////    //uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
            //    ////    //uc_HpAccountDetail1.Uc_hpa_acc_no = AccNo;
            //    ////    //ddlECDType.DataSource = uc_HpAccountSummary1.getAvailableECD_types();
            //    ////    //ddlECDType.DataBind();

            //    ////    //lblAccNo.Text = AccNo;


            //    ////    //txtAccountNo.Text = Acc.Hpa_seq.ToString();
            //    ////    //ddl_Location.SelectedValue = Acc.Hpa_pc;
            //    ////    ////---------------**************************************************---------------------------

            //    ////    //TODO: Load sat_receiptitm to grid payments.

            //    ////    //set receipt heder values that are needed to be updated
            //    ////    Rh.Sar_tot_settle_amt = Convert.ToDecimal(txtReciptAmount.Text.Trim());
            //    ////    //Rh.Sar_acc_no = Acc.Hpa_acc_no;
            //    ////    Rh.Sar_acc_no = "na";
            //    ////    // Rh.Sar_anal_5=
            //    ////    //Rh.Sar_anal_6=
            //    ////    //Rh.Sar_anal_7=
            //    ////    //Rh.Sar_comm_amt=
            //    ////    Rh.Sar_mod_by = GlbUserName;
            //    ////    Rh.Sar_mod_when = Convert.ToDateTime(lblCreateDate.Text);
            //    ////    Rh.Sar_is_mgr_iss = false; 
            //    ////    Rh.Sar_is_oth_shop = false;

            //    ////    Receipt_List.Add(Rh);
            //    ////    bind_gvReceipts(Receipt_List);

            //    ////    //commented on 16-07-2012 (not veiwing the previous payment details.)
            //    ////    //RecieptItem Ri = new RecieptItem();
            //    ////    //Ri.Sard_receipt_no = ReceiptNo;
            //    ////    //_recieptItem=CHNLSVC.Sales.GetReceiptDetails(Ri);
            //    ////    //gvPayment.DataSource = _recieptItem;
            //    ////    //gvPayment.DataBind();

            //    //////    set_PaidAmount();
            //    //// //   set_BalanceAmount();

            //    ////   IsEditMode = true;
            //    ////    //txtAccountNo.Enabled = true;
            //    ////    //added on 20-7-2012
            //    ////    //EditReceiptOriginalAmt = Rh.Sar_tot_settle_amt;
            //    ////    return;
            //    }
            //}
            //------------------------function to edit receipt--End--------------------
            //if (IsEditMode == true)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Only one Receipt can edit or cancel at a time.");
            //    return;
            //}
            //if (lblAccNo.Text == "")
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Account number first!");
            //    txtAccountNo.Enabled = true;
            //    return;
            //}

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
            //  DataTable hierchy_tbl_ = new DataTable();
            Hp_AccountSummary SUMMARY_ = new Hp_AccountSummary();
            // hierchy_tbl_ = SUMMARY_.getHP_Hierachy(GlbUserDefProf);//call sp_get_hp_hierachy
            Decimal reciptMaxAllowAmount_ = -99;
            // if (hierchy_tbl_.Rows.Count > 0)
            // {

            if (rdoBtnManual.Checked)
            {
                receipt_type = "HPRM";
            }
            else
            {
                receipt_type = "HPRS";
            }
            // foreach (DataRow da in hierchy_tbl_.Rows)
            // {
            //string party_tp = Convert.ToString(da["MPI_CD"]);
            //string party_cd = Convert.ToString(da["MPI_VAL"]);
            string party_tp = "PC";
            string party_cd = GlbUserDefProf;

            reciptMaxAllowAmount_ = CHNLSVC.Sales.Get_MaxHpReceiptAmount(receipt_type, party_tp, party_cd);
            if (reciptMaxAllowAmount_ >= 0)
            {

            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Maximum receipt amount not define.");
                return;
            }

            //    }
            //   }
            if (Convert.ToDecimal(txtReciptAmount.Text) > reciptMaxAllowAmount_ && reciptMaxAllowAmount_ >= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed " + reciptMaxAllowAmount_);
                return;
            }

            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {
                //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
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
                Convert.ToDateTime(lblCreateDate.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt date!");
                return;
            }

            if (rdoBtnSystem.Checked == false && rdoBtnManual.Checked == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Receipt type!");
                return;
            }
            //if (rdoBtnCustomer.Checked == false && rdoBtnManager.Checked == false)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select issue party!");
            //    return;
            //}
            if ((txtReciptAmount.Text) == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
                return;
            }
            //DataTable _hierchy_tbl = new DataTable();
            //Hp_AccountSummary SUMM = new Hp_AccountSummary();
            //_hierchy_tbl = SUMM.getHP_Hierachy(GlbUserDefProf);//call sp_get_hp_hierachy
            //Decimal MaxAllowAmount = -99;
            //if (_hierchy_tbl.Rows.Count > 0)
            //{
            //    foreach (DataRow da in _hierchy_tbl.Rows)
            //    {
            //        string party_tp = Convert.ToString(da["MPI_CD"]);
            //        string party_cd = Convert.ToString(da["MPI_VAL"]);
            //        MaxAllowAmount = CHNLSVC.Sales.Get_MaxHpReceiptAmount(Rh.Sar_receipt_type, party_tp, party_cd);
            //        if (MaxAllowAmount >= 0)
            //        {
            //            break;
            //        }

            //    }
            //}
            //if (Convert.ToDecimal(txtReciptAmount.Text) > MaxAllowAmount && MaxAllowAmount >= 0)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed Rs.10,000!");
            //    return;
            //}
            //if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
            //    return;
            //}
            //IsEditMode = false;

            RecieptHeader _recHeader = new RecieptHeader();

            #region Receipt Header Value Assign
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader.Sar_acc_no = "na";

            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            _recHeader.Sar_currency_cd = "LKR";

            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            _recHeader.Sar_debtor_add_1 = txtPreAdd1.Text;
            _recHeader.Sar_debtor_add_2 = txtPreAdd2.Text;
            _recHeader.Sar_debtor_cd = txtPreCode.Text;
            _recHeader.Sar_debtor_name = txtPreName.Text;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            _recHeader.Sar_is_mgr_iss = false;

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            _recHeader.Sar_is_oth_shop = false; // Not sure!
            _recHeader.Sar_remarks = "COLLECTION";


            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = Convert.ToDateTime(lblCreateDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;

            if (ddlinsType.SelectedValue == "Total Cash")
            {
                if (rdoBtnManual.Checked)
                {
                    _recHeader.Sar_receipt_type = "HPDPM";
                }
                else { _recHeader.Sar_receipt_type = "HPDPS"; }
            }
            else if (ddlinsType.SelectedValue == "Additional Rental")
            {
                if (rdoBtnManual.Checked)
                {
                    _recHeader.Sar_receipt_type = "HPARM";
                }
                else { _recHeader.Sar_receipt_type = "HPARS"; }
            }

            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);

            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            _recHeader.Sar_anal_5 = _varInstallComRate;
            //_recHeader.Sar_comm_amt = _varInstallComRate * _recHeader.Sar_tot_settle_amt / 100;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_anal_6 = 0;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);

            //Fill Aanal fields and other required fieles as necessary.
            #endregion

            //if (rdoBtnManual.Checked)
            //{
            #region commented my function
            ////check in the (temp_collect_man_doc_det)
            //Int32 nextReceiptSeqNo;
            //Boolean isTemp = get_temp_man_receipts(GlbUserName, GlbUserDefProf, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text));
            //if (isTemp == false)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt number already used!");
            //    return;
            //}
            //nextReceiptSeqNo = get_Next_ManReceiptNo(GlbUserName, GlbUserDefProf, ddlPrefix.SelectedValue);
            //if (nextReceiptSeqNo == -99)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No more receipts left in the book!");
            //    return;
            //}


            //else if (nextReceiptSeqNo == 0)//no records in  (temp_collect_man_doc_det)
            //{
            //    #region
            //    Int32 effect = validate_Man_ReceiptNo(Convert.ToInt32(txtReceiptNo.Text.Trim()));//validate from original table
            //    if (effect != -99)
            //    {
            //        Decimal tot_receiptAmt = 0;
            //        foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //        {
            //            //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //            //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //            Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //            tot_receiptAmt = tot_receiptAmt + amt;

            //        }
            //        if (Convert.ToDecimal(lblACC_BAL.Text) < (tot_receiptAmt + Convert.ToDecimal(txtReciptAmount.Text)))
            //        {
            //            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the Account Balance!");
            //            return;
            //        }

            //        Receipt_List.Add(_recHeader);
            //        bind_gvReceipts(Receipt_List);

            //        //TODO:
            //        // add to the (temp_collect_man_doc_det)
            //        TempPickManualDocDet obj = saveTo_temp_manDocDet_obj();
            //        //      CHNLSVC.Sales.SaveTemp_coll_Man_doc_dt(obj);

            //    }
            //    else
            //    {
            //        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number not correct!");
            //        return;
            //    }
            //    #endregion

            //}
            //else if (Convert.ToInt32(txtReceiptNo.Text.Trim()) == nextReceiptSeqNo)//if nextReceiptSeqNo !=-99 and nextReceiptSeqNo!=0
            //{
            //    Decimal tot_receiptAmt = 0;
            //    foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //    {
            //        //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //        //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //        Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //        tot_receiptAmt = tot_receiptAmt + amt;

            //    }
            //    if (Convert.ToDecimal(lblACC_BAL.Text) < (tot_receiptAmt + Convert.ToDecimal(txtReciptAmount.Text)))
            //    {
            //        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the Account Balance!");
            //        return;
            //    }
            //    //write to the  (temp_collect_man_doc_det)
            //    TempPickManualDocDet obj = saveTo_temp_manDocDet_obj();
            //    //     CHNLSVC.Sales.SaveTemp_coll_Man_doc_dt(obj);

            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "correct :D !");
            //    Receipt_List.Add(_recHeader);
            //    bind_gvReceipts(Receipt_List);
            //}
            //else
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence is not correct!");
            //    return;
            //}

            #endregion
            //--------------------------------------------------------------------------------------------------------------------------------------------------
            //HPRS
            //   _recHeader.Sar_receipt_type = "HPRS";
            //-------------------************* 
            //Decimal tot_receiptAmt = 0;
            //foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //{
            //    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //    Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //    tot_receiptAmt = tot_receiptAmt + amt;

            //}
            //-------------------------*********
            //if (Convert.ToDecimal(lblACC_BAL.Text) < (tot_receiptAmt + Convert.ToDecimal(txtReciptAmount.Text)))
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the Account Balance!");
            //    return;
            //}
            //  _recHeader.Sar_receipt_type


            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            if (X == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT NUMBER!");
                return;
            }
            else
            {

                Receipt_List.Add(_recHeader);
                bind_gvReceipts(Receipt_List);
            }
            //}
            //else //if System Receipt No
            //{
            //    //TODO: validate System Receipt No
            //    Receipt_List.Add(_recHeader);
            //    bind_gvReceipts(Receipt_List);
            //}
            lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) - Convert.ToDecimal(txtReciptAmount.Text)).ToString("0.00");
            set_PaidAmount();
            set_BalanceAmount();

            txtReciptAmount.Text = "";
            txtReceiptNo.Text = "";
            txtReceiptNo.Focus();
        }

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            //-------------------------------
            divCardDet.Visible = false;
            divCRDno.Visible = false;
            divChequeNum.Visible = false;
            divCredit.Visible = false;
            divAdvReceipt.Visible = false;
            divCreditCard.Visible = false;
            divBankDet.Visible = false;
            //-------------------------------

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;


            List<PaymentTypeRef> _case = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment types are not properly setup!");
                return;
            }
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                //divCredit.Visible = false; 
                divAdvReceipt.Visible = true;
            }
            else
            {
                //divCredit.Visible = false; 
                //divAdvReceipt.Visible = false;

            }
            if (ddlPayMode.SelectedValue == "CHEQUE")
            {
                //divCRDno.Visible = false;
                divChequeNum.Visible = true;
                divBankDet.Visible = true;
            }
            else
            {
                //divChequeNum.Visible = false;
                //  divCRDno.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "CRCD")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = true;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "DEBT")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = false;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            Decimal BankOrOtherCharge = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;

            foreach (PaymentType pt in PaymentTypes)
            {
                if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = BalanceAmount * BCR / 100;

                    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    BankOrOtherCharge = BankOrOtherCharge + BCV;

                    BankOrOther_Charges = BankOrOtherCharge;
                }
            }

            //-----------------**********
            AmtToPayForFinishPayment = (BankOrOtherCharge + BalanceAmount);
            txtPayAmount.Text = AmtToPayForFinishPayment.ToString();

            //-----------------**********
            txtPayAmount.Focus();

            //---------------

            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }

        protected void AddPayment(object sender, EventArgs e)
        {
            try
            {
                Decimal d = Convert.ToDecimal(txtPayAmount.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid amount!");
                return;
            }

            if (Convert.ToDecimal(lblPayBalance.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Amount reach.");
                return;
            }

            Decimal sum_receipt_amt = 0;
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {
                //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                sum_receipt_amt = sum_receipt_amt + amt;
            }
            Decimal BankOrOtherCharge_ = 0;
            if (AmtToPayForFinishPayment != Convert.ToDecimal(txtPayAmount.Text))
            {
                foreach (PaymentType pt in PaymentTypes)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        //   Convert.ToDecimal(txtPayAmount.Text) - BCV;
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100);


                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;


                        BankOrOther_Charges = BankOrOtherCharge_;
                    }
                }
            }

            if ((PaidAmount + Convert.ToDecimal(txtPayAmount.Text.Trim()) - BankOrOther_Charges) > Convert.ToDecimal(lblTotAmt.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot Exceed Total Receipt Amount ");
                return;
            }
            Decimal bankorother = BankOrOther_Charges;
            //lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) - Convert.ToDecimal(txtPayAmount.Text)).ToString("0.00");
            AddPayment();

            set_PaidAmount();
            set_BalanceAmount();
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


            //  _payAmount = Convert.ToDecimal(txtPayAmount.Text);
            _payAmount = Convert.ToDecimal(txtPayAmount.Text) - BankOrOther_Charges;

            //if (_recieptItem.Count <= 0)
            //{
            RecieptItem _item = new RecieptItem();
            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

            string _cardno = string.Empty;
            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text.Trim();
            if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrCardNo.Text.Trim() == "" || txtPayCrCardType.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Card Details.");
                    return;
                }
                _cardno = txtPayCrCardNo.Text.Trim();
                _item.Sard_chq_bank_cd = _cardno;


            }
            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            { _cardno = txtPayAdvReceiptNo.Text; _item.Sard_chq_bank_cd = txtPayCrBank.Text; }
            if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Cheque Details.");
                    return;
                }
                _cardno = txtChequeNo.Text.Trim();
                //_item.Sard_chq_bank_cd = _cardno;
                _item.Sard_ref_no = _cardno;
            }

            if (ddlPayMode.SelectedValue.ToString() == "DEBT" || ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtPayCrBank.Text.Trim() == "" || txtPayCrBranch.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank Details.");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), txtPayCrBranch.Text.Trim(), "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK DETAILS!");
                    return;
                }
            }
            _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
            _item.Sard_cc_period = _period;
            _item.Sard_cc_tp = txtPayCrCardType.Text;
            _item.Sard_chq_bank_cd = txtPayCrBank.Text;
            _item.Sard_chq_branch = txtPayCrBranch.Text;
            _item.Sard_credit_card_bank = null;
            _item.Sard_deposit_bank_cd = null;
            _item.Sard_deposit_branch = null;
            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
            _item.Sard_anal_3 = BankOrOther_Charges;
            // _paidAmount += _payAmount;

            _item.Sard_receipt_no = "";//To be filled when saving.

            _item.Sard_ref_no = _cardno;

            _recieptItem.Add(_item);


            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            clearPaymetnScreen();

        }
        private void clearPaymetnScreen()
        {
            txtPayAmount.Text = "";
            ddlPayMode.SelectedIndex = 0;
            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
            txtChequeNo.Text = "";
        }

        protected void ImgBtnBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
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

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            set_PaidAmount();
            set_BalanceAmount();
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            //try {
            //   DateTime receiptDT= Convert.ToDateTime(txtReceiptDate.Text).Date;
            //}
            //catch(Exception ex){
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Receipt date!");
            //    return;
            //}
            _ddl.Items.Clear();
            //  List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            //   List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", Convert.ToDateTime(txtReceiptDate.Text).Date);
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPSA", DateTime.Now.Date);
            // _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
            //_ddl.DataTextField = "Sapt_cd";
            //_ddl.DataValueField = "Sapt_cd";
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

        protected void ddlinsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlinsType.SelectedValue == "Total Cash")
            {
                lblCashAmt.Text = lblTotCash.Text;
                lblCashBal.Text = lblTotCash.Text;
            }
            else if (ddlinsType.SelectedValue == "Additional Rental")
            {
                lblCashAmt.Text = lblAddRent.Text;
                lblCashBal.Text = lblAddRent.Text;
            }
        }

        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
                    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
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
                //foreach (GridViewRow gvr in this.gvReceipts.Rows)
                //{
                //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                //Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                //receiptAmt = receiptAmt + amt;
                receiptAmt = Convert.ToDecimal(lblTotAmt.Text);
                //}
                BalanceAmount = receiptAmt - PaidAmount;
            }
            lblPayBalance.Text = BalanceAmount.ToString();
        }

        protected void CollectTxn(string _type, decimal _amt, string _hpMnlRef)
        {
            string _desc = "";
            decimal _damt = 0;
            decimal _camt = 0;


            HpTransaction _TmpTrans = new HpTransaction();
            if (_type == "OPBAL")
            {
                _desc = "OPENING BALANCE";
                _damt = Convert.ToDecimal(lblTotHire.Text);
                _camt = 0;
            }
            else if (_type == "DIRIYA")
            {
                _desc = "DIRIYA INSTALLMENTS";
                _damt = _varInsAmount - _varFInsAmount;
                _camt = 0;
            }
            else if (_type == "HPDPM" || _type == "HPDPS")
            {
                _desc = "DOWN PAYMENT";
                _damt = 0;
                _camt = _amt;
            }
            else if (_type == "HPARM" || _type == "HPARS")
            {
                _desc = "ADDITIONAL PAYMENT";
                _damt = 0;
                _camt = _amt;
            }


            if (_damt > 0 || _camt > 0)
            {
                _TmpTrans.Hpt_seq = 1;
                _TmpTrans.Hpt_ref_no = "na";
                _TmpTrans.Hpt_com = GlbUserComCode;
                _TmpTrans.Hpt_pc = GlbUserDefProf;
                _TmpTrans.Hpt_acc_no = "na";
                _TmpTrans.Hpt_txn_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
                _TmpTrans.Hpt_txn_tp = _type;
                _TmpTrans.Hpt_txn_ref = "na";
                _TmpTrans.Hpt_desc = _desc;
                _TmpTrans.Hpt_mnl_ref = _hpMnlRef;
                _TmpTrans.Hpt_crdt = _camt;
                _TmpTrans.Hpt_dbt = _damt;
                _TmpTrans.Hpt_bal = 0;
                _TmpTrans.Hpt_ars = 0;
                _TmpTrans.Hpt_cre_by = GlbUserName;
                _TmpTrans.Hpt_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _MainTrans.Add(_TmpTrans);
            }
            //_MainTxnAuto = new MasterAutoNumber();
            //_MainTxnAuto.Aut_cate_cd = GlbUserDefProf;
            //_MainTxnAuto.Aut_cate_tp = "PC";
            //_MainTxnAuto.Aut_direction = 1;
            //_MainTxnAuto.Aut_modify_dt = null;
            //_MainTxnAuto.Aut_moduleid = "HP";
            //_MainTxnAuto.Aut_number = 0;
            //_MainTxnAuto.Aut_start_char = "HPT";
            //_MainTxnAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;
        }

        protected void OnRemoveFromCusDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            string _cusCode = (string)gvCus.DataKeys[row_id][0];

            if (_HpCustomer != null)
                if (_HpCustomer.Count > 0)
                {
                    _HpCustomer.RemoveAt(row_id);
                    gvCus.DataSource = _HpCustomer;
                    gvCus.DataBind();

                    List<HpCustomer> _hpcust = _HpAccCust;

                    // foreach (HpCustomer _cust in _hpcust)
                    // {
                    //     if (_cust.Htc_cust_cd == _cusCode)
                    //     {
                    _hpcust.RemoveAll(x => x.Htc_cust_cd == _cusCode);
                    //_hpcust.RemoveAt(row_id);
                    //     }
                    // }

                    _HpAccCust = _hpcust;

                    txtProAdd1.Text = "";
                    txtProAdd2.Text = "";

                    txtPreCode.Text = "";
                    txtPreName.Text = "";
                    txtPreAdd1.Text = "";
                    txtPreAdd2.Text = "";

                }
        }

        protected void OnRemoveFromGurDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            string _cusCode = (string)gvGur.DataKeys[row_id][0];

            if (_HpGuranter != null)
                if (_HpGuranter.Count > 0)
                {
                    _HpGuranter.RemoveAt(row_id);
                    gvGur.DataSource = _HpGuranter;
                    gvGur.DataBind();

                    List<HpCustomer> _hpgur = _HpAccCust;

                    _hpgur.RemoveAll(x => x.Htc_cust_cd == _cusCode);


                    _HpAccCust = _hpgur;
                }
        }


        protected void imgInsCom_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInsuCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInsCom.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgPol_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInsuPolicy(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInsPol.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void CheckValidInsuCom(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInsCom.Text))
            {
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid insuarance company.");
                    txtInsCom.Text = "";
                    txtInsCom.Focus();
                    return;
                }
                else
                {
                    txtInsCom.Text = _OutPartyDetails.Mbi_cd;
                }
            }
            txtInsPol.Focus();
        }

        protected void CheckValidPolicy(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtInsPol.Text))
            {
                InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                if (_insuPolicy.Svip_polc_cd == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid insuarance policy.");
                    txtInsPol.Text = "";
                    txtInsPol.Focus();
                    return;
                }

                else
                {
                    txtInsPol.Text = _insuPolicy.Svip_polc_cd;
                }
            }
        }

        protected void btnInsuPrint_Click(object sender, EventArgs e)
        {

            GlbReportPath = "~/Reports_Module/Sales_Rep/InsuPrint.rpt";
            GlbReportMapPath = "~/Reports_Module/Sales_Rep/InsuPrint.rpt";

            GlbDocNosList = txtReAcc.Text.Trim();
            GlbReportName = "Sahana";

            GlbMainPage = "~/HP_Module/AccountCreation.aspx";
            string Msg = "window.open('../Test/PdfPrint.aspx',  '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InsuPrint", Msg, true);

        }

        //protected void btnAll_Click(object sender, EventArgs e)
        //{
        //    string _item = "";
        //    decimal _qty = 0;
        //    decimal _uprice = 0;
        //    double _taxRate = 0;
        //    decimal _amount = 0;
        //    string _pb = "";
        //    string _lvl = "";
        //    decimal _totAmt = 0;
        //    int _row_id = 0;



        //    for (int i = 0; i < gvItemWithPrice.Rows.Count; i++)
        //    //foreach (GridViewRow rowItem in gvItemWithPrice.Rows)
        //    {
        //        InvoiceItem _tempHPItems = new InvoiceItem();
        //        //CheckBox check = (CheckBox)gvItemWithPrice.SelectedRow.FindControl("chkGet");
        //        CheckBox check = (CheckBox)(gvItemWithPrice.Rows[i].Cells[0].FindControl("chkGet"));
        //        if (check.Checked == false)
        //        {
        //            //_row_id = rowItem.RowIndex;
        //            _item = gvItemWithPrice.Rows[i].Cells[1].Text.ToString();
        //            _qty = Convert.ToDecimal(gvItemWithPrice.Rows[i].Cells[3].Text);
        //            _uprice = Math.Round(Convert.ToDecimal(gvItemWithPrice.Rows[i].Cells[4].Text), 0);
        //            _amount = Math.Round(_qty * _uprice, 0);
        //            _pb = (string)gvItemWithPrice.DataKeys[i][0];
        //            _lvl = (string)gvItemWithPrice.DataKeys[i][1];
        //            _taxRate = Convert.ToDouble(TaxCalculation(GlbUserComCode, _item, "GOD", _amount, 0));
        //            _taxRate = Math.Round(_taxRate + 0.49);
        //            _totAmt = Math.Round(_amount + Convert.ToDecimal(_taxRate), 0);
        //            _NetAmt = Math.Round(_NetAmt + _totAmt, 0);
        //            _TotVat = Math.Round(_TotVat + Convert.ToDecimal(_taxRate), 0);
        //            check.Checked = true;

        //            _tempHPItems.Sad_alt_itm_cd = _item;
        //            _tempHPItems.Sad_alt_itm_desc = "";
        //            _tempHPItems.Sad_comm_amt = 0;
        //            _tempHPItems.Sad_disc_amt = 0;
        //            _tempHPItems.Sad_disc_rt = 0;
        //            _tempHPItems.Sad_do_qty = 0;
        //            _tempHPItems.Sad_fws_ignore_qty = 0;
        //            _tempHPItems.Sad_inv_no = "";
        //            _tempHPItems.Sad_is_promo = false;
        //            _tempHPItems.Sad_itm_cd = _item;
        //            _tempHPItems.Sad_itm_line = i + 1;
        //            _tempHPItems.Sad_itm_seq = 0;
        //            _tempHPItems.Sad_itm_stus = "GOD";
        //            _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(_taxRate);
        //            _tempHPItems.Sad_itm_tp = "";
        //            _tempHPItems.Sad_job_line = 0;
        //            _tempHPItems.Sad_job_no = "";
        //            _tempHPItems.Sad_merge_itm = "";
        //            _tempHPItems.Sad_outlet_dept = "";
        //            _tempHPItems.Sad_pb_lvl = _lvl;
        //            _tempHPItems.Sad_pb_price = _uprice;
        //            _tempHPItems.Sad_pbook = _pb;
        //            _tempHPItems.Sad_print_stus = true;
        //            _tempHPItems.Sad_promo_cd = "";
        //            _tempHPItems.Sad_qty = _qty;
        //            _tempHPItems.Sad_res_line_no = 0;
        //            _tempHPItems.Sad_res_no = "";
        //            _tempHPItems.Sad_seq = 0;
        //            _tempHPItems.Sad_seq_no = 0;
        //            _tempHPItems.Sad_srn_qty = 0;
        //            _tempHPItems.Sad_tot_amt = _totAmt;
        //            _tempHPItems.Sad_unit_amt = _amount;
        //            _tempHPItems.Sad_unit_rt = _uprice;
        //            _tempHPItems.Sad_uom = "";
        //            _tempHPItems.Sad_warr_based = false;
        //            _tempHPItems.Sad_warr_period = 0;
        //            _tempHPItems.Sad_warr_remarks = "";
        //            _AccountItems.Add(_tempHPItems);


        //            lblTotVat.Text = _TotVat.ToString("0.00");
        //            lblTot.Text = _NetAmt.ToString("0.00");

        //        }

        //    }

        //    gvAllItems.DataSource = _AccountItems;
        //    gvAllItems.DataBind();

        //}

        //protected void btnCreate_Click1(object sender, EventArgs e)
        //{
        //    GlbReportSerialList = null;
        //    GlbReportWarrantyList = null;
        //    GlbDocNosList = "AAZKI-HS00001";
        //    GlbReportPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
        //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
        //    GlbReportName = "Invoice";
        //    GlbMainPage = "~/HP_Module/AccountCreation.aspx";
        //    Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
        //}

    }
}
