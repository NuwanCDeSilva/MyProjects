using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class ReturnCheque : BasePage
    {
        #region PropertyData
        Boolean _mgrIssu = false;
        string _oldChkNo
        {
            get { if (Session["_oldChkNo"] != null) { return (string)Session["_oldChkNo"]; } else { return ""; } }
            set { Session["_oldChkNo"] = value; }
        }
        string _propCenter
        {
            get { if (Session["_propCenter"] != null) { return (string)Session["_propCenter"]; } else { return ""; } }
            set { Session["_propCenter"] = value; }
        }
        string _com
        {
            get { if (Session["_com"] != null) { return (string)Session["_com"]; } else { return ""; } }
            set { Session["_com"] = value; }
        }
        string _oldBranch
        {
            get { if (Session["_oldBranch"] != null) { return (string)Session["_oldBranch"]; } else { return ""; } }
            set { Session["_oldBranch"] = value; }
        }
        string _oldBankCd
        {
            get { if (Session["_oldBankCd"] != null) { return (string)Session["_oldBankCd"]; } else { return ""; } }
            set { Session["_oldBankCd"] = value; }
        }
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        DataTable _chkData
        {
            get { if (Session["_chkData"] != null) { return (DataTable)Session["_chkData"]; } else { return new DataTable(); } }
            set { Session["_chkData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }
        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }
        string _para = "";
        RecieptHeader _recHdr = new RecieptHeader();
        RecieptItem _recItm = new RecieptItem();
        MasterBankBranch _bankBranc = new MasterBankBranch();
        MasterOutsideParty _outSideParty = new MasterOutsideParty();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearPage();
            }
            else
            {
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    _serPopShow = false;
                    PopupSearch.Hide();
                }
            }
        }
        private void ClearPage()
        {
            try
            {
                _serData = new DataTable();
                _chkData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _ava = false;
                _toolTip = string.Empty;

                _oldChkNo = "";
                _propCenter = "";
                _com = "";
                _oldBranch = "";
                _oldBankCd = "";

                _mgrIssu = false;

                txtFrom.Text = DateTime.Today.AddDays(-31).ToString("dd/MMM/yyyy");
                txtTo.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                txtCompany.Text = "";
                txtPc.Text = "";
                txtBank.Text = "";
                txtBranch.Text = "";
                txtChequeNo.Text = "";
                dgvChekDetails.DataSource = new int[] { };
                dgvChekDetails.DataBind();

                txtCorrChkNo.Text = "";
                ddlCorrBank.SelectedIndex = 0;
                txtCorrBranch.Text = "";
                txtCorrDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");

                txtReturnDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                txtRetBank.Text = "";
                txtRetBank_TextChanged(null, null);
                ddlRetBankTp.SelectedIndex = 0;
                
                dgvRecDet.DataSource = new int[] { };
                dgvRecDet.DataBind();

                txtTotSysAmt.Text = "";
                txtTotRetAmt.Text = "";
                txtTotManAmt.Text = "";
                pnlCheqCore.Enabled = false;
                lbtnEditCheque.Enabled = false;
                lbtnEditCheque.CssClass = "buttoncolorleft";
                lbtnEditCheque.OnClientClick = "return Enable();";

                lbtnClearCheque.Enabled = false;
                lbtnClearCheque.CssClass = "buttoncolorleft";
                lbtnClearCheque.OnClientClick = "return Enable();";

                lbtnSeCorrBranch.Enabled = false;
                lbtnSeCorrBranch.CssClass = "buttoncolorleft";

                btnCorrDate.Enabled = false;
                btnCorrDate.CssClass = "buttoncolorleft";
                DataTable _dt=CHNLSVC.Financial.GetBanks();
                ddlCorrBank.DataSource = _dt;
                ddlCorrBank.DataTextField = "mbi_desc";
                ddlCorrBank.DataValueField = "mbi_cd";
                ddlCorrBank.DataBind();
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                txtCompany_TextChanged(null, null);
                decimal _tmp=0;
                txtTotRetAmt.Text = _tmp.ToString("N2");
                txtTotSysAmt.Text = _tmp.ToString("N2");
                txtTotManAmt.Text = _tmp.ToString("N2");
                lbtnSave.Enabled = true;
                lbtnSave.CssClass = "buttonUndocolor";
                lbtnSave.OnClientClick = "return ConfSave();";
                bool b10153 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10153);
                if (!b10153)
                {
                    txtPc.Text = Session["UserDefProf"].ToString();
                    txtPc_TextChanged(null, null);
                    txtPc.Enabled = false;
                    lbtnSePc.Enabled = false;
                }
                txtRetBankDesc.Text = "";
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
        }

        private void DataAvailable(DataTable _dt, string _valText, string _valCol, string _valToolTipCol = "")
        {
            _ava = false;
            _toolTip = string.Empty;
            foreach (DataRow row in _dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[_valCol].ToString()))
                {
                    if (_valText == row[_valCol].ToString())
                    {
                        _ava = true;
                        if (!string.IsNullOrEmpty(_valToolTipCol))
                        {
                            _toolTip = row[_valToolTipCol].ToString();
                        }
                        break;
                    }
                }
            }
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp)
        {
            OrderSearchGridData(_colName, _ordTp);
            try
            {
                dgvResult.DataSource = new int[] { };
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        BindDdlSerByKey(_serData);
                    }
                }
                dgvResult.DataBind();
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                _serPopShow = true;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                decimal tmpAmo = 0;
                decimal _retVal = decimal.TryParse(txtTotRetAmt.Text, out tmpAmo) ? Convert.ToDecimal(txtTotRetAmt.Text) : 0;
                if(tmpAmo==0 || tmpAmo<0)
                {
                    DispMsg("Total Return amount should be greater than zero");
                    return;
                }
                if (dgvRecDet.Rows.Count > 0)
                {
                    txtPc.Text = txtPc.Text.Trim().ToUpper();
                    if (string.IsNullOrEmpty(_com))
                    {
                        DispMsg("Invalid company !");
                       // txtRetBank.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(_propCenter))
                    {
                        DispMsg("Invalid profit center !");
                        //txtRetBank.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRetBank.Text))
                    {
                        DispMsg("Please select return bank !");
                        txtRetBank.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtReturnDate.Text))
                    {
                        DispMsg("Please select return date !");
                        txtPc.Focus();
                        return;
                    }
                     tmpAmo = 0;
                     _retVal = decimal.TryParse(txtTotRetAmt.Text, out tmpAmo) ? Convert.ToDecimal(txtTotRetAmt.Text) : 0;
                    int seqNo = CHNLSVC.Inventory.GetSerialID();
                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                    //get reciept number
                    _receiptAuto.Aut_cate_cd = Session["UserDefProf"].ToString();//BaseCls.GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = Session["UserDefProf"].ToString();
                    _receiptAuto.Aut_start_char = "RCHQ";
                    _receiptAuto.Aut_direction = 0;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RTCHQ";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_year = 2012;
                    string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                    //insert reciept
                    RecieptHeader recieptHeadder = new RecieptHeader();
                    recieptHeadder.Sar_seq_no = seqNo;
                    recieptHeadder.Sar_receipt_no = _cusNo;
                    recieptHeadder.Sar_com_cd = _com.ToUpper();
                    recieptHeadder.Sar_receipt_type = "RTCHQ";
                    recieptHeadder.Sar_receipt_date = CHNLSVC.Security.GetServerDateTime().Date;
                    recieptHeadder.Sar_profit_center_cd = _propCenter.ToUpper();
                    recieptHeadder.Sar_debtor_name = "CHEQUE";
                    recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(txtTotRetAmt.Text);
                    recieptHeadder.Sar_direct = false;
                    recieptHeadder.Sar_act = true;
                    recieptHeadder.Sar_create_by = Session["UserID"].ToString();
                    recieptHeadder.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;
                    recieptHeadder.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;
                    recieptHeadder.Sar_session_id = Session["SessionID"].ToString();
                    
                    //  CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder); **

                    //insert reciept item
                    RecieptItem recieptItem = new RecieptItem();
                    recieptItem.Sard_seq_no = seqNo;
                    recieptItem.Sard_line_no = 1;
                    recieptItem.Sard_receipt_no = _cusNo;
                   
                    recieptItem.Sard_chq_bank_cd = txtBank.Text.Trim();
                    recieptItem.Sard_pay_tp = "CASH";
                    recieptItem.Sard_settle_amt = 0;
                    //
                    recieptItem.Sard_ref_no = _oldChkNo;
                    recieptItem.Sard_settle_amt =Convert.ToDecimal(txtTotRetAmt.Text);
                   

                    //CHNLSVC.Sales.SaveReceiptItem(recieptItem); **

                    //add return cheque record
                    ChequeReturn chequeReturn = new ChequeReturn();
                    chequeReturn.Seq = seqNo;
                    chequeReturn.RefNo = _cusNo;
                    chequeReturn.Bank = txtBank.Text.Trim();
                    chequeReturn.Pc = _propCenter.ToUpper();
                    chequeReturn.Bank_type = ddlRetBankTp.SelectedValue == "CASH" ? 1 : 0;// listBoxBnkTp.SelectedItem.ToString()=="CASH"?1:0;
                    chequeReturn.Returndate = Convert.ToDateTime(txtReturnDate.Text);
                    chequeReturn.Return_bank = txtRetBank.Text.Trim();
                    chequeReturn.Cheque_no = _oldChkNo;
                    chequeReturn.Company = _com.ToUpper();
                    chequeReturn.Create_by = Session["UserID"].ToString();
                    chequeReturn.Create_Date = CHNLSVC.Security.GetServerDateTime().Date;
                    chequeReturn.Act_value = Convert.ToDecimal(txtTotRetAmt.Text);
                    chequeReturn.Sys_value = Convert.ToDecimal(txtTotSysAmt.Text);
                    chequeReturn.Srcq_mgr_chg = Convert.ToDecimal(txtTotManAmt.Text);
                    chequeReturn.Srcq_chq_branch = txtCorrBranch.Text.Trim();
                    // chequeReturn.Settle_val = Convert.ToDecimal(txtTotRetAmt.Text);

                    //Int32 eff= CHNLSVC.Financial.SaveReturnCheque(chequeReturn);//**
                    Int32 eff = 0;
                    RecieptItem _tmpRecHdr = CHNLSVC.Sales.GET_SAT_RECIEPT_FOR_RETURN_CHK(_oldChkNo);
                    if (_tmpRecHdr != null)
                    {
                        RecieptHeader _recHdr = CHNLSVC.Sales.GetReceiptHdr(_tmpRecHdr.Sard_receipt_no).FirstOrDefault();
                        if (_recHdr!=null)
                        {
                            recieptHeadder.Sar_debtor_cd = _recHdr.Sar_debtor_cd;
                        }
                        recieptHeadder.Sar_manual_ref_no = _tmpRecHdr.Sard_inv_no;
                        recieptItem.Sard_inv_no = _tmpRecHdr.Sard_inv_no;
                        recieptItem.Sard_deposit_bank_cd = _tmpRecHdr.Sard_deposit_bank_cd;
                        
                    }                  
                    ////// ADDed BY Dulaj 2018/Feb/23
                    DataTable datasource = CHNLSVC.Financial.GetReturnChequesNew(chequeReturn.Pc, chequeReturn.Cheque_no, chequeReturn.Bank);
                    List<RecieptItem> _recieptItemList = new List<RecieptItem>();
                    foreach (DataRow dr in datasource.Rows)
                    {
                        //total = total + Convert.ToDecimal(dr["sard_settle_amt"]);
                        RecieptItem item = new RecieptItem();
                        item.Sard_inv_no = (dr["sard_inv_no"]).ToString();
                        item.Sard_settle_amt = Convert.ToDecimal(dr["sard_settle_amt"]);
                        _recieptItemList.Add(item);
                    }
                    //////
                    string _err = "";                    
                    int check = CHNLSVC.Financial.CheckReturnCheque(chequeReturn.Cheque_no, chequeReturn.Bank);
                    eff = CHNLSVC.Financial.ChequeReturnWeb(recieptHeadder, recieptItem, chequeReturn, _recieptItemList, out _err);
                    if (eff > 0)
                    {
                        ClearPage();
                        if (check > 0)
                        {
                            string val = CHNLSVC.Financial.GetRefNumber(chequeReturn.Cheque_no, chequeReturn.Bank);
                            DispMsg("Successfully Updated ! " + val, "S");                              
                        }
                        else
                            DispMsg("Successfully Saved ! " + "   " + _cusNo, "S");                          
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_err))
                        {
                            DispMsg(_err);
                        }
                        else
                        {
                            DispMsg("Error Occurred while processing !", "E");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void ClearData()
        {

        }
        protected void txtSerByKey_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "Company")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                else if (_serType == "AllProfitCenters")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                else if (_serType == "Bank")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                    _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                else if (_serType == "REBank")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                    _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                else if (_serType == "BankBranch")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                else if (_serType == "Cheque")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Cheque);
                    _serData = CHNLSVC.CommonSearch.SearchChequeDetailsReturnChk(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text, DateTime.Now, DateTime.Now);
                }
                else if (_serType == "CORRBankBranch")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                    _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text);
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                _serPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Company")
                {
                    txtCompany.Text = code;
                    txtCompany_TextChanged(null, null);
                }
                else if (_serType == "AllProfitCenters")
                {
                    txtPc.Text = code;
                    txtPc_TextChanged(null, null);
                }
                else if (_serType == "Bank")
                {
                    txtBank.Text = code;
                    txtBank_TextChanged(null, null);
                }
                else if (_serType == "REBank")
                {
                    txtRetBank.Text = code;
                    txtRetBank_TextChanged(null, null);
                }
                else if (_serType == "BankBranch")
                {
                    txtBranch.Text = code;
                    txtBranch_TextChanged(null, null);
                }
                else if (_serType == "Cheque")
                {
                    txtChequeNo.Text = code;
                    txtChequeNo_TextChanged(null, null);
                }
                else if (_serType == "CORRBankBranch")
                {
                    txtCorrBranch.Text = code;
                    txtCorrBranch_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(txtCompany.Text.Trim().ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtBank.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BrandManager:
                    {
                        paramsText.Append(ddlCorrBank.SelectedValue + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Cheque:
                    {
                        paramsText.Append(txtCompany.Text.Trim().ToUpper() + seperator + txtPc.Text.Trim().ToUpper() + seperator +txtBank.Text.Trim().ToUpper() + seperator + txtBranch.Text.ToUpper().Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        protected void lbtnMainSer_Click(object sender, EventArgs e)
        {
            try
            {
                _oldChkNo = "";
                _propCenter = "";
                _com = "";
                _oldBranch = "";
                _oldBankCd = "";

                dgvRecDet.DataSource = new int[] { };
                dgvRecDet.DataBind();

                dgvChekDetails.DataSource = new int[] { };
                dgvChekDetails.DataBind();

                pnlCheqCore.Enabled = false;
                lbtnEditCheque.Enabled = false;
                lbtnEditCheque.CssClass = "buttoncolorleft";
                lbtnEditCheque.OnClientClick = "return Enable();";

                lbtnClearCheque.Enabled = false;
                lbtnClearCheque.CssClass = "buttoncolorleft";
                lbtnClearCheque.OnClientClick = "return Enable();";
                lbtnSeCorrBranch.Enabled = false;
                lbtnSeCorrBranch.CssClass = "buttoncolorleft";

                btnCorrDate.Enabled = false;
                btnCorrDate.CssClass = "buttoncolorleft";
                _chkData = new DataTable();
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Focus(); DispMsg("Please select the company !"); return;
                }
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    txtBank.Focus(); DispMsg("Please select the bank !"); return;
                }
                //if (string.IsNullOrEmpty(txtChequeNo.Text))
                //{
                //    if (string.IsNullOrEmpty(txtPc.Text))
                //    {
                //        txtPc.Focus(); DispMsg("Please select the profit center !"); return;
                //    }
                //    if (string.IsNullOrEmpty(txtBank.Text))
                //    {
                //        txtBank.Focus(); DispMsg("Please select the bank !"); return;
                //    }
                //}
                if (string.IsNullOrEmpty(txtFrom.Text))
                {
                    btnFrom.Focus(); DispMsg("Please select from date !"); return;
                } 
                if (string.IsNullOrEmpty(txtTo.Text))
                {
                    btnTo.Focus(); DispMsg("Please select to date !"); return;
                }
                DateTime _dtFrom = DateTime.Today;
                DateTime _dtTo = DateTime.Today;
                if (!DateTime.TryParse(txtFrom.Text, out _dtFrom))
                {
                    btnFrom.Focus();  DispMsg("Please select valid from date !"); return;
                }
                if (!DateTime.TryParse(txtTo.Text, out _dtTo))
                {
                    btnTo.Focus(); DispMsg("Please select valid to date !"); return;
                }
                TimeSpan _ts = _dtTo.Subtract(_dtFrom);
                if (_ts.Days<0)
                {
                    btnTo.Focus(); DispMsg("Please select valid date range !"); return;
                }
                if (_ts.Days > 31)
                {
                   // btnTo.Focus(); DispMsg("Please select valid date range !"); return;
                }
                _recHdr = new RecieptHeader { Sar_profit_center_cd=txtPc.Text.Trim().ToUpper(), Sar_com_cd=txtCompany.Text.Trim().ToUpper()};
                _recItm = new RecieptItem { 
                    Sard_chq_bank_cd = txtBank.Text.ToUpper(), 
                    Sard_chq_branch = string.IsNullOrEmpty(txtBranch.Text) ? null : txtBranch.Text.Trim().ToUpper(),
                    Sard_ref_no = string.IsNullOrEmpty(txtChequeNo.Text) ? null : txtChequeNo.Text.Trim().ToUpper()
                };
                _chkData = CHNLSVC.CommonSearch.GetChequeDetForReturn(_recHdr, _recItm, _dtFrom, _dtTo);
                BindChequData(_chkData);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void BindChequData(DataTable _dt)
        {
            dgvChekDetails.DataSource = new int[] { };
            if (_dt.Rows.Count > 0)
            {
                dgvChekDetails.DataSource = _dt;
            }
            dgvChekDetails.DataBind();
            foreach (GridViewRow row in dgvChekDetails.Rows)
            {
                Label lblsar_profit_center_cd = row.FindControl("lblsar_profit_center_cd") as Label;
                Label lblsard_chq_bank_cd = row.FindControl("lblsard_chq_bank_cd") as Label;
                Label lblsard_chq_branch = row.FindControl("lblsard_chq_branch") as Label;
                _outSideParty = CHNLSVC.General.GetOutsideParty(lblsard_chq_bank_cd.Text.Trim().ToUpper());
                _bankBranc = CHNLSVC.General.GetBankBranchData(new MasterBankBranch() { 
                    Mbb_country_cd= ""
                    }).FirstOrDefault(); ;
            }
        }
        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCompany.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCompany.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCompany.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCompany.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCompany.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCompany.Text = string.Empty;
                        txtCompany.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                    txtPc.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnSeCom_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("Company", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtPc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPc.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtPc.Text))
                {
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        txtPc.Text = ""; txtCompany.Focus(); DispMsg("Please select the company !"); return;
                    }
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", txtPc.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtPc.Text.ToUpper().Trim(), "CODE", "Description");
                    txtPc.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtPc.Text = string.Empty;
                        txtPc.Focus();
                        DispMsg("Please enter valid profit center !");
                        return;
                    }
                    txtBank.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSePc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Focus(); DispMsg("Please select the company !"); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, null, null);
                LoadSearchPopup("AllProfitCenters", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtBank_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBank.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtBank.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                    _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para, "CODE", txtBank.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtBank.Text.ToUpper().Trim(), "CODE", "Description");
                    txtBank.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtBank.Text = string.Empty;
                        txtBank.Focus();
                        DispMsg("Please enter valid bank !");
                        return;
                    }
                    txtBranch.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeBank_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para,null,null);
                LoadSearchPopup("Bank", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtBranch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBranch.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtBranch.Text))
                {
                    if (string.IsNullOrEmpty(txtBank.Text))
                    {
                        txtBranch.Text = ""; txtBank.Focus(); DispMsg("Please select the bank !"); return;
                    }
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, "CODE", txtBranch.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtBranch.Text.ToUpper().Trim(), "CODE", "Description");
                    txtBranch.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtBranch.Text = string.Empty;
                        txtBranch.Focus();
                        DispMsg("Please enter valid branch !");
                        return;
                    }
                    txtChequeNo.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeBranch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    txtBank.Focus(); txtBranch.Text = ""; DispMsg("Please select the bank ! "); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, null, null);
                LoadSearchPopup("BankBranch", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBranch.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtBranch.Text))
                {
                    if (string.IsNullOrEmpty(txtBank.Text))
                    {
                        txtBranch.Text = ""; txtBank.Focus(); DispMsg("Please select the bank !"); return;
                    }
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                    _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, "CODE", txtBranch.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtBranch.Text.ToUpper().Trim(), "CODE", "Description");
                    txtBranch.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtBranch.Text = string.Empty;
                        txtBranch.Focus();
                        DispMsg("Please enter valid branch !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeChequeNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Focus(); txtChequeNo.Text = ""; DispMsg("Please select the company ! "); return;
                }
                //if (string.IsNullOrEmpty(txtPc.Text))
                //{
                //    txtPc.Focus(); txtChequeNo.Text = ""; DispMsg("Please select the profit center ! "); return;
                //}
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    txtBank.Focus(); txtChequeNo.Text = ""; DispMsg("Please select the bank ! "); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Cheque);
                _serData = CHNLSVC.CommonSearch.SearchChequeDetailsReturnChk(_para, null, null, Convert.ToDateTime(txtFrom.Text),Convert.ToDateTime(txtTo.Text));
                LoadSearchPopup("Cheque", "Cheque #", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvChekDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //_chkData
            try
            {
                dgvChekDetails.PageIndex = e.NewPageIndex;
                if (_chkData.Rows.Count > 0)
                {
                    dgvChekDetails.DataSource = _chkData;
                }
                else
                {
                    dgvChekDetails.DataSource = new int[] { };
                }
                dgvChekDetails.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void GridViewDataBind(string _chkNo, string _bankCd, string _profCenter)
        {
            try
            {
                //add 19-03-2013
                lbtnSave.Enabled = true;
                txtTotSysAmt.Text = string.Format("{0:n2}", 0);
                txtTotRetAmt.Text = string.Format("{0:n2}", 0);
                string _type = "";
                string _value = "";
                Int32 _days = 0;
                decimal _mgrChg = 0;
                Boolean _mgrChgCal = false;

                //----------------------------------------------------------
                //check return cheque table
                DataTable datatable = CHNLSVC.Financial.GetReturnChequeCount(_chkNo, _bankCd, _profCenter);
                if (Convert.ToInt32(datatable.Rows[0][0].ToString()) <= 0)
                {
                    DataTable datasource = CHNLSVC.Financial.GetReturnChequesNew(_profCenter, _chkNo, _bankCd);
                    DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(_profCenter, _chkNo, _bankCd);

                    datasource.Merge(remsource);

                    //GridViewCheques.DataSource = null;
                    //GridViewCheques.AutoGenerateColumns = false;
                    //GridViewCheques.DataSource = datasource;

                    //GridRowCount.Value = GridViewCheques.Rows.Count.ToString();//TODO:
                    decimal total = 0;


                    if (datasource != null)
                    {
                        if (datasource.Rows.Count > 0)
                        {
                            //DropDownListNBank.SelectedValue = Convert.ToString(datasource.Rows[0]["SARD_CHQ_BANK_CD"]);//
                            //TextBoxBranch.Text = Convert.ToString(datasource.Rows[0]["SARD_CHQ_BRANCH"]);

                            //try
                            //{
                            //    TextBoxChDate.Value = Convert.ToDateTime(datasource.Rows[0]["sard_chq_dt"]);
                            //}
                            //catch (Exception EX)
                            //{

                            //}

                        }
                        else
                        {
                            DispMsg("Invalid Cheque Number!"); return;
                        }
                    }
                    //sard_chq_branch
                    foreach (DataRow dr in datasource.Rows)
                    {
                        total = total + Convert.ToDecimal(dr["sard_settle_amt"]);

                    }


                    for (int x = 0; x < dgvRecDet.Rows.Count; x++)
                    {
                        Label sar_is_mgr_iss = dgvRecDet.Rows[x].FindControl("lblSfd_itm") as Label;
                        if (Convert.ToInt16(sar_is_mgr_iss.Text) == 1)
                        {
                            _mgrIssu = true;
                            goto L4;
                        }
                    }
                L4:
                    { Int16 x = 0; }


                    if (_mgrIssu == true)
                    {
                        _days = Convert.ToInt32((Convert.ToDateTime(txtReturnDate.Text).Date - Convert.ToDateTime(txtCorrDate.Text).Date).TotalDays);
                        if (_days < 0)
                        {
                            _days = 0;
                        }
                        List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(txtCompany.Text.ToUpper(), txtPc.Text.Trim(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                        if (_hir2.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _two in _hir2)
                            {
                                _type = _two.Mpi_cd;
                                _value = _two.Mpi_val;

                                List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQMAN", Convert.ToDateTime(txtReturnDate.Text).Date);
                                if (_ser != null)
                                {
                                    foreach (HpServiceCharges _ser1 in _ser)
                                    {
                                        if (_ser1.Hps_from_val <= _days && _ser1.Hps_to_val >= _days)
                                        {
                                            _mgrChg = Math.Round(((total * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                            _mgrChgCal = true;
                                            goto L3;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            DispMsg("Profit center hirarchy not set.");
                            lbtnSave.Enabled = false;
                            return;
                        }

                        if (_mgrChgCal == false)
                        {
                            DispMsg("Manager issue cheque and cannot find manager charge definition.");
                            _mgrChg = 0;
                            txtTotManAmt.Text = string.Format("{0:n2}", _mgrChg);
                            lbtnSave.Enabled = false;
                            return;
                        }
                    }

                L3: Int16 i = 0;

                    txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                    txtTotRetAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                    txtTotManAmt.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                }
                else
                {
                    if (_chkNo != "")
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Entering Cheque is already available as Return Cheque");
                        DispMsg("Cheque already Returned!");
                        //  this.btnClear_Click(null, null);
                        try
                        {
                            DataTable datasource = CHNLSVC.Financial.GetReturnChequesNew(_profCenter, _chkNo, _bankCd);

                            DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(_profCenter, _chkNo, _bankCd);


                            datasource.Merge(remsource);

                            //GridViewCheques.DataSource = null;
                            //GridViewCheques.AutoGenerateColumns = false;
                            //GridViewCheques.DataSource = datasource;
                            DataTable dt = CHNLSVC.Financial.get_rtn_chq_byBankChqNo(_bankCd, _chkNo.Trim());

                            Decimal total = 0;
                            _mgrChg = 0;
                            if (dt != null)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    total = total + Convert.ToDecimal(dr["srcq_act_val"]);
                                    _mgrChg = _mgrChg + Convert.ToDecimal(dr["srcq_mgr_chg"]);
                                }
                                txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                                txtTotRetAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                                txtTotManAmt.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                            }
                            //txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                            txtTotRetAmt.Text = string.Format("{0:n2}", total);//total.ToString();

                            total = 0;
                            foreach (DataRow dr in datasource.Rows)
                            {
                                total = total + Convert.ToDecimal(dr["sard_settle_amt"]);
                            }
                            txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                        }
                        catch (Exception ex) { }

                        lbtnSave.Enabled = false;
                    }
                    else
                    {
                        lbtnClear_Click(null, null);
                    }

                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...");
            }
        }
        protected void lbtnAddChkCorr_Click(object sender, EventArgs e)
        {
             _oldChkNo = "";
             _oldBankCd = "";
             _oldBranch = "";
             _propCenter = "";
             _com = "";

            lbtnSave.Enabled = true;
            lbtnSave.CssClass = "buttonUndocolor";
            lbtnSave.OnClientClick = "return ConfSave();";

            txtCorrChkNo.Text = "";
            ddlCorrBank.SelectedIndex =0;
            txtCorrBranch.Text = "";

            pnlCheqCore.Enabled = true;

            lbtnEditCheque.Enabled = true;
            lbtnEditCheque.CssClass = "buttonUndocolorLeft";
            lbtnEditCheque.OnClientClick = "return ConfEdit();";

            lbtnClearCheque.Enabled = true;
            lbtnClearCheque.CssClass = "buttonUndocolorLeft";
            lbtnClearCheque.OnClientClick = "return ConfClear();";

            lbtnSeCorrBranch.Enabled = true;
            lbtnSeCorrBranch.CssClass = "buttonUndocolorLeft";

            btnCorrDate.Enabled = true;
            btnCorrDate.CssClass = "buttonUndocolorLeft";

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label lblsard_ref_no = row.FindControl("lblsard_ref_no") as Label;
            Label lblsard_chq_branch = row.FindControl("lblsard_chq_branch") as Label;
            Label lblsar_profit_center_cd = row.FindControl("lblsar_profit_center_cd") as Label;
            Label lblsard_chq_bank_cd = row.FindControl("lblsard_chq_bank_cd") as Label;
            Label lblsard_chq_dt = row.FindControl("lblsard_chq_dt") as Label;
            dgvRecDet.DataSource = new int[] { };
            //DataTable _dtRetChkCount = CHNLSVC.Financial.GetReturnChequeCount(lblsard_ref_no.Text, lblsard_chq_bank_cd.Text, lblsar_profit_center_cd.Text.ToUpper());
            DataTable _dtRetChkCount = CHNLSVC.Financial.GetReturnChequeCountWithoutPc(lblsard_ref_no.Text, lblsard_chq_bank_cd.Text);
            if (Convert.ToInt32(_dtRetChkCount.Rows[0][0].ToString()) <= 0)
            {
                if (Convert.ToInt32(_dtRetChkCount.Rows[0][0].ToString()) <= 0)
                {

                    DataTable datasource = CHNLSVC.Financial.GetReturnChequesNew(lblsar_profit_center_cd.Text.ToUpper(), lblsard_ref_no.Text, lblsard_chq_bank_cd.Text);
                    DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(lblsar_profit_center_cd.Text.ToUpper(), lblsard_ref_no.Text, lblsard_chq_bank_cd.Text);
                    datasource.Merge(remsource);
                    if (datasource.Rows.Count > 0)
                    {
                        dgvRecDet.DataSource = datasource;
                    }
                }
                GridViewDataBind(lblsard_ref_no.Text.Trim(), lblsard_chq_bank_cd.Text.Trim().ToUpper(), lblsar_profit_center_cd.Text.Trim().ToUpper());
                #region
                DataTable dt = CHNLSVC.Financial.get_rtn_chq_byBankChqNo(lblsard_chq_bank_cd.Text.Trim(), lblsard_ref_no.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["srcq_pc"]) == lblsar_profit_center_cd.Text)
                        {
                            lbtnSave.Enabled = false;
                            lbtnSave.OnClientClick = "";
                            lbtnSave.CssClass = "buttoncolor";
                        }
                        //MessageBox.Show("Already Returned!");
                        DataTable datasource = CHNLSVC.Financial.GetReturnChequesNew(lblsar_profit_center_cd.Text.ToUpper(), lblsard_ref_no.Text, lblsard_chq_bank_cd.Text.Trim());
                        DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(lblsar_profit_center_cd.Text.ToUpper(), lblsard_ref_no.Text, lblsard_chq_bank_cd.Text.Trim());

                        datasource.Merge(remsource);

                        //GridViewCheques.DataSource = null;
                        //GridViewCheques.AutoGenerateColumns = false;
                        //GridViewCheques.DataSource = datasource;

                        Decimal total = 0;
                        decimal _mgrChg = 0;
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (Convert.ToString(dr["srcq_pc"]) == lblsar_profit_center_cd.Text.ToUpper())
                                {
                                    total = total + Convert.ToDecimal(dr["srcq_act_val"]);
                                    _mgrChg = _mgrChg + Convert.ToDecimal(dr["srcq_mgr_chg"]);
                                }
                            }
                            txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                            txtTotRetAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                        }
                        //txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                        txtTotRetAmt.Text = string.Format("{0:n2}", total);//total.ToString();


                        total = 0;
                        foreach (DataRow dr in datasource.Rows)
                        {
                            //if (Convert.ToString(dr["srcq_pc"]) == TextBoxLocation.Text)
                            //{
                            total = total + Convert.ToDecimal(dr["sard_settle_amt"]);
                            //}
                        }
                        txtTotSysAmt.Text = string.Format("{0:n2}", total);//total.ToString();
                        txtTotManAmt.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                    }

                }

                #endregion
            }
            else
            {
                DispMsg("Cheque has been already returned !");
                
                lbtnSave.Enabled = false;
                lbtnSave.OnClientClick = "";
                lbtnSave.CssClass = "buttoncolor";
                return;
            }
            dgvRecDet.DataBind();

            #region check clear data fill
            txtCorrChkNo.Text = lblsard_ref_no.Text;
            ddlCorrBank.SelectedIndex = ddlCorrBank.Items.IndexOf(ddlCorrBank.Items.FindByValue(lblsard_chq_bank_cd.Text));
            txtCorrBranch.Text = lblsard_chq_branch.Text;
            txtCorrDate.Text = lblsard_chq_dt.Text;

            _oldChkNo = lblsard_ref_no.Text;
            _oldBankCd = lblsard_chq_bank_cd.Text;
            _oldBranch = lblsard_chq_branch.Text;
            _propCenter = lblsar_profit_center_cd.Text;
            _com = txtCompany.Text.Trim().ToUpper();
            #endregion
        }

        protected void txtCorrChkNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCorrBranch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCorrBranch.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCorrBranch.Text))
                {
                    if (ddlCorrBank.SelectedIndex<1)
                    {
                        ddlCorrBank.Focus(); DispMsg("Please select the bank !"); return;
                    }
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                    _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, "CODE", txtCorrBranch.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCorrBranch.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCorrBranch.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCorrBranch.Text = string.Empty;
                        txtCorrBranch.Focus();
                        DispMsg("Please enter valid branch !");
                        return;
                    }
                    txtChequeNo.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCorrBank_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSeCorrBank_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSeCorrBranch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCorrBank.SelectedIndex<1)
                {
                    ddlCorrBank.Focus();  DispMsg("Please select the bank ! "); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                _serData = CHNLSVC.CommonSearch.SearchBankBranchData(_para, null, null);
                LoadSearchPopup("CORRBankBranch", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnEditCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCorrDate.Text))
                {
                    DispMsg("Please select the cheque date"); return;
                }
                string bankCd = ddlCorrBank.SelectedValue;
                string branch = txtCorrBranch.Text.Trim();
                int result = CHNLSVC.Financial.UpdateReturnCheques(txtCorrChkNo.Text.Trim(), _oldChkNo, ddlCorrBank.SelectedValue, _oldBankCd, 
                   txtCorrBranch.Text.Trim(), Convert.ToDateTime(txtCorrDate.Text).Date, _com, _propCenter);
                if (result > 0)
                {
                    DispMsg("Updated Successfully!","S");
                    //lbtnClearCheque_Click(null, null);
                }
                else
                {
                    DispMsg("Return cheque not updated !", "S");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnClearCheque_Click(object sender, EventArgs e)
        {
            txtCorrChkNo.Text = "";
            txtCorrDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            //TextBoxNChequeNumber.Text = "";
            txtCorrBranch.Text = "";
            ddlCorrBank.SelectedIndex = 0;
        }

        protected void txtRetBank_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtRetBank.ToolTip = string.Empty;
                txtRetBankDesc.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtRetBank.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                    _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para, "CODE", txtRetBank.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtRetBank.Text.ToUpper().Trim(), "CODE", "Description");
                    txtRetBank.ToolTip = _ava ? _toolTip : "";
                    txtRetBankDesc.Text = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtRetBank.Text = string.Empty;
                        txtRetBank.Focus();
                        DispMsg("Please enter valid return bank !");
                        return;
                    }
                   // txtBranch.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeRetBank_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                _serData = CHNLSVC.CommonSearch.SearchBankDetails(_para, null, null);
                LoadSearchPopup("REBank", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _dtFrom=new DateTime(2016,08,01);
                DateTime _dtTo=new DateTime(2016,08,31);
                DataTable _dt = CHNLSVC.Inventory.CalculateAodExcelData(_dtFrom, _dtTo);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void ddlCorrBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCorrBranch.Text = "";
            txtCorrBranch_TextChanged(null, null);
        }

       

       

    }
}