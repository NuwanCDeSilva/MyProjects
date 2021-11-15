using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.IO;

namespace FF.WebERPClient.HP_Module
{
    public partial class HpRevert : BasePage
    {

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["HpRevertViewState"];
            LosFormatter m_formatter = new LosFormatter();
            try
            {
                viewStateBag = m_formatter.Deserialize(m_viewState);
            }
            catch
            {
                throw new HttpException("The View State is invalid.");
            }
            return viewStateBag;
        }
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            LosFormatter m_formatter = new LosFormatter();
            m_formatter.Serialize(ms, viewState);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string viewStateString = sr.ReadToEnd();
            Session["HpRevertViewState"] = viewStateString;
            ms.Close();
            return;
        }



        protected List<ReptPickSerials> _popUpList
        {
            get { return (List<ReptPickSerials>)Session["_popUpList"]; }
            set { Session["_popUpList"] = value; }
        }
        protected List<ReptPickSerials> _selectedItemList
        {
            get { return (List<ReptPickSerials>)Session["_selectedItemList"]; }
            set { Session["_selectedItemList"] = value; }
        }
        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)Session["AccountsList"]; }
            set { Session["AccountsList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAccountItem(string.Empty);
                BindSelectedItems(null);
                _selectedItemList = new List<ReptPickSerials>();
                txtProfitCenter.Attributes.Add("onkeyup", "return clickButton(event,'" + ImgBtnProfitCenter.ClientID + "')");
                txtProfitCenter.Attributes.Add("onblur", "CheckProfitCenter('" + txtProfitCenter.ClientID + "')");
                ProfitCenterValidate();
                txtDate.Text = FormatToDate(DateTime.Now.Date.ToShortDateString());
                txtAccountNo.Attributes.Add("onkeyup", "return clickButton(event,'" + ImgBtnAccountNo.ClientID + "')");
                txtAccountNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAccount, ""));
            }
        }

        #region Search
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + txtProfitCenter.Text.Trim() + seperator + "A" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void ImgBtnPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtProfitCenter.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }
        protected void ImgAccountSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid profit center"); return; }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccountNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        #endregion

        private void ProfitCenterValidate()
        {
            string v = Request.QueryString["pc"];
            if (v != null) { MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, v); if (_pc != null) { txtProfitCenter.Text = v; return; } }
            txtProfitCenter.Text = GlbUserDefProf;
        }
        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            LoadAccountDetail(accountNo, DateTime.Now.Date);
        }
        private void LoadAccountDetail(string _account, DateTime _date)
        {
            lblAccountNo.Text = _account;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, _account);
            //lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
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

                lblAccountDate.Text = FormatToDate(account.Hpa_acc_cre_dt.ToShortDateString());
                if (CheckBoxView.Checked)
                    uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, _date.Date, GlbUserDefProf);
                BindAccountItem(account.Hpa_acc_no);
                btnProcess.Enabled = true;
                if (CheckEligibilityForRevert(account.Hpa_acc_no))
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Advice: This account is <label style='font-weight:bold;'> NOT </label> eligible for revert");
            }
        }
        protected void btn_validateACC_Click(object sender, EventArgs e)
        {
            BindAccountItem(string.Empty);
            BindSelectedItems(null);
            BindCustomerDetails(null);
            _selectedItemList = new List<ReptPickSerials>();


            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid profit center");
                txtProfitCenter.Text = string.Empty;
                return;
            }
            string location = txtProfitCenter.Text.Trim();
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

                    
                    LoadAccountDetail(ac.Hpa_acc_no, DateTime.Now.Date);


                }
            }
            else if (accList.Count > 1)
            {
                //show a pop up to select the account number
                grvMpdalPopUp.DataSource = accList;
                grvMpdalPopUp.DataBind();
                ModalPopupExtItem.Show();
            }

        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblACode.Text = string.Empty;
            lblAName.Text = string.Empty;
            lblAAddress1.Text = string.Empty;
            if (_hdr != null)
            {
                lblACode.Text = _hdr.Sah_cus_cd;
                lblAName.Text = _hdr.Sah_cus_name;
                lblAAddress1.Text = _hdr.Sah_d_cust_add1 + " " + _hdr.Sah_d_cust_add2;
            }
        }
        private void BindAccountItem(string _account)
        {

            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;

            if (_invoice != null)
                if (_invoice.Count > 0)
                    _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvATradeItem.DataSource = _table;
            }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                    #region New Method
                    var _sales = from _lst in _invoice
                                 where _lst.Sah_direct == true
                                 select _lst;

                    foreach (InvoiceHeader _hdr in _sales)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }
                    #endregion
                    gvATradeItem.DataSource = _itemList;
                }

            gvATradeItem.DataBind();



            if (_hdrs != null)
                BindCustomerDetails(_hdrs);

        }
        private void BindSelectedItems(List<ReptPickSerials> _list)
        {
            if (_list == null) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else if (_list.Count <= 0) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else { gvAReturnItem.DataSource = _list; }
            gvAReturnItem.DataBind();
        }
        protected void SelectedItem_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
            if (_selectedItemList == null) return;
            if (_selectedItemList.Count <= 0) return;

            int row_id = e.RowIndex;
            Int32 _serialID = (Int32)gvAReturnItem.DataKeys[row_id][0];
            string _item = (string)gvAReturnItem.DataKeys[row_id][1];

            MasterItem _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 != -1)
            {
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                _selectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
            }
            else
            {
                _selectedItemList.RemoveAll(x => x.Tus_itm_cd == _item);
            }

            BindSelectedItems(_selectedItemList);

        }
        static int _count = 0;
        protected void AccountItem_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnIsForward = (HiddenField)e.Row.FindControl("hdnIsForwardSale");
                    HiddenField _hdnInvoiceNo = (HiddenField)e.Row.FindControl("hdnInvoiceNo");
                    HiddenField _hdnDONo = (HiddenField)e.Row.FindControl("hdnDONo");
                    HiddenField _hdnLineNo = (HiddenField)e.Row.FindControl("hdnlineNo");

                    if (Convert.ToBoolean(_hdnIsForward.Value.ToString()) == true)
                    {
                        e.Row.Style.Add("color", "#990000"); //e.Row.Style.Add("background-color", "#9DC2D5");
                        e.Row.Style.Add("font-weight", "bold");
                    }

                    e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                    e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(
                                gvATradeItem,
                                String.Concat("Select$", e.Row.RowIndex),
                                true);

                    _count += 1;

                }

        }

        private void BindPopSerial(List<ReptPickSerials> _list)
        {
            gvPopSerial.DataSource = _list;
            gvPopSerial.DataBind();
        }
        //Add double click item to IN area
        private void AddInItem(ReptPickSerials _ser)
        {
            _popUpList.Add(_ser);
        }
        protected void BindSelectedAccItemetail(Object sender, EventArgs e)
        {
            //DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no"
            string _sad_itm_cd = gvATradeItem.SelectedDataKey[0].ToString();
            decimal _sad_qty = Convert.ToDecimal(gvATradeItem.SelectedDataKey[1].ToString());
            decimal _sad_unit_rt = Convert.ToDecimal(gvATradeItem.SelectedDataKey[2].ToString());
            Int32 _sad_itm_line = Convert.ToInt32(gvATradeItem.SelectedDataKey[3].ToString());
            bool _isForwardSale = Convert.ToBoolean(gvATradeItem.SelectedDataKey[4].ToString());
            string _invoice = gvATradeItem.SelectedDataKey[5].ToString();
            string _status = gvATradeItem.SelectedDataKey[6].ToString();
            hdnSystemQty.Value = "0";

            MasterItem _itms = CHNLSVC.Inventory.GetItem(GlbUserComCode, _sad_itm_cd);

            if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == false)
            {

                if (_itms.Mi_is_ser1 == -1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exchange not processing for decimal allow items");
                    return;
                }
                List<InventoryBatchN> _lst = new List<InventoryBatchN>();
                _lst = CHNLSVC.Inventory.GetDeliveryOrderDetail(GlbUserComCode, _invoice, _sad_itm_line);

                string _docno = string.Empty;
                int _itm_line = -1;
                int _batch_line = -1;
                List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbUserSessionID, string.Empty, _invoice, _sad_itm_line);
                _popUpList = _serLst;

                //Behaviour of the serialized item
                if (_itms.Mi_is_ser1 == 1)
                {
                    #region Serialized Item Handle
                    if (_lst != null && _serLst != null)
                        if (_serLst.Count > 0)
                            if (_sad_qty > 1)
                            {
                                //More than one qty
                                BindPopSerial(_serLst);
                                divPopUpNonSeral.Visible = false;
                                MPESerial.Show();
                            }
                            else if (_sad_qty <= 1)
                            {
                                //only one qty
                                _docno = _lst[0].Inb_doc_no;
                                _itm_line = _lst[0].Inb_itm_line;
                                _batch_line = _lst[0].Inb_batch_line;
                                AddInItem(_serLst[0]);
                                foreach (ReptPickSerials _lt in _popUpList)
                                {
                                    string _item = _lt.Tus_itm_cd;
                                    Int32 _serialID = _lt.Tus_ser_id;
                                    MasterItem _items = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                                    if (_items.Mi_is_ser1 != -1)
                                    {
                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_ser_id == Convert.ToInt32(_serialID)
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lssst in _selectedItemList
                                                                         where _lssst.Tus_ser_id == _one.Tus_ser_id
                                                                         select _lssst;

                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }
                                    else
                                    {

                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_itm_cd == _item
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lsst in _selectedItemList
                                                                         where _lsst.Tus_itm_cd == _one.Tus_itm_cd
                                                                         select _lsst;
                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }

                                }

                            }
                    #endregion
                }

                //Behaviour of the non-serialized item
                else if (_itms.Mi_is_ser1 == 0)
                {
                    #region Non-Serialized Item
                    if (_lst != null && _serLst != null)
                        if (_serLst.Count > 0)
                            if (_sad_qty > 1)
                            {
                                //More than one qty
                                BindPopSerial(_serLst);
                                divPopUpNonSeral.Visible = true;
                                txtPopQty.Text = FormatToQty(Convert.ToString(_sad_qty));
                                hdnSystemQty.Value = txtPopQty.Text;
                                MPESerial.Show();
                            }
                            else if (_sad_qty <= 1)
                            {
                                //only one qty
                                _docno = _lst[0].Inb_doc_no;
                                _itm_line = _lst[0].Inb_itm_line;
                                _batch_line = _lst[0].Inb_batch_line;
                                AddInItem(_serLst[0]);
                                foreach (ReptPickSerials _lt in _popUpList)
                                {
                                    string _item = _lt.Tus_itm_cd;
                                    Int32 _serialID = _lt.Tus_ser_id;
                                    MasterItem _items = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                                    if (_items.Mi_is_ser1 != -1)
                                    {
                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_ser_id == Convert.ToInt32(_serialID)
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lssst in _selectedItemList
                                                                         where _lssst.Tus_ser_id == _one.Tus_ser_id
                                                                         select _lssst;

                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }
                                    else
                                    {

                                        var _match = (from _lsst in _popUpList
                                                      where _lsst.Tus_itm_cd == _item
                                                      select _lsst);
                                        if (_match != null)
                                            foreach (ReptPickSerials _one in _match)
                                            {
                                                if (_selectedItemList != null)
                                                    if (_selectedItemList.Count > 0)
                                                    {
                                                        var _duplicate = from _lsst in _selectedItemList
                                                                         where _lsst.Tus_itm_cd == _one.Tus_itm_cd
                                                                         select _lsst;
                                                        if (_duplicate.Count() <= 0)
                                                        {
                                                            _selectedItemList.Add(_one);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _selectedItemList.Add(_one);
                                                    }
                                            }
                                    }

                                }
                            }
                    #endregion
                }
            }
            else if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "The selected item is not delivered!");
                return;
            }
            BindSelectedItems(_selectedItemList);

        }
        protected bool MyFunction(string _ispick)
        {
            if (_ispick == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void ConfirmPopUpSerial_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvPopSerial.Rows)
            {
                CheckBox _chk = (CheckBox)gvr.Cells[0].FindControl("chkPopSerSelect");

                if (_chk.Checked)
                {
                    HiddenField _serialId = (HiddenField)gvr.Cells[0].FindControl("hdnPopSerialID");
                    HiddenField _item = (HiddenField)gvr.Cells[0].FindControl("hdnPopItem");
                    MasterItem _items = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item.Value);
                    if (_items.Mi_is_ser1 != -1)
                    {
                        var _match = (from _lst in _popUpList
                                      where _lst.Tus_ser_id == Convert.ToInt32(_serialId.Value)
                                      select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList
                                                         where _lst.Tus_ser_id == _one.Tus_ser_id
                                                         select _lst;

                                        if (_duplicate.Count() <= 0)
                                        {
                                            _selectedItemList.Add(_one);
                                        }
                                    }
                                    else
                                    {
                                        _selectedItemList.Add(_one);
                                    }
                            }
                    }
                    else
                    {
                        var _match = (from _lst in _popUpList
                                      where _lst.Tus_itm_cd == _item.Value.ToString()
                                      select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList
                                                         where _lst.Tus_itm_cd == _one.Tus_itm_cd
                                                         select _lst;
                                        if (_duplicate.Count() <= 0)
                                        {
                                            _selectedItemList.Add(_one);
                                        }

                                    }
                                    else
                                    {
                                        _selectedItemList.Add(_one);
                                    }
                            }
                    }

                }
            }
            BindSelectedItems(_selectedItemList);
        }

        private bool CheckEligibilityForRevert(string accNo)
        {
            HpAccount _ac = CHNLSVC.Sales.GetHP_Account_onAccNo(accNo);
            decimal _clsBal = 0;
            int result =CHNLSVC.Financial.GetClosingBalance(DateTime.Now.Date, _ac.Hpa_acc_no, out _clsBal);
            if (_ac != null)
            {
                decimal _eligibility = _clsBal / _ac.Hpa_hp_val * 100;
                if (_eligibility <= 25)
                    return true;
                else
                    return false;
            }
            return false;
            //decimal _hirevalue = uc_HpAccountSummary1.Uc_HireValue;
            //decimal _accountbalance = uc_HpAccountSummary1.Uc_AccBalance;
            //decimal _eligibility = _accountbalance / _hirevalue * 100;
            //if (_eligibility <= 25)
            //    return true;
            //else
            //    return false;




        }

        protected void Process(object sender, EventArgs e)
        {
            #region Check for fulfilment
            if (_selectedItemList == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the revert item");
                return;
            }

            else if (_selectedItemList.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the revert item");
                return;
            }

            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the profit center");
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the date");
                txtDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the account no");
                txtAccountNo.Focus();
                return;
            }

            #endregion

            string _bin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);

            _selectedItemList.ForEach(x => x.Tus_loc = GlbUserDefLoca);
            _selectedItemList.ForEach(x => x.Tus_bin = _bin);

            #region Fill Revert Header

            HpRevertHeader _rvhdr = new HpRevertHeader();
            _rvhdr.Hrt_acc_no = lblAccountNo.Text;
            _rvhdr.Hrt_bal = 0;
            _rvhdr.Hrt_bal_cap = 0;
            _rvhdr.Hrt_bal_intr = 0;
            _rvhdr.Hrt_com = GlbUserComCode;
            _rvhdr.Hrt_cre_by = GlbUserName;
            _rvhdr.Hrt_cre_dt = DateTime.Now.Date;
            _rvhdr.Hrt_is_rls = false;
            _rvhdr.Hrt_mod_by = GlbUserName;
            _rvhdr.Hrt_mod_dt = DateTime.Now.Date;
            _rvhdr.Hrt_pc = txtProfitCenter.Text;
            _rvhdr.Hrt_ref = "0";
            _rvhdr.Hrt_rvt_dt = Convert.ToDateTime(txtDate.Text);
            _rvhdr.Hrt_seq = 0;
            _rvhdr.Hrt_rvt_comment = txtRemarks.Text;


            //ADDED BY SACHITH 2012/11/05
            //ADD HRT_RVT_BY,HRT_RLS_DT

            string _rvtBy=(string.IsNullOrEmpty(txtRevertedBy.Text))?GlbUserName:txtRevertedBy.Text;
            _rvhdr.Hrt_rvt_by=_rvtBy;
            _rvhdr.Hrt_rls_dt = new DateTime(9999,12,31);

            //END
        

            #endregion

            #region Fill Inventory Header
            InventoryHeader inHeader = new InventoryHeader();
            inHeader.Ith_acc_no = lblAccountNo.Text;
            inHeader.Ith_anal_10 = true;
            inHeader.Ith_anal_11 = true;
            inHeader.Ith_anal_12 = true;
            inHeader.Ith_anal_2 = lblAccountNo.Text;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_bus_entity = string.Empty;
            inHeader.Ith_cate_tp = "NOR";
            inHeader.Ith_channel = string.Empty;
            inHeader.Ith_com = GlbUserComCode;
            inHeader.Ith_com_docno = string.Empty;
            inHeader.Ith_cre_by = string.Empty;
            inHeader.Ith_cre_when = DateTime.MinValue;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";
            inHeader.Ith_direct = true;
            inHeader.Ith_doc_date = DateTime.Today;
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = DateTime.Today.Year;
            inHeader.Ith_entry_no = string.Empty;
            inHeader.Ith_entry_tp = string.Empty;
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = "1";
            inHeader.Ith_isprinted = true;
            inHeader.Ith_is_manual = true;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = GlbUserDefLoca;
            inHeader.Ith_manual_ref = string.Empty;
            inHeader.Ith_mod_by = GlbUserName;
            inHeader.Ith_mod_when = DateTime.MinValue;
            inHeader.Ith_noofcopies = 2;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_remarks = txtRemarks.Text;
            inHeader.Ith_sbu = string.Empty;
            inHeader.Ith_seq_no = 6;
            inHeader.Ith_session_id = GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "RV";
            inHeader.Ith_vehi_no = string.Empty;
            #endregion

            #region Fill Inentory AutoNumber
            MasterAutoNumber invAuto = new MasterAutoNumber();
            invAuto.Aut_cate_cd = GlbUserDefLoca;
            invAuto.Aut_cate_tp = "LOC";
            invAuto.Aut_direction = null;
            invAuto.Aut_modify_dt = null;
            invAuto.Aut_moduleid = "ADJ";
            invAuto.Aut_number = 0;
            invAuto.Aut_start_char = "ADJ";
            invAuto.Aut_year = null;
            #endregion

            #region Fill Revert AutoNumber
            MasterAutoNumber rvAuto = new MasterAutoNumber();
            rvAuto.Aut_cate_cd = GlbUserDefLoca;
            rvAuto.Aut_cate_tp = "PC";
            rvAuto.Aut_direction = 1;
            rvAuto.Aut_modify_dt = null;
            rvAuto.Aut_moduleid = "RV";
            rvAuto.Aut_number = 0;
            rvAuto.Aut_start_char = "RV";
            rvAuto.Aut_year = null;
            string _rvdoc = string.Empty;
            string _adjdoc = string.Empty;
            #endregion

            try
            {
                btnProcess.Enabled = false;
                btnClear.Enabled = false;
                btnClose.Enabled = false;
                CHNLSVC.Sales.SaveRevert(0, _rvhdr, inHeader, _selectedItemList, null, rvAuto, invAuto, out _rvdoc, out _adjdoc);
                //CHNLSVC.Sales.SaveRevert(uc_HpAccountSummary1.Uc_AccBalance / uc_HpAccountSummary1.Uc_CashPrice, _rvhdr, inHeader, _selectedItemList, null, rvAuto, invAuto, out _rvdoc, out _adjdoc);
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                btnClose.Enabled = true;
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                btnClose.Enabled = true;
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, ex.Message);
                return;
            }
            finally
            {
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                btnClose.Enabled = true;
                //TODO: Revert Print goes here

                //ADDED BY SACHITH 2012/11/05
                //GET MESSAGE AND PRINT

                string message = "";
                if (CheckEligibilityForRevert(lblAccountNo.Text))
                {
                    message = "Customer paid more than 75% of Hire price";
                }

                GlbDocNosList = _rvdoc;
                GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";

                GlbMainPage = "~/Inventory_Module/InterCompanyOutWardEntry.aspx";
               
                //END

                string Msg = "<script> alert('Successfully Saved! Document No : " + _rvdoc + " and inventory document : " + _adjdoc + " \n"+message+"'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts123", Msg, false);
                Msg = "<script> window.open('../Reports_Module/Inv_Rep/Print.aspx','_blank');  </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts23", Msg, false);

                Msg = "<script> window.location = 'HpRevert.aspx';  </script>";
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "alerts2311", Msg, false);
            }
        }

        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        protected void Clear(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/HpRevert.aspx", false);
        }

        protected void CheckBoxView_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxView.Checked)
            {
                    uc_HpAccountSummary1.Visible = true;
                    LoadAccountDetail(lblAccountNo.Text, DateTime.Now);
            }
            else
                uc_HpAccountSummary1.Visible = false;
            }
        }
    }
