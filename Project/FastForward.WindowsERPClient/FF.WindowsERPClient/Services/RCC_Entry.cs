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
    public partial class RCC_Entry : Base
    {
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

        public RCC_Entry()
        {
            try
            {
                InitializeComponent();
                InitializeValuesNDefaultValueSet();
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "RCCAPP1"))
                {
                    lblRcc.Text = "RCC Details (Approval)";
                    btnUpd.Enabled = false;
                }
                else
                {
                    lblRcc.Text = "RCC Details";
                    btnUpd.Enabled = true;
                }
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
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out  _allowCurrentTrans);
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

            InvNo = string.IsNullOrEmpty(_InvNo) ? string.Empty : (_InvNo.ToUpper() == "N/A"? string.Empty : (_InvNo.ToUpper() == "NA" ? string.Empty : _InvNo));
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

            MasterItem msitem = CHNLSVC.Inventory.GetItem("", ItemCode);
            ItemType = msitem.Mi_itm_tp;
            ItemCat = msitem.Mi_cate_1;

            pnlCharge.Visible = false;
            if (txtRCCType.Text == "CUST")
                pnlCharge.Visible = true;
            else if (txtRCCType.Text == "STK")
            {
                DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster(ItemStatus, "ALL");
                if (_status.Rows.Count > 0)
                {
                    if (Convert.ToInt32(_status.Rows[0]["MIS_IS_PAY_MAN"]) == 1)
                        pnlCharge.Visible = true;
                }
            }

            //GetInvoiceDetails(_seqno, _itemLine, _bachLine, _sLine);
        }

        protected void LoadRCCDetails(out bool _isvalid)
        {
            _isvalid = true;
            try
            {
                RCC _RCC = null;
                _RCC = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text);

                //add aby akila 2018/01/02
                if (_RCC == null || string.IsNullOrEmpty(_RCC.Inr_no))
                {
                    MessageBox.Show("RCC details not found. Please check the RCC #", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _isvalid = false;
                    return;
                }


                //Tharindu 2017-12-21
                if(_RCC.Inr_loc_cd != BaseCls.GlbUserDefLoca)
                {
                    MessageBox.Show("This Rcc is not belongs this location", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _isvalid = false;
                    return;
                }
           
                if (_RCC.Inr_stus == "C")
                {
                    MessageBox.Show("This Rcc is Canceled", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _isvalid = false;
                    txtRCC.Text = string.Empty;
                    return;
                }



                cmbJobType.SelectedValue = _RCC.Inr_sub_tp;
                txtRCCType.Text = _RCC.Inr_tp;
                load_rcc_type();
                cmbRccSubType.SelectedValue = _RCC.Inr_sub_tp;
                txtRCC.Text = _RCC.Inr_no;

                txtDate.Text = _RCC.Inr_dt.ToShortDateString();
                txtDate.Enabled = false;
                txtManual.Text = _RCC.Inr_manual_ref;

                txtAccNo.Text = (_RCC.Inr_acc_no == null) ? string.Empty : _RCC.Inr_acc_no;
                txtCustAddr.Text = (_RCC.Inr_addr == null) ? string.Empty : _RCC.Inr_addr;
                txtcustCode.Text = (_RCC.Inr_cust_cd == null) ? string.Empty : _RCC.Inr_cust_cd;
                txtCustName.Text = (_RCC.Inr_cust_name == null) ? string.Empty : _RCC.Inr_cust_name;
                txtInvDate.Text = (_RCC.Inr_inv_dt.ToString() == null) ? string.Empty : _RCC.Inr_inv_dt.ToString("dd/MMM/yyyy");
                txtInvoice.Text = (_RCC.Inr_inv_no == null) ? string.Empty : _RCC.Inr_inv_no;
                txtTel.Text = (_RCC.Inr_tel == null) ? string.Empty : _RCC.Inr_tel;


                Service_Req_Hdr _reqHdr = CHNLSVC.CustService.GetServiceReqHeader(BaseCls.GlbUserComCode, _RCC.Inr_no);
                if (_reqHdr != null)
                {
                    txtPhone.Text = _reqHdr.Srb_b_phno;
                    txtContLoc.Text = _reqHdr.Srb_cnt_add1;
                    txtContNo.Text = _reqHdr.Srb_cnt_phno;
                    txtContPersn.Text = _reqHdr.Srb_cnt_person;
                    txtEmail.Text = _reqHdr.Srb_email;
                }
                else
                {
                    txtPhone.Text = "";
                    txtContLoc.Text = "";
                    txtContNo.Text = "";
                    txtContPersn.Text = "";
                    txtEmail.Text = "";
                }

                txtEasyLoc.Text = (_RCC.Inr_easy_loc == null) ? string.Empty : _RCC.Inr_easy_loc;
                txtInsp.Text = (_RCC.Inr_insp_by == null) ? string.Empty : _RCC.Inr_insp_by;
                txtItem.Text = (_RCC.Inr_itm == null) ? string.Empty : _RCC.Inr_itm;
                GetItemData();
                txtRem.Text = (_RCC.Inr_rem1 == null) ? string.Empty : _RCC.Inr_rem1;
                txtRepairRem.Text = (_RCC.Inr_rem2 == null) ? string.Empty : _RCC.Inr_rem2;
                txtCompleteRem.Text = (_RCC.Inr_rem3 == null) ? string.Empty : _RCC.Inr_rem3;
                txtSerial.Text = (_RCC.Inr_ser == null) ? string.Empty : _RCC.Inr_ser;
                txtWarranty.Text = (_RCC.Inr_warr == null) ? string.Empty : _RCC.Inr_warr;
                cmbAcc.SelectedValue = string.IsNullOrEmpty(_RCC.Inr_accessories) ? 0 : Convert.ToInt32(_RCC.Inr_accessories);
                cmbCond.SelectedValue = string.IsNullOrEmpty(_RCC.Inr_condition) ? 0 : Convert.ToInt32(_RCC.Inr_condition);
                cmbDefect.SelectedValue = string.IsNullOrEmpty(_RCC.Inr_def_cd) ? 0 : Convert.ToInt32(_RCC.Inr_def_cd);
                Is_External = Convert.ToInt32(_RCC.INR_IS_EXTERNAL);
                Is_In = Convert.ToInt32(_RCC.Inr_in_stus);
                Is_Out = Convert.ToInt32(_RCC.Inr_out_stus);

                txtAgent.Text = _RCC.Inr_agent;

                cmbColMethod.SelectedValue = Convert.ToInt32(_RCC.Inr_col_method);


                txtJob1.Text = (_RCC.Inr_jb_no == null) ? string.Empty : _RCC.Inr_jb_no;

                //9/2/2015 load job stage 
                DataTable _dt = CHNLSVC.CustService.getJobStageByJobNo(txtJob1.Text, BaseCls.GlbUserComCode, Convert.ToInt32(_isOnlineSCM2));
                if (_dt.Rows.Count > 0)
                    lblJobStage.Text = _dt.Rows[0]["jbs_desc"].ToString();
                else
                    lblJobStage.Text = "Job Pending";

                txtJob2.Text = (_RCC.Inr_anal7 == null) ? string.Empty : _RCC.Inr_anal7;
                txtOrdNo.Text = (_RCC.Inr_anal1 == null) ? string.Empty : _RCC.Inr_anal1;
                txtDispatchNo.Text = (_RCC.Inr_anal2 == null) ? string.Empty : _RCC.Inr_anal2;
                txtHologram.Text = (_RCC.Inr_hollogram_no == null) ? string.Empty : _RCC.Inr_hollogram_no;

                txtWarPeriod.Text = _RCC.INR_WAR_PERIOD.ToString();

                RccStage = _RCC.Inr_stage;
                RccNo = _RCC.Inr_no;

                btnRej.Enabled = false;
                btnApp.Enabled = false;

                if (_RCC.Inr_stus == "C")
                    lblCurStatus.Text = "CANCELED";
                else if (_RCC.Inr_stus == "A")
                    lblCurStatus.Text = "APPROVED";
                else if (_RCC.Inr_stus == "R")
                    lblCurStatus.Text = "REJECT";
                else if (_RCC.Inr_stus == "P")
                    lblCurStatus.Text = "PENDING";
                else
                    lblCurStatus.Text = string.Empty;


                if (_RCC.Inr_repair_stus != "")
                    cmbReason.SelectedValue = Convert.ToInt32(_RCC.Inr_repair_stus);

                if (_RCC.Inr_ret_condition != "")
                    cmbRetCond.SelectedValue = Convert.ToInt32(_RCC.Inr_ret_condition);

                //chkRepaired.Checked = _RCC.Inr_is_repaired;
                if (_RCC.Inr_is_repaired)
                {
                    radOk.Checked = true;
                    radNotOk.Checked = false;
                }
                else
                {
                    radNotOk.Checked = true;
                    radOk.Checked = false;
                }

                if (_RCC.Inr_closure_tp != "")
                    cmbClosureType.SelectedValue = Convert.ToInt32(_RCC.Inr_closure_tp);

                if (RccStage == 1)
                    lblStatus.Text = "Raised";

                if (RccStage == 2)
                    lblStatus.Text = "Opened";

                if (RccStage == 3)
                {
                    lblStatus.Text = "Repaired/Returned";
                    if (_RCC.Inr_tp == "STK" || _RCC.Inr_tp == "FIXED")
                    {
                        if (_RCC.Inr_in_stus == true && _RCC.Inr_out_stus == true)
                        {
                            if (_RCC.Inr_sub_tp == "NOR")
                            {
                                btnConfirm.Enabled = true;
                                btnUpd.Enabled = false;
                            }
                            else
                            {
                                btnConfirm.Enabled = false;
                                btnUpd.Enabled = true;
                            }
                        }
                        else
                        {
                            btnConfirm.Enabled = false;
                            btnUpd.Enabled = true;
                        }
                    }
                }
                if (RccStage == 4)
                    lblStatus.Text = "Completed";

                if (RccStage == 5)
                {
                    if (_RCC.Inr_stus == "P")
                    {
                        lblStatus.Text = "Pending Request";
                        btnRej.Enabled = true;
                        btnApp.Enabled = true;
                        btnUpd.Enabled = false;
                    }
                    if (_RCC.Inr_stus == "A")
                    {
                        lblStatus.Text = "Approved Request";
                        btnUpd.Enabled = true;
                    }
                    if (_RCC.Inr_stus == "R")
                    {
                        lblStatus.Text = "Rejected Request";
                    }
                }

                if (_RCC.Inr_stage == 4)
                {
                    chkAddToStock.Visible = true;
            }
                else
                {
                    chkAddToStock.Visible = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isvalid = false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        private void Set_check_Controls(int _stage)
        {
            chkOpen.Enabled = false;
            chkRaise.Enabled = false;
            chkComplete.Enabled = false;
            chkRepair.Enabled = false;

            chkOpen.Checked = false;
            chkRaise.Checked = false;
            chkComplete.Checked = false;
            chkRepair.Checked = false;
            chkPending.Checked = false;
            //btnUpdate.Enabled = true;
            pnlJob.Enabled = false;
            chkManual.Enabled = false;
            pnl_Cond.Enabled = false;
            pnl_Repair.Enabled = false;
            pnl_comp.Enabled = false;
            btnSearch.Enabled = false;

            switch (_stage)
            {
                case 1:
                    {
                        chkOpen.Enabled = true;
                        chkRaise.Checked = true;
                        pnlJob.Enabled = true;
                        btnNew.Enabled = false;
                        pnlJob.Enabled = true;
                        btnSearch.Enabled = true;
                        break;
                    }

                case 2:
                    {
                        chkRepair.Enabled = true;
                        chkOpen.Checked = true;
                        chkRaise.Checked = true;
                        // chkRepair.Checked = true;
                        tabControl1.SelectedIndex = 1;
                        btnNew.Enabled = false;
                        pnl_Repair.Enabled = true;


                        break;
                    }
                case 3:
                    {
                        chkComplete.Enabled = true;
                        chkRaise.Checked = true;
                        chkOpen.Checked = true;
                        chkRepair.Checked = true;
                        //chkComplete.Checked = true;
                        tabControl1.SelectedIndex = 2;
                        btnNew.Enabled = false;
                        pnl_comp.Enabled = true;

                        break;
                    }
                case 4:
                    {
                        chkComplete.Enabled = true;
                        chkRaise.Checked = true;
                        chkOpen.Checked = true;
                        chkRepair.Checked = true;
                        chkComplete.Checked = true;
                        chkPending.Checked = true;
                        tabControl1.SelectedIndex = 0;
                        btnNew.Enabled = false;


                        break;
                    }
                case 5:
                    {
                        chkOpen.Checked = false;
                        chkRepair.Checked = false;
                        chkComplete.Checked = false;
                        chkRaise.Enabled = false;

                        break;
                    }
                default:
                    {
                        chkOpen.Enabled = true;
                        chkOpen.Checked = true;
                        chkRepair.Enabled = true;
                        chkRepair.Checked = true;
                        chkComplete.Enabled = true;
                        chkComplete.Checked = true;
                        chkRaise.Enabled = true;
                        chkRaise.Checked = true;
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
                txtCloseDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

                cmbColMethod.Items.Clear();
                cmbColMethod.DataSource = null;
                cmbColMethod.DataSource = CHNLSVC.Inventory.GetRCCDef("COLMETHOD");
                cmbColMethod.DisplayMember = "ird_desc";
                cmbColMethod.ValueMember = "ird_cd";
                cmbColMethod.SelectedIndex = -1;

                cmbAcc.Items.Clear();
                cmbAcc.DataSource = null;
                cmbAcc.DataSource = CHNLSVC.Inventory.GetRCCDef("ACC");
                cmbAcc.DisplayMember = "ird_desc";
                cmbAcc.ValueMember = "ird_cd";
                cmbAcc.SelectedIndex = -1;

                cmbCond.Items.Clear();
                cmbCond.DataSource = null;
                cmbCond.DataSource = CHNLSVC.Inventory.GetRCCDef("COND");
                cmbCond.DisplayMember = "ird_desc";
                cmbCond.ValueMember = "ird_cd";
                cmbCond.SelectedIndex = -1;

                cmbDefect.Items.Clear();
                cmbDefect.DataSource = null;
                cmbDefect.DataSource = CHNLSVC.Inventory.GetRCCDef("DEF");
                cmbDefect.DisplayMember = "ird_desc";
                cmbDefect.ValueMember = "ird_cd";
                cmbDefect.SelectedIndex = -1;

                cmbRetCond.Items.Clear();
                cmbRetCond.DataSource = null;
                cmbRetCond.DataSource = CHNLSVC.Inventory.GetRCCDef("RETCON");
                cmbRetCond.DisplayMember = "ird_desc";
                cmbRetCond.ValueMember = "ird_cd";
                cmbRetCond.SelectedIndex = -1;

                cmbReason.Items.Clear();
                cmbReason.DataSource = null;
                cmbReason.DataSource = CHNLSVC.Inventory.GetRCCDef("REPDET");
                cmbReason.DisplayMember = "ird_desc";
                cmbReason.ValueMember = "ird_cd";
                cmbReason.SelectedIndex = -1;

                cmbClosureType.Items.Clear();
                cmbClosureType.DataSource = null;
                cmbClosureType.DataSource = CHNLSVC.Inventory.GetRCCDef("CLOSE");
                cmbClosureType.DisplayMember = "ird_desc";
                cmbClosureType.ValueMember = "ird_cd";
                cmbClosureType.SelectedIndex = -1;

                Dictionary<string, string> JobTypes = new Dictionary<string, string>();
                JobTypes.Add("NOR", "Workshop");
                JobTypes.Add("FLD", "Field");
                cmbJobType.DataSource = new BindingSource(JobTypes, null);
                cmbJobType.DisplayMember = "Value";
                cmbJobType.ValueMember = "Key";

                ucPayModes1.InvoiceType = "RCC";
                ucPayModes1.LoadData();
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
            chkAddToStock.Checked = false; //add by akila 2017/12/20
            chkAddToStock.Visible = false;

            ClearRccItem();
            ClearRccJob();
            ClearRccRepair();
            ClearRccComplete();
            txtRCC.Enabled = true;
            btnSearch_Rcc.Enabled = true;

            chkOpen.Checked = false;
            chkRaise.Checked = false;
            chkComplete.Checked = false;
            chkRepair.Checked = false;
            chkPending.Checked = false;

            chkOpen.Enabled = false;
            chkRaise.Enabled = false;
            chkComplete.Enabled = false;
            chkRepair.Enabled = false;
            chkPending.Enabled = false;
            lblWarStus.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblCurStatus.Text = string.Empty;
            txtDate.Enabled = true;
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

            ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.ClearControls();

            ucPayModes1.InvoiceType = "RCC";
            ucPayModes1.LoadData();

            _scvDefList = new List<Service_Req_Def>();
            grvDef.DataSource = null;

            oMainList = new List<ImageUploadDTO>();
            dgvFiles.DataSource = null;
            txtFilePath.Text = "";
            txtFileSize.Text = "";

            btnAttachDocs.Enabled = false;
            _isExternal = false;
            _isOnlineSCM2 = false;

            btnNew.Enabled = true;
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            //updated by akila 2017/12/20
            if (chkAddToStock.Checked && chkComplete.Checked)
            {
                //Updated Rcc - add the excess item to the inventory
                AddExcessItemToStock();
            }
            else
            {
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (txtDate.Value.Date != DateTime.Now.Date)
                        {
                            if (RccStage == 0)
                            {
                                //txtDate.Enabled = true;
                                MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDate.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (RccStage == 0)
                        {
                            txtDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDate.Focus();
                            return;
                        }
                    }


                }

                //kapila 4/7/2014
                if (CheckServerDateTime() == false) return;

                //kapila 5/2/2015
                if (RccStage == 0)
                {
                    if (txtRCCType.Text == "STK")
                    {
                        //kapila 9/4/2016
                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        if (_mstLoc.Ml_cate_2 != "ELITE" && _mstLoc.Ml_cate_2 != "APPLE" && _mstLoc.Ml_cate_2 != "MSR")
                            if (_serLocation == "ES002")    //kapila 27/1/2016
                            {
                                MessageBox.Show("You don't have the permission for stock item for this service center", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        //16/5/2015
                        //if (_serLocation != "RIT1" && _serLocation != "RBIT1" && _serLocation != "RCIT1" && _serLocation != "RGIT1" && _serLocation != "RKIT1" && _serLocation != "RIT1A")
                        if (_isOnlineSCM2 == false)
                        {
                            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10101))
                            {
                                MessageBox.Show("You don't have the permission for stock item.\nPermission Code :- 10101", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                    }
                }

                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                SaveRCC();
                btnNew.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveReceiptHeader(string _rccnum, out string _recNo)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            decimal _valPd = 0;
            ReptPickHeader _SerHeader = new ReptPickHeader();
            List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
            _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
            _ReceiptHeader.Sar_receipt_type = "ADVAN";  //21/8/2015  ADRCC--> ADVAN
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
            _ReceiptHeader.Sar_prefix = "RCC";
            _ReceiptHeader.Sar_manual_ref_no = _rccnum;
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
            _ReceiptHeader.Sar_debtor_cd = txtcustCode.Text;    //4/9/2015
            _ReceiptHeader.Sar_debtor_name = txtCustName.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_1 = txtCustAddr.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_2 = "";
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = ""; // txtMobile.Text.Trim();
            _ReceiptHeader.Sar_nic_no = ""; // txtNIC.Text.Trim();
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = ""; // txtNote.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = "";
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;
            _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
            _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
            _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
            _ReceiptHeader.Sar_anal_1 = ""; // cmbDistrict.Text;
            _ReceiptHeader.Sar_anal_2 = ""; // txtProvince.Text.Trim();
            _ReceiptHeader.Sar_anal_3 = _rccnum;
            _ReceiptHeader.Sar_anal_8 = 0;
            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;
            _ReceiptHeader.SAR_VALID_TO = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(_valPd));


            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            Int32 _line = 0;
            foreach (RecieptItem line in ucPayModes1.RecieptItemList)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _line = _line + 1;
                line.Sard_line_no = _line;
                line.Sard_ref_no = _rccnum;
                _ReceiptDetailsSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "RCC";
            masterAuto.Aut_year = null;

            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = BaseCls.GlbUserDefProf;
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;

            string QTNum = "";
            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, null, null, null,null, masterAutoRecTp, null, out QTNum);
            _recNo = QTNum;

        }
        
        private void SaveRCC()
        {
            try
            {
                string _genInventoryDoc = string.Empty;
                string QTNum = "";
                #region validation

                if (RccStage == 0)
                {
                    if (chkReq.Checked == true) //kapila 10/3/2016
                        if (txtRCCType.Text == "STK")
                        {
                            MessageBox.Show("Stock item cannot be raised as a request", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    if (txtRCCType.Text == "CUST")
                    {
                        if (txtInvoice.Text == "")
                        {
                            MessageBox.Show("Please enter invoice #!", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (txtInvDate.Text == "")
                        {
                            MessageBox.Show("Please enter invoice date!", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        //kapila 4/3/2015
                        if (_isDocAttachMan == true && dgvFiles.Rows.Count == 0)
                        {
                            MessageBox.Show("Documents are not attached !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        //kapila 9/11/2015
                        MasterItem _itemdetail = new MasterItem();
                        _itemdetail = CHNLSVC.Inventory.GetItem("", txtItem.Text);
                        if (_itemdetail.Mi_is_ser1 == 1)
                        {
                            if (txtSerial.Text != "N/A")
                            {
                                DataTable _seltbl = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtItem.Text, txtSerial.Text);
                                if (_seltbl.Rows.Count > 0)
                                {
                                    SystemInformationMessage("This item is not a customer item, Serial already available in location :  " + _seltbl.Rows[0]["ins_loc"].ToString() + " document date :" + Convert.ToDateTime(_seltbl.Rows[0]["ins_doc_dt"].ToString()).Date + " document # :" + _seltbl.Rows[0]["ins_doc_no"].ToString(), "Serial available");

                                    return;
                                }

                                DataTable _seltblscm = CHNLSVC.Inventory.CheckSerialAvailabilityscm(txtItem.Text, txtSerial.Text);
                                if (_seltblscm.Rows.Count > 0)
                                {
                                    //kapila 2/9/2016
                                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, _seltblscm.Rows[0]["location_code"].ToString());
                                    if (_mstLoc == null)  //kapila 5/1/2017
                                    {
                                        SystemInformationMessage("Serial already available in location :  " + _seltblscm.Rows[0]["location_code"].ToString() + " document date :" + Convert.ToDateTime(_seltblscm.Rows[0]["inv_date"].ToString()).Date + " document :# " + _seltblscm.Rows[0]["doc_ref_no"].ToString(), "Serial available");
                                        return;
                                    }
                                    if (_mstLoc.Ml_anal1 != "SCM2")
                                    {
                                        SystemInformationMessage("This item is not a customer item, Serial already available in location :  " + _seltblscm.Rows[0]["location_code"].ToString() + " document date :" + Convert.ToDateTime(_seltblscm.Rows[0]["inv_date"].ToString()).Date + " document :# " + _seltblscm.Rows[0]["doc_ref_no"].ToString(), "Serial available");

                                        return;
                                    }
                                }
                            }
                        }
                    }
                    if (txtRCC.Enabled == true)
                    {
                        MessageBox.Show("Please press New button!", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if ((chkManual.Checked == true) && (txtManual.Text == ""))
                    {

                        MessageBox.Show("Please Enter Manual Reference Number!", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (cmbAcc.SelectedIndex == -1)
                    {

                        MessageBox.Show("Please select Accessory", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (cmbColMethod.SelectedIndex == -1)
                    {

                        MessageBox.Show("Please select the collection method", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRCCType.Text))
                    {

                        MessageBox.Show("Please select RCC Type", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //if (cmbRccSubType.SelectedIndex == -1)
                    //{

                    //    MessageBox.Show("Please select RCC Sub Type", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    if (txtAgent.Text == "")
                    {
                        MessageBox.Show("Please select Agent", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (txtContPersn.Text == "")
                    {
                        MessageBox.Show("Please enter contact person", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtContPersn.Focus();
                        return;
                    }
                    if (txtContNo.Text == "")
                    {
                        MessageBox.Show("Please enter contact number", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtContNo.Focus();
                        return;
                    }
                    if (txtContLoc.Text == "")
                    {
                        MessageBox.Show("Please enter contact location", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtContLoc.Focus();
                        return;
                    }
                    if (txtTel.Text == "")
                    {
                        MessageBox.Show("Please enter mobile number", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTel.Focus();
                        return;
                    }
                    //if (cmbDefect.SelectedIndex == -1)
                    //{

                    //    MessageBox.Show("Please select Defect Type", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    if (_isExternal == false)
                    {
                        if (_isOnlineSCM2 == true)
                        {
                            if (grvDef.Rows.Count == 0)
                            {
                                MessageBox.Show("Please select Defect Types", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    if (cmbCond.SelectedIndex == -1)
                    {

                        MessageBox.Show("Please select Condition", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (txtEasyLoc.Text == "")
                    {

                        MessageBox.Show("Please enter easy location", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (txtItem.Text == "" || txtSerial.Text == "" || txtItemDesn.Text == "")
                    {

                        MessageBox.Show("Please select product details", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!String.IsNullOrEmpty(txtTel.Text))
                    {
                        Boolean _isValid = IsValidMobileOrLandNo(txtTel.Text.Trim());

                        if (_isValid == false)
                        {
                            MessageBox.Show("Invalid mobile number.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtTel.Text = "";
                            txtTel.Focus();
                            return;
                        }
                    }
                    //kapila 22/12/2015
                    _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                    Service_Req_Hdr _custReqHdr = new Service_Req_Hdr();
                    if (_isExternal == false)
                    {

                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(null, _serLocation);
                        if (_mstLoc != null)
                        {
                            if (_mstLoc.Ml_anal1 == "SCM2")
                                _isOnlineSCM2 = true;
                            else
                                _isOnlineSCM2 = false;
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

                    if (_isExternal == false)
                    {
                        if (_isOnlineSCM2 == true)
                        {
                            if (txtcustCode.Text == "CASH")
                            {
                                MessageBox.Show("Please select registered customer or create new customer", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            //if (Is_Dealer_Inv == true)
                            //{
                            //    MessageBox.Show("Please select registered customer or create new customer", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    return;
                            //}

                            //7/10/2015 check selected valid service agent with the item 
                            Int32 _effItm = CHNLSVC.Inventory.CheckValidServiceAgent(txtItem.Text, _serChannelComp, _serLocation);
                            if (_effItm == 0)
                            {
                                MessageBox.Show("Invalid Service agent. This item cannot be sent to this service agent", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    if (txtSerial.Text != "" && txtSerial.Text != "NA" && txtSerial.Text != "N/A")
                    {
                        Boolean _IsSerialFound = CHNLSVC.Inventory.IsRccSerialFound(txtItem.Text, txtSerial.Text);
                        if (_IsSerialFound == true)
                        {
                            RCC __RCC = null;
                            __RCC = CHNLSVC.Inventory.GetRCCbySerial(txtItem.Text, txtSerial.Text);
                            MessageBox.Show("Another RCC found for this serial number. RCC # is " + __RCC.Inr_no, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                    }
                    Boolean _isAgent = CHNLSVC.Sales.IsCheckServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                    if (_isAgent == false)
                    {
                        MessageBox.Show("Invalid Service Agent !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAgent.Focus();
                    }
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
                                MessageBox.Show("You are not allowed to raised RCC(s) more than " + _allowCount, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    }

                    if (ucPayModes1.Balance != 0 && txtRCCType.Text == "CUST")
                        if (MessageBox.Show("Payment not completed. Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

                }
                if (RccStage == 1)
                {
                    if (txtJob1.Text == "")
                    {

                        MessageBox.Show("Please enter the job number", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //kapila 3/11/2015
                    _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                    Service_Req_Hdr _custReqHdr = new Service_Req_Hdr();
                    if (_isExternal == false)
                    {

                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(null, _serLocation);
                        if (_mstLoc != null)
                        {
                            if (_mstLoc.Ml_anal1 == "SCM2")
                                _isOnlineSCM2 = true;
                            else
                                _isOnlineSCM2 = false;
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

                    if (_isExternal == false)
                    {
                        if (_isOnlineSCM2 == true)
                        {
                            MessageBox.Show("You cannot update the status of the job. This is done by service center", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                if (RccStage == 2)
                {
                    bool _isRepaired = radOk.Checked ? true : false;

                    if (cmbReason.SelectedIndex == -1 && _isRepaired == false)
                    {

                        MessageBox.Show("Please select the reason", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (cmbRetCond.SelectedIndex == -1)
                    {

                        MessageBox.Show("Please select the return condition", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                }
                if (RccStage == 3)
                {
                    if (cmbClosureType.SelectedIndex == -1)
                    {

                        MessageBox.Show("Please select closure type", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }                    
                }
                #endregion

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
                if (txtRCCType.Text != "STK")
                {
                    if (!string.IsNullOrEmpty(txtInvDate.Text))
                    {
                        _RCC.Inr_inv_dt = Convert.ToDateTime(txtInvDate.Text);
                    }
                    else
                    {
                        _RCC.Inr_inv_dt = Convert.ToDateTime(DateTime.Now).Date;
                    }
                }
                else
                {
                    _RCC.Inr_inv_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                }
                //_RCC.Inr_oth_doc_no = 
                //_RCC.Inr_oth_doc_dt = 
                _RCC.Inr_cust_cd = txtcustCode.Text;
                _RCC.Inr_cust_name = txtCustName.Text;
                _RCC.Inr_addr = txtCustAddr.Text;
                _RCC.Inr_tel = txtTel.Text;
                _RCC.Inr_itm = txtItem.Text;
                _RCC.Inr_ser = txtSerial.Text;
                _RCC.Inr_warr = txtWarranty.Text;
                _RCC.Inr_def_cd = (3).ToString();  // Convert.ToString(cmbDefect.SelectedValue);
                //_RCC.Inr_def = 
                _RCC.Inr_condition = Convert.ToString(cmbCond.SelectedValue);
                _RCC.Inr_accessories = Convert.ToString(cmbAcc.SelectedValue);
                _RCC.Inr_easy_loc = txtEasyLoc.Text;
                _RCC.Inr_insp_by = txtInsp.Text;
                _RCC.Inr_rem1 = txtRem.Text;
                //_RCC.Inr_def_rem = 
                _RCC.Inr_is_jb_open = false;
                _RCC.Inr_jb_no = txtJob1.Text;
                _RCC.Inr_open_by = BaseCls.GlbUserID;
                //_RCC.Inr_jb_rem = 
                _RCC.Inr_is_repaired = radOk.Checked ? true : false;
                if (RccStage == 2)
                {
                    _RCC.Inr_repair_stus = Convert.ToString(cmbReason.SelectedValue);
                }
                _RCC.Inr_rem2 = txtRepairRem.Text;
                _RCC.Inr_is_returned = false;
                if (_RCC.Inr_stage == 2)
                {
                    _RCC.Inr_return_dt = Convert.ToDateTime(txtRetDate.Value);
                }
                if (RccStage == 2)
                {
                    _RCC.Inr_ret_condition = Convert.ToString(cmbRetCond.SelectedValue);
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
                _RCC.Inr_rem3 = txtCompleteRem.Text;
                _RCC.Inr_closure_tp = Convert.ToString(cmbClosureType.SelectedValue);
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
                _RCC.Inr_anal1 = txtOrdNo.Text;
                _RCC.Inr_anal2 = txtDispatchNo.Text;
                _RCC.Inr_anal7 = txtJob2.Text;
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

                #region AOD OUT validation
                if (txtRCCType.Text == "STK")        //receive location code validation
                {
                    if (_RCC.INR_IS_EXTERNAL == true)       //go to virtual location
                    {
                        DataTable dt = CHNLSVC.Inventory.GetVirtualLocation(BaseCls.GlbUserComCode, "RCC", out _othLoc);
                        OthLocation = _othLoc;
                        if (string.IsNullOrEmpty(_othLoc))
                        {
                            MessageBox.Show("Cannot Process. Virtual location not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    //go to service location
                    {
                        if (RccStage == 0)
                        {
                            DataTable dt = CHNLSVC.Inventory.GetServiceLocation(BaseCls.GlbUserComCode, txtAgent.Text, out _othLoc);
                            OthLocation = _othLoc;
                            if (string.IsNullOrEmpty(_othLoc))
                            {
                                MessageBox.Show("Cannot Process. Internal service location not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }

                if (txtRCCType.Text == "FIXED")        //get fixed asset location
                {

                    DataTable dt = CHNLSVC.Inventory.GetFixAssetLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, out _outLoc);
                    OutLocation = _outLoc;
                    if (string.IsNullOrEmpty(_outLoc))
                    {
                        MessageBox.Show("Cannot Process. Fixed Asset location not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                #endregion

                if (RccStage == 5 && lblStatus.Text == "Approved Request")
                {
                    int row_aff = CHNLSVC.Inventory.UpdateRCCReqRaise(txtRCC.Text);

                    BaseCls.GlbReportTp = "RCC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                    _view.GlbReportDoc = txtRCC.Text; ;
                    BaseCls.GlbReportDoc = txtRCC.Text;
                    _view.Show();
                    _view = null;
                }

                #region RCC raised
                if (RccStage == 0)
                {
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RCC";
                    masterAuto.Aut_start_char = "RCC";
                    masterAuto.Aut_year = null;

                    _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);

                    Service_Req_Hdr _custReqHdr = new Service_Req_Hdr();
                    if (_isExternal == false)
                    {
                        #region check isonline
                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(null, _serLocation);
                        if (_mstLoc != null)
                        {
                            if (_mstLoc.Ml_anal1 == "SCM2")
                                _isOnlineSCM2 = true;
                            else
                                _isOnlineSCM2 = false;
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
                        #endregion
                        if (_isOnlineSCM2 == true)
                        {
                            _reqHdrList = new List<Service_Req_Hdr>();
                            _reqDetList = new List<Service_Req_Det>();

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
                            _custReqHdr.Srb_custexptdt = Convert.ToDateTime(txtDate.Value);
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
                                _custReqHdr.Srb_infm_add1 = txtEasyLoc.Text;
                                _custReqHdr.Srb_phno = _mstPc.Mpc_tel;
                                _custReqHdr.Srb_faxno = _mstPc.Mpc_fax;
                                _custReqHdr.Srb_email = _mstPc.Mpc_email;
                                _custReqHdr.Srb_cnt_person = txtContPersn.Text;
                                _custReqHdr.Srb_cnt_add1 = txtContLoc.Text;
                                _custReqHdr.Srb_cnt_add2 = "";
                                _custReqHdr.Srb_cnt_phno = txtContNo.Text;
                                _custReqHdr.Srb_job_rmk = txtRem.Text;
                                //_custReqHdr.Srb_tech_rmk = 
                            }

                            //------original cust details
                            if (txtRCCType.Text == "STK")
                            {
                                DataTable _dtIncGrp = CHNLSVC.Inventory.GetCustIncomeGroup(BaseCls.GlbUserComCode, txtAgent.Text);
                                if (_dtIncGrp.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(_dtIncGrp.Rows[0]["mbe_income_grup"].ToString()))
                                    {
                                        _custReqHdr.Srb_b_cust_cd = _dtIncGrp.Rows[0]["mbe_income_grup"].ToString();
                                        _custReqHdr.Srb_b_cust_name = _dtIncGrp.Rows[0]["mbe_name"].ToString();
                                        _custReqHdr.Srb_b_mobino = _dtIncGrp.Rows[0]["mbe_mob"].ToString();
                                        _custReqHdr.Srb_b_add1 = _dtIncGrp.Rows[0]["mbe_add1"].ToString();
                                        _custReqHdr.Srb_b_add2 = _dtIncGrp.Rows[0]["mbe_add2"].ToString();
                                        _custReqHdr.Srb_b_phno = _dtIncGrp.Rows[0]["mbe_tel"].ToString();
                                        _custReqHdr.Srb_b_email = _dtIncGrp.Rows[0]["mbe_email"].ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cannot Update.\n Billing customer code not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }

                                else
                                {
                                    MessageBox.Show("Cannot Update.\n Billing customer code not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                _custReqHdr.Srb_b_cust_cd = txtcustCode.Text;
                                _custReqHdr.Srb_b_cust_name = txtCustName.Text;
                                _custReqHdr.Srb_b_mobino = txtTel.Text;
                                _custReqHdr.Srb_b_add1 = txtCustAddr.Text;
                                _custReqHdr.Srb_b_phno = txtPhone.Text;
                                _custReqHdr.Srb_b_email = txtEmail.Text;
                            }

                            if (string.IsNullOrEmpty(_custReqHdr.Srb_b_cust_cd))        //30/3/2016
                            {
                                MessageBox.Show("Invalid customer code !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

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

                            Service_Req_Det _custReqDet = new Service_Req_Det();

                            _custReqDet.Jrd_reqline = 1;
                            _custReqDet.Jrd_loc = _serLocation;
                            DataTable LocDes = CHNLSVC.Sales.getLocDesc(_serChannelComp, "LOC", _serLocation);
                            if (LocDes.Rows.Count > 0)
                                _custReqDet.Jrd_pc = LocDes.Rows[0]["ml_def_pc"].ToString();

                            _custReqDet.Jrd_itm_cd = txtItem.Text;
                            if (ItemStatus == "GOOD")
                                _custReqDet.Jrd_itm_stus = "GOD";
                            else
                                _custReqDet.Jrd_itm_stus = ItemStatus;

                            _custReqDet.Jrd_itm_desc = txtItemDesn.Text;
                            _custReqDet.Jrd_brand = Brand;
                            _custReqDet.Jrd_model = txtModel.Text;
                            _custReqDet.Jrd_itm_cost = 0;
                            _custReqDet.Jrd_ser1 = txtSerial.Text;
                            if (string.IsNullOrEmpty(Serial2))
                                _custReqDet.Jrd_ser2 = "N/A";
                            else
                                _custReqDet.Jrd_ser2 = Serial2;
                            _custReqDet.Jrd_warr = txtWarranty.Text;
                            //_custReqDet.Jrd_regno = 
                            //_custReqDet.Jrd_milage = 

                            if (txtRCCType.Text == "STK")
                            {
                                _custReqDet.Jrd_warr_stus = 1;
                                _custReqDet.Jrd_onloan = 1;
                            }
                            else
                            {
                                _custReqDet.Jrd_warr_stus = Convert.ToInt32(txtWarPeriod.Text) > 0 ? 1 : 0;
                                _custReqDet.Jrd_onloan = 0;
                            }

                            _custReqDet.Jrd_chg_warr_stdt = InvDate;
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
                            if (txtRCCType.Text == "STK")
                                if (cmbJobType.SelectedValue.ToString() != "FLD")
                                    _custReqDet.Jrd_isstockupdate = 1;
                                else
                                    _custReqDet.Jrd_isstockupdate = 0;
                            else
                                _custReqDet.Jrd_isstockupdate = 0;
                            //_custReqDet.Jrd_needgatepass = 
                            _custReqDet.Jrd_iswrn = 0;
                            _custReqDet.Jrd_warrperiod = Convert.ToInt32(txtWarPeriod.Text);
                            _custReqDet.Jrd_warrrmk = WarRem;
                            _custReqDet.Jrd_warrstartdt = InvDate;
                            _custReqDet.Jrd_warrreplace = 0;
                            //kapila 11/3/2016 get inn date
                            InvoiceHeader _invHdr = CHNLSVC.Sales.GetInvoiceHeader(txtInvoice.Text);
                            if (_invHdr != null)
                                _custReqDet.Jrd_date_pur = Convert.ToDateTime(_invHdr.Sah_dt);

                            _custReqDet.Jrd_invc_no = txtInvoice.Text;
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
                            //_custReqDet.Jrd_conf_desc = 
                            //_custReqDet.Jrd_conf_rmk = 
                            //_custReqDet.Jrd_tranf_by = 
                            //_custReqDet.Jrd_tranf_dt = 
                            _custReqDet.Jrd_do_invoice = 0;
                            //_custReqDet.Jrd_insu_com = 
                            //_custReqDet.Jrd_agreeno = 
                            _custReqDet.Jrd_issrn = 0;
                            //_custReqDet.Jrd_isagreement = 
                            //_custReqDet.Jrd_cust_agreeno = 
                            //_custReqDet.Jrd_quo_no = 
                            _custReqDet.Jrd_stage = 1;
                            _custReqDet.Jrd_com = _serChannelComp;  //4/9/2015
                            _custReqDet.Jrd_ser_id = SerialID.ToString();
                            _custReqDet.Jrd_used = 0;
                            //_custReqDet.Jrd_jobno = 
                            //_custReqDet.Jrd_jobline = 

                            //20/8/2015
                            MasterItem _mitm = CHNLSVC.Inventory.GetItem("", txtItem.Text);
                            if (_mitm.Mi_is_ser1 == 1)
                            {
                                #region Get Supplier
                                DataTable _dtSupp = new DataTable();
                                _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, txtItem.Text, txtSerial.Text, 1);
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
                                    _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, txtItem.Text, txtSerial.Text, 2);
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
                                        _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, txtItem.Text, txtSerial.Text, 3);
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
                                            //8/12/2015
                                            _dtSupp = new DataTable();
                                            _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, txtItem.Text, txtSerial.Text, 4);
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
                                                //8/12/2015
                                                _dtSupp = new DataTable();
                                                _dtSupp = CHNLSVC.General.GetSerialSupplierCode(BaseCls.GlbUserComCode, txtItem.Text, txtSerial.Text, 5);
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
                                    }
                                }

                                #endregion

                            }


                            _reqDetList.Add(_custReqDet);
                        }
                    }

                    int row_aff = CHNLSVC.Inventory.SaveRCC(_RCC, _reqHdrList, _reqDetList, _scvDefList, oMainList, Is_Dealer_Inv, _isOnlineSCM2, masterAuto, out _RccNo);
                    if (row_aff != -99 && row_aff >= 0)
                    {
                        //save receipt----------------------------

                        if (ucPayModes1.RecieptItemList.Count > 0)
                            SaveReceiptHeader(_RccNo, out QTNum);

                        //end receipt

                        if (_RCC.Inr_stage != 5)
                        {
                            #region STOCK ITEM AOD OUT
                            if (txtRCCType.Text == "STK")        //AOD out
                            {
                                if (_RCC.Inr_com_cd == "SGL")
                                    if (CHNLSVC.Inventory.IsAgentParaExist(BaseCls.GlbUserComCode, _RCC.Inr_agent) == false)    //21/4/2015
                                        goto NoAOD;

                                if (cmbJobType.SelectedValue.ToString() == "NOR")
                                {
                                    InventoryHeader _inventoryHeader = new InventoryHeader();
                                    #region Inventory Header Value Assign
                                    _inventoryHeader.Ith_acc_no = string.Empty;
                                    _inventoryHeader.Ith_anal_1 = string.Empty;
                                    _inventoryHeader.Ith_anal_10 = false;//Direct AOD
                                    _inventoryHeader.Ith_anal_11 = false;
                                    _inventoryHeader.Ith_anal_12 = false;
                                    _inventoryHeader.Ith_anal_2 = string.Empty;
                                    _inventoryHeader.Ith_anal_3 = string.Empty;
                                    _inventoryHeader.Ith_anal_4 = string.Empty;
                                    _inventoryHeader.Ith_anal_5 = string.Empty;
                                    _inventoryHeader.Ith_anal_6 = 0;
                                    _inventoryHeader.Ith_anal_7 = 0;
                                    _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(txtDate.Value).Date;
                                    _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(txtDate.Value).Date;
                                    _inventoryHeader.Ith_bus_entity = string.Empty;
                                    _inventoryHeader.Ith_cate_tp = "NOR";
                                    _inventoryHeader.Ith_channel = string.Empty;
                                    _inventoryHeader.Ith_com = BaseCls.GlbUserComCode;
                                    _inventoryHeader.Ith_com_docno = string.Empty;
                                    _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                                    _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
                                    _inventoryHeader.Ith_del_add1 = string.Empty;
                                    _inventoryHeader.Ith_del_add2 = string.Empty;
                                    _inventoryHeader.Ith_del_code = string.Empty;
                                    _inventoryHeader.Ith_del_party = string.Empty;
                                    _inventoryHeader.Ith_del_town = string.Empty;
                                    _inventoryHeader.Ith_direct = false;
                                    _inventoryHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Value).Date;
                                    _inventoryHeader.Ith_doc_no = string.Empty;
                                    _inventoryHeader.Ith_doc_tp = string.Empty;
                                    _inventoryHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Value).Date.Year;
                                    _inventoryHeader.Ith_entry_no = string.Empty;
                                    _inventoryHeader.Ith_entry_tp = string.Empty;
                                    _inventoryHeader.Ith_git_close = false;
                                    _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Value).Date;
                                    _inventoryHeader.Ith_git_close_doc = string.Empty;
                                    _inventoryHeader.Ith_is_manual = false;
                                    _inventoryHeader.Ith_isprinted = false;
                                    _inventoryHeader.Ith_sub_docno = _RccNo;
                                    _inventoryHeader.Ith_loading_point = string.Empty;
                                    _inventoryHeader.Ith_loading_user = string.Empty;
                                    _inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                                    _inventoryHeader.Ith_manual_ref = "0";
                                    _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                                    _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
                                    _inventoryHeader.Ith_noofcopies = 0;
                                    _inventoryHeader.Ith_oth_loc = OthLocation;
                                    _inventoryHeader.Ith_oth_docno = txtRCC.Text;
                                    _inventoryHeader.Ith_remarks = string.Empty;
                                    _inventoryHeader.Ith_sbu = string.Empty;
                                    //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
                                    _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                                    _inventoryHeader.Ith_stus = "A";
                                    _inventoryHeader.Ith_sub_tp = "SERVICE";    // string.Empty; 10/7/2013
                                    _inventoryHeader.Ith_cate_tp = "NOR";
                                    _inventoryHeader.Ith_vehi_no = string.Empty;
                                    _inventoryHeader.Ith_oth_com = BaseCls.GlbUserComCode;
                                    if (!string.IsNullOrEmpty(OthLocation))
                                    {
                                          MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(null, OthLocation);
                                          _inventoryHeader.Ith_oth_com = _mstLoc.Ml_com_cd;
                                    }
                                  
                                   
                                    _inventoryHeader.Ith_anal_1 = "0";
                                    _inventoryHeader.Ith_anal_2 = string.Empty;
                                    #endregion

                                    string _message = string.Empty;
                                    string _genSalesDoc = string.Empty;

                                    MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                                    _inventoryAuto.Aut_moduleid = "AOD";
                                    _inventoryAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                                    _inventoryAuto.Aut_cate_tp = "LOC";
                                    _inventoryAuto.Aut_direction = 0;
                                    _inventoryAuto.Aut_modify_dt = null;
                                    _inventoryAuto.Aut_year = DateTime.Now.Year;

                                    //Serials
                                    List<ReptPickSerials> _serialList = new List<ReptPickSerials>();
                                    //string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                                    ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, SerialID);
                                    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                    _reptPickSerial_.Tus_usrseq_no = 1;
                                    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                    _reptPickSerial_.Tus_base_doc_no = "N/A";
                                    _reptPickSerial_.Tus_base_itm_line = 0;
                                    _reptPickSerial_.Tus_new_remarks = "AOD-OUT";       //kapila

                                    MasterItem msitem = new MasterItem();
                                    msitem = CHNLSVC.Inventory.GetItem("", txtItem.Text.ToString());
                                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                    _serialList.Add(_reptPickSerial_);


                                    _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, null, _inventoryHeader, _inventoryAuto, null, null, _serialList, null, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);

                                    _effect = CHNLSVC.Inventory.UpdateJobReqByRcc(_RccNo, _genInventoryDoc);
                                }
                            NoAOD:
                                _effect = 1;
                            }
                            #endregion
                        }

                        //kapila 1/7/2016 send mail to service center
                        if (_isOnlineSCM2==true)
                        {
                            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                            _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(_RCC.Inr_agent, null, null, null, null, null);
                            if (IsValidEmail(_masterBusinessCompany.Mbe_email))
                            {
                                string _mail = "";
                                _mail += "RCC Request Details" + Environment.NewLine + Environment.NewLine;
                                _mail += "RCC No - " + _RccNo  + "" + Environment.NewLine;
                                _mail += "Request Date - " + _RCC.Inr_dt.ToShortDateString() + "" + Environment.NewLine;
                                _mail += "Item Code - " + _RCC.Inr_itm + "" + Environment.NewLine;
                                _mail += "Serial # - " + _RCC.Inr_ser + "" + Environment.NewLine;
                                _mail += "Warranty # - " + _RCC.Inr_warr + "" + Environment.NewLine;
                                _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;
                                CHNLSVC.CommonSearch.Send_SMTPMail(_masterBusinessCompany.Mbe_email, "RCC Request", _mail);
                            }
                        }
                        if (chkManual.Checked == true)
                        {
                            CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "MDOC_RCC", Convert.ToInt32(txtManual.Text), _RccNo);
                        }
                        chkRaise.Checked = false;
                        chkRaise.Enabled = false;
                        MessageBox.Show("Successfully Updated. RCC Number - " + _RccNo, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //print
                        if (_RCC.Inr_stage != 5)
                        {
                            BaseCls.GlbReportTp = "RCC";
                            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                            _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                            BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                            _view.GlbReportDoc = _RccNo;
                            BaseCls.GlbReportDoc = _RccNo;
                            _view.Show();
                            _view = null;

                            if (_RCC.Inr_tp == "CUST" && ucPayModes1.Balance == 0 && ucPayModes1.TotalAmount > 0)
                            {
                                Reports.Sales.ReportViewer _view1 = new Reports.Sales.ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view1.GlbReportName = string.Empty;
                                GlbReportName = string.Empty;
                                BaseCls.GlbReportTp = "REC";
                                _view1.GlbReportName = "ReceiptPrints.rpt";
                                _view1.GlbReportDoc = QTNum;
                                _view1.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view1.Show();
                                _view1 = null;
                            }
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_RccNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
                #region job open
                if (RccStage == 1)
                {
                    if (txtRCCType.Text == "STK" && _RCC.INR_IS_EXTERNAL == false && !txtRCC.Text.Contains("RCCN"))
                    {
                        MessageBox.Show("Job is not opened by service center. Please contact service", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int row_aff = CHNLSVC.Inventory.Update_RCC_JobOpen(_RCC);
                    chkOpen.Checked = false;
                    chkOpen.Enabled = false;
                    _RccNo = txtRCC.Text;
                    MessageBox.Show("Successfully Updated", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion
                #region repair return
                if (RccStage == 2)
                {
                    if (txtRCCType.Text == "STK" && _RCC.INR_IS_EXTERNAL == false && !txtRCC.Text.Contains("RCCN"))
                    {
                        if (_RCC.Inr_sub_tp == "NOR")
                        {
                            MessageBox.Show("Job is not completed by the service center. Please contact service", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string _error = string.Empty;
                    int row_aff = CHNLSVC.Inventory.Update_RCC_Repair(_RCC, out _error);
                    chkRepair.Checked = false;
                    chkRepair.Enabled = false;
                    _RccNo = txtRCC.Text;

                    if (row_aff != -1)
                    {
                        MessageBox.Show("Successfully Updated", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //print
                        BaseCls.GlbReportTp = "RCC";
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                        BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                        _view.GlbReportDoc = _RccNo;
                        BaseCls.GlbReportDoc = _RccNo;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        MessageBox.Show(_error);
                        return;
                    }

                }
                #endregion
                #region complete
                if (RccStage == 3)
                {
                    string _errorMsg = "";
                    if (txtRCCType.Text == "STK" && !txtRCC.Text.Contains("RCCN"))
                    {
                        RCC _RCC1 = null;
                        _RCC1 = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text);

                        if (_RCC1.INR_IS_EXTERNAL == true && txtRCCType.Text == "STK" && _RCC.Inr_sub_tp == "NOR")
                        {
                            MessageBox.Show("Cannot Update. please press the confirm button.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (_RCC1.Inr_in_stus == false || _RCC1.Inr_out_stus == false)
                        {
                            if (_RCC1.Inr_sub_tp == "NOR")
                            {
                                if (_RCC1.INR_IS_EXTERNAL == false)
                                {
                                    string _tmp = "Job Pending";
                                    DataTable _dt = CHNLSVC.CustService.getJobStageByJobNo(txtJob1.Text, BaseCls.GlbUserComCode, Convert.ToInt32(_isOnlineSCM2));
                                    if (_dt.Rows.Count > 0)
                                        _tmp = _dt.Rows[0]["jbs_desc"].ToString();

                                    MessageBox.Show("Gate pass is not generated. Please contact service center\nJob Stage - " + _tmp, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Cannot Update. Automatically generated AOD not found", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        //}

                    }

                    //add by akila 2017/12/29
                    if (cmbClosureType.Items.Count > 0 && cmbClosureType.Text == "Customer Pick Up Pending")
                    {
                        _RCC.NeedToSendSmsReminder = true;
                        _RCC.Inr_is_complete = true;
                        _RCC.Inr_stage = 4;
                    }

                    int row_aff = CHNLSVC.Inventory.Update_RCC_complete(_RCC, null, null, null, out _errorMsg);
                    chkComplete.Checked = false;
                    chkComplete.Enabled = false;
                    _RccNo = txtRCC.Text;
                    MessageBox.Show("Successfully Updated", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                clearAll();
                chkReq.Checked = false;
                btnSearch.Enabled = true;

                if (_RCC.Inr_stage != 5)
                {
                    if (RccStage == 0 || RccStage == 2)
                    {
                        //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        //_view.GlbReportName = "RCCPrint_New.rpt";
                        //BaseCls.GlbReportName = "RCCPrint_New.rpt";
                        //_view.GlbReportDoc = _RccNo;
                        //BaseCls.GlbReportDoc = _RccNo;
                        //_view.Show();
                        //_view = null;
                    }
                    //9/8/2013
                    if (RccStage == 0 && _RCC.Inr_tp == "STK")
                    {
                        if (_RCC.Inr_com_cd == "SGL" && _RCC.Inr_agent != "SGSSC")
                            goto NoAOD1;

                        Reports.Inventory.ReportViewerInventory _views = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportTp = "OUTWARD";
                        _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                        _views.GlbReportDoc = _genInventoryDoc;
                        BaseCls.GlbReportDoc = _genInventoryDoc;
                        _views.Show();
                        _views = null;
                    }
                }
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
                _CommonSearch.IsSearchEnter = true; //kapila 13/5/2016
                if (!chkByAll.Checked)
                {
                    if (chkByCom.Checked)
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RccByCompleteStage);
                        _result = CHNLSVC.CommonSearch.GetRCCByStage(_CommonSearch.SearchParams, null, null);
                    }
                    if (chkByRequest.Checked)
                    {
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "RCCAPP1"))
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RCCReq);
                            _result = CHNLSVC.CommonSearch.GetRCC_REQ(_CommonSearch.SearchParams, null, null);
                        }
                        else
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RccByRequestStage);
                            _result = CHNLSVC.CommonSearch.GetRCCByStage(_CommonSearch.SearchParams, null, null);
                        }
                    }
                    if (chkByAll.Checked)
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RCC);
                        _result = CHNLSVC.CommonSearch.GetRCC(_CommonSearch.SearchParams, null, null);
                    }
                }
                //else if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "RCCAPP1"))
                //{
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RCCReq);
                //    _result = CHNLSVC.CommonSearch.GetRCC_REQ(_CommonSearch.SearchParams, null, null);
                //}
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RCC);
                    _result = CHNLSVC.CommonSearch.GetRCC(_CommonSearch.SearchParams, null, null);
                }

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

                load_charge_details();
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

        private void load_charge_details()
        {
            string _AgentSubChannel = "NA";
            Decimal _Tot = 0;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtAgent.Text);
            if (_mstLoc != null)
                _AgentSubChannel = _mstLoc.Ml_cate_3;

            _lstCharge = new List<Service_Charge>();
            _lstCharge = CHNLSVC.Inventory.Get_RCC_Charge(BaseCls.GlbUserComCode, txtAgent.Text, _AgentSubChannel, Convert.ToDateTime(txtDate.Value).Date);
            if (_lstCharge != null)
            {
                if (_lstCharge.Count != 0)
                {
                    foreach (Service_Charge line in _lstCharge)
                    {
                        _Tot = _Tot + Convert.ToInt16(line.Scg_rate);
                    }
                }
            }
            lblTot.Text = _Tot.ToString("0.00");
            lblBal.Text = _Tot.ToString("0.00");

            //clear payment list
            ucPayModes1.ClearControls();
            ucPayModes1.TotalAmount = Convert.ToDecimal(lblTot.Text);
            ucPayModes1.Amount.Text = Convert.ToString(lblBal.Text);
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.Date = Convert.ToDateTime(txtDate.Value.Date);
            ucPayModes1.LoadData();

            grvCharge.DataSource = null;
            grvCharge.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _lstCharge;
            grvCharge.DataSource = _source;
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

            txtEasyLoc.Text = string.Empty;
            txtRCC.Text = "";
            txtAgent.Text = "";
            grvCharge.DataSource = null;
            txtInsp.Text = "";
            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtItmStus.Text = "";
            txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";
            cmbAcc.SelectedIndex = -1;
            cmbCond.SelectedIndex = -1;
            cmbDefect.SelectedIndex = -1;

            pnlCharge.Visible = false;
            if (txtRCCType.Text == "CUST")
                pnlCharge.Visible = true;
            else if (txtRCCType.Text == "STK")
            {
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster(ItemStatus, "ALL");
                    if (_status.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(_status.Rows[0]["MIS_IS_PAY_MAN"]) == 1)
                            pnlCharge.Visible = true;
                    }
                }
            }


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

        private void ClearRccItem()
        {
            txtAccNo.Text = string.Empty;
            txtCustAddr.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtInvoice.Text = string.Empty;
            txtInvDate.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtPhone.Text = "";
            txtContLoc.Text = "";
            txtContNo.Text = "";
            txtEmail.Text = "";
            txtContPersn.Text = "";
            txtcustCode.Text = "";
            txtEmail.Text = "";


            txtRCC.Text = "";
            txtAgent.Text = "";
            grvCharge.DataSource = null;
            lblBal.Text = "0.00";
            lblTot.Text = "0.00";
            cmbColMethod.SelectedIndex = -1;
            cmbRccSubType.SelectedIndex = -1;
            txtRCCType.Text = "";
            lblRCCType.Text = "";
            txtManual.Text = "";
            chkManual.Checked = false;
            lblStatus.Text = "";

            txtEasyLoc.Text = string.Empty;
            txtInsp.Text = "";
            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtItmStus.Text = "";
            txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";
            cmbAcc.SelectedIndex = -1;
            cmbCond.SelectedIndex = -1;
            cmbDefect.SelectedIndex = -1;
            txtWarRem.Text = "";
            txtWarPeriod.Text = "0";

        }

        private void ClearRccJob()
        {
            txtJob1.Text = "";
            txtJob2.Text = "";
            txtOrdNo.Text = "";
            txtDispatchNo.Text = "";
            txtHologram.Text = "";
        }

        private void ClearRccRepair()
        {
            cmbReason.SelectedIndex = -1;
            cmbRetCond.SelectedIndex = -1;
            txtRepairRem.Text = "";
            txtRetDate.Text = "";
            //chkRepaired.Checked = false;
            radOk.Checked = true;
            radNotOk.Checked = false;
        }

        private void ClearRccComplete()
        {
            cmbClosureType.SelectedIndex = 0;
            txtCompleteRem.Text = "";
            txtCloseDate.Text = "";
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
                _Item = CHNLSVC.Inventory.GetItem("", txtItem.Text);
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
                bool _isValid = false;
                LoadRCCDetails(out _isValid);
                if (_isValid)
                {
                    GetServiceChannel();
                    Set_check_Controls(RccStage);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SeqNo = 0;
                ItmLine = 0;
                BatchLine = 0;
                SerLine = 0;
                SerialNo = string.Empty;
                WarrantyNo = string.Empty; 
                ItemCode = string.Empty;
                ItemStatus = string.Empty;
                InvNo = string.Empty;
                AccNo = string.Empty;
                WarRem = string.Empty;
                Tel = string.Empty;
                CustCode = string.Empty;
                CustAddr = string.Empty;
                WarPeriod = 0;

                string _outLoc = "";
                if (txtRCCType.Text == "")
                {
                    MessageBox.Show("Please select the RCC type", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txtRCCType.Text == "FIXED")        //get fixed asset location
                    {

                        DataTable dt = CHNLSVC.Inventory.GetFixAssetLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, out _outLoc);
                        OutLocation = _outLoc;
                        if (string.IsNullOrEmpty(_outLoc))
                        {
                            MessageBox.Show("Cannot Search. Fixed Asset location not found !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    CommonSearch.RCCSerialSearch _SerSearch = new CommonSearch.RCCSerialSearch();
                    _SerSearch.setInitValues(lblRCCType.Text, chkOthLoc.Checked ? true : false, _outLoc);

                    _SerSearch.ShowDialog();

                    txtWarPeriod.Text = "0";
                    lblFOC.Visible = false;

                    if (txtRCCType.Text == "CUST")
                    {
                        txtInvoice.Text = InvNo;
                        txtAccNo.Text = AccNo;
                        txtWarPeriod.Text = WarPeriod.ToString();
                        txtWarRem.Text = WarRem;

                        txtcustCode.Text = CustCode;
                        txtCustName.Text = string.IsNullOrEmpty(CustName) ? "CASH" : CustName;
                        txtCustAddr.Text = CustAddr;
                        txtInvDate.Text = (InvDate).ToString("dd/MMM/yyyy");
                        txtTel.Text = Tel;

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
                    }
                    txtItem.Text = ItemCode;
                    //24/7/2015
                    if (txtRCCType.Text == "STK")
                    {
                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(ItemCode, ItemStatus);
                        txtWarPeriod.Text = _period.Mwp_val.ToString();
                    }

                    DateTime _warExpDate;
                    if (InvDate.Year >= DateTime.MaxValue.Year)
                    {
                        _warExpDate = DateTime.MinValue;
                    }
                    else
                    {
                        _warExpDate = InvDate.AddMonths(WarPeriod);
                    }

                    //DateTime _warExpDate = InvDate.AddMonths(WarPeriod);

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
                        DataTable odt = new DataTable();
                        foreach (DataRow row in _DTNoRcc.Rows)// add by tharanga 2017/10/07
                        {
                            DataTable int_rcc = CHNLSVC.Inventory.GetRCCbyNoTableNEW(row["INR_NO"].ToString());
                            odt.Merge(int_rcc);
                        }
                       
                       

                        if (_DTNoRcc != null) _pendingCount = _DTNoRcc.Rows.Count;
                        _pendingCount = _pendingCount - odt.Rows.Count; // add by tharanga 2017/10/07
                        if (_allowCount <= _pendingCount)
                        {


                            btnPrint.DataSource = null;
                            btnPrint.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = odt;
                            btnPrint.DataSource = _source;
                            pnlRccpendilglist.Visible = true;

                            MessageBox.Show("You are not allowed to raised RCC(s) more than " + _allowCount, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                }
                ClearRccComplete();
                ClearRccItem();
                ClearRccJob();
                ClearRccRepair();

                txtRCC.Enabled = false;
                btnSearch_Rcc.Enabled = false;

                chkRaise.Enabled = true;
                chkRaise.Checked = true;

                chkComplete.Checked = false;
                chkComplete.Enabled = false;

                chkRepair.Checked = false;
                chkRepair.Enabled = false;

                chkOpen.Checked = false;
                chkOpen.Enabled = false;

                pnlJob.Enabled = false;
                pnl_Cond.Enabled = true;

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
                        MessageBox.Show("Invalid Manual Document Number !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtManual.Text = "";
                        txtManual.Focus();
                    }
                }
                else
                {
                    if (chkManual.Checked == true)
                    {
                        MessageBox.Show("Invalid Manual Document Number !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Invalid Service Agent !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAgent.Focus();
                    }
                    else
                    {
                        if (BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                        {
                            Boolean _isAllowAgent = CHNLSVC.Inventory.IsValidServiceAgent(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtAgent.Text);

                            if (_isAllowAgent == false)
                            {
                                MessageBox.Show("You cannot raise RCC for this agent. Contact inventory department !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtAgent.Focus();
                                return;
                            }
                        }
                        load_charge_details();
                    }

                    GetServiceChannel();

                }
                else
                {
                    grvCharge.DataSource = null;
                    lblTot.Text = "0.00";
                    lblBal.Text = "0.00";
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
            if (_dtserLoc != null && _dtserLoc.Rows.Count > 0)
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

            if (_isOnlineSCM2 == true && _isExternal == false)
                btnDef.Visible = true;
            else
                btnDef.Visible = false;


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
            if (e.KeyCode == Keys.Enter)
                txtEasyLoc.Focus();
        }

        private void txtEasyLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRem.Focus();
        }

        private void chkRepaired_CheckedChanged(object sender, EventArgs e)
        {
            bool _isRepaired = radOk.Checked ? true : false;
            if (_isRepaired == true)
                cmbReason.Enabled = false;
            else
                cmbReason.Enabled = true;
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
                pnlCust.Enabled = true;
                txtCustName.Enabled = true;
                txtCustAddr.Enabled = true;
                txtEmail.Enabled = true;
                txtInvDate.Enabled = true;
                txtInvoice.Enabled = true;
                txtAccNo.Enabled = true;
                btnItemSearch.Enabled = true;
                btnSearch.Enabled = false;
            }
            else
            {
                pnlItem.Enabled = false;
                pnlCust.Enabled = false;
                txtCustName.Enabled = false;
                txtCustAddr.Enabled = false;
                txtEmail.Enabled = false;
                txtInvDate.Enabled = false;
                txtInvoice.Enabled = false;
                txtAccNo.Enabled = false;
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
                    MasterItem msitem = CHNLSVC.Inventory.GetItem("", txtItem.Text);
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
                    FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem("", txtItem.Text.ToUpper());
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
                    MessageBox.Show("Successfully Completed.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Successfully Completed.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        return;
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

                        _RCC.Inr_no = txtRCC.Text.Trim();
                        _RCC.Inr_complete_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_complete_by = BaseCls.GlbUserID;
              
                        _RCC.Inr_cre_by = BaseCls.GlbUserID;
                        _RCC.Inr_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_mod_by = BaseCls.GlbUserID;
                        _RCC.Inr_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                        _RCC.Inr_session_id = BaseCls.GlbUserSessionID;
                        if (chkRaise.Checked==true && chkOpen.Checked==true && chkRepair.Checked==true)
	                    {
                            _RCC.Inr_stage = 4;
                            //add by akila 2017/12/29
                            if (cmbClosureType.Items.Count > 0 && cmbClosureType.Text == "Customer Pick Up Pending")
                            {
                                _RCC.NeedToSendSmsReminder = true;
                            }
                        }
                        

                        int row_aff = CHNLSVC.Inventory.Update_RCC_complete(_RCC, invHdr, PickSerials, PickSerialsSub, out _errorMsg);
                        if (row_aff != 1)
                        {
                            MessageBox.Show(_errorMsg, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            chkComplete.Checked = false;
                            chkComplete.Enabled = false;
                            this.Cursor = Cursors.Default;
                            clearAll();
                            MessageBox.Show("Successfully Updated", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
                        chkComplete.Checked = false;
                        chkComplete.Enabled = false;
                        this.Cursor = Cursors.Default;
                        clearAll();
                        MessageBox.Show(documntNo, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (e.KeyCode == Keys.Enter)
                    cmbDefect.Focus();
            }
        }

        private void cmbDefect_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    cmbAcc.Focus();
            }
        }

        private void cmbAcc_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtInsp.Focus();
            }
        }

        private void txtInsp_KeyDown_1(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtEasyLoc.Focus();
            }
        }

        private void txtEasyLoc_KeyDown_1(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtRem.Focus();
            }
        }

        private void cmbReason_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    cmbRetCond.Focus();
            }
        }

        private void cmbRetCond_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtRepairRem.Focus();
            }
        }

        private void cmbClosureType_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtCompleteRem.Focus();
            }
        }

        private void txtJob1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtJob2.Focus();
            }
        }

        private void txtJob2_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtOrdNo.Focus();
            }
        }

        private void txtOrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtDispatchNo.Focus();
            }
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
            {
                btnSearch_Rcc_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSearch.Focus();
                btnSearch.Select();
            }
                
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
            //msitem = CHNLSVC.Inventory.GetItem("", textBox2.Text.ToString());
            //_reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
            //_reptPickSerial_.Tus_itm_model = msitem.Mi_model;
            //_serialList.Add(_reptPickSerial_);


            //Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, null, _inventoryHeader, _inventoryAuto, null, null, _serialList, null, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT040", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                {

                }
                else
                {
                   // MessageBox.Show("You do not have permission to cancel RCC.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }

                //if (BaseCls.GlbUserID != "ADMIN")
                //{
                //    MessageBox.Show("Still not permission to users.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                InventoryHeader _tmpInvOut = new InventoryHeader();
                InventoryHeader _tmpInvIn = new InventoryHeader();

                if (string.IsNullOrEmpty(txtRCC.Text))
                {
                    MessageBox.Show("Please select RCC number.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRCC.Focus();
                    return;
                }

                //if (!string.IsNullOrEmpty(txtRCC.Text))
                //{
                //    LoadRCCDetails();
                //    Set_check_Controls(RccStage);
                //}

                RCC _RCC = null;
                _RCC = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text);

                //if (_RCC.Inr_tp != "CUST")
                //{

                //    bool _allowCurrentTrans = false;
                //    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.Text, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                //    {
                //        if (_allowCurrentTrans == true)
                //        {
                //            if (txtDate.Value.Date != DateTime.Now.Date)
                //            {

                //                MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //    }
                //}

                if (Is_External == 0)
                {
                    if (RccStage != 1)
                    {
                        MessageBox.Show("You cannot cancel this stage RCC. Stage : " + RccStage, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (Is_External == 1)
                {
                    if (RccStage > 2)
                    {
                        MessageBox.Show("You cannot cancel external RCC in this stage. Stage : " + RccStage, "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (_RCC.Inr_tp == "STK")
                {
                    if (Is_Out == 1 || Is_In == 1)
                    {
                        MessageBox.Show("You cannot cancel this RCC. AOD is alrady confirm.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //if (Is_Out == 1)
                //{
                _tmpInvOut = new InventoryHeader();
                _tmpInvOut = CHNLSVC.Inventory.GetRccAodOut(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtRCC.Text);
                //}

                //if (Is_Out == 1 && Is_In == 1)
                //{
                //    if (_tmpInvOut != null)
                //    {
                //        _tmpInvIn = new InventoryHeader();
                //        _tmpInvIn = CHNLSVC.Inventory.GetRccAodIn(BaseCls.GlbUserComCode, _tmpInvOut.Ith_oth_loc, _tmpInvOut.Ith_doc_no);
                //    }
                //}

                string _outDoc = "";
                string _InLoc = "";
                string _InDoc = "";

                if (_tmpInvOut != null) _outDoc = _tmpInvOut.Ith_doc_no;

                if (_tmpInvIn != null)
                {
                    _InDoc = _tmpInvIn.Ith_doc_no;
                    _InLoc = _tmpInvIn.Ith_loc;
                }

                Cursor.Current = Cursors.WaitCursor;
                string _msg = string.Empty;
                //int result = CHNLSVC.Inventory.RCCCancelProcess(_outDoc, BaseCls.GlbUserID, BaseCls.GlbUserComCode, _InLoc, _InDoc, txtRCC.Text, "C", BaseCls.GlbUserDefLoca, out _msg);

                //Add new cancel method by Chamal 24/07/2014
                int result = CHNLSVC.Inventory.RCCCancelProcessForBackDate(_outDoc, BaseCls.GlbUserID, BaseCls.GlbUserComCode, _InLoc, _InDoc, txtRCC.Text, "C", BaseCls.GlbUserDefLoca, DateTime.Now.Date, BaseCls.GlbUserSessionID, out _msg);
                if (result == 1)
                {
                    Cursor.Current = Cursors.Default;
                    //MessageBox.Show("Successfully Canceled!", "RCC Calcellation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(_msg, "RCC Calcellation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_msg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                Cursor.Current = Cursors.Default;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
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
            if (string.IsNullOrEmpty(txtInvoice.Text)) return;
            bool _isExist = CHNLSVC.Inventory.IsUserEntryExist(BaseCls.GlbUserComCode, string.Empty, "INVOICE", txtInvoice.Text.Trim());
            if (_isExist)
            {
                MessageBox.Show("Selected Invoice already available. Please check for the invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoice.Clear();
                return;
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblTot.Text) > 0)
            {
                pnlPay.Visible = true;
                pnlPay.Location = new Point(2, 254);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            pnlPay.Visible = false;
            lblBal.Text = ucPayModes1.Balance.ToString("0.00");
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
                    lblTot.Text = (Convert.ToDecimal(lblTot.Text) - _amt).ToString();
                    lblBal.Text = (Convert.ToDecimal(lblBal.Text) - _amt).ToString();

                    ucPayModes1.TotalAmount = Convert.ToDecimal(lblTot.Text);
                    ucPayModes1.Amount.Text = Convert.ToString(lblBal.Text);

                    _lstCharge.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _lstCharge;
                    grvCharge.DataSource = _source;
                }
            }
        }

        private void btnCloseDef_Click(object sender, EventArgs e)
        {
            pnlDefTp.Visible = false;
            if (_isOnlineSCM2 == true && _isExternal == false)
                btnDef.Visible = true;
            else
                btnDef.Visible = false;
        }

        private void btnDef_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ItemCat))
                MessageBox.Show("Invalid item category !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                pnlDefTp.Visible = true;
                btnDef.Visible = false;
            }
        }

        private void btn_srch_def_type_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serChannel == null)
                {
                    MessageBox.Show("Invalid service agent !", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

        private bool load_def_desc()
        {
            bool _ok = false;
            lblDefDesc.Text = "";
            if (!string.IsNullOrEmpty(txtDef.Text))
            {
                DataTable _dt = null;
                if (!string.IsNullOrEmpty(_serChannel))
                    _dt = CHNLSVC.CustService.getDefectTypes(_serChannelComp, _serChannel, ItemCat, txtDef.Text);
                else
                    _dt = CHNLSVC.CustService.getDefectTypes(_serChannelComp, null, ItemCat, txtDef.Text);

                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    { lblDefDesc.Text = _dt.Rows[0]["SD_DESC"].ToString(); _ok = true; }
                }
            }
            return _ok;
        }

        private void btn_add_def_Click(object sender, EventArgs e)
        {
            Int32 _count = 0;
            if (string.IsNullOrEmpty(txtDef.Text))
            {
                SystemInformationMessage("Please select the defect type!", "Job Defect"); return;
            }

            if (_scvDefList != null)
            {
                _count = _scvDefList.Where(X => X.Srdf_def_tp == txtDef.Text).Count();
                if (_count > 0)
                {
                    SystemInformationMessage("Defect type already added into the list!", "Defect Type");
                    return;
                }
            }

            if (_scvDefList != null)
            {
                if (_scvDefList.Count > 0)
                    _count = _scvDefList.Where(X => X.Srdf_req_line == 1).Max(t => t.Srdf_def_line);
            }

            Service_Req_Def _jobDef = new Service_Req_Def();
            _jobDef.Srdf_def_tp = txtDef.Text;
            _jobDef.Srdf_def_rmk = txtDefRem.Text;
            _jobDef.Srdf_act = true;
            _jobDef.Srdf_cre_by = BaseCls.GlbUserID;
            _jobDef.Srdf_mod_by = BaseCls.GlbUserID;
            _jobDef.Srdf_req_line = 1;
            //_jobDef.Srdf_req_no =
            //_jobDef.Srdf_seq_no =
            _jobDef.Srdf_stage = "J";
            //_jobDef.Srdf_def_line = _count;
            _jobDef.SDT_DESC = lblDefDesc.Text;

            _scvDefList.Add(_jobDef);

            txtDef.Clear();
            txtDefRem.Clear();
            lblDefDesc.Text = "";

            grvDef.AutoGenerateColumns = false;
            grvDef.DataSource = new List<Service_Req_Def>();
            grvDef.DataSource = _scvDefList;

            if (MessageBox.Show("Do you want to add another defect ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                pnlDefTp.Visible = false;
                btnDef.Visible = true;
            }

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

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTel.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtTel.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid contact number.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Invalid contact number.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (Is_Dealer_Inv == true || chkReq.Checked)
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
                    MessageBox.Show("You cannot create new customer", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                if (Is_Dealer_Inv == true || chkReq.Checked)
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
                    MessageBox.Show("You cannot search customer", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Invalid contact number.", "RCC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            //    SystemWarnningMessage("Select the RCC Number", "RCC");
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
                SystemWarnningMessage("Select the service agent", "RCC");
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

            load_rcc_type();
        }

        private void load_rcc_type()
        {
            DataTable _dt = CHNLSVC.Inventory.GetMasterTypes(txtRCCType.Text);
            if (_dt.Rows.Count > 0)
            {
                lblRCCType.Text = _dt.Rows[0]["mtp_desc"].ToString();
                load_rcc_sub_type();
            }
            else
            {
                MessageBox.Show("Invalid RCC Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblRCCType.Text = "";
                txtRCCType.Focus();
            }

        }

        private void txtRCCType_Leave(object sender, EventArgs e)
        {
            lblRCCType.Text = "";
            if (!string.IsNullOrEmpty(txtRCCType.Text))
                load_rcc_type();
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

        private void btn_pcbupdate_Click(object sender, EventArgs e)
        {
            string _err="";
            string _serial=Microsoft.VisualBasic.Interaction.InputBox("Please Enter the New Serial #?", "Check MPCB Update", "");
            if (!(String.IsNullOrEmpty(_serial.Trim())))
            {
                int i = CHNLSVC.CustService.updateMPCBWarranty(_serial, out _err);

                if (_err == null)
                {
                    MessageBox.Show("Successfuly updated.", "Check MPCB Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Not updated. " + _err, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Serial Not Entered.", "Check MPCB Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlRccpendilglist.Visible = false;
        }

        //Akila 2017/12/11
        private void GenerateCustomerReminder()
        {
            try
            {
                RCC _rccDetails = new RCC();

                if (string.IsNullOrEmpty(txtRCC.Text.Trim()))
                {
                    throw new InvalidOperationException("Please enter RCC #");
                }
                else
                {
                    _rccDetails = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text.Trim());
                    if (_rccDetails != null && !string.IsNullOrEmpty(_rccDetails.Inr_acc_no))
                    {
                        if (_rccDetails.Inr_stus == "A") //check whether rcc is active
                        {
                            if (_rccDetails.Inr_stage == 4) //reminder can generate only if rcc has completed
                            {
                                DateTime? _documentDate = DateTime.Today;
                                string _reminderParameter = string.Empty;

                                if (_rccDetails.Inr_Rem_Count == 0)
                                {
                                    //1st reminder should generate after  30 days rcc completed                                    
                                    _documentDate = _rccDetails.Inr_complete_dt;
                                    _reminderParameter = "RCCREM1DT";
                                    _rccDetails.Inr_Rem_Count += 1;
                                    _rccDetails.Inr_Rem1_Snt_Date = DateTime.Today.Date;
                                }
                                else
                                {
                                    _reminderParameter = "RCCREMCOMDT";

                                    if (_rccDetails.Inr_Rem_Count == 1)
                                    {
                                        _documentDate = _rccDetails.Inr_Rem1_Snt_Date.Value;
                                        _rccDetails.Inr_Rem_Count += 1;
                                        _rccDetails.Inr_Rem2_Snt_Date = DateTime.Today.Date;
                                    }
                                    else if (_rccDetails.Inr_Rem_Count == 2)
                                    {
                                        _documentDate = _rccDetails.Inr_Rem2_Snt_Date.Value;
                                        _rccDetails.Inr_Rem_Count += 1;
                                        _rccDetails.Inr_Rem3_Snt_Date = DateTime.Today.Date;
                                    }
                                    else
                                    {
                                        _reminderParameter = string.Empty;
                                        _documentDate = DateTime.Today;
                                        throw new InvalidOperationException("Number of reminders have already sent : " + _rccDetails.Inr_Rem_Count.ToString());
                                    }
                                }

                                //get reminder generate parameters from mst_syspara_table
                                DataTable _rccReminderParameters = new DataTable();
                                _rccReminderParameters = CHNLSVC.General.GetSysParaDetails(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, _reminderParameter);

                                if (_rccReminderParameters.Rows.Count > 0)
                                {
                                    //_catagory = DAYS, MONTH, YEAR
                                    string _catagory = _rccReminderParameters.Rows[0]["msp_rest_cate_tp"] == DBNull.Value ? "DAYS" : _rccReminderParameters.Rows[0]["msp_rest_cate_tp"].ToString();

                                    //_code = DD, MON, YYYY
                                    string _code = _rccReminderParameters.Rows[0]["msp_rest_cate_cd"] == DBNull.Value ? "DD" : _rccReminderParameters.Rows[0]["msp_rest_cate_cd"].ToString();
                                    int _value = 0;

                                    int.TryParse(_rccReminderParameters.Rows[0]["msp_rest_val"].ToString(), out _value);

                                    TimeSpan _dateDifferance = DateTime.Today.Date.Subtract(_documentDate.Value.Date);

                                    int _dateTimeDifferance = 0;
                                    DateTime _nextReminderDate = DateTime.Today;
                                    if (_code == "DD")
                                    {
                                        _dateTimeDifferance = Convert.ToInt32(_dateDifferance.TotalDays);
                                        _nextReminderDate = DateTime.Today.AddDays(_value);
                                    }
                                    else if (_code == "MON")
                                    {
                                        _dateTimeDifferance = (Convert.ToInt32(_dateDifferance.TotalDays) / 365) * 12;
                                        _nextReminderDate = DateTime.Today.AddMonths(_value);
                                    }
                                    else if (_code == "YYYY")
                                    {
                                        _dateTimeDifferance = (Convert.ToInt32(_dateDifferance.TotalDays) / 365);
                                        _nextReminderDate = DateTime.Today.AddYears(_value);
                                    }

                                    if (_dateTimeDifferance >= _value)
                                    {
                                        //print report
                                        BaseCls.GlbREportRccNO = txtRCC.Text;

                                        BaseCls.GlbReportName = string.Empty;
                                        GlbReportName = string.Empty;
                                        Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();

                                        _view.GlbReportName = "RCC_Letter.rpt";
                                        BaseCls.GlbReportName = "RCC_Letter.rpt";

                                        _view.Show();
                                        _view = null;

                                        //update RCC table , reminder count, reminder date
                                        string _outMessage = null;
                                        CHNLSVC.General.UpdateRccReminderDetails(_rccDetails, out _outMessage);
                                        if (!string.IsNullOrEmpty(_outMessage))
                                        {
                                            throw new InvalidOperationException(_outMessage);
                                        }
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("Reminder cannot be generated until " + _nextReminderDate.ToString("yyyy/MMM/dd"));
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Reminder cannot be generated. Parameter details not found !");
                                }


                            }
                            else { throw new InvalidOperationException("Reminder cannot be generated for incomplete RCC"); }
                        }
                        else { throw new InvalidOperationException("Invalid RCC."); }
                    }
                    else
                    {
                        throw new InvalidOperationException("RCC details not found. Please check the RCC #");
                    }
                }
            }
            catch (InvalidOperationException oex)
            {
                Cursor = DefaultCursor;
                MessageBox.Show(oex.Message, "Generate Reminder - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Cursor = DefaultCursor;
                MessageBox.Show(ex.Message, "Generate Reminder - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrin_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateCustomerReminder();
            //    BaseCls.GlbREportRccNO = txtRCC.Text;

            //    BaseCls.GlbReportName = string.Empty;
            //    GlbReportName = string.Empty;
            //    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();

                
            ////    BaseCls.GlbReportTp = "AODACK";
            //    _view.GlbReportName = "RCC_Letter.rpt";
            //    BaseCls.GlbReportName = "RCC_Letter.rpt";
            //  //  _view.GlbReportDoc = txtReqDocNo.Text;
            //    _view.Show();
            //    _view = null;
              
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //akila 2017/12/20
        private void AddExcessItemToStock()
        {
            try
            {
                //check permission
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10101))
                //{
                //    MessageBox.Show("You don't have the permission for stock item.\nPermission Code :- 10101", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return;
                //}  

                //check rcc number
                if (string.IsNullOrEmpty(txtRCC.Text))
                {
                    MessageBox.Show("Please select RCC # !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //load rcc details
                RCC _rccDetails = new RCC();
                _rccDetails = CHNLSVC.Inventory.GetRccByNo(txtRCC.Text);

                if (_rccDetails == null || string.IsNullOrEmpty(_rccDetails.Inr_no))
                {
                    MessageBox.Show("RCC details not found. Please check the RCC # !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_rccDetails.Inr_stage != 4)
                {
                    MessageBox.Show("This RCC has not completed !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check rcc status 
                if (_rccDetails.Inr_stus == "C")
                {
                    MessageBox.Show("Invalid RCC # !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check rcc stage - if rcc has completed and customer didn't collect it from showroom, then it can ad to stock
                if (_rccDetails.Inr_stage != 4)
                {
                    MessageBox.Show("This RCC has not completed !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if(string.IsNullOrEmpty (txtItem.Text))
                {
                    MessageBox.Show("Item details not found", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if((_rccDetails.Inr_Is_Add_To_Stk == 1) && (!string.IsNullOrEmpty(_rccDetails.Inr_Req_no)))
                {
                    MessageBox.Show("This item has already been added to stock !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //to add the item back to stock rcc need to completed and should pending more than defined time period
                DateTime? _documentDate = DateTime.Today;
                _documentDate = _rccDetails.Inr_complete_dt;

                //get reminder generate parameters from mst_syspara_table
                DataTable _rccReminderParameters = new DataTable();
                _rccReminderParameters = CHNLSVC.General.GetSysParaDetails(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, "RCCREM1DT");
                if (_rccReminderParameters.Rows.Count > 0)
                {
                    //_catagory = DAYS, MONTH, YEAR
                    string _catagory = _rccReminderParameters.Rows[0]["msp_rest_cate_tp"] == DBNull.Value ? "DAYS" : _rccReminderParameters.Rows[0]["msp_rest_cate_tp"].ToString();

                    //_code = DD, MON, YYYY
                    string _code = _rccReminderParameters.Rows[0]["msp_rest_cate_cd"] == DBNull.Value ? "DD" : _rccReminderParameters.Rows[0]["msp_rest_cate_cd"].ToString();
                    
                    int _value = 0;
                    int.TryParse(_rccReminderParameters.Rows[0]["msp_rest_val"].ToString(), out _value);

                    TimeSpan _dateDifferance = DateTime.Today.Date.Subtract(_documentDate.Value.Date);

                    int _dateTimeDifferance = 0;
                    if (_code == "DD")
                    {
                        _dateTimeDifferance = Convert.ToInt32(_dateDifferance.TotalDays);
                    }
                    else if (_code == "MON")
                    {
                        _dateTimeDifferance = (Convert.ToInt32(_dateDifferance.TotalDays) / 365) * 12;
                    }
                    else if (_code == "YYYY")
                    {
                        _dateTimeDifferance = (Convert.ToInt32(_dateDifferance.TotalDays) / 365);
                    }

                    if (_dateTimeDifferance <= _value)
                    {
                        MessageBox.Show("This item cannot add to stock", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Parameter details not found !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _rccDetails.Inr_mod_by = BaseCls.GlbUserID;
                _rccDetails.Inr_session_id = BaseCls.GlbUserSessionID;
                _rccDetails.Inr_Is_Add_To_Stk = chkAddToStock.Checked ? 1 : 0;
            
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();

                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                _inventoryRequest.Itr_tp = "ADJREQ";               
                _inventoryRequest.Itr_anal1 = "EXCESS";
                _inventoryRequest.Itr_sub_tp = "STKDP";
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = "";
                _inventoryRequest.Itr_dt = DateTime.Today.Date;
                _inventoryRequest.Itr_stus = "P";
                _inventoryRequest.Itr_job_no = _rccDetails.Inr_inv_no;
                _inventoryRequest.Itr_bus_code = _rccDetails.Inr_cust_cd;
                _inventoryRequest.Itr_note = string.Empty;
                _inventoryRequest.Itr_anal2 = _rccDetails.Inr_no;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;
                _inventoryRequest.Itr_town_cd = string.Empty;
                _inventoryRequest.Itr_cur_code = string.Empty;
                _inventoryRequest.Itr_exg_rate = 0;
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                _inventoryRequest.Itr_system_module = "ADJREQ";
                _inventoryRequest.Itr_tp = "ADJREQ";

                _inventoryRequest.Rcc_Details = _rccDetails;

                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                if (_item != null && (!string.IsNullOrEmpty(_item.Mi_cd)))
                {
                    //Add request items
                    List<InventoryRequestItem> _itemList = new List<InventoryRequestItem>();
                    InventoryRequestItem _requestItem = new InventoryRequestItem();
                    _requestItem.Itri_line_no = 1;
                    _requestItem.Itri_bqty = 0;
                    _requestItem.Itri_mitm_stus = string.IsNullOrEmpty(txtItmStus.Text) ? "GOD" : txtItmStus.Text.Trim().ToUpper();
                    _requestItem.Itri_mitm_cd = txtItem.Text;
                    _requestItem.Itri_itm_cd = txtItem.Text;
                    _requestItem.Itri_itm_stus = string.IsNullOrEmpty(txtItmStus.Text) ? "GOD" : txtItmStus.Text.Trim().ToUpper();
                    _requestItem.Itri_qty = 1;
                    _requestItem.Itri_app_qty = 1;
                    _requestItem.Tmp_user_id = BaseCls.GlbUserID;
                    _requestItem.Mi_session_id = BaseCls.GlbUserSessionID;
                    _itemList.Add(_requestItem);
                    _inventoryRequest.InventoryRequestItemList = _itemList;

                    if (_itemList != null && _itemList.Count > 0)
                    {
                        //add serials
                        List<InventoryRequestSerials> InventoryRequestSersList = new List<InventoryRequestSerials>();
                        int _count = 1;
                        foreach (InventoryRequestItem _inventoryItem in _itemList)
                        {
                            InventoryRequestSerials _InventoryRequestSerials = new InventoryRequestSerials();
                            _InventoryRequestSerials.Itrs_line_no = _count;
                            _InventoryRequestSerials.Itrs_ser_line = _count;
                            _InventoryRequestSerials.Itrs_itm_cd = _inventoryItem.Itri_itm_cd;
                            _InventoryRequestSerials.Itrs_itm_stus = _inventoryItem.Itri_itm_stus;
                            _InventoryRequestSerials.Itrs_ser_1 = txtSerial.Text.Trim().ToUpper();
                            _InventoryRequestSerials.Itrs_ser_2 = "N/A";
                            _InventoryRequestSerials.Itrs_ser_3 = "N/A";
                            _InventoryRequestSerials.Itrs_qty = 1;
                            _InventoryRequestSerials.Tmp_user_id = BaseCls.GlbUserID;
                            _InventoryRequestSerials.Mi_session_id = BaseCls.GlbUserSessionID;

                            //get serial id

                            InventorySerialN _itemDetails = new InventorySerialN();
                            _itemDetails.Ins_com = BaseCls.GlbUserComCode;
                            _itemDetails.Ins_itm_cd = _inventoryItem.Itri_itm_cd;
                            _itemDetails.Ins_itm_stus = _inventoryItem.Itri_itm_stus;
                            _itemDetails.Ins_ser_1 = txtSerial.Text.Trim().ToUpper();

                            List<InventorySerialN> _serialDetails = new List<InventorySerialN>();
                            _serialDetails = CHNLSVC.Inventory.Get_INR_SER_DATA(_itemDetails);
                            if (_serialDetails != null && _serialDetails.Count > 0)
                            {
                                var _availableSerials = _serialDetails.Where(x => x.Ins_available == 1 || x.Ins_available == -1).ToList();
                                if (_availableSerials != null && _availableSerials.Count > 0)
                                {
                                    MessageBox.Show(string.Format("Adjustment request cannot be generated. Serial - {0} is available on {1}!", _availableSerials.SingleOrDefault().Ins_ser_1, _availableSerials.SingleOrDefault().Ins_loc), "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    _InventoryRequestSerials.Itrs_ser_id = _serialDetails.FirstOrDefault().Ins_ser_id;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Adjustment request cannot be generated. Serial details not found !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            _count += 1;
                            InventoryRequestSersList.Add(_InventoryRequestSerials);
                        }
                        _inventoryRequest.InventoryRequestSerialsList = InventoryRequestSersList;
                    }
                    else
                    {
                        MessageBox.Show("Adjustment request cannot be generate. Item details not found !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Adjustment request cannot be generate. Item details not found !", "Add To Stock - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                      
                

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJREQ";
                masterAuto.Aut_number = 0;
                masterAuto.Aut_start_char = "ADJREQ";
                masterAuto.Aut_year = null;


                int rowsAffected = 0;
                string _docNo = string.Empty;
                string _reqdNum = "";

                rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, masterAuto, out _docNo);
                
                if (rowsAffected == 1)
                {
                    MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Process Terminated", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while updating excess item details !" + Environment.NewLine + ex.Message, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkByAll_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


