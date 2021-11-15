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
    public partial class FixAssetOrAdhocRequestAndApprove : BasePage
    {
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
        List<InventoryAdhocDetail> AdhodDetList
        {
            get { if (Session["AdhodDetList"] != null) { return (List<InventoryAdhocDetail>)Session["AdhodDetList"]; } else { return new List<InventoryAdhocDetail>(); } }
            set { Session["AdhodDetList"] = value; }
        }

        public List<ReptPickSerials> AvailableSerialList
        {
            get { if (Session["AvailableSerialList"] != null) { return (List<ReptPickSerials>)Session["AvailableSerialList"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["AvailableSerialList"] = value; }
        }

        public List<ReptPickSerials> Approved_SerialList
        {
            get { if (Session["Approved_SerialList"] != null) { return (List<ReptPickSerials>)Session["Approved_SerialList"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["Approved_SerialList"] = value; }
        }

        public InventoryAdhocHeader Searched_AdhodHeader
        {
            get { return (InventoryAdhocHeader)Session["Searched_AdhodHeader"]; }
            set { Session["Searched_AdhodHeader"] = value; }
        }
        public List<InventoryAdhocDetail> Det_list_selected
        {
            get { if (Session["Det_list_selected"] != null) { return (List<InventoryAdhocDetail>)Session["Det_list_selected"]; } else { return new List<InventoryAdhocDetail>(); } }
            set { Session["Det_list_selected"] = value; }
        }
        //Always contain the original requested item detail list
        public List<InventoryAdhocDetail> SearchedAdhocDetList
        {
            get { if (Session["SearchedAdhocDetList"] != null) { return (List<InventoryAdhocDetail>)Session["SearchedAdhocDetList"]; } else { return new List<InventoryAdhocDetail>(); } }
            set { Session["SearchedAdhocDetList"] = value; }
        }

        //-------------------------
        public List<HpTransaction> Transaction_List
        {
            get { if (Session["Transaction_List"] != null) { return (List<HpTransaction>)Session["Transaction_List"]; } else { return new List<HpTransaction>(); } }
            set { Session["Transaction_List"] = value; }
        }
        public List<RecieptItem> _recieptItem
        {
            get { if (Session["_recieptItem"] != null) { return (List<RecieptItem>)Session["_recieptItem"]; } else { return new List<RecieptItem>(); } }
            set { Session["_recieptItem"] = value; }
        }
        public List<PriceDefinitionRef> _PriceDefinitionRef
        {
            get { if (Session["_PriceDefinitionRef"] != null) { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } else { return new List<PriceDefinitionRef>(); } }
            set { Session["_PriceDefinitionRef"] = value; }
        }
        public string SelectedItemCD
        {
            get { if (Session["SelectedItemCD"] != null) { return (string)Session["SelectedItemCD"]; } else { return ""; } }
            set { Session["SelectedItemCD"] = value; }
        }
        public Int32 ItmLine
        {
            get { if (Session["ItmLine"] != null) { return (Int32)Session["ItmLine"]; } else { return 0; } }
            set { Session["ItmLine"] = value; }
        }
        public string Gen_ADJ_DocNo
        {
            get { if (Session["Gen_ADJ_DocNo"] != null) { return (string)Session["Gen_ADJ_DocNo"]; } else { return ""; } }
            set { Session["Gen_ADJ_DocNo"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserDefLoca"] == null || Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                if (!IsPostBack)
                {
                    lblRefNo.Text = "Ref #";
                    _serData = new DataTable();
                    BindTransactionTp();
                    //BindAction();
                    BindFilterAction();
                    BindEmptyGrid();
                    ddlTransActionTp.SelectedIndex = ddlTransActionTp.Items.IndexOf(ddlTransActionTp.Items.FindByText("FIXED ASSETS"));
                    ddlTransActionTp_SelectedIndexChanged(null, null);
                    //ddlAction.SelectedIndex = ddlAction.Items.IndexOf(ddlAction.Items.FindByValue("Request"));
                    //ddlAction_TextChanged(null, null);
                    Reset_Session_values();
                    ucPaymodes.InvoiceType = "HPR";
                    ucPaymodes.TotalAmount = 0;
                    ucPaymodes.LoadPayModes();
                    txtDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                    txtSendLoc.Text = Session["UserDefLoca"].ToString();

                    lbtnCancel.Enabled = false;
                    lbtnCancel.CssClass = "buttoncolorleft";
                    lbtnCancel.OnClientClick = "return Enable();";

                    lbtnAppAvaSer.Enabled = false;
                    lbtnAppAvaSer.CssClass = "buttoncolorleft";
                    lbtnAppAvaSer.OnClientClick = "return Enable();";

                    lbtnRejAvaSer.Enabled = false;
                    lbtnRejAvaSer.CssClass = "buttoncolorleft";
                    lbtnRejAvaSer.OnClientClick = "return Enable();";

                    lbtnRejAppSer.Enabled = false;
                    lbtnRejAppSer.CssClass = "buttoncolorleft";
                    lbtnRejAppSer.OnClientClick = "return Enable();";

                    lbtnConfAppser.Enabled = false;
                    lbtnConfAppser.CssClass = "buttoncolorleft";
                    lbtnConfAppser.OnClientClick = "return Enable();";

                    pnlPayMode.Visible = false;
                    pnlPayDetails.Visible = false;
                    LoadLocationDetails();
                }
                else
                {

                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void BindTransactionTp()
        {
            ddlTransActionTp.Items.Clear();
            // ddlTransActionTp.Items.Add(new ListItem("--Select--", "0"));
            ddlTransActionTp.Items.Add(new ListItem("FGAP", "1"));
            ddlTransActionTp.Items.Add(new ListItem("FIXED ASSETS", "2"));
        }
        private void BindAction()
        {
            ddlAction.Items.Clear();
            if (ddlTransActionTp.SelectedValue == "2")
            {
                ddlAction.Items.Add(new ListItem("NEW REQUEST", "1"));
                ddlAction.Items.Add(new ListItem("APPROVE REQUEST", "2"));
                ddlAction.Items.Add(new ListItem("CONFIRMATION", "3"));
            }
            else if (ddlTransActionTp.SelectedValue == "1")
            {
                ddlAction.Items.Add(new ListItem("APPROVE REQUEST", "1"));
                ddlAction.Items.Add(new ListItem("CONFIRMATION", "2"));
            }
            ddlAction.SelectedIndex = 0;
        }
        private void BindFilterAction()
        {
            while (ddlFilterSts.Items.Count > 1)
            {
                ddlFilterSts.Items.RemoveAt(1);
            }
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            List<KeyValuePair<string, string>> myList = status_list.ToList();

            myList.Sort((firstPair, nextPair) =>
            {
                return firstPair.Value.CompareTo(nextPair.Value);
            }
            );
            ddlFilterSts.DataSource = myList;
            ddlFilterSts.DataTextField = "Value";
            ddlFilterSts.DataValueField = "Key";
            if (status_list != null)
            {
                ddlFilterSts.DataBind();
            }
            ddlFilterSts.Items.Insert(0, new ListItem("ALL", "Any"));
            ddlFilterSts.SelectedIndex = 0;
        }

        private void BindEmptyGrid()
        {
            dgvItmDes.DataSource = new int[] { };
            dgvAvailableSerials.DataSource = new int[] { };
            dgvApproveItms.DataSource = new int[] { };

            dgvItmDes.DataBind();
            dgvAvailableSerials.DataBind();
            dgvApproveItms.DataBind();

        }
        private void LoadPriceDefaultValue()
        {
            try
            {
                MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                List<PriceDefinitionRef> _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());

                if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0)
                    {
                        var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                        if (_defaultValue != null)
                            if (_defaultValue.Count > 0)
                            {
                                //DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                                string DefaultBook = _defaultValue[0].Sadd_pb;
                                string DefaultLevel = _defaultValue[0].Sadd_p_lvl;
                                if (DefaultBook == txtPriceBook.Text.Trim().ToUpper())
                                {
                                    txtPbLvl.Text = DefaultLevel;
                                }
                                //DefaultStatus = _defaultValue[0].Sadd_def_stus;
                                //DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                                //LoadInvoiceType();
                                //LoadPriceBook(cmbInvType.Text);
                                //LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                                //LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                                //CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                            }
                    }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void Reset_Session_values()
        {
            ItmLine = 0;
            AdhodDetList = new List<InventoryAdhocDetail>();
            SelectedItemCD = string.Empty;
            AvailableSerialList = new List<ReptPickSerials>();
            Approved_SerialList = new List<ReptPickSerials>();
            Searched_AdhodHeader = null;
            Det_list_selected = new List<InventoryAdhocDetail>();
            SearchedAdhocDetList = new List<InventoryAdhocDetail>();
            Transaction_List = new List<HpTransaction>();
        }
        private void DispMsg(string msgText, string msgType)
        {
            // msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
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
        private void DispMsg(string msgText)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
        }
        protected void lbtnFGap_Click(object sender, EventArgs e)
        {

        }

        private void clearCompleateScreen()
        {
            try
            {
                //BindEmptyGrid();
                //BindTransactionTp();
                Reset_Session_values();
                txtDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                //txtSendLoc.Text = Session["UserDefLoca"].ToString();
                txtRefNo.Text = "";
                txtItmCode.Text = "";
                txtItmDes.Text = "";
                txtItmModel.Text = "";
                txtPbLvl.Text = "";
                txtProfitCenter.Text = "";
                txtItmQty.Text = "";
                txtUPrice.Text = "";

                txtAppVal.Text = string.Format("{0:n2}", 0);
                txtPriceDef.Text = string.Format("{0:n2}", 0);
                txtCollectVal.Text = string.Format("{0:n2}", 0);
                txtRecVal.Text = string.Format("{0:n2}", 0);

                //Button10.Visible = false;
                ////panel_manualReceipt.Visible = false;
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            // _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {


                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "CS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "CS" + seperator + txtPriceBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        string Loc = Session["UserDefLoca"].ToString().ToUpper();
                        Int32 tp = Convert.ToInt32(ddlTransActionTp.SelectedValue);
                        Int32 sts = ddlAction.SelectedItem.Text == "APPROVE REQUEST" ? 1 : ddlAction.SelectedItem.Text == "CONFIRMATION" ? 3 : -1;
                        if (ddlTransActionTp.SelectedValue == "1")
                        {
                            Loc = Session["UserDefLoca"].ToString();
                        }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Loc + seperator + Session["UserDefProf"].ToString() + seperator + tp + seperator + sts);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "FixAssetRefNo")
                {
                    txtRefNo.Text = code;
                    txtRefNo_TextChanged(null, null);
                }
                else if (_serType == "Item")
                {
                    txtItmCode.Text = code;
                    txtItmCode_TextChanged(null, null);
                }
                else if (_serType == "PriceBook")
                {
                    txtPriceBook.Text = code;
                    txtPriceBook_TextChanged(null, null);
                }
                else if (_serType == "PriceLevel")
                {
                    txtPbLvl.Text = code;
                    txtPbLvl_TextChanged(null, null);
                }
                else if (_serType == "UserLocation")
                {
                    txtLocation.Text = code;
                    txtLocation_TextChanged(null, null);
                }
                else if (_serType == "AllProfitCenters")
                {
                    txtProfitCenter.Text = code;
                    txtProfitCenter_TextChanged(null, null);
                }
                else if (_serType == "ItemStatus")
                {
                    ddlFilterSts.SelectedIndex = ddlFilterSts.Items.IndexOf(ddlFilterSts.Items.FindByValue(code));
                    ddlFilterSts_SelectedIndexChanged(null, null);
                }
                else if (_serType == "SendUserLocation")
                {
                    txtSendLoc.Text = code;
                    txtSendLoc_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                if (_serType == "FixAssetRefNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRefNo);
                    if (cmbSearchbykey.SelectedValue == "Date")
                    {
                        _serData = CHNLSVC.CommonSearch.SEARCH_INT_ADHOC_HDR(para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                    }
                    else
                    {
                        _serData = CHNLSVC.CommonSearch.SEARCH_INT_ADHOC_HDR(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    }

                    dgvResult.DataSource = _serData;
                    foreach (DataRow dr in _serData.Rows)
                    {
                        dr["Date"] = Convert.ToDateTime(dr["Date"].ToString()).Date.ToString("dd/MMM/yyyy");
                    }
                }
                else if (_serType == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "PriceBook")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                    _serData = CHNLSVC.CommonSearch.GetPriceBookData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "PriceLevel")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                    _serData = CHNLSVC.CommonSearch.GetPriceLevelData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "UserLocation")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _serData = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "AllProfitCenters")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "ItemStatus")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                    _serData = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "AllProfitCenters")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "ItemStatus")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                    _serData = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "SendUserLocation")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _serData = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            this.cmbSearchbykey.SelectedIndex = 0;
        }

        protected void lbtnSeRefNo_Click(object sender, EventArgs e)
        {

            _serData = new DataTable();
            string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRefNo);
            _serData = CHNLSVC.CommonSearch.SEARCH_INT_ADHOC_HDR(searchparams, null, null);
            dgvResult.DataSource = new int[] { };
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                foreach (DataRow dr in _serData.Rows)
                {
                    dr["Date"] = Convert.ToDateTime(dr["Date"].ToString()).Date.ToString("dd/MMM/yyyy");
                }
                DataTable tmpDt = new DataTable();
                tmpDt.Columns.Add("Reference");
                tmpDt.Columns.Add("Date");
                tmpDt.Columns.Add("Manual No");
                tmpDt.Columns.Add("Status");
                BindCmbSearchbykey(tmpDt);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "FixAssetRefNo";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // ClearDetails();
                txtStatus.Text = "NEW";
                if (txtRefNo.Text.ToUpper().Trim() != "")
                {
                    List<InventoryAdhocHeader> _list = CHNLSVC.Inventory.GET_INT_ADHOC_HDR(new InventoryAdhocHeader()
                    {
                        Iadh_ref_no = txtRefNo.Text.ToUpper().Trim(),
                        Iadh_stus = -1,
                        Iadh_tp = -1
                    });
                    if (_list != null)
                    {
                        if (_list.Count > 0)
                        {
                            txtStatus.Text = _list[0].Iadh_stus == 0 ? "PENDING-P" : _list[0].Iadh_stus == 1 ? "APPROVED-A" : _list[0].Iadh_stus == 2 ? "REJECTED-R" :
                                _list[0].Iadh_stus == 3 ? "CONFIRMED-C" : "";
                            txtDate.Text = _list[0].Iadh_dt.ToString("dd/MMM/yyyy");
                            txtStatus_TextChanged(null, null);
                            lbtnRefNoOk_Click(null, null);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid ref # !!!')", true);
                        txtRefNo.Text = "";
                        txtRefNo.Focus();
                        return;
                    }
                }

            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void DisableButton()
        {
            lbtnAppAvaSer.Enabled = false;
            lbtnAppAvaSer.CssClass = "buttoncolorleft";
            lbtnAppAvaSer.OnClientClick = "return Enable();";

            lbtnRejAvaSer.Enabled = false;
            lbtnRejAvaSer.CssClass = "buttoncolorleft";
            lbtnRejAvaSer.OnClientClick = "return Enable();";

            lbtnRejAppSer.Enabled = false;
            lbtnRejAppSer.CssClass = "buttoncolorleft";
            lbtnRejAppSer.OnClientClick = "return Enable();";

            lbtnConfAppser.Enabled = false;
            lbtnConfAppser.CssClass = "buttoncolorleft";
            lbtnConfAppser.OnClientClick = "return Enable();";
        }
        protected void lbtnRefNoOk_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtRefNo.Text.Trim() == "")
                {
                    DispMsg("Please enter ref #  !!!", "W"); return;
                }
                if (ddlAction.SelectedItem.Text == "Request")
                {
                    DispMsg("Select valid action  !!!", "W"); return;
                }
                Reset_Session_values();
                Int32 type_ = Convert.ToInt32(ddlTransActionTp.SelectedValue);
                int _staus = 0;
                string ref_no = txtRefNo.Text.Trim().ToUpper();
                InventoryAdhocHeader Header = new InventoryAdhocHeader();
                List<InventoryAdhocDetail> det_list = null;
                string location = Session["UserDefLoca"].ToString().ToUpper();
                //****added on 15-01-2013************************************
                if (type_ == 1)
                {//added by prabhath on 28/12/2013
                    location = Session["UserDefLoca"].ToString();
                }
                if (type_ == 1 && ddlAction.SelectedItem.Text == "CONFIRMATION")
                {
                    _staus = 1;
                }
                else
                {
                    _staus = 0;
                }
                SearchedAdhocDetList = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), location, type_, ref_no, _staus, out Header);// status must be 0
                if (SearchedAdhocDetList != null)
                {
                    if (SearchedAdhocDetList.Count > 0)
                    {
                        var _no = SearchedAdhocDetList.Where(x => x.Iadd_stus == 0).ToList();
                        if (_no != null && _no.Count > 0)
                        {
                            //DispMsg("Request not approved !!!", "W"); ;
                        }
                    }
                }
                if (ddlAction.SelectedItem.Text == "NEW REQUEST")
                {
                    det_list = new List<InventoryAdhocDetail>();
                    det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), location, type_, ref_no, 0, out Header);

                    if (det_list != null)
                    {
                        //  det_list = det_list.Where(c => c.Iadd_stus == 0).ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList.AddRange(det_list);
                        var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                      .Select(g => g.First())
                                      .ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList2.AddRange(distinctList);
                        dgvItmDes.DataSource = new int[] { };
                        if (bind_AdhocDetList2 != null)
                        {
                            if (bind_AdhocDetList2.Count > 0)
                            {
                                dgvItmDes.DataSource = bind_AdhocDetList2;
                            }
                        }
                        dgvItmDes.DataBind();
                    }
                    else
                    {
                        DispMsg("There are no Pending requested items available with this Ref.No !!!", "W");
                        return;
                    }
                }
                else if (ddlAction.SelectedItem.Text == "APPROVE REQUEST")
                {
                    det_list = new List<InventoryAdhocDetail>();
                    det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), location, type_, ref_no, 0, out Header);
                    // det_list = det_list.Where(c => c.Iadd_stus != 0).ToList();
                    if (det_list != null)
                    {
                        List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList.AddRange(det_list);
                        var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                      .Select(g => g.First())
                                      .ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList2.AddRange(distinctList);
                        dgvItmDes.DataSource = new int[] { };
                        if (bind_AdhocDetList2 != null)
                        {
                            if (bind_AdhocDetList2.Count > 0)
                            {
                                dgvItmDes.DataSource = bind_AdhocDetList2;
                            }
                        }
                        dgvItmDes.DataBind();
                        LoadAvailableSerial();
                    }
                    else
                    {
                        DispMsg("There are no Pending requested items available with this Ref.No !!!", "W");
                        return;
                    }
                }
                else if (ddlAction.SelectedItem.Text == "CONFIRMATION") //else if (rdoApproved.Checked)
                {
                    det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), location, type_, ref_no, 1, out Header);
                    //  det_list = det_list.Where(c => c.Iadd_stus != 0 && c.Iadd_stus != 1).ToList();
                    if (Header == null)
                    {
                        DispMsg("Please enter valid Ref # !!!", "W");
                        return;
                    }
                    if (Header.Iadh_stus == 3)
                    {
                        DispMsg(Header.Iadh_ref_no + " is already confirmed !!!", "W");
                        return;
                    }
                    if (det_list != null)
                    {
                        List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList.AddRange(det_list);
                        var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                      .Select(g => g.First())
                                      .ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList2.AddRange(distinctList);
                        dgvItmDes.DataSource = new int[] { };
                        if (bind_AdhocDetList2 != null)
                        {
                            if (bind_AdhocDetList2.Count > 0)
                            {
                                dgvItmDes.DataSource = bind_AdhocDetList2;
                            }
                        }
                        dgvItmDes.DataBind();
                    }
                    else
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no Approved request items available with this Ref.No!");
                        DispMsg("There are no Approved request items available with this Ref # !!!", "W");
                        return;
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select 'Search Status' first!");
                    //DispMsg("Select correct Action please !!!", "W");
                    //ddlAction.Focus();
                    //txtRefNo.Text = "";
                    //return;
                }
                AdhodDetList = det_list;
                Searched_AdhodHeader = Header;
                if (Header != null)
                {
                    if (Searched_AdhodHeader.Iadh_stus == 4)
                    {
                        clearCompleateScreen();
                        DispMsg(Header.Iadh_ref_no + " has been Cancelled !!!", "W");
                        return;
                    }
                    txtSendLoc.Text = Session["UserFixedLoc"].ToString();
                    ddlFilterSts.Enabled = true;

                    //lbtnAppAvaSer.Enabled = false;
                    //lbtnAppAvaSer.CssClass = "buttoncolorleft";
                    //lbtnAppAvaSer.OnClientClick = "return Enable();";

                    //lbtnRejAvaSer.Enabled = false;
                    //lbtnRejAvaSer.CssClass = "buttoncolorleft";
                    //lbtnRejAvaSer.OnClientClick = "return Enable();";

                    lbtnAddItm.Enabled = false;
                    lbtnAddItm.CssClass = "buttoncolorleft";
                    lbtnAddItm.OnClientClick = "return Enable();";

                    if (Header.Iadh_tp == 1)//FGAP
                    {
                        lbtnAddItm.Enabled = true;

                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }
                        txtAppVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        txtCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        txtPriceDef.Text = string.Format("{0:n2}", 0);
                        txtRecVal.Text = string.Format("{0:n2}", 0);


                    }
                    else //Header.Iadh_tp == 2 //FIX ASSET
                    {
                        if (Header.Iadh_stus == 0)
                        {

                        }
                        if (Header.Iadh_stus == 1)
                        {

                        }
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no pending/approved request with this Ref.No!");
                    DispMsg("There are no pending/approved request with this Ref.No !!!", "W");
                    lbtnAddItm.Enabled = true;
                    lbtnAddItm.CssClass = "buttonUndocolorLeft";
                    lbtnAddItm.OnClientClick = "";

                    ddlFilterSts.Enabled = true;

                }

                //--------------load first item's details to the gridS.-------------------------------------
                #region load first item's details to the gridS
                //----------GRID 1
                //AvailableSerialList = new List<ReptPickSerials>();
                Approved_SerialList = new List<ReptPickSerials>();
                if (dgvItmDes.Rows.Count > 0)
                {
                    foreach (GridViewRow row in dgvItmDes.Rows)
                    {

                        Label lblQty = row.FindControl("lblQty") as Label;
                        Label lblSts = row.FindControl("lblSts") as Label;
                        Label lblItmCd = row.FindControl("lblItmCd") as Label;

                        string reqQty = lblQty.Text;
                        string reqStatus = lblSts.Text;
                        SelectedItemCD = lblItmCd.Text;

                        var _dup = from _l in AdhodDetList
                                   where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == Header.Iadh_stus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                                   select _l;

                        List<ReptPickSerials> serList = new List<ReptPickSerials>();
                        serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().ToUpper(), SelectedItemCD, string.Empty);

                        //GetInventorySerialListById
                        foreach (InventoryAdhocDetail det in _dup)
                        {
                            var _dup2 = from _l in serList
                                        where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                                        select _l;
                            // AvailableSerialList.AddRange(_dup2);
                            Approved_SerialList.AddRange(_dup2);
                        }
                        //dgvAvailableSerials.DataSource = new int[] { };
                        //if (AvailableSerialList.Count > 0)
                        //{
                        //    dgvAvailableSerials.DataSource = AvailableSerialList;
                        //}
                        //dgvAvailableSerials.DataBind();
                    }
                }
                dgvApproveItms.DataSource = new int[] { };
                if (Approved_SerialList.Count > 0)
                {
                    dgvApproveItms.DataSource = Approved_SerialList;
                }
                dgvApproveItms.DataBind();

                LoadApproveSerialStatus();
                LoadAvailableSerial();
                DesableBtnAvaSerial();

                #endregion

                if (!CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INV5"))
                {
                    lbtnAppAvaSer.Enabled = false;
                    lbtnAppAvaSer.CssClass = "buttoncolorleft";
                    lbtnAppAvaSer.OnClientClick = "return Enable();";

                    lbtnRejAppSer.Enabled = false;
                    lbtnRejAppSer.CssClass = "buttoncolorleft";
                    lbtnRejAppSer.OnClientClick = "return Enable();";
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeItmCode_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "Item";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private InventoryAdhocDetail fillAdhocDet_FixAsset_Request()
        {
            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                Det.Iadd_anal1 = 0;
                decimal qty = 0;
                if (decimal.TryParse(txtItmQty.Text, out qty))
                {
                    Det.Iadd_anal1 = qty;
                }
                txtItmCode_TextChanged(null, null);
                Det.Iadd_anal2 = txtItmModel.Text.Trim().ToUpper();
                Det.Iadd_anal3 = txtItmDes.Text.Trim().ToUpper();
                //Det.Iadd_anal4 = ;
                //Det.Iadd_anal5 =;
                //Det.Iadd_app_itm =;
                //Det.Iadd_app_pb = ;
                //Det.Iadd_app_pb_lvl =;
                //Det.Iadd_app_val = ;
                Det.Iadd_claim_itm = txtItmCode.Text.Trim().ToUpper();
                Det.Iadd_claim_pb = txtPriceBook.Text.Trim();
                Det.Iadd_claim_pb_lvl = txtPbLvl.Text.Trim();
                //Det.Iadd_claim_val = ;
                //Det.Iadd_coll_itm =;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 0;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "W");
            }
            return Det;
        }
        private InventoryAdhocDetail fillAdhocDet_FGAP_Request()
        {

            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                Det.Iadd_anal1 = 0;
                decimal qty = 0;
                if (decimal.TryParse(txtItmQty.Text, out qty))
                {
                    Det.Iadd_anal1 = qty;
                }
                txtItmCode_TextChanged(null, null);
                Det.Iadd_anal2 = txtItmModel.Text.Trim().ToUpper();
                Det.Iadd_anal3 = txtItmDes.Text.Trim().ToUpper();
                //Det.Iadd_anal4 = ;
                //Det.Iadd_anal5 =;
                Det.Iadd_app_itm = txtItmCode.Text.Trim().ToUpper(); ;
                Det.Iadd_app_pb = txtPriceBook.Text.ToUpper();
                Det.Iadd_app_pb_lvl = txtPbLvl.Text.ToUpper();

                Det.Iadd_claim_itm = txtItmCode.Text.Trim().ToUpper();
                //Det.Iadd_claim_pb =;
                //Det.Iadd_claim_pb_lvl = ;
                //Det.Iadd_claim_val = ;
                //Det.Iadd_coll_itm =;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 1;
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim(), "CS", txtPriceBook.Text, txtPbLvl.Text, string.Empty, txtItmCode.Text,
                    Convert.ToDecimal(txtItmQty.Text), Convert.ToDateTime(txtDate.Text));
                if (_priceDetailRef != null)
                {
                    if (_priceDetailRef.Count > 0)
                    {
                        Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                        txtUPrice.Text = unitPrice.ToString("N2");
                        if (txtUPrice.Text.Trim() != "")
                        {
                            Det.Iadd_app_val = unitPrice;
                            //Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        }
                        else
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                            DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                            return null;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                        return null;
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                    return null;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
                return null;
            }
            return Det;
        }

        protected void lbtnAddItm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItmCode.Text))
                {
                    DispMsg("Please enter item code !!!", "W"); txtItmCode.Focus(); return;
                }
                MasterItem msitem = new MasterItem();
                msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItmCode.Text.Trim().ToUpper());
                if (msitem == null)
                {
                    DispMsg("Invalid Item code!");
                    txtItmModel.Text = "";
                    txtItmDes.Text = "";
                    txtItmCode.Text = "";
                    return;
                }
                //Pasindu 2018-06-06
                DataTable DTS = CHNLSVC.MsgPortal.CheckValidItmInFixAsset(txtItmCode.Text.Trim().ToUpper());

                if (DTS == null || DTS.Rows.Count == 0)
                {
                    DispMsg("This Item is not Available for fixed Asset Please Contact Inventroy Department", "Fixed Asset");
                    txtItmCode.Text = "";
                    txtItmModel.Text = "";
                    txtItmQty.Text = "";
                    return;
                }

                txtItmModel.Text = msitem.Mi_model;
                txtItmDes.Text = msitem.Mi_shortdesc;
                decimal _qty = 0;
                if (!decimal.TryParse(txtItmQty.Text.Trim(), out _qty))
                {
                    DispMsg("Please enter valid qty !!!"); return;
                }

                //if (ddlReuestType.SelectedValue == "2")
                foreach (GridViewRow row in dgvItmDes.Rows)
                {
                    Label lblItmCd = row.FindControl("lblItmCd") as Label;
                    if (lblItmCd.Text.Trim() == txtItmCode.Text)
                    {
                        DispMsg("Cannot add same Item twice !!!");
                        return;
                    }
                }
                if (ddlTransActionTp.SelectedValue == "2")
                {
                    InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                    if (Det != null)
                    {
                        AdhodDetList.Add(Det);
                        dgvItmDes.DataSource = new int[] { };
                        if (AdhodDetList.Count > 0)
                        {
                            dgvItmDes.DataSource = AdhodDetList;
                        }
                        dgvItmDes.DataBind();
                    }
                }
                else if (ddlTransActionTp.SelectedValue == "1")
                {
                    if (txtPriceBook.Text.Trim() == "" || txtPbLvl.Text.Trim() == "")
                    {
                        // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Price Book details.");
                        DispMsg("Please enter price book details !!!");
                        return;
                    }
                    if (txtLocation.Text.Trim() == "")
                    {
                        // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Location.");
                        DispMsg("Please enter location !!!");
                        txtLocation.Focus();
                        return;
                    }
                    MasterLocation LOC = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.Trim().ToUpper());
                    if (LOC == null)
                    {
                        txtLocation.Text = "";
                        DispMsg("Please enter valid location !!!");
                        return;
                    }

                    if (txtProfitCenter.Text.Trim() == "")
                    {
                        //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Profit Center.");
                        DispMsg("Please enter profit center !!!");
                        txtProfitCenter.Focus();
                        return;
                    }
                    DataTable DT = CHNLSVC.General.GetPartyCodes(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper());
                    if (DT == null)
                    {
                        txtProfitCenter.Text = "";
                        DispMsg("Please enter valid profit center !!!");
                        txtProfitCenter.Focus();
                        return;
                    }
                    //InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                    txtSendLoc.Text = txtLocation.Text.Trim();
                    InventoryAdhocDetail Det = fillAdhocDet_FGAP_Request();

                    if (Det != null)
                    {
                        if (AdhodDetList == null)
                        {
                            AdhodDetList = new List<InventoryAdhocDetail>();
                        }
                        AdhodDetList.Add(Det);
                        dgvItmDes.DataSource = new int[] { };
                        if (AdhodDetList.Count > 0)
                        {
                            dgvItmDes.DataSource = AdhodDetList;
                        }
                        dgvItmDes.DataBind();

                        #region
                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }

                        txtCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        Decimal priceDiference = Convert.ToDecimal(txtCollectVal.Text) - Convert.ToDecimal(txtAppVal.Text);
                        txtPriceDef.Text = string.Format("{0:n2}", priceDiference);

                        //lblReceiptAmt.Text = priceDiference.ToString();
                        if (ddlAction.SelectedItem.Text != "Approve")
                        {
                            if (priceDiference > 0)
                            {
                                lbtnConfAppser.Enabled = false;
                                lbtnConfAppser.CssClass = "buttoncolorleft";
                                lbtnConfAppser.OnClientClick = "return Enable();";

                                lbtnPaymentComplete.Enabled = true;
                                txtRecVal.Text = priceDiference.ToString();

                                ucPaymodes.PageClear();
                                ucPaymodes.payModeClear();
                                ucPaymodes.InvoiceType = "CS";
                                //ucPayModes1.LoadPayModes();
                                ucPaymodes.TotalAmount = priceDiference;
                                ucPaymodes.LoadData();

                                //-------------------
                                pnlPayMode.Visible = false;
                                pnlPayDetails.Visible = false;
                                ucPaymodes.Visible = false;
                                //-------------------

                            }
                            else
                            {
                                txtRecVal.Text = string.Format("{0:n2}", 0);

                                lbtnConfAppser.Enabled = true;
                                lbtnConfAppser.CssClass = "buttonUndocolorLeft";
                                lbtnConfAppser.OnClientClick = "return ConfirmConfirm()";

                                lbtnRejAvaSer.Enabled = false;
                                lbtnRejAvaSer.CssClass = "buttoncolorleft";
                                lbtnRejAvaSer.OnClientClick = "return Enable();";

                                ucPaymodes.PageClear();
                                ucPaymodes.payModeClear();
                                ucPaymodes.InvoiceType = "CS";
                                //ucPayModes1.LoadPayModes();
                                ucPaymodes.TotalAmount = 0;
                                ucPaymodes.LoadData();
                                //ucPayModes1.LoadPayModes();

                                pnlPayDetails.Visible = false;
                                pnlPayMode.Visible = false;
                                ucPaymodes.Visible = false;
                            }
                        }
                        else
                        {
                            pnlPayDetails.Visible = false;
                            pnlPayMode.Visible = false;
                            ucPaymodes.Visible = false;
                        }
                        // }
                        #endregion
                        //--------------------------------------------------------------
                    }
                    //else
                    //{

                    //}
                }
                txtItmCode.Text = "";
                txtItmDes.Text = "";
                txtItmModel.Text = "";
                txtPbLvl.Text = "";
                txtPriceBook.Text = "";
                // txtPC.Text = "";
                txtItmQty.Text = "";
                txtUPrice.Text = "";
                //txtProfitCenter.Text = "";
                // txtLocation.Text = "";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }


        protected void txtItmCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtItmModel.Text = "";
                txtItmDes.Text = "";
                if (txtItmCode.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItmCode.Text.Trim().ToUpper());
                    if (msitem == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid item code !!!')", true);
                        txtItmCode.Text = "";
                        txtItmCode.Focus();
                        return;
                    }
                    else
                    {
                        txtItmModel.Text = msitem.Mi_model;
                        txtItmDes.Text = msitem.Mi_shortdesc;
                        if (ddlTransActionTp.SelectedItem.Text == "FGAP")
                        {
                            txtItmQty.Focus();
                        }
                        else
                        {
                            txtItmQty.Focus();
                        }
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtPriceBook_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookData(para, "Price Book", txtPriceBook.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Price Book"].ToString()))
                        {
                            if (txtPriceBook.Text.ToUpper() == row["Price Book"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtPriceBook.ToolTip = toolTip;
                    }
                    else
                    {
                        txtPriceBook.ToolTip = "";
                        txtPriceBook.Text = "";
                        txtPriceBook.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid price book !!!')", true);
                        return;
                    }

                }
                if (txtPbLvl.Text.Trim() == "")
                {
                    txtPbLvl.Text = "";
                    return;
                }

                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim(), "CS", txtPriceBook.Text, txtPbLvl.Text, string.Empty, txtItmCode.Text,
                  string.IsNullOrEmpty(txtItmQty.Text) ? 0 : Convert.ToDecimal(txtItmQty.Text), Convert.ToDateTime(txtDate.Text));
                if (_priceDetailRef != null)
                {
                    if (_priceDetailRef.Count > 0)
                    {
                        Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                        txtUPrice.Text = unitPrice.ToString("N2");
                        if (string.IsNullOrEmpty(txtUPrice.Text))
                        {

                            DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                            txtUPrice.Text = "";
                            return;
                        }
                        LoadPriceDefaultValue();
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                        txtUPrice.Text = "";
                        return;
                    }
                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    DispMsg("Prices not defined for this item in this PriceBook and Level!", "W");
                    txtUPrice.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing!!!", "E");
            }
        }

        protected void txtPbLvl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPbLvl.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelData(para, "Price Book", txtPbLvl.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Price Level"].ToString()))
                        {
                            if (txtPbLvl.Text.ToUpper() == row["Price Level"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtPbLvl.ToolTip = toolTip;
                    }
                    else
                    {
                        txtPbLvl.ToolTip = "";
                        txtPbLvl.Text = "";
                        txtPbLvl.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid price book level !!!')", true);
                        return;
                    }

                }
                if (txtPbLvl.Text.Trim() == "")
                {
                    txtPbLvl.Text = "";
                    return;
                }
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim(), "CS", txtPriceBook.Text, txtPbLvl.Text, string.Empty, txtItmCode.Text, txtItmQty.Text == "" ? 1 : Convert.ToDecimal(txtItmQty.Text), Convert.ToDateTime(txtDate.Text));
                if (_priceDetailRef != null)
                {
                    if (_priceDetailRef.Count > 0)
                    {
                        Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                        txtUPrice.Text = unitPrice.ToString("N2");
                        if (txtUPrice.Text.Trim() == "")
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                            DispMsg("Prices not defined for this item in this PriceBook and Level !!!");
                            txtUPrice.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        DispMsg("Prices not defined for this item in this PriceBook and Level !!!");
                        txtUPrice.Text = "";
                        return;
                    }
                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    DispMsg("Prices not defined for this item in this PriceBook and Level !!!");
                    txtUPrice.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...", "E");
            }
        }

        protected void ddlTransActionTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlSendLoc.Visible = ddlTransActionTp.SelectedItem.Text == "FGAP" ? false : true;
                lblRefNo.Text = ddlTransActionTp.SelectedItem.Text == "FGAP" ? "Manual #" : "Ref #";
                ClearDetails();
                clearCompleateScreen();
                //----------view states--------------------------
                Reset_Session_values();
                //-----------------------------------------------
                txtRefNo.Text = "";
                BindAction();

                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS")
                {
                    //txtSendLoc.Text = Session["UserDefLoca"].ToString();
                    pnlSendLoc.Visible = true;
                    pnlPriceDetails.Visible = false;
                    // div_payment.Visible = false;
                    // pnlPayss.Enabled = false;
                    pnlPayMode.Visible = false;
                    pnlPayDetails.Visible = false;
                    ucPaymodes.Visible = false;

                    if (txtStatus.Text != "PENDING-P")
                    {
                        lbtnAppAvaSer.Enabled = true;
                        lbtnAppAvaSer.CssClass = "buttonUndocolorLeft";
                        lbtnAppAvaSer.OnClientClick = "return ConfirmApprove()";

                        lbtnRejAvaSer.Enabled = true;
                        lbtnRejAvaSer.CssClass = "buttonUndocolorLeft";
                        lbtnRejAvaSer.OnClientClick = "return ConfirmReject()";
                    }

                    lbtnSave.Visible = true;
                    lbtnSendReq.Visible = false;

                    //------------------------------------------------------
                    ddlAction.SelectedIndex = 0;//uncommented
                }
                else
                {
                    txtSendLoc.Text = "";
                    pnlPriceDetails.Visible = true;
                    lbtnSendReq.Visible = true;
                    lbtnSave.Visible = false;


                    //pnlPayss.Visible = true;
                    //panel_payment.Visible = true;
                    //ucPayModes1.Visible = true;
                    pnlPayMode.Visible = false;
                    pnlPayDetails.Visible = false;
                    ucPaymodes.Visible = false;

                    //------------------------------------------------------
                    ddlAction.SelectedIndex = 0;//uncommented


                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        //private void grvItmDes_CellClick(object sender, DataGridViewCellEventArgs e)

        // private void deleteItem(DataGridViewRow selectedRow)

        protected void lbtnAddAvaSer_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    foreach (DataGridViewRow gvr in this.grvAvailableSerials.Rows)
            //    {
            //        // CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect1");

            //        DataGridViewCheckBoxCell chk = gvr.Cells["chekSelect1"] as DataGridViewCheckBoxCell;

            //        //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //        if (Convert.ToBoolean(chk.Value) == true)
            //        {
            //            // Label lblSerID = (Label)gvr.Cells[4].FindControl("lblSerID_av");//Convert.ToInt32(gvr.Cells[4].FindControl("lblSerID_av"));
            //            string lblSerID = gvr.Cells["lblSerID_av"].Value.ToString();
            //            Int32 serID = Convert.ToInt32(lblSerID);
            //            string ItemCD = gvr.Cells["Tus_itm_cd"].Value.ToString();
            //            List<ReptPickSerials> serList = new List<ReptPickSerials>();

            //            var _dup = from _l in AvailableSerialList
            //                       where _l.Tus_itm_cd == ItemCD && _l.Tus_ser_id == serID
            //                       select _l;


            //            // serList= _dup.ToList<ReptPickSerials>();
            //            Approved_SerialList.AddRange(_dup);

            //            AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle


            //        }

            //        //serList = List<ReptPickSerials> (_dup);
            //    }
            //    grvApproveItms.DataSource = null;
            //    grvApproveItms.AutoGenerateColumns = false;
            //    // grvApproveItms.DataSource = Approved_SerialList;
            //    BindingSource _source = new BindingSource();
            //    _source.DataSource = Approved_SerialList;
            //    grvApproveItms.DataSource = _source;
            //    //grvApproveItms.DataBind();


            //    grvAvailableSerials.DataSource = null;
            //    grvAvailableSerials.AutoGenerateColumns = false;
            //    //grvAvailableSerials.DataSource = AvailableSerialList;
            //    BindingSource _source2 = new BindingSource();
            //    _source2.DataSource = AvailableSerialList;
            //    grvAvailableSerials.DataSource = _source2;
            //    //    //grvAvailableSerials.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
        }

        //private void btnBack_Click
        //private void grvApproveItms_CellClick(object sender, DataGridViewCellEventArgs e)
        protected void lbtnRejAvaSer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    DispMsg("Please send the request first!");
                    return;
                }
                //Searched_AdhodHeader.Iadh_coll_by = GlbUserID;
                //Searched_AdhodHeader.Iadh_coll_dt = 
                Searched_AdhodHeader.Iadh_stus = 2;
                Searched_AdhodHeader.Iadh_app_by = Session["UserID"].ToString(); //rejected person
                Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                if (effect != -1)
                {
                    DispMsg("Request Rejected Successfully! Reference # : " + Searched_AdhodHeader.Iadh_ref_no, "S");
                    clearCompleateScreen();
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");

                    ClearData();
                    return;
                }
                else
                {
                    DispMsg("Not Rejected!");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...", "E");
            }
        }
        protected void lbtnAppAvaSer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    DispMsg("Please send the request first!");
                    return;
                }
                // string _OrgPC = txtPC.Text.Trim();
                if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INV5"))
                {

                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                    DispMsg("No Permission Granted! \n (required permission type: 'INV5')");
                    return;
                }
                //-----------------------------------------------------------------------------------------
                if (Approved_SerialList.Count > 0)
                {
                    List<InventoryAdhocDetail> approved_detList = new List<InventoryAdhocDetail>();
                    //Approve the requested items.
                    InventoryAdhocDetail Det = null;
                    foreach (ReptPickSerials rps in Approved_SerialList)
                    {
                        #region fill Approved detail

                        Det = new InventoryAdhocDetail();
                        //Det.Iadd_anal1 = 1; //not sure
                        // Decimal TotalAppr = searchedAdhocDetList.Find(x => x.Iadd_claim_itm == rps.Tus_itm_cd);                  
                        foreach (InventoryAdhocDetail adhocDet in SearchedAdhocDetList)
                        {
                            if (adhocDet.Iadd_claim_itm == rps.Tus_itm_cd)
                            {
                                Det.Iadd_anal1 = adhocDet.Iadd_anal1;
                            }

                        }
                        // Det.Iadd_anal1 = Convert.ToDecimal(rps.Tus_new_remarks);//the qty requested/approved


                        Det.Iadd_anal2 = rps.Tus_itm_model;
                        Det.Iadd_anal3 = rps.Tus_itm_desc; ;
                        Det.Iadd_anal4 = rps.Tus_ser_id;
                        //Det.Iadd_anal5 =;
                        Det.Iadd_app_itm = rps.Tus_itm_cd;
                        Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                        Det.Iadd_app_pb_lvl = txtPbLvl.Text.Trim().ToUpper();
                        if (txtUPrice.Text.Trim() != "")
                        {
                            Det.Iadd_app_val = Convert.ToDecimal(txtUPrice.Text.Trim());
                        }

                        Det.Iadd_claim_itm = rps.Tus_itm_cd;
                        //Det.Iadd_claim_pb =;
                        //Det.Iadd_claim_pb_lvl = ;
                        //Det.Iadd_claim_val = ;
                        //Det.Iadd_coll_itm =;
                        //Det.Iadd_coll_pb = ;
                        //Det.Iadd_coll_pb_lvl = ;
                        //Det.Iadd_coll_ser1 = ;
                        //Det.Iadd_coll_ser2 = ;,
                        //Det.Iadd_coll_ser3 = ;
                        //Det.Iadd_coll_ser3 = ;
                        //Det.Iadd_coll_val = ;
                        Det.Iadd_line = ItmLine++;
                        //Det.Iadd_ref_no =;
                        //Det.Iadd_seq = ;
                        Det.Iadd_stus = 1;
                        #endregion

                        approved_detList.Add(Det);
                    }

                    //try {
                    //Update header
                    if (Searched_AdhodHeader == null)
                    {
                        Searched_AdhodHeader = new InventoryAdhocHeader();
                    }
                    Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    Searched_AdhodHeader.Iadh_app_by = Session["UserID"].ToString();
                    Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    Searched_AdhodHeader.Iadh_stus = 1;
                    //call approve method
                    if (approved_detList.Count < 1)
                    {
                        DispMsg("Add serials to Selected Serial List!");
                        return;
                    }

                    Int32 effect = 0;
                    effect = CHNLSVC.Inventory.Save_Adhoc_Approve(Searched_AdhodHeader, approved_detList);
                    if (effect < 0)
                    {
                        clearCompleateScreen();
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Approve. Error occured!");
                        DispMsg("Failed to Approve. Error occured!");
                        return;
                    }
                    else if (effect > 0)
                    {

                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approved Successfully!");
                        DispMsg("Request Approved Successfully ! Reference # : " + Searched_AdhodHeader.Iadh_ref_no, "S");
                        clearCompleateScreen();
                        ClearDetails();
                        txtRefNo.Text = "";
                        ClearData();
                    }
                }
                else
                {
                    DispMsg("Please add available serials !!!");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...", "E");
            }
        }

        protected void lbtnRejAppSer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    DispMsg("Please send the request first!");
                    return;
                }

                //Searched_AdhodHeader.Iadh_coll_by = GlbUserName;
                //Searched_AdhodHeader.Iadh_coll_dt =
                Searched_AdhodHeader.Iadh_stus = 2;
                Searched_AdhodHeader.Iadh_app_by = Session["UserID"].ToString();  //rejected person
                Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                if (effect != -1)
                {
                    DispMsg("Request Rejected Successfully! Reference # : " + Searched_AdhodHeader.Iadh_ref_no, "S");
                    clearCompleateScreen();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");

                    ClearData();
                    return;
                }
                else
                {
                    DispMsg("Not Rejected!");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void txtItmQty_TextChanged(object sender, EventArgs e)
        {
            //txtQty_KeyPress
            Int32 qty = 0;
            if (!Int32.TryParse(txtItmQty.Text.Trim(), out qty))
            {
                DispMsg("Please enter valid Qty !!!"); txtItmQty.Text = ""; return;
            }
            if (pnlPriceDetails.Visible == false)
            {
                if (lbtnAddItm.Enabled)
                {
                    //lbtnAddItm_Click(null, null);
                }
            }
            else
            {
                txtPriceBook.Focus();
            }
        }
        //txtPBLevel_KeyPress
        //txtFgapLoc_KeyPress
        //txtPC_KeyPress
        protected void lbtnConfAppser_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    DispMsg("Please send the request first!");
                    return;
                }

                if (Approved_SerialList.Count < 1)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Confirm List!");
                    DispMsg("Please add serials to Confirm List!");
                    return;
                }
                //--------------------23-01-2013-------------------------------**
                Decimal selectedQty = Approved_SerialList.Count;
                Decimal requestQty = 0;

                foreach (GridViewRow gvr in this.dgvItmDes.Rows)
                {
                    Label lblQty = gvr.FindControl("lblQty") as Label;
                    string reqQty = lblQty.Text;
                    requestQty = Convert.ToDecimal(reqQty) + requestQty;
                }
                if (selectedQty < requestQty && Searched_AdhodHeader.Iadh_tp == 2)
                {

                    //if (MessageBox.Show("Request item count is less than Selected item count. \n Do you want to proceed?", "Confirm save", MessageBoxButtons.YesNo) == DialogResult.No)
                    //{
                    //    return;
                    //}

                }
                else if (selectedQty > requestQty && Searched_AdhodHeader.Iadh_tp == 2)
                {
                    DispMsg("Cannot confirm more than requested Qty.");
                    return;
                }


                //-------------------------------------------------------------**

                List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
                //Approve the requested items.
                InventoryAdhocDetail Det = null;
                foreach (ReptPickSerials rps in Approved_SerialList)
                {
                    #region fill Confirm detail

                    Det = new InventoryAdhocDetail();
                    Det.Iadd_anal1 = 1; //not sure

                    Det.Iadd_anal2 = rps.Tus_itm_model;
                    Det.Iadd_anal3 = rps.Tus_itm_desc;
                    Det.Iadd_anal4 = rps.Tus_ser_id;
                    //Det.Iadd_anal5 =;
                    Det.Iadd_app_itm = rps.Tus_itm_cd;
                    Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                    Det.Iadd_app_pb_lvl = txtPbLvl.Text.Trim().ToUpper();
                    if (txtUPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = Convert.ToDecimal(txtUPrice.Text.Trim());
                    }

                    Det.Iadd_claim_itm = rps.Tus_itm_cd;
                    //Det.Iadd_claim_pb =;
                    //Det.Iadd_claim_pb_lvl = ;
                    //Det.Iadd_claim_val = ;
                    Det.Iadd_coll_itm = rps.Tus_itm_cd;
                    //Det.Iadd_coll_pb = ;
                    //Det.Iadd_coll_pb_lvl = ;
                    //Det.Iadd_coll_ser1 = ;
                    //Det.Iadd_coll_ser2 = ;,
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_val = ;
                    Det.Iadd_line = ItmLine++;
                    //Det.Iadd_ref_no =;
                    //Det.Iadd_seq = ;
                    Det.Iadd_stus = 3;
                    #endregion

                    confirmed_detList.Add(Det);
                }

                //**********24-01-2013*****************
                if (Searched_AdhodHeader.Iadh_tp == 1)
                {
                    Decimal totalVal = 0;
                    Decimal selectedTotVal = 0;
                    foreach (InventoryAdhocDetail detail in AdhodDetList)
                    {
                        totalVal = totalVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                    }
                    foreach (ReptPickSerials rps in Approved_SerialList)
                    {
                        selectedTotVal = selectedTotVal + rps.Tus_unit_price;
                    }
                    if (totalVal < selectedTotVal)
                    {
                        DispMsg("Cannot collect more than approved amount!");
                        return;
                    }
                }
                //***************************
                //try {
                //Update header
                // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
                // Searched_AdhodHeader.Iadh_app_dt =
                // Searched_AdhodHeader.Iadh_stus = 1;
                Searched_AdhodHeader.Iadh_coll_by = Session["UserID"].ToString();
                Searched_AdhodHeader.Iadh_coll_dt = CHNLSVC.Security.GetServerDateTime().Date;
                Searched_AdhodHeader.Iadh_stus = 3;

                #region ADJ(-)
                //string AdjNumber = txtAdjustmentNo.Text.Trim();
                //string AdjNumber = "";
                //string manualNum = txtManualRefNo.Text.Trim();
                //string remarks = txtRemarks.Text.Trim();
                //string adj_base = ddlAdjBased.SelectedValue;
                //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
                //string adj_type = ddlAdjType_.SelectedValue;
                InventoryHeader inHeader = new InventoryHeader();


                //inHeader.Ith_acc_no = "";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = "";
                //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeader.Ith_channel = "";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();

                //inHeader.Ith_com ="";
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = "";
                inHeader.Ith_cre_when = DateTime.MinValue;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";

                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";


                inHeader.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeader.Ith_doc_date = DateTime.Today;
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                inHeader.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeader.Ith_doc_year = DateTime.Today.Year;
                inHeader.Ith_entry_no = "";
                inHeader.Ith_entry_tp = "";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = "";
                inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true;
                inHeader.Ith_job_no = "";
                inHeader.Ith_loading_point = "";
                inHeader.Ith_loading_user = "";
                //inHeader.Ith_loc = Session["UserDefLoca"].ToString(); - commented by Prabhath on 19022014 and added txtSendLoc.Text.trim
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                //{
                //    inHeader.Ith_manual_ref = "N/A";
                //}
                //else
                //{
                //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                //}
                inHeader.Ith_manual_ref = "N/A";
                // inHeader.Ith_manual_ref ="";
                inHeader.Ith_mod_by = Session["UserID"].ToString();//"ADMIN";
                inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 1;
                inHeader.Ith_oth_loc = "";

                inHeader.Ith_remarks = "ADHOC CONFIRM";//txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeader.Ith_sbu = "INV";
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                //inHeader.Ith_seq_no =54;
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeader.Ith_vehi_no = "";

                inHeader.Ith_direct = false;

                //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_git_close = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_oth_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_oth_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_sub_tp = "SYS";
                inHeader.Ith_entry_tp = "SYS";
                inHeader.Ith_acc_no = "FASET_ADH_ADJ"; //Edit Chamal 15-05-2013
                inHeader.Ith_cate_tp = Searched_AdhodHeader.Iadh_tp == 2 ? "FIXED" : "FGAP";
                inHeader.Ith_remarks = "ADHOC CONFIRM";
                //------------------------------------------


                //*********************************
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;

                //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                #endregion
                //------------------------------------------------------------

                #region ADJ(+)
                //string AdjNumber = txtAdjustmentNo.Text.Trim();
                //string AdjNumber = "";
                //string manualNum = txtManualRefNo.Text.Trim();
                //string remarks = txtRemarks.Text.Trim();
                //string adj_base = ddlAdjBased.SelectedValue;
                //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
                //string adj_type = ddlAdjType_.SelectedValue;
                InventoryHeader inHeaderplus = new InventoryHeader();


                //inHeader.Ith_acc_no = "";
                inHeaderplus.Ith_anal_1 = "";
                inHeaderplus.Ith_anal_10 = false;
                inHeaderplus.Ith_anal_11 = false;
                inHeaderplus.Ith_anal_12 = false;
                inHeaderplus.Ith_anal_2 = "";
                inHeaderplus.Ith_anal_3 = "";
                inHeaderplus.Ith_anal_4 = "";
                inHeaderplus.Ith_anal_5 = "";
                inHeaderplus.Ith_anal_6 = 0;
                inHeaderplus.Ith_anal_7 = 0;
                inHeaderplus.Ith_anal_8 = DateTime.MinValue;
                inHeaderplus.Ith_anal_9 = DateTime.MinValue;
                inHeaderplus.Ith_bus_entity = "";
                //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeaderplus.Ith_channel = "";
                inHeaderplus.Ith_com = Session["UserCompanyCode"].ToString();

                //inHeader.Ith_com ="";
                inHeaderplus.Ith_com_docno = "";
                inHeaderplus.Ith_cre_by = "";
                inHeaderplus.Ith_cre_when = DateTime.MinValue;
                inHeaderplus.Ith_del_add1 = "";
                inHeaderplus.Ith_del_add2 = "";
                inHeaderplus.Ith_del_code = "";

                inHeaderplus.Ith_del_party = "";
                inHeaderplus.Ith_del_town = "";


                inHeaderplus.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeaderplus.Ith_doc_date = DateTime.Today;
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeaderplus.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                inHeaderplus.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeaderplus.Ith_doc_year = DateTime.Today.Year;
                inHeaderplus.Ith_entry_no = "";
                inHeaderplus.Ith_entry_tp = "";
                inHeaderplus.Ith_git_close = true;
                inHeaderplus.Ith_git_close_date = DateTime.MinValue;
                inHeaderplus.Ith_git_close_doc = "";
                inHeaderplus.Ith_isprinted = true;
                inHeaderplus.Ith_is_manual = true;
                inHeaderplus.Ith_job_no = "";
                inHeaderplus.Ith_loading_point = "";
                inHeaderplus.Ith_loading_user = "";
                //inHeader.Ith_loc = Session["UserDefLoca"].ToString(); - commented by Prabhath on 19022014 and added txtSendLoc.Text.trim
                inHeaderplus.Ith_loc = txtSendLoc.Text.Trim();
                //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                //{
                //    inHeader.Ith_manual_ref = "N/A";
                //}
                //else
                //{
                //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                //}
                inHeaderplus.Ith_manual_ref = "N/A";
                // inHeader.Ith_manual_ref ="";
                inHeaderplus.Ith_mod_by = Session["UserID"].ToString();//"ADMIN";
                inHeaderplus.Ith_mod_when = DateTime.MinValue;
                inHeaderplus.Ith_noofcopies = 1;
                inHeaderplus.Ith_oth_loc = "";

                inHeaderplus.Ith_remarks = "ADHOC CONFIRM";//txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeaderplus.Ith_sbu = "INV";
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                //inHeader.Ith_seq_no =54;
                inHeaderplus.Ith_session_id = Session["SessionID"].ToString();
                inHeaderplus.Ith_stus = "A";
                inHeaderplus.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeaderplus.Ith_vehi_no = "";

                inHeaderplus.Ith_direct = false;

                //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                inHeaderplus.Ith_anal_12 = false;
                inHeaderplus.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                inHeaderplus.Ith_cre_by = Session["UserID"].ToString();
                inHeaderplus.Ith_noofcopies = 0;
                inHeaderplus.Ith_isprinted = false;
                inHeaderplus.Ith_git_close = false;
                inHeaderplus.Ith_is_manual = false;
                inHeaderplus.Ith_oth_loc = Session["UserDefLoca"].ToString();
                inHeaderplus.Ith_oth_com = Session["UserCompanyCode"].ToString();
                inHeaderplus.Ith_sub_tp = "SYS";
                inHeaderplus.Ith_entry_tp = "SYS";
                inHeaderplus.Ith_acc_no = "FASET_ADH_ADJ"; //Edit Chamal 15-05-2013
                inHeaderplus.Ith_cate_tp = Searched_AdhodHeader.Iadh_tp == 2 ? "FIXED" : "FGAP";
                inHeaderplus.Ith_remarks = "ADHOC CONFIRM";
                //------------------------------------------

                //*********************************
                MasterAutoNumber masterAutoadjp = new MasterAutoNumber();
                masterAutoadjp.Aut_cate_cd = txtSendLoc.Text.ToString();
                masterAutoadjp.Aut_cate_tp = "LOC";
                masterAutoadjp.Aut_direction = null;
                masterAutoadjp.Aut_modify_dt = null;
                masterAutoadjp.Aut_moduleid = "ADJ";
                masterAutoadjp.Aut_number = 5;//what is Aut_number
                masterAutoadjp.Aut_start_char = "ADJ";
                masterAutoadjp.Aut_year = null;
                //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                #endregion




                if (Searched_AdhodHeader.Iadh_tp == 2)
                {
                    #region
                    string location = txtSendLoc.Text.Trim().ToUpper();
                    InventoryAdhocHeader Header;
                    List<InventoryAdhocDetail> det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), location, 2, txtRefNo.Text.Trim(), 0, out Header);

                    foreach (InventoryAdhocDetail invdet in AdhodDetList)
                    {
                        var _dup = from _l in confirmed_detList
                                   where _l.Iadd_claim_itm == invdet.Iadd_app_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                                   select _l;
                        Decimal count_CONF = _dup.Count();

                        Decimal count_REQ = 0;

                        //var _dup2 = from _l in det_list
                        //            where _l.Iadd_claim_itm == invdet.Iadd_claim_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                        //            select _l;
                        //count_REQ = _dup2.Count();
                        foreach (InventoryAdhocDetail req_det in SearchedAdhocDetList)
                        {
                            if (req_det.Iadd_claim_itm == invdet.Iadd_app_itm)
                            {
                                count_REQ = req_det.Iadd_anal1;
                            }
                        }


                        if (count_CONF > count_REQ)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Exceed the request item Qty.!");
                            DispMsg("Cannot Exceed the request item Qty.!");
                            return;
                        }
                    }


                    //call confirm method                

                    string AdjNumber = string.Empty;
                    string AdjNumberPlus = string.Empty;
                    string binno = "";
                    DataTable test = null;
                    List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                    Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);

                    test = CHNLSVC.Inventory.getBinLocation(Session["UserCompanyCode"].ToString(), txtSendLoc.Text.ToString());
                    if (test.Rows.Count > 0) {
                        foreach (DataRow row1 in test.Rows)
                        {
                            if (!string.IsNullOrEmpty(row1[0].ToString()))
                            {
                                binno = row1[0].ToString();
                            }
                        }

                    }
                    List<ReptPickSerials> Approved_SerialList_New = Approved_SerialList;

                    foreach (ReptPickSerials item in Approved_SerialList_New) {
                        item.Tus_bin = binno;
                    }

                    Int16 adjEffectPlus = CHNLSVC.Inventory.ADJPlus_FIXA(inHeaderplus, Approved_SerialList_New, rps_subList, masterAutoadjp, out AdjNumberPlus);
                    Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                    Int32 effect = 0;
                    effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                    if (effect < 0 || adjEffectPlus < 0)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                        DispMsg("Failed to Confirmed. Error occured. Try Again!");
                    }
                    else if (effect > 0 && adjEffectPlus > 0)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully!  ADJ(-) NO. =" + AdjNumber);
                        DispMsg("Request Confirmed Successfully! ADJ(+) NO." + AdjNumberPlus + "ADJ(-) NO. :" + AdjNumber, "S");
                        ClearData();
                        try
                        {
                            //Session["GlbReportType"] = "";
                            //Session["OrNo"] = Searched_AdhodHeader.Iadh_ref_no;

                            //Session["GlbReportName"] = "FixedAssetConfirmationNotes.rpt";
                            //BaseCls.GlbReportHeading = "Fixed Asset Confirmation Report";
                            //Session["GlbReportName"] = "FixedAssetConfirmationNotes.rpt";
                            //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                        clearCompleateScreen();
                        return;
                    }

                    //}
                    //catch(Exception ex){
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured!");
                    //    return;
                    //}
                    #endregion
                }
                if (Searched_AdhodHeader.Iadh_tp == 1)
                {
                    //if (lblReceiptAmt.Text != "0")              
                    if (Convert.ToDecimal(txtRecVal.Text) != 0)
                    {
                        if (Convert.ToDecimal(txtRecVal.Text) > 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please do payments!");
                            DispMsg("Please do payments!");
                            //btnConfirm.Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        //call confirm method

                        string AdjNumber = string.Empty;
                        List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                        Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                        Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                        //added for printing
                        Gen_ADJ_DocNo = AdjNumber;

                        Int32 effect = 0;
                        effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                        if (effect < 0)
                        {
                            // MasterMsgInfoUCtrl .SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                            DispMsg("Failed to Confirmed. Error occured. Try Again!");
                            return;
                        }
                        else if (effect > 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. = " + AdjNumber);
                            DispMsg("Request Confirmed Successfully! ADJ(-) NO. :" + AdjNumber, "S");
                            ClearData();
                            try
                            {
                                //Session["GlbReportType"] = "";
                                //Session["OrNo"] = AdjNumber;

                                //Session["GlbReportName"] = "FixedAssetConfirmationNotes.rpt";
                                //BaseCls.GlbReportHeading = "Fixed Asset Confirmation Report";
                                //Session["GlbReportName"] = "FixedAssetConfirmationNotes.rpt";
                                //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                            try
                            {
                                //Session["GlbReportType"] = "";
                                //Session["OrNo"] = AdjNumber;

                                //Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                                //BaseCls.GlbReportHeading = "Fixed Asset Confirmation Report";
                                //Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                                //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                            }
                            catch (Exception ex)
                            {
                                DispMsg("Error in print document.");
                            }
                            clearCompleateScreen();

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void lbtnPaymentComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucPaymodes.Balance > 0)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment is not done!");
                    DispMsg("Payments is not compleate! \n Please settle the balance.");
                    return;
                }
                _recieptItem = ucPaymodes.RecieptItemList;
                if (_recieptItem == null)
                {
                    return;
                }
                if (Approved_SerialList == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                    DispMsg("Please add serials to Selected list!");
                    return;
                }
                if (Approved_SerialList.Count < 1)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                    DispMsg("Please add serials to Selected list!");
                    return;

                }
                //if (checkManualReceipt.Checked == true && txtManualReceiptNo.Text.Trim()=="")
                //{
                //    MessageBox.Show("Please Enter Manual receipt No!");
                //    return;
                //}
                //else
                //{
                //    txtManualReceiptNo.Text = "";
                //}
                //if (MessageBox.Show("Are you sure you want to Compleate?", "Confirm Compleate", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                //Decimal totalApprovedVal = 0;
                //foreach (InventoryAdhocDetail detail in AdhodDetList)
                //{
                //    totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                //}
                //if (totalApprovedVal != Convert.ToDecimal(lblCollectVal.Text))
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected List!");
                //    return;             
                //}

                //*****************************************************************************************************************
                //Decimal count=0;
                //foreach (DataRow dr in grvItmDes.Rows)
                //{
                //    count = count + Convert.ToDecimal(dr["Iadd_anal1"].ToString());
                //}
                //Decimal count2 = grvApproveItms.Rows.Count;
                //if (count != count2)
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                //    return;
                //}
                //if (BalanceAmount <= 0 )
                //{

                #region Receipt Header Value Assign
                RecieptHeader _recHeader = new RecieptHeader();
                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                //_recHeader.Sar_acc_no = lblAccNo.Text;

                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = Session["UserID"].ToString();
                _recHeader.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;
                //_recHeader.Sar_currency_cd = txtCurrency.Text;
                //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
                //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
                //_recHeader.Sar_debtor_cd = txtCustomer.Text;
                //_recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                //if (rdoBtnManager.Checked)
                //{
                //    _recHeader.Sar_is_mgr_iss = true;
                //}
                //else { _recHeader.Sar_is_mgr_iss = false; }
                _recHeader.Sar_is_oth_shop = false;// Not sure!
                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                //if (GlbUserDefProf != ddl_Location.SelectedValue)
                //{
                //    _recHeader.Sar_is_oth_shop = true;// Not sure!
                //    _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                //    _recHeader.Sar_oth_sr = ddl_Location.SelectedValue;
                //}
                //else
                //{
                //    _recHeader.Sar_is_oth_shop = false; // Not sure!
                //    _recHeader.Sar_remarks = "COLLECTION";
                //}

                _recHeader.Sar_is_used = false;//////////////////////TODO
                //  _recHeader.Sar_manual_ref_no = txtManualReceiptNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = Session["UserID"].ToString();
                _recHeader.Sar_mod_when = CHNLSVC.Security.GetServerDateTime().Date;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                //_recHeader.Sar_prefix = ddlPrefix.SelectedValue;

                _recHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader.Sar_receipt_date = CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                // _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                //if (rdoBtnManual.Checked)
                //{
                //    _recHeader.Sar_receipt_type = "HPRM";
                //}
                //else { _recHeader.Sar_receipt_type = "HPRS"; }
                _recHeader.Sar_receipt_type = "FGAP";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = "FGAP RECEIPT";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = Session["SessionID"].ToString();
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtRecVal.Text), 2);

                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;//////////////////////TODO
                _recHeader.Sar_wht_rate = 0;
                //if (checkManualReceipt.Checked == true)
                //{
                //    _recHeader.Sar_anal_3 = "MANUAL";
                //    _recHeader.Sar_anal_8 = 1;
                //    _recHeader.Sar_prefix = "AUTO";
                //}
                //else
                //{
                //    _recHeader.Sar_anal_3 = "SYSTEM";
                //    _recHeader.Sar_anal_8 = 0;
                //    _recHeader.Sar_prefix = "AUTO";
                //}
                //_recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                //_recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);

                //_recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                #endregion
                #region Receipt Details creation
                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                // receiptHeaderList = Receipt_List;
                receiptHeaderList.Add(_recHeader);
                List<RecieptItem> receipItemList = new List<RecieptItem>();
                receipItemList = _recieptItem;
                List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                Int32 tempHdrSeq = 0;
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                    fill_Transactions(_h);
                    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                    foreach (RecieptItem _i in receipItemList)
                    {
                        if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********
                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            //--------------------------------
                            save_receipItemList.Add(ri);

                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                            _i.Sard_settle_amt = 0;

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            //--------------------------------
                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


                }
                //  gvPayment.DataSource = save_receipItemList;
                //  gvPayment.DataBind();
                #endregion

                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "FGAP";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_start_char = "FGAP";
                //Fill the Aut_start_char at the saving place (in BLL)
                //if (_h.Sar_receipt_type=="HPRS")
                //{ _receiptAuto.Aut_start_char = "HPRS"; }
                //else if (_h.Sar_receipt_type == "HPRM")
                //{ _receiptAuto.Aut_start_char = "HPRM"; }
                //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _receiptAuto.Aut_year = null;
                #endregion

                #region Transaction AutoNumber Value Assign
                MasterAutoNumber _transactionAuto = new MasterAutoNumber();
                _transactionAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                // _transactionAuto.Aut_cate_tp = "PC";//change this to GlbUserDefProf
                _transactionAuto.Aut_cate_tp = "PC";//GlbUserDefProf;
                _transactionAuto.Aut_direction = 1;
                _transactionAuto.Aut_modify_dt = null;
                _transactionAuto.Aut_moduleid = "FGAP";
                _transactionAuto.Aut_number = 0;
                _transactionAuto.Aut_start_char = "FGAP";
                _transactionAuto.Aut_year = null;
                #endregion

                Transaction_List = new List<HpTransaction>();
                fill_Transactions(_recHeader);

                //------------------------------------------------------------------------------------------------------
                //CHNLSVC.Inventory.Save_FGAP_confirmation
                //call confirm method

                //****************************************************************************************************************
                List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
                //Approve the requested items.
                InventoryAdhocDetail Det = null;

                foreach (ReptPickSerials rps in Approved_SerialList)
                {

                    #region fill Confirm detail

                    Det = new InventoryAdhocDetail();
                    Det.Iadd_anal1 = 1; //not sure

                    Det.Iadd_anal2 = rps.Tus_itm_model;
                    Det.Iadd_anal3 = rps.Tus_itm_desc;
                    Det.Iadd_anal4 = rps.Tus_ser_id;
                    //Det.Iadd_anal5 =;
                    Det.Iadd_app_itm = rps.Tus_itm_cd;
                    // Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                    // Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                    if (txtUPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = Convert.ToDecimal(txtUPrice.Text.Trim());
                    }

                    Det.Iadd_claim_itm = rps.Tus_itm_cd;
                    //Det.Iadd_claim_pb =;
                    //Det.Iadd_claim_pb_lvl = ;
                    //Det.Iadd_claim_val = ;
                    Det.Iadd_coll_itm = rps.Tus_itm_cd;
                    //Det.Iadd_coll_pb = ;
                    //Det.Iadd_coll_pb_lvl = ;
                    //Det.Iadd_coll_ser1 = ;
                    //Det.Iadd_coll_ser2 = ;,
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_val = ;
                    Det.Iadd_line = ItmLine++;
                    //Det.Iadd_ref_no =;
                    //Det.Iadd_seq = ;
                    Det.Iadd_stus = 3;
                    #endregion

                    confirmed_detList.Add(Det);
                }

                //try {
                //Update header
                // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
                // Searched_AdhodHeader.Iadh_app_dt = 
                // Searched_AdhodHeader.Iadh_stus = 1;
                Searched_AdhodHeader.Iadh_coll_by = Session["UserID"].ToString();
                Searched_AdhodHeader.Iadh_coll_dt = CHNLSVC.Security.GetServerDateTime().Date;
                Searched_AdhodHeader.Iadh_stus = 3;

                #region ADJ(-)
                //string AdjNumber = txtAdjustmentNo.Text.Trim();
                //string AdjNumber = "";
                //string manualNum = txtManualRefNo.Text.Trim();
                //string remarks = txtRemarks.Text.Trim();
                //string adj_base = ddlAdjBased.SelectedValue;
                //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
                //string adj_type = ddlAdjType_.SelectedValue;
                InventoryHeader inHeader = new InventoryHeader();


                inHeader.Ith_acc_no = "";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_10 = true;
                inHeader.Ith_anal_11 = true;
                inHeader.Ith_anal_12 = false; //update on 07-05-2013 on Chamal's advice
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = "";
                //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeader.Ith_channel = "";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();

                //inHeader.Ith_com ="";
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = "";
                inHeader.Ith_cre_when = DateTime.MinValue;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";

                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";


                inHeader.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeader.Ith_doc_date = CHNLSVC.Security.GetServerDateTime().Date;// DateTime.Today; //update on 07-05-2013 on Chamal's advice
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                inHeader.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeader.Ith_doc_year = DateTime.Today.Year;
                inHeader.Ith_entry_no = "";
                inHeader.Ith_entry_tp = "";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = "";
                inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true;
                inHeader.Ith_job_no = "";
                inHeader.Ith_loading_point = "";
                inHeader.Ith_loading_user = "";
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                //{
                //    inHeader.Ith_manual_ref = "N/A";
                //}
                //else
                //{
                //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                //}
                inHeader.Ith_manual_ref = "N/A";
                // inHeader.Ith_manual_ref ="";
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 1;
                inHeader.Ith_oth_loc = "";

                inHeader.Ith_remarks = "ADHOC CONFIRM";//"ADHOC Confirm";//txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeader.Ith_sbu = "INV";
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                //inHeader.Ith_seq_no =54;
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeader.Ith_vehi_no = "";

                inHeader.Ith_direct = false;

                //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_git_close = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_oth_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_oth_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_sub_tp = "SYS";
                inHeader.Ith_entry_tp = "SYS";
                inHeader.Ith_acc_no = "FGAP";
                inHeader.Ith_cate_tp = "FGAP";
                inHeader.Ith_remarks = "ADHOC CONFIRM";
                //------------------------------------------


                //*********************************
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
                //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                #endregion
                string AdjNumber = string.Empty;
                List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                Int32 effect = 0;
                //effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);//_recieptItem
                string GenReceiptNo = "";
                effect = CHNLSVC.Inventory.Save_FGAP_confirmation(Searched_AdhodHeader, confirmed_detList, receiptHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), out GenReceiptNo);

                if (effect < 0)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                    DispMsg("Failed to Confirmed. Error occured. Try Again!");
                    return;
                }
                else if (effect > 0)
                {
                    clearCompleateScreen();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);
                    //if (checkManualReceipt.Checked == true)
                    //{
                    //    MessageBox.Show("Confirmed Successfully! \n ADJ(-) NO. =" + AdjNumber );
                    //    if (checkManualReceipt.Checked == true)
                    //    {
                    //      Int32 EFF=  CHNLSVC.Inventory.UpdateManualDocNo(Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtManualReceiptNo.Text));
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Confirmed Successfully! \n ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);
                    //    try
                    //    {
                    //        ReportViewer _view = new ReportViewer();                        
                    //        _view.GlbReportName = "ReceiptPrints.rpt";
                    //        _view.GlbReportDoc = GenReceiptNo;
                    //        _view.GlbReportProfit = Session["UserDefProf"].ToString();
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //    catch(Exception ex){

                    //    }

                    //}             
                    DispMsg("Confirmed Successfully! \n ADJ(-) NO. :" + AdjNumber + ", Receipt No.=" + GenReceiptNo, "S");
                    ClearData();
                    try
                    {
                        //ReportViewer _view = new ReportViewer();

                        //Session["GlbReportName"] = string.Empty; //add on 14-June-2013
                        //_view.GlbReportName = string.Empty;////add on 14-June-2013

                        //BaseCls.GlbReportTp = "REC";
                        //_view.GlbReportName = "ReceiptPrints.rpt";
                        //_view.GlbReportDoc = GenReceiptNo;
                        //_view.GlbReportProfit = Session["UserDefProf"].ToString();
                        //_view.Show();
                        //_view = null;
                    }
                    catch (Exception ex)
                    {

                    }
                    //***********************************************************
                    try
                    {
                        //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //BaseCls.GlbReportTp = "OUTWARD";
                        //_view.GlbReportName = Session["UserCompanyCode"].ToString() == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                        //_view.GlbReportDoc = AdjNumber;//"AAZPG-DO-12-00123";
                        //_view.Show();
                        //_view = null;
                    }
                    catch (Exception ex)
                    {
                        DispMsg("Error in printing out document.", "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }
        private void fill_Transactions(RecieptHeader r_hdr)
        {
            try
            {
                //(to write to hpt_txn)
                HpTransaction tr = new HpTransaction();
                // tr.Hpt_acc_no = lblAccNo.Text.Trim();
                tr.Hpt_ars = 0;
                tr.Hpt_bal = 0;
                tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
                tr.Hpt_cre_by = Session["UserID"].ToString();
                tr.Hpt_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                tr.Hpt_dbt = 0;
                tr.Hpt_com = Session["UserCompanyCode"].ToString();
                tr.Hpt_pc = Session["UserDefProf"].ToString();
                if (r_hdr.Sar_is_oth_shop == true)
                {
                    tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + Session["UserDefProf"].ToString();
                    tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+GlbUserDefProf;   //"prefix-receiptNo-pc"

                }
                else
                {
                    tr.Hpt_desc = ("Payment receive").ToUpper();

                }
                if (r_hdr.Sar_is_mgr_iss)
                {
                    //"prefix-receiptNo-issues"
                    //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();

                }
                else
                { //"prefix-receiptNo"
                    //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
                }
                tr.Hpt_pc = Session["UserDefProf"].ToString();

                tr.Hpt_ref_no = "";
                tr.Hpt_txn_dt = CHNLSVC.Security.GetServerDateTime().Date;
                tr.Hpt_txn_ref = "";
                tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
                tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();

                if (Transaction_List == null)
                {
                    Transaction_List = new List<HpTransaction>();
                }
                Transaction_List.Add(tr);
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }
        protected void lbtnSendReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefNo.Text.Trim() == "" && ddlTransActionTp.SelectedValue == "1")
                {
                    txtRefNo.Focus();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter a Reference No!");
                    if (lblRefNo.Text == "Manual #")
                    {
                        DispMsg("Please enter a Manual No !");
                    }
                    else
                    {
                        DispMsg("Please enter a Reference No !");
                    }
                    return;
                }
                //Fill Request header
                InventoryAdhocHeader reqHdr = new InventoryAdhocHeader();
                if (ddlTransActionTp.SelectedValue == "2")
                {
                    reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    //reqHdr.Iadh_adj_no=
                    //reqHdr.Iadh_anal1=
                    //reqHdr.Iadh_anal2=
                    //reqHdr.Iadh_anal3=
                    //reqHdr.Iadh_anal4=
                    //reqHdr.Iadh_anal5=
                    //reqHdr.Iadh_app_by=
                    //reqHdr.Iadh_app_dt=
                    //reqHdr.Iadh_coll_by=
                    //reqHdr.Iadh_coll_dt=
                    reqHdr.Iadh_com = Session["UserCompanyCode"].ToString();
                    reqHdr.Iadh_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    reqHdr.Iadh_loc = Session["UserDefLoca"].ToString();
                    reqHdr.Iadh_pc = Session["UserDefProf"].ToString();
                    //reqHdr.Iadh_ref_no=
                    reqHdr.Iadh_req_by = Session["UserID"].ToString();
                    reqHdr.Iadh_req_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    //reqHdr.Iadh_seq
                    reqHdr.Iadh_stus = 0;
                    // reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
                    reqHdr.Iadh_tp = Convert.ToInt32(ddlTransActionTp.SelectedValue);

                    //add sachith add remark
                    reqHdr.Iadh_anal1 = txtRemarks.Text;

                }
                else if (ddlTransActionTp.SelectedValue == "1")
                {
                    reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    //reqHdr.Iadh_adj_no=
                    //reqHdr.Iadh_anal1=
                    //reqHdr.Iadh_anal2=
                    //reqHdr.Iadh_anal3=
                    //reqHdr.Iadh_anal4=
                    //reqHdr.Iadh_anal5=
                    reqHdr.Iadh_app_by = Session["UserID"].ToString();
                    reqHdr.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    //reqHdr.Iadh_coll_by=
                    //reqHdr.Iadh_coll_dt=
                    reqHdr.Iadh_com = Session["UserCompanyCode"].ToString();
                    reqHdr.Iadh_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    reqHdr.Iadh_loc = txtLocation.Text.Trim().ToUpper();
                    reqHdr.Iadh_pc = txtProfitCenter.Text.Trim().ToUpper();
                    //reqHdr.Iadh_ref_no=
                    //reqHdr.Iadh_req_by = GlbUserName;
                    //reqHdr.Iadh_req_dt =
                    //reqHdr.Iadh_seq
                    reqHdr.Iadh_stus = 1;

                    //reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
                    reqHdr.Iadh_tp = Convert.ToInt32(ddlTransActionTp.SelectedValue);
                    reqHdr.Iadh_anal1 = txtRemarks.Text;
                }

                Int32 effect = 0;
                if (AdhodDetList.Count > 0)
                {
                    if (Approved_SerialList == null)
                    {
                        Approved_SerialList = new List<ReptPickSerials>();
                    }

                    string RefNumber = "";
                    effect = CHNLSVC.Inventory.Save_Adhoc_Request(reqHdr, AdhodDetList, Approved_SerialList, out RefNumber);

                    if (effect > 0)
                    {

                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sent Successfully! Reference# :" + RefNumber);
                        DispMsg("Request Sent Successfully! Reference # :" + RefNumber, "S");
                        ClearData();
                        clearCompleateScreen();
                        try
                        {
                            if (reqHdr.Iadh_tp == 2)
                            {
                                //Session["GlbReportType"] = "";
                                //Session["OrNo"] = RefNumber;

                                //Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "SFixedAssetTransferNotes.rpt" : "FixedAssetTransferNotes.rpt";
                                //BaseCls.GlbReportHeading = "Fixed Asset Confirmation Report";
                                //Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "SFixedAssetTransferNotes.rpt" : "FixedAssetTransferNotes.rpt";
                                //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                            }
                            else if (reqHdr.Iadh_tp == 1)
                            {

                            }

                        }
                        catch (Exception EX)
                        {
                            return;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sending Failed!");
                        DispMsg("Sending Failed!");
                        return;
                    }
                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Items are added. Please add Items first!");
                    DispMsg("No Items are added. Please add Items first!");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lbtnSePriceBook_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                _serData = CHNLSVC.CommonSearch.GetPriceBookData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "PriceBook";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSePbLvl_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                _serData = CHNLSVC.CommonSearch.GetPriceLevelData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "PriceLevel";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeLocation_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _serData = CHNLSVC.CommonSearch.GetUserLocationSearchData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "UserLocation";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSePrCenter_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                _serData = CHNLSVC.CommonSearch.GetPC_SearchData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "AllProfitCenters";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeSts_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                _serData = CHNLSVC.CommonSearch.GetCompanyItemStatusData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "ItemStatus";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlFilterSts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string status = ddlFilterSts.SelectedValue;
                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                if (status == "Any")
                {
                    status = string.Empty;
                }
                serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(Session["UserCompanyCode"].ToString(), txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);

                dgvAvailableSerials.DataSource = new int[] { };
                if (serList != null)
                {
                    if (serList.Count > 0)
                    {
                        dgvAvailableSerials.DataSource = serList;
                    }
                }
                dgvAvailableSerials.DataBind();
                LoadAvailableSerialsStatus();
                DesableBtnAvaSerial();
                AvailableSerialList = serList;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }


        //private void grvAvailableSerials_CellClick
        //txtRefNo_KeyPress
        //txtRefNo_KeyDown() To txtPriceBook_KeyPress()
        protected void lbtnSeSendLoc_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _serData = CHNLSVC.CommonSearch.GetUserLocationSearchData(searchparams, null, null);
                dgvResult.DataSource = new int[] { };
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "SendUserLocation";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        // txtRefNo_MouseDoubleClick To txtRefNo_DoubleClick

        //private void grvAvailableSerials_DataSourceChanged
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lbtnRefNoOk_Click(null, null);
                if (Searched_AdhodHeader == null)
                {
                    return;
                }
                else if (Searched_AdhodHeader.Iadh_ref_no != "")
                {

                    if ((Searched_AdhodHeader.Iadh_tp == 2 && Searched_AdhodHeader.Iadh_stus == 0) || Searched_AdhodHeader.Iadh_tp == 1 && Searched_AdhodHeader.Iadh_stus == 1)
                    {
                        Searched_AdhodHeader.Iadh_stus = 4;
                        Searched_AdhodHeader.Iadh_app_by = Session["UserID"].ToString(); //rejected person
                        Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                        Int32 effect = 0;
                        effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                        if (effect > 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");
                            DispMsg("Request Cancelled Successfully ! Reference # :" + Searched_AdhodHeader.Iadh_ref_no, "S");
                            clearCompleateScreen();
                            ClearData();
                            return;
                        }
                        else
                        {
                            DispMsg("Not Cancelled!");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void ddlAction_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtProfitCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtProfitCenter.ToolTip = "";
                DataTable DT = CHNLSVC.General.GetPartyCodes(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper());
                if (DT == null)
                {
                    txtProfitCenter.Text = "";
                    DispMsg("Invalid Profit center.");
                    txtProfitCenter.Focus();
                    return;
                }
                if (DT.Rows.Count == 0)
                {
                    txtProfitCenter.Text = "";
                    DispMsg("Invalid Profit center.");
                    txtProfitCenter.Focus();
                }
                else
                {
                    txtProfitCenter.ToolTip = DT.Rows[0]["MPC_DESC"].ToString();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MasterLocation LOC = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtLocation.Text.Trim().ToUpper());
                if (LOC == null)
                {
                    txtLocation.Text = "";
                    txtLocation.ToolTip = "";
                    DispMsg("Invalid Location code");
                }
                else
                {
                    txtLocation.ToolTip = LOC.Ml_loc_desc;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void txtSendLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSendLoc.Text))
                {
                    string temploc = "";
                    string ucompany = Session["UserCompanyCode"].ToString();
                    string uprof = Session["UserDefLoca"].ToString();
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = null;
                    string sendloc = txtSendLoc.Text.ToString();
                    if (sendloc != "")
                    {
                        _result = CHNLSVC.MsgPortal.GetFixAssetLocation_NEW(ucompany, sendloc);
                    }
                    else
                    {
                        _result = CHNLSVC.MsgPortal.GetFixAssetLocation_NEW(ucompany, uprof);
                    }


                    //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ml_loc_cd"].ToString()))
                        {
                            if (txtSendLoc.Text.ToUpper() == row["ml_loc_cd"].ToString())
                            {
                                temploc = txtSendLoc.Text.ToUpper();
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtSendLoc.ToolTip = toolTip;
                    }
                    else
                    {
                        txtSendLoc.ToolTip = "";
                        txtSendLoc.Text = "";
                        txtSendLoc.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid send location !!!')", true);
                        txtSendLoc.Text = (temploc);
                        return;
                    }
                }
                else
                {
                    txtSendLoc.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnAddGridItem_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblSts = row.FindControl("lblSts") as Label;
                Label lblQty = row.FindControl("lblQty") as Label;
                Label lblUnitPrice = row.FindControl("lblUnitPrice") as Label;
                #region

                SelectedItemCD = lblItmCd.Text;
                Int32 reqStatus = Convert.ToInt32(lblSts.Text);
                Decimal req_qty = Convert.ToDecimal(lblQty.Text);
                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                string status = string.Empty;
                serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().ToUpper(), SelectedItemCD, status);
                AvailableSerialList = new List<ReptPickSerials>();

                if (serList == null)
                {
                    DispMsg("No available items in the location.");
                    return;
                }
                if (serList.Count < 1)
                {
                    DispMsg("No available items in the location.");
                    return;
                }

                if (reqStatus == 0)//only Pending FIX ASSET
                {
                    AvailableSerialList = serList;

                    string location = txtSendLoc.Text.Trim().ToUpper();
                    InventoryAdhocHeader Header;
                    Det_list_selected = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().ToUpper(), 2, txtRefNo.Text.Trim(), 0, out Header);
                }
                //else if (reqStatus == 1 && ddlReuestType.SelectedValue == "2") //Approved FIX ASSET   
                else if (reqStatus == 1 && ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS") //Approved FIX ASSET
                {
                    //Label lblApprovedSerID = (Label)row.Cells[7].FindControl("lblApprSerID");
                    // Int32 ApprSerID = Convert.ToInt32(lblApprovedSerID.Text.Trim());


                    var _dup = from _l in AdhodDetList
                               where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == reqStatus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                               select _l;

                    foreach (InventoryAdhocDetail det in _dup)
                    {
                        var _dup2 = from _l in serList
                                    where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                                    select _l;


                        AvailableSerialList.AddRange(_dup2);

                    }
                    // serList= _dup.ToList<ReptPickSerials>();

                }
                // else if (reqStatus == 1 && ddlReuestType.SelectedValue == "1")// Approved FGAP 
                else if (reqStatus == 1 && ddlTransActionTp.SelectedItem.Text == "FGAP")// Approved FGAP 
                {
                    //serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                    Decimal unitPrice = Convert.ToDecimal(lblUnitPrice.Text);
                    foreach (ReptPickSerials rpc in serList)//req_qty
                    {
                        rpc.Tus_unit_price = unitPrice;

                    }

                    AvailableSerialList = serList;
                }
                else
                {
                    AvailableSerialList = null;
                }
                //-----------------------------------------------
                if (AvailableSerialList != null)
                {
                    foreach (GridViewRow grvRow in dgvApproveItms.Rows)
                    {
                        Label lblSerId = grvRow.FindControl("lblSerId") as Label;
                        string serID = lblSerId.Text;
                        AvailableSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(serID));//remove already added serials
                    }
                }
                //------------------------------------------------
                dgvAvailableSerials.DataSource = new int[] { };
                if (AvailableSerialList.Count > 0)
                {
                    dgvAvailableSerials.DataSource = AvailableSerialList;
                }
                dgvAvailableSerials.DataBind();
                LoadAvailableSerialsStatus();
                DesableBtnAvaSerial();
                #endregion

            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void lbtnDelGridItem_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblSts = row.FindControl("lblSts") as Label;
                Label lblQty = row.FindControl("lblQty") as Label;
                Label lblUnitPrice = row.FindControl("lblUnitPrice") as Label;
                Int32 reqStatus = Convert.ToInt32(lblSts.Text);
                string DelItemCD = lblItmCd.Text;
                deleteItem(row); //DELETE ITEM.
                //**********
                dgvAvailableSerials.DataSource = new int[] { };
                dgvAvailableSerials.DataSource = new List<ReptPickSerials>();
                dgvAvailableSerials.DataBind();

                Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DelItemCD);//&& x.Iadd_anal2 == DelModle

                dgvApproveItms.DataSource = new int[] { };
                if (Approved_SerialList.Count > 0)
                {
                    dgvApproveItms.DataSource = Approved_SerialList;
                }
                dgvApproveItms.DataBind();
                LoadApproveSerialStatus();
                //*********
                //---------------------------------------------------------------------------------
                #region set approve, collect,price difference, reciept amounts

                if (ddlTransActionTp.SelectedItem.Text == "FGAP")// Approved FGAP 
                {
                    #region
                    Decimal totalApprovedVal = 0;
                    foreach (InventoryAdhocDetail detail in AdhodDetList)
                    {
                        totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                    }

                    txtCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                    Decimal priceDiference = Convert.ToDecimal(txtCollectVal.Text) - Convert.ToDecimal(txtAppVal.Text);
                    txtPriceDef.Text = string.Format("{0:n2}", priceDiference);

                    //lblReceiptAmt.Text = priceDiference.ToString();
                    if (priceDiference > 0)
                    {
                        lbtnConfAppser.Enabled = false;
                        lbtnConfAppser.CssClass = "buttoncolorleft";
                        lbtnConfAppser.OnClientClick = "return Enable();";

                        lbtnPaymentComplete.Visible = true; //panel_manualReceipt.Visible = true;

                        //lbtnSendReq.Enabled = false;
                        //lbtnSendReq.CssClass = "buttoncolorleft";
                        //lbtnSendReq.OnClientClick = "return Enable();";

                        txtRecVal.Text = priceDiference.ToString();

                        ucPaymodes.PageClear();
                        ucPaymodes.payModeClear();
                        ucPaymodes.InvoiceType = "CS";
                        //ucPayModes1.LoadPayModes();
                        ucPaymodes.TotalAmount = priceDiference;
                        ucPaymodes.LoadData();

                    }
                    else
                    {
                        txtRecVal.Text = string.Format("{0:n2}", 0);

                        lbtnConfAppser.Enabled = true;
                        lbtnConfAppser.CssClass = "buttonUndocolorLeft";
                        lbtnConfAppser.OnClientClick = "return ConfirmConfirm()";

                        lbtnPaymentComplete.Visible = false;// panel_manualReceipt.Visible = false;
                        ucPaymodes.PageClear();
                        ucPaymodes.payModeClear();
                        ucPaymodes.InvoiceType = "CS";
                        //ucPayModes1.LoadPayModes();
                        ucPaymodes.TotalAmount = 0;
                        ucPaymodes.LoadData();
                        //ucPayModes1.LoadPayModes();
                    }
                    // }
                    #endregion
                }

                #endregion
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }


        private void deleteItem(GridViewRow row)
        {
            try
            {
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblModel = row.FindControl("lblModel") as Label;
                string DelItemCD = lblItmCd.Text;
                string DelModle = lblModel.Text;
                //if (ddlAction.SelectedValue == "Request")
                if (ddlAction.SelectedItem.Text == "NEW REQUEST")
                {
                    AdhodDetList.RemoveAll(x => x.Iadd_claim_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
                }
                else
                {
                    AdhodDetList.RemoveAll(x => x.Iadd_app_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
                }

                dgvItmDes.DataSource = new int[] { };
                if (AdhodDetList.Count > 0)
                {
                    dgvItmDes.DataSource = AdhodDetList;
                }
                dgvItmDes.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void lbtnDelApproveItm_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblSerId = row.FindControl("lblSerId") as Label;

                Int32 serID = Convert.ToInt32(lblSerId.Text);
                string DEL_ItemCD = lblItmCd.Text;
                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                var _dup = from _l in Approved_SerialList
                           where _l.Tus_itm_cd == DEL_ItemCD && _l.Tus_ser_id == serID
                           select _l;
                AvailableSerialList.AddRange(_dup);
                Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DEL_ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle

                dgvApproveItms.DataSource = new int[] { };
                if (Approved_SerialList.Count > 0)
                {
                    dgvApproveItms.DataSource = Approved_SerialList;
                }
                dgvApproveItms.DataBind();
                LoadApproveSerialStatus();
                if (dgvAvailableSerials.Rows.Count > 0)
                {
                    Label lblItmCds = dgvAvailableSerials.Rows[0].FindControl("lblItmCd") as Label;
                    if (dgvAvailableSerials.Rows.Count > 0 && lblItmCds.Text == DEL_ItemCD)
                    {
                        dgvAvailableSerials.DataSource = new int[] { };
                        if (AvailableSerialList.Count > 0)
                        {
                            dgvAvailableSerials.DataSource = AvailableSerialList;
                        }
                        dgvAvailableSerials.DataBind();
                        LoadAvailableSerialsStatus();
                        DesableBtnAvaSerial();
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void lbtnAddAvaSerial_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblSerId = row.FindControl("lblSerId") as Label;

                Int32 serID = Convert.ToInt32(lblSerId.Text);
                string DEL_ItemCD = lblItmCd.Text;
                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "NEW REQUEST")
                {
                    Boolean hasRequest = CHNLSVC.Inventory.CheckPreRequestAdhocSer(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serID);
                    if (hasRequest == true)
                    {
                        DispMsg("This serial has been requested already!");
                        return;
                    }
                }

                List<ReptPickSerials> serList = new List<ReptPickSerials>();

                var _dup = from _l in AvailableSerialList
                           where _l.Tus_itm_cd == DEL_ItemCD && _l.Tus_ser_id == serID
                           select _l;
                Approved_SerialList.AddRange(_dup);

                AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == DEL_ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle

                dgvApproveItms.DataSource = new int[] { };
                if (Approved_SerialList.Count > 0)
                {
                    dgvApproveItms.DataSource = Approved_SerialList;
                }
                dgvApproveItms.DataBind();
                LoadApproveSerialStatus();
                dgvAvailableSerials.DataSource = new int[] { };
                if (AvailableSerialList.Count > 0)
                {
                    dgvAvailableSerials.DataSource = AvailableSerialList;
                }
                dgvAvailableSerials.DataBind();
                LoadAvailableSerialsStatus();
                DesableBtnAvaSerial();

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        protected void dgvAvailableSerials_DataBound(object sender, EventArgs e)
        {

        }

        protected void dgvAvailableSerials_DataBinding(object sender, EventArgs e)
        {
            if (Searched_AdhodHeader == null)
            {
                return;
            }
            try
            {
                if (Searched_AdhodHeader.Iadh_tp == 2 && Searched_AdhodHeader.Iadh_stus == 1)
                {
                    foreach (GridViewRow gvr in this.dgvAvailableSerials.Rows)
                    {
                        string itemCode = (gvr.FindControl("lblItmCd") as Label).Text;
                        string serID = (gvr.FindControl("lblSerId") as Label).Text;
                        if (Det_list_selected != null)
                        {
                            var _exist = from _dup in Det_list_selected
                                         where _dup.Iadd_claim_itm == itemCode //&& _dup.Sccd_brd == obj.Sccd_brd 
                                         select _dup;

                            if (_exist.Count() != 0)
                            {
                                foreach (InventoryAdhocDetail det in _exist)
                                {
                                    if (det.Iadd_anal4 == Convert.ToInt32(serID))
                                    {
                                        // gvr.DefaultCellStyle.ForeColor = Color.LightSalmon;
                                        //************************
                                        var _dup = from _l in AvailableSerialList
                                                   where _l.Tus_itm_cd == itemCode && _l.Tus_ser_id == Convert.ToInt32(serID)
                                                   select _l;


                                        // serList= _dup.ToList<ReptPickSerials>();
                                        Approved_SerialList.AddRange(_dup);

                                        AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == itemCode && x.Tus_ser_id == Convert.ToInt32(serID));//&& x.Iadd_anal2 == DelModle

                                        dgvApproveItms.DataSource = new int[] { };
                                        if (Approved_SerialList.Count > 0)
                                        {
                                            dgvApproveItms.DataSource = Approved_SerialList;
                                        }
                                        dgvApproveItms.DataBind();
                                        LoadApproveSerialStatus();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            { DispMsg("Error Occurred while processing...\n", "E"); }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbtnAppAvaSer.Enabled = false;
                lbtnAppAvaSer.CssClass = "buttoncolorleft";
                lbtnAppAvaSer.OnClientClick = "return Enable();";

                lbtnRejAvaSer.Enabled = false;
                lbtnRejAvaSer.CssClass = "buttoncolorleft";
                lbtnRejAvaSer.OnClientClick = "return Enable();";

                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS")
                {
                    if (ddlAction.SelectedItem.Text == "APPROVE REQUEST")
                    {
                        lbtnSave.Enabled = false;
                        lbtnSave.CssClass = "buttoncolorleft";
                        lbtnSave.OnClientClick = "return Enable();";
                    }
                    if (ddlAction.SelectedItem.Text == "CONFIRMATION")
                    {
                        lbtnSave.Enabled = false;
                        lbtnSave.CssClass = "buttoncolorleft";
                        lbtnSave.OnClientClick = "return Enable();";
                    }
                }
                ClearDetails();

                txtRefNo.Text = "";
                // clearCompleateScreen();     
                lbtnPaymentComplete.Visible = false;
                if (ddlAction.SelectedItem.Text == "APPROVE REQUEST")
                {
                    lbtnCancel.Enabled = false;
                    lbtnCancel.CssClass = "buttoncolorleft";
                    lbtnCancel.OnClientClick = "return Enable();";

                    if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), string.Empty, "INV5") == false)
                    {
                        // MessageBox.Show("No Permission Granted! This is Head office task. \n(Rquired permission type: 'INV5')");
                        lbtnSave.Enabled = false;
                        lbtnSave.CssClass = "buttoncolorleft";
                        lbtnSave.OnClientClick = "return Enable();";
                        return;
                    }
                    else
                    {
                        if (ddlAction.SelectedItem.Text != "CONFIRMATION" && ddlAction.SelectedItem.Text != "APPROVE REQUEST")
                        {
                            lbtnSave.Enabled = true;
                            lbtnSave.CssClass = "buttonUndocolorLeft";
                            lbtnSave.OnClientClick = "return ConfSendReq()";
                        }

                    }
                    //----------------
                    //pnlPayss.Visible = false;
                    //panel_payment.Visible = false;
                    //ucPayModes1.Visible = false;               

                    //----------------------
                }
                else
                {
                    lbtnCancel.Enabled = false;
                    lbtnCancel.CssClass = "buttoncolorleft";
                    lbtnCancel.OnClientClick = "return Enable();";
                }
                try
                {
                    if (ddlTransActionTp.SelectedValue == "1")
                    {
                        if (ddlAction.SelectedItem.Text == "Request")
                        {
                            DispMsg("Action not valid for FGAP");
                        }
                        if (ddlAction.SelectedItem.Text == "CONFIRMATION")
                        {
                            //lbtnSendReq.Enabled = false;
                            //lbtnSendReq.CssClass = "buttoncolorleft";
                            //lbtnSendReq.OnClientClick = "return Enable();";

                            txtLocation.Text = Session["UserDefLoca"].ToString();
                            txtProfitCenter.Text = Session["UserDefLoca"].ToString();
                        }
                        else
                        {
                            //lbtnSendReq.Enabled = true;
                            //lbtnSendReq.CssClass = "buttonUndocolorLeft";
                            //lbtnSendReq.OnClientClick = "";
                        }
                    }

                    if (ddlTransActionTp.SelectedValue == "2")
                    {
                        if (ddlAction.SelectedItem.Text == "Approve" || ddlAction.SelectedItem.Text == "CONFIRMATION")
                        {
                            //lbtnSendReq.Enabled = false;
                            //lbtnSendReq.CssClass = "buttoncolorleft";
                            //lbtnSendReq.OnClientClick = "return Enable();";
                        }
                        else
                        {
                            //lbtnSendReq.Enabled = true;
                            //lbtnSendReq.CssClass = "buttonUndocolorLeft";
                            //lbtnSendReq.OnClientClick = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    DispMsg("Error Occurred while processing...\n", "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing...\n", "E");
            }
        }

        private void LoadAvailableSerialsStatus()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow item in dgvAvailableSerials.Rows)
            {
                Label lblStsDesc = item.FindControl("lblStsDesc") as Label;
                Label lblSts = item.FindControl("lblSts") as Label;
                if (status_list != null)
                {
                    lblStsDesc.Text = status_list.Where(c => c.Key == lblSts.Text).FirstOrDefault().Value;
                }
            }
        }

        private void LoadApproveSerialStatus()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (GridViewRow item in dgvApproveItms.Rows)
            {
                Label lblStsDesc = item.FindControl("lblStsDesc") as Label;
                Label lblSts = item.FindControl("lblSts") as Label;
                if (status_list != null)
                {
                    lblStsDesc.Text = status_list.Where(c => c.Key == lblSts.Text).FirstOrDefault().Value;
                }
            }
        }

        private void DesableBtnAvaSerial()
        {
            bool _enable = false;
            if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "NEW REQUEST" && txtStatus.Text == "NEW")
            {
                _enable = true;
            }
            if (ddlTransActionTp.SelectedItem.Text == "FGAP" && ddlAction.SelectedItem.Text == "APPROVE REQUEST" && txtStatus.Text == "NEW")
            {
                _enable = true;
            }
            if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "APPROVE REQUEST" && txtStatus.Text == "PENDING-P")
            {
                _enable = true;
            }

            foreach (GridViewRow row in dgvAvailableSerials.Rows)
            {
                LinkButton lbtnAddAvaSerial = row.FindControl("lbtnAddAvaSerial") as LinkButton;
                lbtnAddAvaSerial.Enabled = _enable;
                lbtnAddAvaSerial.CssClass = _enable ? "buttonUndocolorLeft" : "buttoncolorleft";
            }
        }
        private void ClearDetails()
        {
            dgvApproveItms.DataSource = new int[] { };
            dgvItmDes.DataSource = new int[] { };
            dgvAvailableSerials.DataSource = new int[] { };
            dgvItmDes.DataBind();
            dgvApproveItms.DataBind();
            dgvAvailableSerials.DataBind();
            txtStatus.Text = "NEW";
        }

        protected void txtStatus_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRefNo.Text))
            {
                lbtnCancel.Enabled = false;
                lbtnCancel.CssClass = "buttoncolorleft";
                lbtnCancel.OnClientClick = "return Enable()";

                lbtnSave.Enabled = false;
                lbtnSave.CssClass = "buttoncolorleft";
                lbtnSave.OnClientClick = "return Enable()";

                lbtnSendReq.Enabled = false;
                lbtnSendReq.CssClass = "buttoncolorleft";
                lbtnSendReq.OnClientClick = "return Enable()";

                lbtnAppAvaSer.Enabled = false;
                lbtnAppAvaSer.CssClass = "buttoncolorleft";
                lbtnAppAvaSer.OnClientClick = "return Enable()";

                lbtnRejAvaSer.Enabled = false;
                lbtnRejAvaSer.CssClass = "buttoncolorleft";
                lbtnRejAvaSer.OnClientClick = "return Enable()";

                lbtnConfAppser.Enabled = false;
                lbtnConfAppser.CssClass = "buttoncolorleft";
                lbtnConfAppser.OnClientClick = "return Enable()";

                lbtnRejAppSer.Enabled = false;
                lbtnRejAppSer.CssClass = "buttoncolorleft";
                lbtnRejAppSer.OnClientClick = "return Enable()";

                lblRefNo.Text = "Ref #";

                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "NEW REQUEST" && txtStatus.Text == "PENDING-P")
                {
                    lbtnCancel.Enabled = true;
                    lbtnCancel.CssClass = "buttonUndocolorLeft";
                    lbtnCancel.OnClientClick = "return ConfirmCancel()";
                }
                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "APPROVE REQUEST" && txtStatus.Text == "PENDING-P")
                {
                    lbtnAppAvaSer.Enabled = true;
                    lbtnAppAvaSer.CssClass = "buttonUndocolorLeft";
                    lbtnAppAvaSer.OnClientClick = "return ConfirmApprove()";

                    lbtnRejAvaSer.Enabled = true;
                    lbtnRejAvaSer.CssClass = "buttonUndocolorLeft";
                    lbtnRejAvaSer.OnClientClick = "return ConfirmReject()";
                }
                if (ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS" && ddlAction.SelectedItem.Text == "CONFIRMATION" && txtStatus.Text == "APPROVED-A")
                {
                    lbtnConfAppser.Enabled = true;
                    lbtnConfAppser.CssClass = "buttonUndocolorLeft";
                    lbtnConfAppser.OnClientClick = "return ConfirmConfirm()";

                    lbtnRejAppSer.Enabled = true;
                    lbtnRejAppSer.CssClass = "buttonUndocolorLeft";
                    lbtnRejAppSer.OnClientClick = "return ConfirmReject()";
                }
                //if (ddlTransActionTp.SelectedItem.Text == "FGAP" && ddlAction.SelectedItem.Text == "APPROVE REQUEST" && txtStatus.Text == "APPROVED-A")
                //{
                //    lbtnRejAppSer.Enabled = true;
                //    lbtnRejAppSer.CssClass = "buttonUndocolorLeft";
                //    lbtnRejAppSer.OnClientClick = "return ConfirmReject()";
                //}
                if (ddlTransActionTp.SelectedItem.Text == "FGAP" && ddlAction.SelectedItem.Text == "CONFIRMATION" && txtStatus.Text == "APPROVED-A")
                {
                    lbtnConfAppser.Enabled = true;
                    lbtnConfAppser.CssClass = "buttonUndocolorLeft";
                    lbtnConfAppser.OnClientClick = "return ConfirmConfirm()";

                    lbtnRejAppSer.Enabled = true;
                    lbtnRejAppSer.CssClass = "buttonUndocolorLeft";
                    lbtnRejAppSer.OnClientClick = "return ConfirmReject()";
                }
                if (txtStatus.Text == "REJECTED-R")
                {
                    lbtnRejAppSer.Enabled = false;
                    lbtnRejAppSer.CssClass = "buttoncolorleft";
                    lbtnRejAppSer.OnClientClick = "return Enable()";

                    lbtnRejAvaSer.Enabled = false;
                    lbtnRejAvaSer.CssClass = "buttoncolorleft";
                    lbtnRejAvaSer.OnClientClick = "return Enable()";
                }

            }
        }

        private void ClearData()
        {
            txtRefNo.Text = "";
            txtStatus.Text = "NEW";
            txtStatus_TextChanged(null, null);
            txtDate.Text = "";
            txtPbLvl.Text = "";
            txtPriceBook.Text = "";
            txtLocation.Text = "";
            txtProfitCenter.Text = "";
            txtUPrice.Text = "";
            dgvItmDes.DataSource = new int[] { };
            dgvAvailableSerials.DataSource = new int[] { };
            dgvApproveItms.DataSource = new int[] { };
            dgvItmDes.DataBind();
            dgvAvailableSerials.DataBind();
            dgvApproveItms.DataBind();
            BindFilterAction();
            txtRemarks.Text = "";
        }

        private void LoadAvailableSerial()
        {
            dgvAvailableSerials.DataSource = new int[] { };
            dgvAvailableSerials.DataBind();
            foreach (GridViewRow row in dgvItmDes.Rows)
            {
                Label lblItmCd = row.FindControl("lblItmCd") as Label;
                Label lblSts = row.FindControl("lblSts") as Label;
                Label lblQty = row.FindControl("lblQty") as Label;
                Label lblUnitPrice = row.FindControl("lblUnitPrice") as Label;
                #region

                SelectedItemCD = lblItmCd.Text;
                Int32 reqStatus = Convert.ToInt32(lblSts.Text);
                Decimal req_qty = Convert.ToDecimal(lblQty.Text);
                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                string status = string.Empty;
                serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().Trim().ToUpper(), SelectedItemCD, status);
                AvailableSerialList = new List<ReptPickSerials>();



                if (reqStatus == 0)//only Pending FIX ASSET
                {
                    AvailableSerialList = serList;

                    string location = txtSendLoc.Text.Trim().ToUpper();
                    InventoryAdhocHeader Header;
                    Det_list_selected = CHNLSVC.Inventory.GET_adhocDET_byRefNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString().Trim().ToUpper(), 2, txtRefNo.Text.Trim(), 0, out Header);
                }
                //else if (reqStatus == 1 && ddlReuestType.SelectedValue == "2") //Approved FIX ASSET   
                else if (reqStatus == 1 && ddlTransActionTp.SelectedItem.Text == "FIXED ASSETS") //Approved FIX ASSET
                {
                    //Label lblApprovedSerID = (Label)row.Cells[7].FindControl("lblApprSerID");
                    // Int32 ApprSerID = Convert.ToInt32(lblApprovedSerID.Text.Trim());


                    var _dup = from _l in AdhodDetList
                               where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == reqStatus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                               select _l;

                    foreach (InventoryAdhocDetail det in _dup)
                    {
                        var _dup2 = from _l in serList
                                    where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                                    select _l;


                        AvailableSerialList.AddRange(_dup2);

                    }
                    // serList= _dup.ToList<ReptPickSerials>();

                }
                // else if (reqStatus == 1 && ddlReuestType.SelectedValue == "1")// Approved FGAP 
                else if (reqStatus == 1 && ddlTransActionTp.SelectedItem.Text == "FGAP")// Approved FGAP 
                {
                    //serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                    Decimal unitPrice = Convert.ToDecimal(lblUnitPrice.Text);
                    foreach (ReptPickSerials rpc in serList)//req_qty
                    {
                        rpc.Tus_unit_price = unitPrice;

                    }

                    AvailableSerialList = serList;
                }
                else
                {
                    AvailableSerialList = null;
                }
                //-----------------------------------------------
                if (AvailableSerialList != null)
                {
                    foreach (GridViewRow grvRow in dgvApproveItms.Rows)
                    {
                        Label lblSerId = grvRow.FindControl("lblSerId") as Label;
                        string serID = lblSerId.Text;
                        AvailableSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(serID));//remove already added serials
                    }
                }
                //------------------------------------------------
                dgvAvailableSerials.DataSource = new int[] { };
                if (AvailableSerialList.Count > 0)
                {
                    dgvAvailableSerials.DataSource = AvailableSerialList;
                }
                dgvAvailableSerials.DataBind();
                LoadAvailableSerialsStatus();
                DesableBtnAvaSerial();
                #endregion
            }
        }

        private void LoadLocationDetails()
        {
            string ucompany = Session["UserCompanyCode"].ToString();
            string uprof = Session["UserDefLoca"].ToString();


            DataTable serr = CHNLSVC.MsgPortal.GetFixAssetLocation_Other(ucompany, uprof);
            string loc = "";
            if (serr.Rows.Count > 0)
            {
                foreach (DataRow row in serr.Rows)
                {
                    if (!string.IsNullOrEmpty(row["ml_fx_loc"].ToString()))
                    {
                        loc = row["ml_fx_loc"].ToString();
                        DataTable serrr = CHNLSVC.MsgPortal.GetFixAssetLocation_NEW(ucompany, row["ml_fx_loc"].ToString());
                        if (serrr.Rows.Count > 0)
                        {
                            foreach (DataRow row1 in serrr.Rows)
                            {
                                if (!string.IsNullOrEmpty(row1["ml_loc_cd"].ToString()))
                                {

                                    txtSendLoc.Text = row1["ml_loc_cd"].ToString();
                                    Session["UserFixedLoc"] = row1["ml_loc_cd"].ToString();
                                }
                                else
                                {
                                    DispMsg("Fixed Asset Location not setup for this location. Please contact inventory dept.", "W");
                                }
                            }
                        }
                        else
                        {
                            DispMsg("Fixed Asset Location not setup for this location. Please contact inventory dept.", "W");
                        }
                    }
                    else
                    {
                        DispMsg("Fixed Asset Location not setup for this location. Please contact inventory dept.", "W");
                    }
                }
            }
            else
            {
                DispMsg("Fixed Asset Location not setup for this location. Please contact inventory dept.", "W");
            }

        }
    }
}