using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq.Expressions;
using FF.WindowsERPClient.Reports.Sales;
using System.Text.RegularExpressions;
using System.Resources;
using System.Globalization;
using FF.BusinessObjects.Sales; 

namespace FF.WindowsERPClient.Services
{
    public partial class JobConfirmation : Base
    {

        Service_Chanal_parameter _scvParam = new Service_Chanal_parameter();
        List<Service_job_Det> _loadJobDet = new List<Service_job_Det>();
        List<Service_job_Det> _processJobDet = new List<Service_job_Det>();
        List<Service_Cost_sheet> _serCostSheet = new List<Service_Cost_sheet>();
        List<Service_Confirm_detail> _serJobConfDet = null;
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
        List<Service_TempIssue> oTempIssueList = new List<Service_TempIssue>();
        private List<PriceCombinedItemRef> _MainPriceCombinItem = new List<PriceCombinedItemRef>();
        private List<MasterItemComponent> _masterItemComponent = null;
        private List<MasterItemTax> MainTaxConstant = null;
        private List<PriceDetailRef> _priceDetailRef = null;
        private MasterItem _itemdetail = new MasterItem();
        private Boolean _isMinus = false;
        Boolean IsSaleFigureRoundUp = false;
        Boolean _processStart = false;
        DataTable MasterChannel = null;
        private string DefaultInvoiceType = string.Empty;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private string DefaultStatus = string.Empty;
        private string DefaultItemStatus = string.Empty;
        DataTable _levelStatus = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        Boolean IsPriceLevelAllowDoAnyStatus = false;
        private PriceBookLevelRef _priceBookLevelRef = null;
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
        private string _itmType = string.Empty;
        private Boolean _isStandBy = false;
        private DateTime _serverDt = DateTime.Now.Date;
        private Boolean _isRplAllow = false;
        private Boolean _isCalcDisc = false;
        private string _jobTp = "";     //kapila 12/2/2016
        private DateTime _jobDt;
        private Boolean _isDefIssuLoc = false;  //kapila 10/5/2016
        private Boolean _isStrucBaseTax = false;
        private bool IsPriceBookDetAvailable = false;
        public string invno = string.Empty;
        public string othcom = string.Empty;
        //
        private Service_Req_Hdr _scvjobHdr = new Service_Req_Hdr();
        private List<Service_Req_Det> _scvItemList = new List<Service_Req_Det>();

        private List<Service_Tech_Aloc_Hdr> _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
        private List<Service_Req_Det_Sub> _scvItemSubList = new List<Service_Req_Det_Sub>();
        private List<Service_Req_Det_Sub> _tempItemSubList = new List<Service_Req_Det_Sub>();
        private List<Service_TempIssue> _scvStdbyList = new List<Service_TempIssue>();
        private List<Service_free_det> lstService = new List<Service_free_det>();
        private int Count = 0;
        private int duration = 0;
        private List<Service_Req_Def> _scvDefList = null;

        public JobConfirmation()
        {
            InitializeComponent();
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            LoadCachedObjects();
            LoadPriceDefaultValue();
            if (_scvParam == null)
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

        }

        private void LoadPayMode()
        {
            ucPayModes1.InvoiceType = cmbInvType.Text.Trim(); ucPayModes1.Customer_Code = lblCusCode.Text.Trim(); ucPayModes1.LoadPayModes();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        //Add by akila 2017/06/05 load started job in job confirmetion UI
                        if (_scvParam.SP_ISNEEDWIP == 1)
                        {
                            if (_scvParam.SP_AUTO_START_JOB == 1)
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "6,4" + seperator);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "6" + seperator);
                            }                           
                        }
                        else
                        {
                            if (_scvParam.SP_AUTO_START_JOB == 1)
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4" + seperator);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6" + seperator);
                            }
                            
                        }                        
                        
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ConfByJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefChannel + seperator + BaseCls.GlbUserDefProf + seperator + "0" + seperator + txtCanJob.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CLS_ALW_LOC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + ddlCloseType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PreferLoc:
                    {
                        paramsText.Append(othcom + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AC_SVC_ALW_LOC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "AC_SCV" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void LoadCachedObjects()
        { _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString()); _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString()); MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString()); IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString()); }

        private void Clear_Data()
        {
            serviceClear();
            _loadJobDet = new List<Service_job_Det>();
            _processJobDet = new List<Service_job_Det>();
            _serCostSheet = new List<Service_Cost_sheet>();
            _serJobConfDet = new List<Service_Confirm_detail>();
            //_serJobConfDet = null;
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            dgvCostElement.AutoGenerateColumns = false;
            dgvCostElement.DataSource = new List<Service_Cost_sheet>();
            dgvRevElement.AutoGenerateColumns = false;
            dgvRevElement.DataSource = new List<Service_Confirm_detail>();
            //dgvRevElement.DataSource = null;
            grvDefItms.AutoGenerateColumns = false;
            grvDefItms.DataSource = new List<Service_job_Det>();
            _itemdetail = new MasterItem();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            oTempIssueList = new List<Service_TempIssue>();
            //ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.ClearControls();
            pnlStandByItems.Visible = false;
            MainTaxConstant = null;
            _priceDetailRef = null;
            _masterItemComponent = null;
            pnlEdit.Visible = false;
            _processStart = false;
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblCusName.ReadOnly = true;
            lblAdd1.ReadOnly = true;
            lblAdd2.ReadOnly = true;
            lblCusCode.ReadOnly = true;
            chkTaxPayable.Checked = false;
            lblVatExemptStatus.Text = "";            
            WarrantyRemarks = string.Empty;
            SSPriceBookPrice = 0;
            SSNormalPrice = 0;
            LoadPayMode();
            txtDispatchRequried.Text = "";
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = 0;
            SSCombineLine = 0;
            WarrantyPeriod = 0;
            _isCompleteCode = false;
            _isCombineAdding = false;
            _isEditPrice = false;
            _isEditDiscount = false;
            _isInventoryCombineAdded = false;
            _isCheckedPriceCombine = false;
            _isFirstPriceComItem = false;
            _combineCounter = 0;
            ManagerDiscount = null;
            _itmType = string.Empty;
            _isMinus = false;
            chkEditItm.Checked = false;
            loadCloseTypes();
            SetDecimalTextBoxForZero(true, true);
            lblBackDateInfor.Text = "";
            dtDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            _jobDt = dtDate.Value;
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            CalculateGP();

            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblVatExemptStatus.Text = "";
            lblAdd1.Text = "";
            lblAdd2.Text = "";
            lblAdd3.Text = "";
            txtJobItem.Text = "";
            pnlJobCostDet.Visible = false;
            lblJobItemCost.Text = "";
            lblCostItemSer.Text = "";
            lblJobCostLine.Text = "";
            lblJobCostWara.Text = "";
            txtCostItm.Enabled = false;
            txtCostQty.Enabled = false;
            txtUnitCost.Enabled = false;
            txtCostItm.Text = "";
            txtCostQty.Text = "";
            txtUnitCost.Text = "";
            chkInvNow.Enabled = true;
            chkInvNow.Checked = false;
            txtRefNo.Text = "";
            txtRemarks.Text = "";
            txtReqNo.Text = "";
            txtCloseRmk.Text = "";
            txtDelCustomer.Text = "";
            txtDelAddress1.Text = "";
            txtDelAddress2.Text = "";
            txtDelName.Text = "";
            chkChangeCus.Checked = false;
            btnAddCost.Enabled = false;
            ucServicePriority1.clearAll();
            _isStandBy = false;
            btnStandBy.Enabled = false;
            lblJobCap.Visible = false;
            lblIDesc.Text = "";
            lblIDesc.ReadOnly = true;
            lblImodel.Text = "";
            lblIBrand.Text = "";
            chkAddCost.Checked = false;
            chkAddNewRev.Checked = false;
            label7.Visible = true;
            label9.Visible = true;
            txtTotalCost.Visible = true;
            txtGP.Visible = true;
            txtGPRt.Visible = true;
            txtPreferLoc.Text = "";
            LoadInvoiceType();
            cmbInvType.Enabled = true;
            btnSave.Enabled = true;

            //Add by akila 2017/06/15
            lblSvatStatus.Text = "None";
            lblVatExemStatus.Text = "None";
            invno = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("Do you want to clear screen ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            Clear_Data();
            txtJobNo.Text = "";
            serviceClear();




        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
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
                txtJobNo.Select();
                txtJobNo_Leave(null, null);
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

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                Clear_Data();
                    _loadJobDet = new List<Service_job_Det>();
                List<Service_job_Det> _JobDet = new List<Service_job_Det>();
                _JobDet = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtJobNo.Text.Trim());


                if (_JobDet != null && _JobDet.Count > 0)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
                    {
                        foreach (Service_job_Det _jDet in _JobDet)
                        {
                            if (_scvParam.SP_ISNEEDWIP == 1)
                            {
                                // if (_jDet.Jbd_stage == 6 || _jDet.Jbd_stage == 7)

                                //add by akila 2017/06/05
                                if (_scvParam.SP_AUTO_START_JOB == 1)
                                {
                                    if ((_jDet.Jbd_stage == 6)|| (_jDet.Jbd_stage == 4))
                                    {
                                        _loadJobDet.Add(_jDet);
                                    }
                                }
                                else
                                {
                                    if (_jDet.Jbd_stage == 6)
                                    {
                                        _loadJobDet.Add(_jDet);
                                    }
                                }
                                

                            }
                            else
                            {
                                //if (_jDet.Jbd_stage == 3 || _jDet.Jbd_stage == 6 || _jDet.Jbd_stage == 7)

                                //add by akila 2017/06/05
                                if (_scvParam.SP_AUTO_START_JOB == 1)
                                {
                                    if (_jDet.Jbd_stage == 3 || _jDet.Jbd_stage == 6 || _jDet.Jbd_stage == 4)
                                    {
                                        _loadJobDet.Add(_jDet);
                                    }
                                }
                                else
                                {
                                    if (_jDet.Jbd_stage == 3 || _jDet.Jbd_stage == 6)
                                    {
                                        _loadJobDet.Add(_jDet);
                                    }
                                }
                            }
                            invno = _jDet.Jbd_invc_no; // add by tharanga 2017/10/29
                        }

                        _loadJobDet = _loadJobDet.OrderBy(x => x.Jbd_jobline).ToList();
                       

                        grvDefItms.AutoGenerateColumns = false;
                        grvDefItms.DataSource = new DataTable();
                        grvDefItms.DataSource = _loadJobDet;

                        if (_loadJobDet.Count <= 0)
                        {
                            MessageBox.Show("Invalid job number.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtJobNo.Text = "";
                            lblCusCode.Text = "";
                            lblCusName.Text = "";
                            txtRefNo.Text = "";
                            lblAdd1.Text = "";
                            lblAdd2.Text = "";
                            lblAdd3.Text = "";
                            lblCusName.ReadOnly = true;
                            lblAdd1.ReadOnly = true;
                            lblAdd2.ReadOnly = true;
                            _jobTp = "";
                            txtJobNo.Focus();
                            return;
                        }

                        foreach (DataGridViewRow row in grvDefItms.Rows)
                        {

                            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                            ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells[0];

                            if (ch1.Value == null)
                                ch1.Value = false;
                            switch (ch1.Value.ToString())
                            {
                                case "False":
                                    {
                                        ch1.Value = true;
                                        break;
                                    }

                            }
                            goto L1;
                        }

                    L1:
                        Service_JOB_HDR _JobHdr = new Service_JOB_HDR();
                        _JobHdr = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text.Trim(), BaseCls.GlbUserComCode);

                        if (_JobHdr.SJB_COM != null)
                        {
                            lblCusName.Text = _JobHdr.SJB_B_CUST_NAME;
                            lblCusCode.Text = _JobHdr.SJB_B_CUST_CD;
                            txtReqNo.Text = _JobHdr.SJB_REQNO;
                            lblAdd1.Text = _JobHdr.SJB_B_ADD1;
                            lblAdd2.Text = _JobHdr.SJB_B_ADD2;
                            lblAdd3.Text = _JobHdr.SJB_B_ADD3;
                            _jobTp = _JobHdr.SJB_JOBTP;     //kapila 12/2/2016
                            _jobDt = _JobHdr.SJB_DT;

                            if (lblCusCode.Text == "CASH")
                            {
                                lblCusName.ReadOnly = false;
                                lblAdd1.ReadOnly = false;
                                lblAdd2.ReadOnly = false;

                            }
                            else
                            {
                                lblCusName.ReadOnly = true;
                                lblAdd1.ReadOnly = true;
                                lblAdd2.ReadOnly = true;

                                //Add by akila 2017/06/15
                                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, lblCusCode.Text.Trim(), string.Empty, string.Empty, "C");
                                if (_masterBusinessCompany != null)
                                {
                                    lblSvatStatus.Text = _masterBusinessCompany.Mbe_is_svat == true ? "Available" : "None";
                                    lblVatExemptStatus.Text = _masterBusinessCompany.Mbe_tax_ex == true ? "Available" : "None";
                                    lblVatExemStatus.Text = _masterBusinessCompany.Mbe_tax_ex == true ? "Available" : "None";
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid job number.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtJobNo.Text = "";
                            lblCusCode.Text = "";
                            lblCusName.Text = "";
                            txtRefNo.Text = "";
                            lblAdd1.Text = "";
                            lblAdd2.Text = "";
                            lblAdd3.Text = "";
                            lblCusName.ReadOnly = true;
                            lblAdd1.ReadOnly = true;
                            lblAdd2.ReadOnly = true;
                            _jobTp = "";
                            _jobDt = DateTime.Now.Date;
                            grvDefItms.AutoGenerateColumns = false;
                            grvDefItms.DataSource = new DataTable();
                            txtJobNo.Focus();
                            return;
                        }
                        List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(lblCusCode.Text.Trim(), BaseCls.GlbUserComCode);
                        if (oItems != null && oItems.Count > 0)
                        {
                            ucServicePriority1.GblCustCode = lblCusCode.Text.Trim();
                            ucServicePriority1.LoadData();
                        }
                        else
                        {
                            ucServicePriority1.GblCustCode = "CASH";
                            ucServicePriority1.LoadData();
                        }
                    }
                    else
                    {
                        //Check and load allocation technicians jobs for superviser level
                        List<MasterServiceEmployee> _setupEmp = new List<MasterServiceEmployee>();
                        string _type = "";
                        string _value = "";
                        Boolean _found = false;
                        string _SupCha = _scvParam.SP_SUP_CAT_CHA;
                        SystemUser _usr = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                        string _empId = _usr.Se_emp_cd;
                        List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                        if (_Saleshir.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                            {
                                _type = _one.Mpi_cd;
                                _value = _one.Mpi_val;

                                _setupEmp = new List<MasterServiceEmployee>();
                                _setupEmp = CHNLSVC.CustService.GetMasterSerEmp(BaseCls.GlbUserComCode, _type, _value, _SupCha, _empId, 1);

                                if (_setupEmp != null)
                                {
                                    if (_setupEmp.Count > 0)
                                    {
                                        _found = true;
                                        goto L101;
                                    }
                                    else
                                    {
                                        _found = false;
                                    }
                                }

                            }
                        }
                    L101:
                        Boolean _isTechJob = false;
                        if (_setupEmp != null && _setupEmp.Count > 0)
                        {
                            foreach (MasterServiceEmployee _tmpEmp in _setupEmp)
                            {
                                foreach (Service_job_Det _jDet in _JobDet)
                                {
                                    Service_Tech_Aloc_Hdr _techAlloc = new Service_Tech_Aloc_Hdr();
                                    _techAlloc = CHNLSVC.CustService.GetAllocTechJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "J", txtJobNo.Text.Trim(), _jDet.Jbd_jobline, _tmpEmp.Msi_emp, "A", 1);
                                    if (_techAlloc.STH_JOBNO != null)
                                    {
                                        _isTechJob = true;
                                        if (_scvParam.SP_ISNEEDWIP == 1)
                                        {
                                            if (_jDet.Jbd_stage == 6)
                                            {
                                                _loadJobDet.Add(_jDet);
                                            }

                                        }
                                        else
                                        {
                                            if (_jDet.Jbd_stage == 3 || _jDet.Jbd_stage == 6)
                                            {
                                                _loadJobDet.Add(_jDet);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Service_job_Det _jDet in _JobDet)
                            {
                                Service_Tech_Aloc_Hdr _techAlloc = new Service_Tech_Aloc_Hdr();
                                _techAlloc = CHNLSVC.CustService.GetAllocTechJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "J", txtJobNo.Text.Trim(), _jDet.Jbd_jobline, _empId, "A", 1);
                                if (_techAlloc.STH_JOBNO != null)
                                {
                                    _isTechJob = true;
                                    if (_scvParam.SP_ISNEEDWIP == 1)
                                    {
                                        if (_jDet.Jbd_stage == 6)
                                        {
                                            _loadJobDet.Add(_jDet);
                                        }

                                    }
                                    else
                                    {
                                        if (_jDet.Jbd_stage == 3 || _jDet.Jbd_stage == 6)
                                        {
                                            _loadJobDet.Add(_jDet);
                                        }
                                    }
                                }
                            }
                        }


                        _loadJobDet = _loadJobDet.OrderBy(x => x.Jbd_jobline).ToList();


                        grvDefItms.AutoGenerateColumns = false;
                        grvDefItms.DataSource = new DataTable();
                        grvDefItms.DataSource = _loadJobDet;

                        if (_loadJobDet.Count <= 0)
                        {
                            if (_found == false)
                            {
                                MessageBox.Show("Permission denied.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (_isTechJob == false)
                            {
                                MessageBox.Show("Permission denied.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Invalid job number.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            txtJobNo.Text = "";
                            lblCusCode.Text = "";
                            lblCusName.Text = "";
                            txtRefNo.Text = "";
                            lblAdd1.Text = "";
                            lblAdd2.Text = "";
                            lblAdd3.Text = "";
                            lblCusName.ReadOnly = true;
                            lblAdd1.ReadOnly = true;
                            lblAdd2.ReadOnly = true;
                            txtJobNo.Focus();
                            return;
                        }

                        foreach (DataGridViewRow row in grvDefItms.Rows)
                        {

                            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                            ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells[0];

                            if (ch1.Value == null)
                                ch1.Value = false;
                            switch (ch1.Value.ToString())
                            {
                                case "False":
                                    {
                                        ch1.Value = true;
                                        break;
                                    }

                            }
                            goto L1;
                        }

                    L1:
                        Service_JOB_HDR _JobHdr = new Service_JOB_HDR();
                        _JobHdr = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text.Trim(), BaseCls.GlbUserComCode);

                        if (_JobHdr != null)
                        {

                            lblCusName.Text = _JobHdr.SJB_B_CUST_NAME;
                            lblCusCode.Text = _JobHdr.SJB_B_CUST_CD;
                            txtReqNo.Text = _JobHdr.SJB_REQNO;
                            lblAdd1.Text = _JobHdr.SJB_B_ADD1;
                            lblAdd2.Text = _JobHdr.SJB_B_ADD2;
                            lblAdd3.Text = _JobHdr.SJB_B_ADD3;
                            _jobDt = _JobHdr.SJB_DT;

                            if (lblCusCode.Text == "CASH")
                            {
                                lblCusName.ReadOnly = false;
                                lblAdd1.ReadOnly = false;
                                lblAdd2.ReadOnly = false;

                            }
                            else
                            {
                                lblCusName.ReadOnly = true;
                                lblAdd1.ReadOnly = true;
                                lblAdd2.ReadOnly = true;
                            }
                        }
                        List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(lblCusCode.Text.Trim(), BaseCls.GlbUserComCode);
                        if (oItems != null && oItems.Count > 0)
                        {
                            ucServicePriority1.GblCustCode = lblCusCode.Text.Trim();
                            ucServicePriority1.LoadData();
                        }
                        else
                        {
                            ucServicePriority1.GblCustCode = "CASH";
                            ucServicePriority1.LoadData();
                        }

                    }

                    if (lblCusCode.Text == "CASH")
                    {
                        MessageBox.Show("Please check billing customer. Customer code [CASH] not allow.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear_Data();
                        txtJobNo.Text = "";
                        return;
                    }

                    MasterBusinessEntity _busEntity = new MasterBusinessEntity();
                    _busEntity = CHNLSVC.Sales.GetCustomerProfileByCom(lblCusCode.Text.Trim(), null, null, null, null, BaseCls.GlbUserComCode);

                    if (_busEntity != null)
                    {
                        chkTaxPayable.Checked = _busEntity.Mbe_is_tax;
                    }
                    else
                    {
                        chkTaxPayable.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid job number", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Text = "";
                    lblCusCode.Text = "";
                    lblCusName.Text = "";
                    txtRefNo.Text = "";
                    lblAdd1.Text = "";
                    lblAdd2.Text = "";
                    lblAdd3.Text = "";
                    lblCusName.ReadOnly = true;
                    lblAdd1.ReadOnly = true;
                    lblAdd2.ReadOnly = true;
                    txtJobNo.Focus();
                    return;
                }


            }
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
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
                txtJobNo.Select();
                txtJobNo_Leave(null, null);
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

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                    txtJobNo.Select();
                    txtJobNo_Leave(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    // txtInvType.Focus();
                    cmbInvType.Focus();
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



        private void CalculateGP()
        {
            decimal _totCost = 0;
            decimal _totRev = 0;
            decimal _gpAmt = 0;
            decimal _gpRt = 0;

            foreach (Service_Cost_sheet _tmpCost in _serCostSheet)
            {
                _totCost = _totCost + _tmpCost.CS_TOTUNITCOST;
            }

            foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
            {
                _totRev = _totRev + _tmpConf.Jcd_net_amt;
            }


            _gpAmt = _totRev - _totCost;

            if (_totRev > 0)
            {
                _gpRt = _gpAmt / _totRev * 100;
            }

            txtTotalCost.Text = _totCost.ToString("0.00");
            txtTotRev.Text = _totRev.ToString("0.00");
            txtGP.Text = _gpAmt.ToString("0.00");
            txtGPRt.Text = _gpRt.ToString("0.00");

            ucPayModes1.TotalAmount = Convert.ToDecimal(txtTotRev.Text.Trim());
            LoadPayMode();

        }

        private void btnProcessCost_Click(object sender, EventArgs e)
        {
            List<ServiceJobDetail> _lstJob = new List<ServiceJobDetail>();
            _serCostSheet = new List<Service_Cost_sheet>();
            _processJobDet = new List<Service_job_Det>();
            _serJobConfDet = new List<Service_Confirm_detail>();
            Int32 _jobLine = 0;
            string _techClsTp = "";
            if (grvDefItms.Rows.Count <= 0)
            {
                MessageBox.Show("Cannot find any job items.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_MasterProfitCenter.Mpc_issp_tax == true)
            {
                List<MasterPCTax> _masterPCTax = new List<MasterPCTax>();
                _masterPCTax = CHNLSVC.Sales.GetPcTax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, 1, dtDate.Value.Date);

                if (_masterPCTax == null || _masterPCTax.Count <= 0)
                {
                    MessageBox.Show("Profit center base taxes not setup.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (MessageBox.Show("Please confirm selected invoice type is correct ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            Boolean _appItm = false;
            foreach (DataGridViewRow row in grvDefItms.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["col_Chk"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _appItm = true;
                    goto L4;
                }
            }
        L4:

            if (_appItm == false)
            {
                MessageBox.Show("Please select job items.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Get Cost Details
            foreach (DataGridViewRow newRow in grvDefItms.Rows)
            {
                DataGridViewCheckBoxCell chk = newRow.Cells["col_Chk"] as DataGridViewCheckBoxCell;

                if (!string.IsNullOrEmpty(newRow.Cells["col_jtecclstp"].Value.ToString()))
                {
                    _techClsTp = newRow.Cells["col_jtecclstp"].Value.ToString();
                }

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _jobLine = Convert.ToInt32(newRow.Cells["col_JLine"].Value);

                    List<Service_job_Det> _select = (from _lst in _loadJobDet
                                                     where _lst.Jbd_jobline == _jobLine
                                                     select _lst).ToList();

                    if (_select.Count > 0)
                    {
                        _processJobDet.AddRange(_select);
                    }


                }
            }

            if (!string.IsNullOrEmpty(_techClsTp))
            {
                ddlCloseType.SelectedValue = _techClsTp;
            }

            _serCostSheet = CHNLSVC.CustService.ProcessJobCost(_processJobDet);
            _serJobConfDet = CHNLSVC.CustService.ProcessJobRev(_processJobDet);

            dgvCostElement.AutoGenerateColumns = false;
            dgvCostElement.DataSource = new Service_Cost_sheet();
            if (_serCostSheet.Count > 0)
            {
                dgvCostElement.DataSource = _serCostSheet;
            }

            foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
            {
                if (string.IsNullOrEmpty(_tmpConf.Jcd_cuscd))
                {
                    _tmpConf.Jcd_cuscd = lblCusCode.Text.Trim();
                    _tmpConf.Jcd_cusname = lblCusName.Text.Trim();
                    _tmpConf.Jcd_cusadd1 = lblAdd1.Text.Trim();
                    _tmpConf.Jcd_cusadd2 = lblAdd2.Text.Trim();
                    _tmpConf.Jcd_invtype = cmbInvType.Text;
                }
            }

            dgvRevElement.AutoGenerateColumns = false;
            dgvRevElement.DataSource = new List<Service_Confirm_detail>();
            if (_serJobConfDet.Count > 0)
            {
                dgvRevElement.DataSource = _serJobConfDet;
            }

            CalculateGP();

            //check stand by items
            oTempIssueList = new List<Service_TempIssue>();

            foreach (Service_job_Det _det in _processJobDet)
            {
                oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, _det.Jbd_jobno, _det.Jbd_jobline, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
            }
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            dgvItems.DataSource = oTempIssueList;

            if (oTempIssueList.Count > 0)
            {
                List<Service_Confirm_detail> _temp = new List<Service_Confirm_detail>();
                _temp = _serJobConfDet;
                foreach (Service_TempIssue _tmpIssu in oTempIssueList)
                {
                    _temp.RemoveAll(x => x.Jcd_joblineno == _tmpIssu.STI_JOBLINENO && x.Jcd_jobno == _tmpIssu.STI_JOBNO && x.Jcd_itmcd == _tmpIssu.STI_ISSUEITMCD && x.Jcd_itmstus == "STDBY" && x.Jcd_ser_id == _tmpIssu.STI_ISSUESERID);

                }
                _serJobConfDet = _temp;
                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new Service_Confirm_detail();
                dgvRevElement.DataSource = _serJobConfDet;

                List<Service_Cost_sheet> _tmpCost = new List<Service_Cost_sheet>();
                _tmpCost = _serCostSheet;
                foreach (Service_TempIssue _tmpIssu in oTempIssueList)
                {
                    _tmpCost.RemoveAll(x => x.CS_JOBLINENO == _tmpIssu.STI_JOBLINENO && x.CS_JOBNO == _tmpIssu.STI_JOBNO && x.CS_ITMCD == _tmpIssu.STI_ISSUEITMCD && x.CS_ITMSTUS == "STDBY" && x.CS_SERID == _tmpIssu.STI_ISSUESERID);
                }

                _tmpCost.ForEach(x => x.CS_CRE_BY = BaseCls.GlbUserID);

                _serCostSheet = _tmpCost;
                dgvCostElement.AutoGenerateColumns = false;
                dgvCostElement.DataSource = new Service_Cost_sheet();
                dgvCostElement.DataSource = _serCostSheet;
            }

            if (dgvItems.Rows.Count > 0)
            {

                btnStandBy.Enabled = true;
            }
            else
            {
                btnStandBy.Enabled = false;
            }

            foreach (Service_Confirm_detail _chkconf in _serJobConfDet)
            {
                if (_chkconf.Jcd_itmstus == "STDBY")
                {
                    MessageBox.Show("Stand by items available for this job with out issu to customer.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    goto L100;
                }
            }

        L100:

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10802))
            {
                chkAddCost.Enabled = false;
                dgvCostElement.Columns["col_costUnitCost"].Visible = false;
                dgvCostElement.Columns["col_costTotCost"].Visible = false;
                label7.Visible = false;
                label9.Visible = false;
                txtTotalCost.Visible = false;
                txtGP.Visible = false;
                txtGPRt.Visible = false;
            }
            else
            {
                chkAddCost.Enabled = true;
                dgvCostElement.Columns["col_costUnitCost"].Visible = true;
                dgvCostElement.Columns["col_costTotCost"].Visible = true;
                label7.Visible = true;
                label9.Visible = true;
                txtTotalCost.Visible = true;
                txtGP.Visible = true;
                txtGPRt.Visible = true;
            }

            _processStart = true;
            cmbInvType.Enabled = false;

        }

        private void grvDefItms_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvDefItms.IsCurrentCellDirty)
            {
                grvDefItms.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadPriceDefaultValue()
        { if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0) { var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList(); if (_defaultValue != null)                        if (_defaultValue.Count > 0) { DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp; DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus; LoadInvoiceType(); LoadPriceBook(cmbInvType.Text); LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim()); LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim()); CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim()); } } }

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

        //private void LoadPriceLevelMessage()
        //{
        //    DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
        //    if (_msg != null && _msg.Rows.Count > 0) lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
        //    else lblLvlMsg.Text = string.Empty;
        //}

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
                    //LoadPriceLevelMessage();
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
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

                    cmbEditBook.DataSource = _books;
                    cmbEditBook.SelectedIndex = cmbEditBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook))
                    {
                        cmbBook.Text = DefaultBook;
                        cmbEditBook.Text = DefaultBook;
                    }
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
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
                    cmbEditLvl.DataSource = _levels;
                    cmbEditLvl.SelectedIndex = cmbLevel.Items.Count - 1;

                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text))
                    {
                        cmbLevel.Text = DefaultLevel;
                        cmbEditLvl.Text = DefaultLevel;
                    }
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());
                    //LoadPriceLevelMessage();
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
                    var _types = _PriceDefinitionRef.Where(X => X.Sadd_doc_tp != "HS").Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    cmbInvType.DataSource = _types;
                    cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbInvType.Text = DefaultInvoiceType;

                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            if (_serJobConfDet.Count >= 0)
            {
                if (ucPayModes1.RecieptItemList != null)
                {
                    if (ucPayModes1.RecieptItemList.Count > 0)
                    {
                        MessageBox.Show("Already payments added.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;
                List<Service_Confirm_detail> _listWithPrice = new List<Service_Confirm_detail>();
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();

                foreach (Service_Confirm_detail _chkTak in _serJobConfDet)
                {
                    //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, "VAT", string.Empty);
                    List<MasterItemTax> _tax = null;
                    if (_isStrucBaseTax == true)       //kapila
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, string.Empty, string.Empty);

                    if (_tax.Count <= 0)
                    {
                        MessageBox.Show("Tax definition not setup for item " + _chkTak.Jcd_itmcd + " for status " + _chkTak.Jcd_itmstus + ".", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                {
                    if (!_tmpConf.IsPoItem) //Add by akila 2017/06/17
                    {
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, lblCusCode.Text, _tmpConf.Jcd_itmcd, Convert.ToDecimal(_tmpConf.Jcd_qty), Convert.ToDateTime(dtDate.Text), Convert.ToDateTime(dtDate.Value));

                        if (_priceDetailRef.Count > 0)
                        {
                            List<PriceDetailRef> _new = _priceDetailRef;
                            _priceDetailRef = new List<PriceDetailRef>();
                            var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                            if (_p != null)
                                if (_p.Count > 0)
                                    _priceDetailRef.Add(_p[0]);

                            if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                            {
                                var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                                if (_isSuspend > 0)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Price has been suspended. Please contact costing dept. Item : " + _tmpConf.Jcd_itmcd, "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;

                                }
                            }

                            if (_priceDetailRef.Count == 1)
                            {
                                var _one = from _itm in _priceDetailRef
                                           select _itm;
                                int _priceType = 0;
                                foreach (var _single in _one)
                                {
                                    _priceType = _single.Sapd_price_type;
                                    PriceTypeRef _promotion = TakePromotion(_priceType);


                                    decimal UnitPrice = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, _single.Sapd_itm_price, 0, 0, true, _tmpConf.Jcd_invtype), true);
                                    _tmpConf.Jcd_unitprice = _single.Sapd_itm_price;
                                    _tmpConf.Jcd_pbprice = _single.Sapd_itm_price;
                                    _tmpConf.Jcd_pbseqno = _single.Sapd_pb_seq;
                                    _tmpConf.Jcd_pbitmseqno = _single.Sapd_seq_no;
                                    _tmpConf.Jcd_pb = cmbBook.Text;
                                    _tmpConf.Jcd_pblvl = cmbLevel.Text;


                                    _tmpConf.Jcd_amt = FigureRoundUp(Convert.ToDecimal(_tmpConf.Jcd_unitprice) * _tmpConf.Jcd_qty, false);

                                    decimal _vatPortion = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, 0, true, _tmpConf.Jcd_invtype);
                                    //decimal _vatPortion = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, 0, true, _tmpConf.Jcd_invtype), true);
                                    _tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);

                                    decimal _totalAmount = Convert.ToDecimal(_tmpConf.Jcd_qty) * Convert.ToDecimal(_tmpConf.Jcd_unitprice);
                                    //decimal _disAmt = 0;

                                    //if (!string.IsNullOrEmpty(txtDisRate.Text))
                                    //{
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

                                    //Sanjeewa 2016-01-08
                                    if (_isCalcDisc == false)
                                    {
                                        DataTable _discRate = CHNLSVC.CustService.get_isSmartWarr(_tmpConf.Jcd_jobno, Convert.ToInt16(_tmpConf.Jcd_joblineno));

                                        if (_discRate.Rows.Count > 0)
                                        {
                                            foreach (DataRow drow in _discRate.Rows)
                                            {
                                                if (Convert.ToDecimal(drow["jbd_sw_stus"].ToString()) == 1)
                                                {
                                                    _tmpConf.Jcd_dis_rt = Convert.ToDecimal(drow["jbd_rep_perc"].ToString());
                                                }
                                            }
                                        }
                                    }

                                    //Add by akila 2017/07/19
                                    decimal _disAmt = 0;
                                    if (_isVATInvoice)
                                    {
                                        _disAmt = _totalAmount * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100);
                                    }
                                    else
                                    {
                                        _disAmt = (_totalAmount + _vatPortion) * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100);
                                    }
                                    _tmpConf.Jcd_dis = FigureRoundUp(_disAmt, true);


                                    if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                                        _vatPortion = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, string.IsNullOrEmpty(txtCommanDis.Text) ? 0 : Convert.ToDecimal(txtCommanDis.Text), false, _tmpConf.Jcd_invtype);

                                    if (_isVATInvoice) { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_vatPortion) - _disAmt, true); }
                                    //else { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_tmpConf.Jcd_tax) - _disAmt, true); } //removed  by Wimal @ 15/Sep/2018
                                    else { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_vatPortion) - _disAmt, true); } //added  by Wimal @ 15/Sep/2018
                                    _tmpConf.Jcd_net_amt = FigureRoundUp(_totalAmount, true);

                                    _tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);
                                    #region old code
                                    //if (_isVATInvoice)
                                    //    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100), true);
                                    //else
                                    //{
                                    //    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100), true);
                                    //    if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                                    //    {
                                    //        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _tmpConf.Jcd_itmcd, Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                                    //        if (_tax != null && _tax.Count > 0)
                                    //        {
                                    //            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                    //            _vatval = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(_tmpConf.Jcd_dis_rt), true, _tmpConf.Jcd_invtype);
                                    //            //_vatval = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(_tmpConf.Jcd_dis_rt), true, _tmpConf.Jcd_invtype), true);
                                    //            _tmpConf.Jcd_tax = FigureRoundUp(_vatval, true);
                                    //        }
                                    //    }
                                    //}

                                    //_tmpConf.Jcd_dis = FigureRoundUp(_disAmt, true);
                                    ////}

                                    ////if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                                    ////{

                                    //if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                                    //    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                                    //else
                                    //    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_tmpConf.Jcd_tax) - _disAmt, true);
                                    ////}

                                    //_tmpConf.Jcd_net_amt = FigureRoundUp(_totalAmount, true);
                                    #endregion
                                }
                            }
                            else   //kapila  16/6/2017
                            {
                                if (_tmpConf.Jcd_unitprice > 0)
                                {
                                    _tmpConf.Jcd_pb = cmbBook.Text;
                                    _tmpConf.Jcd_pblvl = cmbLevel.Text;
                                }
                            }

                        }
                        _listWithPrice.Add(_tmpConf);
                    }
                    else 
                    {
                        _tmpConf.Jcd_pb = "PO";
                        _tmpConf.Jcd_pblvl = "PO";
                        _listWithPrice.Add(_tmpConf); 
                    }
                }
                _serJobConfDet = new List<Service_Confirm_detail>();
                _serJobConfDet = _listWithPrice;
                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new List<Service_Confirm_detail>();
                dgvRevElement.DataSource = _serJobConfDet;

                CalculateGP();
                Cursor.Current = Cursors.Default;
            }
        }

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
                    _masterPCTax = CHNLSVC.Sales.GetPcTax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, 1, dtDate.Value.Date);

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

                            if (dtDate.Value.Date == _serverDt)
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
                                _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtDate.Value.Date);
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

                                    _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtDate.Value.Date);
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
                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "NBT", string.Empty, dtDate.Value.Date);
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
                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, dtDate.Value.Date);
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

        //private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxfaction)
        //{
        //    try
        //    {

        //        if (_priceBookLevelRef != null)
        //            if (_priceBookLevelRef.Sapl_vat_calc)
        //            {
        //                List<MasterItemTax> _taxs = new List<MasterItemTax>();
        //                if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
        //                var _Tax = from _itm in _taxs
        //                           select _itm;
        //                foreach (MasterItemTax _one in _Tax)
        //                {
        //                    //if (lblVatExemptStatus.Text != "Available")
        //                    //{
        //                    //  if (_isTaxfaction == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
        //                    if (_isTaxfaction == false)
        //                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
        //                    else
        //                        if (chkTaxPayable.Checked == true)
        //                        {
        //                            _discount = _pbUnitPrice * _qty * 0 / 100;//Convert.ToDecimal(txtDisRate.Text) / 100;
        //                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
        //                        }
        //                        else
        //                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;

        //                    //}
        //                    //else
        //                    //{
        //                    //    if (_isTaxfaction) _pbUnitPrice = 0;
        //                    //}
        //                }
        //            }
        //            else
        //                if (_isTaxfaction) _pbUnitPrice = 0;


        //        return _pbUnitPrice;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
        //        return 0;
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            //if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value + Convert.ToDecimal(.49)), 2);


            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return RoundUpForPlace(value, 2);
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

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
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
        }

        private void cmbEditBook_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadPriceLevel(cmbInvType.Text, cmbEditBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbEditBook.Text.Trim(), cmbEditLvl.Text.Trim());
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

        private void cmbBook_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadPriceLevel(cmbInvType.Text, cmbEditBook.Text);
                // LoadLevelStatus(cmbInvType.Text.Trim(), cmbEditBook.Text.Trim(), cmbEditLvl.Text.Trim());

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

        private void cmbEditLvl_Leave(object sender, EventArgs e)
        {
            try
            {
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbEditBook.Text.Trim(), cmbEditLvl.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbEditBook.Text.Trim(), cmbEditLvl.Text.Trim());
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

        private void dgvRevElement_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (chkEditItm.Checked == true)
            //{
            //    string _mainItm = "";
            //    Int16 _mainLine = 0;
            //    string _jobNo = "";
            //    decimal _mainQty = 0;
            //    string _jobItm = "";

            //    txtItem.Text = "";
            //    txtQty.Text = "";
            //    txtEditJobLine.Text = "";
            //    txtEditJobNo.Text = "";
            //    txtJobItem.Text = "";

            //    _mainItm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_itmcd"].Value.ToString();
            //    _mainLine = Convert.ToInt16(dgvRevElement.Rows[e.RowIndex].Cells["jcd_joblineno"].Value);
            //    _mainQty = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_qty"].Value);
            //    _jobNo = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobno"].Value.ToString();
            //    _jobItm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobitmcd"].Value.ToString();

            //    if (!LoadItemDetail(_mainItm))
            //    {
            //        MessageBox.Show("Invalid item selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    txtItem.Text = _mainItm;
            //    txtQty.Text = _mainQty.ToString();
            //    txtEditJobNo.Text = _jobNo;
            //    txtEditJobLine.Text = _mainLine.ToString();
            //    txtJobItem.Text = _jobItm;
            //    cmbEditBook.Focus();
            //    txtItem.ReadOnly = true;
            //    txtQty.ReadOnly = true;
            //    pnlEdit.Visible = true;
            //    //lblLine.Text = _mainLine.ToString();
            //    //lblQty.Text = _mainQty.ToString();
            //}
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

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), false)));
                //txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

                decimal _vatPortion = TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtDisRate.Text), true, cmbInvType.Text.Trim());
                //decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtDisRate.Text), true, cmbInvType.Text.Trim()), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);

                //Sanjeewa 2016-01-08
                DataTable _discRate = CHNLSVC.CustService.get_isSmartWarr(txtEditJobNo.Text.Trim(), Convert.ToInt16(Convert.ToInt16(txtEditJobLine.Text)));

                if (_discRate.Rows.Count > 0)
                {
                    foreach (DataRow drow in _discRate.Rows)
                    {
                        if (Convert.ToDecimal(drow["jbd_sw_stus"].ToString()) == 1)
                        {
                            txtDisRate.Text = drow["jbd_rep_perc"].ToString();
                        }
                    }
                }

                bool _isVATInvoice = false;
                if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                else _isVATInvoice = false;

                decimal _disAmt = 0;
                if (Convert.ToDecimal(txtDisRate.Text) == 100)
                {
                    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                }
                else 
                {
                    if (_isVATInvoice)
                    {
                        _disAmt = _totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100);
                    }
                    else
                    {
                        _disAmt = (_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100);
                    }
                    //_disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                }

                //_disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                //_disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);

                //if (!string.IsNullOrEmpty(txtDisRate.Text))
                //{
                //    //_disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100));
                //    //txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                //    //bool _isVATInvoice = false;
                //    //if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                //    //else _isVATInvoice = false;

                //    //if (_isVATInvoice)
                //    //    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                //    //else
                //    //    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), false);
                //    //txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                //    bool _isVATInvoice = false;
                //    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                //    else _isVATInvoice = false;

                //    if (_isVATInvoice)
                //        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                //    else
                //    {
                //        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                //        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                //        {
                //            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                //            if (_tax != null && _tax.Count > 0)
                //            {
                //                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                //                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                //            }
                //        }
                //    }

                txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                //}

                if (_disAmt >0)
                {
                    _vatPortion = TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtDisRate.Text), false, cmbInvType.Text.Trim());
                    //_vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtDisRate.Text), false, cmbInvType.Text.Trim()), true);
                }

                //if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                //{
                //    //  _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                //    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                //        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                //    else
                //updated by akila 2017/07/10
                if (_vatPortion > 0) { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true); }
                else if (_vatPortion == 0) { _totalAmount = FigureRoundUp(_totalAmount - _disAmt, true); }
                //_totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);

                //_totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                //}
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));
                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));

                //if (_isVATInvoice) { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_vatPortion) - _disAmt, true); }
                //else { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_tmpConf.Jcd_tax) - _disAmt, true); }
                //_tmpConf.Jcd_net_amt = FigureRoundUp(_totalAmount, true);

                //_tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);
            }

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

            if (string.IsNullOrEmpty(lblCusCode.Text))
            {
                MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbEditBook.Text))
            {
                MessageBox.Show("Price book not select.", "Invalid Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbEditLvl.Text))
            {
                MessageBox.Show("Please select the price level", "Invalid Level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbEditLvl.Focus();
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
                //Updated bu akila 2017/06/09
                List<MasterItemTax> _tax = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                }
                else
                {
                    _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);
                }

                if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    _IsTerminate = true;
                if (_tax.Count <= 0)
                    _IsTerminate = true;

                //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);
                //if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                //    _IsTerminate = true;
                //if (_tax.Count <= 0)
                //    _IsTerminate = true;
            }
            return _IsTerminate;
        }

        private void CheckItemTax(string _item)
        {

            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
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
        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
           // txtQty.Text = FormatToQty("1");   //comented by kapila on 31/7/2017
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

                        _itmType = _itemdetail.Mi_itm_tp;
                        _isMinus = _itemdetail.Mi_anal4;

                        //if (_itmType == "V")
                        //{
                        //    txtUnitPrice.ReadOnly = false;
                        //}
                        //else
                        //{
                        //    txtUnitPrice.ReadOnly = true;
                        //}

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
                _priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbEditBook.Text, cmbEditLvl.Text, lblCusCode.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtDate.Value), Convert.ToDateTime(dtDate.Value));
                //Add by akila 2017/06/24

                if ((_priceDetailRef == null) || (_priceDetailRef.Count < 1))
                {
                    IsPriceBookDetAvailable = false;
                }
                else { IsPriceBookDetAvailable = true; }

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
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                                _priceDetailRef.Add(_p[0]);
                    }
                    //Inventory Combine Item -------------------------------

                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            MessageBox.Show("Price has been suspended. Please contact costing dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                    }

                    if (_priceDetailRef.Count > 1)
                    {
                        //Find More than one price for the selected item
                        //Load prices for the grid and popup for user confirmation

                        //IsNoPriceDefinition = false;
                        //comment by darshana only for this___________________________
                        //SetColumnForPriceDetailNPromotion(false);
                        //BindNonSerializedPrice(_priceDetailRef);
                        //pnlPriceNPromotion.Visible = true;
                        //____________________________________________________________
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
                            //decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtDisRate.Text), false), true);
                            decimal UnitPrice = _single.Sapd_itm_price;
                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));

                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;

                            //If Promotion Available
                            if (_promotion.Sarpt_is_com)
                            {
                                // SetColumnForPriceDetailNPromotion(false);
                                // gvNormalPrice.DataSource = new List<PriceDetailRef>();
                                // gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                                //  gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                                //  BindNonSerializedPrice(_priceDetailRef);
                                // gvPromotionPrice_CellDoubleClick(0, false);
                                //  pnlPriceNPromotion.Visible = true;
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

        private void cmbEditBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbEditLvl.Focus();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
            else if (e.KeyCode == Keys.F2)
            {
                if (chkEditItm.Checked == false)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItem;
                    _CommonSearch.ShowDialog();
                    txtItem.Select();
                }
            }
        }

        private void cmbEditLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
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
                btnAddItem.Focus();
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

                    if ((!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false) && (!IsPriceBookDetAvailable)) // updated by akila 2017/06/24
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

                        //Updated by akila 2017/06/01
                        if (_pb_price > 0)
                        {
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
                        else
                        {
                            if (_itmType != "V")
                            {
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                                _isEditPrice = false;
                            }
                            else { _isEditPrice = true; }
                        }

                        ////Commented by akila 2017/06/01
                        //if (_MasterProfitCenter.Mpc_edit_price)
                        //{

                        //    if (_pb_price > _txtUprice)
                        //    {
                        //        decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                        //        if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                        //        {
                        //            MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                        //            _isEditPrice = false;
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            _isEditPrice = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (_itmType != "V")
                        //    {
                        //        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                        //    }
                        //    _isEditPrice = false;
                        //}
                    }
                    else
                    {
                        if (_itmType == "V" && IsPriceBookDetAvailable)
                        {
                            decimal _pb_price;
                            _pb_price = SSPriceBookPrice;

                            decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);

                            if (_pb_price > 0)
                            {
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
                            else
                            {
                                if (_itmType != "V")
                                {
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                                    _isEditPrice = false;
                                }
                                else { _isEditPrice = true; }
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);

                if (_itmType == "V" && _isMinus == true)
                {
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

                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    MessageBox.Show("Please enter valid discount rate.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Text = "0.00";
                    txtDisRate.Focus();
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
                        //Sanjeewa 2016-01-08
                        DataTable _discRate = CHNLSVC.CustService.get_isSmartWarr(txtEditJobNo.Text.Trim(), Convert.ToInt16(Convert.ToInt16(txtEditJobLine.Text)));
                        decimal _swdisc = 0;
                        if (_discRate.Rows.Count > 0)
                        {
                            foreach (DataRow drow in _discRate.Rows)
                            {
                                if (Convert.ToDecimal(drow["jbd_sw_stus"].ToString()) == 1)
                                {
                                    _swdisc = Convert.ToDecimal(drow["jbd_rep_perc"].ToString());
                                }
                            }
                        }

                        if (_swdisc <= 0)
                        {
                            ManagerDiscount = CHNLSVC.Sales.GetGeneralEntityDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtDate.Value).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCusCode.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
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
        private void txtUnitAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUnitAmt.Text)) { return; }

                if (!IsNumeric(txtUnitAmt.Text))
                {
                    MessageBox.Show("Invalid unit amount", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (ucPayModes1.RecieptItemList != null)
            {
                if (ucPayModes1.RecieptItemList.Count > 0)
                {
                    MessageBox.Show("You have already added payment.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (chkEditItm.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtLineTotAmt.Text))
                {
                    if (Convert.ToDecimal(txtLineTotAmt.Text) <= 0)
                    {
                        MessageBox.Show("Price cannot be zero.", "Total amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtQty.Focus();
                        return;
                    }

                    foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                    {
                        //if (_tmpConf.Jcd_jobno == txtEditJobNo.Text.Trim() && _tmpConf.Jcd_joblineno == Convert.ToInt16(txtEditJobLine.Text) && _tmpConf.Jcd_itmcd == txtItem.Text.Trim())
                        if (_tmpConf.Jcd_jobno == txtEditJobNo.Text.Trim() && _tmpConf.Jcd_line == Convert.ToInt16(txtEditJobLine.Text) && _tmpConf.Jcd_itmcd == txtItem.Text.Trim()) //updated by akila 2017/06/27
                        {
                            _tmpConf.Jcd_unitprice = Convert.ToDecimal(txtUnitPrice.Text);
                            _tmpConf.Jcd_amt = Convert.ToDecimal(txtUnitAmt.Text);
                            _tmpConf.Jcd_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                            _tmpConf.Jcd_dis = Convert.ToDecimal(txtDisAmt.Text);
                            _tmpConf.Jcd_tax = Convert.ToDecimal(txtTaxAmt.Text);
                            _tmpConf.Jcd_net_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                            _tmpConf.Jcd_pb = cmbEditBook.Text;
                            _tmpConf.Jcd_pblvl = cmbEditLvl.Text;
                            _tmpConf.Jcd_invtype = cmbInvType.Text;
                            _tmpConf.Jcd_itmdesc = lblIDesc.Text;
                            
                        }
                    }

                    dgvRevElement.AutoGenerateColumns = false;
                    dgvRevElement.DataSource = new List<Service_Confirm_detail>();
                    dgvRevElement.DataSource = _serJobConfDet;
                    SetDecimalTextBoxForZero(true, true);
                    txtEditJobLine.Text = "";
                    txtEditJobNo.Text = "";
                    txtItem.Text = "";
                    txtJobItem.Text = "";
                    lblIDesc.Text = "";
                    lblImodel.Text = "";
                    lblIBrand.Text = "";
                    chkEditItm.Checked = false;
                    pnlEdit.Visible = false;
                    CalculateGP();
                }
            }
            else if (chkAddNewRev.Checked == true)
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please select revenue item.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLineTotAmt.Text))
                {
                    MessageBox.Show("Please enter valid price.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter valid qty.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text) <= 0)
                {
                    MessageBox.Show("Please enter valid qty.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    MessageBox.Show("Please enter valid price.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(SSPriceBookSequance))
                {
                    MessageBox.Show("Please enter valid price.", "Unit amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }



                Service_Confirm_detail _addConf = new Service_Confirm_detail();
                MasterItem _itmMas = new MasterItem();
                _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                _addConf.Jcd_amt = Convert.ToDecimal(txtUnitAmt.Text);
                _addConf.Jcd_balqty = Convert.ToDecimal(txtQty.Text);
                _addConf.Jcd_batchline = 0;
                _addConf.Jcd_dis = Convert.ToDecimal(txtDisAmt.Text);
                _addConf.Jcd_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                _addConf.Jcd_invtype = cmbInvType.Text;
                _addConf.Jcd_itmbrand = _itmMas.Mi_brand;
                _addConf.Jcd_itmcd = txtItem.Text.Trim();
                _addConf.Jcd_itmdesc = lblIDesc.Text.Trim();
                _addConf.Jcd_itmline = 0;
                _addConf.Jcd_itmmodel = _itmMas.Mi_model;
                _addConf.Jcd_itmstus = cmbStatus.Text;
                _addConf.Jcd_itmtp = _itmMas.Mi_itm_tp;
                _addConf.Jcd_itmuom = _itmMas.Mi_itm_uom;
                _addConf.Jcd_jobitmcd = txtJobItem.Text.Trim();
                _addConf.Jcd_jobitmser = txtjobSer.Text.Trim();
                _addConf.Jcd_joblineno = Convert.ToInt32(txtEditJobLine.Text);
                _addConf.Jcd_jobno = txtEditJobNo.Text;
                _addConf.Jcd_jobwarrno = txtjobWara.Text;
                _addConf.Jcd_line = _serJobConfDet.Count + 1;
                _addConf.Jcd_net_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                _addConf.Jcd_pb = cmbEditBook.Text;
                _addConf.Jcd_pblvl = cmbEditLvl.Text;
                _addConf.Jcd_pbprice = SSPriceBookPrice;
                _addConf.Jcd_pbseqno = Convert.ToInt32(SSPriceBookSequance);
                _addConf.Jcd_qty = Convert.ToDecimal(txtQty.Text);
                _addConf.Jcd_rmk = "ADD";
                _addConf.Jcd_tax = Convert.ToDecimal(txtTaxAmt.Text);
                _addConf.Jcd_tax_rt = 0;
                _addConf.Jcd_unitprice = Convert.ToDecimal(txtUnitPrice.Text);
                _addConf.WarrantyRemark = WarrantyRemarks;
                _addConf.WarrantyRepirod = WarrantyPeriod;
                _addConf.Jcd_cuscd = lblCusCode.Text.Trim();
                _addConf.Jcd_cusname = lblCusName.Text.Trim();
                _addConf.Jcd_cusadd1 = lblAdd1.Text.Trim();
                _addConf.Jcd_cusadd2 = lblAdd2.Text.Trim();
                _serJobConfDet.Add(_addConf);

                Service_JOB_HDR _JobHdr = new Service_JOB_HDR();
                _JobHdr = CHNLSVC.CustService.GET_SCV_JOB_HDR(txtJobNo.Text.Trim(), BaseCls.GlbUserComCode);

                decimal _rate = CHNLSVC.CustService.GetScvJobStageCost(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, BaseCls.GlbUserDefLoca, _JobHdr.SJB_JOBCAT, Convert.ToDecimal(txtQty.Text), dtDate.Value.Date, "7", txtItem.Text);
                if (_rate != -1)
                {
                    Service_Cost_sheet _tmpcostSheet = new Service_Cost_sheet();
                    _tmpcostSheet.CS_ACT = 1;
                    _tmpcostSheet.CS_BALQTY = Convert.ToDecimal(txtQty.Text);
                    _tmpcostSheet.CS_COM = BaseCls.GlbUserComCode;
                    _tmpcostSheet.CS_CRE_BY = BaseCls.GlbUserID;
                    _tmpcostSheet.CS_CUSCD = lblCusCode.Text.Trim();
                    _tmpcostSheet.CS_CUSNAME = lblCusName.Text.Trim();
                    _tmpcostSheet.CS_DIRECT = "IN";
                    _tmpcostSheet.CS_DOCDT = dtDate.Value.Date;
                    _tmpcostSheet.CS_DOCQTY = Convert.ToDecimal(txtQty.Text);
                    _tmpcostSheet.CS_ITMCD = txtItem.Text;
                    _tmpcostSheet.CS_ITMDESC = _itmMas.Mi_shortdesc;
                    _tmpcostSheet.CS_ITMTP = _itmMas.Mi_itm_tp;
                    _tmpcostSheet.CS_JOBCLOSE = 1;
                    _tmpcostSheet.CS_JOBITMCD = txtJobItem.Text.Trim();
                    _tmpcostSheet.CS_JOBITMSER = txtjobSer.Text;
                    _tmpcostSheet.CS_JOBITMWARR = txtjobWara.Text;
                    _tmpcostSheet.CS_JOBLINENO = Convert.ToInt32(txtEditJobLine.Text);
                    _tmpcostSheet.CS_JOBNO = txtJobNo.Text.Trim();
                    _tmpcostSheet.CS_LINE = _serCostSheet.Count + 1;
                    _tmpcostSheet.CS_LOC = BaseCls.GlbUserDefLoca;
                    _tmpcostSheet.CS_PC = BaseCls.GlbUserDefProf;
                    _tmpcostSheet.CS_QTY = Convert.ToDecimal(txtQty.Text);
                    _tmpcostSheet.CS_TOTUNITCOST = Convert.ToDecimal(_rate) * Convert.ToDecimal(txtQty.Text);
                    _tmpcostSheet.CS_UNITCOST = Convert.ToDecimal(_rate);
                    _tmpcostSheet.CS_UOM = _itmMas.Mi_itm_uom;
                    _tmpcostSheet.CS_DOCTP = "ADD";

                    _serCostSheet.Add(_tmpcostSheet);

                    dgvCostElement.AutoGenerateColumns = false;
                    dgvCostElement.DataSource = new Service_Cost_sheet();
                    dgvCostElement.DataSource = _serCostSheet;
                }

                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new List<Service_Confirm_detail>();
                dgvRevElement.DataSource = _serJobConfDet;
                SetDecimalTextBoxForZero(true, true);
                txtEditJobLine.Text = "";
                txtEditJobNo.Text = "";
                txtItem.Text = "";
                txtJobItem.Text = "";
                lblIDesc.Text = "";
                lblIDesc.ReadOnly = true;
                lblImodel.Text = "";
                lblIBrand.Text = "";
                pnlEdit.Visible = false;
                CalculateGP();
            }
        }

        private Int32 getPendingMRNCount(String jobNum, Int32 jobLine)
        {
            int RecCount = 0;

            try
            {
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Ith_doc_tp = "AOD";
                _inventoryRequest.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryRequest.FromDate = "01-01-1900";
                _inventoryRequest.ToDate = "31-12-2999";

                DataTable dtTemp = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                if (dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").Length > 0)
                {
                    DataTable dtNew = dtTemp.Select("ith_direct = 0 AND ith_job_no='" + jobNum + "'").CopyToDataTable();
                    if (dtNew.Rows.Count > 0)
                    {
                        RecCount = dtNew.Rows.Count;
                    }
                    else
                    {
                        return RecCount;
                    }
                }
                else
                {
                    return RecCount;

                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return RecCount;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Service_confirm_Header _saveConfHdr = new Service_confirm_Header();
                List<Service_confirm_Header> _finalConfHdr = new List<Service_confirm_Header>();
                InvoiceHeader _invheader = new InvoiceHeader();
                List<InvoiceItem> _invItem = new List<InvoiceItem>();
                RecieptHeader _recHeader = new RecieptHeader();
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                InventoryHeader invHdr = new InventoryHeader();
                InventoryHeader _aodHdr = new InventoryHeader();
                List<ReptPickSerials> _AodserialList = new List<ReptPickSerials>();
                MasterAutoNumber _AodAuto = new MasterAutoNumber();
                RCC _rcc = new RCC();
                Boolean _isRcc = false;
                Boolean _isStockUpdate = false;
                Int32 _jobLine = 0;
                string _msg = "";
                string _invoicePrefix = "";
                Boolean _priceAvailable = true;
                string _rccpc = string.Empty;
                string _rccNo = "";
                Int32 _isFgap = 0;
                Int32 _isStk = 0;

                //add by akila 2017/08/21
                //if (!ValidateCustomer(lblCusCode.Text.ToUpper().Trim(),cmbInvType.Text))
                //{
                //    return;
                //}

                if (_processStart == false)
                {
                    MessageBox.Show("Please process cost and revenue calculation before confirm the job.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (CheckServerDateTime() == false) return;

              

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtDate, lblBackDateInfor, dtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtDate.Value.Date != DateTime.Now.Date)
                        {
                            dtDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtDate.Focus();
                        return;
                    }
                }
                #region  check credit invoice allowe validation add by tharanga 2018/09/12
                MasterBusinessEntity _masterBusinessCompany = null;
                if (!string.IsNullOrEmpty(lblCusCode.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(lblCusCode.Text, null, null, null, null, BaseCls.GlbUserComCode);
                if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd)) 
                {
                    Int32 eff = CHNLSVC.General.save_busentity_newcom(BaseCls.GlbUserComCode, lblCusCode.Text);
                }


                if (!IsValidCustomer(lblCusCode.Text.Trim(), cmbInvType.SelectedValue.ToString()))
                {
                    return;
                }
                if ((!string.IsNullOrEmpty(lblCusCode.Text.Trim())) && (lblCusCode.Text != "CASH") && (BaseCls.GlbUserComCode != "AAL"))
                {
                    if (!IsCustCreditLimitIsValid())
                    {
                        return;
                    }
                }
                #endregion

                if (chkInvNow.Checked)
                {
                    var _distinctCusCnt = (from n in _serJobConfDet
                                           group n by new { n.Jcd_cuscd, n.Jcd_cusname, n.Jcd_cusadd1, n.Jcd_cusadd2, n.Jcd_invtype } into r
                                           select new { Jcd_cuscd = r.Key.Jcd_cuscd, Jcd_cusname = r.Key.Jcd_cusname, Jcd_cusadd1 = r.Key.Jcd_cusadd1, Jcd_cusadd2 = r.Key.Jcd_cusadd2, Jcd_invtype = r.Key.Jcd_invtype }).ToList();
                    if (_distinctCusCnt.Count > 1)
                    {
                        MessageBox.Show("Invoice now option can't be use for mutiple job confirmations", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (_isStandBy == false)
                {
                    if (dgvItems.Rows.Count <= 0)
                    {
                        foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                        {
                            if (_tmpConf.Jcd_itmstus == "STDBY")
                            {
                                if (MessageBox.Show("Stand by items available for this job. Pls. confirm.", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                            }

                        }
                    }
                }

                Int32 pendingCount = 0;

                foreach (Service_job_Det _det in _processJobDet)
                {
                    pendingCount = getPendingMRNCount(_det.Jbd_jobno, _det.Jbd_jobline);

                    if (pendingCount > 0)
                    {
                        MessageBox.Show("Pending MRN available for this job item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (chkInvNow.Checked == true)
                {
                    if (_isStandBy == false)
                    {
                        oTempIssueList = new List<Service_TempIssue>();

                        foreach (Service_job_Det _det in _processJobDet)
                        {
                            oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, _det.Jbd_jobno, _det.Jbd_jobline, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
                        }
                        dgvItems.AutoGenerateColumns = false;
                        dgvItems.DataSource = new List<Service_TempIssue>();
                        dgvItems.DataSource = oTempIssueList;

                        if (dgvItems.Rows.Count > 0)
                        {
                            MessageBox.Show("Unsettle stand by items available.Please confirm add those items to invoice", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            pnlStandByItems.Show();
                            return;
                        }

                    }
                }

                if (dgvRevElement.Rows.Count <= 0)
                {
                    _serJobConfDet = new List<Service_Confirm_detail>();
                    if ((cmbInvType.Text != "DEBT") && (cmbInvType.Text != "HEDN") && (cmbInvType.Text != "AADN") && (cmbInvType.Text != "TVDN") && (cmbInvType.Text != "TVFS")) //updated by akila 2017/06/22 , 2017/08/03
                    {
                        MessageBox.Show("You cannot confirm with out revenue items for selected invoice type.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        chkInvNow.Checked = true;
                    }


                    if (MessageBox.Show("You are going to confirm the job with out revenue items. Pls. confirm ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (_isStandBy == false)
                        {
                            oTempIssueList = new List<Service_TempIssue>();

                            foreach (Service_job_Det _det in _processJobDet)
                            {
                                oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, _det.Jbd_jobno, _det.Jbd_jobline, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
                            }
                            dgvItems.AutoGenerateColumns = false;
                            dgvItems.DataSource = new List<Service_TempIssue>();
                            dgvItems.DataSource = oTempIssueList;

                            if (dgvItems.Rows.Count > 0)
                            {
                                MessageBox.Show("Unsettle stand by items available.Please confirm add those items to invoice", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                pnlStandByItems.Show();
                                return;
                            }

                        }
                    }
                }
                else
                {
                    foreach (Service_Confirm_detail _chkConf in _serJobConfDet)
                    {
                        if (string.IsNullOrEmpty(_chkConf.Jcd_pb) || _chkConf.Jcd_pb == "N/A")
                        {
                            foreach (Service_job_Det _tmp in _processJobDet)
                            {
                                if (_tmp.Jbd_jobline == _chkConf.Jcd_joblineno && _tmp.Jbd_jobno == _chkConf.Jcd_jobno)
                                {
                                    if (_tmp.Jbd_warr_stus == 0)
                                    {
                                        _priceAvailable = false;
                                        goto L1;
                                    }
                                    else
                                    {
                                        _chkConf.Jcd_pb = "N/A";
                                        _chkConf.Jcd_pblvl = "N/A";
                                        //chkInvNow.Checked = true;
                                    }
                                }
                            }

                        }
                    }
                }
            L1:

                if (_priceAvailable == false)
                {
                    MessageBox.Show("Job item is over warranty and cannot confirm as FOC.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (_processJobDet.Count <= 0)
                {
                    MessageBox.Show("Cannot find process job item details.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (Service_job_Det _det in _processJobDet)
                {
                    List<Service_job_Det> oJobitms = new List<Service_job_Det>();
                    oJobitms = CHNLSVC.CustService.GetJobDetails(_det.Jbd_jobno, _det.Jbd_jobline, BaseCls.GlbUserComCode);

                    foreach (Service_job_Det _newDet in oJobitms)
                    {
                        if (_newDet.Jbd_stage >= 7)
                        {
                            MessageBox.Show("Cannot confirm. Please check the job stage.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        _isFgap = _newDet.Jbd_is_fgap;
                        _isStk = _newDet.jbd_isstockupdate;
                    }

                    // CHECK ALREADY CONFIRM DETAILS AVAILABILITY
                    List<Service_confirm_Header> _exHdr = new List<Service_confirm_Header>();
                    _exHdr = CHNLSVC.CustService.GetConfDetByJobNo(BaseCls.GlbUserComCode, _det.Jbd_jobno, _det.Jbd_jobline);

                    if (_exHdr != null)
                    {
                        foreach (Service_confirm_Header _exDet in _exHdr)
                        {
                            MessageBox.Show("Already confirm confirmation available for selected job item. Pls. cancel and re-process. Confirmation # : " + _exDet.Jch_no, "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

                if (Convert.ToDecimal(txtTotRev.Text) <= 0)
                {
                    if ((cmbInvType.Text != "DEBT") && (cmbInvType.Text != "HEDN") && (cmbInvType.Text != "AADN") && (cmbInvType.Text != "TVDN") && (cmbInvType.Text != "TVFS")) //updated by akila 2017/06/22 , 2017/08/03
                    //if (cmbInvType.Text != "DEBT") //updated by akila 2017/06/22
                    {
                        MessageBox.Show("You cannot confirm as zero for selected invoice type.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                //   Nadeeka
                #region chk Arrears
                decimal _arrblk = 0;
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ARRBLK", "COM", BaseCls.GlbUserComCode);
                if (para.Count > 0)
                {
                    _arrblk = para[0].Hsy_val;
                }

                
                //commented by akila 2017/10/18
                //foreach (Service_Confirm_detail conitem in _serJobConfDet)
                //{

                if (_serJobConfDet != null && _serJobConfDet.Count > 0)
                {
                    var _distinctJobs = _serJobConfDet.GroupBy(x => new { x.Jcd_jobno, x.Jcd_joblineno }).Select(grp => grp.Key).ToList();
                    if (_distinctJobs != null && _distinctJobs.Count > 0)
                    {
                        foreach (var element in _distinctJobs)
                        {
                            List<Service_job_Det> _JobDet = new List<Service_job_Det>();
                            //foreach (Service_Confirm_detail item in oMainDetailList)
                            //{
                            _JobDet = CHNLSVC.CustService.GetJobDetails(element.Jcd_jobno.ToString(), Convert.ToInt32(element.Jcd_joblineno), BaseCls.GlbUserComCode);
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
                                                    // DateTime dt1 = GetLastDayOfPreviousMonth(dtDate.Value.Date);
                                                    DateTime dt1 = GetLastDayOfPreviousMonth(_jobDt.Date);
                                                    decimal _arrears = CHNLSVC.Sales.Get_Acc_Arrears(_invHdr[0].Sah_acc_no, dt1, null);
                                                    if (_arrears > 0)
                                                    {
                                                        if (chkInvNow.Checked == true)
                                                        {
                                                            if (_arrblk > 0)
                                                            {
                                                                MessageBox.Show(" Please  settle the arrears for Hp Account No  : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " and  Arrears Amount : " + _arrears, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return;
                                                            }

                                                            else
                                                            {
                                                                // MessageBox.Show(" Pls settle the arrears for hp account # : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " Arrears Amount : " + _arrears, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                if (MessageBox.Show("Please settle the arrears for Hp Account No : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " and  Arrears Amount : " + _arrears + " , Do you want to continue this ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                                                {
                                                                    return;
                                                                }
                                                                // return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (MessageBox.Show(" Please settle the arrears for Hp Account No : " + _invHdr[0].Sah_acc_no + " Invoice No :" + jitem.Jbd_invc_no + " and  Arrears Amount : " + _arrears + " , Do you want to continue this ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                                            {
                                                                return;
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                    
                //}
                #endregion


                if (chkInvNow.Checked == true)
                {
                    if (Convert.ToDecimal(txtTotRev.Text) > 0)
                    {
                        if (cmbInvType.Text == "CS")
                            if (ucPayModes1.RecieptItemList == null)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        if (cmbInvType.Text == "CS")
                            if (ucPayModes1.RecieptItemList != null)
                                if (ucPayModes1.RecieptItemList.Count <= 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                    }
                    _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);
                    if (string.IsNullOrEmpty(_invoicePrefix))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    foreach (Service_Confirm_detail _chkTak in _serJobConfDet)
                    {
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, "VAT", string.Empty);
                        if (_isStrucBaseTax == true)
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd);
                            _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, "VAT", string.Empty, _mstItem.Mi_anal1);
                        }
                        else
                            _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _chkTak.Jcd_itmcd, _chkTak.Jcd_itmstus, "VAT", string.Empty);
                        if (_tax.Count <= 0)
                        {
                            MessageBox.Show("Tax definition not setup for item " + _chkTak.Jcd_itmcd + " for status " + _chkTak.Jcd_itmstus + ".", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                if (string.IsNullOrEmpty(ddlCloseType.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select job close type.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlCloseType.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(ddlCloseType.Text))
                {
                    int _mpcb = 0;
                    if (ddlCloseType.SelectedValue.ToString() == "MPCB")
                    {
                        foreach (Service_Confirm_detail _tmpSerJob in _serJobConfDet)
                        {
                            MasterItem _itm = new MasterItem();
                            _itm = CHNLSVC.General.GetItemMaster(_tmpSerJob.Jcd_itmcd);

                            if (_itm != null)
                            {
                                if (_itm.Mi_cate_1 == "PHACC" | _itm.Mi_cate_1 == "MOACC" | _itm.Mi_cate_1 == "MPCB")
                                {
                                    _mpcb = 1;
                                }
                            }
                        }
                        if (_mpcb != 1)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("There is no Main PCB Replaced. If you have replaced Main PCB, Please check the Item category Definition with Inventory Department.", "MPCB Replacement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ddlCloseType.Focus();
                            return;
                        }
                    }
                }

                //kapila 11/7/2017
                //if (!string.IsNullOrEmpty(cmbInvType.Text))
                //{
                //    List<MasterInvoiceType> _lstSaleType = CHNLSVC.Sales.GetInvoiceTypeByCode(cmbInvType.Text);
                //    if (_lstSaleType != null)
                //    {
                //        if (_lstSaleType[0].Srtp_main_tp == "CREDIT")
                //        {
                //            DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, lblCusCode.Text.Trim());
                //            if (_table != null && _table.Rows.Count > 0)
                //            {
                //                var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                //                if (_isAvailable == null || _isAvailable.Count <= 0)
                //                {
                //                    MessageBox.Show("Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    lblCusCode.Text = "";
                //                    return;
                //                }
                //            }
                //        }
                //    }
                //}

                Boolean _isOverWarra = false;
                Boolean _isWithOutInv = false;
                Boolean _withoutWarRep = false;
                string _salesInvoice = string.Empty;
                //  if (ddlCloseType.SelectedValue.ToString()  ==Convert.ToString( "WRPL"))

                if (!string.IsNullOrEmpty(ddlCloseType.Text))
                {
                    List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel).Where(X => X.SCT_TP == ddlCloseType.SelectedValue.ToString()).ToList();
                    if (oCloseType != null)
                    {
                        if (oCloseType[0].SCT_REPL_ALLOW == 1)//Nadeeka 24-08-2015
                        {
                            _isRplAllow = true;
                        }
                        else
                        { _isRplAllow = false; }
                    }
                }

                Boolean _isDisc = false;
                if (_isRplAllow == true) // Sanjeewa 2016-01-14
                {
                    foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                    {
                        if (_tmpConf.Jcd_dis_rt > 0)
                        {
                            if (MessageBox.Show("Discount is not allowed for Warranty Replacements. Do you want to Reset Discount?", "Discount not Allowed", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                            {
                                _isDisc = true;
                                goto D1;
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                ddlCloseType.Focus();
                                return;
                            }
                        }
                    }
                }
            D1:
                if (_isDisc == true)
                {
                    foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                    {
                        _isCalcDisc = true;
                        txtCommanDis.Text = "0";
                        btnDisApply_Click(sender, e);
                        _isCalcDisc = false;
                    }
                    this.Cursor = Cursors.Default;
                    ddlCloseType.Focus();
                    return;
                }

                if (_isRplAllow == true) // Nadeeka 24-08-2015
                {
                    foreach (Service_job_Det _tmp in _processJobDet)
                    {

                        if (_tmp.Jbd_warr_stus == 0)
                        {
                            _isOverWarra = true;
                            goto L2;
                        }
                        else if (string.IsNullOrEmpty(_tmp.Jbd_invc_no) || _tmp.Jbd_invc_no == "N/A")
                        {
                            _isWithOutInv = true;
                            goto L2;
                        }
                        _salesInvoice = _tmp.Jbd_invc_no;

                    }
                }
                else       //kapila 20/1/2016
                {
                    foreach (Service_job_Det _tmp in _processJobDet)
                    {
                        Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound(_tmp.Jbd_jobno);
                        if (_isWarRep == true)
                        {
                            _withoutWarRep = true;
                            goto L2;
                        }
                    }
                }

            L2:

                if (_isOverWarra == true)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Over Warranty items cannot complete as warranty replacement.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlCloseType.Focus();
                    return;
                }
                if (_withoutWarRep == true)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Supplier Warranty replaced items should be completed as warranty replacement.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlCloseType.Focus();
                    return;
                }

                Boolean _isWarRep1 = false;
                Boolean _isWarRep12 = false;

                if (_isWithOutInv == true)
                {
                    foreach (Service_job_Det _tmp in _processJobDet)
                    {
                        _isWarRep1 = CHNLSVC.CustService.IsWarReplaceFound(_tmp.Jbd_jobno);
                        _isWarRep12 = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(_tmp.Jbd_jobno);
                        goto L3;
                    }
                L3:

                    if (_isWarRep12 == false) //Sanjeewa 2016-03-21
                    {
                        if (_isStk == 1)    //5/2/2016  kapila
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Cannot find sales details. This is a stock item", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ddlCloseType.Focus();
                            return;
                        }
                        if (_jobTp != "E")      //kapila 12/2/2016
                            if (_isFgap == 0)//12-10-2015  Nadeeka 
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Cannot find sales details.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ddlCloseType.Focus();
                                return;
                            }
                    }
                }

                //   if (ddlCloseType.SelectedValue.ToString() == Convert.ToString("WRPL"))
                if (_isRplAllow == true)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        MessageBox.Show("Please select old part receive location.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchRequried.Focus();
                        return;
                    }
                    else
                    {
                        if (IsValidLocation() == false)
                        {
                            MessageBox.Show("Please select valid old part receive location.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDispatchRequried.Clear();
                            txtDispatchRequried.Focus();
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtPreferLoc.Text))
                    {
                        if (IsValidPreferLocation() == false)
                        {
                            MessageBox.Show("Please select valid customer prefer location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPreferLoc.Clear();
                            txtPreferLoc.Focus();
                            return;
                        }
                    }

                    Warr_Replacement_Det _getPreviousRep = new Warr_Replacement_Det();
                    DataTable _invDetScm = new DataTable();
                    DataTable _invDet = new DataTable();
                    foreach (Service_job_Det _process_PreRep in _processJobDet)
                    {
                        _getPreviousRep = new Warr_Replacement_Det();
                        _getPreviousRep = CHNLSVC.CustService.GetWarrantyReplacementHistory(_process_PreRep.Jbd_itm_cd, _process_PreRep.Jbd_ser1, "C", _process_PreRep.Jbd_warr);

                        if (_getPreviousRep.Swr_ref != null)
                        {
                            if (MessageBox.Show("The job serial " + _getPreviousRep.Swr_n_itm_ser + " previously replaced serial under job number " + _getPreviousRep.Swr_jobno + ". Previous serial is " + _getPreviousRep.Swr_o_itm_ser + ". Do you want to close this job as warranty replacement.", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }

                        }
                        else
                        {
                            _getPreviousRep = CHNLSVC.CustService.GetWarrantyReplacementHistory(_process_PreRep.Jbd_itm_cd, _process_PreRep.Jbd_ser1, "M", _process_PreRep.Jbd_warr);

                            if (_getPreviousRep.Swr_ref != null)
                            {
                                if (MessageBox.Show("The job serial " + _getPreviousRep.Swr_n_itm_ser + " previously main unit replaced serial under job number " + _getPreviousRep.Swr_jobno + ". Previous serial is " + _getPreviousRep.Swr_o_itm_ser + ". Do you want to close this job as warranty replacement.", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                            }

                        }

                        //Check sales details_________________
                        if (string.IsNullOrEmpty(_getPreviousRep.Swr_ref))
                        {
                            _invDet = CHNLSVC.CustService.GetInvDetBySerial(_process_PreRep.Jbd_invc_no, _process_PreRep.Jbd_ser1, _process_PreRep.Jbd_itm_cd);
                            if (_invDet.Rows.Count <= 0)
                            {
                                _invDetScm = CHNLSVC.CustService.GetInvDetWithDofrmScm(_process_PreRep.Jbd_invc_no, _process_PreRep.Jbd_itm_cd, _process_PreRep.Jbd_ser1);
                            }
                        }
                        else
                        {
                            _invDet = CHNLSVC.CustService.GetInvDetBySerial(_process_PreRep.Jbd_invc_no, _getPreviousRep.Swr_sal_ser, _getPreviousRep.Swr_sal_itm);
                            if (_invDet.Rows.Count <= 0)
                            {
                                _invDetScm = CHNLSVC.CustService.GetInvDetWithDofrmScm(_process_PreRep.Jbd_invc_no, _getPreviousRep.Swr_sal_itm, _getPreviousRep.Swr_sal_ser);
                            }
                        }

                        if (_invDet.Rows.Count <= 0 && _invDetScm.Rows.Count <= 0)
                        {
                            if (_isFgap == 0)//12-10-2015  Nadeeka 
                            {
                                if (_isWarRep12 == false)//Sanjeewa 2016-03-21
                                {
                                    MessageBox.Show("Relevant credit details cannot found.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }

                if (_scvParam == null)  //kapila 25/5/2017
                {
                    if (string.IsNullOrEmpty(txtRemarks.Text))
                    {
                        MessageBox.Show("Please enter remarks.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRemarks.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        MessageBox.Show("Please enter reference number.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefNo.Focus();
                        return;
                    }
                }
                else
                {
                    if (_scvParam.SP_CUST_ADDR_OPT == 0)
                    {
                        if (string.IsNullOrEmpty(txtRemarks.Text))
                        {
                            MessageBox.Show("Please enter remarks.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtRemarks.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(txtRefNo.Text))
                        {
                            MessageBox.Show("Please enter reference number.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtRefNo.Focus();
                            return;
                        }
                    }
                }
                #region validate prefer location by tharanga 2018/03/08
                if (ddlCloseType.SelectedValue.ToString() == "WRPL")
                {
                    if (string.IsNullOrEmpty(txtPreferLoc.Text.Trim()))
                    {
                        foreach (Service_job_Det _det in _processJobDet)
                        {
                            InvoiceHeader dtinv = CHNLSVC.Sales.GetInvoiceHeader(_det.Jbd_invc_no); // add by tharanga 2017/10/23
                            MasterProfitCenter _mstPc = new MasterProfitCenter();
                            _mstPc = CHNLSVC.Sales.GetProfitCenter(dtinv.Sah_com, dtinv.Sah_pc);
                             DataTable _pc = new DataTable();
                             _pc = CHNLSVC.CustService.get_profitcenter(BaseCls.GlbUserComCode, _mstPc.Mpc_cd);
                                foreach (DataRow pcdet in _pc.Rows)
                                {
                                    string det = pcdet["mpc_chnl"].ToString();
                                    if (det == "D-FREE" || det == "DFREES" || det == "DUTY_FREE" || det == "PDF" || det == "CLCDF" || det == "DUTY_FREE_DEALERS" ||
                                        det =="DEALER_CHANNEL" || det =="DEL")
                                    {
                                        if (string.IsNullOrEmpty(txtPreferLoc.Text))
                                        {
                                            txtPreferLoc.Text = BaseCls.GlbUserDefLoca;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please enter Prefer Location .", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtPreferLoc.Focus();
                                        return;
                                    }
                                }
                                break;

                         
                  
                        }
                        //MessageBox.Show("Please enter Prefer Location .", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtPreferLoc.Focus();
                        //return;
                    }

                }


                #endregion
                if (MessageBox.Show("Do you want to confirm ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    btnSave.Enabled = true;
                    return;
                }

                btnSave.Enabled = false;

                foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                {
                    if (string.IsNullOrEmpty(_tmpConf.Jcd_cuscd))
                    {
                        _tmpConf.Jcd_cuscd = lblCusCode.Text.Trim();
                        _tmpConf.Jcd_cusname = lblCusName.Text.Trim();
                        _tmpConf.Jcd_cusadd1 = lblAdd1.Text.Trim();
                        _tmpConf.Jcd_cusadd2 = lblAdd2.Text.Trim();
                        _tmpConf.Jcd_invtype = cmbInvType.Text;
                    }
                }

                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new Service_Confirm_detail();
                dgvRevElement.DataSource = _serJobConfDet;

                var _distinctCus = (from n in _serJobConfDet
                                    group n by new { n.Jcd_cuscd, n.Jcd_cusname, n.Jcd_cusadd1, n.Jcd_cusadd2, n.Jcd_invtype } into r
                                    select new { Jcd_cuscd = r.Key.Jcd_cuscd, Jcd_cusname = r.Key.Jcd_cusname, Jcd_cusadd1 = r.Key.Jcd_cusadd1, Jcd_cusadd2 = r.Key.Jcd_cusadd2, Jcd_invtype = r.Key.Jcd_invtype }).ToList();



                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "CONF";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "CONF";
                masterAuto.Aut_year = null;

                if (_distinctCus.Count > 0)
                {
                    foreach (var _lst in _distinctCus)
                    {
                        _saveConfHdr = new Service_confirm_Header();
                        _saveConfHdr.Jch_no = "1";
                        _saveConfHdr.Jch_com = BaseCls.GlbUserComCode;
                        _saveConfHdr.Jch_chnl = _scvParam.SP_SERCHNL;
                        _saveConfHdr.Jch_loc = BaseCls.GlbUserDefLoca;
                        _saveConfHdr.Jch_pc = BaseCls.GlbUserDefProf;
                        _saveConfHdr.Jch_dt = dtDate.Value.Date;
                        _saveConfHdr.Jch_jobno = txtJobNo.Text.Trim();
                        _saveConfHdr.Jch_invtp = _lst.Jcd_invtype;
                        _saveConfHdr.Jch_reqno = txtReqNo.Text;
                        _saveConfHdr.Jch_manualref = txtRefNo.Text;
                        _saveConfHdr.Jch_rmk = txtRemarks.Text;
                        _saveConfHdr.Jch_isdoneinvoiced = 0;
                        _saveConfHdr.Jch_cust_cd = _lst.Jcd_cuscd;
                        _saveConfHdr.Jch_cust_name = _lst.Jcd_cusname;
                        _saveConfHdr.Jch_add1 = _lst.Jcd_cusadd1;
                        _saveConfHdr.Jch_add2 = _lst.Jcd_cusadd2;
                        _saveConfHdr.Jch_curr_cd = "LKR";
                        _saveConfHdr.Jch_ex_rt = 1;
                        _saveConfHdr.Jch_jobclosetp = ddlCloseType.SelectedValue.ToString();
                        _saveConfHdr.Jch_jobclosedesc = ddlCloseType.Text.ToString();
                        _saveConfHdr.Jch_jobclosermk = txtCloseRmk.Text;
                        _saveConfHdr.Jch_stus = "A";
                        _saveConfHdr.Jch_cre_by = BaseCls.GlbUserID;
                        _saveConfHdr.Jch_mod_by = BaseCls.GlbUserID;
                        if (chkInvNow.Checked == true)
                        {
                            _saveConfHdr.Jch_isdoneinvoiced = 1;
                        }
                        else if (_serJobConfDet.Count <= 0)
                        {
                            _saveConfHdr.Jch_isdoneinvoiced = 1;
                        }
                        else
                        {
                            _saveConfHdr.Jch_isdoneinvoiced = 0;
                        }
                        _finalConfHdr.Add(_saveConfHdr);
                    }
                }
                else
                {
                    _saveConfHdr = new Service_confirm_Header();
                    _saveConfHdr.Jch_no = "1";
                    _saveConfHdr.Jch_com = BaseCls.GlbUserComCode;
                    _saveConfHdr.Jch_chnl = _scvParam.SP_SERCHNL;
                    _saveConfHdr.Jch_loc = BaseCls.GlbUserDefLoca;
                    _saveConfHdr.Jch_pc = BaseCls.GlbUserDefProf;
                    _saveConfHdr.Jch_dt = dtDate.Value.Date;
                    _saveConfHdr.Jch_jobno = txtJobNo.Text.Trim();
                    _saveConfHdr.Jch_invtp = cmbInvType.Text;
                    _saveConfHdr.Jch_reqno = txtReqNo.Text;
                    _saveConfHdr.Jch_manualref = txtRefNo.Text;
                    _saveConfHdr.Jch_rmk = txtRemarks.Text;
                    _saveConfHdr.Jch_isdoneinvoiced = 0;
                    _saveConfHdr.Jch_cust_cd = lblCusCode.Text.Trim();
                    _saveConfHdr.Jch_cust_name = lblCusName.Text.Trim();
                    _saveConfHdr.Jch_add1 = lblAdd1.Text.Trim();
                    _saveConfHdr.Jch_add2 = lblAdd2.Text.Trim();
                    _saveConfHdr.Jch_curr_cd = "LKR";
                    _saveConfHdr.Jch_ex_rt = 1;
                    _saveConfHdr.Jch_jobclosetp = ddlCloseType.SelectedValue.ToString();
                    _saveConfHdr.Jch_jobclosedesc = ddlCloseType.Text.ToString();
                    _saveConfHdr.Jch_jobclosermk = txtCloseRmk.Text;
                    _saveConfHdr.Jch_stus = "A";
                    _saveConfHdr.Jch_cre_by = BaseCls.GlbUserID;
                    _saveConfHdr.Jch_mod_by = BaseCls.GlbUserID;
                    if (chkInvNow.Checked == true)
                    {
                        _saveConfHdr.Jch_isdoneinvoiced = 1;

                    }
                    else if (_serJobConfDet.Count <= 0)
                    {
                        _saveConfHdr.Jch_isdoneinvoiced = 1;
                    }
                    else
                    {
                        _saveConfHdr.Jch_isdoneinvoiced = 0;
                    }
                    _finalConfHdr.Add(_saveConfHdr);
                }


                if (chkInvNow.Checked == true)
                {
                    _invoiceAuto = new MasterAutoNumber();
                    _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _invoiceAuto.Aut_cate_tp = "PRO";
                    _invoiceAuto.Aut_direction = 1;
                    _invoiceAuto.Aut_modify_dt = null;
                    _invoiceAuto.Aut_moduleid = cmbInvType.Text;
                    _invoiceAuto.Aut_number = 0;
                    _invoiceAuto.Aut_start_char = _invoicePrefix;
                    _invoiceAuto.Aut_year = Convert.ToDateTime(dtDate.Value).Year;

                    _invheader.Sah_com = BaseCls.GlbUserComCode;
                    _invheader.Sah_cre_by = BaseCls.GlbUserID;
                    _invheader.Sah_cre_when = DateTime.Now;
                    _invheader.Sah_currency = "LKR";
                    _invheader.Sah_cus_add1 = lblAdd1.Text.Trim();
                    _invheader.Sah_cus_add2 = lblAdd2.Text.Trim(); ;
                    _invheader.Sah_cus_cd = lblCusCode.Text.Trim();
                    _invheader.Sah_cus_name = lblCusName.Text.Trim();
                    _invheader.Sah_d_cust_add1 = "";
                    _invheader.Sah_d_cust_add2 = "";
                    _invheader.Sah_d_cust_cd = lblCusCode.Text.Trim();
                    _invheader.Sah_d_cust_name = lblCusName.Text.Trim();
                    _invheader.Sah_direct = true;
                    _invheader.Sah_dt = Convert.ToDateTime(dtDate.Value);
                    _invheader.Sah_epf_rt = 0;
                    _invheader.Sah_esd_rt = 0;
                    _invheader.Sah_ex_rt = 1;
                    _invheader.Sah_inv_no = "na";
                    _invheader.Sah_inv_sub_tp = "SA";
                    _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                    _invheader.Sah_is_acc_upload = false;
                    _invheader.Sah_man_ref = txtRefNo.Text;
                    _invheader.Sah_manual = false;//chkManualRef.Checked ? true : false;
                    _invheader.Sah_mod_by = BaseCls.GlbUserID;
                    _invheader.Sah_mod_when = DateTime.Now;
                    _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                    _invheader.Sah_pdi_req = 0;
                    _invheader.Sah_ref_doc = txtJobNo.Text;
                    _invheader.Sah_remarks = txtRemarks.Text;
                    _invheader.Sah_sales_chn_cd = "";
                    _invheader.Sah_sales_chn_man = "";
                    _invheader.Sah_sales_ex_cd = "N/A";
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
                    //if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) _invheader.Sah_stus = "D";
                    _invheader.Sah_town_cd = "";
                    _invheader.Sah_tp = "INV";
                    _invheader.Sah_wht_rt = 0;
                    _invheader.Sah_direct = true;
                    _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                    _invheader.Sah_anal_11 = 0;// (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
                    _invheader.Sah_del_loc = BaseCls.GlbUserDefLoca; //(chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                    _invheader.Sah_grn_com = "";
                    _invheader.Sah_grn_loc = "";
                    _invheader.Sah_is_grn = false;
                    _invheader.Sah_grup_cd = "";
                    //_invheader.Sah_is_svat = false;// lblSVatStatus.Text == "Available" ? true : false; commented by akila 2017/06/15
                    _invheader.Sah_is_svat = lblSvatStatus.Text== "Available" ? true : false; 
                    _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                    _invheader.Sah_anal_4 = "";
                    _invheader.Sah_anal_2 = "SCV";
                    _invheader.Sah_anal_6 = "";
                    _invheader.Sah_man_cd = "";
                    _invheader.Sah_is_dayend = 0;
                    _invheader.Sah_remarks = txtRemarks.Text.Trim();
                    _invheader.Sah_anal_1 = "";


                    //foreach (Service_Confirm_detail _tmpSerJob in _serJobConfDet)
                    //{
                    //    InvoiceItem _tmpInvItm = new InvoiceItem();
                    //    _tmpInvItm.Sad_disc_amt = _tmpSerJob.Jcd_dis;
                    //    _tmpInvItm.Sad_disc_rt = _tmpSerJob.Jcd_dis_rt;
                    //    if (_tmpSerJob.Jcd_itmtp == "V")
                    //    {
                    //        _tmpInvItm.Sad_do_qty = _tmpSerJob.Jcd_qty;
                    //    }
                    //    else
                    //    {
                    //        _tmpInvItm.Sad_do_qty = 0;
                    //    }
                    //    _tmpInvItm.Sad_fws_ignore_qty = 0;
                    //    _tmpInvItm.Sad_inv_no = "1";
                    //    _tmpInvItm.Sad_itm_cd = _tmpSerJob.Jcd_itmcd;
                    //    _tmpInvItm.Sad_itm_line = _tmpSerJob.Jcd_line;
                    //    _tmpInvItm.Sad_itm_seq = _tmpSerJob.Jcd_pbitmseqno;
                    //    _tmpInvItm.Sad_itm_stus = _tmpSerJob.Jcd_itmstus;
                    //    _tmpInvItm.Sad_itm_tax_amt = _tmpSerJob.Jcd_tax;
                    //    _tmpInvItm.Sad_itm_tp = _tmpSerJob.Jcd_itmtp;
                    //    _tmpInvItm.Sad_job_line = _tmpSerJob.Jcd_joblineno;
                    //    _tmpInvItm.Sad_job_no = _tmpSerJob.Jcd_jobno;
                    //    _tmpInvItm.Sad_pb_lvl = _tmpSerJob.Jcd_pblvl;
                    //    _tmpInvItm.Sad_pb_price = _tmpSerJob.Jcd_pbprice;
                    //    _tmpInvItm.Sad_pbook = _tmpSerJob.Jcd_pb;
                    //    _tmpInvItm.Sad_qty = _tmpSerJob.Jcd_qty;
                    //    _tmpInvItm.Sad_seq = _tmpSerJob.Jcd_pbseqno;
                    //    _tmpInvItm.Sad_srn_qty = 0;
                    //    _tmpInvItm.Sad_tot_amt = _tmpSerJob.Jcd_net_amt;
                    //    _tmpInvItm.Sad_unit_amt = _tmpSerJob.Jcd_amt;
                    //    _tmpInvItm.Sad_unit_rt = _tmpSerJob.Jcd_unitprice;
                    //    _tmpInvItm.Sad_uom = _tmpSerJob.Jcd_itmuom;
                    //    _tmpInvItm.Sad_warr_period = 0;
                    //    _tmpInvItm.Sad_warr_remarks = "";

                    //    _tmpInvItm.Sad_alt_itm_desc = _tmpSerJob.Jcd_itmdesc;
                    //    //if ((_tmpSerJob.Jcd_rmk == "ADD") || (_tmpSerJob.Jcd_pb == "PO")) //updated by akila 2017/06/21 if po item thenchnaged po item discription shoul display in invoice
                    //    //{
                    //    //    _tmpInvItm.Sad_alt_itm_desc = _tmpSerJob.Jcd_itmdesc;
                    //    //}
                    //    _tmpInvItm.Sad_merge_itm = _tmpSerJob.Jcd_mainitmcd;
                    //    _tmpInvItm.Sad_conf_line = _tmpSerJob.Jcd_line;
                    //    _invItem.Add(_tmpInvItm);
                    //}

             
                    if (_serJobConfDet.Count <= 0)
                    {
                        Int32 _lineNo = 0;
                        foreach (Service_job_Det _process in _processJobDet)
                        {
                           InvoiceItem _tmpInvItm = new InvoiceItem();
                            _lineNo = _lineNo + 1;
                            //_tmpInvItm.Sad_disc_amt = 0;
                            //_tmpInvItm.Sad_disc_rt = 0;
                            //_tmpInvItm.Sad_do_qty = 0;
                            //_tmpInvItm.Sad_fws_ignore_qty = 0;
                            //_tmpInvItm.Sad_inv_no = "1";
                            if (string.IsNullOrEmpty(_scvParam.sp_def_debt_cd))
                            {
                                MessageBox.Show("Default charge code not setup.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            MasterItem _itm = new MasterItem();
                            _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _scvParam.sp_def_debt_cd);
                            List<MasterItemTax> _tax = new List<MasterItemTax>();
                            _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _scvParam.sp_def_debt_cd, null, null, null);
                            if (_tax.Count <= 0)
                            {
                                MessageBox.Show("Tax definition not setup for item " + _scvParam.sp_def_debt_cd + ".", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnSave.Enabled = true;
                                return;
                            }

                            if (string.IsNullOrEmpty(_itm.Mi_cd))
                            {
                                MessageBox.Show("Default charge code not setup.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //_tmpInvItm.Sad_itm_cd = _scvParam.sp_def_debt_cd;
                            //_tmpInvItm.Sad_itm_line = _lineNo;
                            //_tmpInvItm.Sad_itm_seq = 0;
                            //_tmpInvItm.Sad_itm_stus = "GOD";
                            //_tmpInvItm.Sad_itm_tax_amt = 0;
                            //_tmpInvItm.Sad_itm_tp = _itm.Mi_itm_tp; //"V";
                            //_tmpInvItm.Sad_job_line = _process.Jbd_jobline;
                            //_tmpInvItm.Sad_job_no = _process.Jbd_jobno;
                            //_tmpInvItm.Sad_pb_lvl = "N/A";
                            //_tmpInvItm.Sad_pb_price = 0;
                            //_tmpInvItm.Sad_pbook = "N/A";
                            //_tmpInvItm.Sad_qty = 1;
                            //_tmpInvItm.Sad_do_qty = 1;
                            //_tmpInvItm.Sad_seq = 0;
                            //_tmpInvItm.Sad_srn_qty = 0;
                            //_tmpInvItm.Sad_tot_amt = 0;
                            //_tmpInvItm.Sad_unit_amt = 0;
                            //_tmpInvItm.Sad_unit_rt = 0;
                            //_tmpInvItm.Sad_uom = _itm.Mi_itm_uom;//"NOS";
                            //_tmpInvItm.Sad_warr_period = 0;
                            //_tmpInvItm.Sad_warr_remarks = "";
                            //_tmpInvItm.Sad_conf_line = _lineNo;
                            ////if (_tmpSerJob.Jcd_rmk == "ADD")
                            ////{
                            ////    _tmpInvItm.Sad_alt_itm_desc = _tmpSerJob.Jcd_itmdesc;
                            ////}

                            //_invItem.Add(_tmpInvItm);

                            //Fill Confriamtion details when not include any conf. details
                            Service_Confirm_detail _tmpConfDet = new Service_Confirm_detail();
                            _tmpConfDet.Jcd_amt = 0;
                            _tmpConfDet.Jcd_balqty = 1;
                            _tmpConfDet.Jcd_batchline = 0;
                            _tmpConfDet.Jcd_costsheetlineno = 0;
                            _tmpConfDet.Jcd_cusadd1 = lblAdd1.Text.Trim();
                            _tmpConfDet.Jcd_cusadd2 = lblAdd2.Text.Trim();
                            _tmpConfDet.Jcd_cuscd = lblCusCode.Text.Trim();
                            _tmpConfDet.Jcd_cusname = lblCusName.Text.Trim();
                            _tmpConfDet.Jcd_dis = 0;
                            _tmpConfDet.Jcd_dis_rt = 0;
                            _tmpConfDet.Jcd_foc = 0;
                            _tmpConfDet.Jcd_gatepass_raised = 0;
                            _tmpConfDet.Jcd_invtype = cmbInvType.Text;
                            _tmpConfDet.Jcd_isadditm = 0;
                            _tmpConfDet.Jcd_iswarr = _process.Jbd_swarr_stus;
                            _tmpConfDet.Jcd_itmbrand = _itm.Mi_brand;
                            _tmpConfDet.Jcd_itmcd = _scvParam.sp_def_debt_cd;
                            _tmpConfDet.Jcd_itmdesc = _itm.Mi_shortdesc;
                            _tmpConfDet.Jcd_itmline = 0;
                            _tmpConfDet.Jcd_itmmodel = _itm.Mi_model;
                            _tmpConfDet.Jcd_itmstus = "GOD";
                            _tmpConfDet.Jcd_itmtp = _itm.Mi_itm_tp;
                            _tmpConfDet.Jcd_itmuom = _itm.Mi_itm_uom;
                            _tmpConfDet.Jcd_jobitmcd = _process.Jbd_itm_cd;
                            _tmpConfDet.Jcd_jobitmser = _process.Jbd_ser1;
                            _tmpConfDet.Jcd_joblineno = _process.Jbd_jobline;
                            _tmpConfDet.Jcd_jobno = _process.Jbd_jobno;
                            _tmpConfDet.Jcd_jobwarrno = _process.Jbd_warr;
                            _tmpConfDet.Jcd_line = _lineNo;
                            _tmpConfDet.Jcd_net_amt = 0;
                            _tmpConfDet.Jcd_pb = "N/A";
                            _tmpConfDet.Jcd_pbitmseqno = 0;
                            _tmpConfDet.Jcd_pblvl = "N/A";
                            _tmpConfDet.Jcd_pbprice = 0;
                            _tmpConfDet.Jcd_pbseqno = 0;
                            _tmpConfDet.Jcd_qty = 1;
                            _tmpConfDet.Jcd_ser_id = 0;
                            _tmpConfDet.Jcd_serline = 0;
                            _tmpConfDet.Jcd_tax = 0;
                            _tmpConfDet.Jcd_tax_rt = 0;
                            _tmpConfDet.Jcd_unitprice = 0;

                            _serJobConfDet.Add(_tmpConfDet);
                        }
                    }

                    

                    #region Old Code
                    foreach (Service_Confirm_detail _tmpSerJob in _serJobConfDet)
                    {
                        InvoiceItem _tmpInvItm = new InvoiceItem();
                        _tmpInvItm.Sad_disc_amt = _tmpSerJob.Jcd_dis;
                        _tmpInvItm.Sad_disc_rt = _tmpSerJob.Jcd_dis_rt;
                        if (_tmpSerJob.Jcd_itmtp == "V")
                        {
                            _tmpInvItm.Sad_do_qty = _tmpSerJob.Jcd_qty;
                        }
                        else
                        {
                            _tmpInvItm.Sad_do_qty = 0;
                        }
                        _tmpInvItm.Sad_fws_ignore_qty = 0;
                        _tmpInvItm.Sad_inv_no = "1";
                        _tmpInvItm.Sad_itm_cd = _tmpSerJob.Jcd_itmcd;
                        _tmpInvItm.Sad_itm_line = _tmpSerJob.Jcd_line;
                        _tmpInvItm.Sad_itm_seq = _tmpSerJob.Jcd_pbitmseqno;
                        _tmpInvItm.Sad_itm_stus = _tmpSerJob.Jcd_itmstus;
                        _tmpInvItm.Sad_itm_tax_amt = _tmpSerJob.Jcd_tax;
                        _tmpInvItm.Sad_itm_tp = _tmpSerJob.Jcd_itmtp;
                        _tmpInvItm.Sad_job_line = _tmpSerJob.Jcd_joblineno;
                        _tmpInvItm.Sad_job_no = _tmpSerJob.Jcd_jobno;
                        _tmpInvItm.Sad_pb_lvl = _tmpSerJob.Jcd_pblvl;
                        _tmpInvItm.Sad_pb_price = _tmpSerJob.Jcd_pbprice;
                        _tmpInvItm.Sad_pbook = _tmpSerJob.Jcd_pb;
                        _tmpInvItm.Sad_qty = _tmpSerJob.Jcd_qty;
                        _tmpInvItm.Sad_seq = _tmpSerJob.Jcd_pbseqno;
                        _tmpInvItm.Sad_srn_qty = 0;
                        _tmpInvItm.Sad_tot_amt = _tmpSerJob.Jcd_net_amt;
                        _tmpInvItm.Sad_unit_amt = _tmpSerJob.Jcd_amt;
                        _tmpInvItm.Sad_unit_rt = _tmpSerJob.Jcd_unitprice;
                        _tmpInvItm.Sad_uom = _tmpSerJob.Jcd_itmuom;
                        _tmpInvItm.Sad_warr_period = 0;
                        _tmpInvItm.Sad_warr_remarks = "";

                        _tmpInvItm.Sad_alt_itm_desc = _tmpSerJob.Jcd_itmdesc;
                        //if ((_tmpSerJob.Jcd_rmk == "ADD") || (_tmpSerJob.Jcd_pb == "PO")) //updated by akila 2017/06/21 if po item thenchnaged po item discription shoul display in invoice
                        //{
                        //    _tmpInvItm.Sad_alt_itm_desc = _tmpSerJob.Jcd_itmdesc;
                        //}
                        _tmpInvItm.Sad_merge_itm = _tmpSerJob.Jcd_mainitmcd;
                        _tmpInvItm.Sad_conf_line = _tmpSerJob.Jcd_line;
                        _invItem.Add(_tmpInvItm);
                    }
                    #endregion


                    


                    if (ucPayModes1.RecieptItemList != null)
                    {
                        if (ucPayModes1.RecieptItemList.Count > 0)
                        {
                            _recHeader.Sar_acc_no = "";
                            _recHeader.Sar_act = true;
                            _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                            _recHeader.Sar_comm_amt = 0;
                            _recHeader.Sar_create_by = BaseCls.GlbUserID;
                            _recHeader.Sar_create_when = DateTime.Now;
                            _recHeader.Sar_currency_cd = "LKR";
                            _recHeader.Sar_debtor_add_1 = "";
                            _recHeader.Sar_debtor_add_2 = "";
                            _recHeader.Sar_debtor_cd = "";
                            _recHeader.Sar_debtor_name = "";
                            _recHeader.Sar_direct = true;
                            _recHeader.Sar_direct_deposit_bank_cd = "";
                            _recHeader.Sar_direct_deposit_branch = "";
                            _recHeader.Sar_epf_rate = 0;
                            _recHeader.Sar_esd_rate = 0;
                            _recHeader.Sar_is_mgr_iss = false;
                            _recHeader.Sar_is_oth_shop = false;
                            _recHeader.Sar_is_used = false;
                            _recHeader.Sar_manual_ref_no = txtRefNo.Text;
                            _recHeader.Sar_mob_no = "";
                            _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                            _recHeader.Sar_mod_when = DateTime.Now;
                            _recHeader.Sar_nic_no = "";
                            _recHeader.Sar_oth_sr = "";
                            _recHeader.Sar_prefix = "";
                            _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                            _recHeader.Sar_receipt_date = Convert.ToDateTime(dtDate.Value);
                            _recHeader.Sar_receipt_no = "na";
                            _recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                            _recHeader.Sar_ref_doc = "";
                            _recHeader.Sar_remarks = txtRemarks.Text;
                            _recHeader.Sar_seq_no = 1;
                            _recHeader.Sar_ser_job_no = "";
                            _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                            _recHeader.Sar_tel_no = "";
                            _recHeader.Sar_tot_settle_amt = 0;
                            _recHeader.Sar_uploaded_to_finance = false;
                            _recHeader.Sar_used_amt = 0;
                            _recHeader.Sar_wht_rate = 0;


                            _receiptAuto = new MasterAutoNumber();
                            _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                            _receiptAuto.Aut_cate_tp = "PRO";
                            _receiptAuto.Aut_direction = 1;
                            _receiptAuto.Aut_modify_dt = null;
                            _receiptAuto.Aut_moduleid = "RECEIPT";
                            _receiptAuto.Aut_number = 0;
                            _receiptAuto.Aut_start_char = "DIR";
                            _receiptAuto.Aut_year = Convert.ToDateTime(dtDate.Text).Year;

                        }
                    }

                    _masterAutoDo = new MasterAutoNumber();
                    _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _masterAutoDo.Aut_cate_tp = "LOC";
                    _masterAutoDo.Aut_direction = 0;
                    _masterAutoDo.Aut_moduleid = "DO";
                    _masterAutoDo.Aut_start_char = "DO";
                    _masterAutoDo.Aut_year = dtDate.Value.Year;

                    invHdr = new InventoryHeader();
                    invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                    invHdr.Ith_com = BaseCls.GlbUserComCode;
                    invHdr.Ith_doc_tp = "DO";
                    invHdr.Ith_pc = BaseCls.GlbUserDefProf;
                    invHdr.Ith_doc_date = Convert.ToDateTime(dtDate.Value).Date;
                    invHdr.Ith_doc_year = Convert.ToDateTime(dtDate.Text).Year;
                    invHdr.Ith_cate_tp = "DPS";
                    invHdr.Ith_sub_tp = cmbInvType.Text.Trim();
                    invHdr.Ith_bus_entity = lblCusCode.Text.Trim();
                    invHdr.Ith_del_add1 = lblAdd1.Text.Trim();
                    invHdr.Ith_del_add1 = lblAdd2.Text.Trim();
                    invHdr.Ith_is_manual = false;
                    invHdr.Ith_stus = "A";
                    invHdr.Ith_cre_by = BaseCls.GlbUserID;
                    invHdr.Ith_mod_by = BaseCls.GlbUserID;
                    invHdr.Ith_direct = false;
                    invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr.Ith_manual_ref = txtRefNo.Text;
                    invHdr.Ith_vehi_no = string.Empty;
                    invHdr.Ith_remarks = string.Empty;


                    _rccNo = "";
                    //check job type whether rcc or not
                    Service_JOB_HDR _JobHdrRcc = new Service_JOB_HDR();
                    _JobHdrRcc = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text.Trim(), BaseCls.GlbUserComCode);
                    _isRcc = false;
                    _rcc = new RCC();

                    if (_JobHdrRcc != null)
                    {
                        if (_JobHdrRcc.SJB_JOBSTP == "RCC")
                        {

                            RCC _rccDet = CHNLSVC.Inventory.GetRccByNo(_JobHdrRcc.SJB_REQNO);

                            if (_rccDet == null)
                            {
                                MessageBox.Show("Job is RCC and cannot find valid RCC request.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

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
                            _isStockUpdate = true;
                            _aodIssuLoc = _proJob.Jbd_aodissueloc;

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
                                _aodHdr.Ith_anal_8 = Convert.ToDateTime(dtDate.Value).Date;
                                _aodHdr.Ith_anal_9 = Convert.ToDateTime(dtDate.Value).Date;
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
                                _aodHdr.Ith_doc_date = Convert.ToDateTime(dtDate.Value).Date;
                                _aodHdr.Ith_doc_no = string.Empty;
                                _aodHdr.Ith_doc_tp = "AOD";
                                _aodHdr.Ith_doc_year = Convert.ToDateTime(dtDate.Value).Date.Year;
                                _aodHdr.Ith_entry_no = string.Empty;
                                _aodHdr.Ith_entry_tp = string.Empty;
                                _aodHdr.Ith_git_close = false;
                                _aodHdr.Ith_git_close_date = Convert.ToDateTime(dtDate.Value).Date;
                                _aodHdr.Ith_git_close_doc = string.Empty;
                                _aodHdr.Ith_is_manual = false;
                                _aodHdr.Ith_isprinted = false;
                                if (!string.IsNullOrEmpty(_rccNo))
                                {
                                    _aodHdr.Ith_sub_docno = _rccNo;
                                }
                                else
                                {
                                    _aodHdr.Ith_sub_docno = txtJobNo.Text.Trim();
                                }
                                _aodHdr.Ith_loading_point = string.Empty;
                                _aodHdr.Ith_loading_user = string.Empty;
                                _aodHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                                _aodHdr.Ith_manual_ref = "0";
                                _aodHdr.Ith_mod_by = BaseCls.GlbUserID;
                                _aodHdr.Ith_mod_when = DateTime.Now.Date;
                                _aodHdr.Ith_noofcopies = 0;
                                _aodHdr.Ith_oth_loc = _aodIssuLoc;
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
                                _aodHdr.Ith_job_no = txtJobNo.Text;

                                _AodAuto.Aut_moduleid = "AOD";
                                _AodAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                                _AodAuto.Aut_cate_tp = "LOC";
                                _AodAuto.Aut_direction = 0;
                                _AodAuto.Aut_modify_dt = null;
                                _AodAuto.Aut_year = DateTime.Now.Year;
                                _AodAuto.Aut_start_char = "AOD";
                            }

                            Int32 _serID = 0;

                            DataTable _dtSer = CHNLSVC.CustService.getSerialIDByDocument(_proJob.Jbd_aodrecno, _proJob.Jbd_itm_cd, _proJob.Jbd_ser1);
                            if (_dtSer.Rows.Count > 0)
                            {
                                _serID = Convert.ToInt32(_dtSer.Rows[0]["its_ser_id"]);
                            }
                            else
                            {
                                MessageBox.Show("Cannot find AOD receive details.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //_AodserialList = new List<ReptPickSerials>();
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, _proJob.Jbd_itm_cd, _serID);
                            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                            _reptPickSerial_.Tus_usrseq_no = 1;
                            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                            _reptPickSerial_.Tus_base_doc_no = "N/A";
                            _reptPickSerial_.Tus_base_itm_line = 0;
                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                            _reptPickSerial_.Tus_job_no = txtJobNo.Text.Trim();
                            _reptPickSerial_.Tus_job_line = _proJob.Jbd_jobline;

                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _proJob.Jbd_itm_cd);
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _AodserialList.Add(_reptPickSerial_);

                        }
                    }
                }

                if (chkTaxPayable.Checked == true)
                {// Nadeeka 30-12-2015
                    if (IsDiffTax(_invItem) == false)
                    {
                        MessageBox.Show("Two different tax rates are not allowed according to the new government procedures for tax invoices.", "Tax Rates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
               

                //if (_invItem.Count > 0)
                //{
                //    foreach (var item in _invItem)
                //    {
                //        //_serJobConfDet.Where(r => r.Jcd_itmcd == item.Sad_itm_cd && r.Jcd_itmstus == item.Sad_itm_stus && r.Jcd_line == item.Sad_conf_line).ToList().ForEach(y => y.Jcd_itmline = item.Sad_itm_line);
                //    }
                //}
                Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
                Service_Req_Det _jobdetail = new Service_Req_Det();
                if (chkInvNow.Checked==true)
                {
                    Service_Chanal_parameter oChnnalPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    if (oChnnalPara.SP_ISNEEDGATEPASS == 0)
                    {
                     
                        #region ACRequest Tharanga 2018-06-12

                        DataTable jobhdr = new DataTable();
                        DataTable jobdet = new DataTable();
                        //Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
                        //Service_Req_Det _jobdetail = new Service_Req_Det();


                        jobhdr = CHNLSVC.CustService.GetJobHeader(txtJobNo.Text.ToString().Trim(), BaseCls.GlbUserComCode);
                        jobdet = CHNLSVC.CustService.GetJobDetail(txtJobNo.Text.ToString().Trim(), BaseCls.GlbUserComCode);
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

                            //if (MessageBox.Show("Do you want continue without free service shedule ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                            //{
                            //    return;
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

                                if (lstService.Count <= 0)
                                {
                                    MessageBox.Show("Cannot process. Please fill The Service Deatils", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    btnSave.Enabled = true;
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
                                    //_jobdetail.Jrd_loc = _dr["JBD_LOC"].ToString();
                                    //_jobdetail.Jrd_pc = _dr["JBD_PC"].ToString();
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
                                #region add shedule
                                string jobNo;
                                string receiptNo = string.Empty;
                                string _msg1 = "";
                                for (int i = 0; i < lstService.Count; i++)
                                {
                                    _jobHeader.Srb_dt = lstService[i].Servicedates;
                                    _jobHeader.Srb_st_dt = lstService[i].Servicedates;
                                    _jobHeader.Srb_ed_dt = lstService[i].Servicedates;
                                    _jobHeader.Srb_custexptdt = lstService[i].Servicedates;
                                }
                                #endregion
                            //}




                        }

                        #endregion
                    }

                  
                }

                MasterAutoNumber _jobAuto1 = new MasterAutoNumber();
                Int32 _warStus = 0;


                #region Job Auto Number
                _jobAuto1.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _jobAuto1.Aut_cate_tp = "LOC";
                _jobAuto1.Aut_moduleid = "SVREQ";
                _jobAuto1.Aut_direction = 0;
                _jobAuto1.Aut_year = _jobHeader.Srb_dt.Year;
                _jobAuto1.Aut_start_char = "SVREQ";
                #endregion
                string scv_req_no = string.Empty;

                Int32 _effect = CHNLSVC.CustService.Save_Job_Confirmation(_finalConfHdr, _serJobConfDet, _serCostSheet, masterAuto, _invheader, _invItem, _recHeader, ucPayModes1.RecieptItemList, _invoiceAuto, _receiptAuto, chkInvNow.Checked, _processJobDet, invHdr, _masterAutoDo, txtDispatchRequried.Text, _aodHdr, _AodserialList, _AodAuto, _rcc, _isRcc, _isStockUpdate, txtPreferLoc.Text.Trim(), BaseCls.GlbDefSubChannel, out _msg, out scv_req_no, _scvParam.SP_AUTO_START_JOB
                    , _jobHeader, _scvItemList, lstService, _scvDefList, _scvItemSubList, _jobAuto1, BaseCls.GlbDefSubChannel, "", "", 0, _jobAuto1, 0); // updated by akila 2017/06/05

           

                if (_effect == 1)
                {
                    if (chkInvNow.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(scv_req_no))
                        {
                            MessageBox.Show("Job confirmed and invoiced and Shedule service request are created ." + scv_req_no, "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Job confirmed and invoiced.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Job confirmed.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();

                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, string.Empty, string.Empty, _salesInvoice, "C", null, null);


                    if (_isRcc == true)
                    {   // Nadeeka 25-07-2015

                        Service_Chanal_parameter _Parameters = null; //kapila 6/10/2016
                        _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                        if (_Parameters != null)
                        {
                            //if (_Parameters.SP_CLS_STAGE_WS == 8)
                            //{
                            FF.BusinessObjects.RCC _RCC = null;
                            _RCC = CHNLSVC.Inventory.GetRccByNo(_rccNo);
                            CHNLSVC.CustService.SendConfirmationMail(BaseCls.GlbUserComCode, _RCC.Inr_loc_cd, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), _rccNo, dtDate.Value.Date, txtCloseRmk.Text, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID);
                            //}
                        }
                    }
                    else
                    {
                        if (_invHdr != null && _invHdr.Count > 0)
                        {
                            CHNLSVC.CustService.SendConfirmationMail(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), "-1", dtDate.Value.Date, txtCloseRmk.Text, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID);
                        }
                    }

                    if (_isRplAllow == true)
                    {// NadeekA 29/11/2015
                        if (_invHdr != null && _invHdr.Count > 0)
                        {
                            CHNLSVC.CustService.SendWarantyReplacementMail(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), dtDate.Value.Date, txtCloseRmk.Text, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID, 0);
                        }
                    }

                    //Tharaka send SMS to tech
                    sendSMStoTech(_finalConfHdr[0].Jch_jobno, 1);

                    if (chkInvNow.Checked == true)
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
                        _viewsvc.GlbReportDoc = _msg;
                        BaseCls.GlbReportDoc = _msg;

                        if (BaseCls.GlbDefSubChannel == "MCS")
                        { if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
                        else
                        { BaseCls.GlbReportDirectPrint = 0; }
                        if (BaseCls.GlbReportDirectPrint == 1)
                        {// Nadeeka 11-07-2015 (Direct Print/ Sanjeewa's process)

                            FF.WindowsERPClient.Reports.Service.clsServiceRep obj = new FF.WindowsERPClient.Reports.Service.clsServiceRep();
                            obj.InvociePrintServicePhone();

                            Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                            obj._JobInvoicePh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                            MessageBox.Show("Please check whether printer load the Invoice documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            obj._JobInvoicePh.PrintToPrinter(1, false, 0, 0);

                        }
                        else
                        {
                            _viewsvc.Show();
                            _viewsvc = null;
                        }
                    }
                    Clear_Data();
                    serviceClear();
                    btnSave.Enabled = true;
                    txtJobNo.Text = "";



                }
                else
                {
                    MessageBox.Show("Job confirmation terminate." + _msg, "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = true;
                    if (dgvRevElement.Rows.Count <= 0)
                    {
                        _serJobConfDet = new List<Service_Confirm_detail>();
                    }
                    return;
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = true;
                if (dgvRevElement.Rows.Count <= 0)
                {
                    _serJobConfDet = new List<Service_Confirm_detail>();
                }
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void JobConfirmation_Load(object sender, EventArgs e)
        {
            Clear_Data();
            txtJobNo.Text = "";
        }

        private void grvDefItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            if (grvDefItms.ColumnCount > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (_processStart == true)
                    {
                        MessageBox.Show("Already process cost derive. Cannot change confirm job item now.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)grvDefItms.Rows[grvDefItms.CurrentRow.Index].Cells[0];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "False":
                            {
                                ch1.Value = true;
                                break;
                            }
                        case "True":
                            {
                                ch1.Value = false;
                                break;
                            }
                    }
                }

            }
        }

        private void grvDefItms_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int16 _jobLine = 0;
            string _jobNo = "";
            string _jobItm = "";
            string _jobSer = "";
            string _jobWara = "";

            if (grvDefItms.ColumnCount > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex != 0)
                {
                    if (chkAddNewRev.Checked == true)
                    {

                        txtItem.Text = "";
                        txtQty.Text = "";
                        txtEditJobLine.Text = "";
                        txtEditJobNo.Text = "";
                        txtJobItem.Text = "";
                        txtjobSer.Text = "";
                        txtjobWara.Text = "";

                        _jobLine = Convert.ToInt16(grvDefItms.Rows[e.RowIndex].Cells["col_JLine"].Value);
                        _jobNo = txtJobNo.Text.Trim();
                        _jobItm = grvDefItms.Rows[e.RowIndex].Cells["col_JItm"].Value.ToString();
                        _jobSer = grvDefItms.Rows[e.RowIndex].Cells["col_JSerial"].Value.ToString();
                        _jobWara = grvDefItms.Rows[e.RowIndex].Cells["col_JWarra"].Value.ToString();

                        txtEditJobLine.Text = _jobLine.ToString();
                        txtEditJobNo.Text = _jobNo;
                        txtJobItem.Text = _jobItm;
                        txtjobSer.Text = _jobSer;
                        txtjobWara.Text = _jobWara;
                        lblIBrand.Text = "";
                        lblIDesc.Text = "";
                        lblImodel.Text = "";

                        pnlEdit.Visible = true;
                        txtItem.Focus();
                    }
                    else if (chkAddCost.Checked == true)
                    {
                        _jobLine = Convert.ToInt16(grvDefItms.Rows[e.RowIndex].Cells["col_JLine"].Value);
                        _jobNo = txtJobNo.Text.Trim();
                        _jobItm = grvDefItms.Rows[e.RowIndex].Cells["col_JItm"].Value.ToString();
                        _jobSer = grvDefItms.Rows[e.RowIndex].Cells["col_JSerial"].Value.ToString();
                        _jobWara = grvDefItms.Rows[e.RowIndex].Cells["col_JWarra"].Value.ToString();

                        lblJobCostLine.Text = _jobLine.ToString();
                        lblJobItemCost.Text = _jobItm;
                        lblCostItemSer.Text = _jobSer;
                        lblJobCostWara.Text = _jobWara;
                        txtCostItm.Focus();
                    }
                }
            }

        }

        private void loadCloseTypes()
        {
            List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
            ddlCloseType.DisplayMember = "SCT_DESC";
            ddlCloseType.ValueMember = "SCT_TP";
            ddlCloseType.DataSource = oCloseType;

            //Add  by akila (temporarily)
            if (BaseCls.GlbUserComCode == "AAL")
            {
                if (oCloseType != null)
                {
                    if (oCloseType.Count > 0)
                    {
                        int _index = oCloseType.IndexOf(oCloseType.Where(x => x.SCT_TP == "CMP").First());
                        if (_index >= 0) { ddlCloseType.SelectedIndex = _index; }                        
                    }
                }
            }


            //List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
            //ddlCloseType.DisplayMember = "SCT_DESC";
            //ddlCloseType.ValueMember = "SCT_TP";
            //ddlCloseType.DataSource = oCloseType;
            //ddlCloseType.SelectedValue = "CMP";   //kapila 7/10/2016 req. by dilanda
        }

        private void btnCloseEdit_Click(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtQty.Text = "";
            txtEditJobLine.Text = "";
            txtEditJobNo.Text = "";
            txtJobItem.Text = "";
            txtjobSer.Text = "";
            txtjobWara.Text = "";
            ClearPriceTextBox();
            chkEditItm.Checked = false;
            pnlEdit.Visible = false;
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (chkEditItm.Checked == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtItem.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select valid item.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Clear();
                        txtItem.Focus();
                        return;
                    }
                    else
                    {
                        MasterItem _itmMas = new MasterItem();
                        _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        lblIBrand.Text = _itmMas.Mi_brand;
                        lblIDesc.Text = _itmMas.Mi_shortdesc;
                        lblImodel.Text = _itmMas.Mi_model;
                        txtQty.Text = "1";
                        _itmType = _itmMas.Mi_itm_tp;

                        if (_itmMas.Mi_is_editlongdesc == true)
                        {
                            lblIDesc.ReadOnly = false;
                        }
                        else
                        {
                            lblIDesc.ReadOnly = true;
                        }
                    }
                }
                else
                {
                    MasterItem _itmMas = new MasterItem();
                    _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    lblIBrand.Text = _itmMas.Mi_brand;
                    lblIDesc.Text = _itmMas.Mi_shortdesc;
                    lblImodel.Text = _itmMas.Mi_model;
                    txtQty.Text = "1";
                    _itmType = _itmMas.Mi_itm_tp;
                }
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void dgvRevElement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRevElement.Rows.Count > 0) //&& e.RowIndex >= 0
            {
                if (e.ColumnIndex == 1)
                {
                    chkEditItm.Checked = false;
                    chkAddNewRev.Checked = false;
                    if (MessageBox.Show("Do you want to edit selected details ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string _mainItm = "";
                        Int16 _mainLine = 0;
                        string _jobNo = "";
                        decimal _mainQty = 0;
                        string _jobItm = "";
                        string _jobpb = "";
                        string _jobpblvl = "";
                        string _jobitmstus = "";
                        decimal _jobqty = 0;
                        decimal _jobuprice = 0;
                        decimal _jobamt = 0;
                        decimal _jobdrate = 0;
                        decimal _jobdamt = 0;
                        decimal _jobtax = 0;
                        decimal _jobnet = 0;

                        chkEditItm.Checked = true;


                        txtItem.Text = "";
                        txtQty.Text = "";
                        txtEditJobLine.Text = "";
                        txtEditJobNo.Text = "";
                        txtJobItem.Text = "";

                        _mainItm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_itmcd"].Value.ToString();
                        _mainLine = Convert.ToInt16(dgvRevElement.Rows[e.RowIndex].Cells["Jcd_line"].Value); //updated by akila 2017/06/27
                        //_mainLine = Convert.ToInt16(dgvRevElement.Rows[e.RowIndex].Cells["jcd_joblineno"].Value);
                        _mainQty = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_qty"].Value);
                        _jobNo = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobno"].Value.ToString();
                        _jobItm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobitmcd"].Value.ToString();
                        _jobpb = dgvRevElement.Rows[e.RowIndex].Cells["jcd_pb"].Value.ToString();
                        _jobpblvl = dgvRevElement.Rows[e.RowIndex].Cells["jcd_pblvl"].Value.ToString();
                        _jobitmstus = dgvRevElement.Rows[e.RowIndex].Cells["jcd_itmstus"].Value.ToString();
                        _jobqty = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_qty"].Value);
                        _jobuprice = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_unitprice"].Value);
                        _jobamt = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_amt"].Value);
                        _jobdrate = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_dis_rt"].Value);
                        _jobdamt = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_dis"].Value);
                        _jobtax = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_tax"].Value);
                        _jobnet = Convert.ToDecimal(dgvRevElement.Rows[e.RowIndex].Cells["jcd_net_amt"].Value);

                        if (!LoadItemDetail(_mainItm))
                        {
                            MessageBox.Show("Invalid item selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        MasterItem _itmMas = new MasterItem();
                        _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItm);
                        lblIBrand.Text = _itmMas.Mi_brand;
                        lblIDesc.Text = string.IsNullOrEmpty(lblIDesc.Text) ? _itmMas.Mi_shortdesc : lblIDesc.Text;//updated by akila 2017/06/28
                        lblImodel.Text = _itmMas.Mi_model;

                        txtItem.Text = _mainItm;
                        txtQty.Text = _mainQty.ToString();
                        txtEditJobNo.Text = _jobNo;
                        txtEditJobLine.Text = _mainLine.ToString();
                        txtJobItem.Text = _jobItm;
                        //kapila 26/5/2017
                        cmbEditBook.Text = _jobpb;
                        cmbEditLvl.Text = _jobpblvl;
                        cmbStatus.Text = _jobitmstus;
                        txtQty.Text = _jobqty.ToString();
                        txtUnitPrice.Text = _jobuprice.ToString("0.00");
                        txtUnitAmt.Text = _jobamt.ToString("0.00");
                        txtDisRate.Text = _jobdrate.ToString("0.00");
                        txtDisAmt.Text = _jobdamt.ToString("0.00");
                        txtTaxAmt.Text = _jobtax.ToString("0.00");
                        txtLineTotAmt.Text = (_jobamt + _jobtax).ToString("0.00");
                        cmbEditBook.Focus();
                        txtItem.ReadOnly = true;
                        txtQty.ReadOnly = true;
                        btnSearch_Item.Enabled = false;
                        txtItem.ReadOnly = true;
                        pnlEdit.Visible = true;

                        if (_itmMas.Mi_is_editlongdesc == true)
                        {
                            lblIDesc.ReadOnly = false;
                        }
                        else
                        {
                            lblIDesc.ReadOnly = true;
                        }
                        //lblIDesc.Text = _itmMas.Mi_shortdesc; commented by akila. value has already assign
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)dgvRevElement.Rows[dgvRevElement.CurrentRow.Index].Cells[0];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "False":
                            {
                                ch1.Value = true;
                                break;
                            }
                        case "True":
                            {
                                ch1.Value = false;
                                break;
                            }
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (MessageBox.Show("Do you want to remove selected details ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Int16 _JobLine = 0;
                        string _jobNo = "";
                        string _jobItm = "";
                        string _itm = "";
                        string _rmk = "";


                        _JobLine = Convert.ToInt16(dgvRevElement.Rows[e.RowIndex].Cells["jcd_joblineno"].Value);
                        _jobNo = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobno"].Value.ToString();
                        _jobItm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_jobitmcd"].Value.ToString();
                        _itm = dgvRevElement.Rows[e.RowIndex].Cells["jcd_itmcd"].Value.ToString();
                        if (dgvRevElement.Rows[e.RowIndex].Cells["jcd_rmk"].Value != null)
                        {
                            _rmk = dgvRevElement.Rows[e.RowIndex].Cells["jcd_rmk"].Value.ToString();
                        }

                        if (_rmk != "ADD")
                        {
                            MessageBox.Show("Only allow to remove additional adding items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        List<Service_Confirm_detail> _temp = new List<Service_Confirm_detail>();
                        _temp = _serJobConfDet;


                        _temp.RemoveAll(x => x.Jcd_joblineno == _JobLine && x.Jcd_jobno == _jobNo && x.Jcd_itmcd == _itm && x.Jcd_jobitmcd == _jobItm);
                        _serJobConfDet = _temp;

                        dgvRevElement.AutoGenerateColumns = false;
                        dgvRevElement.DataSource = new Service_Confirm_detail();
                        dgvRevElement.DataSource = _serJobConfDet;
                        CalculateGP();
                    }
                }
            }
        }

        private void btnSearchCostItm_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCostItm;
                _CommonSearch.ShowDialog();
                txtCostItm.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCostItm_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCostItm.Text))
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtCostItm.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select valid item.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCostItm.Clear();
                        txtCostItm.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCostQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCostQty.Text))
            {
                if (!IsNumeric(txtCostQty.Text))
                {
                    MessageBox.Show("Please enter valid qty.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCostQty.Clear();
                    txtCostQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtCostQty.Text) <= 0)
                {
                    MessageBox.Show("Please enter valid qty.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCostQty.Clear();
                    txtCostQty.Focus();
                    return;
                }

            }
        }

        private void txtUnitCost_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUnitCost.Text))
            {
                if (!IsNumeric(txtUnitCost.Text))
                {
                    MessageBox.Show("Please enter valid unit cost.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitCost.Clear();
                    txtUnitCost.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtUnitCost.Text) <= 0)
                {
                    MessageBox.Show("Please enter valid unit cost.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitCost.Clear();
                    txtUnitCost.Focus();
                    return;
                }

            }
        }

        private void txtCostItm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCostQty.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearchCostItm_Click(null, null);
            }
        }

        private void txtCostItm_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCostItm_Click(null, null);
        }

        private void txtCostQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitCost.Focus();
            }
        }

        private void txtUnitCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCost.Focus();
            }
        }

        private void btnAddCost_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucPayModes1.RecieptItemList != null)
                {
                    if (ucPayModes1.RecieptItemList.Count > 0)
                    {
                        MessageBox.Show("You have already added payment.", "Job confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtCostItm.Text))
                {
                    MessageBox.Show("Please select valid cost item.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCostItm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCostQty.Text))
                {
                    MessageBox.Show("Please enter valid qty.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCostQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitCost.Text))
                {
                    MessageBox.Show("Please enter valid unit cost.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitCost.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblJobItemCost.Text))
                {
                    MessageBox.Show("Please select job item.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Service_Cost_sheet _tmpcostSheet = new Service_Cost_sheet();
                MasterItem _mstItm = new MasterItem();

                _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtCostItm.Text.Trim());

                _tmpcostSheet.CS_ACT = 1;
                _tmpcostSheet.CS_BALQTY = Convert.ToDecimal(txtCostQty.Text);
                _tmpcostSheet.CS_COM = BaseCls.GlbUserComCode;
                _tmpcostSheet.CS_CRE_BY = BaseCls.GlbUserID;
                _tmpcostSheet.CS_CUSCD = lblCusCode.Text.Trim();
                _tmpcostSheet.CS_CUSNAME = lblCusName.Text.Trim();
                _tmpcostSheet.CS_DIRECT = "IN";
                _tmpcostSheet.CS_DOCDT = dtDate.Value.Date;
                _tmpcostSheet.CS_DOCQTY = Convert.ToDecimal(txtCostQty.Text);
                _tmpcostSheet.CS_ITMCD = txtCostItm.Text;
                _tmpcostSheet.CS_ITMDESC = _mstItm.Mi_shortdesc;
                _tmpcostSheet.CS_ITMTP = _mstItm.Mi_itm_tp;
                _tmpcostSheet.CS_JOBCLOSE = 1;
                _tmpcostSheet.CS_JOBITMCD = lblJobItemCost.Text;
                _tmpcostSheet.CS_JOBITMSER = lblCostItemSer.Text;
                _tmpcostSheet.CS_JOBITMWARR = lblJobCostWara.Text;
                _tmpcostSheet.CS_JOBLINENO = Convert.ToInt32(lblJobCostLine.Text);
                _tmpcostSheet.CS_JOBNO = txtJobNo.Text.Trim();
                _tmpcostSheet.CS_LINE = _serCostSheet.Count + 1;
                _tmpcostSheet.CS_LOC = BaseCls.GlbUserDefLoca;
                _tmpcostSheet.CS_PC = BaseCls.GlbUserDefProf;
                _tmpcostSheet.CS_QTY = Convert.ToDecimal(txtCostQty.Text);
                _tmpcostSheet.CS_TOTUNITCOST = Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(txtCostQty.Text);
                _tmpcostSheet.CS_UNITCOST = Convert.ToDecimal(txtUnitCost.Text);
                _tmpcostSheet.CS_UOM = _mstItm.Mi_itm_uom;
                _tmpcostSheet.CS_DOCTP = "ADD";

                _serCostSheet.Add(_tmpcostSheet);

                dgvCostElement.AutoGenerateColumns = false;
                dgvCostElement.DataSource = new Service_Cost_sheet();
                dgvCostElement.DataSource = _serCostSheet;
                chkAddCost.Checked = false;

                CalculateGP();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAddCost_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddCost.Checked == true)
            {
                pnlJobCostDet.Visible = true;
                lblJobItemCost.Text = "";
                lblCostItemSer.Text = "";
                lblJobCostLine.Text = "";
                lblJobCostWara.Text = "";
                txtCostItm.Enabled = true;
                txtCostQty.Enabled = true;
                txtUnitCost.Enabled = true;
                txtCostItm.Text = "";
                txtCostQty.Text = "";
                txtUnitCost.Text = "";
                btnAddCost.Enabled = true;
                txtCostItm.Focus();
                chkAddNewRev.Checked = false;
            }
            else
            {
                pnlJobCostDet.Visible = false;
                lblJobItemCost.Text = "";
                lblCostItemSer.Text = "";
                lblJobCostLine.Text = "";
                lblJobCostWara.Text = "";
                txtCostItm.Enabled = false;
                txtCostQty.Enabled = false;
                txtUnitCost.Enabled = false;
                txtCostItm.Text = "";
                txtCostQty.Text = "";
                txtUnitCost.Text = "";
                btnAddCost.Enabled = false;

            }
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (chkInvNow.Checked == true)
            {
                //added by Wimal @ 19/07/2018 - torestrict Only Cash payments 
                decimal cashNEtAmt = 0;
                DataTable _result = CHNLSVC.General.SearchSalesTypes(":",  "MAIN TYPE","CASH");
                foreach (DataRow dr in _result.Rows)
                {
                    Console.Write(dr["TYPE"].ToString());
                    cashNEtAmt = cashNEtAmt + (from a in _serJobConfDet
                                               where a.Jcd_invtype == dr["TYPE"].ToString() 
                                               select a.Jcd_net_amt).Sum();                        
                }
                //ucPayModes1.TotalAmount = Convert.ToDecimal(txtTotRev.Text.Trim());
                //ucPayModes1.TotalAmount = Convert.ToDecimal(cashNEtAmt.ToString());
                ucPayModes1.TotalAmount = Convert.ToDecimal(txtTotRev.Text. ToString());
                ucPayModes1.Amount.Text = ucPayModes1.TotalAmount.ToString("N2");  // add by akila 2017/06/07
                ucPayModes1.Amount.Text = cashNEtAmt.ToString();

                LoadPayMode();
                pnlPayments.Visible = true;
            }
            else
            {
                MessageBox.Show("Payment can only add for invoice now option.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClosePayment_Click(object sender, EventArgs e)
        {
            pnlPayments.Visible = false;
        }

        private void chkInvNow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvNow.Checked == true)
            {

                foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                {
                    if (_tmpConf.Jcd_cuscd != lblCusCode.Text.Trim())
                    {
                        MessageBox.Show("Cannot invoice this level.Due to multiple customers selected.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkInvNow.Checked = false;
                        return;
                    }
                }

                //if (dgvRevElement.Rows.Count <= 0)
                //{
                //    MessageBox.Show("Please add revenue items first.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    chkInvNow.Checked = false;
                //    return;
                //}
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

        private void lblCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (chkChangeCus.Checked == true)
                    {
                        btnSearchCus_Click(null, null);
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lblCusCode.Text == "CASH")
                    {
                        lblCusName.Focus();
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

        private void btnSearchCus_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkChangeCus.Checked == true)
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 1;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                    //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = lblCusCode;
                    //_CommonSearch.IsSearchEnter = true;
                    //_CommonSearch.ShowDialog();
                    //lblCusCode.Select();

                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                    _commonSearch.ReturnIndex = 0;
                    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _commonSearch.dvResult.DataSource = _result;
                    _commonSearch.BindUCtrlDDLData(_result);
                    _commonSearch.obj_TragetTextBox = lblCusCode;
                    _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                    this.Cursor = Cursors.Default;
                    _commonSearch.ShowDialog();
                    lblCusCode.Select();
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

        private void lblCusCode_DoubleClick(object sender, EventArgs e)
        {
            if (chkChangeCus.Checked == true)
            {
                btnSearchCus_Click(null, null);
            }
        }

        private void checkCustomer(string _com, string _identification)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                List<MasterBusinessEntity> _customerList = new List<MasterBusinessEntity>();
                BlackListCustomers _blackListCustomers = new BlackListCustomers();
                string _cusCode = "";
                string _info = "";

                if (!string.IsNullOrEmpty(_identification))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, _identification.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {

                        _cusCode = _masterBusinessCompany.Mbe_cd;
                        _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                        if (_blackListCustomers != null)
                        {
                            if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                            {

                                _info = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                MessageBox.Show(_info, "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                            {
                                _info = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                MessageBox.Show(_info, "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {


                        }
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

        private void lblCusCode_Leave(object sender, EventArgs e)
        {           
            if (chkChangeCus.Checked == true)
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                if (!string.IsNullOrEmpty(lblCusCode.Text))
                {
                    if (lblCusCode.Text == "CASH")
                    {
                        MessageBox.Show("Please select valid customer code.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblCusCode.Text = "";
                        lblCusName.Text = "";
                        lblAdd1.Text = "";
                        lblAdd2.Text = "";
                        lblAdd3.Text = "";
                        chkTaxPayable.Checked = false;
                        lblCusName.ReadOnly = true;
                        lblAdd1.ReadOnly = true;
                        lblAdd2.ReadOnly = true;
                        return;
                    }
                    checkCustomer(null, lblCusCode.Text);
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, lblCusCode.Text.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        lblCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        lblCusName.Text = _masterBusinessCompany.Mbe_name;
                        lblAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        lblAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        chkTaxPayable.Checked = _masterBusinessCompany.Mbe_is_tax;

                        if (lblCusCode.Text == "CASH")
                        {
                            lblCusName.ReadOnly = false;
                            lblAdd1.ReadOnly = false;
                            lblAdd2.ReadOnly = false;

                        }
                        else
                        {
                            lblCusName.ReadOnly = true;
                            lblAdd1.ReadOnly = true;
                            lblAdd2.ReadOnly = true;
                        }

                        if (_masterBusinessCompany != null)
                        {
                            lblSvatStatus.Text = _masterBusinessCompany.Mbe_is_svat == true ? "Available" : "None";
                            lblVatExemptStatus.Text = _masterBusinessCompany.Mbe_tax_ex == true ? "Available" : "None";
                            lblVatExemStatus.Text = _masterBusinessCompany.Mbe_tax_ex == true ? "Available" : "None";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid customer.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblCusCode.Text = "";
                        lblCusName.Text = "";
                        lblAdd1.Text = "";
                        lblAdd2.Text = "";
                        lblAdd3.Text = "";
                        chkTaxPayable.Checked = false;
                        lblCusName.ReadOnly = true;
                        lblAdd1.ReadOnly = true;
                        lblAdd2.ReadOnly = true;
                        return;
                    }
                }
            }
        }

        private void btnPnlDelInsCancel_Click(object sender, EventArgs e)
        {
            pnlDeliveryInstruction.Visible = false;
        }

        private void btnAnotherCus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDelCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtDelCustomer.Select();

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

        private void txtDelCustomer_DoubleClick(object sender, EventArgs e)
        {
            btnAnotherCus_Click(null, null);
        }

        private void txtDelCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnAnotherCus_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDelName.Focus();
            }
        }

        private void btnOthCus_Click(object sender, EventArgs e)
        {
            if (chkInvNow.Checked)
            {
                MessageBox.Show("This option not applicable for Invoice now facility", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;            
            }
            if (pnlDeliveryInstruction.Visible == false)
            {
                pnlDeliveryInstruction.Visible = true;
                cmbInvType.Enabled = true;
                txtDelCustomer.Focus();
            }
            else
            {
                pnlDeliveryInstruction.Visible = false;
                cmbInvType.Enabled = false;
            }
        }

        private void btnPnlDelInsConfirm_Click(object sender, EventArgs e)
        {
            Boolean _appItm = false;
            string _jobItm = "";
            Int32 _jobLine = 0;
            string _jobNo = "";
            string _invItm = "";
            Int32 _confLine = 0;

            foreach (DataGridViewRow row in dgvRevElement.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["col_chkSelect"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _appItm = true;
                    goto L4;
                }
            }
        L4:

            if (_appItm == false)
            {
                MessageBox.Show("Please select items need to invoice with above customer.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkInvNow.Checked == true)
            {
                chkInvNow.Checked = false;
            }

            foreach (DataGridViewRow row in dgvRevElement.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["col_chkSelect"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _jobItm = row.Cells["jcd_jobitmcd"].Value.ToString();
                    _jobLine = Convert.ToInt16(row.Cells["jcd_joblineno"].Value);
                    _jobNo = row.Cells["jcd_jobno"].Value.ToString();
                    _invItm = row.Cells["jcd_itmcd"].Value.ToString();
                    _confLine = Convert.ToInt16(row.Cells["jcd_line"].Value);

                    foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                    {
                        if (_tmpConf.Jcd_jobitmcd == _jobItm && _tmpConf.Jcd_joblineno == _jobLine && _tmpConf.Jcd_jobno == _jobNo && _tmpConf.Jcd_itmcd == _invItm && _tmpConf.Jcd_line == _confLine)
                        {
                            _tmpConf.Jcd_cuscd = txtDelCustomer.Text.Trim();
                            _tmpConf.Jcd_cusname = txtDelName.Text.Trim();
                            _tmpConf.Jcd_cusadd1 = txtDelAddress1.Text.Trim();
                            _tmpConf.Jcd_cusadd2 = txtDelAddress2.Text.Trim();
                            _tmpConf.Jcd_invtype = cmbInvType.Text;

                            decimal _vatPortion = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(_tmpConf.Jcd_dis_rt), true, _tmpConf.Jcd_invtype);
                            //decimal _vatPortion = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(_tmpConf.Jcd_dis_rt), true, _tmpConf.Jcd_invtype), true);
                            _tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);

                            decimal _totalAmount = FigureRoundUp(_tmpConf.Jcd_amt + Convert.ToDecimal(_tmpConf.Jcd_tax) - Convert.ToDecimal(_tmpConf.Jcd_dis), true);
                            _tmpConf.Jcd_net_amt = FigureRoundUp(_totalAmount, true);
                        }
                    }
                }
            }

            txtDelCustomer.Text = "";
            txtDelName.Text = "";
            txtDelAddress1.Text = "";
            txtDelAddress2.Text = "";
            dgvRevElement.AutoGenerateColumns = false;
            dgvRevElement.DataSource = new Service_Confirm_detail();
            dgvRevElement.DataSource = _serJobConfDet;
            pnlDeliveryInstruction.Visible = false;
            cmbInvType.Enabled = false;

        }

        private void chkChangeCus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangeCus.Checked == true)
            {
                lblCusCode.ReadOnly = false;
            }
            else
            {
                lblCusCode.ReadOnly = true;
            }
        }

        private void txtDelCustomer_Leave(object sender, EventArgs e)
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

            if (!string.IsNullOrEmpty(txtDelCustomer.Text))
            {
                checkCustomer(null, txtDelCustomer.Text);
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtDelCustomer.Text.Trim(), string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                {
                    txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                    txtDelName.Text = _masterBusinessCompany.Mbe_name;
                    txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                    txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;

                    if (txtDelCustomer.Text == "CASH")
                    {
                        txtDelName.ReadOnly = false;
                        txtDelAddress1.ReadOnly = false;
                        txtDelAddress2.ReadOnly = false;

                    }
                    else
                    {
                        txtDelName.ReadOnly = true;
                        txtDelAddress1.ReadOnly = true;
                        txtDelAddress2.ReadOnly = true;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid customer.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDelCustomer.Text = "";
                    txtDelName.Text = "";
                    txtDelAddress1.Text = "";
                    txtDelAddress2.Text = "";
                    txtDelName.ReadOnly = true;
                    txtDelAddress1.ReadOnly = true;
                    txtDelAddress2.ReadOnly = true;
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            pnlStandByItems.Visible = false;
            _isStandBy = false;
        }

        private void btnStandByConfirm_Click(object sender, EventArgs e)
        {
            if (chkInvNow.Checked == false)
            {
                MessageBox.Show("Stand by items can only add if invoice is generate now.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isStandBy = false;
                return;
            }

            if (MessageBox.Show("Do you want to confirm ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (oTempIssueList.Count <= 0)
                {
                    MessageBox.Show("Error occure while confirm.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isStandBy = false;
                    return;
                }

                chkInvNow.Checked = true;
                chkInvNow.Enabled = false;
                Service_Confirm_detail _rtntmpList = new Service_Confirm_detail();
                Service_Cost_sheet _rtncostList = new Service_Cost_sheet();
                string _jobItm = "";
                string _jobSer = "";
                string _jobWara = "";

                foreach (Service_TempIssue _tmpIssu in oTempIssueList)
                {
                    MasterItem _itm = CHNLSVC.Inventory.GetItem(_tmpIssu.STI_COM, _tmpIssu.STI_ISSUEITMCD);
                    if (_itm != null)
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            List<InventorySerialRefN> _serDet = CHNLSVC.Inventory.GetItemDetailBySerial(_tmpIssu.STI_COM, _tmpIssu.STI_LOC, _tmpIssu.STI_ISSUESERIALNO);
                            if (_serDet != null)
                            {
                                foreach (InventorySerialRefN _tmpSer in _serDet)
                                {
                                    _rtntmpList = new Service_Confirm_detail();
                                    _rtntmpList.Jcd_jobno = _tmpIssu.STI_JOBNO;
                                    _rtntmpList.Jcd_joblineno = _tmpIssu.STI_JOBLINENO;
                                    _rtntmpList.Jcd_itmcd = _tmpSer.Ins_itm_cd;
                                    _rtntmpList.Jcd_itmstus = _tmpSer.Ins_itm_stus;
                                    _rtntmpList.Jcd_qty = 1;
                                    _rtntmpList.Jcd_balqty = 1;
                                    _rtntmpList.Jcd_pb = "";
                                    _rtntmpList.Jcd_pblvl = "";
                                    _rtntmpList.Jcd_unitprice = 0;
                                    _rtntmpList.Jcd_amt = 0;
                                    _rtntmpList.Jcd_tax_rt = 0;
                                    _rtntmpList.Jcd_tax = 0;
                                    _rtntmpList.Jcd_dis_rt = 0;
                                    _rtntmpList.Jcd_net_amt = 0;
                                    _rtntmpList.Jcd_itmtp = _itm.Mi_itm_tp;
                                    _jobItm = (from _jobLst in _processJobDet
                                               where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                               select _jobLst.Jbd_itm_cd).ToList<string>()[0];

                                    _jobSer = (from _jobLst in _processJobDet
                                               where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                               select _jobLst.Jbd_ser1).ToList<string>()[0];

                                    _jobWara = (from _jobLst in _processJobDet
                                                where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                                select _jobLst.Jbd_warr).ToList<string>()[0];

                                    _rtntmpList.Jcd_jobitmcd = _jobItm;
                                    _rtntmpList.Jcd_jobitmser = _jobSer;
                                    _rtntmpList.Jcd_jobwarrno = _jobWara;
                                    _rtntmpList.Jcd_itmdesc = _itm.Mi_shortdesc;
                                    _rtntmpList.Jcd_itmmodel = _itm.Mi_model;
                                    _rtntmpList.Jcd_itmbrand = _itm.Mi_brand;
                                    _rtntmpList.Jcd_itmuom = _itm.Mi_itm_uom;
                                    _rtntmpList.Jcd_mov_doc = _tmpSer.Ins_doc_no;
                                    _rtntmpList.Jcd_itmline = _tmpSer.Ins_itm_line;
                                    _rtntmpList.Jcd_batchline = _tmpSer.Ins_batch_line;
                                    _rtntmpList.Jcd_serline = _tmpSer.Ins_ser_line;
                                    _rtntmpList.Jcd_ser_id = _tmpSer.Ins_ser_id;
                                    _rtntmpList.Jcd_line = _serJobConfDet.Count + 1;
                                    _serJobConfDet.Add(_rtntmpList);

                                    //Adding Cost
                                    _rtncostList = new Service_Cost_sheet();
                                    _rtncostList.CS_JOBNO = _tmpIssu.STI_JOBNO;
                                    _rtncostList.CS_JOBLINENO = _tmpIssu.STI_JOBLINENO;
                                    _rtncostList.CS_COM = _tmpIssu.STI_COM;
                                    _rtncostList.CS_LOC = _tmpIssu.STI_LOC;
                                    _rtncostList.CS_PC = BaseCls.GlbUserDefProf;
                                    _rtncostList.CS_JOBITMCD = _jobItm;
                                    _rtncostList.CS_JOBITMSER = _jobSer;
                                    _rtncostList.CS_JOBITMWARR = _jobWara;
                                    _rtncostList.CS_ITMCD = _tmpSer.Ins_itm_cd;
                                    _rtncostList.CS_ITMSTUS = _tmpSer.Ins_itm_stus;
                                    _rtncostList.CS_ITMSER = _tmpSer.Ins_ser_1;
                                    _rtncostList.CS_UOM = _itm.Mi_itm_uom;
                                    _rtncostList.CS_QTY = 1;
                                    _rtncostList.CS_DIRECT = "IN";
                                    _rtncostList.CS_UNITCOST = _tmpSer.Ins_unit_cost;
                                    _rtncostList.CS_TOTUNITCOST = _tmpSer.Ins_unit_cost * 1;
                                    _rtncostList.CS_DOCTP = "";
                                    _rtncostList.CS_DOCNO = _tmpSer.Ins_doc_no;
                                    _rtncostList.CS_SERID = _tmpSer.Ins_ser_id;
                                    _rtncostList.CS_DOCDT = _tmpSer.Ins_doc_dt;
                                    _rtncostList.CS_ITMTP = _itm.Mi_itm_tp;
                                    _rtncostList.CS_ITMDESC = _itm.Mi_shortdesc;
                                    _rtncostList.CS_ISFOC = 0;
                                    _rtncostList.CS_ISREVITM = 0;
                                    _rtncostList.CS_ACT = 1;
                                    _serCostSheet.Add(_rtncostList);
                                }
                            }
                        }
                        else
                        {
                            _rtntmpList = new Service_Confirm_detail();
                            _rtntmpList.Jcd_jobno = _tmpIssu.STI_JOBNO;
                            _rtntmpList.Jcd_joblineno = _tmpIssu.STI_JOBLINENO;
                            _rtntmpList.Jcd_itmcd = _tmpIssu.STI_ISSUEITMCD;
                            _rtntmpList.Jcd_itmstus = _tmpIssu.STI_ISSUEITMSTUS;
                            _rtntmpList.Jcd_qty = _tmpIssu.STI_BALQTY;
                            _rtntmpList.Jcd_balqty = _tmpIssu.STI_BALQTY;
                            _rtntmpList.Jcd_pb = "";
                            _rtntmpList.Jcd_pblvl = "";
                            _rtntmpList.Jcd_unitprice = 0;
                            _rtntmpList.Jcd_amt = 0;
                            _rtntmpList.Jcd_tax_rt = 0;
                            _rtntmpList.Jcd_tax = 0;
                            _rtntmpList.Jcd_dis_rt = 0;
                            _rtntmpList.Jcd_net_amt = 0;
                            _rtntmpList.Jcd_itmtp = _itm.Mi_itm_tp;
                            _jobItm = (from _jobLst in _processJobDet
                                       where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                       select _jobLst.Jbd_itm_cd).ToList<string>()[0];

                            _jobSer = (from _jobLst in _processJobDet
                                       where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                       select _jobLst.Jbd_ser1).ToList<string>()[0];

                            _jobWara = (from _jobLst in _processJobDet
                                        where _jobLst.Jbd_jobline == _tmpIssu.STI_JOBLINENO && _jobLst.Jbd_jobno == _tmpIssu.STI_JOBNO
                                        select _jobLst.Jbd_warr).ToList<string>()[0];

                            _rtntmpList.Jcd_jobitmcd = _jobItm;
                            _rtntmpList.Jcd_jobitmser = _jobSer;
                            _rtntmpList.Jcd_jobwarrno = _jobWara;
                            _rtntmpList.Jcd_itmdesc = _itm.Mi_shortdesc;
                            _rtntmpList.Jcd_itmmodel = _itm.Mi_model;
                            _rtntmpList.Jcd_itmbrand = _itm.Mi_brand;
                            _rtntmpList.Jcd_itmuom = _itm.Mi_itm_uom;
                            _rtntmpList.Jcd_mov_doc = null;
                            _rtntmpList.Jcd_itmline = 0;
                            _rtntmpList.Jcd_batchline = 0;
                            _rtntmpList.Jcd_serline = 0;
                            _rtntmpList.Jcd_ser_id = 0;
                            _rtntmpList.Jcd_line = _serJobConfDet.Count + 1;
                            _serJobConfDet.Add(_rtntmpList);

                            //Cost for none serial
                            _rtncostList = new Service_Cost_sheet();
                            _rtncostList.CS_JOBNO = _tmpIssu.STI_JOBNO;
                            _rtncostList.CS_JOBLINENO = _tmpIssu.STI_JOBLINENO;
                            _rtncostList.CS_COM = _tmpIssu.STI_COM;
                            _rtncostList.CS_LOC = _tmpIssu.STI_LOC;
                            _rtncostList.CS_PC = BaseCls.GlbUserDefProf;
                            _rtncostList.CS_JOBITMCD = _jobItm;
                            _rtncostList.CS_JOBITMSER = _jobSer;
                            _rtncostList.CS_JOBITMWARR = _jobWara;
                            _rtncostList.CS_ITMCD = _tmpIssu.STI_ISSUEITMCD;
                            _rtncostList.CS_ITMSTUS = _tmpIssu.STI_ISSUEITMSTUS;
                            _rtncostList.CS_ITMSER = "N/A";
                            _rtncostList.CS_UOM = _itm.Mi_itm_uom;
                            _rtncostList.CS_QTY = _tmpIssu.STI_BALQTY;
                            _rtncostList.CS_DIRECT = "IN";

                            decimal _LtsCost = CHNLSVC.Inventory.GetLatestCost(_tmpIssu.STI_COM, _tmpIssu.STI_LOC, _tmpIssu.STI_ISSUEITMCD, _tmpIssu.STI_ISSUEITMSTUS);

                            _rtncostList.CS_UNITCOST = _LtsCost;
                            _rtncostList.CS_TOTUNITCOST = _LtsCost * _tmpIssu.STI_BALQTY;
                            _rtncostList.CS_DOCTP = "";
                            _rtncostList.CS_DOCNO = "";
                            _rtncostList.CS_SERID = 0;
                            _rtncostList.CS_DOCDT = DateTime.Now.Date;
                            _rtncostList.CS_ITMTP = _itm.Mi_itm_tp;
                            _rtncostList.CS_ITMDESC = _itm.Mi_shortdesc;
                            _rtncostList.CS_ISFOC = 0;
                            _rtncostList.CS_ISREVITM = 0;
                            _rtncostList.CS_ACT = 1;
                            _serCostSheet.Add(_rtncostList);
                        }
                    }
                }
                dgvCostElement.AutoGenerateColumns = false;
                dgvCostElement.DataSource = new List<Service_Cost_sheet>();
                dgvCostElement.DataSource = _serCostSheet;

                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new List<Service_job_Det>();
                dgvRevElement.DataSource = _serJobConfDet;
                _isStandBy = true;
                btnStandBy.Enabled = false;
                dgvItems.AutoGenerateColumns = false;
                dgvItems.DataSource = new List<Service_TempIssue>();
                pnlStandByItems.Visible = false;
            }
            else
            {
                _isStandBy = false;
                return;
            }
        }

        private void btnStandBy_Click(object sender, EventArgs e)
        {
            oTempIssueList = new List<Service_TempIssue>();

            foreach (Service_job_Det _det in _processJobDet)
            {
                oTempIssueList.AddRange(CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, _det.Jbd_jobno, _det.Jbd_jobline, string.Empty, BaseCls.GlbUserDefLoca, "STBYI"));
            }
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            dgvItems.DataSource = oTempIssueList;

            if (dgvItems.Rows.Count > 0)
            {
                pnlStandByItems.Show();
                return;
            }
            else
            {
                MessageBox.Show("No any stand by items are available.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtCommanDis_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCommanDis.Text))
            {
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please select job number.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommanDis.Text = "";
                    txtCommanDis.Focus();
                    return;
                }

                if (!IsNumeric(txtCommanDis.Text))
                {
                    MessageBox.Show("Please enter valid discount rate.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommanDis.Text = "";
                    txtCommanDis.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtCommanDis.Text) > 100)
                {
                    MessageBox.Show("Please enter valid discount rate.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommanDis.Text = "";
                    txtCommanDis.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtCommanDis.Text) < 0)
                {
                    MessageBox.Show("Please enter valid discount rate.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommanDis.Text = "";
                    txtCommanDis.Focus();
                    return;
                }

            }
        }

        private void btnDisApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommanDis.Text))
            {
                MessageBox.Show("Please enter valid discount rate.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCommanDis.Text = "";
                txtCommanDis.Focus();
                return;
            }

            if (Convert.ToDecimal(txtCommanDis.Text.Trim()) > 0)
            {
                if (_MasterProfitCenter.Mpc_def_dis_rate < Convert.ToDecimal(txtCommanDis.Text.Trim()))
                {
                    MessageBox.Show("Allowed maximum discount rate exceed.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommanDis.Text = "";
                    txtCommanDis.Focus();
                    return;
                }
            }


            if (MessageBox.Show("Confirm to apply all items ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (Service_Confirm_detail _tmpConf in _serJobConfDet)
                {
                    //kapila 26/5/2017 AAL service mod
                    foreach (DataGridViewRow row in dgvRevElement.Rows)
                    {
                        if (row.Cells[4].Value.ToString() == _tmpConf.Jcd_itmcd && row.Cells[6].Value.ToString() == _tmpConf.Jcd_itmstus && Convert.ToDecimal(row.Cells[7].Value) == Convert.ToDecimal(_tmpConf.Jcd_qty) && Convert.ToDecimal(row.Cells[8].Value) == Convert.ToDecimal(_tmpConf.Jcd_unitprice))
                        {
                            Int32 _r = Convert.ToInt32(row.Cells[0].Value);
                            if (_r == 1)
                                _tmpConf.Jcd_dis_rt = Convert.ToDecimal(txtCommanDis.Text);
                            else
                                _tmpConf.Jcd_dis_rt = 0;
                            break;
                        }
                    }
                    //_tmpConf.Jcd_dis_rt = Convert.ToDecimal(txtCommanDis.Text);
                    decimal _vatPortion = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(txtCommanDis.Text), true, _tmpConf.Jcd_invtype);
                    //decimal _vatPortion = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(txtCommanDis.Text), true, _tmpConf.Jcd_invtype), true);
                    _tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);

                    decimal _totalAmount = Convert.ToDecimal(_tmpConf.Jcd_qty) * Convert.ToDecimal(_tmpConf.Jcd_unitprice);

                    //decimal _disAmt = 0;
                    //_disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100), true);

                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    //Add by akila 2017/07/19
                    decimal _disAmt = 0;
                    if (_isVATInvoice)
                    {
                        _disAmt = _totalAmount * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100);
                    }
                    else
                    {
                        _disAmt = (_totalAmount + _vatPortion) * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100);
                    }
                    _tmpConf.Jcd_dis = FigureRoundUp(_disAmt, true);

                    //if (_isVATInvoice)
                    //    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100), true);
                    //else
                    //{
                    //    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) / 100), true);
                    //    if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                    //    {
                    //        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _tmpConf.Jcd_itmcd, Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                    //        if (_tax != null && _tax.Count > 0)
                    //        {
                    //            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                    //            _tmpConf.Jcd_tax = FigureRoundUp(_vatval, true);
                    //        }
                    //    }
                    //}

                    //_tmpConf.Jcd_dis = FigureRoundUp(_disAmt, true);

                    //if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                    //    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    //else
                   // _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_tmpConf.Jcd_tax) - _disAmt, true);
                    //}

                    //_tmpConf.Jcd_net_amt = FigureRoun[[dUp(_totalAmount, true);

                    if (Convert.ToDecimal(_tmpConf.Jcd_dis_rt) > 0)
                        _vatPortion = TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(txtCommanDis.Text), false, _tmpConf.Jcd_invtype);
                        //_vatPortion = FigureRoundUp(TaxCalculation(_tmpConf.Jcd_itmcd, _tmpConf.Jcd_itmstus, Convert.ToDecimal(_tmpConf.Jcd_qty), _priceBookLevelRef, Convert.ToDecimal(_tmpConf.Jcd_unitprice), 0, Convert.ToDecimal(txtCommanDis.Text), false, _tmpConf.Jcd_invtype), true);

                    if (_isVATInvoice) { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_vatPortion) - _disAmt, true); }
                    else { _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(_tmpConf.Jcd_tax) - _disAmt, true); }
                    _tmpConf.Jcd_net_amt = FigureRoundUp(_totalAmount, true);
                    
                    _tmpConf.Jcd_tax = Convert.ToDecimal(_vatPortion);
                }
                dgvRevElement.AutoGenerateColumns = false;
                dgvRevElement.DataSource = new List<Service_Confirm_detail>();
                dgvRevElement.DataSource = _serJobConfDet;
                CalculateGP(); //Add by akila  2017/06/15(Calculate GP after apply discounts)
            }
        }

        private void cmbInvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessCost.Focus();
            }
        }

        private void chkTaxPayable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaxPayable.Checked == true)
            {
                MasterBusinessEntity _busEntity = new MasterBusinessEntity();
                _busEntity = CHNLSVC.Sales.GetCustomerProfileByCom(lblCusCode.Text.Trim(), null, null, null, null, BaseCls.GlbUserComCode);

                if (_busEntity != null)
                {
                    if (_busEntity.Mbe_is_tax == false)
                    {
                        MessageBox.Show("Customer not setup as tax customer.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkTaxPayable.Checked = false;
                        return;
                    }
                }
            }
        }

        private void btnPnlDelInsClear_Click(object sender, EventArgs e)
        {
            txtDelCustomer.Text = "";
            txtDelName.Text = "";
            txtDelAddress1.Text = "";
            txtDelAddress2.Text = "";
            txtDelCustomer.Focus();
        }

        private void chkAddNewRev_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddNewRev.Checked == true)
            {
                chkEditItm.Checked = false;
                btnSearch_Item.Enabled = true;
                txtQty.ReadOnly = false;
                txtUnitPrice.ReadOnly = false;
                lblJobCap.Visible = true;
                txtItem.ReadOnly = false;
                chkAddCost.Checked = false;
            }
            else
            {
                lblJobCap.Visible = false;
            }

        }

        private void dgvCostElement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCostElement.ColumnCount > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Do you want to remove selected details ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Int16 _JobLine = 0;
                        string _jobNo = "";
                        string _jobItm = "";
                        string _itm = "";
                        string _rmk = "";


                        _JobLine = Convert.ToInt16(dgvCostElement.Rows[e.RowIndex].Cells["cs_joblineno"].Value);
                        _jobNo = dgvCostElement.Rows[e.RowIndex].Cells["cs_jobno"].Value.ToString();
                        _jobItm = dgvCostElement.Rows[e.RowIndex].Cells["cs_jobitmcd"].Value.ToString();
                        _itm = dgvCostElement.Rows[e.RowIndex].Cells["cs_itmcd"].Value.ToString();
                        if (dgvCostElement.Rows[e.RowIndex].Cells["cs_doctp"].Value != null)
                        {
                            _rmk = dgvCostElement.Rows[e.RowIndex].Cells["cs_doctp"].Value.ToString();
                        }

                        if (_rmk != "ADD")
                        {
                            MessageBox.Show("Only allow to remove additional adding items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        List<Service_Cost_sheet> _temp = new List<Service_Cost_sheet>();
                        _temp = _serCostSheet;


                        _temp.RemoveAll(x => x.CS_JOBLINENO == _JobLine && x.CS_JOBNO == _jobNo && x.CS_ITMCD == _itm && x.CS_JOBITMCD == _jobItm);
                        _serCostSheet = _temp;

                        dgvCostElement.AutoGenerateColumns = false;
                        dgvCostElement.DataSource = new Service_Confirm_detail();
                        dgvCostElement.DataSource = _serCostSheet;
                        CalculateGP();

                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10805))
            {
                MessageBox.Show("Permission denied. Required permission code : 10805", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                pnlCancel.Visible = true;
            }
        }

        private void btnCancelClose_Click(object sender, EventArgs e)
        {
            pnlCancel.Visible = false;
        }

        private void btnSearchCanJob_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanJob;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtCanJob.Focus();
                _CommonSearch.ShowDialog();
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

        private void txtCanJob_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanJob;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtCanJob.Focus();
                _CommonSearch.ShowDialog();
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

        private void txtCanJob_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCanJob;
                    this.Cursor = Cursors.Default;
                    _CommonSearch.IsSearchEnter = true;
                    txtCanJob.Focus();
                    _CommonSearch.ShowDialog();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCanConf.Focus();
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

        private void btnSearchCanConf_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCanJob.Text))
                {
                    MessageBox.Show("Please select job number first.", "Cancel confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanJob.Focus();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ConfByJob);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_BY_JOB(_CommonSearch.SearchParams, null, null);
                //remove cancel records - Wimal  @ q9/07/2018
                foreach (DataRow dr in _result.Rows)
                {
                    if (dr["STUS"].ToString() == "C")
                        dr.Delete();
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanConf;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtCanConf.Focus();
                _CommonSearch.ShowDialog();

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

        private void txtCanConf_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtCanJob.Text))
                    {
                        MessageBox.Show("Please select job number first.", "Cancel confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCanJob.Focus();
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ConfByJob);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_BY_JOB(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCanConf;
                    this.Cursor = Cursors.Default;
                    _CommonSearch.IsSearchEnter = true;
                    txtCanConf.Focus();
                    _CommonSearch.ShowDialog();
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

        private void txtCanConf_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCanJob.Text))
                {
                    MessageBox.Show("Please select job number first.", "Cancel confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanJob.Focus();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ConfByJob);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_BY_JOB(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanConf;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtCanConf.Focus();
                _CommonSearch.ShowDialog();

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

        private void cmbCancel_Click(object sender, EventArgs e)
        {
            string _err = "";
            if (string.IsNullOrEmpty(txtCanJob.Text))
            {
                MessageBox.Show("Please select job number first.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCanJob.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCanConf.Text))
            {
                MessageBox.Show("Please select confirmation number.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCanConf.Focus();
                return;
            }

            Service_confirm_Header _confByJob = new Service_confirm_Header();
            _confByJob = CHNLSVC.CustService.GetConfDetByJob(BaseCls.GlbUserComCode, txtCanJob.Text.Trim(), txtCanConf.Text.Trim());

            if (_confByJob.Jch_com != null)
            {
                if (_confByJob.Jch_stus == "C")
                {
                    MessageBox.Show("Selected confirmation is already cancelled.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanConf.Focus();
                    return;
                }
                if (_confByJob.Jch_stus != "A")
                {
                    MessageBox.Show("The status of selected confirmation not allow to cancel.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanConf.Focus();
                    return;
                }
                if (_confByJob.Jch_isdoneinvoiced == 1)
                {
                    MessageBox.Show("Selected confirmation is already invoiced.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanConf.Focus();
                    return;
                }

                //get confirmation item details to check any request if close type wrpl

                //   if (_confByJob.Jch_jobclosetp == "WRPL")
                if (!string.IsNullOrEmpty(_confByJob.Jch_jobclosetp))
                {
                    List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel).Where(X => X.SCT_TP == _confByJob.Jch_jobclosetp).ToList();
                    if (oCloseType != null)
                    {
                        if (oCloseType[0].SCT_REPL_ALLOW == 1)//Nadeeka 24-08-2015
                        {
                            _isRplAllow = true;
                        }
                        else
                        { _isRplAllow = false; }
                    }
                }

                if (_isRplAllow == true)
                {
                    List<Service_Confirm_detail> _confDet = new List<Service_Confirm_detail>();
                    _confDet = CHNLSVC.CustService.GetServiceConfirmDetials(BaseCls.GlbUserComCode, _confByJob.Jch_seq, txtCanConf.Text.Trim());

                    if (_confDet != null)
                    {
                        foreach (Service_Confirm_detail _list in _confDet)
                        {
                            List<RequestApprovalHeader> _reqApp = new List<RequestApprovalHeader>();
                            _reqApp = CHNLSVC.CustService.GetWarrRepReqByJobNumber(txtCanJob.Text.Trim(), _list.Jcd_joblineno);

                            if (_reqApp != null)
                            {
                                foreach (RequestApprovalHeader _appDet in _reqApp)
                                {
                                    if (_appDet.Grah_app_stus == "P" || _appDet.Grah_app_stus == "A" || _appDet.Grah_app_stus == "R" || _appDet.Grah_app_stus == "C")
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Cannot cancel. Warranty replacement already proceed.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtCanConf.Focus();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }


                int _effect = CHNLSVC.CustService.JobConfCancel(BaseCls.GlbUserComCode, txtCanJob.Text.Trim(), txtCanConf.Text.Trim(), _confByJob.Jch_seq, "C", BaseCls.GlbUserID, out _err);

                if (_effect == 1)
                {
                    MessageBox.Show("Sucsessfully cancelled.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCanConf.Text = "";
                    txtCanJob.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Error while cancelling. " + _err, "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Cannot find valid confirmation.", "Confirmation Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCanConf.Focus();
                return;
            }
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtJobNo.Text, 0, lblCusCode.Text);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnJobTasks_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select the job number.", "Job Tasks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ServiceTasks frm = new ServiceTasks(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ServiceReturnStandBy frm = new ServiceReturnStandBy();
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnSearch_DispatchRequired_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                DataTable _result=new DataTable(); 
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CLS_ALW_LOC);

                _result = CHNLSVC.CommonSearch.Get_CLS_ALW_LOC_SearchData(_CommonSearch.SearchParams, null, null);
                if (BaseCls.GlbUserComCode=="ABE") // add by tharanga 2017/10/24
                {
                    _result = CHNLSVC.CustService.get_SCV_CLS_ALW_LOC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "WRPL", 1, BaseCls.GlbUserDefLoca);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDispatchRequried;
                _CommonSearch.ShowDialog();
                txtDispatchRequried.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtDispatchRequried_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (ddlCloseType.SelectedValue.ToString() == "WRPL")
                //{
                //    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                //    {
                //        MessageBox.Show("Please enter old part receive location.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtDispatchRequried.Focus();
                //        return;
                //    }
                //}

                if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    //if (txtDispatchRequried.Text != "N/A")
                    //{
                    if (IsValidLocation() == false)
                    {
                        MessageBox.Show("Please check the dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchRequried.Clear();
                        txtDispatchRequried.Focus();
                        return;
                    }
                    //kapila 10/5/2016
                    DataTable _dt = null;
                    if (_isDefIssuLoc == true)
                    {
                        _dt = CHNLSVC.CustService.get_SCV_CLS_ALW_LOC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlCloseType.SelectedValue.ToString(), 0, txtDispatchRequried.Text);
                        if (_dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Invalid issue location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDispatchRequried.Clear();
                            txtDispatchRequried.Focus();
                            return;
                        }
                        else
                        {

                        }
                    }
                }

                //txtPreferLoc.Text = txtDispatchRequried.Text;
                //}
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private bool IsValidLocation()
        {
            bool status = false;
            if (BaseCls.GlbUserComCode == "ABE")// add by tharanga 2017/10/23
            {
                InvoiceHeader dtinv = CHNLSVC.Sales.GetInvoiceHeader(invno); // add by tharanga 2017/10/23
                InventoryHeader _InventoryHeader = CHNLSVC.Inventory.GetINTHDRByOthDoc(dtinv.Sah_com, "DO", dtinv.Sah_inv_no);
                if (_InventoryHeader != null)
                {
                   txtPreferLoc.Text=_InventoryHeader.Ith_loc;
                   othcom = _InventoryHeader.Ith_com;
                   status = true;
                }
                if ((dtinv != null ) && (!string.IsNullOrEmpty(dtinv.Sah_inv_no))) //updated by akila 2018/02/02
                {
                    DataTable dtloc = CHNLSVC.Inventory.Get_location_by_code(dtinv.Sah_com, txtDispatchRequried.Text);
                    if (dtloc.Rows.Count > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        //MessageBox.Show("Dispatch location details cannot found.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        status = false;
                    }

                }
                else
                {
                    //MessageBox.Show("Dispatch location details cannot found.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    status = false;
                }
            }
            else
            {
                txtDispatchRequried.Text = txtDispatchRequried.Text.Trim().ToUpper().ToString();
                MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtDispatchRequried.Text.ToString());
                status = (_masterLocation == null) ? false : true;
            }
            return status;
        }

        private bool IsValidPreferLocation()
        {
            bool status = false;
            if (BaseCls.GlbUserComCode == "ABE")// add by tharanga 2017/10/23
            {
                InvoiceHeader dtinv = CHNLSVC.Sales.GetInvoiceHeader(invno); // add by tharanga 2017/10/23
                //InventoryHeader _InventoryHeader = CHNLSVC.Inventory.GetINTHDRByOthDoc(dtinv.Sah_com, "DO", dtinv.Sah_inv_no);
                //if (_InventoryHeader != null)
                //{
                //    txtPreferLoc.Text = _InventoryHeader.Ith_loc;
                //    status = true;
                //}
                if ((dtinv != null) && (!string.IsNullOrEmpty(dtinv.Sah_inv_no))) //updated by akila 2018/02/02
                {
                    DataTable dtloc = CHNLSVC.Inventory.Get_location_by_code(dtinv.Sah_com, txtPreferLoc.Text);
                    if (dtloc.Rows.Count > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }

                }
                else
                {
                    //MessageBox.Show("Dispatch location details cannot found.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    status = false;
                }
            }
            else
            {
                txtPreferLoc.Text = txtPreferLoc.Text.Trim().ToUpper().ToString();
                MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtPreferLoc.Text.ToString());
                status = (_masterLocation == null) ? false : true;
               
            }
            return status;
        }

        private void ddlCloseType_SelectedIndexChanged(object sender, EventArgs e)
        { // Nadeeka 10-06-2015
            if (!string.IsNullOrEmpty(ddlCloseType.Text))
            {
                _isDefIssuLoc = false;
                List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel).Where(X => X.SCT_TP == ddlCloseType.SelectedValue.ToString()).ToList();
                if (oCloseType != null)
                {
                    if (oCloseType[0].SCT_REPL_ALLOW == 1)//Nadeeka 24-08-2015
                    {
                        _isRplAllow = true;
                    }
                    else
                    { _isRplAllow = false; }
                }


                if (_isRplAllow == true)
                {
                    pnlIssueLoc.Visible = true;
                    //if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    //{
                    txtDispatchRequried.Text = "";

                    //kapila 10/5/2016
                    DataTable _dt = CHNLSVC.CustService.get_SCV_CLS_ALW_LOC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlCloseType.SelectedValue.ToString(), 1, null);
                    if (_dt.Rows.Count > 0)
                    {
                        if (BaseCls.GlbUserComCode == "ABE")
                        { txtDispatchRequried.Text = _dt.Rows[0]["Location"].ToString(); }
                        else
                        {
                            txtDispatchRequried.Text = _dt.Rows[0]["scal_alw_loc"].ToString();
                        }
                        _isDefIssuLoc = true;
                    }

                    txtPreferLoc.Text = "";
                    //}
                 
                }
                else
                {
                    pnlIssueLoc.Visible = false;
                    txtDispatchRequried.Text = "";
                    txtPreferLoc.Text = "";
                }
            }
            if (BaseCls.GlbUserComCode=="ABE")
            {
                txtDispatchRequried_Leave(null, null);  
            }
            
        }

        private void txtPreferLoc_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtPreferLoc.Text))
                {
                    if (IsValidPreferLocation() == false)
                    {
                        MessageBox.Show("Please select valid customer prefer location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPreferLoc.Clear();
                        txtPreferLoc.Focus();
                        return;
                    }
                }


                //}
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearchPreferLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                 DataTable _result1=new DataTable ();
                 DataTable _result2 = new DataTable();
                if (BaseCls.GlbUserComCode.Trim() == "ABE" && ddlCloseType.SelectedValue.ToString().Trim() == "WRPL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result1 = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PreferLoc);
                    _result2 = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result2 = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                }
                DataTable _result = new DataTable();
                _result.Merge(_result1);
                _result.Merge(_result2); 
                //_result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPreferLoc;
                _CommonSearch.ShowDialog();
                txtPreferLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_DispatchRequired_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtPreferLoc.Focus();
            }
        }
        public static DateTime GetLastDayOfPreviousMonth(DateTime startDate)
        {

            DateTime lastDayLastMonth = new DateTime(startDate.Year, startDate.Month, 1);
            lastDayLastMonth = lastDayLastMonth.AddDays(-1);

            startDate = lastDayLastMonth;

            return startDate;
        }
        private void txtPreferLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchPreferLoc_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtRefNo.Focus();
            }
        }

        private void txtCloseRmk_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendSMStoTech(String job, Int32 jobLine)
        {
            string outMsg;
            Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, 6);
            Service_Message oMessage = new Service_Message();
            if (oTemplate != null && oTemplate.Sml_templ_mail != null)
            {
                string emailBody = oTemplate.Sml_templ_mail;
                String SmsBody = oTemplate.Sml_templ_sms;
                oMessage.Sm_com = BaseCls.GlbUserComCode;
                oMessage.Sm_jobno = job;
                oMessage.Sm_joboline = jobLine;
                oMessage.Sm_jobstage = 6;
                oMessage.Sm_ref_num = string.Empty;
                oMessage.Sm_status = 0;
                oMessage.Sm_msg_tmlt_id = 6;
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
                Int32 R1 = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                if (R1 < 1)
                {
                    MessageBox.Show("Customer message send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, 7);
            oMessage = new Service_Message();
            if (oTemplate != null && oTemplate.Sml_templ_sms != null)
            {
                string emailBody = oTemplate.Sml_templ_mail;
                String SmsBody = oTemplate.Sml_templ_sms;
                oMessage.Sm_com = BaseCls.GlbUserComCode;
                oMessage.Sm_jobno = job;
                oMessage.Sm_joboline = jobLine;
                oMessage.Sm_jobstage = 6;
                oMessage.Sm_ref_num = string.Empty;
                oMessage.Sm_status = 0;
                oMessage.Sm_msg_tmlt_id = 7;
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
                Int32 R1 = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                if (R1 < 1)
                {
                    MessageBox.Show("Customer message send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUnitAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDisRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucPayModes1_Load(object sender, EventArgs e)
        {

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
                        if (_allowInvType.srtp_main_tp == "CREDIT")
                        {
                            //get customer allow inv types
                            DataTable _cutomerAllowInvTypes = new DataTable();
                            _cutomerAllowInvTypes = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, _customer);
                            if (_cutomerAllowInvTypes.Rows.Count > 0)
                            {
                                var _selectedTypes = _cutomerAllowInvTypes.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == _allowInvType.srtp_main_tp).ToList();
                                if (_selectedTypes != null && _selectedTypes.Count > 0) { return true; }
                                else { MessageBox.Show("Customer not allow to enter for selected invoice type.", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                            }
                            else { MessageBox.Show("Customer allowed invoice types not found!", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                        }
                        else { return true; }
                    }
                    else { MessageBox.Show("Invoice types not found", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                }
            }
            catch (Exception ex)
            {
                _isAllow =  false;
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

        private void btnServiceadd_Click(object sender, EventArgs e)
        {
            //Service_free_det objlist = new Service_free_det();
            if (Convert.ToDateTime(dtDate.Text).Date > Convert.ToDateTime(dtpFirstservceday.Text).Date)
            {
                   MessageBox.Show("First service can't back date", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnServiceClose_Click(object sender, EventArgs e)
        {
            pnlService.Visible = false;
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
            dtpFirstservceday.Text = Convert.ToString( DateTime.Now.Date);
            txtscvloac.Text = string.Empty;
            txtscvprocnter.Text = string.Empty;
            lstService = new List<Service_free_det>();
            if (dgvService.DataSource != null)
            {
                dgvService.DataSource = null;
            }
        }

        private void btnCreateservice_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo.Text))
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

      

        private void btnCustcomm_Click_1(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }
        private bool IsValidCustomer(string _customerCode, string _invType, decimal _invAmount = 0)
        {
            bool _returnStatus = true;

            try
            {
                if (string.IsNullOrEmpty(_invType))
                {
                    MessageBox.Show("Invalid invoice type", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbInvType.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(_customerCode))
                {
                    MessageBox.Show("Please select a customer!", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtCustomer.Focus();
                    return false;
                }
                else
                {
                    //Get allowed types for selected inv type--------------------------
                    sar_tp _allowInvType = new sar_tp();
                    _allowInvType.srtp_cd = _invType;
                    _allowInvType = CHNLSVC.General.GetMasterPrefixData(_allowInvType);

                    if (_allowInvType != null)
                    {
                        if (_allowInvType.srtp_main_tp == "CREDIT")
                        {
                            //get customer allow inv types---------------------------
                            DataTable _cutomerAllowInvTypes = new DataTable();
                            _cutomerAllowInvTypes = CHNLSVC.Sales.GetCustomerAllowInvoiceTypeNew(BaseCls.GlbUserComCode, _customerCode);
                            if (_cutomerAllowInvTypes.Rows.Count > 0)
                            {
                                List<MasterInvoiceType> SalesTypes = new List<MasterInvoiceType>();
                                foreach (DataRow _row in _cutomerAllowInvTypes.Rows)
                                {
                                    MasterInvoiceType _type = new MasterInvoiceType();
                                    _type.Srtp_cd = _row["Srtp_cd"].ToString();
                                    _type.Srtp_desc = _row["SRTP_DESC"].ToString();
                                    _type.Srtp_valid_from_dt = _row["MBSA_VALID_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_row["MBSA_VALID_FRM_DT"]).Date;
                                    _type.Srtp_valid_to_dt = _row["MBSA_VALID_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_row["MBSA_VALID_TO_DT"]).Date;
                                    SalesTypes.Add(_type);
                                }

                                if (SalesTypes != null && SalesTypes.Count > 0)
                                {
                                    //check selected invtype is allow----------------------------------
                                    var _selectedTypes = SalesTypes.Where(x => x.Srtp_cd == _invType).ToList();
                                    if (_selectedTypes != null && _selectedTypes.Count > 0)
                                    {
                                        //check invoice type valid period----------------------------------------
                                        var _tmpResult = SalesTypes.Where(x => x.Srtp_cd == _invType && x.Srtp_valid_from_dt <= dtDate.Value.Date && x.Srtp_valid_to_dt >= dtDate.Value.Date).ToList();
                                        if (_tmpResult == null || _tmpResult.Count < 1)
                                        {
                                            MessageBox.Show("Customer is not in valid date range for enter transaction under selected invoice type.!", "Invoice Types", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                         //   ClearCustomer(true);
                                            cmbInvType.Focus();
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                    else { MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type.", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //ClearCustomer(true); 
                                        //txtCustomer.Focus(); 
                                        return false; }
                                }
                            }
                            else { MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type!", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                               // ClearCustomer(true);
                                cmbInvType.Focus(); return false; }
                        }
                        else { return true; }
                    }
                    else { MessageBox.Show("Invoice types not found", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //ClearCustomer(true); 
                        cmbInvType.Focus(); return false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating customer details. " + Environment.NewLine + ex.Message, "Invoice Types", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            
                _returnStatus = false;
            }

            return _returnStatus;
        }
        private bool IsCustCreditLimitIsValid()
        {
            bool _isValid = false;
            try
            {
                string _invType = cmbInvType.Items.Count > 0 ? cmbInvType.SelectedValue.ToString() : string.Empty;

                //Get allowed types for selected inv type--------------------------
                sar_tp _allowInvType = new sar_tp();
                _allowInvType.srtp_cd = _invType;
                _allowInvType = CHNLSVC.General.GetMasterPrefixData(_allowInvType);

                decimal _invoiceAmount = 0;
                decimal.TryParse(txtTotRev.Text, out _invoiceAmount);

                if (_allowInvType != null)
                {
                    if (_allowInvType.srtp_main_tp == "CREDIT")
                    {
                        if (_invoiceAmount > 0)
                        {
                            //check customer allow credit limit

                            if (!string.IsNullOrEmpty(lblCusCode.Text.Trim()))
                            {
                                CustomerAccountRef _custAccount = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, lblCusCode.Text.Trim());
                                if (_custAccount != null && _custAccount.Saca_com_cd != null)
                                {
                                    DataTable _custInvBalance = new DataTable();
                                    _custInvBalance = CHNLSVC.Sales.GetCustomerInvoiceBalance(BaseCls.GlbUserComCode, lblCusCode.Text.Trim(), _invType);
                                    if (_custInvBalance.Rows.Count > 0)
                                    {
                                        decimal _availableBalance = 0;
                                        decimal.TryParse(_custInvBalance.Rows[0]["BalanceAmt"].ToString(), out _availableBalance);
                                        decimal _tmptotalBalance = _availableBalance + _invoiceAmount;
                                        decimal _creditLimit = _custAccount.Saca_crdt_lmt;
                                        if (_tmptotalBalance > _creditLimit)
                                        {
                                            MessageBox.Show("Customer credit limit has exceeded", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Customer due invoice balance not found", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Customer credit limit information not found", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select a customer", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid invoice amount", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Allowed invoice type not found", "Sales Invoice - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating customer credit limit. " + Environment.NewLine + ex.Message, "Invoice Types", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _isValid = false;
            }
            return _isValid;
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
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtscvloac.Select();
                ValidateServiceLocation(txtscvloac.Text);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }

        private void txtscvloac_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtscvloac.Text))
            {
                ValidateServiceLocation(txtscvloac.Text);
            }
            
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

        private void txtNoofservice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
