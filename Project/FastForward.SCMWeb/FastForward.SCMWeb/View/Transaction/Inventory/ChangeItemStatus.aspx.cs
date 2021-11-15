using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Reflection;
using FastForward.SCMWeb.Services;
using System.Collections;
using System.Globalization;


namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ChangeItemStatus : BasePage
    {
        #region Varialbe
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }
        protected List<ReptPickSerials> _ReptPickSerials { get { return (List<ReptPickSerials>)Session["_ReptPickSerials"]; } set { Session["_ReptPickSerials"] = value; } }

        protected List<ReptPickItems> _saveonly { get { return (List<ReptPickItems>)Session["_saveonly"]; } set { Session["_saveonly"] = value; } }


        private string _SerialSearchType = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private List<InventoryRequestItem> ScanItemListExcel = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        private List<InventoryRequestItem> ItemList = new List<InventoryRequestItem>();
        private List<string> Seriallist = new List<string>();


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    grdItems.DataSource = new int[] { };
                    grdItems.DataBind();

                    grdSerial.DataSource = new int[] { };
                    grdSerial.DataBind();

                    grdResult.DataSource = new int[] { };
                    grdResult.DataBind();

                    DateTime _ddate = DateTime.Now;
                    dtpDate.Text = _ddate.ToString("dd/MMM/yyyy");

                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ-S", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    ddlSeqNo.DataSource = SeqNumList;
                    ddlSeqNo.DataBind();
                    loadItemStatus_new();
                    ViewState["ITEMCHANGE"] = null;
                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[12] { new DataColumn("itri_itm_cd"), new DataColumn("itri_mitm_cd"), new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus_desc"), new DataColumn("itri_itm_stus"), new DataColumn("itri_note_desc"), new DataColumn("itri_note"), new DataColumn("itri_unit_price"), new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_line_no") });
                    ViewState["ITEMCHANGE"] = dtitems;
                    this.BindGrid();

                    ViewState["ITEMCHANGESER"] = null;
                    DataTable dtitemsser = new DataTable();
                    dtitemsser.Columns.AddRange(new DataColumn[15] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus_desc"), new DataColumn("tus_itm_stus"), new DataColumn("tus_new_status_desc"), new DataColumn("tus_new_status"), new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), new DataColumn("tus_new_remarks"), new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line") });
                    ViewState["ITEMCHANGESER"] = dtitemsser;
                    this.BindSerGrid();
                    ucOutScan._derectOut = true;
                    ucOutScan.adjustmentTypeValue = "-";
                    ucOutScan.doc_tp = "ADJ-S";
                    EnableDeleteButton();
                    ucOutScan.isApprovalSend = false;
                    // Back date 
                    BackDatefucntion();
                    loadItemStatus();
                }
                else
                {
                    string sessopnpopup = (string)Session["POPUPSHOW"];
                    if (!string.IsNullOrEmpty(sessopnpopup))
                    {
                        UserPopoup.Show();
                        Session["POPUPSHOW"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    Information.Visible = true;
                    lbtnimgselectdate.Visible = true;

                    DateTime Selecteddate = Convert.ToDateTime(dtpDate.Text.Trim());
                    DateTime appfromdate = Convert.ToDateTime(_bdt.Gad_act_from_dt);
                    DateTime apptodate = Convert.ToDateTime(_bdt.Gad_act_to_dt);

                    if (_bdt.Gad_alw_curr_trans == true)
                    {
                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                    else
                    {
                        if (dtpDate.Text == DateTime.Now.Date.ToString())
                        {
                            Session["ALLOWCURDATE"] = "1";
                        }
                        else
                        {
                            Session["ALLOWCURDATE"] = "0";
                        }

                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                }
                else
                {
                    lbtnimgselectdate.Visible = false;
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }
        public void CheckSessionIsExpired()
        {
            if (Session["UserID"].ToString() != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),out _expMsg) == true)
                {
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                }
            }
        }
        private void BackDatefucntion()
        {
            bool _allowCurrentTrans = false;
            Session["GlbModuleName"] = "m_Trans_Inventory_StatusChange";
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), dtpDate, lblBackDateInfor, "", out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(dtpDate.Text).Date != DateTime.Now.Date)
                    {
                        dtpDate.Enabled = true;
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        dtpDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpDate.Enabled = true;
                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    dtpDate.Focus();
                    return;
                }
            }
        }
        protected void BindGrid()
        {
            try
            {
                grdItems.DataSource = (DataTable)ViewState["ITEMCHANGE"];
                grdItems.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindSerGrid()
        {
            try
            {
                grdSerial.DataSource = (DataTable)ViewState["ITEMCHANGESER"];
                grdSerial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    Process();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        private void Clear()
        {
            try
            {
                Session["CSSDOC"] = null;

                ViewState["ITEMCHANGE"] = null;
                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[12] { new DataColumn("itri_itm_cd"), new DataColumn("itri_mitm_cd"), new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus_desc"), new DataColumn("itri_itm_stus"), new DataColumn("itri_note_desc"), new DataColumn("itri_note"), new DataColumn("itri_unit_price"), new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_line_no") });
                ViewState["ITEMCHANGE"] = dtitems;
                this.BindGrid();

                ViewState["ITEMCHANGESER"] = null;
                DataTable dtitemsser = new DataTable();
                dtitemsser.Columns.AddRange(new DataColumn[15] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus_desc"), new DataColumn("tus_itm_stus"), new DataColumn("tus_new_status_desc"), new DataColumn("tus_new_status"), new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), new DataColumn("tus_new_remarks"), new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line") });
                ViewState["ITEMCHANGESER"] = dtitemsser;
                this.BindSerGrid();

                txtDocumentNo.Text = string.Empty;
                txtManualRef.Text = string.Empty;
                txtOtherRef.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtUserSeqNo.Text = string.Empty;

                EnableDeleteButton();
                //ucOutScan.PageClear();
                ddlSeqNo.SelectedIndex = 0;
                ucOutScan.userSeqNo = string.Empty;
                ucOutScan.ScanItemList = null;
                Session["DUPLICATE_SERIAL"] = null;
                _ReptPickSerials = new List<ReptPickSerials>();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void EnableDeleteButton()
        {
            try
            {
                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelete");

                    _delbutton.Enabled = true;
                    _delbutton.CssClass = "buttonUndocolor";
                    _delbutton.OnClientClick = "ConfirmDelete();";
                }

                foreach (GridViewRow hiderowbtnser in this.grdSerial.Rows)
                {
                    LinkButton _delbuttonser = (LinkButton)hiderowbtnser.FindControl("lbtndeleteserial");

                    _delbuttonser.Enabled = true;
                    _delbuttonser.CssClass = "buttonUndocolor";
                    _delbuttonser.OnClientClick = "ConfirmDeleteSerial();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlSeqNo.Text))
                {
                    txtUserSeqNo.Text = ddlSeqNo.Text;
                    LoadItems(txtUserSeqNo.Text);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //private List<ReptPickSerials> LoadItems(string _seqNo)
        //{
        //    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
        //    List<ReptPickItems> _reptItems = new List<ReptPickItems>();
        //    List<ReptPickSerials> _serList = new List<ReptPickSerials>();

        //    Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ-S", Session["UserID"].ToString(), 0, _seqNo);
        //    if (user_seq_num == -1)
        //    {
        //        user_seq_num = GenerateNewUserSeqNo();
        //    }

        //    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ-S");

        //    _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
        //    foreach (ReptPickItems _reptitem in _reptItems)
        //    {
        //        InventoryRequestItem _itm = new InventoryRequestItem();
        //        MasterItem _itms = new MasterItem();
        //        _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
        //        _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
        //        _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
        //        _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
        //        //_itm.Itri_line_no = Convert.ToInt32(_reptitem.tui));

        //        _itm.Itri_note = SetNewItemStatus(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);
        //        _itm.Itri_qty = 0;
        //        _itm.Mi_longdesc = _itms.Mi_longdesc;
        //        _itm.Mi_model = _itms.Mi_model;
        //        _itm.Mi_brand = _itms.Mi_brand;
        //        _itm.Itri_unit_price = 0;
        //        _itmList.Add(_itm);
        //    }
        //    ScanItemList = _itmList;
        //    grdItems.AutoGenerateColumns = false;
        //    grdItems.DataSource = _itmList;
        //    grdItems.DataBind();

        //    ucOutScan.ScanItemList = _itmList;

        //    if (_serList != null)
        //    {
        //        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
        //        foreach (var itm in _scanItems)
        //        {
        //            foreach (GridViewRow row in this.grdItems.Rows)
        //            {
        //                Label _item = (Label)row.FindControl("lblitri_itm_cd");
        //                Label _itemline = (Label)row.FindControl("lblitri_line_no");
        //                Label _pickqty = (Label)row.FindControl("lblitri_qty");

        //                if (itm.Peo.Tus_itm_cd == _item.Text && itm.Peo.Tus_base_itm_line == Convert.ToInt32(Convert.ToDecimal(_itemline.Text)))
        //                {
        //                    _pickqty.Text = Convert.ToDecimal(itm.theCount).ToString();
        //                }
        //            }
        //        }
        //        grdSerial.AutoGenerateColumns = false;
        //        grdSerial.DataSource = _serList;
        //        grdSerial.DataBind();
        //    }
        //    else
        //    {
        //        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
        //        grdSerial.AutoGenerateColumns = false;
        //        grdSerial.DataSource = emptyGridList;
        //        grdSerial.DataBind();
        //    }
        //    return _serList;
        //}

        private List<ReptPickSerials> LoadItems(string _seqNo)
        {
            List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
            List<ReptPickItems> _reptItems = new List<ReptPickItems>();
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ-S", Session["UserID"].ToString(), 0, _seqNo);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo();
            }

            //Get Serials
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ-S");
            if (_serList != null)
            {
                foreach (ReptPickSerials item in _serList)
                {
                    item.Tus_itm_stus_Desc = getItemStatusDesc(item.Tus_itm_stus);
                    item.Tus_new_status_Desc = getItemStatusDesc(item.Tus_new_status);
                    
                }

                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (GridViewRow row in this.grdItems.Rows)
                        {
                            Label _item = (Label)row.FindControl("lblitri_itm_cd");
                            Label _itemline = (Label)row.FindControl("lblitri_line_no");
                            Label _pickqty = (Label)row.FindControl("lblitri_qty");

                            if (itm.Peo.Tus_itm_cd == _item.Text && itm.Peo.Tus_base_itm_line == Convert.ToInt32(Convert.ToDecimal(_itemline.Text)))
                            {
                                _pickqty.Text = Convert.ToDecimal(itm.theCount).ToString();
                            }
                        }
                    }
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();

                    ucOutScan.BinCode = _serList[0].Tus_bin;
                    ucOutScan.ToBinCode = _serList[0].Tus_bin_to;
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = emptyGridList;
                grdSerial.DataBind();
            }

            // Get Items
            _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
            foreach (ReptPickItems _reptitem in _reptItems)
            {
                if (_serList != null)
                {
                    var _filter = _serList.Where(x => x.Tus_itm_cd == _reptitem.Tui_req_itm_cd && x.Tus_itm_stus == _reptitem.Tui_req_itm_stus).ToList();
                    if (_filter.Count > 0)
                    {
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        MasterItem _itms = new MasterItem();
                        _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                        _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                        _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                        _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                        // var _filter = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus).Tus_itm_line;

                        _itm.Itri_line_no = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus).Tus_itm_line;

                        if (_itms.Mi_is_ser1 == 1)
                        {
                            _itm.Itri_qty = 1;
                        }
                        else
                        {
                            decimal QtyValue = _serList.Where(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus).Sum(e => e.Tus_qty);
                            _itm.Itri_qty = QtyValue;
                        }

                        //Set Pick Qty
                        //_itm.Itri_qty = SetPickQty(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);

                        //Set New Item Status
                        _itm.Itri_note = SetNewItemStatus(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);

                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_itm_stus_desc = getItemStatusDesc(_itm.Itri_itm_stus);
                        _itm.Itri_note_desc = getItemStatusDesc(_itm.Itri_note);
                        //_itm.Itri_unit_price = 0;
                        //_itm.Itri_note = _reptitem.Tui_pic_itm_stus;
                        _itm.Itri_mitm_cd = _reptitem.Tui_pic_itm_cd;
                        _itmList.Add(_itm);
                    }
                }
            }
            ScanItemList = _itmList;

            foreach (InventoryRequestItem item in ScanItemList)
            {
                var _filter = _serList.Find(x => x.Tus_itm_cd == item.Itri_itm_cd);
                if (_filter != null)
                {
                    item.Itri_mitm_cd = _filter.Tus_new_itm_cd;
                }
              
            }

            grdItems.AutoGenerateColumns = false;
            grdItems.DataSource = _itmList;
            grdItems.DataBind();

            ucOutScan.ScanItemList = _itmList;
            ucOutScan.PickSerial = _serList;
            ucOutScan.userSeqNo = _seqNo;
            ViewState["userSeqNo"] = _seqNo;
            return _serList;
        }



        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 0, Session["UserCompanyCode"].ToString());
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ-S";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = generated_seq.ToString();
            RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();

            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(_SerialSearchType + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ucOutScan.TXTItemCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        if (ddlAdjTypeSearch.Text == "ADJ+") paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "1" + seperator);
                        else paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "0" + seperator);

                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void lbtnmodelfind_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                DateTime d1 = DateTime.Now;
                d1 = d1.AddMonths(-1);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, d1, DateTime.Now);

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ddr["Date"].ToString();
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

              //  DataView dv = new DataView(_result);
              //  dv.Sort = "Date ASC";
                //_result = dv.ToTable();

                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtTDate.Text = System.DateTime.Now.ToShortDateString();
                txtFDate.Text = d1.ToShortDateString();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "CSSDOC";
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "CSSDOC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                    foreach (DataRow ddr in _result.Rows)
                    {
                        string doc = ddr["Document"].ToString();
                        string subtp = ddr["Sub Type"].ToString();
                        string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                        string manref = ddr["Manual Ref No"].ToString();
                        string otherdoc = ddr["Other Document"].ToString();
                        string entry = ddr["Entry No"].ToString();
                        string job = ddr["Job No"].ToString();
                        string otherloc = ddr["Other Loc"].ToString();
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                    }

                    _result.Clear();
                    _result = dtresultcopyMRN;

                    DataView dv = new DataView(_result);
                    dv.Sort = "Date desc,Document";
                    _result = dv.ToTable();

                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "CSSDOC";
                    UserPopoup.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "CSSDOC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                    foreach (DataRow ddr in _result.Rows)
                    {
                        string doc = ddr["Document"].ToString();
                        string subtp = ddr["Sub Type"].ToString();
                        string date = ddr["Date"].ToString();
                        string manref = ddr["Manual Ref No"].ToString();
                        string otherdoc = ddr["Other Document"].ToString();
                        string entry = ddr["Entry No"].ToString();
                        string job = ddr["Job No"].ToString();
                        string otherloc = ddr["Other Loc"].ToString();
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                    }

                    _result.Clear();
                    _result = dtresultcopyMRN;

                    DataView dv = new DataView(_result);
                    dv.Sort = "Date desc,Document";
                    _result = dv.ToTable();

                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "CSSDOC";
                    Session["CSSDOC"] = "true";
                    ViewState["SEARCH"] = _result;
                    Session["POPUPSHOW"] = "1";
                    UserPopoup.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string ID = grdResult.SelectedRow.Cells[1].Text;
                if (lblvalue.Text == "CSSDOC")
                {
                    string Date = grdResult.SelectedRow.Cells[3].Text;
                    txtDocumentNo.Text = ID;
                    lblvalue.Text = "";
                    Session["CSSDOC"] = Date;
                    txtDocumentNo_Leave();
                    UserPopoup.Hide();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                if (lblvalue.Text == "CSSDOC")
                {
                    grdResult.DataSource = null;

                    grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                    grdResult.DataBind();
                    UserPopoup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "CSSDOC")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                    foreach (DataRow ddr in _result.Rows)
                    {
                        string doc = ddr["Document"].ToString();
                        string subtp = ddr["Sub Type"].ToString();
                        string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                        string manref = ddr["Manual Ref No"].ToString();
                        string otherdoc = ddr["Other Document"].ToString();
                        string entry = ddr["Entry No"].ToString();
                        string job = ddr["Job No"].ToString();
                        string otherloc = ddr["Other Loc"].ToString();
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                    }

                    _result.Clear();
                    _result = dtresultcopyMRN;

                    DataView dv = new DataView(_result);
                    dv.Sort = "Date desc,Document";
                    _result = dv.ToTable();

                    grdResult.DataSource = null;
                    grdResult.DataBind();
                    grdResult.DataSource = _result;
                    grdResult.DataBind();

                    lblvalue.Text = "CSSDOC";
                    Session["CSSDOC"] = "true";
                    ViewState["SEARCH"] = _result;
                    UserPopoup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private void txtDocumentNo_Leave()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    if (ddlAdjTypeSearch.Text == "ADJ+") _direction = 1;

                    #region Clear Data
                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmSave();";

                    txtManualRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Text = string.Empty;
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "ADJ")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true && _direction == 0)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == false && _direction == 1)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Document No !!!')", true);
                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_entry_no;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtUserSeqNo.Text = string.Empty;
                        ddlSeqNo.Text = string.Empty;
                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            //_itm.Itri_mitm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus_desc = getItemStatusDesc(itm.Peo.Tus_itm_stus);
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                           // _itm.Itri_note = itm.Peo.Tus_itm_stus;
                            _itmList.Add(_itm);
                        }
                        ScanItemList = _itmList;

                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = ItemDes(ScanItemList);
                        grdItems.DataBind();

                        foreach (ReptPickSerials item in _serList)
                        {
                            item.Tus_itm_stus_Desc = getItemStatusDesc(item.Tus_itm_stus);
                            item.Tus_new_status_Desc = "";
                            item.Tus_new_status = "";
                        }

                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = SerialDes(_serList);
                        grdSerial.DataBind();

                        ViewState["ITEMCHANGE"] = null;
                        this.BindGrid();
                        DataTable dt1 = new DataTable();
                        dt1 = ConvertToDataTable(ScanItemList);
                        ViewState["ITEMCHANGE"] = dt1;
                        this.BindGrid();

                        ViewState["ITEMCHANGESER"] = null;
                        this.BindSerGrid();
                        DataTable dt2 = new DataTable();
                        dt2 = ConvertToDataTable(_serList);
                        ViewState["ITEMCHANGESER"] = dt2;
                        this.BindSerGrid();


                        foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                        {
                            LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelete");

                            _delbutton.Enabled = false;
                            _delbutton.CssClass = "buttoncolor";
                            _delbutton.OnClientClick = "return Enable();";
                        }

                        foreach (GridViewRow hiderowbtnser in this.grdSerial.Rows)
                        {
                            LinkButton _delbuttonser = (LinkButton)hiderowbtnser.FindControl("lbtndeleteserial");

                            _delbuttonser.Enabled = false;
                            _delbuttonser.CssClass = "buttoncolor";
                            _delbuttonser.OnClientClick = "return Enable();";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item not found !!!')", true);
                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        return;
                    }
                    #endregion

                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        protected void lbtnprintord_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                   // ClearAll();
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        private void ClearAll()
        {
            try
            {
                Clear();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolor";
                btnSave.OnClientClick = "ConfirmSave();";

                grdItems.DataSource = null;
                grdSerial.DataSource = null;
                grdItems.DataBind();
                grdSerial.DataBind();
                ViewState["SerialList"] = null;
                ViewState["ScanItemList"] = null;
                ViewState["ITEMCHANGE"] = null;
                ScanItemList = null;
                //ucOutScan.GridClear();
                Session["EXCEL"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            if (txtconfirmdelete.Value == "Yes")
            {
                try
                {
                    if ((btnSave.Enabled == true) && (!string.IsNullOrEmpty(txtUserSeqNo.Text.Trim())))
                    {
                        var lb = (LinkButton)sender;
                        var row = (GridViewRow)lb.NamingContainer;

                        if (row != null)
                        {
                            string _item = (row.FindControl("lblitri_itm_cd") as Label).Text;

                            if (string.IsNullOrEmpty(_item))
                            {
                                return;
                            }

                            string _itmStatus = (row.FindControl("lblcurstgrd") as Label).Text;
                            string _itmCost = (row.FindControl("lblunitcostgrd") as Label).Text;
                            string _qty = (row.FindControl("lblitri_qty") as Label).Text;
                            string _lineNo = (row.FindControl("lblitri_line_no") as Label).Text;

                            bool result = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, Convert.ToInt32(Convert.ToDecimal(_lineNo)), 2);

                            List<ReptPickSerials> _list = new List<ReptPickSerials>();
                            _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "ADJ-S");
                            if (_list != null)
                            {
                                if (_list.Count > 0)
                                {
                                    var _delete = (from _lst in _list
                                                   where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus && _lst.Tus_base_itm_line == Convert.ToInt32(Convert.ToDecimal(_lineNo))
                                                   select _lst).ToList();

                                    foreach (ReptPickSerials _ser in _delete)
                                    {
                                        Delete_Serials(_item, _itmStatus, _ser.Tus_ser_id);
                                    }
                                }
                            }
                            LoadItems(txtUserSeqNo.Text);
                        }
                    }
                }
                catch (Exception err)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        private void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine date conflict with the server date.please contact system administrator !!!')", true);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine time zone conflict with the server time zone.please contact system administrator !!!')", true);
                return false;
            }
            return true;
        }

        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }

                    string msg = "The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                }
            }
            return _appropriate;
        }

        private void Process()
        {
            try
            {
                if (grdItems.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add items !!!')", true);
                    ucOutScan.TXTItemCode.Focus();
                    return;
                }
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "m_Trans_Inventory_StatusChange", dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                DateTime dt1 = Convert.ToDateTime(dtpDate.Text);
                DateTime dt2 = dt1.Date;

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";

                ////bool _allowCurrentTrans = false;
                ////if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDate, lblH1, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                ////{
                ////if (_allowCurrentTrans == true)
                ////{

                //if (dt2 != DateTime.Now.Date)
                //{
                //    dtpDate.Enabled = true;
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                //    dtpDate.Focus();
                //    return;
                //}

                //String test_string = txtManualRef.Text.Trim();

                //if (!string.IsNullOrEmpty(test_string))
                //{
                //    if (!System.Text.RegularExpressions.Regex.IsMatch(test_string, "^[a-zA-Z0-9\x20]+$"))
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Manual Ref # should not contain special characters !!!')", true);
                //        txtManualRef.Focus();
                //        return;
                //    }
                //}

                //String test_string1 = txtOtherRef.Text.Trim();

                //if (!string.IsNullOrEmpty(test_string1))
                //{
                //    if (!System.Text.RegularExpressions.Regex.IsMatch(test_string1, "^[a-zA-Z0-9\x20]+$"))
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Other Ref # should not contain special characters !!!')", true);
                //        txtOtherRef.Focus();
                //        return;
                //    }
                //}

                //String test_string2 = txtRemarks.Text.Trim();

                //if (!string.IsNullOrEmpty(test_string2))
                //{
                //    if (!System.Text.RegularExpressions.Regex.IsMatch(test_string2, "^[a-zA-Z0-9\x20]+$"))
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Remarks should not contain special characters !!!')", true);
                //        txtRemarks.Focus();
                //        return;
                //    }
                //}

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo_minus = "";
                string documntNo_plus = "";
                string error = string.Empty;

                Int32 _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);

                string isexcelupload = (string)Session["EXCEL"];

                if (string.IsNullOrEmpty(isexcelupload))
                {
                    reptPickSerialsList = LoadItems(_userSeqNo.ToString());
                }

               reptPickSerialsList = LoadItems(_userSeqNo.ToString());
               // reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "ADJ-S");
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-S");
                #region Check Duplicate Serials
                if (reptPickSerialsList != null)
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Tus_ser_1)).Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials !!!')", true);
                        return;
                    }
                }
                #endregion

                

                #region Check Serial Scan or not

                if (grdItems == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }
                if (grdItems.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }
                foreach (GridViewRow row in this.grdItems.Rows)
                {
                    MasterItem _itms = new MasterItem();

                    Label _item = (Label)row.FindControl("lblitri_itm_cd");
                    Label _pickitemqty = (Label)row.FindControl("lblitri_qty");

                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item.Text.Trim());
                    if (_itms.Mi_is_ser1 == 1)
                    {
                        if (Convert.ToDecimal(_pickitemqty.Text) == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial number is not picked for item  " + _itms.Mi_cd + " !!!')", true);
                            return;
                        }
                    }
                }

                #endregion

                #region Check Referance Date and the Doc Date

                if (IsReferancedDocDateAppropriate(reptPickSerialsList, dt2) == false)
                {
                    return;
                }
                #endregion

                InventoryHeader _hdrMinus = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    _hdrMinus.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _hdrMinus.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _hdrMinus.Ith_channel = string.Empty;
                    }
                }

                _hdrMinus.Ith_acc_no = "STATUS_CHANGE";
                _hdrMinus.Ith_anal_1 = "";
                _hdrMinus.Ith_anal_2 = "";
                _hdrMinus.Ith_anal_3 = "";
                _hdrMinus.Ith_anal_4 = "";
                _hdrMinus.Ith_anal_5 = "";
                _hdrMinus.Ith_anal_6 = _userSeqNo;
                _hdrMinus.Ith_anal_7 = 0;
                _hdrMinus.Ith_anal_8 = DateTime.MinValue;
                _hdrMinus.Ith_anal_9 = DateTime.MinValue;
                _hdrMinus.Ith_anal_10 = false;
                _hdrMinus.Ith_anal_11 = false;
                _hdrMinus.Ith_anal_12 = false;
                _hdrMinus.Ith_bus_entity = "";
                _hdrMinus.Ith_cate_tp = "STUS";
                _hdrMinus.Ith_com = Session["UserCompanyCode"].ToString();
                _hdrMinus.Ith_com_docno = "";
                _hdrMinus.Ith_cre_by = Session["UserID"].ToString();
                _hdrMinus.Ith_cre_when = DateTime.Now;
                _hdrMinus.Ith_del_add1 = "";
                _hdrMinus.Ith_del_add2 = "";
                _hdrMinus.Ith_del_code = "";
                _hdrMinus.Ith_del_party = "";
                _hdrMinus.Ith_del_town = "";
                _hdrMinus.Ith_direct = false;
                _hdrMinus.Ith_doc_date = Convert.ToDateTime(dtpDate.Text);
                _hdrMinus.Ith_doc_no = string.Empty;
                _hdrMinus.Ith_doc_tp = "ADJ";
                _hdrMinus.Ith_doc_year = dt2.Year;
                _hdrMinus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrMinus.Ith_entry_tp = "STTUS";
                _hdrMinus.Ith_git_close = true;
                _hdrMinus.Ith_git_close_date = DateTime.MinValue;
                _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin;
                _hdrMinus.Ith_isprinted = false;
                _hdrMinus.Ith_is_manual = false;
                _hdrMinus.Ith_job_no = string.Empty;
                _hdrMinus.Ith_loading_point = string.Empty;
                _hdrMinus.Ith_loading_user = string.Empty;
                _hdrMinus.Ith_loc = Session["UserDefLoca"].ToString();
                _hdrMinus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrMinus.Ith_mod_by = Session["UserID"].ToString();
                _hdrMinus.Ith_mod_when = DateTime.Now;
                _hdrMinus.Ith_noofcopies = 0;
                _hdrMinus.Ith_oth_loc = string.Empty;
                _hdrMinus.Ith_oth_docno = "N/A";
                _hdrMinus.Ith_remarks = txtRemarks.Text;
                _hdrMinus.Ith_session_id = Session["SessionID"].ToString();
                _hdrMinus.Ith_stus = "A";
                _hdrMinus.Ith_sub_tp = "SYS";
                _hdrMinus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "ADJ";
                _autonoMinus.Aut_number = 5;
                _autonoMinus.Aut_start_char = "ADJ";
                _autonoMinus.Aut_year = null;
                #endregion
                InventoryHeader _hdrPlus = new InventoryHeader();
                #region Fill InventoryHeader
                _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
                _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
                _hdrPlus.Ith_acc_no = "STATUS_CHANGE";
                _hdrPlus.Ith_anal_1 = "";
                _hdrPlus.Ith_anal_2 = "";
                _hdrPlus.Ith_anal_3 = "";
                _hdrPlus.Ith_anal_4 = "";
                _hdrPlus.Ith_anal_5 = "";
                _hdrPlus.Ith_anal_6 = 0;
                _hdrPlus.Ith_anal_7 = 0;
                _hdrPlus.Ith_anal_8 = DateTime.MinValue;
                _hdrPlus.Ith_anal_9 = DateTime.MinValue;
                _hdrPlus.Ith_anal_10 = false;
                _hdrPlus.Ith_anal_11 = false;
                _hdrPlus.Ith_anal_12 = false;
                _hdrPlus.Ith_bus_entity = "";
                _hdrPlus.Ith_cate_tp = "STTUS";
                _hdrPlus.Ith_com = Session["UserCompanyCode"].ToString();
                _hdrPlus.Ith_com_docno = "";
                _hdrPlus.Ith_cre_by = Session["UserID"].ToString();
                _hdrPlus.Ith_cre_when = DateTime.Now;
                _hdrPlus.Ith_del_add1 = "";
                _hdrPlus.Ith_del_add2 = "";
                _hdrPlus.Ith_del_code = "";
                _hdrPlus.Ith_del_party = "";
                _hdrPlus.Ith_del_town = "";
                _hdrPlus.Ith_direct = true;
                _hdrPlus.Ith_doc_date = Convert.ToDateTime(dtpDate.Text);
                _hdrPlus.Ith_doc_no = string.Empty;
                _hdrPlus.Ith_doc_tp = "ADJ";
                _hdrPlus.Ith_doc_year = dt2.Year;
                _hdrPlus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrPlus.Ith_entry_tp = "STTUS";
                _hdrPlus.Ith_git_close = true;
                _hdrPlus.Ith_git_close_date = DateTime.MinValue;
                _hdrPlus.Ith_git_close_doc = string.Empty;
                _hdrPlus.Ith_isprinted = false;
                _hdrPlus.Ith_is_manual = false;
                _hdrPlus.Ith_job_no = string.Empty;
                _hdrPlus.Ith_loading_point = string.Empty;
                _hdrPlus.Ith_loading_user = string.Empty;
                _hdrPlus.Ith_loc = Session["UserDefLoca"].ToString();
                _hdrPlus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrPlus.Ith_mod_by = Session["UserID"].ToString();
                _hdrPlus.Ith_mod_when = DateTime.Now;
                _hdrPlus.Ith_noofcopies = 0;
                _hdrPlus.Ith_oth_loc = string.Empty;
                _hdrPlus.Ith_oth_docno = "N/A";
                _hdrPlus.Ith_remarks = txtRemarks.Text;
                _hdrPlus.Ith_session_id = Session["SessionID"].ToString();
                _hdrPlus.Ith_stus = "A";
                _hdrPlus.Ith_sub_tp = "STTUS";
                _hdrPlus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoPlus.Aut_cate_tp = "LOC";
                _autonoPlus.Aut_direction = null;
                _autonoPlus.Aut_modify_dt = null;
                _autonoPlus.Aut_moduleid = "ADJ";
                _autonoPlus.Aut_number = 5;
                _autonoPlus.Aut_start_char = "ADJ";
                _autonoPlus.Aut_year = null;
                #endregion

                #region Status Change Adj- >>>> Adj+
                error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus);

                if ((string.IsNullOrEmpty(error)) || (error.Contains("|")))
                {
                    string okmsg = "Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus;


                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + okmsg + "')", true);

                    ClearAll();


                    //if (MessageBox.Show("Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus + "\nDo you want to print this?", "Process Completed : STTUS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    //    BaseCls.GlbReportTp = "OUTWARD";
                    //    Reports.Inventory.ReportViewerInventory _viewMinus = new Reports.Inventory.ReportViewerInventory();
                    //    if (Session["UserCompanyCode"].ToString() == "SGL") 
                    //        _viewMinus.GlbReportName = "SOutward_Docs.rpt";
                    //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") 
                    //        _viewMinus.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //    else _viewMinus.GlbReportName = "Outward_Docs.rpt";
                    //    _viewMinus.GlbReportDoc = documntNo_minus;
                    //    _viewMinus.Show();
                    //    _viewMinus = null;

                    //    BaseCls.GlbReportTp = "INWARD";
                    //    Reports.Inventory.ReportViewerInventory _viewPlus = new Reports.Inventory.ReportViewerInventory();
                    //    if (Session["UserCompanyCode"].ToString() == "SGL") 
                    //        _viewPlus.GlbReportName = "Inward_Docs.rpt";
                    //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") 
                    //        _viewPlus.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //    else _viewPlus.GlbReportName = "Inward_Docs.rpt";
                    //    _viewPlus.GlbReportDoc = documntNo_plus;
                    //    _viewPlus.Show();
                    //    _viewPlus = null;
                    //}
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    error = error.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                    if (error == "ERROR   ORA-02290  check constraint (EMS.CHK_INLFREEQTY) violated ORA-06512  at  EMS.SP_UPDATE_INRLOC , line 31 ORA-06512  at line 1")
                    {
                        string fronError = "Enterd items/Serials are reserved !";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + fronError + "')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + error + "')", true);
                    }
                    
                }

                #endregion
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            try
            {
                excelUpload.Show();
                //mpexcel.Show();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //protected void lbtnuploadexcel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fileupexcelupload.HasFile)
        //        {
        //            string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
        //            string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

        //            if (Extension != ".xls")
        //            {
        //                lblwarningEXCEL.Text = "Please select a valid excel (.xls) file !!!";
        //                mpexcel.Show();
        //                return;
        //            }

        //            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

        //            string FilePath = Server.MapPath(FolderPath + FileName);
        //            fileupexcelupload.SaveAs(FilePath);
        //            Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);

        //            string isserialduplicate = (string)Session["DUPLICATE_SERIAL"];

        //            if (!string.IsNullOrEmpty(isserialduplicate))
        //            {
        //                lblwarningEXCEL.Text = "Some serial/serials have been already processed !!!";
        //                mpexcel.Show();
        //                return;
        //            }

        //            SaveListInExcel();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an excel file !!!')", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //}


        private void SaveListInExcel()
        {
            try
            {
                Int32 seqno = 0;

                if (grdItems.Rows.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtUserSeqNo.Text.Trim()))
                {
                    seqno = GenerateNewUserSeqNo();
                    txtUserSeqNo.Text = seqno.ToString();
                }

                DataTable dt = (DataTable)ViewState["ITEMCHANGE"];

                if (dt.Rows.Count > 0)
                {
                    ScanItemListExcel = GlobalMethod.ToGenericList<InventoryRequestItem>(dt, InventoryRequestItem.ConverterForCommonOutCopy);
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemListExcel)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(seqno);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _reptitm.Tui_sup = _addedItem.Itri_mitm_cd;
                    _saveonly.Add(_reptitm);
                }

                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(seqno), string.Empty, string.Empty, 0, 3);

                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //AddItem(txtItemCode.Text, _itms.Mi_std_cost.ToString(), ddlStatus.SelectedItem.Value, txtQty.Text, (ViewState["userSeqNo"].ToString()), txtSerialI.Text);

                //AddSerials(txtItemCode.Text, txtSerialI.Text, (ViewState["userSeqNo"].ToString()));

                //foreach (DataRow ddr in dt.Rows)
                //{
                //    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                //    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    _reptPickSerial_.Tus_usrseq_no = Convert.ToInt32(seqno);
                //    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    _reptPickSerial_.Tus_base_doc_no = "";
                //    _reptPickSerial_.Tus_base_itm_line = 0;
                //    _reptPickSerial_.Tus_itm_desc = ddr["mi_longdesc"].ToString();
                //    _reptPickSerial_.Tus_itm_model = ddr["mi_model"].ToString();
                //    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                //    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                //    _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                //    _reptPickSerial_.Tus_itm_cd =  ddr["itri_itm_cd"].ToString();
                //    _reptPickSerial_.Tus_itm_stus = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_qty = Convert.ToDecimal(ddr["itri_app_qty"].ToString());
                //    _reptPickSerial_.Tus_ser_1 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_2 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_3 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_4 = "N/A";
                //    _reptPickSerial_.Tus_ser_id = 0;
                //    _reptPickSerial_.Tus_serial_id = "0";
                //    _reptPickSerial_.Tus_unit_cost = 0;
                //    _reptPickSerial_.Tus_unit_price = 0;
                //    _reptPickSerial_.Tus_unit_price = 0;
                //    _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;
                //    _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;
                //    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null); 
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls":
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                var lista = new List<string>();

                lista = (List<string>)Session["SERIAL_LIST_CSST"];

                if (lista != null)
                {
                    Seriallist = lista;
                }

                foreach (DataRow item in dt.Rows)
                {
                    string serial = item["Serial No"].ToString().Trim().ToUpper();
                    Session["DUPLICATE_SERIAL"] = null;
                    if (Seriallist.Contains(serial) == true)
                    {
                        Session["DUPLICATE_SERIAL"] = "YES";
                    }
                }

                string isserialduplicate = (string)Session["DUPLICATE_SERIAL"];

                if (!string.IsNullOrEmpty(isserialduplicate))
                {
                    return;
                }

                DataTable dtitemsexcel = new DataTable();
                dtitemsexcel.Columns.AddRange(new DataColumn[12] { new DataColumn("itri_itm_cd"), new DataColumn("itri_mitm_cd"), new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus_desc"), new DataColumn("itri_itm_stus"), new DataColumn("itri_note_desc"), new DataColumn("itri_note"), new DataColumn("itri_unit_price"), new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_line_no") });

                Int32 seqno = GenerateNewUserSeqNo();
                txtUserSeqNo.Text = seqno.ToString();

                foreach (DataRow item in dt.Rows)
                {
                    string _item = item["Item Code"].ToString();

                    List<string> duplicte = new List<string>();

                    foreach (GridViewRow grv in grdItems.Rows)
                    {
                        Label _itemlabel = (Label)grv.FindControl("lblitri_itm_cd");
                        duplicte.Add(_itemlabel.Text);
                    }

                    bool hasCar = duplicte.Contains(_item);

                    if (hasCar == true)
                    {
                        continue;
                    }


                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    decimal _price = _itemdetail.Mi_itmtot_cost;
                    int index = dt.Rows.IndexOf(item);

                    DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(item["Item Status"].ToString());
                    DataTable dtstatusnew = CHNLSVC.Sales.GetItemStatusVal(item["New Item Status"].ToString());

                    string _itemval = string.Empty;
                    string _itemvalnew = string.Empty;
                    string _itemvaldesc = string.Empty;
                    string _itemvalnewdesc = string.Empty;

                    foreach (DataRow itemval in dtstatus.Rows)
                    {
                        _itemval = itemval["MIS_CD"].ToString();
                    }

                    foreach (DataRow itemvalnew in dtstatusnew.Rows)
                    {
                        _itemvalnew = itemvalnew["MIS_CD"].ToString();
                    }


                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(_itemval);

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            _itemvaldesc = ddr2[0].ToString();
                        }
                    }

                    DataTable dtstatustx1 = CHNLSVC.Sales.GetItemStatusTxt(_itemvalnew);

                    if (dtstatustx1.Rows.Count > 0)
                    {
                        foreach (DataRow ddr3 in dtstatustx1.Rows)
                        {
                            _itemvalnewdesc = ddr3[0].ToString();
                        }
                    }

                    dtitemsexcel.Rows.Add(_item,item["New Item Code"], _description, _model, _itemvaldesc, _itemval, _itemvalnewdesc, _itemvalnew, _price, item["Qty"].ToString(), item["Qty"].ToString(), index + 1);

                    AddItem(_item, _price.ToString(), _itemval, item["Qty"].ToString(), seqno.ToString(), item["Serial No"].ToString(), _itemvalnew, item["New Item Code"].ToString(), index + 1);
                }


                DataTable dtitemsserexcel = new DataTable();
                //dtitemsserexcel.Columns.AddRange(new DataColumn[13] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus"), new DataColumn("tus_new_status"), new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), new DataColumn("tus_new_remarks"), new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line") });
                dtitemsserexcel.Columns.AddRange(new DataColumn[15] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus_desc"), new DataColumn("tus_itm_stus"), new DataColumn("tus_new_status_desc"), new DataColumn("tus_new_status"), new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), new DataColumn("tus_new_remarks"), new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line") });

                foreach (DataRow item in dt.Rows)
                {
                    string _item = item["Item Code"].ToString();

                    List<string> duplicteser = new List<string>();

                    foreach (GridViewRow grvser in grdSerial.Rows)
                    {
                        Label _itemlabelser = (Label)grvser.FindControl("lblitmgrd2");
                        duplicteser.Add(_itemlabelser.Text);
                    }

                    bool hasCarser = duplicteser.Contains(_item);

                    if (hasCarser == true)
                    {
                        continue;
                    }

                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    decimal _price = _itemdetail.Mi_itmtot_cost;
                    int index = dt.Rows.IndexOf(item);

                    DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(item["Item Status"].ToString());
                    DataTable dtstatusnew = CHNLSVC.Sales.GetItemStatusVal(item["New Item Status"].ToString());

                    string _itemval = string.Empty;
                    string _itemvalnew = string.Empty;
                    string _itemval2 = string.Empty;
                    string _itemvalnew2 = string.Empty;

                    foreach (DataRow itemval in dtstatus.Rows)
                    {
                        _itemval = itemval["MIS_CD"].ToString();
                    }

                    foreach (DataRow itemvalnew in dtstatusnew.Rows)
                    {
                        _itemvalnew = itemvalnew["MIS_CD"].ToString();
                    }

                    List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                    Int32 _serialid = 0;

                    foreach (ReptPickSerials serial in Tempserial_list)
                    {
                        if (serial.Tus_ser_1 == item["Serial No"].ToString())
                        {
                            _serialid = serial.Tus_ser_id;
                        }
                    }


                    DataTable dtstatustx12 = CHNLSVC.Sales.GetItemStatusTxt(_itemval);

                    if (dtstatustx12.Rows.Count > 0)
                    {
                        foreach (DataRow ddr5 in dtstatustx12.Rows)
                        {
                            _itemval2 = ddr5[0].ToString();
                        }
                    }

                    DataTable dtstatustx13 = CHNLSVC.Sales.GetItemStatusTxt(_itemvalnew);

                    if (dtstatustx13.Rows.Count > 0)
                    {
                        foreach (DataRow ddr in dtstatustx13.Rows)
                        {
                            _itemvalnew2 = ddr[0].ToString();
                        }
                    }

                    var list = new List<string>();

                    list = (List<string>)Session["SERIAL_LIST_CSST"];

                    if (list != null)
                    {
                        Seriallist = list;
                    }

                    if (!string.IsNullOrEmpty(item["Serial No"].ToString().Trim().ToUpper()))
                    {
                        Seriallist.Add(item["Serial No"].ToString().Trim().ToUpper());  
                    }
                    
                    Session["SERIAL_LIST_CSST"] = Seriallist;
                    
                    dtitemsserexcel.Rows.Add(_item, _model, _itemval2,_itemval,_itemvalnew2, _itemvalnew, item["Qty"].ToString(), item["Serial No"].ToString(), "", "", item["Bin"].ToString(), "", _serialid, seqno, index + 1);
                    AddSerials(_item, item["Serial No"].ToString(), seqno.ToString(), _itemvalnew, item["New Item Code"].ToString(), item["Bin"].ToString(), index + 1, _itemval, Convert.ToDecimal(item["Qty"].ToString()));
                }

                connExcel.Close();

                DataTable dtcopy = new DataTable();
                DataTable dtoriginal = new DataTable();
                DataTable dtvwstatetbl = ViewState["ITEMCHANGE"] as DataTable;

                if ((ViewState["ITEMCHANGE"] != null) && (dtvwstatetbl.Rows.Count != 0))
                {
                    dtcopy = ViewState["ITEMCHANGE"] as DataTable;
                    dtcopy.Merge(dtitemsexcel, true, MissingSchemaAction.Ignore);
                    ViewState["ITEMCHANGE"] = dtcopy;

                    grdItems.DataSource = dtcopy;
                    grdItems.DataBind();
                }
                else
                {
                    dtoriginal = dtitemsexcel.Copy();
                    ViewState["ITEMCHANGE"] = dtoriginal;

                    grdItems.DataSource = dtoriginal;
                    grdItems.DataBind();
                }

                DataTable dtcopySerial = new DataTable();
                DataTable dtoriginalSerial = new DataTable();
                DataTable dtvwstatetblSerial = ViewState["ITEMCHANGESER"] as DataTable;

                if ((ViewState["ITEMCHANGESER"] != null) && (dtvwstatetblSerial.Rows.Count != 0))
                {
                    dtcopySerial = ViewState["ITEMCHANGESER"] as DataTable;
                    dtcopySerial.Merge(dtitemsserexcel, true, MissingSchemaAction.Ignore);
                    ViewState["ITEMCHANGESER"] = dtcopySerial;

                    grdSerial.DataSource = dtcopySerial;
                    grdSerial.DataBind();
                }
                else
                {
                    dtoriginalSerial = dtitemsserexcel.Copy();
                    ViewState["ITEMCHANGESER"] = dtoriginalSerial;

                    grdSerial.DataSource = dtoriginalSerial;
                    grdSerial.DataBind();
                }
                Session["EXCEL"] = "Yes";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial, string _newstatus, string _newitem, Int32 LineNo)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;

                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = Convert.ToInt32(LineNo);
                        _itm.Itri_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);

                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                        _itm.Itri_note = _newstatus;
                        _itm.Itri_mitm_cd = _newitem;

                        ScanItemList.Add(_itm);
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = Convert.ToInt32(LineNo);
                        _itm.Itri_qty = Convert.ToDecimal(_qty);

                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);

                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                        _itm.Itri_note = _newstatus;
                        _itm.Itri_mitm_cd = _newitem;

                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = Convert.ToInt32(LineNo);
                    _itm.Itri_qty = Convert.ToDecimal(_qty);

                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_bqty = Convert.ToDecimal(_qty);

                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                    _itm.Itri_note = _newstatus;
                    _itm.Itri_mitm_cd = _newitem;

                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                 _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(_UserSeqNo);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_mitm_cd);
                    _reptitm.Tui_pic_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
               // CHNLSVC.Inventory.SavePickedItems(_saveonly);
                ViewState["ScanItemList"] = ScanItemList;
                ItemList = ScanItemList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void AddSerials(string _item, string _Serial, string _Seqno, string _newstatus, string _newitem, string _bin, Int32 _lineno, string _curstus, decimal _qty,int serialId=0)
        {
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();


            if (msitem.Mi_is_ser1 == 1)
            {
                Tempserial_list = new List<ReptPickSerials>();
                Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);

                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(_Serial))
                {
                    _Serial = "N/A";
                }
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                Tempserial_list = new List<ReptPickSerials>();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_seq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                _reptPickSerial_.Tus_base_itm_line = 0;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = _bin;
                _reptPickSerial_.Tus_itm_cd = _item;
                _reptPickSerial_.Tus_itm_stus = _curstus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                _reptPickSerial_.Tus_ser_1 = _Serial;
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = serialId;
                _reptPickSerial_.Tus_serial_id = "0";
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_job_no = "";
                _reptPickSerial_.Tus_job_line = _lineno;
                _reptPickSerial_.Tus_new_status = _newstatus;
                Tempserial_list.Add(_reptPickSerial_);
            }

            ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;

            //var serialLine = (from _ls in ScanItemList
            //                  where _ls.Itri_itm_cd == _item
            //                  select _ls.Itri_line_no).Max();


            if (_Serial == "N/A" || _Serial == "")
            {

            }
            Int32 user_seq_num = -1;
           
                foreach (ReptPickSerials serial in Tempserial_list)
                {
                    #region PRN
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ-S", Session["UserCompanyCode"].ToString(), _Seqno, 0);
                    if (user_seq_num != -1)
                    {
                        generated_seq = user_seq_num;
                    }
                    else
                    {
                        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ-S", 1, Session["UserCompanyCode"].ToString());

                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "ADJ-S";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = generated_seq;
                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = _Seqno;
                        RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }

                    if (msitem.Mi_is_ser1 != -1)
                    {
                        int rowCount = 0;


                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));

                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_.Tus_seq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                        _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                        _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                        _reptPickSerial_.Tus_base_doc_no = _Seqno;
                        _reptPickSerial_.Tus_itm_cd = _item;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serial.Tus_itm_line);
                        _reptPickSerial_.Tus_itm_line = Convert.ToInt32(serial.Tus_itm_line);  //Convert.ToInt32(_lineno);
                        if (_reptPickSerial_.Tus_ser_2 == "")
                        {
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                        }
                        _reptPickSerial_.Tus_bin = _bin;
                        _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                        _reptPickSerial_.Tus_ser_1 = _Serial;
                        _reptPickSerial_.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_new_itm_cd = _newitem;//newitemCode;
                        _reptPickSerial_.Tus_new_status = _newstatus;//newItemStatus;
                        _reptPickSerial_.Tus_qty = _qty;

                        //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        _ReptPickSerials.Add(_reptPickSerial_);
                        rowCount++;
                    }
                    else
                    {
                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_.Tus_seq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_base_doc_no = _Seqno;
                        _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_lineno); //Convert.ToInt32(serial.Tus_itm_line);
                        _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_lineno);//Convert.ToInt32(serial.Tus_itm_line);
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                        _reptPickSerial_.Tus_bin = _bin;
                        _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                        _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                        _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                        _reptPickSerial_.Tus_ser_1 = _Serial;
                        _reptPickSerial_.Tus_ser_2 = "N/A";
                        _reptPickSerial_.Tus_ser_3 = "N/A";
                        _reptPickSerial_.Tus_ser_4 = "N/A";
                        _reptPickSerial_.Tus_ser_id = serialId;
                        _reptPickSerial_.Tus_serial_id = "0";
                        _reptPickSerial_.Tus_unit_cost = 0;
                        _reptPickSerial_.Tus_unit_price = 0;
                        _reptPickSerial_.Tus_unit_price = 0;
                        _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                        _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                        _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                        _reptPickSerial_.Tus_qty = _qty;
                        _reptPickSerial_.Tus_new_itm_cd = _newitem;//newitemCode;
                        _reptPickSerial_.Tus_new_status = _newstatus;//newItemStatus;

                        //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        _ReptPickSerials.Add(_reptPickSerial_);
                    }






                    #endregion
                }
           
        
            
            ViewState["SerialList"] = Tempserial_list;
        }

        protected void lbtndeleteserial_Click(object sender, EventArgs e)
        {
            if (txtconfirmdeleteSerial.Value == "Yes")
            {
                try
                {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    string _item = string.Empty;
                    string _itmStatus = string.Empty;
                    string _serialID = string.Empty;
                    string _bin = string.Empty;
                    string _serial_1 = string.Empty;
                    string _requestno = string.Empty;
                    string _lineNo = string.Empty;

                    if (row != null)
                    {
                        _item = (row.FindControl("lblitmgrd2") as Label).Text;
                        _itmStatus = (row.FindControl("lblstusgrd2") as Label).Text;
                        _serialID = (row.FindControl("lblseridgrd2") as Label).Text;
                        _bin = (row.FindControl("lblbingrd") as Label).Text;
                        _serial_1 = (row.FindControl("lblser1grd2") as Label).Text;
                        _requestno = (row.FindControl("lblrequest2") as Label).Text;
                        _lineNo = (row.FindControl("lblbaseline") as Label).Text;
                    }

                    if (string.IsNullOrEmpty(_item))
                    {
                        return;
                    }

                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(),Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _item, null);
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, Convert.ToInt32(_serialID), 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus);
                    } 
                    LoadItems(txtUserSeqNo.Text);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        private string SetNewItemStatus(List<ReptPickSerials> _serList, String ItemCode, String ItemStatus)
        {
            string NewItemStatus = string.Empty;

            if (_serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus).Count > 0)
            {
                NewItemStatus = _serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus)[0].Tus_new_status;
            }

            return NewItemStatus;
        }

        private string getItemStatusDesc(string stis)
        {
            List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
            stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            return stis;
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }


        //new excelupload code by rukshan
        protected void btnprocess_Click(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {

                    Label3.Visible = true;
                    Label3.Text = "Please select a valid excel (.xls) file";
                    DisplayMessage("Please select a valid excel (.xls) file", 2);
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                string value = string.Empty;
                ExcelProcess(FilePath, 1, out value);
                if (value == "1")
                {
                    DisplayMessage("Excel file upload completed. Please save data", 1);
                    Label3.Visible = false;
                    lblsuccess2.Visible = true;
                    lblsuccess2.Text = "Excel file upload completed. Please save data";
                    pnlupload.Visible = false;
                    excelUpload.Show();
                }
                else if (value == "2")
                {
                    DisplayMessage("This excel sheet contains empty values please check again", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "This excel sheet contains empty values please check again";

                    excelUpload.Show();

                }
                else if (value == "3")
                {
                    DisplayMessage("Excel  Data Invalid Please check Excel File and Upload", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";

                    excelUpload.Show();
                }
                else if (value == "4")
                {
                    DisplayMessage("Please Check Serial  number", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Please Check Serial  number";

                    excelUpload.Show();
                }

                else if (value == "5")
                {
                    DisplayMessage("Cannot change status from another location", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Cannot change status from another location";

                    excelUpload.Show();
                }
                else if (value == "6")
                {
                    DisplayMessage("No qty availabe for given status", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "No qty availabe for given status";

                    excelUpload.Show();
                }
                else
                {

                    DisplayMessage("Excel contains duplicate records please check", 2);
                    Label3.Visible = true;
                    Label3.Text = "Excel contains duplicate records please check";
                    excelUpload.Show();
                }
            }
            else
            {
                DisplayMessage("Please select the correct upload file path", 2);
                Label3.Visible = true;
                Label3.Text = "Please select the correct upload file path";
                excelUpload.Show();

            }
        }
        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }
      
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }

        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }
        private void loadItemStatus_new()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }
        private List<InventoryRequestItem> ItemDes(List<InventoryRequestItem> _itm)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (InventoryRequestItem item in _itm)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Itri_itm_stus);
                    if (oStatus != null)
                    {
                        item.Itri_itm_stus_desc = oStatus.Mis_desc;
                        item.Itri_note_desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Itri_itm_stus_desc = item.Mi_itm_stus;
                        item.Itri_note_desc = item.Mi_itm_stus;
                    }
                    MasterItemStatus oStatus2 = oMasterItemStatuss.Find(x => x.Mis_cd == item.Itri_note);
                    if (oStatus2 != null)
                    {
                        item.Itri_note_desc = oStatus2.Mis_desc;
                    }
                    else
                    {
                        item.Itri_note_desc = item.Mi_itm_stus;
                    }
                }
               
            }
            return _itm;
        }

        private List<ReptPickSerials> SerialDes(List<ReptPickSerials> _serial)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (ReptPickSerials item in _serial)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Tus_itm_stus);
                    if (oStatus != null)
                    {
                        item.Tus_itm_stus_Desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Tus_itm_stus_Desc = item.Tus_itm_stus;
                    }
                    MasterItemStatus oStatus2 = oMasterItemStatuss.Find(x => x.Mis_cd == item.Tus_new_status);
                    if (oStatus2 != null)
                    {
                        item.Tus_new_status_Desc = oStatus2.Mis_desc;
                    }
                    else
                    {
                        //item.Tus_new_status_Desc = item.Tus_itm_stus;
                    }
                }

            }
            return _serial;
        }
        private void ExcelProcess(string FilePath, int option, out string value)
        {
            Int32 seqno = -1;
            DataTable[] GetExecelTbl = LoadData(FilePath);
            _ReptPickSerials = new List<ReptPickSerials>();
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                   
                        // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {

                                string _item = GetExecelTbl[0].Rows[i][0].ToString();
                                string _bin = GetExecelTbl[0].Rows[i][1].ToString();
                                string _status = GetExecelTbl[0].Rows[i][2].ToString();
                                string _serial = GetExecelTbl[0].Rows[i][3].ToString();
                                string _qty = GetExecelTbl[0].Rows[i][4].ToString();
                                string _newstatus = GetExecelTbl[0].Rows[i][5].ToString();
                                string _newitem = GetExecelTbl[0].Rows[i][6].ToString();
                                string _location = GetExecelTbl[0].Rows[i][7].ToString();
                                Int32 _serialid = 0;
                                if (seqno == -1)
                                {
                                    seqno = GenerateNewUserSeqNo();
                                }
                               
                                txtUserSeqNo.Text = seqno.ToString();
                                  MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == _status);
                                  if (oStatus != null)
                                  {

                                      _status = oStatus.Mis_cd;
                                  }
                                  MasterItemStatus oStatus2 = oMasterItemStatuss.Find(x => x.Mis_desc == _newstatus);
                                  if (oStatus2 != null)
                                  {
                                      _newstatus = oStatus2.Mis_cd;
                                  }
                                  
                                MasterItem _itemdetail = new MasterItem();
                                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                string _description = _itemdetail.Mi_longdesc;
                                string _model = _itemdetail.Mi_model;
                                decimal _price = _itemdetail.Mi_itmtot_cost;
                                if (Session["UserDefLoca"].ToString()!= _location )
                                {
                                    DisplayMessage("Cannot change status from another location", 2);
                                    Label3.Visible = true;
                                    Label3.Text = "Cannot change status from another location";
                                    excelUpload.Show();
                                    value = "5";
                                    return;
                                }
                                else { 

                                List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                                if (Tempserial_list != null)
                                {
                                    if (Tempserial_list.Count > 0)
                                    {
                                        if (_itemdetail.Mi_is_ser1 == 1)
                                        {
                                            _serialid = Tempserial_list.Find(x => x.Tus_itm_cd == _item && x.Tus_ser_1 == _serial).Tus_ser_id;
                                        }
                                        else
                                        {
                                            List<ReptPickSerials> varList =  Tempserial_list.Where(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _status).ToList();
                                            if(varList != null)
                                            {
                                                if (varList.Count > 0)
                                                {
                                                    _serialid = Tempserial_list.Find(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _status).Tus_ser_id;
                                                }
                                                else
                                                {
                                                    value = "6";
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                value = "6";
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        value = "4";
                                        return;
                                    }
                                }
                                else
                                {
                                    value = "2";
                                    return;
                                }

                                AddItem(_item, _price.ToString(), _status, _qty, seqno.ToString(), _serial, _newstatus, _newitem, i);
                                AddSerials(_item, _serial, seqno.ToString(), _newstatus, _newitem, _bin, i, _status, Convert.ToDecimal(_qty), _serialid);
                                }
                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }

                        }

                        if (_ReptPickSerials.Count > 0)
                        {
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials_Excel(_ReptPickSerials, _saveonly, null);
                            if (affected_rows == 1)
                            {
                                value = "1";
                            }
                        }

                        ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                        grdItems.DataSource = ItemDes(ScanItemList.Distinct().ToList());
                        grdItems.DataBind();
                        List<ReptPickSerials> _serials = new List<ReptPickSerials>();
                        _serials = new List<ReptPickSerials>();
                        _serials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), seqno, "ADJ-S");
                        Session["EXCEL"] = "Yes";
                        grdSerial.DataSource =SerialDes(_serials);
                        grdSerial.DataBind();
                    // ViewState["_lstTaxDet"] = _lstTaxDet;

                }
            }
            value = "1";
        }

        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                string Doc = txtDocumentNo.Text;
                txtDocumentNo.Text = Doc;
                    lblvalue.Text = "";
                    Session["CSSDOC"] = Doc;
                    txtDocumentNo_Leave();
                    UserPopoup.Hide();
                  
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

  
    }


}