using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
//using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class Sales_Reversal : Base
    {
        private List<InventorySubSerialMaster> _invSubSerList
        {
            get { if (Session["_invSubSerList"] != null) { return (List<InventorySubSerialMaster>)Session["_invSubSerList"]; } else { return new List<InventorySubSerialMaster>(); } }
            set { Session["_invSubSerList"] = value; }
        }
        private string _docNo
        {
            get { if (Session["_docNo"] != null) { return (string)Session["_docNo"]; } else { return ""; } }
            set { Session["_docNo"] = value; }
        }
        private RequestApprovalHeader _refAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _refAppDet = new List<RequestApprovalDetail>();
        private MasterAutoNumber _refAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _refAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _refAppDetLog = new List<RequestApprovalDetailLog>();

        private RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqAppDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqAppSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqAppDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

        private RequestApprovalHeader _ReqRegHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqRegDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqRegSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqRegAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqRegHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqRegDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqRegSerLog = new List<RequestApprovalSerialsLog>();


        private RequestApprovalHeader _ReqInsHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqInsDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqInsSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqInsHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqInsDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<ReptPickSerialsSub> _doitemSubSerials = new List<ReptPickSerialsSub>();
        private List<VehicalRegistration> _regDetails = new List<VehicalRegistration>();
        private List<RecieptHeader> _regRecList = new List<RecieptHeader>();
        private List<RecieptHeader> _insRecList = new List<RecieptHeader>();
        private List<VehicleInsuarance> _insDetails = new List<VehicleInsuarance>();
        private List<RequestApprovalSerials> _repSer = new List<RequestApprovalSerials>();
        private List<RequestApprovalDetail> _repItem = new List<RequestApprovalDetail>();
        private List<RequestAppAddDet> _repAddDet = new List<RequestAppAddDet>();
        public Reversal_serial _serialRpt = new Reversal_serial();
        //public ApprovalNote _appNote = new ApprovalNote();
        protected bool _isBackDate { get { return (bool)Session["_isBackDate"]; } set { Session["_isBackDate"] = value; } }
        private Boolean _isAppUser = false;
        private Int32 _appLvl = 0;
        private string _dCusCode = "";
        private string _dCusAdd1 = "";
        private string _dCusAdd2 = "";
        private string _currency = "";
        private decimal _exRate = 0;
        private string _invTP = "";
        private string _executiveCD = "";
        private string _manCode = "";
        private Boolean _isTax = false;
        private string _defBin = "";
        private Boolean _isFromReq = false;

        private string _status = "P";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear_Page();
                LoadAppLevelStatus();
                BackDatePermission();
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
                if (Session["DPopup"].ToString() == "DPopup")
                {
                    UserDPopoup.Show();
                    UserPopoup.Hide();
                    Session["DPopup"] = "";
                }
                //if (Session["popup"].ToString() == "Rereport")
                //{
                //    UserRereportPopoup.Show();

                //}
                //else if (Session["popup"].ToString() == "ServiceApproval")
                //{
                //    UserServiceApprovalPopoup.Show();
                //    Session["popup"] = "";
                //}
                //else if (Session["popup"].ToString() == "CashRefundRequest")
                //{
                //    UserCashRefundRequestsPopup.Show();
                //}
                //else if (Session["popup"].ToString() == "Cancel")
                //{
                //    userCancelPopoup.Show();
                //}
            }
        }

        #region Rooting for Method
        private void Clear_Page()
        {
            try
            {
                // Session["documntNo"] = "";
                Session["DPopup"] = "";
                ViewState["RevsFilterSerial"] = "";
                Session["GlbModuleName"] = "m_Trans_Sales_Reversal";
                Session["_isFromReq"] = "false";
                Session["_isAppUser"] = "false";
                Session["_appLvl"] = "0";
                Session["_dCusCode"] = "";
                Session["_dCusAdd1"] = "";
                Session["_dCusAdd2"] = "";
                Session["_currency"] = "";
                Session["_exRate"] = "0";
                Session["_invTP"] = "0";
                Session["_executiveCD"] = "";
                Session["_manCode"] = "";
                Session["_isTax"] = "";
                Session["_defBin"] = "";
                Session["_status"] = "P";
                Session["popup"] = "";
                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppSer = new List<RequestApprovalSerials>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();
                _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

                _refAppHdr = new RequestApprovalHeader();
                _refAppDet = new List<RequestApprovalDetail>();
                _refAppAuto = new MasterAutoNumber();

                _refAppHdrLog = new RequestApprovalHeaderLog();
                _refAppDetLog = new List<RequestApprovalDetailLog>();

                _ReqRegHdr = new RequestApprovalHeader();
                _ReqRegDet = new List<RequestApprovalDetail>();
                _ReqRegSer = new List<RequestApprovalSerials>();
                _ReqRegAuto = new MasterAutoNumber();

                _ReqRegHdrLog = new RequestApprovalHeaderLog();
                _ReqRegDetLog = new List<RequestApprovalDetailLog>();
                _ReqRegSerLog = new List<RequestApprovalSerialsLog>();

                _ReqInsHdr = new RequestApprovalHeader();
                _ReqInsDet = new List<RequestApprovalDetail>();
                _ReqInsSer = new List<RequestApprovalSerials>();
                _ReqInsAuto = new MasterAutoNumber();

                _ReqInsHdrLog = new RequestApprovalHeaderLog();
                _ReqInsDetLog = new List<RequestApprovalDetailLog>();
                _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();
                _regDetails = new List<VehicalRegistration>();
                _regRecList = new List<RecieptHeader>();
                _insRecList = new List<RecieptHeader>();
                _insDetails = new List<VehicleInsuarance>();
                _repSer = new List<RequestApprovalSerials>();
                _repItem = new List<RequestApprovalDetail>();
                _repAddDet = new List<RequestAppAddDet>();
                lbtnCustomer.Visible = true;
                txtCusCode.Enabled = true;
                //lbtnCancel.Enabled = true;
                //lbtnCancel.OnClientClick = "CancelConfirm();";
                //lbtnCancel.CssClass = "buttonUndocolor";

                _isFromReq = false;
                Session["_isFromReq"] = "false";
                _isAppUser = false;
                Session["_isAppUser"] = "false";
                _appLvl = 0;
                Session["_appLvl"] = 0;
                txtCusCode.Text = "";
                txtInvoice.Text = "";
                lblInvDate.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                txtRemarks.Text = "";
                txtManRef.Text = "";
                txtSRNremarks.Text = "";
                lblBackDateInfor.Text = "";
                lblSalePc.Text = "";
                txtSRNDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();

                bool _allowCurrentTrans = false;
                // IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans);
                chkApp.Checked = false;
                chkApp.Enabled = false;
                chkOthSales.Enabled = true;
                chkOthSales.Checked = false;
                lblReq.Text = "";
                lblStatus.Text = "";
                txtCusCode.Enabled = true;
                txtInvoice.Enabled = true;

                lbtnRequest.Enabled = true;
                lbtnRequest.OnClientClick = "SaveConfirm();";
                lbtnRequest.CssClass = "buttonUndocolor";

                txtReqLoc.Text = "";
                lblReturnLoc.Text = "";
                lblReqPC.Text = "";
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";
                lblTotInvAmt.Text = "0";
                lblTotPayAmt.Text = "0";
                lblOutAmt.Text = "0";
                lblTotRetAmt.Text = "0";
                lblCrAmt.Text = "0";
                txtRevEngine.Enabled = false;

                lbtnRevRegAdd.Enabled = false;
                lbtnRevRegAdd.OnClientClick = "return Enable();";
                lbtnRevRegAdd.CssClass = "buttoncolor";

                btnGetRegAll.Enabled = false;


                txtRevRegItem.Enabled = false;
                lblRegReq.Text = "";
                lblRegStatus.Text = "";
                chkRevReg.Checked = false;
                chkRevReg.Enabled = true;

                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";
                txtRevInsEngine.Enabled = false;

                lbtnRevInsAdd.Enabled = false;
                lbtnRevInsAdd.OnClientClick = "return Enable();";
                lbtnRevInsAdd.CssClass = "buttoncolor";


                btnGetInsAll.Enabled = false;
                txtRevInsItem.Enabled = false;
                lblInsReq.Text = "";
                lblInsStatus.Text = "";
                chkRevIns.Checked = false;
                chkRevIns.Enabled = true;
                txtSubType.Text = "";
                lblSubDesc.Text = "";
                txtActLoc.Text = "";
                txtActLoc.Enabled = false;

                lbtnRevInsAdd.Enabled = false;
                lbtnRevInsAdd.OnClientClick = "return Enable();";
                lbtnRevInsAdd.CssClass = "buttoncolor";


                lbtnSearchActLoc.Enabled = false;
                lbtnSearchActLoc.OnClientClick = "return Enable();";
                lbtnSearchActLoc.CssClass = "buttoncolor";

                txtItem.Text = "";
                lblInvLine.Text = "";
                txtNewSch.Text = "";
                lblRepPrice.Text = "";
                txtNewSerial.Text = "";
                txtNewPrice.Text = "";
                txtRQty.Text = "";
                _manCode = "";
                _executiveCD = "";

                txtFDate.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                grdPaymentDetails.DataSource = new int[] { };
                grdPaymentDetails.DataBind();



                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT014", Session["UserID"].ToString());
                if (_sysApp.Sarp_cd != null)
                {
                    chkApp.Checked = true;
                    chkApp.Enabled = true;
                    _isAppUser = true;
                    Session["_isAppUser"] = "true";
                    _appLvl = _sysApp.Sarp_app_lvl;
                    Session["_appLvl"] = _sysApp.Sarp_app_lvl;
                }

                if (Session["_isAppUser"].ToString() == "true")
                {
                    //lbtnApprovalSave.Enabled = true;
                    //lbtnApprovalSave.OnClientClick = "ApprovalConfirm();";
                    //lbtnApprovalSave.CssClass = "buttonUndocolor";
                    //lbtnReject.Enabled = true;
                    //lbtnReject.OnClientClick = "RejectConfirm();";
                    //lbtnReject.CssClass = "buttonUndocolor";
                    txtReqLoc.Enabled = true;
                }
                else
                {
                    //lbtnApprovalSave.Enabled = false;
                    //lbtnApprovalSave.OnClientClick = "return Enable();";
                    //lbtnApprovalSave.CssClass = "buttoncolor";
                    //lbtnReject.Enabled = false;
                    //lbtnReject.OnClientClick = "return Enable();";
                    //lbtnReject.CssClass = "buttoncolor";
                    txtReqLoc.Enabled = false;
                }

                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", Session["UserID"].ToString(), txtReqLoc.Text);
                }

                grdPendings.AutoGenerateColumns = false;
                grdPendings.DataSource = new List<RequestApprovalHeader>();
                grdPendings.DataSource = _TempReqAppHdr;
                grdPendings.DataBind();

                grdInvItem.AutoGenerateColumns = false;
                grdInvItem.DataSource = new List<InvoiceItem>();
                grdInvItem.DataBind();

                for (int i = 0; i < grdInvItem.Rows.Count; i++)
                {
                    TextBox tb = ((TextBox)grdInvItem.Rows[i].FindControl("col_invRevQty"));
                    tb.Enabled = true;
                }

                grdDelDetails.AutoGenerateColumns = false;
                grdDelDetails.DataSource = new List<ReptPickSerials>();
                Session["gvSerData"] = new List<ReptPickSerials>();
                grdDelDetails.DataBind();

                grdSelectReversal.AutoGenerateColumns = false;
                grdSelectReversal.DataSource = new List<ReptPickSerials>();
                grdSelectReversal.DataBind();


                grdRegDetails.AutoGenerateColumns = false;
                grdRegDetails.DataSource = new List<VehicalRegistration>();
                grdRegDetails.DataBind();

                grdInsDetails.AutoGenerateColumns = false;
                grdInsDetails.DataSource = new List<VehicleInsuarance>();
                grdInsDetails.DataBind();

                String _tempDefBin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (!string.IsNullOrEmpty(_tempDefBin))
                {
                    _defBin = _tempDefBin;
                    Session["_defBin"] = _tempDefBin;
                }
                else
                {
                    _defBin = "";
                    Session["_defBin"] = "";
                }

                txtCusCode.Focus();
                lblSerItem.Text = "";
                lblSerial.Text = "";
                lblWarranty.Text = "";
                lblSerRem.Text = "";
                txtJobNo.Text = "";

                grdSerApp.AutoGenerateColumns = false;
                grdSerApp.DataSource = new List<RequestApprovalSerials>();
                grdSerApp.DataBind();
                grdRereportItems.AutoGenerateColumns = false;
                grdRereportItems.DataSource = new List<RequestAppAddDet>();
                grdRereportItems.DataBind();


                SystemAppLevelParam _aodApp = new SystemAppLevelParam();

                _aodApp = CHNLSVC.Sales.CheckApprovePermission("ARQT038", Session["UserID"].ToString());

                if (_aodApp.Sarp_cd != null)
                {
                    txtActLoc.Enabled = true;
                    lbtnSearchActLoc.Enabled = true;
                    lbtnSearchActLoc.OnClientClick = "SaveConfirm();";
                    lbtnSearchActLoc.CssClass = "buttonUndocolor";
                    txtActLoc.Text = "";
                }
                else
                {
                    txtActLoc.Text = "";
                    txtActLoc.Enabled = false;

                    lbtnSearchActLoc.Enabled = false;
                    lbtnSearchActLoc.OnClientClick = "return Enable();";
                    lbtnSearchActLoc.CssClass = "buttoncolor";

                }

                //tbMain.Enabled = true;

                txtSItem.Text = string.Empty;
                txtSSerial.Text = string.Empty;

                ddlChangestatus.SelectedIndex = -1;

                grdPaymentDetails.DataSource = new int[] { };
                grdPaymentDetails.DataBind();


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
            }
            catch (Exception err)
            {
                string _Msg = "Please Reload Page";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        private void LoadAppLevelStatus()
        {
            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr1 = dt.Rows[i];
                if (dr1["MIC_CD"].ToString() == "CONS")
                    dr1.Delete();
            }
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                foreach (GridViewRow row in grdDelDetails.Rows)
                {
                    DropDownList col_appstatus = (DropDownList)row.FindControl("col_appstatus");
                    col_appstatus.DataSource = dt;
                    col_appstatus.DataTextField = "MIS_DESC";
                    col_appstatus.DataValueField = "MIC_CD";
                    col_appstatus.DataBind();
                    col_appstatus.Items.Insert(0, "--Select--");

                    ddlChangestatus.DataSource = dt;
                    ddlChangestatus.DataTextField = "MIS_DESC";
                    ddlChangestatus.DataValueField = "MIC_CD";
                    ddlChangestatus.DataBind();
                    ddlChangestatus.Items.Insert(0, "--Select--");


                }

                ddlChangestatus.DataSource = dt;
                ddlChangestatus.DataTextField = "MIS_DESC";
                ddlChangestatus.DataValueField = "MIC_CD";
                ddlChangestatus.DataBind();
                ddlChangestatus.Items.Insert(0, "--Select--");
                //foreach (GridViewRow row in grdSelectReversal.Rows)
                //{
                //    DropDownList colR_appstatus = (DropDownList)row.FindControl("colR_appstatus");
                //    colR_appstatus.DataSource = dt;
                //    colR_appstatus.DataTextField = "MIS_DESC";
                //    colR_appstatus.DataValueField = "MIC_CD";
                //    colR_appstatus.DataBind();
                //    colR_appstatus.Items.Insert(0, "--Select--");
                //}
            }


        }
        private bool IsValidAdjustmentSubType()
        {
            bool status = false;
            txtSubType.Text = txtSubType.Text.Trim().ToUpper().ToString();
            DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("SRN", txtSubType.Text.ToString());
            if (_adjSubType.Rows.Count > 0)
            {
                lblSubDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                Session["_lblSubDesc"] = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        private void Load_InvoiceDetails(string _pc)
        {

            try
            {
                decimal _unitAmt = 0;
                decimal _disAmt = 0;
                decimal _taxAmt = 0;
                decimal _totAmt = 0;
                string _type = "";
                lbtnRequest.Enabled = true;
                lbtnRequest.OnClientClick = "RequestConfirm();";
                lbtnRequest.CssClass = "buttonUndocolor";
                List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                List<InvoiceItem> _InvList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                _doitemserials = new List<ReptPickSerials>();
                _regDetails = new List<VehicalRegistration>();
                _type = null;

                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                if (_isFromReq == false)
                {
                    _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), _type);
                }
                else if (_isFromReq == true)
                {
                    _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(txtInvoice.Text.Trim(), _type, Session["UserCompanyCode"].ToString(), _pc, lblReq.Text.Trim());
                }
                if (_paramInvoiceItems.Count > 0)
                {
                    List<InvoiceItem> _BACKITEM = new List<InvoiceItem>();
                    _BACKITEM = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());
                    foreach (InvoiceItem item in _paramInvoiceItems)
                    {
                        decimal qty = _BACKITEM.Find(x => x.Sad_itm_line == item.Sad_itm_line).Sad_qty;
                        //_unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));

                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                        item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                        item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                        item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                        item.Cus_ITM_QTY = qty;
                        if (_isFromReq == false)
                        {
                            item.Sad_srn_qty = 0;
                        }

                        _InvList.Add(item);

                        if (_isFromReq == false)
                        {

                            if (chkserial.Checked)
                            {
                                _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForReversal(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), Session["_defBin"].ToString(), item.Sad_inv_no, item.Sad_itm_line);

                                _doitemserials.AddRange(_tempDOSerials);
                            }
                        }

                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            item.Sad_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == item.Sad_itm_stus).Mis_desc;
                        }

                    }

                    if (_isFromReq == true)
                    {
                        _tempDOSerials = CHNLSVC.Inventory.GetRevReqSerial(Session["UserCompanyCode"].ToString(), lblReturnLoc.Text.Trim(), Session["UserID"].ToString(), Session["SessionID"].ToString(), Session["_defBin"].ToString(), txtInvoice.Text.Trim(), lblReq.Text.Trim());
                        _doitemserials.AddRange(_tempDOSerials);

                    }

                }
                else
                {

                    // string _Msg = "Details cannot found for " + _type + " Sales.";
                    string _Msg = "Cannot find details for the sales reversal";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    lbtnRequest.Enabled = false;
                    lbtnRequest.OnClientClick = "return Enable();";
                    lbtnRequest.CssClass = "buttoncolor";
                }

                _InvDetailList = _InvList;
                var newList = _InvDetailList.OrderBy(x => x.Sad_itm_line).ToList();
                MasterItem _item = new MasterItem();
                foreach (var item in newList)
                {

                    _item = new MasterItem();
                    _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Sad_itm_cd);
                    if (_item != null)
                    {
                        if (_item.Mi_model != null)
                        {
                            newList.Where(c => c.Sad_itm_cd == _item.Mi_cd).FirstOrDefault().Mi_model = _item.Mi_model;
                        }
                    }
                }
                grdInvItem.AutoGenerateColumns = false;
                grdInvItem.DataSource = new List<InvoiceItem>();
                newList = newList.OrderBy(c => c.Sad_itm_cd).ToList();
                grdInvItem.DataSource = newList;//_InvDetailList;
                grdInvItem.DataBind();
                ViewState["_InvDetailList"] = _InvDetailList;
                //for (int i = 0; i < grdInvItem.Rows.Count; i++)
                //{
                //    Label _itm = ((Label)grdInvItem.Rows[i].FindControl("col_invItem"));
                //    var _chkitm = _doitemserials.Where(x => x.Tus_itm_cd == _itm.Text).ToList();
                //    if (_chkitm.Count > 0)
                //    {
                //        TextBox tb = ((TextBox)grdInvItem.Rows[i].FindControl("col_invRevQty"));
                //        tb.Enabled = false;
                //    }

                //}
                if (_doitemserials != null)
                {
                    int i = 1;
                    foreach (ReptPickSerials _Itm in _doitemserials)
                    {
                        _Itm.Tus_temp_itm_line = i;
                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            _Itm.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == _Itm.Tus_itm_stus).Mis_desc;
                            if ((_Itm.Tus_appstatus != "") && (_Itm.Tus_appstatus != null))
                            {
                                _Itm.Tus_new_status_Desc = oItemStaus.Find(x => x.Mis_cd == _Itm.Tus_appstatus).Mis_desc;
                            }
                        }
                        i++;
                    }



                    grdDelDetails.AutoGenerateColumns = false;
                    grdDelDetails.DataSource = new List<ReptPickSerials>();
                    grdDelDetails.DataSource = _doitemserials;
                    Session["gvSerData"] = _doitemserials;
                    grdDelDetails.DataBind();
                    if (_isFromReq == true)
                    {
                        //grdSelectReversal.AutoGenerateColumns = false;
                        //grdSelectReversal.DataSource = new List<ReptPickSerials>();
                        //grdSelectReversal.DataSource = _doitemserials;
                        //grdSelectReversal.DataBind();
                        //ViewState["RevsFilterSerial"] = _doitemserials;
                        ////
                        List<string> itemCodes = new List<string>();
                        #region Dulaj 2018/Oct/19 Sales Reversal New Process
                        DataTable dtTempHdr = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), txtInvoice.Text);
                        List<ReptPickSerials> tempPickSer = new List<ReptPickSerials>();
                        if (dtTempHdr != null)
                        {
                            if (dtTempHdr.Rows.Count > 0)
                            {
                                ReptPickSerials rept = new ReptPickSerials();

                                rept.Tus_usrseq_no = Convert.ToInt32(dtTempHdr.Rows[0]["tuh_usrseq_no"].ToString());
                                tempPickSer = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(rept);

                                List<ReptPickSerials> newReptPickSerials = new List<ReptPickSerials>();
                                if (tempPickSer != null)
                                {
                                    if (tempPickSer.Count > 0)
                                    {

                                        foreach (ReptPickSerials serilas in tempPickSer)
                                        {
                                            //  tempPickSer.Where(x => _doitemserials.Contains(x.Tus_serial_id));
                                            //var result = _doitemserials.First(x => x.Tus_serial_id.Equals(serilas.Tus_serial_id)).First();
                                            foreach (ReptPickSerials serilasPick in _doitemserials)
                                            {
                                                if (serilasPick.Tus_ser_id.Equals(serilas.Tus_ser_id))
                                                {
                                                    newReptPickSerials.Add(serilasPick);
                                                    itemCodes.Add(serilasPick.Tus_itm_cd);
                                                }
                                            }

                                        }
                                        _doitemserials = newReptPickSerials;
                                    }
                                }
                            }
                        }

                        if (itemCodes.Count > 0)
                        {
                            itemCodes = itemCodes.Distinct().ToList();
                            List<InvoiceItem> inItemList = new List<InvoiceItem>();
                            foreach (string itemcd in itemCodes)
                            {
                                var _filerdItem = _InvDetailList.Where(x => x.Sad_itm_cd.Equals(itemcd)).FirstOrDefault();
                                if (_filerdItem != null)
                                {
                                    inItemList.Add(_filerdItem);
                                }
                            }
                            if (inItemList.Count > 0)
                            {
                                _InvDetailList = inItemList;
                            }
                            grdInvItem.AutoGenerateColumns = false;
                            grdInvItem.DataSource = new List<InvoiceItem>();
                            newList = newList.OrderBy(c => c.Sad_itm_cd).ToList();
                            grdInvItem.DataSource = newList;//_InvDetailList;
                            grdInvItem.DataBind();
                            ViewState["_InvDetailList"] = _InvDetailList;
                        }
                        #endregion
                        grdSelectReversal.AutoGenerateColumns = false;
                        grdSelectReversal.DataSource = new List<ReptPickSerials>();
                        grdSelectReversal.DataSource = _doitemserials;
                        grdSelectReversal.DataBind();
                        ViewState["RevsFilterSerial"] = _doitemserials;
                        //////
                    }


                    ViewState["_olddoitemserials"] = _doitemserials;
                    ViewState["_doitemserials"] = _doitemserials;
                    ViewState["_doitemserials_backup"] = _doitemserials;

                }
                else
                {
                    for (int i = 0; i < grdInvItem.Rows.Count; i++)
                    {
                        TextBox tb = ((TextBox)grdInvItem.Rows[i].FindControl("col_invRevQty"));
                        tb.Enabled = false;
                    }
                }
                LoadAppLevelStatus();

            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Fail to retrive data due to records missmatch.');", true);
            }

        }
        private void LoadInvoiceDetails()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                {
                    _isFromReq = false;
                    Session["_isFromReq"] = "false";
                    lblReq.Text = "";
                    lblStatus.Text = "";

                    lblSerItem.Text = "";
                    lblSerial.Text = "";
                    lblWarranty.Text = "";
                    lblSerRem.Text = "";
                    txtJobNo.Text = "";

                    _repSer = new List<RequestApprovalSerials>();

                    grdSerApp.AutoGenerateColumns = false;
                    grdSerApp.DataSource = new List<RequestApprovalSerials>();
                    grdSerApp.DataBind();
                    _repAddDet = new List<RequestAppAddDet>();

                    grdRereportItems.AutoGenerateColumns = false;
                    grdRereportItems.DataSource = new List<RequestAppAddDet>();
                    grdRereportItems.DataBind();

                    lblRevItem.Text = "";
                    txtItem.Text = "";
                    lblInvLine.Text = "";
                    lblRepPrice.Text = "";
                    txtNewSerial.Text = "";
                    txtNewPrice.Text = "";


                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    if (chkOthSales.Checked == false)
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
                    }
                    else
                    {
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), null, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
                    }

                    if (_invHdr.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid invoice number.');document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        _dCusCode = "";
                        Session["_dCusCode"] = "";
                        _dCusAdd1 = "";
                        Session["_dCusAdd1"] = "";
                        _dCusAdd2 = "";
                        Session["_dCusAdd2"] = "";
                        _currency = "";
                        Session["_currency"] = "";
                        _exRate = 0;
                        Session["_exRate"] = 0;
                        _invTP = "";
                        Session["_invTP"] = "";
                        lblSalePc.Text = "";
                        _executiveCD = "";
                        Session["_executiveCD"] = "";
                        _manCode = "";
                        Session["_manCode"] = "";
                        _isTax = false;
                        Session["_isTax"] = "false";
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        foreach (InvoiceHeader _tempInv in _invHdr)
                        {
                            if (_tempInv.Sah_inv_tp == "HS")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid invoice.');document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                                txterrorno.Value = "1";
                                txtInvoice.Text = "";
                                return;
                            }

                            if (_tempInv.Sah_stus != "R")
                            {
                                txtCusCode.Text = _tempInv.Sah_cus_cd;
                                lblInvCusName.Text = _tempInv.Sah_cus_name;
                                lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                                lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                                lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                                lblSalePc.Text = _tempInv.Sah_pc;
                                _dCusCode = _tempInv.Sah_d_cust_cd;

                                //added by Wimal @ 29/06/2018 to correct credit note value
                                lblTotInvAmt.Text = _tempInv.Sah_anal_7.ToString("n");
                                lblTotPayAmt.Text = _tempInv.Sah_anal_8.ToString("n");
                                lblOutAmt.Text = (_tempInv.Sah_anal_7 - _tempInv.Sah_anal_8).ToString("n");

                                Session["_dCusCode"] = _tempInv.Sah_d_cust_cd;
                                _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                                Session["_dCusAdd1"] = _tempInv.Sah_d_cust_add1;
                                _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                                Session["_dCusAdd2"] = _tempInv.Sah_d_cust_add2;
                                _currency = _tempInv.Sah_currency;
                                Session["_currency"] = _tempInv.Sah_currency;
                                _exRate = _tempInv.Sah_ex_rt;
                                Session["_exRate"] = _tempInv.Sah_ex_rt;
                                _invTP = _tempInv.Sah_inv_tp;
                                Session["_invTP"] = _tempInv.Sah_inv_tp;
                                _executiveCD = _tempInv.Sah_sales_ex_cd;
                                Session["_executiveCD"] = _tempInv.Sah_sales_ex_cd;
                                _manCode = _tempInv.Sah_man_cd;
                                Session["_manCode"] = _tempInv.Sah_man_cd;
                                _isTax = _tempInv.Sah_tax_inv;
                                Session["_isTax"] = _tempInv.Sah_tax_inv;
                                chkOthSales.Enabled = false;
                                Load_InvoiceDetails(Session["UserDefProf"].ToString());
                                lbtnCustomer.Visible = false;
                                txtCusCode.Enabled = false;
                                getPDASerial();
                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invoice is already reversed.');document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                                txtInvoice.Text = "";
                                txterrorno.Value = "2";
                                return;
                            }
                        }
                    }

                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }

        }

        private void Load_Registration_Details(string _com, string _pc, string _appCode, string _revReqNo)
        {
            _regDetails = new List<VehicalRegistration>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempAddReg = new VehicalRegistration();
            Int32 I = 0;

            RequestApprovalHeader _tempRegReq = new RequestApprovalHeader();
            _tempRegReq = CHNLSVC.General.GetRelatedRequestByRef(_com, _pc, _revReqNo, _appCode);

            if (_tempRegReq != null)
            {
                lblRegReq.Text = _tempRegReq.Grah_ref;
                if (_tempRegReq.Grah_app_stus == "A")
                {
                    lblRegStatus.Text = "APPROVED";
                    chkRevReg.Checked = true;
                    chkRevReg.Enabled = false;

                    lbtnRegDetails.Enabled = true;
                    lbtnRegDetails.OnClientClick = "SaveConfirm();";
                    lbtnRegDetails.CssClass = "buttonUndocolor";

                }
                else if (_tempRegReq.Grah_app_stus == "P")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Related registration refund request is still not approved. You cannot generate registration refund at this time. You have to refund it seperatly after getting approval.');", true);

                    // MessageBox.Show("Related registration refund request is still not approved. You cannot generate registration refund at this time. You have to refund it seperatly after getting approval.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "PENDING";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
                else if (_tempRegReq.Grah_app_stus == "R")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Related registration refund request is rejected. Please contact registration department.');", true);

                    // MessageBox.Show("Related registration refund request is rejected. Please contact registration department.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "REJECT";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
                else if (_tempRegReq.Grah_app_stus == "F")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Related registration refund is already done.');", true);

                    //MessageBox.Show("Related registration refund is already done.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRegStatus.Text = "FINISHED";
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }

                //load request registration refund details
                _tempReg = CHNLSVC.Sales.GetRefundReqVehReg(Session["UserCompanyCode"].ToString(), _pc, lblRegReq.Text.Trim(), "ARQT016");
                chkRevReg.Checked = true;
                chkRevReg.Enabled = false;
                foreach (VehicalRegistration regDet in _tempReg)
                {
                    if (regDet.P_svrt_prnt_stus == 2)
                    {
                        string _Msg = "Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already refunded.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        // MessageBox.Show("Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        // goto skipAdd;
                    }
                    else if (regDet.P_srvt_rmv_stus != 0)
                    {
                        string _Msg = "Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already send to RMV.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        //MessageBox.Show("Request engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already send to RMV.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        //goto skipAdd;
                    }

                    _tempAddReg = regDet;
                    _regDetails.Add(_tempAddReg);
                    //skipAdd:
                    I = I + 1;
                }


                grdRegDetails.AutoGenerateColumns = false;
                grdRegDetails.DataSource = new List<VehicalRegistration>();
                grdRegDetails.DataSource = _regDetails;
                grdRegDetails.DataBind();
                ViewState["_regDetails"] = _regDetails;
                if (grdRegDetails.Rows.Count == 0)
                {
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                }
            }

        }
        private void Load_Insuarance_Details(string _com, string _pc, string _appCode, string _revReqNo)
        {
            _insDetails = new List<VehicleInsuarance>();
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempAddIns = new VehicleInsuarance();
            Int32 I = 0;

            RequestApprovalHeader _tempInsReq = new RequestApprovalHeader();
            _tempInsReq = CHNLSVC.General.GetRelatedRequestByRef(_com, _pc, _revReqNo, _appCode);

            if (_tempInsReq != null)
            {
                lblInsReq.Text = _tempInsReq.Grah_ref;
                if (_tempInsReq.Grah_app_stus == "A")
                {
                    lblInsStatus.Text = "APPROVED";
                    chkRevIns.Checked = true;
                    chkRevIns.Enabled = false;

                    lbtnRegDetails.Enabled = true;
                    lbtnRegDetails.OnClientClick = "SaveConfirm();";
                    lbtnRegDetails.CssClass = "buttonUndocolor";
                }
                else if (_tempInsReq.Grah_app_stus == "P")
                {
                    string _Msg = "Related insuarance refund request is still not approved. You cannot generate insuarance refund at this time. You have to refund it seperatly after getting approval.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    //.Show("Related insuarance refund request is still not approved. You cannot generate insuarance refund at this time. You have to refund it seperatly after getting approval.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "PENDING";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }
                else if (_tempInsReq.Grah_app_stus == "R")
                {
                    string _Msg = "Related insuarance refund request is rejected. Please contact insuarace dept. for more information.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    //MessageBox.Show("Related insuarance refund request is rejected. Please contact insuarace dept. for more information.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "REJECT";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }
                else if (_tempInsReq.Grah_app_stus == "F")
                {
                    string _Msg = "Related insuarance refund is already done.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    //MessageBox.Show("Related insuarance refund is already done.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInsStatus.Text = "FINISHED";
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;
                }

                //load request registration refund details
                _tempIns = CHNLSVC.Sales.GetRefundReqVehIns(Session["UserCompanyCode"].ToString(), _pc, lblInsReq.Text.Trim(), "ARQT017");

                foreach (VehicleInsuarance insDet in _tempIns)
                {
                    if (insDet.Svit_cvnt_issue == 2)
                    {
                        string _Msg = "Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already refunded.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        //MessageBox.Show("Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already refunded.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        // goto skipAdd;
                    }
                    else if (insDet.Svit_polc_stus == true)
                    {
                        string _Msg = "Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already issue policy.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        // MessageBox.Show("Request engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already issue policy.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        //goto skipAdd;
                    }

                    _tempAddIns = insDet;
                    _insDetails.Add(_tempAddIns);
                    //skipAdd:
                    I = I + 1;
                }


                grdInsDetails.AutoGenerateColumns = false;
                grdInsDetails.DataSource = new List<VehicalRegistration>();
                grdInsDetails.DataSource = _insDetails;
                grdInsDetails.DataBind();
                ViewState["_insDetails"] = _insDetails;
            }

        }

        protected void CollectRefApp()
        {
            string _type = "";

            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            _refAppHdr = new RequestApprovalHeader();
            _refAppDet = new List<RequestApprovalDetail>();
            _refAppAuto = new MasterAutoNumber();


            _refAppHdr.Grah_com = Session["UserCompanyCode"].ToString();
            _refAppHdr.Grah_loc = Session["UserDefProf"].ToString();
            _refAppHdr.Grah_app_tp = "ARQT022";
            _refAppHdr.Grah_fuc_cd = null;
            _refAppHdr.Grah_ref = null;
            _refAppHdr.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _refAppHdr.Grah_cre_by = Session["UserID"].ToString();
            _refAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_mod_by = Session["UserID"].ToString();
            _refAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_app_stus = "P";
            _refAppHdr.Grah_app_lvl = 0;
            _refAppHdr.Grah_app_by = string.Empty;
            _refAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _refAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _refAppHdr.Grah_sub_type = txtSubType.Text.Trim();
            _refAppHdr.Grah_oth_pc = lblSalePc.Text.Trim();



            _tempReqAppDet = new RequestApprovalDetail();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(lblCrAmt.Text);
            _tempReqAppDet.Grad_val2 = 0;
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal2 = "";
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;

            _refAppDet.Add(_tempReqAppDet);



            _refAppAuto = new MasterAutoNumber();
            _refAppAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _refAppAuto.Aut_cate_tp = "PC";
            _refAppAuto.Aut_direction = 1;
            _refAppAuto.Aut_modify_dt = null;
            _refAppAuto.Aut_moduleid = "REQ";
            _refAppAuto.Aut_number = 0;
            _refAppAuto.Aut_start_char = "CSREF";
            _refAppAuto.Aut_year = null;
        }
        protected void CollectRefAppLog()
        {
            string _type = "";
            _refAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();

            _ReqAppHdrLog.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqAppHdrLog.Grah_loc = Session["UserDefProf"].ToString();
            _ReqAppHdrLog.Grah_app_tp = "ARQT022";
            _ReqAppHdrLog.Grah_fuc_cd = null;
            _ReqAppHdrLog.Grah_ref = null;
            _ReqAppHdrLog.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqAppHdrLog.Grah_cre_by = Session["UserID"].ToString();
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = Session["UserID"].ToString();
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = "P";
            _ReqAppHdrLog.Grah_app_lvl = 0;
            _ReqAppHdrLog.Grah_app_by = string.Empty;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdrLog.Grah_sub_type = txtSubType.Text.Trim();
            _ReqAppHdrLog.Grah_oth_pc = lblSalePc.Text.Trim();


            _tempReqAppDet = new RequestApprovalDetailLog();
            _tempReqAppDet.Grad_ref = null;
            _tempReqAppDet.Grad_line = 1;
            _tempReqAppDet.Grad_req_param = "SALES CASH REFUND";
            _tempReqAppDet.Grad_val1 = Convert.ToDecimal(lblCrAmt.Text);
            _tempReqAppDet.Grad_val2 = 0;
            _tempReqAppDet.Grad_val3 = 0;
            _tempReqAppDet.Grad_val4 = 0;
            _tempReqAppDet.Grad_val5 = 0;
            _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
            _tempReqAppDet.Grad_anal2 = "";
            _tempReqAppDet.Grad_anal3 = "";
            _tempReqAppDet.Grad_anal4 = "";
            _tempReqAppDet.Grad_anal5 = "";
            _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
            _tempReqAppDet.Grad_is_rt1 = false;
            _tempReqAppDet.Grad_is_rt2 = false;
            _ReqAppDetLog.Add(_tempReqAppDet);


        }

        private bool ChechServiceInvoice()
        {
            bool status = false;
            String err;
            InvoiceHeader _header = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text.Trim().ToUpper());
            if (_header.Sah_anal_2 == "SCV")
            {
                // Check ADO in by other location
                status = CHNLSVC.CustService.CheckPendignAODForInvoiceReversal(txtInvoice.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), out err);
                if (status == false)
                {

                    string _Msg = "Cannot cancel this invoice. ADO in by other location";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    return status;
                }

                //Check warranty replacements
                List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoice.Text.Trim());
                var Job_n_Lines = _list.Select(e => new { e.Sad_job_no, e.Sad_job_line }).Distinct();
                foreach (var Job_n_Line in Job_n_Lines)
                {
                    DataTable dtDocs;
                    DataTable dtRecSerial;
                    DataTable dtIssuedSer;
                    DataTable dtRequest = CHNLSVC.CustService.GET_WRR_RPLC_DETAILS(Session["UserCompanyCode"].ToString(), Job_n_Line.Sad_job_no, out dtDocs, out dtRecSerial, out dtIssuedSer);
                    if (dtRequest != null && dtRequest.Rows.Count > 0)
                    {
                        if (dtRequest.Select("GRAH_APP_STUS = 'F' OR GRAH_APP_STUS = 'N'").Length > 0)
                        {
                            string _Msg = "Cannot cancel this invoice. Warranty replacements details found.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                            status = false;
                            return status;
                        }
                    }
                }
            }
            else
            {
                status = true;
            }
            return status;
        }
        protected void CollectReqApp()
        {
            string _type = "";
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            _ReqAppHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqAppDet = new List<RequestApprovalDetail>();
            _ReqAppSer = new List<RequestApprovalSerials>();
            _ReqAppAuto = new MasterAutoNumber();
            // List<InvoiceItem> _GetInvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;


            _ReqAppHdr.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqAppHdr.Grah_loc = Session["UserDefProf"].ToString();
            _ReqAppHdr.Grah_app_tp = "ARQT014";
            _ReqAppHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdr.Grah_ref = null;
            _ReqAppHdr.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqAppHdr.Grah_cre_by = Session["UserID"].ToString();
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = Session["UserID"].ToString();
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = Session["_status"].ToString();//_status;
            _ReqAppHdr.Grah_app_lvl = 0;
            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdr.Grah_sub_type = txtSubType.Text.Trim();
            _ReqAppHdr.Grah_oth_pc = lblSalePc.Text.Trim();
            _InvDetailList.RemoveAll(x => x.Sad_srn_qty == 0);

            if (_InvDetailList.Count > 0 && _InvDetailList != null)//(_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)//_InvDetailList
                {
                    _tempReqAppDet = new RequestApprovalDetail();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "SALES REVERSAL";
                    _tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    //Add Grad_anal15 column by Chamal 17-07-2015
                    if (string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    { _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd; }
                    else
                    { _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd; }
                    try
                    {
                        // List<string> values = grdSelectReversal.Rows.Cast<GridViewRow>().Where(row => (string)row.Cells[5].Text == item.Sad_itm_cd).Select(row => (string)row.Cells[6].Text).ToList<string>();
                        foreach (GridViewRow _row in grdSelectReversal.Rows)
                        {
                            string itemcode = ((Label)_row.FindControl("colR_item")).Text;
                            string status = ((Label)_row.FindControl("colR_appstatus")).Text;
                            if (itemcode == item.Sad_itm_cd)
                            {
                                _tempReqAppDet.Grad_anal8 = status;
                            }
                        }
                        //  IEnumerable<GridViewRow> rows = MainGridView.Rows.Cast<GridViewRow>().Where(row => row.Cells[0].Text == "Total" || row.Cells[1].Text == "Total");

                    }
                    catch (Exception) { _tempReqAppDet.Grad_anal8 = item.Mi_itm_stus; }

                    _ReqAppDet.Add(_tempReqAppDet);
                }
            }

            if (_doitemserials == null)
            {
                _doitemserials = new List<ReptPickSerials>();
            }
            if (_doitemserials.Count > 0 && _doitemserials != null)
            {
                _repSer = ViewState["_repSer"] as List<RequestApprovalSerials>;
                if (_repSer == null)
                {
                    _repSer = new List<RequestApprovalSerials>();
                }
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    _tempReqAppSer.Gras_anal5 = "";
                    if (_repSer.Count > 0)
                    {
                        foreach (RequestApprovalSerials _tmp in _repSer)
                        {
                            if (_tmp.Gras_anal2 == ser.Tus_itm_cd && _tmp.Gras_anal3 == ser.Tus_ser_1)
                            {
                                _tempReqAppSer.Gras_anal5 = _tmp.Gras_anal5;

                            }
                        }
                    }
                    _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqAppSer.Add(_tempReqAppSer);
                }
            }



            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "RVREQ";
            _ReqAppAuto.Aut_year = null;
        }

        protected void CollectReqAppLog()
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;

            string _type = "";
            _ReqAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();
            _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

            _ReqAppHdrLog.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqAppHdrLog.Grah_loc = Session["UserDefProf"].ToString();
            _ReqAppHdrLog.Grah_app_tp = "ARQT014";
            _ReqAppHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdrLog.Grah_ref = null;
            _ReqAppHdrLog.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqAppHdrLog.Grah_cre_by = Session["UserID"].ToString();
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = Session["UserID"].ToString();
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = Session["_status"].ToString();//_status;
            _ReqAppHdrLog.Grah_app_lvl = 0;
            _ReqAppHdrLog.Grah_app_by = string.Empty;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_remaks = txtRemarks.Text.Trim();
            _ReqAppHdrLog.Grah_sub_type = txtSubType.Text.Trim();
            _ReqAppHdrLog.Grah_oth_pc = lblSalePc.Text.Trim();

            if ((_InvDetailList.Count > 0) && (_InvDetailList != null))
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "SALES REVERSAL";
                    _tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    //Add Grad_anal15 column by Darshana 20-07-2015
                    if (string.IsNullOrEmpty(item.Sad_sim_itm_cd))
                    { _tempReqAppDet.Grad_anal15 = item.Sad_itm_cd; }
                    else
                    { _tempReqAppDet.Grad_anal15 = item.Sad_sim_itm_cd; }
                    _ReqAppDetLog.Add(_tempReqAppDet);
                }
            }
            if (_doitemserials != null)
            {
                if ((_doitemserials.Count > 0) && (_doitemserials != null))
                {
                    Int32 _line = 0;
                    foreach (ReptPickSerials ser in _doitemserials)
                    {
                        _line = _line + 1;
                        _tempReqAppSer = new RequestApprovalSerialsLog();
                        _tempReqAppSer.Gras_ref = null;
                        _tempReqAppSer.Gras_line = _line;
                        _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                        _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                        _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                        _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                        _tempReqAppSer.Gras_anal5 = "";
                        foreach (RequestApprovalSerials _tmp in _repSer)
                        {
                            if (_tmp.Gras_anal2 == ser.Tus_itm_cd && _tmp.Gras_anal3 == ser.Tus_ser_1)
                            {
                                _tempReqAppSer.Gras_anal5 = _tmp.Gras_anal5;

                            }
                        }
                        _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                        _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                        _tempReqAppSer.Gras_anal8 = 0;
                        _tempReqAppSer.Gras_anal9 = 0;
                        _tempReqAppSer.Gras_anal10 = 0;
                        _tempReqAppSer.Gras_lvl = 0;
                        _ReqAppSerLog.Add(_tempReqAppSer);
                    }
                }
            }

        }
        protected void CollectRegApp()
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            _regDetails = ViewState["_regDetails"] as List<VehicalRegistration>;
            string _type = "";
            _ReqRegHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqRegDet = new List<RequestApprovalDetail>();
            _ReqRegSer = new List<RequestApprovalSerials>();
            _ReqRegAuto = new MasterAutoNumber();

            _ReqRegHdr.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqRegHdr.Grah_loc = Session["UserDefProf"].ToString();
            _ReqRegHdr.Grah_app_tp = "ARQT016";
            _ReqRegHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqRegHdr.Grah_ref = null;
            _ReqRegHdr.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqRegHdr.Grah_cre_by = Session["UserID"].ToString();
            _ReqRegHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_mod_by = Session["UserID"].ToString();
            _ReqRegHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_app_stus = "P";
            _ReqRegHdr.Grah_app_lvl = 0;
            _ReqRegHdr.Grah_app_by = string.Empty;
            _ReqRegHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdr.Grah_remaks = txtRemarks.Text.Trim();

            decimal _regQty = 0;
            if (_InvDetailList.Count > 0 && _InvDetailList != null)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _regQty = 0;
                    foreach (VehicalRegistration tmpReg in _regDetails)
                    {
                        if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                        {
                            _regQty = _regQty + 1;
                        }
                    }

                    if (_regQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetail();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL REGISTRATION REQUEST";
                        _tempReqAppDet.Grad_val1 = _regQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _regQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqRegDet.Add(_tempReqAppDet);
                    }
                }
            }

            if (_regDetails.Count > 0 && _regDetails != null)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqRegSer.Add(_tempReqAppSer);
                }
            }


            _ReqRegAuto = new MasterAutoNumber();
            _ReqRegAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _ReqRegAuto.Aut_cate_tp = "PC";
            _ReqRegAuto.Aut_direction = 1;
            _ReqRegAuto.Aut_modify_dt = null;
            _ReqRegAuto.Aut_moduleid = "REQ";
            _ReqRegAuto.Aut_number = 0;
            _ReqRegAuto.Aut_start_char = "CSREGRF";
            _ReqRegAuto.Aut_year = null;
        }
        protected void CollectRegAppLog()
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            _regDetails = ViewState["_regDetails"] as List<VehicalRegistration>;

            string _type = "";
            _ReqRegHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqRegDetLog = new List<RequestApprovalDetailLog>();
            _ReqRegSerLog = new List<RequestApprovalSerialsLog>();


            _ReqRegHdrLog.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqRegHdrLog.Grah_loc = Session["UserDefProf"].ToString();
            _ReqRegHdrLog.Grah_app_tp = "ARQT016";
            _ReqRegHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqRegHdrLog.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqRegHdrLog.Grah_cre_by = Session["UserID"].ToString();
            _ReqRegHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_mod_by = Session["UserID"].ToString();
            _ReqRegHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_app_stus = "P";
            _ReqRegHdrLog.Grah_app_lvl = 0;
            _ReqRegHdrLog.Grah_app_by = string.Empty;
            _ReqRegHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqRegHdrLog.Grah_remaks = txtRemarks.Text.Trim();

            decimal _regQty = 0;

            if (_InvDetailList.Count > 0 && _InvDetailList != null)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _regQty = 0;
                    foreach (VehicalRegistration tmpReg in _regDetails)
                    {
                        if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                        {
                            _regQty = _regQty + 1;
                        }
                    }

                    if (_regQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetailLog();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL REGISTRATION REQUEST";
                        _tempReqAppDet.Grad_val1 = _regQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _regQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqRegDetLog.Add(_tempReqAppDet);
                    }
                }
            }

            if (_regDetails.Count > 0 && _regDetails != null)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqRegSerLog.Add(_tempReqAppSer);
                }
            }


        }
        protected void CollectInsAppLog()
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            _insDetails = ViewState["_insDetails"] as List<VehicleInsuarance>;
            string _type = "";
            _ReqInsHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqInsDetLog = new List<RequestApprovalDetailLog>();
            _ReqInsSerLog = new List<RequestApprovalSerialsLog>();


            _ReqInsHdrLog.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqInsHdrLog.Grah_loc = Session["UserDefProf"].ToString();
            _ReqInsHdrLog.Grah_app_tp = "ARQT017";
            _ReqInsHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqInsHdrLog.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqInsHdrLog.Grah_cre_by = Session["UserID"].ToString();
            _ReqInsHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_mod_by = Session["UserID"].ToString();
            _ReqInsHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_app_stus = "P";
            _ReqInsHdrLog.Grah_app_lvl = 0;
            _ReqInsHdrLog.Grah_app_by = string.Empty;
            _ReqInsHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_remaks = txtRemarks.Text.Trim();

            decimal _insQty = 0;

            if (_InvDetailList.Count > 0 && _InvDetailList != null)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _insQty = 0;
                    foreach (VehicleInsuarance tmpIns in _insDetails)
                    {
                        if (item.Sad_itm_cd == tmpIns.Svit_itm_cd && item.Sad_inv_no == tmpIns.Svit_inv_no)
                        {
                            _insQty = _insQty + 1;
                        }
                    }

                    if (_insQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetailLog();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL INSUARANCE REQUEST";
                        _tempReqAppDet.Grad_val1 = _insQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _insQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqInsDetLog.Add(_tempReqAppDet);
                    }
                }
            }

            if (_insDetails.Count > 0 && _insDetails != null)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqInsSerLog.Add(_tempReqAppSer);
                }
            }


        }
        protected void CollectInsApp()
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            _insDetails = ViewState["_insDetails"] as List<VehicleInsuarance>;
            string _type = "";
            _ReqInsHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqInsDet = new List<RequestApprovalDetail>();
            _ReqInsSer = new List<RequestApprovalSerials>();
            _ReqInsAuto = new MasterAutoNumber();

            _ReqInsHdr.Grah_com = Session["UserCompanyCode"].ToString();
            _ReqInsHdr.Grah_loc = Session["UserDefProf"].ToString();
            _ReqInsHdr.Grah_app_tp = "ARQT017";
            _ReqInsHdr.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqInsHdr.Grah_ref = null;
            _ReqInsHdr.Grah_oth_loc = Session["UserDefLoca"].ToString();
            _ReqInsHdr.Grah_cre_by = Session["UserID"].ToString();
            _ReqInsHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_mod_by = Session["UserID"].ToString();
            _ReqInsHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_app_stus = "P";
            _ReqInsHdr.Grah_app_lvl = 0;
            _ReqInsHdr.Grah_app_by = string.Empty;
            _ReqInsHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdr.Grah_remaks = txtRemarks.Text.Trim();

            decimal _insQty = 0;
            if (_InvDetailList.Count > 0 && _InvDetailList != null)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _insQty = 0;
                    foreach (VehicleInsuarance tmpIns in _insDetails)
                    {
                        if (item.Sad_itm_cd == tmpIns.Svit_itm_cd && item.Sad_inv_no == tmpIns.Svit_inv_no)
                        {
                            _insQty = _insQty + 1;
                        }
                    }

                    if (_insQty > 0)
                    {
                        _tempReqAppDet = new RequestApprovalDetail();
                        _tempReqAppDet.Grad_ref = null;
                        _tempReqAppDet.Grad_line = item.Sad_itm_line;
                        _tempReqAppDet.Grad_req_param = "REVERSAL INSUARANCE REQUEST";
                        _tempReqAppDet.Grad_val1 = _insQty;
                        _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                        _tempReqAppDet.Grad_val3 = _insQty;
                        _tempReqAppDet.Grad_val4 = 0;
                        _tempReqAppDet.Grad_val5 = 0;
                        _tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                        _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                        _tempReqAppDet.Grad_anal3 = "";
                        _tempReqAppDet.Grad_anal4 = "";
                        _tempReqAppDet.Grad_anal5 = "";
                        _tempReqAppDet.Grad_date_param = Convert.ToDateTime(lblInvDate.Text).Date;
                        _tempReqAppDet.Grad_is_rt1 = false;
                        _tempReqAppDet.Grad_is_rt2 = false;

                        _ReqInsDet.Add(_tempReqAppDet);
                    }
                }
            }

            if (_insDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqInsSer.Add(_tempReqAppSer);
                }
            }


            _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = 1;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "REQ";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "CSINSRF";
            _ReqInsAuto.Aut_year = null;
        }

        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    _direction = 0;

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSelectReversal.AutoGenerateColumns = false;
                    grdSelectReversal.DataSource = _emptySer;
                    grdSelectReversal.DataBind();

                    #endregion

                    // InventoryHeader _invHdr = new InventoryHeader();

                    //  _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);


                    //txtUserSeqNo.Text = _invHdr.Ith_entry_no;


                    #region Get Serials
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();


                    _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtTemp.Text);

                    grdSelectReversal.AutoGenerateColumns = false;
                    grdSelectReversal.DataSource = _serList;
                    grdSelectReversal.DataBind();
                    ViewState["TempserList"] = _serList;

                    ViewState["_doitemserials"] = _serList;
                }
                else
                {
                    //MessageBox.Show("Item not found!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTemp.Text = "";
                    txtTemp.Focus();
                    return;
                }
                    #endregion

            }
            catch (Exception err)
            {
                string _Msg = err.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);

            }

        }
        #endregion
        #region Modal Popup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Name = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Category")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
                txtSubType.Text = Name;
                lblSubDesc.Text = Des;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                txtCusCode.Text = Name;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "InvoicebyCustomer")
            {
                txtInvoice.Text = Name;
                LoadInvoiceDetails();
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "InvoiceForReversal")
            {
                txtInvoice.Text = Name;
                LoadInvoiceDetails();
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "InvoiceForReversalOth")
            {
                txtInvoice.Text = Name;
                LoadInvoiceDetails();
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Proftcenter")
            {
                txtReqLoc.Text = Name;
                LoadInvoiceDetails();
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Re-Item")
            {
                txtItem.Text = Name;
                lblvalue.Text = "";
                UserRereportPopoup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "JobNo")
            {
                txtJobNo.Text = Name;
                lblvalue.Text = "";
                UserServiceApprovalPopoup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cancel")
            {
                txtCancelInvoice.Text = Name;
                lblvalue.Text = "";
                userCancelPopoup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "RevRegItem")
            {
                txtRevRegItem.Text = Name;
                lblvalue.Text = "";
                UserCashRefundRequestsPopup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "RevEngine")
            {
                txtRevEngine.Text = Name;
                lblvalue.Text = "";
                UserCashRefundRequestsPopup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "RevInsItem")
            {
                txtRevInsItem.Text = Name;
                lblvalue.Text = "";
                UserCashRefundRequestsPopup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "RevInsEngine")
            {
                txtRevInsEngine.Text = Name;
                lblvalue.Text = "";
                UserCashRefundRequestsPopup.Show();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "SearchActLoc")
            {
                txtActLoc.Text = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                txtInvoiceNo.Text = Name;
                lblvalue.Text = "";
                UserDPopoup.Hide();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;

            if (lblvalue.Text == "Category")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();

                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoicebyCustomer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversal")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversalOth")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proftcenter")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Re-Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserRereportPopoup.Show();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "JobNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeJob);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserServiceApprovalPopoup.Show();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevRegItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "SearchActLoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Category")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoicebyCustomer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversal")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversalOth")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proftcenter")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Re-Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserRereportPopoup.Show();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "JobNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeJob);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserServiceApprovalPopoup.Show();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevRegItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "SearchActLoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Category")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoicebyCustomer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversal")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceForReversalOth")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proftcenter")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Re-Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserRereportPopoup.Show();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "JobNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeJob);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserServiceApprovalPopoup.Show();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchReversal);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevRegItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "RevInsEngine")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
                DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "SearchActLoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
                return;
            }
        }
        #endregion
        #region Modal Popup 2
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;

            if (lblvalue.Text == "TempDoc")
            {
                string Inv = grdResultD.SelectedRow.Cells[5].Text;
                txtTemp.Text = Name;
                txtInvoice.Text = Inv;
                lblvalue.Text = "";
                Session["Doc"] = "";
                LoadInvoiceDetails();
                if (txterrorno.Value == "1")
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid invoice.');", true);
                    return;
                }
                else if (txterrorno.Value == "2")
                {
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invoice is already reversed.');", true);
                    return;
                }
                GetTempDocData(Name);
                UserDPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                txtInvoiceNo.Text = grdResultD.SelectedRow.Cells[1].Text;
                txtInvoiceNo_TextChanged(null, null);
                lblvalue.Text = "";
            }
            if (lblvalue.Text == "srn")
            {
                txtInvoiceNo.Text = grdResultD.SelectedRow.Cells[1].Text;

                txtInvoiceNo_TextChanged(null, null);
                lblvalue.Text = "";
                UserDPopoup.Hide();
            }
            if (lblvalue.Text == "srncancel")
            {
                string status = grdResultD.SelectedRow.Cells[6].Text;
                if (status == "CANCELED")
                {
                    DisplayMessage("Already canceled", 1);
                    userCancelPopoup.Show();
                    UserDPopoup.Hide();
                    return;
                }
                txtCancelInvoice.Text = grdResultD.SelectedRow.Cells[1].Text;
                lblvalue.Text = "";
                userCancelPopoup.Show();
                UserDPopoup.Hide();
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResultD.PageIndex = e.NewPageIndex;
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            //if (lblvalue.Text == "TempDoc")
            //{
            //    txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).Date.AddMonths(-1).ToShortDateString();
            //    txtTDate.Text = Convert.ToDateTime(System.DateTime.Now).Date.ToShortDateString();
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            //    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text));
            //    grdResultD.DataSource = _result;
            //    grdResultD.DataBind();
            //    grdResultD.PageIndex = 0;
            //    UserDPopoup.Show();
            //}
            //if (lblvalue.Text == "InvoiceWithDate")
            //{

            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchReversalInvoice(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
            //    grdResultD.DataSource = _result;
            //    grdResultD.DataBind();
            //    grdResultD.PageIndex = 0;
            //    UserDPopoup.Show();
            //}
            //if (lblvalue.Text == "srn")
            //{

            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
            //    DataTable result = CHNLSVC.CommonSearch.SearchReversalInvoice(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
            //    grdResultD.DataSource = _result;
            //    grdResultD.DataBind();
            //    grdResultD.PageIndex = 0;
            //    UserDPopoup.Show();
            //}

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "srn")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);//SearchReversalInvoice
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "srncancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srncancel";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                // Session["Doc"] = "true";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "srn")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text.ToString()), Convert.ToDateTime(txtTDate.Text.ToString()));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                // Session["Doc"] = "true";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "srncancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srncancel";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            else if (lblvalue.Text == "srn")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text.ToString()), Convert.ToDateTime(txtTDate.Text.ToString()));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                // Session["Doc"] = "true";
                txtSearchbywordD.Focus();
            }
            else if (lblvalue.Text == "srncancel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srncancel";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
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
        #endregion
        #region  Rooting for Method Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "SRN" + seperator + "1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator + Session["UserCompanyCode"].ToString() + seperator + lblSalePc.Text.Trim() + seperator + txtRevRegItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuaranceDet:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator + Session["UserCompanyCode"].ToString() + seperator + lblSalePc.Text.Trim() + seperator + txtRevInsItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "INV" + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchReversal:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
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

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        protected void lbtncategory_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
            DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Category";

            UserPopoup.Show();
        }
        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "Customer";
            UserPopoup.Show();
        }
        protected void lbtnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkOthSales.Checked == false)
                {

                    if (!string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus);
                        DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(SearchParams, null, null);
                        grdResult.DataSource = _result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(_result);
                        lblvalue.Text = "InvoicebyCustomer";
                        UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal);
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(SearchParams, null, null);
                        //DataView dv = _result.DefaultView;
                        //dv.Sort = "Invoice No DESC";
                        //DataTable sortedDT = dv.ToTable();
                        grdResult.DataSource = _result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(_result);
                        lblvalue.Text = "InvoiceForReversal";
                        UserPopoup.Show();

                    }
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "InvoiceForReversalOth";
                    UserPopoup.Show();

                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        protected void lbtnproftcenter_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Proftcenter";
                UserPopoup.Show();
            }

            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        protected void lbtCancelCir_Click(object sender, EventArgs e)
        {
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchReversal);
            //DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(SearchParams, null, null);
            //grdResult.DataSource = _result;
            //grdResult.DataBind();
            //BindUCtrlDDLData(_result);
            //lblvalue.Text = "Cancel";
            //Session["popup"] = "Cancel";
            //UserPopoup.Show();
            if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

            }
            else
            {
                txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");

            }
            if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

            }
            else
            {
                txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            }
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
            DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "srncancel";
            BindUCtrlDDLData2(result);
            ViewState["SEARCH"] = result;
            UserDPopoup.Show();
            Session["DPopup"] = "DPopup";
            txtSearchbywordD.Text = "";
            txtSearchbywordD.Focus();
        }
        #endregion
        #region  Rooting for TextChange
        protected void txtSubType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblSubDesc.Text = string.Empty;
                if (string.IsNullOrEmpty(txtSubType.Text)) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid return sub type.');", true);
                    lblSubDesc.Text = string.Empty;
                    txtSubType.Text = "";
                    txtSubType.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        protected void txtCusCode_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "CODE", txtCusCode.Text);
            if (_result != null)
            {
                if (_result.Rows.Count == 0)
                {
                    txtCusCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid customer');", true);
                }
            }
            else
            {
                txtCusCode.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid customer');", true);
            }

        }
        protected void txtInvoice_TextChanged(object sender, EventArgs e)
        {
            LoadInvoiceDetails();
        }
        protected void txtReqLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReqLoc.Text))
                {
                    Int32 _isValidPC = 0;

                    _isValidPC = CHNLSVC.Security.Check_User_PC(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), txtReqLoc.Text.Trim());
                    if (_isValidPC == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid or accsess denied location.');", true);

                        txtReqLoc.Text = "";
                        txtReqLoc.Focus();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        private void BackDatePermission()
        {
            try
            {
                _isBackDate = false;
                bool _allowCurrentTrans = false;
                LinkButton btntest = new LinkButton();
                if (IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", this.Page, "", btntest, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans))
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

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
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null)
                {
                    _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                    txtSRNDate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
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
                    lbtnSRNDate.Visible = true;
                }
                else
                {
                    lbtnSRNDate.Visible = false;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                chkFin.Checked = false;
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkApp.Checked == false)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", null, txtReqLoc.Text);
                }
                else
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", Session["UserID"].ToString(), txtReqLoc.Text);
                }
                grdPendings.Columns[1].HeaderText = "Req. No";
                grdPendings.AutoGenerateColumns = false;
                grdPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No any request / approval found.');", true);
                    // MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                grdPendings.DataSource = _TempReqAppHdr;
                grdPendings.DataBind();
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }

        protected void lbtnItemsselect_Click(object sender, EventArgs e)
        {
            if (grdInvItem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                if (_isFromReq == true)
                {
                    string _Msg = "Cannot edit requested details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                Int32 _line = Convert.ToInt32((row.FindControl("col_invLine") as Label).Text);
                string _Item = (row.FindControl("col_invItem") as Label).Text;
                decimal Rtotal = Convert.ToDecimal((row.FindControl("col_invRevQty") as TextBox).Text);
                if (Rtotal == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Reverse quantity is zero');", true);
                    return;
                }
                decimal _repPrice = Convert.ToDecimal((row.FindControl("col_invTot") as Label).Text) / Rtotal;
                lblInvLine.Text = _line.ToString();
                lblRevItem.Text = _Item;
                lblRepPrice.Text = _repPrice.ToString();
                txtSItem.Text = _Item;
                txtSSerial.Text = "";
                LoadAppLevelStatus();
            }


        }


        protected void lbtnItemsfilter_Click(object sender, EventArgs e)
        {
            if (grdInvItem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());

                if (_isFromReq == true)
                {
                    string _Msg = "Cannot edit requested details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                string _Item = (row.FindControl("col_invItem") as Label).Text;
                Session["_invoiceline"] = (row.FindControl("col_invLine") as Label).Text;
                txtSItem.Text = _Item;
                txtSSerial.Text = "";
                LoadAppLevelStatus();
            }


        }
        protected void lbtnNewItem_Click(object sender, EventArgs e)
        {

            txtItem.Text = "";
            txtNewSerial.Text = "";
            txtNewPrice.Text = "";
            txtRQty.Text = "";
            Session["popup"] = "Rereport";
            UserServiceApprovalPopoup.Hide();
            UserCashRefundRequestsPopup.Hide();
            userCancelPopoup.Hide();
            UserRereportPopoup.Show();



        }

        protected void lbtnAttSerApp_Click(object sender, EventArgs e)
        {
            Session["popup"] = "ServiceApproval";
            UserCashRefundRequestsPopup.Hide();
            userCancelPopoup.Hide();
            UserRereportPopoup.Hide();
            UserServiceApprovalPopoup.Show();
        }

        protected void lbtnRegDetails_Click(object sender, EventArgs e)
        {
            Session["popup"] = "CashRefundRequest";

            userCancelPopoup.Hide();
            UserRereportPopoup.Hide();
            UserServiceApprovalPopoup.Hide();
            UserCashRefundRequestsPopup.Show();
        }

        protected void lbtnPendings_Click(object sender, EventArgs e)
        {
            try
            {
                string _reqNo = "";
                string _stus = "";
                string _invNo = "";
                string _remarks = "";
                string _type = "";
                string _pc = "";
                string _retLoc = "";
                string _retSubType = "";
                string _salesPC = "";
                string _reqDate = "";

                txtInvoice.Text = "";
                txtCusCode.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblInvDate.Text = "";
                lblSalePc.Text = "";
                txtInvoice.Enabled = true;
                txtCusCode.Enabled = true;

                lbtnRequest.Enabled = false;
                lbtnRequest.OnClientClick = "return Enable();";
                lbtnRequest.CssClass = "buttoncolor";

                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();


                grdInvItem.AutoGenerateColumns = false;
                grdInvItem.DataSource = new List<InvoiceItem>();
                grdInvItem.DataSource = _InvDetailList;
                grdInvItem.DataBind();

                grdDelDetails.AutoGenerateColumns = false;
                grdDelDetails.DataSource = new List<ReptPickSerials>();
                grdDelDetails.DataSource = _doitemserials;
                Session["gvSerData"] = _doitemserials;
                grdDelDetails.DataBind();

                grdSelectReversal.AutoGenerateColumns = false;
                grdSelectReversal.DataSource = new List<ReptPickSerials>();
                grdSelectReversal.DataSource = _doitemserials;
                grdSelectReversal.DataBind();


                grdPaymentDetails.DataSource = new int[] { };
                grdPaymentDetails.DataBind();

                if (grdPendings.Rows.Count == 0) return;
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    _reqNo = (row.FindControl("col_reqNo") as Label).Text;
                    _stus = (row.FindControl("col_Status") as Label).Text;
                    Session["_stus"] = _stus;
                    _invNo = (row.FindControl("col_Inv") as Label).Text;
                    _remarks = (row.FindControl("col_remarks") as Label).Text;
                    _type = (row.FindControl("col_Type") as Label).Text;
                    _pc = (row.FindControl("col_pc") as Label).Text;
                    _retLoc = (row.FindControl("col_Type") as Label).Text;
                    _retSubType = (row.FindControl("col_SubType") as Label).Text;
                    _salesPC = (row.FindControl("col_OthPC") as Label).Text;
                    _reqDate = (row.FindControl("col_reqDate") as Label).Text;
                }


                if (_salesPC != _pc)
                {
                    chkOthSales.Checked = true;
                    chkOthSales.Enabled = false;
                }
                else
                {
                    chkOthSales.Checked = false;
                    chkOthSales.Enabled = false;
                }

                txtInvoice.Text = _invNo;
                lblReq.Text = _reqNo;
                txtRemarks.Text = _remarks;
                Session["_remark"] = _remarks;
                txtSRNremarks.Text = _remarks;
                lblReturnLoc.Text = _type;
                lblReqPC.Text = _pc;
                txtSubType.Text = _retSubType;
                lblSalePc.Text = _salesPC;
                Session["documntNo"] = _reqNo;
                lblSubDesc.Text = string.Empty;
                Session["_reqDate"] = _reqDate;
                if (!string.IsNullOrEmpty(txtSubType.Text))
                {
                    if (IsValidAdjustmentSubType() == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid return sub type.');", true);

                        lblSubDesc.Text = string.Empty;
                        txtSubType.Text = "";
                        txtSubType.Focus();
                        return;
                    }
                }


                lbtnRegDetails.Enabled = false;
                lbtnRegDetails.OnClientClick = "return Enable();";
                lbtnRegDetails.CssClass = "buttoncolor";
                if (_stus == "A")
                {
                    lblStatus.Text = "APPROVED";
                    lbtnRegDetails.Enabled = true;
                    lbtnRegDetails.CssClass = "buttonUndocolor";
                }
                else if (_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                }
                else if (_stus == "R")
                {
                    lblStatus.Text = "REJECT";
                }
                else if (_stus == "F")
                {
                    lblStatus.Text = "FINISHED";
                }

                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), _salesPC, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);

                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error loading request.');", true);


                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblSalePc.Text = "";
                        lblTotInvAmt.Text = "";
                        lblTotPayAmt.Text = "";
                        lblOutAmt.Text = "";
                        lblTotRetAmt.Text = "";
                        lblTotalRevAmt.Text = "";

                        _dCusCode = "";
                        Session["_dCusCode"] = "";
                        _dCusAdd1 = "";
                        Session["_dCusAdd1"] = "";
                        _dCusAdd2 = "";
                        Session["_dCusAdd2"] = "";
                        _currency = "";
                        Session["_currency"] = "";
                        _exRate = 0;
                        Session["_exRate"] = 0;
                        _invTP = "";
                        Session["_invTP"] = "";
                        _executiveCD = "";
                        Session["_executiveCD"] = "";
                        _manCode = "";
                        Session["_manCode"] = "";
                        _isTax = false;
                        Session["_isTax"] = false;
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        txtCusCode.Text = _tempInv.Sah_cus_cd;
                        lblInvCusName.Text = _tempInv.Sah_cus_name;
                        Session["_dCusName"] = _tempInv.Sah_cus_name;
                        lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                        lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                        lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                        Session["_dInvDate"] = _tempInv.Sah_dt.ToShortDateString();
                        lblSalePc.Text = _tempInv.Sah_pc;
                        lblTotInvAmt.Text = _tempInv.Sah_anal_7.ToString("n");
                        lblTotPayAmt.Text = _tempInv.Sah_anal_8.ToString("n");
                        lblOutAmt.Text = (_tempInv.Sah_anal_7 - _tempInv.Sah_anal_8).ToString("n");
                        Session["AppUsr"] = _tempInv.Sah_mod_by;
                        Session["AppDate"] = _tempInv.Sah_mod_when;

                        _dCusCode = _tempInv.Sah_d_cust_cd;
                        Session["_dCusCode"] = _tempInv.Sah_d_cust_cd;
                        _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                        Session["_dCusAdd1"] = _tempInv.Sah_d_cust_add1;
                        _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                        Session["_dCusAdd2"] = _tempInv.Sah_d_cust_add2;
                        _currency = _tempInv.Sah_currency;
                        Session["_currency"] = _tempInv.Sah_currency;
                        _exRate = _tempInv.Sah_ex_rt;
                        Session["_exRate"] = _tempInv.Sah_ex_rt;
                        _invTP = _tempInv.Sah_inv_tp;
                        Session["_invTP"] = _tempInv.Sah_inv_tp;
                        _executiveCD = _tempInv.Sah_sales_ex_cd;
                        Session["_executiveCD"] = _tempInv.Sah_sales_ex_cd;
                        _manCode = _tempInv.Sah_man_cd;
                        Session["_manCode"] = _tempInv.Sah_man_cd;
                        _isTax = _tempInv.Sah_tax_inv;
                        Session["_isTax"] = _tempInv.Sah_tax_inv;
                        if (lblStatus.Text == "FINISHED")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected request is in FINISHED status.');", true);

                            //lbtnApprovalSave.Enabled = false;
                            //lbtnApprovalSave.OnClientClick = "return Enable();";
                            //lbtnApprovalSave.CssClass = "buttoncolor";
                            //lbtnCancel.Enabled = false;
                            //lbtnCancel.OnClientClick = "return Enable();";
                            //lbtnCancel.CssClass = "buttoncolor";

                            return;
                        }
                        else if (_tempInv.Sah_stus == "C")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected invoice is cancelled.');", true);
                            //lbtnApprovalSave.Enabled = false;
                            //lbtnApprovalSave.OnClientClick = "return Enable();";
                            //lbtnApprovalSave.CssClass = "buttoncolor";
                            //lbtnCancel.Enabled = false;
                            //lbtnCancel.OnClientClick = "return Enable();";
                            //lbtnCancel.CssClass = "buttoncolor";

                            return;
                        }
                        else if (_tempInv.Sah_stus == "R")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This invoice is already reversed.');", true);
                            //lbtnApprovalSave.Enabled = false;
                            //lbtnApprovalSave.OnClientClick = "return Enable();";
                            //lbtnApprovalSave.CssClass = "buttoncolor";
                            //lbtnCancel.Enabled = false;
                            //lbtnCancel.OnClientClick = "return Enable();";
                            //lbtnCancel.CssClass = "buttoncolor";

                            return;
                        }
                        else if (lblStatus.Text == "APPROVED")
                        {
                            //lbtnCancel.Enabled = true;
                            //lbtnCancel.OnClientClick = "CancelConfirm();";
                            //lbtnCancel.CssClass = "buttonUndocolor";

                        }
                        _isFromReq = true;
                        Session["_isFromReq"] = "true";
                        //if (lblStatus.Text == "APPROVED")
                        //{
                        //   Session["_isFromReq"] = "false";
                        //lbtnCancel.Enabled = true;
                        //lbtnCancel.OnClientClick = "CancelConfirm();";
                        //lbtnCancel.CssClass = "buttonUndocolor";

                        //}

                        chkRevReg.Checked = false;
                        chkRevReg.Enabled = false;
                        chkRevIns.Checked = false;
                        chkRevIns.Enabled = false;
                        Load_InvoiceDetails(_pc);
                        Load_Registration_Details(Session["UserCompanyCode"].ToString(), _pc, "ARQT016", _reqNo);
                        Load_Insuarance_Details(Session["UserCompanyCode"].ToString(), _pc, "ARQT017", _reqNo);

                        txtCusCode.Enabled = false;
                        txtInvoice.Enabled = false;

                        lbtnRequest.Enabled = false;
                        lbtnRequest.OnClientClick = "return Enable();";
                        lbtnRequest.CssClass = "buttoncolor";
                        // dgvInvItem.Columns["col_invRevQty"].ReadOnly = true;


                        List<InvoiceItem> _tmpInv = new List<InvoiceItem>();
                        List<InvoiceItem> _newList = new List<InvoiceItem>();
                        List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

                        decimal _rtnSerQty = 0;
                        decimal _fwsQty = 0;

                        _tmpInv = _InvDetailList;
                        _InvDetailList = null;


                        foreach (InvoiceItem itm in _tmpInv)
                        {
                            _rtnSerQty = 0;
                            _fwsQty = 0;
                            foreach (ReptPickSerials _tmpser in _doitemserials)
                            {

                                string _item = _tmpser.Tus_itm_cd;
                                Int32 _line = _tmpser.Tus_base_itm_line;

                                if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                                {
                                    _rtnSerQty = _rtnSerQty + 1;
                                }

                            }
                            #region amount calculation  Dulaj 2018/Nov/13
                            //temp commented by wimal @ 24/07/2018 to reaised reversal for forward sales
                            //_fwsQty = itm.Sad_srn_qty - _rtnSerQty;
                            itm.Sad_fws_ignore_qty = _fwsQty;
                            if (_rtnSerQty > 0)
                            {
                                itm.Sad_disc_amt = itm.Sad_disc_amt / itm.Sad_srn_qty;
                                itm.Sad_itm_tax_amt = itm.Sad_itm_tax_amt / itm.Sad_srn_qty;
                                itm.Sad_unit_amt = itm.Sad_unit_amt / itm.Sad_srn_qty;
                                itm.Sad_srn_qty = _rtnSerQty;
                            }
                            if (_rtnSerQty > 0)
                            {
                                itm.Sad_disc_amt = itm.Sad_disc_amt * itm.Sad_srn_qty;
                                itm.Sad_itm_tax_amt = itm.Sad_itm_tax_amt * itm.Sad_srn_qty;
                                itm.Sad_unit_amt = itm.Sad_unit_amt * itm.Sad_srn_qty;
                                itm.Sad_tot_amt = (itm.Sad_disc_amt*-1) + itm.Sad_itm_tax_amt + itm.Sad_unit_amt;
                            }
                            #endregion
                            _newList.Add(itm);

                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                itm.Sad_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == itm.Sad_itm_stus).Mis_desc;
                            }

                        }

                        _InvDetailList = _newList;

                        grdInvItem.AutoGenerateColumns = false;
                        grdInvItem.DataSource = new List<InvoiceItem>();
                        grdInvItem.DataSource = _InvDetailList;
                        grdInvItem.DataBind();
                        for (int i = 0; i < grdInvItem.Rows.Count; i++)
                        {
                            TextBox tb = ((TextBox)grdInvItem.Rows[i].FindControl("col_invRevQty"));
                            tb.Enabled = false;
                        }


                        decimal _totRetAmt = 0;
                        decimal _crAmt = 0;
                        decimal _outAmt = 0;
                        decimal _preRevAmt = 0;
                        decimal _preCrAmt = 0;
                        decimal _balCrAmt = 0;
                        decimal _newOut = 0;


                        DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(txtInvoice.Text.Trim());
                        if (_revAmt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                            {
                                _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                            }
                        }

                        DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(txtInvoice.Text.Trim());
                        if (_preCr.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                            {
                                _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                            }
                        }


                        foreach (InvoiceItem _temp in _InvDetailList)
                        {
                            _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
                        }

                        lblTotalRevAmt.Text = _preRevAmt.ToString("n");
                        lblTotRetAmt.Text = _totRetAmt.ToString("n");

                        _outAmt = Convert.ToDecimal(lblOutAmt.Text);
                        _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

                        _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

                        if (_balCrAmt < 0)
                        {
                            _balCrAmt = 0;
                        }

                        if (_outAmt > 0)
                        {
                            _crAmt = _balCrAmt - _outAmt;
                        }
                        else
                        {
                            if (_totRetAmt <= _balCrAmt)
                            {
                                _crAmt = _totRetAmt;
                            }
                            else
                            {
                                _crAmt = _balCrAmt;
                            }
                        }

                        if (_crAmt > 0)
                        {
                            lblCrAmt.Text = _crAmt.ToString("n");
                        }
                        else
                        {
                            lblCrAmt.Text = "0";
                        }

                        DataTable _newRep = new DataTable();
                        _newRep = CHNLSVC.General.SearchrequestAppAddDetByRef(_reqNo);

                        grdRereportItems.DataSource = _newRep;
                        grdRereportItems.DataBind();
                        //if (_newRep.Rows.Count > 0)
                        // {
                        for (int i = 0; i < grdInvItem.Rows.Count; i++)
                        {
                            LinkButton tb = ((LinkButton)grdRereportItems.Rows[i].FindControl("lbtnRereportItemsDel"));
                            tb.Visible = false;
                        }
                        // }
                        //Load collection deatails
                        DataTable _collDet = CHNLSVC.Sales.GetInvoiceReceiptDet(txtInvoice.Text.Trim());
                        if (_collDet.Rows.Count > 0)
                        {
                            DataRow[] result = _collDet.Select("SAR_RECEIPT_TYPE = 'DEBT' AND SAR_RECEIPT_TYPE = 'DIR'");
                            grdPaymentDetails.DataSource = result;
                            grdPaymentDetails.DataBind();
                            DataRow dr = null;
                            DataTable _Coll = new DataTable();
                            _Coll.Columns.Add(new DataColumn("col_recSeq", typeof(string)));
                            _Coll.Columns.Add(new DataColumn("col_recNo", typeof(string)));
                            _Coll.Columns.Add(new DataColumn("col_recDT", typeof(string)));
                            _Coll.Columns.Add(new DataColumn("col_recPayTp", typeof(string)));
                            _Coll.Columns.Add(new DataColumn("col_recPayRef", typeof(string)));
                            _Coll.Columns.Add(new DataColumn("col_recAmt", typeof(string)));
                            foreach (DataRow drow in _collDet.Rows)
                            {

                                if (drow["SAR_RECEIPT_TYPE"].ToString() == "DEBT" || drow["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                                {
                                    dr = _Coll.NewRow();



                                    //grdPaymentDetails.Rows.Add();
                                    dr["col_recSeq"] = drow["sar_receipt_no"].ToString();
                                    dr["col_recNo"] = drow["sar_anal_3"].ToString();
                                    dr["col_recDT"] = drow["sar_receipt_date"].ToString();
                                    dr["col_recPayTp"] = drow["sard_pay_tp"].ToString();
                                    dr["col_recPayRef"] = drow["sard_ref_no"].ToString();
                                    dr["col_recAmt"] = drow["sard_settle_amt"].ToString();
                                    _Coll.Rows.Add(dr);
                                }

                            }
                            grdPaymentDetails.DataSource = _Coll;
                            grdPaymentDetails.DataBind();
                        }
                        List<RequestApprovalSerials> _tmpRepSer = new List<RequestApprovalSerials>();
                        DataTable _tbl = CHNLSVC.General.Get_gen_reqapp_ser(Session["UserCompanyCode"].ToString(), _reqNo, out _tmpRepSer);
                        var _filter = _tmpRepSer.Where(x => x.Gras_anal5 != "").ToList();
                        if (_filter.Count > 0)
                        {
                            grdSerApp.AutoGenerateColumns = false;
                            grdSerApp.DataSource = new List<RequestApprovalSerials>();
                            grdSerApp.DataSource = _filter;
                            grdSerApp.DataBind();

                            ViewState["_repSer"] = _filter;
                        }
                        for (int i = 0; i < grdSerApp.Rows.Count; i++)
                        {
                            LinkButton tb = ((LinkButton)grdRereportItems.Rows[i].FindControl("lbtnSerAppDel"));
                            tb.Visible = false;
                        }



                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);


            }
        }

        protected void lbtnFilterSerial_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtSSerial.Text))
            //{
            //    string _Msg = "please type the serial number";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtSItem.Text))
            //{
            //    string _Msg = "Please select an item";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}
            _doitemserials = ViewState["_olddoitemserials"] as List<ReptPickSerials>;
            if ((_doitemserials == null) || (_doitemserials.Count == 0))
            {
                _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;

                if (!chkserial.Checked)
                {
                    // if ((_doitemserials == null) || (_doitemserials.Count == 0))
                    // {
                    _doitemserials = CHNLSVC.Inventory.GetInvoiceSerialForReversalBYSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), Session["_defBin"].ToString(), txtInvoice.Text, txtSSerial.Text);
                    List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    foreach (ReptPickSerials serial in _doitemserials)
                    {
                        MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == serial.Tus_itm_stus);
                        if (oStatus != null)
                        {
                            serial.Tus_itm_stus_Desc = oStatus.Mis_desc;

                        }
                        else
                        {
                            serial.Tus_itm_stus_Desc = serial.Tus_itm_stus;
                        }
                    }
                    ViewState["_doitemserials_backup"] = _doitemserials;
                    // }
                }
            }
            List<ReptPickSerials> lbtnFilterSerial = new List<ReptPickSerials>();
            lbtnFilterSerial = _doitemserials;
            if (txtSSerial.Text == "")
            {
                if (string.IsNullOrEmpty(txtSItem.Text))
                {
                    string _Msg = "Please select an item";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                lbtnFilterSerial = lbtnFilterSerial.Where(x => x.Tus_itm_cd == txtSItem.Text).ToList();
                grdDelDetails.DataSource = lbtnFilterSerial;
                Session["gvSerData"] = lbtnFilterSerial;
                grdDelDetails.DataBind();
                LoadAppLevelStatus();
                return;
            }

            lbtnFilterSerial = lbtnFilterSerial.Where(x => x.Tus_ser_1 == txtSSerial.Text).ToList();
            grdDelDetails.DataSource = lbtnFilterSerial;
            Session["gvSerData"] = lbtnFilterSerial;
            grdDelDetails.DataBind();
            LoadAppLevelStatus();
        }

        protected void btnAllSerial_Click(object sender, EventArgs e)
        {
            _doitemserials = ViewState["_olddoitemserials"] as List<ReptPickSerials>;
            grdDelDetails.DataSource = _doitemserials;
            Session["gvSerData"] = lbtnFilterSerial;
            grdDelDetails.DataBind();
            LoadAppLevelStatus();
        }


        protected void lbtnAddReversalSerial_Click(object sender, EventArgs e)
        {
            _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
            List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            if (_isFromReq == true)
            {
                //added by Wimal temp @ -6/Aug/2018 - chk reversal              
                _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(txtInvoice.Text.Trim(), null, Session["UserCompanyCode"].ToString(), lblReqPC.Text.Trim(), lblReq.Text.Trim());
                //removed by Wimal temp @ -6/Aug/2018 
                //string _Msg = "Cannot edit requested details.";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //return;
            }

            List<ReptPickSerials> OldRevsFilterSerial = new List<ReptPickSerials>();
            List<ReptPickSerials> ChrckFilterSerial = new List<ReptPickSerials>();
            List<ReptPickSerials> RevsFilterSerial = new List<ReptPickSerials>();
            RevsFilterSerial = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            bool _ischeck = false;
            _doitemserials = ViewState["_doitemserials_backup"] as List<ReptPickSerials>;
            txtSItem.Text = txtSItem.Text.Trim();
            if (!string.IsNullOrEmpty(txtqty.Text))
            {
                if (string.IsNullOrEmpty(txtSItem.Text))
                {
                    string _Msg = "Please select item";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                //added by Wimal @ 08/Aug/2018 to restrict add items not in SRN req - start
                if (_isFromReq == true)
                {
                    var _filter = _paramInvoiceItems.Where(x => x.Sad_itm_cd == txtSItem.Text).ToList();
                    if (_filter.Count <= 0)
                    {
                        string _Msg = "Not a requested Item";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }
                //added by Wimal @ 08/Aug/2018 to restrict add items not in SRN req - end

                // RevsFilterSerial = _doitemserials;
                if (RevsFilterSerial == null)
                {
                    RevsFilterSerial = new List<ReptPickSerials>();
                }
                int val;
                if (!int.TryParse(txtqty.Text, out val))
                {
                    string _Msg = "please enter valid qty";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                int qty = Convert.ToInt32(txtqty.Text);
                MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtSItem.Text);
                if (_itemdetail != null)
                {
                    if (_itemdetail.Mi_is_ser1 == 0)
                    {
                        var _filter = RevsFilterSerial.Where(x => x.Tus_itm_cd == txtSItem.Text).ToList();
                        if (_filter.Count > 0)
                        {
                            string _Msg = "Already add item";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                        else
                        {
                            List<ReptPickSerials> RevsFilterSerial2 = new List<ReptPickSerials>();
                            RevsFilterSerial2 = _doitemserials.Where(x => x.Tus_itm_cd == txtSItem.Text).Take(qty).ToList();

                            if (RevsFilterSerial2.Count > 0)
                            {
                                var _scanItems = RevsFilterSerial2.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key });
                                foreach (var _lineserial in _scanItems)
                                {
                                    removeITem(txtSItem.Text, _lineserial.Peo.Tus_base_itm_line, qty);
                                }


                                RevsFilterSerial = RevsFilterSerial.Concat(RevsFilterSerial2).ToList();
                                if ((ddlChangestatus.SelectedIndex == 0))
                                {
                                    string _Msg = "Please Select status to be change -" + txtSItem.Text;
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                    return;
                                }
                                if ((ddlChangestatus.SelectedIndex > 0))
                                {

                                    foreach (ReptPickSerials serial in RevsFilterSerial)
                                    {
                                        serial.Tus_new_status_Desc = ddlChangestatus.SelectedItem.Text;
                                        serial.Tus_appstatus = ddlChangestatus.SelectedValue;

                                    }
                                }

                                grdSelectReversal.DataSource = RevsFilterSerial;
                                grdSelectReversal.DataBind();
                                ViewState["RevsFilterSerial"] = RevsFilterSerial;
                                btnAllSerial_Click(null, null);
                                ddlChangestatus.SelectedIndex = -1;
                                txtSSerial.Text = string.Empty;
                                txtSItem.Text = string.Empty;
                                txtqty.Text = string.Empty;
                            }
                            else
                            {
                                int line = Convert.ToInt32(Session["_invoiceline"]);
                                List<ReptPickSerials> _SERIID = new List<ReptPickSerials>();
                                _SERIID = CHNLSVC.Inventory.GetInvoiceSerialForReversalBYITM(Session["UserCompanyCode"].ToString(), txtInvoice.Text, line, txtSItem.Text, qty);
                                //_SERIID = _SERIID.Take(qty).ToList();
                                foreach (ReptPickSerials serial in _SERIID)
                                {
                                    serial.Tus_new_status_Desc = ddlChangestatus.SelectedItem.Text;
                                    serial.Tus_appstatus = ddlChangestatus.SelectedValue;
                                    serial.Tus_loc = Session["UserDefLoca"].ToString();
                                    serial.Tus_base_itm_line = line;
                                    removeITem(serial.Tus_itm_cd, line, serial.Tus_qty);
                                    serial.Tus_bin = Session["_defBin"].ToString();
                                }

                                grdSelectReversal.DataSource = _SERIID;
                                grdSelectReversal.DataBind();
                                ViewState["RevsFilterSerial"] = _SERIID;
                                btnAllSerial_Click(null, null);
                                ddlChangestatus.SelectedIndex = -1;
                                txtSSerial.Text = string.Empty;
                                txtSItem.Text = string.Empty;
                                txtqty.Text = string.Empty;
                            }
                        }
                    }
                }
                return;
            }



            foreach (GridViewRow row in grdDelDetails.Rows)
            {

                RevsFilterSerial = _doitemserials;
                if (RevsFilterSerial == null)
                {
                    RevsFilterSerial = new List<ReptPickSerials>();
                }
                CheckBox chk = (CheckBox)row.FindControl("chk_DItem");
                if (chk.Checked)
                {
                    //added by Wimal @ 08/Aug/2018 to restrict add items not in SRN req - start
                    if (_isFromReq == true)
                    {
                        var _filter = _paramInvoiceItems.Where(x => x.Sad_itm_cd == (row.FindControl("col_item") as Label).Text).ToList();
                        if (_filter.Count <= 0)
                        {
                            string _Msg = "Not a requested Item";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                    //added by Wimal @ 08/Aug/2018 to restrict add items not in SRN req - end

                    _ischeck = true;
                    string _Item = (row.FindControl("col_item") as Label).Text;
                    Int32 _line = Convert.ToInt32((row.FindControl("col_BaseLine") as Label).Text);
                    string _Serial = (row.FindControl("col_Serial") as Label).Text;
                    string _wara = (row.FindControl("col_Wara") as Label).Text;
                    string _lineno = (row.FindControl("col_Tus_temp_itm_line") as Label).Text;
                    string _serId = (row.FindControl("col_Tus_ser_id") as Label).Text;
                    decimal _qty = Convert.ToDecimal((row.FindControl("col_Qty") as Label).Text);

                    DropDownList _status = (DropDownList)row.FindControl("col_appstatus");
                    int selectedvalue = _status.SelectedIndex;

                    RevsFilterSerial = RevsFilterSerial.Where(x => x.Tus_itm_cd == _Item && (x.Tus_temp_itm_line == Convert.ToInt32(_lineno))).ToList();

                    if ((ddlChangestatus.SelectedIndex == 0) && (selectedvalue == 0))
                    {
                        string _Msg = "Please Select status to be change -" + _Item;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                    if ((ddlChangestatus.SelectedIndex > 0))
                    {

                        foreach (ReptPickSerials serial in RevsFilterSerial)
                        {
                            serial.Tus_new_status_Desc = ddlChangestatus.SelectedItem.Text;
                            serial.Tus_appstatus = ddlChangestatus.SelectedValue;
                            serial.Tus_itm_line = Convert.ToInt32(_lineno);
                            serial.Tus_itm_stus = ddlChangestatus.SelectedValue;
                            serial.Tus_new_status = ddlChangestatus.SelectedValue;
                            serial.Tus_ser_remarks = "SRN";

                        }
                    }
                    if ((selectedvalue > 0))
                    {

                        foreach (ReptPickSerials serial in RevsFilterSerial)
                        {
                            serial.Tus_new_status_Desc = _status.SelectedItem.Text;
                            serial.Tus_appstatus = _status.SelectedValue;
                            serial.Tus_itm_line = Convert.ToInt32(_lineno);
                            // serial.Tus_appstatus = _status.SelectedItem.Text;

                        }
                    }
                    //if (RevsFilterSerial.Count > 0)
                    //{
                    //    RevsFilterSerial = RevsFilterSerial.GroupBy(x => x.Tus_itm_cd).Select(y => y.First()).ToList();
                    //}
                    if (RevsFilterSerial.Count > 0)
                    {
                        if (OldRevsFilterSerial == null)
                        {
                            ViewState["RevsFilterSerial"] = RevsFilterSerial;
                            OldRevsFilterSerial = ViewState["_doitemserials_backup"] as List<ReptPickSerials>;

                        }
                        else
                        {
                            OldRevsFilterSerial = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                            if (OldRevsFilterSerial == null)
                            {
                                OldRevsFilterSerial = new List<ReptPickSerials>();
                            }
                            // ChrckFilterSerial = OldRevsFilterSerial.Where(x => x.Tus_itm_cd == _Item && (x.Tus_temp_itm_line == Convert.ToInt32(_lineno))).ToList();
                            ChrckFilterSerial = OldRevsFilterSerial.Where(x => x.Tus_itm_cd == _Item && (x.Tus_ser_id == Convert.ToInt32(_serId))).ToList();
                            if (ChrckFilterSerial.Count == 1)
                            {
                                string _Msg = "Item " + _Item + " is already added";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                return;
                            }
                            OldRevsFilterSerial = OldRevsFilterSerial.Concat(RevsFilterSerial).ToList();
                            ViewState["RevsFilterSerial"] = OldRevsFilterSerial;
                        }
                        _status.SelectedIndex = -1;
                    }
                    else
                    {
                        OldRevsFilterSerial = ViewState["_doitemserials_backup"] as List<ReptPickSerials>;
                        List<ReptPickSerials> _con = new List<ReptPickSerials>();
                        RevsFilterSerial = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                        var _con2 = OldRevsFilterSerial.SingleOrDefault(x => x.Tus_itm_cd == _Item && x.Tus_ser_1 == _Serial && x.Tus_ser_id == Convert.ToInt32(_serId));
                        _con.Add(_con2);
                        if (_con.Count > 1)
                        {

                        }
                        if ((ddlChangestatus.SelectedIndex > 0))
                        {
                            foreach (ReptPickSerials serial in _con)
                            {
                                serial.Tus_new_status_Desc = ddlChangestatus.SelectedItem.Text;
                                serial.Tus_appstatus = ddlChangestatus.SelectedValue;
                                serial.Tus_itm_line = Convert.ToInt32(_lineno);
                            }
                        }
                        if ((selectedvalue > 0))
                        {
                            foreach (ReptPickSerials serial in _con)
                            {
                                serial.Tus_new_status_Desc = _status.SelectedItem.Text;
                                serial.Tus_appstatus = _status.SelectedValue;
                                serial.Tus_itm_line = Convert.ToInt32(_lineno);
                                // serial.Tus_appstatus = _status.SelectedItem.Text;
                            }
                        }
                        RevsFilterSerial = RevsFilterSerial.Concat(_con).ToList();
                        ViewState["RevsFilterSerial"] = RevsFilterSerial;
                    }
                    chk.Checked = false;
                    CheckBox chk2 = (CheckBox)grdDelDetails.HeaderRow.FindControl("allDchk");
                    _status.SelectedIndex = -1;
                    chk2.Checked = false;
                    removeITem(_Item, _line, _qty);
                }
            }
            if (_ischeck == false)
            {
                string _Msg = "please select an item to reverse";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }
            OldRevsFilterSerial = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            //_InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            //if (_InvDetailList.Count > 0 && _InvDetailList != null)//(_InvDetailList.Count > 0)
            //{
            //    var gropitem = _InvDetailList.GroupBy(d => d.Sad_itm_cd).Select(g => new
            //    {
            //        Key = g.Key
            //    }).ToList();
            //    foreach (var _ck in gropitem)
            //    {
            //        var _filter = OldRevsFilterSerial.Where(x => x.Tus_itm_cd == _ck.Key).ToList();
            //        if (_filter.Count == 0)
            //        {
            //            _InvDetailList.RemoveAll(R => R.Sad_itm_cd == _ck.Key);
            //        }
            //    }

            //}

            grdSelectReversal.DataSource = OldRevsFilterSerial;
            grdSelectReversal.DataBind();
            ViewState["RevsFilterSerial"] = OldRevsFilterSerial;
            ddlChangestatus.SelectedIndex = -1;
            ViewState["_doitemserials"] = OldRevsFilterSerial;// _doitemserials

            // grdInvItem.DataSource = _InvDetailList;
            //  grdInvItem.DataBind();
            // ViewState["_InvDetailList"] = _InvDetailList;
            btnAllSerial_Click(null, null);
            ddlChangestatus.SelectedIndex = -1;
            txtSSerial.Text = string.Empty;
            txtSItem.Text = string.Empty;
        }

        private void removeITem(string _item, int _line, decimal _qty)
        {
            List<InvoiceItem> _temp = new List<InvoiceItem>();
            List<InvoiceItem> _newList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempser = new List<ReptPickSerials>();
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _temp = _InvDetailList;
            _InvDetailList = null;

            foreach (InvoiceItem itm in _temp)
            {
                if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                {
                    itm.Sad_srn_qty = itm.Sad_srn_qty + _qty;
                    itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty;//- _qty;
                    //itm.Sad_unit_amt = itm.Sad_unit_amt / itm.Sad_qty * itm.Sad_srn_qty;
                    //itm.Sad_itm_tax_amt = itm.Sad_itm_tax_amt / itm.Sad_qty * itm.Sad_srn_qty;
                    //itm.Sad_disc_amt = itm.Sad_disc_amt / itm.Sad_qty * itm.Sad_srn_qty;
                    //itm.Sad_tot_amt = itm.Sad_tot_amt / itm.Sad_qty * itm.Sad_srn_qty;
                }


                _newList.Add(itm);

            }

            //_newList.RemoveAll(x => x.Sad_srn_qty <= 0);
            _InvDetailList = _newList;





            grdInvItem.AutoGenerateColumns = false;
            grdInvItem.DataSource = new List<InvoiceItem>();
            grdInvItem.DataSource = _InvDetailList;
            grdInvItem.DataBind();
            ViewState["_InvDetailList"] = _InvDetailList;
        }
        protected void lbtnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Re-Item";
                Session["popup"] = "Rereport";
                UserRereportPopoup.Show();
                UserPopoup.Show();
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                if (_isFromReq == true)
                {
                    string _Msg = "Cannot edit requested details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
                if (_repAddDet == null)
                {
                    _repAddDet = new List<RequestAppAddDet>();
                }
                if (string.IsNullOrEmpty(lblRevItem.Text))
                {
                    string _Msg = "Please select reverse item.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    UserRereportPopoup.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtRQty.Text))
                {
                    string _Msg = "Please insert the quantity.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    UserRereportPopoup.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    string _Msg = "Please select re-report item..";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    txtItem.Focus();
                    UserRereportPopoup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(lblInvLine.Text))
                {

                    string _Msg = "Please select reverse item.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    UserRereportPopoup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewPrice.Text))
                {

                    string _Msg = "Please enter re-report price.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    UserRereportPopoup.Show();
                    return;
                }

                var currrange = (from cur in _repItem
                                 where cur.Grad_anal2 == lblRevItem.Text.Trim() && cur.Grad_val2 == Convert.ToInt16(lblInvLine.Text) && cur.Grad_anal4 == txtNewSerial.Text.Trim() && cur.Grad_anal3 == txtItem.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {

                    string _Msg = "Selected item already exsist .";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    txtItem.Focus();
                    UserRereportPopoup.Show();
                    return;
                }

                RequestApprovalDetail _tmpRepItem = new RequestApprovalDetail();
                _tmpRepItem.Grad_anal2 = lblRevItem.Text.Trim();
                _tmpRepItem.Grad_anal3 = txtItem.Text.Trim();
                _tmpRepItem.Grad_val2 = Convert.ToInt16(lblInvLine.Text);
                _tmpRepItem.Grad_anal4 = txtNewSerial.Text.Trim();

                RequestAppAddDet _tmpAddDet = new RequestAppAddDet();
                _tmpAddDet.Grad_anal1 = lblRevItem.Text.Trim();
                _tmpAddDet.Grad_anal2 = txtItem.Text.Trim();
                _tmpAddDet.Grad_anal3 = txtNewSerial.Text.Trim();
                _tmpAddDet.Grad_anal5 = txtNewSch.Text.Trim();
                _tmpAddDet.Grad_anal6 = Convert.ToDecimal(lblRepPrice.Text);
                _tmpAddDet.Grad_anal7 = Convert.ToDecimal(txtNewPrice.Text);
                _tmpAddDet.Grad_anal8 = Convert.ToInt32(lblInvLine.Text);
                _tmpAddDet.Grad_anal9 = Convert.ToDecimal(txtRQty.Text);


                _repItem.Add(_tmpRepItem);
                _repAddDet.Add(_tmpAddDet);

                grdRereportItems.AutoGenerateColumns = false;
                grdRereportItems.DataSource = new List<RequestAppAddDet>();
                grdRereportItems.DataSource = _repAddDet;
                grdRereportItems.DataBind();
                ViewState["_repAddDet"] = _repAddDet;
                ViewState["_repItem"] = _repItem;
                lblRevItem.Text = "";
                txtItem.Text = "";
                lblInvLine.Text = "";
                lblRepPrice.Text = "";
                txtNewSerial.Text = "";
                txtNewPrice.Text = "";
                txtRQty.Text = "";
                txtNewSch.Text = "";
                UserRereportPopoup.Show();

            }
            catch (Exception ex)
            {

                string _Msg = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }

        }

        protected void lbtnSerSelect_Click(object sender, EventArgs e)
        {
            _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
            if (_isFromReq == true)
            {
                string _Msg = "Cannot edit requested details.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }
            if (grdSelectReversal.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _Item = (row.FindControl("colR_item") as Label).Text;
                string _Serial = (row.FindControl("colR_Serial") as Label).Text;
                string _wara = (row.FindControl("colR_Wara") as Label).Text;
                lblSerItem.Text = _Item;
                lblSerial.Text = _Serial;
                lblWarranty.Text = _wara;
            }
        }

        protected void lbtnSerRDel_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            List<ReptPickSerials> SelectedReves = new List<ReptPickSerials>();
            SelectedReves = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            if (grdSelectReversal.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                if (lblStatus.Text == "APPROVED")
                {
                    Session["_isFromReq"] = "false";
                    _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                    //lbtnCancel.Enabled = true;
                    //lbtnCancel.OnClientClick = "CancelConfirm();";
                    //lbtnCancel.CssClass = "buttonUndocolor";

                }

                //added by Wimal temp @ -6/Aug/2018 - chk reversal
                //if (_isFromReq == true)
                //{
                //    string _Msg = "Cannot edit requested details.";
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //    return;
                //}

                string _Item = (row.FindControl("colR_item") as Label).Text;
                string _serial = (row.FindControl("colR_Serial") as Label).Text;
                string _line = (row.FindControl("colTus_itm_line") as Label).Text;
                string _seriId = (row.FindControl("colR_SerID") as Label).Text;
                if (SelectedReves.Count > 0)
                {
                    Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("SRN", Session["UserCompanyCode"].ToString(), txtInvoice.Text, 1);

                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _Item);

                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_seriId), _Item, null);
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _Item, Convert.ToInt32(_seriId), 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _Item, _status);
                    }






                    SelectedReves.RemoveAll(x => x.Tus_itm_cd == _Item && x.Tus_ser_1 == _serial && x.Tus_itm_line == Convert.ToInt32(_line));
                    grdSelectReversal.DataSource = SelectedReves;
                    grdSelectReversal.DataBind();
                    ViewState["_doitemserials"] = SelectedReves;
                    ViewState["RevsFilterSerial"] = SelectedReves;

                    //

                    _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                    if (_InvDetailList != null)
                    {
                        var _filter = _InvDetailList.SingleOrDefault(x => x.Sad_itm_cd == _Item && x.Sad_itm_line == Convert.ToInt32(_line));
                        if (_filter != null)
                        {//Edited By Dulaj 2018/Nov/09 add all calculation except _filter.Sad_srn_qty = _filter.Sad_srn_qty - 1;
                            _filter.Sad_itm_tax_amt = _filter.Sad_itm_tax_amt / _filter.Sad_srn_qty;
                            _filter.Sad_disc_amt = _filter.Sad_disc_amt / _filter.Sad_srn_qty;
                            _filter.Sad_srn_qty = _filter.Sad_srn_qty - 1;
                            _filter.Sad_unit_amt = _filter.Sad_pb_price * _filter.Sad_srn_qty;
                            _filter.Sad_itm_tax_amt = _filter.Sad_itm_tax_amt * _filter.Sad_srn_qty;
                            _filter.Sad_disc_amt = _filter.Sad_disc_amt * _filter.Sad_srn_qty;
                            _filter.Sad_tot_amt = (_filter.Sad_unit_amt + _filter.Sad_itm_tax_amt) - _filter.Sad_disc_amt;
                        }
                    }
                    grdInvItem.DataSource = _InvDetailList;
                    grdInvItem.DataBind();
                    ViewState["_InvDetailList"] = _InvDetailList;

                }

            }

        }
        protected void lbtnDeleteItem_Click(object sender, EventArgs e)
        {
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            List<ReptPickSerials> SelectedReves = new List<ReptPickSerials>();
            SelectedReves = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            if (grdInvItem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {

                _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
                if (_isFromReq == true)
                {
                    string _Msg = "Cannot edit requested details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                if (_InvDetailList == null || _InvDetailList.Count == 0) return;

                string _item = (row.FindControl("col_invItem") as Label).Text;
                Int32 _line = Convert.ToInt32((row.FindControl("col_invLine") as Label).Text);

                List<InvoiceItem> _temp = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                _doitemserials = new List<ReptPickSerials>();

                _temp = _InvDetailList;

                _temp.RemoveAll(x => x.Sad_itm_cd == _item && x.Sad_itm_line == _line);
                _InvDetailList = _temp;

                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem item in _InvDetailList)
                    {
                        _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), _defBin, item.Sad_inv_no, item.Sad_itm_line);
                        _doitemserials.AddRange(_tempDOSerials);
                    }
                }

                grdInvItem.AutoGenerateColumns = false;
                grdInvItem.DataSource = new List<InvoiceItem>();
                grdInvItem.DataSource = _InvDetailList;
                grdInvItem.DataBind();
                ViewState["_InvDetailList"] = _InvDetailList;

                grdDelDetails.AutoGenerateColumns = false;
                grdDelDetails.DataSource = new List<ReptPickSerials>();
                grdDelDetails.DataSource = _doitemserials;
                Session["gvSerData"] = _doitemserials;
                grdDelDetails.DataBind();
                ViewState["_doitemserials"] = _doitemserials;
            }


        }

        #region  Rooting for Clear button
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }
        #endregion
        #region  Rooting for Save /Temp
        protected void lbtnTempSave_Click(object sender, EventArgs e)
        {
            try
            {

                TempProcess(true);

            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        protected void lbtnReversalSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //added by dilshan ***********          
            if (lblReturnLoc.Text != Session["UserDefLoca"].ToString())
            {
                if (chkDiffLoc.Checked == true)
                {
                    Process();
                }
                else
                {
                    string _Msg = "Request location and your profit center is not match.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
            }
            else
            {
                Process();
            }
            //****************************
            //Process();

            //}
            //catch (Exception ex)
            //{

            // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
        }
        private void Process()
        {
            try
            {
                _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                _InvDetailList.Where(x => x.Sad_srn_qty > 0).ToList();//Dulaj 2018/Nov/13
                _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;//ViewState["_doitemserials"] as List<ReptPickSerials>;

                string _msg = "";
                decimal _retVal = 0;
                Boolean _isOthRev = false;
                string _orgPC = "";

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(lblReq.Text))
                {
                    string _Msg = "Please select an approved request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (lblReqPC.Text != Session["UserDefProf"].ToString())
                {
                    string _Msg = "Request profit center and your profit center is not match.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    return;
                }
                //commented by dilshan on 12/04/2018
                //if (lblReturnLoc.Text != Session["UserDefLoca"].ToString())
                //{
                //    string _Msg = "Request location and your profit center is not match.";
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //    return;
                //}
                if (lblStatus.Text == "FINISHED")
                {
                    string _Msg = "Request is already finished.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (lblStatus.Text != "APPROVED")
                {
                    string _Msg = "Request is still not approved.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (grdInvItem.Rows.Count <= 0)
                {
                    string _Msg = "Cannot find reverse details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    return;
                }


                if (chkOthSales.Checked == true)
                {
                    _isOthRev = true;
                }
                else
                {
                    _isOthRev = false;
                }

                _orgPC = lblSalePc.Text.Trim();

                decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtSRNDate.Text).Date, out _wkNo, Session["UserCompanyCode"].ToString());

                if (_weekNo == 0)
                {
                    string _Msg = "Week Definition is still not setup for current date.Please contact retail accounts dept.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtSRNDate.Text).Date != DateTime.Now.Date)
                        {
                            txtSRNDate.Enabled = true;
                            string _Msg = "Back date not allow for selected date!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            txtSRNDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtSRNDate.Enabled = true;
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        txtSRNDate.Focus();
                        return;
                    }
                }


                decimal rtnQty = 0;
                decimal regQty = 0;

                if (chkRevReg.Checked == true)
                {
                    if (_regDetails.Count > 0)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            regQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicalRegistration tempReg in _regDetails)
                            {
                                if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                                {
                                    regQty = regQty + 1;
                                }
                            }

                            if (rtnQty < regQty)
                            {
                                string _Msg = "You are going to reverse registration details more than return qty.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                                return;
                            }
                        }
                    }
                    else
                    {
                        string _Msg = "Cannot find registration details.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                        return;
                    }
                }


                decimal insQty = 0;
                rtnQty = 0;
                if (chkRevIns.Checked == true)
                {
                    if (_insDetails.Count > 0)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            insQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicleInsuarance tempIns in _insDetails)
                            {
                                if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                                {
                                    insQty = insQty + 1;
                                }
                            }

                            if (rtnQty < insQty)
                            {
                                string _Msg = "You are going to reverse insuarance details more than return qty.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                                return;
                            }
                        }
                    }
                    else
                    {
                        string _Msg = "Cannot find insuarance details.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }


                if (chkCashRefund.Checked == true)
                {
                    CollectRefApp();
                    CollectRefAppLog();
                }

                _retVal = 0;

                foreach (InvoiceItem tmpItem in _InvDetailList)
                {
                    _retVal = _retVal + tmpItem.Sad_tot_amt;
                }

                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                if (chkOthSales.Checked == false)
                {
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
                }
                else
                {
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), null, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
                }

                InvoiceHeader _invheader = new InvoiceHeader();

                //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                _invheader.Sah_com = Session["UserCompanyCode"].ToString();
                _invheader.Sah_cre_by = Session["UserID"].ToString();
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = _currency;
                _invheader.Sah_currency = Session["_currency"].ToString();
                _invheader.Sah_cus_add1 = lblInvCusAdd1.Text.Trim();
                _invheader.Sah_cus_add2 = lblInvCusAdd2.Text.Trim();
                _invheader.Sah_cus_cd = txtCusCode.Text.Trim();
                _invheader.Sah_cus_name = lblInvCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = _dCusAdd1;
                _invheader.Sah_d_cust_add1 = Session["_dCusAdd1"].ToString();
                _invheader.Sah_d_cust_add2 = _dCusAdd2;
                _invheader.Sah_d_cust_add2 = Session["_dCusAdd1"].ToString();
                _invheader.Sah_d_cust_cd = _dCusCode;
                _invheader.Sah_d_cust_cd = Session["_dCusCode"].ToString();
                _invheader.Sah_direct = false;
                _invheader.Sah_dt = Convert.ToDateTime(txtSRNDate.Text).Date;
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = _exRate;
                _invheader.Sah_ex_rt = Convert.ToDecimal(Session["_exRate"].ToString());
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "REV";
                _invheader.Sah_inv_tp = _invTP;
                _invheader.Sah_inv_tp = Session["_invTP"].ToString();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = _manCode;
                _invheader.Sah_man_cd = Session["_manCode"].ToString();
                _invheader.Sah_man_ref = txtManRef.Text.Trim();
                _invheader.Sah_manual = false;
                _invheader.Sah_mod_by = Session["UserID"].ToString();
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = lblSalePc.Text.Trim(); //Session["UserDefProf"].ToString();
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtInvoice.Text.Trim();
                _invheader.Sah_remarks = txtSRNremarks.Text;
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = _executiveCD;
                _invheader.Sah_sales_ex_cd = Session["_executiveCD"].ToString();
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = Session["SessionID"].ToString();
                _invheader.Sah_structure_seq = "";
                _invheader.Sah_stus = "A";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_tax_inv = _isTax;
                _invheader.Sah_tax_inv = Convert.ToBoolean(Session["_isTax"].ToString());
                _invheader.Sah_anal_5 = txtSubType.Text.Trim();
                _invheader.Sah_anal_3 = lblReq.Text.Trim();
                _invheader.Sah_anal_4 = "ARQT014";
                _invheader.Sah_anal_2 = txtManRef.Text;
                // _invheader.Sah_anal_8 = Convert.ToDecimal(lblCrAmt.Text);
                // _invheader.Sah_anal_7 = _retVal;
                _invheader.Sah_anal_7 = Convert.ToDecimal(lblCrAmt.Text);

                if (_invHdr != null)
                {
                    if (_invHdr.Count > 0)
                    {
                        _invheader.Sah_currency = _invHdr[0].Sah_currency;
                        _invheader.Sah_cus_cd = _invHdr[0].Sah_cus_cd;
                        _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                        _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                        _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                        _invheader.Sah_pdi_req = _invHdr[0].Sah_pdi_req;
                        _invheader.Sah_remarks = _invHdr[0].Sah_remarks;
                        _invheader.Sah_tax_inv = _invHdr[0].Sah_tax_inv;
                        _invheader.Sah_is_svat = _invHdr[0].Sah_is_svat;
                    }
                }





                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = lblSalePc.Text.Trim();
                _invoiceAuto.Aut_cate_tp = "PC";
                _invoiceAuto.Aut_direction = 0;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "REV";
                _invoiceAuto.Aut_number = 0;
                if (Session["UserCompanyCode"].ToString() == "LRP")
                {
                    _invoiceAuto.Aut_start_char = "RINREV";
                }
                else if (Session["UserCompanyCode"].ToString() == "AIS")
                {
                    _invoiceAuto.Aut_start_char = "AINREV";

                }
                else
                {
                    _invoiceAuto.Aut_start_char = "INREV";
                }
                _invoiceAuto.Aut_year = null;

                InventoryHeader _inventoryHeader = new InventoryHeader();
                MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                //inventory document

                if (_doitemserials.Count > 0)
                {
                    _inventoryHeader.Ith_com = Session["UserCompanyCode"].ToString();
                    _inventoryHeader.Ith_loc = Session["UserDefLoca"].ToString();
                    DateTime _docDate = Convert.ToDateTime(txtSRNDate.Text).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = txtSubType.Text.Trim();
                    _inventoryHeader.Ith_bus_entity = "";
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim(); ;
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = Session["SessionID"].ToString();
                    _inventoryHeader.Ith_pc = lblSalePc.Text.Trim();
                    _inventoryHeader.Ith_oth_loc = txtActLoc.Text.Trim();


                    _SRNAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(txtSRNDate.Text).Year;
                }

                List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

                if (chkRevReg.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _regDetails
                                group n by new { n.P_srvt_ref_no } into r
                                select new { P_srvt_ref_no = r.Key.P_srvt_ref_no, P_svrt_reg_val = r.Sum(p => p.P_svrt_reg_val) }).ToList();

                    RecieptHeader _revRecHdr = new RecieptHeader();
                    _regRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), s.P_srvt_ref_no);
                        _revRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.P_srvt_ref_no);
                        _revRecHdr.Sar_tot_settle_amt = s.P_svrt_reg_val;
                        _revRecHdr.Sar_direct = false;
                        _regRecList.Add(_revRecHdr);
                    }

                    _regReversReceiptHeader = new List<RecieptHeader>();
                    _regRevReceipt = new MasterAutoNumber();

                    _regRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _regRevReceipt.Aut_cate_tp = "PC";
                    _regRevReceipt.Aut_direction = null;
                    _regRevReceipt.Aut_modify_dt = null;
                    _regRevReceipt.Aut_moduleid = "RECEIPT";
                    _regRevReceipt.Aut_number = 0;
                    _regRevReceipt.Aut_start_char = "RGRF";
                    _regRevReceipt.Aut_year = null;

                    _regReversReceiptHeader = _regRecList;
                }

                List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

                if (chkRevIns.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _insDetails
                                group n by new { n.Svit_ref_no } into r
                                select new { Svit_ref_no = r.Key.Svit_ref_no, Svit_ins_val = r.Sum(p => p.Svit_ins_val) }).ToList();

                    RecieptHeader _revInsRecHdr = new RecieptHeader();
                    _insRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), s.Svit_ref_no);
                        _revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.Svit_ref_no);
                        _revInsRecHdr.Sar_tot_settle_amt = s.Svit_ins_val;
                        _revInsRecHdr.Sar_direct = false;
                        _insRecList.Add(_revInsRecHdr);
                    }

                    _insReversReceiptHeader = new List<RecieptHeader>();
                    _insRevReceipt = new MasterAutoNumber();

                    _insRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _insRevReceipt.Aut_cate_tp = "PC";
                    _insRevReceipt.Aut_direction = null;
                    _insRevReceipt.Aut_modify_dt = null;
                    _insRevReceipt.Aut_moduleid = "RECEIPT";
                    _insRevReceipt.Aut_number = 0;
                    _insRevReceipt.Aut_start_char = "RGRF";
                    _insRevReceipt.Aut_year = null;

                    _insReversReceiptHeader = _insRecList;
                }

                string _ReversNo = "";
                string _crednoteNo = ""; //add by chamal 05-12-2012

                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal15"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();
                    if (_newStus1 != null)
                    {
                        _newStus = _newStus1[0];

                        string _stus;
                        string _itm = _ser.Tus_itm_cd;
                        string _orgStus = _ser.Tus_itm_stus;
                        string _serial = _ser.Tus_ser_1;
                        if (!string.IsNullOrEmpty(_newStus))
                        {
                            _stus = _newStus;

                            DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                            if (dt.Rows.Count > 0)
                            {
                                DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                    {
                                        string _Msg = "Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial + ",Cash sales reversal";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                        return;
                                    }
                                }
                            }
                        }

                    }



                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(Session["UserCompanyCode"].ToString(), "RVR", _newStus, (((Convert.ToDateTime(txtSRNDate.Text).Year - _ser.Tus_doc_dt.Year) * 12) + Convert.ToDateTime(txtSRNDate.Text).Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }

                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }

                }

                if (!ChechServiceInvoice())
                {
                    return;
                }

                #region Check receving serials are duplicating :: Chamal 08-May-2014
                string _err = string.Empty;
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    //string _Msg = _err.ToString();
                    string _Msg = "Please check the below item(s). These serial(s) are already available in your location";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    return;
                }
                #endregion
                #region Check Scan Completed
                ReptPickHeader _tmpPickHdr = new ReptPickHeader();
                _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                {
                    Tuh_doc_no = _invheader.Sah_ref_doc,
                    Tuh_doc_tp = "SRN",
                    Tuh_direct = true,
                    Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                    Tuh_usr_loc = Session["UserDefLoca"].ToString()
                }).FirstOrDefault();
                if (_tmpPickHdr != null)
                {
                    if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                    {
                        if (_tmpPickHdr.Tuh_fin_stus != 1)
                        {
                            //lbtnSave.Enabled = true;
                            //lbtnSave.CssClass = "buttonUndocolorLeft floatRight";
                            //lbtnSave.OnClientClick = "SaveConfirma();";
                            // DisplayMessage("Scanning is not completed for the selected document !");
                            string _Msg = "Scanning is not completed for the selected document !";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                            return;
                        }
                    }
                }
                #endregion

                foreach (var item in _doitemserials)
                {
                    item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
                }
                _inventoryHeader.Ith_gen_frm = "SCMWEB";
                _invheader.Sah_process_name = "EXCHANGEWEB";
                int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, _regDetails, chkRevReg.Checked, _insRevReceipt, _insReversReceiptHeader, _insDetails, chkRevIns.Checked, _isOthRev, Session["UserDefProf"].ToString(), _refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, chkCashRefund.Checked, out _crednoteNo);
                //Added By Dulaj 2018/Nov/14
                if (effect == 1)
                {
                    DataTable dtTempHdr = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), txtInvoice.Text);
                    List<ReptPickSerials> tempPickSer = new List<ReptPickSerials>();
                    if (dtTempHdr != null)
                    {
                        if (dtTempHdr.Rows.Count > 0)
                        {
                            int effect2 = CHNLSVC.Sales.UpdateCashConvertionDocNo(txtInvoice.Text, _ReversNo);
                        }
                    }
                }
                //
                
                Clear_Page();

                if (effect == 1)
                {
                    string _Msg = string.Empty;
                    if (string.IsNullOrEmpty(_crednoteNo))
                    {
                        _Msg = "Successfully created.Reversal No: " + _ReversNo;
                    }
                    else
                    {
                        _Msg = "Successfully created.Reversal No   : " + _ReversNo + "  " + "SRN No   :" + "  " + _crednoteNo;
                    }
                    //string _Msg = "Successfully created.Reversal No: " + _ReversNo;
                    //string _Msg = "Successfully created.Reversal No   : " + _ReversNo + "  " + "SRN No   :" + "  " + _crednoteNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);


                    //ReportViewer _view = new ReportViewer();
                    //Session["GlbReportName"] = string.Empty;
                    //_view.GlbReportName = string.Empty;
                    //BaseCls.GlbReportTp = "INV";
                    //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                    //_view.GlbReportDoc = _ReversNo;
                    //_view.GlbSerial = null;
                    //_view.GlbWarranty = null;
                    //_view.Show();
                    //_view = null;

                    //if (!string.IsNullOrEmpty(_crednoteNo))
                    //{
                    //    ReportViewerInventory _insu = new ReportViewerInventory();
                    //    Session["GlbReportName"] = string.Empty;
                    //    _insu.GlbReportName = string.Empty;
                    //    BaseCls.GlbReportTp = "INWARD";
                    //    _insu.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SInward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                    //    _insu.GlbReportDoc = _crednoteNo;
                    //    _insu.Show();
                    //    _insu = null;
                    //}

                }
                else
                {
                    if (!string.IsNullOrEmpty(_ReversNo))
                    {
                        string _Msg = _ReversNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    }
                    else
                    {
                        string _Msg = "Creation Fail.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    }

                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);

            }
        }
        private void TempProcess(bool _isTemp)
        {
            try
            {
                string _msg = "";
                decimal _retVal = 0;
                Boolean _isOthRev = false;
                string _orgPC = "";
                _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                if (_doitemserials == null)
                {
                    _doitemserials = new List<ReptPickSerials>();
                }
                if (CheckServerDateTime() == false) return;

                if (grdInvItem.Rows.Count <= 0)
                {
                    string _Msg = "Cannot find reverse details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    return;
                }


                if (chkOthSales.Checked == true)
                {
                    _isOthRev = true;
                }
                else
                {
                    _isOthRev = false;
                }

                _orgPC = lblSalePc.Text.Trim();

                decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtSRNDate.Text).Date, out _wkNo, Session["UserCompanyCode"].ToString());

                if (_weekNo == 0)
                {
                    string _Msg = "Week Definition is still not setup for current date.Please contact retail accounts dept.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtSRNDate.Text).Date != DateTime.Now.Date)
                        {
                            txtSRNDate.Enabled = true;
                            string _Msg = "Back date not allow for selected date!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            txtSRNDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtSRNDate.Enabled = true;
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        txtSRNDate.Focus();
                        return;
                    }
                }


                decimal rtnQty = 0;
                decimal regQty = 0;

                if (chkRevReg.Checked == true)
                {
                    if (_regDetails.Count > 0 && _regDetails != null)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            regQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicalRegistration tempReg in _regDetails)
                            {
                                if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                                {
                                    regQty = regQty + 1;
                                }
                            }

                            if (rtnQty < regQty)
                            {
                                string _Msg = "You are going to reverse registration details more than return qty.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                                return;
                            }
                        }
                    }
                    else
                    {
                        string _Msg = "Cannot find registration details.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                        return;
                    }
                }


                decimal insQty = 0;
                rtnQty = 0;
                if (chkRevIns.Checked == true)
                {
                    if (_insDetails.Count > 0 && _insDetails != null)
                    {
                        foreach (InvoiceItem temp in _InvDetailList)
                        {
                            rtnQty = 0;
                            insQty = 0;
                            rtnQty = temp.Sad_srn_qty;

                            foreach (VehicleInsuarance tempIns in _insDetails)
                            {
                                if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                                {
                                    insQty = insQty + 1;
                                }
                            }

                            if (rtnQty < insQty)
                            {
                                string _Msg = "You are going to reverse insuarance details more than return qty.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                                return;
                            }
                        }
                    }
                    else
                    {
                        string _Msg = "Cannot find insuarance details.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }


                if (chkCashRefund.Checked == true)
                {
                    CollectRefApp();
                    CollectRefAppLog();
                }

                _retVal = 0;

                foreach (InvoiceItem tmpItem in _InvDetailList)
                {
                    _retVal = _retVal + tmpItem.Sad_tot_amt;
                }


                /*  ANAL 8*/
                #region anal 8
                decimal _totRetAmt = 0;
                decimal _crAmt = 0;
                decimal _outAmt = 0;
                decimal _preRevAmt = 0;
                decimal _preCrAmt = 0;
                decimal _balCrAmt = 0;

                foreach (InvoiceItem _temp in _InvDetailList)
                {
                    _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
                }

                lblTotalRevAmt.Text = _preRevAmt.ToString("n");
                lblTotRetAmt.Text = _totRetAmt.ToString("n");

                _outAmt = Convert.ToDecimal(lblOutAmt.Text);
                _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

                _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

                if (_balCrAmt < 0)
                {
                    _balCrAmt = 0;
                }

                if (_outAmt > 0)
                {
                    _crAmt = _balCrAmt - _outAmt;
                }
                else
                {
                    if (_totRetAmt <= _balCrAmt)
                    {
                        _crAmt = _totRetAmt;
                    }
                    else
                    {
                        _crAmt = _balCrAmt;
                    }
                }

                if (_crAmt > 0)
                {
                    lblCrAmt.Text = _crAmt.ToString("n");
                }
                else
                {
                    lblCrAmt.Text = "0";
                }
                #endregion
                ////////////////////////////////////////////////////////////////
                InvoiceHeader _invheader = new InvoiceHeader();

                //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                _invheader.Sah_com = Session["UserCompanyCode"].ToString();
                _invheader.Sah_cre_by = Session["UserID"].ToString();
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = _currency;
                _invheader.Sah_currency = Session["_currency"].ToString();
                _invheader.Sah_cus_add1 = lblInvCusAdd1.Text.Trim();
                _invheader.Sah_cus_add2 = lblInvCusAdd2.Text.Trim();
                _invheader.Sah_cus_cd = txtCusCode.Text.Trim();
                _invheader.Sah_cus_name = lblInvCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = _dCusAdd1;
                _invheader.Sah_d_cust_add1 = Session["_dCusAdd1"].ToString();
                _invheader.Sah_d_cust_add2 = _dCusAdd2;
                _invheader.Sah_d_cust_add2 = Session["_dCusAdd1"].ToString();
                _invheader.Sah_d_cust_cd = _dCusCode;
                _invheader.Sah_d_cust_cd = Session["_dCusCode"].ToString();
                _invheader.Sah_direct = false;
                _invheader.Sah_dt = Convert.ToDateTime(txtSRNDate.Text).Date;
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = _exRate;
                _invheader.Sah_ex_rt = Convert.ToDecimal(Session["_exRate"].ToString());
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "REV";
                _invheader.Sah_inv_tp = _invTP;
                _invheader.Sah_inv_tp = Session["_invTP"].ToString();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = _manCode;
                _invheader.Sah_man_cd = Session["_manCode"].ToString();
                _invheader.Sah_man_ref = txtManRef.Text.Trim();
                _invheader.Sah_manual = false;
                _invheader.Sah_mod_by = Session["UserID"].ToString();
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = lblSalePc.Text.Trim(); //Session["UserDefProf"].ToString();
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtInvoice.Text.Trim();
                _invheader.Sah_remarks = txtSRNremarks.Text;
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = _executiveCD;
                _invheader.Sah_sales_ex_cd = Session["_executiveCD"].ToString();
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = Session["SessionID"].ToString();
                _invheader.Sah_structure_seq = "";
                _invheader.Sah_stus = "A";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_tax_inv = _isTax;
                _invheader.Sah_tax_inv = Convert.ToBoolean(Session["_isTax"].ToString());
                _invheader.Sah_anal_5 = txtSubType.Text.Trim();
                _invheader.Sah_anal_3 = lblReq.Text.Trim();
                _invheader.Sah_anal_4 = "ARQT014";
                _invheader.Sah_anal_8 = Convert.ToDecimal(lblCrAmt.Text);
                _invheader.Sah_anal_7 = _retVal;


                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = lblSalePc.Text.Trim();
                _invoiceAuto.Aut_cate_tp = "PC";
                _invoiceAuto.Aut_direction = 0;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "REV";
                _invoiceAuto.Aut_number = 0;
                if (Session["UserCompanyCode"].ToString() == "LRP")
                {
                    _invoiceAuto.Aut_start_char = "RINREV";
                }
                else if (Session["UserCompanyCode"].ToString() == "AIS")
                {
                    _invoiceAuto.Aut_start_char = "AINREV";
                }
                else
                {
                    _invoiceAuto.Aut_start_char = "INREV";
                }
                _invoiceAuto.Aut_year = null;

                InventoryHeader _inventoryHeader = new InventoryHeader();
                MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                //inventory document

                if (_InvDetailList.Count > 0)//
                {
                    _inventoryHeader.Ith_com = Session["UserCompanyCode"].ToString();
                    _inventoryHeader.Ith_loc = Session["UserDefLoca"].ToString();
                    DateTime _docDate = Convert.ToDateTime(txtSRNDate.Text).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = txtSubType.Text.Trim();
                    _inventoryHeader.Ith_bus_entity = "";
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim(); ;
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = Session["SessionID"].ToString();
                    _inventoryHeader.Ith_pc = lblSalePc.Text.Trim();
                    _inventoryHeader.Ith_oth_loc = txtActLoc.Text.Trim();
                    _inventoryHeader.Ith_oth_docno = txtInvoice.Text;
                    _inventoryHeader.Ith_bus_entity = txtCusCode.Text;

                    _SRNAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(txtSRNDate.Text).Year;
                }

                List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

                if (chkRevReg.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _regDetails
                                group n by new { n.P_srvt_ref_no } into r
                                select new { P_srvt_ref_no = r.Key.P_srvt_ref_no, P_svrt_reg_val = r.Sum(p => p.P_svrt_reg_val) }).ToList();

                    RecieptHeader _revRecHdr = new RecieptHeader();
                    _regRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), s.P_srvt_ref_no);
                        _revRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.P_srvt_ref_no);
                        _revRecHdr.Sar_tot_settle_amt = s.P_svrt_reg_val;
                        _revRecHdr.Sar_direct = false;
                        _regRecList.Add(_revRecHdr);
                    }

                    _regReversReceiptHeader = new List<RecieptHeader>();
                    _regRevReceipt = new MasterAutoNumber();

                    _regRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _regRevReceipt.Aut_cate_tp = "PC";
                    _regRevReceipt.Aut_direction = null;
                    _regRevReceipt.Aut_modify_dt = null;
                    _regRevReceipt.Aut_moduleid = "RECEIPT";
                    _regRevReceipt.Aut_number = 0;
                    _regRevReceipt.Aut_start_char = "RGRF";
                    _regRevReceipt.Aut_year = null;

                    _regReversReceiptHeader = _regRecList;
                }

                List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
                MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

                if (chkRevIns.Checked == true)
                {
                    //revers registration receipts
                    var _lst = (from n in _insDetails
                                group n by new { n.Svit_ref_no } into r
                                select new { Svit_ref_no = r.Key.Svit_ref_no, Svit_ins_val = r.Sum(p => p.Svit_ins_val) }).ToList();

                    RecieptHeader _revInsRecHdr = new RecieptHeader();
                    _insRecList = new List<RecieptHeader>();

                    foreach (var s in _lst)
                    {
                        //_revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), s.Svit_ref_no);
                        _revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.Svit_ref_no);
                        _revInsRecHdr.Sar_tot_settle_amt = s.Svit_ins_val;
                        _revInsRecHdr.Sar_direct = false;
                        _insRecList.Add(_revInsRecHdr);
                    }

                    _insReversReceiptHeader = new List<RecieptHeader>();
                    _insRevReceipt = new MasterAutoNumber();

                    _insRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                    _insRevReceipt.Aut_cate_tp = "PC";
                    _insRevReceipt.Aut_direction = null;
                    _insRevReceipt.Aut_modify_dt = null;
                    _insRevReceipt.Aut_moduleid = "RECEIPT";
                    _insRevReceipt.Aut_number = 0;
                    _insRevReceipt.Aut_start_char = "RGRF";
                    _insRevReceipt.Aut_year = null;

                    _insReversReceiptHeader = _insRecList;
                }

                string _ReversNo = "";
                string _crednoteNo = ""; //add by chamal 05-12-2012

                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal15"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();
                    if (_newStus1 != null)
                    {
                        _newStus = _newStus1[0];

                        string _stus;
                        string _itm = _ser.Tus_itm_cd;
                        string _orgStus = _ser.Tus_itm_stus;
                        string _serial = _ser.Tus_ser_1;
                        if (!string.IsNullOrEmpty(_newStus))
                        {
                            _stus = _newStus;

                            DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                            if (dt.Rows.Count > 0)
                            {
                                DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                    {
                                        string _Msg = "Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial + ",Cash sales reversal";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                        return;
                                    }
                                }
                            }
                        }

                    }



                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(Session["UserCompanyCode"].ToString(), "RVR", _newStus, (((Convert.ToDateTime(txtSRNDate.Text).Year - _ser.Tus_doc_dt.Year) * 12) + Convert.ToDateTime(txtSRNDate.Text).Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }

                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }

                }

                if (!ChechServiceInvoice())
                {
                    return;
                }

                #region Check receving serials are duplicating :: Chamal 08-May-2014
                string _err = string.Empty;
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    string _Msg = _err.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    return;
                }
                #endregion
                foreach (var item in _doitemserials)
                {
                    item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
                }
                _inventoryHeader.Ith_gen_frm = "SCMWEB";
                int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, _regDetails, chkRevReg.Checked, _insRevReceipt, _insReversReceiptHeader, _insDetails, chkRevIns.Checked, _isOthRev, Session["UserDefProf"].ToString(), _refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, chkCashRefund.Checked, out _crednoteNo, _isTemp);

                Clear_Page();

                if (effect == 1)
                {
                    string _Msg = "Successfully created Temp Reversal No: " + _ReversNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);


                    //ReportViewer _view = new ReportViewer();
                    //Session["GlbReportName"] = string.Empty;
                    //_view.GlbReportName = string.Empty;
                    //BaseCls.GlbReportTp = "INV";
                    //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                    //_view.GlbReportDoc = _ReversNo;
                    //_view.GlbSerial = null;
                    //_view.GlbWarranty = null;
                    //_view.Show();
                    //_view = null;

                    //if (!string.IsNullOrEmpty(_crednoteNo))
                    //{
                    //    ReportViewerInventory _insu = new ReportViewerInventory();
                    //    Session["GlbReportName"] = string.Empty;
                    //    _insu.GlbReportName = string.Empty;
                    //    BaseCls.GlbReportTp = "INWARD";
                    //    _insu.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SInward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                    //    _insu.GlbReportDoc = _crednoteNo;
                    //    _insu.Show();
                    //    _insu = null;
                    //}

                }
                else
                {
                    if (!string.IsNullOrEmpty(_ReversNo))
                    {
                        string _Msg = _ReversNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    }
                    else
                    {
                        string _Msg = "Creation Fail.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    }

                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);

            }
        }
        #endregion
        #region  Rooting for Request
        protected void lbtnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestconformmessageValue.Value == "Yes")
                {
                    RequestProcess();
                }


            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }
        private void RequestProcess()
        {
            try
            {
                _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
                if (_repAddDet == null)
                {
                    _repAddDet = new List<RequestAppAddDet>();
                }
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    string _Msg = "Invoice customer is missing.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    string _Msg = "Please select invoice number.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    string _Msg = "Please enter remarks.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtRemarks.Focus();
                    return;
                }

                if (grdInvItem.Rows.Count <= 0)
                {
                    string _Msg = "No items are selected to generate request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtInvoice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    string _Msg = "Please select return category.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtSubType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblSalePc.Text))
                {
                    string _Msg = "Original sales profit center is missing.Please re-enter invoice #.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (chkRevReg.Checked == true)
                {
                    if (grdRegDetails.Rows.Count <= 0)
                    {
                        string _Msg = "No registration details are not found to generate registration refund request.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtInvoice.Focus();
                        return;
                    }
                }

                if (chkRevIns.Checked == true)
                {
                    if (grdInsDetails.Rows.Count <= 0)
                    {
                        string _Msg = "No insuarance details are not found to generate insuarance refund request.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtInvoice.Focus();
                        return;
                    }
                }

                //check pending requests for invoice
                List<RequestApprovalHeader> _pendingRequest = CHNLSVC.General.GetPendingSRNRequest(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text, "ARQT014");
                if (_pendingRequest != null && _pendingRequest.Count > 0)
                {
                    List<RequestApprovalHeader> _app = (from _res in _pendingRequest
                                                        where _res.Grah_app_stus == "A"
                                                        select _res).ToList<RequestApprovalHeader>();
                    if (_app != null && _app.Count > 0)
                    {
                        string _Msg = "This invoice has approved Reuqest.Pleses Finish approve request.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                    List<RequestApprovalHeader> _pen = (from _res in _pendingRequest
                                                        where _res.Grah_app_stus == "P"
                                                        select _res).ToList<RequestApprovalHeader>();
                    if (_pen != null && _pen.Count > 0)
                    {
                        string _Msg = "This invoice has pending request, Please approve pending request";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;

                    }
                }


                decimal rtnQty = 0;
                decimal regQty = 0;

                if (_regDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        regQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicalRegistration tempReg in _regDetails)
                        {
                            if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                            {
                                regQty = regQty + 1;
                            }
                        }

                        if (rtnQty < regQty)
                        {
                            string _Msg = "You are going to reverse registration details more than return qty.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                }

                decimal insQty = 0;

                if (_insDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        insQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicleInsuarance tempIns in _insDetails)
                        {
                            if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                            {
                                insQty = insQty + 1;
                            }
                        }

                        if (rtnQty < insQty)
                        {
                            string _Msg = "You are going to reverse insuarance details more than return qty.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                }

                _status = "P";
                Session["_status"] = "P";
                DataTable _appSt = CHNLSVC.Sales.checkAppStatus(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "P", this.GlbModuleName);
                if (_appSt.Rows.Count > 0)
                {
                    if (lblSalePc.Text.Trim() != Session["UserDefProf"].ToString())
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "ApConfirm();", true);

                        if (txtAppconformmessageValue.Value == "Yes")
                        {
                            _status = "P";
                            Session["_status"] = "P";
                        }
                        else
                        {
                            _status = "P";
                            Session["_status"] = "P";
                            return;
                        }
                    }
                    else
                    {
                        _status = "A";
                        Session["_status"] = "A";
                    }
                }
                else
                {
                    if (lblSalePc.Text.Trim() == Session["UserDefProf"].ToString())
                    {
                        if (Convert.ToDateTime(lblInvDate.Text).Date == Convert.ToDateTime(DateTime.Now).Date)
                        {
                            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10057))
                            {
                                _status = "A";
                                Session["_status"] = "A";
                            }
                            else
                            {
                                _status = "P";
                                Session["_status"] = "P";
                            }
                        }
                        else
                        {
                            _status = "P";
                            Session["_status"] = "P";
                        }
                    }
                    else
                    {
                        _status = "P";
                        Session["_status"] = "P";
                    }
                }

                if (Session["_status"].ToString() == "P")
                {
                    if (txtSubType.Text != "REF")
                    {
                        if (_repAddDet.Count <= 0)
                        {
                            string _Msg = "Please enter re-report item details.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                }
                _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                //   _InvDetailList.RemoveAll(x => x.Sad_srn_qty == 0);
                //var _filte2 = _InvDetailList.Where(x => x.Sad_srn_qty == 1).ToList();
                //if (_filte2.Count == 0)
                //{
                //    var _filte = _InvDetailList.Where(x => x.Sad_srn_qty == 0).ToList();
                //    if (_filte.Count > 0)
                //    {
                //        string _Msg = "Please add the revers quntity";
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //        return;
                //    }
                //}
                CollectReqApp();
                CollectReqAppLog();

                if (chkRevReg.Checked == true)
                {
                    CollectRegApp();
                    CollectRegAppLog();
                }

                if (chkRevIns.Checked == true)
                {
                    CollectInsApp();
                    CollectInsAppLog();
                }


                var _lst = (from n in _ReqAppDet
                            group n by new { n.Grad_anal2 } into r
                            select new { Grad_anal2 = r.Key.Grad_anal2, grad_val3 = r.Sum(p => p.Grad_val3) }).ToList();
                foreach (var s in _lst)
                {
                    string _item = s.Grad_anal2;
                    decimal _qty = s.grad_val3;

                    MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                    decimal _count = _ReqAppSer.Where(X => X.Gras_anal2 == _item).Count();

                    if (_itm.Mi_cd != null)
                    {
                        DataTable _type = new DataTable();
                        _type = CHNLSVC.Sales.GetItemTp(_itm.Mi_itm_tp);

                        if (_type.Rows.Count > 0)
                        {
                            if (Convert.ToInt16(_type.Rows[0]["mstp_is_inv"]) == 1)
                            {
                                if (_qty != _count)
                                {
                                    //MessageBox.Show("Deliverd qty and serial mismatch. DO Qty : " + _qty + " Serials :" + _count + " for the item : " + _item, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //return;
                                }
                            }
                        }

                    }

                }


                List<InvoiceItem> _temp = new List<InvoiceItem>();
                InvoiceItem _orgInvDet = new InvoiceItem();
                _temp = _InvDetailList;
                string _revItem = "";
                string _delItem = "";
                Int32 _line = 0;
                decimal _rtnQty = 0;
                decimal _invQty = 0;
                decimal _doQty = 0;
                decimal _curRtnQty = 0;

                foreach (InvoiceItem itm in _temp)
                {
                    if (!string.IsNullOrEmpty(itm.Sad_sim_itm_cd))
                    {
                        _delItem = itm.Sad_sim_itm_cd;
                    }
                    else
                    {
                        _delItem = itm.Sad_itm_cd;
                    }
                    _line = itm.Sad_itm_line;
                    _rtnQty = itm.Sad_srn_qty;
                    _revItem = itm.Sad_itm_cd;

                    _orgInvDet = CHNLSVC.Sales.GetInvDetByLine(itm.Sad_inv_no, _revItem, _line);

                    if (_orgInvDet.Sad_inv_no == null)
                    {
                        string _Msg = "Cannot load item details in invoice. " + _revItem + ", Cash sales reversal";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }

                    _invQty = _orgInvDet.Sad_qty;
                    _doQty = _orgInvDet.Sad_do_qty;
                    _curRtnQty = _orgInvDet.Sad_srn_qty;

                    if (_invQty < _rtnQty)
                    {
                        string _Msg = "You are going to revers more then invoice qty.Item : " + _revItem + " Inv. Qty : " + _invQty + "Rtn. Qty : " + _rtnQty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }

                    if ((_invQty - _curRtnQty) < _rtnQty)
                    {
                        string _Msg = "You are going to revers more then current available qty.Item : " + _revItem + " Inv. Qty : " + _invQty + "Current Rtn. Qty : " + _rtnQty + "Already Rtn. Qty : " + _curRtnQty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }

                    MasterItem _itmDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _revItem);

                    DataTable _rtnItmtype = new DataTable();
                    _rtnItmtype = CHNLSVC.Sales.GetItemTp(_itmDet.Mi_itm_tp);

                    if (_rtnItmtype.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(_rtnItmtype.Rows[0]["mstp_is_inv"]) == 1)
                        {
                            decimal _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count();

                            if (_doQty < _serCount)
                            {
                                string _Msg = "You are going to revers more than deliverd qty.Item : " + _revItem + " Del. Qty : " + _doQty + "Serial : " + _serCount;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                return;
                            }

                            //imagin previous srns are having
                            if (_rtnQty != _serCount)
                            {
                                if (_invQty - _doQty == 0)
                                {
                                    if (_rtnQty != _serCount)
                                    {
                                        string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        return;
                                    }

                                    if (_rtnQty < _serCount)
                                    {
                                        string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    //if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                    //{
                    //    itm.Sad_srn_qty = itm.Sad_srn_qty - _qty;
                    //    itm.Sad_fws_ignore_qty = itm.Sad_fws_ignore_qty - _qty;
                    //}

                }

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, _ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegAuto, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, chkRevReg.Checked, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsAuto, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, chkRevIns.Checked, out _docNo, out _regAppNo, out _insAppNo, _repAddDet);

                if (effet == 1)
                {
                    if (chkRevReg.Checked == false && chkRevIns.Checked == false)
                    {
                        string _Msg = "Request generated." + _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
                    }
                    else if (chkRevReg.Checked == true && chkRevIns.Checked == false)
                    {
                        string _Msg = "Request generated.Rev. req. # : " + _docNo + " Reg. refund Req. # : " + _regAppNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);


                    }
                    else if (chkRevReg.Checked == false && chkRevIns.Checked == true)
                    {
                        string _Msg = "Request generated.Rev. req. # : " + _docNo + " Ins. refund Req. # : " + _insAppNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);


                    }
                    else if (chkRevReg.Checked == true && chkRevIns.Checked == true)
                    {
                        string _Msg = "Request generated.Rev. req. # : " + _docNo + " Ins. refund Req. # : " + _insAppNo + " Reg. refund req. # : " + _regAppNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);


                    }
                    Clear_Page();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    {
                        string _Msg = _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
                    }
                    else
                    {
                        string _Msg = "Creation fail";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        #endregion
        #region  Rooting for Reject
        protected void lbtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRejectconformmessageValue.Value == "No")
                {

                    return;
                }
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16037))
                {
                    string Msg = "Sorry, You have no permission to reject!( Advice: Required permission code :16037)";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                    return;
                }
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    string _Msg = "Please select the request number.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (lblStatus.Text == "FINISHED")
                {
                    string _Msg = "Request is already finished.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (lblStatus.Text == "APPROVED")
                {
                    string _Msg = "Request is already approved.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    string _Msg = "Request is already rejected.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = Session["UserCompanyCode"].ToString();
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "R";
                _RequestApprovalStatus.Grah_app_by = Session["UserID"].ToString();
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = txtSubType.Text.Trim();

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                if (_rowEffect == 1)
                {
                    string _Msg = "Successfully rejected.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
                    this.Clear_Page();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                    }
                    else
                    {
                        string _Msg = "Fail to approved.Please re-try";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

            }
        }
        #endregion
        #region  Rooting for Approval
        protected void lbtnApprovalSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16038))
                {
                    string Msg = "Sorry, You have no permission to approve!( Advice: Required permission code :16038)";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                    return;
                }

                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(lblReq.Text) == true)
                {
                    string _Msg = "Please select the request number.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (lblStatus.Text == "FINISHED")
                {
                    string _Msg = "Request is already finished.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                if (lblStatus.Text == "APPROVED")
                {
                    string _Msg = "Request is already approved.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (lblStatus.Text == "REJECT")
                {
                    string _Msg = "Request is already rejected.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    string _Msg = "Please select reversal sub type.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                if (txtApprovalconformmessageValue.Value == "No")
                {
                    return;
                }
                //set_approveUser_infor("ARQT014");

                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                _RequestApprovalStatus.Grah_com = Session["UserCompanyCode"].ToString();
                _RequestApprovalStatus.Grah_loc = lblReqPC.Text.Trim();
                _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                _RequestApprovalStatus.Grah_ref = lblReq.Text;
                _RequestApprovalStatus.Grah_app_stus = "A";
                _RequestApprovalStatus.Grah_app_by = Session["UserID"].ToString();
                _RequestApprovalStatus.Grah_app_lvl = 1;
                _RequestApprovalStatus.Grah_remaks = txtRemarks.Text.Trim();
                _RequestApprovalStatus.Grah_sub_type = txtSubType.Text.Trim();

                //update request details
                foreach (GridViewRow row in grdSelectReversal.Rows)
                {

                    string _stus;

                    string _itm = (row.FindControl("colR_item") as Label).Text;
                    string _orgStus = (row.FindControl("colR_itmstatus") as Label).Text;
                    string _serial = (row.FindControl("colR_Serial") as Label).Text;
                    _stus = (row.FindControl("colR_appstatus") as Label).Text;
                    if (_stus == "")
                    {
                        string _Msg = "Please Select Item Status";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }

                    if (_stus != null)
                    {
                        //_stus = grv.Cells["col_appstatus"].Value.ToString();

                        DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                            if (dt1.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                {
                                    string _Msg = "Cannot approved different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial;
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        _stus = (row.FindControl("colR_appstatus") as Label).Text;
                    }
                    CHNLSVC.Sales.UpdateRequestAppStatus(_stus, lblReq.Text, _itm);
                }

                _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);

                //_rowEffect=CHNLSVC.Sales.UpdateRequestAppStatus(

                if (_rowEffect == 1)
                {
                    string _Msg = "Successfully approved.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
                    this.Clear_Page();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                    }
                    else
                    {
                        string _Msg = "Fail to approved.Please re-try";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    }
                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);

            }
        }
        #endregion
        #region  Rooting for Cancel
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            lblErrorcancel.Text = "";
            txtCancelInvoice.Text = "";
            userCancelPopoup.Show();
        }

        protected void lbtnCancelProcess_Click(object sender, EventArgs e)
        {
            if (txtCancelconformmessageValue.Value == "No")
            {
                userCancelPopoup.Show();
                return;
            }
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16039))
            {
                string Msg = "Sorry, You have no permission to cancel!( Advice: Required permission code :16039)";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                return;
            }
            double housrs = 0;
            lblErrorcancel.Text = "";
            List<Hpr_SysParameter> _list = CHNLSVC.Sales.GetAll_hpr_Para("REVPERIOD", "COM", Session["UserCompanyCode"].ToString());
            if (_list != null && _list.Count > 0)
            {
                housrs = (double)_list[0].Hsy_val;
            }

            //get reversal
            InvoiceHeader _header = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCancelInvoice.Text.Trim().ToUpper());
            //validate
            if (_header == null)
            {
                string _Msg = "Invalid invoice no";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                userCancelPopoup.Show();
                return;
            }

            if (_header.Sah_direct != false)
            {

                string _Msg = "Invalid invoice no";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                userCancelPopoup.Show();
                return;
            }
            // Added by Nadeeka 13-05-2015
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (_header.Sah_dt.Date != Convert.ToDateTime(txtSRNDate.Text))
                    {

                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        userCancelPopoup.Show();
                        return;
                    }
                }
                else
                {


                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    userCancelPopoup.Show();
                    return;
                }
            }

            string _error = "";


            //check crnote used or not
            List<RecieptItem> _recList = CHNLSVC.Sales.GetRecieptItemByRef(txtCancelInvoice.Text.Trim());
            if (_recList != null && _recList.Count > 0)
            {

                string _Msg = "Credit Note used as Payment, can not cancel";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                userCancelPopoup.Show();
                return;
            }

            string _do = "";
            // Check SRN items available or not =====  Nadeeka 12-05-2015
            DataTable _tblSRN = CHNLSVC.Sales.Check_SRN_Stock_Avilability(txtCancelInvoice.Text.Trim());
            if (_tblSRN.Rows.Count > 0)
            {
                foreach (DataRow drow in _tblSRN.Rows)
                {
                    _do = _do + " Doc # " + drow["ITH_DOC_NO"].ToString() + " Doc Date " + Convert.ToDateTime(drow["ITH_DOC_DATE"].ToString()).ToString("dd-MM-yyyy") + "\n";
                }
            }
            if (_tblSRN != null && _tblSRN.Rows.Count > 0)
            {

                string _Msg = "SRN Items not available, can not cancel " + _do;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                userCancelPopoup.Show();
                return;
            }

            //process
            int result = CHNLSVC.Sales.ProcessReversalCancel(_header, Session["UserID"].ToString(), DateTime.Now, out _error);
            if (!string.IsNullOrEmpty(_error))
            {

                string _Msg = "Error occurred while processing";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
                userCancelPopoup.Show();

            }
            else
            {
                string _Msg = "Successfully Cancelled";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
                userCancelPopoup.Hide();
                return;
            }


        }

        protected void lbtnCancelClear_Click(object sender, EventArgs e)
        {
            txtCancelInvoice.Text = "";
            userCancelPopoup.Show();
        }
        #endregion
        #region  Rooting for Reverse
        protected void lbtnReverse_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "FINISHED")
            {
                string _Msg = "Request is already finished.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }
            _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
            _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
            _ReqAppSer = ViewState["_ReqAppSer"] as List<RequestApprovalSerials>;
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10110))    //kapila 31/8/2015
            {
                string _Msg = "Sorry, You have no permission for reverse!( Advice: Required permission code :10110)";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }

            string _docNo = "";
            string _regAppNo = "";
            string _insAppNo = "";
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                string _Msg = "Invoice customer is missing.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                txtCusCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtInvoice.Text))
            {
                string _Msg = "Please select invoice number.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                txtInvoice.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                string _Msg = "Please enter remarks.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                txtRemarks.Focus();
                return;
            }

            if (grdInvItem.Rows.Count <= 0)
            {
                string _Msg = "No items are selected to generate request.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                txtInvoice.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSubType.Text))
            {
                string _Msg = "Please select return category.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                txtSubType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lblSalePc.Text))
            {
                string _Msg = "Original sales profit center is missing.Please re-enter invoice #.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                return;
            }

            if (chkRevReg.Checked == true)
            {
                if (grdRegDetails.Rows.Count <= 0)
                {
                    string _Msg = "No registration details are not found to generate registration refund request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    txtInvoice.Focus();
                    return;
                }
            }

            if (chkRevIns.Checked == true)
            {
                if (grdInsDetails.Rows.Count <= 0)
                {
                    string _Msg = "No insuarance details are not found to generate insuarance refund request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtInvoice.Focus();
                    return;
                }
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtSRNDate.Text).Date != DateTime.Now.Date)
                    {

                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        txtSRNDate.Focus();
                        return;
                    }
                }
                else
                {

                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtSRNDate.Focus();
                    return;
                }
            }

            //check pending requests for invoice
            List<RequestApprovalHeader> _pendingRequest = CHNLSVC.General.GetPendingSRNRequest(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text, "ARQT014");
            if (_pendingRequest != null && _pendingRequest.Count > 0)
            {
                List<RequestApprovalHeader> _app = (from _res in _pendingRequest
                                                    where _res.Grah_app_stus == "A"
                                                    select _res).ToList<RequestApprovalHeader>();
                if (_app != null && _app.Count > 0)
                {
                    string _Msg = "This invoice has approved Reuqest.Pleses Finish approve request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
                List<RequestApprovalHeader> _pen = (from _res in _pendingRequest
                                                    where _res.Grah_app_stus == "P"
                                                    select _res).ToList<RequestApprovalHeader>();
                if (_pen != null && _pen.Count > 0)
                {
                    string _Msg = "This invoice has pending request, Please approve pending request";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;

                }
            }


            decimal rtnQty = 0;
            decimal regQty = 0;

            if (_regDetails.Count > 0 && _regDetails != null)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    regQty = 0;
                    rtnQty = temp.Sad_srn_qty;

                    foreach (VehicalRegistration tempReg in _regDetails)
                    {
                        if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                        {
                            regQty = regQty + 1;
                        }
                    }

                    if (rtnQty < regQty)
                    {
                        string _Msg = "You are going to reverse registration details more than return qty.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }
            }

            decimal insQty = 0;

            if (_insDetails.Count > 0 && _insDetails != null)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    insQty = 0;
                    rtnQty = temp.Sad_srn_qty;

                    foreach (VehicleInsuarance tempIns in _insDetails)
                    {
                        if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                        {
                            insQty = insQty + 1;
                        }
                    }

                    if (rtnQty < insQty)
                    {
                        string _Msg = "You are going to reverse insuarance details more than return qty.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }
            }

            _status = "A";
            Session["_status"] = _status;

            if (_status == "P")
            {
                if (txtSubType.Text != "REF")
                {
                    if (_repAddDet.Count <= 0 && _repAddDet != null)
                    {
                        string _Msg = "Please enter re-report item details.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                    }
                }
            }
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            //  _InvDetailList.RemoveAll(x => x.Sad_srn_qty == 0);
            //var _filte2 = _InvDetailList.Where(x => x.Sad_srn_qty == 1).ToList();
            //if (_filte2.Count == 0)
            //{
            //    var _filte = _InvDetailList.Where(x => x.Sad_srn_qty == 0).ToList();
            //    if (_filte.Count > 0)
            //    {
            //        string _Msg = "Please add the revers quntity";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //        return;
            //    }
            //}

            CollectReqApp();
            CollectReqAppLog();

            if (chkRevReg.Checked == true)
            {
                CollectRegApp();
                CollectRegAppLog();
            }

            if (chkRevIns.Checked == true)
            {
                CollectInsApp();
                CollectInsAppLog();
            }


            var _lst = (from n in _ReqAppDet
                        group n by new { n.Grad_anal2 } into r
                        select new { Grad_anal2 = r.Key.Grad_anal2, grad_val3 = r.Sum(p => p.Grad_val3) }).ToList();
            foreach (var s in _lst)
            {
                string _item = s.Grad_anal2;
                decimal _qty = s.grad_val3;

                MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                decimal _count = _ReqAppSer.Where(X => X.Gras_anal2 == _item).Count();

                if (_itm.Mi_cd != null)
                {
                    DataTable _type = new DataTable();
                    _type = CHNLSVC.Sales.GetItemTp(_itm.Mi_itm_tp);

                    if (_type.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(_type.Rows[0]["mstp_is_inv"]) == 1)
                        {
                            if (_qty != _count)
                            {
                                //MessageBox.Show("Deliverd qty and serial mismatch. DO Qty : " + _qty + " Serials :" + _count + " for the item : " + _item, "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //return;
                            }
                        }
                    }

                }

            }


            List<InvoiceItem> _temp = new List<InvoiceItem>();
            InvoiceItem _orgInvDet = new InvoiceItem();
            _temp = _InvDetailList;
            string _revItem = "";
            string _delItem = "";
            Int32 _line = 0;
            decimal _rtnQty = 0;
            decimal _invQty = 0;
            decimal _doQty = 0;
            decimal _curRtnQty = 0;

            foreach (InvoiceItem itm in _temp)
            {
                if (!string.IsNullOrEmpty(itm.Sad_sim_itm_cd))
                {
                    _delItem = itm.Sad_sim_itm_cd;
                }
                else
                {
                    _delItem = itm.Sad_itm_cd;
                }
                _line = itm.Sad_itm_line;
                _rtnQty = itm.Sad_srn_qty;
                _revItem = itm.Sad_itm_cd;

                _orgInvDet = CHNLSVC.Sales.GetInvDetByLine(itm.Sad_inv_no, _revItem, _line);

                if (_orgInvDet.Sad_inv_no == null)
                {
                    string _Msg = "Cannot load item details in invoice. " + _revItem;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                _invQty = _orgInvDet.Sad_qty;
                _doQty = _orgInvDet.Sad_do_qty;
                _curRtnQty = _orgInvDet.Sad_srn_qty;

                if (_invQty < _rtnQty)
                {
                    string _Msg = "You are going to revers more then invoice qty.Item : " + _revItem + " Inv. Qty : " + _invQty + "Rtn. Qty : " + _rtnQty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    return;
                }

                if ((_invQty - _curRtnQty) < _rtnQty)
                {
                    string _Msg = "You are going to revers more then current available qty.Item : " + _revItem + " Inv. Qty : " + _invQty + "Current Rtn. Qty : " + _rtnQty + "Already Rtn. Qty : " + _curRtnQty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

                MasterItem _itmDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _revItem);

                DataTable _rtnItmtype = new DataTable();
                _rtnItmtype = CHNLSVC.Sales.GetItemTp(_itmDet.Mi_itm_tp);

                if (_rtnItmtype.Rows.Count > 0 && _rtnItmtype != null)
                {
                    if (Convert.ToInt16(_rtnItmtype.Rows[0]["mstp_is_inv"]) == 1)
                    {
                        if (_ReqAppSer.Count > 0)
                        {
                            decimal _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count();

                            if (_doQty < _serCount)
                            {
                                string _Msg = "You are going to revers more than deliverd qty.Item : " + _revItem + " Del. Qty : " + _doQty + "Serial : " + _serCount;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                return;
                            }

                            //imagin previous srns are having
                            if (_rtnQty != _serCount)
                            {
                                if (_invQty - _doQty == 0)
                                {
                                    if (_rtnQty != _serCount)
                                    {
                                        string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        return;
                                    }

                                    if (_rtnQty < _serCount)
                                    {
                                        string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }



            }

            // int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, _ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegAuto, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, chkRevReg.Checked, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsAuto, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, chkRevIns.Checked, out _docNo, out _regAppNo, out _insAppNo, _repAddDet);

            // if (effet == 1)
            //  {
            //     DataTable _dtReq = CHNLSVC.Sales.getReqHdrByReqNo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _docNo);

            //     load_approved_req(_dtReq.Rows[0]["grah_ref"].ToString(), _dtReq.Rows[0]["grah_app_stus"].ToString(), _dtReq.Rows[0]["grah_fuc_cd"].ToString(), _dtReq.Rows[0]["grah_remaks"].ToString(), _dtReq.Rows[0]["grah_oth_loc"].ToString(), _dtReq.Rows[0]["grah_loc"].ToString(), _dtReq.Rows[0]["grah_sub_type"].ToString(), _dtReq.Rows[0]["grah_oth_pc"].ToString());

            save_reverse();

            //Clear_Page();
            // }
            // else
            // {
            //    if (!string.IsNullOrEmpty(_docNo))
            //    {
            //        string _Msg = "_docNo";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

            //    }
            //    else
            //    {
            //        string _Msg = "Creation fail.";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


            //    }
            //}



        }
        private void save_reverse()
        {

            string _msg = "";
            decimal _retVal = 0;
            Boolean _isOthRev = false;
            string _orgPC = "";

            if (CheckServerDateTime() == false) return;

            //if (string.IsNullOrEmpty(lblReq.Text))
            //{
            //    string _Msg = "Please select an approved request.";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}

            //if (lblReqPC.Text != Session["UserDefProf"].ToString())
            //{
            //    string _Msg = "Request profit center and your profit center is not match.";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}

            //if (lblReturnLoc.Text != Session["UserDefLoca"].ToString())
            //{
            //    string _Msg = "Request location and your profit center is not match.";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}

            //if (lblStatus.Text != "APPROVED")
            //{
            //    string _Msg = "Request is still not approved.";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //    return;
            //}

            if (grdInvItem.Rows.Count <= 0)
            {
                string _Msg = "Cannot find reverse details.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }


            if (chkOthSales.Checked == true)
            {
                _isOthRev = true;
            }
            else
            {
                _isOthRev = false;
            }

            _orgPC = lblSalePc.Text.Trim();

            decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtSRNDate.Text).Date, out _wkNo, Session["UserCompanyCode"].ToString());

            if (_weekNo == 0)
            {
                string _Msg = "Week Definition is still not setup for current date.Please contact retail accounts dept.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtSRNDate.Text).Date != DateTime.Now.Date)
                    {
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtSRNDate.Focus();
                        return;
                    }
                }
                else
                {
                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtSRNDate.Focus();
                    return;
                }
            }


            decimal rtnQty = 0;
            decimal regQty = 0;

            if (chkRevReg.Checked == true)
            {
                if (_regDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        regQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicalRegistration tempReg in _regDetails)
                        {
                            if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                            {
                                regQty = regQty + 1;
                            }
                        }

                        if (rtnQty < regQty)
                        {
                            string _Msg = "You are going to reverse registration details more than return qty.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    string _Msg = "Cannot find registration details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
            }


            decimal insQty = 0;
            rtnQty = 0;
            if (chkRevIns.Checked == true)
            {
                if (_insDetails.Count > 0)
                {
                    foreach (InvoiceItem temp in _InvDetailList)
                    {
                        rtnQty = 0;
                        insQty = 0;
                        rtnQty = temp.Sad_srn_qty;

                        foreach (VehicleInsuarance tempIns in _insDetails)
                        {
                            if (temp.Sad_itm_cd == tempIns.Svit_itm_cd)
                            {
                                insQty = insQty + 1;
                            }
                        }

                        if (rtnQty < insQty)
                        {
                            string _Msg = "You are going to reverse insuarance details more than return qty.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    string _Msg = "Cannot find insuarance details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
            }


            if (chkCashRefund.Checked == true)
            {
                CollectRefApp();
                CollectRefAppLog();
            }

            _retVal = 0;

            foreach (InvoiceItem tmpItem in _InvDetailList)
            {

                //unit price
                tmpItem.Sad_unit_amt = tmpItem.Sad_unit_rt * tmpItem.Sad_srn_qty;
                //Cus_ITM_QTY invoice qty
                tmpItem.Sad_itm_tax_amt = (tmpItem.Sad_itm_tax_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_disc_amt = (tmpItem.Sad_disc_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_disc_rt = (tmpItem.Sad_disc_rt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_tot_amt = (tmpItem.Sad_tot_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                //
                _retVal = _retVal + tmpItem.Sad_tot_amt;
            }

            /*  ANAL 8*/
            #region anal 8
            decimal _totRetAmt = 0;
            decimal _crAmt = 0;
            decimal _outAmt = 0;
            decimal _preRevAmt = 0;
            decimal _preCrAmt = 0;
            decimal _balCrAmt = 0;
            Boolean _is_SVAT = false;
            decimal _tot_tax_amt = 0;
            DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(txtInvoice.Text.Trim());
            if (_revAmt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                {
                    _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                }
            }

            DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(txtInvoice.Text.Trim());
            if (_preCr.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                {
                    _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                }
            }

            foreach (InvoiceItem _temp in _InvDetailList)
            {
                _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
            }

            lblTotalRevAmt.Text = _preRevAmt.ToString("n");
            lblTotRetAmt.Text = _totRetAmt.ToString("n");

            _outAmt = Convert.ToDecimal(lblOutAmt.Text);
            _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

            _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

            if (_balCrAmt < 0)
            {
                _balCrAmt = 0;
            }

            if (_outAmt > 0)
            {
                _crAmt = _balCrAmt - _outAmt;
            }
            else
            {
                if (_totRetAmt <= _balCrAmt)
                {
                    _crAmt = _totRetAmt;
                }
                else
                {
                    _crAmt = _balCrAmt;
                }
            }

            if (_is_SVAT == true)
            {
                List<InvoiceItem> _paramInvItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), null);
                foreach (InvoiceItem item in _paramInvItems)
                {
                    _tot_tax_amt = _tot_tax_amt + (Convert.ToDecimal(item.Sad_itm_tax_amt));
                }
                _crAmt = _crAmt + _tot_tax_amt;
                lblOutAmt.Text = (Convert.ToDecimal(lblOutAmt.Text) - _tot_tax_amt).ToString("0.00");
                lblTotPayAmt.Text = (Convert.ToDecimal(lblTotPayAmt.Text) + _tot_tax_amt).ToString("0.00");
            }

            if (_crAmt > 0)
            {
                lblCrAmt.Text = _crAmt.ToString("n");
            }
            else
            {
                lblCrAmt.Text = "0";
            }
            #endregion
            ////////////////////////////////////////////////////////////////


            InvoiceHeader _invheader = new InvoiceHeader();

            //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
            _invheader.Sah_com = Session["UserCompanyCode"].ToString();
            _invheader.Sah_cre_by = Session["UserID"].ToString();
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = _currency;
            _invheader.Sah_cus_add1 = lblInvCusAdd1.Text.Trim();
            _invheader.Sah_cus_add2 = lblInvCusAdd2.Text.Trim();
            _invheader.Sah_cus_cd = txtCusCode.Text.Trim();
            _invheader.Sah_cus_name = lblInvCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = _dCusAdd1;
            _invheader.Sah_d_cust_add2 = _dCusAdd2;
            _invheader.Sah_d_cust_cd = _dCusCode;
            _invheader.Sah_direct = false;
            _invheader.Sah_dt = Convert.ToDateTime(txtSRNDate.Text).Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = _exRate;
            _invheader.Sah_ex_rt = Convert.ToDecimal(Session["_exRate"].ToString());
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "REV";
            _invheader.Sah_inv_tp = _invTP;
            _invheader.Sah_inv_tp = Session["_invTP"].ToString();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = _manCode;
            _invheader.Sah_man_ref = txtManRef.Text.Trim();
            _invheader.Sah_manual = false;
            _invheader.Sah_mod_by = Session["UserID"].ToString();
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = lblSalePc.Text.Trim(); //Session["UserDefProf"].ToString();
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtInvoice.Text.Trim();
            _invheader.Sah_remarks = txtSRNremarks.Text;
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = Session["_executiveCD"].ToString();
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = 1;
            _invheader.Sah_session_id = Session["SessionID"].ToString();
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "A";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_tax_inv = _isTax;
            _invheader.Sah_anal_5 = txtSubType.Text.Trim();
            _invheader.Sah_anal_3 = lblReq.Text.Trim();
            _invheader.Sah_anal_4 = "ARQT014";
            //_invheader.Sah_anal_8 = Convert.ToDecimal(lblCrAmt.Text);
            _invheader.Sah_anal_7 = Convert.ToDecimal(lblCrAmt.Text);
            _invheader.Sah_anal_2 = txtManRef.Text.Trim();

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();

            if (chkOthSales.Checked == false)
            {
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
            }
            else
            {
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), null, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);
            }


            if (_invHdr != null)
            {
                if (_invHdr.Count > 0)
                {
                    _invheader.Sah_currency = _invHdr[0].Sah_currency;
                    _invheader.Sah_cus_cd = _invHdr[0].Sah_cus_cd;
                    _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                    _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                    _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                    _invheader.Sah_pdi_req = _invHdr[0].Sah_pdi_req;
                    _invheader.Sah_remarks = _invHdr[0].Sah_remarks;
                    _invheader.Sah_tax_inv = _invHdr[0].Sah_tax_inv;
                    _invheader.Sah_is_svat = _invHdr[0].Sah_is_svat;
                    _invheader.Sah_tax_exempted = _invHdr[0].Sah_tax_exempted;
                    //add by lakshan 13 Mar 2017 as per the dharshana
                    _invheader.Sah_man_ref = _invHdr[0].Sah_man_ref;
                    _invheader.Sah_currency = _invHdr[0].Sah_currency;
                    _invheader.Sah_town_cd = _invHdr[0].Sah_town_cd;
                    _invheader.Sah_d_cust_cd = _invHdr[0].Sah_d_cust_cd;
                    _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                    _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                    _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                    //_invheader.Sah_anal_2 = _invHdr[0].Sah_anal_2;
                    _invheader.Sah_acc_no = _invHdr[0].Sah_acc_no;
                    _invheader.Sah_d_cust_name = _invHdr[0].Sah_d_cust_name;
                    //  _invheader.Sah_inv_no = _invHdr[0].Sah_inv_no;
                }
            }



            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = lblSalePc.Text.Trim();
            _invoiceAuto.Aut_cate_tp = "PC";
            _invoiceAuto.Aut_direction = 0;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "REV";
            _invoiceAuto.Aut_number = 0;
            if (Session["UserCompanyCode"].ToString() == "LRP")
            {
                _invoiceAuto.Aut_start_char = "RINREV";
            }
            else if (Session["UserCompanyCode"].ToString() == "AIS")
            {
                _invoiceAuto.Aut_start_char = "AINREV";
            }
            else
            {
                _invoiceAuto.Aut_start_char = "INREV";
            }
            _invoiceAuto.Aut_year = null;

            InventoryHeader _inventoryHeader = new InventoryHeader();
            MasterAutoNumber _SRNAuto = new MasterAutoNumber();
            //inventory document
            if (_doitemserials != null)
            {
                if (_doitemserials.Count > 0)
                {
                    _inventoryHeader.Ith_com = Session["UserCompanyCode"].ToString();
                    _inventoryHeader.Ith_loc = Session["UserDefLoca"].ToString();
                    DateTime _docDate = Convert.ToDateTime(txtSRNDate.Text).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = txtSubType.Text.Trim();
                    _inventoryHeader.Ith_bus_entity = Session["_dCusCode"].ToString();
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_remarks = txtSRNremarks.Text.Trim(); ;
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = Session["UserID"].ToString();
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = Session["SessionID"].ToString();
                    _inventoryHeader.Ith_pc = lblSalePc.Text.Trim();
                    _inventoryHeader.Ith_oth_loc = txtActLoc.Text.Trim();


                    _SRNAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(txtSRNDate.Text).Year;
                }
            }

            List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

            if (chkRevReg.Checked == true)
            {
                //revers registration receipts
                var _lst = (from n in _regDetails
                            group n by new { n.P_srvt_ref_no } into r
                            select new { P_srvt_ref_no = r.Key.P_srvt_ref_no, P_svrt_reg_val = r.Sum(p => p.P_svrt_reg_val) }).ToList();

                RecieptHeader _revRecHdr = new RecieptHeader();
                _regRecList = new List<RecieptHeader>();

                foreach (var s in _lst)
                {
                    _revRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.P_srvt_ref_no);
                    _revRecHdr.Sar_tot_settle_amt = s.P_svrt_reg_val;
                    _revRecHdr.Sar_direct = false;
                    _regRecList.Add(_revRecHdr);
                }

                _regReversReceiptHeader = new List<RecieptHeader>();
                _regRevReceipt = new MasterAutoNumber();

                _regRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                _regRevReceipt.Aut_cate_tp = "PC";
                _regRevReceipt.Aut_direction = null;
                _regRevReceipt.Aut_modify_dt = null;
                _regRevReceipt.Aut_moduleid = "RECEIPT";
                _regRevReceipt.Aut_number = 0;
                _regRevReceipt.Aut_start_char = "RGRF";
                _regRevReceipt.Aut_year = null;

                _regReversReceiptHeader = _regRecList;
            }

            List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

            if (chkRevIns.Checked == true)
            {
                //revers registration receipts
                var _lst = (from n in _insDetails
                            group n by new { n.Svit_ref_no } into r
                            select new { Svit_ref_no = r.Key.Svit_ref_no, Svit_ins_val = r.Sum(p => p.Svit_ins_val) }).ToList();

                RecieptHeader _revInsRecHdr = new RecieptHeader();
                _insRecList = new List<RecieptHeader>();

                foreach (var s in _lst)
                {
                    _revInsRecHdr = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), s.Svit_ref_no);
                    _revInsRecHdr.Sar_tot_settle_amt = s.Svit_ins_val;
                    _revInsRecHdr.Sar_direct = false;
                    _insRecList.Add(_revInsRecHdr);
                }

                _insReversReceiptHeader = new List<RecieptHeader>();
                _insRevReceipt = new MasterAutoNumber();

                _insRevReceipt.Aut_cate_cd = lblSalePc.Text.Trim();
                _insRevReceipt.Aut_cate_tp = "PC";
                _insRevReceipt.Aut_direction = null;
                _insRevReceipt.Aut_modify_dt = null;
                _insRevReceipt.Aut_moduleid = "RECEIPT";
                _insRevReceipt.Aut_number = 0;
                _insRevReceipt.Aut_start_char = "RGRF";
                _insRevReceipt.Aut_year = null;

                _insReversReceiptHeader = _insRecList;
            }

            string _ReversNo = "";
            string _crednoteNo = ""; //add by chamal 05-12-2012
            if (_doitemserials != null)
            {
                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(lblReq.Text);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal2"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();
                    if (_newStus1 != null)
                    {
                        if (_newStus1.Count > 0)
                        {
                            _newStus = _newStus1[0];

                            string _stus;
                            string _itm = _ser.Tus_itm_cd;
                            string _orgStus = _ser.Tus_itm_stus;
                            string _serial = _ser.Tus_ser_1;
                            if (!string.IsNullOrEmpty(_newStus))
                            {
                                _stus = _newStus;

                                DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                                if (dt.Rows.Count > 0)
                                {
                                    DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                                    if (dt1.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                        {

                                            string _Msg = "Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial;
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                            return;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if (_ser.Tus_ser_1 != "N/A")
                    {
                        List<ReptPickSerials> dOSERILA = new List<ReptPickSerials>();
                        dOSERILA = CHNLSVC.Inventory.GetInvoiceSerialForReversalBYSerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), _ser.Tus_bin, _ser.Tus_base_doc_no, _ser.Tus_ser_1);
                        if (dOSERILA != null)
                        {
                            if (dOSERILA.Count > 0)
                            {
                                _ser.Tus_doc_no = dOSERILA[0].Tus_doc_no;
                            }
                        }
                    }
                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(Session["UserCompanyCode"].ToString(), "RVR", _newStus, (((Convert.ToDateTime(txtSRNDate.Text).Year - _ser.Tus_doc_dt.Year) * 12) + Convert.ToDateTime(txtSRNDate.Text).Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }

                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }

                }
            }

            if (!ChechServiceInvoice())
            {
                return;
            }

            #region Check receving serials are duplicating :: Chamal 08-May-2014
            string _err = string.Empty;
            if (_doitemserials != null)
            {
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    string _Msg = _err.ToString();
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    DisplayMessage(_Msg, 1);

                    return;
                }
                //    if (CHNLSVC.Inventory.CheckSerialFoundDO(_inventoryHeader.Ith_com, _inventoryHeader.Ith_entry_no, _doitemserials, out _err) <= 0)
                //    {
                //        string _Msg = _err.ToString();
                //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //        DisplayMessage(_Msg, 1);

                //        return;
                //}
            }
            #endregion

            int reptSeqNo = 0;
            if (_doitemserials != null)
            {
                if (_doitemserials.Count > 0)
                {
                    reptSeqNo = _doitemserials[0].Tus_usrseq_no;
                }
            }
            else
            {
                _doitemserials = new List<ReptPickSerials>();
            }
            bool _serialsNotAva = false;
            string _itmCd = ""; Int32 _invLine = 0;
            decimal _selectedSerialCount = 0, _balQty = 0;
            foreach (var item in _InvDetailList)
            {
                if (_doitemserials != null)
                {
                    var vList = _doitemserials.Where(c => c.Tus_itm_cd == item.Sad_itm_cd && c.Tus_base_itm_line == item.Sad_itm_line).ToList();
                    if (vList != null)
                    {
                        _selectedSerialCount = vList.Sum(c => c.Tus_qty);
                    }
                }
                if (item.Sad_do_qty > 0)
                {
                    ////temp commented by Wimal @ 24/07/2018 to raised SRn with FW sales Qty
                    _balQty = item.Sad_qty - item.Sad_do_qty;
                    if (_balQty <= 0)
                    {
                        //added by Wimal @ 24/07/2018
                        if (item.Sad_srn_qty < _selectedSerialCount)
                        //Commennted by Wimal @ 24/07/2018
                        //if (item.Sad_srn_qty > _selectedSerialCount)
                        {
                            _itmCd = item.Sad_itm_cd;
                            _invLine = item.Sad_itm_line;
                            _serialsNotAva = true;
                            break;
                        }
                    }
                    else
                    {
                        //by Wimal @ 03/09/2018 - update Do qty on FWD sales reversal.
                        item.Sad_fws_ignore_qty = (item.Sad_srn_qty - item.Sad_do_qty);
                        //////temp REMOVED by Wimal @ 24/07/2018 to raised SRn with FW sales Qty
                        //if (item.Sad_srn_qty > _balQty)
                        //{
                        //    _itmCd = item.Sad_itm_cd;
                        //    _invLine = item.Sad_itm_line;
                        //    _serialsNotAva = true;
                        //    break;
                        //}
                    }
                    //////temp added by Wimal @ 24/07/2018 to raised SRn with FW sales Qty
                    //_balQty = item.Sad_qty - item.Sad_do_qty; //fw sale Qty
                    //if (item.Sad_srn_qty - _selectedSerialCount > _balQty)
                    //{
                    //    _itmCd = item.Sad_itm_cd;
                    //    _invLine = item.Sad_itm_line;
                    //    _serialsNotAva = true;
                    //    break;
                    //}
                }
                //by Wimal @ 03/09/2018 - update Do qty on FWD sales reversal.
                if (item.Sad_do_qty == 0)
                {
                    item.Sad_fws_ignore_qty = (item.Sad_srn_qty);
                }

            }
            if (_serialsNotAva)
            {
                DisplayMessage("Already DO has be made for item " + _itmCd + " in line # " + _invLine.ToString() + ". Therefore please select the reversal serial !", 1); return;
            }
            foreach (var item in _doitemserials)
            {
                item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
            }
            _inventoryHeader.Ith_gen_frm = "SCMWEB";
            _invheader.Sah_process_name = "EXCHANGEWEB";
            int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, _regDetails, chkRevReg.Checked, _insRevReceipt, _insReversReceiptHeader, _insDetails, chkRevIns.Checked, _isOthRev, Session["UserDefProf"].ToString(), _refAppHdr, _refAppDet, _refAppAuto, _refAppHdrLog, _refAppDetLog, chkCashRefund.Checked, out _crednoteNo);



            if (effect == 1)
            {
                if (reptSeqNo != 0)
                {
                    CHNLSVC.Inventory.DeleteTempPickObjs(reptSeqNo);
                }

                string _Msg = string.Empty;
                if (string.IsNullOrEmpty(_crednoteNo))
                {
                    _Msg = "Successfully created.Reversal No: " + _ReversNo;
                }
                else
                {
                    _Msg = "Successfully created.Reversal No   : " + _ReversNo + "  " + "SRN No   :" + "  " + _crednoteNo;
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
                Session["documntNo"] = _ReversNo;
                _print();
                Clear_Page();
                //lblMssg.Text = "Do you want print now?";
                //PopupConfBox.Show();
                //ReportViewer _view = new ReportViewer();
                //Session["GlbReportName"] = string.Empty;
                //_view.GlbReportName = string.Empty;
                //BaseCls.GlbReportTp = "INV";
                //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                //_view.GlbReportDoc = _ReversNo;
                //_view.GlbSerial = null;
                //_view.GlbWarranty = null;
                //_view.Show();
                //_view = null;

                if (!string.IsNullOrEmpty(_crednoteNo))
                {
                    //ReportViewerInventory _insu = new ReportViewerInventory();
                    //Session["GlbReportName"] = string.Empty;
                    //_insu.GlbReportName = string.Empty;
                    //BaseCls.GlbReportTp = "INWARD";
                    //_insu.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SInward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt";
                    //_insu.GlbReportDoc = _crednoteNo;
                    //_insu.Show();
                    //_insu = null;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(_ReversNo))
                {

                    //string _Msg = "_ReversNo";
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _ReversNo + "');", true);
                    DisplayMessage(_ReversNo, 4);

                }
                else
                {

                    string _Msg = "Creation Fail.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                }

            }

        }



        private void load_approved_req(string _reqno, string _staus, string _invno, string _rem, string _tp, string _prof, string _subtp, string _othpc)
        {

            string _reqNo = "";
            string _stus = "";
            string _invNo = "";
            string _remarks = "";
            string _type = "";
            string _pc = "";
            string _retLoc = "";
            string _retSubType = "";
            string _salesPC = "";

            txtInvoice.Text = "";
            txtCusCode.Text = "";
            lblInvCusName.Text = "";
            lblInvCusAdd1.Text = "";
            lblInvCusAdd2.Text = "";
            lblInvDate.Text = "";
            lblSalePc.Text = "";
            txtInvoice.Enabled = true;
            txtCusCode.Enabled = true;
            //btnRequest.Enabled = false;


            //grdPaymentDetails.Rows.Clear();

            _reqNo = _reqno;
            _stus = _staus;
            _invNo = _invno;
            _remarks = _rem;
            _type = _tp;
            _pc = _prof;
            _retSubType = _subtp;
            _salesPC = _othpc;



            if (_salesPC != _pc)
            {
                chkOthSales.Checked = true;
                chkOthSales.Enabled = false;
            }
            else
            {
                chkOthSales.Checked = false;
                chkOthSales.Enabled = false;
            }

            txtInvoice.Text = _invNo;
            lblReq.Text = _reqNo;
            txtRemarks.Text = _remarks;
            txtSRNremarks.Text = _remarks;
            lblReturnLoc.Text = _type;
            lblReqPC.Text = _pc;
            txtSubType.Text = _retSubType;
            lblSalePc.Text = _salesPC;

            lblSubDesc.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtSubType.Text))
            {
                if (IsValidAdjustmentSubType() == false)
                {
                    string _Msg = "Invalid return sub type.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    lblSubDesc.Text = string.Empty;
                    txtSubType.Text = "";
                    txtSubType.Focus();
                    return;
                }
            }
            if (_stus == "A")
            {
                lblStatus.Text = "APPROVED";

            }
            else if (_stus == "P")
            {
                lblStatus.Text = "PENDING";
            }
            else if (_stus == "R")
            {
                lblStatus.Text = "REJECT";
            }
            else if (_stus == "F")
            {
                lblStatus.Text = "FINISHED";
            }

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), _salesPC, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtSRNDate.Text, txtSRNDate.Text);

            foreach (InvoiceHeader _tempInv in _invHdr)
            {
                if (_tempInv.Sah_inv_no == null)
                {
                    string _Msg = "Error loading request.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtInvoice.Text = "";
                    txtCusCode.Text = "";
                    lblInvCusName.Text = "";
                    lblInvCusAdd1.Text = "";
                    lblInvCusAdd2.Text = "";
                    lblInvDate.Text = "";
                    lblSalePc.Text = "";
                    lblTotInvAmt.Text = "";
                    lblTotPayAmt.Text = "";
                    lblOutAmt.Text = "";
                    lblTotRetAmt.Text = "";
                    lblTotalRevAmt.Text = "";

                    _dCusCode = "";
                    _dCusAdd1 = "";
                    _dCusAdd2 = "";
                    _currency = "";
                    _exRate = 0;
                    _invTP = "";
                    _executiveCD = "";
                    _manCode = "";
                    _isTax = false;
                    txtInvoice.Focus();
                    return;
                }
                else
                {
                    txtCusCode.Text = _tempInv.Sah_cus_cd;
                    lblInvCusName.Text = _tempInv.Sah_cus_name;
                    lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                    lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                    lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                    lblSalePc.Text = _tempInv.Sah_pc;
                    lblTotInvAmt.Text = _tempInv.Sah_anal_7.ToString("n");
                    lblTotPayAmt.Text = _tempInv.Sah_anal_8.ToString("n");
                    lblOutAmt.Text = (_tempInv.Sah_anal_7 - _tempInv.Sah_anal_8).ToString("n");

                    _dCusCode = _tempInv.Sah_d_cust_cd;
                    _dCusAdd1 = _tempInv.Sah_d_cust_add1;
                    _dCusAdd2 = _tempInv.Sah_d_cust_add2;
                    _currency = _tempInv.Sah_currency;
                    _exRate = _tempInv.Sah_ex_rt;
                    _invTP = _tempInv.Sah_inv_tp;
                    _executiveCD = _tempInv.Sah_sales_ex_cd;
                    _manCode = _tempInv.Sah_man_cd;
                    _isTax = _tempInv.Sah_tax_inv;

                    if (lblStatus.Text == "FINISHED")
                    {
                        string _Msg = "Selected request is in FINISHED status.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        //btnApp.Enabled = false;
                        // btnCancel.Enabled = false;
                        return;
                    }
                    else if (_tempInv.Sah_stus == "C")
                    {
                        string _Msg = "Selected invoice is cancelled.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        //btnApp.Enabled = false;
                        //btnCancel.Enabled = false;
                        return;
                    }
                    else if (_tempInv.Sah_stus == "R")
                    {
                        string _Msg = "This invoice is already reversed.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                        //btnApp.Enabled = false;
                        //btnCancel.Enabled = false;
                        return;
                    }
                    else if (lblStatus.Text == "APPROVED")
                    {
                        // btnCancel.Enabled = true;
                    }
                    _isFromReq = true;
                    chkRevReg.Checked = false;
                    chkRevReg.Enabled = false;
                    chkRevIns.Checked = false;
                    chkRevIns.Enabled = false;

                    txtCusCode.Enabled = false;
                    txtInvoice.Enabled = false;
                    //btnRequest.Enabled = false;
                    //dgvInvItem.Columns["col_invRevQty"].ReadOnly = true;



                    List<InvoiceItem> _tmpInv = new List<InvoiceItem>();
                    List<InvoiceItem> _newList = new List<InvoiceItem>();
                    List<ReptPickSerials> _tempser = new List<ReptPickSerials>();

                    decimal _rtnSerQty = 0;
                    decimal _fwsQty = 0;

                    _tmpInv = _InvDetailList;
                    _InvDetailList = null;


                    foreach (InvoiceItem itm in _tmpInv)
                    {
                        _rtnSerQty = 0;
                        _fwsQty = 0;
                        foreach (ReptPickSerials _tmpser in _doitemserials)
                        {

                            string _item = _tmpser.Tus_itm_cd;
                            Int32 _line = _tmpser.Tus_base_itm_line;

                            if (itm.Sad_itm_cd == _item && itm.Sad_itm_line == _line)
                            {
                                _rtnSerQty = _rtnSerQty + 1;

                            }

                        }

                        _fwsQty = itm.Sad_srn_qty - _rtnSerQty;
                        itm.Sad_fws_ignore_qty = _fwsQty;
                        _newList.Add(itm);

                    }

                    _InvDetailList = _newList;

                    grdInvItem.AutoGenerateColumns = false;
                    grdInvItem.DataSource = new List<InvoiceItem>();
                    grdInvItem.DataSource = _InvDetailList;
                    grdInvItem.DataBind();

                    decimal _totRetAmt = 0;
                    decimal _crAmt = 0;
                    decimal _outAmt = 0;
                    decimal _preRevAmt = 0;
                    decimal _preCrAmt = 0;
                    decimal _balCrAmt = 0;
                    decimal _newOut = 0;


                    DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(txtInvoice.Text.Trim());
                    if (_revAmt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                        {
                            _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                        }
                    }

                    DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(txtInvoice.Text.Trim());
                    if (_preCr.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                        {
                            _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                        }
                    }


                    foreach (InvoiceItem _temp in _InvDetailList)
                    {
                        _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
                    }

                    lblTotalRevAmt.Text = _preRevAmt.ToString("n");
                    lblTotRetAmt.Text = _totRetAmt.ToString("n");

                    _outAmt = Convert.ToDecimal(lblOutAmt.Text);
                    _outAmt = Convert.ToDecimal(lblTotInvAmt.Text) - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

                    _balCrAmt = Convert.ToDecimal(lblTotPayAmt.Text) - _preCrAmt;

                    if (_balCrAmt < 0)
                    {
                        _balCrAmt = 0;
                    }

                    if (_outAmt > 0)
                    {
                        _crAmt = _balCrAmt - _outAmt;
                    }
                    else
                    {
                        if (_totRetAmt <= _balCrAmt)
                        {
                            _crAmt = _totRetAmt;
                        }
                        else
                        {
                            _crAmt = _balCrAmt;
                        }
                    }






                    if (_crAmt > 0)
                    {
                        lblCrAmt.Text = _crAmt.ToString("n");
                    }
                    else
                    {
                        lblCrAmt.Text = "0";
                    }

                    DataTable _newRep = new DataTable();
                    //_newRep = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);
                    _newRep = CHNLSVC.General.SearchrequestAppAddDetByRef(_reqNo);

                    grdRereportItems.DataSource = _newRep;
                    grdRereportItems.DataBind();
                    //Load collection deatails
                    DataTable _collDet = CHNLSVC.Sales.GetInvoiceReceiptDet(txtInvoice.Text.Trim());
                    if (_collDet.Rows.Count > 0)
                    {
                        foreach (DataRow drow in _collDet.Rows)
                        {
                            if (drow["SAR_RECEIPT_TYPE"].ToString() == "DEBT" || drow["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                            {
                                //grdPaymentDetails.Rows.Add();
                                //dgvPaymentDetails["col_recSeq", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_no"].ToString();
                                //dgvPaymentDetails["col_recNo", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_anal_3"].ToString();
                                //dgvPaymentDetails["col_recDT", dgvPaymentDetails.Rows.Count - 1].Value = drow["sar_receipt_date"].ToString();
                                //dgvPaymentDetails["col_recPayTp", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_pay_tp"].ToString();
                                //dgvPaymentDetails["col_recPayRef", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_ref_no"].ToString();
                                //dgvPaymentDetails["col_recAmt", dgvPaymentDetails.Rows.Count - 1].Value = drow["sard_settle_amt"].ToString();
                            }

                        }

                    }


                }
            }

        }
        #endregion

        #region  Rooting for Rereport Item popup
        protected void btnReOk_Click(object sender, EventArgs e)
        {
            UserRereportPopoup.Hide();
        }

        protected void lbtnRereportItemsDel_Click(object sender, EventArgs e)
        {

            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
            if (_isFromReq == true)
            {
                string _Msg = "Cannot edit requested details.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }
            _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
            _repItem = ViewState["_repItem"] as List<RequestApprovalDetail>;
            if (grdRereportItems.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                if (_repAddDet == null)
                {
                    _repAddDet = new List<RequestAppAddDet>();
                    UserRereportPopoup.Show();
                    return;
                }
                if (_repItem == null)
                {
                    _repItem = new List<RequestApprovalDetail>();
                    UserRereportPopoup.Show();
                    return;
                }
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    UserRereportPopoup.Show();
                    return;
                }
                Int16 _engine = Convert.ToInt16((row.FindControl("col_RevLine") as Label).Text);
                string _item = (row.FindControl("col_OldItm") as Label).Text;

                List<RequestAppAddDet> _tmp1 = new List<RequestAppAddDet>();
                List<RequestApprovalDetail> _temp = new List<RequestApprovalDetail>();
                _temp = _repItem;
                _tmp1 = _repAddDet;

                _temp.RemoveAll(x => x.Grad_anal1 == _item && x.Grad_val2 == _engine);
                _repItem = _temp;
                _tmp1.RemoveAll(x => x.Grad_anal1 == _item && x.Grad_anal8 == _engine);
                _repAddDet = _tmp1;


                grdRereportItems.AutoGenerateColumns = false;
                grdRereportItems.DataSource = new List<RequestAppAddDet>();
                grdRereportItems.DataSource = _repAddDet;
                grdRereportItems.DataBind();
                ViewState["_repAddDet"] = _repAddDet;
                UserRereportPopoup.Show();

            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            if (txtItem.Text != "")
            {
                //kapila 10/4/2015
                MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                if (_item == null)
                {
                    string _Msg = "Invalid item code";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    //lbReerrors.Text = "Invalid item code";
                    //lbReerrors.Visible = true;
                    UserRereportPopoup.Show();
                    txtItem.Focus();
                    txtItem.Text = "";
                    return;
                }
                UserRereportPopoup.Show();
            }
        }

        protected void txtNewPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNewPrice.Text))
                {
                    if (!IsNumeric(txtNewPrice.Text))
                    {
                        string _Msg = "Price should be numeric.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        //lbReerrors.Text = "Price should be numeric.";
                        //lbReerrors.Visible = true;
                        UserRereportPopoup.Show();
                        txtNewPrice.Text = "";
                        txtNewPrice.Focus();

                        return;
                    }
                }
                UserRereportPopoup.Show();
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void txtNewSerial_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtNewSerial.Text))
                {
                    UserRereportPopoup.Show();
                    return;
                }

                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), txtNewSerial.Text.Trim(), string.Empty, string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    txtNewSerial.Text = _serialList.Tus_ser_1;
                    txtRQty.Text = "1";
                }
                else
                {
                    // lbReerrors.Text = "Invalid serial.";
                    // lbReerrors.Visible = true;
                    string _Msg = "Invalid serial.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    txtNewSerial.Text = "";
                    txtNewSerial.Focus();
                    UserRereportPopoup.Show();
                    return;
                }
                UserRereportPopoup.Show();
            }
            catch (Exception err)
            {

                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void txtRQty_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRQty.Text))
            {
                /// lbReerrors.Text = "Enter valid qty .";
                // lbReerrors.Visible = true;
                string _Msg = "Enter valid qty .";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                UserRereportPopoup.Show();
                txtRQty.Focus();
                txtRQty.Text = "";
                return;
            }

            if (!IsNumeric(txtRQty.Text))
            {
                //lbReerrors.Text = "Enter valid qty .";
                //lbReerrors.Visible = true;
                string _Msg = "Enter valid qty .";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                UserRereportPopoup.Show();
                txtRQty.Focus();
                txtRQty.Text = "";
                return;
            }

            if (Convert.ToDecimal(txtRQty.Text) <= 0)
            {
                //lbReerrors.Text = "Enter valid qty .";
                //lbReerrors.Visible = true;
                string _Msg = "Enter valid qty .";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                UserRereportPopoup.Show();
                txtRQty.Focus();
                txtRQty.Text = "";
                return;
            }

            if (!string.IsNullOrEmpty(txtNewSerial.Text))
            {
                // if (Convert.ToDecimal(txtRQty.Text) > 1)
                // {
                //lbReerrors.Text = "Enter valid qty .";
                // lbReerrors.Visible = true;
                string _Msg = "Enter serial number";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                UserRereportPopoup.Show();
                txtNewSerial.Focus();
                txtNewSerial.Text = "";
                //   return;
                // }
            }
            UserRereportPopoup.Show();
        }
        #endregion
        #region  Rooting for Service Approval popup
        protected void lbtnSearchJobNo_Click(object sender, EventArgs e)
        {
            try
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeJob);
                DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "JobNo";
                UserServiceApprovalPopoup.Show();
                UserPopoup.Show();
            }
            catch (Exception err)
            {

                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        protected void lbtnSerAppAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblSerItem.Text))
                {
                    //lblSAerror.Text = "Please select retrun item.";
                    // lblSAerror.Visible = true;
                    string _Msg = "Please select retrun item.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    UserServiceApprovalPopoup.Show();
                    lblSerItem.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    //lblSAerror.Text = "Please select related job #.";
                    //lblSAerror.Visible = true;
                    string _Msg = "Please select related job #.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);


                    UserServiceApprovalPopoup.Show();

                    txtJobNo.Text = "";
                    txtJobNo.Focus();
                    return;
                }
                _repSer = ViewState["_repSer"] as List<RequestApprovalSerials>;
                if (_repSer == null)
                {
                    _repSer = new List<RequestApprovalSerials>();
                }

                var currrange = (from cur in _repSer
                                 where cur.Gras_anal2 == lblSerItem.Text.Trim() && cur.Gras_anal3 == lblSerial.Text.Trim() && cur.Gras_anal5 == txtJobNo.Text.Trim()
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    //lblSAerror.Text = "Selected details already exsist .";
                    //lblSAerror.Visible = true;
                    string _Msg = "Selected details already exsist .";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    UserServiceApprovalPopoup.Show();
                    txtJobNo.Focus();
                    return;
                }

                RequestApprovalSerials _tmpRepSer = new RequestApprovalSerials();
                _tmpRepSer.Gras_anal2 = lblSerItem.Text.Trim();
                _tmpRepSer.Gras_anal3 = lblSerial.Text.Trim();
                _tmpRepSer.Gras_anal5 = txtJobNo.Text.Trim();

                _repSer.Add(_tmpRepSer);

                grdSerApp.AutoGenerateColumns = false;
                grdSerApp.DataSource = new List<RequestApprovalSerials>();
                grdSerApp.DataSource = _repSer;
                grdSerApp.DataBind();

                ViewState["_repSer"] = _repSer;
                lblSerItem.Text = "";
                lblSerial.Text = "";
                lblWarranty.Text = "";
                txtJobNo.Text = "";
                lblSerRem.Text = "";
                UserServiceApprovalPopoup.Show();

            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        protected void lbtnSerAppDel_Click(object sender, EventArgs e)
        {
            if (grdSerApp.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                if (txtDelJobconformmessageValue.Value == "No")
                {
                    UserServiceApprovalPopoup.Show();
                    return;
                }
                string _item = (row.FindControl("col_SerItem") as Label).Text;
                string _serial = (row.FindControl("col_SerSerial") as Label).Text;
                string _jobNo = (row.FindControl("col_JobNo") as Label).Text;

                List<RequestApprovalSerials> _temp = new List<RequestApprovalSerials>();
                _temp = _repSer;

                _temp.RemoveAll(x => x.Gras_anal2 == _item && x.Gras_anal3 == _serial && x.Gras_anal5 == _jobNo);
                _repSer = _temp;


                grdSerApp.AutoGenerateColumns = false;
                grdSerApp.DataSource = new List<RequestApprovalSerials>();
                grdSerApp.DataSource = _repSer;
                grdSerApp.DataBind();

                UserServiceApprovalPopoup.Show();
            }


        }
        #endregion

        #region  Rooting for Cash popup
        protected void lbtnTemp_Click(object sender, EventArgs e)
        {
            txtFDate.Text = Convert.ToDateTime(System.DateTime.Now).Date.AddMonths(-1).ToShortDateString();
            txtTDate.Text = Convert.ToDateTime(System.DateTime.Now).Date.ToShortDateString();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text));
            grdResultD.DataSource = _result;
            grdResultD.DataBind();
            BindUCtrlDDLData2(_result);
            lblvalue.Text = "TempDoc";
            UserDPopoup.Show();
        }

        protected void chkRevReg_CheckedChanged(object sender, EventArgs e)
        {
            _isFromReq = Convert.ToBoolean(Session["_isFromReq"].ToString());
            if (_isFromReq == true) return;
            if (chkRevReg.Checked == true)
            {
                txtRevEngine.Enabled = true;
                //btnRevRegAdd.Enabled = true;
                //btnGetRegAll.Enabled = true;
                txtRevRegItem.Enabled = true;
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";
                _regDetails = new List<VehicalRegistration>();

                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                VehicalRegistration _tempAddReg = new VehicalRegistration();
                _regDetails = new List<VehicalRegistration>();
                Int32 I = 0;

                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem item in _InvDetailList)
                    {

                        _tempReg = CHNLSVC.Sales.GetVehRegForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                        foreach (VehicalRegistration regDet in _tempReg)
                        {
                            foreach (ReptPickSerials _tmpSer in _doitemserials)
                            {
                                if (_tmpSer.Tus_itm_cd == regDet.P_srvt_itm_cd && _tmpSer.Tus_ser_1 == regDet.P_svrt_engine)
                                {
                                    if (regDet.P_svrt_prnt_stus == 2)
                                    {
                                        string _Msg = "You cannot refund reverse engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " due to it is already refunded.";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        UserCashRefundRequestsPopup.Show();
                                        goto skipAdd;
                                    }
                                    else if (regDet.P_srvt_rmv_stus != 0)
                                    {

                                        string _Msg = "You cannot refund reverse engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " due to it is already send to RMV.";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                        UserCashRefundRequestsPopup.Show();

                                        goto skipAdd;
                                    }
                                    _tempAddReg = regDet;
                                    _regDetails.Add(_tempAddReg);
                                skipAdd:
                                    I = I + 1;
                                }

                            }

                        }

                    }

                }
                else
                {

                    string _Msg = "Cannot find invoice details.First you have to select invoice details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    chkRevReg.Checked = false;
                    txtRevEngine.Enabled = false;
                    //btnRevRegAdd.Enabled = false;
                    //btnGetRegAll.Enabled = false;
                    txtRevRegItem.Enabled = false;
                    txtRevEngine.Text = "";
                    txtRevRegItem.Text = "";
                    UserCashRefundRequestsPopup.Show();
                    return;
                }
                grdRegDetails.AutoGenerateColumns = false;
                grdRegDetails.DataSource = new List<VehicalRegistration>();
                grdRegDetails.DataSource = _regDetails;
                grdRegDetails.DataBind();
                if (grdRegDetails.Rows.Count == 0)
                {
                    chkRevReg.Checked = false;
                }
            }
            else
            {
                txtRevEngine.Enabled = false;
                //btnRevRegAdd.Enabled = false;
                //btnGetRegAll.Enabled = false;
                txtRevRegItem.Enabled = false;
                txtRevEngine.Text = "";
                txtRevRegItem.Text = "";

                _regDetails = new List<VehicalRegistration>();
                grdRegDetails.AutoGenerateColumns = false;
                grdRegDetails.DataSource = new List<VehicalRegistration>();
                grdRegDetails.DataSource = _regDetails;
                grdRegDetails.DataBind();
            }
        }

        protected void txtRevRegItem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRevRegItem.Text))
            {
                InvoiceItem _tempInvItem = new InvoiceItem();
                _tempInvItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim());

                if (_tempInvItem != null)
                {
                    if (_tempInvItem.Sad_inv_no == null)
                    {
                        string _Msg = "Canot find such item in selected invoice.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtRevRegItem.Focus();
                        UserCashRefundRequestsPopup.Show();
                        return;
                    }
                }
                else
                {
                    string _Msg = "Canot find such item in selected invoice.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtRevRegItem.Focus();
                    UserCashRefundRequestsPopup.Show();
                    return;
                }
            }
        }

        protected void lbtnRevRegItem_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
            DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "RevRegItem";
            UserPopoup.Show();
        }

        protected void lbtnRevEngine_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtRevRegItem.Text))
            {
                string _Msg = "Please select invoice item.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
            DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "RevEngine";
            UserPopoup.Show();
        }

        protected void lbtnRevRegAdd_Click(object sender, EventArgs e)
        {
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempOne = new VehicalRegistration();
            Boolean _isExsist = false;

            if (string.IsNullOrEmpty(txtRevRegItem.Text))
            {
                string _Msg = "Item is missing.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                txtRevRegItem.Focus();
                UserCashRefundRequestsPopup.Show();
                return;
            }

            if (string.IsNullOrEmpty(txtRevEngine.Text))
            {
                string _Msg = "Engine # is missing.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                txtRevEngine.Focus();
                UserCashRefundRequestsPopup.Show();
                return;
            }

            foreach (VehicalRegistration _tempregList in _regDetails)
            {
                if (_tempregList.P_srvt_itm_cd == txtRevRegItem.Text.Trim() && _tempregList.P_svrt_engine == txtRevEngine.Text.Trim())
                {
                    string _Msg = "selected engine # is already exsist.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtRevEngine.Focus();
                    UserCashRefundRequestsPopup.Show();
                    return;
                }
            }

            decimal rtnQty = 0;
            decimal regQty = 0;

            if (_regDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    regQty = 0;
                    rtnQty = temp.Sad_qty;

                    foreach (VehicalRegistration tempReg in _regDetails)
                    {
                        if (temp.Sad_itm_cd == tempReg.P_srvt_itm_cd)
                        {
                            regQty = regQty + 1;
                        }
                    }

                    regQty = regQty + 1;

                    if (rtnQty < regQty)
                    {
                        string _Msg = "You are going to add registration details more than return qty for above item.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        UserCashRefundRequestsPopup.Show();
                        return;
                    }
                }
            }

            //_tempReg = CHNLSVC.Sales.GetVehRegForRev(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim(), 2);
            _tempReg = CHNLSVC.Sales.GetVehRegForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), txtRevRegItem.Text.Trim(), 2);
            _isExsist = false;

            if (_tempReg.Count > 0)
            {
                foreach (VehicalRegistration _one in _tempReg)
                {
                    if (_one.P_srvt_itm_cd == txtRevRegItem.Text.Trim() && _one.P_svrt_engine == txtRevEngine.Text.Trim())
                    {
                        _isExsist = true;
                        if (_one.P_svrt_prnt_stus == 2)
                        {
                            string _Msg = "Selected engine # is already refunded.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            _isExsist = false;
                            UserCashRefundRequestsPopup.Show();
                            return;
                        }
                        else if (_one.P_srvt_rmv_stus != 0)
                        {
                            string _Msg = "Selected engine # is already send to RMV.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            _isExsist = false;
                            UserCashRefundRequestsPopup.Show();
                            return;
                        }

                        if (_isExsist == true)
                        {
                            _tempOne = _one;
                            _regDetails.Add(_tempOne);
                        }
                    }

                }
            }
            else if (_tempReg.Count == 0)
            {
                string _Msg = "Cannot find relavant engine / chassis for above item.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                _isExsist = false;
                txtRevEngine.Text = "";
                txtRevEngine.Focus();
                UserCashRefundRequestsPopup.Show();
                return;

            }

            if (_isExsist == false)
            {
                string _Msg = "Cannot find relavant engine / chassis for above item.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                _isExsist = false;
                txtRevEngine.Text = "";
                txtRevEngine.Focus();
                UserCashRefundRequestsPopup.Show();
                return;
            }


            grdRegDetails.AutoGenerateColumns = false;
            grdRegDetails.DataSource = new List<VehicalRegistration>();
            grdRegDetails.DataSource = _regDetails;
            grdRegDetails.DataBind();
            txtRevRegItem.Text = "";
            txtRevEngine.Text = "";
            txtRevRegItem.Focus();
        }

        protected void btnGetRegAll_Click(object sender, EventArgs e)
        {
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            VehicalRegistration _tempAddReg = new VehicalRegistration();
            _regDetails = new List<VehicalRegistration>();
            Int32 I = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    //_tempReg = CHNLSVC.Sales.GetVehRegForRev(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                    _tempReg = CHNLSVC.Sales.GetVehRegForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                    foreach (VehicalRegistration regDet in _tempReg)
                    {
                        if (regDet.P_svrt_prnt_stus == 2)
                        {
                            string _Msg = "Engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already refunded.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            UserCashRefundRequestsPopup.Show();

                            goto skipAdd;
                        }
                        else if (regDet.P_srvt_rmv_stus != 0)
                        {
                            string _Msg = "Engine # : " + regDet.P_svrt_engine + " of item : " + regDet.P_srvt_itm_cd + " is already send to RMV.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            UserCashRefundRequestsPopup.Show();


                            goto skipAdd;
                        }

                        _tempAddReg = regDet;
                        _regDetails.Add(_tempAddReg);
                    skipAdd:
                        I = I + 1;
                    }

                }

            }
            else
            {
                string _Msg = "Cannot find invoice details.First you have to select invoice details.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();
                return;
            }

            grdRegDetails.AutoGenerateColumns = false;
            grdRegDetails.DataSource = new List<VehicalRegistration>();
            grdRegDetails.DataSource = _regDetails;
            grdRegDetails.DataBind();
        }

        protected void lbtnRegDetailssDel_Click(object sender, EventArgs e)
        {
            if (grdRereportItems.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                if (txtDelRegconformmessageValue.Value == "No")
                {
                    UserCashRefundRequestsPopup.Show();
                    return;
                }
                if (_regDetails == null || _regDetails.Count == 0) return;

                string _item = (row.FindControl("col_regItm") as Label).Text;
                string _engine = (row.FindControl("col_regEngine") as Label).Text;

                List<VehicalRegistration> _temp = new List<VehicalRegistration>();
                _temp = _regDetails;

                _temp.RemoveAll(x => x.P_srvt_itm_cd == _item && x.P_svrt_engine == _engine);
                _regDetails = _temp;


                grdRegDetails.AutoGenerateColumns = false;
                grdRegDetails.DataSource = new List<VehicalRegistration>();
                grdRegDetails.DataSource = _regDetails;
                grdRegDetails.DataBind();
            }





        }

        protected void chkRevIns_CheckedChanged(object sender, EventArgs e)
        {
            if (_isFromReq == true) return;
            if (chkRevIns.Checked == true)
            {
                txtRevInsEngine.Enabled = true;
                // btnRevInsAdd.Enabled = true;
                btnGetInsAll.Enabled = true;
                txtRevInsItem.Enabled = true;
                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";
                _insDetails = new List<VehicleInsuarance>();

                List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
                VehicleInsuarance _tempAddIns = new VehicleInsuarance();
                _insDetails = new List<VehicleInsuarance>();
                Int32 I = 0;

                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem item in _InvDetailList)
                    {

                        _tempIns = CHNLSVC.Sales.GetVehInsForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                        foreach (VehicleInsuarance insDet in _tempIns)
                        {
                            foreach (ReptPickSerials _tmpSer in _doitemserials)
                            {
                                if (_tmpSer.Tus_itm_cd == insDet.Svit_itm_cd && _tmpSer.Tus_ser_1 == insDet.Svit_engine)
                                {
                                    if (insDet.Svit_cvnt_issue == 2)
                                    {
                                        string _Msg = "You cannot refund reverse engine # :";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        UserCashRefundRequestsPopup.Show();
                                        goto skipAdd;
                                    }
                                    else if (insDet.Svit_polc_stus == true)
                                    {
                                        string _Msg = "You cannot refund reverse engine # :";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                        UserCashRefundRequestsPopup.Show();
                                        goto skipAdd;
                                    }
                                    _tempAddIns = insDet;
                                    _insDetails.Add(_tempAddIns);
                                skipAdd:
                                    I = I + 1;
                                }

                            }

                        }

                    }

                }
                else
                {
                    string _Msg = "Cannot find invoice details.First you have to select invoice details.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    UserCashRefundRequestsPopup.Show();
                    return;
                }


                grdInsDetails.AutoGenerateColumns = false;
                grdInsDetails.DataSource = new List<VehicleInsuarance>();
                grdInsDetails.DataSource = _insDetails;
                grdInsDetails.DataBind();
                if (grdInsDetails.Rows.Count == 0)
                {
                    chkRevIns.Checked = false;
                }
            }
            else
            {
                txtRevInsEngine.Enabled = false;
                //btnRevInsAdd.Enabled = false;
                btnGetInsAll.Enabled = false;
                txtRevInsItem.Enabled = false;
                txtRevInsEngine.Text = "";
                txtRevInsItem.Text = "";

                _insDetails = new List<VehicleInsuarance>();
                grdInsDetails.AutoGenerateColumns = false;
                grdInsDetails.DataSource = new List<VehicleInsuarance>();
                grdInsDetails.DataSource = _insDetails;
                grdInsDetails.DataBind();
            }
        }

        protected void txtRevInsItem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRevInsItem.Text))
            {
                InvoiceItem _tempInvItem = new InvoiceItem();
                _tempInvItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim());

                if (_tempInvItem != null)
                {
                    if (_tempInvItem.Sad_inv_no == null)
                    {
                        string _Msg = "Canot find such item in selected invoice.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        UserCashRefundRequestsPopup.Show();
                        txtRevInsItem.Focus();
                        return;
                    }
                }
                else
                {
                    string _Msg = "Canot find such item in selected invoice.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    UserCashRefundRequestsPopup.Show();
                    txtRevInsItem.Focus();
                    return;
                }
            }
        }

        protected void lbtnRevInsItem_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
            DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "RevInsItem";
            UserPopoup.Show();
        }

        protected void lbtnRevInsEngine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRevInsItem.Text))
            {
                string _Msg = "Please select invoice item.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();

                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuaranceDet);
            DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "RevInsEngine";
            UserPopoup.Show();
        }

        protected void lbtnRevInsAdd_Click(object sender, EventArgs e)
        {
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempOne = new VehicleInsuarance();
            Boolean _isExsist = false;

            if (string.IsNullOrEmpty(txtRevInsItem.Text))
            {
                string _Msg = "Item is missing.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();
                txtRevInsItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRevInsEngine.Text))
            {
                string _Msg = "Engine # is missing.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();
                txtRevInsEngine.Focus();
                return;
            }

            foreach (VehicleInsuarance _tempinsList in _insDetails)
            {
                if (_tempinsList.Svit_itm_cd == txtRevInsItem.Text.Trim() && _tempinsList.Svit_engine == txtRevInsEngine.Text.Trim())
                {
                    string _Msg = "selected engine # is already exsist.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    UserCashRefundRequestsPopup.Show();
                    txtRevInsEngine.Focus();
                    return;
                }
            }

            decimal rtnQty = 0;
            decimal insQty = 0;

            if (_insDetails.Count > 0)
            {
                foreach (InvoiceItem temp in _InvDetailList)
                {
                    rtnQty = 0;
                    insQty = 0;
                    rtnQty = temp.Sad_qty;

                    foreach (VehicleInsuarance tempins in _insDetails)
                    {
                        if (temp.Sad_itm_cd == tempins.Svit_itm_cd)
                        {
                            insQty = insQty + 1;
                        }
                    }

                    insQty = insQty + 1;

                    if (rtnQty < insQty)
                    {
                        string _Msg = "You are going to add registration details more than return qty for above item.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        UserCashRefundRequestsPopup.Show();
                        return;
                    }
                }
            }

            //_tempIns = CHNLSVC.Sales.GetVehInsForRev(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim(), 2);
            _tempIns = CHNLSVC.Sales.GetVehInsForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), txtRevInsItem.Text.Trim(), 2);
            _isExsist = false;

            foreach (VehicleInsuarance _one in _tempIns)
            {
                if (_one.Svit_itm_cd == txtRevInsItem.Text.Trim() && _one.Svit_engine == txtRevInsEngine.Text.Trim())
                {
                    _isExsist = true;
                    if (_one.Svit_cvnt_issue == 2)
                    {
                        string _Msg = "Selected engine # is already refunded.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        UserCashRefundRequestsPopup.Show();
                        _isExsist = false;
                        return;
                    }
                    else if (_one.Svit_polc_stus == true)
                    {
                        string _Msg = "Selected engine # already issue policy.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        UserCashRefundRequestsPopup.Show();
                        _isExsist = false;
                        return;
                    }

                    if (_isExsist == true)
                    {
                        _tempOne = _one;
                        _insDetails.Add(_tempOne);
                    }
                }

            }

            if (_isExsist == false)
            {
                string _Msg = "Invalid engine # selected.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();
                txtRevInsEngine.Text = "";
                txtRevInsEngine.Focus();
                return;
            }


            grdInsDetails.AutoGenerateColumns = false;
            grdInsDetails.DataSource = new List<VehicleInsuarance>();
            grdInsDetails.DataSource = _insDetails;
            grdInsDetails.DataBind();
            txtRevInsItem.Text = "";
            txtRevInsEngine.Text = "";
            txtRevInsItem.Focus();
        }

        protected void btnGetInsAll_Click(object sender, EventArgs e)
        {
            List<VehicleInsuarance> _tempIns = new List<VehicleInsuarance>();
            VehicleInsuarance _tempAddIns = new VehicleInsuarance();
            _insDetails = new List<VehicleInsuarance>();
            Int32 I = 0;

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    //_tempIns = CHNLSVC.Sales.GetVehInsForRev(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);
                    _tempIns = CHNLSVC.Sales.GetVehInsForRev(Session["UserCompanyCode"].ToString(), lblSalePc.Text.Trim(), txtInvoice.Text.Trim(), item.Sad_itm_cd, 2);

                    foreach (VehicleInsuarance insDet in _tempIns)
                    {
                        if (insDet.Svit_cvnt_issue == 2)
                        {
                            string _Msg = "Engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already refunded.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            UserCashRefundRequestsPopup.Show();

                            goto skipAdd;
                        }
                        else if (insDet.Svit_polc_stus == true)
                        {
                            string _Msg = "Engine # : " + insDet.Svit_engine + " of item : " + insDet.Svit_itm_cd + " is already issue cover note.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            UserCashRefundRequestsPopup.Show();
                            goto skipAdd;
                        }

                        _tempAddIns = insDet;
                        _insDetails.Add(_tempAddIns);
                    skipAdd:
                        I = I + 1;
                    }

                }

            }
            else
            {
                string _Msg = "Cannot find invoice details.First you have to select invoice details.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                UserCashRefundRequestsPopup.Show();

                return;
            }

            grdInsDetails.AutoGenerateColumns = false;
            grdInsDetails.DataSource = new List<VehicleInsuarance>();
            grdInsDetails.DataSource = _insDetails;
            grdInsDetails.DataBind();

        }

        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            UserPopoup.Hide();
            UserRereportPopoup.Hide();
            UserServiceApprovalPopoup.Hide();
            UserCashRefundRequestsPopup.Hide();
            userCancelPopoup.Hide();
            //if (Session["popup"].ToString() == "Rereport")
            //{
            //    UserRereportPopoup.Show();
            //}
            //else if (Session["popup"].ToString() == "ServiceApproval")
            //{
            //    UserServiceApprovalPopoup.Show();
            //}
            //else if (Session["popup"].ToString() == "CashRefundRequest")
            //{
            //    UserCashRefundRequestsPopup.Show();
            //}
            //else if (Session["popup"].ToString() == "Cancel")
            //{
            //    userCancelPopoup.Show();
            //}


        }

        protected void chkIsMan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsMan.Checked == true)
            {
                txtManRef.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_SRN");
                if (_NextNo != 0)
                {
                    txtManRef.Text = _NextNo.ToString();
                }
                else
                {
                    string _Msg = "Cannot find valid manual document.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtManRef.Text = "";
                }
            }
            else
            {
                txtManRef.Text = string.Empty;

            }
        }

        protected void lbtnSearchActLoc_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "SearchActLoc";
            UserPopoup.Show();
        }

        protected void chkIsMan_CheckedChanged1(object sender, EventArgs e)
        {
            if (chkIsMan.Checked == true)
            {
                txtManRef.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_SRN");
                if (_NextNo != 0)
                {
                    txtManRef.Text = _NextNo.ToString();
                }
                else
                {
                    string _Msg = "Cannot find valid manual document";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    txtManRef.Text = "";
                }
            }
            else
            {
                txtManRef.Text = string.Empty;

            }
        }
        protected void col_invRevQty_TextChanged(object sender, EventArgs e)
        {
            if (grdInvItem.Rows.Count == 0) return;
            var lb = (TextBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                decimal _qty = Convert.ToDecimal((row.FindControl("col_invQty") as Label).Text);
                decimal _Rqty = Convert.ToDecimal((row.FindControl("col_invRevQty") as TextBox).Text);
                string _Itm = (row.FindControl("col_invItem") as Label).Text;
                string _line = (row.FindControl("col_invLine") as Label).Text;

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16056))
                {
                    //lbtncancel.Enabled = true;
                    string msg = "You dont have permission to edit qty .Permission code : 16056";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }

                if (_Rqty > _qty)
                {
                    _Rqty = 0;
                    string _Msg = "Cannot exceed invoice qty";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                }
                else
                {
                    _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
                    if (_doitemserials.Count > 0)
                    {
                        decimal _Rqty2 = Convert.ToDecimal((row.FindControl("col_invRevQty") as TextBox).Text);
                        int itmcount = _doitemserials.Where(x => x.Tus_itm_cd == _Itm && x.Tus_itm_line == Convert.ToDecimal(_line)).Count();
                        if (itmcount > 0)
                        {
                            List<ReptPickSerials> _reversItm = new List<ReptPickSerials>();
                            _reversItm = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                            if (_reversItm != null)
                            {
                                int itmrevcount = _reversItm.Where(x => x.Tus_itm_cd == _Itm).Count();
                                if (itmrevcount != _Rqty2)
                                {
                                    _Rqty = itmrevcount;
                                    string _Msg = "Cannot change qty";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                }
                            }
                            else
                            {
                                decimal count = _doitemserials.Where(x => x.Tus_itm_cd == _Itm).Count();

                                decimal focqty = _qty - count;
                                if (focqty < _Rqty)
                                {
                                    _Rqty = 0;
                                    string _Msg = "Cannot exceed qty";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                }
                            }

                        }
                    }
                    _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                    if (_InvDetailList != null)
                    {
                        foreach (InvoiceItem _item in _InvDetailList)
                        {
                            if ((_item.Sad_itm_cd == _Itm) && (_item.Sad_itm_line == Convert.ToDecimal(_line)))
                            {
                                _item.Sad_srn_qty = _Rqty;
                            }

                        }
                        for (int i = 0; i < grdInvItem.Rows.Count; i++)
                        {
                            TextBox tb = ((TextBox)grdInvItem.Rows[i].FindControl("col_invRevQty"));
                            tb.Enabled = true;
                        }
                    }
                    _InvDetailList = _InvDetailList.OrderBy(c => c.Sad_itm_cd).ToList();
                    grdInvItem.AutoGenerateColumns = false;
                    grdInvItem.DataSource = new List<InvoiceItem>();
                    grdInvItem.DataSource = _InvDetailList;//_InvDetailList;
                    grdInvItem.DataBind();
                    ViewState["_InvDetailList"] = _InvDetailList;
                }

            }
        }
        protected void lbtntab_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text != "APPROVED")
            {
                string _Msg = "Sales reversal request is still not approved";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //if (tbReg.SelectedTab == tbReg.TabPages[2])
                //{
                //    MessageBox.Show("Sales reversal request is still not approved.", "Cash Sales Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    tbReg.SelectTab(0);
                //    return;
                //}
            }
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            if (Session["popup"].ToString() == "Rereport")
            {
                UserRereportPopoup.Show();
            }
            else if (Session["popup"].ToString() == "ServiceApproval")
            {
                UserServiceApprovalPopoup.Show();
            }
            else if (Session["popup"].ToString() == "CashRefundRequest")
            {
                UserCashRefundRequestsPopup.Show();
            }
            else if (Session["popup"].ToString() == "Cancel")
            {
                userCancelPopoup.Show();
            }
            Session["DPopup"] = "";
        }

        protected void chkOthSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthSales.Checked == true)
            {
                lbtnCustomer.Visible = false;
                txtCusCode.Enabled = false;
            }
            else
            {
                lbtnCustomer.Visible = true;
                txtCusCode.Enabled = true;
            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {

                    if (txtInvoiceNo.Text.ToString() != "")
                    {
                        Session["documntNo"] = txtInvoiceNo.Text.ToString();
                        _print();
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
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

        protected void lbtnprintserial_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["GlbReportName"] = "serial_items.rpt";
                Session["GlbReportName"] = "serial_items.rpt";
                InventoryHeader _otherdoc = new InventoryHeader();
                _otherdoc = CHNLSVC.Inventory.GetINTHDRByOthDoc(Session["UserCompanyCode"].ToString(), "SRN", txtInvoiceNo.Text.ToString());

                if (_otherdoc != null)
                {
                    Session["documntNo"] = _otherdoc.Ith_doc_no;
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDFSer(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    PopupConfBox.Hide();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('No SRN Found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                CHNLSVC.MsgPortal.SaveReportErrorLog("Sales Reversal Serial Print", "Sales Reversal", ex.Message, Session["UserID"].ToString());
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

        protected void lbtnreversalno_Click(object sender, EventArgs e)
        {
            try
            {

            }

            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }

        protected void chkFin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                if (chkFin.Checked)
                {
                    _TempReqAppHdr = CHNLSVC.Sales.getcompleded_ReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", null, txtReqLoc.Text);
                    chkApp.Checked = false;
                    grdPendings.Columns[1].HeaderText = "Rev. No";
                }
                else
                {
                    grdPendings.Columns[1].HeaderText = "Req. No";
                }

                grdPendings.AutoGenerateColumns = false;
                grdPendings.DataSource = new List<RequestApprovalHeader>();

                if (_TempReqAppHdr == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No any request / approval found.');", true);
                    // MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                grdPendings.DataSource = _TempReqAppHdr;
                grdPendings.DataBind();

            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }

        protected void lbtnSerVar_Click(object sender, EventArgs e)
        {
            lblSerVarError.Text = "";
            lblSerVarError.Visible = false;
            txtMainItmSer1.Text = "";
            txtSubSerial.Text = "";
            dgvSubSerPick.DataSource = new int[] { };
            dgvSubSerPick.DataBind();

            dgvPopSerial.DataSource = new int[] { };
            bool isDataAva = false;
            if (Session["gvSerData"] != null)
            {
                DataTable _dtSerData = Session["gvSerData"] as DataTable;
                if (_dtSerData == null)
                {
                    List<ReptPickSerials> _serList = Session["gvSerData"] as List<ReptPickSerials>;
                    if (_serList != null)
                    {
                        dgvPopSerial.DataSource = _serList;
                        isDataAva = true;
                    }
                }
                else
                {
                    dgvPopSerial.DataSource = _dtSerData;
                    isDataAva = true;
                }
            }
            if (!isDataAva)
            {
                DisplayMessage("No serial found please pick the serials !!!", 1); return;
            }
            dgvPopSerial.DataBind();
            LoadGridStatus();
            txtMainItmSer1.Focus();
            Session["popSerVar"] = "Show";
            popSerVar.Show();
        }
        private void LoadGridStatus()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow item in dgvPopSerial.Rows)
            {
                Label lblStsDesc = item.FindControl("lblStsDesc") as Label;
                Label lblSts = item.FindControl("col_itmstatus") as Label;
                if (status_list != null)
                {
                    lblStsDesc.Text = status_list.Where(c => c.Key == lblSts.Text).FirstOrDefault().Value;
                }
            }
        }
        protected void lbtnSerVarClose_Click(object sender, EventArgs e)
        {
            popSerVar.Hide();
            Session["popSerVar"] = "Hide";
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
                    Ins_available = 1
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
                else if (_intSerList.Count == 0)
                {
                    lblSerVarError.Text = "Main item serial # is invalid !!!";
                    lblSerVarError.Visible = true;
                    txtMainItmSer1.Text = "";
                    txtMainItmSer1.Focus();
                    popSerVar.Show();
                    return;
                }
                else if (_intSerList.Count == 1)
                {
                    _invSubSerList = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = _intSerList[0].Ins_ser_id, Irsms_act = true });
                    if (_invSubSerList != null)
                    {
                        if (_invSubSerList.Count > 0)
                        {
                            dgvSubSerPick.DataSource = _invSubSerList;
                            dgvSubSerPick.DataBind();

                            txtSubSerial.Text = "";
                            txtSubSerial.Focus();
                        }
                    }
                }
                else //if (_intSerList.Count > 1)
                {
                    lblSerVarError.Text = "";
                    lblSerVarError.Visible = false;
                    InventorySerialN _invSerIn = new InventorySerialN();
                    bool haveDo = false;
                    foreach (GridViewRow row in dgvPopSerial.Rows)
                    {
                        Label col_doc = row.FindControl("col_doc") as Label;
                        foreach (var item in _intSerList)
                        {
                            if (col_doc.Text == item.Ins_doc_no)
                            {
                                _invSerIn = item;
                                haveDo = true;
                                break;
                            }
                        }
                        if (haveDo)
                        {
                            break;
                        }
                    }
                    if (!haveDo)
                    {
                        lblSerVarError.Text = "Do # not found !!!";
                        lblSerVarError.Visible = false;
                    }
                    else
                    {
                        bool serHave = false;
                        _invSubSerList = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = _invSerIn.Ins_ser_id, Irsms_act = true });
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
                            //else
                            //{
                            //    lblSerVarError.Text = "No sub serials found !!!";
                            //    lblSerVarError.Visible = true;
                            //}
                        }
                        if (!serHave)
                        {
                            CompleteSubSerPick();
                            // DisplayMessage("No sub serial found !!!"); return;
                        }
                    }
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
                Label col_Serial = dr.FindControl("col_Serial") as Label;
                CheckBox chkPickSer = dr.FindControl("chkPickSer") as CheckBox;
                if (col_Serial.Text == txtMainItmSer1.Text.Trim())
                {
                    chkPickSer.Checked = true;
                    dr.BackColor = Color.FromName("#E54E24");
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
        }

        public void _print()
        {
            try
            {
                Session["GlbReportType"] = "SCM1_SRN";
                //if (Session["UserCompanyCode"].ToString() == "AAL")
                //{
                //    Session["GlbReportName"] = "SRN_AAL.rpt";
                //}
                if (Session["UserCompanyCode"].ToString() == "ABE")
                {
                    Session["GlbReportName"] = "SRN_ABE.rpt";
                }
                else if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    Session["GlbReportName"] = "SRN_AAL.rpt";
                }
                else
                {
                    Session["GlbReportName"] = "SRN.rpt";
                }
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Sales Reversal Print", "Sales Reversal", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["GlbReportType"] = "SCM1_SRN";
            Session["GlbReportName"] = "SRN.rpt";
            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            PrintPDF(targetFileName);
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
            //PrintPDF(targetFileName);
            //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


            // Session["GlbReportType"] = "SCM1_SRN";
            // Session["DocNo"] = Session["documntNo"].ToString();
            // BaseCls.GlbReportHeading = "SRN Print";
            // Session["GlbReportName"] = "SRN.rpt";
            //// Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);

            // string url = "<script>window.open('/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        }

        public void PrintPDF(string targetFileName)
        {
            try
            {
                clsSales obj = new clsSales();
                obj.GetSRNdata(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["documntNo"].ToString(), Session["UserDefLoca"].ToString());
                ReportDocument rptDoc = Session["UserCompanyCode"].ToString() == "AAL" ? (ReportDocument)obj._SRNreport_AAL : Session["UserCompanyCode"].ToString() == "ABE" ? (ReportDocument)obj._SRNreport_ABE : (ReportDocument)obj._SRNreport;
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
                    //Dulaj 2018/Jul/30
                    List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                    if (chkApp.Checked == false)
                    {
                        _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", null, txtReqLoc.Text);
                    }
                    else
                    {
                        _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "ARQT014", Session["UserID"].ToString(), txtReqLoc.Text);
                    }
                    if (!String.IsNullOrEmpty(txtdocname.Text))
                    {
                        if (_TempReqAppHdr != null)
                        {
                            var templist = _TempReqAppHdr.Where(x => x.Grah_fuc_cd.Equals(txtdocname.Text.Trim()));
                            templist = templist.Where(x=>x.Grah_app_stus!="C");                            
                            foreach (var tem in templist)
                            {
                                if (!string.IsNullOrEmpty(tem.Grah_ref))
                                {
                                    ModalPopupExtenderOutstanding.Show();
                                    return;
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    //
                    SaveData(false);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }

            }
        }

        protected void btnConOutfYes_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
            SaveData(true);
        }
        protected void btnConfClose_ClickExcel(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
        }
        protected void btnConfNoOut_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
        }

        private void SaveData(bool reqbase)
        {
            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            MasterLocation _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtpdaloc.Text);
            if (_MasterLocation == null)
            {
                txtpdaloc.Text = String.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid location')", true);
                ddlloadingbay.Focus();
                MPPDA.Show();
                return;
            }
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

            Int32 user_seq_num = 0;
            if (chkmultiuser.Checked == false)
            {
                DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), txtdocname.Text);
                Int64 _seqno = 0;
                if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
                {
                    string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                    string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));
                    _seqno = _headerchk2.Rows[0].Field<Int64>("TUH_USRSEQ_NO");
                    if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                        {
                            string msg2 = "Document " + txtdocname.Text + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                            return;
                        }
                }
                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE_LOC("SRN", Session["UserCompanyCode"].ToString(), txtdocname.Text, 1, txtpdaloc.Text);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo("SRN", txtdocname.Text);
                    if (user_seq_num > 0)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = "SRN";
                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                        _inputReptPickHeader.Tuh_usr_loc = txtpdaloc.Text;//Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                        val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                        if (val == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

                            return;
                        }
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
            }
            else
            {
                user_seq_num = GenerateNewUserSeqNo("SRN", txtdocname.Text);
                if (user_seq_num > 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "SRN";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = txtpdaloc.Text;//Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (val == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

                        return;
                    }
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
                // _doitemserials = Session["gvSerData"] as List<ReptPickSerials>;
                List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
                _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                if (_InvDetailList.Count > 0)
                {
                    foreach (InvoiceItem _itm in _InvDetailList)
                    {
                        ReptPickItems _reptitm = new ReptPickItems();
                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                        if (reqbase)
                        {
                            _reptitm.Tui_req_itm_qty = _itm.Sad_srn_qty;//Edited By Dulaj 2018/Oct/11
                        }
                        else
                        {
                            _reptitm.Tui_req_itm_qty = _itm.Sad_qty;//Edited By Dulaj 2018/Oct/11
                        }
                        _reptitm.Tui_req_itm_cd = _itm.Sad_itm_cd;
                        _reptitm.Tui_req_itm_stus = _itm.Sad_itm_stus;
                        _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Sad_itm_line);//Darshana request add by rukshan
                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                        _reptitm.Tui_pic_itm_qty = 0;
                        _saveonly.Add(_reptitm);


                    }
                }
                val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
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

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpda.Checked == true)
            {
                List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
                _doitemserials = Session["gvSerData"] as List<ReptPickSerials>;
                txtpdaloc.Text = Session["UserDefLoca"].ToString();
                //if (_doitemserials.Count == 0)
                //{
                //    string _Msg = "Only DO Items can be sent to PDA";
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                //    return;
                //}
                txtdocname.Text = txtInvoice.Text;
                MPPDA.Show();
            }
            else
            {
                MPPDA.Hide();
            }
        }
        private void getPDASerial(int _seq = 0)
        {
            List<ReptPickSerials> _serial = new List<ReptPickSerials>();
            _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
            _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
            _serial = CHNLSVC.Sales.Get_TEMP_PICK_SER_BY_loc(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), Session["UserDefLoca"].ToString());
            if (_serial != null)
            {
                var _scanInvoiceItems = _serial.GroupBy(x => new { x.Tus_usrseq_no, x.Tus_base_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                List<InvoiceList> _invoi = new List<InvoiceList>();
                int _grnline = 1;
                foreach (var itm in _scanInvoiceItems)
                {
                    InvoiceList _invObj = new InvoiceList();
                    _invObj.Invoiceno = itm.Peo.Tus_base_doc_no;
                    _invObj.GRNA = "GRNA_" + _grnline;
                    _invObj.SEQ = itm.Peo.Tus_usrseq_no;
                    _grnline++;
                    _invoi.Add(_invObj);
                }
                if (_invoi.Count > 1)
                {
                    if (_seq == 0)
                    {
                        grdinvoice.DataSource = _invoi;
                        grdinvoice.DataBind();
                        MPMULTIINVOICE.Show();
                        return;
                    }
                    else
                    {
                        _serial = _serial.Where(x => x.Tus_usrseq_no == _seq).ToList();
                    }

                }
            }
            if (_serial != null)
            {
                List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                foreach (ReptPickSerials _itm in _serial)
                {
                    _itm.Tus_appstatus = _itm.Tus_new_status;
                    if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
                    {

                        MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == _itm.Tus_itm_stus);
                        if (oStatus != null)
                        {
                            _itm.Tus_itm_stus_Desc = oStatus.Mis_desc;

                        }
                        MasterItemStatus oStatus1 = oMasterItemStatuss.Find(x => x.Mis_cd == _itm.Tus_appstatus);
                        if (oStatus != null)
                        {
                            _itm.Tus_new_status_Desc = oStatus.Mis_desc;

                        }

                    }
                }

                ///
                foreach (ReptPickSerials _itm in _serial)
                {
                    MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Tus_itm_cd);
                    if (_itemdetail != null)
                    {
                        if (_itemdetail.Mi_is_ser1 == 0)
                        {

                        }
                        else if (_itemdetail.Mi_is_ser1 == 1)
                        {
                            var _filter = _doitemserials.Find(x => x.Tus_ser_1 == _itm.Tus_ser_1);
                            if (_filter != null)
                            {
                                var _replace = _serial.Find(x => x.Tus_itm_line == _itm.Tus_itm_line);
                                _itm.Tus_base_itm_line = _filter.Tus_base_itm_line;
                                _itm.Tus_doc_no = _filter.Tus_doc_no;
                                _itm.Tus_batch_line = _filter.Tus_batch_line;
                                _itm.Tus_bin = _filter.Tus_bin;
                                _itm.Tus_com = _filter.Tus_com;
                                _itm.Tus_cre_by = _filter.Tus_cre_by;
                                _itm.Tus_cre_dt = _filter.Tus_cre_dt;
                                _itm.Tus_cross_batchline = _filter.Tus_cross_batchline;
                                _itm.Tus_cross_itemline = _filter.Tus_cross_itemline;
                                _itm.Tus_cross_seqno = _filter.Tus_cross_seqno;
                                _itm.Tus_cross_serline = _filter.Tus_cross_serline;
                                _itm.Tus_doc_dt = _filter.Tus_doc_dt;
                                _itm.Tus_doc_no = _filter.Tus_doc_no;
                                _itm.Tus_exist_grncom = _filter.Tus_exist_grncom;
                                _itm.Tus_exist_grndt = _filter.Tus_exist_grndt;
                                _itm.Tus_exist_grnno = _filter.Tus_exist_grnno;
                                _itm.Tus_exist_supp = _filter.Tus_exist_supp;
                                _itm.Tus_itm_brand = _filter.Tus_itm_brand;
                                _itm.Tus_itm_cd = _filter.Tus_itm_cd;
                                _itm.Tus_itm_desc = _filter.Tus_itm_desc;
                                _itm.Tus_itm_line = _filter.Tus_itm_line;
                                _itm.Tus_itm_model = _filter.Tus_itm_model;
                                _itm.Tus_itm_stus = _filter.Tus_itm_stus;
                                _itm.Tus_loc = _filter.Tus_loc;
                                _itm.Tus_orig_grncom = _filter.Tus_orig_grncom;
                                _itm.Tus_orig_grndt = _filter.Tus_orig_grndt;
                                _itm.Tus_orig_grnno = _filter.Tus_orig_grnno;
                                _itm.Tus_orig_supp = _filter.Tus_orig_supp;
                                _itm.Tus_out_date = _filter.Tus_out_date;
                                _itm.Tus_qty = _filter.Tus_qty;
                                _itm.Tus_seq_no = _filter.Tus_seq_no;
                                _itm.Tus_serial_id = _filter.Tus_serial_id;
                                _itm.Tus_ser_1 = _filter.Tus_ser_1;
                                _itm.Tus_ser_2 = _filter.Tus_ser_2;
                                _itm.Tus_ser_3 = _filter.Tus_ser_3;
                                _itm.Tus_ser_4 = _filter.Tus_ser_4;
                                _itm.Tus_ser_id = _filter.Tus_ser_id;
                                _itm.Tus_ser_line = _filter.Tus_ser_line;
                                _itm.Tus_session_id = _filter.Tus_session_id;
                                _itm.Tus_unit_cost = _filter.Tus_unit_cost;
                                _itm.Tus_unit_price = _filter.Tus_unit_price;
                                _itm.Tus_usrseq_no = _filter.Tus_usrseq_no;
                                _itm.Tus_warr_no = _filter.Tus_warr_no;
                                _itm.Tus_warr_period = _filter.Tus_warr_period;
                                _itm.Tus_new_status = _filter.Tus_new_status;
                                _itm.Tus_new_remarks = _filter.Tus_new_remarks;
                                _itm.Tus_isapp = _filter.Tus_isapp;
                                _itm.Tus_iscovernote = _filter.Tus_iscovernote;
                                _itm.Tus_base_itm_line = _filter.Tus_base_itm_line;
                                _itm.Tus_base_doc_no = _filter.Tus_base_doc_no;
                                _itm.ItemType = _filter.ItemType;
                                _itm.Tus_resqty = _filter.Tus_resqty;
                                _itm.Tus_ageloc = _filter.Tus_ageloc;
                                _itm.Tus_ageloc_dt = _filter.Tus_ageloc_dt;
                                _itm.Tus_isownmrn = _filter.Tus_isownmrn;
                                _itm.Tus_job_no = _filter.Tus_job_no;
                                _itm.Tus_job_line = _filter.Tus_job_line;
                                _itm.Tus_res_no = _filter.Tus_res_no;
                                _itm.Tus_res_line = _filter.Tus_res_line;
                                _itm.Tus_batch_no = _filter.Tus_batch_no;
                                _itm.Tus_exp_dt = _filter.Tus_exp_dt;
                                _itm.Tus_manufac_dt = _filter.Tus_manufac_dt;
                                _itm.Tus_is_pgs = _filter.Tus_is_pgs;
                                _itm.Tus_pgs_count = _filter.Tus_pgs_count;
                                _itm.Tus_pgs_prefix = _filter.Tus_pgs_prefix;
                                _itm.Tus_st_pg = _filter.Tus_st_pg;
                                _itm.Tus_ed_pg = _filter.Tus_ed_pg;
                                _itm.Tus_new_itm_cd = _filter.Tus_new_itm_cd;
                                _itm.Tus_appstatus = _filter.Tus_appstatus;
                                _itm.Mis_desc = _filter.Mis_desc;
                                _itm.Tus_isqty = _filter.Tus_isqty;
                                _itm.Tus_isSelect = _filter.Tus_isSelect;
                                _itm.Tus_bin_to = _filter.Tus_bin_to;
                                _itm.Tus_itm_stus_Desc = _filter.Tus_itm_stus_Desc;
                                _itm.Tus_new_status_Desc = _filter.Tus_new_status_Desc;
                                _itm.Tus_temp_itm_line = _filter.Tus_temp_itm_line;
                                _itm.SerialAvailable = _filter.SerialAvailable;
                                _itm.Tus_itm_model_desc = _filter.Tus_itm_model_desc;
                                _itm.MainItemSerialNo = _filter.MainItemSerialNo;
                                _itm.Tus_ser_remarks = _filter.Tus_ser_remarks;
                                _itm.IRSM_SYS_BLNO = _filter.IRSM_SYS_BLNO;
                                _itm.IRSM_BLNO = _filter.IRSM_BLNO;
                                _itm.IRSM_BL_DT = _filter.IRSM_BL_DT;
                                _itm.IRSM_SYS_FIN_NO = _filter.IRSM_SYS_FIN_NO;
                                _itm.IRSM_FIN_NO = _filter.IRSM_FIN_NO;
                                _itm.IRSM_FIN_DT = _filter.IRSM_FIN_DT;
                                _itm.Tus_base_doc_no_1 = _filter.Tus_base_doc_no_1;
                                _itm.Tus_ins_pick = _filter.Tus_ins_pick;
                                _itm.TmpSerPick = _filter.TmpSerPick;
                                _itm.Tus_Warranty_Remark = _filter.Tus_Warranty_Remark;
                                _itm.pickserial = _filter.pickserial;
                                _itm.Tus_temp_seq = _filter.Tus_temp_seq;
                                _itm.Tus_tmp_qty_to_pick = _filter.Tus_tmp_qty_to_pick;
                                //_itm.Tus_cross_docno = _filter.Tus_cross_docno;
                                //_itm.Tus_fifo_doc = _filter.Tus_fifo_doc;
                                _itm.Tus_ageloc = _filter.Tus_ageloc;
                                //_itm.Tus_issue_dt = _filter.Tus_issue_dt;
                                //_itm.Tus_fifo_dt = _filter.Tus_fifo_dt;
                            }
                        }
                    }
                }
                int maxline = _serial.Max(x => x.Tus_itm_line);
                var _scanNonItems = _serial.GroupBy(x => new { x.Tus_itm_cd, x.Tus_qty, x.Tus_itm_line, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanNonItems)
                {
                    MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                    if (msitem != null)
                    {
                        if (msitem.Mi_is_ser1 == 0)
                        {
                            List<ReptPickSerials> _SERIID = new List<ReptPickSerials>();
                            _SERIID = CHNLSVC.Inventory.GetInvoiceSerialForReversalBYITM(Session["UserCompanyCode"].ToString(), txtInvoice.Text, itm.Peo.Tus_base_itm_line, itm.Peo.Tus_itm_cd, 0);
                            ReptPickSerials serial = _serial.Find(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd && x.Tus_itm_line == itm.Peo.Tus_itm_line
                                && x.Tus_base_itm_line == itm.Peo.Tus_base_itm_line);
                            for (int i = 1; i < itm.Peo.Tus_qty; i++)
                            {

                                ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                                serial.Tus_qty = 1;
                                ReptPickSerials _tmpSer = ReptPickSerials.CreateNewObject(serial);
                                _serial.Add(_tmpSer);
                                //#region Fill Pick Serial Object
                                //_newReptPickSerials.Tus_usrseq_no = serial.Tus_usrseq_no;
                                //_newReptPickSerials.Tus_doc_no = serial.Tus_doc_no;
                                //_newReptPickSerials.Tus_seq_no = serial.Tus_seq_no;
                                //_newReptPickSerials.Tus_itm_line = serial.Tus_itm_line;
                                //_newReptPickSerials.Tus_batch_line = 0;
                                //_newReptPickSerials.Tus_ser_line = 0;
                                //_newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                                //_newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                                //_newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                                //_newReptPickSerials.Tus_bin = serial.Tus_bin;
                                //_newReptPickSerials.Tus_itm_cd = serial.Tus_itm_cd;
                                //_newReptPickSerials.Tus_itm_stus = serial.Tus_itm_stus;
                                //_newReptPickSerials.Tus_unit_cost = serial.Tus_unit_cost;
                                //_newReptPickSerials.Tus_unit_price = serial.Tus_unit_price;
                                //_newReptPickSerials.Tus_qty = 1;

                                //_newReptPickSerials.Tus_ser_id = 0;//CHNLSVC.Inventory.GetSerialID();

                                //_newReptPickSerials.Tus_ser_1 = "N/A";
                                //_newReptPickSerials.Tus_ser_2 = "N/A";
                                //_newReptPickSerials.Tus_ser_3 = "N/A";
                                //_newReptPickSerials.Tus_warr_no = serial.Tus_warr_no;
                                //_newReptPickSerials.Tus_itm_desc = serial.Tus_itm_desc;
                                //_newReptPickSerials.Tus_itm_model = serial.Tus_itm_model;
                                //_newReptPickSerials.Tus_itm_brand = serial.Tus_itm_brand;
                                //_newReptPickSerials.Tus_itm_line = maxline + i;
                                //_newReptPickSerials.Tus_base_itm_line = serial.Tus_base_itm_line;
                                //_newReptPickSerials.Tus_base_doc_no = serial.Tus_base_doc_no;
                                //_newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                                //_newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                                //_newReptPickSerials.Tus_session_id = serial.Tus_session_id;
                                //_newReptPickSerials.Tus_new_itm_cd = serial.Tus_new_itm_cd;
                                //_serial.Add(_newReptPickSerials);
                                //#endregion
                            }

                            var _finllist = _serial.Where(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd
                               && x.Tus_base_itm_line == itm.Peo.Tus_base_itm_line).ToList();
                            foreach (var item in _finllist)
                            {
                                if (_SERIID != null)
                                {
                                    if (_SERIID.Count > 0)
                                    {
                                        var _idlist = _SERIID.Find(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd && x.Tus_isSelect == false);
                                        if (_idlist != null)
                                        {
                                            item.Tus_qty = 1;
                                            item.Tus_ser_id = _idlist.Tus_ser_id;
                                            item.Tus_doc_no = _idlist.Tus_doc_no;
                                            foreach (var item2 in _SERIID)
                                            {
                                                if (item2.Tus_ser_id == _idlist.Tus_ser_id)
                                                {
                                                    item2.Tus_isSelect = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }



                        }
                    }
                }


                var _scanItems = _serial.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                foreach (var itm in _scanItems)
                {

                    foreach (InvoiceItem _invItem in _InvDetailList)
                        if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                        {
                            //it.Sad_do_qty = q.theCount;
                            //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                            _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                        }
                }



                grdInvItem.AutoGenerateColumns = false;
                grdInvItem.DataSource = new List<InvoiceItem>();
                grdInvItem.DataSource = _InvDetailList;//_InvDetailList;
                grdInvItem.DataBind();
                ViewState["_InvDetailList"] = _InvDetailList;

                grdSelectReversal.AutoGenerateColumns = false;
                grdSelectReversal.DataSource = new List<ReptPickSerials>();
                grdSelectReversal.DataSource = _serial;
                grdSelectReversal.DataBind();
                ViewState["RevsFilterSerial"] = _serial;

                // ViewState["_doitemserials"] = OldRevsFilterSerial;
            }


        }

        private void DisplayMessage(String Msg, Int32 option)
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
                //WriteErrLog(Msg);
            }
            else if (option == 5)
            {
                // WriteErrLog(Msg);
            }
        }
        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnsupplier_Click(object sender, EventArgs e)
        {
            try
            {

                if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtFDate.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MMM/yyyy");

                }
                if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchRevInvoice(SearchParams, null, null, Convert.ToDateTime(txtTDate.Text.ToString()), Convert.ToDateTime(txtFDate.Text.ToString()));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void SelectReqItem_Click(object sender, EventArgs e)
        {
            GridViewRow drDelect = (sender as LinkButton).NamingContainer as GridViewRow;
            Label col_Invoice = drDelect.FindControl("col_SEQ") as Label;
            if (!string.IsNullOrEmpty(col_Invoice.Text))
            {
                getPDASerial(Convert.ToInt32(col_Invoice.Text));
            }

        }
        public class InvoiceList
        {
            public string Invoiceno { get; set; }
            public string GRNA { get; set; }
            public int SEQ { get; set; }
        }

        protected void lbtnappTypePrint_Click(object sender, EventArgs e)
        {
            try
            {
                MasterCompany _newCom = new MasterCompany();
                _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                Session["CompanyName"] = _newCom.Mc_desc;
                if (Session["_stus"].ToString() == "A")
                {
                    if (appType.SelectedValue == "I")
                    {
                        bool _isSerialPrint = true;
                        Session["GlbReportType"] = "Serial";
                        Session["GlbReportName"] = "Reversal_serial.rpt";
                        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + "_1" + ".pdf";
                        PrintRevarsalSerial_PDF(targetFileName, string.Empty, _isSerialPrint);
                        lblMssgInv.Text = "Do you want to print the Approve note?";
                        popupInv.Show();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>window.open('/Temp/" + Session["UserID"].ToString() + "_1" + ".pdf','_blank');</script>", false);
                    }
                    else if (appType.SelectedValue == "S")
                    {
                        ApprovalNotePrintWith_Serial(string.Empty);
                    }
                    else
                    {
                        DisplayMessage("Plase Select Print type", 1);
                    }
                }
                else
                {
                    DisplayMessage("Please approve revesal before document print", 1);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Sales Reversal Print Approval", "Sales Reversal", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void ApprovalNotePrintWith_Serial(string serial)
        {
            //lblMssgSer.Text = "Do you want to print the " + "" + " Approve note?";
            //popUpSerial.Show();
            //List<ReptPickSerials> dt = ViewState["_doitemserials"] as List<ReptPickSerials>;
            //foreach (ReptPickSerials _val in dt)
            //{
            Session["GlbReportType"] = "Reversal";
            Session["GlbReportName"] = "ApprovalNote.rpt";
            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            PrintRevarsal_PDF(targetFileName, string.Empty, false);
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            //}
        }

        //protected void FilesLoad()
        //{
        //    string[] filePaths;
        //    List<ReptPickSerials> dt = ViewState["_doitemserials"] as List<ReptPickSerials>;
        //    List<ListItem> files = new List<ListItem>();
        //    filePaths = Directory.GetFiles(Server.MapPath("~/Temp/"));
        //    foreach (string filePath in filePaths)
        //    {
        //        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
        //    }
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
        //        zip.AddDirectoryByName("Files");
        //        int i = 0;
        //        foreach (ReptPickSerials _val in dt)
        //        {
        //            //var filePath =Convert.ToString( files[0].Text);
        //            zip.AddFile(files[i].Text, "Files");
        //            i++;
        //        }
        //        Response.Clear();
        //        Response.BufferOutput = false;
        //        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        //        Response.ContentType = "application/zip";
        //        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
        //        zip.Save(Response.OutputStream);
        //        Response.End();
        //    }
        //}

        protected void ApprovalNotePrint()
        {
            Session["GlbReportType"] = "Reversal";
            Session["GlbReportName"] = "ApprovalNote.rpt";
            string nameM = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
            string targetFileNameM = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + "2" + ".pdf";
            PrintRevarsal_PDF(targetFileNameM, string.Empty, false);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>window.open('/Temp/" + Session["UserID"].ToString() + "2" + ".pdf','_blank');</script>", false);
        }
        public void PrintRevarsalSerial_PDF(string targetFileName, string serialNo, bool _isSerialPrint)
        {
            try
            {
                DataTable dtval = CHNLSVC.General.getItemInSerial_Rev(Session["documntNo"].ToString());
                //List<ReptPickSerials> dt = ViewState["_doitemserials"] as List<ReptPickSerials>;
                //DataTable dtval = ToDataTable(dt);//_doitemserials
                if (appType.SelectedValue == "I" && _isSerialPrint == true)
                {
                    _serialRpt.Database.Tables["ReversalSerial"].SetDataSource(dtval);
                    _serialRpt.SetParameterValue("Company", Session["CompanyName"].ToString());
                    _serialRpt.SetParameterValue("user", Session["UserID"].ToString());
                    _serialRpt.SetParameterValue("RequestNo", Session["documntNo"].ToString());
                    _serialRpt.SetParameterValue("InvNo", txtInvoice.Text);
                }

                ReportDocument rptDoc = (ReportDocument)_serialRpt;
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
        public DataTable getLinq(DataTable dt2, DataTable dt1)
        {
            DataTable dtMerged =
                 (from a in dt1.AsEnumerable()
                  join b in dt2.AsEnumerable()
                                    on
          a["tus_ser_1"].ToString() equals b["tus_ser_1"].ToString()
                                  into g
                  where g.Count() > 0
                  select a).CopyToDataTable();
            return dtMerged;
        }

        public void PrintRevarsal_PDF(string targetFileName, string serialNo, bool _isSerialPrint)
        {
            try
            {
                DataTable _final = new DataTable();
                DataTable resultSet = CHNLSVC.General.getItemInSerial_Rev(Session["documntNo"].ToString());
                List<ReptPickSerials> dt = ViewState["_doitemserials"] as List<ReptPickSerials>;

                DataTable realSerial = CHNLSVC.General.getItemInSerial_Rev(Session["documntNo"].ToString());
                DataTable dtval = ToDataTable(dt);//_doitemserials
                dtval = dtval.AsEnumerable().GroupBy(r => r["tus_ser_1"]).Select(g => g.OrderBy(r => r["tus_ser_1"]).First()).CopyToDataTable();

                _final = getLinq(realSerial, dtval);
                ApprovalNote _appNote = new ApprovalNote();
                ApprovalNoteInvoiceWise _appNoteInv = new ApprovalNoteInvoiceWise();
                ReportDocument rptDoc = new ReportDocument();
                if (appType.SelectedValue == "I")
                {
                    _appNoteInv.Database.Tables["ReturnApproval"].SetDataSource(_final);
                    _appNoteInv.SetParameterValue("CusCode", Session["_dCusCode"].ToString());
                    _appNoteInv.SetParameterValue("CusName", Session["_dCusName"].ToString());
                    _appNoteInv.SetParameterValue("InvDate", Convert.ToDateTime(Session["_dInvDate"]));
                    _appNoteInv.SetParameterValue("Category", Session["_lblSubDesc"].ToString());
                    _appNoteInv.SetParameterValue("Remark", Session["_remark"].ToString());
                    _appNoteInv.SetParameterValue("AppUsr", Session["AppUsr"].ToString());
                    _appNoteInv.SetParameterValue("AppDate", Convert.ToDateTime(Session["AppDate"]));
                    _appNoteInv.SetParameterValue("SerialNo", serialNo);
                    _appNoteInv.SetParameterValue("IsStock", "true");
                    _appNoteInv.SetParameterValue("RequestNo", Session["documntNo"].ToString());
                    _appNoteInv.SetParameterValue("RequestDate", Session["_reqDate"].ToString());
                    rptDoc = (ReportDocument)_appNoteInv;
                }
                else
                {
                    _appNote.Database.Tables["ReturnApproval"].SetDataSource(_final);
                    _appNote.SetParameterValue("CusCode", Session["_dCusCode"].ToString());
                    _appNote.SetParameterValue("CusName", Session["_dCusName"].ToString());
                    _appNote.SetParameterValue("InvDate", Convert.ToDateTime(Session["_dInvDate"]));
                    _appNote.SetParameterValue("Category", Session["_lblSubDesc"].ToString());
                    _appNote.SetParameterValue("Remark", Session["_remark"].ToString());
                    _appNote.SetParameterValue("AppUsr", Session["AppUsr"].ToString());
                    _appNote.SetParameterValue("AppDate", Convert.ToDateTime(Session["AppDate"]));
                    _appNote.SetParameterValue("SerialNo", serialNo);
                    _appNote.SetParameterValue("IsStock", "true");
                    _appNote.SetParameterValue("RequestNo", Session["documntNo"].ToString());
                    _appNote.SetParameterValue("RequestDate", Session["_reqDate"].ToString());
                    rptDoc = (ReportDocument)_appNote;
                }
                //ReportDocument rptDoc = (appType.SelectedValue == "I" && _isSerialPrint == true) ? (ReportDocument)_serialRpt : (ReportDocument)_appNote;
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
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        protected void btnSerialYes_Click(object sender, EventArgs e)
        {

        }

        protected void btnSerialNo_Click(object sender, EventArgs e)
        {

        }

        protected void btnInvYes_Click(object sender, EventArgs e)
        {
            ApprovalNotePrint();
        }

        protected void btnInvNo_Click(object sender, EventArgs e)
        {

        }
    }
}