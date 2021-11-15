using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Sales;

namespace FF.WindowsERPClient.Services
{
    public partial class Service_Request : Base
    {
        #region Global variables

        private Service_Chanal_parameter _scvParam = null;

        //_scvParam.SP_DB_SERIAL;
        private Service_Req_Hdr _scvjobHdr = null;

        private List<Service_Req_Det> _scvItemList = null;
        private List<Service_Req_Def> _scvDefList = null;
        private List<Service_Tech_Aloc_Hdr> _scvEmpList = null;
        private List<Service_Req_Det_Sub> _scvItemSubList = null;
        private List<Service_Req_Det_Sub> _tempItemSubList = null;
        private List<Service_TempIssue> _scvStdbyList = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private List<Service_Req_Det> _reqDet = null;
        private MasterLocation _scvLoc = null;
        private Service_Req_Hdr _reqHdr = null;
        private int _jobRecall = 0;
        private int _jobRecallSeq = 0;
        private bool _isBlack = false;
        private string _isBlackInfor = string.Empty;

        private Int32 _warStus = 0;
        private string _itmBrand = "";
        private Int32 _jobItemLine = 1;
        private string _jobStage = "2_12";

        private string _warrSearchtp = string.Empty;
        private string _warrSearchorder = string.Empty;
        private Boolean _isGroup = false;
        private string _chgJobStage = string.Empty;
        private Boolean _isJobOpen = false;
        private string _reqSubTp = "";
        //InventorySerialMaster VehicleObect;
        //List<ServiceJobDefect> Defect_List;
        //int Defect_LineNo;
        //int IsService;
        //int IsUpdateShedule;
        //int ServiceTerm;

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
                case CommonUIDefiniton.SearchUserControlType.ServiceRequests:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1" + seperator);
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + _scvParam.SP_SERCHNL + seperator + txtTaskLoc.Text + seperator + "1" + seperator + dtDate.Value.Date.ToShortDateString() + seperator + "1.1" + seperator);
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

        public Service_Request()
        {
            //try
            //{
            Clear();
            _scvjobHdr = new Service_Req_Hdr();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();
            //}
            //catch (Exception ex)
            //{ SystemErrorMessage(ex); }
        }

        private void Clear()
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0) Controls[0].Dispose();
            InitializeComponent();

            _jobRecall = 0;
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
            pnlItem.Size = new Size(334, 294);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            pnlHistory.Size = new Size(775, 504);
            pnlPaymode.Size = new Size(990, 222);
            ChkExternal.Enabled = false;
            //pnlInspection.Enabled = false;
            LoadPayMode();

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = true;

            optReq.Enabled = true;
            optInspection.Enabled = true;
            optJob.Enabled = true;
            btnPrint.Visible = false;

            _scvjobHdr = new Service_Req_Hdr();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

            grvJobItms.DataSource = null;
            grvDef.DataSource = null;
            grvTech.DataSource = null;

            txtReqLineNo.Text = "0";
            if (_scvParam != null)
            {
                lblSerSrch.Text = _scvParam.SP_DB_SERIAL;
                lblRegNo.Text = _scvParam.SP_DB_CHASSIS;
                txtCustCode.Text = _scvParam.SP_EXTERNALCUST;
                txtTaskLoc.Text = _scvParam.SP_DEF_CAT;
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
            }

            pnlCustBill.Enabled = true;
            btnNewCust.Enabled = true;
            chkEditCustomer.Enabled = true;
            this.Cursor = Cursors.Default;

            pnlWarr.Height = 128;
            pnlDOInvoiceItems.Size = new Size(508, 236);
            pnlDOInvoiceItems.Visible = false;

            _jobItemLine = 1;
        }

        private void ClearScreen()
        {
            this.Cursor = Cursors.WaitCursor;

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
            pnlItem.Size = new Size(334, 294);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            pnlHistory.Size = new Size(775, 504);

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = true;

            _scvjobHdr = new Service_Req_Hdr();
            _reqHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

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
            pnlItem.Size = new Size(334, 294);
            pnlAdditionalItems.Size = new Size(702, 163);
            pnlReq.Size = new Size(890, 207);
            ChkExternal.Enabled = false;
            //pnlInspection.Enabled = false;

            //lblReqRmk.Visible = true;
            txtReqRmk.Visible = true;

            _scvjobHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

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
            if (string.IsNullOrEmpty(txtEWarPrd.Text) || string.IsNullOrEmpty(txtESerial.Text) || string.IsNullOrEmpty(txtEWarr.Text) || string.IsNullOrEmpty(txtEItem.Text) || string.IsNullOrEmpty(txtEItemDesc.Text) || string.IsNullOrEmpty(txtEModel.Text) || string.IsNullOrEmpty(txtEBrand.Text))
            {
                MessageBox.Show("Item/Description/Model/Brand/Serial/Warranty cannot be blank !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            txtDef.Focus();
            pnlItem.Visible = false;
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
            lblWarNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblWarPrd.Text = "";
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
            _itmBrand = "";
            _warStus = 0;

            lblPartNo.Text = "";
        }

        private void Set_Job_Item()
        {
            Service_Req_Det _jobDt = new Service_Req_Det();
            _jobDt.Jrd_seq_no = 0;
            _jobDt.Jrd_reqno = "";
            _jobDt.Jrd_reqline = 1;
            _jobDt.Jrd_sjobno = "";
            _jobDt.Jrd_loc = BaseCls.GlbUserDefLoca;
            _jobDt.Jrd_pc = BaseCls.GlbUserDefProf;
            _jobDt.Jrd_itm_cd = _scvParam.SP_INSPECTION_ITM;
            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _jobDt.Jrd_itm_cd);
            _jobDt.Jrd_itm_stus = "SER";
            _jobDt.Jrd_itm_desc = _item.Mi_shortdesc;
            _jobDt.Jrd_brand = _item.Mi_brand;
            _jobDt.Jrd_model = _item.Mi_model;
            _jobDt.Jrd_itm_cost = 0;
            _jobDt.Jrd_ser1 = "N/A";
            _jobDt.Jrd_ser2 = "N/A";
            _jobDt.Jrd_warr = "N/A";
            _jobDt.Jrd_regno = "N/A";
            _jobDt.Jrd_milage = 0;
            _jobDt.Jrd_warr_stus = 0;
            _jobDt.Jrd_onloan = 0;
            _jobDt.Jrd_availabilty = 1;
            _jobDt.Jrd_msnno = "";
            _jobDt.Jrd_itmtp = _item.Mi_itm_tp;
            //   _jobDt.Jrd_cate1 = _item.Mi_cate_1;
            _jobDt.Jrd_custnotes = "";
            _jobDt.Jrd_isstockupdate = 0;
            _jobDt.Jrd_needgatepass = 0;
            _jobDt.Jrd_iswrn = 0;
            _jobDt.Jrd_warrperiod = 0;
            _jobDt.Jrd_warrrmk = "N/A";
            _jobDt.Jrd_warrreplace = 0;
            _jobDt.Jrd_date_pur = dtDate.Value.Date;
            _jobDt.Jrd_invc_no = "";
            _jobDt.Jrd_invc_showroom = _scvLoc.Ml_loc_desc;
            _jobDt.Jrd_isexternalitm = 0;
            _jobDt.Jrd_conf_desc = "";
            _jobDt.Jrd_isagreement = "0";
            _jobDt.Jrd_stage = Convert.ToInt32(1);
            _jobDt.Jrd_com = BaseCls.GlbUserComCode;
            _jobDt.Jrd_ser_id = "0";
            // _jobDt.Jrd_supp_cd = "";
            // _jobDt.Jrd_act = 0;

            _scvItemList.Add(_jobDt);

            _jobItemLine = _jobItemLine + 1;

            txtSer.Clear();
            txtWar.Clear();
            txtRegNo.Clear();

            lblItem.Text = _jobDt.Jrd_itm_cd;
            lblItemDesc.Text = _jobDt.Jrd_itm_desc;
            lblSerNo.Text = _jobDt.Jrd_ser1;
            lblWarNo.Text = _jobDt.Jrd_warr;
            lblModel.Text = _jobDt.Jrd_model;
            lblBrand.Text = _jobDt.Jrd_brand;
            //  lblItemCat.Text = _jobDt.Jrd_cate1;

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_Req_Det>();
            grvJobItms.DataSource = _scvItemList;
            grvJobItms.Refresh();
        }

        private void add_job_item(int _active, Service_Req_Det _reqItm)
        {
            Service_Req_Det _jobDt = new Service_Req_Det();
            _jobDt.Jrd_seq_no = 0;
            _jobDt.Jrd_reqno = "";
            _jobDt.Jrd_reqline = _jobItemLine;
            _jobDt.Jrd_sjobno = "";
            _jobDt.Jrd_loc = BaseCls.GlbUserDefLoca;
            _jobDt.Jrd_pc = BaseCls.GlbUserDefProf;
            _jobDt.Jrd_itm_cd = lblItem.Text;
            _jobDt.Jrd_itm_stus = "SER";
            _jobDt.Jrd_itm_desc = lblItemDesc.Text;
            _jobDt.Jrd_brand = lblBrand.Text;
            _jobDt.Jrd_model = lblModel.Text;
            _jobDt.Jrd_itm_cost = 0;
            _jobDt.Jrd_ser1 = lblSerNo.Text;
            _jobDt.Jrd_ser2 = "N/A";
            _jobDt.Jrd_warr = lblWarNo.Text;
            _jobDt.Jrd_regno = lblRegNo.Text;

            if (txtMilage.Text.Length > 0)
            {
                _jobDt.Jrd_milage = Convert.ToInt32(txtMilage.Text.ToString());
            }

            _jobDt.Jrd_warr_stus = (lblWarStus.Text == "UNDER WARRANTY") ? 1 : 0;
            _jobDt.Jrd_onloan = 0;
            //_jobDt.Jrd_chg_warr_stdt = "";
            //_jobDt.Jrd_chg_warr_rmk = "";
            //_jobDt.Jrd_isinsurance = "";
            //_jobDt.Jrd_req_tp = "";
            //_jobDt.Jrd_ser_term = "";
            //_jobDt.Jrd_lastwarr_stdt = "";
            //_jobDt.Jrd_issued = "";
            //_jobDt.Jrd_mainitmcd = "";
            //_jobDt.Jrd_mainitmser = "";
            //_jobDt.Jrd_mainitmwarr = "";
            //_jobDt.Jrd_itmmfc = "";   
            //_jobDt.Jrd_mainitmmfc = "";
            _jobDt.Jrd_availabilty = 1;
            //_jobDt.Jrd_usejob = "";
            _jobDt.Jrd_msnno = txtCoupon.Text; //MSN No or Coupon No
            _jobDt.Jrd_itmtp = txtItmTp.Text;
            //  _jobDt.Jrd_reqline = Convert.ToInt32(txtReqLineNo.Text.ToString());
            _jobDt.Jrd_sentwcn = 0;
            //_jobDt.Jrd_serlocchr = "";
            _jobDt.Jrd_custnotes = "";
            //_jobDt.Jrd_mainreqno = "";
            //_jobDt.Jrd_mainreqloc = "";
            //_jobDt.Jrd_mainjobno = "";
            //_jobDt.Jrd_reqitmtp = "";
            _jobDt.Jrd_reqno = txtReqNo.Text;
            // if (_reqItm != null) _jobDt.Jrd_reqline = _reqItm.Jrd_reqline;
            _jobDt.Jrd_isstockupdate = 0;
            if (_reqItm != null) _jobDt.Jrd_isstockupdate = _reqItm.Jrd_isstockupdate;
            _jobDt.Jrd_needgatepass = 0;
            _jobDt.Jrd_iswrn = 0;
            _jobDt.Jrd_warrperiod = Convert.ToInt32(lblWarPrd.Text);
            _jobDt.Jrd_warrrmk = lblWarRem.Text;
            _jobDt.Jrd_warrstartdt = Convert.ToDateTime(lblWarStart.Text);
            _jobDt.Jrd_warrreplace = 0;
            _jobDt.Jrd_date_pur = Convert.ToDateTime(lblInvDt.Text).Date;
            _jobDt.Jrd_invc_no = lblInv.Text;
            //_jobDt.Jrd_waraamd_seq = "";
            //_jobDt.Jrd_waraamd_by = "";
            //_jobDt.Jrd_waraamd_dt = "";
            _jobDt.Jrd_invc_showroom = lblDelLoc.Text;
            if (_reqItm != null) _jobDt.Jrd_aodissueloc = _reqItm.Jrd_aodissueloc;
            if (_reqItm != null) _jobDt.Jrd_aodissuedt = _reqItm.Jrd_aodissuedt;
            if (_reqItm != null) _jobDt.Jrd_aodissueno = _reqItm.Jrd_aodissueno;
            //_jobDt.Jrd_aodrecno = "";
            //_jobDt.Jrd_techst_dt = "";
            //_jobDt.Jrd_techfin_dt = "";
            //_jobDt.Jrd_msn_no = "";
            _jobDt.Jrd_isexternalitm = (ChkExternal.Checked == true) ? 1 : 0;
            //_jobDt.Jrd_conf_dt = "";
            //_jobDt.Jrd_conf_cd = "";
            _jobDt.Jrd_conf_desc = "";
            //_jobDt.Jrd_conf_rmk = "";
            //_jobDt.Jrd_tranf_by = "";
            //_jobDt.Jrd_tranf_dt = "";
            //_jobDt.Jrd_do_invoice = 0;
            //_jobDt.Jrd_insu_com = "";
            //_jobDt.Jrd_agreeno = "";
            //_jobDt.Jrd_issrn = 0;
            _jobDt.Jrd_isagreement = "0";
            //_jobDt.Jrd_cust_agreeno = "";
            //_jobDt.Jrd_quo_no = "";
            _jobDt.Jrd_stage = 1;
            _jobDt.Jrd_com = BaseCls.GlbUserComCode;
            _jobDt.Jrd_ser_id = "0";
            if (_reqItm != null) if (!string.IsNullOrEmpty(_reqItm.Jrd_ser_id)) if (_reqItm.Jrd_ser_id != "0") _jobDt.Jrd_ser_id = _reqItm.Jrd_ser_id;
            //_jobDt.Jrd_techst_dt_man = "";
            //_jobDt.Jrd_techfin_dt_man = "";
            //_jobDt.Jrd_reqwcn = "";
            //_jobDt.Jrd_reqwcndt = "";
            //_jobDt.Jrd_reqwcnsysdt = "";
            //_jobDt.Jrd_sentwcn = "";
            //_jobDt.Jrd_recwcn = "";
            //_jobDt.Jrd_takewcn = "";
            //_jobDt.Jrd_takewcndt = "";
            //_jobDt.Jrd_takewcnsysdt = "";
            //_jobDt.Jrd_part_cd = "";
            //_jobDt.Jrd_oem_no = "";
            //_jobDt.Jrd_case_id = "";
            //_jobDt.Jrd_oldjobline = "";
            //_jobDt.Jrd_tech_rmk = "";
            //_jobDt.Jrd_tech_custrmk = "";
            //_jobDt.StageText = "";

            _scvItemList.Add(_jobDt);

            _jobItemLine = _jobItemLine + 1;

            txtSer.Clear();
            txtWar.Clear();
            txtRegNo.Clear();

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_Req_Det>();
            grvJobItms.DataSource = _scvItemList;
            grvJobItms.Refresh();
        }

        private void grvJobItms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 _line = Convert.ToInt32(grvJobItms.Rows[e.RowIndex].Cells["JRD_REQLINE"].Value);

                    if (optInspection.Checked == true)
                    {
                        if (_scvParam.SP_INSPECTION_ITM == grvJobItms.Rows[e.RowIndex].Cells["JBD_ITM_CD"].Value.ToString())
                        {
                            SystemInformationMessage("Can't remove inspection item", "Inspection Job");
                            return;
                        }
                    }

                    //remove defects related to this item line
                    List<Service_Req_Def> _temp = new List<Service_Req_Def>();
                    _temp = _scvDefList;
                    _temp.RemoveAll(x => x.Srdf_req_line == _line);
                    _scvDefList = _temp;

                    //remove Sub Items related to this item line
                    List<Service_Req_Det_Sub> _tempSub = new List<Service_Req_Det_Sub>();
                    _tempSub = _scvItemSubList;
                    _tempSub.RemoveAll(x => x.Jrds_jobline == _line);
                    _scvItemSubList = _tempSub;

                    //remove Allocate Emp related to this item line
                    List<Service_Tech_Aloc_Hdr> _tempEmp = new List<Service_Tech_Aloc_Hdr>();
                    _tempEmp = _scvEmpList;
                    _tempEmp.RemoveAll(x => x.STH_JOBLINE == _line);
                    _scvEmpList = _tempEmp;

                    grvDef.AutoGenerateColumns = false;
                    grvDef.DataSource = new List<Service_Req_Def>();
                    grvDef.DataSource = _scvDefList;

                    //remove item line
                    _scvItemList.RemoveAt(e.RowIndex);

                    grvJobItms.AutoGenerateColumns = false;
                    grvJobItms.DataSource = new List<Service_Req_Det>();
                    grvJobItms.DataSource = _scvItemList;

                    load_item_det(0);
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
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.obj_TragetTextBox = txtDef;
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
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "SER";
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
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void btn_add_ser_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(lblItem.Text))
            {
                SystemInformationMessage("Please enter the job item details!", "Item not found");
                return;
            }

            if (lblSerNo.Text.ToUpper() != "N/A")
            {
                Int32 _count = _scvItemList.Where(X => X.Jrd_itm_cd == lblItem.Text && X.Jrd_ser1 == lblSerNo.Text).Count();
                if (_count > 0)
                {
                    SystemInformationMessage("Already added this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                    return;
                }

                //List<Service_Req_Det> _pendingjobdetList = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(lblSerNo.Text, lblItem.Text, BaseCls.GlbUserComCode);
                //_count = _pendingjobdetList.Where(X => X.Jrd_stage >= Convert.ToDecimal(1.1) && X.Jrd_stage <= 8).Count();
                //if (_count > 0)
                //{
                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                //    return;
                //}
                //_count = _pendingjobdetList.Where(X => X.Jrd_stage >= 13 && X.Jrd_stage <= 14).Count();
                //if (_count > 0)
                //{
                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                //    return;
                //}
            }
            if (lblWarNo.Text.ToUpper() != "N/A")
            {
                Int32 _count = _scvItemList.Where(X => X.Jrd_warr == lblWarNo.Text).Count();
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
            //    List<Service_Req_Def> oItems = CHNLSVC.CustService.GetRequestJobDefectsJobEnty(txtReqNo.Text, _jobItemLine);
            //    grvDef.DataSource = new List<Service_Req_Def>();
            //    grvDef.DataSource = oItems;
            //}
        }

        private void txtESerial_Leave(object sender, EventArgs e)
        {
            //check job found for this serial -TODO
        }

        private void btnCloseEItm_Click(object sender, EventArgs e)
        {
            pnlItem.Visible = false;
            if (optInspection.Checked == false) ChkExternal.Checked = false;
        }

        private void grvJobItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["jrd_select"].Value = false;
                }
                grvJobItms.Rows[e.RowIndex].Cells["jrd_select"].Value = true;

                if (!string.IsNullOrEmpty(txtReqNo.Text))
                {
                    txtSer.Text = grvJobItms.Rows[e.RowIndex].Cells["JrD_SER1"].Value.ToString();
                    Load_Serial_Infor(txtSer, string.Empty, dtDate.Value.Date);
                }
                else
                    load_item_det(e.RowIndex);
            }
        }

        private void load_item_det(Int32 _index)
        {
            if (_scvItemList != null)
            {
                if (_scvItemList.Count > 0)
                {
                    Service_Req_Det _item = _scvItemList[_index];
                    lblItem.Text = _item.Jrd_itm_cd;
                    lblItemDesc.Text = _item.Jrd_itm_desc;
                    lblSerNo.Text = _item.Jrd_ser1;
                    lblWarNo.Text = _item.Jrd_warr;
                    lblModel.Text = _item.Jrd_model;
                    lblBrand.Text = _item.Jrd_brand;
                    txtJobItemLine.Text = _item.Jrd_reqline.ToString();
                    //lblItemCat.Text = _item.jrd_cate1;

                    string _warrStatus = string.Empty;
                    if (_item.Jrd_warr_stus == 1) _warrStatus = "UNDER WARRANTY"; else _warrStatus = "OVER WARRANTY";

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
                        grvJobItms.Rows[i].Cells["Jrd_select"].Value = true;
                    }
                    load_item_det(0);

                    Int32 _jbLine1 = Convert.ToInt32(txtJobItemLine.Text);
                    Int32 _count1 = _scvDefList.Where(X => X.Srdf_req_line == _jbLine1 && X.Srdf_def_tp == txtDef.Text).Count();
                    if (_count1 > 0)
                    {
                        SystemInformationMessage("Defect type already added into the list!", "Defect Type");
                        return;
                    }

                    Service_Req_Def _jobDef = new Service_Req_Def();
                    _jobDef.Srdf_def_line = 1;
                    _jobDef.Srdf_def_tp = txtDef.Text;
                    _jobDef.Srdf_def_rmk = txtDefRem.Text;
                    _jobDef.Srdf_req_line = Convert.ToInt32(txtJobItemLine.Text);
                    _jobDef.jrd_itm_cd = lblItem.Text;
                    _jobDef.jrd_ser1 = lblSerNo.Text;
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
                        if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jrd_select"].Value) == true)
                        {
                            _isSelect = true;
                            load_item_det(i);
                            Service_Req_Def _jobDef = new Service_Req_Def();
                            _jobDef.Srdf_def_line = count + 1;
                            _jobDef.Srdf_def_tp = txtDef.Text;
                            _jobDef.Srdf_def_rmk = txtDefRem.Text.Replace("'", "`");
                            _jobDef.Srdf_req_line = _jobItemLine;
                            _jobDef.jrd_itm_cd = lblItem.Text;
                            _jobDef.jrd_ser1 = lblSerNo.Text;
                            _jobDef.SDT_DESC = lblDefDesc.Text;
                            _jobDef.Srdf_req_no = txtReqNo.Text;
                            _jobDef.Srdf_req_line = _jobItemLine;

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
            Int32 _count = _scvDefList.Where(X => X.Srdf_req_line == _jbLine && X.Srdf_def_tp == txtDef.Text).Count();
            //if (_count > 0)
            //{
            //    SystemInformationMessage("Defect type already added into the list!", "Defect Type");
            //    return;
            //}

            if (_scvDefList != null)
            {
                if (_scvDefList.Count > 0)
                {
                    var nonNulls = _scvDefList.Where(X => X.Srdf_req_line == _jbLine);
                    if (nonNulls.Count() > 0)
                    {
                        _count = _scvDefList.Where(X => X.Srdf_req_line == _jbLine).Max(t => t.Srdf_def_line);
                    }
                }
            }

            //Service_Req_Def _jobDef = new Service_Req_Def();
            //_jobDef.SRD_DEF_LINE = _count + 1;
            //_jobDef.SRD_DEF_TP = txtDef.Text;
            //_jobDef.SRD_DEF_RMK = txtDefRem.Text;
            //_jobDef.Srdf_req_line = _jbLine;
            //_jobDef.jrd_itm_cd = lblItem.Text;
            //_jobDef.jrd_ser1 = lblSerNo.Text;
            //_jobDef.SDT_DESC = lblDefDesc.Text;

            //_scvDefList.Add(_jobDef);

            txtDef.Clear();
            txtDefRem.Clear();
            lblDefDesc.Text = "";

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Req_Def>();
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
                    grvDef.DataSource = new List<Service_Req_Def>();
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
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "WARR";
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
                _warrSearchtp = _scvParam.SP_DB_SERIAL;
                _warrSearchorder = "OTHER";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
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
                    Load_Serial_Infor(txtSer, string.Empty, dtDate.Value.Date);
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

            if (string.IsNullOrEmpty(_warrNo))
            {
                if (_txt == txtSer) _ser = txtSer.Text.ToString();
                if (_txt == txtWar) _warr = txtWar.Text.ToString();
                if (_txt == txtRegNo) _regno = txtWar.Text.ToString();
            }
            else
            {
                _warr = _warrNo;
            }

            _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(_ser, "", _regno, _warr, "", "", 0, out _returnStatus, out _returnMsg);
            if (_warrMstDic == null)
            {
                SystemInformationMessage("There is no warranty details available.", "No warranty");
                _txt.Clear();
                _txt.Focus();
                return;
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
                FillItemDetails(_warrMst[0], _jobDt);
                if (_warrMstSub != null)
                {
                    if (_warrMstSub.Count > 0)
                    {
                        Cursor = Cursors.WaitCursor;
                        _tempItemSubList = new List<Service_Req_Det_Sub>();
                        _tempItemSubList = FillItemSubDetails(_warrMstSub);
                        pnlAdditionalItems.Visible = true;
                        grvAddiItems.AutoGenerateColumns = false;
                        grvAddiItems.DataSource = new List<Service_Req_Det_Sub>();
                        grvAddiItems.DataSource = _tempItemSubList;
                        Cursor = Cursors.Default;
                    }
                }
            }
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
                foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                {
                    _warrMst = pair.Key;
                    _warrMstSub = pair.Value;
                }

                if (_warrMst.Count == 1)
                {
                    FillItemDetails(_warrMst[0], dtDate.Value.Date);
                }
                pnlMultiItems.Visible = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void FillItemDetails(InventorySerialMaster _warrItem, DateTime _jobDt)
        {
            lblItem.Text = _warrItem.Irsm_itm_cd;
            lblItemDesc.Text = _warrItem.Irsm_itm_desc;
            lblModel.Text = _warrItem.Irsm_itm_model;
            lblBrand.Text = _warrItem.Irsm_itm_brand;
            lblSerNo.Text = _warrItem.Irsm_ser_1;
            lblWarNo.Text = _warrItem.Irsm_warr_no;
            string _warrStatus = string.Empty;
            if (_warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).Date >= _jobDt.Date)
            { _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green; }
            else
            { _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red; }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Irsm_doc_dt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Irsm_warr_period.ToString();
            lblWarRemain.Text = (_warrItem.Irsm_doc_dt.AddMonths(_warrItem.Irsm_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
            if (Convert.ToInt32(lblWarRemain.Text) < 0) lblWarRemain.Text = "0";
            lblWarRem.Text = _warrItem.Irsm_warr_rem;

            lblInv.Text = _warrItem.Irsm_invoice_no;
            lblInvDt.Text = _warrItem.Irsm_invoice_dt.ToString("dd-MMM-yyyy");
            lblAccNo.Text = _warrItem.Irsm_acc_no;
            lblDelLoc.Text = _warrItem.Irsm_loc;
            lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            if (_jobRecall == 0)
            {
                txtMobile.Text = _warrItem.Irsm_cust_mobile;
                txtCustCode.Text = _warrItem.Irsm_cust_cd;
                txtCusName.Text = _warrItem.Irsm_cust_name;
                txtAddress1.Text = _warrItem.Irsm_cust_addr;
            }

            List<Service_job_Det> _preSerJob = new List<Service_job_Det>();
            _preSerJob = CHNLSVC.CustService.getPrejobDetails(BaseCls.GlbUserComCode, txtSer.Text.Trim(), lblItem.Text.Trim());

            lblAttempt.Text = _preSerJob.Count.ToString();

            lblSuppCode.Text = _warrItem.Irsm_orig_supp;
            lblSuppName.Text = _warrItem.Irsm_exist_supp;
            txtItmTp.Text = _warrItem.Irsm_anal_3;
            lblItemCat.Text = _warrItem.Irsm_anal_4;

            _scvjobHdr.Srb_cust_cd = _warrItem.Irsm_cust_cd;
            _scvjobHdr.Srb_cust_name = _warrItem.Irsm_cust_name;
            _scvjobHdr.Srb_add1 = _warrItem.Irsm_cust_addr;
            _scvjobHdr.Srb_mobino = _warrItem.Irsm_cust_mobile;

            lblBuyerCustCode.Text = _scvjobHdr.Srb_cust_cd;
            lblBuyerCustName.Text = _scvjobHdr.Srb_cust_name;
            lblBuyerCustAdd1.Text = _scvjobHdr.Srb_add1;
            lblBuyerCustAdd2.Text = _scvjobHdr.Srb_add2;
            lblBuyerCustMobi.Text = _scvjobHdr.Srb_mobino;

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

            checkCustomer(null, _scvjobHdr.Srb_cust_cd);
            if (_isBlack == true)
            {
                MessageBox.Show("Customer is black listed.", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (txtCustCode.Text == "CASH")
            {
                MessageBox.Show("Please select valid customer.Cannot use customer code as [CASH].", "Customer Infor", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private InventorySerialMaster FillReqItemDetails(Service_Req_Hdr _reqHdr, Service_Req_Det _reqItm)
        {
            InventorySerialMaster _warrItem = new InventorySerialMaster();

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _reqItm.Jrd_itm_cd);

            _warrItem.Irsm_itm_cd = _reqItm.Jrd_itm_cd;
            _warrItem.Irsm_itm_desc = _reqItm.Jrd_itm_desc;
            _warrItem.Irsm_itm_model = _reqItm.Jrd_model;
            _warrItem.Irsm_itm_brand = _reqItm.Jrd_brand;
            _warrItem.Irsm_ser_1 = _reqItm.Jrd_ser1;
            _warrItem.Irsm_warr_no = _reqItm.Jrd_warr;
            _warrItem.Irsm_doc_dt = _reqItm.Jrd_warrstartdt;
            _warrItem.Irsm_warr_period = _reqItm.Jrd_warrperiod;
            _warrItem.Irsm_warr_rem = _reqItm.Jrd_warrrmk;

            _warrItem.Irsm_invoice_no = _reqItm.Jrd_invc_no;
            _warrItem.Irsm_invoice_dt = _reqItm.Jrd_date_pur;
            _warrItem.Irsm_acc_no = _reqItm.Jrd_msnno;
            //_warrItem.Irsm_loc = "";
            //_warrItem.Irsm_loc_desc = "";

            _warrItem.Irsm_cust_mobile = _reqHdr.Srb_mobino;
            _warrItem.Irsm_cust_cd = _reqHdr.Srb_cust_cd;
            _warrItem.Irsm_cust_name = _reqHdr.Srb_cust_name;
            _warrItem.Irsm_cust_addr = _reqHdr.Srb_add1;

            //_warrItem.Irsm_orig_supp;
            //_warrItem.Irsm_exist_supp;
            _warrItem.Irsm_anal_3 = _masterItem.Mi_itm_tp;
            _warrItem.Irsm_anal_4 = _masterItem.Mi_cate_1;

            return _warrItem;
        }

        private List<Service_Req_Det_Sub> FillItemSubDetails(List<InventorySubSerialMaster> _warrItemSub)
        {
            int _SubLine = 0;
            List<Service_Req_Det_Sub> _newSubList = new List<Service_Req_Det_Sub>();
            foreach (InventorySubSerialMaster _sub in _warrItemSub)
            {
                Service_Req_Det_Sub _newSub = new Service_Req_Det_Sub();
                _newSub.Jrds_availabilty = true;
                _newSub.Jrds_brand = "";
                _newSub.Jrds_cre_dt = DateTime.Now.Date;
                _newSub.Jrds_issub = true;
                _newSub.Jrds_itm_cd = _sub.Irsms_itm_cd;
                _newSub.Jrds_itm_cost = 0;
                _newSub.Jrds_itm_desc = _sub.Irsms_warr_no;
                _newSub.Jrds_itm_stus = "GOD";
                //_newSub.JBDS_ITM_STUS_TEXT = "";
                _newSub.Jrds_itm_tp = _sub.Irsms_tp;
                _newSub.Jrds_jobline = Convert.ToInt32(txtJobItemLine.Text.ToString());
                _newSub.Jrds_jobno = "";
                _newSub.Jrds_model = "";
                //_newSub.JBDS_NEED_REPLACE = 0;
                _newSub.Jrds_qty = 1;
                //_newSub.JBDS_REPL_ITMCD = "";
                //_newSub.JBDS_REPL_SERID = 0;
                //_newSub.JBDS_RTN_WH = 0;
                //_newSub.JBDS_SEQ_NO = 0;
                _newSub.Jrds_ser1 = _sub.Irsms_sub_ser;
                //_newSub.JBDS_SER2 = "";
                //_newSub.JBDS_SJOBNO = "";
                _newSub.Jrds_warr = lblWarNo.Text;
                _newSub.Jrds_warr_period = _sub.Irsms_warr_period;
                _newSub.Jrds_warr_rmk = _sub.Irsms_warr_rem;

                if (Convert.ToDateTime(lblWarStart.Text.ToString()).AddMonths(_sub.Irsms_warr_period).Date >= dtDate.Value.Date)
                { _newSub.Jrds_cre_by = "UNDER WARRANTY"; }
                else
                { _newSub.Jrds_cre_by = "OVER WARRANTY"; }

                //_newSub.JrDS_IT = (Convert.ToDateTime(lblWarStart.Text.ToString()).AddMonths(_sub.Irsms_warr_period).Date - dtDate.Value.Date).TotalDays.ToString();
                _newSub.Jrds_seq_no = _SubLine + 1;
                _newSubList.Add(_newSub);
            }
            return _newSubList;
        }

        private void txtEItem_Leave(object sender, EventArgs e)
        {
            lblItemCat.Text = "";
            if (!string.IsNullOrEmpty(txtEItem.Text))
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtEItem.Text.ToUpper());
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
                txtEWarr.Focus();
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
            }
            else
            {
                pnlWarr.Height = 109;
                btnMore1.Text = "More >>";
                btnMore1.BackColor = Color.Maroon;
                btnMore1.ForeColor = Color.White;
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
            //if (optReq.Checked == true)
            //{
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceRequests);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceRequests(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtReqNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtReqNo.Focus();
            btn_add_ser.Focus();
            //}
            //else if (optJob.Checked == true)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    _jobStage = "2_12";
            //    CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //    _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            //    _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtReqNo;
            //    this.Cursor = Cursors.Default;
            //    _CommonSearch.IsSearchEnter = true;
            //    _CommonSearch.ShowDialog();
            //    txtReqNo.Focus();
            //}
            //else if (optInspection.Checked == true)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    _jobStage = "1.1";
            //    CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //    _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            //    _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            //    DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtReqNo;
            //    this.Cursor = Cursors.Default;
            //    _CommonSearch.IsSearchEnter = true;
            //    _CommonSearch.ShowDialog();
            //    txtReqNo.Focus();
            //}
        }

        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { dtpFromAL.Focus(); }
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
                    var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_def_profit") == BaseCls.GlbUserDefProf.ToString() && x.Field<string>("esep_epf") == txtEmpCode.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
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
                    grvJobItms.Rows[i].Cells["Jrd_select"].Value = true;
                }
                load_item_det(0);
            }
            else
            {
                bool _isSelect = false;
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jrd_select"].Value) == true)
                    {
                        _isSelect = true;
                        load_item_det(i);
                    }
                }

                if (_isSelect == false)
                {
                    SystemInformationMessage("Please select the job item", "Job Item");
                    return;
                }
            }

            int _jbLine = Convert.ToInt32(txtJobItemLine.Text);
            if (_scvEmpList != null && _scvEmpList.Count > 0)
            {
                int _count = _scvEmpList.Where(X => X.STH_JOBLINE == _jbLine && X.STH_EMP_CD == txtEmpCode.Text).Count();
                if (_count > 0)
                { SystemInformationMessage("Technician already assign for this item!", "Technician"); return; }
            }

            Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();
            Service_Req_Det _jobItem = _scvItemList.Where(x => x.Jrd_jobline == _jbLine).ToList()[0];

            oAloc_Hdr.STH_SEQ = 0;
            oAloc_Hdr.STH_ALOCNO = "";
            oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
            oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
            oAloc_Hdr.STH_TP = "J";
            oAloc_Hdr.STH_JOBNO = _jobItem.Jrd_jobno;
            oAloc_Hdr.STH_JOBLINE = _jobItem.Jrd_jobline;
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
            oAloc_Hdr.STH_JOB_ITM = _jobItem.Jrd_itm_cd;
            oAloc_Hdr.STH_SER = _jobItem.Jrd_ser1;
            _scvEmpList.Add(oAloc_Hdr);

            txtEmpCode.Clear();
            lblEmpName.Text = "";
            // dtpFromAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);
            // dtpToAL.Value = new DateTime(dtDate.Value.Date.Year, dtDate.Value.Date.Month, dtDate.Value.Date.Day, 12, 00, 0);

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
            grvTech.DataSource = _scvEmpList;

            txtMobile.Focus();
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
                if (optInspection.Checked == true)
                {
                    ChkExternal.Checked = true;
                    pnlItem.Visible = true;
                    pnlSer.Enabled = false;
                    optJob.Enabled = false;
                    optReq.Enabled = false;
                    //txtEItem.Focus();
                }
                else
                {
                    if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        ChkExternal.Checked = false;
                        return;
                    }
                    ClearScreen();
                    ChkExternal.Checked = true;
                    pnlItem.Visible = true;
                    pnlSer.Enabled = false;
                    optJob.Enabled = false;
                    optReq.Enabled = false;
                    txtEItem.Focus();
                }
            }
            else
            {
                Clear();
                ChkExternal.Checked = false;
                pnlItem.Visible = false;
                pnlSer.Enabled = true;
                optJob.Enabled = true;
                optReq.Enabled = true;
                txtSer.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_reqSubTp == "RCC")    //kapila 4/11/2015
            {
                SystemInformationMessage("You cannot save RCC request!", "Job Item");
                return;
            }
            if (grvJobItms.Rows.Count == 0)
            {
                SystemInformationMessage("Job item(s) not found!", "Job Item");
                return;
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

            //if (optInspection.Checked == true)
            //{
            //    if (string.IsNullOrEmpty(txtVisitChgCode.Text))
            //    {
            //        SystemInformationMessage("Please enter visit charge!", "Inspection Job");
            //        txtVisitChgCode.Focus();
            //        return;
            //    }
            //}

            if (optReq.Checked == false && !string.IsNullOrEmpty(txtCustCode.Text))
            {
                if (txtCustCode.Text == "CASH")
                {
                    SystemInformationMessage("Please enter a valid customer code.", "Job request");
                    txtCustCode.Text = "";
                    txtCustCode.Focus();
                    return;
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

            if (MessageBox.Show("Are you sure ?", "Job request", MessageBoxButtons.YesNo) == DialogResult.No) return;

            //if (_scvjobHdr != null)
            //{
            //    if (!string.IsNullOrEmpty(_scvjobHdr.SJB_CHG_CD))
            //    {
            //        SystemInformationMessage("Can't update the job, because payment is available!", "Job Update");
            //        return;
            //    }
            //}

            Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
            MasterAutoNumber _jobAuto = new MasterAutoNumber();
            RecieptHeader _recHeader = null;
            MasterAutoNumber _recAuto = null;

            #region Job Header
            // _jobHeader.SJB_RECALL = _jobRecall;
            _jobHeader.Srb_reqno = txtReqNo.Text;
            _jobHeader.Srb_dt = Convert.ToDateTime(dtDate.Value).Date;
            _jobHeader.Srb_com = BaseCls.GlbUserComCode;
            _jobHeader.Srb_jobcat = txtTaskLoc.Text;
            _jobHeader.Srb_jobtp = (ChkExternal.Checked == true) ? "E" : "I";
            _jobHeader.Srb_manualref = txtManRef.Text;
            _jobHeader.Srb_orderno = txtOrdRef.Text;

            if (optReq.Checked == false)
            { _jobHeader.Srb_jobstp = "SERDESK"; }
            else
            { _jobHeader.Srb_jobstp = "RCC"; }
            //_jobHeader.Srb_MANUALREF =
            //_jobHeader.Srb_OTHERREF =
            _jobHeader.Srb_reqno = txtReqNo.Text;
            _jobHeader.Srb_jobstage = 1;
            if (_scvEmpList != null && _scvEmpList.Count > 0) _jobHeader.Srb_jobstage = 3;
            if (optInspection.Checked == true)
            {
                if (string.IsNullOrEmpty(txtReqNo.Text))
                {
                    _jobHeader.Srb_jobstage = Convert.ToDecimal(1.1);
                    if (_scvParam.SP_ISINSPECTION == 1)
                    {
                        if (string.IsNullOrEmpty(txtVisitChgCode.Text))
                        {
                            SystemInformationMessage("Please select the visit charge.", "Inspection Job");
                            txtVisitChgCode.Focus();
                            return;
                        }
                    }
                }
            }
            _jobHeader.Srb_rmk = txtJobRem.Text;
            _jobHeader.Srb_prority = txtPriority.Text;
            //_jobHeader.Srb_ST_DT =
            //_jobHeader.Srb_ED_DT =
            //_jobHeader.Srb_NOOFPRINT =
            //_jobHeader.Srb_LASTPRINTBY =
            //_jobHeader.Srb_ORDERNO =
            _jobHeader.Srb_custexptdt = dtExpectOn.Value.Date;
            //_jobHeader.Srb_SUBSTAGE =
            if (ChkExternal.Checked == false)
            {
                _jobHeader.Srb_cust_cd = _scvjobHdr.Srb_cust_cd;
                _jobHeader.Srb_cust_tit = _scvjobHdr.Srb_cust_tit;
                _jobHeader.Srb_cust_name = _scvjobHdr.Srb_cust_name;
                _jobHeader.Srb_nic = _scvjobHdr.Srb_nic;
                //_jobHeader.Srb_DL =
                //_jobHeader.Srb_PP =
                _jobHeader.Srb_mobino = _scvjobHdr.Srb_mobino;
                _jobHeader.Srb_add1 = _scvjobHdr.Srb_add1;
                _jobHeader.Srb_add2 = _scvjobHdr.Srb_add2;
                //_jobHeader.Srb_ADD3 =
                _jobHeader.Srb_town = _scvjobHdr.Srb_town;

                _jobHeader.Srb_phno = _scvjobHdr.Srb_phno;
                _jobHeader.Srb_faxno = _scvjobHdr.Srb_faxno;
                _jobHeader.Srb_email = _scvjobHdr.Srb_email;
            }
            else
            {
                _jobHeader.Srb_cust_cd = txtCustCode.Text;
                _jobHeader.Srb_cust_tit = cmbTitle.Text;
                _jobHeader.Srb_cust_name = txtCusName.Text;
                _jobHeader.Srb_nic = txtNIC.Text;
                //_jobHeader.Srb_DL =
                //_jobHeader.Srb_PP =
                _jobHeader.Srb_mobino = txtMobile.Text;
                _jobHeader.Srb_add1 = txtAddress1.Text;
                _jobHeader.Srb_add2 = txtAddress2.Text;
                //_jobHeader.Srb_ADD3 =
                _jobHeader.Srb_town = txtTown.Text;

                _jobHeader.Srb_phno = txtContNo.Text;
                //_jobHeader.Srb_FAXNO =
                _jobHeader.Srb_email = txtemail.Text;
            }

            _jobHeader.Srb_cnt_person = txtContPersn.Text;
            _jobHeader.Srb_cnt_add1 = txtContLoc.Text;
            //_jobHeader.Srb_CNT_ADD2 =
            _jobHeader.Srb_cnt_phno = txtContNo.Text;
            _jobHeader.Srb_job_rmk = txtJobRem.Text;
            _jobHeader.Srb_tech_rmk = txtTechIns.Text;

            _jobHeader.Srb_b_cust_cd = txtCustCode.Text;
            _jobHeader.Srb_b_cust_tit = cmbTitle.Text;
            _jobHeader.Srb_b_cust_name = txtCusName.Text;
            _jobHeader.Srb_b_nic = txtNIC.Text;
            //_jobHeader.Srb_B_DL =
            //_jobHeader.Srb_B_PP =
            _jobHeader.Srb_b_mobino = txtMobile.Text;
            _jobHeader.Srb_b_add1 = txtAddress1.Text;
            _jobHeader.Srb_b_add2 = txtAddress2.Text;
            _jobHeader.Srb_b_add3 = txtTown.Text.ToString();
            if (txtTown.Tag != null) _jobHeader.Srb_b_town = txtTown.Tag.ToString();
            _jobHeader.Srb_b_phno = _jobHeader.Srb_b_phno;
            //_jobHeader.Srb_B_FAX =
            _jobHeader.Srb_b_email = txtemail.Text;

            if (string.IsNullOrEmpty(_jobHeader.Srb_cust_cd) && string.IsNullOrEmpty(_scvjobHdr.Srb_cust_name))
            {
                _jobHeader.Srb_cust_cd = _scvjobHdr.Srb_b_cust_cd;
                _jobHeader.Srb_cust_tit = _scvjobHdr.Srb_b_cust_tit;
                _jobHeader.Srb_cust_name = _scvjobHdr.Srb_b_cust_name;
                _jobHeader.Srb_nic = _scvjobHdr.Srb_b_nic;
                //_jobHeader.Srb_DL =
                //_jobHeader.Srb_PP =
                _jobHeader.Srb_mobino = _scvjobHdr.Srb_b_mobino;
                _jobHeader.Srb_add1 = _scvjobHdr.Srb_b_add1;
                _jobHeader.Srb_add2 = _scvjobHdr.Srb_b_add2;
                _jobHeader.Srb_add3 = _scvjobHdr.Srb_b_add3;
                _jobHeader.Srb_town = _scvjobHdr.Srb_b_town;

                _jobHeader.Srb_phno = _scvjobHdr.Srb_phno;
                //_jobHeader.Srb_FAXNO =
                _jobHeader.Srb_email = _scvjobHdr.Srb_b_email;
            }

            _jobHeader.Srb_infm_person = txtInfoPersn.Text;
            _jobHeader.Srb_infm_add1 = txtInfoLoc.Text;
            //_jobHeader.Srb_INFM_ADD2 =
            _jobHeader.Srb_infm_phno = txtInfoNo.Text;
            _jobHeader.Srb_stus = "P";
            _jobHeader.Srb_cre_by = BaseCls.GlbUserID;
            //_jobHeader.Srb_CRE_DT =
            _jobHeader.Srb_mod_by = BaseCls.GlbUserID;
            //_jobHeader.Srb_MOD_DT =
            _jobHeader.Srb_seq_no = _jobRecallSeq;
            //            _jobHeader.Srb_SESSION_ID = BaseCls.GlbUserSessionID;

            //if (optInspection.Checked == true)
            //{
            //    _jobHeader.Srb_CHG_CD = txtVisitChgCode.Text;
            //    _jobHeader.Srb_CHG = Convert.ToDecimal(lblCharge.Text.ToString());
            //}

            //Tharaka 2015-01-28
            //if (optReq.Checked)
            //{
            //    _jobHeader.JobCategori = 1;
            //}
            //else if (optInspection.Checked)
            //{
            //    _jobHeader.JobCategori = 2;
            //}
            //else if (optJob.Checked)
            //{
            //    _jobHeader.JobCategori = 3;
            //}
            #endregion

            #region Job Auto Number
            _jobAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            _jobAuto.Aut_cate_tp = "LOC";
            _jobAuto.Aut_moduleid = "SVREQ";
            _jobAuto.Aut_direction = 0;
            _jobAuto.Aut_year = _jobHeader.Srb_dt.Year;
            _jobAuto.Aut_start_char = "SVREQ";
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
                    _recHeader.Sar_mob_no = "";
                    _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _recHeader.Sar_mod_when = DateTime.Now;
                    _recHeader.Sar_nic_no = "";
                    _recHeader.Sar_oth_sr = "";
                    _recHeader.Sar_prefix = "";
                    _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                    _recHeader.Sar_receipt_date = Convert.ToDateTime(dtDate.Value);
                    //_recHeader.Sar_receipt_no = "na";
                    //_recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                    _recHeader.Sar_receipt_type = "DIR";
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
                    _recAuto.Aut_start_char = "DIR";
                    _recAuto.Aut_year = Convert.ToDateTime(dtDate.Text).Year;

                }
            }
            #endregion

            string jobNo;
            string receiptNo = string.Empty;
            string _msg = "";
            int eff = CHNLSVC.CustService.Save_Req(_jobHeader, _scvItemList, _scvDefList, _scvItemSubList, _jobAuto, BaseCls.GlbDefSubChannel, txtItmTp.Text, _itmBrand, _warStus, _jobAuto, out _msg, out jobNo, 0, DateTime.Now.Date, DateTime.Now.Date);
            if (eff == 1)
            {
                SystemInformationMessage(_msg, "Job request");

                //Reports.Service.ReportViewerSVC _viewJob = new Reports.Service.ReportViewerSVC();
                //BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                //BaseCls.GlbReportName = "ServiceJobCardAut.rpt";
                //BaseCls.GlbReportDoc = jobNo;
                //_viewJob.Show();
                //_viewJob = null;

                if (!string.IsNullOrEmpty(receiptNo))
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
                Clear();
            }
            else
            {
                MessageBox.Show(_msg, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
                    if (_isJobOpen == true)
                    {
                        MessageBox.Show("Cannot cancel. Already open job for this request #", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                }
            }

            Int32 _resultCNCL = -1;
            String _msg = string.Empty;

            if (MessageBox.Show("Are you sure ?", "Job request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

            _resultCNCL = CHNLSVC.CustService.Update_SCV_Req_Hdr(txtReqNo.Text);
            if (_resultCNCL == 1)
            {
                MessageBox.Show("Successfully Cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear screen ?", "Job request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            Clear();
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
            ChkExternal.Enabled = true;
            txtTaskLoc.Text = _scvParam.SP_DEF_CAT;
        }

        private void btnPopupVehClose_Click(object sender, EventArgs e)
        {
            pnlAdditionalItems.Visible = false;
        }

        private void btnAddAdditional_Click(object sender, EventArgs e)
        {
            if (grvAddiItems.Rows.Count <= 0)
            { SystemInformationMessage("First select additionl item!", "Job Additionl Item"); return; }
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
                        SystemInformationMessage("First select additionl item!", "Job Additionl Item");
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
                    foreach (Service_Req_Det_Sub _lst in _tempItemSubList)
                    {
                        if (_lst.JBDS_SELECT == true)
                        {
                            if (_isSelect == false)
                            {
                                btn_add_ser_Click(null, null);
                                _isSelect = true;
                            }
                            _lst.Jrds_warr = lblWarNo.Text;
                            _lst.Jrds_jobline = _jobItemLine - 1;
                            _lst.Jrds_line = _subLine;
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
            txtemail.Text = _masterBusinessCompany.Mbe_email;
            txtTown.Text = _masterBusinessCompany.Mbe_town_cd;
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
                        txtCustCode.Text = "CASH";
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
                        { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                    txtCustCode.Focus();
                    return;
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        protected void LoadCustomerDetailsByNIC()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCustCode.Text = "CASH";
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
                        txtCustCode.Text = "CASH";
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
        }

        private void btnSearch_CustCode_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                //CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                //_commonSearch.ReturnIndex = 0;
                //_commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                //_commonSearch.dvResult.DataSource = _result;
                //_commonSearch.BindUCtrlDDLData(_result);
                //_commonSearch.obj_TragetTextBox = txtCustCode;
                //_commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                //this.Cursor = Cursors.Default;
                //_commonSearch.ShowDialog();
                //txtCustCode.Select();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustCode.Select();
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
                SystemInformationMessage("Please enter a valid customer code.", "Job request");
                txtCustCode.Text = "";
                txtCustCode.Focus();
                return;
            }

            LoadCustomerDetailsByCustomer();
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
                Load_Serial_Infor(txtWar, string.Empty, dtDate.Value.Date);
            }
        }

        private void txtReqNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReqNo.Text)) return;

            //if (optReq.Checked == true)
            //{
            if (_scvItemList != null)
            {
                if (_scvItemList.Count > 0)
                {
                    if (MessageBox.Show("Request items are already exist. Do you want to reset it?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                }
                ClearForm();
            }
            //   LoadRequest(BaseCls.GlbUserComCode, txtReqNo.Text);
            LoadJob(BaseCls.GlbUserComCode, txtReqNo.Text, "0");

            if (CHNLSVC.CustService.IsJobOpenReq(txtReqNo.Text) == true)
                _isJobOpen = true;
            else
                _isJobOpen = false;

            optReq.Enabled = false;
            optInspection.Enabled = false;
            optJob.Enabled = false;
            pnlCustBill.Enabled = false;
            btnNewCust.Enabled = false;
            chkEditCustomer.Enabled = false;
        }

        private void ClearForm()
        {
            clear_Ext_Job_Items();
            _scvjobHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_Req_Det>();

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Req_Def>();

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();

            grvAddiItems.AutoGenerateColumns = false;
            grvAddiItems.DataSource = new List<Service_Req_Det_Sub>();

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
                        FillItemDetails(FillReqItemDetails(_reqHdr, _itm), dtDate.Value.Date);
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
            txtTaskLoc.Text = "WR";
            txtPriority.Text = "NORMAL";
            txtReqRmk.Text = _reqHdr.Srb_rmk;
            txtJobRem.Text = _reqHdr.Srb_rmk;


            txtMobile.Text = _reqHdr.Srb_b_mobino;
            txtCustCode.Text = _reqHdr.Srb_b_cust_cd;
            txtCusName.Text = _reqHdr.Srb_b_cust_name;
            txtAddress1.Text = _reqHdr.Srb_b_add1;
            txtAddress2.Text = _reqHdr.Srb_b_add2;
            txtTown.Text = _reqHdr.Srb_b_town;

            lblBuyerCustMobi.Text = _reqHdr.Srb_mobino;
            lblBuyerCustCode.Text = _reqHdr.Srb_cust_cd;
            lblBuyerCustName.Text = _reqHdr.Srb_cust_name;
            lblBuyerCustAdd1.Text = _reqHdr.Srb_add1;
            lblBuyerCustAdd2.Text = _reqHdr.Srb_add2;

        }

        private void LoadJob(string _com, string _jobNo, string _jobStage)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;

            _scvjobHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

            _returnStatus = CHNLSVC.CustService.GetScvReq(_com, _jobNo, out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _returnMsg);
            if (_returnStatus != 1)
            {
                SystemInformationMessage(_returnMsg, "Service Job");
                txtReqNo.Clear();
                txtReqNo.Focus();
                return;
            }

            if (_jobStage == "1.1")
            {
                if (_scvjobHdr.Srb_jobstage != Convert.ToDecimal(1.1))
                {
                    SystemInformationMessage("The job is not inspection stage!", "Inspection Job");
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                    return;
                }
            }

            if (optInspection.Checked == true) _jobRecall = 2;
            if (optJob.Checked == true) _jobRecall = 1;

            _jobRecallSeq = _scvjobHdr.Srb_seq_no;

            txtNIC.Text = _scvjobHdr.Srb_b_nic;
            txtMobile.Text = _scvjobHdr.Srb_b_mobino;
            txtCustCode.Text = _scvjobHdr.Srb_b_cust_cd;
            txtCusName.Text = _scvjobHdr.Srb_b_cust_name;
            cmbTitle.Text = _scvjobHdr.Srb_b_cust_tit;
            txtAddress1.Text = _scvjobHdr.Srb_b_add1;
            txtAddress2.Text = _scvjobHdr.Srb_b_add2;
            txtTown.Text = _scvjobHdr.Srb_b_add3;
            txtTown.Tag = _scvjobHdr.Srb_b_town;

            txtContPersn.Text = _scvjobHdr.Srb_cnt_person;
            txtContNo.Text = _scvjobHdr.Srb_cnt_phno;
            txtContLoc.Text = _scvjobHdr.Srb_cnt_add1;
            txtemail.Text = _scvjobHdr.Srb_b_email;

            txtInfoPersn.Text = _scvjobHdr.Srb_infm_person;
            txtInfoNo.Text = _scvjobHdr.Srb_infm_phno;
            txtInfoLoc.Text = _scvjobHdr.Srb_infm_add1;

            txtReqRmk.Text = _scvjobHdr.Srb_rmk;
            txtJobRem.Text = _scvjobHdr.Srb_job_rmk;
            txtTechIns.Text = _scvjobHdr.Srb_tech_rmk;

            txtManRef.Text = _scvjobHdr.Srb_manualref;
            txtOrdRef.Text = _scvjobHdr.Srb_orderno;

            //kapila 4/11/2015
            _reqSubTp = _scvjobHdr.Srb_jobstp;

            if (_scvjobHdr.Srb_jobtp == "E")
            {
                ChkExternal.Checked = true;
                ChkExternal.Enabled = false;

                pnlItem.Visible = true;
                pnlSer.Enabled = false;
                optJob.Enabled = false;
                optReq.Enabled = false;
            }

            //if (!string.IsNullOrEmpty(_scvjobHdr.Srb_CHG_CD))
            //{
            //    txtVisitChgCode.Text = _scvjobHdr.Srb_CHG_CD;
            //    lblCharge.Text = lblCharge.Text = String.Format("{0:#,###,###.00}", _scvjobHdr.Srb_CHG.ToString());
            //}

            grvJobItms.AutoGenerateColumns = false;
            grvJobItms.DataSource = new List<Service_Req_Det>();
            grvJobItms.DataSource = _scvItemList;

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Req_Def>();
            grvDef.DataSource = _scvDefList;

            grvTech.AutoGenerateColumns = false;
            grvTech.DataSource = new List<Service_Tech_Aloc_Hdr>();
            grvTech.DataSource = _scvEmpList;

            grvAddiItems.AutoGenerateColumns = false;
            grvAddiItems.DataSource = new List<Service_Req_Det_Sub>();
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
                Service_Req_Det _tempReqDet = _reqDet.Where(x => x.Jrd_Select == true).ToList()[0];
                txtReqLineNo.Text = _tempReqDet.Jrd_reqline.ToString();
                Load_Serial_Infor(txtWar, _tempReqDet.Jrd_warr, _tempReqDet.Jrd_chg_warr_stdt.Date);
                add_job_item(1, _tempReqDet);
                txtWar.Text = _tempReqDet.Jrd_warr;
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
            _CommonSearch.ShowDialog();
            txtTaskLoc.Focus();
        }

        private void btn_srch_prio_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePriority);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvPriority(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
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
        }

        private void txtVisitChgCode_Leave(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (!string.IsNullOrEmpty(txtVisitChgCode.Text))
            {
                if (string.IsNullOrEmpty(txtTown.Text))
                {
                    if (optInspection.Checked == true)
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
                    decimal _rate = CHNLSVC.CustService.GetScvJobStageRate(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, BaseCls.GlbUserDefLoca, txtTaskLoc.Text, 1, dtDate.Value.Date, _chgJobStage, txtVisitChgCode.Text, _scvLoc.Ml_town_cd, txtTown.Tag.ToString(), out _msg);
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

                    Set_Job_Item();
                    clear_Ext_Job_Items();
                    //pnlInspection.Enabled = true;
                    ChkExternal.Enabled = true;

                    txtReqNo.Clear();
                    txtDef.Focus();
                }
                else
                {
                    //pnlHead.Location = new Point(559, 299);
                    //pnlHead.Size = new Size(447, 292);
                    //pnlInspection.Enabled = false;
                    //lblReqRmk.Visible = true;
                    txtReqRmk.Visible = true;
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
            else
            {
                MessageBox.Show("Please select the request item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            label83.Text = _scvParam.SP_DEFECTDESCRIPTION;
            if (string.IsNullOrEmpty(_scvParam.SP_DEFECTDESCRIPTION))
            {
                label83.Text = "Defects";
            }

            _scvLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            ChkExternal.Enabled = true;

            dtpToAL.Value = DateTime.Now.AddHours(1);
            dtpFromAL.Value = DateTime.Now;
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
                cmbTitle.Focus();
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
            if (e.KeyCode == Keys.F2)
                btn_srch_task_loc_Click(null, null);
        }

        private void txtOrigin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManRef.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btn_srch_task_loc_Click(null, null);
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
                ucServicePriority1.GblCustCode = "CASH";
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
                    grvJobItms.Rows[i].Cells["Jrd_select"].Value = true;
                    selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["JRD_REQLINE"].Value);
                }
                _isSelect = true;
                // selectedItmLine = 1;
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jrd_select"].Value) == true)
                    {
                        _isSelect = true;
                        //selectedItmLine = i;
                        selectedItmLine = Convert.ToInt32(grvJobItms.Rows[i].Cells["JRD_REQLINE"].Value);
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
                    grvJobItms.Rows[i].Cells["Jrd_select"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    grvJobItms.Rows[i].Cells["Jrd_select"].Value = false;
                }
            }
        }

        private void btnLoadDOInvoice_Click(object sender, EventArgs e)
        {
            pnlDOInvoiceItems.Visible = true;
        }

        private void btnLoadItems_Click(object sender, EventArgs e)
        {
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
                                Int32 _count = _scvItemList.Where(X => X.Jrd_itm_cd == lblItem.Text && X.Jrd_ser1 == lblSerNo.Text).Count();
                                if (_count > 0)
                                {
                                    SystemInformationMessage("Already added this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                    return;
                                }

                                //List<Service_Req_Det> _pendingjobdetList = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(lblSerNo.Text, lblItem.Text, BaseCls.GlbUserComCode);
                                //_count = _pendingjobdetList.Where(X => X.Jrd_stage >= Convert.ToDecimal(1.1) && X.Jrd_stage <= 8).Count();
                                //if (_count > 0)
                                //{
                                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                //    return;
                                //}
                                //_count = _pendingjobdetList.Where(X => X.Jrd_stage >= 13 && X.Jrd_stage <= 14).Count();
                                //if (_count > 0)
                                //{
                                //    SystemInformationMessage("Pending job available for this item!", "Duplicate" + _scvParam.SP_DB_SERIAL);
                                //    return;
                                //}
                            }
                            if (lblWarNo.Text.ToUpper() != "N/A")
                            {
                                Int32 _count = _scvItemList.Where(X => X.Jrd_warr == lblWarNo.Text).Count();
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
                            //    List<Service_Req_Def> oItems = CHNLSVC.CustService.GetRequestJobDefectsJobEnty(txtReqNo.Text, _jobItemLine);
                            //    grvDef.DataSource = new List<Service_Req_Def>();
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
                    grvJobItms.Rows[i].Cells["Jrd_select"].Value = true;
                }
                selectedItmLine = 1;
                SerialNo = grvJobItms.Rows[0].Cells["JBD_SER1"].ToString();
            }
            else
            {
                for (int i = 0; i < grvJobItms.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grvJobItms.Rows[i].Cells["Jrd_select"].Value) == true)
                    {
                        selectedItmLine = i;
                    }
                }
            }

            ImageUpload frm = new ImageUpload(txtReqNo.Text, selectedItmLine, SerialNo, 0);
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

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

                        if (_blackListCustomers != null)
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
                            _isBlackInfor = "Exsisting customer.";
                            return;
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
                                    _isBlackInfor = "Exsisting customer.";
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
                                        _isBlackInfor = "Exsisting customer.";
                                        // return;
                                    }
                                }
                            }

                            else
                            {
                                _isBlackInfor = "Cannot find exsisting customer details for given identification.";
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
                    MessageBox.Show("Invalid phone number.", "Job request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Invalid phone number.", "Job request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInfoNo.Text = "";
                    txtInfoNo.Focus();
                    return;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void txtRegNo_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_regno_Click(null, null);
        }

        private void txtRegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_regno_Click(null, null);
        }

        private void txtTaskLoc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_task_loc_Click(null, null);
        }

    }
}