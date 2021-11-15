using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;


namespace FF.WebERPClient.Inventory_Module
{
    public partial class InterCompanyOutWardEntry : BasePage
    {

        protected MasterItem _masterItem = null;
        private List<InventoryLocation> _inventoryLocation = null;
        const string COM_OUT = "COM_OUT";

        private List<InventoryRequestItem> ScanItemList
        {
            get { return (List<InventoryRequestItem>)ViewState["ScanItemList"]; }
            set { ViewState["ScanItemList"] = value; }
        }

        private string _receCompany
        {
            get { return Convert.ToString(Session["_receCompany"]); }
            set { Session["_receCompany"] = value; }
        }


        //Request Types -combo
        //from date
        //to date

        //date
        //request no

        //rec com -f2
        //rec loc -f2
        //remarks
        //veh

        //ser1 -f2
        //ser2 -f2
        //ser3 -f2

        //bin - combo
        //item -f2
        //status - combo
        //qty

        //AD-HOC SESSION ASSIGNMENT FOR CHECKING PURPOSE ONLY
        private void Ad_hoc_sessions()
        {
            Session["UserID"] = "ADMIN";
            Session["UserCompanyCode"] = "ABL";
            Session["SessionID"] = "666";
            Session["UserDefLoca"] = "MSR01";
            Session["UserDefProf"] = "39";


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Ad_hoc_sessions();

            if (!IsPostBack)
            {
                ScanItemList = new List<InventoryRequestItem>();
                BindRequestTypesDDLData();
                BindReceiveCompany();
                BindMRNListGridData();
                BindPickSerials(0);
                BindMrnDetail(string.Empty);


                //For item search
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");
                txtDispatchRequried.Attributes.Add("onKeyup", "return clickButton(event,'" + imgDispatchRequried.ClientID + "')");
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
         
            }

            txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
            txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));
            txtDispatchRequried.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnLocation, ""));

            IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgDate, lblDispalyInfor);

        }

        #region  Data Bind

        private void BindRequestTypesDDLData()
        {
            ddlType.Items.Clear();
            List<MasterType> _masterType = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.REQ.ToString());
            _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString()));
            _masterType.Add(new MasterType { Mtp_cd = "-1", Mtp_desc = "" });
            ddlType.DataSource = _masterType.OrderBy(items => items.Mtp_desc).Where(x => x.Mtp_desc != "DAMAGE INFORM");
            ddlType.DataTextField = "Mtp_desc";
            ddlType.DataValueField = "Mtp_cd";
            ddlType.DataBind();
        }

        private void BindReceiveCompany()
        {
            DDLRecCompany.Items.Clear();
            List<MasterCompany> _list = CHNLSVC.General.GetALLMasterCompaniesData();
            _list.Add(new MasterCompany { Mc_cd = "" });
            DDLRecCompany.DataSource = _list.OrderBy(items => items.Mc_cd);
            DDLRecCompany.DataTextField = "Mc_cd";
            DDLRecCompany.DataValueField = "Mc_cd";
            DDLRecCompany.DataBind();


        }

        private void BindMRNListGridData()
        {
            InventoryRequest _inventoryRequest = new InventoryRequest();
            //_inventoryRequest.Itr_com = GlbUserComCode;
            //edit chamal 18-08-2012
            _inventoryRequest.Itr_issue_com = GlbUserComCode;
            _inventoryRequest.Itr_tp = ddlType.SelectedValue.ToString() == "-1" ? string.Empty : ddlType.SelectedValue.ToString();
            _inventoryRequest.Itr_loc = GlbUserDefLoca;
            _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
            _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
            DataTable _table = CHNLSVC.Inventory.GetAllMaterialRequestsTable(_inventoryRequest);

            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvPending.DataSource = _table;
            }
            else
            {
                gvPending.DataSource = CHNLSVC.Inventory.GetAllMaterialRequestsList(_inventoryRequest).Where(X => X.Itr_tp != "DIN");
            }
            gvPending.DataBind();
        }

        protected void BindPickSerials(int _userSeqNo)
        {
            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, COM_OUT);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvSerial.DataSource = _table;

            }
            else
            {
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, COM_OUT);
                gvSerial.DataSource = _list;
            }
            gvSerial.DataBind();

        }

        static int _count = 0;
        protected void OnPendingRequestBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                            gvPending,
                            String.Concat("Select$", e.Row.RowIndex),
                            true);

                _count += 1;
            }
        }
        protected void BindSelectedMRNDetail(Object sender, EventArgs e)
        {
            if (!chkDirect.Checked)
            {
                RequestNo = gvPending.SelectedDataKey.Value.ToString();
                //HWCHANGE
                UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, GlbUserComCode, RequestNo, 0);

                DDLRecCompany.Items.Clear();
                txtDispatchRequried.Enabled = false;
                DDLRecCompany.Items.Add(gvPending.SelectedDataKey[1].ToString());
                txtDispatchRequried.Text = gvPending.SelectedDataKey[2].ToString();
                string _reqtype = gvPending.SelectedDataKey[3].ToString();
                string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                if (UserSeqNo <= 0 && _reqtype.Trim() == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString())
                {
                    List<InventoryRequestSerials> _list = CHNLSVC.Inventory.GetAllGRANSerialsList(GlbUserComCode, GlbUserDefLoca, CommonUIDefiniton.MasterTypeCategory.GRAN.ToString(), RequestNo);
                    bool _isLowStock = false;
                    string _lowstockitem = string.Empty;
                    foreach (InventoryRequestSerials _one in _list)
                    {
                        string _serial = _one.Itrs_ser_1;
                        string _item = _one.Itrs_itm_cd;
                        Int64 _serialId = _one.Itrs_ser_id;

                        MasterItem msitem = new MasterItem();
                        msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                        if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                        {
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, string.Empty, _item, Convert.ToInt32(_serialId));
                            if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                            {
                                if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ",serial : " + _serial; else _lowstockitem += ", item : " + _item + ",serial : " + _serial;
                                _isLowStock = true;
                            }
                        }
                        else
                        {
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, _item, _one.Itrs_itm_stus);
                            if (_inventoryLocation != null)
                                if (_inventoryLocation.Count > 0)
                                {
                                    decimal _invBal = _inventoryLocation[0].Inl_qty;
                                    if (_one.Itrs_qty > _invBal)
                                    {
                                        if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                        _isLowStock = true;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                    _isLowStock = true;
                                }
                            else
                            {
                                if (string.IsNullOrEmpty(_lowstockitem)) _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus; else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                _isLowStock = true;
                            }
                        }
                    }

                    if (_isLowStock) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no stock for the followin item(s)." + _lowstockitem); return; }

                    foreach (InventoryRequestSerials _one in _list)
                    {
                        string _serial = _one.Itrs_ser_1;
                        string _item = _one.Itrs_itm_cd;
                        Int64 _serialId = _one.Itrs_ser_id;

                        Int32 generated_seq = -1;
                        Int32 userseq_no;

                        Int32 user_seq_num = 0;
                        if (UserSeqNo == 0)
                            user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, GlbUserComCode, RequestNo, 0);
                        else
                            user_seq_num = UserSeqNo;

                        if (user_seq_num != -1)
                        {
                            generated_seq = user_seq_num;
                            userseq_no = generated_seq;
                            UserSeqNo = userseq_no;
                        }
                        else
                        {
                            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, COM_OUT, 1, GlbUserComCode);//direction always =1 for this method
                            userseq_no = generated_seq;
                            UserSeqNo = userseq_no;
                            ReptPickHeader RPH = new ReptPickHeader();
                            RPH.Tuh_doc_tp = COM_OUT;
                            RPH.Tuh_cre_dt = DateTime.Today;
                            RPH.Tuh_ischek_itmstus = false;
                            RPH.Tuh_ischek_reqqty = false;
                            RPH.Tuh_ischek_simitm = false;
                            RPH.Tuh_session_id = GlbUserSessionID;
                            RPH.Tuh_usr_com = GlbUserComCode;
                            RPH.Tuh_usr_id = GlbUserName;
                            RPH.Tuh_usrseq_no = generated_seq;
                            RPH.Tuh_direct = false;
                            RPH.Tuh_doc_no = RequestNo;
                            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                        }

                        MasterItem msitem = new MasterItem();
                        msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                        if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                        {
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, string.Empty, _item, Convert.ToInt32(_serialId));
                            _reptPickSerial_.Tus_cre_by = GlbUserName;
                            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, Convert.ToInt32(_serialId), -1);
                            _reptPickSerial_.Tus_cre_by = GlbUserName;
                            _reptPickSerial_.Tus_base_doc_no = RequestNo;
                            _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no;
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(_reptPickSerial_.Tus_ser_id);
                            _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus;
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        }
                        else
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_com = GlbUserComCode;
                            _reptPickSerial_.Tus_base_doc_no = RequestNo;
                            _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no;
                            _reptPickSerial_.Tus_bin = _defbin;
                            _reptPickSerial_.Tus_cre_by = GlbUserName;
                            _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                            _reptPickSerial_.Tus_cross_batchline = 0;
                            _reptPickSerial_.Tus_cross_itemline = 0;
                            _reptPickSerial_.Tus_cross_seqno = 0;
                            _reptPickSerial_.Tus_cross_serline = 0;
                            _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtDate.Text);
                            _reptPickSerial_.Tus_doc_no = "N/A";
                            _reptPickSerial_.Tus_exist_grncom = "N/A";
                            _reptPickSerial_.Tus_isapp = 1;
                            _reptPickSerial_.Tus_iscovernote = 1;
                            _reptPickSerial_.Tus_itm_brand = msitem.Mi_brand;
                            _reptPickSerial_.Tus_itm_cd = _item;
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_line = 0;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_itm_stus = _one.Itrs_itm_stus;
                            _reptPickSerial_.Tus_loc = GlbUserDefLoca;
                            _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus;
                            _reptPickSerial_.Tus_qty = _one.Itrs_qty;
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_ser_line = 0;
                            _reptPickSerial_.Tus_session_id = GlbUserSessionID;
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            _reptPickSerial_.Tus_warr_no = "N/A";
                            _reptPickSerial_.Tus_warr_period = 0;
                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        }
                    }
                }
                txtRequest.Text = RequestNo;

                //HWCHANGE
                BindPickSerials(UserSeqNo);
                BindMrnDetail(RequestNo);
            }
            else
            {
                txtDispatchRequried.Text = string.Empty;
            }
        }

        public string RequestNo
        {
            get { return Convert.ToString(Session["PurchaseOrder"]); }
            set { Session["PurchaseOrder"] = value; }
        }
        public string SelectedStatus
        {
            get { return Convert.ToString(Session["SelectedStatus"]); }
            set { Session["SelectedStatus"] = value; }
        }


        protected void BindMrnDetail(string _mrn)
        {

            DataTable _table = CHNLSVC.Inventory.GetMaterialRequestItemByRequestNoTable(_mrn);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvItems.DataSource = _table;
            }
            else
            {
                if (!string.IsNullOrEmpty(_mrn))
                {
                    ScanItemList = CHNLSVC.Inventory.GetMaterialRequestItemByRequestNoList(_mrn);
                    gvItems.DataSource = ScanItemList;
                }
            }
            gvItems.DataBind();
        }

        protected void OnSelectedItemBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvItems, String.Concat("Select$", e.Row.RowIndex), true);

                _count += 1;

                string _item = e.Row.Cells[0].Text;
                string _status = e.Row.Cells[3].Text;

                if (string.IsNullOrEmpty(_status) || _status == "&nbsp;")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _item + " item status can not be empty. Please contact IT Dept.");
                }

                List<ReptPickSerials> _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, COM_OUT);
                Label _lblPickQty = (Label)e.Row.FindControl("lblPickQty");
                if (_list != null)
                    if (_list.Count > 0)
                    {
                        var _lst = (from _ls in _list
                                    where _ls.Tus_itm_cd == _item //&& _ls.Tus_itm_stus == _status
                                    select _ls.Tus_qty).Sum();

                        _lblPickQty.Text = _lst.ToString();
                    }
                    else
                    { _lblPickQty.Text = "0"; }
            }
        }

        //protected void BindSelectedItemToText(Object sender, EventArgs e)
        //{
        //    txtItem.Text = gvItems.SelectedDataKey[0].ToString();
        //    ddlStatus.Items.Clear();
        //    ddlStatus.Items.Add(gvItems.SelectedDataKey[1].ToString());
        //    SelectedStatus = gvItems.SelectedDataKey[1].ToString();
        //    if (!string.IsNullOrEmpty(txtItem.Text))
        //    {
        //        SelectedItemLineNo = Convert.ToInt32(gvItems.SelectedDataKey[3].ToString());
        //        CheckItem();
        //    }

        //}

        protected void BindItemStatus(string _company, string _location, string _bin, string _item, string _serial)
        {
            ddlStatus.Items.Clear();
            DataTable _tbl = CHNLSVC.Inventory.GetAvailableItemStatus(_company, _location, _bin, _item, _serial);
            ddlStatus.DataSource = _tbl;
            ddlStatus.DataTextField = "ins_itm_stus";
            ddlStatus.DataValueField = "ins_itm_stus";
            ddlStatus.DataBind();


        }

        #endregion

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
                        paramsText.Append(_receCompany + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItem.Text + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgDispatchRequried_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null); //Edit Chamal 25/06/2012
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDispatchRequried.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        #endregion

        protected void SearchRequest(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                if (string.IsNullOrEmpty(txtTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the date range!");
                    txtTo.Focus();
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the date range!");
                    txtFrom.Focus();
                    return;
                }
            }

            BindMRNListGridData();
        }
        protected void ReceiveCompany_OnChange(object sender, EventArgs e)
        {
            string _selectCompany = DDLRecCompany.SelectedItem.Text;
            _receCompany = _selectCompany;
            txtDispatchRequried.Text = string.Empty;

        }

        #region Check Serial 1

        //protected void CheckSerial()
        //{
        //    if (string.IsNullOrEmpty(txtSerial1.Text)) return;

        //    //check direction
        //    //if direction - out, get the serial details from the system.
        //    //if direction - in, check the serial availability.

        //    //check serial availability

        //    if (!string.IsNullOrEmpty(SelectedStatus) && !string.IsNullOrEmpty(RequestNo))
        //    {
        //        if (ddlStatus.SelectedValue != SelectedStatus)
        //        {
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item status and the serial status is different");
        //            txtSerial1.Text = "";
        //            txtSerial1.Focus();
        //            return;
        //        }
        //    }

        //    List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLBinCode.SelectedValue.ToString(), txtSerial1.Text.Trim(), txtSerial2.Text.Trim());

        //    if (_list != null || _list.Count > 0)
        //    {

        //        Int32 _noOfLines = _list.Count;

        //        //If only one line = means serial having single items
        //        if (_noOfLines == 1)
        //        {
        //            ReptPickSerials _List = new ReptPickSerials();
        //            foreach (ReptPickSerials _pick in _list)
        //            {
        //                _List = _pick;
        //            }

        //            DDLBinCode.Items.Clear();
        //            ddlStatus.Items.Clear();

        //            DDLBinCode.Items.Add(_List.Tus_bin);
        //            ddlStatus.Items.Add(_List.Tus_itm_stus);
        //            if (string.IsNullOrEmpty(txtItem.Text)) txtItem.Text = _List.Tus_itm_cd;
        //            txtSerial2.Text = _List.Tus_ser_2;
        //            txtSerial3.Text = _List.Tus_ser_3;
        //            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
        //            txtQty.Text = "1";
        //        }
        //        //if more than one line = means serial having more than item
        //        if (_noOfLines > 1)
        //        {
        //            //Load to the common modal
        //        }

        //    }
        //}
        //protected void CheckSerial1(object sender, EventArgs e)
        //{
        //    CheckSerial();
        //}

        #endregion

        #region Check Item

        protected void CheckItem()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;

            if (string.IsNullOrEmpty(txtRequest.Text))
                if (!chkDirect.Checked)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the Referance document or direct method first"); return;
                }

            if (string.IsNullOrEmpty(DDLRecCompany.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select receiving company"); return;
            }

            if (string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select receiving location"); return;
            }

            _masterItem = new MasterItem();

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);
            if (_masterItem != null)
                lblModel.Text = "Description : " + _masterItem.Mi_longdesc + "    Model : " + _masterItem.Mi_model;

            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
            if (_inventoryLocation == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No stock balance available"); return; }

            var _status = from _ls in _inventoryLocation
                          select _ls.Inl_itm_stus;
            ddlStatus.DataSource = _status;
            ddlStatus.DataBind();





        }
        protected void CheckItem(object sender, EventArgs e)
        {
            CheckItem();
        }

        #endregion

        #region Check Qty

        protected void CheckQty()
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;

            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                ddlStatus.Focus();
                return;
            }

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
            if (_masterItem != null)
            {

                //check for the location balance.
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());

                if (_inventoryLocation.Count == 1)
                {
                    foreach (InventoryLocation _loc in _inventoryLocation)
                    {
                        //SetItemBalance(_loc);
                        decimal _formQty = Convert.ToDecimal(txtQty.Text);
                        if (_formQty > _loc.Inl_free_qty)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please check the inventory balance!");
                            txtQty.Text = string.Empty;
                            txtQty.Focus();
                            return;
                        }
                    }
                }

            }

        }
        protected void CheckQty(object sender, EventArgs e)
        {
            CheckQty();
        }

        #endregion

        private void ClearItemDetail()
        {
            txtItem.Text = string.Empty;
            txtQty.Text = string.Empty;
            lblModel.Text = string.Empty;
            ddlStatus.Items.Clear();
        }
        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = GlbUserComCode;
            string _selectCompany = DDLRecCompany.SelectedValue.ToString();
            string _itemReceiveCompany = _master.Irsm_rec_com;
            string _comOutDocType = "NON";

            if (_userCompany == _selectCompany)
            {
                _comOutDocType = "AOD-OUT";
            }
            else if (_selectCompany == _itemReceiveCompany)
            {
                _comOutDocType = "PRN";
            }
            else if (_selectCompany != _itemReceiveCompany)
            {
                _comOutDocType = "DO";
            }

            return _comOutDocType;
        }
        public Int32 UserSeqNo
        {
            get { return Convert.ToInt32(Session["UserSeqNo"]); }
            set { Session["UserSeqNo"] = value; }
        }
        public Int32 SelectedItemLineNo
        {
            get { return Convert.ToInt32(Session["ReqLineNo"]); }
            set { Session["ReqLineNo"] = value; }
        }
        protected void AddItem(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DDLRecCompany.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receiving company");
                DDLRecCompany.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receiving location");
                txtDispatchRequried.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtItem.Focus();
                return;
            }

            //Load Item details
            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());



            if (string.IsNullOrEmpty(ddlStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                ddlStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty");
                txtQty.Focus();
                return;
            }

            //check for the location balance.
            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());

            decimal _formQty = Convert.ToDecimal(txtQty.Text);
            foreach (InventoryLocation _loc in _inventoryLocation)
            {
                if (_formQty > _loc.Inl_free_qty)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please check the inventory balance!");
                    txtQty.Focus();
                    return;
                }
            }


            if (ScanItemList != null)
                if (ScanItemList.Count > 0)
                {

                    var _duplicate = from _ls in ScanItemList
                                     where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
                                     select _ls;

                    if (_duplicate != null)
                        if (_duplicate.Count() > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item already available");
                            return;
                        }

                    var _maxline = (from _ls in ScanItemList
                                    select _ls.Itri_line_no).Max();

                    InventoryRequestItem _itm = new InventoryRequestItem();

                    _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                    _itm.Itri_itm_cd = txtItem.Text.Trim();
                    _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                    _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _masterItem.Mi_longdesc;
                    _itm.Mi_model = _masterItem.Mi_model;
                    _itm.Mi_brand = _masterItem.Mi_brand;

                    ScanItemList.Add(_itm);
                    gvItems.DataSource = ScanItemList;
                    gvItems.DataBind();
                }
                else
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                    _itm.Itri_itm_cd = txtItem.Text.Trim();
                    _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _masterItem.Mi_longdesc;
                    _itm.Mi_model = _masterItem.Mi_model;
                    _itm.Mi_brand = _masterItem.Mi_brand;

                    ScanItemList.Add(_itm);
                    gvItems.DataSource = ScanItemList;
                    gvItems.DataBind();
                }

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Added Successfully!");
            DDLRecCompany.Enabled = false;
            txtDispatchRequried.Enabled = false;
            ClearItemDetail();
        }

        protected void OnRemoveFromItemGrid(object sender, GridViewDeleteEventArgs e)
        {

            if (RequestNo != null && !string.IsNullOrEmpty(RequestNo))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not remove the item which allocated by request");
                return;
            }

            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gvItems.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gvItems.DataKeys[row_id][0];
            string _itmStatus = (string)gvItems.DataKeys[row_id][1];
            decimal _qty = (decimal)gvItems.DataKeys[row_id][2];


            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, COM_OUT);
            if (_list != null)
                if (_list.Count > 0)
                {
                    var _delete = (from _lst in _list
                                   where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus
                                   select _lst).ToList();

                    foreach (ReptPickSerials _ser in _delete)
                    {
                        string _items = _ser.Tus_itm_cd;
                        Int32 _serialID = _ser.Tus_ser_id;
                        string _bin = _ser.Tus_bin;
                        string serial_1 = _ser.Tus_ser_1;

                        _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

                        if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        {

                            CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, _serialID, 1);
                        }
                        else
                        {
                            CHNLSVC.Inventory.DeleteTempPickSerialByItem(GlbUserComCode, GlbUserDefLoca, UserSeqNo, _item, _itmStatus);

                        }
                    }
                }

            ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);

            if (ScanItemList != null)
                if (ScanItemList.Count > 0)
                {
                    gvItems.DataSource = ScanItemList;
                    gvItems.DataBind();
                }
                else
                    BindMrnDetail(RequestNo);
            BindPickSerials(UserSeqNo);

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }

        protected void chkDirect_CheckChange(object sender, EventArgs e)
        {
            UserSeqNo = 0;
            RequestNo = null;
            if (chkDirect.Checked)
            {
                txtRequest.Text = string.Empty;
                CPEPending.Collapsed = true;
                this.CPEPending.ClientState = "true";
                pnlPending.Enabled = false;
                txtDispatchRequried.Enabled = true;

            }
            else
            {
                pnlPending.Enabled = true;
                txtDispatchRequried.Enabled = false;
            }
            ScanItemList = new List<InventoryRequestItem>();
            BindMrnDetail(string.Empty);
            BindPickSerials(-1);
            ddlStatus.Items.Clear();
            BindReceiveCompany();
            txtDispatchRequried.Text = string.Empty;

        }

        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/InterCompanyOutWardEntry.aspx");
        }

        public List<ReptPickSerials> serial_list
        {
            get { return (List<ReptPickSerials>)ViewState["ReptPickSerials"]; }
            set { ViewState["ReptPickSerials"] = value; }
        }
        protected void GvItems_SelectedIndexChanged(object sender, EventArgs e)//pick button is a select link
        {
            GridViewRow gvr = gvItems.SelectedRow;
            Label ScannedDoQty = (Label)gvr.Cells[5].FindControl("lblPickQty");

            lblScanQty.Text = string.IsNullOrEmpty(ScannedDoQty.Text) ? "0" : ScannedDoQty.Text;
            lblInvoiceQty.Text = gvItems.SelectedRow.Cells[4].Text.ToString();
            string _itmStatus = gvItems.SelectedRow.Cells[3].Text.ToString();

            string longDiscript = gvItems.SelectedRow.Cells[1].Text.ToString();
            lblPopupItemCode.Text = gvItems.SelectedRow.Cells[0].Text.ToString();
            hdnInvoiceLineNo.Value = gvItems.SelectedDataKey[3].ToString();

            divPopupImg.Visible = false;
            lblpopupMsg.Text = string.Empty;

            if (lblPopupItemCode.Text != "&nbsp;")
            {
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                lblPopupBinCode.Visible = false;
                txtPopupQty.Visible = true;

                MasterItem msitem = new MasterItem();
                msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);

                if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item // (msitem.Mi_is_ser1 == false)
                {
                    serial_list = new List<ReptPickSerials>();
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }
                else if (msitem.Mi_is_ser1 == 1) //serial
                {
                    serial_list = new List<ReptPickSerials>();
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }
                else if (msitem.Mi_is_ser1 == -1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Non serial, decimal allow");
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }

            }

        }

        #region Scan Serial
        protected void btnPopupOk_Click(object sender, EventArgs e)
        {

            Int32 generated_seq = -1;

            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 userseq_no;
            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if (num_of_checked_itms > pending_amt)
            {
                divPopupImg.Visible = true;
                lblpopupMsg.Text = "Can't exceed Request Qty!";
                ModalPopupExtItem.Show();
                return;
            }
            Int32 user_seq_num = 0;
            if (UserSeqNo == 0)
                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, GlbUserComCode, RequestNo, 0);
            else
                user_seq_num = UserSeqNo;
            if (user_seq_num != -1)//check whether Tuh_doc_no exestst in temp_pick_hdr
            {

                generated_seq = user_seq_num;
                userseq_no = generated_seq;
                UserSeqNo = userseq_no;
            }
            else
            {

                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, COM_OUT, 1, GlbUserComCode);//direction always =1 for this method
                //assign user_seqno
                userseq_no = generated_seq;
                UserSeqNo = userseq_no;
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = COM_OUT;
                RPH.Tuh_cre_dt = DateTime.Today;
                RPH.Tuh_ischek_itmstus = false;
                RPH.Tuh_ischek_reqqty = false;
                RPH.Tuh_ischek_simitm = false;
                RPH.Tuh_session_id = GlbUserSessionID;
                RPH.Tuh_usr_com = GlbUserComCode;
                RPH.Tuh_usr_id = GlbUserName;
                RPH.Tuh_usrseq_no = generated_seq;
                RPH.Tuh_direct = false;
                RPH.Tuh_doc_no = RequestNo;
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            {
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {

                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_usrseq_no = generated_seq;

                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_base_doc_no = RequestNo;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = mi.Mi_model;

                        _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(_reptPickSerial_.Tus_ser_id);
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                        rowCount++;
                        //isManualscan = true;

                    }

                }
            }

            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = RequestNo;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;

                        _reptPickSerial_nonSer.Tus_new_remarks = DocumentTypeDecider(_reptPickSerial_nonSer.Tus_ser_id);
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        rowCount++;
                        //isManualscan = true;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                        //  binCD = _reptPickSerial_nonSer.Tus_bin;//the last items's bin will be assigned at last
                    }

                }
            }
            //------------------------------------------------------------------------------------------------------------------------------
            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);



                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;


                        Decimal pending_amt_ = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                        //only updated if the whole amount is finished.
                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        }


                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = RequestNo;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        //TODO: if the item IN document GRN ->PRN else DO
                        _reptPickSerial_nonSer.Tus_new_remarks = "DO";

                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        rowCount++;

                        actual_non_ser_List.Add(_reptPickSerial_nonSer);
                    }
                }
            }
            BindPickSerials(userseq_no);
            gvItems.DataSource = ScanItemList;
            gvItems.DataBind();
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();

            if (ddlPopupSerial.SelectedValue == "Serial 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupExtItem.Show();


            }
            else if (ddlPopupSerial.SelectedValue == "Serial 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupExtItem.Show();
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select serial type from drop down!");

            }

        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                Decimal availability = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - (Convert.ToDecimal(lblScanQty.Text.ToString()));
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Invoice Qty! Can add only " + availability + " itmes more.");
                return;
            }

            ModalPopupExtItem.Show();
        }

        protected void OnRemoveFromSerialGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gvSerial.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gvSerial.DataKeys[row_id][0];
            string _status = (string)gvSerial.DataKeys[row_id][1];
            Int32 _serialID = (Int32)gvSerial.DataKeys[row_id][4];
            string _bin = (string)gvSerial.DataKeys[row_id][5];
            string serial_1 = (string)gvSerial.DataKeys[row_id][3];

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(GlbUserComCode, GlbUserDefLoca, UserSeqNo, _item, _status);

            }
            ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - 1);

            BindPickSerials(UserSeqNo);
            if (ScanItemList != null)
                if (ScanItemList.Count > 0)
                {
                    gvItems.DataSource = ScanItemList;
                    gvItems.DataBind();
                }
                else
                    BindMrnDetail(RequestNo);
            BindPickSerials(UserSeqNo);

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }
        #endregion
        protected void Process(object sender, EventArgs e)
        {

            if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgDate, lblDispalyInfor) == false)
            {
                if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                    return;
                }
            }

            bool _isGRANfromDIN = false;
            bool _isGRAN = false;
            if (!string.IsNullOrEmpty(txtRequest.Text))
            {
                InventoryRequest _reqno = new InventoryRequest();
                _reqno.Itr_req_no = txtRequest.Text.Trim();
                InventoryRequest _din = CHNLSVC.Inventory.GetInventoryRequestData(_reqno);
                if (_din != null)
                    if (!string.IsNullOrEmpty(_din.Itr_com))
                    {
                        if (!string.IsNullOrEmpty(_din.Itr_anal1))
                            _isGRANfromDIN = true;
                        else
                            _isGRANfromDIN = false;

                        if (_din.Itr_tp == "GRAN")
                            _isGRAN = true;
                        else
                            _isGRAN = false;
                    }
            }



            if (!chkDirect.Checked)
                if (string.IsNullOrEmpty(txtRequest.Text) || txtRequest.Text == "N/A")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the direct check box or select request no");
                    return;
                }

            if (string.IsNullOrEmpty(DDLRecCompany.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receiving company");
                DDLRecCompany.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDispatchRequried.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receiving location");
                txtDispatchRequried.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the date");
                txtDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRequest.Text))
            {
                txtRequest.Text = "N/A";
            }

            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                txtRemarks.Text = "N/A";
            }

            if (string.IsNullOrEmpty(txtVehicle.Text))
            {
                txtVehicle.Text = "N/A";
            }

            if (gvItems.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                return;
            }

            if (gvSerial.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serials");
                return;
            }

            bool _isOk = IsAllScaned();
            if (_isOk == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Scan serial count and the serial are mismatch");
                return;
            }

            string _requestno = RequestNo;
            Int32 _userSeqNo = UserSeqNo;

            InvoiceHeader _invoiceheader = new InvoiceHeader();
            InventoryHeader _inventoryHeader = new InventoryHeader();
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();

            List<ReptPickSerials> _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, COM_OUT);
            List<ReptPickSerialsSub> _reptPickSerialsSub = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, COM_OUT);

            #region Check Duplicate Serials
            var _dup = _reptPickSerials.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

            string _duplicateItems = string.Empty;
            bool _isDuplicate = false;
            if (_dup != null)
                if (_dup.Count > 0)
                    foreach (Int32 _id in _dup)
                    {
                        Int32 _counts = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                        if (_counts > 1)
                        {
                            _isDuplicate = true;
                            var _item = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                            foreach (string _str in _item)
                                if (string.IsNullOrEmpty(_duplicateItems))
                                    _duplicateItems = _str;
                                else
                                    _duplicateItems += "," + _str;
                        }
                    }
            if (_isDuplicate)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                return;
            }
            #endregion

            #region Invoice Header Value Assign
            _invoiceheader.Sah_com = GlbUserComCode;
            _invoiceheader.Sah_cre_by = GlbUserName;
            _invoiceheader.Sah_cre_when = DateTime.Now;
            _invoiceheader.Sah_currency = "LKR";
            _invoiceheader.Sah_cus_add1 = string.Empty;
            _invoiceheader.Sah_cus_add2 = string.Empty;
            _invoiceheader.Sah_cus_cd = "CASH";
            _invoiceheader.Sah_cus_name = string.Empty;
            _invoiceheader.Sah_d_cust_add1 = string.Empty;
            _invoiceheader.Sah_d_cust_add2 = string.Empty;
            _invoiceheader.Sah_d_cust_cd = "CASH";
            _invoiceheader.Sah_direct = true;
            _invoiceheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invoiceheader.Sah_epf_rt = 0;
            _invoiceheader.Sah_esd_rt = 0;
            _invoiceheader.Sah_ex_rt = 1;
            _invoiceheader.Sah_inv_no = "NA";
            _invoiceheader.Sah_inv_sub_tp = "SA"; //(Old Value - CS)Change value as per the dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_inv_tp = "INTR"; //(Old Value - CRED)Change value as per the dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_is_acc_upload = false;
            _invoiceheader.Sah_man_cd = string.Empty;
            _invoiceheader.Sah_man_ref = string.Empty;
            _invoiceheader.Sah_manual = false;
            _invoiceheader.Sah_mod_by = GlbUserName;
            _invoiceheader.Sah_mod_when = DateTime.Now;
            _invoiceheader.Sah_pc = GlbUserDefProf;
            _invoiceheader.Sah_pdi_req = 0;
            _invoiceheader.Sah_ref_doc = string.Empty;
            _invoiceheader.Sah_remarks = string.Empty;
            _invoiceheader.Sah_sales_chn_cd = string.Empty;
            _invoiceheader.Sah_sales_chn_man = string.Empty;
            _invoiceheader.Sah_sales_ex_cd = string.Empty;
            _invoiceheader.Sah_sales_region_cd = string.Empty;
            _invoiceheader.Sah_sales_region_man = string.Empty;
            _invoiceheader.Sah_sales_sbu_cd = string.Empty;
            _invoiceheader.Sah_sales_sbu_man = string.Empty;
            _invoiceheader.Sah_sales_str_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_cd = string.Empty;
            _invoiceheader.Sah_sales_zone_man = string.Empty;
            _invoiceheader.Sah_seq_no = 1;
            _invoiceheader.Sah_session_id = GlbUserSessionID;
            _invoiceheader.Sah_structure_seq = string.Empty;
            _invoiceheader.Sah_stus = "D";
            _invoiceheader.Sah_town_cd = string.Empty;
            _invoiceheader.Sah_tp = "INV";
            _invoiceheader.Sah_wht_rt = 0;
            #endregion

            #region Invoice AutoNumber

            _invoiceAuto.Aut_cate_cd = GlbUserDefProf;
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = null;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CRED";
            _invoiceAuto.Aut_start_char = "CRED";
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            #region Inventory AutoNumber

            _inventoryAuto.Aut_cate_cd = GlbUserDefLoca;
            _inventoryAuto.Aut_cate_tp = "LOC";
            _inventoryAuto.Aut_direction = null;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_moduleid = string.Empty;
            _inventoryAuto.Aut_start_char = string.Empty;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            #region Inventory Header Value Assign

            _inventoryHeader.Ith_acc_no = string.Empty;
            _inventoryHeader.Ith_anal_1 = string.Empty;
            _inventoryHeader.Ith_anal_10 = string.IsNullOrEmpty(_requestno) ? true : false;//Direct AOD
            _inventoryHeader.Ith_anal_11 = false;
            _inventoryHeader.Ith_anal_12 = false;
            _inventoryHeader.Ith_anal_2 = string.Empty;
            _inventoryHeader.Ith_anal_3 = string.Empty;
            _inventoryHeader.Ith_anal_4 = string.Empty;
            _inventoryHeader.Ith_anal_5 = string.Empty;
            _inventoryHeader.Ith_anal_6 = 0;
            _inventoryHeader.Ith_anal_7 = 0;
            _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_bus_entity = string.Empty;
            _inventoryHeader.Ith_cate_tp = string.Empty;
            _inventoryHeader.Ith_channel = string.Empty;
            _inventoryHeader.Ith_com = GlbUserComCode;
            _inventoryHeader.Ith_com_docno = string.Empty;
            _inventoryHeader.Ith_cre_by = GlbUserName;
            _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
            _inventoryHeader.Ith_del_add1 = string.Empty;
            _inventoryHeader.Ith_del_add2 = string.Empty;
            _inventoryHeader.Ith_del_code = string.Empty;
            _inventoryHeader.Ith_del_party = string.Empty;
            _inventoryHeader.Ith_del_town = string.Empty;
            _inventoryHeader.Ith_direct = false;
            _inventoryHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
            _inventoryHeader.Ith_doc_no = string.Empty;
            _inventoryHeader.Ith_doc_tp = string.Empty;
            _inventoryHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Date.Year;
            _inventoryHeader.Ith_entry_no = string.Empty;
            _inventoryHeader.Ith_entry_tp = string.Empty;
            _inventoryHeader.Ith_git_close = false;
            _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_git_close_doc = string.Empty;
            _inventoryHeader.Ith_is_manual = false;
            _inventoryHeader.Ith_isprinted = false;
            _inventoryHeader.Ith_job_no = string.Empty;
            _inventoryHeader.Ith_loading_point = string.Empty;
            _inventoryHeader.Ith_loading_user = string.Empty;
            _inventoryHeader.Ith_loc = GlbUserDefLoca;
            _inventoryHeader.Ith_manual_ref = _requestno;
            _inventoryHeader.Ith_mod_by = GlbUserName;
            _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryHeader.Ith_noofcopies = 0;
            _inventoryHeader.Ith_oth_loc = txtDispatchRequried.Text.Trim();
            _inventoryHeader.Ith_remarks = string.Empty;
            _inventoryHeader.Ith_sbu = string.Empty;
            _inventoryHeader.Ith_seq_no = 0;
            _inventoryHeader.Ith_session_id = GlbUserSessionID;
            _inventoryHeader.Ith_stus = "A";
            _inventoryHeader.Ith_sub_tp = string.Empty;
            _inventoryHeader.Ith_vehi_no = string.Empty;
            _inventoryHeader.Ith_oth_com = DDLRecCompany.SelectedValue.ToString();
            #endregion

            string _message = string.Empty;
            string _genInventoryDoc = string.Empty;
            string _genSalesDoc = string.Empty;

            //Process
            Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(GlbUserComCode, GlbUserDefProf, DDLRecCompany.SelectedValue.ToString(), _requestno, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, _reptPickSerialsSub, out _message, out _genSalesDoc, out _genInventoryDoc, _isGRAN, _isGRANfromDIN);
            string Msg = string.Empty;
            if (!string.IsNullOrEmpty(_genInventoryDoc)) _genInventoryDoc.Trim().Remove(_genInventoryDoc.Length - 1);
            if (!string.IsNullOrEmpty(_genSalesDoc)) _genSalesDoc.Trim().Remove(_genSalesDoc.Length - 1);


            if (_effect == -1)
                Msg = "alert('" + _message + "'); ";
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly processed!. Document No(s) - " + _genInventoryDoc + ", " + _genSalesDoc);
                Msg = "alert('Successfuly processed!. Document No(s) :" + _genInventoryDoc;
                if (!string.IsNullOrEmpty(_genSalesDoc)) Msg += "," + _genSalesDoc + " ";
                Msg += "');";
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CommonOutMsg", Msg, true);
            if (_effect != -1)
            {
                GlbDocNosList = _genInventoryDoc;
                GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";

                GlbMainPage = "~/Inventory_Module/InterCompanyOutWardEntry.aspx";

                Msg = "window.open('../Reports_Module/Inv_Rep/Print.aspx',  '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CommonOutPrint", Msg, true);
            }
        }

        //HWCHANGE
        private bool IsAllScaned()
        {
            bool _isok = false;
            List<ReptPickSerials> _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, COM_OUT);
            if (ScanItemList != null && _list != null)
            {
                foreach (GridViewRow _itm in gvItems.Rows)
                {
                    string _item = _itm.Cells[0].Text;
                    Label _lbl = (Label)_itm.FindControl("lblPickQty");
                    decimal _scanQty = Convert.ToDecimal(_lbl.Text);
                    var _serialcount = (from _l in _list
                                        where _l.Tus_itm_cd == _item
                                        select _l.Tus_qty).Sum();

                    if (_scanQty != _serialcount)
                    {
                        _isok = false;
                        break;
                    }
                    else
                        _isok = true;
                }
            }


            return _isok;
        }

        protected void CheckLocation(object sender, EventArgs e)
        {
            string _defualloc = GlbUserDefLoca;
            string _selectedLoc = txtDispatchRequried.Text.Trim();

            if (DDLRecCompany.SelectedValue.ToString() == GlbUserComCode)
            {
                if (_defualloc.Trim() == _selectedLoc.Trim())
                {
                    txtDispatchRequried.Text = string.Empty;
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not out to the same location");
                    return;
                }
            }

        }

    }
}