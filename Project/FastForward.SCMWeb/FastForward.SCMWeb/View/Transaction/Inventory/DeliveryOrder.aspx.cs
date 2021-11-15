using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class DeliveryOrder : Base
    {
        #region transport prop
        private string docNo { get; set; }
        public bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        DataTable _serData
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
        protected List<string> _SOANOList { get { return (List<string>)Session["_SOANOList"]; } set { Session["_SOANOList"] = value; } }
        protected String _soano { get { return (String)Session["_soano"]; } set { Session["_soano"] = value; } }
        protected String _sono { get { return (String)Session["_sono"]; } set { Session["_sono"] = value; } }
        protected String _simItem
        {
            get
            {
                if (Session["_simItem"] != null)
                {
                    return (string)Session["_simItem"];
                }
                else
                {
                    return "";
                }
            }
            set { Session["_simItem"] = value; }
        }
        public bool _isQutationBaseDO
        {
            get
            {
                if (ViewState["_isQutationBaseDO"] == null)
                {
                    return false;
                }
                else { return Convert.ToBoolean(ViewState["_isQutationBaseDO"].ToString()); }
            }
            set { ViewState["_isQutationBaseDO"] = value; }
        }
        protected bool Issoa_select { get { return (bool)Session["Issoa_select"]; } set { Session["Issoa_select"] = value; } }
        protected bool IsSO_base { get { return (bool)Session["_IsSO_base"]; } set { Session["_IsSO_base"] = value; } }
        protected List<ImpCusdecHdr> _hddr { get { return (List<ImpCusdecHdr>)Session["_hddr"]; } set { Session["_hddr"] = value; } }
        //protected List<Transport> _traList
        //{
        //    get
        //    {
        //        if (Session["_traList"] == null)
        //        {
        //            return new List<Transport>();
        //        }
        //        else
        //        {
        //            return (List<Transport>)Session["_traList"];
        //        }
        //    }
        //    set { Session["_traList"] = value; }
        //}
        private MasterCompany oMstCom { get { return (MasterCompany)Session["oMstCom"]; } set { Session["oMstCom"] = value; } }
        private MasterItem msitem { get { return (MasterItem)Session["msitem"]; } set { Session["msitem"] = value; } }
        protected string _resqty { get { return (string)Session["_resqty"]; } set { Session["_resqty"] = value; } }
        protected List<InvoiceItem> invoice_items { get { return (List<InvoiceItem>)Session["invoice_items"]; } set { Session["invoice_items"] = value; } }
        protected List<InvoiceItem> invoice_items_bind { get { return (List<InvoiceItem>)Session["invoice_items_bind"]; } set { Session["invoice_items_bind"] = value; } }
        protected string _profitCenter { get { return (string)Session["_profitCenter"]; } set { Session["_profitCenter"] = value; } }
        protected string _accNo { get { return (string)Session["_accNo"]; } set { Session["_accNo"] = value; } }
        protected string _invoiceType { get { return (string)Session["_invoiceType"]; } set { Session["_invoiceType"] = value; } }
        protected bool _isCheckStatus { get { Session["_isCheckStatus"] = (Session["_isCheckStatus"] == null) ? false : (bool)Session["_isCheckStatus"]; return (bool)Session["_isCheckStatus"]; } set { Session["_stopit"] = value; } }
        protected bool _isRevertStatus { get { Session["_isRevertStatus"] = (Session["_isRevertStatus"] == null) ? false : (bool)Session["_isRevertStatus"]; return (bool)Session["_isRevertStatus"]; } set { Session["_isRevertStatus"] = value; } }
        protected bool _isAgePriceLevel { get { Session["_isAgePriceLevel"] = (Session["_isAgePriceLevel"] == null) ? false : (bool)Session["_isAgePriceLevel"]; return (bool)Session["_isAgePriceLevel"]; } set { Session["_isAgePriceLevel"] = value; } }
        protected bool _isSerializedPrice { get { Session["_isSerializedPrice"] = (Session["_isSerializedPrice"] == null) ? false : (bool)Session["_isSerializedPrice"]; return (bool)Session["_isSerializedPrice"]; } set { Session["_isSerializedPrice"] = value; } }
        protected List<ReptPickSerials> serial_list { get { return (List<ReptPickSerials>)Session["serial_list"]; } set { Session["serial_list"] = value; } }
        protected List<QoutationDetails> invoice_itemsQ { get { return (List<QoutationDetails>)Session["invoice_itemsQ"]; } set { Session["invoice_itemsQ"] = value; } }
        protected List<QoutationDetails> invoice_items_bindQ { get { return (List<QoutationDetails>)Session["invoice_items_bindQ"]; } set { Session["invoice_items_bindQ"] = value; } }

        private List<InvoiceVoucher> oInvoiceVouchers = null;
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }

        protected List<ImpCusdecItm> _ImpCusdecItm { get { return (List<ImpCusdecItm>)Session["_ImpCusdecItm"]; } set { Session["_ImpCusdecItm"] = value; } }


        protected MasterLocation _MasterLocation { get { return (MasterLocation)Session["_MasterLocation"]; } set { Session["_MasterLocation"] = value; } }

        protected bool IsGrn { get { Session["IsGrn"] = (Session["IsGrn"] == null) ? false : (bool)Session["IsGrn"]; return (bool)Session["IsGrn"]; } set { Session["IsGrn"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // _traList = new List<Transport>();

                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                clear();
                LoadDeliveroption();
                System.Web.UI.Page _page = new Page();
                bool _allowCurrentTrans = false;
                bool _checkBackDate = IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _page, Convert.ToDateTime(dtpDODate.Text).Date.ToString(), btnDoDAte, _lblcontrol, "m_Trans_Inventory_CustomerDeliveryOrder", out _allowCurrentTrans);

            }
            else
            {
                if (Session["TransportMode"] != null)
                {

                    //if (Session["TransportMode"].ToString() == "Show")
                    //{
                    //    popupTransport.Show();
                    //}
                    //else
                    //{
                    //    popupTransport.Hide();
                    //}
                }
            }
            rbnMainType.Items[0].Attributes.CssStyle.Add("margin-right", "35px");
        }

        #region Alerts
        protected void lbtnWGRN_Click(object sender, EventArgs e)
        {
            DivWarning.Visible = false;
            DivSuccess.Visible = false;
            DivInformation.Visible = false;
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            DivSuccess.Visible = false;
        }

        #endregion
        private bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
           // if (_isAllow == true)
           // {
                _imgcontrol.Visible = true;
                if (_bdt != null)
                {
                    _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                   // dtpDODate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
                    _lblcontrol.Text = _outmsg;
                }
          //  }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;

            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                    btnDoDAte.Visible = true;
                    dtpDODate.Visible = true;

                    btnDoDAte.Enabled = true;
                    dtpDODate.Enabled = true;
                }
                else
                {
                    btnDoDAte.Visible = true;
                    dtpDODate.Visible = true;
                    btnDoDAte.Enabled = true;
                    dtpDODate.Enabled = true;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            //_lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        #region Main Buttons

        //protected void lbtnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["GlbReportType"] = "SCM1_DO";
        //        if (Session["UserCompanyCode"].ToString() == "ABE")
        //        {
        //            Session["GlbReportName"] = "Outward_Docs_DO_ABE.rpt";
        //        }
        //        else
        //        {
        //            Session["GlbReportName"] = "Outward_Docs_DO.rpt";
        //        }

        //        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

        //        clsInventory obj = new clsInventory();
        //        obj.printOutwardDocs("Outward_Docs_DO.rpt", "DPS32-DO-18-00027");
        //        if (Session["UserCompanyCode"].ToString() == "ABE")
        //        {
        //            PrintPDF(targetFileName, obj._outdocDO_ABE);
        //        }
        //        else
        //        {
        //            PrintPDF(targetFileName, obj._outdocDO);
        //        }

        //        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        CHNLSVC.MsgPortal.SaveReportErrorLog("DO Print", "DeliveryOrder", ex.Message, Session["UserID"].ToString());
        //    }
        //}
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "1")
            {
                try
                {
                   

                    #region Back-Date check - DULAJ 2018-Feb-27

                    bool _checkBackDate = false;
                    BackDates _bdt = new BackDates();
                    bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", "m_Trans_Inventory_CustomerDeliveryOrder", Convert.ToDateTime(dtpDODate.Text).Date.ToString(), out _bdt);

                    if (_bdt != null)
                    {
                        if (Convert.ToDateTime(dtpDODate.Text).Date != DateTime.Now.Date)
                        {
                            if (_isAllow)
                            {
                                _checkBackDate = true;
                            }
                            else
                            {
                                dtpDODate.Enabled = true;
                                DisplayMessage("Selected date not allowed for transaction!");
                                dtpDODate.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (_bdt.Gad_alw_curr_trans)
                            {
                                _checkBackDate = true;
                            }
                            else
                            {
                                dtpDODate.Enabled = true;
                                DisplayMessage("Selected date not allowed for transaction!");
                                dtpDODate.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dtpDODate.Text).Date != DateTime.Now.Date)
                        {
                            dtpDODate.Enabled = true;
                            DisplayMessage("Selected date not allowed for transaction!");
                            dtpDODate.Focus();
                            return;
                        }
                    }

                    #endregion
                   
                    bool _isserialMaintans = (bool)Session["_isserialMaintan"];
                    if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                    {
                        DisplayMessage("Can not save re-called documents");
                        return;
                    }
                    //if (lblentryno.Text != null)
                    //{
                    //    List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
                    //    oCusdecItm0 = CHNLSVC.Financial.GET_CUSTDECITM_DOC(lblentryno.Text);
                    //    if (oCusdecItm0 != null)
                    //    {
                    //        if (oCusdecItm0.Count > 0)
                    //        {
                    //            foreach (InvoiceItem _invoiceItem in invoice_items)
                    //            {
                    //                var _filter = oCusdecItm0.Where(x => x.Cui_itm_cd == _invoiceItem.Sad_itm_cd).ToList();
                    //                if (_filter.Count == 0)
                    //                {
                    //                    DisplayMessage("entry Item details and DO item details mismatch", 1);
                    //                    return;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //if (txtVehicleNo.Text != "")
                    //{
                    //    bool _vehicle = validateVehicle(txtVehicleNo.Text);
                    //    if (_vehicle == false)
                    //    {
                    //        DisplayMessage("Please check vehicle number.");
                    //        return;
                    //    }
                    //}


                    foreach (GridViewRow item in dvDOItems.Rows)
                    {

                        Label Do_qty = (Label)item.FindControl("lblSad_do_qty");
                        Label Bond_qty = (Label)item.FindControl("lblCus_ITM_QTY");
                        Label Item = (Label)item.FindControl("lblSad_itm_cd");
                        Label Inv_qty = (Label)item.FindControl("lblSad_qty");
                        Label Pick_qty = (Label)item.FindControl("lblPickQty");

                        if (Convert.ToDecimal(Do_qty.Text) == Convert.ToDecimal(Inv_qty.Text))
                        {
                            if (Convert.ToDecimal(Pick_qty.Text) > 0)
                            {
                                string msg = "Already completed this item" + Item.Text;
                                DisplayMessage(msg);
                                return;
                            }
                        }

                        if (_MasterLocation.Ml_cate_1 == "DFS")
                        {
                            if (Convert.ToDecimal(Pick_qty.Text) > Convert.ToDecimal(Bond_qty.Text))
                            {
                                string msg = "pick qty is more than bond qty item. " + Item.Text;
                                DisplayMessage(msg);
                                return;
                            }
                        }



                    }

                    //if (_MasterLocation.Ml_cate_1 == "DFS")
                    //{
                    //    foreach (GridViewRow item in dvDOItems.Rows)
                    //    {

                    //        Label Do_qty = (Label)item.FindControl("lblSad_do_qty");
                    //        Label Bond_qty = (Label)item.FindControl("lblCus_ITM_QTY");
                    //        Label Item = (Label)item.FindControl("lblSad_itm_cd");
                    //        if (Convert.ToDecimal(Do_qty.Text) > Convert.ToDecimal(Bond_qty.Text))
                    //        {
                    //            string msg = "pick qty is more than bond qty item. " + Item.Text;
                    //            DisplayMessage(msg);
                    //            return;
                    //        }
                    //    }

                    //}
                    if (rbnMainType.SelectedIndex == 0)
                    {
                        #region MyRegion
                        MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustCode.Text, string.Empty, string.Empty, "C");


                        if (CheckServerDateTime() == false) return;

                        if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                        {
                            DisplayMessage("Select the invoice no");
                            return;
                        }

                        if (chkManualRef.Checked)
                            if (string.IsNullOrEmpty(txtManualRefNo.Text))
                            {
                                DisplayMessage("Please enter a valid manual reference #” ");
                                return;
                            }

                        if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "0";
                        if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                        if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                        int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, Convert.ToDateTime(dtpDODate.Text).Date);
                        if (resultDate > 0)
                        {
                            DisplayMessage("Delivery date should be greater than or equal to invoice date.");
                            return;
                        }
                                              

                        if (IsGiftVoucherAvailable())
                        {
                            if (gvSGv.Rows.Count <= 0)
                            {
                                DisplayMessage("Please verify the gift voucher.");
                                return;
                            }

                            if (IsGiftVoucherNAttachedItemTally() == false)
                                return;
                        }


                        if (dvDOItems.Rows.Count > 0)
                        {
                            for (int i = 0; i < dvDOItems.Rows.Count; i++)
                            {
                                GridViewRow dr = dvDOItems.Rows[i];
                                Label lblSad_isapp = (Label)dr.FindControl("lblSad_isapp");
                                Label lblSad_itm_cd = (Label)dr.FindControl("lblSad_itm_cd");
                                CheckBox chkSad_isapp = (CheckBox)dr.FindControl("chkSad_isapp");
                                CheckBox chkSad_iscovernote = (CheckBox)dr.FindControl("chkSad_iscovernote");

                                if (chkSad_isapp.Checked != true)
                                {
                                    DisplayMessage("For item code " + lblSad_itm_cd.Text.Trim() + ", is not approved for delivery!");
                                    return;
                                }
                                if (chkSad_iscovernote.Checked != true)
                                {
                                    DisplayMessage("For item code " + lblSad_itm_cd.Text.Trim() + ", still not issue cover note!");
                                    return;
                                }
                            }
                        }

                        List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                        InventoryHeader invHdr = new InventoryHeader();
                        string documntNo = "";
                        Int32 result = -99;

                        Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), lblInvoiceNo.Text, 0);
                        reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "DO");

                        if (reptPickSerialsList == null)
                        {
                            if ((_SOANOList == null) || (_SOANOList.Count == 0))
                            {
                                _SOANOList = new List<string>();
                                List<INT_REQ> INT_REQ = new List<INT_REQ>();
                                List<InventoryRequestItem> _reqItem = new List<InventoryRequestItem>();
                                INT_REQ = CHNLSVC.Inventory.GETREQBY_REF(_sono, Session["UserCompanyCode"].ToString(), "SOA", null, 0);
                                if (INT_REQ != null)
                                {
                                    foreach (INT_REQ item in INT_REQ)
                                    {
                                        string _soanonew = item.ITR_REQ_NO;
                                        _SOANOList.Add(_soanonew);
                                    }
                                }
                            }

                            if (_SOANOList != null)
                            {
                                if (_SOANOList.Count > 0)
                                {
                                    foreach (string _f in _SOANOList)
                                    {

                                        string _SOA = _f;//Session["SONUMBER"].ToString();
                                        Int32 _userSeqNo1 = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("SOA", Session["UserCompanyCode"].ToString(), _SOA, 0);
                                        if (reptPickSerialsList == null)
                                        {
                                            reptPickSerialsList = new List<ReptPickSerials>();
                                        }
                                        if (reptPickSerialsList.Count == 0)
                                        {
                                            reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo1, "SOA");
                                        }
                                        else
                                        {
                                            List<ReptPickSerials> reptPickSerialsListTEMP = new List<ReptPickSerials>();
                                            reptPickSerialsListTEMP = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo1, "SOA");
                                            if (reptPickSerialsListTEMP != null)
                                            {
                                                if (reptPickSerialsListTEMP.Count > 0)
                                                {
                                                    reptPickSerialsList.AddRange(reptPickSerialsListTEMP);

                                                }

                                            }

                                        }

                                        if (reptPickSerialsList == null)
                                        {
                                            DisplayMessage("No delivery items found!");
                                            return;
                                        }
                                    }
                                }

                            }
                        }

                        #region Check Registration Txn Serials
                        bool _isRegTxnFound = false;
                        List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                        _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), lblInvoiceNo.Text, String.Empty);
                        if (_tmpReg != null && _tmpReg.Count > 0) _isRegTxnFound = true;

                        if (_isRegTxnFound == true)
                        {
                            foreach (ReptPickSerials _serRow in reptPickSerialsList)
                            {
                                MasterItem _itm = CHNLSVC.Inventory.GetItem("", _serRow.Tus_itm_cd);
                                if (_itm.Mi_need_reg == true)
                                {
                                    int _countReg = _tmpReg.Where(x => x.P_srvt_itm_cd == _serRow.Tus_itm_cd && x.P_svrt_engine == _serRow.Tus_ser_1).Count();
                                    if (_countReg <= 0)
                                    {
                                        DisplayMessage("Invalid delivery item/serial [" + _serRow.Tus_itm_cd + "] - [" + _serRow.Tus_ser_1 + "]");
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Check Reference Date and the Doc Date

                        if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                        {
                            return;
                        }

                        #endregion Check Reference Date and the Doc Date

                        #region Check Duplicate Serials

                        var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                        if (_isserialMaintans)
                        {
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
                                DisplayMessage("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                                return;
                            }
                        }
                        #endregion Check Duplicate Serials

                        #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                        //add by darshana on 12-Mar-2014 - To Gold operation totally operate as consignment base and no need to generate grn.
                        MasterCompany _masterComp = null;
                        _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

                        if (_masterComp.Mc_anal13 == 0)
                        {
                            if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(dtpDODate.Text).Date, reptPickSerialsList, out documntNo) < 0)
                            {
                                DisplayMessage(documntNo);
                                return;
                            }
                        }

                        //add by Chamal on 22-Aug-2014
                        var _consSupp = from _ListConsSupp in reptPickSerialsList
                                        where _ListConsSupp.Tus_itm_stus == "CONS"
                                        group _ListConsSupp by new { _ListConsSupp.Tus_orig_supp } into list
                                        select new { supp = list.Key.Tus_orig_supp };
                        foreach (var listsSupp in _consSupp)
                        {
                            MasterBusinessEntity _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), listsSupp.supp.ToString(), null, null, "S");
                            if (_supDet == null)
                            {
                                DisplayMessage("Cannot find supplier details.");
                                return;
                            }
                        }

                        #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                        List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                        reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DO");

                        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        foreach (DataRow r in dt_location.Rows)
                        {
                            // Get the value of the wanted column and cast it to string
                            invHdr.Ith_sbu = (string)r["ML_OPE_CD"];
                            if (System.DBNull.Value != r["ML_CATE_2"])
                            {
                                invHdr.Ith_channel = (string)r["ML_CATE_2"];
                            }
                            else
                            {
                                invHdr.Ith_channel = string.Empty;
                            }
                            invHdr.Ith_cate_tp = (string)r["ML_CATE_1"];
                        }

                        #region Fill DO Header

                        invHdr.Ith_loc = Session["UserDefLoca"].ToString();
                        invHdr.Ith_com = Session["UserCompanyCode"].ToString();
                        invHdr.Ith_doc_tp = "DO";
                        invHdr.Ith_doc_date = Convert.ToDateTime(dtpDODate.Text).Date;
                        invHdr.Ith_doc_year = Convert.ToDateTime(dtpDODate.Text).Year;

                        //invHdr.Ith_cate_tp = _invoiceType;

                        invHdr.Ith_sub_tp = "DPS";
                        invHdr.Ith_is_manual = false;
                        invHdr.Ith_stus = "A";
                        invHdr.Ith_cre_by = Session["UserID"].ToString();
                        invHdr.Ith_mod_by = Session["UserID"].ToString();
                        invHdr.Ith_direct = false;
                        invHdr.Ith_session_id = Session["SessionID"].ToString();
                        invHdr.Ith_manual_ref = txtManualRefNo.Text;
                        invHdr.Ith_vehi_no = txtVehicleNo.Text;
                        invHdr.Ith_remarks = txtRemarks.Text;
                        invHdr.Ith_anal_1 = _userSeqNo.ToString();
                        invHdr.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                        invHdr.Ith_entry_no = lblentryno.Text;//lblInvoiceNo.Text.ToString();
                        invHdr.Ith_bus_entity = txtCustCode.Text;
                        invHdr.Ith_del_party = txtCustName.Text; //ass per the dharshana 26Sep2017
                        invHdr.Ith_del_add1 = txtCustAddress1.Text;
                        invHdr.Ith_del_add2 = txtCustAddress2.Text;
                        invHdr.Ith_acc_no = _accNo;
                        invHdr.Ith_pc = _profitCenter;
                      

                        #endregion Fill DO Header

                        MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                        masterAutoNum.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        masterAutoNum.Aut_cate_tp = "LOC";
                        masterAutoNum.Aut_direction = 0;
                        masterAutoNum.Aut_moduleid = "DO";
                        masterAutoNum.Aut_start_char = "DO";
                        masterAutoNum.Aut_year = Convert.ToDateTime(dtpDODate.Text).Year;
                        List<ReptPickSerials> reptPickSerialsListGRN = new List<ReptPickSerials>();
                        List<ReptPickSerialsSub> reptPickSubSerialsListGRN = new List<ReptPickSerialsSub>();
                        if (reptPickSerialsList != null)
                        { reptPickSerialsListGRN = reptPickSerialsList; }

                        if (reptPickSubSerialsList != null)
                        { reptPickSubSerialsListGRN = reptPickSubSerialsList; }

                        InventoryHeader _invHeaderGRN = null;
                        MasterAutoNumber _masterAutoGRN = null;
                        string documntNoGRN = "";
                        if (IsGrn)
                        {
                            _invHeaderGRN = new InventoryHeader();
                            _invHeaderGRN.Ith_com = Session["UserCompanyCode"].ToString();
                            _invHeaderGRN.Ith_loc = Session["UserDefLoca"].ToString();
                            _invHeaderGRN.Ith_doc_date = Convert.ToDateTime(dtpDODate.Text).Date;
                            _invHeaderGRN.Ith_doc_year = Convert.ToDateTime(dtpDODate.Text).Date.Year;
                            _invHeaderGRN.Ith_direct = true;
                            _invHeaderGRN.Ith_doc_tp = "GRN";
                            _invHeaderGRN.Ith_cate_tp = "NOR";
                            _invHeaderGRN.Ith_sub_tp = "LOCAL";
                            //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                            _invHeaderGRN.Ith_is_manual = true;
                            _invHeaderGRN.Ith_manual_ref = txtManualRefNo.Text;
                            _invHeaderGRN.Ith_remarks = txtRemarks.Text;
                            _invHeaderGRN.Ith_stus = "A";
                            _invHeaderGRN.Ith_cre_by = Session["UserID"].ToString();
                            _invHeaderGRN.Ith_cre_when = DateTime.Now;
                            _invHeaderGRN.Ith_mod_by = Session["UserID"].ToString();
                            _invHeaderGRN.Ith_mod_when = DateTime.Now;
                            _invHeaderGRN.Ith_session_id = Session["SessionID"].ToString();
                            _invHeaderGRN.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                            _invHeaderGRN.Ith_entry_no = lblInvoiceNo.Text.ToString();

                            _masterAutoGRN = new MasterAutoNumber();
                            _masterAutoGRN.Aut_cate_cd = Session["UserDefLoca"].ToString();
                            _masterAutoGRN.Aut_cate_tp = "LOC";
                            _masterAutoGRN.Aut_direction = null;
                            _masterAutoGRN.Aut_modify_dt = null;
                            _masterAutoGRN.Aut_moduleid = "GRN";
                            _masterAutoGRN.Aut_number = 0;
                            _masterAutoGRN.Aut_start_char = "GRN";
                            _masterAutoGRN.Aut_year = _invHeaderGRN.Ith_doc_date.Date.Year;

                            //result = CHNLSVC.Inventory.DeliveryOrder_Auto(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out documntNoGRN);
                        }

                        foreach (ReptPickSerials _serial in reptPickSerialsList)
                        {
                            _serial.Tus_base_doc_no = lblInvoiceNo.Text;
                        }


                        if (_MasterLocation.Ml_cate_1 == "DFS")
                        {
                            #region dfs
                            var _scanItemsNon = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_qty, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItemsNon)
                            {
                                MasterItem msitem = CHNLSVC.Inventory.GetItem(invHdr.Ith_com, itm.Peo.Tus_itm_cd);
                                if (msitem != null)
                                {
                                    if (msitem.Mi_is_ser1 == 0)
                                    {
                                        ReptPickSerials serial = reptPickSerialsList.Find(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd && x.Tus_itm_line == itm.Peo.Tus_itm_line);
                                        for (int i = 1; i < itm.Peo.Tus_qty; i++)
                                        {
                                            // _nonserialserialgenpath = true;
                                            serial.Tus_qty = 1;
                                            #region Add by lakshan issue fix ref update 2016 Sep 20

                                            ReptPickSerials _tmpSer = ReptPickSerials.CreateNewObject(serial);
                                            #endregion
                                            //if (reptPickSerialsList!=null)
                                            //{
                                            //    if (reptPickSerialsList.Count>0)
                                            //    {
                                            //        var _serAva = reptPickSerialsList.Where(c => c.Tus_itm_cd == _tmpSer.Tus_itm_cd
                                            //            && c.Tus_itm_stus == _tmpSer.Tus_itm_stus
                                            //            && c.Tus_ser_1 == "N/A"
                                            //            && c.Tus_base_doc_no == _tmpSer.Tus_base_doc_no
                                            //            && c.Tus_base_itm_line == _tmpSer.Tus_base_itm_line
                                            //            && c.Tus_job_no == _tmpSer.Tus_job_no
                                            //            && c.Tus_job_line == _tmpSer.Tus_job_line
                                            //            ).FirstOrDefault();
                                            //        if (_serAva!=null)
                                            //        {
                                            //            _serAva.Tus_qty = serial.Tus_qty;
                                            //        }
                                            //        else
                                            //        {
                                            //            reptPickSerialsList.Add(_tmpSer);
                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        reptPickSerialsList.Add(_tmpSer);
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    reptPickSerialsList.Add(_tmpSer);
                                            //}
                                            reptPickSerialsList.Add(_tmpSer);
                                        }
                                    }
                                }
                            }
                            if (_ImpCusdecItm.Count > 0)
                            {
                                var _bondItems = _ImpCusdecItm.GroupBy(x => new { x.Cui_itm_cd, x.Cui_qty, x.Cui_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                                foreach (var bonditm in _bondItems)
                                {
                                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                                    foreach (var itm in _scanItems)
                                    {
                                        MasterItem _itemdetail = new MasterItem();
                                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                        if (itm.Peo.Tus_itm_cd == bonditm.Peo.Cui_itm_cd)
                                        {
                                            int _qty = Convert.ToInt32(bonditm.Peo.Cui_qty);
                                            if (_itemdetail.Mi_is_ser1 == 1)
                                            {
                                                var _filter = reptPickSerialsList.Where(x => x.Tus_itm_cd == bonditm.Peo.Cui_itm_cd && x.Tus_isSelect == false).Take(_qty).ToList();
                                                // var firstFiveArrivals = reptPickSerialsList.OrderBy(i => i.ArrivalTime).Take(5);
                                                if (_filter != null)
                                                {

                                                    foreach (ReptPickSerials _serial in _filter)
                                                    {

                                                        if (_serial.Tus_batch_no == "")
                                                        {
                                                            var _filter2 = _ImpCusdecItm.Find(x => x.Cui_doc_no == bonditm.Peo.Cui_doc_no && x.Cui_itm_cd == _serial.Tus_itm_cd);
                                                            if (_filter2 != null)
                                                            {
                                                                _serial.Tus_job_no = _filter2.Cui_Bond_no; //bonditm.Peo.Cui_doc_no;//_filter2.Cui_Bond_no;
                                                                _serial.Tus_job_line = Convert.ToInt32(_filter2.Cui_line);
                                                                _serial.Tus_itri_line_no = Convert.ToInt32(_filter2.Tmp_itri_line);
                                                                if (_filter2.Cui_is_res)
                                                                {
                                                                    _serial.Tus_resqty = _serial.Tus_qty;
                                                                }
                                                            }

                                                            _serial.Tus_base_doc_no_1 = bonditm.Peo.Cui_doc_no;
                                                            _serial.Tus_isSelect = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // var _filter = reptPickSerialsList.Where(x => x.Tus_itm_cd == bonditm.Peo.Cui_itm_cd && x.Tus_isSelect == false);
                                                var _filter = reptPickSerialsList.Where(x => x.Tus_itm_cd == bonditm.Peo.Cui_itm_cd && x.Tus_isSelect == false).Take(_qty).ToList();
                                                if (_filter != null)
                                                {
                                                    foreach (ReptPickSerials _serial in _filter)
                                                    {

                                                        // _filter.Tus_qty = _filter.Tus_qty - _qty;
                                                        decimal value = _serial.Tus_qty - _qty;
                                                        if (_serial.Tus_qty == 0)
                                                        {
                                                            _serial.Tus_qty = value;
                                                        }
                                                        if (_serial.Tus_qty != 0)
                                                        {
                                                            // _filter.Tus_qty = value;
                                                            if (_serial.Tus_batch_no == "")
                                                            {
                                                                var _filter2 = _ImpCusdecItm.Find(x => x.Cui_doc_no == bonditm.Peo.Cui_doc_no && x.Cui_itm_cd == _serial.Tus_itm_cd);
                                                                if (_filter2 != null)
                                                                {
                                                                    _serial.Tus_job_no = _filter2.Cui_Bond_no;// bonditm.Peo.Cui_doc_no;// _filter2.Cui_Bond_no;no need chamal
                                                                    _serial.Tus_job_line = Convert.ToInt32(_filter2.Cui_line);
                                                                    _serial.Tus_itri_line_no = Convert.ToInt32(_filter2.Tmp_itri_line);
                                                                    if (_filter2.Cui_is_res)
                                                                    {
                                                                        _serial.Tus_resqty = _serial.Tus_qty;
                                                                    }
                                                                }

                                                                _serial.Tus_base_doc_no_1 = bonditm.Peo.Cui_doc_no;
                                                                _serial.Tus_isSelect = true;
                                                            }





                                                            //ReptPickSerials _newserial = new ReptPickSerials();
                                                            //_newserial.Tus_cre_by = Session["UserID"].ToString();
                                                            //_newserial.Tus_usrseq_no = _filter.Tus_usrseq_no;
                                                            //_newserial.Tus_seq_no = _filter.Tus_seq_no;
                                                            //_newserial.Tus_cre_by = _filter.Tus_cre_by;
                                                            //_newserial.Tus_base_doc_no = _filter.Tus_base_doc_no;
                                                            //_newserial.Tus_base_itm_line = _filter.Tus_base_itm_line;
                                                            //_newserial.Tus_itm_desc = _filter.Tus_itm_desc;
                                                            //_newserial.Tus_itm_model = _filter.Tus_itm_model;
                                                            //_newserial.Tus_com = _filter.Tus_com;
                                                            //_newserial.Tus_loc = _filter.Tus_loc;
                                                            //_newserial.Tus_bin = _filter.Tus_bin;
                                                            //_newserial.Tus_itm_cd = _filter.Tus_itm_cd;
                                                            //_newserial.Tus_itm_stus = _filter.Tus_itm_stus;
                                                            //_newserial.Tus_itm_line = _filter.Tus_itm_line + 1;
                                                            //_newserial.Tus_qty = _filter.Tus_qty;
                                                            //_newserial.Tus_ser_1 = "N/A";
                                                            //_newserial.Tus_ser_2 = "N/A";
                                                            //_newserial.Tus_ser_3 = "N/A";
                                                            //_newserial.Tus_ser_4 = "N/A";
                                                            //_newserial.Tus_ser_id = 0;
                                                            //_newserial.Tus_serial_id = "0";
                                                            //_newserial.Tus_unit_cost = 0;
                                                            //_newserial.Tus_unit_price = 0;
                                                            //_newserial.Tus_unit_price = 0;
                                                            //_newserial.Tus_job_no = _filter.Tus_job_no;
                                                            //_newserial.Tus_isSelect = true;
                                                            //_newserial.Tus_batch_no = _filter.Tus_batch_no;
                                                            //if (_newserial.Tus_batch_no == "")
                                                            //{
                                                            //    var _filter2 = _ImpCusdecItm.Find(x => x.Cui_doc_no == bonditm.Peo.Cui_doc_no && x.Cui_itm_cd == _newserial.Tus_itm_cd);
                                                            //    if (_filter2 != null)
                                                            //    {
                                                            //        _newserial.Tus_job_no = _filter2.Cui_Bond_no;// bonditm.Peo.Cui_doc_no;//_filter2.Cui_Bond_no;
                                                            //        _newserial.Tus_job_line = Convert.ToInt32(_filter2.Cui_line);
                                                            //        if (_filter2.Cui_is_res)
                                                            //        {
                                                            //            _newserial.Tus_resqty = _newserial.Tus_qty;
                                                            //        }
                                                            //    }

                                                            //    _newserial.Tus_base_doc_no_1 = bonditm.Peo.Cui_doc_no;
                                                            //    _newserial.Tus_isSelect = true;
                                                            //}
                                                            //reptPickSerialsList.Add(_newserial);
                                                        }
                                                        else
                                                        {
                                                            _serial.Tus_qty = _qty;
                                                            if (_serial.Tus_batch_no == "")
                                                            {
                                                                var _filter2 = _ImpCusdecItm.Find(x => x.Cui_doc_no == bonditm.Peo.Cui_doc_no && x.Cui_itm_cd == _serial.Tus_itm_cd);
                                                                if (_filter2 != null)
                                                                {
                                                                    _serial.Tus_job_no = _filter2.Cui_Bond_no;// bonditm.Peo.Cui_doc_no;// _filter2.Cui_Bond_no;no need chamal
                                                                    _serial.Tus_job_line = Convert.ToInt32(_filter2.Cui_line);
                                                                    _serial.Tus_itri_line_no = Convert.ToInt32(_filter2.Tmp_itri_line);
                                                                    if (_filter2.Cui_is_res)
                                                                    {
                                                                        _serial.Tus_resqty = _serial.Tus_qty;
                                                                    }
                                                                }

                                                                _serial.Tus_base_doc_no_1 = bonditm.Peo.Cui_doc_no;
                                                                _serial.Tus_isSelect = true;
                                                            }

                                                        }

                                                    }
                                                }


                                            }
                                            // int qty = 0;

                                            //if (itm.theCount >= bonditm.theCount)
                                            //{
                                            //    qty = bonditm.theCount;
                                            //}
                                            //else
                                            //{
                                            //    qty = bonditm.theCount - itm.theCount;
                                            //}
                                            //if (_itemdetail.)
                                            //  qty = itm.theCount;

                                            //foreach (ReptPickSerials _serial in reptPickSerialsList)
                                            //{
                                            //    if (itm.Peo.Tus_itm_cd == _serial.Tus_itm_cd)
                                            //    {
                                            //            if (_serial.Tus_batch_no == "")
                                            //            {
                                            //                var _filter = _ImpCusdecItm.Find(x => x.Cui_doc_no == bonditm.Peo.Cui_doc_no && x.Cui_itm_cd == _serial.Tus_itm_cd);
                                            //                if (_filter != null)
                                            //                {
                                            //                    _serial.Tus_job_no = _filter.Cui_Bond_no;
                                            //                    _serial.Tus_job_line = Convert.ToInt32(_filter.Cui_line);
                                            //                    if (_filter.Cui_is_res)
                                            //                    {
                                            //                        _serial.Tus_resqty = _serial.Tus_qty;
                                            //                    }
                                            //                }


                                            //            }

                                            //                _serial.Tus_base_doc_no_1 = bonditm.Peo.Cui_doc_no;

                                            //    }
                                            //}
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DisplayMessage("Entry no missing");
                                return;
                            }
                            // var _II = reptPickSerialsList.Where(x => x.Tus_job_no == "").ToList();
                            // reptPickSerialsList = reptPickSerialsList.Where(x => x.Tus_job_no != "").ToList();
                            #endregion
                        }
                        else
                        {
                            //var _scanItemsNon = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_qty, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            //foreach (var itm in _scanItemsNon)
                            //{
                            //    MasterItem msitem = CHNLSVC.Inventory.GetItem(invHdr.Ith_com, itm.Peo.Tus_itm_cd);
                            //    if (msitem != null)
                            //    {
                            //        if (msitem.Mi_is_ser1 == 0)
                            //        {
                            //            ReptPickSerials serial = reptPickSerialsList.Find(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd && x.Tus_itm_line == itm.Peo.Tus_itm_line);
                            //            for (int i = 1; i < itm.Peo.Tus_qty; i++)
                            //            {
                            //                // _nonserialserialgenpath = true;
                            //                serial.Tus_qty = 1;
                            //                #region Add by lakshan issue fix ref update 2016 Sep 20

                            //                ReptPickSerials _tmpSer = ReptPickSerials.CreateNewObject(serial);
                            //                #endregion
                            //                reptPickSerialsList.Add(_tmpSer);
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        //MasterProfitCenter _MasterProfitCenter = (MasterProfitCenter)Session["MasterProfitCenter_1"];
                        //foreach (GridViewRow item in dvDOItems.Rows)
                        //{
                        //    Label _itm_cd = (Label)item.FindControl("lblSad_itm_cd");
                        //    Label _line = (Label)item.FindControl("lblsad_itm_line");
                        //    Label _warr_period = (Label)item.FindControl("lblSad_warr_period");
                        //    Label _warr_remarks = (Label)item.FindControl("lblSad_warr_remarks");
                        //    var _Filtr = reptPickSerialsList.Where(x => x.Tus_itm_cd == _itm_cd.Text && x.Tus_base_itm_line == Convert.ToInt32(_line.Text)).ToList();
                        //    if (_Filtr != null)
                        //    {
                        //        foreach (ReptPickSerials _serial in _Filtr)
                        //        {
                        //            int extendwarr = 0;
                        //            if (_MasterProfitCenter.Mpc_wara_extend > 0)
                        //            {
                        //                extendwarr = _MasterProfitCenter.Mpc_wara_extend;
                        //                _serial.Tus_warr_period = Convert.ToInt32(_warr_period.Text) + extendwarr;

                        //                decimal year = _serial.Tus_warr_period / 12;

                        //                _serial.Tus_Warranty_Remark = year + "Year Comprehensive Warranty";
                        //            }
                        //            else
                        //            {
                        //            _serial.Tus_warr_period = Convert.ToInt32(_warr_period.Text);
                        //            _serial.Tus_Warranty_Remark = _warr_remarks.Text;
                        //            }


                        //        }

                        //    }
                        //}

                        foreach (ReptPickSerials _serial in reptPickSerialsList)
                        {
                            MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _serial.Tus_itm_cd);
                            if (_itemdetail != null)
                            {
                                _serial.Tus_itm_desc = _itemdetail.Mi_longdesc;
                                _serial.Tus_itm_model = _itemdetail.Mi_model;
                                _serial.Tus_itm_brand = _itemdetail.Mi_brand;
                            }
                        }
                        #region Check Scan Completed
                        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
                        _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_doc_no = invHdr.Ith_oth_docno,
                            Tuh_doc_tp = "DO",
                            Tuh_direct = false,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                        }).FirstOrDefault();
                        if (_tmpPickHdr != null)
                        {
                            if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                            {
                                bool _isPdaComplete = false;
                                if (_tmpPickHdr.Tuh_fin_stus == 1 || _tmpPickHdr.Tuh_fin_stus == 2)
                                {
                                    _isPdaComplete = true;
                                }
                                if (!_isPdaComplete)
                                {
                                    lbtnSave.Enabled = true;
                                    lbtnSave.CssClass = "buttonUndocolorLeft floatRight";
                                    lbtnSave.OnClientClick = "SaveConfirm();";
                                    DisplayMessage("Scanning is not completed for the selected document !"); return;
                                }
                            }
                        }
                        #endregion

                        if (_SOANOList == null)
                        {
                            _SOANOList = new List<string>();
                        }
                        if (_SOANOList.Count == 0)
                        {

                            List<INT_REQ> INT_REQ = new List<INT_REQ>();
                            List<InventoryRequestItem> _reqItem = new List<InventoryRequestItem>();
                            INT_REQ = CHNLSVC.Inventory.GETREQBY_REF(_sono, Session["UserCompanyCode"].ToString(), "SOA", null, 0);
                            if (INT_REQ != null)
                            {
                                foreach (INT_REQ item in INT_REQ)
                                {
                                    string _soanonew = item.ITR_REQ_NO;
                                    _SOANOList.Add(_soanonew);
                                }
                            }
                        }
                        bool founditem = false;
                        var _scanItemslist = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItemslist)
                        {
                            founditem = false;
                            foreach (GridViewRow item in dvDOItems.Rows)
                            {
                                if (founditem == false)
                                {
                                    Label _itm_cd = (Label)item.FindControl("lblSad_itm_cd");
                                    Label _simiitem = (Label)item.FindControl("lblSad_sim_itm_cd");
                                    if ((_itm_cd.Text == itm.Peo.Tus_itm_cd) || (_simiitem.Text == itm.Peo.Tus_itm_cd))
                                    {
                                        founditem = true;
                                    }
                                }
                            }
                            if (founditem == false)
                            {
                                DisplayMessage("Invoice item and scan item mismatch.please check serial " + itm.Peo.Tus_itm_cd);
                                return;
                            }
                        }

                        invHdr.TMP_CHK_SER_IS_AVA = true;
                        //invHdr.UpdateResLog = true;
                        invHdr.TMP_IS_ALLOCATION = true;
                        invHdr.TMP_IS_ALERT = false;
                        invHdr.UpdateResLog = true;
                        List<InvoiceItem> _invItms = CHNLSVC.Sales.GetAllInvoiceItems(invHdr.Ith_oth_docno);
                        if (_invItms != null)
                        {
                            var _invItmsWithRes = _invItms.Where(c => !string.IsNullOrEmpty(c.Sad_res_no) && c.Sad_res_no != "N/A").ToList();
                            if (_invItmsWithRes != null)
                            {
                                foreach (var item in _invItmsWithRes)
                                {
                                    foreach (var _pickSer in reptPickSerialsList)
                                    {
                                        if (_pickSer.Tus_itm_cd == item.Sad_itm_cd && _pickSer.Tus_base_itm_line == item.Sad_itm_line)
                                        {
                                            _pickSer.Tus_resqty = _pickSer.Tus_qty;
                                        }
                                    }
                                }
                            }
                        }
                        HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("LOC", invHdr.Ith_loc, "TRANSREQD", DateTime.Now.Date);
                        if (_sysPara != null)
                        {
                            if (_sysPara.Hsy_val == 1)
                            {
                                if (_traListDo.Count == 0)
                                {
                                    DisplayMessage("Please enter transport data !");
                                    return;
                                }
                            }
                        }
                        var _zeroRefserialList = reptPickSerialsList.Where(c => c.Tus_base_itm_line == 0).ToList();
                        if (_zeroRefserialList != null)
                        {
                            if (_zeroRefserialList.Count > 0)
                            {
                                DisplayMessage("Base reference data is invalid !"); return;
                            }
                        }
                        invHdr.TMP_PROJECT_NAME = "SCMWEB";
                        invHdr.TmpValidateInvDo = true;
                        #region add reservatin qty update when SOA available 30 jun 2017
                        InvoiceHeader _satHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(invHdr.Ith_oth_docno);
                        if (_satHdr != null)
                        {
                            if (!string.IsNullOrEmpty(_satHdr.Sah_ref_doc))
                            {
                                List<InventoryRequestItem> _soaItmList = CHNLSVC.Inventory.GetSOAItemDataByInvoice(_satHdr.Sah_ref_doc);
                                if (_soaItmList.Count > 0)
                                {
                                    foreach (var item in reptPickSerialsList)
                                    {
                                        var _soaAva = _soaItmList.Where(c => c.Itri_itm_cd == item.Tus_itm_cd && c.Itri_itm_stus == item.Tus_itm_stus).FirstOrDefault();
                                        if (_soaAva != null)
                                        {
                                            item.Tus_resqty = 1;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        invHdr.Ith_gen_frm = "SCMWEB";
                        invHdr.Ith_sub_docno = _satHdr.Sah_ref_doc;//added by Wiaml @ 29/06/2018 to Show invoice DocRef on movement Audit Trial
                        foreach (var item in _traListDo)
                        {
                            item.itrn_isactive = 1;
                        }
                        //Added By Dulaj to Save delivary customer details
                        if (_satHdr.Sah_d_cust_cd != null && _satHdr.Sah_d_cust_name != null && _satHdr.Sah_d_cust_add1 != null && _satHdr.Sah_d_cust_add2 != null)
                        {
                            if (_satHdr.Sah_d_cust_cd != "" && _satHdr.Sah_d_cust_name != "" && _satHdr.Sah_d_cust_add1 != "" && _satHdr.Sah_d_cust_add2 != "")
                            {
                                invHdr.Ith_del_code = _satHdr.Sah_d_cust_cd;
                                invHdr.Ith_del_party = _satHdr.Sah_d_cust_name;
                                invHdr.Ith_del_add1 = _satHdr.Sah_d_cust_add1;
                                invHdr.Ith_del_add2 = _satHdr.Sah_d_cust_add2;
                            }
                        }
                        result = CHNLSVC.Inventory.DeliveryOrderEntry(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out  documntNoGRN, IsGrn, null, _traListDo, _SOANOList);

                        if (result != -99 && result >= 0)
                        {
                            string m = "Delivery Order Note Successfully Saved! Document No : " + documntNo;
                            DisplayMessage(m, 3);
                            //added by dilshan on 04-09-2018******
                            CHNLSVC.MsgPortal.GenarateDOSMS(Session["UserCompanyCode"].ToString(), documntNo, txtCustCode.Text, lblInvoiceNo.Text);
                            //************************************
                            Session["documntNo"] = documntNo;
                            _sono = null;
                            IsSO_base = false;
                            Issoa_select = false;
                            _soano = string.Empty;
                            _resqty = "0";
                            _SOANOList = new List<string>();
                            oMstCom = CHNLSVC.General.GetCompByCode(Convert.ToString(Session["UserCompanyCode"]));
                            txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
                            txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                            Session["SONUMBER"] = "";
                            _ImpCusdecItm = new List<ImpCusdecItm>();
                            Session["TransportMode"] = null;
                            hdfIsRecalled.Value = "0";

                            txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                            txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
                            txtFindCustomer.Text = string.Empty;
                            txtFindInvoiceNo.Text = string.Empty;
                            chkDelFrmAnyLoc.Checked = false;
                            txtDocumentNo.Text = string.Empty;
                            ucOutScan.doc_tp = "DO";
                            ucOutScan.isApprovalSend = false;

                            ucOutScan.adjustmentTypeValue = "-";
                            ucOutScan.PNLTobechange.Visible = false;
                            dvPendingInvoices.DataSource = new int[] { };
                            dvPendingInvoices.DataBind();

                            dtpDODate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                            txtInvcNo.Text = string.Empty;
                            txtInvcDate.Text = string.Empty;

                            txtManualRefNo.Text = string.Empty;
                            txtVehicleNo.Text = string.Empty;
                            txtCustCode.Text = string.Empty;
                            txtCustName.Text = string.Empty;
                            txtCustAddress1.Text = string.Empty;
                            txtCustAddress2.Text = string.Empty;
                            txtRemarks.Text = string.Empty;
                            lblentryno.Text = string.Empty;
                            lblInvoiceNo.Text = string.Empty;
                            serial_list = new List<ReptPickSerials>();
                            invoice_items = new List<InvoiceItem>();

                            dvDOItems.DataSource = new int[] { };
                            dvDOItems.DataBind();

                            gvGiftVoucher.DataSource = new int[] { };
                            gvGiftVoucher.DataBind();

                            dvDOSerials.DataSource = new int[] { };
                            dvDOSerials.DataBind();

                            gvSGv.DataSource = new int[] { };
                            gvSGv.DataBind();
                            dgvTrns.DataSource = new int[] { };
                            dgvTrns.DataBind();
                            _traListDo = new List<Transport>();
                            _print();
                            //lblMssg.Text = "Do you want print now?";
                            // PopupConfBox.Show();


                            // clear();
                        }
                        else
                        {
                            if (documntNoGRN.Contains("CHK_INLFREEQTY"))
                            {
                                DisplayMessageNew("Free Qty is not available in location :" + Session["UserDefLoca"].ToString(), 1);
                            }
                            else if (documntNoGRN.Contains("NO_STOCK_BALANCE"))
                            {
                                DisplayMessageNew("Stock balance is not available in location :" + Session["UserDefLoca"].ToString(), 1);
                            }
                            else if (documntNoGRN.Contains("CHK_INLRESQTY"))
                            {
                                DisplayMessage("There is no reserved stock available. Please check the stock balances.");
                            }
                            else if (documntNoGRN.Contains("CHK_INLQTY"))
                            {
                                DisplayMessage("There is no stock available. Please check the stock balances.");
                            }
                            else if (documntNoGRN.Contains("PUR_HDR.POH_SUPP"))
                            {
                                DisplayMessage("Please check the supplier codes in picked serials !");
                            }
                            else if (documntNoGRN.Contains("REFLINE") || documntNoGRN.Contains("quantity"))
                            {
                                DisplayMessage(documntNoGRN);
                            }
                            else
                            {
                                DisplayMessageNew(documntNoGRN, 4);
                            }
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        }
                        #endregion
                    }
                    else
                    {

                        ///////////////////////////////////////////
                        //
                        //
                        //
                        //////////////////////////////////////////

                        if (CheckServerDateTime() == false) return;

                        if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                        {
                            DisplayMessage("Select the Quotation no");
                            return;
                        }

                        if (chkManualRef.Checked)
                            if (string.IsNullOrEmpty(txtManualRefNo.Text))
                            {
                                DisplayMessage("You do not enter a valid manual document no.");
                                return;
                            }

                        if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "0";
                        if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                        if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                        int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, Convert.ToDateTime(dtpDODate.Text).Date);
                        if (resultDate > 0)
                        {
                            DisplayMessage("Delivery date should be greater than or equal to quotation date.");
                            return;
                        }

                   

                    



                        if (IsGiftVoucherAvailable())
                        {
                            if (gvSGv.Rows.Count <= 0)
                            {
                                DisplayMessage("Please verify the gift voucher.");
                                return;
                            }

                            if (IsGiftVoucherNAttachedItemTally() == false)
                                return;
                        }

                        List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                        InventoryHeader invHdr = new InventoryHeader();
                        string documntNo = "";
                        Int32 result = -99;

                        Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", Session["UserCompanyCode"].ToString(), lblInvoiceNo.Text, 0);
                        reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "QUO");

                        reptPickSerialsList.ForEach(x => x.Tus_session_id = Session["SessionID"].ToString());

                        if (reptPickSerialsList == null)
                        {
                            DisplayMessage("No delivery items found!");
                            return;
                        }
                        else
                        {
                            foreach (QoutationDetails _serRow in invoice_itemsQ)
                            {
                                reptPickSerialsList.Where(w => w.Tus_itm_cd == _serRow.Qd_itm_cd && w.Tus_base_itm_line == _serRow.Qd_line_no).ToList().ForEach(s => s.Tus_unit_price = _serRow.Qd_unit_price);
                            }
                        }

                        #region Check Registration Txn Serials

                        bool _isRegTxnFound = false;
                        List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                        _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), lblInvoiceNo.Text, String.Empty);
                        if (_tmpReg != null && _tmpReg.Count > 0) _isRegTxnFound = true;

                        if (_isRegTxnFound == true)
                        {
                            foreach (ReptPickSerials _serRow in reptPickSerialsList)
                            {
                                MasterItem _itm = CHNLSVC.Inventory.GetItem("", _serRow.Tus_itm_cd);
                                if (_itm.Mi_need_reg == true)
                                {
                                    int _countReg = _tmpReg.Where(x => x.P_srvt_itm_cd == _serRow.Tus_itm_cd && x.P_svrt_engine == _serRow.Tus_ser_1).Count();
                                    if (_countReg <= 0)
                                    {
                                        DisplayMessage("Invalid delivery item/serial [" + _serRow.Tus_itm_cd + "] - [" + _serRow.Tus_ser_1 + "]");
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Check Reference Date and the Doc Date

                        if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                        {
                            return;
                        }

                        #endregion Check Reference Date and the Doc Date

                        #region Check Duplicate Serials

                        var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

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
                            DisplayMessage("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                            return;
                        }

                        #endregion Check Duplicate Serials

                        #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                        //add by darshana on 12-Mar-2014 - To Gold operation totally operate as consignment base and no need to generate grn.
                        MasterCompany _masterComp = null;
                        _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

                        if (_masterComp.Mc_anal13 == 0)
                        {
                            if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(dtpDODate.Text).Date, reptPickSerialsList, out documntNo) < 0)
                            {
                                DisplayMessage(documntNo);
                                return;
                            }
                        }

                        //add by Chamal on 22-Aug-2014
                        var _consSupp = from _ListConsSupp in reptPickSerialsList
                                        where _ListConsSupp.Tus_itm_stus == "CONS"
                                        group _ListConsSupp by new { _ListConsSupp.Tus_orig_supp } into list
                                        select new { supp = list.Key.Tus_orig_supp };
                        foreach (var listsSupp in _consSupp)
                        {
                            MasterBusinessEntity _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), listsSupp.supp.ToString(), null, null, "S");
                            if (_supDet == null)
                            {
                                DisplayMessage("Cannot find supplier details.");
                                return;
                            }
                        }

                        #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                        List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                        reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "QUO");

                        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        foreach (DataRow r in dt_location.Rows)
                        {
                            // Get the value of the wanted column and cast it to string
                            invHdr.Ith_sbu = (string)r["ML_OPE_CD"];
                            if (System.DBNull.Value != r["ML_CATE_2"])
                            {
                                invHdr.Ith_channel = (string)r["ML_CATE_2"];
                            }
                            else
                            {
                                invHdr.Ith_channel = string.Empty;
                            }
                        }

                        #region Fill DO Header

                        invHdr.Ith_loc = Session["UserDefLoca"].ToString();
                        invHdr.Ith_com = Session["UserCompanyCode"].ToString();
                        invHdr.Ith_doc_tp = "DO";
                        invHdr.Ith_doc_date = Convert.ToDateTime(dtpDODate.Text).Date;
                        invHdr.Ith_doc_year = Convert.ToDateTime(dtpDODate.Text).Date.Year;
                        invHdr.Ith_cate_tp = _invoiceType;
                        invHdr.Ith_sub_tp = "DPS";
                        invHdr.Ith_is_manual = false;
                        invHdr.Ith_stus = "A";
                        invHdr.Ith_cre_by = Session["UserID"].ToString();
                        invHdr.Ith_mod_by = Session["UserID"].ToString();
                        invHdr.Ith_direct = false;
                        invHdr.Ith_session_id = Session["SessionID"].ToString();//Session["SessionID"].ToString();
                        invHdr.Ith_manual_ref = txtManualRefNo.Text;
                        invHdr.Ith_vehi_no = txtVehicleNo.Text;
                        invHdr.Ith_remarks = txtRemarks.Text;
                        invHdr.Ith_anal_1 = _userSeqNo.ToString();
                        invHdr.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                        invHdr.Ith_entry_no = lblentryno.Text;//lblInvoiceNo.Text.ToString();
                        invHdr.Ith_bus_entity = txtCustCode.Text;
                        invHdr.Ith_del_party = txtCustCode.Text; //As per the dharshana 26Sep2017
                        invHdr.Ith_del_add1 = txtCustAddress1.Text;
                        invHdr.Ith_del_add2 = txtCustAddress2.Text;
                        invHdr.Ith_acc_no = _accNo;
                        invHdr.Ith_pc = _profitCenter;
                        invHdr.Ith_sub_docno = lblInvoiceNo.Text.ToString();
                        #endregion Fill DO Header

                        MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                        masterAutoNum.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        masterAutoNum.Aut_cate_tp = "LOC";
                        masterAutoNum.Aut_direction = 0;
                        masterAutoNum.Aut_moduleid = "DO";
                        masterAutoNum.Aut_start_char = "DO";
                        masterAutoNum.Aut_year = Convert.ToDateTime(dtpDODate.Text).Date.Year;
                        List<ReptPickSerials> reptPickSerialsListGRN = new List<ReptPickSerials>();
                        List<ReptPickSerialsSub> reptPickSubSerialsListGRN = new List<ReptPickSerialsSub>();
                        if (reptPickSerialsList != null)
                        { reptPickSerialsListGRN = reptPickSerialsList; }

                        if (reptPickSubSerialsList != null)
                        { reptPickSubSerialsListGRN = reptPickSubSerialsList; }

                        InventoryHeader _invHeaderGRN = null;
                        MasterAutoNumber _masterAutoGRN = null;
                        string documntNoGRN = "";
                        if (IsGrn)
                        {
                            _invHeaderGRN = new InventoryHeader();
                            _invHeaderGRN.Ith_com = Session["UserCompanyCode"].ToString();
                            _invHeaderGRN.Ith_loc = Session["UserDefLoca"].ToString();
                            _invHeaderGRN.Ith_doc_date = Convert.ToDateTime(dtpDODate.Text).Date;
                            _invHeaderGRN.Ith_doc_year = Convert.ToDateTime(dtpDODate.Text).Date.Year;
                            _invHeaderGRN.Ith_direct = true;
                            _invHeaderGRN.Ith_doc_tp = "GRN";
                            _invHeaderGRN.Ith_cate_tp = "NOR";
                            _invHeaderGRN.Ith_sub_tp = "LOCAL";
                            //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                            _invHeaderGRN.Ith_is_manual = true;
                            _invHeaderGRN.Ith_manual_ref = txtManualRefNo.Text;
                            _invHeaderGRN.Ith_remarks = txtRemarks.Text;
                            _invHeaderGRN.Ith_stus = "A";
                            _invHeaderGRN.Ith_cre_by = Session["UserID"].ToString();
                            _invHeaderGRN.Ith_cre_when = DateTime.Now;
                            _invHeaderGRN.Ith_mod_by = Session["UserID"].ToString();
                            _invHeaderGRN.Ith_mod_when = DateTime.Now;
                            _invHeaderGRN.Ith_session_id = Session["SessionID"].ToString();
                            _invHeaderGRN.Ith_oth_docno = lblInvoiceNo.Text.ToString();
                            _invHeaderGRN.Ith_entry_no = lblInvoiceNo.Text.ToString();

                            _masterAutoGRN = new MasterAutoNumber();
                            _masterAutoGRN.Aut_cate_cd = Session["UserDefLoca"].ToString();
                            _masterAutoGRN.Aut_cate_tp = "LOC";
                            _masterAutoGRN.Aut_direction = null;
                            _masterAutoGRN.Aut_modify_dt = null;
                            _masterAutoGRN.Aut_moduleid = "GRN";
                            _masterAutoGRN.Aut_number = 0;
                            _masterAutoGRN.Aut_start_char = "GRN";
                            _masterAutoGRN.Aut_year = _invHeaderGRN.Ith_doc_date.Year;
                        }

                        foreach (ReptPickSerials _serial in reptPickSerialsList)
                        {
                            MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _serial.Tus_itm_cd);
                            if (_itemdetail != null)
                            {
                                _serial.Tus_itm_desc = _itemdetail.Mi_longdesc;
                                _serial.Tus_itm_model = _itemdetail.Mi_model;
                                _serial.Tus_itm_brand = _itemdetail.Mi_brand;
                            }
                        }

                        foreach (var item in _traListDo)
                        {
                            item.itrn_isactive = 1;
                        }
                        invHdr.Ith_gen_frm = "SCMWEBQUT";
                        result = CHNLSVC.Inventory.DeliveryOrderEntryQuotation_Based(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN
                            , reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out  documntNoGRN, IsGrn, null, _traListDo);

                        if (result != -99 && result >= 0)
                        {
                            DisplayMessage("Delivery Order Note Successfully Saved! Document No : " + documntNo, 3);
                            //Added by dilshan on 04-09-2018*********
                            CHNLSVC.MsgPortal.GenarateDOSMS(Session["UserCompanyCode"].ToString(), documntNo, txtCustCode.Text, lblInvoiceNo.Text);
                            //***************************************
                            Session["documntNo"] = documntNo;

                            _print();
                            //lblMssg.Text = "Do you want print now?";
                            //PopupConfBox.Show();



                        }
                        else
                        {
                            DisplayMessage(documntNo.Replace("\n", "").Replace(":", " ").Replace("\"", " "), 4);
                        }
                    }

                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }

            }
        }


        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
                clear();
            }
        }

        #endregion


        #region Upper Controls
        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Customer";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show();
        }

        protected void txtFindCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindCustomer.Text)) return;

                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtFindCustomer.Text, string.Empty, string.Empty, "C");
                CHNLSVC.CloseAllChannels();
                if (_masterBusinessCompany.Mbe_cd == null)
                {
                    DisplayMessage("Please select the valid customer");
                    txtFindCustomer.Text = "";
                    txtFindCustomer.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message);
            }
        }

        protected void txtFindInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbnMainType.SelectedIndex == 0)
                {

                    if (string.IsNullOrEmpty(txtFindInvoiceNo.Text)) return;
                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtFindInvoiceNo.Text);
                    if (_hdr == null)
                    {
                        DisplayMessage("Please select the valid invoice no");
                        txtFindInvoiceNo.Text = string.Empty;
                        txtFindInvoiceNo.Focus();
                        return;
                    }
                    else
                    {
                        string _loguserloc = Session["UserDefLoca"].ToString();
                        if (_hdr.Sah_del_loc != _loguserloc)
                        {
                            string msg = "Invoice prefer delivery location is : " + _hdr.Sah_del_loc + " " + " Cannot deliver from your location.";
                            DisplayMessage(msg);
                            return;
                        }
                        else if (_hdr.Sah_stus == "D")
                        {
                            DisplayMessage("Invoice is already delivered.");
                            return;
                        }
                        else if (_hdr.Sah_stus == "C")
                        {
                            DisplayMessage("Invoice is cancelled invoice");
                            return;
                        }
                        btnGetInvoices_Click(null, null);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtFindInvoiceNo.Text)) return;

                    DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, txtFindCustomer.Text, txtFindInvoiceNo.Text, "A", Session["UserDefProf"].ToString());
                    if (dt == null)
                    {
                        DisplayMessage("Please select the valid quotation no");
                        txtFindInvoiceNo.Text = string.Empty;
                        txtFindInvoiceNo.Focus();
                        return;
                    }
                    else
                    {
                        btnGetInvoices_Click(null, null);
                    }
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
            }
        }

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            if (rbnMainType.SelectedIndex == 0)
            {
                ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable result = CHNLSVC.CommonSearch.GetInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InvSalesInvoice";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
            }
            else
            {
                ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable result = CHNLSVC.CommonSearch.GetQuotationAllWeb(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CustomerQuo";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show();
            }
        }

        protected void btnDocumentNo_Click(object sender, EventArgs e)
        {
            txtFDate.Text = DateTime.Now.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
            txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, DateTime.Now.AddMonths(-1).Date, DateTime.Now.Date);
            var sortedTable = result.AsEnumerable()
             .OrderByDescending(r => r.Field<String>("Document"))
             .CopyToDataTable();
            grdResultD.DataSource = sortedTable;
            grdResultD.DataBind();
            lblvalue.Text = "MovementDocDateSearch";
            BindUCtrlDDLData2(result);
            ViewState["SEARCH"] = result;
            UserDPopoup.Show();
            Session["DPopup"] = "DPopup";
            txtSearchbywordD.Focus();
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

        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "SaveConfirm();";
            lbtnSave.CssClass = "buttonUndocolor";

            try
            {
                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    //if (rbnMainType.SelectedIndex == 0)
                    //{
                    #region MyRegion
                    bool _invalidDoc = true;

                    #region Clear Data

                    txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
                    txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                    dtpDODate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");

                    DataTable dt = null;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;

                    chkManualRef.Checked = false;
                    chkEditAddress.Checked = false;
                    txtManualRefNo.Text = "";

                    lblBackDateInfor.Text = string.Empty;
                    lblInvoiceNo.Text = string.Empty;
                    lblInvoiceDate.Text = string.Empty;

                    txtCustAddress1.Text = string.Empty;
                    txtCustAddress2.Text = string.Empty;
                    txtCustCode.Text = string.Empty;
                    txtCustName.Text = string.Empty;
                    txtFindCustomer.Text = string.Empty;
                    txtFindInvoiceNo.Text = string.Empty;
                    txtVehicleNo.Text = string.Empty;
                    txtRemarks.Text = string.Empty;

                    List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                    _emptyserList = null;
                    dvDOSerials.AutoGenerateColumns = false;
                    dvDOSerials.DataSource = _emptyserList;

                    List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                    _emptyinvoiceitemList = null;
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = _emptyinvoiceitemList;

                    #endregion Clear Data

                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "DO")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        reCellQuatation();
                        return;

                        CHNLSVC.CloseAllChannels();
                        DisplayMessage("Invalid Document No!");
                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        btnGetInvoices.Enabled = false;
                        //lbtnSave.Enabled = false;
                        hdfIsRecalled.Value = "1";

                        dtpDODate.Text = _invHdr.Ith_doc_date.Date.ToString("dd/MMM/yyyy");
                        lblInvoiceNo.Text = _invHdr.Ith_oth_docno;
                        txtInvcNo.Text = lblInvoiceNo.Text;
                        txtCustCode.Text = _invHdr.Ith_bus_entity;
                        txtCustAddress1.Text = _invHdr.Ith_del_add1;
                        txtCustAddress1.ToolTip = _invHdr.Ith_del_add1;
                        txtCustAddress2.Text = _invHdr.Ith_del_add2;
                        txtManualRefNo.Text = _invHdr.Ith_manual_ref;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtRemarks.ToolTip = _invHdr.Ith_remarks;

                        InvoiceHeader _saleHdr = new InvoiceHeader();
                        _saleHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invHdr.Ith_oth_docno);
                        txtCustName.Text = _saleHdr.Sah_cus_name;
                        lblInvoiceDate.Text = _saleHdr.Sah_dt.Date.ToString("dd/MMM/yyyy");
                        txtInvcDate.Text = lblInvoiceDate.Text;
                    }

                    #endregion Check Valid Document No

                    #region Get Serials

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(Session["UserCompanyCode"].ToString(), _invHdr.Ith_pc, "INV", _invHdr.Ith_oth_docno, string.Empty);
                    if (invoice_items != null)
                    {
                        if (_serList != null)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                            foreach (var itm in _scanItems)
                            {
                                foreach (InvoiceItem _invItem in invoice_items)
                                    if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                    {
                                        //it.Sad_do_qty = q.theCount;
                                        //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                        _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                    }
                            }
                        }
                        setItemDescriptions(invoice_items);
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = invoice_items;
                        dvDOItems.DataBind();

                        setItemStatus2(_serList);
                        dvDOSerials.AutoGenerateColumns = false;
                        dvDOSerials.DataSource = _serList;
                        dvDOSerials.DataBind();
                    }
                    else
                    {
                        CHNLSVC.CloseAllChannels();
                        DisplayMessage("Item not found!");
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Focus();
                        return;
                    }

                    #endregion Get Serials
                    #endregion
                    //}
                    //else
                    //{
                    //    reCellQuatation();

                    //    #region MyRegion
                    //    //    bool _invalidDoc = true;

                    //    //    #region Clear Data

                    //    //    txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
                    //    //    txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
                    //    //    dtpDODate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");

                    //    //    DataTable dt = null;
                    //    //    dvPendingInvoices.AutoGenerateColumns = false;
                    //    //    dvPendingInvoices.DataSource = dt;
                    //    //    dvPendingInvoices.DataBind();

                    //    //    chkManualRef.Checked = false;
                    //    //    chkEditAddress.Checked = false;
                    //    //    txtManualRefNo.Text = "";

                    //    //    lblBackDateInfor.Text = string.Empty;
                    //    //    lblInvoiceNo.Text = string.Empty;
                    //    //    lblInvoiceDate.Text = string.Empty;

                    //    //    txtCustAddress1.Text = string.Empty;
                    //    //    txtCustAddress2.Text = string.Empty;
                    //    //    txtCustCode.Text = string.Empty;
                    //    //    txtCustName.Text = string.Empty;
                    //    //    txtFindCustomer.Text = string.Empty;
                    //    //    txtFindInvoiceNo.Text = string.Empty;
                    //    //    txtVehicleNo.Text = string.Empty;
                    //    //    txtRemarks.Text = string.Empty;

                    //    //    List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                    //    //    _emptyserList = null;
                    //    //    dvDOSerials.AutoGenerateColumns = false;
                    //    //    dvDOSerials.DataSource = _emptyserList;
                    //    //    dvDOSerials.DataBind();

                    //    //    List<QoutationDetails> _emptyinvoiceitemList = new List<QoutationDetails>();
                    //    //    _emptyinvoiceitemList = null;
                    //    //    dvDOItems.AutoGenerateColumns = false;
                    //    //    dvDOItems.DataSource = _emptyinvoiceitemList;
                    //    //    dvDOItems.DataBind();

                    //    //    #endregion Clear Data

                    //    //    InventoryHeader _invHdr = new InventoryHeader();
                    //    //    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

                    //    //    #region Check Valid Document No

                    //    //    if (_invHdr == null)
                    //    //    {
                    //    //        _invalidDoc = false;
                    //    //        goto err;
                    //    //    }
                    //    //    if (_invHdr.Ith_doc_tp != "DO")
                    //    //    {
                    //    //        _invalidDoc = false;
                    //    //        goto err;
                    //    //    }
                    //    //    if (_invHdr.Ith_cate_tp != "QUO")
                    //    //    {
                    //    //        _invalidDoc = false;
                    //    //        goto err;
                    //    //    }
                    //    //    if (_invHdr.Ith_direct == true)
                    //    //    {
                    //    //        _invalidDoc = false;
                    //    //        goto err;
                    //    //    }

                    //    //err:
                    //    //    if (_invalidDoc == false)
                    //    //    {
                    //    //        DisplayMessage("Invalid Document No!");
                    //    //        txtDocumentNo.Text = "";
                    //    //        txtDocumentNo.Focus();
                    //    //        return;
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        btnGetInvoices.Enabled = false;
                    //    //        //lbtnSave.Enabled = false;
                    //    //        hdfIsRecalled.Value = "1";

                    //    //        dtpDODate.Text = _invHdr.Ith_doc_date.Date.ToString("dd/MMM/yyyy");
                    //    //        lblInvoiceNo.Text = _invHdr.Ith_oth_docno;
                    //    //        txtInvcNo.Text = lblInvoiceNo.Text;
                    //    //        txtCustCode.Text = _invHdr.Ith_bus_entity;
                    //    //        txtCustAddress1.Text = _invHdr.Ith_del_add1;
                    //    //        txtCustAddress2.Text = _invHdr.Ith_del_add2;
                    //    //        txtManualRefNo.Text = _invHdr.Ith_manual_ref;
                    //    //        txtRemarks.Text = _invHdr.Ith_remarks;

                    //    //        DataTable dtinv = CHNLSVC.Sales.GetPendingQuotationToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, txtFindCustomer.Text, _invHdr.Ith_oth_docno, "D", Session["UserDefProf"].ToString());
                    //    //        if (dtinv != null)
                    //    //        {
                    //    //            txtCustName.Text = dtinv.Rows[0]["QH_PARTY_NAME"].ToString();
                    //    //            lblInvoiceDate.Text = Convert.ToDateTime(dtinv.Rows[0]["QH_DT"].ToString()).Date.ToString("dd/MMM/yyyy");
                    //    //            txtInvcDate.Text = lblInvoiceDate.Text;
                    //    //        }
                    //    //    }

                    //    //    #endregion Check Valid Document No

                    //    //    #region Get Serials

                    //    //    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    //    //    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    //    //    invoice_itemsQ = CHNLSVC.Sales.GetAllQuotationItemList(_invHdr.Ith_oth_docno);
                    //    //    if (invoice_items != null)
                    //    //    {
                    //    //        if (_serList != null)
                    //    //        {
                    //    //            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    //    //            foreach (var itm in _scanItems)
                    //    //            {
                    //    //                foreach (QoutationDetails _invItem in invoice_itemsQ)
                    //    //                    if (itm.Peo.Tus_itm_cd == _invItem.Qd_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Qd_line_no)
                    //    //                    {
                    //    //                        //it.Sad_do_qty = q.theCount;
                    //    //                        //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                    //    //                        _invItem.Qd_pick_qty = itm.theCount; // Current scan qty
                    //    //                    }
                    //    //            }
                    //    //        }
                    //    //        dvDOItems.AutoGenerateColumns = false;
                    //    //        dvDOItems.DataSource = invoice_items;
                    //    //        dvDOItems.DataBind();
                    //    //        dvDOSerials.AutoGenerateColumns = false;
                    //    //        dvDOSerials.DataSource = _serList;
                    //    //        dvDOSerials.DataBind();
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        CHNLSVC.CloseAllChannels();
                    //    //        DisplayMessage("Item not found!");
                    //    //        txtDocumentNo.Text = "";
                    //    //        txtDocumentNo.Focus();
                    //    //        return;
                    //    //    }

                    //    //    #endregion Get Serials 
                    //    #endregion
                    //}

                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "";
                    lbtnSave.CssClass = "buttoncolor";
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnDealerInvoice_Click(object sender, EventArgs e)
        {
            mpDealerInvoice.Show();
        }

        protected void chkOtherLocationInoices_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region dgv Pending Invoice

        protected void btndgvpendSelect_Click(object sender, EventArgs e)
        {
            try
            {
                // Stopwatch stopwatch = Stopwatch.StartNew();

                foreach (GridViewRow item in dvPendingInvoices.Rows)
                {
                    item.BackColor = System.Drawing.Color.Transparent;
                }

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSAH_INV_NO = (Label)row.FindControl("lblSAH_INV_NO");
                Label lblSAH_DT = (Label)row.FindControl("lblSAH_DT");
                Label lblSAH_CUS_CD = (Label)row.FindControl("lblSAH_CUS_CD");
                Label lblSAH_CUS_NAME = (Label)row.FindControl("lblSAH_CUS_NAME");
                Label lblSAH_CUS_ADD1 = (Label)row.FindControl("lblSAH_CUS_ADD1");
                Label lblSAH_CUS_ADD2 = (Label)row.FindControl("lblSAH_CUS_ADD2");
                Label lblSAH_PC = (Label)row.FindControl("lblSAH_PC");
                Label lblSAH_INV_TP = (Label)row.FindControl("lblSAH_INV_TP");
                Label lblSAH_ACC_NO = (Label)row.FindControl("lblSAH_ACC_NO");
                Label lblSAH_REF_DOC = (Label)row.FindControl("lblSAH_REF_DOC");
                Label lblSAH_ANAL_2 = (Label)row.FindControl("lblSAH_ANAL_2");

                row.BackColor = System.Drawing.Color.LightCyan;

                lblInvoiceNo.Text = lblSAH_INV_NO.Text.Trim();
                txtInvcNo.Text = lblInvoiceNo.Text;
                ViewState["userSeqNo"] = txtInvcNo.Text;
                ucOutScan.userSeqNo = txtInvcNo.Text;
                lblInvoiceDate.Text = Convert.ToDateTime(lblSAH_DT.Text.Trim()).Date.ToString("dd/MMM/yyyy");
                txtInvcDate.Text = lblSAH_DT.Text;
                txtCustCode.Text = lblSAH_CUS_CD.Text.Trim();
                txtCustName.Text = lblSAH_CUS_NAME.Text.Trim();
                txtCustAddress1.Text = lblSAH_CUS_ADD1.Text.Trim();
                txtCustAddress1.ToolTip = lblSAH_CUS_ADD1.Text.Trim();
                txtCustAddress2.Text = lblSAH_CUS_ADD2.Text.Trim();
                _profitCenter = lblSAH_PC.Text.Trim();
                _invoiceType = lblSAH_INV_TP.Text.Trim();
                _accNo = lblSAH_ACC_NO.Text.Trim();
                lblentryno.Text = lblSAH_ANAL_2.Text;
                Session["InvoiceProfitcenter"] = _profitCenter;
                Issoa_select = false;
                if (txtCustCode.Text == "AHDR2B0002")
                {
                    btnBOC.Visible = true;
                }
                else
                {
                    btnBOC.Visible = false;
                }
                if (rbnMainType.SelectedIndex == 0)
                {
                    _hddr = new List<ImpCusdecHdr>();
                    _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_MasterLocation.Ml_cate_1 == "DFS")
                    {
                        if (chkRno.Checked)
                        {
                            _hddr = CHNLSVC.Sales.GECUSBOND_CUS(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblSAH_CUS_CD.Text, null, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text));
                        }
                    }
                    if (_hddr.Count > 0)
                    {
                        _hddr = _hddr.OrderBy(c => c.CUH_DOC_NO).ToList();
                        grdCUSDec.DataSource = _hddr;
                        grdCUSDec.DataBind();

                        grdCusitem.DataSource = new int[] { };
                        grdCusitem.DataBind();

                        if (_ImpCusdecItm.Count > 0)
                        {
                            foreach (GridViewRow gvrow in grdCUSDec.Rows)
                            {
                                CheckBox chk = (CheckBox)gvrow.FindControl("chk_bond");
                                Label _rno = (Label)gvrow.FindControl("lblcuh_doc_no");
                                var _filter = _ImpCusdecItm.Where(x => x.Cui_doc_no == _rno.Text).ToList();
                                if (_filter.Count > 0)
                                {
                                    chk.Checked = true;
                                    row.BackColor = System.Drawing.Color.LightCyan;
                                }
                            }
                            grdCusitem.DataSource = _ImpCusdecItm;
                            grdCusitem.DataBind();

                        }
                        mpAddBondItems.Show();
                    }
                    else
                    {
                        _sono = lblSAH_REF_DOC.Text;
                        LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter, lblSAH_REF_DOC.Text);
                    }
                    dvPendingInvoices.Columns[1].HeaderText = "Invoice Number";

                    // stopwatch.Stop();
                    //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    //DisplayMessage(stopwatch.ElapsedMilliseconds.ToString());
                }
                else
                {
                    LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
                    dvPendingInvoices.Columns[1].HeaderText = "Invoice Number";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }


        protected void dvPendingInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvPendingInvoices.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)Session["PendingInvoicesToDO"];
            dvPendingInvoices.AutoGenerateColumns = false;
            dvPendingInvoices.DataSource = dt;
            dvPendingInvoices.DataBind();
        }

        #endregion

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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
                case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + oMstCom.Mc_anal19 + seperator + txtFDate.Text + seperator + txtTDate.Text + seperator + txtCustCode.Text + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "DO" + seperator + "0" + seperator);
                        break;
                    }
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
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void clear()
        {
            _isQutationBaseDO = false;
            ucOutScan._isQutationBaseDO = false;
            _sono = null;
            IsSO_base = false;
            Issoa_select = false;
            _soano = "";

            _resqty = "0";
            oMstCom = CHNLSVC.General.GetCompByCode(Convert.ToString(Session["UserCompanyCode"]));
            txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            Session["SONUMBER"] = "";
            _ImpCusdecItm = new List<ImpCusdecItm>();
            Session["TransportMode"] = null;
            hdfIsRecalled.Value = "0";

            rbnMainType.SelectedIndex = 0;

            if (BaseCls.GlbIsManChkLoc == true)
            {
                txtManualRefNo.ReadOnly = true;
            }

            txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
            txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
            txtFindCustomer.Text = string.Empty;
            txtFindInvoiceNo.Text = string.Empty;
            chkDelFrmAnyLoc.Checked = false;
            txtDocumentNo.Text = string.Empty;
            ucOutScan.doc_tp = "DO";
            ucOutScan.isApprovalSend = false;

            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.PNLTobechange.Visible = false;
            dvPendingInvoices.DataSource = new int[] { };
            dvPendingInvoices.DataBind();

            dtpDODate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
            txtInvcNo.Text = string.Empty;
            txtInvcDate.Text = string.Empty;

            txtManualRefNo.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtCustCode.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtCustAddress1.Text = string.Empty;
            txtCustAddress2.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblentryno.Text = string.Empty;
            lblInvoiceNo.Text = string.Empty;

            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PageClear();
            ucOutScan.doc_tp = "DO";

            dvDOItems.DataSource = new int[] { };
            dvDOItems.DataBind();

            gvGiftVoucher.DataSource = new int[] { };
            gvGiftVoucher.DataBind();

            dvDOSerials.DataSource = new int[] { };
            dvDOSerials.DataBind();

            gvSGv.DataSource = new int[] { };
            gvSGv.DataBind();

            grdCusitem.DataSource = new int[] { };
            grdCusitem.DataBind();


            mpPickSerial.Hide();
            Session["PendingInvoicesToDO"] = null;
            Session["_itemLineNo"] = null;
            Session["_invoiceNo"] = null;
            Session["_itemCode"] = null;
            Session["_scanQty"] = null;
            Session["_itemstatus"] = null;
            Session["_ageingDays"] = null;
            Session["PopupQty"] = null;
            Session["serial_list"] = null;
            Session["_itemCode"] = null;

            rbnMainType_SelectedIndexChanged(null, null);

            fileupexcelupload.Attributes.Clear();

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "SaveConfirm();";
            lbtnSave.CssClass = "buttonUndocolor";
            if (Session["GlbDefaultBin"] == null)
            {
                lblbinMssg.Text = "BIN  is not allocate for your location.";
                SbuPopup.Show();
                return;
            }
            ucOutScan.BinCode = Session["GlbDefaultBin"].ToString();
            _MasterLocation = new MasterLocation();

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
        private void clearHalf()
        {
            dvDOItems.DataSource = new int[] { };
            dvDOItems.DataBind();

            gvGiftVoucher.DataSource = new int[] { };
            gvGiftVoucher.DataBind();

            dvDOSerials.DataSource = new int[] { };
            dvDOSerials.DataBind();

            //grdSerial.DataSource = new int[] { };
            //grdSerial.DataBind();

            mpPickSerial.Hide();
            Session["PendingInvoicesToDO"] = null;
            Session["_itemLineNo"] = null;
            Session["_invoiceNo"] = null;
            Session["_itemCode"] = null;
            Session["_scanQty"] = null;
            Session["_itemstatus"] = null;
            Session["_ageingDays"] = null;
            Session["PopupQty"] = null;
            Session["serial_list"] = null;
            Session["_itemCode"] = null;
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
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

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }
        private void DisplayMessageNew(String Msg, Int32 option)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);

            }

        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        #region Search

        protected void btnSearchClose_Click(object sender, EventArgs e)
        {
            mpSearch.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                DivWarning.Visible = true;
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
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                DivWarning.Visible = true;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "Customer")
                {
                    txtFindCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "InvSalesInvoice")
                {
                    txtFindInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "MovementDocDateSearch")
                {
                    txtDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDocumentNo_TextChanged(null, null);
                }
                if (lblvalue.Text == "CustomerQuo")
                {
                    txtFindInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtFindInvoiceNo_TextChanged(null, null);
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                DivWarning.Visible = true;
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show();
                return;
            }
            else if (lblvalue.Text == "InvSalesInvoice")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show();
                return;
            }
            else if (lblvalue.Text == "MovementDocDateSearch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, DateTime.Now.AddYears(-2).Date, DateTime.Now.Date);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show();
                return;
            }
            else if (lblvalue.Text == "CustomerQuo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotationAllWeb(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show();
                return;
            }
        }

        private void FilterData()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblvalue.Text == "Rebond")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                    DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderForInvoice(SearchParams, ddlSearchbykeyD.Text, txtSearchbywordD.Text.Trim());
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                }
                if (lblvalue.Text == "Customer")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                }
                else if (lblvalue.Text == "InvSalesInvoice")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                    DataTable result = CHNLSVC.CommonSearch.GetInvoiceSearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                }
                else if (lblvalue.Text == "MovementDocDateSearch")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    if (txtSearchbywordD.Text == "%")
                    {
                        txtSearchbywordD.Text = "";
                    }
                    DataTable result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "MovementDocDateSearch";
                    BindUCtrlDDLData2(result);
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                    Session["DPopup"] = "DPopup";
                    txtSearchbyword.Focus();                 
                }
                else if (lblvalue.Text == "CustomerQuo")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                    DataTable result = CHNLSVC.CommonSearch.GetQuotationAllWeb(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show();
                }
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

        #endregion

        private void ValidateTrue()
        {
            DivWarning.Visible = false;
            lblWarning.Text = "";
            DivSuccess.Visible = false;
            lblSuccess.Text = "";
            DivInformation.Visible = false;
        }
        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }
        private List<InvoiceItem> setItemDescriptions(List<InvoiceItem> itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (InvoiceItem item in itemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Sad_itm_stus);
                    if (oStatus != null)
                    {
                        item.Sad_itm_stus_desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Sad_itm_stus_desc = item.Mi_itm_stus;
                    }
                }
            }

            return itemList;
        }

        private List<ReptPickSerials> setItemDescriptions_2(List<ReptPickSerials> serialList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                if (serialList != null)
                {
                    foreach (ReptPickSerials serial in serialList)
                    {
                        MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == serial.Tus_itm_stus);
                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), serial.Tus_itm_cd);
                        if (oStatus != null)
                        {
                            serial.Tus_itm_stus_Desc = oStatus.Mis_desc;

                        }
                        else
                        {
                            serial.Tus_itm_stus_Desc = serial.Tus_itm_stus;
                        }

                        if (_masterItem != null)
                        {
                            serial.Tus_itm_desc = _masterItem.Mi_longdesc;
                            serial.Tus_itm_model = _masterItem.Mi_model;
                            serial.Tus_itm_brand = _masterItem.Mi_brand;
                        }
                    }
                }
            }

            return serialList;
        }

        public void LoadInvoiceItems(string _invNo, string _pc, string _so = null)
        {
            invoice_items_bind = new List<InvoiceItem>();

            //Get Invoice Items Details
            invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(Session["UserCompanyCode"].ToString(), _pc, "INV", _invNo, "A");


            if (invoice_items != null)
            {
                if (invoice_items.Count > 0)
                {
                    dvDOItems.Enabled = true;
                    //Check serial reserved for vehicle registration
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("RECEIPT", Session["UserCompanyCode"].ToString(), _invNo, 0);
                    if (user_seq_num != -1)
                    {
                        dvDOItemsDataBind(invoice_items);
                        dvDOItemsCheckBox();
                        DisplayMessage("Insurance dept. still not issue cover note.");
                        return;
                    }

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), _invNo, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");

                    if (_serList != null)
                    {
                        #region option 1
                        //added by Wimal @ 07/09/2018 
                        foreach (InvoiceItem _invItem in invoice_items)
                        {
                            _invItem.Sad_srn_qty = 0;
                        }
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (InvoiceItem _invItem in invoice_items)
                            {                              
                                //MasterItem msitem = new MasterItem();
                                //msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                {
                                    // if (msitem.Mi_is_ser1 == 0)//(_itemSerializedStatus == "0")
                                    //  {

                                    //_invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                    // }
                                    //else if (msitem.Mi_is_ser1 == 1)// (_itemSerializedStatus == "1")
                                    //{

                                    //    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty

                                    //}


                                }

                            }
                        }
                        dvDOSerialsDataBind(_serList);

                        //dvDOItemsDataBind(invoice_items);
                        //dvDOItemsCheckBox();
                        #endregion
                    }
                    else
                    {
                        List<INT_REQ> INT_REQ = new List<INT_REQ>();
                        INT_REQ = CHNLSVC.Inventory.GETREQBY_REF(_so, Session["UserCompanyCode"].ToString(), "SOA", null, 0);
                        if (INT_REQ != null)
                        {

                            if (Issoa_select == false)
                            {
                                if (INT_REQ.Count > 0)
                                {
                                    grdSOA.DataSource = INT_REQ;
                                    grdSOA.DataBind();
                                    MdlSOA.Show();
                                    //_result = true;
                                    return;
                                }
                            }

                            if (INT_REQ.Count > 0)
                            {
                                if (_SOANOList != null)
                                {
                                    if (_SOANOList.Count > 0)
                                    {
                                        var prioritySeats = INT_REQ.Where(s => _SOANOList.Contains(s.ITR_REQ_NO)).ToList();
                                        _soano = _SOANOList[0].ToString();
                                    }
                                    else
                                    {
                                        _soano = INT_REQ[0].ITR_REQ_NO;
                                    }
                                }
                                else
                                {
                                    _soano = INT_REQ[0].ITR_REQ_NO;
                                }
                            }
                            else
                            {
                                _soano = INT_REQ[0].ITR_REQ_NO;
                            }

                            if (INT_REQ.Count > 0)
                            {
                                if (INT_REQ.Count > 1)
                                {
                                    bool _chkreq = false;
                                    foreach (INT_REQ _item in INT_REQ)
                                    {

                                        foreach (string _f in _SOANOList)
                                        {
                                            if (_f == _item.ITR_REQ_NO)
                                            {
                                                _soano = _item.ITR_REQ_NO;
                                                _chkreq = true;
                                            }
                                        }
                                        //_soano = INT_REQ[0].ITR_REQ_NO;
                                        if (_chkreq)
                                        {
                                            int user_seq_num1 = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("SOA", Session["UserCompanyCode"].ToString(), _soano, 0);
                                            if (user_seq_num1 > 0)
                                            {
                                                List<ReptPickSerials> oSerials = new List<ReptPickSerials>();
                                                oSerials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num1, "SOA");
                                                if (oSerials != null)
                                                {
                                                    if (_serList == null)
                                                    {
                                                        _serList = oSerials;
                                                    }
                                                    else
                                                    {
                                                        _serList.AddRange(oSerials);
                                                    }
                                                }
                                                _chkreq = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Session["SONUMBER"] = _soano;
                                    int user_seq_num2 = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("SOA", Session["UserCompanyCode"].ToString(), _soano, 0);
                                    if (user_seq_num2 > 0)
                                    {
                                        _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num2, "SOA");
                                    }
                                }
                                if (_serList != null)
                                {
                                    #region option 2
                                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                                    foreach (var itm in _scanItems)
                                    {
                                        foreach (InvoiceItem _invItem in invoice_items)
                                        {
                                            //MasterItem msitem = new MasterItem();
                                            //msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                            if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                            {
                                                _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                            }

                                        }
                                    }
                                    dvDOSerialsDataBind(_serList);

                                    // dvDOItemsDataBind(invoice_items);
                                    //dvDOItemsCheckBox();
                                    #endregion
                                }
                                else
                                {
                                    if (chkAODoutserials.Checked)
                                    {
                                        #region option 3
                                        string _docno = string.Empty;
                                        foreach (InvoiceItem _itm in invoice_items)
                                        {
                                            MasterItem msitem = new MasterItem();
                                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                            if (msitem.Mi_is_ser1 == 0)
                                            {

                                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                                _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                                _reptPickSerial_.Tus_seq_no = user_seq_num;
                                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                                _reptPickSerial_.Tus_base_doc_no = _itm.Sad_inv_no;
                                                _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                                _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                                _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                                                _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                                                _reptPickSerial_.Tus_itm_line = _itm.Sad_itm_line;
                                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_itm.Sad_qty);
                                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                                _reptPickSerial_.Tus_ser_id = 0;
                                                _reptPickSerial_.Tus_serial_id = "0";
                                                _reptPickSerial_.Tus_unit_cost = 0;
                                                _reptPickSerial_.Tus_unit_price = 0;
                                                _reptPickSerial_.Tus_unit_price = 0;
                                                // _reptPickSerial_.Tus_job_no = _itm.Sad_inv_no;
                                                //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                                //_reptPickSerial_.Tus_job_line = JobLineNo;
                                                //_reptPickSerial_.Tus_exist_supp = suppler;
                                                //_reptPickSerial_.Tus_orig_supp = suppler;
                                                if (_itm.Sad_res_line_no == -1)
                                                {
                                                    _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Sad_qty);
                                                }
                                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                            }
                                        }
                                        #endregion
                                    }
                                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                                    dvDOSerialsDataBind(_serList);
                                    if (_serList != null)
                                    {
                                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                                        foreach (var itm in _scanItems)
                                        {
                                            foreach (InvoiceItem _invItem in invoice_items)
                                            {
                                                //MasterItem msitem = new MasterItem();
                                                //msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                                if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                                {
                                                    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                                }

                                            }
                                        }
                                    }
                                    // List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                                    // dvDOSerialsDataBind(_serList);
                                }
                            }



                            else
                            {
                                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                                dvDOSerialsDataBind(_serList);
                            }
                        }
                        else
                        {
                            if (chkAODoutserials.Checked)
                            {
                                string _docno = string.Empty;
                                foreach (InvoiceItem _itm in invoice_items)
                                {
                                    MasterItem msitem = new MasterItem();
                                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                    if (msitem.Mi_is_ser1 == 0 || msitem.Mi_is_ser1 == -1)
                                    {
                                        if (_itm.Sad_do_qty != _itm.Sad_qty)
                                        {
                                            decimal _qty = _itm.Sad_qty - _itm.Sad_do_qty;
                                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                            _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                            _reptPickSerial_.Tus_seq_no = user_seq_num;
                                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                            _reptPickSerial_.Tus_base_doc_no = _itm.Sad_inv_no;
                                            _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                            _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                            _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                                            _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                                            _reptPickSerial_.Tus_itm_line = _itm.Sad_itm_line;
                                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                                            _reptPickSerial_.Tus_ser_1 = "N/A";
                                            _reptPickSerial_.Tus_ser_2 = "N/A";
                                            _reptPickSerial_.Tus_ser_3 = "N/A";
                                            _reptPickSerial_.Tus_ser_4 = "N/A";
                                            _reptPickSerial_.Tus_ser_id = 0;
                                            _reptPickSerial_.Tus_serial_id = "0";
                                            _reptPickSerial_.Tus_unit_cost = 0;
                                            _reptPickSerial_.Tus_unit_price = 0;
                                            _reptPickSerial_.Tus_unit_price = 0;
                                            // _reptPickSerial_.Tus_job_no = _itm.Sad_inv_no;
                                            //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                            //_reptPickSerial_.Tus_job_line = JobLineNo;
                                            //_reptPickSerial_.Tus_exist_supp = suppler;
                                            //_reptPickSerial_.Tus_orig_supp = suppler;
                                            if (_itm.Sad_res_line_no == -1)
                                            {
                                                _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_itm.Sad_qty);
                                            }
                                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                        }
                                    }
                                }
                            }
                            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                            List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                            dvDOSerialsDataBind(_serList);
                            if (_serList != null)
                            {
                                var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                                foreach (var itm in _scanItems)
                                {
                                    foreach (InvoiceItem _invItem in invoice_items)
                                    {
                                        //MasterItem msitem = new MasterItem();
                                        //msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                        if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                        {
                                            _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                        }

                                    }
                                }
                            }
                            // dvDOItemsDataBind(invoice_items);
                            // dvDOItemsCheckBox();
                        }
                    }

                    if (gvSGv.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvSGv.Rows.Count; i++)
                        {
                            Label lblsgv_invline = (Label)gvSGv.Rows[i].FindControl("lblsgv_invline");
                            Int32 _invline = Convert.ToInt32(lblsgv_invline.ToString());
                            foreach (InvoiceItem _invItem in invoice_items)
                                if (_invline == _invItem.Sad_itm_line)
                                    _invItem.Sad_srn_qty += 1;
                        }
                    }

                    //Wimal @ 29/06/2018  to avaoid part DO error
                    if (_serList == null)
                    {
                        foreach (InvoiceItem _invItem in invoice_items)
                        {
                            _invItem.Sad_srn_qty = 0;
                        }
                    }

                    dvDOItemsDataBind(invoice_items);
                    dvDOItemsCheckBox();
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();

                dvDOSerialsDataBind(emptyGridList);

                InvoiceItem it = new InvoiceItem();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Sad_alt_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Sad_qty = 0;
                it.Sad_tot_amt = 0;

                invoice_items_bind = new List<InvoiceItem>();

                invoice_items_bind.Add(it);

                dvDOItemsDataBind(invoice_items_bind);
                dvDOItemsCheckBox();
                return;
            }

            //get all from sat_itm
        }

        public void LoadInvoiceItemsQuatation(string _invNo, string _pc)
        {
            invoice_items_bindQ = new List<QoutationDetails>();
            //Get Invoice Items Details
            invoice_itemsQ = CHNLSVC.Sales.GetAllQuotationItemList(_invNo);
            if (invoice_itemsQ != null)
            {
                if (invoice_itemsQ.Count > 0)
                {
                    dvDOItems.Enabled = true;
                    //Check serial reserved for vehicle registration
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("RECEIPT", Session["UserCompanyCode"].ToString(), _invNo, 0);
                    if (user_seq_num != -1)
                    {
                        List<InvoiceItem> oNewItems = ConvertToInvoiceItems(invoice_itemsQ);
                        dvDOItemsDataBind(oNewItems);
                        DisplayMessage("Insurance dept. still not issue cover note.");
                        return;
                    }

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", Session["UserCompanyCode"].ToString(), _invNo, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    // _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "QUO");
                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (QoutationDetails _invItem in invoice_itemsQ)
                                if (itm.Peo.Tus_itm_cd == _invItem.Qd_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Qd_line_no)
                                {
                                    //it.Sad_do_qty = q.theCount;
                                    //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                    _invItem.Qd_pick_qty = itm.theCount; // Current scan qty
                                }
                        }
                        dvDOSerialsDataBind(_serList);

                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        dvDOSerialsDataBind(emptyGridList);
                    }

                    if (gvSGv.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvSGv.Rows.Count; i++)
                        {
                            Label lblsgv_invline = (Label)gvSGv.Rows[i].FindControl("lblsgv_invline");
                            Int32 _invline = Convert.ToInt32(lblsgv_invline.ToString());
                            foreach (QoutationDetails _invItem in invoice_itemsQ)
                                if (_invline == _invItem.Qd_line_no)
                                    _invItem.Qd_pick_qty += 1;
                        }
                    }

                    List<InvoiceItem> oNewItems2 = ConvertToInvoiceItems(invoice_itemsQ);
                    dvDOItemsDataBind(oNewItems2);
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                dvDOSerialsDataBind(emptyGridList);

                QoutationDetails it = new QoutationDetails();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Qd_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Qd_frm_qty = 0;
                it.Qd_tot_amt = 0;

                invoice_items_bindQ = new List<QoutationDetails>();

                invoice_items_bindQ.Add(it);

                List<InvoiceItem> oNewItems = ConvertToInvoiceItems(invoice_items_bindQ);
                dvDOItemsDataBind(oNewItems);

                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending items found for this invoice!");
                return;
            }

            //get all from sat_itm
        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            string _baseDocTp = "DO";
            if (rbnMainType.SelectedIndex != 0)
            {
                _baseDocTp = "QUO";
            }
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), _baseDocTp, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = _baseDocTp;
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false; //direction always (-) for change status
            RPH.Tuh_doc_no = lblInvoiceNo.Text;
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            CHNLSVC.CloseAllChannels();
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        private void dvDOItemsCheckBox()
        {
            for (int i = 0; i < dvDOItems.Rows.Count; i++)
            {
                GridViewRow dr = dvDOItems.Rows[i];
                Label lblSad_isapp = (Label)dr.FindControl("lblSad_isapp");
                CheckBox chkSad_isapp = (CheckBox)dr.FindControl("chkSad_isapp");
                if (lblSad_isapp.Text == "True")
                {
                    chkSad_isapp.Checked = true;
                }
                else
                {
                    chkSad_isapp.Checked = false;
                }

                Label lblSad_iscovernote = (Label)dr.FindControl("lblSad_iscovernote");
                CheckBox chkSad_iscovernote = (CheckBox)dr.FindControl("chkSad_iscovernote");
                if (lblSad_iscovernote.Text == "True")
                {
                    chkSad_iscovernote.Checked = true;
                }
                else
                {
                    chkSad_iscovernote.Checked = false;
                }
            }
        }

        private void addSearchTypesPickSerial()
        {
            ddlSearchbykeyA.Items.Clear();
            ddlSearchbykeyA.Items.Add(new ListItem("Serial1"));
            ddlSearchbykeyA.Items.Add(new ListItem("Serial2"));
        }

        private Int32 isItemSelected()
        {
            Int32 result = 0;
            for (int i = 0; i < grdAdSearch.Rows.Count; i++)
            {
                GridViewRow dr = grdAdSearch.Rows[i];
                CheckBox selectchk = (CheckBox)dr.FindControl("selectchk");
                if (selectchk != null && selectchk.Checked)
                {
                    result = result + 1;
                }
            }
            return result;
        }

        private List<ReptPickSerials> GetAgeItemList(List<ReptPickSerials> _referance, DateTime _documentDate, Int32 _noOfDays)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();

            if (_isAgePriceLevel)
            {
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _documentDate.AddDays(-_noOfDays) || (x.Tus_itm_stus == "AGE" || x.Tus_itm_stus == "AGLP")).ToList();
            }
            else
            {
                _ageLst = _referance;
            }

            return _ageLst;

        }

        private List<ReptPickSerials> GetInvoiceSerials(List<ReptPickSerials> _referance, Int32 ItemLineNo, string ScanDocument)
        {
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            List<InvoiceSerial> _invoiceSerialList = new List<InvoiceSerial>();
            _invoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(ScanDocument);

            //var _promo1 = (from p in _referance
            //               from i in _invoiceSerialList
            //               where (p.Tus_itm_cd == i.Sap_itm_cd) &&
            //               (p.Tus_ser_1 == i.Sap_ser_1) &&
            //               (ItemLineNo == i.Sap_itm_line) 
            //               select p).ToList();

            _serList = (from p in _referance
                        from i in _invoiceSerialList
                        where (p.Tus_itm_cd == i.Sap_itm_cd) &&
                        (p.Tus_ser_1 == i.Sap_ser_1) &&
                        (ItemLineNo == i.Sap_itm_line) &&
                        (ScanDocument == i.Sap_inv_no)
                        select p).ToList();
            return _serList;
        }

        protected void OnRemoveFromSerialGrid(GridViewRow dr)
        {
            try
            {
                int row_id = dr.RowIndex;
                Label lblTUS_ITM_CD = (Label)dr.FindControl("lblTUS_ITM_CD");
                Label lblTUS_ITM_STUS = (Label)dr.FindControl("lblTUS_ITM_STUS");
                Label lblTUS_SER_ID = (Label)dr.FindControl("lblTUS_SER_ID");
                Label lblTUS_BIN = (Label)dr.FindControl("lblTUS_BIN");
                Label lblTUS_SER_1 = (Label)dr.FindControl("lblTUS_SER_1");


                if (string.IsNullOrEmpty(lblTUS_ITM_CD.Text.Trim()))
                    return;
                string _item = lblTUS_ITM_CD.Text.Trim();
                string _status = lblTUS_ITM_STUS.Text.Trim();
                Int32 _serialID = Convert.ToInt32(lblTUS_SER_ID.Text.Trim());
                string _bin = Convert.ToString(lblTUS_BIN.Text.Trim());
                string serial_1 = Convert.ToString(lblTUS_SER_1.Text.Trim());

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), lblInvoiceNo.Text, 0);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), _item, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                }

                //LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
                if (rbnMainType.SelectedIndex == 0)
                {
                    LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
                }
                else
                {
                    LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void OnRemoveFromSerialGridQtation(GridViewRow dr)
        {
            try
            {
                int row_id = dr.RowIndex;
                Label lblTUS_ITM_CD = (Label)dr.FindControl("lblTUS_ITM_CD");
                Label lblTUS_ITM_STUS = (Label)dr.FindControl("lblTUS_ITM_STUS");
                Label lblTUS_SER_ID = (Label)dr.FindControl("lblTUS_SER_ID");
                Label lblTUS_BIN = (Label)dr.FindControl("lblTUS_BIN");
                Label lblTUS_SER_1 = (Label)dr.FindControl("lblTUS_SER_1");

                if (string.IsNullOrEmpty(lblTUS_ITM_CD.Text.Trim()))
                    return;
                string _item = lblTUS_ITM_CD.Text.Trim();
                string _status = lblTUS_ITM_STUS.Text.Trim();
                Int32 _serialID = Convert.ToInt32(lblTUS_SER_ID.Text.Trim());
                string _bin = Convert.ToString(lblTUS_BIN.Text.Trim());
                string serial_1 = Convert.ToString(lblTUS_SER_1.Text.Trim());

                Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", Session["UserCompanyCode"].ToString(), lblInvoiceNo.Text, 0);

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                }

                //LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
                if (rbnMainType.SelectedIndex == 0)
                {
                    LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
                }
                else
                {
                    LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                CHNLSVC.CloseChannel();
                return;
            }
        }


        private bool IsGiftVoucherNAttachedItemTally()
        {
            oInvoiceVouchers = (List<InvoiceVoucher>)Session["oInvoiceVouchers "];

            var _attachedItem = oInvoiceVouchers.Select(x => x.Stvo_gv_itm).ToList().Distinct();

            foreach (var _itm in _attachedItem)
            {
                var _scanqty = invoice_items.Where(x => x.Sad_itm_cd == _itm).ToList().Sum(x => x.Sad_srn_qty);
                int _pickgv = 0;

                foreach (InvoiceVoucher item in oInvoiceVouchers)
                {
                    _pickgv = oInvoiceVouchers.FindAll(x => x.Stvo_gv_itm == item.Stvo_gv_itm).Count;

                }
                if (_scanqty != _pickgv)
                {
                    DisplayMessage("Item and verified gift voucher are mismatched. Attach item : " + _itm);
                    return false;
                }

                if (_scanqty != _pickgv)
                {
                    DisplayMessage("Item and verified gift voucher are mismatched. Attach item : " + _itm);
                    return false;
                }
            }
            return true;
        }

        private bool IsGiftVoucherAvailable()
        {
            var _count = 0;
            if (rbnMainType.SelectedIndex == 0)
            {
                _count = invoice_items.Where(x => x.Sad_itm_tp == "G" && string.IsNullOrEmpty(x.Sad_promo_cd) && x.Sad_qty - x.Sad_do_qty > 0).Count();
            }
            else
            {
                _count = invoice_itemsQ.Where(x => x.Mi_type == "G" && x.Qd_frm_qty - x.Qd_issue_qty > 0).Count();
            }
            if (_count <= 0)
                return false;
            else
                return true;
        }

        private void dvDOItemsDataBind(List<InvoiceItem> oItems)
        {
            _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_MasterLocation.Ml_cate_1 == "DFS")
            {
                if (_ImpCusdecItm.Count > 0)
                {
                    foreach (ImpCusdecItm _itm in _ImpCusdecItm)
                    {

                        var _filter = oItems.FirstOrDefault(x => x.Sad_itm_cd == _itm.Cui_itm_cd);//invoice item = GRN Item code not a base item code
                        if (_filter != null)
                        {
                            _filter.Cus_ITM_QTY = _filter.Cus_ITM_QTY + _itm.Cui_qty;

                        }


                    }

                    var _removezero = oItems.Where(y => y.Cus_ITM_QTY != 0).ToList();
                    if (_removezero.Count > 0)
                    {
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = setItemDescriptions(_removezero);
                        Session["dvDOItems"] = _removezero;
                        dvDOItems.DataBind();
                    }
                    else
                    {
                        DisplayMessage("Please select valid bond", 2);
                    }
                }
                else
                {
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = setItemDescriptions(oItems);
                    Session["dvDOItems"] = oItems;
                    dvDOItems.DataBind();
                }
            }
            else
            {
                dvDOItems.AutoGenerateColumns = false;
                dvDOItems.DataSource = setItemDescriptions(oItems);
                Session["dvDOItems"] = oItems;
                dvDOItems.DataBind();

            }

        }

        private void dvDOSerialsDataBind(List<ReptPickSerials> oItems)
        {
            dvDOSerials.AutoGenerateColumns = false;

            dvDOSerials.DataSource = setItemDescriptions_2(oItems);
            dvDOSerials.DataBind();
            Session["dvDOSerials"] = oItems;
        }

        private void reCellQuatation()
        {
            bool _invalidDoc = true;

            #region Clear Data

            txtFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
            txtTo.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");
            dtpDODate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MMM/yyyy");

            DataTable dt = null;
            dvPendingInvoices.AutoGenerateColumns = false;
            dvPendingInvoices.DataSource = dt;
            dvPendingInvoices.DataBind();

            chkManualRef.Checked = false;
            chkEditAddress.Checked = false;
            txtManualRefNo.Text = "";

            lblBackDateInfor.Text = string.Empty;
            lblInvoiceNo.Text = string.Empty;
            lblInvoiceDate.Text = string.Empty;

            txtCustAddress1.Text = string.Empty;
            txtCustAddress2.Text = string.Empty;
            txtCustCode.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtFindCustomer.Text = string.Empty;
            txtFindInvoiceNo.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
            _emptyserList = null;
            dvDOSerials.AutoGenerateColumns = false;
            dvDOSerials.DataSource = _emptyserList;
            dvDOSerials.DataBind();

            List<QoutationDetails> _emptyinvoiceitemList = new List<QoutationDetails>();
            _emptyinvoiceitemList = null;
            dvDOItems.AutoGenerateColumns = false;
            dvDOItems.DataSource = _emptyinvoiceitemList;
            dvDOItems.DataBind();

            #endregion Clear Data

            InventoryHeader _invHdr = new InventoryHeader();
            _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);

            #region Check Valid Document No

            if (_invHdr == null)
            {
                _invalidDoc = false;
                goto err;
            }
            if (_invHdr.Ith_doc_tp != "DO")
            {
                _invalidDoc = false;
                goto err;
            }
            if (_invHdr.Ith_cate_tp != "QUO")
            {
                _invalidDoc = false;
                goto err;
            }
            if (_invHdr.Ith_direct == true)
            {
                _invalidDoc = false;
                goto err;
            }

        err:
            if (_invalidDoc == false)
            {
                DisplayMessage("Invalid Document No!");
                txtDocumentNo.Text = "";
                txtDocumentNo.Focus();
                return;
            }
            else
            {
                btnGetInvoices.Enabled = false;
                //lbtnSave.Enabled = false;
                hdfIsRecalled.Value = "1";

                dtpDODate.Text = _invHdr.Ith_doc_date.Date.ToString("dd/MMM/yyyy");
                lblInvoiceNo.Text = _invHdr.Ith_oth_docno;
                txtInvcNo.Text = lblInvoiceNo.Text;
                txtCustCode.Text = _invHdr.Ith_bus_entity;
                txtCustAddress1.Text = _invHdr.Ith_del_add1;
                txtCustAddress1.ToolTip = _invHdr.Ith_del_add1;
                txtCustAddress2.Text = _invHdr.Ith_del_add2;
                txtManualRefNo.Text = _invHdr.Ith_manual_ref;
                txtRemarks.Text = _invHdr.Ith_remarks;
                txtRemarks.ToolTip = _invHdr.Ith_remarks;

                DataTable dtinv = CHNLSVC.Sales.GetPendingQuotationToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, txtFindCustomer.Text, _invHdr.Ith_oth_docno, "D", Session["UserDefProf"].ToString());
                if (dtinv != null)
                {
                    txtCustName.Text = dtinv.Rows[0]["QH_PARTY_NAME"].ToString();
                    lblInvoiceDate.Text = Convert.ToDateTime(dtinv.Rows[0]["QH_DT"].ToString()).Date.ToString("dd/MMM/yyyy");
                    txtInvcDate.Text = lblInvoiceDate.Text;
                }
            }

            #endregion Check Valid Document No

            #region Get Serials

            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
            invoice_itemsQ = CHNLSVC.Sales.GetAllQuotationItemList(_invHdr.Ith_oth_docno);
            if (invoice_items != null)
            {
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    foreach (var itm in _scanItems)
                    {
                        foreach (QoutationDetails _invItem in invoice_itemsQ)
                            if (itm.Peo.Tus_itm_cd == _invItem.Qd_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Qd_line_no)
                            {
                                //it.Sad_do_qty = q.theCount;
                                //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                _invItem.Qd_pick_qty = itm.theCount; // Current scan qty
                            }
                    }
                }
                dvDOItems.AutoGenerateColumns = false;
                dvDOItems.DataSource = invoice_items;
                dvDOItems.DataBind();
                dvDOSerials.AutoGenerateColumns = false;
                dvDOSerials.DataSource = _serList;
                dvDOSerials.DataBind();
            }
            else
            {
                CHNLSVC.CloseAllChannels();
                DisplayMessage("Item not found!");
                txtDocumentNo.Text = "";
                txtDocumentNo.Focus();
                return;
            }

            #endregion Get Serials
        }

        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {

        }

        protected void lbtngrdItemsDalete_Click(object sender, EventArgs e)
        {

        }

        protected void lbtngrdItemsDalete_Click1(object sender, EventArgs e)
        {
            if (dvDOSerials.Rows.Count > 0)
            {
                if (rbnMainType.SelectedIndex == 0)
                {
                    //if (lbtnSave.Enabled == true)
                    if (hdfIsRecalled.Value == "0")
                    {
                        if (hdfSerialDelete.Value == "1")
                        {
                            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                            Label lblTUS_ITM_CD = (Label)dr.FindControl("lblTUS_ITM_CD");
                            Label lblTUS_SER_1 = (Label)dr.FindControl("lblTUS_SER_1");

                            bool _isDelete = CHNLSVC.Inventory.Is_Serial_Can_Remove(Session["UserCompanyCode"].ToString(), txtInvcNo.Text, "VEHI_REG", Convert.ToString(lblTUS_ITM_CD.Text.Trim()), Convert.ToString(lblTUS_SER_1.Text.Trim()));
                            if (_isDelete == true)
                            {
                                OnRemoveFromSerialGrid(dr);
                            }
                            else
                            {
                                DisplayMessage("Can't remove this serial!");
                            }
                        }
                    }
                }
                else
                {
                    //if (lbtnSave.Enabled == true)
                    if (hdfIsRecalled.Value == "0")
                    {
                        if (hdfSerialDelete.Value == "1")
                        {
                            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                            Label lblTUS_ITM_CD = (Label)dr.FindControl("lblTUS_ITM_CD");
                            Label lblTUS_SER_1 = (Label)dr.FindControl("lblTUS_SER_1");

                            bool _isDelete = CHNLSVC.Inventory.Is_Serial_Can_Remove(Session["UserCompanyCode"].ToString(), txtInvcNo.Text, "VEHI_REG", Convert.ToString(lblTUS_ITM_CD.Text.Trim()), Convert.ToString(lblTUS_SER_1.Text.Trim()));
                            if (_isDelete == true)
                            {
                                OnRemoveFromSerialGridQtation(dr);
                            }
                            else
                            {
                                DisplayMessage("Can't remove this serial!");
                            }
                        }
                    }

                }
            }
        }

        protected void btnBOC_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddSerials_Click(object sender, EventArgs e)
        {
            try
            {
                bool _autoScroll = false;
                int _itemLineNo = 0;


                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSAD_INV_NO = (Label)dr.FindControl("lblSAD_INV_NO");
                Label lblsad_itm_line = (Label)dr.FindControl("lblsad_itm_line");
                Label lblSad_itm_cd = (Label)dr.FindControl("lblSad_itm_cd");
                Label lblSad_sim_itm_cd = (Label)dr.FindControl("lblSad_sim_itm_cd");
                Label lblSad_itm_stus = (Label)dr.FindControl("lblSad_itm_stus");
                Label lblSad_qty = (Label)dr.FindControl("lblSad_qty");
                Label lblSad_do_qty = (Label)dr.FindControl("lblSad_do_qty");
                Label lblPickQty = (Label)dr.FindControl("lblPickQty");
                Label lblsad_pbook = (Label)dr.FindControl("lblsad_pbook");
                Label lblsad_pb_lvl = (Label)dr.FindControl("lblsad_pb_lvl");
                Label lblSAD_PROMO_CD = (Label)dr.FindControl("lblSAD_PROMO_CD");
                CheckBox chkSad_isapp = (CheckBox)dr.FindControl("chkSad_isapp");
                CheckBox chkSad_iscovernote = (CheckBox)dr.FindControl("chkSad_iscovernote");
                Label BondQty = (Label)dr.FindControl("lblCus_ITM_QTY");
                _simItem = lblSad_sim_itm_cd.Text.Trim();

                string _invoiceNo = lblSAD_INV_NO.Text.Trim();
                _itemLineNo = Convert.ToInt32(lblsad_itm_line.Text);

                Session["_itemLineNo"] = _itemLineNo;
                Session["_invoiceNo"] = _invoiceNo;

                string _itemCode = lblSad_itm_cd.Text.Trim();
                Session["_itemCode"] = _itemCode;
                string _similaritemCode = lblSad_sim_itm_cd.Text.Trim();
                if (!string.IsNullOrEmpty(_similaritemCode))
                {
                    if (_itemCode != _similaritemCode)
                    {
                        _itemCode = _similaritemCode;
                    }
                }
                string _itemstatus = lblSad_itm_stus.Text.Trim();
                decimal _invoiceQty = 0;
                decimal bondqty = Convert.ToDecimal(BondQty.Text.Trim());
                _invoiceQty = Convert.ToDecimal(lblSad_qty.Text.Trim());
                
                //Comment and added Wimal @ 06/09/2018 to get DO pending qty as Instructed Dilanda
                //lbliqty.Text = _invoiceQty.ToString();
                decimal doPendingqty = Convert.ToDecimal(lblSad_qty.Text.Trim()) - Convert.ToDecimal(lblSad_do_qty.Text.Trim());
                lbliqty.Text = doPendingqty.ToString ();
                if (bondqty != 0)
                {
                    if (bondqty < _invoiceQty)
                    {
                        _invoiceQty = bondqty;
                    }
                }

                decimal _doQty = Convert.ToDecimal(lblSad_do_qty.Text.Trim());
                decimal _scanQty = Convert.ToDecimal(lblPickQty.Text.Trim());
                string _priceBook = lblsad_pbook.Text.Trim();
                string _priceLevel = lblsad_pb_lvl.Text.Trim();
                int pbCount = CHNLSVC.Sales.GetDOPbCount(Session["UserCompanyCode"].ToString(), _priceBook, _priceLevel);
                string _promotioncd = lblSAD_PROMO_CD.Text.Trim();
                _isAgePriceLevel = false;
                _isSerializedPrice = false;
                int _ageingDays = -1;

                Session["_scanQty"] = _scanQty;
                Session["_itemstatus"] = _itemstatus;

                MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_item.Mi_cate_1);
                List<PriceBookLevelRef> _level = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _priceBook, _priceLevel);
                if (_level != null)
                    if (_level.Count > 0)
                    {
                        var _lvl = _level.Where(x => x.Sapl_isage).ToList();
                        if (_lvl != null) if (_lvl.Count() > 0)
                                _isAgePriceLevel = true;

                        var _lvl1 = _level.Where(x => x.Sapl_is_serialized).ToList();
                        if (_lvl1 != null) if (_lvl1.Count() > 0)
                                _isSerializedPrice = true;
                    }

                if (_categoryDet != null && _isAgePriceLevel)
                    if (_categoryDet.Rows.Count > 0)
                    {
                        if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                            _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                        else _ageingDays = 0;
                    }

                Session["_ageingDays"] = _ageingDays;

                if ((_invoiceQty - _doQty) <= 0)
                    return;
                if ((_invoiceQty - _doQty) <= _scanQty)
                {
                    DisplayMessage("The serial is already added for the item.");
                    return;
                }

                if (rbnMainType.SelectedIndex == 0)
                {
                    if (chkSad_isapp.Checked != true)
                    {
                        DisplayMessage("Item is not approved for delivery!");
                        return;
                    }
                    if (chkSad_iscovernote.Checked != true)
                    {
                        DisplayMessage("Still not issue cover note!");
                        return;
                    }
                }

                _autoScroll = true;

                mpGiftVoucher.Hide();
                lblGvItem.Text = string.Empty; ;
                lblGvStatus.Text = string.Empty;
                lblGvQty.Text = string.Empty;
                lblInvLine.Text = string.Empty;
                if (_item.Mi_itm_tp == "G" && !string.IsNullOrEmpty(_promotioncd))
                {
                    DisplayMessage("This gift voucher referring promotion");
                    return;
                }

                if (_item.Mi_itm_tp == "G" && string.IsNullOrEmpty(_promotioncd))
                {
                    DataTable _voucher = CHNLSVC.Sales.GetInvoiceVoucher(lblInvoiceNo.Text.Trim(), _itemCode);
                    if (_voucher != null)
                        if (_voucher.Rows.Count > 0)
                        {
                            mpGiftVoucher.Visible = true;
                            lblGvItem.Text = _itemCode;
                            lblGvStatus.Text = _itemstatus;
                            lblGvQty.Text = Convert.ToString(_invoiceQty - _doQty);
                            lblInvLine.Text = Convert.ToString(_itemLineNo);
                        }
                }
                else if (chkChangeSimilarItem.Checked == false)
                {
                    if (string.IsNullOrEmpty(_itemCode))
                    {
                        DisplayMessage("Please select the item code");
                        return;
                    }
                    if (string.IsNullOrEmpty(Session["GlbDefaultBin"].ToString()))
                    {
                        DisplayMessage("Please select the bin code");
                        return;
                    }
                    lblPopupQty.Text = (_invoiceQty - _doQty).ToString();

                    if (string.IsNullOrEmpty(lblPopupQty.Text))
                    {
                        DisplayMessage("Please select the requested qty");
                        return;
                    }
                    if (string.IsNullOrEmpty(_doQty.ToString()))
                    {
                        DisplayMessage("Please select the requested qty");
                        return;
                    }
                    if (string.IsNullOrEmpty(_itemstatus))
                    {
                        DisplayMessage("Please select the item status");
                        return;
                    }

                    if (pbCount <= 0) _isCheckStatus = false;
                    else _isCheckStatus = true;


                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
                    List<MasterCompanyItemStatus> _tempRevertlist = CHNLSVC.Inventory.GetAllCompanyStatuslist(Session["UserCompanyCode"].ToString());
                    List<MasterCompanyItemStatus> _list = null;

                    //lblPopupItemDesc.Text = msitem.Mi_longdesc;
                    //lblPopupItemModel.Text = msitem.Mi_model;
                    //lblPopupItemBrand.Text = msitem.Mi_brand;

                    //if (msitem.Mi_is_ser1 == -1) 
                    //    txtPopupQty.Text = Convert.ToDecimal(lblPopupQty.Text).ToString();

                    string ScanDocument = _invoiceNo;
                    Int32 ItemLineNo = _itemLineNo;

                    //hdnInvoiceNo.Text = ScanDocument;
                    //hdnInvoiceLineNo.Text = ItemLineNo.ToString();

                    #region Get Revert Release Allow Status
                    if (_isRevertStatus)
                    {
                        _list = _tempRevertlist.Where(x => x.Mic_isrvt == true).ToList();
                    }
                    else
                    {
                        _list = _tempRevertlist;
                    }
                    #endregion

                    #region Load Serial/Non-Serial Detail into the Grid
                    if (_isCheckStatus)
                    {
                        serial_list = CHNLSVC.Sales.GetStatusGodSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _priceBook, _priceLevel, _itemstatus);
                    }
                    else
                    {
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _priceBook, _priceLevel);
                    }

                    _isAgePriceLevel = _isAgePriceLevel;

                    serial_list = GetAgeItemList(serial_list, DateTime.Now, _ageingDays);

                    if (_isSerializedPrice == true)
                    {
                        serial_list = GetInvoiceSerials(serial_list, _itemLineNo, _invoiceNo);
                    }

                    Session["PopupQty"] = _invoiceQty - _doQty;

                    addSearchTypesPickSerial();
                    setItemDescriptions_2(serial_list);


                    //if (msitem.Mi_is_ser1 == 0)
                    //{
                    //    Int32 countSelect = 0; decimal qty = Convert.ToDecimal(lblSad_itm_cd.Text);
                    //    foreach (var item in serial_list)
                    //    {
                    //        countSelect++;
                    //        if (countSelect <= qty)
                    //        {
                    //            item.Tus_isSelect = true;
                    //        }
                    //        else
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}
                    Session["serial_list"] = serial_list;
                    grdAdSearch.DataSource = serial_list;
                    lblPopupQty.Text = (_invoiceQty - _doQty).ToString();
                    grdAdSearch.DataBind();
                    mpPickSerial.Show();

                    #endregion

                    return;

                    ////DisplayMessageJS("Develop 807");

                    ////string _itemStatus = lblSad_itm_stus.Text.Trim();
                    ////int _lineNo = Convert.ToInt32(lblsad_itm_line.Text);
                    ////if (string.IsNullOrEmpty(_itemCode)) return;
                    ////string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                    ////DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, "Item", _itemCode);
                    ////grdAdSearch.DataSource = _result;
                    ////grdAdSearch.DataBind();

                    ////_result.Columns.RemoveAt(0);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(2);
                    ////_result.Columns.RemoveAt(1);
                    ////BindUCtrlDDLData3(_result);
                    //////lblAvalue.Text = "Serial";
                    ////mpPickSerial.Show();

                    ////CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                    ////_commonOutScan.PriceBook = _priceBook;
                    ////_commonOutScan.PriceLevel = _priceLevel;
                    ////_commonOutScan.ModuleTypeNo = 1;
                    ////_commonOutScan.ScanDocument = _invoiceNo;
                    ////_commonOutScan.DocumentType = "DO";
                    ////_commonOutScan.PopupItemCode = _itemCode;
                    ////_commonOutScan.ItemStatus = _itemstatus;
                    ////_commonOutScan.ItemLineNo = _itemLineNo;
                    ////_commonOutScan.PopupQty = _invoiceQty - _doQty;
                    ////_commonOutScan.ApprovedQty = _doQty;
                    ////_commonOutScan.ScanQty = _scanQty;
                    ////_commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
                    ////_commonOutScan.IsSerializedPrice = _isSerializedPrice; //Add by Chamal 23/07/2014
                    ////_commonOutScan.DocumentDate = dtpDODate.Value.Date;
                    ////_commonOutScan.NoOfDays = _ageingDays;
                    ////if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
                    ////else _commonOutScan.IsCheckStatus = true;

                    ////_commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                    //////this.Enabled = false;
                    ////_commonOutScan.ShowDialog();
                    //////this.Enabled = true;
                }
                else if (chkChangeSimilarItem.Checked)
                {
                    DataTable _dtTable;
                    if (_level != null && _level.Count > 0)
                    {
                        foreach (PriceBookLevelRef _lvlDet in _level)
                        {
                            //Add Chamal 29/03/2013
                            decimal _balQty = _invoiceQty - _doQty;
                            if (_isAgePriceLevel == false)
                                _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblSad_itm_cd.Text.Trim(), _lvlDet.Sapl_itm_stuts);
                            else
                                _dtTable = CHNLSVC.Inventory.GetInventoryBalanceByBatch(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblSad_itm_cd.Text.Trim(), _lvlDet.Sapl_itm_stuts);

                            if (_dtTable != null)
                            {
                                if (_dtTable.Rows.Count > 0)
                                {
                                    bool _isInventoryBalanceAvailable = false;

                                    if (_isAgePriceLevel == false)
                                        _isInventoryBalanceAvailable = true;
                                    else
                                    {
                                        var _isChkStus = _level.Where(x => x.Sapl_chk_st_tp).Count();
                                        if (_isChkStus > 0)
                                        {
                                            var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<string>("inb_itm_stus") == _itemstatus && x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Text).Date.AddDays(-_ageingDays)).Count();
                                            if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                        }
                                        else
                                        {
                                            var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Text).Date.AddDays(-_ageingDays)).Count();
                                            if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                        }
                                    }

                                    if (_isInventoryBalanceAvailable)
                                    {
                                        DisplayMessage("Cannot select the similar item! Because stock balance are available for invoice item");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    List<MasterItemSimilar> dt = CHNLSVC.Inventory.GetSimilarItems("S", _itemCode, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(dtpDODate.Text).Date, lblInvoiceNo.Text, lblSAD_PROMO_CD.Text, Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString());
                    if (dt != null)
                    {
                        if (dt.Count > 0)
                        {
                            grdsimilItem.AutoGenerateColumns = false;
                            grdsimilItem.DataSource = dt;
                            grdsimilItem.DataBind();
                            lblItemcode.Text = lblSad_itm_cd.Text;
                            lblitemLineno.Text = lblsad_itm_line.Text;
                            ModalPopupSimilerItem.Show();
                        }
                    }

                    // DisplayMessage("Develop 882");
                    //CommonSearch.SearchSimilarItems _similarItems = new CommonSearch.SearchSimilarItems();
                    //_similarItems.DocumentType = "S";
                    //_similarItems.ItemCode = _itemCode;
                    //_similarItems.FunctionDate = dtpDODate.Value.Date;
                    //_similarItems.DocumentNo = lblInvoiceNo.Text;
                    //_similarItems.PromotionCode = dvDOItems.Rows[e.RowIndex].Cells["sad_promo_cd"].Value.ToString();
                    //_similarItems.obj_TragetTextBox = txtDocumentNo;
                    //_similarItems.ShowDialog();
                    //if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                    //{
                    //    dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value = txtDocumentNo.Text;
                    //    txtDocumentNo.Clear();
                    //    CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(lblInvoiceNo.Text, _itemLineNo, dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString());
                    //}
                    chkChangeSimilarItem.Checked = false;
                }

                //LoadInvoiceItems(_invoiceNo, _profitCenter);
                if (rbnMainType.SelectedIndex == 0)
                {
                    LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
                }
                else
                {
                    LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void lbtnDFSSerial_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSAD_INV_NO = (Label)dr.FindControl("lblSAD_INV_NO");
                Label lblSad_itm_cd = (Label)dr.FindControl("lblSad_itm_cd");
                Label lblSad_sim_itm_cd = (Label)dr.FindControl("lblSad_sim_itm_cd");
                Label lblsad_res_line_no = (Label)dr.FindControl("lblsad_res_line_no");
                Label lblPickQty = (Label)dr.FindControl("lblPickQty");
                Label lblSad_qty = (Label)dr.FindControl("lblSad_qty");
                Label lblSad_do_qty = (Label)dr.FindControl("lblSad_do_qty");
                Label lblsad_itm_line = (Label)dr.FindControl("lblsad_itm_line");
                Label lblSad_itm_stus = (Label)dr.FindControl("lblSad_itm_stus");
                bool _isserialMaintan = (bool)Session["_isserialMaintan"];
                if (_isserialMaintan == false)
                {
                    _resqty = lblsad_res_line_no.Text;
                    Session["_itemCode"] = lblSad_itm_cd.Text;
                    Session["_scanQty"] = lblPickQty.Text;
                    decimal _invoiceQty = Convert.ToDecimal(lblSad_qty.Text);
                    decimal _doQty = Convert.ToDecimal(lblSad_do_qty.Text);
                    Session["PopupQty"] = _invoiceQty - _doQty;
                    Session["_itemLineNo"] = lblsad_itm_line.Text;
                    Session["_invoiceNo"] = lblSAD_INV_NO.Text;
                    Session["_itemstatus"] = lblSad_itm_stus.Text;
                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblSad_itm_cd.Text);
                    if (msitem.Mi_is_ser1 == 1)
                    {
                        pnlserialDFS.Visible = true;
                        pnlQtyDFS.Visible = false;
                    }
                    else
                    {
                        pnlserialDFS.Visible = false;
                        pnlQtyDFS.Visible = true;
                    }
                    MpserialPicker.Show();
                    //ucOutScan.isserialMaintan = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }
        protected void chkEditAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAddress.Checked)
            {
                txtCustAddress1.ReadOnly = false;
                txtCustAddress2.ReadOnly = false;
                txtCustAddress1.Focus();
            }
            else
            {
                txtCustAddress1.ReadOnly = true;
                txtCustAddress2.ReadOnly = true;
            }
        }

        #region Pick Serial

        protected void btnPSPClose_Click(object sender, EventArgs e)
        {
            // mpPickSerial.Hide();
        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            try
            {
                String _itemCode = Session["_itemCode"].ToString();
                string serial_no = txtSearchbywordA.Text.Trim();
                //call query.
                string serch_serial = txtSearchbywordA.Text.Trim() + "%";
                //string bin = lblPopupBinCode.Text.Trim();
                //lblPopupItemCode.Text = lblPopupItemCode.Text;

                if (ddlSearchbykeyA.SelectedValue.ToString() == "Serial1")
                {
                    if (!string.IsNullOrEmpty(_simItem))
                    {
                        serial_list = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _simItem.Trim(), null, serch_serial, null);
                    }
                    else
                    {
                        serial_list = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode.Trim(), null, serch_serial, null);
                    }
                }
                else if (ddlSearchbykeyA.SelectedValue.ToString() == "Serial2")
                {
                    serial_list = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode.Trim(), null, null, serch_serial);
                }
                else
                {
                    DisplayMessageJS("Select serial type from drop down!");
                    return;
                }

                int _ageingDays = (int)Session["_ageingDays"];
                Int32 _itemLineNo = (Int32)Session["_itemLineNo"];
                string _invoiceNo = Session["_invoiceNo"].ToString();

                serial_list = GetAgeItemList(serial_list, DateTime.Now, _ageingDays);
                if (_isSerializedPrice == true)
                {
                    serial_list = GetInvoiceSerials(serial_list, _itemLineNo, _invoiceNo);
                }




                setItemDescriptions_2(serial_list);
                Session["serial_list"] = serial_list;
                grdAdSearch.DataSource = serial_list;
                grdAdSearch.DataBind();
                mpPickSerial.Show();

            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {

            MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), Session["_itemCode"].ToString());
            Int32 num_of_checked_itms = isItemSelected();
            string valu = Session["_scanQty"].ToString();
            decimal PickQty = Convert.ToDecimal(Session["_scanQty"].ToString());
            Int32 generated_seq = -1;
            string MainItemCode = msitem.Mi_cd;
            decimal lblpopUpQty = Convert.ToDecimal(Session["PopupQty"].ToString());

            Int32 _itemLineNo = Convert.ToInt32(Session["_itemLineNo"]);
            string _invoiceNo = Session["_invoiceNo"].ToString();
            string ItemStatus = Session["_itemstatus"].ToString();

            string ScanDocument = _invoiceNo;

            if (msitem.Mi_is_ser1 == -1)
            {
                if (Convert.ToInt32(lblpopUpQty) < Convert.ToDecimal(txtPopupQty.Text))
                {
                    DisplayMessage("Can't exceed the request Qty!");
                    return;
                }

                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), msitem.Mi_cd, ItemStatus);
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
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Please check the inventory balance!");
                    return;
                }
            }

            Decimal pending_amt = Convert.ToDecimal(lblPopupQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if (num_of_checked_itms > pending_amt)
            {
                DisplayMessage("Can't exceed Approved Qty. You can add only " + pending_amt + " itmes more.");
                return;
            }


            bool _isWriteToTemporaryTable = true;
            if (rbnMainType.SelectedIndex == 0)
            {
                #region MyRegion
                Int32 user_seq_num = 0;

                if (_isWriteToTemporaryTable)//Added by Prabhath on 14/06/2013
                {
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                    if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                        generated_seq = user_seq_num;
                    }
                    else
                    {
                        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "DO", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                        //assign user_seqno
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "DO";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = generated_seq;

                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = ScanDocument;
                        //write entry to TEMP_PICK_HDR
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                    }
                }

                if (msitem.Mi_is_ser1 != -1)
                //change msitem.Mi_is_ser1 == true ///
                {
                    List<ReptPickSerials> _list = (List<ReptPickSerials>)Session["serial_list"];
                    int rowCount = 0;

                    if (_list != null)
                    {
                        foreach (var item in _list)
                        {
                            //GridViewRow dr = grdAdSearch.Rows[i];
                            //CheckBox selectchk = (CheckBox)dr.FindControl("selectchk");
                            //Label lblTus_ser_id = (Label)dr.FindControl("lblTus_ser_id"); //item.Tus_ser_id
                            //Label lblTus_bin = (Label)dr.FindControl("lblTus_bin");//item.Tus_bin

                            Int32 serID = item.Tus_ser_id;

                            if (item.Tus_isSelect)
                            {
                                //-------------
                                string binCode = item.Tus_bin;
                                string _tmpItemCode = MainItemCode;
                                if (!string.IsNullOrEmpty(_simItem))
                                {
                                    _tmpItemCode = _simItem;
                                }
                                //ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, MainItemCode, serID);
                                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, _tmpItemCode, serID);
                                //Update_inrser_INS_AVAILABLE
                                Boolean update_inr_ser = false;
                                //if (_isWriteToTemporaryTable) update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), MainItemCode, serID, -1);
                                if (_isWriteToTemporaryTable) update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _tmpItemCode, serID, -1);

                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_job_no = string.Empty;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = 0;
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = -1;

                                if (_isWriteToTemporaryTable) affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }

                        }
                    }
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                    _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_itm_cd = MainItemCode;
                    _reptPickSerial_.Tus_itm_stus = ItemStatus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                    _reptPickSerial_.Tus_ser_1 = "N/A";
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_job_no = string.Empty;
                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    _reptPickSerial_.Tus_job_line = 0;
                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }
                #endregion
            }
            else
            {
                #region MyRegion
                Int32 user_seq_num = 0;
                if (_isWriteToTemporaryTable)//Added by Nadeeka 10-09-2015
                {
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                    if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                        generated_seq = user_seq_num;
                    }
                    else
                    {
                        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "QUO", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "QUO";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = generated_seq;
                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = ScanDocument;
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                }

                if (msitem.Mi_is_ser1 != -1)
                {
                    int rowCount = 0;

                    for (int i = 0; i < grdAdSearch.Rows.Count; i++)
                    {
                        GridViewRow dr = grdAdSearch.Rows[i];
                        CheckBox selectchk = (CheckBox)dr.FindControl("selectchk");
                        Label lblTus_ser_id = (Label)dr.FindControl("lblTus_ser_id");
                        Label lblTus_bin = (Label)dr.FindControl("lblTus_bin");
                        Label lblTus_ser_1 = (Label)dr.FindControl("lblTus_ser_1");
                        Label lblTus_ser_2 = (Label)dr.FindControl("lblTus_ser_2");

                        Int32 serID = Convert.ToInt32(lblTus_ser_id.Text.Trim());

                        if (selectchk.Checked)
                        {
                            //-------------
                            string binCode = lblTus_bin.Text.Trim();
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, MainItemCode, serID);
                            Boolean update_inr_ser = false;
                            if (_isWriteToTemporaryTable) update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), MainItemCode, serID, -1);

                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                            _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_job_no = string.Empty;
                            _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                            _reptPickSerial_.Tus_job_line = 0;
                            Int32 affected_rows = -1;

                            if (_isWriteToTemporaryTable) affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                            rowCount++;
                            if (!_isWriteToTemporaryTable)
                            {
                                List<ReptPickSerials> _selectedItemList = new List<ReptPickSerials>();
                                _selectedItemList.Add(_reptPickSerial_);
                            }
                        }

                    }
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                    _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_itm_cd = MainItemCode;
                    _reptPickSerial_.Tus_itm_stus = ItemStatus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                    _reptPickSerial_.Tus_ser_1 = "N/A";
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                    _reptPickSerial_.Tus_job_no = string.Empty;
                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    _reptPickSerial_.Tus_job_line = 0;

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }
                #endregion
            }

            //LoadInvoiceItems(_invoiceNo, _profitCenter);
            if (rbnMainType.SelectedIndex == 0)
            {
                LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
                Session["_scanQty"] = 0;
            }
            else
            {
                LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
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

        #endregion

        protected void btnsgv_delete_Click(object sender, EventArgs e)
        {

        }

        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {

        }

        #region Gift Voucher
        protected void btnGVCLose_Click(object sender, EventArgs e)
        {
            mpGiftVoucher.Hide();
        }

        protected void btnAddGv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPage.Text))
                {
                    DisplayMessage("Please select the page no");
                    txtPage.Text = "";
                    txtPage.Focus();
                    return;
                }

                DataTable _voucher = CHNLSVC.Sales.GetInvoiceVoucher(lblInvoiceNo.Text.Trim(), lblGvItem.Text.Trim());
                if (_voucher != null)
                    if (_voucher.Rows.Count > 0)
                    {
                        var _isExist = _voucher.AsEnumerable().Where(x => (x.Field<Int16>("stvo_stus") == 1 || x.Field<Int16>("stvo_stus") == 2) && x.Field<Int64>("stvo_pageno") == Convert.ToInt64(txtPage.Text.Trim())).ToList();
                        if (_isExist == null || _isExist.Count <= 0)
                        {
                            CHNLSVC.CloseAllChannels();
                            DisplayMessage("There is no such gift voucher available for the selected item.");
                            return;
                        }

                        if (_isExist != null)
                            if (_isExist.Count > 0)
                            {
                                if (Session["oInvoiceVouchers"] != null)
                                {
                                    oInvoiceVouchers = (List<InvoiceVoucher>)Session["oInvoiceVouchers"];
                                }
                                else
                                {
                                    oInvoiceVouchers = new List<InvoiceVoucher>();
                                }

                                if (oInvoiceVouchers.FindAll(x => x.Stvo_gv_itm == lblGvItem.Text.Trim() && x.Stvo_pageno == Convert.ToInt32(txtPage.Text)).Count > 0)
                                {
                                    DisplayMessage("You have already selected this no");
                                    txtPage.Text = "";
                                    txtPage.Focus();
                                    return;
                                }
                                else
                                {
                                    string _item = lblGvItem.Text.Trim();
                                    string _status = lblGvStatus.Text.Trim();
                                    int _book = Convert.ToInt32(_isExist[0].Field<Int64>("stvo_bookno"));
                                    int _page = Convert.ToInt32(txtPage.Text.Trim());
                                    string _attachedItem = Convert.ToString(_isExist[0].Field<string>("stvo_itm_cd"));

                                    var _itmExist = invoice_items.Where(x => (x.Sad_itm_cd == _attachedItem || x.Sad_sim_itm_cd == _attachedItem) && (x.Sad_qty - x.Sad_do_qty) > 0).ToList();
                                    if (_itmExist == null || _itmExist.Count <= 0)
                                    {
                                        CHNLSVC.CloseAllChannels();
                                        DisplayMessage("This gift voucher attached item is " + _attachedItem + " and this item not available in current invoice");
                                        txtPage.Text = "";
                                        return;
                                    }

                                    InvoiceVoucher oNewItem = new InvoiceVoucher();
                                    oNewItem.Stvo_gv_itm = _item;
                                    oNewItem.Stvo_bookno = _book;
                                    oNewItem.Stvo_pageno = _page;
                                    oNewItem.Stvo_itm_cd = _attachedItem;
                                    oNewItem.Stvo_inv_no = Convert.ToString(lblInvoiceNo.Text.Trim());
                                    oNewItem.Stvo_stus = Convert.ToInt32(2);
                                    oInvoiceVouchers.Add(oNewItem);

                                    gvGiftVoucher.DataSource = oInvoiceVouchers;
                                    gvGiftVoucher.DataBind();

                                    txtPage.Text = "";
                                    txtPage.Focus();
                                }
                            }
                    }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
            }
        }

        protected void btngv_delete_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmGv_Click(object sender, EventArgs e)
        {
            if (gvGiftVoucher.Rows.Count <= 0)
            {
                DisplayMessage("Please select the gift voucher(s).");
                return;
            }

            //foreach (DataGridViewRow _row in gvGiftVoucher.Rows)
            //{
            //    string _item = Convert.ToString(_row.Cells["gv_item"].Value);
            //    string _status = Convert.ToString(_row.Cells["gv_status"].Value);
            //    int _book = Convert.ToInt32(Convert.ToString(_row.Cells["gv_book"].Value));
            //    int _page = Convert.ToInt32(Convert.ToString(_row.Cells["gv_page"].Value));
            //    string _attachedItem = Convert.ToString(Convert.ToString(_row.Cells["gv_attacheditem"].Value));
            //    string _invline = Convert.ToString(Convert.ToString(_row.Cells["gv_invline"].Value));

            //    object[] _obj = new object[7];
            //    _obj.SetValue(_item, 1);
            //    _obj.SetValue(_status, 2);
            //    _obj.SetValue(_book, 3);
            //    _obj.SetValue(_page, 4);
            //    _obj.SetValue(_attachedItem, 5);
            //    _obj.SetValue(_invline, 6);

            //    gvSGv.AllowUserToAddRows = true;
            //    gvSGv.Rows.Insert(gvSGv.NewRowIndex, _obj);
            //    gvSGv.AllowUserToAddRows = false;
            //}

            oInvoiceVouchers = (List<InvoiceVoucher>)Session["oInvoiceVouchers"];
            gvSGv.DataSource = oInvoiceVouchers;
            gvSGv.DataBind();

            gvGiftVoucher.DataSource = new int[] { };
            gvGiftVoucher.DataBind();

            mpGiftVoucher.Hide();
            //LoadInvoiceItems(lblInvoiceNo.Text.Trim(), _profitCenter);
            if (rbnMainType.SelectedIndex == 0)
            {
                LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
            }
            else
            {
                LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
            }
        }

        #endregion

        protected void btntest_Click(object sender, EventArgs e)
        {
            mpGiftVoucher.Show();
        }

        #region Dealer Invoice

        protected void btnDealerInvoiceClose_Click(object sender, EventArgs e)
        {
            mpDealerInvoice.Hide();
        }

        protected void txtSCMInvcNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mpDealerInvoice.Show();
                if (string.IsNullOrEmpty(txtSCMInvcNo.Text)) return;

                DateTime _bondDate = DateTime.Now.Date;
                txtSCMInvcDate.Text = "";
                txtSCMCustCode.Text = "";
                txtSCMCustName.Text = "";

                DataTable _dt = CHNLSVC.Sales.GetSalesHdr(txtSCMInvcNo.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    DisplayMessage("Already Uploaded");
                    txtSCMInvcNo.Text = "";
                    txtSCMInvcDate.Text = "";
                    txtSCMCustCode.Text = "";
                    txtSCMCustName.Text = "";
                    txtSCMInvcNo.Focus();
                    return;
                }

                DataTable _dt1 = CHNLSVC.Sales.GetSCMInvc(txtSCMInvcNo.Text);
                if (_dt1 != null && _dt1.Rows.Count > 0)
                {
                    foreach (DataRow _dr1 in _dt1.Rows)
                    {
                        txtSCMCustCode.Text = _dr1["CUSTOMER_CODE"].ToString();

                        DataTable _dt2 = CHNLSVC.Sales.GetCustomerDetails(Session["UserCompanyCode"].ToString(), txtSCMCustCode.Text);
                        if (_dt2 != null && _dt2.Rows.Count > 0)
                        {
                            foreach (DataRow _dr2 in _dt2.Rows)
                            {
                                txtSCMCustName.Text = _dr2["COMPANY_NAME"].ToString();
                                break;
                            }
                        }
                        DateTime _date = Convert.ToDateTime(_dr1["INVOICE_DATE"].ToString()).Date;
                        txtSCMInvcDate.Text = _date.ToString("dd/MMM/yyyy");
                    }
                }
                else
                {
                    DisplayMessage("Invalid Invoice No");
                    txtSCMInvcNo.Text = "";
                    txtSCMInvcNo.Text = "";
                    txtSCMInvcDate.Text = "";
                    txtSCMCustCode.Text = "";
                    txtSCMCustName.Text = "";
                    txtSCMInvcNo.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
            }
        }

        #endregion

        protected void btnUploadSerials_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblInvoiceNo.Text == "lblInvoiceNo" || string.IsNullOrEmpty(lblInvoiceNo.Text))
                {
                    DisplayMessage("Please select a invoice");
                    return;
                }

                if (!fileupexcelupload.HasFile)
                {

                    DisplayMessage("Please select a excel file to upload");
                    return;
                }

                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    default:
                        {
                            DisplayMessageJS("Please select a valid excel file");
                            return;
                        }
                }


                string FolderPath = @"DeliveryOrderFiles\";

                string FilePath = Server.MapPath(FolderPath + FileName);

                foreach (string filename in Directory.GetFiles(Server.MapPath(FolderPath)))
                {
                    File.Delete(filename);
                }

                fileupexcelupload.SaveAs(FilePath);

                FilePath = Server.MapPath(FolderPath + FileName);

                conStr = String.Format(conStr, FilePath, "Yes");
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
                connExcel.Close();

                if (dt.Rows.Count <= 0)
                {
                    DisplayMessage("No data found!");
                    return;
                }

                List<ReptPickItems> _itemList = new List<ReptPickItems>();

                //add to list
                foreach (DataRow _row in dt.Rows)
                {
                    ReptPickItems _itemOne = new ReptPickItems();
                    _itemOne.Tui_req_itm_cd = _row[0].ToString();//Item Code
                    _itemOne.Tui_pic_itm_cd = _row[1].ToString();//Serial No

                    if (_itemOne.Tui_req_itm_cd.Trim() == "" && _itemOne.Tui_pic_itm_cd.Trim() == "")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(_itemOne.Tui_req_itm_cd) || string.IsNullOrEmpty(_itemOne.Tui_pic_itm_cd))
                    {
                        DisplayMessage("Invalid file contents found!");
                        return;
                    }

                    _itemList.Add(_itemOne);
                }

                conStr = "";
                var _result = _itemList.GroupBy(x => new { x.Tui_req_itm_cd, x.Tui_pic_itm_cd }).Select(g => new { g.Key.Tui_req_itm_cd, g.Key.Tui_pic_itm_cd, qty = g.Count() }).Where(a => a.qty > 1).ToList();
                foreach (var prodCount in _result)
                {
                    conStr = conStr + "Item code : " + prodCount.Tui_req_itm_cd + " | serial no : " + prodCount.Tui_pic_itm_cd + "\n";
                }

                if (!string.IsNullOrEmpty(conStr))
                {
                    DisplayMessage("Duplicate contents found!");
                    return;
                }

                int _status = CHNLSVC.Inventory.GetExcelSerials(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), lblInvoiceNo.Text, _itemList, Session["UserID"].ToString(), out conStr);
                if (_status == -99)
                {
                    DisplayMessage("Upload fail : " + conStr.Replace("\n", ""));
                    return;
                }
                //LoadInvoiceItems(lblInvoiceNo.Text, _profitCenter);
                if (rbnMainType.SelectedIndex == 0)
                {
                    LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
                }
                else
                {
                    LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
                }
                btnUploadSerials.Enabled = false;

                DisplayMessage("Upload Successfully");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void rbnMainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearHalf();

            if (rbnMainType.SelectedIndex == 0)
            {
                _isQutationBaseDO = false;
                ucOutScan._isQutationBaseDO = false;
                lblDocType.Text = "Invoice";
                textBox1.Text = "Invoice No :";
                textBox2.Text = "Invoice Date :";
                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, Session["UserDefLoca"].ToString(), txtFindCustomer.Text, txtFindInvoiceNo.Text, chkDelFrmAnyLoc.Checked ? 1 : 0);
                if (dt.Rows.Count > 0)
                {
                    Session["PendingInvoicesToDO"] = dt;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                    dvPendingInvoices.DataBind();
                }
                else
                {
                    dvPendingInvoices.DataSource = new int[] { };
                    dvPendingInvoices.DataBind();
                    Session["PendingInvoicesToDO"] = null;
                }
                dvPendingInvoices.Columns[1].HeaderText = "";
                dvPendingInvoices.Columns[1].HeaderText = "Quotation Number";
                dvPendingInvoices.Columns[9].Visible = true;
            }
            else
            {
                _isQutationBaseDO = true;
                ucOutScan._isQutationBaseDO = true;
                lblDocType.Text = "Quotation";
                textBox1.Text = "Quotation No :";
                textBox2.Text = "Quotation Date :";
                DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, txtFindCustomer.Text, txtFindInvoiceNo.Text, "A", Session["UserDefProf"].ToString());
                if (dt.Rows.Count > 0)
                {

                    DataTable dtNew = convertQuatationtoToInvoice(dt);
                    dvPendingInvoices.AutoGenerateColumns = false;
                    Session["PendingInvoicesToDO"] = dtNew;
                    dvPendingInvoices.DataSource = dtNew;
                    dvPendingInvoices.DataBind();
                }
                else
                {
                    dvPendingInvoices.DataSource = new int[] { };
                    dvPendingInvoices.DataBind();
                    Session["PendingInvoicesToDO"] = null;
                }
                dvPendingInvoices.Columns[9].Visible = false;
                dvPendingInvoices.Columns[1].HeaderText = "Invoice Number";
            }
        }

        private DataTable convertQuatationtoToInvoice(DataTable dtQuatation)
        {
            DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO("test", Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, Session["UserDefLoca"].ToString(), "test", "test", chkDelFrmAnyLoc.Checked ? 1 : 0);

            for (int i = 0; i < dtQuatation.Rows.Count; i++)
            {
                DataRow drNew = dt.NewRow();
                drNew["SAH_INV_NO"] = dtQuatation.Rows[i]["QH_NO"].ToString();
                drNew["SAH_PC"] = dtQuatation.Rows[i]["QH_PC"].ToString();
                drNew["SAH_DT"] = dtQuatation.Rows[i]["QH_DT"].ToString();
                //drNew["SAH_ACC_NO"] = dtQuatation.Rows[i]["SAH_ACC_NO"].ToString();
                drNew["SAH_CUS_CD"] = dtQuatation.Rows[i]["qh_del_cuscd"].ToString();
                drNew["SAH_CUS_NAME"] = dtQuatation.Rows[i]["qh_del_cusname"].ToString();
                drNew["SAH_CUS_ADD1"] = dtQuatation.Rows[i]["qh_del_cusadd1"].ToString();
                drNew["SAH_CUS_ADD2"] = dtQuatation.Rows[i]["qh_del_cusadd2"].ToString();
                drNew["SAH_INV_TP"] = dtQuatation.Rows[i]["QH_TP"].ToString();
                dt.Rows.Add(drNew);
            }
            return dt;
        }

        private List<InvoiceItem> ConvertToInvoiceItems(List<QoutationDetails> oQuatationItems)
        {
            List<InvoiceItem> oresult = new List<InvoiceItem>();

            foreach (QoutationDetails item in oQuatationItems)
            {
                InvoiceItem oNewItem = new InvoiceItem();
                oNewItem.Sad_inv_no = item.Qd_no;
                oNewItem.Sad_itm_line = item.Qd_line_no;
                oNewItem.Sad_itm_cd = item.Qd_itm_cd;
                oNewItem.Sad_sim_itm_cd = item.Qd_itm_cd;
                oNewItem.Mi_longdesc = item.Mi_longdesc;
                oNewItem.Mi_model = item.Mi_model;
                oNewItem.Sad_itm_stus = item.Qd_itm_stus;
                oNewItem.Sad_qty = item.Qd_frm_qty;
                oNewItem.Sad_do_qty = item.Qd_issue_qty;
                oNewItem.Sad_srn_qty = item.Qd_pick_qty;
                oNewItem.Sad_warr_period = item.Qd_warr_pd;
                oNewItem.Sad_warr_remarks = item.Qd_warr_rmk;
                oNewItem.Sad_pbook = item.Qd_pbook;
                oNewItem.Sad_pb_lvl = item.Qd_pb_lvl;
                oresult.Add(oNewItem);
            }
            return oresult;
        }

        protected void btnSAD_INV_NO_Click(object sender, EventArgs e)
        {
            try
            {
                //ucOutScan.IsFromCustDO = true;
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSAD_INV_NO = (Label)dr.FindControl("lblSAD_INV_NO");
                Label lblSad_itm_cd = (Label)dr.FindControl("lblSad_itm_cd");
                Label lblSad_sim_itm_cd = (Label)dr.FindControl("lblSad_sim_itm_cd");
                Label lblsad_res_line_no = (Label)dr.FindControl("lblsad_res_line_no");
                Label lblPickQty = (Label)dr.FindControl("lblPickQty");
                Label lblSad_qty = (Label)dr.FindControl("lblSad_qty");
                Label lblSad_do_qty = (Label)dr.FindControl("lblSad_do_qty");
                Label lblsad_itm_line = (Label)dr.FindControl("lblsad_itm_line");
                Label lblSad_itm_stus = (Label)dr.FindControl("lblSad_itm_stus");
                Label lblsad_res_no = (Label)dr.FindControl("lblsad_res_no");

                if (!string.IsNullOrEmpty(lblSad_sim_itm_cd.Text))
                {
                    ucOutScan.TXTItemCode.Text = lblSad_sim_itm_cd.Text.Trim();
                }
                else
                {
                    ucOutScan.TXTItemCode.Text = lblSad_itm_cd.Text.Trim();
                }
                if (lblsad_res_line_no.Text == "-1")//Invoice res qty use 
                {
                    ucOutScan.isResQTY = true;
                }
                else if (!string.IsNullOrEmpty(lblsad_res_no.Text) && (lblsad_res_no.Text != "N/A"))//Invoice res qty use 
                {
                    ucOutScan.isResQTY = true;
                }
                else
                {
                    ucOutScan.isResQTY = false;
                }

                ucOutScan.ScanDocument = lblSAD_INV_NO.Text;
                ucOutScan.ItemCodeChange();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbnMainType.SelectedIndex == 0)
                {
                    DataTable dt = CHNLSVC.Sales.GetPendingInvoicesToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, Session["UserDefLoca"].ToString(), txtFindCustomer.Text, txtFindInvoiceNo.Text, chkDelFrmAnyLoc.Checked ? 1 : 0);
                    if (dt.Rows.Count > 0)
                    {
                        if (chkPendingDoc.Checked)
                        {
                            #region Check Scan Completed
                            foreach (DataRow item in dt.Rows)
                            {
                                ReptPickHeader _tmpPickHdr = new ReptPickHeader();
                                _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                                {
                                    Tuh_doc_no = item[6].ToString(),//invHdr.Ith_oth_docno,
                                    Tuh_doc_tp = "DO",
                                    Tuh_direct = false,
                                    Tuh_usr_com = Session["UserCompanyCode"].ToString()
                                }).FirstOrDefault();
                                if (_tmpPickHdr != null)
                                {
                                    if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                                    {
                                        if (_tmpPickHdr.Tuh_fin_stus != 1)
                                        {
                                            item.Delete();
                                        }

                                    }
                                    else
                                    {
                                        item.Delete();
                                    }
                                }
                                else
                                {
                                    item.Delete();
                                }
                            }
                            #endregion
                            dt.AcceptChanges();
                        }

                        dvPendingInvoices.AutoGenerateColumns = false;
                        dvPendingInvoices.DataSource = dt;
                        dvPendingInvoices.DataBind();
                        Session["PendingInvoicesToDO"] = dt;
                    }
                    else
                    {
                        dt = null;
                        dvPendingInvoices.AutoGenerateColumns = false;
                        dvPendingInvoices.DataSource = dt;
                        dvPendingInvoices.DataBind();
                        DisplayMessage("No pending invoices found!");
                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetPendingQuotationToDO(Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, txtFindCustomer.Text, txtFindInvoiceNo.Text, "A", Session["UserDefProf"].ToString());
                    if (dt.Rows.Count > 0)
                    {

                        DataTable dtNew = convertQuatationtoToInvoice(dt);
                        dvPendingInvoices.AutoGenerateColumns = false;
                        Session["PendingInvoicesToDO"] = dtNew;
                        dvPendingInvoices.DataSource = dtNew;
                        dvPendingInvoices.DataBind();
                    }
                    else
                    {
                        dvPendingInvoices.DataSource = new int[] { };
                        dvPendingInvoices.DataBind();
                        Session["PendingInvoicesToDO"] = null;
                    }
                }

                if (rbnMainType.SelectedValue == "invo")
                {
                    dvPendingInvoices.Columns[1].HeaderText = "Invoice Number";
                    dvPendingInvoices.Columns[9].Visible = true;
                }
                else
                {
                    dvPendingInvoices.Columns[9].Visible = false;
                    dvPendingInvoices.Columns[1].HeaderText = "Quotation Number";
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
            }
        }

        protected void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualRef.Checked)
            {
                SetManualRefNo();
                txtManualRefNo.Enabled = false;
            }
            else
            {
                txtManualRefNo.Enabled = true;
                txtManualRefNo.Text = "";
            }
        }

        private void SetManualRefNo()
        {
            try
            {
                if (chkManualRef.Checked == true)
                {
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_DO");
                    if (_NextNo != 0)
                        txtManualRefNo.Text = _NextNo.ToString();
                    else
                        txtManualRefNo.Text = string.Empty;
                }
                else
                {
                    txtManualRefNo.Text = string.Empty;
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        private void LoadDeliveroption()
        {
            loadItemStatus();
            DataTable _result = CHNLSVC.CommonSearch.GET_DeliverByOption(Session["UserCompanyCode"].ToString());
            ddlDeliver.DataSource = _result;
            ddlDeliver.DataValueField = "rtm_tp";
            ddlDeliver.DataTextField = "rtm_tp";
            ddlDeliver.DataBind();

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

        protected void Button1_Click(object sender, EventArgs e)
        {

            //Session["GlbReportType"] = "SCM1_DO";
            //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
            //BaseCls.GlbReportHeading = "OUTWARD DOC";
            //Session["GlbReportName"] = "Outward_Docs_Full.rpt";
            //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);

            PopupConfBox.Hide();
            Session["GlbReportType"] = "SCM1_DO";
            Session["GlbReportName"] = "Outward_Docs_DO.rpt";
            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

            clsInventory obj = new clsInventory();
            obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
            PrintPDF(targetFileName, obj._outdocDO);

            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            PopupConfBox.Hide();

        }

        protected void lprintcourier_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["Type"] = "DO";
                Session["RefDoc"] = txtDocumentNo.Text.Trim();
                Session["InvNo"] = txtInvcNo.Text.Trim();
                Session["CourierType"] = "DO";
                Session["CourierCom"] = "";
                if (txtDocumentNo.Text.Trim() == null | txtDocumentNo.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Document No");
                }
                else
                {
                    //PopupConfBox.Hide();
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "abscourier.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.get_courierdata(Session["RefDoc"].ToString(), Session["UserCompanyCode"].ToString(), Session["Type"].ToString(), Session["UserID"].ToString());
                    if (Session["UserCompanyCode"].ToString() == "AAL")
                    {
                        // Add by Tharindu 2018-04-17
                        DataTable CourierCom = CHNLSVC.CommonSearch.GetCourierCompany(Session["RefDoc"].ToString(), "", Session["UserCompanyCode"].ToString());
                        string code = "";
                        if (CourierCom.Rows.Count > 0)
                        {
                            code = CourierCom.Rows[0][0].ToString();
                        }


                        if (code == "ABDR1A1353")
                        {
                            PrintPDF(targetFileName, obj._abscour);

                            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        }
                        //else if (code != "ABDR1A1353" && code == string.Empty)
                        //{
                        //    PrintPDF(targetFileName, obj._abscour);
                        //}
                        else if (code == "CERTLCS")
                        {
                            PrintPDF(targetFileName, obj._aalcour);

                            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        }

                    }
                    else
                    {
                        PrintPDF(targetFileName, obj._abscour);

                        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }

                    //string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //PopupConfBox.Hide();

                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("DO Courier Print", "DeliveryOrder", ex.Message, Session["UserID"].ToString());
            }
        }

        public void _print()
        {
            try
            {
                Session["GlbReportType"] = "SCM1_DO";
                if (Session["UserCompanyCode"].ToString() == "ABE")
                {
                    Session["GlbReportName"] = "Outward_Docs_DO_ABE.rpt";
                }
                else
                {
                    Session["GlbReportName"] = "Outward_Docs_DO.rpt";
                }

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsInventory obj = new clsInventory();
                obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                if (Session["UserCompanyCode"].ToString() == "ABE")
                {
                    PrintPDF(targetFileName, obj._outdocDO_ABE);
                }
                else
                {
                    PrintPDF(targetFileName, obj._outdocDO);
                }

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("DO Print", "DeliveryOrder", ex.Message, Session["UserID"].ToString());
            }
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

        //public void PrintPDF(string targetFileName)
        //{
        //    try
        //    {
        //        clsInventory obj = new clsInventory();
        //        obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
        //        ReportDocument Rel = new ReportDocument();
        //        ReportDocument rptDoc = (ReportDocument)obj._outdocDO;
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            Session["documntNo"] = txtDocumentNo.Text;
            _print();
            //lblMssg.Text = "Do you want print now?";
            //PopupConfBox.Show();
        }




        protected void lbtnTransportMth_Click(object sender, EventArgs e)
        {
            popupTransport.Show();
            Session["TransportMode"] = "Show";
        }

        protected void lbtnCls_Click(object sender, EventArgs e)
        {
            popupTransport.Hide();
            Session["TransportMode"] = "Close";
        }


        protected void lserialprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["documntNo"] = txtDocumentNo.Text.Trim();
                if (txtDocumentNo.Text.Trim() == null | txtDocumentNo.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Document No");
                }
                else
                {
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "serial_items.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                    PrintPDF(targetFileName, obj._serialItems);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("DO Serial Print", "DeliveryOrder", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void lbtngrdCusde_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow item in grdCUSDec.Rows)
                {
                    item.BackColor = System.Drawing.Color.Transparent;
                }

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label doc = (Label)row.FindControl("lblcuh_doc_no");

                //_ImpCusdecItm = CHNLSVC.Financial.GET_CUSDEC_GRNITEM_DOC(doc.Text);
                //grdCusitem.DataSource = _ImpCusdecItm;
                //grdCusitem.DataBind();
                //row.BackColor = System.Drawing.Color.LightCyan;
                //mpAddBondItems.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void chk_bond_Click(object sender, EventArgs e)
        {
            try
            {


                if (grdCUSDec.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {

                        Label doc = (Label)row.FindControl("lblcuh_doc_no");
                        Label _bond = (Label)row.FindControl("lblcuh_sun_bond_no");
                        Label _otherno = (Label)row.FindControl("lblcuh_oth_no");
                        if (_ImpCusdecItm == null)
                        {
                            _ImpCusdecItm = new List<ImpCusdecItm>();
                        }
                        // _ImpCusdecItm = CHNLSVC.Financial.GET_CUSDEC_ITEM_BY_DOC(doc.Text);
                        List<InventoryRequestItem> _REQITM = new List<InventoryRequestItem>();
                        var Filter = _ImpCusdecItm.Where(x => x.Cui_doc_no == doc.Text).ToList();
                        if (Filter.Count == 0)
                        {
                            List<ImpCusdecItm> _ImpCusdecItm_2 = new List<ImpCusdecItm>();
                            _ImpCusdecItm_2 = new List<ImpCusdecItm>();
                            ImpCusdecHdr _hdr = new ImpCusdecHdr();
                            _hdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(_otherno.Text);

                            if (_hdr != null)
                            {
                                if (_hdr.CUH_TP == "BOI")
                                {
                                    _ImpCusdecItm_2 = CHNLSVC.Financial.GET_CUSDEC_GRNITEM_DOC_WEB(doc.Text, "BOI");
                                }
                                else if (_hdr.CUH_TP == "EXP")
                                {
                                    _ImpCusdecItm_2 = CHNLSVC.Financial.GET_CUSDEC_GRNITEM_DOC_WEB(doc.Text, "EXP");
                                }
                                else
                                {
                                    _ImpCusdecItm_2 = CHNLSVC.Financial.GET_CUSDEC_GRNITEM_DOC(doc.Text);
                                }
                            }
                            else
                            {
                                _ImpCusdecItm_2 = CHNLSVC.Financial.GET_CUSDEC_GRNITEM_DOC(doc.Text);
                            }



                            if (_ImpCusdecItm_2 != null)
                            {
                                _ImpCusdecItm.AddRange(_ImpCusdecItm_2);
                            }


                            foreach (ImpCusdecItm _IMP in _ImpCusdecItm)
                            {
                                if (_IMP.Cui_doc_no == doc.Text)
                                {
                                    if (_hdr.CUH_TP != "BOI" && _hdr.CUH_TP != "EXP")
                                    {
                                        _IMP.Cui_Bond_no = _bond.Text;
                                    }
                                    else
                                    {
                                        _IMP.Cui_Bond_no = _IMP.Cui_oth_doc_no;
                                    }

                                    _IMP.Cui_qty = _IMP.Cui_qty - _IMP.Cui_bal_qty1;

                                    _REQITM = CHNLSVC.Inventory.GetMRN_Req_item(doc.Text);
                                    var _filter = _REQITM.Find(x => x.Itri_itm_cd == _IMP.Cui_base_itm);
                                    if (_filter != null)
                                    {
                                        if (_filter.Itri_res_qty > 0)
                                        {

                                            _IMP.Cui_is_res = true;

                                        }
                                    }
                                    //_ImpCusdecItm.ForEach(X => X.Cui_Bond_no = _bond.Text);
                                    //ImpCusdecItm.ForEach(X => X.Cui_qty = X.Cui_qty - X.Cui_bal_qty1);
                                }

                            }


                        }
                        // _ImpCusdecItm.RemoveAll(D => D.Cui_bal_qty1 != D.Cui_qty);
                        grdCusitem.DataSource = _ImpCusdecItm;
                        grdCusitem.DataBind();

                        row.BackColor = System.Drawing.Color.LightCyan;
                        mpAddBondItems.Show();
                    }
                    else
                    {
                        mpAddBondItems.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }
        protected void btncontinue_Click(object sender, EventArgs e)
        {
            LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
        }



        protected void selectchk_CheckedChanged(object sender, EventArgs e)
        {
            List<ReptPickSerials> _list = (List<ReptPickSerials>)Session["serial_list"];
            var lb = (CheckBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label lblTus_ser_1 = row.FindControl("lblTus_ser_1") as Label;
            Label lblTus_ser_id = row.FindControl("lblTus_ser_id") as Label;
            CheckBox selectchk = row.FindControl("selectchk") as CheckBox;
            decimal count = _list.Count(x => x.Tus_isSelect == true);
            Session["_scanQty"] = selectchk.Checked ? (Convert.ToDecimal(Session["_scanQty"]) + 1) : (Convert.ToDecimal(Session["_scanQty"]) - 1);
            decimal qty = Convert.ToDecimal(Session["_scanQty"]);
            _list.Where(c => c.Tus_ser_id == Convert.ToInt32(lblTus_ser_id.Text)).FirstOrDefault().Tus_isSelect = selectchk.Checked;
            if (Convert.ToDecimal(lbliqty.Text) < Convert.ToDecimal(Session["_scanQty"]))
            {
                DisplayMessage("Cannot exceed Invoice Qty");
                _list.Where(c => c.Tus_ser_id == Convert.ToInt32(lblTus_ser_id.Text)).FirstOrDefault().Tus_isSelect = false;
                Session["_scanQty"] = (Convert.ToDecimal(Session["_scanQty"])) - 1;
                grdAdSearch.DataSource = _list;
                Session["serial_list"] = _list;
                grdAdSearch.DataBind();
                mpPickSerial.Show();

                return;
            }
            if (count > Convert.ToDecimal(lbliqty.Text))
            {

                DisplayMessage("Cannot exceed Invoice Qty");
                _list.Where(c => c.Tus_ser_id == Convert.ToInt32(lblTus_ser_id.Text)).FirstOrDefault().Tus_isSelect = false;
                grdAdSearch.DataSource = _list;
                Session["serial_list"] = _list;
                grdAdSearch.DataBind();
                mpPickSerial.Show();
                return;
            }
            // _list.Where(c => c.Tus_ser_1 == lblTus_ser_1.Text).FirstOrDefault();

            grdAdSearch.DataSource = _list;
            Session["serial_list"] = _list;
            grdAdSearch.DataBind();
            mpPickSerial.Show();
        }

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpda.Checked == true)
            {
                txtdocname.Text = lblInvoiceNo.Text;
                MPPDA.Show();
            }
            else
            {
                MPPDA.Hide();
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
            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), txtdocname.Text, 0);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo("DO", txtdocname.Text);
                if (user_seq_num > 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "DO";
                    _inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    // _inputReptPickHeader.Tuh_base_doc = _sono;
                    if (string.IsNullOrEmpty(_soano))
                    {
                        _inputReptPickHeader.Tuh_is_take_res = true;
                    }
                    #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                    List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtdocname.Text.Trim());
                    if (_invReqList.Count > 0)
                    {
                        _inputReptPickHeader.Tuh_is_take_res = true;
                    }
                    else
                    {
                        _inputReptPickHeader.Tuh_is_take_res = false;
                    }
                    #endregion
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

                _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                _inputReptPickHeader.Tuh_doc_tp = "DO";
                _inputReptPickHeader.Tuh_direct = false;
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_base_doc = _sono;
                //if (string.IsNullOrEmpty(_soano))
                //{
                //    _inputReptPickHeader.Tuh_is_take_res = true;
                //}
                #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtdocname.Text.Trim());
                if (_invReqList.Count > 0)
                {
                    _inputReptPickHeader.Tuh_is_take_res = true;
                }
                else
                {
                    _inputReptPickHeader.Tuh_is_take_res = false;
                }
                #endregion
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                return;
            }
            else
            {
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                if (invoice_items != null)
                {
                    if (_MasterLocation.Ml_cate_1 == "DFS")
                    {
                        if (chkRno.Checked)
                        {
                            invoice_items = invoice_items.Where(y => y.Cus_ITM_QTY != 0).ToList();
                        }
                    }


                    bool _Isrespath = false;
                    foreach (InvoiceItem _itm in invoice_items)
                    {

                        if (_itm.Sad_do_qty != _itm.Sad_qty)
                        {
                            ReptPickItems _reptitm = new ReptPickItems();
                            _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                            _reptitm.Tui_req_itm_qty = _itm.Sad_qty - _itm.Sad_do_qty;//_itm.Sad_qty;
                            _reptitm.Tui_req_itm_cd = _itm.Sad_itm_cd;
                            _reptitm.Tui_req_itm_stus = _itm.Sad_itm_stus;
                            _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Sad_itm_line);//Darshana request add by rukshan
                            // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);

                            _reptitm.Tui_pic_itm_qty = 0;
                            if (_itm.Sad_res_line_no == -1)
                            {
                                _Isrespath = true;
                                _reptitm.Tui_batch = "1";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_itm.Sad_res_no))
                                {
                                    _Isrespath = true;
                                    _reptitm.Tui_batch = "1";
                                }
                                else
                                {
                                    _reptitm.Tui_batch = "0";
                                }

                            }
                            _saveonly.Add(_reptitm);
                        }



                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);

                    if (_Isrespath)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                        _inputReptPickHeader.Tuh_doc_tp = "DO";
                        _inputReptPickHeader.Tuh_direct = false;
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_base_doc = _sono;
                        #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                        List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtdocname.Text.Trim());
                        if (_invReqList.Count > 0)
                        {
                            _inputReptPickHeader.Tuh_is_take_res = true;
                        }
                        else
                        {
                            _inputReptPickHeader.Tuh_is_take_res = false;
                        }
                        #endregion
                        val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item Details empty.please select invoice')", true);
                    return;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                MPPDA.Hide();
            }

        }


        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 0, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno      
            if (generated_seq > 0)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }



        protected void txtRemarks_TextChanged(object sender, EventArgs e)
        {
            txtRemarks.ToolTip = "";
            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                txtRemarks.ToolTip = txtRemarks.Text;
            }
        }

        private void getPDASerial()
        {

        }


        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
            //Response.Redirect("~/View/MasterFiles/Warehouse/BinSetup.aspx");
        }
        protected void lbtnSgrdItem_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label similarItem = (Label)row.FindControl("MISI_SIM_ITM_CD");
                if (!string.IsNullOrEmpty(similarItem.Text))
                {
                    CHNLSVC.Sales.UpdateInvoiceSimilarItemCode(lblInvoiceNo.Text, Convert.ToInt32(lblitemLineno.Text), lblItemcode.Text, similarItem.Text);
                }
                LoadInvoiceItems(txtInvcNo.Text.Trim(), Session["InvoiceProfitcenter"].ToString());
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
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
        #region Modal Popup 2
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            // UserPopup.Hide();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "Rebond")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderForInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "Rebond";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "MovementDocDateSearch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "MovementDocDateSearch";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbyword.Focus();
            }
        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Rebond")
            {
                lblentryno.Text = grdResultD.SelectedRow.Cells[1].Text;
            }
            if (lblvalue.Text == "MovementDocDateSearch")
            {
                txtDocumentNo.Text = grdResultD.SelectedRow.Cells[1].Text;
                txtDocumentNo_TextChanged(null, null);
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            try
            {
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        #endregion
        protected void lbtnentry_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
            DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderForInvoice(SearchParams, null, null);
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            BindUCtrlDDLData2(result);
            lblvalue.Text = "Rebond";
            ViewState["SEARCH"] = result;
            UserDPopoup.Show();
            Session["DPopup"] = "DPopup";
            txtSearchbyword.Focus();
        }


        protected void lbtnSerClick_Click(object sender, EventArgs e)
        {

            Int32 num_of_checked_itms = isItemSelected();
            decimal PickQty = Convert.ToDecimal(Session["_scanQty"].ToString());
            Int32 generated_seq = -1;
            string MainItemCode = msitem.Mi_cd;
            decimal lblpopUpQty = Convert.ToDecimal(Session["PopupQty"].ToString());

            Int32 _itemLineNo = Convert.ToInt32(Session["_itemLineNo"]);
            string _invoiceNo = Session["_invoiceNo"].ToString();
            string ItemStatus = Session["_itemstatus"].ToString();

            string ScanDocument = _invoiceNo;


            if (Convert.ToInt32(lblpopUpQty) <= Convert.ToDecimal(PickQty))
            {
                DisplayMessage("Cannot exceed the request Qty");
                MpserialPicker.Show();
                return;
            }







            bool _isWriteToTemporaryTable = true;
            if (rbnMainType.SelectedIndex == 0)
            {
                #region MyRegion
                Int32 user_seq_num = 0;

                if (_isWriteToTemporaryTable)//Added by Prabhath on 14/06/2013
                {
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                    if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                        generated_seq = user_seq_num;
                    }
                    else
                    {
                        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "DO", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                        //assign user_seqno
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "DO";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = generated_seq;

                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = ScanDocument;
                        //write entry to TEMP_PICK_HDR
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                    }
                }

                if (msitem.Mi_is_ser1 == 1)
                {

                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                    _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_itm_cd = MainItemCode;
                    _reptPickSerial_.Tus_itm_stus = ItemStatus;
                    // _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                    _reptPickSerial_.Tus_ser_1 = txtPriceEdit.Text;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_job_no = string.Empty;
                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    _reptPickSerial_.Tus_job_line = 0;
                    _reptPickSerial_.Tus_qty = 1;
                    if (_resqty == "-1")
                    {
                        _reptPickSerial_.Tus_resqty = 1;
                    }
                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    PickQty = PickQty + 1;
                    Session["_scanQty"] = PickQty;
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                    _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_itm_cd = MainItemCode;
                    _reptPickSerial_.Tus_itm_stus = ItemStatus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtqtyDFS.Text);
                    _reptPickSerial_.Tus_ser_1 = "N/A";
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_job_no = string.Empty;
                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    _reptPickSerial_.Tus_job_line = 0;
                    if (_resqty == "-1")
                    {
                        _reptPickSerial_.Tus_resqty = Convert.ToDecimal(txtqtyDFS.Text);
                    }
                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    PickQty = PickQty + 1;
                    Session["_scanQty"] = PickQty;
                }
                #endregion
            }
            else
            {

            }

            //LoadInvoiceItems(_invoiceNo, _profitCenter);
            if (rbnMainType.SelectedIndex == 0)
            {
                LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
            }
            else
            {
                LoadInvoiceItemsQuatation(txtInvcNo.Text.Trim(), _profitCenter);
            }
            txtPriceEdit.Text = string.Empty;
            txtPriceEdit.Focus();
            MpserialPicker.Show();
        }

        protected void btnnserbon_Click(object sender, EventArgs e)
        {
            if (_hddr.Count > 0)
            {
                _hddr = CHNLSVC.Sales.GECUSBOND_CUS(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtCustCode.Text, txtsearchbond.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text));
                //_hddr = _hddr.Where(x => x.CUH_DOC_NO == txtsearchbond.Text).ToList();
                if (_hddr.Count > 0)
                {
                    grdCUSDec.DataSource = _hddr;
                    grdCUSDec.DataBind();

                    grdCusitem.DataSource = new int[] { };
                    grdCusitem.DataBind();


                    mpAddBondItems.Show();
                }
                else
                {
                    DisplayMessage("Please enter a valid document number ");
                    mpAddBondItems.Show();
                }
            }
        }

        protected void btnSOA_Click(object sender, EventArgs e)
        {
            try
            {
                _SOANOList = new List<string>();

                foreach (GridViewRow dgvr in grdSOA.Rows)
                {
                    string _soa = (dgvr.FindControl("lblitr_req_no") as Label).Text;

                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_Soa");
                    if (chk.Checked)
                    {

                        _SOANOList.Add(_soa);
                        Issoa_select = true;
                    }


                }
                LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter, _sono);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnsalesorder_Click(object sender, EventArgs e)
        {
            try
            {


                // IsSO_base = true;

                // LoadInvoiceItems(txtInvcNo.Text.Trim(), _profitCenter);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            try
            {
                int val = 0;
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", Session["UserCompanyCode"].ToString(), txtInvcNo.Text, 0);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo("DO", txtdocname.Text);
                    if (user_seq_num > 0)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = "DO";
                        _inputReptPickHeader.Tuh_direct = false;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = txtInvcNo.Text.Trim();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_base_doc = _sono;
                        _inputReptPickHeader.Tuh_is_take_res = false;
                        if (string.IsNullOrEmpty(_soano))
                        {
                            _inputReptPickHeader.Tuh_is_take_res = true;
                        }
                        #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                        List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtInvcNo.Text.Trim());
                        if (_invReqList.Count > 0)
                        {
                            _inputReptPickHeader.Tuh_is_take_res = true;
                        }
                        #endregion
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

                    _inputReptPickHeader.Tuh_doc_no = txtInvcNo.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "DO";
                    _inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_base_doc = _sono;
                    _inputReptPickHeader.Tuh_is_take_res = false;
                    //if (string.IsNullOrEmpty(_soano))
                    //{
                    //    _inputReptPickHeader.Tuh_is_take_res = true;
                    //}
                    #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                    List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtInvcNo.Text.Trim());
                    if (_invReqList.Count > 0)
                    {
                        _inputReptPickHeader.Tuh_is_take_res = true;
                    }
                    #endregion
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
                    if (invoice_items != null)
                    {
                        if (_MasterLocation.Ml_cate_1 == "DFS")
                        {

                            invoice_items = invoice_items.Where(y => y.Cus_ITM_QTY != 0).ToList();
                        }


                        bool _Isrespath = false;
                        foreach (InvoiceItem _itm in invoice_items)
                        {

                            if (_itm.Sad_do_qty != _itm.Sad_qty)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                                _reptitm.Tui_req_itm_qty = _itm.Sad_qty - _itm.Sad_do_qty;//_itm.Sad_qty;
                                _reptitm.Tui_req_itm_cd = _itm.Sad_itm_cd;
                                _reptitm.Tui_req_itm_stus = _itm.Sad_itm_stus;

                                _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Sad_itm_line);//Darshana request add by rukshan
                                // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                                _reptitm.Tui_pic_itm_qty = 0;
                                if (_itm.Sad_res_line_no == -1)
                                {
                                    _Isrespath = true;
                                }
                                _saveonly.Add(_reptitm);
                            }



                        }
                        val = CHNLSVC.Inventory.SavePickedItems(_saveonly);

                        if (_Isrespath)
                        {
                            ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                            _inputReptPickHeader.Tuh_is_take_res = true;
                            _inputReptPickHeader.Tuh_doc_no = txtInvcNo.Text.Trim();
                            _inputReptPickHeader.Tuh_doc_tp = "DO";
                            _inputReptPickHeader.Tuh_direct = false;
                            _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                            _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                            _inputReptPickHeader.Tuh_base_doc = _sono;
                            #region change HDR reservation tp by lakshan as per the darshana 21 Feb 2017
                            List<InventoryRequest> _invReqList = CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_INVOICE(txtInvcNo.Text.Trim());
                            if (_invReqList.Count > 0)
                            {
                                _inputReptPickHeader.Tuh_is_take_res = true;
                            }
                            else
                            {
                                _inputReptPickHeader.Tuh_is_take_res = false;
                            }
                            #endregion
                            val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item Details empty.please select invoice')", true);
                        return;
                    }
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
        private void LoadLabelData()
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
            while (ddlServiceBy.Items.Count > 1)
            {
                ddlServiceBy.Items.RemoveAt(1);
            }
            ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name != string.Empty && c.Mbe_name != null).OrderBy(c => c.Mbe_name);
            ddlServiceBy.DataTextField = "mbe_name";
            ddlServiceBy.DataValueField = "rtnp_pty_cd";
            ddlServiceBy.DataBind();
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
        protected void ddlTransportMe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTransportLabelData();
            BindTransService();
            SHowSearchButton();
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
        public void ClearUcTrans()
        {
            ddlTransportMe.SelectedIndex = 0;
            ddlTransportMe_SelectedIndexChanged(null, null);
            BindTransService();
            ddlServiceBy.SelectedIndex = 0;
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
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
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
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
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
            if (ddlServiceBy.SelectedIndex > 0)
            {
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
            }
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
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData);
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
        protected void lbtnSeVehicle_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
            _serData = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, null, null);
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
                        _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
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
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
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
                        _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
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
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Helper", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }

        protected void linkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "RegistrationDet")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                    _serData = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "EetimateByJob")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
                    _serData = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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
                DispMsg("Error Occurred :" + ex.Message, "E");
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
                if (_serType == "RegistrationDet")
                {
                    txtSubLvl.Text = code;
                    txtSubLvl_TextChanged(null, null);
                }
                else if (_serType == "Helper")
                {
                    txtNxtLvlD2.Text = code;
                    txtNxtLvlD2_TextChanged(null, null);
                }
                else if (_serType == "Driver")
                {
                    txtNxtLvlD1.Text = code;
                    txtNxtLvlD1_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnclosebondchnge_Click(object sender, EventArgs e)
        {
            MPBONDCH.Hide();
        }

        protected void btnbondchnge_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbondchno.Text.ToString() == "" || txtbondchloc.Text.ToString()=="")
                {
                    DispMsg("Please Enter Loc And BOI/EXP NO", "N");
                    return;
                }
                string sunreqno = CHNLSVC.Financial.GetEXPBOISunReqNo(txtbondchno.Text.ToString(), txtbondchloc.Text.ToString());
                if (sunreqno=="")
                {
                    DispMsg("Can't Find SI No", "N");
                    return;
                }
                int eff = CHNLSVC.Financial.UpdateBondLoc(sunreqno, txtbondchno.Text.ToString(), txtbondchloc.Text.ToString());
            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }

        protected void Chkbondchange_CheckedChanged(object sender, EventArgs e)
        {

            if (Chkbondchange.Checked == true)
            {

                MPBONDCH.Show();
            }
            else
            {
                MPBONDCH.Hide();
            }
        }
    }
}