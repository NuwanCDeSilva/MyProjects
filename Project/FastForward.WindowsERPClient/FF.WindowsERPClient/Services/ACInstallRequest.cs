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
using System.Collections;
using FF.WindowsERPClient.Services;
using FF.WindowsERPClient.CommonSearch;
using System.IO;
using System.Drawing.Drawing2D;


//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Services
{
    public partial class ACInstallRequest : Base
    {
        DataTable param = new DataTable();
        #region static variables
        static int RccStage;
        static string RccNo;
        static Int32 SeqNo;
        static Int32 ItmLine;
        static Int32 BatchLine;
        static Int32 SerLine;
        static string SerialNo;
        static string WarrantyNo;
        static string ItemCode;
        static string ItemCat;

        static string InvNo;
        static string AccNo;
        static Int32 WarPeriod;
        static string WarRem;

        static string CustName;
        static string CustAddr;
        static DateTime InvDate;
        static string Tel;
        static Int32 SerialID;
        static string CustCode;
        static string ItemStatus;
        static string Brand;
        static string Serial2;
        static string ItemType;

        static string OthLocation;
        static string OutLocation;

        static Int32 Is_External;
        static Int32 Is_In = 0;
        static Int32 Is_Out = 0;

        static Boolean Is_Dealer_Inv = false;

        private List<Service_Req_Hdr> _reqHdrList = null;
        private List<Service_Req_Det> _reqDetList = null;
        private List<RCC_Det> _rccDetList = null;
        private List<Service_Charge> _lstCharge = null;
        private List<Service_Req_Def> _scvDefList = null;
        private string _serChannel = "";
        private string _serChannelComp = "";
        private string _serLocation = "";
        private Boolean _isDocAttachMan = false;
        private Boolean _isExternal = false;
        private Boolean _isOnlineSCM2 = false;

        List<ImageUploadDTO> oMainList = new List<ImageUploadDTO>();
        private Service_Chanal_parameter _scvParam = null;



        #endregion
        const string InvoiceBackDateName = "RCCENTRY";
        //private Service_Chanal_parameter _scvParam = null;

        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        public ACInstallRequest()
        {
            try
            {
                InitializeComponent();
                InitializeValuesNDefaultValueSet();

                btnRej.Enabled = false;
                btnApp.Enabled = false;
                btnConfirm.Enabled = false;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }


        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            //   IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out  _allowCurrentTrans);
        }

        public void setSearchValues(string _serial, string _warranty, string _itmCode, Int32 _seqno, Int32 _itemLine, Int32 _bachLine, Int32 _sLine, string _InvNo, string _AccNo, Int32 _WarPeriod, string _WarRem, string _CustName, string _CustAddr, DateTime _InvDate, string _Tel, Int32 _serialID, string _custCode, string _itemStus, string _Brand, string _serial2)
        {
            SeqNo = _seqno;
            ItmLine = _itemLine;
            BatchLine = _bachLine;
            SerLine = _sLine;
            SerialNo = _serial;
            WarrantyNo = _warranty;
            ItemCode = _itmCode;
            ItemStatus = "GOD";
            if (!string.IsNullOrEmpty(_itemStus))      //20/5/2015
            {
                DataTable _dtStatus = CHNLSVC.Inventory.GetItemStatusMaster(null, _itemStus);  //20/5/2015
                if (_dtStatus.Rows.Count > 0)
                    ItemStatus = _dtStatus.Rows[0]["mis_cd"].ToString();
            }

            Brand = _Brand;
            Serial2 = _serial2;
            SerialID = _serialID;

            InvNo = _InvNo;
            AccNo = _AccNo;
            WarPeriod = _WarPeriod;
            WarRem = _WarRem;

            CustCode = _custCode;
            txtcustCode.Text = CustCode;
            CustName = _CustName;
            CustAddr = _CustAddr;

            InvDate = _InvDate;
            Tel = _Tel;

            if (CustCode == "CASH")
                Is_Dealer_Inv = true;
            else if (CHNLSVC.Inventory.checkDealerInvoice(InvNo) == true)
                Is_Dealer_Inv = true;
            else
                Is_Dealer_Inv = false;

            MasterItem msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, ItemCode);
            ItemType = msitem.Mi_itm_tp;
            ItemCat = msitem.Mi_cate_1;

            //pnlCharge.Visible = false;
            //if (txtRCCType.Text == "CUST")
            //    pnlCharge.Visible = true;
            //else if (txtRCCType.Text == "STK")
            //{
            //    DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster(ItemStatus, "ALL");
            //    if (_status.Rows.Count > 0)
            //    {
            //        if (Convert.ToInt32(_status.Rows[0]["MIS_IS_PAY_MAN"]) == 1)
            //            pnlCharge.Visible = true;
            //    }
            //}

            //GetInvoiceDetails(_seqno, _itemLine, _bachLine, _sLine);
        }

        protected void LoadRCCDetails()
        {
            try
            {
                RCC _RCC = null;
                _RCC = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text);

                cmbJobType.SelectedValue = _RCC.Inr_sub_tp;
                txtRCCType.Text = _RCC.Inr_tp;
                //  load_rcc_type();
                cmbRccSubType.SelectedValue = _RCC.Inr_sub_tp;
                txtRCC.Text = _RCC.Inr_no;

                txtDate.Text = _RCC.Inr_dt.ToShortDateString();
                // txtDate.Enabled = false;
                txtManual.Text = _RCC.Inr_manual_ref;

                txtAccNo.Text = (_RCC.Inr_acc_no == null) ? string.Empty : _RCC.Inr_acc_no;
                txtCustAddr.Text = (_RCC.Inr_addr == null) ? string.Empty : _RCC.Inr_addr;
                txtcustCode.Text = (_RCC.Inr_cust_cd == null) ? string.Empty : _RCC.Inr_cust_cd;
                txtCustName.Text = (_RCC.Inr_cust_name == null) ? string.Empty : _RCC.Inr_cust_name;
                txtInvDate.Text = (_RCC.Inr_inv_dt.ToString() == null) ? string.Empty : _RCC.Inr_inv_dt.ToString("dd/MMM/yyyy");
                txtInvoice.Text = (_RCC.Inr_inv_no == null) ? string.Empty : _RCC.Inr_inv_no;
                txtTel.Text = (_RCC.Inr_tel == null) ? string.Empty : _RCC.Inr_tel;

                // btnCancel.Enabled = false;
                Service_Req_Hdr _reqHdr = CHNLSVC.CustService.GetServiceReqHeader(BaseCls.GlbUserComCode, _RCC.Inr_no);
                if (_reqHdr != null)
                {
                    txtPhone.Text = _reqHdr.Srb_b_phno;
                    txtContLoc.Text = _reqHdr.Srb_cnt_add1;
                    txtContNo.Text = _reqHdr.Srb_cnt_phno;
                    txtContPersn.Text = _reqHdr.Srb_cnt_person;
                    txtEmail.Text = _reqHdr.Srb_email;

                    if (_reqHdr.Srb_stus == "C")
                        lblCurStatus.Text = "CANCELED";
                    else if (_reqHdr.Srb_stus == "A")
                        lblCurStatus.Text = "APPROVED";
                    else if (_reqHdr.Srb_stus == "F")
                        lblCurStatus.Text = "JOB OPEN";

                    else if (_reqHdr.Srb_stus == "P")
                    {
                        lblCurStatus.Text = "PENDING";
                        btnCancel.Enabled = true;
                    }
                    else
                        lblCurStatus.Text = string.Empty;
                }
                else
                {
                    txtPhone.Text = "";
                    txtContLoc.Text = "";
                    txtContNo.Text = "";
                    txtContPersn.Text = "";
                    txtEmail.Text = "";
                }

                //txtEasyLoc.Text = (_RCC.Inr_easy_loc == null) ? string.Empty : _RCC.Inr_easy_loc;
                //txtInsp.Text = (_RCC.Inr_insp_by == null) ? string.Empty : _RCC.Inr_insp_by;
                txtItem.Text = (_RCC.Inr_itm == null) ? string.Empty : _RCC.Inr_itm;
                GetItemData();
                //txtRem.Text = (_RCC.Inr_rem1 == null) ? string.Empty : _RCC.Inr_rem1;
                //txtRepairRem.Text = (_RCC.Inr_rem2 == null) ? string.Empty : _RCC.Inr_rem2;
                //txtCompleteRem.Text = (_RCC.Inr_rem3 == null) ? string.Empty : _RCC.Inr_rem3;
                txtSerial.Text = (_RCC.Inr_ser == null) ? string.Empty : _RCC.Inr_ser;
                txtWarranty.Text = (_RCC.Inr_warr == null) ? string.Empty : _RCC.Inr_warr;
                //cmbAcc.SelectedValue = Convert.ToInt32(_RCC.Inr_accessories);
                //cmbCond.SelectedValue = Convert.ToInt32(_RCC.Inr_condition);
                //cmbDefect.SelectedValue = Convert.ToInt32(_RCC.Inr_def_cd);
                Is_External = Convert.ToInt32(_RCC.INR_IS_EXTERNAL);
                Is_In = Convert.ToInt32(_RCC.Inr_in_stus);
                Is_Out = Convert.ToInt32(_RCC.Inr_out_stus);

                txtAgent.Text = _RCC.Inr_agent;

                //   cmbColMethod.SelectedValue = Convert.ToInt32(_RCC.Inr_col_method);


                txtJob1.Text = (_RCC.Inr_jb_no == null) ? string.Empty : _RCC.Inr_jb_no;

                //9/2/2015 load job stage 
                DataTable _dt = CHNLSVC.CustService.getJobStageByJobNo(txtJob1.Text, BaseCls.GlbUserComCode, Convert.ToInt32(_isOnlineSCM2));
                if (_dt.Rows.Count > 0)
                    lblJobStage.Text = _dt.Rows[0]["jbs_desc"].ToString();
                else
                    lblJobStage.Text = "Job Pending";

                //   txtExecName.Text = (_RCC.Inr_anal7 == null) ? string.Empty : _RCC.Inr_anal7;
                //  txtExeContNo.Text = (_RCC.Inr_anal1 == null) ? string.Empty : _RCC.Inr_anal1;
                //  txtDispatchNo.Text = (_RCC.Inr_anal2 == null) ? string.Empty : _RCC.Inr_anal2;
                txtHologram.Text = (_RCC.Inr_hollogram_no == null) ? string.Empty : _RCC.Inr_hollogram_no;

                txtWarPeriod.Text = _RCC.INR_WAR_PERIOD.ToString();

                RccStage = _RCC.Inr_stage;
                RccNo = _RCC.Inr_no;

                DataTable _dtReqDet = CHNLSVC.CustService.GetSCVReqData(txtRCC.Text, BaseCls.GlbUserComCode);
                txtExecName.Text = _dtReqDet.Rows[0]["Jrd_conf_rmk"].ToString();
                txtExeContNo.Text = _dtReqDet.Rows[0]["Jrd_conf_desc"].ToString();

                btnRej.Enabled = false;
                btnApp.Enabled = false;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        private void Set_check_Controls(int _stage)
        {

            chkPending.Checked = false;
            //btnUpdate.Enabled = true;
            pnlJob.Enabled = false;
            chkManual.Enabled = false;

            btnSearch.Enabled = false;

            switch (_stage)
            {
                case 1:
                    {

                        pnlJob.Enabled = true;
                        btnNew.Enabled = false;
                        pnlJob.Enabled = true;
                        btnSearch.Enabled = true;
                        break;
                    }

                case 2:
                    {

                        // chkRepair.Checked = true;

                        btnNew.Enabled = false;



                        break;
                    }
                case 3:
                    {

                        //chkComplete.Checked = true;

                        btnNew.Enabled = false;


                        break;
                    }
                case 4:
                    {

                        chkPending.Checked = true;

                        btnNew.Enabled = false;


                        break;
                    }
                case 5:
                    {


                        break;
                    }
                default:
                    {

                        btnNew.Enabled = true;
                        btnSearch.Enabled = true;
                        //btnUpdate.Enabled = false;
                        break;
                    }
            }

        }

        private void InitializeValuesNDefaultValueSet()
        {
            try
            {
                txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
                //   txtCloseDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

                cmbColMethod.Items.Clear();
                cmbColMethod.DataSource = null;
                cmbColMethod.DataSource = CHNLSVC.Inventory.GetRCCDef("COLMETHOD");
                cmbColMethod.DisplayMember = "ird_desc";
                cmbColMethod.ValueMember = "ird_cd";
                cmbColMethod.SelectedIndex = -1;







                Dictionary<string, string> JobTypes = new Dictionary<string, string>();
                JobTypes.Add("NOR", "Workshop");
                JobTypes.Add("FLD", "Field");
                cmbJobType.DataSource = new BindingSource(JobTypes, null);
                cmbJobType.DisplayMember = "Value";
                cmbJobType.ValueMember = "Key";
                cmbJobType.SelectedIndex = 1;

                DataTable _dt = CHNLSVC.Inventory.Get_SCV_Area();
                cmbArea.DataSource = _dt;
                cmbArea.DisplayMember = "areaname";
                cmbArea.ValueMember = "areaname";
                cmbArea.SelectedIndex = -1;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BindRCCSubType()
        {
            try
            {
                //cmbRccSubType.Items.Clear();
                cmbRccSubType.DataSource = null;
                cmbRccSubType.DataSource = CHNLSVC.General.GetAllSubTypes(txtRCCType.Text.ToString());
                cmbRccSubType.DisplayMember = "Mstp_desc";
                cmbRccSubType.ValueMember = "Mstp_cd";
                cmbRccSubType.SelectedIndex = -1;


                if (txtRCCType.Text == "STK")
                {
                    chkOthLoc.Checked = false;
                    chkOthLoc.Enabled = false;
                }
                else
                {
                    chkOthLoc.Enabled = true;
                }


            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

                clearAll();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void clearAll()
        {
            ClearItem();
            clearCust();
            ClearRccJob();
            ClearRccRepair();
            ClearRccComplete();
            txtRCC.Enabled = true;
            btnSearch_Rcc.Enabled = true;


            chkPending.Checked = false;


            // chkPending.Enabled = false;
            lblWarStus.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblCurStatus.Text = string.Empty;
            // txtDate.Enabled = true;
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");



            _scvDefList = new List<Service_Req_Def>();


            oMainList = new List<ImageUploadDTO>();
            dgvFiles.DataSource = null;
            txtFilePath.Text = "";
            txtFileSize.Text = "";
            txtRem.Text = "";
            txtItemLoc.Text = "";
            txtItmCap.Text = "";

            btnAttachDocs.Enabled = false;
            _isExternal = false;
            _isOnlineSCM2 = false;

            btnNew.Enabled = true;
            DataTable param = new DataTable();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            SaveRCC();
            btnNew.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void SaveRCC()
        {

            try
            {
                string _genInventoryDoc = string.Empty;
                string QTNum = "";
                #region validation

                if (dtpIns.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Please enter valid installation date!", "Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvDelDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Please select the items!", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (txtRCC.Enabled == true)
                {
                    MessageBox.Show("Please press New button!", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtAgent.Text == "")
                {
                    MessageBox.Show("Please select Agent", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (cmbArea.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Area", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //7/10/2015 check selected valid service agent with the item 
                //Int32 _effItm = CHNLSVC.Inventory.CheckValidServiceAgent(txtItem.Text, _serChannelComp, _serLocation);
                //if (_effItm == 0)
                //{
                //    MessageBox.Show("Invalid Service agent. This item cannot be sent to this service agent", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}


                Boolean _isAgent = CHNLSVC.Sales.IsCheckServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                if (_isAgent == false)
                {
                    MessageBox.Show("Invalid Service Agent !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAgent.Focus();
                }

                _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                if (_isExternal == true)
                {
                    if (string.IsNullOrEmpty(txtItemLoc.Text))
                    {
                        MessageBox.Show("Please enter item location !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemLoc.Focus();
                    }
                    if (string.IsNullOrEmpty(txtItmCap.Text))
                    {
                        MessageBox.Show("Please enter item capacity !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItmCap.Focus();
                    }
                }


                #endregion

                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                #region FILL RCC OBJECT
                RCC _RCC = new RCC();
                string _RccNo = "";

                if (RccStage != 0)
                {
                    _RCC.Inr_no = txtRCC.Text;
                }
                _RCC.Inr_com_cd = BaseCls.GlbUserComCode;
                _RCC.Inr_loc_cd = BaseCls.GlbUserDefLoca;
                _RCC.Inr_dt = Convert.ToDateTime(txtDate.Value);
                _RCC.Inr_is_manual = chkManual.Checked;
                _RCC.Inr_manual_ref = txtManual.Text;
                _RCC.Inr_tp = txtRCCType.Text.ToString();
                _RCC.Inr_sub_tp = cmbJobType.SelectedValue.ToString(); // "NOR";   // Convert.ToString(cmbRccSubType.SelectedValue);
                _RCC.Inr_agent = Convert.ToString(txtAgent.Text);
                _RCC.Inr_col_method = Convert.ToString(cmbColMethod.SelectedValue);
                _RCC.Inr_inv_no = txtInvoice.Text;
                _RCC.Inr_acc_no = txtAccNo.Text;

                if (!string.IsNullOrEmpty(txtInvDate.Text))
                {
                    _RCC.Inr_inv_dt = Convert.ToDateTime(txtInvDate.Text);
                }
                else
                {
                    _RCC.Inr_inv_dt = Convert.ToDateTime(DateTime.Now).Date;
                }

                //_RCC.Inr_oth_doc_no = 
                //_RCC.Inr_oth_doc_dt = 
                _RCC.Inr_cust_cd = txtcustCode.Text;
                _RCC.Inr_cust_name = txtCustName.Text;
                _RCC.Inr_addr = txtCustAddr.Text;
                _RCC.Inr_tel = txtTel.Text;
                foreach (DataGridViewRow row in dgvDelDetails.Rows)
                {
                    _RCC.Inr_itm = row.Cells["item"].Value.ToString();
                    _RCC.Inr_ser = row.Cells["serial"].Value.ToString();
                    _RCC.Inr_warr = row.Cells["warranty"].Value.ToString();
                }
                _RCC.Inr_def_cd = (3).ToString();  // Convert.ToString(cmbDefect.SelectedValue);
                _RCC.Inr_def = txtItemLoc.Text;
                _RCC.Inr_condition = Convert.ToString(cmbArea.SelectedValue);
                _RCC.Inr_accessories = txtItmCap.Text;
                _RCC.Inr_easy_loc = optPLC.Checked ? "PLC" : "RET";
                // _RCC.Inr_insp_by = txtInsp.Text;
                _RCC.Inr_rem1 = txtRem.Text;
                //_RCC.Inr_def_rem = 
                _RCC.Inr_is_jb_open = false;
                _RCC.Inr_jb_no = txtJob1.Text;
                _RCC.Inr_open_by = BaseCls.GlbUserID;
                //_RCC.Inr_jb_rem = 
                // _RCC.Inr_is_repaired = radOk.Checked ? true : false;
                if (RccStage == 2)
                {
                    // _RCC.Inr_repair_stus = Convert.ToString(cmbReason.SelectedValue);
                }
                //  _RCC.Inr_rem2 = txtRepairRem.Text;
                _RCC.Inr_is_returned = false;
                if (_RCC.Inr_stage == 2)
                {
                    //   _RCC.Inr_return_dt = Convert.ToDateTime(txtRetDate.Value);
                }
                if (RccStage == 2)
                {
                    //   _RCC.Inr_ret_condition = Convert.ToString(cmbRetCond.SelectedValue);
                }
                _RCC.Inr_hollogram_no = _serChannelComp;
                _RCC.Inr_is_replace = false;
                _RCC.Inr_is_resell = false;
                _RCC.Inr_is_ret = false;
                _RCC.Inr_is_dispose = false;
                _RCC.Inr_acknoledge_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_is_complete = false;
                _RCC.Inr_complete_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_complete_by = BaseCls.GlbUserID;
                // _RCC.Inr_rem3 = txtCompleteRem.Text;
                // _RCC.Inr_closure_tp = Convert.ToString(cmbClosureType.SelectedValue);
                if (chkReq.Checked == true)
                {
                    _RCC.Inr_stage = 5;
                    _RCC.Inr_stus = "P";
                    _RCC.Inr_is_req = true;
                }
                else
                {
                    _RCC.Inr_stage = 1;
                    _RCC.Inr_stus = "A";
                    _RCC.Inr_is_req = false;
                }

                _RCC.Inr_cre_by = BaseCls.GlbUserID;
                _RCC.Inr_cre_dt = DateTime.Now;
                _RCC.Inr_mod_by = BaseCls.GlbUserID;
                _RCC.Inr_mod_dt = DateTime.Now;
                _RCC.Inr_session_id = BaseCls.GlbUserSessionID;
                //  _RCC.Inr_anal1 = txtExeContNo.Text;
                //  _RCC.Inr_anal2 = txtDispatchNo.Text;
                //  _RCC.Inr_anal7 = txtExecName.Text;
                _RCC.Inr_is_req = chkReq.Checked;
                _RCC.Inr_serial_id = SerialID;
                _RCC.INR_IS_EXTERNAL = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);
                if (!string.IsNullOrEmpty(txtWarPeriod.Text))
                {
                    _RCC.INR_WAR_PERIOD = Convert.ToInt32(txtWarPeriod.Text);
                }
                else
                {
                    _RCC.INR_WAR_PERIOD = 0;
                }

                #endregion

                string _othLoc;
                string _outLoc;
                Int32 _effect = 0;

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ACI";
                masterAuto.Aut_start_char = "ACI";
                masterAuto.Aut_year = null;

                Service_Req_Hdr _custReqHdr = new Service_Req_Hdr();
                #region req header
                _reqHdrList = new List<Service_Req_Hdr>();
                _reqDetList = new List<Service_Req_Det>();
                _rccDetList = new List<RCC_Det>();

                //30/12/2014 generate SCM2 request

                _custReqHdr.Srb_dt = Convert.ToDateTime(txtDate.Value);
                _custReqHdr.Srb_com = _serChannelComp;  //4/9/2015
                _custReqHdr.Srb_jobcat = "WW";
                _custReqHdr.Srb_jobtp = "I";
                _custReqHdr.Srb_jobstp = "RCC";
                _custReqHdr.Srb_manualref = "";
                _custReqHdr.Srb_otherref = "";
                _custReqHdr.Srb_refno = "";  //aod #
                _custReqHdr.Srb_jobstage = 1;
                _custReqHdr.Srb_rmk = txtRem.Text;
                _custReqHdr.Srb_prority = "NORMAL";
                _custReqHdr.Srb_st_dt = Convert.ToDateTime(txtDate.Value);
                _custReqHdr.Srb_ed_dt = Convert.ToDateTime(txtDate.Value);
                _custReqHdr.Srb_noofprint = 0;
                _custReqHdr.Srb_lastprintby = "";
                _custReqHdr.Srb_orderno = "";
                _custReqHdr.Srb_custexptdt = Convert.ToDateTime(dtpIns.Value.Date);
                _custReqHdr.Srb_substage = "1";

                //-----shop details
                _custReqHdr.Srb_cust_cd = BaseCls.GlbUserDefProf;
                MasterProfitCenter _mstPc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (_mstPc != null)
                {
                    //_custReqHdr.Srb_cust_tit = 
                    _custReqHdr.Srb_cust_name = _mstPc.Mpc_desc;
                    //_custReqHdr.Srb_nic = 
                    //_custReqHdr.Srb_dl = 
                    //_custReqHdr.Srb_pp = 
                    if (_mstPc.Mpc_tel.Length > 20)
                        _custReqHdr.Srb_mobino = (_mstPc.Mpc_tel).Substring(1, 20);
                    else
                        _custReqHdr.Srb_mobino = (_mstPc.Mpc_tel);
                    _custReqHdr.Srb_add1 = _mstPc.Mpc_add_1;
                    _custReqHdr.Srb_add2 = _mstPc.Mpc_add_2;
                    //_custReqHdr.Srb_add3 = 
                    //_custReqHdr.Srb_town =
                    _custReqHdr.Srb_phno = _mstPc.Mpc_tel;
                    _custReqHdr.Srb_faxno = _mstPc.Mpc_fax;
                    _custReqHdr.Srb_email = _mstPc.Mpc_email;
                    _custReqHdr.Srb_cnt_person = txtContPersn.Text;
                    _custReqHdr.Srb_cnt_add1 = txtContLoc.Text;
                    _custReqHdr.Srb_cnt_add2 = "";
                    _custReqHdr.Srb_cnt_phno = txtContNo.Text;
                    _custReqHdr.Srb_job_rmk = "";
                    //_custReqHdr.Srb_tech_rmk = 
                }

                //------original cust details

                _custReqHdr.Srb_b_cust_cd = txtcustCode.Text;
                if (_custReqHdr.Srb_b_cust_cd == "CASH") _custReqHdr.Srb_b_cust_cd = "CASH.";
                _custReqHdr.Srb_b_cust_name = txtCustName.Text;
                _custReqHdr.Srb_b_mobino = txtTel.Text;
                _custReqHdr.Srb_b_add1 = txtCustAddr.Text;
                _custReqHdr.Srb_b_phno = txtPhone.Text;
                _custReqHdr.Srb_b_email = txtEmail.Text;


                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(_custReqHdr.Srb_b_cust_cd, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    _custReqHdr.Srb_b_cust_tit = _masterBusinessCompany.MBE_TIT;

                    _custReqHdr.Srb_b_nic = _masterBusinessCompany.Mbe_nic;
                    _custReqHdr.Srb_b_dl = _masterBusinessCompany.Mbe_dl_no;
                    _custReqHdr.Srb_b_pp = _masterBusinessCompany.Mbe_pp_no;

                    _custReqHdr.Srb_b_add2 = _masterBusinessCompany.Mbe_add2;
                    _custReqHdr.Srb_b_add3 = "";
                    _custReqHdr.Srb_b_town = _masterBusinessCompany.Mbe_town_cd;

                    _custReqHdr.Srb_b_fax = _masterBusinessCompany.Mbe_fax;
                    _custReqHdr.Srb_b_email = _masterBusinessCompany.Mbe_email;
                    _custReqHdr.Srb_b_phno = _masterBusinessCompany.Mbe_tel;
                }
                //_custReqHdr.Srb_infm_person = 
                //_custReqHdr.Srb_infm_add1 = 
                //_custReqHdr.Srb_infm_add2 = 
                //_custReqHdr.Srb_infm_phno = 
                _custReqHdr.Srb_stus = "P";
                _custReqHdr.Srb_cre_by = BaseCls.GlbUserID;
                //_custReqHdr.Srb_cre_dt = 
                _custReqHdr.Srb_mod_by = BaseCls.GlbUserID;
                //_custReqHdr.Srb_mod_dt = 

                _reqHdrList.Add(_custReqHdr);
                #endregion

                #region req details
                Int32 _reqLine = 1;

                foreach (DataGridViewRow row in dgvDelDetails.Rows)
                {
                    Service_Req_Det _custReqDet = new Service_Req_Det();
                    RCC_Det _rccdet = new RCC_Det();

                    _rccdet.Inrd_acc_no = row.Cells["acc_no"].Value.ToString();
                    _rccdet.Inrd_inv_dt = Convert.ToDateTime(row.Cells["inv_date"].Value);
                    _rccdet.Inrd_inv_no = row.Cells["inv_no"].Value.ToString();
                    _rccdet.Inrd_itm = row.Cells["item"].Value.ToString();
                    _rccdet.Inrd_line = _reqLine;
                    //if (row.Cells["do_date"].Value.ToString()!="01/Dec/2999")
                    //_rccdet.Inrd_oth_doc_dt = Convert.ToDateTime(row.Cells["do_date"].Value);
                    _rccdet.Inrd_oth_doc_no = row.Cells["acc_no"].Value.ToString();
                    _rccdet.Inrd_qty = Convert.ToInt32(row.Cells["qty"].Value);
                    _rccdet.Inrd_ser = row.Cells["serial"].Value.ToString();
                    _rccdet.Inrd_warr = row.Cells["warranty"].Value.ToString();

                    _rccDetList.Add(_rccdet);

                    if (_isExternal == false)
                    {
                        _custReqDet.Jrd_reqline = _reqLine;
                        _custReqDet.Jrd_loc = _serLocation;
                        DataTable LocDes = CHNLSVC.Sales.getLocDesc(_serChannelComp, "LOC", _serLocation);
                        if (LocDes.Rows.Count > 0)
                            _custReqDet.Jrd_pc = LocDes.Rows[0]["ml_def_pc"].ToString();

                        _custReqDet.Jrd_itm_cd = row.Cells["item"].Value.ToString();
                        if (ItemStatus == "GOOD")
                            _custReqDet.Jrd_itm_stus = "GOD";
                        else
                            _custReqDet.Jrd_itm_stus = ItemStatus;

                        MasterItem _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row.Cells["item"].Value.ToString());

                        _custReqDet.Jrd_itm_desc = _mstItm.Mi_shortdesc;
                        _custReqDet.Jrd_brand = _mstItm.Mi_brand;
                        _custReqDet.Jrd_model = _mstItm.Mi_model;
                        _custReqDet.Jrd_itm_cost = 0;
                        _custReqDet.Jrd_ser1 = row.Cells["serial"].Value.ToString();
                        if (string.IsNullOrEmpty(Serial2))
                            _custReqDet.Jrd_ser2 = "N/A";
                        else
                            _custReqDet.Jrd_ser2 = Serial2;
                        _custReqDet.Jrd_warr = row.Cells["warranty"].Value.ToString();
                        //_custReqDet.Jrd_regno = 
                        //_custReqDet.Jrd_milage = 

                        if (txtRCCType.Text == "STK")
                        {
                            _custReqDet.Jrd_warr_stus = 1;
                            _custReqDet.Jrd_onloan = 1;
                        }
                        else
                        {
                            _custReqDet.Jrd_warr_stus = 1; // Convert.ToInt32(txtWarPeriod.Text) > 0 ? 1 : 0;
                            _custReqDet.Jrd_onloan = 0;
                        }

                        _custReqDet.Jrd_chg_warr_stdt = Convert.ToDateTime(row.Cells["inv_date"].Value);
                        _custReqDet.Jrd_chg_warr_rmk = WarRem;
                        //_custReqDet.Jrd_sentwcn = 
                        //_custReqDet.Jrd_isinsurance = 
                        //_custReqDet.Jrd_ser_term = 
                        //_custReqDet.Jrd_lastwarr_stdt = 
                        //_custReqDet.Jrd_issued = 
                        //_custReqDet.Jrd_mainitmcd = 
                        //_custReqDet.Jrd_mainitmser = 
                        //_custReqDet.Jrd_mainitmwarr = 
                        //_custReqDet.Jrd_itmmfc = 
                        //_custReqDet.Jrd_mainitmmfc = 
                        _custReqDet.Jrd_availabilty = 1;
                        //_custReqDet.Jrd_usejob = 
                        //_custReqDet.Jrd_msnno = 
                        _custReqDet.Jrd_itmtp = ItemType;
                        //_custReqDet.Jrd_serlocchr = 
                        //_custReqDet.Jrd_custnotes = 
                        //_custReqDet.Jrd_mainreqno = 
                        //_custReqDet.Jrd_mainreqloc = 
                        //_custReqDet.Jrd_mainjobno = 

                        _custReqDet.Jrd_isstockupdate = 0;
                        //_custReqDet.Jrd_needgatepass = 
                        _custReqDet.Jrd_iswrn = 0;
                        _custReqDet.Jrd_warrperiod = 3;  // Convert.ToInt32(txtWarPeriod.Text);
                        _custReqDet.Jrd_warrrmk = WarRem;
                        _custReqDet.Jrd_warrstartdt = Convert.ToDateTime(row.Cells["inv_date"].Value);
                        _custReqDet.Jrd_warrreplace = 0;
                        //_custReqDet.Jrd_date_pur = 
                        _custReqDet.Jrd_invc_no = row.Cells["inv_no"].Value.ToString();
                        //_custReqDet.Jrd_waraamd_seq = 
                        //_custReqDet.Jrd_waraamd_by = 
                        //_custReqDet.Jrd_waraamd_dt = 
                        //_custReqDet.Jrd_invc_showroom = 
                        _custReqDet.Jrd_aodissueloc = BaseCls.GlbUserDefLoca;
                        _custReqDet.Jrd_aodissuedt = Convert.ToDateTime(txtDate.Value);
                        //_custReqDet.Jrd_aodissueno = 
                        //_custReqDet.Jrd_aodrecno = 
                        //_custReqDet.Jrd_techst_dt = 
                        //_custReqDet.Jrd_techfin_dt = 
                        //_custReqDet.Jrd_msn_no = 
                        _custReqDet.Jrd_isexternalitm = 0;
                        //_custReqDet.Jrd_conf_dt = 
                        //_custReqDet.Jrd_conf_cd = 
                        _custReqDet.Jrd_conf_desc = txtExeContNo.Text;
                        _custReqDet.Jrd_conf_rmk = txtExecName.Text;
                        //_custReqDet.Jrd_tranf_by = 
                        //_custReqDet.Jrd_tranf_dt = 
                        _custReqDet.Jrd_do_invoice = 0;
                        //_custReqDet.Jrd_insu_com = 
                        //_custReqDet.Jrd_agreeno = 
                        _custReqDet.Jrd_issrn = 0;
                        //_custReqDet.Jrd_isagreement = 
                        //_custReqDet.Jrd_cust_agreeno = 
                        //_custReqDet.Jrd_quo_no = 
                        _custReqDet.Jrd_stage = Convert.ToInt32(1.1);
                        _custReqDet.Jrd_com = _serChannelComp;  //4/9/2015
                        _custReqDet.Jrd_ser_id = SerialID.ToString();
                        _custReqDet.Jrd_used = 0;
                        //_custReqDet.Jrd_jobno = 
                        //_custReqDet.Jrd_jobline = 

                        //20/8/2015
                        MasterItem _mitm = CHNLSVC.Inventory.GetItem("", row.Cells["item"].Value.ToString());
                        if (_mitm.Mi_is_ser1 == 1)
                        {
                            #region Get Supplier
                            DataTable _dtSupp = new DataTable();
                            _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, row.Cells["item"].Value.ToString(), row.Cells["serial"].Value.ToString(), 1);
                            if (_dtSupp != null && _dtSupp.Rows.Count > 0)
                            {
                                for (int i = 0; i < _dtSupp.Rows.Count; i++)
                                {
                                    _custReqDet.Jrd_supp_cd = _dtSupp.Rows[i]["EXPORTER"].ToString();
                                    break;
                                }
                            }
                            else
                            {
                                _dtSupp = new DataTable();
                                _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, row.Cells["item"].Value.ToString(), row.Cells["serial"].Value.ToString(), 2);
                                if (_dtSupp != null && _dtSupp.Rows.Count > 0)
                                {
                                    for (int j = 0; j < _dtSupp.Rows.Count; j++)
                                    {
                                        _custReqDet.Jrd_supp_cd = _dtSupp.Rows[j]["EXPORTER"].ToString();
                                        break;
                                    }
                                }
                                else
                                {
                                    //4/9/2015
                                    _dtSupp = new DataTable();
                                    _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, row.Cells["item"].Value.ToString(), row.Cells["serial"].Value.ToString(), 3);
                                    if (_dtSupp != null && _dtSupp.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < _dtSupp.Rows.Count; j++)
                                        {
                                            _custReqDet.Jrd_supp_cd = _dtSupp.Rows[j]["EXPORTER"].ToString();
                                            break;
                                        }
                                    }
                                }
                            }

                            #endregion

                        }

                        _reqLine = _reqLine + 1;
                        _reqDetList.Add(_custReqDet);
                    }

                }
                #endregion
                string job=string.Empty;
                int row_aff = CHNLSVC.Inventory.SaveACInstallRequest(_RCC, _reqHdrList, _reqDetList, _rccDetList, Is_Dealer_Inv, _isOnlineSCM2, masterAuto, out _RccNo, out job,null,0,false);
                if (row_aff != -99 && row_aff >= 0)
                {

                    MessageBox.Show("Successfully Updated. Request Number - " + _RccNo, "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ACInstallRequest formnew = new ACInstallRequest();
                    formnew.MdiParent = this.MdiParent;
                    formnew.Location = this.Location;
                    formnew.Show();
                    this.Close();

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "ACInsReqPrint.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "ACInsReqPrint.rpt";
                    _view.GlbReportDoc = _RccNo;
                    BaseCls.GlbReportDoc = _RccNo;
                    _view.Show();
                    _view = null;

                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_RccNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                clearAll();
                chkReq.Checked = false;
                btnSearch.Enabled = true;


            NoAOD1:
                RccStage = 0;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnSearch_Rcc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = null;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ACInsReq);
                _result = CHNLSVC.CommonSearch.GetAcInsSearchData(_CommonSearch.SearchParams, null, null);


                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRCC;
                _CommonSearch.ShowDialog();

                txtRCC_Leave(null, null);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InsReqInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MasterType:
                    {
                        paramsText.Append("RCC" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ACInsReq:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RCC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RCCReq:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceAgentLoc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RccByCompleteStage:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "4" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RccByRequestStage:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "5" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(_serChannelComp + seperator + _serChannel + seperator + ItemCat + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Agent_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = null;
                _CommonSearch.ReturnIndex = 0;
                if (BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgentLoc);
                    _result = CHNLSVC.CommonSearch.GetServiceAgentLocation(_CommonSearch.SearchParams, null, null);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
                    _result = CHNLSVC.CommonSearch.GetServiceAgent(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgent;
                _CommonSearch.ShowDialog();
                txtAgent.Select();


            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void load_rcc_sub_type()
        {

            BindRCCSubType();

            txtAccNo.Text = string.Empty;
            txtCustAddr.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtInvoice.Text = string.Empty;
            txtInvDate.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtPhone.Text = "";
            txtEmail.Text = "";


            cmbColMethod.SelectedIndex = -1;

            //  txtEasyLoc.Text = string.Empty;
            txtRCC.Text = "";
            txtAgent.Text = "";
            // grvCharge.DataSource = null;
            // txtInsp.Text = "";
            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtItmStus.Text = "";
            // txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";
            // cmbAcc.SelectedIndex = -1;
            //cmbCond.SelectedIndex = -1;
            //   cmbDefect.SelectedIndex = -1;

            //    pnlCharge.Visible = false;



            if (txtRCCType.Text == "STK")
            {
                MasterProfitCenter _mstPc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (_mstPc != null)
                {
                    txtcustCode.Text = _mstPc.Mpc_cd;
                    txtCustName.Text = _mstPc.Mpc_desc;
                    txtCustAddr.Text = _mstPc.Mpc_add_1 + " " + _mstPc.Mpc_add_2;
                    txtTel.Text = _mstPc.Mpc_tel;
                    txtPhone.Text = _mstPc.Mpc_tel;
                    txtEmail.Text = _mstPc.Mpc_email;
                }
            }

        }

        private void clearCust()
        {
            txtCustAddr.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtPhone.Text = "";
            txtContNo.Text = "";
            txtcustCode.Text = "";
            txtTel.Text = string.Empty;
            txtContLoc.Text = "";

            txtEmail.Text = "";
            txtContPersn.Text = "";
            txtAgent.Text = "";

            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = null;
            DataTable param = new DataTable();

        }
        private void ClearItem()
        {
            txtAccNo.Text = string.Empty;

            txtInvoice.Text = string.Empty;
            txtInvDate.Text = string.Empty;

            txtDono.Text = "";
            txtQty.Text = "";
            txtLine.Text = "";

            txtRCC.Text = "";


            cmbColMethod.SelectedIndex = -1;
            cmbRccSubType.SelectedIndex = -1;
            // txtRCCType.Text = "";

            txtManual.Text = "";
            chkManual.Checked = false;
            lblStatus.Text = "";


            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtItmStus.Text = "";
            // txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";

            txtWarRem.Text = "";
            txtWarPeriod.Text = "0";


        }

        private void ClearRccJob()
        {
            txtJob1.Text = "";
            txtExecName.Text = "";
            txtExeContNo.Text = "";
            // txtDispatchNo.Text = "";
            txtHologram.Text = "";
        }

        private void ClearRccRepair()
        {

            //chkRepaired.Checked = false;

        }

        private void ClearRccComplete()
        {

        }

        protected void GetItemData()
        {
            try
            {
                MasterItem _Item = null;
                if (txtItem.Text == "")
                {
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    return;
                }
                _Item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                if (_Item != null)
                {
                    txtItemDesn.Text = _Item.Mi_shortdesc;
                    txtModel.Text = _Item.Mi_model;
                }
                else
                {
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManual.Checked == true)
                {
                    txtManual.Enabled = true;
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "MDOC_RCC");
                    if (_NextNo != 0)
                    {
                        txtManual.Text = _NextNo.ToString();
                    }
                    else
                    {
                        txtManual.Text = "";
                    }
                }

                else
                {
                    txtManual.Text = string.Empty;
                    txtManual.Enabled = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRCC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRCC.Text))
            {
                LoadRCCDetails();
                GetServiceChannel();
                //  Set_check_Controls(RccStage);
            }
        }

        private void load_inv_details(Int32 _itmLine)
        {
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                DataTable salesDetails = CHNLSVC.Sales.GetSalesDetailsByLine(txtInvoice.Text, _itmLine);
                foreach (DataRow row in salesDetails.Rows)
                {
                    if (!string.IsNullOrEmpty(txtcustCode.Text))
                        if (txtcustCode.Text != row["SAH_CUS_CD"].ToString())
                        {
                            MessageBox.Show("Customer code mismatch", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    // txtInvoice.Text = InvNo;
                    txtAccNo.Text = row["SAH_ACC_NO"].ToString();
                    txtWarPeriod.Text = WarPeriod.ToString();
                    txtWarRem.Text = WarRem;

                    txtcustCode.Text = row["SAH_CUS_CD"].ToString();
                    txtCustName.Text = row["SAH_CUS_NAME"].ToString();
                    txtCustAddr.Text = row["SAH_CUS_ADD1"].ToString();
                    txtInvDate.Text = row["SAH_DT"].ToString();
                    txtTel.Text = row["MBE_MOB"].ToString();

                    txtItem.Text = row["SAD_ITM_CD"].ToString();
                    txtQty.Text = row["SAD_QTY"].ToString();
                    txtLine.Text = row["sad_itm_line"].ToString();

                    txtWarPeriod.Text = row["sad_warr_period"].ToString();
                    WarPeriod = Convert.ToInt32(row["sad_warr_period"]);

                    DateTime _warExpDate = Convert.ToDateTime(row["SAH_DT"]).AddMonths(WarPeriod);

                    if ((DateTime.Now).Date <= _warExpDate)
                    {
                        lblWarStus.ForeColor = Color.Blue;
                        lblWarStus.Text = "UNDER WARRANTY";
                    }
                    else
                    {
                        lblWarStus.ForeColor = Color.Red;
                        lblWarStus.Text = "OVER WARRANTY";
                    }

                    GetItemData();

                    txtSerial.Text = "";
                    txtWarranty.Text = "";
                    txtItmStus.Text = row["SAD_ITM_STUS"].ToString();

                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string _outLoc = "";

            if (chkPending.Checked == true)
            {
                Int32 _line = 0;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                    _CommonSearch.ReturnIndex = 9;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsReqInvoice);
                    DataTable _result = CHNLSVC.CommonSearch.SearchInv4InsReq(_CommonSearch.SearchParams, null, null, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
                    _CommonSearch.dtpFrom.Value = DateTime.Now.Date.AddMonths(-1);
                    _CommonSearch.dtpTo.Value = DateTime.Now.Date;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    //_commonSearch.IsSearchEnter = true;
                    this.Cursor = Cursors.Default;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();


                    string[] seperator = new string[] { "@" };
                    string[] searchParams = txtInvoice.Text.Split(seperator, StringSplitOptions.None);
                    txtInvoice.Text = searchParams[0].ToString();
                    _line = Convert.ToInt32(searchParams[1]);

                    load_inv_details(_line);
                }
                catch (Exception ex)
                {
                    txtInvoice.Clear();
                    this.Cursor = Cursors.Default;
                    // SystemErrorMessage(ex); 
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseAllChannels();
                }
            }
            else
            {
                CommonSearch.RCCSerialSearch _SerSearch = new CommonSearch.RCCSerialSearch();
                _SerSearch.setInitValues(lblRCCType.Text, chkOthLoc.Checked ? true : false, _outLoc);

                _SerSearch.ShowDialog();

                if (!string.IsNullOrEmpty(txtcustCode.Text))
                {
                    if (txtcustCode.Text != CustCode)
                    {
                        MessageBox.Show("Customer code mismatch", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    txtcustCode.Text = CustCode;
                    txtCustName.Text = CustName;
                    txtCustAddr.Text = CustAddr;
                    txtTel.Text = Tel;
                }

                txtWarPeriod.Text = "0";
                lblFOC.Visible = false;

                txtInvoice.Text = InvNo;
                txtAccNo.Text = AccNo;
                txtWarPeriod.Text = WarPeriod.ToString();
                txtWarRem.Text = WarRem;
                txtQty.Text = "1";


                txtInvDate.Text = (InvDate).ToString("dd/MMM/yyyy");


                //kapila 1/8/2015
                List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
                _InvDetailList = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());
                Boolean _isFree = false;
                if (_InvDetailList != null)
                {
                    foreach (InvoiceItem _ser in _InvDetailList.Where(x => x.Sad_itm_cd == ItemCode || x.Sad_sim_itm_cd == ItemCode))
                    {
                        if (_ser.Sad_tot_amt / _ser.Sad_qty == 0)
                            _isFree = true;

                    }
                    if (_isFree == true)
                        lblFOC.Visible = true;
                    else
                        lblFOC.Visible = false;
                }


                //GetInvoiceDetails(SeqNo, ItmLine, BatchLine, SerLine);

                txtItem.Text = ItemCode;
                //24/7/2015
                if (txtRCCType.Text == "STK")
                {
                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(ItemCode, ItemStatus);
                    txtWarPeriod.Text = _period.Mwp_val.ToString();
                }
                DateTime _warExpDate = InvDate.AddMonths(WarPeriod);

                if ((DateTime.Now).Date <= _warExpDate)
                {
                    lblWarStus.ForeColor = Color.Blue;
                    lblWarStus.Text = "UNDER WARRANTY";
                }
                else
                {
                    lblWarStus.ForeColor = Color.Red;
                    lblWarStus.Text = "OVER WARRANTY";
                }

                GetItemData();

                txtSerial.Text = SerialNo;
                txtWarranty.Text = WarrantyNo;
                txtItmStus.Text = ItemStatus;

            }

            InvoiceHeader _invH = CHNLSVC.Sales.GetInvoiceHeader(txtInvoice.Text);
            if (_invH != null)
            {
                DataTable _dtEmp = CHNLSVC.Sales.GetinvEmp(BaseCls.GlbUserComCode, _invH.Sah_sales_ex_cd);
                if (_dtEmp.Rows.Count > 0)
                {
                    txtExecName.Text = _dtEmp.Rows[0]["esep_name_initials"].ToString();
                    txtExeContNo.Text = _dtEmp.Rows[0]["esep_mobi_no"].ToString();
                }
            }

            //get DO number
            DataTable _dtDO = CHNLSVC.Inventory.GetDODetByInvItem(BaseCls.GlbUserComCode, InvNo, ItemCode);
            if (_dtDO.Rows.Count > 0)
                txtDono.Text = _dtDO.Rows[0]["itb_doc_no"].ToString();
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //kapila 24/7/2014 check aloow no of pending RCC
                Int32 _allowCount = 0;
                Int32 _pendingCount = 0;
                DataTable _DTallowRcc = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (_DTallowRcc != null)
                {
                    _allowCount = Convert.ToInt32(_DTallowRcc.Rows[0]["ML_FWSALE_QTY"]);
                    if (_allowCount > 0)
                    {
                        DataTable _DTNoRcc = CHNLSVC.Inventory.GetNoOfPendingRCC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToDateTime(txtDate.Value).Date);

                        if (_DTNoRcc != null) _pendingCount = _DTNoRcc.Rows.Count;
                        if (_allowCount <= _pendingCount)
                        {
                            MessageBox.Show("You are not allowed to raised RCC(s) more than " + _allowCount, "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                }
                ClearRccComplete();
                ClearItem();
                clearCust();
                ClearRccJob();
                ClearRccRepair();

                txtRCC.Enabled = false;
                btnSearch_Rcc.Enabled = false;



                pnlJob.Enabled = false;


                btnAttachDocs.Enabled = true;

                txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

                RccStage = 0;

                _scvDefList = new List<Service_Req_Def>();

                btnNew.Enabled = false;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbRccSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RCCSerialSearch _rccSerialSearch = new RCCSerialSearch();
        }

        private void txtAgent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Agent_Click(null, null);
            }
            {
                if (e.KeyCode == Keys.Enter)
                    btnSearch_Click(null, null);
            }
        }


        protected void IsValidManualNo(object sender, EventArgs e)
        {
            try
            {
                if (txtManual.Text != "")
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "MDOC_RCC", Convert.ToInt32(txtManual.Text));
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtManual.Text = "";
                        txtManual.Focus();
                    }
                }
                else
                {
                    if (chkManual.Checked == true)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtManual.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtManual_Leave(object sender, EventArgs e)
        {
            IsValidManualNo(null, null);
        }

        private void txtAgent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAgent.Text))
                {
                    Boolean _isAgent = CHNLSVC.Sales.IsCheckServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                    if (_isAgent == false)
                    {
                        MessageBox.Show("Invalid Service Agent !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAgent.Focus();
                    }
                    else
                    {
                        if (BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                        {
                            Boolean _isAllowAgent = CHNLSVC.Inventory.IsValidServiceAgent(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtAgent.Text);

                            if (_isAllowAgent == false)
                            {
                                MessageBox.Show("You cannot raise RCC for this agent. Contact inventory department !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtAgent.Focus();
                                return;
                            }
                        }

                    }

                    GetServiceChannel();

                }
                else
                {

                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void GetServiceChannel()
        {
            _serChannel = "";
            _serChannelComp = "";
            _serLocation = "";

            _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

            DataTable _dtserLoc = CHNLSVC.Sales.GetServiceAgentbyLoc(BaseCls.GlbUserComCode, txtAgent.Text);
            if (_dtserLoc != null)
            {
                _serLocation = _dtserLoc.Rows[0]["mbe_acc_cd"].ToString();
                DataTable _dtLoc = null;
                _dtLoc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, _serLocation);
                if (_dtLoc.Rows.Count != 0)
                {
                    _serChannel = _dtLoc.Rows[0]["ml_cate_3"].ToString();
                    _serChannelComp = _dtLoc.Rows[0]["ml_com_cd"].ToString();
                }

                MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(null, _serLocation);
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_anal1 == "SCM2")
                        _isOnlineSCM2 = true;
                    else
                        _isOnlineSCM2 = false;

                    if (string.IsNullOrEmpty(_serChannel))
                        _dtLoc = CHNLSVC.Inventory.Get_location_by_code(_mstLoc.Ml_com_cd, _serLocation);
                    if (_dtLoc.Rows.Count != 0)
                    {
                        _serChannel = _dtLoc.Rows[0]["ml_cate_3"].ToString();
                        _serChannelComp = _dtLoc.Rows[0]["ml_com_cd"].ToString();
                    }
                }
                else
                {
                    if (BaseCls.GlbUserComCode == "LRP")
                    {
                        _mstLoc = CHNLSVC.General.GetLocationInfor("ABL", _serLocation);
                        if (_mstLoc != null)
                        {
                            if (_mstLoc.Ml_anal1 == "SCM2")
                                _isOnlineSCM2 = true;
                            else
                                _isOnlineSCM2 = false;
                        }
                    }
                }
            }




            _isDocAttachMan = false;
            if (!string.IsNullOrEmpty(_serLocation))
            {
                Service_Chanal_parameter oChnlPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, _serLocation);
                if (oChnlPara != null)
                {
                    if (oChnlPara.SP_IS_RCC_DOC_ATTACH == 1)        //11/5/2015
                        _isDocAttachMan = true;
                }
                //_scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                //if (_scvParam != null)
                //    if (_scvParam.SP_IS_RCC_DOC_ATTACH == 1)
                //        _isDocAttachMan = true;
            }
        }

        private void txtInsp_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtEasyLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chkRepaired_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(false, sender, e);
        }

        private void RCC_Entry_Load(object sender, EventArgs e)
        {
            BackDatePermission();
            _scvDefList = new List<Service_Req_Def>();
            //_scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());

        }

        private void chkReq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReq.Checked == true)
            {
                pnlItem.Enabled = true;
                // pnlCust.Enabled = true;
                txtCustName.Enabled = true;
                txtInvDate.Enabled = true;
                txtInvoice.Enabled = true;
                txtAccNo.Enabled = true;

                txtCustName.Enabled = true;
                //  txtCustAddr.Enabled = true;
                txtEmail.Enabled = true;

                btnItemSearch.Enabled = true;
                btnSearch.Enabled = false;
            }
            else
            {
                pnlItem.Enabled = false;
                //   pnlCust.Enabled = false
                txtCustName.Enabled = false;
                txtInvDate.Enabled = false;
                txtInvoice.Enabled = false;
                txtAccNo.Enabled = false;

                txtCustName.Enabled = false;
                //    txtCustAddr.Enabled = false;
                txtEmail.Enabled = false;
                btnItemSearch.Enabled = false;
                btnSearch.Enabled = true;
            }
            clearAll();
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
                if (txtItem.Text != "")
                {
                    LoadItemDetails();
                    MasterItem msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    ItemType = msitem.Mi_itm_tp;
                    ItemCat = msitem.Mi_cate_1;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }


        private void LoadItemDetails()
        {
            try
            {
                if (txtItem.Text != "")
                {
                    FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.ToUpper());
                    if (_item != null)
                    {
                        txtItemDesn.Text = _item.Mi_shortdesc;
                        txtModel.Text = _item.Mi_model;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtItem.Text = "";
                        txtItem.Focus();
                        txtItemDesn.Text = "";
                        txtModel.Text = "";
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRej_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 X = CHNLSVC.Inventory.UpdateRCCStatus(0, 1, BaseCls.GlbUserID, Convert.ToDateTime(txtDate.Value).Date, txtRCC.Text.ToString());
                    MessageBox.Show("Successfully Completed.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 X = CHNLSVC.Inventory.UpdateRCCStatus(1, 0, BaseCls.GlbUserID, Convert.ToDateTime(txtDate.Value).Date, txtRCC.Text.ToString());
                    MessageBox.Show("Successfully Completed.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string hdnAllowBin = "0";
            string _outDocNo = "";
            string documntNo = "";
            string _outLoc = "";
            string _inLoc = "";
            Int32 result = 0;
            InventoryHeader invHdr = new InventoryHeader();
            List<ReptPickSerials> PickSerials = new List<ReptPickSerials>();
            Boolean _noAodInPrint = false;
            List<ReptPickSerialsSub> PickSerialsSub = new List<ReptPickSerialsSub>();

            //bool _allowCurrentTrans = false;
            //if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            //{
            //    if (_allowCurrentTrans == true)
            //    {
            //        if (txtDate.Value.Date != DateTime.Now.Date)
            //        {
            //            if (RccStage == 0)
            //            {
            //                //txtDate.Enabled = true;
            //                MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                txtDate.Focus();
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (RccStage == 0)
            //        {
            //            txtDate.Enabled = true;
            //            MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtDate.Focus();
            //            return;
            //        }
            //    }
            //}

            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    #region AOD IN
                    btnConfirm.Enabled = false;

                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        if (CHNLSVC.Inventory.IsAgentParaExist(BaseCls.GlbUserComCode, txtAgent.Text) == false)    //21/4/2015
                        {
                            result = 1;
                            goto NoAODin;
                        }
                    }
                    //get outward doc no of the rcc
                    DataTable dt = null;
                    if (_isExternal == true)
                        dt = CHNLSVC.Inventory.GetDocNoByJobNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtRCC.Text, out _outDocNo, Is_External);
                    else
                        dt = CHNLSVC.Inventory.GetDocNoByJobNo(BaseCls.GlbUserComCode, _serLocation, txtRCC.Text, out _outDocNo, Is_External);
                    if (dt.Rows.Count == 0)
                    {
                        if (BaseCls.GlbUserComCode == "SGL")   //28/8/2014
                        {
                            if (CHNLSVC.Inventory.IsAgentParaExist(BaseCls.GlbUserComCode, txtAgent.Text) == true)    //21/4/2015
                            {
                                DataTable dt1 = CHNLSVC.Inventory.GetOutDocNoByJobNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtRCC.Text, out _outDocNo, Is_External);
                                if (dt1.Rows.Count == 0)
                                {
                                    MessageBox.Show("AOD Out not found for this RCC!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                    _outLoc = Convert.ToString(dt1.Rows[0]["ith_loc"]);
                            }
                            else
                            {
                                result = 1;
                                _noAodInPrint = true;
                                goto NoAODin;
                            }
                        }
                        else
                        {
                            MessageBox.Show("AOD Out not found for this RCC!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        _outLoc = Convert.ToString(dt.Rows[0]["ith_loc"]);
                        _inLoc = Convert.ToString(dt.Rows[0]["ith_oth_loc"]);
                    }


                    if (_inLoc == BaseCls.GlbUserDefLoca)
                    {
                        //get serials of that doc no
                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        if (_mstLoc != null)
                        {
                            if (_mstLoc.Ml_allow_bin == false)
                            {
                                hdnAllowBin = "0";
                            }
                            else
                            {
                                hdnAllowBin = "1";
                            }
                        }

                        this.Cursor = Cursors.WaitCursor;


                        invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                        invHdr.Ith_com = BaseCls.GlbUserComCode;
                        invHdr.Ith_oth_docno = _outDocNo;
                        invHdr.Ith_doc_date = DateTime.Now.Date;
                        invHdr.Ith_doc_year = (DateTime.Now.Date).Year;

                        invHdr.Ith_doc_tp = "AOD";
                        invHdr.Ith_cate_tp = "NOR";
                        invHdr.Ith_sub_tp = "SERVICE";

                        //invHdr.Ith_oth_com =
                        invHdr.Ith_is_manual = false;
                        invHdr.Ith_stus = "A";
                        invHdr.Ith_cre_by = BaseCls.GlbUserID;
                        invHdr.Ith_mod_by = BaseCls.GlbUserID;
                        invHdr.Ith_direct = true;
                        invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                        invHdr.Ith_manual_ref = "N/A";
                        invHdr.Ith_remarks = "";
                        invHdr.Ith_vehi_no = "";
                        invHdr.Ith_anal_12 = false;
                        invHdr.Ith_sub_docno = txtRCC.Text;

                        invHdr.Ith_oth_com = BaseCls.GlbUserComCode;
                        invHdr.Ith_oth_loc = _outLoc;
                        string _unavailableitemlist = string.Empty;

                        ReptPickHeader _reptPickHdr = new ReptPickHeader();
                        _reptPickHdr.Tuh_direct = true;
                        _reptPickHdr.Tuh_doc_no = _outDocNo; //Outward Doc No
                        _reptPickHdr.Tuh_doc_tp = "AOD"; //Doc Type
                        _reptPickHdr.Tuh_ischek_itmstus = false;
                        _reptPickHdr.Tuh_ischek_reqqty = true;
                        _reptPickHdr.Tuh_ischek_simitm = false;
                        _reptPickHdr.Tuh_session_id = BaseCls.GlbUserSessionID;//Session ID
                        _reptPickHdr.Tuh_usr_com = BaseCls.GlbUserComCode; //Company
                        _reptPickHdr.Tuh_usr_id = BaseCls.GlbUserID; //User Name

                        if (Is_External == 0)
                        {
                            DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(BaseCls.GlbUserComCode, _outDocNo);
                            if (_headerchk != null && _headerchk.Rows.Count > 0)
                            {
                                string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                                string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));
                                if (!string.IsNullOrEmpty(_headerUser))
                                    if (BaseCls.GlbUserID.Trim() != _headerUser.Trim())
                                    {
                                        MessageBox.Show("Document " + _outDocNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate, "Scanned Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //return;
                                    }
                            }


                        }

                        PickSerials = CHNLSVC.Inventory.GetOutwarditems(BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, _reptPickHdr, out _unavailableitemlist);

                        MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                        masterAutoNum.Aut_moduleid = "AOD";
                        masterAutoNum.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                        masterAutoNum.Aut_cate_tp = "LOC";
                        masterAutoNum.Aut_direction = 1;
                        masterAutoNum.Aut_modify_dt = null;
                        masterAutoNum.Aut_year = DateTime.Now.Year;
                        masterAutoNum.Aut_start_char = "AOD";

                        if (PickSerials == null || PickSerials.Count == 0)
                        {
                            MessageBox.Show("Serials not found for the AOD out!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        // result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerials, PickSerialsSub, masterAutoNum, out documntNo);
                        result = 1;
                    }
                    else
                        result = 1;

                    #endregion

                NoAODin:

                    #region FILL RCC OBJECT
                    if (result > 0)
                    {
                        string _errorMsg = "";
                        RCC _RCC = new RCC();

                        _RCC.Inr_no = txtRCC.Text;
                        _RCC.Inr_complete_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_complete_by = BaseCls.GlbUserID;

                        _RCC.Inr_cre_by = BaseCls.GlbUserID;
                        _RCC.Inr_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_mod_by = BaseCls.GlbUserID;
                        _RCC.Inr_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_session_id = BaseCls.GlbUserSessionID;

                        int row_aff = CHNLSVC.Inventory.Update_RCC_complete(_RCC, invHdr, PickSerials, PickSerialsSub, out _errorMsg);

                        this.Cursor = Cursors.Default;
                        clearAll();
                        MessageBox.Show("Successfully Updated", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //if (_noAodInPrint == false) //28/8/2014
                        //{
                        //    BaseCls.GlbReportName = string.Empty;
                        //    GlbReportName = string.Empty;
                        //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //    BaseCls.GlbReportTp = "INWARD";
                        //    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Inward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                        //    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Inward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                        //    _view.GlbReportDoc = documntNo;
                        //    _view.Show();
                        //    _view = null;
                        //}

                    }
                    else
                    {

                        this.Cursor = Cursors.Default;
                        clearAll();
                        MessageBox.Show(documntNo, "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    #endregion


                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbColMethod_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtAgent.Focus();
            }
        }

        private void cmbCond_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void cmbDefect_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void cmbAcc_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void txtInsp_KeyDown_1(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void txtEasyLoc_KeyDown_1(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void cmbReason_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void cmbRetCond_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void cmbClosureType_KeyDown(object sender, KeyEventArgs e)
        {
            {

            }
        }

        private void txtJob1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtExecName.Focus();
            }
        }

        private void txtJob2_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtExeContNo.Focus();
            }
        }

        private void txtOrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            //{
            //    if (e.KeyCode == Keys.Enter)
            //        txtDispatchNo.Focus();
            //}
        }

        private void txtDispatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtHologram.Focus();
            }
        }

        private void txtAgent_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Agent_Click(null, null);
        }

        private void txtRCC_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Rcc_Click(null, null);
        }

        private void txtRCC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Rcc_Click(null, null);
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnItemSearch_Click(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItemSearch_Click(null, null);
        }

        private void txtRem_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    btnUpd_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //InventoryHeader _inventoryHeader = new InventoryHeader();
            //#region Inventory Header Value Assign
            //_inventoryHeader.Ith_acc_no = string.Empty;
            //_inventoryHeader.Ith_anal_1 = string.Empty;
            //_inventoryHeader.Ith_anal_10 = false;//Direct AOD
            //_inventoryHeader.Ith_anal_11 = false;
            //_inventoryHeader.Ith_anal_12 = false;
            //_inventoryHeader.Ith_anal_2 = string.Empty;
            //_inventoryHeader.Ith_anal_3 = string.Empty;
            //_inventoryHeader.Ith_anal_4 = string.Empty;
            //_inventoryHeader.Ith_anal_5 = string.Empty;
            //_inventoryHeader.Ith_anal_6 = 0;
            //_inventoryHeader.Ith_anal_7 = 0;
            //_inventoryHeader.Ith_anal_8 = Convert.ToDateTime(dateTimePicker1.Value).Date;
            //_inventoryHeader.Ith_anal_9 = Convert.ToDateTime(dateTimePicker1.Value).Date;
            //_inventoryHeader.Ith_bus_entity = string.Empty;
            //_inventoryHeader.Ith_cate_tp = "NOR";
            //_inventoryHeader.Ith_channel = string.Empty;
            //_inventoryHeader.Ith_com = BaseCls.GlbUserComCode;
            //_inventoryHeader.Ith_com_docno = string.Empty;
            //_inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
            //_inventoryHeader.Ith_cre_when = DateTime.Now.Date;
            //_inventoryHeader.Ith_del_add1 = string.Empty;
            //_inventoryHeader.Ith_del_add2 = string.Empty;
            //_inventoryHeader.Ith_del_code = string.Empty;
            //_inventoryHeader.Ith_del_party = string.Empty;
            //_inventoryHeader.Ith_del_town = string.Empty;
            //_inventoryHeader.Ith_direct = false;
            //_inventoryHeader.Ith_doc_date = Convert.ToDateTime(dateTimePicker1.Value);
            //_inventoryHeader.Ith_doc_no = string.Empty;
            //_inventoryHeader.Ith_doc_tp = string.Empty;
            //_inventoryHeader.Ith_doc_year = Convert.ToDateTime(dateTimePicker1.Value).Date.Year;
            //_inventoryHeader.Ith_entry_no = string.Empty;
            //_inventoryHeader.Ith_entry_tp = string.Empty;
            //_inventoryHeader.Ith_git_close = false;
            //_inventoryHeader.Ith_git_close_date = Convert.ToDateTime(dateTimePicker1.Value).Date;
            //_inventoryHeader.Ith_git_close_doc = string.Empty;
            //_inventoryHeader.Ith_is_manual = false;
            //_inventoryHeader.Ith_isprinted = false;
            //_inventoryHeader.Ith_job_no = textBox1.Text;
            //_inventoryHeader.Ith_loading_point = string.Empty;
            //_inventoryHeader.Ith_loading_user = string.Empty;
            //_inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            //_inventoryHeader.Ith_manual_ref = "0";
            //_inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
            //_inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            //_inventoryHeader.Ith_noofcopies = 0;
            //_inventoryHeader.Ith_oth_loc = "VTABL";
            //_inventoryHeader.Ith_oth_docno = string.Empty;
            //_inventoryHeader.Ith_remarks = string.Empty;
            //_inventoryHeader.Ith_sbu = string.Empty;
            ////_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            //_inventoryHeader.Ith_session_id = GlbUserSessionID;
            //_inventoryHeader.Ith_stus = "A";
            //_inventoryHeader.Ith_sub_tp = "SERVICE";    // string.Empty; 10/7/2013
            //_inventoryHeader.Ith_vehi_no = string.Empty;
            //_inventoryHeader.Ith_oth_com = BaseCls.GlbUserComCode;
            //_inventoryHeader.Ith_anal_1 = "0";
            //_inventoryHeader.Ith_anal_2 = string.Empty;
            //#endregion

            //string _message = string.Empty;
            //string _genInventoryDoc = string.Empty;
            //string _genSalesDoc = string.Empty;

            //MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            //_inventoryAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            //_inventoryAuto.Aut_cate_tp = "LOC";
            //_inventoryAuto.Aut_direction = null;
            //_inventoryAuto.Aut_modify_dt = null;
            //_inventoryAuto.Aut_moduleid = string.Empty;
            //_inventoryAuto.Aut_start_char = string.Empty;
            //_inventoryAuto.Aut_modify_dt = null;
            //_inventoryAuto.Aut_year = Convert.ToDateTime(txtDate.Value).Year;

            ////Serials
            //List<ReptPickSerials> _serialList = new List<ReptPickSerials>();
            ////string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            //ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, textBox2.Text.ToString(),Convert.ToInt32( textBox3.Text));
            //_reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
            //_reptPickSerial_.Tus_usrseq_no = 1;
            //_reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
            //_reptPickSerial_.Tus_base_doc_no = "N/A";
            //_reptPickSerial_.Tus_base_itm_line = 0;
            //_reptPickSerial_.Tus_new_remarks = "AOD-OUT";       //kapila

            //MasterItem msitem = new MasterItem();
            //msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, textBox2.Text.ToString());
            //_reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
            //_reptPickSerial_.Tus_itm_model = msitem.Mi_model;
            //_serialList.Add(_reptPickSerial_);


            //Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, null, _inventoryHeader, _inventoryAuto, null, null, _serialList, null, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            _view.GlbReportName = "ACInsReqPrint.rpt";
            BaseCls.GlbReportName = "ACInsReqPrint.rpt";
            _view.GlbReportDoc = txtRCC.Text;
            BaseCls.GlbReportDoc = txtRCC.Text;
            _view.Show();
            _view = null;
            //try
            //{

            //    if (lblCurStatus.Text != "Pending")
            //    {
            //        MessageBox.Show("This is not a pending request", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            //    Cursor.Current = Cursors.WaitCursor;

            //    Int32 _eff = CHNLSVC.Inventory.CancelAcInstallReq(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtRCC.Text, "C", BaseCls.GlbUserID);
            //    if (_eff == 1)
            //    {
            //        MessageBox.Show("Successfully Cancelled.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        ACInstallRequest formnew = new ACInstallRequest();
            //        formnew.MdiParent = this.MdiParent;
            //        formnew.Location = this.Location;
            //        formnew.Show();
            //        this.Close();
            //    }
            //    Cursor.Current = Cursors.Default;

            //}
            //catch (Exception err)
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseChannel();
            //    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {
            //check serial availability
            if (string.IsNullOrEmpty(txtSerial.Text)) return;
            if (string.IsNullOrEmpty(txtItem.Text)) { MessageBox.Show("Please select the item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSerial.Clear(); txtItem.Clear(); txtItem.Focus(); return; }
            bool _isExist = CHNLSVC.Inventory.IsUserEntryExist(BaseCls.GlbUserComCode, txtItem.Text.Trim(), "SERIAL", txtSerial.Text.Trim());
            if (_isExist)
            {
                MessageBox.Show("Selected serial already available. Please check for the serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSerial.Clear();
                return;
            }
        }

        private void txtWarranty_Leave(object sender, EventArgs e)
        {
            //check warranty availability
            if (string.IsNullOrEmpty(txtWarranty.Text)) return;
            if (string.IsNullOrEmpty(txtItem.Text)) { MessageBox.Show("Please select the item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtWarranty.Clear(); txtItem.Clear(); txtItem.Focus(); return; }
            bool _isExist = CHNLSVC.Inventory.IsUserEntryExist(BaseCls.GlbUserComCode, txtItem.Text.Trim(), "WARRANTY", txtWarranty.Text.Trim());
            if (_isExist)
            {
                MessageBox.Show("Selected warranty already available. Please check for the warranty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWarranty.Clear();
                return;
            }
        }

        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            //check invoice availability


        }

        private void btnPay_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {

        }

        private void grvCharge_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (_lstCharge[e.RowIndex].Scg_is_mand == true)
                {
                    MessageBox.Show("Selected charge type cannot be removed. !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Decimal _amt = Convert.ToDecimal(_lstCharge[e.RowIndex].Scg_rate);


                    _lstCharge.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _lstCharge;

                }
            }
        }

        private void btnCloseDef_Click(object sender, EventArgs e)
        {

        }

        private void btnDef_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ItemCat))
                MessageBox.Show("Invalid item category !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

            }
        }

        private void btn_srch_def_type_Click(object sender, EventArgs e)
        {

        }

        private void txtDef_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_def_type_Click(null, null);
        }

        private void txtDef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_def_type_Click(null, null);
        }

        private void txtDef_Leave(object sender, EventArgs e)
        {

        }


        private void btn_add_def_Click(object sender, EventArgs e)
        {


        }

        private void grvDef_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTel.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtTel.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid contact number.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTel.Text = "";
                    txtTel.Focus();
                    return;
                }
            }
        }

        private void txtContNo_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtContNo.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtContNo.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid contact number.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContNo.Text = "";
                    txtContNo.Focus();
                    return;
                }
            }
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                //6/2/2015
                if (Is_Dealer_Inv == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    General.CustomerCreation _CusCre = new General.CustomerCreation();
                    _CusCre._isFromOther = true;
                    _CusCre.obj_TragetTextBox = txtcustCode;
                    this.Cursor = Cursors.Default;
                    _CusCre.ShowDialog();
                    txtcustCode.Select();
                    CustCode = txtcustCode.Text;
                    load_customer(CustCode);
                }
                else
                {
                    MessageBox.Show("You cannot create new customer", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            { txtcustCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void load_customer(string _code)
        {
            if (_code != "CASH")
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(_code, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    txtcustCode.Text = _masterBusinessCompany.Mbe_cd;
                    CustCode = txtcustCode.Text;
                    txtCustAddr.Text = _masterBusinessCompany.Mbe_add1 + " " + _masterBusinessCompany.Mbe_add2;
                    txtCustName.Text = _masterBusinessCompany.Mbe_name;
                    txtTel.Text = _masterBusinessCompany.Mbe_mob;
                    txtPhone.Text = _masterBusinessCompany.Mbe_tel;
                    txtEmail.Text = _masterBusinessCompany.Mbe_email;
                }
                else
                {
                    txtCustAddr.Text = "";
                    txtCustName.Text = "";
                    txtTel.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                }
            }
        }

        private void btnSearch_CustCode_Click(object sender, EventArgs e)
        {
            try
            {
                //6/2/2015

                if (Is_Dealer_Inv == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                    _commonSearch.ReturnIndex = 0;
                    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _commonSearch.dvResult.DataSource = _result;
                    _commonSearch.BindUCtrlDDLData(_result);
                    _commonSearch.obj_TragetTextBox = txtcustCode;
                    _commonSearch.IsSearchEnter = true;
                    this.Cursor = Cursors.Default;
                    _commonSearch.ShowDialog();
                    //txtcustCode.Select();
                    load_customer(txtcustCode.Text);
                }
                else
                {
                    MessageBox.Show("You cannot search customer", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }


        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPhone.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtPhone.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid contact number.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Text = "";
                    txtPhone.Focus();
                    return;
                }
            }
        }

        private void btnAttachDocs_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtRCC.Text))
            //{
            //    SystemWarnningMessage("Select the RCC Number", "Installation Request");
            //    return;
            //}

            //get pending RCCs
            //string _chnl = "";
            //DataTable _dtRcc = CHNLSVC.Inventory.GetPendingRCC(BaseCls.GlbUserComCode);
            //foreach (DataRow dr in _dtRcc.Rows)
            //{
            //    //get the channel
            //    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, dr["inr_loc_cd"].ToString());
            //    _chnl = dt_location.Rows[0]["ml_cate_2"].ToString();

            //    //get the maximum period defined for the channel
            //    DataTable dt_stus = CHNLSVC.General.get_stus_chnl(BaseCls.GlbUserComCode, _chnl);
            //    Int32 _maxPeriod = Convert.ToInt32(dt_stus.Rows[0]["msc_rcc_can_prd"]);

            //    //check exceed the period
            //    if (Convert.ToInt32(DateTime.Now.Date - Convert.ToDateTime(dr["inr_dt"])) > _maxPeriod)
            //    {
            //    }
            //    //if so cancell

            //}

            if (string.IsNullOrEmpty(_serLocation))
            {
                SystemWarnningMessage("Select the service agent", "Installation Request");
                return;
            }
            pnlImageUpload.Visible = true;
        }

        private void btnAttachDocs_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCloseImgUp_Click(object sender, EventArgs e)
        {
            pnlImageUpload.Visible = false;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            FileInfo oFileInfo = new FileInfo(txtFilePath.Text);
            if (oMainList.FindAll(x => x.ImagePath == txtFilePath.Text).Count > 0)
            {
                MessageBox.Show("This image is already added.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFilePath.Clear();
                return;
            }

            ImageUploadDTO oItem = new ImageUploadDTO();
            oItem.ImagePath = txtFilePath.Text.Trim();
            oItem.JobLine = 1;
            oItem.JobNumber = "";
            oItem.image = ReadFile(txtFilePath.Text.Trim());
            oItem.FileName = oFileInfo.Name;
            oMainList.Add(oItem);

            dgvFiles.DataSource = new List<ImageUploadDTO>();
            dgvFiles.DataSource = oMainList;

            txtFilePath.Clear();
            if (MessageBox.Show("Do you want to add new item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                btnSearchFile_Click(null, null);
            else
                pnlImageUpload.Visible = false;

        }

        private byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image file";
            theDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //  pictureBox1.Image = new Bitmap(theDialog.FileName);
                txtFilePath.Text = theDialog.FileName;
            }
        }

        private void btnClearImg_Click(object sender, EventArgs e)
        {
            dgvFiles.DataSource = new List<ImageUploadDTO>();
            txtFileSize.Text = "";
            txtFilePath.Text = "";
        }

        private void btn_srch_rcctype_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MasterType);
            _result = CHNLSVC.CommonSearch.SearchMasterType(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRCCType;
            _CommonSearch.ShowDialog();
            cmbColMethod.Select();

            //   load_rcc_type();
        }

        private void load_rcc_type()
        {
            //DataTable _dt = CHNLSVC.Inventory.GetMasterTypes(txtRCCType.Text);
            //if (_dt.Rows.Count > 0)
            //{
            //    lblRCCType.Text = _dt.Rows[0]["mtp_desc"].ToString();
            //    load_rcc_sub_type();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid RCC Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    lblRCCType.Text = "";
            //    txtRCCType.Focus();
            //}

        }

        private void txtRCCType_Leave(object sender, EventArgs e)
        {

        }

        private void txtRCCType_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    cmbColMethod.Focus();
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

        private void dgvDelDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDelDetails.Rows.Count > 0)
            {
                Int32 _rowIndex = e.RowIndex;
                if (_rowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Do you need to remove this item ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dgvDelDetails.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (_isExternal == true)        //kapila 27/12/2016
            {
                if (dgvDelDetails.Rows.Count > 0)
                {
                    MessageBox.Show("Already added a serial", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (String.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the details.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!String.IsNullOrEmpty(txtTel.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtTel.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTel.Text = "";
                    txtTel.Focus();
                    return;
                }
            }
            if (txtSerial.Text != "" && txtSerial.Text != "NA" && txtSerial.Text != "N/A")
            {
                Boolean _IsSerialFound = CHNLSVC.Inventory.IsRccSerialFound(txtItem.Text, txtSerial.Text);
                if (_IsSerialFound == true)
                {
                    RCC __RCC = null;
                    __RCC = CHNLSVC.Inventory.GetRCCbySerial(txtItem.Text, txtSerial.Text);
                    MessageBox.Show("Another Request found for this serial number. RCC # is " + __RCC.Inr_no, "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }

            foreach (DataGridViewRow row in dgvDelDetails.Rows)
            {
                if (chkPending.Checked == true)
                {
                    if (row.Cells[10].Value.ToString().Equals(txtLine.Text) && row.Cells[1].Value.ToString().Equals(txtInvoice.Text))
                    {
                        MessageBox.Show("Already added !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }
                else
                {
                    if (row.Cells[8].Value.ToString().Equals(txtSerial.Text))
                    {
                        MessageBox.Show("Already added !", "Installation Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }
            }

            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            DataRow dr;
            DataTable _tmp = new DataTable();

            _tmp.Columns.Add("invno", typeof(string));
            _tmp.Columns.Add("invdate", typeof(DateTime));
            _tmp.Columns.Add("dono", typeof(string));
            _tmp.Columns.Add("dodate", typeof(DateTime));
            _tmp.Columns.Add("accno", typeof(string));
            _tmp.Columns.Add("item", typeof(string));
            _tmp.Columns.Add("qty", typeof(Int32));
            _tmp.Columns.Add("serial", typeof(string));
            _tmp.Columns.Add("warranty", typeof(string));
            _tmp.Columns.Add("invline", typeof(Int32));

            if (chkPending.Checked == true && Convert.ToInt32(txtQty.Text) > 1)
            {
                for (int i = 0; i < Convert.ToInt32(txtQty.Text); i++)
                {
                    dr = _tmp.NewRow();
                    dr["invno"] = txtInvoice.Text;
                    if (!string.IsNullOrEmpty(txtInvDate.Text))
                        dr["invdate"] = Convert.ToDateTime(txtInvDate.Text);
                    dr["dono"] = txtDono.Text;
                    if (!string.IsNullOrEmpty(txtDOdate.Text))
                        dr["dodate"] = Convert.ToDateTime(txtDOdate.Text);
                    else
                        dr["dodate"] = Convert.ToDateTime("01/Dec/2999");
                    dr["accno"] = txtAccNo.Text;
                    dr["item"] = txtItem.Text;
                    if (!string.IsNullOrEmpty(txtQty.Text))
                        dr["qty"] = 1;
                    dr["serial"] = txtSerial.Text;
                    dr["warranty"] = txtWarranty.Text;
                    if (!string.IsNullOrEmpty(txtLine.Text))
                        dr["invline"] = Convert.ToInt32(txtLine.Text);
                    _tmp.Rows.Add(dr);
                }
            }
            else
            {
                dr = _tmp.NewRow();
                dr["invno"] = txtInvoice.Text;
                if (!string.IsNullOrEmpty(txtInvDate.Text))
                    dr["invdate"] = Convert.ToDateTime(txtInvDate.Text);
                dr["dono"] = txtDono.Text;
                if (!string.IsNullOrEmpty(txtDOdate.Text))
                    dr["dodate"] = Convert.ToDateTime(txtDOdate.Text);
                else
                    dr["dodate"] = Convert.ToDateTime("01/Dec/2999");
                dr["accno"] = txtAccNo.Text;
                dr["item"] = txtItem.Text;
                if (!string.IsNullOrEmpty(txtQty.Text))
                    dr["qty"] = 1;
                dr["serial"] = txtSerial.Text;
                dr["warranty"] = txtWarranty.Text;
                if (!string.IsNullOrEmpty(txtLine.Text))
                    dr["invline"] = Convert.ToInt32(txtLine.Text);
                _tmp.Rows.Add(dr);
            }
            param.Merge(_tmp);

            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = param;

            ClearItem();


        }

        private void txtItem_DoubleClick_1(object sender, EventArgs e)
        {
            btnItemSearch_Click(null, null);
        }

        private void txtItem_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItemSearch_Click(null, null);
        }


    }
}


