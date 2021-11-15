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
    public partial class InterCompanyInWardEntry : BasePage
    {

        protected static MasterItem _masterItem = null;
        private static List<InventoryLocation> _inventoryLocation = null;


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

        #region Sessions Variables
        public string OutwardNo
        {
            get { return Session["OutwardNo"].ToString(); }
            set { Session["OutwardNo"] = value; }
        }

        public string OutwardType
        {
            get { return Session["OutwardType"].ToString(); }
            set { Session["OutwardType"] = value; }
        }

        public string OutwardCompany
        {
            get { return Session["OutwardCompany"].ToString(); }
            set { Session["OutwardCompany"] = value; }
        }

        public string OutwardLocation
        {
            get { return Session["OutwardLocation"].ToString(); }
            set { Session["OutwardLocation"] = value; }
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

        public List<ReptPickSerials> PickSerialsList
        { 
            get { return (List<ReptPickSerials>)ViewState["ScanItemSerList"]; }
            set { ViewState["ScanItemSerList"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OutwardType = string.Empty;
                //Check location has maintain bin locations
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(GlbUserComCode, GlbUserDefLoca);
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_allow_bin == false)
                    {
                        hdnAllowBin.Value = "0";
                    }
                    else
                    {
                        hdnAllowBin.Value = "1";
                    }
                }
                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                if (!string.IsNullOrEmpty(_defBin))
                {
                    hdnAllowBin.Value = _defBin;
                }
                else
                {
                    string Msg = "<script>alert('Default Bin Not Setup For Location : " + GlbUserDefLoca + "');window.location = '../Default.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);                    
                    return;
                }


                BindRequestTypesDDLData(); //Load pending outward entries
                BindBinCode();
                BindOutwardListGridData();
                //BindPickSerials();

                //For serial search
                txtSerial1.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSerial1.ClientID + "')");
                //For item search
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");

            }
            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            //if (CHNLSVC.General.IsAllowBackDate(GlbUserComCode, GlbUserDefLoca, string.Empty, string.Empty) == true)
            //{
            //    imgDate.Visible = true;
            //}

            //Check back date
            IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgDate, lblDispalyInfor);


            //txtSerial1.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSerial1, ""));
            //txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
            //txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));

        }

        #region  Data Bind

        private void BindRequestTypesDDLData()
        {
            ddlType.Items.Clear();
            List<MasterType> _masterType = CHNLSVC.General.GetOutwardTypes();
            _masterType.Add(new MasterType { Mtp_cd = "-1", Mtp_desc = "" });
            ddlType.DataSource = _masterType.OrderBy(items => items.Mtp_desc);
            ddlType.DataTextField = "Mtp_desc";
            ddlType.DataValueField = "Mtp_cd";
            ddlType.DataBind();
        }

        protected void BindBinCode()
        {
            List<string> bincode_list = new List<string>();

            bincode_list = CHNLSVC.Inventory.GetAll_binCodes_for_loc(GlbUserComCode, GlbUserDefLoca);
            bincode_list.Add("");
            bincode_list.RemoveAt(0);
            DDLBinCode.DataSource = bincode_list.OrderBy(Items => Items);
            DDLBinCode.DataBind();
        }

        //Bind pending outward documents
        private void BindOutwardListGridData()
        {
            InventoryHeader _inventoryRequest = new InventoryHeader();
            _inventoryRequest.Ith_oth_com = GlbUserComCode;
            _inventoryRequest.Ith_doc_tp = ddlType.SelectedValue.ToString() == "-1" ? string.Empty : ddlType.SelectedValue.ToString();
            _inventoryRequest.Ith_oth_loc = GlbUserDefLoca;
            _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
            _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
            if (!string.IsNullOrEmpty(_inventoryRequest.Ith_doc_tp))
            {
                if (_inventoryRequest.Ith_doc_tp == "AOD-") _inventoryRequest.Ith_doc_tp = "AOD";
            }
            DataTable _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);

            if (_table.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _table.AsEnumerable()
                   group dr by new { docno = dr["ith_doc_no"], docdate = dr["ith_doc_date"], doctype = dr["ith_doc_tp"], manualref = dr["ith_manual_ref"], com = dr["ith_com"], loc = dr["ith_loc"] } into item
                   select new
                   {
                       docno = item.Key.docno,
                       docdate = item.Key.docdate,
                       doctype = item.Key.doctype,
                       manualref = item.Key.manualref,
                       //itemqty = item.Sum(p => p.Field<double>("Tus_qty"))
                       com = item.Key.com,
                       loc = item.Key.loc
                   };


                //_table = SetEmptyRow(_table);
                //gvPending.DataSource = _table;
                gvPending.DataSource = _tblItems;
            }
            else
            {
                gvPending.DataSource = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
            }
            gvPending.DataBind();

            //Bind item grid
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            _table = new DataTable();
            _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, OutwardType);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvSerial.DataSource = _table;

                var _tblItems =
                    from dr in _table.AsEnumerable()
                    group dr by new { itemcode = dr["Tus_itm_cd"], itemdesc = dr["Tus_itm_desc"], itemmodel = dr["Tus_itm_model"], itemstatus = dr["Tus_itm_stus"] } into item
                    select new
                    {
                        itemcode = item.Key.itemcode,
                        itemdesc = item.Key.itemdesc,
                        itemmodel = item.Key.itemmodel,
                        itemstatus = item.Key.itemstatus,
                        //itemqty = item.Sum(p => p.Field<double>("Tus_qty"))
                        itemqty = item.Sum(p => 0)
                    };
                gvItem.DataSource = _tblItems;
            }

            gvSerial.DataBind();
            gvItem.DataBind();
        }

        protected void BindPickSerials()
        {
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, OutwardType);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvSerial.DataSource = _table;
                gvItem.DataSource = _table;
            }
            else
            {
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, OutwardType);
                PickSerialsList = _list; 
                gvSerial.DataSource = _list;
                //chamal
                var _tblItems =
                    from _pickSerials in _list
                    group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item
                    select new { itemcode = item.Key.Tus_itm_cd, itemdesc = item.Key.Tus_itm_desc, itemmodel = item.Key.Tus_itm_model, itemstatus = item.Key.Tus_itm_stus, itemqty = item.Sum(p => p.Tus_qty) };
                
                gvItem.DataSource = _tblItems;


 
            }
            gvSerial.DataBind();
            gvItem.DataBind();
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

        protected void BindSelectedOutwardNo(Object sender, EventArgs e)
        {
            OutwardNo = gvPending.SelectedDataKey[0].ToString();
            hdnOutwarddate.Value = Convert.ToDateTime(gvPending.SelectedDataKey[1].ToString()).Date.ToString("dd/MMM/yyyy");
            OutwardType = gvPending.SelectedDataKey[2].ToString();
            txtRequest.Text = OutwardNo;
            txtIssueCom.Text = gvPending.SelectedDataKey[3].ToString();
            txtIssueLoca.Text = gvPending.SelectedDataKey[4].ToString();
            BindOutwardItems();
        }

        protected void BindOutwardItems()
        {
            PickSerialsList = null;
            ReptPickHeader _reptPickHdr = new ReptPickHeader();

            Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, OutwardNo);

            UserSeqNo = _seq;

            _reptPickHdr.Tuh_direct = true;
            _reptPickHdr.Tuh_doc_no = OutwardNo; //Outward Doc No
            _reptPickHdr.Tuh_doc_tp = OutwardType; //Doc Type
            _reptPickHdr.Tuh_ischek_itmstus = false;
            _reptPickHdr.Tuh_ischek_reqqty = true;
            _reptPickHdr.Tuh_ischek_simitm = false;
            _reptPickHdr.Tuh_session_id = GlbUserSessionID;//Session ID
            _reptPickHdr.Tuh_usr_com = GlbUserComCode; //Company
            _reptPickHdr.Tuh_usr_id = GlbUserName; //User Name
            _reptPickHdr.Tuh_usrseq_no = _seq;

            List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(GlbUserDefLoca, hdnAllowBin.Value, _reptPickHdr);

            if (PickSerials != null)
            {
                var _tblItems =
                    from _pickSerials in PickSerials
                    group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item
                    select new { itemcode = item.Key.Tus_itm_cd, itemdesc = item.Key.Tus_itm_desc, itemmodel = item.Key.Tus_itm_model, itemstatus = item.Key.Tus_itm_stus, itemqty = item.Sum(p => p.Tus_qty) };

                gvItem.DataSource = _tblItems;
                gvItem.DataBind();

                gvSerial.DataSource = PickSerials;
                gvSerial.DataBind();

                PickSerialsList = PickSerials;
            }


        }

        protected void OnSelectedItemBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                            gvItem,
                            String.Concat("Select$", e.Row.RowIndex),
                            true);
                _count += 1;
            }
        }

        protected void BindSelectedItemToText(Object sender, EventArgs e)
        {
            //txtItem.Text = gvItem.SelectedDataKey[0].ToString();
            //SelectedItemLineNo = Convert.ToInt32(gvItem.SelectedDataKey[3].ToString());
            //CheckItem();
        }

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
                        paramsText.Append(GlbUserComCode + seperator);
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
        protected void imgBtnSerial_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerial);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSerial1.ClientID;
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

            BindOutwardListGridData();
        }

        #region not in use
        #region Check Serial 1

        protected void CheckSerial()
        {
            if (string.IsNullOrEmpty(txtSerial1.Text)) return;

            //check direction
            //if direction - out, get the serial details from the system.
            //if direction - in, check the serial availability.

            //check serial availability

            List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLBinCode.SelectedValue.ToString(), txtSerial1.Text.Trim(), txtSerial2.Text.Trim());

            if (_list != null || _list.Count > 0)
            {

                Int32 _noOfLines = _list.Count;

                //If only one line = means serial having single items
                if (_noOfLines == 1)
                {
                    ReptPickSerials _List = new ReptPickSerials();
                    foreach (ReptPickSerials _pick in _list)
                    {
                        _List = _pick;
                    }

                    DDLBinCode.Items.Clear();
                    ddlStatus.Items.Clear();

                    DDLBinCode.Items.Add(_List.Tus_bin);
                    ddlStatus.Items.Add(_List.Tus_itm_stus);
                    if (string.IsNullOrEmpty(txtItem.Text)) txtItem.Text = _List.Tus_itm_cd;
                    txtSerial2.Text = _List.Tus_ser_2;
                    txtSerial3.Text = _List.Tus_ser_3;
                    _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                    txtQty.Text = "1";
                }
                //if more than one line = means serial having more than item
                if (_noOfLines > 1)
                {
                    //Load to the common modal
                }

            }
        }
        protected void CheckSerial1(object sender, EventArgs e)
        {
            CheckSerial();
        }

        #endregion

        #region Check Item

        protected void CheckItem()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;

            if (string.IsNullOrEmpty(DDLBinCode.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin!");
                DDLBinCode.Focus();
                return;
            }

            _masterItem = new MasterItem();

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);
            if (_masterItem.Mi_is_ser1 == 1)
            {
                txtQty.Text = "1";
                txtSerial1.Text = "";
                if (_masterItem.Mi_is_ser2) txtSerial2.Text = "";
                if (_masterItem.Mi_is_ser3) txtSerial3.Text = "";
            }

            if (_masterItem.Mi_is_ser1 == 0 || _masterItem.Mi_is_ser1 == -1)
            {
                txtQty.Text = "";
                txtSerial1.Text = "N/A";
                if (_masterItem.Mi_is_ser2 == false) txtSerial2.Text = "N/A";
                if (_masterItem.Mi_is_ser3 == false) txtSerial3.Text = "N/A";
            }

            if (!string.IsNullOrEmpty(DDLBinCode.SelectedValue.ToString()))
                BindItemStatus(GlbUserComCode, GlbUserDefLoca, DDLBinCode.SelectedValue.ToString(), txtItem.Text, string.Empty);
            else
                BindItemStatus(GlbUserComCode, GlbUserDefLoca, "", txtItem.Text, "");

            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
            if (_inventoryLocation == null)
            {
                // divFree.InnerText = " "; divReserved.InnerText = " "; 
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No stock balance available");
                //txtItem.Focus();
                return;
            }
            if (_inventoryLocation.Count == 1)
            {
                foreach (InventoryLocation _loc in _inventoryLocation)
                {
                    //SetItemBalance(_loc);
                }

            }
            else
            {
                //divFree.InnerText = "";
                //divReserved.InnerText = "";
            }
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

            if (DDLBinCode.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin");
                DDLBinCode.Focus();
                return;
            }

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
                if (_masterItem.Mi_is_ser1 == 1)
                {

                    decimal _formQty = Convert.ToDecimal(txtQty.Text);
                    if (_formQty > 1)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Serialized item can have only one qty at a time");
                    }
                    txtQty.Text = "1";
                }
                else if (_masterItem.Mi_is_ser1 == 0 || _masterItem.Mi_is_ser1 == -1)
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
                                txtQty.Focus();
                                return;
                            }
                        }

                    }
                    else
                    {
                        //divFree.InnerText = "";
                        //divReserved.InnerText = "";
                    }

                }

            }

        }
        protected void CheckQty(object sender, EventArgs e)
        {
            CheckQty();
        }

        #endregion
        #endregion

        private void ClearItemDetail(bool _isSerialized)
        {
            if (_isSerialized == false)
            {
                txtItem.Text = "";
                //divSerialized.Style.Add("background-color", "White");
                //divSSerialized.Style.Add("background-color", "White");
                //divWarranty.Style.Add("background-color", "White");

                //divFree.InnerText = "";
                //divReserved.InnerText = "";
                txtQty.Text = "";

                //lblBrand.Text = "";
                //lblItmDescription.Text = "";
                //lblModel.Text = "";
            }

            txtSerial1.Text = "";
            txtSerial2.Text = "";
            txtSerial3.Text = "";
            //txtMFC.Text = "N/A";

        }
        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = GlbUserComCode;
            string _itemReceiveCompany = _master.Irsm_rec_com;
            string _comOutDocType = "NON";
            return _comOutDocType;
        }


        #region Add Item
        protected void AddItem(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter the remarks");
            //    txtRemarks.Focus();
            //    return;
            //}


            //if (string.IsNullOrEmpty(DDLBinCode.SelectedValue.ToString()))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin");
            //    DDLBinCode.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
            //    txtItem.Focus();
            //    return;
            //}

            ////Load Item details
            //_masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());

            //if (string.IsNullOrEmpty(ddlStatus.SelectedValue.ToString()))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
            //    ddlStatus.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtSerial1.Text.ToString()) && _masterItem.Mi_is_ser1 == 1)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serial");
            //    txtSerial1.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtSerial2.Text.ToString()) && _masterItem.Mi_is_ser1 == 1 && _masterItem.Mi_is_ser2 == true)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the seria2");
            //    txtSerial2.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtSerial3.Text.ToString()) && _masterItem.Mi_is_ser1 == 1 && _masterItem.Mi_is_ser3 == true)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the seria3");
            //    txtSerial3.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtQty.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty");
            //    txtQty.Focus();
            //    return;
            //}

            ////check for the location balance.
            //_inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());

            //decimal _formQty = Convert.ToDecimal(txtQty.Text);
            //foreach (InventoryLocation _loc in _inventoryLocation)
            //{
            //    if (_formQty > _loc.Inl_free_qty)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please check the inventory balance!");
            //        txtQty.Focus();
            //        return;
            //    }
            //}


            ////check for already scan detail
            //int _userSeqNo = 0;
            //_userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(GlbUserComCode, OutwardNo);
            //if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table. 
            //{
            //    _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, OutwardType, 1, GlbUserComCode);

            //    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

            //    _inputReptPickHeader.Tuh_direct = true;
            //    _inputReptPickHeader.Tuh_doc_no = OutwardNo;
            //    _inputReptPickHeader.Tuh_doc_tp = OutwardType;
            //    _inputReptPickHeader.Tuh_ischek_itmstus = false;
            //    _inputReptPickHeader.Tuh_ischek_reqqty = false;
            //    _inputReptPickHeader.Tuh_ischek_simitm = false;
            //    _inputReptPickHeader.Tuh_session_id = GlbUserSessionID;
            //    _inputReptPickHeader.Tuh_usr_com = GlbUserComCode;
            //    _inputReptPickHeader.Tuh_usr_id = GlbUserName;
            //    _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

            //    //Save it to the scmrep.temp_pick_hdr header table. 
            //    Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

            //}
            ////Set Session
            //UserSeqNo = _userSeqNo;

            ////If serial itm
            //if (_masterItem.Mi_is_ser1 == 1)
            //{
            //    ReptPickSerials _list = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, DDLBinCode.SelectedValue.ToString(), txtItem.Text.Trim(), txtSerial1.Text.Trim());
            //    if (_list != null)
            //    {
            //        _list.Tus_cre_by = GlbUserName;
            //        _list.Tus_usrseq_no = UserSeqNo;
            //        _list.Tus_new_status = DocumentTypeDecider(_list.Tus_ser_id);
            //        _list.Tus_base_doc_no = OutwardNo;
            //        _list.Tus_base_itm_line = SelectedItemLineNo;
            //        _list.Tus_itm_desc = _masterItem.Mi_longdesc;
            //        _list.Tus_itm_model = _masterItem.Mi_model;
            //        _list.Tus_itm_brand = _masterItem.Mi_brand;


            //        CHNLSVC.Inventory.SaveAllScanSerials(_list, null);
            //        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToInt32(_list.Tus_serial_id), -1);
            //    }
            //    else
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The item/serial already scaned or incorrect information");
            //        return;
            //    }

            //    BindPickSerials();
            //    ClearItemDetail(true);
            //    return;

            //}

            ////If non serial itm, but serial id have
            //if (_masterItem.Mi_is_ser1 == 0)
            //{
            //    //TODO: Load top most serial_id from serial table to scan table
            //    List<ReptPickSerials> _list = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, DDLBinCode.SelectedValue.ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text));
            //    if (_list != null || _list.Count > 0)
            //    {
            //        foreach (ReptPickSerials _single in _list)
            //        {

            //            _single.Tus_cre_by = GlbUserName;
            //            _single.Tus_usrseq_no = UserSeqNo;
            //            _single.Tus_new_status = DocumentTypeDecider(_single.Tus_ser_id);
            //            _single.Tus_base_doc_no = OutwardNo;
            //            _single.Tus_base_itm_line = SelectedItemLineNo;
            //            _single.Tus_itm_desc = _masterItem.Mi_longdesc;
            //            _single.Tus_itm_model = _masterItem.Mi_model;
            //            _single.Tus_itm_brand = _masterItem.Mi_brand;

            //            CHNLSVC.Inventory.SaveAllScanSerials(_single, null);
            //            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToInt32(_single.Tus_serial_id), -1);
            //        }
            //        BindPickSerials();
            //        ClearItemDetail(false);
            //        return;
            //    }
            //    else
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The item already scaned or incorrect information");
            //        return;
            //    }
            //}

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Added Successfully!");

            //Decimal Items

            ////if non serial itm,no serial id = -1
            //if (_masterItem.Mi_is_ser1 == -1)
            //{
            //    //Add directly to the scan  table
            //    CHNLSVC.Inventory.SaveAllScanSerials(AddItemToSerialList(-1), null);
            //    BindPickSerials();
            //    ClearItemDetail(false);
            //    return;
            //}

        }
        #endregion

        protected void OnRemoveFromSerialGrid(object sender, GridViewDeleteEventArgs e)
        {
            Int32 _serialID = -1;
            if (OutwardNo == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "(R)Select the outward document!");
                return;
            }

            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gvSerial.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gvSerial.DataKeys[row_id][0];
            _serialID = (Int32)gvSerial.DataKeys[row_id][4];
            string _bin = (string)gvSerial.DataKeys[row_id][5];

            if (_serialID == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can not remove none-serialized items!");
                return;
            }

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

            //if (_masterItem.Mi_is_ser1 == 1)
            //{
            //    CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
            //}
            //else
            //{
            //    CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID), _item, _bin);
            //}

            CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));

            BindPickSerials();
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }
        protected void OnRemoveFromItemGrid(object sender, GridViewDeleteEventArgs e)
        {
            if (OutwardNo == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not remove the item which allocated by request");
                return;
            }

            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gvItem.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gvItem.DataKeys[row_id][0];
            string _itmStatus = (string)gvItem.DataKeys[row_id][1];

            CHNLSVC.Inventory.DeleteTempPickSerialByItem(GlbUserComCode, GlbUserDefLoca, UserSeqNo, _item, _itmStatus);

            BindPickSerials();

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }

        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        #region Save Process
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GlbUserComCode))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Session expired! Please re-login to system.");
                txtDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the date");
                txtDate.Focus();
                return;
            }

            if (IsDate(txtDate.Text, DateTimeStyles.None) == false)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invald Date.");
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

            if (DateTime.Compare(Convert.ToDateTime(hdnOutwarddate.Value.ToString()).Date, Convert.ToDateTime(txtDate.Text).Date) > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inward entry date should be greater than or equal to outward entry date.");
                return;
            }

            if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgDate, lblDispalyInfor) == false)
            {
                if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                    return;
                }
            }

            //Process
            InventoryHeader invHdr = new InventoryHeader();

            invHdr.Ith_loc = GlbUserDefLoca;
            invHdr.Ith_com = GlbUserComCode;
            invHdr.Ith_oth_docno = OutwardNo;
            invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
            invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
            if (OutwardType == "AOD")
            {
                invHdr.Ith_doc_tp = "AOD";
                invHdr.Ith_cate_tp = "NOR";
            }
            else if (OutwardType == "DO")
            {
                invHdr.Ith_doc_tp = "GRN";
                invHdr.Ith_cate_tp = "LOCAL";
            }
            else if (OutwardType == "PRN")
            {
                invHdr.Ith_doc_tp = "SRN";
                invHdr.Ith_cate_tp = "NOR";
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Critical, "New outward document type found!");
                return;
            }
            //invHdr.Ith_oth_com =
            invHdr.Ith_is_manual = false;
            invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = GlbUserName;
            invHdr.Ith_mod_by = GlbUserName;
            invHdr.Ith_direct = true;
            invHdr.Ith_session_id = GlbUserSessionID;
            invHdr.Ith_manual_ref = "N/A";
            invHdr.Ith_remarks = txtRemarks.Text;
            invHdr.Ith_vehi_no = txtVehicle.Text;
            invHdr.Ith_sub_tp = "NOR";
            invHdr.Ith_oth_com = txtIssueCom.Text;
            invHdr.Ith_oth_loc = txtIssueLoca.Text;

            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, GlbUserComCode, OutwardNo, 1);
            //List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            //reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seq_num, OutwardType);

            if (PickSerialsList == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Critical, "Please select outward document!");
                return;
            }

            btnSave.Enabled = false;

            List<ReptPickSerialsSub> reptPickSerials_SubList = new List<ReptPickSerialsSub>();
            reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, OutwardType);

            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
            masterAutoNum.Aut_cate_cd = GlbUserDefLoca;
            masterAutoNum.Aut_cate_tp = "LOC";
            masterAutoNum.Aut_direction = 1;
            masterAutoNum.Aut_modify_dt = null;
            masterAutoNum.Aut_year = DateTime.Now.Year;//Convert.ToDateTime(txtDate.Text).Year;

            string documntNo = string.Empty;
            Int32 result = -99;
            if (OutwardType == "AOD")
            {
                masterAutoNum.Aut_moduleid = "AOD";
                masterAutoNum.Aut_start_char = "AOD";
                result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
            }
            else if (OutwardType == "DO")
            {
                masterAutoNum.Aut_moduleid = "GRN";
                masterAutoNum.Aut_start_char = "GRN";
                result = CHNLSVC.Inventory.GRN(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
            }
            else if (OutwardType == "PRN")
            {
                masterAutoNum.Aut_moduleid = "PRN";
                masterAutoNum.Aut_start_char = "PRN";
                result = CHNLSVC.Inventory.SRN(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Critical, "Invalid outward document type found!");
                return;
            }


            if (result != -99 && result > 0)
            {
                string Msg = "<script>alert('AOD Receipt Successfully Saved! Document No. : " + documntNo + "');window.location = 'InterCompanyInWardEntry.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                //GlbDocNosList = documntNo;
                //GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Inward_Doc.rpt";
                //GlbReportMapPath = "~/Reports_Module/Inv_Rep/Inward_Doc.rpt";

                //GlbMainPage = "~/Inventory_Module/InterCompanyInWardEntry.aspx";
                //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");
            }
            else
            {
                string Msg = "<script>alert('Sorry, not Saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            btnSave.Enabled = true;
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly processed!");
        }
        #endregion

        protected void SearchRequest(object sender, ImageClickEventArgs e)
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

            BindOutwardListGridData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/InterCompanyInWardEntry.aspx");
        }
    }
}