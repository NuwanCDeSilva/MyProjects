using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FF.WindowsERPClient.Sales
{
    public partial class DutyFreeInvoice : Base
    {

        #region Table Column Names
        //NorPrice_Select
        //NorPrice_Serial
        //NorPrice_Item
        //NorPrice_UnitPrice
        //NorPrice_Circuler
        //NorPrice_PriceType
        //NorPrice_PriceTypeDescription
        //NorPrice_ValidTill
        //NorPrice_Pb_Seq
        //NorPrice_PbLineSeq
        //NorPrice_PromotionCD
        //NorPrice_IsFixQty
        //NorPrice_BkpUPrice
        //NorPrice_WarrantyRmk


        //PromPrice_Select
        //PromPrice_Serial
        //PromPrice_Item
        //PromPrice_UnitPrice
        //PromPrice_Circuler
        //PromPrice_PriceType
        //PromPrice_PriceTypeDescription
        //PromPrice_ValidTill
        //PromPrice_Pb_Seq
        //PromPrice_PbLineSeq
        //PromPrice_PromotionCD
        //PromPrice_IsFixQty
        //PromPrice_BkpUPrice
        //PromPrice_WarrantyRmk

        //PromItm_ItemLine
        //PromItm_Item
        //PromItm_Description
        //PromItm_Modle
        //PromItm_Serial
        //PromItm_Qty
        //PromItm_UnitPrice
        //PromItm_PbSeq
        //PromItm_PbLineSeq
        //PromItm_MainItem
        //PromItm_MainSerial
        //PromItm_BkpSubSerial
        //PromItm_BkpUnitPrice


        //ProSer_Select
        //ProSer_Item
        //ProSer_Serial1
        //ProSer_Serial2
        //ProSer_Warranty
        //ProSer_Status
        //ProSer_SerialID

        //PopComItm_Item
        //PopComItm_Description
        //PopComItm_Status
        //PopComItm_Qty

        //PopComItmSer_Select
        //PopComItmSer_Item
        //PopComItmSer_Serial1
        //PopComItmSer_Serial2
        //PopComItmSer_Warranty
        //PopComItmSer_Status
        //PopComItmSer_Serialid

        //InvItm_No
        //InvItm_Item
        //InvItm_Description
        //InvItm_Status
        //InvItm_Qty
        //InvItm_UPrice
        //InvItm_UnitAmt
        //InvItm_DisRate
        //InvItm_DisAmt
        //InvItm_TaxAmt
        //InvItm_LineAmt
        //InvItm_Book
        //InvItm_Level
        //InvItm_Delete
        //InvItm_SerialShow
        //InvItm_SerialAdd
        //InvItm_PbPrice
        //InvItm_PbSeq
        //InvItm_PbLineSeq
        //InvItm_WarraPeriod
        //InvItm_WarraRemarks
        //InvItm_IsPromo
        //InvItm_PromoCd
        //InvItm_JobLine
        //InvItm_Circuler

        //popSer_Remove
        //popSer_Item
        //popSer_Model
        //popSer_Status
        //popSer_Qty
        //popSer_Serial1
        //popSer_Serial2
        //popSer_Warranty
        //popSer_BaseItemLine
        //popSer_SerialID
        //popSer_NewStatus
        //popSer_UnitPrice

        //txtDate
        //cmbInvType
        //txtDocRefNo
        //txtInvoiceNo
        //lblCurrency

        //txtCustomer
        //txtNIC
        //txtMobile
        //txtCusName
        //txtAddress1
        //txtAddress2

        //chkTaxPayable
        //lblSVatStatus
        //lblVatExemptStatus

        //lblAccountBalance
        //lblAvailableCredit

        //txtExecutive
        //lblExecutiveName
        //txtManualRefNo
        //chkManualRef

        //txtSerialNo
        //txtItem
        //cmbBook
        //cmbLevel
        //cmbStatus
        //txtQty
        //txtUnitPrice
        //txtUnitAmt
        //txtDisRate
        //txtDisAmt
        //txtTaxAmt
        //txtLineTotAmt
        //gvInvoiceItem

        //lblGrndSubTotal
        //lblGrndDiscount
        //lblGrndAfterDiscount
        //lblGrndTax
        //lblGrndTotalAmount

        //ucPayModes1

        //pnlConsumerPrice
        //pnlDeliveryInstruction
        //pnlInventoryCombineSerialPick
        //pnlMain
        //pnlMultiCombine
        //pnlMultipleItem
        //pnlPriceNPromotion



        //pnlDeliveryInstruction
        //----------------------
        //txtDelLocation
        //lblDeliveryLocation
        //chkOpenDelivery
        //txtDelCustomer
        //txtDelName
        //txtDelAddress1
        //txtDelAddress2

        //pnlInventoryCombineSerialPick
        //-----------------------------
        //gvPopComItem
        //gvPopComItemSerial
        //txtInvComSerSearch
        #endregion

        #region Variables
        private List<InvoiceItem> _invoiceItemList = null;
        private List<InvoiceItem> _invoiceItemListWithDiscount = null;

        private List<RecieptItem> _recieptItem = null;
        private List<RecieptItem> _newRecieptItem = null;

        private MasterBusinessEntity _businessEntity = null;
        private List<MasterItemComponent> _masterItemComponent = null;
        private PriceBookLevelRef _priceBookLevelRef = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;

        private List<PriceDetailRef> _priceDetailRef = null;
        private MasterBusinessEntity _masterBusinessCompany = null;

        //For take selected serials to save/ for temporary
        private List<PriceSerialRef> _MainPriceSerial = null;
        private List<PriceSerialRef> _tempPriceSerial = null;

        //For take selected combine to save/ for temporary
        private List<PriceCombinedItemRef> _MainPriceCombinItem = null;
        private List<PriceCombinedItemRef> _tempPriceCombinItem = null;

        //private bool IsNoPriceDefinition = false;
        bool _isInventoryCombineAdded = false;

        private Int32 ScanSequanceNo = 0;

        //Store the scan serial list even the invoice going to be deliver
        private List<ReptPickSerials> ScanSerialList = null;
        //Status of the price level which need to allocate the inventory status should check from the inventory
        private bool IsPriceLevelAllowDoAnyStatus = false;
        //Store Warranty Remarks
        private string WarrantyRemarks = string.Empty;
        //Store Warranty Period
        private Int32 WarrantyPeriod = 0;
        //Store Serial no which come from txtSerialNo
        private string ScanSerialNo = string.Empty;
        //Stores the default value of the item status
        private string DefaultItemStatus = string.Empty;
        //Stores invoice select serials
        private List<InvoiceSerial> InvoiceSerialList = null;

        private List<ReptPickSerials> InventoryCombinItemSerialList = null;
        private List<ReptPickSerials> PriceCombinItemSerialList = null;

        private Int32 _lineNo = 0;
        private bool _isEditPrice = false;
        private bool _isEditDiscount = false;

        //Count And Display Only
        private decimal GrndSubTotal = 0;
        private decimal GrndDiscount = 0;
        private decimal GrndTax = 0;
        //private decimal _paidAmount = 0;
        //private decimal _toBePayNewAmount = 0;
        private bool _isCompleteCode = false;

        public decimal SSPriceBookPrice = 0;
        public string SSPriceBookSequance = string.Empty;
        public string SSPriceBookItemSequance = string.Empty;
        public string SSIsLevelSerialized = string.Empty;
        public string SSPromotionCode = string.Empty;
        public string SSCirculerCode = string.Empty;
        public Int32 SSPRomotionType = 0;
        public Int32 SSCombineLine = 0;

        Dictionary<decimal, decimal> ManagerDiscount = null;


        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private string DefaultInvoiceType = string.Empty;
        private string DefaultStatus = string.Empty;
        private string DefaultBin = string.Empty;
        MasterItem _itemdetail = null;
        private List<MasterItemTax> MainTaxConstant = null;
        private List<ReptPickSerials> _promotionSerial = null;
        private List<ReptPickSerials> _promotionSerialTemp = null;

        private bool _isBackDate = false;

        MasterProfitCenter _MasterProfitCenter = null;
        List<PriceDefinitionRef> _PriceDefinitionRef = null;
        const string InvoiceBackDateName = "SALESENTRY";

        DataTable PassportTable;
        MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        CashGeneralEntiryDiscountDef GeneralDiscount = null;
        DataTable _tblExecutive = null;

        private bool _isNewPromotionProcess = false;
        private List<PriceDetailRef> _PriceDetailRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefNormal = null;
        private DateTime _serverDt = DateTime.Now.Date;
        private Boolean _isStrucBaseTax = false;  //kapila 22/4/2016
        bool _IsVirtualItem = false;
        bool IsInvoiceCompleted = false;

        #endregion

        #region Ad-hoc Session
        private void Ad_hoc_Session()
        {
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "ADMIN";

            //BaseCls.GlbUserComCode = "SGL";
            //BaseCls.GlbUserDefLoca = "SGMTR";
            //BaseCls.GlbUserDefProf = "SGMTR";
            //BaseCls.GlbUserID = "PRABHATH";

            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "ADMIN";

        }
        #endregion

        public DutyFreeInvoice()
        {
            //Ad_hoc_Session();
            InitializeComponent();
            if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
            {
                MessageBox.Show("You do not have assigned a profit center. " + this.Text + " is terminating now!", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
            if (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca))
            {
                MessageBox.Show("You do not have assigned a delivery location. " + this.Text + " is de-activating delivery option now!", "De-activate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                chkDeliverLater.Enabled = false;
            }
            else chkDeliverLater.Enabled = true;
            LoadCachedObjects();
            SetGridViewAutoColumnGenerate();
            SetPanelSize();
            InitializeValuesNDefaultValueSet();
            LoadCurrancyCodes();
            LoadCountries();
            CreatePassportTable();
        }

        private void cmbExecutive_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue))) return;

            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {

                if (_tblExecutive != null)
                {
                    var _find = (from DataRow _l in _tblExecutive.Rows where _l.Field<string>("esep_first_name") == cmbExecutive.Text select _l).ToList();
                    if (_find != null && _find.Count > 0)
                    {
                        txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the correct sales executive", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtExecutive.Text = string.Empty;
                        cmbExecutive.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the correct sales executive", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExecutive.Text = string.Empty;
                cmbExecutive.SelectedIndex = -1;
            }
        }

        private void LoadExecutive()
        {
            cmbExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.ValueMember = "esep_epf"; cmbExecutive.DisplayMember = "esep_first_name";
                if (_lst != null && _lst.Count > 0) cmbExecutive.DataSource = _lst.CopyToDataTable(); cmbExecutive.DropDownWidth = 200;
                if (_tblExecutive != null)
                {
                    cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man;
                }
                //MSR channel load default executive with null record
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                {
                    //DataRow dr = _tblExecutive.NewRow();
                    //dr["esep_epf"] = "";
                    //dr["esep_first_name"] = "";
                    //_tblExecutive.Rows.Add(dr);
                    cmbExecutive.SelectedIndex = -1;
                }

                txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                Parallel.ForEach(_lst, x => _string.Add(x.Field<string>("esep_first_name")));
                cmbExecutive.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbExecutive.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbExecutive.AutoCompleteCustomSource = _string;
                AutoCompleteStringCollection _string0 = new AutoCompleteStringCollection();
                var _lst0 = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "TECH").ToList();
                cmbTechnician.ValueMember = "esep_epf";
                cmbTechnician.DisplayMember = "esep_first_name";
                if (_lst0 != null && _lst0.Count > 0) cmbTechnician.DataSource = _lst0.CopyToDataTable();
                cmbTechnician.DropDownWidth = 200; if (_lst0 != null && _lst0.Count > 0)
                { Parallel.ForEach(_lst0, x => _string0.Add(x.Field<string>("esep_first_name"))); cmbTechnician.AutoCompleteSource = AutoCompleteSource.CustomSource; cmbTechnician.AutoCompleteMode = AutoCompleteMode.SuggestAppend; cmbTechnician.AutoCompleteCustomSource = _string0; }
                var _manname = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_epf") == _MasterProfitCenter.Mpc_man).ToList();
                if (_manname != null && _manname.Count > 0) { string _name = _manname[0].Field<string>("esep_first_name") + " (" + _MasterProfitCenter.Mpc_man + ")"; this.Text = "Invoice | Manager : " + _name; }
            }
        }

        private void LoadCountries()
        {
            DataTable _dt = CHNLSVC.General.GetCountry();
            cmbCountry.DataSource = _dt;
            cmbCountry.ValueMember = "MCU_CD";
            cmbCountry.DisplayMember = "MCU_DESC";
        }

        private void CreatePassportTable()
        {
            PassportTable = new DataTable();
            PassportTable.Columns.Add("FlightNo");
            PassportTable.Columns.Add("PassportNo");
        }

        private void LoadCurrancyCodes()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _cur;
                cmbCurrancy.DataSource = _source;
                cmbCurrancy.DisplayMember = "Mcr_cd";
                cmbCurrancy.ValueMember = "Mcr_cd";

                cmbCurrancy.SelectedValue = _MasterProfitCenter.Mpc_def_exrate;
            }
        }

        #region Rooting for Form Load Event Bind and Default Value
        private void LoadCachedObjects()
        {
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
        }
        private void SetPanelSize()
        {
            pnlMultipleItem.Size = new Size(610, 155);
            pnlMultiCombine.Size = new Size(597, 140);
            pnlConsumerPrice.Size = new Size(553, 137);
            pnlPriceNPromotion.Size = new Size(859, 305);
            pnlDeliveryInstruction.Size = new Size(441, 246);
            pnlInventoryCombineSerialPick.Size = new Size(648, 242);
            pnlDiscountRequest.Size = new Size(484, 143);
            pnlGroupSale.Size = new Size(200, 35);
        }
        private void SetGridViewAutoColumnGenerate()
        {
            gvInvoiceItem.AutoGenerateColumns = false;
            gvPopSerial.AutoGenerateColumns = false;
            gvDisItem.AutoGenerateColumns = false;
            //gvMultiCombineItem.AutoGenerateColumns = false;
            //gvMultipleItem.AutoGenerateColumns = false;
            gvNormalPrice.AutoGenerateColumns = false;
            gvPopComItem.AutoGenerateColumns = false;
            gvPopComItemSerial.AutoGenerateColumns = false;
            gvPopConsumPricePick.AutoGenerateColumns = false;
            gvPopSerial.AutoGenerateColumns = false;
            gvPromotionItem.AutoGenerateColumns = false;
            gvPromotionPrice.AutoGenerateColumns = false;
            gvPromotionSerial.AutoGenerateColumns = false;
            gvGiftVoucher.AutoGenerateColumns = false;
        }
        private void LoadInvoiceProfitCenterDetail()
        {
            if (_MasterProfitCenter != null)
                if (_MasterProfitCenter.Mpc_cd != null)
                {
                    if (!_MasterProfitCenter.Mpc_edit_price) txtUnitPrice.ReadOnly = true;
                    txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer;
                    //txtDelLocation.Text = _MasterProfitCenter.Mpc_def_loc; TODO : in delivery instruction, remove comment
                    lblCurrency.Text = _MasterProfitCenter.Mpc_def_exrate;
                    txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                }

        }
        private void LoadPriceDefaultValue()
        {
            if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0)
                {
                    var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                    if (_defaultValue != null)
                        if (_defaultValue.Count > 0)
                        {
                            DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                            DefaultBook = _defaultValue[0].Sadd_pb;
                            DefaultLevel = _defaultValue[0].Sadd_p_lvl;
                            DefaultStatus = _defaultValue[0].Sadd_def_stus;
                            DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                            LoadInvoiceType();
                            LoadPriceBook(cmbInvType.Text);
                            LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                            LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                            CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                        }
                }
            cmbTitle.SelectedIndex = 0;
        }
        private void VariableInitialization()
        {
            InvItm_Qty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UnitAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisRate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_TaxAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_LineAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;



            InvItm_Qty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UnitAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_TaxAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_LineAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            InvItm_Qty.DefaultCellStyle.Format = "0.0000";
            InvItm_UPrice.DefaultCellStyle.Format = "N";
            InvItm_UnitAmt.DefaultCellStyle.Format = "N";
            InvItm_DisRate.DefaultCellStyle.Format = "N";
            InvItm_DisAmt.DefaultCellStyle.Format = "N";
            InvItm_TaxAmt.DefaultCellStyle.Format = "N";
            InvItm_LineAmt.DefaultCellStyle.Format = "N";


            btnSave.Enabled = true;
            txtInvoiceNo.Enabled = true;

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
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            MainTaxConstant = new List<MasterItemTax>();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _masterItemComponent = new List<MasterItemComponent>();
            _newRecieptItem = new List<RecieptItem>();

            //_paidAmount = 0;
            _lineNo = 0;
            //_toBePayNewAmount = 0;

            SetDecimalTextBoxForZero(true, true);

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;

            _isCompleteCode = false;
            chkOpenDelivery.Enabled = false;

        }
        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("1");
            txtTaxAmt.Text = FormatToCurrency("0");
            if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isAccBal) { lblAccountBalance.Text = FormatToCurrency("0"); lblAvailableCredit.Text = FormatToCurrency("0"); }
        }
        private void VaribleClear()
        {
            _lineNo = 1;
            _isEditPrice = false;
            _isEditDiscount = false;

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;

            //_paidAmount = 0;
            SSCombineLine = 1;

        }
        private void LoadCancelPermission()
        {
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "SACAN"))
            {
                btnCancel.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = false;
            }
        }
        private void LoadPayMode()
        {
            ucPayModes1.InvoiceType = cmbInvType.Text.Trim();
            ucPayModes1.IsDutyFree = true;
            ucPayModes1.LoadPayModes();
            ucPayModes1.Amount.Enabled = false;
            ucPayModes1.AddButton.Visible = false;
        }
        private void BackDatePermission()
        {
            bool _alwCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _alwCurrentTrans);
        }
        private void CheckPrintStatus()
        {
            if (_MasterProfitCenter != null)
                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com))
                    if (_MasterProfitCenter.Mpc_print_payment)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;

        }
        private void InitializeValuesNDefaultValueSet()
        {
            txtDate.Value = CHNLSVC.Security.GetServerDateTime().AddHours(_MasterProfitCenter.Mpc_add_hours);
            _serverDt = CHNLSVC.Security.GetServerDateTime().AddHours(_MasterProfitCenter.Mpc_add_hours);
            VaribleClear();
            VariableInitialization();
            LoadInvoiceProfitCenterDetail();
            LoadPriceDefaultValue();
            LoadCancelPermission();
            SetDecimalTextBoxForZero(true, true);
            LoadPayMode();
            LoadControl();
            lblBackDateInfor.Text = string.Empty;

            ResetDeliveryInstructionToOriginalCustomer();
            chkDeliverLater_CheckedChanged(null, null);
            CheckPrintStatus();
            LoadExecutive();
            cmbExecutive.SelectedIndex = -1;
            txtNationality.Text = "SL";

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
        }
        #endregion

        #region Rooting for Delivery Now Event
        private void chkDeliverLater_CheckedChanged(object sender, EventArgs e)
        {
            txtDelLocation.Text = BaseCls.GlbUserDefLoca;
            chkOpenDelivery.Checked = false;
            if (chkDeliverLater.Checked)
            {
                chkOpenDelivery.Enabled = true;
                txtDelLocation.Enabled = true;
                btnSearchDelLocation.Enabled = true;
            }
            else
            {
                chkOpenDelivery.Enabled = false;
                txtDelLocation.Enabled = false;
                btnSearchDelLocation.Enabled = false;
            }
        }
        #endregion

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbInvType.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbInvType.Text + seperator + cmbBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + Convert.ToDateTime(txtDate.Text).ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InventoryItem:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCustomer;
            _CommonSearch.ShowDialog();
            txtCustomer.Select();
        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtNIC;
            _CommonSearch.ShowDialog();
            txtNIC.Select();
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtMobile;
            _CommonSearch.ShowDialog();
            txtMobile.Select();
        }

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
            DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
            _CommonSearch.ShowDialog();
            txtInvoiceNo.Select();
             */
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchInvoice(_CommonSearch.SearchParams, null, null, txtDate.Value.Date.AddMonths(-1), txtDate.Value.Date);
                _CommonSearch.dtpFrom.Value = txtDate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = txtDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                //_commonSearch.IsSearchEnter = true; 
                this.Cursor = Cursors.Default;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); this.Cursor = Cursors.Default; CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
            DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSerialNo;
            _CommonSearch.ShowDialog();
            txtSerialNo.Select();
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
            DataTable _result = CHNLSVC.CommonSearch.GetInventoryItem(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItem;
            _CommonSearch.txtSearchbyword.Text = txtItem.Text;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtItem.Select();
        }

        private void btnSearch_Executive_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
            DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtExecutive;
            _CommonSearch.ShowDialog();
            txtExecutive.Select();
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCustomer.Focus();
        }

        private void txtInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPPNo.Focus();
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPPNo.Focus();
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbExecutive.Focus();
        }

        private void txtExecutive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Executive_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtManualRefNo.Focus();
        }

        private void txtExecutive_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Executive_Click(null, null);
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Serial_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtItem.Focus();
        }

        private void txtSerialNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial_Click(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                cmbBook.Focus();
        }

        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            //
            //if (pnlGroupSale.Visible)
            //    pnlGroupSale.Visible = false;
            //else
            //    pnlGroupSale.Visible = true;
            pnlFlight.Visible = true;
            pnlMain.Enabled = false;


        }

        private void btnSearch_Group_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbInvType.Text.Trim())) { MessageBox.Show("Please select the invoice type!", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); cmbInvType.Focus(); return; }
            if (cmbInvType.Text.Trim() != "CRED") { MessageBox.Show("Group sales only available for credit sales!", "Credit Sale", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
            DataTable _result = CHNLSVC.CommonSearch.GetGroupSaleSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtGroup;
            _CommonSearch.ShowDialog();
            txtGroup.Select();
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Group_Click(null, null);
        }

        private void txtGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Group_Click(null, null);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #endregion

        #region Rooting for Key Down Navigation

        #region Event Control
        private void LoadControl()
        {
            txtAddress1.GotFocus += txtAddress_GotFocus;
            txtAddress2.GotFocus += txtAddress_GotFocus;

            txtAddress1.LostFocus += txtAddress_LostFocus;
            txtAddress2.LostFocus += txtAddress_LostFocus;
            btnAddItem.GotFocus += btnAddItem_GotFocus;
            btnAddItem.LostFocus += btnAddItem_LostFocus;
            btnAddItem.MouseHover += btnAddItem_GotFocus;
            btnAddItem.MouseLeave += btnAddItem_LostFocus;

            txtInvoiceNo.DragDrop += new DragEventHandler(txtItem_DragDrop);
            txtInvoiceNo.DragOver += new DragEventHandler(txtItem_DragEnter);
        }

        const byte CtrlMask = 8;
        private void txtItem_DragDrop(object sender, DragEventArgs e)
        {
            txtInvoiceNo.Text = e.Data.GetData(DataFormats.Text).ToString().Trim();
            if ((e.KeyState & CtrlMask) != CtrlMask)
            {
                CheckInvoiceNo(null, null);
            }
        }
        private void txtItem_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                if ((e.KeyState & CtrlMask) == CtrlMask)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        private void btnAddItem_GotFocus(object sender, EventArgs e)
        {
            btnAddItem.BackColor = Color.Yellow;
        }

        private void btnAddItem_LostFocus(object sender, EventArgs e)
        {
            btnAddItem.BackColor = Color.Transparent;
        }

        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbInvType.Focus();
        }

        private void cmbInvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDocRefNo.Focus();
        }

        private void txtDocRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInvoiceNo.Focus();
        }

        private void txtCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress1.Focus();
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress2.Focus();
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusFlight.Focus();
            }
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbLevel.Focus();
        }

        private void cmbLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUnitPrice.Focus();
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUnitAmt.Focus();
        }

        private void txtUnitAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisRate.Focus();
        }

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisAmt.Focus();
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTaxAmt.Focus();
        }

        private void txtTaxAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLineTotAmt.Focus();
        }

        private void txtLineTotAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }
        #endregion

        #region Rooting for check group sale code and its value loading
        private void txtGroup_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGroup.Text)) return;
            if (string.IsNullOrEmpty(cmbInvType.Text.Trim())) { MessageBox.Show("Please select the invoice type!", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); cmbInvType.Focus(); return; }
            if (cmbInvType.Text.Trim() != "CRED") { MessageBox.Show("Group sales only available for credit sales!", "Credit Sale", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            GroupSaleHeader _groupSale = CHNLSVC.Sales.GetGroupSaleHeaderDetails(txtGroup.Text.Trim());
            if (_groupSale != null)
                if (!string.IsNullOrEmpty(_groupSale.Hgr_com))
                {
                    ClearTop2p0();
                    ClearTop2p1();
                    ClearTop2p2();
                    txtCustomer.Text = _groupSale.Hgr_Grup_com;
                    LoadCustomerDetailsByCustomer(null, null);
                    return;
                }

            MessageBox.Show("Please check the group sale code.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Rooting for Manual Document Process
        protected void IsValidManualNo(object sender, EventArgs e)
        {
            if (txtManualRefNo.Text != "")
            {
                Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_INV", Convert.ToInt32(txtManualRefNo.Text));
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtManualRefNo.Text = "";
                    txtManualRefNo.Focus();
                }
            }
            else
            {
                if (chkManualRef.Checked == true)
                {
                    MessageBox.Show("Invalid Manual Document Number !", "Manual Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtManualRefNo.Focus();
                }
            }
        }
        protected void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualRef.Checked == true)
            {
                txtManualRefNo.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_INV");
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
                txtManualRefNo.Enabled = true;
            }
        }
        #endregion

        #region Rooting for Re-call Invoice
        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            cmbInvType.Text = _hdr.Sah_inv_tp;
            txtDate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy"); ;
            txtCustomer.Text = _hdr.Sah_cus_cd;
            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCustomer.Text);
            txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            lblCurrency.Text = _hdr.Sah_currency;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            txtDocRefNo.Text = _hdr.Sah_ref_doc;
            txtPoNo.Text = _hdr.Sah_anal_4;
        }
        private void RecallInvoice()
        {

            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);
            if (_hdr == null) { MessageBox.Show("Please select the valid invoice", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInvoiceNo.Text = string.Empty; return; }
            if (_hdr.Sah_pc != BaseCls.GlbUserDefProf.ToString()) { MessageBox.Show("Please select the valid invoice", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInvoiceNo.Text = string.Empty; return; }
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


            List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
            ucPayModes1.RecieptItemList = _itms;
            ucPayModes1.LoadRecieptGrid();

            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            ucPayModes1.LoadData();




        }
        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2.Text = _masterBusinessCompany.Mbe_add2;

            txtAddress1.Text = txtAddress1.Text.Replace(Environment.NewLine, "");
            txtAddress2.Text = txtAddress2.Text.Replace(Environment.NewLine, "");
            //txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            txtPPNo.Text = _masterBusinessCompany.Mbe_pp_no;
            if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_country_cd))
            {
                cmbCountry.SelectedValue = _masterBusinessCompany.Mbe_country_cd;
            }
            if (dateTimePickerDateOfBirth.MinDate <= _masterBusinessCompany.Mbe_dob)
            {
                dateTimePickerDateOfBirth.Value = _masterBusinessCompany.Mbe_dob;
            }


            if (_isRecall == false)
            {
                if (string.IsNullOrEmpty(txtDelAddress1.Text)) txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                if (string.IsNullOrEmpty(txtDelAddress2.Text)) txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                if (string.IsNullOrEmpty(txtDelCustomer.Text) || txtDelCustomer.Text.Trim() == "CASH") txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                if (string.IsNullOrEmpty(txtDelName.Text) || txtDelName.Text.Trim().Length <= 6) txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAddress1.Text = _hdr.Sah_cus_add1;
                txtAddress2.Text = _hdr.Sah_cus_add2;

                txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                txtDelCustomer.Text = _hdr.Sah_d_cust_cd;
                txtDelName.Text = _hdr.Sah_d_cust_name;
            }

            if (_isRecall == false)
            {
                if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
            }

            if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
            if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
            GetNICGender();
            if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
            else
            {
                string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                bool _exist = cmbTitle.Items.Contains(_title);
                if (_exist)
                    cmbTitle.Text = _title;
            }
            //BindLoyalities(ddlPayLoyality, txtCustomer.Text, string.Empty, DateTime.Now.Date);
        }
        //Display Account Balance
        private void ViewCustomerAccountDetail(string _customer)
        {
            if (string.IsNullOrEmpty(_customer.Trim())) return;
            if (_customer != "CASH")
            {
                CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                lblAccountBalance.Text = FormatToCurrency(Convert.ToString(_account.Saca_acc_bal));
                lblAvailableCredit.Text = FormatToCurrency(Convert.ToString((_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal)));
            }
        }
        protected void CheckInvoiceNo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCustomer.Focus(); return; }
            RecallInvoice();

        }
        #endregion

        #region Rooting for Calculation Text Box Value, Grand Total & Tax
        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim())));

                decimal _vatPortion = TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    _disAmt = _totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100);
                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    _totalAmount = _totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt;
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
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
            //TODO: remove remark, when apply payment UC
            //txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //lblPayBalance.Text = lblGrndTotalAmount.Text;

        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false)
                    {
                        if (_isStrucBaseTax == true)       //kapila 22/4/2016
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString());
                    }
                    else
                    {
                        if (_isStrucBaseTax == true)       //kapila 22/4/2016
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    }

                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxfaction == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;


            return _pbUnitPrice;
        }
        #endregion

        #region Rooting for Customer Detail Load / Collect Advance Detail
        private void GetNICGender()
        {
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = (nicarray[2]).ToString();
            if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
            {
                cmbTitle.Text = "Ms.";
            }
            else
            {
                cmbTitle.Text = "Mr.";
            }
        }
        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
            {
                MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCusName.Clear();
                txtAddress1.Clear();
                txtAddress2.Clear();
                //txtMobile.Clear();
                txtNIC.Clear();
                chkTaxPayable.Checked = false;
                txtCustomer.Clear();
                txtCustomer.Focus();
                return;
            }

            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            }

            if (_masterBusinessCompany != null)
            {


                if (_masterBusinessCompany.Mbe_cd == "CASH")
                {
                    txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                    txtCusName.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    // txtMobile.Text = "";
                    txtNIC.Text = "";
                    chkTaxPayable.Checked = false;
                }
                else
                {
                    LoadTaxDetail(_masterBusinessCompany);
                    SetCustomerAndDeliveryDetails(false, null);

                }
            }
            else
            {
                MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomer.Text = "";
                txtCusName.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                // txtMobile.Text = "";
                txtNIC.Text = "";
                chkTaxPayable.Checked = false;
                txtCustomer.Focus();
                return;
            }

            ViewCustomerAccountDetail(txtCustomer.Text);
        }
        protected void LoadCustomerDetailsByPP(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPPNo.Text)) return;
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtPPNo.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, txtPPNo.Text, null, BaseCls.GlbUserComCode);
            }


            //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
            if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
            {
                LoadTaxDetail(_masterBusinessCompany);
                SetCustomerAndDeliveryDetails(false, null);
            }
            else
            {
                txtCusName.Focus();
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
                    MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNIC.Text = ""; return;
                }
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");

            }
            DOBGenarate();
            //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
            if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
            {
                LoadTaxDetail(_masterBusinessCompany);
                SetCustomerAndDeliveryDetails(false, null);
            }
            else
            {
                GetNICGender();
                if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
                //txtMobile.Focus();
                return;
            }
            ViewCustomerAccountDetail(txtCustomer.Text);
        }
        protected void LoadCustomerDetailsByMobile(object sender, EventArgs e)
        {

        }
        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }
        private void cmbTitle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
            {
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                    lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                }
            }
            else
            {
                MessageBox.Show("Item details not found. Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor = DefaultCursor;
                _isValid = false;
            }
                

            return _isValid;
        }
        #endregion

        #region Rooting for Price Book/Level/Status and Invoice Type

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataSource = _books;
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook)) cmbBook.Text = DefaultBook;
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbLevel.DataSource = _levels;
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text)) cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }

        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    cmbInvType.DataSource = _types;
                    cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbInvType.Text = DefaultInvoiceType;

                    //var _val = (from _p in _type.AsEnumerable()
                    //            select new
                    //            {
                    //                Code = _p.Field<string>(0),
                    //                Description = _p.Field<string>(1)

                    //            }).ToList();

                    //multiColumnCombo1._queryObject = _val;
                    //multiColumnCombo1.DataSource = _type;

                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }

        private DataTable _levelStatus = null;
        private bool LoadLevelStatus(string _invType, string _book, string _level)
        {
            _levelStatus = null;
            bool _isAvailable = false;
            string _initPara = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            _levelStatus = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(_initPara, null, null);
            if (_levelStatus != null)
                if (_levelStatus.Rows.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    _types.Add("");
                    cmbStatus.DataSource = _types;
                    cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbStatus.Text = DefaultStatus;
                    //Load Level definition
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
            return _isAvailable;
        }

        private void CheckPriceLevelStatusForDoAllow(string _level, string _book)
        {
            if (!string.IsNullOrEmpty(_level.Trim()) && !string.IsNullOrEmpty(_book.Trim()))
            {
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
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

        private void CheckLevelStatusWithInventoryStatus()
        {
            if (IsPriceLevelAllowDoAnyStatus == false)
            {
                string _invoiceStatus = cmbStatus.Text.Trim();
                string _inventoryStatus = string.Empty;
                if (chkDeliverLater.Checked == false)
                    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                    {
                        //pick inventory status
                        if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                        {
                            List<InventoryLocation> _balance = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), _invoiceStatus);
                            if (_balance == null)
                            {
                                MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                cmbStatus.Text = "";
                                return;
                            }
                            if (_balance.Count <= 0)
                            {
                                MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                cmbStatus.Text = "";
                                return;
                            }

                        }
                    }
                    else
                    {
                        //pick serial status
                        DataTable _serialstatus = CHNLSVC.Inventory.GetAvailableItemStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, DefaultBin, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                        if (_serialstatus != null)
                            if (_serialstatus.Rows.Count > 0)
                            {
                                _inventoryStatus = _serialstatus.Rows[0].Field<string>("ins_itm_stus");

                                if (_levelStatus != null)
                                    if (_levelStatus.Rows.Count > 0)
                                    {
                                        var _exist = _levelStatus.AsEnumerable().Where(x => x.Field<string>("Code") == _invoiceStatus).Select(y => y.Field<string>("Code")).ToList();
                                        if (_exist != null)
                                            if (_exist.Count > 0)
                                            {
                                                string _code = Convert.ToString(_exist[0]);
                                                cmbStatus.Text = _code;
                                                return;
                                            }

                                    }

                                if (!string.IsNullOrEmpty(_inventoryStatus))
                                    if (!_inventoryStatus.Equals(_invoiceStatus))
                                    {
                                        MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        cmbStatus.Text = "";
                                        return;
                                    }
                            }
                    }
            }
        }

        private void ClearPriceTextBox()
        {
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
        }
        private void cmbBook_Leave(object sender, EventArgs e)
        {
            LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
            ClearPriceTextBox();
        }

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
            ClearPriceTextBox();
            SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            CheckLevelStatusWithInventoryStatus();
        }
        #endregion

        #region Rooting for Serial Validate and Load other detail
        private void CheckSerialAvailability(object sender, EventArgs e)
        {
            if (pnlMain.Enabled == false) return;
            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) return;
            if (txtSerialNo.Text.Trim().ToUpper() == "N/A" || txtSerialNo.Text.Trim().ToUpper() == "NA") { this.Cursor = Cursors.Default; MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtSerialNo.Clear(); txtItem.Clear(); return; }

            txtItem.Text = string.Empty;
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;

            DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
            Int32 _isAvailable = _multiItemforSerial.Rows.Count;

            if (_isAvailable <= 0)
            {
                MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSerialNo.Clear();
                txtItem.Clear();
                return;
            }

            if (_isAvailable > 1)
            {
                pnlMain.Enabled = false;
                pnlMultipleItem.Visible = true;
                gvMultipleItem.DataSource = _multiItemforSerial;
                return;
            }

            string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
            if (LoadMultiCombinItem(_item) == false)
            {
                LoadItemDetail(_item);
                txtItem.Text = _item;
                txtQty.Text = FormatToQty("1");
            }


        }
        private bool LoadMultiCombinItem(string _item)
        {
            bool _isManyItem = false;
            if (LoadMultiCombineItem(_item))
            {
                _isManyItem = true;
            }
            return _isManyItem;
        }

        #region Multi-Item for one serial

        #region Panel Movement
        Point muItemPoint = new Point();
        private void pnlMultipleItem_MouseDown(object sender, MouseEventArgs e)
        {
            muItemPoint.X = e.X;
            muItemPoint.Y = e.Y;

        }

        private void pnlMultipleItem_MouseUp(object sender, MouseEventArgs e)
        {
            pnlMultipleItem.Location = new Point(e.X - muItemPoint.X + pnlMultipleItem.Location.X, e.Y - muItemPoint.Y + pnlMultipleItem.Location.Y);
        }
        #endregion

        private void cmsMuItem_Description_Click(object sender, EventArgs e)
        {
            if (cmsMuItem_Description.CheckState == CheckState.Checked) gvMultipleItem.Columns["MuItm_Description"].Visible = true; else gvMultipleItem.Columns["MuItm_Description"].Visible = false;
        }
        private void cmsMuItem_Brand_Click(object sender, EventArgs e)
        {
            if (cmsMuItem_Brand.CheckState == CheckState.Checked) gvMultipleItem.Columns["MuItm_Brand"].Visible = true; else gvMultipleItem.Columns["MuItm_Brand"].Visible = false;
        }
        private void cmsMuItem_Model_Click(object sender, EventArgs e)
        {
            if (cmsMuItem_Model.CheckState == CheckState.Checked) gvMultipleItem.Columns["MuItm_Model"].Visible = true; else gvMultipleItem.Columns["MuItm_Model"].Visible = false;
        }
        private void btnPnlMuItemClose_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
        }
        private void gvMultipleItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultipleItem.RowCount > 0)
            {
                string _item = gvMultipleItem.SelectedRows[0].Cells["MuItm_Item"].Value.ToString();
                string _serial = gvMultipleItem.SelectedRows[0].Cells["MuItm_Serial"].Value.ToString();
                txtItem.Text = _item.Trim();
                LoadMultiCombinItem(_item);
                btnPnlMuItemClose_Click(null, null);
            }
        }
        #endregion

        #region Multi-Combine Item
        private void btnPnlMuComItemClose_Click(object sender, EventArgs e)
        {
            pnlMultiCombine.Visible = false;
            pnlMain.Enabled = true;
            txtItem.Clear();
            txtSerialNo.Clear();
        }
        private bool LoadMultiCombineItem(string _item)
        {
            bool _isMultiCom = false;
            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
            if (_invnetoryCombinAnalalize != null)
                if (_invnetoryCombinAnalalize.Rows.Count > 0)
                {
                    gvMultiCombineItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    gvMultiCombineItem.DataSource = _invnetoryCombinAnalalize;
                    _isMultiCom = true;
                    pnlMain.Enabled = false;
                    pnlMultiCombine.Visible = true;
                    gvMultiCombineItem.Focus();
                }
            return _isMultiCom;
        }
        private void gvMultiCombineItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultiCombineItem.RowCount > 0)
            {
                string _item = gvMultiCombineItem.SelectedRows[0].Cells["Item"].Value.ToString();
                txtItem.Text = _item.Trim();
                txtQty.Text = FormatToQty("1");
                bool _isValid = LoadItemDetail(_item);
                if (_isValid)
                {
                    pnlMultiCombine.Visible = false;
                    pnlMain.Enabled = true;
                    CheckQty();
                    btnAddItem.Focus();
                }
                //pnlMultiCombine.Visible = false;
                //pnlMain.Enabled = true;
                //CheckQty();
                //btnAddItem.Focus();
            }
        }
        private void gvMultiCombineItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvMultiCombineItem.RowCount > 0 && e.KeyCode == Keys.Enter)
            {
                string _item = gvMultiCombineItem.SelectedRows[0].Cells["Item"].Value.ToString();
                txtItem.Text = _item.Trim();
                txtQty.Text = FormatToQty("1");
                LoadItemDetail(_item);
                pnlMultiCombine.Visible = false;
                pnlMain.Enabled = true;
                CheckQty();
                btnAddItem.Focus();
            }
        }
        #region Panel Movement
        Point muComItemPoint = new Point();
        private void pnlMultiCombine_MouseDown(object sender, MouseEventArgs e)
        {
            muComItemPoint.X = e.X;
            muComItemPoint.Y = e.Y;

        }

        private void pnlMultiCombine_MouseUp(object sender, MouseEventArgs e)
        {
            pnlMultiCombine.Location = new Point(e.X - muComItemPoint.X + pnlMultiCombine.Location.X, e.Y - muComItemPoint.Y + pnlMultiCombine.Location.Y);
        }
        #endregion
        #endregion

        #endregion

        #region Rooting for Delivery Instruction Management
        private void btnDeliveryInstruction_Click(object sender, EventArgs e)
        {
            if (pnlDeliveryInstruction.Visible)
            {
                pnlDeliveryInstruction.Visible = false;
                pnlMain.Enabled = true;
            }
            else
            {
                pnlDeliveryInstruction.Visible = true;
                pnlMain.Enabled = false;
            }
        }
        private void btnPnlDelInsCancel_Click(object sender, EventArgs e)
        {
            ResetDeliveryInstructionToOriginalCustomer();
            btnDeliveryInstruction_Click(null, null);
        }
        private void ClearDeliveryInstruction(bool _isReset)
        {

            txtDelLocation.Clear();
            txtDelCustomer.Clear();
            txtDelName.Clear();
            txtDelAddress1.Clear();
            txtDelAddress2.Clear();

            if (_isReset)
            {
                ResetDeliveryInstructionToOriginalCustomer();
            }

        }
        private void ResetDeliveryInstructionToOriginalCustomer()
        {
            txtDelLocation.Text = BaseCls.GlbUserDefLoca;
            txtDelCustomer.Text = txtCustomer.Text;
            txtDelName.Text = txtCusName.Text;
            txtDelAddress1.Text = txtAddress1.Text;
            txtAddress2.Text = txtAddress2.Text;
            chkOpenDelivery.Checked = false;
        }
        private void btnPnlDelInsClear_Click(object sender, EventArgs e)
        {
            ClearDeliveryInstruction(false);
        }
        private void chkOpenDelivery_CheckedChanged(object sender, EventArgs e)
        {


            if (_MasterProfitCenter != null)
                if (_MasterProfitCenter.Mpc_com != null)
                {
                    ;
                    if (string.IsNullOrEmpty(_MasterProfitCenter.Mpc_def_loc))
                    {
                        if (chkOpenDelivery.Checked == false)
                        {
                            MessageBox.Show("Default location not setup. You have to contact inventory department.", "Default Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDelLocation.Text = BaseCls.GlbUserDefLoca;
                            return;
                        }
                    }
                    else
                    {
                        if (chkOpenDelivery.Checked == false)
                            txtDelLocation.Text = _MasterProfitCenter.Mpc_def_loc;
                        else
                            txtDelLocation.Clear();
                    }
                }



        }
        private void btnPnlDelInsReset_Click(object sender, EventArgs e)
        {
            ResetDeliveryInstructionToOriginalCustomer();
        }
        private void btnPnlDelInsConfirm_Click(object sender, EventArgs e)
        {
            btnDeliveryInstruction_Click(null, null);
        }
        private void btnSearchDelLocation_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDelLocation;
            _CommonSearch.ShowDialog();
            txtDelLocation.Select();
        }
        private void txtDelLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchDelLocation_Click(null, null);
        }
        private void txtDelLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchDelLocation_Click(null, null);
        }
        private void btnSearchDelCustomer_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDelCustomer;
            _CommonSearch.ShowDialog();
            txtDelCustomer.Select();
        }
        private void txtDelCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchDelCustomer_Click(null, null);
        }
        private void txtDelCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchDelCustomer_Click(null, null);
        }
        #region Panel Movement
        Point delInstructionPoint = new Point();
        private void pnlDeliveryInstruction_MouseDown(object sender, MouseEventArgs e)
        {
            delInstructionPoint.X = e.X;
            delInstructionPoint.Y = e.Y;

        }

        private void pnlDeliveryInstruction_MouseUp(object sender, MouseEventArgs e)
        {
            pnlDeliveryInstruction.Location = new Point(e.X - delInstructionPoint.X + pnlDeliveryInstruction.Location.X, e.Y - delInstructionPoint.Y + pnlDeliveryInstruction.Location.Y);
        }
        #endregion
        #endregion

        #region Rooting for Item Code Validation
        bool _isItemChecking = false;
        private void CheckItemCode(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            if (_isItemChecking) { _isItemChecking = false; return; }
            _isItemChecking = true;

            if (!LoadItemDetail(txtItem.Text.Trim()))
            {
                MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Clear();
                txtItem.Focus();
                if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";
                return;
            }
            if (_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            {
                MessageBox.Show("You have to select the serial no for the serialized item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";
            //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) txtQty.Text = FormatToQty("1"); else txtQty.Text = FormatToQty("1");

            //updated by akila 2017/08/04  

            IsVirtual(_itemdetail.Mi_itm_tp);
            if (_IsVirtualItem)
            {
                bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
                if (block)
                {
                    txtItem.Text = "";
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                txtQty.Text = FormatToQty("1");
            }

            //txtQty.Text = FormatToQty("1");
            CheckQty();
            btnAddItem.Focus();
        }
        #endregion

        private bool IsVirtual(string _type)
        {
            if (_type == "V") { _IsVirtualItem = true; return true; } else { _IsVirtualItem = false; return false; }
        }

        private bool _isBlocked = false;
        private bool CheckBlockItem(string _item, int _pricetype, bool _isCombineItemAddingNow)
        {
            if (_isCombineItemAddingNow) return false;
            _isBlocked = false;
            if (_priceBookLevelRef.Sapl_is_serialized == false)
            {
                MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
                if (_block != null)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(_item + " item already blocked by the Costing Dept.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isBlocked = true;
                }
            }
            return _isBlocked;
        }

        #region Rooting for Invoice Type Validation
        private void cmbInvType_Leave(object sender, EventArgs e)
        {
            LoadPriceBook(cmbInvType.Text.Trim());
            LoadPriceLevel(cmbInvType.Text.Trim(), cmbBook.Text.Trim());
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            LoadPayMode();
        }
        #endregion

        #region Rooting for Tax Definition/Checking
        /// <summary>
        /// This Object Contain the total fraction of the VAT
        /// </summary>
        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
        }
        private decimal CheckSubItemTax(string _item)
        {
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                TaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
                if (TaxConstant != null)
                    if (TaxConstant.Count > 0)
                        _fraction = TaxConstant[0].Mict_tax_rate;
            }
            return _fraction;
        }

        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            if (!_isCompleteCode)
            {
                List<MasterItemTax> _tax = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila 22/4/2016
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                }
                else
                    _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);

                if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    _IsTerminate = true;
                if (_tax.Count <= 0)
                    _IsTerminate = true;
            }
            return _IsTerminate;
        }
        #endregion

        #region Rooting for Invnetory Combine
        private bool BindItemComponent(string _item)
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
        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItem.Text.Trim());
                if (_isCompleteCode)
                {
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                        {
                            _isInventoryCombineAdded = false;
                            _isCompleteCode = true;
                            _IsTerminate = false;
                            return _IsTerminate;
                        }
                        else
                        {
                            _isCompleteCode = false;
                            _IsTerminate = true;
                        }
                    }
                    else
                    {
                        _isCompleteCode = false;
                        _IsTerminate = true;
                    }
                }
            }
            else
            {
                _isCompleteCode = false;
                _IsTerminate = true;
            }

            return _IsTerminate;
        }
        #endregion

        #region Rooting for Price and Promotion
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
        private void SetSSPriceDetailVariable(string _circuler, string _pblineseq, string _pbseqno, string _pbprice, string _promotioncd, string _promotiontype)
        {
            SSCirculerCode = _circuler;
            SSPriceBookItemSequance = _pblineseq;
            SSPriceBookPrice = Convert.ToDecimal(_pbprice);
            SSPriceBookSequance = _pbseqno;
            SSPromotionCode = _promotioncd;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }

        private void UncheckNormalPriceOrPromotionPrice(bool _isNormal, bool _isPromotion)
        {
            if (_isNormal)
                if (gvNormalPrice.RowCount > 0)
                {
                    foreach (DataGridViewRow _r in gvNormalPrice.Rows)
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells[0].Value;
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            _chk.Value = false;
                        }
                    }
                }

            if (_isPromotion)
                if (gvPromotionPrice.RowCount > 0)
                    foreach (DataGridViewRow row in gvPromotionPrice.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            chk.Value = false;
                        }
                    }
        }
        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
                NorPrice_Select.Visible = true;

                NorPrice_Serial.DataPropertyName = "sars_ser_no";
                NorPrice_Serial.Visible = true;
                NorPrice_Item.DataPropertyName = "Sars_itm_cd";
                NorPrice_Item.Visible = true;
                NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                NorPrice_UnitPrice.Visible = true;
                NorPrice_Circuler.DataPropertyName = "sars_circular_no";
                NorPrice_PriceType.DataPropertyName = "sars_price_type";
                NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                NorPrice_ValidTill.DataPropertyName = "sars_val_to";
                NorPrice_ValidTill.Visible = true;
                NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                NorPrice_PbLineSeq.DataPropertyName = "1";
                NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";

                PromPrice_Select.Visible = true;

                PromPrice_Serial.DataPropertyName = "sars_ser_no";
                PromPrice_Serial.Visible = true;
                PromPrice_Item.DataPropertyName = "Sars_itm_cd";
                PromPrice_Item.Visible = true;
                PromPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                PromPrice_UnitPrice.Visible = true;
                PromPrice_Circuler.DataPropertyName = "sars_circular_no";
                PromPrice_PriceType.DataPropertyName = "sars_price_type";
                PromPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                PromPrice_ValidTill.DataPropertyName = "sars_val_to";
                PromPrice_ValidTill.Visible = true;
                PromPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                PromPrice_PbLineSeq.DataPropertyName = "1";
                PromPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                PromPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                PromPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                PromPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
            }
            else
            {
                NorPrice_Select.Visible = false;

                NorPrice_Serial.Visible = false;
                NorPrice_Item.DataPropertyName = "sapd_itm_cd";
                NorPrice_Item.Visible = true;
                NorPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                NorPrice_UnitPrice.Visible = true;
                NorPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                NorPrice_Circuler.Visible = true;
                NorPrice_PriceType.DataPropertyName = "Sarpt_cd";
                NorPrice_PriceTypeDescription.DataPropertyName = "";
                NorPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                NorPrice_ValidTill.Visible = true;
                NorPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                NorPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                NorPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                NorPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                NorPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                NorPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";

                PromPrice_Select.Visible = true;

                PromPrice_Serial.Visible = false;
                PromPrice_Item.DataPropertyName = "sapd_itm_cd";
                PromPrice_Item.Visible = true;
                PromPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                PromPrice_UnitPrice.Visible = true;
                PromPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                PromPrice_Circuler.Visible = true;
                PromPrice_PriceType.DataPropertyName = "sapd_price_type"; //"Sarpt_cd";
                PromPrice_PriceTypeDescription.DataPropertyName = "";
                PromPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                PromPrice_ValidTill.Visible = true;
                PromPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                PromPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                PromPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                PromPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                PromPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                PromPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
            }
        }
        protected void BindPriceAndPromotion(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = x.Sars_itm_price * CheckSubItemTax(x.Sars_itm_cd));
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }
        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            _list.ForEach(x => x.Sapd_cre_by = Convert.ToString(x.Sapd_itm_price));
            _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }

        #region Normal Price Grid
        private void gvNormalPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if non-serialized, pick price and go to main screen
            //if serialized check the 'check box' and validate the main screen qty
            //load the available qty
            if (gvNormalPrice.ColumnCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                    if (_priceBookLevelRef.Sapl_is_serialized)
                    {
                        UncheckNormalPriceOrPromotionPrice(false, true);
                        //CheckBox _chk = (CheckBox)gvNormalPrice.Rows[_row].Cells[0].Value;
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)gvNormalPrice.Rows[_row].Cells[0];
                        if (Convert.ToBoolean(_chk.Value)) _chk.Value = false; else _chk.Value = true;
                        //if (_chk.Checked)
                        //    _chk.Checked = false;
                        //else
                        //    _chk.Checked = true;

                        //decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                        //                  where ((CheckBox)row.Cells[0].Value).Checked == true
                        //                  select row).Count();
                        decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                          where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                          select row).Count();

                        if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            //_chk.Checked = false;
                            //MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //return;
                            _chk.Value = false; this.Cursor = Cursors.Default;
                            MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        //string _unitPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value.ToString();
                        //string _bkpPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value.ToString();
                        //string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                        //string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                        //string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();

                        //if (!string.IsNullOrEmpty(_unitPrice))
                        //{
                        //    txtUnitPrice.Text = _unitPrice;

                        //    SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                        //    SSPriceBookSequance = _pbseq;
                        //    SSPriceBookItemSequance = _pblineseq;
                        //    WarrantyRemarks = _warrantyrmk;
                        //    CalculateItem();
                        //    pnlMain.Enabled = true;
                        //    pnlPriceNPromotion.Visible = false;
                        //}
                    }
            }
        }
        private void gvNormalPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvNormalPrice.ColumnCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                    if (_priceBookLevelRef.Sapl_is_serialized == false)
                    {
                        string _unitPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value.ToString();
                        string _bkpPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value.ToString();
                        string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                        string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                        string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();

                        if (!string.IsNullOrEmpty(_unitPrice))
                        {
                            txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                            SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                            SSPriceBookSequance = _pbseq;
                            SSPriceBookItemSequance = _pblineseq;
                            WarrantyRemarks = _warrantyrmk;

                            CalculateItem();
                            pnlMain.Enabled = true;
                            pnlPriceNPromotion.Visible = false;
                        }
                    }
            }
        }
        #endregion

        #region Promotion Price Grid
        private void gvPromotionPrice_CellDoubleClick(Int32 _row, bool _isValidate)
        {
            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;

                if (Convert.ToBoolean(_chk.Value))
                {
                    _isSelected = true;
                }

                UncheckNormalPriceOrPromotionPrice(true, false);
                string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);

                if (_isValidate)
                {

                    if (_isSelected)
                    {
                        _chk.Value = false;
                    }
                    else
                    {
                        _chk.Value = true;
                    }


                    decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                      where ((CheckBox)row.Cells[0].Value).Checked == true
                                      select row).Count();
                    if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        _chk.Value = false;
                        MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            else
            {
                DataGridViewCheckBoxCell chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;

                if (Convert.ToBoolean(chk.Value))
                {
                    _isSelected = true;
                }

                UncheckNormalPriceOrPromotionPrice(false, true);

                BindingSource _source = new BindingSource();
                _source.DataSource = new List<PriceCombinedItemRef>();
                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();

                if (_isSelected)
                {
                    chk.Value = false;
                }
                else
                {
                    string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                    string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                    string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                    string _pblineseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_PbLineSeq"].Value.ToString();
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
                    chk.Value = true;
                }
            }
        }
        private void gvPromotionPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if non-serialized, show the promotion items
            //if serialized, show the promotion and check the 'check box' 
            //load the available qty
            if (gvPromotionPrice.RowCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                    gvPromotionPrice_CellDoubleClick(_row, true);
            }
        }
        private void gvPromotionPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //non-serialized or serialized, show the promotion items
            //load the available qty
            if (gvPromotionPrice.RowCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                    gvPromotionPrice_CellDoubleClick(_row, false);
            }
        }
        private void gvPromotionPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if serialized, check the check box 
            //load the available qty
            //else no action
            if (gvPromotionPrice.RowCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                    if (_priceBookLevelRef.Sapl_is_serialized)
                        gvPromotionPrice_CellDoubleClick(_row, false);
            }
        }
        #endregion

        #region Promotion Item Grid
        private void BindPriceCombineItem(Int32 _pbseq, Int32 _pblineseq, Int32 _priceType, string _mainItem, string _mainSerial)
        {
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            PriceTypeRef _list = TakePromotion(_priceType);
            if (_list.Sarpt_is_com)
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _mainItem, _mainSerial);
                    PromItm_Serial.Visible = true;
                }
                else
                {
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _mainItem, string.Empty);
                    PromItm_Serial.Visible = false;
                }
            _tempPriceCombinItem.ForEach(x => x.Mi_cre_by = Convert.ToString(x.Mi_std_price));
            _tempPriceCombinItem.ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(txtQty.Text.Trim()));
            _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
            gvPromotionItem.DataSource = _tempPriceCombinItem;
        }
        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty, bool _isPromotion)
        {
            List<ReptPickSerials> _lst = null;
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            //Load serials
            //EVEN THIS CALLED NON-SERIALIZED, CAN USE FOR SERIALIZED ITEM
            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 == 1)
            {
                if (IsPriceLevelAllowDoAnyStatus)
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
                else
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);

                foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                    _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

                _lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);
                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                if (_isPromotion)
                    gvPromotionSerial.DataSource = _lst;
                else
                    gvPopComItemSerial.DataSource = _lst;

                _promotionSerial = _lst;
            }
            else
            {
                MessageBox.Show("No need to pick non serialized item", "Non Serialized Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }

        private void DisplayAvailableQty(string _item, Label _withStatus, Label _withoutStatus, string _status)
        {
            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _woStatus = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    var _wStatus = _inventoryLocation.Where(x => x.Inl_itm_stus == _status).Select(x => x.Inl_free_qty).Sum();
                    _withStatus.Text = FormatToQty(Convert.ToString(_wStatus));
                    _withoutStatus.Text = FormatToQty(Convert.ToString(_woStatus));
                }
                else { _withStatus.Text = FormatToQty("0"); _withoutStatus.Text = FormatToQty("0"); }
            else { _withoutStatus.Text = FormatToQty("0"); _withStatus.Text = FormatToQty("0"); }
        }

        private void LoadGiftVoucherBalance(string _item, Label _withStatus, Label _withoutStatus, out List<ReptPickSerials> GiftVoucher)
        {
            List<ReptPickSerials> _gifVoucher = CHNLSVC.Inventory.GetAvailableGiftVoucher(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item);
            if (_gifVoucher == null || _gifVoucher.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("There is no gift vouchers available.", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                _withStatus.Text = string.Empty;
                _withoutStatus.Text = string.Empty;
                GiftVoucher = _gifVoucher;
                return;
            }
            int _count = _gifVoucher.AsEnumerable().Count();
            _withStatus.Text = FormatToQty(Convert.ToString(_count));
            _withoutStatus.Text = FormatToQty(Convert.ToString(_count));
            var _list = _gifVoucher.AsEnumerable().Where(x => x.Tus_itm_cd == _item).ToList();
            GiftVoucher = _list;
        }

        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty, bool _isPromotion, int _isStatusCol)
        {
            List<ReptPickSerials> _lst = null;
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 == 1)
            {
                if (IsPriceLevelAllowDoAnyStatus)
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
                else
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);

                if (IsPriceLevelAllowDoAnyStatus == false && (_lst == null || _lst.Count <= 0))
                {
                    if (cmbStatus.Items.Contains("CONS"))
                    {
                        _status = "CONS";
                        _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);
                    }
                }
                foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                    _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

                _lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);
                #region Age Price level - serial pick
                bool _isAgePriceLevel = false;
                int _noOfDays = 0;
                CheckNValidateAgeItem(_item.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), _status, out _isAgePriceLevel, out _noOfDays);
                List<ReptPickSerials> _newlist = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgePriceLevel, _noOfDays, _lst);
                #endregion
                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                BindingSource _source = new BindingSource();
                var _list = new BindingList<ReptPickSerials>(_newlist);
                if (_isPromotion)
                {
                    _source.DataSource = _lst;
                    gvPromotionSerial.DataSource = _list;
                }
                else
                {
                    _source.DataSource = _lst;
                    gvPopComItemSerial.DataSource = _list;
                }
                _promotionSerial = _lst;
            }
            else
            {
                if (_isStatusCol == 7) return;
                this.Cursor = Cursors.Default;
                { MessageBox.Show("No need to pick non serialized item", "Non Serialized Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
        }
        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }
        private void CheckNValidateAgeItem(string _itemc, string _itemcategory, string _bookc, string _levelc, string _status, out bool IsAgePriceLevel, out int AgeDays)
        {
            bool _isAgePriceLevel = false;
            int _ageingDays = -1;
            MasterItem _item = null;
            if (string.IsNullOrEmpty(_itemcategory))
            { _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemc); if (_item != null) _itemcategory = _item.Mi_cate_1; }
            List<PriceBookLevelRef> _level = _priceBookLevelRefList;
            if (_level != null)
                if (_level.Count > 0)
                {
                    var _lvl = _level.Where(x => x.Sapl_isage && x.Sapl_itm_stuts == _status).ToList();
                    if (_lvl != null) if (_lvl.Count() > 0)
                            _isAgePriceLevel = true;
                }
            if (_isAgePriceLevel)
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemcategory);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                    else _ageingDays = 0;
                }
            }

            IsAgePriceLevel = _isAgePriceLevel;
            AgeDays = _ageingDays;
        }
        private void gvPromotionItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionItem.RowCount > 0)
                {
                    int _col = e.ColumnIndex;
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                    {
                        string _originalItem = gvPromotionItem.Rows[_row].Cells["PromItm_Item"].Value.ToString();
                        string _item = gvPromotionItem.Rows[_row].Cells["PromItm_Item"].Value.ToString();
                        string _similerItem = "";//Convert.ToString(gvPromotionItem.Rows[_row].Cells["PromItm_SimilerItem"].Value);
                        string _status = "";//Convert.ToString(gvPromotionItem.Rows[_row].Cells["PromItm_Status"].Value); //cmbStatus.Text.Trim();
                        string _qty = gvPromotionItem.Rows[_row].Cells["PromItm_Qty"].Value.ToString();
                        string _serial = gvPromotionItem.Rows[_row].Cells["PromItm_Serial"].Value.ToString();
                        bool _haveSerial = Convert.ToBoolean(gvPromotionItem.Rows[_row].Cells["PromItm_increse"].Value.ToString());
                        string _PromotionCD = Convert.ToString(gvPromotionPrice.SelectedRows[0].Cells["PromPrice_PromotionCD"].Value);
                        List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

                        if (!string.IsNullOrEmpty(_similerItem))
                            _item = _similerItem;
                        bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item).Mi_itm_tp);

                        if (!_isGiftVoucher) DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
                        else LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);
                        if (gvPromotionItem.Columns[e.ColumnIndex].Name != "PromItm_SelectSimilerItem")
                        {
                            if (_isGiftVoucher)
                            {
                                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                _promotionSerial = new List<ReptPickSerials>();
                                _promotionSerialTemp = new List<ReptPickSerials>();
                                if (_giftVoucher != null)
                                    if (_giftVoucher.Count > 0)
                                        _lst.AddRange(_giftVoucher);
                                _promotionSerial = _lst;
                                gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                                gvPopComItemSerial.DataSource = _lst;
                                txtPriNProSerialSearch.Text = ".";
                                txtPriNProSerialSearch.Text = string.Empty;

                            }
                            else if (_priceBookLevelRef.Sapl_is_serialized)
                            {
                                if (_haveSerial == false && chkDeliverLater.Checked == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater.Checked == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && chkDeliverLater.Checked == false)
                                {
                                    List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serial);
                                    if (_ref != null)
                                        if (_ref.Count > 0)
                                        {
                                            var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                            if (_available == null || _available.Count <= 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                        }
                                }

                            }
                            else if (chkDeliverLater.Checked == false)
                            {
                                LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                            }
                            else
                            {
                                var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                                gvPromotionSerial.DataSource = _list;
                            }
                        }

                        #region Similar Item Call

                        if (!_isGiftVoucher)
                            if (gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater.Checked == false)
                            {
                                DataTable _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);
                                if (_dtTable != null)
                                    if (_dtTable.Rows.Count > 0)
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Stock balance is available for the promotion item. No need to pick similar item here!.", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                TextBox _box = new TextBox();
                                CommonSearch.SearchSimilarItems _similarItems = new CommonSearch.SearchSimilarItems();
                                _similarItems.DocumentType = "S";
                                _similarItems.ItemCode = _item;
                                _similarItems.FunctionDate = txtDate.Value.Date;
                                _similarItems.DocumentNo = string.Empty;
                                _similarItems.PromotionCode = _PromotionCD;
                                _similarItems.obj_TragetTextBox = _box;
                                _similarItems.ShowDialog();
                                if (!string.IsNullOrEmpty(_box.Text))
                                {
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Similer_item = _box.Text);
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_increse = false);
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_sub_ser = string.Empty);
                                    BindingSource _source = new BindingSource();
                                    _source.DataSource = _tempPriceCombinItem;
                                    gvPromotionItem.DataSource = _source;
                                    _box.Clear();

                                }
                            }
                            else if ((gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater.Checked == true))
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("You can not pick similar item unless you have deliver now!", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;

                            }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Promotion Item Serial Grid
        private void gvPromotionSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPromotionSerial.ColumnCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                {
                    DataGridViewCheckBoxCell _cell = gvPromotionSerial.Rows[_row].Cells["ProSer_Select"] as DataGridViewCheckBoxCell;
                    string _id = gvPromotionSerial.Rows[_row].Cells["ProSer_SerialID"].Value.ToString();
                    if (Convert.ToBoolean(_cell.Value) == true)
                    {

                        _cell.Value = false;
                        PriceCombinItemSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(_id));
                    }
                    else
                    {
                        _cell.Value = true;
                        var _n = _promotionSerial.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                        _promotionSerialTemp.AddRange(_n);

                    }
                }
            }

        }
        private void gvPromotionSerial_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (gvPromotionSerial.ColumnCount > 0)
            {
                Int32 _rowindex = e.RowIndex;
                if (_rowindex != -1)
                {
                    for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
                    {
                        string _item = gvPromotionSerial.Rows[index].Cells["ProSer_Item"].Value.ToString();
                        string _serialID = gvPromotionSerial.Rows[index].Cells["ProSer_SerialID"].Value.ToString();
                        DataGridViewCheckBoxCell _check = gvPromotionSerial.Rows[index].Cells["ProSer_Select"] as DataGridViewCheckBoxCell;

                        string _selectedid = string.Empty;
                        if (PriceCombinItemSerialList != null)
                            if (PriceCombinItemSerialList != null)
                                if (PriceCombinItemSerialList.Count > 0)
                                {
                                    var _id = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_id == Convert.ToInt32(_serialID)).Select(y => y.Tus_ser_id);
                                    if (_id != null)
                                        if (_id.Count() > 0)
                                        {
                                            foreach (var f in _id)
                                                if (!string.IsNullOrEmpty(Convert.ToString(f)))
                                                    _check.Value = true;
                                        }
                                }

                    }
                }
            }
        }
        #endregion

        private void btnPriClose_Click(object sender, EventArgs e)
        {
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            pnlMain.Enabled = true;
            pnlPriceNPromotion.Visible = false;

        }
        private void btnPriNProCancel_Click(object sender, EventArgs e)
        {
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            pnlPriceNPromotion.Visible = false;
            pnlMain.Enabled = true;

        }
        private void btnPriNProConfirm_Click(object sender, EventArgs e)
        {
            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                //check for normal prices
                //check for promotion prices
                //Both grid cant have ->msg
                //get the what ever price type and calculate the count
                //if qty <> count ->msg
                //if promotion
                // check the selected promotion sub items serial pick/not pick (if delivery now!)
                // fill the objects for promotion item + promotion sub item + promotion sub item serial (if available)
                //focus main screen with out price
            }
            else
            {
                //check for promotion item selected
                //collect the promotion item and check for the serial picked
                //if not tally->msg
                //focus main screen with price
                bool _isSelect = false;
                DataGridViewRow _pickedRow = new DataGridViewRow();

                foreach (DataGridViewRow _row in gvPromotionPrice.Rows)
                {
                    if (Convert.ToBoolean(_row.Cells["PromPrice_Select"].Value) == true)
                    {
                        _isSelect = true;
                        _pickedRow = _row;
                        break;
                    }
                }

                if (!_isSelect)
                {
                    MessageBox.Show("You have to select a promotion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //if (_tempPriceCombinItem == null)
                //{
                //    MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
                //if (_tempPriceCombinItem.Count <= 0)
                //{
                //    MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                if (_isSelect)
                {
                    string _mainItem = _pickedRow.Cells["PromPrice_Item"].Value.ToString();
                    string _pbSeq = _pickedRow.Cells["PromPrice_Pb_Seq"].Value.ToString();
                    string _pbLineSeq = _pickedRow.Cells["PromPrice_PbLineSeq"].Value.ToString();
                    string _pbWarranty = _pickedRow.Cells["PromPrice_WarrantyRmk"].Value.ToString();
                    string _unitprice = _pickedRow.Cells["PromPrice_UnitPrice"].Value.ToString();
                    string _promotioncode = _pickedRow.Cells["PromPrice_PromotionCD"].Value.ToString();
                    string _circulerncode = _pickedRow.Cells["PromPrice_Circuler"].Value.ToString();
                    string _promotiontype = _pickedRow.Cells["PromPrice_PriceType"].Value.ToString();
                    string _pbPrice = _pickedRow.Cells["PromPrice_BkpUPrice"].Value.ToString();
                    bool _isSingleItemSerialized = false;

                    if (chkDeliverLater.Checked == false)
                        foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                        {
                            string _item = _ref.Sapc_itm_cd;
                            string _status = cmbStatus.Text.Trim();
                            string _qty = Convert.ToString(_ref.Sapc_qty);
                            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                            if (_itm.Mi_is_ser1 == 1)
                            {
                                _isSingleItemSerialized = true;
                                decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                if (_serialcount != Convert.ToDecimal(_qty))
                                {
                                    MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else if (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1)
                            {
                                decimal _pickQty = 0;
                                if (IsPriceLevelAllowDoAnyStatus)
                                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum();
                                else
                                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, cmbStatus.Text.Trim());

                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_pickQty > _invBal)
                                        {
                                            this.Cursor = Cursors.Default;
                                            MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }


                    if (chkDeliverLater.Checked == false && _isSingleItemSerialized)
                        if (PriceCombinItemSerialList == null)
                        {
                            MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    if (chkDeliverLater.Checked == false && _isSingleItemSerialized)
                        if (PriceCombinItemSerialList.Count <= 0)
                        {
                            MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                    SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);

                    _MainPriceCombinItem = _tempPriceCombinItem;
                    txtUnitPrice.Text = FormatToCurrency(_unitprice);
                    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), false)));
                    CalculateItem();
                    pnlPriceNPromotion.Visible = false;
                    pnlMain.Enabled = true;
                    btnAddItem.Focus();
                }
            }
        }

        private void btnPriNProSerConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtPriNProSerialSearch.Text = string.Empty;
                decimal _serialcount = 0;
                decimal _promotionItemQty = Convert.ToDecimal(gvPromotionItem.SelectedRows[0].Cells["PromItm_Qty"].Value);
                string _promotionItem = gvPromotionItem.SelectedRows[0].Cells["PromItm_Item"].Value.ToString();
                string _promotionOriginalItem = gvPromotionItem.SelectedRows[0].Cells["PromItm_Item"].Value.ToString();
                string _SimilerItem = Convert.ToString(gvPromotionItem.SelectedRows[0].Cells["PromItm_SimilerItem"].Value);
                if (!string.IsNullOrEmpty(_SimilerItem)) _promotionItem = _SimilerItem;
                foreach (DataGridViewRow _row in gvPromotionSerial.Rows)
                {
                    if (Convert.ToBoolean(_row.Cells["ProSer_Select"].Value) == true)
                        _serialcount += 1;
                }
                if (_serialcount != _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }
                if (_serialcount > _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }
                if (PriceCombinItemSerialList != null)
                    if (PriceCombinItemSerialList.Count > 0)
                    {
                        decimal _count = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _promotionItem).Count();
                        if (_count >= _promotionItemQty)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("You already pick serials for the item", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                    }
                foreach (DataGridViewRow _r in gvPromotionSerial.Rows)
                {
                    if (Convert.ToBoolean(_r.Cells["ProSer_Select"].Value) == true)
                    {
                        string _item = Convert.ToString(_r.Cells["ProSer_Item"].Value);
                        string _serial = Convert.ToString(_r.Cells["ProSer_Serial1"].Value);
                        string _serial2 = Convert.ToString(_r.Cells["ProSer_Serial2"].Value);
                        MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        string _prefix = Convert.ToString(_r.Cells["ProSer_Serial3"].Value);
                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        ReptPickSerials _serLst = null;
                        if (!_isGiftVoucher) _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serial); else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, Convert.ToInt32(_serial2), Convert.ToInt32(_serial), _prefix);
                        _serLst.Tus_session_id = _promotionOriginalItem;
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
                                                this.Cursor = Cursors.Default;
                                                { MessageBox.Show(_serLst.Tus_ser_1 + "Serial duplicating!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                gvPromotionSerial.DataSource = _lst;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtPriNProSerialSearch_TextChanged(object sender, EventArgs e)
        {
            if (gvPromotionSerial.ColumnCount > 0)
            {
                if (!string.IsNullOrEmpty(txtPriNProSerialSearch.Text.Trim()))
                {
                    var query = _promotionSerial.Where(x => x.Tus_ser_1.Contains(txtPriNProSerialSearch.Text.Trim())).ToList();
                    if (query != null)
                        if (query.Count() > 0)
                            gvPromotionSerial.DataSource = query;
                        else
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    else
                        gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                }
                else
                {
                    gvPromotionSerial.DataSource = _promotionSerial;
                }

                foreach (DataGridViewRow _r in gvPromotionSerial.Rows)
                {
                    string _id = _r.Cells["ProSer_SerialID"].Value.ToString();
                    DataGridViewCheckBoxCell _chk = _r.Cells["ProSer_Select"] as DataGridViewCheckBoxCell;
                    if (_promotionSerialTemp != null)
                        if (_promotionSerialTemp.Count > 0)
                        {
                            var _exist = _promotionSerialTemp.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                            if (_exist != null)
                                if (_exist.Count > 0)
                                    _chk.Value = true;
                        }
                }
            }

        }

        #region Panel Movement
        Point muPricePoint = new Point();
        private void pnlPriceNPromotion_MouseDown(object sender, MouseEventArgs e)
        {
            muPricePoint.X = e.X;
            muPricePoint.Y = e.Y;
        }

        private void pnlPriceNPromotion_MouseUp(object sender, MouseEventArgs e)
        {
            pnlPriceNPromotion.Location = new Point(e.X - muPricePoint.X + pnlPriceNPromotion.Location.X, e.Y - muPricePoint.Y + pnlPriceNPromotion.Location.Y);
        }
        #endregion
        #endregion

        #region Rooting for Consumable Item
        protected void BindConsumableItem(List<InventoryBatchRefN> _consumerpricelist)
        {
            _consumerpricelist.ForEach(x => x.Inb_unit_cost = x.Inb_unit_price * CheckSubItemTax(x.Inb_itm_cd));
            gvPopConsumPricePick.DataSource = _consumerpricelist;
        }
        private bool ConsumerItemProduct()
        {
            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            if (_isMRP && chkDeliverLater.Checked == false)
            {
                //GetConsumerProductPriceList
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef.Sapl_chk_st_tp)

                    _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());
                else
                    _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);

                if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        pnlMain.Enabled = false;
                        pnlConsumerPrice.Visible = true;
                        BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            MessageBox.Show("invoice qty is " + txtQty.Text + " and inventory available qty having only " + _batchRef[0].Inb_free_qty.ToString(), "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _isAvailable = true;
                            return _isAvailable;
                        }
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_batchRef[0].Inb_unit_price * CheckSubItemTax(_batchRef[0].Inb_itm_cd))));
                        txtUnitPrice.Focus();
                        _isAvailable = false;
                    }
                _isEditPrice = false;
                _isEditDiscount = false;

                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("1");
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(val));
                CalculateItem();
                _isAvailable = true;
            }

            return _isAvailable;
        }
        #region Panel Movement
        Point ConsumItemPoint = new Point();
        private void pnlConsumerPrice_MouseDown(object sender, MouseEventArgs e)
        {
            ConsumItemPoint.X = e.X;
            ConsumItemPoint.Y = e.Y;

        }
        private void pnlConsumerPrice_MouseUp(object sender, MouseEventArgs e)
        {
            pnlConsumerPrice.Location = new Point(e.X - ConsumItemPoint.X + pnlConsumerPrice.Location.X, e.Y - ConsumItemPoint.Y + pnlConsumerPrice.Location.Y);
        }
        #endregion
        private void btnPnlConsumeClose_Click(object sender, EventArgs e)
        {
            pnlConsumerPrice.Visible = false;
            pnlMain.Enabled = true;
        }
        private void gvPopConsumPricePick_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPopConsumPricePick.Rows.Count > 0)
            {
                string _freeQty = gvPopConsumPricePick.SelectedRows[0].Cells["inb_free_qty"].Value.ToString();
                string _unitPrice = gvPopConsumPricePick.SelectedRows[0].Cells["Inb_unit_price"].Value.ToString();
                if (!string.IsNullOrEmpty(_freeQty))
                    if (Convert.ToDecimal(_freeQty) < Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        MessageBox.Show("Selected price does not meet the quantity requirement.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                pnlMain.Enabled = true;
                pnlConsumerPrice.Visible = false;
                txtUnitPrice.Text = _unitPrice;
            }
        }
        #endregion

        #region Rooting for Check Inventory Balance and Display
        private void DisplayAvailableQty(string _item, Label _withStatus, Label _withoutStatus)
        {
            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _woStatus = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    var _wStatus = _inventoryLocation.Where(x => x.Inl_itm_stus == cmbStatus.Text.Trim()).Select(x => x.Inl_free_qty).Sum();
                    _withStatus.Text = FormatToQty(Convert.ToString(_wStatus));
                    _withoutStatus.Text = FormatToQty(Convert.ToString(_woStatus));
                }
                else { _withStatus.Text = FormatToQty("0"); _withoutStatus.Text = FormatToQty("0"); }
            else { _withoutStatus.Text = FormatToQty("0"); _withStatus.Text = FormatToQty("0"); }
        }
        #endregion

        #region Rooting for Check Qty
        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
            {
                //User Can edit the price for any amount and having inventory status
                //No price book price available and no restriction for price amendment
                SetDecimalTextBoxForZero(false, false);
                _isAvailable = true;
                return _isAvailable;
            }
            return _isAvailable;
        }
        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;

            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new MethodInvoker(() => CheckQtyPriliminaryRequirements()));
            //    return true;
            //}

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                //MessageBox.Show("Please select the item code", "Invalid item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true; return _IsTerminate;
            }

            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select the valid qty", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                txtQty.Text = FormatToQty("1");

            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }

            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                MessageBox.Show("Please select the invoice type", "Invalid Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbInvType.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtCustomer.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                MessageBox.Show("Price book not select.", "Invalid Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                MessageBox.Show("Please select the price level", "Invalid Level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                MessageBox.Show("Please select the item status", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;

        }
        protected void BindSerializedPrice(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = CheckSubItemTax(x.Sars_itm_cd) * x.Sars_itm_price);
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }
        private void gvPromotionPrice_CellDoubleClick(Int32 _row, bool _isValidate, bool _IsSerializedPriceLevel)
        {
            if (_IsSerializedPriceLevel)
            {
                DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;
                if (Convert.ToBoolean(_chk.Value)) _isSelected = true;
                UncheckNormalPriceOrPromotionPrice(true, false);
                string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);
                if (_isValidate)
                {
                    if (_isSelected) _chk.Value = false; else _chk.Value = true;
                    decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                      where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                      select row).Count();
                    if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        _chk.Value = false; this.Cursor = Cursors.Default;
                        MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (_isSelected) _chk.Value = false; else _chk.Value = true;
            }
            else
            {
                DataGridViewCheckBoxCell chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;
                if (Convert.ToBoolean(chk.Value)) _isSelected = true;
                UncheckNormalPriceOrPromotionPrice(false, true);
                BindingSource _source = new BindingSource();
                _source.DataSource = new List<PriceCombinedItemRef>();
                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                if (_isSelected) chk.Value = false;
                else
                {
                    string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                    string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                    string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                    string _pblineseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_PbLineSeq"].Value.ToString();
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
                    chk.Value = true;
                }
            }
        }

        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
            bool _isAvailable = false;
            if (_isSerialized)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are selected a serialized price level, hence you have not select the serial no. Please select the serial no.", "Serialized Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAvailable = true;
                    return _isAvailable;
                }
                List<PriceSerialRef> _list = null;
                if (_isNewPromotionProcess == false)
                    _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerialNo.Text.Trim());
                else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0)
                    _list = _PriceSerialRefNormal;
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0)
                    _list = _PriceSerialRefPromo;
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list != null)
                {
                    if (_list.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtQty.Text = FormatToQty("0");
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                    _list = _oneSerial;
                    if (_list.Count < Convert.ToDecimal(txtQty.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected qty is exceeds available serials at the price definition!", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtQty.Text = FormatToQty("0");
                        // IsNoPriceDefinition = true;
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    if (_list.Count == 1)
                    {
                        string _book = _list[0].Sars_pbook;
                        string _level = _list[0].Sars_price_lvl;
                        cmbBook.Text = _book;
                        cmbLevel.Text = _level;
                        if (!_isSerialized)
                            cmbLevel_Leave(null, null);

                        int _priceType = 0;
                        _priceType = _list[0].Sars_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), false);//TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false);

                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _list[0].Sars_warr_remarks;
                        SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

                        Int32 _pbSq = _list[0].Sars_pb_seq;
                        string _mItem = _list[0].Sars_itm_cd;
                        _isAvailable = true;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        SetColumnForPriceDetailNPromotion(true);
                        BindSerializedPrice(_list);

                        if (gvPromotionPrice.RowCount > 0)
                        {
                            gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                            pnlPriceNPromotion.Visible = true;
                            pnlMain.Enabled = false;
                            return _isAvailable;
                        }
                        else
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //}
                        //else
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        return _isAvailable;
                    }
                    if (_list.Count > 1)
                    {
                        SetColumnForPriceDetailNPromotion(true);
                        BindPriceAndPromotion(_list);
                        DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
                        pnlMain.Enabled = false;
                        pnlPriceNPromotion.Visible = true;
                        _isAvailable = true;
                        return _isAvailable;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Text = FormatToQty("0");
                    _isAvailable = true;
                    txtQty.Focus();
                    return _isAvailable;
                }
            }
            return _isAvailable;
            //bool _isAvailable = false;
            //if (_priceBookLevelRef.Sapl_is_serialized)
            //{
            //    //List<PriceSerialRef> _list = CHNLSVC.Sales.GetAllPriceSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //    List<PriceSerialRef> _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerialNo.Text.Trim());
            //    _tempPriceSerial = new List<PriceSerialRef>();
            //    _tempPriceSerial = _list;
            //    if (_list != null)
            //    {
            //        if (_list.Count <= 0)
            //        {
            //            MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            txtQty.Text = FormatToQty("0");
            //            _isAvailable = true;
            //            txtQty.Focus();
            //            return _isAvailable;
            //        }

            //        if (_list.Count < Convert.ToDecimal(txtQty.Text))
            //        {
            //            MessageBox.Show("Selected qty is exceeds available serials at the price definition!", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            txtQty.Text = FormatToQty("0");
            //            // IsNoPriceDefinition = true;
            //            _isAvailable = true;
            //            txtQty.Focus();
            //            return _isAvailable;
            //        }

            //        if (_list.Count > 0)
            //        {
            //            BindPriceAndPromotion(_list);
            //            DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty);
            //            pnlMain.Enabled = false;
            //            pnlPriceNPromotion.Visible = true;
            //            // IsNoPriceDefinition = false;
            //            _isAvailable = true;
            //            return _isAvailable;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtQty.Text = FormatToQty("0");
            //        _isAvailable = true;
            //        txtQty.Focus();
            //        return _isAvailable;
            //    }
            //}
            //return _isAvailable;
        }


        protected bool CheckQty()
        {
            if (pnlMain.Enabled == false) return true;
            WarrantyRemarks = string.Empty;
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;


            #region Check Priliminary Requirement
            if (CheckQtyPriliminaryRequirements()) return true;
            #endregion
            #region  Inventory Combine Item
            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    MessageBox.Show("This compete code does not having a collection. Please contact inventory", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            #endregion
            #region Check for Tax Setup
            if (CheckTaxAvailability())
            {
                MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }
            #endregion
            CheckItemTax(txtItem.Text.Trim());
            #region Profit Center Allows Without Price
            if (CheckProfitCenterAllowForWithoutPrice())
            {
                _IsTerminate = true;
                return _IsTerminate;
            }
            #endregion

            #region Consumer Item
            if (ConsumerItemProduct())
            {
                _IsTerminate = true;
                return _IsTerminate;
            }
            #endregion
            #region Check & Load Serialized Prices and Its Promotion
            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
            {
                if (CheckSerializedPriceLevelAndLoadSerials(true))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            }
            #endregion


            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));

            if (_priceDetailRef.Count <= 0)
            {
                //Inventory Combine Item -------------------------------
                if (!_isCompleteCode)
                {
                    MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //IsNoPriceDefinition = true;
                    SetDecimalTextBoxForZero(true, false);
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
                    var _p = _new.Where(x => x.Sapd_price_type == 0).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                            _priceDetailRef.Add(_p[0]);
                }
                //Inventory Combine Item -------------------------------

                if (_priceDetailRef.Count > 1)
                {
                    //Find More than one price for the selected item
                    //Load prices for the grid and popup for user confirmation

                    //IsNoPriceDefinition = false;
                    SetColumnForPriceDetailNPromotion(false);
                    BindNonSerializedPrice(_priceDetailRef);
                    pnlPriceNPromotion.Visible = true;
                    _IsTerminate = true;
                    pnlMain.Enabled = false;
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
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), false);

                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));

                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            SetColumnForPriceDetailNPromotion(false);
                            BindNonSerializedPrice(_priceDetailRef);
                            //BindPriceCombineItem(_pbSq, _pbiSq, _priceType, _mItem, string.Empty);
                            gvPromotionPrice_CellDoubleClick(0, false);
                            //IsNoPriceDefinition = false;
                            pnlPriceNPromotion.Visible = true;
                            pnlMain.Enabled = false;
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                        else
                        {
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
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
        private void txtQty_Leave(object sender, EventArgs e)
        {
            CheckQty();
        }
        #endregion

        #region Rooting for Unit Price
        protected void CheckUnitPrice(object sender, EventArgs e)
        {
            if (txtUnitPrice.ReadOnly) return;
            if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
            {
                MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;


            if (!_isCompleteCode)
            {
                if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                {
                    decimal _pb_price;
                    if (SSPriceBookPrice == 0 && !_MasterProfitCenter.Mpc_without_price)
                    {
                        MessageBox.Show("Price not define. Please check the system updated price.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUnitPrice.Text = FormatToCurrency("0");
                        return;
                    }
                    _pb_price = SSPriceBookPrice;
                    decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                    if (_MasterProfitCenter.Mpc_edit_price)
                    {

                        if (_pb_price > _txtUprice)
                        {
                            decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                            if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                            {
                                MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        protected bool CheckDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);

                if (_disRate > 0)
                {
                    ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    if (ManagerDiscount.Count > 0)
                    {
                        var vals = ManagerDiscount.Select(x => x.Key).ToList();
                        var rates = ManagerDiscount.Select(x => x.Value).ToList();

                        if (rates[0] < _disRate)
                        {
                            MessageBox.Show("You can not discount price more than " + rates[0] + "%.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisRate.Text = FormatToCurrency("0");
                            // txtDiscount.Focus();
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                            _isEditDiscount = true;
                    }
                    else
                    {
                        MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }
                }
                else
                    _isEditDiscount = false;


            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }


            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }
        private Object thisLock = new Object();
        protected void CheckDiscountRate(object sender, EventArgs e)
        {
            if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
            {
                MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CheckDiscountRate();
        }
        private bool CheckDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
                decimal _qty = Convert.ToDecimal(txtQty.Text);
                decimal _totAmt = _uRate * _qty;
                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                if (_disAmt > 0)
                {
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (vals < _disAmt && rates == 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                            CalculateItem();
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                            CalculateItem();
                            CheckNewDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        if (GeneralDiscount != null)
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (vals < _disAmt && rates == 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtDisRate.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckNewDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                    }
                }
                else
                    _isEditDiscount = false;

            }
            else if (_isEditPrice)
            {
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
            }

            if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);

                if (_disRate > 0)
                {
                    if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (rates < _disRate)
                        {
                            CalculateItem();
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDisRate.Text = FormatToCurrency("0");
                            CalculateItem();
                            // txtDiscount.Focus();
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                            _isEditDiscount = true;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }
                }
                else
                    _isEditDiscount = false;


            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }


            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = val.ToString();//FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
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
            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (string.IsNullOrEmpty(txtTaxAmt.Text)) txtTaxAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtTaxAmt.Text);
            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
        }

        protected void CheckTotalAmt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;
            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

            if (string.IsNullOrEmpty(txtTaxAmt.Text)) txtTaxAmt.Text = FormatToCurrency("0");
            CalculateItem();
            decimal val = Convert.ToDecimal(txtTaxAmt.Text);
            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(val));
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
            _tempItem.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Sad_do_qty = (IsGiftVoucher(_item.Mi_itm_tp)) ? Convert.ToDecimal(txtQty.Text) : 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtItem.Text;
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = 0;//Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = cmbStatus.Text;
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            //_tempItem.Sad_itm_tp = "";
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = cmbLevel.Text;
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = cmbBook.Text;
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Sad_warr_period = 0;
            _tempItem.Sad_warr_remarks = "";
            _tempItem.Mi_longdesc = _item.Mi_longdesc;
            _tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Sad_warr_period = WarrantyPeriod;
            _tempItem.Sad_warr_remarks = WarrantyRemarks;
            if (!string.IsNullOrEmpty(txtDisRate.Text.Trim()) && IsNumeric(txtDisRate.Text.Trim())) if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 0 && GeneralDiscount != null) { _tempItem.Sad_dis_type = "M"; _tempItem.Sad_dis_seq = GeneralDiscount.Sgdd_seq; _tempItem.Sad_dis_line = 0; }

            return _tempItem;
        }
        private void ClearAfterAddItem()
        {
            txtItem.Text = "";
            cmbStatus.Text = DefaultItemStatus;
            txtQty.Text = FormatToQty("0");
            LoadItemDetail(string.Empty);
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            txtItem.ReadOnly = false;
        }
        private bool _isCombineAdding = false;
        private int _combineCounter = 0;
        private string _paymodedef = string.Empty;
        private bool _isCheckedPriceCombine = false;
        private bool _isFirstPriceComItem = false;

        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;// CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            if (_lst[0].Sapl_set_warr == true) { WarrantyPeriod = _lst[0].Sapl_warr_period; }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                                else { _isNoWarranty = true; }
                            }
                        }
                }
            return _isNoWarranty;
        }

        //private bool CheckItemWarrantyNew(string _item, string _status, Int32 _pbSeq, Int32 _itmSeq, string _pb, string _pbLvl, Boolean _isPbWara, decimal _unitPrice, Int32 _pbWarrPd)
        //{
        //    bool _isNoWarranty = false;
        //    //List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
        //    //if (_lvl != null)
        //    //    if (_lvl.Count > 0)
        //    //    {
        //    //        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
        //    //        if (_lst != null)
        //    //if (_lst.Count > 0)
        //    //{
        //    DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item.Trim(), _status.Trim(), Convert.ToDateTime(txtDate.Text).Date);

        //    if (_isPbWara == true && _unitPrice > 0)
        //    {
        //        WarrantyPeriod = _pbWarrPd;
        //        PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, _itmSeq, _pbSeq);
        //        if (_lsts != null)
        //        {
        //            WarrantyRemarks = _lsts.Sapd_warr_remarks;
        //        }

        //    }
        //    else if (_temWarr != null && _temWarr.Rows.Count > 0)
        //    {
        //        WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
        //        WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
        //    }
        //    else if (txtDate.Value.Date != _serverDt)
        //    {
        //        MasterItemWarrantyPeriod _period = CHNLSVC.Sales.GetItemWarrEffDt(_item, _status, 1, txtDate.Value.Date);
        //        if (_period != null)
        //        {
        //            WarrantyPeriod = _period.Mwp_val;
        //            WarrantyRemarks = _period.Mwp_rmk;
        //        }
        //        else
        //        {
        //            LogMasterItemWarranty _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_item.Trim(), _status.Trim(), 1, txtDate.Value.Date); if (_periodLog != null) { WarrantyPeriod = _periodLog.Lmwp_val; WarrantyRemarks = _periodLog.Lmwp_rmk; }
        //            else { _isNoWarranty = true; }
        //        }
        //    }
        //    else
        //    {
        //        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
        //        else { _isNoWarranty = true; }
        //    }
        //    //}
        //    //}
        //    return _isNoWarranty;
        //}

        protected void BindAddItem()
        {
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = _invoiceItemList;
        }

        string _serial2 = "";
        string _prefix = "";

        private void AddItem(bool _isPromotion)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;



                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                #region Priority Base Validation
                if (_masterBusinessCompany == null)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer code", "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")
                    if ((Convert.ToDecimal(lblAvailableCredit.Text) - Convert.ToDecimal(txtLineTotAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCustomer.Text != "CASH")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the customer's account balance", "Account Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbLevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the item status", "Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbInvType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    if (chkDeliverLater.Checked == false)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSerialNo.Focus();
                            return;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItem.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount %", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount amount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisAmt.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the VAT amount", "Tax Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaxAmt.Focus();
                    return;
                }



                #endregion

                #region Scan By Serial - check for serial
                //Scan By Serial ------------------start--------------------------

                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                }

                //Scan By Serial -------------------end-------------------------
                #endregion

                #region Price Combine Checking Process
                if (_isCheckedPriceCombine == false)
                    if (_MainPriceCombinItem != null)
                        if (_MainPriceCombinItem.Count > 0)
                        {
                            string _serialiNotpick = string.Empty;
                            string _serialDuplicate = string.Empty;
                            string _taxNotdefine = string.Empty;
                            string _noInventoryBalance = string.Empty;
                            string _noWarrantySetup = string.Empty;

                            string _mItem = txtItem.Text.Trim();


                            var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
                            if (_dupsMain != null)
                                if (_dupsMain.Count() > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    _isCheckedPriceCombine = false;
                                    MessageBox.Show(_mItem + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;
                                string _status = cmbStatus.Text.Trim();


                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                if (_isStrucBaseTax == true)       //kapila 22/4/2016
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);

                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                { if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item; else _taxNotdefine += "," + _item; }

                                if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                                { if (string.IsNullOrEmpty(_noWarrantySetup)) _noWarrantySetup = _item; else _noWarrantySetup += "," + _item; }


                                if (chkDeliverLater.Checked == false && _isCheckedPriceCombine == false)
                                {
                                    _isCheckedPriceCombine = true;
                                    _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    if (_itm.Mi_is_ser1 == 1)
                                    {
                                        var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);

                                        if (_qty > _exist.Count())
                                        { if (string.IsNullOrEmpty(_serialiNotpick)) _serialiNotpick = _item; else _serialiNotpick += "," + _item; }

                                        foreach (ReptPickSerials _p in _exist)
                                        {
                                            string _serial = _p.Tus_ser_1;
                                            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);

                                            if (_dup != null)
                                                if (_dup.Count() > 0)
                                                {
                                                    if (string.IsNullOrEmpty(_serialDuplicate)) _serialDuplicate = _item + "/" + _serial;
                                                    else _serialDuplicate = "," + _item + "/" + _serial;
                                                }
                                        }
                                    }

                                    decimal _pickQty = 0;
                                    if (IsPriceLevelAllowDoAnyStatus)
                                        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum();
                                    else
                                        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                                    _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                    MasterItem _itmS = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    if (!IsGiftVoucher(_itmS.Mi_itm_tp))
                                    {
                                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, cmbStatus.Text.Trim());

                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                {
                                                    if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                                    else _noInventoryBalance = "," + _item;
                                                }
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                                else _noInventoryBalance = "," + _item;
                                            }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                            else _noInventoryBalance = "," + _item;
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(_taxNotdefine))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_serialiNotpick))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show("Item Qty and picked serial mismatch for the following item(s) " + _serialiNotpick, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_serialDuplicate))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show("Serial duplicating for the following item(s) " + _serialDuplicate, "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noInventoryBalance))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_noInventoryBalance + " item(s) does not having inventory balance for give.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noWarrantySetup))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_noWarrantySetup + " item(s)'s warranty not define.", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            _isFirstPriceComItem = true;
                            _isCheckedPriceCombine = true;
                        }

                #endregion

                #region  Adding Com Items - Inventory Comcodes

                if (_isCompleteCode && _isInventoryCombineAdded == false) BindItemComponent(txtItem.Text);

                if (_masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
                {
                    //InventoryCombinItemSerialList = new List<ReptPickSerials>();
                    string _combineStatus = string.Empty;
                    decimal _discountRate = -1;
                    decimal _combineQty = 0;
                    string _mainItem = string.Empty;
                    _combineCounter = 0;

                    _isInventoryCombineAdded = true; _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                    if (_discountRate == -1) _discountRate = Convert.ToDecimal(txtDisRate.Text);

                    List<MasterItemComponent> _comItem = new List<MasterItemComponent>();

                    #region Com item check after pick serial (check com main item seperatly, coz its serial already in txtSerialNo textbox)

                    foreach (string _item in _masterItemComponent.Select(x => x.ComponentItem.Mi_cd))
                        _masterItemComponent.Where(s => s.ComponentItem.Mi_cd == _item).ToList().ForEach(y => y.ComponentItem.Mi_itm_tp = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item).Mi_itm_tp);

                    #region Main Item Check
                    var _item_ = (from _n in _masterItemComponent where _n.Micp_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text));

                        if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_mItem + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }

                        if (_priceDetailRef.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(_item_[0].ToString() + " does not having price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                    }
                    #endregion

                    #region Sub Item Cheking for Warranty
                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }

                    }
                    #endregion
                    #region Serial Check for Main and Sub Items
                    bool _isMainSerialCheck = false;
                    if (ScanSerialList != null && chkDeliverLater.Checked == false)
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
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show(_item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        _isInventoryCombineAdded = false;
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
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                            }
                        }

                    }
                    #endregion

                    #endregion

                    #region Com item check for its serial status
                    if (InventoryCombinItemSerialList.Count == 0)
                    {
                        _isCombineAdding = true;
                        foreach (MasterItemComponent _com in _masterItemComponent)
                        {
                            List<MasterItemTax> _taxs = new List<MasterItemTax>();
                            if (_isStrucBaseTax == true)       //kapila 22/4/2016
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, "VAT", string.Empty, _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);

                            if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isInventoryCombineAdded = false;
                                return;
                            }


                            if (chkDeliverLater.Checked == false)
                            {
                                decimal _pickQty = 0;
                                if (IsPriceLevelAllowDoAnyStatus)
                                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                                else
                                    _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _com.ComponentItem.Mi_cd, cmbStatus.Text.Trim());

                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_pickQty > _invBal)
                                        {
                                            this.Cursor = Cursors.Default;
                                            MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _isInventoryCombineAdded = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _isInventoryCombineAdded = false;
                                    return;
                                }
                            }



                            _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);

                            if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
                            {
                                _comItem.Add(_com);
                            }
                        }

                        if (_comItem.Count > 1 && chkDeliverLater.Checked == false)
                        {//hdnItemCode.value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
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

                            var _listComItem = (from _one in _comItem
                                                where _one.ComponentItem.Mi_itm_tp != "M"
                                                select new
                                                {
                                                    Mi_cd = _one.ComponentItem.Mi_cd,
                                                    Mi_longdesc = _one.ComponentItem.Mi_longdesc,
                                                    Micp_itm_cd = _one.Micp_itm_cd,
                                                    Micp_qty = _one.Micp_qty,
                                                    Mi_itm_tp = _one.ComponentItem.Mi_itm_tp
                                                }).ToList();

                            gvPopComItem.DataSource = _listComItem;
                            pnlInventoryCombineSerialPick.Visible = true;
                            pnlMain.Enabled = false;
                            _isInventoryCombineAdded = false;
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (_comItem.Count == 1 && chkDeliverLater.Checked == false)
                        {//hdnItemCode.Value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
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
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        //If going to deliver now
                        if (chkDeliverLater.Checked == false && InventoryCombinItemSerialList.Count > 0)
                        {
                            var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                            if (_comItemSer != null)
                                if (_comItemSer.Count > 0)
                                {
                                    foreach (ReptPickSerials _serItm in _comItemSer)
                                    {
                                        txtSerialNo.Text = _serItm.Tus_ser_1;
                                        ScanSerialNo = txtSerialNo.Text;
                                        //hdnSerialNo.Value = ScanSerialNo;
                                        txtSerialNo.Text = ScanSerialNo;
                                        txtItem.Text = _com.ComponentItem.Mi_cd;
                                        //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                                        //LoadItemDetail(txtItem.Text);
                                        cmbStatus.Text = _combineStatus;
                                        txtQty.Text = FormatToQty("1");
                                        CheckQty();
                                        txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                        txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
                                        txtLineTotAmt.Text = FormatToCurrency("0");
                                        CalculateItem();
                                        AddItem(false);
                                        ScanSerialNo = string.Empty;
                                        txtSerialNo.Text = string.Empty;
                                        //hdnSerialNo.Value = "";
                                        txtSerialNo.Text = string.Empty;
                                    }
                                    _combineCounter += 1;
                                }
                                else
                                {
                                    txtItem.Text = _com.ComponentItem.Mi_cd;
                                    //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                                    //LoadItemDetail(txtItem.Text);
                                    cmbStatus.Text = _combineStatus;
                                    txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                                    CheckQty();
                                    txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(false);
                                    ScanSerialNo = string.Empty;
                                    txtSerialNo.Text = string.Empty;
                                    //hdnSerialNo.Value = "";
                                    txtSerialNo.Text = string.Empty;

                                    _combineCounter += 1;
                                }

                        }
                        //If deliver later
                        else if (chkDeliverLater.Checked && InventoryCombinItemSerialList.Count == 0)
                        {
                            txtItem.Text = _com.ComponentItem.Mi_cd;
                            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                            LoadItemDetail(txtItem.Text.Trim());
                            cmbStatus.Text = _combineStatus;
                            txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                            CheckQty();
                            txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                            txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
                            txtLineTotAmt.Text = FormatToCurrency("0");
                            CalculateItem();
                            AddItem(false);
                            _combineCounter += 1;
                        }

                    }
                    #endregion

                    if (_combineCounter == _masterItemComponent.Count) { _masterItemComponent = new List<MasterItemComponent>(); _isCompleteCode = false; _isInventoryCombineAdded = false; _isCombineAdding = false; ScanSerialNo = string.Empty; InventoryCombinItemSerialList = new List<ReptPickSerials>(); txtSerialNo.Text = string.Empty; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; } //hdnSerialNo.Value = ""
                }

                #endregion

                #region Check item with serial status & load particular serial details

                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                if (chkDeliverLater.Checked == false || IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isInventoryCombineAdded = false;
                            txtSerialNo.Focus();
                            return;
                        }
                        if (!IsGiftVoucher(_itm.Mi_itm_tp)) _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                        else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);
                        //_serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                    }
                    else if (_itm.Mi_is_ser1 == 0)
                    {
                        if (IsPriceLevelAllowDoAnyStatus == false)
                            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                        else
                            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()));
                    }
                }
                #endregion

                #region Check for fulfilment before adding
                if (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance))
                {
                    if (!_isCombineAdding && !_MasterProfitCenter.Mpc_without_price)
                    {
                        this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text) == 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                if (!_isCombineAdding)
                {
                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila 22/4/2016
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);

                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbStatus.Focus();
                        return;
                    }
                }

                if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false)
                {
                    bool _isTerminate = CheckQty();
                    if (_isTerminate) { this.Cursor = Cursors.Default; return; }
                }

                if (CheckBlockItem(txtItem.Text.Trim(), SSPRomotionType, _isCombineAdding)) //added by akila 2017/08/04
                    return;

                if (_isCombineAdding == false)
                {
                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_lsts != null && _isCombineAdding == false && !_MasterProfitCenter.Mpc_without_price)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //decimal sysUPrice = _lsts.Sapd_itm_price * (_priceBookLevelRef.Sapl_vat_calc == true ? MainTaxConstant[0].Mict_tax_rate : 1); commnet by tharanga 2018/04/10
                            decimal sysUPrice = Math.Round( _lsts.Sapd_itm_price * (_priceBookLevelRef.Sapl_vat_calc == true ? MainTaxConstant[0].Mict_tax_rate : 1),2);//add by tharanga 2018/04/10
                           // string VAL= Convert.ToString( _lsts.Sapd_itm_price * (_priceBookLevelRef.Sapl_vat_calc == true ? MainTaxConstant[0].Mict_tax_rate : 1));
                            string VAL = FormatToCurrency(Convert.ToString(_lsts.Sapd_itm_price * (_priceBookLevelRef.Sapl_vat_calc == true ? MainTaxConstant[0].Mict_tax_rate : 1)));
                            sysUPrice = Convert.ToDecimal(VAL);
                            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
                            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                        if (!_MasterProfitCenter.Mpc_edit_price)
                                        {
                                            if (sysUPrice != pickUPrice)
                                            {
                                                this.Cursor = Cursors.Default;
                                                MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (sysUPrice != pickUPrice)
                                                if (sysUPrice > pickUPrice)
                                                {
                                                    decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
                                                    decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                                                    if (ddUprice > pickUPrice)
                                                    {
                                                        this.Cursor = Cursors.Default;
                                                        MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }
                                                }

                                        }
                        }
                    }
                    else
                    {
                        _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);

                        if (_isCombineAdding == false && !_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p && _priceBookLevelRef.Sapl_is_serialized == false)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                #endregion

                #region Check Item Serial pick or not (function for common item - not for comcode items, but its go through here also

                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false)
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text && x.Tus_ser_1 == ScanSerialNo).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(ScanSerialNo + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtSerialNo.Focus();
                                    return;
                                }
                        }

                        if (!IsPriceLevelAllowDoAnyStatus)
                        {
                            if (_serLst != null)
                                if (string.IsNullOrEmpty(_serLst.Tus_com))
                                {
                                    if (_serLst.Tus_itm_stus != cmbStatus.Text.Trim())
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show(ScanSerialNo + " serial status is not match with the price level status", "Price Level Restriction", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false)
                    {
                        decimal _pickQty = 0;
                        if (IsPriceLevelAllowDoAnyStatus)
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                        else
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());

                        if (_inventoryLocation != null)
                            if (_inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Selected Serial is duplicating.", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                        }
                    }
                    else
                    {
                        decimal _pickQty = 0;
                        if (IsPriceLevelAllowDoAnyStatus)
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                        else
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());

                        if (_inventoryLocation != null)
                            if (_inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)
                                {
                                    this.Cursor = Cursors.Default;
                                    if (MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)) + " \nCancel item add??", "Inventory Balance", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        return;
                                    }

                                }
                            }
                    }
                //check balance ----------------------
                #endregion

                #region Get/Check Warranty Period and Remarks
                //Get Warranty Details --------------------------
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;//CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.Text.Trim() select _l).ToList();
                        if (_lst != null)
                            if (_lst.Count > 0)
                            {
                                DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date);
                                if (_lst[0].Sapl_set_warr == true)
                                {
                                    WarrantyPeriod = _lst[0].Sapl_warr_period;
                                }
                                else if (_temWarr != null && _temWarr.Rows.Count > 0)
                                {
                                    WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                                    WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
                                }
                                else
                                {
                                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.Trim(), cmbStatus.Text.Trim());
                                    if (_period != null)
                                    {
                                        WarrantyPeriod = _period.Mwp_val;
                                        WarrantyRemarks = _period.Mwp_rmk;
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                       where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text)
                                       select _list;

                    if (_similerItem.Count() > 0)
                    //Similer item available
                    {
                        _isDuplicateItem = true;
                        foreach (var _similerList in _similerItem)
                        {
                            _duplicateComLine = _similerList.Sad_job_line;
                            _duplicateItmLine = _similerList.Sad_itm_line;
                            _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                            _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                            _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                            _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);

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
                if (chkDeliverLater.Checked == false || IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    if (_isFirstPriceComItem)
                        _isCombineAdding = true;

                    if (ScanSequanceNo == 0) ScanSequanceNo = -100;

                    //Serialized
                    if (_itm.Mi_is_ser1 == 1)
                    {

                        //ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                        _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                        _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                        _serLst.Tus_usrseq_no = ScanSequanceNo;
                        _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                        _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                        _serLst.ItemType = _itm.Mi_itm_tp;
                        ScanSerialList.Add(_serLst);
                    }

                    //Non-Serialized but serial ID 8523
                    if (_itm.Mi_is_ser1 == 0)
                    {
                        //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                    var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());
                    gvGiftVoucher.DataSource = filenamesList;

                    if (_isFirstPriceComItem)
                    {
                        _isCombineAdding = false;
                        _isFirstPriceComItem = false;
                    }

                    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;

                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = ScanSerialList;//.Where(x => x.Tus_ser_1 != "N/A").ToList();


                    if (_isFirstPriceComItem)
                    {
                        _isCombineAdding = false;
                        _isFirstPriceComItem = false;
                    }
                }

                gvPopSerial.DataSource = new List<ReptPickSerials>();
                gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                var filenamesList1 = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());
                gvGiftVoucher.DataSource = filenamesList1;
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
                        {//hdnItemCode.Value
                            var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.Trim() select _i).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                    _isDuplicate = true;
                        }
                    }

                if (_isDuplicate == false)
                {
                    //hdnItemCode.Value.ToString()
                    InvoiceSerial _invser = new InvoiceSerial();
                    _invser.Sap_del_loc = BaseCls.GlbUserDefLoca;
                    _invser.Sap_itm_cd = txtItem.Text.Trim();
                    _invser.Sap_itm_line = _lineNo;
                    _invser.Sap_remarks = string.Empty;
                    _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                    _invser.Sap_ser_1 = txtSerialNo.Text;
                    _invser.Sap_ser_2 = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                    InvoiceSerialList.Add(_invser);
                }
                //}
                #endregion

                CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);

                #region  Adding Combine Items - Price Combine
                if (_MainPriceCombinItem != null)
                {
                    string _combineStatus = string.Empty;
                    decimal _combineQty = 0;
                    bool _isSingleItemSerializedInCombine = true;
                    if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                    {
                        _isCombineAdding = true;
                        if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                        if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                        if (chkDeliverLater.Checked == true)
                        {
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                // _combineStatus = _list.Status; 
                                if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                                LoadItemDetail(txtItem.Text.Trim());
                                if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                                {
                                    //get distinct serial list
                                    List<ReptPickSerials> _distinctList = PriceCombinItemSerialList.GroupBy(x => x.Tus_itm_cd).Select(g => g.First()).ToList<ReptPickSerials>();
                                    foreach (ReptPickSerials _lists in _distinctList.Where(x => x.Tus_itm_cd == txtItem.Text.Trim()).ToList())
                                    {
                                        txtSerialNo.Text = _lists.Tus_ser_1;
                                        ScanSerialNo = _lists.Tus_ser_1;
                                        string _originalItms = _lists.Tus_session_id;
                                        if (string.IsNullOrEmpty(_originalItm))
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty))); else txtQty.Text = FormatToQty(Convert.ToString((1)));
                                            txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem(); AddItem(_isPromotion);
                                        }
                                        else
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum(); txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty))); else txtQty.Text = FormatToQty(Convert.ToString((1)));
                                            txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0"); cmbStatus.Text = "GOD";
                                            CalculateItem(); AddItem(_isPromotion);
                                        }
                                        _combineCounter += 1;
                                    }
                                }
                                else
                                {
                                    cmbStatus.Text = _combineStatus; txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty /* * _combineQty */))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion);
                                    _combineCounter += 1;
                                }
                            }
                        }
                        else
                        {
                            if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0) _isSingleItemSerializedInCombine = false;
                            foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                            {
                                txtSerialNo.Text = _list.Tus_ser_1;
                                ScanSerialNo = _list.Tus_ser_1;
                                string _originalItm = _list.Tus_session_id;
                                _combineStatus = _list.Tus_itm_stus;
                                if (string.IsNullOrEmpty(_originalItm))
                                {
                                    txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice)); txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion);
                                }
                                else
                                {
                                    txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                    var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                                    bool _isIncreaseable = Convert.ToBoolean(_Increaseable[0]); txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    if (_isIncreaseable) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion);
                                }
                                _combineCounter += 1;
                            }
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                MasterItem _i = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _list.Sapc_itm_cd);
                                _combineStatus = _list.Status;
                                if (_i.Mi_is_ser1 != 1)
                                {
                                    string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                    if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                    LoadItemDetail(txtItem.Text.Trim()); cmbStatus.Text = _combineStatus;
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion);
                                    _combineCounter += 1;
                                }
                            }
                        }

                        if (chkDeliverLater.Checked == true)
                            if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes) ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        if (chkDeliverLater.Checked == false)
                        {
                            if (_isSingleItemSerializedInCombine)
                            {
                                if (_combineCounter == PriceCombinItemSerialList.Count)
                                { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes)  ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }
                                else if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes) ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }
                            }
                            else
                                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes)  ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        }
                    }

                }
                #endregion

                txtSerialNo.Text = "";
                //hdnSerialNo.Value = "";
                ClearAfterAddItem();

                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                SSPromotionCode = string.Empty;
                SSPRomotionType = 0;

                txtItem.Focus();
                BindAddItem();
                SetDecimalTextBoxForZero(true);

                decimal _tobepay = 0;
                if (lblSVatStatus.Text == "Available")
                    _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else
                    _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                //ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                //ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());

                ucPayModes1.TotalAmount = _tobepay;
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepay));
                if (ucPayModes1.HavePayModes)
                    ucPayModes1.LoadData();
                this.Cursor = Cursors.Default;
                if (_isCombineAdding == false) { this.Cursor = Cursors.Default;
                ucPayModes1.InvoiceItemList = _invoiceItemList;
                    if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { cmbCurrancy.Focus(); } }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected void AddItem(Object sender, EventArgs e)
        {
            AddItem(SSPromotionCode == "0" ? false : true);
            cmbCurrancy_SelectedIndexChanged(null, null);
        }
        private void SetDecimalTextBoxForZero(bool _isUnit)
        {

            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");

        }
        #endregion

        #region Rooting for Inventory Combine
        private void gvPopComItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPopComItem.ColumnCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                {
                    string _item = gvPopComItem.Rows[_row].Cells["PopComItm_Item"].Value.ToString();
                    string _status = gvPopComItem.Text.Trim();
                    string _qty = gvPopComItem.Rows[_row].Cells["PopComItm_Qty"].Value.ToString();

                    if (chkDeliverLater.Checked == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), false);
                    if (chkDeliverLater.Checked)
                        return;
                }
            }
        }

        private void gvPopComItemSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPopComItemSerial.ColumnCount > 0)
            {
                Int32 _row = e.RowIndex;
                if (_row != -1)
                {
                    DataGridViewCheckBoxCell _cell = gvPopComItemSerial.Rows[_row].Cells["PopComItmSer_Select"] as DataGridViewCheckBoxCell;
                    string _id = gvPopComItemSerial.Rows[_row].Cells["PopComItmSer_Serialid"].Value.ToString();
                    if (Convert.ToBoolean(_cell.Value) == true)
                    {

                        _cell.Value = false;
                        InventoryCombinItemSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(_id));
                    }
                    else
                    {
                        _cell.Value = true;
                        var _n = _promotionSerial.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                        _promotionSerialTemp.AddRange(_n);

                    }
                }
            }
        }

        private void gvPopComItemSerial_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (gvPopComItemSerial.ColumnCount > 0)
            {
                Int32 _rowindex = e.RowIndex;
                if (_rowindex != -1)
                {
                    for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
                    {
                        string _item = gvPopComItemSerial.Rows[index].Cells["PopComItmSer_Item"].Value.ToString();
                        string _serialID = gvPopComItemSerial.Rows[index].Cells["PopComItmSer_Serialid"].Value.ToString();
                        DataGridViewCheckBoxCell _check = gvPopComItemSerial.Rows[index].Cells["PopComItmSer_Select"] as DataGridViewCheckBoxCell;

                        string _selectedid = string.Empty;
                        if (InventoryCombinItemSerialList != null)
                            if (InventoryCombinItemSerialList != null)
                                if (InventoryCombinItemSerialList.Count > 0)
                                {
                                    var _id = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_id == Convert.ToInt32(_serialID)).Select(y => y.Tus_ser_id);
                                    if (_id != null)
                                        if (_id.Count() > 0)
                                        {
                                            foreach (var f in _id)
                                                if (!string.IsNullOrEmpty(Convert.ToString(f)))
                                                    _check.Value = true;
                                        }
                                }

                    }
                }
            }
        }

        private void txtInvComSerSearch_TextChanged(object sender, EventArgs e)
        {
            if (gvPopComItemSerial.ColumnCount > 0)
            {
                if (!string.IsNullOrEmpty(txtInvComSerSearch.Text.Trim()))
                {
                    var query = _promotionSerial.Where(x => x.Tus_ser_1.Contains(txtInvComSerSearch.Text.Trim())).ToList();
                    if (query != null)
                        if (query.Count() > 0)
                            gvPopComItemSerial.DataSource = query;
                        else
                            gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                    else
                        gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                }
                else
                {
                    gvPopComItemSerial.DataSource = _promotionSerial;
                }

                foreach (DataGridViewRow _r in gvPopComItemSerial.Rows)
                {
                    string _id = _r.Cells["PopComItmSer_Serialid"].Value.ToString();
                    DataGridViewCheckBoxCell _chk = _r.Cells["PopComItmSer_Select"] as DataGridViewCheckBoxCell;
                    if (_promotionSerialTemp != null)
                        if (_promotionSerialTemp.Count > 0)
                        {
                            var _exist = _promotionSerialTemp.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                            if (_exist != null)
                                if (_exist.Count > 0)
                                    _chk.Value = true;
                        }
                }
            }
        }

        private void btnInvCombineSerClose_Click(object sender, EventArgs e)
        {
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            pnlMain.Enabled = true;
            pnlInventoryCombineSerialPick.Visible = false;
        }

        private void btnInvComSerClear_Click(object sender, EventArgs e)
        {
            txtInvComSerSearch.Clear();
        }

        private void btnInvComSerConfirm_Click(object sender, EventArgs e)
        {
            txtInvComSerSearch.Text = string.Empty;
            decimal _serialcount = 0;
            decimal _promotionItemQty = Convert.ToDecimal(gvPopComItem.SelectedRows[0].Cells["PopComItm_Qty"].Value);
            string _promotionItem = gvPopComItem.SelectedRows[0].Cells["PopComItm_Item"].Value.ToString();
            foreach (DataGridViewRow _row in gvPopComItemSerial.Rows)
            {
                if (Convert.ToBoolean(_row.Cells["PopComItmSer_Select"].Value) == true)
                    _serialcount += 1;
            }

            if (_serialcount != _promotionItemQty)
            {
                MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (_serialcount > _promotionItemQty)
            {
                MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (InventoryCombinItemSerialList != null)
                if (InventoryCombinItemSerialList.Count > 0)
                {
                    decimal _count = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _promotionItem).Count();
                    if (_count >= _promotionItemQty)
                    {
                        MessageBox.Show("You already pick serials for the item", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            foreach (DataGridViewRow _r in gvPopComItemSerial.Rows)
            {
                if (Convert.ToBoolean(_r.Cells["PopComItmSer_Select"].Value) == true)
                {
                    string _item = Convert.ToString(_r.Cells["PopComItmSer_Item"].Value);
                    string _serial = Convert.ToString(_r.Cells["PopComItmSer_Serial1"].Value);

                    ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serial);
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
                                            MessageBox.Show("Selected serial is duplicating!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            List<ReptPickSerials> _lst = new List<ReptPickSerials>();
            gvPopComItemSerial.DataSource = _lst;
        }

        private void btnInvComSerTotConfirm_Click(object sender, EventArgs e)
        {

            if (InventoryCombinItemSerialList != null)
                if (InventoryCombinItemSerialList.Count > 0)
                {
                    foreach (DataGridViewRow _r in gvPopComItem.Rows)
                    {
                        string _item = _r.Cells["PopComItm_Item"].Value.ToString();
                        decimal _qty = Convert.ToDecimal(_r.Cells["PopComItm_Qty"].Value.ToString());

                        var _serCount = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Count();
                        if (_serCount > 0)
                        {
                            if (Convert.ToDecimal(_serCount) != _qty)
                            {
                                MessageBox.Show("Scan Serial and the qty is mismatching. No of serials : " + FormatToQty(Convert.ToString(Convert.ToDecimal(_serCount))) + ", but approved only " + FormatToQty(Convert.ToString(_qty)), "Qty and Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Scan Serial and the qty is mismatching. No of serials : " + FormatToQty(Convert.ToString(Convert.ToDecimal("0"))) + ", but approved only " + FormatToQty(Convert.ToString(_qty)), "Qty and Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    _promotionSerial = new List<ReptPickSerials>();
                    _promotionSerialTemp = new List<ReptPickSerials>();
                    pnlMain.Enabled = true;
                    pnlInventoryCombineSerialPick.Visible = false;
                    AddItem(false);
                }
        }
        #endregion

        #region Rooting for Invoice Item Event
        private void gvInvoiceItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvInvoiceItem.ColumnCount > 0)
            {
                Int32 _rowIndex = e.RowIndex;
                Int32 _colIndex = e.ColumnIndex;
                if (_rowIndex != -1)
                {
                    #region Deleting Row
                    if (_colIndex == 0)
                    {
                        if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        Int32 _combineLine = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);
                        if (_recieptItem != null)
                            if (_recieptItem.Count > 0)
                            {
                                MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        if (_MainPriceSerial != null)
                            if (_MainPriceSerial.Count > 0)
                            {

                                //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                                string _item = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value;
                                decimal _uRate = (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value;
                                string _pbook = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value;
                                string _level = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value;

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
                            CalculateGrandTotal(Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value), (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_DisAmt"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_TaxAmt"].Value, false);
                            InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[_rowIndex].Sad_itm_line);
                            _tempList.RemoveAt(_rowIndex);
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
                        gvPopSerial.DataSource = new List<ReptPickSerials>();
                        gvPopSerial.DataSource = ScanSerialList;//.Where(x => x.Tus_ser_1 != "N/A").ToList(); ;

                        ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                        ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
                        ucPayModes1.LoadData();
                        return;
                    }
                    #endregion
                    #region Pick Serial Show
                    if (_colIndex == 1)
                    {

                        return;
                    }
                    #endregion
                    #region Add Serial
                    if (_colIndex == 2)
                    {

                        return;
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Rooting for Prerequities for Save/Hold the invoice
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
                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (IsGiftVoucher(_itm.Mi_itm_tp)) continue;
                //Serialized Item
                if (_itm.Mi_is_ser1 == 1)
                {
                    ReptPickSerials _chk = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serialno);
                    if (string.IsNullOrEmpty(_chk.Tus_com)) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else if (IsPriceLevelAllowDoAnyStatus == false)
                        if (_chk.Tus_itm_stus != _status) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                //Non-serialized item but have serial line
                else if (_itm.Mi_is_ser1 == 0)
                {
                    List<ReptPickSerials> _chk;
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _status, _qty);
                    else
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty, _qty);


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
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);

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
        private void CollectBusinessEntity()
        {
            _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_act = true;
            _businessEntity.Mbe_add1 = txtAddress1.Text;
            _businessEntity.Mbe_add2 = txtAddress2.Text;
            _businessEntity.Mbe_cd = "c1";
            _businessEntity.Mbe_com = BaseCls.GlbUserComCode;
            _businessEntity.Mbe_contact = string.Empty;
            _businessEntity.Mbe_email = string.Empty;
            _businessEntity.Mbe_fax = string.Empty;
            _businessEntity.Mbe_is_tax = false;
            // _businessEntity.Mbe_mob = txtMobile.Text;
            _businessEntity.Mbe_name = txtCusName.Text;
            _businessEntity.Mbe_nic = txtNIC.Text;
            _businessEntity.Mbe_tax_no = string.Empty;
            _businessEntity.Mbe_tel = string.Empty;
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_pp_no = txtPPNo.Text;
            _businessEntity.Mbe_pc_stus = "GOOD";
            _businessEntity.Mbe_ho_stus = "GOOD";
        }
        private Dictionary<string, string> GetInvoiceSerialnWarranty(string _invoiceno)
        {
            StringBuilder _serial = new StringBuilder();
            StringBuilder _warranty = new StringBuilder();
            Dictionary<string, string> _list = new Dictionary<string, string>();

            List<ReptPickSerials> _advSerial = CHNLSVC.Inventory.GetInvoiceAdvanceReceiptSerial(BaseCls.GlbUserComCode, _invoiceno);
            List<InventorySerialN> _invSerial = CHNLSVC.Inventory.GetDeliveredSerialDetail(BaseCls.GlbUserComCode, _invoiceno);

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

        #region Rooting for Back Date Checking When Process

        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _alwCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty.ToUpper().ToString(), BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _alwCurrentTrans) == false)
            {
                if (_alwCurrentTrans == true)
                {

                    if (txtDate.Value.Date != DateTime.Now.AddHours(_MasterProfitCenter.Mpc_add_hours).Date)
                    {
                        txtDate.Enabled = true;
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }

            }

            //if (!_isDelverNow && _isOK)
            //{
            //    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), "m_Trans_Inventory_CustomerDeliveryOrder", txtDate, lblBackDateInfor, txtDate.Value.Date.ToString()) == false)
            //    {
            //        if (txtDate.Value.Date != DateTime.Now.Date)
            //        {
            //            txtDate.Enabled = true;
            //            MessageBox.Show("Back date not allow for selected date for the location " + BaseCls.GlbUserDefLoca + "!(Delivery Order).", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtDate.Focus();
            //            _isOK = false;
            //            _isBackDate = false;
            //            return _isOK;
            //        }
            //    }

            //}

            //if (_isBuyBackItemAvailable && _isOK)
            //{
            //    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), "m_Trans_Inventory_StockAdjustment", txtDate, lblBackDateInfor, txtDate.Value.Date.ToString()) == false)
            //    {
            //        if (txtDate.Value.Date != DateTime.Now.Date)
            //        {
            //            txtDate.Enabled = true;
            //            MessageBox.Show("Back date not allow for selected date for the location " + BaseCls.GlbUserDefLoca + "!(BuyBack).", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtDate.Focus();
            //            _isOK = false;
            //            _isBackDate = false;
            //            return _isOK;
            //        }
            //    }

            //}

            return _isOK;
        }

        #endregion

        #region Rooting for Save Invoice
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (MessageBox.Show("Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
                {
                    MessageBox.Show("Please select executive before save.", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                //{
                //    MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtCustomer.Clear();
                //    txtCustomer.Focus();
                //    return;
                //}

                //validate customer code
                MasterBusinessEntity _customer = CHNLSVC.Sales.GetCustomerProfile(txtCustomer.Text, "", "", "", "");
                //invalid customer code
                if (_customer.Mbe_cd == null)
                {
                    MessageBox.Show("Entered customer code is invalid.Please check again", "Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustomer.Focus();
                    return;
                }


                //end

                if (pnlMain.Enabled == false) return;

                if (IsBackDateOk(chkDeliverLater.Checked, true) == false) return;

                bool _isHoldInvoiceProcess = false;
                InvoiceHeader _hdr = new InvoiceHeader();

                #region Check the invoice no for edit
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                    if (_hdr != null)
                        if (_hdr.Sah_stus != "H")
                        {
                            MessageBox.Show("You can not edit already saved invoice", "Invoice Re-call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                }
                #endregion

                #region Get to know whether recalled invoice is Hold invoice & tag as hold
                if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
                if (_isHoldInvoiceProcess && chkDeliverLater.Checked == false)
                {
                    MessageBox.Show("You can not use 'Deliver Now!' option for hold invoice", "Invoice Hold", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region Check for fulfilment for the save process
                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbInvType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbLevel.Focus();
                    return;
                }

                if (_invoiceItemList.Count <= 0)
                {
                    MessageBox.Show("Please select the items for invoice", "Invoice item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtExecutive.Text))
                {
                    MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExecutive.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblCurrency.Text))
                {
                    MessageBox.Show("Please select the currency code", "Currency", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblCurrency.Focus();
                    return;
                }

                if (_MasterProfitCenter.Mpc_check_pay && _recieptItem.Count <= 0)
                {
                    MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    MessageBox.Show("Please enter the customer name", "Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text) && string.IsNullOrEmpty(txtAddress2.Text))
                {
                    MessageBox.Show("Please enter the customer address", "Customer Address", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #endregion

                #region Check for payment if the invoice tyoe is cash
                if (cmbInvType.Text == "CS")
                    if (_recieptItem == null)
                    {
                        MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count <= 0)
                        {
                            MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count >= 0)
                        {
                            decimal _realPay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                            else
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());


                            decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                            //if (_totlaPay != Convert.ToDecimal(lblGrndTotalAmount.Text))
                            //if (_totlaPay != _realPay)
                            //{
                            //    MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    return;
                            //}


                            if (_totlaPay < _realPay)
                            {
                                MessageBox.Show("Please enter the full payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            //add minus record
                            if (_totlaPay > _realPay)
                            {
                                RecieptItem _rec = new RecieptItem();
                                _rec.Sard_inv_no = _recieptItem[0].Sard_inv_no;
                                _rec.Sard_pay_tp = "CASH";
                                _rec.Sard_rmk = "Balance";
                                _rec.Sard_settle_amt = (-1) * (_totlaPay - _realPay);
                                _rec.Sard_anal_4 = 1;
                                _rec.Sard_anal_1 = _MasterProfitCenter.Mpc_def_exrate;
                                _recieptItem.Add(_rec);
                            }
                        }

                #endregion

                #region Check for availability of the invoice prefix
                string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);

                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                Int32 _count = 1;
                #region Assigning new receipt line no to recipt items
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
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

                #region Check Referance Date and the Doc Date
                if (IsReferancedDocDateAppropriate(ScanSerialList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    return;
                #endregion

                #region Deliver Now! - Check for serialied item qty with it's scan serial count
                if (chkDeliverLater.Checked == false)
                {
                    string _itmList = string.Empty;
                    bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);
                    if (_isqtyNserialOk == false)
                    {
                        MessageBox.Show("Invoice qty and no. of serials are mismatched. Please check the following item for its serials and qty./n Item List : " + _itmList, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                #endregion

                #region Deliver Now! - Check the Inventory Availability
                if (chkDeliverLater.Checked == false)
                {
                    string _nottallylist = string.Empty;
                    bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                    if (_isTallywithinventory == false)
                    {
                        MessageBox.Show("Following item does not having inventory balance for raise delivery order; " + _nottallylist, "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

                _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }

                #endregion

                #region Inventory Header Value Assign
                invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                invHdr.Ith_com = BaseCls.GlbUserComCode;
                invHdr.Ith_doc_tp = "DO";
                invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                invHdr.Ith_cate_tp = "DPS";
                invHdr.Ith_sub_tp = "DPS";
                invHdr.Ith_bus_entity = txtCustomer.Text.Trim();
                invHdr.Ith_del_code = txtCustomer.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress1.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress2.Text.Trim();
                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = BaseCls.GlbUserID;
                invHdr.Ith_mod_by = BaseCls.GlbUserID;
                invHdr.Ith_direct = false;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                invHdr.Ith_manual_ref = txtManualRefNo.Text;
                invHdr.Ith_vehi_no = string.Empty;
                invHdr.Ith_remarks = string.Empty;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                #endregion

                #region Inventory AutoNumber Value Assign
                MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _masterAutoDo.Aut_cate_tp = "LOC";
                _masterAutoDo.Aut_direction = 0;
                _masterAutoDo.Aut_moduleid = "DO";
                _masterAutoDo.Aut_start_char = "DO";
                _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                #endregion

                #region Invoice Header Value Assign
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = Convert.ToDateTime(txtDate.Text);
                _invheader.Sah_currency = lblCurrency.Text;
                _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
                _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                _invheader.Sah_cus_name = txtCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                _invheader.Sah_d_cust_name = txtDelName.Text.Trim();
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text).Date;
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
                if (string.IsNullOrEmpty(Convert.ToString(cmbTechnician.SelectedValue))) _invheader.Sah_anal_1 = string.Empty;
                else _invheader.Sah_anal_1 = Convert.ToString(cmbTechnician.SelectedValue);
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                MasterCompany _com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                decimal _exchangRate = 1;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, _pc.Mpc_def_exrate, Convert.ToDateTime(txtDate.Text), _com.Mc_cur_cd, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                if (_exchangRate == 1)
                {
                    if (MessageBox.Show("Exchange rate between your profit center and company set as 1.\nDo you want to Proceed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                _invheader.Sah_ex_rt = _exchangRate;
                /*
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = "";
                _invheader.Sah_man_ref = txtManualRefNo.Text;
                _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtDocRefNo.Text;
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
                if (chkDeliverLater.Checked == false) _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_anal_1 = BaseCls.GlbUserDefLoca;
                _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = chkDeliverLater.Checked ? 0 : 1;

                _invheader.Sah_del_loc = chkDeliverLater.Checked == false ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                _invheader.Sah_grn_com = _customerCompany;
                _invheader.Sah_grn_loc = _customerLocation;
                _invheader.Sah_is_grn = _isCustomerHasCompany;

                _invheader.Sah_grup_cd = "";//string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;

                _invheader.Sah_anal_4 = txtPoNo.Text.Trim();
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
                _invheader.Sah_anal_6 = "";
                _invheader.Sah_structure_seq = "";
                */
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                // _invheader.Sah_currency = "LKR";
                _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
                _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                _invheader.Sah_cus_name = txtCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                _invheader.Sah_d_cust_name = txtDelName.Text.Trim();
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text).Date;
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                //_invheader.Sah_ex_rt = 1;
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_ref = txtManualRefNo.Text;
                _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtDocRefNo.Text;
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
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = "";
                _invheader.Sah_stus = "A";
                if (chkDeliverLater.Checked == false) _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = (chkDeliverLater.Checked) ? 0 : 1;
                _invheader.Sah_del_loc = (chkDeliverLater.Checked == false) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                _invheader.Sah_grn_com = _customerCompany;
                _invheader.Sah_grn_loc = _customerLocation;
                _invheader.Sah_is_grn = _isCustomerHasCompany;
                _invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
                _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                _invheader.Sah_anal_4 = txtPoNo.Text.Trim();
                _invheader.Sah_anal_6 = "";
                _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();

                if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());

                #endregion

                #region Receipt Header Value Assign
                _recHeader.Sar_acc_no = "";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = Convert.ToDateTime(txtDate.Text);
                _recHeader.Sar_currency_cd = "LKR";//txtCurrency.Text;
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
                // _recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_nic_no = txtNIC.Text;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = "";
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
                _recHeader.Sar_receipt_no = "na";
                _recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = string.Empty;// txtPayRemarks.Text;
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;
                _recHeader.Sar_tot_settle_amt = 0;
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                #endregion

                #region Invoice AutoNumber Value Assign
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = cmbInvType.Text;
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
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                    }
                #endregion

                if (txtCustomer.Text.Trim() != "CASH")
                {
                    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCustomer.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                    if (_en != null)
                        if (string.IsNullOrEmpty(_en.Mbe_com))
                        {
                            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
                            _invheader.Sah_is_svat = _en.Mbe_is_svat;
                        }
                }

                List<InvoiceVoucher> _giftVoucher = null;
                List<ReptPickSerials> _gvLst = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();
                if (_gvLst != null)
                    if (_gvLst.Count > 0)
                    {
                        _giftVoucher = new List<InvoiceVoucher>();
                        foreach (ReptPickSerials _one in _gvLst)
                        {
                            string _attachedItem = string.Empty;
                            if (gf_assignItem.Visible)
                            {
                                _attachedItem = (from DataGridViewRow _row in gvGiftVoucher.Rows where Convert.ToString(_row.Cells["gf_serial1"].Value) == _one.Tus_ser_1 && Convert.ToString(_row.Cells["gf_serial2"].Value) == _one.Tus_ser_2 && Convert.ToString(_row.Cells["gf_item"].Value) == _one.Tus_itm_cd select Convert.ToString(_row.Cells[7].Value)).ToList()[0];
                                if (string.IsNullOrEmpty(_attachedItem))
                                    _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];
                            }
                            else
                                _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];
                            InvoiceVoucher _gift = new InvoiceVoucher();
                            _gift.Stvo_bookno = Convert.ToInt32(_one.Tus_ser_2);
                            _gift.Stvo_cre_by = BaseCls.GlbUserID;
                            _gift.Stvo_cre_when = DateTime.Now;
                            _gift.Stvo_gv_itm = _one.Tus_itm_cd;
                            _gift.Stvo_inv_no = string.Empty;
                            _gift.Stvo_itm_cd = _attachedItem;
                            _gift.Stvo_pageno = Convert.ToInt32(_one.Tus_ser_1);
                            _gift.Stvo_prefix = _one.Tus_ser_3;
                            _gift.Stvo_price = _one.Tus_unit_price;
                            _giftVoucher.Add(_gift);
                            ScanSerialList.Remove(_one);
                        }
                    }


                CollectBusinessEntity();

                string _invoiceNo = "";
                string _receiptNo = "";
                string _deliveryOrderNo = "";


                _invoiceItemListWithDiscount = new List<InvoiceItem>();
                List<InvoiceItem> _discounted = null;
                bool _isDifferent = false;
                decimal _tobepay = 0;

                Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                _invoiceItemListWithDiscount = _discounted;


                if (_isDifferent)
                {
                    //lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                    //gvRePayment.DataSource = _recieptItem;
                    //_toBePayNewAmount = _tobepay;
                    //gvRePayment.DataBind();
                    //MPEReAddPayment.Show();
                    //return;
                }

                //foreach (InvoiceItem _itmWar in _invoiceItemList)
                //{
                //    //Check Selected price book and level is warranty base price level.
                //    PriceBookLevelRef _pbLvl = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _itmWar.Sad_pbook, _itmWar.Sad_pb_lvl, _itmWar.Sad_itm_stus);
                //    if (_pbLvl != null)
                //    {
                //        if (_pbLvl.Sapl_set_warr == true || txtDate.Value.Date != _serverDt)
                //        {
                //            if (CheckItemWarrantyNew(_itmWar.Sad_itm_cd, _itmWar.Sad_itm_stus, _itmWar.Sad_seq, _itmWar.Sad_itm_seq, _itmWar.Sad_pbook, _itmWar.Sad_pb_lvl, true, _itmWar.Sad_unit_rt, _pbLvl.Sapl_warr_period))
                //            {
                //                MessageBox.Show(_itmWar.Sad_itm_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //            else
                //            {
                //                _itmWar.Sad_warr_period = WarrantyPeriod;
                //                _itmWar.Sad_warr_remarks = WarrantyRemarks;
                //            }
                //        }
                //    }
                //    else if (txtDate.Value.Date != _serverDt)
                //    {
                //        if (CheckItemWarrantyNew(_itmWar.Sad_itm_cd, _itmWar.Sad_itm_stus, _itmWar.Sad_seq, _itmWar.Sad_itm_seq, _itmWar.Sad_pbook, _itmWar.Sad_pb_lvl, false, _itmWar.Sad_unit_rt, _pbLvl.Sapl_warr_period))
                //        {
                //            MessageBox.Show(_itmWar.Sad_itm_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //        else
                //        {
                //            _itmWar.Sad_warr_period = WarrantyPeriod;
                //            _itmWar.Sad_warr_remarks = WarrantyRemarks;
                //        }
                //    }
                //}

                int effect = -1;
                string _error = string.Empty;
                try
                {

                    btnSave.Enabled = false;
                    string _buybackno = string.Empty;

                    // effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, chkDeliverLater.Checked == false ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, null, null, null, out _buybackno);
                    effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, (chkDeliverLater.Checked == false) ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, _giftVoucher, null, null, null, out _buybackno, ref IsInvoiceCompleted);
                    //2013/03/29
                    //save passport info
                    int line = 0;
                    if (effect != -1)
                    {
                        foreach (DataRow dr in PassportTable.Rows)
                        {

                            CustomerPassoprt _pass = new CustomerPassoprt();
                            _pass.Sdcd_flight = dr[0].ToString();
                            _pass.Sdcd_invc_no = _invoiceNo;
                            line = line + 1;
                            _pass.Sdcd_line = line;
                            _pass.Sdcd_cre_by = BaseCls.GlbUserID;
                            _pass.Sdcd_cre_dt = DateTime.Now;
                            _pass.Sdcd_ref = dr[1].ToString();

                            CHNLSVC.Sales.SaveCustomerPassportNums(_pass);
                        }
                        //save customer passport
                        CustomerPassoprt _cusPass = new CustomerPassoprt();

                        _cusPass.Sdcd_invc_no = _invoiceNo;
                        line = line + 1;
                        _cusPass.Sdcd_line = line;
                        _cusPass.Sdcd_cre_by = BaseCls.GlbUserID;
                        _cusPass.Sdcd_cre_dt = DateTime.Now;
                        _cusPass.Sdcd_ref = txtPPNo.Text;
                        _cusPass.Sdcd_flight = txtCusFlight.Text;

                        CHNLSVC.Sales.SaveCustomerPassportNums(_cusPass);

                        if (!string.IsNullOrEmpty(txtMobile.Text))
                        {
                            CustomerPassoprt _cusPass1 = new CustomerPassoprt();

                            _cusPass1.Sdcd_invc_no = _invoiceNo;
                            line = line + 1;
                            _cusPass1.Sdcd_line = line;
                            _cusPass1.Sdcd_cre_by = BaseCls.GlbUserID;
                            _cusPass1.Sdcd_cre_dt = DateTime.Now;
                            _cusPass1.Sdcd_ref = txtMobile.Text;
                            _cusPass1.Sdcd_flight = txtCusFlight.Text;

                            CHNLSVC.Sales.SaveCustomerPassportNums(_cusPass1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error occured processing");
                    }

                    if (!string.IsNullOrEmpty(_error))
                    {
                        effect = -1;
                        //MessageBox.Show("following item/serial does not having inventory balance." + _error, "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel();
                    return;
                }
                finally
                {

                    string Msg = string.Empty;
                    btnSave.Enabled = true;


                    if (effect != -1)
                    {
                        if (chkDeliverLater.Checked == false)
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". ";
                        }
                        else
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                        }
                        /*
                        if (cmbInvType.Text.Trim() == "CS")
                        {
                            var _isCashPaymentExsit = _recieptItem.Where(x => x.Sard_pay_tp == "CASH").ToList();
                            if (_isCashPaymentExsit != null)
                                if (_isCashPaymentExsit.Count > 0)
                                {
                                    decimal _cashamt = _isCashPaymentExsit.Sum(x => x.Sard_settle_amt);
                                    string _customerGiven = PaymentBalanceConfirmation(Msg, _cashamt);
                                    if (!string.IsNullOrEmpty(_customerGiven))
                                    {
                                        string BalanceToGive = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_customerGiven) - _cashamt));
                                        MessageBox.Show("You have to give back as balance " + BalanceToGive + "\n in " + lblCurrency.Text + ".", "Balance To Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                        }
                        else
                        {
                            MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                         */
                        MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        #region Printing
                        if (chkManualRef.Checked == false)
                        {
                            MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                            ReportViewer _view = new ReportViewer();

                            if (BaseCls.GlbUserComCode == "SGL")
                            {
                                BaseCls.GlbReportName = "sInvoiceDutyFree.rpt";//Add by Chamal 04-Mar-2014
                            }
                            else
                            {
                                if (BaseCls.GlbUserComCode == "EDL" | BaseCls.GlbUserComCode == "JAC")
                                {
                                    BaseCls.GlbReportName = "InvoiceDutyFreeEdison.rpt";
                                }
                                else
                                {
                                    BaseCls.GlbReportName = "InvoiceDutyFree.rpt";
                                }
                            }
                            _view.GlbReportDoc = _invoiceNo;

                            _view.Show();
                            _view = null;
                            //if (_itm.Mbe_sub_tp != "C.")
                            //{

                            //ReportViewer _view = new ReportViewer();
                            //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                            //_view.GlbReportDoc = _invoiceNo;
                            //_view.Show();
                            //_view = null;



                            //}
                            // else
                            // {
                            ////Dealer
                            //ReportViewer _view = new ReportViewer();
                            //_view.GlbReportName = "InvoicePrints.rpt";
                            //_view.GlbReportDoc = _invoiceNo;
                            //_view.Show();
                            //_view = null;

                            //if (_recieptItem != null)
                            //    if (_recieptItem.Count > 0)
                            //        if (_itm.Mbe_cate == "LEASE")
                            //        {
                            //            ReportViewer _viewt = new ReportViewer();
                            //            _viewt.GlbReportName = "InvoicePrint_insus.rpt";
                            //            _viewt.GlbReportDoc = _invoiceNo;
                            //            _viewt.Show();
                            //            _viewt = null;
                            //        }
                            //  }

                            //=========================DO
                            if (chkDeliverLater.Checked == false)
                            {
                                ReportViewerInventory _views = new ReportViewerInventory();
                                BaseCls.GlbReportTp = "OUTWARD";
                                _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SOutward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                                _views.GlbReportDoc = _deliveryOrderNo;
                                _views.Show();
                                _views = null;
                            }

                        }
                        #endregion

                        btnClear_Click(null, null);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_error))
                            MessageBox.Show("Generating Invoice is terminated due to following reason, " + _error, "Generating Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        CHNLSVC.CloseChannel();
                    }
                }
            }
            catch (Exception)
            {
                CHNLSVC.CloseChannel();
            }

        }
        private string PaymentBalanceConfirmation(string Msg, decimal _cashamount)
        {
            string _cashGiven = Microsoft.VisualBasic.Interaction.InputBox(Msg + "\nPlease enter customer tender amount.", "Balance", FormatToCurrency(Convert.ToString(_cashamount)), -1, -1);
            if (!string.IsNullOrEmpty(_cashGiven))
            {
                if (IsNumeric(_cashGiven) == false)
                {
                    Msg = "Invalid amount. ";
                    PaymentBalanceConfirmation(Msg, _cashamount);
                }

                if (Convert.ToDecimal(_cashGiven) < _cashamount)
                {
                    Msg = "Invalid amount. ";
                    PaymentBalanceConfirmation(Msg, _cashamount);
                }
            }

            return _cashGiven;
        }
        #endregion

        #region  Rooting for Hold invoice
        private void Hold()
        {
            if (IsBackDateOk(false, true) == false) return;

            if (chkDeliverLater.Checked == false)
            {
                MessageBox.Show("Deliver Now is not allow for holding an invoice", "Hold Invoice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomer.Focus();
                return;
            }


            if (_invoiceItemList.Count <= 0)
            {
                MessageBox.Show("Please select the items for invoice", "Invoice Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (string.IsNullOrEmpty(txtExecutive.Text))
            {
                MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExecutive.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lblCurrency.Text))
            {
                MessageBox.Show("Please select the currency code", "Currency", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCurrency.Focus();
                return;
            }

            if (_recieptItem.Count > 0)
            {
                MessageBox.Show("Please remove the payment details.", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
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
                    MessageBox.Show("Hold invoice can only re-hold with in the date" + _hdr.Sah_dt.Date.ToShortDateString(), "Holding...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (_hdr.Sah_stus != "H")
                {
                    MessageBox.Show("You can not hold the invoice which already " + _hdr.Sah_stus == "C" ? "canceled." : _hdr.Sah_stus == "A" ? "approved." : _hdr.Sah_stus == "D" ? "delivered." : ".", "Hold Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }



            #region Invoice Header Value Assign
            _invheader.Sah_com = BaseCls.GlbUserComCode;
            _invheader.Sah_cre_by = BaseCls.GlbUserID;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = lblCurrency.Text;
            _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
            _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();
            _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
            _invheader.Sah_cus_name = txtCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
            _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtDelName.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text).Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
            MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            MasterCompany _com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            decimal _exchangRate = 1;
            MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, _pc.Mpc_def_exrate, DateTime.Now, _com.Mc_cur_cd, BaseCls.GlbUserDefProf);
            if (_exc1 != null)
            {
                _exchangRate = _exc1.Mer_bnkbuy_rt;
            }
            _invheader.Sah_ex_rt = _exchangRate;
            _invheader.Sah_inv_no = _hdr.Sah_pc != null ? Convert.ToString(_hdr.Sah_seq_no) : Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = "";
            _invheader.Sah_man_ref = txtManualRefNo.Text;
            _invheader.Sah_manual = chkManualRef.Checked ? true : false;
            _invheader.Sah_mod_by = BaseCls.GlbUserID;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = BaseCls.GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtManualRefNo.Text;
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = Convert.ToInt32(_invheader.Sah_inv_no);
            _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "H";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_direct = true;
            _invheader.Sah_anal_1 = BaseCls.GlbUserDefLoca;
            _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
            _invheader.Sah_anal_7 = chkDeliverLater.Checked ? 0 : 1;

            _invheader.Sah_del_loc = txtDelLocation.Text;
            _invheader.Sah_grn_com = _customerCompany;
            _invheader.Sah_grn_loc = _customerLocation;
            _invheader.Sah_is_grn = _isCustomerHasCompany;

            _invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            _invheader.Sah_remarks = txtRemarks.Text.Trim();


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
                string _buybackno = string.Empty;
                int effect = CHNLSVC.Sales.SaveInvoice(_invheader, _invoiceItemList, null, _recHeader, _recieptItem, null, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, true, false, out _error, null, null, null, out _buybackno);
                if (string.IsNullOrEmpty(_error))
                {
                    btnHold.Enabled = true;
                    string Msg = "Successfully Saved! Document No : " + _invoiceNo + ".";
                    MessageBox.Show(Msg, "Hold", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("please check the following item/serials for inventory balance; " + _error, "Qty and Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                btnHold.Enabled = true;

            }


        }
        private void btnHold_Click(object sender, EventArgs e)
        {
            if (pnlMain.Enabled == false) return;
            if (CheckServerDateTime() == false) return;
            if (MessageBox.Show("Do you want to hold?", "Holding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Hold();
        }

        #endregion

        #region Rooting for Cancel Invoice
        private void Cancel()
        {
            if (IsBackDateOk(true, false) == false) return;

            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the invoice no", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoiceNo.Focus();
                return;
            }

            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, txtInvoiceNo.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());
            if (_header.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Selected invoice no already canceled or invalid.", "Invalid Invoice no", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Int32 _isRegistered = CHNLSVC.Sales.CheckforInvoiceRegistration(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text.Trim());
            if (_isRegistered != 1)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("This invoice already registered!. You are not allow for cancelation.", "Registration Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Int32 _isInsured = CHNLSVC.Sales.CheckforInvoiceInsurance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text.Trim());
            if (_isInsured != 1)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("This invoice already insured!. You are not allow for cancelation.", "Insurance Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataTable _buybackdoc = CHNLSVC.Inventory.GetBuyBackInventoryDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNo.Text.Trim());
                if (_buybackdoc != null)
                    if (_buybackdoc.Rows.Count > 0)
                    {
                        string _adjno = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_doc_no"));
                        string _buybackloc = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_loc"));

                        if (!string.IsNullOrEmpty(_adjno))
                        {
                            _header[0].Sah_del_loc = _buybackloc;
                            DataTable _referdoc = CHNLSVC.Inventory.CheckInwardDocumentUseStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _adjno);
                            if (_referdoc != null)
                                if (_referdoc.Rows.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    string _referno = Convert.ToString(_referdoc.Rows[0].Field<string>("ith_doc_no"));
                                    MessageBox.Show("The invoice having buy back return item which already out from the location refer document " + _referno + ", buy back inventory no " + _adjno, "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            List<InventoryHeader> _cancelDocument = null;

            try
            {
                DataTable _consignDocument = CHNLSVC.Inventory.GetConsginmentDocumentByInvoice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNo.Text.Trim());
                if (_consignDocument != null)
                    if (_consignDocument.Rows.Count > 0)
                    {
                        foreach (DataRow _r in _consignDocument.Rows)
                        {
                            InventoryHeader _one = new InventoryHeader();
                            if (_cancelDocument == null) _cancelDocument = new List<InventoryHeader>();
                            string _type = _r["ith_doc_tp"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_tp"]);
                            string _document = _r["ith_doc_no"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_no"]);
                            bool _direction = _r["ith_direct"] == DBNull.Value ? false : Convert.ToBoolean(_r["ith_direct"]);

                            _one.Ith_doc_no = _document;
                            _one.Ith_doc_tp = _type;
                            _one.Ith_direct = _direction;
                            _cancelDocument.Add(_one);
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                string _msg = "";
                Int32 _effect = CHNLSVC.Sales.InvoiceCancelation(_header[0], out _msg, _cancelDocument);
                this.Cursor = Cursors.Default;
                string Msg = "Successfully Canceled!";
                this.Cursor = Cursors.Default;
                MessageBox.Show(Msg, "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (pnlMain.Enabled == false) return;
            if (CheckServerDateTime() == false) return;
            if (MessageBox.Show("Do you want to cancel?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            RecallInvoice();
            Cancel();
        }
        #endregion

        #region Rooting for Invoice Item Additionals
        private void cmsInvoiceItem_Description_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_Description.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_Description"].Visible = true; else gvInvoiceItem.Columns["InvItm_Description"].Visible = false;
        }

        private void cmsInvoiceItem_UnitAmt_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_UnitAmt.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_UnitAmt"].Visible = true; else gvInvoiceItem.Columns["InvItm_UnitAmt"].Visible = false;
        }

        private void cmsInvoiceItem_DisRate_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_DisRate.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_DisRate"].Visible = true; else gvInvoiceItem.Columns["InvItm_DisRate"].Visible = false;
        }

        private void cmsInvoiceItem_DisAmt_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_DisAmt.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_DisAmt"].Visible = true; else gvInvoiceItem.Columns["InvItm_DisAmt"].Visible = false;
        }

        private void cmsInvoiceItem_TaxAmt_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_TaxAmt.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_TaxAmt"].Visible = true; else gvInvoiceItem.Columns["InvItm_TaxAmt"].Visible = false;
        }

        private void cmsInvoiceItem_Book_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_Book.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_Book"].Visible = true; else gvInvoiceItem.Columns["InvItm_Book"].Visible = false;
        }

        private void cmsInvoiceItem_Level_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_Level.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_Level"].Visible = true; else gvInvoiceItem.Columns["InvItm_Level"].Visible = false;
        }

        private void cmsInvoiceItem_WarrantyPeriod_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_WarrantyPeriod.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_WarraPeriod"].Visible = true; else gvInvoiceItem.Columns["InvItm_WarraPeriod"].Visible = false;
        }

        private void cmsInvoiceItem_WarrantyRemarks_Click(object sender, EventArgs e)
        {
            if (cmsInvoiceItem_WarrantyRemarks.CheckState == CheckState.Checked) gvInvoiceItem.Columns["InvItm_WarraRemarks"].Visible = true; else gvInvoiceItem.Columns["InvItm_WarraRemarks"].Visible = false;
        }

        private void gvPopSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPopSerial.ColumnCount > 0)
            {
                Int32 _rowIndex = e.RowIndex;
                Int32 _colIndex = e.ColumnIndex;
                if (_rowIndex != -1)
                {

                    if (_colIndex == 0)
                    {

                        if (_recieptItem != null)
                            if (_recieptItem.Count > 0) { MessageBox.Show("You are already payment added!", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                        if (ScanSerialList != null)
                            if (ScanSerialList.Count > 0)
                            {
                                int row_id = e.RowIndex;
                                string _item = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Item"].Value);
                                string _comline = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_SerialID"].Value);
                                Int32 _combineLine; if (string.IsNullOrEmpty(_comline)) _combineLine = -1; else _combineLine = Convert.ToInt32(gvPopSerial.Rows[_rowIndex].Cells["popSer_SerialID"].Value);
                                decimal uPrice = Convert.ToDecimal(gvPopSerial.Rows[_rowIndex].Cells["popSer_UnitPrice"].Value);
                                Int32 _invLine = Convert.ToInt32(gvPopSerial.Rows[_rowIndex].Cells["popSer_BaseItemLine"].Value);
                                string _combineStatus = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Status"].Value);
                                string _serialno = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Serial1"].Value);


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

                                gvPopSerial.DataSource = new List<ReptPickSerials>();
                                gvInvoiceItem.DataSource = new List<InvoiceItem>();

                                if (ScanSerialList != null)
                                    if (ScanSerialList.Count > 0)
                                    {
                                        gvPopSerial.DataSource = ScanSerialList;//.Where(X => X.Tus_ser_1 != "N/A").ToList(); ;
                                        gvInvoiceItem.DataSource = _invoiceItemList;
                                    }

                            }
                    }
                }
            }
        }
        #endregion

        #region Rooting for Clear Screen

        private void ClearTop1p0()
        {
            txtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            cmbInvType.Text = string.Empty;
            txtDocRefNo.Clear();
            txtInvoiceNo.Clear();
            lblCurrency.Text = "LKR - Sri Lankan Rupees";
            btnCustomer.Enabled = true;
        }

        private void ClearTop2p0()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtCustomer.Clear();
            txtNIC.Clear();
            // txtMobile.Clear();
            txtCusName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtPPNo.Clear();
            txtMobile.Clear();
            txtFlight.Clear();
            txtPassport.Clear();
            dateTimePickerDateOfBirth.Value = _date;
        }

        private void ClearTop2p1()
        {
            chkTaxPayable.Checked = false;
            lblSVatStatus.Text = string.Empty;
            lblVatExemptStatus.Text = string.Empty;
        }

        private void ClearTop2p2()
        {
            lblAccountBalance.Text = FormatToCurrency("0");
            lblAvailableCredit.Text = FormatToCurrency("0");
        }

        private void ClearRight1p0()
        {
            txtExecutive.Clear();
            //lblExecutiveName.Text = string.Empty;
            txtManualRefNo.Clear();
            chkManualRef.Checked = false;
        }

        private void ClearMiddle1p0()
        {
            txtSerialNo.Clear();
            txtItem.Clear();
            cmbBook.Text = string.Empty;
            cmbLevel.Text = string.Empty;
            cmbStatus.Text = string.Empty;
            txtQty.Text = FormatToQty("1");
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            gvInvoiceItem.DataSource = new List<InvoiceItem>();

        }

        private void ClearRight1p1()
        {
            lblGrndSubTotal.Text = FormatToCurrency("0");
            lblGrndDiscount.Text = FormatToCurrency("0");
            lblGrndAfterDiscount.Text = FormatToCurrency("0");
            lblGrndTax.Text = FormatToCurrency("0");
            lblGrndTotalAmount.Text = FormatToCurrency("0");
        }

        private void ClearPayMode()
        {
            ucPayModes1.ClearControls();
        }
        private void ClearConsumablePanle()
        {
            gvPopConsumPricePick.DataSource = new List<InventoryBatchRefN>();
        }
        private void ClearDeliveryInstructionPanel()
        {
            txtDelLocation.Clear();
            chkOpenDelivery.Checked = false;
            txtDelCustomer.Clear();
            txtDelName.Clear();
            txtDelAddress1.Clear();
            txtDelAddress2.Clear();
        }
        private void ClearInventoryCombineSerialPickPanel()
        {
            gvPopComItem.DataSource = new List<MasterItemComponent>();
            gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
            txtInvComSerSearch.Clear();
        }
        private void ClearMultiCombinePanel()
        {
            gvMultiCombineItem.DataSource = new DataTable();
        }
        private void ClearMultiItemPanel()
        {
            gvMultipleItem.DataSource = new DataTable();
        }
        private void ClearPriceNPromotionPanel()
        {
            gvNormalPrice.DataSource = new List<PriceDetailRef>();
            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
            txtPriNProSerialSearch.Clear();
            lblPriNProAvailableQty.Text = FormatToQty("0");
            lblPriNProAvailableStatusQty.Text = FormatToQty("0");
        }

        private void HideConsumerPricePanel()
        {
            pnlConsumerPrice.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideDeliveryInstructionPanel()
        {
            pnlDeliveryInstruction.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideInventoryCombineSerialPickPanel()
        {
            pnlInventoryCombineSerialPick.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideMultiCombinePanel()
        {
            pnlMultiCombine.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideMultipleItemPanel()
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HidePriceNPromotionPanel()
        {
            pnlPriceNPromotion.Visible = false;
            pnlMain.Enabled = true;
        }

        private void ClearVariable()
        {
            btnSave.Enabled = true;
            txtInvoiceNo.Enabled = true;

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
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            MainTaxConstant = new List<MasterItemTax>();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();

            _lineNo = 1;
            //_toBePayNewAmount = 0;
            _isEditPrice = false;
            _isEditDiscount = false;

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;

            //_paidAmount = 0;
            SSCombineLine = 1;

            _isCompleteCode = false;
            PassportTable = new DataTable();
            CreatePassportTable();
            grvFlight.DataSource = null;
            GeneralDiscount = null;
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadItemDetail(string.Empty);
            ClearVariable();
            ClearTop1p0();
            ClearTop2p0();
            ClearTop2p1();
            ClearTop2p2(); ClearRight1p0(); ClearMiddle1p0(); ClearRight1p1(); ClearPayMode(); ClearConsumablePanle(); ClearDeliveryInstructionPanel();
            ClearInventoryCombineSerialPickPanel(); ClearMultiCombinePanel(); ClearMultiItemPanel(); ClearPriceNPromotionPanel();
            HideConsumerPricePanel(); HideDeliveryInstructionPanel(); HideInventoryCombineSerialPickPanel(); HideMultiCombinePanel(); HideMultipleItemPanel(); HidePriceNPromotionPanel();
            InitializeValuesNDefaultValueSet();
            ucPayModes1.Amount.Enabled = false;
            ucPayModes1.AddButton.Visible = false;
            txtDate.Value = CHNLSVC.Security.GetServerDateTime().AddHours(_MasterProfitCenter.Mpc_add_hours);
            txtCusFlight.Text = "";
            txtNationality.Text = "SL";
            LoadCurrancyCodes();
            txtRemarks.Text = "";
            BackDatePermission();
            IsInvoiceCompleted = false;
        }
        #endregion

        #region Rooting for Re-Print Invoice
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            if (_itm.Mbe_sub_tp != "C.")
            {
                //Showroom
                //========================= INVOCIE  CASH/CREDIT/ HIRE 
                ReportViewer _view = new ReportViewer();
                BaseCls.GlbReportTp = "INV";
                _view.GlbReportName = "InvoiceHalfPrints.rpt";
                _view.GlbReportDoc = txtInvoiceNo.Text;
                _view.Show();
                _view = null;
            }
            else
            {
                //Dealer
                ReportViewer _view = new ReportViewer();
                _view.GlbReportName = "InvoicePrints.rpt";
                _view.GlbReportDoc = txtInvoiceNo.Text;
                _view.Show();
                _view = null;

                List<FF.BusinessObjects.RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
                if (_itms != null)
                    if (_itms.Count > 0)
                        if (_itm.Mbe_cate == "LEASE")
                        {
                            ReportViewer _viewt = new ReportViewer();
                            _viewt.GlbReportName = "InvoicePrint_insus.rpt";
                            _viewt.GlbReportDoc = txtInvoiceNo.Text;
                            _viewt.Show();
                            _viewt = null;

                        }
            }

        }
        #endregion

        #region Rooting for the Payment Event
        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            _recieptItem = ucPayModes1.RecieptItemList;
            decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
            if (_totlaPay == Convert.ToDecimal(lblGrndTotalAmount.Text))
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
            cmbCurrancy_SelectedIndexChanged(null, null);
        }
        #endregion

        #region Rooting for the Address Events
        private void txtAddress_GotFocus(object sender, EventArgs e)
        {
            TextBox _box = (TextBox)(sender);
            _box.SelectionStart = _box.Text.Length;
        }
        private void txtAddress_LostFocus(object sender, EventArgs e)
        {
            TextBox _box = (TextBox)(sender);
            _box.SelectionStart = 0;
        }
        #endregion

        #region Rooting for Apply Discount & Get Confirmation for the payment
        private List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount;
        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {
            pnlDiscountRequest.Visible = false;
        }
        protected void BindGeneralDiscount()
        {
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
        }
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (pnlDiscountRequest.Visible)
            {
                pnlDiscountRequest.Visible = false;
                return;
            }
            else
                pnlDiscountRequest.Visible = true;

            BindGeneralDiscount();
            ddlDisCategory.Text = "Customer";

            if (string.IsNullOrEmpty(txtCustomer.Text))
            { MessageBox.Show("Please select the customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (txtCustomer.Text == "CASH")
            { MessageBox.Show("Please select the valid customer. Customer should be registered.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (_invoiceItemList != null)
                if (_invoiceItemList.Count > 0)
                {
                    ddlDisCategory.Enabled = true;
                }
                else
                { ddlDisCategory.Text = "Customer"; ddlDisCategory.Enabled = false; }
            else { ddlDisCategory.Text = "Customer"; ddlDisCategory.Enabled = false; }


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
                }
        }
        protected void Category_onChange(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDisCategory.Text)) return;
            if (ddlDisCategory.Text.ToString() == "Customer")
            {
                gvDisItem.Enabled = false;
                txtDisAmount.Enabled = true;
            }
            else
            {
                gvDisItem.Enabled = true;
                txtDisAmount.Enabled = false;
            }
        }
        protected void SaveDiscountRequest(object sender, EventArgs e)
        {

            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact costing dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_infor.Count <= 0)
            {
                MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact costing dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlDisCategory.Text == "Customer")
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    MessageBox.Show("Please select the discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    MessageBox.Show("Please select the valid discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    MessageBox.Show("Discount rate can not exceed the 100%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    MessageBox.Show("Discount rate can not exceed the 0%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            if (ddlDisCategory.Text == "Item")
            {

                if (gvDisItem.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    foreach (DataGridViewRow _r in gvDisItem.Rows)
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        MessageBox.Show("Please select the item which you need to request", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

            }

            string _customer = txtCustomer.Text;
            string _customerReq = _customer + "REQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;

            if (gvDisItem.Rows.Count > 0)
            {

                foreach (DataGridViewRow _r in gvDisItem.Rows)
                {
                    DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                    if (Convert.ToBoolean(_chk.Value) == true)
                    {
                        string _item = Convert.ToString(_r.Cells["DisItem_Item"].Value); //item code
                        DataGridViewComboBoxCell _type = (DataGridViewComboBoxCell)_r.Cells["DisItem_Type"];
                        DataGridViewTextBoxCell _amt = (DataGridViewTextBoxCell)_r.Cells["DisItem_Amount"];
                        string _pricebook = Convert.ToString(_r.Cells["DisItem_Book"].Value);
                        string _pricelvl = Convert.ToString(_r.Cells["DisItem_Level"].Value);

                        if (string.IsNullOrEmpty(Convert.ToString(_amt.Value).Trim()))
                        {
                            MessageBox.Show("Please select the amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(_amt.Value).Trim()))
                        {
                            MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) <= 0)
                        {
                            MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) > 100 && _type.Value.ToString().Contains("Rate"))
                        {
                            MessageBox.Show("Rate can not be exceed the 100% in " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }

                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = BaseCls.GlbUserComCode;
                        _discount.Sgdd_cre_by = BaseCls.GlbUserID;
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;

                        if (_type.Value.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(_amt.Value.ToString().Trim());
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(_amt.Value.ToString().Trim());

                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtDate.Text).Date;
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtDate.Text).Date;
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = BaseCls.GlbUserComCode;
                _discount.Sgdd_cre_by = BaseCls.GlbUserID;
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisAmount.Text.Trim());
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtDate.Text).Date;
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = string.Empty;
                _discount.Sgdd_pb_lvl = string.Empty;
                _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtDate.Text).Date;
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customerReq, BaseCls.GlbUserID, _list, txtCustomer.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                string Msg = string.Empty;
                if (_effect > 0)
                {
                    Msg = "Successfully Saved! Document No : " + _customerReq + ".";
                }
                else
                {
                    Msg = "Document not processed! please try again.";
                }
                MessageBox.Show(Msg, "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        #region Panel Movement
        Point disReqPoint = new Point();
        private void pnlDiscountRequest_MouseDown(object sender, MouseEventArgs e)
        {
            disReqPoint.X = e.X;
            disReqPoint.Y = e.Y;

        }

        private void pnlDiscountRequest_MouseUp(object sender, MouseEventArgs e)
        {
            pnlDiscountRequest.Location = new Point(e.X - disReqPoint.X + pnlDiscountRequest.Location.X, e.Y - disReqPoint.Y + pnlDiscountRequest.Location.Y);
        }
        #endregion

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            General.CustomerCreation _CusCre = new General.CustomerCreation();
            _CusCre._isFromOther = true;
            _CusCre.obj_TragetTextBox = txtCustomer;
            _CusCre.ShowDialog();
            txtCustomer.Select();
            if (chkDeliverLater.Checked) txtItem.Focus(); else txtSerialNo.Focus();
        }
        #endregion

        #region Rooting for Dispose
        private void Invoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            _invoiceItemList = null;
            _invoiceItemListWithDiscount = null;
            _recieptItem = null;
            _newRecieptItem = null;
            _businessEntity = null;
            _masterItemComponent = null;
            _priceBookLevelRef = null;
            _priceBookLevelRefList = null;
            _priceDetailRef = null;
            _masterBusinessCompany = null;
            _MainPriceSerial = null;
            _tempPriceSerial = null;
            _MainPriceCombinItem = null;
            _tempPriceCombinItem = null;
            ScanSerialList = null;
            InvoiceSerialList = null;
            InventoryCombinItemSerialList = null;
            PriceCombinItemSerialList = null;
            ManagerDiscount = null;
            _itemdetail = null;
            MainTaxConstant = null;
            _promotionSerial = null;
            _promotionSerialTemp = null;
            _MasterProfitCenter = null;
            _PriceDefinitionRef = null;
        }
        #endregion

        #region Rooting for Change Invoice Type
        private void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInvType.Text == "CRED")
                btnCustomer.Enabled = false;
            else
                btnCustomer.Enabled = true;
        }
        #endregion

        private void btnPayAdd_Click(object sender, EventArgs e)
        {
            //check value
            try
            {
                decimal val;
                if (!decimal.TryParse(txtCurrancyValue.Text, out val))
                {
                    MessageBox.Show("Pay Amount has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), txtDate.Value, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                else
                {
                    MessageBox.Show("Exchange Rate not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                ucPayModes1.Amount.Enabled = false;
                ucPayModes1.Amount.Text = Math.Round((Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate), 4).ToString();

                //if (Convert.ToDecimal(FormatToCurrency((Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate).ToString())) > ucPayModes1.TotalAmount)
                //{
                //    MessageBox.Show("You can not pay more than total amount\nTotal Amount you can pay is " + cmbCurrancy.SelectedValue.ToString() + " " + Math.Round((ucPayModes1.Balance / _exchangRate), 2), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //else

                ucPayModes1.ExchangeRate = _exchangRate;
                ucPayModes1.CurrancyAmount = Convert.ToDecimal(txtCurrancyValue.Text);
                ucPayModes1.CurrancyCode = cmbCurrancy.SelectedValue.ToString();
                ucPayModes1.button1_Click(null, null);
                txtCurrancyValue.Text = "";
                lblLocalCurValue.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtCurrancyValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnCloseFlight_Click(object sender, EventArgs e)
        {
            pnlFlight.Visible = false;
            pnlMain.Enabled = true;
            txtFlight.Text = "";
            txtPassport.Text = "";
        }

        private void btnAddFlight_Click(object sender, EventArgs e)
        {
            if (txtFlight.Text == "")
            {
                MessageBox.Show("Please enter Flight no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPassport.Text == "")
            {
                MessageBox.Show("Please enter Passport no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DataRow> _tem = (from _res in PassportTable.AsEnumerable()
                                  where _res.Field<string>("PassportNo") == txtPassport.Text
                                  select _res).ToList<DataRow>();

            if (_tem != null && _tem.Count > 0)
            {
                MessageBox.Show("Given Passport already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow dr = PassportTable.NewRow();
            dr[0] = txtFlight.Text;
            dr[1] = txtPassport.Text;
            PassportTable.Rows.Add(dr);
            txtPassport.Text = "";
            txtFlight.Text = "";

            grvFlight.DataSource = PassportTable;
        }

        private void grvFlight_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (PassportTable.Rows.Count > 0)
                    {
                        PassportTable.Rows.RemoveAt(e.RowIndex);
                    }
                    grvFlight.DataSource = PassportTable;
                }
            }
        }

        private void txtCurrancyValue_Leave(object sender, EventArgs e)
        {
            if (txtCurrancyValue.Text != "")
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                    lblLocalCurValue.Text = (Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate).ToString();
                }
            }
        }

        private void cmbCurrancy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCurrancyValue.Focus();
            }
        }

        private void txtCurrancyValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayAdd.Focus();
            }
        }

        private void txtFlight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassport.Focus();
            }
        }

        private void txtPassport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddFlight.Focus();
            }
        }

        private string CreateCustomer()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            CustomerAccountRef _account = new CustomerAccountRef();
            List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();

            //Add Chamal 01-May-2014
            _custProfile.Mbe_act = true;
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID.ToString();
            //
            string _cusCode;
            int _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode, null);
            if (_effect > 0)
            {
                txtCusName.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtPPNo.Text = "";
                txtMobile.Text = "";
                txtFlight.Text = "";
                txtPassport.Text = "";
                //txtCusFlight.Text = "";
                //txtNationality.Text = "";
                dateTimePickerDateOfBirth.Value = _date;
                return _cusCode;
            }
            else
                return "";
        }



        private void Collect_Cust()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;

            _custProfile.Mbe_name = txtCusName.Text;
            _custProfile.Mbe_add1 = txtAddress1.Text;
            _custProfile.Mbe_add2 = txtAddress2.Text;
            _custProfile.Mbe_pp_no = txtPPNo.Text;
            _custProfile.Mbe_dob = dateTimePickerDateOfBirth.Value;
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_nic = txtNIC.Text;
            // _custProfile.Mbe_mob = txtMobile.Text;
            if (cmbCountry.SelectedItem != null)
                _custProfile.Mbe_country_cd = cmbCountry.SelectedValue.ToString();
            //_custProfile.Mbe_nationality = txtNationality.Text;


            //_custProfile = new MasterBusinessEntity();
            //_custProfile.Mbe_acc_cd = null;
            //_custProfile.Mbe_act = true;
            //_custProfile.Mbe_tel = txtPerPhone.Text.ToUpper();
            //_custProfile.Mbe_add1 = txtPerAdd1.Text.Trim().ToUpper();
            //_custProfile.Mbe_add2 = txtPerAdd2.Text.Trim().ToUpper();
            //_custProfile.Mbe_town_cd = txtPerTown.Text.ToUpper();
            //_custProfile.Mbe_country_cd = txtPerCountry.Text;
            //_custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            //_custProfile.Mbe_province_cd = txtPerProvince.Text;
            //if (chkSMS.Checked == true)
            //{
            //    _isSMS = true;
            //}
            //else
            //{
            //    _isSMS = false;
            //}
            //_custProfile.Mbe_agre_send_sms = _isSMS;
            //_custProfile.Mbe_br_no = txtBR.Text.Trim();
            //_custProfile.Mbe_cate = cmbType.Text;
            //_custProfile.Mbe_com = BaseCls.GlbUserComCode;
            //_custProfile.Mbe_contact = null;
            //_custProfile.Mbe_country_cd = null;
            //_custProfile.Mbe_cr_add1 = txtPreAdd1.Text.Trim();
            //_custProfile.Mbe_cr_add2 = txtPreAdd2.Text.Trim();
            //_custProfile.Mbe_cr_country_cd = txtPreCountry.Text.Trim();
            //_custProfile.Mbe_cr_distric_cd = txtPreDistrict.Text;
            //_custProfile.Mbe_cr_email = null;
            //_custProfile.Mbe_cr_fax = null;
            //_custProfile.Mbe_cr_postal_cd = txtPrePostal.Text.Trim().ToUpper();
            //_custProfile.Mbe_cr_province_cd = txtPreProvince.Text.Trim();
            //_custProfile.Mbe_cr_tel = txtPrePhone.Text.Trim().ToUpper();
            //_custProfile.Mbe_cr_town_cd = txtPreTown.Text.Trim().ToUpper();
            //_custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            //_custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            //_custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            //_custProfile.Mbe_cust_com = BaseCls.GlbUserComCode.ToUpper();
            //_custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca.ToUpper();
            //_custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            //_custProfile.Mbe_dl_no = txtDL.Text.Trim().ToUpper();
            //_custProfile.Mbe_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            //_custProfile.Mbe_email = txtPerEmail.Text.Trim();
            //_custProfile.Mbe_fax = null;
            //_custProfile.Mbe_ho_stus = "GOOD";
            //_custProfile.Mbe_income_grup = null;
            //_custProfile.Mbe_intr_com = false;
            //_custProfile.Mbe_is_suspend = false;
            //_custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            //_custProfile.Mbe_cust_loc = BaseCls.GlbUserDefProf;
            //if (chkSVAT.Checked == true)
            //{
            //    _isSVAT = true;
            //}
            //else
            //{
            //    _isSVAT = false;
            //}

            //_custProfile.Mbe_is_svat = _isSVAT;

            //if (chkVAT.Checked == true)
            //{
            //    _isVAT = true;
            //}
            //else
            //{
            //    _isVAT = false;
            //}
            //_custProfile.Mbe_is_tax = _isVAT;
            //_custProfile.Mbe_mob = txtMob.Text.Trim();
            //_custProfile.Mbe_name = txtName.Text.Trim().ToUpper();
            //_custProfile.Mbe_nic = txtNIC.Text.Trim().ToUpper();
            //_custProfile.Mbe_oth_id_no = null;
            //_custProfile.Mbe_oth_id_tp = null;
            //_custProfile.Mbe_pc_stus = "GOOD";
            //_custProfile.Mbe_postal_cd = txtPerPostal.Text.Trim().ToUpper();
            //_custProfile.Mbe_pp_no = txtPP.Text.Trim();
            //_custProfile.Mbe_province_cd = txtPerProvince.Text.Trim().ToUpper();
            //_custProfile.Mbe_sex = cmbSex.Text;
            //_custProfile.Mbe_sub_tp = null;
            //_custProfile.Mbe_svat_no = txtSVATReg.Text.Trim().ToUpper();

            //if (chkVatEx.Checked == true)
            //{
            //    _TaxEx = true;
            //}
            //else
            //{
            //    _TaxEx = false;
            //}
            //_custProfile.Mbe_tax_ex = _TaxEx;
            //_custProfile.Mbe_tax_no = txtVatreg.Text.Trim();
            //_custProfile.Mbe_tp = "C";
            //_custProfile.Mbe_wr_add1 = txtWorkAdd1.Text.Trim();
            //_custProfile.Mbe_wr_add2 = txtWorkAdd2.Text.Trim();
            //_custProfile.Mbe_wr_com_name = txtWorkName.Text.Trim();
            //_custProfile.Mbe_wr_country_cd = null;
            //_custProfile.Mbe_wr_dept = txtWorkDept.Text.Trim();
            //_custProfile.Mbe_wr_designation = txtWorkDesig.Text.Trim();
            //_custProfile.Mbe_wr_distric_cd = null;
            //_custProfile.Mbe_wr_email = txtWorkEmail.Text.Trim();
            //_custProfile.Mbe_wr_fax = txtWorkFax.Text.Trim();
            //_custProfile.Mbe_wr_proffesion = null;
            //_custProfile.Mbe_wr_province_cd = null;
            //_custProfile.Mbe_wr_tel = txtWorkPhone.Text.Trim();
            //_custProfile.Mbe_wr_town_cd = null;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtCusName.Text == "")
            {
                MessageBox.Show("Customer Name Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtAddress1.Text == "")
            {
                MessageBox.Show("Customer Address Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPPNo.Text == "")
            {
                MessageBox.Show("Customer Passport Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Collect_Cust();
            txtCustomer.Text = CreateCustomer();
            txtItem.Focus();
            LoadCustomerDetailsByCustomer(null, null);
            if (txtCustomer.Text != "")
            {
                MessageBox.Show("Customer created successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtPPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void cmbCurrancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal val;

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                else
                {

                }
                if (_exchangRate > 0)
                    txtCurrancyValue.Text = (Math.Round((ucPayModes1.Balance / _exchangRate), 4).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }


        private void DOBGenarate()
        {
            char[] nicarray = txtNIC.Text.ToCharArray();
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
                dateTimePickerDateOfBirth.Value = dob;
                //dob.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {

            }
        }

        private void DutyFreeInvoice_Load(object sender, EventArgs e)
        {
            BackDatePermission();
        }


        private void txtCusName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCusName.Text)) return; try
            { this.Cursor = Cursors.WaitCursor; if (string.IsNullOrEmpty(txtDelName.Text) || txtDelName.Text.Trim().Length <= 6)                    txtDelName.Text = txtCusName.Text; }
            catch (Exception ex) { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress1.Text)) return; try
            { this.Cursor = Cursors.WaitCursor; if (string.IsNullOrEmpty(txtDelAddress1.Text))                    txtDelAddress1.Text = txtAddress1.Text; }
            catch (Exception ex) { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; }
        }
        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress2.Text)) return; try
            { this.Cursor = Cursors.WaitCursor; if (string.IsNullOrEmpty(txtDelAddress2.Text))                    txtDelAddress2.Text = txtAddress2.Text; }
            catch (Exception ex) { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void dateTimePickerDateOfBirth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCountry.Focus();
            }
        }

        private void cmbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCreate.Focus();
            }
        }

        private void txtCusFlight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNationality.Focus();
            }
        }

        private void txtNationality_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCreate.Focus();
        }

        private void cmbExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {
                txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
            }
        }

        private void cmbTitle_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtCusName.Text))
                    txtCusName.Text = cmbTitle.Text.Trim();
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    if (string.IsNullOrEmpty(_title))
                        txtCusName.Text = cmbTitle.Text.Trim() + txtCusName.Text;
                    else
                    {
                        bool _isExist = cmbTitle.Items.Contains(_title);
                        if (_isExist)
                        {
                            string _currentCustomerName = txtCusName.Text.Trim();
                            txtCusName.Text = _currentCustomerName.Replace(_title.ToUpper(), cmbTitle.Text.Trim().ToUpper());
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void cmbExecutive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCusName.Focus();
        }


        #region Rooting for Go Home and Fly a Kite

        #endregion

    }
}


//private void AddItem(bool _isPromotion)
//{
//    ReptPickSerials _serLst = null;
//    List<ReptPickSerials> _nonserLst = null;
//    MasterItem _itm = null;

//    #region Priority Base Validation
//    if (CheckDiscountAmount() == false)
//    {
//        MessageBox.Show("Please check the discount amount.");
//        return;
//    }
//    if (CheckDiscountRate() == false)
//    {
//        MessageBox.Show("Please check the discount rate.");
//        return;
//    }

//    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")
//        if ((Convert.ToDecimal(lblAvailableCredit.Text) - Convert.ToDecimal(txtLineTotAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCustomer.Text != "CASH")
//        {
//            MessageBox.Show("Please check the account balance");
//            return;
//        }
//    #endregion

//    #region Scan By Serial - check for serial
//    //Scan By Serial ------------------start--------------------------

//    if (string.IsNullOrEmpty(ScanSerialNo))
//    {
//        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
//        if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
//        {
//            MessageBox.Show("Please select the serial no");
//            txtSerialNo.Focus();
//            return;
//        }
//    }

//    //Scan By Serial -------------------end-------------------------
//    #endregion

//    #region Price Combine Checking Process
//    if (_isCheckedPriceCombine == false)
//        if (_MainPriceCombinItem != null)
//            if (_MainPriceCombinItem.Count > 0)
//            {
//                string _serialiNotpick = string.Empty;
//                string _serialDuplicate = string.Empty;
//                string _taxNotdefine = string.Empty;
//                string _noInventoryBalance = string.Empty;
//                string _noWarrantySetup = string.Empty;

//                string _mItem = txtItem.Text.Trim();
//                _priceDetailRef = new List<PriceDetailRef>();
//                _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToDateTime(txtDate.Text));

//                if (_priceDetailRef.Count <= 0)
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show(_mItem + " does not having price. Please contact costing dept.");
//                    return;
//                }

//                var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
//                if (_dupsMain != null)
//                    if (_dupsMain.Count() > 0)
//                    {
//                        _isCheckedPriceCombine = false;
//                        MessageBox.Show(_mItem + " serial " + ScanSerialNo + " is already picked!");
//                        return;
//                    }


//                if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show(txtItem.Text + " warranty period not setup");
//                    return;
//                }



//                foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
//                {
//                    string _item = _ref.Sapc_itm_cd;
//                    decimal _qty = _ref.Sapc_qty;
//                    string _status = cmbStatus.Text.Trim();


//                    List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
//                    if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
//                    { if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item; else _taxNotdefine += "," + _item; }

//                    if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
//                    { if (string.IsNullOrEmpty(_noWarrantySetup)) _noWarrantySetup = _item; else _noWarrantySetup += "," + _item; }


//                    if (chkDeliverLater.Checked == false && _isCheckedPriceCombine == false)
//                    {
//                        _isCheckedPriceCombine = true;
//                        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
//                        if (_itm.Mi_is_ser1 == 1)
//                        {
//                            var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);

//                            if (_qty > _exist.Count())
//                            { if (string.IsNullOrEmpty(_serialiNotpick)) _serialiNotpick = _item; else _serialiNotpick += "," + _item; }

//                            foreach (ReptPickSerials _p in _exist)
//                            {
//                                string _serial = _p.Tus_ser_1;
//                                var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);

//                                if (_dup != null)
//                                    if (_dup.Count() > 0)
//                                    {
//                                        if (string.IsNullOrEmpty(_serialDuplicate)) _serialDuplicate = _item + "/" + _serial;
//                                        else _serialDuplicate = "," + _item + "/" + _serial;
//                                    }
//                            }
//                        }

//                        decimal _pickQty = 0;
//                        if (IsPriceLevelAllowDoAnyStatus)
//                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum();
//                        else
//                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

//                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
//                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, cmbStatus.Text.Trim());

//                        if (_inventoryLocation != null)
//                            if (_inventoryLocation.Count > 0)
//                            {
//                                decimal _invBal = _inventoryLocation[0].Inl_qty;
//                                if (_pickQty > _invBal)
//                                {
//                                    if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
//                                    else _noInventoryBalance = "," + _item;
//                                }
//                            }
//                            else
//                            {
//                                if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
//                                else _noInventoryBalance = "," + _item;
//                            }
//                        else
//                        {
//                            if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
//                            else _noInventoryBalance = "," + _item;
//                        }
//                    }
//                }

//                if (!string.IsNullOrEmpty(_taxNotdefine))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show(_taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.");
//                    return;
//                }

//                if (!string.IsNullOrEmpty(_serialiNotpick))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show("Item Qty and picked serial mismatch for the following item(s) " + _serialiNotpick);
//                    return;
//                }

//                if (!string.IsNullOrEmpty(_serialDuplicate))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show("Serial duplicating for the following item(s) " + _serialDuplicate);
//                    return;
//                }
//                if (!string.IsNullOrEmpty(_noInventoryBalance))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show(_noInventoryBalance + " item(s) does not having inventory balance for give.");
//                    return;
//                }

//                if (!string.IsNullOrEmpty(_noWarrantySetup))
//                {
//                    _isCheckedPriceCombine = false;
//                    MessageBox.Show(_noWarrantySetup + " item(s) warranty not define.");
//                    return;
//                }

//                _isFirstPriceComItem = true;
//                _isCheckedPriceCombine = true;
//            }

//    #endregion

//    #region  Adding Com Items - Inventory Comcodes

//    if (_isCompleteCode && _isInventoryCombineAdded == false) BindItemComponent(txtItem.Text);

//    if (_masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
//    {
//        //InventoryCombinItemSerialList = new List<ReptPickSerials>();
//        string _combineStatus = string.Empty;
//        decimal _discountRate = -1;
//        decimal _combineQty = 0;

//        _isInventoryCombineAdded = true; _isCombineAdding = true;
//        if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
//        if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
//        if (_discountRate == -1) _discountRate = Convert.ToDecimal(txtDisRate.Text);

//        List<MasterItemComponent> _comItem = new List<MasterItemComponent>();

//        #region Com item check after pick serial (check com main item seperatly, coz its serial already in txtSerialNo textbox)

//        foreach (string _item in _masterItemComponent.Select(x => x.ComponentItem.Mi_cd))
//            _masterItemComponent.Where(s => s.ComponentItem.Mi_cd == _item).ToList().ForEach(y => y.ComponentItem.Mi_itm_tp = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item).Mi_itm_tp);

//        var _item_ = (from _n in _masterItemComponent where _n.ComponentItem.Mi_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
//        if (!string.IsNullOrEmpty(_item_[0]))
//        {
//            string _mItem = Convert.ToString(_item_[0]);
//            _priceDetailRef = new List<PriceDetailRef>();
//            _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text));

//            if (_priceDetailRef.Count <= 0)
//            {
//                MessageBox.Show(_item_[0].ToString() + " does not having price. Please contact costing dept.");
//                return;
//            }
//        }

//        bool _isMainSerialCheck = false;
//        if (ScanSerialList != null && chkDeliverLater.Checked == false)
//        {
//            //check main item serial duplicates
//            if (ScanSerialList.Count > 0)
//            {
//                if (_isMainSerialCheck == false)
//                {

//                    var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
//                    if (_dup != null)
//                        if (_dup.Count() > 0)
//                        {
//                            MessageBox.Show(_item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!");
//                            return;
//                        }
//                    _isMainSerialCheck = true;
//                }

//                //Check scan item duplicates


//                foreach (MasterItemComponent _com in _masterItemComponent)
//                {
//                    string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
//                    var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);

//                    if (_dup != null)
//                        if (_dup.Count() > 0)
//                        {
//                            MessageBox.Show("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!");
//                            return;
//                        }
//                }
//            }

//        }
//        #endregion

//        #region Com item check for its serial status
//        if (InventoryCombinItemSerialList.Count == 0)
//        {
//            _isCombineAdding = true;
//            foreach (MasterItemComponent _com in _masterItemComponent)
//            {
//                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);
//                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
//                {
//                    MessageBox.Show(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.");
//                    return;
//                }


//                if (chkDeliverLater.Checked == false)
//                {
//                    decimal _pickQty = 0;
//                    if (IsPriceLevelAllowDoAnyStatus)
//                        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
//                    else
//                        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

//                    _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
//                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _com.ComponentItem.Mi_cd, cmbStatus.Text.Trim());

//                    if (_inventoryLocation != null)
//                        if (_inventoryLocation.Count > 0)
//                        {
//                            decimal _invBal = _inventoryLocation[0].Inl_qty;
//                            if (_pickQty > _invBal)
//                            {
//                                MessageBox.Show(_com.ComponentItem.Mi_cd + " item inventory balance exceeds");
//                                return;
//                            }
//                        }
//                        else
//                        {
//                            MessageBox.Show(_com.ComponentItem.Mi_cd + " item inventory balance exceeds");
//                            return;
//                        }
//                    else
//                    {
//                        MessageBox.Show(_com.ComponentItem.Mi_cd + " item inventory balance exceeds");
//                        return;
//                    }
//                }



//                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);

//                if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
//                {
//                    _comItem.Add(_com);
//                }
//            }

//            if (_comItem.Count > 1 && chkDeliverLater.Checked == false)
//            {//hdnItemCode.value
//                ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
//                if (_pick != null)
//                    if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
//                    {
//                        var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
//                        if (_dup != null)
//                            if (_dup.Count <= 0)
//                            {
//                                InventoryCombinItemSerialList.Add(_pick);
//                            }
//                    }

//                _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);
//                gvPopComItem.DataSource = _comItem;
//                _isInventoryCombineAdded = false;
//                return;
//            }
//            else if (_comItem.Count == 1 && chkDeliverLater.Checked == false)
//            {//hdnItemCode.Value
//                ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
//                if (_pick != null)
//                    if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
//                    {
//                        var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
//                        if (_dup != null)
//                            if (_dup.Count <= 0)
//                            {
//                                InventoryCombinItemSerialList.Add(_pick);
//                            }
//                    }
//            }
//        }
//        #endregion



//        #region  Adding Com items to grid after pick all items (non serialized added randomly,bt check it if deliver now!)
//        SSCombineLine += 1;
//        foreach (MasterItemComponent _com in _masterItemComponent)
//        {
//            //If going to deliver now
//            if (chkDeliverLater.Checked == false && InventoryCombinItemSerialList.Count > 0)
//            {
//                var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
//                if (_comItemSer != null)
//                    if (_comItemSer.Count > 0)
//                    {
//                        foreach (ReptPickSerials _serItm in _comItemSer)
//                        {
//                            txtSerialNo.Text = _serItm.Tus_ser_1;
//                            ScanSerialNo = txtSerialNo.Text;
//                            //hdnSerialNo.Value = ScanSerialNo;
//                            txtSerialNo.Text = ScanSerialNo;
//                            txtItem.Text = _com.ComponentItem.Mi_cd;
//                            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                            LoadItemDetail(txtItem.Text);
//                            cmbStatus.Text = _combineStatus;
//                            txtQty.Text = FormatToQty("1");
//                            CheckQty();
//                            txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
//                            txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
//                            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
//                            txtLineTotAmt.Text = FormatToCurrency("0");
//                            CalculateItem();
//                            AddItem(false);
//                            ScanSerialNo = string.Empty;
//                            txtSerialNo.Text = string.Empty;
//                            //hdnSerialNo.Value = "";
//                            txtSerialNo.Text = string.Empty;
//                        }
//                        _combineCounter += 1;
//                    }
//                    else
//                    {
//                        txtItem.Text = _com.ComponentItem.Mi_cd;
//                        //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                        LoadItemDetail(txtItem.Text);
//                        cmbStatus.Text = _combineStatus;
//                        txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
//                        CheckQty();
//                        txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
//                        txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
//                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
//                        txtLineTotAmt.Text = FormatToCurrency("0");
//                        CalculateItem();
//                        AddItem(false);
//                        ScanSerialNo = string.Empty;
//                        txtSerialNo.Text = string.Empty;
//                        //hdnSerialNo.Value = "";
//                        txtSerialNo.Text = string.Empty;

//                        _combineCounter += 1;
//                    }

//            }
//            //If deliver later
//            else if (chkDeliverLater.Checked && InventoryCombinItemSerialList.Count == 0)
//            {
//                txtItem.Text = _com.ComponentItem.Mi_cd;
//                //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                LoadItemDetail(txtItem.Text.Trim());
//                cmbStatus.Text = _combineStatus;
//                txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
//                CheckQty();
//                txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
//                txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
//                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
//                txtLineTotAmt.Text = FormatToCurrency("0");
//                CalculateItem();
//                AddItem(false);
//                _combineCounter += 1;
//            }

//        }
//        #endregion

//        if (_combineCounter == _masterItemComponent.Count) { _masterItemComponent = new List<MasterItemComponent>(); _isCompleteCode = false; _isInventoryCombineAdded = false; _isCombineAdding = false; ScanSerialNo = string.Empty; txtSerialNo.Text = ""; InventoryCombinItemSerialList = new List<ReptPickSerials>(); txtSerialNo.Text = string.Empty; return; } //hdnSerialNo.Value = ""
//    }

//    #endregion

//    #region Check item with serial status & load particular serial details
//    _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

//    if (chkDeliverLater.Checked == false)
//    {
//        if (_itm.Mi_is_ser1 == 1)
//        {
//            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
//            {
//                MessageBox.Show("Please select the serial no");
//                txtSerialNo.Focus();
//                return;
//            }
//            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
//        }
//        else if (_itm.Mi_is_ser1 == 0)
//        {
//            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
//        }
//    }
//    #endregion

//    #region Check for fulfilment before adding
//    if (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance))
//    {
//        if (!_isCombineAdding) { MessageBox.Show("Please select the valid price"); return; }

//    }
//    if (string.IsNullOrEmpty(txtQty.Text.Trim())) { MessageBox.Show("Please select the valid qty"); return; }
//    if (Convert.ToDecimal(txtQty.Text) == 0) { MessageBox.Show("Please select the valid qty"); return; }
//    if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { MessageBox.Show("Please select the valid qty"); return; }

//    if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { MessageBox.Show("Please select the valid unit price"); return; }


//    if (string.IsNullOrEmpty(cmbInvType.Text))
//    {
//        MessageBox.Show("Please select the invoice type");
//        cmbInvType.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(txtCustomer.Text))
//    {
//        MessageBox.Show("Please select the customer");
//        txtCustomer.Focus();
//        return;
//    }
//    if (string.IsNullOrEmpty(txtItem.Text))
//    {
//        MessageBox.Show("Please select the item");
//        txtItem.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(cmbBook.Text))
//    {
//        MessageBox.Show("Please select the price book");
//        cmbBook.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(cmbLevel.Text))
//    {
//        MessageBox.Show("Please select the price level");
//        cmbLevel.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(cmbStatus.Text))
//    {
//        MessageBox.Show("Please select the item status");
//        cmbStatus.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(txtQty.Text))
//    {
//        MessageBox.Show("Please select the qty");
//        txtQty.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(txtUnitPrice.Text))
//    {
//        MessageBox.Show("Please select the unit price");
//        txtUnitPrice.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(txtDisRate.Text))
//    {
//        MessageBox.Show("Please select the discount pecentage");
//        txtDisRate.Focus();
//        return;
//    }

//    if (string.IsNullOrEmpty(txtDisAmt.Text))
//    {
//        MessageBox.Show("Please select the discount amount");
//        txtDisAmt.Focus();
//        return;
//    }


//    if (string.IsNullOrEmpty(txtTaxAmt.Text))
//    {
//        MessageBox.Show("Please select the VAT amount");
//        txtTaxAmt.Focus();
//        return;
//    }

//    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);
//    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
//    {
//        MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.");
//        cmbStatus.Focus();
//        return;
//    }

//    if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false)
//    {
//        bool _isTerminate = CheckQty();
//        if (_isTerminate) return;
//    }

//    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
//    if (_lsts != null && _isCombineAdding == false)
//    {
//        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
//        {
//            MessageBox.Show(txtItem.Text + " price not available. Please contact costing dept.");
//            return;
//        }
//        else
//        {
//            decimal sysUPrice = _lsts.Sapd_itm_price * MainTaxConstant[0].Mict_tax_rate;
//            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
//            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
//            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

//            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
//                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

//                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
//                        if (!_MasterProfitCenter.Mpc_edit_price)
//                        {
//                            if (sysUPrice != pickUPrice)
//                            {
//                                MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!");
//                                return;
//                            }
//                        }
//                        else
//                        {
//                            if (sysUPrice != pickUPrice)
//                                if (sysUPrice > pickUPrice)
//                                {
//                                    decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
//                                    decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
//                                    if (ddUprice > pickUPrice)
//                                    {
//                                        MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!");
//                                        return;
//                                    }
//                                }

//                        }
//        }
//    }
//    else
//    {
//        if (_isCombineAdding == false)
//        {
//            MessageBox.Show(txtItem.Text + " price not available. Please contact costing dept.");
//            return;
//        }
//    }

//    #endregion

//    #region Check Item Serial pick or not (function for common item - not for comcode items, but its go through here also


//    if (chkDeliverLater.Checked == false)
//    {
//        if (_itm.Mi_is_ser1 == 1)
//        {
//            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text && x.Tus_ser_1 == ScanSerialNo).ToList();
//            if (_dup != null)
//                if (_dup.Count > 0)
//                {
//                    MessageBox.Show(ScanSerialNo + " serial is already picked!");
//                    txtSerialNo.Focus();
//                    return;
//                }
//        }

//        if (!IsPriceLevelAllowDoAnyStatus)
//        {
//            if (_serLst != null)
//                if (string.IsNullOrEmpty(_serLst.Tus_com))
//                {
//                    if (_serLst.Tus_itm_stus != cmbStatus.Text.Trim())
//                    {
//                        MessageBox.Show(ScanSerialNo + " serial status is not match with the price level status");
//                        txtSerialNo.Focus();
//                        return;
//                    }
//                }
//        }

//    }
//    #endregion

//    CalculateItem();

//    #region Check Inventory Balance if deliver now!

//    //check balance ----------------------
//    if (chkDeliverLater.Checked == false)
//    {
//        decimal _pickQty = 0;
//        if (IsPriceLevelAllowDoAnyStatus)
//            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
//        else
//            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

//        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
//        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());

//        if (_inventoryLocation != null)
//            if (_inventoryLocation.Count > 0)
//            {
//                decimal _invBal = _inventoryLocation[0].Inl_qty;
//                if (_pickQty > _invBal)
//                {
//                    MessageBox.Show(txtItem.Text + " item inventory balance exceeds");
//                    return;
//                }
//            }
//            else
//            {
//                MessageBox.Show(txtItem.Text + " item inventory balance exceeds");
//                return;
//            }
//        else
//        {
//            MessageBox.Show(txtItem.Text + " item inventory balance exceeds");
//            return;
//        }


//        if (_itm.Mi_is_ser1 == 1 && ScanSerialList.Count > 0)
//        {
//            var _serDup = (from _lst in ScanSerialList
//                           where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.Trim()
//                           select _lst).ToList();

//            if (_serDup != null)
//                if (_serDup.Count > 0)
//                {
//                    MessageBox.Show("Serial duplicating.");
//                    return;
//                }

//        }



//    }
//    //check balance ----------------------
//    #endregion

//    #region Get/Check Warranty Period and Remarks
//    //Get Warranty Details --------------------------
//    List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
//    if (_lvl != null)
//        if (_lvl.Count > 0)
//        {
//            var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.Text.Trim() select _l).ToList();
//            if (_lst != null)
//                if (_lst.Count > 0)
//                {
//                    if (_lst[0].Sapl_set_warr == true)
//                    {
//                        WarrantyPeriod = _lst[0].Sapl_warr_period;
//                    }
//                    else
//                    {
//                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.Trim(), cmbStatus.Text.Trim());
//                        if (_period != null)
//                        {
//                            WarrantyPeriod = _period.Mwp_val;
//                            WarrantyRemarks = _period.Mwp_rmk;
//                        }
//                        else
//                        {
//                            MessageBox.Show("Warranty period not setup");
//                            return;
//                        }
//                    }
//                }
//        }
//    //Get Warranty Details --------------------------
//    #endregion

//    bool _isDuplicateItem = false;
//    Int32 _duplicateComLine = 0;
//    Int32 _duplicateItmLine = 0;

//    #region Adding Invoice Item
//    //Adding Items to grid goes here ----------------------------------------------------------------------
//    if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
//    //No Records
//    {
//        _isDuplicateItem = false;
//        _lineNo += 1;
//        if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
//        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm));
//    }
//    else
//    //Having some records
//    {
//        var _similerItem = from _list in _invoiceItemList
//                           where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text)
//                           select _list;

//        if (_similerItem.Count() > 0)
//        //Similer item available
//        {
//            _isDuplicateItem = true;
//            foreach (var _similerList in _similerItem)
//            {
//                _duplicateComLine = _similerList.Sad_job_line;
//                _duplicateItmLine = _similerList.Sad_itm_line;
//                _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
//                _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
//                _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
//                _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);

//            }
//        }
//        else
//        //No similer item found
//        {
//            _isDuplicateItem = false;
//            _lineNo += 1;
//            if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
//            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm));
//        }

//    }
//    //Adding Items to grid end here ----------------------------------------------------------------------
//    #endregion

//    #region Adding Serial/Non Serial items
//    //Scan By Serial ----------start----------------------------------
//    if (chkDeliverLater.Checked == false)
//    {
//        if (_isFirstPriceComItem)
//            _isCombineAdding = true;

//        if (ScanSequanceNo == 0) ScanSequanceNo = -100;

//        //Serialized
//        if (_itm.Mi_is_ser1 == 1)
//        {

//            //ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
//            _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
//            _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
//            _serLst.Tus_usrseq_no = ScanSequanceNo;
//            _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
//            _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
//            _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
//            ScanSerialList.Add(_serLst);
//        }

//        //Non-Serialized but serial ID 8523
//        if (_itm.Mi_is_ser1 == 0)
//        {
//            //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
//            if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
//            {
//                MessageBox.Show(txtItem.Text + " item qty is exceeds available qty");
//                return;
//            }
//            _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
//            _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
//            _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
//            _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
//            _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
//            _nonserLst.ForEach(x => x.Tus_ser_id = -1);
//            _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
//            ScanSerialList.AddRange(_nonserLst);
//        }

//        gvPopSerial.DataSource = ScanSerialList;//.Where(x => x.Tus_ser_1 != "N/A").ToList();


//        if (_isFirstPriceComItem)
//        {
//            _isCombineAdding = false;
//            _isFirstPriceComItem = false;
//        }
//    }
//    //Scan By Serial ----------end----------------------------------
//    #endregion

//    #region Add Invoice Serial Detail
//    //if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
//    //{
//    bool _isDuplicate = false;
//    if (InvoiceSerialList != null)
//        if (InvoiceSerialList.Count > 0)
//        {
//            if (_itm.Mi_is_ser1 == 1)
//            {//hdnItemCode.Value
//                var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.Trim() select _i).ToList();
//                if (_dup != null)
//                    if (_dup.Count > 0)
//                        _isDuplicate = true;
//            }
//        }

//    if (_isDuplicate == false)
//    {
//        //hdnItemCode.Value.ToString()
//        InvoiceSerial _invser = new InvoiceSerial();
//        _invser.Sap_del_loc = BaseCls.GlbUserDefLoca;
//        _invser.Sap_itm_cd = txtItem.Text.Trim();
//        _invser.Sap_itm_line = _lineNo;
//        _invser.Sap_remarks = string.Empty;
//        _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
//        _invser.Sap_ser_1 = txtSerialNo.Text;
//        _invser.Sap_ser_2 = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
//        InvoiceSerialList.Add(_invser);
//    }
//    //}
//    #endregion

//    CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);

//    #region  Adding Combine Items - Price Combine
//    if (_MainPriceCombinItem != null)
//    {
//        string _combineStatus = string.Empty;
//        decimal _combineQty = 0;

//        if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
//        {
//            _isCombineAdding = true;
//            if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
//            if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
//            if (chkDeliverLater.Checked == true)
//            {

//                foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
//                {
//                    txtItem.Text = _list.Sapc_itm_cd;
//                    //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                    LoadItemDetail(txtItem.Text.Trim());
//                    cmbStatus.Text = _combineStatus;
//                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
//                    txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty)));
//                    txtDisRate.Text = FormatToCurrency("0");
//                    txtDisAmt.Text = FormatToCurrency("0");
//                    txtTaxAmt.Text = FormatToCurrency("0");
//                    txtLineTotAmt.Text = FormatToCurrency("0");
//                    CalculateItem();
//                    AddItem(_isPromotion);
//                    _combineCounter += 1;
//                }
//            }
//            else
//            {
//                foreach (ReptPickSerials _list in PriceCombinItemSerialList)
//                {
//                    txtSerialNo.Text = _list.Tus_ser_1;
//                    ScanSerialNo = _list.Tus_ser_1;
//                    txtItem.Text = _list.Tus_itm_cd;
//                    //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                    LoadItemDetail(txtItem.Text.Trim());
//                    cmbStatus.Text = _combineStatus;
//                    decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
//                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
//                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
//                    txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
//                    txtDisRate.Text = FormatToCurrency("0");
//                    txtDisAmt.Text = FormatToCurrency("0");
//                    txtTaxAmt.Text = FormatToCurrency("0");
//                    txtLineTotAmt.Text = FormatToCurrency("0");
//                    CalculateItem();
//                    AddItem(_isPromotion);
//                    _combineCounter += 1;
//                }

//                foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
//                {
//                    MasterItem _i = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _list.Sapc_itm_cd);

//                    if (_i.Mi_is_ser1 != 1)
//                    {
//                        txtItem.Text = _list.Sapc_itm_cd;
//                        //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
//                        LoadItemDetail(txtItem.Text.Trim());
//                        cmbStatus.Text = _combineStatus;
//                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
//                        txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty)));
//                        txtDisRate.Text = FormatToCurrency("0");
//                        txtDisAmt.Text = FormatToCurrency("0");
//                        txtTaxAmt.Text = FormatToCurrency("0");
//                        txtLineTotAmt.Text = FormatToCurrency("0");
//                        CalculateItem();
//                        AddItem(_isPromotion);
//                        _combineCounter += 1;
//                    }
//                }

//            }

//            if (chkDeliverLater.Checked == true)
//                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; ScanSerialNo = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; return; }//hdnSerialNo.Value = ""
//            if (chkDeliverLater.Checked == false)
//                if (_combineCounter == PriceCombinItemSerialList.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; ScanSerialNo = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; return; }//hdnSerialNo.Value = ""
//        }


//    }
//    #endregion

//    txtSerialNo.Text = "";
//    //hdnSerialNo.Value = "";
//    ClearAfterAddItem();

//    SSPriceBookSequance = "0";
//    SSPriceBookItemSequance = "0";
//    SSPriceBookPrice = 0;

//    txtItem.Focus();
//    BindAddItem();
//    SetDecimalTextBoxForZero(true);

//    #region Set Default Paymode if available
//    List<PaymentType> _tp = CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, cmbInvType.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()));
//    //if (_tp != null)
//    //    if (_tp.Count > 0)
//    //    {
//    //        var Mod = _tp.Where(x => x.Stp_def == true).Select(y => y.Stp_pay_tp);
//    //        if (Mod != null)
//    //            if (Mod.Count() > 0)
//    //            {
//    //                foreach (string v in Mod)
//    //                    _paymodedef = v;

//    //                if (ddlPayMode.Items.Count > 1)
//    //                    ddlPayMode.SelectedValue = _paymodedef;
//    //            }
//    //    }
//    #endregion
//}