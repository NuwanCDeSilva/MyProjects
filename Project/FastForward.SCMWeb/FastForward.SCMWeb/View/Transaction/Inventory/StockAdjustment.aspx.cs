using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class StockAdjustment : Base
    {
        #region add by lakshan 10Dec2017
       
        bool _showExcelPop
        {
            get { if (Session["_showExcelPopBL"] != null) { return (bool)Session["_showExcelPopBL"]; } else { return false; } }
            set { Session["_showExcelPopBL"] = value; }
        }
        bool _showErrPop
        {
            get { if (Session["_showErrPopBL"] != null) { return (bool)Session["_showErrPopBL"]; } else { return false; } }
            set { Session["_showErrPopBL"] = value; }
        }
        string _filPath
        {
            get { if (Session["_filPathBL"] != null) { return (string)Session["_filPathBL"]; } else { return ""; } }
            set { Session["_filPathBL"] = value; }
        }

        public string _mainModuleName
        {
            get { if (Session["_mainModuleName"] != null) { return (string)Session["_mainModuleName"]; } else { return ""; } }
            set { Session["_mainModuleName"] = value; }
        }
        List<StcAdjExcelItem> _StcAdjExcelItem
        {
            get { if (Session["_StcAdjExcelItem"] != null) { return (List<StcAdjExcelItem>)Session["_StcAdjExcelItem"]; } else { return new List<StcAdjExcelItem>(); } }
            set { Session["_StcAdjExcelItem"] = value; }
        }
        #endregion
        private List<InventoryRequestItem> ScanItemList = null;
        private List<string> SeqNumList = null;
        protected List<ReptPickSerialsSub> _reptPickSerialsSub { get { return (List<ReptPickSerialsSub>)Session["_reptPickSerialsSub"]; } set { Session["_reptPickSerialsSub"] = value; } }

        int cRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserDefLoca"] == null || Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                ValidateTrue();
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.Date.ToString();
                    BackDatefucntion();
                    string _defBin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (string.IsNullOrEmpty(_defBin))
                    {
                        string _mmsg = "Default Bin Not Setup For Location : " + Session["UserDefLoca"].ToString();
                        DispMsg(_mmsg);
                        Response.Redirect("~/View/ADMIN/Home.aspx"); 
                    }
                    else
                    {
                        Session["GlbDefaultBin"] = _defBin;
                    }
                    //added by nuwan for uc item clear option
                    Session["CLEARITEM"] = null;
                    PageClear();                    
                }
                else
                {
                    try
                    {
                        if (Session["Doc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            UserPopup.Hide();
                            Session["Doc"] = "";
                        }
                        else if (Session["TempDoc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            UserPopup.Hide();
                            Session["TempDoc"] = "";
                        }
                        else if (Session["Adv"].ToString() == "true")
                        {
                            UserAdPopup.Show();
                            UserDPopoup.Hide();
                            UserPopup.Hide();
                            Session["Adv"] = "";
                        }
                        else if(Session["test"] == "true")
                        {
                            UserPopup.Show();
                            Session["test"] = "";
                        }
                        
                    }
                        
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Reload Page..');", true);
                    }
                    
                }
                ChangeName();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        private void ChangeName()
        {
            var id = Request.QueryString["id"];
            if (id != null)
            {
                ItemLi.InnerText = "Asset";
                //GrnItemLabal.InnerText = "Add GRN Asset";
                //asstetStatusDiv.InnerText = "Asset Status";
                //NewItemDetails.InnerHtml = "New Asset Details";
                //NewItemDiv.InnerHtml = "New Asset";
                grdItems.Columns[3].HeaderText = "Asset";
                //grdDOSerials.Columns[4].HeaderText = "Asset";
                //showPickedLable.Text = "Show picked Asset on top";
                //ListItem.InnerText = "Asset";

            }

        }
        protected void grdItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
              var id = Request.QueryString["id"];
              if (id != null)
              {
                  if (e.Row.RowType == DataControlRowType.Header)
                  {
                      e.Row.Cells[3].Text = "Asset";
                  }
              }
        }
        protected void grdSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
              var id = Request.QueryString["id"];
              if (id != null)
              {
                  if (e.Row.RowType == DataControlRowType.Header)
                  {
                      e.Row.Cells[2].Text = "Asset";
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
                    lbtnDate.Visible = true;

                    DateTime Selecteddate = Convert.ToDateTime(txtDate.Text.Trim());
                    DateTime appfromdate = Convert.ToDateTime(_bdt.Gad_act_from_dt);
                    Session["appfromdate"] = appfromdate;
                    DateTime apptodate = Convert.ToDateTime(_bdt.Gad_act_to_dt);
                    Session["apptodate"] = apptodate;

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
                        if (txtDate.Text == DateTime.Now.Date.ToString())
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
                    lbtnDate.Visible = false;
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
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),out  _expMsg) == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _expMsg + "');", true);
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                    return;
                }
            }
        }
        private void BackDatefucntion()
        {
            bool _allowCurrentTrans = false;
            Session["GlbModuleName"] = "m_Trans_Inventory_StockAdjustment";
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDate, lblBackDateInfor, "", out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        string _Msg = "Back date not allow for selected date!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        txtDate.Focus();
                        return;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    string _Msg = "Back date not allow for selected date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                    txtDate.Focus();
                    return;
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                ddlType.Enabled = false;
                if(ddlType.SelectedValue == "+")
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    ucInScan.Visible = true;
                    ucOutScan.Visible = false;

                    ucInScan.adjustmentType = ddlType.SelectedItem.Text;
                    ucInScan.adjustmentTypeValue = ddlType.SelectedItem.Value;

                     var id = Request.QueryString["id"];
                     if (id != null)
                     {
                         ucInScan.changeName();
                     }
                    
                }
                else if (ddlType.SelectedValue == "-")
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    ucOutScan.Visible = true;
                    ucInScan.Visible = false;

                    ucOutScan.adjustmentTypeValue = ddlType.SelectedItem.Value;
                    var id = Request.QueryString["id"];
                    if (id != null)
                    {
                        ucOutScan.changeName();
                    }                    
                }
                else
                {
                    ucOutScan.Visible = false;
                    ucInScan.Visible = false;
                }
                ddlSeqNo.DataSource = SeqNumList;
                ddlSeqNo.DataBind();

                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtSubType_Click(object sender, EventArgs e)
        {
            try
            {                
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "123";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSubType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    txtSubType.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter the sub type !!!');", true);
                    return;
                }

                DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("ADJ", txtSubType.Text.ToString());
                if (_adjSubType.Rows.Count > 0)
                {
                    txtSubType.ToolTip = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                    txtRef.Focus();
                    return;
                }
                else
                {
                    txtSubType.ToolTip = string.Empty;
                    txtSubType.Text = string.Empty;
                    txtSubType.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter a valid sub type !');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnDocumentNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTypeSearch.SelectedValue == "0")
                {
                    txtDocumentNo.Text = string.Empty;
                    ddlTypeSearch.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select a document type !');", true);
                    return;
                }
                var sortedTable = new DataTable();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = null;
                if (chkTemporarySave.Checked == true)
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                   
                    lblvalue.Text = "TempDoc";
                    Session["TempDoc"] = "true";
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                    lblvalue.Text = "Doc";
                    Session["Doc"] = "true";
                   
                  //  lblvalue.Text = "Doc";
                   // Session["TempDoc"] = "true";
                }
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                //lblvalue.Text = "128";
                txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool _invalidDoc = true;
                int _direction = 0;
                int _lineNo = 0;

                if (ddlTypeSearch.SelectedValue == "0")
                {
                    txtDocumentNo.Text = string.Empty;
                    ddlTypeSearch.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please Select Type.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "ConfirmSave();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "ConfirmTempSave();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

                    ddlType.SelectedValue = "0";
                    txtSubType.Text = string.Empty;
                    txtRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Text = string.Empty;
                    //txtUserSeqNo.Clear();
                    #endregion

                    ucInScan.Visible = false;
                    ucOutScan.Visible = false;

                    txtDocumentNo.Text = string.Empty;
                    txtDocumentNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the document no !');", true);
                    return;
                }


                string Name = txtDocumentNo.Text;
                if (chkTemporarySave.Checked == false)
                {
                    txtDocumentNo.Text = Name;
                    lblvalue.Text = "";
                    Session["Doc"] = "";
                    Session["DocType"] = "Doc";
                    GetDocData(Name);
                    UserDPopoup.Hide();
                    return;
                }
                if (chkTemporarySave.Checked == true)
                {
                    txtDocumentNo.Text = Name;
                    lblvalue.Text = "";
                    Session["TempDoc"] = "";
                    Session["DocType"] = "TempDoc";
                    GetTempDocData(Name);
                    UserDPopoup.Hide();
                    return;
                }

                //string docNo = txtDocumentNo.Text;
                //string type = ddlTypeSearch.SelectedValue;


                //PageClear();

                //txtDocumentNo.Text = docNo;
                //ddlTypeSearch.SelectedValue = type;

                //if (type == "+")
                //{
                //    _direction = 1;
                //    ucInScan.Visible = true;
                //    ucOutScan.Visible = false;
                //}
                //else
                //{
                //    _direction = 0;
                //    ucInScan.Visible = false;
                //    ucOutScan.Visible = true;
                //}
                ////_direction = (type == "+") ? 1 : 0;

                //InventoryHeader _invHdr = new InventoryHeader();
                //_invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                //if (_invHdr == null)
                //{
                //    _invalidDoc = false;
                //    goto err;
                //}
                //if (_invHdr.Ith_doc_tp != "ADJ")
                //{
                //    _invalidDoc = false;
                //    goto err;
                //}
                //if (_invHdr.Ith_direct == true && _direction == 0)
                //{
                //    _invalidDoc = false;
                //    goto err;
                //}
                //if (_invHdr.Ith_direct == false && _direction == 1)
                //{
                //    _invalidDoc = false;
                //    goto err;
                //}

                //err:
                //    if (_invalidDoc == false)
                //    {
                //        txtDocumentNo.Text = string.Empty;
                //        txtDocumentNo.Focus();
                //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid Document No!.');", true);
                //        return;
                //    }
                //    else
                //    {
                //        //cmdPrint.Enabled = true;
                //        //gvItems.ReadOnly = true;
                //        //gvSerial.ReadOnly = true;
                //        //btnSave.Enabled = false;
                //        //txtAdjSubType.Text = _invHdr.Ith_sub_tp;
                //        //txtManualRef.Text = _invHdr.Ith_manual_ref;
                //        //txtOtherRef.Text = _invHdr.Ith_entry_no;
                //        //txtRemarks.Text = _invHdr.Ith_remarks;
                //        //txtUserSeqNo.Clear();
                //        //ddlSeqNo.Text = string.Empty;
                //    }

                
                //List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                //List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                //_serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                //if (_serList != null)
                //{
                //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                //    foreach (var itm in _scanItems)
                //    {
                //        _lineNo += 1;
                //        InventoryRequestItem _itm = new InventoryRequestItem();
                //        _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                //        _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                //        _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                //        _itm.Itri_line_no = _lineNo;
                //        _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                //        _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                //        _itm.Mi_model = itm.Peo.Tus_itm_model;
                //        _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                //        _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                //        _itmList.Add(_itm);

                //    }
                //    ScanItemList = _itmList;

                //    grdItems.AutoGenerateColumns = false;
                //    grdItems.DataSource = ScanItemList;
                //    grdItems.DataBind();

                //    grdSerial.AutoGenerateColumns = false;
                //    grdSerial.DataSource = _serList;
                //    grdSerial.DataBind();

                //}
                //else
                //{
                //    txtDocumentNo.Text = string.Empty;
                //    txtDocumentNo.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item not found!.');", true);                    
                //    return;
                //} 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlSeqNo.SelectedItem.Text))
                {
                    txtUserSeqNo.Text = ddlSeqNo.SelectedItem.Text;
                    LoadItems(txtUserSeqNo.Text);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtngrdItemsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtRequestNo.Text))
                //{
                //    lblWarning.Text = "Can Not Delete Items.";
                //    divWarning.Visible = true;
                //    return;
                //}
                //LinkButton btn = (LinkButton)sender;
                //GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                //item.RemoveAt(grdr.RowIndex);

                //txtTotalAmount.Text = item.Sum(x => x.AMOUNT).ToString("N2");

                //ViewState["int_req_itm"] = item;
                //grdItems.DataSource = item;
                //grdItems.DataBind();

                ///////////////////////////////////////////

                if (hdnItemDelete.Value == "No")
                {
                    return;
                }
                if (grdItems.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string _itemStatus = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    OnRemoveFromItemGrid(_itemCode, _itemStatus, _lineNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {
            try
            {
                //LinkButton btn = (LinkButton)sender;w
                //GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                //ViewState["Item"] = item[grdr.RowIndex].ITRI_ITM_CD;
                //ViewState["Location"] = item[grdr.RowIndex].ITRI_LOC;
                //ViewState["RowIndex"] = grdr.RowIndex;

                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                //DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, null, null);
                //grdResult1.DataSource = result;
                //grdResult1.DataBind();
                //lblvalue.Text = "50";
                //BindUCtrlDDLData1(result);
                //ViewState["SEARCH"] = result;


                //List<INT_REQ_SER> req_serList = new List<INT_REQ_SER>();
                //if (ViewState["int_req_ser"] != null)
                //{
                //    req_serList = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                //    req_serList = (from i in req_serList
                //                   where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                //                   select i).ToList();
                //}
                //grdSerial.DataSource = req_serList;
                //grdSerial.DataBind();

                //PopupSerial.Show();

                ///////////////////////////////////////////////////                         

                if (grdItems.Rows.Count == 0) return;

                if (ddlType.SelectedItem.Value == "+")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    List<InventoryRequestItem> item = (List<InventoryRequestItem>)ucInScan.ScanItemList;
                    
                    ucInScan.TXTItemCode.Text = item[grdr.RowIndex].Itri_itm_cd;
                    ucInScan.PopulateData(item[grdr.RowIndex].Itri_itm_cd, grdr.RowIndex);                    
                    return;
                
                }

                
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string _itemStatus = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                    SearchParams = SearchParams + "|" + _itemStatus + "|" + "NotSerial" + "|";
                    DataTable _result = CHNLSVC.Inventory.GetSupplierSerialWEB(SearchParams, "Item", _itemCode);
                    grdAdSearch.DataSource = _result;
                    grdAdSearch.DataBind();

                    _result.Columns.RemoveAt(0);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(1);
                    BindUCtrlDDLData3(_result);
                    lblAvalue.Text = "Serial";
                    UserAdPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtngrdSerialDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSerialDelete.Value == "No")
                {
                    return;
                }
                if (grdSerial.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string _item = (row.FindControl("ser_Item") as Label).Text;
                    string _serialID = (row.FindControl("ser_SerialID") as Label).Text;
                    string _status = (row.FindControl("ser_Status") as Label).Text;
                    string serialI = (row.FindControl("ser_Serial1") as Label).Text;
                    if (string.IsNullOrEmpty(_item)) return;
                    OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialID), _status, serialI);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {
                    int _direction = 0;
                    _direction = 0;
                    if (ddlTypeSearch.SelectedValue == "+") _direction = 1;
                    Session["documntNo"] = txtDocumentNo.Text;
                    string _document = Session["documntNo"].ToString();
                     
                    if (_document == "")
                    {
                        _document = txtDocumentNo.Text;
                       
                    }
                    if (_document != "")
                    {
                        if (_direction == 1)
                        {
                            Session["direction"] = "IN";
                        }
                        else
                        {
                            Session["direction"] = "OUT";
                        }

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

        protected void lbtnprintserial_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {
                    int _direction = 0;
                    _direction = 0;
                    if (ddlType.SelectedItem.Value == "+") _direction = 1;
                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        if (_direction == 1)
                        {
                            Session["direction"] = "IN";
                        }
                        else
                        {
                            Session["direction"] = "OUT";
                        }

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
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Lakshika 2016-09-13
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16054))
                {
                    if (hdnSave.Value == "Yes")
                    {
                        Process(false);
                    }
                }
                else
                {
                    DisplayMessage("Sorry, You have no permission to save/temp save! - ( Advice: Required permission code : 16054)", 2);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnTempSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Lakshika 2016-09-13
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16054))
                {
                    if (hdnTempSave.Value == "Yes")
                    {
                        Process(true);
                    }
                }
                else
                {
                    DisplayMessage("Sorry, You have no permission to save/temp save! - ( Advice: Required permission code : 16054)", 2);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnClear.Value == "Yes")
                {
                    PageClear();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        private void Process(bool IsTemp)
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(ddlType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type !');", true);
                    return;
                }
                //if (string.IsNullOrEmpty(txtOtherRef.Text))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Supplier');", true);
                //    return;
                //}

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the adjustment sub type ! ');", true);
                    return;
                }

                if (chkTemporarySave.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtDocumentNo.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have selected temporarily save option. Please deselect to temporarily save the document !');", true);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtRef.Text)) txtRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        {
                            if(Convert.ToDateTime(Session["appfromdate"]) > Convert.ToDateTime(txtDate.Text))
                            {
                                txtDate.Enabled = true;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date lower than the back date from date !');", true);
                                txtDate.Focus();
                                return;
                            }
                            if (Convert.ToDateTime(Session["apptodate"]) < Convert.ToDateTime(txtDate.Text))
                            {
                                txtDate.Enabled = true;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date greater than the back date to date !');", true);
                                txtDate.Focus();
                                return;
                            }
                        }
                        if (Convert.ToDateTime(txtDate.Text).Date > DateTime.Now.Date)
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date greater than current date !');", true);
                            txtDate.Focus();
                            return;
                        }
                        //Remove Udaya 31.08.2017

                        //if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        //{
                        //    txtDate.Enabled = true;
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date is not allowed for transaction !');", true);
                        //    // MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtDate.Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        if (Convert.ToDateTime(Session["appfromdate"]) > Convert.ToDateTime(txtDate.Text))
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date lower than the back date from date !');", true);
                            txtDate.Focus();
                            return;
                        }
                        if (Convert.ToDateTime(Session["apptodate"]) < Convert.ToDateTime(txtDate.Text))
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date greater than the back date to date !');", true);
                            txtDate.Focus();
                            return;
                        }
                        //Remove Udaya 31.08.2017

                        //txtDate.Enabled = true;
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date is not allowed for transaction !');", true);
                        ////MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtDate.Focus();
                        //return;
                    }
                }

                //TODO
                //foreach (GridViewRow row in this.grdItems.Rows)
                //{
                //    if (Convert.ToDecimal(row.Cells["itm_AppQty"].Value) > Convert.ToDecimal(row.Cells["itm_PickQty"].Value))
                //    {
                //        MessageBox.Show("All serials not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //}

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;
                _direction = 0;
                if (ddlType.SelectedItem.Value == "+") _direction = 1;

                _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);

                //TODO
                _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);

                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "ADJ");
                
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ");
                //if (reptPickSerialsList == null)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found');", true);
                //    // MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                foreach (ReptPickSerials item in reptPickSerialsList)
                {
                    item.Tus_orig_grnno = item.Tus_exist_grnno;
                    item.Tus_orig_supp = item.Tus_exist_supp;
                }

                #region Check Referance Date and the Doc Date
                if (_direction == 0)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    {
                        return;
                    }
                }
                #endregion
                if (hdnTempSave.Value == "No")
                {
                    return;
                }
                #region Check Duplicate Serials
                if (reptPickSerialsList != null)
                {
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
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials. ');", true);
                        // MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                #endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                inHeader.Ith_acc_no = "STOCK_ADJ";
                inHeader.Ith_anal_1 = "";

                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                if (Session["DocType"].ToString() == "TempDoc")
                {
                    inHeader.Ith_anal_10 = true;
                    inHeader.Ith_anal_2 = txtDocumentNo.Text;
                }
                else
                {
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_2 = "";
                }


                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";// txtDAdd1.Text.Trim();
                inHeader.Ith_del_add2 = "";// txtDAdd2.Text.Trim();
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";

                if (ddlType.SelectedItem.Value == "+")
                {
                    inHeader.Ith_direct = true;
                    inHeader.Ith_doc_tp = "ADJ";
                }
                else
                {
                    inHeader.Ith_direct = false;
                    inHeader.Ith_doc_tp = "ADJ";
                }
                inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                inHeader.Ith_doc_no = string.Empty;
                //inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                if (IsTemp == true)
                {
                    inHeader.Ith_entry_no = txtUserSeqNo.Text;
                }
                else
                {
                    inHeader.Ith_entry_no = "";
                }
                inHeader.Ith_entry_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtRef.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = txtOtherRef.Text.Trim();
                inHeader.Ith_remarks = txtRemarks.Text;
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;
                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
                #endregion

                int _line = 0;
                #region Update some serial items
                if (reptPickSerialsList != null)
                {
                    if (_direction == 1)
                    {
                        foreach (var _seritem in reptPickSerialsList)
                        {
                            _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                            _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                        }
                    }
                    else if (_direction == 0)
                    {
                        foreach (var _ADJSer in reptPickSerialsList)
                        {
                            _line = _line + 1;
                            _ADJSer.Tus_base_itm_line = _line;
                        }
                    }
                }
                #endregion

                #region Inventory balance
                MasterItem msitem = new MasterItem();
                var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus,x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                foreach (var itm in _scanItems)
                {

                      ////
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itm.Peo.Tus_itm_cd, itm.Peo.Tus_itm_stus);
                            if (_inventoryLocation != null)
                            {
                                if (_inventoryLocation.Count == 1)
                                {
                                    foreach (InventoryLocation _loc in _inventoryLocation)
                                    {
                                        decimal _formQty = 0;
                                        msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                        if (msitem.Mi_is_ser1 == 1)
                                        {
                                            _formQty = Convert.ToDecimal(itm.theCount);
                                        }
                                        else
                                        {
                                            _formQty = Convert.ToDecimal(itm.Peo.Tus_qty);
                                        }

                                        if (ddlType.Text != "+")
                                        {
                                            if (_formQty > _loc.Inl_free_qty)
                                            {
                                                string msg = "Please check the inventory balance - Item code (" + itm.Peo.Tus_itm_cd + ")";
                                                DisplayMessage(msg, 2);
                                                return;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    string msg = "Please check the inventory balance - Item code (" + itm.Peo.Tus_itm_cd + ")";
                                    DisplayMessage(msg, 2);
                                    return;
                                }
                            }
                            //else
                            //{
                            //    string msg = "Please check the inventory balance - Item code (" + itm.Peo.Tus_itm_cd + ")";
                            //    DisplayMessage(msg, 2);
                            //    return;
                            //}
                }
                #endregion


                #region Save Adj+ / Adj-
                inHeader.TMP_CHK_LOC_BAL = true;
                inHeader.Ith_gen_frm = "SCMWEB";
                if (_direction == 1)
                {
                    result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, IsTemp);
                }
                else
                {
                    result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, IsTemp);
                }
                
                //result = CHNLSVC.Inventory.SaveADJ(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, IsTemp, out documntNo);
                
                if (result != -99 && result >= 0)
                {
                    //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                    //if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                    //{
                    //    if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //    else _view.GlbReportName = "Outward_Docs.rpt";
                    //}
                    //else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                    //{
                    //    if (_direction == 1) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //    else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //}
                    //else
                    //{
                    //    if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //    else _view.GlbReportName = "Outward_Docs.rpt";
                    //}
                    //_view.GlbReportDoc = documntNo;
                    //_view.Show();
                    //_view = null;
                    string _msg = "Successfully Saved! Document No : " + documntNo;
                    Session["documntNo"] = documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                    PageClear();

                    if (_direction == 1)
                    {
                        Session["direction"] = "IN";                        
                    }
                    else
                    {
                        Session["direction"] = "OUT";                        
                    }

                    lblMssg.Text = "Do you want print now?";
                    PopupConfBox.Show();

                }
                else
                {
                    string _msg = documntNo + " Process Terminated : ADJ";
                   // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _msg + "');", true);
                    DisplayMessage(_msg, 4);
                    //MessageBox.Show(documntNo, "Process Terminated : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //PageClear();
                }

                #endregion
            }
            catch (Exception err)
            {
                  
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
           
        }
        #region error msg
        private void DisplayMessage(String Msg, Int32 option)
        {
            string _Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        #endregion
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

                //DocSubType
                if (lblvalue.Text == "123")
                {
                    txtSubType.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSubType_TextChanged(null, null);
                }
                //MovementDocDateSearch
                if (lblvalue.Text == "128")
                {
                    txtDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDocumentNo_TextChanged(null, null);
                }

                ViewState["SEARCH"] = null;

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
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        if (ddlTypeSearch.SelectedValue == "+") paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "1" + seperator);
                        else paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "0" + seperator);

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtOtherRef.Text);
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
        private void FilterData()
        {
            //DocSubType
            if (lblvalue.Text == "123")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "123";
                ViewState["SEARCH"] = result;
                Session["test"] = "true";
                UserPopup.Show();
            }
            //MovementDocDateSearch
            if (lblvalue.Text == "128")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "128";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //SupplierFrmSerial
            if (lblvalue.Text == "233")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "233";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
        }

        #region Modal Popup 2
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Doc")
            {
                txtDocumentNo.Text = Name;
                Session["documntNo"] = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                GetDocData(Name);
                UserDPopoup.Hide();
                ucInScan.adjustmentTypeValue = ddlTypeSearch.SelectedValue;
                ddlType.SelectedValue = ddlTypeSearch.SelectedValue;
                ddlType.DataBind();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                txtDocumentNo.Text = Name;
                Session["documntNo"] = Name;
                lblvalue.Text = "";
                Session["TempDoc"] = "";
                Session["DocType"] = "TempDoc";
                GetTempDocData(Name);
                UserDPopoup.Hide();
                ucInScan.adjustmentTypeValue = ddlTypeSearch.SelectedValue;
                ddlType.SelectedValue = ddlTypeSearch.SelectedValue;
                ddlType.DataBind();
                return;
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "Doc";
                txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                Session["TempDoc"] = "true";
                UserDPopoup.Show();
            }
        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                Session["TempDoc"] = "true";
                UserDPopoup.Show();
            }
        }
        #endregion
        #region Modal Popup 3
        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdAdSearch.SelectedRow.Cells[1].Text;
        }
        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAdSearch.PageIndex = e.NewPageIndex;

            if (lblAvalue.Text == "Adv-ser")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
            }
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, null, null);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
                return;
            }
        }
        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";
                UserAdPopup.Show();
                return;
            }
        }
        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";
                Session["Adv"] = "true";
                UserAdPopup.Show();
                return;
            }
        }
        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            // Add Item ...
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                if (chk.Checked)
                {
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                    txtOtherRef.Text = (dgvr.FindControl("Col_Supplier") as Label).Text;
                    txtOtherRef.Enabled = false;
                    bool _balnce = CheckItem(_item);
                    if (_balnce == true)
                    {
                        AddItem(_item, _UnitCost, _status, _qty, txtUserSeqNo.Text, _serial);
                    }

                }
            }
            // Add serial ...Save TEMP_PICK_SER
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                if (chk.Checked)
                {
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                    bool _balnce = CheckItem(_item);
                    //if (_balnce == true)
                    //{
                    //    AddSerials(_item, _serial, txtUserSeqNo.Text);
                    //}
                }
            }
        }
        protected bool CheckItem(string _item)
        {
            try
            {
                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);

                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No stock balance available');", true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        protected void OnRemoveFromItemGrid(string _itemCode, string _itemStatus, int _lineNo)
        {
            try
            {

               Boolean _res = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 1);

                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "ADJ");
                if (_list != null)
                    if (_list.Count > 0)
                    {
                        var _delete = (from _lst in _list
                                       where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_base_itm_line == _lineNo
                                       select _lst).ToList();

                        foreach (ReptPickSerials _ser in _delete)
                        {
                            Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id, _ser.Tus_ser_1);
                        }
                        //}
                    }

                //ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
                LoadItems(txtUserSeqNo.Text);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID, string serialI )
        {
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _itemCode, serialI);
                CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
            }
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

        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status, string serialI)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _item, serialI);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _status);
                }
                LoadItems(txtUserSeqNo.Text);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //private void AddSerials(string _item, string _Serial, string _Seqno)
        //{
        //    Int32 generated_seq = -1;
        //    MasterItem msitem = new MasterItem();
        //    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
        //    List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
        //    //if (IsTemp == true)
        //    //{
        //    //    Tempserial_list = Tempserial_list.Where(x => (x.Tus_ser_1 == _Serial)).ToList();
        //    //}
        //    //else
        //    //{
        //    Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();
        //    // }



        //    if (_Serial == "N/A" || _Serial == "")
        //    {
        //        return;
        //    }
        //    foreach (ReptPickSerials serial in Tempserial_list)
        //    {
        //        #region PRN
        //        Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("PRN", Session["UserCompanyCode"].ToString(), _Seqno, 0);
        //        if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
        //        {
        //            generated_seq = user_seq_num;
        //        }
        //        else
        //        {
        //            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PRN", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
        //            //assign user_seqno
        //            ReptPickHeader RPH = new ReptPickHeader();
        //            RPH.Tuh_doc_tp = "PRN";
        //            RPH.Tuh_cre_dt = DateTime.Today;
        //            RPH.Tuh_ischek_itmstus = true;
        //            RPH.Tuh_ischek_reqqty = true;
        //            RPH.Tuh_ischek_simitm = true;
        //            RPH.Tuh_session_id = Session["SessionID"].ToString();
        //            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
        //            RPH.Tuh_usr_id = Session["UserID"].ToString();
        //            RPH.Tuh_usrseq_no = generated_seq;

        //            RPH.Tuh_direct = false;
        //            RPH.Tuh_doc_no = _Seqno;
        //            //write entry to TEMP_PICK_HDR
        //            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

        //        }

        //        if (msitem.Mi_is_ser1 != -1)
        //        //change msitem.Mi_is_ser1 == true
        //        {
        //            int rowCount = 0;



        //            //-------------

        //            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
        //            //Update_inrser_INS_AVAILABLE
        //            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //            _reptPickSerial_.Tus_usrseq_no = generated_seq;
        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //            _reptPickSerial_.Tus_base_doc_no = _Seqno;
        //            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serial.Tus_itm_line);
        //            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
        //            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
        //            _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
        //            _reptPickSerial_.Tus_job_line = serial.Tus_job_line;

        //            //enter row into TEMP_PICK_SER
        //            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

        //            rowCount++;
        //            //isManualscan = true;

        //        }
        //        else
        //        {
        //            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //            _reptPickSerial_.Tus_usrseq_no = generated_seq;
        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //            _reptPickSerial_.Tus_base_doc_no = _Seqno;
        //            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serial.Tus_itm_line);
        //            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
        //            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
        //            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
        //            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
        //            _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
        //            _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
        //            _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
        //            _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
        //            _reptPickSerial_.Tus_ser_1 = "N/A";
        //            _reptPickSerial_.Tus_ser_2 = "N/A";
        //            _reptPickSerial_.Tus_ser_3 = "N/A";
        //            _reptPickSerial_.Tus_ser_4 = "N/A";
        //            _reptPickSerial_.Tus_ser_id = 0;
        //            _reptPickSerial_.Tus_serial_id = "0";
        //            _reptPickSerial_.Tus_unit_cost = 0;
        //            _reptPickSerial_.Tus_unit_price = 0;
        //            _reptPickSerial_.Tus_unit_price = 0;
        //            _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
        //            _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
        //            //enter row into TEMP_PICK_SER
        //            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
        //        }


        //        #endregion
        //    }

        //    if (ViewState["SerialList"] != null)
        //    {
        //        serial_list = ViewState["SerialList"] as List<ReptPickSerials>;
        //        serial_list = serial_list.Concat(Tempserial_list).ToList();
        //        grdSerial.DataSource = serial_list;
        //        grdSerial.DataBind();
        //        ViewState["SerialList"] = serial_list;
        //        return;
        //    }
        //    grdSerial.DataSource = Tempserial_list;
        //    grdSerial.DataBind();
        //    ViewState["SerialList"] = Tempserial_list;

        //}

        #endregion

        private void GetDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 0;
                                        
                    if (ddlTypeSearch.SelectedItem.Value == "+")
                    {
                        _direction = 1;
                        ucInScan.Visible = true;
                        ucOutScan.Visible = false;
                    }
                    else
                    {
                        _direction = 0;
                        ucInScan.Visible = false;
                        ucOutScan.Visible = true;
                    }

                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "ConfirmSave();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "ConfirmTempSave();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

                    ddlType.SelectedValue = "0";
                    txtSubType.Text = string.Empty;
                    txtRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Text = string.Empty;
                    //txtUserSeqNo.Clear();
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
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
                        //// MessageBox.Show("Invalid Document No!", "ADJ No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtDocumentNo.Text = "";
                        //txtDocumentNo.Focus();
                        //return;                       

                        ucInScan.Visible = false;
                        ucOutScan.Visible = false;

                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select a valid document !');", true);
                        return;
                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;

                        if (chkTemporarySave.Checked == true)
                        {
                            lbtnSave.Enabled = true;
                            lbtnSave.OnClientClick = "ConfirmSave();";
                            lbtnSave.CssClass = "buttonUndocolor";
                        }
                        else
                        {
                            lbtnSave.Enabled = false;
                            lbtnSave.OnClientClick = "ConfirmSave();";
                            lbtnSave.CssClass = "buttoncolor";
                        }

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "ConfirmTempSave();";
                        lbtnTempSave.CssClass = "buttoncolor";

                        txtSubType.Text = _invHdr.Ith_sub_tp;
                        txtRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_oth_docno;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        //txtUserSeqNo.Clear();
                        ddlSeqNo.Text = string.Empty;

                    }
                    #endregion
                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    //_serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    _serList = CHNLSVC.Inventory.GetSaveSerDet(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), DocNo, Session["UserID"].ToString(), "ADJ", Session["SessionID"].ToString(), 0);
                    if (_serList != null)
                    {
                        //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand}).Select(
                            group => new { Peo = group.Key, theCount = group.Sum(c => c.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            MasterItem _mstItm = new MasterItem();
                            _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);

                            if (_mstItm.Mi_is_ser1 == 1)
                            {
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            }
                            else
                            {
                                //_itm.Itri_app_qty = Convert.ToDecimal(itm.Peo.Tus_qty);
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                //_itm.Itri_qty = Convert.ToDecimal(itm.Peo.Tus_qty);
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            }

                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            //_itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itmList.Add(_itm);

                        }
                        ScanItemList = _itmList;
                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                        foreach (InventoryRequestItem _addedItem in ScanItemList)
                        {

                           
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _addedItem.Itri_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == _addedItem.Itri_itm_stus).Mis_desc;
                            }


                        }
                        foreach (ReptPickSerials _ser in  _serList)
                        {                         
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _ser.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == _ser.Tus_itm_stus).Mis_desc;
                            }


                        }
                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = ScanItemList;
                        grdItems.DataBind();
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();
                    }
                    //else
                    //{
                    //    //MessageBox.Show("Item not found!", "ADJ No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtDocumentNo.Text = "";
                    //    txtDocumentNo.Focus();
                    //    return;
                    //}
                    #endregion
                    foreach (GridViewRow gvr in grdItems.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                        LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                        Delrow.Enabled = false;
                        Delrow.OnClientClick = "return Enable();";

                    }
                    foreach (GridViewRow gvr in grdSerial.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                    }
                    ucOutScan.LBTNAdd.Enabled = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    Int32 affected_rows;
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 0;

                    if (ddlTypeSearch.SelectedItem.Value == "+")
                    {
                        _direction = 1;
                        ucInScan.Visible = true;
                        ucOutScan.Visible = false;
                        ddlType.SelectedValue = "0";
                        ddlType.SelectedValue = ddlTypeSearch.SelectedValue;
                        ucInScan.adjustmentTypeValue = ddlType.SelectedValue;
                        ucInScan.adjustmentType = ddlType.SelectedItem.Text;
                        //ddlType.SelectedItem.Text = ddlTypeSearch.SelectedItem.Text;
                        //ddlType.SelectedItem.Value = ddlTypeSearch.SelectedItem.Value;
                        //ddlType.SelectedValue = ddlTypeSearch.SelectedValue; 
                    }
                    else
                    {
                        _direction = 0;
                        ucInScan.Visible = false;
                        ucOutScan.Visible = true;
                        ddlType.SelectedValue = "0";
                        ddlType.SelectedValue = ddlTypeSearch.SelectedValue;
                        ucOutScan.adjustmentTypeValue = ddlType.SelectedValue;
                        ucOutScan.adjustmentType = ddlType.SelectedItem.Text;
                        //ddlType.SelectedItem.Text = ddlTypeSearch.SelectedItem.Text;
                        //ddlType.SelectedItem.Value = ddlTypeSearch.SelectedItem.Value;
                        //ddlType.SelectedValue = ddlTypeSearch.SelectedValue; 
                    }

                    //////////////#region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "ConfirmSave();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = false;
                    lbtnTempSave.OnClientClick = "ConfirmTempSave();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

                    
                    txtSubType.Text = string.Empty;
                    txtRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Text = string.Empty;
                    ////////////////////#endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);
                    ////////////////////////#region Check Valid Document No
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
                        //// MessageBox.Show("Invalid Document No!", "ADJ No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtDocumentNo.Text = "";
                        //txtDocumentNo.Focus();
                        //return;
                                            

                        ucInScan.Visible = false;
                        ucOutScan.Visible = false;

                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid Document No!');", true);
                        return;

                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;
                        foreach (GridViewRow gvr in grdItems.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                            LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;

                            Addrow.Enabled = true;
                            Delrow.Enabled = true;
                        }
                        foreach (GridViewRow gvr in grdSerial.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;

                            Addrow.Enabled = true;
                        }

                        //btnSave.Enabled = false;
                        lbtnSave.Enabled = true;
                        lbtnSave.OnClientClick = "ConfirmSave();";
                        lbtnSave.CssClass = "buttonUndocolor";

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "ConfirmTempSave();";
                        lbtnTempSave.CssClass = "buttoncolor";

                        txtSubType.Text = _invHdr.Ith_sub_tp;
                        txtRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_oth_docno;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        
                        //txtUserSeqNo.Clear();
                        ddlSeqNo.Text = string.Empty;
                    }
                    /////////////////////////////#endregion
                    txtUserSeqNo.Text = _invHdr.Ith_entry_no;
                    if (!string.IsNullOrEmpty(txtUserSeqNo.Text))
                    {
                        ucInScan.userSeqNo = txtUserSeqNo.Text;
                        ucOutScan.userSeqNo = txtUserSeqNo.Text;
                    }
                    // txtOtherRef.Text=_invHdr.
                    affected_rows = CHNLSVC.Inventory.DeleteTempPickObjs(Convert.ToInt32(txtUserSeqNo.Text));

                    LoadItems(txtUserSeqNo.Text);

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), txtDocumentNo.Text, 0);
                    if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                        txtUserSeqNo.Text = user_seq_num.ToString();
//                        ViewState["userSeqNo"] = txtUserSeqNo.Text;
                        ucInScan.userSeqNo = txtUserSeqNo.Text;
                        ucOutScan.userSeqNo = txtUserSeqNo.Text;
                    }
                    else
                    {
                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "ADJ";
                        RPH.Tuh_cre_dt = DateTime.Today;
                        RPH.Tuh_ischek_itmstus = true;
                        RPH.Tuh_ischek_reqqty = true;
                        RPH.Tuh_ischek_simitm = true;
                        RPH.Tuh_session_id = Session["SessionID"].ToString();
                        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        RPH.Tuh_usr_id = Session["UserID"].ToString();
                        RPH.Tuh_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);

                        RPH.Tuh_direct = false;
                        RPH.Tuh_doc_no = DocNo;
                        //write entry to TEMP_PICK_HDR
                        affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();

                    //List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                    //if (_serListT != null)
                    //{
                    //  //  var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //    foreach (var itm in _serListT)
                    //    {
                    //        _lineNo += 1;
                    //        InventoryRequestItem _itm = new InventoryRequestItem();
                    //        _itm.Itri_app_qty = itm.Inb_qty;
                    //        _itm.Itri_itm_cd = itm.Inb_itm_cd;
                    //        _itm.Itri_itm_stus = itm.Inb_itm_stus;
                    //        _itm.Itri_line_no = itm.Inb_itm_line;
                    //        _itm.Itri_qty = itm.Inb_base_qty;

                    //        MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Inb_itm_cd);
                    //        _itm.Mi_longdesc = msitem.Mi_longdesc;
                    //        _itm.Mi_model = msitem.Mi_model;
                    //        _itm.Mi_brand = msitem.Mi_brand;
                    //        _itm.Itri_unit_price = itm.Inb_unit_cost;
                    //        _itmList.Add(_itm);


                    //        List<InventoryRequestItem> _ListItem = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                    //        //if (_ListItem.Count <= Convert.ToInt16(0))
                    //        //{
                    //        // CHNLSVC.Inventory.SavePickedItems(;
                    //        AddItem(itm.Inb_itm_cd, itm.Inb_unit_cost.ToString(), itm.Inb_itm_stus, itm.Inb_qty.ToString(), txtUserSeqNo.Text, null);
                    //        // }
                    //    }

                    //    ScanItemList = _itmList;

                    //    grdItems.AutoGenerateColumns = false;
                    //    grdItems.DataSource = ScanItemList;
                    //    grdItems.DataBind();
                    //    ViewState["TempScanItemList"] = ScanItemList;
                    //    ucInScan.ScanItemList = ScanItemList;
                    //    ucOutScan.ScanItemList = ScanItemList;
                    //}

                    //Get serial details
                    //_serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtDocumentNo.Text);
                    _serList = CHNLSVC.Inventory.GetTempSaveDet(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), DocNo, Session["UserID"].ToString(), "ADJ", Session["SessionID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text));

                    if (_serList != null)
                    {
                        //if (_serList.Count > 0)
                        //{
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                       // var _scanItems = _serList;
            //            //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_unit_cost,,x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Sum() });
            //            var _invoice =
            //from _pickSerials in _serList
            //group _pickSerials by new { _pickSerials.Tus_base_doc_no, _pickSerials.Tus_base_itm_line,_pickSerials.Tus_itm_cd } into itm
            //select new { invoiceno = itm.Key.Tus_base_doc_no, lineno = itm.Key.Tus_base_itm_line, itemqty = itm.Sum(p => p.Tus_qty) };


                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            MasterItem _masItm = new MasterItem();
                            _masItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                            if (_masItm.Mi_is_ser1 == 1)
                            {
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            }
                            else
                            {
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.Peo.Tus_qty);
                                _itm.Itri_qty = Convert.ToDecimal(itm.Peo.Tus_qty);
                            }
                            
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itmList.Add(_itm);

                          


                            List<InventoryRequestItem> _ListItem = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                            //if (_ListItem.Count <= Convert.ToInt16(0))
                            //{
                            // CHNLSVC.Inventory.SavePickedItems(;
                            AddItem(itm.Peo.Tus_itm_cd, itm.Peo.Tus_unit_cost.ToString(), itm.Peo.Tus_itm_stus, _itm.Itri_qty.ToString(), txtUserSeqNo.Text, null);
                            // }
                        }

                            //ScanItemList = _itmList;

                            foreach (ReptPickSerials serial in _serList)
                            {
                                serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                // AddSerials(serial.Tus_itm_cd, serial.Tus_ser_1, txtUserSeqNo.Text);

                                List<ReptPickSerials> _Listserial = ViewState["SerialList"] as List<ReptPickSerials>;
                                if (_Listserial.Count <= Convert.ToInt16(0))
                                {
                                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                                }

                                //if (affected_rows == 0)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial Load Fail');", true);
                                //    return;
                                //}
                                // AddSerials(serial.Tus_itm_cd, serial.Tus_ser_1.ToString(), txtUserSeqNo.Text,true);
                            }
                            if (_serList != null)
                            {
                                var _FilterItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                                foreach (var itm in _scanItems)
                                {
                                    foreach (InventoryRequestItem dr in ScanItemList)
                                    {
                                        if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                                        {
                                            if (itm.Peo.Tus_qty > 1)
                                            {
                                                dr.Itri_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                                               // dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                                            }
                                            else
                                            {
                                                dr.Itri_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                               // dr.Itri_app_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                            }
                                        }
                                    }
                                }
                            }
                            grdItems.AutoGenerateColumns = false;
                            grdItems.DataSource = ScanItemList;
                            grdItems.DataBind();
                            ViewState["TempScanItemList"] = ScanItemList;
                            ucInScan.ScanItemList = ScanItemList;
                            ucOutScan.ScanItemList = ScanItemList;
                            grdSerial.AutoGenerateColumns = false;
                            grdSerial.DataSource = _serList;
                            grdSerial.DataBind();
                            ViewState["TempserList"] = _serList;
                        //}
                    }
                    //else
                    //{
                    //    //MessageBox.Show("Item not found!", "ADJ No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtDocumentNo.Text = "";
                    //    txtDocumentNo.Focus();
                    //    return;
                    //}
                    #endregion


                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item already available.');", true);
                                //return;
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
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                //Added by Prabhath on 17/12/2013 ************* end **************
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
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        //Added by Prabhath on 17/12/2013 ************* end **************
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
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }



                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    
                    _saveonly.Add(_reptitm);

                     List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _addedItem.Itri_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == _addedItem.Itri_itm_stus).Mis_desc;
                    }

                    
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                grdItems.DataSource = null;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;

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
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResultD.DataSource = _result;
            grdResultD.DataBind();
            UserDPopoup.Show();
        }

        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                if (ddlType.SelectedItem.Value == "+")
                {
                    _direction = 1;
                }

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ", Session["UserID"].ToString(), _direction, _seqNo);
                //if (user_seq_num == -1)
                //{
                //    user_seq_num = GenerateNewUserSeqNo();
                //}
                if (txtUserSeqNo.Text == "")
                {
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                }
                else
                {
                    user_seq_num = Convert.ToInt32(txtUserSeqNo.Text);
                }

                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ");

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    //var count = count(from ser in serList
                    //            where ser.Tus_itm_cd == _reptitem.Tui_req_itm_cd &&  ser.Tus_itm_stus == _reptitem.Tui_req_itm_stus                                
                    //            select new
                    //            {
                    //                ser
                    //            });
                    var count = 0;
                    if(serList == null)
                        count = 0;
                    else
                        count = serList.Count(x => x.Tus_itm_cd == _reptitem.Tui_req_itm_cd && x.Tus_itm_stus == _reptitem.Tui_req_itm_stus);
                    
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_itm_stus_desc = getItemStatusDesc(_itm.Itri_itm_stus);
                   
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    if (ddlType.SelectedValue == "+")
                    {
                        _itm.Itri_qty = Convert.ToInt32(count);
                    }
                    _itm.Itri_app_qty = Convert.ToInt32(count);
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                   // _itm.Itri_unit_price = Convert.ToDecimal(_reptitem..ToString());
                    _itm.Itri_supplier = _reptitem.Tui_sup;
                    _itm.Itri_batchno = _reptitem.Tui_batch;
                    _itm.Itri_grnno = _reptitem.Tui_grn;
                    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                    if (ddlType.SelectedValue == "+")
                    {
                        decimal _tmpCost = 0;
                        _itm.Itri_unit_price = decimal.TryParse(_reptitem.Tui_pic_itm_stus, out _tmpCost) ? Convert.ToDecimal(_reptitem.Tui_pic_itm_stus) : 0;
                    }
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
              //  ViewState["ScanItemList"] = ScanItemList;

                ucOutScan.ScanItemList = ScanItemList;
                ucInScan.ScanItemList = ScanItemList;

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ");
                if (_serList != null)
                {
                    foreach (ReptPickSerials itemSer in _serList)
                    {
                        itemSer.Tus_itm_stus_Desc = getItemStatusDesc(itemSer.Tus_itm_stus);

                        
                    }

                    //if (_direction == 0)
                    //{
                    //    //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    //    foreach (var itm in _scanItems)
                    //    {
                    //        foreach (GridViewRow row in grdItems.Rows)
                    //        {
                    //            if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                    //            {
                    //                ((Label)row.FindControl("lblitri_app_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //    foreach (var itm in _scanItems)
                    //    {
                    //        foreach (GridViewRow row in grdItems.Rows)
                    //        {
                    //            if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                    //            {
                    //                ((Label)row.FindControl("lblitri_app_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                    //            }
                    //        }
                    //    }
                    //}
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = _serList;
                    ucOutScan.PickSerial = _serList;
                    if (ddlType.SelectedItem.Value == "+")
                    {
                        ucInScan.ScanItemList = ScanItemList;
                        ucInScan.PickSerial = _serList;
                    }
                    else
                    {
                        ucOutScan.ScanItemList = ScanItemList;
                        ucOutScan.PickSerial = _serList;
                    }
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = emptyGridList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = emptyGridList;
                    ucOutScan.PickSerial = emptyGridList;
                    ucInScan.PickSerial = emptyGridList;
                }
                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = gvItems;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                //Cursor.Current = Cursors.Default;
                //CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getItemStatusDesc(string stis)
        {
            List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
            if (oStatuss.FindAll(x => x.Mis_cd == stis).Count > 0)
            {
                stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            }
            return stis;
        }
        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            if (ddlType.SelectedItem.Value == "+")
            {
                _direction = 1;
            }
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)//direction always (-) for change status
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();
            //write entry to TEMP_PICK_HDR
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
        private void ValidateTrue()
        {
            //divWarning.Visible = false;
            //lblWarning.Text = "";
            //divSuccess.Visible = false;
            //lblSuccess.Text = "";
            //divAlert.Visible = false;
            //lblAlert.Text = "";
        }
        private void PageClear()
        {
            _mainModuleName = "StockADJ";
            //lbtnTempSave.Enabled = false;
            _reptPickSerialsSub = new List<ReptPickSerialsSub>();
            Session["documntNo"] = "";
            txtDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            lbtnDate.Enabled = true;
            lbtnDate.Visible = true;
            CalendarExtender3.Enabled = true;
            ddlType.Enabled = true;
            ddlType.SelectedIndex = 0;
            txtSubType.Text = string.Empty;
            txtRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            Session["123"] = "";
            txtUserSeqNo.ReadOnly = true;
            txtUserSeqNo.Text = string.Empty;

            ddlTypeSearch.SelectedIndex = 0;
            txtDocumentNo.Text = string.Empty;

            grdItems.DataSource = new int[] { };
            grdItems.DataBind();
            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();

            txtFromDateSearch.Text = DateTime.Now.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
            txtToDateSearch.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
   
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PageClear();
            ucOutScan.doc_tp = "ADJ";
            
            ucOutScan.ListClear();
            ucOutScan.ScanItemList = null;
            ucOutScan.isApprovalSend = false;

            ucInScan.doc_tp = "ADJ";
            ucInScan.PageClear();
            ucInScan.ListClear();
            ucOutScan.PageClear();
            ucOutScan.ListClear();
            ucOutScan.ScanItemList = null;
           // ucOutScan.DDLStatus.SelectedValue = "CONS";
           // ucOutScan.DDLStatus.Items.Remove(ucOutScan.DDLStatus.Items[0]);
            
                        
            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "ConfirmSave();";
            lbtnSave.CssClass = "buttonUndocolor";
            lbtnTempSave.Enabled = true;
            lbtnTempSave.OnClientClick = "ConfirmTempSave();";
            lbtnTempSave.CssClass = "buttonUndocolor";
            //lbtnTempSave.Enabled = false;
            //lbtnTempSave.OnClientClick = "return Enable();";
            //lbtnTempSave.CssClass = "buttoncolor";

            Session["DocType"] = "0";
            Session["Doc"] = "0";
            Session["TempDoc"] = "0";
            Session["Adv"] = "0";
            Session["GlbModuleName"] = "m_Trans_Inventory_PurchaseReturnNote";
            ViewState["userSeqNo"] = null;
            ViewState["SerialList"] = null;
            ucInScan.userSeqNo = null;
            ucOutScan.userSeqNo = null;

            ucInScan.Visible = false;
            ucOutScan.Visible = false;

            bool _allowCurrentTrans = false;
            //IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_InternalAssetItemIssue", txtDate, lblBackDateInfor, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans);
            if (string.IsNullOrEmpty(lblBackDateInfor.Text))
            {
                //lbtnDate.Enabled = false;
                //lbtnDate.Visible = false;
                CalendarExtender3.Enabled = false;
            }
            else
            {
                //lbtnDate.Enabled = true;
                //lbtnDate.Visible = true;
                CalendarExtender3.Enabled = true;
            }
        }

        protected void Disable_Click(object sender, System.EventArgs e)
        {
            lbtnTempSave.Enabled = false;
            //Label1.Text = "LinkButton Now Disable";
            DisplayMessage("Temporary save is disabled!!", 2);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["direction"].ToString() == "IN")
            {
                try
                {
                    Session["GlbReportType"] = "SCM1_ADJIN";
                    BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    BaseCls.GlbReportHeading = "INWARD DOC";

                    if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_ADJ_ABE.rpt";
                    }
                    else
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full.rpt";
                        Session["GlbReportName"] = "Inward_Docs_Full.rpt";
                    }

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        PrintPDF(targetFileName, obj._indocfull_ABE_ADJ);
                    }
                    else
                    {
                        PrintPDF(targetFileName, obj._indocfull);
                    }

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
                catch(Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock Adjustment IN Print", "StockAdjustment", ex.Message, Session["UserID"].ToString());
                }
            }
            else
            {
                try
                {             
                    Session["GlbReportType"] = "SCM1_ADJOT";
                    BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    BaseCls.GlbReportHeading = "INWARD DOC";

                    if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        Session["GlbReportName"] = "Outward_Docs_Full_ABE.rpt";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "AOA")
                    {
                        Session["GlbReportName"] = "Outward_Docs_Full_AOA_OUTP.rpt";
                    }
                    else
                    {
                        Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                        Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                    }
                 
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());

                    if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        PrintPDF(targetFileName, obj._outdocfull_AEB);
                    }
                    else if (Session["UserCompanyCode"].ToString() == "AOA")
                    {
                        PrintPDF(targetFileName, obj._outdocfull_AOA);
                    }
                    else
                    {                       
                        PrintPDF(targetFileName, obj._outdocfull);
                    }    
              
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    // Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
                catch(Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Stock Adjustment OUT Print", "StockAdjustment", ex.Message, Session["UserID"].ToString());
                }
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }


        protected void lbtngrdviewsubserial_Click(object sender, EventArgs e)
        {
             var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("ser_Item") as Label).Text;
                    string serial = (row.FindControl("ser_Serial1") as Label).Text;
                    string serialID = (row.FindControl("ser_SerialID") as Label).Text;
                    DataTable _result = new DataTable();
                    if (txtUserSeqNo.Text != "")
                    {
                         _result = CHNLSVC.Inventory.GetSubSerials(_itemCode, Convert.ToInt32(txtUserSeqNo.Text), serial);
                    }
                    if (_result.Rows.Count > 0)
                    {
                        SubItemList(_result);
                        //  GgdsubItem.DataSource = _result;
                        // GgdsubItem.DataBind();
                        // ViewState["SubItem"] = _result;
                        getRowValue(1);
                        int count = 1;

                        foreach (DataRow row1 in _result.Rows)
                        {
                            // your index is in i
                            count = _result.Rows.IndexOf(row1);
                        }
                        Session["subItemRowCount"] = count.ToString();
                        Session["subItemCurrentRowCount"] = "1";
                    }
                    else
                    {

                        List<InventoryWarrantySubDetail> _result1 = CHNLSVC.Inventory.GetSubItemSerials(null, null, Convert.ToInt32(serialID));
                        if (_result1!=null)
                        {
                            grdviewsubserials.DataSource = _result1;
                            grdviewsubserials.DataBind();
                            userSubSerialview.Show();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub item not found');", true);
                        }
                    }
                }
        }

        private void SubItemList(DataTable _tbl)
        {
            tstSubSerial.Focus();
            if (_tbl != null)
            {
                DataRow dr = null;
                DataTable _SubItemTbl = new DataTable();
                _SubItemTbl.Columns.Add(new DataColumn("ID", typeof(int)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_m_ser", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_itm_cd", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_tp", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("mis_desc", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_itm_stus", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_sub_ser", typeof(string)));
                int row = 0;
                foreach (DataRow tblr in _tbl.Rows)
                {
                    dr = _SubItemTbl.NewRow();
                    dr["ID"] = row;
                    dr["tpss_m_ser"] = tblr["tpss_m_ser"].ToString();
                    dr["tpss_itm_cd"] = tblr["tpss_itm_cd"].ToString();
                    dr["tpss_tp"] = tblr["tpss_tp"].ToString();
                    dr["mis_desc"] = tblr["mis_desc"].ToString();
                    dr["tpss_itm_stus"] = tblr["tpss_itm_stus"].ToString();
                    dr["tpss_sub_ser"] = tblr["tpss_sub_ser"].ToString();
                    _SubItemTbl.Rows.Add(dr);
                    row++;

                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), tblr["tpss_itm_cd"].ToString());
                    ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
                    #region Fill Pick Sub Serial Object
                    _ReptPickSerialsSub.Tpss_itm_brand = msitem.Mi_brand;
                    _ReptPickSerialsSub.Tpss_itm_cd = tblr["tpss_itm_cd"].ToString();
                    _ReptPickSerialsSub.Tpss_itm_desc = msitem.Mi_longdesc;
                    _ReptPickSerialsSub.Tpss_itm_model = msitem.Mi_model;
                    _ReptPickSerialsSub.Tpss_itm_stus = tblr["tpss_itm_stus"].ToString();
                    _ReptPickSerialsSub.Tpss_m_itm_cd = tblr["tpss_m_itm_cd"].ToString();
                    _ReptPickSerialsSub.Tpss_m_ser = tblr["tpss_m_ser"].ToString();
                    _ReptPickSerialsSub.Tpss_mfc = "";
                    _ReptPickSerialsSub.Tpss_ser_id =0;


                    _ReptPickSerialsSub.Tpss_sub_ser = tblr["tpss_sub_ser"].ToString();
                   
               

                    //_ReptPickSerialsSub.Tpss_tp = row["micp_itm_tp"].ToString();
                    _ReptPickSerialsSub.Tpss_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);//generated_seq;
                    _reptPickSerialsSub.Add(_ReptPickSerialsSub);
                    #endregion

                }

                GgdsubItem.DataSource = _SubItemTbl;
                GgdsubItem.DataBind();
                tstSubSerial.Focus();
                userSubSerial.Show();
                // tstSubSerial.Focus();
                ViewState["SubItem"] = _SubItemTbl;

            }
        }
        protected void lbtnupdate_Click(object sender, EventArgs e)
        {
            if (GgdsubItem.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string ItemCode = (row.FindControl("tpss_itm_cd") as Label).Text;
                string Status = (row.FindControl("mis_desc") as Label).Text;
                int ID = Convert.ToInt32((row.FindControl("ID") as Label).Text);
                Session["subItemCurrentRowCount"] = ID - 1;
                txtSubproduct.Text = ItemCode;
                ddlSIStatus.SelectedItem.Text = Status;
                userSubSerial.Show();
                tstSubSerial.Focus();
            }
        }
        protected void tstSubSerial_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(tstSubSerial.Text))
            {
                userSubSerial.Show();
                return;
            }
            List<ReptPickSerialsSub> _listSubSer = CHNLSVC.Inventory.GET_TEMP_PICK_SER_SUB(new ReptPickSerialsSub()
            {
                Tpss_sub_ser = tstSubSerial.Text
            });
            if (_listSubSer != null)
            {
                if (_listSubSer.Count > 0)
                {
                    tstSubSerial.Text = "";
                    tstSubSerial.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                    userSubSerial.Show();
                    return;
                }
            }

            List<InventorySubSerialMaster> _subSerList = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster()
            {
                Irsms_sub_ser = tstSubSerial.Text
            });

            if (_subSerList != null)
            {
                if (_subSerList.Count > 0)
                {
                    tstSubSerial.Text = "";
                    tstSubSerial.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                    userSubSerial.Show();
                    return;
                }
            }

            DataTable _tbl = ViewState["SubItem"] as DataTable;
            int RowCount = Convert.ToInt32(Session["subItemRowCount"].ToString());
            cRow = Convert.ToInt32(Session["subItemCurrentRowCount"].ToString());

            tstSubSerial.Focus();
            foreach (DataRow row in _tbl.Rows)
            {
                if (row["tpss_sub_ser"].ToString() == tstSubSerial.Text.Trim())
                {
                    tstSubSerial.Text = "";
                    tstSubSerial.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                    userSubSerial.Show();
                    return;
                }
                if (row["tpss_itm_cd"].ToString() == txtSubproduct.Text)
                {
                    row.SetField("tpss_itm_stus", ddlSIStatus.SelectedItem.Text);
                    row.SetField("tpss_sub_ser", tstSubSerial.Text);
                    row.AcceptChanges();
                    UpdateSubSerial(row["tpss_itm_cd"].ToString());
                    GgdsubItem.DataSource = _tbl;
                    GgdsubItem.DataBind();
                    userSubSerial.Show();
                    ViewState["SubItem"] = _tbl;
                    cRow++;
                    getRowValue(cRow);
                    Session["subItemCurrentRowCount"] = cRow.ToString();
                    cRow = cRow - 1;
                    if (RowCount == cRow)
                    {
                        userSubSerial.Hide();
                        //lbtnTempSave.Focus();
                        //if (Session["_itemSerializedStatus"].ToString() == "2")
                        //{
                        //    txtSerial2.Focus();
                        //}

                    }
                    tstSubSerial.Text = "";

                    return;
                }
            }

        }
        private void getRowValue(int _row)
        {
            DataTable _tbl = ViewState["SubItem"] as DataTable;
            var chosenRow = (from row in _tbl.AsEnumerable()
                             where row.Field<int>("ID") == _row
                             select row);

            if (chosenRow != null)
            {
                foreach (DataRow _one in chosenRow)
                {
                    txtSubproduct.Text = _one["tpss_itm_cd"].ToString();
                    ddlSIStatus.SelectedItem.Text = _one["mis_desc"].ToString();
                }
            }


        }
        private void UpdateSubSerial(string _mitem)
        {
            //DataTable _tbl = ViewState["SubItem"] as DataTable;
            //foreach (DataRow dr in _tbl.Rows)
            //{
            //ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
            #region Fill Pick Sub Serial Object
            //_ReptPickSerialsSub.Tpss_itm_stus = ddlSIStatus.SelectedValue;
            //_ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
            //_ReptPickSerialsSub.Tpss_m_ser = Session["MainItemSerial"].ToString();
            //_ReptPickSerialsSub.Tpss_itm_cd = txtSubproduct.Text;
            //_ReptPickSerialsSub.Tpss_sub_ser = tstSubSerial.Text;
            //// _ReptPickSerialsSub.Tpss_usrseq_no = Convert.ToInt32(Session["userSeqNo"].ToString());
            //_reptPickSerialsSub.Add(_ReptPickSerialsSub);

            var _fiterserial = _reptPickSerialsSub.SingleOrDefault(x => x.Tpss_itm_cd == _mitem);
            if (_fiterserial != null)
            {
                _fiterserial.Tpss_itm_cd = txtSubproduct.Text;
                _fiterserial.Tpss_sub_ser = tstSubSerial.Text;
            }

            Int32 Result = CHNLSVC.Inventory.UpdateAllScanSubSerials(_reptPickSerialsSub);
            if (Result == 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error');", true);
            }
            #endregion
            //  }



        }

        protected void lbtnserialprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["documntNo"] = txtDocumentNo.Text;
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
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Stock Adjusment Serial Print", "StockAdjustment", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            popupExcel.Hide();
        }
        protected void lbtnUploadExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblExcelUploadError.Visible = false;
                lblExcelUploadError.Text = "";
                if (fileUploadExcel.HasFile)
                {
                    string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileUploadExcel.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX" || Extension == ".xlsm" || Extension == ".XLSM")
                    {

                    }
                    else
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Please select a valid excel (.xls or .xlsx or .xlsm) file";
                    }

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                    fileUploadExcel.SaveAs(ValidateFilePath);
                    _filPath = ValidateFilePath;
                    UploadData();
                    _showExcelPop = false;
                    popupExcel.Hide();
                    //  DispMsg("Excel file upload completed. Do you want to process ? ");
                }
                else
                {
                    DispMsg("Please select the correct upload file path !");
                }
                if (lblExcelUploadError.Visible == true)
                {
                    _showExcelPop = true;
                    popupExcel.Show();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }

        }
        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelProcess_Click(object sender, EventArgs e)
        {
            popOpExcSave.Hide();
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
        public DataTable[] ReadExcelData(string FileName, out string _error)
        {
            _error = "";
            #region Excel Process
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                try
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
                }
                catch (Exception ex)
                {
                    _error = ex.Message;
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
            #endregion
        }
        private void UploadData()
        {
            try
            {
                _StcAdjExcelItem = new List<StcAdjExcelItem>();
                string _error = "";
                #region Excel hdr data read
                if (string.IsNullOrEmpty(_filPath))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable[] GetExecelTbl = ReadExcelData(_filPath, out _error);
                if (!string.IsNullOrEmpty(_error))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = _error;
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DateTime _tmpDt = new DateTime();
                if (GetExecelTbl == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }

                #endregion
                DataTable _dtExData = GetExecelTbl[0];
                #region MyRegion
                if (_dtExData == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                if (_dtExData.Rows.Count < 2)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                #endregion
                StcAdjExcelItem _blItm = new StcAdjExcelItem();
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    #region column null check
                    if (string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString())
                        )
                    {
                        continue;
                    }
                    #endregion
                    #region itm
                    _blItm = new StcAdjExcelItem();
                    _blItm.item_code = string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString()) ? "" : _dtExData.Rows[i][0].ToString().Trim().ToUpper();
                    _blItm.status = string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString()) ? "" : _dtExData.Rows[i][1].ToString().Trim();

                    //_blItm.uom = string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString()) ? "" : _dtExData.Rows[i][1].ToString().Trim();
                    _blItm.qty = string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString()) ? "" : _dtExData.Rows[i][2].ToString().Trim().ToUpper();
                    #endregion
                    _StcAdjExcelItem.Add(_blItm);
                }
                popupExcel.Hide();
                //popOpExcSave.Show();
                ProcessExcelData();
            }
            catch (Exception ex)
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = ex.Message;
                _showExcelPop = true;
                popupExcel.Show();
                return;
            }

        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        public void ProcessData()
        {
            try
            {
                if (_StcAdjExcelItem.Count > 0)
                {
                    Int32 serialno = 0;
                    serialno = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());
                    List<ReptPickSerials> _SerialsLst = new List<ReptPickSerials>();
                    foreach(StcAdjExcelItem itm in  _StcAdjExcelItem ){
                        DataTable dtitemdata = CHNLSVC.Inventory.GetItemData(itm.item_code.ToUpper().Trim());

                        if (dtitemdata.Rows.Count == 0)
                        {
                            lblExcelUploadError.Visible = true;
                            lblExcelUploadError.Text = "Invalid item code (" + itm.item_code + ")";
                            _showExcelPop = true;
                            popupExcel.Show();
                            return;
                        }
                        if (itm.uomqty == 0)
                        {
                            lblExcelUploadError.Visible = true;
                            lblExcelUploadError.Text = "Invalid UOM item code (" + itm.item_code + ")";
                            _showExcelPop = true;
                            popupExcel.Show();
                            return;
                        }
                         List<InventoryBatchRefN> itmBalance = CHNLSVC.Inventory.getItemBalanceQty(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itm.item_code, itm.status);
                         if (itmBalance.Count > 0)
                         {
                             decimal stkQty = itmBalance.Sum(x => x.Inb_qty);
                             if (stkQty>0)
                             {
                                 Decimal adjQty = stkQty - (Convert.ToDecimal(itm.qty) * itm.uomqty);
                                 if (adjQty >= 0)
                                 {
                                     
                                     decimal selectQty=0;
                                     decimal itmQty = 0;
                                     foreach (InventoryBatchRefN bthItm in itmBalance)
                                     {
                                         if ( adjQty==0)
                                         {
                                             break;
                                         }
                                         else
                                         {
                                             if (bthItm.Inb_qty == adjQty || bthItm.Inb_qty < adjQty)
                                             {
                                                 itmQty = bthItm.Inb_qty;
                                                 selectQty+=itmQty;
                                                 adjQty = adjQty - itmQty;
                                             }
                                             else if (bthItm.Inb_qty > adjQty)
                                             {
                                                 itmQty = adjQty;
                                                 selectQty += itmQty;
                                                 adjQty = adjQty - itmQty;
                                             }
                                             else 
                                             {
                                                 itmQty = adjQty;
                                                 selectQty += adjQty;
                                             }
                                         }
                                         ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                                         _inputReptPickSerials.Tus_usrseq_no = Convert.ToInt32(serialno);
                                         _inputReptPickSerials.Tus_doc_no = serialno.ToString();
                                         _inputReptPickSerials.Tus_seq_no = Convert.ToInt32(serialno);

                                         _inputReptPickSerials.Tus_itm_line = 0;
                                         _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                                         _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                                         _inputReptPickSerials.Tus_bin = bthItm.Inb_bin;
                                         _inputReptPickSerials.Tus_itm_cd = itm.item_code.ToUpper().Trim();
                                         _inputReptPickSerials.Tus_itm_stus = itm.status;
                                         _inputReptPickSerials.Tus_qty = itmQty;

                                         _inputReptPickSerials.Tus_ser_id = 0; /*Convert.ToInt32(serialidforitem)*/
                                         _inputReptPickSerials.Tus_ser_remarks = "ADJ Excel Upload";
                                         _inputReptPickSerials.Tus_batch_line = 0;
                                         _inputReptPickSerials.Tus_ser_line = 0;
                                         _inputReptPickSerials.Tus_unit_cost = 0;
                                         _inputReptPickSerials.Tus_seq_no = 0;
                                         _inputReptPickSerials.Tus_doc_dt = DateTime.Now;
                                         _inputReptPickSerials.Tus_warr_no = null;
                                         _inputReptPickSerials.Tus_warr_period = 0;
                                         _inputReptPickSerials.Tus_orig_grncom = null;
                                         _inputReptPickSerials.Tus_orig_grnno = null;
                                         _inputReptPickSerials.Tus_orig_grndt = DateTime.MinValue;
                                         _inputReptPickSerials.Tus_orig_supp = null;
                                         _inputReptPickSerials.Tus_exist_grncom = null;
                                         _inputReptPickSerials.Tus_exist_grnno = null;
                                         _inputReptPickSerials.Tus_exist_grndt = DateTime.MinValue;
                                         _inputReptPickSerials.Tus_exist_supp = null;
                                         _inputReptPickSerials.Tus_ageloc = null;
                                         _inputReptPickSerials.Tus_ageloc_dt = DateTime.MinValue;

                                         _inputReptPickSerials.Tus_pkg_uom_qty = Convert.ToDecimal(itmQty)/itm.uomqty;
                                         _inputReptPickSerials.Tus_pkg_uom_tp = itm.uom;

                                         string itm_desc = dtitemdata.Rows[0]["MI_SHORTDESC"].ToString();
                                         string itm_model = dtitemdata.Rows[0]["MI_MODEL"].ToString();
                                         string itm_brand = dtitemdata.Rows[0]["MI_BRAND"].ToString();

                                         _inputReptPickSerials.Tus_itm_desc = itm_desc;
                                         _inputReptPickSerials.Tus_itm_model = itm_model;
                                         _inputReptPickSerials.Tus_itm_brand = itm_brand;
                                         _SerialsLst.Add(_inputReptPickSerials);
                                        //Int32 ret=CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);
                                        //if (ret <=0)
                                        //{
                                        //    lblExcelUploadError.Visible = true;
                                        //    lblExcelUploadError.Text = "Unable to save tempory item.";
                                        //    _showExcelPop = true;
                                        //    popupExcel.Show();
                                        //    return;
                                        //}
                                     }
                                 }
                                 else
                                 {
                                     lblExcelUploadError.Visible = true;
                                     lblExcelUploadError.Text = "Item stock can't existing stock in the system.(System stock Item Code :"+itm.item_code+" quatity :" + stkQty.ToString() + ").";
                                     _showExcelPop = true;
                                     popupExcel.Show();
                                     return;
                                 }
                             }
                         }
                         else
                         {
                             lblExcelUploadError.Visible = true;
                             lblExcelUploadError.Text = "Item stock balance is zero.(Item Code :" + itm.item_code + ")";
                             _showExcelPop = true;
                             popupExcel.Show();
                             return;
                         }
                    }
                    string error = string.Empty;
                    Int32 ret = CHNLSVC.Inventory.SaveAdjItemSerials(_SerialsLst,out error);
                    if (ret <= 0 || error!="")
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = (!string.IsNullOrEmpty(error)) ? error : "Unable to save tempory item.";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }
                    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                    List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                    string documntNo = serialno.ToString() ;
                    Int32 result = -99;
                    Int32 _userSeqNo = serialno;
                    int _direction = 0;

                    
                    reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "UPLD");

                    //reptPickSubSerialsList = null;
                    //if (reptPickSerialsList == null)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found');", true);
                    //    // MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    foreach (ReptPickSerials item in reptPickSerialsList)
                    {
                        item.Tus_orig_grnno = item.Tus_exist_grnno;
                        item.Tus_orig_supp = item.Tus_exist_supp;
                    }

                    #region Check Referance Date and the Doc Date
                    if (_direction == 0)
                    {
                        if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                        {
                            return;
                        }
                    }
                    #endregion
                    #region Check Duplicate Serials
                    if (reptPickSerialsList != null)
                    {
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
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials. ');", true);
                            // MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    #endregion
                    InventoryHeader inHeader = new InventoryHeader();
                    #region Fill InventoryHeader
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    foreach (DataRow r in dt_location.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                        if (System.DBNull.Value != r["ML_CATE_2"])
                        {
                            inHeader.Ith_channel = (string)r["ML_CATE_2"];
                        }
                        else
                        {
                            inHeader.Ith_channel = string.Empty;
                        }
                    }
                    inHeader.Ith_acc_no = "STOCK_ADJ";
                    inHeader.Ith_anal_1 = "";

                    inHeader.Ith_anal_3 = "";
                    inHeader.Ith_anal_4 = "";
                    inHeader.Ith_anal_5 = "";
                    inHeader.Ith_anal_6 = 0;
                    inHeader.Ith_anal_7 = 0;
                    inHeader.Ith_anal_8 = DateTime.MinValue;
                    inHeader.Ith_anal_9 = DateTime.MinValue;
                 
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_2 = "";


                    inHeader.Ith_anal_11 = false;
                    inHeader.Ith_anal_12 = false;
                    inHeader.Ith_bus_entity = "";
                    inHeader.Ith_cate_tp = "DEXU";
                    inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                    inHeader.Ith_com_docno = "";
                    inHeader.Ith_cre_by = Session["UserID"].ToString();
                    inHeader.Ith_cre_when = DateTime.Now;
                    inHeader.Ith_del_add1 = "";// txtDAdd1.Text.Trim();
                    inHeader.Ith_del_add2 = "";// txtDAdd2.Text.Trim();
                    inHeader.Ith_del_code = "";
                    inHeader.Ith_del_party = "";
                    inHeader.Ith_del_town = "";
                    inHeader.TMP_SAVE_PKG_DATA = true;
                    if (ddlType.SelectedItem.Value == "+")
                    {
                        inHeader.Ith_direct = true;
                        inHeader.Ith_doc_tp = "ADJ";
                    }
                    else
                    {
                        inHeader.Ith_direct = false;
                        inHeader.Ith_doc_tp = "ADJ";
                    }
                    inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                    inHeader.Ith_doc_no = string.Empty;
                    //inHeader.Ith_doc_tp = "ADJ";
                    inHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                    
                    inHeader.Ith_entry_no = "";
                    inHeader.Ith_entry_tp = "DEXU";
                    inHeader.Ith_git_close = true;
                    inHeader.Ith_git_close_date = DateTime.MinValue;
                    inHeader.Ith_git_close_doc = string.Empty;
                    inHeader.Ith_isprinted = false;
                    inHeader.Ith_is_manual = false;
                    inHeader.Ith_job_no = string.Empty;
                    inHeader.Ith_loading_point = string.Empty;
                    inHeader.Ith_loading_user = string.Empty;
                    inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                    inHeader.Ith_manual_ref = txtRef.Text.Trim();
                    inHeader.Ith_mod_by = Session["UserID"].ToString();
                    inHeader.Ith_mod_when = DateTime.Now;
                    inHeader.Ith_noofcopies = 0;
                    inHeader.Ith_oth_loc = string.Empty;
                    inHeader.Ith_oth_docno = txtOtherRef.Text.Trim();
                    inHeader.Ith_remarks = txtRemarks.Text;
                    //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                    inHeader.Ith_session_id = Session["SessionID"].ToString();
                    inHeader.Ith_stus = "A";
                    inHeader.Ith_sub_tp = "DEXU";
                    inHeader.Ith_vehi_no = string.Empty;
                    inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;
                    #endregion
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    #region Fill MasterAutoNumber
                    masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "ADJ";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "ADJ";
                    masterAuto.Aut_year = null;
                    #endregion

                    int _line = 0;
                    #region Update some serial items
                    if (reptPickSerialsList != null)
                    {
                        if (_direction == 1)
                        {
                            foreach (var _seritem in reptPickSerialsList)
                            {
                                _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                                _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                                _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                                _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                            }
                        }
                        else if (_direction == 0)
                        {
                            foreach (var _ADJSer in reptPickSerialsList)
                            {
                                _line = _line + 1;
                                _ADJSer.Tus_base_itm_line = _line;
                            }
                        }
                    }
                    #endregion

                    #region Inventory balance
                    MasterItem msitem = new MasterItem();
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    foreach (var itm in _scanItems)
                    {

                        ////
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itm.Peo.Tus_itm_cd, itm.Peo.Tus_itm_stus);
                        if (_inventoryLocation != null)
                        {
                            if (_inventoryLocation.Count == 1)
                            {
                                foreach (InventoryLocation _loc in _inventoryLocation)
                                {
                                    decimal _formQty = 0;
                                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                    if (msitem.Mi_is_ser1 == 1)
                                    {
                                        _formQty = Convert.ToDecimal(itm.theCount);
                                    }
                                    else
                                    {
                                        _formQty = Convert.ToDecimal(itm.Peo.Tus_qty);
                                    }

                                    if (ddlType.Text != "+")
                                    {
                                        if (_formQty > _loc.Inl_free_qty)
                                        {
                                            string msg = "Please check the inventory balance - Item code (" + itm.Peo.Tus_itm_cd + ")";
                                            DisplayMessage(msg, 2);
                                            return;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                string msg = "Please check the inventory balance - Item code (" + itm.Peo.Tus_itm_cd + ")";
                                DisplayMessage(msg, 2);
                                return;
                            }
                        }
                    }
                    #endregion


                    #region Save Adj+ / Adj-
                    inHeader.TMP_CHK_LOC_BAL = true;
                    inHeader.Ith_gen_frm = "SCMWEBEXUPLD";
                
                   result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, false);
                 
                    if (result != -99 && result >= 0)
                    {
                        string _msg = "Successfully Saved! Document No : " + documntNo;
                        Session["documntNo"] = documntNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                        PageClear();

                        
                        Session["direction"] = "OUT";

                        lblMssg.Text = "Do you want print now?";
                        PopupConfBox.Show();

                    }
                    else
                    {
                        string _msg = documntNo + " Process Terminated : ADJ";
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _msg + "');", true);
                        DisplayMessage(_msg, 4);
                        //MessageBox.Show(documntNo, "Process Terminated : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //PageClear();
                    }

                    #endregion

                }
                else
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Please select valid excel to upload.";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = ex.Message;
                _showExcelPop = true;
                popupExcel.Show();
                return;
            }
        }
        private decimal ToDecimal(string _qty)
        {
            decimal d = 0, td = 0;
            d = decimal.TryParse(_qty, out td) ? Convert.ToDecimal(_qty) : 0;
            return d;
        }
        private void ProcessExcelData()
        {
            try
            {
                List<TmpValidation> _errList = new List<TmpValidation>();
                TmpValidation _err = new TmpValidation();
                string _erMsg = "";
                MasterItem _mstItm = new MasterItem();
                foreach (var item in _StcAdjExcelItem)
                {
                    #region validation
                    _erMsg = "";
                    _err = new TmpValidation();
                    _err.Sad_itm_line = 1;
                    if (_errList.Count > 0)
                    {
                        _err.Sad_itm_line = _errList.Max(c => c.Sad_itm_line) + 1;
                    }
                    else
                    {
                        _err.Sad_itm_line = _err.Sad_itm_line + 1;
                    }
                    if (string.IsNullOrEmpty(item.item_code))
                    {
                        _erMsg = "Please enter item code";
                        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    }
                    if (!string.IsNullOrEmpty(item.item_code))
                    {
                        _mstItm = CHNLSVC.General.GetItemMaster(item.item_code);
                        if (_mstItm == null)
                        {
                            _erMsg = "Invalid item code ! ";
                            _err.Sad_itm_cd = item.item_code;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                        if (_mstItm != null)
                        {
                            if (_mstItm.Mi_is_ser1 == 1)
                            {
                                _erMsg = "Invalid item code(Serialize item cannot upload using Excel upload process.)";
                                _err.Sad_itm_cd = item.item_code;
                                _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(item.qty))
                    {
                        _erMsg = "Please enter quentity ";
                        _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.qty;
                        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    }
                    if (!string.IsNullOrEmpty(item.qty) )
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(item.qty, out n);
                        if (isNumeric)
                        {
                            if (Convert.ToDecimal(item.qty) < 0)
                            {
                                _erMsg = "Please enter valid quentity ";
                                _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.qty;
                                _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                            }
                        }
                        else
                        {
                            _erMsg = "Please enter valid number for quentity ";
                            _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.qty;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                    }
                   
                    //if (string.IsNullOrEmpty(item.uom))
                    //{
                    //    _erMsg = "Please enter UOM ";
                    //    _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.uom;
                    //    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    //}
                    //if (!string.IsNullOrEmpty(item.uom))
                    //{
                    //    DataTable uom = CHNLSVC.General.GetMstItmUOM(item.uom);
                    //    if (uom.Rows.Count > 0)
                    //    {
                    //        if (uom.Rows[0]["MSU_CD"] == DBNull.Value)
                    //        {
                    //            _erMsg = "Please enter valid UOM code. ";
                    //            _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.uom;
                    //            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        _erMsg = "Please enter valid UOM code. ";
                    //        _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.uom;
                    //        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    //    }
                    //}
                    if (string.IsNullOrEmpty(item.status))
                    {
                        _erMsg = "Please enter item status";
                        _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.status;
                        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    }
                   
                    if (!string.IsNullOrEmpty(item.status))
                    {
                        DataTable stus = CHNLSVC.Inventory.GetItemStatusMaster(item.status, null);
                        if (stus.Rows.Count > 0)
                        {
                            if (stus.Rows[0]["MIS_CD"] == DBNull.Value)
                            {
                                _erMsg = "Please enter valid Status code. ";
                                _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd)) ? _err.Sad_itm_cd : item.status;
                                _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                            }
                        }
                        else
                        {
                            _erMsg = "Please enter valid Status code. ";
                            _err.Sad_itm_cd = (!string.IsNullOrEmpty(_err.Sad_itm_cd))?_err.Sad_itm_cd:item.status;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                    }
                    if (_mstItm != null)
                    {
                        UnitConvert _obj = new UnitConvert();
                        _obj.mmu_com = Session["UserCompanyCode"].ToString();
                        _obj.mmu_model = _mstItm.Mi_cd;
                         List<UnitConvert> uomdt=CHNLSVC.Inventory.GET_UNIT_CONVERTER_DATA(_obj);
                         if (uomdt.Count > 0)
                         {
                             List<UnitConvert> has=uomdt.Where(x=>x.mmu_model_uom==item.uom).ToList();
                            if(has.Count>0){
                                item.uom = has[0].mmu_model_uom;
                                item.uomqty = has[0].mmu_qty;
                            }
                            else
                            {
                                item.uom = _mstItm.Mi_itm_uom;
                                item.uomqty = 1;
                            }
                         }
                         else
                         {
                             item.uom = _mstItm.Mi_itm_uom;
                             item.uomqty = 1;
                         }
                    }

                    if (!string.IsNullOrEmpty(_err.errorMsg))
                    {
                        _errList.Add(_err);
                    }
                    #endregion
                }
                if (_errList.Count > 0)
                {
                    dgvError.DataSource = _errList;
                    dgvError.DataBind();
                    _showErrPop = true;
                    popupErro.Show();
                }
                else
                {
                    popOpExcSave.Show();
                    //Int32 _c = 0;
                    //foreach (var item in _blItmsUpItmList)
                    //{
                    //    _c++;
                    //    if (_c == 200)
                    //    {
                    //        int x = 0;
                    //    }
                    //    txtItem.Text = item.Ibi_itm_cd;
                    //    txtItem_TextChanged(null, null);
                    //    txtQty.Text = item.Ibi_qty.ToString();
                    //    txtQty_TextChanged(null, null);
                    //    ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByValue(item.Ibi_tp));
                    //    txtUnitPrice.Text = item.Ibi_unit_rt.ToString();
                    //    txtUnitPrice_TextChanged(null, null);
                    //    ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(item.Ibi_tag));
                    //    _bindGrid = true;
                    //    btnAddNewtems_Click(null, null);
                    //    _bindGrid = false;
                    //}
                    //BindPInvoiceItems();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        protected void btnExcelDataUpload_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16054))
            {
                if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                _showExcelPop = true;
                popupExcel.Show();
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to save/temp save! - ( Advice: Required permission code : 16054)", 2);
                return;
            }
         
        }
        public void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
           
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16054))
            {
                popupExcel.Show();
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to save/temp save! - ( Advice: Required permission code : 16054)", 2);
                return;
            }
        }
    }
}