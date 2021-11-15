using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class InternalAssetItemIssue : Base 
    {
        private List<string> SeqNumList = null;
        private List<InventoryRequestItem> ScanItemList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageClear();
                    ucOutScan.PNLTobechange.Visible = false;
                }
                else
                {
                    if (Session["Date"] == "true")
                    {
                        UserDPopoup.Show();
                        Session["Date"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnIssueNo_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
               
                txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString(); 
                txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "IS";
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtIssueNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetDocData(txtIssueNo.Text);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnRequestor_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "6";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
              
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtRequestor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestor.Text == Session["UserDefLoca"].ToString())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('You cannot enter same location which you have already logged !!!');", true);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, "Code", "%" + txtRequestor.Text.ToString());
                if (_result.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter valid Requestor');", true);
                }
                else
                {
                    txtRequestor.ToolTip = _result.Rows[0][1].ToString();
                }

                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnRequestNo_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(txtRequestor.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select Requestor');", true);
                    return;
                }
                ViewState["SEARCH"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PRNRequest);
                DataTable result = CHNLSVC.CommonSearch.SearchPRNREQNo(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "409";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
              
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtRequestNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequestor.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select Requestor..');", true);
                    txtRequestNo.Text = "";
                    return;
                }
                DataTable _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), txtRequestor.Text, "%" + txtRequestNo.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataTable RemoveDup = new DataTable();
                    RemoveDup = _result;
                    RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_ref");

                    grdPendingOrders.DataSource = RemoveDup;
                    grdPendingOrders.DataBind();
                    ViewState["Request"] = _result;
                }
                else
                {
                    grdPendingOrders.DataSource = new int[] { };
                    grdPendingOrders.DataBind();
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

                if (grdItems.Rows.Count == 0) return;
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("col_itri_itm_cd") as Label).Text;
                    string _ReqNo = (row.FindControl("col_ITR_REQ_NO") as Label).Text;
                   //string line = (row.FindControl("col_itri_line_no") as Label).Text;
                   // if (line != "")
                    //{
                    //    int _lineNo = Convert.ToInt32(line);
                   // }
                    int _lineNo = Convert.ToInt32((row.FindControl("col_itri_line_no") as Label).Text);

                    ucOutScan.TXTItemCode.Text = _itemCode;

                    ucOutScan.txtItemCode_TextChanged(null,null);
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
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    Process();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
    

        protected void lbtnSearchall_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showStickyWarningToast", "closeDialog();", true);
                if (Convert.ToDateTime(txtFromDate.Text).Date > Convert.ToDateTime(txtToDate.Text).Date)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select a valid date range');", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtRequestor.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select Requestor..');", true);
                    txtRequestNo.Text = "";
                    return;
                }
                DataTable _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), txtRequestor.Text, "%" + txtRequestNo.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataTable RemoveDup = new DataTable();
                    RemoveDup = _result;
                    RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_ref");

                    grdPendingOrders.DataSource = RemoveDup;
                    grdPendingOrders.DataBind();
                    ViewState["Request"] = _result;
                }
                else
                {
                    string _Msg = "There is no requests available for requestor"+" "+txtRequestor.Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('"+_Msg+"');", true);
                    txtRequestNo.Text = "";
                    grdPendingOrders.DataSource = new int[] { };
                    grdPendingOrders.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        #region Modalpopup

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //DocSubType
                if (lblvalue.Text == "6")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "6";
                    ViewState["SEARCH"] = _result;
                    UserPopup.Show();
                    return;
                }
                //RequestNo
                if (lblvalue.Text == "409")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PRNRequest);
                    DataTable result = CHNLSVC.CommonSearch.SearchPRNREQNo(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "409";
                    ViewState["SEARCH"] = result;
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
               
                string Name = grdResult.SelectedRow.Cells[1].Text;
                //UserProfitCenter
                if (lblvalue.Text == "6")
                {
                    if (Name == Session["UserDefLoca"].ToString())
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot enter same location which you have already logged !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        return;
                    }
                    string des = grdResult.SelectedRow.Cells[2].Text;
                    txtRequestor.Text = Name;
                    txtRequestor.ToolTip = des;
                    
                    lblvalue.Text = "";
                    UserPopup.Hide();

                    ViewState["SEARCH"] = null;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    return;
                }
                //RequestNo
                if (lblvalue.Text == "409")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true); txtRequestNo.Text = Name;
                    lblvalue.Text = "";
                    UserPopup.Hide();
                    ViewState["SEARCH"] = null;
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            //DocSubType
            if (lblvalue.Text == "6")
            {                           
                ViewState["SEARCH"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "6";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();


                return;
            }
            //RequestNo
            if (lblvalue.Text == "409")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PRNRequest);
                DataTable result = CHNLSVC.CommonSearch.SearchPRNREQNo(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "409";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                return;
            }   
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            UserPopup.Hide();
        }
        #endregion

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PRNRequest:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtRequestor.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "0" + seperator);

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
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        private void PageClear()
        {
            LoadDeliveroption();   
            ucOutScan.PageClear();
            ucOutScan.doc_tp = "IS";
            ucOutScan.isApprovalSend = false;
            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.userSeqNo = txtUserSeqNo.Text;
            Session["ISSerials"] = "";
            Session["ISITEM"] = "";
            GetSeqNo();
            txtDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtRequestor.Text = string.Empty;
            txtRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            txtIssueNo.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;
            txtFromDate.Text = DateTime.Now.AddMonths(-1).Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtRequestNo.Text = string.Empty;

            grdPendingOrders.DataSource = new int[] { };
            grdPendingOrders.DataBind();
            grdItems.DataSource = new int[] { };
            grdItems.DataBind();
            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();             
        }

        protected void selectchk_CheckedChanged(object sender, EventArgs e)
        {
            DataTable _FilterTbl = ViewState["Request"] as DataTable;
            if (grdPendingOrders.Rows.Count == 0) return;

            var lb = (CheckBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("selectchk") as CheckBox);
                Label _Reqno = (row.Cells[0].FindControl("col_itr_req_no") as Label);
                Session["_Reqno"] = _Reqno.Text;
                if (chkRow.Checked)
                {
                    calItemQty(_Reqno.Text, _FilterTbl, 0, false, false, chkRow);
                   
                }
                else
                {

                    DataTable _Item = new DataTable();
                    _Item = Session["ISITEM"] as DataTable;

                    DataRow[] rows;
                    rows = _Item.Select("ITR_REQ_NO = '" + _Reqno.Text + "'");
                    foreach (DataRow r in rows)
                    {
                        r.Delete();
                    }
                    grdItems.DataSource = _Item;
                    grdItems.DataBind();
                }
            }

            //foreach (GridViewRow row in grdPendingOrders.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[0].FindControl("selectchk") as CheckBox);
            //        if (chkRow.Checked)
            //        {
            //            Label _Reqno = (row.Cells[0].FindControl("col_itr_req_no") as Label);

            //            calItemQty(_Reqno.Text, _FilterTbl);
                        
                       
            //        }
            //        else
            //        {
            //            grdItems.DataSource = new int[] { };
            //            grdItems.DataBind();
            //        }
            //    }
            //}
        }

        private void LoadItem(DataTable _tbl, string _ReqNo, int user_seq_num, bool _isTemp, bool _delete)
        {

            DataTable _Item = new DataTable();
            _Item = Session["ISITEM"] as DataTable;
            if (_delete == true)
            {
                DataView dv = new DataView(_Item);
                dv.RowFilter = "itr_req_no <> '" + _ReqNo + "'";
                _tbl.Merge(dv.ToTable());
                if (_tbl.Rows.Count > 0)
                {
                    grdItems.DataSource = _tbl;
                    grdItems.DataBind();
                    Session["ISITEM"] = _tbl;
                    return;
                }
                else
                {
                    string _Msg = "purchase order required in order to process";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                }
            }
            if (_Item != null)
            {
                _Item.Merge(_tbl);
                if (_Item.Rows.Count > 0)
                {
                    grdItems.DataSource = _Item;
                    grdItems.DataBind();
                    Session["ISITEM"] = _Item;
                }
                else
                {
                    string _Msg = "purchase order required in order to process";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                }
            }
            else
            {
                if (_tbl.Rows.Count > 0)
                {
                    grdItems.DataSource = _tbl;
                    grdItems.DataBind();
                    Session["ISITEM"] = _tbl;
                }
                else
                {
                    string _Msg = "purchase order required in order to process";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                }
            }
          
            ucOutScan.ScanDocument = _ReqNo;
            Session["RequestNo"] = _ReqNo;
            if (_isTemp == true)
            {
                DataTable _ItemTest = new DataTable();
                _ItemTest = Session["ISITEM"] as DataTable;
                List<ReptPickSerials> _serList = new List<ReptPickSerials>(); ;
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty, x.Tus_base_itm_line,x.Tus_base_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (DataRow dr in _ItemTest.Rows)
                        {
                            if ((itm.Peo.Tus_itm_cd == dr["itri_itm_cd"].ToString()) && (itm.Peo.Tus_base_itm_line == Convert.ToInt32(dr["itri_line_no"].ToString())) && (itm.Peo.Tus_base_doc_no == dr["itr_req_no"].ToString()))
                            {
                                MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                if (msitem.Mi_is_ser1 == 1)
                                {
                                    dr["itri_mqty"] = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    

                                }
                                else
                                {
                                    dr["itri_mqty"] = itm.Peo.Tus_qty; // Current scan qty   

                                }
                            }
                        }
                    }

                    grdItems.DataSource = _ItemTest;
                    grdItems.DataBind();
                }
                else
                {
                    grdItems.DataSource = _ItemTest;
                    grdItems.DataBind();
                }
          
            }
               
        }
        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                if (grdSerial.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string ItemCode = (row.FindControl("tus_itm_cd") as Label).Text;
                    if (string.IsNullOrEmpty(ItemCode)) return;

                    string _item = (row.FindControl("TUS_ITM_CD") as Label).Text; ;
                    string _status = (row.FindControl("TUS_ITM_STUS") as Label).Text; ;
                    Int32 _serialID = Convert.ToInt32((row.FindControl("TUS_SER_ID") as Label).Text);
                    string _bin = (row.FindControl("TUS_BIN") as Label).Text;
                    string serial_1 = (row.FindControl("TUS_SER_1") as Label).Text;
                    string qty = (row.FindControl("Tus_qty") as Label).Text;
                    string ReqDoc = (row.FindControl("tus_base_doc_no") as Label).Text;
                    string ReqItemLine = (row.FindControl("tus_base_itm_line") as Label).Text;
                    string Qty = (row.FindControl("Tus_qty") as Label).Text;

                    Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("IS", Session["UserCompanyCode"].ToString(), txtUserSeqNo.Text, 0);
                    DataTable _ItemTest = new DataTable();
                    _ItemTest = Session["ISITEM"] as DataTable;
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>(); 
                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                    DataTable _Item = new DataTable();
                    _Item = Session["ISITEM"] as DataTable;
                    bool ISdelete = false;
                    if (_Item != null)
                    {
                        foreach (DataRow drow in _Item.Rows)
                        {
                            if ((drow["ITR_REQ_NO"].ToString() == ReqDoc) && (drow["itri_line_no"].ToString() == ReqItemLine) && (drow["itri_itm_cd"].ToString() == _item))
                            {
                                decimal ItemQty = Convert.ToDecimal(drow["itri_mqty"].ToString());
                                decimal newPik = ItemQty - Convert.ToDecimal(Qty);
                                drow["itri_mqty"] = newPik.ToString();
                            }
                           
                        }
                        for (int i = _Item.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = _Item.Rows[i];
                            if ((dr["ITR_REQ_NO"].ToString() == "N/A") && (dr["itri_itm_cd"].ToString() == _item))
                            {
                                decimal ItemQty = Convert.ToDecimal(dr["itri_mqty"].ToString());
                                decimal newPik = ItemQty - Convert.ToDecimal(Qty);
                                dr["itri_mqty"] = newPik.ToString();
                                // drow.Delete();
                                if (newPik == 0)
                                {
                                    dr.Delete();
                                }
                            }
                              
                        }
                      _Item.AcceptChanges();
                        grdItems.DataSource = _Item;
                        grdItems.DataBind();
                        Session["ISITEM"] = _Item;

                    }


                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), _item, serial_1);
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                    }
                    if (UserSeqNo != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                                             
                        _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNo, "IS");
                        if (_serList != null)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty, x.Tus_base_itm_line, x.Tus_base_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {
                                foreach (DataRow dr in _ItemTest.Rows)
                                {
                                    if ((itm.Peo.Tus_itm_cd == dr["itri_itm_cd"].ToString()) && (itm.Peo.Tus_base_itm_line == Convert.ToInt32(dr["itri_line_no"].ToString())) && (itm.Peo.Tus_base_doc_no == dr["itr_req_no"].ToString()))
                                    {
                                        MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                        if (msitem.Mi_is_ser1 == 1)
                                        {
                                            dr["itri_mqty"] = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    

                                        }
                                        else
                                        {
                                            dr["itri_mqty"] = itm.Peo.Tus_qty; // Current scan qty   

                                        }
                                    }
                                    else
                                    {
                                        if (dr["itri_itm_cd"].ToString() == _item)
                                        {
                                            if (_masterItem.Mi_is_ser1 == 1 )
                                            {
                                                dr["itri_mqty"] = 0;
                                            }
                                            else if (_masterItem.Mi_is_ser1 == 0)
                                            {
                                                decimal pickqqty = Convert.ToDecimal(dr["itri_mqty"].ToString());
                                                dr["itri_mqty"] = (pickqqty - Convert.ToDecimal(qty)).ToString();
                                            }
                                           
                                        }
                                       // dr["itri_mqty"] = 0;
                                    }
                                }
                            }

                           

                            grdSerial.AutoGenerateColumns = false;
                            grdSerial.DataSource = _serList;
                            grdSerial.DataBind();
                            ViewState["SerialList"] = _serList;
                            ucOutScan.PickSerial = _serList;
                        }
                        else
                        {
                            //foreach (DataRow dr in _ItemTest.Rows)
                            //{
                                
                            //  dr["itri_mqty"] = 0;
                                
                            //}
                            //grdItems.DataSource = _ItemTest;
                            //grdItems.DataBind();
                            //Session["ISITEM"] = _ItemTest;
                            grdSerial.AutoGenerateColumns = false;
                            grdSerial.DataSource = _serList;
                            grdSerial.DataBind();
                            ViewState["SerialList"] = _serList;
                            ucOutScan.PickSerial = _serList;
                        }
                    }
                    
                   // LoadPOItems(txtPONo.Text);
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
         
        }

        private void Process()
        {
            try
            {
                if (txtVehicle.Text != "")
                {
                    bool _vehicle = validateVehicle(txtVehicle.Text);
                    if (_vehicle == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check vehicle number.');", true);
                        return;
                    }
                }
          
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtRequestor.Text))
                {
                    string _Msg = "Please select the Requestor";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                        return;
                }
                if( grdItems.Rows.Count==0){
                    string _Msg = "Please select the RequestNo";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                      return;
                }
                if (string.IsNullOrEmpty(txtRef.Text)) txtRef.Text = "N/A";

                bool _allowCurrentTrans = false;
                
              
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;

                _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "IS", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);
               
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "IS");
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "IS");
                if (reptPickSerialsList == null)
                {
                    string _Msg = "Please add serial for requested items";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+_Msg+"');", true);                   
                    return;
                }
                

                //if (txtSavelconformmessageValue.Value == "No")
                //{
                //    return;
                //}
              
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
                    string _Msg = "Following item serials are duplicating. Please remove the duplicated serials." + _duplicateItems;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+_Msg+"');", true);                   
                    return;
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
                inHeader.Ith_acc_no = "Assets Transfer";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = "IS";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";
               
                inHeader.Ith_direct = false;

                inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year =  Convert.ToDateTime(txtDate.Text).Year;
                inHeader.Ith_entry_no = "";
                inHeader.Ith_entry_tp = "IS";
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
                inHeader.Ith_oth_loc = txtRequestor.Text;
                inHeader.Ith_oth_docno = "N/A";
                inHeader.Ith_remarks = txtRemarks.Text;
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "IS";
                inHeader.Ith_vehi_no = txtVehicle.Text;
                 inHeader.Ith_anal_3 = ddlDeliver.SelectedItem.Text;//add rukshan 06/jan/2016
                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "IS";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "IS";
                masterAuto.Aut_year = null;
                #endregion

                #region Update some serial items
                if (_direction == 1)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                        // _seritem.Tus_exist_grndt = dtpDate.Value.Date;   //kapila commented on 3/7/2015 which this data will be added item wise
                        _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        //_seritem.Tus_orig_grndt = dtpDate.Value.Date;  //kapila commented on 3/7/2015 which this data will be added item wise
                    }
                }
                #endregion

                #region Save Adj-
              
                 result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);

                if (result != -99 && result >= 0)
                {

                    string _Msg = "Successfully Saved! Document No : " + documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('"+_Msg+"');", true);
                    PageClear();
                    //if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //    if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                    //    if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                    //    {
                    //        if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //        else _view.GlbReportName = "Outward_Docs.rpt";
                    //    }
                    //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                    //    {
                    //        if (_direction == 1) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //        else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //    }
                    //    else
                    //    {
                    //        if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //        else _view.GlbReportName = "Outward_Docs.rpt";
                    //    }
                    //    _view.GlbReportDoc = documntNo;
                    //    _view.Show();
                    //    _view = null;
                    //}
                    //StockAdjustment _JobEntry = new StockAdjustment();
                    //_JobEntry.MdiParent = this.MdiParent;
                    //_JobEntry.Location = this.Location;
                    //_JobEntry.GlbModuleName = this.GlbModuleName;
                    //_JobEntry.Show();
                    //this.Close();
                    //this.Dispose();
                    //GC.Collect();
                }
                else
                {
                    string _Msg =  documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);                   
                    
                    
                }

                #endregion
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);                   
                    
              
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
        private void GetDocData(string Doc)
        {

            try
            {
                if (!string.IsNullOrEmpty(Doc))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                     _direction = 0;

                    #region Clear Data
                     foreach (GridViewRow gvr in grdSerial.Rows)
                     {
                         LinkButton Delrow = gvr.FindControl("lbtngrdSerialDalete") as LinkButton;
                         Delrow.Enabled = false;
                         Delrow.OnClientClick = "return Enable();";                        
                     }

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();
                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    txtRemarks.Text = string.Empty;
                    txtRef.Text = "";
                    txtUserSeqNo.Text="";
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(Doc);
                    txtRequestor.Text = _invHdr.Ith_oth_loc;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result1 = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, "Code", "%" + txtRequestor.Text.ToString());
                   
                    txtRequestor.ToolTip = _result1.Rows[0][1].ToString();
                
                    txtRef.Text = _invHdr.Ith_manual_ref;
                    txtRemarks.Text = _invHdr.Ith_remarks;
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
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Document No!');", true);                   
                        txtIssueNo.Text="";
                        txtIssueNo.Focus();
                        return;
                    }
                    else
                    {
                        lbtnSave.Enabled = false;
                        lbtnSave.OnClientClick = "return Enable();";
                        lbtnSave.CssClass = "buttoncolor";
                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    List<InventoryBatchN> _serList2 = new List<InventoryBatchN>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(Doc);
                    _serList2 = CHNLSVC.Inventory.Get_Int_Batch(Doc);
                    DataTable _result = new DataTable();
                    if ((_serList != null) && (_serList.Count !=0))
                    {

                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_qty, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_base_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {

                            _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), null, itm.Peo.Tus_base_doc_no, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                            if (_result != null)
                            {
                                foreach (DataRow row in _result.Rows)
                                {
                                    if ((row["itri_itm_cd"].ToString() == itm.Peo.Tus_itm_cd) && (row["ITR_REQ_NO"].ToString() == itm.Peo.Tus_base_doc_no))
                                    {
                                        row["itri_mqty"] = itm.Peo.Tus_qty;
                                    }

                                }
                            }
                        }

                        foreach (ReptPickSerials _listbind in _serList)
                        {
                            DataRow dr = null;
                            dr = _result.NewRow();
                            dr["ITRI_ITM_CD"] = _listbind.Tus_itm_cd;
                            dr["MI_LONGDESC"] = _listbind.Tus_itm_desc;
                            dr["MI_MODEL"] = _listbind.Tus_itm_model;
                            // dr["MI_PART_NO"] = _listbind.;
                            dr["ITRI_MQTY"] = _listbind.Tus_qty;
                            dr["ITRI_QTY"] = 0.0;
                            dr["ITRI_APP_QTY"] = 0.0;
                            dr["ITRI_QTY"] = 0.0;
                            dr["ITRI_PO_QTY"] = 0.0;
                            dr["ITRI_BQTY"] = 0.0;
                            dr["ITR_REQ_NO"] = _listbind.Tus_base_doc_no;
                            _result.Rows.Add(dr);
                        }
                       
                        var resultd = _serList2
                                      .Where(ah => !_serList.Any(h => h.Tus_itm_cd == ah.Inb_itm_cd))
                                      .ToList();
                        if (resultd.Count > 0)
                        {
                            foreach (InventoryBatchN batvh in resultd)
                            {
                                
                                ReptPickSerials _newserial = new ReptPickSerials();
                                _newserial.Tus_itm_cd = batvh.Inb_itm_cd;
                                _newserial.Tus_base_doc_no = batvh.Inb_base_ref_no;
                                _newserial.Tus_qty = batvh.Inb_qty;
                                DataView dv = new DataView(_result);
                                dv.RowFilter = "ITRI_ITM_CD = '" + batvh.Inb_itm_cd + "'";
                                _newserial.Tus_itm_desc = dv.Table.Rows[0]["MI_LONGDESC"].ToString();

                                _newserial.Tus_itm_model = dv.Table.Rows[0]["MI_MODEL"].ToString();
                                _newserial.Tus_itm_brand = dv.Table.Rows[0]["MI_BRAND"].ToString();
                                DataTable _status = CHNLSVC.Sales.GetItemStatusTxt(batvh.Inb_itm_stus);
                                _newserial.Mis_desc = _status.Rows[0]["mis_desc"].ToString();
                                _newserial.Tus_ser_1 = "N/A";
                                _newserial.Tus_ser_2 = "N/A";
                                _newserial.Tus_warr_no = "N/A";
                                _serList.Add(_newserial);

                            }
                            var _scanItems2 = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_qty, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_base_doc_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems2)
                            {

                               // _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), null, itm.Peo.Tus_base_doc_no, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                                if (_result != null)
                                {
                                    foreach (DataRow row in _result.Rows)
                                    {
                                        if ((row["itri_itm_cd"].ToString() == itm.Peo.Tus_itm_cd) && (row["ITR_REQ_NO"].ToString() == itm.Peo.Tus_base_doc_no))
                                        {
                                            row["itri_mqty"] = itm.Peo.Tus_qty;
                                        }

                                    }
                                }

                            }
                           
                        }

                       
                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = _result;
                        grdItems.DataBind();

                        grdItems.Columns[8].Visible = false;
                        grdItems.Columns[0].Visible = false;

                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();
                        grdSerial.Columns[0].Visible = false;
                    }
                    else 
                    {
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch(_invHdr.Ith_doc_no.ToString());
                        List<ReptPickSerials> _getItem = new List<ReptPickSerials>();
                        _getItem = CHNLSVC.Sales.GetInvItem(Doc);

                        foreach (InventoryBatchN _Batch in _serListT)
                        {
                            _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), null, _Batch.Inb_base_ref_no, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                            if (_result != null)
                            {
                                foreach (ReptPickSerials itm in _getItem)
                                {
                                    foreach (DataRow row in _result.Rows)
                                    {
                                        if ((row["itri_itm_cd"].ToString() == itm.Tus_itm_cd) && (row["ITR_REQ_NO"].ToString() == _Batch.Inb_base_ref_no))
                                        {
                                            row["itri_mqty"] = itm.Tus_qty;
                                        }

                                    }
                                }
                              
                            }
                        }
                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = _result;
                        grdItems.DataBind();


                        ReptPickSerials _serial = new ReptPickSerials();
                        if (_serListT != null)
                        {
                            foreach (InventoryBatchN _batch in _serListT)
                            {
                                _serial.Tus_itm_cd = _batch.Inb_itm_cd;
                                _serial.Tus_qty = _batch.Itb_bal_qty1;
                                DataTable _status = CHNLSVC.Sales.GetItemStatusTxt(_batch.Inb_itm_stus);                                
                                _serial.Mis_desc = _status.Rows[0]["mis_desc"].ToString();
                                _serial.Tus_ser_1 = "N/A";
                                _serial.Tus_ser_2 = "N/A";
                                _serial.Tus_warr_no = "N/A";
                                _serial.Tus_base_doc_no = _batch.Inb_base_ref_no;
                                _serList.Add(_serial);

                            }
                            grdItems.Columns[8].Visible = false;
                            grdSerial.AutoGenerateColumns = false;
                            grdSerial.DataSource = _serList;
                            grdSerial.DataBind();
                            grdSerial.Columns[2].Visible = false;
                            grdSerial.Columns[3].Visible = false;
                            grdSerial.Columns[4].Visible = false;
                        }
                        
                    }

                    //else
                    //{

                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item not found');", true);
                    //    txtIssueNo.Text = "";
                    //    txtIssueNo.Focus();
                    //    return;
                    //}
                    #endregion

                }
            }
            catch (Exception err)
            {
                string _Msg = err.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
                        
               
            }
           
        }

        private void calItemQty(string Reqno, DataTable _FilterTbl,int user_seq_num,bool _isTemp, bool _delete,CheckBox _check)
        {

            DataView dv = new DataView(_FilterTbl);
            dv.RowFilter = "itr_req_no = '" + Reqno + "'";

            DataTable _ManageTbl = dv.ToTable();
            if (_ManageTbl!=null)
            {
                for(int i = _ManageTbl.Rows.Count-1; i >= 0; i--)
                {
                    DataRow dr = _ManageTbl.Rows[i];
                    string _poqty_s = dr["itri_po_qty"].ToString();
                    //if ((_poqty_s == "") || (_poqty_s == "N/A"))
                    //{
                    //    _check.Checked = false;
                    //    string _msg = "Request no: "+ Reqno + " has not been used for a purchase order";
      
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                    //    return;
                    //   // _poqty_s = "0";
                    //}
                    string _Issqty_s = dr["itri_issue_qty"].ToString();
                    if ((_Issqty_s == "") || (_Issqty_s == "N/A"))
                    {
                        _Issqty_s = "0";
                    }

                    if ((_poqty_s == "") || (_poqty_s == "N/A"))
                    {
                        _poqty_s = "0";
                    }
                    decimal _poqty = Convert.ToDecimal(_poqty_s);
                    decimal _Issqty = Convert.ToDecimal(_Issqty_s);

                    decimal _balanceQty = _poqty - _Issqty;
                    if (_balanceQty == 0)
                    {
                        dr.Delete();
                    }
                    else
                    {
                        dr["itri_bqty"] = _balanceQty.ToString();
                    }
                   // dr.AcceptChanges();
                }

                
                // ViewState["ISITEM"] = dv;
               
               // ViewState["Request"] = _FilterTbl;

                LoadItem(_ManageTbl, Reqno, user_seq_num, _isTemp, _delete);
            }
        }

        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
               
                txtUserSeqNo.Text = ddlSeqNo.SelectedItem.Text;
                LoadSeqItems(Convert.ToInt32(txtUserSeqNo.Text));
                ucOutScan.userSeqNo = txtUserSeqNo.Text;
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
          
        }
   
        private void GetSeqNo()
        {
           SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "IS", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            ddlSeqNo.DataSource = SeqNumList;
            ddlSeqNo.DataBind();
        }

        private void LoadSeqItems(Int32 user_seq_num)
        {
            try
            {
                int _direction = 0;
                _direction = 0;
               // ReptPickHeader _header = new ReptPickHeader();
               //_header= CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");
            
              
                
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");
                if (_serList != null)
                {
                    var distinctItems = _serList.GroupBy(x => x.Tus_base_doc_no).Select(y => y.First()).ToList();
                    foreach (ReptPickSerials _Hdd in distinctItems)
                    {
                        string RequestNo = _Hdd.Tus_base_doc_no;
                        if (RequestNo != null)
                        {
                            DataTable _result = CHNLSVC.Inventory.Get_A_F_PoReq(Session["UserCompanyCode"].ToString(), null,  RequestNo, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                            if (_result != null)
                            {

                                calItemQty(RequestNo, _result, Convert.ToInt32(txtUserSeqNo.Text), true, false,null);
                            }
                        }
                    }


                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = _serList;
                    ucOutScan.PickSerial = _serList;
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = emptyGridList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = emptyGridList;
                    ucOutScan.PickSerial = emptyGridList;
                }

                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = gvItems;
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        #region Modal Popup 2

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "IS")
            {
                try
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    lblvalue.Text = "IS";
                    UserDPopoup.Show();
                    return;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                }

            }
        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Des = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "IS")
            {
                txtIssueNo.Text = Des;
                GetDocData(Des);
                return;
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "IS")
            {
                try
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
                    txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                    txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    BindUCtrlDDLData2(_result);
                    lblvalue.Text = "IS";
                    UserDPopoup.Show();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                }

            }
            

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "IS")
            {
                try
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtTDate.Text).Date);
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    lblvalue.Text = "IS";
                    UserDPopoup.Show();
                    return;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                }
            }

        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "IS")
            {
                try
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtTDate.Text).Date);
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    lblvalue.Text = "IS";
                    UserDPopoup.Show();
                    Session["Date"] = "true";
                    return;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
                }
            }
        }
        #endregion
    }
}