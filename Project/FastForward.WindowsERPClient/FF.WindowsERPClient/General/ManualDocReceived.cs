using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Diagnostics;

namespace FF.WindowsERPClient.General
{
    public partial class ManualDocReceived : Base
    {
        //sp_get_manual_docs_ByRef  NEW  in FMS
        //sp_get_transferd_man_docs  NEW IN FMS
        //sp_getreqbytype  = UPDATE
        //sp_getpendingreqbytype =UPDATE
        List<RequestApprovalHeader> all_requests = new List<RequestApprovalHeader>();//all requests of the profit center
        public ManualDocReceived()
        {
            try
            {
                InitializeComponent();
                LoadRefDetails(0);
                //LoadRefDetails(2);
                txtRecDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                load_Pending_Requests();
                load_userloc_allRequests();
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.INVMAN, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
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
        private void load_userloc_allRequests()
        {
            all_requests = CHNLSVC.Sales.getReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT011", DateTime.MinValue.Date, DateTime.MaxValue.Date);
            if (all_requests == null)
            {
                return;
            }
            foreach (RequestApprovalHeader rah in all_requests)
            {
                if (rah.Grah_app_stus == "A")
                {
                    //app_stus
                    rah.Grah_app_stus = "APPROVED";

                }
                else if (rah.Grah_app_stus == "P")
                {
                    //app_stus
                    rah.Grah_app_stus = "PENDING";
                    rah.Grah_app_by = "N/A";
                }
                else if (rah.Grah_app_stus == "R")
                {
                    //app_stus
                    rah.Grah_app_stus = "REJECTED";
                }
                else if (rah.Grah_app_stus == "C")
                {
                    //app_stus
                    rah.Grah_app_stus = "CANCELLED";
                    rah.Grah_app_by = "N/A";
                }
                else if (rah.Grah_app_stus == "F")
                {
                    //app_stus
                    rah.Grah_app_stus = "FINISHED";
                }
            }
            grvAllRequests.AutoGenerateColumns = false;
            grvAllRequests.DataSource = all_requests;

        }
        private void load_Pending_Requests()
        {
            //Pending list.
            grvPendingRegReq.DataSource = null;
            grvPendingRegReq.AutoGenerateColumns = false;

            grvApprovePendingReq.DataSource = null;
            grvApprovePendingReq.AutoGenerateColumns = false;

            grvReqbooks.DataSource = null;
            grvReqbooks.AutoGenerateColumns = false;

            List<RequestApprovalHeader> p_lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT011", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehRegRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT020.ToString(), 0, GlbReqUserPermissionLevel);
            if (p_lst == null)
            {
                p_lst = new List<RequestApprovalHeader>();
            }
            if (p_lst != null)
            {
                var _duplicate = from _dup in p_lst
                                 where _dup.Grah_app_stus == "P"
                                 select _dup;
                if (_duplicate.Count() > 0)
                {
                    p_lst = _duplicate.ToList();
                    grvPendingRegReq.DataSource = null;
                    grvPendingRegReq.AutoGenerateColumns = false;
                    grvPendingRegReq.DataSource = p_lst;
                }
            }
        }
        private void LoadRefDetails(Int16 IsTrans)
        {
            ddlRef.Items.Clear();
            // ddlRef.Items.Add(new ListItem("--Select Ref No--", "-1"));
            DataTable _tbl = CHNLSVC.Inventory.GetManualDocs(BaseCls.GlbUserDefLoca, IsTrans);

            if (_tbl != null)
            {
                if (_tbl.Rows.Count > 0)
                {
                    DataRow dr = _tbl.NewRow();
                    _tbl.Rows.Add(dr);
                    dr["MDD_REF"] = "---SELECT---";
                }
            }
            ddlRef.DataSource = _tbl;
            ddlRef.DisplayMember = "MDD_REF";
            ddlRef.ValueMember = "MDD_REF";
            ddlRef.SelectedValue = "---SELECT---";
            //ddlRef.DataTextField = "MDD_REF";
            //ddlRef.DataValueField = "MDD_REF";           
        }

        private void ddlRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //save to temporary table
                string _DocStatus = string.Empty;
                List<RequestApprovalHeader> approvedHeaders = null;
                if (chkTrans.Checked == true)
                {
                    _DocStatus = "F";
                }
                else
                {
                    _DocStatus = "P";
                }
                Int16 _userSeqNo = CHNLSVC.Inventory.SavePickedManualDocDetail(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _DocStatus);
                //***********
                //if (_userSeqNo < 1 && chkTrans.Checked == false)
                //{
                //    _DocStatus = "T";
                //    _userSeqNo = CHNLSVC.Inventory.SavePickedManualDocDetail_TRNS(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _DocStatus);
                //}
                //*************
                LoadManualDocDetails();

                DataTable dt = CHNLSVC.Inventory.GetManualDocsGet_manual_docs_ByRef(BaseCls.GlbUserComCode, ddlRef.SelectedValue.ToString());
                if (dt != null)
                {
                    try
                    {
                        string issueloc = dt.Rows[0]["MDD_ISSUE_LOC"].ToString();
                        DateTime issuedt = Convert.ToDateTime(dt.Rows[0]["MDD_RECEIVE_DT"].ToString());
                        txtDate.Value = issuedt;
                        txtIssLoc.Text = issueloc;
                    }
                    catch (Exception ex)
                    {
                        txtIssLoc.Text = "";
                    }
                }
                gvManualDocDet.Focus();
                //----------------------------------------------------------------------------------------------------------------------------------------------
                if (ddlRef.SelectedValue.ToString() == "---SELECT---")
                {
                    return;
                }
                if (gvManualDocDet.Rows.Count < 1)
                {
                    return;
                }
                //***************added 28-03-2013****************************
                if (chkTrans.Checked == false)
                {
                    return;
                }
                #region    CHECK- WHETHER ONLY THE APPROVED ITEMS ARE GOING TO BE TRANSFERED
                List<RequestApprovalHeader> approved = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRef.SelectedValue.ToString(), HirePurchasModuleApprovalCode.ARQT011.ToString(), 0, GlbReqUserPermissionLevel);
                if (approved == null)
                {
                    return;
                }
                List<RequestApprovalSerials> ApprSerList = new List<RequestApprovalSerials>();
                foreach (RequestApprovalHeader HDR in approved)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    string appRequestNo = HDR.Grad_ref.ToString();

                    DataTable dt_ = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, appRequestNo, out List);
                    ApprSerList.AddRange(List);
                }
                Int32 REQ_SERIAL_COUNT = ApprSerList.Count;//ONLY ONE PENDING REQUEST CAN BE HELD FOR A PERTICULAR DOCUMENT-REF
                Int32 CURRENTLY_SELECTED_COUNT = gvManualDocDet.Rows.Count;
                if (REQ_SERIAL_COUNT < 1)
                {
                    return; //no approved are sent 
                }
                if (approved != null)
                {
                    foreach (DataGridViewRow dgrv_row in gvManualDocDet.Rows)
                    {
                        //if (dgrv_row.Cells["MDD_PREFIX"].ToString() == APP_ser.Gras_anal5 && dgrv_row.Cells["MDD_BK_NO"].ToString() == APP_ser.Gras_anal6.ToString())
                        string prifix = dgrv_row.Cells["MDD_PREFIX"].Value.ToString();
                        string bookNo = dgrv_row.Cells["MDD_BK_NO"].Value.ToString();

                        // Decimal book_no = Convert.ToInt32(dgrv_row.Cells["MDD_BK_NO"].ToString() == null ? "-1" : dgrv_row.Cells["MDD_BK_NO"].ToString());
                        string document = ddlRef.SelectedValue.ToString();
                        Int32 COUNT = 0;
                        try
                        {
                            var _count = from _dup in ApprSerList
                                         where _dup.Gras_anal5 == prifix && _dup.Gras_anal6 == Convert.ToInt32(bookNo) && _dup.Gras_anal3 == document
                                         select _dup;
                            COUNT = _count.Count();
                            if (COUNT < 1)
                            {
                                //TODO: REMOVE FROM GRID AND TEMP TABLE

                                Int32 eff = CHNLSVC.Inventory.Delete_Selected_Item_Line(Convert.ToInt32(bookNo), prifix, BaseCls.GlbUserID);


                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    LoadManualDocDetails();
                }
                //}
                #endregion
                //********************************************************       
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
        private void LoadManualDocDetails()
        {
            gvManualDocDet.DataSource = null;
            gvManualDocDet.AutoGenerateColumns = false;
            gvManualDocDet.DataSource = CHNLSVC.Inventory.GetManualDocDet(BaseCls.GlbUserID);
            //gvManualDocDet.DataBind();
            // pnlManualDocDet.Update();

        }

        private void gvManualDocDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    //----------------ADD 28-03-2013-----------------------------
                    if (chkTrans.Checked == true)
                    {
                        //string document = ddlRef.SelectedValue.ToString();
                        //MessageBox.Show("Not allowd to delete!","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        //return;
                    }

                    //-----------------------------------------------------------

                    if (MessageBox.Show("Are you sure to Delete?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    Int32 rowIndex = e.RowIndex;
                    string _Bkno = gvManualDocDet["MDD_BK_NO", rowIndex].Value.ToString();
                    string _prefix = gvManualDocDet["MDD_PREFIX", rowIndex].Value.ToString();
                    string _user = gvManualDocDet["MDD_USER", rowIndex].Value.ToString();

                    Int32 eff = CHNLSVC.Inventory.Delete_Selected_Item_Line(Convert.ToInt32(_Bkno), _prefix, _user);

                    LoadManualDocDetails();
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

        private void SaveManualDocument()
        {
            try
            {
                if (ddlRef.SelectedValue.ToString() == "---SELECT---")
                {
                    //throw new UIValidationException("Please select reference no");
                    MessageBox.Show("Please select reference no");
                    return;
                }
                if (chkTrans.Checked == true)
                {
                    //throw new UIValidationException("Cannot confirm. This is already confirmed");
                    MessageBox.Show("Cannot confirm. This is already confirmed");
                    return;
                }
                string _AdjNumber = string.Empty;
                Int32 rows_inserted = 0;
                Int32 _userSeqNo = 0;

                //InventoryHeader _invHeader = new InventoryHeader();
                //DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(GlbUserComCode, GlbUserDefLoca);
                //foreach (DataRow r in dt_location.Rows)
                //{
                //    // Get the value of the wanted column and cast it to string
                //    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                //    if (System.DBNull.Value != r["ML_CATE_2"])
                //    {
                //        _invHeader.Ith_channel = (string)r["ML_CATE_2"];
                //    }
                //    else
                //    {
                //        _invHeader.Ith_channel = string.Empty;
                //    }
                //}

                //_invHeader.Ith_com = GlbUserComCode;
                //_invHeader.Ith_loc = GlbUserDefLoca;
                //DateTime _docDate = Convert.ToDateTime(txtRecDate.Text);
                //_invHeader.Ith_doc_date = _docDate;
                //_invHeader.Ith_doc_year = _docDate.Year;
                //_invHeader.Ith_direct = true;
                //_invHeader.Ith_doc_tp = "ADJ";
                //_invHeader.Ith_cate_tp = "NOR";
                //_invHeader.Ith_sub_tp = "NOR";
                //_invHeader.Ith_bus_entity = "";
                //_invHeader.Ith_is_manual = false;
                //_invHeader.Ith_manual_ref = "";
                ////_invHeader.Ith_remarks = txtRemarks.Text;
                //_invHeader.Ith_stus = "A";
                //_invHeader.Ith_cre_by = GlbUserName;
                //_invHeader.Ith_cre_when = DateTime.Now;
                //_invHeader.Ith_mod_by = GlbUserName;
                //_invHeader.Ith_mod_when = DateTime.Now;
                //_invHeader.Ith_session_id = GlbUserSessionID;
                //_invHeader.Ith_oth_docno = "";

                //MasterAutoNumber _masterAuto = new MasterAutoNumber();
                //_masterAuto.Aut_cate_cd = GlbUserDefLoca;
                //_masterAuto.Aut_cate_tp = "LOC";
                //_masterAuto.Aut_direction = null;
                //_masterAuto.Aut_modify_dt = null;
                //_masterAuto.Aut_moduleid = "ADJ";
                //_masterAuto.Aut_number = 0;
                //_masterAuto.Aut_start_char = "ADJ";
                //_masterAuto.Aut_year = null;

                _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", 1, BaseCls.GlbUserComCode);

                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

                //GET manual document serial list
                int Z = CHNLSVC.Inventory.GetManualDocSerialList(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID, _userSeqNo, _defBin, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

                //rows_inserted = CHNLSVC.Inventory.SaveManualDocReceipt(_invHeader, _reptPickSerialList, null, _masterAuto, out _AdjNumber);

                //Int16 rws = CHNLSVC.Inventory.SaveManualDocPages(_reptPickSerialList, ddlRef.SelectedValue);
                int rws = CHNLSVC.Inventory.SaveManualDocPages(BaseCls.GlbUserComCode, _userSeqNo, ddlRef.SelectedValue.ToString());

                //CHNLSVC.Inventory.UpdateManualDocs(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID);
                CHNLSVC.Inventory.UpdateManualDocs_NEW(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID, BaseCls.GlbUserDefLoca);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Manual Document Receive Receipt " + _AdjNumber + " Sucessfully saved.");
                MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ClearData();
                this.btnClear_Click(null, null);
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

        private void ClearData()
        {
            LoadRefDetails(0);
            chkTrans.Checked = false;
            txtTransLoc.Text = "";

            DataTable dt = new DataTable();
            gvManualDocDet.DataSource = null;
            gvManualDocDet.AutoGenerateColumns = false;
            gvManualDocDet.DataSource = dt;
            //pnlFooter.Update();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    ddlRef.SelectedValue.ToString();
                }
                catch (Exception EX)
                {
                    return;
                }
                if (MessageBox.Show("Are you sure to Confirm?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                btnConfirm.Enabled = false;
                SaveManualDocument();
                btnConfirm.Enabled = true;
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
        protected void GetUserAppLevel(HirePurchasModuleApprovalCode CD)
        {
            RequestApprovalCycleDefinition(false, CD, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        }
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                //GetUserAppLevel(HirePurchasModuleApprovalCode.ARQT011);
                GetUserAppLevel(HirePurchasModuleApprovalCode.INVMAN);
                try
                {
                    ddlRef.SelectedValue.ToString();
                }
                catch (Exception EX)
                {
                    return;
                }
                if (txtTransLoc.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Transfer location!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTransLoc.Focus();
                    return;
                }
                else
                {
                    txtTransLoc.Text = txtTransLoc.Text.Trim().ToUpper();
                }

                //if (MessageBox.Show("Are you sure to Transfer?", "Confirm Transfer", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                foreach (DataGridViewRow grow in gvManualDocDet.Rows)
                {

                    //if (grow.Cells["MDD_ITM_CD"].Value.ToString() == "HPRM" || grow.Cells["MDD_ITM_CD"].Value.ToString() == "HPRS")
                    //{
                    List<RequestApprovalHeader> approvedHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRef.SelectedValue.ToString(), HirePurchasModuleApprovalCode.ARQT011.ToString(), 1, 0);
                    Int32 COUNT = 0;
                    if (approvedHeaders != null)
                    {
                        var _count = from _dup in approvedHeaders
                                     where _dup.Grah_fuc_cd == ddlRef.SelectedValue.ToString()
                                     select _dup;
                        COUNT = _count.Count();
                    }
                    //var _count = from _dup in approvedHeaders
                    //             where _dup.Grah_fuc_cd == ddlRef.SelectedValue.ToString()
                    //             select _dup;
                    if (COUNT == 0)
                    {
                        if (MessageBox.Show("Cannot Transfer without Approval.\nDo you want to send a Transfer Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {

                            #region
                            //----------added 28-03-2013
                            List<RequestApprovalHeader> pendingHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRef.SelectedValue.ToString(), HirePurchasModuleApprovalCode.ARQT011.ToString(), 0, 100);
                            Int32 COUNT_2 = 0;
                            if (pendingHeaders != null)
                            {
                                var _count = from _dup in pendingHeaders
                                             where _dup.Grah_fuc_cd == ddlRef.SelectedValue.ToString()
                                             select _dup;
                                COUNT_2 = _count.Count();
                                if (COUNT_2 > 0)
                                {
                                    MessageBox.Show("Pending request exists already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //----------added 28-03-2013
                            #endregion

                            #region fill RequestApprovalHeader

                            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                            ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                            ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                            ra_hdr.Grah_app_stus = "P";
                            ra_hdr.Grah_app_tp = "ARQT011";
                            ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                            ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                            ra_hdr.Grah_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            ra_hdr.Grah_fuc_cd = ddlRef.SelectedValue.ToString();//lblAccNo.Text;
                            ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;

                            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                            ra_hdr.Grah_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;
                            //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                            //{
                            //    ra_hdr.Grah_oth_loc = "1";
                            //}
                            //else
                            //{ 
                            ra_hdr.Grah_oth_loc = txtTransLoc.Text.Trim().ToUpper();//"0";
                            //}
                            //if (ddlPendinReqNo.SelectedValue.ToString() == "New Request" || ddlPendinReqNo.SelectedValue.ToString() == string.Empty)
                            //if (ddlPendinReqNo.SelectedValue == null)
                            //{
                            //ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                            //}
                            //else if (ddlPendinReqNo.SelectedValue.ToString() == "New Request")
                            //{
                            //   ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                            //}
                            //else
                            // {
                            //   ra_hdr.Grah_ref = ddlPendinReqNo.SelectedValue.ToString();
                            //}
                            ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                            ra_hdr.Grah_remaks = "MAN DOC TRANSFER";

                            #endregion

                            #region fill List<RequestApprovalDetail>
                            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                            RequestApprovalDetail ra_det = new RequestApprovalDetail();
                            ra_det.Grad_ref = ra_hdr.Grah_ref;
                            ra_det.Grad_line = 1;
                            ra_det.Grad_req_param = "MAN_DOC";
                            // ra_det.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                            ra_det.Grad_is_rt1 = false;
                            // ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                            //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                            ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                            ra_det_List.Add(ra_det);
                            #endregion

                            #region fill RequestApprovalHeaderLog

                            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                            ra_hdrLog.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date; //Convert.ToDateTime(txtReceiptDate.Text);
                            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                            ra_hdrLog.Grah_app_stus = "P";
                            ra_hdrLog.Grah_app_tp = "ARQT011";
                            ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                            ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                            ra_hdrLog.Grah_cre_dt = CHNLSVC.Security.GetServerDateTime().Date; ;//Convert.ToDateTime(txtReceiptDate.Text);

                            ra_hdrLog.Grah_fuc_cd = ddlRef.SelectedValue.ToString();// lblAccNo.Text;

                            ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;
                            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                            ra_hdrLog.Grah_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(txtReceiptDate.Text);
                            //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                            //{
                            //    ra_hdrLog.Grah_oth_loc = "1";
                            //}
                            //else
                            // {
                            ra_hdrLog.Grah_oth_loc = "0";
                            //}
                            //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                            ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                            ra_hdrLog.Grah_remaks = "";

                            #endregion

                            #region fill List<RequestApprovalDetailLog>

                            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                            RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                            ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                            ra_detLog.Grad_line = 1;
                            ra_detLog.Grad_req_param = "MAN_DOC";
                            // ra_detLog.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                            ra_detLog.Grad_is_rt1 = false;
                            //  ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                            ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                            ra_detLog_List.Add(ra_detLog);
                            ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                            #endregion

                            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                            _ReqAppAuto.Aut_cate_tp = "PC";
                            _ReqAppAuto.Aut_direction = 1;
                            _ReqAppAuto.Aut_modify_dt = null;
                            _ReqAppAuto.Aut_moduleid = "REQ";
                            _ReqAppAuto.Aut_number = 0;
                            _ReqAppAuto.Aut_start_char = "MANDOC";
                            _ReqAppAuto.Aut_year = null;

                            string referenceNo = "";
                            string Req_status = "";
                            //CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqRequestLevel, GlbReqIsApprovalUser, true, ra_hdr.Grah_ref.ToString());
                            // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, true, out referenceNo);
                            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(_ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out Req_status);
                            if (eff > 0)
                            {
                                Int32 eff2 = CHNLSVC.Inventory.Save_mandoc_request_serials(referenceNo, ddlRef.SelectedValue.ToString(), GlbReqUserPermissionLevel, BaseCls.GlbUserID);

                                MessageBox.Show("Request sent!\nReference:" + referenceNo);
                                if (Req_status == "A")
                                {
                                    MessageBox.Show("Request is also in the Approved Status!");
                                }

                                //---------------add 28-03-2013--------------------------
                                try
                                {
                                    //Int32 eff2 = CHNLSVC.Inventory.Save_mandoc_request_serials(referenceNo, ddlRef.SelectedValue.ToString(), GlbReqUserPermissionLevel, BaseCls.GlbUserID);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Could not save book serial details.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                //-----------------------------------------------------
                                this.btnClear_Click(null, null);
                            }
                            else
                            {
                                MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;

                        }
                    }

                    //} if condition


                }

                //************ADD------28-03-2013------------
                List<RequestApprovalHeader> approved = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRef.SelectedValue.ToString(), HirePurchasModuleApprovalCode.ARQT011.ToString(), 1, 0);
                Int32 CNT = 0;
                if (approved != null)
                {
                    var _count = from _dup in approved
                                 where _dup.Grah_fuc_cd == ddlRef.SelectedValue.ToString() && _dup.Grah_oth_loc == txtTransLoc.Text.Trim().ToUpper()
                                 select _dup;
                    CNT = _count.Count();
                }
                if (CNT == 0)
                {
                    MessageBox.Show("Please enter approved transfer location?", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-------------------------------------------

                if (MessageBox.Show("Are you sure to Transfer?", "Confirm Transfer", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                //***************added 28-03-2013****************************
                #region    CHECK- WHETHER ONLY THE APPROVED ITEMS ARE GOING TO BE TRANSFERED
                List<RequestApprovalSerials> ApprSerList = new List<RequestApprovalSerials>();
                foreach (RequestApprovalHeader HDR in approved)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    string appRequestNo = HDR.Grad_ref.ToString();

                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, appRequestNo, out List);
                    ApprSerList.AddRange(List);
                }
                Int32 REQ_SERIAL_COUNT = ApprSerList.Count;//ONLY ONE PENDING REQUEST CAN BE HELD FOR A PERTICULAR DOCUMENT-REF
                Int32 CURRENTLY_SELECTED_COUNT = gvManualDocDet.Rows.Count;
                if (REQ_SERIAL_COUNT != CURRENTLY_SELECTED_COUNT)
                {
                    MessageBox.Show("Please select all the requested books!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (REQ_SERIAL_COUNT != CURRENTLY_SELECTED_COUNT)
                //{
                //Int32 COUNT_SER = 0;
                if (approved != null)
                {
                    foreach (RequestApprovalSerials APP_ser in ApprSerList)
                    {
                        bool exist = false;
                        foreach (DataGridViewRow dgrv_row in gvManualDocDet.Rows)
                        {

                            if (dgrv_row.Cells["MDD_PREFIX"].Value.ToString() == APP_ser.Gras_anal5 && dgrv_row.Cells["MDD_BK_NO"].Value.ToString() == APP_ser.Gras_anal6.ToString())
                            {
                                exist = true;
                            }
                        }
                        if (exist == false)
                        {
                            MessageBox.Show("Please select the requested books!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                //}
                #endregion
                //********************************************************         
                btnTransfer.Enabled = false;
                TransferManualDocument(approved);
                btnTransfer.Enabled = true;
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
        private void TransferManualDocument(List<RequestApprovalHeader> approvedReqList)
        {
            try
            {
                if (chkTrans.Checked == false)
                {
                    //throw new UIValidationException("Cannot transfer. This is not confirmed yet");
                    MessageBox.Show("Cannot transfer. This is not confirmed yet");
                    return;
                }
                if (chkTrans.Checked == false)
                {
                    //throw new UIValidationException("Please select transfer location");
                    MessageBox.Show("Please select transfer location.");
                    return;
                }
                if (txtTransLoc.Text.Trim() == "")
                {
                    //throw new UIValidationException("Please select transfer location");
                    MessageBox.Show("Please select transfer location.");
                    return;
                }

                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                string _AdjNumber = string.Empty;

                //put adj OUT
                List<ReptPickSerials> _list = CHNLSVC.Inventory.Get_all_Serial_details(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

                InventoryHeader _invHeader = new InventoryHeader();
                _invHeader.Ith_com = BaseCls.GlbUserComCode;
                _invHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                DateTime _docDate = Convert.ToDateTime(txtRecDate.Value);
                _invHeader.Ith_doc_date = _docDate;
                _invHeader.Ith_doc_year = _docDate.Year;
                _invHeader.Ith_direct = false;
                _invHeader.Ith_doc_tp = "ADJ";
                _invHeader.Ith_cate_tp = "NOR";
                _invHeader.Ith_sub_tp = "NOR";
                _invHeader.Ith_bus_entity = "";
                _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = "";
                //_invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = BaseCls.GlbUserID;
                _invHeader.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;
                _invHeader.Ith_mod_by = BaseCls.GlbUserID;
                _invHeader.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;
                _invHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _invHeader.Ith_oth_docno = "";

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "ADJ";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "ADJ";
                _masterAuto.Aut_year = null;

                string _message = string.Empty;
                string _genInventoryDoc = string.Empty;
                string _genSalesDoc = string.Empty;
                int rows_inserted = 0;

                //*************************************************************
                DataTable dtTempManDoc = CHNLSVC.Inventory.GetTempManualDocDet(BaseCls.GlbUserComCode, BaseCls.GlbUserID);
                List<ManualDocDetail> transList = new List<ManualDocDetail>();

                foreach (DataRow dr in dtTempManDoc.Rows)
                {
                    string ref_ = ddlRef.SelectedValue.ToString();
                    string itmCd = dr["MDD_ITM_CD"].ToString();
                    string prifix = dr["MDD_PREFIX"].ToString();
                    string bookNo = dr["MDD_BK_NO"].ToString();
                    List<ManualDocDetail> DocList = CHNLSVC.Inventory.Get_manual_doc(BaseCls.GlbUserComCode, ref_, itmCd, prifix, bookNo);
                    if (DocList != null)
                    {
                        ManualDocDetail mandoc = DocList[0];
                        Int32 first = DocList[0].Mdd_current;

                        //mandoc.Mdd_bk_no;
                        // mandoc.Mdd_bk_tp;
                        mandoc.Mdd_cnt = DocList[0].Mdd_last - DocList[0].Mdd_current + 1;
                        mandoc.Mdd_current = first;
                        //mandoc.Mdd_dt;
                        mandoc.Mdd_first = first;
                        //mandoc.Mdd_issue_by;
                        //mandoc.Mdd_issue_loc;
                        //mandoc.Mdd_itm_cd;
                        //mandoc.Mdd_last;
                        //mandoc.Mdd_line;
                        mandoc.Mdd_loc = txtTransLoc.Text.Trim().ToUpper();
                        //mandoc.Mdd_prefix;
                        //mandoc.Mdd_receive_dt;
                        mandoc.Mdd_ref = ref_;
                        //mandoc.Mdd_rem;
                        mandoc.Mdd_status = "P";
                        mandoc.Mdd_trans_loc = txtTransLoc.Text.Trim().ToUpper();
                        mandoc.Mdd_used = 0;
                        //mandoc.Mdd_user;
                        mandoc.Mdd_using = 0;

                        transList.Add(mandoc);
                    }
                }
                Int32 effect = CHNLSVC.Inventory.SaveManualDocDet(transList);
                //*************************************************************
                //Process adj minus
               // Int32 Eff = CHNLSVC.Inventory.Manual_Doc_Transfer(_invHeader, _list, null, _masterAuto, out _AdjNumber);

                // Int32 Eff2 = CHNLSVC.Inventory.UpdateTransferStatus(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID, txtTransLoc.Text.Trim().ToUpper());
                Int32 Eff2 = CHNLSVC.Inventory.UpdateTransferStatus_NEW(ddlRef.SelectedValue.ToString(), BaseCls.GlbUserID, txtTransLoc.Text.Trim().ToUpper(), BaseCls.GlbUserDefLoca);

                MessageBox.Show("Successfully transfered " + ddlRef.SelectedValue.ToString() + "\n ADJ(-): " + _AdjNumber);
                //****************add 28-03-2013******************************** 
                try
                {
                    foreach (RequestApprovalHeader appreqheader in approvedReqList)
                    {
                        string Req_no = appreqheader.Grah_ref;
                        RequestApprovalHeader REQ_HEADER_toCompleate = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                        REQ_HEADER_toCompleate.Grah_app_stus = "F";
                        Int32 eff = CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER_toCompleate);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //****************add 28-03-2013********************************  

                // ClearData();
                this.btnClear_Click(null, null);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ManualDocReceived formnew = new ManualDocReceived();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
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

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
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
        #endregion
        private void btnPcSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIssLoc;
                _CommonSearch.ShowDialog();
                txtIssLoc.Focus();
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

        private void btnSearchTransLoc_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtTransLoc;
            //_CommonSearch.ShowDialog();
            //txtTransLoc.Focus();
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTransLoc;
                _CommonSearch.ShowDialog();
                txtTransLoc.Focus();
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

        private void ManualDocReceived_Load(object sender, EventArgs e)
        {

        }

        private void chkTrans_CheckedChanged(object sender, EventArgs e)
        {
            Int16 status = 0;
            gvManualDocDet.DataSource = null;
            gvManualDocDet.AutoGenerateColumns = false;

            if (chkTrans.Checked == true)
            {
                btnTransfer.Enabled = true;
                btnConfirm.Enabled = false;
                status = 1; //F books

                btnSendReq.Visible = true;
            }
            else
            {
                btnTransfer.Enabled = false;
                btnConfirm.Enabled = true;
                //status = 2;   //P AND T books
                status = 0;   //P

                btnSendReq.Visible = false;
            }
            //----------------------------------------------------------
            try
            {
                //   LoadRefDetails(status);

                DataTable _tbl = CHNLSVC.Inventory.GetManualDocs(BaseCls.GlbUserDefLoca, status);//_IsRec= 1
                if (_tbl != null)
                {
                    if (_tbl.Rows.Count > 0)
                    {
                        DataRow dr = _tbl.NewRow();
                        _tbl.Rows.Add(dr);
                        dr["MDD_REF"] = "---SELECT---";
                    }
                }
                //ddlRef.DataSource = null;
                ddlRef.DataSource = _tbl;
                ddlRef.DisplayMember = "MDD_REF";
                ddlRef.ValueMember = "MDD_REF";
                ddlRef.SelectedValue = "---SELECT---";
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

        private void txtTransLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F2)
            {
                this.btnSearchTransLoc_Click(null, null);
            }
        }

        private void txtTransLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchTransLoc_Click(null, null);
        }

        private void txtIssLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnPcSearch_Click(null, null);
        }

        private void txtIssLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F2)
            {
                this.btnPcSearch_Click(null, null);
            }
        }

        private void txtIssLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            { this.btnPcSearch_Click(null, null); }
        }

        private void txtTransLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            { this.btnSearchTransLoc_Click(null, null); }
        }

        private void grvPendingRegReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    #region
                    /*
                DataGridViewRow row = grvPendingRegReq.Rows[rowIndex];
                string Req_no = row.Cells["rpend_Grah_ref"].Value.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.INVMRN, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = Req_no;
                #endregion


                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "MAN_DOC";
                // ra_det.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_det.Grad_is_rt1 = false;
                // ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog
                //REQ_HEADER

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = REQ_HEADER.Grah_app_by;
                ra_hdrLog.Grah_app_dt = REQ_HEADER.Grah_app_dt;
                ra_hdrLog.Grah_app_lvl = REQ_HEADER.Grah_app_lvl;
                ra_hdrLog.Grah_app_stus = "A";// REQ_HEADER.Grah_app_stus;
                ra_hdrLog.Grah_app_tp = REQ_HEADER.Grah_app_tp;
                ra_hdrLog.Grah_com = REQ_HEADER.Grah_com;
                ra_hdrLog.Grah_cre_by = REQ_HEADER.Grah_cre_by;
                ra_hdrLog.Grah_cre_dt = REQ_HEADER.Grah_cre_dt;
                ra_hdrLog.Grah_fuc_cd = REQ_HEADER.Grah_fuc_cd;
                ra_hdrLog.Grah_loc = REQ_HEADER.Grah_loc;

                ra_hdrLog.Grah_mod_by = REQ_HEADER.Grah_mod_by;
                ra_hdrLog.Grah_mod_dt = REQ_HEADER.Grah_mod_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdrLog.Grah_oth_loc = "1";
                //}
                //else
                // {
                ra_hdrLog.Grah_oth_loc = REQ_HEADER.Grah_oth_loc;
                //}
                //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdrLog.Grah_ref = Req_no;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = Req_no;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "MAN_DOC";
                // ra_detLog.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_detLog.Grad_is_rt1 = false;
                //  ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                #endregion

                string referenceNo = "";
                string Req_status = "";
                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out Req_status);
                if (eff > 0)
                {
                    // MessageBox.Show("Request sent!\nReference:" + referenceNo);
                    if (Req_status == "A")
                    {
                        MessageBox.Show("Request Approved !");
                    }
                    else
                    {
                        MessageBox.Show("Request Updated !");
                    }
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!");
                }
                */
                    #endregion

                    //--------new------------------------------------
                    DataGridViewRow row = grvPendingRegReq.Rows[rowIndex];
                    string Req_no = row.Cells["rpend_Grah_ref"].Value.ToString();
                    RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                    List<RequestApprovalHeader> p_lst = new List<RequestApprovalHeader>();
                    p_lst.Add(REQ_HEADER);
                    grvApprovePendingReq.DataSource = null;
                    grvApprovePendingReq.AutoGenerateColumns = false;
                    grvApprovePendingReq.DataSource = p_lst;

                    //TODO: FILL  REQ.BOOK GRID
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, Req_no, out List);
                    grvReqbooks.DataSource = null;
                    grvReqbooks.AutoGenerateColumns = false;
                    grvReqbooks.DataSource = dt;
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                // toolStrip1.Show();
                btnTransfer.Visible = true;
                btnConfirm.Visible = true;
            }
            else
            {
                // toolStrip1.Hide();
                btnTransfer.Visible = false;
                btnConfirm.Visible = false;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvApprovePendingReq.Rows.Count < 1)
                {
                    return;
                }
                Int32 COUNT = 0;
                foreach (DataGridViewRow dgvr in grvReqbooks.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        COUNT = COUNT + 1;
                    }
                }
                if (COUNT == 0)
                {
                    MessageBox.Show("Please select books to approve!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataGridViewRow row = grvApprovePendingReq.Rows[0];

                string Req_no = row.Cells["a_Grah_ref"].Value.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.INVMAN, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                if (GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }

                if (REQ_HEADER.Grah_app_lvl == GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has already updated the status!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //GlbReqUserPermissionLevel
                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = Req_no;
                ra_hdr.Grah_app_stus = "A";
                #endregion


                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "MAN_DOC";
                // ra_det.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_det.Grad_is_rt1 = false;
                // ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog
                //REQ_HEADER

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = REQ_HEADER.Grah_app_by;
                ra_hdrLog.Grah_app_dt = REQ_HEADER.Grah_app_dt;
                ra_hdrLog.Grah_app_lvl = REQ_HEADER.Grah_app_lvl;
                ra_hdrLog.Grah_app_stus = "A";// REQ_HEADER.Grah_app_stus;
                ra_hdrLog.Grah_app_tp = REQ_HEADER.Grah_app_tp;
                ra_hdrLog.Grah_com = REQ_HEADER.Grah_com;
                ra_hdrLog.Grah_cre_by = REQ_HEADER.Grah_cre_by;
                ra_hdrLog.Grah_cre_dt = REQ_HEADER.Grah_cre_dt;
                ra_hdrLog.Grah_fuc_cd = REQ_HEADER.Grah_fuc_cd;
                ra_hdrLog.Grah_loc = REQ_HEADER.Grah_loc;

                ra_hdrLog.Grah_mod_by = REQ_HEADER.Grah_mod_by;
                ra_hdrLog.Grah_mod_dt = REQ_HEADER.Grah_mod_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdrLog.Grah_oth_loc = "1";
                //}
                //else
                // {
                ra_hdrLog.Grah_oth_loc = REQ_HEADER.Grah_oth_loc;
                //}
                //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdrLog.Grah_ref = Req_no;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = Req_no;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "MAN_DOC";
                // ra_detLog.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_detLog.Grad_is_rt1 = false;
                //  ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                #endregion

                if (MessageBox.Show("Are you sure to Approve?", "Confirm Approve", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                string referenceNo = "";
                string Req_status = "";

                // GlbReqIsFinalApprovalUser = true;

                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out Req_status);
                if (eff > 0)
                {
                    // MessageBox.Show("Request sent!\nReference:" + referenceNo);
                    if (Req_status == "A")
                    {
                        MessageBox.Show("Request Approved Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Request Updated Successfully!");
                    }
                    try
                    {
                        //TODO: FILL  REQ.BOOK GRID
                        // grvReqbooks.
                        grvReqbooks.EndEdit();
                        List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                        List<RequestApprovalSerials> saveSerList = new List<RequestApprovalSerials>();
                        DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, Req_no, out List);
                        DataTable dt2 = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, Req_no, out saveSerList);
                        foreach (RequestApprovalSerials reqSer in List)
                        {
                            string document = reqSer.Gras_anal3;
                            string itemcode = reqSer.Gras_anal4;
                            string prifix = reqSer.Gras_anal5;
                            Decimal bookNo = reqSer.Gras_anal6;
                            foreach (DataGridViewRow dgvr in grvReqbooks.Rows)
                            {
                                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                                string prifix_ = dgvr.Cells["Gras_anal5"].Value.ToString();
                                string bookno = dgvr.Cells["Gras_anal6"].Value.ToString();
                                bool ischecked = Convert.ToBoolean(dgvr.Cells[0].Value);
                                //if (Convert.ToBoolean(chk.Value == null ? false : chk.Value) == false)
                                if (ischecked == false)
                                {
                                    //list.Add(dgvr.Cells[1].Value.ToString());
                                    saveSerList.RemoveAll(x => x.Gras_anal5 == prifix_ && x.Gras_anal6 == Convert.ToDecimal(bookno));
                                }
                            }
                        }
                        //TODO: REMOVE UNCKECKED 
                        //  _tempReqAppSer.Gras_anal3 = document;
                        //_tempReqAppSer.Gras_anal4 = itemcode;
                        //_tempReqAppSer.Gras_anal5 = prifix;
                        //_tempReqAppSer.Gras_anal6 = bookNo;

                        //  GlbReqIsFinalApprovalUser = true;
                        if (saveSerList.Count == 0)
                        {
                            return;
                        }
                        Int32 ef = CHNLSVC.General.Save_approve_ser_and_Log(saveSerList, false, Req_no, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Serials not saved!");
                    }
                }
                else
                {
                    MessageBox.Show("Sorry. Request not updated!");
                }
                //}
                load_Pending_Requests();
                load_userloc_allRequests();
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

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvApprovePendingReq.Rows.Count < 1)
                {
                    return;
                }
                DataGridViewRow row = grvApprovePendingReq.Rows[0];
                //  DataGridViewRow row = grvPendingRegReq.Rows[rowIndex];
                string Req_no = row.Cells["a_Grah_ref"].Value.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);

                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.INVMAN, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                if (GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }
                if (REQ_HEADER.Grah_app_lvl == GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has updated the status already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = Req_no;
                #endregion


                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "MAN_DOC";
                // ra_det.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_det.Grad_is_rt1 = false;
                // ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog
                //REQ_HEADER

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = REQ_HEADER.Grah_app_by;
                ra_hdrLog.Grah_app_dt = REQ_HEADER.Grah_app_dt;
                ra_hdrLog.Grah_app_lvl = REQ_HEADER.Grah_app_lvl;
                ra_hdrLog.Grah_app_stus = "R";// REQ_HEADER.Grah_app_stus;
                ra_hdrLog.Grah_app_tp = REQ_HEADER.Grah_app_tp;
                ra_hdrLog.Grah_com = REQ_HEADER.Grah_com;
                ra_hdrLog.Grah_cre_by = REQ_HEADER.Grah_cre_by;
                ra_hdrLog.Grah_cre_dt = REQ_HEADER.Grah_cre_dt;
                ra_hdrLog.Grah_fuc_cd = REQ_HEADER.Grah_fuc_cd;
                ra_hdrLog.Grah_loc = REQ_HEADER.Grah_loc;

                ra_hdrLog.Grah_mod_by = REQ_HEADER.Grah_mod_by;
                ra_hdrLog.Grah_mod_dt = REQ_HEADER.Grah_mod_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdrLog.Grah_oth_loc = "1";
                //}
                //else
                // {
                ra_hdrLog.Grah_oth_loc = REQ_HEADER.Grah_oth_loc;
                //}
                //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdrLog.Grah_ref = Req_no;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = Req_no;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "MAN_DOC";
                // ra_detLog.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_detLog.Grad_is_rt1 = false;
                //  ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                #endregion

                if (MessageBox.Show("Are you sure to Reject?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                string referenceNo = "";
                string Req_status = "";

                // GlbReqIsFinalApprovalUser = true;

                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out Req_status);
                if (eff > 0)
                {
                    RequestApprovalHeader REQ_HEADER_toReject = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                    REQ_HEADER_toReject.Grah_app_stus = "R";
                    eff = CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER_toReject);
                    if (eff > 0)
                    {
                        MessageBox.Show("Request Rejected Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Request Not Rejected!");
                    }

                }
                else
                {
                    MessageBox.Show("Sorry. Request Not Rejected!");
                }
                load_Pending_Requests();
                load_userloc_allRequests();
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

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSendReq_Click(object sender, EventArgs e)
        {
            try
            {


                // GetUserAppLevel(HirePurchasModuleApprovalCode.ARQT011);
                GetUserAppLevel(HirePurchasModuleApprovalCode.INVMAN);
                if (ddlRef.SelectedValue.ToString() == "---SELECT---")
                {
                    MessageBox.Show("Please select a document!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTransLoc.Text == "")
                {
                    MessageBox.Show("Please enter transfer location!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #region
                //----------added 28-03-2013
                List<RequestApprovalHeader> pendingHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlRef.SelectedValue.ToString(), HirePurchasModuleApprovalCode.ARQT011.ToString(), 0, 100);


                //Added by Prabhath on 11/10/2013
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT011", Convert.ToString(ddlRef.SelectedValue)))
                { return; }


                Int32 COUNT_2 = 0;
                if (pendingHeaders != null)
                {
                    var _count = from _dup in pendingHeaders
                                 where _dup.Grah_fuc_cd == ddlRef.SelectedValue.ToString()
                                 select _dup;
                    COUNT_2 = _count.Count();
                    if (COUNT_2 > 0)
                    {
                        MessageBox.Show("Pending request exists already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //----------added 28-03-2013
                #endregion

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT011";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_fuc_cd = ddlRef.SelectedValue.ToString();//lblAccNo.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdr.Grah_oth_loc = "1";
                //}
                //else
                //{ 
                ra_hdr.Grah_oth_loc = txtTransLoc.Text.Trim().ToUpper();//"0";
                //}
                //if (ddlPendinReqNo.SelectedValue.ToString() == "New Request" || ddlPendinReqNo.SelectedValue.ToString() == string.Empty)
                //if (ddlPendinReqNo.SelectedValue == null)
                //{
                //ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                //}
                //else if (ddlPendinReqNo.SelectedValue.ToString() == "New Request")
                //{
                //   ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                //}
                //else
                // {
                //   ra_hdr.Grah_ref = ddlPendinReqNo.SelectedValue.ToString();
                //}
                ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "MAN DOC TRANSFER";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "MAN_DOC";
                // ra_det.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_det.Grad_is_rt1 = false;
                // ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date; //Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT011";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = CHNLSVC.Security.GetServerDateTime().Date; ;//Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_fuc_cd = ddlRef.SelectedValue.ToString();// lblAccNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;
                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(txtReceiptDate.Text);
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdrLog.Grah_oth_loc = "1";
                //}
                //else
                // {
                ra_hdrLog.Grah_oth_loc = "0";
                //}
                //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "MAN_DOC";
                // ra_detLog.Grad_val1 = _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                ra_detLog.Grad_is_rt1 = false;
                //  ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                #endregion

                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "MANDOC";
                _ReqAppAuto.Aut_year = null;

                string referenceNo = "";
                string Req_status = "";

                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(_ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out Req_status);
                if (eff > 0)
                {
                    Int32 eff2 = CHNLSVC.Inventory.Save_mandoc_request_serials(referenceNo, ddlRef.SelectedValue.ToString(), GlbReqUserPermissionLevel, BaseCls.GlbUserID);

                    MessageBox.Show("Request sent!\nReference:" + referenceNo);
                    if (Req_status == "A")
                    {
                        MessageBox.Show("Request is also in the Approved Status!");
                    }

                    //---------------add 28-03-2013--------------------------
                    try
                    {
                        //Int32 eff2 = CHNLSVC.Inventory.Save_mandoc_request_serials(referenceNo, ddlRef.SelectedValue.ToString(), GlbReqUserPermissionLevel, BaseCls.GlbUserID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not save book serial details.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //-----------------------------------------------------
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
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

        private void grvAllRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    if (chkTrans.Checked == true)
                    {
                        try
                        {
                            Int32 rowIndex = e.RowIndex;
                            string DOC = grvAllRequests["fuc_cd", rowIndex].Value.ToString();
                            //MessageBox.Show();
                            string transLoc = grvAllRequests["oth_loc", rowIndex].Value.ToString();
                            txtTransLoc.Text = transLoc;
                            ddlRef.SelectedValue = DOC;
                        }
                        catch (Exception ex)
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

        private void grvAllRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in grvReqbooks.Rows)
            {
                r.Cells["chkAppr"].Value = chkAll.Checked;
            }

        }


    }
}
