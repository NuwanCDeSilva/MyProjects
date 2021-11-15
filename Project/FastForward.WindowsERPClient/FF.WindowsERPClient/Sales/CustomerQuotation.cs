using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FF.WindowsERPClient.Sales
{
    public partial class CustomerQuotation : Base
    {
        MasterProfitCenter _MasterProfitCenter = null;
        List<PriceDefinitionRef> _PriceDefinitionRef = null;

        private PriceBookLevelRef _priceBookLevelRef = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private MasterCompany _companyDet = null;
        private MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
        private MasterItem _itemdetail = new MasterItem();
        private List<PriceCombinedItemRef> _MainPriceCombinItem = new List<PriceCombinedItemRef>();
        private List<MasterItemComponent> _masterItemComponent = null;
        private List<MasterItemTax> MainTaxConstant = null;
        private List<PriceDetailRef> _priceDetailRef = null;
        private List<ReptPickSerials> PriceCombinItemSerialList = null;
        private List<PriceCombinedItemRef> _tempPriceCombinItem = new List<PriceCombinedItemRef>();
        private List<QoutationDetails> _invoiceItemList = new List<QoutationDetails>();
        private List<QuotationSerial> _QuoSerials = new List<QuotationSerial>();
        private List<ReptPickSerials> _ResList = new List<ReptPickSerials>();
        List<InventoryRequestItem> _invReqItemList = null;      //kapila 13/7/2016
        private List<QuotationSerial> _QuoSerialsSCM = new List<QuotationSerial>();  //kapila 13/7/2016
        private bool IsPriceLevelAllowDoAnyStatus = false;

        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private string DefaultInvoiceType = string.Empty;
        private string DefaultStatus = string.Empty;
        private Boolean _isBackDate = false;
        private Int16 _quoValidPeriod = 0;
        private string WarrantyRemarks = string.Empty;
        public decimal SSPriceBookPrice = 0;
        public decimal SSNormalPrice = 0;
        public string SSPriceBookSequance = string.Empty;
        public string SSPriceBookItemSequance = string.Empty;
        public string SSIsLevelSerialized = string.Empty;
        public string SSPromotionCode = string.Empty;
        public string SSCirculerCode = string.Empty;
        public Int32 SSPRomotionType = 0;
        public Int32 SSCombineLine = 0;
        private int WarrantyPeriod = 0;
        private bool _isCompleteCode = false;
        private Boolean _isCombineAdding = false;
        private Boolean _isEditPrice = false;
        private Boolean _isEditDiscount = false;
        bool _isInventoryCombineAdded = false;
        private bool _isCheckedPriceCombine = false;
        private bool _isFirstPriceComItem = false;
        private int _combineCounter = 0;
        Dictionary<decimal, decimal> ManagerDiscount = null;
        private Int32 _lineNo = 0;
        private decimal GrndSubTotal = 0;
        private decimal GrndDiscount = 0;
        private decimal GrndTax = 0;
        private bool IsSaleFigureRoundUp = false;
        static int VirtualCounter = 1;
        private DataTable _tblExecutive = null;
        Int32 _serID = 0;
        private string _itmType = string.Empty;
        private Boolean _isMinus = false;
        public Int32 quoSeq = 0;
        private decimal _totDPAmt = 0;  //kapila 8/1/2016
        private decimal _dpRate = 0;
        private Int32 _isQuoBase = 0;   //kapila 11/3/2016
        private Int32 _isSelQuoBaseLevel = 0;
        private Int32 _recordCount = 0;
        private string _qh_jobno = "";
        private string _QH_ANAL_4 = "";
        private Boolean _isStrucBaseTax = false;    //kapila 17/7/2017

        private System.Windows.Forms.Label[] lblCriteria;
        private System.Windows.Forms.Button[] btnOk;
        private System.Windows.Forms.Button[] btnFail;
        private System.Windows.Forms.Label[] lblVal;

        private void LoadValue(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("lblVal", _num);
            int n = 1;
            pnlVal.Height = 25;
            while (n < _num + 1)
            {
                lblVal[n].Tag = n;
                lblVal[n].Width = 235;
                lblVal[n].Height = 21;
                lblVal[n].BackColor = Color.NavajoWhite;
                lblVal[n].Left = xPos;
                lblVal[n].Top = yPos;
                yPos = yPos + lblVal[n].Height + 6;
                pnlVal.Controls.Add(lblVal[n]);
                pnlVal.Height = pnlVal.Height + 25;
                n++;
            }
        }

        private void LoadCriteria(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("lblCriteria", _num);
            int n = 1;
            pnlCriteria.Height = 25;
            while (n < _num + 1)
            {
                lblCriteria[n].Tag = n;
                lblCriteria[n].Width = 235;
                lblCriteria[n].Height = 21;
                lblCriteria[n].BackColor = Color.Orange;
                lblCriteria[n].Left = xPos;
                lblCriteria[n].Top = yPos;
                yPos = yPos + lblCriteria[n].Height + 6;
                pnlCriteria.Controls.Add(lblCriteria[n]);
                pnlCriteria.Height = pnlCriteria.Height + 25;
                n++;
            }
        }
        private void LoadOk(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("btnOk", _num);
            int n = 1;
            pnlOk.Height = 25;
            while (n < _num + 1)
            {
                btnOk[n].Tag = n;
                btnOk[n].Width = 25;
                btnOk[n].Height = 25;
                object O = global::FF.WindowsERPClient.Properties.Resources.ResourceManager.GetObject("security");
                btnOk[n].Image = (Image)O;
                btnOk[n].Left = xPos;
                btnOk[n].Top = yPos;
                btnOk[n].Visible = false;
                yPos = yPos + btnOk[n].Height + 2;
                pnlOk.Controls.Add(btnOk[n]);
                pnlOk.Height = pnlOk.Height + 25;
                n++;
            }
        }
        private void LoadFail(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("btnFail", _num);
            int n = 1;
            pnlFail.Height = 25;
            while (n < _num + 1)
            {
                btnFail[n].Tag = n;
                btnFail[n].Width = 25;
                btnFail[n].Height = 25;
                object O = global::FF.WindowsERPClient.Properties.Resources.ResourceManager.GetObject("ExitSCM2");
                btnFail[n].Image = (Image)O;
                btnFail[n].Left = xPos;
                btnFail[n].Top = yPos;
                btnFail[n].Visible = false;
                yPos = yPos + btnFail[n].Height + 2;
                pnlFail.Controls.Add(btnFail[n]);
                pnlFail.Height = pnlFail.Height + 25;
                n++;
            }
        }
        private void AddControls(string anyControl, int cNumber)
        {
            switch (anyControl)
            {
                case "lblCriteria":
                    {
                        lblCriteria = new System.Windows.Forms.Label[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            lblCriteria[i] = new System.Windows.Forms.Label();
                        }
                        break;
                    }
                case "btnOk":
                    {
                        btnOk = new System.Windows.Forms.Button[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            btnOk[i] = new System.Windows.Forms.Button();
                        }
                        break;
                    }
                case "btnFail":
                    {
                        btnFail = new System.Windows.Forms.Button[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            btnFail[i] = new System.Windows.Forms.Button();
                        }
                        break;
                    }
                case "lblVal":
                    {
                        lblVal = new System.Windows.Forms.Label[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            lblVal[i] = new System.Windows.Forms.Label();
                        }
                        break;
                    }
            }
        }
        public CustomerQuotation()
        {
            try
            {
                InitializeComponent();
                if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                {
                    MessageBox.Show("You do not have assigned a profit center. " + this.Text + " is terminating now!", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                if (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca))
                {
                    MessageBox.Show("You do not have assigned a delivery location. " + this.Text + " is de-activating delivery option now!", "De-activate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                pnlMsg.Size = new Size(418, 321);
                pnlNewSer.Size = new Size(611, 157);
                pnlNewSer.Location = new Point(279, 128);
                _invReqItemList = new List<InventoryRequestItem>();
                //if(BaseCls.GlbUserComCode != "AAL")
                //{
                //    chkWH.Visible = false;
                //    txtWH.Visible = false;
                //    btn_srch_WHLoc.Visible = false;
                //}
                //LoadCachedObjects();
                //InitializeValuesNDefaultValueSet();
                Clear_Data();
                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadCachedObjects()
        {
            try
            {
                _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
                IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
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

                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerQuo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + lblItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialSCM:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtWH.Text + seperator + lblItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Data();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadExecutive()
        {
            try
            {
                DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                cmbExecutive.ValueMember = "esep_epf";
                cmbExecutive.DisplayMember = "esep_first_name";
                cmbExecutive.DataSource = _tblExecutive;
                cmbExecutive.DropDownWidth = 200;
                if (_tblExecutive != null) { cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man; }
                txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                if (_tblExecutive != null)
                {
                    var _lst = (from DataRow _l in _tblExecutive.Rows select _l).ToList();
                    Parallel.ForEach(_lst, x =>
                    {
                        _string.Add(x.Field<string>("esep_first_name"));
                    });
                    cmbExecutive.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbExecutive.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbExecutive.AutoCompleteCustomSource = _string;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Clear_Data()
        {

            lblBrNo.Text = string.Empty;
            _MasterProfitCenter = null;
            _PriceDefinitionRef = null;

            _priceBookLevelRef = null;
            _priceBookLevelRefList = null;
            _companyDet = null;
            PriceCombinItemSerialList = null;
            _masterBusinessCompany = new MasterBusinessEntity();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _itemdetail = new MasterItem();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _invoiceItemList = new List<QoutationDetails>();
            _QuoSerials = new List<QuotationSerial>();
            _QuoSerialsSCM = new List<QuotationSerial>();
            _ResList = new List<ReptPickSerials>();
            _invReqItemList = new List<InventoryRequestItem>();
            _priceDetailRef = null;
            MainTaxConstant = null;
            _masterItemComponent = null;
            _isCheckedPriceCombine = false;
            IsSaleFigureRoundUp = false;
            chkDeliverLater.Checked = true;
            dgItem.AutoGenerateColumns = false;
            dgItem.DataSource = new List<QoutationDetails>();

            IsPriceLevelAllowDoAnyStatus = false;
            dgvSerial.AutoGenerateColumns = false;
            dgvSerial.DataSource = new List<QuotationSerial>();
            dgvSerialSCM.AutoGenerateColumns = false;
            dgvSerialSCM.DataSource = new List<QuotationSerial>();
            lblLine.Text = "";
            lblItem.Text = "";
            lblItmStus.Text = "";
            lblQty.Text = "";
            txtengine.Text = "";
            txtChasis.Text = "";
            VirtualCounter = 1;
            DefaultBook = string.Empty;
            DefaultLevel = string.Empty;
            DefaultInvoiceType = string.Empty;
            DefaultStatus = string.Empty;
            _isBackDate = false;
            _quoValidPeriod = 0;
            _lineNo = 0;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            lblGrndSubTotal.Text = FormatToCurrency("0");
            lblGrndDiscount.Text = FormatToCurrency("0");
            lblGrndAfterDiscount.Text = FormatToCurrency("0");
            lblGrndTax.Text = FormatToCurrency("0");
            lblGrndTotalAmount.Text = FormatToCurrency("0");

            LoadCachedObjects();
            InitializeValuesNDefaultValueSet();
            //BackDatePermission();
            txtDocRefNo.Text = "";
            txtManualRefNo.Text = "";
            txtInvoiceNo.Text = "";
            txtCustomer.Text = "";
            txtCusName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtMobile.Text = "";

            txtDCusAdd1.Text = "";
            txtDCusAdd2.Text = "";
            txtDCusCode.Text = "";
            txtdCusName.Text = "";
            txtDNic.Text = "";
            txtDMob.Text = "";
            txtDFax.Text = "";
            txtItem.Text = "";
            txtModel.Text = "";
            chkTaxPayable.Checked = false;
            WarrantyRemarks = string.Empty;
            SSPriceBookPrice = 0;
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = 0;
            SSNormalPrice = 0;
            SSCombineLine = 0;
            WarrantyPeriod = 0;
            _isCompleteCode = false;
            _isCombineAdding = false;
            _isEditPrice = false;
            _isEditDiscount = false;
            _isInventoryCombineAdded = false;
            ManagerDiscount = null;
            pnlPriceNPromotion.Visible = false;
            _isFirstPriceComItem = false;
            _combineCounter = 0;
            txtExecutive.Text = "";
            txtPaymentTerm.Text = "";
            txtRemarks.Text = "";
            txtAddWara.Text = "";
            lblVatExemptStatus.Text = "";
            lblSVatStatus.Text = "";
            dtpValidTo.Enabled = true;
            btn_save.Enabled = true;
            chkRes.Checked = false;
            _serID = 0;
            lblStus.Text = "";
            _itmType = string.Empty;
            _isMinus = false;
            _totDPAmt = 0;
            quoSeq = 0;
            chkWH.Checked = false;
            chkWH.Enabled = true;
            txtWH.Enabled = false;
            btn_srch_WHLoc.Enabled = false;
            cmbInvType.Enabled = true;
            _qh_jobno = "";
            _QH_ANAL_4 = "";
            LoadExecutive();
        }

        private void BackDatePermission()
        {
            try
            {
                _isBackDate = false;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void InitializeValuesNDefaultValueSet()
        {
            try
            {
                DateTime _validTo = DateTime.Now.Date;
                txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
                _companyDet = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                //UpdateDefaultButton by akila 2017/01/220
                if (string.IsNullOrEmpty(_companyDet.Mc_anal4))
                {
                    throw new Exception("Quotation period has not defined");
                }
                _quoValidPeriod = Convert.ToInt16(_companyDet.Mc_anal4);
                //_quoValidPeriod = Convert.ToInt16(_companyDet.Mc_anal4);
                _validTo = Convert.ToDateTime(txtDate.Value.Date).AddDays(_quoValidPeriod).Date;
                dtpValidTo.Value = _validTo.Date;
                //dtpValidTo.Text = _validTo.Date.ToString("dd/MM/yyyy");
                //VaribleClear();
                //VariableInitialization();
                LoadInvoiceProfitCenterDetail();
                LoadPriceDefaultValue();
                //LoadCancelPermission();
                SetDecimalTextBoxForZero(true, true);
                lblBackDateInfor.Text = string.Empty;
                //BackDatePermission();
                txtQty.Text = FormatToQty("1");
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void CheckUnitPrice(object sender, EventArgs e)
        {
            try
            {
                if (txtUnitPrice.ReadOnly) return;

                if (!IsNumeric(txtUnitPrice.Text))
                {
                    MessageBox.Show("Please enter valid unit price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUnitPrice.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtUnitPrice.Text) < 0)
                {
                    MessageBox.Show("Please enter valid unit price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUnitPrice.Focus();
                    return;
                }

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
                        if (SSPriceBookPrice == 0 && _itmType != "V")
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
                            if (_itmType != "V")
                            {
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                            }
                            _isEditPrice = false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);

                if (_itmType == "V" && _isMinus == true)
                {
                    _totDPAmt = _totDPAmt + val;
                    val = val * -1;
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
                }
                CalculateItem();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void LoadInvoiceProfitCenterDetail()
        {
            try
            {
                if (_MasterProfitCenter != null)
                    if (_MasterProfitCenter.Mpc_cd != null)
                    {
                        if (!_MasterProfitCenter.Mpc_edit_price) txtUnitPrice.ReadOnly = true;
                        txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer;
                        //txtDelLocation.Text = _MasterProfitCenter.Mpc_def_loc; TODO : in delivery instruction, remove comment
                        //lblCurrency.Text = _MasterProfitCenter.Mpc_def_exrate + " - Sri Lankan Rupees";
                        //txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void LoadPriceDefaultValue()
        {
            try
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
                                //DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                                LoadInvoiceType();
                                LoadPriceBook(cmbInvType.Text);
                                LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                                LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());

                            }
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private DataTable _levelStatus = null;

        private bool LoadLevelStatus(string _invType, string _book, string _level)
        {

            _levelStatus = null;
            bool _isAvailable = false;
            try
            {
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
                        //kapila 11/3/2016
                        if (_priceBookLevelRef.SAPL_QUO_BASE == 1)
                            _isQuoBase = 1;
                        else
                            _isQuoBase = 0;
                    }
                    else
                        cmbStatus.DataSource = null;
                else
                    cmbStatus.DataSource = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            return _isAvailable;
        }

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
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
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

        private void cmbInvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDocRefNo.Focus();
        }

        private void cmbInvType_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadPriceBook(cmbInvType.Text.Trim());
                LoadPriceLevel(cmbInvType.Text.Trim(), cmbBook.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInvType.Text == "CRED")
            {
                btnCustomer.Enabled = false;
                chkWH.Visible = true;
                txtWH.Visible = true;
                btn_srch_WHLoc.Visible = true;
                chkWH.Checked = false;
            }
            else
            {
                btnCustomer.Enabled = true;
                chkWH.Visible = false;
                txtWH.Visible = false;
                btn_srch_WHLoc.Visible = false;
                chkWH.Checked = false;
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustomer;
                _CusCre.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDocRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRefNo.Focus();
        }

        private void txtManualRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpValidTo.Focus();
        }



        private void txtDate_Leave(object sender, EventArgs e)
        {
            dtpValidTo.Value = Convert.ToDateTime(txtDate.Value).AddDays(_quoValidPeriod).Date;
        }



        private void dtpValidTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCustomer.Focus();
        }

        private void dtpValidTo_Leave(object sender, EventArgs e)
        {
            Int32 _days = 0;
            _days = (Convert.ToDateTime(dtpValidTo.Value).Date - Convert.ToDateTime(txtDate.Text).Date).Days;

            if (dtpValidTo.Value.Date < DateTime.Today.Date)
            {
                MessageBox.Show("Valid date cannot be less than current date", "Customer Quotation - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpValidTo.Focus();
                return;
            }

            if (_quoValidPeriod < _days)
            {
                MessageBox.Show("Cannot exceed define valid days." + _quoValidPeriod, "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpValidTo.Value = Convert.ToDateTime(txtDate.Value).AddDays(_quoValidPeriod).Date;
                dtpValidTo.Focus();
                return;
            }
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCustomer_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCustomer;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtCustomer.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtDCusCode.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text)) return;
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusName.Clear();
                    txtAddress1.Clear();
                    txtAddress2.Clear();
                    txtMobile.Clear();
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
                    _dpRate = _masterBusinessCompany.Mbe_min_dp_per;

                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusName.Clear();
                        txtAddress1.Clear();
                        txtAddress2.Clear();
                        txtMobile.Clear();
                        chkTaxPayable.Checked = false;
                        txtCustomer.Clear();
                        txtCustomer.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        if (_isAvailable == null || _isAvailable.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The selected Customer is not allowed to perform the transaction under the selected invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCusName.Clear();
                            txtAddress1.Clear();
                            txtAddress2.Clear();
                            txtMobile.Clear();
                            chkTaxPayable.Checked = false;
                            txtCustomer.Clear();
                            txtCustomer.Focus();
                            return;
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("The selected Customer is not allowed to perform the transaction under the selected invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusName.Clear();
                        txtAddress1.Clear();
                        txtAddress2.Clear();
                        txtMobile.Clear();
                        chkTaxPayable.Checked = false;
                        txtCustomer.Clear();
                        txtCustomer.Focus();
                        return;
                    }


                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.ReadOnly = false;
                        txtAddress1.ReadOnly = false;
                        txtAddress2.ReadOnly = false;

                        txtCusName.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtMobile.Text = "";

                    }
                    else
                    {

                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                        txtCusName.ReadOnly = true;
                        txtAddress1.ReadOnly = true;
                        txtAddress2.ReadOnly = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Text = "";
                    txtCusName.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtMobile.Text = "";
                    txtCustomer.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }

        protected void LoadDeliverCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDCusCode.Text)) return;
                //if (cmbInvType.Text.Trim() == "CRED" && txtDCusCode.Text.Trim() == "CASH")
                //{
                //    MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtdCusName.Clear();
                //    txtDCusAdd1.Clear();
                //    txtDCusAdd2.Clear();
                //    txtDMob.Clear();
                //    txtDCusCode.Clear();
                //    txtDFax.Clear();
                //    txtDNic.Clear();
                //    txtDCusCode.Focus();
                //    return;
                //}


                txtdCusName.Text = "";
                txtDCusAdd1.Text = "";
                txtDCusAdd2.Text = "";
                txtDMob.Text = "";
                txtDFax.Text = "";
                txtDNic.Text = "";
                lblBrNo.Text = string.Empty;

                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtDCusCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtDCusCode.Text, string.Empty, string.Empty, "C");
                }

                if (_masterBusinessCompany.Mbe_cd != null)
                {


                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtDCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtdCusName.Text = "";
                        txtDCusAdd1.Text = "";
                        txtDCusAdd2.Text = "";
                        txtDMob.Text = "";
                        txtDFax.Text = "";
                        txtDNic.Text = "";
                    }
                    else
                    {
                        txtdCusName.Text = _masterBusinessCompany.Mbe_name;
                        txtDCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        txtDCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        txtDMob.Text = _masterBusinessCompany.Mbe_mob;
                        txtDFax.Text = _masterBusinessCompany.Mbe_fax;
                        txtDNic.Text = _masterBusinessCompany.Mbe_nic;
                        if(!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_br_no))
                        {
                            lblBrNo.Text = "Business Reg. No: " + _masterBusinessCompany.Mbe_br_no;
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDCusCode.Text = "";
                    txtdCusName.Text = "";
                    txtDCusAdd1.Text = "";
                    txtDCusAdd2.Text = "";
                    txtDMob.Text = "";
                    txtDFax.Text = "";
                    txtDNic.Text = "";
                    lblBrNo.Text = string.Empty;
                    txtDCusCode.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, QuotationHeader _hdr)
        {
            try
            {
                txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                txtCusName.Text = _masterBusinessCompany.Mbe_name;
                txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
                txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
                txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                chkTaxPayable.Checked = _masterBusinessCompany.Mbe_is_tax;

                txtDCusAdd1.Text = "";
                txtDCusAdd2.Text = "";
                txtDCusCode.Text = "";
                txtdCusName.Text = "";
                txtDNic.Text = "";
                txtDMob.Text = "";
                txtDFax.Text = "";
                lblBrNo.Text = string.Empty;

                if (_isRecall == false)
                {
                    if (string.IsNullOrEmpty(txtDCusAdd1.Text)) txtDCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                    if (string.IsNullOrEmpty(txtDCusAdd2.Text)) txtDCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                    if (string.IsNullOrEmpty(txtDCusCode.Text)) txtDCusCode.Text = _masterBusinessCompany.Mbe_cd;
                    if (string.IsNullOrEmpty(txtdCusName.Text)) txtdCusName.Text = _masterBusinessCompany.Mbe_name;
                    if (string.IsNullOrEmpty(txtDNic.Text)) txtDNic.Text = _masterBusinessCompany.Mbe_nic;
                    if (string.IsNullOrEmpty(txtDMob.Text)) txtDMob.Text = _masterBusinessCompany.Mbe_mob;
                    if (string.IsNullOrEmpty(txtDFax.Text)) txtDFax.Text = _masterBusinessCompany.Mbe_fax;

                    if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_br_no))
                    {
                        lblBrNo.Text = "Business Reg. No: " + _masterBusinessCompany.Mbe_br_no;
                    }
                }
                else
                {
                    txtCusName.Text = _hdr.Qh_party_name;
                    txtAddress1.Text = _hdr.Qh_add1;
                    txtAddress2.Text = _hdr.Qh_add2;
                    txtCustomer.Text = _hdr.Qh_party_cd;
                    txtMobile.Text = _hdr.Qh_tel;
                    chkTaxPayable.Checked = _hdr.Qh_is_tax;


                    txtDCusAdd1.Text = _hdr.Qh_del_cusadd1;
                    txtDCusAdd2.Text = _hdr.Qh_del_cusadd2;
                    txtDCusCode.Text = _hdr.Qh_del_cuscd;
                    txtdCusName.Text = _hdr.Qh_del_cusname;
                    txtDNic.Text = _hdr.Qh_del_cusid;
                    txtDMob.Text = _hdr.Qh_del_custel;
                    txtDFax.Text = _hdr.Qh_del_cusfax;
                }

                //if (_isRecall == false)
                //{
                //    if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
                //}

                if (string.IsNullOrEmpty(txtDNic.Text)) { cmbTitle.SelectedIndex = 0; return; }
                if (IsValidNIC(txtDNic.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
                GetNICGender();
                if (string.IsNullOrEmpty(txtdCusName.Text)) txtdCusName.Text = cmbTitle.Text.Trim();
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    bool _exist = cmbTitle.Items.Contains(_title);
                    if (_exist)
                        cmbTitle.Text = _title;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void GetNICGender()
        {
            String nic_ = txtDNic.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = "";
            if (nic_.Length == 10)     //kapila 14/1/2016
                thirdNum = (nicarray[2]).ToString();
            else if (nic_.Length == 12)
                thirdNum = (nicarray[4]).ToString();

            if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
            {
                cmbTitle.Text = "Ms.";
            }
            else
            {
                cmbTitle.Text = "Mr.";
            }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItem;
                    _CommonSearch.ShowDialog();
                    txtItem.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CheckItemCode(object sender, EventArgs e)
        {
            try
            {
                _isCombineAdding = false;
                if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtModel.Clear();
                    txtItem.Focus();
                    cmbStatus.Text = "";
                    _itmType = string.Empty;
                    _isMinus = false;
                    return;
                }

                txtQty.Text = FormatToQty("1");
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool LoadItemDetail(string _item)
        {

            _itemdetail = new MasterItem();

            bool _isValid = false;
            try
            {
                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        txtItem.Text = _itemdetail.Mi_cd;
                        txtModel.Text = _itemdetail.Mi_model;
                        _itmType = _itemdetail.Mi_itm_tp;
                        _isMinus = _itemdetail.Mi_anal4;

                        if (_itmType == "V")
                        {
                            txtUnitPrice.ReadOnly = false;
                        }
                        else
                        {
                            if (BaseCls.GlbUserDefProf == "33")//enable txt box unite price rqst by asanka
                            { txtUnitPrice.ReadOnly = false; }
                            else { txtUnitPrice.ReadOnly = true; }
                            //txtUnitPrice.ReadOnly = true;
                        }
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            return _isValid;
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbLevel.Focus();
        }

        private void cmbBook_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                //CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void cmbLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                //CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                //SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        private decimal TaxCalculationForNorPrice(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxfaction)
        {
            try
            {
                if (_priceBookLevelRef != null)
                    if (_priceBookLevelRef.Sapl_vat_calc)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString()); 
                        else
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            //if (lblVatExemptStatus.Text != "Available")
                            //{
                            //  if (_isTaxfaction == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                            if (_isTaxfaction == false)
                                if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                    _pbUnitPrice = _pbUnitPrice;
                                else
                                _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            else
                                //if (chkTaxPayable.Checked == true)
                                //{
                                //    _discount = _pbUnitPrice * _qty * Convert.ToDecimal(txtDisRate.Text) / 100;
                                //    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                //}
                                //else
                                _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;

                            //}
                            //else
                            //{
                            //    if (_isTaxfaction) _pbUnitPrice = 0;
                            //}
                        }
                    }
                    else
                        if (_isTaxfaction) _pbUnitPrice = 0;


                return _pbUnitPrice;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxfaction)
        {
            try
            {

                if (_priceBookLevelRef != null)
                    if (_priceBookLevelRef.Sapl_vat_calc)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                                _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), null, null, _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString()); 
                        else
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");

                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            //if (lblVatExemptStatus.Text != "Available")
                            //{
                            //  if (_isTaxfaction == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                            if (_isTaxfaction == false)
                                if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                    _pbUnitPrice = _pbUnitPrice;
                                else
                                _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            else
                                if (chkTaxPayable.Checked == true)
                                {
                                    _discount = _pbUnitPrice * _qty * Convert.ToDecimal(txtDisRate.Text) / 100;
                                    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;

                            //}
                            //else
                            //{
                            //    if (_isTaxfaction) _pbUnitPrice = 0;
                            //}
                        }
                    }
                    else
                        if (_isTaxfaction) _pbUnitPrice = 0;


                return _pbUnitPrice;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //private decimal FigureRoundUp(decimal value)
        //{
        //    //if (IsSaleFigureRoundUp) return Math.Round(value);
        //    //else return value;
        //    if (IsSaleFigureRoundUp) return RoundUpForPlace(Math.Round(value + Convert.ToDecimal(.49)), 2);
        //    else return RoundUpForPlace(value, 2);
        //}

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            //if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value + Convert.ToDecimal(.49)), 2);


            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return RoundUpForPlace(value, 2);
        }


        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), false)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), true), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    //_disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100));
                    //txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                    //bool _isVATInvoice = false;
                    //if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    //else _isVATInvoice = false;

                    //if (_isVATInvoice)
                    //    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                    //else
                    //    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                    //txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }

                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    //  _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
            //if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            //{
            //    txtUnitAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim())));

            //    decimal _vatPortion = TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), true);
            //    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

            //    decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
            //    decimal _disAmt = 0;

            //    if (!string.IsNullOrEmpty(txtDisRate.Text))
            //    {
            //        _disAmt = _totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100);
            //        txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
            //    }

            //    if (!string.IsNullOrEmpty(txtTaxAmt.Text))
            //    {
            //        _totalAmount = _totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt;
            //    }

            //    txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            //}
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

            //if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //    txtQty.Text = FormatToQty("1");

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


        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            if (!_isCompleteCode)
            {
                List<MasterItemTax> _tax = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
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

        private void CheckItemTax(string _item)
        {

            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                if (_isStrucBaseTax == true)       //kapila
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    MainTaxConstant = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim(), null, null, _mstItem.Mi_anal1);
                    foreach (MasterItemTax _ones in MainTaxConstant)
                    {
                        _ones.Mict_tax_rate = 1;
                    }
                }
                else
                    MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
            }
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

        }

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

        private decimal CheckSubItemTax(string _item)
        {
            //update by akila 2017/08/11
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                if (_isStrucBaseTax == true)
                {
                    _fraction = 1;
                }
                else
                {
                    TaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());

                    if (TaxConstant != null)
                        if (TaxConstant.Count > 0)
                            _fraction = TaxConstant[0].Mict_tax_rate;
                }
            }
            return _fraction;

            //decimal _fraction = 1;
            //List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            //if (_priceBookLevelRef.Sapl_vat_calc == true)
            //{
            //    TaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
            //    if (TaxConstant != null)
            //        if (TaxConstant.Count > 0)
            //            _fraction = TaxConstant[0].Mict_tax_rate;
            //}
            //return _fraction;
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
            if (string.IsNullOrEmpty(_promotioncd) || _promotioncd.Trim().ToUpper() == "N/A") SSPromotionCode = string.Empty;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }

        private void UncheckNormalPriceOrPromotionPrice(bool _isNormal, bool _isPromotion)
        {
            if (_isNormal)
                if (gvNormalPrice.RowCount > 0)
                {
                    foreach (DataGridViewRow _r in gvNormalPrice.Rows)
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells[0];
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
            _tempPriceCombinItem.Where(x => x.Sapc_increse).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(txtQty.Text.Trim()));
            _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
            _tempPriceCombinItem.Where(x => !string.IsNullOrEmpty(x.Sapc_sub_ser)).ToList().ForEach(x => x.Sapc_increse = true);
            gvPromotionItem.DataSource = _tempPriceCombinItem;
        }

        private void gvPromotionPrice_CellDoubleClick(Int32 _row, bool _isValidate)
        {
            try
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
                                          where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected bool CheckQty()
        {
            try
            {
                WarrantyRemarks = string.Empty;
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
                    MessageBox.Show("Selected item tax definition is not setup for the selected status. Please contact inventory dept.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                //#region Consumer Item
                //if (ConsumerItemProduct())
                //{
                //    _IsTerminate = true;
                //    return _IsTerminate;
                //}
                //#endregion
                //#region Check & Load Serialized Prices and Its Promotion
                //if (CheckSerializedPriceLevelAndLoadSerials())
                //{
                //    _IsTerminate = true;
                //    return _IsTerminate;
                //}
                //#endregion

                //if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;


                _priceDetailRef = new List<PriceDetailRef>();
                //Akila - CRQ Allow to proceed with the promotions which are valid within the quotation expire date
                _priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), DateTime.Today.Date, DateTime.Today.Date);
                //_priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(dtpValidTo.Value));

                if (_priceDetailRef.Count <= 0)
                {
                    //Inventory Combine Item -------------------------------
                    if (!_isCompleteCode && _itmType != "V")
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
                        //var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        var _p = _new.Where(x => x.Sapd_price_type == 0).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                                _priceDetailRef.Add(_p[0]);
                    }
                    //Inventory Combine Item -------------------------------

                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        //updated by akila 2017/08/14
                        if (_priceDetailRef.OrderByDescending(x => x.Sapd_from_date).First().Sapd_price_stus == "S")
                        {
                            MessageBox.Show("Price has been suspended. Please contact costing dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                        //var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        //if (_isSuspend > 0)
                        //{
                        //    MessageBox.Show("Price has been suspended. Please contact costing dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    _IsTerminate = true;
                        //    return _IsTerminate;
                        //}
                    }

                    if (_priceDetailRef.Count > 1)
                    {
                        //Find More than one price for the selected item
                        //Load prices for the grid and popup for user confirmation

                        //IsNoPriceDefinition = false;
                        SetColumnForPriceDetailNPromotion(false);
                        BindNonSerializedPrice(_priceDetailRef);
                        pnlPriceNPromotion.Visible = true;
                        _IsTerminate = true;
                        // pnlMain.Enabled = false;
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
                            //decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), false);
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), false), true);

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
                                gvNormalPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                                BindNonSerializedPrice(_priceDetailRef);
                                //BindPriceCombineItem(_pbSq, _pbiSq, _priceType, _mItem, string.Empty);
                                gvPromotionPrice_CellDoubleClick(0, false);
                                //IsNoPriceDefinition = false;
                                pnlPriceNPromotion.Visible = true;
                                //pnlMain.Enabled = false;
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (!IsNumeric(txtQty.Text))
                    {
                        MessageBox.Show("Invalid qty.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQty.Focus();
                        return;
                    }
                    CheckQty();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        protected void CheckDiscountRate(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDisRate.Text)) { return; }

                if (!IsNumeric(txtDisRate.Text))
                {
                    MessageBox.Show("Please enter valid discount rate.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Text = "0.00";
                    txtDisRate.Focus();
                    return;
                }

                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                {
                    MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Clear();
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }
                CheckDiscountRate();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected bool CheckDiscountRate()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTaxAmt.Focus();
        }
        protected void CheckDiscountAmount(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisAmt.Text)) { return; }

            if (!IsNumeric(txtDisAmt.Text))
            {
                MessageBox.Show("Invalid discount amount.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDisAmt.Text = "0.00";
                txtDisAmt.Focus();
                return;
            }

            //CheckDiscountAmount();
        }

        private bool CheckDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
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
                    if (ManagerDiscount.Count > 0)
                    {
                        var vals = ManagerDiscount.Select(x => x.Key).ToList();
                        var rates = ManagerDiscount.Select(x => x.Value).ToList();

                        if (vals[0] < _disAmt && rates[0] == 0)
                        {
                            MessageBox.Show("You can not discount price more than " + vals[0] + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            txtDisRate.Text = "0";
                            CalculateItem();
                            _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                            txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                            CalculateItem();
                            CheckDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        if (ManagerDiscount.Count > 0)
                        {
                            var vals = ManagerDiscount.Select(x => x.Key).ToList();
                            var rates = ManagerDiscount.Select(x => x.Value).ToList();

                            if (vals[0] < _disAmt)
                            {
                                MessageBox.Show("You can not discount price more than " + vals[0] + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtDisRate.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                txtDisRate.Text = "0";
                                CalculateItem();
                                _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
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

        private void AddItem(bool _isPromotion, string _originalItem)
        {
            bool _isAdded = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;

                //#region Gift Voucher Check
                //if ((chkPickGV.Checked || IsGiftVoucher(_itemdetail.Mi_itm_tp)) && _isCombineAdding == false)
                //{
                //    if (gvInvoiceItem.Rows.Count <= 0)
                //    { MessageBox.Show("Please select the selling item before add gift voucher.", "Need Selling Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                //    if (gvInvoiceItem.Rows.Count > 0)
                //    {
                //        var _noOfSets = _invoiceItemList.Select(x => x.Sad_job_line).Distinct().ToList();

                //        var _giftCount = _invoiceItemList.Where(x => IsGiftVoucher(x.Sad_itm_tp)).Sum(x => x.Sad_qty);
                //        var _nonGiftCount = _invoiceItemList.Sum(x => x.Sad_qty) - _giftCount;
                //        if (_nonGiftCount < _giftCount + 1)
                //        {
                //            MessageBox.Show("You can not add more gift vouchers than selling qty", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //    }

                //    DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByPageNItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtSerialNo.Text.Trim()), txtItem.Text.Trim());
                //    if (_giftVoucher != null)
                //        if (_giftVoucher.Rows.Count > 0)
                //        {
                //            _serial2 = Convert.ToString(_giftVoucher.Rows[0].Field<Int64>("gvp_book"));
                //            _prefix = Convert.ToString(_giftVoucher.Rows[0].Field<string>("gvp_gv_prefix"));
                //        }

                //}
                //#endregion

                //#region Check for Payment
                //if (_recieptItem != null)
                //    if (_recieptItem.Count > 0)
                //    {
                //        this.Cursor = Cursors.Default;
                //        MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //#endregion

                SSNormalPrice = 0;
                //Get Normal Price



                #region Priority Base Validation
                if (_masterBusinessCompany == null)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer code", "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _isAdded = false;
                    return;
                }

                if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")
                    //if ((Convert.ToDecimal(lblAvailableCredit.Text) - Convert.ToDecimal(txtLineTotAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCustomer.Text != "CASH")
                    //{
                    //    this.Cursor = Cursors.Default;
                    //    MessageBox.Show("Please check the customer's account balance", "Account Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(cmbBook.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbBook.Focus();
                        _isAdded = false;
                        return;
                    }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbLevel.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the item status", "Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbInvType.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {

                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    _isAdded = false;
                    return;

                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount %", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Focus();
                    _isAdded = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount amount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisAmt.Focus();
                    _isAdded = false;
                    return;
                }


                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the VAT amount", "Tax Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaxAmt.Focus();
                    _isAdded = false;
                    return;
                }
                #endregion

                //#region Scan By Serial - check for serial
                //if (string.IsNullOrEmpty(txtSerialNo.Text))
                //{
                //    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                //    {
                //        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                //        //Edt0001
                //        if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater.Checked == false && _priceBookLevelRef.Sapl_is_serialized))
                //        {
                //            this.Cursor = Cursors.Default;
                //            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            txtSerialNo.Focus();
                //            return;
                //        }
                //    }
                //}
                //#endregion

                #region Price Combine Checking Process - Costing Dept.
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

                            //if (CheckBlockItem(_mItem, SSPRomotionType))
                            //{
                            //    _isCheckedPriceCombine = false;
                            //    return;
                            //}

                            //var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
                            //if (_dupsMain != null)
                            //    if (_dupsMain.Count() > 0)
                            //    {
                            //        this.Cursor = Cursors.Default;
                            //        _isCheckedPriceCombine = false;
                            //        MessageBox.Show(_mItem + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        return;
                            //    }

                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItm = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;
                                string _status = cmbStatus.Text.Trim();
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;

                                //if (CheckBlockItem(_item, SSPRomotionType))
                                //{
                                //    _isCheckedPriceCombine = false;
                                //    break;
                                //}

                                //Updated by akila 2018/03/21 
                                //List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);

                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                if (_isStrucBaseTax == true)
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                {
                                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                                }

                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                { if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item; else _taxNotdefine += "," + _item; }

                                if (CheckItemWarranty(_item, cmbStatus.Text.Trim()))
                                { if (string.IsNullOrEmpty(_noWarrantySetup)) _noWarrantySetup = _item; else _noWarrantySetup += "," + _item; }

                                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                                //if ((chkDeliverLater.Checked == false && _isCheckedPriceCombine == false) || IsGiftVoucher(_itm.Mi_itm_tp))
                                //{
                                //    _isCheckedPriceCombine = true;

                                //    if (_itm.Mi_is_ser1 == 1)
                                //    {
                                //        var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);

                                //        if (_qty > _exist.Count())
                                //        { if (string.IsNullOrEmpty(_serialiNotpick)) _serialiNotpick = _item; else _serialiNotpick += "," + _item; }

                                //        foreach (ReptPickSerials _p in _exist)
                                //        {
                                //            string _serial = _p.Tus_ser_1;
                                //            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);

                                //            if (_dup != null)
                                //                if (_dup.Count() > 0)
                                //                {
                                //                    if (string.IsNullOrEmpty(_serialDuplicate)) _serialDuplicate = _item + "/" + _serial;
                                //                    else _serialDuplicate = "," + _item + "/" + _serial;
                                //                }
                                //        }
                                //    }

                                //    decimal _pickQty = 0;
                                //    if (IsPriceLevelAllowDoAnyStatus)
                                //        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum();
                                //    else
                                //        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                                //    _pickQty += Convert.ToDecimal(txtQty.Text.Trim());

                                //    List<InventoryLocation> _inventoryLocation = null;
                                //    if (IsPriceLevelAllowDoAnyStatus)
                                //        _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);
                                //    else
                                //        _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, cmbStatus.Text.Trim());

                                //    if (_inventoryLocation != null)
                                //        if (_inventoryLocation.Count > 0)
                                //        {
                                //            decimal _invBal = _inventoryLocation[0].Inl_qty;
                                //            if (_pickQty > _invBal)
                                //            {
                                //                if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                //                else _noInventoryBalance = "," + _item;
                                //            }
                                //        }
                                //        else
                                //        {
                                //            if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                //            else _noInventoryBalance = "," + _item;
                                //        }
                                //    else
                                //    {
                                //        if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item;
                                //        else _noInventoryBalance = "," + _item;
                                //    }
                                //}
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
                            //if (!string.IsNullOrEmpty(_noInventoryBalance) && !IsGiftVoucher(_itm.Mi_itm_tp))
                            //{
                            //    this.Cursor = Cursors.Default;
                            //    _isCheckedPriceCombine = false;
                            //    MessageBox.Show(_noInventoryBalance + " item(s) does not having inventory balance for release.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}

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

                if (_masterItemComponent != null && _masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
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
                    //updated by akila 2017/08/15
                    var _item_ = (from _n in _masterItemComponent where _n.Micp_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    //var _item_ = (from _n in _masterItemComponent where _n.ComponentItem.Mi_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();

                        //Updated by Akila - promotion price will be loaded by checking current date. if the to date is < valid date message will pop up and ask user to change the valid date.
                        _priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text), DateTime.Today.Date);
                        //_priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(dtpValidTo.Value));

                        if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_mItem + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }


                        if (_priceDetailRef.Count > 0)
                        {
                            DateTime _PriceValidDate = _priceDetailRef.Distinct().Select(x => x.Sapd_to_date).First();
                            if (_PriceValidDate.Date < dtpValidTo.Value.Date)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Selected item price will be expired on " + _PriceValidDate.Date.ToShortDateString() + Environment.NewLine + "If you want to continue with selected price please change the valid date", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dtpValidTo.Enabled = true;
                                _isInventoryCombineAdded = false;
                                _isAdded = false;
                                return;
                            }
                        }


                        if (_priceDetailRef.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(_item_[0].ToString() + " does not having price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _isInventoryCombineAdded = false;
                            _isAdded = false;
                            return;
                        }
                        else
                        {
                            //if (CheckBlockItem(_mItem, _priceDetailRef[0].Sapd_price_type))
                            //{
                            //    _isInventoryCombineAdded = false;
                            //    return;
                            //}

                            if (_priceDetailRef.Count == 1 && _priceDetailRef[0].Sapd_price_type != 0 && _priceDetailRef[0].Sapd_price_type != 4)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(_item_[0].ToString() + " price is available for only promotion. Complete code does not support for promotion", "Available Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isInventoryCombineAdded = false;
                                return;
                            }



                        }





                    }
                    #endregion

                    #region Sub Item Cheking for Warranty
                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }

                        //if (CheckBlockItem(_com.ComponentItem.Mi_cd, _priceDetailRef[0].Sapd_price_type))
                        //{
                        //    _isInventoryCombineAdded = false;
                        //    return;
                        //}

                    }
                    #endregion
                    #region Serial Check for Main and Sub Items
                    bool _isMainSerialCheck = false;
                    //if (ScanSerialList != null && chkDeliverLater.Checked == false)
                    //{
                    //    //check main item serial duplicates
                    //    if (ScanSerialList.Count > 0)
                    //    {
                    //        if (_isMainSerialCheck == false)
                    //        {

                    //            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
                    //            if (_dup != null)
                    //                if (_dup.Count() > 0)
                    //                {
                    //                    this.Cursor = Cursors.Default;
                    //                    MessageBox.Show(_item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                    _isInventoryCombineAdded = false;
                    //                    return;
                    //                }
                    //            _isMainSerialCheck = true;
                    //        }

                    //        //Check scan item duplicates


                    //        foreach (MasterItemComponent _com in _masterItemComponent)
                    //        {
                    //            string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
                    //            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);

                    //            if (_dup != null)
                    //                if (_dup.Count() > 0)
                    //                {
                    //                    this.Cursor = Cursors.Default;
                    //                    MessageBox.Show("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                    _isInventoryCombineAdded = false;
                    //                    return;
                    //                }
                    //        }
                    //    }

                    //}
                    #endregion

                    #endregion

                    #region Com item check for its serial status
                    //if (InventoryCombinItemSerialList.Count == 0)
                    //{
                    //    _isCombineAdding = true;
                    //    foreach (MasterItemComponent _com in _masterItemComponent)
                    //    {
                    //        List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);
                    //        if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    //        {
                    //            this.Cursor = Cursors.Default;
                    //            MessageBox.Show(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            _isInventoryCombineAdded = false;
                    //            return;
                    //        }


                    //        if (chkDeliverLater.Checked == false)
                    //        {
                    //            decimal _pickQty = 0;
                    //            if (IsPriceLevelAllowDoAnyStatus)
                    //                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                    //            else
                    //                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                    //            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                    //            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _com.ComponentItem.Mi_cd, cmbStatus.Text.Trim());

                    //            if (_inventoryLocation != null)
                    //                if (_inventoryLocation.Count > 0)
                    //                {
                    //                    decimal _invBal = _inventoryLocation[0].Inl_qty;
                    //                    if (_pickQty > _invBal)
                    //                    {
                    //                        this.Cursor = Cursors.Default;
                    //                        MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                        _isInventoryCombineAdded = false;
                    //                        return;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    this.Cursor = Cursors.Default;
                    //                    MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                    _isInventoryCombineAdded = false;
                    //                    return;
                    //                }
                    //            else
                    //            {
                    //                this.Cursor = Cursors.Default;
                    //                MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                _isInventoryCombineAdded = false;
                    //                return;
                    //            }
                    //        }



                    //        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);

                    //        if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
                    //        {
                    //            _comItem.Add(_com);
                    //        }
                    //    }

                    //    if (_comItem.Count > 1 && chkDeliverLater.Checked == false)
                    //    {//hdnItemCode.value
                    //        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                    //        if (_pick != null)
                    //            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                    //            {
                    //                var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                    //                if (_dup != null)
                    //                    if (_dup.Count <= 0)
                    //                    {
                    //                        InventoryCombinItemSerialList.Add(_pick);
                    //                    }
                    //            }

                    //        _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);

                    //        var _listComItem = (from _one in _comItem
                    //                            where _one.ComponentItem.Mi_itm_tp != "M"
                    //                            select new
                    //                            {
                    //                                Mi_cd = _one.ComponentItem.Mi_cd,
                    //                                Mi_longdesc = _one.ComponentItem.Mi_longdesc,
                    //                                Micp_itm_cd = _one.Micp_itm_cd,
                    //                                Micp_qty = _one.Micp_qty,
                    //                                Mi_itm_tp = _one.ComponentItem.Mi_itm_tp
                    //                            }).ToList();

                    //        gvPopComItem.DataSource = _listComItem;
                    //        pnlInventoryCombineSerialPick.Visible = true;
                    //        pnlMain.Enabled = false;
                    //        _isInventoryCombineAdded = false;
                    //        this.Cursor = Cursors.Default;
                    //        return;
                    //    }
                    //    else if (_comItem.Count == 1 && chkDeliverLater.Checked == false)
                    //    {//hdnItemCode.Value
                    //        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                    //        if (_pick != null)
                    //            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                    //            {
                    //                var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                    //                if (_dup != null)
                    //                    if (_dup.Count <= 0)
                    //                    {
                    //                        InventoryCombinItemSerialList.Add(_pick);
                    //                    }
                    //            }
                    //    }
                    //}


                    #endregion

                    #region  Adding Com items to grid after pick all items (non serialized added randomly,bt check it if deliver now!)
                    SSCombineLine += 1;
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        //If going to deliver now
                        //if (chkDeliverLater.Checked == false && InventoryCombinItemSerialList.Count > 0)
                        //{
                        //    var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                        //    if (_comItemSer != null)
                        //        if (_comItemSer.Count > 0)
                        //        {
                        //            foreach (ReptPickSerials _serItm in _comItemSer)
                        //            {
                        //                txtSerialNo.Text = _serItm.Tus_ser_1;
                        //                ScanSerialNo = txtSerialNo.Text;
                        //                //hdnSerialNo.Value = ScanSerialNo;
                        //                txtSerialNo.Text = ScanSerialNo;
                        //                txtItem.Text = _com.ComponentItem.Mi_cd;
                        //                //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        //                //LoadItemDetail(txtItem.Text);
                        //                cmbStatus.Text = _combineStatus;
                        //                txtQty.Text = FormatToQty("1");
                        //                CheckQty();
                        //                txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                        //                txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                        //                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
                        //                txtLineTotAmt.Text = FormatToCurrency("0");
                        //                CalculateItem();
                        //                AddItem(false, string.Empty);
                        //                ScanSerialNo = string.Empty;
                        //                txtSerialNo.Text = string.Empty;
                        //                //hdnSerialNo.Value = "";
                        //                txtSerialNo.Text = string.Empty;
                        //            }
                        //            _combineCounter += 1;
                        //        }
                        //        else
                        //        {
                        //            txtItem.Text = _com.ComponentItem.Mi_cd;
                        //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        //            //LoadItemDetail(txtItem.Text);
                        //            cmbStatus.Text = _combineStatus;
                        //            txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                        //            CheckQty();
                        //            txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                        //            txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                        //            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true)));
                        //            txtLineTotAmt.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            AddItem(false, string.Empty);
                        //            ScanSerialNo = string.Empty;
                        //            txtSerialNo.Text = string.Empty;
                        //            //hdnSerialNo.Value = "";
                        //            txtSerialNo.Text = string.Empty;

                        //            _combineCounter += 1;
                        //        }

                        //}
                        //If deliver later
                        //else if (chkDeliverLater.Checked && InventoryCombinItemSerialList.Count == 0)
                        //{
                        txtItem.Text = _com.ComponentItem.Mi_cd;
                        //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        LoadItemDetail(txtItem.Text.Trim());
                        cmbStatus.Text = _combineStatus;
                        txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                        CheckQty();
                        txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                        txtDisAmt.Text = FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100, true).ToString();
                        txtTaxAmt.Text = FigureRoundUp(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), true), true).ToString();
                        txtLineTotAmt.Text = FormatToCurrency("0");
                        CalculateItem();
                        AddItem(false, string.Empty);
                        _combineCounter += 1;
                        //}

                    }
                    #endregion

                    if (_combineCounter == _masterItemComponent.Count)
                    {
                        _masterItemComponent = new List<MasterItemComponent>();
                        _isCompleteCode = false; _isInventoryCombineAdded = false;
                        _isCombineAdding = false; //ScanSerialNo = string.Empty;
                        //InventoryCombinItemSerialList = new List<ReptPickSerials>();
                        //txtSerialNo.Text = string.Empty;

                        if (_isCombineAdding == false)
                        {
                            this.Cursor = Cursors.Default;

                            //txtSerialNo.Text = "";
                            ClearAfterAddItem();

                            SSPriceBookSequance = "0";
                            SSPriceBookItemSequance = "0";
                            SSPriceBookPrice = 0;
                            if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                            SSPRomotionType = 0;

                            txtItem.Focus();
                            BindAddItem();
                            SetDecimalTextBoxForZero(true);

                            //decimal _tobepay = 0;
                            //if (lblSVatStatus.Text == "Available")
                            //    _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                            //else
                            //    _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                            //ucPayModes1.TotalAmount = _tobepay;
                            //ucPayModes1.InvoiceItemList = _invoiceItemList;
                            //ucPayModes1.SerialList = InvoiceSerialList;
                            //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepay));
                            //if (ucPayModes1.HavePayModes)
                            //    ucPayModes1.LoadData();
                            this.Cursor = Cursors.Default;

                            if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                                _isAdded = true;
                                txtItem.Focus();

                            }

                        }
                        _isAdded = true;
                        return;
                    } //hdnSerialNo.Value = ""
                }

                #endregion

                //#region Check item with serial status & load particular serial details

                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                //bool _isAgePriceLevel = false;
                //int _noofDays = 0;
                //DateTime _serialpickingdate = txtDate.Value.Date;
                //CheckNValidateAgeItem(txtItem.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), out _isAgePriceLevel, out _noofDays);
                //if (_isAgePriceLevel) _serialpickingdate = _serialpickingdate.AddDays(-_noofDays);


                ////Edt0001
                //if (_priceBookLevelRef.Sapl_is_serialized)
                //{
                //    if (chkDeliverLater.Checked == false)
                //    {
                //        if (_itm.Mi_is_ser1 == 1)
                //        {
                //            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                //            {
                //                this.Cursor = Cursors.Default;
                //                MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                txtSerialNo.Focus();
                //                return;
                //            }
                //            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());

                //        }
                //        else if (_itm.Mi_is_ser1 == 0)
                //        {
                //            if (IsPriceLevelAllowDoAnyStatus == false)
                //                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                //            else
                //                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);

                //        }
                //    }
                //    else
                //    {
                //        if (_itm.Mi_is_ser1 == 1)
                //        {
                //            _serLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), txtSerialNo.Text.Trim())[0];
                //        }
                //        else if (_itm.Mi_is_ser1 == 0)
                //        {
                //            _nonserLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), string.Empty);
                //        }

                //    }
                //}
                //else if (chkDeliverLater.Checked == false || IsGiftVoucher(_itm.Mi_itm_tp))
                //{
                //    if (_itm.Mi_is_ser1 == 1)
                //    {
                //        if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                //        {
                //            this.Cursor = Cursors.Default;
                //            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            txtSerialNo.Focus();
                //            return;
                //        }

                //        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                //        if (!_isGiftVoucher)
                //            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                //        else
                //            _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);
                //    }
                //    else if (_itm.Mi_is_ser1 == 0)
                //    {
                //        if (IsPriceLevelAllowDoAnyStatus == false)
                //            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                //        else
                //            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);

                //    }
                //}

                //#endregion

                #region Check for fulfilment before adding
                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && _itmType != "V")
                {
                    if (!_isCombineAdding) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                }
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text) == 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                if (!_isCombineAdding)
                {
                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty, _mstItem.Mi_anal1);
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

                //if (CheckBlockItem(txtItem.Text.Trim(), SSPRomotionType))
                //    return;

                if (_isCombineAdding == false && _itmType != "V")
                {
                    //Divide this to 2 parts, 
                    //  1. If serialized level, check from serialized price table
                    //  2. Else current rooting is ok.

                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));

                    //Updated by Akila - promotion price will be loaded by checking current date. if the to date is < valid date message will pop up and ask user to change the valid date.
                    if (_lsts != null)
                    {
                        if (gvPromotionPrice.Rows.Count > 0)
                        {
                            string _selectedPromoCode = string.Empty;
                            foreach (DataGridViewRow _row in gvPromotionPrice.Rows)
                            {
                                if (Convert.ToBoolean(_row.Cells["PromPrice_Select"].Value) == true)
                                {
                                    _selectedPromoCode = _row.Cells["PromPrice_PromotionCD"].Value.ToString();
                                }
                            }

                            if (_priceDetailRef.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(_selectedPromoCode))
                                {
                                    var _tmpPrice = _priceDetailRef.Where(x => x.Sapd_promo_cd == _selectedPromoCode).Select(x => x.Sapd_to_date).ToList();
                                    if (_tmpPrice != null && _tmpPrice.Count > 0)
                                    {
                                        DateTime _PriceValidDate = _tmpPrice.First();
                                        if (_PriceValidDate.Date < dtpValidTo.Value.Date)
                                        {
                                            this.Cursor = Cursors.Default;
                                            MessageBox.Show("Selected item price will be expired on " + _PriceValidDate.Date.ToShortDateString() + Environment.NewLine + "If you want to continue with selected price please change the valid date", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            dtpValidTo.Enabled = true;
                                            _isInventoryCombineAdded = false;
                                            _isAdded = false;
                                            return;
                                        }
                                    }
                                    //DateTime _PriceValidDate = _priceDetailRef.Where(x => x.Sapd_promo_cd == _selectedPromoCode).Select(x => x.Sapd_to_date).First();
                                    //if (_PriceValidDate.Date < dtpValidTo.Value.Date)
                                    //{
                                    //    this.Cursor = Cursors.Default;
                                    //    MessageBox.Show("Selected item price will be expired on " + _PriceValidDate.Date.ToShortDateString() + Environment.NewLine + "If you want to continue with selected price please change the valid date", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //    dtpValidTo.Enabled = true;
                                    //    _isInventoryCombineAdded = false;
                                    //    _isAdded = false;
                                    //    return;
                                    //}
                                }
                            }
                        }
                        else
                        {
                            DateTime _PriceValidDate = _priceDetailRef.Distinct().Select(x => x.Sapd_to_date).First();
                            if (_PriceValidDate.Date < dtpValidTo.Value.Date)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Selected item price will be expired on " + _PriceValidDate.Date.ToShortDateString() + Environment.NewLine + "If you want to continue with selected price please change the valid date", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dtpValidTo.Enabled = true;
                                _isInventoryCombineAdded = false;
                                _isAdded = false;
                                return;
                            }
                        }

                        //DateTime _PriceValidDate = _priceDetailRef.Distinct().Select(x => x.Sapd_to_date).First();
                        //if (_PriceValidDate.Date < dtpValidTo.Value.Date)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    MessageBox.Show("Selected item price will be expired on " + _PriceValidDate.Date.ToShortDateString() + Environment.NewLine + "If you want to continue with selected price please change the valid date", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    dtpValidTo.Enabled = true;
                        //    _isInventoryCombineAdded = false;
                        //    _isAdded = false;
                        //    return;
                        //}
                    }

                    if (_lsts != null && _isCombineAdding == false)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isAdded = false;
                            return;
                        }
                        else
                        {
                            decimal sysUPrice = 0;
                            if (_priceBookLevelRef.Sapl_vat_calc == true)
                            {
                                sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * MainTaxConstant[0].Mict_tax_rate, true);
                            }
                            else
                            {
                                sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price, true);
                            }
                            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);

                            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                        if (!_MasterProfitCenter.Mpc_edit_price)
                                        {
                                            if (Math.Round(sysUPrice, 2) != Math.Round(pickUPrice, 2))
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
                        if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized == false)
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
                    //if (chkDeliverLater.Checked == false)
                    //{
                    //    if (_itm.Mi_is_ser1 == 1)
                    //    {
                    //        var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text && x.Tus_ser_1 == ScanSerialNo).ToList();
                    //        if (_dup != null)
                    //            if (_dup.Count > 0)
                    //            {
                    //                this.Cursor = Cursors.Default;
                    //                MessageBox.Show(ScanSerialNo + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                txtSerialNo.Focus();
                    //                return;
                    //            }
                    //    }

                    //    if (!IsPriceLevelAllowDoAnyStatus)
                    //    {
                    //        if (_serLst != null)
                    //            if (string.IsNullOrEmpty(_serLst.Tus_com))
                    //            {
                    //                if (_serLst.Tus_itm_stus != cmbStatus.Text.Trim())
                    //                {
                    //                    this.Cursor = Cursors.Default;
                    //                    MessageBox.Show(ScanSerialNo + " serial status is not match with the price level status", "Price Level Restriction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                    txtSerialNo.Focus();
                    //                    return;
                    //                }
                    //            }
                    //    }

                    //}
                #endregion

                    CalculateItem();

                #region Check Inventory Balance if deliver now!

                //check balance ----------------------
                //if (_isCombineAdding == false)
                //if (chkDeliverLater.Checked == false)
                //{
                //    decimal _pickQty = 0;
                //    if (IsPriceLevelAllowDoAnyStatus)
                //        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                //    else
                //        _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                //    _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                //    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());

                //    if (_inventoryLocation != null)
                //        if (_inventoryLocation.Count > 0)
                //        {
                //            decimal _invBal = _inventoryLocation[0].Inl_qty;
                //            if (_pickQty > _invBal)
                //            {
                //                this.Cursor = Cursors.Default;
                //                MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            this.Cursor = Cursors.Default;
                //            MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //    else
                //    {
                //        this.Cursor = Cursors.Default;
                //        MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }


                //    if (_itm.Mi_is_ser1 == 1 && ScanSerialList.Count > 0)
                //    {
                //        var _serDup = (from _lst in ScanSerialList
                //                       where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.Trim()
                //                       select _lst).ToList();

                //        if (_serDup != null)
                //            if (_serDup.Count > 0)
                //            {
                //                this.Cursor = Cursors.Default;
                //                MessageBox.Show("Selected Serial is duplicating.", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //    }
                //}
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
                                if (_lst[0].Sapl_set_warr == true)
                                {
                                    WarrantyPeriod = _lst[0].Sapl_warr_period;
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
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                }
                else
                //Having some records
                {
                    var _similerItem = from _list in _invoiceItemList
                                       where _list.Qd_itm_cd == txtItem.Text && _list.Qd_itm_stus == cmbStatus.Text && _list.Qd_pbook == cmbBook.Text && _list.Qd_pb_lvl == cmbLevel.Text && _list.Qd_unit_price == Convert.ToDecimal(txtUnitPrice.Text)
                                       select _list;

                    if (_similerItem.Count() > 0)
                    //Similar item available
                    {
                        _isDuplicateItem = true;
                        foreach (var _similerList in _similerItem)
                        {
                            _duplicateComLine = _similerList.Qd_citm_line;
                            _duplicateItmLine = _similerList.Qd_line_no;
                            _similerList.Qd_dis_amt = Convert.ToDecimal(_similerList.Qd_dis_amt) + Convert.ToDecimal(txtDisAmt.Text);
                            _similerList.Qd_itm_tax = Convert.ToDecimal(_similerList.Qd_itm_tax) + Convert.ToDecimal(txtTaxAmt.Text);
                            _similerList.Qd_frm_qty = Convert.ToDecimal(_similerList.Qd_frm_qty) + Convert.ToDecimal(txtQty.Text);
                            _similerList.Qd_to_qty = Convert.ToDecimal(_similerList.Qd_to_qty) + Convert.ToDecimal(txtQty.Text);
                            _similerList.Qd_tot_amt = Convert.ToDecimal(_similerList.Qd_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);

                        }
                    }
                    else
                    //No similar item found
                    {
                        _isDuplicateItem = false;
                        _lineNo += 1;
                        if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                    }

                }
                //Adding Items to grid end here ----------------------------------------------------------------------
                #endregion

                //#region Adding Serial/Non Serial items
                ////Scan By Serial ----------start----------------------------------
                //if (chkDeliverLater.Checked == false || _priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp))
                //{
                //    if (_isFirstPriceComItem)
                //        _isCombineAdding = true;

                //    if (ScanSequanceNo == 0) ScanSequanceNo = -100;

                //    //Serialized
                //    if (_itm.Mi_is_ser1 == 1)
                //    {

                //        //ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                //        _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                //        _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                //        _serLst.Tus_usrseq_no = ScanSequanceNo;
                //        _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                //        _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                //        _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                //        _serLst.ItemType = _itm.Mi_itm_tp;
                //        ScanSerialList.Add(_serLst);
                //    }

                //    //Non-Serialized but serial ID 8523
                //    if (_itm.Mi_is_ser1 == 0)
                //    {
                //        //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                //        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                //        {
                //            if (_isAgePriceLevel == false)
                //            {
                //                this.Cursor = Cursors.Default;
                //                MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                //                foreach (InvoiceItem _one in _partly)
                //                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                //                return;
                //            }
                //            else
                //            {
                //                this.Cursor = Cursors.Default;
                //                if (gvInvoiceItem.Rows.Count > 0) MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with inventory/costing dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                //                foreach (InvoiceItem _one in _partly)
                //                    // int _rownum = Convert.ToInt32(from DataGridViewRow _r in gvInvoiceItem.Rows where Convert.ToInt32(_r.Cells[""].Value) == _one.Sad_itm_line select _r.Index);
                //                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                //                return;
                //            }
                //        }
                //        _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                //        _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                //        _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                //        _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                //        _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                //        _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                //        _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                //        _nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                //        ScanSerialList.AddRange(_nonserLst);
                //    }

                //    gvPopSerial.DataSource = new List<ReptPickSerials>();
                //    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                //    //gvGiftVoucher.DataSource = new List<ReptPickSerials>();
                //    //gvGiftVoucher.DataSource = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();
                //    var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());
                //    gvGiftVoucher.DataSource = filenamesList;

                //    if (_isFirstPriceComItem)
                //    {
                //        _isCombineAdding = false;
                //        _isFirstPriceComItem = false;
                //    }

                //    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;
                //}
                ////Scan By Serial ----------end----------------------------------
                //#endregion

                #region Add Invoice Serial Detail
                //if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                //{
                bool _isDuplicate = false;
                //if (InvoiceSerialList != null)
                //    if (InvoiceSerialList.Count > 0)
                //    {
                //        if (_itm.Mi_is_ser1 == 1)
                //        {//hdnItemCode.Value
                //            var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.Trim() select _i).ToList();
                //            if (_dup != null)
                //                if (_dup.Count > 0)
                //                    _isDuplicate = true;
                //        }
                //    }

                //if (_isDuplicate == false)
                //{
                //    //hdnItemCode.Value.ToString()
                //    InvoiceSerial _invser = new InvoiceSerial();
                //    _invser.Sap_del_loc = BaseCls.GlbUserDefLoca;
                //    _invser.Sap_itm_cd = txtItem.Text.Trim();
                //    _invser.Sap_itm_line = _lineNo;
                //    _invser.Sap_remarks = string.Empty;
                //    _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                //    _invser.Sap_ser_1 = txtSerialNo.Text;
                //    _invser.Sap_ser_2 = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                //    InvoiceSerialList.Add(_invser);
                //}
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
                        //if (chkDeliverLater.Checked == true)
                        //{
                        foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                        {
                            string _originalItm = _list.Sapc_itm_cd;
                            string _similerItem = _list.Similer_item;
                            if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                            //if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                            LoadItemDetail(txtItem.Text.Trim());
                            //if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                            //{
                            //    foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.Trim()).ToList())
                            //    {
                            //        txtSerialNo.Text = _lists.Tus_ser_1;
                            //        ScanSerialNo = _lists.Tus_ser_1;
                            //        string _originalItms = _lists.Tus_session_id;

                            //        if (string.IsNullOrEmpty(_originalItm))
                            //        {
                            //            txtItem.Text = _lists.Tus_itm_cd;
                            //            _serial2 = _lists.Tus_ser_2;
                            //            _prefix = _lists.Tus_ser_3;
                            //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                            //            LoadItemDetail(txtItem.Text.Trim());
                            //            cmbStatus.Text = _combineStatus;
                            //            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                            //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                            //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                            //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                            //            txtDisRate.Text = FormatToCurrency("0");
                            //            txtDisAmt.Text = FormatToCurrency("0");
                            //            txtTaxAmt.Text = FormatToCurrency("0");
                            //            txtLineTotAmt.Text = FormatToCurrency("0");
                            //            CalculateItem();
                            //            AddItem(_isPromotion, string.Empty);//todo add temppickserial similer item and swap the similer item
                            //        }
                            //        else
                            //        {
                            //            txtItem.Text = _lists.Tus_itm_cd;
                            //            _serial2 = _lists.Tus_ser_2;
                            //            _prefix = _lists.Tus_ser_3;
                            //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                            //            LoadItemDetail(txtItem.Text.Trim());
                            //            cmbStatus.Text = _combineStatus;
                            //            //decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Similer_item == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                            //            //decimal Qty = _MainPriceCombinItem.Where(x => x.Similer_item == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                            //            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                            //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                            //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                            //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                            //            txtDisRate.Text = FormatToCurrency("0");
                            //            txtDisAmt.Text = FormatToCurrency("0");
                            //            txtTaxAmt.Text = FormatToCurrency("0");
                            //            txtLineTotAmt.Text = FormatToCurrency("0");
                            //            CalculateItem();
                            //            AddItem(_isPromotion, _originalItm);//todo add temppickserial similer item and swap the similer item
                            //        }
                            //        _combineCounter += 1;
                            //    }
                            //}
                            //else
                            //{
                            cmbStatus.Text = _combineStatus;
                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                            txtDisRate.Text = FormatToCurrency("0");
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtTaxAmt.Text = FormatToCurrency("0");
                            txtLineTotAmt.Text = FormatToCurrency("0");
                            CalculateItem();
                            AddItem(_isPromotion, _originalItm);
                            _combineCounter += 1;
                            //}
                        }
                        //}
                        //else
                        //{

                        //    if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0) _isSingleItemSerializedInCombine = false;

                        //    foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                        //    {
                        //        txtSerialNo.Text = _list.Tus_ser_1;
                        //        ScanSerialNo = _list.Tus_ser_1;
                        //        string _originalItm = _list.Tus_session_id;

                        //        if (string.IsNullOrEmpty(_originalItm))
                        //        {
                        //            txtItem.Text = _list.Tus_itm_cd;
                        //            _serial2 = _list.Tus_ser_2;
                        //            _prefix = _list.Tus_ser_3;
                        //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        //            LoadItemDetail(txtItem.Text.Trim());
                        //            cmbStatus.Text = _combineStatus;
                        //            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                        //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                        //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        //            txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                        //            txtDisRate.Text = FormatToCurrency("0");
                        //            txtDisAmt.Text = FormatToCurrency("0");
                        //            txtTaxAmt.Text = FormatToCurrency("0");
                        //            txtLineTotAmt.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            AddItem(_isPromotion, string.Empty);//todo add temppickserial similer item and swap the similer item
                        //        }
                        //        else
                        //        {
                        //            txtItem.Text = _list.Tus_itm_cd;
                        //            _serial2 = _list.Tus_ser_2;
                        //            _prefix = _list.Tus_ser_3;
                        //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        //            LoadItemDetail(txtItem.Text.Trim());
                        //            cmbStatus.Text = _combineStatus;
                        //            //decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Similer_item == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                        //            //decimal Qty = _MainPriceCombinItem.Where(x => x.Similer_item == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                        //            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                        //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                        //            var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                        //            bool _isIncreaseable = Convert.ToBoolean(_Increaseable[0]);
                        //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        //            if (_isIncreaseable) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                        //            txtDisRate.Text = FormatToCurrency("0");
                        //            txtDisAmt.Text = FormatToCurrency("0");
                        //            txtTaxAmt.Text = FormatToCurrency("0");
                        //            txtLineTotAmt.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            AddItem(_isPromotion, _originalItm);//todo add temppickserial similer item and swap the similer item
                        //        }
                        //        _combineCounter += 1;
                        //    }

                        //    foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                        //    {
                        //        MasterItem _i = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _list.Sapc_itm_cd);

                        //        if (_i.Mi_is_ser1 != 1)
                        //        {
                        //            string _originalItm = _list.Sapc_itm_cd;
                        //            string _similerItem = _list.Similer_item;
                        //            if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                        //            //txtItem.Text = _list.Sapc_itm_cd;
                        //            //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        //            LoadItemDetail(txtItem.Text.Trim());
                        //            cmbStatus.Text = _combineStatus;
                        //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                        //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                        //            txtDisRate.Text = FormatToCurrency("0");
                        //            txtDisAmt.Text = FormatToCurrency("0");
                        //            txtTaxAmt.Text = FormatToCurrency("0");
                        //            txtLineTotAmt.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            AddItem(_isPromotion, _originalItm);
                        //            _combineCounter += 1;
                        //        }
                        //    }

                        //}

                        //if (chkDeliverLater.Checked == true)
                        //if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); _isCombineAdding = false; SSPromotionCode = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { txtItem.Focus(); _isAdded = true; } } _isAdded = true; return; }//hdnSerialNo.Value = ""
                        //if (chkDeliverLater.Checked == false)
                        //{
                        //    if (_isSingleItemSerializedInCombine)
                        //    {
                        //        if (_combineCounter == PriceCombinItemSerialList.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        //    }
                        //    else
                        //        if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        //}
                    }


                }
                #endregion

                //txtSerialNo.Text = "";
                ClearAfterAddItem();

                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                SSPRomotionType = 0;

                txtItem.Focus();
                BindAddItem();
                SetDecimalTextBoxForZero(true);

                decimal _tobepays = 0;
                //if (lblSVatStatus.Text == "Available")
                //    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                //else
                //    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                //ucPayModes1.TotalAmount = _tobepays;
                //ucPayModes1.InvoiceItemList = _invoiceItemList;
                //ucPayModes1.SerialList = InvoiceSerialList;
                //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                //if (ucPayModes1.HavePayModes)
                //    ucPayModes1.LoadData();
                //LookingForBuyBack();
                this.Cursor = Cursors.Default;
                if (_isCombineAdding == false)
                {
                    this.Cursor = Cursors.Default;

                    txtItem.Focus();

                }
                _isAdded = true;
            }
            catch (Exception ex)
            {
                _isAdded = false;
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (_isAdded) { dtpValidTo.Enabled = false; } else { dtpValidTo.Enabled = true; }
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ClearAfterAddItem()
        {
            txtItem.Text = "";
            txtModel.Text = "";
            cmbStatus.Text = "";
            txtQty.Text = FormatToQty("1");
            LoadItemDetail(string.Empty);
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            txtItem.ReadOnly = false;
        }

        protected void BindAddItem()
        {
            dgItem.AutoGenerateColumns = false;
            dgItem.DataSource = new List<QoutationDetails>();
            dgItem.DataSource = _invoiceItemList;
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

        private QoutationDetails AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            QoutationDetails _tempItem = new QoutationDetails();
            _tempItem.Qd_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Qd_cbatch_line = 0;
            _tempItem.Qd_cdoc_no = Convert.ToString(SSPRomotionType);
            _tempItem.Qd_citm_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Qd_cost_amt = 0;
            _tempItem.Qd_dis_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Qd_dit_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Qd_frm_qty = Convert.ToDecimal(txtQty.Text);
            if (_item.Mi_anal4 == true)
            {
                _tempItem.Qd_issue_qty = Convert.ToDecimal(txtQty.Text);
            }
            else
            {
                _tempItem.Qd_issue_qty = 0;
            }
            _tempItem.Qd_itm_cd = txtItem.Text;
            _tempItem.Qd_itm_desc = _item.Mi_longdesc;
            _tempItem.Qd_itm_stus = cmbStatus.Text;
            _tempItem.Qd_itm_tax = Convert.ToDecimal(txtTaxAmt.Text);
            _tempItem.Qd_line_no = _lineNo;
            _tempItem.Qd_nitm_cd = null;
            _tempItem.Qd_nitm_desc = null;
            _tempItem.Qd_no = "";
            _tempItem.Qd_pb_lvl = cmbLevel.Text;
            _tempItem.Qd_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Qd_pb_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Qd_pbook = cmbBook.Text;
            _tempItem.Qd_quo_tp = "R";
            _tempItem.Qd_res_no = null;
            _tempItem.Qd_res_qty = 0;
            _tempItem.Qd_resbal_qty = 0;
            _tempItem.Qd_resitm_cd = txtModel.Text;
            _tempItem.Qd_resline_no = 0;
            _tempItem.Qd_resreq_no = null;
            _tempItem.Qd_seq_no = 0;
            _tempItem.Qd_to_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Qd_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            _tempItem.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);

            string _NormalPb = "";
            string _NormalLvl = "";

            MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            _NormalPb = _mastercompany.Mc_anal7;
            _NormalLvl = _mastercompany.Mc_anal8;

            if (Convert.ToDecimal(txtUnitPrice.Text) > 0)
            {
                List<PriceDetailRef> _NormalPrice = new List<PriceDetailRef>();
                _NormalPrice = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _NormalPb, _NormalLvl, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(dtpValidTo.Value));

                if (_NormalPrice.Count <= 0)
                {

                    _tempItem.Qd_unit_cost = 0;

                }
                else
                {


                    List<PriceDetailRef> _new = _NormalPrice;
                    _NormalPrice = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                            _NormalPrice.Add(_p[0]);



                    if (_NormalPrice != null && _NormalPrice.Count > 0)
                    {
                        var _isSuspend = _NormalPrice.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            _tempItem.Qd_unit_cost = 0;
                        }
                        else
                        {
                            foreach (PriceDetailRef _tmp in _NormalPrice)
                            {
                                PriceBookLevelRef _LevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _NormalPb, _NormalLvl);
                                decimal _vatPortion = 0;
                                _vatPortion = FigureRoundUp(TaxCalculationForNorPrice(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _LevelRef, Convert.ToDecimal(_tmp.Sapd_itm_price), 0, true), true);
                                _vatPortion = _vatPortion / Convert.ToDecimal(txtQty.Text);
                                _tempItem.Qd_unit_cost = FigureRoundUp(_tmp.Sapd_itm_price + _vatPortion, true);
                            }
                        }
                    }

                }
            }
            else
            {
                _tempItem.Qd_unit_cost = 0;
            }
            _tempItem.Qd_uom = _item.Mi_itm_uom;
            _tempItem.Qd_warr_rmk = WarrantyRemarks;
            _tempItem.Qd_warr_pd = WarrantyPeriod;

            //kapila 11/3/2016
            _tempItem.Qd_quo_base = _isQuoBase;
            if (_isQuoBase == 1)
                _isSelQuoBaseLevel = 1;



            //_tempItem.Sad_alt_itm_cd = "";
            //_tempItem.Sad_alt_itm_desc = "";
            //_tempItem.Sad_comm_amt = 0;
            //_tempItem.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
            //_tempItem.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
            //_tempItem.Sad_do_qty = 0;
            //_tempItem.Sad_inv_no = "";
            //_tempItem.Sad_is_promo = _isPromotion;
            //_tempItem.Sad_itm_cd = txtItem.Text;
            //_tempItem.Sad_itm_line = _lineNo;
            //_tempItem.Sad_itm_seq = Convert.ToInt32(SSPriceBookItemSequance);
            //_tempItem.Sad_itm_stus = cmbStatus.Text;
            //_tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            //_tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            //_tempItem.Sad_job_no = "";
            //_tempItem.Sad_merge_itm = "";
            //_tempItem.Sad_pb_lvl = cmbLevel.Text;
            //_tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            //_tempItem.Sad_pbook = cmbBook.Text;
            //_tempItem.Sad_print_stus = false;
            //_tempItem.Sad_promo_cd = SSPromotionCode;
            //_tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_res_line_no = 0;
            //_tempItem.Sad_res_no = "";
            //_tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            //_tempItem.Sad_seq_no = 0;
            //_tempItem.Sad_srn_qty = 0;
            //_tempItem.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            //_tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            //_tempItem.Sad_uom = "";
            //_tempItem.Sad_warr_based = false;
            //_tempItem.Mi_longdesc = _item.Mi_longdesc;
            //_tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            //_tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            //_tempItem.Sad_warr_period = WarrantyPeriod;
            //_tempItem.Sad_warr_remarks = WarrantyRemarks;
            //_tempItem.Sad_sim_itm_cd = _originalItem;
            //_tempItem.Sad_merge_itm = Convert.ToString(SSPRomotionType);

            return _tempItem;
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

        private void RemoveControls()
        {
            try
            {
                for (int i = 1; i < _recordCount + 1; i++)
                {
                    pnlMsg.Controls.Remove(lblCriteria[i]);
                    pnlOk.Controls.Remove(btnOk[i]);
                    pnlFail.Controls.Remove(btnFail[i]);
                    pnlVal.Controls.Remove(lblVal[i]);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void AddItem(Object sender, EventArgs e)
        {
            //kapila 8/7/2016

            Boolean _isFailFound = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;

            }

            AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);

            //dtpValidTo.Enabled = false;  - commented by akila

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

            return _reqNo;
        }

        private void btnSearch_dCus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDCusCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtDCusCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                    DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                    _CommonSearch.ShowDialog();
                    txtInvoiceNo.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCustomer.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Quotation") == txtInvoiceNo.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid quotation", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoiceNo.Clear();
                    txtInvoiceNo.Focus();
                    return;
                }

                load_save_Quotation();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void load_save_Quotation()
        {
            try
            {
                QuotationHeader _saveHdr = new QuotationHeader();

                _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtInvoiceNo.Text);

                if (_saveHdr != null)
                {
                    quoSeq = _saveHdr.Qh_seq_no;
                    txtDate.Text = _saveHdr.Qh_dt.ToShortDateString();
                    txtDocRefNo.Text = _saveHdr.Qh_ref;
                    txtCustomer.Text = _saveHdr.Qh_party_cd;
                    txtCusName.Text = _saveHdr.Qh_party_name;
                    txtAddress1.Text = _saveHdr.Qh_add1;
                    txtAddress2.Text = _saveHdr.Qh_add2;
                    txtMobile.Text = _saveHdr.Qh_mobi;
                    chkTaxPayable.Checked = _saveHdr.Qh_is_tax;
                    txtDCusCode.Text = _saveHdr.Qh_del_cuscd;
                    txtdCusName.Text = _saveHdr.Qh_del_cusname;
                    txtDCusAdd1.Text = _saveHdr.Qh_del_cusadd1;
                    txtDCusAdd2.Text = _saveHdr.Qh_del_cusadd2;
                    txtDNic.Text = _saveHdr.Qh_del_cusid;
                    txtDMob.Text = _saveHdr.Qh_del_custel;
                    txtDFax.Text = _saveHdr.Qh_del_cusfax;
                    txtRemarks.Text = _saveHdr.Qh_remarks;
                    txtPaymentTerm.Text = _saveHdr.Qh_anal_2;
                    txtAddWara.Text = _saveHdr.Qh_add_wararmk;
                    cmbExecutive.SelectedValue = _saveHdr.Qh_sales_ex;
                    chkRes.Checked = Convert.ToBoolean(_saveHdr.Qh_anal_5);
                    lblStus.Text = _saveHdr.Qh_stus == "A" ? "Active" : "Cancelled";
                    _qh_jobno = _saveHdr.Qh_jobno;
                    _QH_ANAL_4 = _saveHdr.Qh_anal_4;

                    List<QoutationDetails> _recallList = new List<QoutationDetails>();
                    _recallList = CHNLSVC.Sales.Get_all_linesForQoutation(txtInvoiceNo.Text);
                    _invoiceItemList = _recallList.ToList();
                    dgItem.AutoGenerateColumns = false;
                    dgItem.DataSource = new List<QoutationDetails>();
                    dgItem.DataSource = _recallList;

                    List<QuotationSerial> _recallSerList = new List<QuotationSerial>();
                    _recallSerList = CHNLSVC.Sales.GetQuoSerials(txtInvoiceNo.Text);

                    dgvSerial.AutoGenerateColumns = false;
                    dgvSerial.DataSource = new List<QuotationSerial>();
                    dgvSerial.DataSource = _recallSerList;

                    //btnSave.Enabled = false; // Nadeeka 25-08-2015 (Need to update quotation )
                }
                else
                {
                    MessageBox.Show("Invalid quotation.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear_Data();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void txtDCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDCusCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtDCusCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtDCusCode;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtDCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtdCusName.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchEngine_Click(object sender, EventArgs e)
        {
            DataTable _dtSysPara = new DataTable();
            Boolean _isFailFound = false;
            try
            {
                if (string.IsNullOrEmpty(lblItem.Text))
                {
                    MessageBox.Show("Please select item first.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (chkWH.Checked && string.IsNullOrEmpty(txtWH.Text))
                {
                    MessageBox.Show("Please select the warehouse code", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWH.Focus();
                    return;
                }

                MasterItem _mstItm = new MasterItem();
                if (chkWH.Checked)
                {
                    _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, lblItem.Text);
                    if (_mstItm.Mi_cd != null)
                        if (BaseCls.GlbUserComCode == "AAL" && _mstItm.Mi_cate_1 == "MC")
                        {
                            _dtSysPara = CHNLSVC.Sales.SP_CHECK_MST_SYS_PARA(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, lblItem.Text);
                            DataRow[] result = _dtSysPara.Select("para_stus = 1");
                            if (result.Length > 0)
                            {
                                RemoveControls();

                                LoadCriteria(_dtSysPara.Rows.Count);
                                LoadValue(_dtSysPara.Rows.Count);
                                LoadOk(_dtSysPara.Rows.Count);
                                LoadFail(_dtSysPara.Rows.Count);

                                _recordCount = _dtSysPara.Rows.Count;

                                for (int i = 0; i < _dtSysPara.Rows.Count; i++)
                                {
                                    lblCriteria[i + 1].Text = _dtSysPara.Rows[i]["para_crit"].ToString();
                                    lblVal[i + 1].Text = _dtSysPara.Rows[i]["PARA_ALW_VAL"].ToString();
                                    if (Convert.ToInt32(_dtSysPara.Rows[i]["para_stus"]) == 1)
                                    {
                                        btnFail[i + 1].Visible = true;
                                        _isFailFound = true;
                                    }
                                    else
                                        btnOk[i + 1].Visible = true;
                                }

                            }
                            if (_isFailFound == true)
                            {
                                pnlMsg.Visible = true;
                                groupBox1.Enabled = false;
                                return;
                            }
                        }
                }

                DataTable _result = null;

                if (chkWH.Checked)   //SCM
                {
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialSCM);
                    //_result = CHNLSVC.CommonSearch.GetAvailableSerialSCM(_CommonSearch.SearchParams, null, null);
                    DataTable _dtItmStus = CHNLSVC.Inventory.GetItemStatusMaster(lblItmStus.Text, null);
                    _result = CHNLSVC.Inventory.getNextSerialSCM(BaseCls.GlbUserComCode, txtWH.Text, lblItem.Text, _dtItmStus.Rows[0]["mis_old_cd"].ToString());
                    if (_result.Rows.Count > 0)
                    {
                        int _x = dgvSerialSCM.Rows.Count;
                        txtengine.Text = _result.Rows[_x]["serial_no"].ToString();
                        txtChasis.Text = _result.Rows[_x]["chassis_no"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Available Engine/Serial not found", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                    _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtengine;
                    _CommonSearch.ShowDialog();
                    txtengine.Select();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtengine_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblItem.Text))
                {
                    MessageBox.Show("Please select item first.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtengine;
                _CommonSearch.ShowDialog();
                txtengine.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtengine_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(lblItem.Text))
                    {
                        MessageBox.Show("Please select item first.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtengine;
                    _CommonSearch.ShowDialog();
                    txtengine.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSerial.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtengine_Leave(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtengine.Text))
                {
                    return;
                }

                ReptPickSerials _serialList = new ReptPickSerials();
                if (chkWH.Checked == false)
                {
                    _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblItem.Text.Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                    if (_serialList.Tus_ser_1 != null)
                    {
                        txtengine.Text = _serialList.Tus_ser_1;
                        _serID = _serialList.Tus_ser_id;
                        if (_serialList.Tus_ser_2 != null)
                        {
                            txtChasis.Text = _serialList.Tus_ser_2;
                        }
                        else
                        {
                            txtChasis.Text = "";
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid serials.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtengine.Text = "";
                        txtChasis.Text = "";
                        _serID = 0;
                        txtengine.Focus();
                        return;
                    }
                }
                else
                {
                    DataTable _dtInvSer = CHNLSVC.Inventory.IsValid_SCM_Serial(BaseCls.GlbUserComCode, txtWH.Text, lblItem.Text, txtengine.Text);
                    if (_dtInvSer.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid serials.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtengine.Text = "";
                        txtChasis.Text = "";
                        txtengine.Focus();
                        return;
                    }
                    else
                    {
                        if (_dtInvSer.Rows[0]["chassis_no"].ToString() != null)
                        {
                            txtChasis.Text = _dtInvSer.Rows[0]["chassis_no"].ToString();
                        }
                        else
                        {
                            txtChasis.Text = "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            try
            {
                cmbInvType.Enabled = true;
                if (string.IsNullOrEmpty(lblItem.Text))
                {
                    MessageBox.Show("Please select item.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(txtengine.Text))
                {
                    MessageBox.Show("Please select serial #.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //if (string.IsNullOrEmpty(txtChasis.Text))
                //{
                //    MessageBox.Show("Please select chasiss.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (string.IsNullOrEmpty(lblLine.Text))
                {
                    MessageBox.Show("Please select item.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //kapila 13/7/2016
                if (chkWH.Checked && string.IsNullOrEmpty(txtWH.Text))
                {
                    MessageBox.Show("Please select the warehouse.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (chkWH.Checked)  //kapila 13/7/2016
                {
                    var _record = (from _lst in _QuoSerialsSCM
                                   where _lst.Qs_item == lblItem.Text.Trim() && _lst.Qs_ser == txtengine.Text.Trim() && _lst.Qs_chassis == txtChasis.Text.Trim()
                                   select _lst.Qs_item).ToList();

                    if (_record.Count > 0)
                    {
                        MessageBox.Show("The serial is already selected.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Int32 _qty = 0;
                    foreach (QuotationSerial _tmp in _QuoSerialsSCM)
                    {
                        if (_tmp.Qs_item == lblItem.Text && _tmp.Qs_main_line == Convert.ToInt16(lblLine.Text))
                        {
                            _qty = _qty + 1;
                        }
                    }

                    decimal _orgQty = 0;
                    _orgQty = Convert.ToDecimal(lblQty.Text);

                    if (Convert.ToInt16(_orgQty) <= _qty)
                    {
                        MessageBox.Show("Qty mismatch.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    QuotationSerial _addSer = new QuotationSerial();
                    _addSer.Qs_chassis = txtChasis.Text;
                    _addSer.Qs_item = lblItem.Text;
                    _addSer.Qs_main_line = Convert.ToInt16(lblLine.Text);
                    _addSer.Qs_no = txtWH.Text;  // null;
                    _addSer.Qs_seq_no = 0;
                    _addSer.Qs_ser = txtengine.Text;
                    _addSer.Qs_ser_line = _QuoSerialsSCM.Count + 1;
                    _addSer.Qs_ser_id = _serID;
                    _addSer.Qs_ser_loc = BaseCls.GlbUserDefLoca;
                    _addSer.Qs_itm_stus = lblItmStus.Text;
                    _QuoSerialsSCM.Add(_addSer);

                    dgvSerialSCM.AutoGenerateColumns = false;
                    dgvSerialSCM.DataSource = new List<QuotationSerial>();
                    dgvSerialSCM.DataSource = _QuoSerialsSCM;

                    chkWH.Enabled = false;
                    txtWH.Enabled = false;
                    btn_srch_WHLoc.Enabled = false;
                    cmbInvType.Enabled = false;

                }
                else
                {
                    var _record = (from _lst in _QuoSerials
                                   where _lst.Qs_item == lblItem.Text.Trim() && _lst.Qs_ser == txtengine.Text.Trim() && _lst.Qs_chassis == txtChasis.Text.Trim()
                                   select _lst.Qs_item).ToList();

                    if (_record.Count > 0)
                    {
                        MessageBox.Show("Already select this serial.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Int32 _qty = 0;
                    foreach (QuotationSerial _tmp in _QuoSerials)
                    {
                        if (_tmp.Qs_item == lblItem.Text && _tmp.Qs_main_line == Convert.ToInt16(lblLine.Text))
                        {
                            _qty = _qty + 1;
                        }
                    }

                    decimal _orgQty = 0;
                    _orgQty = Convert.ToDecimal(lblQty.Text);

                    if (Convert.ToInt16(_orgQty) <= _qty)
                    {
                        MessageBox.Show("Qty mismatch.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    QuotationSerial _addSer = new QuotationSerial();
                    _addSer.Qs_chassis = txtChasis.Text;
                    _addSer.Qs_item = lblItem.Text;
                    _addSer.Qs_main_line = Convert.ToInt16(lblLine.Text);
                    _addSer.Qs_no = null;
                    _addSer.Qs_seq_no = 0;
                    _addSer.Qs_ser = txtengine.Text;
                    _addSer.Qs_ser_line = _QuoSerials.Count + 1;
                    _addSer.Qs_ser_id = _serID;
                    _addSer.Qs_ser_loc = BaseCls.GlbUserDefLoca;
                    _QuoSerials.Add(_addSer);

                    dgvSerial.AutoGenerateColumns = false;
                    dgvSerial.DataSource = new List<QuotationSerial>();
                    dgvSerial.DataSource = _QuoSerials;


                    ReptPickSerials _tempItem = new ReptPickSerials();
                    _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblItem.Text.Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                    if (_tempItem.Tus_itm_cd != null)
                    {
                        MasterItem _itemList = new MasterItem();
                        _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, lblItem.Text.Trim());


                        _tempItem.Tus_itm_desc = _itemList.Mi_shortdesc;
                        _tempItem.Tus_itm_model = _itemList.Mi_model;
                        _tempItem.Tus_itm_brand = _itemList.Mi_brand;
                        _tempItem.Tus_base_doc_no = null;
                        _tempItem.Tus_base_itm_line = Convert.ToInt16(lblLine.Text);
                        _tempItem.Tus_isapp = 1;
                        _tempItem.Tus_iscovernote = 1;
                        _tempItem.Tus_com = BaseCls.GlbUserComCode;
                        _tempItem.Tus_loc = BaseCls.GlbUserDefLoca;
                        if (chkRes.Checked == true)// Nadeeka 10-09-2015
                        {
                            _tempItem.Tus_resqty = 1;
                        }
                        _ResList.Add(_tempItem);


                    }
                }


                txtengine.Text = "";
                txtChasis.Text = "";
                txtengine.Focus();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dgItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _mainItm = "";
                Int16 _mainLine = 0;
                decimal _mainQty = 0;

                _mainItm = dgItem.Rows[e.RowIndex].Cells["col_item"].Value.ToString();
                _mainLine = Convert.ToInt16(dgItem.Rows[e.RowIndex].Cells["col_line"].Value);
                _mainQty = Convert.ToDecimal(dgItem.Rows[e.RowIndex].Cells["col_qty"].Value);

                lblItem.Text = _mainItm;
                lblLine.Text = _mainLine.ToString();
                lblQty.Text = _mainQty.ToString();
                lblItmStus.Text = dgItem.Rows[e.RowIndex].Cells["col_stus"].Value.ToString();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDMob_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDMob.Text))
                {
                    if (!IsValidMobileOrLandNo(txtDMob.Text))
                    {
                        MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDMob.Text = ""; return;
                    }
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtDMob.Text, "C");

                    if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                    {
                        //  if (MessageBox.Show("Customer for the given mobile number already exists. Do you wish to load the relevant details?", "Customer Quotation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //  {
                        txtDCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        LoadDeliverCustomerDetailsByCustomer(null, null);
                        // }
                        // else
                        // {
                        //      txtMobile.Text = "";
                        //     txtMobile.Focus();
                        //     return;
                        //  }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDFax_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDFax.Text))
                {
                    if (!IsValidMobileOrLandNo(txtDFax.Text))
                    {
                        MessageBox.Show("Please select the valid fax #", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDFax.Text = ""; return;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        MessageBox.Show("Please select the valid mobile #", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMobile.Text = ""; return;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPriClose_Click(object sender, EventArgs e)
        {
            // PriceCombinItemSerialList = new List<ReptPickSerials>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            // _promotionSerial = new List<ReptPickSerials>();
            // _promotionSerialTemp = new List<ReptPickSerials>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            // pnlMain.Enabled = true;
            pnlPriceNPromotion.Visible = false;
        }

        private void dgItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgItem.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;
                    string _mainItm = "";
                    Int16 _mainLine = 0;
                    decimal _totVal = 0;

                    if (_rowIndex != -1)
                    {
                        #region Deleting Row
                        if (_colIndex == 0)
                        {
                            if (MessageBox.Show("Do you want to remove the item?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            _mainItm = dgItem.Rows[e.RowIndex].Cells["col_item"].Value.ToString();
                            _mainLine = Convert.ToInt16(dgItem.Rows[e.RowIndex].Cells["col_line"].Value);

                            //kapila 8/1/2016
                            _totVal = Convert.ToDecimal(dgItem.Rows[e.RowIndex].Cells["col_Tot"].Value);
                            if (_totVal < 0) _totDPAmt = _totDPAmt + _totVal;

                            _QuoSerials.RemoveAll(x => x.Qs_item == _mainItm && x.Qs_main_line == _mainLine);


                            // Int32 _combineLine = Convert.ToInt32(dgItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);
                            //if (_MainPriceSerial != null)
                            //if (_MainPriceSerial.Count > 0)
                            //{

                            //    //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                            //    string _item = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value;
                            //    decimal _uRate = (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value;
                            //    string _pbook = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value;
                            //    string _level = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value;

                            //    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                            //    var _remove = from _list in _tempSerial
                            //                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                            //                  select _list;

                            //    foreach (PriceSerialRef _single in _remove)
                            //    {
                            //        _tempSerial.Remove(_single);
                            //    }

                            //    _MainPriceSerial = _tempSerial;
                            //}

                            List<QoutationDetails> _tempList = _invoiceItemList;
                            //var _promo = (from _pro in _invoiceItemList
                            //              where _pro.qd_ == _combineLine
                            //              select _pro).ToList();

                            //if (_promo.Count() > 0)
                            //{
                            //    foreach (InvoiceItem code in _promo)
                            //    {
                            //        CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                            //        //_tempList.Remove(code);
                            //        ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                            //        InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                            //    }
                            //    _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                            //}
                            //else
                            //{
                            CalculateGrandTotal(Convert.ToDecimal(dgItem.Rows[_rowIndex].Cells["col_qty"].Value), (decimal)dgItem.Rows[_rowIndex].Cells["col_UP"].Value, (decimal)dgItem.Rows[_rowIndex].Cells["col_DisAmt"].Value, (decimal)dgItem.Rows[_rowIndex].Cells["col_Tax"].Value, false);
                            //InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[_rowIndex].Sad_itm_line);
                            _tempList.RemoveAt(_rowIndex);
                            //}

                            _invoiceItemList = _tempList;

                            Int32 _newLine = 1;
                            List<QoutationDetails> _tempLists = _invoiceItemList;
                            List<QuotationSerial> _tempSerList = _QuoSerials;

                            if (_tempLists != null)
                                if (_tempLists.Count > 0)
                                {
                                    foreach (QoutationDetails _itm in _tempLists)
                                    {
                                        Int32 _line = _itm.Qd_line_no;
                                        _invoiceItemList.Where(Y => Y.Qd_line_no == _line).ToList().ForEach(x => x.Qd_line_no = _newLine);


                                        foreach (QuotationSerial _ser in _tempSerList)
                                        {

                                            _QuoSerials.Where(T => T.Qs_main_line == _line).ToList().ForEach(S => S.Qs_main_line = _newLine);
                                        }
                                        //InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                        //ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

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
                            dgvSerial.AutoGenerateColumns = false;
                            dgvSerial.DataSource = new List<QuotationSerial>();
                            dgvSerial.DataSource = _QuoSerials;

                            //gvPopSerial.DataSource = new List<ReptPickSerials>();
                            //gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                            //gvGiftVoucher.DataSource = new List<ReptPickSerials>();
                            //gvGiftVoucher.DataSource = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();

                            //gvGiftVoucher.DataSource = ScanSerialList.Where(x=> x.Tus_itm_cd)

                            //ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                            //ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
                            //ucPayModes1.LoadData();
                            //LookingForBuyBack();
                            return;
                        }
                        #endregion


                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private void dgvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSerial.ColumnCount > 0)
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

                            _QuoSerials.RemoveAt(_rowIndex);

                            dgvSerial.AutoGenerateColumns = false;
                            dgvSerial.DataSource = new List<QuotationSerial>();
                            dgvSerial.DataSource = _QuoSerials;

                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUnitAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUnitAmt.Text)) { return; }

                if (!IsNumeric(txtUnitAmt.Text))
                {
                    MessageBox.Show("Invalid unit amount", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUnitAmt.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtdCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDCusAdd1.Focus();
            }
        }

        private void txtDCusAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDCusAdd2.Focus();
            }
        }

        private void txtDCusAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDNic.Focus();
            }
        }

        private void txtDNic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDMob.Focus();
            }

        }

        private void txtDMob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDFax.Focus();
            }
        }

        private void txtDFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        #region Normal Price Grid
        private void gvNormalPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if non-serialized, pick price and go to main screen
            //if serialized check the 'check box' and validate the main screen qty
            //load the available qty
            try
            {
                if (gvNormalPrice.ColumnCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                        if (_priceBookLevelRef.Sapl_is_serialized)
                        {
                            UncheckNormalPriceOrPromotionPrice(false, true);
                            DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)gvNormalPrice.Rows[_row].Cells[0];
                            if (Convert.ToBoolean(_chk.Value))
                                _chk.Value = false;
                            else
                                _chk.Value = true;

                            decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                              where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                              select row).Count();
                            if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                            {
                                _chk.Value = false;
                                MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            decimal _unitPrice = FigureRoundUp(Convert.ToDecimal(gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value), true);
                            decimal _bkpPrice = FigureRoundUp(Convert.ToDecimal(gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value), true);
                            string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                            string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                            string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();

                            //if (!string.IsNullOrEmpty(_unitPrice))
                            //{
                            //decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), false));
                            txtUnitPrice.Text = _unitPrice.ToString();

                            SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                            SSPriceBookSequance = _pbseq;
                            SSPriceBookItemSequance = _pblineseq;
                            WarrantyRemarks = _warrantyrmk;
                            CalculateItem();
                            //pnlMain.Enabled = true;
                            pnlPriceNPromotion.Visible = false;
                            //}
                        }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void gvNormalPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvNormalPrice.ColumnCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                        if (_priceBookLevelRef.Sapl_is_serialized == false)
                        {
                            decimal _unitPrice = FigureRoundUp(Convert.ToDecimal(gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value), true);//gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value.ToString();
                            decimal _bkpPrice = FigureRoundUp(Convert.ToDecimal(gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value), true);//gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value.ToString();
                            string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                            string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                            string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();

                            //if (!string.IsNullOrEmpty(_unitPrice))
                            //{
                            txtUnitPrice.Text = _unitPrice.ToString();
                            SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                            SSPriceBookSequance = _pbseq;
                            SSPriceBookItemSequance = _pblineseq;
                            WarrantyRemarks = _warrantyrmk;

                            CalculateItem();
                            // pnlMain.Enabled = true;
                            pnlPriceNPromotion.Visible = false;
                            //}
                        }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        private void gvPromotionPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void btnPriNProConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Akila processing promotions which are valid within the quotation expire date
                if (gvPromotionPrice.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _row in gvPromotionPrice.Rows)
                    {
                        if (Convert.ToBoolean(_row.Cells["PromPrice_Select"].Value) == true)
                        {
                            DateTime _promoValidDate = _row.Cells["PromPrice_ValidTill"].Value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_row.Cells["PromPrice_ValidTill"].Value.ToString());
                            if (_promoValidDate < dtpValidTo.Value.Date)
                            {
                                MessageBox.Show("Selected promotion cannot accept." + System.Environment.NewLine + "This promotion will be expired on " + _promoValidDate.Date.ToShortDateString() + ". If you want to proceed with selected promotion please change the valid date ", "Normal Or Promotion Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }

                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    #region When Price Level Serialized
                    int _normalCount = (from DataGridViewRow row in gvNormalPrice.Rows
                                        where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                        select row).Count();

                    int _promoCount = (from DataGridViewRow row in gvPromotionPrice.Rows
                                       where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                       select row).Count();

                    int _totalPickedSerial = _normalCount + _promoCount;

                    if (_totalPickedSerial == 0)
                    {
                        MessageBox.Show("Please select the price from normal or promotion", "Normal Or Promotion Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_totalPickedSerial > 1)
                    {
                        MessageBox.Show("You have selected more than one selection.", "Qty And Selection Mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        return;
                    }

                    //Get the normal price 
                    if (_normalCount > 0)
                    {
                        var _normalRow = from DataGridViewRow row in gvNormalPrice.Rows
                                         where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                         select row;
                        if (_normalRow != null)
                        {
                            foreach (var _row in _normalRow)
                            {
                                string _unitPrice = _row.Cells["NorPrice_UnitPrice"].Value.ToString();
                                string _bkpPrice = _row.Cells["NorPrice_BkpUPrice"].Value.ToString();
                                string _pbseq = _row.Cells["NorPrice_Pb_Seq"].Value.ToString();
                                string _pblineseq = string.Empty;
                                if (string.IsNullOrEmpty(Convert.ToString(_row.Cells["NorPrice_PbLineSeq"].Value))) _pblineseq = "1";
                                else _pblineseq = _row.Cells["NorPrice_PbLineSeq"].Value.ToString();
                                string _warrantyrmk = _row.Cells["NorPrice_WarrantyRmk"].Value.ToString();

                                if (!string.IsNullOrEmpty(_unitPrice))
                                {
                                    txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                                    SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                                    SSPriceBookSequance = _pbseq;
                                    SSPriceBookItemSequance = _pblineseq;
                                    WarrantyRemarks = _warrantyrmk;

                                    CalculateItem();
                                    // pnlMain.Enabled = true;
                                    pnlPriceNPromotion.Visible = false;
                                }
                            }
                        }
                        return;
                    }


                    //Get the promotion
                    if (_promoCount > 0)
                    {
                        var _promoRow = from DataGridViewRow row in gvPromotionPrice.Rows
                                        where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                        select row;
                        if (_promoRow != null)
                        {
                            foreach (var _row in _promoRow)
                            {
                                string _mainItem = _row.Cells["PromPrice_Item"].Value.ToString();
                                string _pbSeq = _row.Cells["PromPrice_Pb_Seq"].Value.ToString();
                                string _pbLineSeq = "0";
                                if (Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value) == string.Empty)
                                    _pbLineSeq = "0";
                                else
                                    _pbLineSeq = Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value);
                                string _pbWarranty = _row.Cells["PromPrice_WarrantyRmk"].Value.ToString();
                                string _unitprice = _row.Cells["PromPrice_UnitPrice"].Value.ToString();
                                string _promotioncode = _row.Cells["PromPrice_PromotionCD"].Value.ToString();
                                string _circulerncode = _row.Cells["PromPrice_Circuler"].Value.ToString();
                                string _promotiontype = _row.Cells["PromPrice_PriceType"].Value.ToString();
                                string _pbPrice = _row.Cells["PromPrice_BkpUPrice"].Value.ToString();
                                bool _isSingleItemSerialized = false;

                                foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                                {
                                    string _item = _ref.Sapc_itm_cd;
                                    string _originalItem = _ref.Sapc_itm_cd;
                                    string _similerItem = Convert.ToString(_ref.Similer_item);
                                    if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                    string _status = cmbStatus.Text.Trim();
                                    string _qty = Convert.ToString(_ref.Sapc_qty);
                                    bool _haveSerial = Convert.ToBoolean(_ref.Sapc_increse);
                                    string _serialno = Convert.ToString(_ref.Sapc_sub_ser);

                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                                    if (_itm.Mi_is_ser1 == 1) _isSingleItemSerialized = true;


                                    if (_haveSerial && _itm.Mi_is_ser1 == 1)
                                    {
                                        if (!string.IsNullOrEmpty(_serialno))
                                        {
                                            List<InventorySerialRefN> _refs = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serialno);
                                            if (_ref != null)
                                                if (_refs.Count > 0)
                                                {
                                                    var _available = _refs.Where(x => x.Ins_itm_cd == _item).ToList();
                                                    if (_available == null || _available.Count <= 0)
                                                    {
                                                        MessageBox.Show(_item + " item, " + _serialno + " serial  does not available in the current invnetory stock.", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        return;
                                                    }
                                                }
                                        }
                                        else if (string.IsNullOrEmpty(_serialno) && chkDeliverLater.Checked == false)
                                        {

                                            decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                            if (_serialcount != Convert.ToDecimal(_qty))
                                            {
                                                MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                return;
                                            }
                                        }
                                        else if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked)
                                        {
                                            ReptPickSerials _one = new ReptPickSerials();
                                            if (!string.IsNullOrEmpty(_serialno))
                                                PriceCombinItemSerialList.Add(VirtualSerialLine(_item, _status, Convert.ToDecimal(_qty), _serialno)[0]);

                                        }

                                    }

                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false)
                                    {
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        {
                                            MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            return;
                                        }
                                    }

                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1 && chkDeliverLater.Checked == false)
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus)
                                            _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item).ToList().Select(x => x.Qd_frm_qty).Sum();
                                        else
                                            _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item && x.Qd_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Qd_frm_qty).Sum();

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

                                    else if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked)
                                    {
                                        ReptPickSerials _one = new ReptPickSerials();
                                        if (!string.IsNullOrEmpty(_serialno))
                                        {
                                            _one.Tus_com = BaseCls.GlbUserComCode;
                                            _one.Tus_itm_cd = _item;
                                            _one.Tus_itm_stus = _status;
                                            _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                            _one.Tus_qty = Convert.ToDecimal(_qty);
                                            _one.Tus_ser_1 = _serialno;
                                            _one.Tus_ser_2 = "N/A";
                                            _one.Tus_ser_3 = "N/A";
                                            _one.Tus_ser_4 = "N/A";
                                            _one.Tus_ser_id = -100;
                                            _one.Tus_ser_line = 1;
                                            PriceCombinItemSerialList.Add(_one);
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
                                //pnlMain.Enabled = true;
                                btnAddItem.Focus();

                            }

                        }

                        return;
                    }
                    #endregion
                }

                else
                {
                    //check for promotion item selected
                    //collect the promotion item and check for the serial picked
                    //if not tally->msg
                    //focus main screen with price
                    bool _isSelect = false;
                    DataGridViewRow _pickedRow = new DataGridViewRow();
                    bool _isBuyBackItem = false;


                    foreach (DataGridViewRow _row in gvPromotionPrice.Rows)
                    {
                        if (Convert.ToBoolean(_row.Cells["PromPrice_Select"].Value) == true)
                        {
                            _isSelect = true;
                            _pickedRow = _row;
                            _isBuyBackItem = _row.Cells["PromPrice_PriceType"].Value.ToString() == "3" ? true : false;

                            break;
                        }
                    }

                    bool _isHavingSubItem = IsPromotionHavingSubItem(_pickedRow);

                    if (!_isSelect)
                    {
                        MessageBox.Show("You have to select a promotion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if ((_tempPriceCombinItem == null) && (!_isBuyBackItem)) //Akila - CRQ : 3839,3788 – In here when select buyback promotion , system request promotion item (Free item) selection. When user select buyback promotion it is not needed to select free items.
                    {
                        MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if ((_tempPriceCombinItem.Count <= 0 && _isHavingSubItem) && (!_isBuyBackItem))
                    {
                        MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (_isSelect)
                    {
                        string _mainItem = _pickedRow.Cells["PromPrice_Item"].Value.ToString();
                        string _pbSeq = _pickedRow.Cells["PromPrice_Pb_Seq"].Value.ToString();
                        string _pbLineSeq = _pickedRow.Cells["PromPrice_PbLineSeq"].Value.ToString();
                        string _pbWarranty = _pickedRow.Cells["PromPrice_WarrantyRmk"].Value.ToString();
                        string _unitprice = Convert.ToString(FigureRoundUp(Convert.ToDecimal(_pickedRow.Cells["PromPrice_UnitPrice"].Value.ToString()), true));
                        string _promotioncode = _pickedRow.Cells["PromPrice_PromotionCD"].Value.ToString();
                        string _circulerncode = _pickedRow.Cells["PromPrice_Circuler"].Value.ToString();
                        string _promotiontype = _pickedRow.Cells["PromPrice_PriceType"].Value.ToString();
                        string _pbPrice = _pickedRow.Cells["PromPrice_BkpUPrice"].Value.ToString();
                        bool _isSingleItemSerialized = false;

                        if (chkDeliverLater.Checked == false)
                            foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItem = _ref.Sapc_itm_cd;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
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
                                        _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item).ToList().Select(x => x.Qd_frm_qty).Sum();
                                    else
                                        _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item && x.Qd_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Qd_frm_qty).Sum();

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
                        if (chkDeliverLater.Checked)
                        {
                            foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItem = _ref.Sapc_itm_cd;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                string _status = cmbStatus.Text.Trim();
                                string _qty = Convert.ToString(_ref.Sapc_qty);
                                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                                //if (IsGiftVoucher(_itm.Mi_itm_tp))
                                //{
                                //    _isSingleItemSerialized = true;
                                //    decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                //    if (_serialcount != Convert.ToDecimal(_qty))
                                //    {
                                //        MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                //        return;
                                //    }
                                //}

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
                        //pnlMain.Enabled = true;
                        btnAddItem.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<ReptPickSerials> VirtualSerialLine(string _item, string _status, decimal _qty, string _serialno)
        {
            List<ReptPickSerials> _ser = new List<ReptPickSerials>();
            if (!string.IsNullOrEmpty(_serialno))
            {
                ReptPickSerials _one = new ReptPickSerials();
                _one.Tus_com = BaseCls.GlbUserComCode;
                _one.Tus_itm_cd = _item;
                _one.Tus_itm_stus = _status;
                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                _one.Tus_qty = Convert.ToDecimal(_qty);
                _one.Tus_ser_1 = _serialno;
                _one.Tus_ser_2 = "N/A";
                _one.Tus_ser_3 = "N/A";
                _one.Tus_ser_4 = "N/A";
                _one.Tus_ser_id = VirtualCounter + 1;
                _one.Tus_ser_line = 1;
                _ser.Add(_one);
            }
            else
            {
                for (int i = 0; i < Convert.ToInt32(_qty); i++)
                {
                    ReptPickSerials _one = new ReptPickSerials();
                    _one.Tus_com = BaseCls.GlbUserComCode;
                    _one.Tus_itm_cd = _item;
                    _one.Tus_itm_stus = _status;
                    _one.Tus_loc = BaseCls.GlbUserDefLoca;
                    _one.Tus_qty = 1;
                    _one.Tus_ser_1 = "N/A";
                    _one.Tus_ser_2 = "N/A";
                    _one.Tus_ser_3 = "N/A";
                    _one.Tus_ser_4 = "N/A";
                    _one.Tus_ser_id = VirtualCounter + 1;
                    _one.Tus_ser_line = 1;
                    _ser.Add(_one);
                }
            }
            return _ser;

        }
        private bool IsPromotionHavingSubItem(DataGridViewRow _row)
        {
            try
            {
                bool _yes = false;
                int _pricetype = Convert.ToInt32(_row.Cells["PromPrice_pricetype"].Value);
                DataTable _pricetypetbl = CHNLSVC.Sales.GetPriceTypeByIndent(_pricetype);
                if (_pricetypetbl.Rows[0]["sarpt_is_com"] == DBNull.Value)
                    _yes = false;
                else
                    _yes = Convert.ToBoolean(_pricetypetbl.Rows[0].Field<Int16>("sarpt_is_com"));
                return _yes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return false;
            }
        }

        private void cmbExecutive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPaymentTerm.Focus();
        }

        private void cmbExecutive_Leave(object sender, EventArgs e)
        {
            try
            {
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
                            cmbExecutive.SelectedIndex = 1;
                        }
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the correct sales executive", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExecutive.Text = string.Empty;
                    cmbExecutive.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {
                txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
            }
        }

        private void txtPaymentTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAddWara.Focus();
        }

        private void txtAddWara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    MessageBox.Show("Please select quotation number.", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoiceNo.Focus();
                    return;
                }

                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                BaseCls.GlbReportDoc = txtInvoiceNo.Text.Trim();
                BaseCls.GlbReportComp = BaseCls.GlbUserComCode;
                BaseCls.GlbReportName = "Quotation_RepPrint.rpt";
                _view.GlbReportName = "QUOTATION";
                _view.Show();
                _view = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10139))
            {
                MessageBox.Show("Sorry, You have no permission to cancel the quotation!\n( Advice: Required permission code :10139 )");
                return;
            }
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Please select quotation #", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lblStus.Text == "Cancelled")
            {
                MessageBox.Show("This is already cancelled", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DataTable _dtl = CHNLSVC.Sales.GetDeliveredQuotation(BaseCls.GlbUserComCode, txtInvoiceNo.Text);
            if (_dtl != null && _dtl.Rows.Count > 0)
            {
                MessageBox.Show("Unable to cancel this quotation, Delivery order already raised based on this", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //kapila 2/8/2016
            if (_qh_jobno == "W/H RESERVE")
            {
                List<QuotationSerial> _recallSerList = new List<QuotationSerial>();
                _recallSerList = CHNLSVC.Sales.GetQuoSerials(txtInvoiceNo.Text);
                foreach (QuotationSerial _one in _recallSerList)
                {
                    //check the serial is available
                    DataTable _IsValid = CHNLSVC.Inventory.IsValid_SCM_Serial(BaseCls.GlbUserComCode, _QH_ANAL_4, _one.Qs_item, _one.Qs_ser);
                    if (_IsValid.Rows.Count == 0)
                    {
                        MessageBox.Show("Unable to cancel ! Outward entry has been made for the reserved serial (" + _one.Qs_ser + ")", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            if (MessageBox.Show("Are you sure ?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            QuotationHeader _cancelHdr = new QuotationHeader();
            _cancelHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtInvoiceNo.Text);
            if (_cancelHdr != null)
            {
                string _outmsg = "";
                Int32 _eff = CHNLSVC.Sales.QuotationCancelProcess(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text, _qh_jobno, _QH_ANAL_4, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, out _outmsg);
                if (_eff != -99 && _eff >= 0)
                {
                    MessageBox.Show("Successfully cancelled", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    MessageBox.Show(_outmsg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        private bool IsAllScaned(List<ReptPickSerials> _list)
        {// Nadeeka 08-09-2015
            bool _isok = false;
            if (_invoiceItemList != null && _list != null)
            {
                foreach (DataGridViewRow _itm in dgItem.Rows)
                {
                    string _item = Convert.ToString(_itm.Cells["col_item"].Value); decimal _scanQty = Convert.ToDecimal(_itm.Cells["col_qty"].Value); string _status = Convert.ToString(_itm.Cells["col_stus"].Value); decimal _serialcount = 0;
                    MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    if (_itemDet.Mi_is_ser1 == 1)
                    {
                        _serialcount = (from _l in _list where _l.Tus_itm_cd == _item select _l.Tus_qty).Sum();

                        if (_scanQty != _serialcount)
                        {
                            _isok = false; break;
                        }
                        else _isok = true;
                    }
                }
            }
            return _isok;
        }

        private void txtDCusCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDOCus_Click(object sender, EventArgs e)
        {
            try
            {
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtDCusCode;
                _CusCre.ShowDialog();
                txtDCusCode.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dtpValidTo_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpValidTo.Value.Date < DateTime.Now.Date) -Commented by akila
            //{
            //    MessageBox.Show("Please enter a valid Date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dtpValidTo.Value = DateTime.Now.Date;
            //}
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            pnlMsg.Visible = false;
            groupBox1.Enabled = true;
        }

        private void pnlMsg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDNic_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDNic.Text))
            {
                Boolean _isValid = IsValidNIC(txtDNic.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Please enter a valid NIC", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDNic.Text = "";
                    txtDNic.Focus();
                    return;
                }
                //kapila 9/3/2017
                List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtDNic.Text, string.Empty, "C");

                if (_custList != null && _custList.Count > 1 && txtDNic.Text.ToUpper() != "N/A")
                {

                    pnlDuplicate.Visible = true;
                    lblErrMsg.Text = "Entered NIC number has multiple duplicate records";
                    grvDuplicate.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _custList;
                    grvDuplicate.DataSource = _source;
                    txtDNic.Text = "";
                    return;
                }

                MasterBusinessEntity custProf = GetbyNIC(txtDNic.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null)
                {
                    txtDCusCode.Text = custProf.Mbe_cd;
                    LoadDeliverCustomerDetailsByCustomer(null, null);
                }
                else
                {
                    txtDCusAdd1.Text = "";
                    txtDCusAdd2.Text = "";
                    txtDCusCode.Text = "";
                    txtdCusName.Text = "";
                    txtDMob.Text = "";
                    txtDFax.Text = "";
                }

            }
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode, "C");
        }

        private void txtWH_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWH.Text))
            {
                DataTable _dt = CHNLSVC.Inventory.getMstSysPara(BaseCls.GlbUserComCode, "SCHNL", BaseCls.GlbDefSubChannel, "RESLOCA", txtWH.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("This location is not allowed for reservation", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWH.Clear();
                    txtWH.Focus();
                    return;
                }
            }
        }

        private void chkWH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWH.Checked == false)
            {
                txtWH.Text = "";
                txtengine.Text = "";
                txtChasis.Text = "";
                dgvSerialSCM.Visible = false;
                txtengine.Enabled = true;
                chkRes.Checked = false;
                txtWH.Enabled = false;
                btn_srch_WHLoc.Enabled = false;
            }
            else
            {
                txtengine.Text = "";
                txtChasis.Text = "";
                dgvSerialSCM.Visible = true;
                txtengine.Enabled = false;
                chkRes.Checked = true;
                txtWH.Enabled = true;
                btn_srch_WHLoc.Enabled = true;
            }
        }

        private void btn_srch_WHLoc_Click(object sender, EventArgs e)
        {

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtWH;
            _CommonSearch.ShowDialog();
            txtWH.Select();

        }

        private void dgvSerialSCM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSerialSCM.ColumnCount > 0)
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

                            _QuoSerialsSCM.RemoveAt(_rowIndex);

                            dgvSerialSCM.AutoGenerateColumns = false;
                            dgvSerialSCM.DataSource = new List<QuotationSerial>();
                            dgvSerialSCM.DataSource = _QuoSerialsSCM;

                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCloseNew_Click(object sender, EventArgs e)
        {
            pnlNewSer.Visible = false;
        }

        private void btnUpdSer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Please select the quotation number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<QuotationSerial> _recallSerList = new List<QuotationSerial>();
            _recallSerList = CHNLSVC.Sales.GetQuoSerials(txtInvoiceNo.Text);

            dgvSerialSCMOld.AutoGenerateColumns = false;
            dgvSerialSCMOld.DataSource = new List<QuotationSerial>();
            dgvSerialSCMOld.DataSource = _recallSerList;

            pnlNewSer.Visible = true;
        }

        private void btn_srch_newser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldSer.Text))
            {
                MessageBox.Show("Please select the item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DataTable _result = null;
            DataTable _dtItmStus = CHNLSVC.Inventory.GetItemStatusMaster(lblOldStus.Text, null);
            _result = CHNLSVC.Inventory.getNextSerialSCM(BaseCls.GlbUserComCode, _QH_ANAL_4, lblOldItm.Text, _dtItmStus.Rows[0]["mis_old_cd"].ToString());
            if (_result.Rows.Count > 0)
            {
                int _x = dgvSerialSCM.Rows.Count;
                txtNewSer.Text = _result.Rows[_x]["serial_no"].ToString();
                txtNewOthSer.Text = _result.Rows[_x]["chassis_no"].ToString();
            }
            else
            {
                MessageBox.Show("Available Engine/Serial not found", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvSerialSCMOld_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdNewSer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldSer.Text))
            {
                MessageBox.Show("Please select the serial number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string _outmsg = "";
            Int32 _eff = CHNLSVC.Sales.UpdateQuotationReserve(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text, lblOldItm.Text, txtOldSer.Text, txtNewSer.Text, txtNewOthSer.Text, out _outmsg);
            if (_eff != -99 && _eff >= 0)
            {
                MessageBox.Show("Successfully cancelled", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
            }
            else
            {
                MessageBox.Show(_outmsg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSerialSCMOld_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSerialSCMOld_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtOldSer.Text = dgvSerialSCMOld.Rows[e.RowIndex].Cells["qs_ser"].Value.ToString();
            txtOldOthSer.Text = dgvSerialSCMOld.Rows[e.RowIndex].Cells["qs_chassis"].Value.ToString();
            lblOldItm.Text = dgvSerialSCMOld.Rows[e.RowIndex].Cells["qs_item"].Value.ToString();
            lblOldStus.Text = dgvSerialSCMOld.Rows[e.RowIndex].Cells["Qs_itm_stus"].Value.ToString();
        }

        private void btnPopUpClose_Click(object sender, EventArgs e)
        {
            pnlDuplicate.Visible = false;
        }

        private void txtDNic_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                ReptPickHeader _SerHeader = new ReptPickHeader();
                List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    MessageBox.Show("Please select customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDCusCode.Text))
                {
                    MessageBox.Show("Please select delivery customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDCusCode.Focus();
                    return;
                }

                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                {
                    MessageBox.Show("Please select relavant items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkRes.Checked == true)
                {
                    if (dgvSerial.Rows.Count <= 0 && dgvSerialSCM.Rows.Count <= 0)
                    {
                        MessageBox.Show("You select item reservation option but not select details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //kapila 9/3/2017
                if ((cmbInvType.Text == "CRED") && (string.IsNullOrEmpty(txtDNic.Text) && string.IsNullOrEmpty(txtDMob.Text))) // updated by akila 2018/03/02 either mobile number or id number is mandatory
                {
                    MessageBox.Show("Please enter NIC #", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDNic.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtDCusCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtDCusCode.Text, string.Empty, string.Empty, "C");
                }

                if (_masterBusinessCompany.Mbe_cd == null)//13-10-2015 Nadeeka
                {
                    MessageBox.Show("Please select the valid delivery customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDCusCode.Focus();
                    return;
                }
                if (_dpRate > 0) //kapila 8/1/2016
                {
                    if (((Convert.ToDecimal(lblGrndTotalAmount.Text) + _totDPAmt) / 100 * _dpRate) > _totDPAmt)
                    {
                        MessageBox.Show("Down payment is less than the minimum down payment", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }



                HpSystemParameters _SystemPara = new HpSystemParameters();// Nadeeka 08-09-2015
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "QUOSRL", DateTime.Now.Date);
                if (_SystemPara.Hsy_desc != null)
                {
                    if (_SystemPara.Hsy_val == 1)// Compulsary serial reserve
                    {
                        chkRes.Checked = true;
                        if (dgvSerial.Rows.Count <= 0)
                        {
                            MessageBox.Show("Please select relavant serials.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }



                        #region Check for Scan serial with qty
                        bool _isOk = IsAllScaned(_ResList);
                        if (_isOk == false)
                        {
                            MessageBox.Show("Scan serial count and the serial are mismatch");
                            return;
                        }
                        #endregion
                    }
                }

                if (chkRes.Checked == true)
                {
                    if (dgvSerial.Rows.Count <= 0 && dgvSerialSCM.Rows.Count <= 0)
                    {
                        MessageBox.Show("If you want to reserve the items. Please select relavant serials.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (MessageBox.Show("Confirm in order to reserve the selected serial/serials", "Customer Quotation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                else
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                //updated by akila 2018/01/09 - copied from invoice
                //Check and apply warranty period and remarks - New
                DateTime _serverDt = DateTime.Now.Date;
                DateTime.TryParse(txtDate.Text, out _serverDt);

                foreach (QoutationDetails _itmWar in _invoiceItemList)
                {
                    //Check Selected price book and level is warranty base price level.
                    PriceBookLevelRef _pbLvl = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _itmWar.Qd_pbook, _itmWar.Qd_pb_lvl, _itmWar.Qd_itm_stus);
                    if (_pbLvl != null)
                    {
                        if (_pbLvl.Sapl_set_warr == true || txtDate.Value.Date != _serverDt)
                        {
                            if (CheckItemWarrantyNew(_itmWar.Qd_itm_cd, _itmWar.Qd_itm_stus, _itmWar.Qd_pb_seq, Convert.ToInt32(SSPriceBookItemSequance), _itmWar.Qd_pbook, _itmWar.Qd_pb_lvl, _pbLvl.Sapl_set_warr, _itmWar.Qd_unit_price, _pbLvl.Sapl_warr_period))
                            {
                                MessageBox.Show(_itmWar.Qd_itm_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                _itmWar.Qd_warr_pd = WarrantyPeriod;
                                _itmWar.Qd_warr_rmk = WarrantyRemarks;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot find valid warranty.Please contact IT Dept.", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                //collect quo header
                QuotationHeader _saveHdr = new QuotationHeader();
                _saveHdr.Qh_seq_no = quoSeq;
                _saveHdr.Qh_add1 = txtAddress1.Text;
                _saveHdr.Qh_add2 = txtAddress2.Text;
                _saveHdr.Qh_com = BaseCls.GlbUserComCode;
                _saveHdr.Qh_cre_by = BaseCls.GlbUserID;
                _saveHdr.Qh_cur_cd = "LKR";
                _saveHdr.Qh_del_cusadd1 = txtDCusAdd1.Text;
                _saveHdr.Qh_del_cusadd2 = txtDCusAdd2.Text;
                _saveHdr.Qh_del_cuscd = txtDCusCode.Text;
                _saveHdr.Qh_del_cusfax = txtDFax.Text;
                _saveHdr.Qh_del_cusid = txtDNic.Text;
                _saveHdr.Qh_del_cusname = txtdCusName.Text;
                _saveHdr.Qh_del_custel = txtDMob.Text;
                _saveHdr.Qh_del_cusvatreg = null;
                _saveHdr.Qh_dt = Convert.ToDateTime(txtDate.Text).Date;
                _saveHdr.Qh_ex_dt = Convert.ToDateTime(dtpValidTo.Value).Date;
                _saveHdr.Qh_ex_rt = 1;
                _saveHdr.Qh_frm_dt = Convert.ToDateTime(txtDate.Text).Date;
                _saveHdr.Qh_is_tax = chkTaxPayable.Checked;

                if (chkWH.Checked)
                {
                    _saveHdr.Qh_jobno = "W/H RESERVE";
                    _saveHdr.Qh_anal_4 = txtWH.Text;
                }
                else
                {
                    _saveHdr.Qh_jobno = "";
                    _saveHdr.Qh_anal_4 = "";
                }

                _saveHdr.Qh_mobi = txtMobile.Text;
                _saveHdr.Qh_mod_by = BaseCls.GlbUserID;
                _saveHdr.Qh_no = txtInvoiceNo.Text;
                _saveHdr.Qh_party_cd = txtCustomer.Text;
                _saveHdr.Qh_party_name = txtCusName.Text;
                _saveHdr.Qh_pc = BaseCls.GlbUserDefProf;
                _saveHdr.Qh_ref = txtDocRefNo.Text;
                _saveHdr.Qh_remarks = txtRemarks.Text;
                _saveHdr.Qh_sales_ex = txtExecutive.Text;
                //  _saveHdr.Qh_seq_no = 0;
                _saveHdr.Qh_session_id = BaseCls.GlbUserSessionID;
                _saveHdr.Qh_stus = "A";
                _saveHdr.Qh_sub_tp = "N";
                _saveHdr.Qh_tel = txtMobile.Text;
                _saveHdr.Qh_tp = "C";
                _saveHdr.Qh_anal_1 = _MasterProfitCenter.Mpc_man;
                _saveHdr.Qh_anal_2 = txtPaymentTerm.Text.Trim();
                _saveHdr.Qh_anal_3 = cmbInvType.Text;
                //kapila 11/3/2016
                _saveHdr.Qh_quo_base = _isSelQuoBaseLevel;

                if (quoSeq == 0)
                {
                    _saveHdr.Qh_no = null;
                }

                Int32 _isRes = 0;
                if (chkRes.Checked == true)
                {
                    _isRes = 1;
                }
                else
                {
                    _isRes = 0;
                }



                _saveHdr.Qh_anal_5 = _isRes;

                if (_isRes == 1)// 24-11-2015 Nadeeka
                {

                    _tempSerialSave.ForEach(x => x.Tus_resqty = 1);
                }
                else
                {
                    _tempSerialSave.ForEach(x => x.Tus_resqty = 0);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "QUA";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "QUA";
                masterAuto.Aut_year = null;

                if (chkRes.Checked == true)
                {
                    _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "RECEIPT", 0, BaseCls.GlbUserComCode);
                    _SerHeader.Tuh_usr_id = BaseCls.GlbUserID;
                    _SerHeader.Tuh_usr_com = BaseCls.GlbUserComCode;
                    _SerHeader.Tuh_session_id = BaseCls.GlbUserSessionID;
                    _SerHeader.Tuh_cre_dt = Convert.ToDateTime(dtpValidTo.Text).Date;
                    _SerHeader.Tuh_doc_tp = "QUO";
                    _SerHeader.Tuh_direct = false;
                    _SerHeader.Tuh_ischek_itmstus = true;
                    _SerHeader.Tuh_ischek_simitm = true;
                    _SerHeader.Tuh_ischek_reqqty = true;
                    _SerHeader.Tuh_doc_no = null;


                    foreach (ReptPickSerials line in _ResList)
                    {
                        line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                        line.Tus_cre_by = BaseCls.GlbUserID;
                        _tempSerialSave.Add(line);
                    }
                }

                //kapila 13/7/2016 - warehouse reservation
                InventoryRequest _inventoryRequest = new InventoryRequest();
                MasterAutoNumber master_Auto = new MasterAutoNumber();
                if (_QuoSerialsSCM.Count > 0)
                {
                    string _dispathLoc = "";
                    List<QuotationSerial> _temp = new List<QuotationSerial>();
                    _temp = _QuoSerialsSCM.GroupBy(x => new { x.Qs_itm_stus, x.Qs_item, x.Qs_no }).Select(cl => new QuotationSerial { Qs_item = cl.First().Qs_item, Qs_itm_stus = cl.First().Qs_itm_stus, Qs_no = cl.First().Qs_no, Qs_ser = cl.Count().ToString().ToString().ToString(), }).ToList();

                    if (_temp != null && _temp.Count > 0)
                    {
                        _invReqItemList = new List<InventoryRequestItem>();
                        foreach (QuotationSerial _ser in _temp)
                        {
                            InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                            // decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                            MasterItem _mstItm = new MasterItem();
                            _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _ser.Qs_item);

                            _inventoryRequestItem.Itri_itm_cd = _ser.Qs_item;
                            _inventoryRequestItem.Mi_longdesc = _mstItm.Mi_longdesc;
                            _inventoryRequestItem.Mi_model = _mstItm.Mi_model;
                            _inventoryRequestItem.Mi_brand = _mstItm.Mi_brand;
                            _inventoryRequestItem.MasterItem = _mstItm;

                            _inventoryRequestItem.Itri_itm_stus = _ser.Qs_itm_stus;
                            //_inventoryRequestItem.Itri_res_no = _reservationNo;
                            //_inventoryRequestItem.Itri_note = _remarksText;
                            _inventoryRequestItem.Itri_qty = Convert.ToInt32(_ser.Qs_ser);
                            _inventoryRequestItem.Itri_app_qty = Convert.ToInt32(_ser.Qs_ser);

                            //Add Main item details.
                            _inventoryRequestItem.Itri_mitm_cd = _ser.Qs_item;
                            _inventoryRequestItem.Itri_mitm_stus = _ser.Qs_itm_stus;
                            _inventoryRequestItem.Itri_mqty = Convert.ToInt32(_ser.Qs_ser);
                            _dispathLoc = _ser.Qs_no;

                            _invReqItemList.Add(_inventoryRequestItem);
                        }

                        int _count = 1;
                        _invReqItemList.ForEach(x => x.Itri_line_no = _count++);

                        //Fill the InventoryRequest header & footer data.
                        _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                        _inventoryRequest.Itr_req_no = GetRequestNo();
                        _inventoryRequest.Itr_tp = "MRN";
                        _inventoryRequest.Itr_sub_tp = "NOR";
                        _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                        _inventoryRequest.Itr_ref = string.Empty;
                        _inventoryRequest.Itr_dt = Convert.ToDateTime(txtDate.Text).Date;
                        _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtDate.Text).Date;
                        _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved. 
                        _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                        _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                        ////        _inventoryRequest.Itr_note = txtRemark.Text;
                        _inventoryRequest.Itr_issue_from = _dispathLoc;
                        _inventoryRequest.Itr_rec_to = BaseCls.GlbUserDefLoca;
                        _inventoryRequest.Itr_direct = 0;
                        _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                        _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                        _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                        _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                        ////            _inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                        ////                _inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                        _inventoryRequest.Itr_act = 1;
                        _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                        _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                        _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;

                        _inventoryRequest.InventoryRequestItemList = _invReqItemList;


                        master_Auto.Aut_cate_tp = "LOC";
                        master_Auto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                        master_Auto.Aut_direction = null;
                        master_Auto.Aut_modify_dt = null;
                        master_Auto.Aut_moduleid = "MRN";
                        master_Auto.Aut_number = 0;
                        master_Auto.Aut_start_char = "MRN";
                        master_Auto.Aut_year = null;
                    }
                }

                string QTNum;

                row_aff = (Int32)CHNLSVC.Sales.Quotation_save(_saveHdr, _invoiceItemList, masterAuto, _QuoSerials, _inventoryRequest, _QuoSerialsSCM, master_Auto, chkRes.Checked, _SerHeader, _tempSerialSave, out QTNum);



                if (row_aff >= 1)
                {
                    MessageBox.Show("Successfully created " + QTNum, "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MessageBox.Show("Successfully created.Quotation No: " + QTNum, "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    BaseCls.GlbReportDoc = QTNum;
                    BaseCls.GlbReportComp = BaseCls.GlbUserComCode;
                    BaseCls.GlbReportName = "Quotation_RepPrint.rpt";
                    _view.GlbReportName = "QUOTATION";
                    _view.Show();
                    _view = null;

                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(QTNum))
                    {
                        MessageBox.Show(QTNum, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPriNProCancel_Click(object sender, EventArgs e)
        {
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            pnlPriceNPromotion.Visible = false;
        }

        private void CustomerQuotation_Load(object sender, EventArgs e)
        {

        }

        //akila 2018/01/09 - copied from invoice
        private bool CheckItemWarrantyNew(string _item, string _status, Int32 _pbSeq, Int32 _itmSeq, string _pb, string _pbLvl, Boolean _isPbWara, decimal _unitPrice, Int32 _pbWarrPd)
        {
            bool _isNoWarranty = false;
            MasterItemWarrantyPeriod _period = new MasterItemWarrantyPeriod();
            LogMasterItemWarranty _periodLog = new LogMasterItemWarranty();
            DateTime _serverDt = DateTime.Now.Date;
            DateTime.TryParse(txtDate.Text, out _serverDt);

            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item.Trim(), _status.Trim(), Convert.ToDateTime(txtDate.Text).Date);

            if (_isPbWara == true && _unitPrice > 0)
            {
                WarrantyPeriod = _pbWarrPd;
                PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, _itmSeq, _pbSeq);
                if (_lsts != null)
                {
                    WarrantyRemarks = _lsts.Sapd_warr_remarks;
                }

            }
            else if (_temWarr != null && _temWarr.Rows.Count > 0)
            {
                WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
            }
            else if (txtDate.Value.Date != _serverDt)
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Sales.GetItemWarrEffDt(_item, _status, 1, txtDate.Value.Date);
                if (_period.Mwp_itm_cd != null)
                {
                    WarrantyPeriod = _period.Mwp_val;
                    WarrantyRemarks = _period.Mwp_rmk;
                }
                else
                {
                    _periodLog = new LogMasterItemWarranty();
                    _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_item.Trim(), _status.Trim(), 1, txtDate.Value.Date); if (_periodLog.Lmwp_itm_cd != null) { WarrantyPeriod = _periodLog.Lmwp_val; WarrantyRemarks = _periodLog.Lmwp_rmk; }
                    else { _isNoWarranty = true; }
                }
            }
            else
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period.Mwp_itm_cd != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                else { _isNoWarranty = true; }
            }

            return _isNoWarranty;
        }
    }
}

