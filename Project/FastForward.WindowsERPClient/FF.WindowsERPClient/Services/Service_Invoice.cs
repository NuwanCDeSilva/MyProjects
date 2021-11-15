using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;

namespace FF.WindowsERPClient.Services
{
    public partial class Service_Invoice : Base
    {
        private List<Service_Confirm_detail> oMainDetailList = new List<Service_Confirm_detail>();
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private DataTable _levelStatus = null;
        private string DefaultInvoiceType = string.Empty;
        private string DefaultStatus = string.Empty;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private PriceBookLevelRef _priceBookLevelRef = null;
        private bool _isItemChecking = false;
        private MasterItem _itemdetail = null;
        private bool _isDecimalAllow = false;
        private bool _IsVirtualItem = false;
        private bool _isBlocked = false;
        private string WarrantyRemarks = string.Empty;
        private Int32 WarrantyPeriod = 0;
        private Dictionary<decimal, decimal> ManagerDiscount = null;
        private List<PriceCombinedItemRef> _MainPriceCombinItem = null;
        private List<MasterItemTax> MainTaxConstant = null;
        private bool _isCompleteCode = false;
        private List<MasterItemComponent> _masterItemComponent = null;
        private bool _isInventoryCombineAdded = false;
        private bool IsSaleFigureRoundUp = false;
        private List<PriceDetailRef> _priceDetailRef = null;
        public string SSCirculerCode = string.Empty;
        public string SSPriceBookItemSequance = string.Empty;
        public string SSPriceBookSequance = string.Empty;
        public string SSPromotionCode = string.Empty;
        public Int32 SSPRomotionType = 0;
        public decimal SSPriceBookPrice = 0;
        private bool _isEditDiscount = false;
        private bool _isEditPrice = false;
        private PriortyPriceBook _priorityPriceBook = null;
        private bool IsPriceLevelAllowDoAnyStatus = false;
        private MasterProfitCenter _MasterProfitCenter = null;
        private CashGeneralEntiryDiscountDef GeneralDiscount = null;
        private string _proVouInvcItem = string.Empty;
        private DataTable MasterChannel = null;

        public Service_Invoice()
        {
            InitializeComponent();
            dgvJobConf.AutoGenerateColumns = false;
            dgvDetailItems.AutoGenerateColumns = false;

            pnlDeliveryDetails.Size = new System.Drawing.Size(461, 167);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
   
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "3,2,6" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Service_JobReqNum:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator + "0" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Service_conf_customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void Service_Invoice_Load(object sender, EventArgs e)
        {
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            setDgvColumnsWidth();
            ucPayModes1.MainGrid.Columns[2].Visible = true;
            btnClear_Click(null, null);

            //load priority price book

            if (_PriceDefinitionRef != null)
            {
                List<PriceDefinitionRef> tem = (from _res in _PriceDefinitionRef
                                                where _res.Sadd_def_pb
                                                select _res).ToList<PriceDefinitionRef>();
                if (tem != null && tem.Count > 0)
                {
                    _priorityPriceBook = new PriortyPriceBook();
                    _priorityPriceBook.Sppb_pb = tem[0].Sadd_pb;
                    _priorityPriceBook.Sppb_pb_lvl = tem[0].Sadd_p_lvl;
                }
            }
            LoadCachedObjects();
            LoadPriceDefaultValue();
        }

        private void txtRequest_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_JobReqNum);
            DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_REQNO(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRequest;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            txtRequest.Focus();
            _CommonSearch.ShowDialog();
        }

        private void txtRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRequest_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtJobNo.Focus();
            }
        }

        private void txtRequest_Leave(object sender, EventArgs e)
        {
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch);
            DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            txtJobNo.Focus();
            _CommonSearch.ShowDialog();
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCustomer.Focus();
            }
        }

        private void txtCustomer_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_customer);
            DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_CUST(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCustomer;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            txtCustomer.Focus();
            _CommonSearch.ShowDialog();
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtCustomer_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCustomer.Focus();
            }
        }

        private void getJobConfirmHeader()
        {
        }

        private void btnSearchHeader_Click(object sender, EventArgs e)
        {
            List<Service_confirm_Header> oHeaders = CHNLSVC.CustService.GetServiceConfirmHeader(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, txtJobNo.Text, txtRequest.Text, txtCustomer.Text);
            dgvJobConf.DataSource = new List<Service_confirm_Header>();
            dgvJobConf.DataSource = oHeaders;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvJobConf.DataSource = new List<Service_confirm_Header>();
            oMainDetailList = new List<Service_Confirm_detail>();
            pnlDeliveryDetails.Visible = false;
            ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.ClearControls();
        }

        private void dgvJobConf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = true;
                Int32 seq = Convert.ToInt32(dgvJobConf.Rows[e.RowIndex].Cells["Jch_seq"].Value.ToString());
                String ConfirmNum = dgvJobConf.Rows[e.RowIndex].Cells["Jch_no"].Value.ToString();

                string custCode = dgvJobConf.Rows[e.RowIndex].Cells["JCH_CUST_CD"].Value.ToString();
                string custName = dgvJobConf.Rows[e.RowIndex].Cells["JCH_CUST_NAME"].Value.ToString();
                string InvoiceType = dgvJobConf.Rows[e.RowIndex].Cells["JCH_INVTP"].Value.ToString();

                txtBillingCustCode.Text = custCode;
                txtBillingCustName.Text = custName;
                txtInvoiceType.Text = InvoiceType;

                List<Service_Confirm_detail> tempList = CHNLSVC.CustService.GetServiceConfirmDetials(seq, ConfirmNum);
                if (oMainDetailList.FindAll(x => x.Jcd_seq == seq && x.Jcd_no == ConfirmNum).Count == 0)
                {
                    oMainDetailList.AddRange(tempList);
                    bindDetails();
                }

            }
        }

        private void dgvJobConf_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void dgvJobConf_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvJobConf.IsCurrentCellDirty)
            {
                dgvJobConf.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void bindDetails()
        {
            dgvDetailItems.DataSource = new List<Service_Confirm_detail>();
            dgvDetailItems.DataSource = oMainDetailList;
        }

        private void setDgvColumnsWidth()
        {
            // dgvDetailItems.Columns["JCD_NO "].Width = txtItemCodeI.Width;
            dgvDetailItems.Columns["JCD_JOBNO"].Width = cmbJobNumber.Width;
            dgvDetailItems.Columns["JCD_ITMCD"].Width = txtItemCodeI.Width;
            dgvDetailItems.Columns["JCD_ITMSTUS"].Width = cmbStatus.Width;
            dgvDetailItems.Columns["JCD_QTY"].Width = txtQtyI.Width;
            dgvDetailItems.Columns["JCD_UNITPRICE"].Width = txtUnitPriceI.Width;
            //dgvDetailItems.Columns["JCD_PBPRICE"].Width = txtPriceBook.Width;
            dgvDetailItems.Columns["Value"].Width = txtValueI.Width;
            dgvDetailItems.Columns["JCD_DIS_RT"].Width = txtDisI.Width;
            dgvDetailItems.Columns["JCD_DIS"].Width = txtDisAmountI.Width;
            dgvDetailItems.Columns["JCD_TAX"].Width = txtTaxAmountI.Width;
            dgvDetailItems.Columns["JCD_NET_AMT"].Width = txtAmountI.Width;

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlDeliveryDetails.Visible = false;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            pnlDeliveryDetails.Visible = true;
        }

        private void txtJobNoI_DoubleClick(object sender, EventArgs e)
        {

        }

        private void txtItemCodeI_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCodeI;
                _CommonSearch.ShowDialog();
                txtItemCodeI.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemCodeI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemCodeI_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                cmbBook.Focus();
            }
        }

        private void txtItemCodeI_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCodeI.Text.Trim()))
                return;
            if (_isItemChecking)
            {
                _isItemChecking = false;
                return;
            }
            _isItemChecking = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!LoadItemDetail(txtItemCodeI.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCodeI.Clear();
                    txtItemCodeI.Focus();
                    return;
                }

                IsVirtual(_itemdetail.Mi_itm_tp);

                {
                    txtQtyI.Text = FormatToQty("1");
                }
                if (_IsVirtualItem)
                {
                    bool block = CheckBlockItem(txtItemCodeI.Text.Trim(), 0, false);
                    if (block)
                    {
                        txtItemCodeI.Text = "";
                    }
                }
                CheckQty(true);
                btnAdd.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false;
            }
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == txtInvoiceType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataSource = _books;
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook))
                        cmbBook.Text = DefaultBook;
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }

        private void LoadPriceDefaultValue()
        {
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
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
                            //LoadInvoiceType();
                            LoadPriceBook(txtInvoiceType.Text);
                            LoadPriceLevel(txtInvoiceType.Text, cmbBook.Text.Trim());
                            LoadLevelStatus(txtInvoiceType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                            //CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                        }
                }
            //cmbTitle.SelectedIndex = 0;
        }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == txtInvoiceType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbLevel.DataSource = _levels;
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text))
                        cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());
                    LoadPriceLevelMessage();
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }

        private void LoadPriceLevelMessage()
        {
            DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
            if (_msg != null && _msg.Rows.Count > 0)
                lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
            else
                lblLvlMsg.Text = string.Empty;
        }

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
                    LoadPriceLevelMessage();
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
            return _isAvailable;
        }

        private void txtInvoiceType_TextChanged(object sender, EventArgs e)
        {
            LoadPriceBook(txtInvoiceType.Text);
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();
            _isDecimalAllow = false;

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                //lblIsEditDesctiption.Text = _itemdetail.Mi_is_editshortdesc.ToString();
                //lblItemUOM.Text = _itemdetail.Mi_itm_uom;
                //lblItemType.Text = _itemdetail.Mi_itm_tp;

                lblItemDescription.Text = "Description : " + _description;
                lblItemModel.Text = "Model : " + _model;
                lblItemBrand.Text = "Brand : " + _brand;
                lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;

                _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                if (cmbStatus.SelectedValue != null)
                {
                    lblcostPrice.Text = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, cmbStatus.SelectedValue.ToString()).ToString("N");
                }
            }
            else _isValid = false;
            return _isValid;
        }

        private bool IsVirtual(string _type)
        {
            if (_type == "V")
            {
                _IsVirtualItem = true;
                return true;
            }
            else
            {
                _IsVirtualItem = false;
                return false;
            }
        }

        private bool CheckBlockItem(string _item, int _pricetype, bool _isCombineItemAddingNow)
        {
            if (_isCombineItemAddingNow) return false;
            _isBlocked = false;
            if (_priceBookLevelRef!= null && _priceBookLevelRef.Sapl_is_serialized == false)
            {
                MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                {
                    this.Cursor = Cursors.Default;
                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show(_item + " item already blocked by the Costing Dept.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    _isBlocked = true;
                }
            }
            return _isBlocked;
        }

        protected bool CheckQty(bool _isSearchPromotion)
        {
            txtDisI.Text = FormatToCurrency("0");
            txtDisAmountI.Text = FormatToCurrency("0");
            WarrantyPeriod = 0;
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            //SSPriceBookSequance = "0";
            //SSPriceBookItemSequance = "0";
            //SSPriceBookPrice = 0;
            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    this.Cursor = Cursors.Default;
                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("This compete code does not having a collection. Please contact inventory", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCodeI.Text.Trim());
            if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
            {
                MessageBox.Show("Combine codes can't add.", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (CheckQtyPriliminaryRequirements()) return true;

            //if (_isCombineAdding == false)
            //    if (CheckTaxAvailability())
            //    {
            //        this.Cursor = Cursors.Default;
            //        using (new CenterWinDialog(this)) { MessageBox.Show("Tax rates not setup for selected item code and item status.Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //        _IsTerminate = true;
            //        return _IsTerminate;
            //    }

            //if (_isCombineAdding == false) 
            CheckItemTax(txtItemCodeI.Text.Trim());
            //if (_isCombineAdding == false)
            //    if (CheckProfitCenterAllowForWithoutPrice())
            //    {
            //        _IsTerminate = true;
            //        return _IsTerminate;
            //    }
            //if (_isCombineAdding == false)
            //    if (ConsumerItemProduct())
            //    {
            //        _IsTerminate = true;
            //        return _IsTerminate;
            //    }
            //if (_isSearchPromotion) if (CheckItemPromotion()) { _IsTerminate = true; return _IsTerminate; }
            //if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
            //    if (CheckSerializedPriceLevelAndLoadSerials(true))
            //    {
            //        _IsTerminate = true;
            //        return _IsTerminate;
            //    }
            //if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
            //if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            //{
            //    txtUnitPrice.ReadOnly = false;
            //    txtDisI.ReadOnly = false;
            //    txtDisAmountI.ReadOnly = false;
            //    txtValueI.ReadOnly = true;
            //    txtTaxAmt.ReadOnly = true;
            //    txtLineTotAmt.ReadOnly = true;
            //    return true;
            //}
            //else
            //{
            //    txtUnitPrice.ReadOnly = true;
            //    txtValueI.ReadOnly = true;
            //    txtTaxAmt.ReadOnly = true;
            //    txtLineTotAmt.ReadOnly = true;
            //    if (_itemdetail.Mi_itm_tp == "V")
            //    {
            //        txtDisI.ReadOnly = true;
            //        txtDisAmountI.ReadOnly = true;
            //    }
            //    else
            //    {
            //        txtDisI.ReadOnly = false;
            //        txtDisAmountI.ReadOnly = false;
            //    }
            btnAdd.Enabled = true;
            //}
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItemCodeI.Text, Convert.ToDecimal(txtQtyI.Text), Convert.ToDateTime(dtpDate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    btnAdd.Enabled = false;
                    return _IsTerminate;
                }
                else
                {
                    txtUnitPriceI.Text = FormatToCurrency("0");
                }
            }
            else
            {
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                        {
                            if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                            _priceDetailRef.Add(_p[0]);
                        }
                }
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                    if (_isSuspend > 0)
                    {
                        this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this))
                        { MessageBox.Show("Price has been suspended. Please contact IT dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        _IsTerminate = true;
                        //pnlMain.Enabled = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    //BindNonSerializedPrice(_priceDetailRef);
                    //pnlPriceNPromotion.Visible = true;
                    //_IsTerminate = true;
                    //pnlMain.Enabled = false;

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
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItemCodeI.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQtyI.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmountI.Text.Trim()), Convert.ToDecimal(txtDisI.Text), false), true);
                        txtUnitPriceI.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        //SetColumnForPriceDetailNPromotion(false);
                        //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                        //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                        //BindNonSerializedPrice(_priceDetailRef);

                        //if (gvPromotionPrice.RowCount > 0)
                        //{
                        //    //gvPromotionPrice_CellDoubleClick(0, false, false);
                        //    //pnlPriceNPromotion.Visible = true;
                        //    //pnlMain.Enabled = false;
                        //    //_IsTerminate = true;
                        //    //return _IsTerminate;
                        //}
                        //else
                        //{
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //}

                        //}
                        //else
                        //{
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //}
                    }
                }
            }
            _isEditPrice = false;
            _isEditDiscount = false;
            if (string.IsNullOrEmpty(txtQtyI.Text)) txtQtyI.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQtyI.Text);
            txtQtyI.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();

            //get price for priority pb
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtAmountI.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceType.Text, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, txtCustomer.Text, txtItemCodeI.Text, Convert.ToDecimal(txtQtyI.Text), Convert.ToDateTime(dtpDate.Text));
                string _unitPrice = "";
                if (_priceDetailRef.Count <= 0)
                {
                    return false;
                }

                if (_priceDetailRef.Count <= 0)
                {
                    if (!_isCompleteCode)
                    {
                        //this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this)) { MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //SetDecimalTextBoxForZero(true, false, true);
                        return false;
                    }
                    else
                    {
                        _unitPrice = FormatToCurrency("0");
                    }
                }
                ////////////else
                ////////////{
                ////////////    if (_isCompleteCode)
                ////////////    {
                ////////////        List<PriceDetailRef> _new = _priceDetailRef;
                ////////////        _priceDetailRef = new List<PriceDetailRef>();
                ////////////        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                ////////////        if (_p != null)
                ////////////            if (_p.Count > 0)
                ////////////            {
                ////////////                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                ////////////                _priceDetailRef.Add(_p[0]);
                ////////////            }
                ////////////    }
                ////////////    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                ////////////    {
                ////////////        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                ////////////        if (_isSuspend > 0)
                ////////////        {
                ////////////            return false;
                ////////////        }
                ////////////    }
                ////////////    if (_priceDetailRef.Count > 1)
                ////////////    {
                ////////////        /*
                ////////////        DialogResult _result = new DialogResult();
                ////////////        using (new CenterWinDialog(this)) { _result = MessageBox.Show("This item has " +_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Promotion."+"\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Promotion?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                ////////////        if (_result == DialogResult.Yes)
                ////////////        {
                ////////////            SetColumnForPriceDetailNPromotion(false);
                ////////////            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                ////////////            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                ////////////            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                ////////////            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                ////////////            BindNonSerializedPrice(_priceDetailRef);
                ////////////            pnlPriceNPromotion.Visible = true;
                ////////////            _IsTerminate = true;
                ////////////            pnlMain.Enabled = false;

                ////////////            return _IsTerminate;
                ////////////        }
                ////////////        else {
                ////////////            return false;
                ////////////        }
                ////////////        */
                ////////////        return false;
                ////////////    }
                ////////////    else if (_priceDetailRef.Count == 1)
                ////////////    {
                ////////////        var _one = from _itm in _priceDetailRef
                ////////////                   select _itm;
                ////////////        int _priceType = 0;
                ////////////        foreach (var _single in _one)
                ////////////        {
                ////////////            _priceType = _single.Sapd_price_type;
                ////////////            PriceTypeRef _promotion = TakePromotion(_priceType);
                ////////////            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmountI.Text.Trim()), Convert.ToDecimal(txtDisI.Text), false), true);
                ////////////            _unitPrice = FormatToCurrency(Convert.ToString(UnitPrice));
                ////////////            WarrantyRemarks = _single.Sapd_warr_remarks;
                ////////////            SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                ////////////            Int32 _pbSq = _single.Sapd_pb_seq;
                ////////////            Int32 _pbiSq = _single.Sapd_seq_no;
                ////////////            string _mItem = _single.Sapd_itm_cd;
                ////////////            //if (_promotion.Sarpt_is_com)
                ////////////            //{
                ////////////            //SetColumnForPriceDetailNPromotion(false);
                ////////////            //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                ////////////            //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                ////////////            //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                ////////////            //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                ////////////            //BindNonSerializedPrice(_priceDetailRef);

                ////////////            //if (gvPromotionPrice.RowCount > 0)
                ////////////            //{
                ////////////            //    //gvPromotionPrice_CellDoubleClick(0, false, false);
                ////////////            //    //pnlPriceNPromotion.Visible = true;
                ////////////            //    //pnlMain.Enabled = false;
                ////////////            //    //_IsTerminate = true;
                ////////////            //    //return _IsTerminate;
                ////////////            //}
                ////////////            //else
                ////////////            //{
                ////////////            //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                ////////////            //}

                ////////////            //}
                ////////////            //else
                ////////////            //{
                ////////////            //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                ////////////            //}
                ////////////        }
                ////////////    }
                ////////////}
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQtyI.Text)) txtQtyI.Text = FormatToQty("0");
                decimal vals1 = Convert.ToDecimal(txtQtyI.Text);
                txtQtyI.Text = FormatToQty(Convert.ToString(vals1));
                decimal otherPrice = 0;
                if (!string.IsNullOrEmpty(txtQtyI.Text) && !string.IsNullOrEmpty(_unitPrice))
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    if (!string.IsNullOrEmpty(txtDisI.Text))
                    {
                        _disRate = Convert.ToDecimal(txtDisI.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDisAmountI.Text))
                    {
                        _disAmt = Convert.ToDecimal(txtDisAmountI.Text);
                    }

                    otherPrice = CalculateItemTem(Convert.ToDecimal(txtQtyI.Text), Convert.ToDecimal(_unitPrice), _disAmt, _disRate);
                }
                else
                    return false;
                //decimal otherPrice = Convert.ToDecimal(txtLineTotAmt.Text);
                //if price change display message
                if (otherPrice < normalPrice)
                {
                    DialogResult _result = new DialogResult();
                    //using (new CenterWinDialog(this))
                    { _result = MessageBox.Show(_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + FormatToCurrency(otherPrice.ToString()) + "\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Price?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }

                    if (_result == DialogResult.Yes)
                    {
                        txtUnitPriceI.Text = FormatToCurrency("0");
                        txtValueI.Text = FormatToCurrency("0");
                        txtDisI.Text = FormatToCurrency("0");
                        txtDisAmountI.Text = FormatToCurrency("0");
                        txtTaxAmountI.Text = FormatToCurrency("0");
                        txtAmountI.Text = FormatToCurrency("0");
                        cmbBook.Text = _priorityPriceBook.Sppb_pb;
                        cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                        CheckQty(false);
                    }
                    else
                    {
                        //SSPRomotionType = 0;
                        //SSCirculerCode = string.Empty;
                        //SSPriceBookItemSequance = string.Empty;
                        //SSPriceBookPrice = Convert.ToDecimal(0);
                        //SSPriceBookSequance = string.Empty;
                        //SSPromotionCode = string.Empty;
                        /*
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));
                        if (_priceDetailRef.Count == 1)
                        {
                            var _one = from _itm in _priceDetailRef
                                       select _itm;
                            int _priceType = 0;
                            foreach (var _single in _one)
                            {
                                _priceType = _single.Sapd_price_type;
                                PriceTypeRef _promotion = TakePromotion(_priceType);
                                decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmountI.Text.Trim()), Convert.ToDecimal(txtDisI.Text), false), true);
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                WarrantyRemarks = _single.Sapd_warr_remarks;
                                SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                                Int32 _pbSq = _single.Sapd_pb_seq;
                                Int32 _pbiSq = _single.Sapd_seq_no;
                                string _mItem = _single.Sapd_itm_cd;
                                //if (_promotion.Sarpt_is_com)
                                //{
                                SetColumnForPriceDetailNPromotion(false);
                                gvNormalPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                                BindNonSerializedPrice(_priceDetailRef);

                                if (gvPromotionPrice.RowCount > 0)
                                {
                                    gvPromotionPrice_CellDoubleClick(0, false, false);
                                    pnlPriceNPromotion.Visible = true;
                                    pnlMain.Enabled = false;
                                    _IsTerminate = true;
                                    return _IsTerminate;
                                }
                                else
                                {
                                    if (_isCombineAdding == false) txtUnitPrice.Focus();
                                }

                                //}
                                //else
                                //{
                                //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                                //}
                            }
                        }
                        _isEditPrice = false;
                        _isEditDiscount = false;
                        if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                        decimal vals2 = Convert.ToDecimal(txtQty.Text);
                        txtQty.Text = FormatToQty(Convert.ToString(vals2));
                        CalculateItem();
                         */
                    }
                }
            }

            return _IsTerminate;
        }

        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(txtItemCodeI.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (IsNumeric(txtQtyI.Text) == false)
            {
                this.Cursor = Cursors.Default;
                {
                    MessageBox.Show("Please select the valid qty", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                _IsTerminate = true;
                txtQtyI.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQtyI.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            //if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //    txtQty.Text = FormatToQty("1");

            _MainPriceCombinItem = new List<PriceCombinedItemRef>();

            if (string.IsNullOrEmpty(txtQtyI.Text))
            {
                CalculateItem();
                //SSPriceBookItemSequance = "0";
                //SSPriceBookPrice = 0;
                //SSPriceBookSequance = "0";
                WarrantyPeriod = 0;
                WarrantyRemarks = string.Empty;
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (Convert.ToDecimal(txtQtyI.Text) <= 0)
            {
                CalculateItem();
                //SSPriceBookItemSequance = "0";
                //SSPriceBookPrice = 0;
                //SSPriceBookSequance = "0";
                WarrantyPeriod = 0;
                WarrantyRemarks = string.Empty;
                _IsTerminate = true;
                return _IsTerminate;
            }
            //if (string.IsNullOrEmpty(cmbInvType.Text))
            //{
            //    this.Cursor = Cursors.Default;
            //    //using (new CenterWinDialog(this))
            //    { MessageBox.Show("Please select the invoice type", "Invalid Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //    _IsTerminate = true;
            //    cmbInvType.Focus();
            //    return _IsTerminate;
            //}
            //if (string.IsNullOrEmpty(txtCustomer.Text))
            //{
            //    this.Cursor = Cursors.Default;
            //    //using (new CenterWinDialog(this))
            //    { MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //    _IsTerminate = true;
            //    txtCustomer.Focus();
            //    return _IsTerminate;
            //}
            if (string.IsNullOrEmpty(txtItemCodeI.Text))
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                txtItemCodeI.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                this.Cursor = Cursors.Default;
                // using (new CenterWinDialog(this))
                { MessageBox.Show("Price book not select.", "Invalid Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the price level", "Invalid Level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                this.Cursor = Cursors.Default;
                // using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the item status", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;
        }

        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
            }
        }

        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(txtItemCodeI.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCodeI.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItemCodeI.Text.Trim());

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

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQtyI.Text) && !string.IsNullOrEmpty(txtUnitPriceI.Text))
            {
                txtValueI.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPriceI.Text.Trim()) * Convert.ToDecimal(txtQtyI.Text.Trim()), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItemCodeI.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQtyI.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPriceI.Text.Trim()), Convert.ToDecimal(txtDisAmountI.Text.Trim()), Convert.ToDecimal(txtDisI.Text), true), true);
                txtTaxAmountI.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQtyI.Text) * Convert.ToDecimal(txtUnitPriceI.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisI.Text))
                {
                    bool _isVATInvoice = false;
                    if
                        (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available")
                        _isVATInvoice = true;
                    else
                        _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisI.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisI.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisI.Text) > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItemCodeI.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmountI.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }

                    txtDisAmountI.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmountI.Text))
                {
                    if (Convert.ToDecimal(txtDisI.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmountI.Text) - _disAmt, true);
                }

                txtAmountI.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return RoundUpForPlace(value, 2);
            else return Math.Round(value, 2);
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available")
                        _isVATInvoice = true;
                    else
                        _isVATInvoice = false;

                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (lblVatExemptStatus.Text != "Available")
                        {
                            if (_isTaxfaction == false)
                                _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            else
                                if (_isVATInvoice)
                                {
                                    _discount = _pbUnitPrice * _qty * _disRate / 100;
                                    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                        }
                        else
                        {
                            if (_isTaxfaction)
                                _pbUnitPrice = 0;
                        }
                    }
                }
                else
                    if (_isTaxfaction)
                        _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisI.Text = FormatToCurrency("0");
            txtDisAmountI.Text = FormatToCurrency("0");
            if (_isQty)
                txtQtyI.Text = FormatToQty("1");
            txtTaxAmountI.Text = FormatToCurrency("0");
            if (_isUnit)
                txtUnitPriceI.Text = FormatToCurrency("0");
            txtValueI.Text = FormatToCurrency("0");
            txtAmountI.Text = FormatToCurrency("0");
            if (_isAccBal)
            {
                //lblAccountBalance.Text = FormatToCurrency("0");
                //lblAvailableCredit.Text = FormatToCurrency("0");
            }
        }

        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            //if (_isSerializedPriceLevel)
            //{
            //    NorPrice_Select.Visible = true;

            //    NorPrice_Serial.DataPropertyName = "sars_ser_no";
            //    NorPrice_Serial.Visible = true;
            //    NorPrice_Item.DataPropertyName = "Sars_itm_cd";
            //    NorPrice_Item.Visible = true;
            //    NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
            //    NorPrice_UnitPrice.Visible = true;
            //    NorPrice_Circuler.DataPropertyName = "sars_circular_no";
            //    NorPrice_PriceType.DataPropertyName = "sars_price_type";
            //    NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
            //    NorPrice_ValidTill.DataPropertyName = "sars_val_to";
            //    NorPrice_ValidTill.Visible = true;
            //    NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
            //    NorPrice_PbLineSeq.DataPropertyName = "1";
            //    NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
            //    NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
            //    NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
            //    NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
            //    NorPrice_Book.DataPropertyName = "sars_pbook";
            //    NorPrice_Level.DataPropertyName = "sars_price_lvl";

            //    PromPrice_Select.Visible = true;

            //    PromPrice_Serial.DataPropertyName = "sars_ser_no";
            //    PromPrice_Serial.Visible = true;
            //    PromPrice_Item.DataPropertyName = "Sars_itm_cd";
            //    PromPrice_Item.Visible = true;
            //    PromPrice_UnitPrice.DataPropertyName = "sars_itm_price";
            //    PromPrice_UnitPrice.Visible = true;
            //    PromPrice_Circuler.DataPropertyName = "sars_circular_no";
            //    PromPrice_PriceType.DataPropertyName = "sars_price_type";
            //    PromPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
            //    PromPrice_ValidTill.DataPropertyName = "sars_val_to";
            //    PromPrice_ValidTill.Visible = true;
            //    PromPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
            //    //PromPrice_PbLineSeq.DataPropertyName = "1";
            //    PromPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
            //    PromPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
            //    PromPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
            //    PromPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
            //    PromPrice_Book.DataPropertyName = "sars_pbook";
            //    PromPrice_Level.DataPropertyName = "sars_price_lvl";
            //}
            //else
            //{
            //    NorPrice_Select.Visible = false;

            //    NorPrice_Serial.Visible = false;
            //    NorPrice_Item.DataPropertyName = "sapd_itm_cd";
            //    NorPrice_Item.Visible = true;
            //    NorPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
            //    NorPrice_UnitPrice.Visible = true;
            //    NorPrice_Circuler.DataPropertyName = "Sapd_circular_no";
            //    NorPrice_Circuler.Visible = true;
            //    NorPrice_PriceType.DataPropertyName = "Sarpt_cd";
            //    NorPrice_PriceTypeDescription.DataPropertyName = "SARPT_CD";
            //    NorPrice_ValidTill.DataPropertyName = "Sapd_to_date";
            //    NorPrice_ValidTill.Visible = true;
            //    NorPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
            //    NorPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
            //    NorPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
            //    NorPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
            //    NorPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
            //    NorPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
            //    NorPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
            //    NorPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";

            //    PromPrice_Select.Visible = true;

            //    PromPrice_Serial.Visible = false;
            //    PromPrice_Item.DataPropertyName = "sapd_itm_cd";
            //    PromPrice_Item.Visible = true;
            //    PromPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
            //    PromPrice_UnitPrice.Visible = true;
            //    PromPrice_Circuler.DataPropertyName = "Sapd_circular_no";
            //    PromPrice_Circuler.Visible = true;
            //    PromPrice_PriceType.DataPropertyName = "sapd_price_type"; //"Sarpt_cd";
            //    PromPrice_PriceTypeDescription.DataPropertyName = "Sarpt_cd";
            //    PromPrice_ValidTill.DataPropertyName = "Sapd_to_date";
            //    PromPrice_ValidTill.Visible = true;
            //    PromPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
            //    PromPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
            //    PromPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
            //    PromPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
            //    PromPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
            //    PromPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
            //    PromPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
            //    PromPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";
            //}
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

        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItemCodeI.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
            string tax = FormatToCurrency(Convert.ToString(_vatPortion));

            decimal _totalAmount = Convert.ToDecimal(_qty) * Convert.ToDecimal(_unitPrice);
            decimal _disAmt = 0;

            if (_disRt != 0)
            {
                bool _isVATInvoice = false;
                if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_disRt) / 100), true);
                else
                {
                    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_disRt) / 100), true);
                    if (Convert.ToDecimal(txtDisI.Text) > 0)
                    {
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItemCodeI.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        if (_tax != null && _tax.Count > 0)
                        {
                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        }
                    }
                }

                FormatToCurrency(Convert.ToString(_disAmt));
            }

            if (!string.IsNullOrEmpty(tax))
            {
                if (Convert.ToDecimal(txtDisI.Text) > 0)
                    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                else
                    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(tax) - _disAmt, true);
            }

            return _totalAmount;
        }

        private void cmbBook_Leave(object sender, EventArgs e)
        {
            try
            {
                // btnAddItem.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                LoadPriceLevel(txtInvoiceType.Text, cmbBook.Text);
                LoadLevelStatus(txtInvoiceType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            {
                ClearPriceTextBox();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void CheckPriceLevelStatusForDoAllow(string _level, string _book)
        {
            if (!string.IsNullOrEmpty(_level.Trim()) && !string.IsNullOrEmpty(_book.Trim()))
            {
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _bool = (from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp).ToList();
                        if (_bool != null && _bool.Count() > 0) IsPriceLevelAllowDoAnyStatus = false; else IsPriceLevelAllowDoAnyStatus = true;
                    }
            }
            else
                IsPriceLevelAllowDoAnyStatus = true;
        }

        private void ClearPriceTextBox()
        {
            txtUnitPriceI.Text = FormatToCurrency("0");
            txtValueI.Text = FormatToCurrency("0");
            txtDisI.Text = FormatToCurrency("0");
            txtDisAmountI.Text = FormatToCurrency("0");
            txtTaxAmountI.Text = FormatToCurrency("0");
            txtAmountI.Text = FormatToCurrency("0");
        }

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(txtInvoiceType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                CheckQty(false);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LoadPriceLevelMessage();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CheckLevelStatusWithInventoryStatus();
                lblcostPrice.Text = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCodeI.Text, cmbStatus.SelectedValue.ToString()).ToString("N");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void CheckLevelStatusWithInventoryStatus()
        {
            if (IsPriceLevelAllowDoAnyStatus == false)
            {
                string _invoiceStatus = cmbStatus.Text.Trim();
                string _inventoryStatus = string.Empty;
                //if (chkDeliverLater.Checked == false)
                //    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    //pick inventory status
                    if (!string.IsNullOrEmpty(txtItemCodeI.Text.Trim()))
                    {
                        List<InventoryLocation> _balance = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCodeI.Text.Trim(), _invoiceStatus);
                        if (_balance == null)
                        {
                            this.Cursor = Cursors.Default;
                            //using (new CenterWinDialog(this))
                            { MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            //cmbStatus.Text = "";
                            return;
                        }
                        if (_balance.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            // using (new CenterWinDialog(this))
                            { MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            //cmbStatus.Text = "";
                            return;
                        }
                    }
                }
                //else
                //{
                //    //pick serial status
                //    DataTable _serialstatus = CHNLSVC.Inventory.GetAvailableItemStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, DefaultBin, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                //    if (_serialstatus != null)
                //        if (_serialstatus.Rows.Count > 0)
                //        {
                //            _inventoryStatus = _serialstatus.Rows[0].Field<string>("ins_itm_stus");

                //            if (_levelStatus != null)
                //                if (_levelStatus.Rows.Count > 0)
                //                {
                //                    var _exist = _levelStatus.AsEnumerable().Where(x => x.Field<string>("Code") == _invoiceStatus).Select(y => y.Field<string>("Code")).ToList();
                //                    if (_exist != null)
                //                        if (_exist.Count > 0)
                //                        {
                //                            string _code = Convert.ToString(_exist[0]);
                //                            cmbStatus.Text = _code;
                //                            return;
                //                        }
                //                }

                //            if (!string.IsNullOrEmpty(_inventoryStatus))
                //                if (!_inventoryStatus.Equals(_invoiceStatus))
                //                {
                //                    this.Cursor = Cursors.Default;
                //                    using (new CenterWinDialog(this)) { MessageBox.Show("Selected price level restricted to deliver with the same item status in the invoice. There is no available qty for this status.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                //                    cmbStatus.Text = "";
                //                    return;
                //                }
                //        }
                //}
            }
        }

        private void txtQtyI_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem) return;

            try
            {
                if (Convert.ToDecimal(txtQtyI.Text.Trim()) < 0)
                {
                    MessageBox.Show("Quantity should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (IsNumeric(txtQtyI.Text))
                {
                    if (_isDecimalAllow == false)
                    {
                        if ((Convert.ToDecimal(txtQtyI.Text.Trim()) % 1) != 0)
                        {
                            MessageBox.Show("Decimal is not allowed for this item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtQtyI.Focus();
                        }
                        txtQtyI.Text = decimal.Truncate(Convert.ToDecimal(txtQtyI.Text.ToString())).ToString();
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                //CheckQty(false);
            }
            catch (Exception ex)
            {
                txtQtyI.Text = FormatToQty("1");
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUnitPriceI_Leave(object sender, EventArgs e)
        {
            if (txtUnitPriceI.ReadOnly) return;

            if (_IsVirtualItem)
            {
                CalculateItem();
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPriceI.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return;
                }

                if (string.IsNullOrEmpty(txtItemCodeI.Text))
                    return;
                if (IsNumeric(txtQtyI.Text) == false)
                {
                    this.Cursor = Cursors.Default;

                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                if (Convert.ToDecimal(txtQtyI.Text.Trim()) == 0)
                    return;

                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    if (string.IsNullOrEmpty(txtUnitPriceI.Text))
                        txtUnitPriceI.Text = FormatToCurrency("0");
                    decimal vals = Convert.ToDecimal(txtUnitPriceI.Text);
                    txtUnitPriceI.Text = FormatToCurrency(Convert.ToString(vals));
                    CalculateItem();
                    return;
                }
                //if (!_isCompleteCode)
                //{
                //    //check minus unit price validation
                //    decimal _unitAmt = 0;
                //    bool _isUnitAmt = Decimal.TryParse(txtUnitPrice.Text, out _unitAmt);
                //    if (!_isUnitAmt)
                //    {
                //        //using (new CenterWinDialog(this))
                //        {
                //            MessageBox.Show("Unit Price has to be number!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        }
                //        return;
                //    }
                //    if (_unitAmt <= 0)
                //    {
                //        // using (new CenterWinDialog(this))
                //        {
                //            MessageBox.Show("Unit Price has to be greater than 0!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        }
                //        return;
                //    }

                //    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                //    {
                //        decimal _pb_price;
                //        //if (SSPriceBookPrice == 0)
                //        //{
                //        //    this.Cursor = Cursors.Default;
                //        //    //using (new CenterWinDialog(this))
                //        //    {
                //        //        MessageBox.Show("Price not define. Please check the system updated price.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        //    }
                //        //    txtUnitPrice.Text = FormatToCurrency("0");
                //        //    return;
                //        //}

                //        _pb_price = SSPriceBookPrice;

                //        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);

                //        if (_MasterProfitCenter.Mpc_edit_price)
                //        {
                //            if (_pb_price > _txtUprice)
                //            {
                //                decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                //                if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                //                {
                //                    this.Cursor = Cursors.Default;
                //                    //using (new CenterWinDialog(this))
                //                    {
                //                        MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    }
                //                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                //                    _isEditPrice = false;
                //                    return;
                //                }
                //                else
                //                {
                //                    _isEditPrice = true;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                //            _isEditPrice = false;
                //        }
                //    }
                //}
                //if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                decimal val = Convert.ToDecimal(txtUnitPriceI.Text);
                txtUnitPriceI.Text = FormatToCurrency(Convert.ToString(val));
                CalculateItem();
            }
            catch (Exception ex)
            {
                txtUnitPriceI.Text = FormatToCurrency("0");
                CalculateItem(); this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDisI_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem)
            {
                txtDisI.Clear();
                txtDisAmountI.Clear();
                txtDisAmountI.Text = FormatToCurrency("0");
                txtDisI.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtDisI.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisI.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                    //using (new CenterWinDialog(this))
                    { MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtDisI.Clear();
                    txtDisI.Text = FormatToQty("0");
                    return;
                }

                CheckNewDiscountRate(string.Empty);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        protected bool CheckNewDiscountRate(string txtBox)
        {
            if (string.IsNullOrEmpty(txtItemCodeI.Text)) return false;
            if (IsNumeric(txtQtyI.Text) == false)
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }
            if (Convert.ToDecimal(txtQtyI.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisI.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisI.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null)
                        GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    // if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItemCodeI.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    }
                    //else
                    //{
                    //    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCustomerCode.Text.Trim(), Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
                    //    if (GeneralDiscount != null)
                    //    {
                    //        _IsPromoVou = true;
                    //        GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                    //    }
                    //}
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    // using (new CenterWinDialog(this))
                        //    {
                        //        MessageBox.Show("Voucher already used!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    txtDisRate.Text = FormatToCurrency("0");
                        //    _isEditDiscount = false;
                        //    return false;
                        //}

                        //if (_IsPromoVou == true)
                        //{
                        //    if (rates == 0 && vals > 0)
                        //    {
                        //        CalculateItem();
                        //        if (Convert.ToDecimal(txtDisAmt.Text) != vals)
                        //        {
                        //            this.Cursor = Cursors.Default;
                        //            //using (new CenterWinDialog(this))
                        //            {
                        //                MessageBox.Show("Voucher discuount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //            }
                        //            txtDisRate.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            _isEditDiscount = false;
                        //            return false;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (rates != _disRate)
                        //        {
                        //            CalculateItem();
                        //            this.Cursor = Cursors.Default;
                        //            //using (new CenterWinDialog(this))
                        //            {
                        //                MessageBox.Show("Voucher discuount rate should be " + rates + "% !.\nNot allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //            }
                        //            txtDisRate.Text = FormatToCurrency("0");
                        //            CalculateItem();
                        //            _isEditDiscount = false;
                        //            return false;
                        //        }
                        //    }
                        //}
                        //else
                        {
                            if (rates < _disRate)
                            {
                                if (txtBox == "disCountAmount")
                                {
                                    CalculateItem();
                                    this.Cursor = Cursors.Default;
                                    //using (new CenterWinDialog(this))
                                    {
                                        decimal asd = Convert.ToDecimal(txtAmountI.Text);
                                        MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + " discounted price is " + asd.ToString("N"), "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    txtDisI.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                                else
                                {
                                    CalculateItem();
                                    this.Cursor = Cursors.Default;
                                    //using (new CenterWinDialog(this))
                                    {
                                        MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtAmountI.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    txtDisI.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this))
                        {
                            MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        txtDisI.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            //lblPromoVouUsedFlag.Text = "U";
                            _proVouInvcItem = txtItemCodeI.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisI.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisI.Text))
                txtDisI.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisI.Text);
            txtDisI.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            //btnAddItem.Focus();
            return true;
        }

        private void txtDisAmountI_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem)
            {
                txtDisI.Clear();
                txtDisAmountI.Clear();
                txtDisAmountI.Text = FormatToCurrency("0");
                txtDisI.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtDisAmountI.Text)) return;
                this.Cursor = Cursors.WaitCursor;
                if (Convert.ToDecimal(txtDisAmountI.Text) < 0)
                {
                    //MessageBox.Show("Discount amount should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisAmt.Clear();
                    //txtDisAmt.Text = FormatToQty("0");
                    //return;
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItemCodeI.Text))
            {
                return false;
            }
            if (IsNumeric(txtQtyI.Text) == false)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToDecimal(txtQtyI.Text.Trim()) == 0)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(txtDisAmountI.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQtyI.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDisAmountI.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPriceI.Text);
                decimal _qty = Convert.ToDecimal(txtQtyI.Text);
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
                            Cursor = Cursors.Default;
                            MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisAmountI.Text = FormatToCurrency("0");
                            txtDisI.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0)
                            {
                                txtDisI.Text = "0";
                            }

                            CalculateItem();

                            if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0)
                            {
                                _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtAmountI.Text)) * 100 : 0;
                            }
                            if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0)
                            {
                                txtDisI.Text = FormatToCurrency(Convert.ToString(_percent));
                            }

                            CalculateItem();

                            CheckNewDiscountRate("disCountAmount");

                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        //bool _IsPromoVou = false;
                        //if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItemCodeI.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        }
                        //else
                        //{
                        //    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCustomerCode.Text.Trim(), Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
                        //    if (GeneralDiscount != null)
                        //    {
                        //        _IsPromoVou = true;
                        //        GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                        //    }
                        //}

                        if (GeneralDiscount != null)
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            //if (_IsPromoVou == true)
                            //{
                            //    if (vals < _disAmt && rates == 0)
                            //    {
                            //        this.Cursor = Cursors.Default;
                            //        MessageBox.Show("Voucher discuount amount should be " + vals + "!./nNot allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text, "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtDisAmt.Text = FormatToCurrency("0");
                            //        txtDisRate.Text = FormatToCurrency("0");
                            //        _isEditDiscount = false;
                            //        return false;
                            //    }
                            //}

                            if (vals < _disAmt && rates == 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDisAmountI.Text = FormatToCurrency("0");
                                txtDisI.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0) txtDisI.Text = "0";
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisI.Text) && Convert.ToDecimal(txtDisI.Text) == 0) txtDisI.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckNewDiscountRate(string.Empty);
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;

                            MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtDisAmountI.Text = FormatToCurrency("0");
                            txtDisI.Text = FormatToCurrency("0");
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
                txtDisAmountI.Text = FormatToCurrency("0");
                txtDisI.Text = FormatToCurrency("0");
            }

            if (string.IsNullOrEmpty(txtDisAmountI.Text)) txtDisAmountI.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmountI.Text);
            txtDisAmountI.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbLevel.Focus();
            }
        }

        private void cmbLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbStatus.Focus();
            }
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtyI.Focus();
            }
        }

        private void txtQtyI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitPriceI.Focus();
            }
        }

        private void txtQtyI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtUnitPriceI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValueI.Focus();
            }
        }

        private void txtUnitPriceI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtValueI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisI.Focus();
        }

        private void txtValueI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDisI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisAmountI.Focus();
            }
        }

        private void txtDisI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDisAmountI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTaxAmountI.Focus();
        }

        private void txtDisAmountI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtTaxAmountI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAmountI.Focus();
        }

        private void txtTaxAmountI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtAmountI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        private void txtAmountI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void LoadCachedObjects()
        {
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}