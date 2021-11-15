using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
namespace FF.WindowsERPClient.Services
{
    public partial class ServiceJobTransfer : Base
    {
        private List<Service_JOB_HDR> _jobTransList = null;
        private List<Service_job_Det> _jobSelectItems = new List<Service_job_Det>();
        private List<Service_job_Det> _jobTransItems = null;
        private Service_Chanal_parameter _scvParam = null;
        private string _itemType = "";
        private string _itmBrand = "";
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        public ServiceJobTransfer()
        {
            InitializeComponent();
            txtFrom.Value = txtTo.Value.Date.AddMonths(-1);
            dtpDate.Value = CHNLSVC.Security.GetServerDateTime();
            btnSearch_Click(null, null);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                //case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + null + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJObSerarhEnquiry:
                    {
                      //  paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator);
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        private void btnSrhLocation_Click(object sender, EventArgs e)
        {
            try
            {


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.General.GetServiceLocation(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _jobTransList = new List<Service_JOB_HDR>();
            _jobTransList = CHNLSVC.CustService.getTransferJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtFrom.Value, txtTo.Value, txtJobNumber.Text);
            if (_jobTransList != null)
            {
                _jobTransList = _jobTransList.Distinct().ToList();
                dgvTranfer.AutoGenerateColumns = false;
                dgvTranfer.DataSource = _jobTransList;
            }
        }

        private void dgvTranfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _jobTransItems = new List<Service_job_Det>();
            _jobTransItems = CHNLSVC.CustService.GetPcJobDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dgvTranfer.Rows[e.RowIndex].Cells["col_job"].Value.ToString());
            if (_jobTransItems != null)
            {


                foreach (Service_job_Det _jitem in _jobTransItems)
                {
                    _jitem.Jbd_select = true;
                }
                dgvJobItems.AutoGenerateColumns = false;
                dgvJobItems.DataSource = new List<Service_job_Det>();
                dgvJobItems.DataSource = _jobTransItems;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            if (_jobTransItems != null)
            {
                if (_jobSelectItems != null)
                {
                    if ((_jobSelectItems.FindAll(x => (x.Jbd_jobno == dgvJobItems.SelectedCells[6].Value.ToString() && x.Jbd_jobline == Convert.ToInt32(dgvJobItems.SelectedCells[5].Value.ToString())))).Count > 0)
                    {
                        MessageBox.Show("This Job serial already exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                foreach (Service_job_Det _jitem in _jobTransItems)
                {
                    if (_jitem.Jbd_select == true)
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _jitem.Jbd_itm_cd);
                        if (_item != null)
                        {
                            _itemType = _item.Mi_itm_tp;

                            _itmBrand = _item.Mi_brand;

                        }
                        if (_jitem.Jbd_stage >= 8) // Invoiced Job
                        {
                            MessageBox.Show("Unable to transfer this job item, already invoiced. Job #" + _jitem.Jbd_jobno + " Item " + _jitem.Jbd_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_jitem.Jbd_stage == 4 || _jitem.Jbd_stage == 5) //  WIP OPen / Re-open
                        {
                            MessageBox.Show("Unable to transfer this job item, WIP open. Job #" + _jitem.Jbd_jobno + " Item " + _jitem.Jbd_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        List<Service_confirm_Header> _confHdr = CHNLSVC.CustService.GetConfDetByJobNo(BaseCls.GlbUserComCode, _jitem.Jbd_jobno, _jitem.Jbd_jobline);

                        foreach (Service_confirm_Header item in _confHdr)
                        {
                            List<Service_Confirm_detail> _confdet = (CHNLSVC.CustService.GetServiceConfirmDetJob(_jitem.Jbd_jobno, item.Jch_no)).FindAll(x=> x.Jcd_joblineno !=_jitem.Jbd_jobline);

                            if (_confdet.Count > 1   )
                            {
                                MessageBox.Show("pls cancel exsting confirmation and confirm again for separate items" + _jitem.Jbd_jobno, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        }


                        List<Service_WCN_Detail> _warr = CHNLSVC.CustService.GetSupWarntyClaimAvb(_jitem.Jbd_jobno, _jitem.Jbd_jobline);
                        if (_warr.Count > 1)
                        {
                            MessageBox.Show("unable to tranfer this job , already  raised warranty claim " + _jitem.Jbd_jobno + " Item " + _jitem.Jbd_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


                _jobSelectItems.AddRange(_jobTransItems.Where(S => (S.Jbd_select == true)));
                dgvSelected.AutoGenerateColumns = false;
                dgvSelected.DataSource = new List<Service_job_Det>();
                dgvSelected.DataSource = _jobSelectItems;

            }

        }

        private void btnSrhLocation_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtPC.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtPC.Text.ToString());
                    if (_masterLocation == null)
                    {

                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPC.Clear();
                        txtPC.Focus();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrhLocation_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btnSrhLocation_Click(null, null);

        }

        private void ServiceJobTransfer_Load(object sender, EventArgs e)
        {
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                SystemWarnningMessage("Service parameter(s) not setup!", "Default Parameter(s)");
                this.Close();
            }
        }
        #region Common Message
        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to transfer this job?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDate, label3, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
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


            if (dgvSelected.Rows.Count == 0)
            {
                MessageBox.Show("Please enter details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = true;
                txtFrom.Focus();
                return;
            }
            if (txtPC.Text.Trim() == "")
            {
                MessageBox.Show("Please enter location code!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = true;
                txtPC.Focus();
                return;
            }
            jobTanfer();
        }
        private void jobTanfer()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            try
            {

                SCV_TRANS_LOG _jobTrans = new SCV_TRANS_LOG();
                btnSave.Enabled = false;


                List<SCV_TRANS_LOG> _jobTransList = new List<SCV_TRANS_LOG>();

                foreach (Service_job_Det item in _jobSelectItems)
                {
                    _jobTrans.Stl_seq = 0;
                    _jobTrans.Stl_jobseq = item.Jbd_seq_no;
                    _jobTrans.Stl_jobno = item.Jbd_jobno;
                    _jobTrans.Stl_jobline = item.Jbd_jobline;
                    _jobTrans.Stl_sjobno = string.Empty;
                    _jobTrans.Stl_cur_loc = txtPC.Text;
                    _jobTrans.Stl_from_loc = item.Jbd_loc;
                    _jobTrans.Stl_cre_by = BaseCls.GlbUserID;
                    _jobTrans.Stl_trns_dt = _date.Date;
                    _jobTransList.Add(_jobTrans);


                }
                string transRef;

                MasterAutoNumber _jobAuto = new MasterAutoNumber();

                _jobAuto.Aut_cate_cd = txtPC.Text;
                _jobAuto.Aut_cate_tp = "LOC";
                _jobAuto.Aut_moduleid = "SVJOB";
                _jobAuto.Aut_direction = 0;
                _jobAuto.Aut_year = _date.Year;

                foreach (Service_job_Det _jitem in _jobSelectItems)
                {
                    txtNewPartWarehouse.Text = txtPC.Text;
                    txtOPWarehouse.Text = txtPC.Text;
                    GblJobNum = _jitem.Jbd_jobno;
                    GbljobLineNum = _jitem.Jbd_jobline;

                    getOldParts();
                    getNewItems();

                }
                if (pnlMain.Visible == true)
                {
                    return;
                }
                int effet = CHNLSVC.CustService.Save_Job_Transfer(_jobSelectItems, _jobTransList, _jobAuto, txtPC.Text, _date, BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, _itemType, _itmBrand, 0, _scvParam.SP_TRANS_JOB_CLOSE, BaseCls.GlbUserSessionID, BaseCls.GlbUserDefProf, out transRef);


                if (effet > 0)
                {
                    clear();
                    MessageBox.Show(transRef);
                    // Clear_Data();
                }
                else
                {
                    btnSave.Enabled = true;
                    MessageBox.Show("Process terminated. - " + transRef);
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void getNewItems()
        {
            dgvNewParts.AutoGenerateColumns = false;
            List<Service_stockReturn> oNewItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, string.Empty, BaseCls.GlbUserDefLoca);
            dgvNewParts.DataSource = new List<Service_stockReturn>();

            List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, GbljobLineNum);

            foreach (Service_Job_Det_Sub item in oSubItems)
            {
                oNewItems.RemoveAll(x => x.SERIAL_ID == item.JBDS_REPL_SERID.ToString());
            }

            dgvNewParts.DataSource = oNewItems;

            modifyGrid();

            if (oNewItems.Count > 0)
            {
                pnlMain.Visible = true;
                pnlMain.Width = 593;
                pnlMain.Height = 481;
            }
        }
        private void getOldParts()
        {
            List<Service_OldPartRemove> oldPartList = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, string.Empty, string.Empty).FindAll(x => x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0);
            if (oldPartList.Count > 0)
            {
                foreach (Service_OldPartRemove item in oldPartList)
                {
                    item.SourceTable = "OLD";
                }
                dgvOldParts.AutoGenerateColumns = false;
                dgvOldParts.DataSource = new List<Service_OldPartRemove>();
                dgvOldParts.DataSource = oldPartList;
                modifyGrid();
            }

            //List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, GbljobLineNum);
            //if (oSubItems.Count > 0)
            //{
            //    foreach (Service_Job_Det_Sub item in oSubItems)
            //    {
            //        Service_OldPartRemove oldItem = new Service_OldPartRemove();
            //        oldItem.SOP_SEQNO = item.JBDS_SEQ_NO;
            //        oldItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
            //        oldItem.SOP_OLDITMSTUS_Text = item.JBDS_ITM_STUS_TEXT;
            //        oldItem.SOP_OLDITMSTUS = item.JBDS_ITM_STUS;
            //        oldItem.SOP_OLDITMSER1 = item.JBDS_SER1;
            //        oldItem.SOP_OLDITMQTY = item.JBDS_QTY;
            //        oldItem.SOP_CRE_DT = item.JBDS_CRE_DT;
            //        oldItem.SOP_RMK = item.JBDS_WARR_RMK;
            //        oldItem.SOP_REQWCN = item.JBDS_LINE; // use when update the line
            //        oldItem.SourceTable = "SUB";
            //        oldPartList.Add(oldItem);
            //    }
            //    dgvOldParts.AutoGenerateColumns = false;
            //    dgvOldParts.DataSource = new List<Service_OldPartRemove>();
            //    dgvOldParts.DataSource = oldPartList;
            //    modifyGrid();
            //    if (oldPartList.Count > 0)
            //    {
            //        pnlMain.Visible = true;
            //        pnlMain.Width = 593;
            //        pnlMain.Height = 481;
            //    }
            //}
        }
        private void modifyGrid()
        {
            if (dgvOldParts.Rows.Count > 0)
            {
                for (int i = 0; i < dgvOldParts.Rows.Count; i++)
                {
                    if (dgvOldParts.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                    {
                        dgvOldParts.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                    }

                    if (dgvOldParts.Rows[i].Cells["SourceTable"].Value.ToString() != "SUB")
                    {
                        dgvOldParts.Rows[i].Cells["NewReplace"].ReadOnly = true;
                        dgvOldParts.Rows[i].Cells["NewItems"].ReadOnly = true;
                        dgvOldParts.Rows[i].Cells["NewSerial"].ReadOnly = true;
                    }
                }
            }
            if (dgvNewParts.Rows.Count > 0)
            {
                for (int i = 0; i < dgvNewParts.Rows.Count; i++)
                {
                    if (dgvNewParts.Rows[i].Cells["Serial"].Value.ToString() == "N/A")
                    {
                        dgvNewParts.Rows[i].Cells["Qty"].ReadOnly = false;
                    }
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clear();
            }
        }
        private void clear()
        {
            dgvSelected.AutoGenerateColumns = false;
            _jobSelectItems = null;
            _jobTransItems = null;
            _jobTransList = null;
            _jobTransItems = null;
            btnSave.Enabled = true;
            dgvTranfer.DataSource = null;
            dgvJobItems.DataSource = null;
            dgvSelected.DataSource = null;
            dgvOldParts.AutoGenerateColumns = false;
            dgvOldParts.DataSource = new List<Service_OldPartRemove>();
            dgvNewParts.AutoGenerateColumns = false;
            dgvNewParts.DataSource = new List<Service_stockReturn>();
            txtNewPartWarehouse.Clear();
            txtOPWarehouse.Clear();
            txtPC.Text = "";
            txtJobNumber.Text = "";
            pnlMain.Visible = false;
            _jobSelectItems = new List<Service_job_Det>();

            dgvSelected.DataSource = new List<Service_job_Det>();

            btnSearch_Click(null, null);

        }

        private void btnHide_Click(object sender, EventArgs e)
        {

        }

        private bool isAnySelectedNewPart()
        {
            bool status = false;

            if (dgvNewParts.Rows.Count > 0)
            {
                for (int i = 0; i < dgvNewParts.Rows.Count; i++)
                {
                    if (dgvNewParts.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvNewParts.Rows[i].Cells["select"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private bool isAnySelectedOldPart()
        {
            bool status = false;

            if (dgvOldParts.Rows.Count > 0)
            {
                for (int i = 0; i < dgvOldParts.Rows.Count; i++)
                {
                    if (dgvOldParts.Rows[i].Cells["selectOP"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[i].Cells["selectOP"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        private void btnNPReturn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to return?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                if (isAnySelectedNewPart())
                {
                    List<Service_stockReturn> oNewItems = new List<Service_stockReturn>();

                    for (int i = 0; i < dgvNewParts.Rows.Count; i++)
                    {
                        if (dgvNewParts.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvNewParts.Rows[i].Cells["select"].Value) == true)
                        {
                            Service_stockReturn item = new Service_stockReturn();
                            item.ITEM_CODE = dgvNewParts.Rows[i].Cells["Item"].Value.ToString();
                            item.STATUS_CODE = dgvNewParts.Rows[i].Cells["Status"].Value.ToString();
                            item.SERIAL_NO = dgvNewParts.Rows[i].Cells["Serial"].Value.ToString();
                            item.QTY = Convert.ToDecimal(dgvNewParts.Rows[i].Cells["Qty"].Value.ToString());
                            item.SERIAL_ID = dgvNewParts.Rows[i].Cells["SerialID"].Value.ToString();
                            item.JOB_NO = dgvNewParts.Rows[i].Cells["job"].Value.ToString();
                            item.JOB_LINE = Convert.ToInt32(dgvNewParts.Rows[i].Cells["jobline"].Value.ToString());
                            oNewItems.Add(item);
                        }
                    }

                    Int32 result = 0;
                    string docNum = string.Empty;
                    result = CHNLSVC.CustService.Update_NewItems_ReturnWarehouse_JobTrans(oNewItems, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserSessionID, txtOPWarehouse.Text, dtpDate.Value, out docNum);

                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\nDocument Numbers " + docNum, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // btnView_Click(null, null);
                        dgvNewParts.AutoGenerateColumns = false;
                        dgvNewParts.DataSource = null;
                        if (dgvOldParts.Rows.Count == 0)
                        {
                            pnlMain.Visible = false;
                            jobTanfer();
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Select items.", "Job Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnOPReturn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to return?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                //List<Int32> _ReturnItemList = new List<Int32>();
                List<Tuple<Int32, String, String>> _ReturnItemList = new List<Tuple<Int32, String, String>>();


                if (isAnySelectedOldPart())
                {
                    List<Service_Job_Det_Sub> selectedITem = new List<Service_Job_Det_Sub>();

                    for (int i = 0; i < dgvOldParts.Rows.Count; i++)
                    {
                        //if (dgvOldParts.Rows[i].Cells["selectOP"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[i].Cells["selectOP"].Value) == true && dgvOldParts.Rows[i].Cells["SourceTable"].Value.ToString() != "SUB")
                        //{
                        //    Int32 SeqNo = Convert.ToInt32(dgvOldParts.Rows[i].Cells["sop_seqno"].Value.ToString());
                        //    _ReturnItemList.Add(SeqNo);
                        //}
                        if (dgvOldParts.Rows[i].Cells["selectOP"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[i].Cells["selectOP"].Value) == true)
                        {
                            Int32 SeqNo = Convert.ToInt32(dgvOldParts.Rows[i].Cells["sop_seqno"].Value.ToString());
                            _ReturnItemList.Add(new Tuple<Int32, String, String>(SeqNo, dgvOldParts.Rows[i].Cells["SourceTable"].Value.ToString(), dgvOldParts.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString()));
                        }

                        if (dgvOldParts.Rows[i].Cells["SourceTable"].Value.ToString() == "SUB" && dgvOldParts.Rows[i].Cells["NewReplace"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[i].Cells["NewReplace"].Value) == true)
                        {
                            Service_Job_Det_Sub subItem = new Service_Job_Det_Sub();
                            subItem.JBDS_SEQ_NO = Convert.ToInt32(dgvOldParts.Rows[i].Cells["sop_seqno"].Value.ToString());
                            subItem.JBDS_LINE = Convert.ToInt32(dgvOldParts.Rows[i].Cells["SOP_REQWCN"].Value.ToString());
                            subItem.JBDS_JOBLINE = GbljobLineNum;
                            subItem.JBDS_ITM_CD = dgvOldParts.Rows[i].Cells["NewItems"].Value.ToString();
                            subItem.JBDS_SER1 = dgvOldParts.Rows[i].Cells["NewSerial"].Value.ToString();
                            subItem.JBDS_JOBNO = GblJobNum;
                            selectedITem.Add(subItem);
                        }
                    }

                    Int32 result = 0;
                    string docNum = string.Empty;
                    result = CHNLSVC.CustService.Update_Olppart_ReturnWarehouse_JobTrans(_ReturnItemList, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserSessionID, txtOPWarehouse.Text, selectedITem, out docNum, GblJobNum, GbljobLineNum);

                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\n Doc num : " + docNum, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //  btnView_Click(null, null);
                        dgvOldParts.AutoGenerateColumns = false;
                        dgvOldParts.DataSource = null;
                        if (dgvNewParts.Rows.Count == 0)
                        {
                            pnlMain.Visible = false;
                            jobTanfer();
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Select items.", "Job Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void dgvJobItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJobItems.Rows.Count > 0 && e != null)
            {
                if (Convert.ToBoolean(dgvJobItems.Rows[e.RowIndex].Cells[4].Value.ToString()) == true)
                {
                    dgvJobItems.Rows[e.RowIndex].Cells[4].Value = Convert.ToString(false);

                }
                else
                {
                    dgvJobItems.Rows[e.RowIndex].Cells[4].Value = Convert.ToString(true);
                }
            }
        }

        private void dgvJobItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            if (txtPC.Text == BaseCls.GlbUserDefLoca)
            {
                MessageBox.Show("Same location not allowed for tansfer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtPC.Text)) return;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.General.GetServiceLocation(_CommonSearch.SearchParams, null, null);
            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtPC.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Please select the valid location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPC.Clear();
                txtPC.Focus();
                return;
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (Service_job_Det _jitem in _jobTransItems)
            {
                _jitem.Jbd_select = true;
            }
            dgvJobItems.AutoGenerateColumns = false;
            dgvJobItems.DataSource = new List<Service_job_Det>();
            dgvJobItems.DataSource = _jobTransItems;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (Service_job_Det _jitem in _jobTransItems)
            {
                _jitem.Jbd_select = false;
            }
            dgvJobItems.AutoGenerateColumns = false;
            dgvJobItems.DataSource = new List<Service_job_Det>();
            dgvJobItems.DataSource = _jobTransItems;
        }

        private void chkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSel.Checked == true)
            {
                foreach (Service_job_Det _jitem in _jobTransItems)
                {
                    _jitem.Jbd_select = true;
                }
            }
            else
            {
                foreach (Service_job_Det _jitem in _jobTransItems)
                {
                    _jitem.Jbd_select = false;
                }
            }
            dgvJobItems.AutoGenerateColumns = false;
            dgvJobItems.DataSource = new List<Service_job_Det>();
            dgvJobItems.DataSource = _jobTransItems;
        }

        private void dgvSelected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSelected_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete this item ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string _job = dgvSelected.Rows[e.RowIndex].Cells[0].Value.ToString();
                int _jobline = Convert.ToInt16(dgvSelected.Rows[e.RowIndex].Cells[4].Value.ToString());
                _jobSelectItems.RemoveAll(p => (p.Jbd_jobno == _job && p.Jbd_jobline == _jobline));
                dgvSelected.AutoGenerateColumns = false;
                dgvSelected.DataSource = new List<Service_job_Det>();
                dgvSelected.DataSource = _jobSelectItems;
            }
        }

        private void btn_srch_req_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            //_CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            //_CommonSearch.dtpTo.Value = DateTime.Today;
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtJobNumber;
            //this.Cursor = Cursors.Default;
            //_CommonSearch.IsSearchEnter = true;
            //_CommonSearch.ShowDialog();
            //txtJobNumber.Focus();
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJObSerarhEnquiry);
            //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsEnqeuiy(_CommonSearch.SearchParams, null, null, DateTime.Now.AddMonths(-1), DateTime.Now);
            _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNumber;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNumber.Focus();
        }

        private void txtJobNumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNumber.Text))
            {
                Service_JOB_HDR OJobheader = CHNLSVC.CustService.GET_SCV_JOB_HDR(txtJobNumber.Text, BaseCls.GlbUserComCode);

                if (OJobheader == null || OJobheader.SJB_JOBNO == null)
                {
                    MessageBox.Show("Please enter correct job number", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNumber.Clear();
                    txtJobNumber.Focus();
                    return;
                }
                else
                {
                    btnSearch_Click(null, null);
                }

            }
        }

        private void txtJobNumber_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_req_Click(null, null);
        }

        private void txtJobNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_req_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {

                btnSearch.Focus();
            }
        }

        private void txtJobNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
