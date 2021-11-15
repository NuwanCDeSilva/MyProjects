using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_JobClose : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private Decimal gblJobStage = -1;
        private Int32 currentRow = -1;
        private String GblwarrStatus = string.Empty;
        private int[] GblarrJobLine;
        private bool mouseIsDown = false;
        private Point firstPoint;
        private Boolean _isbulkSer = false;
        private Service_Chanal_parameter _scvParam = null;

        List<Service_OldPartRemove> oOldPartsNew = null;

        public Int32 isActionTaken = 0; // 1-Close 2-Reopen

        private String StartDateLable = string.Empty;

        private Decimal editValueStockR = 0;
        private bool isWarehouseSearch = false;
        public bool IsHavingGitItems = false; //By akila 2017/05/08 - if git items available, service not allow to close

        public ServiceWIP_JobClose(string job, Int32 jobLine, Decimal stage, string stDate, String warrStatus, Boolean _isbulk, int[] _arrJobLine, bool _isHavingGit)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            gblJobStage = stage;
            StartDateLable = stDate;
            GblarrJobLine = _arrJobLine;
            GblwarrStatus = warrStatus;
            _isbulkSer = _isbulk;
            InitializeComponent();

            dgvNewParts.AutoGenerateColumns = false;
            dgvOldParts.AutoGenerateColumns = false;
            dgvPickItems.AutoGenerateColumns = false;

            pnlMain.Size = new Size(617, 259);
            pnlPickItems.Size = new Size(617, 260);

            IsHavingGitItems = _isHavingGit;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc:
                    {
                        if (isWarehouseSearch == true)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "S" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "O" + seperator);
                            break;
                        }

                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ServiceWIP_JobClose_Load(object sender, EventArgs e)
        {
            btnClear_Click(null, null);

            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)");
                this.Enabled = false;
                return;
            }
           



            loadDefaultLocation();
            btnView_Click(null, null);
            loadCloseTypes();
            pnlPickItems.Visible = false;

            if (gblJobStage == 6)
            {
                btnSave.Text = "Re Open";
            }
            else
            {
                btnSave.Text = "Close the job";
            }
        }

        private void txtNewPartWarehouse_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = true;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNewPartWarehouse;
                _CommonSearch.ShowDialog();
                txtNewPartWarehouse.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtNewPartWarehouse_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewPartWarehouse.Text))
            {
                DataTable dtLocations = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (dtLocations.Rows.Count > 0)
                {
                    if (dtLocations.Select("SLL_LOC = '" + txtNewPartWarehouse.Text.Trim() + "'").Length == 0)
                    {
                        MessageBox.Show("Please enter valid location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNewPartWarehouse.Clear();
                        txtNewPartWarehouse.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPartWarehouse.Clear();
                    txtNewPartWarehouse.Focus();
                    return;
                }
            }
        }

        private void txtNewPartWarehouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtNewPartWarehouse_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnNPReturn.Focus();
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isWarehouseSearch = false;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOPWarehouse;
                _CommonSearch.ShowDialog();
                txtOPWarehouse.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOPWarehouse.Text))
            {
                DataTable dtLocations = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (dtLocations.Rows.Count > 0)
                {
                    if (dtLocations.Select("SLL_LOC = '" + txtOPWarehouse.Text.Trim() + "'").Length == 0)
                    {
                        MessageBox.Show("Please enter valid location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOPWarehouse.Clear();
                        txtOPWarehouse.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                textBox1_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnOPReturn.Focus();
            }
        }

        private void dgvOldParts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvOldParts.IsCurrentCellDirty)
            {
                dgvOldParts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNewParts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvNewParts.IsCurrentCellDirty)
            {
                dgvNewParts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNewParts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvNewParts.CurrentCell.ColumnIndex == dgvNewParts.Columns["Qty"].Index)
            {
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }

                if (dgvNewParts.CurrentCell.ColumnIndex == dgvNewParts.Columns["Qty"].Index || dgvNewParts.CurrentCell.ColumnIndex == dgvNewParts.Columns["Remark"].Index)
                {
                    dgvNewParts.Rows[dgvNewParts.CurrentCell.RowIndex].Cells["select"].Value = true;
                }
            }
        }

        private void dgvOldParts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvOldParts.CurrentCell.ColumnIndex == dgvOldParts.Columns["SOP_OLDITMQTY"].Index)
            {
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }

                if (dgvOldParts.CurrentCell.ColumnIndex == dgvOldParts.Columns["SOP_OLDITMQTY"].Index || dgvOldParts.CurrentCell.ColumnIndex == dgvOldParts.Columns["Remark"].Index)
                {
                    dgvOldParts.Rows[dgvOldParts.CurrentCell.RowIndex].Cells["selectOP"].Value = true;
                }
            }
        }

        private void dgvNewParts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvOldParts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvNewParts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNewParts.CurrentCell.ColumnIndex == 6)
                {
                    if (dgvNewParts.CurrentCell != null && dgvNewParts.CurrentCell.Value != null)
                    {
                        decimal newValue = Convert.ToDecimal(dgvNewParts.CurrentCell.Value);
                        if (newValue > editValueStockR)
                        {
                            MessageBox.Show("Please enter valid qty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvNewParts.CurrentCell.Value = editValueStockR;
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvNewParts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dgvNewParts.CurrentCell.ColumnIndex == 6)
                {
                    if (dgvNewParts.CurrentCell != null && dgvNewParts.CurrentCell.Value != null)
                    {
                        editValueStockR = Convert.ToDecimal(dgvNewParts.CurrentCell.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvOldParts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvNewParts.CurrentCell.Value == null)
            //{
            //    return;
            //}
            //MessageBox.Show("E " + dgvNewParts.CurrentCell.Value.ToString());
        }

        private void dgvOldParts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (dgvNewParts.CurrentCell.Value == null)
            //{
            //    return;
            //}
            //MessageBox.Show("S " + dgvNewParts.CurrentCell.Value.ToString());

        }

        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
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

                if (string.IsNullOrEmpty(txtOPWarehouse.Text))
                {
                    MessageBox.Show("please select a warehouse", "Item Return", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOPWarehouse.Focus();
                    return;
                }
                btnOPReturn.Enabled = false;    //kapila 9/2/2016
                List<Tuple<Int32, String, String>> _ReturnItemList = new List<Tuple<Int32, String, String>>();

                if (isAnySelectedOldPart())
                {
                    List<Service_Job_Det_Sub> selectedITem = new List<Service_Job_Det_Sub>();

                    for (int i = 0; i < dgvOldParts.Rows.Count; i++)
                    {
                        //if (dgvOldParts.Rows[i].Cells["selectOP"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[i].Cells["selectOP"].Value) == true && dgvOldParts.Rows[i].Cells["SourceTable"].Value.ToString() != "SUB")
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
                    result = CHNLSVC.CustService.Update_Olppart_ReturnWarehouse(_ReturnItemList, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserSessionID, txtOPWarehouse.Text, selectedITem, out docNum, GblJobNum, GbljobLineNum);

                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\n Doc num : " + docNum, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnView_Click(null, null);

                        string _repname = string.Empty;
                        string _papersize = string.Empty;
                        BaseCls.GlbReportTp = "OUTWARDWIP";
                        CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);

                        if (!(_repname == null || _repname == ""))
                        {
                            //Sanjeewa
                            FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory _views = new FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            _views.GlbReportName = string.Empty;
                            _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : "Outward_Docs.rpt";
                            _views.GlbReportDoc = docNum;
                            _views.Show();
                            _views = null;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(docNum))
                        {
                            Cursor.Current = Cursors.Default;
                            if (docNum.Contains("DUPLICATE_SERIALS_FOUND"))
                            {
                                MessageBox.Show("Process Terminated! Duplicate serials available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {                                
                                    MessageBox.Show("Process Terminated! \n" + docNum, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Process Terminated.\n" + docNum, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            txtRemarkNewItem.Clear();
            getOldParts();
            getNewItems();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                 //Tharanga 2017?may/31
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters.SP_IS_SPCPER_JBOPN == 1)
            {
                btnSave.Enabled = false;
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10825))
                {
                    btnSave.Enabled = false;
                    MessageBox.Show("You don't have permission. Permission code : " + 10825, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }

            }
            else
            {
                btnSave.Enabled = true;
            }
            
            

                if (gblJobStage == 6)
                {
                    if (MessageBox.Show("Do you want to Re-Open the job?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    //DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
                    //if (dtTemp != null && dtTemp.Rows.Count > 0 && dtTemp.Select("MSS_DESC <> 'CANCEL'").ToDataTable().Rows.Count > 0)
                    //{
                    //    MessageBox.Show("Pending MRN available for this job item.", "Question", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}


                    int result1 = CHNLSVC.CustService.Update_Job_dates(GblJobNum, GbljobLineNum, DateTime.Now, DateTime.Now, dtpDate.Value, dtpDate.Value);
                    result1 = CHNLSVC.CustService.Update_JobDetailStage(GblJobNum, GbljobLineNum, 5);
                    //result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                    Service_Job_StageLog oLog = new Service_Job_StageLog();
                    oLog.SJL_REQNO = "";
                    oLog.SJL_JOBNO = GblJobNum;
                    oLog.SJL_JOBLINE = GbljobLineNum;
                    oLog.SJL_COM = BaseCls.GlbUserComCode;
                    oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                    oLog.SJL_JOBSTAGE = 5;
                    oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                    oLog.SJL_CRE_DT = DateTime.Now;
                    oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                    oLog.SJL_INFSUP = 0;
                    result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);



                    if (result1 > 0)
                    {
                        MessageBox.Show("Successfully job Re-Opened.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isActionTaken = 2;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    //By akila 2017/05/08 - if git items available, service not allow to close
                    if (IsHavingGitItems)
                    {
                         MessageBox.Show("Job cannot be closed when there are GIT available", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         Cursor = DefaultCursor;
                            return;
                    }

                    //Sanjeewa 2017-02-25
                    if (!(BaseCls.GlbUserComCode == "ABE" || BaseCls.GlbUserComCode == "SGL")) //Removed as per request 2017-03-10
                    {
                        DataTable _mrn = CHNLSVC.CustService.checkAppMRNforJob(GblJobNum);
                        string _mrnno = "";
                        if (_mrn.Rows.Count > 0)
                        {
                            for (int i = 0; i < _mrn.Rows.Count; i++)
                            {
                                _mrnno = _mrnno + " ";
                            }
                            MessageBox.Show("Pending Approved MRN(s) available. Please cancel the MRN(s) to close the job. " + _mrnno, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    //kapila 3/9/2015
                    _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                    if (_scvParam != null)
                        if (_scvParam.sp_need_act_def == 1)
                        {
                            DataTable _dtDef = CHNLSVC.General.get_job_defects(GblJobNum, "W");
                            if (_dtDef.Rows.Count == 0)
                            {
                                MessageBox.Show("Please enter the actual defects.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    if (MessageBox.Show("Do you want to close the job?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    string msg;
                    Boolean _isWarRep12 = false;
                    _isWarRep12 = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(GblJobNum);

                    if (_isWarRep12 == false) //Sanjeewa 2016-03-21
                    {
                        if (!CheckSupplierClaim(out msg))
                        {
                            MessageBox.Show(msg, "Job Close", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (_scvParam.SP_IS_RTN_OLDPT == 1 && !checkIsRetunAllSubItems())
                    {
                        //MessageBox.Show("Please return all old parts and accresories to close the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);

                    if (dtpDate.Value < oJOB_HDR.SJB_DT)
                    {
                        MessageBox.Show("Please select a valid date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Int32 pendingCount = 0;
                    if (_isbulkSer == false)
                    {
                        pendingCount = getPendingMRNCount(GblJobNum, GbljobLineNum);

                        if (pendingCount > 0)
                        {
                            MessageBox.Show("Pending MRN available for this job item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        foreach (int i in GblarrJobLine)
                        {

                            pendingCount = getPendingMRNCount(GblJobNum, i);

                            if (pendingCount > 0)
                            {
                                MessageBox.Show("Pending MRN available for this job item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }



                    string itmCode;
                    if (ddlCloseType.SelectedValue == "WRPL")
                    {
                        if (_isbulkSer == false)
                        {
                            if (!checkSuppWarranty(out itmCode))
                            {
                                MessageBox.Show("Please request for supplier warranty claim.\nItem Code : " + itmCode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {

                            if (!checkSuppWarrantyBulk(out itmCode))
                            {
                                MessageBox.Show("Please request for supplier warranty claim.\nItem Code : " + itmCode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }


                    }

                    int result1 = 0;

                    //kapila 27/2/2016 get pending acceptance stage is allowed or not
                    decimal _jbStage = 6;
                    DataTable _dtJobHdr = CHNLSVC.CustService.sp_get_job_hdrby_jobno(GblJobNum); //kapila 22/4/2016
                    //Comment Darshana --- as per sandun request......
                    //DataTable _dtPend = CHNLSVC.CustService.GetPendingAcceptanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dtJobHdr.Rows[0]["SJB_JOBCAT"].ToString());
                    //if (_dtPend.Rows.Count > 0)
                    //    if (Convert.ToDecimal(_dtPend.Rows[0]["scs_pend_accept"]) == 1)
                    //        _jbStage = Convert.ToDecimal(5.2);

                    if (_isbulkSer == false)
                    {
                        result1 = CHNLSVC.CustService.Update_Job_dates(GblJobNum, GbljobLineNum, DateTime.Now, DateTime.Now, Convert.ToDateTime(StartDateLable), dtpDate.Value);
                        result1 = CHNLSVC.CustService.Update_JobDetailStage(GblJobNum, GbljobLineNum, _jbStage);
                        result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                        Service_Job_StageLog oLog = new Service_Job_StageLog();
                        oLog.SJL_REQNO = "";
                        oLog.SJL_JOBNO = GblJobNum;
                        oLog.SJL_JOBLINE = GbljobLineNum;
                        oLog.SJL_COM = BaseCls.GlbUserComCode;
                        oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                        oLog.SJL_JOBSTAGE = _jbStage;
                        oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                        oLog.SJL_CRE_DT = DateTime.Now;
                        oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                        oLog.SJL_INFSUP = 0;
                        result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);

                    }

                    else
                    {

                        foreach (int i in GblarrJobLine)
                        {
                            result1 = CHNLSVC.CustService.Update_Job_dates(GblJobNum, i, DateTime.Now, DateTime.Now, Convert.ToDateTime(StartDateLable), dtpDate.Value);
                            result1 = CHNLSVC.CustService.Update_JobDetailStage(GblJobNum, i, _jbStage);
                            result1 = CHNLSVC.CustService.Update_ScvJobDetRemark(txtTechnicanRemark.Text, txtCusRemark.Text, GblJobNum, i, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlCloseType.SelectedValue.ToString(), txtTechClsRmkNew.Text);
                            Service_Job_StageLog oLog = new Service_Job_StageLog();
                            oLog.SJL_REQNO = "";
                            oLog.SJL_JOBNO = GblJobNum;
                            oLog.SJL_JOBLINE = i;
                            oLog.SJL_COM = BaseCls.GlbUserComCode;
                            oLog.SJL_LOC = BaseCls.GlbUserDefLoca;
                            oLog.SJL_JOBSTAGE = _jbStage;
                            oLog.SJL_CRE_BY = BaseCls.GlbUserID;
                            oLog.SJL_CRE_DT = DateTime.Now;
                            oLog.SJL_SESSION_ID = BaseCls.GlbUserSessionID;
                            oLog.SJL_INFSUP = 0;
                            result1 = CHNLSVC.CustService.Save_ServiceJobStageLog(oLog);
                        }
                    }






                    if (result1 > 0)
                    {
                        MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isActionTaken = 1;
                        this.Close();
                        //  return;

                        // Nadeeka Added 15-07-2015

                        //if (result1 == 1)
                        //{
                        //    string outMsg;
                        //    Service_JOB_HDR _jobHeader = new Service_JOB_HDR();
                        //    List<Service_job_Det> _scvItemList = null;
                        //    Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel,3);
                        //    Service_Message oMessage = new Service_Message();
                        //    _jobHeader = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);
                        //    _scvItemList = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);

                        //        if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                        //        {
                        //            //MasterItem oItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _scvItemList[0].Jbd_itm_cd);
                        //            //string emailBody = oTemplate.Sml_templ_mail;
                        //            //emailBody = emailBody.Replace("[B_Cust]", _jobHeader.SJB_B_CUST_NAME)
                        //            //                     .Replace("[Cust]", _jobHeader.SJB_CUST_NAME)
                        //            //                     .Replace("[jobNo]", GblJobNum)
                        //            //                     .Replace("[defect],[defect]", _jobHeader.SJB_B_CUST_NAME)
                        //            //                     .Replace("[date]", DateTime.Now.ToString("dd/MMM/yyyy  hh:mm tt"))
                        //            //                     .Replace("[tech]", " ")
                        //            //                     .Replace("[ItmModel]", oItem.Mi_model)
                        //            //                     .Replace("[ItmDescr]", oItem.Mi_longdesc)
                        //            //                     .Replace("[ItmSerial]", _scvItemList[0].Jbd_ser1)
                        //            //                     .Replace("[ItmCate]", oItem.Mi_cate_1);

                        //            String SmsBody = oTemplate.Sml_templ_sms;

                        //            SmsBody = SmsBody.Replace("[B_Cust]", _jobHeader.SJB_B_CUST_NAME)
                        //                             .Replace("[jobNo]", GblJobNum);


                        //            if (_scvParam.sp_cust_inform == 1)
                        //            {
                        //            DataTable _dt = CHNLSVC.CustService.GetCustSatisByChnl(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, 1);
                        //            string _ans1 = string.Empty;
                        //            string _ans2 = string.Empty;
                        //            string _ans3 = string.Empty;
                        //            string _ans4 = string.Empty;
                        //            string _ans5 = string.Empty;
                        //            if (_dt.Rows.Count > 0)
                        //            {
                        //                foreach (DataRow dr in _dt.Rows)
                        //                {

                        //                    SmsBody = SmsBody + "\n" +  dr["ssq_quest"].ToString();

                        //                    DataTable _dt1 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "1");

                        //                    foreach (DataRow dr1 in _dt1.Rows)
                        //                    {
                        //                        _ans1 = "1.) " + dr1["scst_desc"].ToString();
                        //                    }

                        //                    DataTable _dt2 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "2");

                        //                    foreach (DataRow dr2 in _dt2.Rows)
                        //                    {
                        //                        _ans2 = "2.) " + dr2["scst_desc"].ToString();
                        //                    }
                        //                    DataTable _dt3 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "3");

                        //                    foreach (DataRow dr3 in _dt3.Rows)
                        //                    {
                        //                        _ans3 = "3.) " + dr3["scst_desc"].ToString();
                        //                    }

                        //                    DataTable _dt4 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "4");

                        //                    foreach (DataRow dr4 in _dt4.Rows)
                        //                    {
                        //                        _ans4 = "4.) " + dr4["scst_desc"].ToString();
                        //                    }

                        //                    DataTable _dt5 = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _scvParam.SP_SERCHNL, "5");

                        //                    foreach (DataRow dr5 in _dt5.Rows)
                        //                    {
                        //                        _ans5 = "5.) " + dr5["scst_desc"].ToString();
                        //                    }
                        //                    SmsBody = SmsBody + "\n" + _ans1;
                        //                    SmsBody = SmsBody + "\n" + _ans2;
                        //                    SmsBody = SmsBody + "\n" + _ans3;
                        //                    SmsBody = SmsBody + "\n" + _ans4;
                        //                    SmsBody = SmsBody + "\n" + _ans5;
                        //                }

                        //                }
                        //            }





                        //            oMessage.Sm_com = _jobHeader.SJB_COM;
                        //            oMessage.Sm_jobno = GblJobNum;
                        //            oMessage.Sm_joboline = 1;
                        //            oMessage.Sm_jobstage = 2;
                        //            oMessage.Sm_ref_num = string.Empty;
                        //            oMessage.Sm_status = 0;
                        //            oMessage.Sm_msg_tmlt_id = 1;
                        //            oMessage.Sm_sms_text = SmsBody;
                        //            oMessage.Sm_sms_gap = 0;
                        //            oMessage.Sm_sms_done = 0;
                        //            oMessage.Sm_mail_text = String.Empty ;
                        //            oMessage.Sm_mail_gap = 0;
                        //            oMessage.Sm_email_done = 0;
                        //            oMessage.Sm_cre_by = BaseCls.GlbUserID;
                        //            oMessage.Sm_cre_dt = DateTime.Now;
                        //            oMessage.Sm_mod_by = BaseCls.GlbUserID;
                        //            oMessage.Sm_mod_dt = DateTime.Now;
                        //            Int32 R1 = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                        //            if (R1 < 1)
                        //            {
                        //                MessageBox.Show("Customer message send failed.\n" + outMsg, "Job Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            }
                        //        }



                        //  }




                    }
                    else
                    {
                        MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRemarkNewItem.Clear();
            lblBackDateInfor.Text = "";
            oOldPartsNew = new List<Service_OldPartRemove>();
            dgvOldParts.DataSource = oOldPartsNew;
            dgvNewParts.DataSource = new List<Service_stockReturn>();
            txtNewPartWarehouse.Clear();
            txtOPWarehouse.Clear();
            btnOPReturn.Enabled = true; //kapila 9/2/2016

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10824))
            {                
                dtpDate.Enabled = false;                
            }
            else
            {
                dtpDate.Enabled = true;
            }
            //bool _alwCurrentTrans = false;
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, "m_Trans_Service_WorkInProgress", dtpDate, lblBackDateInfor, string.Empty, out _alwCurrentTrans);

        }

        private void btnNPReturn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to return?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtNewPartWarehouse.Text))
                {
                    MessageBox.Show("please select a warehouse", "Item Return", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPartWarehouse.Focus();
                    return;
                }

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

                            if (item.SERIAL_ID == "N/A")
                            {
                                item.SERIAL_ID = String.Empty;
                            }
                            item.JOB_NO = GblJobNum;
                            item.JOB_LINE = GbljobLineNum;
                            item.Desc = txtRemarkNewItem.Text.Trim();

                            MasterItem oItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.ITEM_CODE);
                            if (oItem.Mi_is_ser1 == -1)
                            {
                                if (oNewItems.Count > 0 && oNewItems.FindAll(x => x.ITEM_CODE == item.ITEM_CODE && x.STATUS_CODE == item.STATUS_CODE).Count > 0)
                                {
                                    Service_stockReturn oOldRecord = oNewItems.Find(x => x.ITEM_CODE == item.ITEM_CODE && x.STATUS_CODE == item.STATUS_CODE);
                                    oOldRecord.QTY = oOldRecord.QTY + item.QTY;
                                }
                                else
                                {
                                    oNewItems.Add(item);
                                }
                            }
                            else
                            {
                                oNewItems.Add(item);
                            }
                        }
                    }

                    Int32 result = 0;
                    string docNum = string.Empty;
                    result = CHNLSVC.CustService.Update_NewItems_ReturnWarehouse(oNewItems, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserSessionID, txtNewPartWarehouse.Text, dtpDate.Value, out docNum);

                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\nDocument Numbers " + docNum, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnView_Click(null, null);
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void loadDefaultLocation()
        {
            DataTable dtLocations = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (dtLocations.Select("sll_def = 1 AND sll_tp = 'S'").Length > 0)
            {
                DataRow[] drTemp = dtLocations.Select("sll_def = 1  AND sll_tp = 'S'");
                txtNewPartWarehouse.Text = drTemp[0]["sll_loc"].ToString();
                txtOPWarehouse.Text = drTemp[0]["sll_loc"].ToString();
            }
            else if (dtLocations.Select("sll_tp = 'S'").Length > 0)
            {
                DataRow[] drTemp = dtLocations.Select("sll_tp = 'S'");
                txtNewPartWarehouse.Text = drTemp[0]["sll_loc"].ToString();
                txtOPWarehouse.Text = drTemp[0]["sll_loc"].ToString();
            }
            else
            {
                txtNewPartWarehouse.Text = BaseCls.GlbUserDefLoca;
                txtOPWarehouse.Text = BaseCls.GlbUserDefLoca;
            }
            if (dtLocations.Select("sll_tp = 'O'").Length > 0)
            {
                DataRow[] drTemp = dtLocations.Select("sll_tp = 'O'");
                txtOPWarehouse.Text = drTemp[0]["sll_loc"].ToString();
            }
        }

        private void getOldParts()
        {
            if (_isbulkSer == false)
            {
                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, string.Empty, string.Empty);
                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1 && x.SOP_RTN_WH == 0 || (x.SOP_REQWCN == 2 && x.SOP_RTN_WH == 0)));
                if (oldPartList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in oldPartList)
                    {
                        item.SourceTable = "OLD";
                    }

                    dgvOldParts.DataSource = oldPartList;
                    modifyGrid();
                }

                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, GbljobLineNum);
                if (oSubItems.Count > 0)
                {
                    foreach (Service_Job_Det_Sub item in oSubItems)
                    {
                        if (item.JBDS_RTN_WH == 1)
                        {
                            continue;
                        }
                        Service_OldPartRemove oldItem = new Service_OldPartRemove();
                        oldItem.SOP_SEQNO = item.JBDS_SEQ_NO;
                        oldItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                        oldItem.SOP_OLDITMSTUS_Text = item.JBDS_ITM_STUS_TEXT;
                        oldItem.SOP_OLDITMSTUS = item.JBDS_ITM_STUS;
                        oldItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                        oldItem.SOP_OLDITMQTY = item.JBDS_QTY;
                        oldItem.SOP_CRE_DT = item.JBDS_CRE_DT;
                        oldItem.SOP_RMK = item.JBDS_WARR_RMK;
                        oldItem.SOP_REQWCN = item.JBDS_LINE; // use when update the line
                        oldItem.SourceTable = "SUB";
                        oldPartList.Add(oldItem);
                    }
                    dgvOldParts.DataSource = null;
                    dgvOldParts.DataSource = oldPartList;
                    modifyGrid();
                }
            }

            else
            {
                List<Service_OldPartRemove> oldPartList1 = new List<Service_OldPartRemove>();
                List<Service_OldPartRemove> oldPartList = new List<Service_OldPartRemove>();
                List<Service_Job_Det_Sub> oSubItems = new List<Service_Job_Det_Sub>();
                foreach (int i in GblarrJobLine)
                {
                    List<Service_OldPartRemove> oldPartList1tmp = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, i, string.Empty, string.Empty);
                    List<Service_OldPartRemove> oldPartListtmp = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1 || x.SOP_REQWCN == 2));
                    oldPartList1.AddRange(oldPartList1tmp);
                    oldPartList.AddRange(oldPartListtmp);
                    List<Service_Job_Det_Sub> oSubItemstemp = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, i);
                    oSubItems.AddRange(oSubItemstemp);
                }


                if (oldPartList.Count > 0)
                {
                    foreach (Service_OldPartRemove item in oldPartList)
                    {
                        item.SourceTable = "OLD";
                    }

                    dgvOldParts.DataSource = oldPartList;
                    modifyGrid();
                }

                //   List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, i);
                if (oSubItems.Count > 0)
                {
                    foreach (Service_Job_Det_Sub item in oSubItems)
                    {
                        if (item.JBDS_RTN_WH == 1)
                        {
                            continue;
                        }
                        Service_OldPartRemove oldItem = new Service_OldPartRemove();
                        oldItem.SOP_SEQNO = item.JBDS_SEQ_NO;
                        oldItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                        oldItem.SOP_OLDITMSTUS_Text = item.JBDS_ITM_STUS_TEXT;
                        oldItem.SOP_OLDITMSTUS = item.JBDS_ITM_STUS;
                        oldItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                        oldItem.SOP_OLDITMQTY = item.JBDS_QTY;
                        oldItem.SOP_CRE_DT = item.JBDS_CRE_DT;
                        oldItem.SOP_RMK = item.JBDS_WARR_RMK;
                        oldItem.SOP_REQWCN = item.JBDS_LINE; // use when update the line
                        oldItem.SourceTable = "SUB";
                        oldPartList.Add(oldItem);
                    }
                    dgvOldParts.DataSource = null;
                    dgvOldParts.DataSource = oldPartList;
                    modifyGrid();
                }

            }
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

        private void getNewItems()
        {
            if (_isbulkSer == false)
            {
                List<Service_stockReturn> oNewItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, string.Empty, BaseCls.GlbUserDefLoca);
                dgvNewParts.DataSource = new List<Service_stockReturn>();

                List<Service_Job_Det_Sub> oSubItems = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, GbljobLineNum);

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    oNewItems.RemoveAll(x => x.SERIAL_ID == item.JBDS_REPL_SERID.ToString());
                }

                dgvNewParts.DataSource = oNewItems;

                modifyGrid();
            }

            else
            {
                List<Service_stockReturn> oNewItems = new List<Service_stockReturn>();
                List<Service_Job_Det_Sub> oSubItems = new List<Service_Job_Det_Sub>();
                dgvNewParts.DataSource = new List<Service_stockReturn>();
                foreach (int i in GblarrJobLine)
                {
                    List<Service_stockReturn> oNewItemstemp = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, i, string.Empty, BaseCls.GlbUserDefLoca);
                    oNewItems.AddRange(oNewItemstemp);

                    List<Service_Job_Det_Sub> oSubItemstemp = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, i);
                    oSubItems.AddRange(oSubItemstemp);
                }

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    oNewItems.RemoveAll(x => x.SERIAL_ID == item.JBDS_REPL_SERID.ToString());
                }

                dgvNewParts.DataSource = oNewItems;

                modifyGrid();


            }
        }

        private void loadCloseTypes()
        {
            List<Service_Close_Type> oCloseType = CHNLSVC.CustService.GetServiceCloseType(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
            ddlCloseType.DisplayMember = "SCT_DESC";
            ddlCloseType.ValueMember = "SCT_TP";
            ddlCloseType.DataSource = oCloseType;

            if (oCloseType.FindAll(x => x.SCT_TP == "CMP").Count > 0)
            {
                ddlCloseType.SelectedValue = "CMP";
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

        private void label24_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label24_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlPickItems.Location.X - xDiff;
                int y = pnlPickItems.Location.Y - yDiff;
                pnlPickItems.Location = new Point(x, y);
            }
        }

        private void label24_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlPickItems.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string SelectedItemCode = string.Empty;
            string SelectedSerial = string.Empty;
            decimal SelectedQty = 0;

            for (int i = 0; i < dgvPickItems.Rows.Count; i++)
            {
                if (dgvPickItems.Rows[i].Cells["selectI"].Value != null && Convert.ToBoolean(dgvPickItems.Rows[i].Cells["selectI"].Value) == true)
                {
                    SelectedItemCode = dgvPickItems.Rows[i].Cells["ITEM_CODEI"].Value.ToString();
                    SelectedSerial = dgvPickItems.Rows[i].Cells["SERIAL_IDI"].Value.ToString();
                    SelectedQty = Convert.ToDecimal(dgvPickItems.Rows[i].Cells["QtyI"].Value.ToString());
                }
            }

            dgvOldParts.Rows[currentRow].Cells["NewItems"].Value = SelectedItemCode;
            dgvOldParts.Rows[currentRow].Cells["NewSerial"].Value = SelectedSerial;
            pnlPickItems.Visible = false;
        }

        private void dgvPickItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                for (int i = 0; i < dgvPickItems.Rows.Count; i++)
                {
                    dgvPickItems.Rows[i].Cells["selectI"].Value = false;
                }
                dgvPickItems.Rows[e.RowIndex].Cells["selectI"].Value = true;
            }
        }

        private void dgvOldParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == dgvOldParts.Columns["NewReplace"].Index)
                {
                    if (dgvOldParts.Rows[e.RowIndex].Cells["NewReplace"].Value != null && Convert.ToBoolean(dgvOldParts.Rows[e.RowIndex].Cells["NewReplace"].Value) == true)
                    {
                        currentRow = e.RowIndex;
                        List<Service_stockReturn> oNewItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, string.Empty, BaseCls.GlbUserDefLoca);
                        dgvPickItems.DataSource = new List<Service_stockReturn>();
                        dgvPickItems.DataSource = oNewItems;
                        pnlPickItems.Show();
                        dgvOldParts.Rows[e.RowIndex].Cells["NewReplace"].Value = true;
                    }
                }
            }
        }

        private void dgvPickItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPickItems.IsCurrentCellDirty)
            {
                dgvPickItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
        }

        private void btnVouNo_Click_1(object sender, EventArgs e)
        {
            txtNewPartWarehouse_DoubleClick(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_DoubleClick(null, null);
        }

        private bool checkSuppWarranty(out string itmCode)
        {
            bool status = true;
            itmCode = string.Empty;

            List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            if (ojob_Det != null && ojob_Det.Count > 0)
            {
                Service_job_Det oJobDetail = ojob_Det[0];
                DateTime dtWarrDate;
                Int32 Period;

                if (oJobDetail.Jbd_swarrstartdt != null && oJobDetail.Jbd_swarrstartdt.Date != Convert.ToDateTime("01-Jan-1900").Date && oJobDetail.Jbd_swarrstartdt != DateTime.MinValue)
                {
                    dtWarrDate = oJobDetail.Jbd_swarrstartdt;
                    Period = oJobDetail.Jbd_swarrperiod;
                }
                else
                {
                    dtWarrDate = oJobDetail.Jbd_warrstartdt;
                    Period = oJobDetail.Jbd_warrperiod;
                }

                if (dtWarrDate.AddMonths(Period).Date >= DateTime.Now.Date)
                {
                    List<Service_supp_claim_itm> oSuppClaimItems = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, oJobDetail.Jbd_supp_cd, null);
                    if (oSuppClaimItems != null && oSuppClaimItems.Count > 0)
                    {
                        if (oSuppClaimItems.FindAll(x => x.SSC_BRND == oJobDetail.Jbd_brand && x.SSC_SUPP == oJobDetail.Jbd_supp_cd).Count > 0)
                        {
                            if (dgvOldParts.Rows.Count > 0)
                            {
                                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, string.Empty, string.Empty);
                                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1));

                                foreach (Service_OldPartRemove oOldPartRemove in oldPartList)
                                {
                                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, oOldPartRemove.SOP_OLDITMCD);
                                    if (oOldPartRemove.SOP_REQUESTNO == "0")
                                    {
                                        status = false;
                                        itmCode = oOldPartRemove.SOP_OLDITMCD;
                                    }
                                }
                            }
                            else
                            {
                                if (oJobDetail.Jbd_reqwcn == 0)
                                {
                                    itmCode = oJobDetail.Jbd_itm_cd;
                                    status = false;
                                }
                            }
                        }
                    }
                }
            }

            return status;
        }

        private bool checkSuppWarrantyBulk(out string itmCode)
        {
            bool status = true;
            itmCode = string.Empty;
            foreach (int i in GblarrJobLine)
            {
                List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(GblJobNum, i, BaseCls.GlbUserComCode);
                if (ojob_Det != null && ojob_Det.Count > 0)
                {
                    Service_job_Det oJobDetail = ojob_Det[0];
                    DateTime dtWarrDate;
                    Int32 Period;

                    if (oJobDetail.Jbd_swarrstartdt != null && oJobDetail.Jbd_swarrstartdt.Date != Convert.ToDateTime("01-Jan-1900").Date && oJobDetail.Jbd_swarrstartdt != DateTime.MinValue)
                    {
                        dtWarrDate = oJobDetail.Jbd_swarrstartdt;
                        Period = oJobDetail.Jbd_swarrperiod;
                    }
                    else
                    {
                        dtWarrDate = oJobDetail.Jbd_warrstartdt;
                        Period = oJobDetail.Jbd_warrperiod;
                    }

                    if (dtWarrDate.AddMonths(Period).Date >= DateTime.Now.Date)
                    {
                        List<Service_supp_claim_itm> oSuppClaimItems = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, oJobDetail.Jbd_supp_cd, null);
                        if (oSuppClaimItems != null && oSuppClaimItems.Count > 0)
                        {
                            if (oSuppClaimItems.FindAll(x => x.SSC_BRND == oJobDetail.Jbd_brand && x.SSC_SUPP == oJobDetail.Jbd_supp_cd).Count > 0)
                            {
                                if (dgvOldParts.Rows.Count > 0)
                                {
                                    List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, i, string.Empty, string.Empty);
                                    List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => (x.SOP_REQWCN == 0 && x.SOP_RTN_WH == 0) || (x.SOP_REQWCN == 1 && x.SOP_RECWNC == 1));

                                    foreach (Service_OldPartRemove oOldPartRemove in oldPartList)
                                    {
                                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, oOldPartRemove.SOP_OLDITMCD);
                                        if (oOldPartRemove.SOP_REQUESTNO == "0")
                                        {
                                            status = false;
                                            itmCode = oOldPartRemove.SOP_OLDITMCD;

                                        }
                                    }
                                }
                                else
                                {
                                    if (oJobDetail.Jbd_reqwcn == 0)
                                    {
                                        itmCode = oJobDetail.Jbd_itm_cd;
                                        status = false;

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return status;
        }

        private bool checkIsRetunAllSubItems()
        {
            bool status = true;

            if (GblwarrStatus.ToUpper() == "UNDER WARRANTY")
            {
                List<Service_stockReturn> oNewItemsFromGrd = (List<Service_stockReturn>)dgvNewParts.DataSource;

                List<Service_OldPartRemove> oldPartListFromGrd = (List<Service_OldPartRemove>)dgvOldParts.DataSource;

                foreach (Service_stockReturn oNewItem in oNewItemsFromGrd)
                {
                    MasterItem oItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, oNewItem.ITEM_CODE);
                    oNewItem.Desc = oItem.Mi_itm_tp;
                }


                List<Service_OldPartRemove> oldPartList1 = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, string.Empty, string.Empty);
                List<Service_OldPartRemove> oldPartList = oldPartList1.FindAll(x => x.SOP_RTN_WH == 1);
                List<Service_Job_Det_Sub> oSubItems1 = CHNLSVC.CustService.GetServiceJobDetailSubItems(GblJobNum, GbljobLineNum);
                List<Service_Job_Det_Sub> oSubItems = oSubItems1.FindAll(x => x.JBDS_RTN_WH == 1);
                foreach (Service_OldPartRemove item in oldPartList1)
                {
                    MasterItem oItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.SOP_OLDITMCD);
                    item.SOP_TP_text = oItem.Mi_itm_tp;
                }

                foreach (Service_Job_Det_Sub item in oSubItems)
                {
                    Service_OldPartRemove oNewItem = new Service_OldPartRemove();
                    oNewItem.SOP_OLDITMCD = item.JBDS_ITM_CD;
                    oNewItem.SOP_OLDITMSER1 = item.JBDS_SER1;
                    oNewItem.SOP_TP_text = item.JBDS_ITM_TP;
                    oldPartList.Add(oNewItem);
                }

                List<String> oItemTypes = oNewItemsFromGrd.Select(x => x.Desc).ToList();

                foreach (String itemType in oItemTypes)
                {

                    if (oNewItemsFromGrd.Count(x => x.Desc == itemType) > oldPartList.Count(x => x.SOP_TP_text == itemType))
                    {
                        string TypeText = string.Empty;
                        if (itemType == "M")
                        {
                            TypeText = "Main";
                        }
                        else if (itemType == "A")
                        {
                            TypeText = "Accessory";
                        }
                        else
                        {
                            TypeText = "other";
                        }

                        MessageBox.Show("Please return '" + TypeText + "' type item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        status = false;
                    }
                }

                //if (dgvNewParts.Rows.Count > 0)
                //{
                //    if (dgvOldParts.Rows.Count > 0)
                //    {
                //        status = false;
                //    }
                //}
            }

            return status;
        }

        private bool CheckSupplierClaim(out string msg)
        {
            bool status = true;
            msg = string.Empty;

            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, "", BaseCls.GlbUserDefLoca);

            if (stockReturnItems != null && stockReturnItems.Count > 0)
            {
                status = true;
                return status;
            }

            List<Service_job_Det> oJobDets = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            if (oJobDets != null && oJobDets.Count > 0)
            {
                Service_job_Det oJobDet = oJobDets[0];
                if (oJobDet.Jbd_reqwcn == 1 && oJobDet.Jbd_recwcn == 0 && oJobDet.Jbd_sentwcn == 1)
                {
                    msg = oJobDet.Jbd_itm_cd + " is sent to supplier claim.";
                    status = false;
                    return status;
                }

                List<Service_OldPartRemove> oOldparts = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, string.Empty, string.Empty);
                if (oOldparts != null && oOldparts.Count > 0)
                {
                    if (oOldparts.FindAll(x => x.SOP_REQWCN == 1 && x.SOP_RECWNC == 0).Count > 0)
                    {
                        string items = string.Empty;
                        List<Service_OldPartRemove> oOldpartsNew = oOldparts.FindAll(x => x.SOP_REQWCN == 1 && x.SOP_RECWNC == 0);
                        foreach (Service_OldPartRemove item in oOldpartsNew)
                        {
                            items += item.SOP_OLDITMCD + ", ";
                        }

                        msg = "Old parts send to supplier claim. Items : " + items;
                        status = false;
                        return status;
                    }
                }
            }

            return status;
        }
    }
}
