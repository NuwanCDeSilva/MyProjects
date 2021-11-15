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

namespace FastForward.SCMWeb.View.Additional
{
    public partial class Serial_Picker : Base
    {
        bool _serPopShow
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
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }

        protected List<ReptPickItems> _ReptPickItems { get { return (List<ReptPickItems>)Session["_ReptPickItems"]; } set { Session["_ReptPickItems"] = value; } }
        protected List<ReptPickSerials> _serList { get { return (List<ReptPickSerials>)Session["_serList"]; } set { Session["_serList"] = value; } }

        protected ReptPickHeader _ReptPickHeader { get { return (ReptPickHeader)Session["_ReptPickHeader"]; } set { Session["_ReptPickHeader"] = value; } }

        protected Int32 user_seq_num { get { return (Int32)Session["user_seq_num"]; } set { Session["user_seq_num"] = value; } }

        protected MasterItem _Mstitem { get { return (MasterItem)Session["_Mstitem"]; } set { Session["_Mstitem"] = value; } }
        private List<MasterItemStatus> _stsList
        {
            get { if (Session["_stsList"] != null) { return (List<MasterItemStatus>)Session["_stsList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_stsList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageclear();
                   
                }
                else
                {

                }
            }
            catch
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        private void pageclear()
        {
            lblTotalqty.Text = "0"; 
            lblDocTotQty.Text = "0"; 
            user_seq_num = -1;
            Session["DIRECTION"] = "0";
            //Session["DOC_TYPE"] = "PRN";
            loadDocType();
            loadSessionvValues();
            loadItemStatus();
            grdpickserial.DataSource = new int[] { };
            grdpickserial.DataBind();
            DateTime date = DateTime.Now;
            _stsList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            txtFDate.Text = date.AddMonths(-2).ToString("dd/MMM/yyyy");
            txtTDate.Text = date.AddDays(1).ToString("dd/MMM/yyyy");

            //Add by akila 2017/09/09
            //Style _style = new Style();
            //if (Session["UserCompanyCode"].ToString()=="AAL")
            //{
            //     if (SerialScanMethod() == 1) { _style.CssClass = "form-control txtUppercase"; } else { _style.CssClass = "form-control txtLowercase"; }
            //        txtserial.ApplyStyle(_style);
            //}
           
        }
        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());

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
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append("SER1_WITEM" + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItemcode.Text.ToUpper() + seperator + ddlBincode.SelectedItem);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ddldoctype.SelectedValue + seperator + ddltypes.SelectedValue);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {

                        paramsText.Append(txtDocNo.Text + seperator);
                        break;
                    } 
                case CommonUIDefiniton.SearchUserControlType.Item_2:
                    {
                        paramsText.Append(txtDocNo.Text + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void PopulateDropDownBin(int direction)
        {
            try
            {
                if (direction == 0)
                {
                    DataTable dtbincode = CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemcode.Text.ToUpper());
                    if (dtbincode.Rows.Count > 0)
                    {
                        ddlBincode.DataSource = dtbincode;
                        ddlBincode.DataTextField = "INB_BIN";
                        ddlBincode.DataValueField = "INB_BIN";
                        ddlBincode.DataBind();

                        //ddlBincode.Items.Insert(0, new ListItem("---Select---", ""));
                        // ddlBincode.SelectedIndex = 0;

                    }
                }
                else
                {
                    DataTable dtbincode = CHNLSVC.Inventory.LoadBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (dtbincode.Rows.Count > 0)
                    {
                        ddlBincode.DataSource = dtbincode;
                        ddlBincode.DataTextField = "ibn_bin_cd";
                        ddlBincode.DataValueField = "ibn_bin_cd";
                        ddlBincode.DataBind();

                        ddlBincode.Items.Insert(0, new ListItem("---Select---", ""));
                        ddlBincode.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        private void PopulateDropDownStatus(int direction)
        {
            try
            {
                if (direction == 0)
                {
                    DataTable dtitmstatus = CHNLSVC.Inventory.LoadItemStatusOfBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemcode.Text.ToUpper().Trim(), ddlBincode.SelectedValue);
                    if (dtitmstatus.Rows.Count > 0)
                    {
                        ddlitemStatus.DataSource = dtitmstatus;
                        ddlitemStatus.DataTextField = "Status";
                        ddlitemStatus.DataValueField = "inb_itm_stus";
                        ddlitemStatus.DataBind();

                        if (ddlitemStatus.Items.Count > 1)
                        {
                            ddlitemStatus.Items.Insert(0, new ListItem("---Select---", ""));
                        }

                    }
                }
                else
                {
                    DataTable oItemStaus = CHNLSVC.General.GetItemStatusByCom(Session["UserCompanyCode"].ToString());

                    if (oItemStaus != null && oItemStaus.Rows.Count > 0)
                    {

                        ddlitemStatus.DataSource = oItemStaus;
                        ddlitemStatus.DataTextField = "mis_desc";
                        ddlitemStatus.DataValueField = "Mis_cd";
                        ddlitemStatus.DataBind();
                        ddlitemStatus.SelectedIndex = ddlitemStatus.Items.IndexOf(ddlitemStatus.Items.FindByValue("GDLP")); 
                        ddlitemStatus.Items.Insert(0, new ListItem("---Select---", ""));
                        ddlitemStatus.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        private void GettempListItem(string doc, string doctype, int direction)
        {
            user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doctype, Session["UserCompanyCode"].ToString(), doc, direction);
            if (user_seq_num > 0)//check whether Tuh_doc_no exists in temp_pick_hdr
            {
                _ReptPickItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                // LoadPickSerial(user_seq_num, doctype);
                _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), null, user_seq_num, doctype);
                if (_ReptPickHeader == null)
                {
                    string msg = "Please select doc agin";
                    txtDocNo.Text = string.Empty;
                    // DisplayMessage(msg, 1);
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msg + "');", true);
                }
                //_ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(new ReptPickHeader()
                //{
                //    Tuh_doc_no = doc,//invHdr.Ith_oth_docno,
                //    Tuh_doc_tp = doctype,
                //    Tuh_direct = Convert.ToBoolean(direction),
                //    Tuh_usr_com = Session["UserCompanyCode"].ToString()
                //}).FirstOrDefault();
               
            }
        }
        private void filterItem(string Item, string status, string line)
        {
            //if (status != "")
            // {
            var filterItem = _ReptPickItems.Find(x => x.Tui_req_itm_cd == Item && x.Tui_pic_itm_cd == line);
            if (filterItem != null)
            {
                lbldocqty.Text = filterItem.Tui_req_itm_qty.ToString();
                lblscanQty.Text = filterItem.Tui_pic_itm_qty.ToString();
                PopulateDropDownStatus(Convert.ToInt32(ddltypes.SelectedValue));
                Session["_lineNo"] = filterItem.Tui_pic_itm_cd;
                //  ddlitemStatus.SelectedValue = filterItem.Tui_req_itm_stus;

            }
            // }
        }
        private void LoadItemdetails(string Item)
        {
            // MasterItem _item = new MasterItem();
            _Mstitem = CHNLSVC.General.GetItemMaster(Item);
            if (_Mstitem != null)
            {
                lblserialize.Text = _Mstitem.Mi_is_ser1 == 1 ? "YES" : "NO";
                lblsubsrial.Text = _Mstitem.Mi_is_scansub == true ? "YES" : "NO";
                lblchassisno.Text = _Mstitem.Mi_is_ser2 == 1 ? "YES" : "NO";
                lblitemDes.Text = _Mstitem.Mi_longdesc;
                lblmodel.Text = _Mstitem.Mi_model;
                if (_Mstitem.Mi_is_ser1 == 1)
                {
                    txtserial.Enabled = true;
                    txtqty.Enabled = false;
                }
                else
                {
                    txtserial.Enabled = false;
                    lbtnserial.Visible = false;
                    txtqty.Enabled = true;
                }
            }
        }
        private void LoadPickSerial(int seq, string doctype)
        {
            if (ddldoctype.SelectedValue == "GRN")
            {
                _serList = CHNLSVC.Inventory.GetAllScanSerialsListPartial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), seq, doctype);
            }
            else
            {
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), seq, doctype);
            }
            if (_serList != null)
            {
                //update by akila 2017/09/12
                var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(i=>i.Tus_qty)});
                //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_qty, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {

                    if ((itm.Peo.Tus_itm_cd == txtItemcode.Text)&& (itm.Peo.Tus_base_itm_line == Convert.ToInt32(Session["_lineNo"])))
                    {
                        if (itm.theCount > 1)
                        {
                            lblscanQty.Text = itm.theCount.ToString();//itm.Peo.Tus_qty; // Current scan qty    

                        }
                        else
                        {
                            lblscanQty.Text = itm.theCount.ToString();//itm.Peo.Tus_qty; // Current scan qty    

                        }
                    }


                
                }
                decimal _TOTAL = _serList.Sum(x => x.Tus_qty);
                lblTotalqty.Text = _TOTAL.ToString();
                var _filterItem = new List<ReptPickSerials>();
                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    _filterItem = _serList.ToList();
                }
                else
                {
                    _filterItem = _serList.Where(x => x.Tus_itm_cd == txtItemcode.Text).ToList();
                }
               // grdpickserial.DataSource = SerialDes(_filterItem);
               // grdpickserial.DataBind();
                if (_filterItem.Count == 0)
                {
                    lblscanQty.Text = "0";

                }

                if (lblscanQty.Text == lbldocqty.Text)
                {
                    decimal _qty = 0,_tmpQty =0;
                    _qty = decimal.TryParse(lblscanQty.Text, out _tmpQty) ? Convert.ToDecimal(lblscanQty.Text) : 0;
                    if (_qty != 0)
                    {
                        string msg = "Item scan completed";
                        // DisplayMessage(msg, 1);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msg + "');", true);
                        //txtItemcode.Text = string.Empty;
                        //lblscanQty.Text = "0";
                        //lbldocqty.Text = "0";
                        //lblserialize.Text = string.Empty;
                        //lblsubsrial.Text = string.Empty;
                        //lblchassisno.Text = string.Empty;
                        //lblitemDes.Text = string.Empty;
                        //lblmodel.Text = string.Empty;
                        txtItemcode.Focus();
                    }
                }
            }
            else
            {
                grdpickserial.DataSource = new int[] { };
                grdpickserial.DataBind();
                lblscanQty.Text = "0";
                lblTotalqty.Text = "0";
            }


        }

        private void LoadInwardCost(string _itm, string _line)
        {
            lblcost.Text = "0";
            DataTable po_items = new DataTable();
            po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtDocNo.Text, 1);
            if (po_items.Rows.Count == 0)
            {
                po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtDocNo.Text, 2);
                foreach (DataRow drValue in po_items.Rows)
                {
                    if (drValue["podi_bal_qty"].ToString() == "0")
                    {
                        drValue.Delete();
                    }

                }

                po_items.AcceptChanges();
            }

            foreach (DataRow _row in po_items.Rows)
            {
                Int32 _lineNo = Convert.ToInt32(_line);
                Int32 _PodlineNo = Convert.ToInt32(_row["podi_line_no"].ToString());
                if (_lineNo == _PodlineNo)
                {
                    string _cost = _row["UNIT_PRICE"].ToString();
                    lblcost.Text = _cost;
                    break;
                }
            }
        }
        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Doc_no")
            {
                txtDocNo.Text = ID;
                GettempListItem(ID, ddldoctype.SelectedValue, Convert.ToInt32(ddltypes.SelectedValue));
                txtItemcode.Focus();
                txtItemcode.Text = string.Empty;
                lblscanQty.Text = "0";
                lbldocqty.Text = "0";
                lblserialize.Text = string.Empty;
                lblsubsrial.Text = string.Empty;
                lblchassisno.Text = string.Empty;
                lblitemDes.Text = string.Empty;
                lblmodel.Text = string.Empty;
                grdpickserial.DataSource = new int[] { };
                grdpickserial.DataBind();
                TotalSanc_docQty();
                convertdecimal();
                ddldoctype.Enabled = false;
                ddltypes.Enabled = false;
                return;
            }
            if (lblvalue.Text == "Item")
            {
                txtItemcode.Text = ID;
                string line = grdResult.SelectedRow.Cells[2].Text;

                PopulateDropDownBin(Convert.ToInt32(ddltypes.SelectedValue));
                PopulateDropDownStatus(Convert.ToInt32(ddltypes.SelectedValue));
                filterItem(ID, ddlitemStatus.SelectedValue, line);
                LoadItemdetails(ID);
                LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                LoadInwardCost(ID.Trim().ToUpper(), line);
                convertdecimal();
                txtserial.Focus();
                return;
            }
            if (lblvalue.Text == "serial")
            {
                txtserial.Text = ID;
                txtserial_TextChanged(null, null);
                return;
            }
        }
        protected void btndgvpendSelect_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label item = (Label)row.FindControl("lblItemCode");
            Label lineval = (Label)row.FindControl("lblLineNo");

            txtItemcode.Text = item.Text;
            string line = lineval.Text;
            string ID = item.Text;
            Session["lineval"] = lineval.Text;
            PopulateDropDownBin(Convert.ToInt32(ddltypes.SelectedValue));
            PopulateDropDownStatus(Convert.ToInt32(ddltypes.SelectedValue));
            filterItem(ID, ddlitemStatus.SelectedValue, line);
            LoadItemdetails(ID);
            LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
            LoadInwardCost(item.Text.Trim().ToUpper(),line);
            convertdecimal();
            return;
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;

            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GET_TEMP_ITM(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "serial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable _result = CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Doc_no")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = new DataTable();
                if (ddldoctype.SelectedValue == "GRN")
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC_PARTIAL(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                else
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                _result.Columns.Remove("DATE");
                lblvalue.Text = "Doc_no";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
                DataTable _result = CHNLSVC.CommonSearch.SearchTempPickItemData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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

        private void FilterData()
        {
            if (lblvalue.Text == "Doc_no")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = new DataTable();
                if (ddldoctype.SelectedValue == "GRN")
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC_PARTIAL(SearchParams, ddlSearchbykeyDate.SelectedItem.ToString(), "%" + txtSearchbywordDate.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                else
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, ddlSearchbykeyDate.SelectedItem.ToString(), "%" + txtSearchbywordDate.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                _result.Columns.Remove("DATE");
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GET_TEMP_ITM(SearchParams, "ITEM CODE", "%" + txtSearchbyword.Text.ToString());//ddlSearchbykey.SelectedItem.ToString() //Removed By Udaya Item search 15.08.2017
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return; 
            }
            if (lblvalue.Text == "serial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable _result = CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
                DataTable _result = CHNLSVC.CommonSearch.SearchTempPickItemData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString().Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
           
        }

        #endregion

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
        protected void lbtnfin_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                try
                {
                    int result = 0;
                    string _err = string.Empty;
                    if (ddldoctype.SelectedValue=="GRN")
                    {
                        result = CHNLSVC.Inventory.DocumentFinishPartially(Session["UserCompanyCode"].ToString(), txtDocNo.Text, ddldoctype.Text, 1, Session["UserID"].ToString(), out _err);
                    }
                    else
                    {
                        result = CHNLSVC.Inventory.updateDocumentFinishStatus(txtDocNo.Text, ddldoctype.Text, 1, out _err);
                    }
                    

                    if (result > 0)
                    {
                        DisplayMessage("Successfully finish", 3);
                    }
                    else if (result==-2) 
                    {
                        DispMsg(_err);
                    }
                    else
                    {
                        DisplayMessage(_err, 4);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtSavelconformmessageValue.Value = "";
        }
        private void loadDocType()
        {
            try
            {
                string doc_derection = (string)Session["DIRECTION"];
                Int32 der = -1;
                if (doc_derection != null)
                {
                    der = Convert.ToInt32(doc_derection);
                }
                DataTable tempdoctype = CHNLSVC.Inventory.GetTempPickDocTypes(der);
                if (tempdoctype.Rows.Count > 0)
                {
                    ddldoctype.DataSource = tempdoctype;
                    ddldoctype.DataTextField = "tdt_tp_desc";
                    ddldoctype.DataValueField = "tdt_tp";
                    ddldoctype.DataBind();
                }
                ddldoctype.Items.Insert(0, new ListItem("Select", ""));
                if (der == 0)
                {
                    ddldoctype.SelectedIndex = 0;
                    ddldoctype.Enabled = true;
                }
                else
                {
                   // ddldoctype.SelectedIndex = 2;
                   // ddldoctype.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 3);
            }
        }
        private void loadSessionvValues()
        {
            string doc_derection = (string)Session["DIRECTION"];
            string doc_type = (string)Session["DOC_TYPE"];
            if (doc_derection != null)
            {
                ddltypes.SelectedIndex = -1;
                //ddltypes.SelectedValue = doc_derection;
            }

            if (doc_type != null)
            {
                ddldoctype.SelectedIndex = 0;
                // ddldoctype.SelectedValue = doc_type;
            }
            else
            {
                ddldoctype.SelectedIndex = 0;
            }
        }

        protected void lbtndocno_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddldoctype.SelectedIndex == 0)
                {
                    DisplayMessage("Please select doc type", 2);
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = new DataTable();
                if (ddldoctype.SelectedValue=="GRN")
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC_PARTIAL(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                else
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                _result.Columns.Remove("DATE");
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "Doc_no";
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DOC_TYPE"] = ddldoctype.SelectedValue;
        }

        #region Modal Popup 2
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyDate.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyDate.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyDate.SelectedIndex = 0;
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            DataTable _result = new DataTable();
            if (ddldoctype.SelectedValue == "GRN")
            {
                _result = CHNLSVC.Inventory.GET_TEMP_DOC_PARTIAL(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            }
            else
            {
                _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            }
            grdResultDate.DataSource = _result;
            grdResultDate.DataBind();
            UserDPopoup.Show();
        }
        protected void grdResultDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Added By Udaya 13.09.2017
            //GettempListItem(grdResultDate.SelectedRow.Cells[1].Text, ddldoctype.SelectedValue, Convert.ToInt32(ddltypes.SelectedValue));
            //LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordDate.ClientID + "').value = '';", true);
            string ID = grdResultDate.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Doc_no")
            {
                txtDocNo.Text = ID;
                GettempListItem(ID, ddldoctype.SelectedValue, Convert.ToInt32(ddltypes.SelectedValue));
                if (_ReptPickHeader != null)
                {
                    if (_ReptPickHeader.Tuh_fin_stus == 1)
                    {
                        DisplayMessage("Already Finish this document", 4);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('This document has been already finished');", true);
                        return;
                    }
                }
                txtItemcode.Focus();
                txtItemcode.Text = string.Empty;
                lblscanQty.Text = "0";
                lbldocqty.Text = "0";
                lblserialize.Text = string.Empty;
                lblsubsrial.Text = string.Empty;
                lblchassisno.Text = string.Empty;
                lblitemDes.Text = string.Empty;
                lblmodel.Text = string.Empty;
                if (Session["UserCompanyCode"].ToString() != "AAL")
                {
                    grdpickserial.DataSource = new int[] { };
                    grdpickserial.DataBind();
                }
                TotalSanc_docQty();
                convertdecimal();
                ddldoctype.Enabled = false;
                ddltypes.Enabled = false;
                if (!string.IsNullOrEmpty(txtDocNo.Text))
                {
                    txtDocNo_TextChanged(null, null);
                }
                return;
            }
        }
        protected void grdResultDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultDate.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc_no")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = new DataTable();
                if (ddldoctype.SelectedValue == "GRN")
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC_PARTIAL(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                else
                {
                    _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                }
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                UserDPopoup.Show();
                return;
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
        protected void txtSearchbywordDate_TextChanged(object sender, EventArgs e)
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
        protected void ddltypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DIRECTION"] = ddltypes.SelectedValue;
            loadDocType();
            if (ddltypes.SelectedValue == "1")
            {
                PopulateDropDownBin(1);
                PopulateDropDownStatus(1);
                lbtnserial.Visible = false;
            }
            else
            {
                lbtnserial.Visible = true;
                ddlBincode.DataSource = new int[] { };
                ddlBincode.DataBind();
                ddlitemStatus.DataSource = new int[] { };
                ddlitemStatus.DataBind();
            }
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            //DataTable _result = CHNLSVC.Inventory.GET_TEMP_DOC(SearchParams, "Doc #", "%" + txtDocNo.Text);
            //if (_result.Rows.Count == 0)
            //{
            //    DisplayMessage("Please select the valid document number", 2);
            //    txtDocNo.Text = string.Empty;
            //    txtDocNo.Focus();
            //    return;
            //}
            GettempListItem(txtDocNo.Text, ddldoctype.SelectedValue, Convert.ToInt32(ddltypes.SelectedValue));
            //Added By Udaya 13.09.2017
            LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                LoadDocData(txtDocNo.Text.Trim(),false);
            }
        }

        protected void lbtnItemcode_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_2);
                //DataTable _result = CHNLSVC.Inventory.GET_TEMP_ITM(SearchParams, null, null);
                DataTable _result = CHNLSVC.CommonSearch.SearchTempPickItemData(SearchParams, null, null);
                if (ddldoctype.SelectedValue=="GRN")
                {
                    _result = CHNLSVC.CommonSearch.SearchTempPickItemDataPartial(SearchParams, null, null);
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.SearchTempPickItemData(SearchParams, null, null);
                }
                grdResult.DataSource = _result;
                grdResult.DataBind();
                DataTable _dt = new DataTable();
                _dt.Columns.Add("Item Code");
                _dt.Columns.Add("Model");
                BindUCtrlDDLData(_dt);
                ddlSearchbykey.SelectedIndex = ddlSearchbykey.Items.IndexOf(ddlSearchbykey.Items.FindByText("Model"));
                lblvalue.Text = "Item_2";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GET_TEMP_ITM(SearchParams, "ITEM CODE", txtItemcode.Text);
                if (_result.Rows.Count == 0)
                {
                    DisplayMessage("Please type correct item code", 2);
                    txtItemcode.Text = string.Empty;
                    txtItemcode.Focus();
                    return;
                }
                else if (_result.Rows.Count > 1)
                {


                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);

                    foreach (DataRow _row in _result.Rows)
                    {
                        string _line = _row[1].ToString();
                        string _QTY = _row[2].ToString();

                        if (_serList != null)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {

                                if ((itm.Peo.Tus_itm_cd == txtItemcode.Text) && (itm.Peo.Tus_base_itm_line == Convert.ToInt32(_line)))
                                {
                                    if (Convert.ToDecimal(_QTY) > Convert.ToDecimal(itm.theCount))
                                    {
                                        lblscanQty.Text = itm.theCount.ToString();
                                        lbldocqty.Text = _QTY.ToString();
                                        Session["_lineNo"] = _line;
                                    }
                                }
                                else
                                {
                                    lblscanQty.Text = "0";
                                    lbldocqty.Text = _QTY.ToString();
                                    Session["_lineNo"] = _line;
                                }
                            }
                        }
                        else
                        {
                            lblscanQty.Text = "0";
                            lbldocqty.Text = _QTY.ToString();
                            Session["_lineNo"] = _line;
                            PopulateDropDownBin(0);
                            PopulateDropDownStatus(0);
                            filterItem(txtItemcode.Text.ToUpper(), ddlitemStatus.SelectedValue, "");
                            LoadItemdetails(txtItemcode.Text.ToUpper());
                            // LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                            txtserial.Focus();
                            return;
                        }

                    }
                }
                PopulateDropDownBin(0);
                PopulateDropDownStatus(0);
                string _line1 = _result.Rows[0][1].ToString();
                string _QTY1 = _result.Rows[0][2].ToString();
                lblscanQty.Text = "0";
                lbldocqty.Text = _QTY1.ToString();
                Session["_lineNo"] = _line1;
                filterItem(txtItemcode.Text.ToUpper(), ddlitemStatus.SelectedValue, "");
                LoadItemdetails(txtItemcode.Text.ToUpper());
                LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                txtserial.Focus();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void ddlBincode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDownStatus(0);
            //filterItem(txtItemcode.Text, null,0);
        }

        protected void ddlitemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filterItem(txtItemcode.Text,null);
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
        protected void lbtnserial_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                _serData = CHNLSVC.CommonSearch.SearchSerialByLocBIN(_para, null, null);
                LoadSearchPopup("ServiceJobSearchWIP", "Serial 1", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
            //try
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    BindUCtrlDDLData(_result);
            //    lblvalue.Text = "serial";
            //    UserPopoup.Show();
            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message, 4);
            //}
        }

        protected void txtserial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MasterCompany _masterComp = new MasterCompany();
                _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_masterComp != null)
                {
                    if (_masterComp.Mc_SerScn_Tp==1)
                    {
                        txtserial.Text = txtserial.Text.ToUpper();
                    }
                }
                if (ddltypes.SelectedValue == "0" && Session["UserCompanyCode"].ToString()== "AAL")
                {
                    if (string.IsNullOrEmpty(txtItemcode.Text))
                    {
                        if (!string.IsNullOrEmpty(txtserial.Text) && txtserial.Text.Trim().ToUpper() != "N/A")
                        {
                            var _hasSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                            {
                                Ins_com = Session["UserCompanyCode"].ToString(),
                                Ins_loc = Session["UserDefLoca"].ToString(),
                                Ins_available = 1,
                                Ins_ser_1 = txtserial.Text.Trim(),
                                Ser_tp = 0
                            }).FirstOrDefault();
                            if (_hasSer != null)
                            {
                                txtItemcode.Text = _hasSer.Ins_itm_cd;
                                txtItemcode_TextChanged(null,null);
                            }
                        }
                    }
                }
                if (ddltypes.SelectedValue == "0")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable _result = CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, "Serial 1", txtserial.Text);
                    if (_result.Rows.Count == 0)
                    {
                        DisplayMessage("Please type correct serial", 2);
                        txtserial.Text = string.Empty;
                        txtserial.Focus();
                        return;
                    }
                }
                if (ddlitemStatus.SelectedValue == "")
                {
                    DisplayMessage("Please select status", 2);
                    txtserial.Text = string.Empty;
                    txtserial.Focus();
                    return;
                }
                if (ddltypes.SelectedValue == "0")
                {
                    savePickSerial(txtItemcode.Text.ToUpper(), txtserial.Text);
                }
                else
                {
                    saveInwardPickSerial(txtItemcode.Text, txtserial.Text);
                }
                txtserial.Text = string.Empty;
                convertdecimal();
                txtserial.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        private void convertdecimal()
        {
            lblTotalqty.Text = Convert.ToDecimal(lblTotalqty.Text).ToString("#,##0.00##");
            lbldocqty.Text = Convert.ToDecimal(lbldocqty.Text).ToString("#,##0.00##");
            lblscanQty.Text = Convert.ToDecimal(lblscanQty.Text).ToString("#,##0.00##");
            lblDocTotQty.Text = Convert.ToDecimal(lblDocTotQty.Text).ToString("#,##0.00##");
        }
        private void saveInwardPickSerial(string _item, string _Serial)
        {
            if (user_seq_num == -1)
            {
                return;
            }
            decimal docqty = Convert.ToDecimal(lbldocqty.Text);
            decimal scanqty = Convert.ToDecimal(lblscanQty.Text);
            decimal totaldocscan = Convert.ToDecimal(lblTotalqty.Text);
            decimal totalscan = Convert.ToDecimal(lblscanQty.Text);

            if (docqty == scanqty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Cannot exceed document qty');", true);
                DisplayMessage("Cannot exceed document qty", 2);
                return;
            }
            string _serialNo2 = string.Empty;
            string _serialNo1 = _Serial;
            string _serialNo3 = string.Empty;
            string _warrantyno = string.Empty;
            string _scanDocument = txtDocNo.Text;
            string _binCode = ddlBincode.Text;
            string _itemStatus = ddlitemStatus.SelectedValue;
            decimal _unitCost = Convert.ToDecimal(lblcost.Text);
            int _line =Convert.ToInt32(Session["_lineNo"]);
            if (string.IsNullOrEmpty(_binCode))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the bin');", true);
                //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ddlBincode.Text == "---Select---")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the  bin');", true);
                //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_scanDocument))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document agin');", true);
                //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_line==0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document agin');", true);
                //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_Mstitem.Mi_is_ser1 >= 1)
            {


                int _serID = CHNLSVC.Inventory.IsExistInSerialMaster("", _item, _serialNo1);
                InventorySerialMaster _serIDMst = new InventorySerialMaster();
                _serIDMst = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serID);

                DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", _item, _serialNo1);

                if (_dtser1 != null)
                {
                    if (_dtser1.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist!');", true);
                        //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                _dtser1.Dispose();


                if ((CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), user_seq_num.ToString(), _item, _serialNo1)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist.!');", true);
                    // MessageBox.Show("Serial no 1 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ddldoctype.SelectedValue == "GRN")
                {
                    if ((CHNLSVC.Inventory.IsExistInTempPickSerialPartial(Session["UserCompanyCode"].ToString(), user_seq_num.ToString(), _item, _serialNo1)) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist.!');", true);
                        // MessageBox.Show("Serial no 1 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                _warrantyno = _serIDMst.Irsm_warr_no;
                // txtWNo.Text = _warrantyno;
                //Write to the Picked items serial table.
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                #region Fill Pick Serial Object
                _inputReptPickSerials.Tus_usrseq_no = user_seq_num;
                _inputReptPickSerials.Tus_doc_no = _scanDocument;
                _inputReptPickSerials.Tus_seq_no = 0;
                _inputReptPickSerials.Tus_itm_line = 0;
                _inputReptPickSerials.Tus_batch_line = 0;
                _inputReptPickSerials.Tus_ser_line = 0;
                _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                _inputReptPickSerials.Tus_bin = _binCode;
                _inputReptPickSerials.Tus_itm_cd = _item;
                _inputReptPickSerials.Tus_itm_stus = _itemStatus;
                if (_itemStatus == "CONS")
                {
                    _inputReptPickSerials.Tus_unit_cost = 0;
                    _inputReptPickSerials.Tus_unit_price = 0;
                }
                else
                {
                    _inputReptPickSerials.Tus_unit_cost = _unitCost;
                    _inputReptPickSerials.Tus_unit_price = _unitCost;
                }
               

                _inputReptPickSerials.Tus_qty = 1;



                if (_serID > 0)
                { _inputReptPickSerials.Tus_ser_id = _serID; }
                else
                { _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID(); }
                _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-P01-" + _inputReptPickSerials.Tus_ser_id.ToString();
                _inputReptPickSerials.Tus_warr_no = _warrantyno;
                _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(Session["_lineNo"]); ;
                _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                _inputReptPickSerials.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                _inputReptPickSerials.Tus_itm_model = _Mstitem.Mi_model;
                _inputReptPickSerials.Tus_itm_brand = _Mstitem.Mi_brand;
                _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]); ;
                _inputReptPickSerials.Tus_base_doc_no = _scanDocument;
                //string _baseItem = Session["baseitem"].ToString();
                //if (string.IsNullOrEmpty(_baseItem))
                //{
                //    _baseItem = PopupItemCode;
                //}
                _inputReptPickSerials.Tus_new_itm_cd = _item;

                //_inputReptPickSerials.Tus_base_doc_no = Session["baseitem"].ToString();
                _inputReptPickSerials.Tus_job_line = Convert.ToInt32(Session["_lineNo"]);
                // _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;
                // _inputReptPickSerials.Tus_exp_dt = Convert.ToDateTime(expirdate);


                #endregion
                //lblScanQty.Text = serCount.ToString();
                int affected_rows = 0;
                if (ddldoctype.SelectedValue == "GRN")
                {
                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerialsPartial(_inputReptPickSerials, null);
                }
                else
                {
                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                }
                if (affected_rows == 1)
                {
                    lbllastserial.Text = _Serial;
                    lblTotalqty.Text = (totaldocscan + 1).ToString();
                    lblscanQty.Text = (totalscan + 1).ToString();
                }
            }
            else if (_Mstitem.Mi_is_ser1 == 0 || _Mstitem.Mi_is_ser1==-1)
            {
                decimal Total = scanqty + Convert.ToDecimal(txtqty.Text);
                //if (docqty < Convert.ToDecimal(txtqty.Text))
                //{
                //    DisplayMessage("Cannot exceed document qty", 2);
                //    return;
                //}
                //if (docqty < Total)
                //{
                //    DisplayMessage("Cannot exceed document qty", 2);
                //    return;
                //}
                #region Non-serialized
                bool _finditem = false;
                int _actualQty = Convert.ToInt32(txtqty.Text.Trim());
                List<ReptPickSerials> _serList2 = new List<ReptPickSerials>();
                //_serList2 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
                if (ddldoctype.SelectedValue=="GRN")
                {
                   _serList2 = CHNLSVC.Inventory.GetAllScanSerialsListPartial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
                }
                else
                {
                    _serList2 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
                }
                #region add by lakshan 17Feb2018
                if (ddldoctype.SelectedValue=="GRN")
                {
                    if (_serList2 != null)
                    {
                        if (_serList2.Count > 0)
                        {
                            _serList2 = _serList2.Where(c => c.Tus_cre_by == Session["UserID"].ToString()).ToList();
                        }
                    } 
                }
                #endregion

                if (_serList2 != null)
                {
                    if (_serList2.Count > 0)
                    {

                        var _filter = _serList2.SingleOrDefault(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == ddlitemStatus.SelectedValue);
                        if (_filter != null)
                        {
                            _finditem = true;
                            _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(txtqty.Text);
                            Int32 value = 0;
                            if (ddldoctype.SelectedValue=="GRN")
                            {
                                string _outStr = "";
                                value = CHNLSVC.Inventory.UpdateAllScanSerialsPartial(_filter, out _outStr);
                            }
                            else
                            {
                                 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                            }
                            //LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                            if (value > 0)
                            {
                               // txtqty.Text = string.Empty;
                                lblscanQty.Text = (totalscan + Convert.ToInt32(txtqty.Text)).ToString();
                                txtqty.Text = string.Empty;
                            }
                        }

                    }
                }
                if (_finditem == false)
                {
                    //Write to the Picked items serials table.
                    ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                    #region Fill Pick Serial Object
                    _newReptPickSerials.Tus_usrseq_no = user_seq_num;
                    _newReptPickSerials.Tus_doc_no = _scanDocument;
                    _newReptPickSerials.Tus_seq_no = 0;
                    _newReptPickSerials.Tus_itm_line = 0;
                    _newReptPickSerials.Tus_batch_line = 0;
                    _newReptPickSerials.Tus_ser_line = 0;
                    _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _newReptPickSerials.Tus_bin = _binCode;
                    _newReptPickSerials.Tus_itm_cd = _item;
                    _newReptPickSerials.Tus_itm_stus = _itemStatus;
                    _newReptPickSerials.Tus_unit_cost = _unitCost;
                    _newReptPickSerials.Tus_unit_price = _unitCost;
                    _newReptPickSerials.Tus_qty = _actualQty;//1
                    _newReptPickSerials.Tus_ser_id = 0;//CHNLSVC.Inventory.GetSerialID();
                    _newReptPickSerials.Tus_ser_1 = "N/A";
                    _newReptPickSerials.Tus_ser_2 = "N/A";
                    _newReptPickSerials.Tus_ser_3 = "N/A";
                    _newReptPickSerials.Tus_warr_no = _warrantyno;
                    _newReptPickSerials.Tus_itm_stus = _itemStatus;
                    if (_itemStatus == "CONS")
                    {
                        _newReptPickSerials.Tus_unit_cost = 0;
                        _newReptPickSerials.Tus_unit_price = 0;
                    }
                    else
                    {
                        _newReptPickSerials.Tus_unit_cost = _unitCost;
                        _newReptPickSerials.Tus_unit_price = _unitCost;
                    }

                    
                    _newReptPickSerials.Tus_itm_line = Convert.ToInt32(Session["_lineNo"]); ;
                    _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                    //string _baseItem = Session["baseitem"].ToString();
                    //if (string.IsNullOrEmpty(_baseItem))
                    //{
                    //    _baseItem = PopupItemCode;
                    //}
                    _newReptPickSerials.Tus_new_itm_cd = _item;
                    //_newReptPickSerials.Tus_exp_dt = Convert.ToDateTime(expirdate);


                    #endregion

                    //int _val = CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                    int _val = 0;
                    if (ddldoctype.SelectedValue == "GRN")
                    {
                        _val = CHNLSVC.Inventory.SaveAllScanSerialsPartial(_newReptPickSerials, null);
                    }
                    else
                    {
                        _val = CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                    }
                    if (_val > 0)
                    {
                        txtqty.Text = string.Empty;
                    }
                }
                #endregion
            }
        }
        private void savePickSerial(string _item, string _Serial)
        {
            if (user_seq_num == -1)
            {
                return;
            }
            decimal docqty = Convert.ToDecimal(lbldocqty.Text);
            decimal scanqty = Convert.ToDecimal(lblscanQty.Text);
            ReptPickItems _pickItmData = new ReptPickItems();
            string _errorMessage = null;
            Int16 _updatedCount = 0;

            if (docqty == scanqty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Cannot exceed document qty');", true);
                DisplayMessage("Cannot exceed document qty", 2);
                return;
            }

            //Add by akila 2017/09/12 - Prepare pick item details
            List<ReptPickSerials> _tmpPickSerials = new List<ReptPickSerials>();
            ReptPickItems _pickItems = new ReptPickItems();

           // _tmpPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
            if (ddldoctype.SelectedValue=="GRN")
            {
                _tmpPickSerials = CHNLSVC.Inventory.GetAllScanSerialsListPartial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
            }
            else
            {
                _tmpPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
            }
            if (_tmpPickSerials != null && _tmpPickSerials.Count > 0)
            {
                _tmpPickSerials = _tmpPickSerials.Where(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == ddlitemStatus.SelectedValue && x.Tus_base_itm_line == Convert.ToInt32(Session["_lineNo"])).ToList();
                if (_tmpPickSerials != null && _tmpPickSerials.Count > 0)
                {
                    var _grpPickSer = _tmpPickSerials.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_base_itm_line, x.Tus_usrseq_no }).Select(x => new
                    {
                        tui_req_itm_cd = x.Key.Tus_itm_cd,
                        tui_req_itm_stus = x.Key.Tus_itm_stus,
                        tui_pic_itm_cd = x.Key.Tus_base_itm_line,
                        tui_usrseq_no = x.Key.Tus_usrseq_no,
                        tui_pic_itm_qty = x.Sum(i => i.Tus_qty)
                    });

                    if (_grpPickSer != null)
                    {
                        foreach (var _element in _grpPickSer)
                        {
                            _pickItems.Tui_usrseq_no = _element.tui_usrseq_no;
                            _pickItems.Tui_req_itm_cd = _element.tui_req_itm_cd;
                            _pickItems.Tui_req_itm_stus = _element.tui_req_itm_stus;
                            _pickItems.Tui_pic_itm_cd = _element.tui_pic_itm_cd.ToString();
                            _pickItems.Tui_pic_itm_qty = _element.tui_pic_itm_qty;
                        }
                    }
                }
            }


            if (_Mstitem.Mi_is_ser1 == 1)
            {
                // List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);

                // Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();
                // foreach (ReptPickSerials serial in Tempserial_list)
                // {
                //  int rowCount = 0;
                //-------------
                ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item, _Serial);
                // ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                //Update_inrser_INS_AVAILABLE
                if (_reptPickSerial_ != null)
                {
                    //Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _reptPickSerial_.Tus_itm_cd, Convert.ToInt32(_reptPickSerial_.Tus_ser_id), -1);

                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = txtDocNo.Text;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);//Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                    // _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                    // _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_exist_grnno = _reptPickSerial_.Tus_doc_no;
                    if (_ReptPickHeader.Tuh_is_take_res == true)
                    {
                        _reptPickSerial_.Tus_resqty = 1;
                    }
                    //enter row into TEMP_PICK_SER
                    _pickItems.Tui_pic_itm_qty += 1;
                    #region add by lakshan 15Sep2017
                    _pickItmData = new ReptPickItems();
                    _pickItmData.Tui_usrseq_no = _reptPickSerial_.Tus_usrseq_no;
                    _pickItmData.Tui_req_itm_cd = _reptPickSerial_.Tus_itm_cd;
                    _pickItmData.Tui_req_itm_stus = _reptPickSerial_.Tus_itm_stus;
                    _pickItmData.Tui_pic_itm_cd = _reptPickSerial_.Tus_base_itm_line.ToString();
                    _pickItmData.Tui_pic_itm_qty = _reptPickSerial_.Tus_qty;
                    #endregion
                    _updatedCount = CHNLSVC.Inventory.UpdateScanSerailItemDetails(_reptPickSerial_, _pickItmData, true, out _errorMessage);
                    if (_updatedCount == 1 && string.IsNullOrEmpty(_errorMessage))
                    {
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _reptPickSerial_.Tus_itm_cd, Convert.ToInt32(_reptPickSerial_.Tus_ser_id), -1);
                        lbllastserial.Text = _Serial;
                    }
                    else
                    {
                        DispMsg(_errorMessage);
                    }
                    //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    //if (affected_rows == 1)
                    //{
                    //    lbllastserial.Text = _Serial;
                    //}
                }
                else
                {
                    DispMsg("");
                }
                //rowCount++;
                //isManualscan = true;
                // }
                LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
            }
            else if ((_Mstitem.Mi_is_ser1 == 0) || (_Mstitem.Mi_is_ser1 == -1))
            {
                decimal Total = scanqty + Convert.ToDecimal(txtqty.Text);
                if (docqty < Convert.ToDecimal(txtqty.Text))
                {
                    DisplayMessage("Cannot exceed document qty", 2);
                    return;
                }
                if (docqty < Total)
                {
                    DisplayMessage("Cannot exceed document qty", 2);
                    return;
                }
                List<ReptPickSerials> _serList2 = new List<ReptPickSerials>();
                _serList2 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);
                if (_serList2 != null)
                {
                    if (_serList2.Count > 0)
                    {
                        int lineno = Convert.ToInt32(Session["_lineNo"]);
                        if (lineno==0)
                        {
                            DisplayMessage("please reload the page", 2);
                            return;
                        }

                        var _filter = _serList2.Where(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == ddlitemStatus.SelectedValue && x.Tus_base_itm_line == lineno).FirstOrDefault();
                        if (_filter != null)
                        {

                            _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(txtqty.Text);
                            _pickItems.Tui_pic_itm_qty += Convert.ToDecimal(txtqty.Text);
                            #region add by lakshan 15Sep2017
                            _pickItmData = new ReptPickItems();
                            _pickItmData.Tui_usrseq_no = _filter.Tus_usrseq_no;
                            _pickItmData.Tui_req_itm_cd = _filter.Tus_itm_cd;
                            _pickItmData.Tui_req_itm_stus = _filter.Tus_itm_stus;
                            _pickItmData.Tui_pic_itm_cd = _filter.Tus_base_itm_line.ToString();
                            _pickItmData.Tui_pic_itm_qty = _filter.Tus_qty;
                            #endregion
                            _updatedCount = CHNLSVC.Inventory.UpdateScanSerailItemDetails(_filter, _pickItems, false, out _errorMessage);

                            //Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                            //LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                        }
                        else
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = txtDocNo.Text;
                            _reptPickSerial_.Tus_doc_no = txtDocNo.Text;
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);
                            _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();

                            _reptPickSerial_.Tus_bin = ddlBincode.SelectedValue.ToString();

                            _reptPickSerial_.Tus_itm_cd = _item;
                            _reptPickSerial_.Tus_itm_stus = ddlitemStatus.SelectedValue;
                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtqty.Text);
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_3 = "N/A";
                            _reptPickSerial_.Tus_ser_4 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_serial_id = "0";
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            //_reptPickSerial_.Tus_job_no = JobNo;
                            //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                            //_reptPickSerial_.Tus_job_line = JobLineNo;
                            //_reptPickSerial_.Tus_exist_supp = suppler;
                            //_reptPickSerial_.Tus_orig_supp = suppler;
                            if (_ReptPickHeader.Tuh_is_take_res == true)
                            {
                                _reptPickSerial_.Tus_resqty = Convert.ToDecimal(txtqty.Text);
                            }

                            if (_pickItems == null)
                            {
                                _pickItems.Tui_usrseq_no = user_seq_num;
                                _pickItems.Tui_req_itm_cd = _item;
                                _pickItems.Tui_req_itm_stus = ddlitemStatus.SelectedValue;
                                _pickItems.Tui_pic_itm_cd = Session["_lineNo"].ToString();
                                _pickItems.Tui_pic_itm_qty = Convert.ToDecimal(txtqty.Text);
                            }
                            #region add by lakshan 15Sep2017
                            _pickItmData = new ReptPickItems();
                            _pickItmData.Tui_usrseq_no = _reptPickSerial_.Tus_usrseq_no;
                            _pickItmData.Tui_req_itm_cd = _reptPickSerial_.Tus_itm_cd;
                            _pickItmData.Tui_req_itm_stus = _reptPickSerial_.Tus_itm_stus;
                            _pickItmData.Tui_pic_itm_cd = _reptPickSerial_.Tus_base_itm_line.ToString();
                            _pickItmData.Tui_pic_itm_qty = _reptPickSerial_.Tus_qty;
                            #endregion
                            _updatedCount = CHNLSVC.Inventory.UpdateScanSerailItemDetails(_reptPickSerial_, _pickItmData, true, out _errorMessage);
                            //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            //LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                        }
                    }
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = txtDocNo.Text;
                    _reptPickSerial_.Tus_doc_no = txtDocNo.Text;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);
                    _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();

                    _reptPickSerial_.Tus_bin = ddlBincode.SelectedValue.ToString();

                    _reptPickSerial_.Tus_itm_cd = _item;
                    _reptPickSerial_.Tus_itm_stus = ddlitemStatus.SelectedValue;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtqty.Text);
                    _reptPickSerial_.Tus_ser_1 = "N/A";
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_unit_cost = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    //_reptPickSerial_.Tus_job_no = JobNo;
                    //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    //_reptPickSerial_.Tus_job_line = JobLineNo;
                    //_reptPickSerial_.Tus_exist_supp = suppler;
                    //_reptPickSerial_.Tus_orig_supp = suppler;
                    if (_ReptPickHeader.Tuh_is_take_res == true)
                    {
                        _reptPickSerial_.Tus_resqty = Convert.ToDecimal(txtqty.Text);
                    }

                    if (_pickItems == null)
                    {
                        _pickItems.Tui_usrseq_no = user_seq_num;
                        _pickItems.Tui_req_itm_cd = _item;
                        _pickItems.Tui_req_itm_stus = ddlitemStatus.SelectedValue;
                        _pickItems.Tui_pic_itm_cd = Session["_lineNo"].ToString();
                        _pickItems.Tui_pic_itm_qty = Convert.ToDecimal(txtqty.Text);
                    }

                    #region add by lakshan 15Sep2017
                    _pickItmData = new ReptPickItems();
                    _pickItmData.Tui_usrseq_no = _reptPickSerial_.Tus_usrseq_no;
                    _pickItmData.Tui_req_itm_cd = _reptPickSerial_.Tus_itm_cd;
                    _pickItmData.Tui_req_itm_stus = _reptPickSerial_.Tus_itm_stus;
                    _pickItmData.Tui_pic_itm_cd = _reptPickSerial_.Tus_base_itm_line.ToString();
                    _pickItmData.Tui_pic_itm_qty = _reptPickSerial_.Tus_qty;
                    #endregion
                    _updatedCount = CHNLSVC.Inventory.UpdateScanSerailItemDetails(_reptPickSerial_, _pickItmData, true, out _errorMessage);

                    //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    //LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
                }

                txtqty.Text = string.Empty;
                LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);
            }

        }



        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (user_seq_num == -1)
            {
                return;
            }
           
            if (txtqty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('can't add zero value!');", true);
                txtqty.Text = "";
                return;
            }
            if (ddlitemStatus.SelectedValue == "")
            {
                DisplayMessage("Please select status", 2);
                txtqty.Text = string.Empty;
                txtqty.Focus();
                return;
            }
            if (_Mstitem.Mi_is_ser1 == 0)
            {
                decimal number = Convert.ToDecimal(txtqty.Text);
                decimal fractionalPart = number % 1;
                if (fractionalPart != 0)
                {
                    DisplayMessage("only allow numeric value", 2);
                    txtqty.Text = "";
                    return;
                }


            }
            if (string.IsNullOrEmpty(txtqty.Text.Trim()))
            {
                DisplayMessage("Quantity should be positive value.", 2);
                return;
            }
            if (Convert.ToDecimal(txtqty.Text.Trim()) < 0)
            {
                DisplayMessage("Quantity should be positive value.", 2);
                return;
            }
            decimal qty;
            if (!decimal.TryParse(txtqty.Text, out qty))
            {
                txtqty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity.');", true);
                return;
            }
            MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemcode.Text.Trim().ToUpper());
            if (_itm.Mi_is_ser1 != -1)
            {
                int integer;
                if (!Int32.TryParse(txtqty.Text, out integer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This item not allow decimal value!');", true);
                    txtqty.Text = "";
                    return;
                }
            }
            
            if (ddltypes.SelectedValue == "0")
            {
                DataTable _inventoryLocation = new DataTable();
                if (_ReptPickHeader.Tuh_is_take_res == true)
                {
                    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus_RESNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemcode.Text.ToUpper().Trim(), null);
                }
                else
                {
                     _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemcode.Text.ToUpper().Trim(), string.Empty);
                }
                
                if (_inventoryLocation.Rows.Count > 0)
                {
                    var count = from bl in _inventoryLocation.AsEnumerable()
                                where bl.Field<string>("MIS_DESC") == ddlitemStatus.SelectedItem.Text && bl.Field<decimal>("INL_FREE_QTY") > 0
                                select bl.Field<decimal>("INL_FREE_QTY");
                    decimal itemcount;
                    string _s = count.FirstOrDefault().ToString();
                    if (_s=="0")
                    {
                        txtqty.Text = "";
                        txtqty.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                        return;
                    }
                    if (!(decimal.TryParse(count.First().ToString(), out itemcount)))
                    {
                        txtqty.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                        return;
                    }
                    else
                    {
                        if (itemcount < qty)
                        {
                            txtqty.Text = string.Empty;
                            txtqty.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please check the inventory balance...!');", true);
                            return;
                        }
                        else
                        {

                            savePickSerial(txtItemcode.Text, "N/A");


                        }
                    }
                }
                else
                {
                    txtItemcode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                    return;
                }
            }
            else
            {
                saveInwardPickSerial(txtItemcode.Text, null);
            }
        }

        protected void lbtnser_Remove_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdpickserial.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = (row.FindControl("lbltus_itm_cd") as Label).Text;
                string _serialID = (row.FindControl("lbltus_ser_id") as Label).Text;
                string _status = (row.FindControl("lbltus_itm_stus") as Label).Text;
                string lbltus_qty = (row.FindControl("lbltus_qty") as Label).Text;
                decimal _tmpQty = 0;
                _tmpQty = decimal.TryParse(lbltus_qty, out _tmpQty) ? Convert.ToDecimal(lbltus_qty) : 0;
                if (string.IsNullOrEmpty(_item)) return;
                OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialID), _status,_tmpQty);
                lbllastserial.Text = string.Empty;
                LoadDocData(txtDocNo.Text.Trim(),true);
            }
        }
        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status,decimal _qty)
        {
            try
            {
                //Add by akila 2017/09/13
                List<ReptPickItems> _pickItems = new List<ReptPickItems>();
                ReptPickItems _deletePickItem = new ReptPickItems();

                _pickItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                if (_pickItems != null && _pickItems.Count > 0)
                {
                    _pickItems = _pickItems.Where(x => x.Tui_req_itm_cd == _item).ToList();
                    if (_pickItems != null && _pickItems.Count > 0)
                    {
                        _deletePickItem = _pickItems.SingleOrDefault();
                        _deletePickItem.Tui_pic_itm_qty -= _qty;
                    }
                }

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), user_seq_num, Convert.ToInt32(_serialID), null, null, _deletePickItem);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);

                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), user_seq_num, _item, _status, _deletePickItem);

                }
                LoadPickSerial(user_seq_num, ddldoctype.SelectedValue);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }

        }


        private void clear()
        {

        }

        protected void btnqty_Click(object sender, EventArgs e)
        {
            txtserial_TextChanged(null, null);
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

                }

            }
            return _serial;
        }

        private void TotalSanc_docQty()
        {
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ddldoctype.SelectedValue);

            if (_serList != null)
            {
                var _scanItems = _serList.GroupBy(x => new { x.Tus_usrseq_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {
                    lblTotalqty.Text = itm.theCount.ToString();
                }
            }
        }


        private void updateitm(ReptPickItems _items)
        {
            //CHNLSVC.Inventory.UpdatePickItemStockInOutNew(_items);
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                LoadDocData(txtDocNo.Text.Trim(), true);
            }
            else
            {
                DispMsg("Please select the document # ");
            }
        }

        //by akila  - Initialize serial scan method
        private Int32 SerialScanMethod()
        {
            Int32 _method = 0;

            try
            {
                MasterCompany _masterComp = new MasterCompany();
                _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_masterComp != null)
                {
                    _method = _masterComp.Mc_SerScn_Tp;
                }
                else { _method = 0; }
            }
            catch (Exception ex)
            {
                _method = 0;
                DisplayMessage("Error occurred while initializing serial scan type" + Environment.NewLine + ex.Message, 4);
            }
            return _method;
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "ServiceWIPMRN_Loc")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                    _serData = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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
        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.Rows.Count > 0)
                {
                    txtserial.Text = dgvResult.SelectedRow.Cells[1].Text; ;
                    txtserial_TextChanged(null, null);
                }
                
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void LoadDocData(string _doc,bool _isGridBind = false)
        {
            lblDocTotQty.Text = (0).ToString();
            lblTotalqty.Text = (0).ToString();
            ReptPickHeader _pickHdrData = new ReptPickHeader();
            List<ReptPickItems> _itmList = new List<ReptPickItems>();
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            if (ddldoctype.SelectedValue == "GRN")
            {
                _pickHdrData = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_PARTIAL(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = _doc
                    }).FirstOrDefault();
                if (_pickHdrData != null)
                {
                    _serList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA_PARTIAL(new ReptPickSerials() { Tus_usrseq_no = _pickHdrData.Tuh_usrseq_no });
                    _itmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA_PARTIAL(new ReptPickItems() { Tui_usrseq_no = _pickHdrData.Tuh_usrseq_no });
                }
            }
            else
            {
                _pickHdrData = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                            Tuh_doc_no = _doc
                        }).FirstOrDefault();
                if (_pickHdrData != null)
                {
                    _serList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _pickHdrData.Tuh_usrseq_no });
                    _itmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _pickHdrData.Tuh_usrseq_no });
                }
            }
            if (_isGridBind)
            {
                grdpickserial.DataSource = new int[] { };
                if (_serList != null)
                {
                    if (_serList.Count > 0)
                    {
                        
                        foreach (var item in _serList)
                        {
                            var _sts = _stsList.Where(c => c.Mis_cd == item.Tus_itm_stus).FirstOrDefault();
                            if (_sts!=null)
                            {
                                item.Tus_itm_stus_Desc = _sts.Mis_desc;
                            }
                        }
                        if (chkUserWise.Checked && ddldoctype.SelectedValue == "GRN")
                        {
                            grdpickserial.DataSource =  _serList.Where(c => c.Tus_cre_by == Session["UserID"].ToString()).ToList();
                        }
                        else
                        {
                            grdpickserial.DataSource = _serList;
                        }
                    }
                }
                grdpickserial.DataBind();
            }
            if (_itmList.Count > 0)
            {
                decimal _totDocQty = 0;
                _totDocQty = _itmList.Sum(c => c.Tui_req_itm_qty);
                lblDocTotQty.Text = _totDocQty.ToString("#,##0.00##");
            }
            if (_serList.Count > 0)
            {
                decimal _totPickQty = 0;
                _totPickQty = _serList.Sum(c => c.Tus_qty);
                lblTotalqty.Text = _totPickQty.ToString("#,##0.00##");
            }
        }
    }



}