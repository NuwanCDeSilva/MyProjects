using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class StockTransferOutwardEntry : Base
    {
        //dilshan on 13/03/2018
        #region transport prop
        private string docNo { get; set; }
        public bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        DataTable _serData1
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }
        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }
        string _para = "";
        private TransportMethod _tMethod
        {
            get { if (Session["_tMethodDo"] != null) { return (TransportMethod)Session["_tMethodDo"]; } else { return new TransportMethod(); } }
            set { Session["_tMethodDo"] = value; }
        }
        private List<TransportMethod> _tMethods
        {
            get { if (Session["_tMethodsDo"] != null) { return (List<TransportMethod>)Session["_tMethodsDo"]; } else { return new List<TransportMethod>(); } }
            set { Session["_tMethodsDo"] = value; }
        }
        private TransportParty _tParty
        {
            get { if (Session["_tParty"] != null) { return (TransportParty)Session["_tParty"]; } else { return new TransportParty(); } }
            set { Session["_tParty"] = value; }
        }
        private List<TransportParty> _tPartys
        {
            get { if (Session["_tPartys"] != null) { return (List<TransportParty>)Session["_tPartys"]; } else { return new List<TransportParty>(); } }
            set { Session["_tPartys"] = value; }
        }
        private Transport _tra
        {
            get { if (Session["_tra"] != null) { return (Transport)Session["_tra"]; } else { return new Transport(); } }
            set { Session["_tra"] = value; }
        }
        protected List<Transport> _traListDo
        {
            get
            {
                if (Session["_traListDo"] == null)
                {
                    return new List<Transport>();
                }
                else
                {
                    return (List<Transport>)Session["_traListDo"];
                }

            }
            set { Session["_traListDo"] = value; }
        }
        #endregion
        //*********************

        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
        MasterLocationNew _mstLocNew = new MasterLocationNew();
        ReptPickHeader _rptHdr = new ReptPickHeader();
        //  const string COM_OUT = "COM_OUT";
        List<InventoryRequest> _serData
        {
            get { if (Session["_serData"] != null) { return (List<InventoryRequest>)Session["_serData"]; } else { return new List<InventoryRequest>(); } }
            set { Session["_serData"] = value; }
        }
        bool _batchBaseOut
        {
            get { if (Session["_batchBaseOut"] != null) { return (bool)Session["_batchBaseOut"]; } else { return false; } }
            set { Session["_batchBaseOut"] = value; }
        }
        bool _pdaFinish
        {
            get { if (Session["_pdaFinish"] != null) { return (bool)Session["_pdaFinish"]; } else { return false; } }
            set { Session["_pdaFinish"] = value; }
        }
        bool _tmp_isDiout
        {
            get { if (Session["_tmp_isDiout"] != null) { return (bool)Session["_tmp_isDiout"]; } else { return false; } }
            set { Session["_tmp_isDiout"] = value; }
        }
        public bool _prodPlanBaseOut
        {
            get { if (Session["_prodPlanBaseOut"] != null) { return (bool)Session["_prodPlanBaseOut"]; } else { return false; } }
            set { Session["_prodPlanBaseOut"] = value; }
        }
        public string _prodNo
        {
            get { if (ViewState["_prodNo"] == null) return null; return ViewState["_prodNo"].ToString(); }
            set { ViewState["_prodNo"] = value; }
        }
        public DateTime _tmpdtFrom
        {
            get { if (ViewState["_dtFrom"] == null) return DateTime.MinValue; return Convert.ToDateTime(ViewState["_dtFrom"].ToString()); }
            set { ViewState["_dtFrom"] = value; }
        }
        public DateTime _tmpdtTo
        {
            get { if (ViewState["_dtTo"] == null) return DateTime.MinValue; return Convert.ToDateTime(ViewState["_dtTo"].ToString()); }
            set { ViewState["_dtTo"] = value; }
        }
        public string _tmpDocTp
        {
            get { if (ViewState["_tmpDocTp"] == null) return null; return ViewState["_tmpDocTp"].ToString(); }
            set { ViewState["_tmpDocTp"] = value; }
        }
        string _searchType
        {
            get { if (Session["_searchType"] != null) { return (string)Session["_searchType"]; } else { return ""; } }
            set { Session["_searchType"] = value; }
        }
        protected string COM_OUT { get { return (string)Session["COM_OUT"]; } set { Session["COM_OUT"] = value; } }
        protected bool _desableSerAdd { get { return (bool)Session["_desableSerAdd"]; } set { Session["_desableSerAdd"] = value; } }
        protected List<SatProjectDetails> _SatProjectDetails { get { return (List<SatProjectDetails>)Session["_SatProjectDetails"]; } set { Session["_SatProjectDetails"] = value; } }
        protected List<Transport> _traList { get { return (List<Transport>)Session["_traList"]; } set { Session["_traList"] = value; } }
        protected List<InventoryRequestItem> ScanItemList { get { return (List<InventoryRequestItem>)Session["ScanItemList"]; } set { Session["ScanItemList"] = value; } }
        protected string _receCompany { get { return (string)Session["_receCompany"]; } set { Session["_receCompany"] = value; } }
        protected Int32 UserSeqNo { get { return Convert.ToInt32(ViewState["userSeqNo"].ToString()); } set { ViewState["userSeqNo"] = value; } }
        protected Int32 SelectedResQty
        {
            get
            {
                if (ViewState["SelectedResQty"] == null)
                {
                    return 0;
                }
                else { return Convert.ToInt32(ViewState["SelectedResQty"].ToString()); }
            }
            set { ViewState["SelectedResQty"] = value; }
        }
        protected bool _derectOut
        {
            get
            {
                if (ViewState["_derectOut"] == null)
                {
                    return false;
                }
                else { return Convert.ToBoolean(ViewState["_derectOut"].ToString()); }
            }
            set { ViewState["_derectOut"] = value; }
        }
        protected string RequestNo { get { return (string)Session["RequestNo"]; } set { Session["RequestNo"] = value; } }
        protected string SelectedStatus { get { return (string)Session["SelectedStatus"]; } set { Session["SelectedStatus"] = value; } }
        protected string JobNo { get { return (string)Session["JobNo"]; } set { Session["JobNo"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected bool _isDecimalAllow { get { return (bool)Session["_isDecimalAllow"]; } set { Session["_isDecimalAllow"] = value; } }
        protected List<ReptPickSerials> serial_list { get { return (List<ReptPickSerials>)Session["serial_list"]; } set { Session["serial_list"] = value; } }
        protected List<ReptPickSerials> SelectedSerialList { get { return (List<ReptPickSerials>)Session["SelectedSerialList"]; } set { Session["SelectedSerialList"] = value; } }
        protected DataTable _unFinishedDirectDocument { get { return (DataTable)Session["_unFinishedDirectDocument"]; } set { Session["_unFinishedDirectDocument"] = value; } }
        protected bool _ServiceJobBase
        {
            get
            {
                if (Session["_ServiceJobBase"] != null)
                {
                    return (bool)Session["_ServiceJobBase"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["_ServiceJobBase"] = value; }
        }
        protected bool _isAgePriceLevel { get { Session["_isAgePriceLevel"] = (Session["_isAgePriceLevel"] == null) ? false : (bool)Session["_isAgePriceLevel"]; return (bool)Session["_isAgePriceLevel"]; } set { Session["_isAgePriceLevel"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                BackDatePermission();
                clear();
                LoadDeliveroption();
                loadItemStatus();
                //_traList = new List<Transport>();
                chkChangeStatus.Visible = true;
                //bool b16062 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16062);
                //if (b16062)
                //{
                chkChangeStatus.Visible = true;
                //}
            }
            else
            {
                if (Session["SHWSECH"] != null && Session["SHWSECH"].ToString() == "123")
                {
                    Session["SHWSECH"] = null;
                    mpSearch.Show();
                }
                else if (Session["POPUP_LOADED"] == "1")
                {
                    Session["POPUP_LOADED"] = null;
                    UserDPopoup.Show();
                }
                else
                {
                    Session["SHWSECH"] = null;
                    mpSearch.Hide();
                }

                if (Session["TransportMode"] != null)
                {
                    if (Session["TransportMode"].ToString() == "Show")
                    {
                        popupTransport.Show();
                    }
                    else
                    {
                        popupTransport.Hide();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Added By Dulaj  2018-Apr/17
            #region Back-Date check
            if (IsBackDateOk() == false)
                return;
            #endregion

            string seq11 = ViewState["userSeqNo"].ToString();
            if (txtDispatchRequried.Text == Session["UserDefLoca"].ToString())
            {
                txtDispatchRequried.Focus();
                DisplayMessage("You cannot make outward entry to the same location !");
                return;
            }
            bool _isserialMaintan = Convert.ToBoolean(Session["_isserialMaintan"]);
            if (hdfSave.Value == "No")
            {
                return;
            }
            btnSave.Enabled = false;
            btnSave.CssClass = "buttoncolorleft";
            btnSave.OnClientClick = "return Enable();";
            if (txtVehicle.Text != "")
            {
                bool _vehicle = validateVehicle(txtVehicle.Text);
                if (_vehicle == false)
                {
                    DisplayMessage("Please check the vehicle number !");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
            }

            if (chkDirectOut.Checked)
            {
                if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    string _tmpCom = Convert.ToString(ddlRecCompany.Text);
                    if (ddlType.SelectedValue == "CONS")
                    {
                        _tmpCom = "";
                    }
                    DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(_tmpCom, txtDispatchRequried.Text.Trim().ToUpper());
                    if (_tbl == null || _tbl.Rows.Count <= 0)
                    {
                        DisplayMessage("Dispatch location is invalid. Please check the location.");
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                    if (_tbl != null) if (_tbl.Rows.Count > 0)
                        {
                            string _fromcompany = Session["UserCompanyCode"].ToString();
                            string _fromlocation = Session["UserDefLoca"].ToString();
                            string _tocompany = Convert.ToString(ddlRecCompany.Text);
                            string _tocategory = _tbl.Rows[0].Field<string>("Ml_cate_3");
                            DataTable _adpoint = CHNLSVC.Inventory.GetSubLocation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                            if (_adpoint != null && _adpoint.Rows.Count > 0)
                            {
                                var _one = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_loc_cd") == txtDispatchRequried.Text.Trim().ToUpper()).ToList();
                                var _two = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_main_loc_cd") == txtDispatchRequried.Text.Trim().ToUpper()).ToList();
                                if (_one.Count > 0 && _two.Count <= 0)
                                    goto xy;
                                if (_one.Count <= 0 && _two.Count > 0) goto xy;
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue))) goto xy;
                            //DataTable _permission = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");
                            //if (_permission == null || _permission.Rows.Count <= 0)
                            //{ this.Cursor = Cursors.Default; MessageBox.Show("Permission Required for dispatch location. Please check the location.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                            if (ddlType.SelectedValue == "CONS")
                            {
                                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode("", txtDispatchRequried.Text.Trim().ToUpper());
                                if (_mstLoc != null)
                                {
                                    _tocompany = _mstLoc.Ml_com_cd;
                                }
                            }
                            //Edit by Chamal 16-Sep-2014
                            DataTable _permCatwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");
                            DataTable _permLocwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, txtDispatchRequried.Text.Trim().ToUpper(), "AODOUT_DIRECT");
                            if (_permLocwise == null || _permLocwise.Rows.Count <= 0)
                            {
                                if (_permCatwise == null || _permCatwise.Rows.Count <= 0)
                                {
                                    DisplayMessage("Permission Required for the dispatch location . Please check the location !");
                                    btnSave.Enabled = true;
                                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                    btnSave.OnClientClick = "SaveConfirma();";
                                    return;
                                }
                            }
                        }
                xy:
                    string _defualloc = Session["UserDefLoca"].ToString(); string _selectedLoc = txtDispatchRequried.Text.Trim().ToUpper();
                    try
                    {
                        if (ddlRecCompany.Text.ToString() == Session["UserCompanyCode"].ToString())
                        {
                            if (_defualloc.Trim() == _selectedLoc.Trim())
                            {
                                DisplayMessage("You cannot process stock outward entry for the same location !");
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                btnSave.OnClientClick = "SaveConfirma();";
                                return;
                            }
                        }
                        txtDispatchRequried.Enabled = false;
                        cmbDirType.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message);
                        CHNLSVC.CloseChannel();
                    }
                }
            }

            #region Priliminary Checking - 1

            if (CheckServerDateTime() == false) return;


            if (gvItems.Rows.Count <= 0)
            {
                DisplayMessage("Please select the required items !");
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }

            if (_isserialMaintan == true)
            {
                //if (BaseCls.GlbMasterLocation.Ml_is_serial)
                //{
                if (gvSerial.Rows.Count <= 0)
                {
                    DisplayMessage("Please select the required serials !");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
                //}
            }

            #endregion



            #region GRAN Check
            bool _isGRANfromDIN = false;
            bool _isGRAN = false;

            for (int i = 0; i < gvPending.Rows.Count; i++)
            {
                CheckBox _chkT = gvPending.Rows[i].FindControl("pen_Select") as CheckBox;
                if (_chkT.Checked)
                {
                    Label lblitr_req_no = gvPending.Rows[i].FindControl("lblitr_req_no") as Label;


                    string _reqnor = lblitr_req_no.Text.Trim();
                    if (!string.IsNullOrEmpty(_reqnor))
                    {
                        InventoryRequest _reqno = new InventoryRequest();
                        _reqno.Itr_req_no = _reqnor;
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
                }
            }
            #endregion

            #region Priliminary Checking - 2

            if (!chkDirectOut.Checked)
                if (string.IsNullOrEmpty(txtRequest.Text) || txtRequest.Text == "N/A")
                {
                    DisplayMessage("Please select the direct out check box or request no !");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }

            if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
            {
                DisplayMessage("Please select the receiving company");
                ddlRecCompany.Focus();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }

            if (string.IsNullOrEmpty(txtDispatchRequried.Text.Trim().ToUpper()))
            {
                DisplayMessage("Please select the receiving location");
                txtDispatchRequried.Focus();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                DisplayMessage("Please select the date");
                txtDate.Focus();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
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
                DisplayMessage("Please select the item");
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }
            if (_isserialMaintan == true)
            {
                //if (BaseCls.GlbMasterLocation.Ml_is_serial)
                // {
                if (gvSerial.Rows.Count <= 0)
                {
                    DisplayMessage("Please select the serials");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
                //}
            }
            #endregion

            #region Manual Ref No validation
            if (chkManualRef.Checked)
                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    DisplayMessage("Please enter a valid manual document no !");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
            #endregion

            string _requestno = RequestNo;

            Int32 _userSeqNo = UserSeqNo;

            InvoiceHeader _invoiceheader = new InvoiceHeader();
            InventoryHeader _inventoryHeader = new InventoryHeader();
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();

            List<ReptPickSerials> _oldlst = null;
            List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
            if (_isserialMaintan == true)
            {
                #region Collecting Data
                if (chkDirectOut.Checked == false)
                {
                    //if (!BaseCls.GlbMasterLocation.Ml_is_serial)
                    {
                        for (int i = 0; i < gvPending.Rows.Count; i++)
                        {
                            CheckBox _chkT = gvPending.Rows[i].FindControl("pen_Select") as CheckBox;
                            if (_chkT.Checked)
                            {
                                string _selecteType = ddlType.SelectedValue;
                                if (_selecteType == "MRNS")
                                {
                                    _selecteType = "MRNA";
                                }
                                Label lblitr_req_no = gvPending.Rows[i].FindControl("lblitr_req_no") as Label;
                                UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(_selecteType, Session["UserCompanyCode"].ToString(), lblitr_req_no.Text, 0);

                                _oldlst = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, _selecteType);
                                if (_oldlst != null)
                                {
                                    _reptPickSerials.AddRange(_oldlst);
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, COM_OUT);
                    //}
                }
                else
                {
                    _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsListForAodOut(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "COM_OUT");
                    //_reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "COM_OUT");
                }
                #endregion

                #region Check for Scan serial with qty

                bool _isOk = IsAllScaned(_reptPickSerials);

                if (_isOk == false && ((bool)Session["_isserialMaintan"]))
                {
                    DisplayMessage("Scanned serial count and the selected serials were mismatched !");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
                #endregion
            }
            #region Check Inter-Company Parameter
            var _document = (from _doc in _reptPickSerials
                             where _doc.Tus_new_remarks == "DO"
                             select _doc.Tus_new_remarks).ToList();

            if (_document != null)
                if (_document.Count > 0)
                {
                    DataTable _adminT = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_adminT == null || _adminT.Rows.Count <= 0)
                    {
                        DisplayMessage("Admin team is not defined !");
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                    string _adminTeam = _adminT.Rows[0].Field<string>("ml_ope_cd");
                    if (string.IsNullOrEmpty(_adminTeam))
                    {
                        DisplayMessage("Admin team is not defined !");
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }

                    List<InterCompanySalesParameter> _priceParam = CHNLSVC.Sales.GetInterCompanyParameter(_adminTeam, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlRecCompany.Text.ToString(), string.Empty);
                    if (_priceParam == null || _priceParam.Count <= 0)
                    {
                        string msg = "There is no inter-company parameters defined for the company !  " + ddlRecCompany.Text.ToString() + " against " + Session["UserCompanyCode"].ToString() + ".";
                        DisplayMessage(msg);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                }
            #endregion

            #region Check Manual with Generated Documents

            bool _isInvalidDocument = false;
            if (chkManualRef.Checked)
            {
                var _type = _reptPickSerials.Select(x => x.Tus_new_remarks).Distinct().ToList();
                if (_type != null)
                    if (_type.Count > 0)
                    {
                        foreach (string _one in _type)
                        {
                            if (_one == "NON")
                            {
                                DisplayMessage("Document Type of the item cannot be identified automatically !");
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                btnSave.OnClientClick = "SaveConfirma();";
                                return;
                            }
                        }

                        var _notfound = _type.Contains(ddlManType.Text.ToString().Trim());
                        if (_notfound == null)
                        {
                            DisplayMessage("Dev 328");
                            btnSave.Enabled = true;
                            btnSave.CssClass = "buttonUndocolorLeft floatRight";
                            btnSave.OnClientClick = "SaveConfirma();";
                            return;
                            //if (MessageBox.Show("Manual document type and the system recognized document types are mismatching. Do you need to proceed this entry anyway?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //    return;
                            //else
                            //{
                            //    _isInvalidDocument = true;
                            //}
                        }
                    }
            }

            #endregion

            #region Check Reference Date and the Doc Date

            if (IsReferancedDocDateAppropriate(_reptPickSerials, Convert.ToDateTime(txtDate.Text).Date) == false)
            {
                DisplayMessage("Date is invalid !!!");
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }


            #endregion

            List<ReptPickSerialsSub> _reptPickSerialsSub = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, COM_OUT);



            #region Check Duplicate Serials
            var _dup = _reptPickSerials.Where(x => x.Tus_ser_id != 0).Select(y => y.Tus_ser_id).ToList();

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
                string msg = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                DisplayMessage(msg);
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }
            #endregion

            #region Invoice Header Value Assign
            _invoiceheader.Sah_com = Session["UserCompanyCode"].ToString();
            _invoiceheader.Sah_cre_by = Session["UserID"].ToString();
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
            _invoiceheader.Sah_inv_sub_tp = "SA"; //(Old Value - CS)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_inv_tp = "INTR"; //(Old Value - CRED)Change value as per the Dilanda request ::Chamal De Silva 18-08-2012 16:30
            _invoiceheader.Sah_is_acc_upload = false;
            _invoiceheader.Sah_man_cd = string.Empty;
            _invoiceheader.Sah_man_ref = string.Empty;
            _invoiceheader.Sah_manual = false;
            _invoiceheader.Sah_mod_by = Session["UserID"].ToString();
            _invoiceheader.Sah_mod_when = DateTime.Now;
            _invoiceheader.Sah_pc = Session["UserDefProf"].ToString();
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
            _invoiceheader.Sah_session_id = Session["SessionID"].ToString();
            _invoiceheader.Sah_structure_seq = string.Empty;
            _invoiceheader.Sah_stus = "D";
            _invoiceheader.Sah_town_cd = string.Empty;
            _invoiceheader.Sah_tp = "INV";
            _invoiceheader.Sah_wht_rt = 0;

            #endregion

            #region Invoice AutoNumber

            _invoiceAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _invoiceAuto.Aut_cate_tp = "PRO";
            _invoiceAuto.Aut_direction = null;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "CRED";
            _invoiceAuto.Aut_start_char = "CRED";
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            #region Inventory AutoNumber

            _inventoryAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
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
            _inventoryHeader.Ith_anal_10 = chkDirectOut.Checked ? true : false;//Direct AOD
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
            _inventoryHeader.Ith_com = Session["UserCompanyCode"].ToString();
            _inventoryHeader.Ith_com_docno = string.Empty;
            _inventoryHeader.Ith_cre_by = Session["UserID"].ToString();
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
            _inventoryHeader.Ith_entry_no = _requestno;
            _inventoryHeader.Ith_entry_tp = string.Empty;
            _inventoryHeader.Ith_git_close = false;
            _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
            _inventoryHeader.Ith_git_close_doc = string.Empty;
            _inventoryHeader.Ith_is_manual = chkManualRef.Checked ? true : false;
            _inventoryHeader.Ith_isprinted = false;
            _inventoryHeader.Ith_job_no = txtBoq.Text;//boq number
            _inventoryHeader.Ith_loading_point = string.Empty;
            _inventoryHeader.Ith_loading_user = string.Empty;
            _inventoryHeader.Ith_loc = Session["UserDefLoca"].ToString();
            _inventoryHeader.Ith_manual_ref = txtManualRef.Text;
            _inventoryHeader.Ith_mod_by = Session["UserID"].ToString();
            _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryHeader.Ith_noofcopies = 0;
            _inventoryHeader.Ith_oth_loc = txtDispatchRequried.Text.Trim().ToUpper();
            _inventoryHeader.Ith_oth_docno = chkDirectOut.Checked ? string.Empty : _requestno;
            _inventoryHeader.Ith_remarks = txtnewRemarks.Text;
            _inventoryHeader.Ith_sbu = string.Empty;
            //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _inventoryHeader.Ith_session_id = Session["SessionID"].ToString();
            _inventoryHeader.Ith_stus = "A";
            _inventoryHeader.Ith_sub_tp = string.Empty;
            _inventoryHeader.Ith_vehi_no = string.Empty;
            _inventoryHeader.Ith_oth_com = ddlRecCompany.Text.ToString();
            _inventoryHeader.Ith_anal_1 = _isInvalidDocument == true ? "1" : "0";
            _inventoryHeader.Ith_anal_2 = chkManualRef.Checked ? ddlManType.Text : string.Empty;

            _inventoryHeader.Ith_sub_tp = ddlType.SelectedValue.ToString();
            _inventoryHeader.Ith_session_id = Session["SessionID"].ToString();
            _inventoryHeader.Ith_vehi_no = txtVehicle.Text;//add rukshan 06/jan/2016
            _inventoryHeader.Ith_anal_3 = ddlDeliver.SelectedItem.Text;//add rukshan 06/jan/2016
            if (_ServiceJobBase == true)
            {
                //_inventoryHeader.Ith_isjobbase = true;
                _inventoryHeader.Ith_job_no = JobNo;
                _inventoryHeader.Ith_cate_tp = "SERVICE";
                _inventoryHeader.Ith_sub_tp = "NOR";
                _inventoryHeader.Ith_sub_docno = JobNo;
            }


            #endregion

            string _message = string.Empty;
            string _genInventoryDoc = string.Empty;
            string _genSalesDoc = string.Empty;

            if (_reptPickSerials.Count == 0)
            {

                if (_isserialMaintan == false)
                {
                    #region non maintaing serial
                    List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
                    _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;
                    List<ReptPickSerials> _repPickSerTmp = new List<ReptPickSerials>();
                    if (!chkDirectOut.Checked)
                    {
                        string _selecteType = ddlType.SelectedValue;
                        if (_selecteType == "MRNS")
                        {
                            _selecteType = "MRNA";
                        }
                        _repPickSerTmp = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, _selecteType);
                    }
                    else
                    {
                        _repPickSerTmp = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, COM_OUT);
                    }

                    if (_repPickSerTmp.Count > 0)
                    {
                        bool _fromPda = false;
                        int _usrSeq = 0;
                        if (Session["_documenttype"] != null)
                        {
                            _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(Session["_documenttype"].ToString(), Session["UserCompanyCode"].ToString(), _requestno, 0);
                        }
                        else
                        {
                            if (_derectOut)
                            {
                                _requestno = _userSeqNo.ToString();
                            }
                            _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, Session["UserCompanyCode"].ToString(), _requestno, 0);
                        }
                        if (_usrSeq != -1)
                        {
                            ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetReportTempPickHdr(new ReptPickHeader()
                            {
                                Tuh_doc_no = _requestno,
                                Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                                Tuh_doc_tp = _derectOut ? "COM_OUT" : ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue,
                                Tuh_isdirect = true,
                                Tuh_usrseq_no = _usrSeq,
                            }).FirstOrDefault();
                            //ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _userSeqNo, ddlType.SelectedValue);
                            if (_ReptPickHeader != null)
                            {
                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                {
                                    _fromPda = true;
                                }
                            }
                        }
                        #region collect item Data 13 Dec 2016 Lakshan
                        if (_derectOut)
                        {
                            bool _genItemData = false;
                            if (_select == null)
                            {
                                _genItemData = true;
                            }
                            if (_select.Count == 0)
                            {
                                _genItemData = true;
                            }
                            if (_genItemData)
                            {
                                List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                                var _itemdet = (from _l in _repPickSerTmp
                                                group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus }
                                                    into batch
                                                    select new { Tus_itm_cd = batch.Key.Tus_itm_cd, Tus_itm_stus = batch.Key.Tus_itm_stus, Tus_qty = batch.Sum(p => p.Tus_qty) }).ToList();
                                if (_itemdet != null && _itemdet.Count > 0)
                                {
                                    int _line = 1;
                                    foreach (var _n in _itemdet)
                                    {
                                        InventoryRequestItem _one = new InventoryRequestItem();
                                        MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _n.Tus_itm_cd);
                                        _one.Itri_app_qty = _n.Tus_qty; _one.Itri_bqty = _n.Tus_qty;
                                        _one.Itri_itm_cd = _n.Tus_itm_cd; _one.Itri_itm_stus = _n.Tus_itm_stus;
                                        _one.Itri_line_no = _line; _line += 1;
                                        _one.Itri_mitm_cd = _n.Tus_itm_cd; _one.Itri_mitm_stus = _n.Tus_itm_stus;
                                        _one.Itri_mqty = _n.Tus_qty; _one.Itri_note = "";
                                        _one.Itri_qty = _n.Tus_qty; _one.Mi_longdesc = _itm.Mi_longdesc;
                                        _one.Mi_model = _itm.Mi_model; _lst.Add(_one);
                                    }
                                }
                                foreach (var item in _lst)
                                {
                                    item.Itri_note = _usrSeq.ToString();
                                    item.Itri_qty = _repPickSerTmp.Where(y => y.Tus_base_doc_no == _usrSeq.ToString() && y.Tus_itm_cd == item.Itri_itm_cd && y.Tus_itm_stus == item.Itri_itm_stus).Sum(z => z.Tus_qty);
                                    item.Itri_bqty = 0;
                                }
                                _select = _lst;
                            }
                        }

                        #endregion
                        // var _serials = _repPickSerTmp.Where(c => c.Tus_ser_1 != "N/A").ToList();
                        // var _nonSerials = _repPickSerTmp.Where(c => c.Tus_ser_1 == "N/A").ToList();
                        foreach (InventoryRequestItem _itm in _select)
                        {
                            if (_fromPda)
                            {
                                var _saveSerList = _repPickSerTmp.Where(c => c.Tus_itm_cd == _itm.Itri_itm_cd).ToList();
                                if (_saveSerList.Count > 0)
                                {
                                    decimal _qty = _saveSerList.Sum(c => c.Tus_qty);
                                    if (_itm.Itri_qty != _qty)
                                    {
                                        DisplayMessage("Pick serials qty is invalid !");
                                        btnSave.Enabled = true;
                                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                        btnSave.OnClientClick = "SaveConfirma();";
                                        return;
                                    }
                                    foreach (var _serItem in _saveSerList)
                                    {
                                        //_serItem.Tus_cre_by = Session["UserID"].ToString();
                                        // _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        //_serItem.Tus_cre_by = Session["UserID"].ToString();
                                        _serItem.Tus_base_doc_no = txtRequest.Text;
                                        _serItem.Tus_base_itm_line = _itm.Itri_line_no;
                                        _serItem.Tus_itm_desc = _itm.Mi_longdesc;
                                        _serItem.Tus_itm_model = _itm.Mi_model;
                                        _serItem.Tus_com = Session["UserCompanyCode"].ToString();
                                        _serItem.Tus_loc = Session["UserDefLoca"].ToString();
                                        _serItem.Tus_bin = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                                        _serItem.Tus_itm_cd = _itm.Itri_itm_cd;
                                        _serItem.Tus_itm_stus = _itm.Itri_itm_stus;
                                        //_serItem.Tus_qty = 1;
                                        if (_itm.Itri_res_qty > 0)
                                        {
                                            _serItem.Tus_resqty = 1;
                                        }
                                        //_serItem.Tus_ser_1 = "N/A";
                                        _serItem.Tus_ser_2 = "N/A";
                                        _serItem.Tus_ser_3 = "N/A";
                                        _serItem.Tus_ser_4 = "N/A";
                                        //_serItem.Tus_ser_id = 0;
                                        //_serItem.Tus_serial_id = "0";
                                        //_serItem.Tus_unit_cost = 0;
                                        // _serItem.Tus_unit_price = 0;
                                        //_serItem.Tus_unit_price = 0;
                                        if (_inventoryHeader.Ith_sub_tp != "RE" && _inventoryHeader.Ith_sub_tp != "EX" &&
                                        _inventoryHeader.Ith_sub_tp != "BOI" && _inventoryHeader.Ith_sub_tp != "EXP")
                                        {
                                            _serItem.Tus_job_no = JobNo;
                                        }
                                        _serItem.Tus_job_line = _itm.Itri_job_line;
                                        _serItem.Tus_new_remarks = "AOD-OUT";
                                        _reptPickSerials.Add(_serItem);
                                    }
                                }
                            }
                            if (!_fromPda)
                            {
                                if (Convert.ToDecimal(_itm.Itri_qty) != 0)
                                {
                                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                    _reptPickSerial_.Tus_usrseq_no = _userSeqNo;
                                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                    _reptPickSerial_.Tus_base_doc_no = txtRequest.Text;
                                    _reptPickSerial_.Tus_base_itm_line = _itm.Itri_line_no;
                                    _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                                    _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                    _reptPickSerial_.Tus_bin = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                                    _reptPickSerial_.Tus_itm_cd = _itm.Itri_itm_cd;
                                    _reptPickSerial_.Tus_itm_stus = _itm.Itri_itm_stus;
                                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(_itm.Itri_qty);
                                    if (_itm.Itri_res_qty > 0)
                                    {
                                        _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Itri_qty);
                                    }
                                    _reptPickSerial_.Tus_ser_1 = "N/A";
                                    _reptPickSerial_.Tus_ser_2 = "N/A";
                                    _reptPickSerial_.Tus_ser_3 = "N/A";
                                    _reptPickSerial_.Tus_ser_4 = "N/A";
                                    _reptPickSerial_.Tus_ser_id = 0;
                                    _reptPickSerial_.Tus_serial_id = "0";
                                    _reptPickSerial_.Tus_unit_cost = 0;
                                    _reptPickSerial_.Tus_unit_price = 0;
                                    _reptPickSerial_.Tus_unit_price = 0;
                                    if (_inventoryHeader.Ith_sub_tp != "RE" && _inventoryHeader.Ith_sub_tp != "EX" &&
                                        _inventoryHeader.Ith_sub_tp != "BOI" && _inventoryHeader.Ith_sub_tp != "EXP")
                                    {
                                        _reptPickSerial_.Tus_job_no = JobNo;
                                    }
                                    else
                                    {
                                        _reptPickSerial_.Tus_job_no = _itm.Itri_job_no;
                                    }
                                    _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                                    _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                                    _reptPickSerials.Add(_reptPickSerial_);
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                    #endregion
                    #region MyRegion
                    /*  foreach (InventoryRequestItem _itm in _select)
                    {
                        if (Convert.ToDecimal(_itm.Itri_qty) != 0)
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            // _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = txtRequest.Text;
                            _reptPickSerial_.Tus_base_itm_line = _itm.Itri_line_no;
                            _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_bin = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                            _reptPickSerial_.Tus_itm_cd = _itm.Itri_itm_cd;
                            _reptPickSerial_.Tus_itm_stus = _itm.Itri_itm_stus;
                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_itm.Itri_qty);
                            if (_itm.Itri_res_qty > 0)
                            {
                                _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Itri_qty);
                            }
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_3 = "N/A";
                            _reptPickSerial_.Tus_ser_4 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_serial_id = "0";
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_job_no = JobNo;
                            _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                            _reptPickSerials.Add(_reptPickSerial_);
                        }
                    }*/
                    #endregion
                }

            }
            foreach (ReptPickSerials _serial in _reptPickSerials)
            {
                _serial.Tus_new_remarks = "AOD-OUT";
                if (txtBoq.Text != "")
                {
                    _serial.Tus_job_no = txtBoq.Text;
                }

            }
            #region Save Process
            //Process
            if (_traList != null)
            {

            }
            if (ddlType.SelectedValue == "PDA")
            {
                _reptPickSerials.ToList().ForEach(c => c.Tus_new_remarks = "AOD-OUT");
            }
            if (_reptPickSerials == null || _reptPickSerials.Count == 0)
            {
                DisplayMessage("No data available !!!");
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }
            //Add by lakshan
            MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_mstLocation != null)
            {
                if (!_mstLocation.Ml_is_serial)
                {
                    int _usrSeq = 0;
                    if (Session["_documenttype"] != null)
                    {
                        _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(Session["_documenttype"].ToString(), Session["UserCompanyCode"].ToString(), _requestno, 0);
                    }
                    else
                    {
                        _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, Session["UserCompanyCode"].ToString(), _requestno, 0);
                    }
                    if (_usrSeq != -1)
                    {
                        ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetReportTempPickHdr(new ReptPickHeader()
                        {
                            Tuh_doc_no = _requestno,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                            Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue,
                            Tuh_isdirect = true,
                            Tuh_usrseq_no = _usrSeq,
                        }).FirstOrDefault();
                        if (_ReptPickHeader != null)
                        {
                            if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                            {
                                _inventoryHeader.Ith_loading_point = _ReptPickHeader.Tuh_load_bay;
                            }
                        }
                    }
                }

            }
            //Add by Lakshan
            if (!_mstLocation.Ml_is_serial)
            {
                Int32 _PickSerCount = 0;
                var vSer = _reptPickSerials.Where(c => c.Tus_ser_1 != "N/A").ToList();
                if (vSer != null)
                {
                    _PickSerCount = vSer.Count;
                }
                if (_PickSerCount > 0)
                {
                    int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(Session["_documenttype"].ToString(), Session["UserCompanyCode"].ToString(), _requestno, 0);
                    if (_usrSeq != -1)
                    {
                        ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetReportTempPickHdr(new ReptPickHeader()
                        {
                            Tuh_doc_no = _requestno,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                            Tuh_doc_tp = ddlType.SelectedValue,
                            Tuh_isdirect = false,
                            Tuh_usrseq_no = _usrSeq,
                        }).FirstOrDefault();
                        if (_ReptPickHeader != null)
                        {
                            if (string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) || string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) || string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                            {
                                DisplayMessage("Loading bay data not found !!!");
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                btnSave.OnClientClick = "SaveConfirma();";
                                return;
                            }
                        }
                    }
                }
            }
            //
            btnSave.Enabled = false;
            btnSave.CssClass = "buttoncolorleft";
            btnSave.OnClientClick = "return Enable();";
            if (ddlType.SelectedValue == "CONS")
            {
                MasterLocation _Tmploc1 = CHNLSVC.General.GetLocationByLocCode("", _inventoryHeader.Ith_oth_loc);
                _inventoryHeader.Ith_oth_com = _Tmploc1.Ml_com_cd;
            }
            MasterLocation _loc = CHNLSVC.General.GetLocationByLocCode(_inventoryHeader.Ith_oth_com, _inventoryHeader.Ith_oth_loc);
            if (_loc == null)
            {
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                DispMsg("Dispatch location is invalid. Please check the location !");
                return;
            }

            #region Check Scan Completed
            _tmpPickHdr = new ReptPickHeader();
            _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
            {
                Tuh_doc_no = _inventoryHeader.Ith_oth_docno,
                Tuh_doc_tp = COM_OUT,
                Tuh_direct = false,
                Tuh_usr_com = Session["UserCompanyCode"].ToString()
            }).FirstOrDefault();
            if (_tmpPickHdr != null)
            {
                if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                {
                    if (_tmpPickHdr.Tuh_fin_stus != 1)
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        DispMsg("Scanning is not completed for the selected document !"); return;
                    }
                }
            }
            #endregion
            #region Get Reservation details from Req 14 Oct 2016
            if (!_derectOut && ddlType.SelectedValue != "PDA")
            {
                InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest { Itr_req_no = _inventoryHeader.Ith_oth_docno, Itr_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue }).FirstOrDefault();
                if (_invReq != null)
                {
                    List<InventoryRequestItem> _invReqItmList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA(new InventoryRequestItem() { Itri_seq_no = _invReq.Itr_seq_no });
                    foreach (var item in _invReqItmList)
                    {
                        foreach (var v in _reptPickSerials)
                        {
                            if (item.Itri_line_no == v.Tus_base_itm_line)
                            {
                                v.Tus_res_no = string.IsNullOrEmpty(item.Itri_res_no) ? null : item.Itri_res_no == "N/A" ? null : item.Itri_res_no;
                                v.Tus_res_line = item.Itri_res_line;
                                v.Tus_resqty = item.Itri_res_qty > 0 ? v.Tus_qty : 0;
                            }
                        }
                    }
                    //    if (!_resNoAva)
                    //    {
                    //        DispMsg("Please check the reservation data !");
                    //        btnSave.Enabled = true;
                    //        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    //        btnSave.OnClientClick = "SaveConfirma();";
                    //        return;
                    //    }
                    //    if (!_resBalAva)
                    //    {
                    //        DispMsg("Please check the reservation qty!");
                    //        btnSave.Enabled = true;
                    //        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    //        btnSave.OnClientClick = "SaveConfirma();";
                    //        return;
                    //    }
                }
            }
            #endregion
            #region Batch base aod out 22 oct 2016
            if (_batchBaseOut)
            {
                _inventoryHeader.BacthBaseDoc = true;
                _inventoryHeader.BacthBaseDocNo = txtBoq.Text.Trim();
                bool b10146 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10146);
                if (!b10146)
                {
                    DispMsg("Sorry, You have no permission !  (Advice: Required permission code : 10146 ) ");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
            }
            #endregion

            #region Cheak Request Data valid 22 Oct 2016
            if (_inventoryHeader.Ith_sub_tp == "MRNA" || _inventoryHeader.Ith_sub_tp == "INTR" || _inventoryHeader.Ith_sub_tp == "REQD"
                || _inventoryHeader.Ith_sub_tp == "MRN" || _inventoryHeader.Ith_sub_tp == "RE" || _inventoryHeader.Ith_sub_tp == "EX" || _inventoryHeader.Ith_sub_tp == "BOI" || _inventoryHeader.Ith_sub_tp == "EXP")
            {
                InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = _inventoryHeader.Ith_oth_docno }).FirstOrDefault();
                if (_invReq != null)
                {
                    List<MasterItem> _mstItemList = new List<MasterItem>();
                    List<InventoryRequestItem> _invReqItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA(new InventoryRequestItem() { Itri_seq_no = _invReq.Itr_seq_no });
                    var _pickSerialDataList = _reptPickSerials.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus }).
                        Select(group => new { Peo = group.Key, theCount = group.Sum(c => c.Tus_qty) });
                    var _invReqDataList = _invReqItemList.GroupBy(x => new { x.Itri_itm_cd, x.Itri_itm_stus }).
                        Select(group => new { Peo = group.Key, theCount = group.Sum(c => c.Itri_bqty) });
                    foreach (var _serData in _pickSerialDataList)
                    {
                        foreach (var _reqData in _invReqDataList)
                        {
                            if (_serData.Peo.Tus_itm_cd == _reqData.Peo.Itri_itm_cd && _serData.Peo.Tus_itm_stus == _reqData.Peo.Itri_itm_stus)
                            {
                                if (_serData.theCount > _reqData.theCount)
                                {
                                    _mstItemList.Add(new MasterItem() { Mi_cd = _reqData.Peo.Itri_itm_cd });
                                }
                            }
                        }
                    }
                    if (_mstItemList.Count > 0)
                    {
                        string _itms = "";
                        foreach (var _mstItem in _mstItemList)
                        {
                            _itms = string.IsNullOrEmpty(_itms) ? _mstItem.Mi_cd : _itms + ", " + _mstItem.Mi_cd;
                        }
                        DispMsg("Request item quantity exceed ! " + _itms);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                    #region Validate item data available in request
                    foreach (var _serial in _reptPickSerials)
                    {
                        foreach (var _reItem in _invReqItemList)
                        {
                            if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                            {
                                List<InventoryBatchRefN> _listInr_batch = CHNLSVC.Inventory.Get_Inr_Batch(
                                new InventoryBatchRefN()
                                {
                                    Inb_job_no = _reItem.Itri_job_no,
                                    Inb_base_refline = _reItem.Itri_job_line
                                });
                                var dataAva = _listInr_batch.Where(c => c.Inb_qty > 0).ToList();
                                InventoryBatchRefN _intBatch = dataAva.FirstOrDefault();
                                if (_intBatch != null)
                                {
                                    if (_reItem.Itri_itm_cd != _intBatch.Inb_itm_cd)
                                    {
                                        if (_intBatch.Inb_itm_cd == _serial.Tus_itm_cd && _reItem.Itri_itm_stus == _serial.Tus_itm_stus)
                                        {
                                            _serial.Tus_is_valid_ser = 1;
                                        }
                                    }
                                    else
                                    {
                                        if (_reItem.Itri_itm_cd == _serial.Tus_itm_cd && _reItem.Itri_itm_stus == _serial.Tus_itm_stus)
                                        {
                                            _serial.Tus_is_valid_ser = 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_reItem.Itri_itm_cd == _serial.Tus_itm_cd && _reItem.Itri_itm_stus == _serial.Tus_itm_stus)
                                {
                                    _serial.Tus_is_valid_ser = 1;
                                }
                            }
                        }
                    }
                    var _invalidSerList = _reptPickSerials.Where(c => c.Tus_is_valid_ser == 0).ToList();
                    if (_invalidSerList != null)
                    {
                        if (_invalidSerList.Count > 0)
                        {
                            DispMsg("Selected item is different from the requested item !");
                            btnSave.Enabled = true;
                            btnSave.CssClass = "buttonUndocolorLeft floatRight";
                            btnSave.OnClientClick = "SaveConfirma();";
                            return;
                        }
                    }
                    #endregion
                }
            }
            #endregion

            _inventoryHeader.TMP_IS_RES_UPDATE = true;
            _inventoryHeader.TMP_UPDATE_BASE_ITM = true;
            _inventoryHeader.TMP_CHK_SER_IS_AVA = true;
            _inventoryHeader.TMP_CHK_LOC_BAL = true;
            if (_inventoryHeader.Ith_sub_tp == "MRNA" || _inventoryHeader.Ith_sub_tp == "MRNS")
            {
                foreach (var _rptSer in _reptPickSerials)
                {
                    _rptSer.Tus_resqty = _rptSer.Tus_qty;
                }
            }

            #region validate aod out company not equal login company 05 Jan 2017 lakshan
            var _notConsList = _reptPickSerials.Where(c => c.Tus_itm_stus != "CONS").FirstOrDefault();
            if (_notConsList != null)
            {
                if (_inventoryHeader.Ith_oth_com != Session["UserCompanyCode"].ToString())
                {
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    DispMsg("Please check the receive company. Item status invalid !"); return;
                }
            }
            #endregion
            #region int_ser line genarate 08 Jan 2017 by lakshan
            MasterLocation _outLoc = CHNLSVC.General.GetLocationByLocCode(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc);
            MasterLocation _inLoc = CHNLSVC.General.GetLocationByLocCode(_inventoryHeader.Ith_oth_com, _inventoryHeader.Ith_oth_loc);
            if (!_inLoc.Ml_is_pda)
            {
                if (!_outLoc.Ml_is_serial)
                {
                    if (string.IsNullOrEmpty(_inventoryHeader.Ith_loading_point))
                    {
                        _inventoryHeader.Ith_loading_point = "LP-01";
                    }
                    if (_inventoryHeader.Ith_loading_point == "N/A")
                    {
                        _inventoryHeader.Ith_loading_point = "LP-01";
                    }
                }
            }
            #endregion
            #region validate serials are pick from dfs locations if in location not use PDA 08 Jan 2017 Lakshan
            if (!_inLoc.Ml_is_pda)
            {
                if (!_outLoc.Ml_is_serial)
                {
                    MasterItem _tmpMstItem = new MasterItem();
                    List<ReptPickSerials> _errList = new List<ReptPickSerials>();
                    foreach (var item in _reptPickSerials)
                    {
                        _tmpMstItem = CHNLSVC.General.GetItemMaster(item.Tus_itm_cd);
                        if (_tmpMstItem.Mi_is_ser1 == 1 && item.Tus_ser_1 == "N/A")
                        {
                            _errList.Add(item);
                        }
                    }
                    if (_errList.Count > 0)
                    {
                        DispMsg("Serials are not scanned for serialized items");
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                }
            }
            #endregion
            if (Session["UserCompanyCode"].ToString() == "ARL")
            {
                _inventoryHeader.TMP_SAVE_PKG_DATA = true;
            }
            #region add validation for allocation data chk 09 Jan 2016
            if (_inventoryHeader.Ith_sub_tp != "MRNA")
            {
                if (_inventoryHeader.Ith_sub_tp != "INTR")
                {
                    _inventoryHeader.TMP_IS_ALLOCATION = true;
                    List<ReptPickSerials> _allocationErrList = CHNLSVC.Inventory.AllocationDataValidateAodOut(_reptPickSerials, _inLoc, Session["UserDefLoca"].ToString());
                    if (_allocationErrList.Count > 0)
                    {
                        DispMsg(_allocationErrList[0].Tmp_err_msg);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                }
            }

            #endregion


            //Udaya 28/04/2017
            List<TmpValidation> locQtyChk = CHNLSVC.Inventory.locQtyCheck(_reptPickSerials, _inventoryHeader, Session["UserDefLoca"].ToString(), Session["UserCompanyCode"].ToString());
            if (locQtyChk.Count > 0)
            {
                dgvlocQty.DataSource = locQtyChk;
                dgvlocQty.DataBind();
                popuplocQty.Show();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                return;
            }
            //
            HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("LOC", _inventoryHeader.Ith_loc, "TRANSREQD", DateTime.Now.Date);
            if (_sysPara != null)
            {
                if (_sysPara.Hsy_val == 1)
                {
                    if (_traListDo.Count == 0)
                    {
                        DispMsg("Please enter transport data !");
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "SaveConfirma();";
                        return;
                    }
                }
            }
            //foreach (var _tra in _traList)
            //{
            //    _tra.itrn_isactive = 1;//As per the dharshan add by lakshan 11Sep2017
            //}
            _inventoryHeader.Ith_gen_frm = "SCMWEB";

            //subodana
            if (chkProd.Checked)
            {
                _inventoryHeader.ProductionBaseDoc = true;
                _inventoryHeader.ProductionBaseDocNo = txtBoq.Text;                
            }
            if (ddlType.SelectedValue == "MRNS")
            {
                _inventoryHeader.Ith_service_job_base = true;
                InventoryRequest _tmpInvreq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest()
                {
                    Itr_req_no = _inventoryHeader.Ith_oth_docno,
                    Itr_stus = "A",
                    Itr_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue
                }).FirstOrDefault();
                _inventoryHeader.Ith_service_job_no = _tmpInvreq.Itr_job_no;
                _inventoryHeader.Ith_service_job_line = _tmpInvreq.Itr_job_line;
                _inventoryHeader.Ith_job_no = _inventoryHeader.Ith_service_job_no;
                _inventoryHeader.Ith_cate_tp = "SERVICE";
                _inventoryHeader.Ith_sub_tp = "NOR";
                _inventoryHeader.Ith_sub_docno = _inventoryHeader.Ith_service_job_no;
                foreach (var _rptSer in _reptPickSerials)
                {
                    _rptSer.Tus_job_no = _tmpInvreq.Itr_job_no;
                    _rptSer.Tus_job_line = _tmpInvreq.Itr_job_line;
                }
            }
            if (chkMakeAdj.Checked)
            {
                bool b10163 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10163);
                if (b10163)
                {
                    _inventoryHeader.Tmp_itm_conv_to_fg = true;
                }
                else
                {
                    _inventoryHeader.Tmp_itm_conv_to_fg = false;
                    DispMsg("Sorry, You have no permission !  (Advice: Required permission code : 10163 ) ");
                    chkMakeAdj.Checked = false;
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "SaveConfirma();";
                    return;
                }
            }
            //Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlRecCompany.Text.ToString(), _requestno, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, _reptPickSerialsSub, out _message, out _genSalesDoc, out _genInventoryDoc, _isGRAN, _isGRANfromDIN, _traList);

            //by dilshan on 15/03/2018

            Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlRecCompany.Text.ToString(), _requestno, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, _reptPickSerialsSub, out _message, out _genSalesDoc, out _genInventoryDoc, _isGRAN, _isGRANfromDIN, _traListDo);
            //************************

            string Msg = string.Empty;

            if (!string.IsNullOrEmpty(_genInventoryDoc))
                _genInventoryDoc.Trim().Remove(_genInventoryDoc.Length - 1);
            if (!string.IsNullOrEmpty(_genSalesDoc))
                _genSalesDoc.Trim().Remove(_genSalesDoc.Length - 1);

            if (_effect > 0)
            {
                string _mailDocNo = "";
                #region collect data available 20 Oct 2016
                DateTime _dtFrom = Convert.ToDateTime(txtFrom.Text);
                DateTime _dtTo = Convert.ToDateTime(txtTo.Text);
                _tmpdtFrom = _dtFrom;
                _tmpdtTo = _dtTo;
                string _docTp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                _tmpDocTp = _docTp;
                bool _pdaCom = chkPendingDoc.Checked;
                bool _isDiout = chkDirectOut.Checked;
                _pdaFinish = chkPendingDoc.Checked;
                _tmp_isDiout = chkDirectOut.Checked;
                #endregion
                string msg = "Successfully processed. Document No(s) - " + _genInventoryDoc + "  " + _genSalesDoc;
                string _doc = _genInventoryDoc;
                if (_genInventoryDoc.Contains(","))
                {
                    string[] arr = _genInventoryDoc.Split(new string[] { "," }, StringSplitOptions.None);
                    _doc = arr[0];
                }
                Session["documntNo"] = _doc;
                _mailDocNo = _genInventoryDoc;
                DisplayMessage(msg, 3);
                Session["print"] = 1;
                #region Add by Lakshan Ip print error occured
                gvPending.DataSource = new int[] { };
                gvPending.DataBind();

                gvItems.DataSource = new int[] { };
                gvItems.DataBind();
                gvSerial.DataSource = new int[] { };
                gvSerial.DataBind();
                Session["ScanItemListNew"] = null;
                #endregion
                //_print();
                #region genarate mail
                CHNLSVC.MsgPortal.GenarateAodOutMailAndSMS(Session["UserCompanyCode"].ToString(), _mailDocNo);
                #endregion
                //lblMssg.Text = "Do you want print now?";
                //PopupConfBox.Show();
                clear();
                //Session["documntNo"] = _genInventoryDoc;
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
                if (_batchBaseOut)
                {
                    DesableBatchControler(true);
                    chkbatch.Checked = false;
                    chkboq.Checked = false;
                }
                Label3.Text = "Do you want print now?";
                popUpPrint.Show();

                //dilshan on 14/03/2018
                lprintcourPrint();
                //*********************
                //#region recall data 
                //if (_docTp != "0" && !_isDiout)
                //{
                //    txtFrom.Text = _dtFrom.ToString("dd/MMM/yyyy");
                //    txtTo.Text = _dtTo.ToString("dd/MMM/yyyy");
                //    ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(_docTp));
                //    ddlType_SelectedIndexChanged(null, null);
                //    //chkPendingDoc.Checked = _pdaCom;
                //    //if (_pdaCom)
                //    //{
                //    //    btnDocSearch_Click(null, null);
                //    //}
                //}
                //#endregion

            }
            else
            {
                if (_message.Contains("CHK_INLFREEQTY"))
                { 
                    //added by Wimal @ 04/Aug/2018--------------------------------------Start
                    MasterLocation _fromloc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    MasterLocation _toloc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.Trim().ToUpper());

                    if (_fromloc.Ml_loc_tp == "ASS" && _toloc.Ml_loc_tp == "WH")
                    {
                        DisplayJobDetl();
                    }
                        else
                    {
                        DisplayMessage("There is no free stock balance available. Please check the stock balances.");
                    }
                    //added by Wimal @ 04/Aug/2018--------------------------------------End
                }
                else if (_message.Contains("CHK_INLRESQTY"))
                {
                    //DisplayMessage("There is no reserved stock available. Please check the stock balances.");
                }
                else if (_message.Contains("NO_STOCK_BALANCE"))
                {                    
                    
                    //added by Wimal @ 04/Aug/2018--------------------------------------Start
                    MasterLocation _fromloc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    MasterLocation _toloc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.Trim().ToUpper());

                    if (_fromloc.Ml_loc_tp == "ASS" && _toloc.Ml_loc_tp == "WH")
                    {
                        DisplayJobDetl();
                    }
                    else
                    {
                        DisplayMessage("There is no free stock available. Please check the stock balances.[Batch]");                    
                    }
                    //added by Wimal @ 04/Aug/2018--------------------------------------End
                }
                else if (_message.Contains("CHK_INLQTY"))
                {
                    DisplayMessage("There is no stock available. Please check the stock balances.");
                }
                else if (_message.Contains("CHK_INBFREEQTY"))
                {
                    DisplayMessage("There is no bin stock available !");
                }
                else if (_message.Contains("CHK_ITRIBQTY"))
                {
                    DisplayMessage("There is no request balance available. Please check the request balances. !");
                }
                else if (_message.Contains("CHK_INBQTY"))
                {
                    DisplayMessage("Please check the stock balances.[Batch]");
                }
                else if (!string.IsNullOrEmpty(_message))
                {

                    DisplayMessage(_message);
                }
                else
                {
                    //  DisplayMessage("Error occurred while processing. " + _message);
                    DisplayMessage("Error occurred while processing ! ");
                    //DisplayMessage("Please check the issues of " + _message);
                }
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                btnSave.OnClientClick = "SaveConfirma();";
            }
            #endregion
        }
        protected void DisplayJobDetl()
        {
            // inform balance job number - Wimal @ 01/08/2018           
                string strBalJobNo = "";
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    Label lblitri_itm_cd1 = gvItems.Rows[i].FindControl("lblitri_itm_cd1") as Label; ;
                    Label lblitri_qty = gvItems.Rows[i].FindControl("lblitri_qty") as Label; ;
                    Label lblitri_note = gvItems.Rows[i].FindControl("lblitri_note") as Label; ;
                    Label lblitri_itm_stus = gvItems.Rows[i].FindControl("lblitri_itm_stus") as Label; ;
                    Label lblitri_line_no = gvItems.Rows[i].FindControl("lblitri_line_no") as Label; ;

                    //Label lblitri_itm_cd = dr.FindControl("lblitri_itm_cd1") as Label;
                    List<InventoryBatchRefN> baldoc = CHNLSVC.Inventory.getItemBalanceQty(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToString(lblitri_itm_cd1.Text), Convert.ToString(lblitri_itm_stus.Text));
                    foreach (var xx in (from x in baldoc
                                        group x by new { x.Inb_itm_cd, x.Inb_job_no }
                                            into g
                                            select new { g.Key.Inb_itm_cd, g.Key.Inb_job_no, qty = g.Sum(s => s.Inb_qty) }))
                    {
                        if ((strBalJobNo) != "")
                        {
                            strBalJobNo = strBalJobNo + " \\n  " + xx.Inb_itm_cd + " / " + xx.Inb_job_no + " / " + xx.qty;
                        }
                        else
                        {                            
                            strBalJobNo = "Balance Available Jobs:-  \\n  " + xx.Inb_itm_cd + " / " + xx.Inb_job_no + " / " + xx.qty;
                        }
                    }              
                //DispMsg(" Balance available from " + strBalJobNo);
               // DisplayMessage(" Balance available from " + strBalJobNo);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + strBalJobNo + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + strBalJobNo + "');", true);
             
            }
        
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Session["SHWSECH"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void gvPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPending.PageIndex = e.NewPageIndex;
                if (_serData != null)
                {
                    if (_serData.Count > 0)
                    {
                        #region Add by Lakshan 21 Sep 2016 remove pending doc
                        List<InventoryRequest> _tempList = new List<InventoryRequest>();
                        List<InventoryRequest> _newListInv = new List<InventoryRequest>();
                        if (chkPendingDoc.Checked)
                        {
                            //bool _docNotAva = false;
                            //foreach (var item in _serData)
                            //{
                            //    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                            //    {
                            //        Tuh_doc_no = item.Itr_req_no,
                            //        Tuh_doc_tp = COM_OUT,
                            //        Tuh_direct = false,
                            //        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                            //    }).FirstOrDefault();
                            //    if (_tmpPickHdr != null)
                            //    {
                            //        if (_tmpPickHdr.Tuh_fin_stus == 1)
                            //        {
                            //            _tempList.Add(item);
                            //        }
                            //    }
                            //}
                            _tempList = _serData.Where(c => c.Itr_pda_comp == 1).ToList();
                            _newListInv = _tempList;
                        }
                        else
                        {
                            _newListInv = _serData;
                        }
                        #endregion
                        hfScrollPosition.Value = "0";
                        gvPending.DataSource = _newListInv;
                    }
                }
                else
                {
                    gvPending.DataSource = new int[] { };
                }
                gvPending.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing !!!");
            }
        }

        protected void gvPending_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        #region Methods

        private void clear()
        {
            chkPendingDoc.Checked = false;
            //lblAllPendin.Visible = false;
            //chkPendingDoc.Visible = false;
            //lblAllPendin.Enabled = false;
            ucOutScan.isbatchserial = false;
            ucOutScan._batchBaseOut = false;
            ucOutScan._prodPlanBaseOut = false;
            _batchBaseOut = false;
            chkAODoutserials.Checked = false; chkpda.Checked = false;
            ucOutScan._derectOut = false;
            _derectOut = false;
            btnSave.Enabled = true;
            btnSave.CssClass = "buttonUndocolorLeft floatRight";
            btnSave.OnClientClick = "SaveConfirma();";
            ucOutScan.PageName = "AOD-OUT";
            _serData = new List<InventoryRequest>();
            UserSeqNo = 0;
            _traList = new List<Transport>();
            Session["TransportMode"] = null;
            //  Session["documntNo"] = "";
            Session["Vehicle"] = "";
            txtFrom.Text = DateTime.Now.AddDays(-7).Date.ToString("dd/MMM/yyyy");
            txtTo.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            LoadDeliveroption();
            gvPending.DataSource = new int[] { };
            gvPending.DataBind();

            gvItems.DataSource = new int[] { };
            gvItems.DataBind();
            Session["ScanItemListNew"] = null;

            COM_OUT = "COM_OUT";
            ddlType.SelectedIndex = 0;
            ucOutScan.doc_tp = COM_OUT;
            ucOutScan.isApprovalSend = false;

            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PageClear();
            ucOutScan.doc_tp = COM_OUT;
            //ucOutScan.isResQTY = false;

            ScanItemList = new List<InventoryRequestItem>();
            _itemdetail = new MasterItem();
            serial_list = new List<ReptPickSerials>();
            SelectedSerialList = new List<ReptPickSerials>();
            _unFinishedDirectDocument = new DataTable();
            _ServiceJobBase = false;

            InitializeForm(true, true);

            // Session["AOD_DIRECT"] = false;
            Session["AOD_DIRECT"] = true;

            txtDispatchRequried.Text = "";
            txtRequest.Text = "";
            gvPending.DataSource = new int[] { };
            gvPending.DataBind();

            chkDirectOut.Checked = false;

            Session["ScanItemListUC"] = null;

            //CHECK SERIAL MAINTANCE LOCATION
            MasterLocationNew _objloc = new MasterLocationNew();
            _objloc.Ml_loc_cd = Session["UserDefLoca"].ToString();
            _objloc.Ml_act = 1;
            // _objloc.Ml_com_cd=Session["UserCompanyCode"].ToString();
            List<MasterLocationNew> _LOC = CHNLSVC.General.GetMasterLocations(_objloc);
            if (_LOC.Count > 0)
            {
                int _isserialMaintan = _LOC.First().Ml_is_serial;
                if (_isserialMaintan == 0)
                {
                    Session["_isserialMaintan"] = false;

                }
                else
                {
                    Session["_isserialMaintan"] = true;
                }

                int _isPDA = _LOC.First().Ml_is_pda;
                if (_isPDA == 1)
                {
                    chkpda.Enabled = true;
                    Session["WAREHOUSE_COM"] = _LOC.First().Ml_wh_com;
                    Session["WAREHOUSE_LOC"] = _LOC.First().Ml_wh_cd;
                }
                else
                {
                    chkpda.Enabled = false;
                }
            }
            PopulateLoadingBays();
            txtBoq.Text = string.Empty;
            if (Session["GlbDefaultBin"] == null)
            {
                lblbinMssg.Text = "BIN  is not allocate for your location.";
                SbuPopup.Show();
                return;
            }
            #region transport data
            _traListDo = new List<Transport>();
            _tMethod = new TransportMethod();
            _tMethods = new List<TransportMethod>();
            _tParty = new TransportParty();
            _tPartys = new List<TransportParty>();
            _tra = new Transport();

            BindTransMethode();
            ddlTransportMe.SelectedIndex = ddlTransportMe.Items.IndexOf(ddlTransportMe.Items.FindByText("COURIER"));
            BindTransService();
            LoadTransportLabelData();
            SHowSearchButton();
            dgvTrns.DataSource = new int[] { };
            dgvTrns.DataBind();
            ddlServiceBy_BindTransService();
            #endregion
        }
        private void BindTransMethode()
        {
            _tMethods = new List<TransportMethod>();
            _tMethod = new TransportMethod();
            _tMethods = CHNLSVC.General.GET_TRANS_METH(_tMethod);
            ddlTransportMe.DataSource = _tMethods.OrderBy(c => c.Rtm_tp);
            ddlTransportMe.DataTextField = "Rtm_tp";
            ddlTransportMe.DataValueField = "rtm_seq";
            ddlTransportMe.DataBind();
        }

        private void InitializeForm(bool _isDocType, bool _isDateReset)
        {
            try
            {
                txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                LinkButton btntest = new LinkButton();
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "", "", btntest, lblBackDateInfor, "m_Trans_Inventory_StockTransferOutward", out  _allowCurrentTrans);
                FillDocumentType();
                SelectedSerialList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>();
                _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>();
                if (_isDateReset)
                    txtFrom.Text = Convert.ToDateTime(DateTime.Today.Date.AddDays(-7)).ToString("dd/MMM/yyyy");
                gvPending.AutoGenerateColumns = false;
                gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                chkDirectOut.Visible = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                UserPermissionforDirectOut();
                if (_isDocType)
                    BindRequestTypesDDLData();
                BindReceiveCompany();
                BindMRNListGridData();
                BindPickSerials(0);
                BindMrnDetail(string.Empty);
                ddlRecCompany.Text = Session["UserCompanyCode"].ToString();
                //CHECK SERIAL MAINTANCE LOCATION
                MasterLocationNew _objloc = new MasterLocationNew();
                _objloc.Ml_loc_cd = Session["UserDefLoca"].ToString();
                _objloc.Ml_act = 1;
                List<MasterLocationNew> _LOC = CHNLSVC.General.GetMasterLocations(_objloc);
                if (_LOC.Count > 0)
                {
                    int _isserialMaintan = _LOC.First().Ml_is_serial;
                    if (_isserialMaintan == 0)
                    {
                        Session["_isserialMaintan"] = false;

                    }
                    else
                    {
                        Session["_isserialMaintan"] = true;
                    }
                }
                bool _isserialMaintan_s = Convert.ToBoolean(Session["_isserialMaintan"].ToString());
                if (_isserialMaintan_s == true)
                {
                    if (!chkDirectOut.Checked)
                    {
                        txtDispatchRequried.Enabled = false;
                        btnSearch_RecLocation.Enabled = false;
                        ddlRecCompany.Enabled = false;
                        if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                        {
                            btnSearch_RecLocation.Enabled = true;
                        }
                    }
                    gvPending.Enabled = true;
                    RequestNo = string.Empty;
                    JobNo = string.Empty;
                    if (ddlType.SelectedValue != "CONS")
                    {
                        UserSeqNo = -100;
                    }
                    ddlManType.Enabled = false;
                }
                else
                {
                    UserSeqNo = -100;
                }
            }
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
        }

        private void DisplayMessage(String Msg)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private void BindRequestTypesDDLData()
        {
            try
            {
                int _selectIndex = 0;
                MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (oLocation.Ml_loc_tp.ToUpper() == "SERS".ToString())
                {
                    _selectIndex = 3;
                }
                ddlType.Items.Clear();
                List<MasterType> _masterType = new List<MasterType>();

                if (oLocation.Ml_cate_1 == "DPS")
                {
                    _masterType = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.REQ.ToString());
                    _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString()));
                    _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.PDA.ToString()));
                    _masterType.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.WHA.ToString()));
                    _masterType = _masterType.FindAll(x => x.Mtp_desc != "Damage Inform Note");
                }

                if (oLocation.Ml_cate_1 == "DFS")
                {
                    _masterType = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.BOND.ToString());
                }

                MasterType oSelect = new MasterType();
                oSelect.Mtp_desc = "Select";
                oSelect.Mtp_cd = "0";
                _masterType.Insert(0, oSelect);

                ddlType.DataSource = _masterType;
                ddlType.DataTextField = "Mtp_desc";
                ddlType.DataValueField = "Mtp_cd";
                if (ddlType.Items.Count > _selectIndex)
                {
                    ddlType.SelectedIndex = _selectIndex;
                }
                //ddlType.Items.IndexOf(ddlType.Items.FindByValue()) _selectIndex;
                ddlType.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void FillDocumentType()
        {
            List<ComboBoxObject> _documentTypeDD = new List<ComboBoxObject>();

            ComboBoxObject _type = new ComboBoxObject();
            _type.Text = "AOD";
            _type.Value = "MDOC_AOD";
            _documentTypeDD.Add(_type);

            _type = new ComboBoxObject();
            _type.Text = "DO";
            _type.Value = "MDOC_DO";
            _documentTypeDD.Add(_type);

            _type = new ComboBoxObject();
            _type.Text = "PRN";
            _type.Value = "MDOC_PRN";
            _documentTypeDD.Add(_type);

            _type = null;

            ddlManType.DataSource = _documentTypeDD;
            ddlManType.DataTextField = "Text";
            ddlManType.DataValueField = "Value";
            ddlManType.DataBind();
        }

        private void UserPermissionforDirectOut()
        {
            //string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, CommonUIDefiniton.UserPermissionType.DIROUT.ToString()))
            //{
            //    chkDirectOut.Enabled = true;
            //}
            //else
            //{
            //    chkDirectOut.Enabled = false;
            //}
        }

        private void BindReceiveCompany()
        {
            try
            {
                ddlRecCompany.DataSource = new List<MasterCompany>();
                List<MasterCompany> _list = CHNLSVC.General.GetALLMasterCompaniesData();
                _list.Insert(0, (new MasterCompany { Mc_cd = "Select" }));
                ddlRecCompany.DataSource = _list;
                ddlRecCompany.DataTextField = "MC_DESC";
                ddlRecCompany.DataValueField = "Mc_cd";
                ddlRecCompany.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
            }
        }

        private void BindMRNListGridData()
        {
            //try
            //{
            if (ddlType.SelectedValue == "PDA")
            {
                //lblAllPendin.Visible = true;
                //chkPendingDoc.Visible = false; 
                //lblAllPendin.Enabled = true;
                Int32 generated_seq = 0;
                string Doctype = "PDA";
                // generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), Doctype, 0, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
                List<ReptPickHeader> _pickhdr = new List<ReptPickHeader>();
                //_pickhdr = CHNLSVC.Inventory.GetAllScanHdrWithDateRange(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), Doctype, 0, Session["UserDefLoca"].ToString());
                ReptPickHeader _repPickHdr = new ReptPickHeader()
                {
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_usr_id = null,
                    Tuh_doc_tp = Doctype,
                    Tuh_isdirect = false,
                    Tuh_usr_loc = Session["UserDefLoca"].ToString()
                };
                DateTime _tmpDt = DateTime.Now;
                DateTime _dtFrom = DateTime.TryParse(txtFrom.Text, out _tmpDt) ? Convert.ToDateTime(txtFrom.Text) : _tmpDt;
                DateTime _dtTo = DateTime.TryParse(txtTo.Text, out _tmpDt) ? Convert.ToDateTime(txtTo.Text) : _tmpDt;
                _dtTo = new DateTime(_dtTo.Year, _dtTo.Month, _dtTo.Day, 23, 59, 59);
                _pickhdr = CHNLSVC.Inventory.GetAllScanHdrWithDateRange(_repPickHdr, 1,
                    _dtFrom,
                    _dtTo
                    );
                if (_pickhdr != null)
                {
                    if (_pickhdr.Count > 0)
                    {
                        List<InventoryRequest> _lstallPDA = new List<InventoryRequest>();
                        foreach (ReptPickHeader _row in _pickhdr)
                        {
                            InventoryRequest _inventoryRequestPDA = new InventoryRequest();
                            //edit Chamal 18-08-2012
                            _inventoryRequestPDA.Itr_issue_com = Session["UserCompanyCode"].ToString();
                            _inventoryRequestPDA.Itr_tp = Doctype;
                            _inventoryRequestPDA.Itr_loc = Session["UserDefLoca"].ToString();
                            _inventoryRequestPDA.Itr_req_no = _row.Tuh_doc_no;
                            _inventoryRequestPDA.Itr_dt = _row.Tuh_cre_dt;
                            _inventoryRequestPDA.Itr_rec_to = _row.Tuh_rec_loc;
                            _inventoryRequestPDA.Itr_com = _row.Tuh_rec_com;
                            _inventoryRequestPDA.Itr_pda_comp = _row.Tuh_fin_stus;
                            _inventoryRequestPDA.Itr_pda_comp_dt = _row.Tuh_fin_time;
                            //_inventoryRequestPDA.Itr_cre_by = !string.IsNullOrEmpty(txtReqBy.Text.Trim()) ? txtReqBy.Text : string.Empty;
                            //_inventoryRequestPDA.Itr_req_no = _hdr.Tuh_doc_no;
                            //_inventoryRequestPDA.Itr_cre_by = _hdr.Tuh_usr_id;
                            _lstallPDA.Add(_inventoryRequestPDA);
                        }

                        if (_lstallPDA != null && _lstallPDA.Count > 0)
                        {
                            if (chkPendingDoc.Checked)
                            {
                                _lstallPDA = _lstallPDA.Where(c => c.Itr_pda_comp == 1).ToList();
                            }
                            _lstallPDA = _lstallPDA.OrderByDescending(x => x.Itr_cre_dt).ThenBy(x => x.Itr_req_no).ToList();
                            hfScrollPosition.Value = "0";
                            gvPending.DataSource = _lstallPDA;
                            gvPending.DataBind();
                            _serData = _lstallPDA;
                            BindPendingGridColor();
                        }
                        else
                        {
                            gvPending.DataSource = new int[] { };
                            gvPending.DataBind();
                        }
                    }

                    return;
                }
            }


            InventoryRequest _inventoryRequest = new InventoryRequest();
            //edit Chamal 18-08-2012
            _inventoryRequest.Itr_issue_com = Session["UserCompanyCode"].ToString();
            _inventoryRequest.Itr_tp = string.IsNullOrEmpty(ddlType.SelectedValue.ToString()) ? "0" : ddlType.SelectedValue.ToString();
            _inventoryRequest.Itr_loc = Session["UserDefLoca"].ToString();
            _inventoryRequest.FromDate = !string.IsNullOrEmpty(txtFrom.Text.Trim()) ? txtFrom.Text : string.Empty;
            _inventoryRequest.ToDate = !string.IsNullOrEmpty(txtTo.Text.Trim()) ? txtTo.Text : string.Empty;
            _inventoryRequest.Itr_cre_by = !string.IsNullOrEmpty(txtReqBy.Text.Trim()) ? txtReqBy.Text : string.Empty;
            List<InventoryRequest> _lstall = new List<InventoryRequest>();
            if (ddlSerTp.SelectedValue == "ReqNo")
            {
                _inventoryRequest.Itr_req_no = !string.IsNullOrEmpty(txtReqNo.Text.Trim()) ? txtReqNo.Text.Trim() + "%" : null;
                _lstall = CHNLSVC.Inventory.GetAllMaterialRequestsListAodScmWeb(_inventoryRequest);
            }
            else
            {
                _inventoryRequest.Itr_ref = !string.IsNullOrEmpty(txtReqNo.Text.Trim()) ? txtReqNo.Text.Trim() + "%" : null;
                _lstall = CHNLSVC.Inventory.GetAllMaterialRequestsListNew(_inventoryRequest);
            }
            List<InventoryRequest> _tempList = new List<InventoryRequest>();
            if (_lstall != null && _lstall.Count > 0)
            {
                hfScrollPosition.Value = "0";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "maintainScrollPosition()", true);
                _lstall = _lstall.OrderByDescending(x => x.Itr_cre_dt).ThenBy(x => x.Itr_req_no).ToList();
                gvPending.DataSource = _lstall;
                gvPending.DataBind();

                var _lst = _lstall.Where(X => X.Itr_tp != "DIN").OrderByDescending(x => x.Itr_dt).ToList();
                #region set fin_stus 18 Nov 2016 lakshan

                //foreach (var item in _lstall)
                //{
                //    item.TMP_Tuh_fin_stus = 0;
                //    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_WITH_COMPLETE_DATE(new ReptPickHeader()
                //    {
                //        Tuh_doc_no = item.Itr_req_no,
                //        Tuh_doc_tp = COM_OUT,
                //        Tuh_direct = false,
                //        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                //    }).FirstOrDefault();
                //    if (_tmpPickHdr != null)
                //    {
                //        item.TMP_Tuh_fin_stus = _tmpPickHdr.Tuh_fin_stus;
                //        item.TMP_Tuh_fin_time = _tmpPickHdr.Tuh_fin_time;
                //    }
                //}

                if (chkPendingDoc.Checked)
                {
                    #region Add by Lakshan 21 Sep 2016 remove pending doc
                    if (_lstall != null)
                    {
                        _tempList = _lstall.Where(c => c.Itr_pda_comp == 1).ToList();
                    }
                    #endregion
                }
                else
                {
                    _tempList = _lstall;
                }
                #endregion

                gvPending.DataSource = _tempList;
                gvPending.DataBind();
                BindPendingGridColor();
                _serData = _tempList;
            }
            else
            {
                gvPending.DataSource = new int[] { };
                gvPending.DataBind();
            }
        }

        protected void BindPickSerials(int _userSeqNo)
        {
            try
            {
                SelectedSerialList.RemoveAll(X => X.Tus_usrseq_no == _userSeqNo);
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                gvSerial.DataSource = setItemStatus2(_list);
                gvSerial.DataBind();

                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, COM_OUT);
                if (_list == null || _list.Count <= 0)
                {

                    if (SelectedSerialList != null)
                        if (SelectedSerialList.Count > 0)
                        {
                            gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                            gvSerial.DataBind();
                        }
                        else
                        {
                            gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                            gvSerial.DataBind();
                        }
                    return;
                }

                SelectedSerialList.AddRange(_list);
                /* gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                 gvSerial.DataBind();
                 if (SelectedSerialList.Count > 0 && _derectOut)
                 {


                     List<ReptPickItems> _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _userSeqNo });
                     if (_repItmList != null)
                     {
                         if (_repItmList.Count > 0)
                         {
                            // ViewState["ItemList"]
                             gvItems.DataSource = _repItmList;
                         }
                     }
                     gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                     gvSerial.DataBind(); return;
                 }*/
                gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                gvSerial.DataBind();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
            }
        }

        protected void BindMrnDetail(string _mrn, string oldItemcode = "", int _userSeqNo = 0)
        {
            try
            {

                if (ddlType.SelectedValue == "PDA")
                {
                    List<ReptPickItems> _listitm = new List<ReptPickItems>();
                    _listitm = CHNLSVC.Inventory.GetAllPickItem(Session["UserCompanyCode"].ToString(), _userSeqNo, "PDA", _mrn, null, null);
                    if (_listitm != null)
                    {
                        // txtDispatchRequried.Text = Session["UserDefLoca"].ToString();
                        BindReceiveCompany();
                        ddlRecCompany.Text = Session["UserCompanyCode"].ToString();
                        List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                        foreach (ReptPickItems _itm in _listitm)
                        {
                            InventoryRequestItem objitm = new InventoryRequestItem();
                            objitm.Itri_itm_cd = _itm.Tui_req_itm_cd;
                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Tui_req_itm_cd);
                            objitm.Mi_longdesc = msitem.Mi_longdesc;
                            objitm.Mi_model = msitem.Mi_model;
                            objitm.Itri_itm_stus = _itm.Tui_req_itm_stus;
                            objitm.Itri_job_line = Convert.ToInt32(_itm.Tui_pic_itm_cd);
                            objitm.Itri_app_qty = _itm.Tui_pic_itm_qty;
                            objitm.Itri_qty = _itm.Tui_pic_itm_qty;
                            objitm.Itri_note = _mrn;
                            _lst.Add(objitm);
                        }
                        gvItems.DataSource = setItemStatus1(_lst);
                        gvItems.DataBind();
                        LoadMrnData(_lst);
                        Session["ScanItemListNew"] = _lst;

                    }
                    return;
                }


                if (ScanItemList == null)
                    ScanItemList = new List<InventoryRequestItem>();
                if (!string.IsNullOrEmpty(_mrn))
                {
                    List<InventoryRequestItem> _lst = CHNLSVC.Inventory.GetMaterialRequestItemByRequestNoList(_mrn);
                    //Add by lakshan
                    if (_lst != null)
                    {
                        foreach (var item in _lst)
                        {
                            item.Itri_app_qty = item.Itri_bqty;
                        }
                    }
                    // List<InventoryBatchN> _intBatch = CHNLSVC.Inventory.Get_Inr_Batch(_invBatch);
                    if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                    {
                        foreach (var item in _lst)
                        {
                            List<InventoryBatchRefN> _listInr_batch = CHNLSVC.Inventory.Get_Inr_Batch(
                                new InventoryBatchRefN()
                                {
                                    Inb_job_no = item.Itri_job_no,
                                    Inb_base_refline = item.Itri_job_line
                                });
                            var dataAva = _listInr_batch.Where(c => c.Inb_qty > 0).ToList(); ;
                            InventoryBatchRefN _intBatch = dataAva.FirstOrDefault();
                            if (_intBatch != null)
                            {
                                if (item.Itri_itm_cd != _intBatch.Inb_itm_cd && item.Itri_itm_cd == _intBatch.Inb_base_itmcd)
                                {
                                    item.Itri_itm_cd = _intBatch.Inb_itm_cd;
                                    item.Itri_mitm_cd = _intBatch.Inb_base_itmcd;
                                }
                                else if (item.Itri_itm_cd != _intBatch.Inb_itm_cd && item.Itri_itm_cd != _intBatch.Inb_base_itmcd)
                                {
                                    item.Itri_itm_cd = _intBatch.Inb_base_itmcd;
                                    item.Itri_mitm_cd = _intBatch.Inb_itm_cd;
                                }
                                else
                                {
                                    item.Itri_mitm_cd = _intBatch.Inb_itm_cd;
                                }
                            }
                        }
                    }
                    if (_lst == null || _lst.Count <= 0)
                    {
                        if (!_derectOut)
                        {
                            if (!chkDirectOut.Checked)
                            {
                                DisplayMessage("There is no pending items to be issued. Please contact IT Dept !");
                            }
                            gvItems.DataSource = setItemStatus1(ScanItemList);
                            gvItems.DataBind();
                            LoadMrnData(ScanItemList);
                            Session["ScanItemListNew"] = ScanItemList;
                            ViewState["ItemList"] = ScanItemList;
                            ucOutScan.ScanItemList = ScanItemList;
                            return;
                        }
                        else
                        {
                            gvItems.DataSource = setItemStatus1(ScanItemList);
                            gvItems.DataBind();
                            LoadMrnData(ScanItemList);
                            Session["ScanItemListNew"] = ScanItemList;
                            ViewState["ItemList"] = ScanItemList;
                            ucOutScan.ScanItemList = ScanItemList;
                            List<ReptPickItems> _listitm = new List<ReptPickItems>();
                            _listitm = CHNLSVC.Inventory.GetAllPickItem(Session["UserCompanyCode"].ToString(), UserSeqNo, COM_OUT, _mrn, null, null);
                            if (_listitm != null)
                            {
                                BindReceiveCompany();
                                ddlRecCompany.Text = Session["UserCompanyCode"].ToString();
                                List<InventoryRequestItem> _newList = new List<InventoryRequestItem>();
                                foreach (ReptPickItems _itm in _listitm)
                                {
                                    InventoryRequestItem objitm = new InventoryRequestItem();
                                    objitm.Itri_itm_cd = _itm.Tui_req_itm_cd;
                                    MasterItem msitem = new MasterItem();
                                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Tui_req_itm_cd);
                                    objitm.Mi_longdesc = msitem.Mi_longdesc;
                                    objitm.Mi_model = msitem.Mi_model;
                                    objitm.Itri_itm_stus = _itm.Tui_req_itm_stus;
                                    objitm.Itri_job_line = Convert.ToInt32(_itm.Tui_pic_itm_cd);
                                    objitm.Itri_app_qty = _itm.Tui_pic_itm_qty;
                                    objitm.Itri_qty = _itm.Tui_pic_itm_qty;
                                    objitm.Itri_note = _mrn;
                                    _newList.Add(objitm);
                                }
                                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, COM_OUT);
                                foreach (InventoryRequestItem item in _newList)
                                {
                                    item.Itri_qty = _list.FindAll(x => x.Tus_itm_cd == item.Itri_itm_cd && x.Tus_itm_stus == item.Itri_itm_stus).Count;
                                    item.Itri_app_qty = item.Itri_qty;
                                    item.Itri_bqty = item.Itri_app_qty - item.Itri_qty;
                                }
                                gvItems.DataSource = setItemStatus1(_newList);
                                gvItems.DataBind();
                                LoadMrnData(_newList);
                                Session["ScanItemListNew"] = _newList;
                            }
                            return;
                        }
                    }
                    _lst.ForEach(X => X.Itri_note = _mrn);
                    _lst.Where(z => z.Itri_note == _mrn).ToList().ForEach(x => x.Itri_qty = 0);
                    if (SelectedSerialList != null)
                    {
                        if (SelectedSerialList.Count > 0)
                        {
                            if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                            {
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd).Sum(z => z.Tus_qty));
                            }
                            else
                            {
                                //ADd by laksha n
                                //base serial_items issue
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd
                                    && y.Tus_itm_stus == x.Itri_itm_stus
                                   && x.Itri_line_no == y.Tus_base_itm_line
                                    ).Sum(z => z.Tus_qty));
                            }
                        }
                    }
                    if (oldItemcode == "")
                    {
                        foreach (InventoryRequestItem item in _lst)
                        {
                            ScanItemList.RemoveAll(x => x.Itri_itm_cd == item.Itri_itm_cd && x.Itri_itm_stus == item.Itri_itm_stus && x.Itri_app_qty == item.Itri_app_qty && x.Itri_res_no == item.Itri_res_no && x.Itri_res_no == item.Itri_res_no);
                        }
                    }
                    else
                    {
                        foreach (InventoryRequestItem item in _lst)
                        {
                            ScanItemList.RemoveAll(x => x.Itri_itm_cd == oldItemcode && x.Itri_itm_stus == item.Itri_itm_stus && x.Itri_app_qty == item.Itri_app_qty && x.Itri_res_no == item.Itri_res_no && x.Itri_res_no == item.Itri_res_no);
                        }
                    }


                    ScanItemList.AddRange(_lst);
                    //List<ReptPickSerials> _list = new List<ReptPickSerials>();
                    //_list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, COM_OUT);

                    //foreach (InventoryRequestItem item in ScanItemList)
                    //{
                    //    item.Itri_qty = _list.FindAll(x => x.Tus_itm_cd == item.Itri_itm_cd && x.Tus_itm_stus == item.Itri_itm_stus).Count;
                    //}

                    gvItems.DataSource = setItemStatus1(ScanItemList);
                    gvItems.DataBind();
                    lblDocQty.Text = "";
                    lblDocSerPickQty.Text = "";
                    if (ScanItemList != null)
                    {
                        decimal _reqQty = ScanItemList.Sum(c => c.Itri_app_qty);
                        decimal _pickQty = ScanItemList.Sum(c => c.Itri_qty);
                        lblDocQty.Text = _reqQty.ToString();
                        lblDocSerPickQty.Text = _pickQty.ToString();
                    }
                    Session["ScanItemListNew"] = ScanItemList;
                    ViewState["ItemList"] = ScanItemList;
                    ucOutScan.ScanItemList = ScanItemList;
                    bool _isserialMaintan = Convert.ToBoolean(Session["_isserialMaintan"].ToString());
                    if (_isserialMaintan == false)
                    {
                        check_itm_grn(setItemStatus1(ScanItemList));
                    }

                }
                else
                {
                    gvItems.DataSource = new int[] { };
                    gvItems.DataBind();
                    Session["ScanItemListNew"] = null;
                }

                if (chkDirectOut.Checked)
                {
                    List<ReptPickSerials> _list = new List<ReptPickSerials>();
                    _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, COM_OUT);

                    foreach (InventoryRequestItem item in ScanItemList)
                    {
                        item.Itri_qty = _list.FindAll(x => x.Tus_itm_cd == item.Itri_itm_cd && x.Tus_itm_stus == item.Itri_itm_stus).Count;
                    }
                    gvItems.DataSource = setItemStatus1(ScanItemList);
                    gvItems.DataBind();
                    LoadMrnData(ScanItemList);
                    Session["ScanItemListNew"] = ScanItemList;
                    ViewState["ItemList"] = ScanItemList;
                    ucOutScan.ScanItemList = ScanItemList;
                }

                //add rukshan - 20-May-2016
                foreach (InventoryRequestItem _itm in ScanItemList)
                {
                    _itm.Itri_bqty = _itm.Itri_app_qty - _itm.Itri_qty;
                    bool _isserialMaintan = Convert.ToBoolean(Session["_isserialMaintan"]);
                    if (!_isserialMaintan)
                    {
                        _itm.Itri_qty = 0;
                        //_itm.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == _itm.Itri_itm_cd && y.Tus_itm_stus == _itm.Itri_itm_stus).Sum(z => z.Tus_qty);
                        //Chg by lakshan
                        _itm.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == _itm.Itri_itm_cd && y.Tus_itm_stus == _itm.Itri_itm_stus && y.Tus_base_itm_line == _itm.Itri_line_no).Sum(z => z.Tus_qty);
                    }
                }
                gvItems.DataSource = setItemStatus1(ScanItemList);
                gvItems.DataBind();
                LoadMrnData(ScanItemList);
                Session["ScanItemListNew"] = ScanItemList;
                ViewState["ItemList"] = ScanItemList;
                ucOutScan.ScanItemList = ScanItemList;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
            }
        }

        private void BackDatePermission()
        {
            try
            {
                bool _allowCurrentTrans = false;
                LinkButton btntest = new LinkButton();
                IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "", txtDate.Text, btntest, lblBackDateInfor, "m_Trans_Inventory_StockTransferOutward", out  _allowCurrentTrans);

                //IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, this.Page,txtDate.Text,null, "m_Trans_Inventory_StockTransferOutward", out _allowCurrentTrans);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
            }
        }

        protected void BindSelectedMRNDetail(int _rowIndex)
        {
            bool _isOk = true;
            if (!chkDirectOut.Checked)
            {
                #region R31

                GridViewRow dr = gvPending.Rows[_rowIndex];
                Label lblitr_req_no = dr.FindControl("lblitr_req_no") as Label;
                Label lblitr_job_no = dr.FindControl("lblitr_job_no") as Label;
                Label lblitr_gran_nstus = dr.FindControl("lblitr_gran_nstus") as Label;
                Label lblitr_gran_app_stus = dr.FindControl("lblitr_gran_app_stus") as Label;
                Label lblitr_com = dr.FindControl("lblitr_com") as Label;
                Label lblitr_rec_to = dr.FindControl("lblitr_rec_to") as Label;
                Label lblitr_tp = dr.FindControl("lblitr_tp") as Label;
                CheckBox _chk = dr.FindControl("pen_Select") as CheckBox;

                RequestNo = lblitr_req_no.Text;
                if (lblitr_job_no != null && !string.IsNullOrEmpty(lblitr_job_no.Text))
                    JobNo = lblitr_job_no.Text;

                string GRNAStatus = lblitr_gran_nstus.Text.Trim();
                string AppGRANStatus = lblitr_gran_app_stus.Text.Trim();

                if (lblitr_tp.Text.Trim() != CommonUIDefiniton.MasterTypeCategory.GRAN.ToString())
                {
                    UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(lblitr_tp.Text, Session["UserCompanyCode"].ToString(), RequestNo, 0);
                }
                else
                {
                    UserSeqNo = -1;
                }
                ucOutScan.userSeqNo = UserSeqNo.ToString();
                ViewState["userSeqNo"] = UserSeqNo;
                lblseq.Text = UserSeqNo.ToString();
                int grnInitialUserSeq = UserSeqNo;
                ddlRecCompany.DataSource = null;
                ddlRecCompany.DataBind();

                txtDispatchRequried.Text = string.Empty;
                string _company = lblitr_com.Text.Trim();

                string _dispatchLocation = lblitr_rec_to.Text.Trim();
                string _reqtype = lblitr_tp.Text.Trim();
                string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                ddlRecCompany.Items.Clear();
                ddlRecCompany.Items.Add(_company);
                ddlRecCompany.SelectedIndex = 0;
                txtDispatchRequried.Text = _dispatchLocation;

                #endregion
                //if (UserSeqNo == -1)
                //UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), COM_OUT, 1, Session["UserCompanyCode"].ToString());

                try
                {
                    if (grnInitialUserSeq == -1 && _reqtype.Trim() == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString())
                    {
                        #region RMain
                        List<InventoryRequestSerials> _list = CHNLSVC.Inventory.GetAllGRANSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), CommonUIDefiniton.MasterTypeCategory.GRAN.ToString(), RequestNo);
                        if (_list == null || _list.Count <= 0)
                        {
                            DisplayMessage("It seems GRNA entry does not having serial details. Please check the GRAN entry!");
                            _isOk = false;
                            _chk.Checked = false;
                            return;
                        }
                        bool _isLowStock = false;
                        string _lowstockitem = string.Empty;
                        foreach (InventoryRequestSerials _one in _list)
                        {
                            string _serial = _one.Itrs_ser_1; string _item = _one.Itrs_itm_cd;
                            Int64 _serialId = _one.Itrs_ser_id; MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                            if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                            {
                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.GetReservedByserialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item, Convert.ToInt32(_serialId));
                                if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                                {
                                    if (string.IsNullOrEmpty(_lowstockitem))
                                        _lowstockitem = " item : " + _item + ",serial : " + _serial;
                                    else _lowstockitem += ", item : " + _item + ",serial : " + _serial;
                                    _isLowStock = true;
                                }
                            }
                            else
                            {
                                #region R1
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _one.Itrs_itm_stus);
                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_one.Itrs_qty > _invBal)
                                        {
                                            if (string.IsNullOrEmpty(_lowstockitem))
                                                _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                            else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                            _isLowStock = true;
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(_lowstockitem))
                                            _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                        else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                        _isLowStock = true;
                                    }
                                else
                                {
                                    if (string.IsNullOrEmpty(_lowstockitem))
                                        _lowstockitem = " item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                    else _lowstockitem += ", item : " + _item + ", status : " + _one.Itrs_itm_stus;
                                    _isLowStock = true;
                                }
                                #endregion
                            }
                        }
                        if (_isLowStock)
                        {
                            DisplayMessage("There is no stock for the following item(s)." + _lowstockitem);
                        }
                        foreach (InventoryRequestSerials _one in _list)
                        {
                            #region R2
                            string _serial = _one.Itrs_ser_1;
                            string _item = _one.Itrs_itm_cd;
                            Int64 _serialId = _one.Itrs_ser_id;
                            Int32 generated_seq = -1;
                            Int32 userseq_no;
                            Int32 user_seq_num = 0;
                            #endregion


                            if (UserSeqNo == 0) user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, Session["UserCompanyCode"].ToString(), RequestNo, 0);

                            else user_seq_num = UserSeqNo;
                            if (user_seq_num != -1)
                            {
                                #region 2
                                generated_seq = user_seq_num;
                                userseq_no = generated_seq;
                                UserSeqNo = userseq_no;
                                if (grnInitialUserSeq == -1)
                                {
                                    userseq_no = generated_seq;
                                    UserSeqNo = userseq_no;

                                    ReptPickHeader RPH = new ReptPickHeader();
                                    RPH.Tuh_doc_tp = COM_OUT;
                                    RPH.Tuh_cre_dt = DateTime.Today;
                                    RPH.Tuh_ischek_itmstus = false;
                                    RPH.Tuh_ischek_reqqty = false;
                                    RPH.Tuh_ischek_simitm = false;
                                    RPH.Tuh_session_id = Session["SessionID"].ToString();
                                    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                                    RPH.Tuh_usr_id = Session["UserID"].ToString();
                                    RPH.Tuh_usrseq_no = generated_seq;
                                    RPH.Tuh_direct = false;
                                    RPH.Tuh_doc_no = RequestNo;
                                    if (ddlType.SelectedValue == "MRNA")
                                    {
                                        RPH.Tuh_is_take_res = true;
                                    }
                                    if (ddlType.SelectedValue == "MRNS")
                                    {
                                        RPH.Tuh_is_take_res = true;
                                    }
                                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                                    grnInitialUserSeq = 100;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 3
                                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), COM_OUT, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                                ViewState["userSeqNo"] = generated_seq;
                                ucOutScan.userSeqNo = generated_seq.ToString();
                                userseq_no = generated_seq;
                                UserSeqNo = userseq_no;
                                ReptPickHeader RPH = new ReptPickHeader();
                                RPH.Tuh_doc_tp = COM_OUT;
                                RPH.Tuh_cre_dt = DateTime.Today;
                                RPH.Tuh_ischek_itmstus = false;
                                RPH.Tuh_ischek_reqqty = false;
                                RPH.Tuh_ischek_simitm = false;
                                RPH.Tuh_session_id = Session["SessionID"].ToString();
                                RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                                RPH.Tuh_usr_id = Session["UserID"].ToString();
                                RPH.Tuh_usrseq_no = generated_seq;
                                RPH.Tuh_direct = false;
                                RPH.Tuh_doc_no = RequestNo;
                                if (ddlType.SelectedValue == "MRNA")
                                {
                                    RPH.Tuh_is_take_res = true;
                                }
                                if (ddlType.SelectedValue == "MRNS")
                                {
                                    RPH.Tuh_is_take_res = true;
                                }
                                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                                #endregion
                            }

                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                            if (msitem.Mi_is_ser1 == 1 || msitem.Mi_is_ser1 == 0)
                            {
                                #region 4
                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.GetReservedByserialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item, Convert.ToInt32(_serialId));
                                if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                                    continue;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, Convert.ToInt32(_serialId), -1);
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_base_doc_no = RequestNo;
                                _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no;
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(_reptPickSerial_.Tus_ser_id);
                                _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus;
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                #endregion
                            }
                            else
                            {
                                #region 1
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                _reptPickSerial_.Tus_base_doc_no = RequestNo;
                                _reptPickSerial_.Tus_base_itm_line = _one.Itrs_line_no;
                                _reptPickSerial_.Tus_bin = _defbin;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
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
                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                _reptPickSerial_.Tus_new_status = _one.Itrs_nitm_stus;
                                _reptPickSerial_.Tus_qty = _one.Itrs_qty;
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_ser_line = 0;
                                _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                                _reptPickSerial_.Tus_unit_cost = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_warr_no = "N/A";
                                _reptPickSerial_.Tus_warr_period = 0;
                                _reptPickSerial_.Tus_new_remarks = "AOD-OUT";

                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                #endregion
                            }
                        }
                        grnInitialUserSeq = -1;
                        #endregion
                    }
                    else if (UserSeqNo == -1)
                    {
                        UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                        ViewState["userSeqNo"] = UserSeqNo;
                        ucOutScan.userSeqNo = UserSeqNo.ToString();
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = false;
                        RPH.Tuh_ischek_reqqty = false;
                        RPH.Tuh_ischek_simitm = false;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = UserSeqNo;
                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = RequestNo;
                        //Add by Lakshan
                        Int32 _isResQty = 0;
                        if (ddlType.SelectedValue == "MRNA")
                        {
                            //List<InventoryRequestItem> _tmpReqItm = new List<InventoryRequestItem>();
                            //_tmpReqItm = CHNLSVC.Inventory.GetMaterialRequestItemByRequestNoList(RequestNo);
                            //if (_tmpReqItm != null)
                            //{
                            //    if (_tmpReqItm.Count > 0)
                            //    {
                            //        decimal _resQty = _tmpReqItm.Where(c => c.Itri_res_qty > 0).Sum(c => c.Itri_res_qty);
                            //        decimal _sendQty = _tmpReqItm.Where(c => c.Itri_qty > 0).Sum(c => c.Itri_res_qty);
                            //        if (_resQty > 0)
                            //        {
                            //            _isResQty = 1;
                            //        }
                            //    }
                            //}
                            _isResQty = 1;
                            RPH.Tuh_is_take_res = true;
                        }
                        if (ddlType.SelectedValue == "MRNS")
                        {
                            _isResQty = 1;
                            RPH.Tuh_is_take_res = true;
                        }
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                    txtRequest.Text = RequestNo;
                    txtRequest.ToolTip = RequestNo;
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 3);
                    _isOk = false;
                    return;
                }
                finally
                {
                    if (_isOk)
                    {
                        BindPickSerials(UserSeqNo);

                        BindMrnDetail(RequestNo, "", UserSeqNo);

                        if (chkAODoutserials.Checked)
                        {
                            List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                            _lst = Session["ScanItemListNew"] as List<InventoryRequestItem>;
                            string _docno = string.Empty;
                            foreach (InventoryRequestItem _itm in _lst)
                            {
                                _docno = _itm.Itri_note;
                                var _filterItem = SelectedSerialList.Where(x => x.Tus_itm_cd == _itm.Itri_itm_cd).ToList();
                                if (_filterItem.Count == 0)
                                {
                                    MasterItem msitem = new MasterItem();
                                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Itri_itm_cd);
                                    if (msitem.Mi_is_ser1 == 0)
                                    {
                                        if (_filterItem.Count == 0)
                                        {
                                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                            _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                                            _reptPickSerial_.Tus_seq_no = UserSeqNo;
                                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                            _reptPickSerial_.Tus_base_doc_no = _itm.Itri_note;
                                            _reptPickSerial_.Tus_base_itm_line = _itm.Itri_line_no;
                                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                            _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                            _reptPickSerial_.Tus_itm_cd = _itm.Itri_itm_cd;
                                            _reptPickSerial_.Tus_itm_stus = _itm.Itri_itm_stus;
                                            _reptPickSerial_.Tus_itm_line = _itm.Itri_line_no;
                                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_itm.Itri_bqty);
                                            _reptPickSerial_.Tus_ser_1 = "N/A";
                                            _reptPickSerial_.Tus_ser_2 = "N/A";
                                            _reptPickSerial_.Tus_ser_3 = "N/A";
                                            _reptPickSerial_.Tus_ser_4 = "N/A";
                                            _reptPickSerial_.Tus_ser_id = 0;
                                            _reptPickSerial_.Tus_serial_id = "0";
                                            _reptPickSerial_.Tus_unit_cost = 0;
                                            _reptPickSerial_.Tus_unit_price = 0;
                                            _reptPickSerial_.Tus_unit_price = 0;
                                            _reptPickSerial_.Tus_job_no = JobNo;
                                            //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                            if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                                            {
                                                _reptPickSerial_.Tus_job_no = _itm.Itri_job_no;
                                                _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                                            }
                                            else
                                            {
                                                //Add by Chamal 26-May-2018 due to Rathmalana AOD Issue UHA/036/0518W0518
                                                if (!string.IsNullOrEmpty(JobNo))
                                                {
                                                    _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                                                }
                                            }
                                            //_reptPickSerial_.Tus_exist_supp = suppler;
                                            //_reptPickSerial_.Tus_orig_supp = suppler;
                                            if (_itm.Itri_res_qty > 0)
                                            {
                                                _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Itri_bqty);
                                            }
                                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                        }
                                    }
                                    if (msitem.Mi_is_ser1 == -1)
                                    {
                                        if (_filterItem.Count == 0)
                                        {
                                            decimal _availableQty = CHNLSVC.Inventory.GET_INR_LOC_BAL_DATA(new InventoryLocation()
                                            {
                                                Inl_com = Session["UserCompanyCode"].ToString(),
                                                Inl_loc = Session["UserDefLoca"].ToString(),
                                                Inl_itm_cd = _itm.Itri_itm_cd,
                                                Inl_itm_stus = _itm.Itri_itm_stus
                                            });
                                            if (_availableQty > 0)
                                            {
                                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                                _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                                                _reptPickSerial_.Tus_seq_no = UserSeqNo;
                                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                                _reptPickSerial_.Tus_base_doc_no = _itm.Itri_note;
                                                _reptPickSerial_.Tus_base_itm_line = _itm.Itri_line_no;
                                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                                _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                                _reptPickSerial_.Tus_itm_cd = _itm.Itri_itm_cd;
                                                _reptPickSerial_.Tus_itm_stus = _itm.Itri_itm_stus;
                                                _reptPickSerial_.Tus_itm_line = _itm.Itri_line_no;
                                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_itm.Itri_bqty);
                                                if (_availableQty < _reptPickSerial_.Tus_qty)
                                                {
                                                    _reptPickSerial_.Tus_qty = _availableQty;
                                                }
                                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                                _reptPickSerial_.Tus_ser_id = 0;
                                                _reptPickSerial_.Tus_serial_id = "0";
                                                _reptPickSerial_.Tus_unit_cost = 0;
                                                _reptPickSerial_.Tus_unit_price = 0;
                                                _reptPickSerial_.Tus_unit_price = 0;
                                                _reptPickSerial_.Tus_job_no = JobNo;
                                                //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                                //_reptPickSerial_.Tus_job_line = JobLineNo;
                                                //_reptPickSerial_.Tus_exist_supp = suppler;
                                                //_reptPickSerial_.Tus_orig_supp = suppler;
                                                if (_itm.Itri_res_qty > 0)
                                                {
                                                    _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Itri_bqty);
                                                }
                                                if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                                                {
                                                    _reptPickSerial_.Tus_job_no = _itm.Itri_job_no;
                                                    _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                                                }
                                                else
                                                {
                                                    //Add by Chamal 26-May-2018 due to Rathmalana AOD Issue UHA/036/0518W0518
                                                    if (!string.IsNullOrEmpty(JobNo))
                                                    {
                                                        _reptPickSerial_.Tus_job_line = _itm.Itri_job_line;
                                                    }
                                                }
                                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                            }
                                        }
                                    }
                                }
                            }
                            BindPickSerials(UserSeqNo);

                            BindMrnDetail(RequestNo, "", UserSeqNo);

                        }
                    }
                }
            }
            else
            {
                BindReceiveCompany();
            }
        }

        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = Session["UserCompanyCode"].ToString();//ABL
            string _selectCompany = ddlRecCompany.Text.ToString();//AAL
            string _itemReceiveCompany = _master.Irsm_rec_com;//AAL
            string _comOutDocType = "NON";
            if (_userCompany == _selectCompany)
                _comOutDocType = "AOD-OUT";
            else if (_selectCompany == _itemReceiveCompany)
                _comOutDocType = "PRN";
            else if (_selectCompany != _itemReceiveCompany)
                _comOutDocType = "DO";
            if (_master.Irsm_itm_stus == "CONS") _comOutDocType = "AOD-OUT"; //Add by Chamal 06-May-2014
            return _comOutDocType;
        }

        protected void BindDirectDetail(string _mrn)
        {
            try
            {
                if (ScanItemList == null) ScanItemList = new List<InventoryRequestItem>();
                if (!string.IsNullOrEmpty(_mrn) && SelectedSerialList != null && SelectedSerialList.Count > 0)
                {
                    List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                    var _itemdet = (from _l in SelectedSerialList group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus } into batch select new { Tus_itm_cd = batch.Key.Tus_itm_cd, Tus_itm_stus = batch.Key.Tus_itm_stus, Tus_qty = batch.Sum(p => p.Tus_qty) }).ToList();
                    if (_itemdet != null && _itemdet.Count > 0)
                    {
                        int _line = 1;
                        foreach (var _n in _itemdet)
                        {
                            InventoryRequestItem _one = new InventoryRequestItem();
                            MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _n.Tus_itm_cd);
                            _one.Itri_app_qty = _n.Tus_qty; _one.Itri_bqty = _n.Tus_qty;
                            _one.Itri_itm_cd = _n.Tus_itm_cd; _one.Itri_itm_stus = _n.Tus_itm_stus;
                            _one.Itri_line_no = _line; _line += 1;
                            _one.Itri_mitm_cd = _n.Tus_itm_cd; _one.Itri_mitm_stus = _n.Tus_itm_stus;
                            _one.Itri_mqty = _n.Tus_qty; _one.Itri_note = _mrn;
                            _one.Itri_qty = _n.Tus_qty; _one.Mi_longdesc = _itm.Mi_longdesc;
                            _one.Mi_model = _itm.Mi_model; _lst.Add(_one);
                        }
                    }
                    _lst.ForEach(X => X.Itri_note = _mrn);
                    _lst.Where(z => z.Itri_note == _mrn).ToList().ForEach(x => x.Itri_qty = 0);
                    if (SelectedSerialList != null)
                        if (SelectedSerialList.Count > 0)
                            if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd).Sum(z => z.Tus_qty));
                            else
                                _lst.ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == _mrn && y.Tus_itm_cd == x.Itri_itm_cd && y.Tus_itm_stus == x.Itri_itm_stus).Sum(z => z.Tus_qty));
                    ScanItemList.AddRange(_lst);
                    foreach (var item in ScanItemList)
                    {
                        item.Itri_bqty = 0;
                    }
                    gvItems.DataSource = setItemStatus1(ScanItemList);
                    gvItems.DataBind();
                    LoadMrnData(ScanItemList);
                    Session["ScanItemListNew"] = ScanItemList;
                }
                else
                {
                    gvItems.DataSource = new int[] { };
                    gvItems.DataBind();
                    Session["ScanItemListNew"] = null;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void OnRemoveFromSerialGrid(GridViewRow dr)
        {
            try
            {
                Label lbltus_itm_cd = dr.FindControl("lbltus_itm_cd") as Label;
                Label lbltus_itm_stus = dr.FindControl("lbltus_itm_stus") as Label;
                Label lbltus_ser_id = dr.FindControl("lbltus_ser_id") as Label;
                Label lbltus_bin = dr.FindControl("lbltus_bin") as Label;
                Label lbltus_ser_1 = dr.FindControl("lbltus_ser_1") as Label;
                Label lbltus_base_doc_no = dr.FindControl("lbltus_base_doc_no") as Label;
                Label lbltus_qty = dr.FindControl("lbltus_qty") as Label;
                LinkButton btntus_itm_stus = dr.FindControl("btntus_itm_stus") as LinkButton;

                int row_id = dr.RowIndex;

                if (string.IsNullOrEmpty(lbltus_itm_cd.Text))
                    return;

                string _item = lbltus_itm_cd.Text;
                string _status = lbltus_itm_stus.Text.Trim();
                Int32 _serialID = (Int32)Convert.ToDecimal(lbltus_ser_id.Text.Trim());
                string _bin = lbltus_bin.Text.Trim();
                string serial_1 = lbltus_ser_1.Text.Trim();
                string _requestno = lbltus_base_doc_no.Text.Trim();
                decimal _decimalQty = decimal.TryParse(lbltus_qty.Text, out _decimalQty) ? Convert.ToDecimal(lbltus_qty.Text) : 0;
                string _stsCd = btntus_itm_stus.Text;
                if (!chkDirectOut.Checked)
                {
                    UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(COM_OUT, Session["UserCompanyCode"].ToString(), _requestno, 0);
                    RequestNo = _requestno;
                }
                else
                {
                    UserSeqNo = UserSeqNo;
                    RequestNo = _requestno;
                }

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), _item, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _stsCd);
                }
                if (Session["ScanItemListUC"] != null)
                {
                    ScanItemList = (List<InventoryRequestItem>)Session["ScanItemListUC"];
                }
                if (_masterItem.Mi_is_ser1 == -1)
                {
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _stsCd).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - _decimalQty);
                }
                else
                {
                    ScanItemList.Where(t => t.Itri_itm_cd == _item && t.Itri_itm_stus == _status).ToList().ForEach(x => x.Itri_qty = x.Itri_qty - 1);
                }
                BindPickSerials(UserSeqNo);
                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        gvItems.DataSource = null;
                        gvItems.DataSource = setItemStatus1(ScanItemList);
                        gvItems.DataBind();
                        LoadMrnData(ScanItemList);
                        Session["ScanItemListNew"] = ScanItemList;
                    }
                    else BindMrnDetail(RequestNo);
                BindMrnDetail(RequestNo);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void OnRemoveFromItemGrid(GridViewRow dr)
        {
            if (hdfItemdel.Value == "No")
            {
                return;
            }

            if (chkDirectOut.Checked == false)
            {
                DisplayMessage("You cannot remove the item which is allocated by request");
                return;
            }
            Label lblitri_itm_cd = dr.FindControl("lblitri_itm_cd1") as Label;
            Label lblitri_itm_stus = dr.FindControl("lblitri_itm_stus") as Label;
            Label lblitri_qty = dr.FindControl("lblitri_qty") as Label;

            int row_id = dr.RowIndex;
            if (string.IsNullOrEmpty(lblitri_itm_cd.Text))
                return;
            string _item = Convert.ToString(lblitri_itm_cd.Text);
            string _itmStatus = Convert.ToString(lblitri_itm_stus.Text);
            decimal _qty = Convert.ToDecimal(lblitri_qty.Text);
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, COM_OUT);

            if (_list != null)
                if (_list.Count > 0)
                {
                    var _delete = (from _lst in _list where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus select _lst).ToList();
                    foreach (ReptPickSerials _ser in _delete)
                    {
                        string _items = _ser.Tus_itm_cd;
                        Int32 _serialID = _ser.Tus_ser_id;
                        string _bin = _ser.Tus_bin;
                        string serial_1 = _ser.Tus_ser_1;

                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                        if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        {
                            CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), null, null);
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                        }
                        else
                        {
                            CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _itmStatus);
                        }
                    }
                }
            ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
            ucOutScan.ScanItemList = ScanItemList;
            if (ScanItemList != null)
                if (ScanItemList.Count > 0)
                {
                    gvItems.DataSource = null;
                    gvItems.DataSource = setItemStatus1(ScanItemList);
                    gvItems.DataBind();
                    LoadMrnData(ScanItemList);
                    Session["ScanItemListNew"] = ScanItemList;
                }
                else
                    BindMrnDetail(RequestNo);
            BindPickSerials(UserSeqNo);
        }

        private bool LoadDirectType()
        {
            bool _isOk = false;
            DataTable _tbl = CHNLSVC.Inventory.GetDirectOutType(Session["UserCompanyCode"].ToString());
            _tbl.Rows.InsertAt(_tbl.NewRow(), 0);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _isOk = true;
                cmbDirType.DataTextField = "DIRTP_DESC";
                cmbDirType.DataValueField = "DIRTP_TP";
                cmbDirType.DataSource = _tbl;
                cmbDirType.DataBind();
            }
            else _isOk = false;
            return _isOk;
        }

        private void LoadUnFinishedDirectDocument()
        {
            if (_unFinishedDirectDocument == null)
                _unFinishedDirectDocument = new DataTable();
            _unFinishedDirectDocument = CHNLSVC.Inventory.GetDirectUnFinishedDocument(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), COM_OUT);
            if (_unFinishedDirectDocument != null && _unFinishedDirectDocument.Rows.Count > 0)
            {
                if (_unFinishedDirectDocument.Rows.Count > 0)
                {
                    DataView dv = _unFinishedDirectDocument.DefaultView;
                    dv.Sort = "tuh_usrseq_no ASC";
                    _unFinishedDirectDocument = dv.ToTable();
                }
                DataRow _rows = null;
                _rows = _unFinishedDirectDocument.NewRow();
                _unFinishedDirectDocument.Rows.InsertAt(_rows, 0);
                cmbDirectScan.DataTextField = "tuh_usrseq_no";
                cmbDirectScan.DataValueField = "tuh_usrseq_no";

                cmbDirectScan.DataSource = _unFinishedDirectDocument;
                cmbDirectScan.DataBind();
            }
        }

        protected void CheckLocation()
        {
            if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                string _tmpCom = Convert.ToString(ddlRecCompany.Text);
                if (ddlType.SelectedValue == "CONS")
                {
                    _tmpCom = "";
                }
                DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(_tmpCom, txtDispatchRequried.Text.Trim().ToUpper());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Dispatch location is invalid. Please check the location.");
                    txtDispatchRequried.Text = "";
                    txtDispatchRequried.Focus();
                    txtDispatchRequried.Enabled = true;
                    cmbDirType.Enabled = true;
                    return;
                }
                if (_tbl != null) if (_tbl.Rows.Count > 0)
                    {
                        string _fromcompany = Session["UserCompanyCode"].ToString();
                        string _fromlocation = Session["UserDefLoca"].ToString();
                        string _tocompany = Convert.ToString(ddlRecCompany.Text);
                        string _tocategory = _tbl.Rows[0].Field<string>("Ml_cate_3");
                        DataTable _adpoint = CHNLSVC.Inventory.GetSubLocation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        if (_adpoint != null && _adpoint.Rows.Count > 0)
                        {
                            var _one = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_loc_cd") == txtDispatchRequried.Text.Trim().ToUpper()).ToList();
                            var _two = _adpoint.AsEnumerable().Where(x => x.Field<string>("ml_main_loc_cd") == txtDispatchRequried.Text.Trim().ToUpper()).ToList();
                            if (_one.Count > 0 && _two.Count <= 0)
                                goto xy;
                            if (_one.Count <= 0 && _two.Count > 0)
                                goto xy;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue)))
                            goto xy;
                        DataTable _permCatwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "AODOUT_DIRECT");

                        bool _isserialMaintan = Convert.ToBoolean(Session["_isserialMaintan"].ToString());
                        if (_isserialMaintan == true)
                        {
                            if (ddlType.SelectedValue == "CONS")
                            {
                                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode("", txtDispatchRequried.Text.Trim().ToUpper());
                                if (_mstLoc != null)
                                {
                                    _tocompany = _mstLoc.Ml_com_cd;
                                }

                            }
                            DataTable _permLocwise = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, txtDispatchRequried.Text.Trim().ToUpper(), "AODOUT_DIRECT");
                            if (_permLocwise == null || _permLocwise.Rows.Count <= 0)
                            {
                                if (_permCatwise == null || _permCatwise.Rows.Count <= 0)
                                {
                                    if (ddlType.SelectedValue != "EX" && ddlType.SelectedValue != "RE" && ddlType.SelectedValue != "BOI" && ddlType.SelectedValue != "EXP")
                                    {
                                        DisplayMessage("Permission Required for the dispatch location. Please check the location !");
                                        txtDispatchRequried.Text = "";
                                        txtDispatchRequried.Focus();
                                        txtDispatchRequried.Enabled = true;
                                        cmbDirType.Enabled = true;
                                        return;
                                    }
                                }
                            }
                        }


                    }
            xy:
                string _defualloc = Session["UserDefLoca"].ToString(); string _selectedLoc = txtDispatchRequried.Text.Trim().ToUpper();
                try
                {
                    if (ddlRecCompany.Text.ToString() == Session["UserCompanyCode"].ToString())
                    {
                        if (_defualloc.Trim() == _selectedLoc.Trim())
                        {
                            txtDispatchRequried.Text = string.Empty;
                            txtDispatchRequried.Enabled = true;
                            cmbDirType.Enabled = true;
                            DisplayMessage("You cannot make outward entry to the same location !");
                            return;
                        }
                    }
                    txtDispatchRequried.Enabled = false; cmbDirType.Enabled = false;
                }
                catch (Exception ex)
                {
                    txtDispatchRequried.Enabled = true;
                    cmbDirType.Enabled = true;
                    CHNLSVC.CloseChannel();
                }
            }
        }

        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_StockTransferOutward", txtDate, lblBackDateInfor, Convert.ToDateTime(txtDate.Text).Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        DisplayMessage("Selected date is not allowed for transaction !");
                        txtDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    DisplayMessage("Selected date is not allowed for transaction !");
                    txtDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        private bool IsAllScaned(List<ReptPickSerials> _list)
        {
            bool _isok = false;
            if (ScanItemList != null && _list != null)
            {
                //foreach (var item in ScanItemList)
                //{
                //    var _serList = _list.Where(c => c.Tus_itm_cd == item.Itri_itm_cd && c.Tus_itm_stus == item.Itri_itm_stus
                //        && c.Tus_base_itm_line == Convert.ToInt32(item.Tus_base_itm_line)).ToList();
                //    if (_serList!=null)
                //    {
                //        if (_serList.Count>0)
                //        {

                //        }
                //    }
                //}
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    Label lblitri_itm_cd1 = gvItems.Rows[i].FindControl("lblitri_itm_cd1") as Label;
                    Label lblitri_qty = gvItems.Rows[i].FindControl("lblitri_qty") as Label;
                    Label lblitri_note = gvItems.Rows[i].FindControl("lblitri_note") as Label;
                    Label lblitri_itm_stus = gvItems.Rows[i].FindControl("lblitri_itm_stus") as Label;
                    Label lblitri_line_no = gvItems.Rows[i].FindControl("lblitri_line_no") as Label;

                    string _item = lblitri_itm_cd1.Text.Trim();
                    decimal _scanQty = Convert.ToDecimal(lblitri_qty.Text);
                    decimal _itmLine = Convert.ToDecimal(lblitri_line_no.Text);
                    string _document = lblitri_note.Text.Trim();
                    string _status = lblitri_itm_stus.Text.Trim();
                    decimal _serialcount = 0;

                    if (chkDirectOut.Checked == false)
                    {
                        if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                            _serialcount = (from _l in _list where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document select _l.Tus_qty).Sum();
                        else if (Convert.ToString(ddlType.SelectedValue) == "PDA")
                            _serialcount = (from _l in _list
                                            where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status
                                            select _l.Tus_qty).Sum();
                        else
                            _serialcount = (from _l in _list
                                            where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status
                                                && _l.Tus_base_itm_line == _itmLine
                                            select _l.Tus_qty).Sum();
                    }
                    else
                    {
                        _serialcount = (from _l in _list
                                        where _l.Tus_itm_cd == _item && _l.Tus_base_doc_no == _document && _l.Tus_itm_stus == _status
                                        select _l.Tus_qty).Sum();
                    }
                    if (_scanQty != _serialcount)
                    {
                        _isok = false;
                        break;
                    }
                    else _isok = true;
                }
            }
            return _isok;
        }

        #endregion

        #region Search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InventoryItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ucOutScan.TXTItemCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "AOD" + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        string recCom = ddlRecCompany.SelectedValue.ToString();
                        paramsText.Append(recCom + seperator); break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator); break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductionPlan:
                    {
                        //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "PRO" + seperator + "A" + seperator +
                        //    txtDispatchRequried.Text.ToString() + seperator);                        
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "PRO" + seperator + "A" + seperator +
                            Session["UserDefLoca"].ToString() + seperator);

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BOQ:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "BOQ" + seperator + "A" + seperator +
                            Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                //dilshan on 13/03/2018
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPartyCd.Text.Trim().ToUpper() + seperator + txtPartyVal.Text.Trim().ToUpper() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Helper:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "HELPER" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Driver:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "DRIVER" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                //    {
                //        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + oMstCom.Mc_anal19 + seperator + txtFDate.Text + seperator + txtTDate.Text + seperator + txtCustCode.Text + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Customer:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + 1 + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Location:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "DO" + seperator + "0" + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerQuo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                //*********************
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnsearchComClose_Click(object sender, EventArgs e)
        {
            Session["SHWSECH"] = null;
            mpSearch.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SHWSECH"] = "123";
                //mpSearch.Show();
                Session["SHWSECH"] = null;
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
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
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                mpSearch.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "Batch")
                {
                    txtBoq.Text = grdResult.SelectedRow.Cells[2].Text;
                    lblvalue.Text = "";
                    loadBoqItem(txtBoq.Text);
                    Session["SHWSECH"] = null;
                    ViewState["SEARCH"] = null;
                    return;
                }
                else if (lblvalue.Text == "ProductionPlan")
                {
                    txtBoq.Text = grdResult.SelectedRow.Cells[1].Text;
                    _prodNo = txtBoq.Text;
                    ucOutScan._prodNo = txtBoq.Text;
                    lblvalue.Text = "ProductionPlan";
                    Session["SHWSECH"] = null;
                    ViewState["SEARCH"] = null;
                    return;
                }
                else if (lblvalue.Text == "ReqLocation")
                {
                    txtReqLoc.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtReqLoc.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    Session["SHWSECH"] = null;
                    ViewState["SEARCH"] = null;
                }
                else if (lblvalue.Text == "boq")
                {
                    txtBoq.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblvalue.Text = "";
                    loadBoqItem(txtBoq.Text);
                    Session["SHWSECH"] = null;
                    ViewState["SEARCH"] = null;
                    return;
                }
                else if (lblvalue.Text == "SystemUser")
                {
                    txtReqBy.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtReqBy.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    Session["SHWSECH"] = null;
                    ViewState["SEARCH"] = null;
                }
                else if (lblvalue.Text == "Location")
                {
                    txtDispatchRequried.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDispatchRequried.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtDispatchRequried_TextChanged(null, null);
                    mpSearch.Hide();
                }
                else if (lblvalue.Text == "ItemS")
                {

                    GridViewRow drSelectedRow = (GridViewRow)Session["SelectedRow"];
                    if (drSelectedRow == null)
                    {
                        DisplayMessage("Error occurred");
                        return;
                    }

                    Label lblitri_note = (Label)drSelectedRow.FindControl("lblitri_note");
                    Label lblitri_line_no = (Label)drSelectedRow.FindControl("lblitri_line_no");
                    Label lblitri_itm_cd1 = (Label)drSelectedRow.FindControl("lblitri_itm_cd1");


                    string NewItemCode = grdResult.SelectedRow.Cells[1].Text;

                    if (!string.IsNullOrEmpty(NewItemCode))
                    {
                        string _old_item = lblitri_itm_cd1.Text;
                        string _new_item = NewItemCode;
                        int result = CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(lblitri_note.Text, (Int32)Convert.ToDecimal(lblitri_line_no.Text.Trim()), _old_item, _new_item);
                        if (result > 0)
                        {
                            DisplayMessage("Successfully updated", 2);
                            BindMrnDetail(lblitri_note.Text, _old_item);
                            return;
                        }
                    }
                }

                Session["SHWSECH"] = null;
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 1);
            }
        }

        private void FilterData()
        {
            try
            {

                if (lblvalue.Text == "boq")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.General.SearchBOQDocNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "boq";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                if (lblvalue.Text == "ProductionPlan")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                    DataTable result = CHNLSVC.CommonSearch.SearchProductionNoAodOut(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "ProductionPlan";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                    return;
                }
                else if (lblvalue.Text == "SystemUser")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                    DataTable result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "SystemUser";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "ItemS")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "ItemS";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Location")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Location";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "ReqLocation")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "ReqLocation";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 1);
            }
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            if (lblvalue.Text == "ProductionPlan")
            {
                if (_dataSource.Columns.Contains("Date"))
                {
                    _dataSource.Columns.Remove("Date");
                }
            }
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }


        #endregion

        protected void btnSearchUsr_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "SystemUser";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnDocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = null;
                if (!string.IsNullOrEmpty(txtFrom.Text))
                {
                    if (string.IsNullOrEmpty(txtTo.Text))
                    {
                        DisplayMessage("Please select the date range!");
                        return;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtTo.Text))
                    {
                        DisplayMessage("Please select the date range!");
                        return;
                    }
                }

                if (ddlType.SelectedIndex == 0)
                {
                    DisplayMessage("Please select a document type");
                    return;
                }
                InitializeForm(false, false);
                // BindMRNListGridData();
                List<InventoryRequest> list = _serData;

                if (list.Count > 0)
                {
                    List<InventoryRequest> newlist = new List<InventoryRequest>();
                    #region Add by Lakshan 21 Sep 2016 remove pending doc
                    List<InventoryRequest> _tempList = new List<InventoryRequest>();
                    if (chkPendingDoc.Checked)
                    {
                        _tempList = list.Where(c => c.Itr_pda_comp == 1).ToList();
                    }
                    else
                    {
                        _tempList = list;
                    }
                    #endregion
                    #region set fin_stus 18 Nov 2016
                    //foreach (var item in _tempList)
                    //{
                    //    item.TMP_Tuh_fin_stus = 0;
                    //    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_WITH_COMPLETE_DATE(new ReptPickHeader()
                    //        {
                    //            Tuh_doc_no = item.Itr_req_no,
                    //            Tuh_doc_tp = COM_OUT,
                    //            Tuh_direct = false,
                    //            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                    //        }).FirstOrDefault();
                    //    if (_tmpPickHdr != null)
                    //    {
                    //        item.TMP_Tuh_fin_stus = _tmpPickHdr.Tuh_fin_stus;
                    //    }
                    //}
                    #endregion
                    if (chkPendingDoc.Checked)
                    {
                        hfScrollPosition.Value = "0";
                        _tempList = _tempList.OrderBy(c => c.TMP_Tuh_fin_time).ToList();
                        gvPending.DataSource = _tempList;
                        gvPending.DataBind();
                    }
                    if (txtReqLoc.Text != "")
                    {
                        foreach (InventoryRequest ir in _tempList)
                        {
                            if (ir.Itr_loc == txtReqLoc.Text)
                            {
                                newlist.Add(ir);
                            }
                        }
                        hfScrollPosition.Value = "0";
                        gvPending.DataSource = newlist;
                        gvPending.DataBind();

                    }
                    else
                    {
                        #region color change 18 Nov 2016
                        gvPending.DataSource = _tempList;
                        gvPending.DataBind();
                        #endregion
                    }
                    BindPendingGridColor();

                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
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
        protected void Btnitr_req_no_Click(object sender, EventArgs e)
        {

            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            ucOutScan._derectOut = false;
            try
            {
                _desableSerAdd = false;
                CheckBox _chk = dr.FindControl("pen_Select") as CheckBox;
                Label lblitr_req_no = dr.FindControl("lblitr_req_no") as Label;
                Label lblitr_tp = dr.FindControl("lblitr_tp") as Label;
                Label lblitr_com = dr.FindControl("lblitr_com") as Label;
                Label lblitr_job_no = dr.FindControl("lblitr_job_no") as Label;
                Label lblitr_sub_tp = dr.FindControl("lblitr_sub_tp") as Label;
                Label lblitr_rec_to = dr.FindControl("lblitr_rec_to") as Label;
                Label lblitr_vehi_no = dr.FindControl("lblitr_vehi_no") as Label;

                DataTable CurUsing = GetRequestCurrentUser(lblitr_req_no.Text);
                MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                bool isPDA = loc.Ml_is_pda;
                if (!isPDA && ddlType.SelectedValue == "PDA")
                {
                    DispMsg("Location is not a PDA location : " + Session["UserDefLoca"].ToString()); return;
                }
                if (CurUsing != null)
                {
                    DataRow row = CurUsing.Rows[0];
                    DisplayMessage("Request is in process by " + row[29].ToString(), 2);

                }
                if (isPDA == true || (isPDA == false && CurUsing == null))
                {

                    if (Session["Vehicle"] != "")
                    {
                        if (Session["Vehicle"].ToString() != lblitr_vehi_no.Text)
                        {
                            DisplayMessage("can not select different type vehicle number", 1);
                            return;
                        }
                    }
                    bool _isReturn = false;

                    string _document = string.Empty;
                    string _documenttype = string.Empty;
                    string _selectJobNo = string.Empty;
                    Session["Vehicle"] = lblitr_vehi_no.Text;
                    try
                    {
                        //Add by lakshan
                        MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        if (_mstLocation != null)
                        {
                            if (!_mstLocation.Ml_is_serial)
                            {
                                int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, Session["UserCompanyCode"].ToString(), lblitr_req_no.Text, 0);
                                if (_usrSeq != -1)
                                {
                                    ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, ddlType.SelectedValue);
                                    if (_ReptPickHeader != null)
                                    {
                                        if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                        {
                                            _desableSerAdd = true;
                                        }
                                    }
                                }
                            }
                        }


                        _document = lblitr_req_no.Text.Trim();
                        _documenttype = lblitr_tp.Text.Trim();
                        ucOutScan.ScanDocument = _document;
                        string _recCompany = lblitr_com.Text.Trim();
                        _selectJobNo = lblitr_job_no.Text.Trim();
                        Session["_selectJobNo"] = _selectJobNo;
                        Session["_documenttype"] = _documenttype;
                        _ServiceJobBase = false;
                        if (_documenttype == "PDA")
                        {
                            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10001))
                            {
                                DisplayMessage("Sorry, You have no permission to direct out! - ( Advice: Required permission code :10001)");
                                return;
                            }
                            else
                            {
                                txtDispatchRequried.Enabled = true;
                                btnSearch_RecLocation.Enabled = true;
                                ddlRecCompany.Enabled = true;
                                chkDirectOut.Visible = false;
                            }
                        }
                        if (_documenttype == "MRN" && lblitr_sub_tp.Text == "SCV")
                        {
                            int i = 0;
                            bool _alreadySelect = false;
                            string _jobNoCurrentSelect = string.Empty;
                            _ServiceJobBase = true;
                            for (i = 0; i <= gvPending.Rows.Count - 1; i++)
                            {
                                if (_chk.Checked)
                                {
                                    _alreadySelect = true;
                                    _jobNoCurrentSelect = lblitr_job_no.Text.Trim();
                                }
                            }

                            if (_alreadySelect == true)
                            {
                                if (_selectJobNo != _jobNoCurrentSelect)
                                {
                                    _chk.Checked = false;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (_documenttype != "PDA")
                            {
                                bool _isserialMaintan_s = Convert.ToBoolean(Session["_isserialMaintan"].ToString());
                                if (_isserialMaintan_s == true)
                                {
                                    txtDispatchRequried.Enabled = false;
                                    btnSearch_RecLocation.Enabled = false;
                                    ddlRecCompany.Enabled = false;
                                    chkDirectOut.Visible = false;
                                    if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                                    {
                                        btnSearch_RecLocation.Enabled = true;
                                    }
                                }
                            }
                        }

                        string _recLocation = lblitr_rec_to.Text.Trim();

                        if (gvItems.Rows.Count > 0 && Convert.ToBoolean(_chk.Checked) == false)
                        {
                            string _ddlCompany = ddlRecCompany.Text.Trim(); string _txtDispatchLocation = txtDispatchRequried.Text.Trim().ToUpper();
                            if (_recCompany != _ddlCompany)
                            {
                                DisplayMessage("Request company is mismatched with the selected document.");
                                _isReturn = true;
                                return;
                            }
                            if (_recLocation != _txtDispatchLocation)
                            {
                                DisplayMessage("Request location is mismatched with the selected document.");
                                _isReturn = true;
                                return;
                            }
                        }

                        if (_isReturn == false)
                            if (_documenttype == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && Convert.ToBoolean(_chk.Checked) == false)
                            {
                                _chk.Checked = true;
                                // var _notBaseDIN = (from GridViewRow _row in gvPending.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_row.Cells[0]).Value) == true && Convert.ToString(_row.Cells[3].Value) == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && string.IsNullOrEmpty(Convert.ToString(_row.Cells["pen_BaseDIN"].Value)) select _row).Count();
                                //var _BaseDIN = (from DataGridViewRow _row in gvPending.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_row.Cells[0]).Value) == true && Convert.ToString(_row.Cells[3].Value) == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString() && !string.IsNullOrEmpty(Convert.ToString(_row.Cells["pen_BaseDIN"].Value)) select _row).Count();

                                int _notBaseDIN = 0;
                                int _BaseDIN = 0;

                                for (int i = 0; i < gvPending.Rows.Count; i++)
                                {
                                    CheckBox _chkT = gvPending.Rows[i].FindControl("pen_Select") as CheckBox;
                                    Label lblitr_tpT = dr.FindControl("lblitr_tp") as Label;
                                    Label lblitr_anal1T = dr.FindControl("lblitr_anal1") as Label;
                                    if (_chkT.Checked && lblitr_tpT.Text.ToUpper() == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString().ToUpper() && string.IsNullOrEmpty(lblitr_anal1T.Text))
                                    {
                                        _notBaseDIN = _notBaseDIN + 1;
                                    }
                                    if (_chkT.Checked && lblitr_tpT.Text.ToUpper() == CommonUIDefiniton.MasterTypeCategory.GRAN.ToString().ToUpper() && !string.IsNullOrEmpty(lblitr_anal1T.Text))
                                    {
                                        _BaseDIN = _BaseDIN + 1;
                                    }


                                }

                                _chk.Checked = false;
                                if (_notBaseDIN > 0 && _BaseDIN > 0)
                                {
                                    DisplayMessage("You cannot select damage information note(DIN) based GRAN with usual GRAN !");
                                    _chk.Checked = true;
                                }
                            }
                        if (_isReturn == false)
                            if (_chk.Checked)
                            {
                                _chk.Checked = false;
                                ScanItemList.RemoveAll(x => x.Itri_note == _document);
                                gvItems.DataSource = null;
                                gvItems.DataSource = setItemStatus1(ScanItemList);
                                gvItems.DataBind();
                                LoadMrnData(ScanItemList);
                                Session["ScanItemListNew"] = ScanItemList;
                                SelectedSerialList.RemoveAll(x => x.Tus_base_doc_no == _document);
                                gvSerial.DataSource = null;
                                gvSerial.DataSource = setItemStatus2(SelectedSerialList);
                                gvSerial.DataBind();
                            }
                            else
                            {
                                _chk.Checked = true;
                                BindSelectedMRNDetail(dr.RowIndex);
                            }
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message);
                        CHNLSVC.CloseChannel();
                        _isReturn = true;
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 3);
            }

        }

        protected void ddlManType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked == true)
                {
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToString(ddlManType.SelectedValue));
                    if (_NextNo != 0)
                        txtManualRef.Text = _NextNo.ToString();
                    else
                        txtManualRef.Text = string.Empty;
                }
                else txtManualRef.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked)
                {
                    ddlManType.Enabled = true;
                    ddlManType_SelectedIndexChanged(null, null);
                    txtManualRef.Enabled = false;
                }
                else
                {
                    ddlManType.Enabled = false;
                    txtManualRef.Enabled = true;
                    txtManualRef.Text = "";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtManualRef_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtManualRef.Text != "" && chkManualRef.Checked)
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToString(ddlManType.SelectedValue), Convert.ToInt32(txtManualRef.Text));
                    if (_IsValid == false)
                    {
                        DisplayMessage("Invalid Manual Document Number !");
                        txtManualRef.Text = "";
                        txtManualRef.Focus();
                        return;
                    }
                }
                else
                {
                    if (chkManualRef.Checked == true)
                    {
                        DisplayMessage("Invalid Manual Document Number !");
                        txtManualRef.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void cmbDirectScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSerialList = new List<ReptPickSerials>();
            ScanItemList = new List<InventoryRequestItem>();
            _itemdetail = new MasterItem();
            serial_list = new List<ReptPickSerials>();
            gvPending.AutoGenerateColumns = false;
            gvItems.AutoGenerateColumns = false;
            gvSerial.AutoGenerateColumns = false;
            if (!string.IsNullOrEmpty(Convert.ToString(cmbDirectScan.SelectedValue)))
            {
                RequestNo = Convert.ToString(cmbDirectScan.SelectedValue); UserSeqNo = Convert.ToInt32(RequestNo);
                var _docdet = _unFinishedDirectDocument.AsEnumerable().Where(y => !string.IsNullOrEmpty(y.Field<string>("tuh_usr_id"))).Where(x => x.Field<Int64>("tuh_usrseq_no") == UserSeqNo && x.Field<string>("tuh_usr_loc") == Session["UserDefLoca"].ToString()).CopyToDataTable();
                if (_docdet != null && _docdet.Rows.Count > 0)
                {
                    ddlRecCompany.DataSource = null;
                    ddlRecCompany.DataBind();

                    txtDispatchRequried.Text = string.Empty;
                    string _reccompany = _docdet.Rows[0].Field<string>("tuh_rec_com");
                    string _reclocation = _docdet.Rows[0].Field<string>("tuh_rec_loc");
                    string _recdirtype = _docdet.Rows[0].Field<string>("tuh_dir_type");
                    ddlRecCompany.Items.Add(_reccompany);
                    ddlRecCompany.SelectedIndex = 0;
                    txtDispatchRequried.Text = _reclocation;
                    cmbDirType.SelectedValue = _recdirtype;
                    ddlRecCompany.Enabled = false;
                    txtDispatchRequried.Enabled = false;
                    cmbDirType.Enabled = false;
                    string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    ddlRecCompany.DataBind();
                    ddlRecCompany.SelectedIndex = ddlRecCompany.Items.IndexOf(ddlRecCompany.Items.FindByValue(Session["UserCompanyCode"].ToString()));
                }
                else
                {
                    DisplayMessage("Selected unfinished document is not valid.");
                    return;
                }
            }
            else
            {
                BindReceiveCompany();
                ddlRecCompany.SelectedValue = Session["UserCompanyCode"].ToString();
                UserSeqNo = -1;
                RequestNo = "-1";
                ddlRecCompany.Enabled = true;
                txtDispatchRequried.Enabled = true;
                cmbDirType.Enabled = true;
            }
            BindPickSerials(UserSeqNo);
            BindDirectDetail(RequestNo);
        }

        protected void lblitri_itm_cd_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkChangeSimilarItem.Checked == false)
                {
                    GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                    LinkButton lblitri_itm_cd = dr.FindControl("lblitri_itm_cd") as LinkButton;
                    Label lblitri_itm_cd1 = dr.FindControl("lblitri_itm_cd1") as Label;
                    Label lblitri_itm_stus = dr.FindControl("lblitri_itm_stus") as Label;
                    Label lblitri_qty = dr.FindControl("lblitri_qty") as Label;
                    Label lblitri_app_qty = dr.FindControl("lblitri_app_qty") as Label;
                    Label lblitri_line_no = dr.FindControl("lblitri_line_no") as Label;
                    Label lblitri_note = dr.FindControl("lblitri_note") as Label;
                    Label lblitri_job_no = dr.FindControl("lblitri_job_no") as Label;
                    Label lblitri_job_line = dr.FindControl("lblitri_job_line") as Label;
                    Label lblitri_res_qty = dr.FindControl("lblitri_res_qty") as Label;

                    ucOutScan.TXTItemCode.Text = lblitri_itm_cd1.Text;
                    //.BindBoxType(ucOutScan.TXTItemCode.Text.ToUpper());

                    string _item = lblitri_itm_cd1.Text.Trim();
                    string _status = lblitri_itm_stus.Text.Trim();
                    decimal _pickedQty = Convert.ToDecimal(lblitri_qty.Text);
                    decimal _approvedQty = Convert.ToDecimal(lblitri_app_qty.Text.Trim());
                    int _lineno = (Int32)Convert.ToDecimal(lblitri_line_no.Text);

                    if (_approvedQty == 0)
                    {
                        DisplayMessage("Approved Qty is 0");
                        return;
                    }

                    if (_pickedQty == _approvedQty)
                        return;
                    string _documentno = Convert.ToString(lblitri_note.Text);
                    if (chkDirectOut.Checked && !string.IsNullOrEmpty(Convert.ToString(cmbDirType.SelectedValue)) && !string.IsNullOrEmpty(Convert.ToString(cmbDirectScan.SelectedValue)))
                        _documentno = RequestNo;

                    UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(Session["_documenttype"].ToString(), Session["UserCompanyCode"].ToString(), _documentno, 0);
                    RequestNo = _documentno;
                    if (UserSeqNo == -1)
                    {
                        UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), Session["_documenttype"].ToString(), 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                        if (chkDirectOut.Checked)
                            RequestNo = Convert.ToString(UserSeqNo);

                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = Session["_documenttype"].ToString();
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = false;
                        RPH.Tuh_ischek_reqqty = false;
                        RPH.Tuh_ischek_simitm = false;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = UserSeqNo;
                        RPH.Tuh_direct = false;
                        RPH.Tuh_rec_com = Convert.ToString(ddlRecCompany.SelectedValue);
                        RPH.Tuh_rec_loc = Convert.ToString(txtDispatchRequried.Text.ToUpper());
                        RPH.Tuh_isdirect = chkDirectOut.Checked; RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        RPH.Tuh_dir_type = Convert.ToString(cmbDirType.SelectedValue);
                        RPH.Tuh_doc_no = RequestNo;
                        if (ddlType.SelectedValue == "MRNA")
                        {
                            RPH.Tuh_is_take_res = true;
                        }
                        if (ddlType.SelectedValue == "MRNS")
                        {
                            RPH.Tuh_is_take_res = true;
                        }
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                        if (chkDirectOut.Checked)
                            ScanItemList.ForEach(x => x.Itri_note = RequestNo);
                    }
                    ucOutScan.ScanDocument = RequestNo; //_commonOutScan.ScanDocument = RequestNo;
                    ucOutScan.PickSerial = new List<ReptPickSerials>();// _commonOutScan.SelectedItemList = new List<ReptPickSerials>();
                    ucOutScan.doc_tp = Session["_documenttype"].ToString();// _commonOutScan.DocumentType = COM_OUT;
                    ucOutScan.TXTItemCode.Text = _item;// _commonOutScan.PopupItemCode = _item;
                    ucOutScan.PopupQty = Convert.ToDecimal(_approvedQty);
                    ucOutScan.ApprovedQty = Convert.ToDecimal(_approvedQty);
                    ucOutScan.ScanQty = Convert.ToDecimal(_pickedQty);
                    ucOutScan.ItemStatus = _status;
                    ucOutScan.ItemLineNo = _lineno;
                    ucOutScan.JobNo = lblitri_job_no.Text;
                    ucOutScan.MainItemCode = lblitri_itm_cd1.Text.Trim();
                    ucOutScan.JobLineNo = (Int32)Convert.ToDecimal(lblitri_job_line.Text);
                    ucOutScan.userSeqNo = UserSeqNo.ToString();
                    ViewState["userSeqNo"] = UserSeqNo.ToString();
                    //ucOutScan.ScanItemList = UserSeqNo.ToString();
                    // ViewState["ItemList"]
                    decimal resQty = 0;
                    resQty = decimal.TryParse(lblitri_res_qty.Text, out resQty) ? Convert.ToDecimal(lblitri_res_qty.Text) : 0;
                    if (resQty > 0)
                    {
                        ucOutScan.isResQTY = true;
                    }
                    else
                    {
                        ucOutScan.isResQTY = false;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ddlType.SelectedValue)))
                    {
                        if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                        {
                            ucOutScan.IsCheckStatus = false;
                        }
                        else
                        {
                            ucOutScan.IsCheckStatus = true;
                        }
                    }
                    else
                    {
                        ucOutScan.IsCheckStatus = true;
                    }

                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10102) == true)
                        ucOutScan.IsCheckStatus = false;

                    ucOutScan.ModuleTypeNo = 3;
                    ucOutScan.ItemCodeChange();
                }
                else
                {
                    GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                    Session["SelectedRow"] = dr;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "ItemS";
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void btnSer_Remove_Click(object sender, EventArgs e)
        {
            if (hdfDelSerila.Value == "Yes")
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                OnRemoveFromSerialGrid(dr);
            }
        }

        protected void btnItm_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                ScanItemList = ucOutScan.ScanItemList;
                OnRemoveFromItemGrid(dr);

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucOutScan.isbatchserial = false;
                _batchBaseOut = false;
                ucOutScan._batchBaseOut = false;
                ucOutScan._prodPlanBaseOut = false;
                //lblAllPendin.Visible = false;
                //lblAllPendin.Visible = false;
                //lblAllPendin.Enabled = false;
                if (ddlType.SelectedIndex > 0)
                {
                    ddlType.ToolTip = ddlType.SelectedItem.Text;
                    // chkPendingDoc.Checked = false;
                }
                if (ddlType.SelectedValue != "CONS")
                {
                    COM_OUT = ddlType.SelectedValue;
                    if (COM_OUT == "MRNS")
                    {
                        COM_OUT = "MRNA";
                    }
                }

                //chkDirectOut.Visible = false;
                ucOutScan.doc_tp = COM_OUT;
                gvPending.DataSource = new int[] { };
                gvPending.DataBind();

                gvItems.DataSource = new int[] { };
                gvItems.DataBind();

                InitializeForm(false, false);
                ucOutScan._derectOut = false;
                if (chkDirectOut.Checked)
                {
                    ucOutScan._derectOut = true;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        #region Pick Serial

        protected void btnPSPClose_Click(object sender, EventArgs e)
        {
            mpPickSerial.Hide();
        }

        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {
            FilerSerialSearch();

        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            FilerSerialSearch();
        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (_mstLocation != null)
                {
                    if (!_mstLocation.Ml_is_serial)
                    {
                        int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, Session["UserCompanyCode"].ToString(), txtRequest.Text, 0);
                        if (_usrSeq != -1)
                        {
                            ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue);
                            if (_ReptPickHeader != null)
                            {
                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                {
                                    DisplayMessage("Serials are already picked from PDA !");
                                    return;
                                }
                            }
                        }
                    }
                }
                if (!(bool)Session["_isserialMaintan"])
                {
                    if (string.IsNullOrEmpty(txtSearchbywordA.Text))
                    {
                        DisplayMessage("Please enter qty");
                        txtSearchbywordA.Focus();
                        return;
                    }
                }
                string selectedItem = Session["SelectedItem"].ToString();
                string SelectedDoc = Session["SelectedDoc"].ToString();
                string SelectedItemStatus = Session["SelectedItemStatus"].ToString();
                string SelectedItemLine = Session["SelectedItemLine"].ToString();
                string SelectedJobNum = Session["SelectedJobNum"].ToString();
                string SelectedJobLine = Session["SelectedJobLine"].ToString();

                MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), selectedItem);
                if (_itm.Mi_is_ser1 == -1 || !((bool)Session["_isserialMaintan"]))
                {
                    if (string.IsNullOrEmpty(txtSearchbywordA.Text))
                    {
                        DisplayMessage("Please enter qty");
                        txtSearchbywordA.Focus();
                        return;
                    }
                    if (Convert.ToDecimal(lblPopupQty.Text) < Convert.ToDecimal(txtSearchbywordA.Text))
                    {
                        if (!_derectOut)
                        {
                            DisplayMessageJS("Can't exceed the request Qty!");
                            txtPopupQty.Text = string.Empty;
                            txtPopupQty.Focus();
                            return;
                        }
                    }

                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), selectedItem, SelectedItemStatus);
                    if (_inventoryLocation != null)
                    {
                        if (_inventoryLocation.Count == 1)
                        {
                            foreach (InventoryLocation _loc in _inventoryLocation)
                            {
                                decimal _formQty = Convert.ToDecimal(txtPopupQty.Text);
                                if (_formQty > _loc.Inl_free_qty)
                                {
                                    DisplayMessage("Please check the inventory balance!");
                                    txtPopupQty.Text = string.Empty;
                                    txtPopupQty.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            DisplayMessage("Please check the inventory balance!");
                            txtPopupQty.Text = string.Empty;
                            txtPopupQty.Focus();
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessage("Please check the inventory balance!");
                        txtPopupQty.Text = string.Empty;
                        txtPopupQty.Focus();
                        return;
                    }
                }

                if (_itm.Mi_is_ser1 == -1 || grdAdSearch.Rows.Count > 0 || !((bool)Session["_isserialMaintan"]))
                {
                    StringBuilder _errorserial = new StringBuilder();
                    StringBuilder _pickedserial = new StringBuilder();

                    List<ReptPickSerials> oSelectedSerialList = new List<ReptPickSerials>();

                    foreach (GridViewRow item in grdAdSearch.Rows)
                    {
                        CheckBox selectchk = item.FindControl("selectchk") as CheckBox;
                        if (selectchk.Checked)
                        {
                            ReptPickSerials oSelectedSerail = new ReptPickSerials();
                            Label lblTus_ser_id = item.FindControl("lblTus_ser_id") as Label;
                            Label lblTus_bin = item.FindControl("lblTus_bin") as Label;
                            Label lblTus_ser_1 = item.FindControl("lblTus_ser_1") as Label;
                            Label lblTus_itm_stus = item.FindControl("lblTus_itm_stus") as Label;
                            Label lbltus_unit_cost = item.FindControl("lbltus_unit_cost") as Label;
                            decimal _price = 0;
                            if (lblTus_itm_stus.Text == SelectedItemStatus)
                            {
                                oSelectedSerail.Tus_ser_id = (Int32)Convert.ToDecimal(lblTus_ser_id.Text);
                                oSelectedSerail.Tus_bin = lblTus_bin.Text.Trim();
                                oSelectedSerail.Tus_itm_cd = selectedItem;
                                oSelectedSerail.Tus_ser_1 = lblTus_ser_1.Text;
                                oSelectedSerail.Tus_unit_cost = decimal.TryParse(lbltus_unit_cost.Text, out _price) ? Convert.ToDecimal(lbltus_unit_cost.Text) : 0;
                                oSelectedSerail.Tus_unit_price = decimal.TryParse(lbltus_unit_cost.Text, out _price) ? Convert.ToDecimal(lbltus_unit_cost.Text) : 0;
                                oSelectedSerialList.Add(oSelectedSerail);
                            }
                            else
                            {
                                DisplayMessage("Please select items with status : " + SelectedItemStatus);
                                return;
                            }
                        }
                    }

                    decimal _appQty = Convert.ToDecimal(Session["SelectedAppveQty"].ToString());
                    decimal _scaned = Convert.ToDecimal(Session["SelectedPickedQty"].ToString());

                    Int32 num_of_checked_itms = oSelectedSerialList.Count;
                    if (_appQty < num_of_checked_itms + _scaned)
                    {
                        if (!_derectOut)
                        {
                            string SSMsg = "You cannot exceed the approved quantity. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.";
                            DisplayMessage(SSMsg);
                            mpPickSerial.Show();
                            return;
                        }
                    }


                    foreach (ReptPickSerials gvr in oSelectedSerialList)
                    {
                        Int32 serID = (Int32)Convert.ToDecimal(gvr.Tus_ser_id);
                        string binCode = gvr.Tus_bin;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, gvr.Tus_itm_cd, serID);
                        if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                        {
                            if (_itm.Mi_is_ser1 == 1)
                            {
                                _pickedserial.Append(gvr.Tus_ser_1 + "/");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(_pickedserial.ToString()))
                    {
                        DisplayMessage("Selected Serial " + _pickedserial.ToString() + " is already picked by another process. Please check your inventory");
                        return;
                    }
                    foreach (ReptPickSerials gvr in oSelectedSerialList)
                    {
                        Int32 serID = Convert.ToInt32(gvr.Tus_ser_id);
                        string binCode = gvr.Tus_bin;

                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, gvr.Tus_itm_cd, serID);

                        if (_reptPickSerial_ == null || _reptPickSerial_.Tus_com == null)
                        {
                            if (_itm.Mi_is_ser1 == 0)
                            {
                                _reptPickSerial_ = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), selectedItem, SelectedItemStatus, 1)[0];
                                serID = _reptPickSerial_.Tus_ser_id;
                            }
                            if (_itm.Mi_is_ser1 == 1)
                            {
                                DisplayMessage("Selected serial " + gvr.Tus_ser_1 + " already picked by another process");
                                continue;
                            }
                        }

                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), gvr.Tus_itm_cd, serID, -1);
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        //_reptPickSerial_.Tus_doc_no = SelectedDoc;
                        _reptPickSerial_.Tus_base_doc_no = SelectedDoc;
                        _reptPickSerial_.Tus_base_itm_line = (Int32)Convert.ToDecimal(SelectedItemLine);
                        _reptPickSerial_.Tus_itm_desc = _itm.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                        _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(gvr.Tus_ser_id);
                        _reptPickSerial_.Tus_job_no = SelectedJobNum;
                        _reptPickSerial_.Tus_pgs_prefix = selectedItem;
                        _reptPickSerial_.Tus_job_line = (Int32)Convert.ToDecimal(SelectedJobLine);
                        if (SelectedResQty > 0)
                        {
                            _reptPickSerial_.Tus_resqty = _reptPickSerial_.Tus_qty;
                        }

                        if (serID > 0)
                        {
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        }

                    }

                    if (_itm.Mi_is_ser1 == -1 || !((bool)Session["_isserialMaintan"]))
                    {
                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_base_doc_no = SelectedDoc;
                        _reptPickSerial_.Tus_base_itm_line = (Int32)Convert.ToDecimal(SelectedItemLine);
                        _reptPickSerial_.Tus_itm_desc = _itm.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                        _reptPickSerial_.Tus_bin = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        _reptPickSerial_.Tus_itm_cd = selectedItem;
                        _reptPickSerial_.Tus_itm_stus = SelectedItemStatus;
                        _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtSearchbywordA.Text.ToString());
                        _reptPickSerial_.Tus_ser_1 = "N/A";
                        _reptPickSerial_.Tus_ser_2 = "N/A";
                        _reptPickSerial_.Tus_ser_3 = "N/A";
                        _reptPickSerial_.Tus_ser_4 = "N/A";
                        _reptPickSerial_.Tus_ser_id = 0;
                        _reptPickSerial_.Tus_serial_id = "0";
                        _reptPickSerial_.Tus_job_no = JobNo;
                        _reptPickSerial_.Tus_pgs_prefix = selectedItem;
                        _reptPickSerial_.Tus_job_line = (Int32)Convert.ToDecimal(SelectedJobLine);
                        _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                        // _reptPickSerial_.Tus_unit_cost = 0;
                        // _reptPickSerial_.Tus_unit_price = 0;
                        // _reptPickSerial_.Tus_unit_price = 0;
                        if (SelectedResQty > 0)
                        {
                            _reptPickSerial_.Tus_resqty = _reptPickSerial_.Tus_qty;
                        }
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);


                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();

                        ReptPickItems _reptitm = new ReptPickItems();
                        _reptitm.Tui_usrseq_no = UserSeqNo;
                        _reptitm.Tui_req_itm_qty = _reptPickSerial_.Tus_qty;
                        _reptitm.Tui_req_itm_cd = _reptPickSerial_.Tus_itm_cd;
                        _reptitm.Tui_req_itm_stus = _reptPickSerial_.Tus_itm_stus;
                        _reptitm.Tui_pic_itm_cd = _reptPickSerial_.Tus_itm_cd;
                        _reptitm.Tui_pic_itm_stus = _reptPickSerial_.Tus_itm_stus; ;
                        _reptitm.Tui_pic_itm_qty = _reptPickSerial_.Tus_qty;
                        _saveonly.Add(_reptitm);

                        affected_rows = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    }



                    BindPickSerials(UserSeqNo);

                    ////if (SelectedSerialList != null)
                    ////    if (SelectedSerialList.Count > 0)
                    ////        if (Convert.ToString(ddlType.SelectedValue) == "INTR")
                    ////            ScanItemList.Where(n => n.Itri_note == SelectedDoc && n.Itri_itm_cd == selectedItem).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == SelectedDoc && y.Tus_itm_cd == selectedItem).Sum(z => z.Tus_qty));
                    ////        else
                    ////            ScanItemList.Where(n => n.Itri_note == SelectedDoc && n.Itri_itm_cd == selectedItem && n.Itri_itm_stus == SelectedItemStatus).ToList().ForEach(x => x.Itri_qty = SelectedSerialList.Where(y => y.Tus_base_doc_no == SelectedDoc && y.Tus_itm_cd == selectedItem && y.Tus_itm_stus == SelectedItemStatus).Sum(z => z.Tus_qty));
                    ////gvSerial.DataSource = null;
                    ////gvSerial.DataSource = ScanItemList;
                    ////gvSerial.DataBind();

                    BindMrnDetail(SelectedDoc);
                    mpPickSerial.Hide();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                mpPickSerial.Show();
            }
        }

        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpPickSerial.Show();
            grdAdSearch.PageIndex = e.NewPageIndex;
            List<ReptPickSerials> dt = (List<ReptPickSerials>)Session["serial_list"];
            grdAdSearch.AutoGenerateColumns = false;
            grdAdSearch.DataSource = dt;
            grdAdSearch.DataBind();
        }

        public void BindUCtrlDDLData3(DataTable _dataSource)
        {
            this.ddlSearchbykeyA.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyA.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyA.SelectedIndex = 0;
        }

        private void FilerSerialSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbywordA.Text.Trim()))
                {
                    List<ReptPickSerials> _lst = new List<ReptPickSerials>();

                    if (ddlSearchbykeyA.SelectedIndex == 0)
                    {
                        _lst = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["SelectedItem"].ToString().Trim(), null, txtSearchbywordA.Text.Trim(), null);
                    }
                    else
                    {
                        _lst = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["SelectedItem"].ToString().Trim(), null, null, txtSearchbywordA.Text.Trim());
                    }

                    if (_lst != null)
                    {
                        if (_lst.Count > 0)
                        {
                            if (_lst.Count == 1)
                            {
                                serial_list = new List<ReptPickSerials>();
                                serial_list.Clear();
                                var _dup = serial_list.Where(x => x.Tus_ser_id == _lst[0].Tus_ser_id).ToList();
                                if (_dup == null || _dup.Count <= 0)
                                    serial_list.Add(_lst[0]);
                                grdAdSearch.DataSource = null;
                                grdAdSearch.DataSource = serial_list;
                                grdAdSearch.DataBind();
                                //CheckAlreadySelectedSerial();
                            }
                            else
                            {
                                grdAdSearch.DataSource = null;
                                grdAdSearch.DataSource = _lst;
                                grdAdSearch.DataBind();
                            }
                        }
                        else
                        {
                            grdAdSearch.DataSource = null;
                            grdAdSearch.DataSource = _lst;
                            grdAdSearch.DataBind();
                        }
                    }
                }
                else
                {
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["SelectedItem"].ToString().Trim(), "-1", string.Empty);
                    serial_list = setItemStatus2(serial_list);
                    grdAdSearch.DataSource = serial_list;
                    grdAdSearch.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

            mpPickSerial.Show();
        }

        #endregion

        protected void btnItm_AddSerial_Click(object sender, EventArgs e)
        {
            if (Session["GlbDefaultBin"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            string lblPopupBinCode = Session["GlbDefaultBin"].ToString();

            grdAdSearch.Visible = true;
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;

            Label lblitri_itm_cd1 = dr.FindControl("lblitri_itm_cd1") as Label;
            Label lblitri_itm_stus = dr.FindControl("lblitri_itm_stus") as Label;
            Label lblitri_qty = dr.FindControl("lblitri_qty") as Label;
            Label lblitri_note = dr.FindControl("lblitri_note") as Label;
            Label lblitri_line_no = dr.FindControl("lblitri_line_no") as Label;

            Label lblitri_job_no = dr.FindControl("lblitri_job_no") as Label;
            Label lblitri_job_line = dr.FindControl("lblitri_job_line") as Label;

            Label lblitri_app_qty = dr.FindControl("lblitri_app_qty") as Label;
            Label lblitri_res_qty = dr.FindControl("lblitri_res_qty") as Label;
            SelectedResQty = Convert.ToInt32(lblitri_res_qty.Text);
            Session["SelectedItem"] = lblitri_itm_cd1.Text.Trim();
            Session["SelectedDoc"] = lblitri_note.Text.Trim();
            Session["SelectedItemStatus"] = lblitri_itm_stus.Text.Trim();
            Session["SelectedItemLine"] = lblitri_line_no.Text.Trim();
            Session["SelectedJobNum"] = lblitri_job_no.Text.Trim();
            Session["SelectedJobLine"] = lblitri_job_line.Text.Trim();
            Session["SelectedAppveQty"] = lblitri_app_qty.Text.Trim();
            Session["SelectedPickedQty"] = lblitri_qty.Text.Trim();

            if (lblitri_note.Text.Contains("GRAN"))
            {
                DisplayMessageJS("You do not need to pick serials for the GRAN.");
                return;
            }

            if (Convert.ToDecimal(lblitri_app_qty.Text) == 0)
            {
                DisplayMessage("Approved Qty is 0", 2);
                return;
            }


            if (string.IsNullOrEmpty(lblitri_itm_cd1.Text))
            {
                DisplayMessage("Please select the item code");
                return;
            }
            if (string.IsNullOrEmpty(lblPopupBinCode))
            {
                DisplayMessage("Please select the bin code.");
                return;
            }
            if (string.IsNullOrEmpty(lblPopupQty.Text))
            {
                DisplayMessage("Please select the requested qty.");
                return;
            }
            if (string.IsNullOrEmpty(lblApprovedQty.Text))
            {
                DisplayMessage("Please select the requested qty.");
                return;
            }
            if (string.IsNullOrEmpty(lblitri_itm_stus.Text))
            {
                DisplayMessage("Please select the item status.");
                return;
            }

            txtPopupQty.Text = "";

            MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitri_itm_cd1.Text);
            List<MasterCompanyItemStatus> _tempRevertlist = CHNLSVC.Inventory.GetAllCompanyStatuslist(Session["UserCompanyCode"].ToString());
            List<MasterCompanyItemStatus> _list = null;

            if (!((bool)Session["_isserialMaintan"]))
            {
                msitem.Mi_is_ser1 = -1;
            }

            if (msitem.Mi_is_ser1 == -1)
            {
                txtPopupQty.Text = Convert.ToDecimal(lblPopupQty.Text).ToString();
                ddlSearchbykeyA.Enabled = false;
                lblPickSerText.Text = "Add Qty";
                lbtnSearchA.Visible = false;
            }
            else
            {
                ddlSearchbykeyA.Enabled = true;
                lblPickSerText.Text = "Search by word";
                lbtnSearchA.Visible = true;
            }

            string hdnInvoiceNo = lblitri_note.Text;
            string hdnInvoiceLineNo = lblitri_line_no.Text;

            _list = _tempRevertlist;

            serial_list = new List<ReptPickSerials>();
            bool _isBondLoc = false;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_mstLoc != null)
            {
                if (_mstLoc.Ml_cate_1 == "DFS")
                {
                    _isBondLoc = true;
                }
            }
            if (ucOutScan.isbatchserial && _batchBaseOut)
            {

            }
            else
            {
                if (!_isBondLoc)
                {
                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblitri_itm_cd1.Text, "-1", string.Empty);
                    serial_list = setItemStatus2(serial_list);
                }
                else
                {
                    serial_list = CHNLSVC.Inventory.SearchSerialForJobItemAOD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(),
                        lblitri_itm_cd1.Text, lblitri_itm_stus.Text, lblitri_job_no.Text, Convert.ToInt16(lblitri_line_no.Text));
                    serial_list = setItemStatus2(serial_list);
                }
            }
            if (msitem.Mi_is_ser1 != 1)
            {
                if (msitem.Mi_is_ser1 != -1)
                {
                    decimal ApprovedQty = Convert.ToDecimal(lblitri_app_qty.Text);
                    decimal ScanQty = Convert.ToDecimal(lblitri_qty.Text);

                    decimal _selectingRows = Convert.ToInt32(ApprovedQty - ScanQty);
                    if (_selectingRows > serial_list.Count)
                    {
                        string msg = "Available balance qty is " + serial_list.Count + ". Do you want to proceed?";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "balanceConfimation('" + msg + "');", true);
                    }
                }
            }

            if (msitem.Mi_is_ser1 == 0)
            {
                Int32 countSelect = 0; decimal qty = Convert.ToDecimal(lblitri_app_qty.Text);
                foreach (var item in serial_list)
                {
                    countSelect++;
                    if (countSelect <= qty)
                    {
                        item.Tus_isSelect = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //Session["PopupQty"] = _invoiceQty - _doQty;
            addSearchTypesPickSerial();
            Session["serial_list"] = serial_list;
            grdAdSearch.DataSource = serial_list;
            lblPopupQty.Text = (Convert.ToDecimal(lblitri_app_qty.Text) - Convert.ToDecimal(lblitri_qty.Text)).ToString();
            grdAdSearch.DataBind();
            mpPickSerial.Show();

            if (!((bool)Session["_isserialMaintan"]))
            {
                grdAdSearch.DataSource = new int[] { };
                grdAdSearch.DataBind();
                grdAdSearch.Visible = false;
                Div4.Style.Add("height", "20px");
            }
        }

        private void addSearchTypesPickSerial()
        {
            ddlSearchbykeyA.Items.Clear();
            ddlSearchbykeyA.Items.Add(new ListItem("Serial1"));
            ddlSearchbykeyA.Items.Add(new ListItem("Serial2"));
        }

        protected void chkDirectOut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ucOutScan._derectOut = false;
                _derectOut = false;
                //ddlType.SelectedIndex = 0;
                //ddlType_SelectedIndexChanged(null,null);
                if (chkDirectOut.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10001))
                    {
                        DisplayMessage("Sorry, You have no permission to direct out!. ( Advice: Required permission code :10001)");
                        CheckBox _chk = (CheckBox)sender;
                        _chk.Checked = false;
                        return;
                    }
                }

                if (chkDirectOut.Checked)
                {
                    if (LoadDirectType() == false)
                    {
                        DisplayMessage("Direct issuing facility is not available to (" + Session["UserCompanyCode"].ToString() + ")");
                        chkDirectOut.Checked = false;
                        return;
                    }
                    _derectOut = true;
                    ucOutScan._derectOut = true;
                    txtDispatchRequried.Enabled = true;
                    btnSearch_RecLocation.Enabled = true;
                    ddlRecCompany.Enabled = true;
                    BindReceiveCompany();
                    ddlRecCompany.Text = Session["UserCompanyCode"].ToString();
                    gvPending.Enabled = false;
                    cmbDirectScan.Enabled = true;
                    cmbDirType.Enabled = true;
                    LoadUnFinishedDirectDocument();

                    Session["AOD_DIRECT"] = true;

                    if (UserSeqNo < 0)
                    {
                        UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), COM_OUT, 1, Session["UserCompanyCode"].ToString());
                        Session["UserSeqNo"] = UserSeqNo;
                        ucOutScan.userSeqNo = UserSeqNo.ToString();

                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = COM_OUT;
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = false;
                        RPH.Tuh_ischek_reqqty = false;
                        RPH.Tuh_ischek_simitm = false;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = UserSeqNo;
                        RPH.Tuh_direct = false;
                        RPH.Tuh_rec_com = Convert.ToString(ddlRecCompany.SelectedValue);
                        RPH.Tuh_rec_loc = Convert.ToString(txtDispatchRequried.Text.ToUpper());
                        RPH.Tuh_isdirect = true;
                        RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        RPH.Tuh_dir_type = Convert.ToString(cmbDirType.SelectedValue);
                        RPH.Tuh_doc_no = UserSeqNo.ToString();
                        if (ddlType.SelectedValue == "MRNA")
                        {
                            RPH.Tuh_is_take_res = true;
                        }
                        if (ddlType.SelectedValue == "MRNS")
                        {
                            RPH.Tuh_is_take_res = true;
                        }
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                }
                else
                {
                    txtDispatchRequried.Enabled = false;
                    btnSearch_RecLocation.Enabled = false;
                    if (ddlType.SelectedValue == "EX" || ddlType.SelectedValue == "RE" || ddlType.SelectedValue == "BOI" || ddlType.SelectedValue == "EXP")
                    {
                        btnSearch_RecLocation.Enabled = true;
                    }
                    ddlRecCompany.Enabled = false;
                    gvPending.Enabled = true;
                    cmbDirectScan.DataSource = null;
                    cmbDirType.Enabled = false;
                    cmbDirectScan.Enabled = false;

                    // Session["AOD_DIRECT"] = false;
                    Session["AOD_DIRECT"] = true;

                }
                SelectedSerialList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>();
                _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>();
                gvPending.AutoGenerateColumns = false;
                gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                return;
            }
            finally
            {
                string seq = ViewState["userSeqNo"].ToString();
                BindReceiveCompany();
                BindMRNListGridData();
                BindPickSerials(0);
                BindMrnDetail(string.Empty);
                ddlRecCompany.Text = Session["UserCompanyCode"].ToString();
                RequestNo = string.Empty;
                JobNo = string.Empty;
                //UserSeqNo = -100;
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void txtDispatchRequried_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                txtDispatchRequried.Enabled = true;
            }
        }

        protected void btntus_itm_stus_Click(object sender, EventArgs e)
        {

        }

        protected void btnsearchComClose_Click1(object sender, EventArgs e)
        {
            Session["SHWSECH"] = null;
        }

        protected void btnclseitmsrtu_Click(object sender, EventArgs e)
        {

        }

        protected void gvItemStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                GridViewRow row = gvItemStatus.Rows[int.Parse(e.CommandArgument.ToString())];

                GridViewRow drSelectedRow = (GridViewRow)Session["SelectedRow"];
                if (drSelectedRow == null)
                {
                    DisplayMessage("Error occurred");
                    return;
                }

                Label lblitri_note = (Label)drSelectedRow.FindControl("lblitri_note");
                Label lblitri_line_no = (Label)drSelectedRow.FindControl("lblitri_line_no");
                Label lblitri_itm_cd1 = (Label)drSelectedRow.FindControl("lblitri_itm_cd1");

                Label lblINL_ITM_STUS = (Label)row.FindControl("lblINL_ITM_STUS");


                string invoiceNumber = lblitri_note.Text.Trim();
                Int32 lineNumber = (Int32)Convert.ToDecimal(lblitri_line_no.Text);
                string _old_item = lblitri_itm_cd1.Text.Trim();


                String newStatus = lblINL_ITM_STUS.Text.Trim();

                String err;
                Int32 result = CHNLSVC.Inventory.UPDATE_ITM_STUS(invoiceNumber, lineNumber, _old_item, newStatus, out   err);
                if (result > 0)
                {
                    DisplayMessage("Successfully updated");
                    mpChangeItemStatus.Hide();
                }
                else
                {
                    DisplayMessage("Err : " + err);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void Btnitri_itm_stus_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkChangeStatus.Checked)
                {
                    GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                    Label lblitri_itm_cd1 = (Label)dr.FindControl("lblitri_itm_cd1");
                    Label lblitri_line_no = (Label)dr.FindControl("lblitri_line_no");
                    List<InventoryRequestItem> _invReqItmLst = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(txtRequest.Text.Trim());
                    var _reqVal = _invReqItmLst.Where(c => c.Itri_itm_cd == lblitri_itm_cd1.Text.Trim()).FirstOrDefault();
                    bool b16062 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16062);
                    if (!b16062)
                    {
                        DispMsg("Sorry, You have no permission. ( Advice: Required permission code : 16062) !!!", "N");
                        return;
                    }
                    Int32 _tmpInt = 0;
                    Int32 _stusChange = Int32.TryParse(_reqVal.ITRI_ITM_COND, out _tmpInt) ? Convert.ToInt32(_reqVal.ITRI_ITM_COND) : 0;
                    if (_stusChange != 0)
                    {
                        DispMsg("Item status cannot change !"); return;
                    }
                    string _old_item = lblitri_itm_cd1.Text.Trim();
                    DataTable dtStatus = CHNLSVC.Inventory.GET_ITMSTATUS_BY_LOC_ITM(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _old_item);
                    if (dtStatus != null && dtStatus.Rows.Count > 0)
                    {
                        Session["SelectedRow"] = dr;
                        gvItemStatus.DataSource = dtStatus;
                        gvItemStatus.DataBind();

                        mpChangeItemStatus.Show();
                    }
                }
                else
                {
                    DisplayMessage("Please select  the change status check box for status change ! ");
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnSearch_RecLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Location";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void ddlRecCompany_DataBound(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl != null)
            {
                foreach (ListItem li in ddl.Items)
                {
                    li.Attributes["title"] = li.Text;
                }
            }
        }

        protected void autoSelect_Click(object sender, EventArgs e)
        {
            if (Session["SelectedAppveQty"] != null && Session["SelectedPickedQty"] != null && grdAdSearch.Rows.Count > 0)
            {
                Decimal val = Convert.ToDecimal(Session["SelectedAppveQty"].ToString()) - Convert.ToDecimal(Session["SelectedPickedQty"].ToString());

                if (grdAdSearch.Rows.Count < val)
                {
                    for (int i = 0; i < grdAdSearch.Rows.Count; i++)
                    {
                        GridViewRow dr = grdAdSearch.Rows[i];
                        CheckBox selectchk = dr.FindControl("selectchk") as CheckBox;
                        selectchk.Checked = true;
                    }

                    mpPickSerial.Show();
                }
            }
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
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

        private List<ReptPickSerials> setItemStatus2(List<ReptPickSerials> oitems)
        {
            if (Session["ItemStatus"] != null)
            {
                List<MasterItemStatus> oStatus = (List<MasterItemStatus>)Session["ItemStatus"];

                foreach (ReptPickSerials item in oitems)
                {
                    item.Tus_itm_stus_Desc = oStatus.Find(x => x.Mis_cd == item.Tus_itm_stus).Mis_desc;
                }
            }
            return oitems;
        }

        private void WriteErrLog(string err)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
            {
                _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + "  | " + err);
            }
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void ddlDeliver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDeliver.Text == "OWN")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "COURIER")
            {
                lblVehicle.Text = "POD Slip No";
            }
            else if (ddlDeliver.Text == "CUSTOMER")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                lblVehicle.Text = "Fleet Job";
            }
        }

        private void LoadDeliveroption()
        {
            DataTable _result = CHNLSVC.CommonSearch.GET_DeliverByOption(Session["UserCompanyCode"].ToString());
            ddlDeliver.DataSource = _result;
            ddlDeliver.DataValueField = "rtm_tp";
            ddlDeliver.DataTextField = "rtm_tp";
            ddlDeliver.DataBind();
            //ddlDeliver.Items.Insert(2, "--Select--");
            // ddlDeliver.SelectedItem.Text = "--Select--";

            if (ddlDeliver.Text == "OWN")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "COURIER")
            {
                lblVehicle.Text = "POD Slip No";
            }
            else if (ddlDeliver.Text == "CUSTOMER")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                lblVehicle.Text = "Fleet Job";
            }
        }

        private bool validateVehicle(string VNO)
        {

            if (ddlDeliver.Text == "OWN")
            {
                DataTable _Vehicle = CHNLSVC.CommonSearch.GET_Vehicle(Session["UserCompanyCode"].ToString(), VNO);
                if ((_Vehicle != null) && (_Vehicle.Rows.Count > 0))
                {
                    return true;
                }
                return false;
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                DataTable _Vehicle = CHNLSVC.CommonSearch.GET_TRANSPORT_JOB(Session["UserCompanyCode"].ToString(), VNO);
                if ((_Vehicle != null) && (_Vehicle.Rows.Count > 0))
                {
                    return true;
                }
                return false;
            }
            return true;
        }


        private void check_itm_grn(List<InventoryRequestItem> _itm)
        {
            List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
            _select = CHNLSVC.Inventory.Check_bl_GRN(_itm, Session["_selectJobNo"].ToString(), Session["UserDefLoca"].ToString());
            gvItems.DataSource = setItemStatus1(_select);
            gvItems.DataBind();
            LoadMrnData(_select);
            Session["ScanItemListNew"] = _select;
        }

        protected void lbtngrdDOItemstEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;

                Label lblitri_itm_cd1 = dr.FindControl("lblitri_itm_cd1") as Label;
                Label lblitri_itm_stus = dr.FindControl("lblitri_itm_stus") as Label;
                Label lblitri_qty = dr.FindControl("lblitri_qty") as Label;
                Label lblitri_note = dr.FindControl("lblitri_note") as Label;
                Label lblitri_line_no = dr.FindControl("lblitri_line_no") as Label;
                Label lblitri_res_qty = dr.FindControl("lblitri_res_qty") as Label;

                MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitri_itm_cd1.Text);
                if (msitem != null)
                {
                    bool _isserialmaintan = (bool)Session["_isserialMaintan"];
                    if (_isserialmaintan == true)
                    {
                        if (msitem.Mi_is_ser1 == 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot change the serialized item pick qty !!!')", true);
                            return;
                        }
                    }
                }
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    gvItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
                    _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;

                    gvItems.DataSource = setItemStatus1(_select);
                    gvItems.DataBind();
                    LoadMrnData(_select);

                }


            }
            catch (Exception ex)
            {

            }
        }


        protected void lbtngrdDOItemstUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                //
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _jobNo = (row.FindControl("lblitri_job_no") as Label).Text;
                    Int32 _jobLine = Convert.ToInt32((row.FindControl("lblitri_job_line") as Label).Text);
                    decimal _appqty = Convert.ToDecimal((row.FindControl("lblitri_bqty") as Label).Text);
                    decimal _qty = Convert.ToDecimal((row.FindControl("txtlblitri_qty") as TextBox).Text);
                    TextBox txtQtyEdit = row.FindControl("txtQtyEdit") as TextBox;
                    string _lineno = (row.FindControl("lblitri_line_no") as Label).Text;
                    string _ItemCode = (row.FindControl("lblitri_itm_cd1") as Label).Text;
                    string _status = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    Label lblitri_res_qty = row.FindControl("lblitri_res_qty") as Label;
                    List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
                    decimal ResQty = Convert.ToDecimal(lblitri_res_qty.Text);
                    DataTable _invLoc = new DataTable();
                    _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;

                    //Add by lakshan
                    MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    bool _isPdaSend = false;
                    if (_mstLocation != null)
                    {
                        if (!_mstLocation.Ml_is_serial)
                        {
                            int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, Session["UserCompanyCode"].ToString(), txtRequest.Text, 0);
                            if (_usrSeq != -1)
                            {
                                ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue);
                                if (_ReptPickHeader != null)
                                {
                                    if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                    {
                                        _isPdaSend = true;
                                    }
                                }
                            }
                        }
                    }
                    //
                    if (ResQty == 0)
                    {
                        _invLoc = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _ItemCode.ToUpper().Trim(), null);
                    }
                    else
                    {
                        _invLoc = CHNLSVC.Inventory.GetItemInventoryBalanceStatus_RES(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _ItemCode.ToUpper().Trim(), null);
                    }

                    if (_appqty < _qty)
                    {
                        DisplayMessage("You can not pick more than App qty");
                        gvItems.EditIndex = -1;
                        gvItems.DataSource = setItemStatus1(_select); ;
                        gvItems.DataBind();
                        LoadMrnData(_select);
                        Session["ScanItemListNew"] = _select;
                    }
                    else if (_isPdaSend)
                    {
                        DisplayMessage("Serials are already picked from PDA !");
                        gvItems.EditIndex = -1;
                        gvItems.DataSource = setItemStatus1(_select); ;
                        gvItems.DataBind();
                        LoadMrnData(_select);
                        Session["ScanItemListNew"] = _select;
                    }
                    else if (_qty == 0)
                    {
                        DisplayMessage("Please enter pick qty");
                        gvItems.EditIndex = -1;
                        gvItems.DataSource = setItemStatus1(_select); ;
                        gvItems.DataBind();
                        LoadMrnData(_select);
                        Session["ScanItemListNew"] = _select;
                    }
                    else if (_invLoc == null || _invLoc.Rows.Count <= 0)
                    {
                        DisplayMessage("No stock balance available!.");
                        gvItems.EditIndex = -1;
                        gvItems.DataSource = setItemStatus1(_select); ;
                        gvItems.DataBind();
                        LoadMrnData(_select);
                        Session["ScanItemListNew"] = _select;
                    }
                    else
                    {
                        gvItems.EditIndex = -1;
                        if (_select.Count > 0)
                        {
                            var _selectItm = _select.Single(x => x.Itri_line_no == Convert.ToInt32(_lineno) && x.Itri_itm_cd == _ItemCode);
                            if (_selectItm != null)
                            {
                                _selectItm.Itri_qty = _qty;
                                _selectItm.Itri_bqty = _selectItm.Itri_bqty - _qty;
                            }
                            MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _ItemCode);
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = UserSeqNo;
                            _reptPickSerial_.Tus_seq_no = UserSeqNo;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = txtRequest.Text;
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_lineno);
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                            _reptPickSerial_.Tus_itm_cd = _ItemCode;
                            _reptPickSerial_.Tus_itm_stus = _status;
                            _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_lineno);
                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_3 = "DFS";
                            _reptPickSerial_.Tus_ser_4 = "DFS";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_serial_id = "0";
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_job_no = _jobNo;
                            //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                            _reptPickSerial_.Tus_job_line = _jobLine;
                            //_reptPickSerial_.Tus_exist_supp = suppler;
                            //_reptPickSerial_.Tus_orig_supp = suppler;
                            if (Convert.ToInt32(lblitri_res_qty.Text) > 0)
                            {
                                _reptPickSerial_.Tus_resqty = _reptPickSerial_.Tus_qty;
                            }
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        }
                        gvItems.DataSource = setItemStatus1(_select); ;
                        gvItems.DataBind();
                        LoadMrnData(_select);
                        Session["ScanItemListNew"] = _select;
                        BindPickSerials(UserSeqNo);
                    }



                }

            }
            catch (Exception ex)
            {

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

            }
        }
        private void SaveData()
        {
            int _chkQty = 0;
            foreach (GridViewRow item in gvPending.Rows)
            {
                CheckBox pen_Select = item.FindControl("pen_Select") as CheckBox;
                if (pen_Select.Checked)
                {
                    _chkQty = _chkQty + 1;
                }
                if (_chkQty > 1)
                {
                    break;
                }
            }
            if (_chkQty > 1)
            {
                DispMsg("Multiple documents selected !"); return;
            }
            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

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
            //Add by lakshan
            List<InventoryRequestItem> _tmpReqItm = new List<InventoryRequestItem>();
            _tmpReqItm = Session["ScanItemListNew"] as List<InventoryRequestItem>;
            Int32 _isResQty = 0;
            if (_tmpReqItm != null)
            {
                if (_tmpReqItm.Count > 0)
                {
                    decimal _resQty = _tmpReqItm.Where(c => c.Itri_res_qty > 0).Sum(c => c.Itri_res_qty);
                    decimal _sendQty = _tmpReqItm.Where(c => c.Itri_qty > 0).Sum(c => c.Itri_res_qty);
                    if (_resQty > 0)
                    {
                        _isResQty = 1;
                    }
                }
            }
            #region Add by Lakshan to chk doc already send or not 01 Oct 2016
            _tmpPickHdr = new ReptPickHeader();
            bool _docAva = false;
            _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
            {
                Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                Tuh_doc_no = txtdocname.Text,
                Tuh_direct = false,
                Tuh_doc_tp = ddlType.SelectedValue
            }).FirstOrDefault();
            if (_tmpPickHdr != null)
            {
                List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repSerList != null)
                {
                    if (_repSerList.Count > 0)
                    {
                        _docAva = true;
                    }
                }
                List<ReptPickItems> _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repItmList != null)
                {
                    if (_repItmList.Count > 0)
                    {
                        _docAva = true;
                    }
                }
            }
            if (_docAva)
            {
                string _msg = "Document has already sent to PDA or has alread processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay " + _tmpPickHdr.Tuh_load_bay;
                DisplayMessage(_msg);
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('+_msg+')", true);
                return;
            }
            #endregion
            //
            /*11 Oct 2016 Add by Lakshan */
            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest()
            {
                Itr_req_no = txtdocname.Text,
                Itr_stus = "A",
                Itr_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue
            }).FirstOrDefault();
            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, Session["UserCompanyCode"].ToString(), txtdocname.Text, 0);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, txtdocname.Text);
                if (user_seq_num > 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                    _inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_is_take_res = _isResQty == 1 ? true : false;
                    if (_reqData != null)
                    {
                        _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                    }
                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (val == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
            }
            else
            {
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                _inputReptPickHeader.Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                _inputReptPickHeader.Tuh_direct = false;
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_is_take_res = _isResQty == 1 ? true : false;
                if (_reqData != null)
                {
                    _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                }
                val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                if (Convert.ToInt32(val) == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }

            DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

            if (dtchkitm.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !')", true);
                return;
            }
            else
            {
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
                _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;
                foreach (InventoryRequestItem _itm in _select)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                    _reptitm.Tui_req_itm_qty = _itm.Itri_bqty;
                    _reptitm.Tui_req_itm_cd = _itm.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _itm.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = _itm.Itri_line_no.ToString();
                    // _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Itri_line_no);//Darshana request add by rukshan
                    // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = 0;
                    _saveonly.Add(_reptitm);


                }
                val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document sent to PDA Successfully !')", true);
                MPPDA.Hide();
            }

        }

        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 0, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ViewState["userSeqNo"] = generated_seq;
            ucOutScan.userSeqNo = generated_seq.ToString();
            if (generated_seq > 0)
            {
                return generated_seq;
            }
            else
            {
                return 0;
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

        }
        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpda.Checked == true)
            {
                chkAODoutserials.Checked = false;
                txtdocname.Text = txtRequest.Text;
                MPPDA.Show();
            }
            else
            {
                MPPDA.Hide();
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int printNo = 0;
        //    int value = Int32.TryParse(Session["print"].ToString(), out printNo) ? Convert.ToInt32(Session["print"].ToString()) : 0;
        //    if (value == 1)
        //    {
        //        Session["GlbReportType"] = "SCM1_AODOT";
        //        //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //        //BaseCls.GlbReportHeading = "OUTWARD DOC";
        //        Session["GlbReportName"] = "Outward_Docs_Full.rpt";                

        //        //PrintPDF();
        //        string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //       // Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);

        //    }
        //    else if (value == 2)
        //    {
        //        Session["GlbReportType"] = "";
        //        //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //        Session["GlbReportName"] = "serial_items.rpt";
        //        //BaseCls.GlbReportHeading = "Item Serials Report";
        //        Session["GlbReportName"] = "serial_items.rpt";
        //      //  Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
        //        string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //    }
        //}

        //public void PrintPDF()
        //{

        //    //clsInventory obj = new clsInventory();
        //    ////ReportDocument Rel = new ReportDocument();
        //    //obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
        //    //string path = Server.MapPath("../../Reports/Inventory/" + Session["GlbReportName"].ToString());
        //    //obj._outdocfull.Load(path);
        //    //BinaryReader stream = new BinaryReader(obj._outdocfull.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //    //Response.ClearContent();
        //    //Response.ClearHeaders();
        //    //Response.ContentType = "application/pdf";
        //    //Response.AddHeader("content-disposition", "attachment; filename=" + "abc.pdf");
        //    //Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
        //    //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //    //Response.Flush();
        //    //Response.Close(); 


        //    //string _reportName = Session["GlbReportName"].ToString();
        //    //clsInventory obj = new clsInventory();
        //    //obj._outdocfull.Load(Path.Combine(Server.MapPath("~/Reports"), _reportName));

        //    //obj._outdocfull.Load(Server.MapPath("~/CrystalPersonInfo.rpt")); // path of report 
        //    ////obj._outdocfull.SetDataSource(datatable); // binding datatable
        //    ////CrystalReportViewer1.ReportSource = crystalReport;

        //    //crystalReport.ExportToHttpResponse
        //    //(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

        //    //Response.Buffer = false;
        //    //Response.ClearContent();
        //    //Response.ClearHeaders();
        //    ////try
        //    ////{
        //    //    this.Response.Clear();
        //    //    this.Response.ContentType = "application/pdf";
        //    //    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
        //    //    return File.Open(obj._outdocfull.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "application/pdf");
        //    //    obj._outdocfull.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    //    Process p = new Process();
        //    //    p.StartInfo = new ProcessStartInfo(Path.Combine(Server.MapPath("~/Reports"), _reportName));
        //    //    p.Start();

        //    //    Stream stream = _rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    //    stream.Seek(0, SeekOrigin.Begin);
        //    //    return File(stream, "application/pdf", "EverestList.pdf");
        //    ////}
        //    ////catch (Exception ex)
        //    ////{
        //    ////    //throw;
        //    ////}
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
            int value = (int)Session["print"];
            if (value == 1)
            {
                Session["GlbReportType"] = "SCM1_AODOT";
                //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                //BaseCls.GlbReportHeading = "OUTWARD DOC";
                Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
                //PrintPDF(targetFileName);
                //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                PopupConfBox.Hide();
            }
            else if (value == 2)
            {
                Session["GlbReportType"] = "";
                Session["GlbReportName"] = "serial_items.rpt";
                Session["GlbReportName"] = "serial_items.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDFSer(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                PopupConfBox.Hide();

                //Session["GlbReportType"] = "";
                //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                //Session["GlbReportName"] = "serial_items.rpt";
                //BaseCls.GlbReportHeading = "Item Serials Report";
                //Session["GlbReportName"] = "serial_items.rpt";
                ////  Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

            }
        }

        //Commented By dilshan on 05-03-2018********************************************
        public void _print()
        {
            int value = (int)Session["print"];
            if (value == 1)
            {
                try
                {
                    string _repname;
                    string _papersize;
                    DataTable _InProduct = new DataTable();

                    Session["GlbReportType"] = "SCM1_AODOT";
                    //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    //BaseCls.GlbReportHeading = "OUTWARD DOC";
                    Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                    Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                    //Wimal @ 11/03/2017
                    CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), "N/A", Session["GlbReportType"].ToString(), out  _repname, out  BaseCls.ShowComName, out _papersize);
                    Session["GlbReportName"] = _repname;

                    _InProduct = CHNLSVC.General.GetItemInProduction(Session["documntNo"].ToString(), Session["UserCompanyCode"].ToString(), "O");
                    if (_InProduct != null)
                    {
                        if (_InProduct.Rows.Count > 0 && Session["UserCompanyCode"].ToString() == "ABE")
                        {
                            Session["GlbReportName"] = "Outward_Docs_Full_ABE_OUTP.rpt";
                        }
                    }
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDF(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
                    //PrintPDF(targetFileName);
                    //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
                }
            }
            else if (value == 2)
            {
                try
                {
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "serial_items.rpt";
                    Session["GlbReportName"] = "serial_items.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDFSer(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print Serial", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
                }
            }

            if (value == 3)
            {
                try
                {
                    string _repname;
                    string _papersize;
                    Session["GlbReportType"] = "SCM1_AODOT";
                    //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    //BaseCls.GlbReportHeading = "OUTWARD DOC";
                    Session["GlbReportName"] = "Outward_Docs_Full_ARL_EXP_Details.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDF(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
                    //PrintPDF(targetFileName);
                    //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print Expiry", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
                }
            }
        }
        //*******************************************************************

        //Added by dilshan on 05-03-2018********************************************
        //public string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
        //public void _print()
        //{
        //    int value = (int)Session["print"];
        //    if (value == 1)
        //    {
        //        try
        //        {
        //            string _repname;
        //            string _papersize;
        //            DataTable _InProduct = new DataTable();

        //            Session["GlbReportType"] = "SCM1_AODOT";
        //            //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //            //BaseCls.GlbReportHeading = "OUTWARD DOC";
        //            Session["GlbReportName"] = "Outward_Docs_Full.rpt";
        //            Session["GlbReportName"] = "Outward_Docs_Full.rpt";
        //            //Wimal @ 11/03/2017
        //            CHNLSVC.General.CheckReportName(Session["UserCompanyCode"].ToString(), "N/A", Session["GlbReportType"].ToString(), out  _repname, out  BaseCls.ShowComName, out _papersize);
        //            Session["GlbReportName"] = _repname;

        //            _InProduct = CHNLSVC.General.GetItemInProduction(Session["documntNo"].ToString(), Session["UserCompanyCode"].ToString(), "O");
        //            if (_InProduct != null)
        //            {
        //                if (_InProduct.Rows.Count > 0 && Session["UserCompanyCode"].ToString() == "ABE")
        //                {
        //                    Session["GlbReportName"] = "Outward_Docs_Full_ABE_OUTP.rpt";
        //                }
        //            }
        //            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            PrintPDFnew(targetFileName);
        //            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
        //            //PrintPDF(targetFileName);
        //            //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //        }
        //        catch (Exception ex)
        //        {
        //            CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
        //        }
        //    }
        //    else if (value == 2)
        //    {
        //        try
        //        {
        //            //Session["GlbReportType"] = "";
        //            //Session["GlbReportName"] = "serial_items.rpt";
        //            //Session["GlbReportName"] = "serial_items.rpt";
        //            //string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //            //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            //PrintPDFSer(targetFileName);
        //            //string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

        //            //****************************
        //            //clsInventory obj = new clsInventory();
        //            //obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
        //            //ReportDocument rptDoc = (ReportDocument)obj._serialItems;
        //            //DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
        //            //rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //            //rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //            //diskOpts.DiskFileName = targetFileName;
        //            //rptDoc.ExportOptions.DestinationOptions = diskOpts;
        //            //rptDoc.Export();

        //            //rptDoc.Close();
        //            //rptDoc.Dispose();
        //            //***************************
        //            serial_items _serialItems = new serial_items();
        //            ReportDocument rptDoc = new ReportDocument();
        //            DataTable item_serials = new DataTable();
        //            DataTable param = new DataTable();
        //            DataRow dr;
        //            string COM = Session["UserCompanyCode"].ToString();
        //            param.Columns.Add("DocNo", typeof(string));
        //            param.Columns.Add("User", typeof(string));
        //            param.Columns.Add("Location", typeof(string));
        //            param.Columns.Add("Company", typeof(string));
        //            //param.Columns.Add("company_name", typeof(string));
        //            //obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());

        //            dr = param.NewRow();
        //            //dr["DocNo"] = docno;
        //            dr["DocNo"] = Session["documntNo"].ToString();
        //            //dr["User"] = user;
        //            dr["User"] = Session["UserID"].ToString();

        //            //DataTable locdet = CHNLSVC.Sales.getLocDesc(COM, "LOC", location);
        //            DataTable locdet = CHNLSVC.Sales.getLocDesc(COM, "LOC", Session["UserDefLoca"].ToString());
        //            dr["Location"] = locdet.Rows[0]["descp"].ToString(); //location;

        //            DataTable Comdescr = CHNLSVC.CustService.sp_get_com_details(COM);


        //            //dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
        //            dr["Company"] = Comdescr.Rows[0]["MC_DESC"].ToString();
        //            //dr["company_name"] = BaseCls.GlbReportCompCode;
        //            param.Rows.Add(dr);


        //            item_serials.Clear();

        //            item_serials = CHNLSVC.MsgPortal.getItemSerials(Session["documntNo"].ToString());

        //            _serialItems.Database.Tables["serial_items"].SetDataSource(item_serials);

        //            _serialItems.Database.Tables["param"].SetDataSource(param);

        //            rptDoc.Load(ReportPath + "\\Inventory\\serial_items.rpt");
        //            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            _serialItems.ExportToDisk(ExportFormatType.PortableDocFormat, targetFileName);
        //            _serialItems.Close();
        //            //_serialItems.Dispose();
        //            rptDoc.Close();
        //            rptDoc.Dispose();
        //            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            //****************************************************

        //            //serial_items _warraPrint = new serial_items();
        //            //ReportDocument rptDoc = new ReportDocument();
        //            //DataTable MST_COM = new DataTable("com");
        //            //DataTable param = new DataTable("param");
        //            //DataRow dr;
        //            //MST_COM = CHNLSVC.General.GetCompanyByCode("ABL");

        //            //param.Columns.Add("user", typeof(string));

        //            //param.Rows.Add(Session["UserID"].ToString());
        //            //DataTable tmp_Table = new DataTable("scm_warranty_movemnet");
        //            //tmp_Table = CHNLSVC.Inventory.getWarrantyPrintMobDetails(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), txtItemCode.Text.ToString(), txtSerial1.Text.ToString(), 1);
        //            //_warraPrint.Database.Tables["WARR_PRINT"].SetDataSource(tmp_Table);

        //            //rptDoc.Load(ReportPath + "\\Inventory\\serial_items.rpt");
        //            //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            //_warraPrint.ExportToDisk(ExportFormatType.PortableDocFormat, targetFileName);
        //            //_warraPrint.Close();
        //            ////_warraPrint.Dispose();
        //            //rptDoc.Close();
        //            //rptDoc.Dispose();
        //            //string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //        }
        //        catch (Exception ex)
        //        {
        //            CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print Serial", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
        //        }
        //    }

        //    if (value == 3)
        //    {
        //        try
        //        {
        //            string _repname;
        //            string _papersize;
        //            ReportDocument rptDoc = new ReportDocument();
        //            Outward_Docs_Full_ARL_EXP_Details _outdocfull_Arl_Exp = new Outward_Docs_Full_ARL_EXP_Details();

        //            Session["GlbReportType"] = "SCM1_AODOT";
        //            //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //            //BaseCls.GlbReportHeading = "OUTWARD DOC";
        //            Session["GlbReportName"] = "Outward_Docs_Full_ARL_EXP_Details.rpt";
        //            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            PrintPDFnew(targetFileName);

        //            //rptDoc.Load(ReportPath + "\\Inventory\\Outward_Docs_Full_ARL_EXP_Details.rpt");
        //            ////string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
        //            //_outdocfull_Arl_Exp.ExportToDisk(ExportFormatType.PortableDocFormat, targetFileName);
        //            //_outdocfull_Arl_Exp.Close();
        //            ////_serialItems.Dispose();
        //            //rptDoc.Close();
        //            //rptDoc.Dispose();

        //            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
        //            //PrintPDF(targetFileName);
        //            //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //        }
        //        catch (Exception ex)
        //        {
        //            CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Print Expiry", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
        //        }
        //    }
        //}

        //public void PrintPDFnew(string targetFileName)
        //{
        //    try
        //    {
        //        ReportDocument rptDoc = new ReportDocument();

        //        clsInventory obj = new clsInventory();
        //        obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
        //        if (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_OUT_SGL;//_outdocfull_IN_SGL;
        //        }
        //        else
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull;
        //        }

        //        if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ARL.rpt")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_Arl;
        //        }

        //        if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ARL_EXP_Details.rpt")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_Arl_Exp;
        //        }

        //        if (Session["GlbReportName"].ToString() == "Outward_Docs_AODOUTNEW.rpt")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_auto;
        //        }
        //        if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ABE_OUTN.rpt")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_N;
        //        }
        //        if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ABE_OUTP.rpt")
        //        {
        //            rptDoc = (ReportDocument)obj._outdocfull_P;
        //        }

        //        DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
        //        rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //        rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //        diskOpts.DiskFileName = targetFileName;
        //        rptDoc.ExportOptions.DestinationOptions = diskOpts;
        //        rptDoc.Export();

        //        rptDoc.Close();
        //        rptDoc.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //*******************************************************************
        public void PrintPDF(string targetFileName)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();

                clsInventory obj = new clsInventory();
                obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                if (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_OUT_SGL;//_outdocfull_IN_SGL;
                }
                else
                {
                    rptDoc = (ReportDocument)obj._outdocfull;
                }


                if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ARL.rpt")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_Arl;
                }

                if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ARL_EXP_Details.rpt")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_Arl_Exp;
                }

                if (Session["GlbReportName"].ToString() == "Outward_Docs_AODOUTNEW.rpt")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_auto;
                }
                if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ABE_OUTN.rpt")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_N;
                }
                if (Session["GlbReportName"].ToString() == "Outward_Docs_Full_ABE_OUTP.rpt")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_P;
                }
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

        public void PrintPDFSer(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                ReportDocument rptDoc = (ReportDocument)obj._serialItems;
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
                        Session["print"] = 1;
                        _print();
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document no !');", true);
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
                        _print();
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document no !');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnprintexpiry_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {

                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        Session["print"] = 3;
                        _print();
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document no !');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnSeDocNo_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text));

            var sortedTable = _result.AsEnumerable()
                .OrderByDescending(r => r.Field<String>("Document"))
                .CopyToDataTable();

            _result = _result.AsEnumerable().OrderByDescending(r => r.Field<String>("Document")).CopyToDataTable();
            ViewState["SEARCH"] = _result;
            grdResultD.DataSource = sortedTable;
            grdResultD.DataBind();
            BindUCtrlDDLData2(sortedTable);
            lblvalue.Text = "Doc";
            Session["POPUP_LOADED"] = "1";
            txtFDate.Text = Convert.ToDateTime(txtFrom.Text).Date.ToShortDateString();
            txtTDate.Text = Convert.ToDateTime(txtTo.Text).Date.ToShortDateString();
            UserDPopoup.Show();
        }


        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                DataTable _result = new DataTable();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text));

                _result = (DataTable)ViewState["SEARCH"];
                var sortedTable = _result.AsEnumerable()
                    .OrderByDescending(r => r.Field<String>("Document"))
                    .CopyToDataTable();

                grdResultD.DataSource = sortedTable;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (result.Rows.Count > 0)
                {
                    DataView dv = result.DefaultView;
                    dv.Sort = "DATE ASC";
                    result = dv.ToTable();
                }
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(result);
                lblvalue.Text = "Batch";
                UserDPopoup.Show();
            }
            //grdResultD.PageIndex = e.NewPageIndex;
            //grdResultD.DataSource = null;
            //grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
            //grdResultD.DataBind();
            //grdResultD.PageIndex = 0;
            //UserDPopoup.Show();
            txtSearchbyword.Focus();
        }


        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_new(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                ViewState["SEARCH"] = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable _result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "DATE ASC";
                    _result = dv.ToTable();
                }
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Batch";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }

        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor_new(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Dpop"] = "true";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable _result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Batch";
                Session["Dpop"] = "true";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }

        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Batch")
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (result.Rows.Count > 0)
                {
                    DataView dv = result.DefaultView;
                    dv.Sort = "DATE ASC";
                    result = dv.ToTable();
                }
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                ViewState["SEARCH"] = result;
                lblvalue.Text = "Batch";
                BindUCtrlDDLData2(result);
                txtSearchbyword.Focus();
                UserDPopoup.Show();
            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                ViewState["SEARCH"] = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
            }


        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;

            if (lblvalue.Text == "Doc")
            {
                txtDocNo.Text = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                GetDocData(Name);
                LoadHeader(false);
                Session["documntNo"] = Name;

                btnSave.Visible = false;

                UserDPopoup.Hide();

                return;
            }
            else if (lblvalue.Text == "Batch")
            {
                txtBoq.Text = grdResultD.SelectedRow.Cells[4].Text;
                txtBoq_TextChanged(null, null);
                ucOutScan.Batchno = txtBoq.Text;
                lblvalue.Text = "";
                UserDPopoup.Hide();
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
        private void LoadHeader(bool istemp)
        {
            try
            {
                gvPending.DataSource = new int[] { };
                gvPending.DataBind();

                DataTable dtheader = new DataTable();
                InventoryHeader _inventoryHeader = new InventoryHeader();
                if (istemp == true)
                {

                }
                else
                {
                    _inventoryHeader = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text.Trim());
                }

                #region Inventory Header Value Assign

                chkDirectOut.Checked = _inventoryHeader.Ith_anal_10;
                txtDate.Text = _inventoryHeader.Ith_anal_8.ToString();
                txtDate.Text = _inventoryHeader.Ith_anal_9.ToString();
                chkManualRef.Checked = _inventoryHeader.Ith_is_manual;
                txtManualRef.Text = _inventoryHeader.Ith_manual_ref;
                txtDispatchRequried.Text = _inventoryHeader.Ith_oth_loc;
                txtRemarks.Text = _inventoryHeader.Ith_remarks;
                txtnewRemarks.Text = _inventoryHeader.Ith_remarks;
                ddlDeliver.SelectedItem.Text = _inventoryHeader.Ith_anal_3;
                txtVehicle.Text = _inventoryHeader.Ith_vehi_no;



                #endregion

            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                    gvSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = new int[] { };
                    gvItems.DataBind();



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


                err:
                    if (_invalidDoc == false)
                    {
                        txtDocNo.Text = "";
                        txtDocNo.Focus();
                        return;
                    }
                    else
                    {

                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtnewRemarks.Text = _invHdr.Ith_remarks;
                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocNo.Text);


                    if (_serList != null)
                    {
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[5] { new DataColumn("Tus_itm_cd"), new DataColumn("Tus_itm_desc"), new DataColumn("Tus_itm_model"), new DataColumn("Tus_itm_stus"), new DataColumn("Tus_qty") });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            InventoryRequestItem _itm = new InventoryRequestItem();
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
                        gvItems.AutoGenerateColumns = false;
                        gvItems.DataSource = new int[] { };
                        gvItems.DataSource = setItemStatus1(_itmList);
                        gvItems.DataBind();
                        LoadMrnData(_itmList);

                        setItemStatus2(_serList);
                        gvSerial.AutoGenerateColumns = false;
                        gvSerial.DataSource = _serList;
                        gvSerial.DataBind();
                    }
                    else
                    {
                        txtDocNo.Text = "";
                        txtDocNo.Focus();
                        return;
                    }
                    #endregion
                    //foreach (GridViewRow gvr in gvItem.Rows)
                    //{
                    //    LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                    //    LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;
                    //    Addrow.Enabled = false;
                    //    Addrow.OnClientClick = "return Enable();";
                    //    Delrow.Enabled = false;
                    //    Delrow.OnClientClick = "return Enable();";

                    //}
                    //foreach (GridViewRow gvr in gvSerial.Rows)
                    //{
                    //    LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;
                    //    Addrow.Enabled = false;
                    //    Addrow.OnClientClick = "return Enable();";
                    //}

                    //Get Transport Details.
                    ucTransportMethode.DgvTrns.DataSource = new int[] { };
                    _traList = new List<Transport>();
                    _traList = CHNLSVC.General.GET_INT_TRANSPORT(new Transport() { Itrn_ref_doc = txtDocNo.Text, Itrn_stus = "A" });
                    if (_traList != null)
                    {
                        if (_traList.Count > 0)
                        {
                            Int32 row = 0;
                            row = _traList.Max(c => c._grdRowNo);
                            foreach (var item in _traList)
                            {
                                item._grdRowNo = row;
                                row++;
                            }
                            ucTransportMethode.DgvTrns.DataSource = _traList;
                        }
                    }
                    ucTransportMethode.DgvTrns.DataBind();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
            }

        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
        }


        protected void lbtnTransportMth_Click(object sender, EventArgs e)
        {
            popupTransport.Show();
            Session["TransportMode"] = "Show";
            ucTransportMethode._serPopShow = false;
        }

        protected void lbtnCls_Click(object sender, EventArgs e)
        {
            popupTransport.Hide();
            Session["TransportMode"] = "Close";
        }

        protected void lprintcour_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["Type"] = "AOD";
                Session["RefDoc"] = txtDocNo.Text.Trim();
                Session["CourierType"] = "AOD";
                if (txtDocNo.Text.Trim() == null | txtDocNo.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Document No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "abscourier.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDFCourier(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //Session["GlbReportName"] = "abscourier.rpt";
                    //BaseCls.GlbReportHeading = "ABS COURIER Report";
                    //Session["GlbReportName"] = "abscourier.rpt";
                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Courier Print", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
            }
        }

        public void PrintPDFCourier(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                ReportDocument rptDoc = new ReportDocument();
                obj.get_courierdata(Session["RefDoc"].ToString(), Session["UserCompanyCode"].ToString(), Session["Type"].ToString(), Session["UserID"].ToString());
                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    rptDoc = (ReportDocument)obj._aalcour;
                }
                else
                {
                    rptDoc = (ReportDocument)obj._abscour;
                }
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

        protected void selectchk_CheckedChanged(object sender, EventArgs e)
        {
            List<ReptPickSerials> _list = (List<ReptPickSerials>)Session["serial_list"];
            var lb = (CheckBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label lblTus_ser_id = row.FindControl("lblTus_ser_id") as Label;
            CheckBox selectchk = row.FindControl("selectchk") as CheckBox;
            _list.Where(c => c.Tus_ser_id == Convert.ToInt32(lblTus_ser_id.Text)).FirstOrDefault().Tus_isSelect = selectchk.Checked;
            grdAdSearch.DataSource = _list;
            grdAdSearch.DataBind();
            mpPickSerial.Show();
        }



        //boq
        protected void lbtnProCode_Click(object sender, EventArgs e)
        {
            if (!chkboq.Checked && !chkbatch.Checked && !chkProd.Checked)
            {
                DispMsg("Please select the type !"); return;
            }
            if (chkboq.Checked)
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BOQ);
                DataTable result = CHNLSVC.CommonSearch.SearchProductionNoAodOut(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                lblvalue.Text = "boq";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                mpSearch.Show();
            }
            else if (chkbatch.Checked)
            {
                txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (result.Rows.Count > 0)
                {
                    DataView dv = result.DefaultView;
                    dv.Sort = "DATE ASC";
                    result = dv.ToTable();
                }
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                ViewState["SEARCH"] = result;
                lblvalue.Text = "Batch";
                BindUCtrlDDLData2(result);
                txtSearchbyword.Focus();
                UserDPopoup.Show();
            }
            else if (chkProd.Checked)
            {
                if (txtDispatchRequried.Text == "")
                {
                    DisplayMessage("Please Select Recieved Loc", 2);
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable result = CHNLSVC.CommonSearch.SearchProductionNoAodOut(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                lblvalue.Text = "ProductionPlan";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                mpSearch.Show();
            }

        }

        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/MasterFiles/Warehouse/BinSetup.aspx");
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

        protected void txtReqLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<InventoryRequest> list = _serData;

                if (list.Count > 0)
                {

                    List<InventoryRequest> newlist = new List<InventoryRequest>();
                    if (txtReqLoc.Text != "")
                    {
                        foreach (InventoryRequest ir in list)
                        {
                            if (ir.Itr_rec_to == txtReqLoc.Text)
                            {
                                newlist.Add(ir);
                            }
                        }
                        hfScrollPosition.Value = "0";
                        gvPending.DataSource = newlist;
                        gvPending.DataBind();

                    }
                    else
                    {
                        hfScrollPosition.Value = "0";
                        gvPending.DataSource = list;
                        gvPending.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnSReqLoc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ReqLocation";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected DataTable GetRequestCurrentUser(string reqNo)
        {
            DataTable curPicker = CHNLSVC.Inventory.GetPickSerByDocument(Session["UserCompanyCode"].ToString(), reqNo);

            if (curPicker.Rows.Count > 0)
            {
                DataRow row = curPicker.Rows[0];
                if (row[29].ToString() != Session["UserID"].ToString())
                {
                    return curPicker;
                }
                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                Session["documntNo"] = txtDocNo.Text;
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
        protected void chkPendingDoc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnDocSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            try
            {
                int val = 0;
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, Session["UserCompanyCode"].ToString(), txtRequest.Text, 0);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo(ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, txtdocname.Text);
                    if (user_seq_num > 0)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                        _inputReptPickHeader.Tuh_direct = false;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = txtRequest.Text.Trim();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        if (ddlType.SelectedValue == "MRNA")
                        {
                            _inputReptPickHeader.Tuh_is_take_res = true;
                        }
                        val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                        if (val == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = txtRequest.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue;
                    _inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    //if (string.IsNullOrEmpty(_soano))
                    //{
                    //    _inputReptPickHeader.Tuh_is_take_res = true;
                    //}
                    if (ddlType.SelectedValue == "MRNA")
                    {
                        _inputReptPickHeader.Tuh_is_take_res = true;
                    }
                    if (ddlType.SelectedValue == "MRNS")
                    {
                        _inputReptPickHeader.Tuh_is_take_res = true;
                    }
                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

                        return;
                    }
                }
                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to serial picker or has alread processed !!!')", true);
                    return;
                }
                else
                {
                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();

                    List<InventoryRequestItem> _select = new List<InventoryRequestItem>();

                    _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;
                    foreach (InventoryRequestItem _itm in _select)
                    {
                        ReptPickItems _reptitm = new ReptPickItems();
                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                        _reptitm.Tui_req_itm_qty = _itm.Itri_bqty;
                        _reptitm.Tui_req_itm_cd = _itm.Itri_itm_cd;
                        _reptitm.Tui_req_itm_stus = _itm.Itri_itm_stus;
                        _reptitm.Tui_pic_itm_cd = _itm.Itri_line_no.ToString();
                        // _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Itri_line_no);//Darshana request add by rukshan
                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                        _reptitm.Tui_pic_itm_qty = 0;
                        _saveonly.Add(_reptitm);


                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
                if (val == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                    MPPDA.Hide();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        protected void ddlSerTp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region Batch wise out 18 Oct 2016 Lakshan
        protected void chkboq_CheckedChanged(object sender, EventArgs e)
        {
            ucOutScan.isbatchserial = false;
            ucOutScan._batchBaseOut = false;
            ucOutScan._prodPlanBaseOut = false;
            if (chkboq.Checked)
            {
                if (chkDirectOut.Checked)
                {
                    ucOutScan.isbatchserial = false;
                    ucOutScan._batchBaseOut = false;
                    _batchBaseOut = false;
                    DesableBatchControler(false);
                }
                else
                {
                    chkboq.Checked = false;
                    DispMsg("Please select direct out option");
                    return;
                }
            }
        }
        protected void chkbatch_CheckedChanged(object sender, EventArgs e)
        {
            ucOutScan.isbatchserial = false;
            ucOutScan._batchBaseOut = false;
            ucOutScan._prodPlanBaseOut = false;
            _batchBaseOut = false;
            if (chkbatch.Checked)
            {
                if (chkDirectOut.Checked)
                {
                    ucOutScan.isbatchserial = true;
                    ucOutScan._batchBaseOut = true;
                    _batchBaseOut = true;
                    DesableBatchControler(false);
                }
                else
                {
                    chkbatch.Checked = false;
                    DispMsg("Please select direct out option");
                    return;
                }
            }
        }

        private void DesableBatchControler(bool b)
        {
            chkDirectOut.Enabled = b;
            chkbatch.Enabled = b;
            chkboq.Enabled = b;
            chkProd.Enabled = b;
            ddlType.Enabled = b;
        }
        protected void txtBoq_TextChanged(object sender, EventArgs e)
        {
            ucOutScan._batchNo = "";
            if (!string.IsNullOrEmpty(txtBoq.Text))
            {
                if (!chkDirectOut.Checked)
                {
                    txtBoq.Text = ""; DispMsg("Please select direct out option"); return;
                }
                if (!chkbatch.Checked && !chkboq.Checked)
                {
                    //txtBoq.Text = ""; DispMsg("Please select the type !"); return;
                }
                if (chkbatch.Checked)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                    DataTable result = new DataTable();
                    if (!string.IsNullOrEmpty(txtBoq.Text))
                    {
                        result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, "BATCH #", txtBoq.Text.Trim(), DateTime.MinValue, DateTime.MaxValue);
                    }
                    bool b2 = false;
                    foreach (DataRow row in result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["BATCH #"].ToString()))
                        {
                            if (txtBoq.Text.ToUpper() == row["BATCH #"].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (!b2)
                    {
                        txtBoq.Text = ""; DispMsg("Please select the valid document # !"); return;
                    }
                    else
                    {
                        ucOutScan._batchNo = txtBoq.Text.Trim();
                        txtBoq.Enabled = false;
                    }
                }
                if (chkboq.Checked)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = new DataTable();
                    if (!string.IsNullOrEmpty(txtBoq.Text))
                    {
                        result = CHNLSVC.General.SearchBOQDocNo(SearchParams, "DOC NO", txtBoq.Text.Trim());
                    }
                    bool b2 = false;
                    foreach (DataRow row in result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["DOC NO"].ToString()))
                        {
                            if (txtBoq.Text.ToUpper() == row["DOC NO"].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (!b2)
                    {
                        txtBoq.Text = ""; DispMsg("Please select the valid document # !"); return;
                    }
                    else
                    {
                        txtBoq.Enabled = false;
                    }
                }
                if (chkProd.Checked)
                {
                    _prodNo = "";
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                    DataTable result = new DataTable();
                    if (!string.IsNullOrEmpty(txtBoq.Text))
                    {
                        result = CHNLSVC.CommonSearch.SearchProductionNoAodOut(SearchParams, "DOC NO", txtBoq.Text.Trim());
                    }
                    bool b2 = false;
                    foreach (DataRow row in result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["DOC NO"].ToString()))
                        {
                            if (txtBoq.Text.ToUpper() == row["DOC NO"].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (!b2)
                    {
                        txtBoq.Text = ""; DispMsg("Please select the valid document # !"); return;
                    }
                    else
                    {
                        txtBoq.Enabled = false;
                        _prodNo = txtBoq.Text.Trim();
                        ucOutScan._prodNo = txtBoq.Text.Trim();
                    }
                }
                if (chkboq.Checked)
                {
                    loadBoqItem(txtBoq.Text);
                }
            }

            //SatProjectHeader _SatProjectHeader = new SatProjectHeader();
            //_SatProjectHeader = CHNLSVC.Sales.GETBOQHDR(Session["UserCompanyCode"].ToString(), txtBoq.Text.Trim());
            //if (_SatProjectHeader == null)
            //{
            //    txtBoq.Text = "";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid BOQ number')", true);
            //    return;
            //}
            //else
            //{
            //    loadBoqItem(txtBoq.Text);
            //}
        }
        private void loadBoqItem(string RequestNo)
        {
            if (RequestNo == "")
            {
                return;
            }
            if (!chkDirectOut.Checked)
            {
                DispMsg("Please select direct out option");
                return;
            }
            ViewState["doc_tp"] = "COM_OUT";
            Session["_documenttype"] = "COM_OUT";
            COM_OUT = "COM_OUT";
            //  ucOutScan, _derectOut = true;
            _derectOut = true;
            UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("BOQ", Session["UserCompanyCode"].ToString(), RequestNo, 0);
            if (UserSeqNo == -1)
            {
                UserSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), ddlType.SelectedValue == "MRNS" ? "MRNA" : ddlType.SelectedValue, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "COM_OUT";
                RPH.Tuh_cre_dt = DateTime.Today;
                RPH.Tuh_ischek_itmstus = false;
                RPH.Tuh_ischek_reqqty = false;
                RPH.Tuh_ischek_simitm = false;
                RPH.Tuh_session_id = Session["SessionID"].ToString();
                RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                RPH.Tuh_usr_id = Session["UserID"].ToString();
                RPH.Tuh_usrseq_no = UserSeqNo;
                RPH.Tuh_direct = false;
                RPH.Tuh_doc_no = RequestNo;
                if (ddlType.SelectedValue == "MRNA")
                {
                    RPH.Tuh_is_take_res = true;
                }
                if (ddlType.SelectedValue == "MRNS")
                {
                    RPH.Tuh_is_take_res = true;
                }
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                ucOutScan.userSeqNo = UserSeqNo.ToString();
                ViewState["userSeqNo"] = UserSeqNo;
                BindPickSerials(UserSeqNo);
                gvItems.DataSource = new int[] { };
                gvItems.DataBind();
            }
            else
            {
                ucOutScan.userSeqNo = UserSeqNo.ToString();
                ViewState["userSeqNo"] = UserSeqNo;
                List<InventoryRequestItem> _lst = new List<InventoryRequestItem>();
                List<ReptPickItems> _listitm = new List<ReptPickItems>();
                _listitm = CHNLSVC.Inventory.GetAllPickItem(Session["UserCompanyCode"].ToString(), UserSeqNo, "COM_OUT", RequestNo, null, null);
                if (_listitm.Count > 0)
                {
                    foreach (ReptPickItems _itm in _listitm)
                    {
                        InventoryRequestItem objitm = new InventoryRequestItem();
                        objitm.Itri_itm_cd = _itm.Tui_req_itm_cd;
                        MasterItem msitem = new MasterItem();
                        msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Tui_req_itm_cd);
                        objitm.Mi_longdesc = msitem.Mi_longdesc;
                        objitm.Mi_model = msitem.Mi_model;
                        objitm.Itri_itm_stus = _itm.Tui_req_itm_stus;
                        objitm.Itri_job_line = Convert.ToInt32(_itm.Tui_pic_itm_cd);
                        objitm.Itri_app_qty = _itm.Tui_req_itm_qty;
                        objitm.Itri_bqty = _itm.Tui_req_itm_qty;
                        objitm.Itri_note = RequestNo;
                        _lst.Add(objitm);
                    }
                    BindPickSerials(UserSeqNo);
                    gvItems.DataSource = setItemStatus1(_lst);
                    gvItems.DataBind();
                    LoadMrnData(_lst);
                    Session["ScanItemListNew"] = _lst;
                }
                if (_listitm.Count == 0)
                {
                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    int val;
                    _SatProjectDetails = CHNLSVC.Sales.GETBOQDETAILS(RequestNo);
                    if (_SatProjectDetails != null)
                    {
                        if (_SatProjectDetails.Count > 0)
                        {

                            foreach (SatProjectDetails _itm in _SatProjectDetails)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = Convert.ToInt32(UserSeqNo);
                                _reptitm.Tui_req_itm_qty = _itm.SPD_EST_QTY;
                                _reptitm.Tui_req_itm_cd = _itm.SPD_ITM;
                                _reptitm.Tui_req_itm_stus = "GDLP";
                                _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.SPD_LINE);//Darshana request add by rukshan
                                // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                                _reptitm.Tui_pic_itm_qty = 0;
                                _saveonly.Add(_reptitm);

                                val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                            }




                        }
                    }
                    _listitm = _saveonly;
                }
            }

        }

        #endregion
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
                    //  txtDate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
                    lblBackDateInfor.Visible = true;
                    lblBackDateInfor.Text = _outmsg;
                    lblBackDateInfor.ForeColor = Color.Red;
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
                    txtDatel.Visible = true;
                    #region add validation to chk period close or not add by lakshan 24 Mar 2017
                    List<RefPrdMt> _perBlockList = CHNLSVC.Inventory.GET_REF_PRD_MT_DATA(new RefPrdMt()
                    {
                        Prd_stus = "CLOSE",
                        Prd_com_cd = Session["UserCompanyCode"].ToString(),
                        Prd_from = _bdt.Gad_act_from_dt,
                        Prd_to = _bdt.Gad_act_to_dt
                    });
                    if (_perBlockList.Count > 0)
                    {
                        txtDatel.Visible = false;
                        // DisplayMessage("Selected period is closed !");
                    }
                    #endregion
                }
                else
                {
                    txtDatel.Visible = true;
                }
            }
            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        protected void linkPrintNo_Click(object sender, EventArgs e)
        {
            #region recall data
            if (_tmpDocTp != "0" && !_tmp_isDiout)
            {
                txtFrom.Text = _tmpdtFrom.ToString("dd/MMM/yyyy");
                txtTo.Text = _tmpdtTo.ToString("dd/MMM/yyyy");
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(_tmpDocTp));
                ddlType_SelectedIndexChanged(null, null);
                //chkPendingDoc.Checked = _pdaCom;
                //if (_pdaCom)
                //{
                //    btnDocSearch_Click(null, null);
                //}
            }
            #endregion
        }

        protected void lbtnPrintOk_Click(object sender, EventArgs e)
        {
            _print();
            #region recall data
            if (_tmpDocTp != "0" && !_tmp_isDiout)
            {
                txtFrom.Text = _tmpdtFrom.ToString("dd/MMM/yyyy");
                txtTo.Text = _tmpdtTo.ToString("dd/MMM/yyyy");
                chkPendingDoc.Checked = _pdaFinish;
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(_tmpDocTp));
                ddlType_SelectedIndexChanged(null, null);
                //chkPendingDoc.Checked = _pdaCom;
                //if (_pdaCom)
                //{
                //    btnDocSearch_Click(null, null);
                //}
            }
            #endregion
        }

        protected void chkChangeStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangeStatus.Checked)
            {
                bool b16062 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16062);
                if (!b16062)
                {
                    chkChangeStatus.Checked = false;
                    DispMsg("Sorry, You have no permission. ( Advice: Required permission code : 16062) !!!", "N");
                    return;
                }
            }
        }

        protected void chkProd_CheckedChanged(object sender, EventArgs e)
        {
            ucOutScan.isbatchserial = false;
            ucOutScan._batchBaseOut = false;
            ucOutScan._prodPlanBaseOut = false;
            if (chkProd.Checked)
            {
                if (chkDirectOut.Checked)
                {
                    ucOutScan.isbatchserial = false;
                    ucOutScan._batchBaseOut = false;
                    ucOutScan._prodPlanBaseOut = true;
                    _batchBaseOut = false;
                    DesableBatchControler(false);
                }
                else
                {
                    chkProd.Checked = false;
                    DispMsg("Please select direct out option");
                    return;
                }
            }
        }

        protected void lbtnlocQty_Click(object sender, EventArgs e)
        {
            popuplocQty.Hide();
        }
        private void LoadMrnData(List<InventoryRequestItem> _reqList)
        {
            lblDocQty.Text = (0).ToString("N2");
            lblDocSerPickQty.Text = (0).ToString("N2");
            if (_reqList != null)
            {
                decimal _reqQty = _reqList.Sum(c => c.Itri_app_qty);
                decimal _pickQty = _reqList.Sum(c => c.Itri_qty);
                lblDocQty.Text = _reqQty.ToString("N2");
                lblDocSerPickQty.Text = _pickQty.ToString("N2");
            }
        }

        protected void chkMakeAdj_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMakeAdj.Checked)
            {
                bool b10146 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10163);
                if (!b10146)
                {
                    DispMsg("Sorry, You have no permission !  (Advice: Required permission code : 10163 ) ");
                    chkMakeAdj.Checked = false;
                    return;
                }
            }
        }

        //dilshan on 13/03/2018
        protected void ddlTransportMe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTransportLabelData();
            BindTransService();
            SHowSearchButton();
        }
        private void LoadTransportLabelData()
        {
            lblSubLvl.Text = "";
            txtSubLvl.Text = "";
            lblSubLvl.Visible = false;
            txtSubLvl.Visible = false;
            divSubLvl.Visible = false;

            lblNxtLvlD1.Text = "";
            txtNxtLvlD1.Text = "";
            lblNxtLvlD1.Visible = false;
            txtNxtLvlD1.Visible = false;
            divNxtLvlD1.Visible = false;

            lblNxtLvlD2.Text = "";
            txtNxtLvlD2.Text = "";
            lblNxtLvlD2.Visible = false;
            txtNxtLvlD2.Visible = false;
            divNxtLvlD2.Visible = false;

            if (ddlTransportMe.SelectedIndex != -1)
            {
                _tMethod = new TransportMethod() { Rtm_tp = ddlTransportMe.SelectedItem.Text };
                _tMethods = new List<TransportMethod>();
                _tMethods = CHNLSVC.General.GET_TRANS_METH(_tMethod);
                if (_tMethods != null)
                {
                    if (_tMethods.Count > 0)
                    {
                        if (_tMethods[0].Rtm_sub_lvl == 1)
                        {
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_sub_dis))
                            {
                                lblSubLvl.Text = _tMethods[0].Rtm_sub_dis;
                                lblSubLvl.Visible = true;
                                txtSubLvl.Visible = true;
                                divSubLvl.Visible = true;
                            }
                        }

                        if (_tMethods[0].Rtm_nxt_lvl == 1)
                        {
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_disp_1))
                            {
                                lblNxtLvlD1.Text = _tMethods[0].Rtm_disp_1;
                                lblNxtLvlD1.Visible = true;
                                txtNxtLvlD1.Visible = true;
                                divNxtLvlD1.Visible = true;
                            }
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_disp_2))
                            {
                                lblNxtLvlD2.Text = _tMethods[0].Rtm_disp_2;
                                lblNxtLvlD2.Visible = true;
                                txtNxtLvlD2.Visible = true;
                                divNxtLvlD2.Visible = true;
                            }
                        }
                    }
                }

            }
        }
        private void BindTransService()
        {
            _tPartys = new List<TransportParty>();
            _tParty = new TransportParty() { Rtnp_tnpt_seq = Convert.ToInt32(ddlTransportMe.SelectedValue), Mbe_com = Session["UserCompanyCode"].ToString() };
            _tPartys = CHNLSVC.General.GET_TRANSPORT_PTY(_tParty);
            while (ddlServiceBy.Items.Count > 0)
            {
                ddlServiceBy.Items.RemoveAt(0);
            }
            ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name != string.Empty && c.Mbe_name != null).OrderBy(c => c.Mbe_name);
            ddlServiceBy.DataTextField = "mbe_name";
            ddlServiceBy.DataValueField = "rtnp_pty_cd";
            ddlServiceBy.DataBind();
            ddlServiceBy_BindTransService();
        }
        protected void ddlServiceBy_BindTransService()
        {
            try
            {
                //if (ddlServiceBy.SelectedIndex > 0)
                //{
                List<MasterBusinessEntity> _busEntList = CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), ddlServiceBy.SelectedValue, "");
                var _busEnt = _busEntList.FirstOrDefault();
                if (_busEnt != null)
                {
                    if (_busEnt.Mbe_curr_slip_gen == 1)
                    {
                        txtSubLvl.Enabled = false;
                        MasterAutoNumber _AutoNo = new MasterAutoNumber();
                        _AutoNo.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        _AutoNo.Aut_cate_tp = "LOC";
                        _AutoNo.Aut_direction = 0;
                        _AutoNo.Aut_modify_dt = null;
                        _AutoNo.Aut_moduleid = "CURR";
                        // _AutoNo.Aut_start_char = "CURR";
                        _AutoNo.Aut_start_char = _busEnt.Mbe_curr_slip_cd.ToUpper();
                        DateTime date = DateTime.Now;
                        _AutoNo.Aut_year = date.Year;
                        Int32 _autoNo = CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char,
                            _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                        //ABS-CURR-17-0001
                        Int32 _rowCount = 0;
                        if (_traListDo.Count > 0)
                        {
                            //_rowCount = _traListDo.Count;
                            if (_traListDo.Count > 0)
                            {
                                var _tmpTraList = _traListDo.Where(c => c.Itrn_trns_pty_cd == ddlServiceBy.SelectedValue).ToList();
                                if (_tmpTraList != null)
                                {
                                    if (_tmpTraList.Count > 0)
                                    {
                                        _rowCount = _tmpTraList.Count;
                                    }
                                }

                            }
                        }
                        _autoNo = _autoNo + _rowCount;
                        string _documentNo = Session["UserDefLoca"].ToString() + "-" + _busEnt.Mbe_curr_slip_cd + "-" + "CO" + "-" + Convert.ToString(date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                        txtSubLvl.Text = _documentNo;
                    }
                    else
                    {
                        txtSubLvl.Enabled = true;
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void SHowSearchButton()
        {
            if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
            {
                lbtnSeVehicle.Visible = true;
                lbtnSeDriver.Visible = true;
                lbtnSeHelper.Visible = true;
            }
            else
            {
                lbtnSeVehicle.Visible = false;
                lbtnSeDriver.Visible = false;
                lbtnSeHelper.Visible = false;
            }
        }
        protected void ddlServiceBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlServiceBy.SelectedIndex > 0)
                {
                    List<MasterBusinessEntity> _busEntList = CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), ddlServiceBy.SelectedValue, "");
                    var _busEnt = _busEntList.FirstOrDefault();
                    if (_busEnt != null)
                    {
                        if (_busEnt.Mbe_curr_slip_gen == 1)
                        {
                            txtSubLvl.Enabled = false;
                            MasterAutoNumber _AutoNo = new MasterAutoNumber();
                            _AutoNo.Aut_cate_cd = Session["UserDefLoca"].ToString();
                            _AutoNo.Aut_cate_tp = "LOC";
                            _AutoNo.Aut_direction = 0;
                            _AutoNo.Aut_modify_dt = null;
                            _AutoNo.Aut_moduleid = "CURR";
                            // _AutoNo.Aut_start_char = "CURR";
                            _AutoNo.Aut_start_char = _busEnt.Mbe_curr_slip_cd.ToUpper();
                            DateTime date = DateTime.Now;
                            _AutoNo.Aut_year = date.Year;
                            Int32 _autoNo = CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char,
                                _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                            //ABS-CURR-17-0001
                            Int32 _rowCount = 0;
                            if (_traListDo.Count > 0)
                            {
                                //_rowCount = _traListDo.Count;
                                if (_traListDo.Count > 0)
                                {
                                    var _tmpTraList = _traListDo.Where(c => c.Itrn_trns_pty_cd == ddlServiceBy.SelectedValue).ToList();
                                    if (_tmpTraList != null)
                                    {
                                        if (_tmpTraList.Count > 0)
                                        {
                                            _rowCount = _tmpTraList.Count;
                                        }
                                    }

                                }
                            }
                            _autoNo = _autoNo + _rowCount;
                            string _documentNo = Session["UserDefLoca"].ToString() + "-" + _busEnt.Mbe_curr_slip_cd + "-" + "CO" + "-" + Convert.ToString(date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                            txtSubLvl.Text = _documentNo;
                        }
                        else
                        {
                            txtSubLvl.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        protected void txtSubLvl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    if (!string.IsNullOrEmpty(txtSubLvl.Text))
                    {
                        FleetVehicleMaster _tmpObj = new FleetVehicleMaster();
                        _tmpObj.Fv_reg_no = txtSubLvl.Text.Trim().ToUpper();
                        FleetVehicleMaster _fleet = new FleetVehicleMaster();
                        _fleet = CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                        if (_fleet != null)
                        {
                            if (_fleet.Fv_com != Session["UserCompanyCode"].ToString())
                            {
                                string _msg = "Cannot view details ! Entered Reg # belongs to " + _fleet.Fv_com + " !";
                                DispMsg(_msg); txtSubLvl.Text = ""; txtSubLvl.Focus(); return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeVehicle_Click(object sender, EventArgs e)
        {
            _serData1 = new DataTable();
            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
            _serData1 = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, null, null);
            LoadSearchPopup("RegistrationDet", "REGISTER NO", "ASC");
        }
        protected void txtNxtLvlD1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    txtNxtLvlD1.ToolTip = string.Empty;
                    if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                        _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                        DataAvailable(_serData1, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
                        txtNxtLvlD1.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtNxtLvlD1.Text = string.Empty;
                            txtNxtLvlD1.Focus();
                            DispMsg("Please enter valid driver !");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnSeDriver_Click(object sender, EventArgs e)
        {
            try
            {
                _serData1 = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Driver", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void txtNxtLvlD2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    txtNxtLvlD2.ToolTip = string.Empty;
                    if (!string.IsNullOrEmpty(txtNxtLvlD2.Text))
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                        _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                        DataAvailable(_serData1, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
                        txtNxtLvlD2.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtNxtLvlD2.Text = string.Empty;
                            txtNxtLvlD2.Focus();
                            DispMsg("Please enter valid helper !");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnSeHelper_Click(object sender, EventArgs e)
        {
            try
            {
                _serData1 = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Helper", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            #region Validate
            if (ddlTransportMe.SelectedIndex == -1)
            {
                DispMsg("Please select a transport methode !!!", "W"); return;
            }
            if (ddlTransportMe.SelectedIndex < 0)
            {
                DispMsg("Please select a service by !!!", "W"); return;
            }
            if (txtSubLvl.Visible && string.IsNullOrEmpty(txtSubLvl.Text))
            {
                DispMsg("Please enter " + lblSubLvl.Text + " !!!", "W"); return;
            }
            //if (string.IsNullOrEmpty(txtRefNo.Text))
            //{
            //    DispMsg("Please enter ref # ", "W"); return;
            //}
            if (txtNxtLvlD1.Visible && string.IsNullOrEmpty(txtNxtLvlD1.Text))
            {
                //DispMsg("Please enter " + lblNxtLvlD1.Text + " !!!", "W"); return;
                txtNxtLvlD1.Text = "N/A";//as per the asanka 26Feb2018
            }
            if (txtNxtLvlD2.Visible && string.IsNullOrEmpty(txtNxtLvlD2.Text))
            {
                DispMsg("Please enter " + lblNxtLvlD2.Text + " !!!", "W"); return;
            }
            if (string.IsNullOrEmpty(txtNoOfPacking.Text))
            {
                DispMsg("Please enter the no of packing required !!!", "W"); return;
            }
            Int32 noOfPack = 0;
            if (!Int32.TryParse(txtNoOfPacking.Text, out noOfPack))
            {
                DispMsg("Please enter valid no packing !!!", "W"); return;
            }
            //if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
            //{
            //    bool _vehicle = validateVehicle(txtVehicle.Text);
            //}
            if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
            {
                if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                    _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                    DataAvailable(_serData1, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
                    txtNxtLvlD1.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtNxtLvlD1.Text = string.Empty;
                        txtNxtLvlD1.Focus();
                        DispMsg("Please enter valid driver !");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtNxtLvlD2.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                    _serData1 = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                    DataAvailable(_serData1, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
                    txtNxtLvlD2.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtNxtLvlD2.Text = string.Empty;
                        txtNxtLvlD2.Focus();
                        DispMsg("Please enter valid helper !");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtSubLvl.Text))
                {
                    FleetVehicleMaster _tmpObj = new FleetVehicleMaster();
                    _tmpObj.Fv_reg_no = txtSubLvl.Text.Trim().ToUpper();
                    FleetVehicleMaster _fleet = new FleetVehicleMaster();
                    _fleet = CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                    if (_fleet != null)
                    {
                        if (_fleet.Fv_com != Session["UserCompanyCode"].ToString())
                        {
                            string _msg = "Cannot view details ! Entered Reg # belongs to " + _fleet.Fv_com + " !";
                            DispMsg(_msg); txtSubLvl.Text = ""; txtSubLvl.Focus(); return;
                        }
                    }
                }

            }
            #endregion
            #region add by lakshan auto no genarate
            bool _autoSlipgen = false;
            string _autoNoCurrCd = "";
            MasterAutoNumber _AutoNo = new MasterAutoNumber();

            //commented by dilshan
            //if (ddlServiceBy.SelectedIndex > 0)
            //{
            List<MasterBusinessEntity> _busEntList = CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), ddlServiceBy.SelectedValue, "");
            var _busEnt = _busEntList.FirstOrDefault();
            if (_busEnt != null)
            {
                if (_busEnt.Mbe_curr_slip_gen == 1)
                {
                    _autoSlipgen = true;
                    _AutoNo = new MasterAutoNumber();
                    _AutoNo.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    _AutoNo.Aut_cate_tp = "LOC";
                    _AutoNo.Aut_direction = 0;
                    _AutoNo.Aut_modify_dt = null;
                    _AutoNo.Aut_moduleid = "CURR";
                    //  _AutoNo.Aut_start_char = "CURR";
                    _AutoNo.Aut_start_char = _busEnt.Mbe_curr_slip_cd.ToUpper();
                    DateTime date = DateTime.Now;
                    _AutoNo.Aut_year = date.Year;
                    _autoNoCurrCd = _busEnt.Mbe_curr_slip_cd;
                }
            }
            //}
            #endregion
            Int32 row = 0;
            Int32 _rowCount = 0;
            if (_traListDo != null)
            {
                if (_traListDo.Count > 0)
                {
                    row = _traListDo.Max(c => c._grdRowNo);
                }
            }
            while (noOfPack > 0)
            {
                noOfPack--;
                _tra = new Transport();
                //_tra.itrn_seq                       
                _tra.Itrn_com = Session["UserCompanyCode"].ToString();
                _tra.Itrn_loc = Session["UserDefLoca"].ToString();
                // _tra.Itrn_trns_no = transActionNo;
                _tra.Itrn_ref_doc = docNo;
                _tra.Itrn_trns_method = ddlTransportMe.SelectedItem.Text;
                _tra.Itrn_trns_pty_tp = ddlTransportMe.SelectedItem.Text;
                _tra.Itrn_trns_pty_cd = ddlServiceBy.SelectedValue;
                _tra.Itrn_ref_no = txtSubLvl.Text;
                _tra.Itrn_rmk = txtRemarksTrans.Text;
                _tra.Itrn_stus = "A";
                _tra.Itrn_cre_by = Session["UserID"].ToString();
                _tra.Itrn_cre_dt = DateTime.Now;
                _tra.Itrn_cre_session = Session["SessionID"].ToString();
                _tra.Itrn_mod_by = Session["UserID"].ToString();
                _tra.Itrn_mod_dt = DateTime.Now;
                _tra.Itrn_mod_session = Session["SessionID"].ToString();
                _tra.Itrn_anal1 = txtNxtLvlD1.Text;
                _tra.Itrn_anal2 = txtNxtLvlD2.Text;
                _tra._grdRowNo = row;
                _tra.Tmp_slip_cd = "";
                _tra.itrn_isactive = 1;
                #region add auto parameters
                if (_autoSlipgen)
                {
                    if (_traListDo.Count > 0)
                    {
                        var _tmpTraList = _traListDo.Where(c => c.Itrn_trns_pty_cd == _tra.Itrn_trns_pty_cd).ToList();
                        if (_tmpTraList != null)
                        {
                            if (_tmpTraList.Count > 0)
                            {
                                _rowCount = _tmpTraList.Count;
                            }
                        }

                    }
                    _tra.Mbe_curr_slip_cd = _autoNoCurrCd;
                    Int32 _autoNo = CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char,
                            _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                    _autoNo = _autoNo + _rowCount;
                    string _documentNo = Session["UserDefLoca"].ToString() + "-" + _tra.Mbe_curr_slip_cd + "-" + "CO" + "-" + Convert.ToString(DateTime.Today.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                    _tra.Tmp_slip_cd = _documentNo;
                    _tra.MstAuto = _AutoNo;
                    _tra.Slip_no_auto_gen = true;
                    _tra.Itrn_ref_no = _tra.Tmp_slip_cd;
                }
                #endregion
                _traListDo.Add(_tra);
                row++;
            }

            dgvTrns.DataSource = new int[] { };
            if (_traListDo.Count > 0)
            {
                var _ds = _traListDo.Where(c => c.Itrn_stus == "A").ToList();
                if (_ds != null)
                {
                    dgvTrns.DataSource = _ds;
                }
            }
            dgvTrns.DataBind();
            ClearUcTrans();
        }
        public void ClearUcTrans()
        {
            ddlTransportMe.SelectedIndex = 0;
            ddlTransportMe_SelectedIndexChanged(null, null);
            BindTransService();
            //ddlServiceBy.SelectedIndex = 0;
            txtSubLvl.Text = "";
            txtRemarks.Text = "";
            txtNxtLvlD1.Text = "";
            txtNxtLvlD2.Text = "";
            txtNoOfPacking.Text = "";
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
        private void LoadSearchPopup(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                if (_serData1 != null)
                {
                    if (_serData1.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData1;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData1);
                    }
                }
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
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }
        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData1.Rows.Count > 0)
                {
                    DataView dv = _serData1.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData1 = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTrns.DataSource = new int[] { };
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (_traListDo != null)
                {
                    if (row != null)
                    {
                        Label lblRowNo = (Label)row.FindControl("lblRowNo");
                        Int32 _rowNo = Convert.ToInt32(lblRowNo.Text);
                        var v = _traListDo.Where(c => c._grdRowNo == _rowNo).FirstOrDefault();
                        if (v != null)
                        {
                            v.Itrn_stus = "C";
                        }
                    }
                    var _ds = _traListDo.Where(c => c.Itrn_stus == "A").ToList();
                    if (_ds != null)
                    {
                        dgvTrns.DataSource = _ds;
                    }
                }
                dgvTrns.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lprintcourPrint()
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["Type"] = "AOD";
                //Session["RefDoc"] = txtDocNo.Text.Trim();
                Session["RefDoc"] = Session["documntNo"];
                Session["CourierType"] = "AOD";
                if (Session["RefDoc"] == null)
                {
                    DisplayMessage("Please check Document", 1);
                }
                else
                {
                    if (Session["UserCompanyCode"].ToString() == "AAL")
                    {
                        Session["GlbReportName"] = "abscourier.rpt";
                        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                        PrintPDFCourier(targetFileName);
                        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                        //Session["GlbReportName"] = "abscourier.rpt";
                        //BaseCls.GlbReportHeading = "ABS COURIER Report";
                        //Session["GlbReportName"] = "abscourier.rpt";
                        //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Stock tran Outward Courier Print", "StockTransferOutwardEntry", ex.Message, Session["UserID"].ToString());
            }
        }
        //*********************
    }
}