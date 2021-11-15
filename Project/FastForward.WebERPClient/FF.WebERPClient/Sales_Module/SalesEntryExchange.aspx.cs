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
using System.Threading;
using System.Collections;
using System.IO;

namespace FF.WebERPClient.Sales_Module
{
    public partial class SalesEntryExchasnge : BasePage
    {

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["SalesEntryViewState"];
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
            Session["SalesEntryViewState"] = viewStateString;
            ms.Close();
            return;
        }




        protected List<InvoiceItem> _invoiceItemList
        {
            get { return (List<InvoiceItem>)Session["_invoiceItemList"]; }
            set { Session["_invoiceItemList"] = value; }
        }
        protected List<InvoiceItem> _invoiceItemListWithDiscount
        {
            get { return (List<InvoiceItem>)Session["_invoiceItemListWithDiscount"]; }
            set { Session["_invoiceItemListWithDiscount"] = value; }
        }

        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)Session["_recieptItem"]; }
            set { Session["_recieptItem"] = value; }
        }
        protected List<RecieptItem> _newRecieptItem
        {
            get { return (List<RecieptItem>)Session["_newRecieptItem"]; }
            set { Session["_newRecieptItem"] = value; }
        }

        protected MasterProfitCenter _masterProfitCenter
        {
            get { return (MasterProfitCenter)Session["_masterProfitCenter"]; }
            set { Session["_masterProfitCenter"] = value; }
        }
        protected MasterBusinessEntity _businessEntity
        {
            get { return (MasterBusinessEntity)Session["_businessEntity"]; }
            set { Session["_businessEntity"] = value; }
        }
        protected List<MasterItemComponent> _masterItemComponent
        {
            get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; }
            set { Session["_masterItemComponent"] = value; }
        }
        protected PriceBookLevelRef _priceBookLevelRef
        {
            get { return (PriceBookLevelRef)Session["_priceBookLevelRef"]; }
            set { Session["_priceBookLevelRef"] = value; }
        }
        protected PriceBookRef _priceBookRef
        {
            get { return (PriceBookRef)Session["_priceBookRef"]; }
            set { Session["_priceBookRef"] = value; }
        }
        protected PriceDefinitionRef _priceDefinitionRef
        {
            get { return (PriceDefinitionRef)Session["_priceDefinitionRef"]; }
            set { Session["_priceDefinitionRef"] = value; }
        }
        protected List<PriceDetailRef> _priceDetailRef
        {
            get { return (List<PriceDetailRef>)Session["_priceDetailRef"]; }
            set { Session["_priceDetailRef"] = value; }
        }
        protected MasterBusinessEntity _masterBusinessCompany
        {
            get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; }
            set { Session["_masterBusinessCompany"] = value; }
        }

        //For take selected serials to save/ for temporary
        protected List<PriceSerialRef> _MainPriceSerial
        {
            get { return (List<PriceSerialRef>)Session["_MainPriceSerial"]; }
            set { Session["_MainPriceSerial"] = value; }
        }
        protected List<PriceSerialRef> _tempPriceSerial
        {
            get { return (List<PriceSerialRef>)Session["_tempPriceSerial"]; }
            set { Session["_tempPriceSerial"] = value; }
        }
        protected List<PriceSerialRef> _tempPriceSerialItm
        {
            get { return (List<PriceSerialRef>)Session["_tempPriceSerialItm"]; }
            set { Session["_tempPriceSerialItm"] = value; }
        }
        //For take selected combine to save/ for temporary
        protected List<PriceCombinedItemRef> _MainPriceCombinItem
        {
            get { return (List<PriceCombinedItemRef>)Session["_MainPriceCombinItem"]; }
            set { Session["_MainPriceCombinItem"] = value; }
        }
        protected List<PriceCombinedItemRef> _tempPriceCombinItem
        {
            get { return (List<PriceCombinedItemRef>)Session["_tempPriceCombinItem"]; }
            set { Session["_tempPriceCombinItem"] = value; }
        }

        protected bool IsNoPriceDefinition
        {
            get { return Convert.ToBoolean(Session["IsNoPriceDefinition"]); }
            set { Session["IsNoPriceDefinition"] = value; }
        }
        bool _isInventoryCombineAdded
        {
            get { return Convert.ToBoolean(Session["_isInventoryCombineAdded"]); }
            set { Session["_isInventoryCombineAdded"] = value; }
        }

        protected Int32 ScanSequanceNo
        {
            get { return Convert.ToInt32(Session["ScanSequanceNo"]); }
            set { Session["ScanSequanceNo"] = value; }
        }
        //Store the scan serial list even the invoice going to be deliver
        protected List<ReptPickSerials> ScanSerialList
        {
            get { return (List<ReptPickSerials>)Session["ScanSerialList"]; }
            set { Session["ScanSerialList"] = value; }
        }
        //Status of the price level which need to allocate the inventory status should check from the inventory
        protected bool IsPriceLevelAllowDoAnyStatus
        {
            get { return (bool)Session["IsPriceLevelAllowDoAnyStatus"]; }
            set { Session["IsPriceLevelAllowDoAnyStatus"] = value; }
        }
        //Store Warranty Remarks
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
        //Store Serial no which come from txtSerialNo
        protected string ScanSerialNo
        {
            get { return (string)hdnSerialNo.Value; }
            set { hdnSerialNo.Value = value; }
        }
        //Stores the default value of the item status
        protected string DefaultItemStatus
        {
            get { return Convert.ToString(Session["DefaultItemStatus"]); }
            set { Session["DefaultItemStatus"] = value; }

        }
        //Stores invoice select serials
        protected List<InvoiceSerial> InvoiceSerialList
        {
            get { return (List<InvoiceSerial>)Session["InvoiceSerialList"]; }
            set { Session["InvoiceSerialList"] = value; }
        }

        protected List<ReptPickSerials> InventoryCombinItemSerialList
        {
            get { return (List<ReptPickSerials>)Session["InventoryCombinItemSerialList"]; }
            set { Session["InventoryCombinItemSerialList"] = value; }
        }
        protected List<ReptPickSerials> PriceCombinItemSerialList
        {
            get { return (List<ReptPickSerials>)Session["PriceCombinItemSerialList"]; }
            set { Session["PriceCombinItemSerialList"] = value; }
        }
        public string CurrancyCode
        {
            get { return (string)ViewState["CurrancyCode"]; }
            set { ViewState["CurrancyCode"] = value; }
        }


        protected Int32 _lineNo
        {
            get { return (Int32)Session["_lineNo"]; }
            set { Session["_lineNo"] = value; }
        }
        protected bool _isEditPrice
        {
            get { return (bool)Session["_isEditPrice"]; }
            set { Session["_isEditPrice"] = value; }
        }
        protected bool _isEditDiscount
        {
            get { return (bool)Session["_isEditDiscount"]; }
            set { Session["_isEditDiscount"] = value; }
        }
        //Count And Display Only
        protected decimal GrndSubTotal
        {
            get { return (decimal)Session["GrndSubTotal"]; }
            set { Session["GrndSubTotal"] = value; }
        }
        protected decimal GrndDiscount
        {
            get { return (decimal)Session["GrndDiscount"]; }
            set { Session["GrndDiscount"] = value; }
        }
        protected decimal GrndTax
        {
            get { return (decimal)Session["GrndTax"]; }
            set { Session["GrndTax"] = value; }
        }

        protected decimal _paidAmount
        {
            get { return (decimal)Session["_paidAmount"]; }
            set { Session["_paidAmount"] = value; }
        }
        protected decimal _toBePayNewAmount
        {
            get { return (decimal)Session["_toBePayNewAmount"]; }
            set { Session["_toBePayNewAmount"] = value; }
        }
        protected bool _isCompleteCode
        {
            get { return (bool)Session["_isCompleteCode"]; }
            set { Session["_isCompleteCode"] = value; }
        }


        public DataTable PassportTable
        {
            get { return (DataTable)ViewState["PassportTable"]; }
            set { ViewState["PassportTable"] = value; }
        }

        #region Invoice Price Picking Logic
        //check qty numeric?
        //check price book select, if not select default,if not msg=>
        //check price level select,if not default,if not msg=>
        //check combine item

        //check profit center without price allow
        //    if true check for price level not exist
        //        if true load company item status

        //check level is free issues

        //check combine price for customer
        //    if true msg=> having combine offer!


        //check for ByBack offer
        //    if true msg=> having Byback offer!

        //check for combine price general
        //    if true msg=> having combine offer!

        //check for free offer for customer
        //    if ture msg=>having free offer!

        //check for combine free offer
        //    if true msg=> having free offer!



        //if not free offer then
        //    if component grd having records then
        //        if check combine exist = false then
        //            if component available then
        //                get price
        //            else
        //                if get price from customer=true
        //                    get price
        //                else
        //                    price=0
        //                ---
        //            ---
        //        else
        //            price=0
        //        ---
        //    else
        //        if get price from customer
        //            get price
        //        else
        //            price=0
        //        ---
        //    ---

        //@ end load price level status@123@
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(GlbUserName) && string.IsNullOrEmpty(GlbUserDefLoca) && string.IsNullOrEmpty(GlbUserDefProf)) { string Msg = "<script>alert('Your current browse session has been expired. Please re-log to the system!'); window.location = '../Default.aspx' </script>"; ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false); Response.Redirect("../Default.aspx", false); }

            if (string.IsNullOrEmpty(GlbUserDefLoca)) { chkDeliveryNow.Checked = true; chkDeliveryNow.Enabled = false; } else { chkDeliveryNow.Checked = false; chkDeliveryNow.Enabled = true; }
            if (string.IsNullOrEmpty(GlbUserDefProf))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You don't have permission for the profit centers. Please contact administrator");
                Response.Redirect("../Default.aspx", false);
                // string Msg = "<script>alert('You don't have permission for the profit centers. Please contact administrator'); window.location = '../Default.aspx' </script>"; ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }


        }

        private void Check()
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(uc_CustomerCreation1.CustCode);


            uc_CustCreationExternalDet1.SetExtraValues(custProf);

            CustomerAccountRef custAccRef = CHNLSVC.Sales.GetCustomerAccount(GlbUserComCode, uc_CustomerCreation1.CustCode);

            if (uc_CustomerCreation1.CustCode == "")
            {
                ButtonCreate.Enabled = true;


            }
            else
            {
                ButtonCreate.Enabled = false;
                string Msgg = "<script>CheckByCustomer();</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);

                //load flight details
            }
        }

        protected void imgBtnMobile_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtMobile.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //REMOVE CUSTOMER DETAIL THID TAB PANEL
            AjaxControlToolkit.TabPanel tab = (AjaxControlToolkit.TabPanel)((AjaxControlToolkit.TabContainer)cusCreP2.FindControl("TabContainer")).FindControl("Tab_taxDet");
            tab.Visible = false;
            //remove current address panel emove comment
            //Panel panel=(Panel)((AjaxControlToolkit.TabPanel)((AjaxControlToolkit.TabContainer)cusCreP2.FindControl("TabContainer")).FindControl("TabPanel1")).FindControl("Panel_CurrentAddress");
            //panel.Visible = false;
            //end

            #region  Invoice Type Validation and Reload
            string continent = Request.Form[ddlPayMode.UniqueID];
            if (!string.IsNullOrEmpty(txtInvType.Text.Trim())) PopulateDropDownList(PopulatePayModes(txtInvType.Text.Trim()), ddlPayMode);
            if (!string.IsNullOrEmpty(continent) && ddlPayMode.Items.Count > 1) ddlPayMode.Items.FindByValue(continent).Selected = true;

            if (string.IsNullOrEmpty(txtInvType.Text)) { divCredit.Visible = false; divAdvReceipt.Visible = false; }
            #endregion

            #region Load & assign defaul, re-bind
            txtAccBalance.Text = hdnAccountBalance.Value;
            txtAvailableCredit.Text = hdnAvailableCredit.Value;

            _masterItemComponent = new List<MasterItemComponent>();
            _masterProfitCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, GlbUserDefProf);

            if (_masterProfitCenter != null)
                if (_masterProfitCenter.Mpc_cd != null)

                    if (!_masterProfitCenter.Mpc_edit_price) txtUnitPrice.ReadOnly = true;


            if (hdnBusinessEntityModalControler.Value == "1")
                MPEBusinessEntity.Show();

            #endregion



            if (!IsPostBack)
            {
                //add hours to date
                MasterProfitCenter _pc = CHNLSVC.General.GetPCByPCCode(GlbUserComCode, GlbUserDefProf);
                DateTime _date = DateTime.Now;
                _date = _date.AddHours(_pc.Mpc_add_hours);
                //CurrancyCode = _pc.Mpc_def_exrate;
                txtDate.Text = _date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                TextBoxTime.Text = _date.ToString("hh:mm tt");

                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgBtnDate, lblDispalyInfor);

                CurrancyCode = "";
                CreatePassportTable();
                if (_masterProfitCenter != null)
                    if (_masterProfitCenter.Mpc_cd != null)
                    {
                        //txtCustomer.Text = _masterProfitCenter.Mpc_def_customer;
                        txtDelLocation.Text = _masterProfitCenter.Mpc_def_loc;
                        txtCurrency.Text = _masterProfitCenter.Mpc_def_exrate;
                        txtExecutive.Text = _masterProfitCenter.Mpc_man;
                    }

                #region Variable Initialization
                btnSave.Enabled = true;
                txtInvoiceNo.Enabled = true;
                VaribleClear();

                WarrantyRemarks = string.Empty;
                WarrantyPeriod = 0;
                ScanSequanceNo = 0;
                ScanSerialNo = string.Empty;
                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;

                _recieptItem = new List<RecieptItem>();
                ScanSerialList = new List<ReptPickSerials>();
                InventoryCombinItemSerialList = new List<ReptPickSerials>();
                ManagerDiscount = new Dictionary<decimal, decimal>();
                _invoiceItemList = new List<InvoiceItem>();
                InvoiceSerialList = new List<InvoiceSerial>();

                _paidAmount = 0;
                _lineNo = 0;
                _toBePayNewAmount = 0;

                SetDecimalTextBoxForZero(true);

                GrndSubTotal = 0;
                GrndDiscount = 0;
                GrndTax = 0;




                _isCompleteCode = false;
                #endregion

                #region Bind Data @ Initializing
                uc_CustomerCreation1.UserControlButtonClicked += new EventHandler(txtHiddenCustCD_TextChanged);

                gvPopComItemSerial.DataSource = InventoryCombinItemSerialList;
                gvPopComItemSerial.DataBind();

                gvPopSerial.DataSource = ScanSerialList;
                gvPopSerial.DataBind();

                BindInvoiceItem();
                BindReceiptItem();
                //BindLoyalities(ddlPayLoyality, string.Empty, string.Empty, DateTime.Now.Date);

                BindPaymentType(ddlPayMode);
                BindItemComponent(string.Empty);
                BindGeneralDiscount();
                #endregion

                #region Bind Scripts
                OnKeyUp();
                OnBlur();
                OnKeyDown();
                #endregion

                #region Load Defaul Values
                PriceDefinitionRef _lst = CHNLSVC.Sales.GetPriceDefinition(GlbUserComCode, GlbUserDefProf, string.Empty, string.Empty, string.Empty);
                if (_lst != null) if (_lst.Sadd_com != null || !string.IsNullOrEmpty(_lst.Sadd_com)) { txtInvType.Text = _lst.Sadd_doc_tp; txtPriceBook.Text = _lst.Sadd_pb; txtPriceLevel.Text = _lst.Sadd_p_lvl; txtStatus.Text = _lst.Sadd_def_stus; DefaultItemStatus = _lst.Sadd_def_stus; }
                CheckPriceLevelStatusForDoAllow();
                #endregion

                SSCombineLine = 1;

                LoadCurrancies();

            }
            if (gvInvoiceItem.Rows.Count > 0)
            {
                txtPriceBook.Enabled = false;
                txtPriceLevel.Enabled = false;
                imgBtnPriceBook.Enabled = false;
                imgBtnPriceLevel.Enabled = false;
            }
            else
            {
                txtPriceBook.Enabled = true;
                txtPriceLevel.Enabled = true;
                imgBtnPriceBook.Enabled = true;
                imgBtnPriceLevel.Enabled = true;

            }
            Check();
            cusCreP1.EnableMainButtons(false);
        }

        private void LoadCurrancies()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                DropDownCurrancy.DataSource = _cur;
                DropDownCurrancy.DataTextField = "Mcr_desc";
                DropDownCurrancy.DataValueField = "Mcr_cd";
                DropDownCurrancy.DataBind();
            }
        }

        private void CreatePassportTable()
        {
            PassportTable = new DataTable();
            PassportTable.Columns.Add("FlightNo");
            PassportTable.Columns.Add("PassportNo");
        }

        #region Backup function and methods for loading
        private void VaribleClear()
        {
            _lineNo = 1;
            _isEditPrice = false;
            _isEditDiscount = false;

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;

            _paidAmount = 0;

        }
        private void CheckPriceLevelStatusForDoAllow()
        {
            if (!string.IsNullOrEmpty(txtPriceLevel.Text.Trim()) && !string.IsNullOrEmpty(txtPriceBook.Text.Trim()))
            {
                List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim());
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _bool = from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp;
                        if (_bool != null) if (_bool.Count() > 0) IsPriceLevelAllowDoAnyStatus = false; else IsPriceLevelAllowDoAnyStatus = true; else IsPriceLevelAllowDoAnyStatus = true;
                    }
            }
            else
                IsPriceLevelAllowDoAnyStatus = true;
        }
        private void OnKeyUp()
        {
            txtReferance.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnRefNo.ClientID + "')");
            txtInvType.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnInvType.ClientID + "')");
            txtInvoiceNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnInvNo.ClientID + "')");
            txtCustomer.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnCustomer.ClientID + "')");

            txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");
            txtPriceBook.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPriceBook.ClientID + "')");
            txtPriceLevel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPriceLevel.ClientID + "')");
            txtStatus.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnStatus.ClientID + "')");
            txtExecutive.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnExecutive.ClientID + "')");
            txtCurrency.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnCurrency.ClientID + "')");
            txtPOno.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPO.ClientID + "')");
            //txtGroup.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnGroup.ClientID + "')");
            txtPayCrBranch.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnBranch.ClientID + "')");
        }
        private void OnBlur()
        {
            //Magic

            txtItem.Attributes.Add("onblur", "GetItemDescription('" + txtItem.ClientID + "','" + txtDescription.ClientID + "|" + txtItem.ClientID + "');IsUOMDecimalAllow('" + txtItem.ClientID + "','" + txtQty.ClientID + "');");
            txtInvoiceNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvoiceNo, ""));
            txtPriceLevel.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPriceLevel, ""));

            txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));
            txtUnitPrice.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnUnitPrice, ""));

            txtDiscount.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDiscountRate, ""));
            txtDiscountAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDiscountAmt, ""));
            txtVATAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnVAT, ""));
            txtTotalAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnTotalAmt, ""));

            txtManualRefNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnManual, ""));
            txtPayCrBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));

        }
        private void OnKeyDown()
        {
            //txtInvType.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtReferance.ClientID + "').focus();return false;}} else {return true}; ");
            //txtReferance.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtInvoiceNo.ClientID + "').focus();return false;}} else {return true}; ");
            //txtInvoiceNo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCustomer.ClientID + "').focus();return false;}} else {return true}; ");
            //txtCustomer.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCusName.ClientID + "').focus();return false;}} else {return true}; ");
            //txtCusName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtNIC.ClientID + "').focus();return false;}} else {return true}; ");
            //txtNIC.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAddress1.ClientID + "').focus();return false;}} else {return true}; ");
            ////txtAddress1.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtMobile.ClientID + "').focus();return false;}} else {return true}; ");
        }
        #endregion

        #region   DataBind Area

        protected void BindInvoiceItem()
        {
            DataTable _table = CHNLSVC.Sales.GetAllSaleDocumentItemTable(GlbUserComCode, GlbUserDefProf, "INV", txtInvoiceNo.Text, "A");
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvInvoiceItem.DataSource = _table;
            }
            else
            {
                _invoiceItemList = CHNLSVC.Sales.GetAllSaleDocumentItemList(GlbUserComCode, GlbUserDefProf, "INV", txtInvoiceNo.Text, "A");
                gvInvoiceItem.DataSource = _invoiceItemList;

            }
            gvInvoiceItem.DataBind();
        }

        protected void BindReceiptItem()
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(txtInvoiceNo.Text);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text);

            }
            gvPayment.DataBind();
        }

        protected void BindAddItem()
        {
            //if (_invoiceItemList.Count > 0)
            //{
            gvInvoiceItem.DataSource = _invoiceItemList;
            gvInvoiceItem.DataBind();
            //}
        }

        protected bool BindItemComponent(string _item)
        {
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(_item);
            if (_masterItemComponent != null)
            {
                if (_masterItemComponent.Count > 0)
                {
                    _masterItemComponent.ForEach(X => X.Micp_must_scan = false);
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else return false;
            }
            else return false;
        }



        protected void BindSerializedPriceSerial(List<PriceSerialRef> _list)
        {
            gvPopSerialPricePick.DataSource = _list;
            gvPopSerialPricePick.DataBind();
        }

        protected void BindPriceSerial(List<PriceDetailRef> _list)
        {
            gvPopPricePick.DataSource = _list;
            gvPopPricePick.DataBind();
        }

        protected void BindConsumableItem(PriceBookLevelRef _level)
        {
            if (_level.Sapl_chk_st_tp)
            {
                gvPopConsumPricePick.DataSource = CHNLSVC.Sales.GetConsumerProductPriceList(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtStatus.Text.Trim());
                gvPopConsumPricePick.DataBind();

            }
            else
            {
                gvPopConsumPricePick.DataSource = CHNLSVC.Sales.GetConsumerProductPriceList(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                gvPopConsumPricePick.DataBind();
            }

        }

        //protected void BindPaymentType(DropDownList _ddl)
        //{
        //    _ddl.Items.Clear();
        //    List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, string.Empty);
        //    _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
        //    _ddl.DataTextField = "Sapt_cd";
        //    _ddl.DataValueField = "Sapt_cd";
        //    _ddl.SelectedValue = "CASH";
        //    _ddl.DataBind();

        //}

        protected void BindPaymentType(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, txtInvType.Text, DateTime.Now.Date);
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

        protected void BindLoyalities(DropDownList _ddl, string _customer, string _cardno, DateTime _date)
        {
            _ddl.Items.Clear();
            List<LoyaltyMemeber> _paymentTypeRef = CHNLSVC.Sales.GetCustomerLoyality(_customer, _cardno, _date);
            List<LoyaltyMemeber> _list = new List<LoyaltyMemeber>();
            _paymentTypeRef.Add(new LoyaltyMemeber() { Salcm_no = "" });
            foreach (LoyaltyMemeber _one in _paymentTypeRef)
            {
                LoyaltyMemeber _o = new LoyaltyMemeber();
                _o.Salcm_no = _one.Salcm_no;
                _list.Add(_o);
            }

            _ddl.DataSource = _list.Distinct();
            _ddl.DataTextField = "Salcm_no";
            _ddl.DataValueField = "Salcm_no";
            _ddl.DataBind();


        }

        #endregion

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + txtInvType.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + txtInvType.Text + seperator + txtPriceBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(GlbUserComCode + seperator + txtPriceBook.Text + seperator + txtPriceLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtPayCrBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + txtDate.Text + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        #region ---------General details to start the invoice---------------
        protected void imgBtnRefNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrder);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtReferance.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCustomer.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnNICNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtNIC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }



        protected void imgBtnInvType_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceTypeData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnInvNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvoiceNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgbtnSearchGrpCode_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvType.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type!"); txtInvType.Focus(); return; }
            if (txtInvType.Text.Trim() != "CRED") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Group sales only available for credit sales!"); return; }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
            DataTable dataSource = CHNLSVC.CommonSearch.GetGroupSaleSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            // MasterCommonSearchUCtrl.ReturnResultControl = txtGroup.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        #region ---------Invoice item details/Buyback details---------------
        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dataSource = null;
            //if (chkSearchBySerial.Checked == false)
            //{
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            dataSource = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //}
            //else
            //{
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
            //    dataSource = CHNLSVC.CommonSearch.SearchAvailableItemSerial(MasterCommonSearchUCtrl.SearchParams, null, null);
            //}

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnSerial_Click(object sender, ImageClickEventArgs e)
        {
            if (chkSearchBySerial.Checked)
            {
                DataTable dataSource = null;
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                dataSource = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(MasterCommonSearchUCtrl.SearchParams, null, null);
                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtSerialNo.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
        }

        protected void imgBtnPriceBook_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type!");
                txtInvType.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnPriceLevel_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type!");
                txtInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price book!");
                txtPriceBook.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnStatus_Click(object sender, ImageClickEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price book!");
                txtPriceBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceLevel.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price book!");
                txtPriceLevel.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtStatus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        #region -------- Invoice header details ----------------------------
        protected void imgBtnExecutive_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
            DataTable dataSource = CHNLSVC.CommonSearch.GetEmployeeData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtExecutive.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnCurrency_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCurrencyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCurrency.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnPO_Click(object sender, ImageClickEventArgs e)
        {

        }
        #endregion

        #region ---------------------- Payments ----------------------
        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgrecSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceiptsByType(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayAdvReceiptNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBankBranchSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayCrBank.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the bank code!");
                txtPayCrBank.Focus();
                return;
            }


            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
            DataTable dataSource = CHNLSVC.CommonSearch.SearchBankBranchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBranch.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        #endregion

        #endregion

        #region  Session Variables

        public decimal SSPriceBookPrice
        {
            get { return Convert.ToDecimal(Session["pb_price"]); }
            set { Session["pb_price"] = value.ToString(); }
        }
        public string SSPriceBookSequance
        {
            get { return Session["pb_seq"].ToString(); }
            set { Session["pb_seq"] = value; }
        }
        public string SSPriceBookItemSequance
        {
            get { return Session["pb_itm_seq"].ToString(); }
            set { Session["pb_itm_seq"] = value; }
        }
        public string SSIsLevelSerialized
        {
            get { return Session["IsLevelSerialized"].ToString(); }
            set { Session["IsLevelSerialized"] = value; }
        }

        public string SSPromotionCode
        {
            get { return Convert.ToString(Session["PromotionCode"]); }
            set { Session["PromotionCode"] = value; }
        }
        public Int32 SSPRomotionType
        {
            get { return Convert.ToInt32(Session["PromotionType"]); }
            set { Session["PromotionType"] = value; }
        }
        public Int32 SSCombineLine
        {
            get { return Convert.ToInt32(Session["CombineLine"]); }
            set { Session["CombineLine"] = value; }
        }


        #endregion

        #region Rooting for Manual Document Process
        protected void IsValidManualNo(object sender, EventArgs e)
        {
            if (txtManualRefNo.Text != "")
            {
                Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, GlbUserDefLoca, "MDOC_INV", Convert.ToInt32(txtManualRefNo.Text));
                if (_IsValid == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                    txtManualRefNo.Text = "";
                    txtManualRefNo.Focus();
                }
            }
            else
            {
                if (chkManualRef.Checked == true)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                    txtManualRefNo.Focus();
                }
            }
        }
        protected void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualRef.Checked == true)
            {
                txtManualRefNo.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, GlbUserDefLoca, "MDOC_INV");
                if (_NextNo != 0)
                {
                    txtManualRefNo.Text = _NextNo.ToString();
                }
                else
                {
                    txtManualRefNo.Text = "";
                }
            }

            else
            {
                txtManualRefNo.Text = string.Empty;
                txtManualRefNo.Enabled = false;
            }
        }
        #endregion

        #region Rooting for Re-call Invoice

        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            txtInvType.Text = _hdr.Sah_inv_tp;
            txtDate.Text = _hdr.Sah_dt.ToShortTimeString();
            txtCustomer.Text = _hdr.Sah_cus_cd;
            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCustomer.Text);
            txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            txtCurrency.Text = _hdr.Sah_currency;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxInvoice.Checked = _hdr.Sah_tax_inv ? true : false;
        }

        private void RecallInvoice()
        {

            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);
            if (_hdr == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid invoice"); txtInvoiceNo.Text = string.Empty; return; }
            AssignInvoiceHeaderDetail(_hdr);

            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            _invoiceItemList = _list;

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;

            InvoiceSerialList = new List<InvoiceSerial>();
            ScanSerialList = new List<ReptPickSerials>();
            InvoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(txtInvoiceNo.Text.Trim());

            foreach (InvoiceItem itm in _list)
            { CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }
            if (InvoiceSerialList == null)
                InvoiceSerialList = new List<InvoiceSerial>();

            gvInvoiceItem.DataSource = _list;
            gvInvoiceItem.DataBind();


            //2012/11/10
            //load currancy
            PriceBookLevelRef _pr = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, _list[0].Sad_pbook, _list[0].Sad_pb_lvl);
            CurrancyCode = _pr.Sapl_currency_cd;
            //
        }

        #endregion

        #region Rooting for Add Item & Validation with other events
        private InvoiceItem AssignDataToObject(bool _isPromotion, MasterItem _item)
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            InvoiceItem _tempItem = new InvoiceItem();
            _tempItem.Sad_alt_itm_cd = "";
            _tempItem.Sad_alt_itm_desc = "";
            _tempItem.Sad_comm_amt = 0;
            _tempItem.Sad_disc_amt = Convert.ToDecimal(txtDiscountAmt.Text);
            _tempItem.Sad_disc_rt = Convert.ToDecimal(txtDiscount.Text);
            _tempItem.Sad_do_qty = 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtItem.Text;
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = 0;//Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = txtStatus.Text;
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtVATAmt.Text);
            _tempItem.Sad_itm_tp = "";
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = txtPriceLevel.Text;
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = txtPriceBook.Text;
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtTotalAmt.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Sad_warr_period = 0;
            _tempItem.Sad_warr_remarks = "";
            _tempItem.Mi_longdesc = _item.Mi_longdesc;
            _tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Sad_warr_period = WarrantyPeriod;
            _tempItem.Sad_warr_remarks = WarrantyRemarks;

            return _tempItem;
        }
        protected void AddItem(Object sender, EventArgs e)
        {
            AddItem(SSPromotionCode == "0" ? false : true);
        }
        private void ClearAfterAddItem()
        {
            txtItem.Text = "";
            txtStatus.Text = DefaultItemStatus;
            txtQty.Text = FormatToQty("0");
            txtDescription.Text = "";
            txtUnitPrice.Text = FormatToCurrency("0");
            txtDiscount.Text = FormatToCurrency("0");
            txtDiscountAmt.Text = FormatToCurrency("0");
            txtVATAmt.Text = FormatToCurrency("0");
            txtTotalAmt.Text = FormatToCurrency("0");
            txtItem.ReadOnly = false;
        }
        private bool _isCombineAdding = false;
        private int _combineCounter = 0;
        private void AddItem(bool _isPromotion)
        {
            ReptPickSerials _serLst = null;
            List<ReptPickSerials> _nonserLst = null;
            MasterItem _itm = null;

            #region Scan By Serial - check for serial
            //Scan By Serial ------------------start--------------------------
            if (chkSearchBySerial.Checked)
            {
                if (string.IsNullOrEmpty(ScanSerialNo))
                {
                    _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                    if (_itm.Mi_is_ser1 == 1 && chkDeliveryNow.Checked == false)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the serial no");
                        txtSerialNo.Focus();
                        return;
                    }
                }
            }
            //Scan By Serial -------------------end-------------------------
            #endregion

            #region  Adding Com Items - Inventory Comcodes

            if (_isCompleteCode && _isInventoryCombineAdded == false) BindItemComponent(txtItem.Text);

            if (_masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
            {
                //InventoryCombinItemSerialList = new List<ReptPickSerials>();
                string _combineStatus = string.Empty;
                decimal _discountRate = -1;
                decimal _combineQty = 0;

                _isInventoryCombineAdded = true; _isCombineAdding = true;
                if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = txtStatus.Text;
                if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                if (_discountRate == -1) _discountRate = Convert.ToDecimal(txtDiscount.Text);

                List<MasterItemComponent> _comItem = new List<MasterItemComponent>();

                #region Com item check after pick serial (check com main item seperatly, coz its serial already in txtSerialNo textbox)

                foreach (string _item in _masterItemComponent.Select(x => x.ComponentItem.Mi_cd))
                    _masterItemComponent.Where(s => s.ComponentItem.Mi_cd == _item).ToList().ForEach(y => y.ComponentItem.Mi_itm_tp = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item).Mi_itm_tp);

                var _item_ = (from _n in _masterItemComponent where _n.ComponentItem.Mi_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                if (!string.IsNullOrEmpty(_item_[0]))
                {
                    string _mItem = Convert.ToString(_item_[0]);
                    _priceDetailRef = new List<PriceDetailRef>();
                    _priceDetailRef = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, txtInvType.Text, txtPriceBook.Text, txtPriceLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text));

                    if (_priceDetailRef.Count <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _item_[0].ToString() + " does not having price. Please contact costing dept.");
                        return;
                    }
                }

                bool _isMainSerialCheck = false;
                if (ScanSerialList != null && chkDeliveryNow.Checked == false)
                {
                    //check main item serial duplicates
                    if (ScanSerialList.Count > 0)
                    {
                        if (_isMainSerialCheck == false)
                        {

                            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
                            if (_dup != null)
                                if (_dup.Count() > 0)
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!");
                                    return;
                                }
                            _isMainSerialCheck = true;
                        }

                        //Check scan item duplicates


                        foreach (MasterItemComponent _com in _masterItemComponent)
                        {
                            string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
                            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);

                            if (_dup != null)
                                if (_dup.Count() > 0)
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!");
                                    return;
                                }
                        }
                    }

                }
                #endregion

                #region Com item check for its serial status
                if (InventoryCombinItemSerialList.Count == 0)
                {
                    _isCombineAdding = true;
                    foreach (MasterItemComponent _com in _masterItemComponent)
                    {
                        List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);
                        if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.");
                            return;
                        }


                        if (chkDeliveryNow.Checked == false)
                        {

                            decimal _pickQty = 0;
                            if (IsPriceLevelAllowDoAnyStatus)
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                            else
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == txtStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, _com.ComponentItem.Mi_cd, txtStatus.Text.Trim());

                            if (_inventoryLocation != null)
                                if (_inventoryLocation.Count > 0)
                                {
                                    decimal _invBal = _inventoryLocation[0].Inl_qty;
                                    if (_pickQty > _invBal)
                                    {
                                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _com.ComponentItem.Mi_cd + " item inventory balance exceeds");
                                        return;
                                    }
                                }
                                else
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _com.ComponentItem.Mi_cd + " item inventory balance exceeds");
                                    return;
                                }
                            else
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _com.ComponentItem.Mi_cd + " item inventory balance exceeds");
                                return;
                            }
                        }



                        _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _com.ComponentItem.Mi_cd);

                        if (_itm.Mi_is_ser1 == 1 && chkDeliveryNow.Checked == false)
                        {
                            _comItem.Add(_com);
                        }
                    }

                    if (_comItem.Count > 1 && chkDeliveryNow.Checked == false)
                    {
                        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, hdnItemCode.Value, txtSerialNo.Text.Trim());
                        if (_pick != null)
                            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                            {
                                var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                                if (_dup != null)
                                    if (_dup.Count <= 0)
                                    {
                                        InventoryCombinItemSerialList.Add(_pick);
                                    }
                            }

                        _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);
                        gvPopComItem.DataSource = _comItem;
                        gvPopComItem.DataBind();
                        MPEComItemSerial.Show();
                        _isInventoryCombineAdded = false;
                        return;
                    }
                    else if (_comItem.Count == 1 && chkDeliveryNow.Checked == false)
                    {
                        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, hdnItemCode.Value, txtSerialNo.Text.Trim());
                        if (_pick != null)
                            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                            {
                                var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                                if (_dup != null)
                                    if (_dup.Count <= 0)
                                    {
                                        InventoryCombinItemSerialList.Add(_pick);
                                    }
                            }
                    }
                }
                #endregion



                #region  Adding Com items to grid after pick all items (non serialized added randomly,bt check it if deliver now!)
                SSCombineLine += 1;
                foreach (MasterItemComponent _com in _masterItemComponent)
                {
                    //If going to deliver now
                    if (chkDeliveryNow.Checked == false && InventoryCombinItemSerialList.Count > 0)
                    {
                        var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                        if (_comItemSer != null)
                            if (_comItemSer.Count > 0)
                            {
                                foreach (ReptPickSerials _serItm in _comItemSer)
                                {
                                    txtSerialNo.Text = _serItm.Tus_ser_1;
                                    ScanSerialNo = txtSerialNo.Text;
                                    hdnSerialNo.Value = ScanSerialNo;
                                    txtItem.Text = _com.ComponentItem.Mi_cd;
                                    txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                                    txtStatus.Text = _combineStatus;
                                    txtQty.Text = FormatToQty("1");
                                    CheckQty();
                                    txtDiscount.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                    txtDiscountAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                    txtVATAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, txtStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDiscountAmt.Text.Trim()), true)));
                                    txtTotalAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(false);
                                    ScanSerialNo = string.Empty;
                                    txtSerialNo.Text = string.Empty;
                                    hdnSerialNo.Value = "";
                                }
                                _combineCounter += 1;
                            }
                            else
                            {
                                txtItem.Text = _com.ComponentItem.Mi_cd;
                                txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                                txtStatus.Text = _combineStatus;
                                txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                                CheckQty();
                                txtDiscount.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                txtDiscountAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                txtVATAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, txtStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDiscountAmt.Text.Trim()), true)));
                                txtTotalAmt.Text = FormatToCurrency("0");
                                CalculateItem();
                                AddItem(false);
                                ScanSerialNo = string.Empty;
                                txtSerialNo.Text = string.Empty;
                                hdnSerialNo.Value = "";
                                _combineCounter += 1;
                            }

                    }
                    //If deliver later
                    else if (chkDeliveryNow.Checked && InventoryCombinItemSerialList.Count == 0)
                    {
                        txtItem.Text = _com.ComponentItem.Mi_cd;
                        txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                        txtStatus.Text = _combineStatus;
                        txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                        CheckQty();
                        txtDiscount.Text = FormatToCurrency(Convert.ToString(_discountRate));
                        txtDiscountAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                        txtVATAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, txtStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDiscountAmt.Text.Trim()), true)));
                        txtTotalAmt.Text = FormatToCurrency("0");
                        CalculateItem();
                        AddItem(false);
                        _combineCounter += 1;
                    }

                }
                #endregion

                if (_combineCounter == _masterItemComponent.Count) { _masterItemComponent = new List<MasterItemComponent>(); _isCompleteCode = false; _isInventoryCombineAdded = false; _isCombineAdding = false; ScanSerialNo = string.Empty; txtSerialNo.Text = ""; InventoryCombinItemSerialList = new List<ReptPickSerials>(); hdnSerialNo.Value = ""; return; }
            }

            #endregion

            #region Check item with serial status & load particular serial details
            _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());

            if (chkDeliveryNow.Checked == false)
            {
                if (_itm.Mi_is_ser1 == 1)
                {
                    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the serial no");
                        txtSerialNo.Focus();
                        return;
                    }
                    _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                }
                else if (_itm.Mi_is_ser1 == 0)
                {
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                    else
                        _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()));
                }
            }
            #endregion

            #region Check for fulfilment before adding
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            }
            if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")
                if ((Convert.ToDecimal(txtAvailableCredit.Text) - Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCustomer.Text != "CASH")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please check the account balance");
                    return;
                }

            if (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) { if (!_isCompleteCode) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid price"); return; } }
            if (string.IsNullOrEmpty(txtQty.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty"); return; }
            if (Convert.ToDecimal(txtQty.Text) == 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty"); return; }
            if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty"); return; }

            if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid unit price"); return; }


            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type");
                txtInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the customer");
                txtCustomer.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the item");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price book");
                txtPriceBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceLevel.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price level");
                txtPriceLevel.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtStatus.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the item status");
                txtStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the qty");
                txtQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the unit price");
                txtUnitPrice.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDiscount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the discount pecentage");
                txtDiscount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDiscountAmt.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the discount amount");
                txtDiscountAmt.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtVATAmt.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the VAT amount");
                txtVATAmt.Focus();
                return;
            }

            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Selected item does not have setup tax definition for the selected status. Please contact inventory dept.");
                txtStatus.Focus();
                return;
            }

            if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false)
            {
                bool _isTerminate = CheckQty();
                if (_isTerminate) return;
            }

            PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
            if (_lsts != null && _isCombineAdding == false)
            {
                if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " price not available. Please contact costing dept.");
                    return;
                }
                else
                {
                    decimal sysUPrice = _lsts.Sapd_itm_price;
                    decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, txtPriceBook.Text, txtPriceLevel.Text);
                    _masterProfitCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, GlbUserDefProf);

                    if (_masterProfitCenter != null && _priceBookLevelRef != null)
                        if (!string.IsNullOrEmpty(_masterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                            if (!_masterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                if (!_masterProfitCenter.Mpc_edit_price)
                                {
                                    if (sysUPrice != pickUPrice)
                                    {
                                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price Book price and the unit price is different. Please check the price you selected!");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (sysUPrice != pickUPrice)
                                        if (sysUPrice > pickUPrice)
                                        {
                                            decimal sysEditRate = _masterProfitCenter.Mpc_edit_rate;
                                            decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                                            if (ddUprice > pickUPrice)
                                            {
                                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price Book price and the unit price is different. Please check the price you selected!");
                                                return;
                                            }
                                        }

                                }
                }
            }
            else
            {
                if (_isCombineAdding == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " price not available. Please contact costing dept.");
                    return;
                }
            }

            #endregion

            #region Check Item Serial pick or not (function for common item - not for comcode items, but its go through here also


            if (chkDeliveryNow.Checked == false)
            {
                if (_itm.Mi_is_ser1 == 1)
                {
                    var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text && x.Tus_ser_1 == ScanSerialNo).ToList();
                    if (_dup != null)
                        if (_dup.Count > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, ScanSerialNo + " serial is already picked!");
                            txtSerialNo.Focus();
                            return;
                        }
                }

                if (!IsPriceLevelAllowDoAnyStatus)
                {
                    if (_serLst != null)
                        if (string.IsNullOrEmpty(_serLst.Tus_com))
                        {
                            if (_serLst.Tus_itm_stus != txtStatus.Text.Trim())
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, ScanSerialNo + " serial status is not match with the price level status");
                                txtSerialNo.Focus();
                                return;
                            }
                        }
                }

            }
            #endregion

            CalculateItem();

            #region Check Inventory Balance if deliver now!

            //check balance ----------------------
            if (chkDeliveryNow.Checked == false)
            {
                decimal _pickQty = 0;
                if (IsPriceLevelAllowDoAnyStatus)
                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                else
                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == txtStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtStatus.Text.Trim());

                if (_inventoryLocation != null)
                    if (_inventoryLocation.Count > 0)
                    {
                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                        if (_pickQty > _invBal)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " item inventory balance exceeds");
                            return;
                        }
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " item inventory balance exceeds");
                        return;
                    }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " item inventory balance exceeds");
                    return;
                }


                if (_itm.Mi_is_ser1 == 1 && ScanSerialList.Count > 0)
                {
                    var _serDup = (from _lst in ScanSerialList
                                   where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.Trim()
                                   select _lst).ToList();

                    if (_serDup != null)
                        if (_serDup.Count > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Serial duplicating.");
                            return;
                        }

                }



            }
            //check balance ----------------------
            #endregion

            #region Get/Check Warranty Period and Remarks
            //Get Warranty Details --------------------------
            List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim());
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == txtStatus.Text.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            if (_lst[0].Sapl_set_warr == true)
                            {
                                WarrantyPeriod = _lst[0].Sapl_warr_period;
                            }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.Trim(), txtStatus.Text.Trim());
                                if (_period != null)
                                {
                                    WarrantyPeriod = _period.Mwp_val;
                                    WarrantyRemarks = _period.Mwp_rmk;
                                }
                                else
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Warranty period not setup");
                                    return;
                                }
                            }
                        }
                }
            //Get Warranty Details --------------------------
            #endregion

            bool _isDuplicateItem = false;
            Int32 _duplicateComLine = 0;
            Int32 _duplicateItmLine = 0;

            #region Adding Invoice Item
            //Adding Items to grid goes here ----------------------------------------------------------------------
            if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
            //No Records
            {
                _isDuplicateItem = false;
                _lineNo += 1;
                if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm));
            }
            else
            //Having some records
            {
                var _similerItem = from _list in _invoiceItemList
                                   where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == txtStatus.Text && _list.Sad_pbook == txtPriceBook.Text && _list.Sad_pb_lvl == txtPriceLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text)
                                   select _list;

                if (_similerItem.Count() > 0)
                //Similer item available
                {
                    _isDuplicateItem = true;
                    foreach (var _similerList in _similerItem)
                    {
                        _duplicateComLine = _similerList.Sad_job_line;
                        _duplicateItmLine = _similerList.Sad_itm_line;
                        _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDiscountAmt.Text);
                        _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtVATAmt.Text);
                        _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                        _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtTotalAmt.Text);

                    }
                }
                else
                //No similer item found
                {
                    _isDuplicateItem = false;
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm));
                }

            }
            //Adding Items to grid end here ----------------------------------------------------------------------
            #endregion

            #region Adding Serial/Non Serial items
            //Scan By Serial ----------start----------------------------------
            if (chkDeliveryNow.Checked == false)
            {
                if (ScanSequanceNo == 0) ScanSequanceNo = -100;

                //Serialized
                if (_itm.Mi_is_ser1 == 1)
                {
                    //ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                    _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                    _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                    _serLst.Tus_usrseq_no = ScanSequanceNo;
                    _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                    _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                    ScanSerialList.Add(_serLst);
                }

                //Non-Serialized but serial ID 8523
                if (_itm.Mi_is_ser1 == 0)
                {
                    //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                    if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, txtItem.Text + " item qty is exceeds available qty");
                        return;
                    }
                    _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                    _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                    _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                    _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                    _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                    _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                    _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                    ScanSerialList.AddRange(_nonserLst);
                }

                gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A").ToList();
                gvPopSerial.DataBind();

            }
            //Scan By Serial ----------end----------------------------------
            #endregion

            #region Add Invoice Serial Detail
            //if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //{
            bool _isDuplicate = false;
            if (InvoiceSerialList != null)
                if (InvoiceSerialList.Count > 0)
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == hdnItemCode.Value.ToString().Trim() select _i).ToList();
                        if (_dup != null)
                            if (_dup.Count > 0)
                                _isDuplicate = true;
                    }
                }

            if (_isDuplicate == false)
            {
                InvoiceSerial _invser = new InvoiceSerial();
                _invser.Sap_del_loc = GlbUserDefLoca;
                _invser.Sap_itm_cd = hdnItemCode.Value.ToString().Trim();
                _invser.Sap_itm_line = _lineNo;
                _invser.Sap_remarks = string.Empty;
                _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                _invser.Sap_ser_1 = txtSerialNo.Text;
                _invser.Sap_ser_2 = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                InvoiceSerialList.Add(_invser);
            }
            //}
            #endregion

            CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDiscountAmt.Text), Convert.ToDecimal(txtVATAmt.Text), true);

            #region  Adding Combine Items - Price Combine
            if (_MainPriceCombinItem != null)
            {
                string _combineStatus = string.Empty;
                decimal _combineQty = 0;

                if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                {
                    _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = txtStatus.Text;
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);

                    foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                    {
                        txtItem.Text = _list.Sapc_itm_cd;
                        txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                        txtStatus.Text = _combineStatus;
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                        txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty)));
                        txtDiscount.Text = FormatToCurrency("0");
                        txtDiscountAmt.Text = FormatToCurrency("0");
                        txtVATAmt.Text = FormatToCurrency("0");
                        txtTotalAmt.Text = FormatToCurrency("0");
                        CalculateItem();
                        AddItem(_isPromotion);
                        _combineCounter += 1;
                    }
                }

                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); }
            }
            #endregion

            txtSerialNo.Text = "";
            hdnSerialNo.Value = "";
            ClearAfterAddItem();

            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;

            txtItem.Focus();
            BindAddItem();
            SetDecimalTextBoxForZero(true);
            if (ddlPayMode.Items.Count > 1)
                ddlPayMode.SelectedValue = "CASH";



            //2012/11/10
            //get currancy code

            PriceBookLevelRef _pb = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, txtPriceBook.Text, txtPriceLevel.Text);
            CurrancyCode = _pb.Sapl_currency_cd;



            if (gvInvoiceItem.Rows.Count > 0)
            {
                txtPriceBook.Enabled = false;
                txtPriceLevel.Enabled = false;
                imgBtnPriceBook.Enabled = false;
                imgBtnPriceLevel.Enabled = false;
            }
            else
            {
                txtPriceBook.Enabled = true;
                txtPriceLevel.Enabled = true;
                imgBtnPriceBook.Enabled = true;
                imgBtnPriceLevel.Enabled = true;

            }

            if (DropDownCurrancy.Items.Count > 1)
            {
                DropDownCurrancy.SelectedValue = "USD";
                DropDownCurrancy_SelectedIndexChanged(null, null);
            }

            //
        }

        private void SetDecimalTextBoxForZero(bool _isUnit)
        {

            txtDiscount.Text = FormatToCurrency("0");
            txtDiscountAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("0");
            txtTotalAmt.Text = FormatToCurrency("0");
            if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0");
            txtVATAmt.Text = FormatToCurrency("0");

        }
        #endregion

        #region Rooting for Invoice Type
        protected void CheckInvoiceType(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvType.Text)) { return; }

            string h = e.GetType().Attributes.ToString();

            if (!string.IsNullOrEmpty(txtInvType.Text))
            {
                if (!CHNLSVC.Sales.IsValidInvoiceType(GlbUserComCode, GlbUserDefProf, txtInvType.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid invoice type");
                    txtInvType.Text = "";
                    txtInvType.Focus();
                    return;
                }
            }

            //BindPaymentType(ddlPayMode);

        }

        #region  Invoice Type Validation
        static BasePage _page = new BasePage();

        private void PopulateDropDownList(List<string> list, DropDownList ddl)
        {
            ddl.DataSource = list;
            ddl.DataBind();
        }
        public List<string> PopulatePayModes(string invoiceType)
        {
            List<string> payTypes = new List<string>();
            try
            {

                if (!string.IsNullOrEmpty(invoiceType))
                {
                    List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, invoiceType, Convert.ToDateTime(txtDate.Text.Trim()));

                    payTypes.Add("");
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        foreach (PaymentType pt in _paymentTypeRef)
                        {
                            payTypes.Add(pt.Stp_pay_tp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return payTypes;
        }
        [System.Web.Services.WebMethod()]
        public static bool IsValidInvoiceType(string InvType)
        {
            _page = new BasePage();
            bool _isvalid = false;
            _isvalid = _page.CHNLSVC.Sales.IsValidInvoiceType(_page.GlbUserComCode, _page.GlbUserDefProf, InvType);
            return _isvalid;
        }
        #endregion

        #endregion

        #region Rooting for Referance No
        protected void CheckRefNo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferance.Text)) { return; }
        }
        #endregion

        #region Rooting for Invoice No
        protected void CheckInvoiceNo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCustomer.Focus(); return; }
            RecallInvoice();

        }
        #endregion

        #region Rooting for Customer Detail Load / Collect Advance Detail
        //Display Account Balance
        private void ViewCustomerAccountDetail(string _customer)
        {
            if (string.IsNullOrEmpty(_customer.Trim())) return;
            if (_customer != "CASH")
            {
                CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(GlbUserComCode, txtCustomer.Text.Trim());
                txtAccBalance.Text = FormatToCurrency(Convert.ToString(_account.Saca_acc_bal));
                txtAvailableCredit.Text = FormatToCurrency(Convert.ToString((_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal)));
            }
        }
        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;

            if (_isRecall == false)
            {
                txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAddress1.Text = _hdr.Sah_cus_add1;
                txtAddress2.Text = _hdr.Sah_cus_add2;

                txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                txtDelCustomer.Text = _hdr.Sah_cus_cd;
                txtDelName.Text = _hdr.Sah_cus_name;
            }

            if (_isRecall == false)
            {
                if (_masterBusinessCompany.Mbe_is_tax) { chkTaxInvoice.Checked = true; chkTaxInvoice.Enabled = true; } else { chkTaxInvoice.Checked = false; chkTaxInvoice.Enabled = false; }
            }

            //BindLoyalities(ddlPayLoyality, txtCustomer.Text, string.Empty, DateTime.Now.Date);
        }
        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;

            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            }

            if (_masterBusinessCompany != null)
            {
                if (_masterBusinessCompany.Mbe_cd == "CASH")
                {
                    txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                    txtCusName.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtMobile.Text = "";
                    txtNIC.Text = "";
                    chkTaxInvoice.Checked = false;
                }
                else
                {

                    SetCustomerAndDeliveryDetails(false, null);

                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid customer");
                txtCustomer.Text = "";
                txtCusName.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtMobile.Text = "";
                txtNIC.Text = "";
                chkTaxInvoice.Checked = false;
                txtCustomer.Focus();
                return;
            }

            ViewCustomerAccountDetail(txtCustomer.Text);
        }
        protected void LoadCustomerDetailsByNIC(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIC.Text)) { return; }
            _masterBusinessCompany = new MasterBusinessEntity();

            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                if (!IsValidNIC(txtNIC.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid NIC"); txtNIC.Text = ""; return;
                }
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
            }

            //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
            if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
            {
                SetCustomerAndDeliveryDetails(false, null);
            }
            else
            {
                txtCusName.Focus();
                return;
            }
            ViewCustomerAccountDetail(txtCustomer.Text);
        }
        protected void LoadCustomerDetailsByMobile(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMobile.Text)) return;
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                if (!IsValidMobileOrLandNo(txtMobile.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid mobile"); txtMobile.Text = ""; return;
                }
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
            }


            //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
            if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
            {
                SetCustomerAndDeliveryDetails(false, null);
            }
            else
            {
                txtCusName.Focus();
                return;
            }
            ViewCustomerAccountDetail(txtCustomer.Text);
        }

        private void CollectBusinessEntity()
        {
            _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_act = true;
            _businessEntity.Mbe_add1 = txtAddress1.Text;
            _businessEntity.Mbe_add2 = txtAddress2.Text;
            _businessEntity.Mbe_cd = "c1";
            _businessEntity.Mbe_com = GlbUserComCode;
            _businessEntity.Mbe_contact = string.Empty;
            _businessEntity.Mbe_email = string.Empty;
            _businessEntity.Mbe_fax = string.Empty;
            _businessEntity.Mbe_is_tax = false;
            _businessEntity.Mbe_mob = txtMobile.Text;
            _businessEntity.Mbe_name = txtCusName.Text;
            _businessEntity.Mbe_nic = txtNIC.Text;
            _businessEntity.Mbe_tax_no = string.Empty;
            _businessEntity.Mbe_tel = string.Empty;
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_pc_stus = "GOOD";
            _businessEntity.Mbe_ho_stus = "GOOD";
        }
        protected void btnBusAdvDetail_Click(object sender, EventArgs e)
        {
            CollectBusinessEntity();
        }

        protected static void SetCurrentCustomerDetailsToUserControl()
        {
            SalesEntryExchasnge _p = new SalesEntryExchasnge();

            TextBox _customer = (TextBox)_p.FindControl("txtCustomer");
            TextBox _name = (TextBox)_p.FindControl("txtCusName");
            TextBox _add1 = (TextBox)_p.FindControl("txtAddress1");
            TextBox _add2 = (TextBox)_p.FindControl("txtAddress2");
            TextBox _mobile = (TextBox)_p.FindControl("txtMobile");
            TextBox _nic = (TextBox)_p.FindControl("txtNIC.ClientID");

            MasterBusinessEntity _entity = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(_customer.Text))
            {
                if (!_customer.Text.Trim().Contains("CASH"))
                    _entity.Mbe_cd = _customer.Text.Trim();
                else
                    _entity.Mbe_cd = "";

                _entity.Mbe_name = _name.Text.Trim();
                _entity.Mbe_mob = _mobile.Text.Trim();
                _entity.Mbe_nic = _nic.Text.Trim();
                _entity.Mbe_add1 = _add1.Text.Trim();
                _entity.Mbe_add2 = _add2.Text.Trim();

                _p.uc_CustomerCreation1.LoadCustProf(_entity);
                _p.uc_CustCreationExternalDet1.SetExtraValues(_entity);

            }


        }
        protected void txtHiddenCustCD_TextChanged(object sender, EventArgs e)
        {
            //SET VALUES IN THE PAGE
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(uc_CustomerCreation1.CustCode);

            uc_CustCreationExternalDet1.SetExtraValues(custProf);
            CustomerAccountRef custAccRef = CHNLSVC.Sales.GetCustomerAccount(GlbUserComCode, uc_CustomerCreation1.CustCode);

        }

        #endregion

        #region Rooting for Price Book & Level
        protected void CheckPriceBook(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceBook.Text.Trim())) return;

            if (string.IsNullOrEmpty(txtInvType.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type"); return; }

            List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
            _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(GlbUserComCode, txtPriceBook.Text.Trim(), string.Empty, txtInvType.Text.Trim(), GlbUserDefProf);

            if (_def.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid price book");
                txtPriceBook.Text = string.Empty;
                txtPriceBook.Focus();
                return;
            }
        }
        protected void CheckPriceLevel(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceLevel.Text)) { txtItem.Focus(); return; }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price book not select. It is set to profit center default");
                PriceDefinitionRef _lst = CHNLSVC.Sales.GetPriceDefinition(GlbUserComCode, GlbUserDefProf, string.Empty, string.Empty, string.Empty);
                if (_lst != null)
                    if (_lst.Sadd_com != null || !string.IsNullOrEmpty(_lst.Sadd_com))
                        txtPriceBook.Text = _lst.Sadd_pb;
            }


            List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
            _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(GlbUserComCode, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim(), txtInvType.Text.Trim(), GlbUserDefProf);
            if (_def.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid level");
                txtPriceLevel.Text = string.Empty;
                txtPriceLevel.Focus();
                return;
            }

            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, txtPriceBook.Text, txtPriceLevel.Text);
            if (_priceBookLevelRef != null)
            {
                if (_priceBookLevelRef.Sapl_is_serialized) SSIsLevelSerialized = "1";
                else SSIsLevelSerialized = "0";
                _priceDefinitionRef = new PriceDefinitionRef();
                _priceDefinitionRef = CHNLSVC.Sales.GetPriceDefinition(GlbUserComCode, GlbUserDefProf, txtInvType.Text, txtPriceBook.Text, txtPriceLevel.Text);
                CheckPriceLevelStatusForDoAllow();
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid level");
                txtPriceLevel.Text = "";
                txtPriceLevel.Focus();
                return;
            }

        }
        #endregion

        #region Rooting for Calculation Text Box Value, Grand Total & Tax
        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                decimal _vatPortion = TaxCalculation(txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDiscountAmt.Text.Trim()), true);
                txtVATAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    _disAmt = _totalAmount * (Convert.ToDecimal(txtDiscount.Text) / 100);
                    txtDiscountAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtVATAmt.Text))
                {
                    _totalAmount = _totalAmount + Convert.ToDecimal(txtVATAmt.Text) - _disAmt;
                }

                txtTotalAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }
        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }

            lblGrndAfterDiscount.Text = FormatToCurrency(Convert.ToString(GrndSubTotal - GrndDiscount));
            lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(GrndSubTotal - GrndDiscount + GrndTax));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));

        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxPotion)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxPotion == false) _taxs = CHNLSVC.Sales.GetTax(GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim()); else _taxs = CHNLSVC.Sales.GetItemTax(GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxPotion == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                    }
                }
                else
                    if (_isTaxPotion) _pbUnitPrice = 0;


            return _pbUnitPrice;
        }
        #endregion

        #region Rooting for Qty
        protected bool CheckQty()
        {
            bool _IsTerminate = false;

            if (string.IsNullOrEmpty(txtItem.Text)) { _IsTerminate = true; return _IsTerminate; };
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty");
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            if (!string.IsNullOrEmpty(hdnSerialNo.Value))
                txtQty.Text = FormatToQty("1");

            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }

            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type");
                _IsTerminate = true;
                txtInvType.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the customer");
                _IsTerminate = true;
                txtCustomer.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the item");
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price book not select. It is set to profit center default");
                //txtPriceBook.Text = _masterProfitCenter.Mpc_def_pb;
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(txtPriceLevel.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price level");
                _IsTerminate = true;
                txtPriceLevel.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(txtStatus.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the item status");
                _IsTerminate = true;
                txtStatus.Focus();
                return _IsTerminate;
            }

            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;

            ManagerDiscount = new Dictionary<decimal, decimal>();
            //Load Price Level Details
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, txtPriceBook.Text, txtPriceLevel.Text);


            //Inventory Combine Item -------------------------------

            if (_masterItemComponent == null || _masterItemComponent.Count == 0)
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItem.Text.Trim());
                if (_isCompleteCode)
                {
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                        {
                            _isInventoryCombineAdded = false;
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "This compete code does not having a collection. Please contact inventory dept.");
                            _isCompleteCode = false;
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "This compete code does not having a collection. Please contact inventory dept.");
                        _isCompleteCode = false;
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                }
            }
            //Inventory Combine Item -------------------------------



            //Check for tax setup  - under darshana confirmation on 02/06/2012
            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(GlbUserComCode, txtItem.Text.Trim(), txtStatus.Text.Trim(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Selected item does not have setup tax definition for the selected status. Please contact costing dept.");
                _IsTerminate = true;
                return _IsTerminate;
            }






            bool _isMRP = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_anal3;

            if (_isMRP && chkDeliveryNow.Checked == false)
            {

                //GetConsumerProductPriceList
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtStatus.Text.Trim());
                if (_batchRef.Count > 0)
                {
                    if (_batchRef.Count > 1)
                    {
                        lblConsumReqQty.Text = txtQty.Text;
                        divConsumPricePick.Visible = true;
                        BindConsumableItem(_priceBookLevelRef);
                        MPEPopup.Show();

                    }
                    else if (_batchRef.Count == 1)
                    {
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_batchRef.Select(Items => Items.Inb_unit_price))));
                        txtUnitPrice.Focus();

                    }
                }

                //TODO: Load MRP rates

                _isEditPrice = false;
                _isEditDiscount = false;

                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(val));
                CalculateItem();
                _IsTerminate = true;
                return _IsTerminate;
            }



            //Check the price for the specific customer availabillity (even for special promotions)
            //Check the price for special promotion without Customer
            //Check the price for normal price
            //If no price =>message

            if (_masterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
            {
                //User Can edit the price for any amount and having inventory status
                //No price book price available and no restriction for price amendment
                SetDecimalTextBoxForZero(false);
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                //Serialized price level
                //User directing for select the serial from pick the serials,
                //It should fire after enter item code, and without enter qty.
                //After selecting serial, the selected serials will goes to DO grid and the items will add to the sales entry end.

                //The event should be performed in lostforcus of the item as same at the "Add Item"  button
                List<PriceSerialRef> _list = new List<PriceSerialRef>();// CHNLSVC.Sales.GetAllPriceSerial(txtPriceBook.Text, txtPriceLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text);
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list.Count < Convert.ToDecimal(txtQty.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Selected qty is exceeds available serials!");
                    txtQty.Text = FormatToQty("0");
                    IsNoPriceDefinition = true;
                    txtQty.Focus();

                }


                if (_list.Count > 0)
                {
                    lblPopSerialQty.Text = txtQty.Text;
                    BindSerializedPriceSerial(_list);
                    divPopSerialPriceList.Visible = true;
                    divPopSerialPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    IsNoPriceDefinition = false;
                    MPEPopup.Show();
                }
                _IsTerminate = true;
                return _IsTerminate;
            }

            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, txtInvType.Text, txtPriceBook.Text, txtPriceLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));



            if (_priceDetailRef.Count <= 0)
            {
                //Inventory Combine Item -------------------------------
                if (!_isCompleteCode)
                {
                    //Msg for no price define
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "There is no price for the selected item");
                    IsNoPriceDefinition = true;
                    _IsTerminate = true;
                    return _IsTerminate;
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency("0");
                }
                //Inventory Combine Item -------------------------------
            }
            else
            {
                //Inventory Combine Item -------------------------------
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    _priceDetailRef.Add(_new.Where(x => x.Sapd_price_type == 0).ToList()[0]);
                }
                //Inventory Combine Item -------------------------------

                if (_priceDetailRef.Count > 1)
                {
                    //Find More than one price for the selected item
                    //Load price prices for the grid and popup for user confirmation
                    lblPopNonSerialQty.Text = txtQty.Text;

                    divPopPriceList.Visible = true;
                    divPopPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    IsNoPriceDefinition = false;
                    MPEPopup.Show();
                    BindPriceSerial(_priceDetailRef);
                    _IsTerminate = true;
                    return _IsTerminate;
                }
                else if (_priceDetailRef.Count == 1)
                {

                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;

                        PriceTypeRef _promotion = TakePromotion(_priceType);

                        //Tax Calculation
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDiscountAmt.Text.Trim()), false);

                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        SSPriceBookPrice = UnitPrice;
                        SSPriceBookSequance = _single.Sapd_pb_seq.ToString();
                        SSPriceBookItemSequance = _single.Sapd_seq_no.ToString();
                        WarrantyRemarks = _single.Sapd_warr_remarks;

                        Int32 _pbSq = _single.Sapd_pb_seq;
                        string _mItem = _single.Sapd_itm_cd;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                            _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, string.Empty);
                            if (_tempPriceCombinItem != null)
                            {
                                gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                                gvPriceItemCombine.DataBind();
                                divPopPriceItemCombination.Visible = true;
                                _IsTerminate = true;
                                MPEPopup.Show();
                                return _IsTerminate;
                            }
                            else
                            {
                                divPopPriceItemCombination.Visible = false;
                                lblMsg.Text = "There is no such combine items pick";
                                _IsTerminate = true;
                                MPEPopup.Show();
                                return _IsTerminate;
                            }
                        }
                        else
                        {
                            txtUnitPrice.Focus();
                        }

                    }

                }

            }

            _isEditPrice = false;
            _isEditDiscount = false;

            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();
            return _IsTerminate;
        }
        protected void CheckQty(object sender, EventArgs e)
        {
            txtUnitPrice.Text = FormatToCurrency("0");
            txtDiscount.Text = FormatToCurrency("0");
            txtDisAmount.Text = FormatToCurrency("0");
            txtVATAmt.Text = FormatToCurrency("0");
            txtTotalAmt.Text = FormatToCurrency("0");
            CheckQty();
        }
        #endregion

        #region Rooting for Unit Price
        protected void CheckUnitPrice(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select valid qty");
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;


            if (!_isCompleteCode)
            {
                if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                {
                    decimal _pb_price;
                    if (SSPriceBookPrice == 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price not define. Please check the system updated price.");
                        txtUnitPrice.Text = FormatToCurrency("0");
                        return;
                    }
                    _pb_price = SSPriceBookPrice;
                    decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                    if (_masterProfitCenter.Mpc_edit_price)
                    {

                        if (_pb_price > _txtUprice)
                        {
                            decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                            if (_diffPecentage > _masterProfitCenter.Mpc_edit_rate)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not deduct price more than " + _masterProfitCenter.Mpc_edit_rate + "% from the price book price.");
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                                _isEditPrice = false;
                                return;
                            }
                            else
                            {
                                _isEditPrice = true;
                            }
                        }
                    }
                    else
                    {
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                        _isEditPrice = false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtUnitPrice.Text);
            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();

        }
        #endregion

        #region Rooting for Discount
        Dictionary<decimal, decimal> ManagerDiscount
        {
            get { return (Dictionary<decimal, decimal>)Session["ManagerDiscount"]; }
            set { Session["ManagerDiscount"] = value; }
        }

        protected void CheckDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty");

                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (!string.IsNullOrEmpty(txtDiscount.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDiscount.Text);

                if (_disRate > 0)
                {
                    ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    if (ManagerDiscount.Count > 0)
                    {
                        var vals = ManagerDiscount.Select(x => x.Key).ToList();
                        var rates = ManagerDiscount.Select(x => x.Value).ToList();

                        if (rates[0] < _disRate)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not discount price more than " + rates[0] + "%.");
                            txtDiscount.Text = FormatToCurrency("0");
                            // txtDiscount.Focus();
                            _isEditDiscount = false;
                            return;
                        }
                        else
                            _isEditDiscount = true;
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Not allow for apply discount");
                        txtDiscount.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return;
                    }
                }
                else
                    _isEditDiscount = false;


            }
            else if (_isEditPrice)
            {
                txtDiscount.Text = FormatToCurrency("0");
            }


            if (string.IsNullOrEmpty(txtDiscount.Text)) txtDiscount.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDiscount.Text);
            txtDiscount.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
        }
        private Object thisLock = new Object();

        protected void CheckDiscountRate(object sender, EventArgs e)
        {
            CheckDiscountRate();
        }

        private void CheckDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty");

                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (!string.IsNullOrEmpty(txtDiscountAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDiscountAmt.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
                decimal _qty = Convert.ToDecimal(txtQty.Text);
                decimal _totAmt = _uRate * _qty;
                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                if (_disAmt > 0)
                {
                    if (ManagerDiscount.Count > 0)
                    {
                        var vals = ManagerDiscount.Select(x => x.Key).ToList();
                        var rates = ManagerDiscount.Select(x => x.Value).ToList();

                        if (vals[0] < _disAmt)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not discount price more than " + vals[0] + ".");
                            txtDiscountAmt.Text = FormatToCurrency("0");
                            txtDiscount.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return;
                        }
                        else
                        {
                            txtDiscount.Text = "0";
                            CalculateItem();
                            _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                            txtDiscount.Text = FormatToCurrency(Convert.ToString(_percent));
                            CalculateItem();
                            CheckDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        if (ManagerDiscount.Count > 0)
                        {
                            var vals = ManagerDiscount.Select(x => x.Key).ToList();
                            var rates = ManagerDiscount.Select(x => x.Value).ToList();

                            if (vals[0] < _disAmt)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not discount price more than " + vals[0] + ".");
                                txtDiscountAmt.Text = FormatToCurrency("0");
                                txtDiscount.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return;
                            }
                            else
                            {
                                txtDiscount.Text = "0";
                                CalculateItem();
                                _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                txtDiscount.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Not allow for discount");
                            txtDiscountAmt.Text = FormatToCurrency("0");
                            txtDiscount.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return;
                        }
                    }
                }
                else
                    _isEditDiscount = false;

            }
            else if (_isEditPrice)
            {
                txtDiscountAmt.Text = FormatToCurrency("0");
                txtDiscount.Text = FormatToCurrency("0");
            }

            if (string.IsNullOrEmpty(txtDiscountAmt.Text)) txtDiscountAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDiscountAmt.Text);
            txtDiscountAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
        }

        protected void CheckDiscountAmount(object sender, EventArgs e)
        {
            CheckDiscountAmount();
        }
        #endregion

        #region Rooting for VAT/Tax
        protected void CheckVAT(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty");
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (string.IsNullOrEmpty(txtVATAmt.Text)) txtVATAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtVATAmt.Text);
            txtVATAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
        }

        protected void CheckTotalAmt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text, NumberStyles.Number) == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid qty");
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (string.IsNullOrEmpty(txtTotalAmt.Text)) txtTotalAmt.Text = FormatToCurrency("0");
            CalculateItem();
            decimal val = Convert.ToDecimal(txtTotalAmt.Text);
            txtTotalAmt.Text = FormatToCurrency(Convert.ToString(val));
        }
        #endregion

        #region Rooting for Pick Item unit price/ serialized unit price/ Promotion
        public bool IsFixQty
        {
            get { return Convert.ToBoolean(Session["IsFixQty"]); }
            set { Session["IsFixQty"] = value; }
        }
        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;

            }
            return _list;
        }
        //When click confirm on the price list for serials
        protected void btnPopConfirm_Click(object sender, EventArgs e)
        {

            #region Confirmation Serialized Price Level Price

            if (divPopSerialPriceList.Visible && divPopSerialPriceList.Disabled == false)
            {
                Int32 _counter = 0;
                if (_MainPriceSerial == null) _MainPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerialItm = new List<PriceSerialRef>();
                bool _isInventoryAvailable = true;
                string _itemSerialList = string.Empty;

                #region  Rooting for selected item/serial/qty checking
                foreach (GridViewRow row in gvPopSerialPricePick.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox _chk = row.FindControl("chkPopPricePick") as CheckBox;
                        string _serial = row.Cells[3].Text;
                        HiddenField _mitem = row.FindControl("hdnMainItem") as HiddenField;

                        if (_chk.Checked)
                        {
                            _counter++;

                            var _obj = from _list in _tempPriceSerial
                                       where _list.Sars_ser_no == _serial && _list.Sars_itm_cd == txtItem.Text
                                       select _list;
                            if (chkDeliveryNow.Checked == false)
                            {
                                //check for serial availability
                                ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, _mitem.Value.ToString().Trim(), _serial.Trim());
                                if (string.IsNullOrEmpty(_serLst.Tus_com))
                                {
                                    _isInventoryAvailable = false;
                                    if (string.IsNullOrEmpty(_itemSerialList))
                                        _itemSerialList = "Item/Serial : " + _mitem.Value + "/" + _serial;
                                    else
                                        _itemSerialList += ", " + _mitem.Value + "/" + _serial;
                                }
                                else
                                {
                                    if (IsPriceLevelAllowDoAnyStatus == false && _serLst.Tus_itm_stus != txtStatus.Text.Trim())
                                    {
                                        _isInventoryAvailable = false;
                                        if (string.IsNullOrEmpty(_itemSerialList))
                                            _itemSerialList = "Item/Serial/Status : " + _mitem.Value + "/" + _serial + "/" + txtStatus.Text.Trim();
                                        else
                                            _itemSerialList += ", " + _mitem.Value + "/" + _serial + "/" + txtStatus.Text.Trim();

                                    }
                                }

                            }

                            if (_obj != null)
                            {
                                foreach (PriceSerialRef _one in _obj)
                                {
                                    _tempPriceSerialItm.Add(_one);
                                }
                            }
                        }
                    }
                }


                //Check scan qty with the selected qty
                if (_counter != Convert.ToDecimal(txtQty.Text))
                {
                    lblMsg.Text = "Select serial and qty mismatch!";
                    MPEPopup.Show();
                    return;
                }

                //Check the availability with the selected serial (and its status)
                if (_isInventoryAvailable == false && chkDeliveryNow.Checked == false)
                {
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        lblMsg.Text = "Following item/serial does not match the status with invnetory balance in your location; " + _itemSerialList;
                    else
                        lblMsg.Text = "Following item/serial does not having invnetory balance in your location; " + _itemSerialList;
                    MPEPopup.Show();
                    return;
                }
                #endregion

                #region Rooting for adding/checking duplicats for the selected item/serial
                string _item = txtItem.Text;
                string _status = txtStatus.Text;
                string _duplicateSerials = string.Empty;

                foreach (PriceSerialRef _one in _tempPriceSerialItm)
                {

                    txtItem.Text = _item;
                    txtStatus.Text = _status;
                    txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                    txtQty.Text = FormatToQty("1");
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_one.Sars_itm_price));

                    var _duplicate = from _dup in _MainPriceSerial
                                     where _dup.Sars_pb_seq == _one.Sars_pb_seq && _dup.Sars_pbook == _one.Sars_pbook && _dup.Sars_price_lvl == _one.Sars_price_lvl && _dup.Sars_itm_cd == _one.Sars_itm_cd && _dup.Sars_ser_no == _one.Sars_ser_no
                                     select _dup;

                    if (_duplicate.Count() <= 0)
                    {
                        _MainPriceSerial.Add(_one);

                        SSPriceBookPrice = _one.Sars_itm_price;
                        SSPriceBookSequance = _one.Sars_pb_seq.ToString();
                        SSPriceBookItemSequance = "1"; //TODO : Table does not having item line no
                        SSPRomotionType = _one.Sars_price_type;
                        WarrantyRemarks = _one.Sars_warr_remarks;

                        if (chkDeliveryNow.Checked == false)
                        {
                            txtSerialNo.Text = _one.Sars_ser_no;
                            hdnSerialNo.Value = _one.Sars_ser_no;
                            hdnItemCode.Value = _one.Sars_itm_cd;
                        }
                        txtUnitPrice.Focus();
                        txtDiscount.Focus();
                        txtDiscountAmt.Focus();
                        txtVATAmt.Focus();
                        txtTotalAmt.Focus();
                        CalculateItem();
                        AddItem(true);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_duplicateSerials))
                            _duplicateSerials = _one.Sars_ser_no;
                        else
                            _duplicateSerials += ", " + _one.Sars_ser_no;

                    }

                }

                if (!string.IsNullOrEmpty(_duplicateSerials))
                {
                    lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                    MPEPopup.Show();
                    return;
                }
                #endregion

                divPopSerialPriceList.Visible = false;
                divPopPriceItemCombination.Visible = false;
                txtUnitPrice.Focus();

            }
            #endregion

            #region  Confirmation Serialized Price Promotion Item

            if (divPopSerialPriceList.Visible && divPopSerialPriceList.Disabled == true && divPopPriceItemCombination.Visible)
            {
                if (_tempPriceCombinItem != null)
                {
                    if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                    foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                    {
                        _MainPriceCombinItem.Add(_item);
                    }
                }

                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;
            }

            #endregion

            #region Confirmation Non-Serialized Price Level Price
            if (divPopPriceList.Visible && divPopPriceList.Disabled == true && divPopPriceItemCombination.Visible || divPopPriceItemCombination.Visible)
            {
                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(SSPriceBookPrice));
                if (_tempPriceCombinItem != null)
                {
                    if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                    string _duplicateSerials = "";
                    foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                    {
                        var _duplicate = from _list in _MainPriceCombinItem
                                         where _list.Sapc_main_itm_cd == _item.Sapc_main_itm_cd && _list.Sapc_itm_cd == _item.Sapc_itm_cd && _list.Sapc_pb_seq == _item.Sapc_pb_seq && _list.Sapc_price == _item.Sapc_price
                                         select _list;

                        if (_duplicate.Count() <= 0)
                        {
                            _MainPriceCombinItem.Add(_item);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(_duplicateSerials))
                                _duplicateSerials = _item.Sapc_itm_cd;
                            else
                                _duplicateSerials += ", " + _item.Sapc_itm_cd;
                        }
                    }

                    if (!string.IsNullOrEmpty(_duplicateSerials))
                    {
                        lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                        MPEPopup.Show();
                        return;
                    }

                    divPopPriceList.Visible = false;
                    divPopPriceItemCombination.Visible = false;
                    txtUnitPrice.Focus();
                }
            }
            #endregion

        }
        //Cancel 
        protected void btnPopCancel_Click(object sender, EventArgs e)
        {
            if (divPopSerialPriceList.Visible)
            {
                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;
            }

            if (divPopPriceList.Visible || (divPopPriceItemCombination.Visible && divPopPriceList.Visible == false))
            {
                //ATN: Process
                //If user wont confirm the combine items, it will pick the price from selected list and forcus to the unit price txt box
                //if user confirm, then add the combnie item to the temp list and forcus the price to txt box

                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(SSPriceBookPrice));
                divPopPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                txtUnitPrice.Focus();
            }
        }
        //When Select the checkbox of the combine items
        protected void CheckPopPriceListClick(object sender, EventArgs e)
        {
            CheckBox chkBx = sender as CheckBox;

            GridViewRow row = chkBx.NamingContainer as GridViewRow;
            HiddenField _priceTp = row.FindControl("hdnPriceType") as HiddenField;
            HiddenField _priceSeq = row.FindControl("hdnPbSeq") as HiddenField;
            HiddenField _mainItem = row.FindControl("hdnMainItem") as HiddenField;
            HiddenField _isFxQty = row.FindControl("hdnIsFixQty") as HiddenField;
            string _mainSerial = row.Cells[3].Text;

            Int32 _priceType = Convert.ToInt32(_priceTp.Value.ToString());
            Int32 _pbSq = Convert.ToInt32(_priceSeq.Value.ToString());
            string _mItem = Convert.ToString(_mainItem.Value.ToString());
            IsFixQty = Convert.ToBoolean(_isFxQty.Value);

            PriceTypeRef _list = TakePromotion(_priceType);
            SSPRomotionType = _priceType;
            SSPromotionCode = row.Cells[1].Text;

            if (chkBx.Checked)
            {

                if (_list.Sarpt_is_com)
                {
                    _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, _mainSerial);
                    if (_tempPriceCombinItem != null)
                    {
                        gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                        gvPriceItemCombine.DataBind();
                        divPopSerialPriceList.Disabled = true;
                        divPopPriceItemCombination.Visible = true;
                        MPEPopup.Show();
                        return;
                    }
                    else
                    {
                        divPopSerialPriceList.Disabled = false;
                        divPopPriceItemCombination.Visible = false;
                        lblMsg.Text = "There is no such combine items pick";
                        MPEPopup.Show();
                        return;
                    }

                }

                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;

            }
            else
            {
                MPEPopup.Show();
                return;
            }

        }
        //If the promotion is not fix item, user will select the qty as per the pointing system
        protected void CheckPopUpCombineItemQty(object sender, EventArgs e)
        {
            //Update temp object
            TextBox _txt = sender as TextBox;
            if (string.IsNullOrEmpty(_txt.Text)) { MPEPopup.Show(); return; }
            if (!IsNumeric(_txt.Text.Trim(), NumberStyles.Number)) { MPEPopup.Show(); return; }
            if (Convert.ToDecimal(_txt.Text) == 0) { MPEPopup.Show(); return; }

            decimal _assQty = Convert.ToDecimal(gvPriceItemCombine.SelectedDataKey[0].ToString());
            decimal _selQty = Convert.ToDecimal(_txt.Text.Trim());

            if (_assQty < _selQty) { lblMsg.Text = "Allocated Qty exceeds by selected qty!"; MPEPopup.Show(); return; }



            //TODO: need to show popup again in any circumstance
        }
        //Serialized Price List Binding
        protected void gvPopSerialPricePick_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divPopSerialPriceList.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(e.Row.Cells[4].Text.Trim()), 0, false);
                        e.Row.Cells[4].Text = FormatToCurrency(Convert.ToString(UnitPrice));
                    }

                }
            }
        }
        //Consumable Item Price Pick
        protected void gvPopConsumPricePick_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divConsumPricePick.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                    gvPopConsumPricePick,
                                    String.Concat("Select$", e.Row.RowIndex),
                                    true);

                        _count += 1;
                    }

                }
            }
        }
        protected void LoadConsumablePriceList(object sender, EventArgs e)
        {
            decimal _unitPrice = Convert.ToDecimal(gvPopConsumPricePick.SelectedDataKey[0]); //Item
            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_unitPrice)));
            divConsumPricePick.Visible = false;
            txtUnitPrice.Focus();

        }
        //ATN : 
        //if the price level non-serial, it wont allow to select multiple promotions
        //and selected price goes to unit price even not pick combine items
        static int _count = 0;
        //PriceList -Non Serial Binding
        protected void gvPopPricePick_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divPopPriceList.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                    gvPopPricePick,
                                    String.Concat("Select$", e.Row.RowIndex),
                                    true);

                        _count += 1;

                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(e.Row.Cells[2].Text.Trim()), 0, false);
                        e.Row.Cells[2].Text = FormatToCurrency(Convert.ToString(UnitPrice));
                    }
                }
            }
        }
        protected void LoadNonSerializedCombination(object sender, EventArgs e)
        {

            string _item = Convert.ToString(gvPopPricePick.SelectedDataKey[0]); //Item
            int _pbseq = Convert.ToInt32(gvPopPricePick.SelectedDataKey[1]);//Pb Seq No
            IsFixQty = Convert.ToBoolean(gvPopPricePick.SelectedDataKey[2]);//Is Fix Qty
            Int32 _priceType = Convert.ToInt32(gvPopPricePick.SelectedDataKey[3]);//Price Type - combine/Free/Normal
            decimal _unitPrice = Convert.ToDecimal(gvPopPricePick.SelectedDataKey[4]);//Price 
            Int32 _itmLine = Convert.ToInt32(gvPopPricePick.SelectedDataKey[5]);//item line no
            _unitPrice = TaxCalculation(txtItem.Text.Trim(), txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), 0, false);
            String _promoCD = Convert.ToString(gvPopPricePick.SelectedDataKey[6]);

            PriceTypeRef _list = TakePromotion(_priceType);
            if (_list.Sarpt_is_com)
            {
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _item, string.Empty);
                if (_tempPriceCombinItem != null)
                {
                    gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                    gvPriceItemCombine.DataBind();
                    divPopPriceList.Disabled = true;
                    divPopPriceItemCombination.Visible = true;

                    SSPriceBookPrice = _unitPrice;
                    SSPriceBookSequance = _pbseq.ToString();
                    SSPriceBookItemSequance = _itmLine.ToString();
                    SSPromotionCode = _promoCD;
                    SSPRomotionType = _priceType;
                    PriceDetailRef _detail = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_detail != null) WarrantyRemarks = _detail.Sapd_warr_remarks;

                    MPEPopup.Show();
                    return;
                }
                else
                {
                    divPopPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    lblMsg.Text = "There is no such combine items pick";
                    MPEPopup.Show();
                    return;
                }
            }

            SSPriceBookPrice = _unitPrice;
            SSPriceBookSequance = _pbseq.ToString();
            SSPriceBookItemSequance = _itmLine.ToString();
            SSPromotionCode = _promoCD;
            SSPRomotionType = _priceType;
            PriceDetailRef _detail0 = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
            if (_detail0 != null) WarrantyRemarks = _detail0.Sapd_warr_remarks;

            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_unitPrice));
            txtUnitPrice.Focus();
            return;

        }
        //Combine Item Binding
        protected void gvPriceItemCombine_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IsFixQty = true;
                TextBox _txt = (TextBox)e.Row.FindControl("txtPopPriceItmComSelQty");
                if (IsFixQty)
                    _txt.Enabled = false;
                else
                {
                    _txt.Enabled = true;
                    //check for the user selection
                    _txt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPopUpCombineItemQty, ""));
                }

                string _item = e.Row.Cells[0].Text.Trim();
                decimal _qty = Convert.ToDecimal(_txt.Text);

                decimal UnitPrice = TaxCalculation(_item, txtStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(e.Row.Cells[3].Text.Trim()), 0, false);
                e.Row.Cells[3].Text = FormatToCurrency(Convert.ToString(UnitPrice));
                if (!divPopSerialPriceList.Visible)
                {
                    e.Row.Cells[5].Text = FormatToCurrency(Convert.ToString(_qty * Convert.ToDecimal(txtQty.Text)));
                    e.Row.Cells[4].Text = e.Row.Cells[5].Text;
                }


                if (chkDeliveryNow.Checked == false)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                                 gvPriceItemCombine,
                                                 String.Concat("Select$", e.Row.RowIndex),
                                                 true);

                    string _items = e.Row.Cells[0].Text.Trim();
                    if (PriceCombinItemSerialList != null)
                        if (PriceCombinItemSerialList.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus)
                            {
                                var _isPick = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == _items).ToList();
                                if (_isPick != null)
                                    if (_isPick.Count > 0)
                                    {
                                        e.Row.Style.Add("color", "#990000");
                                    }
                            }
                            else
                            {
                                var _isPick = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == _items && x.Tus_itm_stus == txtStatus.Text.Trim()).ToList();
                                if (_isPick != null)
                                    if (_isPick.Count > 0)
                                    {
                                        e.Row.Style.Add("color", "#990000");
                                    }
                            }
                        }

                }

            }
        }
        protected void OnRemoveFromInvoiceItemGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            Int32 _combineLine = Convert.ToInt32(gvInvoiceItem.DataKeys[row_id][10]);

            if (_recieptItem != null)
                if (_recieptItem.Count > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Payment already added!");
                    return;
                }



            if (_MainPriceSerial != null)
                if (_MainPriceSerial.Count > 0)
                {

                    //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                    string _item = (string)gvInvoiceItem.DataKeys[row_id][0];
                    decimal _uRate = (decimal)gvInvoiceItem.DataKeys[row_id][2];
                    string _pbook = (string)gvInvoiceItem.DataKeys[row_id][3];
                    string _level = (string)gvInvoiceItem.DataKeys[row_id][4];

                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                    var _remove = from _list in _tempSerial
                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                  select _list;

                    foreach (PriceSerialRef _single in _remove)
                    {
                        _tempSerial.Remove(_single);
                    }

                    _MainPriceSerial = _tempSerial;
                }

            List<InvoiceItem> _tempList = _invoiceItemList;
            var _promo = (from _pro in _invoiceItemList
                          where _pro.Sad_job_line == _combineLine
                          select _pro).ToList();

            if (_promo.Count() > 0)
            {
                foreach (InvoiceItem code in _promo)
                {
                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                    //_tempList.Remove(code);
                    ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                    InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                }
                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);

            }
            else
            {
                CalculateGrandTotal(Convert.ToDecimal(gvInvoiceItem.DataKeys[row_id][5]), (decimal)gvInvoiceItem.DataKeys[row_id][2], (decimal)gvInvoiceItem.DataKeys[row_id][6], (decimal)gvInvoiceItem.DataKeys[row_id][7], false);
                InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[row_id].Sad_itm_line);
                _tempList.RemoveAt(row_id);

            }

            _invoiceItemList = _tempList;

            Int32 _newLine = 1;
            List<InvoiceItem> _tempLists = _invoiceItemList;
            if (_tempLists != null)
                if (_tempLists.Count > 0)
                {
                    foreach (InvoiceItem _itm in _tempLists)
                    {
                        Int32 _line = _itm.Sad_itm_line;
                        _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                        InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                        ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

                        _newLine += 1;
                    }
                    _lineNo = _newLine - 1;
                }
                else
                {
                    _lineNo = 0;
                }
            else
            {
                _lineNo = 0;
            }

            BindAddItem();
            gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A").ToList(); ;
            gvPopSerial.DataBind();

            //2012/11/12

            if (gvInvoiceItem.Rows.Count > 0)
            {
                txtPriceBook.Enabled = false;
                txtPriceLevel.Enabled = false;
                imgBtnPriceBook.Enabled = false;
                imgBtnPriceLevel.Enabled = false;
            }
            else
            {
                txtPriceBook.Enabled = true;
                txtPriceLevel.Enabled = true;
                imgBtnPriceBook.Enabled = true;
                imgBtnPriceLevel.Enabled = true;

            }

            //
        }
        #endregion

        #region Rooting for Clear Screen
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sales_Module/SalesEntryExchange.aspx");
        }
        #endregion

        #region Rooting for Payment / Loyalty
        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { divCredit.Visible = false; divAdvReceipt.Visible = false; return; }

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

            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid payment type"); return; }
            //If the selected paymode having bank settlement.
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    lblPayCrCardNo.Text = "Card No";
                    //txtPayCrCardNo - lblPayCrCardNo
                    //txtPayCrBank
                    //txtPayCrBranch
                    //txtPayCrCardType
                    //txtPayCrExpiryDate
                    //chkPayCrPromotion
                    txtPayCrCardType.Enabled = true;
                    txtPayCrExpiryDate.Enabled = true;
                    chkPayCrPromotion.Enabled = true;
                }

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
                {
                    lblPayCrCardNo.Text = "Card No";
                    //txtPayCrCardNo - lblPayCrCardNo
                    //txtPayCrBank
                    //txtPayCrBranch
                    //txtPayCrCardType
                    //txtPayCrExpiryDate
                    //chkPayCrPromotion
                    txtPayCrCardType.Enabled = false;
                    txtPayCrExpiryDate.Enabled = false;
                    chkPayCrPromotion.Enabled = false;
                }

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString() || _type.Sapt_cd == "TRA CHEQUE")
                {
                    lblPayCrCardNo.Text = "Cheque No";
                    //txtPayCrCardNo - lblPayCrCardNo
                    //txtPayCrBank
                    //txtPayCrBranch
                    //txtPayCrCardType
                    //txtPayCrExpiryDate
                    //chkPayCrPromotion
                    txtPayCrCardType.Enabled = false;
                    txtPayCrExpiryDate.Enabled = false;
                    chkPayCrPromotion.Enabled = false;
                }


            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                divCredit.Visible = false; divAdvReceipt.Visible = true;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
            {
                divCredit.Visible = false; divAdvReceipt.Visible = false;

            }
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Focus();
        }
        private void CheckBank()
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(txtPayCrBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(txtPayCrBank.Text, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    txtPayCrBank.Text = _bankAccounts.Mbi_cd;

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid bank.");
                    txtPayCrBank.Text = "";
                    txtPayCrBank.Focus();
                    return;
                }
            }

            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, txtInvType.Text, DateTime.Now.Date);
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
        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }
            if (string.IsNullOrEmpty(LabelExRate.Text) || LabelExRate.Text == "Rate not saved yet") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Exchange Rate not saved.<br>Please Contact Accounting Dept."); return; }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount"); return; }
            if (string.IsNullOrEmpty(TextBoxCurAmo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount"); return; }

            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount"); return; }

            if (Math.Round((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text)), 2) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == "TRA CHEQUE")
            {
                if (string.IsNullOrEmpty(txtPayCrBank.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card no"); else  MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the checq no"); txtPayCrCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the document no"); txtPayAdvReceiptNo.Focus(); return; }
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid period");
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
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }

            //TODO: Add converted price
            //2012/11/10
            //CURRANCY CALCULATION
            decimal value = Convert.ToDecimal(TextBoxCurAmo.Text) * Convert.ToDecimal(LabelExRate.Text);
            txtPayAmount.Text = (Convert.ToDecimal(txtPayAmount.Text) - value).ToString();


            //END


            _payAmount = Convert.ToDecimal(value);


            if (_recieptItem.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == "TRA CHEQUE") _cardno = txtPayCrCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString()) _cardno = txtPayAdvReceiptNo.Text;

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

                //2012/11/09

                _item.Sard_anal_1 = DropDownCurrancy.SelectedValue.ToString();
                _item.Sard_anal_3 = Convert.ToDecimal(TextBoxCurAmo.Text);
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(GlbUserComCode, DropDownCurrancy.SelectedValue, DateTime.Now, CurrancyCode);
                if (_exc1 != null)
                {
                    _item.Sard_anal_4 = _exc1.Mer_bnkbuy_rt;
                }

                //

                _recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in _recieptItem
                                 where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
                                 select _dup;
                //if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == txtPayCrCardType.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue == "TRA CHEQUE")
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == txtPayCrBank.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
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

                    if (string.IsNullOrEmpty(txtPayCrPeriod.Text.Trim()))
                        txtPayCrPeriod.Text = "0";

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

                    //2012/11/09

                    _item.Sard_anal_1 = DropDownCurrancy.SelectedValue.ToString();
                    _item.Sard_anal_3 = Convert.ToDecimal(TextBoxCurAmo.Text);
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(GlbUserComCode, DropDownCurrancy.SelectedValue, DateTime.Now, CurrancyCode);
                    if (_exc1 != null)
                    {
                        _item.Sard_anal_4 = _exc1.Mer_bnkbuy_rt;
                    }
                    //

                    _recieptItem.Add(_item);
                }
                else
                {
                    //duplicates
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not add duplicate payments");
                    return;

                }



            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));

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
            LabelPcAmo.Text = "";
            txtPayCrPeriod.Enabled = false;
            TextBoxCurAmo.Text = "";

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

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
        }
        #endregion

        #region Rooting for Cancel Invoice
        private void Cancel()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice no");
                txtInvoiceNo.Focus();
                return;
            }

            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(GlbUserComCode, GlbUserDefProf, string.Empty, txtInvoiceNo.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());
            if (_header.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid invoice no");
                return;
            }

            if (_header[0].Sah_stus == "D" && _header[0].Sah_dt != DateTime.Now.Date)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "This invoice already delivered on " + _header[0].Sah_dt + ". You can not cancel the invoice with delivery order");
                return;
            }


            string _msg = "";
            Int32 _effect = CHNLSVC.Sales.InvoiceCancelation(_header[0], out _msg);

            string Msg = "<script>alert('Successfully Canceled!'); window.location = 'SalesEntryExchange.aspx';</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

        }
        protected void Cancel(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

        #region Rooting for Loyalty
        //private void LoyalityMemeberCheck()
        //{
        //    if (string.IsNullOrEmpty(ddlPayLoyality.SelectedValue))
        //    {
        //        if (string.IsNullOrEmpty(txtPayLoyality.Text)) return;

        //    }

        //    string _cardno = "";
        //    if (!string.IsNullOrEmpty(ddlPayLoyality.SelectedValue))
        //    {
        //        _cardno = ddlPayLoyality.SelectedValue.ToString();
        //    }
        //    else if (!string.IsNullOrEmpty(txtPayLoyality.Text))
        //    {
        //        _cardno = txtPayLoyality.Text;
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    List<LoyaltyMemeber> _loyalityMemeber = CHNLSVC.Sales.GetCustomerLoyality(txtCustomer.Text, _cardno, DateTime.Now.Date);

        //    if (_loyalityMemeber.Count <= 0) return;
        //    LoyaltyMemeber _member = _loyalityMemeber[0];

        //    if (_invoiceItemList.Count <= 0) return;
        //    foreach (InvoiceItem _itm in _invoiceItemList)
        //    {
        //        List<LoyaltyPointDiscount> _discount = CHNLSVC.Sales.GetLoyaltyDiscount(_member.Salcm_loty_tp, "COM", GlbUserComCode, DateTime.Now.Date, _member.Salcm_bal_pt, _itm.Sad_pbook, _itm.Sad_pb_lvl);

        //        if (_discount == null) return;
        //        List<LoyaltyPriorityCode> _priority = CHNLSVC.Sales.GetLoyaltyPriority(GlbUserComCode);

        //        foreach (LoyaltyPriorityCode _pro in _priority)
        //        {
        //            foreach (LoyaltyPointDiscount _dis in _discount)
        //            {
        //                var _promo = from _cm in _discount
        //                             where _cm.Saldi_promo == _itm.Sad_promo_cd
        //                             select _cm;

        //                var _item = from _it in _discount
        //                            where _it.Saldi_itm == _itm.Sad_itm_cd
        //                            select _it;

        //                //var _;

        //            }
        //        }



        //    }
        //}

        //protected void LoyaltyMemberCalculate(object sender, EventArgs e)
        //{
        //    LoyalityMemeberCheck();
        //}
        #endregion

        #region Rooting for Re-Print Invoice
        protected void Print(object sender, EventArgs e)
        {
            Button _btn = (Button)sender;
            string _btnname = _btn.ID;



            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            // Response.Redirect("~/Test/Print.aspx");

            Dictionary<string, string> _lst = GetInvoiceSerialnWarranty(txtInvoiceNo.Text.Trim());

            foreach (KeyValuePair<string, string> _d in _lst)
            {
                GlbReportSerialList = _d.Key.Replace("N/A", "");
                GlbReportWarrantyList = _d.Value.Replace("N/A", "");
            }


            if (_btnname == "btnPrint")
            {
                //GlbDocNosList = txtInvoiceNo.Text.Trim();
                //GlbReportPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                //GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                //GlbReportName = "Invoice";

                //GlbMainPage = "~/Sales_Module/SalesEntry.aspx";
                //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");

                MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");


                GlbDocNosList = txtInvoiceNo.Text.Trim();
                if (_itm.Mbe_sub_tp != "C.")
                {
                    GlbReportPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
                }
                else
                {
                    GlbReportPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";
                }
                GlbReportName = "Invoice";

                GlbMainPage = "~/Sales_Module/SalesEntryExchange.aspx";
                string Msg = "window.open('../Test/PdfPrint.aspx',  '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "invoiceRePrint", Msg, true);
            }
            else if (_btnname == "btnDOPrint")
            {
                List<string> __do = CHNLSVC.Sales.DeliveryOrderNoByInvoice(txtInvoiceNo.Text.Trim());
                string _dolist = string.Empty;
                if (__do != null)
                    if (__do.Count > 0)
                    {
                        foreach (string _n in __do)
                        {
                            if (string.IsNullOrEmpty(_dolist))
                            {
                                _dolist = _n;
                            }
                            else
                            {
                                _dolist = "," + _n;
                            }

                        }

                        GlbDocNosList = _dolist.Trim();

                        GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                        GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";

                        GlbMainPage = "~/Inventory_Module/DeliveryOrder.aspx";
                        Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");

                        string Msg = "window.open('../Reports_Module/Inv_Rep/Print.aspx',  '_blank');";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "invoiceDOPrint", Msg, true);


                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "There is no delivery order");
                        return;
                    }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "There is no delivery order");
                    return;
                }

            }


        }
        private Dictionary<string, string> GetInvoiceSerialnWarranty(string _invoiceno)
        {
            StringBuilder _serial = new StringBuilder();
            StringBuilder _warranty = new StringBuilder();
            Dictionary<string, string> _list = new Dictionary<string, string>();

            List<ReptPickSerials> _advSerial = CHNLSVC.Inventory.GetInvoiceAdvanceReceiptSerial(GlbUserComCode, _invoiceno);
            List<InventorySerialN> _invSerial = CHNLSVC.Inventory.GetDeliveredSerialDetail(GlbUserComCode, _invoiceno);

            if (_advSerial != null)
                if (_advSerial.Count > 0)
                {
                    foreach (ReptPickSerials _x in _advSerial)
                    {
                        if (string.IsNullOrEmpty(_serial.ToString()))
                        {
                            _serial.Append(_x.Tus_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Tus_ser_2);
                            _warranty.Append(_x.Tus_warr_no);
                        }
                        else
                        {
                            _serial.Append(", " + _x.Tus_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Tus_ser_2);
                            _warranty.Append(", " + _x.Tus_warr_no);
                        }

                    }
                }

            if (_invSerial != null)
                if (_invSerial.Count > 0)
                {
                    foreach (InventorySerialN _x in _invSerial)
                    {
                        if (string.IsNullOrEmpty(_serial.ToString()))
                        {
                            _serial.Append(_x.Ins_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Ins_ser_2);
                            _warranty.Append(_x.Ins_warr_no);

                        }
                        else
                        {
                            _serial.Append(", " + _x.Ins_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Ins_ser_2);
                            _warranty.Append(", " + _x.Ins_warr_no);

                        }
                    }
                }

            _list.Add(_serial.ToString(), _warranty.ToString());
            return _list;


        }
        #endregion

        #region Apply Discount & Get Confirmation for the payment
        protected List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount
        {
            get { return (List<CashGeneralEntiryDiscountDef>)Session["_CashGeneralEntiryDiscount"]; }
            set { Session["_CashGeneralEntiryDiscount"] = value; }
        }
        protected void BindGeneralDiscount()
        {
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
            gvDisItem.DataBind();

        }
        protected void BindInvoiceItemToDiscountItem(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCustomer.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the customer");
                return;
            }

            if (_invoiceItemList != null)
                if (_invoiceItemList.Count > 0)
                {
                    _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                    foreach (InvoiceItem _i in _invoiceItemList)
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

                    gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                    gvDisItem.DataBind();


                }

            pnlDisItem.Enabled = false;
            ddlDisCategory.SelectedIndex = 0;
            MPEDiscount.Show();

        }
        protected void Category_onChange(object sender, EventArgs e)
        {
            if (ddlDisCategory.SelectedValue.ToString() == "Customer")
            {
                pnlDisItem.Enabled = false;
                txtDisAmount.Enabled = true;
                MPEDiscount.Show();
            }
            else
            {
                pnlDisItem.Enabled = true;
                txtDisAmount.Enabled = false;
                MPEDiscount.Show();
            }
        }
        protected void SaveDiscountRequest(object sender, EventArgs e)
        {

            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();


            if (gvDisItem.Rows.Count > 0)
            {

                string _customer = txtCustomer.Text;
                string _customerReq = _customer + "REQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
                bool _isSuccessful = true;


                foreach (GridViewRow _r in gvDisItem.Rows)
                {

                    CheckBox _chk = (CheckBox)_r.FindControl("chkDisSelect");
                    if (_chk.Checked)
                    {
                        string _item = _r.Cells[1].Text; //item code
                        DropDownList _type = (DropDownList)_r.FindControl("ddlDisType");
                        TextBox _amt = (TextBox)_r.FindControl("txtDisItmAmount");
                        string _pricebook = _r.Cells[4].Text;
                        string _pricelvl = _r.Cells[5].Text;

                        if (string.IsNullOrEmpty(_amt.Text.Trim()))
                        {
                            lblDisMsg.Text = "Please select the amount in " + _item;
                            MPEDiscount.Show();
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(_amt.Text.Trim(), NumberStyles.Currency))
                        {
                            lblDisMsg.Text = "Please select the valid amount in " + _item;
                            MPEDiscount.Show();
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt.Text.Trim()) <= 0)
                        {
                            lblDisMsg.Text = "Please select the valid amount in " + _item;
                            MPEDiscount.Show();
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(_amt.Text.Trim()) > 100 && _type.SelectedValue.Contains("Rate"))
                        {
                            lblDisMsg.Text = "Rate can not be exceed the 100% in " + _item;
                            MPEDiscount.Show();
                            _isSuccessful = false;
                            break;
                        }

                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = GlbUserComCode;
                        _discount.Sgdd_cre_by = GlbUserName;
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;

                        if (_type.SelectedValue.Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(_amt.Text.Trim());
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(_amt.Text.Trim());

                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtDate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = GlbUserName;
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = txtInvType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtDate.Text);
                        _list.Add(_discount);

                    }

                }

                if (_isSuccessful)
                {

                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscount(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), GlbUserComCode, GlbUserDefProf, _customerReq, GlbUserName, _list);
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "<script>alert('Successfully Saved! Document No : " + _customerReq + "'); </script>";

                    }
                    else
                    {
                        Msg = "<script>alert('Document not processed! please try again'); </script>";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }

            }


        }
        #endregion

        #region Rooting for Inventory Combine Item/Serial Scan
        protected void GetItemComponent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                //Load Inventory Components
                _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text);
                if (_masterItemComponent.Count <= 0)
                {
                    //Load Combine Items
                }

            }
        }
        protected void gvComItemSerialBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                                   gvPopComItem,
                                                   String.Concat("Select$", e.Row.RowIndex),
                                                   true);
                Label _item = (Label)e.Row.FindControl("lblPopComItemSerItemCode");


                if (InventoryCombinItemSerialList != null)
                    if (InventoryCombinItemSerialList.Count > 0)
                    {
                        if (IsPriceLevelAllowDoAnyStatus)
                        {
                            var _isPick = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == _item.Text.Trim()).ToList();
                            if (_isPick != null)
                                if (_isPick.Count > 0)
                                {
                                    e.Row.Style.Add("color", "#990000");
                                }
                        }
                        else
                        {
                            var _isPick = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == _item.Text.Trim() && x.Tus_itm_stus == e.Row.Cells[2].Text.Trim()).ToList();
                            if (_isPick != null)
                                if (_isPick.Count > 0)
                                {
                                    e.Row.Style.Add("color", "#990000");
                                }
                        }
                    }
            }
        }
        private void LoadSelectedItemSerialForComItemSerialGv(string _item, string _status, decimal _qty)
        {
            List<ReptPickSerials> _lst = null;
            //Load serials
            //EVEN THIS CALLED NON-SERIALIZED, CAN USE FOR SERIALIZED ITEM
            if (IsPriceLevelAllowDoAnyStatus)
                _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
            else
                _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);

            foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

            gvPopComItemSerial.DataSource = _lst;
            gvPopComItemSerial.DataBind();
            MPEComItemSerial.Show();
        }
        protected void LoadSelectedItemSerialForComItemSerialGv(object sender, EventArgs e)
        {
            hdnComItemSerQty.Value = "0";
            string _status = Convert.ToString(gvPopComItem.SelectedDataKey[0]);
            decimal _qty = Convert.ToDecimal(gvPopComItem.SelectedDataKey[1]);
            Label _item = (Label)((GridView)sender).SelectedRow.FindControl("lblPopComItemSerItemCode");
            hdnComItemSerQty.Value = Convert.ToString(_qty);
            LoadSelectedItemSerialForComItemSerialGv(_item.Text.Trim(), _status, _qty);
        }
        protected void gvComItemSerialWithSerialOnBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (InventoryCombinItemSerialList != null)
                    if (InventoryCombinItemSerialList.Count > 0)
                    {
                        var _isPick = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == e.Row.Cells[1].Text.Trim() && x.Tus_ser_1.Trim() == e.Row.Cells[2].Text.Trim()).ToList();
                        if (_isPick != null)
                            if (_isPick.Count > 0)
                                ((CheckBox)e.Row.FindControl("chkPopComItemSerSelect")).Checked = true;
                    }
            }
        }
        protected void AddComItemSerialToList(object sender, EventArgs e)
        {
            if (gvPopComItemSerial.Rows.Count > 0)
            {
                decimal _serialcount = 0;
                foreach (GridViewRow _r in gvPopComItemSerial.Rows)
                {
                    CheckBox _chk = (CheckBox)_r.FindControl("chkPopComItemSerSelect");
                    if (_chk.Checked)
                        _serialcount += 1;
                }

                if (_serialcount != Convert.ToDecimal(hdnComItemSerQty.Value))
                {
                    lblPopComItemSerSerMsg.Text = "Qty and the selected serials mismatch";
                    MPEComItemSerial.Show();
                    return;
                }


                if (_serialcount > Convert.ToDecimal(hdnComItemSerQty.Value))
                {
                    lblPopComItemSerSerMsg.Text = "Qty and the selected serials mismatch";
                    MPEComItemSerial.Show();
                    return;
                }

                foreach (GridViewRow _r in gvPopComItemSerial.Rows)
                {
                    CheckBox _chk = (CheckBox)_r.FindControl("chkPopComItemSerSelect");
                    if (_chk.Checked)
                    {
                        string _item = Convert.ToString(_r.Cells[1].Text);
                        string _serial = Convert.ToString(_r.Cells[2].Text);

                        ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, _item, _serial);
                        if (_serLst != null)
                            if (_serLst.Tus_ser_1 != null || !string.IsNullOrEmpty(_serLst.Tus_ser_1))
                            {
                                if (InventoryCombinItemSerialList != null)
                                    if (InventoryCombinItemSerialList.Count > 0)
                                    {
                                        var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _serLst.Tus_itm_cd && x.Tus_ser_1 == _serLst.Tus_ser_1).ToList();
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                            {
                                                lblPopComItemSerSerMsg.Text = "Serial duplicating!";
                                                MPEComItemSerial.Show();
                                                return;
                                            }
                                            else
                                                InventoryCombinItemSerialList.Add(_serLst);
                                        else
                                            InventoryCombinItemSerialList.Add(_serLst);
                                    }
                                    else
                                    {
                                        InventoryCombinItemSerialList.Add(_serLst);
                                    }
                                else
                                {
                                    InventoryCombinItemSerialList.Add(_serLst);
                                }
                            }
                    }
                }
            }
            lblPopComItemSerSerMsg.Text = "";
            List<ReptPickSerials> _lst = new List<ReptPickSerials>();
            gvPopComItemSerial.DataSource = _lst;
            gvPopComItemSerial.DataBind();
            MPEComItemSerial.Show();
        }
        protected void ConfirmComItemSerialWithQty(object sender, EventArgs e)
        {
            if (InventoryCombinItemSerialList != null)
                if (InventoryCombinItemSerialList.Count > 0)
                {
                    foreach (GridViewRow _r in gvPopComItem.Rows)
                    {
                        string _item = ((Label)_r.FindControl("lblPopComItemSerItemCode")).Text;
                        decimal _qty = Convert.ToDecimal(_r.Cells[3].Text);

                        var _serCount = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Count();
                        if (_serCount > 0)
                        {
                            if (Convert.ToDecimal(_serCount) != _qty)
                            {
                                lblComItemSerMsg.Text = "Scan Serial and the qty is mismatching";
                                MPEComItemSerial.Show();
                                return;
                            }
                        }
                        else
                        {
                            lblComItemSerMsg.Text = "Scan Serial and the qty is mismatching";
                            MPEComItemSerial.Show();
                            return;
                        }
                    }
                    AddItem(false);
                }
            lblComItemSerMsg.Text = "";
        }
        protected void ConfirmComItemSerialCancel(object sender, EventArgs e)
        {
            InventoryCombinItemSerialList = new List<ReptPickSerials>();

        }
        protected void OnRemoveFromInvoiceItemSerialGrid(object sender, GridViewDeleteEventArgs e)
        {

            if (_recieptItem != null)
                if (_recieptItem.Count > 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Payment already added!"); return; }

            if (ScanSerialList != null)
                if (ScanSerialList.Count > 0)
                {
                    int row_id = e.RowIndex;
                    string _item = Convert.ToString(gvPopSerial.DataKeys[row_id][0]);
                    string _comline = Convert.ToString(gvPopSerial.DataKeys[row_id][1]);
                    Int32 _combineLine; if (string.IsNullOrEmpty(_comline)) _combineLine = -1; else _combineLine = Convert.ToInt32(gvPopSerial.DataKeys[row_id][1]);
                    decimal uPrice = Convert.ToDecimal(gvPopSerial.DataKeys[row_id][2]);
                    Int32 _invLine = Convert.ToInt32(gvPopSerial.DataKeys[row_id][3]);
                    string _combineStatus = Convert.ToString(gvPopSerial.DataKeys[row_id][4]);
                    string _serialno = Convert.ToString(gvPopSerial.DataKeys[row_id][5]);


                    //chk combineline in serial list, if -1 it an idividaul item otherwise combine item
                    //if null -> get the invoiceitem from invoiceline,chk qty -> 
                    //if qty=1, balance total,remove item,remove serial from serialno,remove invoiceserial by serialno
                    //if qty>1, get current values,assign new value,balance total,balance item,remove serial from serialno,remove invoiceserial by serialno
                    //
                    //
                    //if combine item -> take serial list from combine line, get invoice data from invoiceline, check qty ->
                    //if qty=1, balance total,balance item
                    //if qty>0,get current value,assign new value,balance total,balance item
                    //after all, remove serial list from combine line,remove invoiceserial from combineline 
                    //

                    if (_combineLine == -1)
                    {
                        var _invoicelst = _invoiceItemList.Where(x => x.Sad_itm_line == _invLine).ToList();
                        if (_invoicelst != null)
                            if (_invoicelst.Count > 0)
                            {
                                foreach (InvoiceItem _itm in _invoicelst)
                                {
                                    if (_itm.Sad_qty == 1)
                                    {
                                        CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                        _invoiceItemList.Remove(_itm);
                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                    }
                                    else
                                    {

                                        InvoiceItem _myItem = new InvoiceItem();
                                        _myItem = _itm;

                                        decimal o_qty = _itm.Sad_qty;
                                        decimal o_unitprice = _itm.Sad_unit_rt;
                                        decimal o_unitamount = _itm.Sad_unit_amt;
                                        decimal o_tax = _itm.Sad_itm_tax_amt;
                                        decimal o_disamount = _itm.Sad_disc_amt;
                                        decimal o_disrate = _itm.Sad_disc_rt;


                                        decimal n_qty = 0;
                                        decimal n_unitprice = 0;
                                        decimal n_unitamount = 0;
                                        decimal n_tax = 0;
                                        decimal n_disamount = 0;
                                        decimal n_disrate = 0;
                                        decimal n_totalAmount = 0;

                                        n_qty = _itm.Sad_qty - 1;
                                        n_unitprice = _itm.Sad_unit_rt;
                                        n_unitamount = n_qty * n_unitprice;
                                        n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                        n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                        n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                        n_totalAmount = n_unitamount + n_tax - n_disamount;

                                        _itm.Sad_qty = n_qty;
                                        _itm.Sad_unit_amt = n_unitamount;
                                        _itm.Sad_itm_tax_amt = n_tax;
                                        _itm.Sad_disc_amt = n_disamount;
                                        _itm.Sad_disc_rt = n_disrate;
                                        _itm.Sad_tot_amt = n_totalAmount;


                                        CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                        _invoiceItemList.Remove(_myItem);

                                        CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                        _invoiceItemList.Add(_itm);

                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);

                                    }
                                }
                            }

                    }
                    else
                    {
                        var _serLst = ScanSerialList.Where(x => x.Tus_serial_id == Convert.ToString(_combineLine)).ToList();
                        if (_serLst != null)
                            if (_serLst.Count > 0)
                            {

                                foreach (ReptPickSerials _itms in _serLst)
                                {
                                    Int32 _invoiceline = _itms.Tus_base_itm_line;
                                    var _invoiveLst = _invoiceItemList.Where(x => x.Sad_itm_line == _invoiceline).ToList();
                                    if (_invoiveLst != null)
                                        if (_invoiveLst.Count > 0)
                                        {
                                            foreach (InvoiceItem _itm in _invoiveLst)
                                            {
                                                if (_itm.Sad_qty == 1)
                                                {
                                                    CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                                    _invoiceItemList.Remove(_itm);

                                                }
                                                else
                                                {

                                                    InvoiceItem _myItem = new InvoiceItem();
                                                    _myItem = _itm;

                                                    decimal o_qty = _itm.Sad_qty;
                                                    decimal o_unitprice = _itm.Sad_unit_rt;
                                                    decimal o_unitamount = _itm.Sad_unit_amt;
                                                    decimal o_tax = _itm.Sad_itm_tax_amt;
                                                    decimal o_disamount = _itm.Sad_disc_amt;
                                                    decimal o_disrate = _itm.Sad_disc_rt;


                                                    decimal n_qty = 0;
                                                    decimal n_unitprice = 0;
                                                    decimal n_unitamount = 0;
                                                    decimal n_tax = 0;
                                                    decimal n_disamount = 0;
                                                    decimal n_disrate = 0;
                                                    decimal n_totalAmount = 0;

                                                    n_qty = _itm.Sad_qty - 1;
                                                    n_unitprice = _itm.Sad_unit_rt;
                                                    n_unitamount = n_qty * n_unitprice;
                                                    n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                                    n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                                    n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                                    n_totalAmount = n_unitamount + n_tax - n_disamount;

                                                    _itm.Sad_qty = n_qty;
                                                    _itm.Sad_unit_amt = n_unitamount;
                                                    _itm.Sad_itm_tax_amt = n_tax;
                                                    _itm.Sad_disc_amt = n_disamount;
                                                    _itm.Sad_disc_rt = n_disrate;
                                                    _itm.Sad_tot_amt = n_totalAmount;


                                                    CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                                    _invoiceItemList.Remove(_myItem);

                                                    CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                                    _invoiceItemList.Add(_itm);
                                                }
                                            }
                                        }
                                }

                                ScanSerialList.RemoveAll(x => x.Tus_serial_id == Convert.ToString(_combineLine));
                                InvoiceSerialList.RemoveAll(x => x.Sap_ser_2 == Convert.ToString(_combineLine));
                            }
                    }

                    Int32 _newLine = 1;
                    List<InvoiceItem> _tempLists = _invoiceItemList;
                    if (_tempLists != null)
                        if (_tempLists.Count > 0)
                        {
                            foreach (InvoiceItem _itm in _tempLists)
                            {
                                Int32 _line = _itm.Sad_itm_line;
                                _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                                InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

                                _newLine += 1;


                            }
                            _lineNo = _newLine - 1;
                        }
                        else
                        {
                            _lineNo = 0;
                        }
                    else
                    {
                        _lineNo = 0;
                    }



                    gvPopSerial.DataSource = ScanSerialList;//.Where(X => X.Tus_ser_1 != "N/A").ToList(); ;
                    gvPopSerial.DataBind();

                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();

                    if (ScanSerialList != null)
                        if (ScanSerialList.Count > 0)
                            MPESerial.Show();
                }
        }
        #endregion

        #region Rooting for Price Combine Item/Serial Pick
        protected void LoadSelectedpPriceComSerialForPriceComItemSerialGv(object sender, EventArgs e)
        {
            hdnComItemSerQty.Value = "0";
            string _status = txtStatus.Text.Trim();
            decimal _qty = Convert.ToDecimal(gvPriceItemCombine.SelectedDataKey[0]);
            string _item = Convert.ToString(gvPriceItemCombine.SelectedDataKey[6]);
            hdnComItemSerQty.Value = Convert.ToString(_qty);
            LoadSelectedItemSerialForPriceComItemSerialGv(_item.Trim(), _status, _qty);
        }
        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty)
        {
            List<ReptPickSerials> _lst = null;
            //Load serials
            //EVEN THIS CALLED NON-SERIALIZED, CAN USE FOR SERIALIZED ITEM
            MasterItem _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 == 1)
            {
                if (IsPriceLevelAllowDoAnyStatus)
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
                else
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);

                foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                    _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

                _lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);

                gvPopPriceComSerPick.DataSource = _lst;
                gvPopPriceComSerPick.DataBind();
            }
            else
            {
                lblPopPriceComSerMsg.Text = "No need to pick non serialized item";
            }
            MPEPopup.Show();
        }
        protected void gvPriceComItemSerialWithSerialOnBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (PriceCombinItemSerialList != null)
                    if (PriceCombinItemSerialList.Count > 0)
                    {
                        var _isPick = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd.Trim() == e.Row.Cells[1].Text.Trim() && x.Tus_ser_1.Trim() == e.Row.Cells[2].Text.Trim()).ToList();
                        if (_isPick != null)
                            if (_isPick.Count > 0)
                                ((CheckBox)e.Row.FindControl("chkPopPriceComSerSelect")).Checked = true;
                    }
            }
        }
        protected void AddPriceComItemSerialToList(object sender, EventArgs e)
        {
            if (gvPopPriceComSerPick.Rows.Count > 0)
            {
                decimal _serialcount = 0;
                foreach (GridViewRow _r in gvPopPriceComSerPick.Rows)
                {
                    CheckBox _chk = (CheckBox)_r.FindControl("chkPopPriceComSerSelect");
                    if (_chk.Checked)
                        _serialcount += 1;
                }

                if (_serialcount != Convert.ToDecimal(hdnComItemSerQty.Value))
                {
                    lblPopPriceComSerMsg.Text = "Qty and the selected serials mismatch";
                    MPEPopup.Show();
                    return;
                }


                if (_serialcount > Convert.ToDecimal(hdnComItemSerQty.Value))
                {
                    lblPopPriceComSerMsg.Text = "Qty and the selected serials mismatch";
                    MPEPopup.Show();
                    return;
                }

                foreach (GridViewRow _r in gvPopPriceComSerPick.Rows)
                {
                    CheckBox _chk = (CheckBox)_r.FindControl("chkPopPriceComSerSelect");
                    if (_chk.Checked)
                    {
                        string _item = Convert.ToString(_r.Cells[1].Text);
                        string _serial = Convert.ToString(_r.Cells[2].Text);

                        ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, _item, _serial);
                        if (_serLst != null)
                            if (_serLst.Tus_ser_1 != null || !string.IsNullOrEmpty(_serLst.Tus_ser_1))
                            {
                                if (PriceCombinItemSerialList != null)
                                    if (PriceCombinItemSerialList.Count > 0)
                                    {
                                        var _dup = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _serLst.Tus_itm_cd && x.Tus_ser_1 == _serLst.Tus_ser_1).ToList();
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                            {
                                                lblPopPriceComSerMsg.Text = "Serial duplicating!";
                                                MPEPopup.Show();
                                                return;
                                            }
                                            else
                                                PriceCombinItemSerialList.Add(_serLst);
                                        else
                                            PriceCombinItemSerialList.Add(_serLst);
                                    }
                                    else
                                    {
                                        PriceCombinItemSerialList.Add(_serLst);
                                    }
                                else
                                {
                                    PriceCombinItemSerialList.Add(_serLst);
                                }
                            }
                    }
                }
            }
            lblPopPriceComSerMsg.Text = "";
            List<ReptPickSerials> _lst = new List<ReptPickSerials>();
            gvPopPriceComSerPick.DataSource = _lst;
            gvPopPriceComSerPick.DataBind();
            MPEPopup.Show();
        }
        #endregion

        #region Rooting for prerequities for save/hold the invoice
        private bool IsInvoiceItemNSerialListTally(out string _Item)
        {
            bool _tally = true;
            string _errorItem = string.Empty;
            if (IsPriceLevelAllowDoAnyStatus)
            {
                var _itemswitouthstatus = (from _l in _invoiceItemList group _l by new { _l.Sad_itm_cd } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();

                foreach (var _itm in _itemswitouthstatus)
                {
                    string _item = _itm.Sad_itm_cd;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;

                    }

                }

            }
            else
            {
                var _itemswithstatus = (from _l in _invoiceItemList group _l by new { _l.Sad_itm_cd, _l.Sad_itm_stus } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_itm_stus = _i.Key.Sad_itm_stus, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();

                foreach (var _itm in _itemswithstatus)
                {
                    string _item = _itm.Sad_itm_cd;
                    string _status = _itm.Sad_itm_stus;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item && _n.Tus_itm_stus == _status select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;

                    }
                }

            }

            _Item = _errorItem;
            return _tally;
        }
        private bool IsInventoryBalanceNInvoiceItemTally(out string _NotTallyList)
        {
            bool availability = true;
            MasterItem _itm = null;
            string _itemList = string.Empty;

            var _modifySerialList = (from _l in ScanSerialList group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus, _l.Tus_ser_1 } into _new select new { Tus_itm_cd = _new.Key.Tus_itm_cd, Tus_itm_stus = _new.Key.Tus_itm_stus, Tus_ser_1 = _new.Key.Tus_ser_1, Tus_qty = _new.Sum(p => p.Tus_qty) }).ToList();

            foreach (var _serial in _modifySerialList)
            {
                _itm = null;
                string _item = _serial.Tus_itm_cd;
                string _serialno = _serial.Tus_ser_1;
                string _status = _serial.Tus_itm_stus;
                decimal _qty = _serial.Tus_qty;
                _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

                //Serialized Item
                if (_itm.Mi_is_ser1 == 1)
                {
                    ReptPickSerials _chk = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, string.Empty, _item, _serialno);
                    if (string.IsNullOrEmpty(_chk.Tus_com)) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else if (IsPriceLevelAllowDoAnyStatus == false)
                        if (_chk.Tus_itm_stus != _status) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                //Non-serialized item but have serial line
                else if (_itm.Mi_is_ser1 == 0)
                {
                    List<ReptPickSerials> _chk;//= CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, _item, _qty);
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, _item, _status, _qty);
                    else
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, _item, string.Empty, _qty);

                    if (_chk != null)
                        if (_chk.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _chk where i.Tus_itm_cd == _item && i.Tus_itm_stus == _status select i.Tus_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                                if (_chk.Count() < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                //Non_serialized item, no serial line
                else if (_itm.Mi_is_ser1 == -1)
                {
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, _item, string.Empty);

                    if (_inventoryLocation != null)
                        if (_inventoryLocation.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _inventoryLocation where i.Inl_itm_cd == _item && i.Inl_itm_stus == _status select i.Inl_free_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                                if (_inventoryLocation.Count < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
            }
            _NotTallyList = _itemList;
            return availability;

        }
        #endregion

        protected void SaveInvoice(object sender, EventArgs e)//--------------------------------------------------------- save --------------------------------------------
        {
            //2012/11/12
            decimal temamo;
            if (decimal.TryParse(lblPayPaid.Text, out temamo))
            {
                if (decimal.TryParse(lblPayBalance.Text, out temamo))
                {
                    if (Convert.ToDecimal(lblPayBalance.Text) > 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not save with balance");
                        return;
                    }
                }
            }

            if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, Convert.ToDateTime(txtDate.Text).ToString("dd/MMM/yyyy"), imgBtnDate, lblDispalyInfor) == false)
            {
                if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Back date not allow for selected date!");
                    return;
                }
            }

            MasterCompany _com = CHNLSVC.General.GetCompByCode(GlbUserComCode);

            MasterExchangeRate _ex = CHNLSVC.Sales.GetExchangeRate(GlbUserComCode, CurrancyCode, DateTime.Now, _com.Mc_cur_cd);

            if (_ex == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Company Exchange Rate not set.<br>" + CurrancyCode + " to " + _com.Mc_cur_cd);
                return;
            }


            //



            if (string.IsNullOrEmpty(GlbUserComCode))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Session was expired. Please re-log to the system! ");
                return;
            }

            bool _isHoldInvoiceProcess = false;
            InvoiceHeader _hdr;

            #region Check the invoice no for edit
            _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
            if (_hdr != null && _hdr.Sah_stus != "H")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not edit saved invoice");
                return;
            }
            #endregion

            #region Get to know whether recalled invoice is Hold invoice & tag as hold
            if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
            if (_isHoldInvoiceProcess && chkDeliveryNow.Checked == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not use 'Deliver Now!' option for hold invoice");
                return;
            }
            #endregion

            #region Check for fulfilment for the save process
            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type");
                txtInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the customer");
                txtCustomer.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price book");
                txtPriceBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceLevel.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the price level");
                txtPriceLevel.Focus();
                return;
            }

            if (_invoiceItemList.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the items for invoice");
                return;
            }

            if (string.IsNullOrEmpty(txtExecutive.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the executive code");
                txtExecutive.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtCurrency.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the currency code");
            //    txtCurrency.Focus();
            //    return;
            //}

            if (_masterProfitCenter.Mpc_check_pay && _recieptItem.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please enter the payment detail");
                return;
            }
            if (string.IsNullOrEmpty(txtCusName.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please enter the customer");
                return;
            }

            #endregion

            #region Check for payment if the invoice tyoe is cash
            if (txtInvType.Text == "CS")
                if (_recieptItem != null)
                    if (_recieptItem.Count <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please enter the payment detail");
                        return;
                    }
            #endregion

            #region Check for availability of the invoice prefix
            string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(GlbUserComCode, GlbUserDefProf, txtInvType.Text);

            if (string.IsNullOrEmpty(_invoicePrefix))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts dept.");
                return;
            }
            #endregion

            Int32 _count = 1;
            #region Assigning new receipt line no to recipt items
            _recieptItem.ForEach(x => x.Sard_line_no = _count++);
            #endregion
            _count = 1;
            #region Assign new invoice item line for all the objects
            List<InvoiceItem> _linedInvoiceItem = new List<InvoiceItem>();
            foreach (InvoiceItem _item in _invoiceItemList)
            {
                Int32 _currentLine = _item.Sad_itm_line;
                if (ScanSerialList != null)
                    if (ScanSerialList.Count > 0)
                        ScanSerialList.Where(x => x.Tus_base_itm_line == _currentLine).ToList().ForEach(x => x.Tus_base_itm_line = _count);
                if (InvoiceSerialList != null)
                    if (InvoiceSerialList.Count > 0)
                        InvoiceSerialList.Where(x => x.Sap_itm_line == _currentLine).ToList().ForEach(x => x.Sap_itm_line = _count);
                _item.Sad_itm_line = _count;
                _linedInvoiceItem.Add(_item);
                _count += 1;
            }
            _linedInvoiceItem.ForEach(x => x.Sad_isapp = true);
            _linedInvoiceItem.ForEach(x => x.Sad_iscovernote = true);
            _invoiceItemList = new List<InvoiceItem>();
            _invoiceItemList = _linedInvoiceItem;
            #endregion

            #region Deliver Now! - Check for serialied item qty with it's scan serial count
            if (chkDeliveryNow.Checked == false)
            {
                string _itmList = string.Empty;
                bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);
                if (_isqtyNserialOk == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invoice quantites and the serials are mismatched. Please check the following item for its serials. Item List : " + _itmList);
                    return;
                }
            }
            #endregion

            #region Deliver Now! - Check the Inventory Availability
            if (chkDeliveryNow.Checked == false)
            {
                string _nottallylist = string.Empty;
                bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                if (_isTallywithinventory == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Following item does not having inventory balance for raise delivery order; " + _nottallylist);
                    return;
                }
            }
            #endregion

            MasterBusinessEntity _entity = new MasterBusinessEntity();
            InvoiceHeader _invheader = new InvoiceHeader();
            RecieptHeader _recHeader = new RecieptHeader();
            InventoryHeader invHdr = new InventoryHeader();



            #region Showroom manager having a company, and its to take the company to GRN in the DO stage
            bool _isCustomerHasCompany = false;
            string _customerCompany = string.Empty;
            string _customerLocation = string.Empty;

            _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            if (_entity != null)
                if (_entity.Mbe_cd != null)
                    if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                    { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }

            #endregion

            #region Inventory Header Value Assign
            invHdr.Ith_loc = GlbUserDefLoca;
            invHdr.Ith_com = GlbUserComCode;
            invHdr.Ith_doc_tp = "DO";
            invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
            invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
            invHdr.Ith_cate_tp = "DPS";
            invHdr.Ith_is_manual = false;
            invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = GlbUserName;
            invHdr.Ith_mod_by = GlbUserName;
            invHdr.Ith_direct = false;
            invHdr.Ith_session_id = GlbUserSessionID;
            invHdr.Ith_manual_ref = txtManualRefNo.Text;
            invHdr.Ith_vehi_no = string.Empty;
            invHdr.Ith_remarks = string.Empty;
            #endregion

            #region Inventory AutoNumber Value Assign
            MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
            _masterAutoDo.Aut_cate_cd = GlbUserDefLoca;
            _masterAutoDo.Aut_cate_tp = "LOC";
            _masterAutoDo.Aut_direction = 0;
            _masterAutoDo.Aut_moduleid = "DO";
            _masterAutoDo.Aut_start_char = "DO";
            _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            #region Invoice Header Value Assign
            _invheader.Sah_com = GlbUserComCode;
            _invheader.Sah_cre_by = GlbUserName;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = CurrancyCode;
            _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
            _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
            _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
            _invheader.Sah_cus_name = txtCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
            _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
            _invheader.Sah_direct = true;

            _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = txtInvType.Text.Trim();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = "";
            _invheader.Sah_man_ref = txtManualRefNo.Text;
            _invheader.Sah_manual = chkManualRef.Checked ? true : false;
            _invheader.Sah_mod_by = GlbUserName;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtManualRefNo.Text;
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
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
            if (chkDeliveryNow.Checked == false) _invheader.Sah_stus = "D";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_direct = true;
            _invheader.Sah_anal_1 = GlbUserDefLoca;
            _invheader.Sah_tax_inv = chkTaxInvoice.Checked ? true : false;
            _invheader.Sah_anal_11 = chkDeliveryNow.Checked ? 0 : 1;

            _invheader.Sah_del_loc = txtDelLocation.Text;
            _invheader.Sah_grn_com = _customerCompany;
            _invheader.Sah_grn_loc = _customerLocation;
            _invheader.Sah_is_grn = _isCustomerHasCompany;


            //2012/11/10
            //add exchange rate and currancy code

            _com = CHNLSVC.General.GetCompByCode(GlbUserComCode);

            _ex = CHNLSVC.Sales.GetExchangeRate(GlbUserComCode, CurrancyCode, DateTime.Now, _com.Mc_cur_cd);

            _invheader.Sah_ex_rt = _ex.Mer_bnkbuy_rt;
            _invheader.Sah_currency = CurrancyCode;

            //

            // _invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();

            if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());



            #endregion

            #region Receipt Header Value Assign
            _recHeader.Sar_acc_no = "";
            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            _recHeader.Sar_currency_cd = CurrancyCode;
            _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
            _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
            _recHeader.Sar_debtor_cd = txtCustomer.Text;
            _recHeader.Sar_debtor_name = txtCusName.Text;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            _recHeader.Sar_is_mgr_iss = false;
            _recHeader.Sar_is_oth_shop = false;
            _recHeader.Sar_is_used = false;
            _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            _recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            _recHeader.Sar_nic_no = txtNIC.Text;
            _recHeader.Sar_oth_sr = "";
            _recHeader.Sar_prefix = "";
            _recHeader.Sar_profit_center_cd = GlbUserDefProf;
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
            _recHeader.Sar_receipt_no = "na";
            _recHeader.Sar_receipt_type = "DIR";
            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            _recHeader.Sar_tel_no = txtMobile.Text;
            _recHeader.Sar_tot_settle_amt = 0;
            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;
            _recHeader.Sar_wht_rate = 0;


            #endregion

            #region Invoice AutoNumber Value Assign
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = GlbUserDefProf;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = txtInvType.Text;
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = _invoicePrefix;
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            #region Receipt AutoNumber Value Assign
            MasterAutoNumber _receiptAuto = null;
            if (_recieptItem != null)
                if (_recieptItem.Count > 0)
                {
                    _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = "PRO";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "DIR";
                    _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                }
            #endregion

            CollectBusinessEntity();

            string _invoiceNo = "";
            string _receiptNo = "";
            string _deliveryOrderNo = "";


            _invoiceItemListWithDiscount = new List<InvoiceItem>();
            List<InvoiceItem> _discounted = null;
            bool _isDifferent = false;
            decimal _tobepay = 0;

            Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
            CHNLSVC.Sales.GetGeneralPromotionDiscount(GlbUserComCode, GlbUserDefProf, txtInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay);
            _invoiceItemListWithDiscount = _discounted;


            if (_isDifferent)
            {
                lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                gvRePayment.DataSource = _recieptItem;
                _toBePayNewAmount = _tobepay;
                gvRePayment.DataBind();
                MPEReAddPayment.Show();
                return;
            }

            int effect = -1;
            try
            {
                string _error = string.Empty;
                btnSave.Enabled = false;
                effect = CHNLSVC.Sales.SaveInvoice(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, chkDeliveryNow.Checked == false ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "following item/serial does not having inventory balance." + _error);
                    return;
                }

                string uri = HttpContext.Current.Request.Url.AbsoluteUri;

                if (chkDeliveryNow.Checked == false) { CallDObyInvoice = _invoiceNo; CallDobyInvoiceManual = txtManualRefNo.Text; } else { CallDObyInvoice = null; CallDobyInvoiceManual = null; }


                //2012/11/09
                //save passport info
                int line = 0;
                foreach (DataRow dr in PassportTable.Rows)
                {

                    CustomerPassoprt _pass = new CustomerPassoprt();
                    _pass.Sdcd_flight = dr[0].ToString();
                    _pass.Sdcd_invc_no = _invoiceNo;
                    line = line + 1;
                    _pass.Sdcd_line = line;
                    _pass.Sdcd_cre_by = GlbUserName;
                    _pass.Sdcd_cre_dt = DateTime.Now;
                    _pass.Sdcd_ref = dr[1].ToString();

                    CHNLSVC.Sales.SaveCustomerPassportNums(_pass);
                }
                //
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, ex.Message);
                return;
            }
            finally
            {

                string Msg = string.Empty;
                btnSave.Enabled = true;


                if (effect != -1)
                {

                    if (chkDeliveryNow.Checked == false)
                    {
                        Msg = "alert('Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + "'); ";
                    }
                    else
                    {
                        Msg = "alert('Successfully Saved! Document No : " + _invoiceNo + "' ); ";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InvoiceMsg", Msg, true);

                    #region Printing

                    Dictionary<string, string> _lst = GetInvoiceSerialnWarranty(_invoiceNo.Trim());

                    foreach (KeyValuePair<string, string> _d in _lst)
                    {
                        GlbReportSerialList = _d.Key.Replace("N/A", "");
                        GlbReportWarrantyList = _d.Value.Replace("N/A", "");
                    }

                    MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");


                    GlbDocNosList = _invoiceNo.Trim();
                    if (_itm.Mbe_sub_tp != "C.")
                    {
                        GlbReportPath = "~/Reports_Module/Sales_Rep/DFInvoiceHalfPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/DFInvoiceHalfPrint.rpt";
                    }
                    else
                    {
                        GlbReportPath = "~/Reports_Module/Sales_Rep/DFInvoiceHalfPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/DFInvoiceHalfPrint.rpt";
                    }
                    GlbReportName = "Invoice";
                    // GlbMainPage = "~/Sales_Module/SalesEntry.aspx";

                    if (chkDeliveryNow.Checked == false)
                    {
                        GlbReportDeliveryOrderNo = _deliveryOrderNo.Trim();

                        GlbReportDeliveryOrderPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                        GlbReportDeliveryOrderMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";

                        // GlbReportDeliveryOrderMainPage = "~/Sales_Module/SalesEntry.aspx";
                    }


                    if (chkDeliveryNow.Checked == false)
                    {
                        Msg = "window.open('../Reports_Module/Sales_Rep/DFInvoicePrint.aspx',  '_blank'); window.open('../Reports_Module/Inv_Rep/DeliveryOrderPrint.aspx',  '_blank');";
                    }
                    else
                    {
                        Msg = "window.open('../Reports_Module/Sales_Rep/DFInvoicePrint.aspx',  '_blank');";
                    }

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "invoicePrint", Msg, true);


                    #endregion

                    Msg = "window.location = 'SalesEntryExchange.aspx';";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InvoiceClear", Msg, true);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Generating Invoice is terminated!");
                }
            }
        }

        protected void btnConfirm_CheckUserNewPaymentAmount(object sender, EventArgs e)//-------------------------------- save as confrim payments ------------------------
        {
            decimal _gridTotal = 0;
            TextBox _box = null;

            #region Deliver Now! - Check for serialied item qty with it's scan serial count
            if (chkDeliveryNow.Checked == false)
            {
                string _itmList = string.Empty;
                bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);
                if (_isqtyNserialOk == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invoice quantites and the serials are mismatched. Please check the following item for its serials. Item List : " + _itmList);
                    return;
                }
            }
            #endregion

            #region Deliver Now! - Check the Inventory Availability
            if (chkDeliveryNow.Checked == false)
            {
                string _nottallylist = string.Empty;
                bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                if (_isTallywithinventory == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Following item does not having inventory balance for raise delivery order; " + _nottallylist);
                    return;
                }
            }
            #endregion

            if (gvRePayment.Rows.Count > 0)
            {
                foreach (GridViewRow _r in gvRePayment.Rows)
                {
                    _box = (TextBox)_r.FindControl("txtRePayCollectAmt");
                    HiddenField _hdn = (HiddenField)_r.FindControl("hdnRePayLineNo");

                    if (string.IsNullOrEmpty(_box.Text)) { MPEReAddPayment.Show(); continue; }
                    if (!IsNumeric(_box.Text, NumberStyles.Currency))
                    {
                        lblRePayMsg.Text = "Please select the valid amount!"; _box.Text = string.Empty; MPEReAddPayment.Show(); break;
                    }

                    _gridTotal += Convert.ToDecimal(_box.Text.Trim());

                    _recieptItem.Where(x => x.Sard_line_no == Convert.ToInt32(_hdn.Value)).ToList().ForEach(x => x.Sard_settle_amt = Convert.ToDecimal(_box.Text.Trim()));

                }

                if (_gridTotal == 0) { MPEReAddPayment.Show(); return; }
                if (_gridTotal > 0 && _toBePayNewAmount > 0)
                {
                    if (_gridTotal < _toBePayNewAmount) { lblRePayMsg.Text = "Stil need to pay - " + FormatToCurrency(Convert.ToString(_toBePayNewAmount - _gridTotal)); MPEReAddPayment.Show(); return; }
                    if (_gridTotal > _toBePayNewAmount) { lblRePayMsg.Text = "To be paid amount exceed the current payment"; _box.Text = string.Empty; MPEReAddPayment.Show(); return; };
                    if (_gridTotal == _toBePayNewAmount)
                    {
                        _invoiceItemList = _invoiceItemListWithDiscount;

                        string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(GlbUserComCode, GlbUserDefProf, txtInvType.Text);

                        if (string.IsNullOrEmpty(_invoicePrefix))
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts dept.");
                            return;
                        }


                        InvoiceHeader _invheader = new InvoiceHeader();
                        RecieptHeader _recHeader = new RecieptHeader();
                        InventoryHeader invHdr = new InventoryHeader();
                        MasterBusinessEntity _entity = new MasterBusinessEntity();

                        #region Showroom manager having a company, and its to take the company to GRN in the DO stage
                        bool _isCustomerHasCompany = false;
                        string _customerCompany = string.Empty;
                        string _customerLocation = string.Empty;

                        _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                        if (_entity != null)
                            if (_entity.Mbe_cd != null)
                                if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                                { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }

                        #endregion

                        #region Inventory Header Value Assign
                        invHdr.Ith_loc = GlbUserDefLoca;
                        invHdr.Ith_com = GlbUserComCode;
                        invHdr.Ith_doc_tp = "DO";
                        invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                        invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                        invHdr.Ith_cate_tp = "DPS";
                        invHdr.Ith_is_manual = false;
                        invHdr.Ith_stus = "A";
                        invHdr.Ith_cre_by = GlbUserName;
                        invHdr.Ith_mod_by = GlbUserName;
                        invHdr.Ith_direct = false;
                        invHdr.Ith_session_id = GlbUserSessionID;
                        invHdr.Ith_manual_ref = txtManualRefNo.Text;
                        invHdr.Ith_vehi_no = string.Empty;
                        invHdr.Ith_remarks = string.Empty;
                        #endregion

                        #region Inventory AutoNumber Value Assign
                        MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                        _masterAutoDo.Aut_cate_cd = GlbUserDefLoca;
                        _masterAutoDo.Aut_cate_tp = "LOC";
                        _masterAutoDo.Aut_direction = 0;
                        _masterAutoDo.Aut_moduleid = "DO";
                        _masterAutoDo.Aut_start_char = "DO";
                        _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        #endregion

                        #region Invoice Header Value Assign
                        _invheader.Sah_com = GlbUserComCode;
                        _invheader.Sah_cre_by = GlbUserName;
                        _invheader.Sah_cre_when = DateTime.Now;
                        _invheader.Sah_currency = CurrancyCode;
                        _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                        _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
                        _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                        _invheader.Sah_cus_name = txtCusName.Text.Trim();
                        _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                        _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                        _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                        _invheader.Sah_direct = true;
                        _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
                        _invheader.Sah_epf_rt = 0;
                        _invheader.Sah_esd_rt = 0;
                        _invheader.Sah_ex_rt = 1;
                        _invheader.Sah_inv_no = "na";
                        _invheader.Sah_inv_sub_tp = "SA";
                        _invheader.Sah_inv_tp = txtInvType.Text.Trim();
                        _invheader.Sah_is_acc_upload = false;
                        _invheader.Sah_man_cd = "";
                        _invheader.Sah_man_ref = txtManualRefNo.Text;
                        _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                        _invheader.Sah_mod_by = GlbUserName;
                        _invheader.Sah_mod_when = DateTime.Now;
                        _invheader.Sah_pc = GlbUserDefProf;
                        _invheader.Sah_pdi_req = 0;
                        _invheader.Sah_ref_doc = txtManualRefNo.Text;
                        _invheader.Sah_remarks = "";
                        _invheader.Sah_sales_chn_cd = "";
                        _invheader.Sah_sales_chn_man = "";
                        _invheader.Sah_sales_ex_cd = "";
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
                        _invheader.Sah_direct = true;
                        _invheader.Sah_anal_1 = GlbUserDefLoca;
                        _invheader.Sah_tax_inv = chkTaxInvoice.Checked ? true : false;

                        _invheader.Sah_del_loc = txtDelLocation.Text;
                        _invheader.Sah_grn_com = _customerCompany;
                        _invheader.Sah_grn_loc = _customerLocation;
                        _invheader.Sah_is_grn = _isCustomerHasCompany;

                        //_invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                        #endregion

                        #region Receipt Header Value Assign
                        _recHeader.Sar_acc_no = "";
                        _recHeader.Sar_act = true;
                        _recHeader.Sar_com_cd = GlbUserComCode;
                        _recHeader.Sar_comm_amt = 0;
                        _recHeader.Sar_create_by = GlbUserName;
                        _recHeader.Sar_create_when = DateTime.Now;
                        _recHeader.Sar_currency_cd = CurrancyCode;
                        _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
                        _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
                        _recHeader.Sar_debtor_cd = txtCustomer.Text;
                        _recHeader.Sar_debtor_name = txtCusName.Text;
                        _recHeader.Sar_direct = true;
                        _recHeader.Sar_direct_deposit_bank_cd = "";
                        _recHeader.Sar_direct_deposit_branch = "";
                        _recHeader.Sar_epf_rate = 0;
                        _recHeader.Sar_esd_rate = 0;
                        _recHeader.Sar_is_mgr_iss = false;
                        _recHeader.Sar_is_oth_shop = false;
                        _recHeader.Sar_is_used = false;
                        _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                        _recHeader.Sar_mob_no = txtMobile.Text;
                        _recHeader.Sar_mod_by = GlbUserName;
                        _recHeader.Sar_mod_when = DateTime.Now;
                        _recHeader.Sar_nic_no = txtNIC.Text;
                        _recHeader.Sar_oth_sr = "";
                        _recHeader.Sar_prefix = "";
                        _recHeader.Sar_profit_center_cd = GlbUserDefProf;
                        _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
                        _recHeader.Sar_receipt_no = "na";
                        _recHeader.Sar_receipt_type = "DIR";
                        _recHeader.Sar_ref_doc = "";
                        _recHeader.Sar_remarks = "";
                        _recHeader.Sar_seq_no = 1;
                        _recHeader.Sar_ser_job_no = "";
                        _recHeader.Sar_session_id = GlbUserSessionID;
                        _recHeader.Sar_tel_no = txtMobile.Text;
                        _recHeader.Sar_tot_settle_amt = 0;
                        _recHeader.Sar_uploaded_to_finance = false;
                        _recHeader.Sar_used_amt = 0;
                        _recHeader.Sar_wht_rate = 0;
                        #endregion

                        #region Invoice AutoNumber Value Assign
                        MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                        _invoiceAuto.Aut_cate_cd = GlbUserDefProf;
                        _invoiceAuto.Aut_cate_tp = "PRO";
                        _invoiceAuto.Aut_direction = 1;
                        _invoiceAuto.Aut_modify_dt = null;
                        _invoiceAuto.Aut_moduleid = txtInvType.Text;
                        _invoiceAuto.Aut_number = 0;
                        _invoiceAuto.Aut_start_char = _invoicePrefix;
                        _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        #endregion

                        #region Receipt AutoNumber Value Assign
                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "DIR";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        #endregion

                        CollectBusinessEntity();

                        string _invoiceNo = "";
                        string _receiptNo = "";
                        string _deliveryOrderNo = "";


                        //TODO: Check for hold invoice
                        bool _isHoldInvoiceProcess = false;
                        InvoiceHeader _hdr;
                        _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                        if (_hdr != null && _hdr.Sah_stus != "H")
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not edit saved invoice");
                            return;
                        }
                        if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;

                        try
                        {
                            string _error = string.Empty;
                            btnSave.Enabled = false;
                            int effect = CHNLSVC.Sales.SaveInvoice(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, chkDeliveryNow.Checked == false ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error);

                            if (string.IsNullOrEmpty(_error))
                            {

                                string uri = HttpContext.Current.Request.Url.AbsoluteUri;

                                if (chkDeliveryNow.Checked == false) { CallDObyInvoice = _invoiceNo; CallDobyInvoiceManual = txtManualRefNo.Text; } else { CallDObyInvoice = null; CallDobyInvoiceManual = null; }

                                btnSave.Enabled = true;
                                string Msg = string.Empty;
                                if (chkDeliveryNow.Checked)
                                {
                                    Msg = "<script>alert('Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + "'); window.location = 'SalesEntryExchange.aspx';</script>";
                                }
                                else
                                {
                                    Msg = "<script>alert('Successfully Saved! Document No : " + _invoiceNo + "; ); window.location = 'SalesEntryExchange.aspx';</script>";
                                }
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                                if (chkDeliveryNow.Checked == true)
                                {
                                    GlbDocNosList = _invoiceNo;
                                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\InvoicePrint.rpt";
                                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoicePrint.rpt";

                                    GlbMainPage = "~/Sales_Module/SalesEntryExchange.aspx";
                                    Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
                                }
                            }

                            else
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please check the following item/serials for inventory balance; " + _error);
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, ex.Message);
                            return;
                        }
                        finally
                        {
                            btnSave.Enabled = true;

                        }

                    }
                }

            }
        }
        //--------------------------------------------------------------------------------------------------------------- save @ hold -------------------------------------
        #region  Rooting for Hold invoice
        private void Hold()
        {
            if (chkDeliveryNow.Checked == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Deliver Now is not allow for holding an invoice");
                return;
            }


            if (string.IsNullOrEmpty(txtInvType.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the invoice type");
                txtInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the customer");
                txtCustomer.Focus();
                return;
            }


            if (_invoiceItemList.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the items for invoice");
                return;
            }


            if (string.IsNullOrEmpty(txtExecutive.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the executive code");
                txtExecutive.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtCurrency.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the currency code");
            //    txtCurrency.Focus();
            //    return;
            //}

            if (_recieptItem.Count > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please remove the payment details.");
                return;
            }



            //assigning line no to receipt items
            Int32 _count = 0;
            _recieptItem.ForEach(x => x.Sard_line_no = _count++);
            _count = 1;
            _invoiceItemList.ForEach(x => x.Sad_itm_line = _count++);


            InvoiceHeader _invheader = new InvoiceHeader();
            RecieptHeader _recHeader = new RecieptHeader();
            MasterBusinessEntity _entity = new MasterBusinessEntity();

            #region Showroom manager having a company, and its to take the company to GRN in the DO stage
            bool _isCustomerHasCompany = false;
            string _customerCompany = string.Empty;
            string _customerLocation = string.Empty;

            _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            if (_entity != null)
                if (_entity.Mbe_cd != null)
                    if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                    { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }

            #endregion


            InvoiceHeader _hdr;
            _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
            if (_hdr == null) _hdr = new InvoiceHeader();


            if (_hdr.Sah_pc != null)
            {
                //second time
                if (_hdr.Sah_dt.Date != Convert.ToDateTime(txtDate.Text.Trim()).Date)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Hold invoice can only re-hold with in the date" + _hdr.Sah_dt.Date.ToShortDateString());
                    return;
                }

                if (_hdr.Sah_stus != "H")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not hold the invoice which already " + _hdr.Sah_stus == "C" ? "canceled." : _hdr.Sah_stus == "A" ? "approved." : _hdr.Sah_stus == "D" ? "delivered." : ".");
                    return;
                }

            }

            #region Invoice Header Value Assign
            _invheader.Sah_com = GlbUserComCode;
            _invheader.Sah_cre_by = GlbUserName;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = CurrancyCode;
            _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
            _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
            _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
            _invheader.Sah_cus_name = txtCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
            _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = _hdr.Sah_pc != null ? Convert.ToString(_hdr.Sah_seq_no) : Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = txtInvType.Text.Trim();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = "";
            _invheader.Sah_man_ref = txtManualRefNo.Text;
            _invheader.Sah_manual = chkManualRef.Checked ? true : false;
            _invheader.Sah_mod_by = GlbUserName;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtManualRefNo.Text;
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = "";
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = Convert.ToInt32(_invheader.Sah_inv_no);
            _invheader.Sah_session_id = GlbUserSessionID;
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "H";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_direct = true;
            _invheader.Sah_anal_1 = GlbUserDefLoca;
            _invheader.Sah_tax_inv = chkTaxInvoice.Checked ? true : false;
            _invheader.Sah_anal_7 = chkDeliveryNow.Checked ? 0 : 1;

            _invheader.Sah_del_loc = txtDelLocation.Text;
            _invheader.Sah_grn_com = _customerCompany;
            _invheader.Sah_grn_loc = _customerLocation;
            _invheader.Sah_is_grn = _isCustomerHasCompany;

            //_invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();


            #endregion

            #region Invoice AutoNumber Value Assign
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            #endregion

            #region Receipt AutoNumber Value Assign
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            #endregion

            CollectBusinessEntity();

            string _invoiceNo = "";
            string _receiptNo = "";
            string _deliveryOrderNo = "";


            try
            {
                btnSave.Enabled = false;
                string _error = string.Empty;
                int effect = CHNLSVC.Sales.SaveInvoice(_invheader, _invoiceItemList, null, _recHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, true, false, out _error);
                if (string.IsNullOrEmpty(_error))
                {
                    string uri = HttpContext.Current.Request.Url.AbsoluteUri;

                    CallDObyInvoice = null;
                    CallDobyInvoiceManual = null;

                    btnHold.Enabled = true;
                    string Msg = "<script>alert('Successfully Saved! Document No : " + _invoiceNo + "');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HoldAlert", Msg, false);

                    Msg = "window.location = 'SalesEntryExchange.aspx';";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InvoiceHoldClear", Msg, true);

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "please check the following item/serials for inventory balance; " + _error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, ex.Message);
                return;
            }
            finally
            {
                btnHold.Enabled = true;

            }
        }
        protected void Hold(object sender, EventArgs e)
        {
            Hold();
        }
        #endregion

        protected void OnBindInvoiceItem(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _item = e.Row.Cells[1].Text.Trim();
                // string _status = e.Row.Cells[4].Text.Trim();
                // Label _lblqty = (Label)e.Row.FindControl("lblGridQty");
                ImageButton _btn = (ImageButton)e.Row.FindControl("imgBtnGridSerial");
                // decimal _qty = Convert.ToDecimal(_lblqty.Text.Trim());
                MasterItem _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                if (_itm.Mi_is_ser1 != 1) _btn.Visible = false;
                ImageButton _bbtn = (ImageButton)e.Row.FindControl("imgBtnPickSerial");
                if (!string.IsNullOrEmpty(txtQuotation.Text.Trim()))
                    if (chkDeliveryNow.Checked == false)
                    {
                        if (_itm.Mi_is_ser1 != 1)
                        {
                            // _bbtn.Visible = false;
                            _bbtn.ImageUrl = "~/Images/EditIcon.png";
                            e.Row.Cells[16].Visible = false;
                        }
                        else
                        {
                            // _bbtn.Visible = true;
                            _bbtn.ImageUrl = "~/Images/Add-16x16x16.ICO";
                            e.Row.Cells[16].Visible = true;
                        }
                    }
                    else
                    {
                        e.Row.Cells[15].Visible = false;
                        e.Row.Cells[16].Visible = false;
                    }
            }
        }
        protected void OnRowSelectCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SELECT" && chkDeliveryNow.Checked == false)
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '|' });

                Int32 _itmLine = Convert.ToInt32(commandArgs[0]);
                string _item = Convert.ToString(commandArgs[1]);
                string _status = Convert.ToString(commandArgs[2]);
                decimal _qty = Convert.ToDecimal(commandArgs[3]);

                var _dup = ScanSerialList.Where(x => x.Tus_base_itm_line == _itmLine).Count();
                List<ReptPickSerials> _showList = new List<ReptPickSerials>();
                _showList = ScanSerialList.Where(x => x.Tus_base_itm_line == _itmLine).ToList();
                gvPickItemSerial.DataSource = _showList;
                gvPickItemSerial.DataBind();
                MPEItemSerialItm.Show();
            }
            if (e.CommandName == "PICK" && chkDeliveryNow.Checked == false && !string.IsNullOrEmpty(txtQuotation.Text.Trim()))
            {

                Int32 _selectedline = Convert.ToInt32(e.CommandArgument);
                List<ReptPickSerials> serial_list = null;
                var _item = _invoiceItemList.Where(x => x.Sad_itm_line == _selectedline).ToList();

                string _itemcode = string.Empty;
                string _description = string.Empty;
                string _book = string.Empty;
                string _level = string.Empty;
                decimal _invoiceqty = 0;
                decimal _doqty = 0;

                foreach (InvoiceItem _i in _item)
                {
                    _itemcode = _i.Sad_itm_cd;
                    _description = _i.Mi_longdesc;
                    _invoiceqty = _i.Sad_qty;
                    _doqty = _i.Sad_do_qty;
                    _book = _i.Sad_pbook;
                    _level = _i.Sad_pb_lvl;
                }

                hdnInvoiceLineNo.Value = Convert.ToString(_selectedline);

                MasterItem _mitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _itemcode);
                if (_mitem.Mi_is_ser1 == 0 || _mitem.Mi_is_ser1 == -1)
                {
                    string _script = @"
                     var _selectqty=window.prompt('Please enter the qty',' " + _invoiceqty + @" '); 
                     if (_selectqty != null && _selectqty != '')
                        {
                           btnEditBtn_click(_selectqty);
                        } 
                    ";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "PromptMsg", _script, true);
                }
                else
                {
                    lblPopupItemCode.Text = _itemcode;


                    lblScanQty.Text = Convert.ToString(_doqty);
                    lblDeliveredQty.Text = "0";
                    lblInvoiceQty.Text = Convert.ToString(_invoiceqty);

                    divPopupImg.Visible = false;
                    lblpopupMsg.Text = string.Empty;

                    int pbCount = CHNLSVC.Sales.GetDOPbCount(GlbUserComCode, _book, _level);

                    if (lblPopupItemCode.Text != "&nbsp;")
                    {
                        lblPopupItemCode.Text = lblPopupItemCode.Text;
                        lblPopupBinCode.Visible = false;
                        txtPopupQty.Visible = true;

                        serial_list = new List<ReptPickSerials>();

                        if (pbCount <= 0)
                        {
                            serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, _book, _level);
                        }
                        else
                            serial_list = CHNLSVC.Sales.GetStatusGodSerial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, _book, _level, "GOD");

                        gvPopSerialPick.DataSource = serial_list;
                        gvPopSerialPick.DataBind();
                        ModalPopupExtItem.Show();

                    }
                }
            }

        }
        protected void LoadQuotationItem(object sender, EventArgs e)
        {
            //GetQuotationDetailForInvoice
            if (string.IsNullOrEmpty(txtQuotation.Text.Trim())) return;
            _invoiceItemList = CHNLSVC.Sales.GetQuotationDetail(GlbUserComCode, GlbUserDefProf, txtQuotation.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()).Date);
            if (_invoiceItemList != null)
                if (_invoiceItemList.Count > 0)
                {
                    #region Check For Inventory Balance if Delivered Now
                    if (chkDeliveryNow.Checked == false)
                    {
                        bool _isPricelevelallowforDOanystatus = false;
                        string _balanceexceedList = string.Empty;
                        foreach (InvoiceItem _itm in _invoiceItemList)
                        {
                            //------------------------------------------------------------------------------------------------
                            if (!string.IsNullOrEmpty(_itm.Sad_pbook) && !string.IsNullOrEmpty(_itm.Sad_pb_lvl))
                            {
                                List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _itm.Sad_pbook, _itm.Sad_pb_lvl);
                                if (_lvl != null)
                                    if (_lvl.Count > 0)
                                    {
                                        var _bool = from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp;
                                        if (_bool != null) if (_bool.Count() > 0) _isPricelevelallowforDOanystatus = false; else _isPricelevelallowforDOanystatus = true; else _isPricelevelallowforDOanystatus = true;
                                    }
                            }
                            else
                                _isPricelevelallowforDOanystatus = true;

                            //------------------------------------------------------------------------------------------------
                            decimal _pickQty = 0;
                            if (_isPricelevelallowforDOanystatus)
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd).ToList().Select(x => x.Sad_qty).Sum();
                            else
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd && x.Mi_itm_stus == _itm.Mi_itm_stus).ToList().Select(x => x.Sad_qty).Sum();

                            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, _itm.Sad_itm_cd, _itm.Mi_itm_stus);

                            if (_inventoryLocation != null)
                                if (_inventoryLocation.Count > 0)
                                {
                                    decimal _invBal = _inventoryLocation[0].Inl_qty;
                                    if (_pickQty > _invBal)
                                    {
                                        if (string.IsNullOrEmpty(_balanceexceedList))
                                            _balanceexceedList = _itm.Sad_itm_cd;
                                        else
                                            _balanceexceedList = ", " + _itm.Sad_itm_cd;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(_balanceexceedList))
                                        _balanceexceedList = _itm.Sad_itm_cd;
                                    else
                                        _balanceexceedList = ", " + _itm.Sad_itm_cd;
                                }
                            else
                            {
                                if (string.IsNullOrEmpty(_balanceexceedList))
                                    _balanceexceedList = _itm.Sad_itm_cd;
                                else
                                    _balanceexceedList = ", " + _itm.Sad_itm_cd;
                            }
                        }

                        if (!string.IsNullOrEmpty(_balanceexceedList))
                        {
                            _invoiceItemList = new List<InvoiceItem>();
                            ScanSerialList = new List<ReptPickSerials>();
                            InvoiceSerialList = new List<InvoiceSerial>();
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, _balanceexceedList + " item(s) inventory balance exceeds");
                            return;
                        }
                    }
                    #endregion

                    foreach (InvoiceItem itm in _invoiceItemList)
                    { CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }

                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();

                    //Assign Non-Serial item for the list
                    ScanSerialList = new List<ReptPickSerials>();
                    string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                    foreach (InvoiceItem _itm in _invoiceItemList)
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(GlbUserComCode, _itm.Sad_itm_cd);
                        if (_item.Mi_is_ser1 == 0)
                        {
                            List<ReptPickSerials> _nonserLst = null;
                            if (IsPriceLevelAllowDoAnyStatus == false)
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, _itm.Sad_itm_cd, _itm.Sad_itm_stus, Convert.ToDecimal(_itm.Sad_qty));
                            else
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(GlbUserComCode, GlbUserDefLoca, _itm.Sad_itm_cd, string.Empty,Convert.ToDecimal(_itm.Sad_qty));

                            _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(-100));
                            _nonserLst.ForEach(x => x.Tus_base_itm_line = _itm.Sad_itm_line);
                            _nonserLst.ForEach(x => x.Tus_usrseq_no = -100);
                            _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                            _nonserLst.ForEach(x => x.Tus_serial_id = string.Empty);
                            _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                            _nonserLst.ForEach(x => x.Tus_new_status = string.Empty);
                            ScanSerialList.AddRange(_nonserLst);
                        }
                        else if (_item.Mi_is_ser1 == -1)
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_com = GlbUserComCode;
                            _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                            _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                            _reptPickSerial_.Tus_bin = _defbin;
                            _reptPickSerial_.Tus_cre_by = GlbUserName;
                            _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                            _reptPickSerial_.Tus_cross_batchline = 0;
                            _reptPickSerial_.Tus_cross_itemline = 0;
                            _reptPickSerial_.Tus_cross_seqno = 0;
                            _reptPickSerial_.Tus_cross_serline = 0;
                            _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtDate.Text);
                            _reptPickSerial_.Tus_doc_no = string.Empty;
                            _reptPickSerial_.Tus_exist_grncom = string.Empty;
                            _reptPickSerial_.Tus_isapp = 1;
                            _reptPickSerial_.Tus_iscovernote = 1;
                            _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                            _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                            _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_line = 0;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                            _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                            _reptPickSerial_.Tus_loc = GlbUserDefLoca;
                            _reptPickSerial_.Tus_new_status = string.Empty;
                            _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_ser_line = 0;
                            _reptPickSerial_.Tus_session_id = GlbUserSessionID;
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_usrseq_no = -100;
                            _reptPickSerial_.Tus_warr_no = "N/A";
                            _reptPickSerial_.Tus_warr_period = 0;
                            _reptPickSerial_.Tus_new_remarks = string.Empty;
                            ScanSerialList.Add(_reptPickSerial_);
                        }
                    }
                    gvPopSerial.DataSource = ScanSerialList;
                    gvPopSerial.DataBind();
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invalid Quotation No ");
                    _invoiceItemList = new List<InvoiceItem>();
                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();
                }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invalid Quotation No ");
                _invoiceItemList = new List<InvoiceItem>();
                gvInvoiceItem.DataSource = _invoiceItemList;
                gvInvoiceItem.DataBind();
            }

        }
        protected void gvInvoiceItem_RowEditing(object sender, EventArgs e)
        {
            Int32 invoiceline = Convert.ToInt32(hdnInvoiceLineNo.Value);
            string _strQty = hdnEditQty.Value;

            decimal Num;
            bool isNum = decimal.TryParse(_strQty, out Num);
            if (isNum)
            {
                if (Num <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invalid qty.");
                    return;
                }
                decimal _invqty = _invoiceItemList.Where(x => x.Sad_itm_line == invoiceline).Select(y => y.Sad_qty).Sum();
                if (_invqty < Num)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "You can not exceed the approved qty");
                    return;
                }

                var _selectedLine = _invoiceItemList.Where(x => x.Sad_itm_line == invoiceline).ToList();
                foreach (InvoiceItem i in _selectedLine)
                {
                    decimal _newqty = Num;
                    decimal _oldqty = i.Sad_qty;
                    decimal _portion = _newqty / _oldqty;

                    decimal _newunitprice = i.Sad_unit_rt;
                    decimal _newunitamt = i.Sad_unit_amt * _portion;
                    decimal _newtax = i.Sad_itm_tax_amt * _portion;
                    decimal _newdiscount = i.Sad_disc_amt * _portion;


                }


            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Invalid qty.");
                return;
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, " qty selected " + hdnEditQty.Value );
        }

        protected void btnPopupOk_Click(object sender, EventArgs e)
        {
            List<ReptPickSerials> InvoiceItemGridScanSerial = new List<ReptPickSerials>();
            Int32 generated_seq = -1;
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.gvPopSerialPick.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                if (chkSelect.Checked) { num_of_checked_itms++; }
            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                divPopupImg.Visible = true;
                lblpopupMsg.Text = "Can't exceed Invoice Qty!";
                ModalPopupExtItem.Show();
                return;
            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            {
                int rowCount = 0;
                foreach (GridViewRow gvr in this.gvPopSerialPick.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        var _dup = ScanSerialList.Where(x => x.Tus_ser_id == serID).ToList();
                        if (_dup != null)
                            if (_dup.Count > 0)
                            {
                                divPopupImg.Visible = true;
                                lblpopupMsg.Text = "serial already picked!";
                                ModalPopupExtItem.Show();
                                return;
                            }

                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = GlbUserName;
                        _reptPickSerial_.Tus_base_doc_no = string.Empty;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = mi.Mi_model;
                        rowCount++;
                        InvoiceItemGridScanSerial.Add(_reptPickSerial_);
                    }
                }
            }

            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;
                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();
                foreach (GridViewRow gvr in this.gvPopSerialPick.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        _reptPickSerial_nonSer.Tus_base_doc_no = string.Empty;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;



                        rowCount++;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                    }

                }
                InvoiceItemGridScanSerial.AddRange(actual_non_ser_List);
            }
            //------------------------------------------------------------------------------------------------------------------------------
            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.gvPopSerialPick.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        Decimal pending_amt_ = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        }
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        _reptPickSerial_nonSer.Tus_base_doc_no = string.Empty;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);
                        rowCount++;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);
                    }
                }
                InvoiceItemGridScanSerial.AddRange(actual_non_ser_List);
            }

            ScanSerialList.AddRange(InvoiceItemGridScanSerial);
            gvPopSerial.DataSource = ScanSerialList;
            gvPopSerial.DataBind();

            _invoiceItemList.Where(x => x.Sad_itm_line == Convert.ToInt32(hdnInvoiceLineNo.Value)).ToList().ForEach(y => y.Sad_do_qty = y.Sad_do_qty + num_of_checked_itms);
            gvInvoiceItem.DataSource = _invoiceItemList;
            gvInvoiceItem.DataBind();

        }
        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }
        public List<ReptPickSerials> serial_list
        {
            get { return (List<ReptPickSerials>)Session["ReptPickSerials"]; }
            set { Session["ReptPickSerials"] = value; }
        }
        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            if (ddlPopupSerial.SelectedValue == "Serial 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                gvPopSerialPick.DataSource = serial_list;
                gvPopSerialPick.DataBind();

                ModalPopupExtItem.Show();
            }
            else if (ddlPopupSerial.SelectedValue == "Serial 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                gvPopSerialPick.DataSource = serial_list;
                gvPopSerialPick.DataBind();

                ModalPopupExtItem.Show();
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
            foreach (GridViewRow gvr in this.gvPopSerialPick.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }
            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                Decimal availability = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - (Convert.ToDecimal(lblDeliveredQty.Text.ToString()) + Convert.ToDecimal(lblScanQty.Text.ToString()));
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Invoice Qty! Can add only " + availability + " itmes more.");
                return;
            }

            ModalPopupExtItem.Show();
        }

        protected void imgBtnQuotation_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dataSource = null;
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.QuotationForInvoice);
            dataSource = CHNLSVC.CommonSearch.GetQuotationDetailForInvoice(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtQuotation.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        //NEW

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            string nicNo = ((TextBox)cusCreP1.FindControl("txtNicNo")).Text;
            string name = ((TextBox)cusCreP1.FindControl("txtFirstName")).Text;
            string passport = ((TextBox)cusCreP1.FindControl("txtPassportNo")).Text;
            string add1 = ((TextBox)((Panel)((AjaxControlToolkit.TabPanel)((AjaxControlToolkit.TabContainer)cusCreP2.FindControl("TabContainer")).FindControl("TabPanel1")).FindControl("Panel_homeAddr")).FindControl("txtAddresline1")).Text;

            if (name == "" || passport == "" || add1 == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields<br>E.g-Name,Passport,Address");
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
            CustomerCreationUC CUST = new CustomerCreationUC();
            string custCD = CUST.SaveCustomer(finalCust, _account, bisInfoList);
            txtCustomer.Text = custCD;


            // ResetFields(cusCreP1.Controls);
            //ResetFields(cusCreP2.Controls);
            CollapsiblePanelExtender1.Collapsed = true;
            CollapsiblePanelExtender1.ClientState = "true";
            CPEInvoiceItem.Collapsed = false;
            CPEInvoiceItem.ClientState = "false";
            CPEPayment.Collapsed = false;
            CPEPayment.ClientState = "false";
            string Msgg = "<script>CheckByCustomer();</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);

        }

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

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = uc_CustomerCreation1.GetMainCustInfor();

            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = uc_CustCreationExternalDet1.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();

            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            finalCust.Mbe_cd = uc_CustomerCreation1.CustCode;
            finalCust.Mbe_com = GlbUserComCode;

            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();

            CustomerCreationUC CUST = new CustomerCreationUC();
            Int32 effect = CUST.UpdateCustomer(finalCust, 0, bisInfoList);
        }

        protected void ButtonAddPassport_Click(object sender, EventArgs e)
        {
            if (TextBoxFlightNo.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Flight no");
                return;
            }
            if (TextBoxPassport.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Passport no");
                return;
            }

            DataRow dr = PassportTable.NewRow();
            dr[0] = TextBoxFlightNo.Text;
            dr[1] = TextBoxPassport.Text;
            PassportTable.Rows.Add(dr);
            TextBoxPassport.Text = "";
            TextBoxFlightNo.Text = "";

            GridViewItemDetails.DataSource = PassportTable;
            GridViewItemDetails.DataBind();
        }

        protected void DropDownCurrancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrancyCode == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Price Book currancy code not set");
                LabelExRate.Text = "";
                return;
            }

            MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(GlbUserComCode, DropDownCurrancy.SelectedValue, DateTime.Now, CurrancyCode);
            if (_exc1 != null)
            {
                LabelExRate.Text = _exc1.Mer_bnkbuy_rt.ToString("0.0000");
            }
            else
                LabelExRate.Text = "Rate not saved yet";

            //MasterExchangeRate _exc = CHNLSVC.Sales.GetLaterstExchangeRate(GlbUserComCode, DropDownCurrancy.SelectedValue, DateTime.Now);
            //if (_exc != null)
            //{
            //    decimal _comCurRt = _exc.Mer_bnkbuy_rt;
            //    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetLaterstExchangeRate(GlbUserComCode, CurrancyCode, DateTime.Now);
            //    if (_exc1 != null)
            //    {
            //        decimal _pbRt = _exc1.Mer_bnkbuy_rt;
            //        decimal _comPbrate =   _comCurRt/_pbRt;

            //        LabelExRate.Text = _comPbrate.ToString("0.0000");
            //    }
            //    else
            //        LabelExRate.Text = "Rate not saved yet";
            //}
            //else
            //    LabelExRate.Text = "Rate not saved yet";
        }

        protected void TextBoxCurAmo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LabelExRate.Text) || LabelExRate.Text == "Rate not saved yet") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Exchange Rate not saved."); return; }
            decimal amo;
            if (decimal.TryParse(TextBoxCurAmo.Text, out amo))
            {
                LabelPcAmo.Text = (Convert.ToDecimal(LabelExRate.Text) * Convert.ToDecimal(TextBoxCurAmo.Text)).ToString("0.00");
            }
        }

        protected void GridViewItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (PassportTable.Rows.Count > 0)
            {
                PassportTable.Rows.RemoveAt(e.RowIndex);
            }
            GridViewItemDetails.DataSource = PassportTable;
            GridViewItemDetails.DataBind();
        }

    }
}



