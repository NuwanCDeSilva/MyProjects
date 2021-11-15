using FastForward.SCMWeb.Services;
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
    public partial class MaterialRequisitionNoteApprovals : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                pageclear();
                txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtsearch.Text = string.Empty;
                txtPreferLocation.Text = string.Empty;
                rdbRout.Checked = true;
                lblsearch.Text = "Route";
            }
            else
            {
                lblpanelname.Text = "";
                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];

                string[] objectNames = ctrlName.ToString().Split('$');
                if (objectNames[objectNames.Length - 1] == "txtRequestcompany")
                {

                    btnPrefexLoc_Click(null, null);
                    txtApprovalqty.Focus();
                }
                else if (Session["popo"].ToString() == "true")
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
            }
        }
        List<InventoryRequest> _MRNRequest = new List<InventoryRequest>();
        List<InventoryRequestItem> _MRNRequestItem = new List<InventoryRequestItem>();
        List<InventoryLocation> _Invetory = new List<InventoryLocation>();
        protected bool _contine { get { return (bool)Session["_contine"]; } set { Session["_contine"] = value; } }

        protected int _maxline { get { return (int)Session["_maxline "]; } set { Session["_maxline "] = value; } }
        protected List<MasterItemComponent> _masterItemComponent { get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; } set { Session["_masterItemComponent"] = value; } }
        protected List<MasterBufferChannel> _MasterBufferChannel { get { return (List<MasterBufferChannel>)Session["_MasterBufferChannel"]; } set { Session["_MasterBufferChannel"] = value; } }
        private void pageclear()
        {
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
            //txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            // txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            // txtsearch.Text = string.Empty;
            // txtPreferLocation.Text = string.Empty;
            //rdbRout.Checked = true;
            // lblsearch.Text = "Route";
            _MRNRequest = new List<InventoryRequest>();
            _MRNRequestItem = new List<InventoryRequestItem>();
            _Invetory = new List<InventoryLocation>();
            All.Visible = true;
            lbtnShowroom.Visible = true;
            adhoc.Visible = false;
            txtShowroom.Text = string.Empty;
            txtApproval.Text = string.Empty;
            txtApproval.Text = string.Empty;
            txtSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtrequestNo.Text = string.Empty;
            txtPreferloc.Text = string.Empty;
            txtAuthorizedby.Text = Session["UserID"].ToString();
            txtAuthorizedby.Text = Session["UserID"].ToString();
            txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtCompany.Text = Session["UserCompanyCode"].ToString();
            txtremark.Text = string.Empty;
            lblcompany.Text = Session["UserCompanyCode"].ToString();
            txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
            txtRequesttype.Text = string.Empty;
            Session["Itemcode"] = "";
            Session["Isalreadysave"] = "false";
            txtItem.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtItemRemark.Text = string.Empty;
            txtshopstock.Text = string.Empty;
            txtForwardsale.Text = string.Empty;
            txtBufferLimit.Text = string.Empty;
            txtRequestqty.Text = string.Empty;
            txtPrefexlocation.Text = string.Empty;
            ddlPurchasetype.SelectedIndex = -1;
            txtApprovalqty.Text = string.Empty;

            txtpopupItem.Text = string.Empty;
            txtpopupModel.Text = string.Empty;
            txtpopupItemRemark.Text = string.Empty;
            txtpopupshopstock.Text = string.Empty;
            txtpopupForwardsale.Text = string.Empty;
            txtpopupBufferLimit.Text = string.Empty;
            txtpopupRequestqty.Text = string.Empty;
            txtpopupIcompany.Text = string.Empty;
            txtPrefexlocationpopup.Text = string.Empty;
            ddlPurchasetypepopup.SelectedIndex = -1;
            txtpopupApprovalqty.Text = string.Empty;

            Addpurchasetype(txtRequestcompany.Text);
            txtItemRemark.Text = "N/A";
            btnuploadSear_Click(null, null);
            lblMonthlyAcc.Text = "";
            lblYearAcc.Text = "";
            lblMonthlyVal.Text = "";
            lblYearVal.Text = "";
        }

        #region root search option
        protected void rdbRout_CheckedChanged(object sender, EventArgs e)
        {
            lblsearch.Text = "Route";
            txtsearch.Text = "";
            txtsearch.Enabled = false;
            txtPreferLocation.Text = "";
            chkSearchall.Checked = true;
            All.Visible = false;

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
                case CommonUIDefiniton.SearchUserControlType.Item:
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
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["RequestCompany"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append("" + seperator + txtCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtItem.Text.ToUpper().ToUpper() + seperator + "I" + seperator + txtShowroom.Text);
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
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnuploadSear_Click(object sender, EventArgs e)
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
                _MRNRequest = CHNLSVC.Inventory.GetMRN_Req(txtPreferLocation.Text.ToUpper(), txtsearch.Text.ToUpper(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), "MRN", _para, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString());
                if (_MRNRequest != null)
                {
                    foreach (var item in _MRNRequest)
                    {
                        InventoryRequest _inrReqDataTmp = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = item.Itr_req_no }).FirstOrDefault();
                        if (_inrReqDataTmp != null)
                        {
                            item.Itr_req_wp = _inrReqDataTmp.Itr_req_wp;
                        }
                    }
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
        protected void chk_Req_CheckedChanged_Click(object sender, EventArgs e)
        {
            try
            {
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
                                    string _msg = "Request is already processed by user : " + _inrReqDataTmp.Itr_req_wp_usr;
                                    DispMsg(_msg, "N");
                                    return;
                                }
                            }
                        }
                        #endregion
                        lbtnShowroom.Visible = false;
                        adhoc.Visible = false;
                        string Reqno = (row.FindControl("col_itr_req_no") as Label).Text;
                        string Showroom = (row.FindControl("col_itr_loc") as Label).Text;
                        string ReqType = (row.FindControl("col_itr_sub_tp") as Label).Text;
                        string DeliverDate = (row.FindControl("col_itr_dt") as Label).Text;
                        string remark = (row.FindControl("col_itr_note") as Label).Text;
                        string ploc = (row.FindControl("col_itr_loc") as Label).Text;
                        string DispatchLocation = (row.FindControl("col_itr_issue_from") as Label).Text;
                        txtShowroom.Text = Showroom;
                        LoadHpValue();
                        txtrequestNo.Text = Reqno;
                        txtRequesttype.Text = ReqType;
                        txtDeliverDate.Text = DeliverDate;
                        txtremark.Text = remark;
                        txtPreferloc.Text = DispatchLocation;
                        txtPrefexlocation.Text = DispatchLocation;
                        _MRNRequestItem = CHNLSVC.Inventory.GetMRN_Req_item(Reqno);
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                        DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", "%" + Showroom);
                        if (result.Rows.Count > 0)
                        {
                            lblshowroomname.Visible = true;
                            lblshowroomname.Text = result.Rows[0][1].ToString();
                        }
                        decimal totalqty = 0;
                        foreach (InventoryRequestItem _item in _MRNRequestItem)
                        {
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
                                _item.Itri_Prev_sales_qty = CHNLSVC.Inventory.GET_PREVIOUS_SALES_QTY(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), _item.Itri_itm_cd, "DO");
                            }
                            if (chkFwdsale.Checked == true)
                            {
                                _item.Itri_fd_qty = GetForwardsale(txtShowroom.Text, _item.Itri_itm_cd, txtCompany.Text);
                            }
                            if (chkGIT.Checked == true)
                            {
                                _item.Itri_shop_qty = _item.Itri_shop_qty + GetGIT(_item.Itri_itm_cd);
                            }
                            totalqty = _item.Itri_bqty + totalqty;
                        }

                        var _filterMRNItem = _MRNRequestItem.Where(x => x.Itri_bqty != 0).ToList();

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
                        txtDeliverDate.Focus();
                    }
                    else
                    {
                        Session["Itemcode"] = "";
                        ViewState["_ApproveItem"] = "";
                        //ViewState["_MRNRequestItem"] = "";
                        grdInventoryBalance.DataSource = new int[] { };
                        grdInventoryBalance.DataBind();
                        grdApprovMRNitem.DataSource = new int[] { };
                        grdApprovMRNitem.DataBind();
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        // txtPrefexlocation.Text = string.Empty;
                        lblsale.Text = "0.00";
                        txtApprovalqty.Text = string.Empty;
                        Addpurchasetype(txtRequestcompany.Text);
                        //ddlPurchasetype.Items.Clear();
                        row.BackColor = System.Drawing.Color.Transparent;


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

        protected void chkSearchall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchall.Checked == false)
            {
                All.Visible = true;
                chkSearchall.Visible = true;
            }
            else
            {
                All.Visible = false;
                chkSearchall.Visible = false;
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

        protected void chk_ReqItem_CheckedChanged_Click(object sender, EventArgs e)
        {
            if (chkadhoc.Checked == true)
            {
                DisplayMessage("Please untick Ad-hoc", 2);
                return;
            }
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
                        txtModel.Text = ItemModel;
                        txtItemRemark.Text = ItemRemark;
                        txtRequestqty.Text = Req_qty;
                        txtApprovalqty.Text = Req_qty;
                        txtshopstock.Text = shopqty;
                        txtForwardsale.Text = fdqty;
                        txtBufferLimit.Text = bufferqty;
                        GetLASTMONTHSALE(Session["UserCompanyCode"].ToString(), txtShowroom.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), ItemCode);
                        HiddenItemLine.Value = itemline;
                        HiddenItemcode.Value = ItemCode;
                        GetBuferQty(ItemCode, "", 1);
                        GetBuferQty(ItemCode, "", 2);
                        row.BackColor = System.Drawing.Color.LightCyan;
                        GetInventory(ItemCode, Session["UserCompanyCode"].ToString(), string.Empty);
                        Session["Itemcode"] = ItemCode;
                        //txtItemRemark.Text = "N/A" ;
                        txtItemRemark.Focus();
                    }
                    else
                    {
                        ////Session["Itemcode"] = "";
                        ////txtItem.Text.ToUpper() = string.Empty;
                        ////txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        ////txtBufferLimit.Text = string.Empty;
                        ////txtshopstock.Text = string.Empty;
                        ////txtForwardsale.Text = string.Empty;
                        ////txtRequestqty.Text = string.Empty;
                        //txtRequestcompany.Text = string.Empty;
                        //txtPrefexlocation.Text = string.Empty;
                        ////txtApprovalqty.Text = string.Empty;
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


        private void GetInventory(string _Itemcode, string _com, string _status)
        {
            if (_Itemcode == null)
            {
                DisplayMessage("Please select Item...!", 2);
                return;
            }
            _Invetory = CHNLSVC.Inventory.GetItemInventoryBalance(_com, txtShowroom.Text, _Itemcode, _status);
            List<InventoryLocation> _subLoc = new List<InventoryLocation>();
            _subLoc = CHNLSVC.Inventory.GETWH_INV_BALANCE(_com, "", _Itemcode, _status);

            if ((_Invetory != null) || (_subLoc != null))
            {
                if (_Invetory == null)
                {
                    _Invetory = _subLoc;
                }
                else
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

        protected void chkCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["Itemcode"] != null)
            {
                string ItemCode = Session["Itemcode"].ToString();
                GetInventory(ItemCode, Session["UserCompanyCode"].ToString(), string.Empty);
            }
        }

        protected void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["Itemcode"] != null)
            {
                string ItemCode = Session["Itemcode"].ToString();
                GetInventory(ItemCode, "", "Other");
            }
        }

        protected void lbtnApprovItem_Click(object sender, EventArgs e)
        {
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            if (_ApproveItem == null)
            {
                DisplayMessage("There is no approved items found", 2);
                return;
            }
            Toatalapprovalqty();
            ApprovedPopup.Show();
        }


        protected void btnRemark_Click(object sender, EventArgs e)
        {
            txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
            txtPrefexlocation.Focus();
        }

        protected void lbtnItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItem.Text.ToUpper() != "")
                {
                    if (ddlitemserchoption.SelectedValue == "0")
                    {
                        DataTable result1 = CHNLSVC.Inventory.GetItemfromAssambleItem(txtItem.Text.ToUpper());
                        grdcom.DataSource = result1;
                        grdcom.DataBind();
                        lblvalue.Text = "Com_Item";
                        searpnl.Visible = false;
                        ModalPopupCom.Show();
                        return;
                    }
                    // if (chkSimilarItem.Checked == true)
                    if (ddlitemserchoption.SelectedValue == "1")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                        DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, null, null);
                        grdResult.DataSource = result1;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result1);
                        lblvalue.Text = "Similar_Item";
                        searpnl.Visible = false;
                        lblpanelname.Text = "Similar Items";
                        UserPopup.Show();
                        return;
                    }

                    // if (chkReplaceItem.Checked == true)
                    if (ddlitemserchoption.SelectedValue == "2")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                        DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, null, null);
                        grdResult.DataSource = result1;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result1);
                        lblvalue.Text = "Replace_Item";
                        lblpanelname.Text = "Replace Items";

                        UserPopup.Show();
                        return;
                    }
                    // if (chkadhoc.Checked == true)
                    if (ddlitemserchoption.SelectedValue == "3")
                    {
                        if (_MRNRequestItem == null)
                        {
                            DisplayMessage("Request Item is empty..!", 2);
                            return;
                        }
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result);
                        lblvalue.Text = "Item";
                        lblpanelname.Text = "New Items";
                        searpnl.Visible = true;
                        UserPopup.Show();
                        return;
                    }
                    if (chkadhoc2.Checked == true)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result);
                        lblvalue.Text = "Item";
                        UserPopup.Show();
                        return;
                    }
                }
                else
                {
                    // if (chkadhoc.Checked == true)
                    if (ddlitemserchoption.SelectedValue == "3")
                    {
                        if (_MRNRequestItem == null)
                        {
                            DisplayMessage("Request Item empty..!", 2);
                            return;
                        }
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result);
                        lblvalue.Text = "Item";
                        UserPopup.Show();
                        return;
                    }
                    if (chkadhoc2.Checked == true)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        BindUCtrlDDLData(result);
                        lblvalue.Text = "Item";
                        UserPopup.Show();
                        return;
                    }
                    else
                    {
                        DisplayMessage("Please select option to add item", 2);
                    }
                }


            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtncomapny_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Company";

                UserPopup.Show();

                return;
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnRCompnay_Click(object sender, EventArgs e)
        {
            checkcompany(txtRequestcompany.Text);
            Addpurchasetype(txtRequestcompany.Text);
            txtPrefexlocation.Text = "";
            txtPrefexlocation.Focus();
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
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnPrefexLoc_Click(object sender, EventArgs e)
        {
            Session["RequestCompany"] = txtRequestcompany.Text;
            checkLocation(txtPrefexlocation.Text);
            Addpurchasetype(txtRequestcompany.Text);
            //ddlPurchasetype.Focus();
            txtApprovalqty.Focus();
            //txtApprovalqty.BackColor = System.Drawing.Color.LightBlue;
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

        private void checkLocation(string _loc)
        {

            bool Isalreaysave = Convert.ToBoolean(Session["Isalreadysave"].ToString());

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "code", _loc);
            if ((_result == null) || (_result.Rows.Count == 0))
            {
                DisplayMessage("Please type correct Location..!", 2);
                txtPrefexlocation.Text = string.Empty;
                txtPrefexlocation.Focus();
                return;
            }



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
        protected void lbtnAddItem_Click(object sender, EventArgs e)
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

            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            string qty = string.Empty;
            bool _result = CHNLSVC.General.Check_MRN_Item(txtRequestcompany.Text, txtShowroom.Text, txtItem.Text.ToUpper().ToUpper(), out qty);


            if (_result == false)
            {
                DisplayMessage("Unsettled pending qty-" + qty, 2);
                //return;
            }
            if (chkadhoc2.Checked == true)
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
                if (string.IsNullOrEmpty(txtShowroom.Text))
                {
                    DisplayMessage("Please enter showroom..!", 2);
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
                if (ddlPurchasetype.SelectedIndex == 0)
                {
                    DisplayMessage("Please select purchase type..!", 2);
                    return;
                }

                #endregion
                
                #region Add new item into MRN request

                var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == txtItem.Text.ToUpper().ToUpper() && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                if (_checkallreadyadd != null)
                {
                    _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                    _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                    _checkallreadyadd.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _checkallreadyadd.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _checkallreadyadd.Itri_note = txtItemRemark.Text;
                    _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                    _checkallreadyadd.Itri_base_req_no = txtrequestNo.Text;
                    _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                    _checkallreadyadd.Backqty = _checkallreadyadd.Itri_bqty;
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
                    ViewState["_ApproveItem"] = _ApproveItem;
                    txtItem.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtItemRemark.Text = string.Empty;
                    txtBufferLimit.Text = string.Empty;
                    txtshopstock.Text = string.Empty;
                    txtForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtPrefexlocation.Text = txtPreferloc.Text;
                    Addpurchasetype(txtRequestcompany.Text);
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    return;
                }
                else
                {
                    InventoryRequestItem _item = new InventoryRequestItem();
                    _item.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _item.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _item.Itri_note = txtItemRemark.Text;
                    _item.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.Itri_qty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.PoType = ddlPurchasetype.SelectedItem.Text;
                    _item.Itri_itm_cd = txtItem.Text.ToUpper();
                    _item.Itri_note = txtItemRemark.Text;
                    _item.Mst_item_model = txtModel.Text;
                    _item.Backqty = _item.Itri_bqty;
                    _ApproveItem.Add(_item);
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();

                    ViewState["_ApproveItem"] = _ApproveItem;
                    txtItem.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtItemRemark.Text = string.Empty;
                    txtBufferLimit.Text = string.Empty;
                    txtshopstock.Text = string.Empty;
                    txtForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtPrefexlocation.Text = txtPreferloc.Text;
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    return;
                }
                #endregion

            }
            #region validation
            if (_ApproveItem == null)
            {
                _ApproveItem = new List<InventoryRequestItem>();
            }
            if (_MRNRequestItem == null)
            {
                if (chkcomahhoc.Checked)
                {
                    #region Add new item into MRN request
                    var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                    if (_checkallreadyadd != null)
                    {
                        _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                        _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                        _checkallreadyadd.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_note = txtItemRemark.Text;
                        _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                        _checkallreadyadd.Itri_base_req_no = txtrequestNo.Text;
                        _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                        _checkallreadyadd.Backqty = _checkallreadyadd.Itri_bqty;
                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();
                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
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
                        _item.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _item.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _item.Itri_note = txtItemRemark.Text;
                        _item.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.Itri_qty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.PoType = ddlPurchasetype.SelectedItem.Text;
                        _item.Itri_itm_cd = txtItem.Text.ToUpper().ToUpper();
                        _item.Itri_line_no = line;
                        _item.Itri_note = txtItemRemark.Text;
                        _item.Mst_item_model = txtModel.Text;
                        _item.Itri_itm_stus = "GOD";

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
                                _new.Itri_note = txtItemRemark.Text;
                                _new.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                                _new.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                                _new.PoType = ddlPurchasetype.SelectedItem.Text;
                                _new.Itri_itm_cd = txtItem.Text.ToUpper().ToUpper();
                                _new.Itri_line_no = line;
                                _new.Itri_note = txtItemRemark.Text;
                                _new.Mst_item_model = txtModel.Text;
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

                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
                        Addpurchasetype(txtRequestcompany.Text);

                        return;
                    }
                    #endregion
                }
                else
                {
                    DisplayMessage("Request Item empty..!", 2);
                    return;
                }

            }
            if (string.IsNullOrEmpty(txtShowroom.Text))
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
            #endregion
            
            if (chkallItem.Checked == true)
            {
                #region add all Item into approval List
                _ApproveItem.Clear();
                checkcompany(txtRequestcompany.Text);
                Session["RequestCompany"] = txtRequestcompany.Text;
                checkLocation(txtPrefexlocation.Text);
                _ApproveItem = _MRNRequestItem;
                foreach (InventoryRequestItem _ITM in _ApproveItem)
                {
                    _ITM.Itri_app_qty = _ITM.Itri_bqty;
                    _ITM.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _ITM.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _ITM.Backqty = _ITM.Itri_bqty;
                    _ITM.PoType = ddlPurchasetype.SelectedItem.Text;

                }
                grdApprovMRNitem.DataSource = _ApproveItem;
                grdApprovMRNitem.DataBind();
                //  _MRNRequestItem.Clear();
                grdMRNReqItem.DataSource = new int[] { };
                grdMRNReqItem.DataBind();
                txtPrefexlocation.Text = txtPreferloc.Text;
                Addpurchasetype(txtRequestcompany.Text);
                if (grdMRNReqItem.Rows.Count != 0)
                {
                    grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                }
                _MRNRequestItem = new List<InventoryRequestItem>();
                ViewState["_ApproveItem"] = _ApproveItem;
                ViewState["_MRNRequestItem"] = _MRNRequestItem;
                return;
                #endregion
            }
            if (chkadhoc.Checked == true)
            {
                #region Add new item into MRN request
                var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                if (_checkallreadyadd != null)
                {
                    _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                    _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                    _checkallreadyadd.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _checkallreadyadd.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _checkallreadyadd.Itri_note = txtItemRemark.Text;
                    _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                    _checkallreadyadd.Itri_base_req_no = txtrequestNo.Text;
                    _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                    _checkallreadyadd.Backqty = _checkallreadyadd.Itri_bqty;
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
                    ViewState["_ApproveItem"] = _ApproveItem;
                    txtItem.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtItemRemark.Text = string.Empty;
                    txtBufferLimit.Text = string.Empty;
                    txtshopstock.Text = string.Empty;
                    txtForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                    txtPrefexlocation.Text = txtPreferloc.Text;
                    Addpurchasetype(txtRequestcompany.Text);
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    return;
                }
                else
                {
                    InventoryRequestItem _item = new InventoryRequestItem();
                    _item.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _item.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _item.Itri_note = txtItemRemark.Text;
                    _item.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.Itri_qty = Convert.ToDecimal(txtApprovalqty.Text);
                    _item.PoType = ddlPurchasetype.SelectedItem.Text;
                    _item.Itri_itm_cd = txtItem.Text.ToUpper().ToUpper();
                    _item.Itri_note = txtItemRemark.Text;
                    _item.Mst_item_model = txtModel.Text;
                    _maxline = _maxline + 1;
                    _item.Itri_line_no = _maxline;
                    _item.Itri_itm_stus = "GOD";
                    _ApproveItem.Add(_item);
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();

                    ViewState["_ApproveItem"] = _ApproveItem;
                    txtItem.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtItemRemark.Text = string.Empty;
                    txtBufferLimit.Text = string.Empty;
                    txtshopstock.Text = string.Empty;
                    txtForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                    txtPrefexlocation.Text = txtPreferloc.Text;
                    Addpurchasetype(txtRequestcompany.Text);
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    return;
                }
                #endregion
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
            checkcompany(txtRequestcompany.Text);
            Session["RequestCompany"] = txtRequestcompany.Text;
            checkLocation(txtPrefexlocation.Text);
            decimal _reqty = Convert.ToDecimal(txtRequestqty.Text);

            decimal _Appqty = Convert.ToDecimal(txtApprovalqty.Text);


            if (_reqty < _Appqty)
            {
                if (ddlitemserchoption.SelectedValue != "3")
                {
                    DisplayMessage("You are not permitted to exceed the requested quantity..!", 2);
                    return;
                }
                else
                {
                    #region Add new item into MRN request
                    var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                    if (_checkallreadyadd != null)
                    {
                        _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                        // _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                        _checkallreadyadd.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_note = txtItemRemark.Text;
                        _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                        _checkallreadyadd.Itri_base_req_no = txtrequestNo.Text;
                        _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                        _checkallreadyadd.Backqty = _checkallreadyadd.Itri_bqty;
                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();
                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
                        Addpurchasetype(txtRequestcompany.Text);
                        if (grdMRNReqItem.Rows.Count != 0)
                        {
                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                        }
                        return;
                    }
                    else
                    {
                        InventoryRequestItem _item = new InventoryRequestItem();
                        _item.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _item.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _item.Itri_note = txtItemRemark.Text;
                        _item.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.Itri_qty = Convert.ToDecimal(txtApprovalqty.Text);
                        _item.PoType = ddlPurchasetype.SelectedItem.Text;
                        _item.Itri_itm_cd = txtItem.Text.ToUpper().ToUpper();
                        _item.Itri_note = txtItemRemark.Text;
                        _item.Itri_itm_stus = "GOD";
                        _maxline = _maxline + 1;
                        _item.Itri_line_no = _maxline;
                        _item.Mst_item_model = txtModel.Text;
                        MasterItem _MstItem = new MasterItem();
                        _MstItem.Mi_cd = txtItem.Text.ToUpper();
                        _item.MasterItem = _MstItem;
                        _ApproveItem.Add(_item);
                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();

                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
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
            else if (_Appqty == 0)
            {
                DisplayMessage("cant approved  zero quantity..!", 2);
                txtApprovalqty.Text = string.Empty;
                return;
            }
            else if (_reqty == _Appqty)
            {
                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                if ((_checkItemMRNlist != null))
                {
                    var _checkallreadyadd = _ApproveItem.SingleOrDefault(T => T.Itri_com == txtRequestcompany.Text && T.Itri_itm_cd == HiddenItemcode.Value && T.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value) && T.Itri_loc == txtPrefexlocation.Text && T.PoType == ddlPurchasetype.SelectedItem.Text);
                    if (_checkallreadyadd != null)
                    {
                        _checkallreadyadd.Itri_app_qty = _checkallreadyadd.Itri_app_qty + Convert.ToDecimal(txtApprovalqty.Text);
                        _checkallreadyadd.Itri_bqty = _checkallreadyadd.Itri_bqty + Convert.ToDecimal(txtApprovalqty.Text);
                        _checkallreadyadd.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _checkallreadyadd.Itri_note = txtItemRemark.Text;
                        _checkallreadyadd.PoType = ddlPurchasetype.SelectedItem.Text;
                        _checkallreadyadd.Itri_base_req_no = txtrequestNo.Text;
                        _checkallreadyadd.Itri_base_req_line = _checkallreadyadd.Itri_line_no;
                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;
                        _MRNRequestItem.Remove(_checkItemMRNlist);

                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                        grdMRNReqItem.DataBind();
                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();

                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;

                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
                        Addpurchasetype(txtRequestcompany.Text);
                        if (grdMRNReqItem.Rows.Count != 0)
                        {
                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                        }
                        return;
                    }
                    else
                    {


                        _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        _checkItemMRNlist.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                        _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                        _checkItemMRNlist.Itri_bqty = Convert.ToDecimal(txtApprovalqty.Text);
                        _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                        _checkItemMRNlist.Itri_base_req_no = txtrequestNo.Text;
                        _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                        _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;

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
                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                        ViewState["_ApproveItem"] = _ApproveItem;
                        txtItem.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtItemRemark.Text = string.Empty;
                        txtBufferLimit.Text = string.Empty;
                        txtshopstock.Text = string.Empty;
                        txtForwardsale.Text = string.Empty;
                        txtRequestqty.Text = string.Empty;
                        txtRequestcompany.Text = string.Empty;
                        txtPrefexlocation.Text = string.Empty;
                        txtApprovalqty.Text = string.Empty;
                        ddlPurchasetype.Items.Clear();
                        txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                        txtPrefexlocation.Text = txtPreferloc.Text;
                        Addpurchasetype(txtRequestcompany.Text);
                        if (grdMRNReqItem.Rows.Count != 0)
                        {
                            grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                        }

                        return;
                    }

                }
            }
            else
            {
                var _checkItemMRNlist = _MRNRequestItem.SingleOrDefault(x => x.Itri_itm_cd == HiddenItemcode.Value && x.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value));
                if ((_checkItemMRNlist != null))
                {
                    _checkItemMRNlist.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                    _checkItemMRNlist.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                    _checkItemMRNlist.Itri_note = txtItemRemark.Text;
                    _checkItemMRNlist.Itri_app_qty = Convert.ToDecimal(txtApprovalqty.Text);
                    _checkItemMRNlist.PoType = ddlPurchasetype.SelectedItem.Text;
                    _checkItemMRNlist.Itri_base_req_no = txtrequestNo.Text;
                    _checkItemMRNlist.Itri_base_req_line = _checkItemMRNlist.Itri_line_no;
                    _checkItemMRNlist.Backqty = _checkItemMRNlist.Itri_bqty;

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
                        item.Itri_com = txtRequestcompany.Text.ToUpper().Trim();
                        item.Itri_loc = txtPrefexlocation.Text.ToUpper().Trim();
                        item.Itri_bqty = item.Itri_bqty - Convert.ToDecimal(txtApprovalqty.Text);
                        item.Approv_status = "Approved";
                        item.Itri_qty = _qty;
                    }
                    var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();
                    grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList();
                    grdMRNReqItem.DataBind();
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
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
                    txtModel.Text = string.Empty;
                    txtItemRemark.Text = string.Empty;
                    txtBufferLimit.Text = string.Empty;
                    txtshopstock.Text = string.Empty;
                    txtForwardsale.Text = string.Empty;
                    txtRequestqty.Text = string.Empty;
                    txtRequestcompany.Text = string.Empty;
                    txtPrefexlocation.Text = string.Empty;
                    txtApprovalqty.Text = string.Empty;
                    ddlPurchasetype.Items.Clear();
                    txtRequestcompany.Text = Session["UserCompanyCode"].ToString();
                    txtPrefexlocation.Text = txtPreferloc.Text;
                    Addpurchasetype(txtRequestcompany.Text);
                    if (grdMRNReqItem.Rows.Count != 0)
                    {
                        grdMRNReqItem.Rows[0].FindControl("chk_ReqItem").Focus();
                    }
                    return;
                }
            }


        }



        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                MasterItem _itemdetail = new MasterItem();

                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().ToUpper());
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
                lbtnAddItem_Click(null, null);

                txtApprovalqty.BackColor = System.Drawing.Color.White;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
            UserPopup.Show();
        }

        protected void lbtnItemSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "Item2";
                ApprovedPopup.Show();
                UserPopup.Show();
                return;

            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtncomapny2_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Company2";
                ApprovedPopup.Show();
                UserPopup.Show();
                return;
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
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnPrefexLoc2_Click(object sender, EventArgs e)
        {
            Session["RequestCompany"] = txtpopupIcompany.Text;
            checkLocation(txtPrefexlocationpopup.Text);
            Addpurchasetype(txtpopupIcompany.Text);
            ddlPurchasetypepopup.Focus();
            ApprovedPopup.Show();
        }

        protected void btnRCompnay2_Click(object sender, EventArgs e)
        {
            checkcompany(txtpopupIcompany.Text);
            Addpurchasetype2(txtpopupIcompany.Text);
            txtPrefexlocationpopup.Focus();
            ApprovedPopup.Show();
        }
        protected void btnRemark2_Click(object sender, EventArgs e)
        {
            ApprovedPopup.Show();
        }

        protected void chkcancelall_CheckedChanged(object sender, EventArgs e)
        {
            _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
            if (_MRNRequestItem != null)
            {
                if (chkcancelall.Checked == true)
                {
                    foreach (InventoryRequestItem _PendingItem in _MRNRequestItem)
                    {
                        _PendingItem.Approv_status = "Cancel";
                    }
                    grdMRNReqItem.DataSource = _MRNRequestItem;
                    grdMRNReqItem.DataBind();
                }
                else
                {
                    foreach (InventoryRequestItem _PendingItem in _MRNRequestItem)
                    {
                        _PendingItem.Approv_status = "";
                    }
                    grdMRNReqItem.DataSource = _MRNRequestItem;
                    grdMRNReqItem.DataBind();
                    ViewState["_MRNRequestItem"] = _MRNRequestItem;
                }
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
                    _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
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
                    _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    if (_MRNRequestItem != null)
                    {

                        foreach (var item in _MRNRequestItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno)))
                        {
                            item.Approv_status = "Cancel";
                        }

                        var _filtercancel = _MRNRequestItem.Where(x => x.Approv_status != "Cancel").ToList();

                        grdMRNReqItem.DataSource = _filtercancel.OrderBy(x => x.Approv_status == "Approved").ToList(); ;
                        grdMRNReqItem.DataBind();
                        ViewState["_MRNRequestItem"] = _MRNRequestItem;
                    }
                }
            }
        }

        private void Process()
        {
            int rowsAffected = 0;
            string _docNo = string.Empty;
            string _IntdocNo = string.Empty;
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
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
            _inventoryRequest.TMP_IS_RES_UPDATE = true;
            _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
            _inventoryRequest.Itr_sub_tp = txtRequesttype.Text;
            _inventoryRequest.Itr_loc = txtShowroom.Text;
            _inventoryRequest.Itr_ref = txtrequestNo.Text;
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
            _inventoryRequest.Itr_issue_from = txtPreferloc.Text.ToUpper().Trim();
            //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
            //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
            _inventoryRequest.Itr_act = 1;
            _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
            _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();

            //LOAD LOCATION BY SUBODANA
            string toloc = "";
            foreach (GridViewRow gvr in this.grdRequestDetails.Rows)
            {
                CheckBox check = (CheckBox)gvr.FindControl("chk_Req");

                Label lblLocation = (Label)gvr.FindControl("col_itr_loc");
                if (check.Checked)
                {
                    toloc = txtShowroom.Text.ToString(); //lblLocation.Text.ToString();

                }

            }
            if (string.IsNullOrEmpty(toloc))
            {
                _inventoryRequest.Itr_rec_to = txtShowroom.Text;  /*Session["UserDefLoca"].ToString();*/
            }
            else
            {
                _inventoryRequest.Itr_rec_to = toloc;  /*Session["UserDefLoca"].ToString();*/
            }
            #region validate item allocation balance 31 Oct 2016
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
                    List<InventoryAllocateDetails> _allAllocation = new List<InventoryAllocateDetails>();
                    List<InventoryAllocateDetails> _chnlAllocation = new List<InventoryAllocateDetails>();
                    InventoryLocation _inrLocBal = new InventoryLocation();
                    foreach (var item in _ApproveItem)
                    {
                        #region data load to validate
                        _reqAppQty = item.Itri_bqty;
                        _allAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                        {
                            Isa_com = Session["UserCompanyCode"].ToString(),
                            Isa_itm_cd = item.Itri_itm_cd,
                            Isa_itm_stus = item.Itri_itm_stus
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
                                Isa_itm_cd = item.Itri_itm_cd,
                                Isa_itm_stus = item.Itri_itm_stus
                            });
                            if (_chnlAllocation.Count > 0)
                            {
                                _chnlAllocationQty = _chnlAllocation.Sum(c => c.Isa_aloc_bqty);
                                if (_chnlAllocationQty > 0)
                                {
                                    _inventoryRequest.Temp_itr_chnl_allocation = true;
                                    item.Temp_itri_is_allocation = 1;
                                }
                            }
                            _allInvBal = CHNLSVC.Inventory.GET_INR_LOC_BAL_DATA(new InventoryLocation()
                            {
                                Inl_com = Session["UserCompanyCode"].ToString(),
                                Inl_loc = txtShowroom.Text.Trim().ToUpper(),
                                Inl_itm_cd = item.Itri_itm_cd,
                                Inl_itm_stus = item.Itri_itm_stus
                            });
                            //if (_inrLocBal != null)
                            //{
                            //    _allInvBal = _inrLocBal.Inl_free_qty;
                            //}
                        #endregion
                            #region validation
                            if (_reqAppQty > _chnlAllocationQty)
                            {
                                if (_reqAppQty > (_allInvBal - _allAllocationQty + _chnlAllocationQty))
                                {
                                    item.Temp_is_allocation_err = 1;
                                }
                                //else
                                //{
                                //    item.Temp_itri_is_allocation = 1;
                                //}
                                //item.Temp_is_allocation_err = 1;
                            }
                            //else
                            //{
                            //    item.Temp_itri_is_allocation = 1;
                            //}
                        }
                            #endregion
                    }
                    Int32 _errVal = _ApproveItem.Sum(c => c.Temp_is_allocation_err);
                    if (_errVal > 0)
                    {
                        DispMsg("No Unallocated Qty to issue Items !"); return;
                    }
                }
            }
            #endregion
            foreach (var item in _ApproveItem)
            {
                if (!string.IsNullOrEmpty(item.Itri_res_no) && item.Itri_res_no != "N/A")
                {
                    item.Itri_res_qty = item.Itri_qty;
                }
            }
            _inventoryRequest.InventoryRequestItemList = _ApproveItem;
            int _Ins = 0;
            _inventoryRequest.Temp_itr_chnl_allocation = true;
            _inventoryRequest.TMP_SEND_MAIL = true;
            string _outDoc = "";
            rowsAffected = CHNLSVC.Inventory.SaveMRNRequestApprove(_inventoryRequest, GenerateMasterAutoNumber(), _MRNRequestItem, _multipleshowroom, null, _contine, out _docNo, out _Ins, out _IntdocNo, out _outDoc);

            if (rowsAffected != -1)
            {
                string Msg = "Successfully saved. " + _docNo;
                DisplayMessage(Msg, 3);
                pageclear();
            }
            else
            {
                if (_Ins == 1)
                {
                    lblMssg.Text = "Location insurance value exceeding";
                    btnok.Enabled = true;
                    PopupConfBox.Show();
                }
                else
                {
                    DisplayMessage(_docNo, 4);
                }

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
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = txtShowroom.Text; // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            // masterAuto.Aut_moduleid = "REQD";
            masterAuto.Aut_number = 0;
            // masterAuto.Aut_start_char = "REQD";
            masterAuto.Aut_year = null;
            return masterAuto;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {

            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            lbtnSave.Enabled = false;
            lbtnSave.CssClass = "buttoncolorleft";
            lbtnSave.OnClientClick = "return Enable();";
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            if (_ApproveItem == null)
            {
                _ApproveItem = new List<InventoryRequestItem>();
            }
            if (_ApproveItem.Count == 0)
            {
                DisplayMessage("no approved item to save", 2);
                lbtnSave.Enabled = true;
                lbtnSave.CssClass = "buttonUndocolorLeft";
                lbtnSave.OnClientClick = "SaveConfirm();";
                return;
            }

            if (chkcomahhoc.Checked == true)
            {
                if (_contine == false)
                {
                    string _docNo = string.Empty;
                    string _IntdocNo = string.Empty;
                    int _Ins = 0;

                  CHNLSVC.Inventory.checkInsuvaluExcel(_ApproveItem,txtCompany.Text, _contine, out _docNo, out _Ins, out _IntdocNo);
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
                     // DisplayMessage("Location insurance value exceeding", 2);
                 }
               
            }
            else
            {
                foreach (InventoryRequestItem _itm in _ApproveItem)
                {
                    _itm.Itri_bqty = _itm.Itri_app_qty;
                    _itm.Itri_qty = _itm.Itri_app_qty;
                }
                bool _result = CHNLSVC.Inventory.Check_MRN_Item_exceed_Ins(_ApproveItem, txtCompany.Text, txtShowroom.Text, DateTime.Now.Date);
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
                    btnok.Enabled = true;
                    PopupConfBox.Show();
                    // DisplayMessage("Location insurance value exceeding", 2);
                }
            }
            //lbtnSave.Enabled = true;
            // lbtnSave.CssClass = "buttonUndocolorLeft";
            //lbtnSave.OnClientClick = "SaveConfirm();";

        }

        protected void lbtnApprovalMRN_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = Convert.ToDateTime(txtSearchDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtSearchDate.Text).Date.ToShortDateString();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "MRN_App";
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void chkallItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkallItem.Checked == true)
            {
                txtItem.Enabled = false;
                txtModel.Enabled = false;
                txtshopstock.Enabled = false;
                txtForwardsale.Enabled = false;
                txtBufferLimit.Enabled = false;
                txtRequestqty.Enabled = false;
                txtApprovalqty.Enabled = false;
                txtItemRemark.Enabled = false;
            }
            else
            {
                txtItemRemark.Enabled = true;
                txtApprovalqty.Enabled = true;
            }
        }

        private decimal GetBufferQty(string _Item)
        {
            decimal qty = 0;
            DataTable result = CHNLSVC.Inventory.GetBufferQty(txtCompany.Text, txtShowroom.Text, _Item, System.DateTime.Now);
            if ((result != null) && (result.Rows.Count > 0))
            {
                qty = Convert.ToDecimal(result.Rows[0]["BUFFER_QTY"].ToString());

            }
            return qty;
        }
        private decimal GetShopQty(string _Item)
        {
            decimal qty = 0;
            DataTable result = CHNLSVC.Inventory.GetShopQty(txtCompany.Text, txtShowroom.Text, _Item);
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

        protected void lbtncancelrequest_Click(object sender, EventArgs e)
        {
            bool Ischack = false;
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
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
                _MRNRequest = new List<InventoryRequest>();
                _MRNRequestItem = new List<InventoryRequestItem>();
                _Invetory = new List<InventoryLocation>();
                All.Visible = false;

                txtShowroom.Text = string.Empty;
                txtApproval.Text = string.Empty;
                txtApproval.Text = string.Empty;
                txtSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtrequestNo.Text = string.Empty;
                txtPreferloc.Text = string.Empty;
                txtAuthorizedby.Text = Session["UserID"].ToString();
                txtAuthorizedby.Text = Session["UserID"].ToString();
                txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                txtremark.Text = string.Empty;
                lblcompany.Text = Session["UserCompanyCode"].ToString();
                txtRequesttype.Text = string.Empty;
                Session["Itemcode"] = "";

                txtItem.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                txtshopstock.Text = string.Empty;
                txtForwardsale.Text = string.Empty;
                txtBufferLimit.Text = string.Empty;
                txtRequestqty.Text = string.Empty;
                txtRequestcompany.Text = string.Empty;
                txtPrefexlocation.Text = string.Empty;
                ddlPurchasetype.SelectedIndex = -1;
                txtApprovalqty.Text = string.Empty;
                txtpopupItem.Text = string.Empty;
                txtpopupModel.Text = string.Empty;
                txtpopupItemRemark.Text = string.Empty;
                txtpopupshopstock.Text = string.Empty;
                txtpopupForwardsale.Text = string.Empty;
                txtpopupBufferLimit.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.SelectedIndex = -1;
                txtpopupApprovalqty.Text = string.Empty;
            }
        }

        private decimal GetForwardsale(string _showroom, string _Item, string _com)
        {
            decimal functionReturnValue = 0;
            functionReturnValue = CHNLSVC.Inventory.GetForwardsale(_showroom, _Item, _com);
            return functionReturnValue;
        }

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
            _MRNRequest = null;
            _MRNRequestItem = null;
            _Invetory = null;
            All.Visible = false;
            lbtnShowroom.Visible = true;
            adhoc.Visible = false;
            txtShowroom.Text = string.Empty;
            txtApproval.Text = string.Empty;
            txtApproval.Text = string.Empty;
            txtSearchDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtrequestNo.Text = string.Empty;
            txtPreferloc.Text = string.Empty;
            txtAuthorizedby.Text = Session["UserID"].ToString();
            txtAuthorizedby.Text = Session["UserID"].ToString();
            txtDeliverDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtCompany.Text = Session["UserCompanyCode"].ToString();
            txtremark.Text = string.Empty;
            lblcompany.Text = Session["UserCompanyCode"].ToString();
            txtRequesttype.Text = string.Empty;
            Session["Itemcode"] = "";
            Session["Isalreadysave"] = "false";
            txtItem.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtItemRemark.Text = string.Empty;
            txtshopstock.Text = string.Empty;
            txtForwardsale.Text = string.Empty;
            txtBufferLimit.Text = string.Empty;
            txtRequestqty.Text = string.Empty;
            txtRequestcompany.Text = string.Empty;
            txtPrefexlocation.Text = string.Empty;
            ddlPurchasetype.SelectedIndex = -1;
            txtApprovalqty.Text = string.Empty;

            txtpopupItem.Text = string.Empty;
            txtpopupModel.Text = string.Empty;
            txtpopupItemRemark.Text = string.Empty;
            txtpopupshopstock.Text = string.Empty;
            txtpopupForwardsale.Text = string.Empty;
            txtpopupBufferLimit.Text = string.Empty;
            txtpopupRequestqty.Text = string.Empty;
            txtpopupIcompany.Text = string.Empty;
            txtPrefexlocationpopup.Text = string.Empty;
            ddlPurchasetypepopup.SelectedIndex = -1;
            txtpopupApprovalqty.Text = string.Empty;


            #endregion
            _MRNRequestItem = CHNLSVC.Inventory.GetMRN_Req_item(_doc);
            if (_MRNRequestItem.Count > 0)
            {

                DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(_doc);
                if (dtHeaders != null)
                {
                    txtShowroom.Text = dtHeaders.Rows[0]["itr_rec_to"].ToString();
                    txtApproval.Text = dtHeaders.Rows[0]["itr_req_no"].ToString();
                    txtrequestNo.Text = dtHeaders.Rows[0]["itr_ref"].ToString();
                    txtPreferloc.Text = dtHeaders.Rows[0]["itr_loc"].ToString();
                    txtremark.Text = dtHeaders.Rows[0]["itr_note"].ToString();
                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";
                }
                grdApprovMRNitem.DataSource = _MRNRequestItem;
                grdApprovMRNitem.DataBind();
                ViewState["_ApproveItem"] = _MRNRequestItem;
                txtpopupItem.Text = string.Empty;
                txtpopupModel.Text = string.Empty;
                txtpopupshopstock.Text = string.Empty;
                txtpopupForwardsale.Text = string.Empty;
                txtpopupBufferLimit.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                txtpopupApprovalqty.Text = string.Empty;
                Session["Isalreadysave"] = "true";
                Session["RequestCompany"] = txtCompany.Text;
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
                        string backqty = (row.FindControl("col_Aitri_bqty2") as Label).Text;
                        HiddenPOType.Value = (row.FindControl("col_PoType") as Label).Text;
                        if (backqty == "0")
                        {
                            backqty = Req_qty;
                        }
                        txtpopupItem.Text = ItemCode;
                        txtpopupModel.Text = ItemModel;
                        txtpopupItemRemark.Text = ItemRemark;
                        txtpopupRequestqty.Text = backqty;
                        txtpopupApprovalqty.Text = Req_qty;
                        txtpopupshopstock.Text = shopqty;
                        txtpopupForwardsale.Text = fdqty;
                        txtpopupBufferLimit.Text = bufferqty;
                        txtpopupIcompany.Text = com;
                        txtpopupApprovalqty.Text = Appqty;
                        Addpurchasetype2(txtpopupIcompany.Text);
                        txtPrefexlocationpopup.Text = loc;
                        ddlPurchasetypepopup.SelectedItem.Text = HiddenPOType.Value;


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
                        txtpopupModel.Text = string.Empty;
                        txtpopupItemRemark.Text = string.Empty;
                        txtpopupBufferLimit.Text = string.Empty;
                        txtpopupshopstock.Text = string.Empty;
                        txtpopupForwardsale.Text = string.Empty;
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

        protected void chkadhoc_CheckedChanged(object sender, EventArgs e)
        {

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

                        _allInvBal = CHNLSVC.Inventory.GET_INR_LOC_BAL_DATA(new InventoryLocation()
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
                List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
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
                    item.Itri_com = txtpopupIcompany.Text.ToUpper().Trim();
                    item.Itri_loc = txtPrefexlocationpopup.Text.ToUpper().Trim();
                    item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                    item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                }
                grdApprovMRNitem.DataSource = _ApproveItem;
                grdApprovMRNitem.DataBind();
                ViewState["_ApproveItem"] = _ApproveItem;

                txtpopupItem.Text = string.Empty;
                //txtpopupModel.Text = string.Empty;
                // txtpopupItemRemark.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupApprovalqty.Text = string.Empty;
                txtpopupshopstock.Text = string.Empty;
                txtpopupForwardsale.Text = string.Empty;
                txtpopupBufferLimit.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                Toatalapprovalqty();
                ApprovedPopup.Show();


                #endregion
            }
            else
            {
                List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
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
                else if (_reqty == _Appqty)
                {
                    foreach (var item in _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                    {
                        item.Itri_com = txtpopupIcompany.Text.ToUpper().Trim();
                        item.Itri_loc = txtPrefexlocationpopup.Text.ToUpper().Trim();
                        item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                        item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                    }
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();
                    ViewState["_ApproveItem"] = _ApproveItem;
                }
                else
                {
                    foreach (var item in _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)))
                    {
                        item.Itri_com = txtpopupIcompany.Text.ToUpper().Trim();
                        item.Itri_loc = txtPrefexlocationpopup.Text.ToUpper().Trim();
                        item.PoType = ddlPurchasetypepopup.SelectedItem.Text;
                        decimal _remaingValue = item.Itri_app_qty - Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_app_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_bqty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                        item.Itri_qty = Convert.ToDecimal(txtpopupApprovalqty.Text);
                    }
                    List<InventoryRequestItem> _Backtem = new List<InventoryRequestItem>();
                    _Backtem = _ApproveItem.Where(w => w.Itri_itm_cd == HiddenItemcode.Value && w.Itri_line_no == Convert.ToInt32(HiddenItemLine.Value)).ToList();
                    _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    if (_MRNRequestItem != null)
                    {
                        if (_MRNRequestItem.Count > 0)
                        {
                            decimal _bqty = _Backtem[0].Backqty - _Backtem[0].Itri_app_qty;
                            _Backtem.ForEach(w => w.Itri_bqty = _bqty);
                            _MRNRequestItem.AddRange(_Backtem);
                        }
                        else
                        {
                            _MRNRequestItem = _Backtem;
                        }
                    }
                    else
                    {
                        _MRNRequestItem = _Backtem;
                    }
                    grdApprovMRNitem.DataSource = _ApproveItem;
                    grdApprovMRNitem.DataBind();

                    grdMRNReqItem.DataSource = _MRNRequestItem;
                    grdMRNReqItem.DataBind();
                    ViewState["_ApproveItem"] = _ApproveItem;
                    ViewState["_MRNRequestItem"] = _MRNRequestItem;

                }
                txtpopupItem.Text = string.Empty;
                txtpopupModel.Text = string.Empty;
                txtpopupItemRemark.Text = string.Empty;
                txtpopupRequestqty.Text = string.Empty;
                txtpopupApprovalqty.Text = string.Empty;
                txtpopupshopstock.Text = string.Empty;
                txtpopupForwardsale.Text = string.Empty;
                txtpopupBufferLimit.Text = string.Empty;
                txtpopupIcompany.Text = string.Empty;
                txtPrefexlocationpopup.Text = string.Empty;
                ddlPurchasetypepopup.Items.Clear();
                Toatalapprovalqty();
                ApprovedPopup.Show();
            }

        }


        private decimal GetGIT(string _Item)
        {
            decimal _Gitqty = 0;
            DataTable _GitTbl = CHNLSVC.General.GetItemGIT(txtCompany.Text, txtShowroom.Text, _Item, null, null, null, null, null, null, 0);
            if (_GitTbl.Rows.Count > 0)
            {
                _Gitqty = Convert.ToDecimal(_GitTbl.Rows[0]["iti_qty"].ToString());
            }
            return _Gitqty;
        }

        protected void chkadhoc2_CheckedChanged(object sender, EventArgs e)
        {

            if (chkadhoc2.Checked == true)
            {
                PendingDiv.Enabled = false;
                pnlchk.Enabled = false;
            }
            else
            {
                PendingDiv.Enabled = true;
                pnlchk.Enabled = true;
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

        protected void txtApproval_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
            DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, "Document", txtApproval.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
            if (_result.Rows.Count == 0)
            {
                DisplayMessage("Please enter a valid approval number", 2);
                return;
            }

        }

        protected void lbtnAmend_Click(object sender, EventArgs e)
        {
            if (txtUpdateconformmessageValue.Value == "No")
            {
                return;
            }
            lbtnAmend.Enabled = false;
            lbtnAmend.CssClass = "buttoncolorleft";
            lbtnAmend.OnClientClick = "return Enable();";
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16035))
            {
                string Msg = "You dont have permission to Update .Permission code : 16035";
                DisplayMessage(Msg, 1);
                lbtnAmend.Enabled = true;
                lbtnAmend.CssClass = "buttonUndocolorLeft";
                lbtnAmend.OnClientClick = "UpdateConfirm();";
                return;
            }
            int rowsAffected = 0;
            string _docNo = string.Empty;
            List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
            if (_ApproveItem == null)
            {
                string Msg = "Please select approved MRN ";
                DisplayMessage(Msg, 2);
                lbtnAmend.Enabled = true;
                lbtnAmend.CssClass = "buttonUndocolorLeft";
                lbtnAmend.OnClientClick = "UpdateConfirm();";
                return;
            }
            foreach (InventoryRequestItem _req in _ApproveItem)
            {
                MasterItem _MstItem = new MasterItem();
                _req.Itri_bqty = _req.Itri_app_qty;
                _MstItem.Mi_cd = _req.Itri_itm_cd;
                _req.MasterItem = _MstItem;
            }
            InventoryRequest REQHDR = new InventoryRequest();
            REQHDR.Itr_req_no = txtApproval.Text.ToString();
            REQHDR.Itr_exp_dt = Convert.ToDateTime(txtDeliverDate.Text.ToString());
            REQHDR.Temp_itr_chnl_allocation = true;
            rowsAffected = CHNLSVC.Inventory.SaveMRNRequestApproveamend(_ApproveItem, REQHDR, out _docNo);

            if (rowsAffected != -1)
            {
                string Msg = "Successfully Updated. " + _docNo;
                DisplayMessage(Msg, 3);
                pageclear();
            }
            lbtnAmend.Enabled = true;
            lbtnAmend.CssClass = "buttonUndocolorLeft";
            lbtnAmend.OnClientClick = "UpdateConfirm();";
        }

        protected void chkadhoc_CheckedChanged1(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16043))
            {
                string Msg = "You do not have permission to adhoc .Permission code : 16043";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                return;
            }
        }

        protected void chkcomahhoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcomahhoc.Checked == true)
            {
                lblexcel.Text = "Stock Push";
                txtrequestNo.Text = "AD-HOC";
                // pnlComadhoc.Visible = true;
                // pnldefalt.Visible = false;
            }
            else
            {
                lblexcel.Text = "S/R Requesting";
                txtrequestNo.Text = " ";
                // pnldefalt.Visible = true;
                // pnlComadhoc.Visible = false;
            }

        }

        protected void txtShowroom_TextChanged(object sender, EventArgs e)
        {
            lblMonthlyAcc.Text = "";
            lblYearAcc.Text = "";
            lblMonthlyVal.Text = "";
            lblYearVal.Text = "";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtShowroom.Text);
            if (_result.Rows.Count == 0)
            {
                string Msg = "Invalid showrom";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                txtShowroom.Text = "";
                txtShowroom.Focus();
            }
            else
            {
                lblshowroomname.Text = _result.Rows[0][1].ToString();
                LoadHpValue();
            }
        }

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

        #region Modalpopup
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "route")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _rotetbl;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "showroom")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "showroom2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    DataTable result = CHNLSVC.CommonSearch.GetInventoryChannel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();

                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }

                if (lblvalue.Text == "Prefer_Loc_3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_4")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                //if (lblvalue.Text == "MRN_App")
                //{
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                //    DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                //    grdResult.DataSource = _result;
                //    grdResult.DataBind();
                //    UserPopup.Show();
                //    return;
                //}
                if (lblvalue.Text == "Similar_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Replace_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    UserPopup.Show();
                    return;
                }
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
                    lblshowroomname.Text = Des;
                    lblvalue.Text = "";
                    adhoc.Visible = true;
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
                if (lblvalue.Text == "Item")
                {
                    txtItem.Text = Name;
                    lblvalue.Text = "";
                    txtshopstock.Text = "0";
                    txtForwardsale.Text = "0";
                    txtBufferLimit.Text = "0";
                    txtRequestqty.Text = "0";
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
                    txtModel.Text = _itemdetail.Mi_model;
                    txtItemRemark.Focus();
                    UserPopup.Hide();
                    GetBuferQty(Name, "", 2);


                    return;
                }
                if (lblvalue.Text == "Item2")
                {
                    txtpopupItem.Text = Name;
                    lblvalue.Text = "";
                    ApprovedPopup.Show();
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Company")
                {
                    txtRequestcompany.Text = Name;
                    Session["RequestCompany"] = txtRequestcompany.Text;
                    lblvalue.Text = "";
                    Addpurchasetype(Name);
                    txtPrefexlocation.Focus();
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Company2")
                {
                    txtpopupIcompany.Text = Name;
                    lblvalue.Text = "";
                    Addpurchasetype(Name);
                    txtPrefexlocationpopup.Focus();
                    ApprovedPopup.Show();
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
                if (lblvalue.Text == "Prefer_Loc_3")
                {

                    txtPrefexlocationpopup.Text = Name;
                    UserPopup.Hide();
                    lblvalue.Text = "";

                    return;
                }
                if (lblvalue.Text == "MRN_App")
                {
                    string Maintype = grdResult.SelectedRow.Cells[3].Text;
                    string subtype = grdResult.SelectedRow.Cells[4].Text;
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
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Similar_Item")
                {
                    txtItem.Text = Name;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Replace_Item")
                {
                    txtItem.Text = Name;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_4")
                {
                    txtPrefexlocationpopup.Text = Name;
                    ApprovedPopup.Show();
                    lblvalue.Text = "";
                    UserPopup.Hide();
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
            grdResult.PageIndex = e.NewPageIndex;
            // grdResult.PageIndex = 1;
            try
            {
                if (lblvalue.Text == "route")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, null, null);
                    grdResult.DataSource = _rotetbl;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();

                    return;
                }
                if (lblvalue.Text == "showroom")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "showroom2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                    DataTable result = CHNLSVC.CommonSearch.GetInventoryChannel(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                //if (lblvalue.Text == "MRN_App")
                //{
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                //    DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, null, null);
                //    grdResult.DataSource = _result;
                //    grdResult.DataBind();
                //    grdResult.PageIndex = 0;
                //    UserPopup.Show();
                //    return;
                //}
                if (lblvalue.Text == "Similar_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, null, null);
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Replace_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, null, null);
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    grdResult.PageIndex = 0;
                    UserPopup.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "route")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable _rotetbl = CHNLSVC.CommonSearch.Search_Routes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _rotetbl;
                    grdResult.DataBind();
                    Session["popo"] = "true";
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
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "showroom2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    Session["popo"] = "true";
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
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    Session["popo"] = "true";
                    ApprovedPopup.Show();
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    Session["popo"] = "true";
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
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Prefer_Loc_4")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    ApprovedPopup.Show();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Similar_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Replace_Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                    DataTable result1 = CHNLSVC.Inventory.GetReplaceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result1;
                    grdResult.DataBind();
                    Session["popo"] = "true";
                    UserPopup.Show();
                    return;
                }
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
        protected void btnaClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            txtSearchbyword.Text = "";
            ApprovedPopup.Hide();
            lblvalue.Text = "";
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
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "MRN_App";
                UserDPopoup.Show();
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
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "MRN_App")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                grdResultD.SelectedIndex = 0;
                lblvalue.Text = "MRN_App";
                // txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                //txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
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
                UserDPopoup.Show();
                return;
            }

        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

            }
            else
            {
                txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
                //  string _Msg = "Please enter valid date.";
                //   DisplayMessage(_Msg, 2);
            }
            if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
            {

            }
            else
            {
                txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                // string _Msg = "Please enter valid date.";
                //  DisplayMessage(_Msg, 2);
            }
            if (lblvalue.Text == "MRN_App")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.Inventory.GetMRNApprov_doc(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "MRN_App";
                Session["MRN_App"] = "true";
                UserDPopoup.Show();
                return;
            }

        }
        #endregion


        protected void lbtnPrefeslocation4_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc_4";
                UserPopup.Show();
                ApprovedPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
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

            rowsAffected = CHNLSVC.Inventory.CANCEL_MRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtApproval.Text, "C", Session["UserID"].ToString(), DateTime.Now);
            if (rowsAffected == 1)
            {
                string Msg = "Successfully canceled. " + txtApproval.Text;
                DisplayMessage(Msg, 1);
            }
            else
            {
                string Msg = "can not cancel MRN number:" + txtApproval.Text;
                DisplayMessage(Msg, 2);
            }
        }

        protected void btnAddLocs_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucLoactionSearch.Company;
                string chanel = ucLoactionSearch.Channel;
                string subChanel = ucLoactionSearch.SubChannel;
                string area = ucLoactionSearch.Area;
                string region = ucLoactionSearch.Regien;
                string zone = ucLoactionSearch.Zone;
                string pc = ucLoactionSearch.ProfitCenter;
                DataTable dt = new DataTable();
                dt = ViewState["showrooms"] as DataTable;
                if (dt != null)
                {
                    DataTable dt2 = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                    dt.Merge(dt2);
                }
                else
                {
                    dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                }

                // select_LOC_List.Merge(dt);
                ViewState["showrooms"] = dt;
                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;
                grvLocs.DataBind();
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }

        }


        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (chkcomahhoc.Checked == true)
            {
                divUpcompleted.Visible = false;
                Label1.Visible = false;
                excelUpload.Show();
            }
            else
            {
                DisplayMessage("Please check adhoc", 1);
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Label1.Visible = false;
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
                divUpcompleted.Visible = true;
                DisplayMessage("Excel file upload completed. Do you want to process?", 2);
                excelUpload.Show();
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

                    List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();


                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            InventoryRequestItem _InventoryRequestItem = new InventoryRequestItem();
                            MasterItem _MstItem = new MasterItem();
                            _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
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
                            _InventoryRequestItem.Itri_itm_cd = GetExecelTbl[0].Rows[i][4].ToString();
                            _InventoryRequestItem.Itri_itm_stus = "GOD";
                            _InventoryRequestItem.Itri_note = "excel";
                            _InventoryRequestItem.PoType = "";
                            _MstItem.Mi_cd = GetExecelTbl[0].Rows[i][4].ToString();
                            _InventoryRequestItem.MasterItem = _MstItem;
                            _InventoryRequestItem.Itri_loc = GetExecelTbl[0].Rows[i][3].ToString();
                            _InventoryRequestItem.Itri_com = GetExecelTbl[0].Rows[i][2].ToString();
                            _InventoryRequestItem.Showroom = GetExecelTbl[0].Rows[i][1].ToString();
                            _ApproveItem.Add(_InventoryRequestItem);
                            ViewState["_ApproveItem"] = _ApproveItem;
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

        protected void btnok_Click(object sender, EventArgs e)
        {
            //PopupConfBox.Hide();
            btnok.Enabled = false;
            btnok.CssClass = "buttoncolorleft";
            _contine = true;
            Process();
            lbtnSave.Enabled = true;
            lbtnSave.CssClass = "buttonUndocolorLeft";
            lbtnSave.OnClientClick = "SaveConfirm();";
        }


        protected void btnno_Click(object sender, EventArgs e)
        {
            _contine = false;
            PopupConfBox.Hide();
            lbtnSave.Enabled = true;
            lbtnSave.CssClass = "buttonUndocolorLeft";
            lbtnSave.OnClientClick = "SaveConfirm();";
        }

        protected void chkSearchall_CheckedChanged1(object sender, EventArgs e)
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

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                ((CheckBox)e.Row.FindControl("chk_ReqItem")).TabIndex = (short)(e.Row.RowIndex + 102);
        }

        protected void chkSimilarItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSimilarItem.Checked == true)
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                DataTable result1 = CHNLSVC.Inventory.GetSimilarItem(SearchParams, null, null);
                grdResult.DataSource = result1;
                grdResult.DataBind();
                BindUCtrlDDLData(result1);
                lblvalue.Text = "Similar_Item";
                searpnl.Visible = false;
                UserPopup.Show();
                return;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
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
                // }
                //else
                //{
                //    foreach (InventoryRequestItem _PendingItem in _MRNRequestItem)
                //    {
                //        _PendingItem.Approv_status = "";
                //    }
                //    grdMRNReqItem.DataSource = _MRNRequestItem;
                //    grdMRNReqItem.DataBind();
                //    ViewState["_MRNRequestItem"] = _MRNRequestItem;
                //}
            }
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            // if (chkallItem.Checked == true)
            // {
            txtItem.Enabled = false;
            txtModel.Enabled = false;
            txtshopstock.Enabled = false;
            txtForwardsale.Enabled = false;
            txtBufferLimit.Enabled = false;
            txtRequestqty.Enabled = false;
            txtApprovalqty.Enabled = false;
            txtItemRemark.Enabled = false;
            //}
            //else
            //{
            //    txtItemRemark.Enabled = true;
            //    txtApprovalqty.Enabled = true;
            //}     
        }

        private void GetBuferQty(string _Item, string _season, int _option)
        {

            _MasterBufferChannel = CHNLSVC.Inventory.GetBufferQty_Season(_Item, Session["UserCompanyCode"].ToString(), txtShowroom.Text, _season, _option);
            if (_MasterBufferChannel.Count > 0)
            {
                if (_option == 1)
                {
                    foreach (MasterBufferChannel _cn in _MasterBufferChannel)
                    {
                        txtBufferLimit.Text = _cn.MBC_QTY.ToString();
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
                }
                else
                {
                    foreach (MasterBufferChannel _cn in _MasterBufferChannel)
                    {
                        txtBufferLimit.Text = _cn.MBC_QTY.ToString();
                    }
                }
            }

        }

        protected void ddlseason_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBuferQty(txtItem.Text.ToUpper(), ddlseason.SelectedValue, 3);

        }

        private void GetLASTMONTHSALE(string _com, string _pc, DateTime _from, DateTime _to, string _item)
        {
            DataTable result1 = CHNLSVC.Sales.GetLASTMONTHSALE(_com, _pc, _from, _to, _item);
            if (result1.Rows.Count > 0)
            {
                decimal value = Convert.ToDecimal(result1.Rows[0][0].ToString());
                lblsale.Text = value.ToString("#,##0.00");
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItem.Text.ToUpper() != "")
                {

                    MasterItem _itemdetail = new MasterItem();

                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().ToUpper());
                    if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
                    }
                    else
                    {
                        DisplayMessage("Please select valid Item code", 2);
                    }
                    // if (chkadhoc.Checked == true)
                    if (ddlitemserchoption.SelectedValue == "3")
                    {
                        if (_MRNRequestItem == null)
                        {
                            DisplayMessage("Request Item is empty..!", 2);
                            return;
                        }
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "ITEM", txtItem.Text.ToUpper().ToUpper());
                        if (result.Rows.Count == 0)
                        {
                            txtModel.Text = string.Empty;
                            txtItemRemark.Text = string.Empty;
                            txtRequestqty.Text = "0.0";
                            txtApprovalqty.Text = "0.0";
                            txtshopstock.Text = "0.0";
                            txtForwardsale.Text = "0.0";
                            txtBufferLimit.Text = "0.0";
                            HiddenItemLine.Value = "";
                            HiddenItemcode.Value = string.Empty;
                            DisplayMessage("Please enter valid  item", 2);
                            txtItem.Focus();
                            return;
                        }

                        txtModel.Text = _itemdetail.Mi_model;
                        txtItemRemark.Text = string.Empty;
                        txtRequestqty.Text = "0.0";
                        txtApprovalqty.Text = "0.0";
                        txtshopstock.Text = "0.0";
                        txtForwardsale.Text = "0.0";
                        txtBufferLimit.Text = "0.0";
                        HiddenItemLine.Value = "";
                        HiddenItemcode.Value = txtItem.Text.ToUpper().ToUpper();
                        GetLASTMONTHSALE(Session["UserCompanyCode"].ToString(), txtShowroom.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtItem.Text.ToUpper().ToUpper());
                        GetInventory(txtItem.Text.ToUpper().ToUpper(), Session["UserCompanyCode"].ToString(), string.Empty);
                        GetBuferQty(txtItem.Text.ToUpper().ToUpper(), "", 1);
                        GetBuferQty(txtItem.Text.ToUpper().ToUpper(), "", 2);
                        Session["Itemcode"] = txtItem.Text.ToUpper();
                        txtItemRemark.Focus();
                        GetBuferQty(txtItem.Text.ToUpper().ToUpper(), "", 2);
                        if (chkGIT.Checked)
                        {
                            decimal git = GetGIT(txtItem.Text.ToUpper());
                            decimal _shop = Convert.ToDecimal(txtshopstock.Text);
                            txtshopstock.Text = (_shop + GetGIT(txtItem.Text.ToUpper())).ToString();
                        }

                    }


                }


            }



            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void ddlitemserchoption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlitemserchoption.SelectedValue == "3")
            {
                txtItem.Enabled = true;
                txtItem.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                txtRequestqty.Text = "0.0";
                txtApprovalqty.Text = "0.0";
                txtshopstock.Text = "0.0";
                txtForwardsale.Text = "0.0";
                txtBufferLimit.Text = "0.0";
                HiddenItemLine.Value = "";
                HiddenItemcode.Value = string.Empty;
            }
            else if ((ddlitemserchoption.SelectedValue == "2") || (ddlitemserchoption.SelectedValue == "1"))
            {
                txtItem.Enabled = false;
            }

            else
            {
                txtItem.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                txtRequestqty.Text = "0.0";
                txtApprovalqty.Text = "0.0";
                txtshopstock.Text = "0.0";
                txtForwardsale.Text = "0.0";
                txtBufferLimit.Text = "0.0";
                HiddenItemLine.Value = "";
                HiddenItemcode.Value = string.Empty;
                txtItem.Enabled = false;
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
                    List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                    List<InventoryRequestItem> _MRNRequestItem = new List<InventoryRequestItem>();
                    List<InventoryRequestItem> _MRNRequestItem_new = new List<InventoryRequestItem>();
                    _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                    _MRNRequestItem = ViewState["_MRNRequestItem"] as List<InventoryRequestItem>;
                    _MRNRequestItem_new = _ApproveItem.Where(x => x.Itri_line_no == Convert.ToInt32(lineno.Text)).ToList();
                    foreach (InventoryRequestItem _fl in _MRNRequestItem_new)
                    {

                    }
                    if (_MRNRequestItem != null)
                    {
                        _MRNRequestItem.AddRange(_MRNRequestItem_new);


                        _ApproveItem.RemoveAll(x => x.Itri_line_no == Convert.ToInt32(lineno.Text));

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
                        grdMRNReqItem.DataSource = _MRNRequestItem;
                        grdMRNReqItem.DataBind();



                        grdApprovMRNitem.DataSource = _ApproveItem;
                        grdApprovMRNitem.DataBind();


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
                        ViewState["_ApproveItem"] = _ApproveItem;
                        ApprovedPopup.Show();
                    }
                }
                else
                {

                }



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
                //        List<InventoryRequestItem> _ApproveItem = new List<InventoryRequestItem>();
                //        _ApproveItem = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                //        if (_ApproveItem != null)
                //        {
                //            if (_ApproveItem.Count > 0)
                //            {

                //            }
                //        }

            }
            return false;
        }


        protected void lbtncomselect_Click(object sender, EventArgs e)
        {
            if (grdcom.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string ItemCode = (row.FindControl("col_comitm") as Label).Text;
                txtItem.Text = ItemCode;
            }

        }

        protected void txtApprovalqty_TextChanged(object sender, EventArgs e)
        {

            try
            {
                MasterItem _itemdetail = new MasterItem();

                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().ToUpper());
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




            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtPrefexlocation_TextChanged(object sender, EventArgs e)
        {
            txtApprovalqty.Focus();
            txtApprovalqty.BackColor = System.Drawing.Color.LightBlue;
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
        private void LoadHpValue()
        {
            try
            {
                lblMonthlyAcc.Text = "";
                lblYearAcc.Text = "";
                lblMonthlyVal.Text = "";
                lblYearVal.Text = "";
                MasterLocationNew _mstLoc = CHNLSVC.General.GetMasterLocation(new MasterLocationNew() { Ml_com_cd = Session["UserCompanyCode"].ToString(), Ml_loc_cd = txtShowroom.Text.Trim() });
                if (_mstLoc != null)
                {
                    if (!string.IsNullOrEmpty(_mstLoc.Ml_def_pc) && _mstLoc.Ml_def_pc != "N/A")
                    {
                        List<HpAccRestriction> _accRestMonList = CHNLSVC.Sales.getAccRest(_mstLoc.Ml_def_pc, DateTime.Today, 1);
                        if (_accRestMonList != null)
                        {
                            if (_accRestMonList.Count > 0)
                            {
                                HpAccRestriction _accResrMon = _accRestMonList[0];
                                lblMonthlyAcc.Text = _accResrMon.Hrs_no_ac.ToString();
                                lblMonthlyVal.Text = _accResrMon.Hrs_tot_val.ToString();
                            }
                        }
                        List<HpAccRestriction> _accRestYearList = CHNLSVC.Sales.getAccRest(_mstLoc.Ml_def_pc, DateTime.Today, 2);
                        if (_accRestYearList != null)
                        {
                            if (_accRestYearList.Count > 0)
                            {
                                HpAccRestriction _accResrYer = _accRestYearList[0];
                                lblYearAcc.Text = _accResrYer.Hrs_no_ac.ToString();
                                lblYearVal.Text = _accResrYer.Hrs_tot_val.ToString();
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
    }
}