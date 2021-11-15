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
using System.IO;
using FF.WindowsERPClient.Reports.Sales;

namespace FF.WindowsERPClient.Finance
{
    //pkg_search.sp_search_receipt
    //sp_getrecbydaterange
    //sp_getreceipt
    //sp_updaterefunddet
    //sp_get_gen_reqapp_ser
    //sp_get_vehinsubyref =new
    //Refund_update_sat_veh_ins_txn =NEW   
    //Refund_updtREG_sat_veh_ins_txn =new
    //sp_get_vehRegbyref =new

    public partial class RecieptReversal : Base
    {
        RecieptHeader selectedReceiptHdr = new RecieptHeader();
        List<RecieptHeader> _receiptHdr = new List<RecieptHeader>();
        List<RecieptHeader> _receiptHdrExp = new List<RecieptHeader>();

        List<RecieptItem> _RecItemList = new List<RecieptItem>();

        //---------------------------VEH INSU----------------------------------
        List<RecieptHeader> _vehInsu_receiptHdrs = new List<RecieptHeader>();

        List<RecieptItem> _vehInsu_RecItemList = new List<RecieptItem>();

        RequestApprovalHeader _refAppHdr = new RequestApprovalHeader();
        List<RequestApprovalDetail> _refAppDet = new List<RequestApprovalDetail>();
        MasterAutoNumber _refAppAuto = new MasterAutoNumber();

        RequestApprovalHeaderLog _refAppHdrLog = new RequestApprovalHeaderLog();
        List<RequestApprovalDetailLog> _refAppDetLog = new List<RequestApprovalDetailLog>();

        private List<ReceiptItemDetails> _tmpRecItem = new List<ReceiptItemDetails>();

        DateTime currentDate = DateTime.Now.Date;
        Boolean _isAppUser = false;
        int _appLvl = 0;
        Boolean _isFromReq = false;
        Boolean _isExpired = false;
        public RecieptReversal()
        {
            try
            {
                InitializeComponent();
                currentDate = CHNLSVC.Security.GetServerDateTime().Date;
                load_Pending_Requests_Insurance();
                load_Approved_Requests_Insurance();

                load_Pending_Requests_Registration();
                load_Approved_Requests_Registration();

                pickedDate.Value = currentDate;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            //BackDates_MODULE_name = this.GlbModuleName;
            //if (BackDates_MODULE_name==null)
            //{
            //    BackDates_MODULE_name = "m_Trans_Finance_ReceiptReversal";
            //}
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, toolStripLabelBD, string.Empty);

        }
        private void RecieptReversal_Load(object sender, EventArgs e)
        {
            try
            {
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, toolStripLabelBD, string.Empty, out _allowCurrentTrans);
                clear_cashRefund();
                bindRecSubTypes();
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

        private void bindRecSubTypes()
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbRecTp.Text + seperator);
            _CommonSearch.SearchParams = paramsText.ToString();

            DataTable _dt = CHNLSVC.CommonSearch.GetSubReceiptTypes(_CommonSearch.SearchParams, null, null);
            _dt = _dt.AsEnumerable().Where(x => x.Field<Int16>("msrst_refund_prd") > Convert.ToInt16(0)).ToList().CopyToDataTable();

            if (_dt.Rows.Count > 0)
            {
                cmbSubType.DataSource = _dt;
                cmbSubType.DisplayMember = "Description";
                cmbSubType.ValueMember = "Code";
            }
            else
            {
                cmbSubType.DataSource = null;
            }
        }
        private bool IsBackDateOk()
        {
            try
            {
                bool _isOK = true;
                string selectDate = pickedDate.Value.Date.ToShortDateString();//Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToShortDateString();
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, toolStripLabelBD, selectDate, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (pickedDate.Value.Date != DateTime.Now.Date)
                        {
                            pickedDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pickedDate.Focus();
                            _isOK = false;
                            return _isOK;
                        }
                    }
                    else
                    {
                        pickedDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pickedDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }

                return _isOK;
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
        private void load_Approved_Requests_Registration()
        {
            try
            {
                //---------------------------VEH INSU----------------------------------
                //Approved list.
                List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT020", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehRegRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT020.ToString(), 1, GlbReqUserPermissionLevel);
                if (_lst == null)
                {
                    _lst = new List<RequestApprovalHeader>();
                }
                List<RequestApprovalHeader> p_lst_ARQT016 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT016", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT019.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT016 != null)
                {
                    _lst.AddRange(p_lst_ARQT016);
                }
                List<RequestApprovalHeader> p_lst_ARQT018 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT018", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT017.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT018 != null)
                {
                    _lst.AddRange(p_lst_ARQT018);
                }

                if (_lst != null)
                {
                    var _duplicate = from _dup in _lst
                                     where _dup.Grah_app_stus == "A"
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        _lst = _duplicate.ToList();
                        grvAppRegReq.DataSource = null;
                        grvAppRegReq.AutoGenerateColumns = false;
                        grvAppRegReq.DataSource = _lst;
                    }
                }

                ddlApprVehReg_req.Items.Clear();
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        ddlApprVehReg_req.Items.Clear();
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        query.Select(x => x).Distinct();
                        ddlApprVehReg_req.DataSource = query.ToList();
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
        private void load_Approved_Requests_Insurance()
        {
            try
            {
                //---------------------------VEH INSU----------------------------------
                //Approved list.
                List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT021", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT021.ToString(), 1, GlbReqUserPermissionLevel);
                if (_lst == null)
                {
                    _lst = new List<RequestApprovalHeader>();
                }
                List<RequestApprovalHeader> p_lst_ARQT019 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT019", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT019.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT019 != null)
                {
                    _lst.AddRange(p_lst_ARQT019);
                }
                List<RequestApprovalHeader> p_lst_ARQT017 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT017", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT017.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT017 != null)
                {
                    _lst.AddRange(p_lst_ARQT017);
                }

                if (_lst != null)
                {
                    var _duplicate = from _dup in _lst
                                     where _dup.Grah_app_stus == "A"
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        _lst = _duplicate.ToList();
                        grvAppInsReq.DataSource = null;
                        grvAppInsReq.AutoGenerateColumns = false;
                        grvAppInsReq.DataSource = _lst;
                    }
                }

                ddlApprVehIns_req.Items.Clear();
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        ddlApprVehIns_req.Items.Clear();
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        query.Select(x => x).Distinct();
                        ddlApprVehIns_req.DataSource = query.ToList();
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
        private void load_Pending_Requests_Registration()
        {
            try
            {
                //Pending list.
                List<RequestApprovalHeader> p_lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT020", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehRegRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT020.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst == null)
                {
                    p_lst = new List<RequestApprovalHeader>();
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst)
                    //{                    
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);

                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                }
                List<RequestApprovalHeader> p_lst_ARQT016 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT016", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT019.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT016 != null)
                {
                    p_lst.AddRange(p_lst_ARQT016);
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst_ARQT016)
                    //{
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);

                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                }
                List<RequestApprovalHeader> p_lst_ARQT018 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT018", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT017.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT018 != null)
                {
                    p_lst.AddRange(p_lst_ARQT018);
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst_ARQT018)
                    //{
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);

                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                }
                //-------------------------------------------------------------------------------------------------
                if (p_lst != null)
                {
                    if (p_lst.Count > 0)
                    {
                        foreach (RequestApprovalHeader rqh in p_lst)
                        {
                            List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                            DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);
                            try
                            {
                                rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                                rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
                //---------------------------------------------------------------------
                ddlPendingVehReg_req.Items.Clear();
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
                if (p_lst != null)
                {
                    if (p_lst.Count > 0)
                    {
                        ddlPendingVehReg_req.Items.Clear();
                        p_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = p_lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        query.Select(x => x).Distinct();
                        ddlPendingVehReg_req.DataSource = query.ToList();
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
        private void load_Pending_Requests_Insurance()
        {
            try
            {
                //Pending list.
                List<RequestApprovalHeader> p_lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT021", BaseCls.GlbUserID, string.Empty); //CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT021.ToString(), 0, GlbReqUserPermissionLevel);      
                List<RequestApprovalHeader> p_lst_ARQT019 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT019", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT019.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst == null)
                {
                    p_lst = new List<RequestApprovalHeader>();
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst)
                    //{
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);
                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch(Exception ex){

                    //    }

                    //}            
                }
                if (p_lst_ARQT019 != null)
                {
                    p_lst.AddRange(p_lst_ARQT019);
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst_ARQT019)
                    //{
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);

                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                }
                List<RequestApprovalHeader> p_lst_ARQT017 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT017", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT017.ToString(), 0, GlbReqUserPermissionLevel);
                if (p_lst_ARQT017 != null)
                {
                    p_lst.AddRange(p_lst_ARQT017);
                }
                else
                {
                    ////TODO: GET invoice,receipt #                
                    //foreach (RequestApprovalHeader rqh in p_lst_ARQT017)
                    //{
                    //    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    //    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);

                    //    try
                    //    {
                    //        rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                    //        rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                }
                //------------------------------------------------------------------------
                ddlPendingVehIns_req.Items.Clear();
                //-----------------------------------------------------------------------------
                if (p_lst != null)
                {
                    if (p_lst.Count > 0)
                    {
                        foreach (RequestApprovalHeader rqh in p_lst)
                        {
                            List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                            DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, rqh.Grah_ref, out List);
                            try
                            {
                                rqh.Grad_anal1 = List[0].Gras_anal1; //invoice #
                                rqh.Grad_anal5 = List[0].Gras_anal5; //receipt #
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
                //-------------------------------------------------------------------------------
                if (p_lst != null)
                {
                    var _duplicate = from _dup in p_lst
                                     where _dup.Grah_app_stus == "P"
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        p_lst = _duplicate.ToList();
                        //add 18-03-2013
                        grvPendingInsReq.DataSource = null;
                        grvPendingInsReq.AutoGenerateColumns = false;
                        grvPendingInsReq.DataSource = p_lst;
                    }
                }

                if (p_lst != null)
                {
                    if (p_lst.Count > 0)
                    {
                        ddlPendingVehIns_req.Items.Clear();
                        p_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = p_lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        query.Select(x => x).Distinct();
                        ddlPendingVehIns_req.DataSource = query.ToList();

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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearchByDate_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();
                //txtFromDate
                // _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtFromDate_.Text).Date, Convert.ToDateTime(txtToDate_.Text).Date, "ADVAN");
                _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtFromDate.Value, txtToDate.Value, "ADVAN");
                _receiptHdr = _paramReceipt;
                if (_receiptHdr != null)
                {
                    gvReceipt.DataSource = null;
                    gvReceipt.AutoGenerateColumns = false;
                    gvReceipt.DataSource = _receiptHdr;
                    //gvReceipt.DataBind();
                }
                else
                {
                    DataTable _Itemtable = new DataTable();
                    gvReceipt.DataSource = _Itemtable;
                    //gvReceipt.DataBind();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReceiptNo.Text.Trim().ToUpper() == "")
                {
                    return;
                }

                if (string.IsNullOrEmpty(cmbRecTp.Text))
                {
                    MessageBox.Show("Please select receipt type.", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbRecTp.Focus();
                    return;
                }

                DataTable _Itemtable = new DataTable();
                gvReceipt.DataSource = null;
                gvReceipt.AutoGenerateColumns = false;
                gvReceipt.DataSource = _Itemtable;

                gvItems.DataSource = null;
                gvItems.AutoGenerateColumns = false;
                gvItems.DataSource = _Itemtable;


                List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();

                _paramReceipt = CHNLSVC.Sales.GetReceiptHdr(txtReceiptNo.Text.Trim().ToUpper());
                if (_paramReceipt == null)
                {
                    MessageBox.Show("Receipt not found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReceiptNo.Text = "";
                    txtReceiptNo.Focus();
                    return;
                }

                if (_paramReceipt != null)
                {
                    foreach (RecieptHeader rechdr in _paramReceipt)
                    {
                        if (rechdr.Sar_receipt_type != cmbRecTp.Text)
                        {
                            MessageBox.Show(txtReceiptNo.Text.Trim().ToUpper() + " is not " + cmbRecTp.Text + " Receipt!", "Receipt Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtReceiptNo.Text = "";
                            txtReceiptNo.Focus();
                            return;
                        }

                        if (rechdr.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                        {
                            MessageBox.Show("Not allow to refund other profit center receipts.", "Receipt Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtReceiptNo.Text = "";
                            txtReceiptNo.Focus();
                            return;
                        }

                        if (rechdr.Sar_tot_settle_amt == rechdr.Sar_used_amt)
                        {
                            MessageBox.Show(txtReceiptNo.Text.Trim().ToUpper() + " is fully used. Cannot refund!");
                            txtReceiptNo.Text = "";
                            txtReceiptNo.Focus();
                            return;
                        }
                        if ((rechdr.Sar_tot_settle_amt - rechdr.Sar_used_amt) <= 0)
                        {
                            MessageBox.Show(txtReceiptNo.Text.Trim().ToUpper() + " is fully used. Cannot refund!");
                            txtReceiptNo.Text = "";
                            txtReceiptNo.Focus();
                            return;
                        }

                        if (rechdr.Sar_act == false)
                        {
                            MessageBox.Show(txtReceiptNo.Text.Trim().ToUpper() + " is an In-Active receipt!");//cancelled receipt.
                            txtReceiptNo.Text = "";
                            txtReceiptNo.Focus();
                            return;
                        }


                        //DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(BaseCls.GlbUserComCode, txtReceiptNo.Text.Trim());
                        //if (_adv != null && _adv.Rows.Count > 0)
                        //{
                        //    MessageBox.Show("This advance receipt is already picked for a inter-transfer. You are not allow to refund.", "Receipt Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtReceiptNo.Text = "";
                        //    txtReceiptNo.Focus();
                        //    return;
                        //}
                    }
                }

                _receiptHdr = _paramReceipt;
                if (_receiptHdr != null)
                {
                    gvReceipt.DataSource = null;
                    gvReceipt.AutoGenerateColumns = false;
                    gvReceipt.DataSource = _receiptHdr;

                }
                else
                {
                    DataTable _Itemtable_ = new DataTable();
                    gvReceipt.AutoGenerateColumns = false;
                    gvReceipt.DataSource = _Itemtable_;

                }

                //---------load details---------------------------------------
                if (gvReceipt.Rows.Count < 1)
                {
                    MessageBox.Show("Receipt not found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReceiptNo.Text = "";
                    txtReceiptNo.Focus();
                    return;
                }
                DataGridViewRow row = gvReceipt.Rows[0];
                string _RecNo = row.Cells[1].Value.ToString();

                //selectedReceiptHdr = CHNLSVC.Sales.GetReceiptHdr(txtReceiptNo.Text.Trim().ToUpper())[0];
                selectedReceiptHdr = CHNLSVC.Sales.GetReceiptHdr(_RecNo)[0];
                List<RecieptItem> _paramInvoiceItems = new List<RecieptItem>();
                _paramInvoiceItems = CHNLSVC.Sales.GetAllReceiptItems(_RecNo);
                //if (_paramInvoiceItems.Count < 1)
                //{
                //    MessageBox.Show("No Payment details.");
                //}
                _RecItemList = _paramInvoiceItems;
                gvItems.DataSource = null;
                gvItems.AutoGenerateColumns = false;
                gvItems.DataSource = _RecItemList;


                //load recipt items
                _tmpRecItem = new List<ReceiptItemDetails>();
                _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_RecNo);

                dgvItem.Rows.Clear();

                if (_tmpRecItem != null)
                {
                    foreach (ReceiptItemDetails ser in _tmpRecItem)
                    {
                        dgvItem.Rows.Add();
                        dgvItem["col_itmItem", dgvItem.Rows.Count - 1].Value = ser.Sari_item;
                        dgvItem["col_itmDesc", dgvItem.Rows.Count - 1].Value = ser.Sari_item_desc;
                        dgvItem["col_itmModel", dgvItem.Rows.Count - 1].Value = ser.Sari_model;
                        dgvItem["col_itmStatus", dgvItem.Rows.Count - 1].Value = null;
                        dgvItem["col_itmSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial;
                        dgvItem["col_itmOthSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial_1;
                    }
                }

                //------------------------------------------------
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

        private void txtReceiptNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void txtReceiptNo_Leave(object sender, EventArgs e)
        {
            try
            {
                this.button1_Click(sender, e);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region


                if (gvReceipt.Rows.Count <= 0)
                {

                    return;
                }

                //if (gvItems.Rows.Count <= 0)
                //{
                //    MessageBox.Show("Details are not found.");
                //    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Details are not found.");
                //    return;
                //}


                string _RefundNo;
                string _msg = string.Empty;

                RecieptHeader _ReceiptHeader = new RecieptHeader();
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(selectedReceiptHdr.Sar_receipt_no)[0];

                // List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para(BaseCls.GlbUserComCode, "COM", "ADREFMAXDT");
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", BaseCls.GlbUserComCode);
                Int32 COUNT = 0;
                if (para.Count > 0)
                {
                    //DateTime dt = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(para[0].Hsy_val));
                    DateTime dt = _ReceiptHeader.SAR_VALID_TO;
                    //if (dt<DateTime.Now)
                    if (dt < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        List<string> reqNumList = new List<string>();
                        ApproveRequestUC APPROVE = new ApproveRequestUC();
                        List<RequestApprovalHeader> approvedHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT007.ToString(), 1, 0);
                        //reqNumList = APPROVE.getApprovedReqNumbersList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT009.ToString(), 1, 5);

                        if (approvedHeaders != null)
                        {
                            var _count = from _dup in approvedHeaders
                                         where _dup.Grah_fuc_cd == selectedReceiptHdr.Sar_receipt_no
                                         select _dup;
                            COUNT = _count.Count();
                        }


                        if (COUNT == 0)
                        {
                            if (MessageBox.Show("Cannot Refund.\nExceeds Maximum receipt refund days.\nDo you want to send Refund Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                //send request.
                                //if (lblAccNo.Text.Trim() == string.Empty)
                                //{
                                //    MessageBox.Show("Select an Account first!");
                                //    return;
                                //}

                                //if (txtReques.Text == "" || txtReques.Text == string.Empty)
                                //{
                                //    int defaultAmt = 0;
                                //    txtReques.Text = defaultAmt.ToString();
                                //}
                                //if (chkIsECDrate.Checked == true && Convert.ToDecimal(txtReques.Text.Trim()) > 100)
                                //{
                                //    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "ECD rate cannot be grater than 100!");
                                //    txtReques.Focus();
                                //    MessageBox.Show("ECD rate cannot be grater than 100!");
                                //    return;
                                //}
                                //send custom request.
                                #region fill RequestApprovalHeader

                                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_app_dt = currentDate;//CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                                ra_hdr.Grah_app_stus = "P";
                                ra_hdr.Grah_app_tp = "ARQT007";
                                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdr.Grah_fuc_cd = _ReceiptHeader.Sar_receipt_no;//lblAccNo.Text;
                                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;

                                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;// Convert.ToDateTime(txtReceiptDate.Text);


                                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                                //{
                                //    ra_hdr.Grah_oth_loc = "1";
                                //}
                                //else
                                //{ 
                                ra_hdr.Grah_oth_loc = "0";
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
                                ra_hdr.Grah_remaks = "ADVAN REFUND";

                                #endregion

                                #region fill List<RequestApprovalDetail>
                                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                                ra_det.Grad_ref = ra_hdr.Grah_ref;
                                ra_det.Grad_line = 1;
                                ra_det.Grad_req_param = "ADVAN_REFUND";
                                ra_det.Grad_val1 = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;// _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                                ra_det.Grad_is_rt1 = false;
                                ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
                                ra_det_List.Add(ra_det);
                                #endregion

                                #region fill RequestApprovalHeaderLog

                                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_app_dt = currentDate.Date; //Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdrLog.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                                ra_hdrLog.Grah_app_stus = "P";
                                ra_hdrLog.Grah_app_tp = "ARQT007";
                                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdrLog.Grah_fuc_cd = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
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
                                ra_detLog.Grad_req_param = "ADVAN_REFUND";
                                ra_detLog.Grad_val1 = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; //Convert.ToDecimal(txtReques.Text.Trim());
                                ra_detLog.Grad_is_rt1 = false;
                                ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                                ra_detLog.Grad_date_param = currentDate.Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                                ra_detLog_List.Add(ra_detLog);
                                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                                #endregion
                                string referenceNo;

                                //CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqRequestLevel, GlbReqIsApprovalUser, true, ra_hdr.Grah_ref.ToString());
                                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, true, out referenceNo);

                                if (eff > 0)
                                {
                                    //string Msg = "<script>alert('Request sent!' );</script>";
                                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                    MessageBox.Show("Request sent!");
                                }
                                else
                                {
                                    // string Msg = "<script>alert('Request not sent!' );</script>";
                                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                    MessageBox.Show("Sorry. Request not sent!");
                                }
                                return;
                                //BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
                                //txtReques.Text = "";
                            }
                        }
                    }
                }
                //for (int i = 0; i < gvReceipt.Rows.Count; i++)
                //{
                //    CheckBox chk = (CheckBox)gvReceipt.Rows[i].FindControl("chkselect");

                //    if (chk.Checked == true)
                //    {
                if (MessageBox.Show("Are you sure to Refund?", "Confirm Refund", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //_ReceiptHeader.Sar_seq_no = 1;
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _ReceiptHeader.Sar_receipt_type = "ADREF";
                //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                _ReceiptHeader.Sar_tot_settle_amt = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                //_ReceiptHeader.Sar_receipt_no = "1";
                //_ReceiptHeader.Sar_prefix = gvReceipt.DataKeys[i][1].ToString();
                //_ReceiptHeader.Sar_manual_ref_no = gvReceipt.Rows[i].Cells[6].Text;
                _ReceiptHeader.Sar_receipt_date = currentDate.Date;
                _ReceiptHeader.Sar_direct = false;
                _ReceiptHeader.Sar_acc_no = "";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                //_ReceiptHeader.Sar_debtor_cd = gvReceipt.Rows[i].Cells[13].Text;
                //_ReceiptHeader.Sar_debtor_name = gvReceipt.Rows[i].Cells[14].Text;
                //_ReceiptHeader.Sar_debtor_add_1 = gvReceipt.DataKeys[i][2].ToString();
                //_ReceiptHeader.Sar_debtor_add_2 = gvReceipt.DataKeys[i][3].ToString();
                //_ReceiptHeader.Sar_tel_no = gvReceipt.DataKeys[i][4].ToString();
                //_ReceiptHeader.Sar_mob_no = gvReceipt.DataKeys[i][5].ToString();
                //_ReceiptHeader.Sar_nic_no = gvReceipt.DataKeys[i][6].ToString();
                //_ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][14].ToString());
                //_ReceiptHeader.Sar_comm_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][7].ToString());
                _ReceiptHeader.Sar_is_mgr_iss = false;
                //_ReceiptHeader.Sar_esd_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][8].ToString());
                //_ReceiptHeader.Sar_wht_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][9].ToString());
                //_ReceiptHeader.Sar_epf_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][10].ToString());
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                //_ReceiptHeader.Sar_remarks = gvReceipt.DataKeys[i][11].ToString();
                _ReceiptHeader.Sar_is_used = false;
                //_ReceiptHeader.Sar_ref_doc = gvReceipt.Rows[i].Cells[4].Text;
                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_ReceiptHeader.Sar_anal_1 = gvReceipt.DataKeys[i][12].ToString();
                //_ReceiptHeader.Sar_anal_2 = gvReceipt.DataKeys[i][13].ToString();
                //_ReceiptHeader.Sar_anal_3 = gvReceipt.DataKeys[i][15].ToString();
                //_ReceiptHeader.Sar_anal_4 = "";
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;


                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = _ReceiptHeader.Sar_prefix;
                masterAuto.Aut_year = null;

                #region
                RecieptItem ri = new RecieptItem();
                //ri = _i;
                ri.Sard_settle_amt = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ri.Sard_pay_tp = "CASH";
                ri.Sard_line_no = 1;
                // ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                // ri.Sard_seq_no = _h.Sar_seq_no;                  
                // ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                // ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //  ri.Sard_cc_period = _i.Sard_cc_period;
                //  ri.Sard_cc_tp = _i.Sard_cc_tp;
                // ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                // ri.Sard_chq_branch = _i.Sard_chq_branch;
                // ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                // ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                // ri.Sard_deposit_branch = _i.Sard_deposit_branch;                   
                //  ri.Sard_ref_no = _i.Sard_ref_no;
                // ri.Sard_anal_3 = _i.Sard_anal_3;                               
                // _i.Sard_settle_amt = 0;

                _RecItemList = new List<RecieptItem>();
                _RecItemList.Add(ri);
                #endregion
                if (_tmpRecItem == null)
                {
                    _tmpRecItem = new List<ReceiptItemDetails>();
                    ReceiptItemDetails _re = new ReceiptItemDetails();
                    _re.Sari_item = "";
                    _tmpRecItem.Add(_re);
                }

                int effect = CHNLSVC.Sales.CreateRefund(_ReceiptHeader, _RecItemList, masterAuto, _tmpRecItem, null, out _RefundNo);


                try
                {
                    //TODO: IF APPROVED REQUEST- THEN CHANGE THE HEADER STATUS TO 'F'
                    if (COUNT > 0)
                    {
                        List<RequestApprovalHeader> approvedHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT007.ToString(), 1, 0);
                        if (approvedHeaders != null)
                        {
                            foreach (RequestApprovalHeader appHdr in approvedHeaders)
                            {
                                appHdr.Grah_app_stus = "F";
                                appHdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;
                                appHdr.Grah_app_by = BaseCls.GlbUserID;
                                appHdr.Grah_app_dt = currentDate.Date;
                                CHNLSVC.General.UpdateApprovalStatus(appHdr);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                if (effect == 1)
                {
                    MessageBox.Show("Receipt refund Successfully!\n( Refund Receipt#: " + _RefundNo + " )");
                    //string Msg = "<script>alert('Receipt refund Successfully!');window.location = 'ReceiptReversal.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    //this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg);
                        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                    }
                    else
                    {
                        MessageBox.Show("Creation Failed.");
                        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                    }
                }

                //    }
                //}
                #endregion
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

        private void gvReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    DataGridViewRow row = gvReceipt.Rows[e.RowIndex];
                    string _RecNo = row.Cells[1].Value.ToString();

                    //selectedReceiptHdr = CHNLSVC.Sales.GetReceiptHdr(txtReceiptNo.Text.Trim().ToUpper())[0];
                    selectedReceiptHdr = CHNLSVC.Sales.GetReceiptHdr(_RecNo)[0];
                    List<RecieptItem> _paramInvoiceItems = new List<RecieptItem>();
                    _paramInvoiceItems = CHNLSVC.Sales.GetAllReceiptItems(_RecNo);
                    if (_paramInvoiceItems.Count < 1)
                    {
                        // MessageBox.Show("No Payment details.");
                        MessageBox.Show("No Payment details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    _RecItemList = _paramInvoiceItems;
                    gvItems.DataSource = null;
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = _RecItemList;

                    //load recipt items
                    _tmpRecItem = new List<ReceiptItemDetails>();
                    _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_RecNo);

                    dgvItem.Rows.Clear();

                    if (_tmpRecItem != null)
                    {
                        foreach (ReceiptItemDetails ser in _tmpRecItem)
                        {
                            dgvItem.Rows.Add();
                            dgvItem["col_itmItem", dgvItem.Rows.Count - 1].Value = ser.Sari_item;
                            dgvItem["col_itmDesc", dgvItem.Rows.Count - 1].Value = ser.Sari_item_desc;
                            dgvItem["col_itmModel", dgvItem.Rows.Count - 1].Value = ser.Sari_model;
                            dgvItem["col_itmStatus", dgvItem.Rows.Count - 1].Value = null;
                            dgvItem["col_itmSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial;
                            dgvItem["col_itmOthSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial_1;
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

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InterTransferReceipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbRecTp.Text + seperator + cmbSubType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SatReceipt:
                    {
                        if (_isExpired == false)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbRecTp.Text.Trim());
                            break;
                        }
                        else
                        {

                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "ADVAN");
                            break;
                        }
                    }

                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:
                    {
                        paramsText.Append(txtRegInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuaranceDet:     //Added by darshana on 14/03/2013
                    {
                        paramsText.Append(txtInvInvoice.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNote:     //Added by darshana on 15/05/2013
                    {
                        paramsText.Append(txtCusCode.Text + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.vehregdet:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void txtReceiptNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRecTp.Text))
                {
                    MessageBox.Show("Please select receipt type.", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbRecTp.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferReceipt);
                DataTable _result = CHNLSVC.CommonSearch.GetReceiptsByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true; //Add By Chamal 31/Jan/2014
                _CommonSearch.obj_TragetTextBox = txtReceiptNo;
                _CommonSearch.ShowDialog();
                txtReceiptNo.Focus();
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

        private void txtReceiptNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(cmbRecTp.Text))
                    {
                        MessageBox.Show("Please select receipt type.", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbRecTp.Focus();
                        return;
                    }
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferReceipt);
                    DataTable _result = CHNLSVC.CommonSearch.GetReceiptsByType(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.obj_TragetTextBox = txtReceiptNo;
                    _CommonSearch.ShowDialog();
                    txtReceiptNo.Focus();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clear_cashRefund();
                this.Cursor = Cursors.WaitCursor;

                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();

                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, toolStripLabelBD, string.Empty, out _allowCurrentTrans);
                this.Cursor = Cursors.Default;
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

        private void btnInsOk1_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNo = txtVehInsRec1.Text.ToUpper().Trim();
                DataTable dt = CHNLSVC.Sales.Get_vehinsubyRef(receiptNo);
                grv_VehInsReceipts.DataSource = null;
                grv_VehInsReceipts.AutoGenerateColumns = false;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (BaseCls.GlbUserDefProf != dt.Rows[0]["svit_pc"].ToString())
                        {
                            grv_VehInsReceipts.DataSource = null;
                            MessageBox.Show("Receipt belongs to another profit center!");
                            return;
                        }
                        //string pc= dt.Rows[0][""].ToString();
                    }
                }
                grv_VehInsReceipts.DataSource = dt;

                //COMMENTED FOLLOWING REGION ON 15-03-2013
                #region Grid Binds
                ////---------------------------VEH INSU----------------------------------
                ////Approved list.
                //List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT021.ToString(), 1, GlbReqUserPermissionLevel);
                //ddlApprVehIns_req.Items.Clear();
                //if (_lst != null)
                //{
                //    if (_lst.Count > 0)
                //    {
                //        ddlApprVehIns_req.Items.Clear();
                //        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                //        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                //        query.Select(x => x).Distinct();
                //        ddlApprVehIns_req.DataSource = query.ToList();
                //    }
                //}

                ////Pending list.
                //List<RequestApprovalHeader> p_lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT021.ToString(), 0, GlbReqUserPermissionLevel);
                //List<RequestApprovalHeader> p_lst_ARQT019 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT019", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT019.ToString(), 0, GlbReqUserPermissionLevel);

                //if (p_lst_ARQT019!=null)
                //{
                //    p_lst.AddRange(p_lst_ARQT019);
                //}
                //List<RequestApprovalHeader> p_lst_ARQT017 = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT017", BaseCls.GlbUserID, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT017.ToString(), 0, GlbReqUserPermissionLevel);
                //if (p_lst_ARQT017 != null)
                //{
                //    p_lst.AddRange(p_lst_ARQT017);
                //}
                //ddlPendingVehIns_req.Items.Clear();
                //if (p_lst != null)
                //{
                //    if (p_lst.Count > 0)
                //    {
                //        ddlPendingVehIns_req.Items.Clear();
                //        p_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                //        var query = p_lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                //        query.Select(x => x).Distinct();
                //        ddlPendingVehIns_req.DataSource = query.ToList();
                //    }
                //}


                #endregion Grid Binds

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

        private void txtVehInsRec1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnInsOk1_Click(sender, e);
            }
        }

        private void btnVehInsSendReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (grv_VehInsReceipts.Rows.Count < 1)
                {
                    return;
                }
                GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);
                if (GlbReqUserPermissionLevel == -1)
                {
                    //MessageBox.Show("Approval cycle definition is not setup! Please contact IT department..");
                    //return;
                    GlbReqUserPermissionLevel = 0;
                }

                if (MessageBox.Show("Do you want to send Refund Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";
                foreach (DataGridViewRow row in grv_VehInsReceipts.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        //MessageBox.Show(row.Cells["svit_ref_no"].Value.ToString());
                        receiptNo = row.Cells["svit_ref_no"].Value.ToString();
                        AMOUNT = Convert.ToDecimal(row.Cells["svit_ins_val"].Value.ToString());
                        chasiss = row.Cells["svit_chassis"].Value.ToString();
                        engine = row.Cells["svit_engine"].Value.ToString();
                        invoiceNo = row.Cells["svit_inv_no"].Value.ToString();
                        itemCode = row.Cells["svit_itm_cd"].Value.ToString();
                    }
                }
                if (receiptNo == "")
                {
                    MessageBox.Show("Please select the item first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT021", receiptNo))
                {
                    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //GetRecieptNo

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT021";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = receiptNo;// _ReceiptHeader.Sar_receipt_no;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdr.Grah_oth_loc = "1";
                //}
                //else
                //{ 
                ra_hdr.Grah_oth_loc = "0";
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
                // ra_hdr.Grah_ref = reqNo;//CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "VEH INSURANCE REFUND";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_INSU_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = currentDate.Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT021";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_fuc_cd = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
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
                ra_detLog.Grad_req_param = "VEH_INSU_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                ra_detLog.Grad_is_rt1 = false;
                ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_detLog.Grad_date_param = currentDate.Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                #endregion

                #region request serial detail

                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                #endregion


                #region send request

                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "VIRFREQ";
                _ReqAppAuto.Aut_year = null;
                //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);
                string referenceNo;
                string reqStatus;
                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, _ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);

                //Int32 ef = CHNLSVC.General.Save_RequestApprove_Ser_and_log(ReqApp_ser, GlbReqUserPermissionLevel);
                //if (ef<1)
                //{
                //    MessageBox.Show("Could not save serial details. Request sending failed.","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //    return;
                //}
                //string referenceNo;                       
                //Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
                if (eff > 0)
                {
                    MessageBox.Show("Request sent!\nRequest # :" + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (reqStatus == "A")
                    {
                        MessageBox.Show("Request is also Approved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
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

        private void btnRegOk1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetVehicalRegistrations(txtVehRegRec1.Text.Trim().ToUpper());
                grv_VehRegReceipts.DataSource = null;
                grv_VehRegReceipts.AutoGenerateColumns = false;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (BaseCls.GlbUserDefProf != dt.Rows[0]["svrt_pc"].ToString())
                        {
                            grv_VehRegReceipts.DataSource = null;
                            MessageBox.Show("Receipt belongs to another profit center!");
                            return;
                        }
                        //string pc= dt.Rows[0][""].ToString();
                    }
                }
                grv_VehRegReceipts.DataSource = dt;
                GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);

                //COMMENTED FOLLOWING REGION ON 15-03-2013            
                #region Grid Binds
                ////---------------------------VEH INSU----------------------------------
                ////Approved list.
                //List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehRegRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT020.ToString(), 1, GlbReqUserPermissionLevel);
                //ddlApprVehReg_req.Items.Clear();
                //if (_lst != null)
                //{
                //    if (_lst.Count > 0)
                //    {
                //        ddlApprVehReg_req.Items.Clear();
                //        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                //        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                //        query.Select(x => x).Distinct();
                //        ddlApprVehReg_req.DataSource = query.ToList();
                //    }
                //}

                ////Pending list.
                //List<RequestApprovalHeader> p_lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehRegRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT020.ToString(), 0, GlbReqUserPermissionLevel);
                //ddlPendingVehReg_req.Items.Clear();
                //if (p_lst != null)
                //{
                //    if (p_lst.Count > 0)
                //    {
                //        ddlPendingVehReg_req.Items.Clear();
                //        p_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                //        var query = p_lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                //        query.Select(x => x).Distinct();
                //        ddlPendingVehReg_req.DataSource = query.ToList();
                //    }
                //}
                #endregion Grid Binds
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

        private void btnVehRegSendReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (grv_VehRegReceipts.Rows.Count < 1)
                {
                    return;
                }

                GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
                if (GlbReqUserPermissionLevel == -1)
                {
                    // MessageBox.Show("Approval cycle definition is not setup! Please contact IT department..");
                    // return;
                    GlbReqUserPermissionLevel = 0;
                }

                if (MessageBox.Show("Do you want to send Refund Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";
                foreach (DataGridViewRow row in grv_VehRegReceipts.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        //MessageBox.Show(row.Cells["svit_ref_no"].Value.ToString());
                        receiptNo = row.Cells["svrt_ref_no"].Value.ToString();
                        AMOUNT = Convert.ToDecimal(row.Cells["svrt_reg_val"].Value.ToString());//svrt_reg_val
                        chasiss = row.Cells["svrt_chassis"].Value.ToString();
                        engine = row.Cells["svrt_engine"].Value.ToString();
                        invoiceNo = row.Cells["svrt_inv_no"].Value.ToString();
                        itemCode = row.Cells["srvt_itm_cd"].Value.ToString();
                    }
                }
                if (receiptNo == "")
                {
                    MessageBox.Show("Please select the item first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT020", receiptNo))
                {
                    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //GetRecieptNo

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT020";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = receiptNo;// _ReceiptHeader.Sar_receipt_no;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdr.Grah_oth_loc = "1";
                //}
                //else
                //{ 
                ra_hdr.Grah_oth_loc = "0";
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

                //ra_hdr.Grah_ref = reqNo;//CHNLSVC.Inventory.GetSerialID().ToString(); 
                ra_hdr.Grah_remaks = "VEH REGISTRATION REFUND";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_REG_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = currentDate.Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel; //BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT020";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_fuc_cd = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
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
                ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                ra_detLog.Grad_is_rt1 = false;
                ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_detLog.Grad_date_param = currentDate.Date.AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                #endregion

                #region request serial detail

                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;

                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);

                #endregion

                #region send request

                //Int32 ef = CHNLSVC.General.Save_RequestApprove_Ser_and_log(ReqApp_ser, GlbReqUserPermissionLevel);
                //if (ef < 1)
                //{
                //    MessageBox.Show("Could not save serial details. Request sending failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}        
                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "VRGRREQ";//VIRFREQ CCREGRF
                _ReqAppAuto.Aut_year = null;
                //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);
                string referenceNo;
                string reqStatus;
                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, _ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);
                if (eff > 0)
                {
                    MessageBox.Show("Request sent sucessfullys!\nReference #: " + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
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

        private void txtVehRegRec1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnRegOk1_Click(sender, e);
            }
        }

        private void ddlApprVehIns_req_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grv_VehInsAppReq_Item.DataSource = null;
                if (ddlApprVehIns_req.SelectedIndex != -1)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, ddlApprVehIns_req.SelectedItem.ToString(), out List);

                    grv_VehInsAppReq_Item.AutoGenerateColumns = false;
                    grv_VehInsAppReq_Item.DataSource = List;
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

        private void btnSaveVehInsRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                #region
                if (grv_VehInsAppReq_Item.Rows.Count < 1)
                {
                    return;
                }
                // GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);
                string request_No = ddlApprVehIns_req.SelectedItem.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                //-----------------------------check constraints-----------------------------------------------------------------------------
                if (REQ_HEADER.Grah_app_tp == "ARQT017")
                {
                    string org_reqNo = REQ_HEADER.Grah_fuc_cd;
                    RequestApprovalHeader orgiginal_REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, org_reqNo);
                    if (orgiginal_REQ_HEADER == null)
                    {
                        MessageBox.Show("Sale is not reversed yet.\nPlease send a sale reversal request!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (orgiginal_REQ_HEADER.Grah_app_stus != "F")
                    {
                        //check wheather the sale is reversed? header=F
                        MessageBox.Show("Sale is not reversed yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (REQ_HEADER.Grah_app_tp == "ARQT019")
                {
                    string accountNo = REQ_HEADER.Grah_fuc_cd;
                    //TODO:
                    HPAccountLog accLog_finalRecord = CHNLSVC.Sales.GetAccountLog_LatestRecord(accountNo);
                    if (accLog_finalRecord != null)
                    {
                        if (accLog_finalRecord.Hal_sa_sub_tp != "CC")
                        {
                            MessageBox.Show("Not Cash Converted yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not Cash Converted yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                //---------------------------------------------------------------------------------------------------------------------------

                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";

                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehInsAppReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["Gras_anal5"].Value.ToString();
                        AMOUNT = Convert.ToDecimal(row.Cells["Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["Gras_anal4"].Value.ToString();
                        engine = row.Cells["Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //check the constraints
                DataTable dt = CHNLSVC.Sales.Get_vehinsubyRef(receiptNo);
                foreach (DataRow row in dt.Rows)
                {
                    var _duplicate1 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("svit_itm_cd") == itemCode && _dup.Field<string>("svit_chassis") == chasiss && _dup.Field<string>("svit_engine") == engine && _dup.Field<Int16>("SVIT_CVNT_ISSUE") == 2
                                      select _dup;
                    if (_duplicate1.Count() > 0)
                    {
                        MessageBox.Show("This Item cannot Refund. (Item already refunded!)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var _duplicate2 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("svit_itm_cd") == itemCode && _dup.Field<string>("svit_chassis") == chasiss && _dup.Field<string>("svit_engine") == engine && _dup.Field<Int16>("SVIT_POLC_STUS") != 0
                                      select _dup;
                    if (_duplicate2.Count() > 0)
                    {
                        MessageBox.Show("This Item cannot Refund. (Policy assigned)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var _duplicate3 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("svit_itm_cd") == itemCode && _dup.Field<string>("svit_chassis") == chasiss && _dup.Field<string>("svit_engine") == engine
                                      select _dup;
                    if (_duplicate3.Count() < 1)
                    {
                        MessageBox.Show("Item not found. (Not in insurance transactions)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //----------------------------------------------------------------------------------------------------------------------------------------
                if (MessageBox.Show("Are you sure to Refund ?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                #endregion
                RecieptHeader _ReceiptHeader = new RecieptHeader();
                // _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(txtVehInsRec1.Text.Trim().ToUpper())[0];           
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(receiptNo)[0];

                string ORG_RECEIPT_NO = _ReceiptHeader.Sar_anal_3;
                //_ReceiptHeader.Sar_seq_no = 1;
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _ReceiptHeader.Sar_receipt_type = "VHINSRF";
                //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                _ReceiptHeader.Sar_tot_settle_amt = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                //_ReceiptHeader.Sar_receipt_no = "1";
                //_ReceiptHeader.Sar_prefix = gvReceipt.DataKeys[i][1].ToString();
                _ReceiptHeader.Sar_manual_ref_no = request_No;//gvReceipt.Rows[i].Cells[6].Text;
                _ReceiptHeader.Sar_receipt_date = currentDate;
                _ReceiptHeader.Sar_direct = false;
                _ReceiptHeader.Sar_acc_no = "";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                //_ReceiptHeader.Sar_debtor_cd = gvReceipt.Rows[i].Cells[13].Text;
                //_ReceiptHeader.Sar_debtor_name = gvReceipt.Rows[i].Cells[14].Text;
                //_ReceiptHeader.Sar_debtor_add_1 = gvReceipt.DataKeys[i][2].ToString();
                //_ReceiptHeader.Sar_debtor_add_2 = gvReceipt.DataKeys[i][3].ToString();
                //_ReceiptHeader.Sar_tel_no = gvReceipt.DataKeys[i][4].ToString();
                //_ReceiptHeader.Sar_mob_no = gvReceipt.DataKeys[i][5].ToString();
                //_ReceiptHeader.Sar_nic_no = gvReceipt.DataKeys[i][6].ToString();
                //_ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][14].ToString());
                //_ReceiptHeader.Sar_comm_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][7].ToString());
                _ReceiptHeader.Sar_is_mgr_iss = false;
                //_ReceiptHeader.Sar_esd_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][8].ToString());
                //_ReceiptHeader.Sar_wht_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][9].ToString());
                //_ReceiptHeader.Sar_epf_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][10].ToString());
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                //_ReceiptHeader.Sar_remarks = gvReceipt.DataKeys[i][11].ToString();
                _ReceiptHeader.Sar_is_used = false;

                _ReceiptHeader.Sar_ref_doc = receiptNo;

                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_ReceiptHeader.Sar_anal_1 = gvReceipt.DataKeys[i][12].ToString();
                //_ReceiptHeader.Sar_anal_2 = gvReceipt.DataKeys[i][13].ToString();
                _ReceiptHeader.Sar_anal_3 = ORG_RECEIPT_NO;//gvReceipt.DataKeys[i][15].ToString();
                //_ReceiptHeader.Sar_anal_4 = "";
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;


                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = 0;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "VHINSRF";
                masterAuto.Aut_year = null;

                RecieptItem ri = new RecieptItem();
                //ri = _i;
                ri.Sard_settle_amt = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ri.Sard_pay_tp = "CASH";
                ri.Sard_line_no = 1;
                // ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                // ri.Sard_seq_no = _h.Sar_seq_no;                  
                // ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                // ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //  ri.Sard_cc_period = _i.Sard_cc_period;
                //  ri.Sard_cc_tp = _i.Sard_cc_tp;
                // ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                // ri.Sard_chq_branch = _i.Sard_chq_branch;
                // ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                // ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                // ri.Sard_deposit_branch = _i.Sard_deposit_branch;                   
                ri.Sard_ref_no = receiptNo;
                // ri.Sard_anal_3 = _i.Sard_anal_3;                               
                // _i.Sard_settle_amt = 0;

                _RecItemList = new List<RecieptItem>();
                _RecItemList.Add(ri);

                string GenReceiptNo = "";
                Int32 eff = CHNLSVC.Sales.Refund_Insurance(_ReceiptHeader, ri, masterAuto, receiptNo, itemCode, chasiss, engine, BaseCls.GlbUserID, currentDate, out GenReceiptNo);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Refunded!\nReceipt No: " + GenReceiptNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Failed to refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void ddlPendingVehIns_req_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grv_VehInsPendingReq_Item.DataSource = null;
                if (ddlPendingVehIns_req.SelectedIndex != -1)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, ddlPendingVehIns_req.SelectedItem.ToString(), out List);

                    grv_VehInsPendingReq_Item.AutoGenerateColumns = false;
                    grv_VehInsPendingReq_Item.DataSource = List;
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

        private void btnVehInsApproveReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (grv_VehInsPendingReq_Item.Rows.Count < 1)
                {
                    return;
                }
                // GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);

                string request_No = ddlPendingVehIns_req.SelectedItem.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                if (BaseCls.GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }
                if (REQ_HEADER.Grah_app_lvl == BaseCls.GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has approved already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";


                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehInsPendingReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["p_Gras_anal5"].Value.ToString();
                        AMOUNT = AMOUNT + Convert.ToDecimal(row.Cells["p_Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["p_Gras_anal4"].Value.ToString();
                        engine = row.Cells["p_Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["p_Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["p_Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure to approve ?", "Confirm approve", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //RequestApprovalHeader _hdr = new RequestApprovalHeader();
                //List<RequestApprovalDetail> _det = new List<RequestApprovalDetail>();
                //RequestApprovalHeaderLog _hdrlog= new RequestApprovalHeaderLog();
                //List<RequestApprovalDetailLog> _delLog = new List<RequestApprovalDetailLog>();

                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = request_No;
                /* COMMENT
                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT021";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = receiptNo;// _ReceiptHeader.Sar_receipt_no;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdr.Grah_oth_loc = "1";
                //}
                //else
                //{ 
                ra_hdr.Grah_oth_loc = "0";
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
                ra_hdr.Grah_ref = request_No;
                ra_hdr.Grah_remaks = "VEH INSURANCE REFUND";
                */
                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_INSU_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
                ra_det_List.Add(ra_det);
                /*  COMMENT
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_INSU_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);
                ra_det_List.Add(ra_det);
                */
                #endregion

                #region fill RequestApprovalHeaderLog
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
                ra_hdrLog.Grah_ref = request_No;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                //-----------------------------------------------------------------------
                /* COMMENT
                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT021";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_fuc_cd = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
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
                */
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                List<RequestApprovalDetailLog> ra_detLog_List_old = CHNLSVC.General.GetRequestApprovalDetailLog(request_No);

                if (ra_detLog_List_old != null)
                {

                }
                else
                {
                    ra_detLog_List_old = new List<RequestApprovalDetailLog>();
                    RequestApprovalDetailLog log = new RequestApprovalDetailLog();
                    log.Grad_ref = request_No;
                    ra_detLog_List_old.Add(log);
                }

                RequestApprovalDetailLog ra_detLog = ra_detLog_List_old[0];


                //ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                //ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                //ra_detLog.Grad_is_rt1 = false;
                //ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                //ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);
                //REQ_HEADER

                /* COMMENT
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "VEH_INSU_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                ra_detLog.Grad_is_rt1 = false;
                ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);            
                */
                #endregion

                #region request serial detail
                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                /* COMMENT
                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                */
                #endregion
                string referenceNo;
                string reqStatus;

                // GlbReqIsFinalApprovalUser = true;
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);
                // Int32 eff= CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList,null,ra_hdr,ra_det_List,ra_hdrLog,ra_detLog_List,BaseCls.
                if (eff > 0)
                {

                    if (reqStatus == "A")
                    {
                        MessageBox.Show("Request Approved Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Request Updated Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void set_approveUser_infor(string app_tp_code)
        {
            if (app_tp_code == "ARQT020")//VEHICLE REGISTRATION RECEIPT REFUND
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
            }
            if (app_tp_code == "ARQT021")//VEHICLE INSURANCE RECEIPT REFUND
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);
            }
            if (app_tp_code == "ARQT019")//CONVERTED INVOICE VEH. INSURANCE REFUND -ARQT019
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.CCINSRF);
            }
            if (app_tp_code == "ARQT018")//CONVERTED INVOICE REGISTRATION REFUND -ARQT018
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.CCREGRF);
            }
            if (app_tp_code == "ARQT017")//REVESED INVOICE VEH. INSURANCE REFUND -ARQT017
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.CSINSRF);
            }
            if (app_tp_code == "ARQT016")//REVESED INVOICE REGISTRATION REFUND -ARQT016
            {
                GetUserAppLevel(HirePurchasModuleApprovalCode.CSREGRF);
            }
        }

        private void btnVehRegApproveReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (grv_VehRegPendingReq_Item.Rows.Count < 1)
                {
                    return;
                }
                string request_No = ddlPendingVehReg_req.SelectedItem.ToString();

                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);

                set_approveUser_infor(REQ_HEADER.Grah_app_tp);
                //GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
                if (BaseCls.GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }
                if (REQ_HEADER.Grah_app_lvl == BaseCls.GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has approved already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                GlbReqUserPermissionLevel = BaseCls.GlbReqUserPermissionLevel;

                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";


                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehRegPendingReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["prg_Gras_anal5"].Value.ToString();
                        AMOUNT = AMOUNT + Convert.ToDecimal(row.Cells["prg_Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["prg_Gras_anal4"].Value.ToString();
                        engine = row.Cells["prg_Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["prg_Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["prg_Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure to approve ?", "Confirm approve", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = request_No;
                /*
                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT020";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = receiptNo;// _ReceiptHeader.Sar_receipt_no;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                //{
                //    ra_hdr.Grah_oth_loc = "1";
                //}
                //else
                //{ 
                ra_hdr.Grah_oth_loc = "0";
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
                ra_hdr.Grah_ref = request_No;
                ra_hdr.Grah_remaks = "VEH REGISTRATION REFUND";
                */
                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_REG_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
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
                ra_hdrLog.Grah_ref = request_No;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                //-----------------------------------------------------------------------
                #region comment
                /*
            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//BaseCls.GlbReqUserPermissionLevel;//not sure
            ra_hdrLog.Grah_app_stus = "P";
            ra_hdrLog.Grah_app_tp = "ARQT020";
            ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
            ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;
            ra_hdrLog.Grah_fuc_cd = receiptNo;
            ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;
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
            ra_hdrLog.Grah_remaks = "VEH REGISTRATION REFUND";
            */
                #endregion comment
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                List<RequestApprovalDetailLog> ra_detLog_List_old = CHNLSVC.General.GetRequestApprovalDetailLog(request_No);


                if (ra_detLog_List_old != null)
                {

                }
                else
                {
                    ra_detLog_List_old = new List<RequestApprovalDetailLog>();
                    RequestApprovalDetailLog log = new RequestApprovalDetailLog();
                    log.Grad_ref = request_No;
                    ra_detLog_List_old.Add(log);
                }


                RequestApprovalDetailLog ra_detLog = ra_detLog_List_old[0];

                //ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                //ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                //ra_detLog.Grad_is_rt1 = false;
                //ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                //ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);
                //REQ_HEADER

                /*
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                ra_detLog.Grad_is_rt1 = false;
                ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);
                */
                #endregion

                #region request serial detail

                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                #endregion

                string referenceNo;
                string reqStatus;

                //GlbReqIsFinalApprovalUser = true;
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);
                // Int32 eff= CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList,null,ra_hdr,ra_det_List,ra_hdrLog,ra_detLog_List,BaseCls.
                if (eff > 0)
                {

                    if (reqStatus == "A")
                    {
                        MessageBox.Show("Request Approved Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Request Updated Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ddlPendingVehReg_req_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grv_VehRegPendingReq_Item.DataSource = null;
                if (ddlPendingVehReg_req.SelectedIndex != -1)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, ddlPendingVehReg_req.SelectedItem.ToString(), out List);

                    grv_VehRegPendingReq_Item.AutoGenerateColumns = false;
                    grv_VehRegPendingReq_Item.DataSource = List;
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
        private void ddlApprVehReg_req_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grv_VehRegAppReq_Item.DataSource = null;
                if (ddlApprVehReg_req.SelectedIndex != -1)
                {
                    List<RequestApprovalSerials> List = new List<RequestApprovalSerials>();
                    DataTable dt = CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, ddlApprVehReg_req.SelectedItem.ToString(), out List);

                    grv_VehRegAppReq_Item.AutoGenerateColumns = false;
                    grv_VehRegAppReq_Item.DataSource = List;
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
        private void btnSaveVehRegRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                #region
                if (grv_VehRegAppReq_Item.Rows.Count < 1)
                {
                    return;
                }
                //GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
                string request_No = ddlApprVehReg_req.SelectedItem.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                //-----------------------------check constraints-----------------------------------------------------------------------------
                if (REQ_HEADER.Grah_app_tp == "ARQT016")
                {
                    string org_reqNo = REQ_HEADER.Grah_fuc_cd;
                    RequestApprovalHeader orgiginal_REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, org_reqNo);
                    if (orgiginal_REQ_HEADER == null)
                    {
                        MessageBox.Show("Sale is not reversed yet.\nPlease send a sale reversal request!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (orgiginal_REQ_HEADER.Grah_app_stus != "F")
                    {
                        //check wheather the sale is reversed? header=F
                        MessageBox.Show("Sale is not reversed yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                string accountNo = "";
                if (REQ_HEADER.Grah_app_tp == "ARQT018")
                {
                    accountNo = REQ_HEADER.Grah_fuc_cd;
                    //TODO:
                    HPAccountLog accLog_finalRecord = CHNLSVC.Sales.GetAccountLog_LatestRecord(accountNo);
                    if (accLog_finalRecord != null)
                    {
                        if (accLog_finalRecord.Hal_sa_sub_tp != "CC")
                        {
                            MessageBox.Show("Not Cash Converted yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not Cash Converted yet. Cannot continue!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                //---------------------------------------------------------------------------------------------------------------------------
                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";

                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehRegAppReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["rg_Gras_anal5"].Value.ToString();
                        AMOUNT = AMOUNT + Convert.ToDecimal(row.Cells["rg_Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["rg_Gras_anal4"].Value.ToString();
                        engine = row.Cells["rg_Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["rg_Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["rg_Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //check the constraints
                DataTable dt = CHNLSVC.Sales.Get_vehRegbyref(receiptNo);
                foreach (DataRow row in dt.Rows)
                {
                    var _duplicate1 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("SRVT_ITM_CD") == itemCode && _dup.Field<string>("SVRT_CHASSIS") == chasiss && _dup.Field<string>("SVRT_ENGINE") == engine && _dup.Field<Int16>("SVRT_PRNT_STUS") == 2
                                      select _dup;
                    if (_duplicate1.Count() > 0)
                    {
                        MessageBox.Show("This Item cannot Refund. (Item already refunded!)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var _duplicate2 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("SRVT_ITM_CD") == itemCode && _dup.Field<string>("SVRT_CHASSIS") == chasiss && _dup.Field<string>("SVRT_ENGINE") == engine && _dup.Field<Int16>("SRVT_RMV_STUS") != 0
                                      select _dup;
                    //commented on 06-08-2013
                    //if (_duplicate2.Count() > 0)
                    //{
                    //    MessageBox.Show("This Item cannot Refund. (Policy assigned. RMV documents not received)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    var _duplicate3 = from _dup in dt.AsEnumerable()
                                      where _dup.Field<string>("SRVT_ITM_CD") == itemCode && _dup.Field<string>("SVRT_CHASSIS") == chasiss && _dup.Field<string>("SVRT_ENGINE") == engine
                                      select _dup;
                    if (_duplicate3.Count() < 1)
                    {
                        MessageBox.Show("Item not found. (Not in insurance transactions)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //----------------------------------------------------------------------------------------------------------------------------------------
                if (MessageBox.Show("Are you sure to Refund ?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                #endregion

                RecieptHeader _ReceiptHeader = new RecieptHeader();
                // _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(txtVehRegRec1.Text.Trim().ToUpper())[0];
                //receiptNo
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(receiptNo)[0];
                //_ReceiptHeader.Sar_seq_no = 1;

                string ORG_RECEIPT_NO = _ReceiptHeader.Sar_anal_3;
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _ReceiptHeader.Sar_receipt_type = "VHREGRF";
                //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                _ReceiptHeader.Sar_tot_settle_amt = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                //_ReceiptHeader.Sar_receipt_no = "1";
                //_ReceiptHeader.Sar_prefix = gvReceipt.DataKeys[i][1].ToString();
                _ReceiptHeader.Sar_manual_ref_no = request_No;//gvReceipt.Rows[i].Cells[6].Text;
                _ReceiptHeader.Sar_receipt_date = currentDate;
                _ReceiptHeader.Sar_direct = false;
                _ReceiptHeader.Sar_acc_no = accountNo;//"";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                //_ReceiptHeader.Sar_debtor_cd = gvReceipt.Rows[i].Cells[13].Text;
                //_ReceiptHeader.Sar_debtor_name = gvReceipt.Rows[i].Cells[14].Text;
                //_ReceiptHeader.Sar_debtor_add_1 = gvReceipt.DataKeys[i][2].ToString();
                //_ReceiptHeader.Sar_debtor_add_2 = gvReceipt.DataKeys[i][3].ToString();
                //_ReceiptHeader.Sar_tel_no = gvReceipt.DataKeys[i][4].ToString();
                //_ReceiptHeader.Sar_mob_no = gvReceipt.DataKeys[i][5].ToString();
                //_ReceiptHeader.Sar_nic_no = gvReceipt.DataKeys[i][6].ToString();
                //_ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][14].ToString());
                //_ReceiptHeader.Sar_comm_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][7].ToString());
                _ReceiptHeader.Sar_is_mgr_iss = false;
                //_ReceiptHeader.Sar_esd_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][8].ToString());
                //_ReceiptHeader.Sar_wht_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][9].ToString());
                //_ReceiptHeader.Sar_epf_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][10].ToString());
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                //_ReceiptHeader.Sar_remarks = gvReceipt.DataKeys[i][11].ToString();
                _ReceiptHeader.Sar_is_used = false;

                _ReceiptHeader.Sar_ref_doc = receiptNo;

                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_ReceiptHeader.Sar_anal_1 = gvReceipt.DataKeys[i][12].ToString();
                //_ReceiptHeader.Sar_anal_2 = gvReceipt.DataKeys[i][13].ToString();
                _ReceiptHeader.Sar_anal_3 = ORG_RECEIPT_NO;
                //_ReceiptHeader.Sar_anal_4 = "";
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;


                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = 0;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "VHREGRF";
                masterAuto.Aut_year = null;

                RecieptItem ri = new RecieptItem();
                //ri = _i;
                ri.Sard_settle_amt = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ri.Sard_pay_tp = "CASH";
                ri.Sard_line_no = 1;
                // ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                // ri.Sard_seq_no = _h.Sar_seq_no;                  
                // ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                // ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //  ri.Sard_cc_period = _i.Sard_cc_period;
                //  ri.Sard_cc_tp = _i.Sard_cc_tp;
                // ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                // ri.Sard_chq_branch = _i.Sard_chq_branch;
                // ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                // ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                // ri.Sard_deposit_branch = _i.Sard_deposit_branch;                   
                ri.Sard_ref_no = receiptNo;
                // ri.Sard_anal_3 = _i.Sard_anal_3;                               
                // _i.Sard_settle_amt = 0;

                _RecItemList = new List<RecieptItem>();
                _RecItemList.Add(ri);

                string GenReceiptNo = "";

                Int32 eff = CHNLSVC.Sales.Refund_Registration(_ReceiptHeader, ri, masterAuto, receiptNo, itemCode, chasiss, engine, BaseCls.GlbUserID, currentDate, out GenReceiptNo);
                try
                {
                    if (eff > 0)
                    {
                        CHNLSVC.Inventory.UpdateRegistrationRefundSerials(BaseCls.GlbUserComCode, invoiceNo, BaseCls.GlbUserDefProf, itemCode, engine, chasiss, 1);
                    }
                }
                catch (Exception ex)
                {

                }

                if (eff > 0)
                {
                    MessageBox.Show("Successfully Refunded!\nReceipt No: " + GenReceiptNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Failed to refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnInsRecSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef);
                DataTable _result = CHNLSVC.CommonSearch.GetVehicalInsuranceRef(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVehInsRec1;
                _CommonSearch.txtSearchbyword.Text = txtVehInsRec1.Text;
                _CommonSearch.ShowDialog();
                txtVehInsRec1.Focus();
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

        private void btnRegSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRegInvoice.Text))
                {
                    MessageBox.Show("Please enter invoice# first!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVehRegRec2;
                _CommonSearch.ShowDialog();
                txtVehRegRec2.Select();
                txtVehRegRec1.Text = txtVehRegRec2.Text;
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

        private void btnRegOk2_Click(object sender, EventArgs e)
        {
            txtVehRegRec1.Text = txtVehRegRec2.Text;
            this.btnRegOk1_Click(sender, e);
        }

        private void txtVehRegRec1_DoubleClick(object sender, EventArgs e)
        {
            this.ImgRegSearch1_Click(sender, e);
        }

        private void ImgRegSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.vehregdet);
                DataTable _result = CHNLSVC.CommonSearch.Search_veh_reg_ref(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtVehRegRec1;
                _CommonSearch.ShowDialog();
                //txtVehRegRec1.Focus();
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

        private void btnInsRecSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, _searchCatergory, _searchText);
                //dvResult.DataSource = _result;
                //break;

                if (string.IsNullOrEmpty(txtInvInvoice.Text))
                {
                    MessageBox.Show("Please enter invoice# first!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVehInsRec2;
                _CommonSearch.ShowDialog();
                txtVehInsRec2.Select();
                txtVehInsRec1.Text = txtVehInsRec2.Text;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtVehInsRec1.Text = txtVehInsRec2.Text;
            this.btnInsOk1_Click(sender, e);
        }

        private void txtVehInsRec2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.button1_Click_1(sender, e);
        }

        private void txtInvInvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtVehInsRec2.Focus();
        }

        private void btnVehRegRejectReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (grv_VehRegPendingReq_Item.Rows.Count < 1)
                {
                    return;
                }
                string request_No = ddlPendingVehReg_req.SelectedItem.ToString();

                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);

                set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                if (GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }
                if (REQ_HEADER.Grah_app_lvl == GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has approved already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";


                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehRegPendingReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["prg_Gras_anal5"].Value.ToString();
                        AMOUNT = AMOUNT + Convert.ToDecimal(row.Cells["prg_Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["prg_Gras_anal4"].Value.ToString();
                        engine = row.Cells["prg_Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["prg_Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["prg_Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (MessageBox.Show("Are you sure to Reject the Request " + request_No + " ?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                // RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                // set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = request_No;

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_REG_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
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
                ra_hdrLog.Grah_ref = request_No;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                //-----------------------------------------------------------------------
                #region comment

                #endregion comment
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                List<RequestApprovalDetailLog> ra_detLog_List_old = CHNLSVC.General.GetRequestApprovalDetailLog(request_No);
                RequestApprovalDetailLog ra_detLog = ra_detLog_List_old[0];

                //ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                //ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                //ra_detLog.Grad_is_rt1 = false;
                //ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                //ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);
                #endregion

                #region request serial detail

                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                #endregion

                string referenceNo;
                string reqStatus;
                //GlbReqIsFinalApprovalUser = true;
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);
                //-----------------------------------------------------------------------------------------------------------------           
                if (eff > 0)
                {
                    Int32 effF = 0;
                    if (GlbReqIsFinalApprovalUser == true)
                    {
                        REQ_HEADER.Grad_ref = request_No;
                        REQ_HEADER.Grah_app_by = BaseCls.GlbUserID;
                        REQ_HEADER.Grah_app_dt = currentDate;
                        REQ_HEADER.Grah_app_lvl = GlbReqUserPermissionLevel;
                        REQ_HEADER.Grah_app_stus = "R";

                        effF = CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER);
                        if (effF > 0)
                        {
                            MessageBox.Show("Successfully Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.btnClear_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Not Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnClear_Click(sender, e);
                    }
                    //--------------------------------------------------------                          
                }
                else
                {
                    MessageBox.Show("Not Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (grv_VehInsPendingReq_Item.Rows.Count < 1)
                {
                    return;
                }
                // GetUserAppLevel(HirePurchasModuleApprovalCode.VHINSRF);

                string request_No = ddlPendingVehIns_req.SelectedItem.ToString();
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                if (GlbReqUserPermissionLevel == -1)
                {
                    MessageBox.Show("Approval cycle definition is not setup for you! Please contact IT department..");
                    return;
                }
                if (REQ_HEADER.Grah_app_lvl == GlbReqUserPermissionLevel)
                {
                    MessageBox.Show("Same level user has approved already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string receiptNo = "";
                Decimal AMOUNT = 0;
                string chasiss = "";
                string engine = "";
                string invoiceNo = "";
                string itemCode = "";


                Int32 count = 0;
                foreach (DataGridViewRow row in grv_VehInsPendingReq_Item.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    bool isChecked = Convert.ToBoolean(chk.Value);
                    if (isChecked)
                    {
                        count = count + 1;
                        receiptNo = row.Cells["p_Gras_anal5"].Value.ToString();
                        AMOUNT = AMOUNT + Convert.ToDecimal(row.Cells["p_Gras_anal6"].Value.ToString());
                        chasiss = row.Cells["p_Gras_anal4"].Value.ToString();
                        engine = row.Cells["p_Gras_anal3"].Value.ToString();
                        invoiceNo = row.Cells["p_Gras_anal1"].Value.ToString();
                        itemCode = row.Cells["p_Gras_anal2"].Value.ToString();
                    }
                }
                if (count > 1)
                {
                    MessageBox.Show("Select only one item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0)
                {
                    MessageBox.Show("Please select an item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure to Reject the Request " + request_No + "?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                //--------------------------------------------------------------------------         
                // RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
                //set_approveUser_infor(REQ_HEADER.Grah_app_tp);

                #region fill RequestApprovalHeader
                RequestApprovalHeader ra_hdr = REQ_HEADER;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_ref = request_No;

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "VEH_INSU_REFUND";
                ra_det.Grad_val1 = AMOUNT;//selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ra_det.Grad_is_rt1 = false;
                ra_det.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no; 
                ra_det.Grad_anal2 = chasiss;
                ra_det.Grad_anal3 = engine;
                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
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
                ra_hdrLog.Grah_ref = request_No;//REQ_HEADER.Grad_ref;
                ra_hdrLog.Grah_remaks = REQ_HEADER.Grah_remaks;
                //-----------------------------------------------------------------------
                #region comment

                #endregion comment
                #endregion

                #region fill List<RequestApprovalDetailLog>
                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                List<RequestApprovalDetailLog> ra_detLog_List_old = CHNLSVC.General.GetRequestApprovalDetailLog(request_No);
                RequestApprovalDetailLog ra_detLog = ra_detLog_List_old[0];

                //ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                //ra_detLog.Grad_req_param = "VEH_REG_REFUND";//"ADVAN_REFUND";
                ra_detLog.Grad_val1 = AMOUNT; //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; 
                //ra_detLog.Grad_is_rt1 = false;
                //ra_detLog.Grad_anal1 = receiptNo;//_ReceiptHeader.Sar_receipt_no;
                //ra_detLog.Grad_date_param = CHNLSVC.Security.GetServerDateTime().Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_detLog);
                #endregion

                #region request serial detail

                int _line = 1;
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                _tempReqAppSer.Gras_line = _line;
                _tempReqAppSer.Gras_anal1 = invoiceNo;//ser.P_svrt_inv_no;
                _tempReqAppSer.Gras_anal2 = itemCode;//ser.P_srvt_itm_cd;
                _tempReqAppSer.Gras_anal3 = engine;//ser.P_svrt_engine;
                _tempReqAppSer.Gras_anal4 = chasiss;//ser.P_svrt_chassis;
                _tempReqAppSer.Gras_anal5 = receiptNo;//ser.P_srvt_ref_no;
                _tempReqAppSer.Gras_anal6 = AMOUNT;//ser.P_svrt_reg_val;
                _tempReqAppSer.Gras_anal7 = 0;
                _tempReqAppSer.Gras_anal8 = 0;
                _tempReqAppSer.Gras_anal9 = 0;
                _tempReqAppSer.Gras_anal10 = 0;
                //_ReqRegSer.Add(_tempReqAppSer);
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                ReqApp_serList.Add(_tempReqAppSer);
                #endregion

                string referenceNo;
                string reqStatus;
                //GlbReqIsFinalApprovalUser = true;
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);
                //-----------------------------------------------------------------------------------------------------------------           
                if (eff > 0)
                {
                    Int32 effF = 0;
                    if (GlbReqIsFinalApprovalUser == true)
                    {
                        REQ_HEADER.Grad_ref = request_No;
                        REQ_HEADER.Grah_app_by = BaseCls.GlbUserID;
                        REQ_HEADER.Grah_app_dt = currentDate;
                        REQ_HEADER.Grah_app_lvl = GlbReqUserPermissionLevel;
                        REQ_HEADER.Grah_app_stus = "R";

                        effF = CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER);
                        if (effF > 0)
                        {
                            MessageBox.Show("Successfully Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.btnClear_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Not Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnClear_Click(sender, e);
                    }
                    //--------------------------------------------------------                          
                }
                else
                {
                    MessageBox.Show("Not Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /*
                if (MessageBox.Show("Are you sure to Reject the Request " + request_No + "?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            
                REQ_HEADER.Grad_ref = request_No;
                REQ_HEADER.Grah_app_by = BaseCls.GlbUserID;
                REQ_HEADER.Grah_app_dt = currentDate;
                REQ_HEADER.Grah_app_lvl = GlbReqUserPermissionLevel;
                REQ_HEADER.Grah_app_stus = "R";

          
                Int32 eff= CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER);
                if (eff>0)
                {
                    MessageBox.Show("Successfully Rejected!");
                    this.btnClear_Click(sender, e);
                }
                */
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

        private void btnSave__Click(object sender, EventArgs e)
        {
            try
            {
                #region
                if (IsBackDateOk() == false)
                {
                    return;
                }

                if (gvReceipt.Rows.Count <= 0)
                {

                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                //if (gvItems.Rows.Count <= 0)
                //{
                //    MessageBox.Show("Details are not found.");
                //    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Details are not found.");
                //    return;
                //}
                try
                {
                    if (!string.IsNullOrEmpty(selectedReceiptHdr.Sar_manual_ref_no))
                    {
                        Decimal amt = CHNLSVC.Sales.AvailableForRefund(selectedReceiptHdr.Sar_manual_ref_no);
                        if (amt <= 0)
                        {
                            MessageBox.Show("Already Refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                    //MessageBox.Show(ex.Message);
                }

                string _RefundNo;
                string _msg = string.Empty;

                if (selectedReceiptHdr.Sar_receipt_no == null)
                {
                    MessageBox.Show("Please select required receipt.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                    return;
                }

                RecieptHeader _ReceiptHeader = new RecieptHeader();
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHdr(selectedReceiptHdr.Sar_receipt_no)[0];

                //DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(BaseCls.GlbUserComCode, _ReceiptHeader.Sar_receipt_no);
                //if (_adv != null && _adv.Rows.Count > 0)
                //{
                //    MessageBox.Show("This advance receipt is already picked for a inter-transfer. You are not allow to refund.", "Receipt Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.Cursor = Cursors.Default;
                //    return;
                //}

                if (_ReceiptHeader.Sar_receipt_type != cmbRecTp.Text.Trim())
                {
                    MessageBox.Show(_ReceiptHeader.Sar_receipt_no + " is not " + cmbRecTp.Text + " Receipt!");
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (_ReceiptHeader.Sar_tot_settle_amt == _ReceiptHeader.Sar_used_amt)
                {
                    MessageBox.Show(_ReceiptHeader.Sar_receipt_no + " is fully used. Cannot refund!");
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (_ReceiptHeader.Sar_act == false)
                {
                    MessageBox.Show(_ReceiptHeader.Sar_receipt_no + " is an In-Active receipt!");//cancelled receipt.
                    this.Cursor = Cursors.Default;
                    return;
                }



                // List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para(BaseCls.GlbUserComCode, "COM", "ADREFMAXDT");
                List<Hpr_SysParameter> para = new List<Hpr_SysParameter>();
                if (_ReceiptHeader.Sar_receipt_type == "ADVAN")
                {
                    para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", BaseCls.GlbUserComCode);
                }
                else if (_ReceiptHeader.Sar_receipt_type == "ADINS")
                {
                    para = CHNLSVC.Sales.GetAll_hpr_Para("ADINREMXDT", "COM", BaseCls.GlbUserComCode);
                }
                else
                {
                    MessageBox.Show("Parameter not setup", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);//cancelled receipt.
                    this.Cursor = Cursors.Default;
                    return;
                }


                Int32 COUNT = 0;
                if (para.Count > 0)
                {
                    //DateTime dt = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(para[0].Hsy_val));
                    DateTime dt = _ReceiptHeader.SAR_VALID_TO;
                    //if (dt<DateTime.Now)
                    if (dt < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        List<string> reqNumList = new List<string>();
                        ApproveRequestUC APPROVE = new ApproveRequestUC();
                        List<RequestApprovalHeader> approvedHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT007.ToString(), 1, 0);
                        //reqNumList = APPROVE.getApprovedReqNumbersList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT009.ToString(), 1, 5);

                        if (approvedHeaders != null)
                        {
                            var _count = from _dup in approvedHeaders
                                         where _dup.Grah_fuc_cd == selectedReceiptHdr.Sar_receipt_no
                                         select _dup;
                            COUNT = _count.Count();
                        }


                        if (COUNT == 0)
                        {
                            if (MessageBox.Show("Cannot Refund.\nExceeds Maximum receipt refund days.\nDo you want to send Refund Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            else
                            {
                                //send request.
                                //if (lblAccNo.Text.Trim() == string.Empty)
                                //{
                                //    MessageBox.Show("Select an Account first!");
                                //    return;
                                //}

                                //if (txtReques.Text == "" || txtReques.Text == string.Empty)
                                //{
                                //    int defaultAmt = 0;
                                //    txtReques.Text = defaultAmt.ToString();
                                //}
                                //if (chkIsECDrate.Checked == true && Convert.ToDecimal(txtReques.Text.Trim()) > 100)
                                //{
                                //    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "ECD rate cannot be grater than 100!");
                                //    txtReques.Focus();
                                //    MessageBox.Show("ECD rate cannot be grater than 100!");
                                //    return;
                                //}
                                //send custom request.

                                //Added by Prabhath on 11/10/2013
                                //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT007", txtReceiptNo.Text.Trim()))
                                //{ return; }

                                #region fill RequestApprovalHeader

                                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_app_dt = currentDate.Date;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                                ra_hdr.Grah_app_stus = "P";
                                ra_hdr.Grah_app_tp = "ARQT007";
                                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdr.Grah_fuc_cd = _ReceiptHeader.Sar_receipt_no;//lblAccNo.Text;
                                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;

                                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;// Convert.ToDateTime(txtReceiptDate.Text);


                                //if (BaseCls.GlbUserDefProf != ddl_Location.SelectedValue.ToString())
                                //{
                                //    ra_hdr.Grah_oth_loc = "1";
                                //}
                                //else
                                //{ 
                                ra_hdr.Grah_oth_loc = "0";
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
                                ra_hdr.Grah_remaks = "ADVAN REFUND";

                                #endregion

                                #region fill List<RequestApprovalDetail>
                                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                                ra_det.Grad_ref = ra_hdr.Grah_ref;
                                ra_det.Grad_line = 1;
                                ra_det.Grad_req_param = "ADVAN_REFUND";
                                ra_det.Grad_val1 = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;// _ReceiptHeader.Sar_tot_settle_amt;//Convert.ToDecimal(txtReques.Text.Trim());
                                ra_det.Grad_is_rt1 = false;
                                ra_det.Grad_anal1 = _ReceiptHeader.Sar_receipt_no; ////lblAccNo.Text;
                                ra_det.Grad_anal2 = selectedReceiptHdr.Sar_anal_3;//manual number
                                //ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                                ra_det.Grad_date_param = currentDate.Date.AddDays(10);
                                ra_det_List.Add(ra_det);
                                #endregion

                                #region fill RequestApprovalHeaderLog

                                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_app_dt = currentDate.Date; //Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdrLog.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;//not sure
                                ra_hdrLog.Grah_app_stus = "P";
                                ra_hdrLog.Grah_app_tp = "ARQT007";
                                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
                                ra_hdrLog.Grah_fuc_cd = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;//Convert.ToDateTime(txtReceiptDate.Text);
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
                                ra_detLog.Grad_req_param = "ADVAN_REFUND";
                                ra_detLog.Grad_val1 = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt; //Convert.ToDecimal(txtReques.Text.Trim());
                                ra_detLog.Grad_is_rt1 = false;
                                ra_detLog.Grad_anal1 = _ReceiptHeader.Sar_receipt_no;// lblAccNo.Text;
                                ra_detLog.Grad_anal2 = selectedReceiptHdr.Sar_anal_3; //manual number
                                ra_detLog.Grad_date_param = currentDate.Date.AddDays(10);  //Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                                ra_detLog_List.Add(ra_detLog);
                                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                                #endregion
                                string referenceNo;

                                //CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqRequestLevel, GlbReqIsApprovalUser, true, ra_hdr.Grah_ref.ToString());
                                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, true, out referenceNo);

                                if (eff > 0)
                                {
                                    //string Msg = "<script>alert('Request sent!' );</script>";
                                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                    MessageBox.Show("Request sent!");
                                }
                                else
                                {
                                    // string Msg = "<script>alert('Request not sent!' );</script>";
                                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                    MessageBox.Show("Sorry. Request not sent!");
                                }
                                this.Cursor = Cursors.Default;
                                return;
                                //BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
                                //txtReques.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Refund parameters not setup.");//cancelled receipt.
                    this.Cursor = Cursors.Default;
                    return;
                }
                //for (int i = 0; i < gvReceipt.Rows.Count; i++)
                //{
                //    CheckBox chk = (CheckBox)gvReceipt.Rows[i].FindControl("chkselect");

                //    if (chk.Checked == true)
                //    {
                this.Cursor = Cursors.Default;

                if (MessageBox.Show("Are you sure to Refund?", "Confirm Refund", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                btnSave_.Enabled = false;


                //_ReceiptHeader.Sar_seq_no = 1;
                _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                if (_ReceiptHeader.Sar_receipt_type == "ADVAN")
                {
                    _ReceiptHeader.Sar_receipt_type = "ADREF";
                }
                else if (_ReceiptHeader.Sar_receipt_type == "ADINS")
                {
                    _ReceiptHeader.Sar_receipt_type = "INSRF";
                }
                //selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                _ReceiptHeader.Sar_tot_settle_amt = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                //_ReceiptHeader.Sar_receipt_no = "1";
                //_ReceiptHeader.Sar_prefix = gvReceipt.DataKeys[i][1].ToString();
                //_ReceiptHeader.Sar_manual_ref_no = gvReceipt.Rows[i].Cells[6].Text;
                _ReceiptHeader.Sar_receipt_date = currentDate.Date;
                _ReceiptHeader.Sar_direct = false;
                _ReceiptHeader.Sar_acc_no = "";
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
                _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                //_ReceiptHeader.Sar_debtor_cd = gvReceipt.Rows[i].Cells[13].Text;
                //_ReceiptHeader.Sar_debtor_name = gvReceipt.Rows[i].Cells[14].Text;
                //_ReceiptHeader.Sar_debtor_add_1 = gvReceipt.DataKeys[i][2].ToString();
                //_ReceiptHeader.Sar_debtor_add_2 = gvReceipt.DataKeys[i][3].ToString();
                //_ReceiptHeader.Sar_tel_no = gvReceipt.DataKeys[i][4].ToString();
                //_ReceiptHeader.Sar_mob_no = gvReceipt.DataKeys[i][5].ToString();
                //_ReceiptHeader.Sar_nic_no = gvReceipt.DataKeys[i][6].ToString();
                //_ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][14].ToString());
                //_ReceiptHeader.Sar_comm_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][7].ToString());
                _ReceiptHeader.Sar_is_mgr_iss = false;
                //_ReceiptHeader.Sar_esd_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][8].ToString());
                //_ReceiptHeader.Sar_wht_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][9].ToString());
                //_ReceiptHeader.Sar_epf_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][10].ToString());
                _ReceiptHeader.Sar_currency_cd = "LKR";
                _ReceiptHeader.Sar_uploaded_to_finance = false;
                _ReceiptHeader.Sar_act = true;
                _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                _ReceiptHeader.Sar_direct_deposit_branch = "";
                //_ReceiptHeader.Sar_remarks = gvReceipt.DataKeys[i][11].ToString();
                _ReceiptHeader.Sar_is_used = false;
                //_ReceiptHeader.Sar_ref_doc = gvReceipt.Rows[i].Cells[4].Text;
                _ReceiptHeader.Sar_ser_job_no = "";
                _ReceiptHeader.Sar_used_amt = 0;
                _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_ReceiptHeader.Sar_anal_1 = gvReceipt.DataKeys[i][12].ToString();
                //_ReceiptHeader.Sar_anal_2 = gvReceipt.DataKeys[i][13].ToString();
                //_ReceiptHeader.Sar_anal_3 = gvReceipt.DataKeys[i][15].ToString();
                //_ReceiptHeader.Sar_anal_4 = "";
                _ReceiptHeader.Sar_anal_5 = 0;
                _ReceiptHeader.Sar_anal_6 = 0;
                _ReceiptHeader.Sar_anal_7 = 0;
                _ReceiptHeader.Sar_anal_8 = 0;
                _ReceiptHeader.Sar_anal_9 = 0;
                _ReceiptHeader.Sar_loc = BaseCls.GlbUserDefLoca;

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = _ReceiptHeader.Sar_prefix;
                masterAuto.Aut_year = null;

                #region
                RecieptItem ri = new RecieptItem();
                //ri = _i;
                ri.Sard_settle_amt = selectedReceiptHdr.Sar_tot_settle_amt - selectedReceiptHdr.Sar_used_amt;
                ri.Sard_pay_tp = "CASH";
                ri.Sard_line_no = 1;
                // ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                // ri.Sard_seq_no = _h.Sar_seq_no;                  
                // ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                // ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //  ri.Sard_cc_period = _i.Sard_cc_period;
                //  ri.Sard_cc_tp = _i.Sard_cc_tp;
                // ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                // ri.Sard_chq_branch = _i.Sard_chq_branch;
                // ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                // ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                // ri.Sard_deposit_branch = _i.Sard_deposit_branch;                   
                //  ri.Sard_ref_no = _i.Sard_ref_no;
                // ri.Sard_anal_3 = _i.Sard_anal_3;                               
                // _i.Sard_settle_amt = 0;

                _RecItemList = new List<RecieptItem>();
                _RecItemList.Add(ri);
                #endregion
                if (_tmpRecItem == null)
                {
                   _tmpRecItem = new List<ReceiptItemDetails>();
                    ReceiptItemDetails _re=new ReceiptItemDetails();
                    _re.Sari_item="";
                    _tmpRecItem.Add(_re);
                }

              //  int effect = CHNLSVC.Sales.CreateRefund(_ReceiptHeader, _RecItemList, masterAuto, _tmpRecItem, null, out _RefundNo);
                //add by tharanga 2017/10/02
                int effect = CHNLSVC.Sales.CreateRefundNEW(_ReceiptHeader, _RecItemList, masterAuto, _tmpRecItem, null, out _RefundNo);


                try
                {
                    //TODO: IF APPROVED REQUEST- THEN CHANGE THE HEADER STATUS TO 'F'
                    if (COUNT > 0)
                    {
                        List<RequestApprovalHeader> approvedHeaders = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _ReceiptHeader.Sar_receipt_no, HirePurchasModuleApprovalCode.ARQT007.ToString(), 1, 0);
                        if (approvedHeaders != null)
                        {
                            foreach (RequestApprovalHeader appHdr in approvedHeaders)
                            {
                                appHdr.Grah_app_stus = "F";
                                appHdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;
                                appHdr.Grah_app_by = BaseCls.GlbUserID;
                                appHdr.Grah_app_dt = currentDate.Date;
                                CHNLSVC.General.UpdateApprovalStatus(appHdr);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                this.Cursor = Cursors.Default;

                if (effect == 1)
                {
                    MessageBox.Show("Receipt refund Successfully!\n( Refund Receipt#: " + _RefundNo + " )");

                    //string Msg = "<script>alert('Receipt refund Successfully!');window.location = 'ReceiptReversal.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    //this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg);
                        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                    }
                    else
                    {
                        MessageBox.Show("Creation Failed.");
                        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                    }
                }

                //    }
                //}

                btnSave_.Enabled = true;
                this.btnClear_Click(sender, e);
                #endregion
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

        private void grvPendingInsReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                DataGridViewRow row = grvPendingInsReq.Rows[rowIndex];

                string Req_no = row.Cells["pend_Grah_ref"].Value.ToString();
                ddlPendingVehIns_req.SelectedItem = Req_no;

            }
        }

        private void grvAppInsReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    //GridViewRow row = grvInvItems.SelectedRow;
                    DataGridViewRow row = grvAppInsReq.Rows[rowIndex];

                    string Req_no = row.Cells["app_Grah_ref"].Value.ToString();
                    ddlApprVehIns_req.SelectedItem = Req_no;

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

        private void grvPendingRegReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                DataGridViewRow row = grvPendingRegReq.Rows[rowIndex];

                string Req_no = row.Cells["rpend_Grah_ref"].Value.ToString();
                ddlPendingVehReg_req.SelectedItem = Req_no;

            }
        }

        private void grvAppRegReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                DataGridViewRow row = grvAppRegReq.Rows[rowIndex];

                string Req_no = row.Cells["rapp_Grah_ref"].Value.ToString();
                ddlApprVehReg_req.SelectedItem = Req_no;

            }
        }

        private void pickedDate_ValueChanged(object sender, EventArgs e)
        {
            currentDate = Convert.ToDateTime(pickedDate.Value).Date;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT022", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                {
                    chkApp.Enabled = true;
                    _isAppUser = true;
                    _appLvl = _sysApp.Sarp_app_lvl;
                }
                else
                {
                    chkApp.Checked = false;
                    chkApp.Enabled = false;
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
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT022", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT022", BaseCls.GlbUserID, txtReqLoc.Text);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    dgvPendings.DataSource = _TempReqAppHdr;
                }

                if (!string.IsNullOrEmpty(txtSearchCrNote.Text))
                {
                    List<RequestApprovalHeader> _record = (from _lst in _TempReqAppHdr
                                                           where _lst.Grah_fuc_cd == txtSearchCrNote.Text
                                                           select _lst).ToList();

                    if (_record.Count > 0)
                    {
                        dgvPendings.DataSource = _record;
                    }
                    else
                    {
                        MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void clear_cashRefund()
        {

            _refAppHdr = new RequestApprovalHeader();
            _refAppDet = new List<RequestApprovalDetail>();
            _refAppAuto = new MasterAutoNumber();

            _refAppHdrLog = new RequestApprovalHeaderLog();
            _refAppDetLog = new List<RequestApprovalDetailLog>();

            dgvPendings.AutoGenerateColumns = false;
            dgvPendings.DataSource = new List<RequestApprovalHeader>();

            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            btnRequest.Enabled = true;
            chkApp.Checked = false;
            chkApp.Enabled = false;
            txtInvoice.Text = "";
            txtCusCode.Text = "";
            lblInvCusName.Text = "";
            lblInvCusAdd1.Text = "";
            lblInvCusAdd2.Text = "";
            lblInvDate.Text = "";
            lblSalePc.Text = "";
            lblCramt.Text = "";
            txtRefundAmt.Text = "";
            txtRemarks.Text = "";
            lblInvoiceNo.Text = "";
            lblReq.Text = "";
            lblReqPC.Text = "";
            lblStatus.Text = "";
            txtSearchCrNote.Text = "";
            txtInvoice.Enabled = true;
            txtCusCode.Enabled = true;
            txtRefundAmt.Enabled = true;
            txtInvoice.Focus();

        }

        private void dgvPendings_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _reqNo = "";
                string _stus = "";
                string _invNo = "";
                string _remarks = "";
                string _pc = "";
                string _salesPC = "";

                txtInvoice.Text = "";
                txtCusCode.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblInvDate.Text = "";
                lblSalePc.Text = "";
                lblInvoiceNo.Text = "";
                lblCramt.Text = "";
                txtRefundAmt.Text = "";

                txtInvoice.Enabled = true;
                txtCusCode.Enabled = true;
                btnRequest.Enabled = false;

                _reqNo = dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value.ToString();
                _stus = dgvPendings.Rows[e.RowIndex].Cells["col_Status"].Value.ToString();
                _invNo = dgvPendings.Rows[e.RowIndex].Cells["col_Inv"].Value.ToString();
                _remarks = dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value.ToString();
                _pc = dgvPendings.Rows[e.RowIndex].Cells["col_pc"].Value.ToString();
                _salesPC = dgvPendings.Rows[e.RowIndex].Cells["col_OthPC"].Value.ToString();

                txtInvoice.Text = _invNo;
                lblReq.Text = _reqNo;
                txtRemarks.Text = _remarks;
                lblReqPC.Text = _pc;
                lblSalePc.Text = _salesPC;

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




                List<RequestApprovalDetail> _tmpList = new List<RequestApprovalDetail>();
                _tmpList = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, _pc, _reqNo);

                foreach (RequestApprovalDetail _tmp in _tmpList)
                {
                    lblInvoiceNo.Text = _tmp.Grad_anal1;
                    txtRefundAmt.Text = FormatToCurrency (_tmp.Grad_val1.ToString());   //kapila 6/1/2017 remove rounding
                }

                InvoiceHeader _invHdr = new InvoiceHeader();
                _invHdr = CHNLSVC.Sales.get_CrNote(txtInvoice.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                if (_invHdr != null)
                {
                    txtCusCode.Text = _invHdr.Sah_cus_cd;
                    lblInvCusName.Text = _invHdr.Sah_cus_name;
                    lblInvCusAdd1.Text = _invHdr.Sah_cus_add1;
                    lblInvCusAdd2.Text = _invHdr.Sah_cus_add2;
                    lblInvDate.Text = _invHdr.Sah_dt.ToShortDateString();
                    lblSalePc.Text = _invHdr.Sah_pc;
                    lblInvoiceNo.Text = _invHdr.Sah_ref_doc;
                    lblCramt.Text = Convert.ToDecimal(_invHdr.Sah_anal_7 - _invHdr.Sah_anal_8).ToString("n");

                    List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                    _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), null);

                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _paramInvoiceItems;

                    btnMainSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Text = "";
                    txtCusCode.Text = "";
                    lblInvCusName.Text = "";
                    lblInvCusAdd1.Text = "";
                    lblInvCusAdd2.Text = "";
                    lblInvDate.Text = "";
                    lblSalePc.Text = "";
                    lblCramt.Text = "";
                    txtRefundAmt.Text = "";
                    lblInvoiceNo.Text = "";
                    txtInvoice.Focus();
                    btnMainSave.Enabled = false;
                    btnApp.Enabled = false;
                    btnRej.Enabled = false;
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    return;
                }

                txtInvoice.Enabled = false;
                txtCusCode.Enabled = false;
                txtRefundAmt.Enabled = false;



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

        private void btnInvSearch_Click(object sender, EventArgs e)
        {
            try
            {


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();



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

        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                {
                    _isFromReq = false;
                    lblReq.Text = "";
                    lblStatus.Text = "";

                    InvoiceHeader _invHdr = new InvoiceHeader();
                    _invHdr = CHNLSVC.Sales.get_CrNote(txtInvoice.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                    if (_invHdr != null)
                    {
                        txtCusCode.Text = _invHdr.Sah_cus_cd;
                        lblInvCusName.Text = _invHdr.Sah_cus_name;
                        lblInvCusAdd1.Text = _invHdr.Sah_cus_add1;
                        lblInvCusAdd2.Text = _invHdr.Sah_cus_add2;
                        lblInvDate.Text = _invHdr.Sah_dt.ToShortDateString();
                        lblSalePc.Text = _invHdr.Sah_pc;
                        lblInvoiceNo.Text = _invHdr.Sah_ref_doc;
                        lblCramt.Text = Convert.ToDecimal(_invHdr.Sah_anal_7 - _invHdr.Sah_anal_8).ToString("n");
                        txtRefundAmt.Text = FormatToCurrency(Convert.ToDecimal(_invHdr.Sah_anal_7 - _invHdr.Sah_anal_8).ToString());  //kapila 6/1/2017 remove rounding

                        List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                        _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), null);

                        dgvInvItem.AutoGenerateColumns = false;
                        dgvInvItem.DataSource = new List<InvoiceItem>();
                        dgvInvItem.DataSource = _paramInvoiceItems;
                    }
                    else
                    {
                        MessageBox.Show("Invalid invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblSalePc.Text = "";
                        lblCramt.Text = "";
                        txtRefundAmt.Text = "";
                        lblInvoiceNo.Text = "";
                        txtInvoice.Focus();
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

        private void txtInvoice_DoubleClick(object sender, EventArgs e)
        {
            try
            {


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();



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

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                    DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtRefundAmt.Focus();
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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string _docNo = "";
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Invoice customer is missing.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    MessageBox.Show("Please select invoice number.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRefundAmt.Text))
                {
                    MessageBox.Show("Please enter refund amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblSalePc.Text))
                {
                    MessageBox.Show("Original sales profit center is missing.Please re-enter invoice #.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtRefundAmt.Text) > Convert.ToDecimal(lblCramt.Text))
                {
                    MessageBox.Show("Refund amount cannot exceed credit amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtRefundAmt.Text) <= 0)
                {
                    MessageBox.Show("Invalid receipt amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT022", txtInvoice.Text.Trim()))
                {
                    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                CollectRefApp();
                CollectRefAppLog();

                int effet = CHNLSVC.Sales.SaveCashRefundReqApp(_refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, out _docNo);

                if (effet == 1)
                {

                    MessageBox.Show("Request generated." + _docNo, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_cashRefund();
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

        protected void CollectRefApp()
        {
            string _type = "";

            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            _refAppHdr = new RequestApprovalHeader();
            _refAppDet = new List<RequestApprovalDetail>();
            _refAppAuto = new MasterAutoNumber();


            _refAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _refAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _refAppHdr.Grah_app_tp = "ARQT022";
            _refAppHdr.Grah_fuc_cd = txtInvoice.Text;
            _refAppHdr.Grah_ref = null;
            _refAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _refAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _refAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _refAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_app_stus = "P";
            _refAppHdr.Grah_app_lvl = 0;
            _refAppHdr.Grah_app_by = string.Empty;
            _refAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _refAppHdr.Grah_sub_type = null;
            _refAppHdr.Grah_oth_pc = lblSalePc.Text.Trim();



            _tempReqAppDet = new RequestApprovalDetail();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(txtRefundAmt.Text);
            _tempReqAppDet.Grad_val2 = Convert.ToDecimal(lblCramt.Text);
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = lblInvoiceNo.Text.Trim();
            _tempReqAppDet.Grad_anal2 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;

            _refAppDet.Add(_tempReqAppDet);



            _refAppAuto = new MasterAutoNumber();
            _refAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _refAppAuto.Aut_cate_tp = "PC";
            _refAppAuto.Aut_direction = 1;
            _refAppAuto.Aut_modify_dt = null;
            _refAppAuto.Aut_moduleid = "REQ";
            _refAppAuto.Aut_number = 0;
            _refAppAuto.Aut_start_char = "CSREF";
            _refAppAuto.Aut_year = null;
        }

        protected void CollectRefAppLog()
        {
            string _type = "";
            _refAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            _refAppDetLog = new List<RequestApprovalDetailLog>();

            _refAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _refAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _refAppHdrLog.Grah_app_tp = "ARQT022";
            _refAppHdrLog.Grah_fuc_cd = txtInvoice.Text;
            _refAppHdrLog.Grah_ref = null;
            _refAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _refAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _refAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _refAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdrLog.Grah_app_stus = "P";
            _refAppHdrLog.Grah_app_lvl = 0;
            _refAppHdrLog.Grah_app_by = string.Empty;
            _refAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdrLog.Grah_remaks = txtRemarks.Text.Trim();
            _refAppHdrLog.Grah_sub_type = null;
            _refAppHdrLog.Grah_oth_pc = lblSalePc.Text.Trim();


            _tempReqAppDet = new RequestApprovalDetailLog();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(txtRefundAmt.Text);
            _tempReqAppDet.Grad_val2 = Convert.ToDecimal(lblCramt.Text);
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = lblInvoiceNo.Text.Trim();
            _tempReqAppDet.Grad_anal2 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;
            _refAppDetLog.Add(_tempReqAppDet);


        }

        private void btnReqClear_Click(object sender, EventArgs e)
        {
            clear_cashRefund();
        }

        private void txtRefundAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRefundAmt.Text))
                {
                    if (string.IsNullOrEmpty(lblCramt.Text))
                    {
                        MessageBox.Show("Credit amount not found.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Focus();
                        return;
                    }

                    if (!IsNumeric(txtRefundAmt.Text))
                    {
                        MessageBox.Show("Amount should be numeric.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefundAmt.Text = "";
                        txtRefundAmt.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtRefundAmt.Text) > Convert.ToDecimal(lblCramt.Text))
                    {
                        MessageBox.Show("Refund amount cannot exceed credit amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefundAmt.Text = "";
                        txtRefundAmt.Focus();
                        return;
                    }

                    txtRefundAmt.Text =FormatToCurrency( Convert.ToDecimal(txtRefundAmt.Text).ToString());
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

        private void btnCusSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
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

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
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

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtInvoice.Focus();
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

        private void txtReqLoc_DoubleClick(object sender, EventArgs e)
        {
            try
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
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
                        MessageBox.Show("Invalid or accsess denied location.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReqLoc.Text = "";
                        txtReqLoc.Focus();
                    }
                    else
                    {

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

        private void btnApp_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //set_approveUser_infor("ARQT014");

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "A";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clear_cashRefund();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnRej_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "R";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clear_cashRefund();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnMainSave_Click(object sender, EventArgs e)
        {
            try
            {

                string _msg = "";
                Int16 row_aff = 0;

                if (lblReqPC.Text != BaseCls.GlbUserDefProf)
                {
                    MessageBox.Show("Request profit center and your profit center is not match.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (CheckServerDateTime() == false) return;

                if (lblStatus.Text != "APPROVED")
                {
                    MessageBox.Show("Request is still not approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtRefundAmt.Text))
                {
                    MessageBox.Show("Please enter refund amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtRefundAmt.Text) > Convert.ToDecimal(lblCramt.Text))
                {
                    MessageBox.Show("Refund amount cannot exceed credit amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtRefundAmt.Text) <= 0)
                {
                    MessageBox.Show("Invalid receipt amount.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundAmt.Text = "";
                    txtRefundAmt.Focus();
                    return;
                }


                RecieptHeader _saveHdr = new RecieptHeader();
                RecieptItem _saveItm = new RecieptItem();

                _saveHdr.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                _saveHdr.Sar_com_cd = BaseCls.GlbUserComCode;
                _saveHdr.Sar_receipt_type = "CSREF";
                _saveHdr.Sar_receipt_no = null;
                _saveHdr.Sar_prefix = null;
                _saveHdr.Sar_manual_ref_no = lblInvoiceNo.Text;
                _saveHdr.Sar_receipt_date = Convert.ToDateTime(currentDate).Date;
                _saveHdr.Sar_direct = false;
                _saveHdr.Sar_acc_no = "";
                _saveHdr.Sar_is_oth_shop = false;
                _saveHdr.Sar_oth_sr = "";
                _saveHdr.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _saveHdr.Sar_debtor_cd = txtCusCode.Text.Trim();
                _saveHdr.Sar_debtor_name = lblInvCusName.Text.Trim();
                _saveHdr.Sar_debtor_add_1 = lblInvCusAdd1.Text.Trim();
                _saveHdr.Sar_debtor_add_2 = lblInvCusAdd2.Text.Trim();
                _saveHdr.Sar_tel_no = "";
                _saveHdr.Sar_mob_no = null;
                _saveHdr.Sar_nic_no = null;
                _saveHdr.Sar_tot_settle_amt = Convert.ToDecimal(txtRefundAmt.Text);
                _saveHdr.Sar_comm_amt = 0;
                _saveHdr.Sar_is_mgr_iss = false;
                _saveHdr.Sar_esd_rate = 0;
                _saveHdr.Sar_wht_rate = 0;
                _saveHdr.Sar_epf_rate = 0;
                _saveHdr.Sar_currency_cd = "LKR";
                _saveHdr.Sar_uploaded_to_finance = false;
                _saveHdr.Sar_act = true;
                _saveHdr.Sar_direct_deposit_bank_cd = "";
                _saveHdr.Sar_direct_deposit_branch = "";
                _saveHdr.Sar_remarks = txtRemarks.Text;
                _saveHdr.Sar_is_used = false;
                _saveHdr.Sar_ref_doc = lblReq.Text.Trim();
                _saveHdr.Sar_ser_job_no = "";
                _saveHdr.Sar_used_amt = 0;
                _saveHdr.Sar_create_by = BaseCls.GlbUserID;
                _saveHdr.Sar_mod_by = BaseCls.GlbUserID;
                _saveHdr.Sar_session_id = BaseCls.GlbUserSessionID;
                _saveHdr.Sar_anal_1 = null;
                _saveHdr.Sar_anal_2 = null;
                _saveHdr.Sar_anal_3 = null;
                _saveHdr.Sar_anal_8 = 0;
                _saveHdr.Sar_anal_4 = "";
                _saveHdr.Sar_anal_5 = 0;
                _saveHdr.Sar_anal_6 = 0;
                _saveHdr.Sar_anal_7 = 0;
                _saveHdr.Sar_anal_9 = 0;



                _saveItm.Sard_seq_no = _saveHdr.Sar_seq_no;
                _saveItm.Sard_line_no = 1;
                _saveItm.Sard_receipt_no = null;
                _saveItm.Sard_inv_no = txtInvoice.Text;
                _saveItm.Sard_pay_tp = "CASH";
                _saveItm.Sard_settle_amt = Convert.ToDecimal(txtRefundAmt.Text);



                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RECEIPT";
                masterAuto.Aut_number = 5;//what is Aut_number
                if (BaseCls.GlbUserComCode == "LRP")
                {
                    masterAuto.Aut_start_char = "CAREF";
                }
                else
                {
                    masterAuto.Aut_start_char = "CSREF";
                }
                masterAuto.Aut_year = null;


                MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
                masterAutoRecTp.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAutoRecTp.Aut_cate_tp = "PC";
                masterAutoRecTp.Aut_direction = null;
                masterAutoRecTp.Aut_modify_dt = null;
                masterAutoRecTp.Aut_moduleid = "RECEIPT";
                masterAutoRecTp.Aut_number = 5;//what is Aut_number
                if (BaseCls.GlbUserComCode == "LRP")
                {
                    masterAuto.Aut_start_char = "CAREF";
                }
                else
                {
                    masterAuto.Aut_start_char = "CSREF";
                }
                masterAutoRecTp.Aut_year = null;


                string QTNum;

                row_aff = (Int16)CHNLSVC.Sales.SaveCashRefund(_saveHdr, _saveItm, masterAuto, masterAutoRecTp, out QTNum);


                if (row_aff == 1)
                {
                    MessageBox.Show("Successfully refunded.Reference No: " + QTNum, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ReportViewer _view = new ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "REC";
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = QTNum;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;

                    clear_cashRefund();
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

        private void btnCashRefClear_Click(object sender, EventArgs e)
        {
            clear_cashRefund();
        }

        private void btnSearchCrNote_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();


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

        private void btnSearchReqCr_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSearchCrNote;
                _CommonSearch.ShowDialog();
                txtSearchCrNote.Select();

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

        private void txtSearchCrNote_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                    DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSearchCrNote;
                    _CommonSearch.ShowDialog();
                    txtSearchCrNote.Select();
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

        private void txtRefundAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRequest.Focus();
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[3])
            {

                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT022", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                {
                    chkApp.Enabled = true;
                    _isAppUser = true;
                    _appLvl = _sysApp.Sarp_app_lvl;
                }
                else
                {
                    chkApp.Checked = false;
                    chkApp.Enabled = false;
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
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT022", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT022", BaseCls.GlbUserID, txtReqLoc.Text);
                }

                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();



                dgvPendings.DataSource = _TempReqAppHdr;


            }
        }

        private void btnSrhExp_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();
                //txtFromDate
                // _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtFromDate_.Text).Date, Convert.ToDateTime(txtToDate_.Text).Date, "ADVAN");
                _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterangeExpired(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpexpDateFrom.Value, dtpexpDateTo.Value, "ADVAN", txtExRecipt.Text);
                _receiptHdrExp = _paramReceipt;
                if (_receiptHdr != null)
                {
                    gvExpReceipt.DataSource = null;
                    gvExpReceipt.AutoGenerateColumns = false;
                    gvExpReceipt.DataSource = _receiptHdrExp;
                    //gvReceipt.DataBind();
                }
                else
                {
                    DataTable _Itemtable = new DataTable();
                    gvExpReceipt.DataSource = _Itemtable;
                    //gvReceipt.DataBind();
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

        private void txtReceiptNo_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void gvReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvExpReceipt_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvExpReceipt.Rows.Count > 0)
            {
                txtExRecipt.Text = gvExpReceipt.Rows[e.RowIndex].Cells["colRrceipt"].Value.ToString();
                dtpExpDate.Value = Convert.ToDateTime(gvExpReceipt.Rows[e.RowIndex].Cells["colvalid"].Value.ToString()).Date;
            }
        }

        private void gvExpReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSendReqExp_Click(object sender, EventArgs e)
        {// Done By Nadeeka 17-09-2015
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (gvExpReceipt.Rows.Count < 1)
                {
                    return;
                }

                //GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
                //if (GlbReqUserPermissionLevel == -1)
                //{

                //    GlbReqUserPermissionLevel = 0;
                //}

                if (MessageBox.Show("Do you want to send this Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }


                if (txtExRecipt.Text == "")
                {
                    MessageBox.Show("Please select the receipt first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpExpDate.Value.Date < DateTime.Today.Date)
                {
                    MessageBox.Show("Expiry date must be higher than today!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT043", txtExRecipt.Text))
                {
                    MessageBox.Show("There are approved or pending request for this receipt  number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //GetRecieptNo

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT043";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = txtExRecipt.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;

                ra_hdr.Grah_oth_loc = "0";

                ra_hdr.Grah_remaks = "Expiry_Advance";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "Expiry_Advance";
                ra_det.Grad_date_param = dtpExpDate.Value.Date;
                ra_det.Grad_anal1 = txtExRecipt.Text;
                ra_det_List.Add(ra_det);

                //kapila 10/1/2017
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 2;
                ra_det.Grad_req_param = "Settle_Advance";
                ra_det.Grad_date_param = dtpSetValid.Value.Date;
                ra_det.Grad_anal1 = txtExRecipt.Text;
                ra_det_List.Add(ra_det);

                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 3;
                ra_det.Grad_req_param = "Refund_Advance";
                ra_det.Grad_date_param = dtpRefValid.Value.Date;
                ra_det.Grad_anal1 = txtExRecipt.Text;
                ra_det_List.Add(ra_det);

                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = currentDate.Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT043";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;
                ra_hdrLog.Grah_fuc_cd = txtExRecipt.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;

                ra_hdrLog.Grah_oth_loc = "0";

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "Expiry_Advance";
                ra_detLog.Grad_date_param = dtpExpDate.Value.Date;

                ra_detLog.Grad_anal1 = txtExRecipt.Text;
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                #endregion



                #region send request


                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "EXPREC";//VIRFREQ CCREGRF
                _ReqAppAuto.Aut_year = null;
                //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);
                string referenceNo;
                string reqStatus;
                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(null, _ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);
                if (eff > 0)
                {
                    MessageBox.Show("Request sent sucessfullys!\nReference #: " + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
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

        private void btnSrnRecItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();
                //txtFromDate
                // _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtFromDate_.Text).Date, Convert.ToDateTime(txtToDate_.Text).Date, "ADVAN");
                _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterangeItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpFromDateItem.Value, dtpToDateItem.Value, "ADVAN", txtRecItem.Text);
                _receiptHdrExp = _paramReceipt;
                if (_receiptHdr != null)
                {
                    gvRecItem.DataSource = null;
                    gvRecItem.AutoGenerateColumns = false;
                    gvRecItem.DataSource = _receiptHdrExp;
                    //gvReceipt.DataBind();
                }
                else
                {
                    DataTable _Itemtable = new DataTable();
                    gvRecItem.DataSource = _Itemtable;
                    //gvReceipt.DataBind();
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

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItem;
            _CommonSearch.ShowDialog();
            txtItem.Select();
        }

        private void txtengine_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the item.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
            DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtengine;
            _CommonSearch.ShowDialog();
            txtengine.Select();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItem_DoubleClick(null, null);
            }
        }

        private void txtVehInsRec1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvRecItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (gvRecItem.Rows.Count > 0)
            {
                txtRecItem.Text = gvRecItem.Rows[e.RowIndex].Cells["colRec"].Value.ToString();
               
            }
            
            _tmpRecItem = new List<ReceiptItemDetails>();
            if (string.IsNullOrEmpty(gvRecItem.Text))
            {
                _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(txtRecItem.Text);
            }

            if (_tmpRecItem != null)
            {
                gvRecItemSer.DataSource = null;
                gvRecItemSer.AutoGenerateColumns = false;
                gvRecItemSer.DataSource = _tmpRecItem;
                //gvReceipt.DataBind();
            }
            else
            {
                _tmpRecItem = new List<ReceiptItemDetails>();
                gvRecItemSer.DataSource = _tmpRecItem;
                //gvReceipt.DataBind();
            }
        }

        private void gvRecItemSer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Do you want to delete this record ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    _tmpRecItem.RemoveAt(e.RowIndex);
                    gvRecItemSer.AutoGenerateColumns = false;
                    gvRecItemSer.DataSource = new List<ReceiptItemDetails>();
                    gvRecItemSer.DataSource = _tmpRecItem;
                }
            }
        }

        private void btn_add_ser_Click(object sender, EventArgs e)
        {
            Int32 _count1 = _tmpRecItem.Where(X => X.Sari_item == txtItem.Text && X.Sari_serial_1 == txtengine.Text).Count();
            if (_count1 > 0)
            { 
                MessageBox.Show("This Item already added into the list!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
 

            ReceiptItemDetails _recitem = new ReceiptItemDetails();
            _recitem.Sari_item = txtItem.Text;
            _recitem.Sari_item_desc = _itemList.Mi_shortdesc;
            _recitem.Sari_model = _itemList.Mi_model;
            _recitem.Sari_serial = txtengine.Text;
            _recitem.Sari_serial_1 = txtengine.Text;
         
            _tmpRecItem.Add(_recitem);


            if (_tmpRecItem != null)
            {
                gvRecItemSer.DataSource = null;
                gvRecItemSer.AutoGenerateColumns = false;
                gvRecItemSer.DataSource = _tmpRecItem;
                //gvReceipt.DataBind();
            }

            txtItem.Text = "";
            txtengine.Text = "";


        }

        private void btnReqSendItem_Click(object sender, EventArgs e)
        {
            // Done By Nadeeka 17-09-2015
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (gvRecItemSer.Rows.Count < 1)
                {
                    return;
                }

                //GetUserAppLevel(HirePurchasModuleApprovalCode.VHREGRF);
                //if (GlbReqUserPermissionLevel == -1)
                //{

                //    GlbReqUserPermissionLevel = 0;
                //}

                if (MessageBox.Show("Do you want to send this Request?", "Confirm Sending Request", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }


                if (txtRecItem.Text == "")
                {
                    MessageBox.Show("Please select the receipt first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT044", txtRecItem.Text))
                {
                    MessageBox.Show("There are approved or pending request for this receipt  number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //GetRecieptNo

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = currentDate.Date;
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT044";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                ra_hdr.Grah_fuc_cd = txtRecItem.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;

                ra_hdr.Grah_oth_loc = "0";

                ra_hdr.Grah_remaks = "ItemChange_Advance";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "ItemChange_Advance";
              

                ra_det.Grad_anal1 = txtRecItem.Text;

                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = currentDate.Date;
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT044";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;
                ra_hdrLog.Grah_fuc_cd = txtRecItem.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;

                ra_hdrLog.Grah_oth_loc = "0";

                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "ItemChange_Advance";
           

                ra_detLog.Grad_anal1 = txtRecItem.Text;
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

                #endregion

                #region request serial detail

                int _line = 1;
                List<RequestApprovalSerials> ReqApp_serList = new List<RequestApprovalSerials>();
                RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                foreach (ReceiptItemDetails ser in _tmpRecItem)
                {
                    _tempReqAppSer.Gras_ref = ra_hdr.Grah_ref;//"1";
                    _tempReqAppSer.Gras_line = _line;

                    _tempReqAppSer.Gras_anal2 = ser.Sari_item;
                    _tempReqAppSer.Gras_anal3 = ser.Sari_serial_1;
                    _tempReqAppSer.Gras_anal7 = 0;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                }
                               
                ReqApp_serList.Add(_tempReqAppSer);
                #endregion


                #region send request


                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "ITEMCHG";
                _ReqAppAuto.Aut_year = null;
                //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);
                string referenceNo;
                string reqStatus;
                // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
                Int32 eff = CHNLSVC.General.Save_RequestApprove_forReceiptReverse(ReqApp_serList, _ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);
                if (eff > 0)
                {
                    MessageBox.Show("Request sent sucessfullys!\nReference #: " + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sorry. Request not sent!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
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

        private void btnExRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string _appType = string.Empty;
                if(    radioButtonExpire.Checked)
                {
                    _appType="ARQT043";

                }
                else
                {    _appType="ARQT044";
                }

                txtAppType.Text = _appType;

              //  SystemAppLevelParam _sysApp = new SystemAppLevelParam();

               // _sysApp = CHNLSVC.Sales.CheckApprovePermission(_appType, BaseCls.GlbUserID);

                if (CHNLSVC.Financial.CheckAppReqCancelPerm(BaseCls.GlbUserID, _appType)==true)
                {
                    chkApp.Enabled = true;
                    _isAppUser = true;
                   // _appLvl = _sysApp.Sarp_app_lvl;
                }
                else
                {
                    chkApp.Checked = false;
                    chkApp.Enabled = false;
                }

                if (_isAppUser == true)
                {
                    btnExApproved.Enabled = true;
                    btnExReject.Enabled = true;
                    txtExloc.Enabled = true;
                }
                else
                {
                    btnExApproved.Enabled = false;
                    btnExReject.Enabled = false;
                    txtExloc.Enabled = false;
                }

                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf,_appType, null, txtExloc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _appType, BaseCls.GlbUserID, txtExloc.Text);
                }

                gvExApproved.AutoGenerateColumns = false;
                gvExApproved.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Advance Receipt Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    gvExApproved.DataSource = _TempReqAppHdr;
                }

                if (!string.IsNullOrEmpty(txtSearchCrNote.Text))
                {
                    List<RequestApprovalHeader> _record = (from _lst in _TempReqAppHdr
                                                           where _lst.Grah_fuc_cd == txtExAppRec.Text
                                                           select _lst).ToList();

                    if (_record.Count > 0)
                    {
                        gvExApproved.DataSource = _record;
                    }
                    else
                    {
                        MessageBox.Show("No any request / approval found.", "Advance Receipt Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnExApproved_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtSelReceipt.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtSelStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtSelStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //set_approveUser_infor("ARQT014");

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = txtExloc.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtSelReceipt.Text;
                _RequestApprovalStatus.Grah_ref = txtReqEx.Text;
                _RequestApprovalStatus.Grah_app_stus = "A";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtExRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);
                if (txtAppType.Text  == "ARQT043")
                {
                    _rowEffect = CHNLSVC.Sales.Update_ReceiptItemExpriryDate(BaseCls.GlbUserComCode, txtSelReceipt.Text, dtpAppExDate.Value.Date, dtpAppSetDate.Value.Date, dtpAppRefDate.Value.Date);
                }
                else{
                    _rowEffect = CHNLSVC.Sales.Update_ReceiptItemSerial(BaseCls.GlbUserComCode, txtSelReceipt.Text, txtSelReceipt.Text);
                }

                txtSelReceipt.Text = "";
                txtReqEx.Text = "";

                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clear_cashRefund();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnExReject_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "APPROVED")
                {
                    MessageBox.Show("Request is already approved.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    MessageBox.Show("Request is already rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                _RequestApprovalStatus.Grah_loc = txtExloc.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtSelReceipt.Text;
                _RequestApprovalStatus.Grah_ref = txtReqEx.Text;
                _RequestApprovalStatus.Grah_app_stus = "R";
                _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtExRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = null;

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clear_cashRefund();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to approved.Please re-try", "Cash refund", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void txtExRecipt_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtVehRegRec1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExRecipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSrhExp.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {


                    txtExRecipt_DoubleClick(null, null);

                 
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExRecipt_DoubleClick(object sender, EventArgs e)
        {
            _isExpired = true;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SatReceipt);
            DataTable _result = CHNLSVC.CommonSearch.SearchReceipt(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.IsSearchEnter = true;  
            _CommonSearch.obj_TragetTextBox = txtExRecipt;
            _CommonSearch.ShowDialog();
            txtExRecipt.Select();
        }

        private void txtRecItem_DoubleClick(object sender, EventArgs e)
        {

            _isExpired = true;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SatReceipt);
            DataTable _result = CHNLSVC.CommonSearch.SearchReceipt(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.obj_TragetTextBox = txtRecItem;
            _CommonSearch.ShowDialog();
            txtRecItem.Select();
        }

        private void txtRecItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSrnRecItem.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {


                    txtRecItem_DoubleClick(null, null);


                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExAppRec_DoubleClick(object sender, EventArgs e)
        {
            _isExpired = true;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SatReceipt);
            DataTable _result = CHNLSVC.CommonSearch.SearchReceipt(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.obj_TragetTextBox = txtExAppRec;
            _CommonSearch.ShowDialog();
            txtExAppRec.Select();
        }

        private void txtExAppRec_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSrnRecItem.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {


                    txtRecItem_DoubleClick(null, null);


                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExRecipt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvExApproved_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (gvExApproved.Rows.Count > 0)
            {
                txtSelReceipt.Text = gvExApproved.Rows[e.RowIndex].Cells["colexpRec"].Value.ToString();
                txtReqEx.Text = gvExApproved.Rows[e.RowIndex].Cells["colAppReq"].Value.ToString();

               DataTable _dtlreQdet=  CHNLSVC.General.SearchrequestAppDetByRef(txtReqEx.Text);
                  foreach (DataRow _r in _dtlreQdet.Rows)
                  {
                    //  if (!string.IsNullOrEmpty(_r.Field<string>("grad_date_param")))
                      if (radioButtonItem.Checked==false)
                      {
                          if (_r.Field<DateTime>("Grad_req_param").ToString() == "Expiry_Advance")  //kapila 10/1/2017
                          dtpAppExDate.Value = _r.Field<DateTime>("grad_date_param");
                          if (_r.Field<DateTime>("Grad_req_param").ToString() == "Settle_Advance")
                              dtpAppSetDate.Value = _r.Field<DateTime>("grad_date_param");
                          if (_r.Field<DateTime>("Grad_req_param").ToString() == "Refund_Advance")
                              dtpAppRefDate.Value = _r.Field<DateTime>("grad_date_param");
                     }
                        
                  }
            }
        }

        private void gvExApproved_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtpAppExDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonItem.Checked)

            {dtpAppExDate.Visible  =false;
            lblExp.Visible = false;
            }
            else
            {
                dtpAppExDate.Visible = true;
                lblExp.Visible = true;
            }
        }

        private void C(object sender, EventArgs e)
        {

        }

        private void cmbRecTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindRecSubTypes();
        }

        private void txtVehRegRec1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //ImgRegSearch1_Click(null, null);
        }

        private void txtVehRegRec1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                   
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.vehregdet);
                    DataTable _result = CHNLSVC.CommonSearch.Search_veh_reg_ref(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.obj_TragetTextBox = txtVehRegRec1;
                    _CommonSearch.ShowDialog();
                    txtVehRegRec1.Focus();
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
    }
}

