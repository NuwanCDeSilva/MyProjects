using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class JobEstimate : Base
    {
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private DataTable _levelStatus = null;
        private string DefaultInvoiceType = string.Empty;
        private string DefaultStatus = string.Empty;
        private PriceBookLevelRef _priceBookLevelRef = null;
        private bool _IsVirtualItem = false;
        private bool IsSaleFigureRoundUp = false;
        private bool _isCompleteCode = false;
        private MasterProfitCenter _MasterProfitCenter = null;
        private DataTable MasterChannel = null;
        private bool _isEditDiscount = false;
        public decimal SSPriceBookPrice = 0;
        private bool _isEditPrice = false;
        private CashGeneralEntiryDiscountDef GeneralDiscount = null;
        private string _proVouInvcItem = string.Empty;
        private bool _isItemChecking = false;
        private MasterItem _itemdetail = null;
        private bool IsPriceLevelAllowDoAnyStatus = false;
        private bool _isBlocked = false;
        private string WarrantyRemarks = string.Empty;
        private Int32 WarrantyPeriod = 0;
        private Dictionary<decimal, decimal> ManagerDiscount = null;
        private List<MasterItemComponent> _masterItemComponent = null;
        private bool _isInventoryCombineAdded = false;
        private List<PriceCombinedItemRef> _MainPriceCombinItem = null;
        private List<PriceDetailRef> _priceDetailRef = null;
        private PriortyPriceBook _priorityPriceBook = null;
        private bool _isRegistrationMandatory = false;
        private bool _isDecimalAllow = false;
        private MasterBusinessEntity _masterBusinessCompany;
        private DateTime _serverDt = DateTime.Now.Date;
        private Boolean _isStrucBaseTax = false;

        private decimal GrndDiscount = 0;
        private decimal GrndTax = 0;
        private decimal _toBePayNewAmount = 0;

        private decimal GrndSubTotal = 0;

        public string SSCirculerCode = string.Empty;
        public string SSPriceBookItemSequance = string.Empty;
        public string SSPriceBookSequance = string.Empty;
        public string SSPromotionCode = string.Empty;
        public Int32 SSPRomotionType = 0;

        private List<MasterItemTax> MainTaxConstant = null;

        private List<Service_Estimate_Item> oEstimate_Items = new List<Service_Estimate_Item>();

        private string _salesType = "CS";       //kapila  25/5/2017

        public JobEstimate()
        {
            InitializeComponent();
            dgvEstimateItems.AutoGenerateColumns = false;
            dgvJobDetails.AutoGenerateColumns = false;

            dgvEstimateItems.Columns["Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["UnitPrice"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["UnitAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["DiscountRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["DiscountAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["TaxAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["LineAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimateItems.Columns["Cost"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvEstimateItems.Columns["Qty"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["UnitPrice"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["UnitAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["DiscountRate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["DiscountAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["TaxAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["LineAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEstimateItems.Columns["Cost"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //kapila 25/5/2017
            if (BaseCls.GlbUserComCode == "AAL")
                _salesType = "HSCA";
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
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2,3,5,6,4" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void JobEstimate_Load(object sender, EventArgs e)
        {
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                SerialNo.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvJobDetails.Columns["SerialNo"].HeaderText = _Parameters.SP_DB_SERIAL;
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

            GetEstimateType();
            clearAll();

            try
            {
                bool _alwCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _alwCurrentTrans);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            LoadCachedObjects();
            LoadPriceDefaultValue();

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

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10096))
            {
                label45.Visible = false;
                lblcostPrice.Visible = false;
                groupBox6.Visible = false;
                dgvEstimateItems.Columns["Cost"].Visible = false;
            }
            else
            {
                label45.Visible = true;
                lblcostPrice.Visible = true;
                groupBox6.Visible = true;
                dgvEstimateItems.Columns["Cost"].Visible = true;
            }

            txtJobNo.Focus();
        }

        #region events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateSave())
            {
                return;
            }

            if (CheckServerDateTime() == false) return;

            if (MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.Text, dtpDate, lblH1, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpDate.Focus();
                    return;
                }
            }

            if (oEstimate_Items.Count > 0)
            {
                if (checkNonPrintItems() && string.IsNullOrEmpty(txtPrintContains.Text))
                {
                    MessageBox.Show("There are non-print items. Please add print contian text", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrintContains.Focus();
                    return;
                }
                else
                {
                    Service_Estimate_Header oHeader = new Service_Estimate_Header();
                    oHeader.ESH_ESTNO = txtEstimateNo.Text.Trim();
                    oHeader.ESH_DT = dtpDate.Value.Date;
                    oHeader.ESH_COM = BaseCls.GlbUserComCode;
                    oHeader.ESH_LOC = BaseCls.GlbUserDefLoca;
                    oHeader.EST_PC = BaseCls.GlbUserDefProf;
                    oHeader.ESH_TP = cmbEstimateType.SelectedValue.ToString();
                    oHeader.ESH_JOB_NO = dgvJobDetails.SelectedRows[0].Cells["JobNo"].Value.ToString();
                    oHeader.EST_STUS = "P";
                    oHeader.EST_APP = 0;
                    oHeader.EST_APP_BY = BaseCls.GlbUserID;
                    oHeader.EST_APP_DT = DateTime.Today.Date;
                    oHeader.EST_RMK = (txtRemark.Text == "") ? "  " : txtRemark.Text;
                    oHeader.EST_MAN_REF = lblManualRefNo.Text;
                    oHeader.EST_DURATION = Convert.ToInt32(txtDuration.Text);
                    oHeader.EST_CRE_BY = BaseCls.GlbUserID;
                    oHeader.EST_CRE_DT = DateTime.Now;
                    oHeader.EST_MOD_BY = BaseCls.GlbUserID;
                    oHeader.EST_MOD_DT = DateTime.Now;
                    oHeader.EST_PRINT_RMK = txtPrintContains.Text;
                    oHeader.ESH_SEQ_NO = Convert.ToInt32((lblSeq.Text == "") ? "-99" : lblSeq.Text);

                    oHeader.EST_CUST_CD = lblCustomerCode.Text;
                    oHeader.EST_CUST_TP = (_masterBusinessCompany.Mbe_tp == null) ? "C" : _masterBusinessCompany.Mbe_tp;
                    oHeader.EST_IS_TAX = Convert.ToDecimal(_masterBusinessCompany.Mbe_is_tax);
                    oHeader.EST_TAX_NO = _masterBusinessCompany.Mbe_tax_no;
                    oHeader.EST_TAX_EX = Convert.ToDecimal(_masterBusinessCompany.Mbe_tax_ex);
                    oHeader.EST_IS_SVAT = Convert.ToDecimal(_masterBusinessCompany.Mbe_is_svat);
                    oHeader.EST_SVAT_NO = _masterBusinessCompany.Mbe_svat_no;

                    string startChar = "SEST";
                    string modID = "SEST";

                    DataTable oTypes = CHNLSVC.CustService.getEstimateTypesDataTable(cmbEstimateType.SelectedValue.ToString());
                    if (oTypes.Rows.Count > 0)
                    {
                        startChar = oTypes.Rows[0]["ety_st_chr"].ToString();
                        modID = oTypes.Rows[0]["ety_mod_id"].ToString();
                    }

                    MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                    _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _ReqInsAuto.Aut_cate_tp = "PC";
                    _ReqInsAuto.Aut_direction = null;
                    _ReqInsAuto.Aut_modify_dt = null;
                    _ReqInsAuto.Aut_moduleid = modID;
                    _ReqInsAuto.Aut_number = 0;
                    _ReqInsAuto.Aut_start_char = startChar;
                    _ReqInsAuto.Aut_year = DateTime.Today.Year;

                    string estNumber = string.Empty;
                    int result = CHNLSVC.CustService.SAVE_ServiceHeader(oHeader, _ReqInsAuto, oEstimate_Items, out estNumber);

                    if (result > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Record insert successfully.\n Estimate No :" + estNumber, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                        
                        return;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Process Terminated." + "\nError is " + estNumber, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add items..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEstimateNo.Text))
            {
                if (MessageBox.Show("Do you want to aprove this estimate?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtEstimateNo.Text))
                    {
                        Cursor = Cursors.WaitCursor;
                        Int32 result = CHNLSVC.CustService.Update_Estimate_HEaderStatus("A", txtEstimateNo.Text, BaseCls.GlbUserID, BaseCls.GlbUserComCode);
                        Cursor = Cursors.Default;

                        if (result > 0)
                        {
                            MessageBox.Show("Record successfully approved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string vEstno = txtEstimateNo.Text;
                            clearAll();

                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            FF.WindowsERPClient.Reports.Service.ReportViewerSVC _viewsvc = new FF.WindowsERPClient.Reports.Service.ReportViewerSVC();
                            BaseCls.GlbReportTp = "EST";
                            _viewsvc.GlbReportName = "Job_Estimate.rpt";
                            _viewsvc.GlbReportDoc = vEstno;
                            BaseCls.GlbReportDoc = vEstno;
                            _viewsvc.Show();
                            _viewsvc = null;

                            return;
                        }
                        else
                        {
                            MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEstimateNo.Focus();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEstimateNo.Text))
            {
                MessageBox.Show("Please select a estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEstimateNo.Focus();
                return;
            }

            if (MessageBox.Show("Do you want to cancel this estimate?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(txtEstimateNo.Text))
                {
                    Int32 result = CHNLSVC.CustService.Update_Estimate_HEaderStatus("C", txtEstimateNo.Text, BaseCls.GlbUserID, BaseCls.GlbUserComCode);

                    if (result > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Estimate successfully canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                        return;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor = Cursors.Default;
                    txtEstimateNo.Focus();
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void btnJobSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                txtJobNo_DoubleClick(null, null);
            }
            else
            {
                txtJobNo_Leave(null, null);
            }
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DateTime dtTemp = DateTime.Today.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
            _CommonSearch.dtpFrom.Value = dtTemp;
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNo.Focus();
            txtJobNo_Leave(null, null);
        }

        private void txtJobNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                txtJobNo_Leave(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Do you want to close?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            GetJobDetails();
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtItem;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception ex)
            { txtItem.Clear(); this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            //////////if (lblCustomerCode.Text == "")
            //////////{
            //////////    txtItem.Clear();
            //////////    txtJobNo.Focus();
            //////////    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////////    return;
            //////////}
            //////////if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;

            //////////if (_isItemChecking)
            //////////{
            //////////    _isItemChecking = false;
            //////////    return;
            //////////}
            //////////_isItemChecking = true;
            //////////try
            //////////{
            //////////    this.Cursor = Cursors.WaitCursor;
            //////////    if (!LoadItemDetail(txtItem.Text.Trim()))
            //////////    {
            //////////        this.Cursor = Cursors.Default;

            //////////        //using (new CenterWinDialog(this))

            //////////        {
            //////////            MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////////        }

            //////////        txtItem.Clear();
            //////////        txtItem.Focus();

            //////////        //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false)
            //////////        if (IsPriceLevelAllowDoAnyStatus == false) // && chkDeliverLater.Checked == false)
            //////////           // cmbStatus.Text = "";
            //////////        return;
            //////////    }

            //////////    //if (_itemdetail.Mi_is_ser1 == 1 && IsGiftVoucher(_itemdetail.Mi_itm_tp))
            //////////    //{
            //////////    //    if (string.IsNullOrEmpty(txtSerialNo.Text))
            //////////    //    {
            //////////    //        this.Cursor = Cursors.Default;
            //////////    //        using (new CenterWinDialog(this))
            //////////    //        {
            //////////    //            MessageBox.Show("Please select the gift voucher number", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////////    //        }
            //////////    //        txtItem.Clear();
            //////////    //        txtSerialNo.Clear();
            //////////    //    }

            //////////    //    return;
            //////////    //}
            //////////    IsVirtual(_itemdetail.Mi_itm_tp);

            //////////    //if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false))
            //////////    //{
            //////////    //    this.Cursor = Cursors.Default;

            //////////    //    using (new CenterWinDialog(this))
            //////////    //    {
            //////////    //        MessageBox.Show("You have to select the serial no for the serialized item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////////    //    }
            //////////    //    return;
            //////////    //}
            //////////    //if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater.Checked == true && chkDeliverNow.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) && _isRegistrationMandatory)
            //////////    //{
            //////////    //    this.Cursor = Cursors.Default;
            //////////    //    using (new CenterWinDialog(this))
            //////////    //    {
            //////////    //        MessageBox.Show("Registration mandatory items can not save without serial", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////////    //    }

            //////////    //    return;
            //////////    //}

            //////////    //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";

            //////////    //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) txtQty.Text = FormatToQty("0"); else
            //////////    //if (txtSerialNo.Text != "")
            //////////    //{
            //////////    //    txtQty.Text = FormatToQty("1");
            //////////    //}
            //////////    if (_IsVirtualItem)
            //////////    {
            //////////        bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
            //////////        if (block)
            //////////        {
            //////////            txtItem.Text = "";
            //////////        }
            //////////    }
            //////////    CheckQty(true);
            //////////    btnAddItem.Focus();
            //////////}
            //////////catch (Exception ex)
            //////////{ this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            //////////finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }

            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            if (_isItemChecking) { _isItemChecking = false; return; }
            _isItemChecking = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    //if (IsPriceLevelAllowDoAnyStatus == false)
                    //    cmbStatus.Text = "";
                    return;
                }

                IsVirtual(_itemdetail.Mi_itm_tp);

                //if ((_itemdetail.Mi_is_ser1 == 1 && _priceBookLevelRef.Sapl_is_serialized == false))
                //{
                //    this.Cursor = Cursors.Default;

                //    MessageBox.Show("You have to select the serial no for the serialized item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    return;
                //}
                //if ((_itemdetail.Mi_is_ser1 == 1 && _priceBookLevelRef.Sapl_is_serialized == false) && _isRegistrationMandatory)
                //{
                //    this.Cursor = Cursors.Default;
                //    MessageBox.Show("Registration mandatory items can not save without serial", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";

                //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) txtQty.Text = FormatToQty("0"); else
                // if (txtSerialNo.Text != "")
                {
                    txtQty.Text = FormatToQty("1");
                }
                if (_IsVirtualItem)
                {
                    bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
                    if (block)
                    {
                        txtItem.Text = "";
                    }
                }
                CheckQty(true);
                btnAddItem.Focus();
                //Tharanga 2017/06/19

                MasterItem _itmMas = new MasterItem();
                _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
               
                    if (_itmMas.Mi_is_editlongdesc == true)
                    {
                        txtItemDescription.Visible = true;
                        txtItemDescription.Text = _itmMas.Mi_longdesc;
                    }
                    else
                    {
                        txtItemDescription.Visible = false;
                    }


            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                cmbBook.Focus();
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem) return;

            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    MessageBox.Show("Quantity should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (IsNumeric(txtQty.Text))
                {
                    if (_isDecimalAllow == false)
                    {
                        if ((Convert.ToDecimal(txtQty.Text.Trim()) % 1) != 0)
                        {
                            MessageBox.Show("Decimal is not allowed for this item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtQty.Focus();
                        }
                        txtQty.Text = decimal.Truncate(Convert.ToDecimal(txtQty.Text.ToString())).ToString();
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                //CheckQty(false);
            }
            catch (Exception ex)
            {
                txtQty.Text = FormatToQty("1");
                this.Cursor = Cursors.Default;
                SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            if (txtUnitPrice.ReadOnly) return;

            if (_IsVirtualItem)
            {
                CalculateItem();
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                    return;
                if (IsNumeric(txtQty.Text) == false)
                {
                    this.Cursor = Cursors.Default;

                    //using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
                    return;

                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    if (string.IsNullOrEmpty(txtUnitPrice.Text))
                        txtUnitPrice.Text = FormatToCurrency("0");
                    decimal vals = Convert.ToDecimal(txtUnitPrice.Text);
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(vals));
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
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
                CalculateItem();
            }
            catch (Exception ex)
            {
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem(); this.Cursor = Cursors.Default;
                SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitAmt.Focus();
            }
        }

        private void txtUnitAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisRate.Focus();
        }

        private void txtDisRate_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem)
            {
                txtDisRate.Clear();
                txtDisAmt.Clear();
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                {
                    if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                    {
                        this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this))
                        { MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Clear();
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtQty.Text) != 1)
                    {
                        this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this))
                        { MessageBox.Show("Promotion voucher allow for only one(1) item!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Clear();
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                CheckDiscountRate();
                //CheckNewDiscountRate(string.Empty);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTaxAmt.Focus();
        }

        private void txtDisAmt_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem)
            {
                txtDisRate.Clear();
                txtDisAmt.Clear();
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtDisAmt.Text)) return;
                this.Cursor = Cursors.WaitCursor;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                    //MessageBox.Show("Discount amount should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisAmt.Clear();
                    //txtDisAmt.Text = FormatToQty("0");
                    //return;
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtTaxAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLineTotAmt.Focus();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select a item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            if (_MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
            {
                PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                if (_lsts != null)
                {
                    if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                    {
                        MessageBox.Show(txtItem.Text + " does not available price. Please contact IT dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //else
                    //{
                    //    decimal _tax = 0;
                    //    if (MainTaxConstant != null && MainTaxConstant.Count > 0)
                    //    {
                    //        _tax = MainTaxConstant[0].Mict_tax_rate;
                    //    }

                    //    decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * _tax, true);
                    //    decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                    //    if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                    //        if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                    //            if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                    //                if (!_MasterProfitCenter.Mpc_edit_price)
                    //                {
                    //                    //comment by darshana 23-08-2013
                    //                    if (Math.Round(sysUPrice, 2) != Math.Round(pickUPrice, 2))
                    //                    {
                    //                        this.Cursor = Cursors.Default;
                    //                        MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //                        return;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (sysUPrice != pickUPrice)
                    //                        if (sysUPrice > pickUPrice)
                    //                        {
                    //                            decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
                    //                            decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                    //                            if (ddUprice > pickUPrice)
                    //                            {
                    //                                MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //                                return;
                    //                            }
                    //                        }
                    //                }
                    //}
                }
            }

            if (ValidatItemAdd())
            {
                return;
            }
            if (oEstimate_Items.Count > 0)
            {
                if (oEstimate_Items.FindAll(x => x.ESI_ITM_CD == txtItem.Text && x.ESI_PLVL == cmbLevel.SelectedValue.ToString()
                                         && x.ESI_ITM_STUS == cmbStatus.SelectedValue.ToString()
                                         && x.ESI_PB == cmbBook.SelectedValue.ToString()
                                         && x.ESI_DISC_RT == Convert.ToDecimal(txtDisRate.Text)).Count > 0)
                {
                    Service_Estimate_Item oItemExist = oEstimate_Items.Find(x => x.ESI_ITM_CD == txtItem.Text && x.ESI_PLVL == cmbLevel.SelectedValue.ToString() && x.ESI_ITM_STUS == cmbStatus.SelectedValue.ToString() && x.ESI_PB == cmbBook.SelectedValue.ToString() && x.ESI_DISC_RT == Convert.ToDecimal(txtDisRate.Text));
                    if (oItemExist != null)
                    {
                        oItemExist.ESI_SEQ_NO = 0;
                        oItemExist.ESI_ESTNO = txtEstimateNo.Text;
                        oItemExist.ESI_REQNO = lblReqNo.Text;
                        oItemExist.ESI_REQLINE = 0;
                        oItemExist.ESI_JOBNO = dgvJobDetails.SelectedRows[0].Cells["JobNo"].Value.ToString();
                        oItemExist.ESI_JOBLINE = Convert.ToInt32(dgvJobDetails.SelectedRows[0].Cells["JobLine"].Value.ToString());
                        oItemExist.ESI_LINE = (oEstimate_Items.Count);
                        oItemExist.ESI_ITM_CD = txtItem.Text;
                        oItemExist.ESI_ITM_STUS = cmbStatus.SelectedValue.ToString();
                        oItemExist.ESI_ITM_TP = lblItemType.Text;
                        oItemExist.ESI_UOM = lblItemUOM.Text;
                        oItemExist.ESI_QTY += Convert.ToDecimal(txtQty.Text);
                        oItemExist.ESI_PB = cmbBook.SelectedValue.ToString();
                        oItemExist.ESI_PLVL = cmbLevel.SelectedValue.ToString();
                        oItemExist.ESI_PBPRICE = Convert.ToDecimal(txtUnitPrice.Text);   /// price book price
                        oItemExist.ESI_UNIT_RT += Convert.ToDecimal(txtUnitPrice.Text);
                        oItemExist.ESI_DISC_RT = Convert.ToDecimal(txtDisRate.Text);
                        oItemExist.ESI_DISC_AMT += Convert.ToDecimal(txtDisAmt.Text);
                        oItemExist.ESI_TAX_AMT += Convert.ToDecimal(txtTaxAmt.Text);
                        oItemExist.ESI_NET += Convert.ToDecimal(txtLineTotAmt.Text);
                        oItemExist.ESI_ACTIVE = 1;
                        

                        //oItem.ESI_SEQ = "";
                        //oItem.ESI_ITM_SEQ = "";
                        //oItem.ESI_RMK = "";
                        //oItem.ESI_ISSUE_QTY = "";

                        oItemExist.ESI_UNIT_COST = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, oItemExist.ESI_ITM_STUS);

                        oItemExist.ESI_PRINT = 0;
                        //Tharanga 
                        MasterItem _itmMas = new MasterItem();
                        _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        if (_itmMas.Mi_is_editlongdesc == true)
                        {
                            oItemExist.ESI_ITM_Description = txtItemDescription.Text.Trim();
                        }
                        else
                        {
                            oItemExist.ESI_ITM_Description = lblItemDescription.Text.Split(':')[1].Trim();
                        }
                      //  oItemExist.ESI_ITM_Description = lblItemDescription.Text.Split(':')[1].Trim();

                        if (lblIsEditDesctiption.Text.ToUpper() == true.ToString().ToUpper())
                        {
                            oItemExist.isEditDescription = true;
                        }
                        else
                        {
                            oItemExist.isEditDescription = false;
                        }
                        CalculateGrandTotal(oItemExist.ESI_QTY, oItemExist.ESI_UNIT_RT, oItemExist.ESI_DISC_AMT, oItemExist.ESI_TAX_AMT, true);
                        ClearMiddle1p0();
                        calculateCostAndRevenue();
                        BindAddItem();
                        ModifyGrid();
                        calculateTotals();

                        if (MessageBox.Show("Do you want to add another item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            txtItem.Focus();
                        }
                        else
                        {
                            toolStrip1.Focus();
                            btnSave.Select();
                        }
                        return;
                    }
                    return;
                }
            }
            Service_Estimate_Item oItem = new Service_Estimate_Item();
            oItem.ESI_SEQ_NO = 0;
            oItem.ESI_ESTNO = txtEstimateNo.Text;
            oItem.ESI_REQNO = "";
            oItem.ESI_REQLINE = 0;
            oItem.ESI_JOBNO = dgvJobDetails.SelectedRows[0].Cells["JobNo"].Value.ToString();
            oItem.ESI_JOBLINE = Convert.ToInt32(dgvJobDetails.SelectedRows[0].Cells["JobLine"].Value.ToString());
            oItem.ESI_LINE = (oEstimate_Items.Count + 1);
            oItem.ESI_ITM_CD = txtItem.Text;
            oItem.ESI_ITM_STUS = cmbStatus.SelectedValue.ToString();
            oItem.ESI_ITM_TP = lblItemType.Text;
            oItem.ESI_UOM = lblItemUOM.Text;
            oItem.ESI_QTY = Convert.ToDecimal(txtQty.Text);
            oItem.ESI_PB = cmbBook.SelectedValue.ToString();
            oItem.ESI_PLVL = cmbLevel.SelectedValue.ToString();
            oItem.ESI_PBPRICE = Convert.ToDecimal(txtUnitPrice.Text);   /// price book price
            oItem.ESI_UNIT_RT = Convert.ToDecimal(txtUnitAmt.Text);
            oItem.ESI_DISC_RT = Convert.ToDecimal(txtDisRate.Text);
            oItem.ESI_DISC_AMT = Convert.ToDecimal(txtDisAmt.Text);
            oItem.ESI_TAX_AMT = Convert.ToDecimal(txtTaxAmt.Text);
            oItem.ESI_NET = Convert.ToDecimal(txtLineTotAmt.Text);

            MasterItem _itmMas1 = new MasterItem();
            _itmMas1 = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
            if (_itmMas1.Mi_is_editlongdesc == true)
            {
                oItem.ESI_ITM_Description = txtItemDescription.Text.Trim();
            }
            else
            {
                oItem.ESI_ITM_Description = lblItemDescription.Text.Split(':')[1].Trim();
            }

            //oItem.ESI_SEQ = "";
            //oItem.ESI_ITM_SEQ = "";
            //oItem.ESI_RMK = "";
            //oItem.ESI_ISSUE_QTY = "";
            //oItem.ESI_UNIT_COST = "";

            oItem.ESI_UNIT_COST = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, oItem.ESI_ITM_STUS);

            oItem.ESI_PRINT = 0;
           // oItem.ESI_ITM_Description = lblItemDescription.Text.Split(':')[1].Trim();
            oItem.ESI_ACTIVE = 1;

            if (lblIsEditDesctiption.Text.ToUpper() == true.ToString().ToUpper())
            {
                oItem.isEditDescription = true;
            }
            else
            {
                oItem.isEditDescription = false;
            }

            oEstimate_Items.Add(oItem);
            BindAddItem();
            ModifyGrid();
            CalculateGrandTotal(oItem.ESI_QTY, oItem.ESI_UNIT_RT, oItem.ESI_DISC_AMT, oItem.ESI_TAX_AMT, true);
            ClearMiddle1p0();
            calculateCostAndRevenue();
            calculateTotals();

            if (MessageBox.Show("Do you want to add another item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                txtItem.Focus();
            }
            else
            {
                txtPrintContains.Focus();
            }
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
                txtQty.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitPrice.Focus();
            }
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CheckLevelStatusWithInventoryStatus();
                if (cmbStatus.SelectedValue != null)
                {
                    lblcostPrice.Text = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, cmbStatus.SelectedValue.ToString()).ToString("N");
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbBook_Leave(object sender, EventArgs e)
        {
            try
            {
                // btnAddItem.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                LoadPriceLevel(_salesType, cmbBook.Text);
                LoadLevelStatus(_salesType, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            {
                ClearPriceTextBox();
                this.Cursor = Cursors.Default; SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(_salesType, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                CheckQty(false);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LoadPriceLevelMessage();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void dgvEstimateItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstimateItems.Rows.Count > 0)
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        string jobNo = string.Empty;
                        string ItemCode = string.Empty;
                        decimal Qty = 0;
                        decimal UnitPrice = 0;
                        decimal DiscountAmount = 0;
                        decimal TaxAmount = 0;

                        jobNo = dgvEstimateItems.SelectedRows[0].Cells["JobNoItem"].Value.ToString();
                        ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                        Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                        UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                        DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                        TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());

                        string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                        string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                        string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                        if (oEstimate_Items.FindAll(x => x.ESI_ITM_CD == ItemCode
                            && x.ESI_JOBNO == jobNo
                            && x.ESI_QTY == Qty
                            && x.ESI_PLVL == Level
                            && x.ESI_ITM_STUS == Status
                            && x.ESI_PB == book).Count > 0)
                        {
                            oEstimate_Items.RemoveAll(x => x.ESI_ITM_CD == ItemCode
                                && x.ESI_JOBNO == jobNo
                                && x.ESI_QTY == Qty
                                && x.ESI_PLVL == Level
                                && x.ESI_ITM_STUS == Status
                                && x.ESI_PB == book);
                        }

                        CalculateGrandTotal(Qty, UnitPrice, DiscountAmount, TaxAmount, false);

                        BindAddItem();
                    }
                }
            }
            calculateCostAndRevenue();
            calculateTotals();
        }

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisAmt.Focus();
            }
        }

        private void txtLineTotAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
        }

        private void dgvEstimateItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstimateItems.Rows.Count > 0)
            {
                if (e.RowIndex != -1)
                {
                    Service_Estimate_Item oItem = oEstimate_Items.Find(x => x.ESI_ITM_CD == dgvEstimateItems.Rows[e.RowIndex].Cells["Item"].Value.ToString());
                    oItem.ESI_ITM_Description = dgvEstimateItems.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                }
            }
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscountRate.Text) || !string.IsNullOrEmpty(txtDiscoutAmount.Text))
            {
                if (!string.IsNullOrEmpty(txtDiscountRate.Text))
                {
                    decimal _disRate = Convert.ToDecimal(txtDiscountRate.Text);
                    if (oEstimate_Items.Count > 0)
                    {
                        bool addtoAll = false;

                        if (MessageBox.Show("Do you want to add discount to all items?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            addtoAll = true;
                        }
                        else
                        {
                            addtoAll = false;
                        }
                        foreach (Service_Estimate_Item item in oEstimate_Items)
                        {
                            if (_disRate > 0)
                            {
                                if (GeneralDiscount == null)
                                    GeneralDiscount = new CashGeneralEntiryDiscountDef();

                                GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf,
                                                                                             Convert.ToDateTime(dtpDate.Text.Trim()).Date,
                                                                                            item.ESI_PB.Trim(),
                                                                                             item.ESI_PLVL.Trim(),
                                                                                             lblCustomerCode.Text.Trim(),
                                                                                             item.ESI_ITM_CD.Trim(),
                                                                                             _priceBookLevelRef.Sapl_is_serialized ? true : false, false);

                                if (GeneralDiscount == null)
                                {
                                    return;
                                }
                                else if (GeneralDiscount != null)
                                {
                                    decimal vals = GeneralDiscount.Sgdd_disc_val;
                                    decimal rates = GeneralDiscount.Sgdd_disc_rt;

                                    if (rates < _disRate)
                                    {
                                        CalculateItem();
                                        this.Cursor = Cursors.Default;
                                        //using (new CenterWinDialog(this))
                                        {
                                            MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        txtDisRate.Text = FormatToCurrency("0");
                                        CalculateItem();
                                        _isEditDiscount = false;
                                        txtDiscountRate.Clear();
                                        txtDiscoutAmount.Clear();
                                        return;
                                    }
                                    else
                                    {
                                        _isEditDiscount = true;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(txtDiscountRate.Text))
                            {
                                txtDiscountRate.Text = FormatToCurrency("0");
                            }
                            decimal val = Convert.ToDecimal(txtDiscountRate.Text);

                            txtDiscountRate.Text = FormatToCurrency(Convert.ToString(val));

                            string CalcAmount = string.Empty;

                            string lineAmount = CalculateLineItem(item.ESI_QTY.ToString(),
                                                                    item.ESI_UNIT_RT.ToString(),
                                                                    item.ESI_ITM_CD,
                                                                    item.ESI_ITM_STUS,
                                                                    (txtDiscoutAmount.Text == "") ? "0.00" : txtDiscoutAmount.Text,
                                                                    (txtDiscountRate.Text == "") ? "0.00" : txtDiscountRate.Text, out CalcAmount);

                            if (addtoAll == true)
                            {
                                item.ESI_NET = Convert.ToDecimal(lineAmount);
                                item.ESI_DISC_AMT = Convert.ToDecimal((CalcAmount == "") ? "0" : CalcAmount);
                                item.ESI_DISC_RT = Convert.ToDecimal((txtDiscountRate.Text == "") ? "0" : txtDiscountRate.Text);
                            }
                            else
                            {
                                if (item.ESI_DISC_AMT == 0)
                                {
                                    item.ESI_NET = Convert.ToDecimal(lineAmount);
                                    item.ESI_DISC_AMT = Convert.ToDecimal((CalcAmount == "") ? "0" : CalcAmount);
                                    item.ESI_DISC_RT = Convert.ToDecimal((txtDiscountRate.Text == "") ? "0" : txtDiscountRate.Text);
                                }
                            }
                        }
                        BindAddItem();
                        ModifyGrid();
                        calculateTotals();
                    }
                }
                else if (!string.IsNullOrEmpty(txtDiscoutAmount.Text))
                {
                    decimal _disAmount = Convert.ToDecimal(txtDiscoutAmount.Text);
                    if (oEstimate_Items.Count > 0)
                    {
                        bool addtoAll = false;

                        if (MessageBox.Show("Do you want to add discount to all items?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            addtoAll = true;
                        }
                        else
                        {
                            addtoAll = false;
                        }

                        foreach (Service_Estimate_Item item in oEstimate_Items)
                        {
                            if (!string.IsNullOrEmpty(txtDiscoutAmount.Text) && _isEditPrice == false)
                            {
                                decimal _disAmt = Convert.ToDecimal(txtDiscoutAmount.Text);
                                decimal _uRate = Convert.ToDecimal(item.ESI_UNIT_RT);
                                decimal _qty = Convert.ToDecimal(item.ESI_QTY.ToString());
                                decimal _totAmt = _uRate * _qty;
                                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                                decimal realPercentage = 0;

                                realPercentage = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(_totAmt + item.ESI_TAX_AMT)) * 100 : 0;
                                _percent = realPercentage;

                                //if (_disAmt > 0)
                                //{
                                    {
                                        if (GeneralDiscount == null)
                                            GeneralDiscount = new CashGeneralEntiryDiscountDef();

                                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf,
                                                                                                    Convert.ToDateTime(dtpDate.Text.Trim()).Date,
                                                                                                   item.ESI_PB.Trim(),
                                                                                                    item.ESI_PLVL.Trim(),
                                                                                                    lblCustomerCode.Text.Trim(),
                                                                                                    item.ESI_ITM_CD.Trim(),
                                                                                                    _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                                        if (GeneralDiscount != null)
                                        {
                                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                                            if (vals < _disAmt && rates == 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                txtDiscoutAmount.Text = FormatToCurrency("0");
                                                txtDiscountRate.Text = FormatToCurrency("0");
                                                _isEditDiscount = false;
                                                return;
                                            }
                                            else if (rates < realPercentage)
                                            {
                                                CalculateItem();
                                                this.Cursor = Cursors.Default;
                                                //using (new CenterWinDialog(this))
                                                {
                                                    MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + realPercentage.ToString("N2") + "% discounted price is " + _totAmt.ToString("N2"), "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                }
                                                txtDisRate.Text = FormatToCurrency("0");
                                                CalculateItem();
                                                _isEditDiscount = false;
                                                txtDiscountRate.Clear();
                                                txtDiscoutAmount.Clear();
                                                return;
                                            }
                                            else
                                            {
                                                _isEditDiscount = true;
                                                //-----------------
                                                decimal val = _percent;
                                                //txtDiscountRate.Text = FormatToCurrency(Convert.ToString(val));
                                                txtDiscountRate.Text = val.ToString("N");
                                                txtDiscoutAmount.Text = _disAmt.ToString("N");
                                                string CalcAmount = string.Empty;
                                                string lineAmount = CalculateLineItem(item.ESI_QTY.ToString(),
                                                                                        item.ESI_UNIT_RT.ToString(),
                                                                                        item.ESI_ITM_CD,
                                                                                        item.ESI_ITM_STUS,
                                                                                        (txtDiscoutAmount.Text == "") ? "0.00" : txtDiscoutAmount.Text,
                                                                                        _percent.ToString(), out CalcAmount);

                                                if (addtoAll == true)
                                                {
                                                    item.ESI_NET = Convert.ToDecimal(lineAmount);
                                                    item.ESI_DISC_AMT = Convert.ToDecimal((CalcAmount == "") ? "0" : CalcAmount);
                                                    item.ESI_DISC_RT = _percent;
                                                }
                                                else
                                                {
                                                    if (item.ESI_DISC_AMT == 0)
                                                    {
                                                        item.ESI_NET = Convert.ToDecimal(lineAmount);
                                                        item.ESI_DISC_AMT = Convert.ToDecimal((CalcAmount == "") ? "0" : CalcAmount);
                                                        item.ESI_DISC_RT = _percent;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.Cursor = Cursors.Default;
                                            MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtDisAmt.Text = FormatToCurrency("0");
                                            txtDisRate.Text = FormatToCurrency("0");
                                            _isEditDiscount = false;
                                            return;
                                        }
                                    }
                                //}
                                //else
                                //    _isEditDiscount = false;
                            }
                            else if (_isEditPrice)
                            {
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtDisRate.Text = FormatToCurrency("0");
                            }

                            //if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
                            //decimal val = Convert.ToDecimal(txtDisAmt.Text);
                            //txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
                            //CalculateItem();
                            //  return;
                        }
                    }
                }
                txtDiscountRate.Text = "";
            }
            BindAddItem();
            ModifyGrid();
            calculateTotals();
            calculateCostAndRevenue();
            txtDiscountRate.Clear();
            txtDiscoutAmount.Clear();
        }

        private void txtDiscountRate_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscountRate.Text.Contains('.'))
            {
                txtDiscountRate.MaxLength = 4;
            }
        }

        private void txtDiscoutAmount_TextChanged(object sender, EventArgs e)
        {
            // txtDiscountRate.Clear();
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUnitAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDisRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDisAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTaxAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLineTotAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscoutAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEstimateNo_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceEstimate);
            DataTable _result = CHNLSVC.CommonSearch.Get_Service_Estimates(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEstimateNo;
            _CommonSearch.ShowDialog();
            txtEstimateNo_Leave(null, null);
            txtEstimateNo.Select();
        }

        private void btnSearchEstimate_Click(object sender, EventArgs e)
        {
            txtEstimateNo_DoubleClick(null, null);
        }

        private void txtEstimateNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEstimateNo.Text))
            {
                getEstimateDetails();
            }
        }

        private void txtEstimateNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtEstimateNo_DoubleClick(null, null);
            }
        }

        private void dgvJobDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    dgvJobDetails.Rows[i].Cells["select"].Value = false;
                }
                dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = true;
            }
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(txtJobNo.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void txtDiscountRate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtDiscountRate.Text))
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        btnAddDiscount_Click(null, null);
            //    }
            //}
            if (e.KeyCode == Keys.Enter)
            {
                btnAddDiscount.Focus();
            }
        }

        private void txtDiscoutAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtDiscoutAmount.Text))
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        btnAddDiscount_Click(null, null);
            //    }
            //}

            if (!string.IsNullOrEmpty(txtDiscoutAmount.Text))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAddDiscount.Focus();
                }
            }
        }

        private void txtDiscountRate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiscountRate.Text))
            {
                txtDiscountRate.Text = "";
            }
            else
            {
                txtDiscoutAmount.Text = "";
            }
        }

        private void txtDiscoutAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiscoutAmount.Text))
            {
                txtDiscoutAmount.Text = "";
            }
            else
            {
                txtDiscountRate.Text = "";
            }
        }

        private void dgvEstimateItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEstimateItems.IsCurrentCellDirty)
            {
                dgvEstimateItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtPrintContains_TextChanged(object sender, EventArgs e)
        {
            if (txtPrintContains.Text.Length > 190)
            {
                txtPrintContains.Clear();
            }
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtRemark.Text.Length > 90)
            {
                txtRemark.Clear();
            }
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDuration.Text))
            {
                if (Convert.ToInt32(txtDuration.Text) >= 365)
                {
                    txtDuration.Clear();
                }
                else
                {
                    lblExpDate.Text = dtpDate.Value.AddDays(Convert.ToInt32(txtDuration.Text) - 1).ToString("dd-MMM-yyyy");
                }
            }
            else
            {
                lblExpDate.Text = "";
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (oEstimate_Items.Count > 0)
            {
                if (dgvEstimateItems.Rows.Count > 0)
                {
                    if (chkSelectAll.Checked)
                    {
                        for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                        {
                            dgvEstimateItems.Rows[i].Cells["IsPrint"].Value = 1;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                        {
                            dgvEstimateItems.Rows[i].Cells["IsPrint"].Value = 0;
                        }
                    }
                }
            }
        }

        private void txtDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        private void txtDiscountRate_Enter(object sender, EventArgs e)
        {
            txtDiscoutAmount.Clear();
        }

        private void txtDiscoutAmount_Enter(object sender, EventArgs e)
        {
            txtDiscountRate.Clear();
        }

        private void lblName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblName, lblName.Text);
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        private void txtPrintContains_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEstimateNo.Text))
            {
                if (MessageBox.Show("Do you want to print this estimate?", "Service Estimate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    FF.WindowsERPClient.Reports.Service.ReportViewerSVC _viewsvc = new FF.WindowsERPClient.Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportTp = "EST";
                    _viewsvc.GlbReportName = "Job_Estimate.rpt";
                    BaseCls.GlbReportDoc = txtEstimateNo.Text.Trim();
                    _viewsvc.Show();
                    _viewsvc = null;
                }
            }
            else
            {
                MessageBox.Show("Please select a estimate.", "Service Estimate", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion events

        #region Methods

        private void GetEstimateType()
        {
            List<ComboBoxObject> oTypes = CHNLSVC.CustService.getEstimateTypes(null);
            cmbEstimateType.DisplayMember = "Text";
            cmbEstimateType.ValueMember = "Value";
            if (oTypes.Count > 0)
            {
                cmbEstimateType.DataSource = oTypes;
            }
            else
            {
                MessageBox.Show("Please setup the estimate types.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void GetJobDetails()
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                String stage = string.Empty;
                Int32 IsCusExpected = 0;

                stage = "2,3,5,6,4,5.1,5.2";

                DateTime from, to;

                from = Convert.ToDateTime("01-01-1111");
                to = Convert.ToDateTime("31-12-2999");

                DataTable DtDetails = new DataTable();
                DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);
                if (DtDetails.Rows.Count > 0)
                {
                    dgvJobDetails.DataSource = DtDetails;

                    dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, 0));

                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                    lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                    lblAddrss.Text = oJOB_HDR.SJB_B_ADD1;
                    lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
                    lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
                    lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
                    lblCustomerCode.Text = oJOB_HDR.SJB_B_CUST_CD;
                    lblManualRefNo.Text = oJOB_HDR.SJB_MANUALREF;
                    lblReqNo.Text = oJOB_HDR.SJB_REQNO;
                    //lblStatus.Text = oJOB_HDR.SJB_STUS;

                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, oJOB_HDR.SJB_B_CUST_CD, string.Empty, string.Empty, "C");
                    if (_masterBusinessCompany.Mbe_is_tax)
                    {
                        chkTaxPayable.Checked = true;
                        chkTaxPayable.Enabled = true;
                    }
                    else
                    {
                        chkTaxPayable.Checked = false;
                        chkTaxPayable.Enabled = false;
                    }

                    LoadTaxDetail(_masterBusinessCompany);

                    txtDuration.Focus();
                }
                else
                {
                    MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                    txtJobNo.Clear();
                    txtJobNo.Focus();
                    return;
                }
            }
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _salesType).Select(x => x.Sadd_pb).Distinct().ToList();
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
                            LoadPriceBook(_salesType);
                            LoadPriceLevel(_salesType, cmbBook.Text.Trim());
                            LoadLevelStatus(_salesType, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
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
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _salesType && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
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

        private void LoadCachedObjects()
        {
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;

            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true,""), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                    if
                        (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available")
                        _isVATInvoice = true;
                    else
                        _isVATInvoice = false;

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
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return RoundUpForPlace(value, 2);
            else return Math.Round(value, 2);
        }

        private decimal TaxCalculation1(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
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
                    //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty);
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


        //private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        //{
        //    decimal _TaxAmt = 0;
        //    decimal _TotVal = 0;
        //    decimal _TotDis = 0;
        //    _TotVal = _pbUnitPrice * _qty;
        //    _TotDis = _TotVal * _disRate / 100;

        //    if (cmbEstimateType.Text.Trim() == "DEBT")
        //    {
        //        _pbUnitPrice = 0;
        //    }
        //    else
        //    {
        //        //added darshana 30-Dec-2015
        //        if (_MasterProfitCenter.Mpc_issp_tax == true)
        //        {
        //            List<MasterPCTax> _masterPCTax = new List<MasterPCTax>();
        //            _masterPCTax = CHNLSVC.Sales.GetPcTax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, 1, dtpDate.Value.Date);

        //            //if (_masterPCTax == null || _masterPCTax.Count <=0)
        //            //{
        //            //    _pbUnitPrice = -1;
        //            //}
        //            //else
        //            //{
        //            var _pcTaxNBT = from _pcTaxs in _masterPCTax
        //                            where _pcTaxs.Mpt_taxtp == "NBT"
        //                            select _pcTaxs;

        //            foreach (MasterPCTax _one in _pcTaxNBT)
        //            {
        //                if (lblVatExemptStatus.Text != "Available")
        //                {
        //                    //if (_isTaxfaction == false)
        //                    //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                    //else
        //                    //if (_isVATInvoice)
        //                    //{

        //                    _discount = _TotVal * _disRate / 100;
        //                    _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mpt_taxrt / 100);

        //                    _TotVal = _TotVal - _TotDis + _TaxAmt;


        //                    //}
        //                    //else
        //                    //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                }
        //                else
        //                {
        //                    //if (_isTaxfaction) 
        //                    _pbUnitPrice = 0;
        //                }
        //            }

        //            var _pcTaxVAT = from _pcTaxs in _masterPCTax
        //                            where _pcTaxs.Mpt_taxtp == "VAT"
        //                            select _pcTaxs;

        //            foreach (MasterPCTax _one in _pcTaxVAT)
        //            {
        //                if (lblVatExemptStatus.Text != "Available")
        //                {
        //                    //if (_isTaxfaction == false)
        //                    //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                    //else
        //                    //if (_isVATInvoice)
        //                    //{
        //                    //_TotVal = _pbUnitPrice * _qty;
        //                    _discount = _TotVal * _disRate / 100;
        //                    _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mpt_taxrt / 100);


        //                    //}
        //                    //else
        //                    //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                }
        //                else
        //                {
        //                    //if (_isTaxfaction) 
        //                    _pbUnitPrice = 0;
        //                }
        //            }

        //            _pbUnitPrice = _TaxAmt;



        //        }
        //        else
        //        {
        //            if (_priceBookLevelRef != null)
        //                if (_priceBookLevelRef.Sapl_vat_calc)
        //                {
        //                    bool _isVATInvoice = false;

        //                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
        //                    else _isVATInvoice = false;

        //                    if (dtpDate.Value.Date == dtpDate.Value.Date)
        //                    {
        //                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
        //                        //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else 
        //                        _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty);
        //                        MainTaxConstant = _taxs;
        //                        var _Tax = from _itm in _taxs
        //                                   select _itm;
        //                        foreach (MasterItemTax _one in _Tax)
        //                        {
        //                            if (lblVatExemptStatus.Text != "Available")
        //                            {
        //                                //if (_isTaxfaction == false)
        //                                //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                                //else
        //                                //if (_isVATInvoice)
        //                                //{

        //                                _discount = _TotVal * _disRate / 100;
        //                                _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

        //                                _TotVal = _TotVal - _TotDis + _TaxAmt;


        //                                //}
        //                                //else
        //                                //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                            }
        //                            else
        //                            {
        //                                //if (_isTaxfaction) 
        //                                _pbUnitPrice = 0;
        //                            }
        //                        }

        //                        //vAT
        //                        _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty);
        //                        MainTaxConstant.AddRange(_taxs);

        //                        var _Tax1 = from _itm in _taxs
        //                                    select _itm;
        //                        foreach (MasterItemTax _one in _Tax1)
        //                        {
        //                            if (lblVatExemptStatus.Text != "Available")
        //                            {
        //                                //if (_isTaxfaction == false)
        //                                //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                                //else
        //                                //if (_isVATInvoice)
        //                                //{
        //                                //_TotVal = _pbUnitPrice * _qty;
        //                                _discount = _TotVal * _disRate / 100;
        //                                _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);


        //                                //}
        //                                //else
        //                                //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                            }
        //                            else
        //                            {
        //                                //if (_isTaxfaction) 
        //                                _pbUnitPrice = 0;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
        //                        //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dtpDate.Value.Date); else
        //                        _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtpDate.Value.Date);
        //                        MainTaxConstant.AddRange(_taxs);
        //                        var _Tax = from _itm in _taxs
        //                                   select _itm;
        //                        if (_taxs.Count > 0)
        //                        {
        //                            foreach (MasterItemTax _one in _Tax)
        //                            {
        //                                if (lblVatExemptStatus.Text != "Available")
        //                                {
        //                                    //if (_isTaxfaction == false)
        //                                    //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                                    //else
        //                                    //    if (_isVATInvoice)
        //                                    //    {
        //                                    //        _discount = _pbUnitPrice * _qty * _disRate / 100;
        //                                    //        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
        //                                    //    }
        //                                    //    else
        //                                    //        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                                    _discount = _TotVal * _disRate / 100;
        //                                    _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

        //                                    _TotVal = _TotVal - _TotDis + _TaxAmt;
        //                                }
        //                                else
        //                                {
        //                                    //if (_isTaxfaction) 
        //                                    _pbUnitPrice = 0;
        //                                }
        //                            }

        //                            _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtpDate.Value.Date);
        //                            MainTaxConstant.AddRange(_taxs);
        //                            var _Tax1 = from _itm in _taxs
        //                                        select _itm;
        //                            foreach (MasterItemTax _one in _Tax1)
        //                            {
        //                                if (lblVatExemptStatus.Text != "Available")
        //                                {
        //                                    //if (_isTaxfaction == false)
        //                                    //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                                    //else
        //                                    //if (_isVATInvoice)
        //                                    //{
        //                                    //_TotVal = _pbUnitPrice * _qty;
        //                                    _discount = _TotVal * _disRate / 100;
        //                                    _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);


        //                                    //}
        //                                    //else
        //                                    //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                                }
        //                                else
        //                                {
        //                                    //if (_isTaxfaction) 
        //                                    _pbUnitPrice = 0;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
        //                            //if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dtpDate.Value.Date); else
        //                            _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtpDate.Value.Date);
        //                            MainTaxConstant.AddRange(_taxs);
        //                            var _TaxEffDt = from _itm in _taxsEffDt
        //                                            select _itm;
        //                            foreach (LogMasterItemTax _one in _TaxEffDt)
        //                            {
        //                                if (lblVatExemptStatus.Text != "Available")
        //                                {
        //                                    //if (_isTaxfaction == false)
        //                                    //    _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
        //                                    //else
        //                                    //    if (_isVATInvoice)
        //                                    //    {
        //                                    //        _discount = _pbUnitPrice * _qty * _disRate / 100;
        //                                    //        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
        //                                    //    }
        //                                    //    else
        //                                    //        _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
        //                                    _discount = _TotVal * _disRate / 100;
        //                                    _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Lict_tax_rate / 100);

        //                                    _TotVal = _TotVal - _TotDis + _TaxAmt;
        //                                }
        //                                else
        //                                {
        //                                    // if (_isTaxfaction) 
        //                                    _pbUnitPrice = 0;
        //                                }
        //                            }
        //                            _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtpDate.Value.Date);
        //                            MainTaxConstant.AddRange(_taxs);
        //                            var _TaxEffDt1 = from _itm in _taxsEffDt
        //                                             select _itm;
        //                            foreach (LogMasterItemTax _one in _TaxEffDt1)
        //                            {
        //                                if (lblVatExemptStatus.Text != "Available")
        //                                {
        //                                    //if (_isTaxfaction == false)
        //                                    //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                                    //else
        //                                    //if (_isVATInvoice)
        //                                    //{
        //                                    //_TotVal = _pbUnitPrice * _qty;
        //                                    _discount = _TotVal * _disRate / 100;
        //                                    _TaxAmt = _TaxAmt + ((_TotVal) * _one.Lict_tax_rate / 100);


        //                                    //}
        //                                    //else
        //                                    //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
        //                                }
        //                                else
        //                                {
        //                                    //if (_isTaxfaction) 
        //                                    _pbUnitPrice = 0;
        //                                }
        //                            }

        //                        }
        //                    }
        //                }
        //                else
        //                    if (_isTaxfaction) _pbUnitPrice = 0;
        //            _pbUnitPrice = _TaxAmt;
        //        }
        //    }
        //    return _pbUnitPrice;
        //}

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction, string _saleTp)
        {
            decimal _TaxAmt = 0;
            decimal _TotVal = 0;
            decimal _TotDis = 0;
            _TotVal = _pbUnitPrice * _qty;
            _TotDis = _TotVal * _disRate / 100;

            if (_saleTp == "DEBT")
            {
                _pbUnitPrice = 0;
            }
            else
            {
                //added darshana 30-Dec-2015
                if (_MasterProfitCenter.Mpc_issp_tax == true)
                {
                    List<MasterPCTax> _masterPCTax = new List<MasterPCTax>();
                    _masterPCTax = CHNLSVC.Sales.GetPcTax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, 1, dtpDate.Value.Date);

                    //if (_masterPCTax == null || _masterPCTax.Count <=0)
                    //{
                    //    _pbUnitPrice = -1;
                    //}
                    //else
                    //{
                    var _pcTaxNBT = from _pcTaxs in _masterPCTax
                                    where _pcTaxs.Mpt_taxtp == "NBT"
                                    select _pcTaxs;

                    foreach (MasterPCTax _one in _pcTaxNBT)
                    {
                        if (lblVatExemptStatus.Text != "Available")
                        {
                            //if (_isTaxfaction == false)
                            //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            //else
                            //if (_isVATInvoice)
                            //{

                            _discount = _TotVal * _disRate / 100;
                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mpt_taxrt / 100);

                            _TotVal = _TotVal - _TotDis + _TaxAmt;


                            //}
                            //else
                            //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                        }
                        else
                        {
                            //if (_isTaxfaction) 
                            _pbUnitPrice = 0;
                        }
                    }

                    var _pcTaxVAT = from _pcTaxs in _masterPCTax
                                    where _pcTaxs.Mpt_taxtp == "VAT"
                                    select _pcTaxs;

                    foreach (MasterPCTax _one in _pcTaxVAT)
                    {
                        if (lblVatExemptStatus.Text != "Available")
                        {
                            //if (_isTaxfaction == false)
                            //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            //else
                            //if (_isVATInvoice)
                            //{
                            //_TotVal = _pbUnitPrice * _qty;
                            _discount = _TotVal * _disRate / 100;
                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mpt_taxrt / 100);


                            //}
                            //else
                            //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                        }
                        else
                        {
                            //if (_isTaxfaction) 
                            _pbUnitPrice = 0;
                        }
                    }

                    _pbUnitPrice = _TaxAmt;



                }
                else
                {
                    if (_priceBookLevelRef != null)
                        if (_priceBookLevelRef.Sapl_vat_calc)
                        {
                            bool _isVATInvoice = false;

                            if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                            else _isVATInvoice = false;

                            if (dtpDate.Value.Date == _serverDt)
                            {
                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else 

                                //_taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty); //Sanjeewa
                                if (_isStrucBaseTax == true)
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty);

                                var _Tax = from _itm in _taxs
                                           select _itm;
                                foreach (MasterItemTax _one in _Tax)
                                {
                                    if (lblVatExemptStatus.Text != "Available")
                                    {
                                        //if (_isTaxfaction == false)
                                        //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                        //else
                                        //if (_isVATInvoice)
                                        //{

                                        _discount = _TotVal * _disRate / 100;
                                        _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

                                        _TotVal = _TotVal - _TotDis + _TaxAmt;


                                        //}
                                        //else
                                        //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                    {
                                        //if (_isTaxfaction) 
                                        _pbUnitPrice = 0;
                                    }
                                }

                                //vAT
                                //_taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty);//Sanjeewa
                                if (_isStrucBaseTax == true)
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty);

                                var _Tax1 = from _itm in _taxs
                                            select _itm;
                                foreach (MasterItemTax _one in _Tax1)
                                {
                                    if (lblVatExemptStatus.Text != "Available")
                                    {
                                        //if (_isTaxfaction == false)
                                        //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                        //else
                                        //if (_isVATInvoice)
                                        //{
                                        //_TotVal = _pbUnitPrice * _qty;
                                        if (_isTaxfaction == false)
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal - _discount) * _one.Mict_tax_rate / 100);
                                        }
                                        else
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);
                                        }

                                        //}
                                        //else
                                        //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                    {
                                        //if (_isTaxfaction) 
                                        _pbUnitPrice = 0;
                                    }
                                }


                            }
                            else
                            {
                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dtDate.Value.Date); else
                                _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtpDate.Value.Date);
                                var _Tax = from _itm in _taxs
                                           select _itm;
                                if (_taxs.Count > 0)
                                {
                                    foreach (MasterItemTax _one in _Tax)
                                    {
                                        if (lblVatExemptStatus.Text != "Available")
                                        {
                                            //if (_isTaxfaction == false)
                                            //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                            //else
                                            //    if (_isVATInvoice)
                                            //    {
                                            //        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            //        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                            //    }
                                            //    else
                                            //        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

                                            _TotVal = _TotVal - _TotDis + _TaxAmt;
                                        }
                                        else
                                        {
                                            //if (_isTaxfaction) 
                                            _pbUnitPrice = 0;
                                        }
                                    }

                                    _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtpDate.Value.Date);
                                    var _Tax1 = from _itm in _taxs
                                                select _itm;
                                    foreach (MasterItemTax _one in _Tax1)
                                    {
                                        if (lblVatExemptStatus.Text != "Available")
                                        {
                                            //if (_isTaxfaction == false)
                                            //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                            //else
                                            //if (_isVATInvoice)
                                            //{
                                            //_TotVal = _pbUnitPrice * _qty;
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);


                                            //}
                                            //else
                                            //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                        {
                                            //if (_isTaxfaction) 
                                            _pbUnitPrice = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                                    //if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dtDate.Value.Date); else
                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtpDate.Value.Date);
                                    var _TaxEffDt = from _itm in _taxsEffDt
                                                    select _itm;
                                    foreach (LogMasterItemTax _one in _TaxEffDt)
                                    {
                                        if (lblVatExemptStatus.Text != "Available")
                                        {
                                            //if (_isTaxfaction == false)
                                            //    _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                            //else
                                            //    if (_isVATInvoice)
                                            //    {
                                            //        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            //        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                            //    }
                                            //    else
                                            //        _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Lict_tax_rate / 100);

                                            _TotVal = _TotVal - _TotDis + _TaxAmt;
                                        }
                                        else
                                        {
                                            // if (_isTaxfaction) 
                                            _pbUnitPrice = 0;
                                        }
                                    }
                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtpDate.Value.Date);
                                    var _TaxEffDt1 = from _itm in _taxsEffDt
                                                     select _itm;
                                    foreach (LogMasterItemTax _one in _TaxEffDt1)
                                    {
                                        if (lblVatExemptStatus.Text != "Available")
                                        {
                                            //if (_isTaxfaction == false)
                                            //    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                            //else
                                            //if (_isVATInvoice)
                                            //{
                                            //_TotVal = _pbUnitPrice * _qty;
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Lict_tax_rate / 100);


                                            //}
                                            //else
                                            //    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                        {
                                            //if (_isTaxfaction) 
                                            _pbUnitPrice = 0;
                                        }
                                    }

                                }
                            }
                        }
                        else
                            if (_isTaxfaction)
                                _pbUnitPrice = 0;
                    _pbUnitPrice = _TaxAmt;


                }
            }
            return _pbUnitPrice;
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

                //comented by kapila on 9/6/2017 req by pradeep/asanka
                //if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
                //{
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);

                if (_disRate > 0)
                {
                    ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Value).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCustomerCode.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
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


                //}
                //else if (_isEditPrice)
                //{
                //    txtDisRate.Text = FormatToCurrency("0");
                //}

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

        protected bool CheckNewDiscountRate(string txtBox)
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null)
                        GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    // if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCustomerCode.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
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
                                        decimal asd = Convert.ToDecimal(txtLineTotAmt.Text);
                                        MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + " discounted price is " + asd.ToString("N"), "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    txtDisRate.Text = FormatToCurrency("0");
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
                                        MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    txtDisRate.Text = FormatToCurrency("0");
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
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            //lblPromoVouUsedFlag.Text = "U";
                            _proVouInvcItem = txtItem.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisRate.Text))
                txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            //btnAddItem.Focus();
            return true;
        }

        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                return false;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
            {
                return false;
            }
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
                            Cursor = Cursors.Default;
                            MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
                            {
                                txtDisRate.Text = "0";
                            }

                            CalculateItem();

                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
                            {
                                _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                            }
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
                            {
                                txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
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
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCustomerCode.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
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
                                CheckNewDiscountRate(string.Empty);
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

                lblIsEditDesctiption.Text = _itemdetail.Mi_is_editshortdesc.ToString();
                lblItemUOM.Text = _itemdetail.Mi_itm_uom;
                lblItemType.Text = _itemdetail.Mi_itm_tp;

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

        private bool CheckBlockItem(string _item, int _pricetype, bool _isCombineItemAddingNow)
        {
            if (_isCombineItemAddingNow) return false;
            _isBlocked = false;
            if (_priceBookLevelRef.Sapl_is_serialized == false)
            {
                MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
                if (_block != null )
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
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
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

            MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
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
            CheckItemTax(txtItem.Text.Trim());
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
            //    txtDisRate.ReadOnly = false;
            //    txtDisAmt.ReadOnly = false;
            //    txtUnitAmt.ReadOnly = true;
            //    txtTaxAmt.ReadOnly = true;
            //    txtLineTotAmt.ReadOnly = true;
            //    return true;
            //}
            //else
            //{
            //    txtUnitPrice.ReadOnly = true;
            //    txtUnitAmt.ReadOnly = true;
            //    txtTaxAmt.ReadOnly = true;
            //    txtLineTotAmt.ReadOnly = true;
            //    if (_itemdetail.Mi_itm_tp == "V")
            //    {
            //        txtDisRate.ReadOnly = true;
            //        txtDisAmt.ReadOnly = true;
            //    }
            //    else
            //    {
            //        txtDisRate.ReadOnly = false;
            //        txtDisAmt.ReadOnly = false;
            //    }
            btnAddItem.Enabled = true;
            //}
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _salesType, cmbBook.Text, cmbLevel.Text, lblCustomerCode.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpDate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    btnAddItem.Enabled = false;
                    return _IsTerminate;
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency("0");
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
                        
                        //decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        //txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));

                        decimal UnitPrice = _single.Sapd_itm_price;
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        
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
            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();

            //get price for priority pb
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtLineTotAmt.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _salesType, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, lblCustomerCode.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpDate.Text));
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
                ////////////            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
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
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal vals1 = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(vals1));
                decimal otherPrice = 0;
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(_unitPrice))
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    if (!string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDisAmt.Text))
                    {
                        _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                    }

                    otherPrice = CalculateItemTem(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(_unitPrice), _disAmt, _disRate);
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
                        txtUnitPrice.Text = FormatToCurrency("0");
                        txtUnitAmt.Text = FormatToCurrency("0");
                        txtDisRate.Text = FormatToCurrency("0");
                        txtDisAmt.Text = FormatToCurrency("0");
                        txtTaxAmt.Text = FormatToCurrency("0");
                        txtLineTotAmt.Text = FormatToCurrency("0");
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
                                decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
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

        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Please select the valid qty", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            //if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //    txtQty.Text = FormatToQty("1");

            _MainPriceCombinItem = new List<PriceCombinedItemRef>();

            if (string.IsNullOrEmpty(txtQty.Text))
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
            if (Convert.ToDecimal(txtQty.Text) <= 0)
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
            if (string.IsNullOrEmpty(lblCustomerCode.Text))
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                lblCustomerCode.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                this.Cursor = Cursors.Default;
                //using (new CenterWinDialog(this))
                { MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                txtItem.Focus();
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

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            if (_isQty)
                txtQty.Text = FormatToQty("1");
            txtTaxAmt.Text = FormatToCurrency("0");
            if (_isUnit)
                txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
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

        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true,""), true);
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
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    {
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
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
                if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                else
                    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(tax) - _disAmt, true);
            }

            return _totalAmount;
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
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        List<InventoryLocation> _balance = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), _invoiceStatus);
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
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
        }

        protected void BindAddItem()
        {
            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            //for (int i = 0; i < oEstimate_Items.Count; i++)
            //{
            //    oEstimate_Items[i].ESI_LINE = i + 1;
            //}
            dgvEstimateItems.DataSource = oEstimate_Items;
        }

        private void ModifyGrid()
        {
            if (dgvEstimateItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                {
                    if (dgvEstimateItems.Rows[i].Cells["isEdit"].Value != null)
                    {
                        if (dgvEstimateItems.Rows[i].Cells["isEdit"].Value.ToString().ToUpper() == true.ToString().ToUpper())
                        {
                            dgvEstimateItems.Rows[i].Cells["Description"].ReadOnly = false;
                        }
                        else
                        {
                            dgvEstimateItems.Rows[i].Cells["Description"].ReadOnly = true;
                        }
                    }
                }
                dgvEstimateItems.Columns["Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["UnitPrice"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["UnitAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["DiscountRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["DiscountAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["TaxAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["LineAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvEstimateItems.Columns["Cost"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvEstimateItems.Columns["Qty"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["UnitPrice"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["UnitAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["DiscountRate"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["DiscountAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["TaxAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["LineAmount"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEstimateItems.Columns["Cost"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ClearRight1p1()
        {
            lblGrndSubTotal.Text = FormatToCurrency("0");
            lblGrndDiscount.Text = FormatToCurrency("0");
            lblGrndAfterDiscount.Text = FormatToCurrency("0");
            lblGrndTax.Text = FormatToCurrency("0");
            lblGrndTotalAmount.Text = FormatToCurrency("0");
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
            //lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(GrndSubTotal - GrndDiscount + GrndTax, true,false)));
            if (oEstimate_Items != null || oEstimate_Items.Count > 0)
                lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(oEstimate_Items.Sum(x => x.ESI_NET)));
            else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
            //TODO: remove remark, when apply payment UC
            //txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //lblPayBalance.Text = lblGrndTotalAmount.Text;
        }

        private void ClearMiddle1p0()
        {
            txtItem.Clear();
            //cmbBook.Text = string.Empty;
            //cmbLevel.Text = string.Empty;
            //cmbStatus.Text = string.Empty;
            txtQty.Text = FormatToQty("1");
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            lblcostPrice.Text = "";
        }

        private string CalculateLineItem(string qty, string UnitPrice, string ItemCode, string status, string discountAmount, string discountRate, out string CalcAmount)
        {
            string LineAmount = "0";
            if (!string.IsNullOrEmpty(qty) && !string.IsNullOrEmpty(UnitPrice))
            {
                string UnitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(UnitPrice.Trim()) * Convert.ToDecimal(qty.Trim()), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(ItemCode.Trim(), status.ToString().Trim(), Convert.ToDecimal(qty), _priceBookLevelRef, Convert.ToDecimal(UnitPrice.Trim()), Convert.ToDecimal(discountAmount.Trim()), Convert.ToDecimal(discountRate), true,""), true);
                string TaxAmt = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(qty) * Convert.ToDecimal(UnitPrice);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(discountRate))
                {
                    bool _isVATInvoice = false;
                    if
                        (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available")
                        _isVATInvoice = true;
                    else
                        _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(discountRate) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(discountRate) / 100), true);
                        if (Convert.ToDecimal(discountRate) > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, ItemCode.Trim(), Convert.ToString(status), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                TaxAmt = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }
                    CalcAmount = discountAmount;
                    discountAmount = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(TaxAmt))
                {
                    if (Convert.ToDecimal(discountRate) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(TaxAmt) - _disAmt, true);
                }

                LineAmount = FormatToCurrency(Convert.ToString(_totalAmount));
                CalcAmount = discountAmount;
                return LineAmount;
            }
            CalcAmount = discountAmount;
            return LineAmount;
        }

        private void calculateCostAndRevenue()
        {
            try
            {
                if (oEstimate_Items.Count > 0)
                {
                    decimal costTOt = 0;
                    decimal revenueTOt = 0;
                    decimal margineTOt = 0;
                    decimal TaxTotal = 0;

                    foreach (Service_Estimate_Item item in oEstimate_Items)
                    {
                        costTOt += item.ESI_QTY * item.ESI_UNIT_COST;
                        revenueTOt += item.ESI_NET;
                    }

                    TaxTotal = oEstimate_Items.Sum(x => x.ESI_TAX_AMT);

                    margineTOt = revenueTOt - TaxTotal - costTOt;

                    txtCostTotal.Text = costTOt.ToString("N");
                    txtRevenueTotal.Text = (revenueTOt - TaxTotal).ToString("N");
                    txtMarginTotal.Text = margineTOt.ToString("N");

                    if (costTOt != 0)
                    {
                        txtMarginPercentage.Text = FigureRoundUp(((margineTOt / costTOt) * 100), false).ToString("N");
                    }
                    else
                    {
                        txtMarginPercentage.Clear();
                    }
                }
                else
                {
                    txtCostTotal.Text = "0";
                    txtRevenueTotal.Text = "0";
                    txtMarginTotal.Text = "0";
                    txtMarginPercentage.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checkNonPrintItems()
        {
            bool status = false;

            for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
            {
                if (dgvEstimateItems.Rows[i].Cells["IsPrint"].Value != null && Convert.ToBoolean(dgvEstimateItems.Rows[i].Cells["IsPrint"].Value) == true)
                {
                }
                else
                {
                    status = true;
                    break;
                }
            }

            return status;
        }

        private bool ValidatItemAdd()
        {
            bool status = false;

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                status = true;
                MessageBox.Show("Please selete a item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return status;
        }

        private bool validateSave()
        {
            bool status = false;

            if (String.IsNullOrEmpty(txtJobNo.Text))
            {
                status = true;
                MessageBox.Show("Please select a job number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return status;
            }

            if (String.IsNullOrEmpty(txtDuration.Text))
            {
                status = true;
                MessageBox.Show("Please enter duration", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDuration.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtRemark.Text))
            {
                status = true;
                MessageBox.Show("Please enter a remark", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemark.Focus();
                return status;
            }
            if (lblStatus.Tag != null)
            {
                if (lblStatus.Tag.ToString() == "A")
                {
                    status = true;
                    MessageBox.Show("You cann't amend approved estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return status;
                }

                if (lblStatus.Tag.ToString() == "C")
                {
                    status = true;
                    MessageBox.Show("You cann't amend canceled estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return status;
                }
            }
            return status;
        }

        private void getEstimateDetails()
        {
            Service_Estimate_Header oHeader = CHNLSVC.CustService.GetServiceEstimateHeader(txtEstimateNo.Text, BaseCls.GlbUserComCode);
            if (oHeader.ESH_ESTNO == null)
            {
                MessageBox.Show("Please enter correct estimate number!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearAll();
                return;
            }
            cmbEstimateType.SelectedValue = oHeader.ESH_TP;
            dtpDate.Value = oHeader.ESH_DT;
            txtJobNo.Text = oHeader.ESH_JOB_NO;
            txtJobNo_Leave(null, null);
            dgvJobDetails.Enabled = false;
            txtDuration.Text = oHeader.EST_DURATION.ToString();
            txtPrintContains.Text = oHeader.EST_PRINT_RMK;
            txtRemark.Text = oHeader.EST_RMK;
            lblSeq.Text = oHeader.ESH_SEQ_NO.ToString();
            lblStatus.Tag = oHeader.EST_STUS;

            btnApprove.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnCustApprove.Enabled = true;

            switch (oHeader.EST_STUS)
            {
                case "P":
                    {
                        lblStatus.Text = "Pending";
                        break;
                    }
                case "A":
                    {
                        lblStatus.Text = "Approed";
                        btnCancel.Enabled = false;
                        btnApprove.Enabled = false;
                        btnCustApprove.Enabled = true;

                        if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10817))
                        {
                            btnCancel.Text = "Reject";
                            btnCancel.Enabled = true;
                        }
                        else
                        {
                            btnCancel.Text = "Cancel";
                            btnCancel.Enabled = false;
                        }

                        break;
                    }
                case "F":
                    {
                        lblStatus.Text = "Customer Approved";
                        btnApprove.Enabled = false;
                        btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        btnCustApprove.Enabled = false;
                        break;
                    }
                case "C":
                    {
                        lblStatus.Text = "Cancelled";
                        btnApprove.Enabled = false;
                        btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        btnCustApprove.Enabled = false;
                        break;
                    }
                    break;
            }

            List<Service_Estimate_Item> oItems = CHNLSVC.CustService.GetServiceEstimateItems(txtEstimateNo.Text);
            oEstimate_Items.Clear();
            oEstimate_Items.AddRange(oItems);

            for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
            {
                if (dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString() == oItems[0].ESI_JOBLINE.ToString())
                {
                    dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, i));
                }
            }

            BindAddItem();
            ModifyGrid();
            ClearMiddle1p0();
            calculateCostAndRevenue();
            calculateTotals();
        }

        private void calculateTotals()
        {
            decimal subTotal = 0;
            decimal Discount = 0;
            decimal AfteDiscount = 0;
            decimal Tax = 0;
            decimal TotalAmount = 0;

            foreach (Service_Estimate_Item item in oEstimate_Items)
            {
                subTotal += item.ESI_QTY * item.ESI_UNIT_RT;
            }

            Discount = oEstimate_Items.Sum(x => x.ESI_DISC_AMT);
            AfteDiscount = (subTotal - Discount);
            Tax = oEstimate_Items.Sum(x => x.ESI_TAX_AMT);

            TotalAmount = oEstimate_Items.Sum(x => x.ESI_NET);

            lblGrndSubTotal.Text = subTotal.ToString("N");
            lblGrndDiscount.Text = Discount.ToString("N");
            lblGrndAfterDiscount.Text = AfteDiscount.ToString("N");
            lblGrndTax.Text = Tax.ToString("N");
            lblGrndTotalAmount.Text = TotalAmount.ToString("N");
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }

        private void ClearVariable()
        {
            btnSave.Enabled = true;
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = 0;
            SSPriceBookPrice = 0;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _isEditPrice = false;
            _isEditDiscount = false;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _isCompleteCode = false;
            _isRegistrationMandatory = false;
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

        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
            }
        }

        private void btnCustApprove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEstimateNo.Text))
            {
                if (MessageBox.Show("Do you want to aprove this estimate?", "Customer Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;

                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10801))
                    {
                        Int32 result = CHNLSVC.CustService.Update_Estimate_HEaderStatus("F", txtEstimateNo.Text, BaseCls.GlbUserID, BaseCls.GlbUserComCode);

                        if (result > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Record successfully approved.", "Customer Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearAll();
                            return;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("You dont have permission to approve.\nPermission code - 10801", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Please select a estimate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEstimateNo.Focus();
                return;
            }
        }

        //private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        //{
        //    bool _isAvailable = false;
        //    if (_isSerialized)
        //    {
        //        if (string.IsNullOrEmpty(txtSerialNo.Text))
        //        {
        //            this.Cursor = Cursors.Default;
        //            using (new CenterWinDialog(this)) { MessageBox.Show("You are selected a serialized price level, hence you have not select the serial no. Please select the serial no.", "Serialized Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //            _isAvailable = true;
        //            return _isAvailable;
        //        }
        //        List<PriceSerialRef> _list = null;
        //        if (_isNewPromotionProcess == false)
        //            _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerialNo.Text.Trim());
        //        else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0)
        //            _list = _PriceSerialRefNormal;
        //        else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0)
        //            _list = _PriceSerialRefPromo;
        //        _tempPriceSerial = new List<PriceSerialRef>();
        //        _tempPriceSerial = _list;
        //        if (_list != null)
        //        {
        //            if (_list.Count <= 0)
        //            {
        //                this.Cursor = Cursors.Default;
        //                using (new CenterWinDialog(this)) { MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                txtQty.Text = FormatToQty("0");
        //                _isAvailable = true;
        //                txtQty.Focus();
        //                return _isAvailable;
        //            }
        //            var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
        //            _list = _oneSerial;
        //            if (_list.Count < Convert.ToDecimal(txtQty.Text))
        //            {
        //                this.Cursor = Cursors.Default;
        //                using (new CenterWinDialog(this)) { MessageBox.Show("Selected qty is exceeds available serials at the price definition!", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                txtQty.Text = FormatToQty("0");
        //                // IsNoPriceDefinition = true;
        //                _isAvailable = true;
        //                txtQty.Focus();
        //                return _isAvailable;
        //            }
        //            if (_list.Count == 1)
        //            {
        //                string _book = _list[0].Sars_pbook;
        //                string _level = _list[0].Sars_price_lvl;
        //                cmbBook.Text = _book;
        //                cmbLevel.Text = _level;
        //                if (!_isSerialized)
        //                    cmbLevel_Leave(null, null);

        //                int _priceType = 0;
        //                _priceType = _list[0].Sars_price_type;
        //                PriceTypeRef _promotion = TakePromotion(_priceType);
        //                decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false);

        //                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
        //                WarrantyRemarks = _list[0].Sars_warr_remarks;
        //                SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

        //                Int32 _pbSq = _list[0].Sars_pb_seq;
        //                string _mItem = _list[0].Sars_itm_cd;
        //                _isAvailable = true;
        //                //if (_promotion.Sarpt_is_com)
        //                //{
        //                SetColumnForPriceDetailNPromotion(true);
        //                BindSerializedPrice(_list);

        //                if (gvPromotionPrice.RowCount > 0)
        //                {
        //                    gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
        //                    pnlPriceNPromotion.Visible = true;
        //                    pnlMain.Enabled = false;
        //                    return _isAvailable;
        //                }
        //                else
        //                    if (_isCombineAdding == false) txtUnitPrice.Focus();
        //                //}
        //                //else
        //                //    if (_isCombineAdding == false) txtUnitPrice.Focus();
        //                return _isAvailable;
        //            }
        //            if (_list.Count > 1)
        //            {
        //                SetColumnForPriceDetailNPromotion(true);
        //                BindPriceAndPromotion(_list);
        //                DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
        //                pnlMain.Enabled = false;
        //                pnlPriceNPromotion.Visible = true;
        //                _isAvailable = true;
        //                return _isAvailable;
        //            }
        //        }
        //        else
        //        {
        //            this.Cursor = Cursors.Default;
        //            using (new CenterWinDialog(this)) { MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //            txtQty.Text = FormatToQty("0");
        //            _isAvailable = true;
        //            txtQty.Focus();
        //            return _isAvailable;
        //        }
        //    }
        //    return _isAvailable;
        //}

        private void clearAll()
        {
            cmbEstimateType.SelectedIndex = 0;
            dtpDate.Value = DateTime.Now;
            txtJobNo.Clear();
            txtDisAmt.Clear();
            txtEstimateNo.Clear();
            txtItemDescription.Visible = false;
            txtItemDescription.Text = "";
            lblName.Text = "";
            lblAddrss.Text = "";
            lblTele.Text = "";
            lblContactPerson.Text = "";
            lblConPhone.Text = "";
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);

            lblCustomerCode.Text = "";
            lblPromoVouNo.Text = "";
            lblPromoVouUsedFlag.Text = "";

            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            lblIsEditDesctiption.Text = "";
            lblItemUOM.Text = "";
            lblItemType.Text = "";
            lblManualRefNo.Text = "";
            txtDiscountRate.Clear();
            txtDiscoutAmount.Clear();
            lblcostPrice.Text = "";
            lblReqNo.Text = "";
            ClearMiddle1p0();
            txtPrintContains.Clear();
            txtPrintContains.Clear();
            txtCostTotal.Clear();
            txtRevenueTotal.Clear();
            txtMarginTotal.Clear();
            ClearRight1p1();
            txtDuration.Clear();
            lblSeq.Text = "";
            txtRemark.Text = "";
            lblStatus.Text = "";

            oEstimate_Items.Clear();

            txtMarginPercentage.Clear();
            dgvJobDetails.Enabled = true;
            lblStatus.Tag = "";
            txtJobNo.Focus();
            lblBackDateInfor.Text = "";

            lblExpDate.Text = "";
            _masterBusinessCompany = new MasterBusinessEntity();

            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;

            ClearVariable();

            lblcostPrice.Text = "";

            btnApprove.Enabled = true;

            btnCancel.Enabled = true;
            LoadPriceDefaultValue();
        }

        #endregion Methods

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

    }
}