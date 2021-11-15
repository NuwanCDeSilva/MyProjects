using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Sales;
using FF.BusinessObjects.Sales;

namespace FF.WindowsERPClient.Services
{
    public partial class Service_Invoice2 : Base
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
        private Boolean _isStockUpdate = false; //kapila 27/7/2015

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
        //private List<Service_Confirm_detail> oMainDetailList = new List<Service_Confirm_detail>();
        private List<Service_Confirm_detail> oMainDetailList = null;

        private List<RecieptItem> _recieptItem = null;
        private List<InvoiceItem> _invoiceItemList = null;
        private List<ReptPickSerials> ScanSerialList = null;
        private MasterBusinessEntity _businessEntity = null;
        private InventoryHeader _buybackheader = new InventoryHeader();
        private Service_Confirm_detail editItem;

        private List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount;

        private bool mouseIsDown = false;
        private Point firstPoint;

        private bool IsEdit = false;
        private bool IsStartUp = false;
        private bool _isBackDate = false;
        private bool deliveryNow = false;
        private string jobCategory = string.Empty;
        private Int32 SelectedSerialID = 0;
        private List<scv_agr_payshed> _lstShed = new List<scv_agr_payshed>();
        private Boolean _isStrucBaseTax = false;

        private List<Service_free_det> lstService = new List<Service_free_det>();
        private int Count = 0;
        private int duration = 0;
        private List<Service_Req_Def> _scvDefList = null;
        private Service_Req_Hdr _scvjobHdr = new Service_Req_Hdr();
        private List<Service_Req_Det> _scvItemList = new List<Service_Req_Det>();
            
        private List<Service_Tech_Aloc_Hdr> _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
        private List<Service_Req_Det_Sub> _scvItemSubList = new List<Service_Req_Det_Sub>();
        private List<Service_Req_Det_Sub> _tempItemSubList = new List<Service_Req_Det_Sub>();
        private List<Service_TempIssue> _scvStdbyList = new List<Service_TempIssue>();
      
     

        public Service_Invoice2()
        {
            InitializeComponent();
            dgvEstimateItems.AutoGenerateColumns = false;
            gvDisItem.AutoGenerateColumns = false;

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

            dgvJobConf.AutoGenerateColumns = false;
            dgvEstimateItems.AutoGenerateColumns = false;
            dgvItems.AutoGenerateColumns = false;

            pnlDeliveryInstruction.Size = new System.Drawing.Size(522, 167);
            pnlStandByItems.Size = new System.Drawing.Size(466, 195);
            pnlAgr.Size = new Size(562, 278);

            //kapila 7/6/2017
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2,3,5,6,7" + seperator);
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "V" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Service_JobReqNum:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceInvoiceSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AC_SVC_ALW_LOC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "AC_SCV" + seperator);
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
                // SerialNo.HeaderText = _Parameters.SP_DB_SERIAL;

                if (_Parameters.SP_ISNEEDGATEPASS == 1)
                {
                    deliveryNow = false;
                }
                else
                {
                    deliveryNow = true;
                }
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }

            //GetEstimateType();
            dtpFromD.Value = DateTime.Today.AddDays(-30);
            dtpToD.Value = DateTime.Today;

            IsStartUp = true;
            btnClear_Click(null, null);
            btnSearchHeader_Click(null, null);

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
            LoadInvoiceProfitCenterDetail();

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

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10802))
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

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10802))
            {
                label45.Visible = false;
                lblcostPrice.Visible = false;
                label29.Visible = false;
                txtCostTotal.Visible = false;
                label33.Visible = false;
                txtRevenueTotal.Visible = false;
                label35.Visible = false;
                txtMarginTotal.Visible = false;
                label28.Visible = false;
                txtMarginPercentage.Visible = false;
                label30.Visible = false;
            }
            else
            {
                label45.Visible = true;
                lblcostPrice.Visible = true;
                label29.Visible = true;
                txtCostTotal.Visible = true;
                label33.Visible = true;
                txtRevenueTotal.Visible = true;
                label35.Visible = true;
                txtMarginTotal.Visible = true;
                label28.Visible = true;
                txtMarginPercentage.Visible = true;
                label30.Visible = true;
            }

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

            txtJobNo.Focus();

            dtpDate.Enabled = true;
            if (BaseCls.GlbUserComCode=="AAL")
            {
                btnCancel.Enabled = false;
            }
        }

        #region events

        private void btnSave_Click(object sender, EventArgs e)
        {
            string chngecustAdd1 = string.Empty;//Add by tharanga
            string chngecustAdd2 = string.Empty;//add by tharanga 

            //add by akila 2017/08/21
            //if (!ValidateCustomer(txtBillingCustCode.Text.ToUpper().Trim(), txtInvoiceType.Text))
            //{
            //    return;
            //}

            if (string.IsNullOrEmpty(txtCustomerCodeDD.Text))
            {
                MessageBox.Show("Please enter delivery customer code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnDelivery.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCustomerNameDD.Text))
            {
                MessageBox.Show("Please delivery customer name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomerNameDD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddressDD.Text))
            {
                MessageBox.Show("Please enter delivery customer address", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddressDD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBillingCustCode.Text))
            {
                MessageBox.Show("Please enter billing customer code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBillingCustCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBillingCustName.Text))
            {
                MessageBox.Show("Please enter billing customer name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBillingCustName.Focus();
                return;
            }
            if (oMainDetailList.Count == 0)
            {
                MessageBox.Show("Please items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //kapila 27/7/2015
            if (_isStockUpdate == true)
            {
                if (string.IsNullOrEmpty(txtIssueLoc.Text))
                {
                    MessageBox.Show("Please enter issue location.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIssueLoc.Focus();
                    return;
                }
            }

            if (txtInvoiceType.Text == "CS")
            {
                if (_recieptItem == null || _recieptItem.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("For Cash sales, need to add payment details.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkManualRef.Checked)
            {
                if (!string.IsNullOrEmpty(txtManualRefNo.Text) && !IsNumeric(txtManualRefNo.Text))
                {
                    MessageBox.Show("Please enter valid Manual reference number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //kapila 24/7/2017
            //DataTable _dtOldpart = CHNLSVC.CustService.Get_oldpart_byjob(BaseCls.GlbUserComCode, txtJobNo.Text);
            //if (_dtOldpart.Rows.Count > 0)
            //{
            //    if (string.IsNullOrEmpty(_dtOldpart.Rows[0]["sop_rtn_wh"].ToString()))
            //    {
            //        MessageBox.Show("Old parts not returned to the warehouse.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //}

            //   Nadeeka
            #region chk Arrears
            decimal _arrblk = 0;
            List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ARRBLK", "COM", BaseCls.GlbUserComCode);
            if (para.Count > 0)
            {
                _arrblk = para[0].Hsy_val;
            }


            foreach (Service_Confirm_detail conitem in oMainDetailList)
            {
                List<Service_job_Det> _JobDet = new List<Service_job_Det>();
                //foreach (Service_Confirm_detail item in oMainDetailList)
                //{
                _JobDet = CHNLSVC.CustService.GetJobDetails(conitem.Jcd_jobno, conitem.Jcd_joblineno, BaseCls.GlbUserComCode);
                if (_JobDet != null)
                {
                    foreach (Service_job_Det jitem in _JobDet)
                    {
                        if (!string.IsNullOrEmpty(jitem.Jbd_invc_no))
                        {
                            if (jitem.Jbd_invc_no != "N/A" && jitem.Jbd_isstockupdate == 0)
                            {
                                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, jitem.Jbd_invc_no, "C", null, null);
                                if (_invHdr != null && _invHdr.Count > 0)
                                {
                                    if (_invHdr[0].Sah_inv_tp == "HS")
                                    {
                                        DateTime dt1 = GetLastDayOfPreviousMonth(dtpDate.Value.Date);
                                        decimal _arrears = CHNLSVC.Sales.Get_Acc_Arrears(_invHdr[0].Sah_acc_no, dt1, null);
                                        if (_arrears > 0)
                                        {
                                            if (_arrblk > 0)
                                            {
                                                MessageBox.Show(" Please  settle the arrears for Hp Account No  : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " and  Arrears Amount : " + _arrears, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            else
                                            {
                                                if (MessageBox.Show(" Please  settle the arrears for Hp Account No  : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " and  Arrears Amount : " + _arrears + " , Do  you want to continue this ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                                //  MessageBox.Show(" Pls settle the arrears for hp account # : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " Arrears Amount : " + _arrears, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                // return;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            ////    }


            
             



            List<String> jobNumberList = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList();
            List<Service_TempIssue> oTempIssueList = new List<Service_TempIssue>();
            List<Service_TempIssue> oTempIssueList2 = new List<Service_TempIssue>();
            foreach (String item in jobNumberList)
            {
                oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, item, -999, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
            }
            if (oTempIssueList.Count > 0)
            {
                foreach (Service_TempIssue item in oTempIssueList)
                {
                    Service_Confirm_detail tempItem = oMainDetailList.Find(x => x.Jcd_itmcd == item.STI_ISSUEITMCD && x.Jcd_jobno == item.STI_JOBNO && x.Jcd_itmstus == item.STI_ISSUEITMSTUS);
                    if (tempItem != null && tempItem.Jcd_itmcd != null)
                    {
                    }
                    else
                    {
                        oTempIssueList2.Add(item);
                    }
                }
                if (oTempIssueList2.Count > 0)
                {
                    if (MessageBox.Show("There is stand by issued items. Do you want to add to invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ShowStandByItems();
                        return;
                    }
                }
            }
            if (MessageBox.Show("Do you want to save the service invoice", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string taxEmpty = string.Empty;
          

            foreach (Service_Confirm_detail item in oMainDetailList)
            {
                
                //List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, item.Jcd_itmcd, item.Jcd_itmstus, string.Empty, string.Empty);
                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, item.Jcd_itmcd, item.Jcd_itmstus, "VAT", string.Empty);
                if (_isStrucBaseTax == true)
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.Jcd_itmcd);
                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, item.Jcd_itmcd, item.Jcd_itmstus, "VAT", string.Empty, _mstItem.Mi_anal1);
                }
                else
                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, item.Jcd_itmcd, item.Jcd_itmstus, "VAT", string.Empty);
                if (_taxs == null || _taxs.Count == 0)
                {
                    taxEmpty += "," + item.Jcd_itmcd;
                }
                chngecustAdd1 = item.Jcd_cusadd1;//Add by tharanga
                chngecustAdd2 = item.Jcd_cusadd2;//add by tharanga 
            }
            if (!string.IsNullOrEmpty(taxEmpty))
            {
                MessageBox.Show("Tax not setup\n For item " + taxEmpty + ".\nPlease contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            InvoiceHeader _invheader = new InvoiceHeader();
            List<InvoiceItem> _invoiceItem = new List<InvoiceItem>();
            List<InvoiceSerial> _invoiceSerial = new List<InvoiceSerial>();

            _invheader.Sah_com = BaseCls.GlbUserComCode;
            _invheader.Sah_cre_by = BaseCls.GlbUserID;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = "LKR";
          
            //_invheader.Sah_cus_add1 = txtAddressDD.Text.Trim();
            //_invheader.Sah_cus_add2 = txtAddress2DD.Text.Trim();

            _invheader.Sah_cus_add1 = chngecustAdd1; //Add by tharanga
            _invheader.Sah_cus_add2 = chngecustAdd2; //Add by tharanga

            _invheader.Sah_cus_cd = txtBillingCustCode.Text;
            _invheader.Sah_cus_name = txtBillingCustName.Text.Trim();
            _invheader.Sah_d_cust_add1 = txtAddressDD.Text.Trim();
            _invheader.Sah_d_cust_add2 = txtAddress2DD.Text.Trim();
            _invheader.Sah_d_cust_cd = txtCustomerCodeDD.Text.Trim();
            _invheader.Sah_d_cust_name = txtCustomerNameDD.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = dtpDate.Value.Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = txtInvoiceType.Text.Trim();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_ref = txtManualRefNo.Text;
            _invheader.Sah_manual = chkManualRef.Checked ? true : false;
            _invheader.Sah_mod_by = BaseCls.GlbUserID;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = BaseCls.GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtInvoiceRefNo.Text;
            _invheader.Sah_remarks = txtRemarks.Text;
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = BaseCls.GlbUserID;
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = 1;
            _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
            // _invheader.Sah_structure_seq = txtQuotation.Text.Trim();
            _invheader.Sah_stus = "A";
            //  if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) _invheader.Sah_stus = "D";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_direct = true;
            _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
            //_invheader.Sah_anal_11 = (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
            //_invheader.Sah_del_loc = (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            _invheader.Sah_del_loc = string.Empty;
            //_invheader.Sah_grn_com = _customerCompany;
            //_invheader.Sah_grn_loc = _customerLocation;
            //_invheader.Sah_is_grn = _isCustomerHasCompany;
            //_invheader.Sah_grup_cd = string.IsNullOrEmpty(txtGroup.Text.Trim()) ? string.Empty : txtGroup.Text.Trim();
            _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
            _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
            if (chkAgreement.Checked == false)
            { _invheader.Sah_anal_2 = "SCV"; }
            else { _invheader.Sah_anal_2 = "SCVA"; }


            //_invheader.Sah_anal_6 = txtLoyalty.Text.Trim();
            _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
            _invheader.Sah_is_dayend = 0;
            // _invheader.Sah_remarks = txtRemarks.Text.Trim();
            //if (string.IsNullOrEmpty(Convert.ToString(cmbTechnician.SelectedValue))) _invheader.Sah_anal_1 = string.Empty;
            //else _invheader.Sah_anal_1 = Convert.ToString(cmbTechnician.SelectedValue);
            //_invheader.Sah_anal_1 = txtPromotor.Text;
            //if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());


            //Commected by akila. This not necessary. when load from invoice or job, customer will be checked and flag as svat or tax exempted
            //if (txtCustomer.Text.Trim() != "CASH")
            //{
            //    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCustomer.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
            //    if (_en != null)
            //        if (string.IsNullOrEmpty(_en.Mbe_com))
            //        {
            //            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
            //            _invheader.Sah_is_svat = _en.Mbe_is_svat;
            //        }
            //}

            RecieptHeader _recHeader = new RecieptHeader();
            {
                MasterBusinessEntity GetCustomerProfile = CHNLSVC.Sales.GetCustomerProfile(txtBillingCustCode.Text, string.Empty, string.Empty, string.Empty, string.Empty);
                if (!string.IsNullOrEmpty(cmbJobNumber.Text))
                {
                    _recHeader.Sar_acc_no = cmbJobNumber.Text;
                }
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_currency_cd = "LKR";
                _recHeader.Sar_debtor_add_1 = GetCustomerProfile.Mbe_add1;
                _recHeader.Sar_debtor_add_2 = GetCustomerProfile.Mbe_add2;
                _recHeader.Sar_debtor_cd = txtCustomer.Text;
                _recHeader.Sar_debtor_name = GetCustomerProfile.Mbe_name;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                _recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_is_used = false;
                _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                _recHeader.Sar_mob_no = txtTeleDD.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_nic_no = GetCustomerProfile.Mbe_nic;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = "";
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = dtpDate.Value.Date;
                _recHeader.Sar_receipt_no = "na";
                _recHeader.Sar_receipt_type = txtInvoiceType.Text == "CRED" ? "DEBT" : "DIR";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = "";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _recHeader.Sar_tel_no = GetCustomerProfile.Mbe_mob;
                _recHeader.Sar_tot_settle_amt = 0;
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
            }

            string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceType.Text);
            if (string.IsNullOrEmpty(_invoicePrefix))
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = 1;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = txtInvoiceType.Text;
            _invoiceAuto.Aut_number = 0;
            _invoiceAuto.Aut_start_char = _invoicePrefix;
            _invoiceAuto.Aut_year = dtpDate.Value.Year;
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
                    _receiptAuto.Aut_year = dtpDate.Value.Year;
                }

            MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
            _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            _masterAutoDo.Aut_cate_tp = "LOC";
            _masterAutoDo.Aut_direction = 0;
            _masterAutoDo.Aut_moduleid = "DO";
            _masterAutoDo.Aut_start_char = "DO";
            _masterAutoDo.Aut_year = dtpDate.Value.Year;

            string jobList = string.Empty;

            List<string> tempList = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList();

            foreach (string item in tempList)
            {
                jobList += item + ",";
            }
            jobList = jobList.Remove(jobList.Length - 1);

            bool isDeliveryNow = (chkDeliverLater.Checked) ? false : true;

            List<Service_JOB_HDR> jobHeaders = CHNLSVC.CustService.GetServiceJobHeaderAll(jobList, BaseCls.GlbUserComCode);
            if (jobHeaders.FindAll(x => x.SJB_MOD_BY == "F").Count > 0)
            {
                isDeliveryNow = true;

                ScanSerialList = new List<ReptPickSerials>();

                //int count = 1;
                //foreach (Service_Confirm_detail item in oMainDetailList)
                //{
                //    item.Jcd_line = count;
                //    count = count + 1;
                //}
            }

            int count = 1;
           
            //below loop Commented by Wimal @ 12/07/2018 to Stop incorrrect line no inserting into invoice.
           /*foreach (Service_Confirm_detail item in oMainDetailList)
            {
                item.Jcd_line = count;
                count = count + 1;
            }*/

            //if (txtInvoiceType.Text == "CRED" || txtInvoiceType.Text == "DEBT")
            //if (txtInvoiceType.Text == "CRED" || txtInvoiceType.Text == "DEBT")
            //{
            //    isDeliveryNow = false;
            //}

            string _invoiceNo = "";
            string _receiptNo = "";
            string _deliveryOrderNo = "";
            string _error = string.Empty;

            List<InvoiceVoucher> _giftVoucher = null;
            List<ReptPickSerials> _giftVoucherSerial = null;
            MasterAutoNumber _buybackAuto = new MasterAutoNumber();
            string _buybackadj = string.Empty;
            List<ReptPickSerials> BuyBackItemListNew = new List<ReptPickSerials>();

            InventoryHeader invHdr = new InventoryHeader();
            invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
            invHdr.Ith_com = BaseCls.GlbUserComCode;
            invHdr.Ith_doc_tp = "DO";
            invHdr.Ith_pc = BaseCls.GlbUserDefProf;
            invHdr.Ith_doc_date = Convert.ToDateTime(dtpDate.Text).Date;
            invHdr.Ith_doc_year = Convert.ToDateTime(dtpDate.Text).Year;
            //invHdr.Ith_cate_tp = txtInvoiceType.Text;
            invHdr.Ith_cate_tp = "DPS";
            invHdr.Ith_sub_tp = "DPS";
            invHdr.Ith_bus_entity = txtCustomer.Text.Trim();
            invHdr.Ith_del_add1 = txtAddressDD.Text.Trim();
            invHdr.Ith_del_add1 = txtAddress2DD.Text.Trim();
            invHdr.Ith_is_manual = false;
            invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = BaseCls.GlbUserID;
            invHdr.Ith_mod_by = BaseCls.GlbUserID;
            invHdr.Ith_direct = false;
            invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
            invHdr.Ith_manual_ref = txtManualRefNo.Text;
            invHdr.Ith_vehi_no = string.Empty;
            invHdr.Ith_remarks = string.Empty;
            invHdr.Ith_job_no = txtJobNo.Text.Trim();

            //for (int i = 0; i < oMainDetailList.Count; i++)
            //{
            //    oMainDetailList[0].Jcd_itmline = i + 1;
            //}

            int reLine = 0;
            foreach (RecieptItem _reCitem in _recieptItem)
            {
                reLine = reLine + 1;
                _reCitem.Sard_line_no = reLine;
            }

            #region ADO fill 2015-06-26

            List<Service_job_Det> _processJobDet = new List<Service_job_Det>();
            var JobnLines = oMainDetailList.Select(m => new { m.Jcd_jobno, m.Jcd_joblineno }).Distinct().ToList();

            foreach (var itemJobnLines in JobnLines)
            {
                _processJobDet.AddRange(CHNLSVC.CustService.GetJobDetails(itemJobnLines.Jcd_jobno, itemJobnLines.Jcd_joblineno, BaseCls.GlbUserComCode));
            }

            bool _isRcc;
            // Boolean _isStockUpdate = false;
            InventoryHeader _aodHdr = new InventoryHeader();
            string _rccNo = "";
            MasterAutoNumber _AodAuto = new MasterAutoNumber();
            List<ReptPickSerials> _AodserialList = new List<ReptPickSerials>();

            //check job type whether rcc or not
            Service_JOB_HDR _JobHdrRcc = new Service_JOB_HDR();
            if (_processJobDet.Count > 0)
            {
                _JobHdrRcc = CHNLSVC.CustService.GetServiceJobHeader(_processJobDet[0].Jbd_jobno, BaseCls.GlbUserComCode);
            }
            _isRcc = false;
            RCC _rcc = new RCC();

            if (_JobHdrRcc != null)
            {
                if (_JobHdrRcc.SJB_JOBSTP == "RCC")
                {
                    _rcc.Inr_is_repaired = true;
                    _rcc.Inr_no = _JobHdrRcc.SJB_REQNO;
                    _rccNo = _JobHdrRcc.SJB_REQNO;
                    _rcc.Inr_is_returned = true;
                    _rcc.Inr_hollogram_no = "SCM2";
                    _isRcc = true;
                }
            }

            string _aodIssuLoc = "";


            foreach (Service_job_Det _proJob in _processJobDet)
            {
                if (_proJob.Jbd_isstockupdate == 1)
                {
                    //_isStockUpdate = true;
                    //_aodIssuLoc = _proJob.Jbd_aodissueloc;

                    if (_aodHdr.Ith_job_no == null)
                    {
                        //       #region Inventory Header Value Assign
                        _aodHdr.Ith_acc_no = string.Empty;
                        _aodHdr.Ith_anal_1 = string.Empty;
                        _aodHdr.Ith_anal_10 = false;//Direct AOD
                        _aodHdr.Ith_anal_11 = false;
                        _aodHdr.Ith_anal_12 = false;
                        _aodHdr.Ith_anal_2 = string.Empty;
                        _aodHdr.Ith_anal_3 = string.Empty;
                        _aodHdr.Ith_anal_4 = string.Empty;
                        _aodHdr.Ith_anal_5 = string.Empty;
                        _aodHdr.Ith_anal_6 = 0;
                        _aodHdr.Ith_anal_7 = 0;
                        _aodHdr.Ith_anal_8 = Convert.ToDateTime(dtpDate.Value).Date;
                        _aodHdr.Ith_anal_9 = Convert.ToDateTime(dtpDate.Value).Date;
                        _aodHdr.Ith_bus_entity = string.Empty;
                        _aodHdr.Ith_cate_tp = "NOR";
                        _aodHdr.Ith_channel = string.Empty;
                        _aodHdr.Ith_com = BaseCls.GlbUserComCode;
                        _aodHdr.Ith_com_docno = string.Empty;
                        _aodHdr.Ith_cre_by = BaseCls.GlbUserID;
                        _aodHdr.Ith_cre_when = DateTime.Now.Date;
                        _aodHdr.Ith_del_add1 = string.Empty;
                        _aodHdr.Ith_del_add2 = string.Empty;
                        _aodHdr.Ith_del_code = string.Empty;
                        _aodHdr.Ith_del_party = string.Empty;
                        _aodHdr.Ith_del_town = string.Empty;
                        _aodHdr.Ith_direct = false;
                        _aodHdr.Ith_doc_date = Convert.ToDateTime(dtpDate.Value).Date;
                        _aodHdr.Ith_doc_no = string.Empty;
                        _aodHdr.Ith_doc_tp = "AOD";
                        _aodHdr.Ith_doc_year = Convert.ToDateTime(dtpDate.Value).Date.Year;
                        _aodHdr.Ith_entry_no = string.Empty;
                        _aodHdr.Ith_entry_tp = string.Empty;
                        _aodHdr.Ith_git_close = false;
                        _aodHdr.Ith_git_close_date = Convert.ToDateTime(dtpDate.Value).Date;
                        _aodHdr.Ith_git_close_doc = string.Empty;
                        _aodHdr.Ith_is_manual = false;
                        _aodHdr.Ith_isprinted = false;
                        _aodHdr.Ith_sub_docno = (_rccNo != "") ? _rccNo : _proJob.Jbd_jobno;
                        _aodHdr.Ith_loading_point = string.Empty;
                        _aodHdr.Ith_loading_user = string.Empty;
                        _aodHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                        _aodHdr.Ith_manual_ref = "0";
                        _aodHdr.Ith_mod_by = BaseCls.GlbUserID;
                        _aodHdr.Ith_mod_when = DateTime.Now.Date;
                        _aodHdr.Ith_noofcopies = 0;
                        _aodHdr.Ith_oth_loc = txtIssueLoc.Text;  //kapila 27/7/2015
                        _aodHdr.Ith_oth_docno = _rccNo;
                        _aodHdr.Ith_remarks = string.Empty;
                        _aodHdr.Ith_sbu = string.Empty;
                        //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
                        _aodHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                        _aodHdr.Ith_stus = "A";
                        _aodHdr.Ith_sub_tp = "SERVICE";    // string.Empty; 10/7/2013
                        _aodHdr.Ith_cate_tp = "NOR";
                        _aodHdr.Ith_vehi_no = string.Empty;
                        _aodHdr.Ith_oth_com = BaseCls.GlbUserComCode;
                        _aodHdr.Ith_anal_1 = "0";
                        _aodHdr.Ith_anal_2 = string.Empty;
                        _aodHdr.Ith_job_no = _processJobDet[0].Jbd_jobno;

                        _AodAuto.Aut_moduleid = "AOD";
                        _AodAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                        _AodAuto.Aut_cate_tp = "LOC";
                        _AodAuto.Aut_direction = 0;
                        _AodAuto.Aut_modify_dt = null;
                        _AodAuto.Aut_year = DateTime.Now.Year;
                        _AodAuto.Aut_start_char = "AOD";
                    }

                    Int32 _serID = 0;
                    if (String.IsNullOrEmpty(_proJob.Jbd_aodrecno))
                    {
                        MessageBox.Show("Cannot find the AOD Reciept number", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    DataTable _dtSer = CHNLSVC.CustService.getSerialIDByDocument(_proJob.Jbd_aodrecno, _proJob.Jbd_itm_cd, _proJob.Jbd_ser1);
                    if (_dtSer.Rows.Count > 0)
                    {
                        _serID = Convert.ToInt32(_dtSer.Rows[0]["its_ser_id"]);

                        //_AodserialList = new List<ReptPickSerials>();
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, _proJob.Jbd_itm_cd, _serID);
                        if (_reptPickSerial_.Tus_itm_cd == null)
                        {
                            MessageBox.Show("Item is not available in the stock. Item : " + _proJob.Jbd_itm_cd, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial_.Tus_usrseq_no = 1;
                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial_.Tus_base_doc_no = "N/A";
                        _reptPickSerial_.Tus_base_itm_line = 0;
                        _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                        _reptPickSerial_.Tus_job_no = _proJob.Jbd_jobno;
                        _reptPickSerial_.Tus_job_line = _proJob.Jbd_jobline;

                        MasterItem msitem = new MasterItem();
                        msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _proJob.Jbd_itm_cd);
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _AodserialList.Add(_reptPickSerial_);
                    }
                }
            }


            #endregion

            string ADONUmber;

            Int32 effect = 0;

            if (chkTaxPayable.Checked == true)
            {// Nadeeka 30-12-2015
                if (IsDiffTax(_invoiceItemList) == false)
                {
                    MessageBox.Show("Two different tax rates are not allowed according to the new government procedures for tax invoices.", "Tax Rates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            #region ac free shedule
            Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
            Service_Req_Det _jobdetail = new Service_Req_Det();
            Service_Chanal_parameter oChnnalPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (oChnnalPara.SP_ISNEEDGATEPASS == 0)
            {

                #region ACRequest Tharanga 2018-06-12

                DataTable jobhdr = new DataTable();
                DataTable jobdet = new DataTable();
                //Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
                //Service_Req_Det _jobdetail = new Service_Req_Det();


                jobhdr = CHNLSVC.CustService.GetJobHeader(cmbJobNumber.Text.ToString().Trim(), BaseCls.GlbUserComCode);
                jobdet = CHNLSVC.CustService.GetJobDetail(cmbJobNumber.Text.ToString().Trim(), BaseCls.GlbUserComCode);
                int _returnStatus = 0;
                string _returnMsg = string.Empty;
                _returnStatus = CHNLSVC.CustService.GetScvReq(BaseCls.GlbUserComCode, txtJobNo.Text.ToString().Trim(), out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _returnMsg);
                string jobcat = jobdet.Rows[0]["jbd_cate1"].ToString();
                //string jobcat = jobhdr.Rows[0]["sjb_jobcat"].ToString();
                string warrstatus = jobdet.Rows[0]["jbd_warr_stus"].ToString();

                if (jobcat == "AC" && warrstatus == "1")
                {
                    if (lstService.Count <= 0)
                    {
                        //MessageBox.Show("Cannot process. Please fill The Service Deatils", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //btnSave.Enabled = true;
                        //return;
                        if (MessageBox.Show("Do you want continue without free service shedule ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                    }

                    //if (MessageBox.Show("Do you want continue without free service shedule ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //}
                    //else
                    //{
                        if (lstService.Count <= 0)
                        {
                            MessageBox.Show("Cannot process. Please fill The Service Deatils", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            btnSave.Enabled = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(txtscvloac.Text))
                        {
                            MessageBox.Show("Select Free service Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (string.IsNullOrEmpty(txtscvprocnter.Text))
                        {
                            MessageBox.Show("Select Free Profitcenter Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        #region JobHeaderData
                        foreach (DataRow _dr in jobhdr.Rows)
                        {
                            _jobHeader.Srb_seq_no = _dr["SJB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(_dr["SJB_SEQ_NO"].ToString());
                            _jobHeader.Srb_reqno = _dr["SJB_REQNO"].ToString();
                            _jobHeader.Srb_dt = Convert.ToDateTime(_dr["SJB_DT"].ToString());
                            _jobHeader.Srb_com = _dr["SJB_COM"].ToString();
                            _jobHeader.Srb_jobcat = "FF";
                            _jobHeader.Srb_jobtp = _dr["SJB_JOBTP"].ToString();
                            _jobHeader.Srb_jobstp = _dr["SJB_JOBSTP"].ToString();
                            _jobHeader.Srb_manualref = _dr["SJB_MANUALREF"].ToString();
                            _jobHeader.Srb_otherref = txtJobNo.Text.ToString().Trim();
                            _jobHeader.Srb_jobstage = 1; // put due to the service level convertion always this request stage is 1 
                            _jobHeader.Srb_rmk = _dr["SJB_RMK"].ToString();
                            _jobHeader.Srb_prority = _dr["SJB_PRORITY"].ToString();
                            _jobHeader.Srb_st_dt = Convert.ToDateTime(_dr["SJB_ST_DT"].ToString());
                            _jobHeader.Srb_ed_dt = Convert.ToDateTime(_dr["SJB_ED_DT"].ToString());
                            _jobHeader.Srb_noofprint = Convert.ToInt32(_dr["SJB_NOOFPRINT"].ToString());
                            _jobHeader.Srb_lastprintby = _dr["SJB_LASTPRINTBY"].ToString();
                            _jobHeader.Srb_orderno = _dr["SJB_ORDERNO"].ToString();
                            _jobHeader.Srb_custexptdt = Convert.ToDateTime(_dr["SJB_CUSTEXPTDT"].ToString());
                            _jobHeader.Srb_substage = _dr["SJB_SUBSTAGE"].ToString();
                            _jobHeader.Srb_cust_cd = _dr["SJB_CUST_CD"].ToString();
                            _jobHeader.Srb_cust_tit = _dr["SJB_CUST_TIT"].ToString();
                            _jobHeader.Srb_cust_name = _dr["SJB_CUST_NAME"].ToString();
                            _jobHeader.Srb_nic = _dr["SJB_NIC"].ToString();
                            _jobHeader.Srb_dl = _dr["SJB_DL"].ToString();
                            _jobHeader.Srb_pp = _dr["SJB_PP"].ToString();
                            _jobHeader.Srb_mobino = _dr["SJB_MOBINO"].ToString();
                            _jobHeader.Srb_add1 = _dr["SJB_ADD1"].ToString();
                            _jobHeader.Srb_add2 = _dr["SJB_ADD2"].ToString();
                            _jobHeader.Srb_add3 = _dr["SJB_ADD3"].ToString();
                            _jobHeader.Srb_b_cust_cd = _dr["SJB_B_CUST_CD"].ToString();
                            _jobHeader.Srb_b_cust_tit = _dr["SJB_B_CUST_TIT"].ToString();
                            _jobHeader.Srb_b_cust_name = _dr["SJB_B_CUST_NAME"].ToString();
                            _jobHeader.Srb_b_nic = _dr["SJB_B_NIC"].ToString();
                            _jobHeader.Srb_b_dl = _dr["SJB_B_DL"].ToString();
                            _jobHeader.Srb_b_pp = _dr["SJB_B_PP"].ToString();
                            _jobHeader.Srb_mobino = _dr["SJB_B_MOBINO"].ToString();
                            _jobHeader.Srb_b_town = _dr["SJB_B_TOWN"].ToString();
                            _jobHeader.Srb_b_phno = _dr["SJB_B_PHNO"].ToString();
                            _jobHeader.Srb_b_fax = _dr["SJB_B_FAX"].ToString();
                            _jobHeader.Srb_b_email = _dr["SJB_B_EMAIL"].ToString();
                            _jobHeader.Srb_infm_person = _dr["SJB_INFM_PERSON"].ToString();
                            _jobHeader.Srb_infm_add1 = _dr["SJB_INFM_ADD1"].ToString();
                            _jobHeader.Srb_infm_add2 = _dr["SJB_INFM_ADD2"].ToString();
                            _jobHeader.Srb_infm_phno = _dr["SJB_INFM_PHNO"].ToString();
                            _jobHeader.Srb_stus = _dr["SJB_STUS"].ToString();
                            _jobHeader.Srb_cre_by = _dr["SJB_CRE_BY"].ToString();
                            _jobHeader.Srb_mod_by = _dr["SJB_MOD_BY"].ToString();
                            _jobHeader.Srb_mod_dt = Convert.ToDateTime(_dr["SJB_MOD_DT"].ToString());

                        }
                        #endregion

                        #region JobDetailData
                        _scvItemList = new List<Service_Req_Det>();
                        foreach (DataRow _dr in jobdet.Rows)
                        {
                            _jobdetail.Jrd_seq_no = Convert.ToInt32(_dr["JBD_SEQ_NO"].ToString());
                            _jobdetail.Jrd_reqno = _dr["JBD_JOBNO"].ToString();
                            //_jobdetail.Jrd_reqline = Convert.ToInt32(_dr["JBD_REQLINE"].ToString());
                            _jobdetail.Jrd_reqline = 1;
                            _jobdetail.Jrd_sjobno = _dr["JBD_JOBNO"].ToString();
                            _jobdetail.Jrd_loc = _dr["JBD_LOC"].ToString();
                            _jobdetail.Jrd_pc = _dr["JBD_PC"].ToString();
                            _jobdetail.Jrd_sjobno = _dr["JBD_SJOBNO"].ToString();
                            _jobdetail.Jrd_loc = txtscvloac.Text.ToString();// _dr["JBD_LOC"].ToString();
                            _jobdetail.Jrd_pc = txtscvprocnter.Text.ToString();// _dr["JBD_PC"].ToString();
                            _jobdetail.Jrd_itm_cd = _dr["JBD_ITM_CD"].ToString();
                            _jobdetail.Jrd_itm_stus = _dr["JBD_ITM_STUS"].ToString();
                            _jobdetail.Jrd_itm_desc = _dr["JBD_ITM_DESC"].ToString();
                            _jobdetail.Jrd_brand = _dr["JBD_BRAND"].ToString();
                            _jobdetail.Jrd_model = _dr["JBD_MODEL"].ToString();
                            _jobdetail.Jrd_itm_cost = Convert.ToDecimal(_dr["JBD_ITM_COST"].ToString());
                            _jobdetail.Jrd_ser1 = _dr["JBD_SER1"].ToString();
                            _jobdetail.Jrd_ser2 = _dr["JBD_SER2"].ToString();
                            _jobdetail.Jrd_warr = _dr["JBD_WARR"].ToString();
                            _jobdetail.Jrd_regno = _dr["JBD_REGNO"].ToString();
                            _jobdetail.Jrd_milage = Convert.ToInt32(_dr["JBD_MILAGE"].ToString());
                            _jobdetail.Jrd_warr_stus = Convert.ToInt32(_dr["JBD_WARR_STUS"].ToString());
                            _jobdetail.Jrd_onloan = Convert.ToInt32(_dr["JBD_ONLOAN"].ToString());
                            _jobdetail.Jrd_chg_warr_stdt = Convert.ToDateTime(_dr["JBD_CHG_WARR_STDT"].ToString());
                            _jobdetail.Jrd_chg_warr_rmk = _dr["JBD_CHG_WARR_RMK"].ToString();
                            //  _jobHeader.Jrd_sentwcn
                            _jobdetail.Jrd_isinsurance = Convert.ToInt32(_dr["JBD_ISINSURANCE"].ToString());
                            _jobdetail.Jrd_ser_term = Convert.ToInt32(_dr["JBD_SER_TERM"].ToString());
                            _jobdetail.Jrd_lastwarr_stdt = Convert.ToDateTime(_dr["JBD_LASTWARR_STDT"].ToString());
                            _jobdetail.Jrd_issued = Convert.ToInt32(_dr["JBD_ISSUED"].ToString());
                            _jobdetail.Jrd_mainitmcd = _dr["JBD_MAINITMCD"].ToString();
                            _jobdetail.Jrd_mainitmser = _dr["JBD_MAINITMSER"].ToString();
                            _jobdetail.Jrd_mainitmwarr = _dr["JBD_MAINITMWARR"].ToString();
                            _jobdetail.Jrd_itmmfc = _dr["JBD_ITMMFC"].ToString();
                            _jobdetail.Jrd_mainitmmfc = _dr["JBD_MAINITMMFC"].ToString();
                            _jobdetail.Jrd_availabilty = Convert.ToInt32(_dr["JBD_AVAILABILTY"].ToString());
                            _jobdetail.Jrd_usejob = _dr["JBD_USEJOB"].ToString();
                            _jobdetail.Jrd_msnno = _dr["JBD_MSNNO"].ToString();
                            _jobdetail.Jrd_itmtp = _dr["JBD_ITMTP"].ToString();
                            _jobdetail.Jrd_serlocchr = _dr["JBD_SERLOCCHR"].ToString();
                            _jobdetail.Jrd_custnotes = _dr["JBD_CUSTNOTES"].ToString();
                            _jobdetail.Jrd_mainreqno = _dr["JBD_MAINREQNO"].ToString();
                            _jobdetail.Jrd_mainreqloc = _dr["JBD_MAINREQLOC"].ToString();
                            _jobdetail.Jrd_mainjobno = _dr["JBD_MAINJOBNO"].ToString();
                            _jobdetail.Jrd_isstockupdate = Convert.ToInt32(_dr["JBD_ISSTOCKUPDATE"].ToString());
                            _jobdetail.Jrd_needgatepass = Convert.ToInt32(_dr["JBD_ISGATEPASS"].ToString());
                            _jobdetail.Jrd_iswrn = Convert.ToInt32(_dr["JBD_ISWRN"].ToString());
                            _jobdetail.Jrd_warrperiod = Convert.ToInt32(_dr["JBD_WARRPERIOD"].ToString());
                            _jobdetail.Jrd_warrrmk = _dr["JBD_WARRRMK"].ToString();
                            _jobdetail.Jrd_warrstartdt = Convert.ToDateTime(_dr["JBD_WARRSTARTDT"].ToString());
                            _jobdetail.Jrd_warrreplace = Convert.ToInt32(_dr["JBD_WARRREPLACE"].ToString());
                            _jobdetail.Jrd_date_pur = Convert.ToDateTime(_dr["JBD_DATE_PUR"].ToString());
                            _jobdetail.Jrd_invc_no = _dr["JBD_INVC_NO"].ToString();
                            _jobdetail.Jrd_waraamd_seq = _dr["JBD_WARAAMD_SEQ"].ToString();
                            _jobdetail.Jrd_waraamd_by = _dr["JBD_WARAAMD_BY"].ToString();
                            _jobdetail.Jrd_waraamd_dt = Convert.ToDateTime(_dr["JBD_WARAAMD_DT"].ToString());
                            _jobdetail.Jrd_invc_showroom = _dr["JBD_INVC_SHOWROOM"].ToString();
                            _jobdetail.Jrd_aodissueloc = _dr["JBD_AODISSUELOC"].ToString();
                            _jobdetail.Jrd_aodissuedt = Convert.ToDateTime(_dr["JBD_AODISSUEDT"].ToString());
                            _jobdetail.Jrd_aodissueno = _dr["JBD_AODRECNO"].ToString();
                            _jobdetail.Jrd_aodrecno = _dr["JBD_AODISSUENO"].ToString();
                            _jobdetail.Jrd_techst_dt = Convert.ToDateTime(_dr["JBD_TECHST_DT"].ToString());
                            _jobdetail.Jrd_techfin_dt = Convert.ToDateTime(_dr["JBD_TECHFIN_DT"].ToString());
                            //    _jobdetail.Jrd_msn_no = _dr["JBD_MSNNO"].ToString();
                            _jobdetail.Jrd_isexternalitm = Convert.ToInt32(_dr["JBD_ISEXTERNALITM"].ToString());
                            _jobdetail.Jrd_conf_dt = Convert.ToDateTime(_dr["JBD_CONF_DT"].ToString());
                            _jobdetail.Jrd_conf_cd = _dr["JBD_CONF_CD"].ToString();
                            _jobdetail.Jrd_conf_desc = _dr["JBD_CONF_DESC"].ToString();
                            _jobdetail.Jrd_conf_rmk = _dr["JBD_CONF_RMK"].ToString();
                            _jobdetail.Jrd_tranf_by = _dr["JBD_TRANF_BY"].ToString();
                            _jobdetail.Jrd_tranf_dt = Convert.ToDateTime(_dr["JBD_TRANF_DT"].ToString());
                            _jobdetail.Jrd_do_invoice = Convert.ToInt32(_dr["JBD_DO_INVOICE"].ToString());
                            _jobdetail.Jrd_insu_com = _dr["JBD_INSU_COM"].ToString();
                            _jobdetail.Jrd_agreeno = _dr["JBD_AGREENO"].ToString();
                            _jobdetail.Jrd_issrn = Convert.ToInt32(_dr["JBD_ISSRN"].ToString());
                            _jobdetail.Jrd_isagreement = _dr["JBD_ISAGREEMENT"].ToString();
                            _jobdetail.Jrd_cust_agreeno = _dr["JBD_CUST_AGREENO"].ToString();
                            _jobdetail.Jrd_quo_no = _dr["JBD_QUO_NO"].ToString();
                            _jobdetail.Jrd_stage = 1;
                            _jobdetail.Jrd_com = _dr["JBD_COM"].ToString();
                            _jobdetail.Jrd_ser_id = _dr["JBD_SER_ID"].ToString();
                            _jobdetail.Jrd_used = 0;
                            _jobdetail.Jrd_jobno = "";
                            _jobdetail.Jrd_jobline = 0;
                            //     _jobdetail.Jrd_Select = Convert.ToBoolean(_dr["JBD_SJOBNO"].ToString());
                            _jobdetail.Jrd_supp_cd = _dr["JBD_SUPP_CD"].ToString();

                            //_jobdetail.Jrd_used = Convert.ToInt32(_dr["JBD_ACT"].ToString()); due to the qa issues
                            //_jobdetail.Jrd_jobno = _dr["JBD_JOBNO"].ToString();
                            //_jobdetail.Jrd_jobline = Convert.ToInt32(_dr["JBD_JOBLINE"].ToString());
                            //_jobdetail.Jrd_stage = Convert.ToDecimal(_dr["JBD_STAGE"].ToString());

                            _scvItemList.Add(_jobdetail);
                        }
                        #endregion

                        string jobNo;
                        string receiptNo = string.Empty;
                        string _msg1 = "";


                        for (int i = 0; i < lstService.Count; i++)
                        {
                            _jobHeader.Srb_dt = lstService[i].Servicedates;
                            _jobHeader.Srb_st_dt = lstService[i].Servicedates;
                            _jobHeader.Srb_ed_dt = lstService[i].Servicedates;
                            _jobHeader.Srb_custexptdt = lstService[i].Servicedates;

                            //  int eff = CHNLSVC.CustService.Save_Req(_jobHeader, _scvItemList, _scvDefList, _scvItemSubList, _jobAuto1, BaseCls.GlbDefSubChannel, "", "", _warStus, _jobAuto1, out _msg1, out jobNo, 0, DateTime.Now.Date, DateTime.Now.Date);

                        }
                   // }
                }

                #endregion
            }
            #endregion

            #region Job Auto Number
              MasterAutoNumber _jobAuto1 = new MasterAutoNumber();
              _jobAuto1.Aut_cate_cd = txtscvloac.Text;
                _jobAuto1.Aut_cate_tp = "LOC";
                _jobAuto1.Aut_moduleid = "SVREQ";
                _jobAuto1.Aut_direction = 0;
                _jobAuto1.Aut_year = _jobHeader.Srb_dt.Year;
                _jobAuto1.Aut_start_char = "SVREQ";
                #endregion

                string _acreqno = string.Empty;
                if (chkAgreement.Checked == true)
                {
                //effect = CHNLSVC.Sales.ServiceInvoiceSave(_invheader, oMainDetailList, _invoiceItemList, _invoiceSerial, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, isDeliveryNow, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, false, out _error, _AodAuto, _rcc, _isRcc, _isStockUpdate, _aodHdr, _AodserialList, _lstShed, out ADONUmber);//, _giftVoucher, _buybackheader, _buybackAuto, null, out _buybackadj);
                effect = CHNLSVC.Sales.ServiceInvoiceSave(_invheader, oMainDetailList, _invoiceItemList, _invoiceSerial, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, isDeliveryNow, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, false, out _error, _AodAuto, _rcc, _isRcc, _isStockUpdate, _aodHdr, _AodserialList, _lstShed, out ADONUmber,out _acreqno,
                      _jobHeader, _scvItemList, lstService, _scvDefList, _scvItemSubList, _jobAuto1, BaseCls.GlbDefSubChannel, "", "", 0, _jobAuto1,0);
                }
            else
                {
                //effect = CHNLSVC.Sales.ServiceInvoiceSave(_invheader, oMainDetailList, _invoiceItemList, _invoiceSerial, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, isDeliveryNow, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, false, out _error, _AodAuto, _rcc, _isRcc, _isStockUpdate, _aodHdr, _AodserialList, null, out ADONUmber);//, _giftVoucher, _buybackheader, _buybackAuto, null, out _buybackadj);
                   effect = CHNLSVC.Sales.ServiceInvoiceSave(_invheader, oMainDetailList, _invoiceItemList, _invoiceSerial, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, isDeliveryNow, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, false, out _error, _AodAuto, _rcc, _isRcc, _isStockUpdate, _aodHdr, _AodserialList, null, out ADONUmber,out _acreqno,
                          _jobHeader, _scvItemList, lstService, _scvDefList, _scvItemSubList, _jobAuto1, BaseCls.GlbDefSubChannel, "", "", 0, _jobAuto1,0);

                }
            String Msg = string.Empty;

            if (effect != -1)
            {
                if (isDeliveryNow)
                {
                    if (_deliveryOrderNo.Length > 0)
                    {
                        if (string.IsNullOrEmpty(_acreqno))
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". ";
                        }
                        else
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". " + "Service Request No : " + _acreqno; 
                        }
                        
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_acreqno))
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                        }
                        else
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + ". " + "Service Request No : " + _acreqno; 
                        }

                    }

                    sendMsg(_invheader, oMainDetailList);

                }
                else
                {
                    Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                    sendMsg(_invheader, oMainDetailList);
                }

                if (!string.IsNullOrEmpty(ADONUmber))
                {

                    Msg = Msg + "\nAOD Receipt number : " + ADONUmber;
                }

                MessageBox.Show(Msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearAll();
                serviceClear();
                string _repname = string.Empty;
                string _papersize = string.Empty;
                BaseCls.GlbReportTp = "JOBINV";
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                ReportViewer _viewsvc = new ReportViewer();
                _viewsvc.GlbReportName = BaseCls.GlbReportName;
                _viewsvc.GlbReportDoc = _invoiceNo;
                BaseCls.GlbReportDoc = _invoiceNo;

                if (BaseCls.GlbDefSubChannel == "MCS" || BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefSubChannel == "RIT")//Tharanga add ABL 2017/07/07
                { if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
                else
                { BaseCls.GlbReportDirectPrint = 0; }
                if (BaseCls.GlbReportDirectPrint == 1)
                {// Nadeeka 11-07-2015 (Direct Print/ Sanjeewa's process)
                    FF.WindowsERPClient.Reports.Service.clsServiceRep obj = new FF.WindowsERPClient.Reports.Service.clsServiceRep();
                    if (BaseCls.GlbReportName == "Service_Invoice_ABE.rpt")//Add by tharanga 2017/07/07
                    {
                        obj.Service_Invocie_ABE();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        obj._Service_Invoice_ABE.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Invoice documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obj._Service_Invoice_ABE.PrintToPrinter(1, false, 0, 0);

                        if (_deliveryOrderNo.Length > 0)
                        {
                            Reports.Inventory.ReportViewerInventory _ReportViewerInventory = new Reports.Inventory.ReportViewerInventory();
                            _ReportViewerInventory.GlbReportDoc = _deliveryOrderNo;
                            _ReportViewerInventory.Do_print_ABE();
                            _ReportViewerInventory._DO_print_ABE.PrintOptions.PrinterName = _ReportViewerInventory.GetDefaultPrinter();
                            _ReportViewerInventory._DO_print_ABE.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    else
                    {
                        // FF.WindowsERPClient.Reports.Service.clsServiceRep obj = new FF.WindowsERPClient.Reports.Service.clsServiceRep();
                        obj.InvociePrintServicePhone();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        obj._JobInvoicePh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Invoice documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obj._JobInvoicePh.PrintToPrinter(1, false, 0, 0);
                    }
                }
                else
                {
                    _viewsvc.Show();
                    _viewsvc = null;
                    if (BaseCls.GlbUserComCode == "AAA" && BaseCls.GlbDefSubChannel == "RRC1") // Add by tharanga 2017/07/10
                    {
                        if (_deliveryOrderNo.Length > 0)
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            _view.GlbReportName = "DO_print_ABE.rpt";
                            BaseCls.GlbReportName = "DO_print_ABE.rpt";
                            _view.GlbReportDoc = _deliveryOrderNo;
                            _view.Show();
                            _view = null;

                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Process Terminated\n" + _error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
         
            if (IsStartUp == false)
            {
                if (MessageBox.Show("Do you want to clear", "clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            btnAddItem.Enabled = true;
            dgvEstimateItems.Enabled = true;

            dgvAgr.DataSource = null;
            _lstShed = new List<scv_agr_payshed>();
            dgvPaySch.AutoGenerateColumns = false;
            dgvPaySch.DataSource = _lstShed;


            dtpDate.Value = DateTime.Now;
            txtJobNo.Clear();
            txtDisAmt.Clear();

            lblPromoVouNo.Text = "";
            lblPromoVouUsedFlag.Text = "";

            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            lblIsEditDesctiption.Text = "";
            lblItemUOM.Text = "";
            lblItemType.Text = "";
            lblManualRefNo.Text = "";
            lblcostPrice.Text = "";
            lblReqNo.Text = "";
            ClearMiddle1p0();
            txtCostTotal.Clear();
            txtRevenueTotal.Clear();
            txtMarginTotal.Clear();
            ClearRight1p1();
            lblSeq.Text = "";

            //oEstimate_Items.Clear();
            oMainDetailList = null;
            dgvEstimateItems.ReadOnly = false;
            txtMarginPercentage.Clear();
            txtJobNo.Focus();
            lblBackDateInfor.Text = "";

            _masterBusinessCompany = new MasterBusinessEntity();

            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;

            ClearVariable();

            lblcostPrice.Text = "";

            pnlDeliveryDetails.Visible = false;
            pnlDeliveryInstruction.Visible = false;
            pnlStandByItems.Visible = false;
            _recieptItem = new List<RecieptItem>();
            rbnNewItem.Checked = true;
            editItem = new Service_Confirm_detail();
            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            dgvItems.DataSource = new List<Service_TempIssue>();
            dgvJobConf.DataSource = new List<Service_confirm_Header>();

            ucPayModes1.ClearControls();
            ucPayModes1.ClearControls();
            ucPayModes1.TotalAmount = 0;
            ucPayModes1.InvoiceItemList = null;
            ucPayModes1.SerialList = null;
            ucPayModes1.Amount.Text = "0";
            ucPayModes1.Mobile = string.Empty;
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.LoadData();

            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            txtRequest.Focus();
            rbnNewItem.Checked = true;
            chkManualRef.Checked = false;
            chkTaxPayable.Checked = false;

            IsEdit = false;

            dtpFromD.Value = DateTime.Today.AddDays(-30);
            dtpToD.Value = DateTime.Today;
            btnSearchHeader_Click(null, null);
            btnAddItem.Enabled = true;
            btnSave.Enabled = true;
            cmbJobNumber.Text = "";

            cmbJobNumber.DataSource = new List<String>();

            SelectedSerialID = 0;

            IsStartUp = false;
            chkMargeUtem.Checked = false;
            chkSelectAll.Checked = false;
            txtRemarks.Clear();
            btnSave.Enabled = true;
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
                this.Close();
            }
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            GetJobDetails();
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            if (_isItemChecking) { _isItemChecking = false; return; }
            _isItemChecking = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    //MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                    return;
                if (IsNumeric(txtQty.Text) == false)
                {
                    this.Cursor = Cursors.Default;

                    MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!_isCompleteCode)
                {
                    //check minus unit price validation
                    decimal _unitAmt = 0;
                    bool _isUnitAmt = Decimal.TryParse(txtUnitPrice.Text, out _unitAmt);
                    if (!_isUnitAmt)
                    {
                        //using (new CenterWinDialog(this))
                        {
                            MessageBox.Show("Unit Price has to be number!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }
                    if (_unitAmt <= 0)
                    {
                        // using (new CenterWinDialog(this))
                        {
                            MessageBox.Show("Unit Price has to be greater than 0!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                    {
                        decimal _pb_price;
                        //if (SSPriceBookPrice == 0)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    //using (new CenterWinDialog(this))
                        //    {
                        //        MessageBox.Show("Price not define. Please check the system updated price.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    txtUnitPrice.Text = FormatToCurrency("0");
                        //    return;
                        //}

                        _pb_price = SSPriceBookPrice;

                        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);

                        if (_MasterProfitCenter.Mpc_edit_price)
                        {
                            if (_pb_price > _txtUprice)
                            {
                                decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                                if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                                {
                                    this.Cursor = Cursors.Default;
                                    //using (new CenterWinDialog(this))
                                    {
                                        MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
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
            //if (_IsVirtualItem)
            //{
            //    txtDisRate.Clear();
            //    txtDisAmt.Clear();
            //    txtDisAmt.Text = FormatToCurrency("0");
            //    txtDisRate.Text = FormatToCurrency("0");
            //    return;
            //}
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
                        MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Promotion voucher allow for only one(1) item!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Clear();
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                CheckNewDiscountRate(string.Empty);
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
            //if (_IsVirtualItem)
            //{
            //    txtDisRate.Clear();
            //    txtDisAmt.Clear();
            //    txtDisAmt.Text = FormatToCurrency("0");
            //    txtDisRate.Text = FormatToCurrency("0");
            //    return;
            //}
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
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select a item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            if (oMainDetailList == null || oMainDetailList.Count == 0)
            {
                MessageBox.Show("Please select confirmation details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbJobNumber.Text == "")
            {
                MessageBox.Show("Please select a job number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbJobNumber.Focus();
                return;
            }
            // if (rbnNewItem.Checked)

            if (_recieptItem != null)
                if (_recieptItem.Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            {
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

                        //    //decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * _tax, true);
                        //    decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price, true);
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

                MasterItem tempItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                editItem = new Service_Confirm_detail();
                if (IsEdit)
                {
                    if (oMainDetailList.Count > 0)
                    {
                        if (oMainDetailList.FindAll(x => x.Jcd_itmcd == txtItem.Text && x.Jcd_pblvl == cmbLevel.SelectedValue.ToString()
                                                 && x.Jcd_itmstus == cmbStatus.SelectedValue.ToString()
                                                 && x.Jcd_pb == cmbBook.SelectedValue.ToString()
                                                 && x.Jcd_dis_rt == Convert.ToDecimal(txtDisRate.Text)).Count > 0)
                        {
                            editItem = oMainDetailList.Find(x => x.Jcd_itmcd == txtItem.Text && x.Jcd_pblvl == cmbLevel.SelectedValue.ToString()
                                                 && x.Jcd_itmstus == cmbStatus.SelectedValue.ToString()
                                                 && x.Jcd_pb == cmbBook.SelectedValue.ToString()
                                                 && x.Jcd_dis_rt == Convert.ToDecimal(txtDisRate.Text));
                            editItem.Jcd_seq = 0;
                            editItem.Jcd_no = lblConfirmationNo.Text;
                            //editItem.Jcd_line = oMainDetailList.Count + 1;
                            editItem.Jcd_jobno = cmbJobNumber.Text;
                            //newItem.Jcd_joblineno             = "";
                            editItem.Jcd_itmcd = txtItem.Text;
                            editItem.Jcd_itmstus = cmbStatus.SelectedValue.ToString();
                            editItem.Jcd_qty = Convert.ToDecimal(txtQty.Text);
                            //newItem.Jcd_balqty                = "";
                            editItem.Jcd_pb = cmbBook.SelectedValue.ToString();
                            editItem.Jcd_pblvl = cmbLevel.SelectedValue.ToString();
                            editItem.Jcd_unitprice = Convert.ToDecimal(txtUnitPrice.Text);
                            editItem.Jcd_amt = Convert.ToDecimal(txtUnitAmt.Text);
                            //newItem.Jcd_tax_rt =
                            editItem.Jcd_tax = Convert.ToDecimal(txtTaxAmt.Text);
                            editItem.Jcd_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                            editItem.Jcd_dis = Convert.ToDecimal(txtDisAmt.Text);
                            editItem.Jcd_net_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                            editItem.Jcd_itmtp = lblItemType.Text;
                            //newItem.Jcd_foc = "";
                            editItem.Jcd_costelement = "";
                            editItem.Jcd_docno = "";
                            editItem.Jcd_rmk = "";
                            //newItem.Jcd_costsheetlineno = "";
                            //newItem.Jcd_jobitmcd = "";
                            //newItem.Jcd_jobitmser = "";
                            //newItem.Jcd_jobwarrno = "";
                            editItem.Jcd_pbprice = Convert.ToDecimal(SSPriceBookPrice);
                            editItem.Jcd_pbseqno = Convert.ToInt32(SSPriceBookSequance);
                            editItem.Jcd_pbitmseqno = Convert.ToInt32(SSPriceBookItemSequance);
                            editItem.Jcd_itmdesc = tempItem.Mi_longdesc;
                            editItem.Jcd_itmmodel = tempItem.Mi_model;
                            editItem.Jcd_itmbrand = tempItem.Mi_brand;
                            editItem.Jcd_itmuom = "";
                            //newItem.Jcd_mov_doc = tempItem.line
                            //newItem.Jcd_itmline = "";
                            //newItem.Jcd_batchline = "";
                            //newItem.Jcd_serline = "";
                            //newItem.Jcd_ser_id = "";
                            //newItem.Jcd_gatepass_raised = "";
                            //newItem.Jcd_invtype = "";
                            //newItem.Jcd_iswarr = "";
                            //newItem.Jcd_movedoctp = "";

                            //oItem.ESI_SEQ = "";
                            //oItem.ESI_ITM_SEQ = "";
                            //oItem.ESI_RMK = "";
                            //oItem.ESI_ISSUE_QTY = "";
                            //oItem.ESI_UNIT_COST = "";

                            // newItem.cost = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, oItem.ESI_ITM_STUS);

                            editItem.Jcd_itmcd_DESC = tempItem.Mi_longdesc;
                            editItem.IsNewRecord = "Y";

                            editItem.WarrantyRemark = WarrantyRemarks;
                            editItem.WarrantyRepirod = WarrantyPeriod;
                            editItem.IsPRint = true;
                            editItem.costPrice = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, cmbStatus.SelectedValue.ToString());

                            //oMainDetailList.Add(editItem);
                            BindAddItem();
                            ModifyGrid();
                            CalculateGrandTotal(editItem.Jcd_qty, editItem.Jcd_unitprice, editItem.Jcd_dis, editItem.Jcd_tax, true);
                            ClearMiddle1p0();
                            calculateCostAndRevenue();
                            calculateTotals();
                            return;
                        }
                    }
                }
                if (oMainDetailList.Count > 0)
                {
                    if (oMainDetailList.FindAll(x => x.Jcd_itmcd == txtItem.Text && x.Jcd_pblvl == cmbLevel.SelectedValue.ToString()
                                             && x.Jcd_itmstus == cmbStatus.SelectedValue.ToString()
                                             && x.Jcd_pb == cmbBook.SelectedValue.ToString()
                                             && x.Jcd_dis_rt == Convert.ToDecimal(txtDisRate.Text)).Count > 0)
                    {
                        editItem = oMainDetailList.Find(x => x.Jcd_itmcd == txtItem.Text && x.Jcd_pblvl == cmbLevel.SelectedValue.ToString()
                                      && x.Jcd_itmstus == cmbStatus.SelectedValue.ToString()
                                      && x.Jcd_pb == cmbBook.SelectedValue.ToString()
                                      && x.Jcd_dis_rt == Convert.ToDecimal(txtDisRate.Text));

                        editItem.Jcd_seq = 0;
                        editItem.Jcd_no = lblConfirmationNo.Text;
                        //editItem.Jcd_line = oMainDetailList.Count + 1;
                        editItem.Jcd_jobno = cmbJobNumber.Text;
                        //newItem.Jcd_joblineno             = "";
                        editItem.Jcd_itmcd = txtItem.Text;
                        editItem.Jcd_itmstus = cmbStatus.SelectedValue.ToString();
                        editItem.Jcd_qty += Convert.ToDecimal(txtQty.Text);
                        //newItem.Jcd_balqty                = "";
                        editItem.Jcd_pb = cmbBook.SelectedValue.ToString();
                        editItem.Jcd_pblvl = cmbLevel.SelectedValue.ToString();
                        editItem.Jcd_unitprice = Convert.ToDecimal(txtUnitPrice.Text);
                        editItem.Jcd_amt += Convert.ToDecimal(txtUnitAmt.Text);
                        //newItem.Jcd_tax_rt =
                        editItem.Jcd_tax = Convert.ToDecimal(txtTaxAmt.Text);
                        editItem.Jcd_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                        editItem.Jcd_dis = Convert.ToDecimal(txtDisAmt.Text);
                        editItem.Jcd_net_amt = editItem.Jcd_amt + (editItem.Jcd_tax * editItem.Jcd_qty);
                        editItem.Jcd_itmtp = lblItemType.Text;
                        //newItem.Jcd_foc = "";
                        editItem.Jcd_costelement = "";
                        editItem.Jcd_docno = "";
                        editItem.Jcd_rmk = "";
                        //newItem.Jcd_costsheetlineno = "";
                        //newItem.Jcd_jobitmcd = "";
                        //newItem.Jcd_jobitmser = "";
                        //newItem.Jcd_jobwarrno = "";
                        editItem.Jcd_pbprice = Convert.ToDecimal(SSPriceBookPrice);
                        editItem.Jcd_pbseqno = Convert.ToInt32(SSPriceBookSequance);
                        editItem.Jcd_pbitmseqno = Convert.ToInt32(SSPriceBookItemSequance);
                        editItem.Jcd_itmdesc = tempItem.Mi_longdesc;
                        editItem.Jcd_itmmodel = tempItem.Mi_model;
                        editItem.Jcd_itmbrand = tempItem.Mi_brand;
                        editItem.Jcd_itmuom = "";
                        //newItem.Jcd_mov_doc = tempItem.line
                        //newItem.Jcd_itmline = "";
                        //newItem.Jcd_batchline = "";
                        //newItem.Jcd_serline = "";
                        //newItem.Jcd_ser_id = "";
                        //newItem.Jcd_gatepass_raised = "";
                        //newItem.Jcd_invtype = "";
                        //newItem.Jcd_iswarr = "";
                        //newItem.Jcd_movedoctp = "";

                        //oItem.ESI_SEQ = "";
                        //oItem.ESI_ITM_SEQ = "";
                        //oItem.ESI_RMK = "";
                        //oItem.ESI_ISSUE_QTY = "";
                        //oItem.ESI_UNIT_COST = "";

                        // newItem.cost = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, oItem.ESI_ITM_STUS);

                        editItem.Jcd_itmcd_DESC = tempItem.Mi_longdesc;
                        editItem.IsNewRecord = "Y";

                        editItem.WarrantyRemark = WarrantyRemarks;
                        editItem.WarrantyRepirod = WarrantyPeriod;
                        editItem.IsPRint = true;
                        editItem.costPrice = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, cmbStatus.SelectedValue.ToString());

                        //oMainDetailList.Add(editItem);
                        BindAddItem();
                        ModifyGrid();
                        CalculateGrandTotal(editItem.Jcd_qty, editItem.Jcd_unitprice, editItem.Jcd_dis, editItem.Jcd_tax, true);
                        ClearMiddle1p0();
                        calculateCostAndRevenue();
                        calculateTotals();
                        return;
                    }
                }
                //MasterItem tempItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

                Service_Confirm_detail newItem = new Service_Confirm_detail();
                newItem.Jcd_seq = 0;
                newItem.Jcd_no = lblConfirmationNo.Text;
                newItem.Jcd_line = oMainDetailList.Count + 1;
                newItem.Jcd_jobno = cmbJobNumber.Text;
                //newItem.Jcd_joblineno             = "";
                newItem.Jcd_itmcd = txtItem.Text;
                newItem.Jcd_itmstus = cmbStatus.SelectedValue.ToString();
                newItem.Jcd_qty = Convert.ToDecimal(txtQty.Text);
                //newItem.Jcd_balqty                = "";
                newItem.Jcd_pb = cmbBook.SelectedValue.ToString();
                newItem.Jcd_pblvl = cmbLevel.SelectedValue.ToString();
                newItem.Jcd_unitprice = Convert.ToDecimal(txtUnitPrice.Text);
                newItem.Jcd_amt = Convert.ToDecimal(txtUnitAmt.Text);
                //newItem.Jcd_tax_rt =
                newItem.Jcd_tax = Convert.ToDecimal(txtTaxAmt.Text);
                newItem.Jcd_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                newItem.Jcd_dis = Convert.ToDecimal(txtDisAmt.Text);
                newItem.Jcd_net_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                newItem.Jcd_itmtp = lblItemType.Text;
                //newItem.Jcd_foc = "";
                newItem.Jcd_costelement = "";
                newItem.Jcd_docno = "";
                newItem.Jcd_rmk = "";
                //newItem.Jcd_costsheetlineno = "";
                //newItem.Jcd_jobitmcd = "";
                //newItem.Jcd_jobitmser = "";
                //newItem.Jcd_jobwarrno = "";
                newItem.Jcd_pbprice = Convert.ToDecimal(SSPriceBookPrice);
                newItem.Jcd_pbseqno = Convert.ToInt32(SSPriceBookSequance);
                newItem.Jcd_pbitmseqno = Convert.ToInt32(SSPriceBookItemSequance);
                newItem.Jcd_itmdesc = tempItem.Mi_longdesc;
                newItem.Jcd_itmmodel = tempItem.Mi_model;
                newItem.Jcd_itmbrand = tempItem.Mi_brand;
                newItem.Jcd_itmuom = "";
                //newItem.Jcd_mov_doc = tempItem.line
                //newItem.Jcd_itmline = "";
                //newItem.Jcd_batchline = "";
                //newItem.Jcd_serline = "";
                //newItem.Jcd_ser_id = "";
                //newItem.Jcd_gatepass_raised = "";
                //newItem.Jcd_invtype = "";
                //newItem.Jcd_iswarr = "";
                //newItem.Jcd_movedoctp = "";

                newItem.Jcd_ser_id = SelectedSerialID;

                //oItem.ESI_SEQ = "";
                //oItem.ESI_ITM_SEQ = "";
                //oItem.ESI_RMK = "";
                //oItem.ESI_ISSUE_QTY = "";
                //oItem.ESI_UNIT_COST = "";

                // newItem.cost = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, oItem.ESI_ITM_STUS);

                newItem.Jcd_itmcd_DESC = lblItemDescription.Text.Split(':')[1].Trim();
                newItem.IsNewRecord = "Y";

                newItem.WarrantyRemark = WarrantyRemarks;
                newItem.WarrantyRepirod = WarrantyPeriod;
                newItem.IsPRint = true;

                newItem.costPrice = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, cmbStatus.SelectedValue.ToString());

                oMainDetailList.Add(newItem);
                BindAddItem();
                ModifyGrid();
                CalculateGrandTotal(newItem.Jcd_qty, newItem.Jcd_unitprice, newItem.Jcd_dis, newItem.Jcd_tax, true);
                ClearMiddle1p0();
                calculateCostAndRevenue();
                calculateTotals();
                SelectedSerialID = 0;

                if (MessageBox.Show("Do you want to add another item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtItem.Focus();
                }
                else
                {
                    //txtPrintContains.Focus();
                }
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
                if (cmbStatus.SelectedValue == null)
                {
                    MessageBox.Show("Please select a item status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                CheckLevelStatusWithInventoryStatus();
                lblcostPrice.Text = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, cmbStatus.SelectedValue.ToString()).ToString("N");
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
                LoadPriceLevel("CS", cmbBook.Text);
                LoadLevelStatus("CS", cmbBook.Text.Trim(), cmbLevel.Text.Trim());
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
                LoadLevelStatus("CS", cmbBook.Text.Trim(), cmbLevel.Text.Trim());
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
                    //if (rbnDeleteItem.Checked)
                    {
                        if (e.ColumnIndex == 0 && dgvEstimateItems[e.ColumnIndex, e.RowIndex].ReadOnly == false)
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

                            jobNo = dgvEstimateItems.SelectedRows[0].Cells["Jcd_jobno"].Value.ToString();
                            ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                            Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                            UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                            DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                            TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());
                            Int32 lineNum = Convert.ToInt32(dgvEstimateItems.SelectedRows[0].Cells["SeqNo"].Value.ToString());

                            string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                            string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                            string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                            if (oMainDetailList.FindAll(x => x.Jcd_itmcd == ItemCode
                                && x.Jcd_jobno == jobNo
                                && x.Jcd_qty == Qty
                                && x.Jcd_pblvl == Level
                                && x.Jcd_itmstus == Status
                                && x.Jcd_pb == book
                                && x.Jcd_line == lineNum).Count > 0)
                            {
                                oMainDetailList.RemoveAll(x => x.Jcd_itmcd == ItemCode
                                    && x.Jcd_jobno == jobNo
                                    && x.Jcd_qty == Qty
                                    && x.Jcd_pblvl == Level
                                    && x.Jcd_itmstus == Status
                                    && x.Jcd_pb == book
                                    && x.Jcd_line == lineNum);
                            }

                            CalculateGrandTotal(Qty, UnitPrice, DiscountAmount, TaxAmount, false);

                            BindAddItem();
                        }
                        else if (e.ColumnIndex == 0)
                        {
                            {
                                MessageBox.Show("Can’t delete the Job items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        if (e.ColumnIndex == 10 && _isEditPrice == false)
                        {
                            decimal _prevousDisRate = Convert.ToDecimal(dgvEstimateItems.Rows[e.RowIndex].Cells["DiscountRate"].Value);
                            int _lineno0 = Convert.ToInt32(dgvEstimateItems.Rows[e.RowIndex].Cells["SeqNo"].Value);
                            string _book = Convert.ToString(dgvEstimateItems.Rows[e.RowIndex].Cells["Book"].Value);
                            string _level = Convert.ToString(dgvEstimateItems.Rows[e.RowIndex].Cells["Level"].Value);
                            string _item = Convert.ToString(dgvEstimateItems.Rows[e.RowIndex].Cells["Item"].Value);
                            string _status = Convert.ToString(dgvEstimateItems.Rows[e.RowIndex].Cells["Status"].Value);
                            bool _isSerialized = false;

                            //add by akila - 2017/09/06
                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _book, _level);
                        xy:
                            string _userDisRate = Microsoft.VisualBasic.Interaction.InputBox("Enter the discount rate", "Discount", Convert.ToString(_prevousDisRate), this.Width / 2, this.Height / 2);
                            if (string.IsNullOrEmpty(_userDisRate))
                                return;
                            if (IsNumeric(_userDisRate) == false || Convert.ToDecimal(_userDisRate) > 100 || Convert.ToDecimal(_userDisRate) < 0)
                            {
                                MessageBox.Show("Please select the valid discount rate", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                goto xy;
                            }
                            decimal _disRate = Convert.ToDecimal(_userDisRate);

                            if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpDate.Value.Date, _book, _level, txtBillingCustCode.Text.Trim(), _item, _isSerialized, false);
                            if (GeneralDiscount != null)
                            {
                                decimal vals = GeneralDiscount.Sgdd_disc_val;
                                decimal rates = GeneralDiscount.Sgdd_disc_rt;

                                if (rates < _disRate)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("You can not discount price more than " + rates + "%.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            var _itm = oMainDetailList.Where(x => x.Jcd_line == _lineno0).ToList();

                            //var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
                            if (_item != null && _item.Count() > 0)
                                foreach (Service_Confirm_detail _one in _itm)
                                {
                                    CalculateGrandTotal(_one.Jcd_qty, _one.Jcd_unitprice, _one.Jcd_dis, _one.Jcd_tax, false);
                                    decimal _unitRate = _one.Jcd_unitprice;
                                    decimal _unitAmt = _one.Jcd_amt;
                                    decimal _disVal = 0;
                                    decimal _vatPortion = 0;
                                    decimal _lineamount = 0;
                                    decimal _newvatval = 0;

                                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                                    //updated by akila 2017/09/06 - remove round from tax
                                    if (_isTaxDiscount)
                                    {
                                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Jcd_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, false), false);
                                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, false);
                                        _lineamount = FigureRoundUp(_unitRate * _one.Jcd_qty + _vatPortion - _disVal, true);
                                    }
                                    else
                                    {
                                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Jcd_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), false);
                                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, false);

                                        if (_disRate > 0)
                                        {
                                            //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                                            //if (_tax != null && _tax.Count > 0)
                                            //{
                                            //    _newvatval = ((_unitRate * _one.Jcd_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                            //}
                                            _newvatval = FigureRoundUp(TaxCalculation(_item, _status, _one.Jcd_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, false), false);
                                        }

                                        if (_disRate > 0)
                                        {
                                            _lineamount = FigureRoundUp(_unitRate * _one.Jcd_qty + _vatPortion - _disVal, true);
                                            _vatPortion = FigureRoundUp(_newvatval, false);
                                        }
                                        else
                                        {
                                            _lineamount = FigureRoundUp(_unitRate * _one.Jcd_qty + _vatPortion - _disVal, true);
                                        }
                                    }
                                    
                                    oMainDetailList.Where(x => x.Jcd_line == _lineno0).ToList().ForEach(x => x.Jcd_dis_rt = _disRate);
                                    oMainDetailList.Where(x => x.Jcd_line == _lineno0).ToList().ForEach(x => x.Jcd_dis = _disVal);
                                    oMainDetailList.Where(x => x.Jcd_line == _lineno0).ToList().ForEach(x => x.Jcd_tax = _vatPortion);
                                    oMainDetailList.Where(x => x.Jcd_line == _lineno0).ToList().ForEach(x => x.Jcd_net_amt = FigureRoundUp(_lineamount, true));
                                    BindAddItem();
                                    CalculateGrandTotal(_one.Jcd_qty, _unitRate, _disVal, _vatPortion, true);
                                    decimal _tobepays = 0;
                                    if (lblSVatStatus.Text == "Available")
                                    {
                                        _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                                    }
                                    else
                                    {
                                        _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                                    }
                                    ucPayModes1.TotalAmount = _tobepays;
                                    ucPayModes1.InvoiceItemList = ConvertConfirmItmList(oMainDetailList);
                                    //ucPayModes1.SerialList = InvoiceSerialList;
                                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                                    //{
                                    //    ucPayModes1.LoadData();
                                    //}
                                }
                        }

                        if (e.ColumnIndex == 2)
                        {
                            if (dgvEstimateItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && Convert.ToBoolean(dgvEstimateItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == false)
                            {
                                dgvEstimateItems.Rows[e.RowIndex].Cells["PrintCode"].Value = "";
                            }
                            else
                            {
                                dgvEstimateItems.Rows[e.RowIndex].Cells["PrintCode"].Value = "";
                            }
                        }
                    }
                    //else if (rbnEditItem.Checked)
                    //{
                    //    if (dgvEstimateItems[0, e.RowIndex].ReadOnly == false)
                    //    {
                    //        if (MessageBox.Show("Do you want to edit this item?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //        {
                    //            return;
                    //        }
                    //        string jobNo = string.Empty;
                    //        string ItemCode = string.Empty;
                    //        decimal Qty = 0;
                    //        decimal UnitPrice = 0;
                    //        decimal DiscountAmount = 0;
                    //        decimal TaxAmount = 0;

                    //        jobNo = dgvEstimateItems.SelectedRows[0].Cells["Jcd_jobno"].Value.ToString();
                    //        ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                    //        Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                    //        UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                    //        DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                    //        TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());

                    //        string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                    //        string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                    //        string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                    //        if (oMainDetailList.FindAll(x => x.Jcd_itmcd == ItemCode
                    //            && x.Jcd_jobno == jobNo
                    //            && x.Jcd_qty == Qty
                    //            && x.Jcd_pblvl == Level
                    //            && x.Jcd_itmstus == Status
                    //            && x.Jcd_pb == book).Count > 0)
                    //        {
                    //            txtItem.Text = ItemCode;
                    //            cmbBook.Text = book;
                    //            cmbLevel.Text = Level;
                    //            cmbStatus.Text = Status;
                    //            txtQty.Text = Qty.ToString();
                    //            txtUnitPrice.Text = UnitPrice.ToString();
                    //            txtUnitAmt.Text = (UnitPrice * Qty).ToString();

                    //            editItem = oMainDetailList.Find(x => x.Jcd_itmcd == ItemCode
                    //                && x.Jcd_jobno == jobNo
                    //                && x.Jcd_qty == Qty
                    //                && x.Jcd_pblvl == Level
                    //                && x.Jcd_itmstus == Status
                    //                && x.Jcd_pb == book);

                    //            txtDisRate.Text = editItem.Jcd_dis_rt.ToString();
                    //            txtDisAmt.Text = editItem.Jcd_dis.ToString();
                    //            txtTaxAmt.Text = editItem.Jcd_tax.ToString();
                    //            txtLineTotAmt.Text = editItem.Jcd_net_amt.ToString();
                    //            cmbJobNumber.Text = editItem.Jcd_jobno;
                    //        }

                    //        CalculateGrandTotal(Qty, UnitPrice, DiscountAmount, TaxAmount, false);

                    //        BindAddItem();
                    //    }
                    //}
                    // else if (rbnSetUpmarge.Checked)
                    //else
                    if (chkMargeUtem.Checked)
                    {
                        //TextBox gridText = new TextBox();

                        //try
                        //{
                        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        //    _CommonSearch.ReturnIndex = 0;
                        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                        //    DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                        //    _CommonSearch.dvResult.DataSource = _result;
                        //    _CommonSearch.BindUCtrlDDLData(_result);
                        //    _CommonSearch.obj_TragetTextBox = gridText;
                        //    _CommonSearch.ShowDialog();

                        //    string jobNo = string.Empty;
                        //    string ItemCode = string.Empty;
                        //    decimal Qty = 0;
                        //    decimal UnitPrice = 0;
                        //    decimal DiscountAmount = 0;
                        //    decimal TaxAmount = 0;

                        //    jobNo = dgvEstimateItems.SelectedRows[0].Cells["Jcd_jobno"].Value.ToString();
                        //    ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                        //    Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                        //    UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                        //    DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                        //    TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());

                        //    string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                        //    string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                        //    string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                        //    if (oMainDetailList.FindAll(x => x.Jcd_itmcd == ItemCode
                        //        && x.Jcd_jobno == jobNo
                        //        && x.Jcd_qty == Qty
                        //        && x.Jcd_pblvl == Level
                        //        && x.Jcd_itmstus == Status
                        //        && x.Jcd_pb == book).Count > 0)
                        //    {
                        //        Service_Confirm_detail tempItem = oMainDetailList.Find(x => x.Jcd_itmcd == ItemCode
                        //             && x.Jcd_jobno == jobNo
                        //             && x.Jcd_qty == Qty
                        //             && x.Jcd_pblvl == Level
                        //             && x.Jcd_itmstus == Status
                        //             && x.Jcd_pb == book);

                        //        tempItem.PrintCode = gridText.Text;
                        //        BindAddItem();
                        //        chkMargeUtem.Checked = false;
                        //    }
                        //}
                        //catch (Exception err)
                        //{
                        //    Cursor.Current = Cursors.Default;
                        //    CHNLSVC.CloseChannel();
                        //    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
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
                    Service_Confirm_detail oItem = oMainDetailList.Find(x => x.Jcd_itmcd == dgvEstimateItems.Rows[e.RowIndex].Cells["Item"].Value.ToString());
                    oItem.Jcd_itmcd_DESC = dgvEstimateItems.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                    if (dgvEstimateItems.Rows[e.RowIndex].Cells["IsPRintNew"].Value != null && Convert.ToBoolean(dgvEstimateItems.Rows[e.RowIndex].Cells["IsPRintNew"].Value.ToString()))
                    {
                        oItem.IsPRint = true;
                    }
                    else
                    {
                        oItem.IsPRint = false;
                    }
                }
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

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(txtJobNo.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void dgvEstimateItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEstimateItems.IsCurrentCellDirty)
            {
                dgvEstimateItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        private void lblName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
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

        private void txtRequest_Leave(object sender, EventArgs e)
        {
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

        private void txtJobNo_DoubleClick_1(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch);
            //DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtJobNo;
            //this.Cursor = Cursors.Default;
            //_CommonSearch.IsSearchEnter = true;
            //txtJobNo.Focus();
            //_CommonSearch.ShowDialog();

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
        }

        private void txtJobNo_Leave_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);
                if (oJOB_HDR == null || oJOB_HDR.SJB_JOBNO == null)
                {
                    MessageBox.Show("Please select a correct job number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Clear();
                    txtJobNo.Focus();
                    return;
                }
            }
        }

        private void txtJobNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txtJobNo.Text))
                {
                    btnSearchHeader_Click(null, null);
                }
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
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                return;
            }
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomer.Text))
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, BaseCls.GlbUserComCode);

            if (_masterBusinessCompany.Mbe_cd != null)
            {
                if (_masterBusinessCompany.Mbe_act == false)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Focus();
                    txtCustomer.Clear();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid Customer Code.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomer.Focus();
                txtCustomer.Clear();
            }
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

        private void btnSearchHeader_Click(object sender, EventArgs e)
        {
            if (dtpFromD.Value > dtpToD.Value)
            {
                MessageBox.Show("Please enter a valid date range", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime dtFrom;
            DateTime dtTo;
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                dtFrom = DateTime.MinValue;
                dtTo = DateTime.MaxValue;
            }
            else
            {
                dtFrom = dtpFromD.Value;
                dtTo = dtpToD.Value;
            }
            List<Service_confirm_Header> oHeaders = CHNLSVC.CustService.GetServiceConfirmHeader(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, txtJobNo.Text, txtRequest.Text, txtCustomer.Text, "", dtFrom, dtTo);
            dgvJobConf.DataSource = new List<Service_confirm_Header>();
            List<Service_confirm_Header> oHeadersNew = oHeaders.OrderBy(o => o.Jch_dt).ToList();
            if (!string.IsNullOrEmpty(txtcustcd.Text))
            {
                if (oHeaders.Count > 0)
                {
                    oHeaders = oHeaders.Where(r => r.Jch_cust_cd == txtcustcd.Text).ToList();
                }
            }
            if (oHeaders != null && oHeaders.Count > 0)
            {
                dgvJobConf.DataSource = oHeaders;
                ClearJobSearch();
                BindAddItem();
                ModifyGrid();
                CalculateGrandTotal(editItem.Jcd_qty, editItem.Jcd_unitprice, editItem.Jcd_dis, editItem.Jcd_tax, true);
                ClearMiddle1p0();
                calculateCostAndRevenue();
                calculateTotals();
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlDeliveryDetails.Visible = false;
        }

         private void dgvJobConf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (_MasterProfitCenter.Mpc_issp_tax == true)
                {
                    List<MasterPCTax> _masterPCTax = new List<MasterPCTax>();
                    _masterPCTax = CHNLSVC.Sales.GetPcTax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, 1, dtpDate.Value.Date);

                    if (_masterPCTax == null || _masterPCTax.Count <= 0)
                    {
                        MessageBox.Show("Profit center base taxes not setup.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                txtRemarks.Text = "";
                Int32 seq = Convert.ToInt32(dgvJobConf.Rows[e.RowIndex].Cells["Jch_seq"].Value.ToString());
                String ConfirmNum = dgvJobConf.Rows[e.RowIndex].Cells["Jch_no"].Value.ToString();
                DateTime _confDt = Convert.ToDateTime(dgvJobConf.Rows[e.RowIndex].Cells["Jch_dt"].Value.ToString());
                //Comment by Wimal to get multiple jobs for 1 invoice @ 16/07/2018
                //oMainDetailList = new List<Service_Confirm_detail>(); 

                SetReadOnlyValues();

                if (dgvJobConf.Rows[e.RowIndex].Cells["select"].Value != null && Convert.ToBoolean(dgvJobConf.Rows[e.RowIndex].Cells["select"].Value) == true)
                {
                    dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;
                    List<Service_Confirm_detail> tempList = CHNLSVC.CustService.GetServiceConfirmDetials(BaseCls.GlbUserComCode, seq, ConfirmNum);
                    
                    foreach (Service_Confirm_detail item in tempList)
                    {
                        oMainDetailList.RemoveAll(x => x.Jcd_seq == item.Jcd_seq);                        
                    }

                    if (oMainDetailList.Count == 0) 
                    { 
                    txtBillingCustCode.Clear();
                    txtBillingCustName.Clear();
                    txtInvoiceType.Clear();
                    }
                    ClearMiddle1p0();
                    calculateCostAndRevenue();
                    calculateTotals();
                    ModifyGrid();
                    bindDetails();
                    return;
                }

                string custCode = dgvJobConf.Rows[e.RowIndex].Cells["JCH_CUST_CD"].Value.ToString();
                string custName = dgvJobConf.Rows[e.RowIndex].Cells["JCH_CUST_NAME"].Value.ToString();
                string InvoiceType = string.Empty;
                if (dgvJobConf.Rows[e.RowIndex].Cells["JCH_INVTP"].Value != null)
                {
                    InvoiceType = dgvJobConf.Rows[e.RowIndex].Cells["JCH_INVTP"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Please setup invoice type in the confirmation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                 // dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;
                //return;
                //Added by Wimal @ 17/07/2018 check stock item job exist              
                if (oMainDetailList != null && oMainDetailList.Count > 0)
                {
                    var addjobnos = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList();
                    foreach (string jobno in addjobnos)
                    {
                        List<Service_job_Det> jobDets = CHNLSVC.CustService.GetJobDetails(jobno, -777, BaseCls.GlbUserComCode);
                        //List<ServiceJobDetail> jobDets = CHNLSVC.Sales.Get_Sev_JobDet(JobGrd);
                        foreach (var jobdtl in jobDets)
                        {
                            if (jobdtl.Jbd_isstockupdate == 1)
                            {
                                MessageBox.Show("Already Stock item job selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false; 
                                dgvJobConf.EndEdit();
                                return;
                            }
                        }
                    }
                }

                //Added by Wimal @ 17/07/2018 to check stock item job selected to add
                string JobGrd = dgvJobConf.Rows[e.RowIndex].Cells["JCH_JOBNO"].Value.ToString();
                if (oMainDetailList != null && oMainDetailList.Count > 0)
                {
                    List<Service_job_Det> jobDets = CHNLSVC.CustService.GetJobDetails(JobGrd, -777, BaseCls.GlbUserComCode);
                    //List<ServiceJobDetail> jobDets = CHNLSVC.Sales.Get_Sev_JobDet(JobGrd);
                    foreach (var jobdtl in jobDets)
                    {
                        if (jobdtl.Jbd_isstockupdate == 1)
                        {
                            MessageBox.Show("Multiple jobs can't be select for Stock items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;
                            dgvJobConf.EndEdit();
                            return;
                        }
                    }
                }
                

                if (!string.IsNullOrEmpty(txtBillingCustCode.Text) && custCode != txtBillingCustCode.Text)
                {
                                     
                   MessageBox.Show("Please select a same customer's confirmation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);//remove comment by Wimal On 12/07/2018 to add multiple jobs
                   dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;                
                   dgvJobConf.EndEdit();
                    return;
                }

                if (!string.IsNullOrEmpty(txtInvoiceType.Text) && InvoiceType != txtInvoiceType.Text)
                {
                   
                    MessageBox.Show("Please select a same customer's confirmation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); //remove comment by Wimal On 12/07/2018 to add multiple jobs
                    dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;                  
                    dgvJobConf.EndEdit();
                    return;
                }

                dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = true;

                txtBillingCustCode.Text = custCode;
                txtBillingCustName.Text = custName;
                txtInvoiceType.Text = InvoiceType;

                loadDeliveryCustomer(JobGrd);

                string jobNum1 = string.Empty;

                if (oMainDetailList == null)
                {
                    oMainDetailList = new List<Service_Confirm_detail>();
                }               

                if (txtInvoiceType.Text == "" || txtInvoiceType.Text != "CS")
                {
                    List<Service_Confirm_detail> tempList = CHNLSVC.CustService.GetServiceConfirmDetials(BaseCls.GlbUserComCode, seq, ConfirmNum);
                    if (oMainDetailList.FindAll(x => x.Jcd_seq == seq && x.Jcd_no == ConfirmNum).Count == 0)
                    {
                        foreach (Service_Confirm_detail item in tempList)
                        {
                            item.IsNewRecord = "N";

                            decimal taxvalue = 0;
                            decimal linetotal = 0;

                            //Commented by akila 2017/06/16
                            //CalculateTaxForConfirmItems(item.Jcd_itmcd, item.Jcd_qty, item.Jcd_unitprice, item.Jcd_itmstus, item.Jcd_dis_rt, item.Jcd_dis, out taxvalue, out linetotal);
                            //item.Jcd_tax = taxvalue;
                            //item.Jcd_net_amt = linetotal;
                        }
                        
                        oMainDetailList.AddRange(tempList);
                        oMainDetailList.ForEach(x => x.IsPRint = true);
                        bindDetails();
                        jobNum1 = oMainDetailList[0].Jcd_jobno;
                        txtRemarks.Text = dgvJobConf.Rows[e.RowIndex].Cells["jch_rmk"].Value.ToString();

                    }
                }
                else
                {
                    if (oMainDetailList != null && oMainDetailList.Count > 0)
                    {
                        string jobNum = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList()[0];
                        if (jobNum == JobGrd)
                        {
                            List<Service_Confirm_detail> tempList = CHNLSVC.CustService.GetServiceConfirmDetials(BaseCls.GlbUserComCode, seq, ConfirmNum);
                            if (oMainDetailList.FindAll(x => x.Jcd_seq == seq && x.Jcd_no == ConfirmNum).Count == 0)
                            {
                                foreach (Service_Confirm_detail item in tempList)
                                {
                                    item.IsNewRecord = "N";

                                    decimal taxvalue = 0;
                                    decimal linetotal = 0;

                                    //Commented by akila 2017/06/16
                                    //CalculateTaxForConfirmItems(item.Jcd_itmcd, item.Jcd_qty, item.Jcd_unitprice, item.Jcd_itmstus, item.Jcd_dis_rt, item.Jcd_dis, out taxvalue, out linetotal);
                                    //item.Jcd_tax = taxvalue;
                                    //item.Jcd_net_amt = linetotal;
                                }
                                oMainDetailList.AddRange(tempList);
                                oMainDetailList.ForEach(x => x.IsPRint = true);
                                bindDetails();
                                jobNum1 = oMainDetailList[0].Jcd_jobno;
                                txtRemarks.Text = dgvJobConf.Rows[e.RowIndex].Cells["jch_rmk"].Value.ToString();
                            }
                        }
                        else
                        {
                            dgvJobConf.Rows[e.RowIndex].Cells["select"].Value = false;
                        }
                    }
                    else
                    {
                        int _countConf = 0;
                        if (oMainDetailList != null && oMainDetailList.Count > 0)
                        {
                            _countConf = oMainDetailList.FindAll(x => x.Jcd_seq == seq && x.Jcd_no == ConfirmNum).Count;
                        }

                        List<Service_Confirm_detail> tempList = CHNLSVC.CustService.GetServiceConfirmDetials(BaseCls.GlbUserComCode, seq, ConfirmNum);
                        if (_countConf == 0)
                        {
                            {
                                oMainDetailList = new List<Service_Confirm_detail>();
                                foreach (Service_Confirm_detail item in tempList)
                                {
                                    item.IsNewRecord = "N";

                                    decimal taxvalue = 0;
                                    decimal linetotal = 0;
                                    CalculateTaxForConfirmItems(item.Jcd_itmcd, item.Jcd_qty, item.Jcd_unitprice, item.Jcd_itmstus, item.Jcd_dis_rt, item.Jcd_dis, out taxvalue, out linetotal);
                                    item.Jcd_tax = taxvalue;
                                    item.Jcd_net_amt = linetotal;
                                }
                                oMainDetailList.AddRange(tempList);
                                oMainDetailList.ForEach(x => x.IsPRint = true);
                                bindDetails();
                                if (oMainDetailList.Count == 0) return;
                                jobNum1 = oMainDetailList[0].Jcd_jobno;
                                txtRemarks.Text = dgvJobConf.Rows[e.RowIndex].Cells["jch_rmk"].Value.ToString();
                            }
                        }
                    }
                }

                List<Service_JOB_HDR> oheaderList = CHNLSVC.CustService.GetServiceJobHeaderAll(jobNum1, BaseCls.GlbUserComCode);
                if (oheaderList != null && oheaderList.Count > 0)
                {
                    jobCategory = oheaderList[0].SJB_MOD_BY;
                }
                if (oheaderList != null && oheaderList.Count > 0 && oheaderList[0].SJB_MOD_BY == "F")
                {
                    chkDeliverLater.Checked = false;
                    chkDeliverLater.Enabled = false;
                }
                else
                {
                    chkDeliverLater.Enabled = true;
                }

                //kapila 29/7/2015
                DataTable _dtJobHdr = CHNLSVC.CustService.getServicejobDet(JobGrd, 0);
                if (_dtJobHdr.Rows.Count > 0)
                {
                    _isStockUpdate = Convert.ToBoolean(_dtJobHdr.Rows[0]["jbd_isstockupdate"]);
                    txtIssueLoc.Text = _dtJobHdr.Rows[0]["jbd_aodissueloc"].ToString();
                }
                else
                {
                    _isStockUpdate = false;
                    txtIssueLoc.Text = "";
                }


                ClearMiddle1p0();
                calculateCostAndRevenue();
                calculateTotals();
                ModifyGrid();

                if (Convert.ToDecimal(lblGrndTotalAmount.Text) > 0)
                {
                    if (_confDt.Date < Convert.ToDateTime("24-Jan-2016"))
                    {
                        MessageBox.Show("Due to Tax change cannot invoice this job confirmation. Pls re-process confirmation.", "Warning", MessageBoxButtons.OK);
                        btnSave.Enabled = false;
                        return;
                    }
                }
                
            }
        }

        private void lblGrndTotalAmount_TextChanged(object sender, EventArgs e)
        {
            //ucPayModes1.PaidAmountLabel.Text = Convert.ToDecimal(lblGrndTotalAmount.Text).ToString("n");
            //ucPayModes1.balana.Text = Convert.ToDecimal(lblGrndTotalAmount.Text).ToString("n");
            ucPayModes1.InvoiceItemList = ConvertConfirmItmList(oMainDetailList);
            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text);
            ucPayModes1.Amount.Text = Convert.ToString(ucPayModes1.TotalAmount - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text));
            ucPayModes1.IsZeroAllow = true;
            ucPayModes1.Customer_Code = txtBillingCustCode.Text.Trim();
            ucPayModes1.LoadData();
        }

        private void txtInvoiceType_TextChanged(object sender, EventArgs e)
        {
            ucPayModes1.InvoiceItemList = ConvertConfirmItmList(oMainDetailList);
            ucPayModes1.InvoiceType = txtInvoiceType.Text.Trim();
            ucPayModes1.Customer_Code = txtBillingCustCode.Text.Trim();
            ucPayModes1.LoadData();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            txtCustomerCodeDD.Text = txtBillingCustCode.Text;
            //txtCustomerCodeDD_Leave(null, null);
            //  pnlDeliveryDetails.Visible = true;
            pnlDeliveryInstruction.Visible = true;
        }

        private void btnSearchEstimate_Click(object sender, EventArgs e)
        {
        }

        private void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (chkManualRef.Checked == true)
                {
                    txtManualRefNo.Enabled = true;
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_INV");
                    if (_NextNo != 0)
                        txtManualRefNo.Text = _NextNo.ToString();
                    else
                        txtManualRefNo.Text = "";
                }
                else
                {
                    txtManualRefNo.Text = string.Empty;
                    txtManualRefNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                txtManualRefNo.Clear();
                txtManualRefNo.Enabled = false;
                chkManualRef.Checked = false;
                this.Cursor = Cursors.Default;
                SystemErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _recieptItem = ucPayModes1.RecieptItemList;
                decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                if (_totlaPay == Convert.ToDecimal(lblGrndTotalAmount.Text))
                {
                    toolStrip1.Focus();
                    btnSave.Select();
                }
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

        private void txtCustomerCodeDD_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustomerCodeDD;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustomerCodeDD.Select();
            }
            catch (Exception ex) { txtCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtCustomerCodeDD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtCustomerCodeDD_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCustomerNameDD.Focus();
            }
        }

        private void txtCustomerCodeDD_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerCodeDD.Text))
            {
                return;
            }
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomerCodeDD.Text))
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomerCodeDD.Text, null, null, null, null, BaseCls.GlbUserComCode);

            if (_masterBusinessCompany.Mbe_cd != null)
            {
                if (_masterBusinessCompany.Mbe_act == false)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomerCodeDD.Focus();
                    txtCustomerCodeDD.Clear();
                    return;
                }
                else
                {
                    txtCustomerNameDD.Text = _masterBusinessCompany.Mbe_name;
                    txtAddressDD.Text = _masterBusinessCompany.Mbe_add1;
                    txtAddress2DD.Text = _masterBusinessCompany.Mbe_add2;
                    txtTeleDD.Text = _masterBusinessCompany.Mbe_tel;
                    txtRemarkDD.Focus();
                }
            }
            else
            {
                MessageBox.Show("Invalid Customer Code.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomerCodeDD.Focus();
                txtCustomerCodeDD.Clear();
            }
        }

        private void btnSearchCustDD_Click(object sender, EventArgs e)
        {
            txtCustomerCodeDD_DoubleClick(null, null);
        }

        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item.Trim(), _status.Trim(), Convert.ToDateTime(dtpDate.Text).Date);

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
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                                else { _isNoWarranty = true; }
                            }
                        }
                }
            return _isNoWarranty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlStandByItems.Visible = false;
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemCOde = string.Empty;
            string Status = string.Empty;
            string JobNum = string.Empty;

            itemCOde = dgvItems.Rows[e.RowIndex].Cells[2].Value.ToString();
            Status = dgvItems.Rows[e.RowIndex].Cells[3].Value.ToString();
            JobNum = dgvItems.Rows[e.RowIndex].Cells["STI_JOBNO"].Value.ToString();

            txtItem.Text = itemCOde;
            cmbStatus.SelectedText = Status;
            cmbJobNumber.SelectedItem = JobNum;
            txtItem.Focus();
            pnlStandByItems.Visible = false;

            if (dgvItems.Rows[e.RowIndex].Cells["sti_issueserid"].Value != null)
            {
                SelectedSerialID = Convert.ToInt32(dgvItems.Rows[e.RowIndex].Cells["sti_issueserid"].Value.ToString());
            }
        }

        private void dgvEstimateItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            if (dgvEstimateItems[0, e.RowIndex].ReadOnly == false)
            {
                if (MessageBox.Show("Do you want to edit this item?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                IsEdit = true;

                string jobNo = string.Empty;
                string ItemCode = string.Empty;
                decimal Qty = 0;
                decimal UnitPrice = 0;
                decimal DiscountAmount = 0;
                decimal TaxAmount = 0;

                jobNo = dgvEstimateItems.SelectedRows[0].Cells["Jcd_jobno"].Value.ToString();
                ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());

                string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                if (oMainDetailList.FindAll(x => x.Jcd_itmcd == ItemCode
                    && x.Jcd_jobno == jobNo
                    && x.Jcd_qty == Qty
                    && x.Jcd_pblvl == Level
                    && x.Jcd_itmstus == Status
                    && x.Jcd_pb == book).Count > 0)
                {
                    txtItem.Text = ItemCode;
                    cmbBook.Text = book;
                    cmbLevel.Text = Level;
                    cmbStatus.Text = Status;
                    txtQty.Text = Qty.ToString();
                    txtUnitPrice.Text = UnitPrice.ToString();
                    txtUnitAmt.Text = (UnitPrice * Qty).ToString();

                    editItem = oMainDetailList.Find(x => x.Jcd_itmcd == ItemCode
                        && x.Jcd_jobno == jobNo
                        && x.Jcd_qty == Qty
                        && x.Jcd_pblvl == Level
                        && x.Jcd_itmstus == Status
                        && x.Jcd_pb == book);

                    txtDisRate.Text = editItem.Jcd_dis_rt.ToString();
                    txtDisAmt.Text = editItem.Jcd_dis.ToString();
                    txtTaxAmt.Text = editItem.Jcd_tax.ToString();
                    txtLineTotAmt.Text = editItem.Jcd_net_amt.ToString();
                    cmbJobNumber.Text = editItem.Jcd_jobno;
                }

                CalculateGrandTotal(Qty, UnitPrice, DiscountAmount, TaxAmount, false);

                BindAddItem();
            }
            else if (e.ColumnIndex == 18)
            {
                if (chkMargeUtem.Checked)
                {
                    if (dgvEstimateItems.SelectedRows[e.RowIndex].Cells["PrintCode"].Value == null || dgvEstimateItems.SelectedRows[e.RowIndex].Cells["PrintCode"].Value.ToString() == string.Empty)
                    {
                        return;
                    }
                    if (MessageBox.Show("Do you want to clear the print code?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        dgvEstimateItems.SelectedRows[e.RowIndex].Cells["PrintCode"].Value = string.Empty;
                    }
                }
            }
        }

        private void btnSearchJobNum_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick_1(null, null);
        }

        private void btnSearchJobNum_Click_1(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick_1(null, null);
        }

        private void txtInvoiceNumber_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceInvoiceSearch);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceInvoice(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-1), dtpDate.Value.Date);
                _CommonSearch.dtpFrom.Value = dtpDate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = dtpDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNumber;
                //_commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoiceNumber.Select();
            }
            catch (Exception ex)
            { txtInvoiceNumber.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtInvoiceNumber_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
            }
        }

        private void txtInvoiceNumber_Leave(object sender, EventArgs e)
        {
            RecallInvoice();
        }

        private void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            txtInvoiceNumber_DoubleClick(null, null);
        }

        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            txtInvoiceType.Text = _hdr.Sah_inv_tp;
            dtpDate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy"); ;
            txtCustomer.Text = _hdr.Sah_cus_cd;
            // txtLoyalty.Text = _hdr.Sah_anal_6;
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            //  ViewCustomerAccountDetail(txtCustomer.Text);
            // txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(BaseCls.GlbUserComCode, _hdr.Sah_sales_ex_cd);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            //cmbExecutive.DataSource = null;
            //cmbExecutive.Items.Clear();
            //cmbExecutive.Items.Add(_name);
            //cmbExecutive.SelectedValue = _code;
            //lblCurrency.Text = _hdr.Sah_currency;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            //txtDocRefNo.Text = _hdr.Sah_ref_doc;
            //txtPoNo.Text = _hdr.Sah_anal_4;
            //txtRemarks.Text = _hdr.Sah_remarks;

            txtAddressDD.Text = _hdr.Sah_cus_add1;
            txtAddress2DD.Text = _hdr.Sah_cus_add2;
            txtBillingCustCode.Text = _hdr.Sah_cus_cd;
            txtBillingCustName.Text = _hdr.Sah_cus_name;
            txtAddressDD.Text = _hdr.Sah_d_cust_add1;
            txtAddress2DD.Text = _hdr.Sah_d_cust_add2;
            txtCustomerCodeDD.Text = _hdr.Sah_d_cust_cd;
            txtCustomerNameDD.Text = _hdr.Sah_d_cust_name;
            txtRemarks.Text = _hdr.Sah_remarks;
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustomerCodeDD.Text = _masterBusinessCompany.Mbe_cd;
            txtCustomerNameDD.Text = _masterBusinessCompany.Mbe_name;
            txtAddressDD.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2DD.Text = _masterBusinessCompany.Mbe_add2;
            txtTeleDD.Text = _masterBusinessCompany.Mbe_mob;

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

        }

        private void dgvEstimateItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (chkMargeUtem.Checked)
            {
                if (e.KeyCode == Keys.F2)
                {
                    TextBox gridText = new TextBox();
                    try
                    {
                        string oldText = string.Empty;
                        if (dgvEstimateItems.SelectedRows[0].Cells["PrintCode"].Value != null)
                        {
                            oldText = dgvEstimateItems.SelectedRows[0].Cells["PrintCode"].Value.ToString();
                        }

                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                        DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = gridText;
                        _CommonSearch.IsSearchEnter = true;
                        _CommonSearch.ShowDialog();

                        if (string.IsNullOrEmpty(gridText.Text))
                        {
                            return;
                        }
                        string jobNo = string.Empty;
                        string ItemCode = string.Empty;
                        decimal Qty = 0;
                        decimal UnitPrice = 0;
                        decimal DiscountAmount = 0;
                        decimal TaxAmount = 0;

                        jobNo = dgvEstimateItems.SelectedRows[0].Cells["Jcd_jobno"].Value.ToString();
                        ItemCode = dgvEstimateItems.SelectedRows[0].Cells["Item"].Value.ToString();
                        Qty = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["Qty"].Value.ToString());
                        UnitPrice = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["UnitPrice"].Value.ToString());
                        DiscountAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["DiscountAmount"].Value.ToString());
                        TaxAmount = Convert.ToDecimal(dgvEstimateItems.SelectedRows[0].Cells["TaxAmount"].Value.ToString());

                        string Level = dgvEstimateItems.SelectedRows[0].Cells["Level"].Value.ToString();
                        string Status = dgvEstimateItems.SelectedRows[0].Cells["Status"].Value.ToString();
                        string book = dgvEstimateItems.SelectedRows[0].Cells["Book"].Value.ToString();

                        if (oMainDetailList.FindAll(x => x.Jcd_itmcd == ItemCode
                            && x.Jcd_jobno == jobNo
                            && x.Jcd_qty == Qty
                            && x.Jcd_pblvl == Level
                            && x.Jcd_itmstus == Status
                            && x.Jcd_pb == book).Count > 0)
                        {
                            Service_Confirm_detail tempItem = oMainDetailList.Find(x => x.Jcd_itmcd == ItemCode
                                 && x.Jcd_jobno == jobNo
                                 && x.Jcd_qty == Qty
                                 && x.Jcd_pblvl == Level
                                 && x.Jcd_itmstus == Status
                                 && x.Jcd_pb == book);

                            tempItem.PrintCode = gridText.Text;
                            BindAddItem();
                            chkMargeUtem.Checked = false;
                        }
                    }
                    catch (Exception err)
                    {
                        Cursor.Current = Cursors.Default;
                        CHNLSVC.CloseChannel();
                        MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHide_Click_1(object sender, EventArgs e)
        {
            pnlDeliveryInstruction.Visible = false;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                {
                    dgvEstimateItems.Rows[i].Cells["IsPRintNew"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                {
                    dgvEstimateItems.Rows[i].Cells["IsPRintNew"].Value = false;
                }
            }
        }

        #region Panal Move

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlDeliveryDetails.Location.X - xDiff;
                int y = pnlDeliveryDetails.Location.Y - yDiff;
                pnlDeliveryDetails.Location = new Point(x, y);
            }
        }

        private void label7_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void label14_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label14_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlStandByItems.Location.X - xDiff;
                int y = pnlStandByItems.Location.Y - yDiff;
                pnlStandByItems.Location = new Point(x, y);
            }
        }

        private void label14_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        #endregion Panal Move

        #endregion events

        #region Methods

        private void GetJobDetails()
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                String stage = string.Empty;
                Int32 IsCusExpected = 0;

                stage = "2,3,5,3,6";

                DateTime from, to;

                from = Convert.ToDateTime("01-01-1111");
                to = Convert.ToDateTime("31-12-2999");

                DataTable DtDetails = new DataTable();
                DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);
                if (DtDetails.Rows.Count > 0)
                {
                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                    lblManualRefNo.Text = oJOB_HDR.SJB_MANUALREF;
                    lblReqNo.Text = oJOB_HDR.SJB_REQNO;
                    //lblStatus.Text = oJOB_HDR.SJB_STUS;

                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, oJOB_HDR.SJB_CUST_CD, string.Empty, string.Empty, "C");
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

                    //  loadDeliveryCustomer(oJOB_HDR);
                }
                else
                {
                    MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
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
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == "CS").Select(x => x.Sadd_pb).Distinct().ToList();
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
                            LoadPriceBook("CS");
                            LoadPriceLevel("CS", cmbBook.Text.Trim());
                            LoadLevelStatus("CS", cmbBook.Text.Trim(), cmbLevel.Text.Trim());
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
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == "CS" && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
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

                if (cmbStatus.SelectedValue == null)
                {
                    MessageBox.Show("please select a item status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
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

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            decimal _TaxAmt = 0;
            decimal _TotVal = 0;
            decimal _TotDis = 0;
            _TotVal = _pbUnitPrice * _qty;
            _TotDis = _TotVal * _disRate / 100;

            if (txtInvoiceType.Text.Trim() == "DEBT")
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

                            DateTime _serverDt = DateTime.Now.Date;
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
                                //_taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty); //Sanjeewa
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

                                        //updated by akila 2017/06/15
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

                                        //_discount = _TotVal * _disRate / 100;
                                        //_TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);


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
                                //if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dtpDate.Value.Date); else
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
                                    //if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dtpDate.Value.Date); else
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
                            if (_isTaxfaction) _pbUnitPrice = 0;
                    _pbUnitPrice = _TaxAmt;
                }
            }
            return _pbUnitPrice;
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
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
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

                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);


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

            if (!string.IsNullOrEmpty(_item))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    if (_itemdetail.Mi_itm_tp != "V")
                    {
                        _isValid = false;
                        MessageBox.Show("Main items can't add. Please select a virtual item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return _isValid;
                    }
                    else
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
                        return _isValid;
                    }
                }
                else
                {
                    _isValid = false;
                    MessageBox.Show("Invalid item Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return _isValid;
                }
            }
            else
            {
                _isValid = false;
                return _isValid;
            }
        }

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
                MessageBox.Show("Combine item codes can't add.", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;

                txtItem.Clear();
                txtItem.Focus();
                return _IsTerminate;
            }

            if (CheckQtyPriliminaryRequirements()) return true;

            CheckItemTax(txtItem.Text.Trim());

            btnAddItem.Enabled = true;

            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS", cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpDate.Text));
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
                        { MessageBox.Show("Price has been suspended. Please contact IT dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
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
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        txtUnitPrice.Text = _single.Sapd_itm_price.ToString("N2"); //FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
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

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS", _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpDate.Text));
                string _unitPrice = "";
                if (_priceDetailRef.Count <= 0)
                {
                    return false;
                }

                if (_priceDetailRef.Count <= 0)
                {
                    if (!_isCompleteCode)
                    {
                        return false;
                    }
                    else
                    {
                        _unitPrice = FormatToCurrency("0");
                    }
                }
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
            //if (string.IsNullOrEmpty(lblCustomerCode.Text))
            //{
            //    this.Cursor = Cursors.Default;
            //    //using (new CenterWinDialog(this))
            //    { MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //    _IsTerminate = true;
            //    lblCustomerCode.Focus();
            //    return _IsTerminate;
            //}
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

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
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
            dgvEstimateItems.AutoGenerateColumns = false;
            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            //for (int i = 0; i < oEstimate_Items.Count; i++)
            //{
            //    oEstimate_Items[i].ESI_LINE = i + 1;
            //}
            dgvEstimateItems.DataSource = oMainDetailList;

            for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
            {
                if (dgvEstimateItems.Rows[i].Cells["IsNewRecord"].Value.ToString() == "Y")
                {
                    dgvEstimateItems.Rows[i].Cells["Delete"].ReadOnly = false;
                }
                else
                {
                    dgvEstimateItems.Rows[i].Cells["Delete"].ReadOnly = true;
                }
            }
        }

        private void ModifyGrid()
        {
            if (dgvEstimateItems.Rows.Count > 0)
            {
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

                getJobNumbers();
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
            if (oMainDetailList != null)
            {
                if (oMainDetailList.Count > 0)
                { lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(oMainDetailList.Sum(x => x.Jcd_net_amt))); }
            }
            else { lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0")); }
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

                decimal _vatPortion = FigureRoundUp(TaxCalculation(ItemCode.Trim(), status.ToString().Trim(), Convert.ToDecimal(qty), _priceBookLevelRef, Convert.ToDecimal(UnitPrice.Trim()), Convert.ToDecimal(discountAmount.Trim()), Convert.ToDecimal(discountRate), true), true);
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
                if (oMainDetailList != null)
                {
                    if (oMainDetailList.Count > 0)
                    {
                        decimal costTOt = 0;
                        decimal revenueTOt = 0;
                        decimal margineTOt = 0;
                        decimal TaxTotal = 0;

                        foreach (Service_Confirm_detail item in oMainDetailList)
                        {
                            costTOt += item.Jcd_qty * item.Jcd_unitprice;
                            revenueTOt += item.Jcd_net_amt;
                        }

                        TaxTotal = oMainDetailList.Sum(x => x.Jcd_tax);

                        margineTOt = revenueTOt - TaxTotal - costTOt;

                        txtCostTotal.Text = costTOt.ToString("N");
                        txtCostTotal.Text = oMainDetailList.Sum(x => x.costPrice).ToString("N");
                        txtRevenueTotal.Text = (revenueTOt - TaxTotal).ToString("N");

                        txtMarginTotal.Text = (oMainDetailList.Sum(x => x.Jcd_net_amt) - oMainDetailList.Sum(x => x.costPrice)).ToString("N");

                        if (costTOt != 0)
                        {
                            decimal asd = (oMainDetailList.Sum(x => x.Jcd_net_amt) - oMainDetailList.Sum(x => x.costPrice));
                            // txtMarginPercentage.Text = FigureRoundUp(((margineTOt / costTOt) * 100), false).ToString("N");
                            txtMarginPercentage.Text = FigureRoundUp(((asd / costTOt) * 100), false).ToString("N");
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
                if (dgvEstimateItems.Rows[i].Cells["IsPRintNew"].Value != null && Convert.ToBoolean(dgvEstimateItems.Rows[i].Cells["IsPRintNew"].Value) == true)
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
                MessageBox.Show("Please select a item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return status;
        }

        private bool validateSave()
        {
            bool status = false;

            return status;
        }

        private void calculateTotals()
        {
            decimal subTotal = 0;
            decimal Discount = 0;
            decimal AfteDiscount = 0;
            decimal Tax = 0;
            decimal TotalAmount = 0;

            if (oMainDetailList != null && oMainDetailList.Count > 0)
            {
                foreach (Service_Confirm_detail item in oMainDetailList)
                {
                    subTotal += item.Jcd_qty * item.Jcd_amt;
                }


                Discount = oMainDetailList.Sum(x => x.Jcd_dis);
                AfteDiscount = (subTotal - Discount);
                Tax = oMainDetailList.Sum(x => x.Jcd_tax);

                TotalAmount = oMainDetailList.Sum(x => x.Jcd_net_amt);

                lblGrndSubTotal.Text = subTotal.ToString("N");
                lblGrndDiscount.Text = Discount.ToString("N");
                lblGrndAfterDiscount.Text = AfteDiscount.ToString("N");
                lblGrndTax.Text = Tax.ToString("N");
                lblGrndTotalAmount.Text = TotalAmount.ToString("N");
            }
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

        private void bindDetails()
        {
            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            dgvEstimateItems.DataSource = oMainDetailList;

            if (oMainDetailList != null && oMainDetailList.Count > 0)
            {
                for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
                {
                    if (dgvEstimateItems.Rows[i].Cells["IsNewRecord"].Value.ToString() == "Y")
                    {
                        dgvEstimateItems.Rows[i].Cells["Delete"].ReadOnly = false;
                    }
                    else
                    {
                        dgvEstimateItems.Rows[i].Cells["Delete"].ReadOnly = true;
                    }
                }
            }
        }

        private void getJobNumbers()
        {
            if (oMainDetailList != null && oMainDetailList.Count > 0)
            {
                if (oMainDetailList.Count > 0)
                {
                    List<String> jobNumberList = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList();
                    cmbJobNumber.DataSource = jobNumberList;
                }
            }
        }

        private void addStandByIssuedItems(List<Service_TempIssue> oTempIssueList)
        {
        }

        private void ShowStandByItems()
        {
            List<String> jobNumberList = oMainDetailList.Select(x => x.Jcd_jobno).Distinct().ToList();
            List<Service_TempIssue> oTempIssueList = new List<Service_TempIssue>();
            foreach (String item in jobNumberList)
            {
                oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, item, -999, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
            }
            dgvItems.DataSource = oTempIssueList;

            pnlStandByItems.Show();
        }

        private bool checkGatePass()
        {
            bool status = false;

            DataTable dtTemp = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtBillingCustCode.Text.Trim());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                if (jobCategory == "W" && dtTemp.Select("MBSA_SA_TP = 'CRED'").Length > 0)
                {
                    var jobLineList = oMainDetailList.GroupBy(x => new { x.Jcd_jobno, x.Jcd_joblineno }).Select(y => new { y.Key.Jcd_jobno, y.Key.Jcd_joblineno });

                    int gpCount = 0;
                    int TotalCount = 0;

                    foreach (var item in jobLineList)
                    {
                        TotalCount += 1;
                        List<Service_Gate_Pass_HDR> oGPHeader = CHNLSVC.CustService.SCV_CHEK_GP_FOR_JOBLINE(item.Jcd_jobno, item.Jcd_joblineno, BaseCls.GlbUserComCode);
                        if (oGPHeader != null && oGPHeader.Count > 0)
                        {
                            gpCount += 1;
                        }
                    }

                    if (gpCount == TotalCount)
                    {
                        status = true;
                    }
                    else if (gpCount == 0)
                    {
                        status = true;
                    }
                }
                else
                {
                    status = true;
                }
            }
            else
            {
                status = true;
            }

            return status;
        }

        private void loadDeliveryCustomer(String Jobnum)
        {
            Service_JOB_HDR oHdr = CHNLSVC.CustService.GetServiceJobHeader(Jobnum, BaseCls.GlbUserComCode);
            txtCustomerCodeDD.Text = txtBillingCustCode.Text;
            txtCustomerNameDD.Text = oHdr.SJB_B_CUST_NAME;
            txtAddressDD.Text = oHdr.SJB_B_ADD1;
            txtAddress2DD.Text = oHdr.SJB_B_ADD2;
            txtTeleDD.Text = oHdr.SJB_B_MOBINO;

            _masterBusinessCompany = new MasterBusinessEntity();
            //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, oHdr.SJB_CUST_CD, string.Empty, string.Empty, "C");
            _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtBillingCustCode.Text, null, null, null, null, BaseCls.GlbUserComCode);
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
        }

        private void RecallInvoice()
        {
            if (string.IsNullOrEmpty(txtInvoiceNumber.Text)) return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNumber.Text);
            if (_hdr == null)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Please select the valid invoice", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtInvoiceNumber.Text = string.Empty; return;
            }
            //Add by Chamal 20-07-2014
            if (_hdr.Sah_pc != BaseCls.GlbUserDefProf.ToString())
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Please select the valid invoice", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtInvoiceNumber.Text = string.Empty; return;
            }

            //Add by Chamal 25-08-2014
            if (_hdr.Sah_tp != "INV")
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Please select the valid invoice", "Invalid Invoice Category", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtInvoiceNumber.Text = string.Empty;
                return;
            }
            if (_hdr.Sah_inv_tp == "CS")
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else if (_hdr.Sah_inv_tp == "CRED" || _hdr.Sah_inv_tp == "DEBT")
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;

                //MessageBox.Show("Please select the valid invoice", "Invalid Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //txtInvoiceNumber.Text = string.Empty; return;
            }

            this.Cursor = Cursors.Default;

            AssignInvoiceHeaderDetail(_hdr);
            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNumber.Text.Trim());
            _invoiceItemList = _list;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            ScanSerialList = new List<ReptPickSerials>();

            setInvoiceItems(_list);

            //List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
            //ucPayModes1.RecieptItemList = _itms;
            //_recieptItem = _itms;
            //ucPayModes1.LoadRecieptGrid();

            //ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            //ucPayModes1.LoadData();
            //if (_hdr.Sah_stus != "H")
            //{
            //    btnSave.Enabled = false;
            //    txtItem.Enabled = false;
            //    txtSerialNo.Enabled = false;
            //    btnAddItem.Enabled = false;
            //}
            //else
            //{
            //    btnSave.Enabled = true;
            //    txtItem.Enabled = true;
            //    txtSerialNo.Enabled = true;
            //    btnAddItem.Enabled = true;
            //}
        }

        private void setInvoiceItems(List<InvoiceItem> oInvoiceItemList)
        {
            oMainDetailList = new List<Service_Confirm_detail>();
            foreach (InvoiceItem _tempItem in oInvoiceItemList)
            {
                Service_Confirm_detail item = new Service_Confirm_detail();
                item.Jcd_dis = _tempItem.Sad_disc_amt;
                item.Jcd_dis_rt = _tempItem.Sad_disc_rt;
                item.Jcd_qty = _tempItem.Sad_do_qty;
                item.Jcd_itmcd = _tempItem.Sad_itm_cd;
                item.Jcd_pbitmseqno = _tempItem.Sad_itm_seq;
                item.Jcd_itmstus = _tempItem.Sad_itm_stus;
                item.Jcd_tax = _tempItem.Sad_itm_tax_amt;
                item.Jcd_jobno = _tempItem.Sad_job_no;
                item.Jcd_no = _tempItem.Sad_res_no;
                item.Jcd_pblvl = _tempItem.Sad_pb_lvl;
                item.Jcd_pbprice = _tempItem.Sad_pb_price;
                item.Jcd_pb = _tempItem.Sad_pbook;
                item.Jcd_qty = _tempItem.Sad_qty;
                item.Jcd_pbseqno = _tempItem.Sad_seq;
                item.Jcd_net_amt = _tempItem.Sad_tot_amt;
                item.Jcd_unitprice = _tempItem.Sad_unit_rt;
                item.Jcd_amt = _tempItem.Sad_qty * _tempItem.Sad_unit_rt;
                item.Jcd_itmcd_DESC = _tempItem.Mi_longdesc;
                item.Jcd_line = _tempItem.Sad_itm_line;
                item.Jcd_itmdesc = string.IsNullOrEmpty(_tempItem.Sad_alt_itm_desc) ? _tempItem.Mi_longdesc : _tempItem.Sad_alt_itm_desc; // add by akila 2017/06/28
                oMainDetailList.Add(item);
            }

            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            dgvEstimateItems.DataSource = oMainDetailList;

            for (int i = 0; i < dgvEstimateItems.Rows.Count; i++)
            {
                dgvEstimateItems.Rows[i].Cells["Delete"].ReadOnly = true;
            }

            btnAddItem.Enabled = false;
            btnSave.Enabled = false;
            ModifyGrid();
            ClearMiddle1p0();
            calculateCostAndRevenue();
            calculateTotals();
        }

        private void Cancel()
        {
            if (IsBackDateOk(true, false) == false)
                return;
            if (string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Please select the invoice no", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtInvoiceNumber.Focus();
                return;
            }

            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, txtInvoiceNumber.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());

            if (_header.Count <= 0)
            {
                MessageBox.Show("Selected invoice no already canceled or invalid.", "Invalid Invoice no", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            if (_header[0].Sah_inv_sub_tp.Contains("CC"))
            {
                MessageBox.Show("Selected invoice belongs to a cash conversion. You cannot cancel  this invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Int32 _isRegistered = CHNLSVC.Sales.CheckforInvoiceRegistration(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNumber.Text.Trim());

            if (_isRegistered != 1)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("This invoice already registered!. You are not allow for cancelation.", "Registration Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            Int32 _isInsured = CHNLSVC.Sales.CheckforInvoiceInsurance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNumber.Text.Trim());
            if (_isInsured != 1)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("This invoice already insured!. You are not allow for cancelation.", "Insurance Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            //:: Chamal 7-Jul-2014 | :: If promotion voucher no generated invoice, refer for another invoice
            bool _isPromoVou = CHNLSVC.Sales.CheckPromoVoucherInvoiceUsed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNumber.Text.Trim());

            if (_isPromoVou == true)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("This invoice already used for promotion voucher invoice!. You are not allow for cancelation.", "Promotion Voucher Used", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            try
            {
                DataTable _buybackdoc = CHNLSVC.Inventory.GetBuyBackInventoryDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNumber.Text.Trim());
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
                SystemErrorMessage(ex);
            }
            List<InventoryHeader> _cancelDocument = null;
            try
            {
                DataTable _consignDocument = CHNLSVC.Inventory.GetConsginmentDocumentByInvoice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNumber.Text.Trim());
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
                SystemErrorMessage(ex);
            }
            try
            {
                //#region Gatepass Check by tharanga 
                //var _List = oMainDetailList.Select(m => new { m.Jcd_jobno, m.Jcd_joblineno }).Distinct().ToList();
                //#endregion

                this.Cursor = Cursors.WaitCursor;
                string _msg = "";
                Int32 _effect = CHNLSVC.Sales.InvoiceCancelation(_header[0], out _msg, _cancelDocument);
                this.Cursor = Cursors.Default;
                if (_effect == 1)
                {
                    //List<Int32> ConfirmationSeqNumber = oMainDetailList.Select(x => x.Jcd_seq).ToList();
                    List<string> ConfirmationSeqNumber = oMainDetailList.Select(x => x.Jcd_jobno).ToList();

                    string err = string.Empty;

                    foreach (string itemSeqs in ConfirmationSeqNumber)
                    {
                        _effect = CHNLSVC.CustService.UPDATE_SCV_CONF_HDR_ISINVD(0, 0, BaseCls.GlbUserComCode, out err, itemSeqs);
                    }

                    var selectedList = oMainDetailList.Select(m => new { m.Jcd_jobno, m.Jcd_joblineno }).Distinct().ToList();
                    foreach (var oJobNLine in selectedList)
                    {
                        Int32 jobLine = (oJobNLine.Jcd_joblineno == 0) ? 1 : oJobNLine.Jcd_joblineno;

                        int result = CHNLSVC.CustService.Update_JobDetailStage(oJobNLine.Jcd_jobno, jobLine, 7);
                        Service_Job_StageLog oLog = new Service_Job_StageLog();
                        oLog.SJL_REQNO = "";
                        oLog.SJL_JOBNO = oJobNLine.Jcd_jobno;
                        oLog.SJL_JOBLINE = jobLine;
                        oLog.SJL_COM = BaseCls.GlbUserComCode;
                        oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                        oLog.SJL_JOBSTAGE = 7;
                        oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                        oLog.SJL_CRE_DT = DateTime.Now;
                        oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                        oLog.SJL_INFSUP = 0;
                        result = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                    }

                    _msg = "Successfully Canceled!";

                    MessageBox.Show(_msg, "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(_msg, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Cursor = Cursors.Default;

                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                SystemErrorMessage(ex);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CheckServerDateTime() == false) return;

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10821))
            {
                MessageBox.Show("You dont have permission to cancel./nPermission code :- 10821", "Canceling...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //added by Wimal @ 20/07/2018 -  to avaiod cancel SUN uploaded invoice 
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNumber.Text);
            if (_hdr.Sah_is_acc_upload)
            {
                MessageBox.Show("Selected Invoice Can't Cancel.Already uploaded into Finance system", "Canceling...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable _dtcls = CHNLSVC.CustService.check_Invoiced_JobClosed(txtInvoiceNumber.Text);//Sanjeewa 2016-03-24
            string _msg1 = "";
            if (_dtcls != null)
            {
                if (_dtcls.Rows.Count > 0)
                {
                    foreach (DataRow drow in _dtcls.Rows)
                    {
                        _msg1 += drow["jbd_jobno"].ToString() + ", ";
                    }
                }
            }
            if (_msg1 != "")
            {
                MessageBox.Show("Can not cancel the Invoice. Following job numbers are already delivered. " + _msg1, "Canceling...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoiceNumber.Focus();
                return;
            }

            
           //added by Wimal @ 13/072018 to get None return balance DOC
           DataTable _dtpndStockItems = CHNLSVC.CustService.get_BalstockItem(BaseCls.GlbUserComCode, txtInvoiceNumber.Text.Trim());
            if (_dtpndStockItems.Rows.Count  > 0)
            {
                MessageBox.Show("Cannot cancel the invoice. Stock item return AOD document generated" + _msg1, "Canceling...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoiceNumber.Focus();
                return;
            }

            if (MessageBox.Show("Do you want to cancel?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DataTable _chk = CHNLSVC.Sales.CheckTheDocument(BaseCls.GlbUserComCode, txtInvoiceNumber.Text.Trim());
            if (_chk != null && _chk.Rows.Count > 0)
            {
                string _refDocument = _chk.Rows[0].Field<string>("itr_req_no");
                MessageBox.Show("This invoice is already picked for a inter-transfer. You are not allow to cancel this invoice until " + _refDocument + " inter-transfer settled.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            Cancel();
        }

        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty.ToUpper().ToString(), BaseCls.GlbUserDefProf, this.GlbModuleName, dtpDate, lblBackDateInfor, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpDate.Value.Date != DateTime.Now.Date)
                    {
                        //txtDate.Enabled = true;
                        this.Cursor = Cursors.Default;

                        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dtpDate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    //txtDate.Enabled = true;
                    this.Cursor = Cursors.Default;

                    MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtpDate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        private void LoadInvoiceProfitCenterDetail()
        {
            if (_MasterProfitCenter != null)
                if (_MasterProfitCenter.Mpc_cd != null)
                {
                    if (!_MasterProfitCenter.Mpc_edit_price)
                    {
                        txtUnitPrice.ReadOnly = true;
                    }
                    else
                    {
                        txtUnitPrice.ReadOnly = false;
                    }
                    txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer;
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                }
        }

        private void clearAll()
        {
            dtpDate.Value = DateTime.Now;
            txtJobNo.Clear();
            txtDisAmt.Clear();

            lblPromoVouNo.Text = "";
            lblPromoVouUsedFlag.Text = "";

            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            lblIsEditDesctiption.Text = "";
            lblItemUOM.Text = "";
            lblItemType.Text = "";
            lblManualRefNo.Text = "";
            lblcostPrice.Text = "";
            lblReqNo.Text = "";
            ClearMiddle1p0();
            txtCostTotal.Clear();
            txtRevenueTotal.Clear();
            txtMarginTotal.Clear();
            ClearRight1p1();
            lblSeq.Text = "";
            serviceClear();
            oMainDetailList = null;

            txtMarginPercentage.Clear();
            txtJobNo.Focus();
            lblBackDateInfor.Text = "";

            _masterBusinessCompany = new MasterBusinessEntity();

            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;

            ClearVariable();

            lblcostPrice.Text = "";

            pnlDeliveryDetails.Visible = false;
            pnlDeliveryInstruction.Visible = false;
            pnlStandByItems.Visible = false;
            _recieptItem = new List<RecieptItem>();
            rbnNewItem.Checked = true;
            editItem = new Service_Confirm_detail();
            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            dgvItems.DataSource = new List<Service_TempIssue>();
            dgvJobConf.DataSource = new List<Service_confirm_Header>();

            ucPayModes1.ClearControls();
            ucPayModes1.ClearControls();
            ucPayModes1.TotalAmount = 0;
            ucPayModes1.InvoiceItemList = null;
            ucPayModes1.SerialList = null;
            ucPayModes1.Amount.Text = "0";
            ucPayModes1.Mobile = string.Empty;
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.LoadData();

            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            txtRequest.Focus();
            rbnNewItem.Checked = true;
            chkManualRef.Checked = false;
            chkTaxPayable.Checked = false;

            IsEdit = false;

            dtpFromD.Value = DateTime.Today.AddDays(-30);
            dtpToD.Value = DateTime.Today;
            btnSearchHeader_Click(null, null);
            btnAddItem.Enabled = true;
            btnSave.Enabled = true;
            cmbJobNumber.Text = "";

            cmbJobNumber.DataSource = new List<String>();

            SelectedSerialID = 0;

            IsStartUp = false;
            txtRemarks.Clear();
        }

        private void ClearJobSearch()
        {
            dtpDate.Value = DateTime.Now;
            txtJobNo.Clear();
            txtDisAmt.Clear();

            lblPromoVouNo.Text = "";
            lblPromoVouUsedFlag.Text = "";

            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            lblIsEditDesctiption.Text = "";
            lblItemUOM.Text = "";
            lblItemType.Text = "";
            lblManualRefNo.Text = "";
            lblcostPrice.Text = "";
            lblReqNo.Text = "";
            ClearMiddle1p0();
            txtCostTotal.Clear();
            txtRevenueTotal.Clear();
            txtMarginTotal.Clear();
            ClearRight1p1();
            lblSeq.Text = "";

            oMainDetailList = null;

            txtMarginPercentage.Clear();
            txtJobNo.Focus();
            lblBackDateInfor.Text = "";

            _masterBusinessCompany = new MasterBusinessEntity();

            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;

            ClearVariable();

            lblcostPrice.Text = "";

            pnlDeliveryDetails.Visible = false;
            pnlDeliveryInstruction.Visible = false;
            pnlStandByItems.Visible = false;
            _recieptItem = new List<RecieptItem>();
            rbnNewItem.Checked = true;
            editItem = new Service_Confirm_detail();
            dgvEstimateItems.DataSource = new List<Service_Confirm_detail>();
            dgvItems.DataSource = new List<Service_TempIssue>();
            //dgvJobConf.DataSource = new List<Service_confirm_Header>();

            ucPayModes1.ClearControls();
            ucPayModes1.ClearControls();
            ucPayModes1.TotalAmount = 0;
            ucPayModes1.InvoiceItemList = null;
            ucPayModes1.SerialList = null;
            ucPayModes1.Amount.Text = "0";
            ucPayModes1.Mobile = string.Empty;
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.LoadData();

            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            txtRequest.Focus();
            rbnNewItem.Checked = true;
            chkManualRef.Checked = false;
            chkTaxPayable.Checked = false;

            IsEdit = false;

            dtpFromD.Value = DateTime.Today.AddDays(-30);
            dtpToD.Value = DateTime.Today;
            btnAddItem.Enabled = true;
            btnSave.Enabled = true;
            cmbJobNumber.Text = "";

            cmbJobNumber.DataSource = new List<String>();

            SelectedSerialID = 0;

            IsStartUp = false;
        }

        protected void BindGeneralDiscount()
        {
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
        }

        #endregion Methods

        private void dgvJobConf_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                string custCode = dgvJobConf.Rows[e.RowIndex].Cells["JCH_CUST_CD"].Value.ToString();
                if (!string.IsNullOrEmpty(txtBillingCustCode.Text) && custCode != txtBillingCustCode.Text)
                {
                    dgvJobConf.Rows[e.RowIndex].Cells[0].Value = false;
                    MessageBox.Show("Please select a same customer's confirmation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
          }

        private void dgvJobConf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJobConf.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
            {
                if (dgvJobConf.CurrentCell.IsInEditMode)
                {
                    if (dgvJobConf.IsCurrentCellDirty)
                    {
                        dgvJobConf.EndEdit();
                    }
                }
            }
          
        }


        //public Boolean revSelect = false;
        private void dgvJobConf_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvJobConf.IsCurrentCellDirty)
            {
                dgvJobConf.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }          

        }

        private void SetReadOnlyValues()
        {
            foreach (DataGridViewRow oRows in dgvJobConf.Rows)
            {
                //if (oRows.Cells["JCH_CUST_CD"].Value != null && oRows.Cells["JCH_CUST_CD"].Value.ToString().ToUpper() != txtBillingCustCode.Text.ToUpper().Trim())
                //{
                //    oRows.Cells["select"].ReadOnly = true;
                //}
                //else
                //{
                //    oRows.Cells["select"].ReadOnly = false;
                //}
                oRows.Cells["select"].ReadOnly = false;
            }
        }

        private void txtTeleDD_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTeleDD.Text))
            {
                if (!IsValidMobileOrLandNo(txtTeleDD.Text.Trim()))
                {
                    MessageBox.Show("Invalid mobile number.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTeleDD.Text = "";
                    txtTeleDD.Focus();
                    return;
                }
            }
        }

        private void sendMsg(InvoiceHeader invoiHeader, List<Service_Confirm_detail> confDetails)
        {
            string outMsg;
            Service_Chanal_parameter _scvParam = null;
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(invoiHeader.Sah_com, BaseCls.GlbDefSubChannel, 4);
            Service_Message oMessage = new Service_Message();
            string emailBody = string.Empty;
            if (oTemplate != null && oTemplate.Sml_templ_mail != null)
            {
                emailBody = oTemplate.Sml_templ_mail;
            }
            Service_JOB_HDR oJobHeader = CHNLSVC.CustService.GetServiceJobHeader(confDetails[0].Jcd_jobno, invoiHeader.Sah_com);
            List<Service_Tech_Aloc_Hdr> oTechAllocations = CHNLSVC.CustService.GetJobAllocations(oMessage.Sm_jobno, confDetails[0].Jcd_joblineno, oMessage.Sm_com);

            String techNames = string.Empty;
            foreach (Service_Tech_Aloc_Hdr oTechAllo in oTechAllocations)
            {
                techNames += oTechAllo.ESEP_FIRST_NAME + ", ";
            }

            emailBody = emailBody.Replace("[B_Cust]", oJobHeader.SJB_B_CUST_NAME)
                                 .Replace("[jobNo]", oJobHeader.SJB_JOBNO)
                                 .Replace("[date]", oJobHeader.SJB_DT.ToString("dd/MMM/yyyy  hh:mm tt"))
                                 .Replace("[tech]", techNames)
                                 .Replace("[InvoDate]", invoiHeader.Sah_dt.ToString("dd/MMM/yyyy  hh:mm tt"));


            String SmsBody = String.Empty;

            Int32 _smsid = 0;
            if (_scvParam.sp_cust_inform == 1)
            {
                _smsid = CHNLSVC.CustService.GetJobsmsSeq();
                DataTable _dt = CHNLSVC.CustService.GetCustSatisByChnl(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, 1);
                string _ans1 = string.Empty;
                string _ans2 = string.Empty;
                string _ans3 = string.Empty;
                string _ans4 = string.Empty;
                string _ans5 = string.Empty;
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (Convert.ToInt32(dr["ssq_seq"].ToString()) == 2)
                        {
                            SmsBody = SmsBody + "\n" + dr["ssq_quest"].ToString();
                            SmsBody = SmsBody.Replace("[jobNo]", oJobHeader.SJB_JOBNO);
                            SmsBody = SmsBody.Replace("[date]", invoiHeader.Sah_dt.ToString("dd/MMM/yyyy"));

                            DataTable _dt1 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "1");

                            foreach (DataRow dr1 in _dt1.Rows)
                            {
                                _ans1 = " 1." + dr1["scst_desc"].ToString();
                            }

                            DataTable _dt2 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "2");

                            foreach (DataRow dr2 in _dt2.Rows)
                            {
                                _ans2 = " 2." + dr2["scst_desc"].ToString();
                            }
                            DataTable _dt3 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "3");

                            foreach (DataRow dr3 in _dt3.Rows)
                            {
                                _ans3 = " 3." + dr3["scst_desc"].ToString();
                            }

                            DataTable _dt4 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "4");

                            foreach (DataRow dr4 in _dt4.Rows)
                            {
                                _ans4 = " 4." + dr4["scst_desc"].ToString();
                            }

                            DataTable _dt5 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "5");

                            foreach (DataRow dr5 in _dt5.Rows)
                            {
                                _ans5 = " 5." + dr5["scst_desc"].ToString();
                            }
                            SmsBody = SmsBody + "\n" + _ans1;
                            SmsBody = SmsBody + "\n" + _ans2;
                            SmsBody = SmsBody + "\n" + _ans3;
                            SmsBody = SmsBody + "\n" + _ans4;
                            SmsBody = SmsBody + "\n" + _ans5;
                            SmsBody = SmsBody + "\n" + "(" + Convert.ToString(_smsid) + "<Space>Rate)";
                        }
                    }

                }
            }
            else
            {
                if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                {
                    SmsBody = oTemplate.Sml_templ_sms;
                }
            }







            oMessage.Sm_com = invoiHeader.Sah_com;
            oMessage.Sm_jobno = oJobHeader.SJB_JOBNO;
            oMessage.Sm_joboline = 1;
            oMessage.Sm_jobstage = 8;// oJobHeader.SJB_JOBSTAGE; 09-SEP-2015 Nadeeka
            oMessage.Sm_ref_num = Convert.ToString(_smsid);
            oMessage.Sm_status = 0;
            oMessage.Sm_msg_tmlt_id = 4;
            oMessage.Sm_sms_text = SmsBody;
            oMessage.Sm_sms_gap = 0;
            oMessage.Sm_sms_done = 0;
            oMessage.Sm_mail_text = emailBody;
            oMessage.Sm_mail_gap = 0;
            oMessage.Sm_email_done = 0;
            oMessage.Sm_cre_by = BaseCls.GlbUserID;
            oMessage.Sm_cre_dt = DateTime.Now;
            oMessage.Sm_mod_by = BaseCls.GlbUserID;
            oMessage.Sm_mod_dt = DateTime.Now;
            int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                string _repname = string.Empty;
                string _papersize = string.Empty;
                BaseCls.GlbReportTp = "JOBINV";
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                ReportViewer _viewsvc = new ReportViewer();
                _viewsvc.GlbReportName = BaseCls.GlbReportName;
                _viewsvc.GlbReportDoc = txtInvoiceNumber.Text;
                BaseCls.GlbReportDoc = txtInvoiceNumber.Text;
                if (BaseCls.GlbDefSubChannel == "MCS" || BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefSubChannel=="RIT")//Tharanga add ABL 2017/07/07
               
                { if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
                else
                { BaseCls.GlbReportDirectPrint = 0; }
                if (BaseCls.GlbReportDirectPrint == 1)
                {// Nadeeka 11-07-2015 (Direct Print/ Sanjeewa's process)
                    FF.WindowsERPClient.Reports.Service.clsServiceRep obj = new FF.WindowsERPClient.Reports.Service.clsServiceRep();
                    if (BaseCls.GlbReportName == "Service_Invoice_ABE.rpt")//Add by tharanga 2017/07/07
                    {
                        obj.Service_Invocie_ABE();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        obj._Service_Invoice_ABE.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Invoice documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obj._Service_Invoice_ABE.PrintToPrinter(1, false, 0, 0);
                        
                    }
                    else
                    {
                       
                        obj.InvociePrintServicePhone();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        obj._JobInvoicePh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Invoice documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        obj._JobInvoicePh.PrintToPrinter(1, false, 0, 0);
                        
                    }

                }
                else
                {
                    _viewsvc.Show();
                    _viewsvc = null;
                }

            }
            else
            {
                MessageBox.Show("please select a invoice", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServiceReturnStandBy frm = new ServiceReturnStandBy();
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnVPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtJobNo.Text, 0,txtCustomer.Text);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            ServiceTasks frm = new ServiceTasks(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private List<InvoiceItem> ConvertConfirmItmList(List<Service_Confirm_detail> oConItmes)
        {
            List<InvoiceItem> oInvoItems = new List<InvoiceItem>();
            if (oConItmes != null && oConItmes.Count > 0)
            {
                try
                {
                    foreach (Service_Confirm_detail oConItem in oConItmes)
                    {
                        InvoiceItem oInvoItm = new InvoiceItem();
                        oInvoItm.Sad_disc_amt = oConItem.Jcd_dis;
                        oInvoItm.Sad_disc_rt = oConItem.Jcd_dis_rt;
                        oInvoItm.Sad_do_qty = oConItem.Jcd_qty;
                        oInvoItm.Sad_itm_cd = oConItem.Jcd_itmcd;
                        oInvoItm.Sad_itm_seq = oConItem.Jcd_pbitmseqno;
                        oInvoItm.Sad_itm_stus = oConItem.Jcd_itmstus;
                        oInvoItm.Sad_itm_tax_amt = oConItem.Jcd_tax;
                        oInvoItm.Sad_job_no = oConItem.Jcd_jobno;
                        oInvoItm.Sad_res_no = oConItem.Jcd_no;
                        oInvoItm.Sad_pb_lvl = oConItem.Jcd_pblvl;
                        oInvoItm.Sad_pb_price = oConItem.Jcd_pbprice;
                        oInvoItm.Sad_pbook = oConItem.Jcd_pb;
                        oInvoItm.Sad_qty = oConItem.Jcd_qty;
                        oInvoItm.Sad_seq = oConItem.Jcd_pbseqno;
                        oInvoItm.Sad_tot_amt = oConItem.Jcd_net_amt;
                        oInvoItm.Sad_unit_rt = oConItem.Jcd_unitprice;
                        oInvoItm.Mi_longdesc = oConItem.Jcd_itmcd_DESC;
                        oInvoItm.Sad_alt_itm_desc = oConItem.Jcd_itmdesc; //Add by akila 2017/06/28
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return oInvoItems;
        }

        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {
            pnlDiscountRequest.Visible = false;

        }

        private void ddlDisCategory_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (_infor.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == BaseCls.GlbUserID).ToList();
            if (_available == null || _available.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Your user id does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlDisCategory.Text == "Customer")
            {
                foreach (DataGridViewRow _r in gvDisItem.Rows)
                {
                    DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                    if (Convert.ToBoolean(_chk.Value) == true)
                    {
                        _chk.Value = false;
                    }
                }

                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Discount rate can not exceed the 100%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    this.Cursor = Cursors.Default;
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
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the item which you need to request", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                txtDisAmount.Clear();
            }
            string _customer = txtBillingCustCode.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (gvDisItem.Rows.Count > 0 && ddlDisCategory.Text == "Item")
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
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(_amt.Value).Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) > 100 && _type.Value.ToString().Contains("Rate"))
                        {
                            this.Cursor = Cursors.Default;
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
                        _discount.Sgdd_from_dt = Convert.ToDateTime(dtpDate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = txtInvoiceType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(dtpDate.Text);
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
                _discount.Sgdd_from_dt = Convert.ToDateTime(dtpDate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbLevel.Text.Trim();
                _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = txtInvoiceType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(dtpDate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customerReq, BaseCls.GlbUserID, _list, txtBillingCustCode.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully Saved! Document No : " + _customerReq + ".";
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(Msg, "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseChannel();
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }
        }

        private void btnDiscountRequest_Click(object sender, EventArgs e)
        {
            pnlDiscountRequest.Size = new Size(484, 143);
            if (pnlDiscountRequest.Visible)
            {
                pnlDiscountRequest.Visible = false;
                return;
            }
            else
                pnlDiscountRequest.Visible = true;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindGeneralDiscount();
                ddlDisCategory.Text = "Customer";

                if (string.IsNullOrEmpty(txtBillingCustCode.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtBillingCustCode.Text == "CASH")
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid customer. Customer should be registered.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (oMainDetailList != null)
                    if (oMainDetailList.Count > 0)
                    {
                        ddlDisCategory.Enabled = true;
                    }
                    else
                    {
                        ddlDisCategory.Text = "Customer";
                        ddlDisCategory.Enabled = false;
                    }
                else
                {
                    ddlDisCategory.Text = "Customer";
                    ddlDisCategory.Enabled = false;
                }

                if (oMainDetailList != null)
                    if (oMainDetailList.Count > 0)
                    {
                        _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                        foreach (Service_Confirm_detail _i in oMainDetailList)
                        {
                            CashGeneralEntiryDiscountDef _one = new CashGeneralEntiryDiscountDef();

                            var _dup = from _l in _CashGeneralEntiryDiscount
                                       where _l.Sgdd_itm == _i.Jcd_itmcd && _l.Sgdd_pb == _i.Jcd_pb && _l.Sgdd_pb_lvl == _i.Jcd_pblvl
                                       select _l;

                            if (_dup == null || _dup.Count() <= 0)
                            {
                                _one.Sgdd_itm = _i.Jcd_itmcd;
                                _one.Sgdd_pb = _i.Jcd_pb;
                                _one.Sgdd_pb_lvl = _i.Jcd_pblvl;

                                _CashGeneralEntiryDiscount.Add(_one);
                            }
                        }
                        gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                    }
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

        private void btnMoreDetails_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbJobNumber.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(cmbJobNumber.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void btnAgeSrc_Click(object sender, EventArgs e)
        {

            DataTable _agreetbl = CHNLSVC.CustService.getAgrNoInv(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpAgrFrom.Value, dtpAgrTo.Value);
            dgvAgr.AutoGenerateColumns = false;
            dgvAgr.DataSource = _agreetbl;

        }

        private void dgvAgr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtInvoiceRefNo.Text = dgvAgr.Rows[e.RowIndex].Cells["colAgree"].Value.ToString();
                txtBillingCustCode.Text = dgvAgr.Rows[e.RowIndex].Cells["colAgCust"].Value.ToString();
                txtBillingCustName.Text = dgvAgr.Rows[e.RowIndex].Cells["colAgCustName"].Value.ToString();
                txtCustomerNameDD.Text = dgvAgr.Rows[e.RowIndex].Cells["colAgCustName"].Value.ToString();
                txtInvoiceType.Text = "CS";
                _lstShed = CHNLSVC.CustService.getAgrPay(dgvAgr.Rows[e.RowIndex].Cells["colAgree"].Value.ToString());
                dgvPaySch.AutoGenerateColumns = false;
                dgvPaySch.DataSource = _lstShed;
            }
        }

        private void dgvPaySch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkAgreement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgreement.Checked == true)
            {
                pnlAgr.Visible = true;
                btnAddItem.Enabled = false;
                dgvEstimateItems.Enabled = false;
            }
            else

                pnlAgr.Visible = false;
            btnAddItem.Enabled = true;
            dgvEstimateItems.Enabled = true;

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            btnAddItem.Enabled = false;
            //  dgvEstimateItems.Enabled = false;

            List<scv_agr_cha> _lstItem = new List<scv_agr_cha>();
            _lstItem = CHNLSVC.CustService.getAgrItem(txtInvoiceRefNo.Text);
            Decimal _totAmt = 0;
            Decimal _selAmt = 0;
            Boolean _isselect = false;
            foreach (scv_agr_payshed item in _lstShed)
            {
                if (item.Sap_sel == true)
                {
                    _isselect = true;
                    _selAmt = _selAmt + item.Sap_bal_amt;
                }
                _totAmt = _totAmt + item.Sap_amt;
            }

            if (_selAmt == 0)
            {
                MessageBox.Show("Invoice amount should be grater than zero(0).", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            //if (_totAmt == 0)
            //{
            //    MessageBox.Show("Please select the schdule.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            if (_isselect == false)
            {
                MessageBox.Show("Please select the schdule.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dgvEstimateItems.DataSource = null;
            dgvEstimateItems.DataSource = new List<Service_Estimate_Item>();
            if (oMainDetailList == null)
            {
                oMainDetailList = new List<Service_Confirm_detail>();
            }
            oMainDetailList = new List<Service_Confirm_detail>();
            foreach (scv_agr_cha item in _lstItem)
            {
                MasterItem tempItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.Sac_itm_cd);
                Service_Confirm_detail newItem = new Service_Confirm_detail();
                newItem.Jcd_seq = 0;
                newItem.Jcd_no = string.Empty;
                newItem.Jcd_line = oMainDetailList.Count + 1;
                newItem.Jcd_jobno = "N/A";
                newItem.Jcd_itmcd = item.Sac_itm_cd;
                newItem.Jcd_itmstus = "GOD";
                newItem.Jcd_qty = item.Sac_qty;
                newItem.Jcd_pb = "N/A";
                newItem.Jcd_pblvl = "N/A";
                newItem.Jcd_unitprice = item.Sac_unit_rt * (_selAmt / _totAmt);
                newItem.Jcd_amt = item.Sac_unit_amt * (_selAmt / _totAmt);
                newItem.Jcd_tax = item.Sac_tax * (_selAmt / _totAmt);
                newItem.Jcd_dis_rt = item.Sac_dis_rt;
                newItem.Jcd_dis = item.Sac_dis_amt * (_selAmt / _totAmt);
                newItem.Jcd_net_amt = newItem.Jcd_amt + newItem.Jcd_tax - newItem.Jcd_dis;
                newItem.Jcd_itmtp = lblItemType.Text;
                newItem.Jcd_costelement = "";
                newItem.Jcd_docno = "";
                newItem.Jcd_rmk = "";
                newItem.Jcd_pbprice = 0;
                newItem.Jcd_pbseqno = 0;
                newItem.Jcd_pbitmseqno = 0;
                newItem.Jcd_itmdesc = item.Sac_itm_desc;
                newItem.Jcd_itmmodel = tempItem.Mi_model;
                newItem.Jcd_itmbrand = tempItem.Mi_brand;
                newItem.Jcd_itmuom = "";
                newItem.Jcd_ser_id = SelectedSerialID;
                newItem.Jcd_itmcd_DESC = item.Sac_itm_desc;
                newItem.IsNewRecord = "Y";

                newItem.WarrantyRemark = string.Empty;
                newItem.WarrantyRepirod = 0;
                newItem.IsPRint = true;

                newItem.costPrice = CHNLSVC.Inventory.GetLatestCost(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, "GOD");

                oMainDetailList.Add(newItem);

                BindAddItem();
                ModifyGrid();
                CalculateGrandTotal(newItem.Jcd_qty, newItem.Jcd_unitprice, newItem.Jcd_dis, newItem.Jcd_tax, true);
                ClearMiddle1p0();
                calculateCostAndRevenue();
                calculateTotals();
                SelectedSerialID = 0;

                // _lstShed = new List<scv_agr_payshed>();
                dgvPaySch.AutoGenerateColumns = false;
                dgvPaySch.DataSource = null;
                dgvEstimateItems.ReadOnly = true;
            }

            pnlAgr.Visible = false;
        }

        private void btnIssueLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIssueLoc;
                _CommonSearch.ShowDialog();
                txtIssueLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtIssueLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnIssueLoc_Click(null, null);


        }

        private void txtIssueLoc_DoubleClick(object sender, EventArgs e)
        {
            btnIssueLoc_Click(null, null);
        }

        public static DateTime GetLastDayOfPreviousMonth(DateTime startDate)
        {

            DateTime lastDayLastMonth = new DateTime(startDate.Year, startDate.Month, 1);
            lastDayLastMonth = lastDayLastMonth.AddDays(-1);

            startDate = lastDayLastMonth;

            return startDate;
        }

        private void CalculateTaxForConfirmItems(String Item, Decimal Qty, Decimal unitPrice, String ItemStatus, Decimal DisRate, Decimal DisAmount, out decimal TaxValue, out decimal LineTotValue)
        {
            TaxValue = 0;
            LineTotValue = 0;
            decimal UnitAmount = 0;

            if (!string.IsNullOrEmpty(Qty.ToString()) && !string.IsNullOrEmpty(unitPrice.ToString()))
            {
                UnitAmount = FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true);

                decimal _vatPortion = FigureRoundUp(TaxCalculation(Item, ItemStatus, Qty, _priceBookLevelRef, unitPrice, DisAmount, DisRate, true), true);
                TaxValue = _vatPortion;

                decimal _totalAmount = Qty * unitPrice;
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(DisRate.ToString()))
                {
                    bool _isVATInvoice = false;
                    if
                        (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available")
                        _isVATInvoice = true;
                    else
                        _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (DisRate / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (DisRate / 100), true);
                        if (DisRate > 0)
                        {
                            //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, Item, ItemStatus, string.Empty, string.Empty);
                            //if (_tax != null && _tax.Count > 0)
                            //{
                            //    decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            //    TaxValue = FigureRoundUp(_vatval, true);
                            //}
                        }
                    }

                    //txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(TaxValue.ToString()))
                {
                    if (DisRate > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - DisAmount, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + TaxValue - DisAmount, true);
                }

                LineTotValue = _totalAmount;
                //txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }

        private void txtJobNo_TextChanged(object sender, EventArgs e)
        {

        }
        private Boolean IsDiffTax(List<InvoiceItem> _ItemList)
        {// Nadeeka 30-12-2015
            decimal _taxRate = -1;
            Boolean _retVal = true;
            List<MasterItemTax> _itmTax = new List<MasterItemTax>();

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.Mc_resmultaxinv == 1)
            {
                if (_ItemList != null)
                {
                    foreach (InvoiceItem _itm in _ItemList.Where(x => x.Sad_unit_rt > 0))
                    {

                        _itmTax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty);

                        foreach (MasterItemTax _one in _itmTax.Where(x => x.Mict_tax_cd == "VAT"))
                        {
                            if (_taxRate == -1)
                            {
                                _taxRate = _one.Mict_tax_rate;
                            }
                            if (_taxRate != _one.Mict_tax_rate)
                            {
                                _retVal = false;
                            }

                        }

                    }
                }
            }

            return _retVal;
        }

        //add by akila 2017/09/08
        private bool ValidateCustomer(string _customer, string _invType)
        {
            bool _isAllow = false;

            try
            {
                if (string.IsNullOrEmpty(_invType))
                {
                    MessageBox.Show("Invalid invoice type", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (string.IsNullOrEmpty(_customer))
                {
                    MessageBox.Show("Please select a customer!", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    //Get allowed typs for selected inv type
                    sar_tp _allowInvType = new sar_tp();
                    _allowInvType.srtp_cd = _invType;

                    _allowInvType = CHNLSVC.General.GetMasterPrefixData(_allowInvType);
                    if (_allowInvType != null)
                    {
                        if (_allowInvType.srtp_main_tp != "CASH")
                        {
                            //get customer allow inv types
                            DataTable _cutomerAllowInvTypes = new DataTable();
                            _cutomerAllowInvTypes = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, _customer);
                            if (_cutomerAllowInvTypes.Rows.Count > 0)
                            {
                                var _selectedTypes = _cutomerAllowInvTypes.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == _allowInvType.srtp_main_tp).ToList();
                                if (_selectedTypes != null && _selectedTypes.Count > 0) { return true; }
                                else { MessageBox.Show("Customer not allow to ennter for seleced invoice type.", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                            }
                            else { MessageBox.Show("Customer allowd invoice types not found!", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                        }
                        else { return true; }
                    }
                    else { MessageBox.Show("Invoice types not found", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                }
            }
            catch (Exception ex)
            {
                _isAllow = false;
                MessageBox.Show("Error occurred while validation customer!" + Environment.NewLine + ex.Message, "Job Confirmation - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _isAllow;

            //try
        //{
            //    if (string.IsNullOrEmpty(_customer))
        //    {
            //        MessageBox.Show("Please select a customer!", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        _isAllow = false;
            //    }
            //    else
            //    {
            //        DataTable _allowInvTypes = new DataTable();
            //        _allowInvTypes = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, _customer);
            //        if (_allowInvTypes.Rows.Count > 0)
        //        {
            //            if (cmbInvType.Items.Count > 0)
        //            {
            //                var _selectedTypes = _allowInvTypes.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
            //                if (_selectedTypes == null) { MessageBox.Show("Customer not allow to ennter for seleced invoice type.", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _isAllow = false; }
            //                else { _isAllow = true; }
        //            }
            //            else { MessageBox.Show("Invoice types not found", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _isAllow = false; }
        //        }
            //        else { MessageBox.Show("Customer allowd invoice types not found!", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _isAllow = false; }
        //    }
            //}
            //catch (Exception ex)
            //{
            //    _isAllow = false;
            //    MessageBox.Show("Error occurred while validation customer!" + Environment.NewLine + ex.Message, "Job Confirmation - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
            //Cursor = DefaultCursor;
            //return _isAllow;
        }




        private void dgvJobConf_Validated(object sender, EventArgs e)
        {
            //if (remJobNo != "")
            //{
            //    foreach (DataGridViewRow row in dgvJobConf.Rows)
            //    {
            //        //currQty += row.Cells["qty"].Value;               

            //        if (remJobNo == row.Cells["JCH_JOBNO"].Value.ToString())
            //        {
            //            row.Cells["select"].Value = false;
            //        }
            //    }
            //    //More code here
            //    remJobNo = "";
            //}
        }

        private void txtBillingCustCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcustcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtcustcd;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtcustcd.Select();
            }
        }

        private void btnCreateservice_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbJobNumber.Text.ToString()))
                {
                    MessageBox.Show("Please enter valid Job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                    return;
                }

                if (pnlService.Visible)
                    pnlService.Visible = false;
                else
                    pnlService.Visible = true;
                this.pnlService.Size = new System.Drawing.Size(396, 338);
                this.pnlService.Location = new System.Drawing.Point(7, 7);

                txtDuration.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnServiceadd_Click(object sender, EventArgs e)
        {
            //Service_free_det objlist = new Service_free_det();
            if (Convert.ToDateTime(dtpDate.Text).Date > Convert.ToDateTime(dtpFirstservceday.Text).Date)
            {
                MessageBox.Show("First service can;t back date", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                serviceClear();
                return;
            }
            if (string.IsNullOrEmpty(txtscvloac.Text))
            {
                MessageBox.Show("Select Free service Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtscvprocnter.Text))
            {
                MessageBox.Show("Select Free Profitcenter Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }  

            DateTime ct = dtpFirstservceday.Value.Date;
            DateTime val = DateTime.Now.Date;

            Count = Convert.ToInt32(string.IsNullOrEmpty(txtNoofservice.Text) ? "0" : txtNoofservice.Text);
            duration = Convert.ToInt32(string.IsNullOrEmpty(txtDuration.Text) ? "0" : txtDuration.Text);

            if (cbAutocreate.Checked)
            {
                lstService = new List<Service_free_det>();
                for (int i = 1; i < Count + 1; i++)
                {

                    Service_free_det objlist = new Service_free_det();

                    if (i == 1)
                    {
                        val = ct.AddDays(duration);
                    }
                    else
                    {
                        val = val.AddDays(duration);
                    }

                    objlist.Servicedates = val;
                    objlist.Description = "Serivce " + i;
                    objlist.Noofservices = i;
                    lstService.Add(objlist);
                }

            }
            else
            {
                Service_free_det objlist = new Service_free_det();
                objlist.Servicedates = ct;
                objlist.Description = "Serivce ";
                objlist.Noofservices = Count;
                lstService.Add(objlist);
            }

            if (cbAutocreate.Checked)
            {
                DataTable _dt1 = lstService.ToDataTable();
                dgvService.DataSource = null;
                dgvService.AutoGenerateColumns = false;
                dgvService.DataSource = _dt1;
                dgvService.ReadOnly = true;
            }
            else
            {
                DataTable _dt1 = lstService.ToDataTable();
                dgvService.DataSource = null;
                dgvService.AutoGenerateColumns = false;
                dgvService.DataSource = _dt1;
                dgvService.ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtscvloac.Text))
            {
                MessageBox.Show("Select Free service Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtscvprocnter.Text))
            {
                MessageBox.Show("Select Free Profitcenter Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }  
            pnlService.Visible = false;
        }

        private void btnServiceclear_Click(object sender, EventArgs e)
        {

            serviceClear();
        }

        private void serviceClear()
        {
            txtNoofservice.Text = "";
            txtDuration.Text = "";
            cbAutocreate.Checked = false;
            dtpFirstservceday.Text = Convert.ToString(DateTime.Now.Date);
            txtscvloac.Text = string.Empty;
            txtscvprocnter.Text = string.Empty;
            lstService = new List<Service_free_det>();
            if (dgvService.DataSource != null)
            {
                dgvService.DataSource = null;
            }
        }

        private void btnServiceClose_Click(object sender, EventArgs e)
        {
            pnlService.Visible = false;
        }

        private void brnacsevloc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AC_SVC_ALW_LOC);
                _result = CHNLSVC.CommonSearch.Get_CLS_ALW_LOC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtscvloac;
                _CommonSearch.ShowDialog();
                txtscvloac.Select();
                ValidateServiceLocation(txtscvloac.Text);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }

        private void ValidateServiceLocation(string _location)
        {
            try
            {
                DataTable _locationDetails = new DataTable();
                _locationDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, _location);
                if (_locationDetails.Rows.Count > 0)
                {
                    foreach (DataRow _loc in _locationDetails.Rows)
                    {
                        string _tmpLocType = _loc["ml_loc_tp"] == DBNull.Value ? string.Empty : _loc["ml_loc_tp"].ToString();
                        if (_tmpLocType == "SERC")
                        {
                            txtscvprocnter.Text = _loc["ml_def_pc"] == DBNull.Value ? string.Empty : _loc["ml_def_pc"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Select Valid Service Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtscvloac.Text = "";
                            txtscvloac.Focus();

                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Select Valid Service Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtscvloac.Text = "";
                    txtscvloac.Focus();

                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtscvloac_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtscvloac.Text))
            {
                ValidateServiceLocation(txtscvloac.Text);
            }
        }

        private void cbAutocreate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutocreate.Checked)
            {
                txtDuration.Enabled = true;
                dgvService.ReadOnly = false;

                if (dgvService.DataSource != null)
                {
                    if (lstService.Count > 0)
                    {
                        lstService = new List<Service_free_det>();
                        DataTable _dt1 = lstService.ToDataTable();
                        dgvService.DataSource = null;
                        dgvService.AutoGenerateColumns = false;
                        dgvService.DataSource = _dt1;
                    }
                }

            }
            else
            {
                txtDuration.Enabled = false;
                dgvService.ReadOnly = true;
                txtDuration.Text = "";

                if (dgvService.DataSource != null)
                {
                    if (lstService.Count > 0)
                    {
                        lstService = new List<Service_free_det>();
                        DataTable _dt1 = lstService.ToDataTable();
                        dgvService.DataSource = null;
                        dgvService.AutoGenerateColumns = false;
                        dgvService.DataSource = _dt1;
                    }
                }
            }
        }

        private void txtNoofservice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDuration_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

       
    }
}