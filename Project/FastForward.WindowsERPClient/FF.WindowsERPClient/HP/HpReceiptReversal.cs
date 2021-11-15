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


namespace FF.WindowsERPClient.HP
{
    public partial class HpReceiptReversal : Base
    {
        //sp_get_receipthdrall =UPDATE
        //sp_get_receiptitm_OnPayTp =NEW
        //sp_get_All_appreq_reqno  =NEW

        #region Properties/Local Variables
        string tabPage1_Name = "Approved Requests";
        string tabPage2_Name = "Pending Requests";

        private string accountNo;
        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; lblAccountNo.Text = value; }
        }

        RequestApprovalHeader _approvalHeader_appr;
        public RequestApprovalHeader _ApprovalHeader_appr
        {
            get { return _approvalHeader_appr; }
            set { _approvalHeader_appr = value; }
        }
        List<RequestApprovalHeader> _approvalHeader_appr_list;
        public List<RequestApprovalHeader> _ApprovalHeader_appr_list
        {
            get { return _approvalHeader_appr_list; }
            set { _approvalHeader_appr_list = value; }
        }
        //-----------------------------------------------

        RequestApprovalHeader _recieptHeader_appr;
        public RequestApprovalHeader _RecieptHeader_appr
        {
            get { return _recieptHeader_appr; }
            set { _recieptHeader_appr = value; }
        }
        List<RequestApprovalHeader> _recieptHeader_appr_list;
        public List<RequestApprovalHeader> _RecieptHeader_appr_list
        {
            get { return _recieptHeader_appr_list; }
            set { _recieptHeader_appr_list = value; }
        }
        //-----------------------------------------------

        //----------------------------------------------
        RecieptHeader _recieptHeader_req;
        public RecieptHeader _RecieptHeader_req
        {
            get { return _recieptHeader_req; }
            set { _recieptHeader_req = value; }
        }
        List<RecieptHeader> _recieptHeader_req_list;
        public List<RecieptHeader> _RecieptHeader_req_list
        {
            get { return _recieptHeader_req_list; }
            set { _recieptHeader_req_list = value; }
        }
        //-----------------------------------------------
        private List<HpAccount> accountsList;
        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }
        //public List<HpAccount> AccountsList
        //{
        //    get { return (List<HpAccount>)ViewState["AccountsList"]; }
        //    set { ViewState["AccountsList"] = value; }
        //}        
        //--------------------------------------------------------------------------
        private List<RecieptHeader> _RecieptHeaderList;
        public List<RecieptHeader> _recieptHeaderList
        {
            get { return _RecieptHeaderList; }
            set { _RecieptHeaderList = value; }
        }
        //protected List<RecieptHeader> _recieptHeaderList
        //{
        //    get { return (List<RecieptHeader>)ViewState["_recieptHeaderList"]; }
        //    set { ViewState["_recieptHeaderList"] = value; }
        //}                     
        //--------------------------------------------------------------------------
        private List<RecieptItem> _RecieptItem;
        public List<RecieptItem> _recieptItem
        {
            get { return _RecieptItem; }
            set { _RecieptItem = value; }
        }
        //protected List<RecieptItem> _recieptItem
        //{
        //    get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
        //    set { ViewState["_recieptItem"] = value; }
        //}
        //--------------------------------------------------------------------------
        //private decimal _PaidAmount;

        //public decimal _paidAmount
        //{
        //    get { return _PaidAmount; }
        //    set { _PaidAmount = value; }
        //}

        //--------------------------------------------------------------------------
        #endregion
        DateTime currentDate = DateTime.Now.Date;

        public HpReceiptReversal()
        {
            InitializeComponent();
            ucHpAccountSummary2.Clear();
            ucHpAccountDetail2.Clear();
            CurrDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            //CHNLSVC.Security.GetServerDateTime().Date;
            GlbReqIsApprovalNeed = true;

            currentDate = CHNLSVC.Security.GetServerDateTime().Date;
            pickedDate.Value = currentDate;
           
            bindDefaultValuesToGrids();
        }
        private void bindDefaultValuesToGrids()
        {
            string _approvalCode = "";
            if (optManagerIssue.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT002.ToString();
            }

            if (optRtnCheque.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT004.ToString();
            }

            if (optOthReceipt.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT015.ToString();
            }
            //------------Approved list-----------------------------------------
            List<RequestApprovalHeader> a_lst = CHNLSVC.General.GetAllRequests(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _approvalCode, "A");
            List<RequestApprovalHeader> a_lst_bind = new List<RequestApprovalHeader>();

            var appliancesByType = a_lst.GroupBy(item => item.Grah_fuc_cd).ToDictionary(grp => grp.Key, grp => grp.ToList());
            List<string> distinctAccounts = appliancesByType.Keys.ToList();

            // _ApprovalHeader_appr_list = a_lst;  
            //-----------------------------------------------------------------------------------**
            if (a_lst != null)
            {
                foreach (string acc in distinctAccounts)
                {
                    string accNo = acc;
                    Int32 _isApproval = 1;
                    List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, accNo, _approvalCode, _isApproval, GlbReqUserPermissionLevel);

                    if (_lst != null)
                    {
                        foreach (RequestApprovalHeader hd in _lst)
                        {
                            List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
                            try
                            {
                                if (detlist.Count > 0)
                                {
                                    hd.Grad_val1 = detlist[0].Grad_val1;
                                    hd.Grad_anal4 = detlist[0].Grad_anal4;
                                    hd.Grad_anal3 = detlist[0].Grad_anal3; //ADD
                                }
                            }
                            catch (Exception EX)
                            {
                                MessageBox.Show(EX.Message);
                            }

                        }
                    }
                    a_lst_bind.AddRange(_lst);
                }

                Int32 No_of_ApprovedReq = a_lst_bind.Count;
                tabPage1.Text = tabPage1_Name + "(" + No_of_ApprovedReq.ToString() + ")";
            }
            grvApprovedReq.DataSource = null;
            grvApprovedReq.AutoGenerateColumns = false;
            grvApprovedReq.DataSource = a_lst_bind;

            _ApprovalHeader_appr_list = a_lst_bind;

            //*********************************************************************************
            //if (a_lst!=null)
            //{
            //    foreach (RequestApprovalHeader hd_ in a_lst)
            //    {
            //        Int32 _isApproval = 1;
            //        List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, hd_.Grah_fuc_cd, _approvalCode, _isApproval, GlbReqUserPermissionLevel);
            //        a_lst_bind.AddRange(_lst);
            //        if (_lst != null)
            //        {
            //            foreach (RequestApprovalHeader hd in _lst)
            //            {

            //                List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
            //                try
            //                {
            //                    if (detlist.Count > 0)
            //                    {
            //                        hd.Grad_val1 = detlist[0].Grad_val1;
            //                        hd.Grad_anal4 = detlist[0].Grad_anal4;
            //                    }
            //                }
            //                catch (Exception EX)
            //                {
            //                  //  MessageBox.Show(EX.Message);
            //                }                           
            //            }                       
            //        }
            //    }
            //}
            //*********************************************************************************

            //if (a_lst != null)
            //{
            //    foreach (RequestApprovalHeader hd in a_lst)
            //    {

            //        List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
            //        try
            //        {
            //            if (detlist.Count > 0)
            //            {
            //                hd.Grad_val1 = detlist[0].Grad_val1; //approved value
            //                hd.Grad_anal4 = detlist[0].Grad_anal4; //receipt #.
            //            }
            //        }
            //        catch (Exception EX)
            //        {
            //            MessageBox.Show(EX.Message);
            //        }

            //    }

            //   Int32 No_of_ApprovedReq= a_lst.Count;
            //   tabPage1.Text = tabPage1_Name + "(" + No_of_ApprovedReq.ToString() + ")";
            //}



            //Int32 No_of_ApprovedReq = a_lst_bind.Count;
            //  tabPage1.Text = tabPage1_Name + "(" + No_of_ApprovedReq.ToString() + ")";

            //  grvApprovedReq.DataSource = null;
            //  grvApprovedReq.AutoGenerateColumns = false;
            //   grvApprovedReq.DataSource = a_lst_bind;

            //   _ApprovalHeader_appr_list = a_lst_bind;  

            //-----------------Pending list-------------------------------------------------------------------------

            List<RequestApprovalHeader> p_lst = CHNLSVC.General.GetAllRequests(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _approvalCode, "P");
            _RecieptHeader_appr_list = p_lst;
            if (p_lst != null)
            {
                foreach (RequestApprovalHeader hd in p_lst)
                {

                    List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
                    try
                    {
                        if (detlist.Count > 0)
                        {
                            hd.Grad_val1 = detlist[0].Grad_val1; //request value
                            hd.Grad_anal4 = detlist[0].Grad_anal4; //receipt #.
                            hd.Grad_anal3 = detlist[0].Grad_anal3; //ADD
                        }
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message);
                    }

                }

                Int32 No_of_PendingdReq = p_lst.Count;
                tabPage2.Text = tabPage2_Name + "(" + No_of_PendingdReq.ToString() + ")";
            }
            grvPendingReq.DataSource = null;
            grvPendingReq.AutoGenerateColumns = false;
            grvPendingReq.DataSource = p_lst;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }

            InitializeComponent();

            ucHpAccountSummary2.Clear();
            ucHpAccountDetail2.Clear();
            CurrDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            //CHNLSVC.Security.GetServerDateTime().Date;
            GlbReqIsApprovalNeed = true;

            currentDate = CHNLSVC.Security.GetServerDateTime().Date;
            pickedDate.Value = currentDate;

            bindDefaultValuesToGrids();

            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
          
        }
        public void LoadAccountDetail(string _account, DateTime _date)
        {
            lblAccountNo.Text = _account;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(CHNLSVC.Security.GetServerDateTime().Date, _account);

            if (AccountsList != null)
            {
                HpAccount account = new HpAccount();
                foreach (HpAccount acc in AccountsList)
                {
                    if (_account == acc.Hpa_acc_no)
                    {
                        account = acc;
                    }
                }

                ucHpAccountSummary2.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);

            }
        }
        private void LOAD_Account()
        {

            string location = BaseCls.GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            try
            {
                Decimal accSeq = Convert.ToDecimal(acc_seq);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter Account's Sequence No.");
                return;
            }

            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            if(accList==null)
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "R");
            if (accList == null)
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "T");
            AccountsList = accList;//save in veiw state
            if (accList == null)
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                MessageBox.Show("Enter valid Account number!");
                txtAccountNo.Text = null;
                return;
            }
            if (accList.Count == 0)
            {
                MessageBox.Show("Enter valid Account number!");
                txtAccountNo.Text = null;
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                foreach (HpAccount ac in accList)
                {
                    LoadAccountDetail(ac.Hpa_acc_no, CHNLSVC.Security.GetServerDateTime().Date);
                }
            }
            else if (accList.Count > 1)
            {

                HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                f2.visible_panel_accountSelect(true);
                f2.visible_panel_ReqApp(false);
                f2.fill_AccountGrid(accList);
                f2.ShowDialog();

            }
        }
        private void HpReceiptReversal_Load(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            //    Cursor.Current = Cursors.WaitCursor;
            //    LOAD_Account();
            //    Cursor.Current = Cursors.Default;

        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Cursor.Current = Cursors.WaitCursor;
                ucHpAccountSummary2.Clear();
                ucHpAccountDetail2.Clear();
                lblAccountNo.Text = "";
                LOAD_Account();
                ucHpAccountDetail2.Uc_hpa_acc_no = lblAccountNo.Text.Trim();//ac.Hpa_acc_no;
                //---------------------------------------------------
                //----------
                GetUserAppLevel();

                Int32 _GlbReqRequestLevel = GlbReqRequestLevel;
                Int32 _GlbReqRequestApprovalLevel = GlbReqRequestApprovalLevel;
                bool _GlbReqIsApprovalUser = GlbReqIsApprovalUser;
                bool _GlbReqIsFinalApprovalUser = GlbReqIsFinalApprovalUser;
                bool _GlbReqIsRequestGenerateUser = GlbReqIsRequestGenerateUser;
                Int32 _GlbReqUserPermissionLevel = GlbReqUserPermissionLevel;

                if (GlbReqUserPermissionLevel != -1)
                {
                    LoadOptReceipts(lblAccountNo.Text.ToString(), gvItemDetail);
                    if (optApproved.Checked == true)
                    {
                        BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
                    }

                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approval cycle definition is not setup! Please contact IT department..");
                    //MessageBox.Show("Approval cycle definition is not setup! Please contact IT department..");
                }
                //----------
                Cursor.Current = Cursors.Default;

                //**********************                                      

                // List<RecieptHeader> _RecieptHdrList_org = CHNLSVC.Sales.Get_ReceiptHeaderListALL(lblAccountNo.Text, "MI_NEW");
                //foreach (RecieptHeader rh in _RecieptHdrList_org)
                //{
                //    List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(rh.Sar_receipt_no, "MI_NEW_REV");
                //    var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                //                        group _receiptHdr by new { _receiptHdr.Sar_ref_doc } into itm
                //                        select new { ref_Receipt_no = itm.Key.Sar_ref_doc, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                //    //foreach (var _lists in _tbreceiptHdr)
                //    //{
                //      foreach (DataGridViewRow rv in gvItemDetail.Rows)
                //       {
                //            if (rv.Cells["SAR_RECEIPT_NO"].Value.ToString() == _lists.ref_Receipt_no)
                //            {
                //                rv.Cells["SAR_ANAL_5"].Value = _lists.Receipt_Tot;
                //            }
                //        }
                //    //    //lblRefundTot.Text = FormatToCurrency(_lists.Receipt_Tot.ToString());
                //    //}               
                //}
                //string _refundType = "";
                //if (optNewReq.Checked==true)
                //{
                //    if (optManagerIssue.Checked == true)
                //    {
                //        _refundType = "MI_NEW";
                //    }
                //    else if (optRtnCheque.Checked == true)
                //    {
                //        _refundType = "RC_NEW";
                //    }
                //    else if (optOthReceipt.Checked == true)
                //    {
                //        _refundType = "OR_NEW";
                //    }
                //}


                foreach (DataGridViewRow rv in gvItemDetail.Rows)
                {
                    if (optOthReceipt.Checked == true)
                    {
                        //SET THE REFUND AMOUNT.
                        try
                        {
                            List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(rv.Cells["SAR_RECEIPT_NO"].Value.ToString(), "OR_NEW_REV");
                            if (_RecieptHdrList != null)
                            {
                                var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                                                    group _receiptHdr by new { _receiptHdr.Sar_ref_doc } into itm
                                                    select new { ref_Receipt_no = itm.Key.Sar_ref_doc, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                                foreach (var _lists in _tbreceiptHdr)
                                {
                                    rv.Cells["SAR_ANAL_5"].Value = _lists.Receipt_Tot.ToString();
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (optManagerIssue.Checked == true)
                    {
                        //SET THE REFUND AMOUNT.
                        try
                        {
                            List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(rv.Cells["SAR_RECEIPT_NO"].Value.ToString(), "MI_NEW_REV");
                            if (_RecieptHdrList != null)
                            {
                                var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                                                    group _receiptHdr by new { _receiptHdr.Sar_ref_doc } into itm
                                                    select new { ref_Receipt_no = itm.Key.Sar_ref_doc, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                                foreach (var _lists in _tbreceiptHdr)
                                {
                                    rv.Cells["SAR_ANAL_5"].Value = _lists.Receipt_Tot.ToString();
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    if (optRtnCheque.Checked == true && optNewReq.Checked == true)
                    {
                        try
                        {  //SET THE TOTAL AMOUNT PAYED BY THE CHEQUES FOR THE RECEIPT.
                            string SEQno = rv.Cells["SAR_SEQ_NO"].Value.ToString();
                            List<RecieptItem> _RecieptItemsList = CHNLSVC.Sales.Get_receiptitm_OnPayTp(SEQno, "CHEQUE", string.Empty);
                            if (_RecieptItemsList != null)
                            {
                                var _tbreceiptITM = from _receiptItm in _RecieptItemsList
                                                    group _receiptItm by new { _receiptItm.Sard_receipt_no } into itm
                                                    select new { Receipt_no = itm.Key.Sard_receipt_no, Receipt_Tot = itm.Sum(p => p.Sard_settle_amt) };

                                foreach (var _lists in _tbreceiptITM)
                                {
                                    if (rv.Cells["SAR_RECEIPT_NO"].Value.ToString() == _lists.Receipt_no)
                                    {
                                        rv.Cells["SAR_TOT_SETTLE_AMT"].Value = _lists.Receipt_Tot.ToString();
                                    }

                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                        //-------------------------------------------
                        //SET THE REFUND AMOUNT.
                        string ref_doc_no = rv.Cells["SAR_RECEIPT_NO"].Value.ToString();
                        List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(ref_doc_no, "RC_NEW_REV");
                        if (_RecieptHdrList != null)
                        {
                            var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                                                group _receiptHdr by new { _receiptHdr.Sar_ref_doc } into itm
                                                select new { ref_Receipt_no = itm.Key.Sar_ref_doc, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                            foreach (var _lists in _tbreceiptHdr)
                            {
                                rv.Cells["SAR_ANAL_5"].Value = _lists.Receipt_Tot.ToString();
                            }
                        }

                    }
                }
                //**********************

                load_Approved_and_Pending_Requests(lblAccountNo.Text.Trim());
                //----------------------------------------------------------------------------
                string _refundType = "";
                if (optManagerIssue.Checked == true)
                {
                    _refundType = "MI_NEW";
                }
                else if (optRtnCheque.Checked == true)
                {
                    _refundType = "RC_NEW";
                }
                else if (optOthReceipt.Checked == true)
                {
                    _refundType = "OR_NEW";
                }
                _RecieptHeader_req_list = CHNLSVC.Sales.Get_ReceiptHeaderListALL(lblAccountNo.Text.Trim(), _refundType);

                grvAllReceipts.DataSource = null;
                grvAllReceipts.AutoGenerateColumns = false;
                grvAllReceipts.DataSource = _RecieptHeader_req_list;

                //BindReceiptAll(lblAccountNo.Text.Trim(), _refundType, grvAllReceipts);

                // LoadOptReceipts(lblAccountNo.Text.ToString(), grvAllReceipts);

                panel_refundTp.Enabled = false;
            }
        }
        private void load_Approved_and_Pending_Requests(string AccountNo)
        {
            DataTable dt = new DataTable();
            grvApprovedReq.DataSource = null;
            grvAllReceipts.AutoGenerateColumns = false;
            grvApprovedReq.DataSource = dt;

            grvPendingReq.DataSource = null;
            grvPendingReq.AutoGenerateColumns = false;
            grvPendingReq.DataSource = dt;

            grvAllReceipts.DataSource = null;
            grvAllReceipts.AutoGenerateColumns = false;
            grvAllReceipts.DataSource = dt;


            string _approvalCode = "";
            if (optManagerIssue.Checked == true)
            {
                // base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT002, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                _approvalCode = HirePurchasModuleApprovalCode.ARQT002.ToString();// CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                //_documentType = "MANAGER_ISSUE";

            }

            if (optRtnCheque.Checked == true)
            {
                //base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                //RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                _approvalCode = HirePurchasModuleApprovalCode.ARQT004.ToString();
                //_documentType = "RETURN_CHEQUE";
            }

            if (optOthReceipt.Checked == true)
            {
                //base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

                _approvalCode = HirePurchasModuleApprovalCode.ARQT015.ToString();
                //_documentType = "OTHER_RECEIPT";
            }

            Int32 _isApproval = 1;
            // List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, AccountNo, HirePurchasModuleApprovalCode.ARQT002.ToString(), _isApproval, GlbReqUserPermissionLevel);
            List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, AccountNo, _approvalCode, _isApproval, GlbReqUserPermissionLevel);

            if (_lst != null)
            {
                foreach (RequestApprovalHeader hd in _lst)
                {

                    List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
                    try
                    {
                        if (detlist.Count > 0)
                        {
                            hd.Grad_val1 = detlist[0].Grad_val1;
                            hd.Grad_anal4 = detlist[0].Grad_anal4;
                            hd.Grad_anal3 = detlist[0].Grad_anal3; //ADD
                        }
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message);
                    }

                }
            }

            grvApprovedReq.DataSource = null;
            grvApprovedReq.AutoGenerateColumns = false;
            grvApprovedReq.DataSource = _lst;

            _ApprovalHeader_appr_list = _lst;

            //------------Pending list-----------------------------------------
            // List<RequestApprovalHeader> p_lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _approvalCode, BaseCls.GlbUserID, string.Empty);
            List<RequestApprovalHeader> p_lst_bind = new List<RequestApprovalHeader>();
            List<RequestApprovalHeader> p_lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _approvalCode, BaseCls.GlbUserID, BaseCls.GlbUserDefProf);
            if (p_lst != null)
            {
                foreach (RequestApprovalHeader hd in p_lst)
                {
                    if (hd.Grah_fuc_cd == lblAccountNo.Text)
                    {
                        RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, hd.Grah_ref);
                        p_lst_bind.Add(REQ_HEADER);
                    }
                }
            }


            if (p_lst_bind != null)
            {
                foreach (RequestApprovalHeader hd in p_lst_bind)
                {
                    List<RequestApprovalDetail> detlist = CHNLSVC.General.GetRequestByRef(hd.Grah_com, hd.Grah_loc, hd.Grah_ref);
                    try
                    {
                        if (detlist.Count > 0)
                        {
                            hd.Grad_val1 = detlist[0].Grad_val1;
                            hd.Grad_anal4 = detlist[0].Grad_anal4;
                        }
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message);
                    }

                }
            }


            grvPendingReq.DataSource = null;
            grvPendingReq.AutoGenerateColumns = false;
            grvPendingReq.DataSource = p_lst_bind;

            _RecieptHeader_appr_list = p_lst_bind;

            //-----------------------------bind--gvItemDetail-----------------------------------------
            Bind_gvItemDetail();


        }

        private void BindRequestsToDropDown(string _account, ComboBox _ddl)
        {
            if (GlbReqIsApprovalNeed)
            {
                //case
                //1.get user approval level
                //2.if user request generate user, allow to check approval request check box and load approved requests
                //3.else load the request which lower than the approval level in the table which is not approved

                int _isApproval = 0;

                if (GlbReqIsRequestGenerateUser)
                    //no need to load pendings, but if check box select, load apporoved requests
                    if (chkApproved.Checked) _isApproval = 1;
                    else _isApproval = 0;
                else _isApproval = 0;

                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT002.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _ddl.Items.Clear();
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        query.Select(x => x).Distinct();
                        _ddl.DataSource = query;

                        // _ddl.DataBind();
                    }
                }
            }
            else //SHANI ON 04-03-2013
            {
                if (chkApproved.Checked == false)
                {
                    List<RequestApprovalHeader> _lst = null;
                    if (optManagerIssue.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT002.ToString(), 0, GlbReqUserPermissionLevel);
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT004.ToString(), 0, GlbReqUserPermissionLevel);
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT015.ToString(), 0, GlbReqUserPermissionLevel);
                    }
                    if (_lst != null)
                    {
                        if (_lst.Count > 0)
                        {
                            // _ddl.Items.Clear();
                            _ddl.DataSource = null;
                            _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                            var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                            var distinctS = new List<string>(query.Distinct());
                            _ddl.DataSource = distinctS;
                            // _ddl.DataBind();
                        }
                    }

                }
                else
                {
                    List<RequestApprovalHeader> _lst = null;
                    if (optManagerIssue.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT002.ToString(), 1, GlbReqUserPermissionLevel);
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT004.ToString(), 1, GlbReqUserPermissionLevel);
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT015.ToString(), 1, GlbReqUserPermissionLevel);
                    }
                    if (_lst != null)
                    {
                        if (_lst.Count > 0)
                        {
                            // _ddl.Items.Clear();
                            _ddl.DataSource = null;
                            _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                            var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                            var distinctS = new List<string>(query.Distinct());
                            _ddl.DataSource = distinctS;
                            // _ddl.DataBind();
                        }
                    }

                }
            }
        }

        protected void LoadOptReceipts(string _accNo, DataGridView _gvname)
        {
            bool _isApp = false;
            string _refundType = string.Empty;
            if (optApproved.Checked == true)
            {
                _isApp = true;
            }

            if (_isApp == false)
            {
                if (_gvname.Name == "gvItemDetail")
                {
                    if (optManagerIssue.Checked == true)
                    {
                        _refundType = "MI_NEW";
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _refundType = "RC_NEW";
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _refundType = "OR_NEW";
                    }
                }
                else if (_gvname.Name == "grvRefundHistory")
                {
                    if (optManagerIssue.Checked == true) //Reversals
                    {
                        _refundType = "MI_NEW_REV";
                    }
                    if (optRtnCheque.Checked == true) //Reversals
                    {
                        _refundType = "RC_NEW_REV";
                    }
                    if (optOthReceipt.Checked == true) //Reversals
                    {
                        _refundType = "OR_NEW_REV";
                    }
                }

            }
            else
            {
                if (_gvname.Name == "gvItemDetail")
                {
                    if (optManagerIssue.Checked == true)
                    {
                        _refundType = "MI_APP";
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _refundType = "RC_APP";
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _refundType = "OR_APP";
                    }
                }
            }
            BindReceiptAll(_accNo, _refundType, _gvname);
        }
        #region  Data Bind Area
        protected void BindReceiptAll(string _para, string _type, DataGridView _dvname)
        {
            DataTable _table = CHNLSVC.Sales.Get_ReceiptHeaderTableALL(_para, _type);
            if (_table.Rows.Count <= 0)
            {
                // _table = SetEmptyRow(_table);
                _dvname.DataSource = null;
                _dvname.AutoGenerateColumns = false;
                _dvname.DataSource = _table;

                if (_dvname.Name == "grvRefundHistory")
                {
                    lblRefundTot.Text = "0.00";
                }
            }
            else
            {
                List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(_para, _type);
                foreach (RecieptHeader _hdr in _RecieptHdrList)
                {
                    if (optManagerIssue.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "MANAGER_ISSUE";
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "RETURN_CHEQUE";
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "OTHER_RECEIPT";
                    }
                    _hdr.Sar_anal_5 = 0;
                }


                if (_dvname.Name == "grvRefundHistory")
                {
                    _dvname.DataSource = null;
                    _dvname.AutoGenerateColumns = false;
                    _dvname.DataSource = _RecieptHdrList;
                    var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                                        group _receiptHdr by new { _receiptHdr.Sar_receipt_no } into itm
                                        select new { Receipt_no = itm.Key.Sar_receipt_no, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                    foreach (var _lists in _tbreceiptHdr)
                    {
                        //lblRefundTot.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(_lists.Receipt_Tot.ToString()));
                        lblRefundTot.Text = FormatToCurrency(_lists.Receipt_Tot.ToString());
                    }
                }
                else if (_dvname.Name == "gvItemDetail")
                {
                    _recieptHeaderList = _RecieptHdrList;
                    _dvname.DataSource = null;
                    _dvname.AutoGenerateColumns = false;
                    _dvname.DataSource = _recieptHeaderList;
                }

            }
            //  _dvname.DataBind();

        }

        protected void BindRequestApprovalDetailLog(string seqNo)
        {
            //_recieptHeaderList = null;
            _recieptHeaderList = new List<RecieptHeader>();

            List<RecieptHeader> _rhdrList = new List<RecieptHeader>();
            List<RequestApprovalDetailLog> _rappdetlogList = new List<RequestApprovalDetailLog>();

            if (string.IsNullOrEmpty(seqNo))
            {
                LoadOptReceipts(lblAccountNo.Text, gvItemDetail);
            }
            else
            {
                _rappdetlogList = CHNLSVC.General.GetRequestApprovalDetailLog(seqNo);
                foreach (var _lists in _rappdetlogList)
                {
                    RecieptHeader _rhdr = new RecieptHeader();
                    _rhdr.Sar_com_cd = BaseCls.GlbUserComCode;// GlbCompany;
                    _rhdr.Sar_receipt_type = "";
                    _rhdr.Sar_profit_center_cd = _lists.Grad_anal5;
                    _rhdr.Sar_acc_no = _lists.Grad_anal1;
                    _rhdr.Sar_receipt_no = _lists.Grad_anal4;//grad_anal4
                    _rhdr.Sar_receipt_date = _lists.Grad_date_param.Date;
                    _rhdr.Sar_manual_ref_no = _lists.Grad_anal3;
                    _rhdr.Sar_prefix = _lists.Grad_anal2;
                    _rhdr.Sar_tot_settle_amt = _lists.Grad_val2;
                    _rhdr.Sar_anal_5 = _lists.Grad_val1;
                    _rhdr.Sar_anal_1 = _lists.Grad_req_param;
                    _rhdr.Sar_session_id = BaseCls.GlbUserSessionID; // add by akila 2017/12/26
                    _rhdrList.Add(_rhdr);
                }

                _recieptHeaderList = _rhdrList;

                gvItemDetail.DataSource = _recieptHeaderList;
                //gvItemDetail.DataBind();
            }
        }

        #endregion

        //private void set_apprCycleData()
        //{
        //    #region Check Approval Cycle
        //    CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;

        //    string _documentType = string.Empty;
        //    //Base base_ = new Base();
        //    if (optManagerIssue.Checked == true)
        //    {
        //        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT002, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        //        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
        //        _documentType = "MANAGER_ISSUE";

        //    }

        //    if (optRtnCheque.Checked == true)
        //    {
        //        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        //        //RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
        //        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004;
        //        _documentType = "RETURN_CHEQUE";
        //    }

        //    if (optOthReceipt.Checked == true)
        //    {
        //        RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        //        //RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
        //        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015;
        //        _documentType = "OTHER_RECEIPT";
        //    }

        //    #endregion
        //}
        protected void GetUserAppLevel()
        {
            if (optManagerIssue.Checked == false && optRtnCheque.Checked == false && optOthReceipt.Checked == false)
            {
                MessageBox.Show("Select option");
                return;
            }
            if (optManagerIssue.Checked == true)
            {
                // HSMIREV
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSMIREV, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            }

            if (optRtnCheque.Checked == true)
            {
                //HSRCREV
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSRCREV, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            }

            if (optOthReceipt.Checked == true)
            {
                //HSRCTRV
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSRCTRV, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            }
        }
        private void SaveRequest()
        {
            try
            {
                bool _isapprovedrequest = false;
                List<RecieptItem> _recieptItemList = new List<RecieptItem>();

                ////UI validation.
                if (string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    throw new Exception("Please select the account no!");
                }
                if (_recieptHeaderList == null)
                {
                    throw new Exception("Please select the receipt(s)!");
                }

                #region Check Approval Cycle
                CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                string _documentType = string.Empty;
                Base base_ = new Base();

                GetUserAppLevel();
                if (optManagerIssue.Checked == true)
                {
                    // base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT002, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                    _documentType = "MANAGER_ISSUE";

                }

                if (optRtnCheque.Checked == true)
                {
                    //base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    //RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004;
                    _documentType = "RETURN_CHEQUE";
                }

                if (optOthReceipt.Checked == true)
                {
                    //base_.RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
                    //RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015;
                    _documentType = "OTHER_RECEIPT";
                }

                #endregion

                // if (GlbReqIsApprovalNeed == true)
                // {
                if (optApproved.Checked == true)//pending request
                {
                    //if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()) == false)
                    if (ddlRequestNo.SelectedValue != null)
                    {
                        //GlbReqIsApprovalNeed = false; commented by shani
                        GlbReqIsApprovalNeed = true;//add by shani
                        _isapprovedrequest = true;
                    }
                    else
                    {
                        GlbReqIsApprovalNeed = true;//add by shani
                        return;
                    }

                }
                //}
                else
                {
                    _isapprovedrequest = false;
                    GlbReqIsApprovalNeed = true;//add by shani
                }


                //if (GlbReqIsApprovalNeed == true)
                //if (GlbReqIsFinalApprovalUser == false && GlbReqIsApprovalNeed == true)
                if (GlbReqIsFinalApprovalUser == false && GlbReqIsApprovalNeed == true)
                {
                    if (optApproved.Checked == true)
                    {
                        MessageBox.Show("This is a pending request. You cannot re-request!");
                        return;
                    }
                    if (MessageBox.Show("Do you want to send request?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    #region fill RequestApprovalHeader

                    RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                    ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_app_dt = DateTime.Now.Date;
                    ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdr.Grah_app_stus = "P";
                    ra_hdr.Grah_app_tp = _approvalCode.ToString();
                    ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_cre_dt = DateTime.Now.Date;
                    ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
                    ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// GlbUserDefLoca;
                    ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_mod_dt = DateTime.Now.Date;
                    ra_hdr.Grah_oth_loc = BaseCls.GlbUserDefProf;
                    ra_hdr.Grah_remaks = _documentType;

                    if (string.IsNullOrEmpty(ddlRequestNo.Text))
                    {
                        // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString(); commented by shani
                    }
                    else
                    {
                        ra_hdr.Grah_ref = ddlRequestNo.Text.ToString();
                    }

                    #endregion

                    #region fill RequestApprovalHeaderLog

                    RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                    ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_app_dt = DateTime.Now.Date;
                    ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdrLog.Grah_app_stus = "P";
                    ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
                    ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;//GlbUserComCode;
                    ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_cre_dt = DateTime.Now.Date;
                    ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
                    ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                    ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_mod_dt = DateTime.Now.Date;

                    ra_hdrLog.Grah_oth_loc = BaseCls.GlbUserDefProf;

                    ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                    ra_hdrLog.Grah_remaks = _documentType;

                    #endregion

                    #region fill List<RequestApprovalDetail> with Log
                    List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                    List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

                    Int32 _count = 1;

                    foreach (RecieptHeader _hdr in _recieptHeaderList)
                    {
                        if (!string.IsNullOrEmpty(_hdr.Sar_anal_5.ToString()))
                        {
                            if (_hdr.Sar_anal_5 > 0 && _hdr.Sar_anal_4 == "RV")
                            //if (_hdr.Sar_anal_5 > 0)
                            {
                                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                                RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                                //Request Details
                                ra_det.Grad_ref = ra_hdr.Grah_ref;
                                ra_det.Grad_line = _count;
                                ra_det.Grad_req_param = _documentType;
                                ra_det.Grad_val1 = _hdr.Sar_anal_5;                 // Refund Amount
                                ra_det.Grad_val2 = _hdr.Sar_tot_settle_amt;         // Receipt Amount
                                ra_det.Grad_anal1 = lblAccountNo.Text.ToString();   // Account No
                                ra_det.Grad_anal2 = _hdr.Sar_prefix;                // Prefix
                                ra_det.Grad_anal3 = _hdr.Sar_manual_ref_no;         // Manual Ref No
                                ra_det.Grad_anal4 = _hdr.Sar_receipt_no;            // Receipt No
                                ra_det.Grad_anal5 = _hdr.Sar_profit_center_cd;      // Profit Center
                                ra_det.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
                                ra_det_List.Add(ra_det);

                                //Request Details Log
                                ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                                ra_det_log.Grad_line = _count;
                                ra_det_log.Grad_req_param = _documentType;
                                ra_det_log.Grad_val1 = _hdr.Sar_anal_5;                 // Refund Amount
                                ra_det_log.Grad_val2 = _hdr.Sar_tot_settle_amt;         // Receipt Amount
                                ra_det_log.Grad_anal1 = lblAccountNo.Text.ToString();   // Account No
                                ra_det_log.Grad_anal2 = _hdr.Sar_prefix;                // Prefix
                                ra_det_log.Grad_anal3 = _hdr.Sar_manual_ref_no;         // Manual Ref No
                                ra_det_log.Grad_anal4 = _hdr.Sar_receipt_no;            // Receipt No
                                ra_det_log.Grad_anal5 = _hdr.Sar_profit_center_cd;      // Profit Center
                                ra_det_log.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
                                ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                                ra_detLog_List.Add(ra_det_log);
                                _count += 1;
                            }
                        }
                    }

                    if (GlbReqUserPermissionLevel == -1)
                    {
                        //throw new UIValidationException("Approval cycle definition is not setup! Please contact IT department..");
                        throw new Exception("Approval cycle definition is not setup! Please contact IT department..");
                    }
                    #endregion


                    string referenceNo;
                    // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                    Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                    if (eff > 0)
                    {
                        //string Msg = "<script>alert('Request Successfully Saved! Request No : " + referenceNo + "');window.location = 'HpReceiptReversal.aspx';</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Request Successfully Saved!\nRequest No :" + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnClear_Click(null, null);
                    }
                    else
                    {
                        //string Msg = "<script>alert('Request Fail!' );</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        MessageBox.Show("Request Failed!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (MessageBox.Show("Are you sure to save the reversal?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    //----------------------------------------------------------
                    List<RecieptHeader> Final_recieptHeaderList = new List<RecieptHeader>();
                    foreach (RecieptHeader ri in _recieptHeaderList)
                    {
                        ri.Sar_receipt_date = CurrDate.Value.Date;
                        ri.Sar_receipt_type = "HPREV";
                        //if (optRtnCheque.Checked == true)
                        //{
                        //    ri.Sar_receipt_type = "HPRCR";
                        //}
                        //------------------------------------------------------------------
                        //if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()) == false)
                        //{

                        //}
                        if (ri.Sar_anal_4 == "RV")
                        {
                            Final_recieptHeaderList.Add(ri);
                        }
                    }
                    //-----------------------------------------------------
                    try
                    {
                        if (Final_recieptHeaderList.Count < 1)
                        {
                            MessageBox.Show("Enter revert amounts!");
                            return;
                        }
                        //  int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, _isapprovedrequest, ddlRequestNo.Text.ToString());
                        //int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(Final_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, _isapprovedrequest, ddlRequestNo.Text.ToString());

                        string Refund_receipt;
                        int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(Final_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, true, ddlRequestNo.Text.ToString(), out Refund_receipt);
                        if (_ref == 1)
                        {
                            //string Msg = "<script>alert('Reversal Successfully Saved!');window.location = 'HpReceiptReversal.aspx';</script>";
                            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                            MessageBox.Show("Reversal Successfully Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.btnClear_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Reversal Not Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("System Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

            }
            //catch (UIValidationException ex)
            //{
            //    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //    MessageBox.Show("");
            //}
            catch (Exception e)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(e.Message);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveRequest();

        }

        private void optNewReq_CheckedChanged(object sender, EventArgs e)
        {
            //if (optNewReq.Checked == true)
            //{
            //    btnRequest.Enabled = true;
            //    btnSave.Enabled = false;
            //}
            //-------------------------------
            if (optNewReq.Checked == true)
            {
                GlbReqIsApprovalNeed = true;
                //***************************CLEANING*******************************
                ddlRequestNo.DataSource = null;

                gvItemDetail.DataSource = null;
                gvItemDetail.AutoGenerateColumns = false;
                gvItemDetail.DataSource = null;

                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;
                grvRefundHistory.DataSource = null;

                _recieptHeaderList = new List<RecieptHeader>();
                ucHpAccountSummary2.Clear();
                ucHpAccountDetail2.Clear();
                lblAccountNo.Text = "";
                AccountsList = null;
                //******************************************************************
            }
            //else
            //{
            //    GlbReqIsApprovalNeed = false;
            //}
        }

        private void optApproved_CheckedChanged(object sender, EventArgs e)
        {
            //if (optApproved.Checked == true)
            //{
            //    btnRequest.Enabled = false;
            //    btnSave.Enabled = true;
            //}
            //---------------------------------
            if (optApproved.Checked == true)
            {
                // GlbReqIsApprovalNeed = false;
            }

        }

        private void gvItemDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                Int32 rowIndex = e.RowIndex;
                DataGridViewRow grv = gvItemDetail.Rows[rowIndex];
                //string chequeNo = grv.Cells["SRCQ_CHQ"].Value.ToString();          
                // DataGridViewRow row = (DataGridViewRow)chkbox.NamingContainer;
                Int32 row = e.RowIndex;
                string _selectedReceiptNo = grv.Cells["SAR_RECEIPT_NO"].Value.ToString();//row.Cells["SAR_RECEIPT_NO"].Value.ToString();               

                //if (status == true)
                //{
                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;

                LoadOptReceipts(_selectedReceiptNo, grvRefundHistory);

                lblSelectedReceipt.Text = _selectedReceiptNo;
                //decimal refundBalAmt = Convert.ToDecimal(row.Cells[9].Text.ToString()) - Convert.ToDecimal(lblRefundTot.Text.ToString());                   
                decimal refundBalAmt = Convert.ToDecimal(grv.Cells["SAR_TOT_SETTLE_AMT"].Value.ToString()) - Convert.ToDecimal(grv.Cells["SAR_ANAL_5"].Value.ToString()); //Convert.ToDecimal(lblRefundTot.Text.ToString());

                lblRefundBalAmt.Text = FormatToCurrency(refundBalAmt.ToString());
                txtRefundAmt.Text = Convert.ToDecimal(grv.Cells["SAR_ANAL_5"].Value.ToString()).ToString();
                //mpeRefundHistory.Show();
                txtRefundAmt.Focus();
                //}
                lblRefundTot.Text = grv.Cells["SAR_ANAL_5"].Value.ToString();

                btnRefundAmt.Enabled = true;


                //-------------------------------------**----------------------------
                Bind_grvRefundHistory(_selectedReceiptNo);

                Decimal tot = 0;
                if (grvRefundHistory.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvRefundHistory.Rows)
                    {
                        tot = tot + Convert.ToDecimal(dgvr.Cells["h_SAR_TOT_SETTLE_AMT"].Value.ToString());
                    }
                }
                txtTotalRefund.Text = string.Format("{0:n2}", tot);
            }
            //-----------------------------------------------------------------------------------
        }

        private void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
            if (chkApproved.Checked == true)
            {
                btnRefundAmt.Enabled = false;
                btnSave.Enabled = true;
                btnRequest.Enabled = false;
                List<RequestApprovalHeader> _lst = null;
                if (optManagerIssue.Checked == true)
                {
                    _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccountNo.Text, HirePurchasModuleApprovalCode.ARQT002.ToString(), 1, GlbReqUserPermissionLevel);
                }
                else if (optRtnCheque.Checked == true)
                {
                    _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccountNo.Text, HirePurchasModuleApprovalCode.ARQT004.ToString(), 1, GlbReqUserPermissionLevel);
                }
                else if (optOthReceipt.Checked == true)
                {
                    _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccountNo.Text, HirePurchasModuleApprovalCode.ARQT015.ToString(), 1, GlbReqUserPermissionLevel);
                }
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        // _ddl.Items.Clear();
                        ddlRequestNo.DataSource = null;
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        var distinctS = new List<string>(query.Distinct());
                        ddlRequestNo.DataSource = distinctS;
                        // _ddl.DataBind();
                    }
                }
                else
                {
                    ddlRequestNo.DataSource = null;

                    gvItemDetail.AutoGenerateColumns = false;
                    gvItemDetail.DataSource = null;
                }
            }
            if (chkApproved.Checked == false)
            {
                btnRefundAmt.Enabled = true;
            }
        }

        private void ddlRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRequestNo.SelectedValue == null)
            {
                return;
            }
            // string reqNo = ddlRequestNo.SelectedText;
            BindRequestApprovalDetailLog(ddlRequestNo.SelectedValue.ToString());
            //  BindRequestApprovalDetailLog(reqNo);
            if (optApproved.Checked == true)
            {
                DisableCheckbox_gvItemDetail();
            }
        }
        protected void DisableCheckbox_gvItemDetail()
        {
            foreach (DataGridViewRow row in gvItemDetail.Rows)
            {
                //CheckBox ch = (CheckBox)row.FindControl("chkOneReceipt");

                //if (ch.Checked)
                //{
                //    CheckBox ch1 = (CheckBox)gvItemDetail.Rows[row.RowIndex].FindControl("chkOneReceipt");
                //    ch1.Enabled = false;
                //}
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            SaveRequest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRequest();
        }


        protected void btnRefundAmt_Click(object sender, EventArgs e)
        {
            decimal refundTot = 0;
            decimal refundBalAmt = 0;
            decimal refundAmt = 0;

            lblRefundHistoryMsg.Text = string.Empty;

            if (string.IsNullOrEmpty(txtRefundAmt.Text))
            {
                lblRefundHistoryMsg.Text = "Enter the refund amount!";
                txtRefundAmt.Focus();
                //mpeRefundHistory.Show(); TODO:
                return;
            }

            bool isNum = decimal.TryParse(txtRefundAmt.Text, out refundAmt);

            if (!isNum)
            {
                lblRefundHistoryMsg.Text = "Invalid refund amount!";
                txtRefundAmt.Focus();
                //mpeRefundHistory.Show(); TODO
                return;
            }

            refundTot = Convert.ToDecimal(lblRefundTot.Text.ToString());
            refundBalAmt = Convert.ToDecimal(lblRefundBalAmt.Text.ToString());
            refundAmt = Convert.ToDecimal(txtRefundAmt.Text.ToString());

            if (refundAmt <= 0)
            {
                lblRefundHistoryMsg.Text = "You can't enter the zero amount refund!";
                txtRefundAmt.Text = "";
                txtRefundAmt.Focus();
                // mpeRefundHistory.Show(); TODO
                return;
            }

            if (refundAmt > refundBalAmt)
            {
                lblRefundHistoryMsg.Text = "You can't exceed the refund balance amount!";
                txtRefundAmt.Text = "";
                txtRefundAmt.Focus();
                // mpeRefundHistory.Show(); TODO
                return;
            }

            var queryReceiptheaderList = from receiptheader in _recieptHeaderList
                                         where receiptheader.Sar_receipt_no == lblSelectedReceipt.Text.ToString()
                                         select receiptheader;

            foreach (var _lists in queryReceiptheaderList)
            {
                _lists.Sar_anal_5 = refundAmt;
                _lists.Sar_anal_4 = "RV";
            }

            //gvItemDetail.DataSource = _recieptHeaderList;
            //gvItemDetail.DataBind();

            lblRefundHistoryMsg.Text = string.Empty;
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void optManagerIssue_CheckedChanged(object sender, EventArgs e)
        {
            clearTabs();
            _ApprovalHeader_appr = null;
            bindDefaultValuesToGrids();

            if (optManagerIssue.Checked == true)
            {
                chkViewAdd.Checked = false;
                chkViewAdd.Visible = true;
            }
            else
            {
                chkViewAdd.Checked = false;
                chkViewAdd.Visible = false;
            }
           // panel_refundChq.Visible = false;
        }

        private void grvApprovedReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = grvApprovedReq.Rows[rowIndex];

                string Req_no = row.Cells["grah_ref"].Value.ToString();
                string ReciptNo = row.Cells["Grad_anal4"].Value.ToString();
                string AccountNo = row.Cells["Grah_fuc_cd"].Value.ToString();
                lblAccountNo.Text = AccountNo;

                Decimal apprAmt = 0;
                try
                {
                    apprAmt = Convert.ToDecimal(row.Cells["grad_val1"].Value.ToString());
                }
                catch (Exception ex)
                {

                }


                //  BindRequestApprovalDetailLog(Req_no);

                //-------------------------------------------------------------------------------------
                if (_ApprovalHeader_appr_list != null)
                {
                    foreach (RequestApprovalHeader hdr in _ApprovalHeader_appr_list)
                    {
                        if (AccountNo == hdr.Grah_fuc_cd && ReciptNo == hdr.Grad_anal4 && Req_no == hdr.Grah_ref)
                        {
                            List<RequestApprovalHeader> list = new List<RequestApprovalHeader>();
                            _ApprovalHeader_appr = hdr;
                            txtSaveReciptNo.Text = ReciptNo;
                            txtSaveRefundAmt.Text = string.Format("{0:n2}", apprAmt);//"0.00";
                            txtSaveReqNo.Text = Req_no;
                            //list.Add();
                        }
                    }
                }


                //************************load history**************************************************************
                string _selectedReceiptNo = ReciptNo;//row.Cells["SAR_RECEIPT_NO"].Value.ToString();               

                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;

                Bind_grvRefundHistory(ReciptNo);

                Decimal tot = 0;
                if (grvRefundHistory.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvRefundHistory.Rows)
                    {
                        tot = tot + Convert.ToDecimal(dgvr.Cells["h_SAR_TOT_SETTLE_AMT"].Value.ToString());
                    }
                }
                txtTotalRefund.Text = string.Format("{0:n2}", tot);


                //--------------------------add--------------------------------------
                Decimal tot_chqAmt = 0;
                List<RecieptItem> rec_itmList = CHNLSVC.Sales.GetAllReceiptItems(ReciptNo);
                foreach (RecieptItem rec_itm in rec_itmList)
                {
                    if (rec_itm.Sard_pay_tp == "CHEQUE")
                    {
                        tot_chqAmt = tot_chqAmt + rec_itm.Sard_settle_amt;
                    }
                }
                txtCheqAmt.Text = string.Format("{0:n2}", tot_chqAmt);
                //-----------------------------------------------------------------------
            }
        }

        private void grvPendingReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = grvPendingReq.Rows[rowIndex];
                string AccountNo = row.Cells["pend_Grah_fuc_cd"].Value.ToString();
                string Req_no = row.Cells["pend_Grah_ref"].Value.ToString();
                string ReciptNo = row.Cells["pend_Grad_anal4"].Value.ToString();
                Decimal reqAmt = 0;
                try
                {
                    reqAmt = Convert.ToDecimal(row.Cells["pend_Grad_val1"].Value.ToString());
                }
                catch (Exception ex)
                {

                }
                lblAccountNo.Text = AccountNo;
                txtApprReciptNo.Text = ReciptNo;
                txtApprRefundAmt.Text = string.Format("{0:n2}", reqAmt);
                //BindRequestApprovalDetailLog(Req_no);
                //----------------------------------------------------------------------------------------
                // _recieptHeader_appr
                if (_RecieptHeader_appr_list != null)
                {
                    foreach (RequestApprovalHeader hdr in _RecieptHeader_appr_list)
                    {
                        if (AccountNo == hdr.Grah_fuc_cd && ReciptNo == hdr.Grad_anal4)
                        {
                            List<RequestApprovalHeader> list = new List<RequestApprovalHeader>();
                            _RecieptHeader_appr = hdr;
                            txtApprReciptNo.Text = ReciptNo;
                            txtApprRefundAmt.Text = string.Format("{0:n2}", reqAmt);//"0.00";
                            //list.Add();
                        }
                    }
                }

                //************************load history**************************************************************
                string _selectedReceiptNo = ReciptNo;//row.Cells["SAR_RECEIPT_NO"].Value.ToString();               

                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;

                Bind_grvRefundHistory(ReciptNo);

                Decimal tot = 0;
                if (grvRefundHistory.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvRefundHistory.Rows)
                    {
                        tot = tot + Convert.ToDecimal(dgvr.Cells["h_SAR_TOT_SETTLE_AMT"].Value.ToString());
                    }
                }
                txtTotalRefund.Text = string.Format("{0:n2}", tot);

             //--------------------------add--------------------------------------
                Decimal tot_chqAmt = 0;
               List<RecieptItem>rec_itmList = CHNLSVC.Sales.GetAllReceiptItems(ReciptNo);
               foreach(RecieptItem rec_itm in rec_itmList)
               {
                    if (rec_itm.Sard_pay_tp == "CHEQUE")
                    {
                        tot_chqAmt = tot_chqAmt + rec_itm.Sard_settle_amt;
                    }
               }
               txtCheqAmt.Text = string.Format("{0:n2}", tot_chqAmt);
             //-----------------------------------------------------------------------
            }
        }

        private void btnSendReq_Click(object sender, EventArgs e)
        {
            //send request
            //if (grvReqReceipt.Rows.Count<1)
            //{
            //    MessageBox.Show("Select receipt first!","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //    return;
            //}
            
            //------------------------------------------------------------
            List<RecieptHeader> receipts = CHNLSVC.Sales.GetReceiptHdr(txtReqReciptNo.Text.Trim());

            if (!IsNumeric(txtReqRefundAmt.Text))
            {
                MessageBox.Show("Invalid refund amount.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Text = "";
                txtReqRefundAmt.Focus();
                return;
            }

            if (Convert.ToDecimal(txtReqRefundAmt.Text) < 0)
            {
                MessageBox.Show("Invalid refund amount.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Text = "";
                txtReqRefundAmt.Focus();
                return;
            }

            Decimal Refunded_total = Convert.ToDecimal(txtTotalRefund.Text.Trim());

            if (receipts[0].Sar_tot_settle_amt < (Convert.ToDecimal(txtReqRefundAmt.Text.Trim()) + Refunded_total))
            {
                MessageBox.Show("Cannot request more than the total receipt amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (optRtnCheque.Checked==true)
            {
                Decimal tot_receiptChqAmt = Convert.ToDecimal(txtCheqAmt.Text.Trim());
                Decimal tot_refundChqAmt = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim()); 
                
                if (tot_refundChqAmt >= tot_receiptChqAmt)
                {
                    MessageBox.Show("Receipt's cheque amount already refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                //-------------------------

                Decimal cheq_Refunded_total = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim());
                Decimal cheq_reciept_total = Convert.ToDecimal(txtCheqAmt.Text.Trim());

                Decimal leftamount= cheq_reciept_total -cheq_Refunded_total;
                string leftAmt = string.Format("{0:n2}", leftamount);//leftamount.ToString();
                if (cheq_reciept_total < (Convert.ToDecimal(txtReqRefundAmt.Text.Trim()) + cheq_Refunded_total))
                {
                    MessageBox.Show("Cannot exceed the receipt's cheque amount!\n Maximum Reuqest amount: " + leftAmt, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


            }
            //------------------------------------------------------------

            if (txtReqReciptNo.Text.Trim() == "")
            {
                MessageBox.Show("Select receipt first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }
            if (txtReqRefundAmt.Text.Trim() == "")
            {
                MessageBox.Show("Enter refund amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }
            try
            {
                Convert.ToDecimal(txtReqRefundAmt.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid refund amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }
            string _approvalCode = "";
            string _documentType = "";

            if (optManagerIssue.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT002.ToString();
                _documentType = "MANAGER_ISSUE";

                //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT002", lblAccountNo.Text.Trim()))
                //{
                //    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            if (optRtnCheque.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT004.ToString();
                _documentType = "RETURN_CHEQUE";

                //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT004", lblAccountNo.Text.Trim()))
                //{
                //    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            if (optOthReceipt.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT015.ToString();
                _documentType = "OTHER_RECEIPT";

                //if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT015", lblAccountNo.Text.Trim()))
                //{
                //    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            if (MessageBox.Show("Do you want to send request?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            GetUserAppLevel();

            #region fill RequestApprovalHeader

            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_app_by = BaseCls.GlbUserID;
            ra_hdr.Grah_app_dt = DateTime.Now.Date;
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdr.Grah_app_stus = "P";
            ra_hdr.Grah_app_tp = _approvalCode.ToString();
            ra_hdr.Grah_com = BaseCls.GlbUserComCode;
            ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdr.Grah_cre_dt = DateTime.Now.Date;
            ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
            ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// GlbUserDefLoca;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = DateTime.Now.Date;
            ra_hdr.Grah_oth_loc = BaseCls.GlbUserDefProf;
            ra_hdr.Grah_remaks = _documentType;

            ra_hdr.Grah_ref = ""; //TODO:

            #endregion

            #region fill RequestApprovalHeaderLog

            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_app_dt = DateTime.Now.Date;
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdrLog.Grah_app_stus = "P";
            ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
            ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;//GlbUserComCode;
            ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_cre_dt = DateTime.Now.Date;
            ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
            ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_mod_dt = DateTime.Now.Date;

            ra_hdrLog.Grah_oth_loc = BaseCls.GlbUserDefProf;

            ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
            ra_hdrLog.Grah_remaks = _documentType;

            #endregion


            #region fill List<RequestApprovalDetail> with Log
            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

            Int32 _count = 1;

            //foreach (RecieptHeader _hdr in _recieptHeaderList)
            //{
            //    if (!string.IsNullOrEmpty(_hdr.Sar_anal_5.ToString()))
            //    {
            //        if (_hdr.Sar_anal_5 > 0 && _hdr.Sar_anal_4 == "RV")                   
            //        {
            RequestApprovalDetail ra_det = new RequestApprovalDetail();
            RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

            //Request Details
            ra_det.Grad_ref = ra_hdr.Grah_ref;
            ra_det.Grad_line = _count;
            ra_det.Grad_req_param = _documentType;
            ra_det.Grad_val1 = Convert.ToDecimal(txtReqRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount
            ra_det.Grad_val2 = _recieptHeader_req.Sar_tot_settle_amt;         // Receipt Amount
            ra_det.Grad_anal1 = lblAccountNo.Text.ToString();   // Account No
            ra_det.Grad_anal2 = _recieptHeader_req.Sar_prefix;                // Prefix
            ra_det.Grad_anal3 = _recieptHeader_req.Sar_manual_ref_no;         // Manual Ref No
            ra_det.Grad_anal4 = _recieptHeader_req.Sar_receipt_no;            // Receipt No
            ra_det.Grad_anal5 = _recieptHeader_req.Sar_profit_center_cd;      // Profit Center
            ra_det.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
            ra_det_List.Add(ra_det);

            //Request Details Log
            ra_det_log.Grad_ref = ra_hdr.Grah_ref;
            ra_det_log.Grad_line = _count;
            ra_det_log.Grad_req_param = _documentType;
            ra_det_log.Grad_val1 = Convert.ToDecimal(txtReqRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount
            ra_det_log.Grad_val2 = _recieptHeader_req.Sar_tot_settle_amt;         // Receipt Amount
            ra_det_log.Grad_anal1 = lblAccountNo.Text.ToString();   // Account No
            ra_det_log.Grad_anal2 = _recieptHeader_req.Sar_prefix;                // Prefix
            ra_det_log.Grad_anal3 = _recieptHeader_req.Sar_manual_ref_no;         // Manual Ref No
            ra_det_log.Grad_anal4 = _recieptHeader_req.Sar_receipt_no;            // Receipt No
            ra_det_log.Grad_anal5 = _recieptHeader_req.Sar_profit_center_cd;      // Profit Center
            ra_det_log.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
            ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
            ra_detLog_List.Add(ra_det_log);
            _count += 1;
            //        }
            //    }
            //}
            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "HPRVREQ";
            _ReqAppAuto.Aut_year = null;
            //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);

            string referenceNo;
            string reqStatus;

            // Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo);
            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(_ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);

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

        private void grvAllReceipts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = grvAllReceipts.Rows[rowIndex];

                string ReciptNo = row.Cells["new_SAR_RECEIPT_NO"].Value.ToString();
                // ddlApprVehIns_req.SelectedItem = Req_no;
                string AccountNo = row.Cells["new_SAR_ACC_NO"].Value.ToString();

                //List<RecieptHeader> RECHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(lblAccountNo.Text.Trim(), _refundType);
                foreach (RecieptHeader hdr in _RecieptHeader_req_list)
                {
                    if (AccountNo == hdr.Sar_acc_no && ReciptNo == hdr.Sar_receipt_no)
                    {
                        List<RecieptHeader> list = new List<RecieptHeader>();
                        _recieptHeader_req = hdr;
                        txtReqReciptNo.Text = ReciptNo;
                        txtReqRefundAmt.Text = "0.00";
                        //list.Add();
                    }
                }
                // BindRequestApprovalDetailLog(AccountNo);

                //************************load history**************************************************************
                string _selectedReceiptNo = ReciptNo;//row.Cells["SAR_RECEIPT_NO"].Value.ToString();               

                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;

                Bind_grvRefundHistory(ReciptNo);

                Decimal tot = 0;
                if (grvRefundHistory.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvRefundHistory.Rows)
                    {
                        tot = tot + Convert.ToDecimal(dgvr.Cells["h_SAR_TOT_SETTLE_AMT"].Value.ToString());
                    }
                }
                txtTotalRefund.Text = string.Format("{0:n2}", tot);

                //---------------------------------------
                //--------------------------add--------------------------------------
                Decimal tot_chqAmt = 0;
                List<RecieptItem> rec_itmList = CHNLSVC.Sales.GetAllReceiptItems(ReciptNo);
                foreach (RecieptItem rec_itm in rec_itmList)
                {
                    if (rec_itm.Sard_pay_tp == "CHEQUE")
                    {
                        tot_chqAmt = tot_chqAmt + rec_itm.Sard_settle_amt;
                    }
                }
                txtCheqAmt.Text = string.Format("{0:n2}", tot_chqAmt);
                //-----------------------------------------------------------------------
            }
        }

        private void grvPendingReq_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = grvPendingReq.Rows[rowIndex];
                string AccountNo = row.Cells["pend_Grah_fuc_cd"].Value.ToString();
                string ReceiptNp = row.Cells["pend_Grad_anal4"].Value.ToString();
                Decimal reqAmt = 0;
                try
                {
                    reqAmt = Convert.ToDecimal(row.Cells["pend_Grad_val1"].Value.ToString());
                }
                catch (Exception ex)
                {
                }

                lblAccountNo.Text = AccountNo;
                txtApprReciptNo.Text = ReceiptNp;
                txtReqRefundAmt.Text = string.Format("{0:n2}", reqAmt);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnApprove_Click_1(object sender, EventArgs e)
        {
            if (txtApprReciptNo.Text.Trim() == "")
            {
                MessageBox.Show("Select receipt first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }
            if (txtApprRefundAmt.Text.Trim() == "")
            {
                MessageBox.Show("Enter refund amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }
            //----------------------------------------------------------------------------
            if (_RecieptHeader_appr.Description == "RETURN_CHEQUE")
            {
                Decimal tot_refundChqAmt = Convert.ToDecimal(txtCheqAmt.Text.Trim());
                Decimal tot_receiptChqAmt = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim());
                if (tot_refundChqAmt >= tot_receiptChqAmt)
                {
                    MessageBox.Show("Receipt's cheque amount already refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //-------------------------

                Decimal cheq_Refunded_total = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim());
                Decimal cheq_reciept_total = Convert.ToDecimal(txtCheqAmt.Text.Trim());

                Decimal leftamount = cheq_reciept_total - cheq_Refunded_total;
                string leftAmt = string.Format("{0:n2}", leftamount);//leftamount.ToString();

                if (cheq_reciept_total < (Convert.ToDecimal(txtApprRefundAmt.Text.Trim()) + cheq_Refunded_total))
                {
                    MessageBox.Show("Cannot refund more than the receipt's cheque amount!\nMaximum approve amount: " + leftAmt, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //----------------------------------------------------------------------------
            try
            {
                Convert.ToDecimal(txtApprRefundAmt.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid refund amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReqRefundAmt.Focus();
                return;
            }

            string _approvalCode = "";
            string _documentType = "";

            if (optManagerIssue.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT002.ToString();
                _documentType = "MANAGER_ISSUE";
            }
            if (optRtnCheque.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT004.ToString();
                _documentType = "RETURN_CHEQUE";
            }
            if (optOthReceipt.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT015.ToString();
                _documentType = "OTHER_RECEIPT";
            }

            //-------------------------------------------------------------

            GetUserAppLevel();

            string request_No = _RecieptHeader_appr.Grah_ref;
            RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);

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

            if (MessageBox.Show("Are you sure to approve ?", "Confirm approve", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            #region fill RequestApprovalHeader
            RequestApprovalHeader ra_hdr = REQ_HEADER;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
            ra_hdr.Grah_app_dt = currentDate.Date;
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
            ra_hdr.Grah_app_by = BaseCls.GlbUserID;
            ra_hdr.Grah_ref = request_No;
            #endregion
            #region fill RequestApprovalHeaderLog
            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;//REQ_HEADER.Grah_app_by;
            ra_hdrLog.Grah_app_dt = currentDate;//REQ_HEADER.Grah_app_dt;
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//REQ_HEADER.Grah_app_lvl;
            ra_hdrLog.Grah_app_stus = "A";// REQ_HEADER.Grah_app_stus;
            ra_hdrLog.Grah_app_tp = REQ_HEADER.Grah_app_tp;
            ra_hdrLog.Grah_com = REQ_HEADER.Grah_com;
            ra_hdrLog.Grah_cre_by = REQ_HEADER.Grah_cre_by;
            ra_hdrLog.Grah_cre_dt = REQ_HEADER.Grah_cre_dt;
            ra_hdrLog.Grah_fuc_cd = REQ_HEADER.Grah_fuc_cd;
            ra_hdrLog.Grah_loc = REQ_HEADER.Grah_loc;

            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;//REQ_HEADER.Grah_mod_by;
            ra_hdrLog.Grah_mod_dt = currentDate; //REQ_HEADER.Grah_mod_dt;
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
            #endregion

            List<RequestApprovalDetail> ra_det_List = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

            Int32 _count = 1;
            #region fill List<RequestApprovalDetail>
            //List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            foreach (RequestApprovalDetail ra_det in ra_det_List)
            {
                //Request Details
                // ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = _count;
                ra_det.Grad_req_param = _documentType;
                ra_det.Grad_val1 = Convert.ToDecimal(txtApprRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount

                RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();
                //Request Details Log
                ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                ra_det_log.Grad_line = _count;
                ra_det_log.Grad_req_param = _documentType;
                ra_det_log.Grad_val1 = Convert.ToDecimal(txtApprRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount
                ra_det_log.Grad_val2 = ra_det.Grad_val2;         // Receipt Amount
                ra_det_log.Grad_anal1 = ra_det.Grad_anal1;   // Account No
                ra_det_log.Grad_anal2 = ra_det.Grad_anal2;                // Prefix
                ra_det_log.Grad_anal3 = ra_det.Grad_anal3;         // Manual Ref No
                ra_det_log.Grad_anal4 = ra_det.Grad_anal4;            // Receipt No
                ra_det_log.Grad_anal5 = ra_det.Grad_anal5;      // Profit Center
                ra_det_log.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
                ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_det_log);

                _count += 1;
            }
            #endregion

            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "HPRVREQ";
            _ReqAppAuto.Aut_year = null;
            //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);

            string referenceNo;
            string reqStatus;
            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);

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

            }
            else
            {
                MessageBox.Show("Sorry. Request not updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.btnClear_Click(sender, e);
        }

        private void btnApprSave_Click(object sender, EventArgs e)
        {
            if (CheckServerDateTime() == false) return;

            if (txtSaveReciptNo.Text.Trim() == "")
            {
                MessageBox.Show("Select receipt first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                return;
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, pickedDate, lblBackDateInfor, pickedDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (pickedDate.Value.Date != DateTime.Now.Date)
                    {
                        pickedDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pickedDate.Focus();
                        return;
                    }
                }
                else
                {
                    pickedDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date!", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pickedDate.Focus();
                    return;
                }
            }


            //------------------------------------------------------------
            List<RecieptHeader> receipts = CHNLSVC.Sales.GetReceiptHdr(txtSaveReciptNo.Text.Trim());

            Decimal Refunded_total = Convert.ToDecimal(txtTotalRefund.Text.Trim());

            if (receipts[0].Sar_tot_settle_amt < (Convert.ToDecimal(txtSaveRefundAmt.Text.Trim()) + Refunded_total))
            {
                MessageBox.Show("Cannot refund more than the total receipt amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_ApprovalHeader_appr.Description == "RETURN_CHEQUE")
            {
                Decimal tot_refundChqAmt = Convert.ToDecimal(txtCheqAmt.Text.Trim());
                Decimal tot_receiptChqAmt = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim());
                if (tot_refundChqAmt >= tot_receiptChqAmt)
                {
                    MessageBox.Show("Receipt's cheque amount already refunded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //-------------------------

                Decimal cheq_Refunded_total = Convert.ToDecimal(txtTotRtnChqAmt.Text.Trim());
                Decimal cheq_reciept_total=  Convert.ToDecimal(txtCheqAmt.Text.Trim());

                Decimal leftamount = cheq_reciept_total - cheq_Refunded_total;
                string leftAmt = string.Format("{0:n2}", leftamount);//leftamount.ToString();

                if (cheq_reciept_total < (Convert.ToDecimal(txtSaveRefundAmt.Text.Trim()) + cheq_Refunded_total))
                {
                    MessageBox.Show("Cannot refund more than the receipt's cheque amount!\nAmount can be refunded:" + leftAmt, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //------------------------------------------------------------

            if (MessageBox.Show("Are you sure to save the reversal?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //----------------------------------------------------------

            List<RecieptHeader> Final_recieptHeaderList = new List<RecieptHeader>();

            RecieptHeader _rhdr = new RecieptHeader();
            _rhdr.Sar_com_cd = BaseCls.GlbUserComCode;// GlbCompany;
            _rhdr.Sar_session_id = BaseCls.GlbUserSessionID; // add by akila 2017/12/26
            _rhdr.Sar_receipt_type = "";
            _rhdr.Sar_profit_center_cd = _ApprovalHeader_appr.Grad_anal5;
            _rhdr.Sar_acc_no = _ApprovalHeader_appr.Grad_anal1;
            _rhdr.Sar_receipt_no = _ApprovalHeader_appr.Grad_anal4;//grad_anal4
            _rhdr.Sar_receipt_date = _ApprovalHeader_appr.Grad_date_param.Date;
            _rhdr.Sar_manual_ref_no = _ApprovalHeader_appr.Grad_anal3;
            _rhdr.Sar_prefix = _ApprovalHeader_appr.Grad_anal2;
            _rhdr.Sar_tot_settle_amt = _ApprovalHeader_appr.Grad_val1;//_ApprovalHeader_appr.Grad_val2;
            _rhdr.Sar_anal_5 = receipts[0].Sar_anal_5;//_ApprovalHeader_appr.Grad_val1;
            _rhdr.Sar_comm_amt = (receipts[0].Sar_anal_5 * _ApprovalHeader_appr.Grad_val1)/100; //COMMISSION
            _rhdr.Sar_anal_1 = _ApprovalHeader_appr.Grad_req_param;
            _rhdr.Sar_anal_2 = _ApprovalHeader_appr.Grad_anal4;
            _rhdr.Sar_create_by = BaseCls.GlbUserID;
            _rhdr.Sar_create_when = currentDate.Date;
            //  _rhdrList.Add(_rhdr);
            _rhdr.Sar_receipt_date = pickedDate.Value.Date;//CurrDate.Value.Date;
            _rhdr.Sar_receipt_type = "HPREV";
            //if (optRtnCheque.Checked == true)
            //{
            //    _rhdr.Sar_receipt_type = "HPRCR";
            //}
            Final_recieptHeaderList.Add(_rhdr);
            //------------------------------------------------------------------
            //if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()) == false)
            //{

            //}
            //if (_rhdr.Sar_anal_4 == "RV")
            //{
            //    Final_recieptHeaderList.Add(_rhdr);
            //}    

            //foreach (RecieptHeader ri in _recieptHeaderList)
            //{

            //    ri.Sar_receipt_date = CurrDate.Value.Date;
            //    ri.Sar_receipt_type = "HPREV";
            //    if (optRtnCheque.Checked == true)
            //    {
            //        ri.Sar_receipt_type = "HPRCR";
            //    }
            //    //------------------------------------------------------------------
            //    //if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()) == false)
            //    //{

            //    //}
            //    if (ri.Sar_anal_4 == "RV")
            //    {
            //        Final_recieptHeaderList.Add(ri);
            //    }
            //}
            //-----------------------------------------------------
            try
            {
                if (Final_recieptHeaderList.Count < 1)
                {
                    MessageBox.Show("Enter revert amounts!");
                    return;
                }
                //  int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, _isapprovedrequest, ddlRequestNo.Text.ToString());
                //int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(Final_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, _isapprovedrequest, ddlRequestNo.Text.ToString());

                //int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(Final_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, true, ddlRequestNo.Text.ToString());

                string RefundReceipt = "";
                int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(Final_recieptHeaderList, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, true, txtSaveReqNo.Text, out RefundReceipt);
                if (_ref == 1)
                {
                    //string Msg = "<script>alert('Reversal Successfully Saved!');window.location = 'HpReceiptReversal.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    try
                    {
                        string Req_no = txtSaveReqNo.Text;//appreqheader.Grah_ref;
                        RequestApprovalHeader REQ_HEADER_toCompleate = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Req_no);
                        REQ_HEADER_toCompleate.Grah_app_stus = "F";
                        Int32 eff = CHNLSVC.General.UpdateApprovalStatus(REQ_HEADER_toCompleate);
                    }
                    catch (Exception ex)
                    {

                    }

                    MessageBox.Show("Reversal Successfully Saved!\nRefund Receipt:" + RefundReceipt, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Reversal Not Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        //--------------------------------------------------------------------------------------------------------------------------------------
        private void Bind_gvItemDetail()
        {
            string _refundType = "";
            if (optManagerIssue.Checked == true)
            {
                _refundType = "MI_NEW";
            }
            else if (optRtnCheque.Checked == true)
            {
                _refundType = "RC_NEW";
            }
            else if (optOthReceipt.Checked == true)
            {
                _refundType = "OR_NEW";
            }

            //----------------------------------------------------------------------------------------------
            List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(lblAccountNo.Text, _refundType);
            if (_RecieptHdrList != null)
            {
                foreach (RecieptHeader _hdr in _RecieptHdrList)
                {
                    if (optManagerIssue.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "MANAGER_ISSUE";
                    }
                    else if (optRtnCheque.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "RETURN_CHEQUE";
                    }
                    else if (optOthReceipt.Checked == true)
                    {
                        _hdr.Sar_anal_1 = "OTHER_RECEIPT";
                    }
                    _hdr.Sar_anal_5 = 0;
                }
            }

            // _recieptHeaderList = _RecieptHdrList;
            gvItemDetail.DataSource = null;
            gvItemDetail.AutoGenerateColumns = false;
            gvItemDetail.DataSource = _RecieptHdrList;

            //if (_dvname.Name == "grvRefundHistory")
            //{
            //    _dvname.DataSource = null;
            //    _dvname.AutoGenerateColumns = false;
            //    _dvname.DataSource = _RecieptHdrList;
            //    var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
            //                        group _receiptHdr by new { _receiptHdr.Sar_receipt_no } into itm
            //                        select new { Receipt_no = itm.Key.Sar_receipt_no, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

            //    foreach (var _lists in _tbreceiptHdr)
            //    {
            //        //lblRefundTot.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(_lists.Receipt_Tot.ToString()));
            //        lblRefundTot.Text = FormatToCurrency(_lists.Receipt_Tot.ToString());
            //    }
            //}
            //else if (_dvname.Name == "gvItemDetail")
            //{
            //    _recieptHeaderList = _RecieptHdrList;
            //    _dvname.DataSource = null;
            //    _dvname.AutoGenerateColumns = false;
            //    _dvname.DataSource = _recieptHeaderList;
            //}


        }


        private void Bind_grvRefundHistory(string receiptNo)
        {
            txtTotRtnChqAmt.Text = string.Format("{0:n2}", 0);

            string _refundType = "";
            if (optManagerIssue.Checked == true) //Reversals
            {
                _refundType = "MI_NEW_REV";
            }
            if (optRtnCheque.Checked == true) //Reversals
            {
                _refundType = "RC_NEW_REV";
            }
            if (optOthReceipt.Checked == true) //Reversals
            {
                _refundType = "OR_NEW_REV";
            }

            //----------------------------------------------------------------------------------------------

            // List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(receiptNo, _refundType); //COMMENT 24-04-2013
            
            //---------------------------24-04-2013----ADD----------------------------------------------------------------------------------
            List<RecieptHeader> _RecieptHdrList = CHNLSVC.Sales.Get_ReceiptHeaderListALL(receiptNo, "MI_NEW_REV");
            List<RecieptHeader> _RecieptHdrList2 = CHNLSVC.Sales.Get_ReceiptHeaderListALL(receiptNo, "OR_NEW_REV");
            if (_RecieptHdrList == null)
            {
                //if (_RecieptHdrList2 != null)
                //{
                    _RecieptHdrList = _RecieptHdrList2;
                //}               
            }
            else
            {
                if (_RecieptHdrList2!=null)
                {
                    _RecieptHdrList.AddRange(_RecieptHdrList2);
                }
               
            }

           //-------------------------------------------------------------------------------------------------------------------
            if (_RecieptHdrList != null)
            {
                //------------------COMMENTED--24-04-2013---------------------------------------
                //foreach (RecieptHeader _hdr in _RecieptHdrList)
                //{
                //    if (optManagerIssue.Checked == true)
                //    {
                //        _hdr.Sar_anal_1 = "MANAGER_ISSUE";
                //    }
                //    else if (optRtnCheque.Checked == true)
                //    {
                //        _hdr.Sar_anal_1 = "RETURN_CHEQUE";
                //    }
                //    else if (optOthReceipt.Checked == true)
                //    {
                //        _hdr.Sar_anal_1 = "OTHER_RECEIPT";
                //    }
                //    _hdr.Sar_anal_5 = 0;
                //}
                //------------------COMMENTED-----------------------------------------
                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;
                grvRefundHistory.DataSource = _RecieptHdrList;
                var _tbreceiptHdr = from _receiptHdr in _RecieptHdrList
                                    group _receiptHdr by new { _receiptHdr.Sar_receipt_no } into itm
                                    select new { Receipt_no = itm.Key.Sar_receipt_no, Receipt_Tot = itm.Sum(p => p.Sar_tot_settle_amt) };

                foreach (var _lists in _tbreceiptHdr)
                {
                    lblRefundTot.Text = FormatToCurrency(_lists.Receipt_Tot.ToString());
                }

                //---------------------------------add----------------------------------------------
                Decimal return_chq_amt = 0;
                foreach(RecieptHeader rh in _RecieptHdrList)
                {
                    if (rh.Sar_anal_1 == "RETURN_CHEQUE")
                    {
                        return_chq_amt = return_chq_amt + rh.Sar_tot_settle_amt;
                    }
                }
                txtTotRtnChqAmt.Text = string.Format("{0:n2}", return_chq_amt);

             
                //----------------------------------------------------------------------------------
            }
            else
            {
                grvRefundHistory.DataSource = null;
                grvRefundHistory.AutoGenerateColumns = false;
                grvRefundHistory.DataSource = _RecieptHdrList;

                lblRefundTot.Text = "0.00";
            }

        }

        private void optRtnCheque_CheckedChanged(object sender, EventArgs e)
        {
            clearTabs();
            _ApprovalHeader_appr = null;
            bindDefaultValuesToGrids();
            //panel_refundChq.Visible = true;
        }

        private void optOthReceipt_CheckedChanged(object sender, EventArgs e)
        {
            clearTabs();
            _ApprovalHeader_appr = null;
            bindDefaultValuesToGrids();
           // panel_refundChq.Visible = false;
        }

        private void btnRejectReq_Click(object sender, EventArgs e)
        {
            string _approvalCode = "";
            string _documentType = "";

            if (optManagerIssue.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT002.ToString();
                _documentType = "MANAGER_ISSUE";
            }
            if (optRtnCheque.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT004.ToString();
                _documentType = "RETURN_CHEQUE";
            }
            if (optOthReceipt.Checked == true)
            {
                _approvalCode = HirePurchasModuleApprovalCode.ARQT015.ToString();
                _documentType = "OTHER_RECEIPT";
            }

            GetUserAppLevel();

            string request_No = _RecieptHeader_appr.Grah_ref;
            RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);

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

            if (MessageBox.Show("Are you sure to reject ?", "Confirm reject", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            #region fill RequestApprovalHeader
            RequestApprovalHeader ra_hdr = REQ_HEADER;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
            ra_hdr.Grah_app_dt = currentDate.Date;
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
            ra_hdr.Grah_app_by = BaseCls.GlbUserID;
            ra_hdr.Grah_ref = request_No;
            #endregion
            #region fill RequestApprovalHeaderLog
            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;//REQ_HEADER.Grah_app_by;
            ra_hdrLog.Grah_app_dt = currentDate;//REQ_HEADER.Grah_app_dt;
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//REQ_HEADER.Grah_app_lvl;
            ra_hdrLog.Grah_app_stus = "R";// REQ_HEADER.Grah_app_stus;
            ra_hdrLog.Grah_app_tp = REQ_HEADER.Grah_app_tp;
            ra_hdrLog.Grah_com = REQ_HEADER.Grah_com;
            ra_hdrLog.Grah_cre_by = REQ_HEADER.Grah_cre_by;
            ra_hdrLog.Grah_cre_dt = REQ_HEADER.Grah_cre_dt;
            ra_hdrLog.Grah_fuc_cd = REQ_HEADER.Grah_fuc_cd;
            ra_hdrLog.Grah_loc = REQ_HEADER.Grah_loc;

            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;//REQ_HEADER.Grah_mod_by;
            ra_hdrLog.Grah_mod_dt = currentDate; //REQ_HEADER.Grah_mod_dt;
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
            #endregion

            List<RequestApprovalDetail> ra_det_List = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, request_No);
            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

            Int32 _count = 1;
            #region fill List<RequestApprovalDetail>
            //List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            foreach (RequestApprovalDetail ra_det in ra_det_List)
            {
                //Request Details
                // ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = _count;
                ra_det.Grad_req_param = _documentType;
                ra_det.Grad_val1 = Convert.ToDecimal(txtApprRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount

                RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();
                //Request Details Log
                ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                ra_det_log.Grad_line = _count;
                ra_det_log.Grad_req_param = _documentType;
                ra_det_log.Grad_val1 = Convert.ToDecimal(txtApprRefundAmt.Text.Trim());//_recieptHeader_req.Sar_anal_5;                 // Refund Amount
                ra_det_log.Grad_val2 = ra_det.Grad_val2;         // Receipt Amount
                ra_det_log.Grad_anal1 = ra_det.Grad_anal1;   // Account No
                ra_det_log.Grad_anal2 = ra_det.Grad_anal2;                // Prefix
                ra_det_log.Grad_anal3 = ra_det.Grad_anal3;         // Manual Ref No
                ra_det_log.Grad_anal4 = ra_det.Grad_anal4;            // Receipt No
                ra_det_log.Grad_anal5 = ra_det.Grad_anal5;      // Profit Center
                ra_det_log.Grad_date_param = DateTime.Now.AddDays(10).Date; //As per the dilanda's request on 03-08-2012
                ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_det_log);

                _count += 1;
            }
            #endregion

            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "HPRVREQ";
            _ReqAppAuto.Aut_year = null;
            //string reqNo = CHNLSVC.Sales.GetRecieptNo(_ReqAppAuto);

            string referenceNo;
            string reqStatus;
            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(null, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo, out reqStatus);

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
                        //this.btnClear_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Not Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // this.btnClear_Click(sender, e);
                }
                //--------------------------------------------------------                          
            }
            else
            {
                MessageBox.Show("Not Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.btnClear_Click(sender, e);
        }

        private void clearTabs()
        {
            txtSaveReciptNo.Text = "";
            txtSaveRefundAmt.Text = "";
            txtSaveReqNo.Text = "";

            txtApprReciptNo.Text = "";
            txtApprRefundAmt.Text = "";

            txtReqReciptNo.Text = "";
            txtReqRefundAmt.Text = "";

            grvRefundHistory.DataSource = null;
            grvRefundHistory.AutoGenerateColumns = false;
            chkViewAdd.Visible = false;
            txtTotalRefund.Text = "";
        }

        private void chkViewAdd_CheckedChanged(object sender, EventArgs e)
        {
            decimal _HireVal = 0;
            decimal _HalfHire=0;
            decimal _mgrPay = 0;
            decimal _cusPay = 0;
            decimal _totPay = 0;

            if (chkViewAdd.Checked == false)
            {
                lblHireVal.Text = "0.00";
                lblHalfHire.Text = "0.00";
                lblCusPay.Text = "0.00";
                lblManPay.Text = "0.00";
                lblTotPaid.Text = "0.00";
            }
            else if (chkViewAdd.Checked == true)
            {
                if (string.IsNullOrEmpty(lblAccountNo.Text))
                {
                    MessageBox.Show("Please select account #.", "Receipt Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkViewAdd.Checked = false;
                    return;
                }

                HpAccount _tmpAcc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);

                if (_tmpAcc == null)
                {
                    MessageBox.Show("Cannot find details.", "Receipt Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkViewAdd.Checked = false;
                    return;
                }
                else
                {
                    _HireVal = _tmpAcc.Hpa_hp_val;
                    _HalfHire = _HireVal / 2;
                }

                lblHireVal.Text = _HireVal.ToString("n");
                lblHalfHire.Text = _HalfHire.ToString("n");

                List<RecieptHeader> _tmpRecHdr = CHNLSVC.Sales.GetAccRecDet(BaseCls.GlbUserComCode, lblAccountNo.Text);

                if (_tmpRecHdr.Count > 0 && _tmpRecHdr != null)
                {
                    foreach (RecieptHeader _tmp in _tmpRecHdr)
                    {
                        if (_tmp.Sar_is_mgr_iss == true)
                        {
                            if (_tmp.Sar_direct == true)
                            {
                                _mgrPay = _mgrPay + _tmp.Sar_tot_settle_amt;
                            }
                            else if (_tmp.Sar_direct == false)
                            {
                                _mgrPay = _mgrPay - _tmp.Sar_tot_settle_amt;
                            }
                        }
                        else if (_tmp.Sar_is_mgr_iss == false)
                        {
                            if (_tmp.Sar_direct == true)
                            {
                                _cusPay = _cusPay + _tmp.Sar_tot_settle_amt;
                            }
                            else if (_tmp.Sar_direct == false)
                            {
                                _cusPay = _cusPay - _tmp.Sar_tot_settle_amt;
                            }
                        }
                    }
                }

                _totPay = _mgrPay + _cusPay;
                lblManPay.Text = _mgrPay.ToString("n");
                lblCusPay.Text = _cusPay.ToString("n");
                lblTotPaid.Text = _totPay.ToString("n");
            }

        }
    }
}
