using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class InterCompanyInWardEntry : BasePage
    {
        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
        List<ReptPickSerials> _repSerList = new List<ReptPickSerials>();
        List<ReptPickItems> _repItmList = new List<ReptPickItems>();
        private MasterLocationNew _mstLocNew = new MasterLocationNew();
        private bool _partialSave
        {
            get { if (Session["_partialSave"] != null) { return (bool)Session["_partialSave"]; } else { return false; } }
            set { Session["_partialSave"] = value; }
        }
        private List<MasterItemStatus> _stsList
        {
            get { if (Session["_stsList"] != null) { return (List<MasterItemStatus>)Session["_stsList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_stsList"] = value; }
        }

        private List<ReptPickItems> _aodInPartData
        {
            get { if (Session["_aodInPartData"] != null) { return (List<ReptPickItems>)Session["_aodInPartData"]; } else { return new List<ReptPickItems>(); } }
            set { Session["_aodInPartData"] = value; }
        }
        private List<InventorySubSerialMaster> _invSubSerList
        {
            get { if (Session["_invSubSerList"] != null) { return (List<InventorySubSerialMaster>)Session["_invSubSerList"]; } else { return new List<InventorySubSerialMaster>(); } }
            set { Session["_invSubSerList"] = value; }
        }
        private List<MasterItemStatus> oMasterItemStatuss
        {
            get { if (Session["oMasterItemStatuss"] != null) { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["oMasterItemStatuss"] = value; }
        }
        private string _docNo
        {
            get { if (Session["_docNo"] != null) { return (string)Session["_docNo"]; } else { return ""; } }
            set { Session["_docNo"] = value; }
        }
        private bool _showPopSendToPda
        {
            get { if (Session["_showPopSendToPda"] != null) { return (bool)Session["_showPopSendToPda"]; } else { return false; } }
            set { Session["_showPopSendToPda"] = value; }
        }
        private bool _showPopSendPart
        {
            get { if (Session["_showPopSendPart"] != null) { return (bool)Session["_showPopSendPart"]; } else { return false; } }
            set { Session["_showPopSendPart"] = value; }
        }
        private DataTable _dtPendingDoc
        {
            get { if (Session["_dtPendingDoc"] != null) { return (DataTable)Session["_dtPendingDoc"]; } else { return new DataTable(); } }
            set { Session["_dtPendingDoc"] = value; }
        }
        private MasterItem _masterItem = null;
        const string InvoiceBackDateName = "INTERCOMPANYINWARDENTRY";
        private string OutwardNo = string.Empty;
        private string OutwardType = string.Empty;
        private string OutwardCompany = string.Empty;
        private string OutwardLocation = string.Empty;
        private Int32 UserSeqNo = 0;
        private List<ReptPickSerials> PickSerialsList = null;
        private string hdnAllowBin = "0";
        private DateTime? hdnOutwarddate = null;
        string _supplier = string.Empty; string _subdoc = string.Empty;
        string _dono = string.Empty;
        string _userid = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private List<ReptPickSerials> _POPUPSERIALLIST = null;
        Int32 valitem = 0;
        private void BindRequestTypesDDLData()
        {
            try
            {
                List<MasterType> _masterType = CHNLSVC.General.GetOutwardTypes();
                if ((_masterType != null) && (_masterType.Count > 0))
                {
                    var _lst = _masterType.OrderBy(items => items.Mtp_desc).ToList();
                    ddlType.DataSource = _lst;
                    ddlType.DataTextField = "Mtp_desc";
                    ddlType.DataValueField = "Mtp_cd";
                    ddlType.DataBind();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    chkPendingDoc.Checked = false;
                    BindLodingBay();
                    BindLodingBayPartAodIn();
                    _dtPendingDoc = new DataTable();
                    _showPopSendToPda = false;
                    _showPopSendPart = false;
                    chkAODoutserials.Checked = false;
                    chkpda.Checked = false;
                    _stsList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    //if (Session["GlbDefaultBin"] == null)
                    //{
                    //    lblbinMssg.Text = "BIN  is not allocate for your location.";
                    //    SbuPopup.Show();
                    //    return;
                    //}
                    _docNo = "";
                    Session["popSerVar"] = null;
                    DateTime fromdate = DateTime.Now.AddDays(-7);
                    txtFrom.Text = fromdate.ToString("dd/MMM/yyyy");

                    DateTime todate = DateTime.Now;
                    txtTo.Text = todate.ToString("dd/MMM/yyyy");

                    DateTime date = DateTime.Now;
                    txtdate.Text = date.ToString("dd/MMM/yyyy");

                    gvItem.DataSource = new int[] { };
                    gvItem.DataBind();

                    gvSerial.DataSource = new int[] { };
                    Session["gvSerData"] = new int[] { };
                    gvSerial.DataBind();
                    Session["gvSerData"] = null;
                    gvPending.DataSource = new int[] { };
                    gvPending.DataBind();

                    gventryserials.DataSource = new int[] { };
                    gventryserials.DataBind();
                    BindGridPopup();
                    //back Date
                    BackDatefucntion();

                    OutwardType = string.Empty;
                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_mstLoc != null)
                    {
                        if (_mstLoc.Ml_allow_bin == false)
                        {
                            hdnAllowBin = "0";
                        }
                        else
                        {
                            hdnAllowBin = "1";
                        }
                    }
                    string _defBin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (!string.IsNullOrEmpty(_defBin))
                    {
                        hdnAllowBin = _defBin;
                    }
                    else
                    {
                        string msg1 = "Default Bin Not Setup For Location : " + Session["UserDefLoca"].ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg1 + "')", true);
                        Label5.Text = msg1;
                        SbuPopup.Show();
                        return;
                    }
                    BindRequestTypesDDLData();
                    BindOutwardListGridData();
                    hdnOutwarddate = null;
                    Session["BIN"] = hdnAllowBin;

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16019))
                    {
                        lbtnapprove.Enabled = false;
                        lbtnapprove.CssClass = "buttoncolor";
                        lbtnapprove.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbtnapprove.Enabled = true;
                        lbtnapprove.CssClass = "buttonUndocolor";
                        lbtnapprove.OnClientClick = "ConfirmApprove();";
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16020))
                    {
                        lbtnreject.Enabled = false;
                        lbtnreject.CssClass = "buttoncolor";
                        lbtnreject.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbtnreject.Enabled = true;
                        lbtnreject.CssClass = "buttonUndocolor";
                        lbtnreject.OnClientClick = "ConfirmReject();";
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16021))
                    {
                        lbtncancel.Enabled = false;
                        lbtncancel.CssClass = "buttoncolor";
                        lbtncancel.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbtncancel.Enabled = true;
                        lbtncancel.CssClass = "buttonUndocolor";
                        lbtncancel.OnClientClick = "ConfirmCancel();";
                    }
                    Session["documntNo"] = "";
                    Session["POPUP_LOADED"] = null;
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16022))
                    //{
                    //    btnSave.Enabled = false;
                    //    btnSave.CssClass = "buttoncolor";
                    //    btnSave.OnClientClick = "return Enable();";
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                    //    btnSave.CssClass = "buttonUndocolor";
                    //    btnSave.OnClientClick = "ConfirmSave();";
                    //}

                    //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16033))
                    //{
                    //    LinkButton1.Enabled = false;
                    //    LinkButton1.CssClass = "buttoncolor";
                    //    LinkButton1.OnClientClick = "return Enable();";
                    //}
                    //else
                    //{
                    //    LinkButton1.Enabled = true;
                    //    LinkButton1.CssClass = "buttonUndocolor";
                    //    LinkButton1.OnClientClick = "ConfirmSave();";
                    //}

                    foreach (GridViewRow hiderowbtn in this.gvSerial.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndeleteserial");

                        _delbutton.Enabled = true;
                        _delbutton.CssClass = "buttonUndocolor";
                        _delbutton.OnClientClick = "ConfirmDelete();";
                    }

                    DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    Session.Add("WH_AVI", dtchk_warehouse_availability);
                    if (dtchk_warehouse_availability.Rows.Count > 0)
                    {
                        chkpda.Enabled = true;
                        lbtnAllToPda.Enabled = true;
                        lbtnAllToPda.CssClass = "buttonUndocolorLeft";
                        lbtnAllToPda.OnClientClick = "return ConfSend();";
                        foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                        {
                            Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                            Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                        }
                    }
                    else
                    {
                        chkpda.Enabled = false;
                        lbtnAllToPda.Enabled = false;
                        lbtnAllToPda.CssClass = "buttoncolorleft";
                        lbtnAllToPda.OnClientClick = "return Enable();";
                    }

                    PopulateLoadingBays();

                    bool _allowCurrentTrans = false;
                    LinkButton btntest = new LinkButton();
                    IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_StockTransferInward", "", btntest, lblBackDateInfor, "m_Trans_Inventory_StockTransferInward", out _allowCurrentTrans);

                    DataTable dtpda = CHNLSVC.Inventory.CheckIsPDALoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    Session.Add("IsPDALoc", dtpda);
                    if (dtpda.Rows.Count > 0)
                    {
                        foreach (DataRow ddrpda in dtpda.Rows)
                        {
                            Session["PDA_LOCATION"] = ddrpda["ML_IS_PDA"].ToString();
                            if (ddrpda["ML_IS_PDA"].ToString() == "1")
                            {
                                chkpda.Enabled = true;
                                lbtnAllToPda.Enabled = true;
                                lbtnAllToPda.CssClass = "buttonUndocolorLeft";
                                lbtnAllToPda.OnClientClick = "return ConfSend();";
                            }
                            else
                            {
                                chkpda.Enabled = false;
                                lbtnAllToPda.Enabled = false;
                                lbtnAllToPda.CssClass = "buttoncolorleft";
                                lbtnAllToPda.OnClientClick = "return Enable();";
                            }
                        }
                    }
                }
                else
                {
                    if (Session["popSerVar"] != null)
                    {
                        if (Session["popSerVar"] == "Show")
                        {
                            popSerVar.Show();
                        }
                        else
                        {
                            popSerVar.Hide();
                            Session["popSerVar"] = "Hide";
                        }
                    }
                    if (_showPopSendToPda)
                    {
                        popSendToPda.Show();
                    }
                    else
                    {
                        popSendToPda.Hide();
                    }
                    if (_showPopSendPart)
                    {
                        popSendToPdaPart.Show();
                    }
                    else
                    {
                        popSendToPdaPart.Hide();
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

        private void BackDatefucntion()
        {
            bool _allowCurrentTrans = false;
            txtdate.Enabled = false;
            lbtndate.Visible = false;
            Session["GlbModuleName"] = "m_Trans_Inventory_StockTransferInward";
            LinkButton btntest = new LinkButton();
            if (IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_StockTransferInward", "", btntest, lblBackDateInfor, "m_Trans_Inventory_StockTransferInward", out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtdate.Text).Date != DateTime.Now.Date)
                    {
                        txtdate.Enabled = true;
                        string _Msg = "Back date is not allowed !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtdate.Focus();
                        return;
                    }
                }
                else
                {
                    txtdate.Enabled = true;
                    string _Msg = "Back date is not allowed !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    txtdate.Focus();
                    return;
                }
            }
        }

        protected void SearchRequest()
        {
            try
            {
                if ((string.IsNullOrEmpty(txtFrom.Text)) && (string.IsNullOrEmpty(txtTo.Text)))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the date range !!!')", true);
                    return;
                }

                DateTime fromdate = Convert.ToDateTime(txtFrom.Text);
                DateTime todate = Convert.ToDateTime(txtTo.Text);

                if (fromdate > todate)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid date range !!!')", true);
                    return;
                }

                BindOutwardListGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void BindOutwardListGridData()
        {
            try
            {
                DataTable _table = new DataTable();
                DataTable _table2 = new DataTable();
                InventoryHeader _inventoryRequest = new InventoryHeader();
                _inventoryRequest.Ith_oth_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Ith_doc_tp = ddlType.SelectedValue.ToString() == "-1" ? string.Empty : ddlType.SelectedValue.ToString();
                _inventoryRequest.Ith_oth_loc = Session["UserDefLoca"].ToString();
                _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
                _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
                if (!string.IsNullOrEmpty(_inventoryRequest.Ith_doc_tp))
                { if (_inventoryRequest.Ith_doc_tp == "AOD-") _inventoryRequest.Ith_doc_tp = ddlType.SelectedValue; }

                gvPending.DataSource = null;
                gvPending.DataBind();
                gvItem.DataSource = null;
                gvItem.DataBind();
                BindGridStatusItem();
                gvSerial.DataSource = null;
                Session["gvSerData"] = new int[] { };
                gvSerial.DataBind();

                if (string.IsNullOrEmpty(txtlocation.Text.Trim()))
                {
                    //_inventoryRequest.Ith_oth_loc = txtlocation.Text.Trim().ToUpper();
                    _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable(_inventoryRequest);
                    _dtPendingDoc = _table;
                }
                else
                {
                    _inventoryRequest.Ith_loc = txtlocation.Text.Trim().ToUpper();
                    _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsWeb(_inventoryRequest);
                }

                if (_table.Rows.Count <= 0)
                {
                    var _tblItems = from dr in _table.AsEnumerable() group dr by new { ith_doc_no = dr["ith_doc_no"], ith_doc_date = dr["ith_doc_date"], ith_doc_tp = dr["ith_doc_tp"], ith_manual_ref = dr["ith_manual_ref"], ith_com = dr["ith_com"], ith_loc = dr["ith_loc"], ith_bus_entity = dr["ith_bus_entity"], ith_sub_docno = dr["ith_sub_docno"] } into item select new { ith_doc_no = item.Key.ith_doc_no, ith_doc_date = item.Key.ith_doc_date, ith_doc_tp = item.Key.ith_doc_tp, ith_manual_ref = item.Key.ith_manual_ref, ith_com = item.Key.ith_com, ith_loc = item.Key.ith_loc, ith_bus_entity = item.Key.ith_bus_entity, ith_sub_docno = item.Key.ith_sub_docno };
                    gvPending.DataSource = _tblItems;
                    //_dtPendingDoc = (DataTable)gvPending.DataSource;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtlocation.Text.Trim()))
                    {
                        _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsTable2(_inventoryRequest);
                        _dtPendingDoc = _table;
                    }
                    else
                    {
                        _inventoryRequest.Ith_loc = txtlocation.Text.Trim();
                        _table = CHNLSVC.Inventory.GetAllPendingInventoryOutwardsWeb(_inventoryRequest);
                        _dtPendingDoc = _table;
                    }

                    if (!string.IsNullOrEmpty(txtAODNumber.Text.Trim()))
                    {
                        string soundExCode = SoundEx(txtAODNumber.Text.Trim());

                        EnumerableRowCollection<DataRow> query = from contact in _table.AsEnumerable()
                                                                 where SoundEx(contact.Field<string>("ITH_DOC_NO")) == soundExCode
                                                                 select contact;

                        DataView view = query.AsDataView();
                        _table = view.ToTable();
                        _dtPendingDoc = _table;
                    }

                    gvPending.DataSource = null;
                    gvPending.DataBind();
                    if (_table.Rows.Count > 0)
                    {
                        DataView dv = _table.DefaultView;
                        dv.Sort = "ith_doc_date,ith_doc_no DESC";
                        _table = dv.ToTable();
                    }

                    if (_table.Rows.Count > 0)
                    {
                        DataView dv = _table.DefaultView;
                        dv.Sort = "ith_doc_date DESC";
                        _table = dv.ToTable();
                    }
                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_mstLoc.Ml_is_pda)
                    {
                        #region Add by Lakshan 21 Sep 2016 remove pending doc
                        if (!_table.Columns.Contains("TMP_Tuh_fin_stus"))
                        {
                            _table.Columns.Add("TMP_Tuh_fin_stus", typeof(Int32));
                        }
                        if (chkPendingDoc.Checked)
                        {
                            if (!_table.Columns.Contains("TMP_Tuh_fin_time"))
                            {
                                _table.Columns.Add("TMP_Tuh_fin_time", typeof(DateTime));
                            }

                            for (int x = _table.Rows.Count - 1; x >= 0; x--)
                            {
                                DataRow dr = _table.Rows[x];
                                _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_WITH_COMPLETE_DATE(new ReptPickHeader()
                                {
                                    Tuh_doc_no = dr["ith_doc_no"].ToString(),
                                    Tuh_doc_tp = "AOD",
                                    Tuh_direct = true,
                                    Tuh_usr_com = Session["UserCompanyCode"].ToString()
                                }).FirstOrDefault();
                                if (_tmpPickHdr != null)
                                {
                                    if (_tmpPickHdr.Tuh_fin_stus != 1)
                                    {

                                        dr.Delete();
                                    }
                                    else
                                    {
                                        dr["TMP_Tuh_fin_time"] = _tmpPickHdr.Tuh_fin_time;
                                        dr["TMP_Tuh_fin_stus"] = _tmpPickHdr.Tuh_fin_stus;
                                    }
                                }
                                else
                                {
                                    dr.Delete();
                                }
                            }
                            _table.AcceptChanges();
                            if (_table.Rows.Count > 0)
                            {
                                DataView dv = _table.DefaultView;
                                dv.Sort = "TMP_Tuh_fin_time ASC";
                                _table = dv.ToTable();
                            }
                        }
                        if (!chkPendingDoc.Checked)
                        {
                            for (int x = _table.Rows.Count - 1; x >= 0; x--)
                            {
                                DataRow dr = _table.Rows[x];
                                _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                                {
                                    Tuh_doc_no = dr["ith_doc_no"].ToString(),
                                    Tuh_doc_tp = "AOD",
                                    Tuh_direct = true,
                                    Tuh_usr_com = Session["UserCompanyCode"].ToString()
                                }).FirstOrDefault();
                                if (_tmpPickHdr != null)
                                {
                                    if (_tmpPickHdr.Tuh_fin_stus == 1)
                                    {
                                        dr["TMP_Tuh_fin_stus"] = _tmpPickHdr.Tuh_fin_stus;
                                    }
                                }
                            }
                            //_table.AcceptChanges();
                        }
                        #endregion
                    }
                    else
                    {
                        if (!_table.Columns.Contains("TMP_Tuh_fin_stus"))
                        {
                            _table.Columns.Add("TMP_Tuh_fin_stus", typeof(Int32));
                        }
                    }
                    gvPending.DataSource = _table;
                    _dtPendingDoc = _table;
                    gvPending.DataBind();
                }
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _table2 = new DataTable();
                _table2 = CHNLSVC.Inventory.GetAllScanSerials(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, OutwardType);
                if (_table.Rows.Count <= 0)
                {
                    gvSerial.DataSource = _table2;
                    Session["gvSerData"] = _table2;
                    var _tblItems = from dr in _table2.AsEnumerable() group dr by new { Tus_itm_cd = dr["Tus_itm_cd"], Tus_itm_desc = dr["Tus_itm_desc"], Tus_itm_model = dr["Tus_itm_model"], Tus_itm_stus = dr["Tus_itm_stus"] } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => 0) }; gvItem.DataSource = _tblItems;
                }

                gvPending.DataBind();
                gvItem.DataBind();
                BindGridStatusItem();
                gvSerial.DataBind();
                BindGridStatusSerial();
                BindPendingGridColor();
                DataTable _dt1 = Session["WH_AVI"] as DataTable;
                DataTable _dt2 = Session["IsPDALoc"] as DataTable;
                if (_dt1.Rows.Count > 0 && _dt2.Rows.Count > 0)
                {
                    ColourChgAlreadyPDA();
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
        private void BindPendingGridColor()
        {
            foreach (GridViewRow _row in gvPending.Rows)
            {
                Label lblTMP_Tuh_fin_stus = _row.FindControl("lblTMP_Tuh_fin_stus") as Label;
                if (lblTMP_Tuh_fin_stus.Text == "1")
                {
                    _row.BackColor = Color.LightCyan;
                }
            }
        }

        private void ColourChgAlreadyPDA()
        {
            foreach (GridViewRow row in gvPending.Rows)
            {
                Label wh_com = row.FindControl("lblwh_com") as Label;
                if (wh_com.Text != "")
                {
                    row.ForeColor = Color.Purple;
                }
            }
        }
        static private string SoundEx(string word)
        {
            // The length of the returned code.
            int length = 4;

            // Value to return.
            string value = "";

            // The size of the word to process.
            int size = word.Length;

            // The word must be at least two characters in length.
            if (size > 1)
            {
                // Convert the word to uppercase characters.
                word = word.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

                // Convert the word to a character array.
                char[] chars = word.ToCharArray();

                // Buffer to hold the character codes.
                StringBuilder buffer = new StringBuilder();
                buffer.Length = 0;

                // The current and previous character codes.
                int prevCode = 0;
                int currCode = 0;

                // Add the first character to the buffer.
                buffer.Append(chars[0]);

                // Loop through all the characters and convert them to the proper character code.
                for (int i = 1; i < size; i++)
                {
                    switch (chars[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        case 'R':
                            currCode = 6;
                            break;
                    }

                    // Check if the current code is the same as the previous code.
                    if (currCode != prevCode)
                    {
                        // Check to see if the current code is 0 (a vowel); do not process vowels.
                        if (currCode != 0)
                            buffer.Append(currCode);
                    }
                    // Set the previous character code.
                    prevCode = currCode;

                    // If the buffer size meets the length limit, exit the loop.
                    if (buffer.Length == length)
                        break;
                }
                // Pad the buffer, if required.
                size = buffer.Length;
                if (size < length)
                    buffer.Append('0', (length - size));

                // Set the value to return.
                value = buffer.ToString();
            }
            // Return the value.
            return value;
        }

        protected void lbtnbtnDocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _dtPendingDoc = new DataTable();
                Session["POPUP_LOADED"] = null;
                SearchRequest();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvPending_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable _result = new DataTable();
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPending, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvPending_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvItem.DataSource = new int[] { };
                gvItem.DataBind();
                gvSerial.DataSource = new int[] { };
                gvSerial.DataBind();
                string ispopuploaded = (string)Session["POPUP_LOADED"];

                string istempdoc = (string)Session["DocType"];
                if ((ispopuploaded == "1") && (istempdoc == "Doc"))
                {
                    return;
                }

                Session["SERIALLIST"] = null;

                string pdaloc = (string)Session["PDA_LOCATION"];

                OutwardNo = (gvPending.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;
                _docNo = OutwardNo;
                DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "AOD", 1, Session["UserCompanyCode"].ToString());

                if (dtdoccheck1.Rows.Count > 1)
                {
                    gvItem.DataSource = new int[] { };
                    gvItem.DataBind();
                    gvSerial.DataSource = null;
                    Session["gvSerData"] = new int[] { };
                    gvSerial.DataBind();
                    lblIssedCompany.Text = string.Empty;
                    lblIssuedLocation.Text = string.Empty;
                    lblIssueLocDesc.Text = string.Empty;
                    lblIssuedDocNo.Text = string.Empty;
                    LoadDocQtyData(lblIssuedDocNo.Text);
                    lblOutQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                    return;
                }

                DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo);
                if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
                {
                    string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                    string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));
                    string _loadBay = Convert.ToString(_headerchk2.Rows[0].Field<string>("tuh_load_bay"));

                    if (chkpda.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(_headerUser))
                            if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                            {
                                string msg2 = "Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate + " in loading bay " + _loadBay;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                return;
                            }
                    }

                }

                Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), OutwardNo);
                List<ReptPickSerials> PickSerialsPOPUP = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_seq, OutwardNo, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                Session["POPUPSERIALS"] = PickSerialsPOPUP;
                Session["UsSeq"] = _seq.ToString();
                if (chkpda.Checked == true)
                {
                    #region chk pda
                    DataTable dtupdatedlb = CHNLSVC.Inventory.CheckHasLoadingBay(Session["UserCompanyCode"].ToString(), OutwardNo, Session["UserDefLoca"].ToString());

                    if (dtupdatedlb.Rows.Count == 0)
                    {
                        //Int32 deltempser = CHNLSVC.Inventory.DeleteRepSerials(_seq, OutwardNo);
                        gvSerial.DataSource = null;
                        Session["gvSerData"] = new int[] { };
                        gvSerial.DataBind();
                        Session["SERIALLIST"] = null;


                        txtdocname.Text = OutwardNo;
                        MPPDA.Show();

                        lblIssuedDocNo.Text = OutwardNo;
                        LoadDocQtyData(lblIssuedDocNo.Text);
                        lblIssedCompany.Text = (gvPending.SelectedRow.FindControl("lblissecompending") as Label).Text;
                        lblIssuedLocation.Text = (gvPending.SelectedRow.FindControl("lblissuelocpending") as Label).Text;
                        lblOutQty.Text = "";

                    }
                    else
                    {



                        if (gvPending.Rows.Count > 0)
                        {
                            _supplier = string.Empty;
                            _subdoc = string.Empty;
                            string location = string.Empty;

                            string outwrddate = (gvPending.SelectedRow.FindControl("lbldatepending") as Label).Text;

                            string warehousecom = (string)Session["WAREHOUSE_COM"];
                            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

                            DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "AOD", 1, Session["UserCompanyCode"].ToString());

                            if (dtdoccheck.Rows.Count > 1)
                            {
                                gvItem.DataSource = new int[] { };
                                gvItem.DataBind();
                                gvSerial.DataSource = null;
                                Session["gvSerData"] = new int[] { };
                                gvSerial.DataBind();
                                lblIssedCompany.Text = string.Empty;
                                lblIssuedLocation.Text = string.Empty;
                                lblIssueLocDesc.Text = string.Empty;
                                lblIssuedDocNo.Text = string.Empty;
                                LoadDocQtyData(lblIssuedDocNo.Text);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                                return;
                            }

                            DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo);
                            if (_headerchk != null && _headerchk.Rows.Count > 0)
                            {
                                string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                                string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));

                                if (chkpda.Checked == true)
                                {
                                    if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                                        {
                                            string msg2 = "Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                            return;
                                        }
                                }
                            }

                            hdnOutwarddate = Convert.ToDateTime(outwrddate);
                            Session["OUTWARDDATE"] = outwrddate;
                            OutwardType = (gvPending.SelectedRow.FindControl("lbltypepending") as Label).Text;
                            lblIssuedDocNo.Text = OutwardNo;
                            LoadDocQtyData(lblIssuedDocNo.Text);
                            lblIssedCompany.Text = (gvPending.SelectedRow.FindControl("lblissecompending") as Label).Text;
                            lblIssuedLocation.Text = (gvPending.SelectedRow.FindControl("lblissuelocpending") as Label).Text;
                            _supplier = (gvPending.SelectedRow.FindControl("lblsupplerpending") as Label).Text;
                            _subdoc = (gvPending.SelectedRow.FindControl("lblsubdocpending") as Label).Text;

                            DataTable _tbl = null;
                            if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text))
                                _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());
                            if (_tbl != null && _tbl.Rows.Count > 0)

                                foreach (DataRow ddr in _tbl.Rows)
                                {
                                    location = ddr["ml_loc_desc"].ToString();
                                }

                            lblIssueLocDesc.Text = location;

                            _dono = string.Empty;
                            PickSerialsList = null;
                            UserSeqNo = _seq;

                            string _unavailableitemlist = string.Empty;

                            string err = string.Empty;

                            hdnAllowBin = (string)Session["BIN"];

                            List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_seq, OutwardNo, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                            if (!string.IsNullOrEmpty(_unavailableitemlist))
                            {
                                string msg3 = "Following item does not setup in the current system.Item List " + _unavailableitemlist;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the date range !!!')", true);
                                return;
                            }

                            if (PickSerials != null)
                            {
                                DataTable dt = GlobalMethod.ToDataTable(PickSerials);
                            }


                            if (PickSerials != null)
                            {
                                InventoryHeader oAOD_FROM_INTHDR = CHNLSVC.Inventory.Get_Int_Hdr(OutwardNo);
                                MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(oAOD_FROM_INTHDR.Ith_com, oAOD_FROM_INTHDR.Ith_loc);

                                var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                                gvItem.DataSource = _tblItems;
                                gvItem.DataBind();
                                BindGridStatusItem();
                                if (_mstLoc.Ml_is_serial)
                                {
                                    gvSerial.DataSource = PickSerials;
                                    Session["gvSerData"] = PickSerials;
                                    gvSerial.DataBind();
                                    BindGridStatusSerial();
                                }
                                else
                                {
                                    for (int i = 0; i < gvItem.Rows.Count; i++)
                                    {
                                        GridViewRow dr = gvItem.Rows[i];
                                        LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                        Label lblitem = dr.FindControl("lblitem") as Label;

                                        MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                        if (tempItem.Mi_is_ser1 == 1)
                                        {
                                            btnAddSerials.Visible = true;
                                        }
                                    }

                                    List<int> termsList = new List<int>();

                                    foreach (ReptPickSerials item in PickSerials)
                                    {
                                        MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                        if (tempItem.Mi_is_ser1 == 1)
                                        {
                                            termsList.Add(PickSerials.IndexOf(item));
                                        }
                                    }

                                    Session["UsSeq"] = _seq.ToString();

                                    foreach (int item in termsList)
                                    {
                                        PickSerials.RemoveAt(item);
                                    }

                                    gvSerial.DataSource = PickSerials;
                                    Session["gvSerData"] = PickSerials;
                                    gvSerial.DataBind();
                                    BindGridStatusSerial();
                                }

                                PickSerialsList = PickSerials;
                                Session["SERIALLIST"] = PickSerialsList;
                            }
                        }




                    }
                    #endregion
                }
                else
                {
                    #region new re
                    MPPDA.Hide();
                    txtdocname.Text = string.Empty;
                    ddlloadingbay.SelectedIndex = 0;

                    if (gvPending.Rows.Count > 0)
                    {
                        _supplier = string.Empty;
                        _subdoc = string.Empty;
                        string location = string.Empty;

                        string outwrddate = (gvPending.SelectedRow.FindControl("lbldatepending") as Label).Text;

                        string warehousecom = (string)Session["WAREHOUSE_COM"];
                        string warehouseloc = (string)Session["WAREHOUSE_LOC"];

                        DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "AOD", 1, Session["UserCompanyCode"].ToString());

                        if (dtdoccheck.Rows.Count > 1)
                        {
                            gvItem.DataSource = null;
                            gvItem.DataBind();
                            gvSerial.DataSource = null;
                            Session["gvSerData"] = new int[] { };
                            gvSerial.DataBind();
                            lblIssedCompany.Text = string.Empty;
                            lblIssuedLocation.Text = string.Empty;
                            lblIssueLocDesc.Text = string.Empty;
                            lblIssuedDocNo.Text = string.Empty;
                            LoadDocQtyData(lblIssuedDocNo.Text);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                            return;
                        }

                        DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo);
                        if (_headerchk != null && _headerchk.Rows.Count > 0)
                        {
                            string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                            string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));

                            if (chkpda.Checked == true)
                            {
                                if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                                    {
                                        string msg2 = "Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                        return;
                                    }
                            }

                        }

                        hdnOutwarddate = Convert.ToDateTime(outwrddate);
                        Session["OUTWARDDATE"] = outwrddate;
                        OutwardType = (gvPending.SelectedRow.FindControl("lbltypepending") as Label).Text;
                        lblIssuedDocNo.Text = OutwardNo;
                        LoadDocQtyData(lblIssuedDocNo.Text);
                        lblIssedCompany.Text = (gvPending.SelectedRow.FindControl("lblissecompending") as Label).Text;
                        lblIssuedLocation.Text = (gvPending.SelectedRow.FindControl("lblissuelocpending") as Label).Text;
                        _supplier = (gvPending.SelectedRow.FindControl("lblsupplerpending") as Label).Text;
                        _subdoc = (gvPending.SelectedRow.FindControl("lblsubdocpending") as Label).Text;

                        DataTable _tbl = null;
                        if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text))
                            _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());
                        if (_tbl != null && _tbl.Rows.Count > 0)

                            foreach (DataRow ddr in _tbl.Rows)
                            {
                                location = ddr["ml_loc_desc"].ToString();
                            }

                        lblIssueLocDesc.Text = location;

                        _dono = string.Empty; PickSerialsList = null;

                        int affected_rows = 0;
                        UserSeqNo = _seq;
                        //Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, Session["UserCompanyCode"].ToString(), OutwardNo, 1);
                        //if (user_seq_num == -1)
                        //{
                        //    ReptPickHeader _reptPickHdr = new ReptPickHeader();
                        //    user_seq_num = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), OutwardType, 1, Session["UserCompanyCode"].ToString());
                        //    UserSeqNo = _seq;
                        //    _reptPickHdr.Tuh_direct = true;
                        //    _reptPickHdr.Tuh_doc_no = OutwardNo;
                        //    _reptPickHdr.Tuh_doc_tp = OutwardType;
                        //    _reptPickHdr.Tuh_ischek_itmstus = false;
                        //    _reptPickHdr.Tuh_ischek_reqqty = true;
                        //    _reptPickHdr.Tuh_ischek_simitm = false;
                        //    _reptPickHdr.Tuh_session_id = Session["SessionID"].ToString();
                        //    _reptPickHdr.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        //    _reptPickHdr.Tuh_usr_id = Session["UserID"].ToString();
                        //    _reptPickHdr.Tuh_usrseq_no = user_seq_num;
                        //    //affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(_reptPickHdr);
                        //}


                        string _unavailableitemlist = string.Empty;

                        string err = string.Empty;
                        hdnAllowBin = (string)Session["BIN"];
                        Int32 _userSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, Session["UserCompanyCode"].ToString(), OutwardNo, 1);
                        // List<ReptPickSerials> PickSerials2 = CHNLSVC.Inventory.GetOutwarditemsNew(Session["UserDefLoca"].ToString(), hdnAllowBin, _reptPickHdr, out _unavailableitemlist, out err);
                        List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_userSeq, OutwardNo, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        if (PickSerials != null)
                        {
                            if (PickSerials.Count == 0)
                            {
                                PickSerials = null;
                            }
                        }
                        if (!string.IsNullOrEmpty(_unavailableitemlist))
                        {
                            string msg3 = "Following item does not setup in the current system.Item List " + _unavailableitemlist;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the date range !!!')", true);
                            return;
                        }
                        //Add by Lakshan
                        if (chkAODoutserials.Checked)
                        {
                            #region MyRegion pick from aod out
                            if (PickSerials == null)
                            {
                                #region coment
                                /*
                                PickSerials = new List<ReptPickSerials>();
                                List<InventoryItem> _invItm = CHNLSVC.Inventory.GET_INT_ITM_DATA(new InventoryItem() { Iti_doc_no = lblIssuedDocNo.Text });
                                _invItm = _invItm.Where(c => c.Iti_bal_qty > 0).ToList();
                                List<InventoryItem> _tmpInvItm = new List<InventoryItem>();
                                foreach (var _it in _invItm)
                                {
                                    var _ava = _tmpInvItm.Where(c => c.Iti_doc_no == _it.Iti_doc_no && c.Iti_item_code == _it.Iti_item_code && c.Iti_item_status == _it.Iti_item_status).FirstOrDefault();
                                    if (_ava==null)
                                    {
                                        _tmpInvItm.Add(_it);
                                    }
                                }
                              //  _invItm= _invItm.GroupBy(x => new { x.Iti_item_code, x., x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost })
                                MasterItem _tmpMstItem = new MasterItem();
                                List<ReptPickSerials> _tmpPickSerList = new List<ReptPickSerials>();
                                InventorySerialN _tmpIntSer = new InventorySerialN();
                                List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                                foreach (var item in _tmpInvItm)
                                {
                                    _tmpMstItem = CHNLSVC.General.GetItemMaster(item.Iti_item_code);
                                    if (_mstLocation.Ml_is_serial)
                                    {
                                        if (_tmpMstItem.Mi_is_ser1 == 1)
                                        {
                                            _tmpPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_BY_INT_SER(new InventorySerialN() { Ins_doc_no = lblIssuedDocNo.Text, Ins_itm_cd = item.Iti_item_code, Ins_itm_stus = item.Iti_item_status });
                                            _tmpPickSerList=_tmpPickSerList.Where(c => c.Tus_itm_stus == item.Iti_item_status).ToList();
                                            _tmpPickSerList = _tmpPickSerList.Where(c => c.Tus_ins_pick == 0).ToList();
                                            
                                            //if ( item.Iti_bal_qty != _tmpPickSerList.Count)
                                            //{
                                            //    DisplayMessage("Serial data is invalid !");
                                            //    return;
                                            //}
                                            foreach (var _ser in _tmpPickSerList)
                                            {
                                                PickSerials.Add(_ser);
                                            }
                                        }
                                        else
                                        {
                                            _tmpPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_BY_INT_SER(new InventorySerialN() { Ins_doc_no = lblIssuedDocNo.Text, Ins_itm_cd = item.Iti_item_code });
                                            _tmpPickSerList = _tmpPickSerList.Where(c => c.Tus_ins_pick == 0).ToList();
                                            //if ( item.Iti_bal_qty != _tmpPickSerList.Count)
                                            //{
                                            //    DisplayMessage("Serial data is invalid !");
                                            //    return;
                                            //}
                                            foreach (var _ser in _tmpPickSerList)
                                            {
                                                PickSerials.Add(_ser);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //foreach (InventoryBatchN _batvh in _nonserial)
                                        //{
                                        //    if (_batvh.Itb_bal_qty1 > 0 && _batvh.Inb_itm_cd == item.Iti_item_code)
                                        //    {
                                        //        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                        //        _reptPickSerial_.Tus_seq_no = _batvh.Inb_seq_no;
                                        //        _reptPickSerial_.Tus_doc_no = _batvh.Inb_doc_no;
                                        //        _reptPickSerial_.Tus_base_itm_line = item.Iti_item_line;
                                        //        _reptPickSerial_.Tus_itm_desc = _tmpMstItem.Mi_shortdesc;
                                        //        _reptPickSerial_.Tus_itm_model = _tmpMstItem.Mi_model;
                                        //        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        //        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        //        _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                        //        _reptPickSerial_.Tus_itm_cd = _batvh.Inb_itm_cd;
                                        //        _reptPickSerial_.Tus_itm_stus = _batvh.Inb_itm_stus;
                                        //        _reptPickSerial_.Tus_itm_line = _batvh.Inb_itm_line;
                                        //        _reptPickSerial_.Tus_qty = Convert.ToDecimal(_batvh.Itb_bal_qty1);
                                        //        _reptPickSerial_.Tus_ser_1 = "N/A";
                                        //        _reptPickSerial_.Tus_ser_2 = "N/A";
                                        //        _reptPickSerial_.Tus_ser_3 = "N/A";
                                        //        _reptPickSerial_.Tus_ser_4 = "N/A";
                                        //        _reptPickSerial_.Tus_ser_id = 0;
                                        //        _reptPickSerial_.Tus_serial_id = "0";
                                        //        _reptPickSerial_.Tus_unit_cost = 0;
                                        //        _reptPickSerial_.Tus_unit_price = 0;
                                        //        PickSerials.Add(_reptPickSerial_);
                                        //    }
                                        //}
                                    }
                                }
                                
                                //PickSerials = CHNLSVC.Inventory.Get_Int_Ser(lblIssuedDocNo.Text);
                                //MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                                //if (!_mstLocation.Ml_is_serial)
                                //{
                                //    List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                //    foreach (InventoryBatchN _batvh in _nonserial)
                                //    {
                                //        if (_batvh.Inb_qty>0)
                                //        {
                                //            ReptPickSerials _item = new ReptPickSerials();
                                //            _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                //            //_item.Tus_itm_desc=_batvh.
                                //            //_item.Tus_itm_model = _batvh.Inb_itm_cd;
                                //            _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                //            _item.Tus_qty = _batvh.Inb_qty;
                                //            //_item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                //            // _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                //            PickSerials.Add(_item); 
                                //        }
                                //    }
                                //}
                                 * */
                                #endregion
                                MasterLocation _mastLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                                if (_mastLoc.Ml_is_serial)
                                {
                                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, Session["UserCompanyCode"].ToString(), OutwardNo, 1);
                                    if (user_seq_num == -1)
                                    {
                                        ReptPickHeader _reptPickHdr = new ReptPickHeader();
                                        user_seq_num = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), OutwardType, 1, Session["UserCompanyCode"].ToString());
                                        UserSeqNo = _seq;
                                        _reptPickHdr.Tuh_direct = true;
                                        _reptPickHdr.Tuh_doc_no = OutwardNo;
                                        _reptPickHdr.Tuh_doc_tp = "AOD";
                                        _reptPickHdr.Tuh_ischek_itmstus = false;
                                        _reptPickHdr.Tuh_ischek_reqqty = true;
                                        _reptPickHdr.Tuh_ischek_simitm = false;
                                        _reptPickHdr.Tuh_session_id = Session["SessionID"].ToString();
                                        _reptPickHdr.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                                        _reptPickHdr.Tuh_usr_id = Session["UserID"].ToString();
                                        _reptPickHdr.Tuh_usrseq_no = user_seq_num;
                                        //affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(_reptPickHdr);
                                        string _defBin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                                        if (string.IsNullOrEmpty(_defBin))
                                        {
                                            string msg1 = "Default Bin Not Setup For Location : " + Session["UserDefLoca"].ToString();
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg1 + "')", true);
                                            Label5.Text = msg1;
                                            SbuPopup.Show();
                                            return;
                                        }

                                        List<ReptPickSerials> _tmpPickSer = CHNLSVC.Inventory.GetOutwarditems(Session["UserDefLoca"].ToString(), _defBin, _reptPickHdr
                                       , out _unavailableitemlist);
                                        if (_tmpPickSer != null)
                                        {
                                            if (_tmpPickSer.Count != 0)
                                            {
                                                PickSerials = _tmpPickSer;
                                            }
                                        }

                                        /*if (PickSerials != null)
                                        {
                                            if (PickSerials.Count > 0)
                                            {
                                                foreach (ReptPickSerials serial in PickSerials)
                                                {
                                                    serial.Tus_cre_by = Session["UserID"].ToString();
                                                    serial.Tus_usrseq_no = user_seq_num;
                                                    serial.Tus_cre_by = Session["UserID"].ToString();
                                                    serial.Tus_base_doc_no = OutwardNo;
                                                    serial.Tus_loc = Session["UserDefLoca"].ToString();
                                                    serial.Tus_bin = Session["GlbDefaultBin"].ToString();
                                                    Label lbljobnopending = (gvPending.SelectedRow.FindControl("lbljobnopending") as Label);
                                                    if (!string.IsNullOrEmpty(lbljobnopending.Text))
                                                    {
                                                        serial.Tus_job_no = lbljobnopending.Text;
                                                    }

                                                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                                                }
                                            }
                                        }*/
                                    }
                                }
                            }
                            #endregion
                            LoadDocQtyData(OutwardNo);
                        }
                        //End 
                        //Comment by  lakshan
                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        #region coment
                        /*if (!_mstLoc.Ml_is_serial)
                        {
                            if (chkAODoutserials.Checked)
                            {
                                if (PickSerials == null)
                                {
                                    PickSerials = new List<ReptPickSerials>();
                                    PickSerials = CHNLSVC.Inventory.Get_Int_Ser(lblIssuedDocNo.Text);
                                    if (PickSerials.Count == 0)
                                    {
                                        List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                        foreach (InventoryBatchN _batvh in _nonserial)
                                        {
                                            ReptPickSerials _item = new ReptPickSerials();
                                            _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            //_item.Tus_itm_desc=_batvh.
                                            //_item.Tus_itm_model = _batvh.Inb_itm_cd;
                                            _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                            _item.Tus_qty = _batvh.Inb_qty;
                                            //_item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            // _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            PickSerials.Add(_item);
                                        }
                                    }
                                    else
                                    {
                                        List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                        foreach (InventoryBatchN _batvh in _nonserial)
                                        {
                                            ReptPickSerials _item = new ReptPickSerials();
                                            _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            //_item.Tus_itm_desc=_batvh.
                                            //_item.Tus_itm_model = _batvh.Inb_itm_cd;
                                            _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                            _item.Tus_qty = _batvh.Inb_qty;
                                            //_item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            // _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                            PickSerials.Add(_item);
                                        }
                                    }


                                }
                            }
                        }
                        if (chkAODoutserials.Checked)
                        {
                            if (PickSerials == null)
                            {
                                PickSerials = CHNLSVC.Inventory.Get_Int_Ser(OutwardNo);
                                if (PickSerials != null)
                                {
                                    if (PickSerials.Count > 0)
                                    {
                                        foreach (ReptPickSerials serial in PickSerials)
                                        {
                                            serial.Tus_cre_by = Session["UserID"].ToString();
                                            serial.Tus_usrseq_no = UserSeqNo;
                                            serial.Tus_cre_by = Session["UserID"].ToString();
                                            serial.Tus_base_doc_no = OutwardNo;
                                            serial.Tus_loc = Session["UserDefLoca"].ToString();
                                            serial.Tus_bin = Session["GlbDefaultBin"].ToString();
                                            Label lbljobnopending = (gvPending.SelectedRow.FindControl("lbljobnopending") as Label);
                                            if (!string.IsNullOrEmpty(lbljobnopending.Text))
                                            {
                                                serial.Tus_job_no = lbljobnopending.Text;
                                            }
                                           
                                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                                        }
                                    }
                                }
                            }
                        }*/
                        #endregion
                        if (PickSerials != null)
                        {
                            DataTable dt = GlobalMethod.ToDataTable(PickSerials);
                        }

                        if (PickSerials != null)
                        {
                            foreach (ReptPickSerials item in PickSerials)
                            {
                                MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                item.Tus_itm_desc = tempItem.Mi_longdesc;
                                item.Tus_itm_model = tempItem.Mi_model;
                            }
                            InventoryHeader oAOD_FROM_INTHDR = CHNLSVC.Inventory.Get_Int_Hdr(OutwardNo);
                            // MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(oAOD_FROM_INTHDR.Ith_com, oAOD_FROM_INTHDR.Ith_loc);
                            List<ReptPickSerials> PickSerials_ITM = new List<ReptPickSerials>();

                            if (PickSerials_ITM.Count == 0)
                            {
                                List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                bool _getScmBatch = false;
                                if (_nonserial == null)
                                {
                                    _getScmBatch = true;
                                }
                                else
                                {
                                    if (_nonserial.Count == 0)
                                    {
                                        _getScmBatch = true;
                                    }
                                }
                                if (_getScmBatch)
                                {
                                    _nonserial = CHNLSVC.Inventory.Get_Scm_Int_Batch(lblIssuedDocNo.Text);
                                }
                                foreach (InventoryBatchN _batvh in _nonserial)
                                {
                                    if (_batvh.Itb_bal_qty1 > 0)
                                    {
                                        MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _batvh.Inb_itm_cd);

                                        ReptPickSerials _item = new ReptPickSerials();
                                        _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                        _item.Tus_itm_desc = tempItem.Mi_longdesc;
                                        _item.Tus_itm_model = tempItem.Mi_model;
                                        //_item.Tus_itm_desc=_batvh.
                                        //_item.Tus_itm_model = _batvh.Inb_itm_cd;
                                        _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                        _item.Tus_qty = _batvh.Itb_bal_qty1;
                                        //_item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                        // _item.Tus_itm_cd = _batvh.Inb_itm_cd;

                                        PickSerials_ITM.Add(_item);
                                    }

                                }
                            }
                            var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                            gvItem.DataSource = PickSerials_ITM;
                            gvItem.DataBind();
                            LoadMrnData(PickSerials_ITM);
                            BindGridStatusItem();
                            if (_mstLoc.Ml_is_serial)
                            {
                                gvSerial.DataSource = GetGridDataForSerial(PickSerials);
                                Session["gvSerData"] = PickSerials;
                                gvSerial.DataBind();
                                BindGridStatusSerial();
                                for (int i = 0; i < gvItem.Rows.Count; i++)
                                {
                                    GridViewRow dr = gvItem.Rows[i];
                                    LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                    Label lblitem = dr.FindControl("lblitem") as Label;

                                    MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                    if (tempItem.Mi_is_ser1 == 1)
                                    {
                                        btnAddSerials.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < gvItem.Rows.Count; i++)
                                {
                                    GridViewRow dr = gvItem.Rows[i];
                                    LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                    Label lblitem = dr.FindControl("lblitem") as Label;

                                    MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                    if (tempItem.Mi_is_ser1 == 1)
                                    {
                                        btnAddSerials.Visible = true;
                                    }
                                }

                                List<int> termsList = new List<int>();

                                foreach (ReptPickSerials item in PickSerials)
                                {
                                    MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                    if (tempItem.Mi_is_ser1 == 1)
                                    {
                                        termsList.Add(PickSerials.IndexOf(item));
                                    }
                                    item.Tus_itm_desc = tempItem.Mi_longdesc;
                                    item.Tus_itm_model = tempItem.Mi_model;
                                }
                                gvItem.DataSource = _tblItems;
                                gvItem.DataBind();
                                LoadMrnData(PickSerials);
                                BindGridStatusItem();
                                Session["UsSeq"] = _seq.ToString();

                                //PDA scan serial remove  this code then comment by rukshan
                                //foreach (int item in termsList)
                                //{
                                //    PickSerials.RemoveAt(item);
                                //}

                                gvSerial.DataSource = PickSerials; Session["gvSerData"] = PickSerials;
                                gvSerial.DataBind();
                                BindGridStatusSerial();
                            }

                            PickSerialsList = PickSerials;
                            Session["SERIALLIST"] = PickSerialsList;
                        }
                        else
                        {
                            if (chkAODoutserials.Checked)
                            {
                                PickSerials = new List<ReptPickSerials>();
                                PickSerials = CHNLSVC.Inventory.Get_Int_Ser(lblIssuedDocNo.Text);
                                if (PickSerials.Count == 0)
                                {
                                    List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                    foreach (InventoryBatchN _batvh in _nonserial)
                                    {
                                        ReptPickSerials _item = new ReptPickSerials();
                                        _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                        _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                        _item.Tus_qty = _batvh.Itb_bal_qty1;
                                        PickSerials.Add(_item);
                                    }
                                    //foreach (var _serial in PickSerials)
                                    //{
                                    //    Int32 row = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);
                                    //}
                                }
                                foreach (ReptPickSerials item in PickSerials)
                                {
                                    MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                    item.Tus_itm_desc = tempItem.Mi_longdesc;
                                    item.Tus_itm_model = tempItem.Mi_model;
                                }
                                var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                                gvItem.DataSource = _tblItems;
                                gvItem.DataBind();
                                LoadMrnData(PickSerials);
                                BindGridStatusItem();

                                for (int i = 0; i < gvItem.Rows.Count; i++)
                                {
                                    GridViewRow dr = gvItem.Rows[i];
                                    LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                    Label lblitem = dr.FindControl("lblitem") as Label;

                                    MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                    if (tempItem.Mi_is_ser1 == 1)
                                    {
                                        btnAddSerials.Visible = true;
                                    }
                                }
                                LoadDocQtyData(lblIssuedDocNo.Text);
                            }
                            else
                            {
                                try
                                {
                                    if (!chkAODoutserials.Checked && PickSerials == null)
                                    {
                                        #region Load Item
                                        List<ReptPickSerials> _tmpList = new List<ReptPickSerials>();
                                        List<ReptPickSerials> _listTemp = new List<ReptPickSerials>();
                                        _listTemp = CHNLSVC.Inventory.Get_Int_Ser(lblIssuedDocNo.Text);
                                        bool intSerAva = false;
                                        if (_listTemp != null)
                                        {
                                            if (_listTemp.Count > 0)
                                            {
                                                intSerAva = true;
                                            }
                                        }
                                        if (!intSerAva)
                                        {
                                            if (_listTemp != null)
                                            {
                                                List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                                foreach (InventoryBatchN _batvh in _nonserial)
                                                {
                                                    ReptPickSerials _item = new ReptPickSerials();
                                                    _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                                    _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                                    _item.Tus_qty = _batvh.Itb_bal_qty1;
                                                    _tmpList.Add(_item);
                                                }
                                            }
                                            foreach (ReptPickSerials item in _tmpList)
                                            {
                                                MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                                if (tempItem != null)
                                                {
                                                    item.Tus_itm_desc = tempItem.Mi_longdesc;
                                                    item.Tus_itm_model = tempItem.Mi_model;
                                                }
                                            }
                                            var _tblItems = (from _pickSerials in _tmpList group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                                            gvItem.DataSource = _tblItems;
                                            gvItem.DataBind();

                                            //gvItem.DataSource = setItemStatus1(_tblItems);
                                            //gvItem.DataBind();
                                            LoadMrnData(_tmpList);
                                            //ScanItemList.AddRange(_listTemp);
                                            BindGridStatusItem();
                                            for (int i = 0; i < gvItem.Rows.Count; i++)
                                            {
                                                GridViewRow dr = gvItem.Rows[i];
                                                LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                                Label lblitem = dr.FindControl("lblitem") as Label;
                                                btnAddSerials.Visible = false;
                                                //MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                                //if (tempItem.Mi_is_ser1 == 1)
                                                //{
                                                //    btnAddSerials.Visible = true;
                                                //}
                                            }
                                        }
                                        if (intSerAva)
                                        {
                                            if (_listTemp != null)
                                            {
                                                List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text);
                                                foreach (InventoryBatchN _batvh in _nonserial)
                                                {
                                                    if (_batvh.Itb_bal_qty1 > 0)
                                                    {
                                                        ReptPickSerials _item = new ReptPickSerials();
                                                        _item.Tus_itm_cd = _batvh.Inb_itm_cd;
                                                        _item.Tus_itm_stus = _batvh.Inb_itm_stus;
                                                        _item.Tus_qty = _batvh.Itb_bal_qty1;
                                                        _tmpList.Add(_item);
                                                    }
                                                }
                                            }
                                            foreach (ReptPickSerials item in _tmpList)
                                            {
                                                MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                                                if (tempItem != null)
                                                {
                                                    item.Tus_itm_desc = tempItem.Mi_longdesc;
                                                    item.Tus_itm_model = tempItem.Mi_model;
                                                }
                                                else
                                                {

                                                }
                                            }
                                            var _tblItems = (from _pickSerials in _tmpList group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                                            gvItem.DataSource = _tblItems;
                                            gvItem.DataBind();
                                            BindGridStatusItem();
                                            LoadMrnData(_tmpList);
                                            for (int i = 0; i < gvItem.Rows.Count; i++)
                                            {
                                                GridViewRow dr = gvItem.Rows[i];
                                                LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                                                Label lblitem = dr.FindControl("lblitem") as Label;
                                                btnAddSerials.Visible = false;
                                                //MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
                                                //if (tempItem.Mi_is_ser1 == 1)
                                                //{
                                                //    btnAddSerials.Visible = true;
                                                //}
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        LoadDocQtyData(lblIssuedDocNo.Text);
                                }
                                }
                                catch (Exception)
                                {
                                    DispMsg("No data found !");
                                }
                                gvSerial.DataSource = PickSerials;
                                gvSerial.DataBind();
                            }
                        }
                    }
                    #endregion
                }
                //gvItems.DataSource = setItemStatus1(ScanItemList);
                //gvItems.DataBind();
                
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

        protected void lbtnlocsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "AOD" + seperator + "1" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "1")
                {
                    txtlocation.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "1";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindPickSerials(Int32 userseqno)
        {
            try
            {
                OutwardType = ddlType.SelectedValue;
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), userseqno, OutwardType);
                PickSerialsList = _list;
                if (PickSerialsList != null) if (PickSerialsList.Count > 0)
                    {
                        List<int> termsList = new List<int>();

                        foreach (ReptPickSerials item in PickSerialsList)
                        {
                            MasterItem tempItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                            if (tempItem.Mi_is_ser1 == 1 && item.Tus_qty > 1)
                            {
                                termsList.Add(PickSerialsList.IndexOf(item));
                            }
                        }

                        foreach (int item in termsList)
                        {
                            PickSerialsList.RemoveAt(item);
                        }

                        var _tblItems = (from _pickSerials in PickSerialsList group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                        gvItem.DataSource = _tblItems;
                        gvSerial.DataSource = PickSerialsList;
                        Session["gvSerData"] = PickSerialsList;
                        gvItem.DataBind();
                        BindGridStatusItem();
                        gvSerial.DataBind();
                        BindGridStatusSerial();
                    }
                    else
                    {
                        PickSerialsList = new List<ReptPickSerials>();
                        gvItem.DataSource = PickSerialsList;
                        gvSerial.DataSource = PickSerialsList;
                        Session["gvSerData"] = PickSerialsList;
                        gvItem.DataBind();
                        BindGridStatusItem();
                        gvSerial.DataBind();
                        BindGridStatusSerial();
                    }
                else
                {
                    PickSerialsList = new List<ReptPickSerials>();
                    gvItem.DataSource = PickSerialsList;
                    gvSerial.DataSource = PickSerialsList;
                    gvItem.DataBind();
                    BindGridStatusItem();
                    gvSerial.DataBind();
                    BindGridStatusSerial();
                }
                Session["SERIALLIST"] = PickSerialsList;
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

        protected void lbtndeleteserial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    Int32 _serialID = -1;
                    if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "(R)Select the outward document !!!", true);
                        return;
                    }

                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {
                        string _bin = (row.FindControl("lblbinser") as Label).Text;
                        string _item = (row.FindControl("lblitemser") as Label).Text;
                        string _serialIDDup = (row.FindControl("lblserid") as Label).Text;
                        string userseqno = (row.FindControl("lbluserseqno") as Label).Text;
                        Int32 processseqno = Convert.ToInt32(userseqno);

                        _serialID = Convert.ToInt32(_serialIDDup);

                        if (string.IsNullOrEmpty(_bin))
                            return;

                        if (_serialID == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot remove the none serialized items !!!')", true);
                            return;
                        }
                        _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                        CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), processseqno, Convert.ToInt32(_serialID), null, null);
                        BindPickSerials(processseqno);
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

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    Clear();
                    BindOutwardListGridData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            try
            {
                txtManRefNo.Text = "";
                chkPendingDoc.Checked = false;
                chkAODoutserials.Checked = false;
                chkpda.Checked = false;
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft";
                btnSave.OnClientClick = "ConfirmSave();";
                //gvPending.DataSource = null;
                //gvPending.DataBind();

                gvItem.DataSource = null;
                gvItem.DataBind();

                gvSerial.DataSource = null;
                Session["gvSerData"] = null;
                gvSerial.DataBind();

                gventryserials.DataSource = new int[] { };
                gventryserials.DataBind();

                txtAODNumber.Text = string.Empty;
                txtlocation.Text = string.Empty;
                lblIssedCompany.Text = string.Empty;
                lblIssuedLocation.Text = string.Empty;
                lblIssuedDocNo.Text = string.Empty;
                LoadDocQtyData(lblIssuedDocNo.Text);
                txtVehicle.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                lblIssueLocDesc.Text = string.Empty;
                Session["SERIALLIST"] = null;
                Session["OUTWARDDATE"] = null;
                Session["STATUS"] = null;
                Session["ALLOWCURDATE"] = null;
                Session["WRONGDATERANGE"] = null;
                Session["POPUP_LOADED"] = null;
                Session["DocType"] = null;
                Session["POPUPSERIALS"] = null;

                _POPUPSERIALLIST = new List<ReptPickSerials>();

                foreach (GridViewRow hiderowbtn in this.gvSerial.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndeleteserial");

                    _delbutton.Enabled = true;
                    _delbutton.CssClass = "buttonUndocolor";
                    _delbutton.OnClientClick = "ConfirmDelete();";
                }

                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmSave();";
                btnSave.CssClass = "buttonUndocolor";

                LinkButton1.Enabled = true;
                LinkButton1.OnClientClick = "ConfirmSave();";
                LinkButton1.CssClass = "buttonUndocolor";

                Session["UsSeq"] = null;
                Session["oNewSerials"] = null;
                chkpda.Checked = false;
                chktemp.Checked = false;

                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "m_Trans_Inventory_StockTransferInward", txtdate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16019))
                {
                    lbtnapprove.Enabled = false;
                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtnapprove.Enabled = true;
                    lbtnapprove.CssClass = "buttonUndocolor";
                    lbtnapprove.OnClientClick = "ConfirmApprove();";
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16020))
                {
                    lbtnreject.Enabled = false;
                    lbtnreject.CssClass = "buttoncolor";
                    lbtnreject.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtnreject.Enabled = true;
                    lbtnreject.CssClass = "buttonUndocolor";
                    lbtnreject.OnClientClick = "ConfirmReject();";
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16021))
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancel();";
                }

                lblDocQty.Text = (0).ToString("N2");
                lblDocSerPickQty.Text = (0).ToString("N2");

                //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16022))
                //{
                //    btnSave.Enabled = false;
                //    btnSave.CssClass = "buttoncolor";
                //    btnSave.OnClientClick = "return Enable();";
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //    btnSave.CssClass = "buttonUndocolor";
                //    btnSave.OnClientClick = "ConfirmSave();";
                //}

                //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16033))
                //{
                //    LinkButton1.Enabled = false;
                //    LinkButton1.CssClass = "buttoncolor";
                //    LinkButton1.OnClientClick = "return Enable();";
                //}
                //else
                //{
                //    LinkButton1.Enabled = true;
                //    LinkButton1.CssClass = "buttonUndocolor";
                //    LinkButton1.OnClientClick = "ConfirmSave();";
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                    lbtndate.Visible = true;

                    DateTime Selecteddate = Convert.ToDateTime(txtdate.Text.Trim());
                    DateTime appfromdate = Convert.ToDateTime(_bdt.Gad_act_from_dt);
                    DateTime apptodate = Convert.ToDateTime(_bdt.Gad_act_to_dt);

                    if (_bdt.Gad_alw_curr_trans == true)
                    {
                        // if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        // {
                        Session["WRONGDATERANGE"] = "0";

                        // }
                        // else
                        // {
                        //     Session["WRONGDATERANGE"] = "1";
                        // }

                        if (txtdate.Text == DateTime.Now.Date.ToString())
                        {
                            Session["ALLOWCURDATE"] = "1";
                        }
                        else
                        {
                            Session["ALLOWCURDATE"] = "0";
                        }
                    }
                    else
                    {
                        if (txtdate.Text == DateTime.Now.Date.ToString())
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
                    lbtndate.Visible = false;
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
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), out _expMsg) == true)
                {
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                }
            }
        }
        public bool IsUserProcessed(Int32 _seqno, string _document)
        {
            bool _isUserProcessed = false;
            bool _isProcess = CHNLSVC.Inventory.CheckCompanyMulti(Session["UserCompanyCode"].ToString());
            if (_isProcess == false) { _isUserProcessed = false; return _isUserProcessed; }

            string _error = string.Empty;
            DataTable _tbl = CHNLSVC.Inventory.GetProcessUser(_seqno, _document, Session["UserCompanyCode"].ToString());
            if (_tbl == null || _tbl.Rows.Count <= 0) return _isUserProcessed;
            string _user = _tbl.Rows[0].Field<string>("tuh_pro_user");
            if (string.IsNullOrEmpty(_user))
            {
                _isUserProcessed = false;
                try
                {
                    Int32 _effect = CHNLSVC.Inventory.UpdateProcessUser(Session["UserID"].ToString(), _seqno, _document, Session["UserCompanyCode"].ToString(), out _error);
                    if (_effect == -1)
                    {
                        _isUserProcessed = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error generated while processing !!!')", true);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _isUserProcessed = true;
                    _error = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error generated while processing !!!')", true);
                    return false;
                }
            }
            else
            {
                if (_user == Session["UserID"].ToString()) _isUserProcessed = false;
                else
                {
                    DataTable _us = CHNLSVC.Inventory.GetUserNameByUserID(_user);
                    string _name = string.Empty;
                    if (_us != null && _us.Rows.Count > 0)
                    {
                        _name = _us.Rows[0].Field<string>("SE_USR_NAME");
                    }
                    _isUserProcessed = true;

                    string msg = "This document is processing by the user " + _user + "-" + _name;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    return false;
                }
            }
            return _isUserProcessed;
        }

        public static decimal RoundUpForPlace(decimal input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * Convert.ToDecimal(multiplier)) / Convert.ToDecimal(multiplier);
        }
        private void Process(bool istemp)
        {
            try
            {
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "m_Trans_Inventory_StockTransferInward", txtdate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);

                string wrongdaterange = (string)Session["WRONGDATERANGE"];
                string allowcurdate = (string)Session["ALLOWCURDATE"];

                if (wrongdaterange == "1")
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date range is not within the required back date range !!!')", true);
                    return;
                }

                if (allowcurdate == "1")
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to carry out the process for the current date !!!')", true);
                    return;
                }

                OutwardType = ddlType.SelectedValue;
                string outwarddate = (string)Session["OUTWARDDATE"];

                DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                DateTime fromdate = Convert.ToDateTime(txtFrom.Text.Trim());
                DateTime todate = Convert.ToDateTime(txtTo.Text.Trim());

                if (gvItem.Rows.Count <= 0)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item !!!')", true);
                    return;
                }

                if (gvSerial.Rows.Count <= 0)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(Session["UserCompanyCode"].ToString()))
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Session expired !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtdate.Text))
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the date !!!')", true);
                    txtdate.Focus();
                    return;
                }

                if (IsDate(txtdate.Text, DateTimeStyles.None) == false)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid date !!!')", true);
                    txtdate.Focus();
                    return;
                }

                if (fromdate > todaydate)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('From date cannot be a future date !!!')", true);
                    txtFrom.Focus();
                    return;
                }

                if (todate < todaydate)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('To date cannot be a back date !!!')", true);
                    txtTo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
                {
                    lblIssuedDocNo.Text = "N/A";
                    LoadDocQtyData(lblIssuedDocNo.Text);
                }

                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    txtRemarks.Text = "N/A";
                }

                if (string.IsNullOrEmpty(txtVehicle.Text))
                {
                    txtVehicle.Text = "N/A";
                }

                if (DateTime.Compare(Convert.ToDateTime(outwarddate).Date, Convert.ToDateTime(txtdate.Text).Date) > 0)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Inward entry date should be greater than or equal to outward entry date !!!')", true);
                    return;
                }

                string issuecompany = (gvPending.SelectedRow.FindControl("lblissecompending") as Label).Text;
                string pendocno = (gvPending.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;
                Label lblIth_entry_no = (gvPending.SelectedRow.FindControl("lblIth_entry_no") as Label);
                Label lbljobnopending = (gvPending.SelectedRow.FindControl("lbljobnopending") as Label);
                Label lblissuelocpending = (gvPending.SelectedRow.FindControl("lblissuelocpending") as Label);
                Label lblissecompending = (gvPending.SelectedRow.FindControl("lblissecompending") as Label);


                InventoryHeader invHdr = new InventoryHeader();
                invHdr.Ith_loc = Session["UserDefLoca"].ToString();
                invHdr.Ith_com = Session["UserCompanyCode"].ToString();
                invHdr.Ith_oth_docno = pendocno;
                if (!string.IsNullOrEmpty(lblIth_entry_no.Text))
                {
                    invHdr.Ith_entry_no = lblIth_entry_no.Text;
                }
                else
                {
                    invHdr.Ith_entry_no = pendocno;
                }

                if (istemp == true)
                {
                    invHdr.Ith_doc_date = Convert.ToDateTime(outwarddate);
                }
                else
                {
                    invHdr.Ith_doc_date = Convert.ToDateTime(txtdate.Text).Date;
                }

                invHdr.Ith_doc_year = Convert.ToDateTime(txtdate.Text).Year;

                if (OutwardType == "AOD")
                {
                    invHdr.Ith_doc_tp = "AOD";
                    invHdr.Ith_cate_tp = "NOR";
                    invHdr.Ith_sub_tp = "NOR";
                }
                if (!string.IsNullOrEmpty(lbljobnopending.Text))
                {
                    invHdr.Ith_job_no = lbljobnopending.Text;
                }
                //else if (OutwardType == "DO")
                //{ 
                //    invHdr.Ith_doc_tp = "GRN"; 
                //    invHdr.Ith_cate_tp = "LOCAL"; 
                //    invHdr.Ith_sub_tp = "LOCAL"; 
                //    invHdr.Ith_oth_docno = Convert.ToString(gvPending.SelectedRows[0].Cells["pen_subdoc"].Value); 
                //    invHdr.Ith_sub_docno = OutwardNo; 
                //}
                //else if (OutwardType == "PRN")
                //{ 
                //    invHdr.Ith_doc_tp = "SRN";
                //    invHdr.Ith_cate_tp = "NOR";
                //    invHdr.Ith_sub_tp = "NOR";
                //    invHdr.Ith_sub_docno = OutwardNo; 
                //}

                PurchaseOrder _pohdr = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(Session["UserCompanyCode"].ToString(), invHdr.Ith_oth_docno);
                PurchaseOrderDetail _poone = new PurchaseOrderDetail();
                List<PurchaseOrderDetail> _poLst = new List<PurchaseOrderDetail>();
                string _supplierclaimcode = string.Empty;
                if (OutwardType == "DO")
                {
                    //_poone.Pod_seq_no = _pohdr.Poh_seq_no; _poLst = CHNLSVC.Inventory.GetPOItems(_poone);
                    //DataTable _supCode = CHNLSVC.Inventory.GetSupplier(Session["UserCompanyCode"].ToString(), _pohdr.Poh_supp);
                    //if (_supCode != null && _supCode.Rows.Count > 0) _supplierclaimcode = _supCode.Rows[0].Field<string>("MBE_CATE");
                }

                if (Session["DocType"].ToString() == "TempDoc")
                {
                    invHdr.Ith_anal_10 = true;
                    invHdr.Ith_anal_2 = txtAODNumber.Text;
                }
                else
                {
                    invHdr.Ith_anal_10 = false;
                    invHdr.Ith_anal_2 = "";
                }

                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = Session["UserID"].ToString();
                invHdr.Ith_mod_by = Session["UserID"].ToString();
                invHdr.Ith_direct = true;
                invHdr.Ith_session_id = Session["SessionID"].ToString();
                invHdr.Ith_manual_ref = "N/A";
                invHdr.Ith_remarks = txtRemarks.Text;
                invHdr.Ith_vehi_no = txtVehicle.Text;
                invHdr.Ith_bus_entity = OutwardType == "DO" ? _pohdr.Poh_supp : string.Empty;
                invHdr.Ith_oth_com = lblIssedCompany.Text;
                invHdr.Ith_oth_loc = lblIssuedLocation.Text;
                invHdr.Ith_pc = Session["UserDefProf"].ToString();
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, Session["UserCompanyCode"].ToString(), pendocno, 1);

                var list = (List<ReptPickSerials>)Session["SERIALLIST"];

                PickSerialsList = list;
                if (PickSerialsList == null)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an outward document number!!!')", true);
                    return;
                }

                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(invHdr.Ith_oth_com, invHdr.Ith_com, invHdr.Ith_doc_tp, pendocno, invHdr.Ith_doc_date.Date, Session["UserID"].ToString());

                PickSerialsList.ForEach(x => x.Tus_doc_dt = invHdr.Ith_doc_date.Date);

                if (invHdr.Ith_doc_tp == "GRN")
                {
                    if (invHdr.Ith_oth_com == "ABL" && invHdr.Ith_com == "LRP")
                    {
                        PickSerialsList.ForEach(x => x.Tus_orig_grndt = invHdr.Ith_doc_date.Date);
                        PickSerialsList.ForEach(x => x.Tus_exist_grndt = invHdr.Ith_doc_date.Date);
                    }
                    if (invHdr.Ith_oth_com == "SGL" && invHdr.Ith_com == "SGD")
                    {
                        PickSerialsList.ForEach(x => x.Tus_orig_grndt = invHdr.Ith_doc_date.Date);
                        PickSerialsList.ForEach(x => x.Tus_exist_grndt = invHdr.Ith_doc_date.Date);
                    }
                }

                List<ReptPickSerialsSub> reptPickSerials_SubList = new List<ReptPickSerialsSub>();
                reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, OutwardType);
                MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                masterAutoNum.Aut_cate_cd = Session["UserDefLoca"].ToString(); masterAutoNum.Aut_cate_tp = "LOC"; masterAutoNum.Aut_direction = 1;
                masterAutoNum.Aut_modify_dt = null; masterAutoNum.Aut_year = Convert.ToDateTime(txtdate.Text).Year;

                string documntNo = string.Empty;
                Int32 result = -99;
                bool _isok = IsUserProcessed(user_seq_num, pendocno);
                if (_isok)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    return;
                }
                try
                {
                    #region Check receving serials are duplicating :: Chamal 08-May-2014
                    string _err = string.Empty;
                    if (CHNLSVC.Inventory.CheckDuplicateSerialFound(invHdr.Ith_com, invHdr.Ith_loc, PickSerialsList, out _err) <= 0)
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft";
                        btnSave.OnClientClick = "ConfirmSave();";
                        string msg2 = "These serial(s) are already available in your location !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                        return;
                    }
                    #endregion

                    if (OutwardType == "AOD")
                    {
                        masterAutoNum.Aut_moduleid = "AOD";
                        masterAutoNum.Aut_start_char = "AOD";
                        System.Threading.Thread.Sleep(1000);
                        #region cost calculate
                        // Add by Lakshan
                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(lblissecompending.Text.Trim().ToUpper(), lblissuelocpending.Text.Trim().ToUpper());
                        /*if (_mstLoc!=null)
                        {
                            if (_mstLoc.Ml_cate_1=="DFS")
                            {
                                List<InventoryBatchN> _intBatch = CHNLSVC.Inventory.Get_Int_Batch(pendocno);
                                var _serialList = PickSerialsList.Where(c=> c.Tus_ser_1 != "N/A").ToList();
                                var _nonSerialList = PickSerialsList.Where(c => c.Tus_ser_1 == "N/A").ToList();
                                foreach (var _itm in _intBatch)
                                {
                                    _itm.TmpBalQty = _itm.Itb_bal_qty1;
                                }
                                foreach (var item in _intBatch)
                                {
                                    foreach (var _ser in _serialList)
                                    {
                                        if (item.TmpBalQty > 0)
                                        {
                                            if (_ser.Tus_itm_cd == item.Inb_itm_cd && _ser.Tus_itm_stus == item.Inb_itm_stus)
                                            {
                                                decimal _actRate = CHNLSVC.Inventory.GetActualRateAodIn(item.Inb_base_ref_no, item.Inb_base_refline);
                                                _ser.Tus_unit_cost = _actRate;
                                                item.TmpBalQty--;
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                foreach (var _nonSer in _nonSerialList)
                                {
                                    foreach (var item in _intBatch)
                                    {
                                        if (item.TmpBalQty > 0)
                                        {
                                             if (_nonSer.Tus_itm_cd == item.Inb_itm_cd && _nonSer.Tus_itm_stus == item.Inb_itm_stus)
                                            {
                                                decimal _actRate = CHNLSVC.Inventory.GetActualRateAodIn(item.Inb_base_ref_no, item.Inb_base_refline);
                                                _nonSer.Tus_unit_cost = _actRate;
                                                item.TmpBalQty--;
                                            }
                                        }
                                    }
                                }
                            }
                        }*/
                        #endregion
                        btnSave.Enabled = false;
                        btnSave.CssClass = "buttoncolorleft";
                        btnSave.OnClientClick = "return Enable();";
                        #region Set variable from where serial pick add by Lakshan 12 Sep 2016
                        invHdr.Ith_acc_no = "SCMWEB";
                        int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, Session["UserCompanyCode"].ToString(), invHdr.Ith_oth_docno, 1);
                        if (_usrSeq != -1)
                        {
                            ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetReportTempPickHdr(new ReptPickHeader()
                            {
                                Tuh_doc_no = invHdr.Ith_oth_docno,
                                Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                                Tuh_doc_tp = ddlType.SelectedValue,
                                Tuh_isdirect = true,
                                Tuh_usrseq_no = _usrSeq,
                            }).FirstOrDefault();
                            if (_ReptPickHeader != null)
                            {
                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                {
                                    invHdr.Ith_loading_point = _ReptPickHeader.Tuh_load_bay;
                                }
                            }
                        }
                        #endregion
                        #region validate aod out com/location
                        InventoryHeader _invHdrOth = CHNLSVC.Inventory.GET_INT_HDR_DATA(new InventoryHeader()
                        {
                            Ith_doc_no = invHdr.Ith_oth_docno,
                            Ith_loc = invHdr.Ith_oth_loc,
                            Ith_com = invHdr.Ith_oth_com
                        }).FirstOrDefault();
                        if (_invHdrOth.Ith_oth_loc != Session["UserDefLoca"].ToString())
                        {
                            DispMsg("Please check the AOD IN location !"); return;
                        }
                        if (_invHdrOth.Ith_oth_com != Session["UserCompanyCode"].ToString())
                        {
                            DispMsg("Please check the AOD IN Company !"); return;
                        }
                        #endregion
                        #region Check Scan Completed
                        _tmpPickHdr = new ReptPickHeader();
                        _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_doc_no = invHdr.Ith_oth_docno,
                            Tuh_doc_tp = "AOD",
                            Tuh_direct = true,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                        }).FirstOrDefault();
                        if (_tmpPickHdr != null)
                        {
                            if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                            {
                                if (_tmpPickHdr.Tuh_fin_stus != 1)
                                {
                                    btnSave.Enabled = false;
                                    btnSave.CssClass = "buttoncolorleft";
                                    btnSave.OnClientClick = "return Enable();";
                                    DispMsg("Scanning is not completed for the selected document !"); return;
                                }
                            }
                        }
                        #endregion
                        #region validate aod in and aod out
                        List<InventoryBatchN> _aodBatchData = CHNLSVC.Inventory.Get_Int_Batch(invHdr.Ith_oth_docno);
                        if (_aodBatchData != null)
                        {
                            if (_aodBatchData.Count > 0)
                            {
                                foreach (var _serData in PickSerialsList)
                                {
                                    foreach (var _batchData in _aodBatchData)
                                    {
                                        if (_serData.Tus_itm_cd == _batchData.Inb_itm_cd && _serData.Tus_itm_stus == _batchData.Inb_itm_stus)
                                        {
                                            _serData.Tus_is_valid_ser = 1;
                                        }
                                    }
                                }
                                var _invalidSerList = PickSerialsList.Where(c => c.Tus_is_valid_ser == 0).ToList();
                                if (_invalidSerList != null)
                                {
                                    if (_invalidSerList.Count > 0)
                                    {
                                        DispMsg("Selected item is different from the requested item !"); return;
                                    }
                                }
                            }
                        }
                        #endregion
                        invHdr.TMP_IS_RES_UPDATE = true;
                        invHdr.TMP_CHK_SER_IS_AVA = true;
                        invHdr.Tmp_update_job_no = true;
                        invHdr.TMP_PROJECT_NAME = "SCMWEB";
                        MasterLocation _mstInLoc = CHNLSVC.General.GetLocationByLocCode(invHdr.Ith_com, invHdr.Ith_loc);
                        if (_mstInLoc != null)
                        {
                            if (_mstInLoc.Ml_loc_tp == "ASSKD")
                            {
                                invHdr.Tmp_is_kd_operation = true;
                            }
                        }
                        invHdr.Ith_gen_frm = "SCMWEB";
                        invHdr.Ith_manual_ref = !string.IsNullOrEmpty(txtManRefNo.Text) ? txtManRefNo.Text.Trim() : invHdr.Ith_manual_ref;
                        result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo, istemp);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft";
                        btnSave.OnClientClick = "ConfirmSave();";
                        #region genarate mail
                        CHNLSVC.MsgPortal.GenarateAodInwardMailAndSMS(Session["UserCompanyCode"].ToString(), documntNo);
                        #endregion
                    }
                    else if (OutwardType == "DO")
                    {
                        #region MyRegion
                        DataTable _lp = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                        var lst = PickSerialsList.Where(x => x.Tus_ser_4 != "1").ToList();
                        foreach (ReptPickSerials s in lst)
                        {
                            string _item = s.Tus_itm_cd; string _status = s.Tus_itm_stus; decimal _qty = s.Tus_qty;
                            var _lpstatus = _lp.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _status).Select(x => x.Field<string>("mis_lp_cd")).ToList();
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _lpstatus[0], "VAT", string.Empty);
                            var actualprice = _poLst.Where(x => x.Pod_itm_stus == _lpstatus[0] && x.Pod_itm_cd == _item).Select(x => x.Pod_act_unit_price).ToList();
                            if (_tax == null || _tax.Count <= 0)
                            {
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft";
                                btnSave.OnClientClick = "ConfirmSave();";
                                string msg3 = "Company item tax is not setup for the " + _item;
                                s.Tus_ser_4 = "0";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg3 + "')", true);
                                return;
                            }
                            else
                            {
                                #region MyRegion
                                if (string.IsNullOrEmpty(_supplierclaimcode))
                                {
                                    s.Tus_unit_price = s.Tus_unit_cost; s.Tus_unit_cost = actualprice[0];
                                }
                                else
                                {
                                    foreach (MasterItemTax _t in _tax)
                                    {
                                        if (_t.Mict_tax_rate > 0)
                                        {
                                            MasterItemTaxClaim _claim = CHNLSVC.Sales.GetTaxClaimDet(Session["UserCompanyCode"].ToString(), _item, _supplierclaimcode);
                                            if (_claim == null || string.IsNullOrEmpty(_claim.Mic_com))
                                            {
                                                btnSave.Enabled = true;
                                                btnSave.CssClass = "buttonUndocolorLeft";
                                                btnSave.OnClientClick = "ConfirmSave();";
                                                string msg5 = "There is no definition for VAT claimable rate for " + _item;
                                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg5 + "')", true);
                                                s.Tus_ser_4 = "0";
                                                return;
                                            }
                                            decimal _comTx = _tax[0].Mict_tax_rate;
                                            decimal _claimableRate = (_claim.Mic_tax_rt - _claim.Mic_claim) / _claim.Mic_tax_rt;
                                            decimal _actualUnitCost = 0; if (_claimableRate > 0) _actualUnitCost = (actualprice[0] * (_comTx + 100) / 100) * (100 - _claimableRate) / 100; else _actualUnitCost = actualprice[0];
                                            s.Tus_unit_price = RoundUpForPlace(s.Tus_unit_cost, 2); s.Tus_unit_cost = RoundUpForPlace(_actualUnitCost, 2);
                                        }
                                        else
                                        {
                                            s.Tus_unit_price = s.Tus_unit_cost; s.Tus_unit_cost = actualprice[0];
                                        }
                                    }
                                }
                                #endregion
                            }
                            s.Tus_itm_stus = _lpstatus[0]; s.Tus_ser_4 = "1";
                        }
                        PickSerialsList.ForEach(x => x.Tus_ser_4 = null);
                        PickSerialsList.OrderBy(X => X.Tus_itm_cd);

                        masterAutoNum.Aut_moduleid = "GRN";
                        masterAutoNum.Aut_start_char = "GRN";
                        masterAutoNum.Aut_direction = null;
                        masterAutoNum.Aut_year = invHdr.Ith_doc_date.Year;

                        result = CHNLSVC.Inventory.GRNEntry(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);
                        #endregion
                    }
                    else if (OutwardType == "PRN")
                    {
                        #region MyRegion
                        DataTable Invoice = null;
                        if (string.IsNullOrEmpty(_dono))
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "buttonUndocolorLeft";
                            btnSave.OnClientClick = "ConfirmSave();";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find the delivery order details !!!')", true);
                            return;
                        }
                        else
                        {
                            Invoice = CHNLSVC.Inventory.GetInvoiceDet(Session["UserCompanyCode"].ToString(), _dono);
                            if (Invoice == null || Invoice.Rows.Count <= 0)
                            {
                                Invoice = CHNLSVC.Inventory.GetSCMInvoiceDet(Session["UserCompanyCode"].ToString(), _dono);
                                if (Invoice == null || Invoice.Rows.Count <= 0)
                                {
                                    btnSave.Enabled = true;
                                    btnSave.CssClass = "buttonUndocolorLeft";
                                    btnSave.OnClientClick = "ConfirmSave();";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid delivery order no !!!')", true);
                                    return;
                                }
                            }
                        }

                        string _invTp = Invoice.Rows[0].Field<string>("sah_inv_tp");
                        string _pc = Invoice.Rows[0].Field<string>("sah_pc");
                        string _invoiceno = Invoice.Rows[0].Field<string>("sah_inv_no");
                        string _customercode = Invoice.Rows[0].Field<string>("sah_cus_cd");

                        _invTp = "CRED";

                        InvoiceHeader _invheader = new InvoiceHeader();
                        _invheader.Sah_com = Session["UserCompanyCode"].ToString(); _invheader.Sah_cre_by = Session["UserID"].ToString();
                        _invheader.Sah_cre_when = DateTime.Now;
                        _invheader.Sah_currency = "LKR"; _invheader.Sah_cus_add1 = string.Empty;
                        _invheader.Sah_cus_add2 = string.Empty; _invheader.Sah_cus_cd = _customercode; _invheader.Sah_cus_name = string.Empty;
                        _invheader.Sah_d_cust_add1 = string.Empty; _invheader.Sah_d_cust_add2 = string.Empty; _invheader.Sah_d_cust_cd = _customercode;
                        _invheader.Sah_direct = false; _invheader.Sah_dt = Convert.ToDateTime(txtdate.Text).Date; _invheader.Sah_epf_rt = 0;
                        _invheader.Sah_esd_rt = 0; _invheader.Sah_ex_rt = 1; _invheader.Sah_inv_no = "na";
                        _invheader.Sah_inv_sub_tp = "REV"; _invheader.Sah_inv_tp = _invTp; _invheader.Sah_is_acc_upload = false;
                        _invheader.Sah_man_cd = "N/A"; _invheader.Sah_man_ref = string.Empty; _invheader.Sah_manual = false;
                        _invheader.Sah_mod_by = Session["UserID"].ToString(); _invheader.Sah_mod_when = DateTime.Now; _invheader.Sah_pc = _pc;
                        _invheader.Sah_pdi_req = 0; _invheader.Sah_ref_doc = _invoiceno; _invheader.Sah_remarks = string.Empty;
                        _invheader.Sah_sales_chn_cd = string.Empty; _invheader.Sah_sales_chn_man = string.Empty; _invheader.Sah_sales_ex_cd = "N/A";
                        _invheader.Sah_sales_region_cd = string.Empty; _invheader.Sah_sales_region_man = string.Empty; _invheader.Sah_sales_sbu_cd = string.Empty;
                        _invheader.Sah_sales_sbu_man = string.Empty; _invheader.Sah_sales_str_cd = string.Empty; _invheader.Sah_sales_zone_cd = string.Empty;
                        _invheader.Sah_sales_zone_man = string.Empty; _invheader.Sah_seq_no = 1;
                        _invheader.Sah_session_id = Session["SessionID"].ToString(); _invheader.Sah_structure_seq = string.Empty;
                        _invheader.Sah_stus = "A"; _invheader.Sah_town_cd = string.Empty;
                        _invheader.Sah_tp = "INV"; _invheader.Sah_wht_rt = 0;
                        _invheader.Sah_tax_inv = false; _invheader.Sah_anal_5 = _dono;
                        MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                        _invoiceAuto.Aut_cate_cd = _pc; _invoiceAuto.Aut_cate_tp = "PC";
                        _invoiceAuto.Aut_direction = 0; _invoiceAuto.Aut_modify_dt = null;
                        _invoiceAuto.Aut_moduleid = "REV"; _invoiceAuto.Aut_number = 0;
                        if (Session["UserCompanyCode"].ToString() == "LRP") _invoiceAuto.Aut_start_char = "RINREV"; else _invoiceAuto.Aut_start_char = "INREV";
                        _invoiceAuto.Aut_year = null;
                        decimal _unitAmt = 0; decimal _disAmt = 0; decimal _taxAmt = 0; decimal _totAmt = 0;
                        List<InvoiceItem> _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForIntrPRN(_invoiceno, "DELIVERD", pendocno);
                        List<InvoiceItem> CreditNoteLst = new List<InvoiceItem>();
                        if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                        {
                            foreach (ReptPickSerials s in PickSerialsList)
                            {
                                List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), "", string.Empty, _invoiceno, Convert.ToInt32(s.Tus_new_status));
                                var _ucost = _serLst.Where(x => x.Tus_ser_id == s.Tus_ser_id).Select(x => x.Tus_unit_cost).ToList();
                                if (_ucost != null && _ucost.Count() > 0) s.Tus_unit_cost = _ucost[0];
                                var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_line == Convert.ToInt32(s.Tus_new_status)).ToList();
                                if (InvoiceItem != null && InvoiceItem.Count > 0)
                                {
                                    foreach (InvoiceItem item in InvoiceItem)
                                    {
                                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty));
                                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt); item.Sad_disc_amt = Convert.ToDecimal(_disAmt); item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt); item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                                        CreditNoteLst.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int q = 1;
                            _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversalSCM(_invoiceno, "DELIVERD", pendocno);
                            if (_paramInvoiceItems != null && _paramInvoiceItems.Count > 0)
                            {
                                foreach (ReptPickSerials s in PickSerialsList)
                                {
                                    int _isSer = 0;
                                    if (s.Tus_ser_1 != "N/A")
                                    {
                                        _isSer = 1;
                                    }

                                    DataTable _dtscmcost = CHNLSVC.Inventory.GetItemCostSerialSCM(_dono, s.Tus_itm_cd, s.Tus_itm_stus, s.Tus_ser_1, _isSer);
                                    if (_dtscmcost != null || _dtscmcost.Rows.Count > 0)
                                    {
                                        s.Tus_unit_cost = _dtscmcost.Rows[0].Field<decimal>("UNIT_COST");
                                    }
                                    else
                                    {
                                        s.Tus_unit_cost = 0;
                                    }

                                    var InvoiceItem = _paramInvoiceItems.Where(x => x.Sad_itm_cd == s.Tus_itm_cd).ToList();
                                    if (InvoiceItem != null && InvoiceItem.Count > 0)
                                    {
                                        foreach (InvoiceItem item in InvoiceItem)
                                        {
                                            _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty)); _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(s.Tus_qty));
                                            item.Sad_unit_amt = Convert.ToDecimal(_unitAmt); item.Sad_disc_amt = Convert.ToDecimal(_disAmt); item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt); item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                                            item.Sad_itm_line = q;
                                            CreditNoteLst.Add(item);
                                            q += 1;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft";
                                btnSave.OnClientClick = "ConfirmSave();";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invoiced items were not found !!!')", true);
                                return;
                            }
                        }

                        _invheader.Sah_anal_7 = CreditNoteLst.Sum(X => X.Sad_tot_amt);
                        masterAutoNum.Aut_moduleid = "SRN"; masterAutoNum.Aut_start_char = "SRN";
                        PickSerialsList.OrderBy(X => X.Tus_itm_cd); string _crno = string.Empty;
                        invHdr.Ith_oth_loc = string.Empty; invHdr.Ith_cate_tp = "INTR";
                        result = CHNLSVC.Sales.SaveReversalNew(_invheader, CreditNoteLst, _invoiceAuto, false, out _crno, invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, null, null, null, false, null, null, null, false, false, _invheader.Sah_pc, null, null, null, null, null, false, out documntNo);
                        #endregion
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft";
                        btnSave.OnClientClick = "ConfirmSave();";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid outward document type found !!!')", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    return;
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }

                if (result != -99 && result > 0)
                {
                    PickSerialsList.ForEach(x => x.Tus_com = issuecompany);
                    string _refdc = pendocno;
                    if (OutwardType != "AOD")
                    {
                        CHNLSVC.Inventory.SetOffRefDocumentSerial(PickSerialsList, _refdc);
                    }
                    string Msg = "AOD Receipt Successfully Saved! Document No. : " + documntNo + "";
                    Session["documntNo"] = documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                    Clear();
                    BindOutwardListGridData();
                    lblMssg.Text = "Do you want print now?";
                    PopupConfBox.Show();
                    //InitializeComponent();
                    //InitializeForm();
                }
                else
                {
                    string Msg = "" + documntNo;
                    Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                    if (Msg.Contains("FK_INBBIN"))
                    {
                        Msg = "Picked serial bin code is invalid ! ";
                    }
                    if (Msg.Contains("CHK_ITBBALQTY1"))
                    {
                        Msg = "Please check the inventory balances ! ";
                    }
                    if (Msg.Contains("Aod out item quentity exceed"))
                    {
                        Msg = "AOD Out item quantity cannot be exceeded !";
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "')", true);
                    CHNLSVC.CloseChannel();
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft";
                    btnSave.OnClientClick = "ConfirmSave();";
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft";
                btnSave.OnClientClick = "ConfirmSave();";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16022))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have permission for this function.Permission Code :- 16022 !!!')", true);
                        return;
                    }
                    btnSave.Enabled = false;
                    btnSave.CssClass = "buttoncolorleft";
                    btnSave.OnClientClick = "return Enable();";
                    Session["DocType"] = "Doc";
                    #region add pop for patial in by lakshan 29 Apr 2017
                    _partialSave = true;
                    string pendocno = (gvPending.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;
                    if (!string.IsNullOrEmpty(pendocno))
                    {
                        List<InventoryBatchN> _intOutBatch = CHNLSVC.Inventory.Get_Int_Batch(pendocno);
                        decimal _outWardCount = _intOutBatch.Sum(c => c.Itb_bal_qty1);
                        ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                            Tuh_doc_no = pendocno,
                            Tuh_direct = true,
                            Tuh_doc_tp = ddlType.SelectedValue
                        }).FirstOrDefault();
                        if (_tmpPickHdr != null)
                        {
                            List<ReptPickSerials> _repPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                            decimal _pickQty = _repPickSerList.Sum(c => c.Tus_qty);
                            if (_outWardCount != _pickQty)
                            {
                                _partialSave = false;
                            }
                        }
                    }
                    #endregion
                    if (!_partialSave)
                    {
                        lblPartalSave.Text = "Do you want to partial save ? ";
                        PopupPartialSave.Show();
                    }
                    else
                    {
                        Process(false);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft";
                        btnSave.OnClientClick = "ConfirmSave();";
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtapproveconfirm.Value == "Yes")
                {
                    string ordstatus = (string)Session["STATUS"];

                    if (string.IsNullOrEmpty(txtAODNumber.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an entry !!!')", true);
                        lbtnsearchrec.Focus();
                        return;
                    }

                    if (ordstatus == "A")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This entry has been already approved !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    InventoryHeader InvHdr = new InventoryHeader();

                    InvHdr.Ith_stus = "A";
                    InvHdr.Ith_doc_no = txtAODNumber.Text.Trim();
                    InvHdr.Ith_mod_by = _userid;
                    InvHdr.Ith_mod_when = CHNLSVC.Security.GetServerDateTime();

                    Int32 outputresult = CHNLSVC.Inventory.UpdateStockInStatus(InvHdr);

                    if (outputresult == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcancenconfirm.Value == "Yes")
                {
                    string ordstatus = (string)Session["STATUS"];

                    if (string.IsNullOrEmpty(txtAODNumber.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an entry !!!')", true);
                        lbtnsearchrec.Focus();
                        return;
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This entry has been already cancelled !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    InventoryHeader InvHdr = new InventoryHeader();

                    InvHdr.Ith_stus = "C";
                    InvHdr.Ith_doc_no = txtAODNumber.Text.Trim();
                    InvHdr.Ith_mod_by = _userid;
                    InvHdr.Ith_mod_when = CHNLSVC.Security.GetServerDateTime();

                    Int32 outputresult = CHNLSVC.Inventory.UpdateStockInStatus(InvHdr);

                    if (outputresult == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        protected void lbtnreject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtrejectconfirm.Value == "Yes")
                {
                    string ordstatus = (string)Session["STATUS"];

                    if (string.IsNullOrEmpty(txtAODNumber.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an entry !!!')", true);
                        lbtnsearchrec.Focus();
                        return;
                    }

                    if (ordstatus == "R")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This entry has been already rejected !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    InventoryHeader InvHdr = new InventoryHeader();

                    InvHdr.Ith_stus = "R";
                    InvHdr.Ith_doc_no = txtAODNumber.Text.Trim();
                    InvHdr.Ith_mod_by = _userid;
                    InvHdr.Ith_mod_when = CHNLSVC.Security.GetServerDateTime();

                    Int32 outputresult = CHNLSVC.Inventory.UpdateStockInStatus(InvHdr);

                    if (outputresult == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully rejected !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        private void GetDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    bool _invalidDoc = true;
                    int _direction = 1;
                    int _lineNo = 0;
                    _direction = 1;

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _emptySer;
                    Session["gvSerData"] = _emptySer;
                    gvSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    gvItem.AutoGenerateColumns = false;
                    gvItem.DataSource = new int[] { };
                    Session["gvSerData"] = _emptySer;
                    gvItem.DataBind();
                    BindGridStatusItem();

                    btnSave.Enabled = true;
                    btnSave.OnClientClick = "ConfirmSave();";
                    btnSave.CssClass = "buttonUndocolor";

                    LinkButton1.Enabled = true;
                    LinkButton1.OnClientClick = "ConfirmSave();";
                    LinkButton1.CssClass = "buttonUndocolor";
                    txtRemarks.Text = string.Empty;

                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);

                    Session["STATUS"] = _invHdr.Ith_stus;

                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "AOD")
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
                        txtAODNumber.Text = "";
                        txtAODNumber.Focus();
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnSave.OnClientClick = "return Enable();";
                        btnSave.CssClass = "buttoncolor";

                        LinkButton1.Enabled = false;
                        LinkButton1.OnClientClick = "return Enable();";
                        LinkButton1.CssClass = "buttoncolor";
                        txtRemarks.Text = _invHdr.Ith_remarks;
                    }
                    #endregion

                    #region Get SerialslbtnPrint_Click
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtAODNumber.Text);
                    InventoryRequestItem _itm = new InventoryRequestItem();

                    if (_serList != null)
                    {
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[5] { new DataColumn("Tus_itm_cd"), new DataColumn("Tus_itm_desc"), new DataColumn("Tus_itm_model"), new DataColumn("Tus_itm_stus"), new DataColumn("Tus_qty") });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itmList.Add(_itm);
                            dttemp2.Rows.Add(_itm.Itri_itm_cd, _itm.Mi_longdesc, _itm.Mi_model, _itm.Itri_itm_stus, _itm.Itri_app_qty);
                        }
                        ScanItemList = _itmList;
                        gvItem.AutoGenerateColumns = false;

                        gvItem.DataSource = null;
                        gvItem.DataSource = dttemp2;
                        gvItem.DataBind();
                        BindGridStatusItem();

                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;
                        Session["gvSerData"] = _serList;
                        gvSerial.DataBind();
                        BindGridStatusSerial();
                    }
                    else
                    {
                        txtAODNumber.Text = "";
                        txtAODNumber.Focus();
                        return;
                    }
                    #endregion
                    foreach (GridViewRow gvr in gvItem.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                        LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                        Delrow.Enabled = false;
                        Delrow.OnClientClick = "return Enable();";

                    }
                    foreach (GridViewRow gvr in gvSerial.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;

            if (lblvalue.Text == "Doc")
            {
                Session["POPUP_LOADED"] = "1";
                txtAODNumber.Text = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                GetDocData(Name);
                LoadHeader(false);
                Session["documntNo"] = Name;

                foreach (GridViewRow hiderowbtn in this.gvSerial.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndeleteserial");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttoncolor";
                    _delbutton.OnClientClick = "return Enable();";
                }

                UserDPopoup.Hide();

                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                Session["POPUP_LOADED"] = null;
                txtAODNumber.Text = Name;
                lblvalue.Text = "";
                Session["TempDoc"] = "";
                Session["DocType"] = "TempDoc";
                GetTempDocData(Name);
                Session["documntNo"] = Name;
                LoadHeader(true);

                foreach (GridViewRow hiderowbtn in this.gvSerial.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndeleteserial");

                    _delbutton.Enabled = true;
                    _delbutton.CssClass = "buttonUndocolor";
                    _delbutton.OnClientClick = "ConfirmDelete();";
                }

                string outwrddate = (gvPending.SelectedRow.FindControl("lbldatepending") as Label).Text;
                hdnOutwarddate = Convert.ToDateTime(outwrddate);
                Session["OUTWARDDATE"] = outwrddate;

                UserDPopoup.Hide();
                return;
            }
        }

        private void LoadHeader(bool istemp)
        {
            try
            {
                gvPending.DataSource = null;
                gvPending.DataBind();

                DataTable dtheader = new DataTable();

                if (istemp == true)
                {
                    dtheader = CHNLSVC.Inventory.GetTempDocHeaderData(txtAODNumber.Text.Trim());
                }
                else
                {
                    dtheader = CHNLSVC.Inventory.GetDocHeaderData(txtAODNumber.Text.Trim());
                }
                //TMP_Tuh_fin_stus
                if (!dtheader.Columns.Contains("TMP_Tuh_fin_stus"))
                {
                    dtheader.Columns.Add("TMP_Tuh_fin_stus", typeof(Int32));
                }
                gvPending.DataSource = dtheader;
                _dtPendingDoc = dtheader;
                gvPending.DataBind();
                gvPending.SelectedIndex = 0;

                lblIssuedDocNo.Text = (gvPending.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;
                LoadDocQtyData(lblIssuedDocNo.Text);
                lblIssedCompany.Text = (gvPending.SelectedRow.FindControl("lblissecompending") as Label).Text;
                lblIssuedLocation.Text = (gvPending.SelectedRow.FindControl("lblissuelocpending") as Label).Text;
                string location = string.Empty;

                DataTable _tbl = null;
                if (!string.IsNullOrEmpty(lblIssuedLocation.Text) && !string.IsNullOrEmpty(lblIssedCompany.Text))
                    _tbl = CHNLSVC.Inventory.Get_location_by_code(lblIssedCompany.Text.Trim(), lblIssuedLocation.Text.Trim());
                if (_tbl != null && _tbl.Rows.Count > 0)

                    foreach (DataRow ddr in _tbl.Rows)
                    {
                        location = ddr["ml_loc_desc"].ToString();
                    }

                lblIssueLocDesc.Text = location;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_New(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                _result.Columns.Remove("date");
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
        }


        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_New(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_New(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtdate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
        }


        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }

        protected void lbtnsearchrec_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;

                grdResultD.DataSource = null;
                grdResultD.DataBind();
                var sortedTable = new DataTable();
                if (chktemp.Checked == true)
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtdate.Text));
                    lblvalue.Text = "TempDoc";
                    Session["TempDoc"] = "true";
                    sortedTable = _result.AsEnumerable()
              .OrderByDescending(r => r.Field<String>("Document"))
              .CopyToDataTable();

                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_New(SearchParams, null, null, Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtdate.Text));
                    lblvalue.Text = "Doc";
                    Session["Doc"] = "true";
                    if (_result.Rows.Count > 0)
                    {
                        sortedTable = _result.AsEnumerable()
               .OrderByDescending(r => r.Field<String>("Document"))
               .CopyToDataTable();
                        grdResultD.DataSource = sortedTable;
                        grdResultD.DataBind();
                        sortedTable.Columns.Remove("Date");
                        BindUCtrlDDLData2(sortedTable);
                    }
                    grdResultD.DataSource = new int[] { };
                    grdResultD.DataBind();
                    BindUCtrlDDLData2(_result);
                }


                txtFDate.Text = Convert.ToDateTime(txtdate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtdate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayMessage("Please Contact  IT Department for further instructions."); return;
                if (txtsaveconfirm.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16033))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You do not have permission for this function.Permission Code :- 16033 !!!')", true);
                        return;
                    }

                    Session["DocType"] = "TempDoc";
                    Process(true);
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            _direction = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", _direction, Session["UserCompanyCode"].ToString());
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "AOD";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                Session["SEQNO"] = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        private void GetTempDocData(string DocNo)
        {
            try
            {
                Session["SERIALLIST"] = null;
                if (!string.IsNullOrEmpty(DocNo))
                {
                    Int32 affected_rows;
                    bool _invalidDoc = true;
                    int _direction = 1;
                    int _lineNo = 0;
                    _direction = 1;

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _emptySer;
                    Session["gvSerData"] = _emptySer;
                    gvSerial.DataBind();
                    BindGridStatusSerial();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    gvItem.AutoGenerateColumns = false;
                    gvItem.DataSource = new int[] { };
                    gvItem.DataBind();

                    btnSave.Enabled = true;
                    btnSave.OnClientClick = "ConfirmSave();";
                    btnSave.CssClass = "buttonUndocolor";

                    LinkButton1.Enabled = true;
                    LinkButton1.OnClientClick = "ConfirmSave();";
                    LinkButton1.CssClass = "buttonUndocolor";

                    txtRemarks.Text = string.Empty;
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    Session["STATUS"] = _invHdr.Ith_stus;

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "AOD")
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
                        txtAODNumber.Text = "";
                        txtAODNumber.Focus();
                        return;
                    }
                    else
                    {
                        foreach (GridViewRow gvr in gvItem.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                            LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;

                            Addrow.Enabled = true;
                            Delrow.Enabled = true;
                        }
                        foreach (GridViewRow gvr in gvSerial.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;

                            Addrow.Enabled = true;
                        }
                        btnSave.Enabled = true;
                        btnSave.OnClientClick = "ConfirmSave()();";
                        btnSave.CssClass = "buttonUndocolor";

                        LinkButton1.Enabled = false;
                        LinkButton1.OnClientClick = "return Enable();";
                        LinkButton1.CssClass = "buttoncolor";

                        txtRemarks.Text = _invHdr.Ith_remarks;
                    }
                    #endregion

                    LoadItems(_invHdr.Ith_seq_no.ToString());

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("AOD", Session["UserCompanyCode"].ToString(), txtAODNumber.Text, 0);
                    if (user_seq_num != -1)
                    {
                        _invHdr.Ith_seq_no = GenerateNewUserSeqNo();
                    }
                    else
                    {
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "AOD";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = Convert.ToInt32(_invHdr.Ith_seq_no.ToString());

                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = DocNo;
                        affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();

                    _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtAODNumber.Text);
                    InventoryRequestItem _itm = new InventoryRequestItem();

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;

                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itmList.Add(_itm);


                            List<InventoryRequestItem> _ListItem = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                            if (_ListItem.Count == 0)
                            {
                                AddItem(itm.Peo.Tus_itm_cd, itm.Peo.Tus_unit_cost.ToString(), itm.Peo.Tus_itm_stus, itm.theCount.ToString(), _invHdr.Ith_seq_no.ToString(), null);
                            }
                        }

                        ScanItemList = _itmList;

                        foreach (ReptPickSerials serial in _serList)
                        {
                            serial.Tus_usrseq_no = Convert.ToInt32(_invHdr.Ith_seq_no.ToString());

                            List<ReptPickSerials> _Listserial = ViewState["SerialList"] as List<ReptPickSerials>;
                            if ((_Listserial.Count == 0))
                            {
                                affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                            }
                        }
                        gvItem.AutoGenerateColumns = false;
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[5] { new DataColumn("Tus_itm_cd"), new DataColumn("Tus_itm_desc"), new DataColumn("Tus_itm_model"), new DataColumn("Tus_itm_stus"), new DataColumn("Tus_qty") });
                        dttemp2.Rows.Add(_itm.Itri_itm_cd, _itm.Mi_longdesc, _itm.Mi_model, _itm.Itri_itm_stus, _itm.Itri_app_qty);
                        gvItem.DataSource = null;
                        gvItem.DataSource = dttemp2;
                        gvItem.DataBind();
                        BindGridStatusItem();

                        ViewState["TempScanItemList"] = ScanItemList;
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;
                        Session["gvSerData"] = _serList;
                        gvSerial.DataBind();
                        BindGridStatusSerial();
                        ViewState["TempserList"] = _serList;
                        Session["SERIALLIST"] = _serList;
                    }
                    else
                    {
                        txtAODNumber.Text = "";
                        txtAODNumber.Focus();
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception err)
            {
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                _direction = 1;

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "AOD", Session["UserID"].ToString(), _direction, _seqNo);
                if (_seqNo == "")
                {
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                }
                else
                {
                    user_seq_num = Convert.ToInt32(_seqNo);
                }

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                gvItem.AutoGenerateColumns = false;
                gvItem.DataSource = ScanItemList;
                gvItem.DataBind();
                BindGridStatusItem();
                ViewState["ScanItemList"] = ScanItemList;

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "AOD");
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("gvItem")).Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                {
                                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                }
                            }
                        }
                    }
                    else
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (GridViewRow row in gvItem.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitem")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                {
                                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                }
                            }
                        }
                    }
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = _serList;
                    Session["gvSerData"] = _serList;
                    gvSerial.DataBind();
                    BindGridStatusSerial();
                    ViewState["SerialList"] = _serList;
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = emptyGridList;
                    Session["gvSerData"] = emptyGridList;
                    gvSerial.DataBind();
                    BindGridStatusSerial();
                    ViewState["SerialList"] = emptyGridList;
                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
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
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item is already available !!!');", true);
                                return;
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty = 0;
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                ScanItemList.Add(_itm);
                            }
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    _UserSeqNo = GenerateNewUserSeqNo().ToString();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(2);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                DataTable dttemp = new DataTable();
                dttemp.Columns.AddRange(new DataColumn[5] { new DataColumn("Tus_itm_cd"), new DataColumn("Tus_itm_desc"), new DataColumn("Tus_itm_model"), new DataColumn("Tus_itm_stus"), new DataColumn("Tus_qty") });
                dttemp.Rows.Add(_itm.Itri_itm_cd, _itm.Mi_longdesc, _itm.Mi_model, _itm.Itri_itm_stus, _itm.Itri_app_qty);

                gvItem.DataSource = null;
                gvItem.DataSource = dttemp;
                gvItem.DataBind();
                BindGridStatusItem();
                ViewState["ScanItemList"] = ScanItemList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Hide();
                ddlloadingbay.SelectedIndex = 0;
                txtdocname.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void ddlloadingbay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtpdasend.Value == "Yes")
            {
                try
                {
                    SaveData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }
            }
        }

        private void SaveData()
        {
            try
            {
                //if (gvItem.Rows.Count == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No Items found !!!')", true);
                //    gvItem.Focus();
                //    MPPDA.Show();
                //    return;
                //}

                if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                    txtdocname.Focus();
                    MPPDA.Show();
                    return;
                }

                if (ddlloadingbay.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                    ddlloadingbay.Focus();
                    MPPDA.Show();
                    return;
                }

                Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", 1, Session["UserCompanyCode"].ToString());
                _userid = (string)Session["UserID"];
                Int32 val = 0;

                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                #region add to issue fix not data update correctly 18 Feb 2017
                MasterLocationNew _mstLocPda = CHNLSVC.General.GetLocationDataForPdaSend(new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = Session["UserDefLoca"].ToString()
                });
                if (_mstLocPda == null)
                {
                    DispMsg("Please check the warahouse company !"); return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_mstLocPda.Ml_wh_com) && !string.IsNullOrEmpty(_mstLocPda.Ml_wh_cd))
                    {
                        warehousecom = _mstLocPda.Ml_wh_com;
                        warehouseloc = _mstLocPda.Ml_wh_cd;
                    }
                    else
                    {
                        DispMsg("Please check the warahouse company and code !"); return;
                    }
                }
                #endregion
                #region Header
                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(txtdocname.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());

                if (dtdoccheck.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtdoccheck.Rows)
                    {
                        string seqNo = ddr["tuh_usrseq_no"].ToString();
                        _userSeqNo = Convert.ToInt32(seqNo);
                    }
                }
                #region Add by lakshan Check allready send to pda 19 sep 2016
                ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                {
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_doc_no = txtdocname.Text,
                    Tuh_direct = true,
                    Tuh_doc_tp = ddlType.SelectedValue
                }).FirstOrDefault();

                List<ReptPickSerials> _repPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _userSeqNo });
                if (_repPickSerList != null)
                {
                    if (_repPickSerList.Count > 0)
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                        string _msg = "";
                        if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                        }
                        else
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                        }
                        DisplayMessage(_msg);
                        chkpda.Checked = false;
                        return;
                    }
                }
                List<ReptPickItems> _repPickItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _userSeqNo });
                if (_repPickItmList != null)
                {
                    if (_repPickItmList.Count > 0)
                    {
                        string _msg = "";
                        if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                        }
                        else
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                        } DisplayMessage(_msg);
                        chkpda.Checked = false;
                        return;
                    }
                }
                #endregion
                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);

                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                    return;
                }

                if (dtdoccheck.Rows.Count == 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = _userid;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }

                #endregion

                if (Convert.ToInt32(val) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
                else
                {
                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    List<InventoryBatchN> _Item = CHNLSVC.Inventory.Get_Int_Batch(txtdocname.Text.Trim());

                    var _batchData = _Item.GroupBy(x => new { x.Inb_itm_cd, x.Inb_itm_stus, x.Inb_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Itb_bal_qty1) });
                    foreach (var _dr in _batchData)
                    {
                        if (_dr.theCount > 0)
                        {
                            ReptPickItems _itm = new ReptPickItems();
                            _itm.Tui_req_itm_cd = _dr.Peo.Inb_itm_cd;
                            _itm.Tui_req_itm_qty = _dr.theCount;
                            _itm.Tui_req_itm_stus = _dr.Peo.Inb_itm_stus;
                            _itm.Tui_pic_itm_cd = Convert.ToString(_dr.Peo.Inb_itm_line);//Darshana request add by rukshan
                            _itm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                            _saveonly.Add(_itm);
                        }
                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
                #region Items

                //Int32 rownumber = 0;
                //Int32 rowvalline = 0;

                //foreach (GridViewRow row in gvItem.Rows)
                //{
                //    Label lblitem = (Label)row.FindControl("lblitem");
                //    Label lblstatus = (Label)row.FindControl("lblstatus");
                //    Label lblqty = (Label)row.FindControl("lblqty");

                //    ReptPickItems _items = new ReptPickItems();

                //    _items.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                //    _items.Tui_req_itm_cd = lblitem.Text.Trim();
                //    _items.Tui_req_itm_stus = lblstatus.Text.Trim();
                //    _items.Tui_pic_itm_qty = 0;
                //    _items.Tui_req_itm_qty = Convert.ToDecimal(lblqty.Text.Trim());
                //    _items.Tui_pic_itm_cd = rownumber.ToString();
                //    _items.Tui_pic_itm_stus = string.Empty;
                //    _items.Tui_grn = string.Empty;
                //    _items.Tui_batch = string.Empty;
                //    _items.Tui_sup = string.Empty;

                //    valitem = CHNLSVC.Inventory.UpdatePickItem(_items);

                //    DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo), warehousecom, warehouseloc, ddlloadingbay.SelectedValue);
                //    foreach (DataRow ddrrownum in dtrownum.Rows)
                //    {
                //        rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString());
                //    }

                //    ReptPickItems _itemslines = new ReptPickItems();

                //    _itemslines.Tui_pic_itm_cd = rownumber.ToString();
                //    _itemslines.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                //    _itemslines.Tui_req_itm_cd = lblitem.Text.Trim();
                //    _itemslines.Tui_req_itm_stus = lblstatus.Text.Trim();

                //    rowvalline = CHNLSVC.Inventory.UpdatePickItemLine(_itemslines);

                //    if (Convert.ToInt32(valitem) == -1)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                //        CHNLSVC.CloseChannel();
                //        return;
                //    }

                //    if (Convert.ToInt32(rowvalline) == -1)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                //        CHNLSVC.CloseChannel();
                //        return;
                //    }
                //}
                #endregion

                if (dtdoccheck.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document sent to PDA Successfully !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document was successfully updated !!!')", true);
                }
                Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void btnAddSerials_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblitem = dr.FindControl("lblitem") as Label;
            Label lblstatus = dr.FindControl("lblstatus") as Label;

            lblPopupItemCode.Text = lblitem.Text;
            lblItemStatus.Text = lblstatus.Text;

            MasterItem oItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitem.Text.Trim());
            if (oItem.Mi_is_ser1 == 1)
            {
                txtSerial1Add.ReadOnly = false;
            }
            else
            {
                txtSerial1Add.ReadOnly = true;
            }

            if (oItem.Mi_is_ser2 == 1)
            {
                txtSerial2Add.ReadOnly = false;
            }
            else
            {
                txtSerial2Add.ReadOnly = true;
            }

            Session["oNewSerials"] = null;

            grdAdSearch.DataSource = new int[] { };
            grdAdSearch.DataBind();

            txtSerial1Add.Text = "";
            txtSerial2Add.Text = "";

            txtSerial1Add.Focus();
            mpPickSerial.Show();
        }

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }


        #region Pick Serial
        protected void btnPSPClose_Click(object sender, EventArgs e)
        {
            mpPickSerial.Hide();
            Session["mpPickSerial"] = null;
        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            mpPickSerial.Show();
        }

        protected void btnAdvanceAddItemNew_Click(object sender, EventArgs e)
        {
            mpPickSerial.Show();
            if (string.IsNullOrEmpty(txtSerial1Add.Text.Trim()) && string.IsNullOrEmpty(txtSerial2Add.Text.Trim()))
            {
                DisplayMessageJS("Enter serial number");
                return;
            }
            List<ReptPickSerials> oNewSerials = null;
            if (Session["oNewSerials"] == null)
            {
                oNewSerials = new List<ReptPickSerials>();
            }
            else
            {
                oNewSerials = (List<ReptPickSerials>)Session["oNewSerials"];
            }
            if (oNewSerials.FindAll(x => x.Tus_ser_1 == txtSerial1Add.Text.Trim() && x.Tus_ser_2 == txtSerial2Add.Text.Trim()).Count > 0)
            {
                DisplayMessageJS("New serial is already added.");
                return;
            }
            else
            {
                ReptPickSerials oSerial = new ReptPickSerials();
                oSerial.Tus_ser_1 = txtSerial1Add.Text.Trim().Replace(" ", "_");
                oSerial.Tus_ser_2 = txtSerial2Add.Text.Trim().Replace(" ", "_");
                oSerial.Tus_itm_cd = lblPopupItemCode.Text.Trim();
                oSerial.Tus_itm_stus = lblItemStatus.Text.Trim();
                oSerial.Tus_usrseq_no = Convert.ToInt32(Session["UsSeq"]);

                oNewSerials.Add(oSerial);

                txtSerial1Add.Text = "";
                txtSerial2Add.Text = "";
            }

            Session["oNewSerials"] = oNewSerials;
            grdAdSearch.DataSource = oNewSerials;
            grdAdSearch.DataBind();
            txtSerial1Add.Focus();
        }

        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        protected void btnDelPickSer_Click(object sender, EventArgs e)
        {
            mpPickSerial.Show();

            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblTus_ser_1 = dr.FindControl("lblTus_ser_1") as Label;
            Label lblTus_ser_2 = dr.FindControl("lblTus_ser_2") as Label;

            List<ReptPickSerials> oNewSerials = null;
            if (Session["oNewSerials"] != null)
            {
                oNewSerials = (List<ReptPickSerials>)Session["oNewSerials"];
            }
            oNewSerials.RemoveAll(x => x.Tus_ser_1 == lblTus_ser_1.Text.Trim() && x.Tus_ser_2 == lblTus_ser_2.Text);
            Session["oNewSerials"] = oNewSerials;
            grdAdSearch.DataSource = oNewSerials;
            grdAdSearch.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<ReptPickSerials> oNewSerials = null;
            if (Session["oNewSerials"] != null)
            {
                oNewSerials = (List<ReptPickSerials>)Session["oNewSerials"];
            }
            else
            {
                oNewSerials = new List<ReptPickSerials>();
            }

            List<ReptPickSerials> oINT_ITM = CHNLSVC.Sales.GetInvItem(lblIssuedDocNo.Text.Trim());
            if (oINT_ITM != null && oINT_ITM.Count > 0)
            {
                foreach (ReptPickSerials _pick in oNewSerials)
                {
                    ReptPickSerials oItem = oINT_ITM.Find(t => t.Tus_itm_cd == _pick.Tus_itm_cd && t.Tus_itm_stus == _pick.Tus_itm_stus);

                    MasterItem _itmlist = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _pick.Tus_itm_cd.Trim());
                    if (_itmlist != null)
                    {
                        _pick.Tus_base_doc_no = lblIssuedDocNo.Text.Trim();
                        _pick.Tus_base_itm_line = oItem.Tus_itm_line;
                        _pick.Tus_batch_line = oItem.Tus_batch_line;
                        _pick.Tus_bin = Session["GlbDefaultBin"].ToString();// oItem.Tus_bin;
                        _pick.Tus_com = Session["UserCompanyCode"].ToString();
                        _pick.Tus_cre_by = Session["UserID"].ToString();
                        _pick.Tus_cre_dt = System.DateTime.Now;
                        _pick.Tus_cross_batchline = oItem.Tus_base_itm_line;
                        _pick.Tus_cross_itemline = oItem.Tus_itm_line;
                        _pick.Tus_cross_seqno = oItem.Tus_cross_seqno;
                        _pick.Tus_cross_serline = oItem.Tus_ser_line;
                        _pick.Tus_doc_dt = System.DateTime.Now.Date;
                        _pick.Tus_doc_no = lblIssuedDocNo.Text.Trim();

                        //if (_outwardType == "AOD")
                        //{
                        //    _pick.Tus_exist_grncom = _dr["ITS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNCOM"];
                        //    _pick.Tus_exist_grnno = _dr["ITS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNNO"];
                        //    _pick.Tus_exist_grndt = _dr["ITS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_EXIST_GRNDT"];
                        //    _pick.Tus_exist_supp = _dr["ITS_EXIST_SUPP"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_SUPP"];
                        //    _pick.Tus_itm_stus = (String)_dr["ITS_ITM_STUS"];
                        //    _pick.Tus_unit_price = Convert.ToDecimal(_dr["ITB_UNIT_PRICE"]);

                        //    _pick.Tus_ageloc = _dr["ITS_AGELOC"] == DBNull.Value ? string.Empty : (String)_dr["ITS_AGELOC"];
                        //    _pick.Tus_ageloc_dt = _dr["ITS_AGELOC_DT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_AGELOC_DT"];
                        //    if (string.IsNullOrEmpty(_dr["ITS_ISOWNMRN"].ToString()))
                        //    { _pick.Tus_isownmrn = 0; }
                        //    else
                        //    { _pick.Tus_isownmrn = Convert.ToInt32(_dr["ITS_ISOWNMRN"]); }

                        //}
                        //else
                        //{
                        //    _pick.Tus_exist_grncom = _dr["ITS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNCOM"];
                        //    _pick.Tus_exist_grnno = _dr["ITS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNNO"];
                        //    _pick.Tus_exist_grndt = _dr["ITS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_EXIST_GRNDT"];
                        //    _pick.Tus_exist_supp = _dr["ITS_EXIST_SUPP"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_SUPP"];
                        //    _pick.Tus_itm_stus = (String)_dr["ITS_ITM_STUS"];
                        //    _pick.Tus_unit_price = Convert.ToDecimal(_dr["ITB_UNIT_PRICE"]);
                        //}

                        _pick.Tus_itm_brand = _itmlist.Mi_brand;
                        _pick.Tus_itm_cd = oItem.Tus_itm_cd;
                        _pick.Tus_itm_desc = _itmlist.Mi_longdesc;
                        _pick.Tus_itm_line = oItem.Tus_itm_line;
                        _pick.Tus_itm_model = _itmlist.Mi_model;
                        _pick.Tus_loc = Session["UserDefLoca"].ToString();
                        _pick.Tus_new_remarks = String.Empty;
                        _pick.Tus_new_status = String.Empty;

                        //_pick.Tus_orig_grncom = _dr["ITS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_GRNCOM"];
                        //_pick.Tus_orig_grndt = _dr["ITS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_ORIG_GRNDT"];
                        //_pick.Tus_orig_grnno = _dr["ITS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_GRNNO"];
                        //_pick.Tus_orig_supp = _dr["ITS_ORIG_SUPP"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_SUPP"];

                        _pick.Tus_out_date = DateTime.Now.Date;
                        _pick.Tus_qty = 1;
                        _pick.Tus_seq_no = Convert.ToInt32(Session["UsSeq"].ToString());
                        _pick.Tus_ser_1 = _pick.Tus_ser_1;
                        _pick.Tus_ser_2 = _pick.Tus_ser_2;
                        _pick.Tus_ser_3 = string.Empty;
                        _pick.Tus_ser_4 = string.Empty;

                        List<InventorySerialN> _intSerList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                        {
                            Ins_doc_no = lblIssuedDocNo.Text,
                            Ins_com = lblIssedCompany.Text,
                            Ins_loc = lblIssuedLocation.Text
                        });
                        bool serIdAvailable = false;
                        if (_intSerList != null)
                        {
                            if (_intSerList.Count > 0)
                            {
                                var _serData = _intSerList.Where(c => c.Ins_ser_1 == _pick.Tus_ser_1).FirstOrDefault();
                                if (_serData != null)
                                {
                                    _pick.Tus_ser_id = _serData.Ins_ser_id;
                                    serIdAvailable = true;
                                }
                            }
                        }
                        if (!serIdAvailable)
                        {
                            _pick.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        }
                        _pick.Tus_ser_line = 0;
                        _pick.Tus_serial_id = String.Empty;
                        _pick.Tus_session_id = Session["SessionID"].ToString();
                        _pick.Tus_unit_cost = 0;

                        _pick.Tus_usrseq_no = Convert.ToInt32(Session["UsSeq"].ToString());
                        _pick.Tus_warr_no = string.Empty;
                        _pick.Tus_warr_period = 0;

                        _pick.Tus_job_no = string.Empty;
                        _pick.Tus_job_line = 0;
                        _pick.Tus_pgs_prefix = string.Empty;

                        PickSerialsList = (List<ReptPickSerials>)Session["SERIALLIST"];
                        if (PickSerialsList == null)
                        {
                            PickSerialsList = new List<ReptPickSerials>();
                        }
                        Int32 resilt = CHNLSVC.Inventory.SavePickedItemSerials(_pick);

                        PickSerialsList.Add(_pick);

                        gvSerial.DataSource = PickSerialsList;
                        Session["gvSerData"] = PickSerialsList;
                        gvSerial.DataBind();
                        BindGridStatusSerial();
                        Session["SERIALLIST"] = PickSerialsList;
                    }
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var list = new List<ReptPickSerials>();
                list = (List<ReptPickSerials>)Session["POPUPSERIALS"];
                if (list != null)
                {
                    _POPUPSERIALLIST = list;
                }

                gventryserials.DataSource = new int[] { };
                gventryserials.DataBind();

                gventryserials.DataSource = _POPUPSERIALLIST;
                gventryserials.DataBind();
                BindGridPopup();

                MpDelivery.Show();
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

        protected void lbtnViewSerial_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label lblserid = row.FindControl("lblserid") as Label;
            //  lblserid.Text = "32811936";
            Int32 serId = lblserid != null ? Convert.ToInt32(lblserid.Text) : 0;
            if (serId == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the serial id !!! ')", true);
                return;
            }
            dgvSubSerial.DataSource = new int[] { };
            List<InventorySubSerialMaster> _subSer = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = serId, Irsms_act = true });
            if (_subSer != null)
            {
                if (_subSer.Count > 0)
                {
                    dgvSubSerial.DataSource = _subSer;
                }
            }
            dgvSubSerial.DataBind();
            PopupDocument.Show();
        }

        protected void lbtPopDocClose_Click(object sender, EventArgs e)
        {


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["print"] == null) Session["print"] = "0";
            int value = Convert.ToInt32(Session["print"].ToString());
            if (value == 2)
            {
                try
                {
                    Session["GlbReportType"] = "";
                    BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    Session["GlbReportName"] = "serial_items.rpt";
                    BaseCls.GlbReportHeading = "Item Serials Report";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                    PrintPDF(targetFileName, obj._serialItems);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //  Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock Transfer Inward Print Serial", "InterCompanyInWardEntry", ex.Message, Session["UserID"].ToString());
                }
            }
            else
            {
                try
                {
                    Session["GlbReportType"] = "SCM1_AODIN";
                    BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    BaseCls.GlbReportHeading = "INWARD DOC";
                    Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "Outward_Docs_Full_IN_SGL_.rpt" : Session["UserCompanyCode"].ToString() == "ABE" ? "Outward_Docs_Full_ABE.rpt" : Session["UserCompanyCode"].ToString() == "AAL" ? "Outward_Docs_Full_AAL.rpt" : "Outward_Docs_Full.rpt";
                    //Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "AAL" ? "Outward_Docs_Full_AAL.rpt" : "";
                    //Inward_Docs_Full.rpt
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    //obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());

                    if (Session["UserCompanyCode"].ToString() == "SGL")
                    {
                        PrintPDF(targetFileName, obj._outdocfull_IN_SGL);
                    }
                    else if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        PrintPDF(targetFileName, obj._outdocfull_AEB);
                    }
                    else if (Session["UserCompanyCode"].ToString() == "AAL") //Tharindu 2018-02-01
                    {
                        PrintPDF(targetFileName, obj._outdocfull_AAL);
                    }
                    else PrintPDF(targetFileName, obj._outdocfull);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
                catch(Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock Transfer Inward Print", "InterCompanyInWardEntry", ex.Message, Session["UserID"].ToString());
                }
            }
            BindOutwardListGridData();
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
//            BindOutwardListGridData();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {

                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        lblMssg.Text = "Do you want print now?";
                        PopupConfBox.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a document number !!!');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnprintserial_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {
                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        Session["print"] = 2;
                        lblMssg.Text = "Do you want print now?";
                        PopupConfBox.Show();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Document');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnSerVarClose_Click(object sender, EventArgs e)
        {
            popSerVar.Hide();
            Session["popSerVar"] = "Hide";
        }
        protected void lbtnSerVar_Click(object sender, EventArgs e)
        {
            DataTable dt = gvSerial.DataSource as DataTable;
            lblSerVarError.Text = "";
            lblSerVarError.Visible = false;
            txtMainItmSer1.Text = "";
            txtSubSerial.Text = "";
            dgvSubSerPick.DataSource = new int[] { };
            dgvSubSerPick.DataBind();

            dgvPopSerial.DataSource = new int[] { };
            bool isDataAva = false;
            #region Comment by Lakshan
            if (Session["gvSerData"] != null)
            {
                DataTable _dtSerData = Session["gvSerData"] as DataTable;
                if (_dtSerData == null)
                {
                    List<ReptPickSerials> _serList = Session["gvSerData"] as List<ReptPickSerials>;
                    if (_serList != null)
                    {
                        isDataAva = true;
                    }
                }
                else
                {
                    isDataAva = true;
                }
            }
            if (!isDataAva)
            {
                DisplayMessage("No serial found please pick the serials or get aod out serials !!!"); return;
            }
            #endregion
            #region MyRegion
            isDataAva = false;
            if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
            {
                DisplayMessage("Pleas se!"); return;
            }
            Int32 _userSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("AOD", Session["UserCompanyCode"].ToString(), lblIssuedDocNo.Text, 1);
            List<ReptPickSerials> _tmpPickSer = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _userSeq });
            //List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_userSeq, lblIssuedDocNo.Text, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_tmpPickSer != null)
            {
                if (_tmpPickSer.Count > 0)
                {
                    dgvPopSerial.DataSource = _tmpPickSer;
                    isDataAva = true;
                }
            }
            #endregion
            if (!isDataAva)
            {
                DisplayMessage("No serial found please pick the serials or get aod out serials !!!"); return;
            }

            dgvPopSerial.DataBind();
            LoadGridStatus();
            txtMainItmSer1.Focus();
            Session["popSerVar"] = "Show";
            popSerVar.Show();
        }


        protected void txtMainItmSer1_TextChanged(object sender, EventArgs e)
        {
            txtSubSerial.Text = "";
            dgvSubSerPick.DataSource = new int[] { };
            dgvSubSerPick.DataBind();
            lblSerVarError.Text = "";
            lblSerVarError.Visible = false;
            _invSubSerList = new List<InventorySubSerialMaster>();

            if (!string.IsNullOrEmpty(txtMainItmSer1.Text))
            {
                List<InventorySerialN> _intSerList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                {
                    Ins_ser_1 = txtMainItmSer1.Text,
                    Ins_available = 1,
                    Ins_doc_no = _docNo
                });
                if (_intSerList == null)
                {
                    lblSerVarError.Text = "Main item serial # is invalid !!!";
                    lblSerVarError.Visible = true;
                    txtMainItmSer1.Text = "";
                    txtMainItmSer1.Focus();
                    popSerVar.Show();
                    return;
                }
                if (_intSerList.Count == 0)
                {
                    lblSerVarError.Text = "Main item serial # is invalid !!!";
                    lblSerVarError.Visible = true;
                    txtMainItmSer1.Text = "";
                    txtMainItmSer1.Focus();
                    popSerVar.Show();
                    return;
                }
                if (_intSerList.Count > 1)
                {
                    lblSerVarError.Text = "Main item serial # is invalid !!!";
                    lblSerVarError.Visible = true;
                    //txtMainItmSer1.Text = "";
                    //txtMainItmSer1.Focus();
                    //popSerVar.Show();
                    //return;
                }
                _invSubSerList = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = _intSerList[0].Ins_ser_id, Irsms_act = true });
                bool serHave = false;
                if (_invSubSerList != null)
                {
                    if (_invSubSerList.Count > 0)
                    {
                        dgvSubSerPick.DataSource = _invSubSerList;
                        dgvSubSerPick.DataBind();

                        txtSubSerial.Text = "";
                        txtSubSerial.Focus();
                        serHave = true;
                    }
                }
                if (!serHave)
                {

                    CompleteSubSerPick();
                    // DisplayMessage("No sub serial found !!!"); return;
                }
            }
        }

        private void LoadGridStatus()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow item in dgvPopSerial.Rows)
            {
                Label lblStsDesc = item.FindControl("lblStsDes") as Label;
                Label lblSts = item.FindControl("lblstatusser") as Label;
                if (status_list != null)
                {
                    lblStsDesc.Text = status_list.Where(c => c.Key == lblSts.Text).FirstOrDefault().Value;
                }
            }
        }
        protected void txtSubSerial_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubSerial.Text))
            {
                lblSerVarError.Text = "";
                lblSerVarError.Visible = false;
                dgvSubSerPick.DataSource = new int[] { };
                if (_invSubSerList == null)
                {
                    lblSerVarError.Text = "No sub serials found  !!!";
                    lblSerVarError.Visible = true;
                    popSerVar.Show();
                    return;
                }
                bool _haveSubSer = false;
                foreach (var item in _invSubSerList)
                {
                    if (item.Irsms_sub_ser == txtSubSerial.Text)
                    {
                        item.SubSerIsAvailable = true;
                        _haveSubSer = true;
                    }
                }
                if (!_haveSubSer)
                {
                    lblSerVarError.Text = "No sub serials found  !!!";
                    lblSerVarError.Visible = true;
                }
                var allSelect = _invSubSerList.Where(c => c.SubSerIsAvailable == false).ToList();
                if (allSelect == null)
                {
                    CompleteSubSerPick();
                    return;
                }
                else
                {
                    if (allSelect.Count == 0)
                    {
                        CompleteSubSerPick();
                        return;
                    }
                }
                if (_invSubSerList.Count > 0)
                {
                    dgvSubSerPick.DataSource = _invSubSerList;
                }

                dgvSubSerPick.DataBind();
                foreach (GridViewRow gvr in dgvSubSerPick.Rows)
                {
                    CheckBox chkPickSubSer = (CheckBox)gvr.FindControl("chkPickSubSer");
                    if (chkPickSubSer.Checked)
                    {
                        gvr.BackColor = Color.FromName("#E56E94");
                    }
                }
                txtSubSerial.Text = "";
                txtSubSerial.Focus();
                popSerVar.Show();
                return;
            }
        }

        private void CompleteSubSerPick()
        {
            _invSubSerList = new List<InventorySubSerialMaster>();
            foreach (GridViewRow dr in dgvPopSerial.Rows)
            {
                Label lblser1 = dr.FindControl("lblser1") as Label;
                Label lblitemser = dr.FindControl("lblitemser") as Label;
                Label lblstatusser = dr.FindControl("lblstatusser") as Label;
                CheckBox chkPickSer = dr.FindControl("chkPickSer") as CheckBox;
                if (lblser1.Text == txtMainItmSer1.Text.Trim())
                {
                    Int32 _res = CHNLSVC.Inventory.UpdateTempPickSerSerVerification(new ReptPickSerials()
                    {
                        Tus_base_doc_no = lblIssuedDocNo.Text,
                        Tus_itm_cd = lblitemser.Text,
                        Tus_itm_stus = lblstatusser.Text,
                        Tus_ser_1 = lblser1.Text,
                        Tus_ser_ver = 1
                    });
                    if (_res > 0)
                    {
                        chkPickSer.Checked = true;
                        dr.BackColor = Color.FromName("#E54E24");
                    }
                }
                //if (chkPickSer.Checked)
                //{
                //    dr.BackColor = Color.FromName("#E54E24");
                //}
            }
            dgvSubSerPick.DataSource = new int[] { };
            dgvSubSerPick.DataBind();
            txtSubSerial.Text = "";
            txtMainItmSer1.Text = "";
            txtMainItmSer1.Focus();
            lblSerVarError.Text = "";
            lblSerVarError.Visible = false;
            popSerVar.Show();
        }

        protected void lbtnSerVarClear_Click(object sender, EventArgs e)
        {
            txtMainItmSer1.Text = "";
            txtSubSerial.Text = "";
            txtMainItmSer1.Text = "";
            dgvSubSerPick.DataSource = new int[] { };
            dgvSubSerPick.DataBind();
            txtMainItmSer1.Focus();
            lblSerVarError.Text = "";
            lblSerVarError.Visible = false;
        }
        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/MasterFiles/Warehouse/BinSetup.aspx");
        }

        private void BindGridStatusItem()
        {
            bool b = true;
            MasterLocationNew mstLoc = CHNLSVC.General.GetMasterLocation(
                new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = Session["UserDefLoca"].ToString()
                }
                );
            if (mstLoc != null)
            {
                b = mstLoc.Ml_is_pda == 1 ? true : false;
            }
            foreach (GridViewRow item in gvItem.Rows)
            {
                Label lblstatus = item.FindControl("lblstatus") as Label;
                Label lblStsDes = item.FindControl("lblStsDes") as Label;
                LinkButton btnAddSerials = item.FindControl("btnAddSerials") as LinkButton;
                btnAddSerials.Visible = b;
                lblStsDes.Text = _stsList.Where(c => c.Mis_cd == lblstatus.Text).FirstOrDefault().Mis_desc;
            }
        }
        private void BindGridStatusSerial()
        {
            foreach (GridViewRow item in gvSerial.Rows)
            {
                Label lblstatusser = item.FindControl("lblstatusser") as Label;
                Label lblstatusserDesc = item.FindControl("lblstatusserDesc") as Label;
                lblstatusserDesc.Text = _stsList.Where(c => c.Mis_cd == lblstatusser.Text).FirstOrDefault().Mis_desc;
            }
        }
        private void BindGridPopup()
        {
            foreach (GridViewRow item in gventryserials.Rows)
            {
                Label lblstuspopup = item.FindControl("lblstuspopup") as Label;
                Label lblstuspopupDes = item.FindControl("lblstuspopupDes") as Label;
                lblstuspopupDes.Text = _stsList.Where(c => c.Mis_cd == lblstuspopup.Text).FirstOrDefault().Mis_desc;
            }
        }

        protected void chkAODoutserials_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAODoutserials.Checked)
                {
                    chkpda.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpda.Checked)
                {
                    chkAODoutserials.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        /* protected void BindOutwardItems()
         {
             try
             {
                 _dono = string.Empty; PickSerialsList = null;
                 ReptPickHeader _reptPickHdr = new ReptPickHeader(); Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), OutwardNo);
                 UserSeqNo = _seq; _reptPickHdr.Tuh_direct = true; _reptPickHdr.Tuh_doc_no = OutwardNo; _reptPickHdr.Tuh_doc_tp = OutwardType;
                 _reptPickHdr.Tuh_ischek_itmstus = false; _reptPickHdr.Tuh_ischek_reqqty = true; _reptPickHdr.Tuh_ischek_simitm = false; _reptPickHdr.Tuh_session_id = Session["SessionID"].ToString();
                 _reptPickHdr.Tuh_usr_com = Session["UserCompanyCode"].ToString(); _reptPickHdr.Tuh_usr_id = Session["UserID"].ToString(); _reptPickHdr.Tuh_usrseq_no = _seq; string _unavailableitemlist = string.Empty;
                 List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(Session["UserDefLoca"].ToString(), hdnAllowBin, _reptPickHdr, out _unavailableitemlist);
                 if (!string.IsNullOrEmpty(_unavailableitemlist))
                 { 
                     //btnSave.Enabled = false; 
                     DisplayMessage("Following item does not setup in the current system.\nItem List " + _unavailableitemlist); return;
                 }
                 else btnSave.Enabled = true;
                 if (PickSerials != null)
                 {
                     if (OutwardType == "PRN")
                     {
                         DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                         int _adhocline = 1;
                         foreach (ReptPickSerials _pik in PickSerials)
                         {
                             InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_pik.Tus_ser_id);
                             if (_master != null && !string.IsNullOrEmpty(_master.Irsm_com))
                             {
                                 _pik.Tus_new_remarks = _master.Irsm_anal_2;
                                 _dono = _master.Irsm_anal_2; DataTable _tbl = CHNLSVC.Inventory.GetPOLine(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _dono, _pik.Tus_ser_id);
                                 if (_tbl != null && _tbl.Rows.Count > 0)
                                 { _pik.Tus_itm_stus = _tbl.Rows[0].Field<string>("itb_itm_stus"); _pik.Tus_new_status = Convert.ToString(_tbl.Rows[0].Field<Int32>("itb_base_refline")); _pik.Tus_base_itm_line = _tbl.Rows[0].Field<Int32>("itb_base_refline"); }
                                 else
                                 { var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_scm2_imp")).ToList(); _pik.Tus_itm_stus = Convert.ToString(_lp[0]); _pik.Tus_new_status = Convert.ToString(_adhocline); _pik.Tus_base_itm_line = _adhocline; _adhocline += 1; }
                             }
                         }
                     }
                     else if (OutwardType == "DO")
                     {
                         DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                         //int _adhocline = 1;
                         foreach (ReptPickSerials _pik in PickSerials)
                         {
                             var _lp = _status.AsEnumerable().Where(x => x.Field<string>("mis_cd") == _pik.Tus_itm_stus).Select(x => x.Field<string>("mis_lp_cd")).ToList();

                             _pik.Tus_itm_stus = Convert.ToString(_lp[0]);
                             //_pik.Tus_new_status = Convert.ToString(_adhocline); 
                             //_pik.Tus_base_itm_line = _adhocline; _adhocline += 1; 
                         }
                     }
                     var _tblItems = (from _pickSerials in PickSerials group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_stus } into item select new { Tus_itm_cd = item.Key.Tus_itm_cd, Tus_itm_desc = item.Key.Tus_itm_desc, Tus_itm_model = item.Key.Tus_itm_model, Tus_itm_stus = item.Key.Tus_itm_stus, Tus_qty = item.Sum(p => p.Tus_qty) }).ToList();
                     //_sourceItem.DataSource = _tblItems; 
                     //gvItem.DataSource = _sourceItem;
                     //_sourceSerial.DataSource = PickSerials; 
                     //gvSerial.DataSource = _sourceSerial;
                     //PickSerialsList = PickSerials;
                 }
             }
         }*/

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
        protected void lbtnAllToPda_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtPendingDoc.Rows.Count == 0)
                {
                    DispMsg("No pending documents available !"); return;
                }
                #region Doc available
                ReptPickHeader _tmpPickHdr = new ReptPickHeader();

                #endregion
                for (int x = _dtPendingDoc.Rows.Count - 1; x >= 0; x--)
                {
                    #region Remove unavailable data
                    DataRow dr = _dtPendingDoc.Rows[x];
                    string _docNo = dr["ith_doc_no"].ToString();
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = _docNo,
                    }).FirstOrDefault();

                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                dr.Delete();
                                continue;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                dr.Delete();
                            }
                        }

                    }
                    #endregion
                }

                _dtPendingDoc.AcceptChanges();
                if (_dtPendingDoc.Rows.Count > 0)
                {
                    dgvPopPendingDoc.DataSource = _dtPendingDoc;
                    dgvPopPendingDoc.DataBind();
                    _showPopSendToPda = true;
                    popSendToPda.Show();
                }
                else
                {
                    DispMsg("No pending documents available !"); return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }


        protected void lbtnSPDaClose_Click(object sender, EventArgs e)
        {
            popSendToPda.Hide();
            _showPopSendToPda = false;
        }
        private void BindLodingBay()
        {
            DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");
            if (dtbays.Rows.Count > 0)
            {
                ddlSendAllLoadingBay.DataSource = dtbays;
                ddlSendAllLoadingBay.DataTextField = "mws_res_name";
                ddlSendAllLoadingBay.DataValueField = "mws_res_cd";
                ddlSendAllLoadingBay.DataBind();
            }
        }


        protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkAllDocNo = dgvPopPendingDoc.HeaderRow.FindControl("chkAllDocNo") as CheckBox;
                foreach (GridViewRow row in dgvPopPendingDoc.Rows)
                {
                    CheckBox chkDocNo = row.FindControl("chkDocNo") as CheckBox;
                    chkDocNo.Checked = chkAllDocNo.Checked;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnSendToPDA_Click(object sender, EventArgs e)
        {
            try
            {
                SendAllDocToPDA();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        private void SendAllDocToPDA()
        {
            try
            {
                List<ReptPickHeader> _tmpPickHdrList = new List<ReptPickHeader>();
                List<ReptPickHeader> _allReadyPdaSend = new List<ReptPickHeader>();
                ReptPickHeader _tempPickHdr = new ReptPickHeader();
                foreach (GridViewRow row in dgvPopPendingDoc.Rows)
                {
                    CheckBox chkDocNo = row.FindControl("chkDocNo") as CheckBox;
                    Label lbloutwrdnopending = row.FindControl("lbloutwrdnopending") as Label;
                    if (chkDocNo.Checked)
                    {
                        _tempPickHdr = new ReptPickHeader();
                        _tempPickHdr.Tuh_doc_no = lbloutwrdnopending.Text.Trim();
                        _tmpPickHdrList.Add(_tempPickHdr);
                    }
                }

                if (_tmpPickHdrList.Count < 0)
                {
                    DispMsg("Please select a document !"); return;
                }
                if (ddlSendAllLoadingBay.SelectedIndex < 1)
                {
                    DispMsg("Please select the loading bay !"); return;
                }

                foreach (var item in _tmpPickHdrList)
                {
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = item.Tuh_doc_no,
                    }).FirstOrDefault();

                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _allReadyPdaSend.Add(item);
                                continue;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                _allReadyPdaSend.Add(item);
                                continue;
                            }
                        }

                    }
                }
                if (_allReadyPdaSend.Count > 0)
                {
                    DispMsg("Selected documents has already been sent to PDA or has already been processed !"); return;
                }
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                #region add to issue fix not data update correctly 18 Feb 2017
                MasterLocationNew _mstLocPda = CHNLSVC.General.GetLocationDataForPdaSend(new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = Session["UserDefLoca"].ToString()
                });
                if (_mstLocPda == null)
                {
                    DispMsg("Please check the warahouse company !"); return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_mstLocPda.Ml_wh_com) && !string.IsNullOrEmpty(_mstLocPda.Ml_wh_cd))
                    {
                        warehousecom = _mstLocPda.Ml_wh_com;
                        warehouseloc = _mstLocPda.Ml_wh_cd;
                    }
                    else
                    {
                        DispMsg("Please check the warahouse company and code !"); return;
                    }
                }
                #endregion
                if (string.IsNullOrEmpty(warehousecom) || string.IsNullOrEmpty(warehouseloc))
                {
                }
                #region Header
                int _res = 0;
                Int32 _userSeqNo = 0;
                foreach (var item in _tmpPickHdrList)
                {
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_doc_no = item.Tuh_doc_no,
                        Tuh_doc_tp = "AOD",
                        Tuh_direct = true,
                        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                    }).FirstOrDefault();
                    if (_tmpPickHdr == null)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                        _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", 1, Session["UserCompanyCode"].ToString());
                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = "AOD";
                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = item.Tuh_doc_no;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlSendAllLoadingBay.SelectedValue;
                        _res = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                    }
                    else
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                        _userSeqNo = _tmpPickHdr.Tuh_usrseq_no;
                        _inputReptPickHeader.Tuh_doc_no = item.Tuh_doc_no;
                        _inputReptPickHeader.Tuh_doc_tp = "AOD";
                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlSendAllLoadingBay.SelectedValue;
                        _res = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);
                    }
                    if (_res < 1)
                    {
                        DispMsg("Error Occurred while processing !", "E"); return;
                    }
                    else
                    {
                        _res = 0;
                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                        List<InventoryBatchN> _Item = CHNLSVC.Inventory.Get_Int_Batch(item.Tuh_doc_no);
                        var _batchData = _Item.GroupBy(x => new { x.Inb_itm_cd, x.Inb_itm_stus, x.Inb_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Itb_bal_qty1) });
                        foreach (var _dr in _batchData)
                        {
                            if (_dr.theCount > 0)
                            {
                                ReptPickItems _itm = new ReptPickItems();
                                _itm.Tui_req_itm_cd = _dr.Peo.Inb_itm_cd;
                                _itm.Tui_req_itm_qty = _dr.theCount;
                                _itm.Tui_req_itm_stus = _dr.Peo.Inb_itm_stus;
                                _itm.Tui_pic_itm_cd = Convert.ToString(_dr.Peo.Inb_itm_line);
                                _itm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                                _saveonly.Add(_itm);
                            }
                        }
                        _res = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    }
                }
                #endregion

                DispMsg("Document sent to PDA Successfully !", "S");
                _showPopSendToPda = false;
                popSendToPda.Hide();
                dgvPopPendingDoc.DataSource = new int[] { };
                dgvPopPendingDoc.DataBind();
                ddlSendAllLoadingBay.SelectedIndex = 0;
                BindOutwardListGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void chkPendingDoc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lbtnbtnDocSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void btnSendToScan_Click(object sender, EventArgs e)
        {
            try
            {
                #region add to issue fix not data update correctly 18 Feb 2017
                MasterLocationNew _mstLocPda = CHNLSVC.General.GET_MST_LOC_DATA(new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = Session["UserDefLoca"].ToString(),
                    Ml_act = 1
                });
                if (_mstLocPda == null)
                {
                    DispMsg("Please check the location data !"); return;
                }
                else
                {
                    if (_mstLocPda.Ml_is_pda == 1)
                    {
                        DispMsg("This functionality is disabled for warehouse locations !"); return;
                    }
                }
                #endregion
                if (string.IsNullOrEmpty(lblIssuedDocNo.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                    return;
                }
                ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                {
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_doc_no = lblIssuedDocNo.Text.Trim(),
                    Tuh_direct = true,
                    Tuh_doc_tp = ddlType.SelectedValue
                }).FirstOrDefault();
                if (_tmpPickHdr != null)
                {
                    List<ReptPickSerials> _repPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                    if (_repPickSerList != null)
                    {
                        if (_repPickSerList.Count > 0)
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                            string _msg = "";
                            if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                            }
                            else
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                            }
                            DisplayMessage(_msg);
                            chkpda.Checked = false;
                            return;
                        }
                    }
                    List<ReptPickItems> _repPickItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                    if (_repPickItmList != null)
                    {
                        if (_repPickItmList.Count > 0)
                        {
                            string _msg = "";
                            if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                            }
                            else
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                            } DisplayMessage(_msg);
                            chkpda.Checked = false;
                            return;
                        }
                    }
                }
                Int32 _userSeqNo = 0;
                if (_tmpPickHdr != null)
                {
                    _userSeqNo = _tmpPickHdr.Tuh_usrseq_no;
                }
                else
                {
                    _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", 1, Session["UserCompanyCode"].ToString());
                }
                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(lblIssuedDocNo.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());
                if (dtdoccheck.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtdoccheck.Rows)
                    {
                        string seqNo = ddr["tuh_usrseq_no"].ToString();
                        _userSeqNo = Convert.ToInt32(seqNo);
                    }
                }
                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);
                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to serial picker or has alread processed !!!')", true);
                    return;
                }
                if (dtdoccheck.Rows.Count == 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = lblIssuedDocNo.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    //   _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    // _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    var val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = lblIssuedDocNo.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    //_inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    // _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    var val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                #region add items
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                List<InventoryBatchN> _Item = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text.Trim());

                var _batchData = _Item.GroupBy(x => new { x.Inb_itm_cd, x.Inb_itm_stus, x.Inb_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Itb_bal_qty1) });
                foreach (var _dr in _batchData)
                {
                    if (_dr.theCount > 0)
                    {
                        ReptPickItems _itm = new ReptPickItems();
                        _itm.Tui_req_itm_cd = _dr.Peo.Inb_itm_cd;
                        _itm.Tui_req_itm_qty = _dr.theCount;
                        _itm.Tui_req_itm_stus = _dr.Peo.Inb_itm_stus;
                        _itm.Tui_pic_itm_cd = Convert.ToString(_dr.Peo.Inb_itm_line);//Darshana request add by rukshan
                        _itm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                        _saveonly.Add(_itm);
                    }
                }
                Int32 _res = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                //Successfully sent 
                DispMsg("Document sent Successfully !", "S");
                dgvPopPendingDoc.DataSource = new int[] { };
                dgvPopPendingDoc.DataBind();
                BindOutwardListGridData();
                #endregion

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, string _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page;
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null)
                {
                    _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                    txtdate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
                }
            }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;

            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                    lbtndate.Visible = true;
                }
                else
                {
                    lbtndate.Visible = false;
                }
            }
            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        //protected void lbtnPdaPartial_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        popSendToPdaPart.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        DispMsg(ex.Message);
        //    }
        //}
        private void BindLodingBayPartAodIn()
        {
            DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");
            if (dtbays.Rows.Count > 0)
            {
                ddlLoadBayPart.DataSource = dtbays;
                ddlLoadBayPart.DataTextField = "mws_res_name";
                ddlLoadBayPart.DataValueField = "mws_res_cd";
                ddlLoadBayPart.DataBind();
            }
        }
        protected void lbtnParSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendPartiolScan();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        protected void lbtnPdaPartial_Click(object sender, EventArgs e)
        {
            try
            {
                lblDocNoPart.Text = "";
                if (string.IsNullOrEmpty(lblIssuedDocNo.Text))
                {
                    DispMsg("Please select the document no !"); return;
                }
                lblDocNoPart.Text = lblIssuedDocNo.Text.Trim();
                #region Check allready send to pda
                ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                {
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_doc_no = lblIssuedDocNo.Text,
                    Tuh_direct = true,
                    Tuh_doc_tp = ddlType.SelectedValue
                }).FirstOrDefault();
                if (_tmpPickHdr != null)
                {
                    List<ReptPickSerials> _repPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                    if (_repPickSerList != null)
                    {
                        if (_repPickSerList.Count > 0)
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                            string _msg = "";
                            if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                            }
                            else
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                            }
                            DisplayMessage(_msg);
                            chkpda.Checked = false;
                            return;
                        }
                    }
                    List<ReptPickItems> _repPickItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                    if (_repPickItmList != null)
                    {
                        if (_repPickItmList.Count > 0)
                        {
                            string _msg = "";
                            if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                            }
                            else
                            {
                                _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                            } DisplayMessage(_msg);
                            chkpda.Checked = false;
                            return;
                        }
                    }
                }
                #endregion
                #region load pop data
                _aodInPartData = new List<ReptPickItems>();
                List<InventoryBatchN> _Item = CHNLSVC.Inventory.Get_Int_Batch(lblIssuedDocNo.Text.Trim());
                var _batchData = _Item.GroupBy(x => new { x.Inb_itm_cd, x.Inb_itm_stus, x.Inb_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Itb_bal_qty1) });
                Int32 _rowno = 0;
                foreach (var _dr in _batchData)
                {
                    if (_dr.theCount > 0)
                    {
                        ReptPickItems _itm = new ReptPickItems();
                        _itm.Tui_req_itm_cd = _dr.Peo.Inb_itm_cd;
                        _itm.Tui_req_itm_qty = _dr.theCount;
                        _itm.Tui_req_itm_stus = _dr.Peo.Inb_itm_stus;
                        _itm.Tui_pic_itm_cd = Convert.ToString(_dr.Peo.Inb_itm_line);//Darshana request add by rukshan
                        _itm.TMP_ROW_NO = _rowno;
                        _itm.TMP_ITM_APP_QTY = _itm.Tui_req_itm_qty;
                        _itm.TMP_ITM_BAL_QTY = _itm.Tui_req_itm_qty;
                        _aodInPartData.Add(_itm);
                    }
                }
                dgvAodPartIn.DataSource = new int[] { };
                if (_aodInPartData.Count > 0)
                {
                    dgvAodPartIn.DataSource = _aodInPartData;
                }
                else
                {
                    DispMsg("No data found ! "); return;
                }
                dgvAodPartIn.DataBind();
                popSendToPdaPart.Show();
                _showPopSendPart = true;
                #endregion
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnCanPart_Click(object sender, EventArgs e)
        {
            popSendToPdaPart.Hide();
            _showPopSendPart = false;
        }

        protected void lbtnPartQtyEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grdr = (GridViewRow)btn.NamingContainer;
            var row = (GridViewRow)btn.NamingContainer;
            if (row != null)
            {
                dgvAodPartIn.EditIndex = grdr.RowIndex;//e.NewEditIndex;

                dgvAodPartIn.DataSource = _aodInPartData;
                dgvAodPartIn.DataBind();

            }
        }


        public void UpdateTableRowData()
        {
            foreach (GridViewRow row in dgvAodPartIn.Rows)
            {
                Label lblTui_req_itm_stus = row.FindControl("lblTui_req_itm_stus") as Label;
                string _sts = oMasterItemStatuss.Find(x => x.Mis_cd == lblTui_req_itm_stus.Text).Mis_desc;
                Label lblStusDesc = row.FindControl("lblStusDesc") as Label;
                lblStusDesc.Text = _sts;
            }
        }
        protected void lbtnPartQtyUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    Label lblTMP_ROW_NO = row.FindControl("lblTMP_ROW_NO") as Label;
                    Label lblTMP_ITM_APP_QTY = row.FindControl("lblTMP_ITM_APP_QTY") as Label;
                    Label lblTMP_ITM_BAL_QTY = row.FindControl("lblTMP_ITM_BAL_QTY") as Label;
                    Label lblTMP_ITM_PICK_QTY = row.FindControl("lblTMP_ITM_PICK_QTY") as Label;
                    TextBox txtTMP_ITM_PICK_QTY = row.FindControl("txtTMP_ITM_PICK_QTY") as TextBox;
                    decimal _TMP_ROW_NO = Convert.ToDecimal(lblTMP_ROW_NO.Text.Trim());
                    decimal _TMP_ITM_APP_QTY = Convert.ToDecimal(lblTMP_ITM_APP_QTY.Text.Trim());
                    decimal _TMP_ITM_BAL_QTY = Convert.ToDecimal(lblTMP_ITM_BAL_QTY.Text.Trim());
                    decimal _TMP_ITM_PICK_QTY = Convert.ToDecimal(txtTMP_ITM_PICK_QTY.Text.Trim());
                    decimal _pickQty = Convert.ToDecimal(txtTMP_ITM_PICK_QTY.Text.Trim());
                    ReptPickItems _tempItem = _aodInPartData.Where(c => c.TMP_ROW_NO == _TMP_ROW_NO).FirstOrDefault();
                    if (_tempItem != null)
                    {
                        if (_tempItem.TMP_ITM_APP_QTY < _pickQty)
                        {
                            DispMsg("Canot Exccedd the available quentity !");
                        }
                        else
                        {
                            _tempItem.TMP_ITM_PICK_QTY = _pickQty;
                            _tempItem.TMP_ITM_BAL_QTY = _tempItem.TMP_ITM_APP_QTY - _tempItem.TMP_ITM_PICK_QTY;
                        }
                    }
                    dgvAodPartIn.EditIndex = -1;
                    dgvAodPartIn.DataSource = _aodInPartData;
                    dgvAodPartIn.DataBind();
                    UpdateTableRowData();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private void SendPartiolScan()
        {
            try
            {
                if (string.IsNullOrEmpty(lblDocNoPart.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                    return;
                }

                if (ddlLoadBayPart.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                    ddlLoadBayPart.Focus();
                    return;
                }

                Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", 1, Session["UserCompanyCode"].ToString());
                _userid = (string)Session["UserID"];
                Int32 val = 0;

                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                #region add to issue fix not data update correctly 18 Feb 2017
                MasterLocationNew _mstLocPda = CHNLSVC.General.GetLocationDataForPdaSend(new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = Session["UserDefLoca"].ToString()
                });
                if (_mstLocPda == null)
                {
                    DispMsg("Please check the warahouse company !"); return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_mstLocPda.Ml_wh_com) && !string.IsNullOrEmpty(_mstLocPda.Ml_wh_cd))
                    {
                        warehousecom = _mstLocPda.Ml_wh_com;
                        warehouseloc = _mstLocPda.Ml_wh_cd;
                    }
                    else
                    {
                        DispMsg("Please check the warahouse company and code !"); return;
                    }
                }
                #endregion
                #region Header
                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(lblDocNoPart.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());

                if (dtdoccheck.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtdoccheck.Rows)
                    {
                        string seqNo = ddr["tuh_usrseq_no"].ToString();
                        _userSeqNo = Convert.ToInt32(seqNo);
                    }
                }
                #region Add by lakshan Check allready send to pda 19 sep 2016
                ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                {
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_doc_no = lblDocNoPart.Text,
                    Tuh_direct = true,
                    Tuh_doc_tp = ddlType.SelectedValue
                }).FirstOrDefault();

                List<ReptPickSerials> _repPickSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _userSeqNo });
                if (_repPickSerList != null)
                {
                    if (_repPickSerList.Count > 0)
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                        string _msg = "";
                        if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                        }
                        else
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                        }
                        DisplayMessage(_msg);
                        return;
                    }
                }
                List<ReptPickItems> _repPickItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _userSeqNo });
                if (_repPickItmList != null)
                {
                    if (_repPickItmList.Count > 0)
                    {
                        string _msg = "";
                        if (string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id;
                        }
                        else
                        {
                            _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay : " + _tmpPickHdr.Tuh_load_bay;
                        } DisplayMessage(_msg);
                        chkpda.Checked = false;
                        return;
                    }
                }
                #endregion
                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);

                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !!!')", true);
                    return;
                }

                if (dtdoccheck.Rows.Count == 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = _userid;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = lblDocNoPart.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlLoadBayPart.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = lblDocNoPart.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "AOD";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlLoadBayPart.SelectedValue;

                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }

                #endregion

                if (Convert.ToInt32(val) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
                else
                {
                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    foreach (var item in _aodInPartData)
                    {
                        ReptPickItems _itm = new ReptPickItems();
                        _itm.Tui_req_itm_cd = item.Tui_req_itm_cd;
                        _itm.Tui_req_itm_qty = item.TMP_ITM_PICK_QTY;
                        _itm.Tui_req_itm_stus = item.Tui_req_itm_stus;
                        _itm.Tui_pic_itm_cd = item.Tui_pic_itm_cd;//Darshana request add by rukshan
                        _itm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                        _saveonly.Add(_itm);
                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
                if (dtdoccheck.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document sent to PDA Successfully !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document was successfully updated !!!')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnSavePop_Click(object sender, EventArgs e)
        {
            Process(false);
            btnSave.Enabled = true;
            btnSave.CssClass = "buttonUndocolorLeft";
            btnSave.OnClientClick = "ConfirmSave();";
        }

        protected void lbtnCancelPop_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnSave.CssClass = "buttonUndocolorLeft";
            btnSave.OnClientClick = "ConfirmSave();";
        }

        private List<ReptPickSerials> GetGridDataForSerial(List<ReptPickSerials> _list)
        {
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            if (_list != null)
            {
                foreach (var item in _list)
                {
                    var _serAva = _serList.Where(c => c.Tus_itm_cd == item.Tus_itm_cd && c.Tus_itm_stus == item.Tus_itm_stus && c.Tus_ser_1 == item.Tus_ser_1
                        && c.Tus_bin == item.Tus_bin).FirstOrDefault();
                    if (_serAva != null)
                    {
                        _serAva.Tus_qty = _serAva.Tus_qty + item.Tus_qty;
                    }
                    else
                    {
                        ReptPickSerials _rptPickSer = ReptPickSerials.CreateNewObject(item);
                        _serList.Add(_rptPickSer);
                    }
                }
            }
            return _serList;
        }
        private DataTable GetGridDataForSerial(DataTable _list)
        {
            DataTable _serList = new DataTable();
            if (_list != null)
            {
                //foreach (var item in _list)
                //{
                //    var _serAva = _serList.Where(c => c.Tus_itm_cd == item.Tus_itm_cd && c.Tus_itm_stus == item.Tus_itm_stus && c.Tus_ser_1 == item.Tus_ser_1
                //        && c.Tus_bin == item.Tus_bin).FirstOrDefault();
                //    if (_serAva != null)
                //    {
                //        _serAva.Tus_qty = _serAva.Tus_qty + item.Tus_qty;
                //    }
                //    else
                //    {
                //        _serList.Add(item);
                //    }
                //}
            }
            return _serList;
        }
        private void LoadDocQtyData(string _doc)
        {
            try
            {
                lblDocQty.Text = "Document Qty : 0.00";
                lblDocScanQty.Text = "Serial Pick Qty : 0.00";
                lblOutQty.Text = "0.00";
                
                if (!string.IsNullOrEmpty(_doc))
                {
                    List<InventoryBatchN> _invBatchData = CHNLSVC.Inventory.Get_Int_Batch(_docNo);
                    if (_invBatchData != null)
                    {
                        if (_invBatchData.Count > 0)
                        {
                            decimal _docQty = _invBatchData.Sum(c => c.Itb_bal_qty1);
                            lblDocQty.Text = "Document Qty : "+_docQty.ToString("###,##0.####");
                            lblOutQty.Text = _docQty.ToString("###,##0.####");
                        }
                    }

                    _tmpPickHdr = new ReptPickHeader();
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_doc_no = _doc,
                        Tuh_doc_tp = "AOD",
                        Tuh_direct = true,
                        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                    }).FirstOrDefault();
                    if (_tmpPickHdr != null)
                    {
                        List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                decimal _pickQty = _repSerList.Sum(c => c.Tus_qty);
                                lblDocScanQty.Text = "Serial Pick Qty : " + _pickQty.ToString("###,##0.####");
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        //private void LoadMrnData(List<InventoryRequestItem> _reqList)
        //{
        //    lblDocQty.Text = (0).ToString("N2");
        //    lblDocSerPickQty.Text = (0).ToString("N2");
        //    if (_reqList != null)
        //    {
        //        decimal _reqQty = _reqList.Sum(c => c.Itri_app_qty);
        //        decimal _pickQty = _reqList.Sum(c => c.Itri_qty);
        //        lblDocQty.Text = _reqQty.ToString("N2");
        //        lblDocSerPickQty.Text = _pickQty.ToString("N2");
        //    }
        //}
        private void LoadMrnData(List<ReptPickSerials> _reqList)
        {
            lblDocQty.Text = (0).ToString("N2");
            lblDocSerPickQty.Text = (0).ToString("N2");
            if (_reqList != null)
            {
                decimal _reqQty = _reqList.Sum(c => c.Tus_qty);
                //decimal _pickQty = _reqList.Sum(c => c.Itri_qty);
                lblDocQty.Text = _reqQty.ToString("N2");
                //lblDocSerPickQty.Text = _pickQty.ToString("N2");
            }
        }

        private List<InventoryRequestItem> setItemStatus1(List<InventoryRequestItem> oitems)
        {
            if (Session["ItemStatus"] != null)
            {
                List<MasterItemStatus> oStatus = (List<MasterItemStatus>)Session["ItemStatus"];

                foreach (InventoryRequestItem item in oitems)
                {
                    item.Mis_desc = oStatus.Find(x => x.Mis_cd == item.Itri_itm_stus).Mis_desc;
                }
            }

            return oitems;
        }
    }
}