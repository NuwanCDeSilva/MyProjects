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
    public partial class ItemScan : BasePage
    {

        MasterItem _masterItem = null;
        List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
        ReptPickHeader _reptPickHeader = new ReptPickHeader();
        List<ReptPickItems> _reptPickItems = new List<ReptPickItems>();
        List<InventoryLocation> _inventoryLocation = new List<InventoryLocation>();


        private void Ad_hoc_sessions()
        {
            //Session["UserID"] = "ADMIN";
            //Session["UserCompanyCode"] = "ABL";
            //Session["SessionID"] = "666";
            //Session["UserDefLoca"] = "MSR01";
            //Session["UserDefProf"] = "39";

            //GlbSerialScanDirection = 1;
            //GlbSerialScanDocumentType = "ADJ-C";
            //GlbSerialScanUserSeqNo = 1519;
            //GlbSerialBusinessEntity = "ABDRA1002";
            //GlbSerialScanRequestNo = "Test1";
        }

        private void DisplayPickHeader()
        {
            lblDate.Text = _reptPickHeader.Tuh_cre_dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string _type = "";
            if (GlbSerialScanDirection.ToString() == "1")
            { _type = "+"; }
            else
            { _type = "-"; }
            lblDocType.Text = GlbSerialScanDocumentType.ToString() + _type;
            lblSeqNo.Text = GlbSerialScanUserSeqNo.ToString();
            lblUser.Text = _reptPickHeader.Tuh_usr_id;

            lblAllowAnyItem.Text = "No";
            lblAllowAnyQty.Text = _reptPickHeader.Tuh_ischek_reqqty ? "No" : "Yes";
            lblAllowModelChange.Text = _reptPickHeader.Tuh_ischek_simitm ? "Yes" : "No";
            lblAllowStatusChange.Text = _reptPickHeader.Tuh_ischek_itmstus ? "No" : "Yes";

        }

        protected void BindRequestDetail()
        {
            DataTable _table = CHNLSVC.Inventory.GetAllScanRequestItems(GlbSerialScanUserSeqNo);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvRequest.DataSource = _table;
            }
            else
            {
                _reptPickItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(GlbSerialScanUserSeqNo);
                gvRequest.DataSource = _reptPickItems;
            }
            gvRequest.DataBind();
        }

        protected void BindPickSerials()
        {

            DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbSerialScanUserSeqNo, GlbSerialScanDocumentType);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvMainSerial.DataSource = _table;
            }
            else
            {
                _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbSerialScanUserSeqNo, GlbSerialScanDocumentType);
                gvMainSerial.DataSource = _reptPickSerials;
            }
            gvMainSerial.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Ad_hoc_sessions();
            if (GlbSerialScanUserSeqNo == 0) return;



            if (!IsPostBack)
            {
                if (GlbSerialScanUserSeqNo != 0)
                {   //TODO: HW
                    _reptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(GlbUserComCode, GlbUserName, GlbSerialScanUserSeqNo, GlbSerialScanDocumentType);
                    DisplayPickHeader();
                    BindRequestDetail();
                    BindPickSerials();
                }

                //For serial search
                if (GlbSerialScanDirection == 0) txtSerial1.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSerial1.ClientID + "')");
                //For item search
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");

                BindBinCode();
                ClearItemDetail(false);

            }
            txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
            txtSerial1.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSerial1, ""));
            txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));
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
            if (GlbSerialScanDirection == 0)
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerial);
                DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtSerial1.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
        }
        #endregion

        protected void BindBinCode()
        {
            if (GlbSerialScanDirection == 0)
            {
                DDLBin.Items.Clear();
                List<string> bincode_list = new List<string>();

                bincode_list = CHNLSVC.Inventory.GetAll_binCodes_for_loc(GlbUserComCode, GlbUserDefLoca);
                bincode_list.Add("");
                bincode_list.RemoveAt(0);
                DDLBin.DataSource = bincode_list.OrderBy(Items => Items);
                DDLBin.DataBind();
            }
            else if (GlbSerialScanDirection == 1)
            {
                DDLBin.DataSource = CHNLSVC.Inventory.GetAllLocationBin(GlbUserComCode, GlbUserDefLoca);
                DDLBin.DataTextField = "Ibl_bin_cd";
                DDLBin.DataValueField = "Ibl_bin_cd";
                DDLBin.DataBind();
            }

        }

        protected void BindItemStatus(string _company, string _location, string _bin, string _item, string _serial, bool _isAvailable)
        {
            if (_isAvailable)
            {
                DDLStatus.Items.Clear();
                DataTable _tbl = CHNLSVC.Inventory.GetAvailableItemStatus(_company, _location, _bin, _item, _serial);
                DDLStatus.DataSource = _tbl;
                DDLStatus.DataTextField = "ins_itm_stus";
                DDLStatus.DataValueField = "ins_itm_stus";
                DDLStatus.DataBind();
            }
            else
            {
                DDLStatus.Items.Clear();
                DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode); ;
                DDLStatus.DataSource = _tbl;
                DDLStatus.DataTextField = "mic_cd";
                DDLStatus.DataValueField = "mic_cd";
                DDLStatus.DataBind();
            }


        }

        private void SetItemDetail(MasterItem _item)
        {
            lblItmDescription.Text = _item.Mi_longdesc;
            lblModel.Text = _item.Mi_model;
            lblBrand.Text = _item.Mi_brand;
            if (_item.Mi_is_ser1 == 1) divSerialized.Style.Add("background-color", "Green");
            else divSerialized.Style.Add("background-color", "Red");

            if (_item.Mi_is_scansub) divSSerialized.Style.Add("background-color", "Green");
            else divSSerialized.Style.Add("background-color", "Red");

            if (_item.Mi_warr) divWarranty.Style.Add("background-color", "Green");
            else divWarranty.Style.Add("background-color", "Red");
        }
        private void SetItemBalance(InventoryLocation _loc)
        {
            divFree.InnerText = _loc.Inl_free_qty.ToString();
            divReserved.InnerText = _loc.Inl_res_qty.ToString();
        }

        protected void CheckItem()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;

            if (string.IsNullOrEmpty(DDLBin.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin!");
                DDLBin.Focus();
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
            //according to the direction of the scaning document
            if (GlbSerialScanDirection == 0)
            {
                if (!string.IsNullOrEmpty(DDLBin.SelectedValue.ToString()))
                    BindItemStatus(GlbUserComCode, GlbUserDefLoca, DDLBin.SelectedValue.ToString(), txtItem.Text, string.Empty, true);
                else
                    BindItemStatus(GlbUserComCode, GlbUserDefLoca, "", txtItem.Text, "", true);
            }
            else if (GlbSerialScanDirection == 1)
            {
                BindItemStatus(GlbUserComCode, GlbUserDefLoca, "", txtItem.Text, "", false);
            }


            SetItemDetail(_masterItem);
            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLStatus.SelectedValue.ToString());
            if (_inventoryLocation == null) { divFree.InnerText = " "; divReserved.InnerText = " "; return; }
            if (_inventoryLocation.Count == 1)
            {
                foreach (InventoryLocation _loc in _inventoryLocation)
                {
                    SetItemBalance(_loc);
                }

            }
            else
            {
                divFree.InnerText = "";
                divReserved.InnerText = "";
            }


        }

        protected void CheckItem(object senderr, EventArgs e)
        {

            CheckItem();
        }

        protected void CheckSerial()
        {
            if (string.IsNullOrEmpty(txtSerial1.Text)) return;

            //check direction
            //if direction - out, get the serial details from the system.
            //if direction - in, check the serial availability.

            //check serial availability

            _masterItem = new MasterItem();
            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);

            if (_masterItem.Mi_is_ser1 == 1)
            {
                if (string.IsNullOrEmpty(txtSerial1.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serial");
                    txtSerial1.Focus();
                    return;
                }
                if (txtSerial1.Text.Trim() == "N/A" || txtSerial1.Text.Trim() == "NA")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid serial");
                    txtSerial1.Focus();
                    return;
                }
            }
            else if (_masterItem.Mi_is_ser1 == 0 || _masterItem.Mi_is_ser1 == -1)
            {
                txtSerial1.Text = "N/A";
                txtSerial2.Text = "N/A";
                txtSerial3.Text = "N/A";

            }


            List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLBin.SelectedValue.ToString(), txtSerial1.Text.Trim(), txtSerial2.Text.Trim());

            if (_list.Count > 0)
            {
                if (GlbSerialScanDirection == 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected serial aready available in the location!");
                    txtSerial1.Focus();
                    return;
                }

                Int32 _noOfLines = _list.Count;

                //If only one line = means serial having single items
                if (_noOfLines == 1)
                {
                    ReptPickSerials _List = new ReptPickSerials();
                    foreach (ReptPickSerials _pick in _list)
                    {
                        _List = _pick;
                    }


                    DDLBin.Items.Clear();
                    DDLStatus.Items.Clear();

                    DDLBin.Items.Add(_List.Tus_bin);
                    DDLStatus.Items.Add(_List.Tus_itm_stus);
                    if (string.IsNullOrEmpty(txtItem.Text)) txtItem.Text = _List.Tus_itm_cd;
                    txtSerial2.Text = _List.Tus_ser_2;
                    txtSerial3.Text = _List.Tus_ser_3;
                    _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                    txtQty.Text = "1";
                    SetItemDetail(_masterItem);
                }
                //if more than one line = means serial having more than item
                if (_noOfLines > 1)
                {
                    //Load to the common modal
                    ReptPickSerials _List = new ReptPickSerials();
                    foreach (ReptPickSerials _pick in _list)
                    {
                        _List = _pick;
                    }


                    DDLBin.Items.Clear();
                    DDLStatus.Items.Clear();

                    DDLBin.Items.Add(_List.Tus_bin);
                    DDLStatus.Items.Add(_List.Tus_itm_stus);
                    if (string.IsNullOrEmpty(txtItem.Text)) txtItem.Text = _List.Tus_itm_cd;
                    txtSerial2.Text = _List.Tus_ser_2;
                    txtSerial3.Text = _List.Tus_ser_3;
                    _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                    txtQty.Text = "1";
                    SetItemDetail(_masterItem);
                }

            }
        }

        protected void CheckSerial(object sender, EventArgs e)
        {
            CheckSerial();
        }

        protected void CheckQty()
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;

            if (DDLBin.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin");
                DDLBin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DDLStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                DDLStatus.Focus();
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
                    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLStatus.SelectedValue.ToString());

                    if (_inventoryLocation.Count == 1)
                    {
                        foreach (InventoryLocation _loc in _inventoryLocation)
                        {
                            SetItemBalance(_loc);
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
                        divFree.InnerText = "";
                        divReserved.InnerText = "";
                    }

                }

            }

        }

        protected void CheckQty(object sender, EventArgs e)
        {
            CheckQty();
        }

        private void ClearItemDetail(bool _isSerialized)
        {
            if (_isSerialized == false)
            {
                txtItem.Text = "";
                divSerialized.Style.Add("background-color", "White");
                divSSerialized.Style.Add("background-color", "White");
                divWarranty.Style.Add("background-color", "White");

                divFree.InnerText = "";
                divReserved.InnerText = "";
                txtQty.Text = "";

                lblBrand.Text = "";
                lblItmDescription.Text = "";
                lblModel.Text = "";
            }

            txtSerial1.Text = "";
            txtSerial2.Text = "";
            txtSerial3.Text = "";
            txtMFC.Text = "N/A";

        }

        private ReptPickSerials AddItemToSerialList(Int32 _serialid,decimal _qty,MasterItem _itm)
        {
            ReptPickSerials _list = new ReptPickSerials();
            DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;
            _list = new ReptPickSerials();
            _list.Tus_batch_line = 1;
            _list.Tus_bin = DDLBin.SelectedValue.ToString();
            _list.Tus_com = GlbUserComCode.Trim();
            _list.Tus_cre_by = GlbUserName.Trim();
            _list.Tus_cre_dt = _date;
            _list.Tus_doc_dt = _date;
            _list.Tus_doc_no = GlbSerialScanRequestNo;
            _list.Tus_exist_grncom = GlbUserComCode.Trim();
            _list.Tus_exist_grndt = _date;
            _list.Tus_exist_grnno = GlbSerialScanRequestNo;
            _list.Tus_exist_supp = GlbSerialBusinessEntity;//TODO : GET SUPPLIER
            _list.Tus_itm_cd = txtItem.Text.Trim();
            _list.Tus_itm_line = 1;
            _list.Tus_itm_stus = DDLStatus.SelectedValue.ToString();
            _list.Tus_loc = GlbUserDefLoca;
            _list.Tus_orig_grncom = GlbUserComCode.Trim();
            _list.Tus_orig_grndt = _date;
            _list.Tus_orig_grnno = GlbSerialScanRequestNo;
            _list.Tus_orig_supp = GlbSerialBusinessEntity;//TODO : GET SUPPLIER
            _list.Tus_qty = Convert.ToDecimal(_qty);
            _list.Tus_seq_no = GlbSerialScanUserSeqNo;
            _list.Tus_ser_1 = txtSerial1.Text.Trim();
            _list.Tus_ser_2 = txtSerial2.Text.Trim();
            _list.Tus_ser_3 = txtSerial3.Text.Trim();
            _list.Tus_ser_4 = "N/A";
            if (_serialid == -1)
            {
                _list.Tus_ser_id = 0;
            }
            else
            {
                _list.Tus_ser_id = _serialid;
            }
            _list.Tus_ser_line = 1;
            _list.Tus_session_id = GlbUserSessionID;
            _list.Tus_unit_cost = 0;
            _list.Tus_unit_price = 0;
            _list.Tus_usrseq_no = GlbSerialScanUserSeqNo;
            _list.Tus_warr_no = "";
            _list.Tus_warr_period = 0;
            _list.Tus_itm_desc = _itm.Mi_longdesc;
            _list.Tus_itm_model = _itm.Mi_model;
            _list.Tus_itm_brand = _itm.Mi_brand;
            _list.Tus_base_doc_no = "N/A";
            _list.Tus_base_itm_line = 1;

            return _list;
        }

        protected void AddItem(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DDLBin.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin");
                DDLBin.Focus();
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

            if (string.IsNullOrEmpty(DDLStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                DDLStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSerial1.Text.ToString()) && _masterItem.Mi_is_ser1 == 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serial");
                txtSerial1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSerial2.Text.ToString()) && _masterItem.Mi_is_ser1 == 1 && _masterItem.Mi_is_ser2 == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the seria2");
                txtSerial2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSerial3.Text.ToString()) && _masterItem.Mi_is_ser1 == 1 && _masterItem.Mi_is_ser3 == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the seria3");
                txtSerial3.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty");
                txtQty.Focus();
                return;
            }

            //IsExistInTempPickSerial
            if (CHNLSVC.Inventory.IsExistInTempPickSerial(GlbUserComCode, GlbSerialScanUserSeqNo.ToString(), txtItem.Text, txtSerial1.Text) > 0 && _masterItem.Mi_is_ser1 == 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The selected serial is already picked");
                return;
            }


            if (_masterItem.Mi_is_ser1 == 1)
            {
                txtQty.Text = "1";
            }

            if (_masterItem.Mi_is_ser1 <= 0)
            {
                txtSerial1.Text = "";
                txtSerial2.Text = "";
                txtSerial3.Text = "";
            }


            if (GlbSerialScanDirection == 0)
            {
                //check for the location balance.
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), DDLStatus.SelectedValue.ToString());

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
            }


            //If serial itm
            if (_masterItem.Mi_is_ser1 == 1)
            {
                if (GlbSerialScanDirection == 0)//&& GlbSerialScanDocumentType != "GRN"
                {
                    ReptPickSerials _list = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, DDLBin.SelectedValue.ToString(), txtItem.Text.Trim(), txtSerial1.Text.Trim());
                    if (_list != null)
                    {
                        _list.Tus_itm_desc = _masterItem.Mi_longdesc;
                        _list.Tus_itm_model = _masterItem.Mi_model;
                        _list.Tus_itm_brand = _masterItem.Mi_brand;
                        _list.Tus_cre_by = GlbUserName;
                        _list.Tus_usrseq_no = GlbSerialScanUserSeqNo;
                        CHNLSVC.Inventory.SaveAllScanSerials(_list, null);
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToInt32(_list.Tus_serial_id), -1);
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The item/serial already scaned or incorrect information");
                        return;
                    }
                }
                else
                {
                    Int32 _serialid = CHNLSVC.Inventory.GetSerialID();
                    CHNLSVC.Inventory.SaveAllScanSerials(AddItemToSerialList(_serialid, 1, _masterItem), null);
                }
                BindPickSerials();
                ClearItemDetail(true);
                return;

            }

            //If non serial itm, but serial id have
            if (_masterItem.Mi_is_ser1 == 0)
            {
                if (GlbSerialScanDirection == 0)
                {
                    //TODO: Load top most serial_id from serial table to scan table
                    List<ReptPickSerials> _list = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(GlbUserComCode, GlbUserDefLoca, DDLBin.SelectedValue.ToString(), txtItem.Text.Trim(), DDLStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text));
                    if (_list != null || _list.Count > 0)
                    {

                        foreach (ReptPickSerials _single in _list)
                        {
                            _single.Tus_cre_by = GlbUserName;
                            _single.Tus_usrseq_no = GlbSerialScanUserSeqNo;
                            CHNLSVC.Inventory.SaveAllScanSerials(_single, null);
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToInt32(_single.Tus_serial_id), -1);
                        }
                        BindPickSerials();
                        ClearItemDetail(false);
                        return;
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The item already scaned or incorrect information");
                        return;
                    }


                }
                else
                {
                    //loop according to the qty and generate serial id
                    Int32 _count = Convert.ToInt32(txtQty.Text.Trim());
                    for (Int32 i = 0; i <= _count - 1; i++)
                    {
                        Int32 _serialid = CHNLSVC.Inventory.GetSerialID();
                        CHNLSVC.Inventory.SaveAllScanSerials(AddItemToSerialList(_serialid, 1, _masterItem), null);
                    }
                    //TODO: Generate Dummy serial for the selected serial range
                    BindPickSerials();
                    ClearItemDetail(false);
                    return;
                }

            }

            //if non serial itm,no serial id = -1
            if (_masterItem.Mi_is_ser1 == -1)
            {
                //Add directly to the scan  table
                CHNLSVC.Inventory.SaveAllScanSerials(AddItemToSerialList(-1, Convert.ToDecimal(txtQty.Text), _masterItem), null);
                BindPickSerials();
                ClearItemDetail(false);
                return;
            }

        }

        protected void OnRemoveFromGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            string _item = (string)gvMainSerial.DataKeys[row_id][0];
            string _bin1 = (string)gvMainSerial.DataKeys[row_id][1];
            decimal _bin2 = (decimal)gvMainSerial.DataKeys[row_id][2];
            string _bin3 = (string)gvMainSerial.DataKeys[row_id][3];
            Int32 _serialID = (Int32)gvMainSerial.DataKeys[row_id][4];
            string _bin = (string)gvMainSerial.DataKeys[row_id][5];

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

            if (_masterItem.Mi_is_ser1 == 1) CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, GlbSerialScanUserSeqNo, Convert.ToInt32(_serialID));
            else CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, GlbSerialScanUserSeqNo, Convert.ToInt32(_serialID), _item, _bin);


            BindPickSerials();

        }


        //protected List<ReptPickSerials> GetSummary(List<ReptPickSerials> _reptPickSerials)
        //{
        //    var _summary =
        //       from _pickSerials in _reptPickSerials
        //       group _pickSerials by new { _pickSerials.Tus_bin, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_stus, _pickSerials.Tus_ser_1 } into itm
        //       select new { bincode = itm.Key.Tus_bin, itemcode = itm.Key.Tus_itm_cd, itemstatus = itm.Key.Tus_itm_stus, itemqty = itm.Sum(p => p.Tus_qty) };

        //    List<ReptPickSerials> _list = new List<ReptPickSerials>();
        //    foreach (var _single in _summary)
        //    {

        //    }


        //    return _summary;
        //}

        protected void ReturnToBasePage(object sender, EventArgs e)
        {
            Response.Redirect(GlbSerialScanReturnUrl, false);
        }
    }
}