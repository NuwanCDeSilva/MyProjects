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
using System.Linq;
using System.Linq.Expressions;

namespace FF.WindowsERPClient.General
{
    public partial class HpManualReceiptRequestApproval : Base
    {
        private RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqAppDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqAppSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqAppDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

        private List<RequestApprovalSerials> _ReqAppSerTemp = new List<RequestApprovalSerials>();

        private Boolean _isAppUser = false;
        private Int32 _appLvl = 0;
        private Int32 _line = 0;

        public HpManualReceiptRequestApproval()
        {
            InitializeComponent();
        }


        #region Common Searching Area
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

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            txtReqLoc.Text = "";
            _line = 0;
            _ReqAppHdr = new RequestApprovalHeader();
            _ReqAppDet = new List<RequestApprovalDetail>();
            _ReqAppSer = new List<RequestApprovalSerials>();
            _ReqAppAuto = new MasterAutoNumber();

            _ReqAppHdrLog = new RequestApprovalHeaderLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();
            _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

            _ReqAppSerTemp = new List<RequestApprovalSerials>();

            _isAppUser = false;
            _appLvl = 0;
            lblReq.Text = "";
            lblReqPC.Text = "";
            lblStatus.Text = "";
            txtManRec.Text = "";
            txtRemarks.Text = "";
            ddlPrefix.Enabled = true;
            txtManRec.Enabled = true;
            txtRemarks.ReadOnly = false;

            SystemAppLevelParam _sysApp = new SystemAppLevelParam();

            _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT033", BaseCls.GlbUserID);
            if (_sysApp.Sarp_cd != null)
            {
                chkApp.Checked = true;
                chkApp.Enabled = true;
                _isAppUser = true;
                _appLvl = _sysApp.Sarp_app_lvl;
            }

            if (_isAppUser == true)
            {
                btnApp.Enabled = true;
                btnRej.Enabled = true;
                txtReqLoc.Enabled = true;
            }
            else
            {
                btnApp.Enabled = false;
                btnRej.Enabled = false;
                txtReqLoc.Enabled = false;
            }

            List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
            if (chkApp.Checked == false)
            {
                _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", null, txtReqLoc.Text);
            }
            else
            {
                _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", BaseCls.GlbUserID, txtReqLoc.Text);
            }

            //Delete hp temp manual receipt
            CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.GlbModuleName);

            dgvPendings.AutoGenerateColumns = false;
            dgvPendings.DataSource = new List<RequestApprovalHeader>();
            dgvPendings.DataSource = _TempReqAppHdr;

            gvReceipts.AutoGenerateColumns = false;
            gvReceipts.DataSource = new List<RequestApprovalSerials>();


            loadPrifixes();
        }

        private void HpManualReceiptRequestApproval_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void dgvPendings_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string _reqNo = "";
            string _stus = "";
            string _remarks = "";
            string _pc = "";

            _reqNo = dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value.ToString();
            _stus = dgvPendings.Rows[e.RowIndex].Cells["col_Status"].Value.ToString();
            _remarks = dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value.ToString();
            _pc = dgvPendings.Rows[e.RowIndex].Cells["col_pc"].Value.ToString();

            lblReq.Text = _reqNo;
            txtRemarks.Text = _remarks;
            lblReqPC.Text = _pc;

            if (_stus == "A")
            {
                lblStatus.Text = "APPROVED";
                //  btnRegDetails.Enabled =  true;
            }
            else if (_stus == "P")
            {
                lblStatus.Text = "PENDING";
            }
            else if (_stus == "R")
            {
                lblStatus.Text = "REJECT";
            }
            else if (_stus == "F")
            {
                lblStatus.Text = "FINISHED";
            }

            DataTable _d = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, _reqNo, out _ReqAppSer);

            gvReceipts.AutoGenerateColumns = false;
            gvReceipts.DataSource = new List<RequestApprovalHeader>();
            gvReceipts.DataSource = _ReqAppSer;

            btnRequest.Enabled = false;
            txtRemarks.ReadOnly = true;
            ddlPrefix.Enabled = false;
            txtManRec.Enabled = false;
        }

        private void loadPrifixes()
        {
            try
            {
                //MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, ddl_Location.SelectedValue.Trim());
                MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                string docTp = "";
                { docTp = "HPRM"; }

                List<string> prifixes = new List<string>();
                prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
                ddlPrefix.DataSource = prifixes;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", BaseCls.GlbUserID, txtReqLoc.Text);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPendings.DataSource = _TempReqAppHdr;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtReqLoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtReqLoc;
                    _CommonSearch.ShowDialog();
                    txtReqLoc.Select();

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnRefresh.Focus();
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

        private void txtReqLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReqLoc.Text))
                {
                    Int32 _isValidPC = 0;

                    _isValidPC = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtReqLoc.Text.Trim());

                    if (_isValidPC == 0)
                    {
                        MessageBox.Show("Invalid or accsess denied location.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReqLoc.Text = "";
                        txtReqLoc.Focus();
                    }
                    else
                    {

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

        private void btnAddRec_Click(object sender, EventArgs e)
        {
            try
            {


                if (ddlPrefix.Text == "")
                {
                    MessageBox.Show("Please select receipt prefix.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlPrefix.Focus();
                    return;
                }

                if (txtManRec.Text == "")
                {
                    MessageBox.Show("Please select receipt number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }


                string location = BaseCls.GlbUserDefProf;
                string receipt_type = "";
                RecieptHeader Rh = null;
                receipt_type = "HPRM";

                Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.Text.Trim(), txtManRec.Text.Trim());

                if (Rh != null)
                {
                    MessageBox.Show("Receipt number : " + txtManRec.Text + "already used.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }


                for (int x = 0; x < gvReceipts.Rows.Count; x++)
                {

                    string prefix = gvReceipts.Rows[x].Cells["col_recPrefix"].Value.ToString();
                    Int32 recNo = Convert.ToInt32(gvReceipts.Rows[x].Cells["col_recMannual"].Value);

                    if (prefix == ddlPrefix.Text.Trim() && recNo == Convert.ToInt32(txtManRec.Text.Trim()))
                    {
                        MessageBox.Show("Manual receipt number already used.", "Manual Receipt Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManRec.Focus();
                        return;
                    }
                }


                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();


                _tempReqAppSer.Gras_ref = null;
                _tempReqAppSer.Gras_line = 1;
                _tempReqAppSer.Gras_anal1 = null;
                _tempReqAppSer.Gras_anal2 = ddlPrefix.Text.Trim();
                _tempReqAppSer.Gras_anal3 = null;
                _tempReqAppSer.Gras_anal4 = txtManRec.Text.Trim();
                _tempReqAppSer.Gras_anal5 = "";
                _tempReqAppSer.Gras_anal6 = 0;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;


                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);
                //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    MessageBox.Show("Invalid receipt number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }
                else
                {
                    int X1 = CHNLSVC.Inventory.save_temp_existing_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);
                    _ReqAppSerTemp.Add(_tempReqAppSer);
                    gvReceipts.AutoGenerateColumns = false;
                    gvReceipts.DataSource = new List<RequestApprovalSerials>();
                    gvReceipts.DataSource = _ReqAppSerTemp;
                }


                txtManRec.Text = "";
                txtManRec.Focus();
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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = "";
                string _docNo = "";

                if (gvReceipts.Rows.Count == 0)
                {
                    MessageBox.Show("Please select relavant receipts.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }

                CollectReqApp();
                CollectReqAppLog();


                int effet = CHNLSVC.Sales.SaveManualHpRecReq(_ReqAppHdr, _ReqAppSer, _ReqAppAuto, _ReqAppHdrLog, _ReqAppSerLog, out _docNo);

                if (effet == 1)
                {

                    MessageBox.Show("Request generated." + _docNo, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {

                        MessageBox.Show("Error." + _msg, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Creation fail.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        protected void CollectReqApp()
        {


            _ReqAppHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqAppDet = new List<RequestApprovalDetail>();
            _ReqAppSer = new List<RequestApprovalSerials>();
            _ReqAppAuto = new MasterAutoNumber();


            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = "ARQT033";
            _ReqAppHdr.Grah_fuc_cd = null;
            _ReqAppHdr.Grah_ref = null;
            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = "P";
            _ReqAppHdr.Grah_app_lvl = 0;
            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdr.Grah_sub_type = null;
            _ReqAppHdr.Grah_oth_pc = null;


            if (_ReqAppSerTemp.Count > 0)
            {
                Int32 _line = 0;
                foreach (RequestApprovalSerials ser in _ReqAppSerTemp)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Gras_anal1;
                    _tempReqAppSer.Gras_anal2 = ser.Gras_anal2;
                    _tempReqAppSer.Gras_anal3 = ser.Gras_anal3;
                    _tempReqAppSer.Gras_anal4 = ser.Gras_anal4;
                    _tempReqAppSer.Gras_anal5 = ser.Gras_anal5;
                    _tempReqAppSer.Gras_anal6 = ser.Gras_anal6;
                    _tempReqAppSer.Gras_anal7 = ser.Gras_anal7;
                    _tempReqAppSer.Gras_anal8 = ser.Gras_anal8;
                    _tempReqAppSer.Gras_anal9 = ser.Gras_anal9;
                    _tempReqAppSer.Gras_anal10 = ser.Gras_anal10;

                    _ReqAppSer.Add(_tempReqAppSer);
                }
            }



            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQDPREC";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "REQDPREC";
            _ReqAppAuto.Aut_year = null;
        }

        protected void CollectReqAppLog()
        {
            string _type = "";
            _ReqAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();
            _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

            _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdrLog.Grah_app_tp = "ARQT033";
            _ReqAppHdrLog.Grah_fuc_cd = null;
            _ReqAppHdrLog.Grah_ref = null;
            _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = "P";
            _ReqAppHdrLog.Grah_app_lvl = 0;
            _ReqAppHdrLog.Grah_app_by = string.Empty;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdrLog.Grah_sub_type = null;
            _ReqAppHdrLog.Grah_oth_pc = null;



            if (_ReqAppSerTemp.Count > 0)
            {
                Int32 _line = 0;
                foreach (RequestApprovalSerials ser in _ReqAppSerTemp)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Gras_anal1;
                    _tempReqAppSer.Gras_anal2 = ser.Gras_anal2;
                    _tempReqAppSer.Gras_anal3 = ser.Gras_anal3;
                    _tempReqAppSer.Gras_anal4 = ser.Gras_anal4;
                    _tempReqAppSer.Gras_anal5 = ser.Gras_anal5;
                    _tempReqAppSer.Gras_anal6 = ser.Gras_anal6;
                    _tempReqAppSer.Gras_anal7 = ser.Gras_anal7;
                    _tempReqAppSer.Gras_anal8 = ser.Gras_anal8;
                    _tempReqAppSer.Gras_anal9 = ser.Gras_anal9;
                    _tempReqAppSer.Gras_anal10 = ser.Gras_anal10;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqAppSerLog.Add(_tempReqAppSer);
                }
            }

        }

        private void HpManualReceiptRequestApproval_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.GlbModuleName);
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

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to remove last manual receipt ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    List<RequestApprovalSerials> _temp = new List<RequestApprovalSerials>();
                    _temp = _ReqAppSerTemp;

                    int row_id = gvReceipts.Rows.Count - 1;//the last index?

                    string prefix = Convert.ToString(gvReceipts.Rows[row_id].Cells["col_recPrefix"].Value);
                    string receiptNo = Convert.ToString(gvReceipts.Rows[row_id].Cells["col_recMannual"].Value);
                    
                    _temp.RemoveAll(x => x.Gras_anal2 == prefix && x.Gras_anal4 == receiptNo);
                    _ReqAppSerTemp = _temp;

                    gvReceipts.AutoGenerateColumns = false;
                    gvReceipts.DataSource = new List<RecieptHeader>();
                    gvReceipts.DataSource = _ReqAppSerTemp;


                    Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, prefix, Convert.ToInt32(receiptNo));
                    effect = CHNLSVC.Inventory.delete_temp_current_receipt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, prefix, Convert.ToInt32(receiptNo));
                    
                }
            }
            catch (Exception ex)
            {
                return;
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

              

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = lblReq.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "A";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnRej_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = lblReq.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "R";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void txtManRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddRec.Focus();
            }
        }

    }
}
