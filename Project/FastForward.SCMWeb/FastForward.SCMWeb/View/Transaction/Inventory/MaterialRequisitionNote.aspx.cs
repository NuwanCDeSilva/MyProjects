using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class MaterialRequisitionNote : BasePage
    {
        #region Variables
        bool _isValueExceedSave
        {
            set
            {
                Session["_isValueExceedMRN"] = value;
            }
            get
            {
                if (Session["_isValueExceedMRN"] != null)
                {
                    return (bool)Session["_isValueExceedMRN"];
                }
                else
                {
                    return false;
                }
            }
        }
        bool _isReqNoClear
        {
            set
            {
                Session["_isReqNoClear"] = value;
            }
            get
            {
                if (Session["_isReqNoClear"] != null)
                {
                    return (bool)Session["_isReqNoClear"];
                }
                else
                {
                    return false;
                }
            }
        }
        InventoryLocation _invLocBal = new InventoryLocation();
        List<INR_RES_LOG> _resLogAvaData = new List<INR_RES_LOG>();
        protected List<SatProjectDetails> _SatProjectDetails { get { return (List<SatProjectDetails>)Session["_SatProjectDetails"]; } set { Session["_SatProjectDetails"] = value; } }
        private MasterItem _itemdetail = null;
        private DataTable _CompanyItemStatus = null;
        List<InventoryRequestItem> _invReqItemList = null;
        bool _isDecimalAllow = false;
        bool _isShowInventoryBalance = false;
        bool _isApprovedUser = false;
        string _recallloc = string.Empty;
        bool IsRecalled = false;
        DataTable uniqueCols = new DataTable();
        string _userid = string.Empty;
        decimal qtyExists = 0;
        #endregion

        bool AutoApprove = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["REQTYPE"];
                if (!IsPostBack)
                {
                    HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                    if (_sysPara != null)
                    {
                        if (_sysPara.Hsy_val == 1)
                        {
                            divMrnBalance.Visible = true;
                            LoadLocationData();
                        }
                        else
                        {
                            divMrnBalance.Visible = false;
                        }
                    }
                    _isReqNoClear = true;
                    Session["ITEMLIST_INTR"] = new List<InventoryRequestItem>();
                    Session["ITEMLIST_MRN"] = new List<InventoryRequestItem>();
                    pnlAppRemark.Visible = false;
                    txtRemark.Enabled = false;
                    if (string.IsNullOrEmpty(txtRequest.Text))
                    {
                        txtRemark.Enabled = true;
                    }
                    gvInvoice.DataSource = new int[] { };
                    gvInvoice.DataBind();

                    gvItem.DataSource = new int[] { };
                    gvItem.DataBind();

                    dgvPromo.DataSource = new int[] { };
                    dgvPromo.DataBind();

                    DateTime orddate = DateTime.Now;
                    txtRequestDate.Text = orddate.ToString("dd/MMM/yyyy");

                    DateTime orddate1 = DateTime.Now;
                    txtRequriedDate.Text = orddate1.ToString("dd/MMM/yyyy");

                    InitVariables();

                    InitializeForm(true);

                    CheckPermission();

                    lblCaption.Text = "Only Display";
                    ddlRequestSubType_SelectedIndexChanged(null, null);

                    DateTime fromdate = DateTime.Now;
                    dtpFrom.Text = fromdate.ToString("dd/MMM/yyyy");

                    DateTime todate = DateTime.Now;
                    dtpTo.Text = todate.ToString("dd/MMM/yyyy");
                    txtSalesValue.Text = "0";
                    BindRequestSubTypesDDLData(ddlSearchReason);

                    if (_mytype == "MRN")
                    {
                        hidden.Visible = false;
                        hiddendiv.Visible = false;

                        btnBulkCancel.Enabled = false;
                        btnBulkCancel.CssClass = "buttoncolor";
                        blkcancelhide.Visible = false;

                        btnApproved.Enabled = false;
                        btnApproved.CssClass = "buttoncolor";
                        btnApproved.OnClientClick = "return Enable();";
                        //  hidediv.Visible = true;
                        lbltitle.Text = "Material Requisition Note";
                    }
                    else
                    {
                        hidden.Visible = true;
                        hiddendiv.Visible = true;

                        btnBulkCancel.Enabled = true;
                        btnBulkCancel.CssClass = "buttonUndocolor";
                        blkcancelhide.Visible = true;

                        btnApproved.Enabled = true;
                        btnApproved.CssClass = "buttonUndocolor";
                        btnApproved.OnClientClick = "ConfirmApproveRequest();";
                        // hidediv.Visible = false;
                        lbltitle.Text = "Inter Transfer Note";
                    }
                    Session["Dpop"] = "false";
                    Session["DISLOC"] = null;
                    txtDispatchRequried.ToolTip = string.Empty;
                    
                    TextBox _txtRequest = this.Page.FindControl("txtRequest") as TextBox;
                    _txtRequest.Text = string.Empty;
                }
                else
                {
                    if (_mytype != "MRN")
                    {
                        string bulkcancel = (string)Session["BULK_CANCEL"];
                        if (!string.IsNullOrEmpty(bulkcancel))
                        {
                            MpDelivery.Show();
                        }
                        else
                        {
                            MpDelivery.Hide();
                        }
                    }
                    else if (Session["Dpop"].ToString() == "true")
                    {
                        UserDPopoup.Show();

                        Session["popo"] = "";
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

        private void InitVariables()
        {
            gvItem.AutoGenerateColumns = false;
            gvInvoice.AutoGenerateColumns = false;
            _invReqItemList = new List<InventoryRequestItem>();
            _invReqItemList.Clear();
            Session["ITEMLIST_MRN"] = null;
            Session["ITEMLIST_INTR"] = null;
        }

        private void LoadCachedObjects()
        {
            _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
        }

        private void BindRequestSubTypesDDLData(DropDownList ddl)
        {
            try
            {
                string _mytype = Request.QueryString["REQTYPE"];
                ddl.DataSource = new List<MasterSubType>();
                List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes(_mytype);
                var _n = new MasterSubType();
                _n.Mstp_cd = string.Empty;
                _n.Mstp_desc = string.Empty;
                ddl.DataSource = _lst;
                ddl.DataTextField = "MSTP_DESC";
                ddl.DataValueField = "MSTP_CD";
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("Select", "0"));
                ddl.SelectedIndex = 0;
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

        private void BindCompany()
        {
            try
            {
                List<MasterCompany> _lst = CHNLSVC.General.GetALLMasterCompaniesData();
                var _n = new MasterCompany();
                _n.Mc_cd = string.Empty;
                _n.Mc_cd = string.Empty;
                ddlCompany.DataSource = _lst;
                ddlCompany.DataTextField = "Mc_desc";
                ddlCompany.DataValueField = "Mc_cd";
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new ListItem("Select", "0"));
                ddlCompany.SelectedIndex = 0;
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

        private void ClearLayer1(bool _isReloadSubType)
        {
            chkReqType.Checked = false;
            if (_isReloadSubType) BindRequestSubTypesDDLData(ddlRequestSubType);
            BindCompany();
            txtDispatchRequried.Text = string.Empty;
            if (_isReqNoClear)
            {
                txtRequest.Text = string.Empty;
            }
            txtSalesValue.Text = string.Empty;
        }

        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            LoadCachedObjects();
            DataTable _tbl = _CompanyItemStatus;
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            ddl.DataSource = _s;
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
            ddl.SelectedIndex = 0;
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText("GOOD"));
        }

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.0000}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        private void ClearLayer2()
        {
            txtItem.Text = string.Empty;
            BindUserCompanyItemStatusDDLData(cmbStatus);
            txtReservation.Text = string.Empty;

            txtQty.Text = DoFormat(0);

            lblAvalQty.Text = DoFormat(0);

            lblFreeQty.Text = DoFormat(0);

            txtItmRemark.Text = string.Empty;
        }

        private void ClearLayer3()
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItmUom.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSubStatus.Text = string.Empty;
        }

        private void ClearLayer4()
        {
            gvItem.DataSource = new List<RequestApprovalDetail>();
            gvItem.DataBind();
        }
        private void ClearLayer6()
        {
            txtRequestBy.Text = CHNLSVC.Security.GetUserByUserID(Session["UserID"].ToString()).Se_usr_name; ;
            txtNIC.Text = string.Empty;
            txtCollecterName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtAppRemark.Text = string.Empty;
        }
        private void InitializeForm(bool _isLoadSubType)
        {
            try
            {
                _isValueExceedSave = false;
                ClearLayer1(_isLoadSubType);
                ClearLayer2();
                ClearLayer3();
                ClearLayer4();
                ClearLayer6();
                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV3"))
                {
                    _isApprovedUser = true;
                    Session["isApprovedUser"] = "true";
                    AutoApprove = true;

                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmSave();";

                    btnAddItem.Enabled = true;
                    btnAddItem.CssClass = "buttonUndocolor";
                }
                else
                {
                    _isApprovedUser = false;
                    Session["isApprovedUser"] = "false";
                    AutoApprove = false;

                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmSave();";

                    btnAddItem.Enabled = true;
                    btnAddItem.CssClass = "buttonUndocolor";
                }
                if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV7"))
                    _isShowInventoryBalance = true;
                else
                    _isShowInventoryBalance = false;
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

        private void CheckPermission()
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11061))
            {
                chkReqType.Visible = true;
            }
            else
            {
                chkReqType.Visible = false;
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
        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_InterTransferNote", txtRequestDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }
        protected void ddlRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlRequestSubType.SelectedValue.ToString())) return;
                string _subtype = ddlRequestSubType.SelectedValue.ToString();
                InitializeForm(false);
                InitVariables();
                gvInvoice.DataSource = new List<InvoiceItem>();
                gvInvoice.DataBind();

                string _mytype = Request.QueryString["REQTYPE"];

                List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes(_mytype);
                var _isApprove = _lst.Where(x => x.Mstp_cd.Equals(_subtype) && x.Mstp_isapp).ToList();
                if (_isApprove != null && _isApprove.Count > 0) AutoApprove = true;
                else AutoApprove = false;
                string _company = Session["UserCompanyCode"].ToString();

                switch (_subtype)
                {
                    case "ADVAN":
                        lblCaption.Text = "Receipt";

                        lbldoctype.Text = "Receipt";
                        showhide1.Visible = true;
                        showhide2.Visible = false;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;

                        break;

                    case "DISP":
                        lblCaption.Text = "Only Display";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;

                    case "SALES":
                        lblCaption.Text = "Invoice";

                        showhidegrd.Visible = true;
                        showhide1.Visible = true;
                        showhide2.Visible = false;
                        lbldoctype.Text = "Invoice";
                        hidedataentrydv.Visible = false;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;

                        break;

                    case "AGETR":
                        lblCaption.Text = "AGE Item Transfer";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;

                    case "EXCHG":
                        lblCaption.Text = "Product Exchange";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;

                    case "RVRRT":
                        lblCaption.Text = "Reverse & Re-Report";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;

                    case "NOR":
                        lblCaption.Text = "Normal";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = false;
                        break;

                    case "PRQ":
                        lblCaption.Text = "Purchase Request";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;

                    case "SCV":
                        lblCaption.Text = "Service";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = true;
                        break;
                    case "BOQ":
                        // lblCaption.Text = "Service";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = false;
                        break;
                    case "PRO":
                        // lblCaption.Text = "Service";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = false;
                        break;
                    case "ADBOQ":
                        // lblCaption.Text = "Service";

                        showhide1.Visible = false;
                        showhide2.Visible = true;
                        showhidegrd.Visible = false;
                        hidedataentrydv.Visible = true;

                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Text = string.Empty;

                        ddlCompany.SelectedValue = _company;
                        ddlCompany.Enabled = false;
                        break;
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

        #region Rooting for Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InventoryItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InterTransferInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Convert.ToString(ddlCompany.SelectedValue) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InterTransferRequestWeb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserID"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InterTransferReceipt:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + ddlRequestSubType.SelectedValue.ToString().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRNWEB:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + Session["UserID"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductionPlan:
                    {
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
        protected void lbtnreqnosearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCompany.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the company !!!')", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
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

        private void FilterData()
        {
            try
            {

                if (lblvalue.Text == "boq")
                {
                    if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BOQ);
                        DataTable result = CHNLSVC.CommonSearch.SearchBoqProNoInMrn(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "boq";
                        ViewState["SEARCH"] = result;
                        grdResult.PageIndex = 0;
                        SIPopup.Show();
                        txtSearchbyword.Focus();
                        return;
                    }
                    if (ddlRequestSubType.SelectedValue == "PRO")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                        DataTable result = CHNLSVC.CommonSearch.SearchBoqProNoInMrn(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "boq";
                        ViewState["SEARCH"] = result;
                        grdResult.PageIndex = 0;
                        SIPopup.Show();
                        txtSearchbyword.Focus();
                        return;
                    }
                }
                else if (lblvalue.Text == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "1";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "5")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "5";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "428")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferRequestWeb);
                    DataTable result = CHNLSVC.Inventory.GetSearchInterTransferRequestWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);

                    DataTable dtresultcopyIntr = new DataTable();
                    dtresultcopyIntr.Columns.AddRange(new DataColumn[5] { new DataColumn("Inter-Transfer No"), new DataColumn("Manual Ref."), new DataColumn("Status"), new DataColumn("Request From"), new DataColumn("Date") });

                    foreach (DataRow ddrintr in result.Rows)
                    {
                        string intertransfer = ddrintr["Inter-Transfer No"].ToString();
                        string manual_ref = ddrintr["Manual Ref."].ToString();
                        string status = ddrintr["Status"].ToString();
                        string requestfrom = ddrintr["Request From"].ToString();
                        string formatteddate = ddrintr["Date"].ToString();

                        dtresultcopyIntr.Rows.Add(intertransfer, manual_ref, status, requestfrom, formatteddate);
                    }

                    grdResult.DataSource = dtresultcopyIntr;
                    grdResult.DataBind();
                    lblvalue.Text = "428";
                    ViewState["SEARCH"] = dtresultcopyIntr;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "81")
                {
                    if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                        DataTable result = CHNLSVC.CommonSearch.GetItemforBoqMrn(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "81";
                        ViewState["SEARCH"] = result;
                        grdResult.PageIndex = 0;
                        SIPopup.Show();
                        txtSearchbyword.Focus();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                        DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "81";
                        ViewState["SEARCH"] = result;
                        grdResult.PageIndex = 0;
                        SIPopup.Show();
                        txtSearchbyword.Focus();
                    }
                }

                else if (lblvalue.Text == "115")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferInvoice);
                    DataTable result = CHNLSVC.CommonSearch.GetInvoiceforInterTransferSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "115";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "114")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferReceipt);
                    DataTable result = CHNLSVC.CommonSearch.GetReceiptsByType(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);

                    DataTable dtresultcopyIntr = new DataTable();
                    dtresultcopyIntr.Columns.AddRange(new DataColumn[3] { new DataColumn("Receipt No"), new DataColumn("Receipt Ref"), new DataColumn("Receipt Date") });

                    foreach (DataRow ddrintr in result.Rows)
                    {
                        string receptno = ddrintr["Receipt No"].ToString();
                        string recepref = ddrintr["Receipt Ref"].ToString();
                        string receptdate = ((DateTime)ddrintr["Receipt Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                        dtresultcopyIntr.Rows.Add(receptno, recepref, receptdate);
                    }

                    grdResult.DataSource = dtresultcopyIntr;
                    grdResult.DataBind();
                    lblvalue.Text = "114";
                    ViewState["SEARCH"] = dtresultcopyIntr;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "432")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                    // DataTable result = CHNLSVC.Inventory.GetSearchMRNWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    DataTable result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, DateTime.MinValue, DateTime.MinValue);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "432";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
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
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
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
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void txtRequest_Leave(object sender, EventArgs e)
        {
            pnlAppRemark.Visible = false;
            txtRemark.Enabled = true;
            gvItem.DataSource = new int[] { };
            gvItem.DataBind();

            txtRequestBy.Text = "";
            txtApprovedBy.Text = "";
            txtNIC.Text = "";
            txtCollecterName.Text = "";
            txtRemark.Text = "";
            txtAppRemark.Text = "";
            txtSalesValue.Text = "0";
            if (string.IsNullOrEmpty(txtRequest.Text)) return;
            try
            {

                _isReqNoClear = false;
                _recallloc = string.Empty;
                IsRecalled = false;
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                //List<InventoryRequestItem> _invReqList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(txtRequest.Text);
                //_selectedInventoryRequest.InventoryRequestItemList = _invReqList;
                string _mytype = Request.QueryString["REQTYPE"];

                if (_selectedInventoryRequest != null)
                    pnlAppRemark.Visible = true;
                txtRemark.Enabled = false;
                if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_com))
                {
                    _recallloc = _selectedInventoryRequest.Itr_loc;
                    IsRecalled = true;

                    Session["STATUS"] = _selectedInventoryRequest.Itr_stus;

                    this.SetSelectedInventoryRequestData(_selectedInventoryRequest);

                    string isapproveduser = (string)Session["isApprovedUser"];

                    if (isapproveduser == "true")
                    {
                        _isApprovedUser = true;
                    }
                    else
                    {
                        _isApprovedUser = false;
                    }


                    if ((_selectedInventoryRequest.Itr_stus == "C") || (_selectedInventoryRequest.Itr_stus == "A"))
                    {
                        btnApproved.Enabled = false;
                        btnApproved.CssClass = "buttoncolor";
                        btnApproved.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        btnCancel.Enabled = true;
                        btnCancel.CssClass = "buttonUndocolor";
                        btnCancel.OnClientClick = "ConfirmCancelReq();";

                        if (_mytype != "MRN")
                        {
                            btnApproved.Enabled = true;
                            btnApproved.CssClass = "buttonUndocolor";
                            btnApproved.OnClientClick = "ConfirmApproveRequest();";
                        }
                        else
                        {
                            btnApproved.Enabled = true;
                            btnApproved.CssClass = "buttonUndocolor";
                            btnApproved.OnClientClick = "ConfirmApproveRequest();";
                        }
                    }

                    if (_selectedInventoryRequest.Itr_stus == "C")
                    {
                        btnCancel.Enabled = false;
                        btnCancel.CssClass = "buttoncolor";
                        btnCancel.OnClientClick = "return Enable();";
                    }

                    if (_selectedInventoryRequest.Itr_stus == "A")
                    {
                        btnCancel.Enabled = true;
                        btnCancel.CssClass = "buttonUndocolor";
                        btnCancel.OnClientClick = "ConfirmCancelReq();";
                    }

                    if (_selectedInventoryRequest.Itr_stus == "A")
                    {
                        if (_mytype == "MRN")
                        {
                            foreach (GridViewRow hiderowbtn in this.gvItem.Rows)
                            {
                                gvItem.Columns[0].Visible = false;
                                gvItem.Columns[1].Visible = false;
                            }
                        }
                        else
                        {
                            foreach (GridViewRow hiderowbtn in this.gvItem.Rows)
                            {
                                gvItem.Columns[0].Visible = true;
                                gvItem.Columns[1].Visible = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (GridViewRow hiderowbtn in this.gvItem.Rows)
                        {
                            gvItem.Columns[0].Visible = true;
                            gvItem.Columns[1].Visible = true;
                        }
                    }
                    //if (_selectedInventoryRequest.Itr_stus == "A")
                    //{
                    //    foreach (GridViewRow item in gvItem.Rows)
                    //    {
                    //        Label secondlblqty2 = item.FindControl("secondlblqty2") as Label;
                    //    }
                    //}
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request no is invalid !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
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

        private void ClearCustomerDetail()
        {
            lblCusCode.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            gvInvoice.DataSource = null;
            gvInvoice.DataBind();
        }
        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoice.Text)) return;

                DataTable _chk = CHNLSVC.Sales.CheckTheDocument(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim());
                if (_chk != null && _chk.Rows.Count > 0)
                {
                    string _refDocument = _chk.Rows[0].Field<string>("itr_req_no");
                    if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This invoice is already picked for a inter-transfer. You are not allow to raise another request for the pending reference !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }
                    if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This advance receipt is already picked for a inter-transfer. You are not allow to raise another request for the pending reference !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }
                    txtInvoice.Text = "";
                    txtInvoice.Focus();
                    return;
                }

                if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim());
                    if (_adv != null && _adv.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This advance receipt is already picked for a inter-transfer. You are not allow to raise another request for the pending reference !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtInvoice.Text = "";
                        txtInvoice.Focus();
                        return;
                    }

                }

                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text.Trim());
                    if (string.IsNullOrEmpty(_hdr.Sah_com))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the invoice no !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }

                    if (_hdr.Sah_pc != Session["UserDefProf"].ToString())
                    {
                        string msg = "You are going to select " + _hdr.Sah_pc + " invoice !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }

                    List<InvoiceItem> _lst = CHNLSVC.Sales.GetInterTransferInvoice(txtInvoice.Text.Trim());
                    if (_lst == null || _lst.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There is no pending item for request inter-transfer !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }

                    ClearCustomerDetail();

                    lblCusCode.Text = _hdr.Sah_cus_cd;
                    lblCusName.Text = _hdr.Sah_cus_name;
                    lblCusAddress.Text = _hdr.Sah_cus_add1 + " " + _hdr.Sah_cus_add2;
                    _lst.ForEach(x => x.Sad_qty = x.Sad_qty - x.Sad_do_qty);
                    gvInvoice.DataSource = _lst;
                    gvInvoice.DataBind();
                }

                if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    LoadReceiptDetails();
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

        private void LoadReceiptDetails()
        {
            RecieptHeader _ReceiptHeader = null;
            ClearCustomerDetail();
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), ddlRequestSubType.SelectedValue.ToString().ToUpper());
            if (_ReceiptHeader != null)
            {
                if (_ReceiptHeader.Sar_act == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected advance receipt is already cancelled !!!')", true);
                    txtInvoice.Text = string.Empty;
                    return;
                }

                if (_ReceiptHeader.Sar_used_amt > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected advance receipt is already used or refunded. Cannot use for request !!!')", true);
                    txtInvoice.Text = string.Empty;
                    return;
                }

                lblCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                lblCusName.Text = _ReceiptHeader.Sar_debtor_name;
                lblCusAddress.Text = _ReceiptHeader.Sar_debtor_add_1 + " " + _ReceiptHeader.Sar_debtor_add_2;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid receipt no !!!')", true);
                txtInvoice.Text = string.Empty;
                return;
            }
        }

        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                if (ddlRequestSubType.Items.FindByValue(_selectedInventoryRequest.Itr_sub_tp) != null)
                {
                    ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                }

                txtRequest.Text = _selectedInventoryRequest.Itr_req_no;
                ddlCompany.SelectedValue = _selectedInventoryRequest.Itr_issue_com;
                txtDispatchRequried.Text = _selectedInventoryRequest.Itr_issue_from;
                txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToString("dd/MMM/yyyy");
                txtRequriedDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToString("dd/MMM/yyyy");
                txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;

                txtNIC.Text = _selectedInventoryRequest.Itr_collector_id;
                txtCollecterName.Text = _selectedInventoryRequest.Itr_collector_name;
                txtRemark.Text = _selectedInventoryRequest.Itr_note;
                #region re set remark data with split 09 Nov 2016
                //Spliit for Search control type.
                string _remark = _selectedInventoryRequest.Itr_note;
                if (_remark.Contains(";"))
                {
                    string[] seperator = new string[] { ";" };
                    string[] searchParams = _remark.Split(seperator, StringSplitOptions.None);
                    txtRemark.Text = searchParams[0].ToString();
                    txtAppRemark.Text = searchParams[1].ToString();
                }
                #endregion

                txtCollecterName.ToolTip = _selectedInventoryRequest.Itr_collector_name;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable resultData = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, "Code", txtDispatchRequried.Text);


                foreach (DataRow ddr in resultData.Rows)
                {
                    Session["DISLOC"] = ddr["Description"].ToString();
                    string disloc = (string)Session["DISLOC"];
                    txtDispatchRequried.ToolTip = disloc;
                }

                if (_selectedInventoryRequest.InventoryRequestItemList != null)
                {
                    _selectedInventoryRequest.InventoryRequestItemList.ForEach(X => X.Itri_mitm_cd = X.Itri_itm_cd);
                    _selectedInventoryRequest.InventoryRequestItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                    _invReqItemList = _selectedInventoryRequest.InventoryRequestItemList;
                    MasterItem _mstItm = new MasterItem();
                    foreach (var item in _selectedInventoryRequest.InventoryRequestItemList)
                    {
                        item.Tmp_Itri_app_rem_show = 1;
                        #region re set remark data with split 09 Nov 2016
                        //Spliit for Search control type.
                        string _remarkItem = item.Itri_note;
                        if (_remarkItem.Contains(";"))
                        {
                            string[] seperator = new string[] { ";" };
                            string[] searchParams = _remarkItem.Split(seperator, StringSplitOptions.None);
                            item.Itri_note = searchParams[0].ToString();
                            item.Tmp_Itri_app_rem = searchParams[1].ToString();
                        }
                        #endregion
                        #region show balance available in location 10 Nov 2016 by Lakshan
                        _invLocBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(new InventoryLocation()
                        {
                            Inl_com = _selectedInventoryRequest.Itr_issue_com,
                            Inl_loc = _selectedInventoryRequest.Itr_issue_from,
                            Inl_itm_cd = item.Itri_itm_cd,
                            Inl_itm_stus = item.Itri_itm_stus
                        });
                        if (_invLocBal != null)
                        {
                            item.Tmp_dis_loc_bal = _invLocBal.Inl_qty;
                        }
                        _invLocBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(new InventoryLocation()
                        {
                            Inl_com = _selectedInventoryRequest.Itr_com,
                            Inl_loc = _selectedInventoryRequest.Itr_rec_to,
                            Inl_itm_cd = item.Itri_itm_cd,
                            Inl_itm_stus = item.Itri_itm_stus
                        });
                        if (_invLocBal != null)
                        {
                            item.Tmp_loc_bal = _invLocBal.Inl_qty;
                        }
                        item.Tmp_loc_bal_show = 1;
                        hdfLoc.Value = _selectedInventoryRequest.Itr_rec_to;
                        hdfDisLoc.Value = _selectedInventoryRequest.Itr_issue_from;
                        #endregion
                    }
                    foreach (var item in _selectedInventoryRequest.InventoryRequestItemList)
                    {
                        #region UOM setup
                        _mstItm = CHNLSVC.General.GetItemMaster(item.Itri_itm_cd);
                        if (_mstItm != null)
                        {
                            item.MasterItem.Mi_itm_uom = _mstItm.Mi_itm_uom;
                        }
                        #endregion
                    }
                    gvItem.DataSource = _selectedInventoryRequest.InventoryRequestItemList;
                    gvItem.DataBind();
                    LoadGridUnitCost(_selectedInventoryRequest.InventoryRequestItemList);
                    if (!string.IsNullOrEmpty(txtRequest.Text))
                    {
                        Label lblHdrLocBal = gvItem.HeaderRow.FindControl("lblHdrLocBal") as Label;
                        Label lblHdrDisLocBal = gvItem.HeaderRow.FindControl("lblHdrDisLocBal") as Label;
                        lblHdrLocBal.Text = "Bal (" + hdfLoc.Value.ToString() + ")";
                        lblHdrDisLocBal.Text = "Bal (" + hdfDisLoc.Value.ToString() + ")";
                    }

                    string _mytype = Request.QueryString["REQTYPE"];

                    if (_mytype == "MRN")
                    {
                        Session["ITEMLIST_MRN"] = _invReqItemList;
                        Session["ITEMLIST_INTR"] = null;
                    }
                    else
                    {
                        Session["ITEMLIST_MRN"] = null;
                        Session["ITEMLIST_INTR"] = _invReqItemList;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('There are no pending items !!!')", true);
                    InitializeForm(true);
                    InitVariables();
                    return;
                }

                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;
                    txtInvoice_Leave(null, null);
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {

                    txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;
                    LoadReceiptDetails();
                }

                if (_selectedInventoryRequest.Itr_stus.ToUpper() == "A" || _selectedInventoryRequest.Itr_stus.ToUpper() == "F" || _selectedInventoryRequest.Itr_stus.ToUpper() == "C")
                {
                    btnSave.Enabled = false;
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";
                }
                else if (_selectedInventoryRequest.Itr_stus.ToUpper() == "P")
                {
                    btnSave.Enabled = false;
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV3"))
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "buttoncolor";
                        btnSave.OnClientClick = "return Enable();";

                        btnAddItem.Enabled = false;
                        btnAddItem.CssClass = "buttoncolor";
                    }
                }

                DataTable _tbl = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_cre_by);
                if (_tbl != null && _tbl.Rows.Count > 0)
                {
                    string _name = _tbl.Rows[0].Field<string>("se_usr_name");
                    txtRequestBy.Text = _name;
                }
                else txtRequestBy.Text = string.Empty;

                if (_selectedInventoryRequest.Itr_stus == "A")
                {
                    if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_gran_app_by))
                    {
                        DataTable _tbl0 = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_gran_app_by);
                        if (_tbl0 != null && _tbl0.Rows.Count > 0)
                        {
                            string _name = _tbl0.Rows[0].Field<string>("se_usr_name");
                            txtApprovedBy.Text = _name;
                        }
                    }
                    else
                    {
                        DataTable _tbl0 = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_mod_by);
                        if (_tbl0 != null && _tbl0.Rows.Count > 0)
                        {
                            string _name = _tbl0.Rows[0].Field<string>("se_usr_name");
                            txtApprovedBy.Text = _name;
                        }
                    }

                }
                else txtApprovedBy.Text = string.Empty;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (lblvalue.Text == "boq")
                {
                    txtKitCode.Text = "";
                    txtBoq.Text = grdResult.SelectedRow.Cells[1].Text;
                    loadBoqItem(txtBoq.Text);
                    lblvalue.Text = "";
                }
                if (lblvalue.Text == "1")
                {
                    txtDispatchRequried.Text = grdResult.SelectedRow.Cells[1].Text;
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text)) return;

                    if (txtDispatchRequried.Text.Trim() == Session["UserDefLoca"].ToString())
                    {
                        txtDispatchRequried.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not enter same location which you already logged !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }

                    if (!chkReqType.Checked)
                    {
                        if (IsValidLocation() == false)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid location code or permission not allow for the selected location !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            txtDispatchRequried.Text = string.Empty;
                            txtDispatchRequried.Focus();
                            return;
                        }
                    }

                    Session["DISLOC"] = grdResult.SelectedRow.Cells[2].Text;
                    string disloc = (string)Session["DISLOC"];
                    txtDispatchRequried.ToolTip = disloc;

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                if (lblvalue.Text == "428")
                {
                    txtRequest.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRequest_Leave(null, null);
                }
                if (lblvalue.Text == "81")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    CheckItemCode(null, null);
                    cmbStatus.Focus();

                    DataTable dtdiscontinue = CHNLSVC.Inventory.CheckIsItemDiscontinue(txtItem.Text.Trim());

                    string stus = string.Empty;
                    if (dtdiscontinue.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtdiscontinue.Rows)
                        {
                            stus = item["RIR_DESC"].ToString();
                        }
                    }

                    if (stus.ToUpper() == "DISCONTINUE")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('This is a discontinued item !!!');", true);
                    }

                }
                if (lblvalue.Text == "115")
                {
                    txtInvoice.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoice_Leave(null, null);
                }
                if (lblvalue.Text == "114")
                {
                    txtInvoice.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoice_Leave(null, null);
                }
                if (lblvalue.Text == "5")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtLoc.Text = grdResult.SelectedRow.Cells[1].Text;
                    MpDelivery.Show();
                }
                if (lblvalue.Text == "432")
                {
                    txtRequest.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRequest_Leave(null, null);

                    foreach (GridViewRow item in gvItem.Rows)
                    {
                        Label lblstatus2 = (Label)item.FindControl("lblstatus2");

                        Label lbtnstusdesc = (Label)item.FindControl("lbtnstusdesc");

                        DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstatus2.Text.Trim());

                        if (dtstatustx.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2 in dtstatustx.Rows)
                            {
                                lbtnstusdesc.Text = ddr2[0].ToString();
                            }
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
            }
            catch
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItmUom.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSubStatus.Text = string.Empty;
            _isDecimalAllow = false;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_subitem == true ? "Available" : "Non-Serialized";

                    lblItemDescription.Text = _description;
                    lblItemModel.Text = _model;
                    lblItmUom.Text = _itemdetail.Mi_itm_uom;
                    lblItemBrand.Text = _brand;
                    lblItemSubStatus.Text = _serialstatus;
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                }
            return _isValid;
        }

        private void CheckItemCode(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            try
            {
                if (ddlRequestSubType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document sub type !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    ddlRequestSubType.Focus();
                    return;
                }
                if (!chkReqType.Checked)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the dispatch location !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        lbtnreqnosearch.Focus();
                        txtItem.Text = string.Empty;
                        return;
                    }
                }
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the item code !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtItem.Text = string.Empty;
                    txtItem.Focus();
                    return;
                }

                if (ddlRequestSubType.SelectedValue.ToString() != "SALES")
                    if (string.IsNullOrEmpty(cmbStatus.Text))
                        DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
                    else
                        DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, cmbStatus.SelectedValue.ToString());

                if (ddlRequestSubType.SelectedValue.ToString() != "SALES")
                    if (_itemdetail.Mi_itm_tp != "V")
                        LoadDispatchLocationInventoryBalance(txtItem.Text.Trim());
                    else
                        txtReservation.Focus();

                DataTable dtdiscontinue = CHNLSVC.Inventory.CheckIsItemDiscontinue(txtItem.Text.Trim());

                string stus = string.Empty;
                if (dtdiscontinue.Rows.Count > 0)
                {
                    foreach (DataRow item in dtdiscontinue.Rows)
                    {
                        stus = item["RIR_DESC"].ToString();
                    }
                }

                if (stus.ToUpper() == "DISCONTINUE")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('This is a discontinued item !!!');", true);
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

        private void LoadDispatchLocationInventoryBalance(string _item)
        {
            if (_isShowInventoryBalance == false) return;
            List<InventoryLocation> _lst = CHNLSVC.Inventory.GetInventoryBalanceSCMnSCM2(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text, _item, string.Empty);
            if (_lst != null)
                if (_lst.Count > 0)
                {
                    //gvBalance.DataSource = new DataTable();
                    //gvBalance.DataSource = _lst;
                    //gvBalance.Focus();
                }
        }
        private void DisplayAvailableQty(string _item, Label _avalQty, Label _freeQty, string _status)
        {
            List<InventoryLocation> _inventoryLocation = null;
            if (string.IsNullOrEmpty(_status))
            {
                //_inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item.Trim(), string.Empty);
                //Edit by Chamal 12-Mar-2018
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.ToString(), _item.Trim(), string.Empty);
            }
            else
            {
                //_inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item.Trim(), _status);
                //Edit by Chamal 12-Mar-2018
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.ToString(), _item.Trim(), _status);
            }

            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                    var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();

                    _avalQty.Text = DoFormat(Convert.ToDecimal(_aQty));
                    _freeQty.Text = DoFormat(Convert.ToDecimal(_aFree));
                }
                else
                {
                    _avalQty.Text = DoFormat(Convert.ToDecimal(0));
                    _freeQty.Text = DoFormat(Convert.ToDecimal(0));
                }
            else
            {
                _avalQty.Text = DoFormat(Convert.ToDecimal(0));
                _freeQty.Text = DoFormat(Convert.ToDecimal(0));
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                string _mytype = Request.QueryString["REQTYPE"];

                if (_mytype == "MRN")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    txtFDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy");
                    txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                    DataTable result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "SerchMrnWeb";
                    BindUCtrlDDLData2(result);
                    ViewState["SEARCH"] = result;
                    grdResultD.PageIndex = 0;
                    UserDPopoup.Show();
                    txtSearchbyword.Focus();
                    lbtnDateS.Visible = false;

                    //ViewState["SEARCH"] = null;
                    //txtSearchbyword.Text = string.Empty;
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                    //DataTable result = CHNLSVC.Inventory.GetSearchMRNWeb(SearchParams, null, null);
                    //grdResult.DataSource = result;
                    //grdResult.DataBind();
                    //lblvalue.Text = "432";
                    //BindUCtrlDDLData(result);
                    //ViewState["SEARCH"] = result;
                    //grdResult.PageIndex = 0;
                    //SIPopup.Show();
                    //txtSearchbyword.Focus();
                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferRequestWeb);
                    DataTable result = CHNLSVC.Inventory.GetSearchInterTransferRequestWeb(SearchParams, null, null);

                    DataTable dtresultcopyIntr = new DataTable();
                    dtresultcopyIntr.Columns.AddRange(new DataColumn[5] { new DataColumn("Inter-Transfer No"), new DataColumn("Manual Ref."), new DataColumn("Status"), new DataColumn("Request From"), new DataColumn("Date") });

                    foreach (DataRow ddrintr in result.Rows)
                    {
                        string intertransfer = ddrintr["Inter-Transfer No"].ToString();
                        string manual_ref = ddrintr["Manual Ref."].ToString();
                        string status = ddrintr["Status"].ToString();
                        string requestfrom = ddrintr["Request From"].ToString();
                        string date = ddrintr["Date"].ToString();
                        dtresultcopyIntr.Rows.Add(intertransfer, manual_ref, status, requestfrom, date);
                    }

                    grdResult.DataSource = dtresultcopyIntr;
                    grdResult.DataBind();
                    lblvalue.Text = "428";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = dtresultcopyIntr;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
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

        protected void lbtnmodelfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestSubType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document sub type !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    ddlRequestSubType.Focus();
                    return;
                }

                if (!chkReqType.Checked)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the dispatch location !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtItem.Text = string.Empty;
                        return;
                    }
                }

                if (cmbStatus.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select item status !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    cmbStatus.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforBoqMrn(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "81";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "81";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
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

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferInvoice);
                    DataTable result = CHNLSVC.CommonSearch.GetInvoiceforInterTransferSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "115";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferReceipt);
                    DataTable result = CHNLSVC.CommonSearch.GetReceiptsByType(SearchParams, null, null);

                    DataTable dtresultcopyIntr = new DataTable();
                    dtresultcopyIntr.Columns.AddRange(new DataColumn[3] { new DataColumn("Receipt No"), new DataColumn("Receipt Ref"), new DataColumn("Receipt Date") });

                    foreach (DataRow ddrintr in result.Rows)
                    {
                        string receptno = ddrintr["Receipt No"].ToString();
                        string recepref = ddrintr["Receipt Ref"].ToString();
                        string receptdate = ((DateTime)ddrintr["Receipt Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                        dtresultcopyIntr.Rows.Add(receptno, recepref, receptdate);
                    }

                    grdResult.DataSource = dtresultcopyIntr;
                    grdResult.DataBind();
                    lblvalue.Text = "114";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = dtresultcopyIntr;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
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

        #endregion

        private bool IsValidLocation()
        {
            bool status = false;
            txtDispatchRequried.Text = txtDispatchRequried.Text.Trim().ToUpper().ToString();
            MasterLocation _masterLocationDisp = CHNLSVC.General.GetLocationByLocCode(Convert.ToString(ddlCompany.SelectedValue), txtDispatchRequried.Text.ToString());
            MasterLocation _masterLocationFrm = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            status = (_masterLocationDisp == null) ? false : true;
            if (_masterLocationDisp != null)
                if (!string.IsNullOrEmpty(_masterLocationDisp.Ml_com_cd))
                {
                    string _fromcompany = Session["UserCompanyCode"].ToString();
                    string _fromlocation = Session["UserDefLoca"].ToString();
                    string _tocompany = Convert.ToString(ddlCompany.SelectedValue);
                    string _toLocation = txtDispatchRequried.Text.Trim();
                    string _fromChnl = _masterLocationFrm.Ml_cate_3;
                    string _tochnl = _masterLocationDisp.Ml_cate_3;


                    if (string.IsNullOrEmpty(_fromChnl) || string.IsNullOrEmpty(_tochnl))
                    {
                        string msg = "There is no channel define for the " + (string.IsNullOrEmpty(_fromChnl) ? "your location " + _fromlocation : "selected location " + _toLocation) + ". Please contact it dept !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        status = false;
                        return status;
                    }

                    string _mytype = Request.QueryString["REQTYPE"];

                    DataTable _tbs = CHNLSVC.Inventory.GetChannelPermission(_mytype, Convert.ToString(ddlRequestSubType.SelectedValue), _fromChnl, _tochnl);
                    if (_tbs != null && _tbs.Rows.Count > 0)
                    {
                        AutoApprove = true;
                    }
                    else
                    {
                        string _subtype = ddlRequestSubType.SelectedValue.ToString();
                        List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes(_mytype);
                        var _isApprove = _lst.Where(x => x.Mstp_cd.Equals(_subtype) && x.Mstp_isapp).ToList();
                        if (_isApprove != null && _isApprove.Count > 0) AutoApprove = true;
                        else AutoApprove = false;
                    }
                    if (_isApprovedUser) AutoApprove = true;
                }
            return status;
        }

        protected void chkReqType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReqType.Checked == true)
            {
                txtDispatchRequried.Enabled = false;
            }
            else
            {
                txtDispatchRequried.Enabled = true;
            }
        }

        private void SessionClear()
        {
            try
            {
                Session["ITEMLIST_MRN"] = null;
                Session["ITEMLIST_INTR"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    _isReqNoClear = true;
                    Session["ITEMLIST_INTR"] = new List<InventoryRequestItem>();
                    Session["ITEMLIST_MRN"] = new List<InventoryRequestItem>();
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ClearAll()
        {
            try
            {
                InitializeForm(true);
                InitVariables();
                BackDatePermission();
                SessionClear();
                _invReqItemList = null;
                lblCusCode.Text = string.Empty;
                lblCusName.Text = string.Empty;
                lblCusAddress.Text = string.Empty;
                gvInvoice.DataSource = null;
                gvInvoice.DataBind();
                txtRequestBy.Text = string.Empty;
                txtApprovedBy.Text = string.Empty;
                txtNIC.Text = string.Empty;
                txtCollecterName.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtAppRemark.Text = string.Empty;
                txtInvoice.Text = string.Empty;
                Session["DISLOC"] = null;
                txtDispatchRequried.ToolTip = string.Empty;
                dgvPromo.DataSource = new int[] { };
                dgvPromo.DataBind();
                txtRequestBy.Text = string.Empty;
                Session["ITEMLIST_MRN"] = null;
                Session["ITEMLIST_INTR"] = null;
                ddlSearchReason.SelectedIndex = 0;
                txtLoc.Text = string.Empty;
                cmbSearchStatus.SelectedIndex = 0;
                txt_cashGiven.Text = string.Empty;
                txtBoq.Text = string.Empty;
                txtBatch.Text = string.Empty;
                ddlCompany.Enabled = true;

                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("BodyContent");
                TextBox _txtReques = cph.FindControl("txtRequest") as TextBox;
                _txtReques.Text = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void BindInventoryRequestItemsGridData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter item code !!!')", true);
                    return;
                }

                if (cmbStatus.SelectedIndex == 0)
                {
                    cmbStatus.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select item status !!!')", true);
                    return;
                }

                if ((string.IsNullOrEmpty(txtQty.Text)) || ((txtQty.Text == "0")))
                {
                    txtQty.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter required quantity !!!')", true);
                    return;
                }

                if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtReservation.Text))
                {
                    /*
                      DataTable _dt = CHNLSVC.Inventory.GetReservationDet(Session["UserCompanyCode"].ToString(), txtReservation.Text, txtItem.Text, cmbStatus.SelectedValue.ToString());
                      if (_dt.Rows.Count == 0)
                      {
                          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid reservation number !!!')", true);
                          txtReservation.Text = "";
                          txtReservation.Focus();
                          return;
                      }
                      if (_dt.Rows.Count>0)
                      {
                          decimal _resQty = Convert.ToDecimal(_dt.Rows[0]["IRD_RES_BQTY"].ToString());
                          if (_resQty<Convert.ToDecimal(txtQty.Text.Trim()))
                          {
                              string _msg = "Cannot exceed the reservation balance ! " + _resQty.ToString("N2");
                              DispMsg(_msg);
                              txtQty.Text = "";
                              txtQty.Focus();
                              return;
                          }
                      }
                      */
                }

                MasterItem _itemMas = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                _invReqItemList = new List<InventoryRequestItem>();

                DataTable _dtMax = CHNLSVC.General.GetStockRequest("GIT", Session["UserDefLoca"].ToString(), BaseCls.GlbDefChannel, Session["UserCompanyCode"].ToString(), DateTime.Now.Date, txtItem.Text, _itemMas.Mi_brand, _itemMas.Mi_cate_1, _itemMas.Mi_cate_2, _itemMas.Mi_cate_3, _itemMas.Mi_cate_4, _itemMas.Mi_cate_5);
                if (_dtMax.Rows.Count > 0)
                {
                    DataTable _dtGit = CHNLSVC.General.GetItemGIT(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, null, null, null, null, null, null, 0);
                    if (_dtGit.Rows.Count > 0)
                        if (Convert.ToDecimal(_dtGit.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                        {
                            string msg = "GIT available.You are exceeding allowable quantity !!! " + Session["UserDefLoca"].ToString();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('GIT available.You are exceeding allowable quantity !!!')", true);
                            return;
                        }
                }

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? string.Empty : txtItmRemark.Text.Trim();
                //  bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Available" ? true : false;

                List<InventoryRequestItem> _temp = _invReqItemList;
                List<InventoryRequestItem> _resultList = null;


            Outer:
                if (_isSubItemHave)
                {
                    List<MasterItemComponent> _newItemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);
                    if (_newItemComponentList==null)
                    {
                        DispMsg("Item component not available. Please check the item code !"); return;
                    }
                    string MaincomItem = _newItemComponentList.Where(a => a.Micp_itm_tp == "M").FirstOrDefault().ComponentItem.Mi_cd;
                    List<MasterItemComponent> _itemComponentList = new List<MasterItemComponent>();
                    if (_itemComponentList == null)
                    {
                        _isSubItemHave = false;
                        goto Outer;
                    }
                    else
                    {
                        _itemComponentList = _newItemComponentList.Where(c => c.Micp_must_scan == false).ToList();
                    }
                    if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                    {
                        foreach (MasterItemComponent _itemCompo in _itemComponentList)
                        {
                            if (_invReqItemList != null)
                            {
                                if (_invReqItemList.Count > 0)
                                {
                                    _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd) && x.Itri_itm_stus == _itemStatus).ToList();
                                }
                            }
                            if ((_resultList != null) && (_resultList.Count > 0))
                            {
                                foreach (InventoryRequestItem _result in _resultList)
                                {
                                    _result.Itri_qty = _result.Itri_qty + (_mainItemQty * _itemCompo.Micp_qty);
                                }
                            }
                            else
                            {
                                //Issue 
                                InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;
                                
                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _inventoryRequestItem.Itri_itm_cd = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _inventoryRequestItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _inventoryRequestItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _componentItem.Mi_itm_uom = _itemCompo.ComponentItem.Mi_itm_uom;
                                _inventoryRequestItem.Mi_itm_uom = _itemCompo.ComponentItem.Mi_itm_uom;
                                _inventoryRequestItem.MasterItem = _componentItem;

                                if (_inventoryRequestItem.Itri_itm_cd == MaincomItem)
                                {
                                    _inventoryRequestItem.MainCompoitm = "MAIN";
                                }
                                else
                                {
                                    _inventoryRequestItem.MainCompoitm = "SUB";
                                }

                              
                                _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = 0;

                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;
                                string _mytype = Request.QueryString["REQTYPE"];

                                var list = new List<InventoryRequestItem>();

                                if (_mytype == "MRN")
                                {
                                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                                }
                                else
                                {
                                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                                }
                                if (list != null && list.Count>0)
                                {
                                   

                                    var _itemcount = list.Where(a => a.Itri_itm_cd == _inventoryRequestItem.Itri_itm_cd && a.Itri_res_no == _inventoryRequestItem.Itri_res_no && a.Itri_itm_stus == _inventoryRequestItem.Itri_itm_stus).ToList();
                                    if (_itemcount != null && _itemcount.Count>0 && _itemcount.First().Itri_itm_cd == _inventoryRequestItem.Itri_itm_cd)
                                    {
                                        list.Where(a => a.Itri_itm_cd == _inventoryRequestItem.Itri_itm_cd && a.Itri_res_no == _inventoryRequestItem.Itri_res_no && a.Itri_itm_stus == _inventoryRequestItem.Itri_itm_stus).Select(c => { c.Itri_qty = c.Itri_qty + _inventoryRequestItem.Itri_qty; return c; }).ToList();
                                        list.Where(a => a.Itri_itm_cd == _inventoryRequestItem.Itri_itm_cd && a.Itri_res_no == _inventoryRequestItem.Itri_res_no && a.Itri_itm_stus == _inventoryRequestItem.Itri_itm_stus).Select(c => { c.Itri_mqty = c.Itri_mqty + _inventoryRequestItem.Itri_mqty; return c; }).ToList();

                                        _invReqItemList = list;
                                        continue;
                                    }
                                    _invReqItemList = list;
                                }

                                Int32 _tmpLineNo = 0;
                                if (_invReqItemList != null)
                                {
                                    if (_invReqItemList.Count > 0)
                                    {
                                        Int32 _maxTmpLine = _invReqItemList.Max(c => c.Itri_line_no_tmp);
                                        _tmpLineNo = _maxTmpLine;
                                    }
                                }
                                _inventoryRequestItem.Itri_line_no_tmp = _tmpLineNo + 1;
                                _invReqItemList.Add(_inventoryRequestItem);
                                if (_mytype == "MRN")
                                {
                                    Session["ITEMLIST_MRN"] = _invReqItemList;
                                    Session["ITEMLIST_INTR"] = null;
                                }
                                else
                                {
                                    Session["ITEMLIST_MRN"] = null;
                                    Session["ITEMLIST_INTR"] = _invReqItemList;
                                }
                            }
                        }
                    }
                }
                else
                {
                    #region Check Consignment Items :: Chamal 07-May-2014
                    if (_invReqItemList != null)
                    {
                        if (_invReqItemList.Count > 0)
                        {
                            if (_itemStatus == "CONS")
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus != _itemStatus).ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please create separate entry for consignment items !!!')", true);
                                    return;
                                }
                            }
                            else
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus == "CONS").ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please create separate entry for consignment items !!!')", true);
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        //if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }


                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtItem.Text.Trim()) && x.Itri_itm_stus == _itemStatus).ToList();


                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        //if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }
                    else
                    {
                        InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                        MasterItem _masterItem = new MasterItem();
                        _masterItem.Mi_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_itm_cd = _mainItemCode;
                        _masterItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _inventoryRequestItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _masterItem.Mi_model = _itemdetail.Mi_model;
                        _inventoryRequestItem.Mi_model = _itemdetail.Mi_model;
                        _masterItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.Mi_brand = _itemdetail.Mi_brand;
                        _masterItem.Mi_itm_uom = _itemdetail.Mi_itm_uom;
                        _inventoryRequestItem.Mi_itm_uom = _itemdetail.Mi_itm_uom;
                        _inventoryRequestItem.MasterItem = _masterItem;

                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_res_no = _reservationNo;
                        if (!string.IsNullOrEmpty(_reservationNo) && _reservationNo != "N/A")
                        {
                            _inventoryRequestItem.Itri_res_qty = _mainItemQty;
                        }
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = 0;

                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;
                        _inventoryRequestItem.Itri_job_no = txtBatch.Text.Trim();
                        string _mytype = Request.QueryString["REQTYPE"];

                        var list = new List<InventoryRequestItem>();

                        if (_mytype == "MRN")
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                        }
                        else
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                        }

                        if (list != null)
                        {
                            _invReqItemList = list;
                        }
                        Int32 _tmpLineNo = 0;
                        if (_invReqItemList != null)
                        {
                            if (_invReqItemList.Count > 0)
                            {
                                Int32 _maxTmpLine = _invReqItemList.Max(c => c.Itri_line_no_tmp);
                                _tmpLineNo = _maxTmpLine;
                            }
                        }
                        _inventoryRequestItem.Itri_line_no_tmp = _tmpLineNo + 1;
                        _invReqItemList.Add(_inventoryRequestItem);
                      //  MasterCompany mstCompany = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());                    

                        if (_mytype == "MRN")
                        {
                            Session["ITEMLIST_MRN"] = _invReqItemList;
                            Session["ITEMLIST_INTR"] = null;
                        }
                        else
                        {
                            Session["ITEMLIST_MRN"] = null;
                            Session["ITEMLIST_INTR"] = _invReqItemList;
                        }
                    }
                }

                ClearLayer2();
                ClearLayer3();

                gvItem.DataSource = new List<InventoryRequestItem>();


                #region Check Duplicate Serials

                var DistinctItems = _invReqItemList.GroupBy(x => x.Itri_itm_cd).Select(y => y.First());
                _invReqItemList = DistinctItems.ToList();

                #endregion

                gvItem.DataSource = _invReqItemList;
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestSubType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select document sub type !!!')", true);
                    ddlRequestSubType.Focus();
                    return;
                }
                if (!string.IsNullOrEmpty(txtReservation.Text))
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        DispMsg("Please enter dispatch location !"); return;
                    }
                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        txtItem.Focus(); DispMsg("Please select the item !"); return;
                    }
                    if (cmbStatus.SelectedValue == "0")
                    {
                        cmbStatus.Focus(); DispMsg("Please select the item status !"); return;
                    }
                    _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                    new INR_RES_LOG()
                                    {
                                        IRL_RES_NO = txtReservation.Text.Trim(),
                                        IRL_ITM_CD = txtItem.Text.Trim(),
                                        IRL_ITM_STUS = cmbStatus.SelectedValue,
                                        IRL_CURT_COM = ddlCompany.SelectedValue,
                                        IRL_CURT_LOC = txtDispatchRequried.Text.Trim().ToUpper(),
                                        IRL_CURT_DOC_TP = "INV",
                                        IRL_CURT_DOC_NO = txtReservation.Text.Trim(),
                                        IRL_ACT = 1
                                    });
                    if (_resLogAvaData.Count < 1)
                    {
                        DispMsg("Invalid reservation number !"); return;
                    }
                    decimal _resAvaQty = _resLogAvaData.Sum(c => c.IRL_RES_BQTY);
                    decimal _tmpDec = 0;
                    //decimal _mrnBalAva = _inrRes.IRD_RES_BQTY + _inrRes.Ird_so_mrn_bqty;
                    decimal _appqty = decimal.TryParse(txtQty.Text, out _tmpDec) ? Convert.ToDecimal(txtQty.Text) : 0;
                    if (_resAvaQty < _appqty)
                    {
                        DispMsg("No reservation balance available !"); return;
                    }

                    INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                    {
                        IRD_RES_NO = txtReservation.Text.Trim(),
                        IRD_ITM_CD = txtItem.Text.Trim(),
                        IRD_ITM_STUS = cmbStatus.SelectedValue
                    }).FirstOrDefault();
                    if (_inrRes == null)
                    {
                        txtReservation.Text = "";
                        DispMsg("Invalid reservation details !"); return;
                    }
                    decimal _resDBal = _inrRes.IRD_MRN_AVA_BAL;
                    if (_resDBal <= 0)
                    {
                        txtReservation.Text = "";
                        DispMsg("Reservation detail balance is not available !"); txtReservation.Text = ""; return;
                    }
                    if (_resDBal < _appqty)
                    {
                        txtReservation.Text = "";
                        DispMsg("Reservation detail balance is not available !"); txtReservation.Text = ""; return;
                    }

                }
                foreach (GridViewRow hiderowbtn in this.gvItem.Rows)
                {
                    Label _itemlbl = (Label)hiderowbtn.FindControl("lblitem2");
                    Label lblstatus2 = (Label)hiderowbtn.FindControl("lblstatus2");

                    if (_itemlbl.Text == txtItem.Text.Trim())
                    {
                        txtItem.Text = string.Empty;
                        string msg = "Item " + _itemlbl.Text + " has been already added !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        return;
                    }
                }

                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the invoice no first !!!')", true);
                        return;
                    }
                    BindInventoryRequestItemsGridData();
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the advance receipt no first !!!')", true);
                        return;
                    }

                    List<ReceiptItemDetails> _recItems = new List<ReceiptItemDetails>();
                    _recItems = CHNLSVC.Sales.GetAdvanReceiptItems(txtInvoice.Text.Trim());
                    Boolean _isRecItem = false;
                    if (_recItems != null)
                    {
                        foreach (ReceiptItemDetails _rItm in _recItems)
                        {
                            if (_rItm.Sari_item == txtItem.Text.Trim())
                            {
                                _isRecItem = true;
                                goto LD01;
                            }
                        }
                    }
                    else
                    {
                        _isRecItem = true;
                    }

                LD01:
                    if (_isRecItem == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Requested item is not in advance receipt !!!')", true);
                        return;
                    }

                    BindInventoryRequestItemsGridData();
                }
                else if (ddlRequestSubType.SelectedValue.ToUpper() == "BOQ" || ddlRequestSubType.SelectedValue.ToUpper() == "ADBOQ")
                {
                    BindInventoryRequestItemsGridDataBoq();
                }
                else
                {
                    BindInventoryRequestItemsGridData();
                }

                foreach (GridViewRow item in gvItem.Rows)
                {
                    Label lblstatus2 = (Label)item.FindControl("lblstatus2");

                    Label lbtnstusdesc = (Label)item.FindControl("lbtnstusdesc");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstatus2.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lbtnstusdesc.Text = ddr2[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_InterTransferNote", txtRequestDate, lblBackDateInfor, txtRequestDate.Text, out _allowCurrentTrans) == false)
            {
                string msg = "Back date is not allowed for selected date for the location " + Session["UserDefLoca"].ToString() + "!!!";

                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtRequestDate.Text) != DateTime.Now.Date)
                    {
                        txtRequestDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        txtRequestDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtRequestDate.Enabled = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    txtRequestDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }
            return _isOK;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsaveconfirm.Value == "Yes")
                {
                    #region Back Date
                    if (IsBackDateOk() == false) return;
                    #endregion

                    if (CheckServerDateTime() == false) return;
                    #region add by lakshan 09Mar2018
                    bool _showPopMsg = false;
                    HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                    if (_sysPara != null)
                    {
                        if (_sysPara.Hsy_val == 1)
                        {
                            decimal _tmpFreeQty = 0, _tmpMrnQty = 0;
                            _tmpFreeQty = decimal.TryParse(txtFreeVal.Text,out _tmpFreeQty)?Convert.ToDecimal(txtFreeVal.Text.Trim()):0;
                            _tmpMrnQty = decimal.TryParse(txtCurrMrnVal.Text, out _tmpMrnQty) ? Convert.ToDecimal(txtCurrMrnVal.Text.Trim()) : 0;
                            if (_tmpFreeQty < _tmpMrnQty && !_isValueExceedSave)
                            {
                                _showPopMsg = true;
                            }
                        }
                    }
                    #endregion
                    if (_showPopMsg)
                    {
                        lblMssg.Text = "Location free value exceed !";
                        lblMssg1.Text = "Do you want to save  ?";
                        PopupConfBox.Show();
                        return;
                    }
                    else
                    {
                        SaveInventoryRequestData();
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

        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(txtRequest.Text))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = txtRequest.Text;

            return _reqNo;
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string _mytype = Request.QueryString["REQTYPE"];
            string moduleText = _mytype;

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private void LoadEditData(int rowIndex)
        {
            try
            {

                ////string _mainItem = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_MainItem"].Value);
                ////string _item = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Item"].Value);
                ////string _status = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Status"].Value);
                ////Int32 _line = Convert.ToInt32(gvItem.Rows[rowIndex].Cells["Itm_No"].Value);

                ////List<InventoryRequestItem> _invRequestItemList = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).ToList();
                ////InventoryRequestItem _inventoryRequestItem = _invRequestItemList[0];

                ////txtItem.Text = _mainItem;

                ////cmbStatus.SelectedValue = _status;

                ////txtReservation.Text = _inventoryRequestItem.Itri_res_no;
                ////txtQty.Text = _inventoryRequestItem.Itri_qty.ToString();
                ////txtItmRemark.Text = _inventoryRequestItem.Itri_note;

                ////_invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
                ////gvItem.DataSource = new List<InventoryRequestItem>();
                ////gvItem.DataSource = _invReqItemList;
                ////gvItem.DataBind();
                ////txtItem.Focus();
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

        private void SaveInventoryRequestData()
        {
            try
            {

                string _mytype = Request.QueryString["REQTYPE"];

                if (CheckServerDateTime() == false) return;

                if (ddlRequestSubType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select document sub type !!!')", true);
                    ddlRequestSubType.Focus();
                    return;
                }

                if (ddlCompany.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the company !!!')", true);
                    ddlCompany.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlRequestSubType.SelectedValue.ToString()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select request type !!!')", true);
                    return;
                }
                if (!chkReqType.Checked)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter dispatch required location !!!')", true);
                        lbtnreqnosearch.Focus();
                        return;
                    }
                }
                else
                {
                    txtDispatchRequried.Text = "N/A";
                }
                if (txtDispatchRequried.Text.ToString().ToUpper() == Session["UserDefLoca"].ToString().ToString().ToUpper())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid dispatch required location !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtRequestDate.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter request date !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtRequriedDate.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter required date !!!')", true);
                    return;
                }

                if (DateTime.Compare(Convert.ToDateTime(txtRequriedDate.Text.Trim()), Convert.ToDateTime(txtRequestDate.Text.Trim())) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Required date cant be less than request date !!!')", true);
                    return;
                }
                if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                {
                    if (string.IsNullOrEmpty(txtBoq.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter BOQ number !')", true);
                        ddlRequestSubType.Focus();
                        return;
                    }
                }
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add items !!!')", true);
                    lbtnaddinvitems.Focus();
                    return;
                }
                //Add By Lakshan 24 Aug 2016 
                if (_invReqItemList != null)
                {
                    if (_invReqItemList.Count > 0)
                    {
                        //new { c.model, c.item_code, c.item_desc, c.brand, c.cat1 }
                        Int32 _groQty = _invReqItemList.GroupBy(c => new { c.Itri_res_no, c.Itri_itm_stus, c.Itri_itm_cd }).ToList().Count;
                        if (_groQty != _invReqItemList.Count)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item data duplicated !!!')", true);
                            return;
                        }
                    }
                }

                //End
                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Invoice No !!!')", true);
                        return;
                    }

                    decimal _curQty = 0;
                    decimal _invQty = 0;
                    decimal _reqQty = 0;
                    foreach (InventoryRequestItem _tmpItm in _invReqItemList)
                    {
                        _curQty = _tmpItm.Itri_qty;

                        DataTable dt = CHNLSVC.Sales.GetInvItemQty(txtInvoice.Text.Trim(), _tmpItm.Itri_itm_cd);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!string.IsNullOrEmpty(drow["inv_qty"].ToString()))
                                {
                                    _invQty = _invQty + Convert.ToDecimal(drow["inv_qty"]);
                                }
                            }
                        }

                        DataTable dt1 = CHNLSVC.Sales.GetReqItemQty(_mytype, "SALES", txtInvoice.Text.Trim(), _tmpItm.Itri_itm_cd);

                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow drow1 in dt1.Rows)
                            {
                                if (!string.IsNullOrEmpty(drow1["reqQty"].ToString()))
                                {
                                    _reqQty = _reqQty + Convert.ToDecimal(drow1["reqQty"]);
                                }
                            }
                        }

                        if (_invQty < (_reqQty + _curQty))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to request more than the invoice quantity !!!')", true);
                            return;
                        }
                    }

                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Receipt No !!!')", true);
                        return;
                    }
                }

                int _count = 1;

                if (_invReqItemList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add items to list !!!')", true);
                    txtItem.Focus();
                    return;
                }

                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add items to list !!!')", true);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNIC.Text.Trim()))
                {
                    txtNIC.Text = string.Empty;
                }
                else
                {
                    if (IsValidNIC(txtNIC.Text.ToString()) == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid NIC number !!!')", true);
                        txtNIC.Focus();
                        return;
                    }
                }

                if (_mytype != "MRN")
                {
                    if (string.IsNullOrEmpty(txtNIC.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter collector NIC !!!')", true);
                        txtNIC.Focus();
                        return;
                    }
                    else
                    {
                        if (IsValidNIC(txtNIC.Text.ToString()) == false)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid NIC number !!!')", true);
                            txtNIC.Focus();
                            return;
                        }
                    }
                }

                if (_mytype != "MRN")
                {
                    if (string.IsNullOrEmpty(txtCollecterName.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter collector name !!!')", true);
                        txtCollecterName.Focus();
                        return;
                    }
                }

                string _masterLocation = IsRecalled ? _recallloc : (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                InventoryRequest _inventoryRequest = new InventoryRequest();

                _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = _mytype;
                _inventoryRequest.Itr_sub_tp = Convert.ToString(ddlRequestSubType.SelectedValue);
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);

                if (chkReqType.Checked == true && chkReqType.Visible == true)
                {
                    _inventoryRequest.Itr_stus = "A";
                }
                else
                {
                    _inventoryRequest.Itr_stus = AutoApprove == false ? "P" : "A";
                }
                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    _inventoryRequest.Itr_job_no = txtInvoice.Text;
                    _inventoryRequest.Itr_bus_code = lblCusCode.Text;
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    _inventoryRequest.Itr_job_no = txtInvoice.Text;
                    _inventoryRequest.Itr_bus_code = lblCusCode.Text;
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "DISP")
                {
                    _inventoryRequest.Itr_job_no = "DISP";
                }
                else if ((ddlRequestSubType.SelectedValue.ToString().ToUpper() == "BOQ") || (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "PRO")
                    || (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADBOQ"))
                {
                    _inventoryRequest.Itr_job_no = txtBoq.Text;
                }
                _inventoryRequest.Itr_note = txtRemark.Text;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;
                _inventoryRequest.Itr_town_cd = string.Empty;
                _inventoryRequest.Itr_cur_code = string.Empty;
                _inventoryRequest.Itr_exg_rate = 0;
                _inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                _inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
                _inventoryRequest.Itr_mod_by = Session["UserID"].ToString();
                _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();
                _inventoryRequest.Itr_issue_com = ddlCompany.SelectedValue.ToString();

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                if (AutoApprove) _inventoryRequest.InventoryRequestItemList.ForEach(x => x.Itri_app_qty = x.Itri_qty);
                int rowsAffected = 0;
                string _docNo = string.Empty;

                #region validate reservation data available 07 Nov 2016 Lakshan
                bool _resAva = false;
                foreach (var reqItm in _inventoryRequest.InventoryRequestItemList)
                {
                    _resAva = false;
                    if (!string.IsNullOrEmpty(reqItm.Itri_res_no))
                    {
                        if (reqItm.Itri_res_no != "N/A")
                        {
                            _resAva = true;
                        }
                    }
                    if (_resAva)
                    {
                        _inventoryRequest.Temp_is_res_request = true;
                        _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                     new INR_RES_LOG()
                                     {
                                         IRL_RES_NO = reqItm.Itri_res_no,
                                         IRL_ITM_CD = reqItm.Itri_itm_cd,
                                         IRL_ITM_STUS = reqItm.Itri_itm_stus,
                                         IRL_CURT_COM = reqItm.Itri_com,
                                         IRL_CURT_LOC = reqItm.Itri_loc,
                                         IRL_CURT_DOC_TP = "INV",
                                         IRL_CURT_DOC_NO = reqItm.Itri_res_no,
                                         IRL_ACT = 1
                                     });
                        if (_resLogAvaData.Count < 1)
                        {
                            DispMsg("Invalid reservation number !"); return;
                        }
                        decimal _resAvaQty = _resLogAvaData.Sum(c => c.IRL_RES_BQTY);
                        decimal _tmpDec = 0;
                        decimal _appqty = decimal.TryParse(txtQty.Text, out _tmpDec) ? Convert.ToDecimal(txtQty.Text) : 0;
                        if (_resAvaQty < _appqty)
                        {
                            DispMsg("No reservation balance available !"); return;
                        }
                        INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                        {
                            IRD_RES_NO = reqItm.Itri_res_no,
                            IRD_ITM_CD = reqItm.Itri_itm_cd,
                            IRD_ITM_STUS = reqItm.Itri_itm_stus
                        }).FirstOrDefault();
                        decimal _resDBal = _inrRes.IRD_MRN_AVA_BAL;
                        if (_resDBal <= 0)
                        {
                            txtReservation.Text = "";
                            DispMsg("Reservation detail balance is not available !"); txtReservation.Text = ""; return;
                        }
                        if (_appqty > _resDBal)
                        {
                            txtReservation.Text = "";
                            DispMsg("Reservation detail balance is invalid !"); txtReservation.Text = ""; return;
                        }
                    }
                }
                #endregion

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    // Int32 _countItem=_inventoryRequest
                    _inventoryRequest.Itr_system_module = "MRN";
                    #region add auto approve by lakshan 12Jan2018
                    if (_inventoryRequest.Itr_sub_tp == "PRO" && _inventoryRequest.Itr_tp == "MRN")
                    {
                        HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNAUTOAPP", DateTime.Now.Date);
                        if (_sysPara != null)
                        {
                            if (_sysPara.Hsy_val == 1)
                            {
                                _inventoryRequest.Itr_stus = "A";
                                foreach (var _reqItm in _inventoryRequest.InventoryRequestItemList)
                                {
                                    _reqItm.Itri_app_qty = _reqItm.Itri_qty;
                                }
                            }
                        }
                    }
                    #endregion
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);

                    if (rowsAffected != -1)
                    {
                        string Msg = "Inventory Request Document Successfully saved. " + _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);

                        ClearAll();

                        if (AutoApprove || (chkReqType.Checked == true && chkReqType.Visible == true))
                        {
                            //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            //Session["GlbReportName"] = string.Empty;
                            //GlbReportName = string.Empty;
                            //_view.GlbReportName = string.Empty;
                            //_view.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                            //_view.GlbReportDoc = _docNo;
                            //_view.Show();
                            //_view = null;
                        }
                    }
                    else
                    {
                        if (_docNo.Contains("Reservation"))
                        {
                            DispMsg(_docNo); return;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                }
                else
                {
                    _inventoryRequest.Itr_system_module = "MRN";
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    if (rowsAffected != -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Inventory Request Document Successfully Updated')", true);
                    }
                    else
                    {
                        if (_docNo.Contains("Reservation"))
                        {
                            DispMsg(_docNo); return;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                }

                InitVariables();
                LoadCachedObjects();
                InitializeForm(true);
                ddlRequestSubType.SelectedIndex = 3;
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

        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                gvItem.EditIndex = grdr.RowIndex;

                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                }

                gvItem.DataSource = _invReqItemList;
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
                if (string.IsNullOrEmpty(txtRequest.Text) && string.IsNullOrEmpty(txtBoq.Text))
                {
                    Label lblHdrLocBal = gvItem.HeaderRow.FindControl("lblHdrLocBal") as Label;
                    Label lblHdrDisLocBal = gvItem.HeaderRow.FindControl("lblHdrDisLocBal") as Label;
                    Label lblqtyExists = grdr.FindControl("secondlblqty2") as Label;
                    ViewState["qtyExists"] = Convert.ToDecimal(lblqtyExists.Text);
                    lblHdrLocBal.Text = "Bal (" + hdfLoc.Value.ToString() + ")";
                    lblHdrDisLocBal.Text = "Bal (" + hdfDisLoc.Value.ToString() + ")";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            if (!CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV3"))
            {
                if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ABBOQ")
                {
                    if (!string.IsNullOrEmpty(txtRequest.Text))
                    {
                        InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = txtRequest.Text }).FirstOrDefault();
                        if (_invReq != null)
                        {
                            if (_invReq.Itr_stus == "A")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have no permission to approve !!!')", true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have no permission to approve !!!')", true);
                    return;
                }

            }
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string qty = (row.FindControl("lblqty2") as TextBox).Text;
                    string reservation = (row.FindControl("lblres2") as TextBox).Text;
                    string remarks = (row.FindControl("lblremarks2") as TextBox).Text;
                    string itemcode = (row.FindControl("lblitem2") as Label).Text;
                    string resno = (row.FindControl("lblres2") as TextBox).Text;
                    string status = (row.FindControl("lblstatus2") as Label).Text;
                    string _appRemark = (row.FindControl("txtAppRem") as TextBox).Text;

                    if (string.IsNullOrEmpty(qty))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a quantity !!!')", true);
                        return;
                    }

                    if (!IsNumeric(qty, NumberStyles.Float))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                        return;
                    }

                    //if (Convert.ToDecimal(qty) == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity !!!')", true);
                    //    return;
                    //}

                    if (Convert.ToDecimal(qty) < 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                        return;
                    }

                    if (!string.IsNullOrEmpty(resno))
                    {
                        DataTable _dt = CHNLSVC.Inventory.GetReservationDet(Session["UserCompanyCode"].ToString(), resno, itemcode, status);
                        if (_dt.Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid reservation number !!!')", true);
                            resno = string.Empty;
                            return;
                        }
                    }

                    if (remarks.Length > 200)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Maximum characters allowed are 200.Extra characters have been removed !!!')", true);
                        return;
                    }

                    decimal _qty = Convert.ToDecimal(qty);
                    Label lblTmp_base_doc_bal = row.FindControl("lblTmp_base_doc_bal") as Label;
                    Label lblItri_base_req_no = row.FindControl("lblItri_base_req_no") as Label;
                    decimal _boqQty = Convert.ToDecimal(lblTmp_base_doc_bal.Text);
                    if (!string.IsNullOrEmpty(lblItri_base_req_no.Text))
                    {
                        if (_boqQty < _qty)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Base document balance exceed !')", true);
                            return;
                        }
                    }
                    gvItem.EditIndex = -1;

                    string _mytype = Request.QueryString["REQTYPE"];
                    var list = new List<InventoryRequestItem>();

                    if (_mytype == "MRN")
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                    }
                    else
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                    }

                    if (list != null)
                    {
                        _invReqItemList = list;
                    }
                    if (_mytype == "MRN")
                    {
                        Label lblItri_line_no_tmp = row.FindControl("lblItri_line_no_tmp") as Label;
                        Int32 _lineNo = Convert.ToInt32(lblItri_line_no_tmp.Text);
                        var _req = _invReqItemList.Where(c => c.Itri_line_no_tmp == _lineNo).FirstOrDefault();
                        if (_req != null)
                        {
                            _req.Itri_qty = _qty;
                            // _req.Itri_app_qty = _qty;
                            _req.Itri_bqty = _qty;
                            _req.Itri_cncl_qty = 0;
                            _req.Itri_res_no = reservation;
                            _req.Itri_note = remarks;
                            _req.Tmp_Itri_app_rem = _appRemark;
                        }
                    }
                    else
                    {
                        foreach (var item in _invReqItemList)
                        {
                            if (itemcode == item.Itri_itm_cd)
                            {
                                if (string.IsNullOrEmpty(txtRequest.Text))
                                {
                                    item.Itri_qty = _qty;
                                }
                                else
                                {
                                    item.Itri_qty = Convert.ToDecimal(ViewState["qtyExists"].ToString()); //_qty;
                                }
                                item.Itri_app_qty = _qty;
                                item.Itri_bqty = _qty;
                                item.Itri_cncl_qty = 0;
                                item.Itri_res_no = reservation;
                                item.Itri_note = remarks;
                                item.Tmp_Itri_app_rem = _appRemark;
                            }
                        }
                    }


                    gvItem.DataSource = _invReqItemList;
                    gvItem.DataBind();
                    LoadGridUnitCost(_invReqItemList);
                    if (!string.IsNullOrEmpty(txtRequest.Text))
                    {
                        Label lblHdrLocBal = gvItem.HeaderRow.FindControl("lblHdrLocBal") as Label;
                        Label lblHdrDisLocBal = gvItem.HeaderRow.FindControl("lblHdrDisLocBal") as Label;
                        lblHdrLocBal.Text = "Bal (" + hdfLoc.Value.ToString() + ")";
                        lblHdrDisLocBal.Text = "Bal (" + hdfDisLoc.Value.ToString() + ")";
                    }
                    if (!string.IsNullOrEmpty(txtKitCode.Text))
                    {
                        txtKitCode_TextChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                gvItem.EditIndex = -1;

                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                }

                gvItem.DataSource = _invReqItemList;
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtDispatchRequried.Text = string.Empty;
                Session["DISLOC"] = null;
                txtDispatchRequried.ToolTip = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtapprove.Value == "Yes")
                {
                    string _mytype = Request.QueryString["REQTYPE"];
                    _userid = (string)Session["UserID"];

                    if (_mytype == "MRN")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('MRN cannot be updated !!!')", true);
                        return;
                    }

                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                    if (!CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV3"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have no permission to approve !!!')", true);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtRequest.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select request before Approve !!!')", true);
                        return;
                    }

                    string ordstatus = (string)Session["STATUS"];

                    if (ordstatus == "A" || ordstatus == "F" || ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cant approve this request !!!')", true);
                        return;
                    }
                    InventoryRequest _inputInvReq = new InventoryRequest();
                    _inputInvReq.Itr_com = Session["UserCompanyCode"].ToString();
                    _inputInvReq.Itr_loc = IsRecalled ? _recallloc : Session["UserDefLoca"].ToString();
                    _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                    _inputInvReq.Itr_stus = "A";
                    _inputInvReq.Itr_mod_by = _userid;
                    _inputInvReq.Itr_session_id = Session["SessionID"].ToString();
                    _inputInvReq.Itr_gran_app_by = _userid;
                    //Add by Lakshan
                    _inputInvReq.Itr_note = txtRemark.Text.Trim() + ";" + txtAppRemark.Text.Trim();

                    List<InventoryRequestItem> list = new List<InventoryRequestItem>();
                    if (_mytype == "MRN")
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                    }
                    else
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                    }
                    #region add validation for allocation data chk 09 Jan 2016
                    InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _inputInvReq.Itr_req_no }).FirstOrDefault();
                    MasterLocationPriorityHierarchy _locHir = CHNLSVC.General.GET_MST_LOC_INFO_DATA(_invReq.Itr_loc, "CHNL");
                    MasterLocationPriorityHierarchy _locHirUsrLoc = CHNLSVC.General.GET_MST_LOC_INFO_DATA(_invReq.Itr_issue_from, "CHNL");
                    if (_locHir == null)
                    {
                        DispMsg("Please check the channel of the location !"); return;
                    }
                    if (_locHirUsrLoc == null)
                    {
                        DispMsg("Please check the channel of the location !"); return;
                    }
                    if (_locHirUsrLoc.Mli_val != _locHir.Mli_val)
                    {
                        List<InventoryRequestItem> _allocationErrList = CHNLSVC.Inventory.AllocationValidateInterTransferApprove(_inputInvReq, list);
                        if (_allocationErrList.Count > 0)
                        {
                            DispMsg(_allocationErrList[0].Itri_itm_stus_desc);
                            return;
                        }
                    }
                    #endregion
                    string _saveErro = "";
                    int result = CHNLSVC.Inventory.UpdateInventoryRequestStatusWithNote(_inputInvReq, list, out _saveErro);
                    if (result > 0)
                    {
                        string Msg = "Inventory Request " + _inputInvReq.Itr_req_no + " Successfully Approved !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                        ClearAll();
                        //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //Session["GlbReportName"] = string.Empty;
                        //GlbReportName = string.Empty;
                        //_view.GlbReportName = string.Empty;
                        //_view.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                        //_view.GlbReportDoc = txtRequest.Text;
                        //_view.Show();
                        //_view = null;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_saveErro))
                        {
                            DispMsg(_saveErro); return;
                        }
                        else
                        {
                            string msg = "Inventory Request " + _inputInvReq.Itr_req_no + " can't be Approved !";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
                            return;
                        }
                    }
                    ClearAll();
                    
                    LoadCachedObjects();
                   
                    ddlRequestSubType.SelectedIndex = 3;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcancel.Value == "Yes")
                {
                    _userid = (string)Session["UserID"];

                    if (string.IsNullOrEmpty(txtRequest.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select request before cancel !!!')", true);
                        return;
                    }

                    if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
                    {
                        bool b10155 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10155);
                        if (!b10155)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request date should be current date in order to Cancel. Permission Code : 10155 ')", true);
                            return;
                        }
                    }

                    string ordstatus = (string)Session["STATUS"];

                    string isapproveduser = (string)Session["isApprovedUser"];

                    if (isapproveduser == "true")
                    {
                        _isApprovedUser = true;
                    }
                    else
                    {
                        _isApprovedUser = false;
                    }

                    if ((ordstatus == "A" || ordstatus == "F" || ordstatus == "C") && (_isApprovedUser == false))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to cancell the request !!!')", true);
                        return;
                    }

                    InventoryRequest _inputInvReq = new InventoryRequest();
                    _inputInvReq.Itr_com = Session["UserCompanyCode"].ToString();
                    _inputInvReq.Itr_loc = IsRecalled ? _recallloc : Session["UserDefLoca"].ToString();
                    _inputInvReq.Itr_req_no = txtRequest.Text;
                    _inputInvReq.Itr_stus = "C";
                    _inputInvReq.Itr_cre_by = _userid;
                    _inputInvReq.Itr_mod_by = _userid;
                    _inputInvReq.Itr_session_id = Session["SessionID"].ToString();
                    InventoryRequest _tmpInvReq = new InventoryRequest();
                    _tmpInvReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = txtRequest.Text.Trim() }).FirstOrDefault();
                    if (_tmpInvReq != null)
                    {
                        int result = 0;
                        if (_tmpInvReq.Itr_tp == "INTR")
                        {
                            result = CHNLSVC.Inventory.CancelIntertransferDocument(_inputInvReq);
                        }
                        else
                        {
                            result = CHNLSVC.Inventory.CancelMaterialRequestNote(_inputInvReq);
                        }

                        if (result > 0)
                        {
                            string Msg = "Inventory Request " + _inputInvReq.Itr_req_no + " successfully Cancelled !!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                            LoadCachedObjects();
                            ClearAll();
                            ddlRequestSubType.SelectedIndex = 3;
                        }
                        else
                        {
                            string msg = "Inventory Request " + _inputInvReq.Itr_req_no + " can't be Cancel !!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "')", true);
                            return;
                        }
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

        protected void lbtnaddinvitems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the dispatch location !!!');", true);
                    lbtnreqnosearch.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the invoice number !!!');", true);
                    LinkButton2.Focus();
                    return;
                }

                if (gvInvoice.Rows.Count > 0)
                {
                    Label _itemlbl = new Label();
                    Label _itmdesc = new Label();
                    Label _itmmodel = new Label();
                    Label _itmstatus = new Label();
                    Label _itmqty = new Label();
                    foreach (GridViewRow hiderowbtn in this.gvInvoice.Rows)
                    {
                        _itemlbl = (Label)hiderowbtn.FindControl("lblitem");
                        _itmdesc = (Label)hiderowbtn.FindControl("lbldesc");
                        _itmmodel = (Label)hiderowbtn.FindControl("lblmodel");
                        _itmstatus = (Label)hiderowbtn.FindControl("lblstatus");
                        _itmqty = (Label)hiderowbtn.FindControl("lblqty");
                    }

                    string _item = _itemlbl.Text;
                    string _description = _itmdesc.Text;
                    string _brand = string.Empty;
                    string _model = _itmmodel.Text;
                    string _status = _itmstatus.Text;
                    decimal _qty = Convert.ToDecimal(_itmqty.Text);
                    string _reservation = string.Empty;
                    string _remarks = string.Empty;

                    if (!string.IsNullOrEmpty(txt_cashGiven.Text.Trim()))
                    {
                        if (_qty > 0)
                        {
                            string msg = "Quantity is already available for item " + _item + "";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            return;
                        }
                    }

                    if (_qty == 1)
                    {
                        _invReqItemList = new List<InventoryRequestItem>();
                        decimal _alreadyAvailables = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).Sum(y => y.Itri_qty);
                        if (_alreadyAvailables != 0)
                        {
                            decimal _currentAdded = _alreadyAvailables;
                            decimal _wannaAdd = _qty;
                            if (_currentAdded + _wannaAdd > _qty)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to request more than the invoice quantity !!!')", true);
                                return;
                            }
                        }

                        txtItem.Text = _item;
                        CheckItemCode(null, null);
                        cmbStatus.SelectedValue = _status;
                        txtQty.Text = DoFormat(Convert.ToDecimal(_qty));
                        btnAddItem.Focus();
                        BindInventoryRequestItemsGridData();
                        return;
                    }

                    txt_cashGiven.Focus();
                    string _cashGiven = txt_cashGiven.Text.Trim();
                    if (string.IsNullOrEmpty(_cashGiven))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have not selected any qty amount. Item adding terminated !!!')", true);
                        return;
                    }

                    if (Convert.ToDecimal(_cashGiven) > _qty)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to enter more than the invoice quantity !!!')", true);
                        return;
                    }

                    decimal _alreadyAvailable = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).Sum(y => y.Itri_qty);
                    if (_alreadyAvailable != 0)
                    {
                        decimal _currentAdded = _alreadyAvailable;
                        decimal _wannaAdd = Convert.ToDecimal(_cashGiven);
                        if (_currentAdded + _wannaAdd > _qty)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allow to enter more than invoice qty !!!')", true);
                            return;
                        }
                    }

                    txtItem.Text = _item;
                    CheckItemCode(null, null);
                    cmbStatus.SelectedValue = _status;
                    txtQty.Text = DoFormat(Convert.ToDecimal(_cashGiven));
                    btnAddItem.Focus();
                    BindInventoryRequestItemsGridData();
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

        protected void gvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        protected void lbtngrdInvoiceDetailsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {
                        string _mainItem = (row.FindControl("lblmainitem2") as Label).Text;
                        string _item = (row.FindControl("lblitem2") as Label).Text;
                        string _status = (row.FindControl("lblstatus2") as Label).Text;

                        string _mytype = Request.QueryString["REQTYPE"];
                        var list = new List<InventoryRequestItem>();

                        if (_mytype == "MRN")
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                        }
                        else
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                        }

                        if (list != null)
                        {
                            _invReqItemList = list;
                        }
                        if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                        {
                            var _reqItm = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).FirstOrDefault();
                            if (_reqItm != null)
                            {
                                _invReqItemList.Remove(_reqItm);
                            }
                        }
                        else
                        {

                            List<MasterItemComponent> _newItemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItem);
                            if (_newItemComponentList != null && _newItemComponentList.Count>0)
                            {
                                if (_item + "C" != _mainItem)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Main Item ')", true);
                                    return;
                                }
                                decimal mainitemqty = list.Where(a => a.Itri_itm_cd+"C" == _mainItem).Max(a => a.Itri_qty);
                                foreach (var listtt in _newItemComponentList)
                                {
                                    list.Where(a => a.Itri_itm_cd == listtt.ComponentItem.Mi_cd).Select(c => { c.Itri_qty = c.Itri_qty - mainitemqty; return c; }).ToList();
                                    list.Where(a => a.Itri_itm_cd == listtt.ComponentItem.Mi_cd).Select(c => { c.Itri_mqty = c.Itri_mqty - mainitemqty; return c; }).ToList();
                                }
                                _invReqItemList.RemoveAll(x => x.Itri_qty==0);
                            }
                            else
                            {
                                _invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
                            }
                          

                           
                        }
                        gvItem.DataSource = new List<InventoryRequestItem>();
                        gvItem.DataSource = _invReqItemList;
                        gvItem.DataBind();
                        LoadGridUnitCost(_invReqItemList);
                        if (!string.IsNullOrEmpty(txtRequest.Text))
                        {
                            Label lblHdrLocBal = gvItem.HeaderRow.FindControl("lblHdrLocBal") as Label;
                            Label lblHdrDisLocBal = gvItem.HeaderRow.FindControl("lblHdrDisLocBal") as Label;
                            lblHdrLocBal.Text = "Bal (" + hdfLoc.Value.ToString() + ")";
                            lblHdrDisLocBal.Text = "Bal (" + hdfDisLoc.Value.ToString() + ")";
                        }
                        if (_mytype == "MRN")
                        {
                            Session["ITEMLIST_MRN"] = _invReqItemList;
                            Session["ITEMLIST_INTR"] = null;
                        }
                        else
                        {
                            Session["ITEMLIST_MRN"] = null;
                            Session["ITEMLIST_INTR"] = _invReqItemList;
                        }
                        if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                        {
                            if (!string.IsNullOrEmpty(txtKitCode.Text))
                            {
                                txtKitCode_TextChanged(null, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                if (!CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "INV3"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have no permission to execute bulk cancel !!!')", true);
                    return;
                }

                bool b10154 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10154);
                if (!b10154)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have no permission to execute bulk cancel : 10154 ')", true);
                    return;
                }
                dgvPromo.DataSource = null;
                dgvPromo.DataBind();
                txtLoc.Text = string.Empty;
                cmbSearchStatus.SelectedIndex = 0;
                ddlSearchReason.SelectedIndex = 0;
                Session["BULK_CANCEL"] = "1";
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

        protected void btnUserLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
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

        protected void ddlSearchReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        protected void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                string _status = "";
                string _subTp = "";
                if (cmbSearchStatus.SelectedIndex != 0)
                {
                    if (cmbSearchStatus.Text == "APPROVED")
                    {
                        _status = "A";
                    }
                    else if (cmbSearchStatus.Text == "PENDING")
                    {
                        _status = "P";
                    }
                }
                else
                {
                    _status = "";
                }

                if (chkAll.Checked == true)
                {
                    _subTp = "";
                }
                else if (ddlSearchReason.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select reason !!!')", true);
                    ddlSearchReason.Focus();
                    MpDelivery.Show();
                    return;
                }
                else
                {
                    _subTp = Convert.ToString(ddlSearchReason.SelectedValue);
                }

                DataTable _tmpReq = new DataTable();
                DataTable _newReq = new DataTable();
                DataRow dr;

                _newReq.Columns.Add("ITR_REQ_NO", typeof(string));
                _newReq.Columns.Add("ITR_LOC", typeof(string));
                _newReq.Columns.Add("STATUS", typeof(string));
                _newReq.Columns.Add("ITR_DT", typeof(DateTime));
                _newReq.Columns.Add("MSTP_DESC", typeof(string));

                string _userid = (string)Session["UserID"];
                string _mytype = Request.QueryString["REQTYPE"];
                _userid = txtLoc.Text.Trim();

                _tmpReq = CHNLSVC.Inventory.Get_InterTrans_Req(Session["UserCompanyCode"].ToString(), _userid, _mytype, _status, Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), _subTp);

                foreach (DataRow drow in _tmpReq.Rows)
                {
                    DataTable _tempReq = CHNLSVC.Sales.GetinvBatch(drow["ITR_REQ_NO"].ToString());

                    if (_tempReq.Rows.Count == 0)
                    {
                        dr = _newReq.NewRow();
                        dr["ITR_REQ_NO"] = drow["ITR_REQ_NO"].ToString();
                        dr["ITR_LOC"] = drow["ITR_LOC"].ToString();
                        dr["STATUS"] = drow["STATUS"].ToString();
                        dr["ITR_DT"] = drow["ITR_DT"];
                        dr["MSTP_DESC"] = drow["MSTP_DESC"].ToString();
                        _newReq.Rows.Add(dr);
                    }
                }

                dgvPromo.AutoGenerateColumns = false;

                dgvPromo.DataSource = null;
                dgvPromo.DataBind();

                if ((string.IsNullOrEmpty(txtLoc.Text.Trim()) && (cmbSearchStatus.SelectedIndex == 0)))
                {
                    uniqueCols = RemoveDuplicateRows(_newReq, "ITR_REQ_NO");
                    dgvPromo.DataSource = uniqueCols;
                    dgvPromo.DataBind();
                }
                else if ((string.IsNullOrEmpty(txtLoc.Text.Trim()) && (cmbSearchStatus.SelectedIndex != 0)))
                {
                    uniqueCols = RemoveDuplicateRows(_newReq, "ITR_REQ_NO");

                    DataView dv = new DataView(uniqueCols);
                    string filtervaluestatus = cmbSearchStatus.SelectedItem.Text;
                    dv.RowFilter = "STATUS = '" + filtervaluestatus + "'";

                    dgvPromo.DataSource = dv;
                    dgvPromo.DataBind();
                }
                else if ((!string.IsNullOrEmpty(txtLoc.Text.Trim()) && (cmbSearchStatus.SelectedIndex != 0)))
                {
                    uniqueCols = RemoveDuplicateRows(_newReq, "ITR_REQ_NO");

                    DataView dv2 = new DataView(uniqueCols);
                    string filtervaluestatus = cmbSearchStatus.SelectedValue;
                    string filterloc = txtLoc.Text.Trim();
                    dv2.RowFilter = "STATUS = '" + filtervaluestatus + "' AND ITR_LOC = '" + filterloc + "'";

                    dgvPromo.DataSource = dv2;
                    dgvPromo.DataBind();
                }
                else if ((!string.IsNullOrEmpty(txtLoc.Text.Trim()) && (cmbSearchStatus.SelectedIndex == 0)))
                {
                    uniqueCols = RemoveDuplicateRows(_newReq, "ITR_REQ_NO");

                    DataView dv3 = new DataView(uniqueCols);
                    string filtervalueloc2 = txtLoc.Text.Trim();
                    dv3.RowFilter = "ITR_LOC = '" + filtervalueloc2 + "'";

                    dgvPromo.DataSource = dv3;
                    dgvPromo.DataBind();
                }
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

        protected void lbtndclear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcancel.Value == "Yes")
                {
                    Boolean _appPromo = false;
                    InventoryRequest _inputInvReq = new InventoryRequest();
                    List<InventoryRequest> _ReqCancelDet = new List<InventoryRequest>();

                    CheckBox col_p_Get = new CheckBox();
                    foreach (GridViewRow hiderowbtn in this.dgvPromo.Rows)
                    {
                        col_p_Get = (CheckBox)hiderowbtn.FindControl("col_p_Get");

                        if (col_p_Get.Checked == true)
                        {
                            _appPromo = true;
                            goto L4;
                        }
                        else
                        {
                            _appPromo = false;
                        }
                    }

                    if (_appPromo == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No any request is selected for cancel !!!')", true);
                        MpDelivery.Show();
                        return;
                    }

                L4:

                    CheckBox col_p_Getprocess = new CheckBox();

                    foreach (GridViewRow row in this.dgvPromo.Rows)
                    {
                        col_p_Getprocess = (CheckBox)row.FindControl("col_p_Get");

                        if (Convert.ToBoolean(col_p_Getprocess.Checked) == true)
                        {
                            Label location = new Label();
                            location = (Label)row.FindControl("lbllocationcancel");

                            Label reqno = new Label();
                            reqno = (Label)row.FindControl("lblreqnoblk");

                            _inputInvReq = new InventoryRequest();

                            _inputInvReq.Itr_com = Session["UserCompanyCode"].ToString();
                            _inputInvReq.Itr_loc = location.Text;
                            _inputInvReq.Itr_req_no = reqno.Text;
                            _inputInvReq.Itr_stus = "C";
                            _inputInvReq.Itr_mod_by = Session["UserID"].ToString();
                            _inputInvReq.Itr_session_id = Session["SessionID"].ToString();
                            _ReqCancelDet.Add(_inputInvReq);
                        }
                    }

                    //int result = CHNLSVC.Inventory.UpdateInventoryRequestStatusBulk(_ReqCancelDet);
                    int result = CHNLSVC.Inventory.CancelIntertransferDocumentBulk(_ReqCancelDet);

                    if (result > 0)
                    {
                        string Msg = "Successfully cancelled !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                        InitVariables();
                        LoadCachedObjects();
                        InitializeForm(true);
                        ddlRequestSubType.SelectedIndex = 3;
                        txtLoc.Text = string.Empty;
                        cmbSearchStatus.SelectedIndex = 0;
                        ddlSearchReason.SelectedIndex = 0;
                        dgvPromo.DataSource = null;
                        dgvPromo.DataBind();

                        MpDelivery.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        MpDelivery.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                MpDelivery.Show();
            }
            finally
            {
                CHNLSVC.CloseChannel();
                MpDelivery.Show();
            }
        }

        protected void lbtndconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox col_p_Get = new CheckBox();
                foreach (GridViewRow hiderowbtn in this.dgvPromo.Rows)
                {
                    col_p_Get = (CheckBox)hiderowbtn.FindControl("col_p_Get");
                    col_p_Get.Checked = true;
                }
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtndreset_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox col_p_Get = new CheckBox();
                foreach (GridViewRow hiderowbtn in this.dgvPromo.Rows)
                {
                    col_p_Get = (CheckBox)hiderowbtn.FindControl("col_p_Get");
                    col_p_Get.Checked = false;
                }
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtndcancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtLoc.Text = "";
                dgvPromo.DataSource = null;
                dgvPromo.DataBind();
                ddlSearchReason.SelectedIndex = 0;
                txtLoc.Text = string.Empty;
                cmbSearchStatus.SelectedIndex = 0;
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtDispatchRequried_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    return;
                }
                if (txtDispatchRequried.Text.Trim() == Session["UserDefLoca"].ToString())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not enter same location which you already logged !!!')", true);
                    txtDispatchRequried.Text = string.Empty;
                    return;
                }

                if (!chkReqType.Checked)
                {
                    if (IsValidLocation() == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid location code or permission not allow for the selected location !!!')", true);
                        txtDispatchRequried.Text = string.Empty;
                        txtDispatchRequried.Focus();
                        return;
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

        protected void cmbSearchStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void defbtn_Click(object sender, EventArgs e)
        {
            try
            {
                CheckItemCode(null, null);
                cmbStatus.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckItemCode(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SIPopup.Hide();
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            try
            {
                Session["BULK_CANCEL"] = null;
                this.Page_Load(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtRequest_TextChanged(object sender, EventArgs e)
        {
            txtRequest_Leave(null, null);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["RequestNo"] = txtRequest.Text.Trim();

                if (Session["RequestNo"].ToString() == "")
                {
                    string Msg = "Please Select Request No ";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "')", true);
                }
                else
                {
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "RequistionNote.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.RequistionNote(Session["UserCompanyCode"].ToString(), Session["RequestNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString(), ddlCompany.SelectedItem.Text);
                    PrintPDF(targetFileName, obj._ReqestionNote);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Material Requisition Note Print", "MaterialRequisitionNote", ex.Message, Session["UserID"].ToString());
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
        protected void lbtnProCode_Click(object sender, EventArgs e)
        {
            if (ddlRequestSubType.SelectedValue == "ADBOQ" || ddlRequestSubType.SelectedValue == "BOQ")
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BOQ);
                DataTable result = CHNLSVC.CommonSearch.SearchBoqProNoInMrn(SearchParams, null, null);
                if (result.Rows.Count > 0)
                {
                    result = result.AsEnumerable()
                                     .Where(r => r.Field<string>("STATUS") == "APPROVED")
                                     .CopyToDataTable();
                }
                grdResult.DataSource = result;
                ViewState["SEARCH"] = result;
                grdResult.DataBind();
                lblvalue.Text = "boq";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                SIPopup.Show();
            }

            else if (ddlRequestSubType.SelectedValue == "PRO")
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                //DataTable result = CHNLSVC.General.SearchBOQDocNo(SearchParams, null, null);
                DataTable result = CHNLSVC.CommonSearch.SerachSatProHdr(SearchParams, null, null, DateTime.Today.AddDays(-7).Date, DateTime.Today);
                //if (result.Rows.Count > 0)
                //{
                //    result = result.AsEnumerable()
                //                     .Where(r => r.Field<string>("STATUS") == "APPROVED")
                //                     .CopyToDataTable();
                //}
                grdResultD.DataSource = result;
                ViewState["SEARCH"] = result;
                grdResultD.DataBind();
                lblvalue.Text = "boq";
                BindUCtrlDDLData2(result);
                txtSearchbywordD.Focus();
                txtFDate.Text = DateTime.Today.AddDays(-7).Date.ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                UserDPopoup.Show();
            }
            else
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.General.SearchBOQDocNo(SearchParams, null, null);
                if (result.Rows.Count > 0)
                {
                    result = result.AsEnumerable()
                                     .Where(r => r.Field<string>("STATUS") == "APPROVED")
                                     .CopyToDataTable();
                }
                grdResult.DataSource = result;
                ViewState["SEARCH"] = result;
                grdResult.DataBind();
                lblvalue.Text = "boq";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                SIPopup.Show();
            }
        }
        protected void txtBoq_TextChanged(object sender, EventArgs e)
        {
            txtKitCode.Text = "";
            SatProjectHeader _SatProjectHeader = new SatProjectHeader();
            _SatProjectHeader = CHNLSVC.Sales.GETBOQHDR(Session["UserCompanyCode"].ToString(), txtBoq.Text.Trim());
            if (_SatProjectHeader.SPH_COM == null)
            {
                txtBoq.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid BOQ number')", true);
            }
        }

        protected void lbtnBatch_Click(object sender, EventArgs e)
        {
            txtFDate.Text = Convert.ToDateTime(txtRequestDate.Text).Date.AddMonths(-1).ToShortDateString();
            txtTDate.Text = Convert.ToDateTime(txtRequestDate.Text).Date.ToShortDateString();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
            DataTable result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            ViewState["SEARCH"] = result;
            lblvalue.Text = "Batch";
            BindUCtrlDDLData2(result);
            txtSearchbyword.Focus();
            UserDPopoup.Show();

        }

        #region Modal Popup 2
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            SIPopup.Hide();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable _result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Batch";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "SerchMrnWeb")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                DataTable _result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text
                    , !string.IsNullOrEmpty(txtSearchbywordD.Text) ? txtSearchbywordD.Text.Trim() + "%" : "", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "SerchMrnWeb";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "boq" && ddlRequestSubType.SelectedValue == "PRO")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.SerachSatProHdr(SearchParams, "", "", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "boq";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[2].Text;
            if (lblvalue.Text == "Batch")
            {
                txtBatch.Text = grdResultD.SelectedRow.Cells[2].Text;
                lblvalue.Text = "";
            }
            if (lblvalue.Text == "SerchMrnWeb")
            {
                txtRequest.Text = grdResultD.SelectedRow.Cells[1].Text;
                lblvalue.Text = "";
                txtRequest_Leave(null, null);

                foreach (GridViewRow item in gvItem.Rows)
                {
                    Label lblstatus2 = (Label)item.FindControl("lblstatus2");

                    Label lbtnstusdesc = (Label)item.FindControl("lbtnstusdesc");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstatus2.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lbtnstusdesc.Text = ddr2[0].ToString();
                        }
                    }
                }
            }
            if (lblvalue.Text == "boq" && ddlRequestSubType.SelectedValue == "PRO")
            {
                txtKitCode.Text = "";
                txtBoq.Text = grdResultD.SelectedRow.Cells[1].Text;
                loadBoqItem(txtBoq.Text);
                lblvalue.Text = "";
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            grdResultD.DataSource = null;
            grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
            grdResultD.DataBind();
            grdResultD.PageIndex = 0;
            UserDPopoup.Show();
            txtSearchbyword.Focus();

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable _result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Batch";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "SerchMrnWeb")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                DataTable _result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text
                    , !string.IsNullOrEmpty(txtSearchbywordD.Text) ? txtSearchbywordD.Text.Trim() + "%" : "", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "SerchMrnWeb";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "boq" && ddlRequestSubType.SelectedValue == "PRO")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.SerachSatProHdr(SearchParams, ddlSearchbykeyD.SelectedItem.Text
                    , !string.IsNullOrEmpty(txtSearchbywordD.Text) ? txtSearchbywordD.Text.Trim() + "%" : "", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "boq";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }

        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Batch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryItem);
                DataTable _result = CHNLSVC.Inventory.SearchInrBatch(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Batch";
                Session["Dpop"].ToString();
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "SerchMrnWeb")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                DataTable _result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim() + "%", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "SerchMrnWeb";
                Session["Dpop"].ToString();
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "boq" && ddlRequestSubType.SelectedValue == "PRO")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.SerachSatProHdr(SearchParams, ddlSearchbykeyD.SelectedItem.Text
                    , !string.IsNullOrEmpty(txtSearchbywordD.Text) ? txtSearchbywordD.Text.Trim() + "%" : "", Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "boq";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
        }
        #endregion
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            UserDPopoup.Hide();
            SIPopup.Hide();
        }
        protected void txtBatch_TextChanged(object sender, EventArgs e)
        {

            DataTable result = CHNLSVC.Inventory.chekInr_batchno(txtBatch.Text, Session["UserCompanyCode"].ToString());
            if (result.Rows.Count == 0)
            {
                txtBatch.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid batch number');", true);
            }
        }


        private void loadBoqItem(string RequestNo)
        {
            #region Load boq data
            if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "PRO")
            {
                _SatProjectDetails = CHNLSVC.Sales.GET_SAT_PRO_DET_DATA(new SatProjectDetails() { SPD_NO = RequestNo, SPD_ACTVE = 1 });
                //GET_SAT_PRO_DET_DATA(RequestNo);

                if (_SatProjectDetails != null)
                {
                    if (_SatProjectDetails.Count > 0)
                    {
                        List<InventoryRequestItem> _invReqItemList = new List<InventoryRequestItem>();
                        foreach (SatProjectDetails _itm in _SatProjectDetails)
                        {
                            InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                            MasterItem _masterItem = new MasterItem();
                            _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.SPD_ITM);
                            if (_masterItem == null)
                            {
                                DispMsg("Invalid item code : " + _itm.SPD_ITM);
                                return;
                            }
                            _masterItem.Mi_cd = _masterItem.Mi_cd;
                            _inventoryRequestItem.Itri_itm_cd = _masterItem.Mi_cd;
                            _masterItem.Mi_longdesc = _masterItem.Mi_longdesc;
                            _inventoryRequestItem.Mi_longdesc = _masterItem.Mi_longdesc;
                            _masterItem.Mi_model = _masterItem.Mi_model;
                            _inventoryRequestItem.Mi_model = _masterItem.Mi_model;
                            _masterItem.Mi_brand = _masterItem.Mi_brand;
                            _inventoryRequestItem.Mi_brand = _masterItem.Mi_brand;
                            _masterItem.Mi_itm_uom = _masterItem.Mi_itm_uom;
                            _inventoryRequestItem.Mi_itm_uom = _masterItem.Mi_itm_uom;
                            _inventoryRequestItem.MasterItem = _masterItem;

                            _inventoryRequestItem.Itri_itm_stus = "GOD";
                            _inventoryRequestItem.Itri_res_no = "";
                            _inventoryRequestItem.Itri_note = "";
                            _inventoryRequestItem.Itri_qty = _itm.SPD_MRN_BAL;
                            _inventoryRequestItem.Itri_app_qty = 0;

                            _inventoryRequestItem.Itri_mitm_cd = _masterItem.Mi_cd;
                            _inventoryRequestItem.Itri_mitm_stus = "GOD"; ;
                            _inventoryRequestItem.Itri_mqty = 0;
                            _inventoryRequestItem.Itri_job_no = txtBatch.Text.Trim();
                            _inventoryRequestItem.Tmp_kit_cd = _itm.SPD_KIT_ITM;
                            _inventoryRequestItem.Itri_base_req_no = _itm.SPD_NO;
                            _inventoryRequestItem.Itri_base_req_line = _itm.SPD_LINE;
                            Int32 _tmpLineNo = 0;
                            if (_invReqItemList != null)
                            {
                                if (_invReqItemList.Count > 0)
                                {
                                    Int32 _maxTmpLine = _invReqItemList.Max(c => c.Itri_line_no_tmp);
                                    _tmpLineNo = _maxTmpLine;
                                }
                            }
                            _inventoryRequestItem.Itri_line_no_tmp = _tmpLineNo + 1;
                            _inventoryRequestItem.Tmp_base_doc_bal = _inventoryRequestItem.Itri_qty;
                            if (_inventoryRequestItem.Itri_qty > 0)
                            {
                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }

                        Session["ITEMLIST_MRN"] = _invReqItemList;
                        Session["ITEMLIST_INTR"] = _invReqItemList;
                        gvItem.DataSource = _invReqItemList;
                        gvItem.DataBind();
                        LoadGridUnitCost(_invReqItemList);
                    }
                }
            }
            #endregion
        }

        protected void txtRequest_TextChanged1(object sender, EventArgs e)
        {
            DataTable result = null;
            string _mytype = Request.QueryString["REQTYPE"];
            if (_mytype == "MRN")//432
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRNWEB);
                result = CHNLSVC.CommonSearch.SearchMrnDocumentsWeb(SearchParams, "MRN NO", txtRequest.Text, DateTime.MinValue, DateTime.MinValue);
            }
            if (_mytype == "INTR")//428
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferRequestWeb);
                result = CHNLSVC.Inventory.GetSearchInterTransferRequestWeb(SearchParams, "INTER-TRANSFER NO", txtRequest.Text);
            }
            if (result == null || result.Rows.Count == 0)
            {
                DispMsg("Please enter a valid No !!!"); return;
            }
            txtRequest_Leave(null, null);
        }

        protected void txtReservation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReservation.Text))
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    {
                        txtReservation.Text = ""; DispMsg("Please select the dispatch location !"); return;
                    }
                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        txtReservation.Text = ""; DispMsg("Please select the item !"); return;
                    }
                    if (cmbStatus.SelectedValue == "0")
                    {
                        txtReservation.Text = ""; DispMsg("Please select the item status !"); return;
                    }
                    /*
                    INR_RES _inrRes = CHNLSVC.Inventory.GET_INR_RES_DATA(new INR_RES()
                    {
                        IRS_RES_NO = txtReservation.Text.Trim(),
                        IRS_ANAL_2 = txtDispatchRequried.Text.ToUpper().Trim(),
                        IRS_COM = Session["UserCompanyCode"].ToString(),
                        IRS_STUS = "A"
                    }).FirstOrDefault();
                    if (_inrRes == null)
                    {
                        txtReservation.Text = "";
                        DispMsg("Invalid reservation number !"); return;
                    }*/
                    _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                         new INR_RES_LOG()
                                         {
                                             IRL_RES_NO = txtReservation.Text.Trim(),
                                             IRL_ITM_CD = txtItem.Text.Trim(),
                                             IRL_ITM_STUS = cmbStatus.SelectedValue,
                                             IRL_CURT_COM = ddlCompany.SelectedValue,
                                             IRL_CURT_LOC = txtDispatchRequried.Text.Trim().ToUpper(),
                                             IRL_CURT_DOC_TP = "INV",
                                             IRL_CURT_DOC_NO = txtReservation.Text.Trim(),
                                             IRL_ACT = 1
                                         });
                    if (_resLogAvaData.Count < 1)
                    {
                        DispMsg("Invalid reservation number !"); txtReservation.Text = ""; return;
                    }
                    INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                    {
                        IRD_RES_NO = txtReservation.Text.Trim(),
                        IRD_ITM_CD = txtItem.Text.Trim(),
                        IRD_ITM_STUS = cmbStatus.SelectedValue
                    }).FirstOrDefault();
                    if (_inrRes == null)
                    {
                        txtReservation.Text = "";
                        DispMsg("Invalid reservation details !"); txtReservation.Text = ""; return;
                    }
                    decimal _resDBal = _inrRes.IRD_MRN_AVA_BAL;
                    if (_resDBal <= 0)
                    {
                        txtReservation.Text = "";
                        DispMsg("Reservation detail balance is not available !"); txtReservation.Text = ""; return;
                    }
                    //if (_resDBal > _resDBal)
                    //{
                    //    txtReservation.Text = "";
                    //    DispMsg("Reservation detail balance is invalid !"); txtReservation.Text = ""; return;
                    //}
                }
            }
            catch (Exception ex)
            {
                txtReservation.Text = "";
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
            else if (msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }

        private void BindInventoryRequestItemsGridDataBoq()
        {
            try
            {
                #region validate
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter item code !!!')", true);
                    return;
                }

                if (cmbStatus.SelectedIndex == 0)
                {
                    cmbStatus.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select item status !!!')", true);
                    return;
                }

                if ((string.IsNullOrEmpty(txtQty.Text)) || ((txtQty.Text == "0")))
                {
                    txtQty.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter required quantity !!!')", true);
                    return;
                }

                if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                    txtQty.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtReservation.Text))
                {
                    /*
                      DataTable _dt = CHNLSVC.Inventory.GetReservationDet(Session["UserCompanyCode"].ToString(), txtReservation.Text, txtItem.Text, cmbStatus.SelectedValue.ToString());
                      if (_dt.Rows.Count == 0)
                      {
                          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid reservation number !!!')", true);
                          txtReservation.Text = "";
                          txtReservation.Focus();
                          return;
                      }
                      if (_dt.Rows.Count>0)
                      {
                          decimal _resQty = Convert.ToDecimal(_dt.Rows[0]["IRD_RES_BQTY"].ToString());
                          if (_resQty<Convert.ToDecimal(txtQty.Text.Trim()))
                          {
                              string _msg = "Cannot exceed the reservation balance ! " + _resQty.ToString("N2");
                              DispMsg(_msg);
                              txtQty.Text = "";
                              txtQty.Focus();
                              return;
                          }
                      }
                      */
                }

                MasterItem _itemMas = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                _invReqItemList = new List<InventoryRequestItem>();

                DataTable _dtMax = CHNLSVC.General.GetStockRequest("GIT", Session["UserDefLoca"].ToString(), BaseCls.GlbDefChannel, Session["UserCompanyCode"].ToString(), DateTime.Now.Date, txtItem.Text, _itemMas.Mi_brand, _itemMas.Mi_cate_1, _itemMas.Mi_cate_2, _itemMas.Mi_cate_3, _itemMas.Mi_cate_4, _itemMas.Mi_cate_5);
                if (_dtMax.Rows.Count > 0)
                {
                    DataTable _dtGit = CHNLSVC.General.GetItemGIT(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, null, null, null, null, null, null, 0);
                    if (_dtGit.Rows.Count > 0)
                        if (Convert.ToDecimal(_dtGit.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                        {
                            string msg = "GIT available.You are exceeding allowable quantity !!! " + Session["UserDefLoca"].ToString();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('GIT available.You are exceeding allowable quantity !!!')", true);
                            return;
                        }
                }
                #endregion

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? string.Empty : txtItmRemark.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                List<InventoryRequestItem> _temp = _invReqItemList;
                List<InventoryRequestItem> _resultList = null;
                List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);
                bool _isKitComAva = false;
                if (_itemComponentList != null)
                {
                    if (_itemComponentList.Count > 0)
                    {
                        _isKitComAva = true;
                    }
                }
                if (_isKitComAva)
                {
                    if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                    {
                        foreach (MasterItemComponent _itemCompo in _itemComponentList)
                        {
                            if (_invReqItemList != null)
                            {
                                if (_invReqItemList.Count > 0)
                                {
                                    _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd) && x.Itri_itm_stus == _itemStatus).ToList();
                                }
                            }
                            if ((_resultList != null) && (_resultList.Count > 0))
                            {
                                foreach (InventoryRequestItem _result in _resultList)
                                {
                                    _result.Itri_qty = _result.Itri_qty + (_mainItemQty * _itemCompo.Micp_qty);
                                }
                            }
                            else
                            {
                                //Issue 
                                InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _inventoryRequestItem.Itri_itm_cd = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _inventoryRequestItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _inventoryRequestItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _componentItem.Mi_itm_uom = _itemCompo.ComponentItem.Mi_itm_uom;
                                _inventoryRequestItem.Mi_itm_uom = _itemCompo.ComponentItem.Mi_itm_uom;
                                _inventoryRequestItem.MasterItem = _componentItem;

                                _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = 0;

                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;
                                Int32 _tmpLineNo = 0;
                                if (_invReqItemList != null)
                                {
                                    if (_invReqItemList.Count > 0)
                                    {
                                        Int32 _maxTmpLine = _invReqItemList.Max(c => c.Itri_line_no_tmp);
                                        _tmpLineNo = _maxTmpLine;
                                    }
                                }
                                _inventoryRequestItem.Itri_line_no_tmp = _tmpLineNo + 1;
                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                    string _mytype = Request.QueryString["REQTYPE"];
                    if (_mytype == "MRN")
                    {
                        Session["ITEMLIST_MRN"] = _invReqItemList;
                        Session["ITEMLIST_INTR"] = null;
                    }
                    else
                    {
                        Session["ITEMLIST_MRN"] = null;
                        Session["ITEMLIST_INTR"] = _invReqItemList;
                    }
                }
                else
                {
                    #region Check Consignment Items :: Chamal 07-May-2014
                    if (_invReqItemList != null)
                    {
                        if (_invReqItemList.Count > 0)
                        {
                            if (_itemStatus == "CONS")
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus != _itemStatus).ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please create separate entry for consignment items !!!')", true);
                                    return;
                                }
                            }
                            else
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus == "CONS").ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please create separate entry for consignment items !!!')", true);
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        //if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }


                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtItem.Text.Trim()) && x.Itri_itm_stus == _itemStatus).ToList();


                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        //if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }
                    else
                    {
                        InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                        MasterItem _masterItem = new MasterItem();
                        _masterItem.Mi_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_itm_cd = _mainItemCode;
                        _masterItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _inventoryRequestItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _masterItem.Mi_model = _itemdetail.Mi_model;
                        _inventoryRequestItem.Mi_model = _itemdetail.Mi_model;
                        _masterItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.Mi_brand = _itemdetail.Mi_brand;
                        _masterItem.Mi_itm_uom = _itemdetail.Mi_itm_uom;
                        _inventoryRequestItem.Mi_itm_uom = _itemdetail.Mi_itm_uom;
                        _inventoryRequestItem.MasterItem = _masterItem;

                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_res_no = _reservationNo;
                        if (!string.IsNullOrEmpty(_reservationNo) && _reservationNo != "N/A")
                        {
                            _inventoryRequestItem.Itri_res_qty = _mainItemQty;
                        }
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = 0;

                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;
                        _inventoryRequestItem.Itri_job_no = txtBatch.Text.Trim();
                        string _mytype = Request.QueryString["REQTYPE"];

                        var list = new List<InventoryRequestItem>();

                        if (_mytype == "MRN")
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                        }
                        else
                        {
                            list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                        }

                        if (list != null)
                        {
                            _invReqItemList = list;
                        }
                        Int32 _tmpLineNo = 0;
                        if (_invReqItemList != null)
                        {
                            if (_invReqItemList.Count > 0)
                            {
                                Int32 _maxTmpLine = _invReqItemList.Max(c => c.Itri_line_no_tmp);
                                _tmpLineNo = _maxTmpLine;
                            }
                        }
                        _inventoryRequestItem.Itri_line_no_tmp = _tmpLineNo + 1;
                        _invReqItemList.Add(_inventoryRequestItem);

                        if (_mytype == "MRN")
                        {
                            Session["ITEMLIST_MRN"] = _invReqItemList;
                            Session["ITEMLIST_INTR"] = null;
                        }
                        else
                        {
                            Session["ITEMLIST_MRN"] = null;
                            Session["ITEMLIST_INTR"] = _invReqItemList;
                        }
                    }
                }

                ClearLayer2();
                ClearLayer3();

                gvItem.DataSource = new List<InventoryRequestItem>();


                #region Check Duplicate Serials

                var DistinctItems = _invReqItemList.GroupBy(x => x.Itri_itm_cd).Select(y => y.First());
                _invReqItemList = DistinctItems.ToList();

                #endregion

                gvItem.DataSource = _invReqItemList;
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);

                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnHdrDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                }
                if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                {
                    if (_invReqItemList != null)
                    {
                        var _remList = _invReqItemList.Where(c => c.Tmp_itm_select == 1).ToList();
                        if (_remList != null)
                        {
                            foreach (var item in _remList)
                            {
                                _invReqItemList.Remove(item);
                            }
                        }
                    }
                }
                if (_mytype == "MRN")
                {
                    Session["ITEMLIST_MRN"] = _invReqItemList;
                    Session["ITEMLIST_INTR"] = null;
                }
                else
                {
                    Session["ITEMLIST_MRN"] = null;
                    Session["ITEMLIST_INTR"] = _invReqItemList;
                }
                gvItem.DataSource = new int[] { };
                if (_invReqItemList != null)
                {
                    if (_invReqItemList.Count > 0)
                    {
                        gvItem.DataSource = _invReqItemList;
                    }
                }
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
                if (ddlRequestSubType.SelectedValue == "BOQ" || ddlRequestSubType.SelectedValue == "ADBOQ")
                {
                    if (!string.IsNullOrEmpty(txtKitCode.Text))
                    {
                        txtKitCode_TextChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void chkSelectGvItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox btn = (CheckBox)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                CheckBox chkSelectGvItem = row.FindControl("chkSelectGvItem") as CheckBox;
                Label lblnoitm = row.FindControl("lblnoitm") as Label;
                Label lblItri_line_no_tmp = row.FindControl("lblItri_line_no_tmp") as Label;
                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                }
                Int32 _lineNo = Convert.ToInt32(lblItri_line_no_tmp.Text.Trim());
                if (_invReqItemList != null)
                {
                    var _req = _invReqItemList.Where(c => c.Itri_line_no_tmp == _lineNo).FirstOrDefault();
                    if (_req != null)
                    {
                        _req.Tmp_itm_select = chkSelectGvItem.Checked ? 1 : 0;
                    }
                }
                if (_mytype == "MRN")
                {
                    Session["ITEMLIST_MRN"] = _invReqItemList;
                    Session["ITEMLIST_INTR"] = null;
                }
                else
                {
                    Session["ITEMLIST_MRN"] = null;
                    Session["ITEMLIST_INTR"] = _invReqItemList;
                }
                gvItem.DataSource = new int[] { };
                if (_invReqItemList != null)
                {
                    if (_invReqItemList.Count > 0)
                    {
                        gvItem.DataSource = _invReqItemList;
                    }
                }
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
                bool _select = true;
                var _selectAll = _invReqItemList.Where(c => c.Tmp_itm_select == 0).ToList();
                if (_selectAll != null)
                {
                    if (_selectAll.Count > 0 && gvItem.Rows.Count > 0)
                    {
                        _select = false;
                    }
                }
                CheckBox chkSelectAllGvItem1 = gvItem.HeaderRow.FindControl("chkSelectAllGvItem") as CheckBox;
                chkSelectAllGvItem1.Checked = _select ? true : false;
                if (!string.IsNullOrEmpty(txtKitCode.Text))
                {
                    txtKitCode_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void chkSelectAllGvItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSelectAllGvItem = gvItem.HeaderRow.FindControl("chkSelectAllGvItem") as CheckBox;
                bool _select = chkSelectAllGvItem.Checked;
                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    _invReqItemList = list;
                    if (!string.IsNullOrEmpty(txtKitCode.Text))
                    {
                        var _kitList = _invReqItemList.Where(c => c.Tmp_kit_cd == txtKitCode.Text.Trim().ToUpper()).ToList();
                        if (_kitList != null)
                        {
                            if (_kitList.Count > 0)
                            {
                                foreach (var item in _kitList)
                                {
                                    item.Tmp_itm_select = _select ? 1 : 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in _invReqItemList)
                        {
                            item.Tmp_itm_select = _select ? 1 : 0;
                        }
                    }

                }
                if (_mytype == "MRN")
                {
                    Session["ITEMLIST_MRN"] = _invReqItemList;
                    Session["ITEMLIST_INTR"] = null;
                }
                else
                {
                    Session["ITEMLIST_MRN"] = null;
                    Session["ITEMLIST_INTR"] = _invReqItemList;
                }
                gvItem.DataSource = new int[] { };
                if (_invReqItemList != null)
                {
                    if (_invReqItemList.Count > 0)
                    {
                        gvItem.DataSource = _invReqItemList;
                    }
                }
                gvItem.DataBind();
                LoadGridUnitCost(_invReqItemList);
                CheckBox chkSelectAllGvItem1 = gvItem.HeaderRow.FindControl("chkSelectAllGvItem") as CheckBox;
                chkSelectAllGvItem1.Checked = _select ? true : false;
                if (!string.IsNullOrEmpty(txtKitCode.Text))
                {
                    txtKitCode_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtKitCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtKitCode.Text))
                {
                    gvItem.DataSource = new int[] { };
                    string _mytype = Request.QueryString["REQTYPE"];
                    var list = new List<InventoryRequestItem>();

                    if (_mytype == "MRN")
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                    }
                    else
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                    }

                    if (list != null)
                    {
                        _invReqItemList = list;
                        if (_invReqItemList.Count > 0)
                        {
                            var _kitList = _invReqItemList.Where(c => c.Tmp_kit_cd == txtKitCode.Text.Trim().ToUpper()).ToList();
                            if (_kitList != null)
                            {
                                if (_kitList.Count > 0)
                                {
                                    gvItem.DataSource = _kitList;
                                    LoadGridUnitCost(_kitList);
                                }
                            }
                        }
                    }
                    gvItem.DataBind();
                }
                else
                {
                    gvItem.DataSource = new int[] { };
                    string _mytype = Request.QueryString["REQTYPE"];
                    var list = new List<InventoryRequestItem>();

                    if (_mytype == "MRN")
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                    }
                    else
                    {
                        list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                    }

                    if (list != null)
                    {
                        _invReqItemList = list;
                        if (_invReqItemList.Count > 0)
                        {
                            gvItem.DataSource = _invReqItemList;
                        }
                    }
                    gvItem.DataBind();
                    LoadGridUnitCost(_invReqItemList);
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnKitBrekUp_Click(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["REQTYPE"];
                var list = new List<InventoryRequestItem>();

                if (_mytype == "MRN")
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_MRN"];
                }
                else
                {
                    list = (List<InventoryRequestItem>)Session["ITEMLIST_INTR"];
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        var _tmpList = list.Where(c => c.Tmp_kit_cd != null && c.Tmp_kit_cd != "").ToList();
                        if (_tmpList != null)
                        {
                            List<ItemKitComponent> _kitList = new List<ItemKitComponent>();
                            ItemKitComponent _kit = new ItemKitComponent();
                            foreach (var item in _tmpList)
                            {
                                _kit = new ItemKitComponent();
                                _kit.MIKC_ITM_CODE_MAIN = item.Tmp_kit_cd;
                                var _kitava = _kitList.Where(c => c.MIKC_ITM_CODE_MAIN == item.Tmp_kit_cd).FirstOrDefault();
                                if (_kitava == null)
                                {
                                    _kitList.Add(_kit);
                                }

                            }
                            if (_kitList.Count > 0)
                            {
                                dgvKitBup.DataSource = _kitList;
                                dgvKitBup.DataBind();
                                popKitBup.Show();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnKitBupClose_Click(object sender, EventArgs e)
        {
            popKitBup.Hide();
        }

        protected void lbtnSelectKit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow _row = (GridViewRow)btn.NamingContainer;
                Label lblMIKC_ITM_CODE_MAIN = _row.FindControl("lblMIKC_ITM_CODE_MAIN") as Label;
                if (!string.IsNullOrEmpty(lblMIKC_ITM_CODE_MAIN.Text))
                {
                    txtKitCode.Text = lblMIKC_ITM_CODE_MAIN.Text;
                    txtKitCode_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnClearKitCode_Click(object sender, EventArgs e)
        {
            txtKitCode.Text = "";
            txtKitCode_TextChanged(null, null);
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblAvalQty.Text = "0.0000";
                lblFreeQty.Text = "0.0000";
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim().ToUpper());
                    if (_mstItm != null)
                    {
                        lblItemDescription.Text = _mstItm.Mi_longdesc;
                        lblItemModel.Text = _mstItm.Mi_model;
                        lblItmUom.Text = _mstItm.Mi_itm_uom;
                        lblItemBrand.Text = _mstItm.Mi_brand;
                        string _serialstatus = _mstItm.Mi_is_subitem == true ? "Available" : "Non-Serialized";
                        lblItemSubStatus.Text = _serialstatus;

                        //Added by Chamal 12-Mar-2018
                        if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
                        {
                            List<InventoryLocation> _inventoryLocation = null;
                            if (string.IsNullOrEmpty(cmbStatus.Text))
                            {
                                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.ToString().ToUpper(), txtItem.Text.Trim().ToString(), string.Empty);
                            }
                            else
                            {
                                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), txtDispatchRequried.Text.ToString().ToUpper(), txtItem.Text.Trim().ToString(), cmbStatus.Text.ToString());
                            }
                            if (_inventoryLocation != null)
                            {
                                if (_inventoryLocation.Count > 0)
                                {
                                    var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                                    var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                                    lblAvalQty.Text = Convert.ToString(_aQty);
                                    lblFreeQty.Text = Convert.ToString(_aFree);
                                }
                            }
                        }

                    }
                    else
                    {
                        DispMsg("Invalid item code ! ");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "Error");
            }
        }

        protected void txtItem_TextChanged1(object sender, EventArgs e)
        {

        }
        private void LoadLocationData()
        {
            try
            {
                string _err = "";
                txtBGValue.Text = ""; txtStockVal.Text = ""; txtShStkVal.Text = ""; txtAppMrnVal.Text = ""; txtFreeVal.Text = ""; txtCurrMrnVal.Text = ""; txtTotMrnCom.Text = ""; txtTotQtyMain.Text = "";
                decimal BGValue = 0; decimal StockVal = 0; decimal ShStkVal = 0; decimal AppMrnVal = 0; decimal FreeVal = 0; decimal CurrMrnVal = 0; decimal TotMrnComQty = 0; decimal TotMrnMainQty = 0;
                MasterLocationNew _mstLoc = CHNLSVC.General.GetMasterLocations(
                    new MasterLocationNew() { 
                        Ml_loc_cd = Session["UserDefLoca"].ToString().ToUpper(), Ml_com_cd = Session["UserCompanyCode"].ToString() 
                    }).FirstOrDefault();

                if (_mstLoc != null)
                {
                    HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                    if (_sysPara != null)
                    {
                        if (_sysPara.Hsy_val == 1)
                        {
                            BGValue = _mstLoc.Ml_bank_grnt_val;
                            StockVal = _mstLoc.Ml_app_stk_val;
                            ShStkVal = CHNLSVC.MsgPortal.GetMrnShowroomStockValue(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().ToUpper());
                            AppMrnVal = CHNLSVC.MsgPortal.GetApprovedMrnShowroomStockValue(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().ToUpper(),out _err);
                          //  FreeVal = BGValue + StockVal - ShStkVal - AppMrnVal;
                            FreeVal = BGValue - ShStkVal - AppMrnVal;
                            TotMrnComQty = 0;
                            TotMrnMainQty = 0;
                        }
                    }
                }
                #region dataset
                txtBGValue.Text = BGValue.ToString("#,###,##0.0000##");
                txtStockVal.Text = StockVal.ToString("#,###,##0.0000##");
                txtShStkVal.Text = ShStkVal.ToString("#,###,##0.0000##");
                txtAppMrnVal.Text = AppMrnVal.ToString("#,###,##0.0000##");
                txtFreeVal.Text = FreeVal.ToString("#,###,##0.0000##");
                txtCurrMrnVal.Text = CurrMrnVal.ToString("#,###,##0.0000##");
                txtTotMrnCom.Text = TotMrnComQty.ToString("#,###,##0.0000##");
                txtTotQtyMain.Text = TotMrnMainQty.ToString("#,###,##0.0000##");
                #endregion
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private void LoadGridUnitCost(List<InventoryRequestItem> _reqItmList)
        {
            try
            {
                string _err="";
                decimal _totVal = 0, _mainItmQty = 0, _subItmQty = 0;
                 HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                 if (_sysPara != null)
                 {
                     if (_sysPara.Hsy_val == 1)
                     {
                         txtCurrMrnVal.Text = (0).ToString("#,###,##0.0000##");
                         txtTotMrnCom.Text = (0).ToString("#,###,##0.0000##");
                         txtTotQtyMain.Text = (0).ToString("#,###,##0.0000##");
                         if (_reqItmList != null)
                         {
                             if (_reqItmList.Count > 0)
                             {
                                 MasterItem _mstItm = new MasterItem();
                                 foreach (var item in _reqItmList)
                                 {
                                     _mstItm = CHNLSVC.General.GetItemMasterNew(item.Itri_itm_cd);
                                     if (_mstItm!=null)
                                     {
                                         item.Tmp_itm_tp = _mstItm.Mi_itm_tp;
                                     }
                                     if (item.Tmp_itm_tp == "M")
                                     {
                                         _mainItmQty = _mainItmQty + item.Itri_qty;
                                     }
                                     else
                                     {
                                         _subItmQty = _subItmQty + item.Itri_qty;
                                     }
                                 }
                                 _totVal = CHNLSVC.MsgPortal.GetMrnItemsStockValue(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _reqItmList, out _err);
                                 txtCurrMrnVal.Text = _totVal.ToString("#,###,##0.0000##");
                                 txtTotMrnCom.Text = _mainItmQty.ToString("#,###,##0.0000##");
                                 txtTotQtyMain.Text = _subItmQty.ToString("#,###,##0.0000##");
                                 decimal salesTotal = 0;
                                 txtSalesValue.Text = "0.0";
                                 foreach(var items in _reqItmList)
                                 {
                                     DataTable pbpl = CHNLSVC.Inventory.GetPriceBookLvl(Session["UserCompanyCode"].ToString());
                                     decimal amount = 0;
                                     DataTable itmprice = CHNLSVC.Inventory.GetItemPricePc(pbpl.Rows[0]["mc_anal7"].ToString(), pbpl.Rows[0]["mc_anal8"].ToString(), items.Itri_itm_cd, DateTime.Now.Date, Session["UserDefProf"].ToString());
                                     if (itmprice.Rows.Count > 0)
                                     {
                                         amount = Convert.ToDecimal(itmprice.Rows[0][0].ToString());
                                     }                                    
                                     //Added By Dulaj 2018/Jul/16                                     
                                     //decimal amount = CHNLSVC.Inventory.Get_def_price_from_pc(mstCompany.Mc_anal5.ToString(), mstCompany.Mc_anal4.ToString(), items.Itri_itm_cd, DateTime.Now);
                                     amount = amount * items.Itri_qty;
                                     // if(!(txtSalesValue.Text.Equals(string.Empty)))
                                     // {
                                     salesTotal = Convert.ToDecimal(txtSalesValue.Text);
                                     salesTotal = amount + salesTotal;
                                     txtSalesValue.Text = salesTotal.ToString("#,###,##0.0000##");
                                     //  }
                                     //
                                 }
                             }
                             else
                             {
                                 txtSalesValue.Text = "0.0";
                             }
                         }
                        
                     }
                 }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
            //Itri_unit_price
        }

        protected void btnConfOk_Click(object sender, EventArgs e)
        {
            _isValueExceedSave = true;
            btnSave_Click(null, null);
        }

        protected void btnConfNo_Click(object sender, EventArgs e)
        {

        }
    }
}