using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Sales;
using FF.BusinessObjects.CustService;

namespace FF.WindowsERPClient.Services
{
    public partial class JobEntry : Base
    {
        #region Global variables

        private Service_Chanal_parameter _scvParam = null;

        //_scvParam.SP_DB_SERIAL;
        private Service_JOB_HDR _scvjobHdr = null;

        private List<Service_job_Det> _scvItemList = null;
        private List<Service_Job_Defects> _scvDefList = null;
        private List<Service_Tech_Aloc_Hdr> _scvEmpList = null;
        private List<Service_Job_Det_Sub> _scvItemSubList = null;
        private List<Service_Job_Det_Sub> _tempItemSubList = null;
        private List<Service_TempIssue> _scvStdbyList = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private List<Service_Req_Det> _reqDet = null;
        private MasterLocation _scvLoc = null;
        private Service_Req_Hdr _reqHdr = null;
        private InventorySerialMaster _warrItemTemp = null;
        private int _jobRecall = 0;
        private int _jobRecallSeq = 0;
        private bool _isBlack = false;
        private string _isBlackInfor = string.Empty;
        private List<MST_BUSPRIT_LVL> _PriorityLvlList = new List<MST_BUSPRIT_LVL>();
        private Int32 _warStus = 0;
        private string _itmBrand = "";
        private Int32 _jobItemLine = 1;
        private string _jobStage = "2_12";

        private string _warrSearchtp = string.Empty;
        private string _warrSearchorder = string.Empty;
        private Boolean _isGroup = false;
        private string _chgJobStage = "2";
        private Int32 _gridItemLine = 1;

        private String oldJobNumber = string.Empty;

        Boolean _isFree = false; // Nadeeka 28-07-2015

        Service_job_Det recalledJob = null; // Tharaka 2015-08-19
        InventorySerialMaster recalledJobSerial = null;// Tharaka 2015-08-19
        private Int32 _fgap = 0;
        private string _taskDir = "";
        private string _srb_jobstp = "";    //kapila 2/2/2016
        private static List<ImageUploadDTO> oImgList = new List<ImageUploadDTO>();
        private Boolean _isManExtItmDt = false;    //kapila 19/2/2016

        //InventorySerialMaster VehicleObect;
        //List<ServiceJobDefect> Defect_List;
        //int Defect_LineNo;
        //int IsService;
        //int IsUpdateShedule;
        //int ServiceTerm;

        //Akila 2017/05/05 Add new varibale to identify registered item
        public bool IsRegistered = false;
        public void setUploadImgList(List<ImageUploadDTO> _list)
        {
            oImgList = _list;
        }

        //Add by akila 2017/05/06
        List<ServiceTechAlocSupervice> AlocatedSupervicerList = new List<ServiceTechAlocSupervice>();

        private void LoadPayMode()
        {
            if (_scvParam != null)
            {
                ucPayModes1.InvoiceType = _scvParam.SP_DEFINVC_TP;
            }
            else
            {
                ucPayModes1.InvoiceType = "CS";
            }
            ucPayModes1.Customer_Code = txtCustCode.Text;
            ucPayModes1.Mobile = txtMobile.Text.Trim();
            ucPayModes1.LoadPayModes();
        }

        #endregion Global variables

        #region Common Message

        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        #endregion Common Message

        #region Common search area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.JobRegNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + _jobStage + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemSupplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtEItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceRequests:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InspecReq:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + _jobStage + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append(_warrSearchtp + seperator + _warrSearchorder + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _scvParam.SP_SERCHNL + seperator + lblItemCat.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceTaskCate:
                    {
                        paramsText.Append("ACTIVE" + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServicePriority:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceStageChages:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + _scvParam.SP_SERCHNL + seperator + txtTaskLoc.Text + seperator + "1" + seperator + dtDate.Value.Date.ToShortDateString() + seperator + _chgJobStage + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ADVISOR:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "AV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + seperator + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServicePrioritybycust:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbDefSubChannel + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common search area

        #region Rooting for Load Warranty Details

        private void Load_Warranty_Details()
        {
        }

        #endregion Rooting for Load Warranty Details

        public JobEntry()
        {
            //try
            //{
            Clear();
            _scvjobHdr = new Service_JOB_HDR();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();
            cmbWarStus.SelectedIndex = 1;
            //}
            //catch (Exception ex)
            //{ SystemErrorMessage(ex); }
        }

        private void Clear()
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0) Controls[0].Dispose();
            InitializeComponent();

            _chgJobStage = "2";
            _jobItemLine = 1;
            _jobRecall = 0;
            pnlOthWarr.Visible = false;
            pnlSer.Enabled = true;
            pnlItem.Visible = false;
            pnlMultiItems.Visible = false;
            pnlReq.Visible = false;
            dtDate.Value = DateTime.Now.Date;
            dtpFromAL.Value = DateTime.Now.Date;
            dtpToAL.Value = DateTime.Now.AddHours(1);
            dtpFromAL.Value = DateTime.Now;

            pnlHistory.Hide();
            pnlPaymode.Hide();
            //InitializeForm();
            //BackDatePermission(); btnSave.Enabled = true;

            //pnlHead.Location = new Point(559, 299);
            //pnlHead.Size = new Size(447, 292);
            pnlItem.Location = new Point(560, 32);
            pnlMultiItems.Size = new Size(844, 95);
            pnlItem.Size = new Size(334, 320);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            pnlHistory.Size = new Size(775, 504);
            pnlPaymode.Size = new Size(990, 222);

            pnlSearilChange.Size = new System.Drawing.Size(259, 124);

            ChkExternal.Enabled = false;
            //pnlInspection.Enabled = false;
            LoadPayMode();

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = false;

            optReq.Enabled = true;
            optInspection.Enabled = true;
            optJob.Enabled = true;
            btnPrint.Visible = false;
            btnViewPay.Enabled = false;

            _scvjobHdr = new Service_JOB_HDR();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();
            AlocatedSupervicerList = new List<ServiceTechAlocSupervice>();

            grvJobItms.DataSource = null;
            grvDef.DataSource = null;
            grvTech.DataSource = null;

            txtReqLineNo.Text = "0";
            if (_scvParam != null)
            {
                lblSerSrch.Text = _scvParam.SP_DB_SERIAL;
                lblRegNo.Text = _scvParam.SP_DB_CHASSIS;
                txtCustCode.Text = _scvParam.SP_EXTERNALCUST;
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    pnlMilage.Visible = true;
                    pnlItmTp.Visible = false;
                }
                else
                {
                    pnlMilage.Visible = false;
                    pnlItmTp.Visible = true;
                }
                txtTaskLoc.Text = _scvParam.SP_DEF_CAT;

                if (_scvParam.SP_IS_DIRECT_PRINT == 1) { chk_Direct_print.Checked = true; }
                else { chk_Direct_print.Checked = false; }

            }

            pnlCustBill.Enabled = true;
            btnNewCust.Enabled = true;
            chkEditCustomer.Enabled = true;
            this.Cursor = Cursors.Default;

            pnlWarr.Height = 128;
            pnlDOInvoiceItems.Size = new Size(508, 236);
            pnlDOInvoiceItems.Visible = false;

            oldJobNumber = string.Empty;
            pnlSubItems.Size = new Size(642, 217);
            txtESerial2.Text = null; //By akila 2017/06/24 
        }

        private void ClearScreen()
        {
            this.Cursor = Cursors.WaitCursor;

            _jobItemLine = 1;
            pnlSer.Enabled = true;
            pnlItem.Visible = false;
            pnlMultiItems.Visible = false;
            pnlReq.Visible = false;
            dtDate.Value = DateTime.Now.Date;
            dtpFromAL.Value = DateTime.Now.Date;
            dtpToAL.Value = DateTime.Now.AddHours(1);
            pnlHistory.Hide();
            pnlPaymode.Hide();
            //InitializeForm();
            //BackDatePermission(); btnSave.Enabled = true;

            //pnlHead.Location = new Point(559, 299);
            //pnlHead.Size = new Size(447, 292);
            pnlItem.Location = new Point(560, 32);
            pnlMultiItems.Size = new Size(844, 95);
            pnlItem.Size = new Size(334, 320);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            pnlHistory.Size = new Size(775, 504);

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = false;

            _scvjobHdr = new Service_JOB_HDR();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();

            grvJobItms.DataSource = null;
            grvDef.DataSource = null;
            grvTech.DataSource = null;

            txtReqLineNo.Text = "0";
            this.Cursor = Cursors.Default;
        }

        private void ClearCateChange()
        {
            this.Cursor = Cursors.WaitCursor;
            _jobRecall = 0;
            pnlSer.Enabled = true;
            pnlItem.Visible = false;
            pnlMultiItems.Visible = false;
            pnlReq.Visible = false;
            dtDate.Value = DateTime.Now.Date;
            dtpFromAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);
            dtpToAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);
            //pnlHead.Location = new Point(559, 299);
            //pnlHead.Size = new Size(447, 292);
            pnlItem.Location = new Point(560, 32);
            pnlMultiItems.Size = new Size(844, 95);
            pnlItem.Size = new Size(334, 320);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            ChkExternal.Enabled = false;
            //pnlInspection.Enabled = false;

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = false;

            _scvjobHdr = new Service_JOB_HDR();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();

            grvJobItms.DataSource = null;
            grvDef.DataSource = null;
            grvTech.DataSource = null;

            txtReqLineNo.Text = "0";
            this.Cursor = Cursors.Default;
        }

        private void btn_srch_itm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEItem;
            _CommonSearch.ShowDialog();
            txtEItem.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_jobRecall == 1)
            {
                SystemInformationMessage("Can't add additional items for saved job!", "Not Allowed");
                return;
            }

            if (_scvParam.SP_MULTIITM == 0)
            {
                if (_scvItemList != null)
                {
                    Int32 _count = _scvItemList.Count();
                    if (_count > 0)
                    {
                        if (_jobRecall == 2 && _count == 1)
                        {
                        }
                        else
                        {
                            SystemInformationMessage("You have not allowed to add multiple items for the Job!", "Multiple Item Not Allowed");
                            return;
                        }
                    }
                }
            }

            //Add by akila 2017/06/22
            if ((pnlMilage.Visible == true) && (_scvParam.SP_ISAUTOMOBI == 1))
            {
                if (string.IsNullOrEmpty(txtMilage.Text))
                {
                    SystemInformationMessage("Please enter the current mileage reading!", "Mileage");
                    txtMilage.Focus();
                    return;
                }
            }

            //kapila 19/2/2016
            if (_isManExtItmDt == true && txtEBrand.ReadOnly == false)
                if (string.IsNullOrEmpty(txtEModel.Text) || string.IsNullOrEmpty(txtEBrand.Text) || string.IsNullOrEmpty(txtPartNo.Text))
                {
                    MessageBox.Show("Model/Brand/Part Number cannot be blank !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            if (string.IsNullOrEmpty(txtSupplier.Text) || string.IsNullOrEmpty(txtEWarPrd.Text) || string.IsNullOrEmpty(txtESerial.Text) || string.IsNullOrEmpty(txtEWarr.Text) || string.IsNullOrEmpty(txtEItem.Text) || string.IsNullOrEmpty(txtEItemDesc.Text))
            {
                MessageBox.Show("Item/Description/Serial/Warranty/Supplier cannot be blank !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cmbWarStus.SelectedIndex == -1)
            {
                MessageBox.Show("Select the warranty status !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            lblItem.Text = txtEItem.Text.ToString();
            lblItemDesc.Text = txtEItemDesc.Text.ToString();
            lblModel.Text = txtEModel.Text.ToString();
            lblBrand.Text = txtEBrand.Text.ToString();
            lblWarPrd.Text = txtEWarPrd.Text.ToString();
            lblWarRem.Text = txtEWarRem.Text.ToString();
            lblWarStart.Text = dtEWarStart.Value.Date.ToString();
            lblWarEnd.Text = dtEWarEnd.Value.Date.ToString();
            lblWarStus.Text = cmbWarStus.Text.ToString();
            lblSerNo.Text = txtESerial.Text.ToString();
            lblWarNo.Text = txtEWarr.Text.ToString();
            lblWarStus.Text = cmbWarStus.Text.ToString();
            lblWarStart.Text = dtEWarStart.Value.Date.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = dtEWarEnd.Value.Date.ToString("dd-MMM-yyyy");
            lblWarPrd.Text = txtEWarPrd.Text.ToString();
            lblWarRemain.Text = "0";
            lblWarRem.Text = txtEWarRem.Text.ToString();
            lblSuppCode.Text = txtSupplier.Text;    //kapila 27/3/2015

            lblInv.Text = txtEInv.Text.ToString();

            lblInvDt.Text = dtEInvDt.Value.Date.ToString("dd-MMM-yyyy");
            lblAccNo.Text = txtEAccNo.Text.ToString();
            lblDelLoc.Text = "N/A";

            //this is used in <Save_Job>
            _itmBrand = lblBrand.Text.ToString();
            _warStus = (lblWarStus.Text == "UNDER WARRANTY") ? 1 : 0;

            //pnlItem.Visible = false;
            //ChkExternal.Checked = false;

            add_job_item(1, null);
            load_item_det(0);

            clear_Ext_Job_Items();

            if (grvJobItms.Rows.Count > 0)
            {
                grvJobItms_CellClick(null, new DataGridViewCellEventArgs(0, 0));
            }

            txtEItem.Focus();
            //txtDef.Focus();
            //pnlItem.Visible = false;
        }

        private void clear_Ext_Job_Items()
        {
            txtEItem.Text = "";
            txtEItemDesc.Text = "";
            txtEModel.Text = "";
            txtEBrand.Text = "";
            txtEWarPrd.Text = "0";
            txtEWarRem.Text = "";
            cmbWarStus.Text = "";
            txtESerial.Text = "";
            txtEWarr.Text = "";
            txtEInv.Text = "";
            txtEAccNo.Text = "";
            dtEInvDt.Value = DateTime.Now.Date;
            dtEWarStart.Value = DateTime.Now.Date;
            dtEWarEnd.Value = DateTime.Now.Date;

            lblItem.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblWarPrd.Text = "";
            lblWarRem.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblWarStus.Text = "";
            lblSerNo.Text = "";
            txtPartNo.Text = "";    //kapila 9/7/2015
            lblItmStus.Text = "";   //kapila 30/6/2015
            lblWarNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblWarPrd.Text = "";
            lblInvTp.Text = "";     //kapila 9/7/2015
            lblWarRemain.Text = "";
            lblWarRem.Text = "";

            lblInv.Text = "";
            lblInvDt.Text = "";
            lblAccNo.Text = "";
            lblDelLoc.Text = "";

            lblBuyerCustCode.Text = "";
            lblBuyerCustName.Text = "";
            lblBuyerCustAdd1.Text = "";
            lblBuyerCustAdd2.Text = "";
            lblBuyerCustMobi.Text = "";

            //this is used in <Save_Job>
            //_itmBrand = "";
            //_warStus = 0;

            lblPartNo.Text = "";

            lblUnitPrice.Text = "";

            txtESerial2.Text = null; //By akila 2017/06/24 
        }

        private void Set_Job_Item()
        {
            Service_job_Det _jobDt = new Service_job_Det();
            _jobDt.Jbd_seq_no = 0;
            _jobDt.Jbd_jobno = "";
            _jobDt.Jbd_jobline = 1;
            _jobDt.Jbd_sjobno = "";
            _jobDt.Jbd_loc = BaseCls.GlbUserDefLoca;
            _jobDt.Jbd_pc = BaseCls.GlbUserDefProf;
            _jobDt.Jbd_itm_cd = _scvParam.SP_INSPECTION_ITM;
            MasterItem _item = CHNLSVC.Inventory.GetItem("", _jobDt.Jbd_itm_cd);
            _jobDt.Jbd_itm_stus = "SER";
            _jobDt.Jbd_itm_desc = _item.Mi_shortdesc;
            _jobDt.Jbd_brand = _item.Mi_brand;
            _jobDt.Jbd_model = _item.Mi_model;
            _jobDt.Jbd_itm_cost = 0;
            _jobDt.Jbd_ser1 = "N/A";
            _jobDt.Jbd_ser2 = txtPartNo.Text; // "N/A";
            _jobDt.Jbd_warr = "N/A";
            _jobDt.Jbd_regno = "N/A";
            _jobDt.Jbd_milage = 0;
            _jobDt.Jbd_warr_stus = 0;
            _jobDt.Jbd_onloan = 0;
            _jobDt.Jbd_availabilty = 1;
            _jobDt.Jbd_msnno = "";
            _jobDt.Jbd_itmtp = _item.Mi_itm_tp;
            _jobDt.Jbd_cate1 = _item.Mi_cate_1;
            _jobDt.Jbd_custnotes = "";
            _jobDt.Jbd_isstockupdate = 0;
            _jobDt.Jbd_isgatepass = 0;
            _jobDt.Jbd_iswrn = 0;
            _jobDt.Jbd_warrperiod = 0;
            _jobDt.Jbd_warrrmk = "N/A";
            _jobDt.Jbd_warrreplace = 0;
            _jobDt.Jbd_date_pur = dtDate.Value.Date;
            _jobDt.Jbd_invc_no = "";
            _jobDt.Jbd_invc_showroom = _scvLoc.Ml_loc_desc;
            _jobDt.Jbd_isexternalitm = 0;
            _jobDt.Jbd_conf_desc = "";
            _jobDt.Jbd_isagreement = "0";
            _jobDt.Jbd_stage = Convert.ToDecimal(1.1);
            _jobDt.Jbd_com = BaseCls.GlbUserComCode;
            _jobDt.Jbd_ser_id = "0";
            _jobDt.Jbd_supp_cd = "";
            _jobDt.Jbd_act = 0;

            _scvItemList.Add(_jobDt);

            _jobItemLine = _jobItemLine + 1;

            txtSer.Clear();
            txtWar.Clear();
            txtRegNo.Clear();

            lblItem.Text = _jobDt.Jbd_itm_cd;
            lblItemDesc.Text = _jobDt.Jbd_itm_desc;
            lblSerNo.Text = _jobDt.Jbd_ser1;
            txtPartNo.Text = _jobDt.Jbd_ser2;   //kapila 9/7/2015
            lblWarNo.Text = _jobDt.Jbd_warr;
            lblModel.Text = _jobDt.Jbd_model;
            lblBrand.Text = _jobDt.Jbd_brand;
            lblItemCat.Text = _jobDt.Jbd_cate1;

            lblItmStus.Text = "GOOD";
            if (!string.IsNullOrEmpty(_jobDt.Jbd_itm_stus))
            {
                DataTable _dtStatus = CHNLSVC.Inventory.GetItemStatusMaster(_jobDt.Jbd_itm_stus, null);  //kapila 30/6/2015
                if (_dtStatus.Rows.Count > 0)
                    lblItmStus.Text = _dtStatus.Rows[0]["mis_desc"].ToString();
            }

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_job_Det>();
            grvJobItms.DataSource = _scvItemList;
            grvJobItms.Refresh();
        }

        private void add_job_item(int _active, Service_Req_Det _reqItm)
        {

            if (string.IsNullOrEmpty(lblUnitPrice.Text))
            {
                lblUnitPrice.Text = "0";
            }
            Service_job_Det _jobDt = new Service_job_Det();
            _jobDt.Jbd_seq_no = 0;
            _jobDt.Jbd_jobno = "";
            _jobDt.Jbd_jobline = _jobItemLine;
            _jobDt.Jbd_sjobno = "";
            _jobDt.Jbd_loc = BaseCls.GlbUserDefLoca;
            _jobDt.Jbd_pc = BaseCls.GlbUserDefProf;
            _jobDt.Jbd_itm_cd = lblItem.Text;
            _jobDt.Jbd_itm_stus = "SER";
            _jobDt.Jbd_itm_desc = lblItemDesc.Text;
            _jobDt.Jbd_brand = lblBrand.Text;
            _jobDt.Jbd_model = lblModel.Text;
            _jobDt.Jbd_itm_cost = Convert.ToDecimal(lblUnitPrice.Text);
            _jobDt.Jbd_ser1 = lblSerNo.Text;
            _jobDt.Jbd_ser2 = ChkExternal.Checked ? txtESerial2.Text : (string.IsNullOrEmpty(txtPartNo.Text) ? txtChassiNo.Text : txtPartNo.Text); //updated by akila 2017/06/24
            _jobDt.Jbd_warr = lblWarNo.Text;

            _jobDt.Jbd_regno = ChkExternal.Checked ? txtERegNo.Text : txtRegNo.Text; //updated by akila 2017/06/22
            // _jobDt.Jbd_regno = txtRegNo.Text;

            if (txtMilage.Text.Length > 0)
            {
                _jobDt.Jbd_milage = Convert.ToDecimal(txtMilage.Text.ToString());
            }

            _jobDt.Jbd_warr_stus = (lblWarStus.Text == "UNDER WARRANTY") ? 1 : 0;
            _jobDt.Jbd_onloan = 0;
            //_jobDt.Jbd_chg_warr_stdt = "";
            //_jobDt.Jbd_chg_warr_rmk = "";
            //_jobDt.Jbd_isinsurance = "";
            //_jobDt.Jbd_req_tp = "";
            //_jobDt.Jbd_ser_term = "";
            //_jobDt.Jbd_lastwarr_stdt = "";
            //_jobDt.Jbd_issued = "";
            //_jobDt.Jbd_mainitmcd = "";
            //_jobDt.Jbd_mainitmser = "";
            //_jobDt.Jbd_mainitmwarr = "";
            //_jobDt.Jbd_itmmfc = "";
            //_jobDt.Jbd_mainitmmfc = "";
            _jobDt.Jbd_availabilty = 1;
            //_jobDt.Jbd_usejob = "";
            _jobDt.Jbd_msnno = txtCoupon.Text; //MSN No or Coupon No
            _jobDt.Jbd_itmtp = txtItmTp.Text;
            _jobDt.Jbd_reqline = Convert.ToInt32(txtReqLineNo.Text.ToString());
            _jobDt.Jbd_cate1 = lblItemCat.Text;
            //_jobDt.Jbd_serlocchr = "";
            _jobDt.Jbd_custnotes = "";
            //_jobDt.Jbd_mainreqno = "";
            //_jobDt.Jbd_mainreqloc = "";
            //_jobDt.Jbd_mainjobno = "";
            //_jobDt.Jbd_reqitmtp = "";
            _jobDt.Jbd_reqno = txtReqNo.Text;
            if (_reqItm != null) _jobDt.Jbd_reqline = _reqItm.Jrd_reqline;
            _jobDt.Jbd_isstockupdate = 0;
            if (_reqItm != null) _jobDt.Jbd_isstockupdate = _reqItm.Jrd_isstockupdate;
            _jobDt.Jbd_isgatepass = 0;
            _jobDt.Jbd_iswrn = 0;
            if (lblWarPrd.Text == "") { _jobDt.Jbd_warrperiod = 0; } else { _jobDt.Jbd_warrperiod = Convert.ToInt32(lblWarPrd.Text); }
            _jobDt.Jbd_warrrmk = lblWarRem.Text;
            if (lblWarStart.Text == "") { _jobDt.Jbd_warrstartdt = Convert.ToDateTime("01/JAN/1900"); } else { _jobDt.Jbd_warrstartdt = Convert.ToDateTime(lblWarStart.Text); }
            _jobDt.Jbd_warrreplace = 0;
            if (lblInvDt.Text == "") { _jobDt.Jbd_date_pur = Convert.ToDateTime("01/JAN/1900"); } else { _jobDt.Jbd_date_pur = Convert.ToDateTime(lblInvDt.Text).Date; }
            _jobDt.Jbd_invc_no = lblInv.Text;
            //_jobDt.Jbd_waraamd_seq = "";
            //_jobDt.Jbd_waraamd_by = "";
            //_jobDt.Jbd_waraamd_dt = "";
            _jobDt.Jbd_invc_showroom = lblDelLoc.Text;
            if (_reqItm != null) _jobDt.Jbd_aodissueloc = _reqItm.Jrd_aodissueloc;
            if (_reqItm != null) _jobDt.Jbd_aodissuedt = _reqItm.Jrd_aodissuedt;
            if (_reqItm != null) _jobDt.Jbd_aodissueno = _reqItm.Jrd_aodissueno;
            //_jobDt.Jbd_aodrecno = "";
            //_jobDt.Jbd_techst_dt = "";
            //_jobDt.Jbd_techfin_dt = "";
            //_jobDt.Jbd_msn_no = "";
            _jobDt.Jbd_isexternalitm = (ChkExternal.Checked == true) ? 1 : 0;
            //_jobDt.Jbd_conf_dt = "";
            //_jobDt.Jbd_conf_cd = "";
            _jobDt.Jbd_conf_desc = "";
            //_jobDt.Jbd_conf_rmk = "";
            //_jobDt.Jbd_tranf_by = "";
            //_jobDt.Jbd_tranf_dt = "";
            //_jobDt.Jbd_do_invoice = 0;
            //_jobDt.Jbd_insu_com = "";
            //_jobDt.Jbd_agreeno = "";
            //_jobDt.Jbd_issrn = 0;
            _jobDt.Jbd_isagreement = "0";
            //_jobDt.Jbd_cust_agreeno = "";
            //_jobDt.Jbd_quo_no = "";
            _jobDt.Jbd_stage = 2;
            _jobDt.Jbd_com = BaseCls.GlbUserComCode;
            _jobDt.Jbd_ser_id = "0";
            if (_reqItm != null) if (!string.IsNullOrEmpty(_reqItm.Jrd_ser_id)) if (_reqItm.Jrd_ser_id != "0") _jobDt.Jbd_ser_id = _reqItm.Jrd_ser_id;
            //_jobDt.Jbd_techst_dt_man = "";
            //_jobDt.Jbd_techfin_dt_man = "";
            //_jobDt.Jbd_reqwcn = "";
            //_jobDt.Jbd_reqwcndt = "";
            //_jobDt.Jbd_reqwcnsysdt = "";
            //_jobDt.Jbd_sentwcn = "";
            //_jobDt.Jbd_recwcn = "";
            //_jobDt.Jbd_takewcn = "";
            //_jobDt.Jbd_takewcndt = "";
            //_jobDt.Jbd_takewcnsysdt = "";
            _jobDt.Jbd_supp_cd = lblSuppCode.Text;
            //_jobDt.Jbd_part_cd = "";
            //_jobDt.Jbd_oem_no = "";
            //_jobDt.Jbd_case_id = "";
            _jobDt.Jbd_act = _active;
            //_jobDt.Jbd_oldjobline = "";
            //_jobDt.Jbd_tech_rmk = "";
            //_jobDt.Jbd_tech_custrmk = "";
            //_jobDt.StageText = "";
            _jobDt.Jbd_is_fgap = _fgap;

            DataTable _discRate = CHNLSVC.CustService.get_isSmartWarr_Job(_jobDt.Jbd_rep_perc, _jobDt.Jbd_invc_no, _jobDt.Jbd_ser1, _jobDt.Jbd_itm_cd);

            if (_discRate.Rows.Count > 0)
            {
                foreach (DataRow drow in _discRate.Rows)
                {
                    _jobDt.jbd_pb = drow["jbd_pb"].ToString();
                    _jobDt.jbd_pblvl = drow["jbd_pb_lvl"].ToString();
                    _jobDt.jbd_sw_stus = Convert.ToInt16(drow["jbd_sw_stus"].ToString());
                }
            }

            if (_warrItemTemp != null)
            {
                if (_warrItemTemp.Irsm_sup_warr_stdt.AddMonths(_warrItemTemp.Irsm_sup_warr_pd).Date >= dtDate.Value.Date)
                { _jobDt.Jbd_swarr_stus = 1; }
                else
                { _jobDt.Jbd_swarr_stus = 0; }

                _jobDt.Jbd_swarrperiod = _warrItemTemp.Irsm_sup_warr_pd;
                _jobDt.Jbd_swarrrmk = _warrItemTemp.Irsm_sup_warr_rem;
                _jobDt.Jbd_swarrstartdt = _warrItemTemp.Irsm_sup_warr_stdt;
                _jobDt.jbd_del_sale_dt = _warrItemTemp.Irsm_warr_st_dt_web;
            }

            _warrItemTemp = null;

            _scvItemList.Add(_jobDt);

            _jobItemLine = _jobItemLine + 1;

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_job_Det>();
            grvJobItms.DataSource = _scvItemList;
            grvJobItms.Refresh();


            if (_reqItm != null)
            {
                List<Service_Request_Defects> _reqDefList = CHNLSVC.CustService.getRequestDefects(txtReqNo.Text, _reqItm.Jrd_reqline);
                if (_reqDefList != null && _reqDefList.Count > 0 && _scvItemList.Count == 1)
                {
                    foreach (Service_Request_Defects _reqDefOne in _reqDefList)
                    {
                        txtDef.Text = _reqDefOne.Srdf_def_tp;
                        txtDefRem.Text = _reqDefOne.Srdf_def_rmk;
                        DataTable _dt = CHNLSVC.CustService.getDefectTypes(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, lblItemCat.Text, txtDef.Text);
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            lblDefDesc.Text = _dt.Rows[0]["SD_DESC"].ToString();
                            btn_add_def_Click(null, null);
                        }
                    }
                }
            }

            txtSer.Clear();
            txtWar.Clear();
            txtRegNo.Clear();
        }

        private void grvJobItms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _gridItemLine = Convert.ToInt32(grvJobItms.Rows[e.RowIndex].Cells["jbd_jobline"].Value);  //kapila

            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 _line = Convert.ToInt32(grvJobItms.Rows[e.RowIndex].Cells["jbd_jobline"].Value);

                    if (optInspection.Checked == true)
                    {
                        if (_scvParam.SP_INSPECTION_ITM == grvJobItms.Rows[e.RowIndex].Cells["JBD_ITM_CD"].Value.ToString())
                        {
                            SystemInformationMessage("Can't remove inspection item", "Inspection Job");
                            return;
                        }
                    }

                    //remove defects related to this item line
                    List<Service_Job_Defects> _temp = new List<Service_Job_Defects>();
                    _temp = _scvDefList;
                    _temp.RemoveAll(x => x.SRD_JOB_LINE == _line);
                    _scvDefList = _temp;

                    //remove Sub Items related to this item line
                    List<Service_Job_Det_Sub> _tempSub = new List<Service_Job_Det_Sub>();
                    _tempSub = _scvItemSubList;
                    _tempSub.RemoveAll(x => x.JBDS_JOBLINE == _line);
                    _scvItemSubList = _tempSub;

                    //remove Allocate Emp related to this item line
                    List<Service_Tech_Aloc_Hdr> _tempEmp = new List<Service_Tech_Aloc_Hdr>();
                    _tempEmp = _scvEmpList;
                    _tempEmp.RemoveAll(x => x.STH_JOBLINE == _line);
                    _scvEmpList = _tempEmp;

                    grvDef.AutoGenerateColumns = false;
                    grvDef.DataSource = new List<Service_Job_Defects>();
                    grvDef.DataSource = _scvDefList;

                    //remove item line
                    _scvItemList.RemoveAt(e.RowIndex);

                    grvJobItms.AutoGenerateColumns = false;
                    grvJobItms.DataSource = new List<Service_job_Det>();
                    grvJobItms.DataSource = _scvItemList;

                    load_item_det(0);
                }
            }
            else if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Do you want to change the serial?", "Inspection Job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Int32 _line = Convert.ToInt32(grvJobItms.Rows[e.RowIndex].Cells["jbd_jobline"].Value);
                    List<Service_job_Det> _jobDetRecall = CHNLSVC.CustService.GetJobDetails(txtReqNo.Text, _line, BaseCls.GlbUserComCode);
                    if (_jobDetRecall != null && _jobDetRecall.Count > 0)
                    {
                        recalledJob = _jobDetRecall[0];
                        // if (recalledJob.Jbd_stage == Convert.ToDecimal("1.1"))
                        {
                            txtSerialChange.Text = "";
                            lblCSItem.Text = "";
                            lblCSDescription.Text = "";
                            lblCSModel.Text = "";
                            pnlSearilChange.Visible = true;
                        }
                    }
                }
            }
        }

        private void btn_srch_def_type_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                _result = CHNLSVC.CommonSearch.GetDefectTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDef;
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.ShowDialog();
                txtDef.Focus();
                txtDef.SelectAll();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private bool load_def_desc()
        {
            bool _ok = false;
            lblDefDesc.Text = "";
            if (!string.IsNullOrEmpty(txtDef.Text))
            {
                DataTable _dt = CHNLSVC.CustService.getDefectTypes(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, lblItemCat.Text, txtDef.Text);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    { lblDefDesc.Text = _dt.Rows[0]["SD_DESC"].ToString(); _ok = true; }
                }
            }
            return _ok;
        }

        private void btn_srch_ser_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //Updated by akila 2017/05/02
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    _warrSearchtp = "ENGINE #";
                    _warrSearchorder = "SER";
                }
                else
                {
                    _warrSearchtp = _scvParam.SP_DB_SERIAL;
                    _warrSearchorder = "SER";
                }
                //_warrSearchtp = _scvParam.SP_DB_SERIAL;
                //_warrSearchorder = "SER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSer;
                _CommonSearch.ShowDialog();
                txtSer.Select();
                Cursor = Cursors.Default;

                //if ((_scvParam.SP_ISAUTOMOBI == 1) && (!string.IsNullOrEmpty(txtSer.Text)))
                //{
                //    if (_result.Rows.Count > 0)
                //    {
                //        txtRegNo.Text = _result.AsEnumerable().Where(x => x.Field<string>("Engine #") == txtSer.Text.Trim()).Select(x => x.Field<string>("Registration #")).First().ToString();
                //    }
                //}
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void btn_add_ser_Click(object sender, EventArgs e)
        {
            if (optReq.Checked == true)
            {
                if (string.IsNullOrEmpty(txtReqNo.Text))
                {
                    SystemInformationMessage("Please select request no", "Request");
                    txtReqNo.Focus();
                    return;
                }
            }
            if (_jobRecall == 1)
            {
                SystemInformationMessage("Can't add additional items for saved job!", "Not Allowed");
                return;
            }
            if (_scvParam.SP_MULTIITM == 0)
            {
                if (_scvItemList != null)
                {
                    Int32 _count = _scvItemList.Count();
                    if (_count > 0)
                    {
                        if (_jobRecall == 2 && _count == 1)
                        {
                        }
                        else
                        {
                            SystemInformationMessage("You have not allowed to add multiple items for the Job!", "Multiple Item Not Allowed");
                            return;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(lblItem.Text))
            {
                SystemInformationMessage("Please enter the job item details!", "Item not found");
                return;
            }
            _fgap = 0;
            if (Check_Serial_IsStock() == true) return;

            if (lblSerNo.Text.ToUpper() != "N/A")
            {
                Int32 _count = _scvItemList.Where(X => X.Jbd_itm_cd == lblItem.Text && X.Jbd_ser1 == lblSerNo.Text).Count();
                if (_count > 0)
                {
                    SystemInformationMessage("Already added this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                    return;
                }

                int _closeJobStage = 11;
                if (_scvParam.SP_ISNEEDGATEPASS == 0) _closeJobStage = 8;//edit by Chamal 16-Jun-2015

                List<Service_job_Det> _pendingjobdetList = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(lblSerNo.Text, lblItem.Text, BaseCls.GlbUserComCode);

                //_count = _pendingjobdetList.Where(X => X.Jbd_stage >= Convert.ToDecimal(1.1) && X.Jbd_stage <= 8).Count();
                //if (_count > 0)
                //{
                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                //    return;
                //}
                //_count = _pendingjobdetList.Where(X => X.Jbd_stage >= 13 && X.Jbd_stage <= 14).Count(); tharaka 2015-06-13
                //_count = _pendingjobdetList.Where(X => X.Jbd_stage < 11).Count();
                //  _count = _pendingjobdetList.Where(X => X.Jbd_stage < _closeJobStage).Count();
                _count = _pendingjobdetList.Where(X => X.Jbd_is_closed == 0).Count();// Nadeeka 19-10-2015
                if (_count > 0)
                {
                    //Add by akila 2017/06/12
                    if (_scvParam.SP_ALLOW_MULTI_JOB_FOR_SERIAL == 1)
                    {
                        DialogResult _result = MessageBox.Show("Pending job available for this item! Job No - " + _pendingjobdetList[0].Jbd_jobno + Environment.NewLine + "Do you want to continue?", "Add Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        SystemInformationMessage("Pending job available for this item! Job No - " + _pendingjobdetList[0].Jbd_jobno, "Duplicate" + _scvParam.SP_DB_SERIAL); //kapila 30/6/2015
                        return;
                    }

                    //SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);

                    ////Commented by akila 2017/06/12
                    //SystemInformationMessage("Pending job available for this item! Job No - " + _pendingjobdetList[0].Jbd_jobno, "Duplicate" + _scvParam.SP_DB_SERIAL); //kapila 30/6/2015
                    //return;
                }
            }
            if (lblWarNo.Text.ToUpper() != "N/A")
            {
                Int32 _count = _scvItemList.Where(X => X.Jbd_warr == lblWarNo.Text).Count();
                if (_count > 0)
                {
                    SystemInformationMessage("Already added this item!", "Duplicate Warranty No");
                    return;
                }
            }
            if (pnlMilage.Visible == true)
            {
                if (string.IsNullOrEmpty(txtMilage.Text))
                {
                    SystemInformationMessage("Please enter the current mileage reading!", "Mileage");
                    txtMilage.Focus();
                    return;
                }
            }

            //TODO: Check Job Details still pending serials available or no if available block adding

            //kapila 2/12/2016
            Boolean _isFound = false;
            if (lblSerNo.Text != "N/A")
            {
                DataTable _exchangeRequests = new DataTable();
                //_exchangeRequests = CHNLSVC.Sales.GetExchangeRequestBySerail(lblSerNo.Text.Trim(), 0);
                if (_exchangeRequests.Rows.Count > 0)
                {
                    string _userMessage = string.Format("Pending exchange request ({0}) available for the serial {1}", _exchangeRequests.Rows[0]["grah_ref"] == DBNull.Value ? string.Empty : _exchangeRequests.Rows[0]["grah_ref"].ToString(), lblSerNo.Text.Trim());
                    MessageBox.Show(_userMessage, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //_isFound = CHNLSVC.Sales.IsExchangeReqFound(lblSerNo.Text, 0);
                //if (_isFound == true)
                //{
                //    MessageBox.Show("Pending exchange request available for the serial " + lblSerNo.Text, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
            }

            _itmBrand = lblBrand.Text;
            _warStus = (lblWarStus.Text == "UNDER WARRANTY") ? 1 : 0;

            if (_reqDet != null && _reqDet.Count == 1)
            {
                add_job_item(1, _reqDet[0]);
            }
            else
            {
                add_job_item(1, null);
            }

            txtDef.Focus();

            //if (optReq.Checked)
            //{
            //    List<Service_Job_Defects> oItems = CHNLSVC.CustService.GetRequestJobDefectsJobEnty(txtReqNo.Text, _jobItemLine);
            //    grvDef.DataSource = new List<Service_Job_Defects>();
            //    grvDef.DataSource = oItems;
            //}

            if (optReq.Checked == true)
            {
                if (grvJobItms.Rows.Count > 0)
                {
                    grvJobItms_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }
                txtReqNo.Enabled = false;
                btn_srch_req.Enabled = false;
            }
        }

        private void btnCloseEItm_Click(object sender, EventArgs e)
        {
            pnlItem.Visible = false;
            if (ChkExternal.Checked == false)
            {
                if (optInspection.Checked == false) ChkExternal.Checked = false;
            }
        }

        private void grvJobItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = false;
                }
                grvJobItms.Rows[e.RowIndex].Cells["Jbd_select"].Value = true;

                load_item_det(e.RowIndex);

                string serial = grvJobItms.SelectedRows[0].Cells["JBD_SER1"].Value.ToString();
                string WarrantyNum = grvJobItms.SelectedRows[0].Cells["JBD_WARR"].Value.ToString();

                txtWar.Text = WarrantyNum;
                if (serial != "N/A")
                {
                    //kapila 23/6/2017
                    if (WarrantyNum != "N/A" && WarrantyNum != "NA")
                        Load_Serial_Infor(txtWar, WarrantyNum, dtDate.Value.Date);
                }
                txtWar.Clear();
                //private void Load_Serial_Infor(TextBox _txt, string _warrNo, DateTime _jobDt)

            }
        }

        private void load_item_det(Int32 _index)
        {
            if (_scvItemList != null)
            {
                if (_scvItemList.Count > 0)
                {
                    Service_job_Det _item = _scvItemList[_index];
                    lblItem.Text = _item.Jbd_itm_cd;
                    lblItemDesc.Text = _item.Jbd_itm_desc;
                    lblSerNo.Text = _item.Jbd_ser1;
                    txtPartNo.Text = _item.Jbd_ser2;    //kapila 9/7/2015
                    lblWarNo.Text = _item.Jbd_warr;
                    lblModel.Text = _item.Jbd_model;
                    lblBrand.Text = _item.Jbd_brand;
                    txtJobItemLine.Text = _item.Jbd_jobline.ToString();
                    lblItemCat.Text = _item.Jbd_cate1;

                    lblItmStus.Text = "GOOD";
                    if (!string.IsNullOrEmpty(_item.Jbd_itm_stus))
                    {
                        DataTable _dtStatus = CHNLSVC.Inventory.GetItemStatusMaster(_item.Jbd_itm_stus, null);  //kapila 30/6/2015
                        if (_dtStatus.Rows.Count > 0)
                            lblItmStus.Text = _dtStatus.Rows[0]["mis_desc"].ToString();
                    }

                    string _warrStatus = string.Empty;
                    if (_item.Jbd_warr_stus == 1) _warrStatus = "UNDER WARRANTY"; else _warrStatus = "OVER WARRANTY";

                    //if (_item..AddMonths(_warrItem.Irsm_warr_period).Date >= _jobDt.Date)
                    //{ _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green; }
                    //else
                    //{ _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red; }

                    lblWarStus.Text = _warrStatus;
                    //lblWarStart.Text = _warrItem.Irsm_doc_dt.ToString("dd-MMM-yyyy");
                    //lblWarEnd.Text = _warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).ToString("dd-MMM-yyyy");
                    //lblWarPrd.Text = _warrItem.Irsm_warr_period.ToString();
                    //lblWarRemain.Text = (_warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
                    //lblWarRem.Text = _warrItem.Irsm_warr_rem;

                    //lblInv.Text = _warrItem.Irsm_invoice_no;
                    //lblInvDt.Text = _warrItem.Irsm_invoice_dt.ToString("dd-MMM-yyyy");
                    //lblAccNo.Text = _warrItem.Irsm_acc_no;
                    //lblDelLoc.Text = _warrItem.Irsm_loc;
                    //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;
                }
            }
        }

        private void txtEItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_itm_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtESerial.Focus();
            }
        }

        private void txtEItem_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_itm_Click(null, null);
        }

        private void btn_add_def_Click(object sender, EventArgs e)
        {
            if (grvJobItms.Rows.Count <= 0)
            { SystemInformationMessage("First add the defect item!", "Job Item"); return; }
            else
            {
                if (string.IsNullOrEmpty(txtDef.Text))
                {
                    SystemInformationMessage("Please select the defect type!", "Job Defect"); return;
                }
                if (grvJobItms.Rows.Count == 1)
                {
                    for (int i = 0; i < grvJobItms.Rows.Count; i++)
                    {
                        grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                    }
                    load_item_det(0);

                    Int32 _jbLine1 = Convert.ToInt32(txtJobItemLine.Text);
                    Int32 _count1 = _scvDefList.Where(X => X.SRD_JOB_LINE == _jbLine1 && X.SRD_DEF_TP == txtDef.Text).Count();
                    if (_count1 > 0)
                    {
                        SystemInformationMessage("Defect type already added into the list!", "Defect Type");
                        return;
                    }

                    Service_Job_Defects _jobDef = new Service_Job_Defects();
                    _jobDef.SRD_DEF_LINE = _scvDefList.Count + 1;
                    _jobDef.SRD_DEF_TP = txtDef.Text;
                    _jobDef.SRD_DEF_RMK = txtDefRem.Text;
                    _jobDef.SRD_JOB_LINE = Convert.ToInt32(txtJobItemLine.Text);
                    _jobDef.jbd_itm_cd = lblItem.Text;
                    _jobDef.jbd_ser1 = lblSerNo.Text;
                    _jobDef.SDT_DESC = lblDefDesc.Text;

                    _scvDefList.Add(_jobDef);
                    txtEmpCode.Focus();
                }
                else
                {
                    bool _isSelect = false;
                    for (int i = 0; i < grvJobItms.Rows.Count; i++)
                    {
                        Int32 count = 0;
                        if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                        {
                            _isSelect = true;
                            load_item_det(i);
                            Service_Job_Defects _jobDef = new Service_Job_Defects();
                            _jobDef.SRD_DEF_LINE = count + 1;
                            _jobDef.SRD_DEF_TP = txtDef.Text;
                            _jobDef.SRD_DEF_RMK = txtDefRem.Text.Replace("'", "`");
                            _jobDef.SRD_JOB_LINE = Convert.ToInt32(txtJobItemLine.Text.ToString());
                            _jobDef.jbd_itm_cd = lblItem.Text;
                            _jobDef.jbd_ser1 = lblSerNo.Text;
                            _jobDef.SDT_DESC = lblDefDesc.Text;
                            _jobDef.SRD_JOB_NO = txtReqNo.Text;
                            _scvDefList.Add(_jobDef);
                            count++;
                        }
                    }

                    if (_isSelect == false)
                    {
                        SystemInformationMessage("Please select the job item", "Job Item");
                        return;
                    }
                }
            }

            Int32 _jbLine = Convert.ToInt32(txtJobItemLine.Text);
            Int32 _count = _scvDefList.Where(X => X.SRD_JOB_LINE == _jbLine && X.SRD_DEF_TP == txtDef.Text).Count();
            //if (_count > 0)
            //{
            //    SystemInformationMessage("Defect type already added into the list!", "Defect Type");
            //    return;
            //}

            if (_scvDefList != null)
            {
                if (_scvDefList.Count > 0)
                {
                    var nonNulls = _scvDefList.Where(X => X.SRD_JOB_LINE == _jbLine);
                    if (nonNulls.Count() > 0)
                    {
                        _count = _scvDefList.Where(X => X.SRD_JOB_LINE == _jbLine).Max(t => t.SRD_DEF_LINE);
                    }
                }
            }

            //Service_Job_Defects _jobDef = new Service_Job_Defects();
            //_jobDef.SRD_DEF_LINE = _count + 1;
            //_jobDef.SRD_DEF_TP = txtDef.Text;
            //_jobDef.SRD_DEF_RMK = txtDefRem.Text;
            //_jobDef.SRD_JOB_LINE = _jbLine;
            //_jobDef.jbd_itm_cd = lblItem.Text;
            //_jobDef.jbd_ser1 = lblSerNo.Text;
            //_jobDef.SDT_DESC = lblDefDesc.Text;

            //_scvDefList.Add(_jobDef);

            txtDef.Clear();
            txtDefRem.Clear();
            lblDefDesc.Text = "";

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Job_Defects>();
            grvDef.DataSource = _scvDefList;
        }

        private void grvDef_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    _scvDefList.RemoveAt(e.RowIndex);
                    grvDef.AutoGenerateColumns = false;
                    grvDef.DataSource = new List<Service_Job_Defects>();
                    grvDef.DataSource = _scvDefList;
                }
            }
        }

        private void txtDef_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDef.Text))
            {
                if (load_def_desc() == false)
                {
                    SystemInformationMessage("Invalid defect type!", "Defect Type");
                    txtDef.Clear();
                    txtDefRem.Clear();
                    txtDef.Focus();
                }
            }
        }

        private void btn_srch_warr_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //update by akila 2017/05/02
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    _warrSearchtp = "ENGINE #";
                    _warrSearchorder = "WARR";
                }
                else
                {
                    _warrSearchtp = _scvParam.SP_DB_SERIAL;
                    _warrSearchorder = "WARR";
                }
                //_warrSearchtp = _scvParam.SP_DB_SERIAL;
                //_warrSearchorder = "WARR";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtWar;
                _CommonSearch.ShowDialog();
                txtWar.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void btn_srch_regno_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                //Updated by akila 2017/05/02
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    _warrSearchtp = "REG #";
                    _warrSearchorder = "OTHER";
                }
                else
                {
                    _warrSearchtp = _scvParam.SP_DB_SERIAL;
                    _warrSearchorder = "OTHER";
                }
                //_warrSearchtp = _scvParam.SP_DB_SERIAL;
                //_warrSearchorder = "OTHER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegNo;
                _CommonSearch.ShowDialog();
                txtRegNo.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btn_srch_adv_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlMultiItems.Visible = false;
        }

        private void txtSer_Leave(object sender, EventArgs e)
        {
            if (_jobRecall != 1)
            {
                if (!string.IsNullOrEmpty(txtSer.Text))
                {
                    //kapila 27/3/2015
                    if (_scvParam.sp_is_job_by_rcc == 1)   //must open with rcc if exists
                    {
                        DataTable _dt = CHNLSVC.Inventory.getRCCbySerialWar(txtSer.Text, null);
                        if (_dt.Rows.Count > 0)
                        {
                            optReq.Checked = true;
                            txtReqNo.Text = _dt.Rows[0]["inr_no"].ToString();
                            txtReqNo_Leave(null, null);
                            return;
                        }
                    }
                    Load_Serial_Infor(txtSer, string.Empty, dtDate.Value.Date);
                }
            }

            if ((!string.IsNullOrEmpty(txtSer.Text)) && (_scvParam.SP_ISAUTOMOBI == 1))
            {
                string _registrationNo = GetRegistrationNumber(txtSer);
                if (!string.IsNullOrEmpty(_registrationNo))
                {
                    txtRegNo.Text = _registrationNo;
                }
            }
        }

        private void txtSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { btn_add_ser.Focus(); }
            else if (e.KeyCode == Keys.F2)
            { btn_srch_ser_Click(null, null); }
        }

        private DataTable _warrInfor = null;

        private void Load_Serial_Infor(TextBox _txt, string _warrNo, DateTime _jobDt)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
            List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
            Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

            string _ser = null;
            string _warr = null;
            string _regno = null;
            string _invcno = null;

            //Updated by akila 2017/05/03
            string _ser2 = null;
            string _item = null;
            string _tmpControlName = null; ;
            if (string.IsNullOrEmpty(_warrNo))
            {
                if (_txt == txtSer) { _ser = txtSer.Text.ToString().Trim(); _tmpControlName = lblSerSrch.Text; }
                if (_txt == txtWar) { _warr = txtWar.Text.ToString().Trim(); _tmpControlName = lblWarSrch.Text; }
                if (_txt == txtRegNo) { _regno = txtRegNo.Text.ToString().Trim(); _tmpControlName = lblRegNo.Text; }
                if (_txt == txtChassiNo) { _ser2 = txtChassiNo.Text.ToString().Trim(); _tmpControlName = lblChassiNo.Text; }
            }
            else
            {
                //_warr = _warr = txtWar.Text.ToString().Trim(); 
                _warr = _warr = _warrNo;
                _tmpControlName = lblWarSrch.Text;  //add by akila 2017/06/27
            }

            _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, _ser2, _regno, _warr, _invcno, _item, 0, out _returnStatus, out _returnMsg);
            if (_warrMstDic == null)
            {
                SystemInformationMessage("Records not found. Please check the " + _tmpControlName, "Job Entry - Information");
                _txt.Clear();
                _txt.Focus();
                return;
            }


            // Nadeeka Over Warranty Job 12-10-2015
            if (_warrMstDic != null)
            {
                if (_returnStatus == 2)
                {
                    _warStus = 0;
                    lblWarStus.Text = "OVER WARRANTY";

                }
            }



            foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
            {
                _warrMst = pair.Key;
                _warrMstSub = pair.Value;
            }
            if (_warrMst == null)
            {
                SystemInformationMessage("There is no warranty details available.", "No warranty");
                _txt.Clear();
                _txt.Focus();
                return;
            }
            if (_warrMst.Count <= 0)
            {
                SystemInformationMessage("There is no warranty details available.", "No warranty");
                _txt.Clear();
                _txt.Focus();
                return;
            }


            if (_warrMst.Count > 1)
            {
                gvMultipleItem.AutoGenerateColumns = false;
                gvMultipleItem.DataSource = new List<InventorySerialMaster>();
                gvMultipleItem.DataSource = _warrMst;
                pnlMultiItems.Visible = true;
                return;
            }
            else
            {
                FillItemDetails(_warrMst[0], _jobDt, _returnStatus);
                if (_warrMstSub != null)
                {
                    if (_warrMstSub.Count > 0)
                    {
                        Cursor = Cursors.WaitCursor;
                        _tempItemSubList = new List<Service_Job_Det_Sub>();
                        _tempItemSubList = FillItemSubDetails(_warrMstSub);
                        pnlAdditionalItems.Visible = true;

                        if (!string.IsNullOrEmpty(txtReqNo.Text) && optJob.Checked)
                        {
                            Int32 selectedItmLine = 0;
                            for (int i = 0; i < grvJobItms.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                                {
                                    //selectedItmLine = i;
                                    selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
                                }
                            }

                            List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtReqNo.Text, selectedItmLine);

                            grvAddiItems.AutoGenerateColumns = false;
                            grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
                            grvAddiItems.DataSource = _tempItemSubList;// oSubItems;
                            Cursor = Cursors.Default;

                            _scvItemSubList = new List<Service_Job_Det_Sub>();

                            for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                            {
                                if (oSubItems.FindAll(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()).Count > 0)
                                {
                                    grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
                                    _scvItemSubList.Add(oSubItems.Find(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()));
                                }

                                if (Convert.ToInt32(grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString()) < 0)
                                {
                                    grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value = "N/A";
                                }
                            }

                            //btnAddAdditional.Visible = true;
                        }
                        else
                        {
                            btnAddAdditional.Visible = true;

                            grvAddiItems.AutoGenerateColumns = false;

                            if (grvAddiItems.Rows.Count == 0)
                            {
                                grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
                                grvAddiItems.DataSource = _tempItemSubList;

                                for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                                {
                                    if (grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString() != "N/A" && Convert.ToInt32(grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value.ToString()) < 0)
                                    {
                                        grvAddiItems.Rows[i].Cells["JBDS_ITM_STUS_TEXT"].Value = "N/A";
                                    }

                                    grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = false;

                                    string itemCodess = grvAddiItems.Rows[i].Cells["jbds_itm_cd"].Value.ToString();
                                    if (_scvItemSubList.FindAll(x => x.JBDS_ITM_CD == itemCodess).Count > 0)
                                    {
                                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
                                    }
                                }
                            }
                            Cursor = Cursors.Default;
                        }
                        SetReadOnlyClmn();
                    }
                }
            }

            check_Blacklistcustomer();
        }

        private void gvMultipleItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultipleItem.ColumnCount > 0 && e.RowIndex >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                //InventorySerialMaster _oneWarr = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells["irsm_ser_id"].Value.ToString()));
                int _returnStatus = 0;
                string _returnMsg = string.Empty;
                List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
                List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
                Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;
                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster("", "", "", gvMultipleItem.Rows[e.RowIndex].Cells["IRSM_WARR_NO"].Value.ToString(), "", "", 0, out _returnStatus, out _returnMsg);
                if (_warrMstDic != null)    //kapila 19/7/2017
                {
                    foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                    {
                        _warrMst = pair.Key;
                        _warrMstSub = pair.Value;
                    }
                }

                if (_warrMst.Count == 1)
                {
                    FillItemDetails(_warrMst[0], dtDate.Value.Date, _returnStatus);
                }
                pnlMultiItems.Visible = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void FillItemDetails(InventorySerialMaster _warrItem, DateTime _jobDt, int _waraStatus)
        {
            _warrItemTemp = new InventorySerialMaster();
            _warrItemTemp = _warrItem;

            lblItem.Text = _warrItem.Irsm_itm_cd;
            lblItemDesc.Text = _warrItem.Irsm_itm_desc;
            lblModel.Text = _warrItem.Irsm_itm_model;
            lblBrand.Text = _warrItem.Irsm_itm_brand;
            //kapila 4/7/2017
            if (string.IsNullOrEmpty(lblBrand.Text))
            {
                MasterItem _miBrand = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _warrItem.Irsm_itm_cd);
                lblBrand.Text = _miBrand.Mi_brand;
            }
            lblSerNo.Text = _warrItem.Irsm_ser_1;
            txtPartNo.Text = _warrItem.Irsm_ser_2;  //kapila 9/7/2015
            lblWarNo.Text = _warrItem.Irsm_warr_no;

            lblItmStus.Text = "GOOD";
            if (!string.IsNullOrEmpty(_warrItem.Irsm_itm_stus))
            {
                DataTable _dtStatus = CHNLSVC.Inventory.GetItemStatusMaster(_warrItem.Irsm_itm_stus, null);  //kapila 30/6/2015
                if (_dtStatus.Rows.Count > 0)
                    lblItmStus.Text = _dtStatus.Rows[0]["mis_desc"].ToString();
            }


            string _warrStatus = string.Empty;
            if (_warrItem.Irsm_warr_start_dt.AddMonths(_warrItem.Irsm_warr_period).Date >= _jobDt.Date)
            { _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green; }
            else
            { _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red; }
            //  Nadeeka 12-10-2015
            if (_waraStatus == 2)
            {
                _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red;
            }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Irsm_warr_start_dt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Irsm_warr_start_dt.AddMonths(_warrItem.Irsm_warr_period).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Irsm_warr_period.ToString();

            //kapila 10/3/2016
            lblGenWar.Text = 0.ToString();
            //if (_warrItem.Irsm_gen_war_period!=null)
            //lblGenWar.Text = _warrItem.Irsm_gen_war_period.ToString();
            //if (_warrItem.Irsm_gen_war_rem!=null)
            //lblGenWarRem.Text = _warrItem.Irsm_gen_war_rem.ToString();

            if (Convert.ToInt32(lblWarPrd.Text) != Convert.ToInt32(lblGenWar.Text))
                lblWarPrd.ForeColor = Color.Green;
            else
                lblWarPrd.ForeColor = Color.Black;


            //kapila 9/7/2015
            DataTable _dtInv = CHNLSVC.CustService.getSalesTypeByInvNo(_warrItem.Irsm_invoice_no);
            if (_dtInv.Rows.Count > 0)
                lblInvTp.Text = _dtInv.Rows[0]["srtp_desc"].ToString();

            lblWarRemain.Text = (_warrItem.Irsm_warr_start_dt.AddMonths(_warrItem.Irsm_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
            //KAPILA 26/5/2015
            if (Convert.ToInt32(lblWarRemain.Text) < 0) lblWarRemain.Text = "N/A";
            lblWarRem.Text = _warrItem.Irsm_warr_rem;
            lblSwrrRemain.Text = (_warrItem.Irsm_sup_warr_stdt.AddMonths(_warrItem.Irsm_sup_warr_pd).Date - dtDate.Value.Date).TotalDays.ToString();
            lblSwrrPeriod.Text = _warrItem.Irsm_sup_warr_pd.ToString();
            lblSwrrRemark.Text = _warrItem.Irsm_sup_warr_rem;
            //Chamal 14/6/2015
            pnlOthWarr.Visible = false;
            if (!string.IsNullOrEmpty(_warrItem.Irsm_oth_warr_pd.ToString()) && _warrItem.Irsm_oth_warr_pd > 0)
            {
                lblOthwrrRemain.Text = (_warrItem.Irsm_oth_warr_stdt.AddMonths(_warrItem.Irsm_oth_warr_pd).Date - dtDate.Value.Date).TotalDays.ToString();
                lblOthwrrPeriod.Text = _warrItem.Irsm_oth_warr_pd.ToString();
                lblOthwrrRemark.Text = _warrItem.Irsm_oth_warr_rem;
                lblWarrStartDate.Text = _warrItem.Irsm_oth_warr_stdt.ToString("dd/MMM/yyyy");
                pnlOthWarr.Visible = true;
            }


            lblInv.Text = _warrItem.Irsm_invoice_no;

            // Nadeeka 30-07-2015
            #region Free Item
            Boolean _foundprice = false;
            List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
            _InvDetailList = CHNLSVC.Sales.GetInvoiceItems(lblInv.Text.Trim());

            if (_InvDetailList != null)
            {
                foreach (InvoiceItem _ser in _InvDetailList.Where(x => x.Sad_itm_cd == _warrItem.Irsm_itm_cd || x.Sad_sim_itm_cd == _warrItem.Irsm_itm_cd))
                {
                    _foundprice = true;
                    if (_ser.Sad_tot_amt / _ser.Sad_qty == 0)
                    {
                        _isFree = true;
                    }
                    lblUnitPrice.Text = Convert.ToString(_ser.Sad_tot_amt / _ser.Sad_qty);
                }
            }
            else
            {
                DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetailWithCom(BaseCls.GlbUserComCode, lblInv.Text.Trim(), _warrItem.Irsm_itm_cd);
                if (_invoicedt != null || _invoicedt.Rows.Count > 0)
                {
                    foreach (DataRow _r in _invoicedt.Rows)
                    {
                        _foundprice = true;
                        if (_r.Field<string>("SAD_ITM_CD") == _warrItem.Irsm_itm_cd)
                        {
                            if (_r.Field<decimal>("SAD_TOT_AMT") / _r.Field<decimal>("ACTUAL_QTY") == 0)
                            {
                                _isFree = true;
                            }
                            lblUnitPrice.Text = Convert.ToString(_r.Field<decimal>("SAD_TOT_AMT") / _r.Field<decimal>("ACTUAL_QTY"));
                        }
                    }

                }

            }

            if (_foundprice == false)
            {
                // ger replacement item original invoce price   Nadeeka 12-08-2015
                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem("", _warrItem.Irsm_itm_cd);
                if (_itemdetail.Mi_is_ser1 == 1)
                {
                    if (_warrItem.Irsm_ser_1 != "N/A")
                    {
                        DataTable _repHistory = CHNLSVC.Inventory.GetReplaceOriginalItems(_warrItem.Irsm_ser_1);

                        if (_repHistory.Rows.Count > 0)
                        {
                            lblUnitPrice.Text = _repHistory.Rows[0]["grh_unit_price"].ToString();
                            if (Convert.ToDecimal(lblUnitPrice.Text) == 0)
                            {
                                _isFree = true;
                            }
                        }
                    }
                }
            }

            if (_isFree == true)
            {
                lblFOC.Visible = true;
            }
            else
            {
                lblFOC.Visible = false;
            }
            #endregion



            lblInvDt.Text = _warrItem.Irsm_invoice_dt.ToString("dd-MMM-yyyy");
            lblAccNo.Text = _warrItem.Irsm_acc_no;
            lblDelLoc.Text = _warrItem.Irsm_loc;
            lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            if (_jobRecall == 0)
            {
                if (optReq.Checked == false)
                {
                    txtMobile.Text = _warrItem.Irsm_cust_mobile;
                    GroupBussinessEntity _custGrup1 = null;
                    _custGrup1 = CHNLSVC.Sales.GetCustomerProfileByGrup(_warrItem.Irsm_cust_cd, "", "", "", "", "");
                    if (string.IsNullOrEmpty(_custGrup1.Mbg_cd)) _warrItem.Irsm_cust_cd = "CASH.";
                    txtCustCode.Text = _warrItem.Irsm_cust_cd;
                    txtCusName.Text = _warrItem.Irsm_cust_name;
                    txtAddress1.Text = _warrItem.Irsm_cust_addr;
                    txtEmail.Text = _warrItem.Irsm_cust_email;
                }
            }

            List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
            _preSerJob = CHNLSVC.CustService.getPrejobDetails(BaseCls.GlbUserComCode, txtSer.Text.Trim(), lblItem.Text.Trim());

            lblAttempt.Text = _preSerJob.Count.ToString();

            lblSuppCode.Text = _warrItem.Irsm_orig_supp;
            lblSuppName.Text = _warrItem.Irsm_exist_supp;
            txtItmTp.Text = _warrItem.Irsm_anal_3;
            lblItemCat.Text = _warrItem.Irsm_anal_4;

            _scvjobHdr.SJB_CUST_CD = _warrItem.Irsm_cust_cd;
            _scvjobHdr.SJB_CUST_NAME = _warrItem.Irsm_cust_name;
            _scvjobHdr.SJB_ADD1 = _warrItem.Irsm_cust_addr;
            _scvjobHdr.SJB_MOBINO = _warrItem.Irsm_cust_mobile;

            lblBuyerCustCode.Text = _scvjobHdr.SJB_CUST_CD;
            lblBuyerCustName.Text = _scvjobHdr.SJB_CUST_NAME;
            lblBuyerCustAdd1.Text = _scvjobHdr.SJB_ADD1;
            lblBuyerCustAdd2.Text = _scvjobHdr.SJB_ADD2;
            lblBuyerCustMobi.Text = _scvjobHdr.SJB_MOBINO;

            // lblUnitPrice.Text = _warrItem.Irsm_unit_price.ToString();

            FillPriority();

            if (_warrItem.Irsm_sup_warr_stdt == DateTime.MinValue)
            {
                pnlSuppWarr.Visible = false;
                pnlBottom.Location = new Point(0, 125);
            }
            else
            {
                pnlSuppWarr.Visible = true;
                pnlBottom.Location = new Point(0, 216);
            }

            if (_warrItem.PartNumber != null && _warrItem.PartNumber != "N/A")
            {
                lblPartNo.Text = _warrItem.PartNumber;
                lblPartNo.Visible = true;
                label111.Visible = true;
                label113.Visible = true;
                lblModel.Size = new System.Drawing.Size(67, 14);
            }
            else
            {
                lblModel.Size = new System.Drawing.Size(203, 14);
                label111.Visible = false;
                label113.Visible = false;
                lblPartNo.Visible = false;
            }

            checkCustomer(null, _scvjobHdr.SJB_CUST_CD);
            if (_isBlack == true)
            {
                MessageBox.Show("Customer is black listed.", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //if (txtCustCode.Text == "CASH")
            //{
            //    MessageBox.Show("Please select valid customer.Cannot use customer code as [CASH].", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}
        }

        private InventorySerialMaster FillReqItemDetails(Service_Req_Hdr _reqHdr, Service_Req_Det _reqItm)
        {
            InventorySerialMaster _warrItem = new InventorySerialMaster();

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem("", _reqItm.Jrd_itm_cd);

            _warrItem.Irsm_itm_cd = _reqItm.Jrd_itm_cd;
            _warrItem.Irsm_itm_desc = _reqItm.Jrd_itm_desc;
            _warrItem.Irsm_itm_model = _reqItm.Jrd_model;
            _warrItem.Irsm_itm_brand = _reqItm.Jrd_brand;
            _warrItem.Irsm_ser_1 = _reqItm.Jrd_ser1;
            _warrItem.Irsm_warr_no = _reqItm.Jrd_warr;
            _warrItem.Irsm_doc_dt = _reqItm.Jrd_warrstartdt;
            _warrItem.Irsm_warr_period = _reqItm.Jrd_warrperiod;
            _warrItem.Irsm_warr_rem = _reqItm.Jrd_warrrmk;
            _warrItem.Irsm_warr_start_dt = _reqItm.Jrd_warrstartdt;
            _warrItem.Irsm_invoice_no = _reqItm.Jrd_invc_no;
            _warrItem.Irsm_invoice_dt = _reqItm.Jrd_date_pur;
            _warrItem.Irsm_acc_no = _reqItm.Jrd_msnno;
            //_warrItem.Irsm_loc = "";
            //_warrItem.Irsm_loc_desc = "";

            _warrItem.Irsm_cust_mobile = _reqHdr.Srb_mobino;
            _warrItem.Irsm_cust_cd = _reqHdr.Srb_cust_cd;
            _warrItem.Irsm_cust_name = _reqHdr.Srb_cust_name;
            _warrItem.Irsm_cust_addr = _reqHdr.Srb_add1;

            _warrItem.Irsm_orig_supp = _reqItm.Jrd_supp_cd;//Added by Chamal 20-Aug-2015
            _warrItem.Irsm_exist_supp = _reqItm.Jrd_supp_cd;//Added by Chamal 20-Aug-2015
            _warrItem.Irsm_anal_3 = _masterItem.Mi_itm_tp;
            _warrItem.Irsm_anal_4 = _masterItem.Mi_cate_1;

            return _warrItem;
        }

        private List<Service_Job_Det_Sub> FillItemSubDetails(List<InventorySubSerialMaster> _warrItemSub)
        {
            int _SubLine = 0;
            List<Service_Job_Det_Sub> _newSubList = new List<Service_Job_Det_Sub>();
            foreach (InventorySubSerialMaster _sub in _warrItemSub)
            {
                Service_Job_Det_Sub _newSub = new Service_Job_Det_Sub();
                _newSub.JBDS_AVAILABILTY = 1;
                _newSub.JBDS_BRAND = "";
                _newSub.JBDS_CRE_DT = DateTime.Now.Date;
                _newSub.JBDS_ISSUB = 1;
                _newSub.JBDS_ITM_CD = _sub.Irsms_itm_cd;
                _newSub.JBDS_ITM_COST = 0;
                _newSub.JBDS_ITM_DESC = _sub.Irsms_warr_no;
                _newSub.JBDS_ITM_STUS = "GOD";
                //_newSub.JBDS_ITM_STUS_TEXT = "";
                _newSub.JBDS_ITM_TP = _sub.Irsms_tp;
                _newSub.JBDS_JOBLINE = Convert.ToInt32(txtJobItemLine.Text.ToString());
                _newSub.JBDS_JOBNO = "";
                _newSub.JBDS_MODEL = "";
                //_newSub.JBDS_NEED_REPLACE = 0;
                _newSub.JBDS_QTY = 1;
                //_newSub.JBDS_REPL_ITMCD = "";
                //_newSub.JBDS_REPL_SERID = 0;
                //_newSub.JBDS_RTN_WH = 0;
                //_newSub.JBDS_SEQ_NO = 0;
                _newSub.JBDS_SER1 = _sub.Irsms_sub_ser;
                //_newSub.JBDS_SER2 = "";
                //_newSub.JBDS_SJOBNO = "";
                _newSub.JBDS_WARR = lblWarNo.Text;
                _newSub.JBDS_WARR_PERIOD = _sub.Irsms_warr_period;
                _newSub.JBDS_WARR_RMK = _sub.Irsms_warr_rem;

                if (Convert.ToDateTime(lblWarStart.Text.ToString()).AddMonths(_sub.Irsms_warr_period).Date >= dtDate.Value.Date)
                { _newSub.JBDS_CRE_BY = "UNDER WARRANTY"; }
                else
                { _newSub.JBDS_CRE_BY = "OVER WARRANTY"; }

                _newSub.JBDS_ITM_STUS_TEXT = (Convert.ToDateTime(lblWarStart.Text.ToString()).AddMonths(_sub.Irsms_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
                _newSub.JBDS_SEQ_NO = _SubLine + 1;
                _newSubList.Add(_newSub);
            }
            return _newSubList;
        }

        private void txtEItem_Leave(object sender, EventArgs e)
        {
            lblItemCat.Text = "";
            if (!string.IsNullOrEmpty(txtEItem.Text))
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem("", txtEItem.Text.ToUpper());
                if (_item != null)
                {
                    txtEItemDesc.Text = _item.Mi_shortdesc;
                    txtEModel.Text = _item.Mi_model;
                    txtEBrand.Text = _item.Mi_brand;
                    lblItemCat.Text = _item.Mi_cate_1;
                    if (Convert.ToInt32(_item.Mi_is_editshortdesc) == 1)
                        txtEItemDesc.ReadOnly = false;
                    else
                        txtEItemDesc.ReadOnly = true;

                    //kapila 27/3/2015
                    DataTable _dt = CHNLSVC.Inventory.GetSuplierByItem(BaseCls.GlbUserComCode, txtEItem.Text.ToUpper());
                    if (_dt.Rows.Count == 1)
                    {
                        txtSupplier.Text = _dt.Rows[0]["mbii_cd"].ToString();
                        txtSupplier_Leave(null, null);
                    }
                    else
                        txtSupplier.Text = "N/A";

                    //kapila 19/2/2016
                    if (_item.Mi_itm_tp == "V")
                    {
                        txtEBrand.ReadOnly = false;
                        btn_srch_brand.Enabled = true;
                    }
                    else
                    {
                        txtEBrand.ReadOnly = true;
                        btn_srch_brand.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEItem.Focus();
                    return;
                }
            }
        }

        private void txtESerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtESerial2.Focus();
            }
        }

        private void txtEWarr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbWarStus.Focus();
            }
        }

        private void cmbWarStus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtEWarStart.Focus();
            }
        }

        private void dtEWarStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtEWarEnd.Focus();
            }
        }

        private void dtEWarEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEWarRem.Focus();
            }
        }

        private void txtEWarRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEInv.Focus();
            }
        }

        private void txtEInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtEInvDt.Focus();
            }
        }

        private void dtEInvDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEAccNo.Focus();
            }
        }

        private void txtEAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        private void txtDef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_def_type_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDefRem.Focus();
            }
        }

        private void btnEmpSrchTD_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEmpCode;
            _CommonSearch.ShowDialog();
            txtEmpCode.Select();
            Cursor = Cursors.Default;
        }

        private void btnMore1_Click(object sender, EventArgs e)
        {
            if (btnMore1.Text == "More >>")
            {
                pnlWarr.Height = 293;
                if (optReq.Checked == true) pnlWarr.Height = 392;

                btnMore1.Text = "<< More";
                btnMore1.BackColor = Color.Orange;
                btnMore1.ForeColor = Color.Maroon;

                if (pnlSuppWarr.Visible == true)
                {
                    pnlWarr.Height = 488;
                }
                else
                {
                    pnlWarr.Height = 392;
                }

                //add by akila 2017/11/06
                decimal _warraPeriod = 0;
                decimal _genWarraPeriod = 0;

                decimal.TryParse(lblWarPrd.Text, out _warraPeriod);
                decimal.TryParse(lblGenWar.Text, out _genWarraPeriod);

                //kapila 10/3/2016
                if (_warraPeriod != _genWarraPeriod)
                    pnlGenWar.Visible = true;
                else
                    pnlGenWar.Visible = false;

                ////kapila 10/3/2016
                //if (Convert.ToDecimal(lblWarPrd.Text) != Convert.ToDecimal(lblGenWar.Text))
                //    pnlGenWar.Visible = true;
                //else
                //    pnlGenWar.Visible = false;

            }
            else
            {
                pnlWarr.Height = 109;
                btnMore1.Text = "More >>";
                btnMore1.BackColor = Color.Maroon;
                btnMore1.ForeColor = Color.White;
                pnlGenWar.Visible = false;
            }

            if (optReq.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtRegNo.Text))
                {
                    if (!string.IsNullOrEmpty(_reqHdr.Srb_cust_cd))
                    {
                        lblBuyerCustCode.Text = _reqHdr.Srb_cust_cd;
                        lblBuyerCustName.Text = _reqHdr.Srb_cust_name;
                        lblBuyerCustAdd1.Text = _reqHdr.Srb_add1;
                        lblBuyerCustAdd2.Text = _reqHdr.Srb_add2;
                        lblBuyerCustMobi.Text = _reqHdr.Srb_mobino;
                    }
                }
            }

            //if (btnMore1.Text == "More >>")
            //{
            //    pnlWarr.Height = 293;
            //    btnMore1.Text = "<< More";
            //}
            //else
            //{
            //    pnlWarr.Height = 109;
            //    btnMore1.Text = "More >>";
            //}
        }

        private void txtDefRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_add_def.Focus();
            }
        }

        private void txtSer_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_ser_Click(null, null);
        }

        private void txtWar_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_warr_Click(null, null);
        }

        private void txtWar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            { btn_srch_warr_Click(null, null); }
            else if (e.KeyCode == Keys.Enter)
            { btn_add_ser.Focus(); }
        }

        private void txtDef_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_def_type_Click(null, null);
        }

        private void btn_srch_stage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceStageChages);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvStageChg(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtVisitChgCode;
            _CommonSearch.ShowDialog();
            txtVisitChgCode.Select();
            this.Cursor = Cursors.Default;
        }

        private void btn_srch_req_Click(object sender, EventArgs e)
        {
            oldJobNumber = txtReqNo.Text;

            if (optReq.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                _jobStage = "1";
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceRequests);
                _CommonSearch.dtpTo.Value = DateTime.Now.Date;
                _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-3);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceRequests(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtReqNo.Focus();
                btn_add_ser.Focus();
            }
            else if (optJob.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                _jobStage = "2_11";
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                _CommonSearch.dtpTo.Value = DateTime.Now.Date;
                _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtReqNo.Focus();

            }
            else if (optInspection.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                _jobStage = "1.1";
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceRequests);
                _CommonSearch.dtpTo.Value = DateTime.Now.Date;
                _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-3);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceRequests(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtReqNo.Focus();
                btn_add_ser.Focus();
                //this.Cursor = Cursors.WaitCursor;
                //_jobStage = "1.1";
                //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                //_CommonSearch.dtpTo.Value = DateTime.Now.Date;
                //_CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
                //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtReqNo;
                //this.Cursor = Cursors.Default;
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtReqNo.Focus();
            }
        }

        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btnSearchSupervisor.Focus(); 
                btn_add_Tech.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            { btnEmpSrchTD_Click(null, null); }
        }

        private void txtEmpCode_DoubleClick(object sender, EventArgs e)
        {
            btnEmpSrchTD_Click(null, null);
        }

        private void txtEmpCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtEmpCode.Text);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    //var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_def_profit") == BaseCls.GlbUserDefProf.ToString() && x.Field<string>("esep_epf") == txtEmpCode.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
                    var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_epf") == txtEmpCode.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
                    if (query != null && query.Count > 0)
                    {
                        dtTemp = new DataTable();
                        dtTemp = query.CopyToDataTable();
                        lblEmpName.Text = dtTemp.Rows[0]["esep_first_name"].ToString();
                    }
                    else
                    {
                        SystemInformationMessage("Please enter correct EPF number", "EPF No");
                        txtEmpCode.Clear();
                        lblEmpName.Text = "";
                        txtEmpCode.Focus();
                        return;
                    }
                }
                else
                {
                    SystemInformationMessage("Please enter correct EPF number", "EPF No");
                    txtEmpCode.Clear();
                    lblEmpName.Text = "";
                    txtEmpCode.Focus();
                    return;
                }
            }
        }

        private void btn_add_Tech_Click(object sender, EventArgs e)
        {
            if (grvJobItms.Rows.Count <= 0)
            { SystemInformationMessage("Please select a job item.", "Job Item"); return; }

            if (string.IsNullOrEmpty(txtEmpCode.Text))
            { SystemInformationMessage("Please enter the technician.", "Employee"); return; }

            if (dtpFromAL.Value > dtpToAL.Value)
            { SystemInformationMessage("Please enter the valid period.", "Allocate Period"); return; }

            if (grvJobItms.Rows.Count == 1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                }
                load_item_det(0);

                //kapila 9/7/2015
                int _jbLine = Convert.ToInt32(txtJobItemLine.Text);
                if (_scvEmpList != null && _scvEmpList.Count > 0)
                {
                    int _count = _scvEmpList.Where(X => X.STH_JOBLINE == _jbLine && X.STH_EMP_CD == txtEmpCode.Text).Count();
                    if (_count > 0)
                    { SystemInformationMessage("Technician already assign for this item!", "Technician"); return; }
                }

                Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();
                Service_job_Det _jobItem = _scvItemList.Where(x => x.Jbd_jobline == _jbLine).ToList()[0];

                oAloc_Hdr.STH_SEQ = 0;
                oAloc_Hdr.STH_ALOCNO = "";
                oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                oAloc_Hdr.STH_TP = "J";
                oAloc_Hdr.STH_JOBNO = _jobItem.Jbd_jobno;
                oAloc_Hdr.STH_JOBLINE = _jobItem.Jbd_jobline;
                oAloc_Hdr.STH_EMP_CD = txtEmpCode.Text;
                oAloc_Hdr.STH_STUS = "A";
                oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                oAloc_Hdr.STH_TOWN = "";
                oAloc_Hdr.MT_DESC = "";
                oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                oAloc_Hdr.STH_REQNO = "";
                oAloc_Hdr.STH_REQLINE = 0;
                oAloc_Hdr.STH_TERMINAL = 0;
                oAloc_Hdr.ESEP_FIRST_NAME = lblEmpName.Text;
                oAloc_Hdr.STH_JOB_ITM = _jobItem.Jbd_itm_cd;
                oAloc_Hdr.STH_SER = _jobItem.Jbd_ser1;

                _scvEmpList.Add(oAloc_Hdr);
            }
            else
            {
                bool _isSelect = false;
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                    {
                        _isSelect = true;
                        load_item_det(i);

                        //kapila 9/7/2015
                        int _jbLine = Convert.ToInt32(txtJobItemLine.Text);
                        if (_scvEmpList != null && _scvEmpList.Count > 0)
                        {
                            int _count = _scvEmpList.Where(X => X.STH_JOBLINE == _jbLine && X.STH_EMP_CD == txtEmpCode.Text).Count();
                            if (_count > 0)
                            { SystemInformationMessage("Technician already assign for this item!", "Technician"); return; }
                        }

                        Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();
                        Service_job_Det _jobItem = _scvItemList.Where(x => x.Jbd_jobline == _jbLine).ToList()[0];

                        oAloc_Hdr.STH_SEQ = 0;
                        oAloc_Hdr.STH_ALOCNO = "";
                        oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                        oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                        oAloc_Hdr.STH_TP = "J";
                        oAloc_Hdr.STH_JOBNO = _jobItem.Jbd_jobno;
                        oAloc_Hdr.STH_JOBLINE = _jobItem.Jbd_jobline;
                        oAloc_Hdr.STH_EMP_CD = txtEmpCode.Text;
                        oAloc_Hdr.STH_STUS = "A";
                        oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                        oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                        oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                        oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                        oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                        oAloc_Hdr.STH_TOWN = "";
                        oAloc_Hdr.MT_DESC = "";
                        oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                        oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                        oAloc_Hdr.STH_REQNO = "";
                        oAloc_Hdr.STH_REQLINE = 0;
                        oAloc_Hdr.STH_TERMINAL = 0;
                        oAloc_Hdr.ESEP_FIRST_NAME = lblEmpName.Text;
                        oAloc_Hdr.STH_JOB_ITM = _jobItem.Jbd_itm_cd;
                        oAloc_Hdr.STH_SER = _jobItem.Jbd_ser1;
                        _scvEmpList.Add(oAloc_Hdr);

                    }
                }

                if (_isSelect == false)
                {
                    SystemInformationMessage("Please select the job item", "Job Item");
                    return;
                }
            }


            //int _jbLine = Convert.ToInt32(txtJobItemLine.Text);
            //if (_scvEmpList != null && _scvEmpList.Count > 0)
            //{
            //    int _count = _scvEmpList.Where(X => X.STH_JOBLINE == _jbLine && X.STH_EMP_CD == txtEmpCode.Text).Count();
            //    if (_count > 0)
            //    { SystemInformationMessage("Technician already assign for this item!", "Technician"); return; }
            //}

            //Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();
            //Service_job_Det _jobItem = _scvItemList.Where(x => x.Jbd_jobline == _jbLine).ToList()[0];

            //oAloc_Hdr.STH_SEQ = 0;
            //oAloc_Hdr.STH_ALOCNO = "";
            //oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
            //oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
            //oAloc_Hdr.STH_TP = "J";
            //oAloc_Hdr.STH_JOBNO = _jobItem.Jbd_jobno;
            //oAloc_Hdr.STH_JOBLINE = _jobItem.Jbd_jobline;
            //oAloc_Hdr.STH_EMP_CD = txtEmpCode.Text;
            //oAloc_Hdr.STH_STUS = "A";
            //oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
            //oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
            //oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
            //oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
            //oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
            //oAloc_Hdr.STH_TOWN = "";
            //oAloc_Hdr.MT_DESC = "";
            //oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
            //oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
            //oAloc_Hdr.STH_REQNO = "";
            //oAloc_Hdr.STH_REQLINE = 0;
            //oAloc_Hdr.STH_TERMINAL = 0;
            //oAloc_Hdr.ESEP_FIRST_NAME = lblEmpName.Text;
            //oAloc_Hdr.STH_JOB_ITM = _jobItem.Jbd_itm_cd;
            //oAloc_Hdr.STH_SER = _jobItem.Jbd_ser1;
            //_scvEmpList.Add(oAloc_Hdr);




            txtEmpCode.Clear();
            lblEmpName.Text = "";
            // dtpFromAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);
            // dtpToAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
            grvTech.DataSource = _scvEmpList;

            // txtMobile.Focus();
        }

        private void grvTech_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    _scvEmpList.RemoveAt(e.RowIndex);
                    grvTech.AutoGenerateColumns = false;
                    grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
                    grvTech.DataSource = _scvEmpList;
                }
            }
        }

        private void ChkExternal_Click(object sender, EventArgs e)
        {
            if (ChkExternal.Checked == true)
            {
                btnShowExtItemPanel.Visible = true;
                if (optInspection.Checked == true)
                {
                    ChkExternal.Checked = true;
                    pnlItem.Visible = true;
                    //kapila 25/5/2017
                    txtESerial.Text = "N/A";
                    txtEWarr.Text = "N/A";
                    txtSupplier.Text = "N/A";
                    pnlSer.Enabled = false;
                    optJob.Enabled = false;
                    optReq.Enabled = false;
                    txtESerial2.Text = null; //By akila 2017/06/24 
                    btn_add_ser.Enabled = false;
                    //txtEItem.Focus();
                }
                else
                {
                    if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        ChkExternal.Checked = false;
                        btn_add_ser.Enabled = true;
                        return;
                    }
                    ClearScreen();
                    ChkExternal.Checked = true;
                    pnlItem.Visible = true;
                    //kapila 25/5/2017
                    txtESerial.Text = "N/A";
                    txtEWarr.Text = "N/A";
                    txtSupplier.Text = "N/A";
                    pnlSer.Enabled = false;
                    optJob.Enabled = false;
                    optReq.Enabled = false;
                    txtEItem.Focus();
                    ChkExternal.Enabled = false;
                    txtESerial2.Text = null; //By akila 2017/06/24 
                    btn_add_ser.Enabled = false;
                }
            }
            else
            {
                btnShowExtItemPanel.Visible = false;
                Clear();
                ChkExternal.Checked = false;
                pnlItem.Visible = false;
                pnlSer.Enabled = true;
                optJob.Enabled = true;
                optReq.Enabled = true;
                txtSer.Focus();
                btn_add_ser.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grvJobItms.Rows.Count == 0)
            {
                SystemInformationMessage("Job item(s) not found!", "Job Item");


                return;
            }

            foreach (Service_job_Det _jobDet in _scvItemList)
            {
                MasterItemBlock _block = new MasterItemBlock();
                _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _jobDet.Jbd_itm_cd, 9, "P");
                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                {

                    MessageBox.Show("Item : " + _jobDet.Jbd_itm_cd + " is not allow to place job for selected profit center. Please contact Costing Department.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _jobDet.Jbd_itm_cd, 9, "L");
                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                {

                    MessageBox.Show("Item : " + _jobDet.Jbd_itm_cd + " is not allow to place job for selected service center. Please contact Costing Department.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            if (grvDef.Rows.Count == 0)
            {
                SystemInformationMessage("Defect not found!", "Job Defect");
                return;
            }

            if (optInspection.Checked == true)
            {
                if (string.IsNullOrEmpty(txtTown.Text))
                {
                    SystemInformationMessage("Please enter customer live in area!", "Town");
                    txtTown.Focus();
                    return;
                }
            }

            if (txtTown.Tag == null) txtTown.Tag = "0";

            if (optInspection.Checked == true)
            {
                //if (string.IsNullOrEmpty(txtVisitChgCode.Text))
                //{
                //SystemInformationMessage("Please enter visit charge!", "Inspection Job");
                //txtVisitChgCode.Focus();
                //return;
                //}

                if (string.IsNullOrEmpty(_itmBrand)) _itmBrand = lblBrand.Text;
            }
            if (string.IsNullOrEmpty(_itmBrand)) _itmBrand = lblBrand.Text;

            if (optReq.Checked == false && !string.IsNullOrEmpty(txtCustCode.Text))
            {
                if (txtCustCode.Text == "CASH")
                {
                    SystemInformationMessage("Please enter a valid customer code.", "Job entry");
                    txtCustCode.Text = "";
                    txtCustCode.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtCustCode.Text))
            {
                SystemInformationMessage("Please enter a customer code.", "Job entry");
                txtCustCode.Text = "";
                txtCustCode.Focus();
                return;
            }

            if (optJob.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtVisitChgCode.Text))
                {
                    if (ucPayModes1.RecieptItemList == null || ucPayModes1.RecieptItemList.Count <= 0)
                    {
                        SystemInformationMessage("Please enter the payment.", "Payment");
                        txtVisitChgCode.Focus();
                        return;
                    }
                }
            }


            txtAddress1.Text = txtAddress1.Text.Replace("'", "`");
            txtAddress2.Text = txtAddress2.Text.Replace("'", "`");
            txtContLoc.Text = txtContLoc.Text.Replace("'", "`");
            txtInfoLoc.Text = txtInfoLoc.Text.Replace("'", "`");
            txtDefRem.Text = txtDefRem.Text.Replace("'", "`");
            txtManRef.Text = txtManRef.Text.Replace("'", "`");
            txtOrdRef.Text = txtOrdRef.Text.Replace("'", "`");
            txtReqRmk.Text = txtReqRmk.Text.Replace("'", "`");
            txtTechIns.Text = txtTechIns.Text.Replace("'", "`");
            txtJobRem.Text = txtJobRem.Text.Replace("'", "`");
            txtEmail.Text = txtEmail.Text.Replace("'", "`");

            if (MessageBox.Show("Are you sure ?", "Job Entry", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (_scvjobHdr != null)
            {
                if (!string.IsNullOrEmpty(_scvjobHdr.SJB_CHG_CD))
                {
                    SystemInformationMessage("Can't update the job, because payment is available!", "Job Update");
                    return;
                }
            }

            if (_jobRecall > 0)
            {
                bool _Stageup = false;
                List<Service_job_Det> _jobDetRecall = CHNLSVC.CustService.GetJobDetails(txtReqNo.Text, -777, BaseCls.GlbUserComCode);
                if (_jobDetRecall != null && _jobDetRecall.Count > 0)
                {
                    foreach (Service_job_Det _dtt in _jobDetRecall)
                    {
                        if (_dtt.Jbd_stage >= 4)
                        {
                            _Stageup = true;
                            break;
                        }
                    }
                }
                if (_Stageup == true)
                {
                    SystemInformationMessage("Can't update the job, because job is work in progress!", "Job Update");
                    return;
                }
            }

            Service_JOB_HDR _jobHeader = new Service_JOB_HDR();
            MasterAutoNumber _jobAuto = new MasterAutoNumber();
            RecieptHeader _recHeader = null;
            MasterAutoNumber _recAuto = null;

            #region Job Header
            _jobHeader.SJB_RECALL = _jobRecall;
            _jobHeader.SJB_JOBNO = txtReqNo.Text;
            _jobHeader.SJB_DT = Convert.ToDateTime(dtDate.Value).Date;
            _jobHeader.SJB_COM = BaseCls.GlbUserComCode;
            _jobHeader.SJB_JOBCAT = txtTaskLoc.Text;

            _jobHeader.SJB_MANUALREF = txtManRef.Text;
            _jobHeader.SJB_ORDERNO = txtOrdRef.Text;
            _jobHeader.SJB_POD_NO = txtPOD.Text;
            _jobHeader.SJB_REC_LOC = txtLoc.Text;

            if (_jobRecall == 0)
            {
                _jobHeader.SJB_JOBTP = (ChkExternal.Checked == true) ? "E" : "I";

                if (optReq.Checked == false)
                { _jobHeader.SJB_JOBSTP = "SERDESK"; }
                else
                { _jobHeader.SJB_JOBSTP = "RCC"; }

                //kapila 2/2/2016
                if (_srb_jobstp == "MTAGRMNT")
                    _jobHeader.SJB_JOBSTP = "MTAGRMNT";

                if (optInspection.Checked == true)
                { _jobHeader.SJB_JOBSTP = "INSPECTION"; }
                _jobHeader.SJB_REQNO = txtReqNo.Text;
                _jobHeader.SJB_JOBSTAGE = 2;

            }
            else
            {
                _jobHeader.SJB_JOBTP = _scvjobHdr.SJB_JOBTP;
                _jobHeader.SJB_JOBSTP = _scvjobHdr.SJB_JOBSTP;
                _jobHeader.SJB_JOBCAT = _scvjobHdr.SJB_JOBCAT;
                _jobHeader.SJB_REQNO = _scvjobHdr.SJB_REQNO;
                _jobHeader.SJB_JOBSTAGE = 2;


                _jobHeader.SJB_CUST_CD = _scvjobHdr.SJB_CUST_CD;
                _jobHeader.SJB_CUST_TIT = _scvjobHdr.SJB_CUST_TIT;
                _jobHeader.SJB_CUST_NAME = _scvjobHdr.SJB_CUST_NAME;
                _jobHeader.SJB_NIC = _scvjobHdr.SJB_NIC;
                //_jobHeader.SJB_DL =
                //_jobHeader.SJB_PP =
                _jobHeader.SJB_MOBINO = _scvjobHdr.SJB_MOBINO;
                _jobHeader.SJB_ADD1 = _scvjobHdr.SJB_ADD1;
                _jobHeader.SJB_ADD2 = _scvjobHdr.SJB_ADD2;
                _jobHeader.SJB_ADD3 = _scvjobHdr.SJB_ADD3;
                _jobHeader.SJB_TOWN = _scvjobHdr.SJB_TOWN;

                //_jobHeader.SJB_PHNO =
                //_jobHeader.SJB_FAXNO =
                //_jobHeader.SJB_EMAIL =
            }
            //_jobHeader.SJB_MANUALREF =
            //_jobHeader.SJB_OTHERREF =

            if (_scvEmpList != null && _scvEmpList.Count > 0) _jobHeader.SJB_JOBSTAGE = 3;
            if (optInspection.Checked == true)
            {
                if (string.IsNullOrEmpty(txtReqNo.Text))
                {
                    _jobHeader.SJB_JOBSTAGE = Convert.ToDecimal(1.1);
                    //if (_scvParam.SP_ISINSPECTION == 1)
                    //{
                    //    if (string.IsNullOrEmpty(txtVisitChgCode.Text))
                    //    {
                    //        SystemInformationMessage("Please select the visit charge.", "Inspection Job");
                    //        txtVisitChgCode.Focus();
                    //        return;
                    //    }
                    //}
                }
            }
            _jobHeader.SJB_RMK = txtReqRmk.Text;
            _jobHeader.SJB_PRORITY = txtPriority.Text;
            //_jobHeader.SJB_ST_DT =
            //_jobHeader.SJB_ED_DT =
            //_jobHeader.SJB_NOOFPRINT =
            //_jobHeader.SJB_LASTPRINTBY =
            //_jobHeader.SJB_ORDERNO =
            _jobHeader.SJB_CUSTEXPTDT = dtExpectOn.Value.Date;
            //_jobHeader.SJB_SUBSTAGE =
            if (_jobRecall == 0)
            {
                if (ChkExternal.Checked == false)
                {
                    _jobHeader.SJB_CUST_CD = _scvjobHdr.SJB_CUST_CD;
                    _jobHeader.SJB_CUST_TIT = _scvjobHdr.SJB_CUST_TIT;
                    _jobHeader.SJB_CUST_NAME = _scvjobHdr.SJB_CUST_NAME;
                    _jobHeader.SJB_NIC = _scvjobHdr.SJB_NIC;
                    //_jobHeader.SJB_DL =
                    //_jobHeader.SJB_PP =
                    _jobHeader.SJB_MOBINO = _scvjobHdr.SJB_MOBINO;
                    _jobHeader.SJB_ADD1 = _scvjobHdr.SJB_ADD1;
                    _jobHeader.SJB_ADD2 = _scvjobHdr.SJB_ADD2;
                    //_jobHeader.SJB_ADD3 =
                    _jobHeader.SJB_TOWN = _scvjobHdr.SJB_TOWN;

                    //_jobHeader.SJB_PHNO =
                    //_jobHeader.SJB_FAXNO =
                    _jobHeader.SJB_EMAIL = txtEmail.Text;
                }
                else
                {
                    _jobHeader.SJB_CUST_CD = txtCustCode.Text;
                    _jobHeader.SJB_CUST_TIT = cmbTitle.Text;
                    _jobHeader.SJB_CUST_NAME = txtCusName.Text;
                    _jobHeader.SJB_NIC = txtNIC.Text;
                    //_jobHeader.SJB_DL =
                    //_jobHeader.SJB_PP =
                    _jobHeader.SJB_MOBINO = txtMobile.Text;
                    _jobHeader.SJB_ADD1 = txtAddress1.Text;
                    _jobHeader.SJB_ADD2 = txtAddress2.Text;
                    //_jobHeader.SJB_ADD3 =
                    _jobHeader.SJB_TOWN = txtTown.Text;

                    //_jobHeader.SJB_PHNO =
                    //_jobHeader.SJB_FAXNO =
                    _jobHeader.SJB_EMAIL = txtEmail.Text;   //kapila 30/6/2015
                }
            }

            _jobHeader.SJB_CNT_PERSON = txtContPersn.Text;
            _jobHeader.SJB_CNT_ADD1 = txtContLoc.Text;
            //_jobHeader.SJB_CNT_ADD2 =
            _jobHeader.SJB_CNT_PHNO = txtContNo.Text;
            _jobHeader.SJB_JOB_RMK = txtJobRem.Text;
            _jobHeader.SJB_TECH_RMK = txtTechIns.Text;

            _jobHeader.SJB_B_CUST_CD = txtCustCode.Text;
            _jobHeader.SJB_B_CUST_TIT = cmbTitle.Text;
            _jobHeader.SJB_B_CUST_NAME = txtCusName.Text;
            _jobHeader.SJB_B_NIC = txtNIC.Text;
            //_jobHeader.SJB_B_DL =
            //_jobHeader.SJB_B_PP =
            _jobHeader.SJB_B_MOBINO = txtMobile.Text;
            _jobHeader.SJB_B_ADD1 = txtAddress1.Text;
            _jobHeader.SJB_B_ADD2 = txtAddress2.Text;
            _jobHeader.SJB_B_ADD3 = txtTown.Text.ToString();
            if (txtTown.Tag != null) _jobHeader.SJB_B_TOWN = txtTown.Tag.ToString();
            //_jobHeader.SJB_B_PHNO =
            //_jobHeader.SJB_B_FAX =
            _jobHeader.SJB_B_EMAIL = txtEmail.Text; //kapila 30/6/2015

            if (string.IsNullOrEmpty(_jobHeader.SJB_CUST_CD) && string.IsNullOrEmpty(_scvjobHdr.SJB_CUST_NAME))
            {
                if (_jobRecall == 0)
                {
                    _jobHeader.SJB_CUST_CD = _scvjobHdr.SJB_B_CUST_CD;
                    _jobHeader.SJB_CUST_TIT = _scvjobHdr.SJB_B_CUST_TIT;
                    _jobHeader.SJB_CUST_NAME = _scvjobHdr.SJB_B_CUST_NAME;
                    _jobHeader.SJB_NIC = _scvjobHdr.SJB_B_NIC;
                    //_jobHeader.SJB_DL =
                    //_jobHeader.SJB_PP =
                    _jobHeader.SJB_MOBINO = _scvjobHdr.SJB_B_MOBINO;
                    _jobHeader.SJB_ADD1 = _scvjobHdr.SJB_B_ADD1;
                    _jobHeader.SJB_ADD2 = _scvjobHdr.SJB_B_ADD2;
                    _jobHeader.SJB_ADD3 = _scvjobHdr.SJB_B_ADD3;
                    _jobHeader.SJB_TOWN = _scvjobHdr.SJB_B_TOWN;

                    //_jobHeader.SJB_PHNO =
                    //_jobHeader.SJB_FAXNO =
                    //_jobHeader.SJB_EMAIL =
                }
            }

            _jobHeader.SJB_INFM_PERSON = txtInfoPersn.Text;
            _jobHeader.SJB_INFM_ADD1 = txtInfoLoc.Text;
            //_jobHeader.SJB_INFM_ADD2 =
            _jobHeader.SJB_INFM_PHNO = txtInfoNo.Text;
            _jobHeader.SJB_STUS = "P";
            _jobHeader.SJB_CRE_BY = BaseCls.GlbUserID;
            //_jobHeader.SJB_CRE_DT =
            _jobHeader.SJB_MOD_BY = BaseCls.GlbUserID;
            //_jobHeader.SJB_MOD_DT =
            _jobHeader.SJB_SEQ_NO = _jobRecallSeq;
            _jobHeader.SJB_SESSION_ID = BaseCls.GlbUserSessionID;

            if (!string.IsNullOrEmpty(txtVisitChgCode.Text))
            {
                _jobHeader.SJB_CHG_CD = txtVisitChgCode.Text;
                if (!string.IsNullOrEmpty(lblCharge.Text.ToString()))
                {
                    _jobHeader.SJB_CHG = Convert.ToDecimal(lblCharge.Text.ToString());
                }
            }
            else
            {
                _jobHeader.SJB_CHG = 0;
            }

            //Tharaka 2015-01-28
            if (optReq.Checked) _jobHeader.JobCategori = 1;
            else if (optInspection.Checked) _jobHeader.JobCategori = 2;
            else if (optJob.Checked) _jobHeader.JobCategori = 3;
            #endregion

            #region Job Auto Number
            //kapila 15/3/2016
            if (BaseCls.GlbUserDefProf == "RIT1B")
                _jobAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            else
                _jobAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;

            _jobAuto.Aut_cate_tp = "LOC";
            _jobAuto.Aut_moduleid = "SVJOB";
            _jobAuto.Aut_direction = 0;
            _jobAuto.Aut_year = _jobHeader.SJB_DT.Year;
            #endregion

            #region Receipt Header
            if (ucPayModes1.RecieptItemList != null)
            {
                if (ucPayModes1.RecieptItemList.Count > 0)
                {
                    _recHeader = new RecieptHeader();
                    _recHeader.Sar_acc_no = "Job_No";
                    _recHeader.Sar_act = true;
                    _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                    _recHeader.Sar_comm_amt = 0;
                    _recHeader.Sar_create_by = BaseCls.GlbUserID;
                    _recHeader.Sar_create_when = DateTime.Now;
                    _recHeader.Sar_currency_cd = "LKR";
                    _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
                    _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
                    _recHeader.Sar_debtor_cd = txtCustCode.Text;
                    _recHeader.Sar_debtor_name = txtCusName.Text;
                    _recHeader.Sar_direct = true;
                    _recHeader.Sar_direct_deposit_bank_cd = "";
                    _recHeader.Sar_direct_deposit_branch = "";
                    _recHeader.Sar_epf_rate = 0;
                    _recHeader.Sar_esd_rate = 0;
                    _recHeader.Sar_is_mgr_iss = false;
                    _recHeader.Sar_is_oth_shop = false;
                    _recHeader.Sar_is_used = false;
                    _recHeader.Sar_manual_ref_no = txtManRef.Text;
                    _recHeader.Sar_mob_no = txtMobile.Text;
                    _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _recHeader.Sar_mod_when = DateTime.Now;
                    _recHeader.Sar_nic_no = txtNIC.Text;
                    _recHeader.Sar_oth_sr = "SERVICE";
                    _recHeader.Sar_prefix = "ADVAN";
                    _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                    _recHeader.Sar_receipt_date = Convert.ToDateTime(dtDate.Value);
                    //_recHeader.Sar_receipt_no = "na";
                    //_recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                    _recHeader.Sar_receipt_type = "ADVAN";
                    _recHeader.Sar_ref_doc = txtReqNo.Text;
                    _recHeader.Sar_remarks = txtJobRem.Text;
                    //_recHeader.Sar_seq_no = 1;
                    _recHeader.Sar_ser_job_no = "";
                    _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                    _recHeader.Sar_tel_no = txtMobile.Text;
                    _recHeader.Sar_tot_settle_amt = 0;
                    _recHeader.Sar_uploaded_to_finance = false;
                    _recHeader.Sar_used_amt = 0;
                    _recHeader.Sar_wht_rate = 0;


                    _recAuto = new MasterAutoNumber();
                    _recAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _recAuto.Aut_cate_tp = "PRO";
                    _recAuto.Aut_direction = 1;
                    _recAuto.Aut_modify_dt = null;
                    _recAuto.Aut_moduleid = "RECEIPT";
                    _recAuto.Aut_number = 0;
                    _recAuto.Aut_start_char = "SADV";
                    _recAuto.Aut_year = Convert.ToDateTime(dtDate.Text).Year;

                }
            }
            #endregion

            string jobNo;
            string receiptNo = string.Empty;
            string _msg = "";

            fillSubItems();

            if (optReq.Checked && _jobHeader.SJB_JOBSTP == "SERDESK")
            {
                MessageBox.Show("Invalid job type", "Job Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnSave.Enabled = false;
            if (oImgList.Count == 0)
                oImgList = new List<ImageUploadDTO>();
            int eff = CHNLSVC.CustService.Save_Job(_jobHeader, _scvItemList, _scvDefList, _scvEmpList, _scvItemSubList, _recHeader, ucPayModes1.RecieptItemList, oImgList, _recAuto, BaseCls.GlbDefSubChannel, txtItmTp.Text, _itmBrand, _warStus, _jobAuto, out _msg, out jobNo, out receiptNo, AlocatedSupervicerList, _scvParam.SP_AUTO_START_JOB);
            if (eff == 1)
            {
                string outMsg;
                Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(_jobHeader.SJB_COM, BaseCls.GlbDefSubChannel, 1);
                Service_Message oMessage = new Service_Message();
                if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                {
                    MasterItem oItem = CHNLSVC.Inventory.GetItem("", _scvItemList[0].Jbd_itm_cd);
                    string emailBody = oTemplate.Sml_templ_mail;
                    emailBody = emailBody.Replace("[B_Cust]", _jobHeader.SJB_B_CUST_NAME)
                                         .Replace("[Cust]", _jobHeader.SJB_CUST_NAME)
                                         .Replace("[jobNo]", jobNo)
                                         .Replace("[defect],[defect]", _jobHeader.SJB_B_CUST_NAME)
                                         .Replace("[date]", DateTime.Now.ToString("dd/MMM/yyyy  hh:mm tt"))
                                         .Replace("[tech]", " ")
                                         .Replace("[ItmModel]", oItem.Mi_model)
                                         .Replace("[ItmDescr]", oItem.Mi_longdesc)
                                         .Replace("[ItmSerial]", _scvItemList[0].Jbd_ser1)
                                         .Replace("[ItmCate]", oItem.Mi_cate_1);

                    String SmsBody = oTemplate.Sml_templ_sms;

                    SmsBody = SmsBody.Replace("[B_Cust]", _jobHeader.SJB_B_CUST_NAME)
                                     .Replace("[jobNo]", jobNo)
                                     .Replace("[ItmSerial]", _scvItemList[0].Jbd_ser1);

                    oMessage.Sm_com = _jobHeader.SJB_COM;
                    oMessage.Sm_jobno = jobNo;
                    oMessage.Sm_joboline = 1;
                    oMessage.Sm_jobstage = 2;
                    oMessage.Sm_ref_num = string.Empty;
                    oMessage.Sm_status = 0;
                    oMessage.Sm_msg_tmlt_id = 1;
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

                    //kapila 2/7/2016 send email to showroom if job is RCC
                    string _mail = "";
                    if (_jobHeader.SJB_JOBSTP == "RCC")
                    {
                        RCC _objRcc = CHNLSVC.Inventory.GetRccByNo(_jobHeader.SJB_REQNO);
                        if (_objRcc != null)
                        {
                            //   MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                            // _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(_objRcc.Inr_loc_cd, null, null, null, null, null);
                            //kapila 5/7/2017
                            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(_objRcc.Inr_com_cd, _objRcc.Inr_loc_cd);
                            if (_mstLoc != null)
                            {
                                if (_mstLoc.Ml_email != null)   //kapila 6/7/2016
                                    if (IsValidEmail(_mstLoc.Ml_email))
                                    {
                                        _mail += "Job is opened for the following RCC request" + Environment.NewLine + Environment.NewLine;
                                        _mail += "RCC No - " + _jobHeader.SJB_REQNO + "" + Environment.NewLine;
                                        _mail += "Open Date - " + _jobHeader.SJB_DT.ToShortDateString() + "" + Environment.NewLine;
                                        _mail += "Job # - " + _jobHeader.SJB_JOBNO + "" + Environment.NewLine + Environment.NewLine;

                                        _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;
                                        CHNLSVC.CommonSearch.Send_SMTPMail(_mstLoc.Ml_email, "RCC Job Open", _mail);
                                    }
                                // else
                                //  MessageBox.Show("Showroom mail send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        //oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(_jobHeader.SJB_COM, BaseCls.GlbDefSubChannel, 8);
                        //oMessage = new Service_Message();
                        //if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                        //{
                        //    oItem = CHNLSVC.Inventory.GetItem("", _scvItemList[0].Jbd_itm_cd);
                        //    emailBody = oTemplate.Sml_templ_mail;
                        //    emailBody = emailBody.Replace("[ReqNo]", _jobHeader.SJB_REQNO)
                        //                         .Replace("[jobNo]", jobNo)
                        //                         .Replace("[date]", DateTime.Now.ToString("dd/MMM/yyyy  hh:mm tt"));


                        //    oMessage.Sm_com = _jobHeader.SJB_COM;
                        //    oMessage.Sm_jobno = jobNo;
                        //    oMessage.Sm_joboline = 1;
                        //    oMessage.Sm_jobstage = 2;
                        //    oMessage.Sm_ref_num = string.Empty;
                        //    oMessage.Sm_status = 0;
                        //    oMessage.Sm_msg_tmlt_id = 8;
                        //    oMessage.Sm_sms_text = "";
                        //    oMessage.Sm_sms_gap = 0;
                        //    oMessage.Sm_sms_done = 0;
                        //    oMessage.Sm_mail_text = emailBody;
                        //    oMessage.Sm_mail_gap = 0;
                        //    oMessage.Sm_email_done = 0;
                        //    oMessage.Sm_cre_by = BaseCls.GlbUserID;
                        //    oMessage.Sm_cre_dt = DateTime.Now;
                        //    oMessage.Sm_mod_by = BaseCls.GlbUserID;
                        //    oMessage.Sm_mod_dt = DateTime.Now;
                        //    Int32 R2 = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                        //    if (R2 < 1)
                        //    {
                        //        MessageBox.Show("Showroom mail send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}
                    }

                }
                if (_jobHeader.SJB_JOBSTAGE == 3)
                {
                    oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(_jobHeader.SJB_COM, BaseCls.GlbDefSubChannel, 2);
                    if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                    {

                        List<MST_BUSPRIT_LVL> oItems;
                        string custLevel = string.Empty;

                        oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(_jobHeader.SJB_B_CUST_CD, BaseCls.GlbUserComCode);
                        if (oItems == null || oItems.Count == 0)
                        {
                            oItems = CHNLSVC.CustService.GetCustomerPriorityLevel("CASH.", BaseCls.GlbUserComCode);
                        }

                        if (oItems != null && oItems.Count > 0)
                        {
                            MST_BUSPRIT_LVL oSelectedLvl = new MST_BUSPRIT_LVL();
                            String PartyCode = String.Empty;
                            String PartyType = String.Empty;
                            if (oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca).Count > 0)
                            {
                                List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca);
                                oSelectedLvl = ot1[0];
                                PartyCode = BaseCls.GlbUserDefLoca;
                                PartyType = "LOC";
                                custLevel = ot1[0].SCP_DESC;
                            }
                            else if (oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel).Count > 0)
                            {
                                List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel);

                                if (ot1.Count > 0)
                                {
                                    oSelectedLvl = ot1[0];
                                    PartyCode = BaseCls.GlbDefSubChannel;
                                    PartyType = "SCHNL";
                                    custLevel = ot1[0].SCP_DESC;
                                }
                            }
                        }
                        string emailBody = oTemplate.Sml_templ_mail;
                        emailBody = emailBody.Replace("[B_Cust]", _jobHeader.SJB_B_CUST_NAME)
                                             .Replace("[jobNo]", jobNo)
                                             .Replace("[CustLevel]", custLevel);

                        String SmsBody = oTemplate.Sml_templ_sms;

                        SmsBody = SmsBody.Replace("[jobNo]", jobNo);

                        oMessage = new Service_Message();
                        oMessage.Sm_com = _jobHeader.SJB_COM;
                        oMessage.Sm_jobno = jobNo;
                        oMessage.Sm_joboline = 1;
                        oMessage.Sm_jobstage = _jobHeader.SJB_JOBSTAGE;
                        oMessage.Sm_ref_num = string.Empty;
                        oMessage.Sm_status = 0;
                        oMessage.Sm_msg_tmlt_id = 2;
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

                        Int32 R2 = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                        if (R2 < 1)
                        {
                            MessageBox.Show("Technician message send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                SystemInformationMessage(_msg, "Job Entry");

                //Commented by akila 2017/04/39 - As per the request of asanka(No need to view print slip only for auto mobile. (need only recall & get the reprint))
                if ((!string.IsNullOrEmpty(receiptNo)) && (_scvParam.SP_ISAUTOMOBI != 1))
                {
                    ReportViewer _view = new ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "REC";
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = receiptNo;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;
                }

                DataTable JobCat = new DataTable();
                JobCat = CHNLSVC.CustService.sp_get_job_category(txtTaskLoc.Text);
                if (JobCat.Rows.Count > 0)
                {
                    foreach (DataRow drow in JobCat.Rows)
                    {
                        BaseCls.GlbReportTp = drow["sc_direct"].ToString();
                        if (BaseCls.GlbReportTp == "W")
                        {
                            BaseCls.GlbReportTp = "JOBW";
                        }
                        else
                        {
                            BaseCls.GlbReportTp = "JOBF";
                        }
                    }
                }
                //Commented by akila 2017/04/39 - As per the request of asanka(No need to view print slip. (need only recall & get the reprint))
                BaseCls.GlbReportTp = txtTaskLoc.Text;
                Reports.Service.ReportViewerSVC _viewJob = new Reports.Service.ReportViewerSVC();
                BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                BaseCls.GlbReportDoc = jobNo;

                Clear();

                JobEntry _JobEntry = new JobEntry();
                _JobEntry.MdiParent = this.MdiParent;
                _JobEntry.Location = this.Location;
                _JobEntry.GlbModuleName = this.GlbModuleName;
                _JobEntry.Show();
                this.Close();
                this.Dispose();
                GC.Collect();

                Reports.Service.clsServiceRep objSvc = new Reports.Service.clsServiceRep();

                if (BaseCls.GlbDefSubChannel == "MCS")
                {
                    if (chk_Direct_print.Checked == true)
                    {
                        BaseCls.GlbReportDirectPrint = 1;
                    }
                    else
                    {
                        BaseCls.GlbReportDirectPrint = 0;
                    }
                }
                //else if (BaseCls.GlbDefSubChannel == "MBSV") //Tharanga 2017/06/13
                //{ if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
                //else if (BaseCls.GlbDefSubChannel == "AM") //Tharanga 2017/06/13
                //{ if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
                //
                else
                {
                    BaseCls.GlbReportDirectPrint = 0;
                }

                if (BaseCls.GlbReportDirectPrint == 1)
                {
                    string _repname = string.Empty;
                    string _papersize = string.Empty;
                    CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                    if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                    if (BaseCls.GlbReportName == null || BaseCls.GlbReportName == "")
                    {
                        MessageBox.Show("Report is not setup. Contact IT Department...\n", "Report not Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //else if (BaseCls.GlbReportName == "ServiceJobCardAut.rpt") // Tharanga 2017/06/13
                    //{
                    //    objSvc.ServiceJobCardAuto();
                    //    Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                    //    objSvc._serJobAuto.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                    //    MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    objSvc._serJobAuto.PrintToPrinter(1, false, 0, 0);
                    //}
                    //else if (BaseCls.GlbReportName == "JobCard_Power_tools.rpt") // Tharanga 2017/06/13
                    //{
                    //    objSvc.ServiceJobCardPowerToolPrint();
                    //    Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                    //    objSvc._FeildJobCard.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                    //    MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    objSvc._JobCard_Power_tools.PrintToPrinter(1, false, 0, 0);
                    //}

                    //else if (BaseCls.GlbReportName == "FeildJobCard.rpt") // Tharanga 2017/06/13
                    //{
                    //    objSvc.ServiceFeildJobCard();
                    //    Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                    //    objSvc._FeildJobCard.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                    //    MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    objSvc._FeildJobCard.PrintToPrinter(1, false, 0, 0);
                    //}

                    else
                    {
                        objSvc.ServiceJobCardPrint();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        objSvc._JobCardWPh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSvc._JobCardWPh.PrintToPrinter(1, false, 0, 0);
                    }
                }
                else
                {
                    if (_scvParam.SP_ISAUTOMOBI != 1)//Commented by akila 2017/04/39 - As per the request of asanka(No need to view print slip. (need only recall & get the reprint))
                    {
                        _viewJob.Show();
                        _viewJob = null;
                    }
                    else if ((_scvParam.SP_ISAUTOMOBI == 1) && (_scvParam.SP_IS_DIRECT_PRINT == 0))
                    {
                        _viewJob.Show();
                        _viewJob = null;
                    }
                }
            }
            else
            {
                MessageBox.Show(_msg, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (optJob.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtReqNo.Text))
                {
                    if (_scvItemList != null && _scvItemList.Count > 0)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10803))
                        {
                            MessageBox.Show("You don't have the permission for job cancel function.\nPermission Code :- 10803", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {
                            //Cancel code
                            //Int32 _resultCNCL = -1;
                            //String _msg = string.Empty;
                            //_resultCNCL = CHNLSVC.CustService.ServiceApprove(txtReqNo.Text, 0, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, "Cancel", txtJobRem.Text, (Int32)CommonEnum.Job_Cancel, out _msg, "", "");
                            //if (_resultCNCL == -1)
                            //{
                            //    SystemWarnningMessage(_msg, "Job Cancellation");
                            //    return;
                            //}

                            if (MessageBox.Show("Do you want to cancel this job?", "Job Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                JobCancel();
                            }
                        }
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear screen ?", "Job Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            //Clear();
            //lblSerSrch.Text = _scvParam.SP_DB_SERIAL;
            //lblRegNo.Text = _scvParam.SP_DB_CHASSIS;
            //txtCustCode.Text = _scvParam.SP_EXTERNALCUST;
            //if (_scvParam.SP_ISAUTOMOBI == 1)
            //{
            //    pnlMilage.Visible = true;
            //    pnlItmTp.Visible = false;
            //}
            //else
            //{
            //    pnlMilage.Visible = false;
            //    pnlItmTp.Visible = true;
            //}
            //ChkExternal.Enabled = true;
            //txtTaskLoc.Text = _scvParam.SP_DEF_CAT;
            JobEntry _JobEntry = new JobEntry();
            _JobEntry.MdiParent = this.MdiParent;
            _JobEntry.Location = this.Location;
            _JobEntry.GlbModuleName = this.GlbModuleName;
            _JobEntry.Show();
            this.Close();
            this.Dispose();
            GC.Collect();
        }

        private void btnPopupVehClose_Click(object sender, EventArgs e)
        {
            pnlAdditionalItems.Visible = false;
        }

        private void btnAddAdditional_Click(object sender, EventArgs e)
        {
            if (grvAddiItems.Rows.Count <= 0)
            {
                SystemInformationMessage("First select additional item!", "Job Additional Item");
                return;
            }
            else
            {
                if (grvAddiItems.Rows.Count == 1)
                {
                    for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                    {
                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
                    }
                    btn_add_ser_Click(null, null);
                    _scvItemSubList.Add(_tempItemSubList[0]);
                }
                else
                {
                    bool _isSelect = false;
                    for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value) == true)
                        {
                            _isSelect = true;
                        }
                    }

                    if (_isSelect == false)
                    {
                        SystemInformationMessage("First select additional item!", "Job Additional Item");
                        return;
                    }

                    _isSelect = false;
                    //for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                    //{
                    //    if (Convert.ToBoolean(grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value) == true)
                    //    {
                    //        if (_isSelect == false)
                    //        {
                    //            btn_add_ser_Click(null, null);
                    //            _isSelect = true;
                    //        }
                    //        _serJobItemSubList.Add(_tempsubList[i]);
                    //    }
                    //}
                    int _subLine = 1;
                    foreach (Service_Job_Det_Sub _lst in _tempItemSubList)
                    {
                        if (_lst.JBDS_SELECT == true)
                        {
                            if (_isSelect == false)
                            {
                                btn_add_ser_Click(null, null);
                                //if (_jobRecall == 1)
                                //{
                                //    return;
                                //}
                                _isSelect = true;
                            }
                            _lst.JBDS_WARR = lblWarNo.Text;
                            //_lst.JBDS_JOBLINE = _jobItemLine - 1;
                            _lst.JBDS_LINE = _scvItemSubList.Count + 1;
                            if (grvJobItms.RowCount <= 0)
                            { _lst.JBDS_JOBLINE = 0; }
                            else
                            { _lst.JBDS_JOBLINE = Convert.ToInt32(grvJobItms.SelectedRows[0].Cells["jbd_jobline"].Value.ToString()); }
                            _lst.JBDS_LINE = _subLine;

                            if (_scvItemSubList.FindAll(x => x.JBDS_SER1 == _lst.JBDS_SER1).Count > 0)
                            {
                                continue;
                            }

                            _scvItemSubList.Add(_lst);
                            _subLine++;
                        }
                    }
                    pnlAdditionalItems.Visible = false;
                }
            }
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtMobile;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtMobile.Select();
                if (_commonSearch.GlbSelectData == null) return;
                string[] args = _commonSearch.GlbSelectData.Split('|');
                string _cuscode = args[4];
                if (string.IsNullOrEmpty(txtCustCode.Text) || txtCustCode.Text.Trim() == "CASH") txtCustCode.Text = _cuscode;
                else if (txtCustCode.Text.Trim() != _cuscode && txtCustCode.Text.Trim() != "CASH")
                {
                    DialogResult _res = MessageBox.Show("Currently selected customer code " + txtCustCode.Text + " is differ which selected (" + _cuscode + ") from here. Do you need to change current customer code from selected customer", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_res == System.Windows.Forms.DialogResult.Yes)
                    {
                        txtCustCode.Text = _cuscode;
                        txtCustCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtNIC;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtNIC.Select();
            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCode;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustCode.Select();

                txtCustCode_Leave(null, null);
            }
            catch (Exception ex)
            { txtCustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCustCode.Clear();
            txtCusName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            txtTown.Clear();
            txtCusName.Enabled = true;
            txtAddress1.Enabled = true;
            txtAddress2.Enabled = true;
            txtMobile.Enabled = true;
            txtNIC.Enabled = true;
            txtTown.Enabled = true;
            txtEmail.Clear();

            label85.Text = ":: Customer ::";
            label85.BackColor = System.Drawing.Color.MidnightBlue;
            label85.ForeColor = System.Drawing.Color.White;
            //chkTaxPayable.Checked = false;
            //txtLoyalty.Clear();
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            cmbTitle.Text = _masterBusinessCompany.MBE_TIT;
            txtEmail.Text = _masterBusinessCompany.Mbe_email;
            //ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            //ucPayModes1.Mobile = txtMobile.Text.Trim();

            if (_isRecall == false)
            {
                //txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                //txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                //txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                //txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAddress1.Text = _hdr.Sah_cus_add1;
                txtAddress2.Text = _hdr.Sah_cus_add2;

                //txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                //txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                //txtDelCustomer.Text = _hdr.Sah_d_cust_cd;
                //txtDelName.Text = _hdr.Sah_d_cust_name;
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
        }

        private void GetNICGender()
        {
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = (nicarray[2]).ToString();
            if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
            { cmbTitle.Text = "MS."; }
            else
            { cmbTitle.Text = "MR."; }
        }

        private void EnableDisableCustomer()
        {
            if (txtCustCode.Text == "CASH")
            {
                txtCustCode.Enabled = true;
                txtCusName.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;
                txtEmail.Enabled = true;

                btnSearch_NIC.Enabled = true;
                btnSearch_CustCode.Enabled = true;
                btnSearch_Mobile.Enabled = true;
            }
            else
            {
                //txtCustomer.Enabled = false;
                txtCusName.Enabled = false;
                txtAddress1.Enabled = false;
                txtAddress2.Enabled = false;
                txtMobile.Enabled = false;
                txtNIC.Enabled = false;
                txtEmail.Enabled = false;

                //btnSearch_NIC.Enabled = false;
                //btnSearch_Customer.Enabled = false;
                //btnSearch_Mobile.Enabled = false;
            }
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustCode.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH.";
                        EnableDisableCustomer();
                    }
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCode.Text, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("This customer already inactive. Please contact Accounts department.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                        //ClearCustomer(false);
                    }
                    else
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(true);
                    Cursor = DefaultCursor;
                    // txtCustCode.Focus();
                    return;
                }
                EnableDisableCustomer();

                check_Blacklistcustomer();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void check_Blacklistcustomer()
        {
            DataTable _blacklist = CHNLSVC.Sales.CheckBlackListCustomer(txtCustCode.Text);

            if (_blacklist.Rows.Count > 0)
            {
                foreach (DataRow row in _blacklist.Rows)
                {
                    label85.Text = ":: Black listed Customer ::";
                    label85.BackColor = System.Drawing.Color.Yellow;
                    label85.ForeColor = System.Drawing.Color.Red;
                    SystemInformationMessage("Black Listed Customer. Customer Code: " + row["hbl_cust_cd"].ToString() + " - [" + row["hbl_rmk"].ToString() + "]", "Black List Customer");
                }
            }
            else
            {
                label85.Text = ":: Customer ::";
                label85.BackColor = System.Drawing.Color.MidnightBlue;
                label85.ForeColor = System.Drawing.Color.White;
            }
        }

        protected void LoadCustomerDetailsByNIC()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH.";
                        EnableDisableCustomer();
                    }
                    return;
                }

                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNIC.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of active customers are available for the selected NIC.\nPlease contact Accounts Dept.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNIC.Clear(); txtNIC.Focus(); return;
                        }
                    }

                    //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, BaseCls.GlbUserComCode);
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
                        GetNICGender();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, txtNIC.Text.Trim().ToUpper(), null, null, null, null);
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        GetNICGender();
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }
                }

                EnableDisableCustomer();
                txtMobile.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        protected void LoadCustomerDetailsByMobile()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH.";
                        EnableDisableCustomer();
                    }
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMobile.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count > 1)
                        {
                            //Tempory removed by Chamal 26-04-2014
                            //if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _custList.Count + " number of customers are available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtMobile.Clear(); txtMobile.Focus(); return;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtCustCode.Text) && txtCustCode.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCode.Text.Trim(), string.Empty, txtMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                }
                //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                        //ViewCustomerAccountDetail(txtCustomer.Text);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustCode.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, txtMobile.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                    }
                    else
                    {
                        _isGroup = false;
                    }
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void SetCustomerAndDeliveryDetailsGroup(GroupBussinessEntity _cust)
        {
            txtCustCode.Text = _cust.Mbg_cd;
            txtCusName.Text = _cust.Mbg_name;
            txtAddress1.Text = _cust.Mbg_add1;
            txtAddress2.Text = _cust.Mbg_add2;
            txtMobile.Text = _cust.Mbg_mob;
            txtNIC.Text = _cust.Mbg_nic;
            cmbTitle.Text = _cust.Mbg_tit;
            txtEmail.Text = _cust.Mbg_email;
        }

        private void btnSearch_CustCode_Click(object sender, EventArgs e)
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
                _commonSearch.obj_TragetTextBox = txtCustCode;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustCode.Select();
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 1;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtCustCode;
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtCustCode.Select();
            }
            catch (Exception ex) { txtCustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btn_srch_town_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                _CommonSearch.IsSearchEnter = true;
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTown;
                _CommonSearch.ShowDialog();
                txtTown.Select();
            }
            catch (Exception ex) { txtCustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtCustCode_Leave(object sender, EventArgs e)
        {
            if (txtCustCode.Text == "CASH")
            {
                SystemInformationMessage("Please enter a valid customer code.", "Job entry");
                txtCustCode.Text = "";
                txtCustCode.Focus();
                return;
            }

            LoadCustomerDetailsByCustomer();
            FillPriority();
            check_Blacklistcustomer();
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMobile.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
                else
                {
                    LoadCustomerDetailsByMobile();
                    check_Blacklistcustomer();
                }
            }
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    LoadCustomerDetailsByNIC();
                }
            }
        }

        private void txtWar_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWar.Text))
            {
                //kapila 27/3/2015
                if (_scvParam.sp_is_job_by_rcc == 1)   //must open with rcc if exists
                {
                    DataTable _dt = CHNLSVC.Inventory.getRCCbySerialWar(null, txtWar.Text);
                    if (_dt.Rows.Count > 0)
                    {
                        optReq.Checked = true;
                        txtReqNo.Text = _dt.Rows[0]["inr_no"].ToString();
                        txtReqNo_Leave(null, null);
                        return;
                    }
                }

                Load_Serial_Infor(txtWar, string.Empty, dtDate.Value.Date);
            }
        }

        private void txtReqNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReqNo.Text)) return;

            if (optReq.Checked == true)
            {
                //if (_scvItemList != null)
                //{
                //if (_scvItemList.Count > 0)
                //{
                //    if (MessageBox.Show("Job items are already exist. Do you want to reset it?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    {
                //        txtReqNo.Text = oldJobNumber;
                //        return;
                //    }
                //}

                //}
                ClearForm();
                LoadRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtReqNo.Text);
                optReq.Enabled = false;
                optInspection.Enabled = false;
                optJob.Enabled = false;
                pnlCustBill.Enabled = true;  //kapila 1/8/2015 set false to true
                btnNewCust.Enabled = false;
                chkEditCustomer.Enabled = false;
            }
            if (optJob.Checked == true)
            {
                //if (_scvItemList != null)
                //{
                //if (_scvItemList.Count > 0)
                //{
                //    if (MessageBox.Show("Job items are already exist. Do you want to reset it?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    {
                //        txtReqNo.Text = oldJobNumber;
                //        return;
                //    }
                //}
                //}
                ClearForm();
                LoadJob(BaseCls.GlbUserComCode, txtReqNo.Text, "0");
                optReq.Enabled = false;
                optInspection.Enabled = false;
                optJob.Enabled = false;
                pnlCustBill.Enabled = true; //kapila 11/8/2015
                btnNewCust.Enabled = false;
                chkEditCustomer.Enabled = false;
                btnPrint.Visible = true;
                btnViewPay.Enabled = true;
            }
            if (optInspection.Checked == true)
            {
                //if (_scvItemList != null)
                //{
                //    if (_scvItemList.Count > 0)
                //    {
                //        if (MessageBox.Show("Job items are already exist. Do you want to reset it?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                //    }
                //}
                LoadJob(BaseCls.GlbUserComCode, txtReqNo.Text, "1.1");
                optReq.Enabled = false;
                optInspection.Enabled = false;
                optJob.Enabled = false;
                txtEItem.Focus();
            }
            FillPriority();
        }

        private void ClearForm()
        {
            _jobItemLine = 1;
            clear_Ext_Job_Items();
            _scvjobHdr = new Service_JOB_HDR();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_job_Det>();

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Job_Defects>();

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();

            grvAddiItems.AutoGenerateColumns = false;
            grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();

            foreach (Control _obj in this.Controls)
            {
                if (_obj is TextBox)
                {
                    _obj.Text = string.Empty;
                }
                if (_obj is Label)
                {
                    if (_obj.Name.Contains("lbl"))
                    {
                        _obj.Text = string.Empty;
                    }
                }
            }
            dtDate.Value = DateTime.Now.Date;
        }

        private void optReq_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optReq.Checked == true)
                {
                    _chgJobStage = "2";
                    btnJobTasks.Enabled = false;
                    ClearCateChange();
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(err);
            }
        }

        private void optJob_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optJob.Checked == true)
                {
                    ClearCateChange();
                    _chgJobStage = "2";
                    ChkExternal.Enabled = true;
                    btnJobTasks.Enabled = true;
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(err);
            }
        }

        private void txtTown_Leave(object sender, EventArgs e)
        {
            //txtPerDistrict.Text = "";
            //txtPerProvince.Text = "";
            //txtPerPostal.Text = "";
            //txtPerCountry.Text = "";

            if (!string.IsNullOrEmpty(txtTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtTown.Tag = dt.Rows[0]["TOWN_ID"].ToString();
                        //txtPerDistrict.Text = district;
                        //txtPerProvince.Text = province;
                        //txtPerPostal.Text = postalCD;
                        //txtPerCountry.Text = countryCD;
                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTown.Text = "";
                        txtTown.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTown.Text = "";
                    txtTown.Focus();
                }
            }
        }

        private void LoadRequest(string _com, string _loc, string _reqNo)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            Dictionary<Service_Req_Hdr, List<Service_Req_Det>> _result = null;
            _reqHdr = new Service_Req_Hdr();

            string _ser = null;
            string _warr = null;
            string _regno = null;
            string _invcno = null;

            _result = CHNLSVC.CustService.GetScvRequest(_com, _loc, _reqNo, "", 0, out  _returnStatus, out  _returnMsg);
            if (_returnStatus == -1)
            {
                SystemInformationMessage(_returnMsg, "Service Request");
                txtReqNo.Clear();
                txtReqNo.Focus();
                return;
            }
            foreach (KeyValuePair<Service_Req_Hdr, List<Service_Req_Det>> pair in _result)
            {
                _reqHdr = pair.Key;
                _reqDet = pair.Value;
            }

            if (_reqDet.Count == 1)
            {
                foreach (Service_Req_Det _itm in _reqDet)
                {
                    if (_itm.Jrd_isexternalitm == 1)
                    {
                        Load_Serial_Infor(txtReqNo, _itm.Jrd_warr.ToString(), _reqHdr.Srb_dt.Date);
                        txtWar.Text = _itm.Jrd_warr.ToString();
                    }
                    else
                    {
                        FillItemDetails(FillReqItemDetails(_reqHdr, _itm), dtDate.Value.Date, _returnStatus);
                    }
                }
            }

            if (_reqDet.Count > 1)
            {
                grvReqItems.AutoGenerateColumns = false;
                grvReqItems.DataSource = new List<Service_Req_Det>();
                grvReqItems.DataSource = _reqDet;
                pnlReq.Visible = true;
            }

            txtOrigin.Text = "RCC";
            txtTaskLoc.Text = _reqHdr.Srb_jobcat;
            load_job_task_dir();    //kapila 25/1/2016
            txtPriority.Text = "NORMAL";
            txtReqRmk.Text = _reqHdr.Srb_rmk;
            txtJobRem.Text = _reqHdr.Srb_rmk;


            txtMobile.Text = _reqHdr.Srb_b_mobino;
            txtCustCode.Text = _reqHdr.Srb_b_cust_cd;
            txtCusName.Text = _reqHdr.Srb_b_cust_name;
            txtAddress1.Text = _reqHdr.Srb_b_add1;
            txtAddress2.Text = _reqHdr.Srb_b_add2;
            txtTown.Text = _reqHdr.Srb_b_town;
            txtEmail.Text = _reqHdr.Srb_b_email;
            txtInfoLoc.Text = _reqHdr.Srb_infm_add1;
            txtContPersn.Text = _reqHdr.Srb_cnt_person;
            txtContNo.Text = _reqHdr.Srb_cnt_phno;
            //txtContLoc.Text = _reqHdr.Srb_cnt_add1;
            //kapila 10/10/2015
            txtInfoPersn.Text = _reqHdr.Srb_infm_person;
            txtInfoNo.Text = _reqHdr.Srb_infm_phno;

            lblBuyerCustMobi.Text = _reqHdr.Srb_mobino;
            lblBuyerCustCode.Text = _reqHdr.Srb_cust_cd;
            lblBuyerCustName.Text = _reqHdr.Srb_cust_name;
            lblBuyerCustAdd1.Text = _reqHdr.Srb_add1;
            lblBuyerCustAdd2.Text = _reqHdr.Srb_add2;

            dtExpectOn.Value = _reqHdr.Srb_custexptdt;      //kapila 10/11/2015 (ac installation date)
            lblInsDate.Text = _reqHdr.Srb_custexptdt.ToShortDateString();
            //kapila 19/11/2015
            DataTable _dtReqDt = CHNLSVC.CustService.GetSCVReqData(txtReqNo.Text, BaseCls.GlbUserComCode);
            if (_dtReqDt.Rows.Count > 0)
            {
                lblInsExec.Text = _dtReqDt.Rows[0]["Jrd_conf_rmk"].ToString();
                lblInsCont.Text = _dtReqDt.Rows[0]["Jrd_conf_desc"].ToString();
                lblInsRem.Text = _reqHdr.Srb_rmk;
            }
            //kapila 2/2/2016
            _srb_jobstp = _reqHdr.Srb_jobstp;
        }

        private void LoadJob(string _com, string _jobNo, string _jobStage)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;

            _scvjobHdr = new Service_JOB_HDR();
            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            oImgList = new List<ImageUploadDTO>();

            _returnStatus = CHNLSVC.CustService.GetScvJob(_com, _jobNo, out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _scvEmpList, out  _scvStdbyList, out  _returnMsg);
            if (_returnStatus != 1)
            {
                SystemInformationMessage(_returnMsg, "Service Job");
                txtReqNo.Clear();
                //txtReqNo.Focus();
                JobEntry _JobEntry = new JobEntry();
                _JobEntry.MdiParent = this.MdiParent;
                _JobEntry.Location = this.Location;
                _JobEntry.GlbModuleName = this.GlbModuleName;
                _JobEntry.Show();
                this.Close();
                this.Dispose();
                GC.Collect();
                _JobEntry.optJob.Checked = true;
                _JobEntry.txtReqNo.Focus();
                return;
            }

            if (_jobStage == "1.1")
            {
                if (_scvjobHdr.SJB_JOBSTAGE != Convert.ToDecimal(1.1))
                {
                    SystemInformationMessage("The job is not inspection stage!", "Inspection Job");
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                    return;
                }
            }

            if (optInspection.Checked == true) _jobRecall = 2;
            if (optJob.Checked == true) _jobRecall = 1;

            _jobRecallSeq = _scvjobHdr.SJB_SEQ_NO;

            txtNIC.Text = _scvjobHdr.SJB_B_NIC;
            txtMobile.Text = _scvjobHdr.SJB_B_MOBINO;
            txtCustCode.Text = _scvjobHdr.SJB_B_CUST_CD;
            txtCusName.Text = _scvjobHdr.SJB_B_CUST_NAME;
            cmbTitle.Text = _scvjobHdr.SJB_B_CUST_TIT;
            txtAddress1.Text = _scvjobHdr.SJB_B_ADD1;
            txtAddress2.Text = _scvjobHdr.SJB_B_ADD2;
            txtTown.Text = _scvjobHdr.SJB_B_ADD3;
            txtTown.Tag = _scvjobHdr.SJB_B_TOWN;
            txtEmail.Text = _scvjobHdr.SJB_B_EMAIL;

            txtContPersn.Text = _scvjobHdr.SJB_CNT_PERSON;
            txtContNo.Text = _scvjobHdr.SJB_CNT_PHNO;
            txtContLoc.Text = _scvjobHdr.SJB_CNT_ADD1;


            txtInfoPersn.Text = _scvjobHdr.SJB_INFM_PERSON;
            txtInfoNo.Text = _scvjobHdr.SJB_INFM_PHNO;
            txtInfoLoc.Text = _scvjobHdr.SJB_INFM_ADD1;

            txtReqRmk.Text = _scvjobHdr.SJB_RMK;
            txtJobRem.Text = _scvjobHdr.SJB_JOB_RMK;
            txtTechIns.Text = _scvjobHdr.SJB_TECH_RMK;

            txtManRef.Text = _scvjobHdr.SJB_MANUALREF;
            txtOrdRef.Text = _scvjobHdr.SJB_ORDERNO;

            txtPOD.Text = _scvjobHdr.SJB_POD_NO;
            txtLoc.Text = _scvjobHdr.SJB_REC_LOC;

            txtTaskLoc.Text = _scvjobHdr.SJB_JOBCAT;
            txtOrigin.Text = _scvjobHdr.SJB_JOBSTP;

            if (_scvjobHdr.SJB_JOBTP == "E")
            {
                ChkExternal.Checked = true;
                ChkExternal.Enabled = false;

                pnlItem.Visible = true;
                pnlSer.Enabled = false;
                optJob.Enabled = false;
                optReq.Enabled = false;
            }

            if (!string.IsNullOrEmpty(_scvjobHdr.SJB_CHG_CD))
            {
                txtVisitChgCode.Text = _scvjobHdr.SJB_CHG_CD;
                lblCharge.Text = lblCharge.Text = String.Format("{0:#,###,###.00}", _scvjobHdr.SJB_CHG.ToString());
            }

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_job_Det>();
            grvJobItms.DataSource = _scvItemList;

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Job_Defects>();
            grvDef.DataSource = _scvDefList;

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
            grvTech.DataSource = _scvEmpList;

            grvAddiItems.AutoGenerateColumns = false;
            grvAddiItems.DataSource = new List<Service_Job_Det_Sub>();
            grvAddiItems.DataSource = _scvItemSubList;

            //grvAddiItems.AutoGenerateColumns = false;
            //grvAddiItems.DataSource = new List<Service_TempIssue>();
            //grvAddiItems.DataSource = _scvStdbyList;

        }

        private void btnpnlReqClose_Click(object sender, EventArgs e)
        {
            pnlReq.Visible = false;
        }

        private void btnAddReqItems_Click(object sender, EventArgs e)
        {
            if (_reqDet == null) return;
            if (_reqDet.Count <= 0) return;

            int _selectCount = _reqDet.Where(x => x.Jrd_Select == true).Count();
            if (_selectCount <= 0) return;

            if (_scvParam.SP_MULTIITM == 0)
            {
                if (_selectCount > 1)
                {
                    SystemInformationMessage("You have not allowed to add multiple items for the Job!", "Multiple Item Not Allowed");
                    return;
                }
            }

            clear_Ext_Job_Items();
            pnlItem.Visible = false;

            if (_selectCount == 1)
            {
                //updated by akila 2017/11/23
                if (_reqDet != null && _reqDet.Count > 0)
                {
                    var _selectedItem = _reqDet.Where(x => x.Jrd_Select == true).ToList();
                    if (_selectedItem != null && _selectedItem.Count > 0)
                    {
                        Service_Req_Det _tempReqDet = _selectedItem.SingleOrDefault();
                        txtReqLineNo.Text = _tempReqDet.Jrd_reqline.ToString();
                        Load_Serial_Infor(txtWar, _tempReqDet.Jrd_warr, _tempReqDet.Jrd_chg_warr_stdt.Date);
                        add_job_item(1, _tempReqDet);
                        txtWar.Text = _tempReqDet.Jrd_warr;
                    }
                    else
                    {
                        SystemInformationMessage("Request details not found. Please select an item", "Invalid Operation");
                        return;
                    }
                }
                else
                {
                    SystemInformationMessage("Request details not found. Please select an item", "Invalid Operation");
                    return;
                }

                //Service_Req_Det _tempReqDet = _reqDet.Where(x => x.Jrd_Select == true).ToList()[0];
                //txtReqLineNo.Text = _tempReqDet.Jrd_reqline.ToString();
                //Load_Serial_Infor(txtWar, _tempReqDet.Jrd_warr, _tempReqDet.Jrd_chg_warr_stdt.Date);
                //add_job_item(1, _tempReqDet);
                //txtWar.Text = _tempReqDet.Jrd_warr;
            }

            if (_selectCount > 1)
            {
                List<Service_Req_Det> _tempReqDet = _reqDet.Where(x => x.Jrd_Select == true).ToList();
                foreach (Service_Req_Det _one in _tempReqDet)
                {
                    Load_Serial_Infor(txtWar, _one.Jrd_warr, _one.Jrd_chg_warr_stdt.Date);
                    //add_job_item(1);
                    txtWar.Text = _one.Jrd_warr;
                    add_job_item(1, _one);
                }
            }
            pnlReq.Visible = false;
        }

        private void txtReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSer.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_srch_req_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)  //kapila 7/4/2016
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsF3(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
                _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                txtReqNo.Focus();

                _CommonSearch.ShowDialog();

                _CommonSearch.dvResult.Refresh();

                txtReqNo_Leave(null, null);
            }
        }

        private void chkAllReq_CheckedChanged(object sender, EventArgs e)
        {
            if (grvReqItems.Rows.Count > 0)
            {
                if (chkAllReq.Checked == true)
                {
                    for (int i = 0; i < grvReqItems.Rows.Count; i++)
                    {
                        grvReqItems.Rows[i].Cells["Jrd_Select"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < grvReqItems.Rows.Count; i++)
                    {
                        grvReqItems.Rows[i].Cells["Jrd_Select"].Value = false;
                    }
                }
            }
        }

        private void btn_srch_task_loc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceTaskCate);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvTaskCateByLoc(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaskLoc;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtTaskLoc.Focus();

            load_job_task_dir();    //kapila 25/1/2016
        }

        private void load_job_task_dir()
        {
            DataTable JobCat = new DataTable();
            JobCat = CHNLSVC.CustService.sp_get_job_category(txtTaskLoc.Text);
            if (JobCat.Rows.Count > 0)
            {
                foreach (DataRow drow in JobCat.Rows)
                {
                    _taskDir = drow["sc_direct"].ToString();

                }
            }
        }

        private void btn_srch_prio_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePriority);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvPriority(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPriority;
            _CommonSearch.ShowDialog();
            txtPriority.Focus();
        }

        private void txtTaskLoc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaskLoc.Text))
            {
            }
        }

        private void txtPriority_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriority.Text))
            {
            }
        }

        private void txtVisitChgCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_stage_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dtExpectOn.Focus();
            }
        }

        private void txtVisitChgCode_Leave(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (!string.IsNullOrEmpty(txtVisitChgCode.Text))
            {
                if (optInspection.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtTown.Text))
                    {
                        txtVisitChgCode.Clear();
                        lblCharge.Text = "";
                        SystemWarnningMessage("Please select the customers area first", "Customer Town");
                        txtTown.Focus();
                        return;
                    }
                }
                else
                {
                    string _TownTag = string.Empty;
                    if (txtTown.Tag != null) _TownTag = txtTown.Tag.ToString();
                    decimal _rate = CHNLSVC.CustService.GetScvJobStageRate(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, BaseCls.GlbUserDefLoca, txtTaskLoc.Text, 1, dtDate.Value.Date, _chgJobStage, txtVisitChgCode.Text, _scvLoc.Ml_town_cd, _TownTag, out _msg);
                    if (_rate == -1)
                    {
                        txtVisitChgCode.Clear();
                        lblCharge.Text = "";
                        SystemWarnningMessage(_msg, "Charge");
                        txtVisitChgCode.Focus();
                    }
                    else
                    {
                        lblCharge.Text = String.Format("{0:#,###,###.00}", _rate.ToString());
                    }
                }
            }
        }

        private void optInspection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optInspection.Checked == true)
                {
                    if (string.IsNullOrEmpty(_scvLoc.Ml_town_cd))
                    {
                        SystemWarnningMessage("Service center area not setup!", "Inspection");
                        optJob.Checked = true;
                        return;
                    }

                    if (_scvLoc.Ml_town_cd == "N/A")
                    {
                        SystemWarnningMessage("Service center area not setup!", "Inspection");
                        optJob.Checked = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(_scvParam.SP_INSPECTION_ITM))
                    {
                        SystemWarnningMessage("There is no setup for inspection!", "Inspection");
                        optJob.Checked = true;
                        return;
                    }

                    //pnlHead.Location = new Point(559, 338);
                    //pnlHead.Size = new Size(447, 253);
                    //lblReqRmk.Visible = false;
                    txtReqRmk.Visible = false;
                    lblChargeType.Text = "Inspection Visit";
                    _chgJobStage = "1.1";

                    //Set_Job_Item();
                    clear_Ext_Job_Items();
                    //pnlInspection.Enabled = true;
                    btnJobTasks.Enabled = false;
                    ChkExternal.Enabled = true;
                    //ChkExternal.Checked = true;
                    //ChkExternal_Click(sender, e);
                    //ChkExternal.Text = "Inspection Item";

                    txtReqNo.Clear();
                    //txtDef.Focus();

                    //ChkExternal.Checked = true;
                    pnlItem.Visible = true;
                    pnlSer.Enabled = false;
                    optJob.Enabled = false;
                    optReq.Enabled = false;
                    btnShowExtItemPanel.Visible = true;
                    txtEItem.Focus();
                }
                else
                {
                    btnShowExtItemPanel.Visible = true;
                    ChkExternal.Checked = false;
                    ChkExternal.Text = "External Item";

                    //pnlHead.Location = new Point(559, 299);
                    //pnlHead.Size = new Size(447, 292);
                    //pnlInspection.Enabled = false;
                    //lblReqRmk.Visible = true;
                    txtReqRmk.Visible = false;
                    lblChargeType.Text = "Charge Type";
                    _chgJobStage = "2";
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default; SystemErrorMessage(err);
            }
        }

        private void dtpFromAL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpToAL.Focus();
            }
        }

        private void dtpToAL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_add_Tech.Focus();
            }
        }

        private void txtMilage_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnCloseHistory_Click(object sender, EventArgs e)
        {
            pnlHistory.Hide();
            pnlMain.Enabled = true;
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSerNo.Text))
            {
                if (lblSerNo.Text.ToUpper().ToString() != "N/A")
                {
                    pnlMain.Enabled = false;
                    lblDefectHistyHeader.Text = ":: Job History :: [ Item :- " + lblItem.Text + " Serial :-" + lblSerNo.Text + " ]";
                    ucDefectHistory1.Serial = lblSerNo.Text;
                    ucDefectHistory1.Item = lblItem.Text;
                    ucDefectHistory1.loadData();
                    pnlHistory.Show();
                    dtDate.Focus();
                }
            }
        }

        private void JobEntry_Load(object sender, EventArgs e)
        {
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                SystemWarnningMessage("Service parameter(s) not setup!", "Default Parameter(s)");
                this.Enabled = false;
                return;
            }

            if (_scvParam.SP_IS_DIRECT_PRINT == 1) { chk_Direct_print.Checked = true; }
            else { chk_Direct_print.Checked = false; }

            pnlEmp.Enabled = false;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10819) == true) pnlEmp.Enabled = true;

            //Updated by Akila. 2017/05/02
            lblSerSrch.Text = string.IsNullOrEmpty(_scvParam.SP_DB_SERIAL) ? "Serial #" : _scvParam.SP_DB_SERIAL;
            lblRegNo.Text = string.IsNullOrEmpty(_scvParam.SP_DB_VEHI_REG) ? "Registration #" : _scvParam.SP_DB_VEHI_REG;
            lblChassiNo.Text = string.IsNullOrEmpty(_scvParam.SP_DB_CHASSIS) ? "Other Serial" : _scvParam.SP_DB_CHASSIS;
            txtCustCode.Text = _scvParam.SP_EXTERNALCUST;

            //add by akila 2017/06/24
            lblESerial.Text = string.IsNullOrEmpty(_scvParam.SP_DB_SERIAL) ? "Serial #" : _scvParam.SP_DB_SERIAL;
            lblESerial2.Text = lblChassiNo.Text = string.IsNullOrEmpty(_scvParam.SP_DB_CHASSIS) ? "Other Serial" : _scvParam.SP_DB_CHASSIS;

            // if (_scvParam)

            if (_scvParam.SP_ISAUTOMOBI == 1)
            {
                pnlMilage.Visible = true;
                pnlItmTp.Visible = false;

                lblChassiNo.Visible = true;
                txtChassiNo.Visible = true;
                btnSearchChassiNo.Visible = true;
            }
            else
            {
                pnlMilage.Visible = false;
                pnlItmTp.Visible = true;

                lblChassiNo.Visible = false;
                txtChassiNo.Visible = false;
                btnSearchChassiNo.Visible = false;
            }

            //kapila 19/2/2016
            if (_scvParam.SP_IS_MAN_EXT_ITM_DT == 1)
                _isManExtItmDt = true;

            txtTaskLoc.Text = _scvParam.SP_DEF_CAT;
            label83.Text = _scvParam.SP_DEFECTDESCRIPTION;
            if (string.IsNullOrEmpty(_scvParam.SP_DEFECTDESCRIPTION))
            {
                label83.Text = "Defects";
            }

            _scvLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            ChkExternal.Enabled = true;

            dtpToAL.Value = DateTime.Now.AddHours(1);
            dtpFromAL.Value = DateTime.Now;

            //kapila 9/7/2015
            if (_scvParam.sp_is_allow_edit_part == 1)
                txtPartNo.Enabled = true;
            else
                txtPartNo.Enabled = false;

            load_job_task_dir();    //kapila 25/1/2016
        }

        private void txtTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_town_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
            }
        }

        private void ChkExternal_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbTitle.Enabled == true) cmbTitle.Focus(); else txtContPersn.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Mobile_Click(null, null);
            }
        }

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusName.Focus();
            }
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
                txtTown.Focus();
            }
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContPersn.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_NIC_Click(null, null);
            }
        }

        private void txtContPersn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContNo.Focus();
            }
        }

        private void txtContNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContLoc.Focus();
            }
        }

        private void txtContLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoPersn.Focus();
            }
        }

        private void txtInfoPersn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoNo.Focus();
            }
        }

        private void txtInfoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInfoLoc.Focus();
            }
        }

        private void txtInfoLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtExpectOn.Focus();
            }
        }

        private void dtExpectOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTaskLoc.Focus();
            }
        }

        private void txtTaskLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTaskLoc.Focus();
            }
        }

        private void txtOrigin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManRef.Focus();
            }
        }

        private void txtManRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrdRef.Focus();
            }
        }

        private void txtOrdRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtJobRem.Focus();
            }
        }

        private void FillPriority()
        {
            List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(txtCustCode.Text.Trim(), BaseCls.GlbUserComCode);
            if (oItems != null && oItems.Count > 0)
            {
                ucServicePriority1.GblCustCode = txtCustCode.Text.Trim();
                ucServicePriority1.LoadData();
            }
            else
            {
                ucServicePriority1.GblCustCode = "CASH.";
                ucServicePriority1.LoadData();
            }
        }

        private void chkEditCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditCustomer.Checked)
            {
                pnlCustBill.Enabled = true;
            }
            else
            {
                pnlCustBill.Enabled = false;
            }
        }

        private void btnIssStndBy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtReqNo.Text))
            {
                MessageBox.Show("Please enter a job number", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegNo.Focus();
                return;
            }

            bool _isSelect = false;
            Int32 selectedItmLine = 0;
            if (grvJobItms.Rows.Count == 1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                    selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
                }
                _isSelect = true;
                // selectedItmLine = 1;
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                    {
                        _isSelect = true;
                        //selectedItmLine = i;
                        selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
                    }
                }
            }

            if (_isSelect == false)
            {
                SystemInformationMessage("Please select the job item", "Job Item");
                return;
            }
            StandbyIssue frm = new StandbyIssue(txtReqNo.Text.Trim(), selectedItmLine);
            frm.ShowInTaskbar = false;
            frm.ShowDialog();
        }


        private void txtReqNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_req_Click(null, null);
        }

        private void setPanalDefault()
        {
            pnlWarr.Height = 128;
        }

        private void chkAllItems_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllItems.Checked)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = false;
                }
            }
        }

        private void btnLoadDOInvoice_Click(object sender, EventArgs e)
        {
            pnlDOInvoiceItems.Visible = true;
        }

        private void btnLoadItems_Click(object sender, EventArgs e)
        {
            //LoadInvoiceItems();

            //commented by akila 2017/06/09
            if (dgvDOInvoiceItems.Rows.Count > 0)
            {
                if (isAnySelectedDOItems())
                {
                    for (int i = 0; i < dgvDOInvoiceItems.Rows.Count; i++)
                    {
                        if (dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value != null && Convert.ToBoolean(dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value) == true)
                        {
                            txtSer.Text = dgvDOInvoiceItems.Rows[i].Cells["ITS_SER_1D1"].Value.ToString();
                            txtSer_Leave(null, null);
                            //btn_add_ser_Click(null, null);

                            if (_jobRecall == 1)
                            {
                                SystemInformationMessage("Can't add additional items for saved job!", "Not Allowed");
                                return;
                            }
                            if (_scvParam.SP_MULTIITM == 0)
                            {
                                if (_scvItemList != null)
                                {
                                    Int32 _count = _scvItemList.Count();
                                    if (_count > 0)
                                    {
                                        if (_jobRecall == 2 && _count == 1)
                                        {
                                        }
                                        else
                                        {
                                            SystemInformationMessage("You have not allowed to add multiple items for the Job!", "Multiple Item Not Allowed");
                                            return;
                                        }
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(lblItem.Text))
                            {
                                SystemInformationMessage("Please enter the job item details!", "Item not found");
                                return;
                            }

                            if (lblSerNo.Text.ToUpper() != "N/A")
                            {
                                Int32 _count = _scvItemList.Where(X => X.Jbd_itm_cd == lblItem.Text && X.Jbd_ser1 == lblSerNo.Text).Count();
                                if (_count > 0)
                                {
                                    SystemInformationMessage("Already added this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                    return;
                                }

                                int _closeJobStage = 11;
                                if (_scvParam.SP_ISNEEDGATEPASS == 0) _closeJobStage = 8;//edit by Chamal 16-Jun-2015

                                List<Service_job_Det> _pendingjobdetList = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(lblSerNo.Text, lblItem.Text, BaseCls.GlbUserComCode);
                                //_count = _pendingjobdetList.Where(X => X.Jbd_stage >= Convert.ToDecimal(1.1) && X.Jbd_stage <= 8).Count();
                                //if (_count > 0)
                                //{
                                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                //    return;
                                //}
                                // _count = _pendingjobdetList.Where(X => X.Jbd_stage >= 13 && X.Jbd_stage <= 14).Count();
                                //_count = _pendingjobdetList.Where(X => X.Jbd_stage < 11).Count();

                                //if

                                _count = _pendingjobdetList.Where(X => X.Jbd_stage < _closeJobStage).Count();
                                if (_count > 0)
                                {
                                    //SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                    SystemInformationMessage("Pending job available for this item! Job No - " + _pendingjobdetList[0].Jbd_jobno, "Duplicate" + _scvParam.SP_DB_SERIAL); //kapila 30/6/2015
                                    return;
                                }
                            }
                            if (lblWarNo.Text.ToUpper() != "N/A")
                            {
                                Int32 _count = _scvItemList.Where(X => X.Jbd_warr == lblWarNo.Text).Count();
                                if (_count > 0)
                                {
                                    SystemInformationMessage("Already added this item!", "Duplicate Warranty No");
                                    return;
                                }
                            }
                            if (pnlMilage.Visible == true)
                            {
                                if (string.IsNullOrEmpty(txtMilage.Text))
                                {
                                    SystemInformationMessage("Please enter the current milage reading!", "Milage");
                                    txtMilage.Focus();
                                    return;
                                }
                            }

                            //TODO: Check Job Details still pending serials available or no if available block adding

                            _itmBrand = lblBrand.Text;
                            _warStus = (lblWarStus.Text == "UNDER WARRANTY") ? 1 : 0;

                            add_job_item(1, null);
                            txtDef.Focus();

                            //if (optReq.Checked)
                            //{
                            //    List<Service_Job_Defects> oItems = CHNLSVC.CustService.GetRequestJobDefectsJobEnty(txtReqNo.Text, _jobItemLine);
                            //    grvDef.DataSource = new List<Service_Job_Defects>();
                            //    grvDef.DataSource = oItems;
                            //}
                        }
                    }
                    pnlDOInvoiceItems.Visible = false;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pnlDOInvoiceItems.Visible = false;
        }

        private void loadInvoiceDOItems()
        {
            DataTable dtTemp = CHNLSVC.CustService.GET_INT_HDR_ITMS_JOBENTY(txtDONumber.Text, txtInvoiceNumer.Text);
            dgvDOInvoiceItems.DataSource = dtTemp;
        }

        private void txtDONumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadInvoiceDOItems();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtDONumber_DoubleClick(null, null);
            }
        }

        private void txtInvoiceNumer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadInvoiceDOItems();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtInvoiceNumer_DoubleClick(null, null);
            }
        }

        private bool isAnySelectedDOItems()
        {
            bool status = false;

            for (int i = 0; i < dgvDOInvoiceItems.Rows.Count; i++)
            {
                if (dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value != null && Convert.ToBoolean(dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value) == true)
                {
                    status = true;
                    return status;
                }
            }

            return status;
        }

        private void btnSearchDO_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber);
                _result = CHNLSVC.CommonSearch.GetDOInvoiceNumber(_CommonSearch.SearchParams, null, null);
                // _result.Columns.RemoveAt(1);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDONumber;
                _CommonSearch.ShowDialog();
                txtDONumber.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber);
                _result = CHNLSVC.CommonSearch.GetDOInvoiceNumber(_CommonSearch.SearchParams, null, null);
                _result.Columns.RemoveAt(0);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDONumber;
                _CommonSearch.ShowDialog();
                txtDONumber.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtDONumber_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDONumber.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.GetDOInvoiceNumber(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber), "DOCUMENT", txtDONumber.Text);
                if (_result != null && _result.Rows.Count > 0)
                {

                }
                else
                {
                    MessageBox.Show("Please enter valid document number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDONumber.Clear();
                    return;
                }
            }
        }

        private void txtInvoiceNumer_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtInvoiceNumer.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.GetDOInvoiceNumber(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber), "INVOICE", txtInvoiceNumer.Text);
                if (_result != null && _result.Rows.Count > 0)
                {

                }
                else
                {
                    MessageBox.Show("Please enter valid invoice number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoiceNumer.Clear();
                    return;
                }
            }
        }

        private void txtDONumber_DoubleClick(object sender, EventArgs e)
        {
            btnSearchDO_Click(null, null);
        }

        private void txtInvoiceNumer_DoubleClick(object sender, EventArgs e)
        {
            btnSearchInvoice_Click(null, null);
        }

        private void chkSelectAllDO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllDO.Checked)
            {
                for (int i = 0; i < dgvDOInvoiceItems.Rows.Count; i++)
                {
                    dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgvDOInvoiceItems.Rows.Count; i++)
                {
                    dgvDOInvoiceItems.Rows[i].Cells["selectD1"].Value = false;
                }
            }
        }

        private void txtCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_CustCode_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtCustCode_Leave(null, null);
            }
        }

        private void txtMobile_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Mobile_Click(null, null);
        }

        private void txtCustCode_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_CustCode_Click(null, null);
        }

        private void txtTown_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_town_Click(null, null);
        }

        private void txtNIC_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_NIC_Click(null, null);
        }

        private void btnAttachDocs_Click(object sender, EventArgs e)
        {
            Int32 selectedItmLine = 0;
            string SerialNo = "";
            if (grvJobItms.Rows.Count == 1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                }
                selectedItmLine = 1;

            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                    {
                        selectedItmLine = i;
                    }
                }
            }
            foreach (DataGridViewRow row in grvJobItms.Rows)
            {
                SerialNo = row.Cells["JBD_SER1"].Value.ToString();
            }

            //kapila 6/3/2016
            ImageUploadDTO Input = new ImageUploadDTO();
            List<ImageUploadDTO> oItemsGetting = new List<ImageUploadDTO>();
            Input.JobNumber = txtReqNo.Text;
            Input.JobLine = 1;
            string err = "";

            ImageUpload _frmImgUp = new ImageUpload(txtReqNo.Text, 1, "", 1);

            oItemsGetting = CHNLSVC.CustService.GetImages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, Input, out err);
            _frmImgUp.setUploadJobImgList(oItemsGetting);


            ImageUpload frm = new ImageUpload(txtReqNo.Text, selectedItmLine, SerialNo, 1);
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable JobCat = new DataTable();
            JobCat = CHNLSVC.CustService.sp_get_job_category(txtTaskLoc.Text);
            if (JobCat.Rows.Count > 0)
            {
                foreach (DataRow drow in JobCat.Rows)
                {
                    BaseCls.GlbReportTp = drow["sc_direct"].ToString();
                    if (BaseCls.GlbReportTp == "W")
                    {
                        BaseCls.GlbReportTp = "JOBW";
                    }
                    else
                    {
                        BaseCls.GlbReportTp = "JOBF";
                    }
                }
            }

            BaseCls.GlbReportTp = txtTaskLoc.Text;
            Reports.Service.ReportViewerSVC _viewJob = new Reports.Service.ReportViewerSVC();
            BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
            BaseCls.GlbReportDoc = txtReqNo.Text;

            Reports.Service.clsServiceRep objSvc = new Reports.Service.clsServiceRep();

            if (BaseCls.GlbDefSubChannel == "MCS")
            { if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
            if (BaseCls.GlbDefSubChannel == "HERO")
            { if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
            //Tharanga 2017/06/12
            //else if (BaseCls.GlbDefSubChannel == "HERO")
            //{if (chk_Direct_print.Checked == true) { BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }
            //else if (BaseCls.GlbDefSubChannel == "AM")
            //{if (chk_Direct_print.Checked == true){ BaseCls.GlbReportDirectPrint = 1; } else { BaseCls.GlbReportDirectPrint = 0; } }



            else
            { BaseCls.GlbReportDirectPrint = 0; }
            if (BaseCls.GlbReportDirectPrint == 1)
            {
                string _repname = string.Empty;
                string _papersize = string.Empty;
                CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);
                if (!(_repname == null || _repname == "")) { GlbReportName = _repname; BaseCls.GlbReportName = _repname; BaseCls.GlbReportPaperSize = _papersize; }

                if (BaseCls.GlbReportName == null || BaseCls.GlbReportName == "")
                {
                    MessageBox.Show("Report is not setup. Contact IT Department...\n", "Report not Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    if (BaseCls.GlbReportName == "JobCard_ABE_Work_shop.rpt")
                    {
                        objSvc.ABE_ServiceJobCard_Workshop();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        objSvc._JobCard_ABE_Work_shop.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSvc._JobCard_ABE_Work_shop.PrintToPrinter(1, false, 0, 0);
                    }
                    else if (BaseCls.GlbReportName == "JobCard_ABE_Feild.rpt")
                    {
                        objSvc.ABE_ServiceJobCard_Workshop();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        objSvc._JobCard_ABE_Feild.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSvc._JobCard_ABE_Feild.PrintToPrinter(1, false, 0, 0);
                    }

                    else
                    {
                        objSvc.ServiceJobCardPrint();
                        Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                        objSvc._JobCardWPh.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                        MessageBox.Show("Please check whether printer load the Job documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSvc._JobCardWPh.PrintToPrinter(1, false, 0, 0);
                    }

                }
            }
            else
            {
                _viewJob.Show();
                _viewJob = null;
            }
        }

        private void btnPayMode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReqNo.Text))
            {
                if (!string.IsNullOrEmpty(txtVisitChgCode.Text))
                {
                    if (Convert.ToDecimal(lblCharge.Text) > 0)
                    {
                        ucPayModes1.TotalAmount = Convert.ToDecimal(lblCharge.Text.Trim());
                        LoadPayMode();

                        pnlPaymode.Visible = true;
                        pnlHead.Enabled = false;
                        ucPayModes1.Amount.Focus();
                        //ucPayModes1.Refresh  
                    }
                }
            }
        }

        private void btnClosePaymode_Click(object sender, EventArgs e)
        {
            pnlPaymode.Visible = false;
            pnlHead.Enabled = true;
        }

        private void checkCustomer(string _com, string _identification)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                List<MasterBusinessEntity> _customerList = new List<MasterBusinessEntity>();
                BlackListCustomers _blackListCustomers = new BlackListCustomers();
                string _cusCode = "";
                _isBlackInfor = "";
                _isBlack = false;

                if (!string.IsNullOrEmpty(_identification))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, _identification.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {

                        _cusCode = _masterBusinessCompany.Mbe_cd;
                        _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);
                        if (_blackListCustomers != null)//add validateion inform by sanjeewa 2018/06/18
                        {


                            if (_blackListCustomers.Hbl_cust_cd != null)
                            {
                                if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                {
                                    _isBlackInfor = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    _isBlack = true;
                                    return;
                                }
                                else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                {
                                    _isBlackInfor = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    _isBlack = false;
                                    return;
                                }
                            }
                            else
                            {
                                _isBlackInfor = "Existing customer.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        // _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, _identification.Trim(), string.Empty, "C");
                        _customerList = CHNLSVC.Sales.GetBusinessCompanyDetailList(_com, string.Empty, _identification.Trim(), string.Empty, "C");

                        if (_customerList != null)
                        {
                            //if (_masterBusinessCompany.Mbe_cd != null)
                            foreach (MasterBusinessEntity _tmp in _customerList)
                            {
                                _cusCode = _tmp.Mbe_cd;
                                _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                                if (_blackListCustomers != null)
                                {
                                    //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    //_isBlack = true;
                                    //return;
                                    if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                    {
                                        _isBlackInfor = "Black listed customer." + _tmp.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        _isBlack = true;
                                        return;
                                    }
                                    else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                    {
                                        _isBlackInfor = "Black listed customer by showroom end." + _tmp.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        _isBlack = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    _isBlackInfor = "Existing customer.";
                                    // return;
                                }
                            }
                        }

                        else
                        {
                            //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, string.Empty, _identification.Trim(), "C");
                            _customerList = CHNLSVC.Sales.GetBusinessCompanyDetailList(_com, string.Empty, string.Empty, _identification.Trim(), "C");

                            //if (_masterBusinessCompany.Mbe_cd != null)
                            if (_customerList != null)
                            {
                                foreach (MasterBusinessEntity _tmp1 in _customerList)
                                {
                                    _cusCode = _tmp1.Mbe_cd;
                                    _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                                    if (_blackListCustomers != null)
                                    {
                                        //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        //_isBlack = true;
                                        //return;
                                        if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                        {
                                            _isBlackInfor = "Black listed customer." + _tmp1.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                            _isBlack = true;
                                            return;
                                        }
                                        else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                        {
                                            _isBlackInfor = "Black listed customer by showroom end." + _tmp1.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                            _isBlack = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        _isBlackInfor = "Existing customer.";
                                        // return;
                                    }
                                }
                            }

                            else
                            {
                                _isBlackInfor = "Cannot find existing customer details for given identification.";
                                return;
                            }
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

        private void txtContNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContNo.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtContNo.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContNo.Text = "";
                    txtContNo.Focus();
                    return;
                }
            }
        }

        private void txtInfoNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInfoNo.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtInfoNo.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInfoNo.Text = "";
                    txtInfoNo.Focus();
                    return;
                }
            }
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtReqNo.Text, _gridItemLine, txtCustCode.Text);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btn_srch_sup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEItem.Text))
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemSupplier);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSupplierSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSupplier;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtSupplier_Leave(null, null);
            }
            else
            {
                SystemWarnningMessage("Please select the external item code", "External Item");
                return;
            }
        }

        private void txtSupplier_Leave(object sender, EventArgs e)
        {
            DataTable SUP_DET = CHNLSVC.CustService.GetSupplierDetails(txtSupplier.Text, BaseCls.GlbUserComCode);
            if (SUP_DET.Rows.Count > 0)
            {
                DataRow dr;
                dr = SUP_DET.Rows[0];
                lblSuppName.Text = dr["MBE_NAME"].ToString();
            }
            else
                lblSuppName.Text = "";
        }

        private void btnJobTasks_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReqNo.Text))
            {
                MessageBox.Show("Please select the job number.", "Job Tasks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ServiceTasks frm = new ServiceTasks(txtReqNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void txtCusName_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusName.Text))
            {
                string _copyText = txtCusName.Text;
                Clipboard.SetText(_copyText.ToString());
                MessageBox.Show(_copyText, "Copy to Clipboard");
            }
        }

        private void txtAddress1_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress1.Text))
            {
                string _copyText = txtAddress1.Text;
                Clipboard.SetText(_copyText.ToString());
                MessageBox.Show(_copyText, "Copy to Clipboard");
            }
        }

        private void txtAddress2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress2.Text))
            {
                string _copyText = txtAddress2.Text;
                Clipboard.SetText(_copyText.ToString());
                MessageBox.Show(_copyText, "Copy to Clipboard");
            }
        }

        private void grvJobItms_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtReqNo.Text) && grvJobItms.SelectedRows[e.RowIndex].Cells["jbd_jobline"].Value != null)
            //{
            //    List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtReqNo.Text, Convert.ToInt32(grvJobItms.SelectedRows[e.RowIndex].Cells["jbd_jobline"].Value.ToString()));
            //    if (oSubItems.Count > 0)
            //    {
            //        dgvOldParts.AutoGenerateColumns = false;
            //        dgvOldParts.DataSource = oSubItems;
            //        pnlSubItems.Visible = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No sub items to view", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void btnCloseSubItems_Click(object sender, EventArgs e)
        {
            pnlSubItems.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtReqNo.Text))
            {
                MessageBox.Show("Please enter a job number", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegNo.Focus();
                return;
            }

            bool _isSelect = false;
            Int32 selectedItmLine = 0;
            if (grvJobItms.Rows.Count == 1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jbd_select"].Value = true;
                    selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
                }
                _isSelect = true;
                // selectedItmLine = 1;
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jbd_select"].Value) == true)
                    {
                        _isSelect = true;
                        //selectedItmLine = i;
                        selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["jbd_jobline"].Value);
                    }
                }
            }

            if (_isSelect == false)
            {
                SystemInformationMessage("Please select the job item", "Job Item");
                return;
            }
            List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(txtReqNo.Text, selectedItmLine);
            if (oSubItems.Count > 0)
            {
                dgvOldParts.AutoGenerateColumns = false;
                dgvOldParts.DataSource = oSubItems;
                pnlSubItems.Visible = true;
            }
            else
            {
                MessageBox.Show("No sub items to view", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCloseOthWarrPnl_Click(object sender, EventArgs e)
        {
            pnlOthWarr.Visible = false;
        }

        private void fillSubItems()
        {
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            if (grvAddiItems.Rows.Count > 0)
            {
                for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value) == true)
                    {
                        _scvItemSubList.Add(_tempItemSubList.Find(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()));
                    }
                }

                //if (oSubItems.FindAll(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()).Count > 0)
                //{
                //    grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
                //    _scvItemSubList.Add(oSubItems.Find(x => x.JBDS_SER1 == grvAddiItems.Rows[i].Cells["jbds_ser1"].Value.ToString()));
                //}
            }

            int lineNums = 1;
            int JobLine = 0;

            if (grvJobItms.Rows.Count == 1)
            {
                JobLine = 1;
            }
            else
            {
                JobLine = Convert.ToInt32(grvJobItms.SelectedRows[0].Cells["jbd_jobline"].Value.ToString());
            }

            foreach (Service_Job_Det_Sub item in _scvItemSubList)
            {
                item.JBDS_JOBLINE = JobLine;
                item.JBDS_LINE = lineNums;
                lineNums = lineNums + 1;
            }
        }

        private void SetReadOnlyClmn()
        {
            if (grvAddiItems.Rows.Count > 0)
            {

                for (int i = 0; i < grvAddiItems.Rows.Count; i++)
                {
                    if (grvAddiItems.Rows[i].Cells["JBDS_ITM_TP"].Value.ToString().ToUpper() == "M")
                    {
                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].Value = true;
                        grvAddiItems.Rows[i].Cells["JBDS_SELECT"].ReadOnly = true;
                    }
                }
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email address invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(txtReqNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }

        private void txtSer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTaskLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private Int32 JobCancel()
        {
            Int32 result = 0;
            List<Service_job_Det> oJobDetails = CHNLSVC.CustService.GetJobDetails(txtReqNo.Text.Trim(), -777, BaseCls.GlbUserComCode);
            if (oJobDetails != null && oJobDetails.Count > 0)
            {
                if (oJobDetails.FindAll(x => x.Jbd_stage > 3).Count > 0)
                {
                    MessageBox.Show("Cannot close this job. Job items has taken to WIP.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }
                else if (oJobDetails.FindAll(x => x.Jbd_isstockupdate > 1).Count > 0)
                {
                    MessageBox.Show("Cannot close this job. Stock is updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }
                else
                {
                    string err;
                    result = CHNLSVC.CustService.jobCancel(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, oJobDetails, out err);
                    if (result > 0)
                    {
                        MessageBox.Show("Job canceled", "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error : " + err, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        private void txtSerialChange_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchSerialChange_Click(null, null);
        }

        private void txtSerialChange_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerialChange.Text))
            {
                if (ChkExternal.Checked)
                {
                    return;
                }
                List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
                List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
                Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;
                int _returnStatus;
                String _returnMsg;
                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(txtSerialChange.Text.Trim(), "", "", "", "", "", 0, out _returnStatus, out _returnMsg);
                foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                {
                    _warrMst = pair.Key;
                    _warrMstSub = pair.Value;
                }

                recalledJobSerial = _warrMst[0];

                lblCSItem.Text = _warrMst[0].Irsm_itm_cd;
                lblCSDescription.Text = _warrMst[0].Irsm_itm_desc;
                lblCSModel.Text = _warrMst[0].Irsm_itm_model;
                if (_returnStatus == 2)
                {
                    _warStus = 0;
                    lblWarStus.Text = "OVER WARRANTY";

                }

            }
        }

        private void btnSearchSerialChange_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "SER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialChange;
                _CommonSearch.ShowDialog();
                txtSerialChange.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnSerialChangeConfrim_Click(object sender, EventArgs e)
        {
            if (recalledJob != null)
            {
                Service_job_Det _jobDt = new Service_job_Det();
                _jobDt.Jbd_jobline = _jobItemLine;
                _jobDt.Jbd_jobno = txtReqNo.Text.Trim();
                _jobDt.Jbd_loc = BaseCls.GlbUserDefLoca;
                _jobDt.Jbd_pc = BaseCls.GlbUserDefProf;
                _jobDt.Jbd_onloan = 0;
                _jobDt.Jbd_availabilty = 1;
                _jobDt.Jbd_custnotes = "";
                _jobDt.Jbd_isgatepass = 0;
                _jobDt.Jbd_iswrn = 0;
                _jobDt.Jbd_conf_desc = "";
                _jobDt.Jbd_isagreement = "0";
                _jobDt.Jbd_stage = Convert.ToDecimal("1.2");
                _jobDt.Jbd_com = BaseCls.GlbUserComCode;
                if (ChkExternal.Checked == false)
                {
                    MasterItem _item = CHNLSVC.Inventory.GetItem("", recalledJobSerial.Irsm_itm_cd);
                    _jobDt.Jbd_itm_cd = recalledJobSerial.Irsm_itm_cd;
                    _jobDt.Jbd_itm_stus = recalledJobSerial.Irsm_itm_stus;
                    _jobDt.Jbd_itm_desc = recalledJobSerial.Irsm_itm_desc;
                    _jobDt.Jbd_brand = recalledJobSerial.Irsm_itm_brand;
                    _jobDt.Jbd_model = recalledJobSerial.Irsm_itm_model;
                    _jobDt.Jbd_itm_cost = recalledJobSerial.Irsm_unit_cost;
                    _jobDt.Jbd_ser1 = recalledJobSerial.Irsm_ser_1;
                    _jobDt.Jbd_ser2 = recalledJobSerial.Irsm_ser_2;
                    _jobDt.Jbd_warr = recalledJobSerial.Irsm_warr_no;
                    _jobDt.Jbd_regno = recalledJobSerial.Irsm_reg_no;
                    _jobDt.Jbd_warr_stus = (recalledJobSerial.Irsm_warr_stus == "Y") ? 1 : 0;
                    _jobDt.Jbd_itmtp = _item.Mi_itm_tp;
                    _jobDt.Jbd_warrperiod = recalledJobSerial.Irsm_warr_period;
                    _jobDt.Jbd_warrrmk = recalledJobSerial.Irsm_warr_rem;
                    _jobDt.Jbd_ser_id = recalledJobSerial.Irsm_ser_id.ToString();
                    _jobDt.Jbd_warrstartdt = recalledJobSerial.Irsm_warr_start_dt;
                    _jobDt.Jbd_date_pur = recalledJobSerial.Irsm_invoice_dt;
                    _jobDt.Jbd_invc_no = recalledJobSerial.Irsm_invoice_no;
                    _jobDt.Jbd_invc_showroom = recalledJobSerial.Irsm_loc;
                    _jobDt.Jbd_supp_cd = recalledJobSerial.Irsm_orig_supp;
                    _jobDt.Jbd_act = 1;
                }
                else
                {
                    List<Service_job_Det> oJObDets = CHNLSVC.CustService.GetJobDetails(_jobDt.Jbd_jobno, _jobDt.Jbd_jobline, _jobDt.Jbd_com);
                    if (oJObDets != null && oJObDets.Count > 0)
                    {
                        Service_job_Det oJobDet = oJObDets[0];
                        _jobDt.Jbd_itm_cd = oJobDet.Jbd_itm_cd;
                        _jobDt.Jbd_itm_stus = oJobDet.Jbd_itm_stus;
                        _jobDt.Jbd_itm_desc = oJobDet.Jbd_itm_desc;
                        _jobDt.Jbd_brand = oJobDet.Jbd_brand;
                        _jobDt.Jbd_model = oJobDet.Jbd_model;
                        _jobDt.Jbd_itm_cost = oJobDet.Jbd_itm_cost;
                        _jobDt.Jbd_ser1 = txtSerialChange.Text;
                        _jobDt.Jbd_ser2 = oJobDet.Jbd_ser2;
                        _jobDt.Jbd_warr = oJobDet.Jbd_warr;
                        _jobDt.Jbd_regno = oJobDet.Jbd_regno;
                        _jobDt.Jbd_warr_stus = oJobDet.Jbd_warr_stus;
                        _jobDt.Jbd_itmtp = oJobDet.Jbd_itmtp;
                        _jobDt.Jbd_warrperiod = oJobDet.Jbd_warrperiod;
                        _jobDt.Jbd_warrrmk = oJobDet.Jbd_warrrmk;
                        _jobDt.Jbd_ser_id = oJobDet.Jbd_ser_id;
                        _jobDt.Jbd_warrstartdt = oJobDet.Jbd_warrstartdt;
                        _jobDt.Jbd_date_pur = oJobDet.Jbd_date_pur;
                        _jobDt.Jbd_invc_no = oJobDet.Jbd_invc_no;
                        _jobDt.Jbd_invc_showroom = oJobDet.Jbd_invc_showroom;
                        _jobDt.Jbd_supp_cd = oJobDet.Jbd_supp_cd;
                        _jobDt.Jbd_act = oJobDet.Jbd_act;
                    }
                    else
                    {
                        return;
                    }
                }

                //_jobDt.Jbd_stage = 2;

                string err;
                Service_Job_StageLog oLog1 = new Service_Job_StageLog();
                oLog1.SJL_JOBNO = txtReqNo.Text;
                oLog1.SJL_JOBLINE = 0;
                oLog1.SJL_COM = BaseCls.GlbUserComCode;
                oLog1.SJL_LOC = BaseCls.GlbUserDefLoca;
                oLog1.SJL_JOBSTAGE = 2;
                oLog1.SJL_CRE_BY = BaseCls.GlbUserID;
                oLog1.SJL_CRE_DT = DateTime.Now;
                oLog1.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                oLog1.SJL_INFSUP = 0;

                Int32 result = CHNLSVC.CustService.UpdateInspectionSerial(_jobDt, oLog1, out err);
                if (result > 0)
                {
                    MessageBox.Show("Successfully Updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlSearilChange.Visible = false;
                    txtReqNo_Leave(null, null);
                }
            }
        }

        private void btnSerialChangeCancel_Click(object sender, EventArgs e)
        {
            pnlSearilChange.Visible = false;
        }

        private void txtSerialChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchSerialChange_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtSerialChange_Leave(null, null);
            }
        }

        private void btnCloseSearial_Click(object sender, EventArgs e)
        {
            pnlSearilChange.Visible = false;
        }

        private void btnShowExtItemPanel_Click(object sender, EventArgs e)
        {
            clear_Ext_Job_Items();
            pnlItem.Visible = true;
            txtEItem.Focus();
        }

        private bool Check_Serial_IsStock()
        {
            bool _isCheckSales = true;
            if (optReq.Checked == true) _isCheckSales = false;
            if (optInspection.Checked == true) _isCheckSales = false;
            if (ChkExternal.Checked == true) _isCheckSales = false;


            if (_isCheckSales == true)
            {
                if (string.IsNullOrEmpty(lblInv.Text) || lblInv.Text == "N/A")
                {
                    int _returnStatus = 0;
                    string _returnMsg = string.Empty;

                    Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;
                    List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
                    string _ser = null;
                    string _warr = null;
                    string _regno = null;
                    string _invcno = null;
                    string _ser2 = null;

                    //updated by akila 2017/06/23
                    _ser = txtSer.Text.ToString().Trim();
                    _warr = txtWar.Text.ToString().Trim();
                    _regno = txtRegNo.Text.ToString().Trim();
                    _ser2 = txtChassiNo.Text.ToString().Trim();

                    //   _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(txtSer.Text, "", _regno, txtWar.Text, "", "", 0, out _returnStatus, out _returnMsg);

                    //updated by akila 2017/06/23
                    _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, _ser2, _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
                    //_warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(txtSer.Text, "", _regno, txtWar.Text, "", "", 0, out _returnStatus, out _returnMsg);

                    foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                    {
                        _warrMst = pair.Key;

                    }

                    //updated by akila 
                    var _warMStInvoices = _warrMst.OrderByDescending(x => x.Irsm_doc_dt).ThenBy(x => x.Irsm_invoice_no).Select(x => x.Irsm_doc_no).ToList();
                    InventoryHeader _hdr = new InventoryHeader();
                    if (_warMStInvoices != null)
                    {
                        _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_warMStInvoices[0]);
                    }

                    //InventoryHeader _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_warrMst[0].Irsm_doc_no);
                    if (_hdr != null)
                    {
                        //updated by akila 2018/02/20 add type FRISU
                        if (_hdr.Ith_cate_tp != "FGAP" && _hdr.Ith_cate_tp != "FIXED" && _hdr.Ith_cate_tp != "FRISU") // item cat Fixed add by tharanga 2017/08/14
                        {
                            DataTable _outward = CHNLSVC.CustService.Get_last_outwarddoc(lblItem.Text, txtSer.Text);
                            string rowValue = "";
                            if (_outward.Rows.Count > 0)
                            {
                                DataRow rownew = _outward.Rows[0];
                                rowValue = rownew["ith_cate_tp"].ToString();
                            }

                            //updated by akila 2018/02/20 add type FRISU
                            if (rowValue != "FGAP" && rowValue != "FIXED" && rowValue != "FRISU") // item cat Fixed add by tharanga 2017/08/14
                            {


                                if (MessageBox.Show("Sales detail not found. Do you want to check further details?", "Sales Details Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    //  DataTable _outward = CHNLSVC.CustService.Get_last_outwarddoc(lblItem.Text, txtSer.Text);
                                    if (_outward.Rows.Count > 0)
                                    {
                                        foreach (DataRow row in _outward.Rows)
                                        {

                                            string _msg = "";
                                            _msg += "Sales details not found for the serial " + txtSer.Text + ".";
                                            _msg += System.Environment.NewLine + "Last outward document details are,";
                                            _msg += System.Environment.NewLine + "Location Code : " + row["ith_loc"].ToString();
                                            _msg += System.Environment.NewLine + "Doc No : " + row["ith_doc_no"].ToString();
                                            _msg += System.Environment.NewLine + "Doc Type : " + row["ith_doc_tp"].ToString();
                                            _msg += System.Environment.NewLine + "Doc Date : " + Convert.ToDateTime(row["ith_doc_date"]).Date.ToString("dd/MMM/yyyy");
                                            _msg += System.Environment.NewLine + "Manual Ref : " + row["ith_manual_ref"].ToString();
                                            _msg += System.Environment.NewLine + "Remarks : " + row["ith_remarks"].ToString();

                                            SystemInformationMessage(_msg, "More Details");
                                        }
                                    }
                                    else
                                    {
                                        SystemInformationMessage("No Details Found", "Sales Details Missing");
                                    }
                                }

                                btnAdd.Enabled = false;
                                btnAddAdditional.Enabled = false;
                                return true;

                            }
                            else
                            { _fgap = 1; }
                        }
                        else
                        {

                            _fgap = 1;
                        }
                    }




                }
                else
                {
                    Boolean _IsinvSCM2 = false;
                    Boolean _IsinvSCM = false;
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, lblInv.Text.Trim(), "C", null, null);
                    if (_invHdr != null || _invHdr.Count > 0)
                    {
                        _IsinvSCM2 = true;
                    }
                    DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(lblInv.Text.Trim());
                    if (_invoicedt != null || _invoicedt.Rows.Count > 0)
                    {
                        _IsinvSCM = true;
                    }

                    if (_IsinvSCM2 == false && _IsinvSCM == false)
                    {
                        //SystemInformationMessage("Sales detail not found", "Sales Details Missing");
                        if (MessageBox.Show("Sales detail not found. Do you want to check further details?", "Sales Details Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            DataTable _outward = CHNLSVC.CustService.Get_last_outwarddoc(lblItem.Text, txtSer.Text);
                            if (_outward.Rows.Count > 0)
                            {
                                foreach (DataRow row in _outward.Rows)
                                {
                                    string _msg = "";
                                    _msg += "Sales details not found for the serial " + txtSer.Text + ".";
                                    _msg += System.Environment.NewLine + "Last outward document details are,";
                                    _msg += System.Environment.NewLine + "Location Code : " + row["ith_loc"].ToString();
                                    _msg += System.Environment.NewLine + "Doc No : " + row["ith_doc_no"].ToString();
                                    _msg += System.Environment.NewLine + "Doc Type : " + row["ith_doc_tp"].ToString();
                                    _msg += System.Environment.NewLine + "Doc Date : " + Convert.ToDateTime(row["ith_doc_date"]).Date.ToString("dd/MMM/yyyy");
                                    _msg += System.Environment.NewLine + "Manual Ref : " + row["ith_manual_ref"].ToString();
                                    _msg += System.Environment.NewLine + "Remarks : " + row["ith_remarks"].ToString();

                                    SystemInformationMessage(_msg, "More Details");
                                }
                            }
                            else
                            {
                                SystemInformationMessage("No Details Found", "Sales Details Missing");
                            }
                        }
                        btnAdd.Enabled = false;
                        btnAddAdditional.Enabled = false;
                        return true;
                    }
                }
            }

            if (_taskDir == "W")     //kapila 25/1/2016
            {
                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem("", lblItem.Text);
                if (_itemdetail.Mi_is_ser1 == 1)
                {
                    if (lblSerNo.Text != "N/A")
                    {
                        DataTable _seltbl = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", lblItem.Text, lblSerNo.Text);
                        if (_seltbl.Rows.Count > 0)
                        {
                            SystemInformationMessage("This item is not a customer item, Serial already available in location :  " + _seltbl.Rows[0]["ins_loc"].ToString() + " document date :" + Convert.ToDateTime(_seltbl.Rows[0]["ins_doc_dt"].ToString()).Date + " document # :" + _seltbl.Rows[0]["ins_doc_no"].ToString(), "Serial available");
                            btnAdd.Enabled = false;
                            btnAddAdditional.Enabled = false;
                            return true;
                        }

                        //DataTable _seltblscm = CHNLSVC.Inventory.CheckSerialAvailabilityscm(lblItem.Text, lblSerNo.Text);
                        //if (_seltblscm.Rows.Count > 0)
                        //{
                        //    SystemInformationMessage("This item is not a customer item, Serial already available in location :  " + _seltblscm.Rows[0]["location_code"].ToString() + " document date :" + Convert.ToDateTime(_seltblscm.Rows[0]["inv_date"].ToString()).Date + " document :# " + _seltblscm.Rows[0]["doc_ref_no"].ToString(), "Serial available");
                        //    btnAdd.Enabled = false;
                        //    btnAddAdditional.Enabled = false;
                        //    return true;
                        //}
                    }
                }
            }
            return false;

        }

        private void btn_TechJobPrint_Click(object sender, EventArgs e)
        {
            Reports.Service.ReportViewerSVC _viewJob = new Reports.Service.ReportViewerSVC();
            Reports.Service.clsServiceRep objSvc = new Reports.Service.clsServiceRep();

            BaseCls.GlbReportTp = "TECH_" + txtTaskLoc.Text;
            BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
            BaseCls.GlbReportDoc = txtReqNo.Text;
            _viewJob.Show();
            _viewJob = null;

        }

        private void lblWarRem_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblWarRem, lblWarRem.Text);
        }

        private void btnInsDet_Click(object sender, EventArgs e)
        {
            pnlIns.Visible = true;
        }

        private void btnCloseIns_Click(object sender, EventArgs e)
        {
            pnlIns.Visible = false;
        }

        private void btn_srch_brand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEBrand;
            _CommonSearch.txtSearchbyword.Text = txtEBrand.Text;
            _CommonSearch.ShowDialog();
            txtEBrand.Focus();
        }

        private void txtEBrand_Leave(object sender, EventArgs e)
        {
            MasterItemBrand _brd = new MasterItemBrand();
            if (!string.IsNullOrEmpty(txtEBrand.Text))
            {
                _brd = CHNLSVC.Sales.GetItemBrand(txtEBrand.Text);
                if (_brd.Mb_cd == null)
                {
                    SystemInformationMessage("Invalid Brand code", "External Item");
                    txtEBrand.Text = "";
                    txtEBrand.Focus();
                    return;
                }
            }
        }

        private void txtReqNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchChassiNo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                //Updated by akila 2017/05/02
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    _warrSearchtp = "CHASSIS #";
                    _warrSearchorder = "OTHER";
                }
                else
                {
                    _warrSearchtp = _scvParam.SP_DB_SERIAL;
                    _warrSearchorder = "OTHER";
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChassiNo;
                _CommonSearch.ShowDialog();
                txtChassiNo.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtChassiNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearchChassiNo_Click(null, null);
        }

        private void txtChassiNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchChassiNo_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btn_add_ser.Focus();
            }
        }

        private void txtRegNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_regno_Click(null, null);
        }

        private void txtRegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_regno_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btn_add_ser.Focus();
            }
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSer.Text))// ad by akila
            {
                return;
            }

            if (!string.IsNullOrEmpty(txtRegNo.Text))
            {
                //kapila 27/3/2015
                if (_scvParam.sp_is_job_by_rcc == 1)   //must open with rcc if exists
                {
                    DataTable _dt = CHNLSVC.Inventory.getRCCbySerialWar(null, txtRegNo.Text);
                    if (_dt.Rows.Count > 0)
                    {
                        optReq.Checked = true;
                        txtReqNo.Text = _dt.Rows[0]["inr_no"].ToString();
                        txtReqNo_Leave(null, null);
                        return;
                    }
                }

                Load_Serial_Infor(txtRegNo, string.Empty, dtDate.Value.Date);
            }
        }

        private void txtChassiNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChassiNo.Text))
            {
                //kapila 27/3/2015
                if (_scvParam.sp_is_job_by_rcc == 1)   //must open with rcc if exists
                {
                    DataTable _dt = CHNLSVC.Inventory.getRCCbySerialWar(null, txtChassiNo.Text);
                    if (_dt.Rows.Count > 0)
                    {
                        optReq.Checked = true;
                        txtReqNo.Text = _dt.Rows[0]["inr_no"].ToString();
                        txtReqNo_Leave(null, null);
                        return;
                    }
                }

                Load_Serial_Infor(txtChassiNo, string.Empty, dtDate.Value.Date);
            }
        }

        private void btnAddSupervisor_Click(object sender, EventArgs e)
        {
            if (grvJobItms.Rows.Count <= 0)
            { SystemInformationMessage("Please select a job item.", "Job Item"); return; }

            //if (grvTech.Rows.Count < 1)
            //{ SystemInformationMessage("Please enter the technician.", "Employee"); return; }

            if (string.IsNullOrEmpty(txtSupervisor.Text))
            { SystemInformationMessage("Please enter the supervisor.", "Employee"); return; }

            if (AlocatedSupervicerList != null && AlocatedSupervicerList.Count > 0)
            {
                int _count = AlocatedSupervicerList.Where(x => x.Stas_Emp_Cd == txtSupervisor.Text.Trim()).Count();
                if (_count > 0)
                { SystemInformationMessage("Supervisor code already Added", "Supervisor"); return; }
            }

            ServiceTechAlocSupervice _supervisor = new ServiceTechAlocSupervice();
            _supervisor.Stas_Com_Cd = BaseCls.GlbUserComCode;
            _supervisor.Stas_Loc_Cd = BaseCls.GlbUserDefLoca;
            _supervisor.Stas_Emp_Cd = txtSupervisor.Text.Trim();
            _supervisor.Stas_Supervice_Name = lblSupervisorName.Text;
            _supervisor.Stas_Status = "A";
            _supervisor.Stas_Cre_By = BaseCls.GlbUserID;
            _supervisor.Stas_Mod_By = BaseCls.GlbUserID;
            _supervisor.Stas_Session_Id = BaseCls.GlbUserSessionID;
            AlocatedSupervicerList.Add(_supervisor);

            if (AlocatedSupervicerList.Count > 0)
            {
                dgvSupervisor.Rows.Clear();
                BindingSource _source = new BindingSource();
                _source.DataSource = AlocatedSupervicerList.Select(x => new { x.Stas_Emp_Cd, x.Stas_Supervice_Name }).ToList();
                dgvSupervisor.DataSource = _source;
                txtEmpCode.Focus();
            }



        }

        private void btnSearchSupervisor_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ADVISOR);
                _result = CHNLSVC.CommonSearch.SearchServiceAdvisor(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSupervisor;
                _CommonSearch.ShowDialog();
                txtSupervisor.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = DefaultCursor;
                MessageBox.Show("An error occurred while searching supervisor details" + Environment.NewLine + ex.Message, "Employee - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSupervisor_DoubleClick(object sender, EventArgs e)
        {
            btnSearchSupervisor_Click(null, null);
        }

        private void txtSupervisor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { btnAddSupervisor.Focus(); }
            else if (e.KeyCode == Keys.F2)
            { btnSearchSupervisor_Click(null, null); }
        }

        private void txtSupervisor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupervisor.Text))
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtSupervisor.Text.Trim());
                    if (dtTemp.Rows.Count > 0)
                    {
                        var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_epf") == txtSupervisor.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                dtTemp = new DataTable();
                                dtTemp = query.CopyToDataTable();
                                lblSupervisorName.Text = dtTemp.Rows[0]["esep_first_name"].ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct EPF number", "Employee - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSupervisor.Clear();
                            lblSupervisorName.Text = "";
                            txtSupervisor.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Supervisor details not found. Please enter correct EPF number", "Employee - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupervisor.Clear();
                        lblSupervisorName.Text = "";
                        txtSupervisor.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = DefaultCursor;
                MessageBox.Show("An error occurred while validating supervisor details" + Environment.NewLine + ex.Message, "Employee - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetRegistrationNumber(TextBox _txt)
        {
            string _registrationNo = string.Empty;
            string _ser1 = null;
            string _warr = null;
            string _regno = null;
            string _invcno = null;

            try
            {
                //Updated by akila 2017/05/03
                string _ser2 = null;
                string _item = null;
                string _tmpControlName = null; ;

                if (_txt == txtSer) { _ser1 = txtSer.Text.ToString().Trim(); _tmpControlName = lblSerSrch.Text; }
                if (_txt == txtWar) { _warr = txtWar.Text.ToString().Trim(); _tmpControlName = lblWarSrch.Text; }
                if (_txt == txtRegNo) { _regno = txtRegNo.Text.ToString().Trim(); _tmpControlName = lblRegNo.Text; }
                if (_txt == txtChassiNo) { _ser2 = txtChassiNo.Text.ToString().Trim(); _tmpControlName = lblChassiNo.Text; }


                List<InventorySerialMaster> _serialDetails = new List<InventorySerialMaster>();
                _serialDetails = CHNLSVC.CustService.GetServiceItemDetails(_item, _ser1, _ser2, _regno, _warr, _invcno, 0);
                if (_serialDetails != null)
                {
                    if (_serialDetails.Count > 0)
                    {
                        _registrationNo = _serialDetails.Select(x => x.Irsm_reg_no).First() == null ? string.Empty : _serialDetails.Select(x => x.Irsm_reg_no).First().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _registrationNo;
        }

        private void dgvSupervisor_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvSupervisor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    AlocatedSupervicerList.RemoveAt(e.RowIndex);
                    dgvSupervisor.Rows.Clear();
                    BindingSource _source = new BindingSource();
                    _source.DataSource = AlocatedSupervicerList.Select(x => new { x.Stas_Emp_Cd, x.Stas_Supervice_Name }).ToList();
                    dgvSupervisor.DataSource = _source;
                }
            }
        }

        private void btn_srch_ERegNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobRegNo);
            DataTable _result = CHNLSVC.CommonSearch.GetJobRegNoSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtERegNo;
            _CommonSearch.txtSearchbyword.Text = txtERegNo.Text;
            _CommonSearch.ShowDialog();
            txtERegNo.Focus();
            //txtRegNo.Text = txtERegNo.Text;
        }

        private void load_item_det_by_reg_no(string _regno)
        {
            // txtEItem.Text = "";
            DataTable _dt = new DataTable();

            //updated by akila 2017/06/19
            _dt = CHNLSVC.Sales.GetItemByJobRegNo(_regno);
            if (_dt.Rows.Count > 0)
            {
                //load item details
                DataView _tmpDataView = _dt.DefaultView;
                _tmpDataView.Sort = "sjb_dt desc";
                _dt = _tmpDataView.ToTable();

                if (string.IsNullOrEmpty(txtEItem.Text))
                {
                    txtEItem.Text = _dt.Rows[0]["jbd_itm_cd"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_itm_cd"].ToString();
                    if (!string.IsNullOrEmpty(txtEItem.Text))
                    {
                        txtEItemDesc.Text = _dt.Rows[0]["jbd_itm_desc"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_itm_desc"].ToString();
                        txtEModel.Text = _dt.Rows[0]["jbd_model"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_model"].ToString();
                        txtEBrand.Text = _dt.Rows[0]["jbd_brand"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_brand"].ToString();
                        lblItemCat.Text = _dt.Rows[0]["jbd_cate1"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_cate1"].ToString();
                        //txtMilage.Text = _dt.Rows[0]["jbd_milage"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_milage"].ToString(); //commented by akila 2017/07/1
                        txtESerial.Text = _dt.Rows[0]["jbd_ser1"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_ser1"].ToString();
                        txtESerial2.Text = _dt.Rows[0]["jbd_ser2"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_ser2"].ToString();

                        txtEItemDesc.ReadOnly = false;
                        txtEItemDesc.Enabled = true;
                        txtEBrand.ReadOnly = false;
                        btn_srch_brand.Enabled = true;

                        //Get supplier details
                        DataTable _supplierDt = new DataTable();
                        _supplierDt = CHNLSVC.Inventory.GetSuplierByItem(BaseCls.GlbUserComCode, txtEItem.Text.ToUpper());
                        if (_supplierDt.Rows.Count > 0)
                        {
                            txtSupplier.Text = _supplierDt.Rows[0]["mbii_cd"].ToString();
                            txtSupplier_Leave(null, null);
                        }
                        else { txtSupplier.Text = "N/A"; }
                    }
                    else
                    {
                        MessageBox.Show("Invalid item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtEItem.Focus();
                        return;
                    }
                }
                else { txtEItem_Leave(null, null); }

                //commented by akila 2017/06/22
                //txtEItem.Text = _dt.Rows[0]["jbd_itm_cd"] == DBNull.Value ? string.Empty : _dt.Rows[0]["jbd_itm_cd"].ToString();
                //txtEItem_Leave(null, null);

                //load cutomer details
                var _tmpCustomer = _dt.AsEnumerable().Where(x => x.Field<string>("sjb_cust_cd").ToString() != "CASH").Select(x => x.Field<string>("sjb_cust_cd"));
                if ((_tmpCustomer != null) && (_tmpCustomer.Count() > 0))
                {
                    txtCustCode.Text = _tmpCustomer.First().ToString();
                }
                else
                {
                    //MessageBox.Show("Cutomer details not found", "Load Cutomer Details - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustCode.Text = "CASH";
                }
            }
            else { MessageBox.Show("Records not found", "Load External Details - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            if ((!string.IsNullOrEmpty(txtCustCode.Text)) && (txtCustCode.Text != "CASH"))
            {
                LoadCustomerDetailsByCustomer();
                FillPriority();
                check_Blacklistcustomer();
            }
            //txtCustCode_Leave(null, null);



            //commented by akila 2017/06/19
            //if(_dt.Rows.Count>0)
            //{
            //    txtEItem.Text = _dt.Rows[0]["jbd_itm_cd"].ToString();
            //    txtEItem_Leave(null, null);
            //    DataTable _dtJobH = CHNLSVC.CustService.get_JobHeader(_dt.Rows[0]["jbd_jobno"].ToString());
            //    txtCustCode.Text = _dtJobH.Rows[0]["sjb_B_cust_cd"].ToString();
            //    txtCustCode_Leave(null, null);
            //}
            //txtRegNo.Text = _regno;
        }

        private void txtERegNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtERegNo.Text))
                load_item_det_by_reg_no(txtERegNo.Text);
        }

        private void txtERegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_ERegNo_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtEItemDesc.Focus();
            }
        }

        //Add by akila 2017/06/09
        private void LoadInvoiceItems()
        {
            try
            {
                if (dgvDOInvoiceItems.Rows.Count > 0)
                {
                    if (isAnySelectedDOItems())
                    {
                        foreach (DataGridViewRow _dataRow in dgvDOInvoiceItems.Rows)
                        {
                            if (_dataRow.Cells["selectD1"].Value != null && Convert.ToBoolean(_dataRow.Cells["selectD1"].Value) == true)
                            {
                                Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

                                int _returnStatus = 0;
                                string _returnMsg = string.Empty;
                                string _ser = _dataRow.Cells["ITS_SER_1D1"].Value == null ? string.Empty : _dataRow.Cells["ITS_SER_1D1"].Value.ToString(); ;

                                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, null, null, null, null, null, 0, out _returnStatus, out _returnMsg);
                                if (_warrMstDic != null)
                                {
                                    List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
                                    List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
                                    if (_returnStatus == 2)
                                    {
                                        _warStus = 0;
                                        lblWarStus.Text = "OVER WARRANTY";
                                    }

                                    foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                                    {
                                        _warrMst = pair.Key;
                                        _warrMstSub = pair.Value;
                                    }

                                    if (_warrMst.Count > 0)
                                    {
                                        FillItemDetails(_warrMst[0], dtDate.Value.Date, _returnStatus);
                                        add_job_item(1, null);
                                    }
                                }
                            }
                        }

                        check_Blacklistcustomer();
                        txtDef.Focus();
                        pnlDOInvoiceItems.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemInformationMessage("Error occurred while loading invoice item details" + Environment.NewLine + ex.Message, "Load Invoice Item Details");
                return;
            }
        }

        private void txtERegNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_ERegNo_Click(null, null);
        }

        private void txtEItemDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEModel.Focus();
            }
        }

        private void txtEModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEBrand.Focus();
            }
        }

        private void txtEBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtESerial.Focus();
            }
        }

        private void txtESerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEWarr.Focus();
            }
        }

        private void txtESerial_Leave(object sender, EventArgs e)
        {
            //add by akila 2017/06/24
            if ((!string.IsNullOrEmpty(txtESerial.Text)) && (txtESerial.Text != "N/A"))
            {
                LoadItemDEtails_bySerial(txtESerial.Text);
            }
        }

        //add by akila 2017/06/24
        private void LoadItemDEtails_bySerial(string serialNo)
        {
            try
            {
                List<InventorySerialMaster> _serialDetails = new List<InventorySerialMaster>();
                _serialDetails = CHNLSVC.CustService.GetServiceItemDetails(null, txtESerial.Text, null, null, null, null, 0);
                if ((_serialDetails != null) && (_serialDetails.Count > 0))
                {
                    txtEItem.Text = _serialDetails[0].Irsm_itm_cd;
                    if (!string.IsNullOrEmpty(txtEItem.Text))
                    {
                        List<Service_job_Det> _serviceDetails = new List<Service_job_Det>();
                        _serviceDetails = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(serialNo, _serialDetails[0].Irsm_itm_cd, BaseCls.GlbUserComCode);
                        _serviceDetails = _serviceDetails.OrderByDescending(x => x.Jbd_seq_no).ToList();
                        if ((_serviceDetails != null) && (_serviceDetails.Count > 0))
                        {
                            txtEItemDesc.Text = _serviceDetails[0].Jbd_itm_desc;
                            txtEModel.Text = _serviceDetails[0].Jbd_model;
                            txtEBrand.Text = _serviceDetails[0].Jbd_brand;
                            lblItemCat.Text = _serviceDetails[0].Jbd_cate1;
                            // txtMilage.Text = _serviceDetails[0].Jbd_milage.ToString();
                            txtERegNo.Text = _serviceDetails[0].Jbd_regno;
                            txtESerial2.Text = _serviceDetails[0].Jbd_ser2;

                            txtEItemDesc.ReadOnly = false;
                            txtEItemDesc.Enabled = true;
                            txtEBrand.ReadOnly = false;
                            btn_srch_brand.Enabled = true;
                        }
                        else { txtEItem_Leave(null, null); }

                        var _tmpCustomers = _serialDetails.Where(x => x.Irsm_cust_cd != null || x.Irsm_cust_cd != "N/A" || x.Irsm_cust_cd != "CASH").Select(x => x.Irsm_cust_cd).ToList();
                        if (_tmpCustomers != null)
                        {
                            txtCustCode.Text = _tmpCustomers.First().ToString();
                        }
                        else { txtCustCode.Text = "CASH"; }

                        LoadCustomerDetailsByCustomer();
                        FillPriority();
                        check_Blacklistcustomer();
                    }
                    else
                    {
                        MessageBox.Show("Item code not found. Please select a valid item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtEItem.Focus();
                        return;
                    }
                }
                else { MessageBox.Show("Serial details not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                Cursor = DefaultCursor;
                MessageBox.Show("Error occurred while loading serial details" + Environment.NewLine + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvReqItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (grvReqItems.Rows.Count > 0)
                {
                    bool _isRowSelected = false;
                    bool.TryParse(grvReqItems.Rows[e.RowIndex].Cells["Jrd_Select"].Value.ToString(), out _isRowSelected);
                    if (_isRowSelected)
                    {
                        grvReqItems.Rows[e.RowIndex].Cells["Jrd_Select"].Value = false;
                    }
                    else
                    {
                        grvReqItems.Rows[e.RowIndex].Cells["Jrd_Select"].Value = true;
                    }
                }
            }
        }

        private void btnPriority_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10098))
            {
                MessageBox.Show("Sorry, You have no permission to set the priority level!\n( Advice: Required permission code :10098 )");
                return;
            }
            if (string.IsNullOrEmpty(txtCustCode.Text))
            {
                MessageBox.Show("Customer code not found. Please select the Customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            BindHeirachy();
            if (DropDownListPartyTypes.SelectedValue == "LOC")
            {
                txtHierchCode.Text = BaseCls.GlbUserDefLoca;
            }

            this.pnlPriority.Size = new System.Drawing.Size(424, 164);
            this.pnlPriority.Location = new System.Drawing.Point(526, 76);
            pnlPriority.Visible = true;
        }
        private void BindHeirachy()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("LOC", "Location");
            PartyTypes.Add("SCHNL", "Sub Channel");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }
        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrioritynew.Text))
            {
                MessageBox.Show("Select the priority level !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtHierchCode.Text))
            {
                MessageBox.Show("Select the location/Sub channel !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, txtHierchCode.Text);
                if (loc_list == null)
                {
                    MessageBox.Show("Invalid Location Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            foreach (DataGridViewRow row in grvParty.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(DropDownListPartyTypes.SelectedValue.ToString()) && row.Cells[2].Value.ToString().Equals(txtHierchCode.Text) && row.Cells[3].Value.ToString().Equals(txtPrioritynew.Text))
                    return;
            }

            MST_BUSPRIT_LVL _PrtLevel = new MST_BUSPRIT_LVL();
            _PrtLevel.Mbl_act = Convert.ToInt32(chkAct.Checked);
            _PrtLevel.Mbl_cd = txtCustCode.Text;
            _PrtLevel.Mbl_com = BaseCls.GlbUserComCode;
            _PrtLevel.Mbl_cre_by = BaseCls.GlbUserID;
            _PrtLevel.Mbl_mod_by = BaseCls.GlbUserID;
            _PrtLevel.Mbl_prit_cd = txtPrioritynew.Text;
            _PrtLevel.Mbl_pty_cd = txtHierchCode.Text;
            _PrtLevel.Mbl_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
            _PrtLevel.SCP_COLOR = txtPrioritynew.Text;
            _PrtLevel.SCP_DESC = txtPrioritynew.Text;
            _PriorityLvlList.Add(_PrtLevel);

            BindingSource _source = new BindingSource();
            _source.DataSource = _PriorityLvlList;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _source;

            txtPriority.Text = "";
            txtHierchCode.Text = "";

        }

        private void btnUpdProty_Click(object sender, EventArgs e)
        {
            if (_PriorityLvlList.Count == 0)
            {
                MessageBox.Show("Select the priority level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.Sales.SaveCustomerPriorityLevel_jonentry(_PriorityLvlList, txtCustCode.Text);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _PriorityLvlList = new List<MST_BUSPRIT_LVL>();
                pnlPriority.Visible = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _PriorityLvlList;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _source;
                FillPriority();
            }

        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();

                if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "LOC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                }


                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
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

        private void btn_srch_prionew_Click(object sender, EventArgs e)
        {

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePrioritybycust);
            DataTable _result = CHNLSVC.CommonSearch.GET_PRIT_BY_CUST(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPrioritynew;
            _CommonSearch.ShowDialog();
            txtPrioritynew.Focus();
        }

        private void txtPrioritynew_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
                btn_srch_prionew_Click(null, null);
        }

        private void txtPrioritynew_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_prionew_Click(null, null);
        }

        private void btnPrioClose_Click(object sender, EventArgs e)
        {
            pnlPriority.Visible = false;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListPartyTypes.SelectedValue == "LOC")
            {
                txtHierchCode.Text = BaseCls.GlbUserDefLoca;
            }
            if (DropDownListPartyTypes.SelectedValue == "SCHNL")
            {
                txtHierchCode.Text = BaseCls.GlbDefSubChannel;
            }
        }

        private void btnAdvnceSrch_Click(object sender, EventArgs e)
        {
            try
            {
                Services.ServiceAdvanceSearch _advanceSerch = new Services.ServiceAdvanceSearch(txtSer.Text.Trim(), _scvParam);
                // _inventoryTracker.MdiParent = this;
                _advanceSerch.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }


    }
}