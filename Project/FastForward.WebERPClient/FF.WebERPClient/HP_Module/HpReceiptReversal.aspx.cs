using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class HpReceiptReversal : BasePage
    {
        #region Properties/Local Variables

        protected List<RecieptHeader> _recieptHeaderList
        {
            get { return (List<RecieptHeader>)ViewState["_recieptHeaderList"]; }
            set { ViewState["_recieptHeaderList"] = value; }
        }

        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }
        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
            set { ViewState["_recieptItem"] = value; }
        }
        protected decimal _paidAmount
        {
            get { return (decimal)ViewState["_paidAmount"]; }
            set { ViewState["_paidAmount"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            uc_HpAccountDetail1.Uc_panel_height = 310;
            if (!IsPostBack)
            {
                _recieptHeaderList = new List<RecieptHeader>();
                uc_HpAccountDetail1.Uc_hpa_acc_no = string.Empty;
                BindReceiptAll(string.Empty, string.Empty, gvItemDetail);
                _paidAmount = 0;
            }
        }

        #region  Data Bind Area
        protected void BindReceiptAll(string _para, string _type, GridView _dvname)
        {
            DataTable _table = CHNLSVC.Sales.Get_ReceiptHeaderTableALL(_para, _type);
            if (_table.Rows.Count <= 0)
            {
                // _table = SetEmptyRow(_table);
                _dvname.DataSource = _table;

                if (_dvname.ID == "grvRefundHistory")
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


                if (_dvname.ID == "grvRefundHistory")
                {
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
                else if (_dvname.ID == "gvItemDetail")
                {
                    _recieptHeaderList = _RecieptHdrList;
                    _dvname.DataSource = _recieptHeaderList;
                }

            }
            _dvname.DataBind();

        }

        protected void BindRequestApprovalDetailLog(string seqNo)
        {
            //_recieptHeaderList = null;
            _recieptHeaderList = new List<RecieptHeader>();
            RecieptHeader _rhdr = new RecieptHeader();
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
                    _rhdr.Sar_com_cd = GlbCompany;
                    _rhdr.Sar_receipt_type = "";
                    _rhdr.Sar_profit_center_cd = _lists.Grad_anal5;
                    _rhdr.Sar_acc_no = _lists.Grad_anal1;
                    _rhdr.Sar_receipt_no = _lists.Grad_anal4;
                    _rhdr.Sar_receipt_date = _lists.Grad_date_param.Date;
                    _rhdr.Sar_manual_ref_no = _lists.Grad_anal3;
                    _rhdr.Sar_prefix = _lists.Grad_anal2;
                    _rhdr.Sar_tot_settle_amt = _lists.Grad_val2;
                    _rhdr.Sar_anal_5 = _lists.Grad_val1;
                    _rhdr.Sar_anal_1 = _lists.Grad_req_param;
                    _rhdrList.Add(_rhdr);
                }

                _recieptHeaderList = _rhdrList;

                gvItemDetail.DataSource = _recieptHeaderList;
                gvItemDetail.DataBind();
            }
        }

        #endregion

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        #endregion

        #region Modal popup
        protected void grvMpdalPopUpAccDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string location = ddl_Location.SelectedValue;
            string location = GlbUserDefProf;
            GridViewRow row = grvMpdalPopUpAccDet.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            lblAccountNo.Text = accountNo;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, accountNo);
            //lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountsList)
            {
                if (accountNo == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            uc_HpAccountSummary1.set_all_values(account, location, Convert.ToDateTime(DateTime.Now.Date).Date, location);
            uc_HpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
            //BindAccountItem(account.Hpa_acc_no);
            //BindAccountReceipt(account.Hpa_acc_no);

            GetUserAppLevel();
            //GlbReqRequestLevel = _obj_.RequestLevel;
            //GlbReqRequestApprovalLevel = _obj_.RequestApproveLevel;
            //GlbReqIsApprovalUser = _obj_.IsApprovalUser;
            //GlbReqIsFinalApprovalUser = _obj_.IsFinalApprovalUser;
            //GlbReqIsRequestGenerateUser = _obj_.IsRequestGenerateUser;
            //GlbReqUserPermissionLevel = _obj_.UserPermissionLevel;

            Int32 _GlbReqRequestLevel = GlbReqRequestLevel;
            Int32 _GlbReqRequestApprovalLevel = GlbReqRequestApprovalLevel;
            bool _GlbReqIsApprovalUser = GlbReqIsApprovalUser;
            bool _GlbReqIsFinalApprovalUser = GlbReqIsFinalApprovalUser;
            bool _GlbReqIsRequestGenerateUser = GlbReqIsRequestGenerateUser;
            Int32 _GlbReqUserPermissionLevel = GlbReqUserPermissionLevel;

            if (GlbReqUserPermissionLevel != -1)
            {
                LoadOptReceipts(lblAccountNo.Text.ToString(), gvItemDetail);
                BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approval cycle definition is not setup! Please contact IT department..");
            }

        }

        protected void LoadOptReceipts(string _accNo, GridView _gvname)
        {
            bool _isApp = false;
            string _refundType = string.Empty;
            if (optApproved.Checked == true)
            {
                _isApp = true;
            }

            if (_isApp == false)
            {
                if (_gvname.ID == "gvItemDetail")
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
                else if (_gvname.ID == "grvRefundHistory")
                {
                    if (optManagerIssue.Checked == true) //Reversals
                    {
                        _refundType = "MI_NEW_REV";
                    }
                }

            }
            else
            {
                if (_gvname.ID == "gvItemDetail")
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

        #endregion

        #region Events
        protected void btn_validateACC_Click(object sender, EventArgs e)
        {

            //   string location = ddl_Location.SelectedValue;
            string location = GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            AccountsList = accList;//save in veiw state
            if (accList == null || accList.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                txtAccountNo.Text = null;
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                foreach (HpAccount ac in accList)
                {
                    lblAccountNo.Text = ac.Hpa_acc_no;

                    //show acc balance.
                    Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, ac.Hpa_acc_no);
                    //lblACC_BAL.Text = accBalance.ToString();

                    //set UC values.
                    uc_HpAccountSummary1.set_all_values(ac, location, Convert.ToDateTime(DateTime.Now.Date).Date, location);
                    uc_HpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;
                }

                //----------

                GetUserAppLevel();
                //GlbReqRequestLevel = _obj_.RequestLevel;
                //GlbReqRequestApprovalLevel = _obj_.RequestApproveLevel;
                //GlbReqIsApprovalUser = _obj_.IsApprovalUser;
                //GlbReqIsFinalApprovalUser = _obj_.IsFinalApprovalUser;
                //GlbReqIsRequestGenerateUser = _obj_.IsRequestGenerateUser;
                //GlbReqUserPermissionLevel = _obj_.UserPermissionLevel;

                Int32 _GlbReqRequestLevel = GlbReqRequestLevel;
                Int32 _GlbReqRequestApprovalLevel = GlbReqRequestApprovalLevel;
                bool _GlbReqIsApprovalUser = GlbReqIsApprovalUser;
                bool _GlbReqIsFinalApprovalUser = GlbReqIsFinalApprovalUser;
                bool _GlbReqIsRequestGenerateUser = GlbReqIsRequestGenerateUser;
                Int32 _GlbReqUserPermissionLevel = GlbReqUserPermissionLevel;

                if (GlbReqUserPermissionLevel != -1)
                {
                    LoadOptReceipts(lblAccountNo.Text.ToString(), gvItemDetail);
                    BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approval cycle definition is not setup! Please contact IT department..");
                }
                //----------

            }
            else if (accList.Count > 1)
            {
                //show a pop up to select the account number
                grvMpdalPopUpAccDet.DataSource = accList;
                grvMpdalPopUpAccDet.DataBind();
                mpeAccDet.Show();
            }
        }

        public void chkOneReceipt_CheckChangedMethod(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;


            string _selectedReceiptNo = row.Cells[5].Text;
            bool status = chkStatus.Checked;

            if (status == true)
            {
                LoadOptReceipts(_selectedReceiptNo, grvRefundHistory);
                lblSelectedReceipt.Text = _selectedReceiptNo;
                decimal refundBalAmt = Convert.ToDecimal(row.Cells[9].Text.ToString()) - Convert.ToDecimal(lblRefundTot.Text.ToString());
                //lblRefundBalAmt.Text = String.Format("{0:#,##0.00}", refundBalAmt);
                lblRefundBalAmt.Text = FormatToCurrency(refundBalAmt.ToString());
                txtRefundAmt.Text = Convert.ToDecimal(row.Cells[10].Text.ToString()).ToString();
                mpeRefundHistory.Show();
                txtRefundAmt.Focus();
            }
        }

        private void SaveRequest()
        {
            try
            {
                bool _isapprovedrequest = false;
                List<RecieptItem> _recieptItemList = new List<RecieptItem>();

                //UI validation.
                if (string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    throw new UIValidationException("Please select the account no!");
                }
                if (_recieptHeaderList == null)
                {
                    throw new UIValidationException("Please select the receipt(s)!");
                }

                #region Check Approval Cycle
                CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                string _documentType = string.Empty;

                if (optManagerIssue.Checked == true)
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                    _documentType = "MANAGER_ISSUE";
                }

                if (optRtnCheque.Checked == true)
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004;
                    _documentType = "RETURN_CHEQUE";
                }

                if (optOthReceipt.Checked == true)
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015;
                    _documentType = "OTHER_RECEIPT";
                }

                #endregion

                if (GlbReqIsApprovalNeed == true)
                {
                    if (optApproved.Checked == true)
                    {
                        if (string.IsNullOrEmpty(ddlRequestNo.Text) == false)
                        {
                            GlbReqIsApprovalNeed = false;
                            _isapprovedrequest = true;
                        }
                    }
                }
                else
                {
                    _isapprovedrequest = false;
                }

                if (GlbReqIsApprovalNeed == true)
                {

                    #region fill RequestApprovalHeader

                    RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                    ra_hdr.Grah_app_by = GlbUserName;
                    ra_hdr.Grah_app_dt = DateTime.Now.Date;
                    ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdr.Grah_app_stus = "P";
                    ra_hdr.Grah_app_tp = _approvalCode.ToString();
                    ra_hdr.Grah_com = GlbUserComCode;
                    ra_hdr.Grah_cre_by = GlbUserName;
                    ra_hdr.Grah_cre_dt = DateTime.Now.Date;
                    ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
                    ra_hdr.Grah_loc = GlbUserDefProf;// GlbUserDefLoca;
                    ra_hdr.Grah_mod_by = GlbUserName;
                    ra_hdr.Grah_mod_dt = DateTime.Now.Date;
                    ra_hdr.Grah_oth_loc = GlbUserDefProf;
                    ra_hdr.Grah_remaks = _documentType;

                    if (string.IsNullOrEmpty(ddlRequestNo.Text))
                    {
                        ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                    }
                    else
                    {
                        ra_hdr.Grah_ref = ddlRequestNo.Text.ToString();
                    }

                    #endregion

                    #region fill RequestApprovalHeaderLog

                    RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                    ra_hdrLog.Grah_app_by = GlbUserName;
                    ra_hdrLog.Grah_app_dt = DateTime.Now.Date;
                    ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdrLog.Grah_app_stus = "P";
                    ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
                    ra_hdrLog.Grah_com = GlbUserComCode;
                    ra_hdrLog.Grah_cre_by = GlbUserName;
                    ra_hdrLog.Grah_cre_dt = DateTime.Now.Date;
                    ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
                    ra_hdrLog.Grah_loc = GlbUserDefProf;

                    ra_hdrLog.Grah_mod_by = GlbUserName;
                    ra_hdrLog.Grah_mod_dt = DateTime.Now.Date;

                    ra_hdrLog.Grah_oth_loc = GlbUserDefProf;

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
                            if (_hdr.Sar_anal_5 > 0)
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
                        throw new UIValidationException("Approval cycle definition is not setup! Please contact IT department..");
                    }
                    #endregion


                    string referenceNo;
                    Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                    if (eff > 0)
                    {
                        string Msg = "<script>alert('Request Successfully Saved! Request No : " + referenceNo + "');window.location = 'HpReceiptReversal.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                    else
                    {
                        string Msg = "<script>alert('Request Fail!' );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }
                else
                {
                    int _ref = CHNLSVC.Sales.saveAll_HP_ReceiptReversal(_recieptHeaderList, GlbUserComCode, GlbUserDefProf, GlbUserName, _isapprovedrequest, ddlRequestNo.Text.ToString());
                    if (_ref == 1)
                    {
                        string Msg = "<script>alert('Reversal Successfully Saved!');window.location = 'HpReceiptReversal.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }

        }

        #region Approval Request Call

        private void BindRequestsToDropDown(string _account, DropDownList _ddl)
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

                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _ddl.Items.Clear();
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        _ddl.DataSource = query;
                        _ddl.DataBind();
                    }
                }

            }

        }

        #endregion

        #region Check User Approval Level
        protected void GetUserAppLevel()
        {
            //CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
            //string _documentType = string.Empty;


            if (optManagerIssue.Checked == true)
            {
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                //_approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT002;
                //_documentType = "MANAGER_ISSUE_REVERSAL";
            }

            if (optRtnCheque.Checked == true)
            {
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                //_approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT004;
                //_documentType = "RETURN_CHEQUE_RECEIPT_REVERSAL";
            }

            if (optOthReceipt.Checked == true)
            {
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015, DateTime.Now.Date, string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                //_approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT015;
                //_documentType = "OTHER_RECEIPT_REVERSAL";
            }

            //GlbReqUserPermissionLevel 
            //GlbReqIsFinalApprovalUser
            //GlbReqIsRequestGenerateUser

        }

        #endregion

        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);
        }

        protected void ddlRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRequestApprovalDetailLog(ddlRequestNo.Text);
            if (optApproved.Checked == true)
            {
                DisableCheckbox_gvItemDetail();
            }
        }

        protected void DisableCheckbox_gvItemDetail()
        {
            foreach (GridViewRow row in gvItemDetail.Rows)
            {
                CheckBox ch = (CheckBox)row.FindControl("chkOneReceipt");

                if (ch.Checked)
                {
                    CheckBox ch1 = (CheckBox)gvItemDetail.Rows[row.RowIndex].FindControl("chkOneReceipt");
                    ch1.Enabled = false;
                }
            }
        }
        #endregion

        #region Buttons Events

        #region Button Pannel
        #region Request Button
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            SaveRequest();
        }
        #endregion
        #region Reject Button
        protected void btnReject_Click(object sender, EventArgs e)
        {
        }
        #endregion
        #region Clear Button
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            Response.Redirect("~/HP_Module/HpReceiptReversal.aspx", false);
        }
        #endregion
        #region Close Button
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        #endregion
        #endregion

        #region Other Buttons
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
                mpeRefundHistory.Show();
                return;
            }

            bool isNum = decimal.TryParse(txtRefundAmt.Text, out refundAmt);

            if (!isNum)
            {
                lblRefundHistoryMsg.Text = "Invalid refund amount!";
                txtRefundAmt.Focus();
                mpeRefundHistory.Show();
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
                mpeRefundHistory.Show();
                return;
            }

            if (refundAmt > refundBalAmt)
            {
                lblRefundHistoryMsg.Text = "You can't exceed the refund balance amount!";
                txtRefundAmt.Text = "";
                txtRefundAmt.Focus();
                mpeRefundHistory.Show();
                return;
            }

            var queryReceiptheaderList = from receiptheader in _recieptHeaderList
                                         where receiptheader.Sar_receipt_no == lblSelectedReceipt.Text.ToString()
                                         select receiptheader;

            foreach (var _lists in queryReceiptheaderList)
            {
                _lists.Sar_anal_5 = refundAmt;
            }

            gvItemDetail.DataSource = _recieptHeaderList;
            gvItemDetail.DataBind();

            lblRefundHistoryMsg.Text = string.Empty;
        }
        #endregion

        protected void optManagerIssue_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNo.Text) == false)
            {
                Clear();
            }
        }

        protected void optRtnCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNo.Text) == false)
            {
                Clear();
            }
        }

        protected void optOthReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNo.Text) == false)
            {
                Clear();
            }
        }

        #endregion






    }
}