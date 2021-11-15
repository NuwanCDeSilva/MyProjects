using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FastForward.SCMWeb.View.Reports.Warehouse;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class Distribution_approval : Base
    {
        bool _isValueExceedSave
        {
            set
            {
                Session["_isValueExceedDA"] = value;
            }
            get
            {
                if (Session["_isValueExceedDA"] != null)
                {
                    return (bool)Session["_isValueExceedDA"];
                }
                else
                {
                    return false;
                }
            }
        }
        private List<MasterItemStatus> _stsList
        {
            get { if (Session["_stsList"] != null) { return (List<MasterItemStatus>)Session["_stsList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_stsList"] = value; }
        }
        string _printDoc
        {
            get { if (Session["_printDoc"] != null) { return (string)Session["_printDoc"]; } else { return ""; } }
            set { Session["_printDoc"] = value; }
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
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        List<InventoryLocation> _Invetory = new List<InventoryLocation>();
        protected bool _contine { get { return (bool)Session["_contine"]; } set { Session["_contine"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }

        protected int _maxline { get { return (int)Session["_maxline "]; } set { Session["_maxline "] = value; } }
        protected List<MasterItemComponent> _masterItemComponent { get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; } set { Session["_masterItemComponent"] = value; } }
        protected List<MasterBufferChannel> _MasterBufferChannel { get { return (List<MasterBufferChannel>)Session["_MasterBufferChannel"]; } set { Session["_MasterBufferChannel"] = value; } }

        protected List<InventoryRequestItem> _MRNRequestItem { get { return (List<InventoryRequestItem>)Session["_MRNRequestItem"]; } set { Session["_MRNRequestItem"] = value; } }

        protected List<InventoryRequestItem> _ApproveItem { get { return (List<InventoryRequestItem>)Session["_ApproveItem"]; } set { Session["_ApproveItem"] = value; } }

        protected List<InventorySerialN> _serial { get { return (List<InventorySerialN>)Session["_serial"]; } set { Session["_serial"] = value; } }

        protected List<InventorySerialN> _selectserial { get { return (List<InventorySerialN>)Session["_selectserial"]; } set { Session["_selectserial"] = value; } }
        InvReportPara _invRepPara = new InvReportPara();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["popo"] == null)
            {
                Session["popo"] = "";
            }
            if (!IsPostBack)
            {
                pageclear();
            }
            else
            {
                if (Session["popo"].ToString() == "true")
                {
                    UserPopup.Show();
                    Session["popo"] = "";
                }
                if (Session["MRN_App"].ToString() == "true")
                {
                    UserDPopoup.Show();
                    UserPopup.Hide();
                    Session["MRN_App"] = "";
                }
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    _serPopShow = false;
                    PopupSearch.Hide();
                }
            }

        }

        private void pageclear()
        {
            _isValueExceedSave = false;
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
            _stsList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            _contine = false;
            txtsearch.Enabled = false;
            ViewState["_ApproveItem"] = "";
            ViewState["_MRNRequestItem"] = "";
            ViewState["showrooms"] = "";
            Session["MRN_App"] = "";
            Session["popo"] = "";
            grdRequestDetails.DataSource = new int[] { };
            grdRequestDetails.DataBind();
            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();
            grdMRNReqItem.DataSource = new int[] { };
            grdMRNReqItem.DataBind();
            Session["RequestCompany"] = "";
            // _MRNRequest = new List<InventoryRequest>();
            _MRNRequestItem = new List<InventoryRequestItem>();
            _ApproveItem = new List<InventoryRequestItem>();
            _Invetory = new List<InventoryLocation>();
            _serial = new List<InventorySerialN>();
            _selectserial = new List<InventorySerialN>();
            All.Visible = true;
            //lbtnShowroom.Visible = true;
            //adhoc.Visible = false;
            lblShowroom.Text = "N/A";
            //txtApproval.Text = string.Empty; ruk
            //txtSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy"); ruk
            txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lblSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lblrequestNo.Text = "N/A";
            lblPreferloc.Text = "N/A";
            lblbaseitem.Text = "N/A";
            lblAuthorizedby.Text = Session["UserID"].ToString();
            txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lblCompany.Text = Session["UserCompanyCode"].ToString();
            txtremark.Text = "N/A";
            txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
            lblRequesttype.Text = "N/A";
            Session["Itemcode"] = "";
            Session["Isalreadysave"] = "false";
            txtItem.Text = "N/A";
            lblModel.Text = "N/A";
            lblbaseitem.Text = "N/A";
            lblitemdes.Text = "";
            lblshowroomname.Text = "";
            lblGIT.Text = "0";
            lblshop.Text = "0";
            lblshopstock.Text = "0";
            lblForwardsale.Text = "0";
            lblBufferLimit.Text = "0";
            txtRequestqty.Text = "0";
            txtPrefexlocation.Text = "N/A";
            ddlPurchasetype.SelectedIndex = -1;
            txtApprovalqty.Text = "0";
            lbmaxhpaccounts.Text = "";
            lbcurrenthpacc.Text = "";
            lbmaxval.Text = "";
            lbcurrentval.Text = "";
            txtShowroom.Text = "";
            LoadLocationData();
            txtApproval.Text = "";
            lbtnSave.Enabled = true;
            lbtnSave.CssClass = "buttonUndocolorLeft";
            lbtnSave.OnClientClick = "SaveConfirm();";
            Label3.Visible = false;
            txtSalesValues.Text = "0.0";
            //txtpopupItem.Text = string.Empty;
            //txtpopupModel.Text = string.Empty;
            //txtpopupItemRemark.Text = string.Empty;
            //txtpopupshopstock.Text = string.Empty;
            //txtpopupForwardsale.Text = string.Empty;
            //txtpopupBufferLimit.Text = string.Empty;
            //txtpopupRequestqty.Text = string.Empty;
            //txtpopupIcompany.Text = string.Empty;
            //txtPrefexlocationpopup.Text = string.Empty;
            //ddlPurchasetypepopup.SelectedIndex = -1;
            //txtpopupApprovalqty.Text = string.Empty;
            divUpcompleted.Visible = false;
            lblExcelUploadError.Visible = false;
            Label1.Visible = false;
            lblExcelUploadError.Text = "";
            Label5.Visible = false;
            Addpurchasetype(txtRequestcompany.Text);

            lbtnFilter_Click(null, null);

            grdSeasson.DataSource = new int[] { };
            grdSeasson.DataBind();

        }

        #region search option
        protected void rdbRout_CheckedChanged(object sender, EventArgs e)
        {
            lblsearch.Text = "Route";
            txtsearch.Text = "";
            txtsearch.Enabled = false;
            txtPreferLocation.Text = "";
            chkSearchall.Checked = true;
            All.Visible = false;
            // chkSearchall.Visible = true;

        }

        protected void rdbshowroom_CheckedChanged(object sender, EventArgs e)
        {
            lblsearch.Text = "Showroom";
            txtsearch.Text = "";
            txtsearch.Enabled = false;
            txtPreferLocation.Text = "";
            chkSearchall.Checked = true;
            All.Visible = true;
            chkSearchall.Visible = true;
        }

        protected void rdbchannel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbchannel.Checked)
            {
                lblsearch.Text = "Channel";
                txtsearch.Text = "";
                txtPreferLocation.Text = "";
                txtsearch.Enabled = true;
                chkSearchall.Checked = false;
                All.Visible = false;
                chkSearchall.Visible = false;
            }
            else
            {
                txtsearch.Enabled = false;
            }

        }
        #endregion

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "CT004" + seperator + -1);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DispatchRoute:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtShowroom.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + lblCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append("" + seperator + lblCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtItem.Text.ToUpper().ToUpper() + seperator + "I" + seperator + lblShowroom.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SimilarItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtItem.Text.ToUpper().ToUpper() + seperator + "I" + seperator + lblShowroom.Text);
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

        #region Modalpopup
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string Name = grdResult.SelectedRow.Cells[1].Text;
                if (lblvalue.Text == "route")
                {
                    string Des = grdResult.SelectedRow.Cells[2].Text;
                    txtsearch.Text = Name;
                    txtsearch.ToolTip = Des;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "showroom")
                {
                    string Des = grdResult.SelectedRow.Cells[2].Text;
                    txtsearch.Text = Name;
                    txtsearch.ToolTip = Des;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "showroom2")
                {

                    string Des = grdResult.SelectedRow.Cells[2].Text;
                    txtShowroom.Text = Name;
                    LoadLocationData();
                    lblShowroom.Text = Name;
                    lblshowroomname.Text = Des;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "channel")
                {
                    string Des = grdResult.SelectedRow.Cells[2].Text;
                    txtsearch.Text = Name;
                    txtsearch.ToolTip = Des;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc")
                {
                    string Des = grdResult.SelectedRow.Cells[2].Text;
                    txtPreferLocation.Text = Name;
                    txtPreferLocation.ToolTip = Des;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_2")
                {
                    txtPrefexlocation.Text = Name;
                    UserPopup.Hide();
                    lblvalue.Text = "";
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
                UserPopup.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            txtSearchbyword.Text = "";
            UserPopup.Hide();
            lblvalue.Text = "";
        }
        private void FilterData()
        {
            if (lblvalue.Text == "MRN_App")
            {
                if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
                    string _Msg = "Please enter valid date.";
                    DisplayMessage(_Msg, 2);
                }
                if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    string _Msg = "Please enter valid date.";
                    DisplayMessage(_Msg, 2);
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "MRN_App";
                ViewState["SEARCH"] = _result;
                Session["MRN_App"] = "true";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "showroom2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "route")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _rotetbl;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = _rotetbl;
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "showroom")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable result = CHNLSVC.CommonSearch.GetInventoryChannel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                Session["popo"] = "true";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
                return;
            }
        }
        #endregion

        #region Modal Popup 2
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            UserPopup.Hide();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }

        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "MRN_App")
            {
                string Maintype = grdResultD.SelectedRow.Cells[3].Text;
                string subtype = grdResultD.SelectedRow.Cells[4].Text;
                txtApproval.Text = Name;
                GetSaveDoc(Name);
                ApprovedPopup.Show();
                lblvalue.Text = "";
                ApprovedPopup.Show();
                if ((Maintype == "REQD") && (subtype == "NOR"))
                {
                    HiddenPOType.Value = "Normal";
                }
                else if ((Maintype == "REQD") && (subtype == "PRQ"))
                {
                    HiddenPOType.Value = "Purchase";
                }
                else if ((Maintype == "CONSD") && (subtype == "NOR"))
                {
                    HiddenPOType.Value = "Consignment";
                }
                Toatalapprovalqty();
                UserDPopoup.Hide();

                return;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }

        }
        #endregion

        #region item search

        protected void lbtnItemSearch_Click(object sender, EventArgs e)
        {
            ddlitemserchoption.SelectedValue = "9";
            grdItem.DataSource = null;
            grdItem.DataBind();
            MdlItemSear.Show();
        }
        protected void txtsearcItembyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlitemserchoption.SelectedValue == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, "CODE", txtsearcItembyword.Text);
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Similar_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, "CODE", txtsearcItembyword.Text);
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Replace_Item";
                    MdlItemSear.Show();
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnsearcItempop_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlitemserchoption.SelectedValue == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, null, null);
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Similar_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, null, null);
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Replace_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "0")
                {
                    DataTable result1 = CHNLSVC.Inventory.GetComItem(txtItem.Text.ToUpper());
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Com_Item";
                    MdlItemSear.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void grdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string Name = grdItem.SelectedRow.Cells[1].Text;
                txtItem.Text = Name;
                txtPrefexlocation.Text = "";
                txtApprovalqty.Text = "";
                GetInventory(Name, Session["UserCompanyCode"].ToString(), string.Empty);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        #endregion
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

        #region Serach MRN
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbRout.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, "Code", txtsearch.Text.ToUpper());
                    if (_rotetbl.Rows.Count == 0)
                    {
                        string Msg = "Invalid route";
                        txtsearch.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                        return;
                    }

                }
                if (rdbshowroom.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtsearch.Text.ToUpper());
                    if (_result.Rows.Count == 0)
                    {
                        string Msg = "Invalid showroom or no permission";
                        txtsearch.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                        return;
                    }
                }
                if (rdbchannel.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    DataTable result = CHNLSVC.CommonSearch.GetInventoryChannel(SearchParams, "Code", txtsearch.Text.ToUpper());
                    if (result.Rows.Count == 0)
                    {
                        string Msg = "Invalid channel";
                        txtsearch.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnsearchOp_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbRout.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, null, null);
                    grdResult.DataSource = _rotetbl;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_rotetbl);
                    lblvalue.Text = "route";
                    ViewState["SEARCH"] = _rotetbl;
                    UserPopup.Show();
                    return;
                }
                if (rdbshowroom.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "showroom";
                    BindUCtrlDDLData(_result);
                    ViewState["SEARCH"] = _result;
                    UserPopup.Show();
                    return;
                }
                if (rdbchannel.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    DataTable result = CHNLSVC.CommonSearch.GetInventoryChannel(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "channel";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void chkSearchall_CheckedChanged(object sender, EventArgs e)
        {
            txtsearch.Text = string.Empty;
            if (chkSearchall.Checked == false)
            {
                txtsearch.Enabled = true;
            }
            else
            {
                txtsearch.Enabled = false;
            }

        }
        protected void lbtnPreferlocation_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void chkprelocAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprelocAll.Checked == false)
            {
                lbtnPreferlocation.Visible = true;
                txtPreferLocation.Text = string.Empty;
                txtPreferLocation.Enabled = true;
            }
            else
            {
                lbtnPreferlocation.Visible = false;
                txtPreferLocation.Text = string.Empty;
                txtPreferLocation.Enabled = false;
            }
        }
        protected void lbtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string _para = string.Empty;
                if ((rdbRout.Checked == true) && (chkSearchall.Checked == false) && (txtsearch.Text == ""))
                {
                    _para = "R";
                    grdRequestDetails.DataSource = new int[] { };
                    grdRequestDetails.DataBind();
                    DisplayMessage("Please enter route", 2);
                    return;
                }
                if ((rdbshowroom.Checked == true) && (chkSearchall.Checked == false) && (txtsearch.Text == ""))
                {
                    grdRequestDetails.DataSource = new int[] { };
                    grdRequestDetails.DataBind();
                    DisplayMessage("Please enter showroom", 2);
                    return;
                }
                if ((rdbchannel.Checked == true) && (chkSearchall.Checked == false) && (txtsearch.Text == ""))
                {
                    _para = "C";
                    grdRequestDetails.DataSource = new int[] { };
                    grdRequestDetails.DataBind();
                    DisplayMessage("Please enter channel", 2);
                    return;
                }
                if ((rdbRout.Checked == true))
                {
                    _para = "R";
                }
                if ((rdbchannel.Checked == true))
                {
                    _para = "C";

                }
                List<InventoryRequest> _MRNRequest = new List<InventoryRequest>();
                _MRNRequest = CHNLSVC.Inventory.GetMRN_Req(txtPreferLocation.Text.ToUpper(), txtsearch.Text.ToUpper(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), "MRN", _para, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString());

                if (_MRNRequest != null)
                {
                    //foreach (var item in _MRNRequest)
                    //{
                    //    InventoryRequest _inrReqDataTmp = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = item.Itr_req_no }).FirstOrDefault();
                    //    if (_inrReqDataTmp != null)
                    //    {
                    //        item.Itr_req_wp = _inrReqDataTmp.Itr_req_wp;
                    //    }
                    //}
                    grdRequestDetails.DataSource = _MRNRequest.GroupBy(x => x.Itr_req_no).Select(g => g.First());
                    grdRequestDetails.DataBind();
                    UpdateGridRowColor();
                }
                else
                {
                    grdRequestDetails.DataSource = new int[] { };
                    grdRequestDetails.DataBind();
                    string _Msg = "Pending requests not found";
                    DisplayMessage(_Msg, 2);
                }

            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        #endregion


        #region Main Button
        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (chkcomahhoc.Checked == true)
            {
                divUpcompleted.Visible = false;
                lblExcelUploadError.Visible = false;
                Label1.Visible = false;
                lblExcelUploadError.Text = "";
                Label5.Visible = false;
                Control myControl1 = FindControl("lblExcelUploadError");
                if (myControl1 != null)
                {
                    myControl1.Visible = false;
                    //do stuff
                }
                excelUpload.Show();
            }
            else
            {
                DisplayMessage("Please check adhoc", 1);
            }

        }

        protected void llbtncancel_Click(object sender, EventArgs e)
        {
            if (txtACancelconformmessageValue.Value == "No")
            {
                return;
            }
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16035))
            {
                string Msg = "You dont have permission to Update .Permission code : 16025";
                DisplayMessage(Msg, 1);
                return;
            }
            if (string.IsNullOrEmpty(txtApproval.Text))
            {
                string Msg = "Please select approved number";
                DisplayMessage(Msg, 2);
                return;
            }
            int rowsAffected = 0;
            InventoryRequest _invReq = new InventoryRequest();
            _invReq.Itr_session_id = Session["SessionID"].ToString();
            _invReq.Itr_cre_by = Session["UserID"].ToString();
            _invReq.Itr_cre_dt = DateTime.Now;
            _invReq.Itr_mod_by = Session["UserID"].ToString();
            _invReq.Itr_mod_dt = DateTime.Now;
            string _error = "";
            rowsAffected = CHNLSVC.Inventory.CancelDistributionApprovedDocument(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(),
                txtApproval.Text, "C", Session["UserID"].ToString(), DateTime.Now, _invReq, out _error);
            if (rowsAffected == 1)
            {
                string Msg = "Successfully canceled. " + txtApproval.Text;
                DisplayMessage(Msg, 1);
            }
            else
            {
                string Msg = "can not cancel MRN number:" + txtApproval.Text;
                if (_error.Contains("Reservation"))
                {
                    DispMsg("Reservation log data not found !");
                }
                else
                {
                    DisplayMessage(Msg, 2);
                }
            }
        }

        protected void lbtncancelrequest_Click(object sender, EventArgs e)
        {
            bool Ischack = false;
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            // _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            // _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
            if (txtCancelconformmessageValue.Value == "No")
            {
                return;
            }
            try
            {
                foreach (GridViewRow row in grdRequestDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_Req") as CheckBox);
                        if (chkRow.Checked)
                        {
                            Label MRN = (row.Cells[0].FindControl("col_itr_req_no") as Label);
                            int value = CHNLSVC.CustService.Update_ReqHeaderStatus("C", Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), MRN.Text);
                            if (value > 0)
                            {
                                Ischack = true;

                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
            if (Ischack == false)
            {
                DisplayMessage("Please select the S/R Request Number", 2);
                return;
            }
            else
            {
                DisplayMessage("successfully canceled", 3);
                //_MRNRequest = CHNLSVC.Inventory.GetMRN_Req(txtPreferLocation.Text, txtsearch.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), "MRN");
                //grdRequestDetails.DataSource = _MRNRequest.GroupBy(x => x.Itr_req_no).Select(g => g.First());
                //grdRequestDetails.DataBind();
                ViewState["_MRNRequestItem"] = null;
                ViewState["_ApproveItem"] = null;
                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();
                grdMRNReqItem.DataSource = new int[] { };
                grdMRNReqItem.DataBind();

                txtFromDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtsearch.Text = string.Empty;
                txtPreferLocation.Text = string.Empty;
                rdbRout.Checked = true;
                lblsearch.Text = "Route";
                //_MRNRequest = new List<InventoryRequest>();
                _MRNRequestItem = new List<InventoryRequestItem>();
                _Invetory = new List<InventoryLocation>();
                All.Visible = false;

                lblShowroom.Text = string.Empty;
                // txtApproval.Text = string.Empty;
                lblSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                lblrequestNo.Text = string.Empty;
                lblPreferloc.Text = string.Empty;
                lblAuthorizedby.Text = Session["UserID"].ToString();
                txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                txtremark.Text = string.Empty;
                lblCompany.Text = Session["UserCompanyCode"].ToString();
                lblRequesttype.Text = string.Empty;
                Session["Itemcode"] = "";

                txtItem.Text = string.Empty;
                lblModel.Text = string.Empty;
                //txtItemRemark.Text = string.Empty;
                lblshopstock.Text = string.Empty;
                lblForwardsale.Text = string.Empty;
                lblBufferLimit.Text = string.Empty;
                txtRequestqty.Text = string.Empty;
                txtRequestcompany.Text = string.Empty;
                txtPrefexlocation.Text = string.Empty;
                ddlPurchasetype.SelectedIndex = -1;
                txtApprovalqty.Text = string.Empty;
                txtpopupItem.Text = string.Empty;
                //txtpopupModel.Text = string.Empty;ruk
                //txtpopupItemRemark.Text = string.Empty;ruk
                //txtpopupshopstock.Text = string.Empty;ruk
                //txtpopupForwardsale.Text = string.Empty;ruk
                //txtpopupBufferLimit.Text = string.Empty;ruk
                //txtpopupRequestqty.Text = string.Empty;ruk
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.SelectedIndex = -1;
                txtpopupApprovalqty.Text = string.Empty;
            }
        }

        protected void lbtnAmend_Click(object sender, EventArgs e)
        {
            if (txtUpdateconformmessageValue.Value == "No")
            {
                return;
            }
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16035))
            {
                string Msg = "You dont have permission to Update .Permission code : 16035";
                DisplayMessage(Msg, 1);
                return;
            }
            int rowsAffected = 0;
            string _docNo = string.Empty;
            //List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            //_ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;


            if (Convert.ToDateTime(txtDeliverDate.Text) < DateTime.Now.Date)
            {
                DisplayMessage("You cannot select a day earlier than today", 2);
                return;
            }
            if ((_ApproveItem == null) || (_ApproveItem.Count == 0))
            {
                string Msg = "Please select approved MRN ";
                DisplayMessage(Msg, 2);
                return;
            }
            InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = txtApproval.Text }).FirstOrDefault();
            foreach (InventoryRequestItem _req in _ApproveItem)
            {
                _req.Itri_seq_no = _invReq.Itr_seq_no;
                MasterItem _MstItem = new MasterItem();

                _MstItem.Mi_cd = _req.Itri_itm_cd;
                _req.MasterItem = _MstItem;
                _req.Itri_itm_stus = "GOD";
            }
            InventoryRequest REQHDR = new InventoryRequest();
            REQHDR.Itr_req_no = txtApproval.Text.ToString();
            REQHDR.Itr_exp_dt = Convert.ToDateTime(txtDeliverDate.Text.ToString());
            REQHDR.Itr_com = lblCompany.Text.ToString();
            REQHDR.Temp_itr_chnl_allocation = true;
            #region update detail lvl job no add by lakshan 20Oct2017
            InventoryRequest _itrReqRef = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = REQHDR.Itr_ref }).FirstOrDefault();
            string _baseDocJobNo = "";
            Int32 _baseDocJobLine = 0;
            if (_itrReqRef != null)
            {
                _baseDocJobNo = _itrReqRef.Itr_job_no;
                _baseDocJobLine = _itrReqRef.Itr_job_line;
            }
            foreach (var item in _ApproveItem)
            {
                item.Itri_job_no = _baseDocJobNo;
                item.Itri_job_line = _baseDocJobLine;
            }
            #endregion
            rowsAffected = CHNLSVC.Inventory.SaveMRNRequestApproveamend(_ApproveItem, REQHDR, out _docNo);

            if (rowsAffected != -1)
            {
                string Msg = "Successfully Updated. " + _docNo;
                DisplayMessage(Msg, 3);
                pageclear();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {

            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            #region add by lakshan 09Mar2018
            bool _showPopMsg = false;
            HpSystemParameters _sysPara2 = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
            if (_sysPara2 != null)
            {
                if (_sysPara2.Hsy_val == 1)
                {
                    decimal _tmpFreeQty = 0, _tmpMrnQty = 0;
                    _tmpFreeQty = decimal.TryParse(txtFreeVal.Text, out _tmpFreeQty) ? Convert.ToDecimal(txtFreeVal.Text.Trim()) : 0;
                    _tmpMrnQty = decimal.TryParse(txtCurrMrnVal.Text, out _tmpMrnQty) ? Convert.ToDecimal(txtCurrMrnVal.Text.Trim()) : 0;
                    if (_tmpFreeQty < _tmpMrnQty && !_isValueExceedSave)
                    {
                        _showPopMsg = true;
                    }
                }
            }
            if (_showPopMsg)
            {
                lblMssg.Text = "Location free value exceed !";
                lblMssg1.Text = "Do you want to save  ?";
                PopupConfBox.Show();
                return;
            }
            else
            {
                PopupConfBox.Hide();
            }
            #endregion
            lbtnSave.Enabled = false;
            lbtnSave.CssClass = "buttoncolorleft";
            lbtnSave.OnClientClick = "return Enable();";
            // List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            // _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;

       

            if (_ApproveItem == null)
            {
                _ApproveItem = new List<InventoryRequestItem>();
            }
            if (_ApproveItem.Count == 0)
            {
                DisplayMessage("No approved item to save", 2);
                lbtnSave.Enabled = true;
                lbtnSave.CssClass = "buttonUndocolorLeft";
                lbtnSave.OnClientClick = "SaveConfirm();";
                return;
            }
            if (chkcomahhoc.Checked == true)
            {
                //Process();
                //lbtnSave.Enabled = true;
                //lbtnSave.CssClass = "buttonUndocolorLeft";
                //lbtnSave.OnClientClick = "SaveConfirm();";
                if (_contine == false)
                {
                    string _docNo = string.Empty;
                    string _IntdocNo = string.Empty;
                    int _Ins = 0;
                    bool _validation = true;
                    HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPINS", DateTime.Now.Date);
                    if (_sysPara != null)
                    {
                        if (_sysPara.Hsy_val == 0)
                        {
                            _validation = false;
                        }
                    }
                    if (_validation)
                    {
                        CHNLSVC.Inventory.checkInsuvaluExcel(_ApproveItem, lblCompany.Text, _contine, out _docNo, out _Ins, out _IntdocNo);
                    }
                    if (_Ins == 1)
                    {
                        lblMssg.Text = "Location insurance value exceeding";
                        btnok.Enabled = true;
                        PopupConfBox.Show();
                    }
                    else
                    {
                        Process();
                        lbtnSave.Enabled = true;
                        lbtnSave.CssClass = "buttonUndocolorLeft";
                        lbtnSave.OnClientClick = "SaveConfirm();";

                        lblMssg.Text = "Location insurance value exceeding";
                        btnok.Enabled = true;
                        PopupConfBox.Show();
                    }

                }
                else
                {
                    Process();
                    lbtnSave.Enabled = true;
                    lbtnSave.CssClass = "buttonUndocolorLeft";
                    lbtnSave.OnClientClick = "SaveConfirm();";

                    lblMssg.Text = "Location insurance value exceeding";
                    btnok.Enabled = true;
                    PopupConfBox.Show();
                }
            }
            else
            {
                foreach (InventoryRequestItem _itm in _ApproveItem)
                {
                    _itm.Itri_bqty = _itm.Itri_app_qty;
                    _itm.Itri_qty = _itm.Itri_app_qty;
                }
                bool _result = true;
                bool _validation = true;
                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPMRN", DateTime.Now.Date);
                if (_sysPara != null)
                {
                    if (_sysPara.Hsy_val == 0)
                    {
                        _validation = false;
                    }
                }
                if (_validation)
                {
                    _result = CHNLSVC.Inventory.Check_MRN_Item_exceed_Ins(_ApproveItem, lblCompany.Text, lblShowroom.Text, DateTime.Now.Date);
                }
                if (_result == true)
                {
                    Process();
                    lbtnSave.Enabled = true;
                    lbtnSave.CssClass = "buttonUndocolorLeft";
                    lbtnSave.OnClientClick = "SaveConfirm();";
                }
                else
                {
                    lblMssg.Text = "Location insurance value exceeding";
                    PopupConfBox.Show();
                    // DisplayMessage("Location insurance value exceeding", 2);
                }
            }


        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                    System.Threading.Thread.Sleep(3000);
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

        private void Process()
        {
            _printDoc = "";
            int rowsAffected = 0;
            string _docNo = string.Empty;
            string _IntdocNo = string.Empty;
            string _printAppNo = string.Empty;
            // List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            // _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            //  _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
            DataTable _multipleshowroom = ViewState["showrooms"] as DataTable;
            if (Convert.ToDateTime(txtDeliverDate.Text) < DateTime.Now.Date)
            {
                DisplayMessage("You cannot select a day earlier than today", 2);
                return;
            }
            if (chkcomahhoc.Checked == false)
            {
                if (_MRNRequestItem == null)
                {
                    DisplayMessage("Please select the S/R request number", 2);
                    return;
                }
            }

            if (_ApproveItem == null)
            {
                DisplayMessage("no approved item to save", 2);
                return;
            }

            InventoryRequest _inventoryRequest = new InventoryRequest();

            _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
            _inventoryRequest.Itr_sub_tp = lblRequesttype.Text;
            _inventoryRequest.Itr_loc = lblShowroom.Text;
            _inventoryRequest.Itr_ref = lblrequestNo.Text;
            _inventoryRequest.Itr_dt = CHNLSVC.Security.GetServerDateTime().Date;
            _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtDeliverDate.Text);
            _inventoryRequest.Itr_stus = "A";
            //_inventoryRequest.Itr_job_no = txtrequestNo.Text;
            //_inventoryRequest.Itr_bus_code = lblCusCode.Text;                                  
            _inventoryRequest.Itr_note = txtremark.Text;
            _inventoryRequest.Itr_direct = 0;
            _inventoryRequest.Itr_country_cd = string.Empty;
            _inventoryRequest.Itr_town_cd = string.Empty;
            _inventoryRequest.Itr_cur_code = string.Empty;
            _inventoryRequest.Itr_exg_rate = 0;
            _inventoryRequest.Itr_issue_from = lblPreferloc.Text;
            //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
            //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
            _inventoryRequest.Itr_act = 1;
            _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
            _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();
            _inventoryRequest.Itr_gran_app_by = Session["UserID"].ToString();//Added By Udaya 25.08.2017
            _inventoryRequest.Itr_mod_dt = DateTime.Now;//Added By Udaya 30.10.2017
            //LOAD LOCATION BY SUBODANA
            //string toloc = "";
            //foreach (GridViewRow gvr in this.grdRequestDetails.Rows)
            //{
            //    CheckBox check = (CheckBox)gvr.FindControl("chk_Req");

            //    Label lblLocation = (Label)gvr.FindControl("col_itr_loc");
            //    if (check.Checked)
            //    {
            //        toloc = lblLocation.Text.ToString();

            //    }

            //}

            _inventoryRequest.Itr_rec_to = txtShowroom.Text.ToString();  /*Session["UserDefLoca"].ToString();*/
            foreach (var item in _ApproveItem)
            {
                if (!string.IsNullOrEmpty(item.Itri_res_no) && item.Itri_res_no != "N/A")
                {
                    item.Itri_res_qty = item.Itri_qty;
                }
            }

            _inventoryRequest.InventoryRequestItemList = _ApproveItem;

            //restric intr and reqd

            if (_selectserial != null)
            {
                if (_selectserial.Count > 0)
                {
                    foreach (InventorySerialN _ser in _selectserial)
                    {
                        var _found = _inventoryRequest.InventoryRequestItemList.Where(x => x.Itri_loc == _ser.Ins_loc && x.Itri_itm_cd == _ser.Ins_itm_cd).ToList();
                        if (_found != null)
                        {
                            if (_found.Count > 0)
                            {
                                foreach (InventoryRequestItem _ReqItem in _found)
                                {
                                    _ReqItem.Temp_is_allocation_err = 1;//this tag only save inter trans not
                                }
                            }
                        }
                    }

                }
            }
            #region validate location 03Jul2017 ad  by Lakshan
            MasterLocation _mstLoc = new MasterLocation();
            if (_inventoryRequest.InventoryRequestItemList.Count > 0)
            {
                var _showroom = _inventoryRequest.InventoryRequestItemList.GroupBy(x => new { x.Showroom }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var shor in _showroom)
                {
                    var _SelectItemByshowroom = _inventoryRequest.InventoryRequestItemList.Where(x => x.Showroom == shor.Peo.Showroom).ToList();
                    _mstLoc = CHNLSVC.General.GetAllLocationByLocCode("", shor.Peo.Showroom, 1);
                    if (_mstLoc != null)
                    {
                        if (!_mstLoc.Ml_act)
                        {
                            DispMsg("Location is invalid. Please check the location : " + shor.Peo.Showroom);
                            lbtnSave.Enabled = true;
                            lbtnSave.CssClass = "buttonUndocolorLeft";
                            lbtnSave.OnClientClick = "SaveConfirm();";
                            return;
                        }
                    }
                    else
                    {
                        DispMsg("Location is invalid. Please check the location : " + shor.Peo.Showroom);
                        lbtnSave.Enabled = true;
                        lbtnSave.CssClass = "buttonUndocolorLeft";
                        lbtnSave.OnClientClick = "SaveConfirm();";
                        return;
                    }
                }
            }
            #endregion
            int _Ins = 0;
            _inventoryRequest.Temp_itr_chnl_allocation = true;
            _inventoryRequest.TMP_IS_RES_UPDATE = true;
            _inventoryRequest.TMP_SEND_MAIL = true;
            #region update detail lvl job no add by lakshan 20Oct2017
            InventoryRequest _itrReqRef = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _inventoryRequest.Itr_ref }).FirstOrDefault();
            string _baseDocJobNo = "";
            Int32 _baseDocJobLine = 0;
            if (_itrReqRef != null)
            {
                _baseDocJobNo = _itrReqRef.Itr_job_no;
                _baseDocJobLine = _itrReqRef.Itr_job_line;
            }
            foreach (var item in _inventoryRequest.InventoryRequestItemList)
            {
                item.Itri_job_no = _baseDocJobNo;
                item.Itri_job_line = _baseDocJobLine;
            }
            _inventoryRequest.Itr_job_no = _baseDocJobNo;
            _inventoryRequest.Itr_job_line = _baseDocJobLine;
            #endregion
            rowsAffected = CHNLSVC.Inventory.SaveMRNRequestApprove(_inventoryRequest, GenerateMasterAutoNumber(), _MRNRequestItem, _multipleshowroom,
                _selectserial, _contine, out _docNo, out _Ins, out _IntdocNo, out _printAppNo);

            if (rowsAffected != -1)
            {
                _printDoc = _printAppNo;
                if (!string.IsNullOrEmpty(_IntdocNo))
                {
                    string Msg2 = "Successfully saved " + _docNo + "|" + "Other doc no :" + _IntdocNo;
                    DisplayMessage(Msg2, 3);
                }
                else
                {

                    string Msg = "Successfully saved. " + _docNo;
                    DisplayMessage(Msg, 3);
                }

                pageclear();
                lblPrintLbl.Text = "Do you want print now?";
                popPrint.Show();
            }
            else
            {
                if (_Ins == 1)
                {
                    lblMssg.Text = "Location insurance value exceeding";
                    PopupConfBox.Show();
                }
                else
                {
                    DisplayMessage(_docNo, 4);
                }

            }
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = lblShowroom.Text; // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            // masterAuto.Aut_moduleid = "REQD";
            masterAuto.Aut_number = 0;
            // masterAuto.Aut_start_char = "REQD";
            masterAuto.Aut_year = null;
            return masterAuto;
        }

        protected void lbtnApprovItem_Click(object sender, EventArgs e)
        {
            // List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            //  _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            if ((_ApproveItem == null) || (_ApproveItem.Count == 0))
            {
                DisplayMessage("There is no approved items found", 2);
                return;
            }
            Toatalapprovalqty();
            ApprovedPopup.Show();
        }
        #endregion
        private void GetSaveDoc(string _doc)
        {
            #region clear data
            grdRequestDetails.DataSource = new int[] { };
            grdRequestDetails.DataBind();
            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();
            grdMRNReqItem.DataSource = new int[] { };
            grdMRNReqItem.DataBind();

            txtFromDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtsearch.Text = string.Empty;
            txtPreferLocation.Text = string.Empty;
            rdbRout.Checked = true;
            lblsearch.Text = "Route";
            //_MRNRequest = null;
            _MRNRequestItem = null;
            _Invetory = null;
            All.Visible = false;
            lbtnShowroom.Visible = true;
            // adhoc.Visible = false;
            txtShowroom.Text = string.Empty;
            LoadLocationData();
            txtApproval.Text = string.Empty;
            txtApproval.Text = string.Empty;
            lblSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lblrequestNo.Text = string.Empty;
            lblPreferloc.Text = string.Empty;
            lblAuthorizedby.Text = Session["UserID"].ToString();
            txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lblCompany.Text = Session["UserCompanyCode"].ToString();
            txtremark.Text = string.Empty;
            //lblcompany.Text = Session["UserCompanyCode"].ToString();
            lblRequesttype.Text = string.Empty;
            Session["Itemcode"] = "";
            Session["Isalreadysave"] = "false";
            txtItem.Text = string.Empty;
            lblModel.Text = string.Empty;
            //txtItemRemark.Text = string.Empty;
            lblshopstock.Text = string.Empty;
            lblForwardsale.Text = string.Empty;
            lblBufferLimit.Text = string.Empty;
            txtRequestqty.Text = string.Empty;
            txtRequestcompany.Text = string.Empty;
            txtPrefexlocation.Text = string.Empty;
            ddlPurchasetype.SelectedIndex = -1;
            txtApprovalqty.Text = string.Empty;

            txtpopupItem.Text = string.Empty;
            // lblpopupModel.Text = string.Empty;
            //txtpopupItemRemark.Text = string.Empty;
            lblpopupshopstock.Text = string.Empty;
            lblpopupForwardsale.Text = string.Empty;
            lblpopupBufferLimit.Text = string.Empty;
            txtpopupRequestqty.Text = string.Empty;
            txtpopupIcompany.Text = string.Empty;
            txtPrefexlocationpopup.Text = string.Empty;
            ddlPurchasetypepopup.SelectedIndex = -1;
            txtpopupApprovalqty.Text = string.Empty;


            #endregion
            _MRNRequestItem = CHNLSVC.Inventory.GetMRN_Req_item(_doc);


            if (_MRNRequestItem.Count > 0)
            {
                List<InventoryRequestItem> _MRNRequestItemnew = new List<InventoryRequestItem>();
                DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(_doc);
                if (dtHeaders != null)
                {
                    txtShowroom.Text = dtHeaders.Rows[0]["itr_rec_to"].ToString();
                    LoadLocationData();
                    txtApproval.Text = dtHeaders.Rows[0]["itr_req_no"].ToString();
                    lblrequestNo.Text = dtHeaders.Rows[0]["itr_ref"].ToString();
                    lblPreferloc.Text = dtHeaders.Rows[0]["itr_loc"].ToString();
                    txtremark.Text = dtHeaders.Rows[0]["itr_note"].ToString();
                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";
                    lblAuthorizedby.Text = dtHeaders.Rows[0]["itr_cre_by"].ToString();
                    txtDeliverDate.Text = Convert.ToDateTime(dtHeaders.Rows[0]["itr_exp_dt"].ToString()).Date.ToString("dd/MMM/yyyy");

                    _MRNRequestItemnew = CHNLSVC.Inventory.GetMRN_Req_pickitm(txtApproval.Text.ToString());

                }
                if (_MRNRequestItemnew.Count > 0)
                {
                    foreach (var mrnitm in _MRNRequestItem)
                    {
                        int COUNT = _MRNRequestItemnew.Where(a => a.Itri_itm_cd == mrnitm.Itri_itm_cd).Count();
                        if (COUNT > 0)
                        {
                            mrnitm.Itri_mqty = _MRNRequestItemnew.Where(a => a.Itri_itm_cd == mrnitm.Itri_itm_cd).First().Itri_qty;
                        }

                    }
                }

                _MRNRequestItem = _MRNRequestItem.GroupBy(l => new { l.Itri_mitm_cd, l.Mst_item_model })
.Select(cl => new InventoryRequestItem
{
    Itri_itm_cd = cl.First().Itri_itm_cd,
    Itri_line_no = cl.First().Itri_line_no,
    Mst_item_model = cl.First().Mst_item_model,
    Itri_note = cl.First().Itri_note,
    Itri_res_no = cl.First().Itri_res_no,
    Itri_shop_qty = cl.Sum(A => A.Itri_shop_qty),
    Itri_fd_qty = cl.Sum(A => A.Itri_fd_qty),
    Itri_buffer = cl.First().Itri_buffer,
    Itri_qty = cl.Sum(A => A.Itri_qty),
    Itri_bqty = cl.Sum(A => A.Itri_bqty),
    Itri_com = cl.First().Itri_com,
    Itri_loc = cl.First().Itri_loc,
    Itri_app_qty = cl.Sum(A => A.Itri_app_qty),
    Approv_status = cl.First().Approv_status,
    Itri_mqty = cl.Sum(A => A.Itri_mqty),
}).ToList();



                grdApprovMRNitem.DataSource = _MRNRequestItem;
                grdApprovMRNitem.DataBind();
                LoadGridUnitCost(_MRNRequestItem);
                // ViewState["_ApproveItem"] = _MRNRequestItem;
                _ApproveItem = _MRNRequestItem;
                txtpopupItem.Text = string.Empty;
                //txtpopupModel.Text = string.Empty;
                lblpopupshopstock.Text = string.Empty;
                lblpopupForwardsale.Text = string.Empty;
                lblpopupBufferLimit.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                txtpopupApprovalqty.Text = string.Empty;
                Session["Isalreadysave"] = "true";
                Session["RequestCompany"] = lblCompany.Text;
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label3.Visible = false;
            lblExcelUploadError.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {

                    Label3.Visible = true;
                    Label3.Text = "Please select a valid excel (.xls) file";
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                bool validLoc = true;
                DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
                if (GetExecelTbl != null)
                {
                    // ViewState["_ApproveItem"] = "";
                    DataRow dr = null;
                    DataTable _Showroom = new DataTable();
                    _Showroom.Columns.Add(new DataColumn("Showroom", typeof(string)));

                    if (GetExecelTbl[0].Rows.Count > 0)
                    {

                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {
                                InventoryRequestItem _InventoryRequestItem = new InventoryRequestItem();
                                MasterItem _MstItem = new MasterItem();
                                if (_ApproveItem == null)
                                {
                                    _ApproveItem = new List<InventoryRequestItem>();
                                }
                                dr = _Showroom.NewRow();
                                dr["Showroom"] = GetExecelTbl[0].Rows[i][1].ToString();
                                _Showroom.Rows.Add(dr);
                                _InventoryRequestItem.Itri_qty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                                _InventoryRequestItem.Itri_bqty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                                _InventoryRequestItem.Itri_app_qty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                                _InventoryRequestItem.Itri_itm_cd = GetExecelTbl[0].Rows[i][4].ToString().Trim().ToUpper();
                                _InventoryRequestItem.Itri_itm_stus = "GOD";
                                _InventoryRequestItem.Itri_note = "excel";
                                _InventoryRequestItem.PoType = "";
                                _MstItem.Mi_cd = GetExecelTbl[0].Rows[i][4].ToString();
                                _InventoryRequestItem.MasterItem = _MstItem;
                                _InventoryRequestItem.Itri_loc = GetExecelTbl[0].Rows[i][3].ToString();
                                _InventoryRequestItem.Itri_com = GetExecelTbl[0].Rows[i][2].ToString();
                                _InventoryRequestItem.Showroom = GetExecelTbl[0].Rows[i][1].ToString();


                                MasterLocation _mstLoc = CHNLSVC.General.GetAllLocationByLocCode("", _InventoryRequestItem.Showroom, 1);
                                if (_mstLoc != null)
                                {
                                    if (!_mstLoc.Ml_act)
                                    {
                                        lblExcelUploadError.Visible = true;
                                        lblExcelUploadError.Text = "Location is invalid. Please check the location : " + " " + _InventoryRequestItem.Showroom +"row is :"+ i.ToString();
                                        excelUpload.Show();
                                        validLoc = false;
                                    }
                                }
                                else
                                {
                                    lblExcelUploadError.Visible = true;
                                    lblExcelUploadError.Text = "Location is invalid. Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                    excelUpload.Show();
                                    validLoc = false;
                                }
                                DataTable restrictedTable = new DataTable();//CHNLSVC.General.GetRestrictedMrnLoc(_InventoryRequestItem.Showroom, _MstItem.Mi_cd, DateTime.Now.Date.ToString());
                                foreach (DataRow dataRow in restrictedTable.Rows)
                                {
                                    MasterItem mst = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _MstItem.Mi_cd);

                                    if (mst == null)
                                    {
                                        lblExcelUploadError.Visible = true;
                                        lblExcelUploadError.Text = "ItemCode is invalid at line no " + i.ToString();
                                        excelUpload.Show();
                                        return;
                                    }
                                    string itemCode = dataRow["mrq_itm_cd"].ToString();
                                    string cat01 = dataRow["mrq_cat1"].ToString();
                                    string cat02 = dataRow["mrq_cat2"].ToString();
                                    string cat03 = dataRow["mrq_cat3"].ToString();
                                    string cat04 = dataRow["mrq_cat4"].ToString();
                                    string cat05 = dataRow["mrq_cat5"].ToString();
                                    string brand = dataRow["mrq_brd"].ToString();
                                    string qty = dataRow["mrq_qty"].ToString();

                                    if (string.IsNullOrEmpty(itemCode) && string.IsNullOrEmpty(cat01) &&
                                        string.IsNullOrEmpty(cat02) &&
                                        string.IsNullOrEmpty(cat03) &&
                                        string.IsNullOrEmpty(cat04) &&
                                        string.IsNullOrEmpty(cat05) &&
                                        string.IsNullOrEmpty(brand) &&
                                        string.IsNullOrEmpty(qty))
                                    {
                                        lblExcelUploadError.Visible = true;
                                        lblExcelUploadError.Text = "Location is restricted  " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                        excelUpload.Show();
                                        return;
                                    }

                                    if (!(itemCode.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cd.Equals(itemCode))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "ItemCode :" + mst.Mi_cd + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(cat01.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cate_1.Equals(cat01))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Category 01 :" + mst.Mi_cate_1 + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(cat02.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cate_2.Equals(cat02))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Category 02 :" + mst.Mi_cate_2 + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(cat03.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cate_3.Equals(cat01))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Category 02 :" + mst.Mi_cate_3 + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(cat04.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cate_4.Equals(cat04))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Category 04 :" + mst.Mi_cate_4 + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(cat05.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_cate_5.Equals(cat05))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Category 05:" + mst.Mi_cate_5 + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(brand.Equals(string.Empty)))
                                    {
                                        if (mst.Mi_brand.Equals(brand))
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Brand :" + mst.Mi_brand + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }
                                    if (!(qty.Equals(string.Empty)))
                                    {
                                        decimal bufferQty = Convert.ToDecimal(qty);
                                        if (_InventoryRequestItem.Itri_qty > bufferQty)
                                        {
                                            lblExcelUploadError.Visible = true;
                                            lblExcelUploadError.Text = "Quantity :" + _InventoryRequestItem.Itri_qty + " is restricted for the location." + " Please check the location : " + " " + _InventoryRequestItem.Showroom + "row is :" + i.ToString();
                                            excelUpload.Show();
                                        }
                                    }

                                }

                                //if (restrictedTable.Rows.Count > 0)
                                //{
                                //    lblExcelUploadError.Visible = true;
                                //    lblExcelUploadError.Text = "Location's MRN is restricted. Please check the location : " + i.ToString() + " " + _InventoryRequestItem.Showroom;
                                //    excelUpload.Show();
                                //    validLoc = false;
                                //}

                            }
                            catch (Exception ex)
                            {
                                lblExcelUploadError.Visible = false;
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                            }

                        }
                    }
                }
                if (validLoc)
                {
                    divUpcompleted.Visible = true;
                    DisplayMessage("Excel file upload completed. Do you want to process?", 2);
                    excelUpload.Show();
                }
                //Import_To_Grid(FilePath, Extension);
            }
            else
            {
                Label3.Visible = true;
                Label3.Text = "Please select the correct upload file path";
                DisplayMessage("Please select the correct upload file path", 2);
                excelUpload.Show();
                // divalert.Visible = true;
                // lblalert.Text = "Please select an excel file";
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
        protected void btnprocess_Click(object sender, EventArgs e)
        {
            DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
            if (GetExecelTbl != null)
            {
                // ViewState["_ApproveItem"] = "";
                DataRow dr = null;
                DataTable _Showroom = new DataTable();
                _Showroom.Columns.Add(new DataColumn("Showroom", typeof(string)));

                if (GetExecelTbl[0].Rows.Count > 0)
                {

                    // List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();


                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            InventoryRequestItem _InventoryRequestItem = new InventoryRequestItem();
                            MasterItem _MstItem = new MasterItem();
                            //_ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                            if (_ApproveItem == null)
                            {
                                _ApproveItem = new List<InventoryRequestItem>();
                            }
                            dr = _Showroom.NewRow();
                            dr["Showroom"] = GetExecelTbl[0].Rows[i][1].ToString();
                            _Showroom.Rows.Add(dr);
                            _InventoryRequestItem.Itri_qty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                            _InventoryRequestItem.Itri_bqty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                            _InventoryRequestItem.Itri_app_qty = Convert.ToInt32(GetExecelTbl[0].Rows[i][5].ToString());
                            _InventoryRequestItem.Itri_itm_cd = GetExecelTbl[0].Rows[i][4].ToString().Trim().ToUpper();
                            _InventoryRequestItem.Itri_itm_stus = "GOD";
                            _InventoryRequestItem.Itri_note = "excel";
                            _InventoryRequestItem.PoType = "";
                            _MstItem.Mi_cd = GetExecelTbl[0].Rows[i][4].ToString();
                            _InventoryRequestItem.MasterItem = _MstItem;
                            _InventoryRequestItem.Itri_loc = GetExecelTbl[0].Rows[i][3].ToString();
                            _InventoryRequestItem.Itri_com = GetExecelTbl[0].Rows[i][2].ToString();
                            _InventoryRequestItem.Showroom = GetExecelTbl[0].Rows[i][1].ToString();
                            #region add reservation by lakshan 07Nov2017
                            DataRow _dr = GetExecelTbl[0].Rows[i];
                            if (GetExecelTbl[0].Columns.Count > 6)
                            {
                                string _resNo = GetExecelTbl[0].Rows[i][6].ToString();
                                if (!string.IsNullOrEmpty(_resNo) && _resNo != "N/A")
                                {
                                    _InventoryRequestItem.Itri_res_no = GetExecelTbl[0].Rows[i][6].ToString();
                                    if (string.IsNullOrEmpty(GetExecelTbl[0].Rows[i][7].ToString()))
                                    {
                                        Label5.Visible = true;
                                        Label5.Text = "Excel Data Invalid Please enter item status";
                                        excelUpload.Show();
                                    }
                                    else
                                    {
                                        var _validSts = _stsList.Where(c => c.Mis_desc == GetExecelTbl[0].Rows[i][7].ToString()).FirstOrDefault();
                                        if (_validSts == null)
                                        {
                                            Label5.Visible = true;
                                            Label5.Text = "Excel Data Invalid Please check the item status : " + GetExecelTbl[0].Rows[i][7].ToString();
                                            excelUpload.Show();
                                        }
                                        else
                                        {
                                            _InventoryRequestItem.Itri_itm_stus = _validSts.Mis_cd;
                                        }
                                    }
                                    _InventoryRequestItem.Itri_res_no = GetExecelTbl[0].Rows[i][6].ToString();
                                    _InventoryRequestItem.Itri_res_qty = _InventoryRequestItem.Itri_qty;
                                }
                            }
                            #endregion
                            _ApproveItem.Add(_InventoryRequestItem);
                            // ViewState["_ApproveItem"] = _ApproveItem;

                            MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_InventoryRequestItem.Itri_itm_cd);
                            if (_mstItm == null)
                            {
                                DisplayMessage("Item Code Invalid :" + _InventoryRequestItem.Itri_itm_cd, 2);
                                Label3.Visible = true;
                                Label3.Text = "Item Code Invalid :" + _InventoryRequestItem.Itri_itm_cd;
                                excelUpload.Show();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            Label3.Visible = true;
                            Label3.Text = "Excel Data Invalid Please check Excel File and Upload";
                            excelUpload.Show();
                        }
                    }
                    var _sheckduplicate = _ApproveItem.GroupBy(x => new { x.Itri_itm_cd, x.Itri_loc, x.Itri_com, x.Showroom }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var shor in _sheckduplicate)
                    {
                        if (shor.theCount > 1)
                        {
                            Label3.Visible = true;
                            Label3.Text = "Invalid data found from the excel sheet. Please check data ...!";
                            excelUpload.Show();
                            return;
                        }
                        //DisplayMessage("Invalid data found from the excel sheet. Please check data ...! ", 2);

                    }
                    ViewState["showrooms"] = _Showroom;
                }
            }
        }

        protected void chk_Req_CheckedChanged_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGridRowColor();
                if (grdRequestDetails.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        #region add by Lakshan 11 Nov 2016
                        Label col_itr_req_no = row.FindControl("col_itr_req_no") as Label;
                        InventoryRequest _inrReqDataTmp = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = col_itr_req_no.Text }).FirstOrDefault();
                        if (_inrReqDataTmp != null)
                        {
                            if (_inrReqDataTmp.Itr_req_wp == 1)
                            {
                                if (_inrReqDataTmp.Itr_req_wp_usr != Session["UserID"].ToString())
                                {
                                    checkbox.Checked = false;
                                    string _msg = "Request is already processed by user :" + _inrReqDataTmp.Itr_req_wp_usr;
                                    DispMsg(_msg, "N");
                                    return;
                                }
                            }
                        }
                        #endregion
                        //EMPTY  grdApprovMRNitem
                        _ApproveItem = null;
                        // lbtnShowroom.Visible = false;
                        // adhoc.Visible = false;
                        string Reqno = (row.FindControl("col_itr_req_no") as Label).Text;
                        string Showroom = (row.FindControl("col_itr_loc") as Label).Text;
                        string ReqType = (row.FindControl("col_itr_sub_tp") as Label).Text;
                        string DeliverDate = (row.FindControl("col_itr_exp_dt") as Label).Text;
                        string remark = (row.FindControl("col_itr_note") as Label).Text;
                        string ploc = (row.FindControl("col_itr_loc") as Label).Text;
                        string DispatchLocation = (row.FindControl("col_itr_issue_from") as Label).Text;
                        string creuser = (row.FindControl("col_itr_cre_by") as Label).Text;
                        DateTime now = DateTime.Now;
                        var startDate = new DateTime(now.Year, now.Month, 1);
                        var endDate = startDate.AddMonths(1).AddDays(-1);
                        DataTable hsdt = CHNLSVC.Sales.GET_HSLIMIT_DATA(ploc, startDate, endDate);

                        if (hsdt.Rows.Count > 0)
                        {
                            decimal numaccount = Convert.ToDecimal(hsdt.Rows[0]["hrs_no_ac"].ToString());
                            decimal accvalue = Convert.ToDecimal(hsdt.Rows[0]["hrs_tot_val"].ToString());
                            lbmaxhpaccounts.Text = numaccount.ToString();
                            lbmaxval.Text = accvalue.ToString();
                        }
                        else
                        {
                            lbmaxhpaccounts.Text = "";
                            lbmaxval.Text = "";
                        }


                        DataTable hsinvdata = CHNLSVC.Sales.GET_HSINVDATA(ploc, startDate, endDate);
                        int i = 0;
                        decimal total = 0;
                        int accountcount = hsinvdata.Rows.Count;
                        lbcurrenthpacc.Text = accountcount.ToString();
                        if (hsinvdata.Rows.Count > 0)
                        {
                            foreach (var invdata in hsinvdata.Rows)
                            {
                                total = total + Convert.ToDecimal(hsinvdata.Rows[i]["totalval"].ToString());
                                i++;
                            }
                        }
                        lbcurrentval.Text = total.ToString();
                        lblAuthorizedby.Text = creuser;
                        lblShowroom.Text = Showroom;
                        lblrequestNo.Text = Reqno;
                        lblRequesttype.Text = ReqType;
                        txtDeliverDate.Text = Convert.ToDateTime(DeliverDate).Date.ToString("dd/MMM/yyyy");
                        txtremark.Text = remark;
                        lblPreferloc.Text = DispatchLocation;
                        txtPrefexlocation.Text = DispatchLocation;
                        txtShowroom.Text = lblShowroom.Text.ToString();
                        LoadLocationData();
                        _MRNRequestItem = CHNLSVC.Inventory.GetMRN_Req_item(Reqno);
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                        DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", Showroom);
                        if (result.Rows.Count > 0)
                        {
                            lblshowroomname.Visible = true;
                            lblshowroomname.Text = result.Rows[0][1].ToString();
                        }
                        decimal totalqty = 0;
                        MasterItem _mstItm = new MasterItem();
                        foreach (InventoryRequestItem _item in _MRNRequestItem)
                        {
                            DataTable pbpl = CHNLSVC.Inventory.GetPriceBookLvl(Session["UserCompanyCode"].ToString());

                            DataTable itmprice = CHNLSVC.Inventory.GetItemPrice(pbpl.Rows[0]["mc_anal7"].ToString(), pbpl.Rows[0]["mc_anal8"].ToString(), _item.Itri_itm_cd, DateTime.Now.Date);
                            if (itmprice.Rows.Count > 0)
                            {
                                _item.Itri_unit_price = Convert.ToDecimal(itmprice.Rows[0][0].ToString());
                            }

                            MasterItem _componentItem = new MasterItem();
                            _componentItem.Mi_cd = _item.Itri_itm_cd;
                            _item.MasterItem = _componentItem;
                            _item.Itri_com = "N/A";
                            _item.Itri_loc = "N/A";
                            //  _item. = 0;
                            _item.Approv_status = "";
                            decimal Bufferqty = GetBufferQty(_item.Itri_itm_cd);
                            decimal Shopqty = GetShopQty(_item.Itri_itm_cd);
                            _item.Itri_shop_qty = Shopqty;
                            _item.Itri_buffer = Bufferqty;
                            if (chkPresaleqty.Checked == true)
                            {
                                bool _chkPrevSals = true;
                                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPPRS", DateTime.Now.Date);
                                if (_sysPara != null)
                                {
                                    if (_sysPara.Hsy_val == 0)
                                    {
                                        _chkPrevSals = false;
                                    }
                                }
                                if (_chkPrevSals)
                                {
                                    _item.Itri_Prev_sales_qty = CHNLSVC.Inventory.GET_PREVIOUS_SALES_QTY(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), _item.Itri_itm_cd, "DO");
                                }
                                else
                                {
                                    _item.Itri_Prev_sales_qty = 0;
                                }
                            }
                            if (chkFwdsale.Checked == true)
                            {
                                bool _chkForSal = true;
                                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPFOS", DateTime.Now.Date);
                                if (_sysPara != null)
                                {
                                    if (_sysPara.Hsy_val == 0)
                                    {
                                        _chkForSal = false;
                                    }
                                }
                                if (_chkForSal)
                                {
                                    _item.Itri_fd_qty = GetForwardsale(lblShowroom.Text, _item.Itri_itm_cd, lblCompany.Text);
                                }
                                else
                                {
                                    _item.Itri_fd_qty = 0;
                                }

                            }
                            if (chkGIT.Checked == true)
                            {
                                bool _chkGit = true;
                                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPGIT", DateTime.Now.Date);
                                if (_sysPara != null)
                                {
                                    if (_sysPara.Hsy_val == 0)
                                    {
                                        _chkGit = false;
                                    }
                                }
                                if (_chkGit)
                                {
                                    _item.Itri_shop_qty = _item.Itri_shop_qty + GetGIT(_item.Itri_itm_cd);
                                }
                                else
                                {
                                    _item.Itri_shop_qty = _item.Itri_shop_qty;
                                }
                            }
                            totalqty = _item.Itri_bqty + totalqty;
                            #region add uom and model to grid by lakshan 23Aug2017
                            _mstItm = CHNLSVC.General.GetItemMaster(_item.Itri_itm_cd);
                            if (_mstItm != null)
                            {
                                _item.Tmp_itm_model = _mstItm.Mi_model;
                                _item.Tmp_itm_uom = _mstItm.Mi_itm_uom;
                            }
                            #endregion
                        }

                        var _filterMRNItem = _MRNRequestItem.Where(x => x.Itri_bqty != 0).ToList();
                        decimal salesTotal = 0;
                        //Added By Dulaj 2018/Jul/17
                        //foreach (var item in _filterMRNItem)
                        //{
                        //    decimal price = item.Itri_unit_price * item.Itri_qty;
                        //    salesTotal = price + salesTotal;
                        //}
                        txtSalesValues.Text = salesTotal.ToString("#,###,##0.00##");

                        foreach (var items in _filterMRNItem)
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
                            salesTotal = Convert.ToDecimal(txtSalesValues.Text);
                            salesTotal = amount + salesTotal;
                            txtSalesValues.Text = salesTotal.ToString("#,###,##0.00##");
                            //  }
                            //
                        }
                        //                       

                        grdMRNReqItem.DataSource = _filterMRNItem.OrderBy(x => x.Itri_itm_cd);
                        grdMRNReqItem.DataBind();
                        ViewState["_MRNRequestItem"] = _filterMRNItem;
                        ViewState["_ApproveItem"] = "";
                        // grdMRNReqItem.SelectedIndex = 0;
                        var roww = (GridViewRow)grdMRNReqItem.SelectedRow;
                        row.BackColor = System.Drawing.Color.LightCyan;
                        // roww.BackColor = System.Drawing.Color.Bisque;
                        grdMRNReqItem.Focus();
                        // lbtnTotalreq.Text = totalqty.ToString();
                        lbtnTotalreq.Text = totalqty.ToString("n");
                        _maxline = _MRNRequestItem.Max(x => x.Itri_line_no);
                        //checkbox.Checked = false;
                        DataTable _tblroot = CHNLSVC.Inventory.Get_Root_Loc(Session["UserCompanyCode"].ToString(), null, Showroom);
                        if (_tblroot != null)
                        {
                            if (_tblroot.Rows.Count > 0)
                            {

                                droproutstock.DataSource = _tblroot;
                                droproutstock.DataTextField = "FRS_CD";
                                droproutstock.DataValueField = "FRS_CD";
                                droproutstock.DataBind();

                                // txtroutstock.Text = _tblroot.Rows[0][0].ToString();
                            }
                        }
                        txtDeliverDate.Focus();
                    }
                    else
                    {
                        Session["Itemcode"] = "";
                        ViewState["_ApproveItem"] = "";
                        //ViewState["_MRNRequestItem"] = "";
                        grdInventoryBalance.DataSource = new int[] { };
                        grdInventoryBalance.DataBind();
                        //grdApprovMRNitem.DataSource = new int[] { };  ruk
                        // grdApprovMRNitem.DataBind();
                        txtItem.Text = string.Empty;
                        lblModel.Text = string.Empty;
                        // txtItemRemark.Text = string.Empty;
                        lblBufferLimit.Text = string.Empty;
                        lblshopstock.Text = string.Empty;
                        lblForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        // txtPrefexlocation.Text = string.Empty;
                        lblsale.Text = "0.00";
                        txtApprovalqty.Text = string.Empty;
                        Addpurchasetype(txtRequestcompany.Text);
                        //ddlPurchasetype.Items.Clear();
                        row.BackColor = System.Drawing.Color.Transparent;

                        _serial = new List<InventorySerialN>();
                        grdRoutserial.DataSource = _serial;
                        grdRoutserial.DataBind();
                        //lbtnTotalreq.Text = string.Empty;
                        //lblshowroomname.Text = string.Empty;
                        //txtrequestNo.Text = string.Empty;
                        //txtPreferloc.Text = string.Empty;
                        //txtremark.Text = string.Empty;
                        //txtShowroom.Text = string.Empty;
                        //txtRequesttype.Text = string.Empty;

                    }
                }
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        private void GetBuferQty_season(string _Item, string _season, int _option)
        {

            _MasterBufferChannel = CHNLSVC.Inventory.GetBufferQty_Season(_Item, Session["UserCompanyCode"].ToString(), lblShowroom.Text, _season, _option);
            if (_MasterBufferChannel.Count > 0)
            {
                if (_option == 1)
                {
                    foreach (MasterBufferChannel _cn in _MasterBufferChannel)
                    {
                        lblBufferLimit.Text = _cn.MBC_QTY.ToString();
                    }

                }
                else if (_option == 2)
                {

                    var result = _MasterBufferChannel.GroupBy(test => test.MBC_SEASON)
                 .Select(grp => grp.First())
                 .ToList();

                    ddlseason.DataSource = result;
                    ddlseason.DataTextField = "mbc_season";
                    ddlseason.DataValueField = "mbc_season";
                    ddlseason.DataBind();
                    grdSeasson.DataSource = result;
                    grdSeasson.DataBind();

                }
                else
                {
                    foreach (MasterBufferChannel _cn in _MasterBufferChannel)
                    {
                        lblBufferLimit.Text = _cn.MBC_QTY.ToString();
                    }
                }
            }

        }

        private decimal GetBufferQty(string _Item)
        {
            decimal qty = 0;
          //  DataTable result = CHNLSVC.Inventory.GetBufferQty(lblCompany.Text, lblShowroom.Text, _Item, System.DateTime.Now);
           // if ((result != null) && (result.Rows.Count > 0))
           // {
           //     qty = Convert.ToDecimal(result.Rows[0]["BUFFER_QTY"].ToString());
             qty = CHNLSVC.Inventory.GetBufferLevelInrLocation(lblCompany.Text, _Item, lblShowroom.Text);
            if(qty==0)
            {
                DataTable result = CHNLSVC.Inventory.GetBufferQty(lblCompany.Text, lblShowroom.Text, _Item, System.DateTime.Now);
                if ((result != null) && (result.Rows.Count > 0))
                {
                    qty = Convert.ToDecimal(result.Rows[0]["BUFFER_QTY"].ToString());
                }
            }
            
            //}
            return qty;
        }
        private decimal GetShopQty(string _Item)
        {
            decimal qty = 0;
            DataTable result = CHNLSVC.Inventory.GetShopQty(lblCompany.Text, lblShowroom.Text, _Item);
            if ((result != null) && (result.Rows.Count > 0))
            {
                string stringqty = result.Rows[0]["INV_BAL"].ToString();
                if (stringqty != "")
                {
                    qty = Convert.ToDecimal(stringqty);
                }



            }
            return qty;
        }
        private decimal GetForwardsale(string _showroom, string _Item, string _com)
        {
            decimal functionReturnValue = 0;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(_com, _showroom);
            functionReturnValue = CHNLSVC.Inventory.GetForwardsaleNew(_showroom, _Item, _com, _mstLoc.Ml_def_pc);
            return functionReturnValue;
        }
        private decimal GetGIT(string _Item)
        {
            decimal _Gitqty = 0;
            DataTable _GitTbl = CHNLSVC.General.GetItemGIT(lblCompany.Text, lblShowroom.Text, _Item, null, null, null, null, null, null, 0);
            if (_GitTbl.Rows.Count > 0)
            {
                _Gitqty = Convert.ToDecimal(_GitTbl.Rows[0]["iti_qty"].ToString());
            }
            return _Gitqty;
        }
        private void GetInventory(string _Itemcode, string _com, string _status)
        {
            if (_Itemcode == null)
            {
                DisplayMessage("Please select Item...!", 2);
                return;
            }
            _Invetory = CHNLSVC.Inventory.GetItemInventoryBalance(_com, lblShowroom.Text, _Itemcode, _status);
            if (_Invetory != null)
            {
                _Invetory = _Invetory.Where(c => c.Inl_qty > 0 || c.Inl_res_qty > 0 || c.Inl_free_qty > 0).ToList();
            }
            List<InventoryLocation> _subLoc = new List<InventoryLocation>();
            _subLoc = CHNLSVC.Inventory.GETWH_INV_BALANCENew(_com, "", _Itemcode, _status);
            if (_subLoc != null)
            {
                _subLoc = _subLoc.Where(c => c.Inl_qty > 0 || c.Inl_res_qty > 0 || c.Inl_free_qty > 0).ToList();
            }


            if ((_Invetory != null) || (_subLoc != null))
            {
                if (_Invetory == null)
                {
                    _Invetory = _subLoc;
                }
                else if (_subLoc != null)
                {
                    _Invetory.AddRange(_subLoc);

                }

                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (oItemStaus != null && oItemStaus.Count > 0)
                {
                    foreach (InventoryLocation _loc in _Invetory)
                    {
                        _loc.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _loc.Inl_itm_stus).Mis_desc;
                    }

                }

                grdInventoryBalance.DataSource = _Invetory;
                grdInventoryBalance.DataBind();
            }
            else
            {
                DisplayMessage("Please check inventory balance", 2);
                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();

                return;
            }
        }
        private void Toatalapprovalqty()
        {
            decimal tqty = 0;
            foreach (GridViewRow row in grdApprovMRNitem.Rows)
            {
                Label qty = row.FindControl("col_Aitri_app_qty") as Label;
                tqty = tqty + Convert.ToDecimal(qty.Text);
            }
            lbtnapptotal.Text = tqty.ToString("n");
        }
        private void Addpurchasetype(string company)
        {
            ddlPurchasetype.Items.Clear();
            if (company == Session["UserCompanyCode"].ToString())
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Normal", "1"));
                ddlPurchasetype.Items.AddRange(items.ToArray());

                return;
            }
            else
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("---select---", "0"));
                items.Add(new ListItem("Purchase", "1"));
                items.Add(new ListItem("Consignment", "2"));
                ddlPurchasetype.Items.AddRange(items.ToArray());

                return;
            }
        }

        protected void txtDeliverDate_TextChanged(object sender, EventArgs e)
        {
            // DateTime.ParseExact(txtDeliverDate.Text, "dd MM yyyy", null);// will throw an exception if it fails

            if (Regex.IsMatch(txtDeliverDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);
                if (txtremark.Text == "")
                {
                    txtremark.Text = "N/A";
                }
                // txtremark.Text = "N/A";
                txtremark.Focus();
            }
            else
            {
                txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                string _Msg = "Please enter valid date.";
                DisplayMessage(_Msg, 2);
            }
        }
        protected void txtremark_TextChanged(object sender, EventArgs e)
        {
            if (grdMRNReqItem.Rows.Count > 0)
            {
                grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
            }

        }
        protected void grdMRNReqItem_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var c = e.Row.FindControl("col_Approv_status") as Label;
            if (c != null)
            {
                if (c.Text == "Cancel")
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.Red;

                if (c.Text == "Approved")
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.Green;
            }
            var d = e.Row.FindControl("col_Price") as Label;
            if (d != null)
            {
                if (Convert.ToDecimal(d.Text.ToString()) >= 75000)
                    e.Row.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                ((CheckBox)e.Row.FindControl("chk_ReqItem")).TabIndex = (short)(e.Row.RowIndex + 102);
        }
        private bool checkLocation(string _loc)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "code", _loc);
            if ((_result == null) || (_result.Rows.Count == 0))
            {
                DisplayMessage("Please type correct Location..!", 2);
                txtPrefexlocation.Text = string.Empty;
                txtPrefexlocation.Focus();
                return false;
            }
            return true;
        }
        private void checkcompany(string _com)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "code", _com);
            if ((_result == null) && (_result.Rows.Count == 0))
            {

                DisplayMessage("Please type correct company..!", 2);
                txtRequestcompany.Text = string.Empty;
                txtRequestcompany.Focus();
                return;

            }
            bool Isalreaysave = Convert.ToBoolean(Session["Isalreadysave"].ToString());
            if (Isalreaysave == true)
            {
                ApprovedPopup.Show();
            }
        }
        private bool BindItemComponent(string _item)
        {
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(_item.ToUpper());

            if (_masterItemComponent != null)
            {
                if (_masterItemComponent.Count > 0)
                {
                    return true;
                }

            }
            return false;
        }

        private void GetLASTMONTHSALE(string _com, string _pc, DateTime _from, DateTime _to, string _item)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            DataTable result1 = CHNLSVC.Sales.GetLASTMONTHSALE(_com, _pc, firstDayOfMonth, lastDayOfMonth, _item);
            if (result1.Rows.Count > 0)
            {
                decimal value = Convert.ToDecimal(result1.Rows[0][0].ToString());
                lblsale.Text = value.ToString("#,##0.00");
            }
        }
        protected void chk_ReqItem_CheckedChanged_Click(object sender, EventArgs e)
        {
            //if (chkadhoc.Checked == true)
            //{
            //    DisplayMessage("Please untick Ad-hoc", 2);
            //    return;
            //}
            try
            {
                if (grdMRNReqItem.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        // txtItem.Enabled = false;
                        string ItemCode = (row.FindControl("col_ITRI_ITM_CD") as Label).Text;
                        string ItemModel = (row.FindControl("col_mi_model") as Label).Text;
                        string ItemRemark = (row.FindControl("col_itri_note") as Label).Text;
                        string Req_qty = (row.FindControl("col_itri_bqty") as Label).Text;
                        string itemline = (row.FindControl("col_itri_line_no") as Label).Text;
                        string shopqty = (row.FindControl("col_itri_shop_qty") as Label).Text;
                        string fdqty = (row.FindControl("col_itri_fd_qty") as Label).Text;
                        string bufferqty = (row.FindControl("col_itri_buffer") as Label).Text;
                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), ItemCode);
                        if (_itemdetail != null)
                        {
                            lblitemdes.Text = _itemdetail.Mi_longdesc;
                        }
                        DataTable dtdiscontinue = CHNLSVC.Inventory.CheckIsItemDiscontinue(ItemCode);

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
                            string _Msg = "This is a discontinued item";
                            DisplayMessage(_Msg, 1);
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('This is a discontinued item !!!');", true);
                        }

                        txtItem.Text = ItemCode;
                        MasterItemSimilar _simItm = new MasterItemSimilar();
                        _simItm.Misi_itm_cd = txtItem.Text.Trim();
                        _simItm.Tmp_iss_com = Session["UserCompanyCode"].ToString();
                        _simItm.Tmp_req_com = Session["UserCompanyCode"].ToString();
                        _simItm.Tmp_iss_loc = txtPrefexlocation.Text.Trim();
                        _simItm.Tmp_req_loc = txtShowroom.Text.Trim();
                        _simItm.Misi_pc = txtShowroom.Text.Trim();
                        List<MasterItemSimilar> _simItmList = CHNLSVC.Inventory.GetSimilerItemBalanceData(_simItm, Session["UserCompanyCode"].ToString());
                        if (_simItmList.Count > 0)
                        {
                            dgvSimilerItm.DataSource = _simItmList;
                            dgvSimilerItm.DataBind();
                            Label lblIssHdr = dgvSimilerItm.HeaderRow.FindControl("lblIssHdr") as Label;
                            Label lblReqHdr = dgvSimilerItm.HeaderRow.FindControl("lblReqHdr") as Label;
                            lblIssHdr.Text = "Bal (N/A)";
                            lblReqHdr.Text = "Bal (N/A)";
                            if (!string.IsNullOrEmpty(txtPrefexlocation.Text))
                            {
                                lblIssHdr.Text = "Bal (" + txtPrefexlocation.Text.ToUpper() + ")";
                            }
                            if (!string.IsNullOrEmpty(txtShowroom.Text))
                            {
                                lblReqHdr.Text = "Bal (" + txtShowroom.Text.ToUpper() + ")";
                            }
                            popSimItm.Show();
                            //return;
                        }
                        lblModel.Text = ItemModel;
                        lblbaseitem.Text = ItemCode;
                        // txtItemRemark.Text = ItemRemark;
                        txtRequestqty.Text = Req_qty;
                        txtApprovalqty.Text = Req_qty;

                        lblshopstock.Text = shopqty;

                        if (chkGIT.Checked)
                        {
                            lblGIT.Text = GetGIT(ItemCode).ToString();
                            lblshop.Text = GetShopQty(ItemCode).ToString();

                        }
                        lblshop.Text = (Convert.ToDecimal(shopqty) - Convert.ToDecimal(lblGIT.Text)).ToString();
                        DateTime todate = DateTime.Now.AddMonths(-3).Date;
                        bool _chkStockBalance = true;
                        HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTAPPSTK", DateTime.Now.Date);
                        if (_sysPara != null)
                        {
                            if (_sysPara.Hsy_val == 0)
                            {
                                _chkStockBalance = false;
                            }
                        }
                        DataTable dt = new DataTable();
                        if (_chkStockBalance)
                        {
                            dt = CHNLSVC.MsgPortal.StockBalanceSearch1(todate, DateTime.Now.Date, ItemCode, txtShowroom.Text.ToString(), Session["UserCompanyCode"].ToString(), false, "DO");
                        }
                        //DataTable dt = CHNLSVC.MsgPortal.StockBalanceSearch1(todate, DateTime.Now.Date, ItemCode, txtShowroom.Text.ToString(), Session["UserCompanyCode"].ToString(), false, "DO");
                        int docount = 0;
                        if (dt.Rows.Count > 0)
                        {
                            int r = 0;

                            foreach (var dtdata in dt.Rows)
                            {
                                if (dt.Rows[r]["DOC_TYPE"].ToString() == "DO")
                                {
                                    docount = docount + Convert.ToInt32(dt.Rows[r]["OUT_COU"].ToString());
                                }

                                r++;
                            }
                        }
                        decimal advanceval = 0;
                        int m = 0;
                        DataTable getpcs = CHNLSVC.Sales.GET_PCFROMLOC(txtShowroom.Text.ToString(), Session["UserCompanyCode"].ToString());
                        if (getpcs.Rows.Count > 0)
                        {
                            bool _chkAdvancedRec = true;
                            _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISTADVREC", DateTime.Now.Date);
                            if (_sysPara != null)
                            {
                                if (_sysPara.Hsy_val == 0)
                                {
                                    _chkAdvancedRec = false;
                                }
                            }
                            DataTable getadreciept = new DataTable();
                            if (_chkAdvancedRec)
                            {
                                getadreciept = CHNLSVC.Sales.GET_ADVRECFORMRN(ItemCode, Session["UserCompanyCode"].ToString(), getpcs.Rows[0][0].ToString(), todate, DateTime.Now.Date);
                            }
                            if (getadreciept.Rows.Count > 0)
                            {

                                foreach (var dtdata in getadreciept.Rows)
                                {
                                    if (getadreciept.Rows[m]["sar_receipt_type"].ToString() == "ADVAN")
                                        advanceval = advanceval + Convert.ToDecimal(getadreciept.Rows[m]["sard_settle_amt"]);
                                    m++;
                                }
                            }
                        }
                        lbadvancerec.Text = advanceval.ToString();
                        lbreccount.Text = m.ToString();
                        lbpreqty.Text = docount.ToString();
                        lblForwardsale.Text = fdqty;
                        lblBufferLimit.Text = bufferqty;
                        GetLASTMONTHSALE(Session["UserCompanyCode"].ToString(), lblShowroom.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), ItemCode);
                        HiddenItemLine.Value = itemline;
                        HiddenItemcode.Value = ItemCode;
                        GetBuferQty_season(ItemCode, "", 1);
                        GetBuferQty_season(ItemCode, "", 2);
                        row.BackColor = System.Drawing.Color.LightCyan;
                        GetInventory(ItemCode, Session["UserCompanyCode"].ToString(), string.Empty);
                        Session["Itemcode"] = ItemCode;
                        Addpurchasetype(Session["UserCompanyCode"].ToString());
                        //txtItemRemark.Text = "N/A" ;
                        // txtItemRemark.Focus();
                        // txtPrefexlocation.Focus();
                        txtItem.Focus();
                    }
                    else
                    {
                        ////Session["Itemcode"] = "";
                        txtItem.Text = string.Empty;
                        ////txtModel.Text = string.Empty;
                        // txtItemRemark.Text = string.Empty;
                        ////txtBufferLimit.Text = string.Empty;
                        ////txtshopstock.Text = string.Empty;
                        ////txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        //txtRequestcompany.Text = string.Empty;
                        //txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        lblbaseitem.Text = "N/A";
                        grdInventoryBalance.DataSource = new int[] { };
                        grdInventoryBalance.DataBind();
                        //ddlPurchasetype.Items.Clear();
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region load similer item 10 Nov 2016 add by Lakshan
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItemSimilar _simItm = new MasterItemSimilar();
                    _simItm.Misi_itm_cd = txtItem.Text.Trim();
                    _simItm.Tmp_iss_com = Session["UserCompanyCode"].ToString();
                    _simItm.Tmp_req_com = Session["UserCompanyCode"].ToString();
                    _simItm.Tmp_iss_loc = txtPrefexlocation.Text.Trim();
                    _simItm.Tmp_req_loc = txtShowroom.Text.Trim();
                    _simItm.Misi_pc = txtShowroom.Text.Trim();
                    List<MasterItemSimilar> _simItmList = CHNLSVC.Inventory.GetSimilerItemBalanceData(_simItm, Session["UserCompanyCode"].ToString());
                    if (_simItmList.Count > 0)
                    {
                        dgvSimilerItm.DataSource = _simItmList;
                        dgvSimilerItm.DataBind();
                        Label lblIssHdr = dgvSimilerItm.HeaderRow.FindControl("lblIssHdr") as Label;
                        Label lblReqHdr = dgvSimilerItm.HeaderRow.FindControl("lblReqHdr") as Label;
                        lblIssHdr.Text = "Bal (N/A)";
                        lblReqHdr.Text = "Bal (N/A)";
                        if (!string.IsNullOrEmpty(txtPrefexlocation.Text))
                        {
                            lblIssHdr.Text = "Bal (" + txtPrefexlocation.Text.ToUpper() + ")";
                        }
                        if (!string.IsNullOrEmpty(txtShowroom.Text))
                        {
                            lblReqHdr.Text = "Bal (" + txtShowroom.Text.ToUpper() + ")";
                        }
                        popSimItm.Show();
                        return;
                    }
                }
                #endregion
                string ItemCode = txtItem.Text.ToUpper();
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), ItemCode);
                if (_itemdetail != null)
                {
                    lblitemdes.Text = _itemdetail.Mi_longdesc;
                    txtPrefexlocation.Text = "";
                    txtApprovalqty.Text = "";
                    GetInventory(ItemCode, Session["UserCompanyCode"].ToString(), string.Empty);
                    txtPrefexlocation.Focus();
                    GetBuferQty_season(ItemCode, "", 2);
                    if (chkGIT.Checked)
                    {

                        lblGIT.Text = GetGIT(ItemCode).ToString();
                        lblshop.Text = GetShopQty(ItemCode).ToString();
                        lblshopstock.Text = (GetGIT(ItemCode) + GetShopQty(ItemCode)).ToString();
                    }
                }
                else
                {
                    txtItem.Text = ItemCode;
                    DisplayMessage("Please check item code", 2);
                    return;
                }

            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }
        protected void btnPrefexLoc_Click(object sender, EventArgs e)
        {
            Session["RequestCompany"] = txtRequestcompany.Text;
            if (checkLocation(txtPrefexlocation.Text))
            {
                Addpurchasetype(txtRequestcompany.Text);
                //ddlPurchasetype.Focus();
                txtApprovalqty.Focus();
                //ApprovedPopup.Show();
                //txtApprovalqty.BackColor = System.Drawing.Color.LightBlue;
            }

        }
        protected void btnPrefexLocpop_Click(object sender, EventArgs e)
        {
            //Session["RequestCompany"] = txtpopupIcompany.Text;
            //if (checkLocation(txtPrefexlocation.Text))
            //{
            //    Addpurchasetype(txtpopupIcompany.Text);
            //    //ddlPurchasetype.Focus();
            //    txtApprovalqty.Focus();
            //    ApprovedPopup.Show();
            //    //txtApprovalqty.BackColor = System.Drawing.Color.LightBlue;
            //}

        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                string _itemcode = txtItem.Text.ToUpper().Trim();
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemcode);
                if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    if ((_itemdetail.Mi_is_ser1 == 1) || (_itemdetail.Mi_is_ser1 == 0))
                    {
                        decimal number = Convert.ToDecimal(txtApprovalqty.Text);
                        decimal fractionalPart = number % 1;
                        if (fractionalPart != 0)
                        {
                            txtApprovalqty.Text = string.Empty;
                            DisplayMessage("only allow numeric value", 2);
                            return;
                        }
                    }
                    if (Convert.ToDecimal(txtApprovalqty.Text.Trim()) < 0)
                    {
                        txtApprovalqty.Text = string.Empty;
                        DisplayMessage("Quantity should be positive value.", 2);
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Please type correct item code", 2);
                    return;
                }
                lbtnAddItem_Click(null, null);

                txtApprovalqty.BackColor = System.Drawing.Color.White;
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
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
        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                string _itemcode = txtItem.Text.ToUpper().Trim();
                string qty = string.Empty;
                #region add validation for request is in process add by Lakshan 11 Nov 2016
                InventoryRequest _inrReqDataTmp = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = lblrequestNo.Text }).FirstOrDefault();
                if (_inrReqDataTmp != null)
                {
                    if (_inrReqDataTmp.Itr_req_wp == 1)
                    {
                        if (_inrReqDataTmp.Itr_req_wp_usr != Session["UserID"].ToString())
                        {
                            string _msg = "Request is already processed by user : " + _inrReqDataTmp.Itr_req_wp_usr;
                            DispMsg(_msg, "N");
                            return;
                        }
                    }
                }
                #endregion

                #region add validation for allocation data chk 02 Nov 2016
                //Added By Dulaj 2018/Jul/19
                if (string.IsNullOrEmpty(txtShowroom.Text))
                {
                    DisplayMessage("Please select a showroom", 2);
                    return;
                }
                //
                MasterLocationNew _mstLocNew = CHNLSVC.General.GetMasterLocations(new MasterLocationNew() { Ml_loc_cd = txtShowroom.Text.Trim().ToUpper(), Ml_com_cd = Session["UserCompanyCode"].ToString() }).FirstOrDefault();
                MasterLocationPriorityHierarchy _locHir = CHNLSVC.General.GET_MST_LOC_INFO_DATA(txtShowroom.Text.Trim().ToUpper(), "CHNL");
                string _chnl = "";
                if (_locHir != null)
                {
                    _chnl = _locHir.Mli_val;
                }
                if (_mstLocNew != null)
                {
                    //Added By Dulaj 2108/Jul/12
                    MasterLocation _mstLoc = CHNLSVC.General.GetAllLocationByLocCode("", txtShowroom.Text.Trim().ToUpper(), 1);
                    if (_mstLoc != null)
                    {
                        if (!_mstLoc.Ml_act)
                        {
                            DispMsg("Location is inactive. Please check the location :" + txtShowroom.Text.Trim().ToUpper()); return;

                        }
                    }
                    else
                    {
                        DispMsg("Location is invalid. Please check the location :" + txtShowroom.Text.Trim().ToUpper()); return;
                    }
                    DataTable restrictedTable = new DataTable();//CHNLSVC.General.GetRestrictedMrnLoc(txtShowroom.Text.Trim().ToUpper(), txtItem.Text.ToUpper().Trim(), DateTime.Now.Date.ToString());
                    foreach (DataRow dataRow in restrictedTable.Rows)
                    {
                        MasterItem mst = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                        if (mst == null)
                        {
                            DispMsg("ItemCode is invalid"); return;
                        }

                        string itemCode = dataRow["mrq_itm_cd"].ToString();
                        string cat01 = dataRow["mrq_cat1"].ToString();
                        string cat02 = dataRow["mrq_cat2"].ToString();
                        string cat03 = dataRow["mrq_cat3"].ToString();
                        string cat04 = dataRow["mrq_cat4"].ToString();
                        string cat05 = dataRow["mrq_cat5"].ToString();
                        string brand = dataRow["mrq_brd"].ToString();
                        string resQty = dataRow["mrq_qty"].ToString();
                        if (string.IsNullOrEmpty(itemCode) && string.IsNullOrEmpty(cat01) &&
                                      string.IsNullOrEmpty(cat02) &&
                                      string.IsNullOrEmpty(cat03) &&
                                      string.IsNullOrEmpty(cat04) &&
                                      string.IsNullOrEmpty(cat05) &&
                                      string.IsNullOrEmpty(brand) &&
                                      string.IsNullOrEmpty(qty))
                        {
                            DispMsg("Location is restricted. Please check the location :" + txtShowroom.Text.Trim().ToUpper()); return;
                            return;
                        }


                        if (!(itemCode.Equals(string.Empty)))
                        {
                            if (mst.Mi_cd.Equals(itemCode))
                            {
                                DispMsg("ItemCode :" + mst.Mi_cd + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;
                            }
                        }
                        if (!(cat01.Equals(string.Empty)))
                        {
                            if (mst.Mi_cate_1.Equals(cat01))
                            {
                                DispMsg("Category 01 :" + mst.Mi_cate_1 + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;
                            }
                        }
                        if (!(cat02.Equals(string.Empty)))
                        {
                            if (mst.Mi_cate_2.Equals(cat02))
                            {

                                DispMsg("Category 02 :" + mst.Mi_cate_2 + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;

                            }
                        }
                        if (!(cat03.Equals(string.Empty)))
                        {
                            if (mst.Mi_cate_3.Equals(cat01))
                            {

                                DispMsg("Category 02 :" + mst.Mi_cate_3 + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;

                            }
                        }
                        if (!(cat04.Equals(string.Empty)))
                        {
                            if (mst.Mi_cate_4.Equals(cat04))
                            {

                                DispMsg("Category 04 :" + mst.Mi_cate_4 + " is restricted for the location." + " Please check the location : " + txtShowroom.Text.Trim().ToUpper()); return;

                            }
                        }
                        if (!(cat05.Equals(string.Empty)))
                        {
                            if (mst.Mi_cate_5.Equals(cat05))
                            {

                                DispMsg("Category 05:" + mst.Mi_cate_5 + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;

                            }
                        }
                        if (!(brand.Equals(string.Empty)))
                        {
                            if (mst.Mi_brand.Equals(brand))
                            {
                                DispMsg("Brand :" + mst.Mi_brand + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;
                            }
                        }
                        if (!(resQty.Equals(string.Empty)) && !(txtApprovalqty.Text.Equals(string.Empty)))
                        {
                            decimal bufferQty = Convert.ToDecimal(resQty);
                            decimal approvedQty = Convert.ToDecimal(txtApprovalqty.Text);
                            if (approvedQty > bufferQty)
                            {
                                DispMsg("Quatity :" + txtApprovalqty.Text + " is restricted for the location." + " Please check the location :  " + txtShowroom.Text.Trim().ToUpper()); return;                               
                            }
                        }
                    }
                   
                    //End
                    if (_mstLocNew.Ml_loc_tp != "WH")
                    {
                        decimal _allAllocationQty = 0;
                        decimal _chnlAllocationQty = 0;
                        decimal _allInvBal = 0;
                        decimal _reqAppQty = 0;
                        decimal _tmpDecimal = 0;
                        List<InventoryAllocateDetails> _allAllocation = new List<InventoryAllocateDetails>();
                        List<InventoryAllocateDetails> _chnlAllocation = new List<InventoryAllocateDetails>();
                        InventoryLocation _inrLocBal = new InventoryLocation();
                        _reqAppQty = decimal.TryParse(txtApprovalqty.Text, out _tmpDecimal) ? Convert.ToDecimal(txtApprovalqty.Text) : 0;
                        _allAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                        {
                            Isa_com = Session["UserCompanyCode"].ToString(),
                            Isa_itm_cd = txtItem.Text.ToUpper().Trim(),
                            //Isa_itm_stus = item.Itri_itm_stus
                        });
                        if (_allAllocation.Count > 0)
                        {
                            _allAllocationQty = _allAllocation.Sum(c => c.Isa_aloc_bqty);
                        }
                        if (_allAllocationQty > 0)
                        {
                            _chnlAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                            {
                                Isa_chnl = _chnl,
                                Isa_com = Session["UserCompanyCode"].ToString(),
                                Isa_itm_cd = txtItem.Text.ToUpper().Trim(),
                                // Isa_itm_stus = item.Itri_itm_stus
                            });
                            if (_chnlAllocation.Count > 0)
                            {
                                _chnlAllocationQty = _chnlAllocation.Sum(c => c.Isa_aloc_bqty);
                            }

                            _allInvBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE_BY_COM(new InventoryLocation()
                            {
                                Inl_com = Session["UserCompanyCode"].ToString(),
                                Inl_loc = txtShowroom.Text.Trim().ToUpper(),
                                Inl_itm_cd = txtItem.Text.ToUpper().Trim()
                                //Inl_itm_stus = item.Itri_itm_stus
                            });
                            //if (_inrLocBal != null)
                            //{
                            //    _allInvBal = _inrLocBal.Inl_free_qty;
                            //}
                            if (_reqAppQty > _chnlAllocationQty)
                            {
                                decimal _availableBalanc = _chnlAllocationQty + (_allInvBal - _allAllocationQty);
                                if (_reqAppQty > _availableBalanc)
                                {
                                    if (_availableBalanc > -1)
                                    {
                                        DispMsg("You cannot exceed the allocation qty. Available Balance : " + (_allInvBal - _allAllocationQty + _chnlAllocationQty)); return;
                                    }
                                    else
                                    {
                                        DispMsg("You cannot exceed the allocation qty. Available Balance : " + _chnlAllocationQty); return;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                bool _result = CHNLSVC.General.Check_MRN_Item(txtRequestcompany.Text, lblShowroom.Text, _itemcode, out qty);
                if (_result == false)
                {
                    DisplayMessage("Unsettled pending qty-" + qty, 2);
                    //return;
                }

                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemcode);
                if (_itemdetail != null)
                {
                    //validation 
                    DataTable _dtSysPara = CHNLSVC.Sales.SP_CHECK_MST_SYS_PARA(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text);
                    DataRow[] result = _dtSysPara.Select("para_stus = 1");
                    //if (result.Length > 0)
                    //{
                    //    foreach (DataRow _ROW in _dtSysPara.Rows)
                    //    {
                    //        if (_ROW["para_stus"].ToString() == "1")
                    //        {
                    //            _ROW["para_stus"] = "1";

                    //        }
                    //        else
                    //        {
                    //            _ROW["para_stus"] = "0";
                    //        }
                    //    }
                    //    grdvalid.DataSource = _dtSysPara;
                    //    grdvalid.DataBind();
                    //    Mdlvaliresult.Show();
                    //    return;
                    //}
                    if (lblbaseitem.Text == "N/A")
                    {
                        #region validation
                        if (_ApproveItem == null)
                        {
                            _ApproveItem = new List<InventoryRequestItem>();
                        }
                        if (string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                        {
                            DisplayMessage("Please type item..!", 2);
                            return;
                        }

                        if (string.IsNullOrEmpty(txtApprovalqty.Text))
                        {
                            DisplayMessage("Please type approval quantity", 2);
                            return;
                        }
                        if (txtApprovalqty.Text == ".")
                        {
                            DisplayMessage("Please enter a valid quantity", 2);
                            return;
                        }
                        if (string.IsNullOrEmpty(lblShowroom.Text))
                        {
                            DisplayMessage("Please enter showroom..!", 2);
                            return;
                        }
                        if (string.IsNullOrEmpty(txtRequestcompany.Text))
                        {
                            DisplayMessage("Please enter request company..!", 2);
                            return;
                        }

                        if (string.IsNullOrEmpty(txtPrefexlocation.Text) || (txtRequestcompany.Text == "N/A"))
                        {
                            DisplayMessage("Please enter prefer location..!", 2);
                            return;
                        }
                        if (ddlPurchasetype.SelectedValue == "0")
                        {
                            DisplayMessage("Please select purchase type..!", 2);
                            return;
                        }

                        #endregion

                        //New Item
                        // if (chkcomahhoc.Checked == true)
                        //{
                        #region Add new item into MRN request

                        var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == txtItem.Text.ToUpper().ToUpper()
                            && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text && T.Itri_note == "New Item");
                        if (_checkallreadyadd != null)
                        {
                            _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                            _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                            _checkallreadyadd.Itri_loc = txtPrefexlocation.Text;
                            _checkallreadyadd.Itri_com = txtRequestcompany.Text;
                            _checkallreadyadd.Itri_note = "New Item";
                            _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                            _checkallreadyadd.Itri_base_req_no = lblrequestNo.Text;
                            _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                            _checkallreadyadd.Backqty = _checkallreadyadd.Itri_bqty;
                            grdApprovMRNitem.DataSource = _ApproveItem;
                            grdApprovMRNitem.DataBind();
                            LoadGridUnitCost(_ApproveItem);
                            ViewState["_ApproveItem"] = _ApproveItem;
                            txtItem.Text = string.Empty;
                            lblModel.Text = string.Empty;
                            //txtItemRemark.Text = string.Empty;
                            lblBufferLimit.Text = string.Empty;
                            lblshopstock.Text = string.Empty;
                            lblForwardsale.Text = string.Empty;
                            txtRequestqty.Text = string.Empty;
                            txtRequestcompany.Text = string.Empty;
                            txtPrefexlocation.Text = string.Empty;
                            txtApprovalqty.Text = string.Empty;
                            lblbaseitem.Text = "N/A";
                            lblitemdes.Text = string.Empty;
                            lblModel.Text = string.Empty;
                            grdSeasson.DataSource = new int[] { };
                            grdSeasson.DataBind();
                            ddlPurchasetype.Items.Clear();
                            txtPrefexlocation.Text = lblPreferloc.Text;
                            Addpurchasetype(txtRequestcompany.Text);
                            if (grdMRNReqItem.Rows.Count != 0)
                            {
                                grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                            }
                            return;
                        }
                        else
                        {
                            int line = 0;
                            if (_ApproveItem.Count > 0)
                            {
                                line = _ApproveItem.Max(x => x.Itri_line_no);
                            }
                            InventoryRequestItem _item = new InventoryRequestItem();
                            _item.Itri_loc = txtPrefexlocation.Text.ToUpper();
                            _item.Itri_com = txtRequestcompany.Text.ToUpper();
                            //_item.Itri_note = txtItemRemark.Text;
                            _item.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                            _item.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                            _item.PoType = ddlPurchasetype.SelectedItem.Text;
                            _item.Itri_itm_cd = txtItem.Text.ToUpper();
                            _item.Itri_note = "New Item";
                            _item.Mst_item_model = lblModel.Text;
                            _item.Backqty = _item.Itri_bqty;

                            //COM ITEM
                            MasterItem _MstItem = new MasterItem();
                            _MstItem.Mi_cd = txtItem.Text.ToUpper();
                            _item.MasterItem = _MstItem;
                            if (BindItemComponent(txtItem.Text.ToUpper()))
                            {
                                foreach (MasterItemComponent _com in _masterItemComponent)
                                {
                                    InventoryRequestItem _new = new InventoryRequestItem();
                                    _new.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                                    _new.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                                    _new.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                    _new.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                    _new.PoType = ddlPurchasetype.SelectedItem.Text;
                                    _new.Itri_itm_cd = txtItem.Text.ToUpper().ToUpper();
                                    _new.Itri_line_no = line;
                                    _new.Itri_itm_stus = "GOD";

                                    MasterItem _componentItem = new MasterItem();
                                    _componentItem.Mi_cd = _com.ComponentItem.Mi_cd;
                                    _new.MasterItem = _componentItem;


                                    _new.BL = "1";
                                    _new.Itri_itm_cd = _com.ComponentItem.Mi_cd;
                                    _ApproveItem.Add(_new);
                                }

                            }
                            else
                            {
                                _ApproveItem.Add(_item);
                            }







                            grdApprovMRNitem.DataSource = _ApproveItem;
                            grdApprovMRNitem.DataBind();
                            LoadGridUnitCost(_ApproveItem);

                            ViewState["_ApproveItem"] = _ApproveItem;
                            txtItem.Text = string.Empty;
                            lblModel.Text = string.Empty;
                            //txtItemRemark.Text = string.Empty;
                            lblBufferLimit.Text = string.Empty;
                            lblshopstock.Text = string.Empty;
                            lblForwardsale.Text = string.Empty;
                            txtRequestqty.Text = string.Empty;
                            txtRequestcompany.Text = string.Empty;
                            txtPrefexlocation.Text = string.Empty;
                            txtApprovalqty.Text = string.Empty;
                            ddlPurchasetype.Items.Clear();
                            lblbaseitem.Text = "N/A";
                            lblitemdes.Text = string.Empty;
                            lblModel.Text = string.Empty;
                            txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                            Addpurchasetype(txtRequestcompany.Text);
                            grdSeasson.DataSource = new int[] { };
                            grdSeasson.DataBind();
                            txtPrefexlocation.Text = lblPreferloc.Text;
                            if (grdMRNReqItem.Rows.Count != 0)
                            {
                                grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                            }
                            return;
                        }
                        #endregion
                        // }
                        // else
                        // {

                        // }
                    }
                    else
                    {
                        #region validation
                        if (_ApproveItem == null)
                        {
                            _ApproveItem = new List<InventoryRequestItem>();
                        }
                        if (_MRNRequestItem == null)
                        {

                            DisplayMessage("Request Item empty..!", 2);
                            return;


                        }
                        if (string.IsNullOrEmpty(lblShowroom.Text))
                        {
                            DisplayMessage("Please enter showroom..!", 2);
                            return;
                        }
                        if (_MRNRequestItem.Count == 0)
                        {
                            DisplayMessage("No item found..!", 2);
                            return;
                        }
                        if (string.IsNullOrEmpty(txtRequestcompany.Text))
                        {
                            DisplayMessage("Please enter request company..!", 2);
                            return;
                        }

                        if (string.IsNullOrEmpty(txtPrefexlocation.Text))
                        {
                            DisplayMessage("Please enter prefer location..!", 2);
                            return;
                        }
                        if (ddlPurchasetype.SelectedValue == "0")
                        {
                            DisplayMessage("Please select purchase type..!", 2);
                            return;
                        }
                        if (string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                        {
                            DisplayMessage("Please enter item..!", 2);
                            return;
                        }

                        if (string.IsNullOrEmpty(txtApprovalqty.Text))
                        {
                            DisplayMessage("Please enter approval  quantity..!", 2);
                            return;
                        }
                        if (txtApprovalqty.Text == ".")
                        {
                            DisplayMessage("Please enter correct quantity..!", 2);
                            return;
                        }
                        #endregion

                        checkcompany(txtRequestcompany.Text);
                        Session["RequestCompany"] = txtRequestcompany.Text;
                        checkLocation(txtPrefexlocation.Text);
                        decimal _reqty = Convert.ToDecimal(txtRequestqty.Text);
                        decimal _Appqty = Convert.ToDecimal(txtApprovalqty.Text);
                        if (_Appqty == 0)
                        {
                            DisplayMessage("cant approved  zero quantity..!", 2);
                            txtApprovalqty.Text = string.Empty;
                            return;
                        }
                        if (_itemcode == lblbaseitem.Text)
                        {
                            //Request item
                            if (_reqty < _Appqty)
                            {
                                DisplayMessage("You are not permitted to exceed the requested quantity..!", 2);
                                return;
                            }
                            else if (_reqty == _Appqty)
                            {
                                #region
                                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                                if ((_checkItemMRNlist != null))
                                {
                                    var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value) && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                                    if (_checkallreadyadd != null)
                                    {
                                        _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_app_qty;
                                        _checkallreadyadd.Itri_loc = txtPrefexlocation.Text;
                                        _checkallreadyadd.Itri_com = txtRequestcompany.Text;
                                        // _checkallreadyadd.Itri_note = txtItemRemark.Text;
                                        _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                                        _checkallreadyadd.Itri_base_req_no = lblrequestNo.Text;
                                        _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                        _MRNRequestItem.Remove(_checkItemMRNlist);

                                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                                        grdMRNReqItem.DataBind();
                                        grdApprovMRNitem.DataSource = _ApproveItem;
                                        grdApprovMRNitem.DataBind();
                                        LoadGridUnitCost(_ApproveItem);

                                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                        ViewState["_ApproveItem"] = _ApproveItem;
                                        txtItem.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        //txtItemRemark.Text = string.Empty;
                                        lblBufferLimit.Text = "0";
                                        lblbaseitem.Text = "N/A";
                                        lblitemdes.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        grdSeasson.DataSource = new int[] { };
                                        grdSeasson.DataBind();
                                        lblshopstock.Text = "0";
                                        lblForwardsale.Text = "0";
                                        txtRequestqty.Text = "0";
                                        txtApprovalqty.Text = "0";
                                        ddlPurchasetype.Items.Clear();
                                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                        txtPrefexlocation.Text = lblPreferloc.Text;
                                        Addpurchasetype(txtRequestcompany.Text);
                                        if (grdMRNReqItem.Rows.Count != 0)
                                        {
                                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                        }
                                        return;
                                    }
                                    else
                                    {


                                        _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text;
                                        _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                                        // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                                        _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkItemMRNlist.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                                        _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                                        _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                        _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;
                                        _checkItemMRNlist.Itri_buffer = Convert.ToDecimal(lblBufferLimit.Text);
                                        if (BindItemComponent(txtItem.Text.ToUpper()))
                                        {
                                            foreach (MasterItemComponent _com in _masterItemComponent)
                                            {
                                                InventoryRequestItem _new = new InventoryRequestItem();
                                                _new.Itri_app_qty = _checkItemMRNlist.Itri_app_qty;
                                                _new.Itri_base_req_line = _checkItemMRNlist.Itri_base_req_line;
                                                _new.Itri_base_req_no = _checkItemMRNlist.Itri_base_req_no;
                                                _new.Itri_bqty = _checkItemMRNlist.Itri_bqty;
                                                _new.Itri_com = _checkItemMRNlist.Itri_com;
                                                _new.Itri_itm_stus = _checkItemMRNlist.Itri_itm_stus;
                                                _maxline = _maxline + 1;
                                                _new.Itri_line_no = _maxline;
                                                _new.Itri_loc = _checkItemMRNlist.Itri_loc;
                                                _new.Itri_mitm_cd = _checkItemMRNlist.Itri_mitm_cd;
                                                _new.Itri_mitm_stus = _checkItemMRNlist.Itri_mitm_stus;
                                                _new.Itri_qty = _checkItemMRNlist.Itri_qty;
                                                _new.PoType = _checkItemMRNlist.PoType;
                                                _new.Itri_buffer = _checkItemMRNlist.Itri_buffer;
                                                MasterItem _componentItem = new MasterItem();
                                                _componentItem.Mi_cd = _com.ComponentItem.Mi_cd;
                                                _new.MasterItem = _componentItem;


                                                _new.BL = "1";
                                                _new.Itri_itm_cd = _com.ComponentItem.Mi_cd;
                                                _ApproveItem.Add(_new);
                                            }

                                        }
                                        else
                                        {
                                            _ApproveItem.Add(_checkItemMRNlist);
                                        }
                                        _MRNRequestItem.Remove(_checkItemMRNlist);
                                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList(); ;
                                        grdMRNReqItem.DataBind();
                                        grdApprovMRNitem.DataSource = _ApproveItem;
                                        grdApprovMRNitem.DataBind();
                                        LoadGridUnitCost(_ApproveItem);
                                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                        ViewState["_ApproveItem"] = _ApproveItem;
                                        txtItem.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        // txtItemRemark.Text = string.Empty;
                                        lblBufferLimit.Text = "0";
                                        lblshopstock.Text = "0";
                                        lblForwardsale.Text = "0";
                                        txtRequestqty.Text = "0";
                                        txtApprovalqty.Text = "0";
                                        lblbaseitem.Text = "N/A";
                                        lblitemdes.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        grdSeasson.DataSource = new int[] { };
                                        grdSeasson.DataBind();
                                        ddlPurchasetype.Items.Clear();
                                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                        txtPrefexlocation.Text = lblPreferloc.Text;
                                        Addpurchasetype(txtRequestcompany.Text);
                                        if (grdMRNReqItem.Rows.Count != 0)
                                        {
                                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                        }

                                        return;
                                    }

                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                                if ((_checkItemMRNlist != null))
                                {
                                    _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text;
                                    _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                                    // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                                    _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                    // _checkItemMRNlist.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                    _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                                    _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                                    _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                                    _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                    _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;
                                    if (BindItemComponent(txtItem.Text.ToUpper()))
                                    {
                                        foreach (MasterItemComponent _com in _masterItemComponent)
                                        {
                                            InventoryRequestItem _new = new InventoryRequestItem();
                                            _new.Itri_app_qty = _checkItemMRNlist.Itri_app_qty;
                                            _new.Itri_base_req_line = _checkItemMRNlist.Itri_base_req_line;
                                            _new.Itri_base_req_no = _checkItemMRNlist.Itri_base_req_no;
                                            _new.Itri_bqty = _checkItemMRNlist.Itri_bqty;
                                            _new.Itri_com = _checkItemMRNlist.Itri_com;
                                            _new.Itri_itm_stus = _checkItemMRNlist.Itri_itm_stus;
                                            _new.Itri_line_no = _checkItemMRNlist.Itri_line_no;
                                            _new.Itri_loc = _checkItemMRNlist.Itri_loc;
                                            _new.Itri_mitm_cd = _checkItemMRNlist.Itri_mitm_cd;
                                            _new.Itri_mitm_stus = _checkItemMRNlist.Itri_mitm_stus;
                                            _new.Itri_qty = _checkItemMRNlist.Itri_qty;
                                            _new.PoType = _checkItemMRNlist.PoType;

                                            MasterItem _componentItem = new MasterItem();
                                            _componentItem.Mi_cd = _com.ComponentItem.Mi_cd;
                                            _new.MasterItem = _componentItem;


                                            _new.BL = "1";
                                            _new.Itri_itm_cd = _com.ComponentItem.Mi_cd;
                                            _ApproveItem.Add(_new);
                                        }

                                    }
                                    else
                                    {
                                        _ApproveItem.Add(_checkItemMRNlist);
                                    }


                                    decimal _qty = _reqty - _Appqty;
                                    foreach (var item in _MRNRequestItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                                    {
                                        item.Itri_com = txtRequestcompany.Text;
                                        item.Itri_loc = txtPrefexlocation.Text;
                                        item.Itri_bqty = item.Itri_bqty - Convert.ToDecimal(txtApprovalqty.Text);
                                        item.Approv_status = "Approved";
                                        item.Itri_qty = _qty;
                                    }
                                    var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                    grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                                    grdMRNReqItem.DataBind();
                                    grdApprovMRNitem.DataSource = _ApproveItem;
                                    grdApprovMRNitem.DataBind();
                                    LoadGridUnitCost(_ApproveItem);
                                    ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                    ViewState["_ApproveItem"] = _ApproveItem;
                                    foreach (GridViewRow row in grdMRNReqItem.Rows)
                                    {
                                        if (row.RowType == DataControlRowType.DataRow)
                                        {
                                            Label itemcode = row.FindControl("col_ITRI_ITM_CD") as Label;
                                            Label itemlineno = row.FindControl("col_itri_line_no") as Label;
                                            if ((itemcode.Text == HiddenItemcode.Value) && (itemlineno.Text == HiddenItemLine.Value))
                                            {
                                                row.BackColor = row.BackColor = System.Drawing.Color.Beige;
                                            }

                                        }
                                    }
                                    txtItem.Text = string.Empty;
                                    lblModel.Text = string.Empty;
                                    // lblItemRemark.Text = string.Empty;
                                    lblBufferLimit.Text = string.Empty;
                                    lblshopstock.Text = string.Empty;
                                    lblForwardsale.Text = string.Empty;
                                    txtRequestqty.Text = string.Empty;
                                    txtRequestcompany.Text = string.Empty;
                                    txtPrefexlocation.Text = string.Empty;
                                    txtApprovalqty.Text = string.Empty;
                                    ddlPurchasetype.Items.Clear();
                                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                    txtPrefexlocation.Text = lblPreferloc.Text;
                                    lblbaseitem.Text = "N/A";
                                    lblitemdes.Text = string.Empty;
                                    lblModel.Text = string.Empty;
                                    grdSeasson.DataSource = new int[] { };
                                    grdSeasson.DataBind();
                                    Addpurchasetype(txtRequestcompany.Text);
                                    if (grdMRNReqItem.Rows.Count != 0)
                                    {
                                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                    }
                                    return;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            //Similer item or replace item or combine item
                            if (!checkSimilrItem(_itemcode))
                            {
                                if (!checkReplaceItem(_itemcode))
                                {
                                    if (!BindItemComponent(_itemcode))
                                    {
                                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemcode);
                                        if (_itemdetail == null)
                                        {
                                            DisplayMessage("Please check item code", 2);
                                            txtItem.Text = string.Empty;
                                            return;
                                        }
                                    }

                                }
                            }


                            if (_reqty < _Appqty)
                            {
                                DisplayMessage("You are not permitted to exceed the requested quantity..!", 2);
                                return;
                            }
                            else if (_reqty == _Appqty)
                            {
                                #region
                                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                                if ((_checkItemMRNlist != null))
                                {
                                    var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value) && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                                    if (_checkallreadyadd != null)
                                    {
                                        _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkallreadyadd.Itri_loc = txtPrefexlocation.Text;
                                        _checkallreadyadd.Itri_com = txtRequestcompany.Text;
                                        // _checkallreadyadd.Itri_note = txtItemRemark.Text;
                                        _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                                        _checkallreadyadd.Itri_base_req_no = lblrequestNo.Text;
                                        _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                        _MRNRequestItem.Remove(_checkItemMRNlist);

                                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                                        grdMRNReqItem.DataBind();
                                        grdApprovMRNitem.DataSource = _ApproveItem;
                                        grdApprovMRNitem.DataBind();
                                        LoadGridUnitCost(_ApproveItem);
                                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                        ViewState["_ApproveItem"] = _ApproveItem;
                                        txtItem.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        //txtItemRemark.Text = string.Empty;
                                        lblBufferLimit.Text = "0";
                                        lblbaseitem.Text = "N/A";
                                        lblitemdes.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        grdSeasson.DataSource = new int[] { };
                                        grdSeasson.DataBind();
                                        lblshopstock.Text = "0";
                                        lblForwardsale.Text = "0";
                                        txtRequestqty.Text = "0";
                                        txtApprovalqty.Text = "0";
                                        ddlPurchasetype.Items.Clear();
                                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                        txtPrefexlocation.Text = lblPreferloc.Text;
                                        Addpurchasetype(txtRequestcompany.Text);
                                        if (grdMRNReqItem.Rows.Count != 0)
                                        {
                                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                        }
                                        return;
                                    }
                                    else
                                    {


                                        _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text;
                                        _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                                        // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                                        _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkItemMRNlist.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                        _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                                        _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                                        _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                        _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;
                                        _checkItemMRNlist.Itri_itm_cd = _itemcode;
                                        if (BindItemComponent(txtItem.Text.ToUpper()))
                                        {
                                            foreach (MasterItemComponent _com in _masterItemComponent)
                                            {
                                                InventoryRequestItem _new = new InventoryRequestItem();
                                                _new.Itri_app_qty = _checkItemMRNlist.Itri_app_qty;
                                                _new.Itri_base_req_line = _checkItemMRNlist.Itri_base_req_line;
                                                _new.Itri_base_req_no = _checkItemMRNlist.Itri_base_req_no;
                                                _new.Itri_bqty = _checkItemMRNlist.Itri_bqty;
                                                _new.Itri_com = _checkItemMRNlist.Itri_com;
                                                _new.Itri_itm_stus = _checkItemMRNlist.Itri_itm_stus;
                                                _maxline = _maxline + 1;
                                                _new.Itri_line_no = _maxline;
                                                _new.Itri_loc = _checkItemMRNlist.Itri_loc;
                                                _new.Itri_mitm_cd = _checkItemMRNlist.Itri_mitm_cd;
                                                _new.Itri_mitm_stus = _checkItemMRNlist.Itri_mitm_stus;
                                                _new.Itri_qty = _checkItemMRNlist.Itri_qty;
                                                _new.PoType = _checkItemMRNlist.PoType;

                                                MasterItem _componentItem = new MasterItem();
                                                _componentItem.Mi_cd = _com.ComponentItem.Mi_cd;
                                                _new.MasterItem = _componentItem;


                                                _new.BL = "1";
                                                _new.Itri_itm_cd = _com.ComponentItem.Mi_cd;
                                                _ApproveItem.Add(_new);
                                            }

                                        }
                                        else
                                        {
                                            _ApproveItem.Add(_checkItemMRNlist);
                                        }
                                        _MRNRequestItem.Remove(_checkItemMRNlist);
                                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList(); ;
                                        grdMRNReqItem.DataBind();
                                        grdApprovMRNitem.DataSource = _ApproveItem;
                                        grdApprovMRNitem.DataBind();
                                        LoadGridUnitCost(_ApproveItem);
                                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                        ViewState["_ApproveItem"] = _ApproveItem;
                                        txtItem.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        // txtItemRemark.Text = string.Empty;
                                        lblBufferLimit.Text = "0";
                                        lblshopstock.Text = "0";
                                        lblForwardsale.Text = "0";
                                        txtRequestqty.Text = "0";
                                        txtApprovalqty.Text = "0";
                                        lblbaseitem.Text = "N/A";
                                        lblitemdes.Text = string.Empty;
                                        lblModel.Text = string.Empty;
                                        grdSeasson.DataSource = new int[] { };
                                        grdSeasson.DataBind();
                                        ddlPurchasetype.Items.Clear();
                                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                        txtPrefexlocation.Text = lblPreferloc.Text;
                                        Addpurchasetype(txtRequestcompany.Text);
                                        if (grdMRNReqItem.Rows.Count != 0)
                                        {
                                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                        }

                                        return;
                                    }

                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                                if ((_checkItemMRNlist != null))
                                {
                                    _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text;
                                    _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                                    // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                                    _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                    _checkItemMRNlist.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                    _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                                    _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                                    _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                                    _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                                    _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;
                                    _checkItemMRNlist.Itri_itm_cd = _itemcode;
                                    if (BindItemComponent(txtItem.Text.ToUpper()))
                                    {
                                        foreach (MasterItemComponent _com in _masterItemComponent)
                                        {
                                            InventoryRequestItem _new = new InventoryRequestItem();
                                            _new.Itri_app_qty = _checkItemMRNlist.Itri_app_qty;
                                            _new.Itri_base_req_line = _checkItemMRNlist.Itri_base_req_line;
                                            _new.Itri_base_req_no = _checkItemMRNlist.Itri_base_req_no;
                                            _new.Itri_bqty = _checkItemMRNlist.Itri_bqty;
                                            _new.Itri_com = _checkItemMRNlist.Itri_com;
                                            _new.Itri_itm_stus = _checkItemMRNlist.Itri_itm_stus;
                                            _new.Itri_line_no = _checkItemMRNlist.Itri_line_no;
                                            _new.Itri_loc = _checkItemMRNlist.Itri_loc;
                                            _new.Itri_mitm_cd = _checkItemMRNlist.Itri_mitm_cd;
                                            _new.Itri_mitm_stus = _checkItemMRNlist.Itri_mitm_stus;
                                            _new.Itri_qty = _checkItemMRNlist.Itri_qty;
                                            _new.PoType = _checkItemMRNlist.PoType;

                                            MasterItem _componentItem = new MasterItem();
                                            _componentItem.Mi_cd = _com.ComponentItem.Mi_cd;
                                            _new.MasterItem = _componentItem;


                                            _new.BL = "1";
                                            _new.Itri_itm_cd = _com.ComponentItem.Mi_cd;
                                            _ApproveItem.Add(_new);
                                        }

                                    }
                                    else
                                    {
                                        _ApproveItem.Add(_checkItemMRNlist);
                                    }


                                    decimal _qty = _reqty - _Appqty;
                                    foreach (var item in _MRNRequestItem.Where(w => w.Itri_mitm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                                    {
                                        item.Itri_com = txtRequestcompany.Text;
                                        item.Itri_loc = txtPrefexlocation.Text;
                                        item.Itri_bqty = item.Itri_bqty - Convert.ToDecimal(txtApprovalqty.Text);
                                        item.Approv_status = "Approved";
                                        item.Itri_qty = _qty;
                                    }
                                    var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                                    grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                                    grdMRNReqItem.DataBind();
                                    grdApprovMRNitem.DataSource = _ApproveItem;
                                    grdApprovMRNitem.DataBind();
                                    LoadGridUnitCost(_ApproveItem);
                                    ViewState["_MRNRequestItem"] = _MRNRequestItem;
                                    ViewState["_ApproveItem"] = _ApproveItem;
                                    foreach (GridViewRow row in grdMRNReqItem.Rows)
                                    {
                                        if (row.RowType == DataControlRowType.DataRow)
                                        {
                                            Label itemcode = row.FindControl("col_ITRI_ITM_CD") as Label;
                                            Label itemlineno = row.FindControl("col_itri_line_no") as Label;
                                            if ((itemcode.Text == HiddenItemcode.Value) && (itemlineno.Text == HiddenItemLine.Value))
                                            {
                                                row.BackColor = row.BackColor = System.Drawing.Color.Beige;
                                            }

                                        }
                                    }
                                    txtItem.Text = string.Empty;
                                    lblModel.Text = string.Empty;
                                    // lblItemRemark.Text = string.Empty;
                                    lblBufferLimit.Text = string.Empty;
                                    lblshopstock.Text = string.Empty;
                                    lblForwardsale.Text = string.Empty;
                                    txtRequestqty.Text = string.Empty;
                                    txtRequestcompany.Text = string.Empty;
                                    txtPrefexlocation.Text = string.Empty;
                                    txtApprovalqty.Text = string.Empty;
                                    lblbaseitem.Text = "N/A";
                                    lblitemdes.Text = string.Empty;
                                    lblModel.Text = string.Empty;
                                    grdSeasson.DataSource = new int[] { };
                                    grdSeasson.DataBind();
                                    ddlPurchasetype.Items.Clear();
                                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                                    txtPrefexlocation.Text = lblPreferloc.Text;
                                    Addpurchasetype(txtRequestcompany.Text);
                                    if (grdMRNReqItem.Rows.Count != 0)
                                    {
                                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                                    }
                                    return;
                                }
                                #endregion
                            }
                        }
                    }

                }
                else
                {
                    DisplayMessage("Please check item code", 2);
                    return;
                }
            }

            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnPrefeslocation3_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc_3";
                ViewState["SEARCH"] = _result;
                ApprovedPopup.Show();
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            // _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
            if (_MRNRequestItem != null)
            {
                // if (chkcancelall.Checked == true)
                // {
                foreach (InventoryRequestItem _PendingItem in _MRNRequestItem)
                {
                    _PendingItem.Approv_status = "Cancel";
                }
                grdMRNReqItem.DataSource = _MRNRequestItem;
                grdMRNReqItem.DataBind();

            }
        }


        protected void btnApproveall_Click(object sender, EventArgs e)
        {
            #region add all Item into approval List
            if (_ApproveItem != null)
            {
                _ApproveItem.Clear();
            }

            checkcompany(txtRequestcompany.Text);
            Session["RequestCompany"] = txtRequestcompany.Text;
            checkLocation(txtPrefexlocation.Text);
            _ApproveItem = _MRNRequestItem;
            foreach (InventoryRequestItem _ITM in _ApproveItem)
            {
                _ITM.Itri_app_qty = _ITM.Itri_bqty;
                _ITM.Itri_com = txtRequestcompany.Text;
                _ITM.Itri_loc = txtPrefexlocation.Text;
                _ITM.Backqty = _ITM.Itri_bqty;
                _ITM.PoType = ddlPurchasetype.SelectedItem.Text;

            }
            grdApprovMRNitem.DataSource = _ApproveItem;
            grdApprovMRNitem.DataBind();
            LoadGridUnitCost(_ApproveItem);
            //  _MRNRequestItem.Clear();
            grdMRNReqItem.DataSource = new int[] { };
            grdMRNReqItem.DataBind();
            _MRNRequestItem = null;
            _MRNRequestItem = new List<InventoryRequestItem>();
            txtPrefexlocation.Text = lblPreferloc.Text;
            Addpurchasetype(txtRequestcompany.Text);
            if (grdMRNReqItem.Rows.Count != 0)
            {
                grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
            }
            //  ViewState["_ApproveItem"] = _ApproveItem;
            return;
            #endregion
        }


        protected void btnok_Click(object sender, EventArgs e)
        {
            _contine = true;
            Process();
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            _contine = false;
            PopupConfBox.Hide();
        }

        private void Addpurchasetype2(string company)
        {
            ddlPurchasetype.Items.Clear();
            if (company == Session["UserCompanyCode"].ToString())
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Normal", "1"));
                ddlPurchasetypepopup.Items.AddRange(items.ToArray());
                return;
            }
            else
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("---select---", "0"));
                items.Add(new ListItem("Purchase", "1"));
                items.Add(new ListItem("Consignment", "2"));
                ddlPurchasetypepopup.Items.AddRange(items.ToArray());
                return;
            }
        }
        protected void chk_AReqItem_CheckedChanged_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdApprovMRNitem.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {

                        string ItemCode = (row.FindControl("col_AITRI_ITM_CD") as Label).Text;
                        string ItemModel = (row.FindControl("col_Ami_model") as Label).Text;
                        string ItemRemark = (row.FindControl("col_Aitri_note") as Label).Text;
                        string Req_qty = (row.FindControl("col_Aitri_bqty") as Label).Text;
                        string itemline = (row.FindControl("col_Aitri_line_no") as Label).Text;
                        string shopqty = (row.FindControl("col_Aitri_shop_qty") as Label).Text;
                        string fdqty = (row.FindControl("col_Aitri_fd_qty") as Label).Text;
                        string bufferqty = (row.FindControl("col_Aitri_buffer") as Label).Text;
                        string com = (row.FindControl("col_Aitri_com") as Label).Text;
                        string loc = (row.FindControl("col_Aitri_loc") as Label).Text;
                        string Appqty = (row.FindControl("col_Aitri_app_qty") as Label).Text;
                        string backqty = (row.FindControl("col_Aitri_bqty") as Label).Text;
                        // HiddenPOType.Value = (row.FindControl("col_PoType") as Label).Text;
                        txtpopupItem.Text = ItemCode;
                        //txtpopupModel.Text = ItemModel;
                        // txtpopupItemRemark.Text = ItemRemark;
                        if (backqty == "0")
                        {
                            backqty = Req_qty;
                        }
                        txtpopupRequestqty.Text = backqty;
                        txtpopupApprovalqty.Text = Req_qty;
                        lblpopupshopstock.Text = shopqty;
                        lblpopupForwardsale.Text = fdqty;
                        lblpopupBufferLimit.Text = bufferqty;
                        txtpopupIcompany.Text = com;
                        txtpopupApprovalqty.Text = Appqty;
                        Addpurchasetype2(txtpopupIcompany.Text);
                        txtPrefexlocationpopup.Text = loc;
                        // ddlPurchasetypepopup.SelectedItem.Text = HiddenPOType.Value;


                        HiddenItemLine.Value = itemline;
                        HiddenItemcode.Value = ItemCode;


                        row.BackColor = System.Drawing.Color.LightCyan;
                        // GetInventory(ItemCode, Session["UserCompanyCode"].ToString(), string.Empty);
                        Session["Itemcode"] = ItemCode;
                        //txtItemRemark.Focus();
                    }
                    else
                    {
                        ///Session["Itemcode"] = "";
                        txtpopupItem.Text = string.Empty;
                        //txtpopupModel.Text = string.Empty;
                        //txtpopupItemRemark.Text = string.Empty;
                        lblpopupBufferLimit.Text = string.Empty;
                        lblpopupshopstock.Text = string.Empty;
                        lblpopupForwardsale.Text = string.Empty;
                        txtpopupRequestqty.Text = string.Empty;
                        txtpopupIcompany.Text = string.Empty;
                        txtPrefexlocationpopup.Text = string.Empty;
                        txtpopupApprovalqty.Text = string.Empty;
                        ddlPurchasetypepopup.Items.Clear();
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }
                ApprovedPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnappAddItem_Click(object sender, EventArgs e)
        {
            #region add validation for allocation data chk 02 Nov 2016
            MasterLocationNew _mstLocNew = CHNLSVC.General.GetMasterLocations(new MasterLocationNew() { Ml_loc_cd = txtShowroom.Text.Trim().ToUpper(), Ml_com_cd = Session["UserCompanyCode"].ToString() }).FirstOrDefault();
            MasterLocationPriorityHierarchy _locHir = CHNLSVC.General.GET_MST_LOC_INFO_DATA(txtShowroom.Text.Trim().ToUpper(), "CHNL");
            string _chnl = "";
            if (_locHir != null)
            {
                _chnl = _locHir.Mli_val;
            }
            if (_mstLocNew != null)
            {

                if (_mstLocNew.Ml_loc_tp != "WH")
                {
                    decimal _allAllocationQty = 0;
                    decimal _chnlAllocationQty = 0;
                    decimal _allInvBal = 0;
                    decimal _reqAppQty = 0;
                    decimal _tmpDecimal = 0;
                    List<InventoryAllocateDetails> _allAllocation = new List<InventoryAllocateDetails>();
                    List<InventoryAllocateDetails> _chnlAllocation = new List<InventoryAllocateDetails>();
                    InventoryLocation _inrLocBal = new InventoryLocation();
                    _reqAppQty = decimal.TryParse(txtpopupApprovalqty.Text, out _tmpDecimal) ? Convert.ToDecimal(txtpopupApprovalqty.Text) : 0;
                    _allAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                    {
                        Isa_com = Session["UserCompanyCode"].ToString(),
                        Isa_itm_cd = txtpopupItem.Text.ToUpper().Trim(),
                        //Isa_itm_stus = item.Itri_itm_stus
                    });
                    if (_allAllocation.Count > 0)
                    {
                        _allAllocationQty = _allAllocation.Sum(c => c.Isa_aloc_bqty);
                    }
                    if (_allAllocationQty > 0)
                    {
                        _chnlAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                        {
                            Isa_chnl = _chnl,
                            Isa_com = Session["UserCompanyCode"].ToString(),
                            Isa_itm_cd = txtpopupItem.Text.ToUpper().Trim(),
                            // Isa_itm_stus = item.Itri_itm_stus
                        });
                        if (_chnlAllocation.Count > 0)
                        {
                            _chnlAllocationQty = _chnlAllocation.Sum(c => c.Isa_aloc_bqty);
                        }

                        _allInvBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE_BY_COM(new InventoryLocation()
                        {
                            Inl_com = Session["UserCompanyCode"].ToString(),
                            Inl_loc = txtShowroom.Text.Trim().ToUpper(),
                            Inl_itm_cd = txtpopupItem.Text.ToUpper().Trim()
                            //Inl_itm_stus = item.Itri_itm_stus
                        });
                        //if (_inrLocBal != null)
                        //{
                        //    _allInvBal = _inrLocBal.Inl_free_qty;
                        //}
                        List<InventoryRequestItem> _reqItemDetails = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                        if (_reqItemDetails != null)
                        {
                            if (_reqItemDetails.Count > 0)
                            {
                                //_reqAppQty =  _reqAppQty + _reqItemDetails.Sum(c=> c.Itri_qty);
                            }
                        }
                        if (_reqAppQty > _chnlAllocationQty)
                        {
                            decimal _availableBalanc = _chnlAllocationQty + (_allInvBal - _allAllocationQty);
                            if (_reqAppQty > _availableBalanc)
                            {
                                if (_availableBalanc > -1)
                                {
                                    DispMsg("You cannot exceed the allocation qty. Available Balance : " + (_allInvBal - _allAllocationQty + _chnlAllocationQty)); ApprovedPopup.Show(); return;
                                }
                                else
                                {
                                    DispMsg("You cannot exceed the allocation qty. Available Balance : " + _chnlAllocationQty); ApprovedPopup.Show(); return;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            #endregion

            bool Isalreaysave = Convert.ToBoolean(Session["Isalreadysave"].ToString());
            if (Isalreaysave == true)
            {
                #region save
                //  List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                //  _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                if (string.IsNullOrEmpty(txtpopupIcompany.Text))
                {
                    DisplayMessage("Please enter request company..!", 2);
                    ApprovedPopup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtPrefexlocationpopup.Text))
                {
                    DisplayMessage("Please enter prefer location..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (ddlPurchasetypepopup.SelectedValue == "0")
                {
                    DisplayMessage("Please select purchase type..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtpopupItem.Text))
                {
                    DisplayMessage("Please enter item..!", 2);
                    ApprovedPopup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtpopupApprovalqty.Text))
                {
                    DisplayMessage("Please enter approvale quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (txtpopupApprovalqty.Text == ".")
                {
                    DisplayMessage("Please enter correct quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                checkcompany(txtpopupIcompany.Text);
                Session["RequestCompany"] = txtpopupIcompany.Text;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "code", txtPrefexlocationpopup.Text);
                if ((_result == null) || (_result.Rows.Count == 0))
                {
                    DisplayMessage("Please enter correct Location..!", 2);
                    txtPrefexlocation.Text = string.Empty;
                    txtPrefexlocation.Focus();
                    ApprovedPopup.Show();
                    return;
                }
                decimal _reqty = Convert.ToDecimal(txtpopupRequestqty.Text);

                decimal _Appqty = Convert.ToDecimal(txtpopupApprovalqty.Text);


                if (_reqty < _Appqty)
                {
                    DisplayMessage("You are not permitted to exceed the requested quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }



                else if (_Appqty == 0)
                {
                    DisplayMessage("Zero quantity cannot be approved", 2);
                    txtpopupApprovalqty.Text = string.Empty;
                    ApprovedPopup.Show();
                    return;
                }
                foreach (var item in _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                {
                    MasterItem _componentItem = new MasterItem();
                    _componentItem.Mi_cd = item.Itri_itm_cd;
                    item.MasterItem = _componentItem;
                    item.Itri_com = txtpopupIcompany.Text;
                    item.Itri_loc = txtPrefexlocationpopup.Text;
                    item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                    item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                    item.Itri_bqty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                }
                grdApprovMRNitem.DataSource = _ApproveItem;
                grdApprovMRNitem.DataBind();
                LoadGridUnitCost(_ApproveItem);
                // ViewState["_ApproveItem"] = _ApproveItem;

                //txtpopupItemRemark.Text = string.Empty;
                txtpopupItem.Text = string.Empty;
                //txtpopupModel.Text = string.Empty;
                // txtpopupItemRemark.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupApprovalqty.Text = string.Empty;
                lblpopupshopstock.Text = string.Empty;
                lblpopupForwardsale.Text = string.Empty;
                lblpopupBufferLimit.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                Toatalapprovalqty();
                ApprovedPopup.Show();
                #endregion
            }
            else
            {
                //  List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                //_ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                if (string.IsNullOrEmpty(txtpopupIcompany.Text))
                {
                    DisplayMessage("Please enter request company..!", 2);
                    ApprovedPopup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtPrefexlocationpopup.Text))
                {
                    DisplayMessage("Please enter prefer location..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (ddlPurchasetypepopup.SelectedValue == "0")
                {
                    DisplayMessage("Please select purchase type..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtpopupItem.Text))
                {
                    DisplayMessage("Please enter item..!", 2);
                    ApprovedPopup.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtpopupApprovalqty.Text))
                {
                    DisplayMessage("Please enter approvale quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                if (txtpopupApprovalqty.Text == ".")
                {
                    DisplayMessage("Please enter correct quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }
                checkcompany(txtpopupIcompany.Text);
                Session["RequestCompany"] = txtpopupIcompany.Text;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "code", txtPrefexlocationpopup.Text);
                if ((_result == null) || (_result.Rows.Count == 0))
                {
                    DisplayMessage("Please enter correct Location..!", 2);
                    txtPrefexlocation.Text = string.Empty;
                    txtPrefexlocation.Focus();
                    ApprovedPopup.Show();
                    return;
                }

                decimal _reqty = Convert.ToDecimal(txtpopupRequestqty.Text);

                decimal _Appqty = Convert.ToDecimal(txtpopupApprovalqty.Text);


                if (_reqty < _Appqty && chkcomahhoc.Checked != true)
                {
                    DisplayMessage("You are not permitted to exceed the requested quantity..!", 2);
                    ApprovedPopup.Show();
                    return;
                }



                else if (_Appqty == 0)
                {
                    DisplayMessage("Zero quantity cannot be approved", 2);
                    txtpopupApprovalqty.Text = string.Empty;
                    ApprovedPopup.Show();
                    return;
                }
                else if (_reqty == _Appqty)
                {
                    foreach (var item in _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                    {
                        item.Itri_com = txtpopupIcompany.Text;
                        item.Itri_loc = txtPrefexlocationpopup.Text;
                        item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                        item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_bqty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                    }
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    LoadGridUnitCost(_ApproveItem);
                    grdApprovMRNitem.DataBind();
                    _MRNRequestItem.RemoveAll(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                    grdMRNReqItem.DataSource = _MRNRequestItem;
                    grdMRNReqItem.DataBind();
                    // ViewState["_ApproveItem"] = _ApproveItem;
                }
                else
                {
                    foreach (var item in _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                    {
                        item.Itri_com = txtpopupIcompany.Text;
                        item.Itri_loc = txtPrefexlocationpopup.Text;
                        item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                        decimal _remaingValue = item.Itri_app_qty - Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_bqty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                    }
                    List<InventoryRequestItem> _Backtem = new List<InventoryRequestItem>();
                    _Backtem = _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)).ToList();
                    // _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    if (_MRNRequestItem.Count > 0)
                    {
                        decimal _bqty = _Backtem[0].Backqty - _Backtem[0].Itri_app_qty;
                        var _req = _MRNRequestItem.Find(y => y.Itri_itm_cd == HiddenItemcode.Value && y.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                        if (_req != null)
                        {
                            _req.Itri_bqty = _bqty;
                        }
                        else
                        {
                            _Backtem.ForEach(w => w.Itri_bqty = _bqty);
                            _MRNRequestItem.AddRange(_Backtem);
                        }


                    }
                    else
                    {
                        _MRNRequestItem = _Backtem;
                        decimal _bqty = _Backtem[0].Backqty - _Backtem[0].Itri_app_qty;
                        var _req = _MRNRequestItem.Find(y => y.Itri_itm_cd == HiddenItemcode.Value && y.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                        if (_req != null)
                        {
                            _req.Itri_bqty = _bqty;
                        }
                        else
                        {
                            _Backtem.ForEach(w => w.Itri_bqty = _bqty);
                            _MRNRequestItem.AddRange(_Backtem);
                        }
                    }
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
                    LoadGridUnitCost(_ApproveItem);
                    grdMRNReqItem.DataSource = _MRNRequestItem;
                    grdMRNReqItem.DataBind();
                    // ViewState["_ApproveItem"] = _ApproveItem;
                    //ViewState["_MRNRequestItem"] = _MRNRequestItem;

                }
                txtpopupItem.Text = string.Empty;
                //txtpopupModel.Text = string.Empty;
                // txtpopupItemRemark.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupApprovalqty.Text = string.Empty;
                lblpopupshopstock.Text = string.Empty;
                lblpopupForwardsale.Text = string.Empty;
                lblpopupBufferLimit.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                Toatalapprovalqty();
                ApprovedPopup.Show();
            }

        }

        protected void lbtnviewrout_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtItem.Text == "N/A") || (txtItem.Text == ""))
                {
                    DisplayMessage("Please select item", 2);
                    return;
                }
                List<InventoryLocation> _Invetory = new List<InventoryLocation>();
                _Invetory = CHNLSVC.Inventory.Get_Root_Loc_Inv(Session["UserCompanyCode"].ToString(), droproutstock.Text, txtItem.Text.ToUpper().Trim(), null);
                if (_Invetory != null)
                {
                    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        foreach (InventoryLocation _loc in _Invetory)
                        {
                            _loc.Inl_cre_by = droproutstock.Text.ToString();
                            _loc.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _loc.Inl_itm_stus).Mis_desc;
                        }

                    }
                    grdRoutstock.DataSource = _Invetory;
                    grdRoutstock.DataBind();
                    lblAppqty.Text = txtApprovalqty.Text;
                    MdlpopRootinv.Show();
                }
                else
                {
                    string _Msg = "No route stock found";
                    DisplayMessage(_Msg, 4);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtncomselect_Click(object sender, EventArgs e)
        {
            if (grdRoutstock.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string loc = (row.FindControl("col_INL_LOC") as Label).Text;
                string status = (row.FindControl("col_INL_ITM_STUS") as Label).Text;
                lblserloc.Text = loc;
                // List<InventorySerialN> _serial = new List<InventorySerialN>();
                InventorySerialN _serailobj = new InventorySerialN();
                //Ins_ser_1 = txtTus_ser_1.Text,
                //        Ins_itm_cd = lblTus_itm_cd.Text,
                //        Ins_loc=Session["UserDefLoca"].ToString(),
                //        Ins_com = Session["UserCompanyCode"].ToString(),
                //        Ins_available = 0,
                //        Ser_tp=1

                _serailobj.Ins_com = Session["UserCompanyCode"].ToString();
                _serailobj.Ins_loc = loc;
                _serailobj.Ins_itm_cd = txtItem.Text;
                _serailobj.Ins_itm_stus = status;
                _serailobj.Ins_available = 1;
                // _serailobj.Ser_tp = 1;
                _serial = CHNLSVC.Inventory.Get_INR_SER_DATA(_serailobj);
                grdRoutserial.DataSource = _serial;
                grdRoutserial.DataBind();
                Loadcodere();
                MdlpopRootinv.Show();
            }

        }

        private void Loadcodere()
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable _tbl = CHNLSVC.CommonSearch.SER_REF_COND_TP(para, null, null);
            foreach (GridViewRow row in grdRoutserial.Rows)
            {
                DropDownList colR_appstatus = (DropDownList)row.FindControl("col_code");
                colR_appstatus.DataSource = _tbl;
                colR_appstatus.DataTextField = "Description";
                colR_appstatus.DataValueField = "Code";
                colR_appstatus.DataBind();
                colR_appstatus.Items.Insert(0, "--Select--");
            }
        }

        protected void lbtnselectSeri_Click(object sender, EventArgs e)
        {
            if (grdRoutserial.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                if (_serial.Count > 0)
                {
                    string ID = (row.FindControl("col_ins_ser_id") as Label).Text;

                    var _filter = _serial.Find(x => x.Ins_ser_id == Convert.ToInt32(ID));
                    if (_filter.Ser_tp == 1)
                    {
                        _serial.Where(x => x.Ins_ser_id == Convert.ToInt32(ID)).ToList().ForEach(x => x.Ser_tp = 0);
                        var roww = (GridViewRow)grdRoutserial.SelectedRow;
                        row.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        _serial.Where(x => x.Ins_ser_id == Convert.ToInt32(ID)).ToList().ForEach(x => x.Ser_tp = 1);
                        var roww = (GridViewRow)grdRoutserial.SelectedRow;
                        row.BackColor = System.Drawing.Color.LightGreen;

                    }

                    int count = _serial.Count(i => i.Ser_tp == 1);
                    lblAelectQty.Text = count.ToString();


                    MdlpopRootinv.Show();
                }
            }
        }

        protected void btncont_Click(object sender, EventArgs e)
        {
            // txtPrefexlocation.Text = lblserloc.Text;

            #region validation
            decimal _selecqty = Convert.ToDecimal(lblAelectQty.Text);
            if (_selecqty > 0)
            {
                if (Convert.ToDecimal(lblAppqty.Text) < Convert.ToDecimal(lblAelectQty.Text))
                {
                    DisplayMessage("cannot exceed approve qty", 2);
                    return;
                }


                if (_ApproveItem == null)
                {
                    _ApproveItem = new List<InventoryRequestItem>();
                }
                if (_MRNRequestItem == null)
                {
                    DisplayMessage("Request Item empty..!", 2);
                    return;
                }
                if (string.IsNullOrEmpty(lblShowroom.Text))
                {
                    DisplayMessage("Please enter showroom..!", 2);
                    return;
                }
                if (_MRNRequestItem.Count == 0)
                {
                    DisplayMessage("No item found..!", 2);
                    return;
                }
                #region
                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                if ((_checkItemMRNlist != null))
                {
                    if (_ApproveItem.Count > 0)
                    {
                        var _founfApproveItem = _ApproveItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)
                            && _checkItemMRNlist.Itri_loc == lblserloc.Text);

                        if (_founfApproveItem != null)
                        {
                            _founfApproveItem.Itri_app_qty = Convert.ToDecimal(lblAelectQty.Text);

                        }
                        else
                        {
                            _checkItemMRNlist.Itri_loc = lblserloc.Text;
                            _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                            // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                            _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(lblAelectQty.Text);
                            _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                            _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                            _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                            _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                            _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;

                            _ApproveItem.Add(_checkItemMRNlist);
                        }


                    }
                    else
                    {
                        _checkItemMRNlist.Itri_loc = lblserloc.Text;
                        _checkItemMRNlist.Itri_com = txtRequestcompany.Text;
                        // _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                        _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(lblAelectQty.Text);
                        _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                        _checkItemMRNlist.Itri_base_req_no = lblrequestNo.Text;
                        _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                        _checkItemMRNlist.Itri_mitm_cd = lblbaseitem.Text;

                        _ApproveItem.Add(_checkItemMRNlist);
                    }


                    decimal _qty = _checkItemMRNlist.Backqty - Convert.ToDecimal(lblAelectQty.Text);//Convert.ToDecimal(lblAppqty.Text) - Convert.ToDecimal(lblAelectQty.Text);
                    foreach (var item in _MRNRequestItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                    {
                        item.Itri_com = txtRequestcompany.Text;
                        item.Itri_loc = lblserloc.Text;
                        item.Itri_bqty = _qty;// item.Itri_bqty - Convert.ToDecimal(lblAelectQty.Text);
                        item.Approv_status = "Approved";
                        item.Itri_qty = _qty;
                    }
                    var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                    grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                    grdMRNReqItem.DataBind();
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
                    LoadGridUnitCost(_ApproveItem);
                    ViewState["_MRNRequestItem"] = _MRNRequestItem;
                    ViewState["_ApproveItem"] = _ApproveItem;
                    foreach (GridViewRow row in grdMRNReqItem.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label itemcode = row.FindControl("col_ITRI_ITM_CD") as Label;
                            Label itemlineno = row.FindControl("col_itri_line_no") as Label;
                            if ((itemcode.Text == HiddenItemcode.Value) && (itemlineno.Text == HiddenItemLine.Value))
                            {
                                row.BackColor = row.BackColor = System.Drawing.Color.Beige;
                            }

                        }
                    }
                    txtItem.Text = string.Empty;
                    lblModel.Text = string.Empty;
                    // lblItemRemark.Text = string.Empty;
                    lblBufferLimit.Text = string.Empty;
                    lblshopstock.Text = string.Empty;
                    lblForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                    txtPrefexlocation.Text = lblPreferloc.Text;
                    Addpurchasetype(txtRequestcompany.Text);
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    collectserial();
                    return;
                }
                #endregion

            }
            else
            {
                collectserial();
            }
            #endregion

        }

        private void collectserial()
        {

            foreach (GridViewRow ddritem in grdRoutserial.Rows)
            {
                InventorySerialN _serialobj = new InventorySerialN();
                //string select = (ddritem.FindControl("col_Ser_tp") as Label).Text;
                string code = (ddritem.FindControl("col_code") as DropDownList).SelectedValue;
                string serid = (ddritem.FindControl("col_ins_ser_id") as Label).Text;
                string _serialno = (ddritem.FindControl("col_ins_ser_1") as Label).Text;
                string _serial2 = (ddritem.FindControl("col_ins_ser_2") as Label).Text;
                string _serial3 = (ddritem.FindControl("col_ins_ser_3") as Label).Text;
                string _itm = (ddritem.FindControl("col_ins_itm_cd") as Label).Text;
                string _status = (ddritem.FindControl("col_ins_itm_stus") as Label).Text;
                string _remark = (ddritem.FindControl("col_remark") as TextBox).Text;
                string _exdate = (ddritem.FindControl("txtserexdate") as TextBox).Text;
                int select = _serial.Find(x => x.Ins_ser_id == Convert.ToInt32(serid)).Ser_tp;
                if (select == 1)
                {

                    _serialobj.Ins_com = Session["UserCompanyCode"].ToString();
                    _serialobj.Ins_loc = lblserloc.Text;
                    _serialobj.Ins_ser_1 = _serialno;
                    _serialobj.Ins_ser_2 = _serial2;
                    _serialobj.Ins_ser_3 = _serial3;
                    _serialobj.Ins_ser_id = Convert.ToInt32(serid);
                    _serialobj.Ins_itm_cd = _itm;
                    _serialobj.Ins_itm_stus = _status;
                    _serialobj.TmpIsDamgnot = 2;
                    _selectserial.Add(_serialobj);
                }
                else
                {
                    if (code != "--Select--")
                    {
                        RepConditionType _tpeobj = new RepConditionType();
                        _tpeobj.Rct_com = Session["UserCompanyCode"].ToString();
                        _tpeobj.Rct_cate = "CT004";
                        _tpeobj.Rct_tp = code;
                        List<RepConditionType> _tbl = new List<RepConditionType>();
                        _tbl = CHNLSVC.Inventory.GET_REF_COND_TP(_tpeobj);
                        if (_tbl.Count == 1)
                        {

                            _serialobj.Ins_com = Session["UserCompanyCode"].ToString();
                            _serialobj.Ins_loc = lblserloc.Text;
                            _serialobj.Ins_ser_1 = _serialno;
                            _serialobj.Ins_ser_2 = _serial2;
                            _serialobj.Ins_ser_3 = _serial3;
                            _serialobj.Ins_ser_id = Convert.ToInt32(serid);
                            _serialobj.TmpIsDamgnot = _tbl[0].Rct_ini; //1-DIN doc,0 no doc,2-INTR doc
                            if (code != "--Select--")
                            {
                                _serialobj.Ins_res_code = code;
                                _serialobj.Ins_res_remark = _remark;
                            }
                            _serialobj.Ins_itm_cd = _itm;
                            _serialobj.Ins_itm_stus = _status;
                            if (_exdate != "")
                            {
                                _serialobj.Ins_res_exdate = Convert.ToDateTime(_exdate).Date;
                            }
                            _selectserial.Add(_serialobj);
                        }

                    }

                }


            }

        }

        protected void txtShowroom_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtShowroom.Text);
            if (_result.Rows.Count == 0)
            {
                string Msg = "Invalid showrom";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                txtShowroom.Text = "";
                LoadLocationData();
                txtShowroom.Focus();
            }
            else
            {
                LoadLocationData();
                lblshowroomname.Text = _result.Rows[0][1].ToString();
                lblShowroom.Text = _result.Rows[0][0].ToString();

                if (chkcomahhoc.Checked)
                {
                    DateTime now = DateTime.Now;
                    var startDate = new DateTime(now.Year, now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);
                    DataTable hsdt = CHNLSVC.Sales.GET_HSLIMIT_DATA(txtShowroom.Text.ToString(), startDate, endDate);

                    if (hsdt.Rows.Count > 0)
                    {
                        decimal numaccount = Convert.ToDecimal(hsdt.Rows[0]["hrs_no_ac"].ToString());
                        decimal accvalue = Convert.ToDecimal(hsdt.Rows[0]["hrs_tot_val"].ToString());
                        lbmaxhpaccounts.Text = numaccount.ToString();
                        lbmaxval.Text = accvalue.ToString();
                    }
                    else
                    {
                        lbmaxhpaccounts.Text = "";
                        lbmaxval.Text = "";
                    }
                    DataTable hsinvdata = CHNLSVC.Sales.GET_HSINVDATA(txtShowroom.Text.ToString(), startDate, endDate);
                    int i = 0;
                    decimal total = 0;
                    int accountcount = hsinvdata.Rows.Count;
                    lbcurrenthpacc.Text = accountcount.ToString();
                    if (hsinvdata.Rows.Count > 0)
                    {
                        foreach (var invdata in hsinvdata.Rows)
                        {
                            total = total + Convert.ToDecimal(hsinvdata.Rows[i]["totalval"].ToString());
                            i++;
                        }
                    }
                    lbcurrentval.Text = total.ToString();




                }

            }
        }

        protected void lbtnShowroom_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "showroom2";
            ViewState["SEARCH"] = _result;
            UserPopup.Show();
        }

        protected void lbtnApprovalMRN_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = Convert.ToDateTime(lblSearchDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(lblSearchDate.Text).Date.ToShortDateString();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                if (_result.Rows.Count > 150)
                {
                    for (int x = 0; x < _result.Rows.Count; x++)
                    {
                        if (x > 250)
                        {
                            DataRow dr = _result.Rows[x];
                            dr.Delete();
                        }
                    }
                }
                _result.AcceptChanges();
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "MRN_App";
                ViewState["SEARCH"] = _result;
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }


        private bool checkSimilrItem(string _simiItm)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
            DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, "CODE", _simiItm);
            if (result1.Rows.Count == 0)
            {
                return false;
            }
            return true;

        }

        private bool checkReplaceItem(string _replItm)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
            DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, "CODE", _replItm);
            if (result1.Rows.Count == 0)
            {
                return false;
            }
            return true;

        }

        protected void lbtnPrefeslocation_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc_2";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "SCM1_MRNAPP";
                Session["GlbReportName"] = "MRNPrint.rpt";
                Session["documntNo"] = txtApproval.Text;
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                PopupConfBox.Hide();
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Distribution approval Print", "Distribution approval", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void lbtncancelitem_Click(object sender, EventArgs e)
        {

            if (grdMRNReqItem.Rows.Count == 0) return;
            var linkbutton = (LinkButton)sender;
            var row = (GridViewRow)linkbutton.NamingContainer;
            if (row != null)
            {
                string Status = (row.FindControl("col_Approv_status") as Label).Text;
                string lineno = (row.FindControl("col_itri_line_no") as Label).Text;
                if (Status == "Cancel")
                {
                    // _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    if (_MRNRequestItem != null)
                    {
                        foreach (var item in _MRNRequestItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno)))
                        {
                            item.Approv_status = "";
                        }
                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();

                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                        grdMRNReqItem.DataBind();
                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                    }
                    return;
                }
                else if (Status == "Approved")
                {
                    return;
                }
                else
                {
                    //  _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    if (_MRNRequestItem != null)
                    {

                        foreach (var item in _MRNRequestItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno)))
                        {
                            item.Approv_status = "Cancel";
                        }

                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();

                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList(); ;
                        grdMRNReqItem.DataBind();
                        //ViewState["_MRNRequestItem"] = _MRNRequestItem;
                    }
                }
            }
        }


        public void PrintPDF(string targetFileName)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //obj.MRNPrint(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                //ReportDocument Rel = new ReportDocument();
                //ReportDocument rptDoc = (ReportDocument)obj._mrn;
                //DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                //rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //diskOpts.DiskFileName = targetFileName;
                //rptDoc.ExportOptions.DestinationOptions = diskOpts;
                //rptDoc.Export();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void chkcomahhoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcomahhoc.Checked == true)
            {
                lblexcel.Text = "Stock Push";
                lblrequestNo.Text = "AD-HOC";
                // pnlComadhoc.Visible = true;
                // pnldefalt.Visible = false;
            }
            else
            {
                lblexcel.Text = "S/R Requesting";
                lblrequestNo.Text = " ";
                // pnldefalt.Visible = true;
                // pnlComadhoc.Visible = false;
            }

        }


        protected void lbtngrdItemsDalete_Click1(object sender, EventArgs e)
        {
            if (grdApprovMRNitem.Rows.Count > 0)
            {
                var checkbox = (LinkButton)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {


                    Label lineno = (Label)row.FindControl("col_Aitri_line_no");
                    Label item = (Label)row.FindControl("col_AITRI_ITM_CD");


                    List<InventoryRequestItem> _MRNRequestItem_new = new List<InventoryRequestItem>();
                    //_ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                    //_MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    _MRNRequestItem_new = _ApproveItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno.Text) && x.Itri_itm_cd == item.Text).ToList();
                    foreach (InventoryRequestItem _fl in _MRNRequestItem_new)
                    {

                    }
                    if (_MRNRequestItem != null)
                    {


                        _ApproveItem.RemoveAll(x => x.Itri_line_no == Convert.ToInt32(lineno.Text) && x.Itri_itm_cd == item.Text);
                        var _checkitem = _MRNRequestItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno.Text) && x.Itri_itm_cd == item.Text).ToList();
                        if (_checkitem.Count > 0)
                        {
                            if (_MRNRequestItem.Count > 0)
                            {
                                foreach (InventoryRequestItem _itm in _MRNRequestItem)
                                {
                                    if (Convert.ToInt32(lineno.Text) == _itm.Itri_line_no)
                                    {
                                        _itm.Itri_app_qty = 0;
                                        _itm.Itri_com = "N/A";
                                        _itm.Itri_loc = "N/A";
                                        _itm.Approv_status = "";
                                        _itm.Itri_bqty = _itm.Backqty;
                                    }

                                }
                            }

                            else
                            {
                                foreach (InventoryRequestItem _itm in _MRNRequestItem_new)
                                {

                                    _itm.Itri_app_qty = 0;
                                    _itm.Itri_com = "N/A";
                                    _itm.Itri_loc = "N/A";
                                    _itm.Approv_status = "";
                                    _itm.Itri_bqty = _itm.Backqty;


                                }
                                _MRNRequestItem.AddRange(_MRNRequestItem_new.Where(x => x.Itri_note != "New Item").ToList());

                            }
                        }
                        else
                        {
                            foreach (InventoryRequestItem _itm in _MRNRequestItem_new)
                            {

                                _itm.Itri_app_qty = 0;
                                _itm.Itri_com = "N/A";
                                _itm.Itri_loc = "N/A";
                                _itm.Approv_status = "";
                                _itm.Itri_bqty = _itm.Backqty;
                            }
                            _MRNRequestItem.AddRange(_MRNRequestItem_new.Where(x => x.Itri_note != "New Item").ToList());

                        }

                        grdMRNReqItem.DataSource = _MRNRequestItem;
                        grdMRNReqItem.DataBind();



                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();
                        LoadGridUnitCost(_ApproveItem);

                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                        ViewState["_ApproveItem"] = _ApproveItem;
                        ApprovedPopup.Show();
                    }
                    else
                    {
                        List<InventoryRequestItem> _deletItem = new List<InventoryRequestItem>();
                        _deletItem = _ApproveItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno.Text)).ToList();
                        grdMRNReqItem.DataSource = _deletItem;
                        grdMRNReqItem.DataBind();
                        ViewState["_MRNRequestItem"] = _deletItem;

                        _ApproveItem.RemoveAll(x => x.Itri_line_no == Convert.ToInt32(lineno.Text));

                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();
                        LoadGridUnitCost(_ApproveItem);
                        ViewState["_ApproveItem"] = _ApproveItem;
                        ApprovedPopup.Show();
                    }
                }
                else
                {

                }



            }
        }

        protected void ddlitemserchoption_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlitemserchoption.SelectedValue == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, null, null);

                    if (result1.Rows.Count > 0)
                    {
                        result1 = result1.AsEnumerable()
          .GroupBy(r => new { Col1 = r["Item Code"] })
          .Select(g => g.OrderBy(r => r["Item Code"]).First())
          .CopyToDataTable();
                    }
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Similar_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, null, null);
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Replace_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "0")
                {
                    DataTable result1 = CHNLSVC.Inventory.GetComItem(txtItem.Text.ToUpper());
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "Com_Item";
                    MdlItemSear.Show();
                    return;
                }
                if (ddlitemserchoption.SelectedValue == "3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result1 = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                    for (int x = result1.Rows.Count - 1; x >= 0; x--)
                    {
                        if (x > 10)
                        {
                            DataRow dr = result1.Rows[x]; dr.Delete();
                        }
                    }
                    result1.AcceptChanges();
                    grdItem.DataSource = result1;
                    grdItem.DataBind();
                    lblvalue.Text = "New_Item";
                    MdlItemSear.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnnewitmsearch_Click(object sender, EventArgs e)
        {
            if (txtsearcItembyword.Text.ToString() == "")
            {

            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, "ITEM", txtsearcItembyword.Text);
                grdItem.DataSource = result;

                for (int x = result.Rows.Count - 1; x >= 0; x--)
                {
                    if (x > 10)
                    {
                        DataRow dr = result.Rows[x]; dr.Delete();
                    }
                }

                grdItem.DataBind();
                lblvalue.Text = "New_Item";
                MdlItemSear.Show();
                return;
            }


        }

        protected void lbtngrdPickqty_Click(object sender, EventArgs e)
        {
            var linbtn = (LinkButton)sender;
            var row = (GridViewRow)linbtn.NamingContainer;
            var lb = (LinkButton)sender;
            if (row != null)
            {
                string loc = (row.FindControl("col_Aitri_loc") as Label).Text;
                string itmcode = (row.FindControl("col_AITRI_ITM_CD") as Label).Text;
                List<InventorySerialN> Pickitem = CHNLSVC.Inventory.Get_Reserved_SerialsNew(Session["UserCompanyCode"].ToString(), loc.ToString());
                Pickitem = Pickitem.Where(a => a.Ins_itm_cd == itmcode).ToList();
                string pickqty = Pickitem.Count().ToString();
                lbpickqty.Text = pickqty;
                ApprovedPopup.Show();
            }



        }

        protected void grdMRNReqItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        protected void btnCls_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }

        protected void txtSerByKey_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSeNew_Click(object sender, EventArgs e)
        {

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
                if (_serType == "SimilarItem")
                {
                    txtItem.Text = code;
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp)
        {
            OrderSearchGridData(_colName, _ordTp);
            try
            {
                dgvResult.DataSource = new int[] { };
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        BindDdlSerByKey(_serData);
                    }
                }
                dgvResult.DataBind();
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
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }

        protected void lbtnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var row = (GridViewRow)btn.NamingContainer;
                Label col_itr_seq_no = row.FindControl("col_itr_seq_no") as Label;
                Label col_itr_req_no = row.FindControl("col_itr_req_no") as Label;
                LinkButton lbtnProcess = row.FindControl("lbtnProcess") as LinkButton;
                LinkButton lbtnProcessCancel = row.FindControl("lbtnProcessCancel") as LinkButton;
                CheckBox chk_Req = row.FindControl("chk_Req") as CheckBox;

                InventoryRequest _inrReqDataTmp = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = col_itr_req_no.Text }).FirstOrDefault();
                if (_inrReqDataTmp != null)
                {
                    if (_inrReqDataTmp.Itr_req_wp == 1)
                    {
                        if (_inrReqDataTmp.Itr_req_wp_usr != Session["UserID"].ToString())
                        {
                            string _msg = "Request is already processed by user : " + _inrReqDataTmp.Itr_req_wp_usr;
                            DispMsg(_msg, "N");
                            return;
                        }
                    }
                }

                Int32 _res = CHNLSVC.Inventory.UpdateIntReqProcessData(new InventoryRequest()
                {
                    Itr_req_no = col_itr_req_no.Text,
                    Itr_seq_no = Convert.ToInt32(col_itr_seq_no.Text),
                    Itr_req_wp = 1,
                    Itr_req_wp_usr = Session["UserID"].ToString()
                });
                if (_res > 0)
                {
                    foreach (GridViewRow _row in grdRequestDetails.Rows)
                    {
                        CheckBox tmpChk = _row.FindControl("chk_Req") as CheckBox;
                        tmpChk.Checked = false;
                    }
                    chk_Req.Checked = true;
                    lbtnProcess.Visible = false;
                    lbtnProcessCancel.Visible = true;
                    chk_Req_CheckedChanged_Click(chk_Req, null);
                    string _msg = "Request add to process successfully ! ";
                    DispMsg(_msg, "S");
                    UpdateGridRowColor();
                }

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnProcessCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                var row = (GridViewRow)btn.NamingContainer;
                Label col_itr_seq_no = row.FindControl("col_itr_seq_no") as Label;
                Label col_itr_req_no = row.FindControl("col_itr_req_no") as Label;
                LinkButton lbtnProcess = row.FindControl("lbtnProcess") as LinkButton;
                LinkButton lbtnProcessCancel = row.FindControl("lbtnProcessCancel") as LinkButton;
                CheckBox chk_Req = row.FindControl("chk_Req") as CheckBox;

                Int32 _res = CHNLSVC.Inventory.UpdateIntReqProcessData(new InventoryRequest()
                {
                    Itr_req_no = col_itr_req_no.Text,
                    Itr_seq_no = Convert.ToInt32(col_itr_seq_no.Text),
                    Itr_req_wp = 0,
                    Itr_req_wp_usr = Session["UserID"].ToString()
                });
                if (_res > 0)
                {
                    foreach (GridViewRow _row in grdRequestDetails.Rows)
                    {
                        CheckBox tmpChk = _row.FindControl("chk_Req") as CheckBox;
                        tmpChk.Checked = false;
                    }
                    chk_Req.Checked = false;
                    lbtnProcess.Visible = true;
                    lbtnProcessCancel.Visible = false;
                    string _msg = "Successfully removed !";
                    DispMsg(_msg, "S");
                    UpdateGridRowColor();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "S");
            }
        }
        private void UpdateGridRowColor()
        {
            foreach (GridViewRow _row in grdRequestDetails.Rows)
            {
                LinkButton lbtnProcessCancel = _row.FindControl("lbtnProcessCancel") as LinkButton;
                if (lbtnProcessCancel.Visible)
                {
                    _row.BackColor = Color.LightGray;
                }
            }
        }

        protected void btnSimClose_Click(object sender, EventArgs e)
        {

        }

        protected void dgvSimilerItm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void txtPrefexlocationpopup_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtApproval.Text))
            {
                InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = txtApproval.Text.Trim() }).FirstOrDefault();
                if (_invReq != null)
                {
                    if (_invReq.Itr_stus == "F")
                    {
                        txtPrefexlocationpopup.Text = string.Empty;
                        DispMsg("Request already finished ! ");
                    }
                }
            }
            ApprovedPopup.Show();
        }

        protected void lbtnPrintAppDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtApproval.Text))
                {
                    PrintDocument(txtApproval.Text.Trim());
                }
                else
                {
                    DispMsg("Please select the document !");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        public void PrintDocument(string _docNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(_docNo))
                {
                    string[] seperator = new string[] { "|" };
                    string[] searchParams = _docNo.Split(seperator, StringSplitOptions.None);
                    foreach (string _doc in searchParams)
                    {
                        if (!string.IsNullOrEmpty(_doc))
                        {
                            #region print add by lakshan 30 Mar 2017
                            string targetFileName = "";
                            targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                            _invRepPara = new InvReportPara();
                            Session["InvReportPara"] = _invRepPara;
                            _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();//UserCompanyCode
                            _invRepPara._GlbReportCompName = Session["UserCompanyCode"].ToString();
                            _invRepPara._GlbUserID = Session["UserID"].ToString();
                            _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                            _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                            _invRepPara._GlbLocation = "";
                            Session["GlbReportName"] = "Dispatch_Req_Detail_Report.rpt";
                            Session["GlbReportType"] = "";
                            Session["InvReportPara"] = _invRepPara;
                            Dispatch_Req_Detail_Report _rptData = new Dispatch_Req_Detail_Report();
                            ItemDispatchDetailReport(_invRepPara, _doc, out _rptData);
                            PrintPDF(targetFileName, _rptData);
                            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", "Run Ok", Session["UserID"].ToString());
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Distribution approval Print Document", "Distribution approval", ex.Message, Session["UserID"].ToString());
            }
        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;

                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                //rptDoc.FilePath = "";
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        public void ItemDispatchDetailReport(InvReportPara _objRepoPara, string _reqNo, out Dispatch_Req_Detail_Report _rptData)
        {

            _rptData = new Dispatch_Req_Detail_Report();
            DataTable param = new DataTable();
            DataRow dr;
            //tmp_user_pc = new DataTable();
            DataTable GLOB_DataTable = new DataTable();
            DataTable DidpatchReqDetails = new DataTable();

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("powered_by", typeof(string));

            dr = param.NewRow();

            string fdate = _objRepoPara._GlbReportFromDate.Date.ToShortDateString();
            string fdt = Convert.ToDateTime(fdate).ToShortDateString();

            string tdate = _objRepoPara._GlbReportToDate.Date.ToShortDateString();
            string tdt = Convert.ToDateTime(tdate).ToShortDateString();

            dr["fromdate"] = fdt;
            dr["todate"] = tdt;
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbReportCompName;
            dr["powered_by"] = _objRepoPara._GlbReportPoweredBy;

            param.Rows.Add(dr);

            GLOB_DataTable.Clear();

            DidpatchReqDetails = CHNLSVC.MsgPortal.GET_DISTRIBUTION_DOC_DATA_FOR_PRINT(_reqNo);

            DataTable realdata = new DataTable();
            DataRow redr;
            //Header Details
            realdata.Columns.Add("ITR_EXP_DT", typeof(string));
            realdata.Columns.Add("ITR_REQ_NO", typeof(string));
            realdata.Columns.Add("ITR_REF", typeof(string));
            realdata.Columns.Add("ITR_REC_TO", typeof(string));
            realdata.Columns.Add("ITR_ISSUE_FROM", typeof(string));
            //Item Details
            realdata.Columns.Add("MI_CD", typeof(string));
            realdata.Columns.Add("MI_SHORTDESC", typeof(string));
            realdata.Columns.Add("MI_MODEL", typeof(string));
            realdata.Columns.Add("ITRI_ITM_STUS", typeof(string));
            realdata.Columns.Add("ITRI_QTY", typeof(Int32));

            realdata.Columns.Add("ITR_NOTE", typeof(string));
            realdata.Columns.Add("ML_LOC_DESC", typeof(string));
            realdata.Columns.Add("ML_ADD1", typeof(string));
            realdata.Columns.Add("ML_ADD2", typeof(string));

            realdata.Columns.Add("ITRI_APP_QTY", typeof(Int32));

            realdata.Columns.Add("ROWNUM", typeof(Int32));

            int i = 0;
            foreach (var dis in DidpatchReqDetails.Rows)
            {

                redr = realdata.NewRow();
                string date = DidpatchReqDetails.Rows[i]["ITR_EXP_DT"].ToString();
                string dt = Convert.ToDateTime(date).ToShortDateString();
                redr["ITR_EXP_DT"] = dt;// DidpatchReqDetails.Rows[i]["ITR_DT"].ToString();
                redr["ITR_REQ_NO"] = DidpatchReqDetails.Rows[i]["ITR_REQ_NO"].ToString();
                redr["ITR_REF"] = DidpatchReqDetails.Rows[i]["ITR_REF"].ToString();
                redr["ITR_REC_TO"] = DidpatchReqDetails.Rows[i]["ITR_REC_TO"].ToString();
                redr["ITR_ISSUE_FROM"] = DidpatchReqDetails.Rows[i]["ITR_ISSUE_FROM"].ToString();

                redr["MI_CD"] = DidpatchReqDetails.Rows[i]["MI_CD"].ToString();
                redr["MI_SHORTDESC"] = DidpatchReqDetails.Rows[i]["MI_SHORTDESC"].ToString();
                redr["MI_MODEL"] = DidpatchReqDetails.Rows[i]["MI_MODEL"].ToString();
                redr["ITRI_ITM_STUS"] = DidpatchReqDetails.Rows[i]["ITRI_ITM_STUS"].ToString();
                redr["ITRI_QTY"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ITRI_QTY"]);

                redr["ITR_NOTE"] = DidpatchReqDetails.Rows[i]["ITR_NOTE"].ToString();
                redr["ML_LOC_DESC"] = DidpatchReqDetails.Rows[i]["ML_LOC_DESC"].ToString();
                redr["ML_ADD1"] = DidpatchReqDetails.Rows[i]["ML_ADD1"].ToString();
                redr["ML_ADD2"] = DidpatchReqDetails.Rows[i]["ML_ADD2"].ToString();

                redr["ITRI_APP_QTY"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ITRI_APP_QTY"]);

                redr["ROWNUM"] = Convert.ToInt32(DidpatchReqDetails.Rows[i]["ROWNUM"].ToString());



                realdata.Rows.Add(redr);
                i++;
            }


            // param.Rows.Add(realdata);
            if (_objRepoPara._GlbDispatchStatus == "A")
            {
                dr["status"] = "Pending";
            }
            else if (_objRepoPara._GlbDispatchStatus == "F")
            {
                dr["status"] = "Approved";
            }

            _rptData.Database.Tables["DISPATCH_REQ_DETAILS"].SetDataSource(realdata);
            _rptData.Database.Tables["param"].SetDataSource(param);

        }

        protected void btnPrintDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_printDoc))
                {
                    PrintDocument(_printDoc);
                }
                else
                {
                    //DispMsg("Please select the document !");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnPrintDocNo_Click(object sender, EventArgs e)
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
                    new MasterLocationNew()
                    {
                        Ml_loc_cd = txtShowroom.Text.Trim().ToUpper(),
                        Ml_com_cd = Session["UserCompanyCode"].ToString()
                    }).FirstOrDefault();

                MasterLocationNew _mstLoc2 = CHNLSVC.General.GetMasterLocations(
                   new MasterLocationNew()
                   {
                       Ml_loc_cd = _mstLoc.Ml_main_loc_cd,
                       Ml_com_cd = Session["UserCompanyCode"].ToString()
                   }).FirstOrDefault();
                decimal mainVal=0;
                if (_mstLoc2 != null && _mstLoc2.Ml_main_loc_cd != null)
                {
                    mainVal = _mstLoc2.Ml_bank_grnt_val;
                }
                if (_mstLoc != null)
                {
                    HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                    if (_sysPara != null)
                    {
                        if (_sysPara.Hsy_val == 1)
                        {
                            BGValue = _mstLoc.Ml_bank_grnt_val + mainVal;
                            StockVal = _mstLoc.Ml_app_stk_val;
                            ShStkVal = CHNLSVC.MsgPortal.GetMrnShowroomStockValue(Session["UserCompanyCode"].ToString(), txtShowroom.Text.Trim().ToUpper());
                            AppMrnVal = CHNLSVC.MsgPortal.GetApprovedMrnShowroomStockValue(Session["UserCompanyCode"].ToString(), txtShowroom.Text.Trim().ToUpper(), out _err);
                            //    FreeVal = BGValue + StockVal - ShStkVal - AppMrnVal;
                            FreeVal = BGValue - ShStkVal - AppMrnVal;
                            TotMrnComQty = 0;
                            TotMrnMainQty = 0;
                        }
                    }
                }
                #region dataset
                txtBGValue.Text = BGValue.ToString("#,###,##0.00##");
                txtStockVal.Text = StockVal.ToString("#,###,##0.00##");
                txtShStkVal.Text = ShStkVal.ToString("#,###,##0.00##");
                txtAppMrnVal.Text = AppMrnVal.ToString("#,###,##0.00##");
                txtFreeVal.Text = FreeVal.ToString("#,###,##0.00##");
                txtCurrMrnVal.Text = CurrMrnVal.ToString("#,###,##0.00##");
                txtTotMrnCom.Text = TotMrnComQty.ToString("#,###,##0.00##");
                txtTotQtyMain.Text = TotMrnMainQty.ToString("#,###,##0.00##");
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
                string _err = "";
                decimal _totVal = 0, _mainItmQty = 0, _subItmQty = 0;
                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "MRNDETSHOW", DateTime.Now.Date);
                if (_sysPara != null)
                {
                    if (_sysPara.Hsy_val == 1)
                    {
                        txtCurrMrnVal.Text = (0).ToString("#,###,##0.00##");
                        txtTotMrnCom.Text = (0).ToString("#,###,##0.00##");
                        txtTotQtyMain.Text = (0).ToString("#,###,##0.00##");
                        if (_reqItmList != null)
                        {
                            if (_reqItmList.Count > 0)
                            {
                                MasterItem _mstItm = new MasterItem();
                                foreach (var item in _reqItmList)
                                {
                                    _mstItm = CHNLSVC.General.GetItemMasterNew(item.Itri_itm_cd);
                                    if (_mstItm != null)
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
                                _totVal = CHNLSVC.MsgPortal.GetMrnItemsStockValue(Session["UserCompanyCode"].ToString(), txtShowroom.Text.Trim().ToUpper(), _reqItmList, out _err);
                                txtCurrMrnVal.Text = _totVal.ToString("#,###,##0.00##");
                                txtTotMrnCom.Text = _mainItmQty.ToString("#,###,##0.00##");
                                txtTotQtyMain.Text = _subItmQty.ToString("#,###,##0.00##");
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

        protected void lbtnFreeOK_Click(object sender, EventArgs e)
        {
            _isValueExceedSave = true;
            lbtnSave_Click(null, null);
        }

        protected void lbtnFreeNo_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }
    }
}